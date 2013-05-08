// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class RebateAgent : MarshalByRefObject, IRebate
	{
		private string dbCnStr;    

		public RebateAgent(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;
		}
		
		public DataSet ShortSaleBillingSummaryGet(string startDate, string stopDate, string groupCode, string accountNumber)
		{
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;

				if(!startDate.Equals(""))
				{
					SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);			
					paramStartDate.Value = startDate;
				}

				if(!stopDate.Equals(""))
				{
					SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);			
					paramStopDate.Value = stopDate;
				}

				if(!accountNumber.Equals(""))
				{
					SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar, 8);			
					paramAccountNumber.Value = accountNumber;
				}

				if(!groupCode.Equals(""))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
					paramGroupCode.Value = groupCode;
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);   
				dataAdapter.Fill(dataSet, "BillingSummary");

				dataSet.Tables["BillingSummary"].PrimaryKey = new DataColumn[1]
			{				
				dataSet.Tables["BillingSummary"].Columns["ProcessId"]
			};

			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingSummaryGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public DataSet ShortSaleBillingSummaryTradingGroupEmailGet (
			string groupCode,
			int utcOffset)
		{
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryTradingGroupEmailGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;				
				
				if(!groupCode.Equals(""))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
					paramGroupCode.Value = groupCode;
				}
								
				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);			
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);   
				dataAdapter.Fill(dataSet, "TradingGroupEmails");				
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingSummaryAccountsMethodGet]", Log.Error, 1);
				throw;
			}
			
			return dataSet;
		}


		public void ShortSaleBillingSummaryTradingGroupEmailSet (
			string groupCode,
			string emailAddress,
			string actUserId)
		{
			DataSet dataSet = new DataSet();
			SqlConnection dbCn = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);

				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryTradingGroupEmailSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				
				SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
				paramGroupCode.Value = groupCode;
				
				SqlParameter paramEmailAddress = dbCmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 25);			
				paramEmailAddress.Value = emailAddress;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			
				Log.Write("Set email address: " +  groupCode + "|" + emailAddress + "|" + actUserId + ". [ShortSaleAgent.ShortSaleBillingSummaryTradingGroupEmailSet]", Log.Error, 1);						
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingSummaryTradingGroupEmailSet]", Log.Error, 1);
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
	

		public DataSet ShortSaleBillingCorrespondentSummaryGet(
			string startDate,
			string stopDate,
			string groupCode)
		{
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingCorrespondentSummaryGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);			
				paramStartDate.Value = startDate;

				SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);			
				paramStopDate.Value = stopDate;

				if(!groupCode.Equals(""))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
					paramGroupCode.Value = groupCode;
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);   
				dataAdapter.Fill(dataSet, "CorrespondentSummary");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingCorrespondentSummaryGet]", Log.Error, 1);
			}

			return dataSet;
		}


		public int ShortSaleBillingSummaryMarkSet(
			string startDate, 
			string stopDate, 
			string groupCode, 
			string accountNumber,
			bool overWrite, 
			string actUserId)
		{
			int recordsUpdated = 0;
			
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			
			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryOvernightModifiedChargeSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				
				SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);			
				paramStartDate.Value = startDate;
				
				SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);			
				paramStopDate.Value = stopDate;
				
		
				if (!groupCode.Equals(""))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode",SqlDbType.VarChar, 5);			
					paramGroupCode.Value = groupCode;
				}
				
				if(!accountNumber.Equals(""))
				{
					SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar, 8);			
					paramAccountNumber.Value = accountNumber;
				}

				SqlParameter paramOverwrite = dbCmd.Parameters.Add("@Overwrite", SqlDbType.Bit);
				paramOverwrite.Value = overWrite;
				
				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);			
				paramActUserId.Value = actUserId;

				SqlParameter paramRecordsUpdated = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);			
				paramRecordsUpdated.Direction = ParameterDirection.Output;				

				dbCn.Open();
				dbCmd.ExecuteNonQuery();	

				recordsUpdated = int.Parse(paramRecordsUpdated.Value.ToString());			
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingSummaryMarkSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}

			return recordsUpdated;
		}

		
		public DataSet ShortSaleBillingSummaryRecordGet(			
			string processId,
			string bizDate)
		{
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryRecordGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;
				
				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.BigInt);
				paramProcessId.Value = processId;
				
				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);			
				paramBizDate.Value = bizDate;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);   
				dataAdapter.Fill(dataSet, "BillingRecord");

				
				Log.Write("Updated Negative Rebate Billing Summary Record : " +  processId + "|" + bizDate + ". [ShortSaleAgent.ShortSaleBillingSummaryRecordGet]", Log.Error, 1);
				
				dataSet.Tables["BillingRecord"].PrimaryKey = new DataColumn[2]
			{
				dataSet.Tables["BillingRecord"].Columns["BizDate"],
				dataSet.Tables["BillingRecord"].Columns["ProcessId"]
			};
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingSummaryRecordGet]", Log.Error, 1);
				throw;
			}
			
			return dataSet;
		}

		public void ShortSaleBillingSummaryRecordSet (
			string processId,
			string bizDate,
			string rate,
			string price,
			string originalCharge,
			string comment,
			string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryRecordSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.BigInt);
				paramProcessId.Value = processId;
				
				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);			
				paramBizDate.Value = bizDate;
				
				if(!rate.Equals(""))
				{
					SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Decimal);			
					paramRate.Value = rate;
				}
		
				if(!price.Equals(""))
				{
					SqlParameter paramPrice = dbCmd.Parameters.Add("@Price", SqlDbType.Decimal);			
					paramPrice.Value = price;
				}

				if(!originalCharge.Equals(""))
				{
					SqlParameter paramOriginalCharge = dbCmd.Parameters.Add("@OriginalCharge", SqlDbType.Decimal);			
					paramOriginalCharge.Value = originalCharge;
				}

				if(!comment.Equals(""))
				{
					SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 255);			
					paramComment.Value = comment;
				}
				
				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);			
				paramActUserId.Value = actUserId;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingSummaryRecordSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}
		
		public int ShortSaleBillingSummaryDatesLock(
			string startDate, 
			string stopDate, 
			string groupCode, 
			string accountNumber, 
			bool isLocked, 
			string actUserId)
		{
			int recordsUpdated = 0;
			
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryDatesLock", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);			
				paramStartDate.Value = startDate;
				
				SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);			
				paramStopDate.Value = stopDate;
				
				if(!groupCode.Equals(""))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
					paramGroupCode.Value = groupCode;
				}
		
				if(!accountNumber.Equals(""))
				{
					SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar, 8);			
					paramAccountNumber.Value = accountNumber;
				}

				SqlParameter paramIsLocked = dbCmd.Parameters.Add("@Lock", SqlDbType.Bit);
				paramIsLocked.Value = isLocked;
				
				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);			
				paramActUserId.Value = actUserId;

				SqlParameter paramRecordsUpdated = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);			
				paramRecordsUpdated.Direction = ParameterDirection.Output;				

		
				dbCn.Open();
				dbCmd.ExecuteNonQuery();

				recordsUpdated = int.Parse(paramRecordsUpdated.Value.ToString());
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingSummaryDatesLock]", Log.Error, 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}

			return recordsUpdated;
		}		
		
				
		public string ShortSaleBillingSummaryAccountsBorrowedBillGet(
			string startDate, 
			string stopDate)
		{
			string bill = "";

			ShortSaleNegativeRebateBillDocument shortSaleBill = new ShortSaleNegativeRebateBillDocument(dbCnStr, startDate, stopDate, "");
			
			bill =  shortSaleBill.AccountsBorrowedEmailBill();			

			return bill;
		}

		public string ShortSaleBillingSummaryMasterBillGet(
			string startDate, 
			string stopDate)
		{
			string bill = "";

			ShortSaleNegativeRebateBillDocument shortSaleBill = new ShortSaleNegativeRebateBillDocument(dbCnStr, startDate, stopDate, "");
			
			bill =  shortSaleBill.MasterBill();			
	
			return bill;
		}

		public string ShortSaleBillingSummaryBillGet(
			string startDate, 
			string stopDate, 
			string groupCode)
		{
			string bill = "";

			ShortSaleNegativeRebateBillDocument shortSaleBill = new ShortSaleNegativeRebateBillDocument(dbCnStr, startDate, stopDate, groupCode);
			
			bill =  shortSaleBill.CorresspondentBill(true, true);			

			return bill;
		}


		public void ShortSaleBillingSummaryBillingReportGet(
			string	startDate, 
			string	stopDate, 
			string	groupCode,
			ref long		groupCodeCount,
			ref long		secIdCount,
			ref decimal totalCharges)
		{						
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryBillingReportGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);			
				paramStartDate.Value = startDate;
				
				SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);			
				paramStopDate.Value = stopDate;
				
				if(!groupCode.Equals(""))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
					paramGroupCode.Value = groupCode;
				}
					
				SqlParameter paramGroupCodeCount = dbCmd.Parameters.Add("@GroupCodeCount", SqlDbType.BigInt);
				paramGroupCodeCount.Direction = ParameterDirection.Output;
				
				SqlParameter paramSecIdCount = dbCmd.Parameters.Add("@SecIdCount", SqlDbType.BigInt);
				paramSecIdCount.Direction = ParameterDirection.Output;

				SqlParameter paramTotalCharges = dbCmd.Parameters.Add("@TotalCharges", SqlDbType.Decimal);
				paramTotalCharges.Direction = ParameterDirection.Output;
		
				dbCn.Open();
				dbCmd.ExecuteNonQuery();

				groupCodeCount = long.Parse(paramGroupCodeCount.Value.ToString());
				secIdCount =  long.Parse(paramSecIdCount.Value.ToString());
							
				totalCharges = decimal.Parse(paramTotalCharges.Value.ToString());			
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingSummaryBillingReportGet]", Log.Error, 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}

		
		public DataSet ShortSaleBillingSummaryAccountsMethodGet (
			string groupCode,
			int utcOffset)
		{
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryAccountsMethodGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;				
				
				if(!groupCode.Equals(""))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
					paramGroupCode.Value = groupCode;
				}
								
				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);			
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);   
				dataAdapter.Fill(dataSet, "Accounts");				
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingSummaryAccountsMethodGet]", Log.Error, 1);
				throw;
			}
			
			return dataSet;
		}

		
		public void ShortSaleBillingSummaryAccountMethodSet (
			string groupCode,
			string accountNumber,
			bool isOSIBilling,
			bool isPaperBilling,			
			string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryAccountMethodSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;				
			
				SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
				paramGroupCode.Value = groupCode;
					
				SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar, 8);			
				paramAccountNumber.Value = accountNumber;
				
				SqlParameter paramIsOSIBilling = dbCmd.Parameters.Add("@IsOSIBilling", SqlDbType.Bit);
				paramIsOSIBilling.Value = isOSIBilling;
				
				SqlParameter paramIsPaperBilling = dbCmd.Parameters.Add("@IsPaperBilling", SqlDbType.Bit);
				paramIsPaperBilling.Value = isPaperBilling;
											
				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);			
				paramActUserId.Value = actUserId;
		
				dbCn.Open();
				dbCmd.ExecuteNonQuery();	
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.ShortSaleBillingSummaryAccountMethodSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}

		
		public void ShortSaleBillingSummaryTradingGroupsSet (
			string groupCode, 			
			string negativeRebateMarkUp,
			string negativeRebateMarkUpCode,
			string negativeRebateBill,
			string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryTradingGroupSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
				paramGroupCode.Value = groupCode;				
			
				SqlParameter paramNegativeRebateMarkUp = dbCmd.Parameters.Add("@NegativeRebateMarkUp", SqlDbType.Decimal);
				if (!negativeRebateMarkUp.Equals(""))
				{
					paramNegativeRebateMarkUp.Value = negativeRebateMarkUp;
				}
				
				SqlParameter paramNegativeRebateMarkUpCode = dbCmd.Parameters.Add("@NegativeRebateMarkUpCode", SqlDbType.VarChar, 1);
				if (!negativeRebateMarkUpCode.Equals(""))
				{
					paramNegativeRebateMarkUpCode.Value = negativeRebateMarkUpCode;
				}

				SqlParameter paramNegativeRebateBill = dbCmd.Parameters.Add("@NegativeRebateBill", SqlDbType.Bit);
				if (!negativeRebateBill.Equals(""))
				{
					paramNegativeRebateBill.Value = bool.Parse(negativeRebateBill);
				}

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;
					
				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [RebateAgent.ShortSaleBillingSummaryTradingGroupsSet]", Log.Error, 1);
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

		
		public void ShortSaleBillingSummaryTradingGroupsAccountMarkSet(
			string groupCode,
			string accountNumber,
			string negativeRebateMarkUp,
			string negativeRebateMarkUpCode,
			bool delete,
			string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryTradingGroupAccountMarkSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
				paramGroupCode.Value = groupCode;

				SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar, 8);
				paramAccountNumber.Value = accountNumber;
				
				SqlParameter paramNegativeRebateMarkUp = dbCmd.Parameters.Add("@NegativeRebateMarkUp", SqlDbType.Decimal);
				if (!negativeRebateMarkUp.Equals(""))
				{
					paramNegativeRebateMarkUp.Value = negativeRebateMarkUp;
				}

				SqlParameter paramNegativeRebateMarkUpCode = dbCmd.Parameters.Add("@NegativeRebateMarkUpCode", SqlDbType.VarChar, 1);
				if (!negativeRebateMarkUpCode.Equals(""))
				{
					paramNegativeRebateMarkUpCode.Value = negativeRebateMarkUpCode;
				}

				SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);
				paramDelete.Value = delete;
							
				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [RebateAgent.ShortSaleBillingSummaryTradingGroupsAccountMarkSet]", Log.Error, 1);
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

		
		public void ShortSaleBillingSummaryTradingGroupsOfficeCodeMarkSet(
			string groupCode,
			string officeCode,
			string negativeRebateMarkUp,
			string negativeRebateMarkUpCode,
			string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryTradingGroupOfficeCodeMarkSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
				paramGroupCode.Value = groupCode;

				SqlParameter paramOfficeCode = dbCmd.Parameters.Add("@OfficeCode", SqlDbType.VarChar, 3);
				paramOfficeCode.Value = officeCode;

				SqlParameter paramNegativeRebateMarkUp = dbCmd.Parameters.Add("@NegativeRebateMarkUp", SqlDbType.Decimal);
				if (!negativeRebateMarkUp.Equals(""))
				{
					paramNegativeRebateMarkUp.Value = negativeRebateMarkUp;
				}
				
				SqlParameter paramNegativeRebateMarkUpCode = dbCmd.Parameters.Add("@NegativeRebateMarkUpCode", SqlDbType.VarChar, 1);
				if (!negativeRebateMarkUpCode.Equals(""))
				{
					paramNegativeRebateMarkUpCode.Value = negativeRebateMarkUpCode;
				}

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [RebateAgent.TradingGroupsOfficeCodeMarkSet]", Log.Error, 1);
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

		
		public DataSet ShortSaleBillingSummaryActivityGet (
			string groupCode, 
			int utcOffset)
		{
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingActivityGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;				
				
				if (!groupCode.Equals(""))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
					paramGroupCode.Value = groupCode;
				}
		
				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);			
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);   
				dataAdapter.Fill(dataSet, "Activity");				
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [RebateAgent.ShortSaleBillingSummaryActivityGet]", Log.Error, 1);
				throw;
			}
			
			return dataSet;
		}
		
		
		public override object InitializeLifetimeService()
    {
      return null;
    }
  }
}
