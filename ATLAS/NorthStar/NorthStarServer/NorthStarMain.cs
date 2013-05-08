using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using StockLoan.Common;

namespace StockLoan.NorthStar
{
  class NorthStarMain
  {

    private string countryCode;

    private string dbCnStr;
    private SqlConnection dbCn = null;

    private Thread mainThread = null;

    private static bool isStopped = true;
    private static string tempPath;

    public NorthStarMain(string dbCnStr)
    {
      this.dbCnStr = dbCnStr;

      try
      {
        dbCn = new SqlConnection(dbCnStr);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [NorthStarMain.NorthStarMain]", Log.Error, 1);
      }

      countryCode = Standard.ConfigValue("CountryCode", "US");
      Log.Write("Using country code: " + countryCode + " [NorthStarMain.NorthStarMain]", 2);

      if (Standard.ConfigValueExists("TempPath"))
      {
        tempPath = Standard.ConfigValue("TempPath");

        if (!Directory.Exists(tempPath))
        {
          Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [NorthStarMain.NorthStarMain]", Log.Error, 1);
          tempPath = Directory.GetCurrentDirectory();
        }
      }
      else
      {
        Log.Write("A configuration value for TempPath has not been provided. [NorthStarMain.NorthStarMain]", Log.Information, 1);
        tempPath = Directory.GetCurrentDirectory();
      }

      Log.Write("Temporary files will be staged at " + tempPath + ". [NorthStarMain.NorthStarMain]", 2);
    }

    ~NorthStarMain()
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
        mainThread = new Thread(new ThreadStart(NorthStarMainLoop));
        mainThread.Name = "Main";
        mainThread.Start();

        Log.Write("Start command issued with new main thread. [NorthStarMain.Start]", 3);
      }
      else // Old thread will be just fine.
      {
        Log.Write("Start command issued with main thread already running. [NorthStarMain.Start]", 3);
      }
    }

    public void Stop()
    {
      isStopped = true;

      if (mainThread == null)
      {
        Log.Write("Stop command issued, main thread never started. [NorthStarMain.Stop]", 3);
      }
      else if (mainThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
      {
        mainThread.Abort();
        Log.Write("Stop command issued, sleeping main thread aborted. [NorthStarMain.Stop]", 3);
      }
      else
      {
        Log.Write("Stop command issued, main thread is still active. [NorthStarMain.Stop]", 3);
      }
    }

    private void NorthStarMainLoop()
    {
      while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
      {
        Log.Write("Start of cycle. [NorthStarMain.NorthStarMainLoop]", 2);
        KeyValue.Set("NorthStarMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);

        Master.BizDate = KeyValue.Get("BizDate", "2001-01-01", dbCnStr);
        if (isStopped) break;

        KeyValue.Set("NorthStarMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        Log.Write("End of cycle. [NorthStarMain.NorthStarMainLoop]", 2);

        if (!isStopped)
        {
          Thread.Sleep(RecycleInterval());
        }
      }
    }



    private TimeSpan RecycleInterval()
    {
      string recycleInterval;
      string[] values;

      int hours;
      int minutes;

      bool isBizDay = Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Any, dbCn);
      TimeSpan timeSpan;

      char[] delimiter = new char[1];
      delimiter[0] = ':';

      if (isBizDay)
      {
        recycleInterval = KeyValue.Get("NorthStarMainLoopRecycleIntervalBizDay", "0:15", dbCn);
      }
      else
      {
        recycleInterval = KeyValue.Get("NorthStarMainLoopRecycleIntervalNonBizDay", "4:00", dbCn);
      }

      try
      {
        values = recycleInterval.Split(delimiter, 2);
        hours = int.Parse(values[0]);
        minutes = int.Parse(values[1]);
        timeSpan = new TimeSpan(hours, minutes, 0);
      }
      catch
      {
        if (isBizDay)
        {
          KeyValue.Set("NorthStarMainRecycleIntervalBizDay", "0:15", dbCn);
          hours = 0;
          minutes = 30;
          timeSpan = new TimeSpan(hours, minutes, 0);
          Log.Write("NorthStarMainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [NorthStarMain.RecycleInterval]", Log.Error, 1);
        }
        else
        {
          KeyValue.Set("NorthStarMainRecycleIntervalNonBizDay", "4:00", dbCn);
          hours = 6;
          minutes = 0;
          timeSpan = new TimeSpan(hours, minutes, 0);
          Log.Write("NorthStarMainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [NorthStarMain.RecycleInterval]", Log.Error, 1);
        }
      }

      Log.Write("NorthStarMain will recycle in " + hours + " hours, " + minutes + " minutes. [NorthStarMain.RecycleInterval]", 2);
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