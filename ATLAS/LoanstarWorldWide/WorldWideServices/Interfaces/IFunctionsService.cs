using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

using StockLoan.Business;
namespace StockLoan.WebServices.FunctionsService
{
    [ServiceContract]
    public interface IFunctionsService
    {
        [OperationContract]
        bool HolidaySet(string bookGroupSet, string holidayDate, string countryCode, string description, bool isBankHoliday,
                bool isExchangeHoliday, string actUserId, bool isActive, string bookGroup, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] TimeZonesGet(string timeZoneId, string timeZoneName, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] HolidaysGet(string bookGroupToCheck, string countryCode, string description, string utcOffSet, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] HolidaysGetList(string bookGroupToCheck, string compareDate, string description, string utcOffSet, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool IsBankHoliday(string bookGroupToCheck, string countryCode, string holidayDate, string utcOffSet, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool IsExchangeHoliday(string bookGroupToCheck, string countryCode, string holidayDate, string utcOffSet, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool KeyValueSet(string keyId, string keyValue, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        string KeyValuesGet(string keyId, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] KeyValuesListGet(string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] LogicOperatorsGet(string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool SettlementSystsemProcessSet(string bizDate, string userId, string userPassword, string bookGroup, string functionPath);

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
