using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting;
using System.Threading;
using StockLoan.Common;

namespace StockLoan.Main
{
	public class TradingAgent : MarshalByRefObject, ITrading
	{
		private string dbCnStr = "";

		public TradingAgent(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;
		}

        public DataSet TradingSystemsGet()
        {
            DataSet dsTradingSystems = new DataSet();

            try
            {
                serviceGenericTrade.TradeGeneric tradeWS = new serviceGenericTrade.TradeGeneric();

                dsTradingSystems = tradeWS.TradeSystemsGet();
            }
            catch { }

            return dsTradingSystems;
        }

        public DataSet TradingSystemsCounterPartiesGet(string system)
        {
            DataSet dsTradingSystemsCounterParties = new DataSet();

            try
            {
                serviceGenericTrade.TradeGeneric tradeWS = new serviceGenericTrade.TradeGeneric();

                dsTradingSystemsCounterParties = tradeWS.TradeCounterPartiesGet(system);
            }
            catch { }

            return dsTradingSystemsCounterParties;
        }


        public void TradeRequest(
            string bizDate,
            string bookGroup,
            string book,
            string contractId)
        {
            DataSet dsTradeInstructions = new DataSet();

            dsTradeInstructions = TradeInstructionsGet(bizDate, bookGroup, book, contractId);
            string refTrade = "";

            try
            {
                if (dsTradeInstructions.Tables["TradeInstructions"].Rows.Count > 0)
                {
                    serviceGenericTrade.TradeGeneric tradeWS = new serviceGenericTrade.TradeGeneric();

                    Log.Write(DateTime.Parse(dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["ValueDate"].ToString()).ToString() + " " +
                        DateTime.Parse(dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["SettleDate"].ToString()).ToString(), 1);
                    
                    refTrade =   tradeWS.TradeNewRequest(
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["BookGroupAlias"].ToString(),
                        contractId,
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["System"].ToString(),
                        "",
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Sedol"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Exchange"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Book"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["CounterParty"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["CounterPartyTypeCode"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["BuySellCode"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Quantity"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Price"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["CanceledFlag"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Capacity"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["DeliveryType"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["ClientMarketIndicator"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["CurrencyIso"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["SecurityDepot"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Settlement"].ToString(),
                        "Stock Loan",
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Entity"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["MarginRate"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["ValueDate"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["SettleDate"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["TradeDate"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["FixedInterestRate"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Collateralised"].ToString(),
                         dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["System"].ToString());
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                Log.Write(error.InnerException.Message, 1);
                throw new Exception("Error with trade instructions contact trade support");
            }
        }

        public void ReturnRequest(
            string bizDate,
            string bookGroup,
            string book,
            string contractId)
        {
            DataSet dsTradeInstructions = new DataSet();

            dsTradeInstructions = ReturnInstructionsGet(bizDate, bookGroup, book, contractId);
            string refTrade = "";

            try
            {
                if (dsTradeInstructions.Tables["TradeInstructions"].Rows.Count > 0)
                {
                    serviceGenericTrade.TradeGeneric tradeWS = new serviceGenericTrade.TradeGeneric();

                    refTrade = tradeWS.TradeReturnRequest(
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["BookGroupAlias"].ToString(),
                        contractId,
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["System"].ToString(),
                        "",
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Sedol"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Exchange"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Book"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["CounterParty"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["CounterPartyTypeCode"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["BuySellCode"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Quantity"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Price"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["CanceledFlag"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Capacity"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["DeliveryType"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["ClientMarketIndicator"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["CurrencyIso"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["SecurityDepot"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Settlement"].ToString(),
                        "Stock Loan",
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Entity"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["MarginRate"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["ValueDate"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["SettleDate"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["TradeDate"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["FixedInterestRate"].ToString(),
                        dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["Collateralised"].ToString(),
                         dsTradeInstructions.Tables["TradeInstructions"].Rows[0]["System"].ToString());
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                Log.Write(error.InnerException.Message, 1);
                throw new Exception("Error with trade instructions contact trade support");
            }
        }



        private DataSet TradeInstructionsGet(string bizDate,
            string bookGroup,
            string book,
            string contractId)
        {
            DataSet dsTradeInstructions = new DataSet();

            try
            {
                SqlConnection dbCn = new SqlConnection(dbCnStr);

                SqlCommand dbCmd = new SqlCommand("spBookClearingInstructionTradeGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
                paramContractId.Value = contractId;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                paramBook.Value = book;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTradeInstructions, "TradeInstructions");
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
            }

            return dsTradeInstructions;
        }


        private DataSet ReturnInstructionsGet(string bizDate,
            string bookGroup,
            string book,
            string contractId)
        {
            DataSet dsTradeInstructions = new DataSet();

            try
            {
                SqlConnection dbCn = new SqlConnection(dbCnStr);

                SqlCommand dbCmd = new SqlCommand("spBookClearingInstructionReturnGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
                paramContractId.Value = contractId;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                paramBook.Value = book;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTradeInstructions, "TradeInstructions");
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
            }

            return dsTradeInstructions;
        }


		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
