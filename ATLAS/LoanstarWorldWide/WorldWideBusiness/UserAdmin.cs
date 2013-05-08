using System;
using System.Data;
using StockLoan.DataAccess;
using StockLoan.Transport;

using StockLoan.Common;

namespace StockLoan.Business
{

    public class UserAdmin
    {

        public static DataSet RolesGet(string roleName)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = DBUserAdmin.RolesGet(roleName);

                return dsTemp;
            }
            catch 
            {
                throw;
            }
        }

        public static void RoleSet(string roleId, string roleName, string comment, string actUserId, bool delete)
        {
            try
            {
                if (roleName.Equals(""))
                {
                    throw new Exception("Role Name is required");
                }

                if (actUserId.Equals(""))
                {
                    throw new Exception("User Id is required");
                }

                DBUserAdmin.RoleSet(roleId, roleName, comment, actUserId, delete);
            }
            catch 
            {
                throw;
            }
        }


        public static DataSet FunctionsGet(string functionPath)
        {
            try
            {
                DataSet dsFunctions = new DataSet();

                dsFunctions = DBUserAdmin.FunctionsGet(functionPath);

                return dsFunctions;
            }
            catch 
            {
                throw;
            }
        }


        public static DataSet UserBookGroupsGet(string userId, string functionPath)
        {
            DataSet dsTemp = new DataSet();

            try
            {
                if (userId.Equals(""))
                {
                    throw new Exception("User ID is required");
                }

                if (functionPath.Equals(""))
                {
                    throw new Exception("Function Path is required");
                }

                dsTemp = DBUserAdmin.UserBookGroupsGet(userId, functionPath);
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void FunctionSet(string functionPath, bool mayView, bool mayEdit)
        {
            try
            {
                if (functionPath.Equals(""))
                {
                    throw new Exception("Function Path is required");
                }

                DBUserAdmin.FunctionSet(functionPath, mayView, mayEdit);
            }
            catch 
            {
                throw;
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
            try
            {

                if (roleName.Equals(""))
                {
                    throw new Exception("Role Name is required");
                }

                if (functionPath.Equals(""))
                {
                    throw new Exception("Function Path is required");
                }

                if (actUserId.Equals(""))
                {
                    throw new Exception("Act User Id is required");
                }

                DBUserAdmin.RoleFunctionSet(
                    roleName,
                    functionPath, 
                    mayView, 
                    mayEdit, 
                    comment,
                    actUserId, 
                    delete);
            }
            catch 
            {
                throw;
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
            try
            {

                if (roleName.Equals(""))
                {
                    throw new Exception("Role Name is required");
                }

                if (functionPath.Equals(""))
                {
                    throw new Exception("Function Path is required");
                }

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required");
                }

                if (actUserId.Equals(""))
                {
                    throw new Exception("Act User Id is required");
                }

                DBUserAdmin.RoleFunctionsBookGroupSet(
                    roleName, 
                    functionPath, 
                    bookGroup, 
                    actUserId, 
                    isActive,
                    delete);
            }
            catch
            {
                throw;
            }
        }

        
        public static void UserRolesSet(string userId, string roleName, string comment, string actUserId, bool delete)
        {
            try
            {
                if (userId.Equals(""))
                {
                    throw new Exception("User ID is required");
                }
                if (roleName.Equals(""))
                {
                    throw new Exception("Role name is required");
                }
                if (actUserId.Equals(""))
                {
                    throw new Exception("Act User Id is required");
                }

                DBUserAdmin.UserRoleSet(userId, roleName, comment, actUserId, delete);
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet UserGet(string userId)
        {
            try
            {
                DataSet dsUserList = new DataSet();

                dsUserList = DBUserAdmin.UserGet(userId, 0);

                return dsUserList;
            }
            catch 
            {
                throw;
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

            try
            {
                if (userId.Equals(""))
                {
                    throw new Exception("User Id is required");
                }

                DBUserAdmin.UserSet(
                    userId, 
                    shortName,
                    password, 
                    email, 
                    title, 
                    comment,
                    actUserId, 
                    isLocked,
                    isActive, 
                    isLoggedIn);
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet RoleFunctionsGet(string roleName, string functionPath, short utcOffSet)
        {
            try
            {
                DataSet dsRoleFunctions = new DataSet();

                dsRoleFunctions = DBUserAdmin.RoleFunctionsGet(roleName, functionPath, utcOffSet);

                return dsRoleFunctions;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet RoleFunctionsBookGroupGet(string userId, string bookGroup, short utcOffSet)
        {
            try
            {
                DataSet dsRoleFunctionsBookGroup = new DataSet();

                dsRoleFunctionsBookGroup = DBUserAdmin.RoleFunctionsBookGroupGet(userId, bookGroup, utcOffSet);

                if (userId.Equals(""))
                { 
                    DataSet dsTemp = new DataSet();
                    DataSet dsReturn = new DataSet();

                    dsTemp = dsRoleFunctionsBookGroup;

                    dsReturn = Functions.ExtractDistinctActiveRoles(dsTemp);

                    dsRoleFunctionsBookGroup = dsReturn;
                }

                
                return dsRoleFunctionsBookGroup;
            }
            catch
            {
                throw;
            }
        }

        public static DataSet UserRolesGet(string userId, string roleName, short utcOffset)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = DBUserAdmin.UserRolesGet(userId, roleName, utcOffset);

                return dsTemp;
            }
            catch 
            {
                throw;
            }
        }

        public static void MayViewEditBookGroupGet(
            string userId, 
            string functionPath, 
            string bookGroup, 
            ref bool mayViewBookGroup, 
            ref bool mayEditBookGroup)
        {
            try
            {
                if (userId.Equals(""))
                {
                    throw new Exception("User ID is required");
                }
             
                if (functionPath.Equals(""))
                {
                    throw new Exception("Function Path is required");
                }
                
                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required");
                }

                DBUserAdmin.MayViewEditBookGroupGet(
                    userId, 
                    functionPath,
                    bookGroup, 
                    ref mayViewBookGroup, 
                    ref mayEditBookGroup);
            }
            catch 
            {
                throw;
            }
        }

        public static void MayViewEditGet(string userId, string functionName, ref bool mayView, ref bool mayEdit)
        {
            try
            {
                if (userId.Equals(""))
                {
                    throw new Exception("User ID is required");
                }
                if (functionName.Equals(""))
                {
                    throw new Exception("Function Name is required");
                }

                DBUserAdmin.MayViewEditGet(userId, functionName, ref mayView, ref mayEdit);

            }
            catch 
            {
                throw;
            }
        }

    }
}
