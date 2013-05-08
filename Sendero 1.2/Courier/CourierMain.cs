// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Anetics.Common;

namespace Anetics.Courier
{
	public class CourierMain
	{
    private string dbCnStr;
    private SqlConnection dbCn = null;

    private Thread workerThread = null;
    private static bool isStopped = true;
    private static string tempPath;
    
    public CourierMain(string dbCnStr)
    {
      this.dbCnStr = dbCnStr;

      try
      {
        dbCn = new SqlConnection(dbCnStr);
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [CourierMain.CourierMain]", Log.Error, 1);
      }

      if (Standard.ConfigValueExists("TempPath"))
      {
        tempPath = Standard.ConfigValue("TempPath");

        if (!Directory.Exists(tempPath))
        {
          Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [CourierMain.CourierMain]", Log.Error, 1);
          tempPath = Directory.GetCurrentDirectory();
        }
      }
      else
      {
        Log.Write("A configuration value for TempPath has not been provided. [CourierMain.CourierMain]", Log.Information, 1);
        tempPath = Directory.GetCurrentDirectory();
      }

      Log.Write("Temporary files will be staged at " + tempPath + ". [CourierMain.CourierMain]", 2);
    }

    ~CourierMain()
    {
      if (dbCn != null)
      {
        dbCn.Dispose();
      }
    }

    public void Start()
    {
      isStopped = false;

      if ((workerThread == null)||(!workerThread.IsAlive)) // Must create new thread.
      {
        workerThread = new Thread(new ThreadStart(CourierMainLoop));
        workerThread.Name = "Worker";
        workerThread.Start();

        Log.Write("Start command issued with new worker thread. [CourierMain.Start]", 2);
      }
      else // Old thread will be just fine.
      {
        Log.Write("Start command issued with worker thread already running. [CourierMain.Start]", 2);
      }
    }

    public void Stop()
    {
      isStopped = true;

      if (workerThread == null)
      {
        Log.Write("Stop command issued, worker thread never started. [CourierMain.Stop]", 2);
      }
      else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
      {
        workerThread.Abort();
        Log.Write("Stop command issued, sleeping worker thread aborted. [CourierMain.Stop]", 2);
      }
      else
      {
        Log.Write("Stop command issued, worker thread is still active. [CourierMain.Stop]", 2);
      }
    }

