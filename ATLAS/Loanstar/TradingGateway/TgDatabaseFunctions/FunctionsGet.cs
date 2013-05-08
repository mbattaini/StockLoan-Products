using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace StockLoan.TgDatabaseFunctions
{
    public class FunctionsGet
    {
        public static DataSet TradeCounterPartiesGet(string system, string dbCnStr)
        {
            DataSet dsCounterParties = new DataSet();

            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spTgTradeSystemsCounterPartiesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                if (!system.Equals(""))
                {
                    SqlParameter paramSystem = dbCmd.Parameters.Add("@System", SqlDbType.VarChar, 10);
                    paramSystem.Value = system;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsCounterParties, "CounterParties");
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

            return dsCounterParties;
        }

        public static DataSet TradeSystemsGet(string dbCnStr)
        {
            DataSet dsCounterParties = new DataSet();

            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spTgTradeSystemsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
              
                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsCounterParties, "Systems");
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

            return dsCounterParties;
        }
    }
}
