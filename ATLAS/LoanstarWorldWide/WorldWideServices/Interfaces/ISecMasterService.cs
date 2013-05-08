using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

using StockLoan.Business;
namespace StockLoan.WebServices.SecMasterService
{
    [ServiceContract]
    public interface ISecMasterService
    {
        [OperationContract]
        bool PriceSet(string bizDate, string secId, string countryCode, string currencyIso, string price,
                        string priceDate, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] PricesGet(string bizDate, string secId, string currencyIso, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool SecIdAliasSet(string secId, string secIdTypeIndex, string secIdAlias, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] SecMasterGet(string secId, string countryCode, string currencyIso, string bookGroup, string lookUpCriteria, 
                            string userId, string userPassword, string functionPath);

        [OperationContract]
        bool SecMasterSet(string secId, string description, string baseType, string classGroup, string countryCode,
            string currencyIso, string accruedInterest, string recordDateCash, string dividendRate, string secIdGroup, string symbol,
            string Isin, string cusip, string price, string priceDate, bool isActive, string userId, string userPassword, string bookGroup, string functionPath);

    }
}
