using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using StockLoan.DataAccess;
using System.Net.Mail;

namespace StockLoan.Business
{
    public class Functions
    {

        public static DateTime ConvertToDate(string value)
        {
            try
            {
                DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
                dtfi.ShortDatePattern = "dd-MM-yyyy";
                dtfi.DateSeparator = "/";
                DateTime objDate = Convert.ToDateTime(value, dtfi);
                return objDate;
            }
            catch
            {
                throw;
            }
        }

        public static Decimal ConvertToDecimal(string value)
        {
            decimal number;
         
            try
            {
                number = Decimal.Parse(value);
                return number;
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public static Int32 ConvertToInt32(string value)
        {
            Int32 number;
            
            try
            {
                number = Int32.Parse(value);
                return number;
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public static int ConvertToInt(string value)
        {
            int number;
            
            try
            {
                number = int.Parse(value);
                return number;
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public static BalanceTypes BalanceTypeCheck(InformationType infoType, string contractType, decimal amount)
        {
            switch (infoType)
            {
                case InformationType.Contracts:
                    if (contractType.Equals("B"))
                    {
                        return BalanceTypes.Debit;
                    }
                    else
                    {
                        return BalanceTypes.Credit;
                    }

                case InformationType.Marks:
                    if (amount >= 0)
                    {
                        return BalanceTypes.Debit;
                    }
                    else
                    {
                        return BalanceTypes.Credit;
                    }

                case InformationType.Returns:
                    if (contractType.Equals("B"))
                    {
                        return BalanceTypes.Debit;
                    }
                    else
                    {
                        return BalanceTypes.Credit;
                    }
            
                default:
                    return BalanceTypes.Credit;
            }
        }

        public static int CompareDates(string date1, string date2)
        {            
            DateTime dateTime1;
            DateTime dateTime2;
            
            int result = -99;

            try
            {
                dateTime1 = DateTime.Parse(date1);
                dateTime2 = DateTime.Parse(date2);

                if (dateTime1 == dateTime2)
                {
                    result = 0;
                }
                else if (dateTime1 > dateTime2)
                {
                    result = 1;
                }
                else if (dateTime1 < dateTime2)
                {
                    result = -1;
                }
            }
            catch
            {
                result = -99;
            }

            return result;
        }

        private static DataSet RemoveDuplications(DataSet dsTemp, string roleIdToCompare, string functionIdToCompare)
        {
            string roleId = "";
            string functionId = "";
            bool isRowFound = false;

            foreach (DataRow dr in dsTemp.Tables[0].Rows)
            {
                roleId = dr["RoleId"].ToString().ToLower();
                functionId = dr["FunctionId"].ToString().ToLower();

                if ((roleId.Equals(roleIdToCompare)) && (functionId.Equals(functionIdToCompare)))
                {
                    if (!isRowFound)
                    {                                                
                        isRowFound = true;
                    }
                    else
                    {
                        dr.Delete();
                        isRowFound = true;
                    }
                }
            }

            dsTemp.AcceptChanges();

            return dsTemp;
        }

        //BS; Updates for RoleFunctionsBookGroupGet
        public static DataSet ExtractDistinctActiveRoles(DataSet dsTemp)
        {
            bool userIsActive = false;

            try
            {

                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    userIsActive = bool.Parse(dr["UserIsActive"].ToString());
                    if (!userIsActive)
                    {
                        dr.Delete();
                    }
                }

                dsTemp.AcceptChanges();

                DataSet dsTarget = new DataSet();
                dsTarget = dsTemp.Copy();

                string roleIdToCompare = "";
                string functionIdToCompare = "";

                foreach (DataRow dr2 in dsTemp.Tables[0].Rows)
                {
                    roleIdToCompare = dr2["RoleId"].ToString().ToLower();
                    functionIdToCompare = dr2["FunctionId"].ToString().ToLower();

                    dsTarget = RemoveDuplications(dsTarget, roleIdToCompare, functionIdToCompare);

                }
         
                return dsTarget;
            }
            catch
            {
                throw;
            }
        }

        public static DataSet DataSetScrub(DataSet dsTarget, string tableName, string columnName, string value)
        {
            try
            {                
                foreach (DataRow dr in dsTarget.Tables[tableName].Rows)
                {
                    if (!dr[columnName].ToString().Equals(value))
                    {
                        dr.Delete();
                    }
                }
                dsTarget.AcceptChanges();
             
                return dsTarget;
            }
            catch 
            {
                throw;
            }
        }

        public static void DataSetScrub(ref DataSet dsTarget, string tableName, string columnName)
        {
            try
            {
                foreach (DataRow dr in dsTarget.Tables[tableName].Rows)
                {
                    if (!bool.Parse(dr[columnName].ToString()))
                    {
                        dr.Delete();
                    }
                }
                
                dsTarget.AcceptChanges();
            }
            catch 
            {
                throw;
            }
        }

        public static void HolidaySet(
            string bookGroup, 
            string holidayDate, 
            string countryCode, 
            string description, 
            bool isBankHoliday,
            bool isExchangeHoliday, 
            string actUserId, 
            bool isActive)

        {
            try
            {
                if (bookGroup.Trim().Equals(""))
                {
                    throw new Exception("Book Group is required");
                }
                
                if (holidayDate.Trim().Equals(""))
                {
                    throw new Exception("Holiday Date is required");
                }
                
                if(countryCode.Trim().Equals(""))
                {
                    throw new Exception("Country Code is required");
                }
                
                if(actUserId.Trim().Equals("")) 
                {
                    throw new Exception("User Id is required");
                }
                    
                DBStandardFunctions.HolidaySet(
                    bookGroup, 
                    holidayDate, 
                    countryCode, 
                    description, 
                    isBankHoliday, 
                    isExchangeHoliday,
                    actUserId, 
                    isActive);
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet HolidaysGet(string bookGroup, string countryCode, string description, short utcOffset)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = DBStandardFunctions.HolidaysGet(bookGroup, countryCode, description, utcOffset);
                 return dsTemp;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet HolidaysGetList(string bookGroup, string compareDate, string description, short utcOffset)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = DBStandardFunctions.HolidaysGet(bookGroup, "", "", utcOffset);

                DateTime date1;  
                DateTime dateToCompare;
                date1 = DateTime.Parse(compareDate);
                compareDate = date1.ToString("yyyy-MM-dd");
               
                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    dateToCompare = DateTime.Parse(dr["HolidayDate"].ToString());
                    string date2 = dateToCompare.ToString("yyyy-MM-dd");
                
                    if (!compareDate.Equals(date2))
                    {
                        dr.Delete();
                    }
                }
                dsTemp.AcceptChanges();

                return dsTemp;
            }
            catch
            {
                throw;
            }
        }

        public static bool IsBankHoliday(string bookGroup, string holidayDate, short utcOffset)
        {
            try
            {
                bool isBank = false;
                bool isExchange = false;
                string countryCode = "";
                string description = "";
                
                DBStandardFunctions.HolidaysGet(bookGroup, holidayDate, countryCode, description, ref isBank, ref isExchange, utcOffset);
                
                return isBank;
            }
            catch
            {
                throw;
            }
        }

        public static bool IsExchangeHoliday(string bookGroup, string holidayDate, short utcOffset)
        {
            try
            {
                bool isBank = false;
                bool isExchange = false;
                string countryCode = "";
                string description = "";
                
                DBStandardFunctions.HolidaysGet(bookGroup, holidayDate, countryCode, description, ref isBank, ref isExchange, utcOffset);
                
                return isExchange;
            }
            catch
            {
                throw;
            }
        }

        public static string KeyValuesGet(string keyId)
        {
   
            string strReturn = "";

            try
            {
                strReturn = DBStandardFunctions.KeyValuesGet(keyId, strReturn);
            }
            catch 
            {
                throw;
            }
            return strReturn;
        }

        public static DataSet KeyValuesGet()
        {

            DataSet dataSet = new DataSet();

            try
            {
                dataSet = DBStandardFunctions.KeyValuesGet();
                return dataSet;
            }
            catch 
            {
                throw;
            }
        }

        public static void KeyValueSet(string keyId, string keyValue)
        {
            try
            {
                DBStandardFunctions.KeyValueSet(keyId, keyValue);
                
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet LogicOperatorsGet()
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = DBStandardFunctions.LogicOperatorsGet();
             
                return dsTemp;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet TimeZonesGet(string timeZoneId, string timeZoneName)
        {
            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = DBStandardFunctions.TimeZonesGet(timeZoneId, timeZoneName);
                
                return dsTemp;
            }
            catch
            {
                throw;
            }
        }

        public static void TimeZoneSet(
            string timeZoneName, 
            string displayName, 
            string utcOffsetBase, 
            string utcOffsetActive,
            bool supportsDaylightSavingtime, 
            bool isDaylightSavingTime, 
            string actUserId)
        {
            try
            {
                if (timeZoneName.Equals(""))
                {
                    throw new Exception("Time zone name is required");
                }
             
                if (actUserId.Equals(""))
                {
                    throw new Exception("User ID is required");
                }

                DBStandardFunctions.TimeZoneSet(
                    timeZoneName, 
                    displayName, 
                    utcOffsetBase, 
                    utcOffsetActive, 
                    supportsDaylightSavingtime, 
                    isDaylightSavingTime, 
                    actUserId);

            }
            catch 
            {
                throw;
            }
        }

        public static void SettlementSystsemProcessSet(string bizDate)
        {
            if (bizDate.ToString().Equals(""))
            {
                throw new Exception("Biz Date is required");
            }
            else
            {
                try
                {
                    DBStandardFunctions.SettlementSystemProcessSet(bizDate);
                }
                catch 
                {
                    throw;
                }
            }
        }

    }
}
