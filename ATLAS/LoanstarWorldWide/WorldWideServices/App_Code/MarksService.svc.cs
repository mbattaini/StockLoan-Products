using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.Common;
using StockLoan.Business;

namespace StockLoan.WebServices.MarksService
{

    public partial class MarksService : IMarksService
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

        public int MarkAsOfSet(string tradeDate, string settleDate, string bookGroup, string book, string contractId, string contractType,
                string price, string markId, string deliveryCode, string actUserId, string userId, string userPassword, string functionPath)
        {
            string moduleName = "MarksService.MarkAsOfSet";
            int recordsAffected = 0;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    recordsAffected = Marks.MarkAsOfSet(tradeDate, settleDate, bookGroup, book, contractId, contractType, price, markId, 
                                        deliveryCode, actUserId);
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
                return recordsAffected;
            }
            catch
            {
                throw;
            }
        }

        public bool MarkIsExistGet(string bizDate, string bookGroup, string book, string contractId, string contractType, string secId, string amount,
                            string userId, string userPassword, string functionPath)
        {
            string moduleName = "MarksService.MarkIsExistGet";
            DataSet dsMarks = new DataSet();
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    dsMarks = Marks.MarksGet("", bizDate, "", bookGroup, 0);
                    blnResults = Marks.MarkIsExist(bookGroup, book, contractId, contractType, secId, amount, dsMarks);
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
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

        public bool MarkSet(string markId, string bizDate, string bookGroup, string book, string contractId, string contractType,
            string secId, string amount, string openDate, string settleDate, string deliveryCode, string actUserId, bool isActive,
            string userId, string userPassword, string functionPath)
        {
            string moduleName = "MarksService.MarkSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    Marks.MarkSet(markId, bizDate, bookGroup, book, contractId, contractType, secId, amount, openDate, settleDate, 
                                    deliveryCode, actUserId, isActive);
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

        public byte[] MarksGet(string markId, string bizDate, string contractId, string bookGroup, short utcOffset,
                            string userId, string userPassword, string functionPath)
        {
            string moduleName = "MarksService.MarksGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = Marks.MarksGet(markId, bizDate, contractId, bookGroup, utcOffset);
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

        public byte[] MarksSummaryByCashGet(string markId, string bizDate, string contractId, string bookGroup,
                                        string userId, string userPassword, string functionPath)
        {
            string moduleName = "MarksService.MarksSummaryByCashGet";
            DataSet dsMarks = new DataSet();
            DataSet dsMarkSummary = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsMarks = Marks.MarksGet("", bizDate, "", bookGroup, 0);

                    dsMarkSummary = Marks.MarksSummaryByCash(dsMarks);
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
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsMarkSummary);
            }
            catch
            {
                throw;
            }
        }

        public byte[] MarksSummaryGet(string markId, string bizDate, string bizDateFormat, string contractId, string bookGroup,
                                string userId, string userPassword, string functionPath)
        {
            string moduleName = "MarksService.MarksSummaryGet";
            DataSet dsMarks = new DataSet();
            DataSet dsMarkSummary = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsMarks = Marks.MarksGet("", bizDate, "", bookGroup, 0);

                    dsMarkSummary = Marks.MarkSummary(dsMarks, bizDate, bookGroup, bizDateFormat);
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
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsMarkSummary);
            }
            catch
            {
                throw;
            }
        }
    }
}
