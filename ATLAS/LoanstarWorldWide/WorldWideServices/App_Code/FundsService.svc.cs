using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.Common;
using StockLoan.Business;

namespace StockLoan.WebServices.FundsService
{

    public partial class FundsService : IFundsService
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

        public byte[] FundingRateResearchGet(string startDate, string stopDate, string fund, short utcOffset,
                                    string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "FundsService.FundingRateResearch";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Funds.FundingRateResearchGet(startDate, stopDate, fund, utcOffset);
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

        public bool FundingRateSet(string bizDate, string fund, string fundingRate, string actUserId,
                                    string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "FundsService.FundingRateSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Funds.FundingRateSet(bizDate, fund, fundingRate, actUserId);
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

        public byte[] FundingRatesGet(string bizDate, short utcOffset, string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "FundsService.FundingRatesGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Funds.FundingRatesGet(bizDate, utcOffset);
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

        public bool FundingRatesRoll(string bizDate, string bizDatePrior, string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "FundsService.FundingRatesRoll";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Funds.FundingRatesRoll(bizDate, bizDatePrior);
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

        public byte[] FundsGet(string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "FundsService.FundsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Funds.FundsGet();
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

