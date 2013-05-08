using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

using StockLoan.Business;

namespace StockLoan.WebServices.FundsService
{
        
    [ServiceContract]
    public interface IFundsService
    {
        [OperationContract]
        byte[] FundingRateResearchGet(string startDate, string stopDate, string fund, short utcOffset, 
                                    string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool FundingRateSet(string bizDate, string fund, string fundingRate, string actUserId, 
                                    string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] FundingRatesGet(string bizDate, short utcOffset, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool FundingRatesRoll(string bizDate, string bizDatePrior, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] FundsGet(string userId, string userPassword, string bookGroup, string functionPath);

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
