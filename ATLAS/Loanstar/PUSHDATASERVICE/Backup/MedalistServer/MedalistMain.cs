// Licensed Materials - Property of StockLoan, LLC.
// Copyright (C) StockLoan, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using StockLoan.Common;

namespace StockLoan.Medalist
{
    public class MedalistMain
    {
        private string countryCode;

        private string dbCnStr;
        private SqlConnection dbCn = null;

        private Thread mainThread = null;

        private static bool isStopped = true;
        private static string tempPath;

        private PositionAgent positionAgent;

        public MedalistMain(string dbCnStr)
        {
            this.dbCnStr = dbCnStr;

            try
            {
                dbCn = new SqlConnection(dbCnStr);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [MedalistMain.MedalistMain]", Log.Error, 1);
            }

            countryCode = Standard.ConfigValue("CountryCode", "US");
            Log.Write("Using country code: " + countryCode + " [MedalistMain.MedalistMain]", 2);

            if (Standard.ConfigValueExists("TempPath"))
            {
                tempPath = Standard.ConfigValue("TempPath");

                if (!Directory.Exists(tempPath))
                {
                    Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [MedalistMain.MedalistMain]", Log.Error, 1);
                    tempPath = Directory.GetCurrentDirectory();
                }
            }
            else
            {
                Log.Write("A configuration value for TempPath has not been provided. [MedalistMain.MedalistMain]", Log.Information, 1);
                tempPath = Directory.GetCurrentDirectory();
            }

            Log.Write("Temporary files will be staged at " + tempPath + ". [MedalistMain.MedalistMain]", 2);
        }

        ~MedalistMain()
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
                mainThread = new Thread(new ThreadStart(MedalistMainLoop));
                mainThread.Name = "Main";
                mainThread.Start();

