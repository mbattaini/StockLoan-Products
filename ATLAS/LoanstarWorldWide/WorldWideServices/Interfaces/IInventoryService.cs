using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using StockLoan.Business;

namespace StockLoan.WebServices.InventoryService
{
    [ServiceContract]
    public interface IInventoryService
    {
        [OperationContract]
        byte[] DesksGet(string desk, string bookGroup, string sIsNotSubscriber,
                                    string userId, string userPassword, string functionPath);

        [OperationContract]
        bool DeskSet(string desk, string firmCode, string deskTypeCode, string countryCode,
                            string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] DeskTypesGet(string deskTypeCode, string deskType, string sIsActive,
                                    string bookGroup, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] FirmsGet(string firmCode, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        bool FirmSet(string firmCode, string firm, string sIsActive, 
                            string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] InventoryGet(string bizDate, string bookGroup, string desk, string secId, string version, string source, string sourceActor, 
                                string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] InventoryHistoryGet(string bookGroup, string secId, string userId, string userPassword, string functionPath);
        
        [OperationContract]
        bool InventoryItemSet(string bizDate, string desk, string secId, string rate, string quantity, string source, 
                        string sourceActor, string userId, string userPassword, string bookGroup, string functionPath);

        [OperationContract]
        byte[] InventoryFileLayoutGet(string bookGroupGet, string desk, string inventoryType,
                                string bookGroup, string userId, string userPassword, string functionPath);

        [OperationContract]
        bool InventoryFileLayoutSet(string bookGroupSet, string desk, string inventoryType, string recordLength, string headerFlag,
                string dataFlag, string trailerFlag, string delimiter, string accountLocale, string accountOrdinal,
                string accountPosition, string accountLength, string secIdOrdinal, string secIdPosition, string secIdLength,
                string quantityOrdinal, string quantityPosition, string quantityLength, string rateOrdinal,
                string ratePosition, string rateLength, string recordCountOrdinal, string recordCountPosition,
                string recordCountLength, string bizDateDD, string bizDateMM, string bizDateYY, string actor, string bookGroup,
                string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] InventoryRatesGet(string bizDate, string bookGroup, string desk, string secId, string version, string source, 
                string sourceActor, string userId, string userPassword, string functionPath);

        [OperationContract]
        byte[] InventorySubscriptionsGet(string bookGroupGet, string desk, string inventoryType, short utcOffset,
                                                string bookGroup, string userId, string userPassword, string functionPath);

        [OperationContract]
        bool InventorySubscriptionSet(string bookGroupSet, string desk, string inventoryType, string bizDate, string loadTime,
                    string loadStatus, string items, string lastLoadedTime, string lastLoadedVersion, string loadBizDatePrior,
                    string fileTime, string fileChecktime, string fileStatus, string fileName, string fileHost, string fileUserId,
                    string filePassword, string mailAddress, string mailSubject, string comment, string actor, bool isActive,
                    string bookGroup, string userId, string userPassword, string functionPath);
    }

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
