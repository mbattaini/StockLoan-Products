using System;
using System.Text;
using System.Text.RegularExpressions;

using System.Data;
using System.ServiceModel;
using System.Data.SqlClient;

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

using StockLoan.Common;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace StockLoan.Locates
{


    [System.ServiceModel.ServiceContract(Namespace = "StockLoan.Locates", SessionMode = SessionMode.Required, CallbackContract = typeof(ILocatesDuplexCallback))]
    public interface ILocatesDuplex
    {
        [OperationContract]
        string EchoData(int value);

        [OperationContract]
        DataSet Inventory(string GroupCode, string SecId, short OffsetUtc);
        [OperationContract]
        DataSet TradeDates();
        [OperationContract]
        DataSet TradingGroups();
        [OperationContract]
        DataSet Locates(short OffsetUTC, string LocateId, string TradeDateMin, string TradeDateMax, string GroupCode, string SecId, string Status);
        [OperationContract]
        string SubmitLocateList(string clientId, string groupCode, string clientComment, string locateList);        
        [OperationContract]
        DataSet LocateItemGet(string locateId, short utcOffset);        
        [OperationContract]
        void LocateItemSet(long LocateId, string Quantity, string Source, string FeeRate, string PreBorrow, string Comment, string ActUserId);

        [OperationContract]
        void LocatePreBorrowSubmit( long LocateId, string GroupCode, string SecId, string Quantity, string Rate, string ActUserId);
        [OperationContract]
        DataSet LocatesPreBorrowGet(string bizDate, string groupCode, short utcOffset);
        [OperationContract]
        void LocatesPreBorrowSet(string bizDate, string groupCode, string secId, string quantity, string rebateRate, string actUserId);


        [OperationContract]
        string BizDate();
        [OperationContract]
        string BizDateExchange();

        [OperationContract(IsOneWay = false, IsInitiating = true)]
        void Subscribe();
        [OperationContract(IsOneWay = false, IsTerminating = true)]
        void Unsubscribe();

        [OperationContract(IsOneWay = true)]
        void PublishInventoryUpdate(string Desk);

        //-------------------------------------------------------------
        // Test Procedures, to be replaced by real functions
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
    }

    public interface ILocatesDuplexCallback
    {
        //[OperationContract(IsOneWay = true)]
        //void Result(double result);

        [OperationContract(IsOneWay = true)]
        void RecieveInventoryUpdate(string Desk);
    }





    //[System.ServiceModel.ServiceContract(Namespace = "StockLoan.Locates", SessionMode = SessionMode.Required, CallbackContract = typeof(ILocatesDuplexCallback))]
    class LocateServer : ILocatesDuplex
    {
        string strDbConn;

        public static event LocatesUpdateEventHandler InventoryUpdateEvent;
        public delegate void LocatesUpdateEventHandler(object sender, LocatesUpdateEventArgs e);

        ILocatesDuplexCallback callback = null;
        LocatesUpdateEventHandler handlerLocatesUpdate = null;

        Regex rgxRequestParser;
        RegexOptions optionsRegEx = RegexOptions.Compiled | RegexOptions.Multiline;

        public LocateServer()
        {
            //callback = OperationContext.Current.GetCallbackChannel<ILocatesDuplexCallback>();

            strDbConn = string.Format("Trusted_Connection=yes; Data Source={0}; Initial Catalog={1}"
                                        , Standard.ConfigValue("MainDatabaseHost")
                                        , Standard.ConfigValue("MainDatabaseName")
                                      );

            rgxRequestParser = new Regex(@"^(?<SecId>[\w\d]+)([\s|=|:|;])(?<Quantity>[\d|,]+)", optionsRegEx);
        }

        public string EchoData(int value)
        {
            return string.Format("You entered: {0}", value);
        }


        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public void Unsubscribe()
        {
            InventoryUpdateEvent -= handlerLocatesUpdate;
        }

        public void Subscribe()
        {
            callback = OperationContext.Current.GetCallbackChannel<ILocatesDuplexCallback>();
            handlerLocatesUpdate = new LocatesUpdateEventHandler(LocatesUpdateMethod);
            InventoryUpdateEvent += handlerLocatesUpdate;
        }
        public void LocatesUpdateMethod(object sender, LocatesUpdateEventArgs e)
        {
            callback.RecieveInventoryUpdate(e.Message);
        }

        public void PublishInventoryUpdate(string Desk)
        {
            RaiseInventoryUpdateEvent(Desk);
        }
        public void RaiseInventoryUpdateEvent(string Desk)
        {
            if (null != InventoryUpdateEvent)
            {
                LocatesUpdateEventArgs e = new LocatesUpdateEventArgs();
                e.Message = Desk;
                InventoryUpdateEvent(this, e);
            }
        }

        /// <summary>
        /// Current Business Date
        /// </summary>
        /// <returns></returns>
        public string BizDate()
        {
            string strReturnBizDate = "";
            SqlConnection connSqlDb = new SqlConnection(strDbConn);

            try
            {
                strReturnBizDate = Common.KeyValue.Get("BizDate", DateTime.Today.ToString("yyyy-MM-dd"), connSqlDb);
                connSqlDb.Close();
            }
            catch (Exception Ex)
            {
                HandleException(Ex);
            }
            finally
            {
                if (connSqlDb.State != ConnectionState.Closed) {connSqlDb.Close();}
            }
            return strReturnBizDate;
        }
        public string BizDateExchange()
        {
            string strReturnBizDateExchange = "";
            SqlConnection connSqlDb = new SqlConnection(strDbConn);

            try
            {
                strReturnBizDateExchange = Common.KeyValue.Get("BizDateExchange", BizDate(), connSqlDb);
            }
            catch (Exception Ex)
            {
                HandleException(Ex);
            }
            finally
            {
                if (connSqlDb.State != ConnectionState.Closed) { connSqlDb.Close(); }
            }
            return strReturnBizDateExchange;
        }

        //public string BizDateNextExchange = "";
        //public string BizDatePriorExchange = "";

        public DataSet TradeDates()        
        {
            DataSet dataTradeDates = new DataSet();
            SqlConnection connSqlDb = new SqlConnection(strDbConn);

            try
            {               
                SqlCommand cmdTradeDates = new SqlCommand("spShortSaleTradeDates", connSqlDb);
                cmdTradeDates.CommandType = CommandType.StoredProcedure;
                cmdTradeDates.CommandTimeout = 120;

                SqlDataAdapter adptrLocates = new SqlDataAdapter(cmdTradeDates);
                adptrLocates.Fill(dataTradeDates, "TradeDates");
                connSqlDb.Close();                
            }
            catch (Exception Ex)
            {
                HandleException(Ex);
            }
            finally
            {
                if (connSqlDb.State != ConnectionState.Closed) { connSqlDb.Close(); }
            }
            return dataTradeDates;
        }

        public DataSet TradingGroups()
        {
            DataSet dataTradingGroups = new DataSet();
            SqlConnection connSqlDb = new SqlConnection(strDbConn);

            try
            {
                SqlCommand cmdTradingGroups = new SqlCommand("spTradingGroupGet", connSqlDb);
                cmdTradingGroups.CommandType = CommandType.StoredProcedure;
                cmdTradingGroups.CommandTimeout = 120;

                SqlDataAdapter adptrTradingGroups = new SqlDataAdapter(cmdTradingGroups);
                adptrTradingGroups.Fill(dataTradingGroups, "TradingGroups");
                connSqlDb.Close();
            }            
            catch (Exception Ex)
            {
                HandleException(Ex);
            }
            finally
            {
                if (connSqlDb.State != ConnectionState.Closed) { connSqlDb.Close(); }
            }
            return dataTradingGroups;

        }

        
        public DataSet Locates(short OffsetUTC, string LocateId, string TradeDateMin, string TradeDateMax, string GroupCode, string SecId, string Status)
        {
            DataSet dataLocates = new DataSet();
            SqlConnection connSqlDb = new SqlConnection(strDbConn);

            try
            {
                SqlCommand cmdLocatesGet = new SqlCommand("spShortSaleLocateGet", connSqlDb);
                cmdLocatesGet.CommandType = CommandType.StoredProcedure;

                // Offset
                SqlParameter paramUtcOffset = cmdLocatesGet.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = OffsetUTC;

                // Locate ID
                if (!String.IsNullOrEmpty(LocateId))
                {
                    SqlParameter paramLocateId = cmdLocatesGet.Parameters.Add("@LocateId", SqlDbType.BigInt);
                    paramLocateId.Value = LocateId;
                }
                // Trade Date Min
                if (!String.IsNullOrEmpty(TradeDateMin))
                {
                    SqlParameter paramTradeDateMin = cmdLocatesGet.Parameters.Add("@TradeDateMin", SqlDbType.DateTime);
                    paramTradeDateMin.Value = TradeDateMin;
                }
                // Trade Date Max
                if (!String.IsNullOrEmpty(TradeDateMax))
                {
                    SqlParameter paramTradeDateMax = cmdLocatesGet.Parameters.Add("@TradeDateMax", SqlDbType.DateTime);
                    paramTradeDateMax.Value = TradeDateMax;
                }
                // GroupCode
                if (!String.IsNullOrEmpty(GroupCode))
                {
                    SqlParameter paramGroupCode = cmdLocatesGet.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                    paramGroupCode.Value = GroupCode;
                }
                // SecId
                if (!String.IsNullOrEmpty(SecId))
                {
                    SqlParameter paramStatus = cmdLocatesGet.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                    paramStatus.Value = SecId;
                }                
                // Status
                if (!String.IsNullOrEmpty(Status))
                {
                    SqlParameter paramStatus = cmdLocatesGet.Parameters.Add("@Status", SqlDbType.VarChar, 10);
                    paramStatus.Value = Status;
                }
             
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdLocatesGet);
                dataAdapter.Fill(dataLocates, "Locates");
            }
            catch (Exception Ex)
            {
                HandleException(Ex);
            }
            finally
            {
                if (connSqlDb.State != ConnectionState.Closed) { connSqlDb.Close(); }
            }
            return dataLocates;

        }

        public DataSet Inventory(string GroupCode, string SecId, short OffsetUtc)
        {
            DataSet dataInventory = new DataSet();
            SqlConnection connSqlDb = new SqlConnection(strDbConn);

            try
            {
                SqlCommand cmdInventoryGet = new SqlCommand("spInventoryGet", connSqlDb);
                cmdInventoryGet.CommandType = CommandType.StoredProcedure;

                if (!GroupCode.Equals(""))
                {
                    SqlParameter paramGroupCode = cmdInventoryGet.Parameters.Add("@GroupCode", SqlDbType.VarChar, 12);
                    paramGroupCode.Value = GroupCode;
                }

                SqlParameter paramSecId = cmdInventoryGet.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = SecId;

                SqlParameter paramUtcOffset = cmdInventoryGet.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = OffsetUtc;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdInventoryGet);
                dataAdapter.Fill(dataInventory, "Inventory"); 
            }
            catch (Exception Ex)
            {
                HandleException(Ex);
            }
            finally
            {
                if (connSqlDb.State != ConnectionState.Closed) { connSqlDb.Close(); }
            }


            return dataInventory;
        }



        public string SubmitLocateList(string ClientId, string GroupCode, string ClientComment, string LocateList)
        {
            string strReturn = "";
            List<KeyValuePair<string, string>> listRequests = ParseRequest(LocateList);

            if (0 < listRequests.Count)
            {
                SqlConnection connSqlDb = new SqlConnection(strDbConn);
                SqlDataReader readerLocateRequest = null;

                SqlCommand cmdLocateRequest = new SqlCommand("dbo.spShortSaleLocateRequest", connSqlDb);
                cmdLocateRequest.CommandType = CommandType.StoredProcedure;

                SqlParameter paramTradeDate = cmdLocateRequest.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                paramTradeDate.Value = BizDateExchange();

                SqlParameter paramClientId = cmdLocateRequest.Parameters.Add("@ClientId", SqlDbType.VarChar, 25);
                paramClientId.Value = ClientId;

                SqlParameter paramGroupCode = cmdLocateRequest.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                paramGroupCode.Value = GroupCode;

                SqlParameter paramSecId = cmdLocateRequest.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                SqlParameter paramClientQuantity = cmdLocateRequest.Parameters.Add("@ClientQuantity", SqlDbType.BigInt);

                SqlParameter paramClientComment = cmdLocateRequest.Parameters.Add("@ClientComment", SqlDbType.VarChar, 50);
                paramClientComment.Value = ClientComment;

                SqlParameter paramReturnData = cmdLocateRequest.Parameters.Add("@ReturnData", SqlDbType.Bit);
                paramReturnData.Value = 0;

                int n = 0;

                try
                {
                    connSqlDb.Open();

                    for (int iRequest = 0; iRequest < listRequests.Count; iRequest++)
                    {
                        paramSecId.Value = listRequests[iRequest].Key;
                        paramClientQuantity.Value = listRequests[iRequest].Value;

                        readerLocateRequest = cmdLocateRequest.ExecuteReader();

                        while (readerLocateRequest.Read())
                        {
                            n += 1;
                        }

                        readerLocateRequest.Close();
                    }

                    if (n.Equals(listRequests.Count))
                    {
                        Log.Write("Processed " + (listRequests.Count) + " item[s] for " + ClientId + "|" + GroupCode + ". [ShortSaleAgent.LocateListSubmit]", 2);
                    }
                    else
                    {
                        Log.Write("Error: Processed " + n + " item[s] for " + ClientId + "|" + GroupCode +
                            " out of an expected " + (listRequests.Count) + " item[s]. [ShortSaleAgent.LocateListSubmit]", 2);

                        return "Error: Processed " + n + " item[s] out of an expected " + (listRequests.Count) + " item[s].";
                    }

                    Log.Write("Processed " + (listRequests.Count) + " item[s] for " + ClientId + "|" + GroupCode + ". [ShortSaleAgent.LocateListSubmit]", 2);
                }
                catch (Exception Ex)
                {
                    HandleException(Ex);
                    return "Error processing request.";
                }
                finally
                {
                    if (!readerLocateRequest.IsClosed)
                    {
                        readerLocateRequest.Close();
                    }

                    if (connSqlDb.State != ConnectionState.Closed)
                    {
                        connSqlDb.Close();
                    }
                }
            }


            return strReturn;
        }



        public void LocatePreBorrowSubmit( long LocateId, string GroupCode, string SecId, string Quantity, string Rate, string ActUserId)
        {
            SqlConnection connSqlDb = new SqlConnection(strDbConn);

            try
            {
                SqlCommand cmdPreBorrowRequest = new SqlCommand("spPreBorrowRequest", connSqlDb);
                cmdPreBorrowRequest.CommandType = CommandType.StoredProcedure;

                SqlParameter paramLocateId = cmdPreBorrowRequest.Parameters.Add("@LocateId", SqlDbType.BigInt);
                paramLocateId.Value = LocateId;

                SqlParameter paramBizDate = cmdPreBorrowRequest.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = BizDateExchange();

                SqlParameter paramGroupCode = cmdPreBorrowRequest.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                paramGroupCode.Value = GroupCode;

                SqlParameter paramSecId = cmdPreBorrowRequest.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = SecId;

                SqlParameter paramQuantity = cmdPreBorrowRequest.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = Quantity;

                if (!Rate.Equals(""))
                {
                    SqlParameter paramRate = cmdPreBorrowRequest.Parameters.Add("@Rate", SqlDbType.Decimal);
                    paramRate.Value = Rate;
                }

                SqlParameter paramActUserId = cmdPreBorrowRequest.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = ActUserId;

                connSqlDb.Open();
                cmdPreBorrowRequest.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                HandleException(Ex);
            }
            finally
            {
                if (connSqlDb.State != ConnectionState.Closed)
                {
                    connSqlDb.Close();
                }
            }
        }

        public DataSet LocatesPreBorrowGet(string bizDate, string groupCode, short utcOffset)
        {
            SqlConnection connSqlDb = new SqlConnection(strDbConn);
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand cmdPreBorrowGet = new SqlCommand("spPreBorrowGet", connSqlDb);
                cmdPreBorrowGet.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = cmdPreBorrowGet.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = BizDateExchange();

                if (!groupCode.Equals(""))
                {
                    SqlParameter paramGroupCode = cmdPreBorrowGet.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                    paramGroupCode.Value = groupCode;
                }

                SqlParameter paramUtcOffset = cmdPreBorrowGet.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdPreBorrowGet);
                dataAdapter.Fill(dataSet, "PreBorrows");

                Log.Write("Returning a 'PreBorrows' table with " + dataSet.Tables["PreBorrows"].Rows.Count + " rows. [ShortSaleAgent.LocatesPreBorrowGet]", 3);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ShortSaleAgent.LocatesPreBorrowGet]", Log.Error, 1);
            }

            return dataSet;
        }



        public void LocatesPreBorrowSet( string bizDate, string groupCode, string secId, string quantity, string rebateRate, string actUserId)
        {
            SqlConnection connSqlDb = new SqlConnection(strDbConn);

            try
            {
                SqlCommand cmdPreBorrowSet = new SqlCommand("spPreBorrowSet", connSqlDb);
                cmdPreBorrowSet.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = cmdPreBorrowSet.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = BizDateExchange();

                SqlParameter paramGroupCode = cmdPreBorrowSet.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                paramGroupCode.Value = groupCode;

                SqlParameter paramSecId = cmdPreBorrowSet.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramQuantity = cmdPreBorrowSet.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = quantity;

                if (!rebateRate.Equals(""))
                {
                    SqlParameter paramRebateRate = cmdPreBorrowSet.Parameters.Add("@RebateRate", SqlDbType.Float);
                    paramRebateRate.Value = rebateRate;
                }

                SqlParameter paramActUserId = cmdPreBorrowSet.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                connSqlDb.Open();
                cmdPreBorrowSet.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ShortSaleAgent.LocatesPreBorrowSet]", Log.Error, 1);
                throw;
            }
            finally
            {
                if (connSqlDb.State != ConnectionState.Closed)
                {
                    connSqlDb.Close();
                }
            }
        }




        public void LocateItemSet(long LocateId, string Quantity, string Source, string FeeRate, string PreBorrow, string Comment, string ActUserId)
        {
            SqlConnection connSqlDb = new SqlConnection(strDbConn);

            SqlCommand cmdShortSaleLocateSet = new SqlCommand("spShortSaleLocateSet", connSqlDb);
            cmdShortSaleLocateSet.CommandType = CommandType.StoredProcedure;

            SqlParameter paramLocateId = cmdShortSaleLocateSet.Parameters.Add("@LocateId", SqlDbType.BigInt);
            paramLocateId.Value = LocateId;

            if ( !string.IsNullOrEmpty(Quantity))
            {
                SqlParameter paramQuantity = cmdShortSaleLocateSet.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = Quantity;
            }

            if (!string.IsNullOrEmpty(FeeRate))
            {
                SqlParameter paramFeeRate = cmdShortSaleLocateSet.Parameters.Add("@FeeRate", SqlDbType.Float);
                paramFeeRate.Value = FeeRate;
            }

            if (!string.IsNullOrEmpty(Comment))
            {
                SqlParameter paramComment = cmdShortSaleLocateSet.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
                paramComment.Value = Comment;
            }

            if (!string.IsNullOrEmpty(PreBorrow))
            {
                SqlParameter paramPreBorrow = cmdShortSaleLocateSet.Parameters.Add("@PreBorrow", SqlDbType.Bit);
                paramPreBorrow.Value = PreBorrow;
            }

            if (!string.IsNullOrEmpty(ActUserId))
            {
                SqlParameter paramActUserId = cmdShortSaleLocateSet.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = ActUserId;
            }

            if (!string.IsNullOrEmpty(Source))
            {
                SqlParameter paramSource = cmdShortSaleLocateSet.Parameters.Add("@Source", SqlDbType.VarChar, 50);
                paramSource.Value = Source;
            }

            SqlParameter paramReturnData = cmdShortSaleLocateSet.Parameters.Add("@ReturnData", SqlDbType.Bit);
            paramReturnData.Value = 1;

            try
            {
                connSqlDb.Open();
                cmdShortSaleLocateSet.ExecuteNonQuery();

                string strMsg = "Set item: " + LocateId.ToString() + " [ShortSaleAgent.LocateItemSet]";
                // Log.Write(strMsg, 2);
                WriteLogEntry(strMsg, 2, "General");
            }
            catch (Exception Ex)
            {
                HandleException(Ex);
            }
            finally
            {
                if (connSqlDb.State != ConnectionState.Closed)
                {
                    connSqlDb.Close();
                }
            }
        }


        public DataSet LocateItemGet(string locateId, short utcOffset)
        {
            return LocateItemGet("", locateId, utcOffset);
        }

        public DataSet LocateItemGet(string groupCode, string locateId, short utcOffset)
        {
            SqlConnection connSqlDb = new SqlConnection(strDbConn);
            DataSet datasetLocateItem = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spShortSaleLocateGet", connSqlDb);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                if (!string.IsNullOrEmpty(groupCode))
                {
                    SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                    paramGroupCode.Value = groupCode;
                }

                if (!string.IsNullOrEmpty(locateId))
                {
                    SqlParameter paramLocateId = dbCmd.Parameters.Add("@LocateId", SqlDbType.BigInt);
                    paramLocateId.Value = locateId;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);

                dataAdapter.Fill(datasetLocateItem);

                string strMsg = "Get item: " + locateId + " [ShortSaleAgent.LocateItemGet]";
                // Log.Write(strMsg, 2);
                WriteLogEntry(strMsg, 2, "General");
            }
            catch (Exception e)
            {
                Log.Write(e.StackTrace + " [ShortSaleAgent.LocateItemGet]", Log.Error, 1);
            }
            finally
            {
                connSqlDb.Close();
            }

            return datasetLocateItem;
        }


        public List<KeyValuePair<string, string>> ParseRequest(string RequestList)
        {
            List<KeyValuePair<string, string>> listReturn = new List<KeyValuePair<string, string>>();

            MatchCollection clnMatches = rgxRequestParser.Matches(RequestList);
            foreach (Match matchCurrentRow in clnMatches)
            {
                string strSecId = matchCurrentRow.Groups["SecId"].Value;
                string strQuantity = matchCurrentRow.Groups["Quantity"].Value;


                if ((!string.IsNullOrEmpty(strSecId)) && (!string.IsNullOrEmpty(strQuantity)))
                {
                    KeyValuePair<string, string> pairRequestData = new KeyValuePair<string, string>(strSecId, strQuantity);
                    listReturn.Add(pairRequestData);
                }

            }

            return listReturn;
        }



        public CompositeType GetDataUsingDataContract()
        {
            CompositeType composite = new CompositeType();

            return composite;
        }



        //private void HandleException(Exception Ex)
        //{
        //    string strExMsg = string.Format("Exception Thrown From: [{0}.{1}] \nMessage:{2} \nStack Trace: {3}",
        //                                        Ex.Source,
        //                                        Ex.TargetSite.Name,
        //                                        Ex.Message,
        //                                        Ex.StackTrace);
        //    Log.Write(strExMsg, Log.Error, 1);

        //    if (Ex.GetType() == typeof(SqlException))
        //    {
        //        SqlException ExSql = (SqlException)Ex;
        //        foreach (SqlError ErrSql in ExSql.Errors)
        //        {
        //            string strErrMsg = string.Format("SQL Error Num:{0}, Occurred On Server:{1}, in Procedure: {2}, On Line Number:{3}, With Message:{4} ",
        //                                                ErrSql.Number,
        //                                                ErrSql.Server,
        //                                                ErrSql.Procedure,
        //                                                ErrSql.LineNumber,
        //                                                ErrSql.Message
        //                                            );
        //            Log.Write(strErrMsg, Log.Error, 1);                                                        
        //        }

        //    }
        //}

        private void HandleException(Exception Ex)
        {
            ExceptionPolicy.HandleException(Ex, "Exception Policy");
        }




        #region Logging
        private void WriteLogEntry(string Message, int Priority, string Category)
        {
            string[] arrayCategories = { Category };
            WriteLogEntry(Message, Priority, arrayCategories);
        }
        private void WriteLogEntry(string Message, int Priority, string[] Categories)
        {
            // Creates and fills the log entry with user information
            LogEntry logEntry = new LogEntry();
            //logEntry.EventId = EventId;
            logEntry.Priority = Priority;
            logEntry.Message = Message;
            logEntry.Categories.Clear();

            // Add the categories selected by the user
            foreach (string category in Categories)
            {
                logEntry.Categories.Add(category);
            }

            // Writes the log entry.
            Logger.Write(logEntry);
            Console.WriteLine(Message);
        }
        #endregion


    }


    public class LocatesUpdateEventArgs : EventArgs
    {
        private string strMessage = "";
        private double dblValue = 0.0;

        public double Value
        {
            get { return dblValue; }
            set { dblValue = value; }
        }

        public string Message
        {
            get { return strMessage; }
            set { strMessage = value; }
        }

    }




    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    //[DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string strValue = "Hello ";
        DataSet dsValues;


        //[DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        //[DataMember]
        public string StringValue
        {
            get { return strValue; }
            set { strValue = value; }
        }

        //[DataMember]
        public DataSet DatasetValues
        {
            get { return dsValues; }
            set { dsValues = value; }
        }
    }




}
