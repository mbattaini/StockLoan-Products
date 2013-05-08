using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.Common;
using StockLoan.Business;

namespace StockLoan.WebServices.ContractsService
{
    public partial class ContractsService : IContractsService
    {
     
        public byte[] ContractBillingsGet(string bizDate, string bookGroup, string userId, string userPassword, string functionPath)
        {
            string moduleName = "ContractsService.ContractBillingsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Contracts.ContractBillingsGet(bizDate);
                    string sourceAddr = ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
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

        public byte[] ContractDetailsGet(string bizDate, string bookGroup, string userId, string userPassword, string functionPath)
        {
            string moduleName = "ContractsService.ContractDetailsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Contracts.ContractDetailsGet(bizDate, bookGroup);
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
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

        public bool ContractRateChangeAsOfSet(string startDate, string endDate, string bookGroup, string book,
                   string contractId, string oldRate, string newRate, string actUserId, string userId, string userPassword, string functionPath)
        {
            string moduleName = "ContractsService.ContractRateChangeAsOfSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Contracts.ContractRateChangeAsOfSet(startDate, endDate, bookGroup, book, contractId, oldRate, newRate, actUserId);
                    blnResults = true;
                }
                else
                {
                    //This will be the event logging and notification
                }
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

        public bool ContractSet(string bizDate, string bookGroup, string contractId, string contractType, string book, string secId, string quantity,
                string quantitySettled, string amount, string amountSettled, string collateralCode, string valueDate, string settleDate,
                string termDate, string rate, string rateCode, string statusFlag, string poolCode, string divRate, bool divCallable,
                bool incomeTracked, string marginCode, string margin, string currencyIso, string securityDepot, string cashDepot,
                string otherBook, string comment, string fund, string tradeRefId, string feeAmount, string feeCurrencyIso, string feeType,
                bool returnData, bool isIncremental, bool isActive, string userId, string userPassword, string functionPath)
        {
            string moduleName = "ContractsService.ContractSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Contracts.ContractSet(bizDate,bookGroup, contractId, contractType, book, secId, quantity, quantitySettled, amount, amountSettled,
                            collateralCode, valueDate, settleDate, termDate, rate, rateCode, statusFlag, poolCode, divRate, divCallable, 
                            incomeTracked, marginCode, margin, currencyIso, securityDepot, cashDepot, otherBook, comment, functionPath, tradeRefId,
                            feeAmount, feeCurrencyIso, feeType, returnData, isIncremental, isActive);
                    blnResults = true;
                }
                else
                {
                    //This will be the event logging and notification
                }
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

        public byte[] ContractsGet(string bizDate, string bookGroup, string contractId, string contractType, string userId, string userPassword, string functionPath)
        {
            string moduleName = "ContractsService.ContractsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Contracts.ContractsGet(bizDate, bookGroup, contractId, contractType);
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
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

        public byte[] ContractsResearchGet(string bizDate, string startDate, string stopDate, string bookGroup, string book,
                string contractId, string secId, string amount, string logicId, string userId, string userPassword, string functionPath)
        {
            string moduleName = "ContractsService.ContractsResearchGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Contracts.ContractsResearchGet(bizDate, startDate, stopDate, bookGroup, book, contractId, secId, amount, logicId);
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
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

        public byte[] ContractsSummaryByBillings(string bizDate, string startDate, string stopDate, string bookGroup, string book,
                string contractId, string secId, string amount, string logicId, string userId, string userPassword, string functionPath)
        {
            string moduleName = "ContractsService.ContractsSummaryByBillings";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Contracts.ContractsSummaryByBillings(bizDate, startDate, stopDate, bookGroup, book, contractId, secId, amount, logicId);
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
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

        public byte[] ContractSummaryGet(string bizDate, string bookGroup, string userId, string userPassword, string functionPath, string usePoolCode)
        {
            string moduleName = "ContractsService.ContractSummaryGet";
            DataSet dsContracts = new DataSet();
            DataSet dsContractSummary = new DataSet();
            bool blnUsePoolCode = bool.Parse(usePoolCode);
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsContracts = Contracts.ContractsGet(bizDate, bookGroup, "", "");

                    dsContracts = Contracts.ContractDistinctCurrencies(dsContracts, bookGroup);

                    dsContractSummary = Contracts.ContractSummary(dsContracts, bookGroup, blnUsePoolCode);
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsContractSummary);
            }
            catch
            {
                throw;
            }
        }

        public byte[] ContractSummaryByBookCash(string bizDate, string bookGroup, string userId, string userPassword, string functionPath)
        {
            string moduleName = "ContractsService.ContractSummaryByBookCash";
            DataSet dsContracts = new DataSet();
            DataSet dsContractSummary = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsContracts = Contracts.ContractsGet(bizDate, bookGroup, "", "");

                    dsContracts = Contracts.ContractDistinctCurrencies(dsContracts, bookGroup);

                    dsContractSummary = Contracts.ContractSummaryByBookCash(dsContracts, bookGroup);
                    
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsContractSummary);
            }
            catch
            {
                throw;
            }
        }

