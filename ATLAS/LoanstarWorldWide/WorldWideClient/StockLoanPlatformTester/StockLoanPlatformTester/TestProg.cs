using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
//using StockLoan.DataAccess;
using StockLoan.Encryption;

namespace StockLoan.Tester
{
    public class SLTester
    {
        private static string dbCnStr = ""; //DBStandardFunctions.DbCnStr;

        public static void PwdTest()
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spUsersGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 300;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Users");

                foreach (DataRow dr in dsTemp.Tables["Users"].Rows)
                {
                    if (!dr["UserId"].ToString().Equals(""))
                    {
                        string UID = dr["UserId"].ToString();
                        string Pwd = dr["Password"].ToString();
                        string ePwd = EncryptDecrypt.EncryptString(Pwd);

                        string Sql = "Update tbUsers set Password = '" + ePwd + "' Where UserId = '" + UID + "'";

                        //MessageBox.Show(Sql);


                        //dr.AcceptChanges();
                    }
                }

                
                //dsTemp.RemotingFormat = SerializationFormat.Binary;
            }
            catch
            {
                throw;
            }

        }

        public static void RecallSet(string recallId, string bizDate, string bookGroup, string contractId, string contractType,
                                     string book, string secId, string quantity, string openDateTime, string moveToDate,
                                     string buyInDate, string reasonCode, string sequenceNumber, string comment, string status,
                                     string actUserId, bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spRecallSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 300;

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
                    SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 20);
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
