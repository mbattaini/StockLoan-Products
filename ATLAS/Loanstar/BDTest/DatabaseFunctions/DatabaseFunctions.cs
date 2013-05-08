using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StockLoan.Common;

namespace DatabaseFunctions
{
    public class DatabaseFunctions
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

                dbCn.Open();
                s.BulkCopyTimeout = 360;
                s.WriteToServer(sourceTable);
                s.Close();
            }
        }

        public static void  s_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
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

                SqlParameter paramSymbol = dbCmd.Parameters.Add("@Symbol", SqlDbType.VarChar, 12);
                paramSymbol.Value = symbol;

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
    }
}
