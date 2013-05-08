using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

using StockLoan.Business;

namespace StockLoan.WebServices.BooksService
{

    [ServiceContract]
    public interface IBooksService
    {
        [OperationContract]
        bool BookClearingInstructionSet(string bookGroup, string book, string countryCode, string currencyIso, string divRate,
              string cashInstructions, string securityInstructions, string ActUserId, bool isActive, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] BookClearingInstructionsGet(string bookGroup, string book, string userId, string userPassword, string functionPath);

        [OperationContract]
        bool BookContactSet(string bookGroup, string book, string firstName, string lastName, string function,
                string phoneNumber, string faxNumber, string comment, string actUserId, bool isActive, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] BookContactsGet(string bookGroup, string book, short utcOffSet, string userId, string userPassword, string functionPath);

        [OperationContract]
        bool BookCreditLimitSet(string bizDate, string bookGroup, string bookParent, string book, string borrowLimitAmount,
                        string loanLimitAmount, string actUserId, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] BookCreditLimitsGet(string bizDate, string bookGroup, string bookParent, string book,
                        short utcOffSet, string userId, string userPassword, string functionPath);

        [OperationContract]
        bool BookFundSet(string bookGroup, string book, string actUserId, string currencyIso, string fund, bool isActive,
                        string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] BookFundsGet(string bookGroup, string book, string currencyIso, string userId, string userPassword, string functionPath);

        [OperationContract]
        bool BookGroupRoll(string bizDate, string bizDatePrior, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool BookGroupSet(string bookGroupSet, string bookName, string timeZoneId, string bizDate, string bizDateContract,
                            string bizDateBank, string bizDateExchange, string bizDatePrior, string bizDatePriorBank,
                            string bizDatePriorExchange, string bizDateNext, string bizDateNextBank, string bizDateNextExchange,
                            bool useWeekends, string settlementType, string bookGroup, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] BookGroupsGet(string bookGroup, string bizDate, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] BookGroupsGetAll(string bookGroup, string bizDate, string userId, string userPassword, string functionPath);

        [OperationContract]
        bool BookSet(string bookGroup, string bookParent, string book, string bookName, string addressLine1,
                string addressLine2, string addressLine3, string phoneNumber, string faxNumber, string marginBorrow, string marginLoan,
                string markRoundHouse, string roundInstitution, string rateStockBorrow, string rateStockLoan,
                string rateBondBorrow, string rateBondLoan, string countryCode, string fundDefault, string priceMin,
                string amountMin, string actUserId, bool isActive, string userId, string userPassword, string functionPath);


        [OperationContract]
        [FaultContract(typeof(Exception))]
        byte[] BooksGet(string bookGroup, string book, string userId, string userPassword, string functionPath);

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
