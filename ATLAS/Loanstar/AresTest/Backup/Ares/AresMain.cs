using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using	System.Collections;
using Anetics.Common;

namespace Anetics.Ares
{
	public class AresMain
	{
		private string notificationList;
		private string emailForm;
		private string dbCnStr;
		private string worldwideDbCnStr;			

		private SqlConnection dbCn = null;

		private string previousEmail = "";

		private Thread workerThread = null;
		private static bool isStopped = true;
		private static string tempPath;

		private Email email;

		
    
		public AresMain(string dbCnStr, string worldwideDbCnStr)
		{
			this.dbCnStr = dbCnStr;
			this.worldwideDbCnStr = worldwideDbCnStr;
						
			try
			{
				dbCn = new SqlConnection(dbCnStr);
      
				email = new Email(dbCnStr);							
							
				emailForm =  KeyValue.Get("AresEmailFrom", "stockloan@penson.com", dbCnStr);
				notificationList = KeyValue.Get("AresEmailTo", "", dbCnStr);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [AresMain.AresMain]", Log.Error, 1);
			}

			if (Standard.ConfigValueExists("TempPath"))
			{
				tempPath = Standard.ConfigValue("TempPath");

				if (!Directory.Exists(tempPath))
				{
					Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [AresMain.AresMain]", Log.Error, 1);
					tempPath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
				Log.Write("A configuration value for TempPath has not been provided. [AresMain.AresMain]", Log.Information, 1);
				tempPath = Directory.GetCurrentDirectory();
			}

			Log.Write("Temporary files will be staged at " + tempPath + ". [AresMain.AresMain]", 2);
		}

		~AresMain()
		{
			if (dbCn != null)
			{
				dbCn.Dispose();
			}
		}

		public void Start()
		{
			isStopped = false;

			if ((workerThread == null)||(!workerThread.IsAlive)) // Must create new thread.
			{
				workerThread = new Thread(new ThreadStart(AresMainLoop));
				workerThread.Name = "Worker";
				workerThread.Start();

				Log.Write("Start command issued with new worker thread. [AresMain.Start]", 2);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Start command issued with worker thread already running. [AresMain.Start]", 2);
			}
		}

		public void Stop()
		{
			isStopped = true;

			if (workerThread == null)
			{
				Log.Write("Stop command issued, worker thread never started. [AresMain.Stop]", 2);
			}
			else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				workerThread.Abort();
				Log.Write("Stop command issued, sleeping worker thread aborted. [AresMain.Stop]", 2);
			}
			else
			{
				Log.Write("Stop command issued, worker thread is still active. [AresMain.Stop]", 2);
			}
		}

		private void AresMainLoop()
		{
			while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
			{
				Log.Write("Start-of-cycle. [AresMain.AresMainLoop]", 2);
				KeyValue.Set("AresMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        
				Master.BizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);
				Master.BizDatePrior = KeyValue.Get("BizDatePrior", "0001-01-01", dbCn);
				Master.BizDateExchange = KeyValue.Get("BizDateExchange", "0001-01-01", dbCn);
				Master.BizDatePriorExchange = KeyValue.Get("BizDatePriorExchange", "0001-01-01", dbCn);
				Master.ContractsBizDate =	KeyValue.Get("ContractsBizDate", "0001-01-01", dbCn);
				Master.BizDateNext = KeyValue.Get("BizDateNext", "0001-01-01", dbCn);
								
				if (KeyValue.Get("S3StartOfDayDate", "", dbCnStr).Equals(Master.BizDate))
				{					
					BankLoanExceptions();
					BankLoanLocationExceptions();
					RateDifferencesEmail();
					FailsEmail();					
					SegEntriesComparison();
					Straddles();	
					FailedS3MovementsEmail();
					SmartSegSummary();

					try
					{
						StockBorrowUploadFile();
					}
					catch (Exception error)
					{
						Log.Write(error.Message, 1);
					}

				}

				ThresholdRatesMail();
				HardRatesMail();
				RecordDateContractsMail();					
				CreditDebitBalances();
				LikeTrades();
				DeficitReport();
				
				EasyBorrowRatesMail();
				PremiumAutoSet();

				HardToBorrowChargesCSV("", true, "");
				HardToBorrowChargesCSV("SWIM",true, "");
				HardToBorrowChargesCSV("TORC", false, @"\\Rsdmz001\reports\Firm07\TORP\{yyyyMMdd}");

				SchonfeldShortSaleNegativeRebateBillingFileDo();			
				HardToBorrowRatesFile();
				HardToBorrowProfitabilityMail();
				LondonContractsMail();
				AvailabilityFeed();
				HardToBorrowNotBilledMail();

				//CNSFailsMail();								// Daily 6:05 AM (UTC 11:05)
				//HTBAccountsMail();						// Daily 6:10 AM (UTC 11:10)
				//ShortsVsFtdMail();						// Daily 3:00 PM (UTC 20:30)

				KeyValue.Set("AresMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
				Log.Write("End-of-cycle. [AresMain.AresMainLoop]", 2);

				if (!isStopped)
				{
					Thread.Sleep(RecycleInterval());
				}
			}
		}			

		public void CreditDebitBalances()
		{
			if (Master.ContractsBizDate.Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresCreditDebitBalancesSetTime", "06:30", dbCnStr), "HH:mm", null))									
				{
					if (!KeyValue.Get("AresCreditDebitBalancesMailDate", "", dbCnStr).Equals(Master.BizDate))
					{
						try
						{
							string sql = "Select	P.processDate,\r\n" +
								"Sum(Case When S.SettlementDateQuantity >= 0 And S.LocMemo = 'C' Then S.SettlementDateQuantity* P.ClosingPrice  Else 0 End ) As LongAmt,\r\n" +
								"Sum(Case When S.SettlementDateQuantity < 0 And S.LocMemo = 'C' Then S.SettlementDateQuantity * P.ClosingPrice Else 0 End ) As ShortAmt\r\n" +
								"From	dbo.StockLocationCurrent S,\r\n" +
								"		dbo.SecurityPrice P,\r\n" +
								"		dbo.SecurityBase B\r\n" +
								"Where	P.ProcessDate= '" + Master.BizDatePrior + "'\r\n" +
								"And		S.Firm = '07'\r\n" +
								"And		P.Firm = S.Firm\r\n" +
								"And		B.Firm = S.Firm\r\n" +
								"And		P.Cusip = S.Cusip\r\n" +
								"And		B.Cusip = S.Cusip\r\n" +
								"And		B.SecurityTypeCode In ('1', '4', 'A', 'B', 'C')\r\n" +
								"Group By P.processDate";
						
							DataSet dsTemp = new DataSet();
							string emailBody = "";

							SqlConnection localDbCn = new SqlConnection(dbCnStr);
							SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
							dbCmd.CommandType = CommandType.Text;
							
							SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
							dataAdapter.Fill(dsTemp, "CreditDebit");				
						

							foreach(DataRow dr in dsTemp.Tables["CreditDebit"].Rows)
							{
								emailBody += DateTime.Parse(dr["ProcessDate"].ToString()).ToString(Standard.DateFormat) + " " + decimal.Parse(dr["LongAmt"].ToString()).ToString("#,##0.00") + " " + decimal.Parse(dr["ShortAmt"].ToString()).ToString("#,##0.00") + "\r\n";
							}

							string to = KeyValue.Get("AresCreditDebitBalancesMailTo", "mbattaini@penson.com", dbCnStr);							
						
							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresCreditDebitBalancesMailFrom", "stockloan@penson.com", dbCnStr),
									KeyValue.Get("AresCreditDebitBalancesMailSubject", "Credit / Debit Balances For  ", dbCnStr)  + Master.BizDate,
									emailBody);						
							}
	
							KeyValue.Set("AresCreditDebitBalancesMailDate", Master.ContractsBizDate, dbCnStr);
							
						}
						catch (Exception error)
						{
							Log.Write(error.Message + "[AresMain.CreditDebitBalances]", 1);
						}
					}
				}
			}
		}

