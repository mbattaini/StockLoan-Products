using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

using StockLoan.Common;

namespace StockLoan.Xfl
{
    public class Master
    {
        private XflMain xflMain;
        public static string BizDate = "";
        public static string BizDatePrior = "";

        private string senderoDbCnStr = "";         //DChen 
        private string xflDbCnStr = "";             //DChen 
                           

        public void OnStart()
        {
            try
            {
                xflMain.Start();
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [Master.OnStart]", Log.Error, 1);
            }

            try
            {
                KeyValue.Set("XflServiceStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), senderoDbCnStr);
                Log.Write("OnStart completed. [Master.OnStart]", Log.Information, 1);
            }
            catch (Exception ex) 
            {
                Log.Write(ex.Message + " [Master.OnStart]", Log.Error, 1);
            }

        }

        public void OnStop()
        {
            try
            {
                xflMain.Stop();
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [Master.OnStop]", Log.Error, 1);
            }

            try
            {
                KeyValue.Set("XflServiceStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), senderoDbCnStr);
                Log.Write("OnStop completed. [Master.OnStop]", Log.Information, 1);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [Master.OnStop]", Log.Error, 1);
            }
        }


        public Master(int logLevel, string logFilePath)
        {
            Log.Name = "XflServer";

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

            try
            {
                //string senderoDbCnStr = "";       //DChen 
                //string xflDbCnStr = "";           //DChen 

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                // Sendero database connection string
                builder.ApplicationName = "XflServer";
                if (Standard.ConfigValueExists("SenderoDatabaseUser"))
                {
                    builder.UserID = Standard.ConfigValue("SenderoDatabaseUser"); ;
                    builder.Password = Standard.ConfigValue("SenderoDatabasePassword");
                }
                else
                    builder.IntegratedSecurity = true;

                builder.DataSource = Standard.ConfigValue("SenderoDatabaseHost");
                builder.InitialCatalog = Standard.ConfigValue("SenderoDatabaseName");
                senderoDbCnStr = builder.ConnectionString;

                // Xfl database connection string
                builder.Clear();
                builder.ApplicationName = "XflServer";
                if (Standard.ConfigValueExists("XflDatabaseUser"))
                {
                    builder.UserID = Standard.ConfigValue("XflDatabaseUser"); ;
                    builder.Password = Standard.ConfigValue("XflDatabasePassword");
                }
                else
                    builder.IntegratedSecurity = true;

                builder.DataSource = Standard.ConfigValue("XflDatabaseHost");
                builder.InitialCatalog = Standard.ConfigValue("XflDatabaseName");
                xflDbCnStr = builder.ConnectionString;

                Log.Write("Using senderodbCnStr: " + senderoDbCnStr + " [Master.Master]", 2);
                Log.Write("Using xfldbCnStr: " + xflDbCnStr + " [Master.Master]", 2);

                xflMain = new XflMain(senderoDbCnStr, xflDbCnStr);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [Master.Master]", Log.Error, 1);
            }
        }
    }
}
