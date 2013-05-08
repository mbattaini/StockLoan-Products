using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml;
using StockLoan.Common;
using StockLoan.DataAccess;


namespace StockLoan.WorldWideDataService
{   
    public class WorldWideDataMain                  //was StockLoan.MemoSegData
	{
		private string dbCnStr;
		private string worldwideDbCnStr;			
	
		private Thread workerThread = null;
		private static bool isStopped = true;
        private static string tempPath;
        
        private SqlConnection dbCn;

		public WorldWideDataMain(string dbCnStr, string worldwideDbCnStr)
		{
			this.dbCnStr = dbCnStr;
			this.worldwideDbCnStr = worldwideDbCnStr;
						
			try
			{
				dbCn = new SqlConnection(dbCnStr);    				
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [WorldWideDataMain.WorldWideDataMain]", Log.Error, 1);
			}

			if (Standard.ConfigValueExists("TempPath"))
			{
				tempPath = Standard.ConfigValue("TempPath");

				if (!Directory.Exists(tempPath))
				{
                    Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [WorldWideDataMain.WorldWideDataMain]", Log.Error, 1);
					tempPath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
                Log.Write("A configuration value for TempPath has not been provided. [WorldWideDataMain.WorldWideDataMain]", Log.Information, 1);
				tempPath = Directory.GetCurrentDirectory();
			}

            Log.Write("Temporary files will be staged at " + tempPath + ". [WorldWideDataMain.WorldWideDataMain]", 2);
		}

		~WorldWideDataMain()
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
				workerThread = new Thread(new ThreadStart(WorldWideDataMainLoop));
				workerThread.Name = "Worker";
				workerThread.Start();

                Log.Write("Start command issued with new worker thread. [worldWideDataMain.Start]", 2);
			}
			else // Old thread will be just fine.
			{
                Log.Write("Start command issued with worker thread already running. [worldWideDataMain.Start]", 2);
			}
		}

