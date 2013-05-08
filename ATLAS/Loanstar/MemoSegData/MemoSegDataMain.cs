using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using System.Collections;
using System.Xml;
using StockLoan.Common;

using BroadRidge.MemoSegFiles;

namespace StockLoan.MemoSegData
{
	public class MemoSegDataMain
	{
		private string notificationList;		
		private string dbCnStr;
		private string worldwideDbCnStr;			
	
		private Thread workerThread = null;
		private static bool isStopped = true;
		private static string tempPath;		
        
        private SqlConnection triggerDbCn;
        private SqlConnection dbCn;


		public MemoSegDataMain(string dbCnStr, string worldwideDbCnStr)
		{
			this.dbCnStr = dbCnStr;
			this.worldwideDbCnStr = worldwideDbCnStr;
						
			try
			{
				dbCn = new SqlConnection(dbCnStr);    				
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [MemoSegDataMain.MemoSegDataMain]", Log.Error, 1);
			}

			if (Standard.ConfigValueExists("TempPath"))
			{
				tempPath = Standard.ConfigValue("TempPath");

				if (!Directory.Exists(tempPath))
				{
					Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [MemoSegDataMain.MemoSegDataMain]", Log.Error, 1);
					tempPath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
				Log.Write("A configuration value for TempPath has not been provided. [MemoSegDataMain.MemoSegDataMain]", Log.Information, 1);
				tempPath = Directory.GetCurrentDirectory();
			}

			Log.Write("Temporary files will be staged at " + tempPath + ". [MemoSegDataMain.MemoSegDataMain]", 2);
		}

		~MemoSegDataMain()
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
				workerThread = new Thread(new ThreadStart(MemoSegDataMainLoop));
				workerThread.Name = "Worker";
				workerThread.Start();

				Log.Write("Start command issued with new worker thread. [MemoSegDataMain.Start]", 2);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Start command issued with worker thread already running. [MemoSegDataMain.Start]", 2);
			}
		}

		public void Stop()
		{
			isStopped = true;

			if (workerThread == null)
			{
				Log.Write("Stop command issued, worker thread never started. [MemoSegDataMain.Stop]", 2);
			}
			else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				workerThread.Abort();
				Log.Write("Stop command issued, sleeping worker thread aborted. [MemoSegDataMain.Stop]", 2);
			}
			else
			{
				Log.Write("Stop command issued, worker thread is still active. [MemoSegDataMain.Stop]", 2);
			}
		}
		
        private void MemoSegDataMainLoop()
		{
			while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
			{
                string waitTime = KeyValue.Get("BroadRidgeWaitTime", "04:00", dbCnStr);

				Log.Write("Start-of-cycle. [MemoSegDataMain.MemoSegDataMainLoop]", 2);
				KeyValue.Set("MemoSegDataMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        
				Master.BizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);
				Master.BizDatePrior = KeyValue.Get("BizDatePrior", "0001-01-01", dbCn);
				Master.BizDateExchange = KeyValue.Get("BizDateExchange", "0001-01-01", dbCn);
				Master.BizDatePriorExchange = KeyValue.Get("BizDatePriorExchange", "0001-01-01", dbCn);
				Master.ContractsBizDate =	KeyValue.Get("ContractsBizDate", "0001-01-01", dbCn);
				Master.BizDateNext = KeyValue.Get("BizDateNext", "0001-01-01", dbCn);

                DateTime timeUntil = DateTime.ParseExact(waitTime, "HH:mm", null);

                if ((DateTime.Now >= timeUntil) && (Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat))))
                {
                    MemoSegStartOfDayLoad("0234", Standard.ConfigValue("ExDeficitP3", ""));
                    if (isStopped) break;

                    MemoSegStartOfDayLoad("0158", Standard.ConfigValue("ExDeficitBPS", ""));
                    if (isStopped) break;
                }
                else
                {
                    Log.Write("Must wait until after " + waitTime + " to begin processing. [MemoSegDataMain.MemoSegDataMainLoop]", 1);
                }

                KeyValue.Set("MemoSegDataMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
				Log.Write("End-of-cycle. [MemoSegDataMain.MemoSegDataMainLoop]", 2);

				if (!isStopped)
				{
					Thread.Sleep(RecycleInterval());
				}
			}
		}

        private void MemoSegStartOfDayLoad(string system, string filePath)
        {
            if (!KeyValue.Get("MemoSegEdDeficit" + system + "Date", "", dbCnStr).Equals(Master.BizDate))
            {

                try
                {
                    MemoSegStartOfDayParser msegParser = new MemoSegStartOfDayParser(system, filePath, Master.BizDatePrior, dbCnStr);
                    msegParser.Load();

                    KeyValue.Set("MemoSegEdDeficit" + system + "Date", Master.BizDate, dbCnStr);
                }
                catch (Exception e)
                {
                    Log.Write(e.Message + " [MemoSegDataMain.MemoSegStartOfDayLoad]", Log.Error, 1);

                }

               
            }
            else
            {
                Log.Write("Memo Seg start of day exdeficit file for: " + system + " already loaded. [MemoSegDataMain.MemoSegStartOfDayLoad]", 1 );
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
				recycleInterval = KeyValue.Get("MemoSegDataMainRecycleIntervalBizDay", "0:20", dbCn);
			}
			else
			{
				recycleInterval = KeyValue.Get("MemoSegDataMainRecycleIntervalNonBizDay", "6:00", dbCn);
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
					KeyValue.Set("MemoSegDataMainRecycleIntervalBizDay", "0:20", dbCn);
					hours = 0;
					minutes = 20;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [MemoSegDataMain.RecycleInterval]", Log.Error, 1);
				}
				else
				{
					KeyValue.Set("MemoSegDataMainRecycleIntervalNonBizDay", "6:00", dbCn);
					hours = 6;
					minutes = 0;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [MemoSegDataMain.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("MemoSegDataMain will recycle in " + hours + " hours, " + minutes + " minutes. [MemoSegDataMain.RecycleInterval]", 2);
			return timeSpan;
		}
   
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
