using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using StockLoan.Common;
using StockLoan.PushData;
using StockLoan.MainBusiness;

namespace StockLoan.Main
{
	public class PositionAgent : MarshalByRefObject, IPosition
	{
		public event SystemNotificationEventHandler SystemNotificationEvent;

		private string dbCnStr;
		private StockLoan.BackOffice.IBackOffice backOfficeAgent;

		public PositionAgent(string dbCnStr, ref StockLoan.BackOffice.IBackOffice backOfficeAgent)
		{
			this.dbCnStr = dbCnStr;
			this.backOfficeAgent = backOfficeAgent;
		}

		public DataSet BoxPositionGet(string bizDate, string bookGroup, string secId)
		{
			SqlDataAdapter dataAdapter = null;
			SqlCommand dbCmd = null;

			DataSet ds = new DataSet();

			try
			{
				dbCmd = new SqlCommand("spBoxPositionGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				if (!bookGroup.Equals(""))
				{
					SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
					paramBookGroup.Value = bookGroup;
				}

				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(ds, "BoxPosition");

				ds.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.CurrenciesGet]", Log.Error, 1);
			}

			return ds;
		}

		public DataSet ContractDetailsInfo(string bizDate, string bookGroup)
		{
			SqlDataAdapter dataAdapter = null;
			SqlCommand dbCmd = null;

			DataSet ds = new DataSet();

			try
			{
				dbCmd = new SqlCommand("spContractDetailsGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(ds, "ContractDetails");

				ds.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ContractDetailsInfo]", Log.Error, 1);
			}

			return ds;
		}

		public DataSet DeliveryTypesGet()
		{
			SqlDataAdapter dataAdapter = null;
			SqlCommand dbCmd = null;

			DataSet ds = new DataSet();

			try
			{
				dbCmd = new SqlCommand("spDeliveryTypesGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(ds, "DeliveryTypes");

				ds.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.DeliveryTypesGet]", Log.Error, 1);
			}

			return ds;
		}

		public DataSet FundsGet()
		{
			SqlDataAdapter dataAdapter = null;
			SqlCommand dbCmd = null;

			DataSet ds = new DataSet();

			try
			{
				dbCmd = new SqlCommand("spFundsGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(ds, "Funds");

				ds.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.FundsGet]", Log.Error, 1);
			}

			return ds;
		}

		public DataSet FundingRatesGet(string bizDate)
		{
			SqlDataAdapter dataAdapter = null;
			SqlCommand dbCmd = null;

			DataSet ds = new DataSet();

			try
			{
				dbCmd = new SqlCommand("spFundingRatesGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(ds, "FundingRates");

				ds.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.FundingRatesGet]", Log.Error, 1);
			}

			return ds;
		}

        public DataSet FundingRateResearchGet(string startDate, string stopDate,string fund)
        {
            SqlDataAdapter dataAdapter = null;
            SqlCommand dbCmd = null;

            DataSet ds = new DataSet();

            try
            {
                dbCmd = new SqlCommand("spFundingRateResearchGet", new SqlConnection(dbCnStr));
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                paramStartDate.Value = startDate;

                SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
                paramStopDate.Value = stopDate;

                SqlParameter paramFund = dbCmd.Parameters.Add("@Fund", SqlDbType.VarChar, 6);
                paramFund.Value = fund;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(ds, "FundingRates");

                ds.RemotingFormat = SerializationFormat.Binary;
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionAgent.FundingRateResearchGet]", Log.Error, 1);
            }

            return ds;
        }

		public DataSet LogicOperatorsGet()
		{
			SqlDataAdapter dataAdapter = null;
			SqlCommand dbCmd = null;

			DataSet ds = new DataSet();

			try
			{
				dbCmd = new SqlCommand("spLogicOperatorsGet", new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.StoredProcedure;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(ds, "LogicOperators");

				ds.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.LogicOperatorsGet]", Log.Error, 1);
			}

			return ds;
		}


