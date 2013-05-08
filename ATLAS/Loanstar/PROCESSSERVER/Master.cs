using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using StockLoan.Common;

namespace StockLoan.Process
{
  public class Master
  {
    const string APPLICATION_NAME = "ProcessServer";
    const int ON_STOP_SLEEP_MILLISECONDS = 2500;

    public static string BizDate = "";
    public static string BizDatePrior = "";
	public static string BizDatePriorBank = "";
    public static string ContractsBizDate = "";

    private TcpChannel tcpChannel = null;
    private HttpChannel httpChannel = null;

    private int channelPort;
    private string channelProtocol;
    private string channelFormatter;

    private string dbCnStr = "";
    private SqlConnection dbCn = null;
    
    private ProcessMain processMain;
    private ProcessAgent processAgent;

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

      if (Standard.ConfigValueExists("MainDatabase.User")) // User credentials have been specified.
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

	  try
	  {
		  processAgent = new ProcessAgent(dbCnStr);
		  processMain = new ProcessMain(dbCnStr, ref processAgent);
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
        return;
      }

      try
      {
        channelFormatter = Standard.ConfigValue("ChannelFormatter", "binary").ToLower();
        channelProtocol = Standard.ConfigValue("ChannelProtocol", "http").ToLower();
        channelPort = int.Parse(Standard.ConfigValue("ChannelPort", "8824"));

        Log.Write("Running with channel protocol " + channelProtocol + " using a " + channelFormatter + 
          " formatter on port " + channelPort + ". [Master.Master]", Log.Information, 1);        
      
        switch (channelProtocol)
        {
          case "tcp" :
            if (channelFormatter.Equals("binary"))
            {
              tcpChannel = RemotingTools.TcpChannelGet(channelPort);
              ChannelServices.RegisterChannel(tcpChannel, false);
            }
            else
            {
              Log.Write("The " + channelFormatter + " channel formatter has not been implemented for the tcp protocol. [Master.OnStart]", Log.Error, 1);        
            }
            break;
          case "http" :
            if (channelFormatter.Equals("soap"))
            {
              httpChannel = new HttpChannel(channelPort);
              ChannelServices.RegisterChannel(httpChannel, false);
            }  
            else if (channelFormatter.Equals("binary"))
            {
              httpChannel = RemotingTools.HttpChannelGet(channelPort);
              ChannelServices.RegisterChannel(httpChannel, false);
            }
            else
            {
              Log.Write("The " + channelFormatter + " channel formatter has not been implemented for the http protocol. [Master.OnStart]", Log.Error, 1);        
            }
            break;
          default :
            Log.Write("The " + channelProtocol + " channel protocol is not valid. [Master.OnStart]", Log.Error, 1);        
            break;
        }

		processAgent = new ProcessAgent(dbCnStr);
        RemotingServices.Marshal(processAgent, "BackOfficeAgent." + channelFormatter);
        Log.Write("ProcessAgent has been initialized. [Master.Master]", 2);

        try
        { 
          processMain.Start();
		  processAgent.Start();
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
      }

      KeyValue.Set("LoanetServiceStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
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
        processMain.Stop();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [Master.OnStop]", Log.Error, 1);
      }

      try
      {
		  processAgent.Stop(); // Fires event for client notification.

        Log.Write("Stop notification sent; waiting " + ON_STOP_SLEEP_MILLISECONDS + " milliseconds before terminating . [Master.OnStop]", 2);
        Thread.Sleep(ON_STOP_SLEEP_MILLISECONDS);

        RemotingServices.Disconnect(processAgent);
		processAgent = null;
        Log.Write("ProcessAgent has been disconnected. [Master.OnStop]", 2);
                
        if (tcpChannel != null)
        {
          ChannelServices.UnregisterChannel(tcpChannel);
          Log.Write("The tcp channel on port " + channelPort + " has been unregistered. [Master.OnStop]", 2);

        }

        if (httpChannel != null)
        {
          ChannelServices.UnregisterChannel(httpChannel);
          Log.Write("The http channel on port " + channelPort + " has been unregistered. [Master.OnStop]", 2);
        }
      
        Log.Write("OnStop completed. [Master.OnStop]", Log.Information, 1);
        KeyValue.Set("LoanetServiceStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
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
