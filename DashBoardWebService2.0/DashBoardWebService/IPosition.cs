using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using DashBusiness;
namespace DashBoardWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPosition
    {     
        [OperationContract]
        bool UserValidationGet(string userName, string password);

        [OperationContract]
        byte[] ContractDataGet(string bizDate, string bookGroup, string secId, string userId, string functionPath);
        
        [OperationContract]
        byte[] ContractSummaryByCashLoad(string bizDate, string bookGroup, Locale localeType, string userId, string functionPath);

        [OperationContract]
        byte[] ContractSummaryBySecurity(string bizDate, string bookGroup, Locale localeType, string userId, string functionPath);

        [OperationContract]
        byte[] ContractsExcessCollateralSummaryLoad(string bizDate, string bookGroup, Locale localeType, string userId, string functionPath);

        [OperationContract]
        byte[] ContractSummaryByBooKGroupCashLoad(string bizDate, Locale localeType, string userId, string functionPath);

        [OperationContract]
        byte[] ContractSummaryByBooKGroupSharesCashLoad(string bizDate, Locale localeType, string userId, string functionPath);

        [OperationContract]
        byte[] ContractsDetailByBookSummaryLoad(string bizDate, string bookGroup, string book, string currencyIso, Locale localeType, string userId, string functionPath);

        [OperationContract]
        byte[] MarksGet(string bizDate, string bookGroup, string book, string currencyIso, Locale localeType);

        [OperationContract]
        byte[] InventoryHistoryGet(string secId);

        [OperationContract]
        byte[] InventoryHistorySummaryGet(string secId);

        [OperationContract]
        byte[] InventoryDeskGet(string desk, string bizDate);

        [OperationContract]
        byte[] DeskListGet(string bizDate);

        [OperationContract]
        byte[] BookGroupGet(string userId, string functionPath);

        //[OperationContract]
        //string LcorListUpload(string bizDate, string bookGroup, string book, string maxRate, string list);

        [OperationContract]
        byte[] HtbProfitabilityGet(string bizDate, HtbLocale htbLocate, int numOfRecords);

        [OperationContract]
        byte[] HtbProfitabilitySpreadsGet(string bizDate, HtbLocale htbLocate);

        [OperationContract]
        byte[] HolidaysGet();

        [OperationContract]
        bool UserCanView(string userId, string functionPath);

        [OperationContract]
        byte[] WebSecurityProfileDataSetGet(string userId);

        [OperationContract]
        string InventoryRateGet(string secId);

        [OperationContract]
        byte[] InventoryRateHistoryGet(string secId);

        [OperationContract]
        string FileReader(string path);

        [OperationContract]
        byte[] WebReportsGet();

        [OperationContract]
        bool ReportValueSet(string reportName, string reportRecipient, string format, string justify);

        [OperationContract]
        byte[] StraddlesGet(string accountList, string locMemo, string accountType);

        [OperationContract]
        byte[] ShortSaleLocatesSourceGet(string tradeDate, string source);

        [OperationContract]
        byte[] BankLoanReleaseReportGet(string bizDate, string book);

        [OperationContract]
        byte[] MemoSegLockupGet(string system);

        [OperationContract]
        void MemoSegStartOfDaySet(string bizDate, string system, string secId, string quantity);

        [OperationContract]
        byte[] PenaltyBoxGet(bool showHistory);

        [OperationContract]
        void PenaltyBoxItemSet(string secId, string actUserId, string comment);

        [OperationContract]
        void PenaltyBoxCommentSet(string processId, string comment);

        [OperationContract]
        byte[] RecallTradingGet(string bizDate, string bookGroup);

        [OperationContract]
        void RecallTradingSet(string bizDate, string bookGroup, string secId, string comment);

        [OperationContract]
        byte[] ShortSaleBillingBPSItemSet(string bizDate, string accountNumber, string secId, string quantityShorted, string quantityCovered, string settlementDate, string price, string rate, string actUserId);

        [OperationContract]
        byte[] CollateralizationUtilDetailGet(string bizDate, string classGroup);

        [OperationContract]
        byte[] CollateralizationUtilStartOfDayGet(string bizDate);

        [OperationContract]
        byte[] CollateralizationUtilIntraDayGet(string bizDate, string priceThreshold);

        [OperationContract]
        byte[] BorrowsMorningGet(string bizDate);

        [OperationContract]
        void BorrowsMorningSet(string bizDate, string bookGroup, string secId, string quantity);

        [OperationContract]
        void BorrowsMorningPurge(string bizDate);

        [OperationContract]
        byte[] CheckListGet(string bizDate);

        [OperationContract]
        void CheckListItemSet(string bizDate, string processId, string itemComment, string itemUserId);

        [OperationContract]
        string KeyValueGet(string keyId);

        [OperationContract]
        void KeyValueSet(string keyId, string keyValue);

        [OperationContract]
        byte[] BookBalancesGet(string bizDate, string bookGroup);

        [OperationContract]
        byte[] BookCreditLBookBalancesGet(string bizDate, string bookGroup);

        [OperationContract]
        byte[] RecallTradingSupplementGet(string bizDatePrior, string bizDate, string bookGroup);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