    private void CourierMainLoop()
    {
      while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
      {
        Log.Write("Start-of-cycle. [CourierMain.CourierMainLoop]", 2);
        KeyValue.Set("CourierMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        
        Master.BizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);
        Master.BizDatePriorBank = KeyValue.Get("BizDatePriorBank", "0001-01-01", dbCn);
        
        // Update threshold list.
        ThresholdListNsdq();        
        if (isStopped) break;
        ThresholdListNyse();        
        if (isStopped) break;
        ThresholdListAmse();        
        if (isStopped) break;
        ThresholdListChse();        
        if (isStopped) break;
        ThresholdListArca();        
        if (isStopped) break;

        // Get inventory.
        SubscribeInventory();
        if (isStopped) break;

        KeyValue.Set("CourierMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        Log.Write("End-of-cycle. [CourierMain.CourierMainLoop]", 2);

        if (!isStopped)
        {
          Thread.Sleep(RecycleInterval());
        }
      }
    }

    private void ThresholdListNsdq()
    {
      Threshold threshold;
      string status = "OK";
 
      if (KeyValue.Get("ThresholdListBizDateNsdq", "0001-01-01", dbCn).Equals(Master.BizDatePriorBank)) // Already done for today.
      {
        Log.Write("Threshold list from NSDQ is current for " + Master.BizDatePriorBank + ". [CourierMain.ThresholdListNsdq]", 2);
      }
      else
      {
        Log.Write("Will try to set threshold list from NSDQ for " + Master.BizDatePriorBank + ". [CourierMain.ThresholdListNsdq]", 2);

        try
        {
          threshold = new Threshold(dbCn);
          threshold.ListGet(
            Master.BizDatePriorBank,
            "NSDQ",
            KeyValue.Get("ThresholdFileHostNsdq", "ftp.nasdaqtrader.com", dbCn),
            DatePartSet(KeyValue.Get("ThresholdFilePathNameNsdq", "/symboldirectory/regsho/nasdaqth{yyyyMMdd}.txt", dbCn), Master.BizDatePriorBank)
            );

          KeyValue.Set("ThresholdListFileTimeStampNsdq", threshold.FileTimeStamp, dbCn);
          KeyValue.Set("ThresholdListItemCountNsdq", threshold.ItemCount.ToString(), dbCn);
          KeyValue.Set("ThresholdListBizDateNsdq", Master.BizDatePriorBank, dbCn);

          Log.Write("Set " +  threshold.ItemCount.ToString("#,##0") + " securities on the threshold list for NSDQ. [CourierMain.ThresholdListNsdq]", 2);
        }
        catch (Exception e)
        {
          status = e.Message;
          Log.Write(e.Message + " [CourierMain.ThresholdListNsdq]", Log.Error, 1);
        }

        KeyValue.Set("ThresholdListStatusNsdq", status, dbCn);
      }
    }

    private void ThresholdListNyse()
    {
      Threshold threshold;
      string status = "OK";
 
      if (KeyValue.Get("ThresholdListBizDateNyse", "0001-01-01", dbCn).Equals(Master.BizDatePriorBank)) // Already done for today.
      {
        Log.Write("Threshold list from NYSE is current for " + Master.BizDatePriorBank + ". [CourierMain.ThresholdListNyse]", 2);
      }
      else
      {
        Log.Write("Will try to set threshold list from NYSE for " + Master.BizDatePriorBank + ". [CourierMain.ThresholdListNyse]", 2);

        try
        {
          threshold = new Threshold(dbCn);
          threshold.ListGet(
            Master.BizDatePriorBank,
            "NYSE",
            KeyValue.Get("ThresholdFileHostNyse", "www.nyse.com", dbCn),
            DatePartSet(KeyValue.Get("ThresholdFilePathNameNyse", "/threshold/NYSEth{yyyyMMdd}.txt", dbCn), Master.BizDatePriorBank)
            );

          KeyValue.Set("ThresholdListFileTimeStampNyse", threshold.FileTimeStamp, dbCn);
          KeyValue.Set("ThresholdListItemCountNyse", threshold.ItemCount.ToString(), dbCn);
          KeyValue.Set("ThresholdListBizDateNyse", Master.BizDatePriorBank, dbCn);

          Log.Write("Set " +  threshold.ItemCount.ToString("#,##0") + " securities on the threshold list for NYSE. [CourierMain.ThresholdListNyse]", 2);
        }
        catch (Exception e)
        {
          status = e.Message;
          Log.Write(e.Message + " [CourierMain.ThresholdListNyse]", Log.Error, 1);
        }

        KeyValue.Set("ThresholdListStatusNyse", status, dbCn);
      }
    }

    private void ThresholdListAmse()
    {
      Threshold threshold;
      string status = "OK";
 
      if (KeyValue.Get("ThresholdListBizDateAmse", "0001-01-01", dbCn).Equals(Master.BizDatePriorBank)) // Already done for today.
      {
        Log.Write("Threshold list from AMSE is current for " + Master.BizDatePriorBank + ". [CourierMain.ThresholdListAmse]", 2);
      }
      else
      {
        Log.Write("Will try to set threshold list from AMSE for " + Master.BizDatePriorBank + ". [CourierMain.ThresholdListAmse]", 2);

        try
        {
          threshold = new Threshold(dbCn);
          threshold.ListGet(
            Master.BizDatePriorBank,
            "AMSE",
            KeyValue.Get("ThresholdFileHostAmse", "ftp.amex.com", dbCn),
            DatePartSet(KeyValue.Get("ThresholdFilePathNameAmse", "/amextrader/tradingData/data/RegSHO/Daily/AMEXTH{yyyyMMdd}.txt", dbCn), Master.BizDatePriorBank)
            );

          KeyValue.Set("ThresholdListFileTimeStampAmse", threshold.FileTimeStamp, dbCn);
          KeyValue.Set("ThresholdListItemCountAmse", threshold.ItemCount.ToString(), dbCn);
          KeyValue.Set("ThresholdListBizDateAmse", Master.BizDatePriorBank, dbCn);

          Log.Write("Set " +  threshold.ItemCount.ToString("#,##0") + " securities on the threshold list for AMSE. [CourierMain.ThresholdListAmse]", 2);
        }
        catch (Exception e)
        {
          status = e.Message;
          Log.Write(e.Message + " [CourierMain.ThresholdListAmse]", Log.Error, 1);
        }

        KeyValue.Set("ThresholdListStatusAmse", status, dbCn);
      }
    }

    private void ThresholdListChse()
    {
      Threshold threshold;
      string status = "OK";
 
      if (KeyValue.Get("ThresholdListBizDateChse", "0001-01-01", dbCn).Equals(Master.BizDatePriorBank)) // Already done for today.
      {
        Log.Write("Threshold list from CHSE is current for " + Master.BizDatePriorBank + ". [CourierMain.ThresholdListChse]", 2);
      }
      else
      {
        Log.Write("Will try to set threshold list from CHSE for " + Master.BizDatePriorBank + ". [CourierMain.ThresholdListChse]", 2);

        try
        {
          threshold = new Threshold(dbCn);
          threshold.ListGet(
            Master.BizDatePriorBank,
            "CHSE",
            KeyValue.Get("ThresholdFileHostChse", "ftp3.chx.com", dbCn),
            DatePartSet(KeyValue.Get("ThresholdFilePathNameChse", "/regsho/CHXth{yyyyMMdd}.txt", dbCn), Master.BizDatePriorBank)
            );

          KeyValue.Set("ThresholdListFileTimeStampChse", threshold.FileTimeStamp, dbCn);
          KeyValue.Set("ThresholdListItemCountChse", threshold.ItemCount.ToString(), dbCn);
          KeyValue.Set("ThresholdListBizDateChse", Master.BizDatePriorBank, dbCn);

          Log.Write("Set " +  threshold.ItemCount.ToString("#,##0") + " securities on the threshold list for CHSE. [CourierMain.ThresholdListChse]", 2);
        }
        catch (Exception e)
        {
          status = e.Message;
          Log.Write(e.Message + " [CourierMain.ThresholdListChse]", Log.Error, 1);
        }

        KeyValue.Set("ThresholdListStatusChse", status, dbCn);
      }
    }

    private void ThresholdListArca()
    {
      Threshold threshold;
      string status = "OK";
 
      if (KeyValue.Get("ThresholdListBizDateArca", "0001-01-01", dbCn).Equals(Master.BizDatePriorBank)) // Already done for today.
      {
        Log.Write("Threshold list from ARCA is current for " + Master.BizDatePriorBank + ". [CourierMain.ThresholdListArca]", 2);
      }
      else
      {
        Log.Write("Will try to set threshold list from ARCA for " + Master.BizDatePriorBank + ". [CourierMain.ThresholdListArca]", 2);

        try
        {
          threshold = new Threshold(dbCn);
          threshold.ListGet(
            Master.BizDatePriorBank,
            "ARCA",
            KeyValue.Get("ThresholdFileHostArca", "www.archipelago.com", dbCn),
            DatePartSet(KeyValue.Get("ThresholdFilePathNameArca", "/traders/SECreg/SHO/ARCAEXth{yyyyMMdd}.txt", dbCn), Master.BizDatePriorBank)
            );

          KeyValue.Set("ThresholdListFileTimeStampArca", threshold.FileTimeStamp, dbCn);
          KeyValue.Set("ThresholdListItemCountArca", threshold.ItemCount.ToString(), dbCn);
          KeyValue.Set("ThresholdListBizDateArca", Master.BizDatePriorBank, dbCn);

          Log.Write("Set " +  threshold.ItemCount.ToString("#,##0") + " securities on the threshold list for ARCA. [CourierMain.ThresholdListArca]", 2);
        }
        catch (Exception e)
        {
          status = e.Message;
          Log.Write(e.Message + " [CourierMain.ThresholdListArca]", Log.Error, 1);
        }

        KeyValue.Set("ThresholdListStatusArca", status, dbCn);
      }
    }

    private void SubscribeInventory()
    {
      Subscriber subscriber;
      
      Log.Write("Initializing the subscriber. [CourierMain.SubscribeInventory]", 2);

      try
      {
        subscriber = new Subscriber(dbCnStr, dbCn);
        subscriber.SubscribeInventory();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [CourierMain.SubscribeInventory]", Log.Error, 1);
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
        recycleInterval = KeyValue.Get("CourierMainRecycleIntervalBizDay", "0:20", dbCn);
      }
      else
      {
        recycleInterval = KeyValue.Get("CourierMainRecycleIntervalNonBizDay", "6:00", dbCn);
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
          KeyValue.Set("CourierMainRecycleIntervalBizDay", "0:20", dbCn);
          hours = 0;
          minutes = 20;
          timeSpan = new TimeSpan (hours, minutes, 0);
          Log.Write("MainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [CourierMain.RecycleInterval]", Log.Error, 1);
        }
        else
        {
          KeyValue.Set("CourierMainRecycleIntervalNonBizDay", "6:00", dbCn);
          hours = 6;
          minutes = 0;
          timeSpan = new TimeSpan (hours, minutes, 0);
          Log.Write("MainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [CourierMain.RecycleInterval]", Log.Error, 1);
        }
      }

      Log.Write("CourierMain will recycle in " + hours + " hours, " + minutes + " minutes. [CourierMain.RecycleInterval]", 2);
      return timeSpan;
    }

    /// <summary>
    /// Converts a date format string when found embedded within braces ({}) in inputString to the formatted dateValue.
    /// </summary>
    public static string DatePartSet(string inputString, string dateValue)
    {
      int leftBracketIndex = inputString.IndexOf('{');
      int rightBracketIndex = inputString.IndexOf('}');
            
      if ((leftBracketIndex > -1) && (rightBracketIndex > leftBracketIndex))
      {
        string formatString = inputString.Substring(leftBracketIndex + 1, rightBracketIndex - leftBracketIndex - 1);
 
        return inputString.Substring(0, leftBracketIndex) + Tools.FormatDate(dateValue, formatString) + 
          inputString.Substring(rightBracketIndex + 1, inputString.Length - rightBracketIndex - 1);
      }
      else
      {
        return inputString;
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
