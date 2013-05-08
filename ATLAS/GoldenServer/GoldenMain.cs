using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using StockLoan.Common;

namespace StockLoan.Golden
{
  class GoldenMain
  {

    private string countryCode;

    private string dbCnStr;
    private SqlConnection dbCn = null;

    private Thread mainThread = null;

    private static bool isStopped = true;
    private static string tempPath;

    public GoldenMain(string dbCnStr)
    {
      this.dbCnStr = dbCnStr;

      try
      {
        dbCn = new SqlConnection(dbCnStr);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [GoldenMain.GoldenMain]", Log.Error, 1);
      }

      countryCode = Standard.ConfigValue("CountryCode", "US");
      Log.Write("Using country code: " + countryCode + " [GoldenMain.GoldenMain]", 2);

      if (Standard.ConfigValueExists("TempPath"))
      {
        tempPath = Standard.ConfigValue("TempPath");

        if (!Directory.Exists(tempPath))
        {
          Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [GoldenMain.GoldenMain]", Log.Error, 1);
          tempPath = Directory.GetCurrentDirectory();
        }
      }
      else
      {
        Log.Write("A configuration value for TempPath has not been provided. [GoldenMain.GoldenMain]", Log.Information, 1);
        tempPath = Directory.GetCurrentDirectory();
      }

      Log.Write("Temporary files will be staged at " + tempPath + ". [GoldenMain.GoldenMain]", 2);
    }

    ~GoldenMain()
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
        mainThread = new Thread(new ThreadStart(GoldenMainLoop));
        mainThread.Name = "Main";
        mainThread.Start();

        Log.Write("Start command issued with new main thread. [GoldenMain.Start]", 3);
      }
      else // Old thread will be just fine.
      {
        Log.Write("Start command issued with main thread already running. [GoldenMain.Start]", 3);
      }
    }

    public void Stop()
    {
      isStopped = true;

      if (mainThread == null)
      {
        Log.Write("Stop command issued, main thread never started. [GoldenMain.Stop]", 3);
      }
      else if (mainThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
      {
        mainThread.Abort();
        Log.Write("Stop command issued, sleeping main thread aborted. [GoldenMain.Stop]", 3);
      }
      else
      {
        Log.Write("Stop command issued, main thread is still active. [GoldenMain.Stop]", 3);
      }
    }

    private void GoldenMainLoop()
    {
      while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
      {
        Log.Write("Start of cycle. [GoldenMain.GoldenMainLoop]", 2);
        KeyValue.Set("GoldenMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);

        Master.BizDate = KeyValue.Get("BizDate", "2001-01-01", dbCnStr);
        if (isStopped) break;

        KeyValue.Set("GoldenMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        Log.Write("End of cycle. [GoldenMain.GoldenMainLoop]", 2);

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

      //DChen: Original code:  Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Any, dbCn);
      // Restore original code and modify any code which still uses @AnyDate in the future when we update Sendero database.
      // After we consolidated Common project with new Loanstart and Northstar, and updated Snedero database.
      // As of Jan.2009, Common project differs between old Sendero and new Loanstar. it affected NorthStar project
      // in Standard.IsBizDate function where old version used @AnyDate and new version used @HolidayDate parameter
      // Matt decided to include new common in Northstar and comment out the single line where IsBizDate is called, below
      // We should revisit this code and restore it to use IsBizDate when Sendero database is updated. 
      bool isBizDay = true; //DChen: Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Any, dbCn);

      TimeSpan timeSpan;

      char[] delimiter = new char[1];
      delimiter[0] = ':';

      if (isBizDay)
      {
        recycleInterval = KeyValue.Get("GoldenMainLoopRecycleIntervalBizDay", "0:15", dbCn);
      }
      else
      {
        recycleInterval = KeyValue.Get("GoldenMainLoopRecycleIntervalNonBizDay", "4:00", dbCn);
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
          KeyValue.Set("GoldenMainRecycleIntervalBizDay", "0:15", dbCn);
          hours = 0;
          minutes = 30;
          timeSpan = new TimeSpan(hours, minutes, 0);
          Log.Write("GoldenMainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [GoldenMain.RecycleInterval]", Log.Error, 1);
        }
        else
        {
          KeyValue.Set("GoldenMainRecycleIntervalNonBizDay", "4:00", dbCn);
          hours = 6;
          minutes = 0;
          timeSpan = new TimeSpan(hours, minutes, 0);
          Log.Write("GoldenMainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [GoldenMain.RecycleInterval]", Log.Error, 1);
        }
      }

      Log.Write("GoldenMain will recycle in " + hours + " hours, " + minutes + " minutes. [GoldenMain.RecycleInterval]", 2);
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