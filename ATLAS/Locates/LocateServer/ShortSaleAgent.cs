
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using StockLoan.Common;
using Compression;

namespace StockLoan.Locates
{
	public class ShortSaleAgent : MarshalByRefObject, IShortSale
	{
		private string dbCnStr;
		private DataSet dsLocatesSupport = null;
		public DataSet dsLocatesActions = null;

		public ShortSaleAgent(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;

			try
			{

				dsLocatesSupport = new DataSet();
				dsLocatesSupport.Tables.Add("Editing");

				dsLocatesSupport.Tables["Editing"].Columns.Add("LocateId");
				dsLocatesSupport.Tables["Editing"].Columns.Add("StartTime");
				dsLocatesSupport.Tables["Editing"].Columns.Add("EndTime");
				dsLocatesSupport.Tables["Editing"].Columns.Add("ActUserId");

				dsLocatesSupport.AcceptChanges();


				dsLocatesActions = new DataSet();
				dsLocatesActions.Tables.Add("Actions");

				dsLocatesActions.Tables["Actions"].Columns.Add("GroupCode");
				dsLocatesActions.Tables["Actions"].Columns.Add("Action");
				dsLocatesActions.Tables["Actions"].Columns.Add("StartTime");
				dsLocatesActions.Tables["Actions"].Columns.Add("EndTime");
				dsLocatesActions.Tables["Actions"].Columns.Add("ActUserId");

				dsLocatesActions.AcceptChanges();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleAgent.ShortSaleAgent]", Log.Error, 1);
			}
		}

		public string BizDatePrior(string bizDate)
		{
			string countryCode = Standard.ConfigValue("CountryCode", "US");

			DateTime bizDateDateTime = DateTime.Parse(bizDate);
			DateTime bizDatePrior = bizDateDateTime;

			while (true)
			{
				bizDatePrior = bizDatePrior.AddDays(-1.0);

				if (Standard.IsBizDate(bizDatePrior, countryCode, Standard.HolidayType.Exchange, dbCnStr))
				{
					break;
				}
			}

			Log.Write("Returning a prior biz date of " + bizDatePrior.ToString(Standard.DateFormat) + ". [ShortSaleAgent.BizDatePrior]", 3);

			return bizDatePrior.ToString(Standard.DateFormat);
		}

		public string TradeDate()
		{
			return Master.BizDateExchange;
		}

		public void LocatesBeginEdit(string locateId, string actUserId)
		{
			try
			{
				for (int i = 0; i < dsLocatesSupport.Tables["Editing"].Rows.Count; i++)
				{
					if ((dsLocatesSupport.Tables["Editing"].Rows[i]["EndTime"].ToString()).Equals("") &&
						  (dsLocatesSupport.Tables["Editing"].Rows[i]["LocateId"].ToString()).Equals(locateId))
					{
						throw new Exception("Locate already being edited!");
					}
				}

				DataRow dr = dsLocatesSupport.Tables["Editing"].NewRow();

				dr["LocateId"] = locateId;
				dr["StartTime"] = DateTime.Now.ToString();
				dr["EndTime"] = "";
				dr["ActUserId"] = actUserId;

				dsLocatesSupport.Tables["Editing"].Rows.Add(dr);
				dsLocatesSupport.AcceptChanges();

				Log.Write("LocateId: " + locateId + " has entered editing state.", 3);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [ShortSaleAgent.LocatesBeginEdit]", 3);
				throw;
			}
		}

		public void LocatesEndEdit(string locateId, string actUserId)
		{
			try
			{
				for (int i = 0; i < dsLocatesSupport.Tables["Editing"].Rows.Count; i++)
				{
					if ((dsLocatesSupport.Tables["Editing"].Rows[i]["EndTime"].ToString()).Equals("") &&
						  (dsLocatesSupport.Tables["Editing"].Rows[i]["LocateId"].ToString()).Equals(locateId))
					{
						dsLocatesSupport.Tables["Editing"].Rows[i]["EndTime"] = DateTime.Now.ToString();
					}
				}

				dsLocatesSupport.AcceptChanges();

				Log.Write("LocateId: " + locateId + " has exited editing state.", 3);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [ShortSaleAgent.LocatesEndEdit]", 3);
				throw;
			}
		}

