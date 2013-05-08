// Licensed Materials - Property of StockLoan, LLC.
// Copyright (C) StockLoan, LLC. 2005  All rights reserved.

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using StockLoan.Common;

namespace StockLoan.Medalist
{
	public class PositionAgent : MarshalByRefObject, IPosition
	{
		private string dbCnStr;
	
		private ArrayList processStatusEventArgsArray;		

		private bool processStatusIsReady = false;		

		private double heartbeatInterval;
		private string heartbeatAlert = "";

		private DateTime heartbeatTimestamp = DateTime.UtcNow;
		
		private bool faxEnabled;

		private delegate void FaxStatusUpdateDelegate();
		
		

		public PositionAgent(string dbCnStr)
		{
			this.dbCnStr = dbCnStr; 
		}

        public DataSet CurrenciesGet(string bizDate)
        {
            SqlDataAdapter dataAdapter = null;
            SqlCommand dbCmd = null;

            DataSet ds = new DataSet();

            try
            {
                dbCmd = new SqlCommand("spCurrencyGet", new SqlConnection(dbCnStr));
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.VarChar, 10);
                paramBizDate.Value = bizDate;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(ds, "Currency");
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.CurrenciesGet]", Log.Error, 1);
            }

            return ds;
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
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.DealDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
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
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			SqlCommand dbCmd = new SqlCommand("spDealSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

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

                dbCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.DealSet]", Log.Error, 1);
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
		
		public string Alert
		{
			get
			{
				return heartbeatAlert;
			}
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
