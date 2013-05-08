using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using System.Text;
using System.Threading;

using StockLoan.Common;

namespace StockLoan.Golden
{
  public class Master
  {
    const int ON_STOP_SLEEP_MILLISECONDS = 2500;

    public static string BizDate = "";
    private GoldenMain goldenMain = null;
    private string channelFormatter;
    private string channelProtocol;
    private int channelPort;
    private TcpChannel tcpChannel = null;
    private HttpChannel httpChannel = null;

    private MemoSegAgent memoSegAgent = null;
    private RebateAgent rebateAgent = null;
    private string dbCnStr;

    public void OnStart()
    {
      try
      {
          Log.FilePath = Standard.ConfigValue("LogFilePath");
        goldenMain.Start();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [Master.OnStart]", Log.Error, 1);
      }

      try
      {
        channelFormatter = Standard.ConfigValue("ChannelFormatter", "binary").ToLower();
        channelProtocol = Standard.ConfigValue("ChannelProtocol", "tcp").ToLower();
        channelPort = int.Parse(Standard.ConfigValue("ChannelPort", "8823"));

        Log.Write("Running with channel protocol " + channelProtocol + " using a " + channelFormatter + " formatter on port " + channelPort + ". [Master.Master]", Log.Information, 1);        

        switch (channelProtocol)
        {
          case "tcp":
            if (channelFormatter.Equals("binary"))
            {
              tcpChannel = RemotingTools.TcpChannelGet(channelPort);
              ChannelServices.RegisterChannel(tcpChannel, false);
            }
            else
            {
              Log.Write("The " + channelFormatter + " channel formatter has not been implemented for the tcp protocol. [Master.OnStart]", 2);
            }
            break;
          case "http":
            if (channelFormatter.Equals("soap"))
            {
              httpChannel = new HttpChannel(channelPort);
              ChannelServices.RegisterChannel(httpChannel, false);
            }
            else if (channelFormatter.Equals("binary"))
            {
              httpChannel = new HttpChannel(channelPort);
              ChannelServices.RegisterChannel(httpChannel, false);
            }
            else
            {
              Log.Write("The " + channelFormatter + " channel formatter has not been implemented for the http protocol. [Master.OnStart]", 2);
            }
            break;
          default:
            Log.Write("The " + channelProtocol + " channel protocol is not valid. [Master.OnStart]", 2);
            break;
        }

        memoSegAgent = new MemoSegAgent(dbCnStr);
        RemotingServices.Marshal(memoSegAgent, "MemoSegAgent." + channelFormatter);
        Log.Write("MemoSegAgent has been initialized. [Master.OnStart]", 2);

        rebateAgent = new RebateAgent(dbCnStr);
        RemotingServices.Marshal(rebateAgent, "RebateAgent." + channelFormatter);
        Log.Write("RebateAgent has been initialized. [Master.OnStart]", 2);
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [Master.OnStart]", Log.Error, 1);
      }
    }

    public void OnStop()
    {
      try
      {
				goldenMain.Stop();

        if (memoSegAgent != null)
        {
            RemotingServices.Disconnect(memoSegAgent);
            memoSegAgent = null;
            Log.Write("MemoSegAgent has been disconnected. [Master.OnStop]", 2);
        }

        if (rebateAgent != null)
        {
            RemotingServices.Disconnect(rebateAgent);
            rebateAgent = null;
            Log.Write("RebateAgent has been disconnected. [Master.OnStop]", 2);
        }

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

        Log.Write("OnStop completed. [Master.OnStop]", Log.Information, 2);
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [Master.OnStop]", Log.Error, 1);
      }

    }

    public Master(int logLevel, string logFilePath)
    {
      Log.Name = "GoldenServer";
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

      try
      {
       goldenMain = new GoldenMain(dbCnStr);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [Master.Master]", Log.Error, 1);
      }
    }
  }
}
