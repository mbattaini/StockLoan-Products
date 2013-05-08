using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;

using StockLoan.Common;

namespace StockLoan.XflNotification
{
  public class XflNotificationMain
  {
    private string notificationList;
    private string emailFrom;
    private string dbCnStr;
    private SqlConnection dbCn = null;

    private Thread workerThread = null;
    private static bool isStopped = true;
    private static string tempPath;

    private Email email;

    public XflNotificationMain(string dbCnStr)
    {
      this.dbCnStr = dbCnStr;

      try
      {
        dbCn = new SqlConnection(dbCnStr);
        email = new Email(dbCnStr);

        emailFrom = KeyValue.Get("XflNotificationEmailFrom", "stockloan@penson.com", dbCnStr);
        notificationList = KeyValue.Get("XflNotificationEmailTo", "support.stockloanpenson.com@penson.com", dbCnStr);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + "[XflNotificationMain.XflNotificationMain]", Log.Error, 1);
      }

      if (Standard.ConfigValueExists("TempPath"))
      {
        tempPath = Standard.ConfigValue("TempPath");

        if (!Directory.Exists(tempPath))
        {
          Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [XflNotificationMain.XflNotificationMain]", Log.Error, 1);
          tempPath = Directory.GetCurrentDirectory();
        }
      }
      else
      {
        Log.Write("A configuration value for TempPath has not been provided. [XflNotificationMain.XflNotificationMain]", Log.Information, 1);
        tempPath = Directory.GetCurrentDirectory();
      }

      Log.Write("Temporary files will be staged at " + tempPath + ". [XflNotificationMain.XflNotificationMain]", 2);
    }

    ~XflNotificationMain()
    {
      if (dbCn != null)
      {
        dbCn.Dispose();
      }
    }

    public void Start()
    {
      isStopped = false;

      if ((workerThread == null) || (!workerThread.IsAlive)) // Must create new thread.
      {
        workerThread = new Thread(new ThreadStart(XflNotificationMainLoop));
        workerThread.Name = "Worker";
        workerThread.Start();

        Log.Write("Start command issued with new worker thread. [XflNotificationMain.Start]", 2);
      }
      else // Old thread will be just fine.
      {
        Log.Write("Start command issued with worker thread already running. [XflNotificationMain.Start]", 2);
      }
    }

    public void Stop()
    {
      isStopped = true;

      if (workerThread == null)
      {
        Log.Write("Stop command issued, worker thread never started. [XflNotificationMain.Stop]", 2);
      }
      else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
      {
        workerThread.Abort();
        Log.Write("Stop command issued, sleeping worker thread aborted. [XflNotificationMain.Stop]", 2);
      }
      else
      {
        Log.Write("Stop command issued, worker thread is still active. [XflNotificationMain.Stop]", 2);
      }
    }

