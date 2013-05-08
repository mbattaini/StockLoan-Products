// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class PositionAgent : MarshalByRefObject, IPosition
	{
		public event DealEventHandler DealEvent;
		public event ContractEventHandler ContractEvent;
		public event RecallEventHandler  RecallEvent;
		public event ProcessStatusEventHandler ProcessStatusEvent;
		public event BankLoanActivityEventHandler  BankLoanActivityEvent;		

		private string dbCnStr;

		private SubstitutionAgent substitutionAgent;
		private Anetics.Process.IProcess processAgent;		

		private Anetics.Process.HeartbeatEventWrapper heartbeatEventWrapper;
		private Anetics.Process.ProcessStatusEventWrapper processStatusEventWrapper;
		
		private ArrayList processStatusEventArgsArray;		

		private bool processStatusIsReady = false;		

		private double heartbeatInterval;
		private string heartbeatAlert = "";

		private DateTime heartbeatTimestamp = DateTime.UtcNow;
		private Anetics.Process.HeartbeatStatus heartbeatStatus = Anetics.Process.HeartbeatStatus.Unknown;		

		private bool faxEnabled;

		private delegate void FaxStatusUpdateDelegate();
		
		public SubstitutionAgent LocalSubstitutionAgent
		{
			get
			{
				return substitutionAgent;
			}

			set
			{
				substitutionAgent = value;
			}
		}


		public PositionAgent(string dbCnStr, ref Anetics.Process.IProcess processAgent, ref SubstitutionAgent substitutionAgent)
		{
			this.dbCnStr				= dbCnStr;
			this.processAgent		= processAgent;	
			this.substitutionAgent = substitutionAgent;

			heartbeatEventWrapper = new Anetics.Process.HeartbeatEventWrapper(); 
			heartbeatEventWrapper.HeartbeatEvent += new Anetics.Process.HeartbeatEventHandler(HeartbeatOnEvent);

			processStatusEventWrapper = new Anetics.Process.ProcessStatusEventWrapper();
			processStatusEventWrapper.ProcessStatusEvent += new Anetics.Process.ProcessStatusEventHandler(ProcessStatusOnEvent);

			processStatusEventArgsArray = new ArrayList();		
			
			try
			{
				faxEnabled = bool.Parse(Standard.ConfigValue("WebServiceFaxEnabled", "False"));
				Log.Write("Faxing for recalls enabled: " + faxEnabled.ToString() + ". [PositionAgent.PositionAgent]", 2);

				heartbeatInterval = double.Parse(Standard.ConfigValue("HeartbeatInterval", "20000"));        
				Log.Write("Running with heartbeat interval of " + heartbeatInterval + " milliseconds. [PositionAgent.PositionAgent]", 2);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.PositionAgent]", Log.Error, 1);
			} 

			ProcessStatusIsReady = true;
		}

		public void ProcessAgentConnect()
		{
			try
			{
				processAgent.HeartbeatEvent -= new Anetics.Process.HeartbeatEventHandler(heartbeatEventWrapper.DoEvent);
				processAgent.HeartbeatEvent += new Anetics.Process.HeartbeatEventHandler(heartbeatEventWrapper.DoEvent);

				processAgent.ProcessStatusEvent -= new Anetics.Process.ProcessStatusEventHandler(processStatusEventWrapper.DoEvent);
				processAgent.ProcessStatusEvent += new Anetics.Process.ProcessStatusEventHandler(processStatusEventWrapper.DoEvent);
		
				HeartbeatOnEvent(new Anetics.Process.HeartbeatEventArgs(Anetics.Process.HeartbeatStatus.Normal, ""));		
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ProcessAgentConnect]", Log.Error, 1);
			}
		}

		public void ProcessAgentDisconnect()
		{
			if (processAgent == null)
			{
				Log.Write("The process agent has not been enabled for this session. [PositionAgent.ProcessAgentDisconnect]", 2);
				return;
			}

			try
			{
				processAgent.HeartbeatEvent -= new Anetics.Process.HeartbeatEventHandler(heartbeatEventWrapper.DoEvent);
				processAgent.ProcessStatusEvent -= new Anetics.Process.ProcessStatusEventHandler(processStatusEventWrapper.DoEvent);		
			}
			catch
			{
				Log.Write("The process agent server is unreachable. [PositionAgent.ProcessAgentDisconnect]", Log.Warning, 1);
			}
		}

		public bool BlockedSecId(string secId)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  		
			bool blocked = false;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBlockedSecId", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        								
				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;   			
				
				SqlParameter paramBlocked = dbCmd.Parameters.Add("@Blocked", SqlDbType.Bit);
				paramBlocked.Direction = ParameterDirection.Output;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			
				blocked = bool.Parse(paramBlocked.Value.ToString());
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BlockedSecId]", Log.Error, 1);
				blocked = false;
				throw;
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}

			return blocked;
		}
		
		
		public DataSet AccountPositionGet(string secId, bool isActive)
		{
			return AccountPositionGet(Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 0),
				Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 1),
				Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 2),
				Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 3),				
				Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 4), 
				secId, 
				isActive);
		}

		public DataSet AccountPositionGet(string firm, string locMemo, string accountType,string accountNumber,  string currencyCode, string secId, bool isActive)
		{		
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spAccountPositionGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				if (!firm.Equals(""))
				{
					SqlParameter paramFirm = dbCmd.Parameters.Add("@Firm", SqlDbType.Char, 2);
					paramFirm.Value = firm;   			
				}

				if (!locMemo.Equals(""))
				{
					SqlParameter paramLocMemo = dbCmd.Parameters.Add("@LocMemo", SqlDbType.Char, 1);
					paramLocMemo.Value = locMemo;   			
				}
				
				if (!accountType.Equals(""))
				{
					SqlParameter paramAccountType = dbCmd.Parameters.Add("@AccountType", SqlDbType.Char, 1);
					paramAccountType.Value = accountType;   			
				}
				
				if (!accountNumber.Equals(""))
				{
					SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.Char, 2);
					paramAccountNumber.Value = accountNumber;   			
				}

				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;   			
				}
							
				if (!currencyCode.Equals(""))
				{
					SqlParameter paramCurrencyCode = dbCmd.Parameters.Add("@CurrencyCode", SqlDbType.VarChar, 3);
					paramCurrencyCode.Value = currencyCode;   			
				}		

				SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
				paramIsActive.Value = isActive;   							
				  
				dataAdapter = new SqlDataAdapter(dbCmd);  
				dataAdapter.Fill(dataSet, "AccountPosition");
        
				dataSet.Tables["AccountPosition"].PrimaryKey = new DataColumn[4]
			{
				dataSet.Tables["AccountPosition"].Columns["Firm"],
				dataSet.Tables["AccountPosition"].Columns["LocMemo"],
				dataSet.Tables["AccountPosition"].Columns["AccountType"],
				dataSet.Tables["AccountPosition"].Columns["AccountNumber"]
			};

			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.AccountPositionGet]", Log.Error, 1);
				throw;
			}

			return dataSet;  			
		}	

		public DataSet AccountsGet(string groupCode)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spAccountsByGroupCodeGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				if (!groupCode.Equals(""))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
					paramGroupCode.Value = groupCode;   			
				}

				dataAdapter = new SqlDataAdapter(dbCmd);  
				dataAdapter.Fill(dataSet, "Accounts");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.AccountsGet]", Log.Error, 1);
				throw;
			}

			return dataSet;  	
		}
		
		
		public DataSet AccountsGet(int utcOffset)
		{
			return  AccountsGet(Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 0),
				Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 1),
				Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 2),
				Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 3),				
				Tools.SplitItem(Standard.ConfigValue("AccountInformation"), ";", 4),
				utcOffset);
		}

		public DataSet AccountsGet(string firm, string locMemo, string accountType, string accountNumber, string currencyCode, int utcOffset)
		{		
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spAccountsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				if (!firm.Equals(""))
				{
					SqlParameter paramFirm = dbCmd.Parameters.Add("@Firm", SqlDbType.Char, 2);
					paramFirm.Value = firm;   			
				}

				if (!locMemo.Equals(""))
				{
					SqlParameter paramLocMemo = dbCmd.Parameters.Add("@LocMemo", SqlDbType.Char, 1);
					paramLocMemo.Value = locMemo;   			
				}
				
				if (!accountType.Equals(""))
				{
					SqlParameter paramAccountType = dbCmd.Parameters.Add("@AccountType", SqlDbType.Char, 1);
					paramAccountType.Value = accountType;
				}
				
				if (!accountNumber.Equals(""))
				{
					SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.Char, 2);
					paramAccountNumber.Value = accountNumber;   			
				}
				
				if (!currencyCode.Equals(""))
				{
					SqlParameter paramCurrencyCode = dbCmd.Parameters.Add("@CurrencyCode", SqlDbType.VarChar, 3);
					paramCurrencyCode.Value = currencyCode;   			
				}		

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;   			
				  
				dataAdapter = new SqlDataAdapter(dbCmd);  
				dataAdapter.Fill(dataSet, "Accounts");
 			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.AccountsGet]", Log.Error, 1);
				throw;
			}

			return dataSet;  			
		}	
		
		public void AccountSet(string firm, string locMemo, string accountType, string accountNumber, string currencyCode, string actUserId, bool isActive)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;		

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spAccountSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				if (!firm.Equals(""))
				{
					SqlParameter paramFirm = dbCmd.Parameters.Add("@Firm", SqlDbType.Char, 2);
					paramFirm.Value = firm;   			
				}

				if (!locMemo.Equals(""))
				{
					SqlParameter paramLocMemo = dbCmd.Parameters.Add("@LocMemo", SqlDbType.Char, 1);
					paramLocMemo.Value = locMemo;   			
				}
				
				if (!accountType.Equals(""))
				{
					SqlParameter paramAccountType = dbCmd.Parameters.Add("@AccountType", SqlDbType.Char, 1);
					paramAccountType.Value = accountType;
				}
				
				if (!accountNumber.Equals(""))
				{
					SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.Char, 8);
					paramAccountNumber.Value = accountNumber;   			
				}				
				
				if (!currencyCode.Equals(""))
				{
					SqlParameter paramCurrencyCode = dbCmd.Parameters.Add("@CurrencyCode", SqlDbType.VarChar, 3);
					paramCurrencyCode.Value = currencyCode;   			
				}				

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;
				
				SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
				paramIsActive.Value = isActive;   							  				
			
				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.AccountSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}

		public string	AutoBorrowListSend(string bizDate, string bookGroup, string listName)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spAutoBorrowListItemsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        dataAdapter = new SqlDataAdapter(dbCmd);  
				
				if (!bizDate.Equals(""))
				{
					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = bizDate;
				}

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);				
				paramBookGroup.Value = bookGroup;

				SqlParameter paramListName = dbCmd.Parameters.Add("@ListName", SqlDbType.VarChar, 10);
				paramListName.Value = listName;
			
				dataAdapter.Fill(dataSet, "AutoBorrowListItems"); 
			
				foreach (DataRow dataRow in dataSet.Tables["AutoBorrowListItems"].Rows)
				{
					processAgent.AutoBorrowOrder(
						dataRow["BookGroup"].ToString(),
						dataRow["Books"].ToString(),
						dataRow["SecId"].ToString(),
						long.Parse(dataRow["Quantity"].ToString()),
						dataRow["CollateralCode"].ToString(),
						dataRow["Comment"].ToString(),
						decimal.Parse(dataRow["RateMin"].ToString()),
						dataRow["RateMinCode"].ToString(),
						dataRow["QuantityMin"].ToString(),
						dataRow["PriceMax"].ToString(),
						dataRow["WaitTime"].ToString(),
						bool.Parse(dataRow["IncomeTracked"].ToString()),
						decimal.Parse(dataRow["DivRate"].ToString()),
						bool.Parse(dataRow["BookContract"].ToString()),
						dataRow["BatchCode"].ToString(),
						dataRow["PoolCode"].ToString(),
						dataRow["MarginCode"].ToString(),
						decimal.Parse(dataRow["Margin"].ToString()),
						dataRow["ActUserId"].ToString());
				}			
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.AutoBorrowListSend]", Log.Error, 1);
				throw;
			}
			
			return "S";
		}
		
		public DataSet	AutoBorrowDataGet(string bizDate, short utcOffset, string userId, string functionPath)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spAutoBorrowListsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				if (!bizDate.Equals(""))
				{
					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = bizDate;
				}

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);				
				paramUtcOffset.Value = utcOffset;
        
				dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "AutoBorrowLists");

				dbCmd.CommandText = "spAutoBorrowItemsGet";       
				dataAdapter.Fill(dataSet, "AutoBorrowItems"); 

				dbCmd.Parameters.Remove(paramUtcOffset);
				
				dbCmd.CommandText = "spAutoBorrowResultsGet";        
				dataAdapter.Fill(dataSet, "AutoBorrowResults"); 
				
				dbCmd.CommandText = "spBookGroupGet";
        
				SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);      
				paramUserId.Value = userId;
        
				SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);         
				paramFunctionPath.Value = functionPath;    

				dataAdapter.Fill(dataSet, "BookGroups");    
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.AutoBorrowDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}
		
		public DataSet	AutoBorrowItemsGet(int utcOffset, string bizDate)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spAutoBorrowItemsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				if (!bizDate.Equals(""))
				{
					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = bizDate;   			
				}

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;
   			  
				dataAdapter = new SqlDataAdapter(dbCmd);  
				dataAdapter.Fill(dataSet, "AutoBorrowItems");
        
				dataSet.Tables["AutoBorrowItems"].PrimaryKey = new DataColumn[4]
			{
				dataSet.Tables["AutoBorrowItems"].Columns["BizDate"],
				dataSet.Tables["AutoBorrowItems"].Columns["BookGroup"],
				dataSet.Tables["AutoBorrowItems"].Columns["ListName"],
				dataSet.Tables["AutoBorrowItems"].Columns["SecId"]
			};

			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.AutoBorrowItemsGet]", Log.Error, 1);
				throw;
			}

			return dataSet;  			
		}

		public void	AutoBorrowItemSet(
			string bookGroup, 
			string listName, 			
			string secId, 
			string quantity, 
			string collateralCode, 
			string rateMin, 
			string rateMinCode, 
			string priceMax, 
			bool incomeTracked, 
			string margin, 
			string marginCode, 
			string divfRate,
			string comment, 
			string actUserId)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spAutoBorrowItemSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);				
				paramBookGroup.Value = bookGroup;

				SqlParameter paramListName = dbCmd.Parameters.Add("@ListName", SqlDbType.VarChar, 10);
				paramListName.Value = listName;
			
				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;
        
				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
				paramQuantity.Value = quantity;
        
				SqlParameter paramQuantityMin = dbCmd.Parameters.Add("@QuantityMin", SqlDbType.BigInt);
				paramQuantityMin.Value = (long.Parse(quantity) / 2);

				SqlParameter paramCollateralCode = dbCmd.Parameters.Add("@CollateralCode", SqlDbType.VarChar, 1);      
				paramCollateralCode.Value = collateralCode;				

				SqlParameter paramRateMin = dbCmd.Parameters.Add("@RateMin", SqlDbType.Decimal);
				paramRateMin.Value = rateMin;
				
				SqlParameter paramRateMinCode = dbCmd.Parameters.Add("@RateMinCode", SqlDbType.Char, 1);
				paramRateMinCode.Value = rateMinCode;

				SqlParameter paramPriceMax = dbCmd.Parameters.Add("@PriceMax", SqlDbType.Decimal);
				paramPriceMax.Value = priceMax;

				SqlParameter paramIncomeTracked = dbCmd.Parameters.Add("@IncomeTracked", SqlDbType.Bit);
				paramIncomeTracked.Value = incomeTracked;

				SqlParameter paramMargin = dbCmd.Parameters.Add("@Margin", SqlDbType.Decimal);
				paramMargin.Value = margin;
				
				SqlParameter paramMarginCode = dbCmd.Parameters.Add("@MarginCode", SqlDbType.Char, 1);
				paramMarginCode.Value = marginCode;
        
				SqlParameter paramDivRate = dbCmd.Parameters.Add("@DivRate", SqlDbType.Decimal);
				paramDivRate.Value = divfRate;
        
				SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 20);
				paramComment.Value = comment;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.AutoBorrowItemSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}
		
		public DataSet	AutoBorrowListsGet(int utcOffSet, string bizDate)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spAutoBorrowListsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				if (!bizDate.Equals(""))
				{
					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = bizDate;   			
				}

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffSet;
   			  
				dataAdapter = new SqlDataAdapter(dbCmd);  
				dataAdapter.Fill(dataSet, "AutoBorrowLists");
        
				dataSet.Tables["AutoBorrowLists"].PrimaryKey = new DataColumn[3]
			{
				dataSet.Tables["AutoBorrowLists"].Columns["BizDate"],
				dataSet.Tables["AutoBorrowLists"].Columns["BookGroup"],
				dataSet.Tables["AutoBorrowLists"].Columns["ListName"]
			};
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.AutoBorrowListsGet]", Log.Error, 1);
				throw;
			}

			return dataSet;  	
		}

		public void AutoBorrowListSet(
			string bookGroup, 
			string listName, 
			string books, 
			string waitTime, 
			bool bookContract, 
			string batchCode, 
			string poolCode, 
			int itemCount, 
			int filled, 
			string listStatus, 
			string actUserId)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spAutoBorrowListSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);				
				paramBookGroup.Value = bookGroup;

				SqlParameter paramListName = dbCmd.Parameters.Add("@ListName", SqlDbType.VarChar, 10);
				paramListName.Value = listName;

				SqlParameter paramBooks = dbCmd.Parameters.Add("@Books", SqlDbType.VarChar, 255);
				paramBooks.Value = books;

				SqlParameter paramWaitTime = dbCmd.Parameters.Add("@WaitTime", SqlDbType.Int);
				paramWaitTime.Value = waitTime;
        
				SqlParameter paramBookContract = dbCmd.Parameters.Add("@BookContract", SqlDbType.Bit);
				paramBookContract.Value = bookContract;
        
				SqlParameter paramBatchCode = dbCmd.Parameters.Add("@BatchCode", SqlDbType.Char, 1);      
				paramBatchCode.Value = batchCode;				

				SqlParameter paramPoolCode = dbCmd.Parameters.Add("@PoolCode", SqlDbType.Char, 1);      
				paramPoolCode.Value = poolCode;
				
				SqlParameter paramItemCount = dbCmd.Parameters.Add("@ItemCount", SqlDbType.Int);
				paramItemCount.Value = itemCount;

				SqlParameter paramListStatus = dbCmd.Parameters.Add("@ListStatus", SqlDbType.Char, 1);
				paramListStatus.Value = listStatus;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.AutoBorrowListSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}
		
		public DataSet	AutoBorrowResultsGet(int utcOffSet, string bizDate)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spAutoBorrowResultsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				if (!bizDate.Equals(""))
				{
					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = bizDate;   			
				}
				  
				dataAdapter = new SqlDataAdapter(dbCmd);  
				dataAdapter.Fill(dataSet, "AutoBorrowResults");
        
				dataSet.Tables["AutoBorrowResults"].PrimaryKey = new DataColumn[4]
			{
				dataSet.Tables["AutoBorrowResults"].Columns["BizDate"],
				dataSet.Tables["AutoBorrowResults"].Columns["BookGroup"],
				dataSet.Tables["AutoBorrowResults"].Columns["ListName"],
				dataSet.Tables["AutoBorrowResults"].Columns["SecId"]
			};

			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.AutoBorrowListsGet]", Log.Error, 1);
				throw;
			}

			return dataSet;  
		}

		public DataSet BankLoanActivityGet(int utcOffset)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBankLoanActivityGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;
   			  
				dataAdapter = new SqlDataAdapter(dbCmd);  
				dataAdapter.Fill(dataSet, "Activity");
        
				dataSet.Tables["Activity"].PrimaryKey = new DataColumn[3]
			{
				dataSet.Tables["Activity"].Columns["BookGroup"],
				dataSet.Tables["Activity"].Columns["Book"],
				dataSet.Tables["Activity"].Columns["ProcessId"]			  
			};

			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankLoanDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;  
		}
	  
		public DataSet BankLoanReportsGet(int utcOffset)
		{			
			SqlConnection dbCn = null;
			SqlDataAdapter dataAdapter = null;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);

				SqlCommand dbCmd = new SqlCommand("spBankLoanReportsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;
				
				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "BankLoanReports");				
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankLoanReportsGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}
		
		public void BankLoanReportSet(
			string bookGroup, 
			string reportName,
			string reportType,
			string reportHost, 
			string reportHostUserId, 
			string reportHostPassword, 
			string reportPath,
			string fileLoadDate,
			string actUserId)
		{
			SqlConnection dbCn = null;
			
			try
			{
				dbCn = new SqlConnection(dbCnStr);
				
				SqlCommand dbCmd = new SqlCommand("spBankLoanReportSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
							
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;
			
				SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
				paramReportName.Value = reportName;

				SqlParameter paramReportType = dbCmd.Parameters.Add("@ReportType", SqlDbType.Char, 1);
				if (!reportType.Equals(""))
				{				
					paramReportType.Value = reportType;
				}

				SqlParameter paramReportHost = dbCmd.Parameters.Add("@ReportHost", SqlDbType.VarChar, 50);
				if (!reportHost.Equals(""))
				{				
					paramReportHost.Value = reportHost;
				}
			
				SqlParameter paramReportHostUserId = dbCmd.Parameters.Add("@ReportHostUserId", SqlDbType.VarChar, 50);
				if (!reportHostUserId.Equals(""))
				{				
					paramReportHostUserId.Value = reportHostUserId;
				}

				SqlParameter paramReportHostPassword = dbCmd.Parameters.Add("@ReportHostPassword", SqlDbType.VarChar, 50);
				if (!reportHostPassword.Equals(""))
				{			
					paramReportHostPassword.Value = reportHostPassword;
				}

				SqlParameter paramReportpath = dbCmd.Parameters.Add("@Reportpath", SqlDbType.VarChar, 100);
				if (!reportPath.Equals(""))
				{				
					paramReportpath.Value = reportPath;
				}
				
				SqlParameter  paramFileLoadDate = dbCmd.Parameters.Add("@FileLoadDate", SqlDbType.DateTime);
				if (!fileLoadDate.Equals(""))
				{
					paramFileLoadDate.Value = fileLoadDate;
				}				

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				dbCn.Open();
				
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankLoanReportSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}
		
		public DataSet BankLoanReportsDataGet()
		{
			SqlConnection dbCn = null;
			SqlDataAdapter dataAdapter = null;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				SqlCommand dbCmd = new SqlCommand("spBankLoanReportsDataGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				
				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "BankLoanReportsData");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankLoanReportsDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}
		
		public DataSet	BankLoanReportsDataMaskGet(string bookGroup, string reportName, int utcOffset)
		{
			SqlConnection dbCn = null;
			SqlDataAdapter dataAdapter = null;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				SqlCommand dbCmd = new SqlCommand("spBankLoanReportsDataMaskSGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
										
				if (!bookGroup.Equals(""))
				{
					SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
					paramBookGroup.Value = bookGroup;			
				}

				if (!reportName.Equals(""))
				{
					SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
					paramReportName.Value = reportName;
				}

				SqlParameter paramUtcOffSet = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffSet.Value = utcOffset;
				
				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "BankLoanReportsDataMask");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankLoanReportsDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}
		
        public void	BankLoanReportsDataMaskSet(
			string bookGroup, 
			string reportName, 
			string headerFlag,
			string dataFlag,
			string trailerFlag,
			int reportNamePosition,
			int reportNameLength,
			int	reportDatePosition,
			int reportDateLength,
			int	secIdPosition,
			int	secIdLength,
			int quantityPosition,
			int quantityLength,
			int activityPosition,
			int activitylength,
			string ignoreItems,
			int lineLength,
			string actUserId)
		{
			SqlConnection dbCn = null;
			
			try
			{
				dbCn = new SqlConnection(dbCnStr);
				
				SqlCommand dbCmd = new SqlCommand("spBankLoanReportsDataMaskSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
							
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;
			
				SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
				paramReportName.Value = reportName;

				SqlParameter paramHeaderFlag = dbCmd.Parameters.Add("@HeaderFlag", SqlDbType.VarChar, 10);
				paramHeaderFlag.Value = headerFlag;

				SqlParameter paramDataFlag = dbCmd.Parameters.Add("@DataFlag", SqlDbType.VarChar, 10);
				paramDataFlag.Value = dataFlag;

				SqlParameter paramTrailerFlag = dbCmd.Parameters.Add("@TrailerFlag", SqlDbType.VarChar, 10);
				paramTrailerFlag.Value = trailerFlag;
				
				SqlParameter paramReportNamePosition = dbCmd.Parameters.Add("@ReportNamePosition", SqlDbType.Int);
				paramReportNamePosition.Value = reportNamePosition;

				SqlParameter paramReportNameLength = dbCmd.Parameters.Add("@ReportNameLength", SqlDbType.Int);
				paramReportNameLength.Value = reportNameLength;

				SqlParameter paramReportDatePosition = dbCmd.Parameters.Add("@ReportDatePosition", SqlDbType.Int);
				paramReportDatePosition.Value = reportDatePosition;

				SqlParameter paramReportDateLength = dbCmd.Parameters.Add("@ReportDateLength", SqlDbType.Int);
				paramReportDateLength.Value = reportDateLength;
				
				SqlParameter paramSecIdPosition = dbCmd.Parameters.Add("@SecIdPosition", SqlDbType.Int);
				paramSecIdPosition.Value = secIdPosition;

				SqlParameter paramSecIdLength = dbCmd.Parameters.Add("@SecIdLength", SqlDbType.Int);
				paramSecIdLength.Value = secIdLength;

				SqlParameter paramQuantityPosition = dbCmd.Parameters.Add("@QuantityPosition", SqlDbType.Int);
				paramQuantityPosition.Value = quantityPosition;

				SqlParameter paramQuantityLength = dbCmd.Parameters.Add("@QuantityLength", SqlDbType.Int);
				paramQuantityLength.Value = quantityLength;

				SqlParameter paramActivityPosition = dbCmd.Parameters.Add("@ActivityPosition", SqlDbType.Int);
				paramActivityPosition.Value = activityPosition;

				SqlParameter paramActivityLength = dbCmd.Parameters.Add("@ActivityLength", SqlDbType.Int);
				paramActivityLength.Value = activitylength;

				SqlParameter paramIgnoreItems = dbCmd.Parameters.Add("@IgnoreItems", SqlDbType.VarChar, 100);
				paramIgnoreItems.Value = ignoreItems;

				SqlParameter paramLineLength = dbCmd.Parameters.Add("@LineLength", SqlDbType.Int);
				paramLineLength.Value = lineLength;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				dbCn.Open();
				
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankLoanReportsDataMaskSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}

		public DataSet BankLoanPledgeSummaryGet(string bizDate)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBankLoanPledgeSummaryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;    
        dbCmd.CommandTimeout = int.Parse(KeyValue.Get("BankLoanPledgeSummaryTimeOut", "600", dbCnStr));        
        			       
				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				dataAdapter = new SqlDataAdapter(dbCmd);  
				dataAdapter.Fill(dataSet, "BankLoanPledgeSummary");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankLoanPledgeSummaryGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}

		public DataSet BankLoanReleaseSummaryGet(string secId)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBankLoanReleaseSummaryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        			       			  			  
				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}

				dataAdapter = new SqlDataAdapter(dbCmd);  
				dataAdapter.Fill(dataSet, "BankLoanReleaseSummary");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankLoanReleaseSummaryGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}
	  

		public DataSet BankLoanDataGet(short utcOffset, string userId, string functionPath)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBanksGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;
        
				dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "Banks");
        
				dbCmd.CommandText = "spBankLoanActivityGet";
				dataAdapter.Fill(dataSet, "Activity");
        
				dataSet.Tables["Activity"].PrimaryKey = new DataColumn[3]
			{
				dataSet.Tables["Activity"].Columns["BookGroup"],
				dataSet.Tables["Activity"].Columns["Book"],
				dataSet.Tables["Activity"].Columns["ProcessId"]			  
			};

				dbCmd.CommandText = "spBankLoansGet";
				dataAdapter.Fill(dataSet, "Loans");

				dbCmd.Parameters.Remove(paramUtcOffset);

				dbCmd.CommandText = "spBankLoanTypeGet";
				dataAdapter.Fill(dataSet, "LoanTypes");
        
				dbCmd.CommandText = "spBankLoanActivityTypeGet";
				dataAdapter.Fill(dataSet, "ActivityTypes");
			    
				dbCmd.CommandText = "spBookGroupGet";
        
				SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);      
				paramUserId.Value = userId;
        
				SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);         
				paramFunctionPath.Value = functionPath;    

				dataAdapter.Fill(dataSet, "BookGroups");                                         
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankLoanDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;    
		}

		public DataSet BankLoansGet(int utcOffset)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBankLoansGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;
            
				dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "Loans");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankLoansGet]", Log.Error, 1);
				throw;
			}

			return dataSet;   
		}
    
		public void BankLoanSet(string bookGroup,  
			string book, 
			string loanDate, 
			string loanType,
			string activityType,
			string hairCut, 
			string spMin,
			string moodyMin,
			string priceMin, 
			string loanAmount, 
			string comment, 
			string actUserId,
			bool   isActive)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBankLoanSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				SqlParameter paramAccount = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
				paramAccount.Value = book;
        
				SqlParameter paramLoanDate = dbCmd.Parameters.Add("@LoanDate", SqlDbType.DateTime);			
				paramLoanDate.Value = loanDate;
        
				
				SqlParameter paramLoanType = dbCmd.Parameters.Add("@LoanType", SqlDbType.Char, 1);
				
				if (!loanType.Equals(""))
				{
					paramLoanType.Value = loanType;
				}
        
				SqlParameter paramActivityType = dbCmd.Parameters.Add("@ActivityType", SqlDbType.Char, 1);
				if (!activityType.Equals(""))
				{
					paramActivityType.Value = activityType;
				}

				SqlParameter paramHairCut = dbCmd.Parameters.Add("@HairCut", SqlDbType.Decimal);
				if (!hairCut.Equals(""))
				{
					paramHairCut.Value = hairCut;
				}

				SqlParameter paramSp = dbCmd.Parameters.Add("@SpMin", SqlDbType.VarChar, 5);
				if (!spMin.Equals(""))
				{
					paramSp.Value = spMin;
				}
       
				SqlParameter paramMoody = dbCmd.Parameters.Add("@MoodyMin", SqlDbType.VarChar, 5);
				if (moodyMin.Equals(""))
				{
					paramMoody.Value = moodyMin;
				}
       
				SqlParameter paramPriceMin = dbCmd.Parameters.Add("@PriceMin", SqlDbType.Float);
				if (!priceMin.Equals(""))
				{
					paramPriceMin.Value = priceMin;
				}

				SqlParameter paramLoanAmount = dbCmd.Parameters.Add("@LoanAmount", SqlDbType.Decimal);
				if (!loanAmount.Equals(""))
				{
					paramLoanAmount.Value = decimal.Parse(loanAmount).ToString();
				}

				SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
				paramComment.Value = comment;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
				paramIsActive.Value = isActive;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankLoanSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}
    
		public void BankSet(string bookGroup, 
			string book, 
			string name, 
			string contact, 
			string phone, 
			string fax, 
			string actUserId, 
			bool isActive)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
        
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBankSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				SqlParameter paramAccount = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
				paramAccount.Value = book;
        
				SqlParameter paramName = dbCmd.Parameters.Add("@Name", SqlDbType.VarChar, 25);
				paramName.Value = name;
        
				SqlParameter paramContact = dbCmd.Parameters.Add("@Contact", SqlDbType.VarChar, 25);
				paramContact.Value = contact;

				SqlParameter paramPhone = dbCmd.Parameters.Add("@Phone", SqlDbType.VarChar, 14);
				paramPhone.Value = phone;
        
				SqlParameter paramFax = dbCmd.Parameters.Add("@Fax", SqlDbType.VarChar, 14);
				paramFax.Value = fax;
        
				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;
        
				SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
				paramIsActive.Value = isActive;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}
    
		private DataSet BoxPositionLookup(string secId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spBoxPositionGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDate;
      	
				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "BoxPositionItem");

                dataSet.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ServiceAgent.SecMasterLookup]", Log.Error, 1);
			}

			return dataSet;
		}
		
		public void BankLoanPledgeSet(
			string bookGroup, 
			string book,	
			string loanDate, 		  
			string processId, 
			string loanType,
			string activityType,
			string secId, 
			string quantity, 
			string flag,
			string status, 
			string actUserId)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			SqlDataReader dataReader = null;
 
			DataSet dataSet = new DataSet();
			BankLoanActivityEventArgs bankLoanActivityEventArgs = null;

			if (!secId.Equals(""))
			{
				Log.Write("User " + actUserId + " is setting pledge data in " + bookGroup + " for " + quantity + " of " + secId + ". [PositionAgent.BankLoanPledgeSet]", 3);
			}

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBankLoanActivitySet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        			 			  
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;
        
				if (!book.Equals(""))
				{
					SqlParameter paramAccount = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
					paramAccount.Value = book;
				}
			  
				if (!loanDate.Equals(""))
				{
					SqlParameter paramLoanDate = dbCmd.Parameters.Add("@LoanDate", SqlDbType.DateTime);        
					paramLoanDate.Value = loanDate;
				}       			  
			  
				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.Char, 16);
				paramProcessId.Value = (processId.Equals("")) ? Standard.ProcessId() : processId;        
				
				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.Char, 9);
					paramSecId.Value = secId;
				}

				if (!quantity.Equals(""))
				{
					SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
					paramQuantity.Value = quantity;
				}

				SqlParameter paramFlag = dbCmd.Parameters.Add("@Flag", SqlDbType.Char, 1);

				if (!flag.Equals(""))
				{				  
					paramFlag.Value = flag;
				}

				SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.Char, 2);

				if (!status.Equals(""))
				{				  
					paramStatus.Value = status;
				}

				if (!actUserId.Equals(""))
				{
					SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
					paramActUserId.Value = actUserId;
				}
 
				dbCn.Open();
				dataReader = dbCmd.ExecuteReader();
      
				while (dataReader.Read()) // Expect one row.
				{
					bankLoanActivityEventArgs = new BankLoanActivityEventArgs(
						dataReader.GetValue(0).ToString(),
						dataReader.GetValue(1).ToString(),
						dataReader.GetValue(2).ToString(),
						dataReader.GetValue(3).ToString(),
						dataReader.GetValue(4).ToString(),
						dataReader.GetValue(5).ToString(),
						long.Parse(dataReader.GetValue(6).ToString()),
						decimal.Parse(dataReader.GetValue(7).ToString()),					  
						dataReader.GetValue(8).ToString(),
						dataReader.GetValue(9).ToString(),
						dataReader.GetValue(10).ToString(),
						dataReader.GetValue(11).ToString());
				}		

				dataReader.Close();

				if (bankLoanActivityEventArgs.Status.Equals("PR") && !status.Equals(""))
				{
					try
					{
						string recordType, hypothecation, loanPurpose;

						switch (activityType.ToUpper())
						{
							case "F":
								recordType = KeyValue.Get("BankLoanPledgeFreeRecordTypeCode", "001", dbCnStr);
								break;
					
							case "V":
								recordType = KeyValue.Get("BankLoanPledgeValuedRecordTypeCode", "002", dbCnStr);
								break;

							default:
								throw new Exception("Invalid ActivityType: " + activityType);								
						}

						switch (loanType.ToUpper())
						{
							case "C":
								hypothecation = KeyValue.Get("BankLoanCustomerHypothecationCode", "1", dbCnStr);						
								break;

							case "S":
							case "F":
								hypothecation = KeyValue.Get("BankLoanFirmHypothecationCode", "3", dbCnStr);						
								break;

							default:
								throw new Exception("Invalid LoanType: " + loanType);								
						}
					
						loanPurpose = KeyValue.Get("BankLoanPledgeLoanPurposeCode", "1", dbCnStr);
				
						
						
							processAgent.BankLoanPledgeRelease(
								bankLoanActivityEventArgs.ProcessId,
								bankLoanActivityEventArgs.BookGroup,
								bankLoanActivityEventArgs.Book,
								bankLoanActivityEventArgs.LoanDate,
								recordType,
								bankLoanActivityEventArgs.SecId,						  						  
								bankLoanActivityEventArgs.Quantity,						  
								bankLoanActivityEventArgs.Amount,
								loanPurpose,
								" ",
								hypothecation,
								KeyValue.Get("BankLoanPreventPendIndicator", " ", dbCnStr),
								" ",
								KeyValue.Get("BankLoanPledgeIpoIndicator", "N", dbCnStr),
								KeyValue.Get("BankLoanPtaIndicator", " ", dbCnStr),
								" ",
								" ",
								" ",
								" ",
								actUserId);						
					}				
					catch (Exception e)
					{					 
						paramStatus.Value = "PF";
						bankLoanActivityEventArgs.Status = "PF";						
				  
						dbCmd.ExecuteNonQuery();		

						Log.Write(e.Message + " [PositionAgent.BankLoanPledgeSet]", Log.Error, 1);						
					}
				}								  			  
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankLoanPledgeSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}

			BankLoanActivityEventHandler bankLoanActivityEventHandler = new BankLoanActivityEventHandler(BankLoanActivityEventInvoke);
			bankLoanActivityEventHandler.BeginInvoke(bankLoanActivityEventArgs, null, null);
		}
    
		public void BankLoanReleaseSet(
			string bookGroup, 		  
			string book, 
			string processId, 
			string loanDate, 
			string loanType, 
			string activityType,
			string secId, 
			string quantity, 
			string flag,
			string status, 
			string actUserId)	  
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			SqlDataReader dataReader = null;
 
			DataSet dataSet = new DataSet();
			BankLoanActivityEventArgs bankLoanActivityEventArgs = null;
		  
			if (!secId.Equals(""))
			{
				Log.Write("User " + actUserId + " is setting release data in " + bookGroup + " for " + quantity + " of " + secId + ". [PositionAgent.BankLoanPledgeSet]", 3);
			}

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBankLoanActivitySet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
        
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;
        
				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.Char, 16);
				paramProcessId.Value = (processId.Equals("")) ? Standard.ProcessId() : processId;
        
				if (!loanDate.Equals(""))
				{
					SqlParameter paramLoanDate = dbCmd.Parameters.Add("@LoanDate", SqlDbType.DateTime);        
					paramLoanDate.Value = loanDate;
				}       		

				if (!book.Equals(""))
				{
					SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.Char, 4);
					paramBook.Value = book;
				}

				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.Char, 9);
					paramSecId.Value = secId;
				}

				if (!quantity.Equals(""))
				{
					SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
					paramQuantity.Value = quantity;
				}
   
				SqlParameter paramFlag = dbCmd.Parameters.Add("@Flag", SqlDbType.Char, 1);

				if (!flag.Equals(""))
				{				  
					paramFlag.Value = flag;
				}

				SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.Char, 2);

				if (!status.Equals(""))
				{				  
					paramStatus.Value = status;
				}

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;
                
				dbCn.Open();
				dataReader = dbCmd.ExecuteReader();
      
				while (dataReader.Read()) // Expect one row.
				{
					bankLoanActivityEventArgs = new BankLoanActivityEventArgs(
						dataReader.GetValue(0).ToString(),
						dataReader.GetValue(1).ToString(),
						dataReader.GetValue(2).ToString(),
						dataReader.GetValue(3).ToString(),
						dataReader.GetValue(4).ToString(),
						dataReader.GetValue(5).ToString(),
						long.Parse(dataReader.GetValue(6).ToString()),
						decimal.Parse(dataReader.GetValue(7).ToString()),					  
						dataReader.GetValue(8).ToString(),
						dataReader.GetValue(9).ToString(),
						dataReader.GetValue(10).ToString(),
						dataReader.GetValue(11).ToString());
				}
		  
				dataReader.Close();
			
				if (bankLoanActivityEventArgs.Status.Equals("RR") && !status.Equals(""))
				{
					try
					{
						string recordType, releaseType;

						switch (activityType.ToUpper())
						{
							case "F":
								recordType = KeyValue.Get("BankLoanReleaseFreeRecordTypeCode", "003", dbCnStr);						  
								break;
					
							case "V":
								recordType = KeyValue.Get("BankLoanReleaseValuedRecordTypeCode", "004", dbCnStr);						  
								break;
							default:
								throw new Exception("Invalid ActivityType: " + activityType);						  
						}												
				
						releaseType = KeyValue.Get("BankLoanReleaseTypeCode", "1", dbCnStr);

						processAgent.BankLoanPledgeRelease(
							bankLoanActivityEventArgs.ProcessId,
							bankLoanActivityEventArgs.BookGroup,
							bankLoanActivityEventArgs.Book,
							bankLoanActivityEventArgs.LoanDate,
							recordType,						  
							bankLoanActivityEventArgs.SecId,
							bankLoanActivityEventArgs.Quantity,						  
							bankLoanActivityEventArgs.Amount,
							" ",
							releaseType,
							" ",
							" ",
							" ",
							" ",
							" ",
							" ",
							" ",
							" ",
							" ",
							actUserId);
					}
					catch (Exception e)
					{
						paramStatus.Value = "RF";
						bankLoanActivityEventArgs.Status = "RF";	
			
						dbCmd.ExecuteNonQuery();

						Log.Write(e.Message + " [PositionAgent.BankLoanReleaseSet]", Log.Error, 1);						  						
					
						throw;
					}
				}							  			
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BankLoanReleaseSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}  			

			BankLoanActivityEventHandler bankLoanActivityEventHandler = new BankLoanActivityEventHandler(BankLoanActivityEventInvoke);
			bankLoanActivityEventHandler.BeginInvoke(bankLoanActivityEventArgs, null, null);
		}
    
	 
		private void BankLoanActivityEventInvoke(BankLoanActivityEventArgs bankLoanActivityEventArgs)
		{
			BankLoanActivityEventHandler bankLoanActivityEventHandler = null;

			string bankLoanIdentifier = bankLoanActivityEventArgs.BookGroup + ":" + bankLoanActivityEventArgs.Book + ":" + bankLoanActivityEventArgs.SecId + "[" + bankLoanActivityEventArgs.Symbol + "]:" +  bankLoanActivityEventArgs.Quantity.ToString() + " [PositionAgent.BankLoanActivityEventInvoke]";

			try
			{
				if (BankLoanActivityEvent == null)
				{
					Log.Write("Handling a bankloan event for " + bankLoanIdentifier + " with no delegates. [PositionAgent.BankLoanActivityEventInvoke]", 2);
				}
				else
				{
					int n = 0;

					Delegate[] eventDelegates = BankLoanActivityEvent.GetInvocationList();
					Log.Write("Handling a bankloan event for " + bankLoanIdentifier + " with " + eventDelegates.Length + " delegates. [PositionAgent.BankLoanActivityEventInvoke]", 2);
          
					foreach (Delegate eventDelegate in eventDelegates)
					{
						Log.Write("Invoking delegate [" + (++n) + "]. [PositionAgent.BankLoanActivityEventInvoke]", 3);

						try
						{
							bankLoanActivityEventHandler = (BankLoanActivityEventHandler)eventDelegate;
							bankLoanActivityEventHandler(bankLoanActivityEventArgs);
						}
						catch (System.Net.Sockets.SocketException)
						{
							BankLoanActivityEvent -= bankLoanActivityEventHandler;
							Log.Write("Bankloan event delegate [" + n + "] has been removed from the invocation list. [PositionAgent.BankLoanActivityEventInvoke]", 3);
						}
						catch (Exception e)
						{
							Log.Write(e.Message + " [PositionAgent.BankLoanActivityEventInvoke]", Log.Error, 1);
						}
					}

					Log.Write("Done invoking the bankloan event invocation list. [PositionAgent.BankLoanActivityEventInvoke]", 3);
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + ". [PositionAgent.BankLoanActivityEventInvoke]", Log.Error, 1);
			}
		}
    
		private DataSet ContractsModifiedGet(string bookGroup)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
  
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spContractsModifiedGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;                    
			
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;
			  
				dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "Contracts");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ContractsModifiedGet]", Log.Error, 1);
				throw;
			}

			return dataSet; 
		}
	  
		public bool FaxEnabled()
		{
			return faxEnabled;      
		}

		/*public void FaxSend(string recallId, string borrowFaxNumber, string jobId, string name, string subject, string notice, bool isCancel, string actUserId)
		{
			faxSendDelegate.BeginInvoke(recallId, borrowFaxNumber, jobId, name, subject, notice, isCancel, actUserId, null, null);
		}

		public void FaxStatusUpdate()
		{
			faxStatusUpdateDelegate.BeginInvoke(null, null);
		}
    
		private void FaxStatusUpdateDo()
		{
			string status = "";
			long updateCount = 0;
			DataSet dataSet = new DataSet();  
      
			SqlCommand dbCmd = null;
			SqlDataAdapter dataAdapter = null;
			SqlConnection dbCn = null;
			Services.WebService webService = null;
      
			if (!faxEnabled)
			{        
				Log.Write("A fax status update was requested where fax is not enabled. [PositionAgent.FaxStatusUpdateDo]", Log.Warning, 1);
				return;
			}
      
			Log.Write("A fax status update has been initiated. [PositionAgent.FaxStatusUpdateDo]", 3);

			try
			{        
				webService = new Services.WebService(        
					Standard.ConfigValue("WebServiceUserCode"), 
					Standard.ConfigValue("WebServicePassCode"),
					Standard.ConfigValue("WebServiceURI"));                    
      
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spRecallsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;        
        
				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDate;
        
				dataAdapter = new SqlDataAdapter(dbCmd);         
				dataAdapter.Fill(dataSet, "Recalls");                                           
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.FaxStatusUpdateDo]", Log.Error, 1);
				throw;
			}                  
      
			foreach(DataRow dataRow in dataSet.Tables["Recalls"].Select("FaxId <> '' AND FaxId <> '0' AND FaxStatus = 'Send In Process'"))
			{
				try
				{
					Log.Write("Checking: " + dataRow["FaxId"].ToString()  + " [PositionAgent.FaxStatusUpdateDo]", 3);
					status = webService.FaxStatusGet(long.Parse(dataRow["FaxId"].ToString()));
				}
				catch (Exception e)
				{
					status = e.Message;
				}
        
				if(!status.Equals(dataRow["FaxStatus"].ToString())) // Status has changed.
				{
					RecallSet(
						dataRow["RecallId"].ToString(),
						"",
						"",
						"Fax status: " + status,
						"",
						status,
						"",
						"",
						"ADMIN",
						"");
                    
					updateCount ++;         
				}
			}
         
			Log.Write("Fax status was updated on " + updateCount.ToString("#,##0") + " recalls. [PositionAgent.FaxStatusUpdateDo]", 2);      
		}
    
		private void FaxSendDo(string recallId, string borrowFaxNumber, string jobId, string name, string subject, string notice, bool isCancel, string actUserId)
		{
			string  newJobId = "";

			Log.Write("Sending a fax command for recall ID " + recallId + " with Cancel set to " + 
				isCancel.ToString() + ". [MedalistMain.FaxSendlDo]", 3);

			try
			{        
				Services.WebService webService = new Services.WebService(        
					Standard.ConfigValue("WebServiceUserCode"), 
					Standard.ConfigValue("WebServicePassCode"),
					Standard.ConfigValue("WebServiceURI"));                                                                      
     
				if (isCancel)
				{
					webService.FaxCancel(long.Parse(jobId));          
					RecallSet(recallId, "", "", "Pending fax cancelled", "", "Cancelled", "",  "", actUserId, "");                 
				}
				else
				{
					if (borrowFaxNumber.Equals(""))
					{          
						RecallSet(recallId, "", "", "Error: No fax number for borrower", "", "Error: No fax number for borrower", "",  "", actUserId, "");                           
					}
					else
					{
						newJobId = webService.FaxSend(borrowFaxNumber, name, subject, notice, UserEmailGet(actUserId)).ToString(); 
						RecallSet(recallId, "", "", "Fax send is in process", newJobId, "Send In Process", "",  "", actUserId, "");                           
					}
				}        
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [MedalistMain.FaxSendlDo]", Log.Error, 1);
				RecallSet(recallId, "", "", "Fax error: " + e.Message, "", "Error: " + e.Message, "",  "", actUserId, "");                         
			}            
		}   */
        
		public string FutureBizDate(int numberOfDays)
		{
			int index = numberOfDays;
			string countryCode = Standard.ConfigValue("CountryCode", "US");

			DateTime bizDate = DateTime.Parse(Master.BizDateExchange);

			while(index > 0)
			{                  
				bizDate = bizDate.AddDays(1.0);

				if (Standard.IsBizDate(bizDate, countryCode, Standard.HolidayType.Exchange, dbCnStr))        
				{                 
					index --;
				}      
			}

			Log.Write("Returning a future biz date of " + bizDate.ToString(Standard.DateFormat) + " for " +
				numberOfDays + " forward. [PositionAgent.FutureBizDate]", 3);

			return bizDate.ToString(Standard.DateFormat);
		}              
    
		public long CreditGet(string bookGroup, string book, string contractType)
		{     
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;      
      
			SqlParameter paramAvailableCredit  = null;
         
			try
			{      
				dbCn = new SqlConnection(dbCnStr);                
				dbCmd = new SqlCommand("spBookAvailableCreditGet", dbCn);        
				dbCmd.CommandType = CommandType.StoredProcedure;
        
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;
        
				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
				paramBook.Value = book;

				SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);
				paramContractType.Value = contractType;

				paramAvailableCredit = dbCmd.Parameters.Add("@AvailableCredit", SqlDbType.BigInt);
				paramAvailableCredit.Direction = ParameterDirection.Output;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();

				return (long)paramAvailableCredit.Value;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.CreditGet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (dbCn != null && !dbCn.State.Equals(ConnectionState.Closed))
				{
					dbCn.Close();
				}
			}
		}
    
		public DataSet	ContractRateHistoryGet(string bookGroup, string contractType, string secId)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;      
      SqlDataAdapter dataAdapter;

			DataSet dataSet = new DataSet();
         
			try
			{      
				dbCn = new SqlConnection(dbCnStr);                
				dbCmd = new SqlCommand("spContractRateHistoryGet", dbCn);        
				dbCmd.CommandType = CommandType.StoredProcedure;
        
				if (!bookGroup.Equals(""))
				{
					SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
					paramBookGroup.Value = bookGroup;
				}

				if (!contractType.Equals(""))
				{
					SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.VarChar, 1);
					paramContractType.Value = contractType;
				}

				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}

				dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "RateHistory");

                dataSet.RemotingFormat = SerializationFormat.Binary;												
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ContractRateSummaryGet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (dbCn != null && !dbCn.State.Equals(ConnectionState.Closed))
				{
					dbCn.Close();
				}
			}		
	
			return dataSet;
		}
		
		public bool CreditCheck(string bookGroup, string book, decimal amount, string contractType)
		{
			long availableCredit = CreditGet(bookGroup, book, contractType);

			return (amount <= (decimal)availableCredit);
		}
	  
		public DataSet DealDataGet(short utcOffset, string userId, string functionPath, string isActive)
		{
			return DealDataGet(utcOffset, "", userId, functionPath, isActive);
		}    	  
	  
		public DataSet DealDataGet(short utcOffset, string dealIdPrefix,  string userId, string functionPath, string isActive)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			SqlParameter paramUserId = null;
			SqlParameter paramFunctionPath = null;
      
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spDealGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
        
				SqlParameter paramDealIdPrefix = null;
				SqlParameter paramIsActive     = null;

				if (!isActive.Equals(""))
				{
					paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
					paramIsActive.Value = isActive;
				}
        
				if (!dealIdPrefix.Equals(""))
				{
					paramDealIdPrefix = dbCmd.Parameters.Add("@DealIdPrefix", SqlDbType.Char, 1);
					paramDealIdPrefix.Value = dealIdPrefix;        
				}
        
				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;
        
				dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "Deals");

				if (!dealIdPrefix.Equals(""))
				{
					dbCmd.Parameters.Remove(paramDealIdPrefix);
				}
                     
				dbCmd.CommandText = "spBooksGet";                
				dataAdapter.Fill(dataSet, "Books");

				dbCmd.Parameters.Remove(paramUtcOffset);
        
				if (!isActive.Equals(""))
				{
					dbCmd.Parameters.Remove(paramIsActive);
				}

				if (!userId.Equals(""))
				{
					paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);      
					paramUserId.Value = userId;
        
					paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);         
					paramFunctionPath.Value = functionPath;        
				}  

				dbCmd.CommandText = "spBookGroupGet";
				dataAdapter.Fill(dataSet, "BookGroups");

				if (!userId.Equals(""))
				{
					dbCmd.Parameters.Remove(paramUserId);
					dbCmd.Parameters.Remove(paramFunctionPath);       
				}
        
				dataSet.Tables["Deals"].PrimaryKey = new DataColumn[1]
					{ 
						dataSet.Tables["Deals"].Columns["DealId"]
					};

				dataSet.Tables["Books"].PrimaryKey = new DataColumn[2]
					{ 
						dataSet.Tables["Books"].Columns["BookGroup"],
						dataSet.Tables["Books"].Columns["Book"]
					};

                dataSet.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.DealDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}


		public DataSet DealDataGet(string bookGroup, string book, string dealType)
		{
			//Returns all deals by book
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
      
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spDealByBookGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
        
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
				paramBook.Value = book;
				
				SqlParameter paramDealType = dbCmd.Parameters.Add("@DealType", SqlDbType.Char, 1);
				if (dealType.Trim().Equals(""))
					paramDealType.Value = DBNull.Value;
				else
					paramDealType.Value = dealType;

				dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet);

                dataSet.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.DealDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}


		public string DealListSubmit(string bookGroup, string book, string bookContact, string dealType,  string dealIdPrefix, string comment, string list, string actUserId)
		{      
			DealList dealList = new DealList();
  
			if (dealList.Parse(list).Equals("OK"))
			{     
				Log.Write(" Processing a deal list for " + bookGroup + " with " + book + " for " + 
					dealList.Count + " items of type " + dealType + ". [PositionAgent.DealListSubmit]", 2);

				int n = 0;
  
				try 
				{        
					for (int i = 0; i < dealList.Count; i++)
					{
						DealSet(
							Standard.ProcessId(dealIdPrefix),
							bookGroup,
							dealType,
							book,
							bookContact,
							dealList.ItemGet(i).SecId,
							dealList.ItemGet(i).Quantity,
							"",
							"",
							"",
							"",
							"",
							"",
							"",
							"",
							"",
							"",
							"",
							"",
							"",
							"",
							"",
							"",
							comment,
							"D",
							actUserId, 
							true);

						n += 1;
					}
  
					return "OK - Sent " + dealList.Count.ToString("#,##0") + " item[s] to the Blotter.";
				}
				catch(Exception e) 
				{
					Log.Write(e.Message + " [PositionAgent.DealListSubmit]", Log.Error, 1);        
					Log.Write("Sent " + n + " item[s] out of an expected " + dealList.Count + " item[s] to the Blotter. [PositionAgent.DealListSubmit]", 2);
          
					return "Error: Sent " + n + " item[s] out of an expected " + dealList.Count + " item[s] to the Blotter.";
				}
			}
			else
			{
				Log.Write("Processed a deal list for " + bookGroup + " with " + book + " having results of: " + 
					dealList.Status + " [PositionAgent.DealListSubmit]", 2);
        
				return dealList.Status;
			}
		}

		public void DealSet(
			string  dealId,
			string  dealStatus,
			string  actUserId,
			bool    isActive      
			)
		{
			Log.Write("User " + actUserId + " is setting deal data for Deal ID " + dealId + " [PositionAgent.DealSet]", 3);
      
			DealSet(dealId, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "","", "", "", "", "", "", dealStatus, actUserId, isActive);
		}
    
		public void DealSet(
			string  dealId,
			string  bookGroup,
			string  dealType,
			string  book,
			string  bookContact,
			string  secId,
			string  quantity,
			string  amount,
			string  collateralCode,
			string  valueDate,
			string  settleDate,
			string  termDate,
			string  rate,
			string  rateCode,
			string  poolCode,
			string  divRate,
			string  divCallable,
			string  incomeTracked,
			string  margin,
			string  marginCode,
			string  currencyIso,
			string  securityDepot,
			string  cashDepot,
			string  comment,
			string  dealStatus,
			string  actUserId,
			bool    isActive     
			)
		{
			SqlDataReader dataReader = null;
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			SqlCommand dbCmd = new SqlCommand("spDealSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			DealEventArgs dealEventArgs = null;

			if (!secId.Equals(""))
			{
				Log.Write("User " + actUserId + " is setting deal data for Deal ID " + dealId +
					" in " + bookGroup + " for " + quantity + " of " + secId + ". [PositionAgent.DealSet]", 3);
			}

			try
			{        
				SqlParameter paramDealId = dbCmd.Parameters.Add("@DealId", SqlDbType.Char, 16);      
				paramDealId.Value = dealId;

				if (!bookGroup.Equals(""))
				{
					SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);      
					paramBookGroup.Value = bookGroup;
				}

				if (!dealType.Equals(""))
				{
					SqlParameter paramDealType = dbCmd.Parameters.Add("@DealType", SqlDbType.Char, 1);      
					paramDealType.Value = dealType;
				}

				if (!book.Equals(""))
				{
					SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);      
					paramBook.Value = book;
				}

				if (!bookContact.Equals(""))
				{
					SqlParameter paramBookContact = dbCmd.Parameters.Add("@BookContact", SqlDbType.VarChar, 15);      
					paramBookContact.Value = bookContact;
				}

				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);      
					paramSecId.Value = secId;
				}

				if (!quantity.Equals(""))
				{
					SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);      
					paramQuantity.Value = quantity;
				}
        
				if (!amount.Equals(""))
				{
					SqlParameter paramAmount = dbCmd.Parameters.Add("@Amount", SqlDbType.Money);      
					paramAmount.Value = amount;
				}

				if (!collateralCode.Equals(""))
				{
					SqlParameter paramCollateralCode = dbCmd.Parameters.Add("@CollateralCode", SqlDbType.VarChar, 1);      
					paramCollateralCode.Value = collateralCode;
				}

				if (!valueDate.Equals(""))
				{
					SqlParameter paramValueDate = dbCmd.Parameters.Add("@ValueDate", SqlDbType.DateTime);      
					paramValueDate.Value = valueDate;
				}

				if (!settleDate.Equals(""))
				{
					SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);      
					paramSettleDate.Value = settleDate;
				}

				if (!termDate.Equals(""))
				{
					SqlParameter paramTermDate = dbCmd.Parameters.Add("@TermDate", SqlDbType.DateTime);      
					paramTermDate.Value = termDate;
				}

				if (!rate.Equals(""))
				{
					SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Decimal);      
					paramRate.Value = rate;
				}

				if (!rateCode.Equals(""))
				{
					SqlParameter paramRateCode = dbCmd.Parameters.Add("@RateCode", SqlDbType.VarChar, 1);      
					paramRateCode.Value = rateCode;
				}

				if (!poolCode.Equals(""))
				{
					SqlParameter paramPoolCode = dbCmd.Parameters.Add("@PoolCode", SqlDbType.VarChar, 1);      
					paramPoolCode.Value = poolCode;
				}

				if (!divRate.Equals(""))
				{
					SqlParameter paramDivRate = dbCmd.Parameters.Add("@DivRate", SqlDbType.Decimal);      
					paramDivRate.Value = divRate;
				}

				if (!divCallable.Equals(""))
				{
					SqlParameter paramDivCallable = dbCmd.Parameters.Add("@DivCallable", SqlDbType.Bit);      
					paramDivCallable.Value = divCallable;
				}

				if (!incomeTracked.Equals(""))
				{
					SqlParameter paramIncomeTracked = dbCmd.Parameters.Add("@IncomeTracked", SqlDbType.Bit);      
					paramIncomeTracked.Value = incomeTracked;
				}

				if (!margin.Equals(""))
				{
					SqlParameter paramMargin = dbCmd.Parameters.Add("@Margin", SqlDbType.Decimal);      
					paramMargin.Value = margin;
				}

				if (!marginCode.Equals(""))
				{
					SqlParameter paramMarginCode = dbCmd.Parameters.Add("@MarginCode", SqlDbType.Char, 1);      
					paramMarginCode.Value = marginCode;
				}

				if (!currencyIso.Equals(""))
				{
					SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.Char, 3);      
					paramCurrencyIso.Value = currencyIso;
				}

				if (!securityDepot.Equals(""))
				{
					SqlParameter paramSecurityDepot = dbCmd.Parameters.Add("@SecurityDepot", SqlDbType.VarChar, 2);      
					paramSecurityDepot.Value = securityDepot;
				}
        
				if (!cashDepot.Equals(""))
				{
					SqlParameter paramCashDepot = dbCmd.Parameters.Add("@CashDepot", SqlDbType.VarChar, 2);      
					paramCashDepot.Value = cashDepot;
				}

				if (!comment.Equals(""))
				{
					SqlParameter paramComments = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 20);      
					paramComments.Value = comment;
				}

				SqlParameter paramDealStatus = dbCmd.Parameters.Add("@DealStatus", SqlDbType.Char, 1);      
				paramDealStatus.Value = dealStatus;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);      
				paramActUserId.Value = actUserId;

				SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);      
				paramIsActive.Value = isActive;             

				SqlParameter paramReturnData = dbCmd.Parameters.Add("@ReturnData", SqlDbType.Bit);      
				paramReturnData.Value = 1;             

				dbCn.Open();          
				dataReader = dbCmd.ExecuteReader();
                
				while (dataReader.Read()) // Expect one row.
				{
					dealEventArgs = new DealEventArgs(
						dataReader.GetValue(0).ToString(), 
						dataReader.GetValue(1).ToString(), 
						dataReader.GetValue(2).ToString(), 
						dataReader.GetValue(3).ToString(), 
						dataReader.GetValue(4).ToString(), 
						dataReader.GetValue(5).ToString(), 
						dataReader.GetValue(6).ToString(), 
						dataReader.GetValue(7).ToString(), 
						dataReader.GetValue(8).ToString(), 
						dataReader.GetValue(9).ToString(), 
						dataReader.GetValue(10).ToString(), 
						dataReader.GetValue(11).ToString(), 
						dataReader.GetValue(12).ToString(), 
						dataReader.GetValue(13).ToString(), 
						dataReader.GetValue(14).ToString(), 
						dataReader.GetValue(15).ToString(), 
						dataReader.GetValue(16).ToString(), 
						dataReader.GetValue(17).ToString(), 
						dataReader.GetValue(18).ToString(), 
						dataReader.GetValue(19).ToString(), 
						dataReader.GetValue(20).ToString(), 
						dataReader.GetValue(21).ToString(), 
						dataReader.GetValue(22).ToString(), 
						dataReader.GetValue(23).ToString(), 
						dataReader.GetValue(24).ToString(), 
						dataReader.GetValue(25).ToString(), 
						dataReader.GetValue(26).ToString(),
						dataReader.GetValue(27).ToString(),
						(bool)dataReader.GetValue(28));
				}        
				dataReader.Close();
    
				if (dealStatus.Equals("S") && isActive)
				{
					paramReturnData.Value = 0;

					try
					{
							processAgent.ContractAdd(
								dealEventArgs.DealId.ToString(),
								dealEventArgs.BookGroup,
								dealEventArgs.DealType,
								dealEventArgs.SecId,
								dealEventArgs.Book,      
								long.Parse(dealEventArgs.Quantity),
								decimal.Parse(dealEventArgs.Amount),              
								dealEventArgs.CollateralCode,
								"",
								decimal.Parse(dealEventArgs.Rate),
								dealEventArgs.RateCode,
								dealEventArgs.PoolCode,
								dealEventArgs.MarginCode,
								decimal.Parse(dealEventArgs.Margin),
								new decimal(0.0),
								dealEventArgs.Comment,
								"",
								new decimal(0.0),
								bool.Parse(dealEventArgs.IncomeTracked),
								decimal.Parse(dealEventArgs.DivRate),
								actUserId);
						
						paramDealStatus.Value = "P";
						dealEventArgs.DealStatus = "P";
					}
					catch (Exception e)
					{
						paramDealStatus.Value = "E";
						dealEventArgs.DealStatus = "E";

						Log.Write(e.Message + " [PositionAgent.DealSet]", Log.Error, 1);
					}

					dbCmd.ExecuteNonQuery();
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.DealSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (dataReader != null && !dataReader.IsClosed)
				{
					dataReader.Close();
				}

				if (dbCn != null && !dbCn.State.Equals(ConnectionState.Closed))
				{
					dbCn.Close();
				}
			}

			DealEventHandler dealEventHandler = new DealEventHandler(DealEventInvoke);
			dealEventHandler.BeginInvoke(dealEventArgs, null, null);
		}
    
		private void DealEventInvoke(DealEventArgs dealEventArgs)
		{
			DealEventHandler dealEventHandler = null;

			Log.Write(dealEventArgs.DealId + "|" + dealEventArgs.BookGroup + ":" + dealEventArgs.Book + ":" + 
				dealEventArgs.SecId + "[" + dealEventArgs.Symbol + "]:" +  dealEventArgs.Quantity.ToString() + " [PositionAgent.DealEventInvoke]", 3);

			try
			{
				if (DealEvent == null)
				{
					Log.Write("There are no DealEvent delegates. [PositionAgent.DealEventInvoke]", 3);
				}
				else
				{
					int n = 0;

					Delegate[] eventDelegates = DealEvent.GetInvocationList();
					Log.Write("DealEvent has " + eventDelegates.Length.ToString() + " delegates[s]. [PositionAgent.DealEventInvoke]", 2);
        
					foreach (Delegate eventDelegate in eventDelegates)
					{
						Log.Write("Invoking DealEvent delegate [" + (++n) + "]. [PositionAgent.DealEventInvoke]", 3);

						try
						{
							dealEventHandler = (DealEventHandler)eventDelegate;
							dealEventHandler(dealEventArgs);
						}
						catch (System.Net.Sockets.SocketException)
						{
							DealEvent -= dealEventHandler;
							Log.Write("DealEvent delegate [" + n + "] has been removed from the invocation list. [PositionAgent.DealEventInvoke]", 3);
						}
						catch (Exception e)
						{
							Log.Write(e.Message + " [PositionAgent.DealEventInvoke]", Log.Error, 1);
						}
					}
				}
			}
			catch(Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.DealEventInvoke]", Log.Error, 1);
			}
		}
    
		public void BoxSummarySet(string bookGroup, string secId, bool doNotRecall, string comment, string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			SqlCommand dbCmd = new SqlCommand("spBoxSummarySet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			try
			{
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);      
				paramBookGroup.Value = bookGroup;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);      
				paramSecId.Value = secId;
        
				SqlParameter paramDoNotRecall = dbCmd.Parameters.Add("@DoNotRecall", SqlDbType.Bit);      
				paramDoNotRecall.Value = doNotRecall;
        
				SqlParameter paramComments = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);      
				paramComments.Value = comment;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);      
				paramActUserId.Value = actUserId;

				dbCn.Open();                
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{        
				Log.Write(e.Message + " [PositionAgent.BoxSummarySet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (dbCn != null && !dbCn.State.Equals(ConnectionState.Closed))
				{
					dbCn.Close();
				}
			}
		}

		public DataSet BoxSummaryDataGet(short utcOffset,  string userId, string functionPath)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			SqlParameter paramUserId = null;
			SqlParameter paramFunctionPath = null;

			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBookGroupGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
      
				if (!userId.Equals(""))
				{
					paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);      
					paramUserId.Value = userId;
        
					paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);         
					paramFunctionPath.Value = functionPath;        
				}  

				dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "BookGroups");

				if (!userId.Equals(""))
				{
					dbCmd.Parameters.Remove(paramUserId);
					dbCmd.Parameters.Remove(paramFunctionPath);       
				}        

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);      
				paramBizDate.Value = Master.BizDate;      
        
				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);      
				paramUtcOffset.Value = utcOffset;      

				dbCmd.CommandText = "spBoxSummaryGet";        
				dataAdapter.Fill(dataSet, "BoxSummary");

				dataSet.Tables["BoxSummary"].PrimaryKey = new DataColumn[2]
			{
				dataSet.Tables["BoxSummary"].Columns["BookGroup"],
				dataSet.Tables["BoxSummary"].Columns["SecId"],
				};

                dataSet.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BoxSummaryDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}

        public DataSet BoxSummaryDataGet(short utcOffset, string userId, string functionPath, string bizDate)
        {
            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;
            SqlParameter paramUserId = null;
            SqlParameter paramFunctionPath = null;

            SqlDataAdapter dataAdapter;
            DataSet dataSet = new DataSet();

            try
            {
                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spBookGroupGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                if (!userId.Equals(""))
                {
                    paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                    paramUserId.Value = userId;

                    paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
                    paramFunctionPath.Value = functionPath;
                }

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "BookGroups");

                if (!userId.Equals(""))
                {
                    dbCmd.Parameters.Remove(paramUserId);
                    dbCmd.Parameters.Remove(paramFunctionPath);
                }

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                dbCmd.CommandText = "spBoxSummaryGet";
                dataAdapter.Fill(dataSet, "BoxSummary");

                dataSet.Tables["BoxSummary"].PrimaryKey = new DataColumn[2]
			{
				dataSet.Tables["BoxSummary"].Columns["BookGroup"],
				dataSet.Tables["BoxSummary"].Columns["SecId"],
				};

                dataSet.RemotingFormat = SerializationFormat.Binary;
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.BoxSummaryDataGet]", Log.Error, 1);
                throw;
            }

            return dataSet;
        }

		public DataSet BoxFailHistoryGet(string secId)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBoxFailHistoryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
        
				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);      
				paramSecId.Value = secId;
        
				dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "FailHistory");

                dataSet.RemotingFormat = SerializationFormat.Binary;
            }
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.BoxFailHistoryGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}
		
		public DataSet ContractRateComparisonDataGet(string userId, string functionPath)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			SqlDataAdapter dataAdapter;

			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBookGroupGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
        
				SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);      
				paramUserId.Value = userId;
        
				SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);         
				paramFunctionPath.Value = functionPath;        
				
				dataAdapter = new SqlDataAdapter(dbCmd); 
				dataAdapter.Fill(dataSet, "BookGroups");

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);      
				paramUtcOffset.Value = 0;	

				dbCmd.Parameters.Remove(paramUserId);
				dbCmd.Parameters.Remove(paramFunctionPath);
				
				dbCmd.CommandText = "spBooksGet";
				dataAdapter.Fill(dataSet, "Books");

                dataSet.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ContractRateComparisonDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}
	
		public DataSet ContractRateComparisonGet(string bizDate, string bookGroup, string book, string contractType)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spContractRateComparisonGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
       
				dataAdapter = new SqlDataAdapter(dbCmd);        

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);      
				paramBizDate.Value = bizDate;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);      
				paramBookGroup.Value = bookGroup;
			
				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);      
				paramBook.Value = book;				
			  
				SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);      
				paramContractType.Value = contractType;
								
				dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "RateComparison");

                dataSet.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ContractRateComparisonGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}
		
		
		public DataSet ContractDataGet(short utcOffset)
		{
			return ContractDataGet(utcOffset, null, null, null);
		}
    
		public DataSet ContractDataGet(short utcOffset, string bizDate, string userId, string functionPath)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			SqlDataAdapter dataAdapter;
			SqlParameter paramUserId = null;
			SqlParameter paramFunctionPath = null;

			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spContractGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
        
				dataAdapter = new SqlDataAdapter(dbCmd);        

				if (bizDate == null)
				{
					dataAdapter.Fill(dataSet, "Contracts");

					dbCmd.CommandText = "spContractBizDateList";
					dataAdapter.Fill(dataSet, "BizDates");

					if (!userId.Equals(""))
					{
						paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);      
						paramUserId.Value = userId;
        
						paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);         
						paramFunctionPath.Value = functionPath;        
					}  

					dbCmd.CommandText = "spBookGroupGet";
					dataAdapter.Fill(dataSet, "BookGroups");

					if (!userId.Equals(""))
					{
						dbCmd.Parameters.Remove(paramUserId);
						dbCmd.Parameters.Remove(paramFunctionPath);       
					}

					SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);      
					paramUtcOffset.Value = utcOffset;

					dbCmd.CommandText = "spBooksGet";
					dataAdapter.Fill(dataSet, "Books");
        
					dataSet.Tables["Books"].PrimaryKey = new DataColumn[2]
			{
				dataSet.Tables["Books"].Columns["BookGroup"],
				dataSet.Tables["Books"].Columns["Book"]
			};
				}
				else
				{
					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);      
					paramBizDate.Value = bizDate;      

					dataAdapter.Fill(dataSet, "Contracts");

					dbCmd.Parameters.Remove(paramBizDate);
          
					if (!userId.Equals(""))
					{
						paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);      
						paramUserId.Value = userId;
        
						paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);         
						paramFunctionPath.Value = functionPath;        
					}  

					dbCmd.CommandText = "spBookGroupGet";
					dataAdapter.Fill(dataSet, "BookGroups");
        
					if (!userId.Equals(""))
					{
						dbCmd.Parameters.Remove(paramUserId);
						dbCmd.Parameters.Remove(paramFunctionPath);       
					}

					dbCmd.CommandText = "spBooksGet";
					dataAdapter.Fill(dataSet, "Books");
        
					dataSet.Tables["Books"].PrimaryKey = new DataColumn[2]
			{
				dataSet.Tables["Books"].Columns["BookGroup"],
				dataSet.Tables["Books"].Columns["Book"]
			};
				}

				dataSet.Tables["Contracts"].PrimaryKey = new DataColumn[4]
			{
				dataSet.Tables["Contracts"].Columns["BizDate"],
				dataSet.Tables["Contracts"].Columns["BookGroup"],
				dataSet.Tables["Contracts"].Columns["ContractId"],
				dataSet.Tables["Contracts"].Columns["ContractType"]
			};

                dataSet.RemotingFormat = SerializationFormat.Binary;
            }
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ContractDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}

		public DataSet ContractDataGet(short utcOffset, string bookGroup, string contractId)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spContractGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
        
				dataAdapter = new SqlDataAdapter(dbCmd);        
        
				SqlParameter paramBookGroup= dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);      
				paramBookGroup.Value = bookGroup;      
        
				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 15);      
				paramContractId.Value = contractId;
        
				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);      
				paramUtcOffset.Value = utcOffset;
        
				dataAdapter.Fill(dataSet, "Contracts");

				dataSet.Tables["Contracts"].PrimaryKey = new DataColumn[4]
					{ 
						dataSet.Tables["Contracts"].Columns["BizDate"],
						dataSet.Tables["Contracts"].Columns["BookGroup"],
						dataSet.Tables["Contracts"].Columns["ContractId"],
						dataSet.Tables["Contracts"].Columns["ContractType"] };

                dataSet.RemotingFormat = SerializationFormat.Binary;
            }
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ContractDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;    
		}


		public DataSet ContractDataGet(string bookGroup, string book, string contractType)
		{
			//Returns all deals by book
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
      
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spContractByBookGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
				paramBook.Value = book;

				if (!contractType.Trim().Equals(""))
				{
					SqlParameter paramDealType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);
					paramDealType.Value = contractType;
				}

				/*if (contractType.Trim().Equals(""))
					paramDealType.Value = DBNull.Value;
				else
					paramDealType.Value = contractType;*/

				dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "Contracts");

                dataSet.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ContractDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}
    

		public void ContractSet( 
			string  bizDate,
			string  bookGroup,
			string  contractId,
			string  contractType)
		{
			ContractSet(
				bizDate,
				bookGroup, 
				contractId,
				contractType, 
				"", 
				"", 
				"", 
				"", 
				"",
				"", 
				"", 
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"", 
				"",
				"");   
		}

		public void ContractSet(
			string  bizDate,
			string  bookGroup,
			string  contractId,
			string  contractType,
			string  book,
			string  secId,
			string  quantity,
			string  quantitySettled,
			string  amount,
			string  amountSettled,
			string  collateralCode,
			string  valueDate,  
			string  settleDate,
			string  termDate,
			string  rate,
			string  rateCode,
			string  statusFlag,
			string  poolCode,
			string  divRate,
			string  divCallable,
			string  incomeTracked,
			string  marginCode,
			string  margin,
			string  currencyIso,
			string  securityDepot,
			string  cashDepot,
			string  otherBook,
			string  comment)
		{
			ContractEventArgs contractEventArgs = null;

			SqlDataReader dataReader = null;
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			SqlCommand dbCmd = new SqlCommand("spContractSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			try
			{
				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);      
				paramBizDate.Value = bizDate;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);      
				paramBookGroup.Value = bookGroup;

				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 15);      
				paramContractId.Value = contractId;

				SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);      
				paramContractType.Value = contractType;

				SqlParameter paramReturnData = dbCmd.Parameters.Add("@ReturnData", SqlDbType.Bit);      
				paramReturnData.Value = 1;

				if (!book.Equals(""))
				{
					SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);      
					paramBook.Value = book;
				}

				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);      
					paramSecId.Value = secId;
				}

				if (!quantity.Equals(""))
				{
					SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);      
					paramQuantity.Value = quantity;
				}
        
				if (!quantitySettled.Equals(""))
				{
					SqlParameter paramQuantitySettled = dbCmd.Parameters.Add("@QuantitySettled", SqlDbType.BigInt);      
					paramQuantitySettled.Value = quantitySettled;
				}
        
				if (!amount.Equals(""))
				{
					SqlParameter paramAmount = dbCmd.Parameters.Add("@Amount", SqlDbType.Money);      
					paramAmount.Value = amount;
				}

				if (!amountSettled.Equals(""))
				{
					SqlParameter paramAmountSettled = dbCmd.Parameters.Add("@AmountSettled", SqlDbType.Money);      
					paramAmountSettled.Value = amountSettled;
				}

				if (!collateralCode.Equals(""))
				{
					SqlParameter paramCollateralCode = dbCmd.Parameters.Add("@CollateralCode", SqlDbType.VarChar, 1);      
					paramCollateralCode.Value = collateralCode;
				}

				if (!valueDate.Equals(""))
				{
					SqlParameter paramValueDate = dbCmd.Parameters.Add("@ValueDate", SqlDbType.DateTime);      
					paramValueDate.Value = valueDate;
				}

				if (!settleDate.Equals(""))
				{
					SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);      
					paramSettleDate.Value = settleDate;
				}

				if (!termDate.Equals(""))
				{
					SqlParameter paramTermDate = dbCmd.Parameters.Add("@TermDate", SqlDbType.DateTime);      
					paramTermDate.Value = termDate;
				}

				if (!rate.Equals(""))
				{
					SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Decimal);      
					paramRate.Value = rate;
				}

				if (!rateCode.Equals(""))
				{
					SqlParameter paramRateCode = dbCmd.Parameters.Add("@RateCode", SqlDbType.VarChar, 1);      
					paramRateCode.Value = rateCode;
				}

				if (!poolCode.Equals(""))
				{
					SqlParameter paramPoolCode = dbCmd.Parameters.Add("@PoolCode", SqlDbType.VarChar, 1);      
					paramPoolCode.Value = poolCode;
				}

				if (!statusFlag.Equals(""))
				{
					SqlParameter paramStatusFlag = dbCmd.Parameters.Add("@StatusFlag", SqlDbType.VarChar, 1);      
					paramStatusFlag.Value = statusFlag;
				}

				if (!divRate.Equals(""))
				{
					SqlParameter paramDivRate = dbCmd.Parameters.Add("@DivRate", SqlDbType.Decimal);      
					paramDivRate.Value = divRate;
				}

				if (!divCallable.Equals(""))
				{
					SqlParameter paramDivCallable = dbCmd.Parameters.Add("@DivCallable", SqlDbType.Bit);      
					paramDivCallable.Value = divCallable;
				}

				if (!incomeTracked.Equals(""))
				{
					SqlParameter paramIncomeTracked = dbCmd.Parameters.Add("@IncomeTracked", SqlDbType.Bit);      
					paramIncomeTracked.Value = incomeTracked;
				}

				if (!marginCode.Equals(""))
				{
					SqlParameter paramMarginCode = dbCmd.Parameters.Add("@MarginCode", SqlDbType.Char, 1);      
					paramMarginCode.Value = marginCode;
				}

				if (!margin.Equals(""))
				{
					SqlParameter paramMargin = dbCmd.Parameters.Add("@Margin", SqlDbType.Decimal);      
					paramMargin.Value = margin;
				}

				if (!currencyIso.Equals(""))
				{
					SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.Char, 3);      
					paramCurrencyIso.Value = currencyIso;
				}

				if (!securityDepot.Equals(""))
				{
					SqlParameter paramSecurityDepot = dbCmd.Parameters.Add("@SecurityDepot", SqlDbType.VarChar, 2);      
					paramSecurityDepot.Value = securityDepot;
				}
        
				if (!cashDepot.Equals(""))
				{
					SqlParameter paramCashDepot = dbCmd.Parameters.Add("@CashDepot", SqlDbType.VarChar, 2);      
					paramCashDepot.Value = cashDepot;
				}

				if (!otherBook.Equals(""))
				{
					SqlParameter paramOtherBook = dbCmd.Parameters.Add("@OtherBook", SqlDbType.VarChar, 10);      
					paramOtherBook.Value = otherBook;
				}

				if (!comment.Equals(""))
				{
					SqlParameter paramComments = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 20);      
					paramComments.Value = comment;
				}

				dbCn.Open();                
				dataReader = dbCmd.ExecuteReader();
                
				while (dataReader.Read()) // Expect just one row.
				{
					contractEventArgs = new ContractEventArgs(
						dataReader.GetValue(0).ToString(),
						dataReader.GetValue(1).ToString(),
						dataReader.GetValue(2).ToString(),
						dataReader.GetValue(3).ToString(),
						dataReader.GetValue(4).ToString(),
						dataReader.GetValue(5).ToString(),
						dataReader.GetValue(6).ToString(),
						dataReader.GetValue(7).ToString(),
						dataReader.GetValue(8).ToString(),
						dataReader.GetValue(9).ToString(),
						dataReader.GetValue(10).ToString(),
						dataReader.GetValue(11).ToString(),
						dataReader.GetValue(12).ToString(),
						dataReader.GetValue(13).ToString(),
						dataReader.GetValue(14).ToString(),
						dataReader.GetValue(15).ToString(),
						dataReader.GetValue(16).ToString(),
						dataReader.GetValue(17).ToString(),
						dataReader.GetValue(18).ToString(),
						dataReader.GetValue(19).ToString(),
						dataReader.GetValue(20).ToString(),
						dataReader.GetValue(21).ToString(),
						dataReader.GetValue(22).ToString(),
						dataReader.GetValue(23).ToString(),
						dataReader.GetValue(24).ToString(),
						dataReader.GetValue(25).ToString(),
						dataReader.GetValue(26).ToString(),
						dataReader.GetValue(27).ToString(),
						dataReader.GetValue(28).ToString(),
						dataReader.GetValue(29).ToString(),
						dataReader.GetValue(30).ToString(),
						dataReader.GetValue(31).ToString(),
						dataReader.GetValue(32).ToString(),
						dataReader.GetValue(33).ToString(),
						dataReader.GetValue(34).ToString(),
						dataReader.GetValue(35).ToString(),
						dataReader.GetValue(36).ToString(),
						dataReader.GetValue(37).ToString(),
						dataReader.GetValue(38).ToString(),
						dataReader.GetValue(39).ToString(),
						dataReader.GetValue(40).ToString(),
						dataReader.GetValue(41).ToString(),
						dataReader.GetValue(42).ToString(),
						dataReader.GetValue(43).ToString(),
						dataReader.GetValue(44).ToString(),
						dataReader.GetValue(45).ToString()
						);    
				
					Log.Write("Setting a contract for " + contractEventArgs.BookGroup + " on " + contractEventArgs.ContractId + "-" + contractEventArgs.ContractType + " for " + 
						contractEventArgs.Quantity + " of " + contractEventArgs.SecId + ". [PositionAgent.ContractSet]", 2);
					
					ContractEventHandler contractEventHandler = new ContractEventHandler(ContractEventInvoke);
					contractEventHandler.BeginInvoke(contractEventArgs, null, null);
				}
			}
			catch (Exception e)
			{        
				Log.Write(e.Message + " [PositionAgent.ContractSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (dataReader != null && !dataReader.IsClosed)
				{
					dataReader.Close();
				}

				if (dbCn != null && !dbCn.State.Equals(ConnectionState.Closed))
				{
					dbCn.Close();
				}
			}
		}

		private void ContractEventInvoke(ContractEventArgs contractEventArgs)
		{
			ContractEventHandler contractEventHandler = null;
      
			string contractIdentifier = contractEventArgs.BizDate + "|" + contractEventArgs.BookGroup
				+ "|" + contractEventArgs.ContractId + "|" + contractEventArgs.ContractType; 

			try
			{
				if (ContractEvent == null)
				{
					Log.Write("Handling a contract event for " + contractIdentifier + " with no delegates. [PositionAgent.ContractEventInvoke]", 2);
				}
				else
				{
					int n = 0;

					Delegate[] eventDelegates = ContractEvent.GetInvocationList();
					Log.Write("Handling a contract event for " + contractIdentifier + " with " + eventDelegates.Length + " delegates. [PositionAgent.ContractEventInvoke]", 2);
          
					foreach (Delegate eventDelegate in eventDelegates)
					{
						Log.Write("Invoking delegate [" + (++n) + "]. [PositionAgent.ContractEventInvoke]", 3);

						try
						{
							contractEventHandler = (ContractEventHandler)eventDelegate;
							contractEventHandler(contractEventArgs);
						}
						catch (System.Net.Sockets.SocketException)
						{
							ContractEvent -= contractEventHandler;
							Log.Write("Contract event delegate [" + n + "] has been removed from the invocation list. [PositionAgent.ContractEventInvoke]", 3);
						}
						catch (Exception e)
						{
							Log.Write(e.Message + " [PositionAgent.ContractEventInvoke]", Log.Error, 1);
						}
					}

					Log.Write("Done invoking the contract event invocation list. [PositionAgent.ContractEventInvoke]", 3);
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + ". [PositionAgent.ContractEventInvoke]", Log.Error, 1);
			}
		}

		public DataSet DealsDetailDataGet(string BookGroup, string SecId, short UtcOffset)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			SqlParameter paramBookGroup = null;
			SqlParameter paramSecId = null;
			SqlParameter paramUtcOffset = null;

			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spDealDetailGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				dataAdapter = new SqlDataAdapter(dbCmd);
				
				if (!BookGroup.Equals(""))
				{
					paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.Char, 4);
					paramBookGroup.Value = BookGroup;        
				}

				if (!SecId.Equals(""))
				{
					paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.Char, 9);
					paramSecId.Value = SecId;        
				}

				if (!UtcOffset.Equals(""))
				{
					paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);      
					paramUtcOffset.Value = UtcOffset; 
				}
				
				dataAdapter.Fill(dataSet, "Deals");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.DealsDetailDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}

		
		public string RecallTermNoticeDocument(string recallId)
		{
			try
			{
				RecallDocument document = new RecallDocument(dbCnStr, recallId);
				return document.BorrowerAccountName + "|" + document.BorrowerFaxNumber + "|" + document.TermNotice;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + ". [PositionAgent.RecallTermNoticeDocuemnt]", Log.Error, 1);
				throw;
			}
		}
	  
		public DataSet RecallDataGet(short utcOffset)
		{
			return RecallDataGet(utcOffset, "", "", "");
		}
    
		public DataSet RecallDataGet(short utcOffset, string bizDate, string userId, string functionPath)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			SqlParameter paramBizDate = null;
			SqlParameter paramUserId = null;
			SqlParameter paramFunctionPath = null;
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();                  

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spRecallsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
        
				dataAdapter = new SqlDataAdapter(dbCmd);  

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);      
				paramUtcOffset.Value = utcOffset;    
      
				if (!bizDate.Equals(""))
				{
					paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);      
					paramBizDate.Value = bizDate;          
				}

				dataAdapter.Fill(dataSet, "Recalls");                           
        
				if (!bizDate.Equals(""))
				{
					dbCmd.Parameters.Remove(paramBizDate);
				}

				dbCmd.CommandText = "spRecallActivityGet";
				dataAdapter.Fill(dataSet, "RecallActivity");
              
				dbCmd.Parameters.Remove(paramUtcOffset);

				dbCmd.CommandText = "spBookGroupGet";
    
				if (!userId.Equals(""))
				{
					paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);      
					paramUserId.Value = userId;
        
					paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);         
					paramFunctionPath.Value = functionPath;        
				}  
            
				dataAdapter.Fill(dataSet, "BookGroups");
    
				if (!userId.Equals(""))
				{
					dbCmd.Parameters.Remove(paramUserId);
					dbCmd.Parameters.Remove(paramFunctionPath);       
				}

				dbCmd.CommandText = "spContractBizDateList";
				dataAdapter.Fill(dataSet, "BizDates");

				dbCmd.CommandText = "spRecallReasonsGet";
				dataAdapter.Fill(dataSet, "Reasons");                
      
				dataSet.Tables["Recalls"].PrimaryKey = new DataColumn[1]
					{ 
						dataSet.Tables["Recalls"].Columns["RecallId"]
					};
      
				dataSet.Tables["RecallActivity"].PrimaryKey = new DataColumn[2] 
					{
						dataSet.Tables["RecallActivity"].Columns["RecallId"],
						dataSet.Tables["RecallActivity"].Columns["SerialId"]
					};

				dataSet.Tables["Reasons"].PrimaryKey = new DataColumn[1] 
					{
						dataSet.Tables["Reasons"].Columns["ReasonId"]
					};

                dataSet.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.RecallDataGet]", Log.Error, 1);
				throw;
			}
    
			return dataSet;
		}

		public DataSet RecallActivityGet(short utcOffset, string reacallId)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();            
    
			try
			{
				dbCn = new SqlConnection(dbCnStr);
        
				dbCmd = new SqlCommand("spRecallActivityGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
        
				dataAdapter = new SqlDataAdapter(dbCmd); 

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);      
				paramUtcOffset.Value = utcOffset;
        
				SqlParameter paramRecallId = dbCmd.Parameters.Add("@RecallId", SqlDbType.Char, 16);      
				paramRecallId.Value = reacallId;
        
				dataAdapter.Fill(dataSet, "RecallActivity");

                dataSet.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.RecallActivityGet]", Log.Error, 1);
				throw;
			}
    
			return dataSet;
		}
    
		public DataSet RecallIndicatorsGet()
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();             

			try
			{
				dbCn = new SqlConnection(dbCnStr);
        
				dbCmd = new SqlCommand("spRecallIndicatorsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
        
				dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "Indicators");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.RecallIndicatorsGet]", Log.Error, 1);
				throw;
			}
    
			return dataSet;
		}

		public DataSet RecallReasonsGet()
		{
			return RecallReasonsGet("", "");
		}
    
		public DataSet RecallReasonsGet(string reasonId, string reasonCode)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();             

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spRecallReasonsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
        
				if(!reasonId.Equals(""))
				{
					SqlParameter paramReasonId = dbCmd.Parameters.Add("@ReasonId", SqlDbType.Char, 2);
					paramReasonId.Value = reasonId;
				}

				if(!reasonCode.Equals(""))
				{
					SqlParameter paramReasonCode = dbCmd.Parameters.Add("@ReasonCode", SqlDbType.Char, 2);
					paramReasonCode.Value = reasonCode;
				}

				dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "Reasons");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.RecallReasonsGet]", Log.Error, 1);
				throw;
			}
    
			return dataSet;
		}	

		public void RecallNew(      
			string bookGroup, 
			string contractId, 
			string contractType, 
			string book, 
			string bookContact,
			string secId, 
			string quantity, 
			string indicator,
			string baseDueDate, 
			string moveToDate, 
			string openDateTime, 
			string reasonCode,
			string sequenceNumber,
			string faxStatus,
			string comment,      
			string actUserId)
		{    
			string recallId = Standard.ProcessId("R");      
			SqlConnection dbCn = new SqlConnection(dbCnStr);            

			Log.Write("New recall for " + bookGroup + " on " + contractId + "-" + contractType + " for " + 
				quantity + " of " + secId + ". [PositionAgent.RecallNew]", 2);

			try
			{          
				SqlCommand dbCmd = new SqlCommand("spRecallSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramRecallId = dbCmd.Parameters.Add("@RecallId", SqlDbType.Char, 16);      
				paramRecallId.Value = recallId;
      
				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.Char, 9);      
				paramContractId.Value = contractId;      

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);      
				paramBookGroup.Value = bookGroup;      

				SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);      
				paramContractType.Value = contractType;
      
				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);      
				paramBook.Value = book;

				
				SqlParameter paramBookContact = dbCmd.Parameters.Add("@BookContact", SqlDbType.VarChar, 50);				
				if (!bookContact.Equals(""))
				{
					paramBookContact.Value = bookContact;
				}
				
				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);      
				paramSecId.Value = secId;
        
				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt, 8);      
				paramQuantity.Value = quantity;              
        
				SqlParameter paramBaseDueDate = dbCmd.Parameters.Add("@BaseDueDate", SqlDbType.DateTime);      
				paramBaseDueDate.Value = baseDueDate;
                
				if (!moveToDate.Equals(""))
				{
					SqlParameter paramMoveToDate = dbCmd.Parameters.Add("@MoveToDate", SqlDbType.DateTime);      
					paramMoveToDate.Value = moveToDate; 
				}

				SqlParameter paramOpenDateTime = dbCmd.Parameters.Add("@OpenDateTime", SqlDbType.DateTime);      
				paramOpenDateTime.Value = openDateTime; 
        
				SqlParameter paramReasonCode = dbCmd.Parameters.Add("@ReasonCode", SqlDbType.Char, 2);      
				paramReasonCode.Value = reasonCode;

				SqlParameter paramSequenceNumber = dbCmd.Parameters.Add("@SequenceNumber", SqlDbType.SmallInt, 2);      
				paramSequenceNumber.Value = sequenceNumber;
        
				SqlParameter paramComments = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 20);      
				paramComments.Value = comment;        
        
				SqlParameter paramFaxStatus = dbCmd.Parameters.Add("@FaxStatus", SqlDbType.VarChar, 50);      
				paramFaxStatus.Value = faxStatus;
               
				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);      
				paramActUserId.Value = actUserId;
           
				SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.Char, 1);
				paramStatus.Value = "I";
                 
				dbCn.Open();              
				dbCmd.ExecuteNonQuery(); 
   
        
				DataSet ds = RecallReasonsGet("", reasonCode);
        
				reasonCode = ds.Tables["Reasons"].Rows[0]["ReasonId"].ToString();        
        
				processAgent.Recall(
					bookGroup,
					contractType,
					book,
					contractId,
					openDateTime,
					int.Parse(sequenceNumber),
					long.Parse(quantity),
					baseDueDate,
					baseDueDate,
					indicator,
					reasonCode,
					recallId,
					comment,
					actUserId,
					false);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionAgent.RecallNew]", Log.Error, 1);
				throw;
			}     
			finally
			{        
				if (dbCn != null && !dbCn.State.Equals(ConnectionState.Closed))
				{
					dbCn.Close();
				}
			}
		}

		public void RecallDelete(
			string bookGroup,
			string contractType,
			string book,
			string contractId,
			string recallDate,
			int    recallSequence,
			string lenderReference,
			string comment,
			string actUserId)
		{
			Log.Write("Recall delete for ID: " + lenderReference + ". [PositionAgent.RecallDelete]", 2);

			try
			{
				processAgent.Recall(
					bookGroup,
					contractType,
					book,
					contractId,
					recallDate,
					recallSequence,
					0,
					"",
					"",
					"",
					"",
					lenderReference,
					comment,
					actUserId,
					true);
			}        
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionAgent.RecallDelete]", Log.Error, 1);
				throw;
			}        
		}      
    
	
		public void RecallBookContactSet(
			string bookGroup,
			string book,
			string bookContact,
			string actUserId)
		{
			string recallId = Standard.ProcessId("R");      
			SqlConnection dbCn = new SqlConnection(dbCnStr);            
			SqlDataReader dataReader = null;
			
			ArrayList recallEventArgsArray = new ArrayList();
			
			RecallEventArgs recallEventArgsItem = null;

			try
			{          
				SqlCommand dbCmd = new SqlCommand("spRecallBookContactControlSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);      
				paramBookGroup.Value = bookGroup;
      				
				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);      
				paramBook.Value = book;
				
				SqlParameter paramBookContact = dbCmd.Parameters.Add("@BookContact", SqlDbType.VarChar, 50);				
				if (!bookContact.Equals(""))
				{
					paramBookContact.Value = bookContact;
				}
				else
				{
					paramBookContact.Value = "";
				}
							               
				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);      
				paramActUserId.Value = actUserId;
           			           
				dbCn.Open();              				           
				dataReader = dbCmd.ExecuteReader();
                       
				while (dataReader.Read()) // Expect just one row.
				{         
					recallEventArgsItem = new RecallEventArgs(
						dataReader.GetValue(0).ToString(),
						dataReader.GetValue(1).ToString(),
						dataReader.GetValue(2).ToString(),
						dataReader.GetValue(3).ToString(),
						dataReader.GetValue(4).ToString(),
						dataReader.GetValue(5).ToString(),
						dataReader.GetValue(6).ToString(),
						dataReader.GetValue(7).ToString(),
						(bool)dataReader.GetValue(8),
						(bool)dataReader.GetValue(9),
						(bool)dataReader.GetValue(10),
						(bool)dataReader.GetValue(11),
						dataReader.GetValue(12).ToString(),
						dataReader.GetValue(13).ToString(),
						dataReader.GetValue(14).ToString(),
						dataReader.GetValue(15).ToString(),            
						dataReader.GetValue(16).ToString(),
						dataReader.GetValue(17).ToString(),
						dataReader.GetValue(18).ToString(),
						dataReader.GetValue(19).ToString(),
						dataReader.GetValue(20).ToString(),
						dataReader.GetValue(21).ToString(),
						dataReader.GetValue(22).ToString(),
						dataReader.GetValue(23).ToString(),
						dataReader.GetValue(24).ToString(),          
						dataReader.GetValue(25).ToString(),
						dataReader.GetValue(26).ToString(),            
						dataReader.GetValue(27).ToString(),
						dataReader.GetValue(28).ToString()
						);
				
					recallEventArgsArray.Add(recallEventArgsItem);
				}


				for(int count = 0; count <  recallEventArgsArray.Count; count ++)
				{
					RecallEventHandler recallEventHandler = new RecallEventHandler(RecallEventInvoke);
					recallEventHandler.BeginInvoke((RecallEventArgs)recallEventArgsArray[count], null, null);    
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionAgent.RecallNew]", Log.Error, 1);
				throw;
			}     
			finally
			{        
				if (dbCn != null && !dbCn.State.Equals(ConnectionState.Closed))
				{
					dbCn.Close();
				}
			}
		}
		
		public void RecallSet(
			string recallId, 
			string bookContact,
			string moveToDate, 
			string action,
			string faxId,
			string faxStatus,
			string deliveredToday,
			string willNeed,
			string actUserId,
			string status)
		{
			RecallEventArgs recallEventArgsItem = null;
			bool sendFax = false;      
      
			if(faxStatus.Equals("OK"))
			{
				sendFax = true;
				faxStatus = "";
			}
      
			SqlConnection dbCn = new SqlConnection(dbCnStr);      
			SqlDataReader dataReader = null;
      
			SqlCommand dbCmd = new SqlCommand("spRecallSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;
      
			Log.Write("Recall update for ID: " + recallId + ". [PositionAgent.RecallSet]", 2);

			try
			{         
				SqlParameter paramRecallId = dbCmd.Parameters.Add("@RecallId", SqlDbType.Char, 16);      
				paramRecallId.Value = recallId;
      
				SqlParameter paramBookContact = dbCmd.Parameters.Add("@BookContact", SqlDbType.VarChar, 50);
				
				if (!bookContact.Equals(""))
				{
					paramBookContact.Value = bookContact;
				}

				if (!action.Equals(""))
				{
					SqlParameter paramAction = dbCmd.Parameters.Add("@Action", SqlDbType.VarChar, 100);      
					paramAction.Value = action;                
				}
        
				if (!faxId.Equals(""))
				{
					SqlParameter paramFaxId = dbCmd.Parameters.Add("@FaxId", SqlDbType.VarChar, 50);      
					paramFaxId.Value = faxId;                
				}
        
				if (!faxStatus.Equals(""))
				{
					SqlParameter paramFaxStatus = dbCmd.Parameters.Add("@FaxStatus", SqlDbType.VarChar, 50);      
					paramFaxStatus.Value = faxStatus;
				}             
        
				if (!deliveredToday.Equals(""))
				{
					SqlParameter paramDeliveredToday = dbCmd.Parameters.Add("@DeliveredToday", SqlDbType.BigInt);      
					paramDeliveredToday.Value = deliveredToday;
				}             
        
				if (!willNeed.Equals(""))
				{
					SqlParameter paramWillNeed = dbCmd.Parameters.Add("@WillNeed", SqlDbType.BigInt);      
					paramWillNeed.Value = willNeed;
				}             
           
				if (!actUserId.Equals(""))
				{
					SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);      
					paramActUserId.Value = actUserId;
				}

				if (!status.Equals(""))
				{
					SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.Char, 1);      
					paramStatus.Value = status;
				}
    
				SqlParameter paramReturn = dbCmd.Parameters.Add("@ReturnData", SqlDbType.Bit);      
				paramReturn.Value = 1;
        
				dbCn.Open();              
				dataReader = dbCmd.ExecuteReader();
                       
				while (dataReader.Read()) // Expect just one row.
				{         
					recallEventArgsItem = new RecallEventArgs(
						dataReader.GetValue(0).ToString(),
						dataReader.GetValue(1).ToString(),
						dataReader.GetValue(2).ToString(),
						dataReader.GetValue(3).ToString(),
						dataReader.GetValue(4).ToString(),
						dataReader.GetValue(5).ToString(),
						dataReader.GetValue(6).ToString(),
						dataReader.GetValue(7).ToString(),
						(bool)dataReader.GetValue(8),
						(bool)dataReader.GetValue(9),
						(bool)dataReader.GetValue(10),
						(bool)dataReader.GetValue(11),
						dataReader.GetValue(12).ToString(),
						dataReader.GetValue(13).ToString(),
						dataReader.GetValue(14).ToString(),
						dataReader.GetValue(15).ToString(),            
						dataReader.GetValue(16).ToString(),
						dataReader.GetValue(17).ToString(),
						dataReader.GetValue(18).ToString(),
						dataReader.GetValue(19).ToString(),
						dataReader.GetValue(20).ToString(),
						dataReader.GetValue(21).ToString(),
						dataReader.GetValue(22).ToString(),
						dataReader.GetValue(23).ToString(),
						dataReader.GetValue(24).ToString(),          
						dataReader.GetValue(25).ToString(),
						dataReader.GetValue(26).ToString(),            
						dataReader.GetValue(27).ToString(),
						dataReader.GetValue(28).ToString()
						);
				}
         
				if (dataReader.NextResult())
				{
					if (recallEventArgsItem != null)
					{
						ArrayList recallActivityList = new ArrayList();

						while(dataReader.Read())
						{ 
							recallActivityList.Add(dataReader.GetValue(1).ToString());
							recallActivityList.Add(dataReader.GetValue(2).ToString());
						}
          
						recallEventArgsItem.RecallActivity = (string [])recallActivityList.ToArray(typeof(string));         
					}
				}
        
				dataReader.Close();

				if (faxEnabled)
				{
					if(recallEventArgsItem.FaxStatus.Equals("Send Pending") && sendFax)
					{ 
						RecallDocument recallDocument = new RecallDocument(dbCnStr);
						recallDocument.RecallId = recallId;            
          
						Log.Write("Sending a fax for recall ID " + recallId + " [PositionAgent.RecallSet]", 3);             

						/*FaxSend(
							recallId, 
							recallDocument.BorrowerFaxNumber, 
							"",  
							recallEventArgsItem.Book,  
							"Recall of " + recallEventArgsItem.Quantity + " " + recallEventArgsItem.SecId + " [" + recallEventArgsItem.Symbol + "]",  
							recallDocument.TermNotice, 
							false,
							actUserId);                                                                  */
					}
        
					if(recallEventArgsItem.FaxStatus.Equals("Cancel Pending"))
					{                                                      
						Log.Write("Cancelling a fax for recall ID " + recallId+ " [PositionAgent.RecallSet]", 3);       

					/*	FaxSend(
							recallEventArgsItem.RecallId, 
							"", 
							recallEventArgsItem.FaxId, 
							"",  
							"", 
							"", 
							true,
							actUserId);                                              */
					}
				}
  
				RecallEventHandler recallEventHandler = new RecallEventHandler(RecallEventInvoke);
				recallEventHandler.BeginInvoke(recallEventArgsItem, null, null);                   
			}      
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.RecallSet]", Log.Error, 1);
				throw;
			}
			finally
			{        
				dbCn.Close();
			}
		}

		private void RecallEventInvoke(RecallEventArgs recallEventArgsItem)
		{
			RecallEventHandler recallEventHandler = null;
  
			try
			{
				string recallIdentifier = recallEventArgsItem.BookGroup + ":" + recallEventArgsItem.ContractId + ":" + recallEventArgsItem.ContractType; 

				if (RecallEvent == null)
				{
					Log.Write("Handling a recall event for " + recallIdentifier + " with no delegates. [PositionAgent.RecallEventInvoke]", 3);
				}
				else
				{
					int n = 0;

					Delegate[] eventDelegates = RecallEvent.GetInvocationList();
					Log.Write("Handling a recall event for " + recallIdentifier + " with " + eventDelegates.Length + " delegates. [PositionAgent.RecallEventInvoke]", 2);
          
					foreach (Delegate eventDelegate in eventDelegates)
					{
						Log.Write("Invoking delegate [" + (++n) + "]. [PositionAgent.RecallEventInvoke]", 3);

						try
						{
							recallEventHandler = (RecallEventHandler)eventDelegate;
							recallEventHandler(recallEventArgsItem);
						}
						catch (System.Net.Sockets.SocketException)
						{
							RecallEvent -= recallEventHandler;
							Log.Write("Recall event delegate [" + n + "] has been removed from the invocation list. [PositionAgent.RecallEventInvoke]", 3);
						}
						catch (Exception e)
						{
							Log.Write(e.Message + " [PositionAgent.RecallEventInvoke]", Log.Error, 1);
						}
					}

					Log.Write("Done invoking the recall event invocation list. [PositionAgent.RecallEventInvoke]", 3);
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + ". [PositionAgent.RecallEventInvoke]", Log.Error, 1);
			}
		}

		public string ContractAdd(                        
			string  bookGroup,
			string  contractType,
			string  secId,
			string  book,
			string  quantity,
			string  amount,      
			string  collateralCode,
			string  expiryDate,
			string  rate,
			string  rateCode,
			string  poolCode,
			string  marginCode,
			string  margin,
			string  negotiatedNewRate,
			string  comment,
			string  otherBook,
			string  fixedInvesmtmentRate,
			string  incomeTracked,
			string  divRate,
			string  actUserId)      
		{  
			string processId = Standard.ProcessId("C");
      
			Log.Write("New contract for " + bookGroup + " with " + book + " on " + contractType + " of " + quantity + 
				" " + secId + ". [PositionAgent.ContractAdd]", 2);

			try
			{
				processAgent.ContractAdd(
					processId,
					bookGroup,
					contractType,
					secId,
					book,      
					long.Parse(quantity),
					decimal.Parse(amount),          
					collateralCode,
					expiryDate,
					decimal.Parse(rate),
					rateCode,
					poolCode,
					marginCode,
					decimal.Parse(margin),
					decimal.Parse(negotiatedNewRate),
					comment,
					otherBook,
					decimal.Parse(fixedInvesmtmentRate),
					bool.Parse(incomeTracked),
					decimal.Parse(divRate),
					actUserId);                     

				return processId;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ContractAdd]", Log.Error, 1);
				throw;
			}
		}
    
		public void RateChange(      
			string  bookGroup,
			string  contractType,
			string  book,
			string  securityType,
			string  contractId,
			decimal rateOld,
			string  rateCodeOld,
			decimal rateNew,      
			string  rateCodeNew,            
			string  profitCenter,
			string  effectiveDate,
			string  actUserId)
		{
			Log.Write("Rate change for " + bookGroup + " with " + book + " on " + contractId + "-" + contractType + 
				" from " + rateOld + " to " + rateNew + ". [PositionAgent.RateChange]", 2);

			try
			{
				processAgent.RateChange(
					bookGroup,
					contractType,
					book,
					securityType,
					contractId,
					rateOld,
					rateCodeOld,
					rateNew,          
					rateCodeNew,                    
					profitCenter,
					effectiveDate,
					actUserId);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.RateChange]", Log.Error, 1);
				throw;
			}
		}

		public void Return(
			string bookGroup,
			string contractType,
			string contractId,
			string secId,
			long   returnQuantity,            
			decimal returnAmount,
			string callbackRequired,
			string recDelLocation,
			string cashDepot,
			string actUserId)
		{
			Log.Write("Return for " + bookGroup + " on " + contractId + "-" + contractType + " of " + 
				returnQuantity.ToString("#,##0") + ". [PositionAgent.Return]", 2);

			try
			{
					processAgent.Return(
						bookGroup,
						contractType,
						contractId,
						returnQuantity,                    
						returnAmount,
						callbackRequired,
						recDelLocation,
						cashDepot,
						actUserId);		
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.Return]", Log.Error, 1);
				throw;
			}
		}

		private string UserEmailGet(string userId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			SqlDataReader dataReader = null;

			string userEmail = "";

			try
			{
				SqlCommand dbCmd = new SqlCommand("spUsersGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
          
				SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar,(50));
				paramUserId.Value = userId;

				dbCn.Open();
				dataReader = dbCmd.ExecuteReader();

				while (dataReader.Read()) // Expect one row.
				{
					userEmail = dataReader.GetValue(2).ToString().Trim();
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [AdminAgent.UserEmailGet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if ((dataReader != null)) 
				{
					dataReader.Close();
				}

				if (dbCn.State != ConnectionState.Closed) 
				{
					dbCn.Close();
				}
			}
      
			return userEmail;
		}
    
		public void ContractMaintenance(
			string bookGroup,
			string contractId,
			string contractType,
			string book,      
			string poolCode,
			string effectiveDate,
			string deliveryDate,
			string marginCode,
			string margin,
			string divRate,
			string incomeTracked,
			string expiryDate,
			string comment,
			string actUserId)
		{
			Log.Write("Update for " + bookGroup + " with " + book + " on " + contractId + "-" + contractType + 
				". [PositionAgent.ContractMaintenance]", 2);
      
			try
			{
				processAgent.ContractMaintenance(
					bookGroup,
					contractId,
					contractType,
					book,          
					poolCode,
					effectiveDate,
					"",
					"",
					"",
					"",
					"",
					"",
					"",
					actUserId);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ContractMaintenance]", Log.Error, 1);
				throw;
			}
		}

		public void HeartbeatOnEvent(Anetics.Process.HeartbeatEventArgs e)
		{ 
			heartbeatTimestamp = DateTime.UtcNow;
      
			heartbeatAlert = e.Alert;
			heartbeatStatus = e.Status;
      
			Log.Write(heartbeatStatus.ToString() + ": " + heartbeatAlert + " [PositionAgent.HeartbeatOnEvent]", 3);
		}

	

		private bool ProcessStatusIsReady
		{
			get
			{
				return processStatusIsReady;
			}

			set
			{
				Anetics.Process.ProcessStatusEventArgs processStatusEventArgs ;

				try
				{
					lock (this)
					{
						if (value && (processStatusEventArgsArray.Count > 0))
						{          
							processStatusIsReady = false;

							processStatusEventArgs = (Anetics.Process.ProcessStatusEventArgs)processStatusEventArgsArray[0];
							processStatusEventArgsArray.RemoveAt(0);

							Anetics.Process.ProcessStatusEventHandler processStatusEventHandler;
							processStatusEventHandler = new Anetics.Process.ProcessStatusEventHandler(ProcessStatusDoEvent);
              
							processStatusEventHandler.BeginInvoke(processStatusEventArgs, null, null);            
						}
						else
						{
							processStatusIsReady = value;
						}
					}
				}
				catch (Exception ee)
				{
					Log.Write(ee.Message + " [PositionAgent.ProcessStatusIsReady(set)]", Log.Error, 1); 
				}
			}
		}

		private void ProcessStatusOnEvent(Anetics.Process.ProcessStatusEventArgs e)
		{
			lock (this)
			{
				Log.Write("Queuing process status event [" + e.ActCode + "] " + e.ProcessId + " with " +
					processStatusEventArgsArray.Count + " events already queued. [PositionAgent.ProcessStatusOnEvent]", 3);

				processStatusEventArgsArray.Add(e);
    
				if (ProcessStatusIsReady) // Force reset to trigger handling of event.
				{
					ProcessStatusIsReady = true;
				}
			}
		}
    
		private void ProcessStatusDoEvent(Anetics.Process.ProcessStatusEventArgs e)
		{
			Log.Write("Handling status event [" + e.ActCode + "] " + e.ProcessId + ". [PositionAgent.ProcessStatusDoEvent]", 3);

			try
			{
				switch(e.ActCode)
				{
					case "BLA":
						BankLoanPledgeSet(
							e.BookGroup,
							e.Book,
							"",
							e.Tag,							  							  
							"",
							"",
							"",
							"",
							"",
							"",
							"");
						break;

					case "RCA":  
						if (e.HasError)
						{
							RecallSet(
								e.Tag,                    
								"",
								"",
								"Error: " + e.Status,                         
								"",
								"",
								"",
								"",
								"",
								"");                    
						}
						else if (e.Status.Equals("OK"))
						{
							RecallSet(
								Tools.SplitItem(e.Tag, "|", 2),                
								"",
								"",
								"",
								"",
								"OK",
								"",
								"",
								"",
								"");
              
							ContractSet(
								Master.ContractsBizDate, 
								e.BookGroup, 
								e.ContractId,
								e.ContractType);
						}                             
						break;
        
					case "RCD": 
						if (e.HasError)
						{ 
							RecallSet(
								e.Tag,                  
								"",
								"",
								"Error: " + e.Status,
								"",
								"",
								"",
								"",
								"",
								"");                                   
						}
						else if (e.Status.Equals("OK"))
						{
							RecallSet(
								e.Tag,                      
								"",
								"",
								"",
								"",
								"",
								"",
								"",
								"",
								"");
    
							ContractSet(
								Master.ContractsBizDate, 
								e.BookGroup, 
								e.ContractId,
								e.ContractType);
						}      
                                             
						break;
        
					case "NCS":         
						if (e.HasError)
						{
							DealSet(e.ProcessId, "E", e.ActUser, true);
						}
						else if (e.Status.Equals("OK"))
						{
							DealSet(e.ProcessId, "C", e.ActUser, true);
							ContractSet(
								Master.ContractsBizDate, 
								e.BookGroup, 
								e.ContractId,
								e.ContractType);
						}
						break;
                                   
					case "BLD":
					  
						BankLoanPledgeSet(
							e.BookGroup,
							"",
							"",
							e.Tag,
							"",
							"",
							"",
							"",
							"",
							"",
							"");
						break;

					case "DRC":
					case "DBR":  
						RecallSet(
							e.Tag,
							"",
							"",
							"",
							"",
							"",
							"",
							"",
							"ADMIN",
							"");
    
						ContractSet(
							Master.ContractsBizDate, 
							e.BookGroup, 
							e.ContractId, 
							e.ContractType); 
						break;
        
					case "DBT":  
					case "DTX": 
						ContractSet(
							Master.ContractsBizDate, 
							e.BookGroup, 
							e.ContractId, 
							e.ContractType); 
						break;
	
					case "DTA":
						foreach (DataRow row in ContractsModifiedGet(e.BookGroup).Tables["Contracts"].Rows)
						{
							ContractSet(
								row["BizDate"].ToString(),
								e.BookGroup,
								row["ContractId"].ToString(),
								row["ContractType"].ToString(),
								"",
								"",
								"",
								"",
								"",
								"",
								"",
								"",
								"",
								"",
								"",
								"",
								" ",
								"",
								"",
								"",
								"",
								"",
								"",
								"",
								"",
								"",
								"", 
								"");
						}
						break;

					case "DBU": 
					case "DBA":
					case "PCC": 
					case "FRC":
					case "RCB": 
					case "BTN":
					case "NGR": 
					case "NTB":
					case "PTN":
					case "ABW":
						break;

					case "GSR":
						Log.Write("General system reject received from Loanet. [PositionAgent.ProcessStatusOnEvent]", Log.Error, 1);

						break;

					default:
						Log.Write("Unanticipated event type:" + e.ActCode + " [PositionAgent.ProcessStatusOnEvent]", Log.Error, 1);

						break;
				}
    
				ProcessStatusEventArgs processStatusEventArg = new ProcessStatusEventArgs(
					e.ProcessId, 
					e.SystemCode,
					e.ActCode, 
					e.Act, 
					e.ActTime, 
					e.ActUser, 
					e.HasError, 
					e.BookGroup,
					e.ContractId, 
					e.ContractType, 
					e.Book, 
					e.SecId, 
					e.Symbol,
					e.Quantity,
					e.Amount,
					e.Status, 
					e.StatusTime,
					e.Tag
					);
      
				ProcessStatusEventInvoke(processStatusEventArg);
			}
			catch (Exception ee)
			{
				Log.Write(ee.Message + " [PositionAgent.ProcessStatusDoEvent]", Log.Error, 1);
			}
			finally
			{
				ProcessStatusIsReady = true;
			}
		}
    
		public void ProcessStatusEventInvoke(ProcessStatusEventArgs processStatusEventArgs)
		{
			ProcessStatusEventHandler processStatusEventHandler = null;
      
			string processStatusIdentifier =  "[" + processStatusEventArgs.ActCode + "] " + processStatusEventArgs.ProcessId;

			try
			{
				if (ProcessStatusEvent == null)
				{
					Log.Write("Handling a process status event for " + processStatusIdentifier + " with no delegates. [PositionAgent.ProcessStatusEventInvoke]", 2);
				}
				else
				{
					int n = 0;

					Delegate[] eventDelegates = ProcessStatusEvent.GetInvocationList();
					Log.Write("Handling a process status event for " + processStatusIdentifier + " with " + eventDelegates.Length + " delegates. [PositionAgent.ProcessStatusEventInvoke]", 2);
          
					foreach (Delegate eventDelegate in eventDelegates)
					{
						Log.Write("Invoking delegate [" + (++n) + "]. [PositionAgent.ProcessStatusEventInvoke]", 3);

						try
						{
							processStatusEventHandler = (ProcessStatusEventHandler) eventDelegate;
							processStatusEventHandler(processStatusEventArgs);
						}
						catch (System.Net.Sockets.SocketException)
						{
							ProcessStatusEvent -= processStatusEventHandler;
							Log.Write("Process status event delegate [" + n + "] has been removed from the invocation list. [PositionAgent.ProcessStatusEventInvoke]", 3);
						}
						catch (Exception e)
						{
							Log.Write(e.Message + " [PositionAgent.ProcessStatusEventInvoke]", Log.Error, 1);
						}
					}

					Log.Write("Done invoking the process status event invocation list. [PositionAgent.ProcessStatusEventInvoke]", 3);
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ProcessStatusEventInvoke]", Log.Error, 1);
			}
		}
	
  
		public string Alert
		{
			get
			{
				return heartbeatAlert;
			}
		}

		public Anetics.Process.HeartbeatStatus Status
		{
			get
			{
				if (heartbeatTimestamp.AddMilliseconds(1.5 * heartbeatInterval).CompareTo(DateTime.UtcNow).Equals(1))
				{
					return heartbeatStatus;
				}
				else
				{
					return Anetics.Process.HeartbeatStatus.Unknown;
				}
			}
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