                Log.Write("Start command issued with new main thread. [MedalistMain.Start]", 3);
            }
            else // Old thread will be just fine.
            {
                Log.Write("Start command issued with main thread already running. [MedalistMain.Start]", 3);
            }
        }

        public void Stop()
        {
            isStopped = true;

            if (mainThread == null)
            {
                Log.Write("Stop command issued, main thread never started. [MedalistMain.Stop]", 3);
            }
            else if (mainThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
            {
                mainThread.Abort();
                Log.Write("Stop command issued, sleeping main thread aborted. [MedalistMain.Stop]", 3);
            }
            else
            {
                Log.Write("Stop command issued, main thread is still active. [MedalistMain.Stop]", 3);
            }
        }

        private void MedalistMainLoop()
        {
            while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
            {
                Log.Write("Start of cycle. [MedalistMain.MedalistMainLoop]", 2);
                KeyValue.Set("MedalistMainCycleStartTime", "System", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);

                BizDatesSet(Standard.HolidayType.Any);
                BizDatesSet(Standard.HolidayType.Bank);
                BizDatesSet(Standard.HolidayType.Exchange);

                if (isStopped) break;

                KeyValue.Set("MedalistMainCycleStopTime", "System", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
                Log.Write("End of cycle. [MedalistMain.MedalistMainLoop]", 2);

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
                recycleInterval = KeyValue.Get("MedalistMainLoopRecycleIntervalBizDay", "System", "0:15", dbCn);
            }
            else
            {
                recycleInterval = KeyValue.Get("MedalistMainLoopRecycleIntervalNonBizDay", "System", "4:00", dbCn);
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
                    KeyValue.Set("MedalistMainRecycleIntervalBizDay", "System", "0:15", dbCn);
                    hours = 0;
                    minutes = 30;
                    timeSpan = new TimeSpan(hours, minutes, 0);
                    Log.Write("MedalistMainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [MedalistMain.RecycleInterval]", Log.Error, 1);
                }
                else
                {
                    KeyValue.Set("MedalistMainRecycleIntervalNonBizDay", "System", "4:00", dbCn);
                    hours = 6;
                    minutes = 0;
                    timeSpan = new TimeSpan(hours, minutes, 0);
                    Log.Write("MedalistMainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [MedalistMain.RecycleInterval]", Log.Error, 1);
                }
            }

            Log.Write("MedalistMain will recycle in " + hours + " hours, " + minutes + " minutes. [MedalistMain.RecycleInterval]", 2);
            return timeSpan;
        }

        private void BizDatesSet(Standard.HolidayType holidayType)
        {
            double utcOffset;

            try
            {
                utcOffset = double.Parse(KeyValue.Get("BizDateRollUtcOffsetMinutes", "BuisnessDate", "0", dbCn));
            }
            catch
            {
                Log.Write("Unable to parse BizDateRollUtcOffsetMinutes key value. [MedalistMain.BizDatesSet]", Log.Error, 2);
                return;
            }

            DateTime bizDate;
            DateTime bizDateNext;
            DateTime bizDatePrior;

            bizDate = DateTime.UtcNow.AddMinutes(utcOffset).Date;
            while (!Standard.IsBizDate(bizDate, countryCode, holidayType, dbCn))
            {
                bizDate = bizDate.AddDays(1.0);
            }

            bizDateNext = bizDate.AddDays(1.0);
            while (!Standard.IsBizDate(bizDateNext, countryCode, holidayType, dbCn))
            {
                bizDateNext = bizDateNext.AddDays(1.0);
            }

            bizDatePrior = bizDate.AddDays(-1.0);
            while (!Standard.IsBizDate(bizDatePrior, countryCode, holidayType, dbCn))
            {
                bizDatePrior = bizDatePrior.AddDays(-1.0);
            }

            switch (holidayType)
            {
                case Standard.HolidayType.Any:
                    Master.BizDate = bizDate.ToString(Standard.DateFormat);
                    Master.BizDateNext = bizDateNext.ToString(Standard.DateFormat);
                    Master.BizDatePrior = bizDatePrior.ToString(Standard.DateFormat);

                    if (!KeyValue.Get("BizDate", "BuisnessDate", "2001-01-01", dbCn).Equals(Master.BizDate))
                    {
                        KeyValue.Set("BizDate", "BuisnessDate", Master.BizDate, dbCn);
                        Log.Write("BizDate has been set to: " + Master.BizDate + " [MedalistMain.BizDatesSet]", Log.Information, 2);

                        KeyValue.Set("ContractsBizDate", "BuisnessDate", "2001-01-01", dbCn);
                        Log.Write("ContractsBizDate has been set to: 2001-01-01 [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    if (!KeyValue.Get("BizDateNext", "BuisnessDate", "2001-01-01", dbCn).Equals(Master.BizDateNext))
                    {
                        KeyValue.Set("BizDateNext", "BuisnessDate", Master.BizDateNext, dbCn);
                        Log.Write("BizDateNext has been set to: " + Master.BizDateNext + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    if (!KeyValue.Get("BizDatePrior", "BuisnessDate", "2001-01-01", dbCn).Equals(Master.BizDatePrior))
                    {
                        KeyValue.Set("BizDatePrior", "BuisnessDate", Master.BizDatePrior, dbCn);
                        Log.Write("BizDatePrior has been set to: " + Master.BizDatePrior + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    break;
                case Standard.HolidayType.Bank:
                    Master.BizDateBank = bizDate.ToString(Standard.DateFormat);
                    Master.BizDateNextBank = bizDateNext.ToString(Standard.DateFormat);
                    Master.BizDatePriorBank = bizDatePrior.ToString(Standard.DateFormat);

                    if (!KeyValue.Get("BizDateBank", "BuisnessDate", "2001-01-01", dbCn).Equals(Master.BizDateBank))
                    {
                        KeyValue.Set("BizDateBank", "BuisnessDate", Master.BizDateBank, dbCn);
                        Log.Write("BizDateBank has been set to: " + Master.BizDateBank + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    if (!KeyValue.Get("BizDateNextBank", "BuisnessDate", "2001-01-01", dbCn).Equals(Master.BizDateNextBank))
                    {
                        KeyValue.Set("BizDateNextBank", "BuisnessDate", Master.BizDateNextBank, dbCn);
                        Log.Write("BizDateNextBank has been set to: " + Master.BizDateNextBank + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    if (!KeyValue.Get("BizDatePriorBank", "BuisnessDate", "2001-01-01", dbCn).Equals(Master.BizDatePriorBank))
                    {
                        KeyValue.Set("BizDatePriorBank", "BuisnessDate", Master.BizDatePriorBank, dbCn);
                        Log.Write("BizDatePriorBank has been set to: " + Master.BizDatePriorBank + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    break;
                case Standard.HolidayType.Exchange:
                    Master.BizDateExchange = bizDate.ToString(Standard.DateFormat);
                    Master.BizDateNextExchange = bizDateNext.ToString(Standard.DateFormat);
                    Master.BizDatePriorExchange = bizDatePrior.ToString(Standard.DateFormat);

                    if (!KeyValue.Get("BizDateExchange", "BuisnessDate", "2001-01-01", dbCn).Equals(Master.BizDateExchange))
                    {
                        KeyValue.Set("BizDateExchange", "BuisnessDate", Master.BizDateExchange, dbCn);
                        Log.Write("BizDateExchange has been set to: " + Master.BizDateExchange + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    if (!KeyValue.Get("BizDateNextExchange", "BuisnessDate", "2001-01-01", dbCn).Equals(Master.BizDateNextExchange))
                    {
                        KeyValue.Set("BizDateNextExchange", "BuisnessDate", Master.BizDateNextExchange, dbCn);
                        Log.Write("BizDateNextExchange has been set to: " + Master.BizDateNextExchange + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    if (!KeyValue.Get("BizDatePriorExchange", "BuisnessDate", "2001-01-01", dbCn).Equals(Master.BizDatePriorExchange))
                    {
                        KeyValue.Set("BizDatePriorExchange", "BuisnessDate", Master.BizDatePriorExchange, dbCn);
                        Log.Write("BizDatePriorExchange has been set to: " + Master.BizDatePriorExchange + " [MedalistMain.BizDatesSet]", Log.Information, 2);
                    }

                    break;
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

