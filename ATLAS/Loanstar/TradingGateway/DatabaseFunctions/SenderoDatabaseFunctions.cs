using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StockLoan.Common;

namespace StockLoan.DatabaseFunctions
{
    public class SenderoDatabaseFunctions
    {
        public static void BankLoanBPSPositionInsert(
            string bookGroup,
            string book,
            string loanDate,
            string secId,
            string quantity,
            string lastActivityDate,
            string dbCnStr)
        {
            SqlConnection sqlConn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBankLoanPositionInsert", sqlConn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                paramBook.Value = book;

                SqlParameter paramLoanDate = dbCmd.Parameters.Add("@LoanDate", SqlDbType.DateTime);
                paramLoanDate.Value = loanDate;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = quantity;

                SqlParameter paramLastActivityDate = dbCmd.Parameters.Add("@LastActivityDate", SqlDbType.DateTime);
                paramLastActivityDate.Value = lastActivityDate;

                sqlConn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static void BankLoanBPSPurge(string dbCnStr, string bookGroup)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            try
            {
                SqlCommand dbCmd = new SqlCommand("spBankLoanPositionBPSPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBooKGroup = dbCmd.Parameters.Add("@BooKGroup", SqlDbType.VarChar, 10);
                paramBooKGroup.Value = bookGroup;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public static void StockRecordBPSPurge(string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            try
            {
                SqlCommand dbCmd = new SqlCommand("spStockRecordBPSPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 360;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public static void BoxPositionBPSPurge(string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionBPSPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 360;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }


        public static void BoxPositionBPSSegReqCalc(string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionBPSSegReqCalc", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 360;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public static void BoxPositionBPSLongShortCalc(string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionBPSLongShortCalc", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 360;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public static void BoxPositionBPSCustomerLongCalc(string bizDate, string bookGroup, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionBPSCustomerLongCalc", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 360;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBooKGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBooKGroup.Value = bookGroup;
                
                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }


        public static void PendingTradesBPSPurge(string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            try
            {
                SqlCommand dbCmd = new SqlCommand("spPendingTradesBPSPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 360;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public static void StockRecordBulkCopy(DataTable sourceTable, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            using (SqlBulkCopy s = new SqlBulkCopy(dbCn))
            {
                s.DestinationTableName = "tbStockRecordBPS";
                s.NotifyAfter = 10000;
                s.SqlRowsCopied += new SqlRowsCopiedEventHandler(s_SqlRowsCopied);

                s.ColumnMappings.Add("BizDate", "BizDate");
                s.ColumnMappings.Add("AccountNumber", "AccountNumber");
                s.ColumnMappings.Add("AccountType", "AccountType");
                s.ColumnMappings.Add("AccountTypeDesc", "AccountTypeDesc");
                s.ColumnMappings.Add("LocLocation", "LocLocation");
                s.ColumnMappings.Add("LocMemo", "LocMemo");
                s.ColumnMappings.Add("CurrencyCode", "CurrencyCode");
                s.ColumnMappings.Add("SecurityId", "SecId");
                s.ColumnMappings.Add("TradeQuantity", "TradeQuantity");
                s.ColumnMappings.Add("SettlementQuantity", "SettlementQuantity");
                s.ColumnMappings.Add("LastUpdated", "LastUpdated");

                dbCn.Open();
                s.BulkCopyTimeout = 360;
                s.WriteToServer(sourceTable);
                s.Close();
            }
        }

        public static void s_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            Log.Write("Copied : " + e.RowsCopied + "  stock record items. [DatabaseFunctions.s_StockRecordRowsCopied]", 1);
        }

        public static void PendingTradsBulkCopy(DataTable sourceTable, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            using (SqlBulkCopy s = new SqlBulkCopy(dbCn))
            {
                s.DestinationTableName = "tbPendingTradesBPS";
                s.NotifyAfter = 10000;
                s.SqlRowsCopied += new SqlRowsCopiedEventHandler(s_SqlRowsCopied);

                s.ColumnMappings.Add("AccountNumber", "AccountNumber");
                s.ColumnMappings.Add("SecId", "SecId");
                s.ColumnMappings.Add("Quantity", "Quantity");
                s.ColumnMappings.Add("BlotterCode", "BlotterCode");

                dbCn.Open();
                s.BulkCopyTimeout = 360;
                s.WriteToServer(sourceTable);
                s.Close();
            }
        }

        public static void BPSSecMasterItemSet(string bpsIdentifier, string secId, string dbCnStr)
        {
            SqlConnection sqlConn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBPSSecMasterItemSet", sqlConn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBpsIdentifier = dbCmd.Parameters.Add("@BpsIdentifier", SqlDbType.VarChar, 12);
                paramBpsIdentifier.Value = bpsIdentifier;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                sqlConn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static void SecMasterItemSet(string secId, string symbol, string isin, string sedol, string dbCnStr)
        {
            SqlConnection sqlConn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spSecMasterItemSet", sqlConn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramSecIdTypeIndex = dbCmd.Parameters.Add("@SecIdTypeIndex", SqlDbType.TinyInt);
                paramSecIdTypeIndex.Value = 1;

                SqlParameter paramCusip = dbCmd.Parameters.Add("@CUSIP", SqlDbType.VarChar, 12);
                paramCusip.Value = secId;

                SqlParameter paramIsin = dbCmd.Parameters.Add("@ISIN", SqlDbType.VarChar, 12);
                paramIsin.Value = isin;

                SqlParameter paramSedol = dbCmd.Parameters.Add("@Sedol", SqlDbType.VarChar, 12);
                paramSedol.Value = sedol;

                sqlConn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static void SecMasterPriceBPSSet(string secId, string lastPrice, string lastPriceDate, string dbCnStr)
        {
            SqlConnection sqlConn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spSecMasterPriceBPSSet", sqlConn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramLastPrice = dbCmd.Parameters.Add("@LastPrice", SqlDbType.Float);
                paramLastPrice.Value = lastPrice;

                if (!lastPriceDate.Equals("") && !lastPriceDate.Equals("000000"))
                {
                    SqlParameter paramLastPriceDate = dbCmd.Parameters.Add("@LastPriceDate", SqlDbType.DateTime);
                    paramLastPriceDate.Value = DateTime.ParseExact(lastPriceDate, "yyMMdd", null).ToString(Standard.DateFormat);
                }

                sqlConn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static void StockRecordItemSet(string accountNumber,
            string accountType,
            string accountTypeDesc,
            string secId,
            string locLocation,
            string locMemo,
            string currencyCode,
            long tradeQuantity,
            long settlementQuantity,
            string dbCnStr)
        {
            SqlConnection sqlConn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spStockRecordBPSInsert", sqlConn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = KeyValue.Get("BizDate", "", dbCnStr);

                SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar, 9);
                paramAccountNumber.Value = accountNumber;

                SqlParameter paramAccountType = dbCmd.Parameters.Add("@AccountType", SqlDbType.Char, 1);
                paramAccountType.Value = accountType;

                SqlParameter paramAccountTypeDesc = dbCmd.Parameters.Add("@AccountTypeDesc", SqlDbType.VarChar, 25);
                paramAccountTypeDesc.Value = accountTypeDesc;

                SqlParameter paramLocLocation = dbCmd.Parameters.Add("@LocLocation", SqlDbType.Char, 1);
                paramLocLocation.Value = locLocation;

                SqlParameter paramLocMemo = dbCmd.Parameters.Add("@LocMemo", SqlDbType.Char, 1);
                paramLocMemo.Value = locMemo;

                SqlParameter paramCurrencyCode = dbCmd.Parameters.Add("@CurrencyCode", SqlDbType.VarChar, 3);
                paramCurrencyCode.Value = currencyCode;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 9);
                paramSecId.Value = secId;

                SqlParameter paramTradeQuantity = dbCmd.Parameters.Add("@TradeQuantity", SqlDbType.BigInt);
                paramTradeQuantity.Value = tradeQuantity;

                SqlParameter paramSettlementQuantity = dbCmd.Parameters.Add("@SettlementQuantity", SqlDbType.BigInt);
                paramSettlementQuantity.Value = settlementQuantity;

                sqlConn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static DataSet BorrowEasyListGet(string bizDate, string dbCnStr)
        {
            DataSet dsEasyBorrowList = new DataSet();

            try
            {
                SqlConnection dbCn = new SqlConnection(dbCnStr);
                SqlCommand dbCmd = new SqlCommand("spBorrowEasyList", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 300;

                SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                paramTradeDate.Value = bizDate;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsEasyBorrowList, "EasyBorrowList");
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }

            return dsEasyBorrowList;
        }

        public static void BoxPositionLoad(string bizDate, string bizDatePrior, string bookGroup, string firm, string dbCnStr)
        {
            string dayTime;

            SqlConnection dbCn = new SqlConnection(dbCnStr);
            SqlConnection pensonDbCn = new SqlConnection(dbCnStr);

            if (bizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) // Today is the business date.
            {
                dayTime = KeyValue.Get("BoxPositionLoadStartTime", "10:00", dbCn);

                if (dayTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0) // Time now is after the start time.
                {
                    dayTime = KeyValue.Get("BoxPositionLoadEndTime", "24:00", dbCn);

                    if (dayTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) > 0) // Time now is before the end time.
                    {
                        int n = 0;
                        SqlDataReader dataReader = null;

                        Log.Write("Will load box position data now. [StaticData.BoxPositionLoad]", 2);

                        try
                        {
                            SqlCommand pensonDbCmd = new SqlCommand("spBroadRidgeBoxSummaryGet", pensonDbCn);
                            pensonDbCmd.CommandType = CommandType.StoredProcedure;
                            pensonDbCmd.CommandTimeout = 900;

                            SqlParameter paramBizDatePenson = pensonDbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                            paramBizDatePenson.Value = bizDate;


                            SqlCommand dbCmd = new SqlCommand("spBoxPositionItemSet", dbCn);
                            dbCmd.CommandType = CommandType.StoredProcedure;

                            SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                            paramBizDate.Value = bizDate;

                            SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                            paramBookGroup.Value = bookGroup;

                            SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                            paramBizDatePrior.Value = bizDatePrior;

                            SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                            SqlParameter paramCustomerLongSettled = dbCmd.Parameters.Add("@CustomerLongSettled", SqlDbType.BigInt);
                            SqlParameter paramCustomerLongTraded = dbCmd.Parameters.Add("@CustomerLongTraded", SqlDbType.BigInt);
                            SqlParameter paramCustomerShortSettled = dbCmd.Parameters.Add("@CustomerShortSettled", SqlDbType.BigInt);
                            SqlParameter paramCustomerShortTraded = dbCmd.Parameters.Add("@CustomerShortTraded", SqlDbType.BigInt);
                            SqlParameter paramFirmLongSettled = dbCmd.Parameters.Add("@FirmLongSettled", SqlDbType.BigInt);
                            SqlParameter paramFirmLongTraded = dbCmd.Parameters.Add("@FirmLongTraded", SqlDbType.BigInt);
                            SqlParameter paramFirmShortSettled = dbCmd.Parameters.Add("@FirmShortSettled", SqlDbType.BigInt);
                            SqlParameter paramFirmShortTraded = dbCmd.Parameters.Add("@FirmShortTraded", SqlDbType.BigInt);
                            SqlParameter paramCustomerPledgeSettled = dbCmd.Parameters.Add("@CustomerPledgeSettled", SqlDbType.BigInt);
                            SqlParameter paramCustomerPledgeTraded = dbCmd.Parameters.Add("@CustomerPledgeTraded", SqlDbType.BigInt);
                            SqlParameter paramFirmPledgeSettled = dbCmd.Parameters.Add("@FirmPledgeSettled", SqlDbType.BigInt);
                            SqlParameter paramFirmPledgeTraded = dbCmd.Parameters.Add("@FirmPledgeTraded", SqlDbType.BigInt);
                            SqlParameter paramDvpFailInSettled = dbCmd.Parameters.Add("@DvpFailInSettled", SqlDbType.BigInt);
                            SqlParameter paramDvpFailInTraded = dbCmd.Parameters.Add("@DvpFailInTraded", SqlDbType.BigInt);
                            SqlParameter paramDvpFailOutSettled = dbCmd.Parameters.Add("@DvpFailOutSettled", SqlDbType.BigInt);
                            SqlParameter paramDvpFailOutTraded = dbCmd.Parameters.Add("@DvpFailOutTraded", SqlDbType.BigInt);
                            SqlParameter paramBrokerFailInSettled = dbCmd.Parameters.Add("@BrokerFailInSettled", SqlDbType.BigInt);
                            SqlParameter paramBrokerFailInTraded = dbCmd.Parameters.Add("@BrokerFailInTraded", SqlDbType.BigInt);
                            SqlParameter paramBrokerFailOutSettled = dbCmd.Parameters.Add("@BrokerFailOutSettled", SqlDbType.BigInt);
                            SqlParameter paramBrokerFailOutTraded = dbCmd.Parameters.Add("@BrokerFailOutTraded", SqlDbType.BigInt);
                            SqlParameter paramClearingFailInSettled = dbCmd.Parameters.Add("@ClearingFailInSettled", SqlDbType.BigInt);
                            SqlParameter paramClearingFailInTraded = dbCmd.Parameters.Add("@ClearingFailInTraded", SqlDbType.BigInt);
                            SqlParameter paramClearingFailOutSettled = dbCmd.Parameters.Add("@ClearingFailOutSettled", SqlDbType.BigInt);
                            SqlParameter paramClearingFailOutTraded = dbCmd.Parameters.Add("@ClearingFailOutTraded", SqlDbType.BigInt);
                            SqlParameter paramOtherFailInSettled = dbCmd.Parameters.Add("@OtherFailInSettled", SqlDbType.BigInt);
                            SqlParameter paramOtherFailInTraded = dbCmd.Parameters.Add("@OtherFailInTraded", SqlDbType.BigInt);
                            SqlParameter paramOtherFailOutSettled = dbCmd.Parameters.Add("@OtherFailOutSettled", SqlDbType.BigInt);
                            SqlParameter paramOtherFailOutTraded = dbCmd.Parameters.Add("@OtherFailOutTraded", SqlDbType.BigInt);
                            SqlParameter paramSegRequirement = dbCmd.Parameters.Add("@SegReqSettled", SqlDbType.BigInt);

                            dbCn.Open();
                            pensonDbCn.Open();
                            dataReader = pensonDbCmd.ExecuteReader();
                            dayTime = DateTime.UtcNow.ToString(Standard.DateTimeFileFormat);

                            Log.Write("Box position results returned, load commencing. [StaticData.BoxPositionLoad]", 2);

                            while (dataReader.Read())
                            {
                                paramSecId.Value = dataReader.GetValue(0);
                                paramCustomerLongSettled.Value = Tools.ParseLong(dataReader.GetValue(1).ToString());
                                paramCustomerLongTraded.Value = Tools.ParseLong(dataReader.GetValue(2).ToString());
                                paramCustomerShortSettled.Value = Tools.ParseLong(dataReader.GetValue(3).ToString());
                                paramCustomerShortTraded.Value = Tools.ParseLong(dataReader.GetValue(4).ToString());
                                paramFirmLongSettled.Value = Tools.ParseLong(dataReader.GetValue(5).ToString());
                                paramFirmLongTraded.Value = Tools.ParseLong(dataReader.GetValue(6).ToString());
                                paramFirmShortSettled.Value = Tools.ParseLong(dataReader.GetValue(7).ToString());
                                paramFirmShortTraded.Value = Tools.ParseLong(dataReader.GetValue(8).ToString());
                                paramCustomerPledgeSettled.Value = Tools.ParseLong(dataReader.GetValue(9).ToString());
                                paramCustomerPledgeTraded.Value = Tools.ParseLong(dataReader.GetValue(10).ToString());
                                paramFirmPledgeSettled.Value = Tools.ParseLong(dataReader.GetValue(11).ToString());
                                paramFirmPledgeTraded.Value = Tools.ParseLong(dataReader.GetValue(12).ToString());
                                paramDvpFailInSettled.Value = Tools.ParseLong(dataReader.GetValue(13).ToString());
                                paramDvpFailInTraded.Value = Tools.ParseLong(dataReader.GetValue(14).ToString());
                                paramDvpFailOutSettled.Value = Tools.ParseLong(dataReader.GetValue(15).ToString());
                                paramDvpFailOutTraded.Value = Tools.ParseLong(dataReader.GetValue(16).ToString());
                                paramBrokerFailInSettled.Value = Tools.ParseLong(dataReader.GetValue(17).ToString());
                                paramBrokerFailInTraded.Value = Tools.ParseLong(dataReader.GetValue(18).ToString());
                                paramBrokerFailOutSettled.Value = Tools.ParseLong(dataReader.GetValue(19).ToString());
                                paramBrokerFailOutTraded.Value = Tools.ParseLong(dataReader.GetValue(20).ToString());
                                paramClearingFailInSettled.Value = Tools.ParseLong(dataReader.GetValue(21).ToString());
                                paramClearingFailInTraded.Value = Tools.ParseLong(dataReader.GetValue(22).ToString());
                                paramClearingFailOutSettled.Value = Tools.ParseLong(dataReader.GetValue(23).ToString());
                                paramClearingFailOutTraded.Value = Tools.ParseLong(dataReader.GetValue(24).ToString());
                                paramOtherFailInSettled.Value = Tools.ParseLong(dataReader.GetValue(25).ToString());
                                paramOtherFailInTraded.Value = Tools.ParseLong(dataReader.GetValue(26).ToString());
                                paramOtherFailOutSettled.Value = Tools.ParseLong(dataReader.GetValue(27).ToString());
                                paramOtherFailOutTraded.Value = Tools.ParseLong(dataReader.GetValue(28).ToString());
                                paramSegRequirement.Value = Tools.ParseLong(dataReader.GetValue(29).ToString());

                                dbCmd.ExecuteNonQuery();

                                n++;
                                if ((n % 5000) == 0)
                                {
                                    Log.Write("Interim box position insert/update count: " + n + " [StaticData.BoxPositionLoad]", 2);
                                }
                            }

                            Log.Write("Final box position insert/update count: " + n + " [StaticData.BoxPositionLoad]", 2);


                            KeyValue.Set("BoxPositionLoadDateTime", dayTime, dbCn);
                        }
                        catch (Exception e)
                        {
                            Log.Write(e.Message + " [StaticData.BoxPositionLoad]", Log.Error, 1);
                        }
                        finally
                        {
                            if ((dataReader != null) && (!dataReader.IsClosed))
                            {
                                dataReader.Close();
                            }

                            if (dbCn.State != ConnectionState.Closed)
                            {
                                dbCn.Close();
                            }

                            if (pensonDbCn.State != ConnectionState.Closed)
                            {
                                pensonDbCn.Close();
                            }
                        }
                    }
                    else
                    {
                        Log.Write("Time now is after " + dayTime + " UTC, will wait until next business day. [StaticData.BoxPositionLoad]", 2);
                    }
                }
                else
                {
                    Log.Write("Time now is before " + dayTime + " UTC, will wait until later. [StaticData.BoxPositionLoad]", 2);
                }
            }
            else
            {
                Log.Write("Today is not the current business day, will wait. [StaticData.BoxPositionLoad]", 2);
            }
        }

        public static void BoxPositionItemSet(
            string bizDatePrior,
            string bizDate,
            string bookGroup,
            string secId,
            string exDeficitSettled,
            string customerLongSettled,
            string dvpFailInSettled,
            string dvpFailInTraded,
            string dvpFailOutSettled,
            string dvpFailOutTraded,
            string brokerFailInSettled,
            string brokerFailInTraded,
            string brokerFailOutSettled,
            string brokerFailOutTraded,
            string clearingFailInSettled,
            string clearingFailInTraded,
            string clearingFailOutSettled,
            string clearingFailOutTraded,
            string dbCnStr)
        {

            SqlConnection sqlConn = new SqlConnection(dbCnStr);
            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionBPSItemSet", sqlConn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                paramBizDatePrior.Value = bizDatePrior;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                if (!exDeficitSettled.Equals(""))
                {
                    SqlParameter paramExDeficitSettled = dbCmd.Parameters.Add("@ExDeficitSettled", SqlDbType.BigInt);
                    paramExDeficitSettled.Value = decimal.Parse(exDeficitSettled).ToString("###0");
                }

                if (!customerLongSettled.Equals(""))
                {
                    SqlParameter paramCustomerLongSettled = dbCmd.Parameters.Add("@CustomerLongSettled", SqlDbType.BigInt);
                    paramCustomerLongSettled.Value = customerLongSettled;
                }

                if (!dvpFailInSettled.Equals(""))
                {
                    SqlParameter paramDvpFailInSettled = dbCmd.Parameters.Add("@DvpFailInSettled", SqlDbType.BigInt);
                    paramDvpFailInSettled.Value = decimal.Parse(dvpFailInSettled).ToString("###0");
                }

                if (!dvpFailInTraded.Equals(""))
                {
                    SqlParameter paramDvpFailInTraded = dbCmd.Parameters.Add("@DvpFailInTraded", SqlDbType.BigInt);
                    paramDvpFailInTraded.Value = decimal.Parse(dvpFailInTraded).ToString("###0");
                }

                if (!dvpFailOutSettled.Equals(""))
                {
                    SqlParameter paramDvpFailOutSettled = dbCmd.Parameters.Add("@DvpFailOutSettled", SqlDbType.BigInt);
                    paramDvpFailOutSettled.Value = decimal.Parse(dvpFailOutSettled).ToString("###0");
                }

                if (!dvpFailOutTraded.Equals(""))
                {
                    SqlParameter paramDvpFailOutTraded = dbCmd.Parameters.Add("@DvpFailOutTraded", SqlDbType.BigInt);
                    paramDvpFailOutTraded.Value = decimal.Parse(dvpFailOutTraded).ToString("###0");
                }

                if (!brokerFailInSettled.Equals(""))
                {
                    SqlParameter paramBrokerFailInSettled = dbCmd.Parameters.Add("@BrokerFailInSettled", SqlDbType.BigInt);
                    paramBrokerFailInSettled.Value = decimal.Parse(brokerFailInSettled).ToString("###0");
                }

                if (!brokerFailInTraded.Equals(""))
                {
                    SqlParameter paramBrokerFailInTraded = dbCmd.Parameters.Add("@BrokerFailInTraded", SqlDbType.BigInt);
                    paramBrokerFailInTraded.Value = decimal.Parse(brokerFailInTraded).ToString("###0");
                }

                if (!brokerFailOutSettled.Equals(""))
                {
                    SqlParameter paramBrokerFailOutSettled = dbCmd.Parameters.Add("@BrokerFailOutSettled", SqlDbType.BigInt);
                    paramBrokerFailOutSettled.Value = decimal.Parse(brokerFailOutSettled).ToString("###0");
                }

                if (!brokerFailOutTraded.Equals(""))
                {
                    SqlParameter paramBrokerFailOutTraded = dbCmd.Parameters.Add("@BrokerFailOutTraded", SqlDbType.BigInt);
                    paramBrokerFailOutTraded.Value = decimal.Parse(brokerFailOutTraded).ToString("###0");

                }

                if (!clearingFailInSettled.Equals(""))
                {
                    SqlParameter paramClearingFailInSettled = dbCmd.Parameters.Add("@ClearingFailInSettled", SqlDbType.BigInt);
                    paramClearingFailInSettled.Value = decimal.Parse(clearingFailInSettled).ToString("###0");
                }

                if (!clearingFailInTraded.Equals(""))
                {
                    SqlParameter paramClearingFailInTraded = dbCmd.Parameters.Add("@ClearingFailInTraded", SqlDbType.BigInt);
                    paramClearingFailInTraded.Value = decimal.Parse(clearingFailInTraded).ToString("###0");
                }

                if (!clearingFailOutSettled.Equals(""))
                {
                    SqlParameter paramClearingFailOutSettled = dbCmd.Parameters.Add("@ClearingFailOutSettled", SqlDbType.BigInt);
                    paramClearingFailOutSettled.Value = decimal.Parse(clearingFailOutSettled).ToString("###0");
                }

                if (!clearingFailOutTraded.Equals(""))
                {
                    SqlParameter paramClearingFailOutTraded = dbCmd.Parameters.Add("@ClearingFailOutTraded", SqlDbType.BigInt);
                    paramClearingFailOutTraded.Value = decimal.Parse(clearingFailOutTraded).ToString("###0");
                }

                sqlConn.Open();
                dbCmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                {
                    sqlConn.Close();
                }
            }
        }

        public static void BoxPositionPurge(string  bizDate, string bookGroup, string dbCnStr)
        {
            SqlConnection sqlConn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionPurge", sqlConn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                sqlConn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                sqlConn.Close();
            }
        }


        public static void BoxPositionBPSDayCountCalc(string bizDatePrior, string bizDate, string bookGroup, string dbCnStr)
        {
            SqlConnection sqlConn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionBPSDayCountCalc", sqlConn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 1200;

                SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                paramBizDatePrior.Value = bizDatePrior;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                sqlConn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static void BoxPositionIntradayCalc(string bizDate, string bookGroup, bool intialLoad, string dbCnStr)
        {
            SqlConnection sqlConn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spBoxPositionBPSIntraDayCalc", sqlConn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 1200;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramIntialLoad = dbCmd.Parameters.Add("@IsInitalLoad", SqlDbType.Bit);
                paramIntialLoad.Value = intialLoad;

                sqlConn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static void BPSBilingSnapShot(string bizDate, string bizDatePrior, string dbCnStr)
        {
            SqlConnection sqlConn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryBPSOvernightSnapShotControl", sqlConn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 1200;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                paramBizDatePrior.Value = bizDatePrior;
               
                sqlConn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }
            finally
            {
                sqlConn.Close();
            }
        }

        public static void DtcActivityInsert(string messageType, string cusip, string quantity, string contraParty, string settleDate, string reasonCode, string pendMadeFlag, string method, string messageCopy, string dbCnStr)
        {            
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            if (messageCopy.Trim().Equals("Q"))
            {
                return;
            }

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spDtcActivityInsert", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramMessgaeType = dbCmd.Parameters.Add("@MessageType", SqlDbType.VarChar, 50);
                paramMessgaeType.Value = messageType;

                SqlParameter paramCusip = dbCmd.Parameters.Add("@Secid", SqlDbType.VarChar, 9);
                paramCusip.Value = cusip;

                SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = quantity;

                SqlParameter paramContraParty = dbCmd.Parameters.Add("@ContraParty", SqlDbType.VarChar, 4);
                paramContraParty.Value = contraParty;

                SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);
                paramSettleDate.Value = settleDate;

                SqlParameter paramReasonCode = dbCmd.Parameters.Add("@ReasonCode", SqlDbType.VarChar, 3);
                paramReasonCode.Value = reasonCode;

                SqlParameter paramPendMadeFlag = dbCmd.Parameters.Add("@PendMadeFlag", SqlDbType.VarChar, 50);
                paramPendMadeFlag.Value = pendMadeFlag;

                SqlParameter paramMethod = dbCmd.Parameters.Add("@Method", SqlDbType.VarChar, 50);
                paramMethod.Value = method;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }
    }
}
