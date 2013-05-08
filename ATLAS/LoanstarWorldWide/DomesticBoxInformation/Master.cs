using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Reflection;
using System.Reflection.Emit;
using StockLoan.Common;

namespace StockLoan.DomesticBoxInformation
{

  public class Master
  {
      const string APPLICATION_NAME = "DomesticBoxInformationMain";

    public static string BizDate = "";
	public static string BizDatePrior = "";
    public static string BizDatePriorExchange = "";
	public static string BizDateExchange = "";
	public static string BizDateNext = "";
	public static string ContractsBizDate = "";
    
    private string dbCnStr = "";
    private string externalDbCnStr = "";	

    private BDBoxInformationMain BDDataMain;

    public Master() : this(-1, "") {}
    public Master(int logLevel) : this(logLevel, "") {}
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
          Log.Write("Log file[s] will be written to " + Log.FilePath + " [Master.Master].", Log.Information);
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
        Log.Write("", 2); // Another blank space.
        
        Assembly[] assemblies = Thread.GetDomain().GetAssemblies();
        foreach (Assembly assembly in assemblies) // Write component info to log.
        {
          Log.Write("Running: " + assembly.GetName() + " " + assembly.GetName().Version, 2);
        }

        Log.Write("", 2); // Another blank space.
      }

			//Sendero database connection string			
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


      //External database connection string		
      if (Standard.ConfigValueExists("ExternalDatabaseUser"))   
      {
        // User credentials for External database has been specified.
        externalDbCnStr =  
          "User ID=" + Standard.ConfigValue("ExternalDatabaseUser") + "; " + 
          "Password=" + Standard.ConfigValue("ExternalDatabasePassword") + "; " +
          "Data Source=" + Standard.ConfigValue("ExternalDatabaseHost") + "; " + 
          "Initial Catalog=" + Standard.ConfigValue("ExternalDatabaseName") + ";";
      }
      else  
      {
        // Application user context is trusted, no need for user credentials.
        externalDbCnStr = 
          "Trusted_Connection=yes; " + 
          "Data Source=" + Standard.ConfigValue("ExternalDatabaseHost") + "; " + 
          "Initial Catalog=" + Standard.ConfigValue("ExternalDatabaseName") + ";";
      } 

			Log.Write("Using ExternalDbCnStr: " + externalDbCnStr + " [Master.Master]", 2);


      try
      { 
        BDDataMain = new BDBoxInformationMain(dbCnStr, externalDbCnStr);    // Added new argument: ExternalDbCnStr 
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [Master.Master]", Log.Error, 1);
      }

    }

    ~Master()
    {
      Log.Write("Terminated. [~Master.Master]", 2);
    }

    public void OnStart()
    {
      try
      {
        BDDataMain.Start();
        Log.Write("OnStart completed. [Master.OnStart]", Log.Information, 1);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [Master.OnStart]", Log.Error, 1);
      }

      KeyValue.Set("BDDataServiceStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCnStr);
    }

    public void OnStop()
    {
      try
      {
        BDDataMain.Stop();
        Log.Write("OnStop completed. [Master.OnStop]", Log.Information, 1);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [Master.OnStop]", Log.Error, 1);
      }

      KeyValue.Set("BDDataServiceStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCnStr);
    }
  }
}
