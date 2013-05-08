using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBTradeData
    {

        private static string dbCnStr = DBStandardFunctions.DbCnStr;

        public static void TradeDataContractsPurge(string bizDate, string bookGroup)
        {
            DataSet dsTradeSettings = new DataSet();
            SqlConnection dbCn = new SqlConnection(DBStandardFunctions.DbCnStr);


            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spContractPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

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

        public static DataSet TradeDataSettingsGet(string bookGroup)
        {
            DataSet dsTradeSettings = new DataSet();
            SqlConnection dbCn = new SqlConnection(DBStandardFunctions.DbCnStr);


            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookGroupTradeSettingsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTradeSettings, "BookGroups");
            }
            catch
            {
                throw;
            }

            return dsTradeSettings;
        }

        public static void TradeDataSettingSet(string bookGroup, string tradesDate, string marksDate, string recallsDate, string clientsDate)
        {            
            SqlConnection dbCn = new SqlConnection(DBStandardFunctions.DbCnStr);


            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookGroupTradeSettingSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                if (!tradesDate.Equals(""))
                {
                    SqlParameter paramFileNameTradesDate = dbCmd.Parameters.Add("@FileNameTradesDate", SqlDbType.DateTime);
                    paramFileNameTradesDate.Value = tradesDate;
                }

                if (!marksDate.Equals(""))
                {
                    SqlParameter paramFileNameMarksDate = dbCmd.Parameters.Add("@FileNameMarksDate", SqlDbType.DateTime);
                    paramFileNameMarksDate.Value = marksDate;
                }

                if (!recallsDate.Equals(""))
                {
                    SqlParameter paramFileNameRecallsDate = dbCmd.Parameters.Add("@FileNameRecallsDate", SqlDbType.DateTime);
                    paramFileNameRecallsDate.Value = recallsDate;
                }

                if (!clientsDate.Equals(""))
                {
                    SqlParameter paramFileNameClientsDate = dbCmd.Parameters.Add("@FileNameClientsDate", SqlDbType.DateTime);
                    paramFileNameClientsDate.Value = clientsDate;
                }

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

        public static void TradeDataMarksPurge(string bizDate, string bookGroup)
        {            
            SqlConnection dbCn = new SqlConnection(DBStandardFunctions.DbCnStr);


            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spTradeDataMarksPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

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

        public static void TradeDataMarkItemSet(string bizDate, string bookGroup, string contractId, string contractType, string amount)
        {
            SqlConnection dbCn = new SqlConnection(DBStandardFunctions.DbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spTradeDataMarkSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;
                
                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
                paramContractId.Value = contractId;
                    
                SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.VarChar, 1);
                paramContractType.Value = contractType;
                    
                SqlParameter paramAmount = dbCmd.Parameters.Add("@Amount", SqlDbType.Money);
                paramAmount.Value = amount;
                    
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

        public static void TradeDataContractsBizDateRoll(string bizDatePrior, string bizDate, string bookGroup)
        {
            DataSet dsTradeSettings = new DataSet();
            SqlConnection dbCn = new SqlConnection(DBStandardFunctions.DbCnStr);


            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spTradeDataContractBizDateRoll", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                paramBizDatePrior.Value = bizDatePrior;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramRecordCount = dbCmd.Parameters.Add("@RecordCount", SqlDbType.Int);
                paramRecordCount.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();

                Log.Write("Rolled : " + long.Parse(paramRecordCount.Value.ToString()).ToString("#,##0") + " contracts; For BookGroup: " + bookGroup + ".", 1);
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

        public static void TradeDataRecallsPurge(string bizDate, string bookGroup)
        {
            SqlConnection dbCn = new SqlConnection(DBStandardFunctions.DbCnStr);


            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spTradeDataRecallsPurge", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

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

        public static void TradeDataRecallItemSet(string bizDate, string bookGroup, string contractType, string contractId, string book, string secId, string quantity, string recallDate, string buyInDate, string status, string reasonCode, string recallId, string sequenceNumber, string comment)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spTradeDataRecallSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 4);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.Char, 9);
                paramContractId.Value = contractId;

                SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);
                paramContractType.Value = contractType;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.Char, 4);
                paramBook.Value = book;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 9);
                paramSecId.Value = secId;

                SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = quantity;

                SqlParameter paramRecallDate = dbCmd.Parameters.Add("@OpenDateTime", SqlDbType.DateTime);
                paramRecallDate.Value = recallDate;

                SqlParameter paramBuyInDate = dbCmd.Parameters.Add("@BuyInDate", SqlDbType.DateTime);
                paramBuyInDate.Value = buyInDate;

                SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.Char, 1);
                paramStatus.Value = status;

                SqlParameter paramReasonCode = dbCmd.Parameters.Add("@ReasonCode", SqlDbType.Char, 2);
                paramReasonCode.Value = reasonCode;

                SqlParameter paramRecallId = dbCmd.Parameters.Add("@RecallId", SqlDbType.Char, 16);
                paramRecallId.Value = recallId;

                SqlParameter paramSequenceNumber = dbCmd.Parameters.Add("@SequenceNumber", SqlDbType.SmallInt);
                paramSequenceNumber.Value = sequenceNumber;

                SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 11);
                paramComment.Value = comment;

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

        public static void LoanetActivityMessageSet(string bizDate, string messageId)
        {
            SqlConnection dbCn = new SqlConnection(DBStandardFunctions.DbCnStr);


            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spLoanetActivityMessageSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramMessageId = dbCmd.Parameters.Add("@MessageId", SqlDbType.BigInt);
                paramMessageId.Value = messageId;

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

        public static DataSet LoanetActivityMessagesGet(string bizDate)
        {
            DataSet dsTradeSettings = new DataSet();
            SqlConnection dbCn = new SqlConnection(DBStandardFunctions.DbCnStr);


            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spLoanetActivityMessagesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;                

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTradeSettings, "Messages");
            }
            catch
            {
                throw;
            }

            return dsTradeSettings;
        }
    }
}
