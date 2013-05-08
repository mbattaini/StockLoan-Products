using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using System.Collections;
using System.Xml;
using StockLoan.Common;


namespace StockLoan.ComplianceData
{
	public class ComplianceDataMain
	{
		private string dbCnStr;
		private string worldwideDbCnStr;			
	
		private Thread workerThread = null;
		private static bool isStopped = true;
		private static string tempPath;		
                
        private SqlConnection dbCn;


		public ComplianceDataMain(string dbCnStr, string worldwideDbCnStr)
		{
			this.dbCnStr = dbCnStr;
			this.worldwideDbCnStr = worldwideDbCnStr;
						
			try
			{
				dbCn = new SqlConnection(dbCnStr);    				
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ComplianceDataMain.ComplianceDataMain]", Log.Error, 1);
			}

			if (Standard.ConfigValueExists("TempPath"))
			{
				tempPath = Standard.ConfigValue("TempPath");

				if (!Directory.Exists(tempPath))
				{
					Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [ComplianceDataMain.ComplianceDataMain]", Log.Error, 1);
					tempPath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
				Log.Write("A configuration value for TempPath has not been provided. [ComplianceDataMain.ComplianceDataMain]", Log.Information, 1);
				tempPath = Directory.GetCurrentDirectory();
			}

			Log.Write("Temporary files will be staged at " + tempPath + ". [ComplianceDataMain.ComplianceDataMain]", 2);
		}

		~ComplianceDataMain()
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
				workerThread = new Thread(new ThreadStart(ComplianceDataMainLoop));
				workerThread.Name = "Worker";
				workerThread.Start();

