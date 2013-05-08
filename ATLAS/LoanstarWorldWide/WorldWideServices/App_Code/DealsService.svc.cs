using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.Common;
using StockLoan.Business;

namespace StockLoan.WebServices.DealsService
{
    public partial class DealsService : IDealsService
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

        public bool DealSet(string dealId, string bookGroup, string dealType, string book, string bookContact, string contractId,
                string secId, string quantity, string amount, string collateralCode, string valueDate, string settleDate, string termDate,
                string rate, string rateCode, string poolCode, string divRate, bool divCallable, bool incomeTracked, string marginCode,
                string margin, string currencyIso, string securityDepot, string cashDepot, string comment, string fund, string dealStatus,
                bool isActive, string actUserId, bool returnData, string feeAmount, string feeCurrencyIso, string feeType,
                string userId, string userPassword, string functionPath)
        {
            string moduleName = "DealsService.DealSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Deals.DealSet(dealId, bookGroup, dealType, book, bookContact, contractId, secId, quantity, amount, collateralCode, valueDate,
                            settleDate, termDate, rate, rateCode, poolCode, divRate, divCallable, incomeTracked, marginCode, margin, currencyIso,
                            securityDepot, cashDepot, comment, fund, dealStatus, isActive, actUserId, returnData, feeAmount, feeCurrencyIso, feeType);
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

        public byte[] DealsGet(string bizDate, string dealId, string dealIdPrefix, bool isActive, short utcOffSet,
                            string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "DealsService.DealsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                dsTemp = Deals.DealsGet(bizDate, dealId, dealIdPrefix, isActive, utcOffSet);
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

        public bool DealToContract(string dealId, string bizDate, string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "DealsService.DealToContract";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Deals.DealToContract(dealId, bizDate);
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

