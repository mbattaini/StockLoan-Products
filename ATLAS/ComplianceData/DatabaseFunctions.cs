using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

using StockLoan.Common;

namespace StockLoan.ComplianceData
{
    public class DatabaseFunctions
    {
        public static void TradeDataLoad(string bizDate, string settlementDate, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spTradeDataSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 90000;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramSettlementDate = dbCmd.Parameters.Add("@SettlementDate", SqlDbType.DateTime);
                paramSettlementDate.Value = settlementDate;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StockLoan.ComplianceData.DatabaseFunctions.TradeDataLoad]", 1);
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

        public static void CnsProjectionItemSet(string bizDate, string bookGroup, string secId, string quantity, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBoxPositionProjectedClearingFailItemSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = quantity;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StockLoan.ComplianceData.DatabaseFunctions.CnsProjectionItemSet]", 1);
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

        public static void CnsProjectionPurge(string bizDate, string bookGroup, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBoxPositionProjectedClearingFailPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;
           
                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StockLoan.ComplianceData.DatabaseFunctions.CnsProjectionPurge]", 1);
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

        public static DataSet CheckListGet(string bizDate, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsCheckList = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spCheckListGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlDataAdapter dAdapter = new SqlDataAdapter(dbCmd);
                dAdapter.Fill(dsCheckList, "CheckList");
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StockLoan.ComplianceData.DatabaseFunctions.CheckListGet]", 1);
                throw;
            }

            return dsCheckList;
        }



        public static void BroadRidgeShortAccountsPurge(string bizDate, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spShortAccountPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;


                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StockLoan.ComplianceData.DatabaseFunctions.BroadRidgeShortAccountsPurge]", 1);
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

        public static void BroadRidgeShortAccountItemSet(string bizDate, string accountNumber, string secId, string quantity, string lastUpdated, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spShortAccountSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar, 50);
                paramAccountNumber.Value = accountNumber;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = quantity;

                SqlParameter paramLastUpdated = dbCmd.Parameters.Add("@LastUpdated", SqlDbType.DateTime);
                paramLastUpdated.Value = lastUpdated;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StockLoan.ComplianceData.DatabaseFunctions.CnsProjectionItemSet]", 1);
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

        public static void BroadRidgeSatanderPositionItemSet(string bizDate, string bookGroup, string secId, string dvpFailInSettled, string dvpFailOutSettled,  string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBoxPositionSantanderItemSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 50);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramDvpFailInSettled = dbCmd.Parameters.Add("@DvpFailInSettled", SqlDbType.BigInt);
                paramDvpFailInSettled.Value = dvpFailInSettled;

                SqlParameter paramDvpFailOutSettled = dbCmd.Parameters.Add("@DvpFailOutSettled", SqlDbType.BigInt);
                paramDvpFailOutSettled.Value = dvpFailOutSettled;
                
                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StockLoan.ComplianceData.DatabaseFunctions.BroadRidgeSatanderPositionItemSet]", 1);
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
