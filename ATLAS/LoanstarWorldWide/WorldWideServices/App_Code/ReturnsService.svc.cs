using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.Common;
using StockLoan.Business;

namespace StockLoan.WebServices.ReturnsService
{
    public partial class ReturnsService : IReturnsService
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

        public bool ReturnAsOfSet(string tradeDate, string bookGroup, string book, string contractId, string contractType,
                string returnId, string quantity, string actUserId, string settleDate, string userId, string userPassword, string functionPath)
        {
            string moduleName = "ReturnsService.ReturnAsOfSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Returns.ReturnAsOfSet(tradeDate, bookGroup, book, contractId, contractType, returnId, quantity, actUserId, settleDate);
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

        public bool ReturnSet(string returnId, string bizDate, string bookGroup, string book, string contractId, string contractType,
                    string quantity, string actUserId, string settleDateProjected, string settleDateActual, bool isActive,
                    string userId, string userPassword, string functionPath)
        {
            string moduleName = "ReturnsService.ReturnSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Returns.ReturnSet(returnId, bizDate, bookGroup, book, contractId, contractType, quantity, actUserId, settleDateProjected, 
                                settleDateActual, isActive);
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

        public byte[] ReturnsGet(string returnId, string bizDate, string bookGroup, string contractId, short utcOffSet,
                            string userId, string userPassword, string functionPath)
        {
            string moduleName = "ReturnsService.ReturnsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Returns.ReturnsGet(returnId, bizDate, bookGroup, contractId, utcOffSet);
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
        
        public byte[] ReturnsSummaryByCashGet(string returnId, string bizDate, string bookGroup, string contractId, short utcOffSet,
                                        string userId, string userPassword, string functionPath)
        {
            string moduleName = "ReturnsService.ReturnsSummaryByCashGet";
            DataSet dsReturns = new DataSet();
            DataSet dsSummary = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsReturns = Returns.ReturnsGet(returnId, bizDate, bookGroup, contractId, utcOffSet);

                    dsSummary = Returns.ReturnsSummaryByCash(dsReturns);
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
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsSummary);
            }
            catch
            {
                throw;
            }
        }
    }
}
