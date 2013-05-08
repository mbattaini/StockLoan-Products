using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBMarks
    {
        private static string dbCnStr = DBStandardFunctions.DbCnStr;

        public static DataSet MarksGet(
            string markId,
            string bizDate,
            string contractId,
            string bookGroup,
            short utcOffset)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spMarksGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!markId.Equals(""))
                {
                    SqlParameter paramMarkId = dbCmd.Parameters.Add("@MarkId", SqlDbType.VarChar, 16);
                    paramMarkId.Value = markId;
                }

                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = bizDate;
                }

                if (!contractId.Equals(""))
                {
                    SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
                    paramContractId.Value = contractId;
                }

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Marks");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void MarkSet(
            string markId,
            string bizDate,
            string bookGroup,
            string book,
            string contractId,
            string contractType,
            string secId,
            string amount,
            string openDate,
            string settleDate,
            string deliveryCode,
            string actUserId,
            bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spMarkSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramMarkId = dbCmd.Parameters.Add("@MarkId", SqlDbType.VarChar, 16);
                paramMarkId.Value = markId;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                if (!book.Equals(""))
                {
                    SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                    paramBook.Value = book;
                }

                if (!contractId.Equals(""))
                {
                    SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
                    paramContractId.Value = contractId;
                }

                if (!contractType.Equals(""))
                {
                    SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.VarChar, 3);
                    paramContractType.Value = contractType;
                }

                if (!secId.Equals(""))
                {
                    SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                    paramSecId.Value = secId;
                }

                if (!amount.Equals(""))
                {
                    SqlParameter paramAmount = dbCmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                    paramAmount.Value = amount;		// decimal(28,8)
                }

                if (!openDate.Equals(""))
                {
                    SqlParameter paramOpenDate = dbCmd.Parameters.Add("@OpenDate", SqlDbType.DateTime);
                    paramOpenDate.Value = openDate;
                }

                if (!settleDate.Equals(""))
                {
                    SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);
                    paramSettleDate.Value = settleDate;
                }

                if (!deliveryCode.Equals(""))
                {
                    SqlParameter paramDeliveryCode = dbCmd.Parameters.Add("@DeliveryCode", SqlDbType.VarChar, 1);
                    paramDeliveryCode.Value = deliveryCode;
                }

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

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

        public static int RetroMarkSet(
            string tradeDate,
            string settleDate,
            string bookGroup,
            string book,
            string contractId,
            string contractType,
            string price,
            string markId,
            string deliveryCode,
            string actUserId)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            int rowCount = 0;

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spRetroMarkSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                paramTradeDate.Value = tradeDate;

                if (!settleDate.Equals(""))
                {
                    SqlParameter paramSettleDate = dbCmd.Parameters.Add("@SettleDate", SqlDbType.DateTime);
                    paramSettleDate.Value = settleDate;
                }

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                paramBook.Value = book;

                SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
                paramContractId.Value = contractId;

                SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.VarChar, 3);
                paramContractType.Value = contractType;

                SqlParameter paramPrice = dbCmd.Parameters.Add("@Price", SqlDbType.Decimal);
                paramPrice.Value = price;

                SqlParameter paramMarkId = dbCmd.Parameters.Add("@MarkId", SqlDbType.VarChar, 16);
                paramMarkId.Value = markId;

                SqlParameter paramDeliveryCode = dbCmd.Parameters.Add("@DeliveryCode", SqlDbType.Char, 1);
                paramDeliveryCode.Value = deliveryCode;

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                SqlParameter paramRecordsUpdated = dbCmd.Parameters.Add("@RecordsUpdated", SqlDbType.Int);
                paramRecordsUpdated.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();

                rowCount = (int)paramRecordsUpdated.Value;
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

            return rowCount;
        }
    }
}
