using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StockLoanWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPosition" in both code and config file together.
    [ServiceContract]
    public interface IPosition
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here

        [OperationContract]
        bool BlockedSecId(string secId);

        [OperationContract]
        byte[] AccountPositionGet(string secId, bool isActive);
        [OperationContract]
        byte[] AccountPositionGet(string firm, string locMemo, string accountType, string accountNumber, string currencyCode, string secId, bool isActive);

        [OperationContract]
        byte[] AccountsGet(string groupCode);
        [OperationContract]
        byte[] AccountsGet(int utcOffset);
        [OperationContract]
        byte[] AccountsGet(string firm, string locMemo, string accountType, string accountNumber, string currencyCode, int utcOffset);

        [OperationContract]
        void AccountSet(string firm, string locMemo, string accountType, string accountNumber, string currencyCode, string actUserId, bool isActive);

        [OperationContract]
        string AutoBorrowListSend(string bizDate, string bookGroup, string listName);
        [OperationContract]
        byte[] AutoBorrowDataGet(string bizDate, short utcOffset, string userId, string functionPath);

        [OperationContract]
        byte[] AutoBorrowItemsGet(int utcOffset, string bizDate);

        [OperationContract]
        void AutoBorrowItemSet(string bookGroup, string listName, string secId, string quantity, string collateralCode,
                               string rateMin, string rateMinCode, string priceMax, bool incomeTracked, string margin,
                               string marginCode, string divfRate, string comment, string actUserId);

        [OperationContract]
        byte[] AutoBorrowListsGet(int utcOffSet, string bizDate);

        [OperationContract]
        void AutoBorrowListSet(string bookGroup, string listName, string books, string waitTime, bool bookContract,
                                      string batchCode, string poolCode, int itemCount, int filled, string listStatus, string actUserId);

        [OperationContract]
        byte[] AutoBorrowResultsGet(int utcOffSet, string bizDate);

        [OperationContract]
        byte[] BankLoanActivityGet(int utcOffset);

        [OperationContract]
        void BankLoanReportSet(string bookGroup, string reportName, string reportType, string reportHost, string reportHostUserId,
                                      string reportHostPassword, string reportPath, string fileLoadDate, string actUserId);

        [OperationContract]
        byte[] BankLoanReportsDataGet();

        [OperationContract]
        byte[] BankLoanReportsDataMaskGet(string bookGroup, string reportName, int utcOffset);

        [OperationContract]
        void BankLoanReportsDataMaskSet(string bookGroup, string reportName, string headerFlag, string dataFlag, string trailerFlag,
                                        int reportNamePosition, int reportNameLength, int reportDatePosition, int reportDateLength,
                                        int secIdPosition, int secIdLength, int quantityPosition, int quantityLength, 
                                        int activityPosition, int activitylength, string ignoreItems, int lineLength, string actUserId);

        [OperationContract]
        byte[] BankLoanPledgeSummaryGet(string bizDate);

        [OperationContract]
        byte[] BankLoanReleaseSummaryGet(string secId); 

        [OperationContract]
        byte[] BankLoanDataGet(short utcOffset, string userId, string functionPath);

        [OperationContract]
        byte[] BankLoansGet(int utcOffset);

        [OperationContract]
        void BankLoanSet(string bookGroup, string book, string loanDate, string loanType, string activityType, string hairCut,
                                string spMin, string moodyMin, string priceMin, string loanAmount, string comment, string actUserId, bool isActive);

        [OperationContract]
        void BankSet(string bookGroup, string book, string name, string contact, string phone, string fax, string actUserId, bool isActive);
        
        [OperationContract]
        byte[] BoxPositionLookup(string secId, string bizDate);

        [OperationContract]
        void BankLoanPledgeSet(string bookGroup, string book, string loanDate, string processId, string loanType, string activityType,
                               string secId, string quantity, string flag, string status, string actUserId);

        [OperationContract]
        void BankLoanReleaseSet(string bookGroup, string book, string processId, string loanDate, string loanType,
                                string activityType, string secId, string quantity, string flag, string status, string actUserId);

        [OperationContract]
        void BankLoanActivityEventInvoke(BankLoanActivityEventArgs bankLoanActivityEventArgs);

        [OperationContract]
        byte[] ContractsModifiedGet(string bookGroup);

        [OperationContract]
        bool FaxEnabled();

        [OperationContract]
        string FutureBizDate(int numberOfDays);

        [OperationContract]
        long CreditGet(string bookGroup, string book, string contractType);

        [OperationContract]
        byte[] ContractRateHistoryGet(string bookGroup, string contractType, string secId);

        [OperationContract]
        bool CreditCheck(string bookGroup, string book, decimal amount, string contractType);

        [OperationContract]
        byte[] DealDataGet(short utcOffset, string userId, string functionPath, string isActive);

        [OperationContract]
        byte[] DealDataGet(short utcOffset, string dealIdPrefix, string userId, string functionPath, string isActive);

        [OperationContract]
        byte[] DealDataGet(string bookGroup, string book, string dealType);

        [OperationContract]
        string DealListSubmit(string bookGroup, string book, string bookContact, string dealType, string dealIdPrefix, string comment, string list, string actUserId);

        [OperationContract]
        void DealSet(string dealId, string dealStatus, string actUserId, bool isActive);

        [OperationContract]
        void DealSet(string dealId,
                    string bookGroup,
                    string dealType,
                    string book,
                    string bookContact,
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
                    string dealStatus,
                    string actUserId,
                    bool isActive
                    );

        [OperationContract]
        void DealEventInvoke(DealEventArgs dealEventArgs);

        [OperationContract]
        void BoxSummarySet(string bookGroup, string secId, bool doNotRecall, string comment, string actUserId);

        [OperationContract]
        byte[] BoxSummaryDataGet(short utcOffset, string userId, string functionPath, string bizDate);

        [OperationContract]
        byte[] BoxFailHistoryGet(string secId);

        [OperationContract]
        byte[] ContractRateComparisonDataGet(string userId, string functionPath);

        [OperationContract]
        byte[] ContractRateComparisonGet(string bizDate, string bookGroup, string book, string contractType);

        [OperationContract]
        byte[] ContractDataGet(short utcOffset, string bizDate, string userId, string functionPath);

        [OperationContract]
        byte[] ContractDataGet(short utcOffset);

        [OperationContract]
        byte[] ContractDataGet(short utcOffset, string bookGroup, string contractId);

        [OperationContract]
        byte[] ContractDataGet(string bookGroup, string book, string contractType);

        
        
        //--------------------------------------------------

        [OperationContract]
        void ContractSet(string bizDate, string bookGroup, string contractId, string contractType);

        [OperationContract]
        void ContractSet(string bizDate,
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
                        string comment);

        [OperationContract]
        void ContractEventInvoke(ContractEventArgs contractEventArgs);

        [OperationContract]
        byte[] DealsDetailDataGet(string BookGroup, string SecId, short UtcOffset);

        
        
        //======================================== 

        [OperationContract]
        string RecallTermNoticeDocument(string recallId);

        [OperationContract]
        byte[] RecallDataGet(short utcOffset);

        [OperationContract]
        byte[] RecallDataGet(short utcOffset, string bizDate, string userId, string functionPath);

        [OperationContract]
        byte[] RecallActivityGet(short utcOffset, string reacallId);

        [OperationContract]
        byte[] RecallIndicatorsGet();

        [OperationContract]
        byte[] RecallReasonsGet();

        [OperationContract]
        byte[] RecallReasonsGet(string reasonId, string reasonCode);

        [OperationContract]
        void RecallNew(string bookGroup,
                        string contractId,
                        string contractType,
                        string book,
                        string bookContact,
                        string secId,
                        string quantity,
                        string indicator,
                        string baseDueDate,
                        string moveToDate,
                        string openDateTime,
                        string reasonCode,
                        string sequenceNumber,
                        string faxStatus,
                        string comment,
                        string actUserId);

        [OperationContract]
        void RecallDelete(string bookGroup,
                        string contractType,
                        string book,
                        string contractId,
                        string recallDate,
                        int recallSequence,
                        string lenderReference,
                        string comment,
                        string actUserId);

        [OperationContract]
        void RecallBookContactSet(string bookGroup, string book, string bookContact, string actUserId);

        [OperationContract]
        void RecallSet(string recallId,
                            string bookContact,
                            string moveToDate,
                            string action,
                            string faxId,
                            string faxStatus,
                            string deliveredToday,
                            string willNeed,
                            string actUserId,
                            string status);

        [OperationContract]
        void RecallEventInvoke(RecallEventArgs recallEventArgsItem);

        [OperationContract]
        string ContractAdd(string bookGroup,
                            string contractType,
                            string secId,
                            string book,
                            string quantity,
                            string amount,
                            string collateralCode,
                            string expiryDate,
                            string rate,
                            string rateCode,
                            string poolCode,
                            string marginCode,
                            string margin,
                            string negotiatedNewRate,
                            string comment,
                            string otherBook,
                            string fixedInvesmtmentRate,
                            string incomeTracked,
                            string divRate,
                            string actUserId);

        [OperationContract]
        void RateChange(string bookGroup,
                        string contractType,
                        string book,
                        string securityType,
                        string contractId,
                        decimal rateOld,
                        string rateCodeOld,
                        decimal rateNew,
                        string rateCodeNew,
                        string profitCenter,
                        string effectiveDate,
                        string actUserId);

        [OperationContract]
        void Return(string bookGroup,
                    string contractType,
                    string contractId,
                    string secId,
                    long returnQuantity,
                    decimal returnAmount,
                    string callbackRequired,
                    string recDelLocation,
                    string cashDepot,
                    string actUserId);

        [OperationContract]
        string UserEmailGet(string userId);

        [OperationContract]
        void ContractMaintenance(
                    string bookGroup,
                    string contractId,
                    string contractType,
                    string book,
                    string poolCode,
                    string effectiveDate,
                    string deliveryDate,
                    string marginCode,
                    string margin,
                    string divRate,
                    string incomeTracked,
                    string expiryDate,
                    string comment,
                    string actUserId);

        // [OperationContract]
        // void HeartbeatOnEvent(Anetics.Process.HeartbeatEventArgs e);


    }   //interface IPosition



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