		public void FundingRateSet(
			string bizDate,
            string fund,
			string fundingRate,
			string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spFundingRateSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = (bizDate.Equals("")) ? Master.BizDate : bizDate;

				SqlParameter paramFund = dbCmd.Parameters.Add("@Fund", SqlDbType.VarChar, 6);
				paramFund.Value = fund;

				SqlParameter paramFundingRate = dbCmd.Parameters.Add("@FundingRate", SqlDbType.Decimal);
				if (!fundingRate.Equals(""))
				{
					paramFundingRate.Value = fundingRate;
				}
				else
				{
					paramFundingRate.Value = DBNull.Value;
				}

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.FundingRateSet]", Log.Error, 1);
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


		public void DealToContract(string dealId, string bizDate)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spDealToContract", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramDealId = dbCmd.Parameters.Add("@DealId", SqlDbType.VarChar, 16);
				paramDealId.Value = dealId;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDate;

				dbCn.Open();

				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.DealToContract]", Log.Error, 1);
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

		public DataSet DealDataGet(short utcOffset, string userId, string functionPath, string isActive)
		{
			return DealDataGet(utcOffset, "", "", isActive);
		}

		public DataSet DealDataGet(short utcOffset, string bizDate, string isActive)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spDealGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
				paramIsActive.Value = isActive;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "Deals");

				dataSet.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.DealDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}

		public DataRow DealItemGet(string bizDate, string dealId, short utcOffset)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			DataRow dataRow = null;


			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spDealGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				SqlParameter paramDealId = dbCmd.Parameters.Add("@DealId", SqlDbType.VarChar, 16);
				paramDealId.Value = dealId;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "Deal");

				dataSet.RemotingFormat = SerializationFormat.Binary;

				if (dataSet.Tables["Deal"].Rows.Count > 0)
				{
					dataRow = dataSet.Tables["Deal"].NewRow();
					dataRow = dataSet.Tables["Deal"].Rows[0];
				}

			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.DealDataGet]", Log.Error, 1);
				throw;
			}

			return dataRow;
		}

		public void DealSet(
			string dealId,
			string dealStatus,
			string actUserId,
			bool isActive
			)
		{
			Log.Write("User " + actUserId + " is setting deal data for Deal ID " + dealId + " [PositionAgent.DealSet]", 3);

			DealSet(dealId, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", dealStatus, actUserId, isActive, "", "", "");
		}

		public void DealSet(
			string dealId,
			string bookGroup,
			string dealType,
			string book,
			string bookContact,
			string contractId,
			string secId,
			string quantity,
			string amount,
			string collateralCode,
			string valueDate,
			string settleDate,
			string termDate,
			string rate,
			string rateCode,
			string poolCode,
			string divRate,
			string divCallable,
			string incomeTracked,
			string margin,
			string marginCode,
			string currencyIso,
			string securityDepot,
			string cashDepot,
			string comment,
			string fund,
			string dealStatus,
			string actUserId,
			bool isActive,
            string feeAmount,
            string feeCurrencyIso,
            string feeType
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

				if (!contractId.Equals(""))
				{
					SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 25);
					paramContractId.Value = contractId;
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

				if (!fund.Equals(""))
				{
					SqlParameter paramFund = dbCmd.Parameters.Add("@Fund", SqlDbType.VarChar, 6);
					paramFund.Value = fund;
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

                if (!feeAmount.Equals(""))
                {
                    SqlParameter paramFeeAmount = dbCmd.Parameters.Add("@FeeAmount", SqlDbType.Decimal);
                    paramFeeAmount.Value = feeAmount;
                }

                if (!feeCurrencyIso.Equals(""))
                {
                    SqlParameter paramFeeCurrencyIso = dbCmd.Parameters.Add("@FeeCurrencyIso", SqlDbType.Char, 3);
                    paramFeeCurrencyIso.Value = feeCurrencyIso;
                }

                if (!feeType.Equals(""))
                {
                    SqlParameter paramFeeType = dbCmd.Parameters.Add("@FeeType", SqlDbType.Char, 1);
                    paramFeeType.Value = feeType;
                }

				dbCn.Open();

				dbCmd.ExecuteNonQuery();

				if (dealStatus.Equals("S"))
				{

					if (Master.UseSystemSettlementEngine)
					{
						DealToContract(dealId, Master.ContractsBizDate);
					}
					else
					{
						DataRow dataRow = DealItemGet(Master.ContractsBizDate, dealId, 0);

						backOfficeAgent.IntlContractAdd(
							dataRow["DealId"].ToString(),
							dataRow["BookGroup"].ToString(),
							dataRow["DealType"].ToString(),
							dataRow["SecId"].ToString(),
							dataRow["Book"].ToString(),
							long.Parse(dataRow["Quantity"].ToString()),
							decimal.Parse(dataRow["Amount"].ToString()),
							dataRow["CollateralCode"].ToString(),
							"",
							decimal.Parse(dataRow["Rate"].ToString()),
							dataRow["RateCode"].ToString(),
							dataRow["PoolCode"].ToString(),
							dataRow["MarginCode"].ToString(),
							decimal.Parse(dataRow["Margin"].ToString()),
							0,
							dataRow["Comment"].ToString(),
							"",
							0,
							dataRow["SecurityDepot"].ToString(),
							"",
							false,
							decimal.Parse(dataRow["DivRate"].ToString()),
							false,
							dataRow["CurrencyIso"].ToString(),
							dataRow["CashDepot"].ToString(),
							"",
							actUserId);
					}
				}
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

			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spContractGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				dataAdapter = new SqlDataAdapter(dbCmd);

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				dataAdapter.Fill(dataSet, "Contracts");

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

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
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

        public DataSet ContractResearchDataGet(string startDate, string stopDate, string bookGroup, string book, string contractId, string secId)
        {
            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            SqlDataAdapter dataAdapter;
            DataSet dataSet = new DataSet();

            try
            {
                dbCn = new SqlConnection(dbCnStr);
                dbCmd = new SqlCommand("spContractResearchGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                dataAdapter = new SqlDataAdapter(dbCmd);
                
                if (!startDate.Equals(""))
                {
                    SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                    paramStartDate.Value = startDate;
                }

                if (!stopDate.Equals(""))
                {
                    SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
                    paramStopDate.Value = stopDate;
                }
                
                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                if (!book.Equals(""))
                {
                    SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                    paramBook.Value = book;
                }

                if (!contractId.Equals(""))
                {
                    SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
                    paramContractId.Value = contractId;
                }

                if (!secId.Equals(""))
                {
                    SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                    paramSecId.Value = secId;
                }              

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
                Log.Write(e.Message + " [PositionAgent.ContractResearchDataGet]", Log.Error, 1);
                throw;
            }

            return dataSet;
        }
        
        public DataSet ContractResearchDataGet(string bizDate, string bookGroup, string book, string contractId, string secId, string amount, string logicId)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spContractResearchGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				dataAdapter = new SqlDataAdapter(dbCmd);

				if (!bizDate.Equals(""))
				{
					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
					paramBizDate.Value = bizDate;
				}

				if (!bookGroup.Equals(""))
				{
					SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
					paramBookGroup.Value = bookGroup;
				}

				if (!book.Equals(""))
				{
					SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
					paramBook.Value = book;
				}

				if (!contractId.Equals(""))
				{
					SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
					paramContractId.Value = contractId;
				}

				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}

				if (!amount.Equals(""))
				{
					SqlParameter paramAmount = dbCmd.Parameters.Add("@Amount", SqlDbType.Money);
					paramAmount.Value = amount;
				}

				if (!logicId.Equals(""))
				{
					SqlParameter paramLogicId = dbCmd.Parameters.Add("@LogicId", SqlDbType.Char, 1);
					paramLogicId.Value = logicId;
				}

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
				Log.Write(e.Message + " [PositionAgent.ContractResearchDataGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}


		public void ContractSet(
			string bizDate,
			string bookGroup,
			string contractId,
			string contractType,
			string book,
			string secId,
			string quantity,
			string quantitySettled,
			string amount,
			string amountSettled,
			string collateralCode,
			string valueDate,
			string settleDate,
			string termDate,
			string rate,
			string rateCode,
			string statusFlag,
			string poolCode,
			string divRate,
			string divCallable,
			string incomeTracked,
			string marginCode,
			string margin,
			string currencyIso,
			string securityDepot,
			string cashDepot,
			string otherBook,
			string comment,
			bool isActive,
            string feeAmount,
            string feeCurrencyIso,
            string feeType,
            string fund,
            string tradeRefId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			SqlCommand dbCmd = new SqlCommand("spContractSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			try
			{
				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
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
					paramQuantity.Value = long.Parse(quantity);
				}

				if (!quantitySettled.Equals(""))
				{
					SqlParameter paramQuantitySettled = dbCmd.Parameters.Add("@QuantitySettled", SqlDbType.BigInt);
					paramQuantitySettled.Value = long.Parse(quantitySettled);
				}

				if (!amount.Equals(""))
				{
					SqlParameter paramAmount = dbCmd.Parameters.Add("@Amount", SqlDbType.Money);
					paramAmount.Value = decimal.Parse(amount);
				}

				if (!amountSettled.Equals(""))
				{
					SqlParameter paramAmountSettled = dbCmd.Parameters.Add("@AmountSettled", SqlDbType.Money);
					paramAmountSettled.Value = decimal.Parse(amountSettled);
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

				SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
				paramIsActive.Value = isActive;


                if (!feeAmount.Equals(""))
                {
                    SqlParameter paramFeeAmount = dbCmd.Parameters.Add("@FeeAmount", SqlDbType.Decimal);
                    paramFeeAmount.Value = feeAmount;
                }

                if (!feeCurrencyIso.Equals(""))
                {
                    SqlParameter paramFeeCurrencyIso = dbCmd.Parameters.Add("@FeeCurrencyIso", SqlDbType.Char, 3);
                    paramFeeCurrencyIso.Value = feeCurrencyIso;
                }

                if (!feeType.Equals(""))
                {
                    SqlParameter paramFeeType = dbCmd.Parameters.Add("@FeeType", SqlDbType.Char, 1);
                    paramFeeType.Value = feeType;
                }

                if (!fund.Equals(""))
                {
                    SqlParameter paramFund = dbCmd.Parameters.Add("@Fund", SqlDbType.VarChar, 6);
                    paramFund.Value = fund;
                }

                if (!tradeRefId.Equals(""))
                {
                    SqlParameter paramTradeRefId = dbCmd.Parameters.Add("@TradeRefId", SqlDbType.VarChar, 50);
                    paramTradeRefId.Value = tradeRefId;
                }

				dbCn.Open();
				dbCmd.ExecuteNonQuery();

			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ContractSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (!dbCn.State.Equals(ConnectionState.Closed))
				{
					dbCn.Close();
				}
			}
		}

		public DataRow ReturnsItemGet(string returnId, string bizDate, short utcOffset)
		{
			DataRow dr = null;

			try
			{
				dr = ReturnsGet(returnId, bizDate, "", "", utcOffset).Tables["Returns"].Rows[0];
			}
			catch
			{
				dr = null;
			}

			return dr;
		}


		public DataSet ReturnsGet(string returnId, string bizDate, string bookGroup, string contractId, short utcOffset)
		{
			DataSet dsReturns = new DataSet();

			SqlConnection dbCn = null;
			SqlDataAdapter dataAdapter = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);

				SqlCommand dbCmd = new SqlCommand("spReturnGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = bizDate;
                }


				if (!returnId.Equals(""))
				{
					SqlParameter paramReturnId = dbCmd.Parameters.Add("@ReturnId", SqlDbType.VarChar, 16);
					paramReturnId.Value = returnId;
				}

				if (!bookGroup.Equals(""))
				{
					SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
					paramBookGroup.Value = bookGroup;
				}

				if (!contractId.Equals(""))
				{
					SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
					paramContractId.Value = contractId;
				}

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsReturns, "Returns");

				dsReturns.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}

			return dsReturns;
		}


		public DataSet ContractBillingGet(string bizDate)
		{
			DataSet dsBilling = new DataSet();

			SqlConnection dbCn = null;
			SqlDataAdapter dataAdapter = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);

				SqlCommand dbCmd = new SqlCommand("spContractBillingGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsBilling, "Billing");

				dsBilling.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}

			return dsBilling;
		}


		public DataSet ContractSummaryGet(string bizDate)
		{
			DataSet dsContractSummary = new DataSet();

			SqlConnection dbCn = null;
			SqlDataAdapter dataAdapter = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);

				SqlCommand dbCmd = new SqlCommand("spContractSummaryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsContractSummary, "Summary");

				dsContractSummary.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}

			return dsContractSummary;
		}

		public string ReturnSet(string returnId, string bookGroup, string book, string contractId, string contractType, string quantity, string settleDateProjected, string settleDateActual, string actUserId, bool isActive)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			SqlCommand dbCmd = new SqlCommand("spReturnSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			SqlParameter paramReturnId = null;

			try
			{
				paramReturnId = dbCmd.Parameters.Add("@ReturnId", SqlDbType.VarChar, 16);
				paramReturnId.Value = (returnId.Equals("") ? Standard.ProcessId() : returnId);

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDate;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
				paramContractId.Value = contractId;

				SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);
				paramContractType.Value = contractType;

				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
				paramBook.Value = book;

				if (!quantity.Equals(""))
				{
					SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
					paramQuantity.Value = quantity;
				}

				if (!settleDateProjected.Equals(""))
				{
					SqlParameter paramSettleDateProjected = dbCmd.Parameters.Add("@SettleDateProjected", SqlDbType.DateTime);
					paramSettleDateProjected.Value = settleDateProjected;
				}

				if (!settleDateActual.Equals(""))
				{
					SqlParameter paramSettleDateActual = dbCmd.Parameters.Add("@SettleDateActual", SqlDbType.DateTime);
					paramSettleDateActual.Value = settleDateActual;
				}

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
				paramIsActive.Value = isActive;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();

				if (!Master.UseSystemSettlementEngine)
				{
					if (!settleDateActual.Equals(""))
					{
						DataRow dr = null;
						dr = ReturnsItemGet(returnId, Master.BizDate, 0);

						backOfficeAgent.Return(
							dr["ReturnId"].ToString(),
							dr["BookGroup"].ToString(),
							dr["ContractType"].ToString(),
							dr["ContractId"].ToString(),
							long.Parse(dr["Quantity"].ToString()),
							decimal.Parse(dr["CashReturn"].ToString()),
							false.ToString(),
							"",
							dr["CashDepot"].ToString(),
							actUserId);
					}
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ReturnSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (!dbCn.State.Equals(ConnectionState.Closed))
				{
					dbCn.Close();
				}
			}

			return paramReturnId.Value.ToString();
		}


		public DataSet RecallsGet(string recallId, string bizDate, string bookGroup, short utcOffset)
		{
			DataSet dsRecalls = new DataSet();

			SqlConnection dbCn = null;
			SqlDataAdapter dataAdapter = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);

				SqlCommand dbCmd = new SqlCommand("spRecallGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;


				if (!recallId.Equals(""))
				{
					SqlParameter paramRecallId = dbCmd.Parameters.Add("@RecallId", SqlDbType.VarChar, 16);
					paramRecallId.Value = recallId;
				}

				if (!bookGroup.Equals(""))
				{
					SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
					paramBookGroup.Value = bookGroup;
				}

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsRecalls, "Recalls");

				dsRecalls.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}

			return dsRecalls;
		}

		public string RecallSet(string recallId, string bookGroup, string contractId, string contractType, string book, string secId, string quantity, string openDateTime, string moveToDate, string buyInDate, string reasonCode, string sequenceNumber, string comment, string status, string actUserId, bool isActive)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			SqlCommand dbCmd = new SqlCommand("spRecallSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			SqlParameter paramRecallId = null;

			try
			{
				paramRecallId = dbCmd.Parameters.Add("@RecallId", SqlDbType.VarChar, 16);
				paramRecallId.Value = (recallId.Equals("") ? Standard.ProcessId() : recallId);

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDate;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
				paramContractId.Value = contractId;

				SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);
				paramContractType.Value = contractType;

				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
				paramBook.Value = book;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				if (!quantity.Equals(""))
				{
					SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
					paramQuantity.Value = quantity;
				}

				SqlParameter paramOpenDateTime = dbCmd.Parameters.Add("@OpenDateTime", SqlDbType.DateTime);
				paramOpenDateTime.Value = openDateTime;

				if (!moveToDate.Equals(""))
				{
					SqlParameter paramMoveToDate = dbCmd.Parameters.Add("@MoveToDate", SqlDbType.DateTime);
					paramMoveToDate.Value = moveToDate;
				}

				if (!buyInDate.Equals(""))
				{
					SqlParameter paramBuyInDate = dbCmd.Parameters.Add("@BuyInDate", SqlDbType.DateTime);
					paramBuyInDate.Value = buyInDate;
				}

				SqlParameter paramReasonCode = dbCmd.Parameters.Add("@ReasonCode", SqlDbType.VarChar, 2);
				paramReasonCode.Value = reasonCode;

				SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 20);
				paramComment.Value = comment;

				SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.VarChar, 1);
				paramStatus.Value = status;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
				paramIsActive.Value = isActive;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.ReturnSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (!dbCn.State.Equals(ConnectionState.Closed))
				{
					dbCn.Close();
				}
			}

			return paramRecallId.Value.ToString();
		}

		public DataSet MarksGet(string markId, string bizDate, string bookGroup, string contractId, short utcOffSet)
		{
			DataSet dsMarks = new DataSet();

			SqlConnection dbCn = null;
			SqlDataAdapter dataAdapter = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);

				SqlCommand dbCmd = new SqlCommand("spMarkGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = bizDate;
                }


				if (!markId.Equals(""))
				{
					SqlParameter paramMarkId = dbCmd.Parameters.Add("@MarkId", SqlDbType.VarChar, 16);
					paramMarkId.Value = markId;
				}

				if (!bookGroup.Equals(""))
				{
					SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
					paramBookGroup.Value = bookGroup;
				}

				if (!contractId.Equals(""))
				{
					SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
					paramContractId.Value = contractId;
				}

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffSet;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsMarks, "Marks");

				dsMarks.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}

			return dsMarks;
		}

		public string MarkSet(string markId, string bookGroup, string book, string contractId, string contractType, string secId, string amount, string openDate, string settleDate, string deliveryCode, string actUserId, bool isActive)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			SqlCommand dbCmd = new SqlCommand("spMarkSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			SqlParameter paramMarkId = null;

			try
			{
				paramMarkId = dbCmd.Parameters.Add("@MarkId", SqlDbType.VarChar, 16);
				paramMarkId.Value = (markId.Equals("") ? Standard.ProcessId() : markId);

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDate;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				if (!book.Equals(""))
				{
					SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
					paramBook.Value = book;
				}

				if (!contractId.Equals(""))
				{
					SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
					paramContractId.Value = contractId;
				}

				if (!contractType.Equals(""))
				{
					SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);
					paramContractType.Value = contractType;
				}

				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}

				if (!amount.Equals(""))
				{
					SqlParameter paramAmount = dbCmd.Parameters.Add("@Amount", SqlDbType.Money);
					paramAmount.Value = decimal.Parse(amount);
				}

				if (!openDate.Equals(""))
				{
					SqlParameter paramOpenDate = dbCmd.Parameters.Add("@OpenDate", SqlDbType.DateTime);
					paramOpenDate.Value = openDate;
				}

				if (!settleDate.Equals(""))
				{
					SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);
					paramSettleDate.Value = settleDate;
				}

				if (!deliveryCode.Equals(""))
				{
					SqlParameter paramDeliveryCode = dbCmd.Parameters.Add("@DeliveryCode", SqlDbType.VarChar, 1);
					paramDeliveryCode.Value = deliveryCode;
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
				Log.Write(e.Message + " [PositionAgent.MarkSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (!dbCn.State.Equals(ConnectionState.Closed))
				{
					dbCn.Close();
				}
			}

			return paramMarkId.Value.ToString();
		}

		public DataSet MarkContractsGet(string bizDate)
		{
			DataSet dsMarkContracts = new DataSet();

			SqlConnection dbCn = null;
			SqlDataAdapter dataAdapter = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);

				SqlCommand dbCmd = new SqlCommand("spMarkContractsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsMarkContracts, "MarkContracts");

				dsMarkContracts.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}

			return dsMarkContracts;
		}

		public DataSet MarkSummaryGet(string bizDate)
		{
			DataSet dsMarkSummary = new DataSet();

			SqlConnection dbCn = null;
			SqlDataAdapter dataAdapter = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);

				SqlCommand dbCmd = new SqlCommand("spMarkSummaryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsMarkSummary, "MarkSummary");

				dsMarkSummary.RemotingFormat = SerializationFormat.Binary;
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}

			return dsMarkSummary;
		}

		public void SystemNotificationEventInvoke(SystemNotificationEventArgs systemNotificationEventArg)
		{
			SystemNotificationEventHandler systemNotificationEventHandler = null;

			string systemNotificationIdentifier = "[" + systemNotificationEventArg.SystemType.ToString() + "] " + systemNotificationEventArg.ProcessId + "|" + systemNotificationEventArg.BookGroup;

			try
			{
				if (SystemNotificationEvent == null)
				{
					Log.Write("Handling a system notification event for " + systemNotificationIdentifier + " with no delegates. [PositionAgent.SystemNotificationEventInvoke]", 2);
				}
				else
				{
					int n = 0;

					Delegate[] eventDelegates = SystemNotificationEvent.GetInvocationList();
					Log.Write("Handling a system notification event for " + systemNotificationIdentifier + " with " + eventDelegates.Length + " delegates. [PositionAgent.SystemNotificationEventInvoke]", 2);

					foreach (Delegate eventDelegate in eventDelegates)
					{
						Log.Write("Invoking delegate [" + (++n) + "]. [PositionAgent.SystemNotificationEventInvoke]", 3);

						try
						{
							systemNotificationEventHandler = (SystemNotificationEventHandler)eventDelegate;
							systemNotificationEventHandler(systemNotificationEventArg);
						}
						catch (System.Net.Sockets.SocketException)
						{
							SystemNotificationEvent -= systemNotificationEventHandler;
							Log.Write(" System notification  event delegate [" + n + "] has been removed from the invocation list. [PositionAgent.SystemNotificationEventInvoke]", 3);
						}
						catch (Exception e)
						{
							Log.Write(e.Message + " [PositionAgent.SystemNotificationEventInvoke]", Log.Error, 1);
						}
					}

					Log.Write("Done invoking the  system notification event invocation list. [PositionAgent.SystemNotificationEventInvoke]", 3);
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PositionAgent.SystemNotificationEventInvoke]", Log.Error, 1);
			}
		}


		public string RateChangeAsOfSet(string startDate, string endDate, string bookGroup, string book, string contractId, string oldRate, string newRate, string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

            SqlCommand dbCmd = new SqlCommand("spRetroRateChangeSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			SqlParameter paramRecordsUpdated = null;

			try
			{
				SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
				paramStartDate.Value = startDate;

				SqlParameter paramEndDate = dbCmd.Parameters.Add("@EndDate", SqlDbType.DateTime);
				paramEndDate.Value = endDate;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				if (!book.Equals(""))
				{
					SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
					paramBook.Value = book;
				}

				if (!contractId.Equals(""))
				{
					SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
					paramContractId.Value = contractId;
				}

				SqlParameter paramOldRate = dbCmd.Parameters.Add("@OldRate", SqlDbType.Decimal);
				paramOldRate.Value = oldRate;

				SqlParameter paramNewRate = dbCmd.Parameters.Add("@NewRate", SqlDbType.Decimal);
				paramNewRate.Value = newRate;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				paramRecordsUpdated = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);
				paramRecordsUpdated.Direction = ParameterDirection.Output;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
				throw;
			}
			finally
			{
				if (dbCn.State == ConnectionState.Open)
				{
					dbCn.Close();
				}
			}

			return long.Parse(paramRecordsUpdated.Value.ToString()).ToString("#,##0");
		}

      
		public string ContractAsOfSet(
		string bookGroup,
		string contractId,
		string contractType,
		string book,
		string secId,
		string quantity,
		string quantitySettled,
		string amount,
		string amountSettled,
		string collateralCode,
		string valueDate,
		string settleDate,
		string termDate,
		string rate,
		string rateCode,
		string statusFlag,
		string poolCode,
		string divRate,
		string divCallable,
		string incomeTracked,
		string marginCode,
		string margin,
		string currencyIso,
		string securityDepot,
		string cashDepot,
		string otherBook,
		string comment,
		bool isActive,        
        string feeAmount,
        string feeCurrencyIso,
        string feeType,
            string fund,
            string tradeRefId)
		{
			DateTime tradeDateTime = new DateTime();
			DateTime termDateTime = new DateTime();
			
			contractId = Standard.ProcessId();
			
			tradeDateTime = DateTime.Parse(valueDate);

			if (termDate.Equals(""))
			{
				termDateTime = DateTime.Parse(Master.BizDate);
			}
			else
			{
				termDateTime = DateTime.Parse(termDate);
			}

			while (tradeDateTime <= termDateTime)
			{

						ContractSet(
						 tradeDateTime.ToString(Standard.DateFormat),
						 bookGroup,
						 contractId,
						 contractType,
						 book,
						 secId,
						 quantity,
						 quantitySettled,
						 amount,
						 amountSettled,
						 collateralCode,
						 valueDate,
						 settleDate,
						 termDate,
						 rate,
						 rateCode,
						 statusFlag,
						 poolCode,
						 divRate,
						 divCallable,
						 incomeTracked,
						 marginCode,
						 margin,
						 currencyIso,
						 securityDepot,
						 cashDepot,
						 otherBook,
						 comment,
						 true,
                         feeAmount,
                         feeCurrencyIso,
                         feeType,
                         fund,
                         tradeRefId);

				tradeDateTime = tradeDateTime.AddDays(1.0);
			}

			return contractId;
		}

		public string MarkAsOfSet(
			string tradeDate, 
			string settleDate, 
			string bookGroup, 
			string book,
			string contractId, 
			string contractType, 
			string price,
			string markId,
			string deliveryCode, 
			string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			SqlCommand dbCmd = new SqlCommand("spRetroMarkSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			SqlParameter paramRecordsUpdated = null;

			try
			{
				SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
				paramTradeDate.Value = tradeDate;


				if (!settleDate.Equals(""))
				{
					SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);
					paramSettleDate.Value = settleDate;
				}

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
				paramBook.Value = book;
				
				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
				paramContractId.Value = contractId;
			
				SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);
				paramContractType.Value = contractType;

				SqlParameter paramPrice = dbCmd.Parameters.Add("@Price", SqlDbType.Decimal);
				paramPrice.Value = decimal.Parse(price).ToString(Formats.MarkPrice);

				SqlParameter paramMarkId = dbCmd.Parameters.Add("@MarkId", SqlDbType.VarChar, 16);
				paramMarkId.Value = Standard.ProcessId();

				SqlParameter paramDeliveryCode = dbCmd.Parameters.Add("@DeliveryCode", SqlDbType.VarChar, 1);
				paramDeliveryCode.Value = deliveryCode;

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				paramRecordsUpdated = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);
				paramRecordsUpdated.Direction = ParameterDirection.Output;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
				throw;
			}
			finally
			{
				if (dbCn.State == ConnectionState.Open)
				{
					dbCn.Close();
				}
			}

			return long.Parse(paramRecordsUpdated.Value.ToString()).ToString("#,##0");

		}

		public string ReturnAsOfSet(string tradeDate, string settleDate, string bookGroup, string book, string contractId, string contractType, string quantity, string returnId, string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			SqlCommand dbCmd = new SqlCommand("spRetroReturnSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			SqlParameter paramRecordsUpdated = null;

			try
			{
				SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
				paramTradeDate.Value = tradeDate;


				if (!settleDate.Equals(""))
				{
					SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);
					paramSettleDate.Value = settleDate;
				}

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				paramBookGroup.Value = bookGroup;

				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
				paramBook.Value = book;

				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
				paramContractId.Value = contractId;

				SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);
				paramContractType.Value = contractType;

				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
				paramQuantity.Value = long.Parse(quantity);

				SqlParameter paramReturnId = dbCmd.Parameters.Add("@ReturnId", SqlDbType.VarChar, 16);
				paramReturnId.Value = Standard.ProcessId();

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId;

				paramRecordsUpdated = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.BigInt);
				paramRecordsUpdated.Direction = ParameterDirection.Output;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
				throw;
			}
			finally
			{
				if (dbCn.State == ConnectionState.Open)
				{
					dbCn.Close();
				}
			}

			return long.Parse(paramRecordsUpdated.Value.ToString()).ToString("#,##0");
		}

        public string Report_ContractByCounterParty(string bizDate, string bookGroup)
        {
            ContractSummaryDocument cs = new ContractSummaryDocument(dbCnStr, "", bizDate, bookGroup);
            string fileName = "";

            cs.DataGet();
            fileName = cs.ContentGet_ContractByCounterParty();

            return fileName;
        }

		public string Report_ContractBySecurity(string bizDate, string bookGroup)
		{
			ContractSummaryDocument cs = new ContractSummaryDocument(dbCnStr, "", bizDate, bookGroup);
			string fileName = "";


			cs.DataGet();
			fileName = cs.ContentGet_ContractBySecurity();

			return fileName;
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
