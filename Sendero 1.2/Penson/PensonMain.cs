// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Anetics.Common;

namespace Anetics.Penson
{
	public class PensonMain
	{
    private string dbCnStr;
    private SqlConnection dbCn = null;

    private string dbCnStrPenson;
    private SqlConnection dbCnPenson = null;

    private Thread workerThread = null;
    private static bool isStopped = true;
    private static string tempPath;
    
    private static string bizDate;
    private static string bizDatePriorBank;

    public PensonMain(string dbCnStr, string dbCnStrPenson)
    {
      this.dbCnStr = dbCnStr;
      this.dbCnStrPenson = dbCnStrPenson;

      try
      {
        dbCn = new SqlConnection(dbCnStr);
        dbCnPenson = new SqlConnection(dbCnStrPenson);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [PensonMain.PensonMain]", Log.Error, 1);
      }

      if (Standard.ConfigValueExists("TempPath"))
      {
        tempPath = Standard.ConfigValue("TempPath");

        if (!Directory.Exists(tempPath))
        {
          Log.Write("Directory from config value for TempPath, " + tempPath + ", does not exist. [PensonMain.PensonMain]", Log.Error, 1);
          tempPath = Directory.GetCurrentDirectory();
        }
      }
      else
      {
        Log.Write("A configuration value for TempPath has not been provided. [PensonMain.PensonMain]", Log.Information, 1);
        tempPath = Directory.GetCurrentDirectory();
      }

      Log.Write("Temporary files will be staged at " + tempPath + ". [PensonMain.PensonMain]", 2);
    }

    ~PensonMain()
    {
      dbCn.Dispose();
      dbCnPenson.Dispose();
    }

    public void Start()
    {
      isStopped = false;

      if ((workerThread == null)||(!workerThread.IsAlive)) // Must create new thread.
      {
        workerThread = new Thread(new ThreadStart(DoPensonMain));
        workerThread.Name = "Worker";
        workerThread.Start();

        Log.Write("Start command issued with new worker thread. [PensonMain.Start]", 2);
      }
      else // Old thread will be just fine.
      {
        Log.Write("Start command issued with worker thread already running. [PensonMain.Start]", 2);
      }
    }

    public void Stop()
    {
      isStopped = true;

      if (workerThread == null)
      {
        Log.Write("Stop command issued, worker thread never started. [PensonMain.Stop]", 2);
      }
      else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
      {
        workerThread.Abort();
        Log.Write("Stop command issued, sleeping worker thread aborted. [PensonMain.Stop]", 2);
      }
      else
      {
        Log.Write("Stop command issued, worker thread is still active. [PensonMain.Stop]", 2);
      }
    }

    private void DoPensonMain()
    {
      while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
      {
        Log.Write("DoPensonMain cycle start. [PensonMain.DoPensonMain]", 2);
        KeyValue.Set("PensonMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        
        bizDate = KeyValue.Get("BizDate", "0000-00-00", dbCn);
        bizDatePriorBank = KeyValue.Get("BizDatePriorBank", "0000-00-00", dbCn);
        
        StaticLoad();
    
        KeyValue.Set("PensonMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        Log.Write("DoPensonMain cycle stop. [PensonMain.DoPensonMain]", 2);
        
        if (!isStopped)
        {
          Thread.Sleep(RecycleInterval());
        }
      }
    }

    private void StaticLoad()
    {
      Log.Write("Will check static data for " + bizDate + ". [PensonMain.StaticLoad]", 2);
      
      StaticData staticData = new StaticData(dbCn, dbCnStr, dbCnPenson, dbCnStrPenson);
  
     if (!isStopped) // Do box position.
      {
        int i = 0;
        string configValue;

        while ((configValue = Standard.ConfigValue("BoxPosition[" + (++i).ToString("00#") + "]", "")) != "")            
        {
          staticData.BoxPositionLoad(
            i.ToString("00#"),
            Tools.SplitItem(configValue, ";", 0),
            Tools.SplitItem(configValue, ";", 1));
          
          if (isStopped)
          {
            break;
          }
        }
      }
      
			if (!isStopped) // Do rock static data.
			{
				staticData.RockPositionLoad();
			}
			
			if (!isStopped) // Do rock static data.
			{
				staticData.JBOPositionLoad();
			}
			

			if (!isStopped) // Do box location.
      {
        staticData.BoxLocationLoad();
      }
      
      if (!isStopped) // Do security static data.
      {
        staticData.SecurityDataLoad();
      }
      
      if (!isStopped) // Do trading group data.
      {
        staticData.CorrespondentDataLoad();
      }

			if (!isStopped) // Do end of day fail day count set
			{
				staticData.BoxPositionFailDayCountSet();
			}
    }

    private TimeSpan RecycleInterval()
    {
      string recycleInterval;
      string [] values;

      int hours;
      int minutes;

      bool isBizDay = (DateTime.UtcNow.DayOfWeek != DayOfWeek.Saturday) && (DateTime.UtcNow.DayOfWeek != DayOfWeek.Sunday);
      TimeSpan timeSpan;

      char [] delimiter = new char[1];
      delimiter[0] = ':';

      if (isBizDay)
      {
        recycleInterval = KeyValue.Get("PensonMainRecycleIntervalBizDay", "0:20", dbCn);
      }
      else
      {
        recycleInterval = KeyValue.Get("PensonMainRecycleIntervalNonBizDay", "6:00", dbCn);
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
          KeyValue.Set("PensonMainRecycleIntervalBizDay", "0:20", dbCn);
          hours = 0;
          minutes = 30;
          timeSpan = new TimeSpan (hours, minutes, 0);
          Log.Write("PensonMainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [PensonMain.RecycleInterval]", Log.Error, 1);
        }
        else
        {
          KeyValue.Set("PensonMainRecycleIntervalNonBizDay", "6:00", dbCn);
          hours = 6;
          minutes = 0;
          timeSpan = new TimeSpan (hours, minutes, 0);
          Log.Write("PensonMainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [PensonMain.RecycleInterval]", Log.Error, 1);
        }
      }

      Log.Write("PensonMain will recycle in " + hours + " hours, " + minutes + " minutes. [PensonMain.RecycleInterval]", 2);
      return timeSpan;
    }

    public static string BizDate
    {
      get
      {
        return bizDate;
      }
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
