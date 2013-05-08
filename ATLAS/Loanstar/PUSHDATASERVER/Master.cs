using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Reflection;
using System.Reflection.Emit;
using StockLoan.Common;

namespace StockLoan.PushData
{
	public class Master
	{
		const string APPLICATION_NAME = "PushDataServer";
		const int ON_STOP_SLEEP_MILLISECONDS = 2500;

		public static string BizDate = "";
		public static string BizDateNext = "";
		public static string BizDatePrior = "";

		public static string BizDateBank = "";
		public static string BizDateNextBank = "";
		public static string BizDatePriorBank = "";

		public static string BizDateExchange = "";
		public static string BizDateNextExchange = "";
		public static string BizDatePriorExchange = "";

		public static string ContractsBizDate = "";


		private string dbCnStr = "";
		private SqlConnection dbCn = null;

		private string externalDbCnStr = "";
		private SqlConnection externalDbCn = null;


		private PushData PushData;

		public Master() : this(-1, "") { }
		public Master(int logLevel) : this(logLevel, "") { }
		public Master(int logLevel, string logFilePath)
		{
			Log.Name = APPLICATION_NAME;

			if (!logLevel.Equals(-1)) // Command arg was presented, takes priority.
			{
				Log.Level = logLevel.ToString();
			}
			else if (Standard.ConfigValueExists("LogLevel")) // Get value from config file.
			{
				Log.Level = Standard.ConfigValue("LogLevel");
			}
			Log.Write("Log verbosity level set to " + Log.Level + ". [Master.Master]", Log.Information);

			if (!logFilePath.Equals("")) // Command arg was presented, takes priority.
			{
				Log.FilePath = logFilePath;
			}
			else if (Standard.ConfigValueExists("LogFilePath")) // Get value from config file.
			{
				Log.FilePath = Standard.ConfigValue("LogFilePath");
			}

			if (Log.FilePath.Equals(""))
			{
				Log.Write("Log file path not specified [Master.Master].", Log.Warning);
			}
			else
			{
				if (Directory.Exists(Log.FilePath))
				{
					Log.Write("Log file will be written to " + Log.FilePath + " [Master.Master].", Log.Information);
				}
				else
				{
					Log.Write("The log file path " + Log.FilePath + " does not exist. [Master.Master]", Log.Error);
					Log.FilePath = "";
				}
			}

			Log.Write("", 1);
			Log.Write("", 1); // Introduces blank spaces into log file help find session stop/start.
			Log.Write("Initialized. [Master.Master]", 1);

			if (Log.Level.CompareTo("2") >= 0)
			{
				Log.Write("", 2);

				Assembly[] assemblies = Thread.GetDomain().GetAssemblies();
				foreach (Assembly assembly in assemblies) // Write component info to log.
				{
					Log.Write("Running: " + assembly.GetName() + " " + assembly.GetName().Version, 2);
				}

				Log.Write("", 2);
			}

			if (Standard.ConfigValueExists("MainDatabaseUser")) // User credentials have been specified.
			{
				dbCnStr =
				 "User ID=" + Standard.ConfigValue("MainDatabaseUser") + "; " +
				  "Password=" + Standard.ConfigValue("MainDatabasePassword") + "; " +
				  "Data Source=" + Standard.ConfigValue("MainDatabaseHost") + "; " +
				  "Initial Catalog=" + Standard.ConfigValue("MainDatabaseName") + ";";
			}
			else  // Application user context is trusted, no need for user credentials.
			{
				dbCnStr =
				  "Trusted_Connection=yes; " +
				  "Data Source=" + Standard.ConfigValue("MainDatabaseHost") + "; " +
				  "Initial Catalog=" + Standard.ConfigValue("MainDatabaseName") + ";";
			}
		

			Log.Write("Using dbCnStr: " + dbCnStr + " [Master.Master]", 2);
			Log.Write("Using external dbCnStr: " + externalDbCnStr + " [Master.Master]", 2);

			try
			{
				PushData = new PushData(dbCnStr);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Master.Master]", Log.Error, 1);
			}
		}

		~Master()
		{
			if (dbCn != null)
			{
				dbCn.Dispose();
			}

			Log.Write("Terminated. [~Master.Master]", 2);
		}

		public void OnStart()
		{
			if (dbCn != null) // Service has already been started.
			{
				Log.Write("OnStart called with server already running. [Master.OnStart]", Log.Warning, 1);
				return;
			}

			try
			{
				dbCn = new SqlConnection(dbCnStr);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Master.OnStart]", Log.Error, 1);

				dbCn = null;
				return;
			}

			try
			{
				try
				{
					PushData.Start();
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [Master.OnStart]", Log.Error, 1);
				}


				Log.Write("OnStart completed. [Master.OnStart]", Log.Information, 1);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Master.OnStart]", Log.Error, 1);
				dbCn = null;
				return;
			}

			KeyValue.Set("PushDataServiceStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
		}

		public void OnStop()
		{
			if (dbCn == null) // Service has already been stopped.
			{
				Log.Write("OnStop called with server already stopped. [Master.OnStop]", Log.Warning, 1);
				return;
			}

			try
			{
				PushData.Stop();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Master.OnStop]", Log.Error, 1);
			}

			try
			{
				Log.Write("Stop notification sent; waiting " + ON_STOP_SLEEP_MILLISECONDS + " milliseconds before terminating . [Master.OnStop]", 2);
				Thread.Sleep(ON_STOP_SLEEP_MILLISECONDS);

				Log.Write("OnStop completed. [Master.OnStop]", Log.Information, 1);
				KeyValue.Set("PushDataServiceStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Master.OnStop]", Log.Error, 1);
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}

				dbCn = null;
			}
		}
	}
}