		public DataSet LocatesEditing()
		{
			try
			{
				lock (this)
				{
					for (int i = 0; i < dsLocatesSupport.Tables["Editing"].Rows.Count; i++)
					{
						if (!(dsLocatesSupport.Tables["Editing"].Rows[i]["StartTime"].ToString()).Equals("") &&
							  !(dsLocatesSupport.Tables["Editing"].Rows[i]["EndTime"].ToString()).Equals(""))
						{
							dsLocatesSupport.Tables["Editing"].Rows[i].Delete();
						}
					}
				}

				dsLocatesSupport.AcceptChanges();

				Log.Write("Returning: " + dsLocatesSupport.Tables["Editing"].Rows.Count.ToString("#,##0") + " locates in editing state.", 3);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [ShortSaleAgent.LocatesEditing]", 3);
				throw;
			}

			return dsLocatesSupport;
		}



		private void LocatesBeginActions(string groupCode, string actions, string actUserId)
		{
			try
			{
				DataRow dr = dsLocatesActions.Tables["Actions"].NewRow();

				dr["GroupCode"] = groupCode;
				dr["Action"] = actions;
				dr["StartTime"] = DateTime.Now.ToString();
				dr["ActUserId"] = actUserId;

				dsLocatesActions.Tables["Actions"].Rows.Add(dr);
				dsLocatesActions.AcceptChanges();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [ShortSaleAgent.LocatesBeginActions]", 3);
			}
		}


		private void LocatesEndActions(string groupCode, string actions)
		{
			try
			{
				for (int i = 0; i < dsLocatesActions.Tables["Actions"].Rows.Count; i++)
				{
					if (dsLocatesActions.Tables["Actions"].Rows[i]["GroupCode"].ToString().Equals(groupCode) &&
						dsLocatesActions.Tables["Actions"].Rows[i]["Actions"].ToString().Equals(actions))
					{
						dsLocatesActions.Tables["Actions"].Rows[i]["EndTime"] = DateTime.Now.ToString();
						break;
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [ShortSaleAgent.LocatesEndActions]", 3);
			}
		}


		public DataSet LocatesActions()
		{
			return dsLocatesActions;
		}

		public DataSet LocatesPreBorrowGet(
		  string bizDate,
		  string groupCode,
		  short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spPreBorrowGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDateExchange;

				if (!groupCode.Equals(""))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
					paramGroupCode.Value = groupCode;
				}

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "PreBorrows");

				Log.Write("Returning a 'PreBorrows' table with " + dataSet.Tables["PreBorrows"].Rows.Count + " rows. [ShortSaleAgent.LocatesPreBorrowGet]", 3);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.LocatesPreBorrowGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public void LocatesPreBorrowSet(
			string bizDate,
			string groupCode,
			string secId,
			string quantity,
			string rebateRate,
			string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spPreBorrowSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDateExchange;

				SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
				paramGroupCode.Value = groupCode;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
				paramQuantity.Value = quantity;

				if (!rebateRate.Equals(""))
				{
					SqlParameter paramRebateRate = dbCmd.Parameters.Add("@RebateRate", SqlDbType.Float);
					paramRebateRate.Value = rebateRate;
				}

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.LocatesPreBorrowSet]", Log.Error, 1);
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

		public void LocatePreBorrowSubmit(
			long locateId,
			string groupCode,
			string secId,
			string quantity,
			string rate,
			string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spPreBorrowRequest", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramLocateId = dbCmd.Parameters.Add("@LocateId", SqlDbType.BigInt);
				paramLocateId.Value = locateId;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDateExchange;

				SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
				paramGroupCode.Value = groupCode;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
				paramQuantity.Value = quantity;

				if (!rate.Equals(""))
				{
					SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Decimal);
					paramRate.Value = rate;
				}

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.LocatePreBorrowSubmit]", Log.Error, 1);
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


		public DataSet TradingGroupsAccountMarkGet(string tradingGroup, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spTradingGroupAccountMarkGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				if (!tradingGroup.Equals(""))
				{
					SqlParameter paramTradingGroup = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
					paramTradingGroup.Value = tradingGroup;
				}

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "TradingGroupAccountMarks");

