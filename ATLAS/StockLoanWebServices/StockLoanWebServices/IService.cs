using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//--------------------------
using System.ServiceModel.Web;
using System.Data;


namespace StockLoanWebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceAgent" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        string BizDate();

        [OperationContract]
        string BizDateNext();
        
        [OperationContract]
        string BizDatePrior();
        
        [OperationContract]
        string BizDateBank();
        
        [OperationContract]
        string BizDateNextBank();
        
        [OperationContract]
        string BizDatePriorBank();
        
        [OperationContract]
        string BizDateExchange();
        
        [OperationContract]
        string BizDateNextExchange();
        
        [OperationContract]
        string BizDatePriorExchange();
        
        [OperationContract]
        string ContractsBizDate();
        
        [OperationContract]
        bool IsSubstitutionActive(); 

        [OperationContract]
        byte[] KeyValueGet();

        [OperationContract]
        string KeyValueGet(string keyId, string keyValueDefault);

        [OperationContract]
        void KeyValueSet(string keyId, string keyValue);

        [OperationContract]
        string NewProcessId(string prefix);

        [OperationContract]
        byte[] ProcessStatusGet(short utcOffset);

        [OperationContract]
        byte[] FirmGet();

        [OperationContract]
        byte[] CountryGet();
        
        [OperationContract]
        byte[] DeskTypeGet();

        [OperationContract]
        byte[] DeskGet();

        [OperationContract]
        byte[] DeskGet(string desk);

        [OperationContract]
        byte[] DeskGet(bool isNotSubscriber);

        [OperationContract]
        private byte[] DeskGet(string desk, bool isNotSubscriber);

        [OperationContract]
        byte[] BookGroupGet();

        [OperationContract]
        byte[] BookGroupGet(string userId, string functionPath);

        [OperationContract]
        byte[] SecMasterLookup(string secId);

        [OperationContract]
        byte[] SecMasterLookup(string secId, bool withBox);

        [OperationContract]
        byte[] SecMasterLookup(string secId, bool withBox, bool withDeskQuips, short utcOffset, string since);

        [OperationContract]
        byte[] DeskQuipGet(short utcOffset);

        [OperationContract]
        byte[] DeskQuipGet(short utcOffset, string secId);

        [OperationContract]
        void DeskQuipSet(string secId, string deskQuip, string actUserId);

        // [OperationContract]
        // private void DeskQuipEventInvoke(DeskQuipEventArgs deskQuipEventArgs);

        [OperationContract]
        byte[] InventoryDataMaskGet(string desk);

        [OperationContract]
        void InventoryDataMaskSet(string desk, short recordLength, char headerFlag, char dataFlag, char trailerFlag, short accountLocale,
                                char delimiter, short accountOrdinal, short secIdOrdinal, short quantityOrdinal, short recordCountOrdinal,
                                short accountPosition, short accountLength, short bizDateDD, short bizDateMM, short bizDateYY, short secIdPosition,
                                short secIdLength, short quantityPosition, short quantityLength, short recordCountPosition, short recordCountLength, string actUserId);

        [OperationContract]
        byte[] SubscriberListGet(short utcOffset);

        [OperationContract]
        void SubscriberListSet(string desk,
                            string ftpPath,
                            string ftpHost,
                            string ftpUserName,
                            string ftpPassword,
                            string loadExPGP,
                            string comment,
                            string mailAddress,
                            string mailSubject,
                            string isActive,
                            string usePGP,
                            bool isBizDatePrior,
                            string actUserId);

        [OperationContract]
        byte[] PublisherListGet(short utcOffset);

        [OperationContract]
        void PublisherListSet(string desk,
                            string ftpPath,
                            string ftpHost,
                            string ftpUserName,
                            string ftpPassword,
                            string loadExPGP,
                            string comment,
                            string mailAddress,
                            string mailSubject,
                            string isActive,
                            string usePGP,
                            string reportName,
                            string reportFrequency,
                            string reportWaitUntil,
                            string actUserId);

        [OperationContract]
        byte[] PublisherReportsGet(string reportName);

        [OperationContract]
        void PublisherReportSet(string reportName, string reportStoredProc, string reportDescription);

        [OperationContract]
        void BorrowHardSet(string secId, string actUserId, bool delete);

        [OperationContract]
        void BorrowNoSet(string secId, string actUserId, bool delete);

        [OperationContract]
        byte[] InventoryDeskInputDataGet();

        // [OperationContract]
        // override object InitializeLifetimeService();


    }
}
