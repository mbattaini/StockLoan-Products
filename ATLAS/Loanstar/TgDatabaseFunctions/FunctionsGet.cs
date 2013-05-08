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

        public static DataSet TradeMessagesGet(string bizDate, string dbCnStr)
        {
            DataSet dsMessages = new System.Data.DataSet();

            try
            {

                SqlConnection dbCn = new SqlConnection(dbCnStr);

                SqlCommand dbCmd = new SqlCommand("spTgTradeMessageGet", dbCn);
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", System.Data.SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsMessages, "Messages");
            }
            catch (Exception error)
            {
                throw;
            }

            return dsMessages;
        }

        public static DataSet ContractsGet(string bizDate, string bookGroup, string dbCnStr)
        {
            DataSet dsContracts = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spContractGet", new SqlConnection(dbCnStr));
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsContracts, "Contracts");
            }
            catch
            {
                throw;
            }

            return dsContracts;
        }

        public static DataSet ReturnsGet(string bizDate, string bookGroup, string dbCnStr)
        {
            DataSet dsReturns = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spReturnGet", new SqlConnection(dbCnStr));
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsReturns, "Returns");
            }
            catch 
            {
                throw;
            }

            return dsReturns;
        }
    }
}