				Log.Write("Returning a 'TradingGroups' table with " + dataSet.Tables["TradingGroupAccountMarks"].Rows.Count + " rows. [ShortSaleAgent.TradingGroupsAccountMarkGet]", 3);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.TradingGroupsAccountMarkGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public void TradingGroupsAccountMarkSet(
			string groupCode,
			string accountNumber,
			string negativeRebateMarkUp,
			string positiveRebateMarkUp,
			string fedFundsMarkUp,
			string liborFundsMarkUp,
			bool delete,
			string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spTradingGroupAccountMarkSet", dbCn);
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

				SqlParameter paramPositiveRebateMarkUp = dbCmd.Parameters.Add("@PositiveRebateMarkUp", SqlDbType.Decimal);
				if (!positiveRebateMarkUp.Equals(""))
				{
					paramPositiveRebateMarkUp.Value = positiveRebateMarkUp;
				}

				SqlParameter paramFedFunds = dbCmd.Parameters.Add("@FedFundsMarkUp", SqlDbType.Decimal);
				if (!fedFundsMarkUp.Equals(""))
				{
					paramFedFunds.Value = fedFundsMarkUp;
				}

				SqlParameter paramLiborFunds = dbCmd.Parameters.Add("@LiborFundsMarkUp", SqlDbType.Decimal);
				if (!liborFundsMarkUp.Equals(""))
				{
					paramLiborFunds.Value = liborFundsMarkUp;
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
				Log.Write(e.Message + " [ShortSaleAgent.TradingGroupsAccountMarkSet]", Log.Error, 1);
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}

		public DataSet TradingGroupsOfficeCodeMarkGet(string tradingGroup, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spTradingGroupOfficeCodeMarkGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				if (!tradingGroup.Equals(""))
				{
					SqlParameter paramTradingGroup = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
					paramTradingGroup.Value = tradingGroup;
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "TradingGroupOfficeCodeMarks");

				Log.Write("Returning a 'TradingGroups' table with " + dataSet.Tables["TradingGroupOfficeCodeMarks"].Rows.Count + " rows. [ShortSaleAgent.TradingGroupsOfficeCodeMarkGet]", 3);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.TradingGroupsOfficeCodeMarkGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public void TradingGroupsOfficeCodeMarkSet(
			string groupCode,
			string officeCode,
			string negativeRebateMarkUp,
			string positiveRebateMarkUp,
			string fedFundsMarkUp,
			string liborFundsMarkUp,
			string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spTradingGroupOfficeCodeMarkSet", dbCn);
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

				SqlParameter paramPositiveRebateMarkUp = dbCmd.Parameters.Add("@PositiveRebateMarkUp", SqlDbType.Decimal);
				if (!positiveRebateMarkUp.Equals(""))
				{
					paramPositiveRebateMarkUp.Value = positiveRebateMarkUp;
				}

				SqlParameter paramFedFunds = dbCmd.Parameters.Add("@FedFundsMarkUp", SqlDbType.Decimal);
				if (!fedFundsMarkUp.Equals(""))
				{
					paramFedFunds.Value = fedFundsMarkUp;
				}

				SqlParameter paramLiborFunds = dbCmd.Parameters.Add("@LiborFundsMarkUp", SqlDbType.Decimal);
				if (!liborFundsMarkUp.Equals(""))
				{
					paramLiborFunds.Value = liborFundsMarkUp;
				}

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.TradingGroupsOfficeCodeMarkSet]", Log.Error, 1);
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}

		public DataSet TradingGroupsGet(string tradeDate, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spTradingGroupGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				if (!tradeDate.Equals(""))
				{
					SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
					paramTradeDate.Value = tradeDate;
				}
				else
				{
					SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
					paramTradeDate.Value = Master.BizDateExchange;
				}

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "TradingGroups");

				Log.Write("Returning a 'TradingGroups' table with " + dataSet.Tables["TradingGroups"].Rows.Count + " rows. [ShortSaleAgent.TradingGroupsGet]", 3);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.TradingGroupsGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public void TradingGroupSet(
		  string groupCode,
		  string groupName,
		  string minPrice,
		  string autoApprovalMax,
		  string premiumMin,
		  string premiumMax,
		  bool autoEmail,
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
				if (!autoApprovalMax.Equals("") && (Tools.IsNumeric(autoApprovalMax)))
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

				SqlParameter paramAccountParametersEdit = dbCmd.Parameters.Add("@AccountParametersEdit", SqlDbType.Bit);
				paramAccountParametersEdit.Value = 1;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.TradingGroupSet]", Log.Error, 1);
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





		public string LocateListSubmit(string clientId, string groupCode, string clientComment, string locateList)
		{
			List list = new List();

			if (list.Parse(locateList).Equals("OK"))
			{
				SqlDataReader dataReader = null;
				SqlConnection dbCn = new SqlConnection(dbCnStr);

				SqlCommand dbCmd = new SqlCommand("dbo.spShortSaleLocateRequest", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
				paramTradeDate.Value = Master.BizDateExchange;

				SqlParameter paramClientId = dbCmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 25);
				paramClientId.Value = clientId;

				SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
				paramGroupCode.Value = groupCode;

				SqlParameter paramClientComment = dbCmd.Parameters.Add("@ClientComment", SqlDbType.VarChar, 50);
				paramClientComment.Value = clientComment;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				SqlParameter paramClientQuantity = dbCmd.Parameters.Add("@ClientQuantity", SqlDbType.BigInt);

				SqlParameter paramReturnData = dbCmd.Parameters.Add("@ReturnData", SqlDbType.Bit);
				paramReturnData.Value = 0;

				int n = 0;

				try
				{
					dbCn.Open();

					for (int i = 0; i < list.Count; i++)
					{
						//Check locatestock.com for quantity

						paramSecId.Value = list.ItemGet(i).SecId;
						paramClientQuantity.Value = list.ItemGet(i).Quantity;

						dataReader = dbCmd.ExecuteReader();

						while (dataReader.Read())
						{
							n += 1;
						}

						dataReader.Close();
					}

					if (n.Equals(list.Count))
					{
						Log.Write("Processed " + (list.Count) + " item[s] for " + clientId + "|" + groupCode + ". [ShortSaleAgent.LocateListSubmit]", 2);
					}
					else
					{
						Log.Write("Error: Processed " + n + " item[s] for " + clientId + "|" + groupCode +
							" out of an expected " + (list.Count) + " item[s]. [ShortSaleAgent.LocateListSubmit]", 2);

						return "Error: Processed " + n + " item[s] out of an expected " + (list.Count) + " item[s].";
					}

					Log.Write("Processed " + (list.Count) + " item[s] for " + clientId + "|" + groupCode + ". [ShortSaleAgent.LocateListSubmit]", 2);
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [ShortSaleAgent.LocateListSubmit]", Log.Error, 1);

					return "Error processing request.";
				}
				finally
				{
					if (!dataReader.IsClosed)
					{
						dataReader.Close();
					}

					if (dbCn.State != ConnectionState.Closed)
					{
						dbCn.Close();
					}
				}

				return "List of " + list.Count.ToString("#,##0") + " item[s] has been submitted.";
			}
			else
			{
				return list.Status;
			}
		}

		public string LocateSet(string clientId, string groupCode, string clientComment, string secId, string quantity)
		{
			SqlParameter paramLocatedId = null;

			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{

				SqlCommand dbCmd = new SqlCommand("dbo.spShortSaleLocateRequest", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
				paramTradeDate.Value = Master.BizDateExchange;

				SqlParameter paramClientId = dbCmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 25);
				paramClientId.Value = clientId;

				SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
				paramGroupCode.Value = groupCode;

				SqlParameter paramClientComment = dbCmd.Parameters.Add("@ClientComment", SqlDbType.VarChar, 50);
				paramClientComment.Value = clientComment;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramClientQuantity = dbCmd.Parameters.Add("@ClientQuantity", SqlDbType.BigInt);
				paramClientQuantity.Value = quantity;

				SqlParameter paramReturnData = dbCmd.Parameters.Add("@ReturnData", SqlDbType.Bit);
				paramReturnData.Value = 0;

				paramLocatedId = dbCmd.Parameters.Add("@LocateId", SqlDbType.BigInt);
				paramLocatedId.Direction = ParameterDirection.Output;


				dbCn.Open();
				dbCmd.ExecuteNonQuery();

				Log.Write("Set item: " + paramLocatedId.Value.ToString() + " [ShortSaleAgent.LocateItemSet]", 2);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleAgent.LocateSet]", Log.Error, 1);
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}

			return paramLocatedId.Value.ToString();
		}


