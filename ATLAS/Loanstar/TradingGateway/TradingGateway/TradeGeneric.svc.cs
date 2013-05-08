using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using StockLoan.Common;
using StockLoan.MqSeries;
using StockLoan.TGTradeFormats;
using StockLoan.TgDatabaseFunctions;

namespace TradingGateway
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TradeGeneric" in code, svc and config file together.
    public class TradeGeneric : ITradeGeneric
    {
        public string TradeNewRequest(
       string correspondentCode,
       string contractId,
       string system,
       string isin,
       string sedol,
       string exchangeCode,
       string book,
       string counterParty,
       string counterPartyCode,
       string buySellCode,
       string quantity,
       string price,
       string cancelledFlag,
       string capacity,
       string deliveryRisk,
       string clientMarketIndicator,
       string dealingCurrency,
       string countryOfSettlement,
       string placeOfSettlement,
       string businessReason,
       string sourceEntity,
       string marginRate,
       string startDate,
       string endDate,
       string tradeDate,
       string fixedInterestRate,
       string collateralised,
       string clearingSystem)
        {
            string tradeNumber = "";
            string dbCnStr = Standard.ConfigValue("Database");

            string message = "";


            switch (clearingSystem.ToUpper())
            {
                case "STARS":                   
                    tradeNumber = contractId + "O";

                    message = StarsStockLoan.StockLoanTrade(
                            correspondentCode,
                            tradeNumber,
                            isin,
                            sedol,
                            exchangeCode,
                            book,
                            counterParty,
                            counterPartyCode,
                            buySellCode,
                            quantity,
                            price,
                            "",
                            cancelledFlag,
                            capacity,
                            deliveryRisk,
                            clientMarketIndicator,
                            dealingCurrency,
                            countryOfSettlement,
                            placeOfSettlement,
                            businessReason,
                            sourceEntity,
                            marginRate,
                            startDate,
                            endDate,
                            tradeDate,
                            fixedInterestRate,
                            collateralised);

                    MqActivity mqActivity = new MqActivity("");

                    mqActivity.PutMessage(message);
                    mqActivity.MqActivityClose();
                    break;
            }

            FunctionsSet.TradeReferenceSet(DateTime.Now.ToString("yyy-MM-dd"), contractId, tradeNumber, dbCnStr);
            FunctionsSet.TradeMessageSet(DateTime.Now.ToString("yyy-MM-dd"), tradeNumber, message, "", dbCnStr);

            return message;
        }

        public void TradeNewResponse()
        {
        }

        public void TradeRevaluationRequest()
        {
        }

        public void TradeRevaluationResponse()
        {
        }

        public string TradeReturnRequest(
            string correspondentCode,
            string contractId,
            string system,
            string isin,
            string sedol,
            string exchangeCode,
            string book,
            string counterParty,
            string counterPartyCode,
            string buySellCode,
            string quantity,
            string price,
            string cancelledFlag,
            string capacity,
            string deliveryRisk,
            string clientMarketIndicator,
            string dealingCurrency,
            string countryOfSettlement,
            string placeOfSettlement,
            string businessReason,
            string sourceEntity,
            string marginRate,
            string startDate,
            string endDate,
            string tradeDate,
            string fixedInterestRate,
            string collateralised,
            string clearingSystem)
        {
            string tradeNumber = "";
            string dbCnStr = Standard.ConfigValue("Database");

            string message = "";
        
            switch (clearingSystem.ToUpper())
            {
                case "STARS":
                    tradeNumber = contractId + "R";

                    message = StarsStockLoan.StockLoanTrade(
                            correspondentCode,
                            tradeNumber,
                            isin,
                            sedol,
                            exchangeCode,
                            book,
                            counterParty,
                            counterPartyCode,
                            buySellCode,
                            quantity,
                            price,
                            "",
                            cancelledFlag,
                            capacity,
                            deliveryRisk,
                            clientMarketIndicator,
                            dealingCurrency,
                            countryOfSettlement,
                            placeOfSettlement,
                            businessReason,
                            sourceEntity,
                            marginRate,
                            startDate,
                            endDate,
                            tradeDate,
                            fixedInterestRate,
                            collateralised);

                    MqActivity mqActivity = new MqActivity("");

                    mqActivity.PutMessage(message);
                    mqActivity.MqActivityClose();
                    break;
            }

            FunctionsSet.TradeReferenceSet(DateTime.Now.ToString("yyy-MM-dd"), contractId, tradeNumber, dbCnStr);
            FunctionsSet.TradeMessageSet(DateTime.Now.ToString("yyy-MM-dd"), tradeNumber, message, "", dbCnStr);

            return message;
        }


        public DataSet TradeSystemsGet()
        {
            string dbCnStr = Standard.ConfigValue("Database");

            return FunctionsGet.TradeSystemsGet(dbCnStr);
        }


        public DataSet TradeCounterPartiesGet(string system)
        {
            string dbCnStr = Standard.ConfigValue("Database");

            return FunctionsGet.TradeCounterPartiesGet(system, dbCnStr);
        }
    }
}

