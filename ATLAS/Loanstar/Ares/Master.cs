
using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Reflection;
using System.Reflection.Emit;
using StockLoan.Common;

namespace StockLoan.Ares
{

  public class Master
  {
    const string APPLICATION_NAME = "Ares";

    public static string BizDate = "";
	public static string BizDatePrior = "";
    public static string BizDatePriorExchange = "";
	public static string BizDateExchange = "";
	public static string BizDateNext = "";
	public static string ContractsBizDate = "";
    
    private string dbCnStr = "";
    private string worldwideDbCnStr = "";	

    private AresMain aresMain;

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


      //WorldWide database connection string		
      if (Standard.ConfigValueExists("WorldWideDatabaseUser"))   
      {
        // User credentials for WorldWide database has been specified.
        worldwideDbCnStr =  
          "User ID=" + Standard.ConfigValue("WorldWideDatabaseUser") + "; " + 
          "Password=" + Standard.ConfigValue("WorldWideDatabasePassword") + "; " +
          "Data Source=" + Standard.ConfigValue("WorldWideDatabaseHost") + "; " + 
          "Initial Catalog=" + Standard.ConfigValue("WorldWideDatabaseName") + ";";
      }
      else  
      {
        // Application user context is trusted, no need for user credentials.
        worldwideDbCnStr = 
          "Trusted_Connection=yes; " + 
          "Data Source=" + Standard.ConfigValue("WorldWideDatabaseHost") + "; " + 
          "Initial Catalog=" + Standard.ConfigValue("WorldWideDatabaseName") + ";";
      } 

			Log.Write("Using worldwideDbCnStr: " + worldwideDbCnStr + " [Master.Master]", 2);


      try
      { 
        aresMain = new AresMain(dbCnStr, worldwideDbCnStr);    // Added new argument: WorldWideDbCnStr 
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
        aresMain.Start();
        Log.Write("OnStart completed. [Master.OnStart]", Log.Information, 1);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [Master.OnStart]", Log.Error, 1);
      }

      KeyValue.Set("AresServiceStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCnStr);
    }

    public void OnStop()
    {
      try
      {
        aresMain.Stop();
        Log.Write("OnStop completed. [Master.OnStop]", Log.Information, 1);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [Master.OnStop]", Log.Error, 1);
      }

      KeyValue.Set("AresServiceStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCnStr);
    }
  }
}