		public void LocateItemSet(
			long locateId,
			string quantity,
			string source,
			string feeRate,
			string preBorrow,
			string comment,
			string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			SqlCommand dbCmd = new SqlCommand("spShortSaleLocateSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			SqlParameter paramLocateId = dbCmd.Parameters.Add("@LocateId", SqlDbType.BigInt);
			paramLocateId.Value = locateId;

			if (!quantity.Equals(""))
			{
				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
				paramQuantity.Value = quantity;
			}

			if (!feeRate.Equals(""))
			{
				SqlParameter paramFeeRate = dbCmd.Parameters.Add("@FeeRate", SqlDbType.Float);
				paramFeeRate.Value = feeRate;
			}

			if (!comment.Equals(""))
			{
				SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
				paramComment.Value = comment;
			}

			if (!preBorrow.Equals(""))
			{
				SqlParameter paramPreBorrow = dbCmd.Parameters.Add("@PreBorrow", SqlDbType.Bit);
				paramPreBorrow.Value = preBorrow;
			}

			if (!actUserId.Equals(""))
			{
				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;
			}

			if (!source.Equals(""))
			{
				SqlParameter paramSource = dbCmd.Parameters.Add("@Source", SqlDbType.VarChar, 50);
				paramSource.Value = source;
			}

			SqlParameter paramReturnData = dbCmd.Parameters.Add("@ReturnData", SqlDbType.Bit);
			paramReturnData.Value = 1;

			try
			{
				dbCn.Open();
				dbCmd.ExecuteNonQuery();

				Log.Write("Set item: " + locateId.ToString() + " [ShortSaleAgent.LocateItemSet]", 2);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.LocateItemSet]", Log.Error, 1);
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}

		public DataSet LocateDatesGet()
		{
			return LocateDatesGet(null);
		}

		public DataSet LocateDatesGet(string clientId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleLocateDates", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				if (!(clientId == null))
				{
					SqlParameter paramClientId = dbCmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 25);
					paramClientId.Value = clientId;
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

				dataAdapter.Fill(dataSet, "LocateDates");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.LocateDatesGet]", Log.Error, 0);
			}

