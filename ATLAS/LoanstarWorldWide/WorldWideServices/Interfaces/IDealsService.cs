using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

using StockLoan.Business;
namespace StockLoan.WebServices.DealsService
{
        
    [ServiceContract]
    public interface IDealsService
    {
     [OperationContract]
        bool DealSet(string dealId, string bookGroup, string dealType, string book, string bookContact, string contractId,
                string secId, string quantity, string amount, string collateralCode, string valueDate, string settleDate, string termDate,
                string rate, string rateCode, string poolCode, string divRate, bool divCallable, bool incomeTracked, string marginCode,
                string margin, string currencyIso, string securityDepot, string cashDepot, string comment, string fund, string dealStatus,
                bool isActive, string actUserId, bool returnData, string feeAmount, string feeCurrencyIso, string feeType, 
                string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] DealsGet(string bizDate, string dealId, string dealIdPrefix, bool isActive, short utcOffSet, 
                            string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool DealToContract(string dealId, string bizDate, string userId, string userPassword, string bookGroup, string functionPath);
        

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
