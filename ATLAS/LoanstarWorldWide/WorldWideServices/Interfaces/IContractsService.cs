using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

using StockLoan.Business;
namespace StockLoan.WebServices.ContractsService
{

    [ServiceContract]
    public interface IContractsService
    {      
        [OperationContract]
        byte[] ContractBillingsGet(string bizDate, string bookGroup, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] ContractDetailsGet(string bizDate, string bookGroup, string userId, string userPassword, string functionPath);

        [OperationContract]
        bool ContractRateChangeAsOfSet(string startDate, string endDate, string bookGroup, string book,
                   string contractId, string oldRate, string newRate, string actUserId, string userId, string userPassword, string functionPath);

        [OperationContract]
        bool ContractSet(string bizDate, string bookGroup, string contractId, string contractType, string book, string secId, string quantity,
                string quantitySettled, string amount, string amountSettled, string collateralCode, string valueDate, string settleDate,
                string termDate, string rate, string rateCode, string statusFlag, string poolCode, string divRate, bool divCallable,
                bool incomeTracked, string marginCode, string margin, string currencyIso, string securityDepot, string cashDepot,
                string otherBook, string comment, string fund, string tradeRefId, string feeAmount, string feeCurrencyIso, string feeType,
                bool returnData, bool isIncremental, bool isActive, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] ContractsGet(string bizDate, string bookGroup, string contractId, string contractType, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] ContractsResearchGet(string bizDate, string startDate, string stopDate, string bookGroup, string book,
                string contractId, string secId, string amount, string logicId, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] ContractsSummaryByBillings(string bizDate, string startDate, string stopDate, string bookGroup, string book,
                string contractId, string secId, string amount, string logicId, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] ContractSummaryGet(string bizDate, string bookGroup, string userId, string userPassword, string functionPath, string usePoolCode);

        [OperationContract]
        byte[] ContractSummaryByBookCash(string bizDate, string bookGroup, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] ContractSummaryByBookProfitLossGet(string bizDate, string startDate, string stopDate, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] ContractSummarybyCashGet(string bizDate, string bookGroup, string settlementType, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] ContractSummaryByCreditsDebits(string bizDate, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] ContractSummaryByHypothicationGet(string bizDate, string bookGroup, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] ContractSummaryByMarketValueGet(string bizDate, string bookGroup, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] ContractSummaryBySecurityGet(string bizDate, string bookGroup, bool usePoolCode, string userId, string userPassword, string functionPath);

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
