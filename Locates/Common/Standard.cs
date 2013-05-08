using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

namespace StockLoan.Common
{
	/// <summary>
	/// Standard values and common static functions.
	/// </summary>
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
		/// Type of holiday business closure.
		/// </summary>
		public enum HolidayType
		{
			Any,
			Bank,
			Exchange
		}

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
		/// Returns true if date is a business date for country code using a database connection string.
		/// </summary>
		public static bool IsBizDate(DateTime anyDate, string countryCode, HolidayType holidayType, string sqlDbCnStr)
		{
			return IsBizDate(anyDate, countryCode, holidayType, new SqlConnection(sqlDbCnStr));
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
		
		public static string ProcessId()
		{
			return ProcessId("");
		}

		/// <summary>
		/// Returns true if date is a business date for country code using a database connection.
		/// </summary>

        public static bool IsBizDate(DateTime anyDate, string countryCode, HolidayType holidayType, SqlConnection sqlDbCn)
        {
            return IsBizDate(anyDate, countryCode, holidayType, sqlDbCn, bool.Parse(KeyValue.Get("BizDateUseWeekends", "false", sqlDbCn)));
        }

        public static bool IsBizDate(DateTime anyDate, string countryCode, HolidayType holidayType, SqlConnection sqlDbCn, bool useWeekends)
		{
            if (useWeekends == null)
            {
                useWeekends = bool.Parse(KeyValue.Get("BizDateUseWeekends", "false", sqlDbCn));
            }

            if (useWeekends)
			{
				if ((anyDate.DayOfWeek == DayOfWeek.Saturday) || (anyDate.DayOfWeek == DayOfWeek.Sunday))
				{
					return false;
				}
			}

			try
			{
				SqlCommand sqlDbCmd = new SqlCommand("spHolidayGet", sqlDbCn);
				sqlDbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramHolidayDate = sqlDbCmd.Parameters.Add("@HolidayDate", SqlDbType.DateTime);
				paramHolidayDate.Value = anyDate;

				SqlParameter paramCountryCode = sqlDbCmd.Parameters.Add("@CountryCode", SqlDbType.Char, 2);
				paramCountryCode.Value = countryCode;

				SqlParameter paramIsAny = sqlDbCmd.Parameters.Add("@IsBizDate", SqlDbType.Bit);
				paramIsAny.Direction = ParameterDirection.Output;

				SqlParameter paramIsBank = sqlDbCmd.Parameters.Add("@IsBank", SqlDbType.Bit);
				paramIsBank.Direction = ParameterDirection.Output;

				SqlParameter paramIsExchange = sqlDbCmd.Parameters.Add("@IsExchange", SqlDbType.Bit);
				paramIsExchange.Direction = ParameterDirection.Output;

				sqlDbCn.Open();
				sqlDbCmd.ExecuteNonQuery();

				switch (holidayType)
				{
					case HolidayType.Any:
						if (paramIsAny.Value.Equals(DBNull.Value))
						{
							return true;
						}
						else
						{
							return !(bool)paramIsAny.Value;
						}
					case HolidayType.Bank:
						if (paramIsBank.Value.Equals(DBNull.Value))
						{
							return true;
						}
						else
						{
							return !(bool)paramIsBank.Value;
						}
					case HolidayType.Exchange:
						if (paramIsExchange.Value.Equals(DBNull.Value))
						{
							return true;
						}
						else
						{
							return !(bool)paramIsExchange.Value;
						}
					default:
						return false;
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Standard.IsBizDate]", Log.Error, 1);

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