        public byte[] ContractSummaryByBookProfitLossGet(string bizDate, string startDate, string stopDate, string userId, string userPassword, string bookGroup, 
                                                            string functionPath)
        {
            string moduleName = "ContractsService.ContractSummaryByBookProfitLossGet";
            DataSet dsContracts = new DataSet();
            DataSet dsContractSummary = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsContracts = Contracts.ContractsResearchGet(bizDate, startDate, stopDate, bookGroup, "", "", "", "", "");

                    dsContracts = Contracts.ContractDistinctCurrencies(dsContracts, bookGroup);

                    dsContractSummary = Contracts.ContractSummaryByBookProfitLoss(dsContracts, bookGroup);
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsContractSummary);
            }
            catch
            {
                throw;
            }
        }

        public byte[] ContractSummarybyCashGet(string bizDate, string bookGroup, string settlementType, string userId, string userPassword, string functionPath)
        {
            string moduleName = "ContractsService.ContractSummaryByCashGet";
            DataSet dsContracts = new DataSet();
            DataSet dsContractSummary = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsContracts = Contracts.ContractsGet(bizDate, bookGroup, "", "");

                    dsContracts = Contracts.ContractDistinctCurrencies(dsContracts, bookGroup);

                    dsContractSummary = Contracts.ContractSummarybyCash(dsContracts, settlementType);
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsContractSummary);
            }
            catch
            {
                throw;
            }
        }

        public byte[] ContractSummaryByCreditsDebits(string bizDate, string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "ContractsService.ContractSummaryByCreditsDebits";
            DataSet dsContractSummary = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsContractSummary = Contracts.ContractSummaryByCreditsDebits(bizDate);
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsContractSummary);
            }
            catch
            {
                throw;
            }
        }

        public byte[] ContractSummaryByHypothicationGet(string bizDate, string bookGroup, string userId, string userPassword, string functionPath)
        {
            string moduleName = "ContractsService.ContractSummaryByHypothicationGet";
            DataSet dsContracts = new DataSet();
            DataSet dsContractSummary = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsContracts = Contracts.ContractsGet(bizDate, bookGroup, "", "");

                    dsContractSummary = Contracts.ContractSummaryByHypothication(dsContracts, bookGroup);
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsContractSummary);
            }
            catch
            {
                throw;
            }
        }

        public byte[] ContractSummaryByMarketValueGet(string bizDate, string bookGroup, string userId, string userPassword, string functionPath)
        {
            string moduleName = "ContractsService.ContractSummaryByMarketValueGet";
            DataSet dsContracts = new DataSet();
            DataSet dsContractSummary = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsContracts = Contracts.ContractsGet(bizDate, bookGroup, "", "");

                    dsContractSummary = Contracts.ContractSummaryByMarketValue(dsContracts, bookGroup);
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsContractSummary);
            }
            catch
            {
                throw;
            }
        }

        public byte[] ContractSummaryBySecurityGet(string bizDate, string bookGroup, bool usePoolCode, string userId, string userPassword, string functionPath)
        {
            string moduleName = "ContractsService.ContractSummaryBySecurityGet";
            DataSet dsContracts = new DataSet();
            DataSet dsContractSummary = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsContracts = Contracts.ContractsGet(bizDate, bookGroup, "", "");

                    dsContractSummary = Contracts.ContractSummaryBySecurity(dsContracts, bookGroup, usePoolCode);
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr =  ToolFunctions.ToolFunctions.GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsContractSummary);
            }
            catch
            {
                throw;
            }
        }
    }
}
