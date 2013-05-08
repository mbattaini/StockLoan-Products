using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBSecurity
    {
        private static string dbCnStr = DBStandardFunctions.DbCnStr;


        public static DataSet UserPasswordResetCheck(string userId, string ePassword, string sourceAddress)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spUserValidate", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramUserID = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserID.Value = userId;

                SqlParameter paramPassword = dbCmd.Parameters.Add("@Password", SqlDbType.VarChar, 50);
                paramPassword.Value = ePassword;

                SqlParameter paramSourceAddr = dbCmd.Parameters.Add("@SourceAddress", SqlDbType.VarChar, 20);
                paramSourceAddr.Value = sourceAddress;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Users");

                return dsTemp;
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

        public static DataSet UserValidate(string userId)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spUserValidate", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramUserID = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserID.Value = userId;
                
                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Users");

                return dsTemp;
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

        public static bool UserPasswordChange(string userId, string oldPassword, string newPassword)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            bool pwdChange = false;

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spUserChangePassword", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserId.Value = userId;

                SqlParameter paramOldPassword = dbCmd.Parameters.Add("@OldPassword", SqlDbType.VarChar, 50);
                paramOldPassword.Value = oldPassword;

                SqlParameter paramNewPassword = dbCmd.Parameters.Add("@NewPassword", SqlDbType.VarChar, 50);
                paramNewPassword.Value = newPassword;

                SqlParameter paramPwdChange = dbCmd.Parameters.Add("@PwdChange", SqlDbType.Bit);
                paramPwdChange.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();

                pwdChange = bool.Parse(paramPwdChange.Value.ToString()); 
                
                return pwdChange; 
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

        public static DataSet UserPasswordReset(string userId, string newEPassword)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spUserResetPassword", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserId.Value = userId;

                SqlParameter paramNewPassword = dbCmd.Parameters.Add("@NewPassword", SqlDbType.VarChar, 50);
                paramNewPassword.Value = newEPassword;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Users");

                return dsTemp;
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

        public static DataSet SecurityProfileGet(string userId, string userPassword, string bookGroup, string functionPath)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spSecurityProfileGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramUserID = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserID.Value = userId;

                SqlParameter paramUserPWD = dbCmd.Parameters.Add("@UserPassword", SqlDbType.VarChar, 50);
                paramUserPWD.Value = userPassword;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 50);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
                paramFunctionPath.Value = functionPath;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Users");

                return dsTemp;
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
