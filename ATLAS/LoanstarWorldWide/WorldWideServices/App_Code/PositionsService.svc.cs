using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.Common;
using StockLoan.Business;

namespace StockLoan.WebServices.PositionsService
{
    public partial class PositionsService : IPositionsService
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

        public byte[] BoxPositionGet(string bizDate, string bookGroup, string secId, string userId, string userPassword, string functionPath)
        {
            string moduleName = "PositionsService.BoxPositionGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Positions.BoxPositionGet(bizDate, bookGroup, secId);
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

        public byte[] BoxPositionLookupGet(string boxList, string bookGroup, bool includeRates, string userId)
        {
            string moduleName = "PositionsService.BoxPositionLookupGet";
            DataSet dsTemp = new DataSet();
            try
            {
                dsTemp = Positions.BoxPositionLookupGet(boxList, bookGroup, true);
                string sourceAddr = GetSourceIP();
                string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);


                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public byte[] BoxSummaryDataConfigGet(string bizDate, string bookGroup, bool isOptimistic, string hairCutUser, string userId, string userPassword, string functionPath)
        {
            string moduleName = "PositionsService.BoxSummaryDataConfigGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Positions.BoxSummaryDataConfig(bizDate, bookGroup, isOptimistic, hairCutUser);
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

    }
}
