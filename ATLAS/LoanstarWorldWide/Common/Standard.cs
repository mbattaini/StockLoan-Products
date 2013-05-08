using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

namespace StockLoan.Common
{
    public class Standard
    {
        public const string Developer = "StockLoan";

        public const string DateFormat = "yyyy-MM-dd";
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        public const string DateTimeFileFormat = "yyyy-MM-dd HH:mm:ss";
        public const string DateTimeShortFormat = "yyyy-MM-dd HH:mm";

        public const string TimeFormat = "HH:mm:ss.fff";
        public const string TimeFileFormat = "HH:mm:ss";
        public const string TimeShortFormat = "HH:mm";

        /// <summary>
        /// Returns a value for key from the .config file.
        /// </summary>
        public static string ConfigValue(string key)
        {
            return ConfigValue(key, "");
        }

        /// <summary>
        /// Returns a value for key from the .config file or returns the default value if key does not exist.
        /// </summary>
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
            catch (Exception e)
            {
                Log.Write(e.Message + " [Standard.ConfigValue]", Log.Error, 1);
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns true if key exists in .config file.
        /// </summary>
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
            catch (Exception e)
            {
                Log.Write(e.Message + " [Standard.ConfigValueExists]", Log.Error, 1);
                return false;
            }
        }

        /// <summary>
        /// Returns a unique process ID with a prefix.
        /// </summary>
        public static string ProcessId(string prefix)
        {
            lock (typeof(Standard)) // Defend against duplicate process ID.
            {
                if (prefix.Length > 16)
                {
                    prefix = prefix.Substring(0, 16);
                }

                Thread.Sleep(25);
                return prefix + DateTime.UtcNow.ToString("yyyyMMddHHmmssff").Substring(prefix.Length, 16 - prefix.Length);
            }
        }

        /// <summary>
        /// Returns a unique process ID.
        /// </summary>
        public static string ProcessId()
        {
            return ProcessId("");
        }

    }
}
