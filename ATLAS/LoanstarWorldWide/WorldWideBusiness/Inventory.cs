using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.DataAccess;

namespace StockLoan.Business
{
    public class Inventory
    {

        public static DataSet DesksGet(string desk, string bookGroup, bool isNotSubscriber)
        {
            DataSet dsTemp = new DataSet();
            
            try
            {
                dsTemp = DBInventory.DesksGet(desk, bookGroup, isNotSubscriber);

                return dsTemp;
            }
            catch
            {
                throw;
            }
        }

        public static void DeskSet(string desk, string firmCode, string deskTypeCode, string countryCode)
        {
            try
            {
                if (desk.Equals(""))
                {
                    throw new Exception("Desk is required");
                }
            
                if (firmCode.Equals(""))
                {
                    throw new Exception("Firm Code is required");
                }
                
                if (deskTypeCode.Equals(""))
                {
                    throw new Exception("Desk Type Code is required");
                }
                
                if (countryCode.Equals(""))
                {
                    throw new Exception("Country Code is required");
                }

                DBInventory.DeskSet(desk, firmCode, deskTypeCode, countryCode);

            }
            catch
            {
                throw;
            }
        }

        public static DataSet DeskTypesGet(string deskTypeCode, string deskType, string sIsActive)
        {
            DataSet dsTemp = new DataSet();
            
            try
            {
                dsTemp = DBInventory.DeskTypesGet(deskTypeCode, deskType, sIsActive);

                return dsTemp;
            }
            catch
            {
                throw;
            }

        }    

        public static DataSet FirmsGet(string firmCode)
        {
            DataSet dsTemp = new DataSet();
            
            try
            {
                dsTemp = DBInventory.FirmsGet(firmCode);

                return dsTemp;
            }
            catch
            {
                throw;
            }
        }

        public static void FirmSet(string firmCode, string firm, bool isActive)
        {
            try
            {
                if (firmCode.Equals(""))
                {
                    throw new Exception("Firm Code is required");
                }
            
                if (firm.Equals(""))
                {
                    throw new Exception("Firm Name is required");
                }
                
                DBInventory.FirmSet(firmCode, firm, isActive);
            }
            catch
            {
                throw;
            }
        }

        public static DataSet InventoryGet(
            string bizDate, 
            string bookGroup, 
            string desk, 
            string secId, 
            string version, 
            string source, 
            string sourceActor)
        {
            DataSet dsTemp = new DataSet();
            
            try
            {
                dsTemp = DBInventory.InventoryGet(bizDate, bookGroup, desk, secId, version, source, sourceActor);

                return dsTemp;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet InventoryHistoryGet(
            string bookGroup,
            string secId)
        {
            DataSet dsTemp = new DataSet();

            try
            {
                dsTemp = DBInventory.InventoryHistoryGet(bookGroup, secId);

                return dsTemp;
            }
            catch
            {
                throw;
            }
        }

        public static void InventoryItemSet(
            string bizDate, 
            string bookGroup, 
            string desk, 
            string secId, 
            string quantity, 
            string rate, 
            string source, 
            string sourceActor)
        {

            try
            {
                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date is required");
                }

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required");
                }
            
                if (desk.Equals(""))
                {
                    throw new Exception("Desk is required");
                }
                
                if (secId.Equals(""))
                {
                    throw new Exception("Security ID is required");
                }
                
                if (source.Equals(""))
                {
                    throw new Exception("Source is required");
                }
                
                if (sourceActor.Equals(""))
                {
                    throw new Exception("Source Actor (User ID) is required");
                }
                
                DBInventory.InventoryItemSet(bizDate, bookGroup, desk, secId, quantity, rate, source, sourceActor);
                
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet InventoryFileLayoutGet(string bookGroup, string desk, string type)
        {
            DataSet dsTemp = new DataSet();
            try
            {
                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required");
                }
                
                if (desk.Equals(""))
                {
                    throw new Exception("Desk is required");
                }
                
                if (type.Equals(""))
                {
                    throw new Exception("File Type is required");
                }
                
                dsTemp = DBInventory.InventoryFileLayoutGet(bookGroup, desk, type);
                
                return dsTemp;
            }
            catch
            {
                throw;
            }
        }

        public static void InventoryFileLayoutSet(
            string bookGroup, 
            string desk, 
            string inventoryType, 
            string recordLength, 
            string headerFlag,
            string dataFlag, 
            string trailerFlag, 
            string delimiter, 
            string accountLocale, 
            string accountOrdinal,
            string accountPosition, 
            string accountLength, 
            string secIdOrdinal, 
            string secIdPosition,
            string secIdLength,
            string quantityOrdinal, 
            string quantityPosition, 
            string quantityLength, 
            string rateOrdinal,
            string ratePosition, 
            string rateLength, 
            string recordCountOrdinal, 
            string recordCountPosition,
            string recordCountLength, 
            string bizDateDD, 
            string bizDateMM, 
            string bizDateYY, 
            string actor)
        {
            try
            {
                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required");
                }
                
                if (desk.Equals(""))
                {
                    throw new Exception("Desk is required");
                }
                
                if (inventoryType.Equals(""))
                {
                    throw new Exception("File Type is required");
                }

                DBInventory.InventoryFileLayoutSet(
                    bookGroup, 
                    desk, 
                    inventoryType, 
                    recordLength, 
                    headerFlag, 
                    dataFlag,
                    trailerFlag, 
                    delimiter, 
                    accountLocale, 
                    accountOrdinal, 
                    accountPosition,
                    accountLength,
                    secIdOrdinal,
                    secIdPosition, 
                    secIdLength,
                    quantityOrdinal,
                    quantityPosition,
                    quantityLength,
                    rateOrdinal,
                    ratePosition,
                    rateLength, 
                    recordCountOrdinal, 
                    recordCountPosition,
                    recordCountLength, 
                    bizDateDD, 
                    bizDateMM,
                    bizDateYY,
                    actor);
            }
            catch
            {
                throw;
            }
        }

