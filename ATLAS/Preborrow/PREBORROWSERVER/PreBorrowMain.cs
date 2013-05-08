using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using System.Globalization;
using StockLoan.Common;

namespace StockLoan.PreBorrow
{
  public class CentralMain
  {
    private string dbCnstr;
    private SqlConnection dbCn;

    private static string countryCode;
    private static string tempPath;

    private Thread mainThread;

    private static bool isStopped = true;

    public CentralMain(string dbCnstr)
    {
      this.dbCnstr = dbCnstr;

      try
      {
        countryCode = "US";
        dbCn = new SqlConnection(dbCnstr);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [CentralMain.CentralMain]", Log.Error, 1);
      }

      if (Standard.ConfigValueExists("TempPath"))
      {
        tempPath = Standard.ConfigValue("TempPath");

        if (!Directory.Exists(tempPath))
        {
          Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [CentralMain.CentralMain]", Log.Error, 1);
          tempPath = Directory.GetCurrentDirectory();
        }
      }
      else
      {
        Log.Write("A configuration value for TempPath has not been provided. [CentralMain.CentralMain]", Log.Information, 1);
        tempPath = Directory.GetCurrentDirectory();
      }

      Log.Write("Temporary files will be staged at " + tempPath + ". [CentralMain.CentralMain]", 2);
    }

    ~CentralMain()
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
        mainThread = new Thread(new ThreadStart(CentralMainLoop));
        mainThread.Name = "Main";
        mainThread.Start();

        Log.Write("Start command issued with new main thread. [CentralMain.Start]", 3);
      }
      else // Old thread will be just fine.
      {
        Log.Write("Start command issued with main thread already running. [CentralMain.Start]", 3);
      }
    }

    public void Stop()
    {
      isStopped = true;

      if (mainThread == null)
      {
        Log.Write("Stop command issued, main thread never started. [CentralMain.Stop]", 3);
      }
      else if (mainThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
      {
        mainThread.Abort();
        Log.Write("Stop command issued, sleeping main thread aborted. [CentralMain.Stop]", 3);
      }
      else
      {
        Log.Write("Stop command issued, main thread is still active. [CentralMain.Stop]", 3);
      }
    }

    private void CentralMainLoop()
    {
      while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
      {
        Log.Write("Start of cycle. [CentralMain.CentralMainLoop]", 2);
        KeyValue.Set("CentralMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat, CultureInfo.CurrentCulture), dbCn);

        Master.BizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);
        Master.BizDatePrior = KeyValue.Get("BizDatePrior", "0001-01-01", dbCn);
        Master.BizDateExchange = KeyValue.Get("BizDateExchange", "0001-01-01", dbCn);        
        Master.ContractsBizDate = KeyValue.Get("ContractsBizDate", "0001-01-01", dbCn);

        PreBorrowWeekendSnapShot();
        if (isStopped) break;

        
        PreBorrowContractAllocation();
        if (isStopped) break;

        KeyValue.Set("CentralMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat, CultureInfo.CurrentCulture), dbCn);
        Log.Write("End of cycle. [CentralMain.CentralMainLoop]", 2);

        if (!isStopped)
        {
          Thread.Sleep(RecycleInterval());
        }
      }
    }


    private void PreBorrowContractAllocation()
    {
      SqlCommand sqlCommand;
      Log.Write("Doing Pre Borrow Allocationt for " + DateTime.Now.ToString("HH:mm.ss") + ". [CentralMain.PreBorrowContractAllocation]", 2);

      if (!Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Bank, dbCn))
      {
        Log.Write("Wait until BizDate to run pre borrow allocation. [CentralMain.PreBorrowContractAllocation]", 1);
        return;
      }

      try
      {
        if (KeyValue.Get("PreBorrowBillingSnapShotBizDate", "", dbCnstr).Equals(Master.BizDate))
        {

          sqlCommand = new SqlCommand("spPBIntraDayUpdate", dbCn);
          sqlCommand.CommandType = CommandType.StoredProcedure;

          SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
          paramBizDate.Value = Master.BizDate;

          dbCn.Open();
          sqlCommand.ExecuteNonQuery();
        }
        else
        {
          Log.Write("Wait until preborrow start of day snapshot. [CentralMain.PreBorrowContractAllocation]", 1);
        }
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [CentralMain.PreBorrowContractAllocation]", Log.Error, 1);
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed)
        {
          dbCn.Close();
        }
      }
    }

    private void PreBorrowWeekendSnapShot()
    {
      if (!Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Bank, dbCn))
      {
        if (KeyValue.Get("PreBorrowBillingWeekendSnapShotBizDate", "", dbCn).Equals(DateTime.UtcNow.ToString(Standard.DateFormat)))
        {
          Log.Write("Pre Borrow Billing Weekend Snapshot is current for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [CentralMain.PreBorrowWeekendSnapShot]", 2);
        }
        else // do weekend snapshot
        {
          SqlCommand sqlCommand;

          Log.Write("Doing Pre Borrow Billing Weekend Snapshot for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [CentralMain.PreBorrowWeekendSnapShot]", 2);

          try
          {
            sqlCommand = new SqlCommand("spPBWeekendSnapShot", dbCn);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter paramBizDatePrior = sqlCommand.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
            paramBizDatePrior.Value = Master.BizDatePrior;

            SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
            paramBizDate.Value = DateTime.UtcNow.ToString(Standard.DateFormat);

            dbCn.Open();
            sqlCommand.ExecuteNonQuery();
            dbCn.Close();

            KeyValue.Set("PreBorrowBillingWeekendSnapShotBizDate", DateTime.UtcNow.ToString(Standard.DateFormat), dbCn);
          }
          catch (Exception e)
          {
            Log.Write(e.Message + " [CentralMain.PreBorrowWeekendSnapShot]", Log.Error, 1);
          }
          finally
          {
            if (dbCn.State != ConnectionState.Closed)
            {
              dbCn.Close();
            }
          }
        }
      }
      else
      {
        Log.Write("Must wait until weekend to preform weekend snapshot. [CentralMain.PreBorrowWeekendSnapShot]", 1);
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
        recycleInterval = KeyValue.Get("CentralMainLoopRecycleIntervalBizDay", "0:15", dbCn);
      }
      else
      {
        recycleInterval = KeyValue.Get("CentralMainLoopRecycleIntervalNonBizDay", "4:00", dbCn);
      }

      try
      {
        values = recycleInterval.Split(delimiter, 2);
        hours = int.Parse(values[0], CultureInfo.CurrentCulture);
        minutes = int.Parse(values[1], CultureInfo.CurrentCulture);
        timeSpan = new TimeSpan(hours, minutes, 0);
      }
      catch
      {
        if (isBizDay)
        {
          KeyValue.Set("CentralMainRecycleIntervalBizDay", "0:15", dbCn);
          hours = 0;
          minutes = 30;
          timeSpan = new TimeSpan(hours, minutes, 0);
          Log.Write("CentralMainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [CentralMain.RecycleInterval]", Log.Error, 1);
        }
        else
        {
          KeyValue.Set("CentralMainRecycleIntervalNonBizDay", "4:00", dbCn);
          hours = 6;
          minutes = 0;
          timeSpan = new TimeSpan(hours, minutes, 0);
          Log.Write("CentralMainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [CentralMain.RecycleInterval]", Log.Error, 1);
        }
      }

      Log.Write("CentralMain will recycle in " + hours + " hours, " + minutes + " minutes. [CentralMain.RecycleInterval]", 2);
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