    private void XflNotificationMainLoop()
    {
      while (!isStopped)
      {
        Log.Write("Start of cycle. [XflNotificationMain.XflNotificationMainLoop]", 2);
        KeyValue.Set("XflNotificationMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);

        Master.BizDate = KeyValue.Get("BizDate", "2001-01-01", dbCn);
        Master.BizDatePrior = KeyValue.Get("BizDatePrior", "2001-01-01", dbCn);

        XflLoaderTransactionCheck();
        if (isStopped) break;

        XflShortInterestImportCheck();
        if (isStopped) break;

        KeyValue.Set("XflNotificationMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        Log.Write("End of cycle. [XflNotificationMain.XflNotificationMainLoop]", 2);

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

      bool isBizDay = (DateTime.UtcNow.DayOfWeek != DayOfWeek.Saturday) && (DateTime.UtcNow.DayOfWeek != DayOfWeek.Sunday);
      TimeSpan timeSpan;

      char[] delimiter = new char[1];
      delimiter[0] = ':';

      if (isBizDay)
      {
        recycleInterval = KeyValue.Get("XflNotificationMainRecycleIntervalBizDay", "0:15", dbCn);
      }
      else
      {
        recycleInterval = KeyValue.Get("XflNotificationMainRecycleIntervalNonBizDay", "6:00", dbCn);
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
          KeyValue.Set("XflNotificationMainRecycleIntervalBizDay", "0:15", dbCn);
          hours = 0;
          minutes = 20;
          timeSpan = new TimeSpan(hours, minutes, 0);
          Log.Write("MainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [XflNotificationMain.RecycleInterval]", Log.Error, 1);
        }
        else
        {
          KeyValue.Set("XflNotificationMainRecycleIntervalNonBizDay", "6:00", dbCn);
          hours = 6;
          minutes = 0;
          timeSpan = new TimeSpan(hours, minutes, 0);
          Log.Write("MainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [XflNotificationMain.RecycleInterval]", Log.Error, 1);
        }
      }

      Log.Write("XflNotificationMain will recycle in " + hours + " hours, " + minutes + " minutes. [XflNotificationMain.RecycleInterval]", 2);
      return timeSpan;
    }


    private void XflLoaderTransactionCheck()
    {
      string xflLoaderTransactionCheckTime = KeyValue.Get("XflNotificationLoaderTransactionCheckTime", "10:30", dbCnStr);

      if (DateTime.Now.ToString("yyyy-MM-dd").Equals(Master.BizDate))
      {
        try
        {
          if (KeyValue.Get("XflLoaderTransactionDate", "2001-01-01", dbCnStr).Equals(Master.BizDate))
          {
            if (!KeyValue.Get("XflNotificationLoaderTransaction[0]", "2001-01-01", dbCn).Equals(Master.BizDate))
            {
              Log.Write(Standard.ConfigValue("LoaderTransaction[0]") + " [XflNotificationMain.XflLoaderTransactionCheck]", Log.Information, 1);
              email.Send(notificationList, emailFrom, "Xfl Notification Message - Success", Standard.ConfigValue("LoaderTransaction[0]"));
              KeyValue.Set("XflNotificationLoaderTransaction[0]", Master.BizDate, dbCn);
            }
            else
            {
              Log.Write("Success email already sent. [XflNotificationMain.XflLoaderTransactionCheck]", 1);
            }
          }
          else if (xflLoaderTransactionCheckTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0)
          {
            if (!KeyValue.Get("XflNotificationLoaderTransaction[1]", "2001-01-01", dbCn).Equals(Master.BizDate))
            {
              Log.Write(Standard.ConfigValue("LoaderTransaction[1]") + " [XflNotificationMain.XflLoaderTransactionCheck]", Log.Information, 1);
              email.Send(notificationList, emailFrom, "Xfl Notification Message - Error", Standard.ConfigValue("LoaderTransaction[1]"));
              KeyValue.Set("XflNotificationLoaderTransaction[1]", Master.BizDate, dbCn);
            }
            else
            {
              Log.Write("Error email already sent. [XflNotificationMain.XflLoaderTransactionCheck]", 1);
            }
          }
          else
          {
            Log.Write("Must wait until after " + xflLoaderTransactionCheckTime + " UTC to check XFL Loader Transaction status. [XflNotificationMain.XflLoaderTransactionCheck]", 1);
          }
        }
        catch (Exception error)
        {
          Log.Write(error.Message + " [XflNotificationMain.XflLoaderTransactionCheck]", 1);
        }
      }
      else
      {
        Log.Write("Wait until current date to run, " + Master.BizDate + ". [XflNotificationMain.XflLoaderTransactionCheck]", 1);
      }
    }

    private void XflShortInterestImportCheck()
    {
      string xflShortInterestImportCheckTime = KeyValue.Get("XflNotificationShortInterestImportCheckTime", "11:00", dbCnStr);

      if (DateTime.Now.ToString("yyyy-MM-dd").Equals(Master.BizDate))
      {
        try
        {
          if (KeyValue.Get("XflShortInterestDataDate", "2001-01-01", dbCnStr).Equals(Master.BizDatePrior) &&
            KeyValue.Get("XflShortInterestImportDate", "2001-01-01", dbCnStr).Equals(Master.BizDate))
          {
            if (!KeyValue.Get("XflNotificationShortInterestImport[0]", "2001-01-01", dbCn).Equals(Master.BizDate))
            {
              Log.Write(Standard.ConfigValue("ShortInterestImport[0]") + " [XflNotificationMain.XflShortInterestImportCheck]", Log.Information, 1);
              email.Send(notificationList, emailFrom, "Xfl Notification Message - Success", Standard.ConfigValue("ShortInterestImport[0]"));
              KeyValue.Set("XflNotificationShortInterestImport[0]", Master.BizDate, dbCn);
            }
            else
            {
              Log.Write("Success email already sent. [XflNotificationMain.XflShortInterestImportCheck]", 1);
            }
          }
          else if (xflShortInterestImportCheckTime.CompareTo(DateTime.UtcNow.ToString("HH:mm"))<0)
          {
            if (!KeyValue.Get("XflNotificationShortInterestImport[1]", "2001-01-01", dbCn).Equals(Master.BizDate))
            {
              Log.Write(Standard.ConfigValue("ShortInterestImport[1]") + " [XflNotificationMain.XflShortInterestImportCheck]", Log.Information, 1);
              email.Send(notificationList, emailFrom, "Xfl Notification Message - Error", Standard.ConfigValue("ShortInterestImport[1]"));
              KeyValue.Set("XflNotificationShortInterestImport[1]", Master.BizDate, dbCn);
            }
            else
            {
              Log.Write("Error email already sent. [XflNotificationMain.XflShortInterestImportCheck]", 1);
            }
          }
          else
          {
            Log.Write("Must wait until after " + xflShortInterestImportCheckTime + " UTC to check XFL Short Interest Import Status. [XflNotificationMain.XflShortInterestImportCheck]", 1);
          }
        }
        catch (Exception error)
        {
          Log.Write(error.Message + " [XflNotificationMain.XflShortInterestImportCheck]", 1);
        }
      }
      else
      {
        Log.Write("Wait until current date to run, " + Master.BizDate + ". [XflNotificationMain.XflShortInterestImportCheck]", 1);
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