				Log.Write("Start command issued with new worker thread. [ComplianceDataMain.Start]", 2);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Start command issued with worker thread already running. [ComplianceDataMain.Start]", 2);
			}
		}

		public void Stop()
		{
			isStopped = true;

			if (workerThread == null)
			{
				Log.Write("Stop command issued, worker thread never started. [ComplianceDataMain.Stop]", 2);
			}
			else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				workerThread.Abort();
				Log.Write("Stop command issued, sleeping worker thread aborted. [ComplianceDataMain.Stop]", 2);
			}
			else
			{
				Log.Write("Stop command issued, worker thread is still active. [ComplianceDataMain.Stop]", 2);
			}
		}
		
        private void ComplianceDataMainLoop()
		{            
			while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
			{
                string waitTime = KeyValue.Get("ComplianceWaitTime", "06:10", dbCnStr);
                string secmasterWaitTime = KeyValue.Get("PensonSecurityStaticWaitUntil", "06:10", dbCnStr);
                string failDayCountWaitTime = KeyValue.Get("BoxPositionFailDayCountSetTime", "19:30", dbCnStr);

                Log.Write("Start-of-cycle. [ComplianceDataMain.ComplianceDataMainLoop]", 2);
				KeyValue.Set("ComplianceDataMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        
				Master.BizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);
				Master.BizDatePrior = KeyValue.Get("BizDatePrior", "0001-01-01", dbCn);
				Master.BizDateExchange = KeyValue.Get("BizDateExchange", "0001-01-01", dbCn);
				Master.BizDatePriorExchange = KeyValue.Get("BizDatePriorExchange", "0001-01-01", dbCn);
				Master.ContractsBizDate =	KeyValue.Get("ContractsBizDate", "0001-01-01", dbCn);
				Master.BizDateNext = KeyValue.Get("BizDateNext", "0001-01-01", dbCn);

                DateTime timeUntil = DateTime.ParseExact(waitTime, "HH:mm", null);
                DateTime secmasterWaitUntil = DateTime.ParseExact(secmasterWaitTime, "HH:mm", null);
                DateTime failDayCountWaitUntil = DateTime.ParseExact(failDayCountWaitTime, "HH:mm", null);

                if ((DateTime.Now >= timeUntil) && (Master.ContractsBizDate.Equals(DateTime.Now.ToString(Standard.DateFormat))))
                {
                    StraddlesWithAmounts();
                    if (isStopped) break;                    
                    
                    TradingDatesLoad();
                    if (isStopped) break;
                  
                    CnsProjection0158Load();
                    if (isStopped) break;

                    CheckListCreate();
                    if (isStopped) break;

                    BroadRidgeShortsLoad();
                    if (isStopped) break;

                    ShortSaleBillingSnapshotWrapper();
                    if (isStopped) break;

                    ShortSaleBpsBillingBreakdown();
                    if (isStopped) break;
                }
                else
                {
                    Log.Write("Must wait until after " + waitTime + " to begin processing. [ComplianceDataMain.ComplianceDataMainLoop]", 1);
                }


                /***** 2012-05-15  SecurityDataLoad function Moved to DomesticBoxInformationService 
                if ((DateTime.Now >= secmasterWaitUntil) && (Master.ContractsBizDate.Equals(DateTime.Now.ToString(Standard.DateFormat))))
                {
                    SecurityDataLoad();
                    if (isStopped) break;
                }
                else
                {
                    Log.Write("Not time to run securtiy master data. [ComplianceDataMain.ComplianceDataMainLoop]", 1);
                }
                if (isStopped) break;
                *****/

                if ((DateTime.Now >= failDayCountWaitUntil) && (Master.ContractsBizDate.Equals(DateTime.Now.ToString(Standard.DateFormat))))
                {
                    BoxPositionFailDayCountLoad();                
                }
                else
                {
                    Log.Write("Not time to run fail day count data. [ComplianceDataMain.ComplianceDataMainLoop]", 1);
                }
                if (isStopped) break;

                KeyValue.Set("ComplianceDataMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
				Log.Write("End-of-cycle. [ComplianceDataMain.ComplianceDataMainLoop]", 2);

				if (!isStopped)
				{
					Thread.Sleep(RecycleInterval());
				}
			}
		}

        public void ShortSaleBpsBillingBreakdown()
        {
            string reportLoadDate = KeyValue.Get("ComplianceBillingBreakdownLoadDate", "", dbCnStr);            
            string billingLoadDate =  KeyValue.Get("ShortSaleNegRebateBillingBPSSnapShotBizDate", "", dbCnStr);        
            string fileName = "";

            if (billingLoadDate.Equals(Master.BizDate) && !reportLoadDate.Equals(Master.BizDate))
            {
                try
                {
                    ShortSaleBpsBillingBreakdownLoad sLoad = new ShortSaleBpsBillingBreakdownLoad(Master.BizDatePrior, Master.BizDate, dbCnStr);
                    fileName = sLoad.Load();

                    if (fileName.Equals(""))
                    {
                        throw new Exception("Filepath is blank");
                    }

                    Email.Send(KeyValue.Get("ComplianceBillingBreakdownLoadTo", "mbattaini@penson.com", dbCnStr),
                                KeyValue.Get("ComplianceBillingBreakdownFrom", "sendero@penson.com", dbCnStr),
                                "Billing Breakdown Report For " + Master.BizDate,
                                "",
                                fileName,
                                dbCnStr);

                    KeyValue.Set("ComplianceBillingBreakdownLoadDate", Master.BizDate, dbCnStr);

                    Log.Write("Short Sale Billing Breakdown sent for " + Master.BizDate + ". [StockLoan.ComplianceData.ShortSaleBpsBillingBreakdown]", 1);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ". [StockLoan.ComplianceData.ShortSaleBpsBillingBreakdown]", 1);
                }
            }
            else if (reportLoadDate.Equals(Master.BizDate))
            {
                Log.Write("Short Sale Billing Breakdown already sent for " + Master.BizDate + ". [StockLoan.ComplianceData.ShortSaleBpsBillingBreakdown]", 1);
            }
            else
            {
                Log.Write("Short Sale Billing Breakdown not ready for " + Master.BizDate + ". [StockLoan.ComplianceData.ShortSaleBpsBillingBreakdown]", 1);
            }            
        }


        private void StraddlesWithAmounts()
        {
            string straddlesLoadDate = KeyValue.Get("ComplianceStraddlesLoadDate", "", dbCnStr);
            string straddlesWaitTime = KeyValue.Get("ComplianceStraddlesLoadTime", "06:00", dbCnStr);
            string fileName = "";

            DateTime timeUntil = DateTime.ParseExact(straddlesWaitTime, "HH:mm", null);

            if (!straddlesLoadDate.Equals(Master.BizDate) && DateTime.Now >= timeUntil)
            {
                try
                {
                    StraddlesLoad sLoad = new StraddlesLoad(dbCnStr);
                    fileName = sLoad.Load();

                    if (fileName.Equals(""))
                    {
                        throw new Exception("Filepath is blank");
                    }
                    
                    Email.Send(KeyValue.Get("ComplianceStraddlesLoadTo", "mbattaini@penson.com", dbCnStr),
                                KeyValue.Get("ComplianceStraddlesLoadFrom", "sendero@penson.com", dbCnStr),
                                "Straddles For " + Master.BizDate,
                                "",
                                fileName,
                                dbCnStr);

                    KeyValue.Set("ComplianceStraddlesLoadDate", Master.BizDate, dbCnStr);

                    Log.Write("Straddles loaded for " + Master.BizDate + ". [StockLoan.ComplianceData.StraddlesWithAmounts]", 1);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ". [StockLoan.ComplianceData.StraddlesWithAmounts]", 1);
                }
            }
            else
            {
                Log.Write("BR Short Data Loaded For " + Master.BizDate + ". [StockLoan.ComplianceData.StraddlesWithAmounts]", 1);
            }
        }
                    

        private void BroadRidgeShortsLoad()
        {
            string tradeDataLoadDate = KeyValue.Get("ComplianceBroadRidgeShortsLoadDate", "", dbCnStr);
            string tradeDataWaitTime = KeyValue.Get("ComplianceBroadRidgeShortsLoadTime", "16:00", dbCnStr);

            DateTime timeUntil =  DateTime.ParseExact(tradeDataWaitTime, "HH:mm", null);

            if (!tradeDataLoadDate.Equals(Master.BizDate) && DateTime.Now >= timeUntil)
            {
                try
                {
                    BroadRidgeShortAccountsLoad bShorts = new BroadRidgeShortAccountsLoad(Master.BizDate, dbCnStr, worldwideDbCnStr);
                    bShorts.Load();

                    Email.Send(KeyValue.Get("ComplianceBroadRidgeShortsLoadTo", "mbattaini@penson.com", dbCnStr),
                                KeyValue.Get("ComplianceBroadRidgeShortsLoadFrom", "sendero@penson.com", dbCnStr),
                                "BR Shorts Loaded For " + Master.BizDateNext,
                                "",
                                dbCnStr);

                    KeyValue.Set("ComplianceBroadRidgeShortsLoadDate", Master.BizDate, dbCnStr);
                    Log.Write("BR Short Data Loaded For " + Master.BizDateNext + ". [StockLoan.ComplianceData.TradingDatesLoad]", 1);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ". [StockLoan.ComplianceData.TradingDatesLoad]", 1);
                }
            }
            else
            {
                Log.Write("BR Short Data Loaded For " + Master.BizDate + ". [StockLoan.ComplianceData.TradingDatesLoad]", 1);
            }
        }

        private void TradingDatesLoad()
        {
            string tradeDataLoadDate = KeyValue.Get("ComplianceTradeDataLoadDate", "", dbCnStr);

            if (!tradeDataLoadDate.Equals(Master.BizDate))
            {
                try
                {
                    DatabaseFunctions.TradeDataLoad(Master.BizDate, Master.BizDateNext, dbCnStr);

                    Email.Send(KeyValue.Get("ComplianceTradeDataLoadTo", "mbattaini@penson.com", dbCnStr),
                                KeyValue.Get("ComplianceTradeDataLoadFrom", "sendero@penson.com", dbCnStr),
                                "T + 2 Sells Loaded For " + Master.BizDate,
                                "",
                                dbCnStr);

                    KeyValue.Set("ComplianceTradeDataLoadDate", Master.BizDate, dbCnStr);
                    KeyValue.Set("ComplianceCheckListCreateLoadDate", "", dbCnStr);
                    Log.Write("Trade Data Loaded For " + Master.BizDate + ". [StockLoan.ComplianceData.TradingDatesLoad]", 1);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ". [StockLoan.ComplianceData.TradingDatesLoad]", 1);
                }
            }
            else
            {
                Log.Write("Trade Data Loaded For " + Master.BizDate + ". [StockLoan.ComplianceData.TradingDatesLoad]", 1);
            }
        }

        private void CnsProjection0158Load()
        {
            string cnsDataLoadDate = KeyValue.Get("ComplianceCns0158DataLoadDate", "", dbCnStr);

            if (!cnsDataLoadDate.Equals(Master.BizDate))
            {
                try
                {
                    DatabaseFunctions.CnsProjectionPurge(Master.BizDate, "0158", dbCnStr);

                    Cns0158ProjectionParse cnsParse = new Cns0158ProjectionParse(Standard.ConfigValue("CnsProjectionFile0158", ""), "0158", Master.BizDate, dbCnStr);
                    cnsParse.Load();

                    Email.Send(KeyValue.Get("ComplianceCns0158DataLoadTo", "mbattaini@penson.com", dbCnStr),
                                KeyValue.Get("ComplianceCns0158DataLoadFrom", "sendero@penson.com", dbCnStr),
                                "Cns projection Data loaded for 0158 for " + Master.BizDate,
                                "",
                                dbCnStr);

                    KeyValue.Set("ComplianceCns0158DataLoadDate", Master.BizDate, dbCnStr);
                    KeyValue.Set("ComplianceCheckListCreateLoadDate", "", dbCnStr);
                    Log.Write("Cns Projection Data 0158 Loaded For " + Master.BizDate + ". [StockLoan.ComplianceData.CnsProjection0158Load]", 1);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ". [StockLoan.ComplianceData.CnsProjection0158Load]", 1);
                }
            }
            else
            {
                Log.Write("Cns Projection Data 0158 Loaded For " + Master.BizDate + ". [StockLoan.ComplianceData.CnsProjection0158Load]", 1);
            }
        }

        private void CheckListCreate()
        {           
            string tradeDataLoadDate = KeyValue.Get("ComplianceCheckListCreateLoadDate", "", dbCnStr);
            string tradeDataWaitTime = KeyValue.Get("ComplianceCheckListCreateLoadTime", "04:00", dbCnStr);

            string message = "";
           
            if (!tradeDataLoadDate.Equals(Master.BizDate) && DateTime.Now > DateTime.ParseExact(tradeDataWaitTime, "HH:mm", null))
            {
                try
                {

                    CheckListCreation cList = new CheckListCreation(Master.BizDate, dbCnStr);
                    message = cList.Load();

                    Email.Send(KeyValue.Get("ComplianceCheckListTo", "mbattaini@penson.com", dbCnStr),
                                KeyValue.Get("ComplianceCheckListFrom", "sendero@penson.com", dbCnStr),
                                "Check List For " + Master.BizDate,
                                message,
                                dbCnStr);
                    
                    KeyValue.Set("ComplianceCheckListCreateLoadDate", Master.BizDate, dbCnStr);

                    Log.Write("Check List For " + Master.BizDate + ". [StockLoan.ComplianceData.TradingDatesLoad]", 1);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ". [StockLoan.ComplianceData.TradingDatesLoad]", 1);
                }
            }
        }

        private void ShortSaleBillingSnapshotWrapper()
        {
            string shortAccountsLoad = KeyValue.Get("ComplianceBroadRidgeShortsLoadDate", "", dbCnStr);
            string shortbillingLoadDate = KeyValue.Get("ShortSaleNegRebateBillingBPSSnapShotBizDate", "", dbCnStr);
            string boxPositionInitialLoadDate = KeyValue.Get("BroadRidgeBoxPositionLoadDate", "", dbCnStr);
            string waitUntil = KeyValue.Get("ShortSaleNegRebateBillingBPSSnapShotTime", "", dbCnStr);

            if (shortAccountsLoad.Equals(Master.BizDate)
                && boxPositionInitialLoadDate.Equals(Master.BizDate)
                && Master.BizDate.Equals(Master.ContractsBizDate)
                && !shortbillingLoadDate.Equals(Master.BizDate)
                && DateTime.Now > DateTime.ParseExact(waitUntil, "HH:mm", null))
            {
                try
                {
                    SnapshotWrapperLoad snapShot = new SnapshotWrapperLoad(Master.BizDate, Master.BizDatePrior, "0158", dbCnStr);
                    snapShot.Load();

                    KeyValue.Set("ShortSaleNegRebateBillingBPSSnapShotBizDate", Master.BizDate, dbCnStr);
                    Log.Write("BPS Snapshot taken for" + Master.BizDate + ". [StockLoan.ComplianceData.ShortSaleBillingSnapshotWrapper]", 1);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ". [StockLoan.ComplianceData.ShortSaleBillingSnapshotWrapper]", 1);
                }
            }
            else if (shortbillingLoadDate.Equals(Master.BizDate))
            {
                Log.Write("BPS Snapshot already taken for" + Master.BizDate + ". [StockLoan.ComplianceData.ShortSaleBillingSnapshotWrapper]", 1);
            }
            else
            {
                Log.Write("System not ready for snapshot for " + Master.BizDate + ". [StockLoan.ComplianceData.ShortSaleBillingSnapshotWrapper]", 1);
            }
        }

        private void SecurityDataLoad()
        { 
            string securityDataLoadDate = KeyValue.Get("PensonSecurityStaticBizDate", "", dbCnStr);
            string waitUntil = KeyValue.Get("PensonSecurityStaticWaitUntil", "08:00", dbCn);  //UTC time

            if (securityDataLoadDate.Equals(Master.BizDatePrior)) 
            {
                Log.Write("Penson Security Static Data already loaded for " + Master.BizDatePrior + ".  [StockLoan.ComplianceData.SecurityDataLoad]", 2);
                return;
            }
            else if (Master.BizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) 
            {
                if (waitUntil.CompareTo(DateTime.UtcNow.ToString("HH:mm")) > 0) // Too early.
                {
                    Log.Write("Penson Security Static Data will load after " + waitUntil + " UTC.  [StockLoan.ComplianceData.SecurityDataLoad]", 2);
                    return;
                }
            }
            else
            {
                Log.Write("Penson Security Static Data will load tomorrow.  [StockLoan.ComplianceData.SecurityDataLoad]", 2);
                return;
            }

            try
            {
                SecurityDataLoad secData = new SecurityDataLoad(Master.BizDate, Master.BizDatePrior, dbCnStr);
                secData.Load();

                KeyValue.Set("PensonSecurityStaticBizDate", Master.BizDatePrior, dbCnStr);
                Log.Write("Penson Security Static Data loaded for " + Master.BizDatePrior + ".  [StockLoan.ComplianceData.SecurityDataLoad]", 2);
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StockLoan.ComplianceData.SecurityDataLoad]", 1);
            }
        }

        private void BoxPositionFailDayCountLoad()
        {
            string boxPositionFailDayCountBizDate = KeyValue.Get("BoxPositionFailDayCountBizDate", "", dbCnStr);

            if (boxPositionFailDayCountBizDate.Equals(Master.BizDate))
            {
                Log.Write("Box Position Fail Day Count already loaded for " + Master.BizDate + ".  [StockLoan.ComplianceData.BoxPositionFailDayCountLoad]", 2);
                return;
            }

            try
            {
                BoxPositionDayCountLoad boxFailData = new BoxPositionDayCountLoad(Master.BizDate, dbCnStr);
                boxFailData.Load();

                try
                {
                    Email.Send("mbattaini@penson.com;support.stockloan@penson.com",
                        "Sendero@penson.com",
                        "Box Position Fail Day Count run for " + Master.BizDate,
                        "",
                        dbCnStr);
                }
                catch { }
                
                KeyValue.Set("BoxPositionFailDayCountBizDate", Master.BizDate, dbCnStr);
                Log.Write("Box Position Fail Day Count loaded for " + Master.BizDatePrior + ".  [StockLoan.ComplianceData.BoxPositionFailDayCountLoad]", 2);
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StockLoan.ComplianceData.BoxPositionFailDayCountLoad]", 1);
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
				recycleInterval = KeyValue.Get("ComplianceDataMainRecycleIntervalBizDay", "0:20", dbCn);
			}
			else
			{
				recycleInterval = KeyValue.Get("ComplianceDataMainRecycleIntervalNonBizDay", "6:00", dbCn);
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
					KeyValue.Set("ComplianceDataMainRecycleIntervalBizDay", "0:20", dbCn);
					hours = 0;
					minutes = 20;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [ComplianceDataMain.RecycleInterval]", Log.Error, 1);
				}
				else
				{
					KeyValue.Set("ComplianceDataMainRecycleIntervalNonBizDay", "6:00", dbCn);
					hours = 6;
					minutes = 0;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [ComplianceDataMain.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("ComplianceDataMain will recycle in " + hours + " hours, " + minutes + " minutes. [ComplianceDataMain.RecycleInterval]", 2);
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
