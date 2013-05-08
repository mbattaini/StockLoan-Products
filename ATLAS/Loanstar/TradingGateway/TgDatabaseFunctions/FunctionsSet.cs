using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace StockLoan.TgDatabaseFunctions
{
    public class FunctionsSet
    {

        public static void TradeMessageSet(string bizDate, string tradeNumber, string message, string messageResponse, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spTgTradeMessageSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramTradeNumber = dbCmd.Parameters.Add("@TradeNumber", SqlDbType.VarChar, 128);
                paramTradeNumber.Value = tradeNumber;

                if (!message.Equals(""))
                {
                    SqlParameter paramMessage = dbCmd.Parameters.Add("@Message", SqlDbType.VarChar, 8000);
                    paramMessage.Value = message;
                }

                if (!messageResponse.Equals(""))
                {
                    SqlParameter paramMessageResponse = dbCmd.Parameters.Add("@MessageResponse", SqlDbType.VarChar, 8000);
                    paramMessageResponse.Value = messageResponse;
                }

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
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

        public static void TradeReferenceSet(string bizDate, string externalRefId, string tradeNumber, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spTgTradeReferenceSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramExternalRefId = dbCmd.Parameters.Add("@ExternalRefId", SqlDbType.VarChar, 16);
                paramExternalRefId.Value = externalRefId;

                SqlParameter paramTradeNumber = dbCmd.Parameters.Add("@TradeNumber", SqlDbType.VarChar, 128);
                paramTradeNumber.Value = tradeNumber;
                
                dbCn.Open();
                dbCmd.ExecuteNonQuery();
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

        public static void TradeStatusSet(string bizDate, string tradeNumber, string status, string statusDescription, string dbCnStr)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spTgTradeStatusSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;
                
                SqlParameter paramTradeNumber = dbCmd.Parameters.Add("@TradeNumber", SqlDbType.VarChar, 128);
                paramTradeNumber.Value = tradeNumber;

                SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.VarChar, 100);
                paramStatus.Value = status;

                SqlParameter paramStatusDescription = dbCmd.Parameters.Add("@StatusDescription", SqlDbType.VarChar, 1000);
                paramStatusDescription.Value = statusDescription;               

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
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
