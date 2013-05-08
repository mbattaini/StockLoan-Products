using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.Common;
using StockLoan.Business;

namespace StockLoan.WebServices.SecurityService
{
    public partial class SecurityService : ISecurityService
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

        public bool UserPasswordChange(string userId, string oldEPassword, string newEPassword)
        {

            string moduleName = "SecurityService.UserPasswordChange";
            bool blnResult = false;
  
            blnResult = StockLoan.Business.Security.UserPasswordChange(userId, oldEPassword, newEPassword);

            string sourceAddr = GetSourceIP();
            string eventMsg = "User " + userId + " execution of " + moduleName + " successful from IP: " + sourceAddr;
            Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);

            return blnResult;
        }

        public bool UserPasswordReset(string userId, string newEPassword)
        {
            string moduleName = "SecurityService.UserPasswordReset";
            bool blnResult = false;
            
            blnResult = StockLoan.Business.Security.UserPasswordReset(userId, newEPassword);

            string sourceAddr = GetSourceIP();
            string eventMsg = "User " + userId + " execution of " + moduleName + " successful from IP: " + sourceAddr;
            Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);

            return blnResult;
        }
            
        public int UserPasswordResetCheck(string userId, string ePassword, string sourceAddress, int resetReq)
        {
            string moduleName = "SecurityService.UserValidate";
            string sourceAddr = GetSourceIP();
            try
            {
                /*resetReq = StockLoan.Business.Security.UserValidate(userId, ePassword, sourceAddress, resetReq);
                string eventMsg = "User " + userId + " execution of " + moduleName + " successful from IP: " + sourceAddr;
                Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Information);*/

            
            }
            catch 
            {
                resetReq = 9;
            }

            return resetReq;
        }

        public bool UserValidate(string userId, string ePassword, bool pwdEncrypted)
        {
            bool isValid = false;

            try
            {
               isValid = StockLoan.Business.Security.UserValidate(userId,ePassword, pwdEncrypted);           
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                isValid = false;
            }

            return isValid;
        }
    }
}