		public void Stop()
		{
			isStopped = true;

			if (workerThread == null)
			{
				Log.Write("Stop command issued, worker thread never started. [worldWideDataMain.Stop]", 2);
			}
			else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				workerThread.Abort();
				Log.Write("Stop command issued, sleeping worker thread aborted. [worldWideDataMain.Stop]", 2);
			}
			else
			{
				Log.Write("Stop command issued, worker thread is still active. [worldWideDataMain.Stop]", 2);
			}
		}
		
        private void WorldWideDataMainLoop()
		{
			while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
			{
                Log.Write("Start-of-cycle. [WorldWideDataMain.WorldWideDataMainLoop]", 2);

                TimeZonesUpdate();

                BookGroupsBizDateRoll("", DBStandardFunctions.HolidayType.Any);         
                BookGroupsBizDateRoll("", DBStandardFunctions.HolidayType.Bank);        
                BookGroupsBizDateRoll("", DBStandardFunctions.HolidayType.Exchange);    
                
                Log.Write("End-of-cycle. [WorldWideDataMain.WorldWideDataMainLoop]", 2);

                if (!isStopped)
				{
					Thread.Sleep(RecycleInterval());
				}
			}
		}


        private void TimeZonesUpdate()
        {
            int timeZoneCount = 0;

            try
            {
                ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();

                foreach (TimeZoneInfo tZone in timeZones)
                {
                    DBStandardFunctions.TimeZoneSet(tZone.Id, tZone.DisplayName, tZone.BaseUtcOffset.TotalHours.ToString(),
                                                    tZone.GetUtcOffset(System.DateTime.UtcNow).TotalHours.ToString(), tZone.SupportsDaylightSavingTime,
                                                    tZone.IsDaylightSavingTime(TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, tZone)),
                                                    "Admin");
                    timeZoneCount++;
                }

                KeyValue.Set("TimeZonesUpdateDateTime", DateTime.Now.ToString(), dbCnStr);
                Log.Write("TimeZonesUpdate completed, " + timeZoneCount.ToString() + " timezones updated.  [WorldWideDataMain.TimeZonesUpdate]", Log.Information, 2);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + "  [WorldWideDataMain.TimeZonesUpdate]", Log.Error, 1);
            }
        }

        private void BookGroupsBizDateRoll(string bookGroup, DBStandardFunctions.HolidayType holidayType)    //DC  was Standard.HolidayType holidayType)
        {
            bool     useWeekends;
            DateTime bizDate;
			DateTime bizDateNext;
			DateTime bizDatePrior;
            TimeSpan utcOffsetTimeSpan;
            double   utcOffsetValue;           

            DataSet dsBookGroups = new DataSet();

            try
            {
                dsBookGroups = DBBooks.BookGroupsGet(bookGroup, "");
                //dsBookGroups.Tables["BookGroups"].Rows.Count; 

                foreach (DataRow dr in dsBookGroups.Tables["BookGroups"].Rows)
                {
                    useWeekends = bool.Parse(dr["UseWeekends"].ToString());
                    utcOffsetValue = Double.Parse( dr["UtcOffset"].ToString());     
                    utcOffsetTimeSpan = TimeSpan.FromHours(Double.Parse(dr["UtcOffset"].ToString()));

                    bizDate = DateTime.UtcNow.Add(utcOffsetTimeSpan);

                    while (!DBStandardFunctions.IsBizDate(dr["BookGroup"].ToString(), bizDate, "", holidayType, useWeekends, dbCn))
                    {
                        bizDate = bizDate.AddDays(1.0);
                    }

                    bizDateNext = bizDate.AddDays(1.0);
                    while (!DBStandardFunctions.IsBizDate(dr["BookGroup"].ToString(), bizDateNext, "", holidayType, useWeekends, dbCn))
                    {
                        bizDateNext = bizDateNext.AddDays(1.0);
                    }

                    bizDatePrior = bizDate.AddDays(-1.0);
                    while (!DBStandardFunctions.IsBizDate(dr["BookGroup"].ToString(), bizDatePrior, "", holidayType, useWeekends, dbCn))
                    {
                        bizDatePrior = bizDatePrior.AddDays(-1.0);
                    }

                    switch (holidayType)
                    {
                        case DBStandardFunctions.HolidayType.Any:
                            // Compare TimeZone adjusted bizDate As-of-Now to existing bizDate in Database, if differ then new bizDate has occurred, then update database. 
                            if (!bizDate.ToString("yyyy-MM-dd").Equals(DBStandardFunctions.BizDateGet(dr["BookGroup"].ToString(), "BizDate").ToString("yyyy-MM-dd")))
                            {
                                DBStandardFunctions.BookGroupSet(dr["BookGroup"].ToString(), dr["BookName"].ToString(), dr["TimeZoneId"].ToString(),
                                                             bizDate.ToString(Standard.DateFormat), "", "", "", 
                                                             bizDatePrior.ToString(Standard.DateFormat), "", "", 
                                                             bizDateNext.ToString(Standard.DateFormat), "", "", 
                                                             useWeekends, dr["SettlementType"].ToString());

                                Master.BizDate = bizDate.ToString(Standard.DateFormat);
                                Master.BizDateNext = bizDateNext.ToString(Standard.DateFormat);
                                Master.BizDatePrior = bizDatePrior.ToString(Standard.DateFormat);

                                //KeyValue.Set("BizDate", Master.BizDate, dbCn);
                                Log.Write("BizDate      (" + dr["BookGroup"].ToString() + ") has been set to: " + bizDate.ToString(Standard.DateFormat) + "  [WoorldWideDataMain.BookGroupsBizDateRoll]", Log.Information, 2);
                                Log.Write("BizDateNext  (" + dr["BookGroup"].ToString() + ") has been set to: " + bizDateNext.ToString(Standard.DateFormat) + "  [WoorldWideDataMain.BookGroupsBizDateRoll]", Log.Information, 2);
                                Log.Write("BizDatePrior (" + dr["BookGroup"].ToString() + ") has been set to: " + bizDatePrior.ToString(Standard.DateFormat) + "  [WoorldWideDataMain.BookGroupsBizDateRoll]", Log.Information, 2);
                            }
                            break;

                        case DBStandardFunctions.HolidayType.Bank:
                            if (!bizDate.ToString("yyyy-MM-dd").Equals(DBStandardFunctions.BizDateGet(dr["BookGroup"].ToString(), "BizDateBank").ToString("yyyy-MM-dd")))
                            {
                                DBStandardFunctions.BookGroupSet(dr["BookGroup"].ToString(), dr["BookName"].ToString(), dr["TimeZoneId"].ToString(),
                                                             "", "", bizDate.ToString(Standard.DateFormat), "", 
                                                             "", bizDatePrior.ToString(Standard.DateFormat), "", 
                                                             "", bizDateNext.ToString(Standard.DateFormat), "", 
                                                             useWeekends, dr["SettlementType"].ToString());

                                Master.BizDateBank = bizDate.ToString(Standard.DateFormat);
                                Master.BizDateNextBank = bizDateNext.ToString(Standard.DateFormat);
                                Master.BizDatePriorBank = bizDatePrior.ToString(Standard.DateFormat);

                                Log.Write("BizDateBank      (" + dr["BookGroup"].ToString() + ") has been set to: " + bizDate.ToString(Standard.DateFormat) + "  [WoorldWideDataMain.BookGroupsBizDateRoll]", Log.Information, 2);
                                Log.Write("BizDateNextBank  (" + dr["BookGroup"].ToString() + ") has been set to: " + bizDateNext.ToString(Standard.DateFormat) + "  [WoorldWideDataMain.BookGroupsBizDateRoll]", Log.Information, 2);
                                Log.Write("BizDatePriorBank (" + dr["BookGroup"].ToString() + ") has been set to: " + bizDatePrior.ToString(Standard.DateFormat) + "  [WoorldWideDataMain.BookGroupsBizDateRoll]", Log.Information, 2);
                            }
                            break;

                        case DBStandardFunctions.HolidayType.Exchange:
                            if (!bizDate.ToString("yyyy-MM-dd").Equals(DBStandardFunctions.BizDateGet(dr["BookGroup"].ToString(), "BizDateExchange").ToString("yyyy-MM-dd")))
                            {
                                DBStandardFunctions.BookGroupSet(dr["BookGroup"].ToString(), dr["BookName"].ToString(), dr["TimeZoneId"].ToString(),
                                                             "", "", "", bizDate.ToString(Standard.DateFormat), 
                                                             "", "", bizDatePrior.ToString(Standard.DateFormat),
                                                             "", "", bizDateNext.ToString(Standard.DateFormat),  
                                                             useWeekends, dr["SettlementType"].ToString());

                                Master.BizDateExchange = bizDate.ToString(Standard.DateFormat);
                                Master.BizDateNextExchange = bizDateNext.ToString(Standard.DateFormat);
                                Master.BizDatePriorExchange = bizDatePrior.ToString(Standard.DateFormat);

                                //KeyValue.Set("BizDateExchange", Master.BizDate, dbCn);
                                Log.Write("BizDateExchange      (" + dr["BookGroup"].ToString() + ") has been set to: " + bizDate.ToString(Standard.DateFormat) + "  [WoorldWideDataMain.BookGroupsBizDateRoll]", Log.Information, 2);
                                Log.Write("BizDateNextExchange  (" + dr["BookGroup"].ToString() + ") has been set to: " + bizDateNext.ToString(Standard.DateFormat) + "  [WoorldWideDataMain.BookGroupsBizDateRoll]", Log.Information, 2);
                                Log.Write("BizDatePriorExchange (" + dr["BookGroup"].ToString() + ") has been set to: " + bizDatePrior.ToString(Standard.DateFormat) + "  [WoorldWideDataMain.BookGroupsBizDateRoll]", Log.Information, 2);
                            }
                            break;

                    }   // switch 

                }       //foreach BookGroup Loop 

            }
            catch (Exception e)
            {
                Log.Write(e.Message + "  [WoorldWideDataMain.BookGroupsBizDateRoll]", Log.Error, 1);
                //throw;
            }
        }

        private static TimeSpan UtcOffsetTimeSpan(double utcOffsetInterval)
        {
            TimeSpan timeSpan = new TimeSpan();

            try
            {
                timeSpan = TimeSpan.FromHours(utcOffsetInterval);
            }
            catch (Exception e)
            {
                Log.Write( e.Message + ".  [WorldWideDataMain.UtcOffsetTimeSpan]", Log.Error, 1);
            }
            return timeSpan;
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
				recycleInterval = KeyValue.Get("WorldWideDataServiceMainRecycleIntervalBizDay", "0:05", dbCn);
			}
			else
			{
                recycleInterval = KeyValue.Get("WorldWideDataServiceMainRecycleIntervalNonBizDay", "1:00", dbCn);
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
					KeyValue.Set("WorldWideDataMainRecycleIntervalBizDay", "0:15", dbCn);
					hours = 0;
					minutes = 15;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [WorldWideDataMain.RecycleInterval]", Log.Error, 1);
				}
				else
				{
					KeyValue.Set("WorldWideDataMainRecycleIntervalNonBizDay", "4:00", dbCn);
					hours = 4;
					minutes = 0;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [WorldWideDataMain.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("WorldWideDataMain will recycle in " + hours + " hours, " + minutes + " minutes. [WorldWideDataMain.RecycleInterval]", 2);

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