			return dataSet;
		}


		public DataSet LocateItemGet(string locateId, short utcOffset)
		{
			return LocateItemGet("", locateId, utcOffset);
		}

		public DataSet LocateItemGet(string groupCode, string locateId, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleLocateGetTMP", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				if (!groupCode.Equals(""))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
					paramGroupCode.Value = groupCode;
				}

				if (!locateId.Equals(""))
				{
					SqlParameter paramLocateId = dbCmd.Parameters.Add("@LocateId", SqlDbType.BigInt);
					paramLocateId.Value = locateId;
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

				dataAdapter.Fill(dataSet);

				Log.Write("Get item: " + locateId + " [ShortSaleAgent.LocateItemGet]", 2);
			}
			catch (Exception e)
			{
				Log.Write(e.StackTrace + " [ShortSaleAgent.LocateItemGet]", Log.Error, 1);
			}
			finally
			{
				dbCn.Close();
			}

			return dataSet;
		}

        public DataSet LocatesGet(string tradeDate, string groupCode, string secId, short utcOffset)
        {
            return LocatesByStatusGet(tradeDate, groupCode, secId, "", utcOffset);
        }
		
        public DataSet LocatesByStatusGet(string tradeDate, string groupCode, string secId, string status, short utcOffset)
        {
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleLocateGetTMP", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				if (!(tradeDate.Equals("")))
				{
					SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
					paramTradeDate.Value = tradeDate;
				}

				if (!(groupCode.Equals("")))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
					paramGroupCode.Value = groupCode;
				}

				if (!(secId.Equals("")))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}

                if (!(status.Equals("")))
                {
                    SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.VarChar, 10);
                    paramStatus.Value = status;
                }

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

				dataAdapter.Fill(dataSet);

				Log.Write("Returning a 'Locates' table with " + dataSet.Tables["Table"].Rows.Count + " rows. [ShortSaleAgent.LocatesGet]", 2);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.LocatesGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public long LocatesCountGet(string tradeDate, string status)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();
			long numberOfLocates = 0;

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleLocateGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
			
				SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
				paramTradeDate.Value = tradeDate;

				SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.VarChar, 10);
				paramStatus.Value = status;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

				dataAdapter.Fill(dataSet);

				Log.Write("Returning a 'Locates' table with " + dataSet.Tables["Table"].Rows.Count + " rows. [ShortSaleAgent.LocatesGet]", 2);

				numberOfLocates = dataSet.Tables["Table"].Rows.Count;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.LocatesPendingGet]", Log.Error, 1);
			}

			return numberOfLocates;
		}


		public byte[] LocatesCompressedGet(string tradeDateMin, string tradeDateMax, string groupCode, string secId, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();
			byte[] compressedData = null;

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleLocateResearch", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				if (!(tradeDateMin.Equals("")))
				{
					SqlParameter paramTradeDateMin = dbCmd.Parameters.Add("@TradeDateMin", SqlDbType.DateTime);
					paramTradeDateMin.Value = tradeDateMin;
				}

				if (!(tradeDateMax.Equals("")))
				{
					SqlParameter paramTradeDateMax = dbCmd.Parameters.Add("@TradeDateMax", SqlDbType.DateTime);
					paramTradeDateMax.Value = tradeDateMax;
				}

				if (!(groupCode.Equals("")))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
					paramGroupCode.Value = groupCode;
				}

				if (!(secId.Equals("")))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

				dataAdapter.Fill(dataSet);

				compressedData = CompressionTools.CompressDataSet(dataSet);


				Log.Write("Returning a 'Locates' table with " + dataSet.Tables["Table"].Rows.Count + " rows. [ShortSaleAgent.LocatesGet]", 2);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.LocatesGet]", Log.Error, 1);
			}

			return compressedData;
		}

		public DataSet LocatesGet(string tradeDateMin, string tradeDateMax, string groupCode, string secId, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();			

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleLocateResearch", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				if (!(tradeDateMin.Equals("")))
				{
					SqlParameter paramTradeDateMin = dbCmd.Parameters.Add("@TradeDateMin", SqlDbType.DateTime);
					paramTradeDateMin.Value = tradeDateMin;
				}

				if (!(tradeDateMax.Equals("")))
				{
					SqlParameter paramTradeDateMax = dbCmd.Parameters.Add("@TradeDateMax", SqlDbType.DateTime);
					paramTradeDateMax.Value = tradeDateMax;
				}

				if (!(groupCode.Equals("")))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
					paramGroupCode.Value = groupCode;
				}

				if (!(secId.Equals("")))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

				dataAdapter.Fill(dataSet);

				Log.Write("Returning a 'Locates' table with " + dataSet.Tables["Table"].Rows.Count + " rows. [ShortSaleAgent.LocatesGet]", 2);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.LocatesGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public DataSet LocatesSecIdSummaryGet(string tradeDate)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleLocateSecIdSummaryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				dbCmd.CommandTimeout = int.Parse(KeyValue.Get("ShortSaleLocateDataTimeout", "300", dbCnStr));


				SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
				paramTradeDate.Value = tradeDate;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

				dataAdapter.Fill(dataSet, "SecIdSummary");

				Log.Write("Returning a 'Locates Summary' table with " + dataSet.Tables["SecIdSummary"].Rows.Count + " rows. [ShortSaleAgent.LocatesSecIdSummaryGet]", 2);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.LocatesSecIdSummaryGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public DataSet LocatesSummaryGet(string tradeDate)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spShortSaleLocatesSummaryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				dbCmd.CommandTimeout = int.Parse(KeyValue.Get("ShortSaleLocateDataTimeout", "300", dbCnStr));


				SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
				paramTradeDate.Value = tradeDate;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

				dataAdapter.Fill(dataSet, "LocatesSummary");

				Log.Write("Returning a 'Locates Summary' table with " + dataSet.Tables["LocatesSummarymbat"].Rows.Count + " rows. [ShortSaleAgent.LocatesSummaryGet]", 2);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.LocatesSummaryGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public DataSet BorrowHardGet(bool showHistory, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spBorrowHardGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramShowHistory = dbCmd.Parameters.Add("@ShowHistory", SqlDbType.Bit);
				paramShowHistory.Value = showHistory;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "BorrowHard");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.BorrowHardGet]", 2);
				throw;
			}
			return dataSet;
		}

		public void BorrowHardSet(string secId, bool delete, string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			SqlCommand dbCmd = new SqlCommand("spBorrowHardSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			try
			{
				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);
				paramDelete.Value = delete;

				if (dbCn.State != ConnectionState.Open)
				{
					dbCn.Open();
				}
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.BororowHardSet]", Log.Error, 1);
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

		public DataSet BorrowNoGet(bool showHistory, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spBorrowNoGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramShowHistory = dbCmd.Parameters.Add("@ShowHistory", SqlDbType.Bit);
				paramShowHistory.Value = showHistory;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "BorrowNo");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.BorrowNoGet]", 2);
				throw;
			}
			return dataSet;
		}

		public void BorrowNoSet(string secId, bool delete, string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			SqlCommand dbCmd = new SqlCommand("spBorrowNoSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			try
			{
				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);
				paramDelete.Value = delete;

				if (dbCn.State != ConnectionState.Open)
				{
					dbCn.Open();
				}
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.BororowNoSet]", Log.Error, 1);
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

		public DataSet ThresholdList(string effectDate)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spThresholdList", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				if (effectDate != null && !effectDate.Equals(""))
				{
					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = effectDate;
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "ThresholdList");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.ThresholdList]", 1);
				throw;
			}

			return dataSet;
		}

		public DataSet BorrowEasyList(string effectDate, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spBorrowEasyList", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				if (effectDate != null && !effectDate.Equals(""))
				{
					SqlParameter paramBizDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
					paramBizDate.Value = effectDate;
				}

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "BorrowEasyList");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.BorrowEasyList]", 2);
				throw;
			}
			return dataSet;
		}


		public void BorrowEasyListSet(string secId, string actUserId, bool isShortSaleEasy)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spBorrowEasyListSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				SqlParameter paramIsShortSaleEasy = dbCmd.Parameters.Add("@IsShortSaleEasy", SqlDbType.Bit);
				paramIsShortSaleEasy.Value = isShortSaleEasy;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.BorrowEasyListSet]", 2);
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

		public string InventoryListSubmit(string desk, string account, string list, string deskQuip, string actUserId)
		{
			DealList inventoryList = new DealList();

			SqlConnection dbCnItem = null;
			SqlCommand dbCmdItem = null;

			SqlConnection dbCnDq = null;
			SqlCommand dbCmdDq = null;

			SqlParameter paramDqActUserId = null;
			SqlParameter paramDqDeskQuip = null;
			SqlParameter paramDqSecId = null;

			if (inventoryList.Parse(list).Equals("OK"))
			{
				dbCnItem = new SqlConnection(dbCnStr);
				dbCmdItem = new SqlCommand("spInventoryItemSet", dbCnItem);
				dbCmdItem.CommandType = CommandType.StoredProcedure;

				SqlParameter paramItemDesk = dbCmdItem.Parameters.Add("@Desk", SqlDbType.VarChar, 12);
				paramItemDesk.Value = desk;

				SqlParameter paramItemAccount = dbCmdItem.Parameters.Add("@Account", SqlDbType.VarChar, 15);
				paramItemAccount.Value = account;

				SqlParameter paramItemModeCode = dbCmdItem.Parameters.Add("@ModeCode", SqlDbType.Char, 1);
				paramItemModeCode.Value = "D";

				SqlParameter sqlParamItemSecId = dbCmdItem.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				SqlParameter sqlParamItemQuantity = dbCmdItem.Parameters.Add("@Quantity", SqlDbType.BigInt);
				SqlParameter sqlParamItemRate = dbCmdItem.Parameters.Add("@Rate", SqlDbType.Float);

				if (!deskQuip.Equals("")) // Add this quip for each security listed.
				{
					dbCnDq = new SqlConnection(dbCnStr);
					dbCmdDq = new SqlCommand("spDeskQuipSet", dbCnDq);
					dbCmdDq.CommandType = CommandType.StoredProcedure;

					paramDqActUserId = dbCmdDq.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
					paramDqActUserId.Value = actUserId;

					paramDqDeskQuip = dbCmdDq.Parameters.Add("@DeskQuip", SqlDbType.VarChar, 25);
					paramDqDeskQuip.Value = deskQuip;

					paramDqSecId = dbCmdDq.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				}

				try
				{
					dbCnItem.Open();

					if (dbCnDq != null)
					{
						dbCnDq.Open();
					}

					int n = inventoryList.Count;

					for (int i = 0; i < n; i++)
					{
						sqlParamItemSecId.Value = inventoryList.ItemGet(i).SecId;
						sqlParamItemQuantity.Value = Tools.ParseLong(inventoryList.ItemGet(i).Quantity);

						if (!inventoryList.ItemGet(i).Rate.Equals(""))
						{
							sqlParamItemRate.Value = float.Parse(inventoryList.ItemGet(i).Rate);
						}

						dbCmdItem.ExecuteNonQuery();

						if (dbCmdDq != null)
						{
							paramDqSecId.Value = inventoryList.ItemGet(i).SecId;
							dbCmdDq.ExecuteNonQuery();
						}

						Log.Write("Processed " + (inventoryList.Count) + " item[s] from " + actUserId + ". [ShortSaleAgent.InventoryListSubmit]", 2);
					}

					return "OK - Your list of " + (inventoryList.Count) + " item[s] has been submitted.";
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [ShortSaleAgent.LocateListSubmit]", Log.Error, 1);
					return "Error processing request.";
				}
				finally
				{
					if (dbCnItem.State != ConnectionState.Closed)
					{
						dbCnItem.Close();
					}

					if ((dbCnDq != null) && (dbCnDq.State != ConnectionState.Closed))
					{
						dbCnDq.Close();
					}
				}
			}
			else
			{
				return inventoryList.Status;
			}
		}

		public DataSet InventoryHistoryLookupGet(string bizDate, string secId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			string countryCode = Standard.ConfigValue("CountryCode", "US");
			string bizDatePrior = "";

			if (Standard.IsBizDate(DateTime.Parse(bizDate), countryCode, Standard.HolidayType.Exchange, dbCnStr))
			{
				bizDatePrior = BizDatePrior(bizDate);
			}
			else
			{
				bizDatePrior = bizDate;
			}

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryLookupHistoryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
				paramBizDatePrior.Value = bizDatePrior;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "Inventory");

			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.InventoryHistoryLookupGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public DataSet InventoryGet(string secId, short utcOffset)
		{
			return InventoryGet(secId, utcOffset, false);
		}


		public DataSet InventoryGet(string secId, short utcOffset, bool withHistory)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "Inventory");

				if (withHistory)
				{
					if (dataSet.Tables["Inventory"].Rows.Count > 0) // Switch to the resolved SecId.
					{
						paramSecId.Value = dataSet.Tables["Inventory"].Select()[0]["SecId"];
					}

					dbCmd.Parameters.Remove(paramUtcOffset);

					dbCmd.CommandText = "spInventoryHistoryGet";
					dataAdapter.Fill(dataSet, "History");
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.InventoryGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public DataSet InventoryFundingRatesHistoryGet(int listCount, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryFundingRatesHistoryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramListCount = dbCmd.Parameters.Add("@ListCount", SqlDbType.Int);
				paramListCount.Value = listCount;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "InventoryFundingRatesHistory");

			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.InventoryFundingRatesHistoryGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public DataSet InventoryFundingRatesGet(string bizDate, short utcOffset)
		{


			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryFundingRatesGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "InventoryFundingRates");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.InventoryFundingRatesGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public void InventoryFundingRateSet(string fedFundingRate, string liborFundingRate, string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryFundingRateSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramFedFundingRate = dbCmd.Parameters.Add("@FedFundingOpenRate", SqlDbType.Decimal);
				paramFedFundingRate.Value = fedFundingRate;

				SqlParameter paramLiborFundingRate = dbCmd.Parameters.Add("@LiborFundingRate", SqlDbType.Decimal);
				paramLiborFundingRate.Value = liborFundingRate;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.InventoryFundingRateSet]", Log.Error, 1);
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


		public DataSet InventoryGet(string groupCode, string secId, short utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				if (!groupCode.Equals(""))
				{
					SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 12);
					paramGroupCode.Value = groupCode;
				}

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "Inventory");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.InventoryGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public DataTable InventoryDeskListGet(string bizDate, string desk)
		{
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryDeskListGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);
				paramDesk.Value = desk;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "DeskList");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.InventoryGet]", Log.Error, 1);
			}

			return dataSet.Tables["DeskList"];
		}

		public DataSet InventoryRatesGet(string bizDate)
		{
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryRatesGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;

				if (!bizDate.Equals(""))
				{
					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = bizDate;
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "InventoryRates");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.InventoryRatesGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public void InventoryRateSet(string secId, string rate, string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryRateSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramRate = dbCmd.Parameters.Add("@Rate", SqlDbType.Float);
				paramRate.Value = rate;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleAgent.InventoryRateSet]", Log.Error, 1);
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
		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
