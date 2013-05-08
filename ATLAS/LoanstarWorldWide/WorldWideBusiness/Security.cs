using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Data;

using StockLoan.DataAccess;
using StockLoan.Encryption;
using StockLoan.Transport;

using StockLoan.Common;

namespace StockLoan.Business
{

    public class Security
    {
        private struct UserProfile
        {
            public string userId;
            public bool userIsValid;
            public bool userMayView;
            public bool userMayEdit;
        }
    
        private const int DefaultMinimum = 8;
        private const int DefaultMaximum = 12;
        private const int UBoundDigit = 61;

        private bool hasRepeating;
        private bool hasConsecutive;
        private bool hasSymbols;

        private const bool useConsecutiveChars = false;
        private const bool useRepeatChars = false;
        private const bool excludeSymbols = true;
        private const string exclusionList = null;

        public static bool mayView(string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                UserProfile userProfile = SecurityProfileGet(userId, userPassword, bookGroup, functionPath);
                if (userProfile.userIsValid)
                {
                    if (userProfile.userMayView)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    string eventMsg = "Invalid user " + userId + " is attempting actions from IP: "; // +sourceAddr;
                    Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }

        public static bool mayEdit(string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                UserProfile userProfile = SecurityProfileGet(userId, userPassword, bookGroup, functionPath);
                if (userProfile.userIsValid)
                {
                    if (userProfile.userMayEdit)
                    {
                        return true;
                    }
                    else
                    {
                        //string sourceAddr = GetUserSourceIP();
                        string eventMsg = "Invalid user " + userId + " is attempting actions from IP: "; // +sourceAddr;
                        Log.Write(eventMsg, System.Diagnostics.EventLogEntryType.Error);
                        return false;
                    }
                }
                else
                {
                    //This is where we'll log the invalid user login attempt and email stock loan support
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }
                 
        private static UserProfile SecurityProfileGet(string userId, string userPassword, string bookGroup, string functionPath)
        {
            {
                try
                {
                    DataSet dsProfile = new DataSet();

                    dsProfile = DBSecurity.SecurityProfileGet(userId, userPassword, bookGroup, functionPath);
                    dsProfile.Tables[0].TableName = "Profile";

                    return UserDataSetParse(dsProfile.Tables["Profile"]);
                }
                catch
                {
                    throw;
                }
            }
        }

        private static UserProfile UserDataSetParse(DataTable dtUser)
        {
            UserProfile userProfile = new UserProfile();
            try
            {
                userProfile.userId = dtUser.Rows[0]["UserId"].ToString();
                userProfile.userIsValid = bool.Parse(dtUser.Rows[0]["IsValid"].ToString());
                userProfile.userMayView = bool.Parse(dtUser.Rows[0]["MayView"].ToString());
                userProfile.userMayEdit = bool.Parse(dtUser.Rows[0]["MayEdit"].ToString());
            }
            catch
            {
                userProfile.userId = "";
                userProfile.userIsValid = false;
                userProfile.userMayView = false;
                userProfile.userMayEdit = false;
            }

            return userProfile;
        }
 
        public static int UserPasswordResetCheck(string userId, string ePassword, string sourceAddress, int resetReq)
        {
            /*try
            {
               DataSet dsUserInfo = new DataSet();
                dsUserInfo = DBSecurity.UserValidate(userId, ePassword, sourceAddress);
                resetReq = 9;
                
                if (dsUserInfo.Tables["Users"].Rows.Count > 0)
                {
                    if (dsUserInfo.Tables["Users"].Rows[0]["UserId"].ToString().ToUpper().Equals(userId.ToUpper()))
                    {
                        if (dsUserInfo.Tables["Users"].Rows[0]["IsPasswordChangeRequired"].ToString().Equals("True"))
                        {
                            resetReq = 1;
                        }
                        else
                        { resetReq = 0; }
                    }
                    else
                    {
                        resetReq = 9;
                    }

                }
                else
                { 
                    resetReq = 9; 
                }
                return resetReq;
            }
            catch 
            {
                throw;
            }*/

            return 0;
        }

        public static bool UserValidate(string userId, string ePassword, bool pwdEncrypted)
        {
            DataSet dsUsers = new DataSet();

            bool    isValid = false;

            string localPasswordDecrypted = EncryptDecrypt.Decrypt(ePassword);
            string serverPasswordDecrypted = "";            

            try
            {
                dsUsers =  DBSecurity.UserValidate(userId);

                foreach (DataRow drUser in dsUsers.Tables["Users"].Rows)
                {
                    if (drUser["UserId"].ToString().ToUpper().Equals(userId.ToUpper()))
                    {
                        serverPasswordDecrypted = EncryptDecrypt.Decrypt(drUser["Password"].ToString());
                    }
                }

                if (localPasswordDecrypted.Equals(serverPasswordDecrypted))
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
            }
            catch
            {
                throw;
            }

            return isValid;
        }

        public static bool UserPasswordChange(string userId, string oldEPassword, string newEPassword)
        {
            try
            {
                bool pwdChangeReq = false;

                pwdChangeReq = DBSecurity.UserPasswordChange(userId, oldEPassword, newEPassword);

                return pwdChangeReq;
            }
            catch 
            {
                throw;
            }
        }

        public static bool UserPasswordReset(string userId, string newEPassword)
        {
            try
            {
                bool valid = false;
                DataSet dsResetInfo = new DataSet();

                string rndPassword = GenerateRandomPwd();
                string userEPassword = EncryptDecrypt.Encrypt(rndPassword);

                dsResetInfo = DBSecurity.UserPasswordReset(userId, userEPassword);

                if (dsResetInfo.Tables["Users"].Rows.Count > 0)
                {
                    if (dsResetInfo.Tables["Users"].Rows[0]["UserId"].ToString().ToUpper().Equals(userId.ToUpper()))
                    {
                        if (!dsResetInfo.Tables["Users"].Rows[0]["Email"].ToString().Trim().Equals(""))
                        {
                            string emailTitle = DBStandardFunctions.KeyValuesGet("WorldWidePwdChangeTitle", "LoanStar Password Change Notification");

                            string emailBody = "";
                            emailBody += DBStandardFunctions.KeyValuesGet("WorldWidePwdChangeBody1", "LoanStar Password has been reset") + "\r\n \r\n";
                            emailBody += DBStandardFunctions.KeyValuesGet("WorldWidePwdChangeBody2", "If you did not, Please Contact") + "\r\n"; 
                            emailBody += DBStandardFunctions.KeyValuesGet("WorldWidePwdChangeContact", "StockLoan Support");
                            emailBody += DBStandardFunctions.KeyValuesGet("WorldWidePwdChangeBody3", " to report this") + "\r\n \r\n";
                            emailBody += DBStandardFunctions.KeyValuesGet("WorldWidePwdChangeBody4", "Password reset to") + rndPassword + "\r\n \r\n";
                            emailBody += DBStandardFunctions.KeyValuesGet("WorldWidePwdChangeBody5", "Please use this for your next login") + "\r\n \r\n";

                            //Send email notification of password change
                            string emailTo = dsResetInfo.Tables["Users"].Rows[0]["Email"].ToString().Trim();
                            string emailFrom = "bstone@penson.com";  // Clear for testing ONLY DBStandardFunctions.KeyValuesGet("StockLoanMailFrom", "support.stockloan@penson.com");
                            string emailSubject = emailTitle;
                            string emailContent = emailBody + "** " + System.Environment.MachineName + " **";

                            //string emailContent = "Your LoanStar Password has been reset.     " + "\r\n \r\n" +
                            //    "If you did not request this, please contact the local help desk to report this."
                            //    + "\r\n \r\n" +
                            //    "Your password was reset to   '" + rndPassword + "'" + " \r\n" +
                            //    "Use this password for your next login. You will be required to change it at that time"
                            //    + "\r\n \r\n \r\n \r\n \r\n** " + System.Environment.MachineName + " **";

                            Transport.Email.Send(emailFrom, emailTo, emailSubject, emailContent, DBStandardFunctions.DbCnStr);

                            valid = true;
                        }
                    }
                }
                return valid;
            }
            catch 
            {
                throw;
            }
        }

        protected static string GenerateRandomPwd()
        {

            string exclusionList = null;
            // Pick random length between minimum and maximum   
            int pwdLength = GetCryptographicRandomNumber(DefaultMinimum, DefaultMaximum);

            StringBuilder pwdBuffer = new StringBuilder();
            pwdBuffer.Capacity = DefaultMaximum;

            // Generate random characters
            char lastCharacter, nextCharacter;

            // Initial dummy character flag
            lastCharacter = nextCharacter = '\n';

            for (int i = 0; i < pwdLength; i++)
            {
                nextCharacter = GetRandomCharacter();

                if (useConsecutiveChars.Equals(false))
                {
                    while (lastCharacter.Equals(nextCharacter))
                    {
                        nextCharacter = GetRandomCharacter();
                    }
                }

                if (useRepeatChars.Equals(false))
                {
                    string temp = pwdBuffer.ToString();
                    int duplicateIndex = temp.IndexOf(nextCharacter);
                    while (-1 != duplicateIndex)
                    {
                        nextCharacter = GetRandomCharacter();
                        duplicateIndex = temp.IndexOf(nextCharacter);
                    }
                }

                if (!(exclusionList == null))
                {
                    while (-1 != exclusionList.IndexOf(nextCharacter))
                    {
                        nextCharacter = GetRandomCharacter();
                    }
                }

                pwdBuffer.Append(nextCharacter);
                lastCharacter = nextCharacter;
            }

            if (null != pwdBuffer)
            {
                return pwdBuffer.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        protected static int GetCryptographicRandomNumber(int lBound, int uBound)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            uint uRndNum;
            byte[] rndnum = new Byte[4];
            if (lBound.Equals(uBound - 1))
            {
                return lBound;
            }

            uint xcludeRndBase = (uint.MaxValue - (uint.MaxValue % (uint)(uBound - lBound)));
            do
            {
                rng.GetBytes(rndnum);
                uRndNum = System.BitConverter.ToUInt32(rndnum, 0);
            } while (uRndNum >= xcludeRndBase);

            return (int)(uRndNum % (uBound - lBound)) + lBound;
        }

        protected static char GetRandomCharacter()
        {
            char[] pwdCharArray = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?".ToCharArray();

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng = new RNGCryptoServiceProvider();

            int upperBound = pwdCharArray.GetUpperBound(0);

            if (excludeSymbols.Equals(true))
            {
                upperBound = UBoundDigit;
            }

            int randomCharPosition = GetCryptographicRandomNumber(pwdCharArray.GetLowerBound(0), upperBound);

            char randomChar = pwdCharArray[randomCharPosition];

            return randomChar;
        }
         
        protected bool ExcludeSymbols
        {
            get { return this.hasSymbols; }
            set { this.hasSymbols = value; }
        }

        protected bool RepeatCharacters
        {
            get { return this.hasRepeating; }
            set { this.hasRepeating = value; }
        }

        protected bool ConsecutiveCharacters
        {
            get { return this.hasConsecutive; }
            set { this.hasConsecutive = value; }
        }

        private void SendMailToSupport(string eventText, string eventType)
        {
                //Send email notice to support of violations
            string emailTo = DBStandardFunctions.KeyValuesGet("StockLoanSupportMailTo", "support.stockloan@penson.com").ToString().Trim();
            string emailFrom = DBStandardFunctions.KeyValuesGet("StockLoanSystemMailFrom", "bstone@penson.com");
            string emailSubject = "StockLoanWorldWide system Service " + eventType;
            string emailContent = "There has been a problem encountered in StockLoanWorldwide - described as: " + "\r\n \r\n" +
                eventText + "\r\n \r\n \r\n \r\n ** " + System.Environment.MachineName + " **";
        }

    }
}
