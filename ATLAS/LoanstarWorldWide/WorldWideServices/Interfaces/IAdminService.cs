using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using StockLoan.Business;

namespace StockLoan.WebServices.AdminService
//namespace StockLoanAdminService
{
    [ServiceContract]
    public interface IAdminService
    {     
        [OperationContract]
        byte[] CountriesGet(string countryCode, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] CountryCodeIsoConversionsGet(string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool CountrySet(string countryCode, string country, string settleDays, bool isActive,
                        string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] CurrenciesGet(string currencyIso, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] CurrencyConversionsGet(string currencyIsoFrom, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool CurrencyConversionSet(string currencyIsoFrom, string currencyIsoTo, string currencyConvertRate, 
                                    string userId,  string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool CurrencySet(string currencyIso, string currency, bool isActive, string userId,  string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] DeliveryTypesGet(string userId, string userPassword, string bookGroup, string functionPath);

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