        public static DataSet InventoryRatesGet(
            string bizDate, 
            string bookGroup, 
            string desk, 
            string secId, 
            string version, 
            string source, 
            string sourceActor)
        {
            DataSet dsTemp = new DataSet();
            try
            {
                
                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required");
                }
                
                dsTemp = DBInventory.InventoryRatesGet(
                    bizDate, 
                    bookGroup, 
                    desk, 
                    secId, 
                    version, 
                    source,
                    sourceActor);
                
                return dsTemp;
            }
            catch
            {
                throw;
            }
        }

        public static DataSet InventorySubscriptionsGet(string bookGroup, string desk, string inventoryType, short utcOffset)
        {
            short utcOffSet = 0;            
            DataSet dsTemp = new DataSet();
            
            try
            {
                dsTemp = DBInventory.InventorySubscriptionsGet(bookGroup, desk, inventoryType, utcOffSet);
                
                return dsTemp;
            }
            catch
            {
                throw;
            }
        }

        public static void InventorySubscriptionSet(
            string bookGroup, 
            string desk, 
            string inventoryType, 
            string bizDate, 
            string loadTime, 
            string loadStatus,
            string items,
            string lastLoadedTime,
            string lastLoadedVersion,
            string loadBizDatePrior,
            string fileTime, 
            string fileChecktime,
            string fileStatus,
            string fileName, 
            string fileHost, 
            string fileUserId,
            string filePassword, 
            string mailAddress,
            string mailSubject, 
            string comment, 
            string actor, 
            bool isActive)
        {
            try
            {
                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required");
                }

                if (desk.Equals(""))
                {
                    throw new Exception("Desk is required");
                }

                if (inventoryType.Equals(""))
                {
                    throw new Exception("Subscription Type is required");
                }

                DBInventory.InventorySubscriptionSet(
                    bookGroup,
                    desk,
                    inventoryType,
                    bizDate,
                    loadTime,
                    loadStatus,
                    items,
                    lastLoadedTime,
                    lastLoadedVersion,
                    loadBizDatePrior,
                    fileTime,
                    fileChecktime,
                    fileStatus,
                    fileName,
                    fileHost,
                    fileUserId,
                    filePassword,
                    mailAddress,
                    mailSubject,
                    comment,
                    actor,
                    isActive);
            }
            catch
            {
                throw;
            }
        }
    }
}
