using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using StockLoan.Common;

namespace DatabaseFunctions
{
    public class MemoSegDatabaseFunctions
    {
        public static DataSet MemoSegStartOfDayExDeficitGet(string bizDate,
                  string system,                  
                  string dbCnStr)
        {
            DataSet dsExDeficit = new DataSet();
            SqlConnection sqlConn = new SqlConnection(dbCnStr);

            SqlDataAdapter dataAdapter;

            try
            {
                SqlCommand dbCmd = new SqlCommand("spMemoSegStartOfDayExDeficitGet", sqlConn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = KeyValue.Get("BizDate", "", dbCnStr);

                SqlParameter paramSystem = dbCmd.Parameters.Add("@System", SqlDbType.VarChar, 5);
                paramSystem.Value = system;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsExDeficit, "ExDeficit");               
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                throw;
            }

            return dsExDeficit;
        }

        public static void MemoSegExDeficitInsert(string bizDate,
            string system,
            string secId,
            string isin,
            string exDeficit,
            string dbCnStr)
        {
            SqlConnection sqlConn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spMemoSegStartOfDayExDeficitInsert", sqlConn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = KeyValue.Get("BizDate", "", dbCnStr);

                SqlParameter paramSystem = dbCmd.Parameters.Add("@System", SqlDbType.VarChar, 5);
                paramSystem.Value = system;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramIsin = dbCmd.Parameters.Add("@Isin", SqlDbType.VarChar, 12);
                paramIsin.Value = isin;

                SqlParameter paramExDeficit = dbCmd.Parameters.Add("@ExDeficit", SqlDbType.Decimal);
                paramExDeficit.Value = exDeficit;

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

        public static void MemoSegExDeficitPurge(string system, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            try
            {
                SqlCommand dbCmd = new SqlCommand("spMemoSegStartOfDayExDeficitPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = KeyValue.Get("BizDate", "", dbCnStr);

                SqlParameter paramSystem = dbCmd.Parameters.Add("@System", SqlDbType.VarChar, 5);
                paramSystem.Value = system;

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
    }
}
