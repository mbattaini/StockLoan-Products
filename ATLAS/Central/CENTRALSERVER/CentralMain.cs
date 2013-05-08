using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using System.Globalization;
using StockLoan.Common;

namespace StockLoan.Central
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
                Master.ContractsBizDate = KeyValue.Get("ContractsBizDate", "0001-01-01", dbCn);

                AztecInventoryLoad();                
                if (isStopped) break;

                KeyValue.Set("CentralMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat, CultureInfo.CurrentCulture), dbCn);
                Log.Write("End of cycle. [CentralMain.CentralMainLoop]", 2);

                if (!isStopped)
                {
                    Thread.Sleep(RecycleInterval());    
                }
            }
        }

        private void AztecInventoryLoad()
        {
            if (!Master.BizDate.Equals(Master.ContractsBizDate))
            {
                Log.Write("Must wait until current buisness date. [CentralMain.AztecInventoryLoad]", 1);
                return;
            }

            if (!KeyValue.Get("AztecFileLoadDate", "", dbCnstr).Equals(Master.ContractsBizDate))
            {
                try
                {
                    AztecInventoryFile aztecFile = new AztecInventoryFile(dbCnstr);
                    aztecFile.Load();

                    KeyValue.Set("AztecFileLoadDate", Master.ContractsBizDate, dbCnstr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [CentralMain.AztecInventoryLoad]", 1);
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

