using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using System.Collections;
using System.Xml;
using StockLoan.Common;
using StockLoan.ParsingFiles;

namespace StockLoan.BDBoxInformation
{
	public class BDBoxInformationMain
	{
		private string notificationList;		
		private string dbCnStr;
		private string externalDbCnStr;			
	
		private Thread workerThread = null;
		private static bool isStopped = true;
		private static string tempPath;		
        
        private SqlConnection triggerDbCn;
        private SqlConnection dbCn;
        private string bookGroup;
        
        private string preWaitTime;
        private string sodWaitTime;
        private string eodWaitTime;

		public BDBoxInformationMain(string dbCnStr, string externalDbCnStr)
		{
			this.dbCnStr = dbCnStr;
			this.externalDbCnStr = externalDbCnStr;
						
			try
			{
				dbCn = new SqlConnection(dbCnStr);    				
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [BDBoxInformationMain.BDBoxInformationMain]", Log.Error, 1);
			}

			if (Standard.ConfigValueExists("TempPath"))
			{
				tempPath = Standard.ConfigValue("TempPath");

				if (!Directory.Exists(tempPath))
				{
					Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [BDBoxInformationMain.BDBoxInformationMain]", Log.Error, 1);
					tempPath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
				Log.Write("A configuration value for TempPath has not been provided. [BDBoxInformationMain.BDBoxInformationMain]", Log.Information, 1);
				tempPath = Directory.GetCurrentDirectory();
			}

			Log.Write("Temporary files will be staged at " + tempPath + ". [BDBoxInformationMain.BDBoxInformationMain]", 2);
		}

		~BDBoxInformationMain()
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
				workerThread = new Thread(new ThreadStart(BDBoxInformationMainLoop));
				workerThread.Name = "Worker";
				workerThread.Start();

				Log.Write("Start command issued with new worker thread. [BDBoxInformationMain.Start]", 2);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Start command issued with worker thread already running. [BDBoxInformationMain.Start]", 2);
			}
		}

