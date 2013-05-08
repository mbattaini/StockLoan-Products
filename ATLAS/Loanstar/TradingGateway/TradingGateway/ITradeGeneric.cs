using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TradingGateway
{
    [ServiceContract]
    public interface ITradeGeneric
    {
        [OperationContract]
        string TradeNewRequest(
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
            string clearingSystem);

        [OperationContract]
        string TradeReturnRequest(
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
            string clearingSystem);

        [OperationContract]
        void TradeNewResponse();

        [OperationContract]
        void TradeRevaluationRequest();

        [OperationContract]
        void TradeRevaluationResponse();

        [OperationContract]
        DataSet TradeSystemsGet();

        [OperationContract]
        DataSet TradeCounterPartiesGet(string system);
    }
}
