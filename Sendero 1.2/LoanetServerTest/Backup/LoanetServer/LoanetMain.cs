// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Anetics.Common;

namespace Anetics.Loanet
{
	public class LoanetMain
	{
    private string countryCode;

    private string dbCnStr;
    private SqlConnection dbCn = null;

    private Thread mainThread = null;

    private static bool isStopped = true;
    private static string tempPath;

    public LoanetMain(string dbCnStr)
    {
      this.dbCnStr = dbCnStr;

      try
      {
        dbCn = new SqlConnection(dbCnStr);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [LoanetMain.LoanetMain]", Log.Error, 1);
      }

      countryCode = Standard.ConfigValue("CountryCode", "US");
      Log.Write("Using country code: " + countryCode + " [LoanetMain.LoanetMain]", 2);

      if (Standard.ConfigValueExists("TempPath"))
      {
        tempPath = Standard.ConfigValue("TempPath");

        if (!Directory.Exists(tempPath))
        {
          Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [LoanetMain.LoanetMain]", Log.Error, 1);
          tempPath = Directory.GetCurrentDirectory();
        }
      }
      else
      {
        Log.Write("A configuration value for TempPath has not been provided. [LoanetMain.LoanetMain]", Log.Information, 1);
        tempPath = Directory.GetCurrentDirectory();
      }

      Log.Write("Temporary files will be staged at " + tempPath + ". [LoanetMain.LoanetMain]", 2);
    }

    ~LoanetMain()
    {
      if (dbCn != null)
      {
        dbCn.Dispose();
      }
    }

    public void Start()
    {
      isStopped = false;

      if ((mainThread == null) || (!mainThread.IsAlive)) // Must create new thread.
      {
        mainThread = new Thread(new ThreadStart(LoanetMainLoop));
        mainThread.Name = "Main";
        mainThread.Start();

        Log.Write("Start command issued with new main thread. [LoanetMain.Start]", 3);
      }
      else // Old thread will be just fine.
      {
        Log.Write("Start command issued with main thread already running. [LoanetMain.Start]", 3);
      }
    }

