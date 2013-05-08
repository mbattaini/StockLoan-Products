using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;



namespace StockLoan.WebServices.LocatesService
{
    [ServiceContract]
    public interface ILocatesService
    {
        [OperationContract]
        bool WebUserAuthorize(string userId, string password);

        [OperationContract]
        byte[] TradingGroupsGet(string tradeDate, short utcOffset);

        [OperationContract]
        string LocateListSubmit(string clientId, string groupCode, string clientComment, string list);

        [OperationContract]
        byte[] InventoryGet(string groupCode, string secId, short utcOffset);

        [OperationContract]
        byte[] LocateItemGet(string groupCode, string locateId, short utcOffset);

        [OperationContract]
        byte[] LocatesGet(string tradeDate, string groupCode, string clientId, short utcOffset, string status);

        [OperationContract]
        byte[] LocateSummaryGet(string tradeDate, string secId);

        [OperationContract]
        byte[] LocateGroupCodeSummaryGet(string tradeDate);

        [OperationContract]
        void LocateItemSet(long locateId, string quantity, string source, string feeRate, string preBorrow, string comment, string actUserId);

        [OperationContract]
        byte[] SecMasterItemGet(string secId);

        [OperationContract]
        byte[] BoxPositionItemGet(string bookGroup, string secId);

        [OperationContract]
        byte[] BookGroupsGet();

        [OperationContract]
        byte[] LocatesMessageGet();

        [OperationContract]
        void LocatesMessageSet(string userId, string message);
    } 
}
