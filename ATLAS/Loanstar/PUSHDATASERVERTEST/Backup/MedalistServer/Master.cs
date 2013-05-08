// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

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
using Anetics.Common;

namespace Anetics.Medalist
{
  public class Master
  {
    const string APPLICATION_NAME = "MedalistServer";
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

		public static bool IsSubstitutionActive = false;
		public static bool IsSubstitutionDisabled = false;

    private TcpChannel tcpChannel = null;
    private HttpChannel httpChannel = null;

    private int channelPort;
    private string channelProtocol;
    private string channelFormatter;

    private string dbCnStr = "";
    private SqlConnection dbCn = null;

    private MedalistMain medalistMain;

    private ServiceAgent    serviceAgent;
    private AdminAgent      adminAgent;
    private ShortSaleAgent  shortSaleAgent;
    private PositionAgent		positionAgent;		
		private	RebateAgent			rebateAgent;
		private SubstitutionAgent substitutionAgent;

    private Anetics.Process.IProcess processAgent = null;
		private Anetics.SmartSeg.ISmartSeg smartSegAgent = null;

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
        medalistMain = new MedalistMain(dbCnStr);
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
				medalistMain.Start();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Master.OnStart]", Log.Error, 1);
			}

			if (bool.Parse(Standard.ConfigValue("UseProcessAgent", "True")))
			{
				processAgent = (Anetics.Process.IProcess) RemotingTools.ObjectGet(typeof(Anetics.Process.IProcess));
        
				if (processAgent == null)
				{
					Log.Write("Remoting config values for the process agent are not correct. [Master.Master]", Log.Error, 1);        
				}
			}
			else
			{
				Log.Write("Service will start without implementing the process agent. [Master.Master]", Log.Information, 1);        
			}

			if (bool.Parse(Standard.ConfigValue("UseSmartSegAgent", "True")))
			{
				smartSegAgent = (Anetics.SmartSeg.ISmartSeg) RemotingTools.ObjectGet(typeof(Anetics.SmartSeg.ISmartSeg));
       
				if (smartSegAgent == null)
				{
					Log.Write("Remoting config values for the smart seg agent are not correct. [Master.Master]", Log.Error, 1);        
				}
			}
			else
			{
				Log.Write("Service will start without implementing the smartseg agent. [Master.Master]", Log.Information, 1);        
			}

			channelFormatter = Standard.ConfigValue("ChannelFormatter", "binary").ToLower();
			channelProtocol = Standard.ConfigValue("ChannelProtocol", "tcp").ToLower();
			channelPort = int.Parse(Standard.ConfigValue("ChannelPort", "8822"));

			Log.Write("Running with channel protocol " + channelProtocol + " using a " + channelFormatter + 
				" formatter on port " + channelPort + ". [Master.Master]", Log.Information, 1);        
      
			switch (channelProtocol)
			{
				case "tcp" :
					if (channelFormatter.Equals("binary"))
					{
						tcpChannel = RemotingTools.TcpChannelGet(channelPort);
						ChannelServices.RegisterChannel(tcpChannel);
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
						ChannelServices.RegisterChannel(httpChannel);
					}  
					else if (channelFormatter.Equals("binary"))
					{
						httpChannel = new HttpChannel(channelPort);
						ChannelServices.RegisterChannel(httpChannel);
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

			adminAgent = new AdminAgent(dbCnStr);
			RemotingServices.Marshal(adminAgent, "AdminAgent." + channelFormatter);
			Log.Write("AdminAgent has been initialized. [Master.Master]", 2);

			shortSaleAgent = new ShortSaleAgent(dbCnStr);
			RemotingServices.Marshal(shortSaleAgent, "ShortSaleAgent." + channelFormatter);
			Log.Write("ShortSaleAgent has been initialized. [Master.Master]", 2);
     				
			positionAgent = new PositionAgent(dbCnStr, ref processAgent, ref substitutionAgent);
			RemotingServices.Marshal(positionAgent, "PositionAgent." + channelFormatter);
			Log.Write("PositionAgent has been initialized. [Master.Master]", 2);
        
			substitutionAgent = new SubstitutionAgent(dbCnStr, ref smartSegAgent, ref positionAgent);
			RemotingServices.Marshal(substitutionAgent, "SubstitutionAgent." + channelFormatter);
			Log.Write("SubstitutionAgent has been initialized. [Master.Master]", 2);        

			positionAgent.LocalSubstitutionAgent = substitutionAgent;

			
			medalistMain.MedalistMainPositionAgent = positionAgent;
			medalistMain.MedalistMainSubstitutionAgent = substitutionAgent;
        
			serviceAgent = new ServiceAgent(dbCnStr, ref processAgent, ref positionAgent, ref smartSegAgent, ref substitutionAgent);
			RemotingServices.Marshal(serviceAgent, "ServiceAgent." + channelFormatter);
			Log.Write("ServiceAgent has been initialized. [Master.Master]", 2);
        
			rebateAgent = new RebateAgent(dbCnStr);
			RemotingServices.Marshal(rebateAgent, "RebateAgent." + channelFormatter);
			Log.Write("RebateAgent has been initialized. [Master.Master]", 2);			

			serviceAgent.Start();

			Log.Write("OnStart completed. [Master.OnStart]", Log.Information, 1);
		}
		catch (Exception e)
		{
			Log.Write(e.Message + " [Master.OnStart]", Log.Error, 1);
			dbCn = null;
			return;
		}

      KeyValue.Set("MedalistServiceStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
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
        medalistMain.Stop();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [Master.OnStop]", Log.Error, 1);
      }

      try
      {
        serviceAgent.Stop(); // Fires event for client notification.

        Log.Write("Stop notification sent; waiting " + ON_STOP_SLEEP_MILLISECONDS + " milliseconds before terminating . [Master.OnStop]", 2);
        Thread.Sleep(ON_STOP_SLEEP_MILLISECONDS);

        RemotingServices.Disconnect(positionAgent);
        positionAgent = null;
        Log.Write("PositionAgent has been disconnected. [Master.OnStop]", 2);
        
				RemotingServices.Disconnect(substitutionAgent);
				substitutionAgent = null;
				Log.Write("SubstitutionAgent has been disconnected. [Master.OnStop]", 2);

        RemotingServices.Disconnect(shortSaleAgent);
        shortSaleAgent = null;
        Log.Write("ShortSaleAgent hsa been disconnected. [Master.OnStop]", 2);
        
        RemotingServices.Disconnect(adminAgent);
        adminAgent = null;
        Log.Write("AdminAgent has been disconnected. [Master.OnStop]", 2);
        
        RemotingServices.Disconnect(serviceAgent);
        serviceAgent = null;
        Log.Write("ServiceAgent has been disconnected. [Master.OnStop]", 2);
        
				RemotingServices.Disconnect(rebateAgent);
				rebateAgent = null;
				Log.Write("RebateAgent has been disconnected. [Master.OnStop]", 2);						 			

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
        KeyValue.Set("MedalistServiceStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
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