		public void Stop()
		{
			isStopped = true;

			if (workerThread == null)
			{
				Log.Write("Stop command issued, worker thread never started. [BDBoxInformationMain.Stop]", 2);
			}
			else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				workerThread.Abort();
				Log.Write("Stop command issued, sleeping worker thread aborted. [BDBoxInformationMain.Stop]", 2);
			}
			else
			{
				Log.Write("Stop command issued, worker thread is still active. [BDBoxInformationMain.Stop]", 2);
			}
		}

        private void BDBoxInformationMainLoop()
        {
            while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
            {               
                Log.Write("Start-of-cycle. [BDBoxInformationMain.BDBoxInformationMainLoop]", 2);
                KeyValue.Set("BDBoxInformationMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);

                Master.BizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);
                Master.BizDatePrior = KeyValue.Get("BizDatePrior", "0001-01-01", dbCn);
                Master.BizDateExchange = KeyValue.Get("BizDateExchange", "0001-01-01", dbCn);
                Master.BizDatePriorExchange = KeyValue.Get("BizDatePriorExchange", "0001-01-01", dbCn);
                Master.ContractsBizDate = KeyValue.Get("ContractsBizDate", "0001-01-01", dbCn);
                Master.BizDateNext = KeyValue.Get("BizDateNext", "0001-01-01", dbCn);

                preWaitTime = KeyValue.Get("BroadRidgePreWaitTime", "00:00", dbCnStr);
                sodWaitTime = KeyValue.Get("BroadRidgeSodWaitTime", "04:00", dbCnStr);
                eodWaitTime = KeyValue.Get("BroadRidgeEodWaitTime", "23:00", dbCnStr);
                bookGroup = Standard.ConfigValue("BookGroup", "");

                if (TimeCheck(preWaitTime, "Prestart Of Day Load"))
                {
                    LoadPreBoxPoition();
                }
                if (isStopped) { break; }
                
                
                if (TimeCheck(sodWaitTime, "Start Of Day Load"))
                {
                    LoadSecMaster();
                    LoadFails();
                    LoadSegregation();
                    LoadBankLoan();
                    LoadExpend();
                    LoadScanq();
                    LoadCustomerPositions();
                    LoadBoxPosition();
                }
                if (isStopped) { break; }

                LoadIntraDayBoxPosition();
                if (isStopped) { break; }

                if (TimeCheck(eodWaitTime, "End Of Day Load"))
                {                    
                    LoadBoxPositionAgeFails();
                }
                if (isStopped) { break; }


                KeyValue.Set("BDBoxInformationMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
                Log.Write("End-of-cycle. [BDBoxInformationMain.BDBoxInformationMainLoop]", 2);

                if (!isStopped)
                {
                    Thread.Sleep(RecycleInterval());
                }
            }
        }

        private bool TimeCheck(string waitTime, string process)
        {
            DateTime timeUntil = DateTime.ParseExact(waitTime, "HH:mm", null);

            if ((DateTime.Now >= timeUntil) && (Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat))))
            {
                Log.Write("Starting: " + process + "  for " + bookGroup + ".", 1);
                return true;
            }
            else
            {
                Log.Write("Must wait until : " + waitTime + " to start: " + process + "  for " + bookGroup + ".", 1);
                return false;
            }
        }  

        private void LoadSecMaster()
        {
            string keyValueSecMasterDate = KeyValue.Get("BoxInformationSecMasterDate", "", dbCnStr);

            if (!keyValueSecMasterDate.Equals(Master.BizDate))
            {
                string secMasterSQL = "";

                try
                {
                    TextReader textReader = new StreamReader(Standard.ConfigValue("SecMasterSQL"));
                    secMasterSQL = textReader.ReadToEnd();

                    ParseSecMaster parseSecMaster = new ParseSecMaster(dbCnStr, externalDbCnStr, bookGroup);
                    parseSecMaster.ProgressChanged += new EventHandler<ProgressEventArgs>(parseSecMaster_ProgressChanged);
                    parseSecMaster.Interval = 1000;
                    parseSecMaster.Load(secMasterSQL, Master.BizDate);
                    
                    Log.Write("Processed : " + parseSecMaster.Count.ToString("#,##0") + " positions Items.[BDBoxInformationMain.LoadSecMaster]", 3);

                    KeyValue.Set("BoxInformationSecMasterDate", Master.BizDate, dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadSecMaster]", 3);
                    throw;
                }                           
            }
            else
            {
                Log.Write("All security master information has been loaded for : " + Master.BizDate + ".[BDBoxInformationMain.LoadSecMaster]", 1);
            }
        }

        void parseSecMaster_ProgressChanged(object sender, ProgressEventArgs e)
        {
            Log.Write("Progress: " + e.Progress.ToString("#,##0"), 1);
        }

        private void LoadBoxPosition()
        {
            string keyValueBoxPositionDate = KeyValue.Get("BoxInformationBoxPositionLoadDate", "", dbCnStr);

            if (!keyValueBoxPositionDate.Equals(Master.BizDate))
            {
                string boxPositionSQL = "";

                try
                {                   
                    TextReader textReader = new StreamReader(Standard.ConfigValue("BoxPositionSQL"));
                    boxPositionSQL = textReader.ReadToEnd().ToUpper();

                    boxPositionSQL = boxPositionSQL.Replace("%BIZDATEPRIOR%", "'" + Master.BizDatePrior + "'");
                    boxPositionSQL = boxPositionSQL.Replace("%BIZDATE%", "'" + Master.BizDate + "'");
                    boxPositionSQL = boxPositionSQL.Replace("%BOOKGROUP%", "'" + bookGroup + "'");

                    LoadBoxPosition loadBoxPosition = new LoadBoxPosition(dbCnStr);
                    loadBoxPosition.Purge(Master.BizDate, bookGroup);
                    loadBoxPosition.Load(boxPositionSQL);

                    KeyValue.Set("BoxInformationBoxPositionLoadDate", Master.BizDate, dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadBoxPosition]", 3);
                }
            }
            else
            {
                Log.Write("All box position has been loaded for : " + Master.BizDate + ".[BDBoxInformationMain.LoadBoxPosition]", 1);
            }
        }

        private void LoadIntraDayBoxPosition()
        {
            string keyValueBoxPositionDate = KeyValue.Get("BoxInformationBoxPositionLoadDate", "", dbCnStr);

            if (keyValueBoxPositionDate.Equals(Master.BizDate))
            {           
                try
                {                    
                    LoadBoxPosition loadBoxPosition = new LoadBoxPosition(dbCnStr);
                    loadBoxPosition.IntradDay(Master.BizDate, bookGroup);
                    
                    Log.Write("Box position intraday been loaded for: " + Master.BizDate + ".[BDBoxInformationMain.LoadIntraDayBoxPosition]", 1);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadIntraDayBoxPosition]", 3);
                }
            }
            else
            {
                Log.Write("Must wait until box posotion has been loaded for: " + Master.BizDate + ".[BDBoxInformationMain.LoadIntraDayBoxPosition]", 1);
            }
        }

        private void LoadPreBoxPoition()
        {
            string keyValuePreBoxPositionDate = KeyValue.Get("BoxInformationPreBoxPositionLoadDate", "", dbCnStr);
            string keyValueBoxPositionDate = KeyValue.Get("BoxInformationBoxPositionLoadDate", "", dbCnStr);

            if ((!keyValuePreBoxPositionDate.Equals(Master.BizDate)) && (!keyValueBoxPositionDate.Equals(Master.BizDate)))
            {              
                try
                {                  

                    LoadBoxPosition loadBoxPosition = new LoadBoxPosition(dbCnStr);
                    loadBoxPosition.Purge(Master.BizDate, bookGroup);
                    loadBoxPosition.LoadPre(Master.BizDate, Master.BizDatePrior, bookGroup);

                    KeyValue.Set("BoxInformationPreBoxPositionLoadDate", Master.BizDate, dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadPreBoxPoition]", 3);
                }
            }
            else
            {
                Log.Write("(Pre) box position has been loaded for : " + Master.BizDate + ".[BDBoxInformationMain.LoadPreBoxPoition]", 1);
            }
        }

        private void LoadBoxPositionAgeFails()
        {
            string keyValueBoxPositionDate = KeyValue.Get("BoxInformationBoxPositionAgeFailsLoadDate", "", dbCnStr);

            if (!keyValueBoxPositionDate.Equals(Master.BizDate))
            {
                try
                {               
                    LoadBoxPosition loadBoxPosition = new LoadBoxPosition(dbCnStr);
                    loadBoxPosition.AgeFails(Master.BizDate, bookGroup);

                    KeyValue.Set("BoxInformationBoxPositionAgeFailsLoadDate", Master.BizDate, dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadBoxPositionAgeFails]", 3);
                }
            }
            else
            {
                Log.Write("All box position fails has been aged loaded for : " + Master.BizDate + ".[BDBoxInformationMain.LoadBoxPositionAgeFails]", 1);
            }
        }

        private void LoadFails()
        {
            string keyValueClearingFailDate = KeyValue.Get("BoxInformationClearingFailDate", "", dbCnStr);
            string keyValueOtherFailDate = KeyValue.Get("BoxInformationOtherFailDate", "", dbCnStr);

            if (!keyValueClearingFailDate.Equals(Master.BizDate) || !keyValueOtherFailDate.Equals(Master.BizDate))
            {
                ParseClearingFails parseClearing = new ParseClearingFails(dbCnStr, bookGroup);
                parseClearing.ProgressChanged += new EventHandler<ProgressEventArgs>(progressChanged);
                parseClearing.Interval = 1000;
                parseClearing.BoxFailsPurge(Master.BizDate);

                try
                {

                    parseClearing.Load(Standard.ConfigValue("FilePathClearingFails", ""), Master.BizDate, Master.BizDate);
                    Log.Write("Processed : " + parseClearing.Count.ToString("#,##0") + " CNS Items.[BDBoxInformationMain.LoadFails]", 3);

                    KeyValue.Set("BoxInformationClearingFailDate", Master.BizDate, dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadFails]", 3);
                    throw;
                }

                ParseNonClearingFails parseFails = new ParseNonClearingFails(dbCnStr, bookGroup);

                try
                {
                    parseFails.Interval = 1000;
                    parseFails.ProgressChanged += new EventHandler<ProgressEventArgs>(progressChanged);
                    parseFails.Load(Standard.ConfigValue("FilePathNonClearingFails", ""), Master.BizDate, Master.BizDate);
                    
                    Log.Write("Processed : " + parseFails.Count.ToString("#,##0") + " DVP And Broker Items.[BDBoxInformationMain.LoadFails]", 3);

                    KeyValue.Set("BoxInformationOtherFailDate", Master.BizDate, dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadFails]", 3);
                    throw;
                }
            }
            else
            {
                Log.Write("All fails information has been loaded for : " + Master.BizDate + ".[BDBoxInformationMain.LoadFails]", 1);
            }
        }
      
        private void LoadScanq()
        {
            string keyValueScanqDate = KeyValue.Get("BoxInformationScanqDate", "", dbCnStr);

            if (!keyValueScanqDate.Equals(Master.BizDate))
            {
                ParseScanq parseScanq = new ParseScanq(dbCnStr, bookGroup);
                try
                {
                    parseScanq.Interval = 1000;
                    parseScanq.ProgressChanged += new EventHandler<ProgressEventArgs>(progressChanged);
                    parseScanq.Load(Standard.ConfigValue("FilePathScanq"), Master.BizDatePrior, Master.BizDate);

                    Log.Write("Processed : " + parseScanq.Count.ToString("#,##0") + " scanq Items.[BDBoxInformationMain.LoadScanq]", 3);

                    KeyValue.Set("BoxInformationScanqDate", Master.BizDate, dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write("Line Number: " + parseScanq.LineNumber.ToString() + ", Error: "  + error.Message + ".[BDBoxInformationMain.LoadScanq]", 3);
                    throw;
                }
            }
            else
            {
                Log.Write("All scanq information has been loaded for : " + Master.BizDate + ".[BDBoxInformationMain.LoadScanq]", 1);
            }
        }

        private void LoadSegregation()
        {
            string keyValueSegregationDate = KeyValue.Get("BoxInformationSegregationDate", "", dbCnStr);

            if (!keyValueSegregationDate.Equals(Master.BizDate))
            {
                try
                {
                    ParseSegregation parseSeg = new ParseSegregation(dbCnStr, bookGroup);
                    parseSeg.Interval = 1000;
                    parseSeg.ProgressChanged += new EventHandler<ProgressEventArgs>(progressChanged);
                    parseSeg.Load(Standard.ConfigValue("FilePathSegregation"), Master.BizDatePrior, Master.BizDate);

                    Log.Write("Processed : " + parseSeg.Count.ToString("#,##0") + " Seg Items.[BDBoxInformationMain.LoadSegregation]", 3);

                    KeyValue.Set("BoxInformationSegregationDate", Master.BizDate, dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadSegregation]", 3);
                    throw;
                }
            }
            else
            {
                Log.Write("All seg information has been loaded for : " + Master.BizDate + ".[BDBoxInformationMain.LoadSegregation]", 1);
            }
        }

        private void LoadBankLoan()
        {
            string keyValueBankLoanDate = KeyValue.Get("BoxInformationBankLoanDate", "", dbCnStr);

            if (!keyValueBankLoanDate.Equals(Master.BizDate))
            {
                try
                {
                    ParseBankLoan parseBankLoan = new ParseBankLoan(dbCnStr, bookGroup);
                    parseBankLoan.ProgressChanged += new EventHandler<ProgressEventArgs>(progressChanged);
                    parseBankLoan.Load(Standard.ConfigValue("FilePathBankLoan"), Master.BizDatePrior, Master.BizDate);

                    Log.Write("Processed : " + parseBankLoan.Count.ToString("#,##0") + " Bank Loan Items.[BDBoxInformationMain.LoadBankLoan]", 3);

                    KeyValue.Set("BoxInformationBankLoanDate", Master.BizDate, dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadBankLoan]", 3);
                    throw;
                }
            }
            else
            {
                Log.Write("All bank loan information has been loaded for : " + Master.BizDate + ".[BDBoxInformationMain.LoadBankLoan]", 1);
            }
        }

        private void LoadExpend()
        {
            string keyValueExpendDate = KeyValue.Get("BoxInformationExpendDate", "", dbCnStr);

            if (!keyValueExpendDate.Equals(Master.BizDate))
            {
                try
                {
                    ParseExpend parseExpend = new ParseExpend(dbCnStr, bookGroup);
                    parseExpend.Interval = 1000;
                    parseExpend.ProgressChanged += new EventHandler<ProgressEventArgs>(progressChanged);
                    parseExpend.Load(Standard.ConfigValue("FilePathExpend"), Master.BizDatePrior, Master.BizDate);

                    Log.Write("Processed : " + parseExpend.Count.ToString("#,##0") + " Expend Items.[BDBoxInformationMain.LoadExpend]", 3);

                    KeyValue.Set("BoxInformationExpendDate", Master.BizDate, dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadExpend]", 3);
                    throw;
                }
            }
            else
            {
                Log.Write("All expend information has been loaded for : " + Master.BizDate + ".[BDBoxInformationMain.LoadExpend]", 1);
            }
        }

        private void LoadCustomerPositions()
        {           
            string positionSQL = "";

                try
                {
                    TextReader textReader = new StreamReader(Standard.ConfigValue("CustomerPositionSQL"));
                    positionSQL = textReader.ReadToEnd();

                    ParsePositions parsePos = new ParsePositions(dbCnStr, externalDbCnStr, bookGroup);
                    parsePos.Load(positionSQL, Master.BizDate);

                    Log.Write("Processed : " + parsePos.Count.ToString("#,##0") + " customer positions Items.[BDBoxInformationMain.LoadCustomerPositions]", 3);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadCustomerPositions]", 3);                    
                }
        }
       
        void progressChanged(object sender, ProgressEventArgs e)
        {
            Log.Write("Progress: " + e.Progress.ToString("#,##0"), 1);
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
				recycleInterval = KeyValue.Get("BDBoxInformationMainRecycleIntervalBizDay", "0:20", dbCn);
			}
			else
			{
				recycleInterval = KeyValue.Get("BDBoxInformationMainRecycleIntervalNonBizDay", "6:00", dbCn);
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
					KeyValue.Set("BDBoxInformationMainRecycleIntervalBizDay", "0:20", dbCn);
					hours = 0;
					minutes = 20;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [BDBoxInformationMain.RecycleInterval]", Log.Error, 1);
				}
				else
				{
					KeyValue.Set("BDBoxInformationMainRecycleIntervalNonBizDay", "6:00", dbCn);
					hours = 6;
					minutes = 0;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [BDBoxInformationMain.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("BDBoxInformationMain will recycle in " + hours + " hours, " + minutes + " minutes. [BDBoxInformationMain.RecycleInterval]", 2);
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
