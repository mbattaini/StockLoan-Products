// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class MedalistMain
	{    
		private string countryCode;    

		private string dbCnStr;
		private SqlConnection dbCn = null;

		private Thread mainThread = null;

		private static bool isStopped = true;
		private static string tempPath;

		private PositionAgent positionAgent;
		private SubstitutionAgent substitutionAgent;

		public MedalistMain(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [MedalistMain.MedalistMain]", Log.Error, 1);
			} 

			countryCode = Standard.ConfigValue("CountryCode", "US");
			Log.Write("Using country code: " + countryCode + " [MedalistMain.MedalistMain]", 2);      
      
			if (Standard.ConfigValueExists("TempPath"))
			{
				tempPath = Standard.ConfigValue("TempPath");

				if (!Directory.Exists(tempPath))
				{
					Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [MedalistMain.MedalistMain]", Log.Error, 1);
					tempPath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
				Log.Write("A configuration value for TempPath has not been provided. [MedalistMain.MedalistMain]", Log.Information, 1);
				tempPath = Directory.GetCurrentDirectory();
			}

			Log.Write("Temporary files will be staged at " + tempPath + ". [MedalistMain.MedalistMain]", 2);    
		}

		~MedalistMain()
		{
			if (dbCn != null)
			{
				dbCn.Dispose();
			}
		}

		public void Start()
		{
			isStopped = false;

			if ((mainThread == null) || (!mainThread.IsAlive)) // Must create new thread.
			{
				mainThread = new Thread(new ThreadStart(MedalistMainLoop));
				mainThread.Name = "Main";
				mainThread.Start();

				Log.Write("Start command issued with new main thread. [MedalistMain.Start]", 3);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Start command issued with main thread already running. [MedalistMain.Start]", 3);
			}
		}

		public void Stop()
		{
			isStopped = true;

			if (mainThread == null)
			{
				Log.Write("Stop command issued, main thread never started. [MedalistMain.Stop]", 3);
			}
			else if (mainThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				mainThread.Abort();
				Log.Write("Stop command issued, sleeping main thread aborted. [MedalistMain.Stop]", 3);
			}
			else
			{
				Log.Write("Stop command issued, main thread is still active. [MedalistMain.Stop]", 3);
			}
		}

		private void MedalistMainLoop()
		{
			while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
			{
				Log.Write("Start of cycle. [MedalistMain.MedalistMainLoop]", 2);
				KeyValue.Set("MedalistMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);				
				
				BizDatesSet(Standard.HolidayType.Any);
				BizDatesSet(Standard.HolidayType.Bank);
				BizDatesSet(Standard.HolidayType.Exchange);

				if (Master.BizDateExchange.Equals(KeyValue.Get("S3StartOfDayDate", "2001-01-01", dbCnStr)))
				{				
					Master.IsSubstitutionActive = true;
				}
				else
				{
					Master.IsSubstitutionActive = false;
				}

				Master.IsSubstitutionDisabled = bool.Parse(KeyValue.Get("S3Disabled", "false", dbCnStr));

				// Roll open contracts to the current banking business date (BizDateBank).
				ContractBizDateRoll();
				if (isStopped) break;
        
				// Set open recalls to the correct status for the day.
				RecallBizDateSet();
				if (isStopped) break;
				
				// Resets bank loan position data to match DTC records.
				BankLoanDtcReset("0234");
				if (isStopped) break;
				
				BankLoanReportsLoad();
				if(isStopped) break;

				// Close pending and requested pledges; revert pending and requested releases to pledge made state.
				BankLoanBizDateRoll();
				if (isStopped) break;

				// Perform Stock Control Movements based upon Loan type
				BankLoanStockMovementActivity();
				if (isStopped) break;

				// Make the Easy Borrow list for the current trade date (BizDateExchange).
				EasyBorrowListMake();
				if (isStopped) break;

				// Do the Easy Borrow file for the current trade date (BizDateExchange).
				EasyBorrowFileDo();
				if (isStopped) break;
			
				// Do the Inventory Funding Rates Roll
				InventoryFundingRatesRoll();
				if (isStopped) break;
				
				// Send Easy Borrow e-mail notification for the current trade date (BizDateExchange).
				MailSendEasy();
				if (isStopped) break;

				// Send Threshold list change e-mail notification for the current business date (BizDate).
				MailSendThreshold();
				if (isStopped) break;
				
				ShortSaleNegativeRebateBillingSnapShot();
				if (isStopped) break;
				
				MailSendShortSaleAccountsCovered();
				if (isStopped) break;

				SecuritiesInAccountsBlackList();
				if (isStopped) break;

				ShortSaleLocatesAutoEmail();
				if (isStopped) break;

				ShortSaleDailyQuantitiesPurge();
				if (isStopped) break;

				ShortSaleFailsEmail();
				if (isStopped) break;

				KeyValue.Set("MedalistMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
				Log.Write("End of cycle. [MedalistMain.MedalistMainLoop]", 2);

				if (!isStopped)
				{
					Thread.Sleep(RecycleInterval());
				}
			}
		}

		private void BankLoanReportsLoad()
		{
			BankLoanReports bankLoanReports = new BankLoanReports(dbCnStr);
			DataSet dataSet = new DataSet();
			string fileLoadDate = "";
			string listMakeTime = "";

			SqlConnection dbCn = new SqlConnection(dbCnStr);
			
			try
			{			
				
				SqlDataAdapter dataAdapter = null;
				
				SqlCommand dbCmd = new SqlCommand("spBankLoanReportsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "BankLoanReports");				
			}
			catch (Exception e)
			{
				Log.Write(e.Message + "[MedalistMain.BankLoanReportsLoad]", Log.Error, 2);
			}
						
			listMakeTime = KeyValue.Get("BankLoanReportsLoadTime", "11:00", dbCn);

			if (listMakeTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) >= 0) // It is time to do it.
			{
				Log.Write("Must wait until after " + listMakeTime + " UTC to load bank laon reports for " + Master.BizDate + ". [MedalistMain.BankLoanReportsLoad]", Log.Information, 2);				
				return;
			}
			else if (Master.ContractsBizDate.Equals(KeyValue.Get("BankLoanReportsPurgeBizDate", "2001-01-01", dbCn)))
			{
				Log.Write("Bank Loan reports purge done for: " + Master.ContractsBizDate + ". [MedalistMain.BankLoanReportsLoad]", Log.Information, 2);								
			}
			else if (!Master.ContractsBizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) // Is the day to do it.
			{
				Log.Write("Bank Loan reports must wait until current buisness date. [MedalistMain.BankLoanReportsLoad]", Log.Information, 2);				
				return;
			}
			else
			{
				bankLoanReports.Purge("", "");			
				KeyValue.Set("BankLoanReportsPurgeBizDate", Master.ContractsBizDate, dbCn);
			}

			foreach (DataRow dataRow in dataSet.Tables["BankLoanReports"].Rows)
			{											
				try
				{
					if (!Tools.IsDate(dataRow["FileLoadDate"].ToString()).Equals(""))
					{
						fileLoadDate = Tools.FormatDate(dataRow["FileLoadDate"].ToString(), Standard.DateFormat);
					}
					else
					{
						fileLoadDate = "2001-01-01";
					}								

					if (fileLoadDate.Equals(Master.ContractsBizDate))
					{
						Log.Write("Report " + dataRow["ReportName"].ToString() + " for " + dataRow["BookGroup"].ToString() + " is current.", 2);
					}
					else
					{
						bankLoanReports.Load(
							dataRow["BookGroup"].ToString(),					
							dataRow["ReportHost"].ToString(),
							dataRow["ReportHostUserId"].ToString(),
							dataRow["ReportHostPassword"].ToString(),
							dataRow["ReportPath"].ToString(),
							dataRow["ReportName"].ToString());
					}
				}
				catch (Exception e)
				{
					Log.Write(e.Message + "[MedalistMain.BankLoanReportsLoad]", Log.Error, 2);
				}
			}
		}
		
		private void BankLoanDtcReset(string bookGroup)
		{
			BankLoanDtc bankLoanDtc;

			string host = Standard.ConfigValue("BankLoanDtcHost" + bookGroup);
			string path = Standard.ConfigValue("BankLoanDtcPath" + bookGroup);
			string userId = Standard.ConfigValue("BankLoanDtcUserId" + bookGroup);
			string password = Standard.ConfigValue("BankLoanDtcPassword" + bookGroup);
 
			if (host.Equals("") || path.Equals("") || userId.Equals("") || password.Equals("")) // Probably not meant to do this.
			{
				Log.Write("One or more config values are missing. [MedalistMain.BankLoanDtcRefresh]", 2);
				return;
			}

			if (KeyValue.Get("BankLoanBizDate" + bookGroup, "2000-01-01", dbCn).Equals(Master.BizDatePriorBank)) // Already done for today.
			{
				Log.Write("Bank loan DTC reset for " + bookGroup + " is current for " + Master.BizDatePriorBank + ". [MedalistMain.BankLoanDtcRefresh]", 2);
			}
			else
			{
				Log.Write("Will try to reset " + bookGroup + " bank loan data to match DTC records for " + Master.BizDatePriorBank + ". [MedalistMain.BankLoanDtcRefresh]", 2);

				try
				{
					bankLoanDtc = new BankLoanDtc(dbCn);

					if (bankLoanDtc.Reset(Master.BizDatePriorBank, bookGroup, path, host, userId, password))
					{
						KeyValue.Set("BankLoanBizDate" + bookGroup, Master.BizDatePriorBank, dbCn);						
						KeyValue.Set("BankLoanDtcReset" + bookGroup, true.ToString(), dbCn);
					}
					else
					{
						Log.Write("Bank loan DTC data for " + bookGroup + " for " + Master.BizDatePriorBank + " is not available. [MedalistMain.BankLoanDtcRefresh]", 2);						
						KeyValue.Set("BankLoanDtcReset" + bookGroup, false.ToString(), dbCn);
					}
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [MedalistMain.BankLoanDtcRefresh]", Log.Error, 1);
				}
			}
		}

		private void ContractBizDateRoll()
		{
			Master.ContractsBizDate = KeyValue.Get("ContractsBizDate", "2001-01-01", dbCn);

			if (Master.ContractsBizDate.Equals(Master.BizDate))
			{
				Log.Write("Contracts have already been rolled to " + Master.BizDate + ". [MedalistMain.ContractBizDateRoll]", 3);
				return;
			}

			if (Standard.ConfigValueExists("BookGroupList"))
			{
				string [] clientIdList = Standard.ConfigValue("BookGroupList").Split(';');

				foreach(string clientId in clientIdList)
				{
					if (clientId.Length.Equals(4) && !isStopped)
					{
						if (!KeyValue.Get("LoanetPositionBizDate" + clientId, "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
						{
							Log.Write("Waiting on Loanet position data for " + clientId + " for " + Master.BizDatePriorBank + ". [MedalistMain.ContractBizDateRoll]", 2);
							return;
						}
						else
						{
							Log.Write("Loanet position data is current for " + clientId + " for " + Master.BizDatePriorBank + ". [MedalistMain.ContractBizDateRoll]", 3);
						}
					}
				}

				SqlCommand sqlCommand;
      
				try
				{
					sqlCommand = new SqlCommand("spContractBizDateRoll", dbCn);
					sqlCommand.CommandType = CommandType.StoredProcedure;

					SqlParameter paramBizDatePrior = sqlCommand.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
					paramBizDatePrior.Value = Master.BizDatePrior;

					SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = Master.BizDate;

					SqlParameter  paramDayCount = sqlCommand.Parameters.Add("@DayCount", SqlDbType.SmallInt);
					paramDayCount.Value = DateTime.Compare(DateTime.Parse(Master.BizDateNext), DateTime.Parse(Master.BizDate)); 

					SqlParameter  paramRecordCount = sqlCommand.Parameters.Add("@RecordCount", SqlDbType.Int);
					paramRecordCount.Direction = ParameterDirection.Output;

					Log.Write("Rolling contract records from " + Master.BizDatePrior + " to " +  Master.BizDate + ". [MedalistMain.ContractsBizDateRoll]", 2);
          
					dbCn.Open();
					sqlCommand.ExecuteNonQuery();
					dbCn.Close();

					Master.ContractsBizDate = Master.BizDate;
					KeyValue.Set("ContractsBizDate", Master.ContractsBizDate, dbCn);

					int n = (int) paramRecordCount.Value;
					Log.Write("Rolled " + n.ToString("#,##0") + " contract records. [Loanet.ContractBizDateRoll]", 2);
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [MedalistMain.ContractsBizDateRoll]", Log.Error, 1);
				}
				finally
				{
					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
				}
			}
			else
			{
				Log.Write("There is no configuration value for BookGroupList. [MedalistMain.ContractsBizDateRoll]", Log.Warning, 1);
			}
		}

		public void BankLoanStockMovementActivity()
		{

			if (!KeyValue.Get("BankLoanPhase3ActivitySet", "False", dbCn).Equals("True")) return;

			try
			{
				DataSet dsBankLoanActivity = positionAgent.BankLoanActivityGet(-1);
				DataSet dsBankLoan = positionAgent.BankLoansGet(-1);
				SungardWS.SunGard webService = new SungardWS.SunGard();
				string fromCode = "", toCode = "", activityFlag = "";
				int sleepTimeout = Int32.Parse(KeyValue.Get("BankLoanSleepTimeout", "1000", dbCn));

				DataRow[]	foundRows =	dsBankLoanActivity.Tables[0].Select("Flag Is NULL And Status IN ('RM', 'PM')");

				foreach (DataRow row in foundRows)
				{
					DataRow[] bankLoanRow = dsBankLoan.Tables[0].Select("BookGroup = '" + row["BookGroup"] + "' AND Book = '" + row["Book"] + "' AND LoanDate = '" + row["LoanDate"].ToString() + "'");

					if (bankLoanRow.Length != 1) continue; // If no bank loans exist for stock movement activity then skip

					switch (row["Status"].ToString())
					{ 
						case "PM": // Pledge

							fromCode = "C4";

							switch (bankLoanRow[0]["LoanType"].ToString())
							{
								case "C": // Customer
									toCode = "4V";
									break;

								case "F": // Firm
									toCode = "4W";
									break;
								
								case "S": //	Special
									toCode = "4U";
									break;
							}

							break;

						case "RM": // Release

							toCode = "C4";

							switch (bankLoanRow[0]["LoanType"].ToString())
							{
								case "C": // Customer
									fromCode = "4V";
									break;

								case "F": // Firm
									fromCode = "4W";
									break;
									
								case "S": //	Special
									fromCode = "4U";
									break;
							}

							break;
					}
						

					try 
					{
						// Invoke the Flynet Web Service to input the Order Entry into the STKCTL Phase 3 screen.
						//webService.Url = "http://websvcpen01/ITSungard/sungard.asmx?WSDL";
						//webService.Url = "http://localhost:3130/web/SunGard.asmx?wsdl";

						webService.Url = Standard.ConfigValue("BankLoanSTKCTLWebService", "");
						SungardWS.StockControlMovementInfo objStatus = webService.StockControlMovement(fromCode, toCode, row["Quantity"].ToString().Trim(), row["SecId"].ToString().Trim(), "", "", "SENDERO - BANK LOAN");

						string strSuccessCode = KeyValue.Get("BankLoanSTKCTLSuccessCode", "ok", dbCn);
						
						activityFlag = (objStatus.status.Equals(strSuccessCode)) ? "P" : "X"; 

						Log.Write("[MedalistMain.BankLoanStockMovementActivity] ProcessID[" + row["ProcessId"] + "] Phase3 Message=" +  objStatus.message, Log.Information, 1);				  
					}
					catch (Exception e)
					{
						activityFlag = "X";

						Log.Write(e.Message + " - STKCTL Flynet Web Service call failed [MedalistMain.BankLoanStockMovementActivity]", Log.Error, 1);				  
					}
					

					// Update the Status field 
					switch (row["Status"].ToString())
					{
						case "PM":
							positionAgent.BankLoanPledgeSet(row["BookGroup"].ToString(), 
								row["Book"].ToString(), 
								row["LoanDate"].ToString(), 
								row["ProcessId"].ToString(), 
								bankLoanRow[0]["LoanType"].ToString(), 
								bankLoanRow[0]["ActivityType"].ToString(),
								row["SecId"].ToString(),
								row["Quantity"].ToString(),
								activityFlag,
								row["Status"].ToString(),
								"system");
							break;

						case "RM":
							positionAgent.BankLoanReleaseSet(row["BookGroup"].ToString(), 
								row["Book"].ToString(), 
								row["ProcessId"].ToString(), 
								row["LoanDate"].ToString(), 
								bankLoanRow[0]["LoanType"].ToString(), 
								bankLoanRow[0]["ActivityType"].ToString(),
								row["SecId"].ToString(),
								row["Quantity"].ToString(),
								activityFlag,
								row["Status"].ToString(),
								"system");
							break;
					}
				
					Thread.Sleep(sleepTimeout); //Temporarily suspend processing so that Flynet can handle multiple Phase 3 sessions
				}

			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [MedalistMain.BankLoanStockMovementActivity]", Log.Error, 1);				  
			}

		}

		public void BankLoanBizDateRoll()
		{
			if(KeyValue.Get("BankLoanRollBizDate", "0000-01-01", dbCn).Equals(Master.BizDateBank))
			{
				Log.Write("BankLoan BizDate Roll has already run for " + Master.BizDateBank + ". [MedalistMain.BankLoanBizDateRoll]", 2);
				return;
			}
			else
			{
				SqlCommand dbCmd = null;

				try
				{
					dbCn = new SqlConnection(dbCnStr);
					dbCmd = new SqlCommand("spBankLoanBizDateRoll", dbCn);
					dbCmd.CommandType = CommandType.StoredProcedure;                    
        			
					SqlParameter paramRecordsUpdated = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);
					paramRecordsUpdated.Direction = ParameterDirection.Output;
					paramRecordsUpdated.Value = 0;

					dbCn.Open();
					dbCmd.ExecuteNonQuery();
					dbCn.Close();
			  
					Log.Write("BankLoan BizDate Roll updated: " + long.Parse(paramRecordsUpdated.Value.ToString()).ToString("#,##0") + " older then " + Master.BizDateBank + ". [MedalistMain.BankLoanBizDateRoll]", 2);
					KeyValue.Set("BankLoanRollBizDate", Master.BizDateBank, dbCn);
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [PositionAgent.BankLoanBizDateRoll]", Log.Error, 1);				  
				}
				finally
				{
					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
				}
			}
		}

		private void RecallBizDateSet()
		{    
			string RecallBizDate = KeyValue.Get("RecallBizDate", "2001-01-01", dbCn);

			if (RecallBizDate.Equals(Master.BizDate))
			{
				Log.Write("Recalls have already been set for " + Master.BizDate + ". [MedalistMain.RecallBizDateSet]", 3);
				return;
			}
      
			if (Standard.ConfigValueExists("BookGroupList"))
			{
				string [] clientIdList = Standard.ConfigValue("BookGroupList").Split(';');

				foreach(string clientId in clientIdList)
				{
					if (clientId.Length.Equals(4) && !isStopped)
					{
						if (!KeyValue.Get("LoanetRecallsBizDate" + clientId, "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
						{
							Log.Write("Waiting on Loanet recall data for " + clientId + " for " + Master.BizDatePriorBank + ". [MedalistMain.RecallBizDateSet]", 2);
							return;
						}
						else
						{
							Log.Write("Loanet recall data is current for " + clientId + " for " + Master.BizDatePriorBank + ". [MedalistMain.RecallBizDateSet]", 3);
						}
					}
				}
			}

			SqlCommand sqlCommand;
      
			try
			{
				sqlCommand = new SqlCommand("spRecallBizDateSet", dbCn);
				sqlCommand.CommandType = CommandType.StoredProcedure;
        
				SqlParameter  paramRecordCount = sqlCommand.Parameters.Add("@RecordCount", SqlDbType.Int);
				paramRecordCount.Direction = ParameterDirection.Output;
        
				Log.Write("Setting recall records for " + Master.BizDate + ". [MedalistMain.RecallBizDateSet]", 2);
          
				dbCn.Open();
				sqlCommand.ExecuteNonQuery();
				dbCn.Close();                

				KeyValue.Set("RecallBizDate", Master.BizDate, dbCn);

				int n = (int) paramRecordCount.Value;
				Log.Write("Set " + n.ToString("#,##0") + " recall records. [MedalistMain.RecallBizDateSet]", 2);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + "  [MedalistMain.RecallBizDateSet]", Log.Error, 1);
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}    

		private void EasyBorrowListMake()
		{
			string listMakeTime;

			if (KeyValue.Get("EasyBorrowListDate", "", dbCn).Equals(Master.BizDateExchange))
			{
				Log.Write("Easy borrow list is current for " + Master.BizDateExchange + ". [MedalistMain.EasyBorrowListMake]", 2);
			}
			else // Will need new list.
			{
				if (Master.BizDateExchange.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) // Is the day to do it.
				{
					listMakeTime = KeyValue.Get("EasyBorrowListMakeTime", "11:00", dbCn);

					if (listMakeTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0) // It is time to do it.
					{
						SqlCommand sqlDbCmd = new SqlCommand("spBorrowEasySet", dbCn);
						sqlDbCmd.CommandType = CommandType.StoredProcedure;
						sqlDbCmd.CommandTimeout = int.Parse(KeyValue.Get("EasyBorrowListMakeTimeout", "300", dbCn));

						SqlParameter paramTradeDate = sqlDbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);			
						paramTradeDate.Value = Master.BizDateExchange;

						try 
						{
							dbCn.Open();
							sqlDbCmd.ExecuteNonQuery();
							dbCn.Close();
              
							KeyValue.Set("EasyBorrowListDate", Master.BizDateExchange, dbCn);
							Log.Write("Easy Borrow list has been set for " + Master.BizDateExchange + ". [MedalistMain.EasyBorrowListMake]", 2);
						}
						catch(Exception e) 
						{
							Log.Write(e.Message + " [MedalistMain.EasyBorrowListMake]", Log.Error, 1);            
						}
						finally
						{
							if (dbCn.State != ConnectionState.Closed)
							{
								dbCn.Close();
							}
						}
					}
					else
					{
						Log.Write("Must wait until after " + listMakeTime + " UTC to do list for " + Master.BizDateExchange + ". [MedalistMain.EasyBorrowListMake]", 2);
					}
				}
				else
				{
					Log.Write("Must wait until trade date, " + Master.BizDateExchange + ". [MedalistMain.EasyBorrowListMake]", 2);
				}
			}
		}

		private void EasyBorrowFileDo()
		{
			int n = 0;
			string status = "OK";
			string easyBorrowListDate = KeyValue.Get("EasyBorrowListDate", "", dbCn);
			Anetics.Common.Filer filer;

			if (!KeyValue.Get("EasyBorrowFileDate", "", dbCn).Equals(easyBorrowListDate)) // Will need to do the file.
			{
				Log.Write("Will do the easy borrow file for " + easyBorrowListDate + ". [MedalistMain.EasyBorrowFileDo]", 2);

				StreamWriter streamWriter = null;
				SqlDataReader dataReader = null;

				SqlCommand dbCmd = new SqlCommand("spBorrowEasyList", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.VarChar, 25);			
				paramTradeDate.Value = easyBorrowListDate;

				try
				{        
					dbCn.Open();
					dataReader = dbCmd.ExecuteReader();

					streamWriter = new StreamWriter(
						TempPath + "easyBorrows.dat",
						false,
						System.Text.Encoding.ASCII);

					streamWriter.WriteLine("H|EasyBorrowList|" + easyBorrowListDate + "|" +
						KeyValue.Get("EasyBorrowFileSource", "PensonFinancial", dbCnStr));

					while (dataReader.Read())
					{
						streamWriter.WriteLine("D|" + 
							dataReader.GetValue(0).ToString().Trim() + "|" +
							dataReader.GetValue(1).ToString().Trim() + "|" +
							dataReader.GetValue(2).ToString().Trim());
            
						n += 1;
					}

					streamWriter.WriteLine("T|DataItemCount|" + n + "|EOF");
        
					dataReader.Close();
					dbCn.Close();

					streamWriter.Flush();
					streamWriter.Close();

					filer = new Anetics.Common.Filer(TempPath);

					filer.FilePut(
						KeyValue.Get("EasyBorrowFilePathName", @"C:\Anetics\Logs\easyBorrowList.txt", dbCn),
						KeyValue.Get("EasyBorrowFileHostName", "localHost", dbCn),
						KeyValue.Get("EasyBorrowFileUserName", "anonymous", dbCn),
						KeyValue.Get("EasyBorrowFilePassword", "medalist", dbCn),
						TempPath + "easyBorrows.dat");
          
					KeyValue.Set("EasyBorrowFileDate", easyBorrowListDate, dbCnStr);        
				}
				catch (Exception e)
				{
					status = e.Message;
					Log.Write(e.Message + " [MedalistMain.EasyBorrowFileDo]", Log.Error, 1);
				}
				finally
				{
					if ((dataReader != null) && (!dataReader.IsClosed))
					{
						dataReader.Close();
					}

					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
				}
      
				KeyValue.Set("EasyBorrowFileStatus", status, dbCn);
			}
			else
			{
				Log.Write("Easy borrow file is current for " + easyBorrowListDate + ". [MedalistMain.EasyBorrowFileDo]", 2);
			}
		}

		private void MailSendEasy()
		{
			SqlDataReader dataReader = null;        
			int itemCount = 0;
			string easyBorrowFileDate = KeyValue.Get("EasyBorrowFileDate", "", dbCn);

			if (KeyValue.Get("EasyBorrowMailFrom", "support@anetics.com", dbCn).Equals("")) // Do not send mail.
			{
				Log.Write("There is no key value for mail sender (EasyBorrowMailFrom). [MedalistMain.MailSendEasy]", 2);
				return;
			}
      
			if (KeyValue.Get("EasyBorrowMailDate", "2001-01-01", dbCn).Equals(easyBorrowFileDate))
			{
				Log.Write("Easy borrow e-mail notice has already been sent for " + easyBorrowFileDate + ". [MedalistMain.MailSendEasy]", 2);
			}
			else  
			{
				Log.Write("Will do the easy borrow e-mail notice for " + easyBorrowFileDate + ". [MedalistMain.MailSendEasy]", 2);
     
				SqlCommand sqlCmd = new SqlCommand("spBorrowEasyList", dbCn);
				sqlCmd.CommandType = CommandType.StoredProcedure;
				sqlCmd.CommandTimeout = 600;    
        
				string mailContent = "SecId".PadRight(15, ' ') + "Symbol\n";
				mailContent+= "-----".PadRight(15, ' ') + "------\n";
								
				try
				{        
					dbCn.Open();
					dataReader = sqlCmd.ExecuteReader();

					while (dataReader.Read())
					{
						mailContent += dataReader.GetValue(0).ToString().PadRight(15, ' ' ) + 
							dataReader.GetValue(1).ToString()+ "\n";
          
						++itemCount;
					}
				}
				catch (Exception e)
				{
					KeyValue.Set("EasyBorrowMailStatus", e.Message, dbCn);
					Log.Write(e.Message + " [MedalistMain.MailSendEasy]", Log.Error, 1);
					return;
				}
				finally
				{
					if ((dataReader != null) && (!dataReader.IsClosed))
					{
						dataReader.Close();
					}
          
					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
				}

				if (itemCount == 0)
				{
					mailContent += "[None for today.]\n";
				}
				
				Log.Write("Listed " + itemCount + " easy borrow items as added in e-mail notification. [MedalistMain.MailSendEasy]", 2);     

				try
				{
					Email email = new Email(dbCnStr);

					email.Send(
						KeyValue.Get("EasyBorrowMailTo", "support@anetics.com", dbCn),
						KeyValue.Get("EasyBorrowMailFrom", "support@anetics.com", dbCn),
						"Easy Borrow List for " + Tools.FormatDate(Master.BizDateExchange, "dddd, d MMMM yyyy"),
						mailContent);

					KeyValue.Set("EasyBorrowMailStatus", "OK", dbCn);
					KeyValue.Set("EasyBorrowMailDate", easyBorrowFileDate, dbCn);
        
					Log.Write("Easy borrow e-mail notice has been sent for " + easyBorrowFileDate + ". [MedalistMain.MailSendEasy]", 2);
				}
				catch (Exception e)
				{
					KeyValue.Set("EasyBorrowMailStatus", e.Message, dbCn);
					Log.Write(e.Message + " [MedalistMain.MailSendEasy]", Log.Error, 1);
				}
				finally
				{
					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
				}
			}
		}

		private void MailSendThreshold()
		{
			if (KeyValue.Get("ThresholdMailFrom", "support@stockloan.net", dbCn).Equals("")) // Do not send mail.
			{
				Log.Write("There is no key value for mail sender (ThresholdMailFrom). [MedalistMain.MailSendThreshold]", 2);
				return;
			}
      
			if (KeyValue.Get("ThresholdMailDate", "2001-01-01", dbCn).Equals(Master.BizDateBank))
			{
				Log.Write("Threshold list e-mail notice has already been sent for " + Master.BizDateBank + ". [MedalistMain.MailSendThreshold]", 2);
				return;
			}

			if(!KeyValue.Get("ThresholdListBizDateAmse", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
			{
				Log.Write("Waiting for the threshold list from AMSE for " + Master.BizDatePriorBank + ". [MedalistMain.MailSendThreshold]", 2);
				return;
			}
				
			if(!KeyValue.Get("ThresholdListBizDateArca", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
			{
				Log.Write("Waiting for the threshold list from ARCA for " + Master.BizDatePriorBank + ". [MedalistMain.MailSendThreshold]", 2);
				return;
			}
				
			if(!KeyValue.Get("ThresholdListBizDateChse", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
			{
				Log.Write("Waiting for the threshold list from CHSE for " + Master.BizDatePriorBank + ". [MedalistMain.MailSendThreshold]", 2);
				return;
			}
				
			if(!KeyValue.Get("ThresholdListBizDateNsdq", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
			{
				Log.Write("Waiting for the threshold list from NSDQ for " + Master.BizDatePriorBank + ". [MedalistMain.MailSendThreshold]", 2);
				return;
			}

			if(!KeyValue.Get("ThresholdListBizDateNyse", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
			{
				Log.Write("Waiting for the threshold list from NYSE for " + Master.BizDatePriorBank + ". [MedalistMain.MailSendThreshold]", 2);
				return;
			}
				
			SqlCommand sqlCmd = new SqlCommand("spThresholdList", dbCn);
			sqlCmd.CommandType = CommandType.StoredProcedure;

			int itemCount = 0;
			string mailContent =  "List Date".PadRight(14, ' ' ) + 
				"Exchange".PadRight(10, ' ' ) +
				"SecId".PadRight(12, ' ') + 
				"Symbol".PadRight(10, ' ' ) + 
				"Description".PadRight(75, ' ' ) +
				"Day Count".ToString() + "\n";
      
			mailContent +=  "---------".PadRight(14, ' ' ) + 
				"--------".PadRight(10, ' ' ) +
				"-----".PadRight(12, ' ') + 
				"------".PadRight(10, ' ' ) + 
				"-----------".PadRight(75, ' ' ) +
				"---------".ToString() + "\n";

			SqlDataReader sqlDataReader = null;

			try
			{     
				dbCn.Open();
				sqlDataReader = sqlCmd.ExecuteReader();

				while (sqlDataReader.Read())
				{
					mailContent += sqlDataReader.GetValue(0).ToString().PadRight(14, ' ' ) + 
						sqlDataReader.GetValue(1).ToString().PadRight(10, ' ' ) +
						sqlDataReader.GetValue(2).ToString().PadRight(12, ' ') + 
						sqlDataReader.GetValue(3).ToString().PadRight(10, ' ' ) + 
						sqlDataReader.GetValue(4).ToString().PadRight(75, ' ' ) +
						sqlDataReader.GetValue(5).ToString() + "\n";
          
					++itemCount;
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [MedalistMain.MailSendThreshold]", Log.Error, 1);
				return;
			}
			finally
			{
				if ((sqlDataReader != null) && (!sqlDataReader.IsClosed))
				{
					sqlDataReader.Close();
				}
          
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
   
			try
			{
				Email email = new Email(dbCnStr);

				email.Send(					
					KeyValue.Get("ThresholdMailTo", "support@stockloan.net", dbCn),
					KeyValue.Get("ThresholdMailFrom", "support@stockloan.net", dbCn),
					"Threshold List for " + Tools.FormatDate(Master.BizDateBank, "dddd, d MMMM yyyy"),
					mailContent);

				KeyValue.Set("ThresholdMailStatus", "OK", dbCn);
				KeyValue.Set("ThresholdMailDate", Master.BizDateBank, dbCn);
        
				Log.Write("Threshold e-mail notice has been sent for " + Master.BizDateBank + ". [MedalistMain.MailSendThreshold]", 2);
			}
			catch (Exception e)
			{
				KeyValue.Set("ThresholdMailStatus", e.Message, dbCn);
				Log.Write(e.Message + " [MedalistMain.MailSendThreshold]", Log.Error, 1);
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}

		private void MailSendShortSaleAccountsCovered()
		{
			
			string mailContent = "";
			string snapShotDate = KeyValue.Get("ShortSaleNegRebateBillingSnapShotBizDate", "", dbCn);

			if (!snapShotDate.Equals(Master.ContractsBizDate))
			{
				Log.Write("Short Sale Billing Snapshot has not run for today yet. [MedalistMain.MailSendShortSaleAccountsCovered]", 2);
				return;
			}
			else if (KeyValue.Get("ShortSaleAccountsCoveredMailFrom", "stockloan@penson.com", dbCn).Equals("")) // Do not send mail.
			{
				Log.Write("There is no key value for mail sender (ShortSaleAccountsCoveredMailFrom). [MedalistMain.MailSendShortSaleAccountsCovered]", 2);
				return;
			}
			else if (KeyValue.Get("ShortSaleAccountsCoveredMailDate", "2001-01-01", dbCn).Equals(Master.BizDate))
			{
				Log.Write("Short sale accounts charged  notice has already been sent for " + Master.BizDate + ". [MedalistMain.MailSendShortSaleAccountsCovered]", 2);
			}		
			else  
			{
				Log.Write("Will do the short sale accounts charged notice for " + Master.BizDatePrior + ". [MedalistMain.MailSendShortSaleAccountsCovered]", 2);
				try
				{
					ShortSaleNegativeRebateBillDocument shortSaleBill = new ShortSaleNegativeRebateBillDocument(dbCnStr, Master.ContractsBizDate, Master.ContractsBizDate, "");
					mailContent = shortSaleBill.AccountsBorrowedEmailBill();
				}
				catch (Exception e)
				{
					KeyValue.Set("ShortSaleAccountsCoveredMailStatus", e.Message, dbCn);
					Log.Write(e.Message + " [MedalistMain.MailSendShortSaleAccountsCovered]", Log.Error, 1);
					return;
				}
				

				try
				{
					Email email = new Email(dbCnStr);

					email.Send(
						KeyValue.Get("ShortSaleAccountsCoveredMailTo", "mbattaini@penson.com", dbCn),
						KeyValue.Get("ShortSaleAccountsCoveredMailFrom", "stockloan@penson.com", dbCn),
						"Covered Shorts Do Not Buyin! For " + Tools.FormatDate(Master.ContractsBizDate, "dddd, d MMMM yyyy"),
						mailContent);

					KeyValue.Set("ShortSaleAccountsCoveredMailStatus", "OK", dbCn);
					KeyValue.Set("ShortSaleAccountsCoveredMailDate", Master.BizDate, dbCn);
        
					Log.Write("Short Sale Accouts Covered e-mail notice has been sent for " + Master.BizDate + ". [MedalistMain.MailSendShortSaleAccountsCovered]", 2);
				}
				catch (Exception e)
				{
					KeyValue.Set("ShortSaleAccountsCoveredMailStatus", e.Message, dbCn);
					Log.Write(e.Message + " [MedalistMain.MailSendShortSaleAccountsCharged]", Log.Error, 1);
				}
				finally
				{
					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
				}
			}
		}

		private void ShortSaleNegativeRebateBillingSnapShot()
		{
			string listMakeTime;

			if (!Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Bank, dbCn))
			{
				if (KeyValue.Get("ShortSaleNegRebateBillingSnapShotBizDate", "", dbCn).Equals(DateTime.UtcNow.ToString(Standard.DateFormat)))
				{
					Log.Write("Short Sale Negative Rebate Billing Weekend Snapshot is current for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
				}
				else // do weekend snapshot
				{
					SqlCommand sqlCommand;
      
					Log.Write("Doing Short Sale Negative Rebate Billing Weekend Snapshot for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);

					try
					{
						sqlCommand = new SqlCommand("spShortSaleBillingSummaryOvernightWeekendSnapShot", dbCn);
						sqlCommand.CommandType = CommandType.StoredProcedure;

						SqlParameter paramBizDatePrior = sqlCommand.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
						paramBizDatePrior.Value = Master.BizDatePrior;

						SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
						paramBizDate.Value = DateTime.UtcNow.ToString(Standard.DateFormat);			
          
						dbCn.Open();
						sqlCommand.ExecuteNonQuery();
						dbCn.Close();								

						KeyValue.Set("ShortSaleNegRebateBillingSnapShotBizDate", DateTime.UtcNow.ToString(Standard.DateFormat), dbCn);
					}
					catch (Exception e)
					{
						Log.Write(e.Message + " [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", Log.Error, 1);
					}
					finally
					{
						if (dbCn.State != ConnectionState.Closed)
						{
							dbCn.Close();
						}
					}
				}
			}
			else
			{			
				if (KeyValue.Get("ShortSaleNegRebateBillingSnapShotBizDate", "", dbCn).Equals(Master.BizDate))
				{
					Log.Write("Short Sale Negative Rebate Billing Snapshot is current for " + Master.ContractsBizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
				}
				else if (!Master.ContractsBizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
				{
					Log.Write("Contracts data has not been loaded for " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
				}
				else if (!KeyValue.Get("PensonSecurityStaticBizDate", "", dbCn).Equals(Master.BizDatePrior))
				{
					Log.Write("Penson security static data has not loaded yet. [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);       
				}
				else if (!DateTime.Parse(KeyValue.Get("BoxPositionLoadDateTime", "", dbCn)).ToString(Standard.DateFormat).Equals(Master.ContractsBizDate))
				{
					Log.Write("Penson box position data has not been loaded yet. [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
				}
				else 
				{
					if (Master.ContractsBizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) 
					{
						listMakeTime = KeyValue.Get("ShortSaleNegRebateBillingSnapShotTime", "23:00", dbCn);

						if (listMakeTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0)
						{
							Log.Write("Doing Short Sale Negative Rebate Billing  Buisness Day Snapshot for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);

							SqlCommand sqlDbCmd = new SqlCommand("spShortSaleBillingSummaryOvernightSnapShotControl", dbCn);
							sqlDbCmd.CommandType = CommandType.StoredProcedure;
							sqlDbCmd.CommandTimeout = int.Parse(KeyValue.Get("ShortSaleNegRebateBillingSnapShotTimeOut", "300", dbCn));

							try 
							{
								dbCn.Open();
								sqlDbCmd.ExecuteNonQuery();
								dbCn.Close();
              
								KeyValue.Set("ShortSaleNegRebateBillingSnapShotBizDate", Master.BizDate, dbCn);
								KeyValue.Set("ShortSaleNegRebateBillingSnapShotDateTime", DateTime.Now.ToString(Standard.DateTimeFormat), dbCn);
								Log.Write("ShortSale Negative Rebate Billing Snap Shot done for:  " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
							}
							catch(Exception e) 
							{
								Log.Write(e.Message + " [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", Log.Error, 1);            
							}
							finally
							{
								if (dbCn.State != ConnectionState.Closed)
								{
									dbCn.Close();
								}
							}
						}
						else
						{
							Log.Write("Must wait until after " + listMakeTime + " UTC to do snapshopt for " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
						}
					}
					else
					{
						Log.Write("Must wait until trade date, " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
					}
				}
			}
		}

		private void ShortSalePositiveRebateBillingSnapShot()
		{
			string listMakeTime;

			if (!Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Exchange, dbCn))
			{
				if (KeyValue.Get("ShortSalePosRebateBillingSnapShotBizDate", "", dbCn).Equals(DateTime.UtcNow.ToString(Standard.DateFormat)))
				{
					Log.Write("Short Sale Rebate Billing Weekend Snapshot is current for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
				}
				else // do weekend snapshot
				{
					SqlCommand sqlCommand;
      
					Log.Write("Doing Short Sale Positive Rebate Billing Weekend Snapshot for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);

					try
					{
						sqlCommand = new SqlCommand("spShortSaleBillingPositiveRebatesSummaryOvernightWeekendSnapShot", dbCn);
						sqlCommand.CommandType = CommandType.StoredProcedure;

						SqlParameter paramBizDatePrior = sqlCommand.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
						paramBizDatePrior.Value = Master.BizDatePrior;

						SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
						paramBizDate.Value = DateTime.UtcNow.ToString(Standard.DateFormat);			
          
						dbCn.Open();
						sqlCommand.ExecuteNonQuery();
						dbCn.Close();								

						KeyValue.Set("ShortSaleNegRebateBillingSnapShotBizDate", DateTime.UtcNow.ToString(Standard.DateFormat), dbCn);
					}
					catch (Exception e)
					{
						Log.Write(e.Message + " [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", Log.Error, 1);
					}
					finally
					{
						if (dbCn.State != ConnectionState.Closed)
						{
							dbCn.Close();
						}
					}
				}
			}
			else //if (!Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Bank, dbCn))
			{			
				if (KeyValue.Get("ShortSalePosRebateBillingSnapShotBizDate", "", dbCn).Equals(Master.BizDate))
				{
					Log.Write("Short Sale Positive Rebate Billing Snapshot is current for " + Master.ContractsBizDate + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
				}
				else if (!Master.ContractsBizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
				{
					Log.Write("Contracts data has not been loaded for " + Master.BizDate + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
				}
				else if (!KeyValue.Get("PensonSecurityStaticBizDate", "", dbCn).Equals(Master.BizDatePrior))
				{
					Log.Write("Penson security static data has not loaded yet. [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);       
				}
				else if (!DateTime.Parse(KeyValue.Get("BoxPositionLoadDateTime", "", dbCn)).ToString(Standard.DateFormat).Equals(Master.ContractsBizDate))
				{
					Log.Write("Penson box position data has not been loaded yet. [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
				}
				else 
				{
					if (Master.ContractsBizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) 
					{
						listMakeTime = KeyValue.Get("ShortSalePosRebateBillingSnapShotTime", "23:00", dbCn);

						if (listMakeTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0)
						{
							Log.Write("Doing Short Sale Rebate Billing  Buisness Day Snapshot for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);

							SqlCommand sqlDbCmd = new SqlCommand("spShortSaleBillingPositiveRebatesSummaryOvernightSnapShotControl", dbCn);
							sqlDbCmd.CommandType = CommandType.StoredProcedure;
							sqlDbCmd.CommandTimeout = int.Parse(KeyValue.Get("ShortSalePosRebateBillingSnapShotTimeOut", "300", dbCn));

							try 
							{
								dbCn.Open();
								sqlDbCmd.ExecuteNonQuery();
								dbCn.Close();
              
								KeyValue.Set("ShortSalePosRebateBillingSnapShotBizDate", Master.BizDate, dbCn);
								KeyValue.Set("ShortSalePosRebateBillingSnapShotDateTime", DateTime.Now.ToString(Standard.DateTimeFormat), dbCn);
								Log.Write("ShortSale Positive Rebate Billing Snap Shot done for:  " + Master.BizDate + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
							}
							catch(Exception e) 
							{
								Log.Write(e.Message + " [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", Log.Error, 1);            
							}
							finally
							{
								if (dbCn.State != ConnectionState.Closed)
								{
									dbCn.Close();
								}
							}
						}
						else
						{
							Log.Write("Must wait until after " + listMakeTime + " UTC to do snapshopt for " + Master.BizDate + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
						}
					}
					else
					{
						Log.Write("Must wait until trade date, " + Master.BizDate + ". [MedalistMain.ShortSalePositiveRebateBillingSnapShot]", 2);
					}
				}
			}
		}

		public void ShortSaleNegativeRebateGeneralLedgerStatusUpdate ()
		{
			try
			{
				if (KeyValue.Get("ShortSaleNegRebateBillingSnapShotBizDate", "", dbCn).Equals(Master.BizDate))
				{
					Log.Write("Short Sale Negative Rebate Billing Snapshot is current for " + Master.ContractsBizDate + ". [MedalistMain.ShortSaleNegativeRebateGeneralLedgerStatusUpdate]", 2);
			
					Log.Write("Will check general ledger entries for " +Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateGeneralLedgerStatusUpdate]", 2);
     
					SqlCommand sqlCmd = new SqlCommand("spShortSaleBillingSummaryGeneralLedgerStatusUpdate", dbCn);
					sqlCmd.CommandType = CommandType.StoredProcedure;
					sqlCmd.CommandTimeout = 600;    
        
					SqlParameter paramBizDate = sqlCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = Master.BizDate;

					SqlParameter paramRecordsUpdated = sqlCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);
					paramRecordsUpdated.Direction = ParameterDirection.Output;

					dbCn.Open();
					sqlCmd.ExecuteNonQuery();
					dbCn.Close();
					
					Log.Write("Updated " + long.Parse(paramRecordsUpdated.Value.ToString()) + " general ledger entries for " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateGeneralLedgerStatusUpdate]", 2);     
				}
			}
			catch (Exception error)
			{				
				Log.Write(error.Message + " [MedalistMain.ShortSaleNegativeRebateGeneralLedgerStatusUpdate]", Log.Error, 1);
			}			
		}


		public void ShortSalePositiveRebateGeneralLedgerStatusUpdate ()
		{
			try
			{
				if (KeyValue.Get("ShortSalePosRebateBillingSnapShotBizDate", "", dbCn).Equals(Master.BizDate))
				{
					Log.Write("Short Sale Negative Rebate Billing Snapshot is current for " + Master.ContractsBizDate + ". [MedalistMain.ShortSalePositiveRebateGeneralLedgerStatusUpdate]", 2);
			
					Log.Write("Will check general ledger entries for " +Master.BizDate + ". [MedalistMain.ShortSalePositiveRebateGeneralLedgerStatusUpdate]", 2);
     
					SqlCommand sqlCmd = new SqlCommand("spShortSaleBillingPositiveRebatesSummaryGeneralLedgerStatusUpdate", dbCn);
					sqlCmd.CommandType = CommandType.StoredProcedure;
					sqlCmd.CommandTimeout = 600;    
        
					SqlParameter paramBizDate = sqlCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = Master.BizDate;

					SqlParameter paramRecordsUpdated = sqlCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);
					paramRecordsUpdated.Direction = ParameterDirection.Output;

					dbCn.Open();
					sqlCmd.ExecuteNonQuery();
					dbCn.Close();
					
					Log.Write("Updated " + long.Parse(paramRecordsUpdated.Value.ToString()) + " general ledger entries for " + Master.BizDate + ". [MedalistMain.ShortSalePositiveRebateGeneralLedgerStatusUpdate]", 2);     
				}
			}
			catch (Exception error)
			{				
				Log.Write(error.Message + " [MedalistMain.ShortSalePositiveRebateGeneralLedgerStatusUpdate]", Log.Error, 1);
			}			
		}


		private	void InventoryFundingRatesRoll()
		{
			string bizDate = KeyValue.Get("InventoryFundingRateBizDate", "2001-01-01", dbCn);

			if (bizDate.Equals(Master.BizDate))
			{
				Log.Write("Inventory funding rates have already been rolled to " + Master.BizDate + ". [MedalistMain.InventoryFundingRatesRoll]", 3);
				return;
			}
			
			SqlCommand sqlCommand;
      
			try
			{
				sqlCommand = new SqlCommand("spInventoryFundingRatesRoll", dbCn);
				sqlCommand.CommandType = CommandType.StoredProcedure;
	
				Log.Write("Rolling inventory funding rates records from " + Master.BizDatePrior + " to " +  Master.BizDate + ". [MedalistMain.InventoryFundingRatesRoll]", 2);
          
				dbCn.Open();
				sqlCommand.ExecuteNonQuery();
				dbCn.Close();

				KeyValue.Set("InventoryFundingRateBizDate", Master.BizDate, dbCn);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [MedalistMain.InventoryFundingRatesRoll]", Log.Error, 1);
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}

		private void BizDatesSet(Standard.HolidayType holidayType)
		{
			double utcOffset;

			try
			{
				utcOffset = double.Parse(KeyValue.Get("BizDateRollUtcOffsetMinutes", "0", dbCn));
			}
			catch
			{
				Log.Write("Unable to parse BizDateRollUtcOffsetMinutes key value. [MedalistMain.BizDatesSet]", Log.Error,  2);
				return;
			}

			DateTime bizDate;
			DateTime bizDateNext;
			DateTime bizDatePrior;

			bizDate = DateTime.UtcNow.AddMinutes(utcOffset).Date;
			while (!Standard.IsBizDate(bizDate, countryCode, holidayType, dbCn))
			{
				bizDate = bizDate.AddDays(1.0);
			}

			bizDateNext = bizDate.AddDays(1.0);
			while (!Standard.IsBizDate(bizDateNext, countryCode, holidayType, dbCn))
			{
				bizDateNext = bizDateNext.AddDays(1.0);
			}

			bizDatePrior = bizDate.AddDays(-1.0);
			while (!Standard.IsBizDate(bizDatePrior, countryCode, holidayType, dbCn))
			{
				bizDatePrior = bizDatePrior.AddDays(-1.0);
			}

			switch (holidayType)
			{
				case Standard.HolidayType.Any :
					Master.BizDate = bizDate.ToString(Standard.DateFormat);
					Master.BizDateNext = bizDateNext.ToString(Standard.DateFormat);
					Master.BizDatePrior = bizDatePrior.ToString(Standard.DateFormat);
      
					if (!KeyValue.Get("BizDate", "2001-01-01", dbCn).Equals(Master.BizDate)) 
					{
						KeyValue.Set("BizDate", Master.BizDate, dbCn);
						Log.Write("BizDate has been set to: " + Master.BizDate + " [MedalistMain.BizDatesSet]", Log.Information,  2);

						KeyValue.Set("ContractsBizDate", "2001-01-01", dbCn);
						Log.Write("ContractsBizDate has been set to: 2001-01-01 [MedalistMain.BizDatesSet]", Log.Information,  2);
					}
            
					if (!KeyValue.Get("BizDateNext", "2001-01-01", dbCn).Equals(Master.BizDateNext)) 
					{
						KeyValue.Set("BizDateNext", Master.BizDateNext, dbCn);
						Log.Write("BizDateNext has been set to: " + Master.BizDateNext + " [MedalistMain.BizDatesSet]", Log.Information,  2);
					}
            
					if (!KeyValue.Get("BizDatePrior", "2001-01-01", dbCn).Equals(Master.BizDatePrior)) 
					{
						KeyValue.Set("BizDatePrior", Master.BizDatePrior, dbCn);
						Log.Write("BizDatePrior has been set to: " + Master.BizDatePrior + " [MedalistMain.BizDatesSet]", Log.Information,  2);
					}
            
					break;
				case Standard.HolidayType.Bank :
					Master.BizDateBank = bizDate.ToString(Standard.DateFormat);
					Master.BizDateNextBank = bizDateNext.ToString(Standard.DateFormat);
					Master.BizDatePriorBank = bizDatePrior.ToString(Standard.DateFormat);
      
					if (!KeyValue.Get("BizDateBank", "2001-01-01", dbCn).Equals(Master.BizDateBank)) 
					{
						KeyValue.Set("BizDateBank",  Master.BizDateBank, dbCn);
						Log.Write("BizDateBank has been set to: " + Master.BizDateBank + " [MedalistMain.BizDatesSet]", Log.Information,  2);
					}
            
					if (!KeyValue.Get("BizDateNextBank", "2001-01-01", dbCn).Equals(Master.BizDateNextBank)) 
					{
						KeyValue.Set("BizDateNextBank", Master.BizDateNextBank, dbCn);
						Log.Write("BizDateNextBank has been set to: " + Master.BizDateNextBank + " [MedalistMain.BizDatesSet]", Log.Information,  2);
					}
            
					if (!KeyValue.Get("BizDatePriorBank", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank)) 
					{
						KeyValue.Set("BizDatePriorBank", Master.BizDatePriorBank, dbCn);
						Log.Write("BizDatePriorBank has been set to: " + Master.BizDatePriorBank + " [MedalistMain.BizDatesSet]", Log.Information,  2);
					}
            
					break;
				case Standard.HolidayType.Exchange :
					Master.BizDateExchange = bizDate.ToString(Standard.DateFormat);
					Master.BizDateNextExchange = bizDateNext.ToString(Standard.DateFormat);
					Master.BizDatePriorExchange = bizDatePrior.ToString(Standard.DateFormat);
      
					if (!KeyValue.Get("BizDateExchange", "2001-01-01", dbCn).Equals(Master.BizDateExchange)) 
					{
						KeyValue.Set("BizDateExchange", Master.BizDateExchange, dbCn);
						Log.Write("BizDateExchange has been set to: " + Master.BizDateExchange + " [MedalistMain.BizDatesSet]", Log.Information,  2);
					}
            
					if (!KeyValue.Get("BizDateNextExchange", "2001-01-01", dbCn).Equals(Master.BizDateNextExchange)) 
					{
						KeyValue.Set("BizDateNextExchange", Master.BizDateNextExchange, dbCn);
						Log.Write("BizDateNextExchange has been set to: " + Master.BizDateNextExchange + " [MedalistMain.BizDatesSet]", Log.Information,  2);
					}
            
					if (!KeyValue.Get("BizDatePriorExchange", "2001-01-01", dbCn).Equals(Master.BizDatePriorExchange)) 
					{
						KeyValue.Set("BizDatePriorExchange", Master.BizDatePriorExchange, dbCn);
						Log.Write("BizDatePriorExchange has been set to: " + Master.BizDatePriorExchange + " [MedalistMain.BizDatesSet]", Log.Information,  2);
					}
            
					break;
			}
		}

		private void SecuritiesInAccountsBlackList()
		{
			if (KeyValue.Get("SecuritiesInAccountsBlackListBizDate", "", dbCn).Equals(Master.BizDate)) // Already done for today.
			{
				Log.Write("Securities in blocked accounts loaded for " + Master.BizDate + ". [MedalistMain.SecuritiesInAccountsBlackList]", 2);       
				return;
			}

			if (Master.BizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) // Calendar date is business date.
			{				
				if (KeyValue.Get("PensonSecurityStaticBizDate", "", dbCn).Equals(Master.BizDatePrior)) // Do it now
				{
					Log.Write("Penson security static data has loaded, will do SecuritiesInAccountsBlackList. [MedalistMain.SecuritiesInAccountsBlackList]", 2);       			
				}			
				else
				{
					Log.Write("Penson security static data has not loaded yet. [MedalistMain.SecuritiesInAccountsBlackList]", 2);       
					return;
				}
			}
     
			SqlConnection localDbCn = null;
			SqlCommand dbCmd = null;

			Log.Write("Creating securities black list.... [MedalistMain.SecuritiesInAccountsBlackList]", 2);

			try
			{
				localDbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spSecIdBlackList", localDbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        			
				SqlParameter paramFirm = dbCmd.Parameters.Add("@Firm", SqlDbType.Char, 2);
				paramFirm.Value = Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 0);	
				
				SqlParameter paramLocMemo = dbCmd.Parameters.Add("@LocMemo", SqlDbType.Char, 1);
				paramLocMemo.Value = Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 1);			
				
				SqlParameter paramAccountType = dbCmd.Parameters.Add("@AccountType", SqlDbType.Char, 1);
				paramAccountType.Value = Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 2);   											  
							
				SqlParameter paramCurrencyCode = dbCmd.Parameters.Add("@CurrencyCode", SqlDbType.VarChar, 3);
				paramCurrencyCode.Value = Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 4);			
				
				localDbCn.Open();
				dbCmd.ExecuteNonQuery();
			
				Log.Write("Done creating securities black list. [MedalistMain.SecuritiesInAccountsBlackList]", 2);
				KeyValue.Set("SecuritiesInAccountsBlackListBizDate", Master.BizDate, dbCn);
			}
			catch (Exception e)
			{				
				Log.Write(e.Message + " [MedalistMain.SecuritiesInAccountsBlackList]", Log.Error, 1);				
				throw;
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}			
		}
		
		private void ShortSaleLocatesAutoEmail()
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();			
			
			if (Master.BizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat)))
			{
				if (DateTime.Parse(KeyValue.Get("ShortSaleLocatesAutoEmailWaitUntil", "19:00", dbCnStr)) >= DateTime.UtcNow)
				{
					Log.Write("Waiting until after "+ KeyValue.Get("ShortSaleLocatesAutoEmailWaitUntil", "19:00", dbCnStr)+ " to send emails. [ShortSaleAgent.ShortSaleLocatesAutoEmail]", Log.Information, 1);
					return;
				}
			}
			else
			{
				Log.Write("Must wait until trade date, " + Master.BizDate + ". [MedalistMain.EasyBorrowListMake]", 2);			
			}
			
			
			if (bool.Parse(KeyValue.Get("ShortSaleLocatesAutoEmail", "False", dbCnStr)))
			{
				Log.Write("Starting short sale locates auto email process....[ShortSaleAgent.ShortSaleLocatesAutoEmail]", Log.Information, 1);
			
				//get ShortSale and TradingGroups Data
				try
				{
					SqlCommand dbCmd = new SqlCommand("spTradingGroupGet", dbCn);
					dbCmd.CommandType = CommandType.StoredProcedure;

					SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        
					dataAdapter.Fill(dataSet, "TradingGroups");

					Log.Write("Returning a 'TradingGroups' table with " + dataSet.Tables["TradingGroups"].Rows.Count + " rows. [ShortSaleAgent.TradingGroupsGet]", 3);
				
					dbCmd = new SqlCommand("spShortSaleLocateGet", dbCn);
					dbCmd.CommandType = CommandType.StoredProcedure;
					dbCmd.CommandTimeout = 900;

					SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);			
					paramTradeDate.Value = Master.BizDate;
					
					dataAdapter = new SqlDataAdapter(dbCmd);
        
					dataAdapter.Fill(dataSet, "Locates");

					Log.Write("Returning a 'Locates' table with " + dataSet.Tables["Locates"].Rows.Count + " rows. [ShortSaleAgent.LocatesGet]", 2);				
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [MedalistMain.ShortSaleLocatesAutoEmail]", Log.Error, 1);
				}

				try
				{
					string locates = "";					

					foreach (DataRow dataRowGroup in dataSet.Tables["TradingGroups"].Rows)
					{
						if (bool.Parse(dataRowGroup["AutoEmail"].ToString()) && bool.Parse(dataRowGroup["IsActive"].ToString()))
						{
							if (!dataRowGroup["EmailAddress"].ToString().Equals(""))
							{
								if (!dataRowGroup["LastEmailDate"].ToString().Equals(DateTime.Parse(Master.BizDate).ToString()))
								{								
									locates = "Locate ID" + new string(' ', 2) + "Security ID" +  new string(' ', 2) + "Client Requested" +  new string(' ', 2) + "Filled Quantity" +  new string(' ', 2) + "Fee Rate\t\n";
									locates += "---------" + new string(' ', 2) + "-----------" + new string(' ', 2) + "----------------" +  new string(' ', 2) + "---------------" +  new string(' ', 2) + "--------\t\n";
																
									foreach (DataRow dataRowLocate in dataSet.Tables["Locates"].Rows)
									{										
										if (dataRowGroup["GroupCode"].ToString().Trim().Equals(dataRowLocate["GroupCode"].ToString().Trim()))
										{
											locates += dataRowLocate["LocateId"].ToString().PadLeft(9, ' ') + new string(' ', 2) + dataRowLocate["SecId"].ToString().PadLeft(11, ' ') + new string(' ', 2)
												+ dataRowLocate["ClientQuantity"].ToString().PadLeft(16, ' ') + new string(' ', 2) + dataRowLocate["Quantity"].ToString().PadLeft(15, ' ') + new string(' ', 2) + dataRowLocate["FeeRate"].ToString().PadLeft(8, ' ') + "\t\n";
										}
									}
									Email email = new Email(dbCnStr);
									
									email.Send(dataRowGroup["EmailAddress"].ToString(),
										KeyValue.Get("SmtpMailUser", "stockloan@penson.com", dbCnStr),
										"Short Sale Locates - " + Master.BizDate,
										locates);

									TradingGroupSet(dataRowGroup["GroupCode"].ToString(), 
										dataRowGroup["GroupName"].ToString(),
										dataRowGroup["MinPrice"].ToString(), 
										dataRowGroup["AutoApprovalMax"].ToString(), 
										dataRowGroup["PremiumMin"].ToString(), 
										dataRowGroup["PremiumMax"].ToString(),
										bool.Parse(dataRowGroup["AutoEmail"].ToString()), 
										dataRowGroup["EmailAddress"].ToString(), 												
										Master.BizDate, 
										"ADMIN");
								}
								else
								{
									Log.Write("Group: " + dataRowGroup["GroupCode"].ToString()+ " email has already been sent for today. [MedalistMain.ShortSaleLocatesAutoEmail]", Log.Information, 3);
								}
							}
							else
							{
								Log.Write("Group: " + dataRowGroup["GroupCode"].ToString()+ " has no email address. [MedalistMain.ShortSaleLocatesAutoEmail]", Log.Information, 3);
							}
						}				
					}
				}				
				catch (Exception error)
				{
					Log.Write(error.Message + "[ShortSaleAgent.ShortSaleLocatesAutoEmail]", Log.Error, 1);
				}
			}
			else
			{
				Log.Write("Short sale locates auto email process disabled. [ShortSaleAgent.ShortSaleLocatesAutoEmail]", Log.Information, 1);
			}
		}

		public void ShortSaleDailyQuantitiesPurge()
		{
			if(KeyValue.Get("ShortSaleDailyQuantitiesPurgeBizDate", "0000-01-01", dbCn).Equals(Master.BizDateBank))
			{
				Log.Write("ShortSale Daily Quantities purge already completed for " + Master.BizDateBank + ". [MedalistMain.ShortSaleDailyQuantitiesPurge]",  Log.Information, 2);
				return;
			}
			else
			{
				SqlCommand dbCmd = null;

				try
				{
					dbCn = new SqlConnection(dbCnStr);
					dbCmd = new SqlCommand("spShortSaleDailyQuantitiesPurge", dbCn);
					dbCmd.CommandType = CommandType.StoredProcedure;                    

					dbCn.Open();
					dbCmd.ExecuteNonQuery();
					dbCn.Close();
			  
					Log.Write("ShortSale Daily Quantities purge completed for " + Master.BizDateBank + ". [MedalistMain.ShortSaleDailyQuantitiesPurge]", Log.Information, 2);
					KeyValue.Set("ShortSaleDailyQuantitiesPurgeBizDate", Master.BizDateBank, dbCn);
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [MedalistMain.ShortSaleDailyQuantitiesPurge]", Log.Error, 1);				  
				}
				finally
				{
					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
				}
			}
		}

		void TradingGroupSet (
			string groupCode, 
			string groupName, 
			string minPrice, 
			string autoApprovalMax, 
			string premiumMin, 
			string premiumMax,
			bool	 autoEmail,
			string emailAddress, 
			string lastEmailDate,	
			string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spTradingGroupSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
				paramGroupCode.Value = groupCode;

				
				SqlParameter paramGroupName = dbCmd.Parameters.Add("@GroupName", SqlDbType.VarChar, 50);
				if (!groupName.Equals(""))
				{
					paramGroupName.Value = groupName;
				}

				SqlParameter paramMinPrice = dbCmd.Parameters.Add("@MinPrice", SqlDbType.Float);
				if (!minPrice.Equals(""))
				{
					paramMinPrice.Value = minPrice;
				}

				SqlParameter paramAutoApprovalMax = dbCmd.Parameters.Add("@AutoApprovalMax", SqlDbType.BigInt);
				if (!autoApprovalMax.Equals("")&& (Tools.IsNumeric(autoApprovalMax)))
				{
					paramAutoApprovalMax.Value = autoApprovalMax;
				}			
				
				SqlParameter paramPremiumMin = dbCmd.Parameters.Add("@PremiumMin", SqlDbType.BigInt);
				if (!premiumMin.Equals(""))
				{
					paramPremiumMin.Value = premiumMin;
				}

				SqlParameter paramPremiumMax = dbCmd.Parameters.Add("@PremiumMax", SqlDbType.BigInt);
				if (!premiumMax.Equals(""))
				{
					paramPremiumMax.Value = premiumMax;
				}

				SqlParameter paramAutoEmail = dbCmd.Parameters.Add("@AutoEmail", SqlDbType.Bit);
				paramAutoEmail.Value = autoEmail;

				SqlParameter paramEmailAddress = dbCmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 100);
				if (!emailAddress.Equals(""))
				{
					paramEmailAddress.Value = emailAddress;
				}

				SqlParameter paramLastEmailDate = dbCmd.Parameters.Add("@LastEmailDate", SqlDbType.DateTime);
				if (!lastEmailDate.Equals(""))
				{
					paramLastEmailDate.Value = lastEmailDate;
				}
				
				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;
				
				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [MedalistMain.TradingGroupSet]", Log.Error, 1);
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}
		
		private void ShortSaleFailsEmail()
		{
			if(KeyValue.Get("ShortSaleFailsEmailBizDate", "0000-01-01", dbCnStr).Equals(Master.BizDate))
			{
				Log.Write("ShortSale Fails Email purge already sent for " + Master.BizDate + ". [MedalistMain.ShortSaleFailsEmail]",  Log.Information, 2);
				return;
			}
			else
			{
				SqlConnection tempdbCn = null;
				SqlCommand dbCmd = null;
				SqlDataReader dataReader = null;
				
				string	sqlText = "",
								secIdList = "";
				string failDayCount = KeyValue.Get("ShortSaleLocateFailDayCountMax", "1", dbCnStr);

				try
				{					
					sqlText = "Select distinct(S.SecId), \n" +
										"				SIl.SecIdLink As Symbol\n" +
										"From tbShortSaleLocates S left join tbBoxPosition B on B.SecId = S.SecId,\n" + 
										"			tbSecIdLinks SIL\n" + 
										"Where S.TradeDate = '" + Master.BizDatePrior + "'\n" +
										"And B.BizDate = '" + Master.BizDatePrior + "'\n" +
										"And B.BookGroup = '0234'\n" +
										"And S.Status = 'None'\n" + 
										"And S.Comment  = 'No Supply.'\n" +
										"And S.Quantity = 0\n" + 
										"And B.NetPositionSettledDayCount > " + failDayCount + "\n" +
										"And S.SecId Not In (Select SecId From tbBorrowNo Where EndTime is Null)\n" +
										"And SIL.SecId = S.SecId\n" +
										"And SIL.SecIdTypeIndex = 2\n" +
										"Order By Symbol\n";
										
					tempdbCn = new SqlConnection(dbCnStr);
					dbCmd = new SqlCommand(sqlText, tempdbCn);
					dbCmd.CommandType = CommandType.Text;                    

					tempdbCn.Open();
					dataReader = dbCmd.ExecuteReader();

					while(dataReader.Read())
					{
						secIdList += dataReader.GetValue(0).ToString() + " " + dataReader.GetValue(1).ToString() + "\n";
					}

					Email email = new Email(dbCnStr);
					email.Send(
						KeyValue.Get("ShortSaleFailsMailTo", "mbattaini@penson.com", dbCnStr),
						KeyValue.Get("ShortSaleFailsMailFrom", "stockloan@penson.com", dbCnStr),
						"Sendero Short Sale Fails For " + Master.BizDatePrior, 
						secIdList);
					
					Log.Write("ShortSale Fail Email sent for " + Master.BizDateBank + ". [MedalistMain.ShortSaleFailsEmail]", Log.Information, 2);
					KeyValue.Set("ShortSaleFailsEmailBizDate", Master.BizDate, dbCn);
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [MedalistMain.ShortSaleFailsEmail]", Log.Error, 1);				  
				}
				finally
				{
					if ((dataReader != null) && (!dataReader.IsClosed))
					{
						dataReader.Close();
					}
        
					if (tempdbCn.State != ConnectionState.Closed)
					{
						tempdbCn.Close();
					}
				}
			}
		}

		private TimeSpan RecycleInterval()
		{
			string recycleInterval;
			string [] values;

			int hours;
			int minutes;

			bool isBizDay = Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Any, dbCn);
			TimeSpan timeSpan;

			char [] delimiter = new char[1];
			delimiter[0] = ':';

			if (isBizDay)
			{
				recycleInterval = KeyValue.Get("MedalistMainLoopRecycleIntervalBizDay", "0:15", dbCn);
			}
			else
			{
				recycleInterval = KeyValue.Get("MedalistMainLoopRecycleIntervalNonBizDay", "4:00", dbCn);
			}

			try
			{
				values = recycleInterval.Split(delimiter, 2);
				hours = int.Parse(values[0]);
				minutes = int.Parse(values[1]);
				timeSpan = new TimeSpan (hours, minutes, 0);
			}
			catch
			{
				if (isBizDay)
				{
					KeyValue.Set("MedalistMainRecycleIntervalBizDay", "0:15", dbCn);
					hours = 0;
					minutes = 30;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MedalistMainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [MedalistMain.RecycleInterval]", Log.Error, 1);
				}
				else
				{
					KeyValue.Set("MedalistMainRecycleIntervalNonBizDay", "4:00", dbCn);
					hours = 6;
					minutes = 0;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MedalistMainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [MedalistMain.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("MedalistMain will recycle in " + hours + " hours, " + minutes + " minutes. [MedalistMain.RecycleInterval]", 2);
			return timeSpan;
		}

		public static string TempPath
		{
			get
			{
				return tempPath;
			}
		}

		public static bool IsStopped
		{
			get
			{
				return isStopped;
			}
		}

		public PositionAgent MedalistMainPositionAgent
		{
			set
			{
				positionAgent = value;
			}
		}

		public SubstitutionAgent MedalistMainSubstitutionAgent
		{
			set
			{
				substitutionAgent = value;
			}
		}
	}
}

