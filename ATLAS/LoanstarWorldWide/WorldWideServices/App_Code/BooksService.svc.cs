using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.Common;
using StockLoan.Business;

namespace StockLoan.WebServices.BooksService
{
    public partial class BooksService : IBooksService
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

        public bool BookClearingInstructionSet(string bookGroup, string book, string countryCode, string currencyIso, string divRate,
              string cashInstructions, string securityInstructions, string ActUserId, bool isActive, string userId,  string userPassword, string functionPath)
        {            
            string moduleName = "BooksService.BookClearingInstructionSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Books.BookClearingInstructionSet(bookGroup, book, countryCode, currencyIso, divRate, cashInstructions, 
                            securityInstructions, ActUserId, isActive);
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

        public byte[] BookClearingInstructionsGet(string bookGroup, string book, string userId,  string userPassword, string functionPath)
        {
            string moduleName = "BooksService.BookClearingInstructionsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Books.BookClearingInstructionsGet(bookGroup, book);
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

        public bool BookContactSet(string bookGroup, string book, string firstName, string lastName, string function,
                string phoneNumber, string faxNumber, string comment, string actUserId, bool isActive, string userId,  string userPassword, string functionPath)
        {
            string moduleName = "BooksService.BookContactSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Books.BookContactSet(bookGroup, book, firstName, lastName, function, phoneNumber, faxNumber, comment,
                            actUserId, isActive);
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

        public byte[] BookContactsGet(string bookGroup, string book, short utcOffSet, string userId,  string userPassword, string functionPath)
        {
            string moduleName = "BooksService.BookContactsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Books.BookContactsGet(bookGroup, book, utcOffSet);
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

        public bool BookCreditLimitSet(string bizDate, string bookGroup, string bookParent, string book, string borrowLimitAmount,
                        string loanLimitAmount, string actUserId, string userId, string userPassword, string functionPath)
        {
            string moduleName = "BooksService.BookCreditLimitSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))
                {
                    Books.BookCreditLimitSet(bizDate, bookGroup, bookParent, book, borrowLimitAmount, loanLimitAmount, actUserId);
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

        public byte[] BookCreditLimitsGet(string bizDate, string bookGroup, string bookParent, string book,
                        short utcOffSet, string userId, string userPassword, string functionPath)
        {
            string moduleName = "BooksService.BookCreditLimitsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Books.BookCreditLimitsGet(bizDate, bookGroup, bookParent, book, utcOffSet);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                    throw new Exception(eventMsg);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public bool BookFundSet(string bookGroup, string book, string actUserId, string currencyIso, string fund, bool isActive,
                        string userId, string userPassword, string functionPath)
        {
            string moduleName = "BooksService.BookFundSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Books.BookFundSet(bookGroup, book, actUserId, currencyIso, fund, isActive);
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

        public byte[] BookFundsGet(string bookGroup, string book, string currencyIso, string userId, string userPassword, string functionPath)
        {
            string moduleName = "BooksService.BookFundsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Books.BookFundsGet(bookGroup, book, currencyIso);
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

        public bool BookGroupRoll(string bizDate, string bizDatePrior, string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "BooksService.BookGroupRoll";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Books.BookGroupRoll(bizDate, bizDatePrior);
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

        public bool BookGroupSet(string bookGroupSet, string bookName, string timeZoneId, string bizDate, string bizDateContract,
                            string bizDateBank, string bizDateExchange, string bizDatePrior, string bizDatePriorBank,
                            string bizDatePriorExchange, string bizDateNext, string bizDateNextBank, string bizDateNextExchange,
                            bool useWeekends, string settlementType, string bookGroup, string userId, string userPassword, string functionPath)
        {
            string moduleName = "BooksService.BookGroupSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                Books.BookGroupSet(bookGroupSet, bookName, timeZoneId, bizDate, bizDateContract, bizDateBank, bizDateExchange, bizDatePrior,
                                bizDatePriorBank, bizDatePriorExchange, bizDateNext, bizDateNextBank, bizDateNextExchange,
                                useWeekends, settlementType);
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

        public byte[] BookGroupsGet(string bookGroup, string bizDate, string userId, string userPassword, 
                        string functionPath)
        {
            string moduleName = "BooksService.BookGroupsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Books.BookGroupsGet(bookGroup, bizDate);
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

        public byte[] BookGroupsGetAll(string bookGroup, string bizDate, string userId, string userPassword,
                        string functionPath)
        {
            string moduleName = "BooksService.BookGroupsGetAll";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    bookGroup = "";
                    dsTemp = Books.BookGroupsGet(bookGroup, bizDate);
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

        public bool BookSet(string bookGroup, string bookParent, string book, string bookName, string addressLine1,
                string addressLine2, string addressLine3, string phoneNumber, string faxNumber, string marginBorrow, string marginLoan,
                string markRoundHouse, string roundInstitution, string rateStockBorrow, string rateStockLoan,
                string rateBondBorrow, string rateBondLoan, string countryCode, string fundDefault, string priceMin,
                string amountMin, string actUserId, bool isActive, string userId, string userPassword, string functionPath)
        {
            string moduleName = "BooksService.BookSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Books.BookSet(bookGroup, bookParent, book, bookName, addressLine1, addressLine2, addressLine3, phoneNumber, faxNumber,
                            marginBorrow, marginLoan, markRoundHouse, roundInstitution, rateStockBorrow, rateStockLoan, rateBondBorrow,
                            rateBondLoan, countryCode, fundDefault, priceMin, amountMin, actUserId,isActive);
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

        public byte[] BooksGet(string bookGroup, string book, string userId, string userPassword, string functionPath)
        {
            string moduleName = "BooksService.BooksGet";
            string eventMsg = "";
            string sourceAddr = "";

            DataSet dsTemp = new DataSet();



            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Books.BooksGet(bookGroup, book);
                    sourceAddr = GetSourceIP();
                    eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    sourceAddr = GetSourceIP();
                    eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);

                    throw new Exception("Permissions Failed.");
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<Exception>(ex, new FaultReason("Security"));
            }

            return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
        }
    }
}
