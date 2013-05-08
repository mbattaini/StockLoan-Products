using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBUserAdmin
    {
        private static string dbCnStr = DBStandardFunctions.DbCnStr;

        public static DataSet FunctionsGet(string functionPath)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spFunctionsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!functionPath.Equals(""))
                {
                    SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
                    paramFunctionPath.Value = functionPath;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Functions");

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

        public static DataSet RoleFunctionsBookGroupGet(string userId, string bookGroup, short utcOffSet)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spRoleFunctionsBookGroupGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!userId.Equals(""))
                {
                    SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                    paramUserId.Value = userId;
                }

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                if (!utcOffSet.Equals(""))
                {
                    SqlParameter paramUtcOffSet = dbCmd.Parameters.Add("@UtcOffSet", SqlDbType.Bit);
                    paramUtcOffSet.Value = utcOffSet;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "RoleFunctionsBookGroup");

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

        public static void RoleFunctionsBookGroupSet(
            string roleName,
            string functionPath,
            string bookGroup,
            string actUserId,
            short isActive,
            short delete)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spRoleFunctionsBookGroupSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramRoleName = dbCmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50);
                paramRoleName.Value = roleName;

                SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
                paramFunctionPath.Value = functionPath;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

                SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);
                paramDelete.Value = delete;

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


        public static void RoleFunctionSet(
            string roleName,
            string functionPath,
            bool mayView,
            bool mayEdit,
            string comment,
            string actUserId,
            bool delete)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spRoleFunctionSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramRoleName = dbCmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50);
                paramRoleName.Value = roleName;

                SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
                paramFunctionPath.Value = functionPath;

                SqlParameter paramMayView = dbCmd.Parameters.Add("@MayView", SqlDbType.Bit);
                paramMayView.Value = mayView;

                SqlParameter paramMayEdit = dbCmd.Parameters.Add("@MayEdit", SqlDbType.Bit);
                paramMayEdit.Value = mayEdit;

                if (!comment.Equals(""))
                {
                    SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
                    paramComment.Value = comment;
                }

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);
                paramDelete.Value = delete;

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

        public static void UserRoleSet(
            string userId,
            string roleName,
            string comment,
            string actUserId,
            bool delete)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spUserRoleSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramUserID = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserID.Value = userId;

                SqlParameter paramRoleName = dbCmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50);
                paramRoleName.Value = roleName;

                if (!comment.Equals(""))
                {
                    SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
                    paramComment.Value = comment;
                }

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);
                paramDelete.Value = delete;

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

        public static void FunctionSet(string functionPath, bool mayView, bool mayEdit)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spFunctionSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
                paramFunctionPath.Value = functionPath;

                SqlParameter paramMayView = dbCmd.Parameters.Add("@MayView", SqlDbType.Bit);
                paramMayView.Value = mayView;

                SqlParameter paramMayEdit = dbCmd.Parameters.Add("@MayEdit", SqlDbType.Bit);
                paramMayEdit.Value = mayEdit;

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

        public static DataSet RolesGet(string roleName)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spRolesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!roleName.Equals(""))
                {
                    SqlParameter paramRoleName = dbCmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50);      //DC 
                    paramRoleName.Value = roleName;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "RoleNames");

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

        public static void RoleSet(string roleId, string roleName, string comment, string actUserId, bool delete)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spRoleSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramRole = dbCmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50);
                paramRole.Value = roleName;

                if (!comment.Equals(""))
                {
                    SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
                    paramComment.Value = comment;
                }

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);
                paramDelete.Value = delete;

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

        public static DataSet RoleFunctionsGet(string roleName, string functionPath, short utcOffSet)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spRoleFunctionsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!roleName.Equals(""))
                {
                    SqlParameter paramRoleName = dbCmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50);
                    paramRoleName.Value = roleName;
                }

                if (!functionPath.Equals(""))
                {
                    SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
                    paramFunctionPath.Value = functionPath;
                }

                if (!utcOffSet.Equals(""))
                {
                    SqlParameter paramUtcOffSet = dbCmd.Parameters.Add("@UtcOffSet", SqlDbType.SmallInt);
                    paramUtcOffSet.Value = utcOffSet;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "RoleFunctions");

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

        public static DataSet UserRolesGet(string userId, string roleName, short utcOffSet)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spUserRolesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!userId.Equals(""))
                {
                    SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                    paramUserId.Value = userId;
                }

                if (!roleName.Equals(""))
                {
                    SqlParameter paramRoleName = dbCmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50);
                    paramRoleName.Value = roleName;
                }

                SqlParameter paramUtcOffSet = dbCmd.Parameters.Add("@UtcOffSet", SqlDbType.SmallInt);
                paramUtcOffSet.Value = utcOffSet;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "UserRoles");

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


        public static void MayViewEditBookGroupGet(
            string userId,
            string functionPath,
            string bookGroup,
            ref bool mayViewBook,
            ref bool mayEditBook)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spUserViewEditBookGroup", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserId.Value = userId;

                SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
                paramFunctionPath.Value = functionPath;

                SqlParameter paramBokGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBokGroup.Value = bookGroup;

                SqlParameter paramMayView = dbCmd.Parameters.Add("@MayView", SqlDbType.Bit);
                paramMayView.Direction = ParameterDirection.Output;

                SqlParameter paramMayEdit = dbCmd.Parameters.Add("@MayEdit", SqlDbType.Bit);
                paramMayEdit.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();

                mayViewBook = bool.Parse(paramMayView.Value.ToString());
                mayEditBook = bool.Parse(paramMayEdit.Value.ToString());

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

        public static void MayViewEditGet(string userId, string functionPath, ref bool MayView, ref bool MayEdit)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spUserViewEdit", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserId.Value = userId;

                SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
                paramFunctionPath.Value = functionPath;

                SqlParameter paramMayView = dbCmd.Parameters.Add("@MayView", SqlDbType.Bit);
                paramMayView.Direction = ParameterDirection.Output;

                SqlParameter paramMayEdit = dbCmd.Parameters.Add("@MayEdit", SqlDbType.Bit);
                paramMayEdit.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();

                MayView = bool.Parse(paramMayView.Value.ToString());
                MayEdit = bool.Parse(paramMayEdit.Value.ToString());
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

        public static DataSet UserGet(string userId, short utcOffSet)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spUsersGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!userId.Equals(""))
                {
                    SqlParameter paramUserID = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                    paramUserID.Value = userId;
                }

                if (!utcOffSet.Equals(""))
                {
                    SqlParameter paramOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                    paramOffset.Value = utcOffSet;
                }

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

        public static void UserSet(
            string userId,
            string shortName,
            string password,
            string email,
            string title,
            string comment,
            string actUserId,
            bool isLocked,
            bool isActive,
            bool isLoggedIn)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spUserSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramUserID = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserID.Value = userId;

                if (!shortName.Equals(""))
                {
                    SqlParameter paramShortName = dbCmd.Parameters.Add("@ShortName", SqlDbType.VarChar, 15);
                    paramShortName.Value = shortName;
                }

                if (!password.Equals(""))
                {
                    SqlParameter paramPassword = dbCmd.Parameters.Add("@Password", SqlDbType.VarChar, 50);
                    paramPassword.Value = password;
                }

                if (!email.Equals(""))
                {
                    SqlParameter paramEmail = dbCmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                    paramEmail.Value = email;
                }
                if (!title.Equals(""))
                {
                    SqlParameter paramTitle = dbCmd.Parameters.Add("@Title", SqlDbType.VarChar, 50);
                    paramTitle.Value = title;
                }

                if (!comment.Equals(""))
                {
                    SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar, 50);
                    paramComment.Value = comment;
                }

                if (!actUserId.Equals(""))
                {
                    SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                    paramActUserId.Value = actUserId;
                }

                SqlParameter paramIsLocked = dbCmd.Parameters.Add("@IsLocked", SqlDbType.Bit);
                paramIsLocked.Value = isLocked;

                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

                SqlParameter paramIsLoggedIn = dbCmd.Parameters.Add("@IsLoggedIn", SqlDbType.Bit);
                paramIsLoggedIn.Value = isLoggedIn;

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

        public static DataSet UserBookGroupsGet(string userId, string functionPath)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spUserBookGroupsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
                paramUserId.Value = userId;
                
                SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
                paramFunctionPath.Value = functionPath;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "UserBookGroups");

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
