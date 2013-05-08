
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace StockLoan.Business
{
    public enum BalanceTypes
    {
        Credit,
        Debit
    }

    public enum BillingType
    {
        Fee,
        Rebate
    }

    public enum InformationType
    {
        None,
        Deals,
        Contracts,
        Recalls,
        Returns,
        Marks
    }

    public enum SettlementType
    {
        Pending,
        Settled,
        SettledToday,
        None,
        All
    }

    public enum ContractType
    {
        Borrows,
        Loans,
        None,
        All
    }

    public enum InformationTimeType
    {
        Asof,
        Current
    }

    public enum UserAllowances
    {
        AddNew,
        Update,
        Delete,
        View,
        All,
        None
    }

    public enum SecurityTypes
    {
        Isin,
        Sedol,
        Cusip,
        Symbol,
        Unknown
    }
	
    public class SystemStandards
    {
        public const string Developer = "StockLoan";

        public const string DateFormat = "yyyy-MM-dd";
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        public const string DateTimeFileFormat = "yyyy-MM-dd HH:mm:ss";
        public const string DateTimeShortFormat = "yyyy-MM-dd HH:mm";

        public const string TimeFormat = "HH:mm:ss.fff";
        public const string TimeFileFormat = "HH:mm:ss";
        public const string TimeShortFormat = "HH:mm";

        public static bool ConfigValueExists(string key)
        {
            try
            {
                string s = ConfigurationManager.AppSettings[key];

                if (s == null)
                {
                    return false;
                }
                else
                {
                    return (s.Length > 0);
                }
            }
            catch 
            {
                throw;
            }
        }

        public enum HolidayType
        {
            Any,
            Bank,
            Exchange
        }

        public static string ConfigValue(string key)
        {
            return ConfigValue(key, "");
        }

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

        public static bool IsBizDate(DateTime anyDate, string countryCode, HolidayType holidayType, string sqlDbCnStr)
        {
            return true; //BS; For initial coding only!!!!
        }

        public static string ProcessId(string prefix)
        {
            lock (typeof(SystemStandards)) // Defend against duplicate process ID.
            {
                if (prefix.Length > 16)
                {
                    prefix = prefix.Substring(0, 16);
                }

                Thread.Sleep(25);
                return prefix + DateTime.UtcNow.ToString("yyyyMMddHHmmssff").Substring(prefix.Length, 16 - prefix.Length);
            }
        }

        public static string ProcessId()
        {
            return ProcessId("");
        }

        /// <summary>
        /// Returns true if date is a business date for country code using a database connection.
        /// </summary>


        public static bool IsBizDate(DateTime anyDate, string countryCode, HolidayType holidayType, bool useWeekends) //SqlConnection sqlDbCn, )
        {

            if (useWeekends)
            {
                if ((anyDate.DayOfWeek == DayOfWeek.Saturday) || (anyDate.DayOfWeek == DayOfWeek.Sunday))
                {
                    return false;
                }
            }

            try
            {
                switch (holidayType)
                {
                    case HolidayType.Any:
                        {
                            return true;
                        }
                    case HolidayType.Bank:
                        {
                            return true;
                        }
                    case HolidayType.Exchange:
                        {
                            return true;
                        }
                    default:
                        return false;
                }
            }
            catch 
            {
                throw;
            }
            finally
            {
          }
        }

        public struct PriceFileMask
        {
            int secIdIndex;
            int secIdAltIndex;
            int countryCodeIndex;
            int currencyIsoIndex;
            int priceIndex;

            public int SecIdIndex
            {
                get
                {
                    return secIdIndex;
                }

                set
                {
                    secIdIndex = value;
                }
            }

            public int SecIdAltIndex
            {
                get
                {
                    return secIdAltIndex;
                }

                set
                {
                    secIdAltIndex = value;
                }
            }

            public int CountryCodeIndex
            {
                get
                {
                    return countryCodeIndex;
                }

                set
                {
                    countryCodeIndex = value;
                }
            }

            public int CurrencyIsoIndex
            {
                get
                {
                    return currencyIsoIndex;
                }

                set
                {
                    currencyIsoIndex = value;
                }
            }

            public int PriceIndex
            {
                get
                {
                    return priceIndex;
                }

                set
                {
                    priceIndex = value;
                }
            }
        }

        public struct ReportItem
        {
            public string reportId;
            public string reportName;
            public bool isBizDateStartParamUsed;
            public bool isBizDateStopParamUsed;
            public bool isBookGroupParamUsed;
            public string report_sp;
        }

        public struct ReportRequestItem
        {
            public string bizDate;
            public string reportId;
            public string recipient;
            public string reportType;
            public string recipientEmail;
            public string bizDateStartParam;
            public string bizDateStopParam;
            public string bookGroupParam;
            public string emailDate;
            public string status;
            public string statusMessage;
        }

        public enum BalanceTypes
        {
            Credit,
            Debit
        }

        public enum BillingType
        {
            Fee,
            Rebate
        }

        public enum InformationType
        {
            None,
            Deals,
            Contracts,
            Recalls,
            Returns,
            Marks
        }

        public enum SettlementType
        {
            Pending,
            Settled,
            SettledToday,
            None,
            All
        }

        public enum ContractType
        {
            Borrows,
            Loans,
            None,
            All
        }

        public enum InformationTimeType
        {
            Asof,
            Current
        }

        public enum UserAllowances
        {
            AddNew,
            Update,
            Delete,
            View,
            All,
            None
        }

        public enum SecurityTypes
        {
            Isin,
            Sedol,
            Cusip,
            Symbol,
            Unknown
        }
    }
}