		public void LondonContractsMail()
		{
			DataSet dsContracts = new DataSet();
			SqlConnection localDbCn = null;
			SqlDataAdapter dataAdapter = null;

			string numOfRecords = "";
			string body = "";
			
			
			if (Master.ContractsBizDate.Equals(Master.BizDate))
			{
				if (Master.ContractsBizDate.Equals(KeyValue.Get("AresLondonContractsMailDate", "", dbCnStr)))
				{
					Log.Write("London Contracts Email has already occured. [AresMain.LondonContractsMail]", 1);					
				}
				else
				{
					try
					{		
						string sql =	"select	c.bookgroup,\r\n"+
							"c.book,\r\n" +
							"(Select BookName From dbo.tbBooks (nolock) Where BookGroup = '0234' And	Book = C.Book) As BookName,\r\n" +
							"c.contracttype,\r\n" +
							"c.secid,\r\n" +
							"s.symbol,\r\n" +
							"c.quantity,\r\n" +
							"c.amount,\r\n" +
							"c.rate,\r\n" +
							"c.margin\r\n" +
							"from	dbo.tbContracts C,\r\n" +
							"dbo.SecurityBase S\r\n" +
							"where	c.bizdate = '" + Master.ContractsBizDate + "'\r\n" +
							"and		c.bookgroup = '0234'\r\n" +
							"and		c.poolcode = 'L'\r\n" +
							"and		s.cusip = c.secid\r\n" +
							"Order By BookGroup";
																	
						localDbCn = new SqlConnection(dbCnStr);
						
						SqlCommand dbCmd = new SqlCommand(sql, localDbCn);
						dbCmd.CommandType = CommandType.Text;
						
						dataAdapter = new SqlDataAdapter(dbCmd);
						dataAdapter.Fill(dsContracts, "contracts");	
																		
						
						body = "London Contracts\r\n\r\n";
						body += "BookGroup".PadRight(11, ' ') + "Book".PadRight(11, ' ') + "Name".PadRight(18, ' ') + "T".PadRight(3, ' ') + "Security ID".PadRight(15, ' ') + "Symbol".PadRight(8, ' ') +  "Quantity".PadRight(10, ' ') + "Amount".PadRight(15, ' ') + "Rate".PadRight(8) + "Margin".PadRight(8) +"\r\n";
						body += "---------".PadRight(11, ' ') + "----".PadRight(11, ' ') + "----".PadRight(18, ' ') +  "-".PadRight(3, ' ') + "-----------".PadRight(15, ' ') + "------".PadRight(8, ' ') +  "--------".PadRight(10, ' ') + "------".PadRight(15, ' ') + "----".PadRight(8) + "------".PadRight(8) +"\r\n";
						
						foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
						{
							body += dr["BookGroup"].ToString().PadRight(11, ' ') +
								dr["Book"].ToString().PadRight(11, ' ') + 
								dr["BookName"].ToString().PadRight(18).Substring(0, 18) +
								dr["ContractType"].ToString().PadRight(3, ' ') +								
								dr["SecId"].ToString().PadRight(15, ' ') +
								dr["Symbol"].ToString().PadRight(8, ' ').Substring(0, 8) +
								long.Parse(dr["Quantity"].ToString()).ToString("#,##0").PadRight(12, ' ').Substring(0, 12)  +
								decimal.Parse(dr["Amount"].ToString()).ToString("#,##0.00").PadRight(15, ' ').Substring(0, 15) +
								decimal.Parse(dr["Rate"].ToString()).ToString("0.00").PadRight(8, ' ').Substring(0, 8) +
								decimal.Parse(dr["Margin"].ToString()).ToString("0.00").PadRight(8, ' ').Substring(0, 8) + "\r\n";
						}

						string to = KeyValue.Get("AresLondonContractsMailTo", "mbattaini@penson.com", dbCnStr);							
						
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresLondonContractsMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresLondonContractsMailSubject", "London Contarcts For  ", dbCnStr) + Master.ContractsBizDate,
								body);						
						}
	
						KeyValue.Set("AresLondonContractsMailDate", Master.ContractsBizDate, dbCnStr);
					}
					catch (Exception error)
					{
						Log.Write(error.Message + "[AresMain.AresLondonContractsMail]", 1);
					}
					finally
					{
						if (localDbCn.State != ConnectionState.Closed)
						{
							localDbCn.Close();
						}
					}
				}				
			}
		}

		public void HardToBorrowProfitabilityMail()
		{
			DataSet dsHardToBorrow = new DataSet();
			SqlConnection localDbCn = null;
			SqlDataAdapter dataAdapter = null;

			string numOfRecords = "";
			string body = "";
			
			
			if (Master.ContractsBizDate.Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				if (Master.ContractsBizDate.Equals(KeyValue.Get("AresHardToBorrowProfitabilityMailDate", "", dbCnStr)))
				{
					Log.Write("Hard To Borrow Profitability has already occured. [AresMain.AresHardToBorrowProfitabilityMail]", 1);					
				}
				else if (KeyValue.Get("ShortSaleNegRebateBillingSnapShotBizDate", "", dbCnStr).Equals(DateTime.Now.AddDays(-1.0).ToString(Standard.DateFormat)))
				{
					try
					{		
						numOfRecords = KeyValue.Get("AresHardToBorrowProfitabilityNumOfRecords", "15", dbCnStr);
						
						string sql = "select	top(" + numOfRecords+ ") s.Secid,\r\n"+
							"b.symbol,\r\n"+
							"sum(isnull(s.originalcharge, 0)) as cost,\r\n" +
							"sum(isnull(s.modifiedcharge, 0)) as charge\r\n" +
							"from	dbo.tbshortsalebillingsummary s,\r\n" +
							"dbo.securitybase b\r\n" +
							"where	s.bizdate = '" + Master.BizDatePrior + "'\r\n" +
							"and		b.cusip = s.secid\r\n" +
							"Group By s.SecId, b.symbol\r\n" +
							"Order By charge DESC";						
						
						localDbCn = new SqlConnection(dbCnStr);
						
						SqlCommand dbCmd = new SqlCommand(sql, localDbCn);
						dbCmd.CommandType = CommandType.Text;
						
						dataAdapter = new SqlDataAdapter(dbCmd);
						dataAdapter.Fill(dsHardToBorrow, "HTB");	
																		
						
						body = "Most Profitable Securities\r\n\r\n";
						body += "Cusip".PadRight(11, ' ') + "Symbol".PadRight(10, ' ') + "Cost".PadRight(15, ' ') + "Charge".PadRight(15, ' ') + "\r\n";
						body += "-----".PadRight(11, ' ') + "------".PadRight(10, ' ') + "----".PadRight(15, ' ') + "------".PadRight(15, ' ') + "\r\n";
						
						foreach (DataRow dr in dsHardToBorrow.Tables["HTB"].Rows)
						{
							body += dr["SecId"].ToString().PadRight(11, ' ') +
								dr["Symbol"].ToString().PadRight(10, ' ') +
								decimal.Parse(dr["Cost"].ToString()).ToString("#,##0.00").PadLeft(15, ' ').Substring(0, 15) +
								decimal.Parse(dr["Charge"].ToString()).ToString("#,##0.00").PadLeft(15, ' ').Substring(0, 15) + "\r\n";
						}


						body += "\r\n\r\n\r\n";
						body += "Deepest Hard To Borrow Rates\r\n";

						sql = "select	distinct(s.Secid),\r\n" +
							"B.symbol,\r\n" +
							"s.rate\r\n" +
							"from	dbo.tbshortsalebillingsummary s,\r\n" + 
							"			dbo.securitybase b\r\n" +
							"where	s.bizdate = '" + Master.BizDatePrior +"'\r\n" +
							"and		b.cusip = s.secid\r\n" +
							"and		s.rate is not null\r\n" +
							"Group By s.SecId, b.symbol, s.rate\r\n" +
							"Order By s.rate asc";

						
						dbCmd.CommandText = sql;
						dataAdapter.Fill(dsHardToBorrow, "HTBRates");	

						body += "Cusip".PadRight(11, ' ') + "Symbol".PadRight(10, ' ') + "Rate".PadRight(8, ' ') + "\r\n";
						body += "-----".PadRight(11, ' ') + "------".PadRight(10, ' ') + "----".PadRight(8, ' ') + "\r\n";					

						int counter = int.Parse(numOfRecords);

						foreach (DataRow dr in dsHardToBorrow.Tables["HTBRates"].Rows)
						{
							if (counter == 0) break;

							body += dr["SecId"].ToString().PadRight(11, ' ') +
								dr["Symbol"].ToString().PadRight(10, ' ') +
								float.Parse(dr["Rate"].ToString()).ToString("0.000").PadLeft(8, ' ') + "\r\n";
							
							counter --;
						}

						string to = KeyValue.Get("AresHardToBorrowProfitabilityMailTo", "mbattaini@penson.com", dbCnStr);							
						
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresHardToBorrowProfitabilityMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresHardToBorrowProfitabilityMailSubject", "Hard To Borrow Profitability For ", dbCnStr) + Master.BizDatePrior,
								body);						
						}
	
						KeyValue.Set("AresHardToBorrowProfitabilityMailDate", Master.ContractsBizDate, dbCnStr);
					}
					catch (Exception error)
					{
						Log.Write(error.Message + "[AresMain.AresHardToBorrowProfitabilityMail]", 1);
					}
					finally
					{
						if (localDbCn.State != ConnectionState.Closed)
						{
							localDbCn.Close();
						}
					}
				}				
			}
		}

		public void HardToBorrowNotBilledMail()
		{
			DataSet dsHardToBorrow = new DataSet();
			SqlConnection localDbCn = null;
			SqlDataAdapter dataAdapter = null;
	
			string body = "";
			
			
			if (Master.ContractsBizDate.Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				if (Master.ContractsBizDate.Equals(KeyValue.Get("AresHardToBorrowNotBilledMailDate", "", dbCnStr)))
				{
					Log.Write("Hard To Borrow Profitability has already occured. [AresMain.AresHardToBorrowProfitabilityMail]", 1);					
				}
				else if (KeyValue.Get("ShortSaleNegRebateBillingSnapShotBizDate", "", dbCnStr).Equals(Master.BizDate))
				{
					try
					{		
						
						string sql = "SELECT	DISTINCT(C.SECID) AS SECID,\r\n"+
																	"ISNULL(SUM(C.QUANTITY), 0) AS QUANTITY,\r\n"+
																	"AVG(C.RATE) AS RATE,\r\n"+
																	"ISNULL((SELECT CUSTOMERSHORTSETTLED FROM TBBOXPOSITION WHERE BIZDATE = C.BIZDATE AND SECID = C.SECID AND BOOKGROUP = C.BOOKGROUP), 0) AS SHORT,\r\n"+
																	"ISNULL((SELECT  IsNull(ClearingFailOutSettled, 0) + IsNull(DvpFailOutSettled, 0) + IsNull(BrokerFailOutSettled, 0) FROM TBBOXPOSITION WHERE BIZDATE = C.BIZDATE AND SECID = C.SECID AND BOOKGROUP = C.BOOKGROUP), 0) AS FAILS\r\n"+
													"INTO		#SHORTS\r\n"+
													"FROM		TBCONTRACTS C\r\n"+
													"WHERE	C.BIZDATE = '" + Master.BizDate + "'\r\n"+
													"AND			C.CONTRACTTYPE = 'B'\r\n"+
													"AND			C.BOOKGROUP = '0234'\r\n"+
													"AND			(C.RATE < 0 OR C.POOLCODE = '6')\r\n"+
													"AND			C.QUANTITY > 0\r\n" +
													"AND			C.SECID NOT IN (SELECT SECID FROM TBSHORTSALEBILLINGSUMMARY WHERE	BIZDATE = C.BIZDATE)\r\n"+
													"GROUP BY C.SECID, C.BIZDATE, C.BOOKGROUP\r\n"+
													"DELETE	FROM	#SHORTS WHERE SHORT = 0 OR QUANTITY = 0\r\n"+
													"SELECT	S.SECID,\r\n"+
													"				B.SYMBOL,\r\n"+
													"				S.QUANTITY,\r\n"+
													"				S.RATE,\r\n"+
													"				S.SHORT,\r\n"+
													"				S.FAILS\r\n"+
													"FROM		#SHORTS S,\r\n"+
													"				SECURITYBASE B	\r\n"+	
													"WHERE	B.CUSIP = S.SECID\r\n"+
													"ORDER BY B.SYMBOL\r\n";

						localDbCn = new SqlConnection(dbCnStr);
						
						SqlCommand dbCmd = new SqlCommand(sql, localDbCn);
						dbCmd.CommandType = CommandType.Text;
						
						dataAdapter = new SqlDataAdapter(dbCmd);
						dataAdapter.Fill(dsHardToBorrow, "HTB");	
																		
						
					
						body = "Cusip".PadRight(11, ' ') + "Symbol".PadRight(10, ' ') + "Quantity".PadLeft(15, ' ') + "Rate".PadLeft(8, ' ') + "Short".PadLeft(15, ' ') + "Fails".PadLeft(15, ' ') + "\r\n";
						body += "-----".PadRight(11, ' ') + "------".PadRight(10, ' ') + "--------".PadLeft(15, ' ') + "----".PadLeft(8, ' ') + "-----".PadLeft(15, ' ') + "-----".PadLeft(15, ' ') + "\r\n";
						
						foreach (DataRow dr in dsHardToBorrow.Tables["HTB"].Rows)
						{
							body += dr["SecId"].ToString().PadRight(11, ' ') +
								dr["Symbol"].ToString().PadRight(10, ' ') +
								long.Parse(dr["Quantity"].ToString()).ToString("#,##0").PadLeft(15, ' ').Substring(0, 15) +
								decimal.Parse(dr["Rate"].ToString()).ToString("0.00").PadLeft(8, ' ').Substring(0, 8) +
								long.Parse(dr["Short"].ToString()).ToString("#,##0").PadLeft(15, ' ').Substring(0, 15) +
								long.Parse(dr["Fails"].ToString()).ToString("#,##0").PadLeft(15, ' ').Substring(0, 15)  + "\r\n";
						}
										

						string to = KeyValue.Get("AresHardToBorrowNotBilledMailTo", "mbattaini@penson.com", dbCnStr);							
						
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresHardToBorrowNotBilledMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresHardToBorrowNotBilledMailSubject", "Hard To Borrow Not Billed For ", dbCnStr) + Master.BizDate,
								body);						
						}
	
						KeyValue.Set("AresHardToBorrowNotBilledMailDate", Master.ContractsBizDate, dbCnStr);
					}
					catch (Exception error)
					{
						Log.Write(error.Message + "[AresMain.HardToBorrowNotBilledMail]", 1);
					}
					finally
					{
						if (localDbCn.State != ConnectionState.Closed)
						{
							localDbCn.Close();
						}
					}
				}				
			}
		}



		private void PremiumAutoSet()
		{
			SqlConnection localDbCn = null;

			if (Master.ContractsBizDate.Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				if (Master.ContractsBizDate.Equals(KeyValue.Get("AresPremiumAutoSetDate", "", dbCnStr)))
				{
					Log.Write("Premium Auto Set has already occured. [AresMain.PremiumAutoSet]", 1);					
				}
				else 	if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresPremiumAutoSetTime", "04:30", dbCnStr), "HH:mm", null))									
				{
					try
					{						
						localDbCn = new SqlConnection(dbCnStr);
						SqlCommand dbCmd = new SqlCommand("spPremiumAutoSet",localDbCn);
						dbCmd.CommandType = CommandType.StoredProcedure;
						
						SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
						paramBizDate.Value = Master.BizDate;

						SqlParameter paramRecordCount = dbCmd.Parameters.Add("@RecordCount", SqlDbType.BigInt);
						paramRecordCount.Direction = ParameterDirection.Output;
							
						localDbCn.Open();
						dbCmd.ExecuteNonQuery();
												
						string emailBody = "Premium Auto Set " + long.Parse(paramRecordCount.Value.ToString()).ToString("#,###0") + " item(s).";
						
						string to = KeyValue.Get("AresPremiumAutoSetMailTo", "mbattaini@penson.com", dbCnStr);							
						
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresPremiumAutoSetMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresPremiumAutoSetMailSubject", "Premium Auto  ", dbCnStr)  + Master.BizDate,
								emailBody);						
						}
	
						KeyValue.Set("AresPremiumAutoSetDate", Master.ContractsBizDate, dbCnStr);
					}
					catch (Exception error)
					{
						Log.Write(error.Message + "[AresMain.PremiumAutoSet]", 1);
					}
					finally
					{
						if (localDbCn.State != ConnectionState.Closed)
						{
							localDbCn.Close();
						}
					}
				}
				else
				{
					Log.Write("Must wait until " + KeyValue.Get("AresPremiumAutoSetTime", "04:30", dbCnStr) + ". [AresMain.PremiumAutoSet]", 1);
				}
			}
		}

		private void HardToBorrowChargesCSV(string groupCode, bool isEmail, string remoteFilePath)
		{
			if (Master.ContractsBizDate.Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				if (Master.ContractsBizDate.Equals(KeyValue.Get("AresHtbChargesCSV_" + groupCode + "_Date", "", dbCnStr)))
				{
					Log.Write("HTB Charges CSV has been sent for " + groupCode + ". [AresMain.PremiumAutoSet]", 1);					
				}
				else 	if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresHtbChargesCSVTime", "04:30", dbCnStr), "HH:mm", null))									
				{
						
					StreamWriter sr = null;
				
					string fileName = "";
					string tempFileName = "";
					string fileContent = "";				
					
					int index = 0;

					string tempPath = KeyValue.Get("AresHtbChargesCSVFilePath", @"C:\", dbCnStr);

					try
					{
						string sql ="select	groupcode,\r\n" +
							"accountnumber,\r\n"+
							"sum(isnull(modifiedcharge, 0)) As Charge\r\n" + 
							"from	dbo.tbshortsalebillingsummary\r\n" +
							"where	bizdate >= '" + Master.BizDatePrior+ "'\r\n" + 												
							(groupCode.Equals("")?"":"And GroupCode = '" + groupCode + "'\r\n") +
							"Group by groupcode, accountnumber\r\n" +
							"Order by groupcode, accountnumber\r\n";
												
						DataSet dsTemp = new DataSet();

						SqlConnection localDbCn = new SqlConnection(dbCnStr);
						SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
						dbCmd.CommandType = CommandType.Text;

							
						SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
						dataAdapter.Fill(dsTemp, "Charges");				
						

						foreach(DataRow dr in dsTemp.Tables["Charges"].Rows)
						{
							fileContent += dr["GroupCode"].ToString() + "," + dr["AccountNumber"].ToString() + "," + dr["Charge"].ToString() + "\r\n";
						}
						
						
						tempFileName = @"\HHTB_ChargesCSV_" + groupCode + "_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyyMMdd") + ".csv";					
						fileName = tempPath + tempFileName;
						sr = File.CreateText(fileName);
						sr.Write(fileContent);
						sr.Close();															
												
						
						if (isEmail)
						{																							
							string to = KeyValue.Get("AresHtbCharges_" + groupCode + "_To", "mbattaini@penson.com", dbCnStr);							
						
							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresHtbCharges_" + groupCode + "_From", "sendero@penson.com", dbCnStr),
									"HTB Charges For " + groupCode.ToUpper() + " " + Master.ContractsBizDate,
									"",
									fileName);						
							}
						}
						else
						{
							Filer filer = new Filer();

							remoteFilePath = DatePartSet(remoteFilePath, Master.BizDate);
							
							filer.FileNoTempPut(remoteFilePath + tempFileName, "localHost", "", "", fileName);
						}
	
						KeyValue.Set("AresHtbChargesCSV_" + groupCode + "_Date", Master.ContractsBizDate, dbCnStr);
					}
					catch (Exception error)
					{
						Log.Write(error.Message + "[AresMain.HardToBorrowChargesCSV]", 1);
					}
				}
				else
				{
					Log.Write("Must wait until " + KeyValue.Get("AresHtbChargesCSVTime", "04:30", dbCnStr) + ". [AresMain.HardToBorrowChargesCSV]", 1);
				}
			}
			else
			{
				Log.Write("Must wait until current buisness date. [AresMain.HardToBorrowChargesCSV]", 1);
			}
		}

		private void SchonfeldShortSaleNegativeRebateBillingFileDo()
		{			
			if (KeyValue.Get("ShortSaleNegRebateBillingSnapShotBizDate", "", dbCnStr).Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				if (!KeyValue.Get("AresSchonfeldShortSaleNegRebateFileDate", "", dbCnStr).Equals(DateTime.Now.ToString(Standard.DateFormat)))
				{

					StreamWriter sr = null;

					try
					{
						SqlConnection dbCn = null;

						SqlCommand dbCmd = null;
						SqlParameter paramStartDate = null;
						SqlParameter paramStopDate = null;
						SqlDataAdapter dataAdapter = null;

						DataSet dataSet = new DataSet();
						
						string groupCodeText = "";
						string filePath = "";
				
						groupCodeText = KeyValue.Get("AresSchonfeldGroupCodeList", @"LSPS;STRD;SCHO;OPUS;QTSL;FNYS;WHEA;TRIL;WHNE;TORB;MAXT;FIRS;", dbCnStr);
						filePath =  KeyValue.Get("AresSchonfeldFilePath", @"\\Rsdmz001\Reports\firm07\Schonfeld\NEG_REBATES", dbCnStr);

						dataSet = new DataSet();

						dbCn = new SqlConnection(dbCnStr);
						dbCmd = new SqlCommand("spShortSaleBillingSummaryGet", dbCn);
						dbCmd.CommandType = CommandType.StoredProcedure;
						dbCmd.CommandTimeout = 600;

						paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
						paramStartDate.Value = DateTime.Now.ToString(Standard.DateFormat);

						paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
						paramStopDate.Value = DateTime.Now.ToString(Standard.DateFormat);

						dataAdapter = new SqlDataAdapter(dbCmd);
						dataAdapter.Fill(dataSet, "BillingSummary");

						string page = "";

						page = "BizDate|GroupCode|Account|CUSIP|Symbol|QuantityCovered|Rate|Charge\r\n";

						foreach (DataRow dr in dataSet.Tables["BillingSummary"].Rows)
						{
							if (groupCodeText.IndexOf(dr["GroupCode"].ToString().Trim() + ";") > -1)
							{
								if (long.Parse(dr["QuantityCovered"].ToString()) > -1)
								{
									page += DateTime.Parse(dr["BizDate"].ToString()).ToString("yyyy-MM-dd") + "|" +
										dr["GroupCode"].ToString() + "|" +
										dr["AccountNumber"].ToString() + "|" +
										dr["SecId"].ToString() + "|" +
										dr["Symbol"].ToString() + "|" +
										dr["QuantityCovered"].ToString() + "|" +
										((!dr["MarkupRate"].ToString().Equals("")) ? decimal.Parse(dr["MarkupRate"].ToString()).ToString("0.000") : "") + "|" +
										decimal.Parse(dr["ModifiedCharge"].ToString()).ToString("#,##0.00") + "\r\n";
								}
							}
						}

						sr = File.CreateText(@filePath + @"\HardToBorrowExtract_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
						sr.Write(page);
						sr.Close();
				
						email.Send("mbattaini@penson.com", "stockloan@penson.com", "Schonfeld Neg Rebate File Uplaoded " + DateTime.Now.ToString(Standard.DateFormat), "");
						KeyValue.Set("AresSchonfeldShortSaleNegRebateFileDate", DateTime.Now.ToString(Standard.DateFormat), dbCnStr);
					}   
					catch (Exception error)
					{ 
						Log.Write(error.Message, 2);
					}
				}			
			}
		}

		private void StockBorrowUploadFile()
		{
			DataSet dsBoxSummary = new DataSet();		
			DataSet dsReportRecipients = new DataSet();
				
			StreamWriter sr = null;
				
			string fileName = "";
			string fileContent = "";													

			string tempPath = KeyValue.Get("AresStockBorrowUploadFilePath", @"C:\", dbCnStr);

			if (!KeyValue.Get("AresStockBorrowUploadFileDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				if (Master.BizDate.Equals(Master.ContractsBizDate)  && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
				{
					if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresStockBorrowSetTime", "06:15", dbCnStr), "HH:mm", null))									
					{					
						SqlConnection dbCn = new SqlConnection(dbCnStr);
			
						SqlCommand dbCmd = new SqlCommand("dbo.spBoxSummaryGet", dbCn);
						dbCmd.CommandType = CommandType.StoredProcedure;

						SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
						paramBizDate.Value = Master.BizDate;

						SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
						dataAdapter.Fill(dsBoxSummary, "BoxSummary");
					
						foreach (DataRow dr in dsBoxSummary.Tables["BoxSummary"].Rows)
						{
							if (((dr["BookGroup"].ToString().Equals("0234")) &&
								(dr["BaseType"].ToString().Equals("E")) &&
								(long.Parse(dr["Available"].ToString()) * double.Parse(dr["LastPrice"].ToString())) >= long.Parse(KeyValue.Get("AresStockBorrowUploadAmountMin", "1000000", dbCnStr)) &&
								(double.Parse(dr["LastPrice"].ToString()) >= double.Parse(KeyValue.Get("AresStockBorrowUploadPriceMin", "5.00", dbCnStr))) &&
								(bool.Parse(dr["IsNoLend"].ToString()) == false) &&
								(bool.Parse(dr["IsThreshold"].ToString()) == false)))
							{
								fileContent += dr["SecId"].ToString().Trim() + "," + dr["Available"].ToString() + ",,,\r\n";
							}
						}
											
						fileName = tempPath + @"\D" + DateTime.Parse(Master.ContractsBizDate).ToString("MMddyy") + "0234.txt";
						sr = File.CreateText(fileName);
						sr.Write(fileContent);
						sr.Close();										

						Filer filer = new Filer(@"C:\temp.filer");
						filer.FileNoTempPut(@"/stkbw-penson-upload/581-1.txt", "ftp.americaneagle.com", "stkbw-penson-upload", @"d$R2sl1pp3rY", fileName);						

						email.Send("mbattaini@penson.com", "stockloan@penson.com", "Stock borrow file successfuly uploaded for " + Master.BizDate, "");
						KeyValue.Set("AresStockBorrowUploadFileDate", Master.ContractsBizDate, dbCnStr);
					}	
				}
			}
		}
					
		private DataSet ReportRecipientsGet(string reportName)
		{
			DataSet dsReportRecipients = new DataSet();
			
			try
			{
				SqlCommand dbCmd = new SqlCommand("spReportValuesGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
				paramReportName.Value = reportName;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsReportRecipients, "ReportRecipients");		
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
				throw;
			}
		
			return dsReportRecipients;
		}
		
		private void ReportRecipientSet(string reportName, string reportRecipientNumber, string dateTime)
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spReportValueSet", localDbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.Char, 50);
				paramReportName.Value = reportName;

				SqlParameter paramReportRecipientNumber = dbCmd.Parameters.Add("@ReportRecipientNumber", SqlDbType.BigInt);
				paramReportRecipientNumber.Value = reportRecipientNumber;

				SqlParameter paramLastDeliveredDate = dbCmd.Parameters.Add("@LastDeliveredDate", SqlDbType.DateTime);
				paramLastDeliveredDate.Value = dateTime;
				
				localDbCn.Open();
				dbCmd.ExecuteNonQuery();				
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}
		}

		private DataSet AvailabilityFeedDataGet(string bookGroup, string minPrice, string minAvail, string minRate)
		{
			DataSet dsAvailability = new DataSet();
			try
			{
				string sql = "select	b.secid,\r\n" +
					"l.secidlink As Symbol,\r\n" +
					"b.netpositionsettled As Avail,\r\n" +
					"IsNull(IsNull((select avg(rate) from dbo.tbcontracts where bizdate = b.bizdate and bookgroup = b.bookgroup and contracttype = 'L' and secid = b.secid ), (select rate from tbinventoryratecontrol where bizdate = b.bizdate and secid = b.secid and rate < 5)), 0) As Rate\r\n" +
					"from		tbboxposition b,\r\n" +
					"			tbsecmaster s,\r\n" +
					"			tbsecidlinks l\r\n" +
					"where	b.bizdate = '" + Master.BizDate + "'\r\n" +
					"and		b.bookgroup = '" + bookGroup + "'\r\n" +
					"and		b.netpositionsettled <= b.netpositiontraded\r\n" +
					"and		b.netpositionsettled >= " + minAvail + "\r\n" +
					"and		s.secid = b.secid\r\n" +
					"and		s.lastprice >= " + minPrice +"\r\n" +
					"and		l.secid = s.secid\r\n" +
					"and		l.secidtypeindex = 2\r\n" +
					"and		s.basetype <> 'B'\r\n" +
					"Order By l.SecidLink";

				SqlCommand dbCmd = new SqlCommand(sql, new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.Text;
				
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsAvailability, "Avail");			
			
				foreach (DataRow dr in dsAvailability.Tables["Avail"].Rows)
				{
					if (decimal.Parse(dr["Rate"].ToString()) <= decimal.Parse(minRate))
					{
						dr.Delete();
					}
				}

				dsAvailability.AcceptChanges();
			}
			catch {}

			return dsAvailability;
		}

		
		private void AvailabilityFeed()
		{				
			if (Master.ContractsBizDate.Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresAvailFeedSetTime", "06:15", dbCnStr), "HH:mm", null))									
				{	
					try
					{
						DataSet dsReportRecipients = null;
						DataSet dsAvailability = null;
						StreamWriter sr = null;

						dsReportRecipients = ReportRecipientsGet("AVAIL_Feed");
			
						dsAvailability = AvailabilityFeedDataGet(
							KeyValue.Get("AresAvailBookGroup", "0234", dbCnStr),
							KeyValue.Get("AresAvailMinPrice", "3.00", dbCnStr),
							KeyValue.Get("AresAvailMinAvail", "5000", dbCnStr),
							KeyValue.Get("AresAvailMinRate", "-5", dbCnStr));
			
						string format = "";	
						string body = "";
        
						Email mail = new Email(dbCnStr);
						Filer filer = new Filer();

						string tempPath = KeyValue.Get("AresAvailFeedFilePath", @"C:\", dbCnStr);
						string fileName = "";								

						// Loop through the list of Recipients, check if each recipient has received their file for today 
						foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
						{				
							if (dr["LastDeliveredDate"].ToString().Equals(""))
							{
								dr["LastDeliveredDate"] = Master.BizDatePrior;
							}

							if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
							{							
								format = "";
								body = "";

								// Create the report file for this recipient's specific file format attribute in tbReportValues 
								foreach(DataRow dr2 in dsAvailability.Tables["Avail"].Rows)
								{
									format = dr["format"].ToString();
					
									if (dr["Justify"].ToString().Trim().ToUpper().Equals("Y"))
									{
										format = format.Replace("%secid", dr2["SecId"].ToString().Trim());
										format = format.Replace("%symbol", dr2["Symbol"].ToString().Trim().PadRight(6, ' '));
										format = format.Replace("%quantity", dr2["Avail"].ToString().PadLeft(15, ' '));
										format = format.Replace("%rate", decimal.Parse(dr2["Rate"].ToString()).ToString("00.000").PadLeft(8, ' '));
									}
									else
									{
										format = format.Replace("%secid", dr2["SecId"].ToString().Trim());
										format = format.Replace("%symbol", dr2["Symbol"].ToString().Trim());
										format = format.Replace("%quantity", dr2["Avail"].ToString());
										format = format.Replace("%rate", dr2["Rate"].ToString());
									}
					
									Log.Write(format, 1);
									body += format + "\r\n";

								} // foreach dr2 Loop 
	
								fileName = tempPath + @"\AVAIL_Feed_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyyMMdd") + "_" + dr["ReportRecipientNumber"].ToString() +".csv";
								
								sr = File.CreateText(fileName);
								sr.Write(body);
								sr.Close();

								if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
								{
									mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "AVAIL_ " + Master.ContractsBizDate, body, fileName);
								}
								else
								{																			
									mail.Send("mbattaini@penson.com", "sendero@penson.com", "AVAIL Feed", "AVAIL Feed uploaded to " + @dr["ReportRecipient"].ToString(), fileName);						
									filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\AVAIL_Feed_" + DateTime.Now.ToString("yyyyMMdd") + ".csv", "localHost", "", "", fileName);
								}
				
								ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.BizDate);
							}
						}	
					}				
					catch (Exception error)
					{
						Log.Write(error.Message, 1);
					}
				}
			}
		}
 

		private void SegEntriesComparison()
		{		
			DataSet dsMemoSegEntries = new DataSet();
			DataSet dsSegEntries = new DataSet();
			
			long memosegquantity = 0;
			long segquantity = 0;
			if (!KeyValue.Get("AresSegEntriesComparisonMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				SqlConnection localDbCn = new SqlConnection(dbCnStr);
	
				SqlCommand dbCmd = new SqlCommand("spMemoSegEntriesGet", localDbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDate;
				
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsMemoSegEntries, "MemoSegEntries");

				dbCmd.CommandText = "spSegEntriesGet";				
				dataAdapter.Fill(dsSegEntries, "SegEntries");
			
				foreach(DataRow dr in dsMemoSegEntries.Tables["MemoSegEntries"].Rows)
				{
					memosegquantity += long.Parse(dr["Quantity"].ToString());
				}
			
				foreach(DataRow dr in dsSegEntries.Tables["SegEntries"].Rows)
				{
					if (dr["Indicator"].ToString().Trim().Equals("D"))
					{
						segquantity += (long.Parse(dr["Quantity"].ToString()) * -1);
					}
					else
					{
						segquantity += long.Parse(dr["Quantity"].ToString());
					}
				}
		
				email.Send(KeyValue.Get("AresSegEntriesComparisonMailTo", "mbattaini@penson.com",dbCnStr), "sendero@penson.com", "Seg Entries", "Memo Seg Entries: " + memosegquantity.ToString("#,##0") + "\nSeg Entries: " + segquantity.ToString("#,##0"));
				
				KeyValue.Set("AresSegEntriesComparisonMailDate", Master.BizDate, dbCnStr);
			}			
		}

		private void SmartSegSummary()
		{
			string body = "";

			DataSet dsMemoSegEntries = new DataSet();
			DataSet dsSegEntries = new DataSet();
			
			long memosegquantity = 0;
			long segquantity = 0;

			if ((KeyValue.Get("S3EODMemoSegEntriesDate", "", dbCnStr).Equals(Master.ContractsBizDate)) && (!KeyValue.Get("AresEODSegEntriesComparisonMailDate", "", dbCnStr).Equals(Master.ContractsBizDate)))
			{
				if (File.GetLastAccessTime(@"\\ftpen003\shrNDMReportTransfer\PFSSSX.txt") > DateTime.Parse("14:15"))
				{
					SqlConnection localDbCn = new SqlConnection(dbCnStr);
	
					SqlCommand dbCmd = new SqlCommand("spMemoSegEntriesGet", localDbCn);
					dbCmd.CommandType = CommandType.StoredProcedure;

					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = Master.BizDate;
				
					SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
					dataAdapter.Fill(dsMemoSegEntries, "MemoSegEntries");

					dbCmd.CommandText = "spSegEntriesGet";				
					dataAdapter.Fill(dsSegEntries, "SegEntries");
			
					foreach(DataRow dr in dsMemoSegEntries.Tables["MemoSegEntries"].Rows)
					{
						memosegquantity += long.Parse(dr["Quantity"].ToString());
					}
			
					foreach(DataRow dr in dsSegEntries.Tables["SegEntries"].Rows)
					{
						if (dr["Indicator"].ToString().Trim().Equals("D"))
						{
							segquantity += (long.Parse(dr["Quantity"].ToString()) * -1);
						}
						else
						{
							segquantity += long.Parse(dr["Quantity"].ToString());
						}
					}
	
					body = "Intra-day And End Of Seg Breaks\nAcctNumber  Type     CUSIP           Qty   Ind\n";
			
					foreach(DataRow dr in dsSegEntries.Tables["SegEntries"].Rows)
					{
						if ((bool.Parse(dr["IsFailed"].ToString()) == true) || (bool.Parse(dr["IsRequested"].ToString()) == true && bool.Parse(dr["IsProcessed"].ToString()) == false))
						{
							body+= dr["AccountNumber"].ToString().PadRight(12, ' ') +
								dr["AccountType"].ToString().PadRight(9,  ' ') +
								dr["SecId"].ToString().PadRight(16, ' ') +
								dr["Quantity"].ToString().PadRight(6, ' ') +
								dr["Indicator"].ToString().PadRight(4, ' ') + "\n";
						}
					}

					email.Send(KeyValue.Get("AresSegEntriesComparisonMailTo", "mbattaini@penson.com",dbCnStr), "sendero@penson.com", "Seg Entries End Of Day Summary", "Memo Seg Entries: " + memosegquantity.ToString("#,##0") + "\nSeg Entries: " + segquantity.ToString("#,##0") +"\n\n" + body, @"\\ftpen003\shrNDMReportTransfer\PFSSSX.txt");
				
					KeyValue.Set("AresEODSegEntriesComparisonMailDate", Master.BizDate, dbCnStr);
				}
			}
		}

		private void HardToBorrowRatesFile()
		{
			DataSet dsHardToBorrow = new DataSet();		
			DataSet dsReportRecipients = new DataSet();
				
			StreamWriter sr = null;
				
			string fileName = "";
			string fileContent = "";				
					
			int index = 0;

			string tempPath = KeyValue.Get("AresHardToBorrowRatesFilePath", @"C:\", dbCnStr);

			if (!KeyValue.Get("AresHardToBorrowRatesMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				if (KeyValue.Get("AztecFileLoadDate", "", dbCnStr).Equals(Master.ContractsBizDate))
				{
					SqlConnection dbCn = new SqlConnection(dbCnStr);
			
					SqlCommand dbCmd = new SqlCommand("dbo.spBorrowHardListRatesGet", dbCn);
					dbCmd.CommandType = CommandType.StoredProcedure;

					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = Master.ContractsBizDate;
					
					SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
					dataAdapter.Fill(dsHardToBorrow, "HardToBorrow");
					
					foreach (DataRow dr in dsHardToBorrow.Tables["HardToBorrow"].Rows)
					{
						fileContent += dr["SecId"].ToString() + "," + dr["Symbol"].ToString() + "," + decimal.Parse(dr["Rate"].ToString()).ToString("0.000") + "\r\n";
					}						
						
					fileName = tempPath + @"\HardToBorrowRates_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyyMMdd") + ".csv";
					sr = File.CreateText(fileName);
					sr.Write(fileContent);
					sr.Close();
						
					dbCmd.Parameters.Remove(paramBizDate);

					dsReportRecipients = ReportRecipientsGet("HTB_Rates");			// Using Matt's new ReportRecipient dataset function 
	
					Email mail = new Email(dbCnStr);
					Filer filer = new Filer();

					foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows) // HTB_Rates is for files only
					{
						try
						{
							if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
							{
								mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "Hard To Borrow File", "", fileName);						
							}
							else
							{						
								mail.Send("mbattaini@penson.com", "sendero@penson.com", "Hard To Borrow File Update", "HTB Rates uploaded to " + @dr["ReportRecipient"].ToString(), fileName);						
								filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\HTB_Rates_" + DateTime.Now.ToString("yyyyMMdd") + ".csv", "localHost", "", "", fileName);
							}

							index ++;
						}
						catch {}
					}											

					KeyValue.Set("AresHardToBorrowRatesMailDate", Master.ContractsBizDate, dbCnStr);
				}				
			}
		}
				
		private void EasyBorrowRatesMail()
		{
			DataSet dsEasyBorrow = new DataSet();		
			DataSet dsReportRecipients = new DataSet();
				
			StreamWriter sr = null;
				
			string fileName = "";
			string fileContent = "";				
					
			int index = 0;

			string tempPath = KeyValue.Get("AresEasyBorrowRatesFilePath", @"C:\", dbCnStr);

			if (!KeyValue.Get("AresEasyBorrowRatesMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				if (KeyValue.Get("EasyBorrowFileDate", "", dbCnStr).Equals(Master.ContractsBizDate))
				{
					SqlConnection dbCn = new SqlConnection(dbCnStr);
			
					SqlCommand dbCmd = new SqlCommand("dbo.spBorrowEasyListRatesGet", dbCn);
					dbCmd.CommandType = CommandType.StoredProcedure;

					SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
					dataAdapter.Fill(dsEasyBorrow, "EasyBorrow");
					
					foreach (DataRow dr in dsEasyBorrow.Tables["EasyBorrow"].Rows)
					{
						fileContent += dr["CUSIP"].ToString() + "," + dr["Symbol"].ToString() + "," + decimal.Parse(dr["LowestRate"].ToString()).ToString("0.000") + "\r\n";
					}
												
					fileName = tempPath + @"\EasyBorrowRates_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyyMMdd") + ".csv";
					sr = File.CreateText(fileName);
					sr.Write(fileContent);
					sr.Close();
						
					dsReportRecipients = ReportRecipientsGet("ETB_Rates");				// Using Matt's new ReportRecipient dataset function 
						
					Email mail = new Email(dbCnStr);
					Filer filer = new Filer();

					foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
					{
						if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
						{
							mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "Easy Borrow Rates For " + Master.ContractsBizDate, fileContent, fileName);
						}
						else
						{																			
							mail.Send("mbattaini@penson.com", "sendero@penson.com", "Easy To Borrow File Update", "ETB Rates uploaded to " + @dr["ReportRecipient"].ToString(), fileName);						
							filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\ETB_Rates_" + DateTime.Now.ToString("yyyyMMdd") + ".csv", "localHost", "", "", fileName);
						}						
						
						index ++;
					}											

					KeyValue.Set("AresEasyBorrowRatesMailDate", Master.ContractsBizDate, dbCnStr);
				}				
			}
		}

		private void CNSFailsMail()
		{		//Continuous-Net-Settlement CNS Fails file daily 11:05 (6:05 AM), data from BizDatePrior
	
			SqlConnection wwDbCn = null;
			SqlDataAdapter wwDataAdapter = null;
			DataSet dsCNSFails = new DataSet();		
			DataSet dsReportRecipients = new DataSet();
			
			StreamWriter sr = null;
			
			string sql = "";
			string tempPath = ""; 
			string fileName = "";
			string lineFormat = "";
			string fileContent = "";		
			string fileMakeTime = null; 
			string worldwideProcessDateStr = "";
			string ShortSellFinalProcessDateStr = "";
				
			int rptCount = 0;
			int rowCount = 0; 

			// Aztec file must already be loaded for ContractsBizDate 
			if (!KeyValue.Get("AztecFileLoadDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				return;
			}

			// Skip If already done for current ContractsBizDate 
			if (KeyValue.Get("AresCNSFailsMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				return; 
			}

			// Check if CNS_Fails MakeTime is ready for today 
			fileMakeTime = KeyValue.Get("AresCNSFailsMakeTime", "11:05", dbCnStr);
			if (fileMakeTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) > 0 )
			{
				KeyValue.Set("AresCNSFailsMailStatus", "WAIT -- " + Master.ContractsBizDate, dbCnStr);
				return;
			}				

			try
			{
				// Verify Prerequisite: WorldWide ProcessDates must equal to Sendero BizDatePrior

				wwDbCn = new SqlConnection(worldwideDbCnStr);
				sql = "Use WorldWide \r\n" +
					"Declare @BizDatePrior datetime \r\n" +
					"Select  Top 1 CPD.WorldWideProcessDate, SSF.ProcessDate As ShortSellFinalProcessDate \r\n" +
					"From    dbo.CurrentProcessDate CPD With (NOLOCK) \r\n" +
					"Inner   Join dbo.SecShortSellFinal  SSF With (NOLOCK) \r\n" +
					"On      CPD.ProcessDate = SSF.ProcessDate \r\n" +
					"Where   CPD.Firm = '07' \r\n" +
					"And     CPD.ProcessDate = CPD.WorldWideProcessDate \r\n" +
					"And     CPD.ProcessDate = IsNull(@BizDatePrior, CPD.ProcessDate) \r\n" ;

				SqlCommand wwDbCmd = new SqlCommand(sql, wwDbCn);
				wwDbCmd.CommandType = CommandType.Text;

				wwDataAdapter = new SqlDataAdapter(wwDbCmd);
				wwDataAdapter.Fill(dsCNSFails, "ProcessDates");

				if ( dsCNSFails.Tables["ProcessDates"].Rows.Count == 0)
				{
					KeyValue.Set("AresCNSFailsMailStatus", "ERROR (" + DateTime.Now.ToString("yyyy-MM-dd") + "): No ProcessDate data returned from WorldWide.CurrentProcessDate table.", dbCnStr);
					Log.Write("ERROR: No ProcessDate data returned from [WorldWide].[CurrentProcessDate] table.  [AresMain.CNSFailsMail]", 2);
					return;
				}

				foreach (DataRow dr in dsCNSFails.Tables["ProcessDates"].Rows)
				{
					worldwideProcessDateStr = DateTime.Parse(dr["WorldWideProcessDate"].ToString()).ToString("yyyy-MM-dd");   
					ShortSellFinalProcessDateStr = DateTime.Parse(dr["ShortSellFinalProcessDate"].ToString()).ToString("yyyy-MM-dd"); 
				}	

				if ((worldwideProcessDateStr == ShortSellFinalProcessDateStr) && 
					(ShortSellFinalProcessDateStr == DateTime.Parse( Master.BizDatePrior).ToString("yyyy-MM-dd")) )
				{		
					
					//------ Prerequisite Dates are good, now get the report dataset ------------------ 
					sql = "Use WorldWide \r\n" +
						"Select S.Cusip, \r\n" +
						"       B.Symbol, \r\n" +
						"       Sum(S.OriginalSECShortSellQuantity) As SUM_OriginalSECShortSellQuantity \r\n" +
						"From  	dbo.SecShortSellFinal S With (Nolock) \r\n" +
						"Inner  Join dbo.SecurityBase B With (Nolock) \r\n" +
						"On     S.Firm = B.Firm \r\n" +
						"And    S.Cusip = B.Cusip  \r\n" +
						"Where	S.Firm = '07' \r\n" +
						"And    S.ProcessDate = '" + Master.BizDatePrior + "' \r\n" +
						"Group By S.Cusip, B.Symbol \r\n" +
						"Order By B.Symbol \r\n" ;

					wwDbCmd.CommandText = sql;
					wwDataAdapter.Fill(dsCNSFails,"CNSFails");


					//------ Get the Recipients list ------------------ 
					dsReportRecipients = ReportRecipientsGet("CNS_Fails");							// Using Matt's new Dataset function 


					//------ Initialize report file and email object 
					tempPath = KeyValue.Get("AresCNSFailsFilePath", @"C:\", dbCnStr);   // C:\Sendero\DailyReports_Archive\
					Email mail = new Email(dbCnStr);
					Filer filer = new Filer();

					//------ Loop through the list of recipients, cehck if each recipients has received file for today
					foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows) 
					{
						try
						{
							if (dr["LastDeliveredDate"].ToString().Equals(""))
							{
								dr["LastDeliveredDate"] = Master.BizDatePrior;
							}

							if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
							{
								lineFormat = ""; 
								fileContent = ""; 

								//------ Create the CNS_Fails report filecontent for this recipient's specific file format (attribute in tbReportValues)

								// fileContent = "Write Column Header Name needed for the file" + "\r\n";

								foreach (DataRow drFails in dsCNSFails.Tables["CNSFails"].Rows)
								{
									lineFormat = dr["Format"].ToString(); 

									if (dr["Justify"].ToString().Trim().ToUpper().Equals("Y"))
									{
										lineFormat = lineFormat.Replace("%secid", drFails["Cusip"].ToString().Trim());
										lineFormat = lineFormat.Replace("%symbol", drFails["Symbol"].ToString().Trim().PadRight(6, ' '));
										lineFormat = lineFormat.Replace("%quantity", drFails["SUM_OriginalSECShortSellQuantity"].ToString().PadLeft(15, ' '));
									}
									else
									{
										lineFormat = drFails["Cusip"].ToString().Trim() + "," +  drFails["Symbol"].ToString().Trim() + "," + decimal.Parse(drFails["SUM_OriginalSECShortSellQuantity"].ToString()).ToString("0");
									}

									fileContent +=  lineFormat + "\r\n";
									rowCount++; 
								}	// foreach drFails Loop 

								// Create the file 
								fileName = tempPath + @"\CNS_Fails_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyyMMdd") + ".csv";		
								sr = File.CreateText(fileName);
								sr.Write(fileContent);
								sr.Close();

								//------ Send the email with CNS Fails file attachment ------------------------------- 
								if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
								{
									mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "CNS Fails " + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd"), "For ProcessDate = " + Master.BizDatePrior, fileName); 
								}
								else
								{	// Not an Email address, upload to a ftp site 					
									mail.Send("mbattaini@penson.com;dchen@penson.com", "sendero@penson.com", "CNS_Fails File Update", "CNS_Fails File Uploaded to " + @dr["ReportRecipient"].ToString(), fileName);
									filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\CNS_Fails_" + DateTime.Now.ToString("yyyyMMdd") + ".csv", "localHost", "", "", fileName);
								} 
												
								// Report mailed/uploaded, now Update tbReportValues for this report/recipient to new date. 
								ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.BizDate);
								rptCount ++;
							}

						}
						catch (Exception error)
						{  
							KeyValue.Set("AresCNSFailsMailStatus", "ERROR (" + DateTime.Now.ToString("yyyy-MM-dd") + "): " + error.Message, dbCnStr);
							Log.Write("ERROR: " + error.Message + ".  [AresMain.CNSFailsMail]", Log.Error, 1); 
							;
						}

					}	 // foreach dsReportRecipients dr Loop 										

					//------ Successfully completed CNS_Fails File Creatiing and Mailing 
					KeyValue.Set("AresCNSFailsMailDate", Master.ContractsBizDate, dbCnStr);
					KeyValue.Set("AresCNSFailsMailStatus", "OK", dbCnStr);
					Log.Write("Daily CNS_Fails file mailed, for BizDate: " + Master.BizDate + ". Data RowCount = " + rowCount.ToString() + ", Recipient Count = " + rptCount.ToString() + ". [AresMain.CNSFailsMail]", 2);

				} 
				else 							
				{	
					//WorldWide ProcessDates Does NOT Match Sendero BizDatePrior, this is Error condition, Send warning mail, Write to Log, and Exit Sub 
					KeyValue.Set("AresCNSFailsMailStatus", "ERROR: CNS Fails PrecessDates does Not match: BizDatePrior(" + Master.BizDatePrior + "), WorldWide ProcessDate(" + worldwideProcessDateStr + "), ShortSellFinal ProcessDate(" + ShortSellFinalProcessDateStr + ").  [AresMain.CNSFailsMail]", dbCnStr);
					Log.Write("ERROR: CNS Fails PrecessDates does Not match: BizDatePrior(" + Master.BizDatePrior + "), WorldWide ProcessDate(" + worldwideProcessDateStr + "), ShortSellFinal ProcessDate(" + ShortSellFinalProcessDateStr +").  [AresMain.CNSFailsMail]", 2);
					return; 
				}

			}
			catch (Exception error)
			{
				KeyValue.Set("AresCNSFailsMailStatus", "ERROR (" + DateTime.Now.ToString(Standard.DateFormat) + "): " + error.Message, dbCnStr);
				Log.Write("ERROR: " + error.Message + ".  [AresMain.CNSFailsMail]", Log.Error, 1);
				return;
			}
			finally
			{
				if (wwDbCn.State != ConnectionState.Closed)
				{
					wwDbCn.Close();
				}
			}

		} 


		private void HTBAccountsMail()
		{		// Hard-To-Borrow Securities listed by Accounts, Report send Daily 11:10 (6:10 AM), data from BizDatePrior 

			DataSet dsHTBAccounts = new DataSet();		
			DataSet dsReportRecipients = new DataSet();
				
			StreamWriter sr = null;
				
			string tempPath = ""; 
			string fileName = "";
			string lineFormat = ""; 
			string fileContent = "";		
			string fileMakeTime = null; 
					
			int rptCount = 0;
			int rowCount = 0;

			// Aztec file must already be loaded for COntractBizDate 
			if (!KeyValue.Get("AztecFileLoadDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				return;
			}

			// Skip if already done for current COntractsBizDate 
			if (KeyValue.Get("AresHTBAccountsMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				return;
			}

			// Check if HTB Accounts data is ready to be send out for today 
			fileMakeTime = KeyValue.Get("AresHTBAccountsMakeTime", "11:10", dbCnStr);
			if (fileMakeTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) > 0 )
			{
				KeyValue.Set("AresHTBAccountsMailStatus", "WAIT -- " + Master.ContractsBizDate, dbCnStr);
				return;
			}


			try 
			{
				//------ Get the list of report recipients 
				dsReportRecipients = ReportRecipientsGet("HTB_Accounts");		// Using Matt's new Dataset function 

				//------ Get the report dataset 
				SqlConnection dbCn = new SqlConnection(dbCnStr);
								
				SqlCommand dbCmd = new SqlCommand("dbo.spShortSaleBillingSummaryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
				paramStartDate.Value = Master.BizDatePrior;
										
				SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
				paramStopDate.Value = Master.BizDatePrior;
										
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsHTBAccounts, "HardToBorrowAccounts");
				
				//------ Initialize report file and email object 
				tempPath = KeyValue.Get("AresHTBAccountsFilePath", @"C:\", dbCnStr);			// C:\Sendero\DailyReports_Archive\
				Email mail = new Email(dbCnStr);
				Filer filer = new Filer();

				//------ Loop through the list of recipients, check if each recioient has received file for today
				foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)  // HTB_Accounts is for files only
				{
					try
					{
						if (dr["LastDeliveredDate"].ToString().Equals(""))
						{
							dr["LastDeliveredDate"] = Master.BizDatePrior;
						}

						if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
						{
							//------ Create the HTB_Accounts report filecontent for this recipient's specific file format (attribute in tbReportValues) 
							lineFormat = ""; 
							fileContent = ""; 
							fileContent = "GroupCode,Account,Secid,Symbol,Shorted,Covered" + "\r\n";   // File requires header columns 

							foreach (DataRow drHTB in dsHTBAccounts.Tables["HardToBorrowAccounts"].Rows)
							{
								lineFormat = dr["Format"].ToString();

								if (dr["Justify"].ToString().Trim().ToUpper().Equals("Y"))
								{
									lineFormat = lineFormat.Replace("%GroupCode", drHTB["GroupCode"].ToString().Trim().PadRight(6, ' '));
									lineFormat = lineFormat.Replace("%Account", drHTB["AccountNumber"].ToString().Trim().PadRight(10, ' '));
									lineFormat = lineFormat.Replace("%SecId", drHTB["SecId"].ToString().Trim().PadRight(12, ' '));
									lineFormat = lineFormat.Replace("%Symbol", drHTB["Symbol"].ToString().Trim().PadRight(6, ' '));
									lineFormat = lineFormat.Replace("%Shorted", drHTB["QuantityShorted"].ToString().Trim().PadLeft(15, ' '));
									lineFormat = lineFormat.Replace("%Covered", drHTB["QuantityCovered"].ToString().Trim().PadLeft(15, ' '));
								}
								else
								{
									lineFormat = drHTB["GroupCode"].ToString().Trim() + "," +  drHTB["AccountNumber"].ToString() + "," +  drHTB["SecId"].ToString() + "," + drHTB["Symbol"].ToString() + "," + decimal.Parse(drHTB["QuantityShorted"].ToString()).ToString("0") + "," + decimal.Parse(drHTB["QuantityCovered"].ToString()).ToString("0"); 
								}

								fileContent += lineFormat + "\r\n";
								rowCount++; 
							}	// foreach drHTB Loop 
						
							//------ Create the file 
							fileName = tempPath + @"\HTB_ACCOUNTS_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyyMMdd") + ".csv";  
							sr = File.CreateText(fileName);
							sr.Write(fileContent);
							sr.Close();

							//------ Send the email with HTB_Accounts file 
							if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
							{
								mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "HTB Accounts " + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd"), "For ProcessDate = " + Master.BizDatePrior, fileName); 
							}
							else
							{	// Not an Email address, upload to a ftp site 					
								mail.Send("mbattaini@penson.com;dchen@penson.com", "sendero@penson.com", "HTB Accounts File Update", "HTB Accounts Uploaded to " + @dr["ReportRecipient"].ToString(), fileName);
								filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\HTB_ACCOUNTS_" + DateTime.Now.ToString("yyyyMMdd") + ".csv", "localHost", "", "", fileName);
							} 

							//------ Report mailed/uploaded, now Update tbReportValues for this report/recipient to new date. 
							ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.BizDate);
							rptCount ++;
						}
					}
					catch (Exception error)
					{	
						KeyValue.Set("AresHTBAccountsMailStatus", "ERROR (" + DateTime.Now.ToString("yyyy-MM-dd") + "): " + error.Message.Substring(0,220), dbCnStr); 
						Log.Write("ERROR: " + error.Message + ".  [AresMain.HTBAccountsMail]", Log.Error, 1); 
					}

				}	// foreach dsReportRecipients dr Loop 									

				//------ Successfully completed HTB_Accounts file creating and mailing 
				KeyValue.Set("AresHTBAccountsMailDate", Master.ContractsBizDate, dbCnStr);
				KeyValue.Set("AresHTBAccountsMailStatus", "OK", dbCnStr);
				Log.Write("Daily HTB_Accounts file mailed, for BizDate: " + Master.BizDate + ". Data RowCount = " + rowCount.ToString() + ", Recipient Count = " + rptCount.ToString() + ".  [AresMain.HTBAccountsMail]", 2);

			}
			catch (Exception error)	
			{
				KeyValue.Set("AresHTBAccountsMailStatus", "ERROR (" + DateTime.Now.ToString("yyyy-MM-dd") + "): " + error.Message, dbCnStr);
				Log.Write("ERROR: " + error.Message + ".  [AresMain.HTBAccountsMail]", Log.Error, 1);
				return;
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
  
}
		

		private void ShortsVsFtdMail()
		{	//Daily 20:00 (Local 3:00 PM), Must wait for DTC Drop Hit  

			// Local variables declaration 
			DataSet dsShortsVsFTD = new DataSet();		
			DataSet dsReportRecipients = new DataSet();
				
			StreamWriter sr = null;
				
			string tempPath = ""; 
			string fileName = "";
			string lineFormat = ""; 
			string fileContent = "";		
			string fileMakeTime = null; 

			int rptCount = 0;
			int rowCount = 0;

			// Skip if already done for current ContractsBizDate 
			if (KeyValue.Get("AresShortsVsFTDMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				return;
			}

			// Check if MakeTime is ready for ShortsVsFTD data to be send out for today 
			fileMakeTime = KeyValue.Get("AresShortsVsFtdMakeTime", "20:00", dbCnStr);
			if (fileMakeTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) > 0 )
			{
				KeyValue.Set("AresShortsVsFTDMailStatus", "WAIT -- " + Master.ContractsBizDate, dbCnStr);
				return;
			}

			try
			{ 
				//------ Initialize report file and email object 
				tempPath = KeyValue.Get("AresShortsVsFtdFilePath", @"C:\", dbCnStr);			// C:\Sendero\DailyReports_Archive\
				Email mail = new Email(dbCnStr);
				Filer filer = new Filer();


				//------ PART ONE ------ ShortsVsFTD EXCEPTIONS File --------------------------------------------

				//------ Get the list of report recipients for ShortsVsFTD_EXCEPTIONS list 
				dsReportRecipients = ReportRecipientsGet("ShortsVsFTD_Exceptions");				// Using Matt's new Dataset function 

				//------ Get the Exceptions List Dataset 
				SqlConnection dbCn = new SqlConnection(dbCnStr);
								
				SqlCommand dbCmd = new SqlCommand("dbo.spShortsVsFtdGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDate;
										
				SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
				paramBizDatePrior.Value = Master.BizDatePrior;
										
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsShortsVsFTD, "Exceptions");
        
				//rowCount = dsShortsVsFTD.Tables["Exceptions"].Rows.Count;   

				//Outer Loop of ReportRecipients for EXCEPTIONS file 
				foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows) 
				{
					try
					{
						if (dr["LastDeliveredDate"].ToString().Equals(""))
						{
							dr["LastDeliveredDate"] = Master.BizDatePrior;
						}

						if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
						{
							//------ Create the ShortsVsFTD EXCEPTIONS report filecontent for this recipient's specific file format (attribute in tbReportValues) ------------------ 
							rptCount = 0; 
							rowCount = 0; 
							lineFormat = ""; 
							fileContent = "CUSIP,SYMBOL,PRICE,CNS FTD,EX/DEF,SHORTS,NET BORROW,BUY IN,BUY IN AMT ($),PLEDGE RELEASE" + "\r\n";	// File has header columns 

							foreach (DataRow drExceptions in dsShortsVsFTD.Tables["Exceptions"].Rows)
							{
								lineFormat = dr["Format"].ToString();

								if (dr["Justify"].ToString().Trim().ToUpper().Equals("Y"))
								{
									lineFormat = lineFormat.Replace("%CUSIP", drExceptions["CUSIP"].ToString().Trim().PadRight(12, ' '));
									lineFormat = lineFormat.Replace("%Symbol", drExceptions["Symbol"].ToString().Trim().PadRight(6, ' '));
									lineFormat = lineFormat.Replace("%Price", drExceptions["Price"].ToString().Trim().PadLeft(18, ' '));
									lineFormat = lineFormat.Replace("%CNS_FTD", drExceptions["CNS_FTD"].ToString().Trim().PadLeft(15, ' '));
									lineFormat = lineFormat.Replace("%EX_DEF", drExceptions["EX_DEF"].ToString().Trim().PadLeft(15, ' '));
									lineFormat = lineFormat.Replace("%Shorts", drExceptions["Shorts"].ToString().Trim().PadLeft(15, ' '));
									lineFormat = lineFormat.Replace("%NetBorrow", drExceptions["NetBorrow"].ToString().Trim().PadLeft(15, ' '));
									lineFormat = lineFormat.Replace("%BuyIn", drExceptions["BuyIn"].ToString().Trim().PadLeft(15, ' '));
									lineFormat = lineFormat.Replace("%BuyInAmount", drExceptions["BuyInAmount"].ToString().Trim().PadLeft(18, ' '));
									lineFormat = lineFormat.Replace("%PledgeRelease", drExceptions["PledgeRelease"].ToString().Trim().PadLeft(15, ' '));
								}
								else
								{
									lineFormat = 
										drExceptions["CUSIP"].ToString().Trim() + "," +  
										drExceptions["Symbol"].ToString().Trim() + "," +  
										decimal.Parse(drExceptions["Price"].ToString()).ToString("0.####") + "," + 
										decimal.Parse(drExceptions["CNS_FTD"].ToString()).ToString("0") + "," + 
										decimal.Parse(drExceptions["EX_DEF"].ToString()).ToString("0") + "," + 
										decimal.Parse(drExceptions["Shorts"].ToString()).ToString("0") + "," + 
										decimal.Parse(drExceptions["NetBorrow"].ToString()).ToString("0") + "," + 
										decimal.Parse(drExceptions["BuyIn"].ToString()).ToString("0") + "," + 
										decimal.Parse(drExceptions["BuyInAmount"].ToString()).ToString("0.####") + "," + 
										decimal.Parse(drExceptions["PledgeRelease"].ToString()).ToString("0") ;
								}

								fileContent += lineFormat + "\r\n";
								rowCount++; 

							}	// foreach inner drExceptions File Loop 
						
							//------ Create the file 
							fileName = tempPath + @"\Shorts-Vs-FTD-" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd") + ".csv";  
							sr = File.CreateText(fileName);
							sr.Write(fileContent);
							sr.Close();

							//------ Send the email with HTB_Accounts file 
							if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
							{
								mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "Shorts Vs FTD " + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd"), "", fileName); 
							}
							else
							{	// Not an Email address, upload to a ftp site 					
								mail.Send("mbattaini@penson.com;dchen@penson.com", "sendero@penson.com", "Short-Vs-FTD Exceptions File Update", "Short-Vs-FTD-Exceptions File Uploaded to " + @dr["ReportRecipient"].ToString(), fileName);
								filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\Shorts-Vs-FTD-" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd") + ".csv", "localHost", "", "", fileName);
							} 

							//------ Report has been mailed/uploaded, now Update tbReportValues for this report/recipient to new date. 
							ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.BizDate);
							rptCount ++;
						}

					}
					catch (Exception error)
					{	
						KeyValue.Set("AresShortsVsFTDMailStatus", "ShortsVsFTD EXCEPTIONS File ERROR (" + DateTime.Now.ToString("yyyy-MM-dd") + "): " + error.Message.Substring(0,180), dbCnStr);
						Log.Write("ERROR: " + error.Message + ".  [AresMain.ShortsVsFTDMail]", Log.Error, 1); 
					}

				}	// foreach Outer Loop:  dsReportRecipients for EXCEPTIONS report 


				//------ PART TWO ------ ShortsVsFTD Fails-To-BUY-IN File --------------------------------------------

				//------ Get the list of report recipients for ShortsVsFTD FAILS-To-BUY-IN list 
				dsReportRecipients = ReportRecipientsGet("ShortsVsFTD_FailsToBuyIn");				// Using Matt's new ReportRecipient dataset function 

				//------ Get the Fails-to-Buy-Ins List Dataset 
				SqlParameter paramFailsBuyInList = dbCmd.Parameters.Add("@FailsBuyInList", SqlDbType.Bit);
				paramFailsBuyInList.Value = 1;
										
				dataAdapter.Fill(dsShortsVsFTD, "FailsBuyIn");
				//rowCount = dsShortsVsFTD.Tables["FailsBuyIn"].Rows.Count;   

				//Outer Loop of ReportRecipients for BUY-INS file 
				foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows) 
				{
					try
					{
						if (dr["LastDeliveredDate"].ToString().Equals(""))
						{
							dr["LastDeliveredDate"] = Master.BizDatePrior;
						}

						if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
						{
							//------ Create the ShortsVsFTD BUY-INS report filecontent for this recipient's specific file format (attribute in tbReportValues) ------------------ 
							rptCount = 0; 
							rowCount = 0; 
							lineFormat = ""; 
							fileContent = "";    //CUSIP,SYMBOL,BUYIN	// File does Not need header columns 

							foreach (DataRow drFails in dsShortsVsFTD.Tables["FailsBuyIn"].Rows)
							{
								lineFormat = dr["Format"].ToString();

								if (dr["Justify"].ToString().Trim().ToUpper().Equals("Y"))
								{
									lineFormat = lineFormat.Replace("%CUSIP", drFails["CUSIP"].ToString().Trim().PadRight(12, ' '));
									lineFormat = lineFormat.Replace("%Symbol", drFails["Symbol"].ToString().Trim().PadRight(6, ' '));
									lineFormat = lineFormat.Replace("%BuyIn", drFails["BuyIn"].ToString().Trim().PadLeft(15, ' '));
								}
								else
								{
									lineFormat = 
										drFails["CUSIP"].ToString().Trim() + "," +  
										drFails["Symbol"].ToString().Trim() + "," +  
										decimal.Parse(drFails["BuyIn"].ToString()).ToString("0") ; 
								}

								fileContent += lineFormat + "\r\n";
								rowCount++; 

							}	// foreach inner drFails File Loop 
						
							//------ Create the file 
							fileName = tempPath + @"\FAILS_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy_MM_dd") + ".txt"; 
							sr = File.CreateText(fileName);
							sr.Write(fileContent);
							sr.Close();

							//------ Send the email with FAILS-To-Buy_In file 
							if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
							{
								mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "EOD FAILS File " + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd"), "FAILS_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy_MM_dd") + @".txt to be copy to \\RSPen001\Fails folder.", fileName); 
							}
							else
							{	// Not an Email address, upload to a ftp site 
								mail.Send("mbattaini@penson.com;dchen@penson.com", "sendero@penson.com", "EOD FAILS File Update", "EOD FAILS_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy_MM_dd") + ".txt  File Uploaded to " + @dr["ReportRecipient"].ToString(), fileName);
								filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\FAILS_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy_MM_dd") + ".txt", "localHost", "", "", fileName);
							} 

							//------ EOD FAILS file has been mailed/uploaded, now Update tbReportValues for this report/recipient to new date. 
							ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.BizDate);
							rptCount ++;
						}

					}
					catch (Exception error)
					{	
						KeyValue.Set("AresShortsVsFTDMailStatus", "ShortsVsFTD FAILS File ERROR (" + DateTime.Now.ToString("yyyy-MM-dd") + "): " + error.Message.Substring(0,180), dbCnStr);
						Log.Write("ERROR: " + error.Message + ".  [AresMain.ShortsVsFTDMail]", Log.Error, 1); 
					}

				}	// foreach outer Loop:  dsReportRecipients for Buy-Ins report 
 
				
				//------ Successfully completed BOTH ShortsVsFTD Exceptions and FAILS-BuyIn files creating and mailing 
				KeyValue.Set("AresShortsVsFTDMailDate", Master.ContractsBizDate, dbCnStr);
				KeyValue.Set("AresShortsVsFTDMailStatus", "OK", dbCnStr);
				Log.Write("Daily Shorts-Vs-FTD Exception files and Fails files mailed/uploaded, for BizDate: " + Master.BizDate + ".  [AresMain.ShortsVsFtdMail]", 2);

			}
			catch (Exception error)
			{
					KeyValue.Set("AresShortsVsFTDMailStatus", "ERROR (" + DateTime.Now.ToString("yyyy-MM-dd") + "): " + error.Message.Substring(0,220), dbCnStr);
					Log.Write("ERROR: " + error.Message + ".  [AresMain.ShortsVsFtdMail]", Log.Error, 1);
					return;
			}
			finally
			{		// Graceful Exit 
					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
			}


		}


		private void FailedS3MovementsEmail()
		{
			DataSet dsTempMvts = new DataSet();
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			SqlDataAdapter dataAdapter = null;
				
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{											
					string sql ="select	accountnumber,\r\n" +
						"accounttype,\r\n" + 
						"secid,\r\n" +
						"quantity,\r\n" +
						"indicator\r\n" +
						"from	tbsegentries with (nolock)\r\n" +
						"where	bizdate = '" + Master.BizDate + "'\r\n" +
						"and		((isfailed = 1) or (isRequested = 1 and isProcessed = 0))\r\n" +
						"and		TimeOfDay = 'I'";


					string emailBody = "";

					SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
					dbCmd.CommandType = CommandType.Text;

							
					dataAdapter = new SqlDataAdapter(dbCmd);
					dataAdapter.Fill(dsTempMvts, "FailedMovements");

					
					emailBody +=  "AccountNumber".PadRight(15, ' ') + "T".PadRight(2, ' ') + "SecId".PadRight(10, ' ') +  "Quantity".PadLeft(15, ' ').Substring(0, 15) + "I".PadLeft(3, ' ') +	"\r\n";

					foreach (DataRow dr in dsTempMvts.Tables["FailedMovements"].Rows)
					{
						emailBody +=  dr["AccountNumber"].ToString().PadRight(15, ' ') + dr["AccountType"].ToString().PadRight(2, ' ') + dr["SecId"].ToString().PadRight(10, ' ') +  dr["Quantity"].ToString().PadLeft(15, ' ').Substring(0, 15) + dr["Indicator"].ToString().PadLeft(3, ' ') +	"\r\n";
					}
							
					if (!previousEmail.Equals(emailBody))
					{	
						string to = KeyValue.Get("AresS3FailMovementsMailTo", "mbattaini@penson.com", dbCnStr);
							
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresS3FailMovementsMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresS3FailMovementsMailSubject", "Failed Intra-day S3 Movements", dbCnStr),
								emailBody);															
					
							previousEmail = emailBody;
						}								
					}
				}				
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}							
		}
		
		private void BankLoanLocationExceptions()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();
				
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresBankLoanLocationExceptionsEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{							
						if (KeyValue.Get("BankLoanDtcReset0234", "", dbCnStr).Equals("True"))
						{							
							string emailBody = "";

							SqlCommand dbCmd = new SqlCommand("spBankLoanPledgeSummaryGet",localDbCn);
							dbCmd.CommandType = CommandType.StoredProcedure;

							dbCmd.CommandTimeout = 1600;

							SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
							paramBizDate.Value = Master.ContractsBizDate;

							SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);											
							dataAdapter.Fill(dataSet, "BankLoanPledgeSummary");
				
							emailBody = "Special Loan Exceptions\r\n";
							emailBody += "-----------------------\r\n";
								
							foreach(DataRow dr in dataSet.Tables["BankLoanPledgeSummary"].Rows)
							{
								if ((((long) dr["SpecialQuantityAvailable"]) < 0) && ((((long) dr["SpecialQuantityPledged"]) > 0) ))
								{
									emailBody += dr["SecId"].ToString() + " " +  dr["Symbol"].ToString().PadRight(10, ' ') + " " + dr["SpecialQuantityAvailable"].ToString() + "\r\n";
								}
							}
								
							emailBody += "\r\n\r\nCustomer Loan Exceptions\r\n";
							emailBody += "-----------------------\r\n";
								
							foreach(DataRow dr in dataSet.Tables["BankLoanPledgeSummary"].Rows)
							{
								if ((((long) dr["CustomerQuantityAvailable"]) < 0)  && ((((long) dr["CustomerQuantityPledged"]) > 0) ))
								{
									emailBody += dr["SecId"].ToString() + " " +  dr["Symbol"].ToString().PadRight(10, ' ') + " " + dr["CustomerQuantityAvailable"].ToString() + "\r\n";
								}
							}
								
							emailBody += "\r\n\r\nFirm Loan Exceptions\r\n";
							emailBody += "-----------------------\r\n";

							foreach(DataRow dr in dataSet.Tables["BankLoanPledgeSummary"].Rows)
							{
								if ((((long) dr["FirmQuantityAvailable"]) < 0)  && ((((long) dr["FirmQuantityPledged"]) > 0) ))
								{
									emailBody += dr["SecId"].ToString() + " " + dr["Symbol"].ToString().PadRight(10, ' ') + " " + dr["FirmQuantityAvailable"].ToString() + "\r\n";
								}
							}																																																
								
							string to = KeyValue.Get("AresBankLoanLocationExceptionsMailTo", "", dbCnStr);
							
							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresBankLoanLocationExceptionsMailFrom", "stockloan@penson.com", dbCnStr),
									KeyValue.Get("AresBankLoanLocationExceptionsMailSubject", "BankLoan Location Exceptions", dbCnStr),
									emailBody);															
					
								KeyValue.Set("AresBankLoanLocationExceptionsEmailDate", Master.ContractsBizDate, dbCnStr);
							}								
						}						
					}					
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}		

		private void Straddles()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresStraddlesEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{							
						string sql ="select	a.accountnumber, \r\n" +
							"a.cusip,\r\n" +
							"a.settlementdatequantity\r\n" +
							"from	stocklocationcurrent a,\r\n" +
							"stocklocationcurrent b\r\n" +
							"where	a.firm = '07'\r\n" +
							"and		b.firm = a.firm\r\n" +
							"and		a.cusip = b.cusip\r\n" +
							"and		a.accountnumber = b.accountnumber\r\n" +
							"and		a.accounttype = 3\r\n" +
							"and		b.accounttype = 3\r\n" +
							"and		a.settlementdatequantity > 0\r\n" +
							"and		b.settlementdatequantity < 0\r\n" +
							"and		a.locmemo  ='C'\r\n" +
							"and		b.locmemo = 'S'\r\n";


						string emailBody = "";

						SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
						dbCmd.CommandType = CommandType.Text;

						SqlDataReader dataReader = null;
					
						localDbCn.Open();
						dataReader = dbCmd.ExecuteReader();				
							
						int count = 0;

						emailBody = "Account".PadRight(15, ' ') +  "CUSIP".PadRight(10, ' ') +  "Quantity".PadLeft(15, ' ') + "\r\n";

						while (dataReader.Read())
						{
							emailBody += dataReader.GetValue(0).ToString().PadRight(15, ' ') +  dataReader.GetValue(1).ToString().PadRight(10, ' ') +  dataReader.GetValue(2).ToString().PadLeft(15, ' ') + "\r\n";
							count ++;
						}
						if (count == 0)
						{
							emailBody += "[No straddles to report]";
						}
				
						string to = KeyValue.Get("AresStraddlesMailTo", "mbattaini@penson.com", dbCnStr);
							
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresStraddlesMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresStraddlesMailSubject", "Straddles Account Type 3", dbCnStr),
								emailBody);															
					
							KeyValue.Set("AresStraddlesEmailDate", Master.ContractsBizDate, dbCnStr);
						}

						dataReader.Close();
					}						
				}									
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}		

		private void BankLoanExceptions()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresBankLoanExceptionsEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{							
						if (KeyValue.Get("BankLoanDtcReset0234", "", dbCnStr).Equals("True"))
						{
							string sql ="select	distinct(b.secid), \r\n" +
								"sil.secidlink as symbol, \r\n" +
								"sum (b.CustomerPledgeSettled + b.FirmPledgeSettled)\r\n" +
								"from	tbBoxPosition b,\r\n" +
								"			tbsecidlinks sil\r\n" +
								"Where	b.secid not in (select secid from tbBankLoanposition where secid = b.secid)\r\n" +
								"and		b.bookgroup = '0234' and b.bizdate = '" + Master.ContractsBizDate + "' and (b.CustomerPledgeSettled + b.FirmPledgeSettled) > 0	\r\n" +
								"and		sil.secid = b.secid\r\n" +
								"and		sil.secidtypeindex = 2\r\n" +
								"group by b.secid, sil.secidlink\r\n" +
								"order by sil.secidlink\r\n";


							string emailBody = "";

							SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
							dbCmd.CommandType = CommandType.Text;

							SqlDataReader dataReader = null;
					
							localDbCn.Open();
							dataReader = dbCmd.ExecuteReader();				
				
							while (dataReader.Read())
							{
								emailBody += dataReader.GetValue(0).ToString().PadRight(12, ' ') +  dataReader.GetValue(1).ToString().PadRight(10, ' ') +  dataReader.GetValue(2).ToString().PadLeft(15, ' ') + "\r\n";
							}
				
							string to = KeyValue.Get("AresBankLoanExceptionsMailTo", "", dbCnStr);
							
							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresBankLoanExceptionsMailFrom", "stockloan@penson.com", dbCnStr),
									KeyValue.Get("AresBankLoanExceptionsMailSubject", "BankLoan Exceptions", dbCnStr),
									emailBody);															
					
								KeyValue.Set("AresBankLoanExceptionsEmailDate", Master.ContractsBizDate, dbCnStr);
							}

							dataReader.Close();
						}						
					}					
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}		

		private void RateDifferencesEmail()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresRateDifferencesEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{							
						if (KeyValue.Get("BankLoanDtcReset0234", "", dbCnStr).Equals("True"))
						{
							string sql ="Select	C.ContractId,\r\n" +
								"C.SecId,\r\n" +
								"C.Rate,\r\n" +
								"S.ContractId,\r\n" +
								"S.SecId,\r\n" +
								"S.Rate\r\n" +
								"from	tbContracts C,\r\n" +
								"			tbContracts S\r\n" +
								"Where	C.BizDate = '" + Master.ContractsBizDate + "'\r\n" +
								"And		C.BookGroup = '0234'\r\n" +
								"And		S.BizDate = C.BizDate\r\n" +
								"And		S.BookGroup = C.BookGroup\r\n" +
								"And		S.SecId = C.SecId\r\n" +
								"And		S.Rate > 0\r\n" +
								"And		C.Rate < 0\r\n" +
								"And		S.ContractId <> C.ContractId\r\n" +
								"And		C.ContractType = 'L'\r\n" +
								"And		S.ContractType = C.ContractType\r\n";


							string emailBody = "";

							SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
							dbCmd.CommandType = CommandType.Text;

							SqlDataReader dataReader = null;
					
							localDbCn.Open();
							dataReader = dbCmd.ExecuteReader();				
				
							while (dataReader.Read())
							{
								emailBody += dataReader.GetValue(0).ToString().PadRight(12, ' ') +  dataReader.GetValue(1).ToString().PadRight(10, ' ') +  dataReader.GetValue(2).ToString().PadLeft(15, ' ') + 
									dataReader.GetValue(3).ToString().PadRight(12, ' ') +  dataReader.GetValue(4).ToString().PadRight(10, ' ') +  dataReader.GetValue(5).ToString().PadLeft(15, ' ') + "\r\n";
							}
				
							string to = KeyValue.Get("AresRateDifferencesMailTo", "", dbCnStr);
							
							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresRateDifferencessMailFrom", "stockloan@penson.com", dbCnStr),
									KeyValue.Get("AresRateDifferencesMailSubject", "Rate Differences", dbCnStr),
									emailBody);															
					
								KeyValue.Set("AresRateDifferencesEmailDate", Master.ContractsBizDate, dbCnStr);
							}

							dataReader.Close();
						}						
					}					
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}		

		private void CnsDeliveriesMail ()
		{			
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
	
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresCNSDeliveriesEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{
						if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresCNSDeliveriesCreateTime", "06:00", dbCnStr), "HH:mm", null))
						{
						
							string sql = "Select	S.CUSIP,\r\n" +
								"	M.SecIdLink,\r\n" +
								"	S.SettlementDateQuantityCurrent\r\n" +
								"From	StockLocationCurrent S,\r\n" +
								"			tbSecIdLinks M\r\n" +
								"Where	S.AccountNumber = '93111128'\r\n" +
								"And		S.CUSIP IN (	Select distinct(secid)\r\n" +
								"									from	tbContracts\r\n" +
								"							WHere	BizDate = (select keyvalue from tbkeyvalues where keyid = 'ContractsBizDate')\r\n" +
								"And		BookGroup  ='0234'					\r\n" +
								"And		Rate < 0)\r\n" +
								"And	S.SettlementDateQuantityCurrent < 0\r\n" +
								"And	M.SecId = S.CUSIP\r\n" +
								"And	M.SecIdTypeIndex = 2\r\n" +
								"Order  by M.SecIdLink\r\n";

							string emailBody = "";

				
		
							SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
							dbCmd.CommandType = CommandType.Text;

							SqlDataReader dataReader = null;
					
							localDbCn.Open();
							dataReader = dbCmd.ExecuteReader();				
				
							while (dataReader.Read())
							{
								emailBody += dataReader.GetValue(0).ToString().PadRight(12, ' ') +  dataReader.GetValue(1).ToString().PadRight(10, ' ') +  dataReader.GetValue(2).ToString().PadLeft(15, ' ') + "\r\n";
							}
				
							string to = KeyValue.Get("AresCnsDeliveriesMailTo", "", dbCnStr);
							
							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresCnsDeliveriesMailFrom", "stockloan@penson.com", dbCnStr),
									KeyValue.Get("AresCnsDeliveriesMailSubject", "CNS", dbCnStr),
									emailBody);															
					
								KeyValue.Set("AresCNSDeliveriesEmailDate", Master.ContractsBizDate, dbCnStr);
							}

							dataReader.Close();
						}						
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}

		public void RecordDateContractsMail()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
	
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresRecordDateContractsEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{
						if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresRecordDateContractsCreateTime", "06:00", dbCnStr), "HH:mm", null))
						{
						
							string sql =	"Select	C.ContractId, \r\n"+
								"C.SecId, \r\n" +
								"SIL.SecIdLink,\r\n" +
								"C.Rate,\r\n" + 
								"S.RecordDateCash\r\n" +
								"From	tbContracts C,\r\n" +
								"			tbSecMaster S,\r\n" +
								"			tbSecIdLinks SIL\r\n" +
								"Where	C.BizDate = '" + Master.ContractsBizDate + "'\r\n" +
								"And		C.BookGroup = '0234' \r\n" +
								"And		C.ContractType = 'L'\r\n" +
								"And		C.Rate > 0\r\n" +
								"And		S.SecId = C.SecId\r\n" +
								"And		S.RecordDateCash = '" + Master.BizDateNext + "'\r\n" +
								"And		SIL.SecId = C.SecId\r\n" +
								"And		SIL.SecIdTypeIndex = 2";

							string emailBody = "";
						
							SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
							dbCmd.CommandType = CommandType.Text;

							SqlDataReader dataReader = null;
					
							localDbCn.Open();
							dataReader = dbCmd.ExecuteReader();				
				
							while (dataReader.Read())
							{
								emailBody += dataReader.GetValue(0).ToString().PadRight(12, ' ') 
									+		dataReader.GetValue(1).ToString().PadLeft(12, ' ') 
									+		dataReader.GetValue(2).ToString().PadLeft(15, ' ') 
									+		dataReader.GetValue(3).ToString().PadLeft(8, ' ') 
									+		dataReader.GetValue(4).ToString().PadLeft(15, ' ') +"\r\n";
							}
				
							string to = KeyValue.Get("AresRecordDateContractsMailTo", "", dbCnStr);
							
							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresRecordDateContractsMailFrom", "stockloan@penson.com", dbCnStr),
									KeyValue.Get("AresRecordDateContractsMailSubject", "Contracts with record date tomorrow", dbCnStr),
									emailBody);															
					
								KeyValue.Set("AresRecordDateContractsEmailDate", Master.ContractsBizDate, dbCnStr);
							}

							dataReader.Close();
						}						
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}

		public void ThresholdRatesMail()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
	
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresThresholdRatesMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{
						if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresThresholdRatesCreateTime", "06:00", dbCnStr), "HH:mm", null))
						{						
							if (KeyValue.Get("AztecFileLoadDate", "", dbCnStr).Equals(Master.ContractsBizDate))								
							{
								string sql =	"Create Table #Rates (\r\n" + 
									"	SecId varchar(12),\r\n" +
									"	Symbol varchar(12),\r\n" +
									"	Rate decimal(12,5))\r\n" +
									"Insert Into #Rates (\r\n" +
									"		SecId,\r\n" +
									"		Symbol,\r\n" +
									"		Rate)\r\n" +
									"Select	I.SecId,\r\n" +
									"		IsNUll(SIL.SecIdLink, I.SecId) As Symbol,\r\n" +
									"		AVG(I.Rate) - 2\r\n" +
									"From	dbo.tbInventoryRateControl I,\r\n" +
									"		dbo.tbSecIdLinks SIL\r\n" +
									"Where	I.BizDate = '" + Master.BizDate + "'\r\n" +
									"And		I.Rate <= 0\r\n" +
									"And		Sil.Secid = I.Secid\r\n" +
									"And		Sil.SecIdTypeIndex = 2\r\n" +
									"Group By I.SecId, Sil.SecIdLink\r\n" +
									"Order BY Sil.SecIdLink\r\n" +
									"Insert Into #Rates (\r\n" +
									"		SecId,\r\n" +
									"		Symbol,\r\n" +
									"		Rate)\r\n" +
									"Select	S.SecId,\r\n" +
									"		IsNUll(SIL.SecIdLink, S.SecId) As Symbol,\r\n" +
									"		AVG(S.Rate) - 2\r\n" +
									"From	dbo.tbContracts S,\r\n" +
									"		dbo.tbSecIdLinks SIL\r\n" +
									"Where	S.BizDate = '" + Master.BizDate + "'\r\n" +
									"And		S.BookGroup = '0234'\r\n" +
									"And		S.ContractType = 'B'\r\n" +
									"And		S.Rate <= 0\r\n" +
									"And		Sil.Secid = S.Secid\r\n" +
									"And		Sil.SecIdTypeIndex = 2\r\n" +
									"And		S.SecId Not In (Select distinct(secid) from #Rates Where secid = s.secid)\r\n" +
									"Group By S.SecId, Sil.SecIdLink\r\n" +
									"Order BY Sil.SecIdLink\r\n" +
									"select	R.SecId,\r\n" +
									"		R.Symbol,\r\n" +
									"		R.Rate\r\n" +
									"from	#rates R\r\n" +			
									"Where R.SecId In (Select distinct(secid) From dbo.tbThreshold Where SecId = R.SecId And BizDate = '" + Master.BizDatePriorExchange + "')\r\n" +
									"Order By R.SecId\r\n" +
									"Drop Table #Rates\r\n";

								string emailBody = "";
						
								SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
								dbCmd.CommandType = CommandType.Text;

								SqlDataReader dataReader = null;
					
								localDbCn.Open();
								dataReader = dbCmd.ExecuteReader();				
				
								while (dataReader.Read())
								{
									emailBody += dataReader.GetValue(0).ToString().PadRight(12, ' ') + " " 					
										+		dataReader.GetValue(1).ToString().PadLeft(12, ' ') + " "
										+		dataReader.GetValue(2).ToString().PadLeft(15, ' ') +"\r\n";
								}
				
								string to = KeyValue.Get("AresThresholdRatesMailTo", "", dbCnStr);
							
								if (!to.Equals(""))
								{
									Email email = new Email(dbCnStr);
									email.Send( to,
										KeyValue.Get("AresThresholdRatesMailFrom", "stockloan@penson.com", dbCnStr),
										KeyValue.Get("AresThresholdRatesMailSubject", "Penson Threshold Rates For: " + Master.BizDate , dbCnStr),
										emailBody);															
					
									KeyValue.Set("AresThresholdRatesMailDate", Master.ContractsBizDate, dbCnStr);
								}

								dataReader.Close();
							}						
						}
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}			
		}

		public void HardRatesMail()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
	
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresHardRatesMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{
						if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresHardRatesCreateTime", "06:00", dbCnStr), "HH:mm", null))
						{						
							if (KeyValue.Get("AztecFileLoadDate", "", dbCnStr).Equals(Master.ContractsBizDate))								
							{
								string sql =	"Create Table #Rates (\r\n" + 
									"	SecId varchar(12),\r\n" +
									"	Symbol varchar(12),\r\n" +
									"	Rate decimal(12,5))\r\n" +
									"Insert Into #Rates (\r\n" +
									"		SecId,\r\n" +
									"		Symbol,\r\n" +
									"		Rate)\r\n" +
									"Select	I.SecId,\r\n" +
									"		IsNUll(SIL.SecIdLink, I.SecId) As Symbol,\r\n" +
									"		AVG(I.Rate) - 2\r\n" +
									"From	dbo.tbInventoryRateControl I,\r\n" +
									"		dbo.tbSecIdLinks SIL\r\n" +
									"Where	I.BizDate = '" + Master.BizDate + "'\r\n" +
									"And		I.Rate <= 0\r\n" +
									"And		Sil.Secid = I.Secid\r\n" +
									"And		Sil.SecIdTypeIndex = 2\r\n" +
									"Group By I.SecId, Sil.SecIdLink\r\n" +
									"Order BY Sil.SecIdLink\r\n" +
									"Insert Into #Rates (\r\n" +
									"		SecId,\r\n" +
									"		Symbol,\r\n" +
									"		Rate)\r\n" +
									"Select	S.SecId,\r\n" +
									"		IsNUll(SIL.SecIdLink, S.SecId) As Symbol,\r\n" +
									"		AVG(S.Rate) - 2\r\n" +
									"From	dbo.tbContracts S,\r\n" +
									"		dbo.tbSecIdLinks SIL\r\n" +
									"Where	S.BizDate = '" + Master.BizDate + "'\r\n" +
									"And		S.BookGroup = '0234'\r\n" +
									"And		S.ContractType = 'B'\r\n" +
									"And		S.Rate <= 0\r\n" +
									"And		Sil.Secid = S.Secid\r\n" +
									"And		Sil.SecIdTypeIndex = 2\r\n" +
									"And		S.SecId Not In (Select distinct(secid) from #Rates Where secid = s.secid)\r\n" +
									"Group By S.SecId, Sil.SecIdLink\r\n" +
									"Order BY Sil.SecIdLink\r\n" +
									"select	R.SecId,\r\n" +
									"		R.Symbol,\r\n" +
									"		R.Rate\r\n" +
									"from	#rates R\r\n" +
									"Where R.SecId Not In (Select distinct(secid) From dbo.tbThreshold Where SecId = R.SecId And BizDate = '" + Master.BizDatePriorExchange + "')\r\n" +
									"Order By R.SecId\r\n" +
									"Drop Table #Rates\r\n";

								string emailBody = "";
						
								SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
								dbCmd.CommandType = CommandType.Text;

								SqlDataReader dataReader = null;
					
								localDbCn.Open();
								dataReader = dbCmd.ExecuteReader();				
				
								while (dataReader.Read())
								{
									emailBody += dataReader.GetValue(0).ToString().PadRight(12, ' ') + " " 					
										+		dataReader.GetValue(1).ToString().PadLeft(12, ' ') + " "
										+		dataReader.GetValue(2).ToString().PadLeft(15, ' ') +"\r\n";
								}
				
								string to = KeyValue.Get("AresHardRatesMailTo", "", dbCnStr);
							
								if (!to.Equals(""))
								{
									Email email = new Email(dbCnStr);
									email.Send( to,
										KeyValue.Get("AresHardRatesMailFrom", "stockloan@penson.com", dbCnStr),
										KeyValue.Get("AresHardRatesMailSubject", "Penson Hard Rates For: " + Master.BizDate , dbCnStr),
										emailBody);															
					
									KeyValue.Set("AresHardRatesMailDate", Master.ContractsBizDate, dbCnStr);
								}

								dataReader.Close();
							}						
						}
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}
			
		private void LikeTrades ()
		{
			try
			{
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresLikeTradesEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{
						string to =  KeyValue.Get("AresLikeTradesMailTo", "", dbCnStr);
						string emailBody  = 
							"BookGroup	T ContractId  Security ID   Quantity\r\r" +
							"---------  - ----------  -----------   -----------\r\n" +

							"[No like contracts for 7380]\r\n";


						Email email = new Email(dbCnStr);

						if (!to.Equals(""))
						{
							email.Send( to,
								KeyValue.Get("AresLikeTradesMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresLikeTradesMailSubject", "Borrow / Loan Comparison For 7380", dbCnStr),
								emailBody);		
				
							KeyValue.Set("AresLikeTradesEmailDate", Master.ContractsBizDate, dbCnStr);
						}
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
		}

		private void DeficitReport()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			string emailBody = "";

			try
			{
				if (!KeyValue.Get("AresDeficitReportEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
				{
					if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresDeficitReportCreateTime", "06:00", dbCnStr), "HH:mm", null))
					{							
						Common.Filer filer = new Common.Filer(Standard.ConfigValue("TempPath") + "deficitreport.txt");
						filer.Timeout = 90000;
								
						StreamReader streamReader = null;
								
						streamReader = new StreamReader(
							filer.StreamGet(
							Standard.ConfigValue("Phase3DeficitReportName", ""),
							Standard.ConfigValue("Phase3DeficitReportHost", ""),
							Standard.ConfigValue("Phase3DeficitReportUserName", ""),
							Standard.ConfigValue("Phase3DeficitReportPassword", "")),
							System.Text.Encoding.ASCII);     
						streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
				
						string fileContents = "";
						int lineCount = 1;
						int itemCount = 0;
						string line = "-";


						//fileContents = streamReader.ReadToEnd();

						//Log.Write(streamReader.ReadToEnd(), 1);
						int lineLength = 140;
								
						Log.Write("Filtering report: " + Standard.ConfigValue("Phase3DeficitReportName", ""), 1);

						while (line!= null)
						{  							
							line = streamReader.ReadLine();
							if (line != null)
							{
								line = line.Replace("\r", "");
														 
								try
								{
									if (line.Substring(0, 6).Equals("07960H"))
									{						
										string reportDate = "";
										reportDate = DateTime.Parse(line.Substring(116, 8)).ToString(Standard.DateFormat);
						
										if (!reportDate.Equals(Master.ContractsBizDate))
										{
											Log.Write("File is not for current date " +  Master.ContractsBizDate + ". [DeficitReport.Load]", 2);
											return;
										}
									}
									else if (line.Substring(0, 6).Equals("079604"))
									{																																		
										line = line.Replace("079604", "");
										emailBody += line + "\r\n";
										
										itemCount ++;																			
									}
							
									lineCount ++;
								}
								catch (Exception error)
								{
									Log.Write(error.Message, 1);
								}
							}
						}
						

						string to = KeyValue.Get("AresDeficitReportMailTo", "", dbCnStr);
							
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresDeficitReportMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresDeficitReportMailSubject", "CNS", dbCnStr),
								emailBody);															
					
							KeyValue.Set("AresDeficitReportEmailDate", Master.ContractsBizDate, dbCnStr);
						}
					}
				}
			}			
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}

		private void FailsEmail()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresFailsEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{							
						string sql ="select	B.SecId,\r\n" +
							"S.SecIdLink,\r\n" +
							"SB.LastPrice, \r\n" +
							"B.ExDeficitSettled,\r\n" +
							"B.DeficitDayCount,\r\n" +
							"DvpFailOutSettled,\r\n" +
							"DvpFailOutDayCount,\r\n" +
							"ClearingFailOutSettled,\r\n" +
							"ClearingFailOutDayCount,\r\n" +
							"BrokerFailOutSettled,\r\n" +
							"BrokerFailOutDayCount,\r\n" +
							"(Select	isnull(sum(quantitysettled), 0) from tbcontracts where BookGroup = '0234' and BizDate = '" + Master.ContractsBizDate + "' and secid = b.secid and contracttype = 'L') aS Loans\r\n" +
							"from	tbBoxPosition B,\r\n" +
							"		tbSecIdLinks S,\r\n" +
							"		tbSecMaster SB\r\n" +
							"where	B.BookGroup = '0234'\r\n" +
							"And		B.BizDate = '"+ Master.ContractsBizDate +"'\r\n" +
							"And		B.SecId = S.SecId\r\n" +
							"And		S.SecIdTypeIndex = 2\r\n" +
							"And		SB.SecId = S.SecId\r\n" +
							"and		((B.DeficitDayCount > 4 AND B.ExDeficitSettled < 0) OR	DvpFailOutDayCount > 4 OR ClearingFailOutDayCount > 4 OR BrokerFailOutDayCount > 4)\r\n" +
							"and		(Select	isnull(sum(quantitysettled), 0) from tbcontracts where BookGroup = '0234' and BizDate = '" + Master.ContractsBizDate +"' and secid = b.secid and contracttype = 'L') > 0\r\n" +
							"Group By B.SecId,\r\n" +
							"		S.SecIdLink,\r\n" +
							"		SB.LastPrice,\r\n" +
							"		B.ExDeficitSettled,\r\n" +
							"		B.DeficitDayCount,\r\n" +
							"		DvpFailOutSettled,\r\n" +
							"		DvpFailOutDayCount,\r\n" +
							"		ClearingFailOutSettled,\r\n" +
							"		ClearingFailOutDayCount,\r\n" +
							"		BrokerFailOutSettled,\r\n" +
							"		BrokerFailOutDayCount\r\n" +
							"Order by loans desc\r\n";


						string emailBody = "";

						SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
						dbCmd.CommandType = CommandType.Text;

						SqlDataReader dataReader = null;
					
						localDbCn.Open();
						dataReader = dbCmd.ExecuteReader();				

						emailBody = "SecId".PadRight(12, ' ') +
							"Symbol".PadRight(12, ' ') + 
							"Price".PadRight(12, ' ') + 
							"ExDeficit".PadRight(12, ' ') +  
							"Days".PadRight(12, ' ')  +
							"DvpFTD".PadRight(12, ' ') +  
							"Days".PadRight(12, ' ')  +
							"CnsFTD".PadRight(12, ' ') +  
							"Days".PadRight(12, ' ') +
							"BrokerFTD".PadRight(12, ' ') +
							"Days".PadRight(12, ' ')  +
							"Loans".PadRight(12, ' ') + "\r\n";
				
						while (dataReader.Read())
						{
							emailBody +=	dataReader.GetValue(0).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(1).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(2).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(3).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(4).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(5).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(6).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(7).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(8).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(9).ToString().PadRight(12, ' ')	+  
								dataReader.GetValue(10).ToString().PadRight(12, ' ')	+ 
								dataReader.GetValue(11).ToString().PadRight(12, ' ') + "\r\n";
						}
				
						string to = KeyValue.Get("AresFailsMailTo", "", dbCnStr);
							
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresFailsMailFrom", "sendero@penson.com", dbCnStr),
								KeyValue.Get("AresFailsMailSubject", "Fails Email", dbCnStr),
								emailBody);															
					
							KeyValue.Set("AresFailsEmailDate", Master.ContractsBizDate, dbCnStr);
						}

						dataReader.Close();
					}						
				}					
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}

		private void RedsFailsEmail()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresRedsFailsEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{							
						string sql ="select	B.SecId,\r\n" +
							"S.SecIdLink,\r\n" +
							"SB.LastPrice, \r\n" +
							"B.ExDeficitSettled,\r\n" +
							"B.DeficitDayCount,\r\n" +
							"DvpFailOutSettled,\r\n" +
							"DvpFailOutDayCount,\r\n" +
							"ClearingFailOutSettled,\r\n" +
							"ClearingFailOutDayCount,\r\n" +
							"BrokerFailOutSettled,\r\n" +
							"BrokerFailOutDayCount\r\n" +								
							"from	tbBoxPosition B,\r\n" +
							"		tbSecIdLinks S,\r\n" +
							"		tbSecMaster SB\r\n" +
							"where	B.BookGroup = '0234'\r\n" +
							"And		B.BizDate = '"+ Master.ContractsBizDate +"'\r\n" +
							"And		B.SecId = S.SecId\r\n" +
							"And		S.SecIdTypeIndex = 2\r\n" +
							"And		SB.LastPrice >= 0.50\r\n" +
							"And		SB.BaseType = 'E'\r\n" + 
							"And		SB.SecId = S.SecId\r\n" +
							"and		((B.DeficitDayCount > 10 AND B.ExDeficitSettled < 0) OR	DvpFailOutDayCount > 10 OR ClearingFailOutDayCount > 10 OR BrokerFailOutDayCount > 10)\r\n" +
							"Group By B.SecId,\r\n" +
							"		S.SecIdLink,\r\n" +
							"		SB.LastPrice,\r\n" +
							"		B.ExDeficitSettled,\r\n" +
							"		B.DeficitDayCount,\r\n" +
							"		DvpFailOutSettled,\r\n" +
							"		DvpFailOutDayCount,\r\n" +
							"		ClearingFailOutSettled,\r\n" +
							"		ClearingFailOutDayCount,\r\n" +
							"		BrokerFailOutSettled,\r\n" +
							"		BrokerFailOutDayCount\r\n";								


						string emailBody = "";

						SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
						dbCmd.CommandType = CommandType.Text;

						SqlDataReader dataReader = null;
					
						localDbCn.Open();
						dataReader = dbCmd.ExecuteReader();				

						emailBody = "SecId".PadRight(12, ' ') +
							"Symbol".PadRight(12, ' ') + 
							"Price".PadRight(12, ' ') + 
							"ExDeficit".PadRight(12, ' ') +  
							"Days".PadRight(12, ' ')  +
							"DvpFTD".PadRight(12, ' ') +  
							"Days".PadRight(12, ' ')  +
							"CnsFTD".PadRight(12, ' ') +  
							"Days".PadRight(12, ' ') +
							"BrokerFTD".PadRight(12, ' ') +
							"Days".PadRight(12, ' ')  + "\r\n";
				
						while (dataReader.Read())
						{
							emailBody +=	dataReader.GetValue(0).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(1).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(2).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(3).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(4).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(5).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(6).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(7).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(8).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(9).ToString().PadRight(12, ' ')	+  
								dataReader.GetValue(10).ToString().PadRight(12, ' ') + "\r\n";
						}
				
						string to = KeyValue.Get("AresRedsFailsMailTo", "", dbCnStr);
							
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresRedsFailsMailFrom", "sendero@penson.com", dbCnStr),
								KeyValue.Get("AresRedsFailsMailSubject", "Fails Email Beyond 10 days", dbCnStr),
								emailBody);															
					
							KeyValue.Set("AresRedsFailsEmailDate", Master.ContractsBizDate, dbCnStr);
						}

						dataReader.Close();
					}						
				}					
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}


		private TimeSpan RecycleInterval()
		{
			string recycleInterval;
			string [] values;

			int hours;
			int minutes;

			bool isBizDay = (DateTime.UtcNow.DayOfWeek != DayOfWeek.Saturday) && (DateTime.UtcNow.DayOfWeek != DayOfWeek.Sunday);
			TimeSpan timeSpan;

			char [] delimiter = new char[1];
			delimiter[0] = ':';

			if (isBizDay)
			{
				recycleInterval = KeyValue.Get("AresMainRecycleIntervalBizDay", "0:20", dbCn);
			}
			else
			{
				recycleInterval = KeyValue.Get("AresMainRecycleIntervalNonBizDay", "6:00", dbCn);
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
					KeyValue.Set("AresMainRecycleIntervalBizDay", "0:20", dbCn);
					hours = 0;
					minutes = 20;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [AresMain.RecycleInterval]", Log.Error, 1);
				}
				else
				{
					KeyValue.Set("AresMainRecycleIntervalNonBizDay", "6:00", dbCn);
					hours = 6;
					minutes = 0;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [AresMain.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("AresMain will recycle in " + hours + " hours, " + minutes + " minutes. [AresMain.RecycleInterval]", 2);
			return timeSpan;
		}
   
		public static string DatePartSet(string inputString, string dateValue)
		{
			int leftBracketIndex = inputString.IndexOf('{');
			int rightBracketIndex = inputString.IndexOf('}');
            
			if ((leftBracketIndex > -1) && (rightBracketIndex > leftBracketIndex))
			{
				string formatString = inputString.Substring(leftBracketIndex + 1, rightBracketIndex - leftBracketIndex - 1);
 
				return inputString.Substring(0, leftBracketIndex) + Tools.FormatDate(dateValue, formatString) + 
					inputString.Substring(rightBracketIndex + 1, inputString.Length - rightBracketIndex - 1);
			}
			else
			{
				return inputString;
			}
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
	}
}
