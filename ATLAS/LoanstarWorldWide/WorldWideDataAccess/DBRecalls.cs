using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBRecalls
    {
        private static string dbCnStr = DBStandardFunctions.DbCnStr;

        public static DataSet RecallsGet(string bizDate, string recallId, string bookGroup, short utcOffset)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spRecallsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                if (!recallId.Equals(""))
                {
                    SqlParameter paramRecallId = dbCmd.Parameters.Add("@RecallId", SqlDbType.VarChar, 16);
                    paramRecallId.Value = recallId;
                }

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Recalls");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void RecallSet(
            string recallId,
            string bizDate,
            string bookGroup,
            string contractId,
            string contractType,
            string book,
            string secId,
            string quantity,
            string openDateTime,
            string moveToDate,
            string buyInDate,
            string reasonCode,
            string sequenceNumber,
            string comment,
            string status,
            string actUserId,
            bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spRecallSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramRecallId = dbCmd.Parameters.Add("@RecallId", SqlDbType.VarChar, 16);
                paramRecallId.Value = recallId;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 16);
                paramContractId.Value = contractId;

                SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.VarChar, 3);
                paramContractType.Value = contractType;

                SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
                paramBook.Value = book;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
                paramQuantity.Value = quantity;

                SqlParameter paramOpenDateTime = dbCmd.Parameters.Add("@OpenDateTime", SqlDbType.DateTime);
                paramOpenDateTime.Value = openDateTime;

                if (!moveToDate.Equals(""))
                {
                    SqlParameter paramMoveToDate = dbCmd.Parameters.Add("@MoveToDate", SqlDbType.DateTime);
                    paramMoveToDate.Value = moveToDate;
                }

                if (!buyInDate.Equals(""))
                {
                    SqlParameter paramBuyInDate = dbCmd.Parameters.Add("@BuyInDate", SqlDbType.DateTime);
                    paramBuyInDate.Value = buyInDate;
                }

                SqlParameter paramReasonCode = dbCmd.Parameters.Add("@ReasonCode", SqlDbType.VarChar, 2);
                paramReasonCode.Value = reasonCode;

                if (!sequenceNumber.Equals(""))
                {
                    SqlParameter paramSequenceNumber = dbCmd.Parameters.Add("@SequenceNumber", SqlDbType.SmallInt);
                    paramSequenceNumber.Value = sequenceNumber;
                }

                if (!comment.Equals(""))
                {
                    SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
                    paramComment.Value = comment;
                }

                SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.VarChar, 1);
                paramStatus.Value = status;

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
    }
}
