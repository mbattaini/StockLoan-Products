using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBStandardFunctions
    {
        public enum HolidayType
        {
            Any,
            Bank,
            Exchange
        }

        public static string DbCnStr
        {
            get
            {
                return ("Trusted_Connection=yes; " +
                 "Data Source=" + DBStandardFunctions.ConfigValue("MainDatabaseHost", "") + "; " +
                 "Initial Catalog=" + DBStandardFunctions.ConfigValue("MainDatabaseName", "") + ";");
            }
        }

        private static string dbCnStr = DBStandardFunctions.DbCnStr;

        public static string ConfigValue(string key, string defaultValue)
        {
            try
            {
                string s = ConfigurationManager.AppSettings[key];
                if (s == null)
                {
                    return defaultValue;
                }
                else
                {
                    return s;
                }
            }
            catch
            {
                throw;
            }
        }

        public static void HolidaysGet(
            string bookGroup,
            string holidayDate,
            string countryCode,
            string description,
            ref bool isBank,
            ref bool isExchange,
            short utcOffset)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spHolidayGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }
                if (!holidayDate.Equals(""))
                {
                    SqlParameter paramHolidayDate = dbCmd.Parameters.Add("@HolidayDate", SqlDbType.DateTime);
                    paramHolidayDate.Value = holidayDate;
                }
                if (!countryCode.Equals(""))
                {
                    SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
                    paramCountryCode.Value = countryCode;
                }
                if (!description.Equals(""))
                {
                    SqlParameter paramDescription = dbCmd.Parameters.Add("@Description", SqlDbType.VarChar, 250);
                    paramDescription.Value = description;
                }

                SqlParameter paramIsBank = dbCmd.Parameters.Add("@IsBank", SqlDbType.Bit);
                paramIsBank.Direction = ParameterDirection.Output;

                SqlParameter paramIsExchange = dbCmd.Parameters.Add("@IsExchange", SqlDbType.Bit);
                paramIsExchange.Direction = ParameterDirection.Output;

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                DataSet dsHolidays = new DataSet();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsHolidays, "Holidays");

                isBank = bool.Parse(paramIsBank.Value.ToString());
                isExchange = bool.Parse(paramIsExchange.Value.ToString());
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

        public static DataSet HolidaysGet(string bookGroup, string countryCode, string description, short utcOffset)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            try
            {
                DataSet dsHolidays = new DataSet();
                SqlCommand dbCmd = new SqlCommand("dbo.spHolidayGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                if (!countryCode.Equals(""))
                {
                    SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
                    paramCountryCode.Value = countryCode;
                }

                if (!description.Equals(""))
                {
                    SqlParameter paramDescription = dbCmd.Parameters.Add("@Description", SqlDbType.VarChar, 250);
                    paramDescription.Value = description;
                }

                SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
                paramUtcOffset.Value = utcOffset;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsHolidays, "Holidays");

                return dsHolidays;
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
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spHolidaySet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlParameter paramHolidayDate = dbCmd.Parameters.Add("@HolidayDate", SqlDbType.DateTime);
                paramHolidayDate.Value = holidayDate;

                SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
                paramCountryCode.Value = countryCode;

                if (!description.Equals(""))
                {
                    SqlParameter paramDescription = dbCmd.Parameters.Add("@Description", SqlDbType.VarChar, 250);
                    paramDescription.Value = description;
                }

                SqlParameter paramIsBankHoliday = dbCmd.Parameters.Add("@IsBankHoliday", SqlDbType.Bit);
                paramIsBankHoliday.Value = isBankHoliday;

                SqlParameter paramIsExchangeHoliday = dbCmd.Parameters.Add("@IsExchangeHoliday", SqlDbType.Bit);
                paramIsExchangeHoliday.Value = isExchangeHoliday;

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

                if (!isActive.Equals(""))
                {
                    SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                    paramIsActive.Value = isActive;
                }

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

        public static string KeyValuesGet(string keyId, string keyValue)
        {
            try
            {
                keyValue = KeyValue.Get(keyId, keyValue, dbCnStr);
            }
            catch
            {
                throw;
            }

            return keyValue;
        }

        public static DataSet KeyValuesGet()
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsKeyValues = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spKeyValueGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsKeyValues, "KeyValues");
            }
            catch
            {
                throw;
            }

            return dsKeyValues;
        }

        public static void KeyValueSet(string keyId, string keyValue)
        {
            if (keyId.Trim().Equals(""))
            {
                throw new Exception("Key ID is required");
            }

            try
            {
                KeyValue.Set(keyId, keyValue, dbCnStr);
            }
            catch
            {
                throw;
            }
        }

        public static DataSet LogicOperatorsGet()
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsDataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spLogicOperatorsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsDataSet, "LogicOperators");
            }
            catch
            {
                throw;
            }

            return dsDataSet;
        }

        public static void SettlementSystemProcessSet(string bizDate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spSettlementSystemProcess", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

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

        public static DataSet TimeZonesGet(string timeZoneId, string timeZoneName)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsDataSet = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spTimeZonesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!timeZoneId.Equals(""))
                {
                    SqlParameter paramTimeZoneId = dbCmd.Parameters.Add("@TimeZoneId", SqlDbType.Int);
                    paramTimeZoneId.Value = int.Parse(timeZoneId);
                }

                if (!timeZoneName.Equals(""))
                {
                    SqlParameter paramTimeZoneName = dbCmd.Parameters.Add("@TimeZoneName", SqlDbType.VarChar, 100);
                    paramTimeZoneName.Value = timeZoneName;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsDataSet, "TimeZones");
            }
            catch
            {
                throw;
            }

            return dsDataSet;
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
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spTimeZoneSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramTimeZoneName = dbCmd.Parameters.Add("@TimeZoneName", SqlDbType.VarChar, 100);
                paramTimeZoneName.Value = timeZoneName;

                if (!displayName.Equals(""))
                {
                    SqlParameter paramDisplayName = dbCmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 100);
                    paramDisplayName.Value = displayName;
                }

                if (!utcOffsetBase.Equals(""))
                {
                    SqlParameter paramUtcOffsetBase = dbCmd.Parameters.Add("@UtcOffsetBase", SqlDbType.Decimal);
                    paramUtcOffsetBase.Value = utcOffsetBase;
                }

                if (!utcOffsetActive.Equals(""))
                {
                    SqlParameter paramUtcOffsetActive = dbCmd.Parameters.Add("@UtcOffsetActive", SqlDbType.Decimal);
                    paramUtcOffsetActive.Value = utcOffsetActive;
                }

                SqlParameter paramSupportsDaylightSavingTime = dbCmd.Parameters.Add("@SupportsDaylightSavingTime", SqlDbType.Bit);
                paramSupportsDaylightSavingTime.Value = supportsDaylightSavingtime;

                SqlParameter paramIsDaylightSavingTime = dbCmd.Parameters.Add("@IsDaylightSavingTime", SqlDbType.Bit);
                paramIsDaylightSavingTime.Value = isDaylightSavingTime;

                SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
                paramActUserId.Value = actUserId;

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

        public static void BookGroupSet(
            string bookGroup,
            string bookName,
            string timeZoneId,
            string bizDate,
            string bizDateContract,
            string bizDateBank,
            string bizDateExchange,
            string bizDatePrior,
            string bizDatePriorBank,
            string bizDatePriorExchange,
            string bizDateNext,
            string bizDateNextBank,
            string bizDateNextExchange,
            bool? useWeekends,
            string settlementType)
        {

            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBookGroupSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                if (!bookName.Equals(""))
                {
                    SqlParameter paramBookName = dbCmd.Parameters.Add("@BookName", SqlDbType.VarChar, 50);
                    paramBookName.Value = bookName;
                }
                if (!timeZoneId.Equals(""))
                {
                    SqlParameter paramTimeZoneId = dbCmd.Parameters.Add("@TimeZoneId", SqlDbType.Int);
                    paramTimeZoneId.Value = timeZoneId;
                }
                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = bizDate;
                }
                if (!bizDateContract.Equals(""))
                {
                    SqlParameter paramBizDateContract = dbCmd.Parameters.Add("@BizDateContract", SqlDbType.DateTime);
                    paramBizDateContract.Value = bizDateContract;
                }
                if (!bizDateBank.Equals(""))
                {
                    SqlParameter paramBizDateBank = dbCmd.Parameters.Add("@BizDateBank", SqlDbType.DateTime);
                    paramBizDateBank.Value = bizDateBank;
                }
                if (!bizDateExchange.Equals(""))
                {
                    SqlParameter paramBizDateExchange = dbCmd.Parameters.Add("@BizDateExchange", SqlDbType.DateTime);
                    paramBizDateExchange.Value = bizDateExchange;
                }
                if (!bizDatePrior.Equals(""))
                {
                    SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                    paramBizDatePrior.Value = bizDatePrior;
                }
                if (!bizDatePriorBank.Equals(""))
                {
                    SqlParameter paramBizDatePriorBank = dbCmd.Parameters.Add("@BizDatePriorBank", SqlDbType.DateTime);
                    paramBizDatePriorBank.Value = bizDatePriorBank;
                }
                if (!bizDatePriorExchange.Equals(""))
                {
                    SqlParameter paramBizDatePriorExchange = dbCmd.Parameters.Add("@BizDatePriorExchange", SqlDbType.DateTime);
                    paramBizDatePriorExchange.Value = bizDatePriorExchange;
                }
                if (!bizDateNext.Equals(""))
                {
                    SqlParameter paramBizDateNext = dbCmd.Parameters.Add("@BizDateNext", SqlDbType.DateTime);
                    paramBizDateNext.Value = bizDateNext;
                }
                if (!bizDateNextBank.Equals(""))
                {
                    SqlParameter paramBizDateNextBank = dbCmd.Parameters.Add("@BizDateNextBank", SqlDbType.DateTime);
                    paramBizDateNextBank.Value = bizDateNextBank;
                }
                if (!bizDateNextExchange.Equals(""))
                {
                    SqlParameter paramBizDateNextExchange = dbCmd.Parameters.Add("@BizDateNextExchange", SqlDbType.DateTime);
                    paramBizDateNextExchange.Value = bizDateNextExchange;
                }

                
                if (useWeekends != null)
                {
                SqlParameter paramUseWeekends = dbCmd.Parameters.Add("@UseWeekends", SqlDbType.Bit);
                paramUseWeekends.Value = useWeekends;
                }

                if (!settlementType.Equals(""))
                {
                    SqlParameter paramSettlementType = dbCmd.Parameters.Add("@SettlementType", SqlDbType.VarChar, 10);
                    paramSettlementType.Value = settlementType;
                }

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

        public static DateTime BizDateGet(string bookGroup, string bizDateType)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DateTime bizDateValue;

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBizDateGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                if (!bizDateType.Equals(""))
                {
                    SqlParameter paramBizDateType = dbCmd.Parameters.Add("@BizDateType", SqlDbType.VarChar, 20);
                    paramBizDateType.Value = bizDateType;
                }

                SqlParameter paramBizDateValue = dbCmd.Parameters.Add("@BizDateValue", SqlDbType.DateTime);
                paramBizDateValue.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();

                bizDateValue = DateTime.Parse(paramBizDateValue.Value.ToString());
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

            return bizDateValue;
        }


        public static string BizDateStrGet(string bookGroup, string bizDateType)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            string bizDateValue;

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spBizDateGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                if (!bizDateType.Equals(""))
                {
                    SqlParameter paramBizDateType = dbCmd.Parameters.Add("@BizDateType", SqlDbType.VarChar, 20);
                    paramBizDateType.Value = bizDateType;
                }

                SqlParameter paramBizDateValue = dbCmd.Parameters.Add("@BizDateValue", SqlDbType.DateTime);
                paramBizDateValue.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();

               
                bizDateValue = paramBizDateValue.Value.ToString();

                if (!bizDateValue.Equals(""))
                {
                    bizDateValue = DateTime.Parse(paramBizDateValue.Value.ToString()).ToString("yyyy-MM-dd");
                }
            }
            catch
            {
                bizDateValue = "";
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }

            return bizDateValue;
        }



        public static bool IsBizDate(string bookGroup, DateTime anyDate, string countryCode, HolidayType holidayType, bool useWeekends, string sqlDbCnStr)
        {
            return IsBizDate(bookGroup, anyDate, countryCode, holidayType, useWeekends, new SqlConnection(sqlDbCnStr));
        }

        public static bool IsBizDate(string bookGroup, DateTime anyDate, string countryCode, HolidayType holidayType, bool useWeekends, SqlConnection sqlDbCn)
        {   //dc 

            if (!useWeekends)
            {
                if ((anyDate.DayOfWeek == DayOfWeek.Saturday) || (anyDate.DayOfWeek == DayOfWeek.Sunday))
                {
                    return false;
                }
            }

            try
            {
                bool isBank = false;
                bool isExchange = false;

                DBStandardFunctions.HolidaysGet(bookGroup, anyDate.ToString("yyyy-MM-dd"), countryCode, "", ref isBank, ref isExchange, 0);

                switch (holidayType)
                {
                    case HolidayType.Any:
                        if (isBank.Equals(DBNull.Value) && isExchange.Equals(DBNull.Value))
                        {
                            return true;
                        }
                        else
                        {
                            return !(isBank && isExchange);
                        }
                    case HolidayType.Bank:
                        if (isBank.Equals(DBNull.Value))
                        {
                            return true;
                        }
                        else
                        {
                            return !isBank;
                        }
                    case HolidayType.Exchange:
                        if (isExchange.Equals(DBNull.Value))
                        {
                            return true;
                        }
                        else
                        {
                            return !isExchange;
                        }
                    default:
                        return false;
                }
            }
            catch
            {
                return true;
            }
            finally
            {
                if (sqlDbCn.State != ConnectionState.Closed)
                {
                    sqlDbCn.Close();
                }
            }
        }
    }
}
