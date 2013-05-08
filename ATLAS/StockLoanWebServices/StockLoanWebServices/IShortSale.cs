using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
//--------------------------
using System.Data;


namespace StockLoanWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService2" in both code and config file together.
    [ServiceContract]
    public interface IShortSale
    {
        [OperationContract]
        string BizDatePrior(string bizDate);

        [OperationContract]
        string TradeDate();

        [OperationContract]
        byte[] LocatesPreBorrowGet(string bizDate, string groupCode, short utcOffset);

        [OperationContract]
        void LocatesPreBorrowSet(string bizDate, string groupCode, string secId, string quantity, string rebateRate, string actUserId);

        [OperationContract]
        void LocatePreBorrowSubmit(long locateId, string groupCode, string secId, string quantity, string rate, string actUserId);

        [OperationContract]
        byte[] TradingGroupsAccountMarkGet(string tradingGroup, short utcOffset);

        [OperationContract]
        void TradingGroupsAccountMarkSet(
            string groupCode,
            string accountNumber,
            string negativeRebateMarkUp,
            string positiveRebateMarkUp,
            string fedFundsMarkUp,
            string liborFundsMarkUp,
            bool delete,
            string actUserId);

        [OperationContract]
        byte[] TradingGroupsOfficeCodeMarkGet(string tradingGroup, short utcOffset);

        [OperationContract]
        void TradingGroupsOfficeCodeMarkSet(
            string groupCode,
            string officeCode,
            string negativeRebateMarkUp,
            string positiveRebateMarkUp,
            string fedFundsMarkUp,
            string liborFundsMarkUp,
            string actUserId);

        [OperationContract]
        byte[] TradingGroupsGet(string tradeDate, short utcOffset);

        [OperationContract]
        void TradingGroupSet(
            string groupCode,
            string groupName,
            string minPrice,
            string autoApprovalMax,
            string premiumMin,
            string premiumMax,
            bool autoEmail,
            string emailAddress,
            string lastEmailDate,
            string actUserId);

        [OperationContract]
        byte[] LocateDataGet(string status, short utcOffset);

        [OperationContract]
        string LocateListSubmit(string clientId, string groupCode, string clientComment, string locateList);

        [OperationContract]
        string LocateSet(string clientId, string groupCode, string clientComment, string secId, string quantity);

        [OperationContract]
        void LocateItemSet(
            long locateId,
            string quantity,
            string source,
            string feeRate,
            string preBorrow,
            string comment,
            string actUserId);

        [OperationContract]
        void LocateEventInvoke(LocateEventArgs locateEventArgs);

        [OperationContract]
        byte[] LocateDatesGet();

        [OperationContract]
        byte[] LocateDatesGet(string clientId);

        // For Penson.
        [OperationContract]
        byte[] LocateListGet(short utcOffset, string tradeDate);

        [OperationContract]
        byte[] LocateListGet(short utcOffset, string tradeDateMin, string tradeDateMax, string groupCode, string secId);
        // For Penson end.

        [OperationContract]
        byte[] LocatesGet(string tradeDate, short utcOffset);

        [OperationContract]
        byte[] LocateItemGet(string locateId, short utcOffset);

        [OperationContract]
        byte[] LocateItemGet(string groupCode, string locateId, [OperationContract] short utcOffset);

        [OperationContract]
        byte[] LocatesGet(string tradeDate, string clientId, short utcOffset);

        [OperationContract]
        byte[] LocatesGet(string tradeDate, string clientId, string status, short utcOffset);

        [OperationContract]
        byte[] LocatesGet(string tradeDateMin, string tradeDateMax, string groupCode, string secId, short utcOffset);

        [OperationContract]
        byte[] BorrowHardGet(bool showHistory, short utcOffset);

        [OperationContract]
        void BorrowHardSet(string secId, bool delete, string actUserId);

        [OperationContract]
        byte[] BorrowNoGet(bool showHistory, short utcOffset);

        [OperationContract]
        void BorrowNoSet(string secId, bool delete, string actUserId);

        [OperationContract]
        byte[] ThresholdList(string effectDate);

        [OperationContract]
        byte[] BorrowEasyList(string effectDate, short utcOffset);

        [OperationContract]
        void BorrowEasyListSet(string secId, string actUserId, bool isShortSaleEasy);

        [OperationContract]
        string InventoryListSubmit(string desk, string account, string list, string deskQuip, string actUserId);

        [OperationContract]
        byte[] InventoryHistoryLookupGet(string bizDate, string secId);

        [OperationContract]
        byte[] InventoryGet(string secId, short utcOffset);

        [OperationContract]
        byte[] InventoryGet(string secId, short utcOffset, bool withHistory);

        [OperationContract]
        byte[] InventoryFundingRatesHistoryGet(int listCount, short utcOffset);

        [OperationContract]
        byte[] InventoryFundingRatesGet(string bizDate, short utcOffset);

        [OperationContract]
        void InventoryFundingRateSet(string fedFundingRate, string liborFundingRate, string actUserId);

        [OperationContract]
        byte[] InventoryGet(string groupCode, string secId, short utcOffset);

        [OperationContract]
        DataTable InventoryDeskListGet(string bizDate, string desk);

        [OperationContract]
        byte[] InventoryRatesGet(string bizDate);

        [OperationContract]
        void InventoryRateSet(string secId, string rate, string actUserId);

        // [OperationContract]
        // override object InitializeLifetimeService();

    }


}
