using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.Common;
using StockLoan.Business;

namespace StockLoan.WebServices.UserAdminService
{

    public partial class UserAdminService : IUserAdminService
    {
        public string GetSourceIP()
        {
            OperationContext context = OperationContext.Current;
            MessageProperties messageProperties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpointProperty =
                messageProperties[RemoteEndpointMessageProperty.Name]
                as RemoteEndpointMessageProperty;

            string sourceAddr = endpointProperty.Address.ToString();
            return sourceAddr;
        }

        public bool FunctionSet(string functionPathSet, bool mayView, bool mayEdit, string bookGroup, string functionPath, string userId, string userPassword)
        {   
            string moduleName = "UserAdminService.FunctionSet";
            bool blnResults = false;
            
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    UserAdmin.FunctionSet(functionPathSet, mayView, mayEdit);
                    blnResults = true;
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " Set values for " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to Update Data from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

        public byte[] FunctionsGet(string functionPathGet, string bookGroup, string functionPath, string userId, string userPassword)
        {   //DC  20110125
            string moduleName = "UserAdminService.FunctionsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = UserAdmin.FunctionsGet(functionPathGet);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public bool RoleFunctionSet(string roleName, string functionPathSet, bool mayView, bool mayEdit, string comment, bool delete,
                                    string userId, string userPassword, string bookGroup, string functionPath)
        {   //DC 20110126
            string moduleName = "UserAdminService.RoleFunctionSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    UserAdmin.RoleFunctionSet(roleName, functionPathSet, mayView, mayEdit, comment, userId, delete);
                    blnResults = true;
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " Set values for " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to Update Data from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

        public bool RoleFunctionsBookGroupSet(string roleNameSet, string functionPathSet, string bookGroupSet, string actUserId, short isActive,
                        short delete, string bookGroup, string functionPath, string userId, string userPassword)
        {   
            string moduleName = "UserAdminService.RoleFunctionsBookGroupSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))
                {
                    UserAdmin.RoleFunctionsBookGroupSet(roleNameSet, functionPathSet, bookGroupSet, actUserId, isActive, delete);
                    blnResults = true;
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " Set values for " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to Update Data from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

        public bool RoleSet(string roleId, string roleName, string comment, bool delete, 
                            string userId, string userPassword, string bookGroup, string functionPath)
        {   //DC 20110121 
            string moduleName = "UserAdminService.RoleSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    UserAdmin.RoleSet(roleId, roleName, comment, userId, delete);
                    blnResults = true;
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " Set values for " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to Update Data from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

        public byte[] RolesGet(string roleName, string userId, string userPassword, string bookGroup, string functionPath)
        {   //DC 20110121 

            string moduleName = "UserAdminService.RolesGet";  //DC
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = UserAdmin.RolesGet(roleName);      //DC 
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public byte[] UserGet(string userIdGet, string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "UserAdminService.UserGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = UserAdmin.UserGet(userIdGet);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public byte[] RoleFunctionsGet(string roleName, string functionPathGet, short utcOffSet, string userId, string userPassword, string bookGroup, string functionPath)
        {   //DC  20110126 
            string moduleName = "UserAdminService.RoleFunctionsGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = UserAdmin.RoleFunctionsGet(roleName, functionPathGet, utcOffSet);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public byte[] RoleFunctionsBookGroupGet(string userIdGet, string bookGroupGet, string bookGroup, string functionPath, string userId,
                    string userPassword)
        {    
            string moduleName = "UserAdminService.RoleFunctionsBookGroupGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = UserAdmin.RoleFunctionsBookGroupGet(userIdGet, bookGroupGet, 0);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public byte[] UserRolesGet(string userIdGet, string roleName, short utcOffset, string userId, string userPassword, string bookGroup, 
                string functionPath)
        {
            string moduleName = "UserAdminService.UserRolesGet";
            DataSet dsTemp = new DataSet();
            try
            {
                if (StockLoan.Business.Security.mayView(userId, userPassword, bookGroup, functionPath))
                {
                    dsTemp = UserAdmin.UserRolesGet(userIdGet, roleName, utcOffset);
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " View output from " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to View output from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }

        public bool UserRolesSet(string userIdSet, string roleName, string comment, bool delete, string userId, string userPassword, string bookGroup, string functionPath)
        {//DC 20110125 
            string moduleName = "UserAdminService.UserRolesSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    UserAdmin.UserRolesSet(userIdSet, roleName, comment, userId, delete);
                    blnResults = true;
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " Set values for " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to Update Data from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

        public bool UserSet(string userIdSet, string shortName, string newPassword, string email, string title, string comment, bool isLocked, 
            bool isActive, bool isLoggedIn, string userId, string userPassword, string bookGroup, string functionPath)
        {
            string moduleName = "UserAdminService.UserSet";
            bool blnResults = false;
            try
            {
                if (StockLoan.Business.Security.mayEdit(userId, userPassword, bookGroup, functionPath))    
                {
                    UserAdmin.UserSet(userIdSet, userIdSet, newPassword, email, title, comment, userId, isLocked, isActive, isLoggedIn);
                    blnResults = true;
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " Set values for " + moduleName + " from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);
                }
                else
                {
                    string sourceAddr = GetSourceIP();
                    string eventMsg = "User " + userId + " attempt to Update Data from " + moduleName + " failed from IP: " + sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                }
                return blnResults;
            }
            catch
            {
                throw;
            }
        }

        public byte[] UserBookGroupsGet(string userId, string functionPath)
        {
            string moduleName = "UserAdminService.UserBookGroupsGet";
            
            DataSet dsTemp = new DataSet();

            try
            {
                dsTemp = UserAdmin.UserBookGroupsGet(userId, functionPath);

                string sourceAddr = GetSourceIP();
                string eventMsg = "User " + userId + " Set values for " + moduleName + " from IP: " + sourceAddr;
                Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);

                return ToolFunctions.ToolFunctions.ConvertDataSet(dsTemp);
            }
            catch
            {
                throw;
            }
        }
    }
}
