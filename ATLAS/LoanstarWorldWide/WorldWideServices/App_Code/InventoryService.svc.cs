using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.Common;
using StockLoan.Business;

namespace StockLoan.WebServices.InventoryService
{
    public partial class InventoryService : IInventoryService
    {
        public string GetSourceIP()
        {
            OperationContext context = OperationContext.Current;
            MessageProperties messageProperties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpointProperty =
                messageProperties[RemoteEndpointMessageProperty.Name]
                as RemoteEndpointMessageProperty;

            string sourceAddr = endpointProperty.Address.ToString();
            return sourceAddr;
        }

        public byte[] DesksGet(string desk, string bookGroup, string sIsNotSubscriber,
                                    string userId, string userPassword, string functionPath)
        {
            string moduleName = "InventoryService.DesksGet";
            bool isNotSubscriber = bool.Parse(sIsNotSubscriber.ToString());
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Inventory.DesksGet(desk, bookGroup, isNotSubscriber);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public bool DeskSet(string desk, string firmCode, string deskTypeCode, string countryCode,
                            string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "InventoryService.DeskSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))
                {
                    Inventory.DeskSet(desk, firmCode, deskTypeCode, countryCode);
                    blnResults = true;
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " Set values for " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to Update Data from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

        public byte[] DeskTypesGet(string deskTypeCode, string deskType, string sIsActive,
                                    string bookGroup, string userId, string userPassword, string functionPath)
        {
            string moduleName = "InventoryService.DeskTypesGet";
            //bool isActive = bool.Parse(sIsActive.ToString());
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Inventory.DeskTypesGet(deskTypeCode, deskType, sIsActive);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public byte[] FirmsGet(string firmCode, string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "InventoryService.FirmsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Inventory.FirmsGet(firmCode);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public bool FirmSet(string firmCode, string firm, string sIsActive, 
                            string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "InventoryService.FirmSet";
            bool blnResults = false;
            bool isActive = bool.Parse(sIsActive.ToString());
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))
                {
                    Inventory.FirmSet(firmCode, firm, isActive);
                    blnResults = true;
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " Set values for " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to Update Data from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

        public byte[] InventoryGet(string bizDate, string bookGroup, string desk, string secId, string version, string source, string sourceActor, 
                                string userId, string userPassword, string functionPath)
        {
            string moduleName = "InventoryService.InventoryGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Inventory.InventoryGet(bizDate, bookGroup, desk, secId, version, source, sourceActor);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public byte[] InventoryHistoryGet(string bookGroup, string secId, string userId, string userPassword, string functionPath)
        {
            string moduleName = "InventoryService.InventoryHistoryGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Inventory.InventoryHistoryGet(bookGroup, secId);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public bool InventoryItemSet(string bizDate, string desk, string secId, string rate, string quantity, string source,
                        string sourceActor, string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "InventoryService.InventorySet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))
                {
                    Inventory.InventoryItemSet(bizDate, bookGroup, desk, secId, quantity, rate, source, sourceActor);
                    blnResults = true;
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " Set values for " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to Update Data from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

        public byte[] InventoryFileLayoutGet(string bookGroupGet, string desk, string inventoryType,
                                string bookGroup, string userId, string userPassword, string functionPath)
        {
            string moduleName = "InventoryService.InventoryFileLayoutGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Inventory.InventoryFileLayoutGet(bookGroupGet, desk, inventoryType);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public bool InventoryFileLayoutSet(string bookGroupSet, string desk, string inventoryType, string recordLength, string headerFlag,
                string dataFlag, string trailerFlag, string delimiter, string accountLocale, string accountOrdinal,
                string accountPosition, string accountLength, string secIdOrdinal, string secIdPosition, string secIdLength,
                string quantityOrdinal, string quantityPosition, string quantityLength, 
                string rateOrdinal, string ratePosition, string rateLength, 
                string recordCountOrdinal, string recordCountPosition, string recordCountLength, 
                string bizDateDD, string bizDateMM, string bizDateYY, string actor, string bookGroup,
                string userId, string userPassword, string functionPath)
        {
            string moduleName = "InventoryService.InventoryFileLayoutSet";
            bool blnResults = false;

            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))
                {
                    Inventory.InventoryFileLayoutSet(bookGroupSet, desk, inventoryType, recordLength, headerFlag,
                            dataFlag, trailerFlag, delimiter, accountLocale, accountOrdinal, accountPosition, 
                            accountLength, secIdOrdinal, secIdPosition, secIdLength, quantityOrdinal, 
                            quantityPosition, quantityLength, rateOrdinal, ratePosition, rateLength, 
                            recordCountOrdinal, recordCountPosition, recordCountLength, bizDateDD, bizDateMM, 
                            bizDateYY, actor);
                    blnResults = true;
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " Set values for " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to Update Data from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

        public byte[] InventoryRatesGet(string bizDate, string bookGroup, string desk, string secId, string version, string source,
                string sourceActor, string userId, string userPassword, string functionPath)
        {
            string moduleName = "InventoryService.InventoryRatesGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Inventory.InventoryRatesGet(bizDate, bookGroup, desk, secId, version, source, sourceActor);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public byte[] InventorySubscriptionsGet(string bookGroupGet, string desk, string inventoryType, short utcOffset,
                                        string bookGroup, string userId, string userPassword, string functionPath)
        {
            string moduleName = "InventoryService.InventorySubscriptionsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Inventory.InventorySubscriptionsGet(bookGroupGet, desk, inventoryType, utcOffset);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
                }
            catch
            {
                throw;
            }
        }

        public bool InventorySubscriptionSet(string bookGroupSet, string desk, string inventoryType, string bizDate, string loadTime,
                    string loadStatus, string items, string lastLoadedTime, string lastLoadedVersion, string loadBizDatePrior,
                    string fileTime, string fileChecktime, string fileStatus, string fileName, string fileHost, string fileUserId,
                    string filePassword, string mailAddress, string mailSubject, string comment, string actor, bool isActive,
                    string bookGroup, string userId, string userPassword, string functionPath)
        {
            string moduleName = "InventoryService.InventorySubscriptionSet";
            bool blnResults = false;

            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))
                {
                    Inventory.InventorySubscriptionSet(bookGroupSet, desk, inventoryType, bizDate, loadTime, loadStatus, items, lastLoadedTime,
                                lastLoadedVersion, loadBizDatePrior, fileTime, fileChecktime, fileStatus, fileName, fileHost, fileUserId, 
                                filePassword, mailAddress, mailSubject, comment, actor, isActive);
                    blnResults = true;
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " Set values for " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to Update Data from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

    }
}