    public void Stop()
    {
      isStopped = true;

      if (mainThread == null)
      {
        Log.Write("Stop command issued, main thread never started. [LoanetMain.Stop]", 3);
      }
      else if (mainThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
      {
        mainThread.Abort();
        Log.Write("Stop command issued, sleeping main thread aborted. [LoanetMain.Stop]", 3);
      }
      else
      {
        Log.Write("Stop command issued, main thread is still active. [LoanetMain.Stop]", 3);
      }
    }

    private void LoanetMainLoop()
    {
      while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
      {
        Log.Write("Start of cycle. [LoanetMain.LoanetMainLoop]", 2);
        KeyValue.Set("LoanetMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        
        Master.BizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);
        Master.BizDatePrior = KeyValue.Get("BizDatePrior", "0001-01-01", dbCn);
				Master.BizDatePriorBank = KeyValue.Get("BizDatePriorBank", "0001-01-01", dbCn);
        Master.ContractsBizDate = KeyValue.Get("ContractsBizDate", "0001-01-01", dbCn);
        Master.IsS3Active = bool.Parse(KeyValue.Get("IsS3Active", "False", dbCn));
 
        if (Standard.ConfigValueExists("PositionFileList(plo)"))
        {        
          if (Standard.ConfigValueExists("PositionRemotePath"))
          {        
            Position("plo");        
          }
          else
          {
            Log.Write("A configuration value for PositionRemotePath does not exist. [LoanetMain.LoanetMainLoop]", 3);
          }
        }
        else
        {
          Log.Write("A configuration value for PositionFileList(plo) does not exist. [LoanetMain.LoanetMainLoop]", 3);
        }
        
        if (isStopped)
        {
          break;
        }

        if (Standard.ConfigValueExists("PositionFileList(peo)"))
        {        
          if (Standard.ConfigValueExists("PositionRemotePath"))
          {        
            Position("peo");        
          }
          else
          {
            Log.Write("A configuration value for PositionRemotePath does not exist. [LoanetMain.LoanetMainLoop]", 3);
          }
        }
        else
        {
          Log.Write("A configuration value for PositionFileList(peo) does not exist. [LoanetMain.LoanetMainLoop]", 3);
        }
        
        if (isStopped)
        {
          break;
        }
                
        if (Standard.ConfigValueExists("MarksFileList"))
        {        
          if (Standard.ConfigValueExists("MarksRemotePath"))
          {        
            Marks();        
          }
          else
          {
            Log.Write("A configuration value for MarksRemotePath does not exist. [LoanetMain.LoanetMainLoop]", 3);
          }
        }
        else
        {
          Log.Write("A configuration value for MarksFileList does not exist. [LoanetMain.LoanetMainLoop]", 3);
        }
        
        if (isStopped)
        {
          break;
        }
        
        if (Standard.ConfigValueExists("CollateralFileList"))
        {        
          if (Standard.ConfigValueExists("CollateralRemotePath"))
          {        
            Collateral();        
          }
          else
          {
            Log.Write("A configuration value for CollateralRemotePath does not exist. [LoanetMain.LoanetMainLoop]", 3);
          }
        }
        else
        {
          Log.Write("A configuration value for CollateralFileList does not exist. [LoanetMain.LoanetMainLoop]", 3);
        }
        
        if (isStopped)
        {
          break;
        }
        
        if (Standard.ConfigValueExists("RecallsFileList"))
        {        
          if (Standard.ConfigValueExists("RecallsRemotePath"))
          {        
            Recalls();        
          }
          else
          {
            Log.Write("A configuration value for RecallsRemotePath does not exist. [LoanetMain.LoanetMainLoop]", 3);
          }
        }
        else
        {
          Log.Write("A configuration value for RecallsFileList does not exist. [LoanetMain.LoanetMainLoop]", 3);
        }
        
        if (isStopped)
        {
          break;
        }
                
        if (Standard.ConfigValueExists("ClientsFileList"))
        {
          if (Standard.ConfigValueExists("ClientsRemotePath"))
          {        
            Clients();        
          }
          else
          {
            Log.Write("A configuration value for ClientsRemotePath does not exist. [LoanetMain.LoanetMainLoop]", 3);
          }
        }
        else
        {
          Log.Write("A configuration value for ClientsFileList does not exist. [LoanetMain.LoanetMainLoop]", 3);
        }
        
        if (isStopped)
        {
          break;
        }
        
        if (Standard.ConfigValueExists("ClientsLongFileList"))
        {
          if (Standard.ConfigValueExists("ClientsLongRemotePath"))
          {        
            ClientsLong();        
          }
          else
          {
            Log.Write("A configuration value for ClientsLongRemotePath does not exist. [LoanetMain.LoanetMainLoop]", 3);
          }
        }
        else
        {
          Log.Write("A configuration value for ClientsLongFileList does not exist. [LoanetMain.LoanetMainLoop]", 3);
        }
        
        if (isStopped)
        {
          break;
        }
        
        KeyValue.Set("LoanetMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        Log.Write("End of cycle. [LoanetMain.LoanetMainLoop]", 2);
        
        if (!isStopped)
        {
          Thread.Sleep(RecycleInterval());
        }
      }
    }

    private void Position(string fileType)
    {
      string [] clientIdList = Standard.ConfigValue("PositionFileList(" + fileType + ")").Split(';');

      foreach(string clientId in clientIdList)
      {
        if (clientId.Length.Equals(4) && !isStopped)
        {
          Loanet loanet = new Loanet(dbCn);
          loanet.PositionDo(clientId, fileType);
        }
      }
    }

    private void Marks()
    {
      string [] clientIdList = Standard.ConfigValue("MarksFileList").Split(';');
      
      foreach(string clientId in clientIdList)
      {
        if (clientId.Length.Equals(4) && !isStopped)
        {
          Loanet loanet = new Loanet(dbCn);
          loanet.MarksDo(clientId);
        }
      }
    }
  
    private void Collateral()
    {
      string [] clientIdList = Standard.ConfigValue("CollateralFileList").Split(';');

      foreach(string clientId in clientIdList)
      {
        if (clientId.Length.Equals(4) && !isStopped)
        {
          Loanet loanet = new Loanet(dbCn);
          loanet.CollateralDo(clientId);
        }
      }
    }

    private void Recalls()
    {
      string [] clientIdList = Standard.ConfigValue("RecallsFileList").Split(';');

      foreach(string clientId in clientIdList)
      {
        if (clientId.Length.Equals(4) && !isStopped)
        {
          Loanet loanet = new Loanet(dbCn);
          loanet.RecallsDo(clientId);
        }
      }
    }

    private void Clients()
    {
      string [] clientIdList = Standard.ConfigValue("ClientsFileList").Split(';');
      
      foreach(string clientId in clientIdList)
      {
        if (clientId.Length.Equals(4) && !isStopped)
        {
          Loanet loanet = new Loanet(dbCn);
          loanet.ClientsDo(clientId);
        }
      }
    }

    private void ClientsLong()
    {
      string [] clientIdList = Standard.ConfigValue("ClientsLongFileList").Split(';');
      
      foreach(string clientId in clientIdList)
      {
        if (clientId.Length.Equals(4) && !isStopped)
        {
          Loanet loanet = new Loanet(dbCn);
          loanet.ClientsLongDo(clientId);
        }
      }
    }

    private TimeSpan RecycleInterval()
    {
      string recycleInterval;
      string [] values;

      int hours;
      int minutes;

      bool isBizDay = Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Any, dbCn);
      TimeSpan timeSpan;

      char [] delimiter = new char[1];
      delimiter[0] = ':';

      if (isBizDay)
      {
        recycleInterval = KeyValue.Get("LoanetMainRecycleIntervalBizDay", "0:20", dbCn);
      }
      else
      {
        recycleInterval = KeyValue.Get("LoanetMainRecycleIntervalNonBizDay", "6:00", dbCn);
      }

      try
      {
        values = recycleInterval.Split(delimiter, 2);
        hours = int.Parse(values[0]);
        minutes = int.Parse(values[1]);
        timeSpan = new TimeSpan (hours, minutes, 0);
      }
      catch
      {
        if (isBizDay)
        {
          KeyValue.Set("LoanetMainRecycleIntervalBizDay", "0:20", dbCn);
          hours = 0;
          minutes = 20;
          timeSpan = new TimeSpan (hours, minutes, 0);
          Log.Write("LoanetMainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [LoanetMain.RecycleInterval]", Log.Error, 1);
        }
        else
        {
          KeyValue.Set("LoanetMainRecycleIntervalNonBizDay", "6:00", dbCn);
          hours = 6;
          minutes = 0;
          timeSpan = new TimeSpan (hours, minutes, 0);
          Log.Write("LoanetMainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [LoanetMain.RecycleInterval]", Log.Error, 1);
        }
      }

      Log.Write("LoanetMainLoop will recycle in " + hours + " hours, " + minutes + " minutes. [LoanetMain.RecycleInterval]", 2);
      return timeSpan;
    }

    public static string TempPath
    {
      get
      {
        return tempPath;
      }
    }
    
    public static bool IsStopped
    {
      get
      {
        return isStopped;
      }
    }
  }
}
