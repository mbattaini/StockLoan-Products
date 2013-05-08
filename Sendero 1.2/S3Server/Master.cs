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
using Anetics.SmartSeg;
using Anetics.S3;

namespace Anetics.S3
{
	public class Master
	{
		const string APPLICATION_NAME = "S3";
		const int ON_STOP_SLEEP_MILLISECONDS = 2500;

		public static string BizDate = "";
		public static string BizDatePrior = "";
		public static string ContractsBizDate = "";
		public static string BizDateExchange = "";
		public static string BizDatePriorExchange= "";

		private TcpChannel tcpChannel = null;
		private HttpChannel httpChannel = null;

		private string dbCnStr = "";
		private string pensonDbCnStr = "";
		private S3Main s3Main;
	
		private S3Agent binAgent;		

		private string channelFormatter;
		private string channelProtocol;
		private int channelPort;

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

			if (Standard.ConfigValueExists("MainDatabaseUser")) // User credentials have been specified.
			{
				dbCnStr =  
					"User ID=" + Standard.ConfigValue("MainDatabaseUser") + "; " + 
					"Password=" + Standard.ConfigValue("MainDatabasePassword") + "; " +
					"Data Source=" + Standard.ConfigValue("MainDatabaseHost") + "; " + 
					"Initial Catalog=" + Standard.ConfigValue("MainDatabaseName") + ";";

				pensonDbCnStr =  
					"User ID=" + Standard.ConfigValue("PensonDatabaseUser") + "; " + 
					"Password=" + Standard.ConfigValue("PensonDatabasePassword") + "; " +
					"Data Source=" + Standard.ConfigValue("PensonDatabaseHost") + "; " + 
					"Initial Catalog=" + Standard.ConfigValue("PensonDatabaseName") + ";";
			}
			else  // Application user context is trusted, no need for user credentials.
			{
				dbCnStr = 
					"Trusted_Connection=yes; " + 
					"Data Source=" + Standard.ConfigValue("MainDatabaseHost") + "; " + 
					"Initial Catalog=" + Standard.ConfigValue("MainDatabaseName") + ";";

				pensonDbCnStr = 
					"Trusted_Connection=yes; " + 
					"Data Source=" + Standard.ConfigValue("PensonDatabaseHost") + "; " + 
					"Initial Catalog=" + Standard.ConfigValue("PensonDatabaseName") + ";";
			}

			Log.Write("Using dbCnStr: " + dbCnStr + " [Master.Master]", 2);
			Log.Write("Using pensonDbCnStr: " + pensonDbCnStr + " [Master.Master]", 2);

			try
			{ 
				s3Main = new S3Main(dbCnStr, pensonDbCnStr);
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
        
				Log.Write("OnStart completed. [Master.OnStart]", Log.Information, 1);
   								
				try
				{
					try
					{ 
						s3Main.Start();					
					}
					catch (Exception e)
					{
						Log.Write(e.Message + " [Master.OnStart]", Log.Error, 1);
					}
					
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
								httpChannel = RemotingTools.HttpChannelGet(channelPort);
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

					binAgent = new S3Agent(dbCnStr);
					RemotingServices.Marshal(binAgent, "SmartSegAgent." + channelFormatter);
					Log.Write("SmartSegAgent has been initialized. [Master.Master]", 2);

					binAgent.Start();

					Log.Write("OnStart completed. [Master.OnStart]", Log.Information, 1);
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [Master.OnStart]", Log.Error, 1);				
				}						
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Master.OnStart]", Log.Error, 1);
			}

			KeyValue.Set("S3ServiceStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCnStr);
		}

		public void OnStop()
		{			
			try
			{
				s3Main.Stop();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Master.OnStop]", Log.Error, 1);
			}

			try
			{
				binAgent.Stop();
			
				Log.Write("Stop notification sent; waiting " + ON_STOP_SLEEP_MILLISECONDS + " milliseconds before terminating . [Master.OnStop]", 2);
				Thread.Sleep(ON_STOP_SLEEP_MILLISECONDS);
				
				RemotingServices.Disconnect(binAgent);
				binAgent = null;
				Log.Write("SmartSegAgent has been disconnected. [Master.OnStop]", 2);
				
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
				KeyValue.Set("S3ServiceStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCnStr);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Master.OnStop]", Log.Error, 1);
			}				
		}	
	}
}
