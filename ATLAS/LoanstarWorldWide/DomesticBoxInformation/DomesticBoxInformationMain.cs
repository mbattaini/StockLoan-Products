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
using StockLoan.DataAccess;

namespace StockLoan.DomesticBoxInformation
{
	public class BDBoxInformationMain
	{
		private string notificationList;		
		private string dbCnStr;
		private string externalDbCnStr;			
	
		private Thread workerThread = null;
        private Thread mqThread = null;
		private static bool isStopped = true;
		private static string tempPath;		
        
        private SqlConnection triggerDbCn;
        private SqlConnection dbCn;
        private string bookGroup;
        
        private string preWaitTime;
        private string sodWaitTime;
        private string eodWaitTime;

        private int mqCycle;

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
			else
			{
				Log.Write("Start command issued with worker thread already running. [BDBoxInformationMain.Start]", 2);
			}


            if ((mqThread == null) || (!mqThread.IsAlive)) // Must create new thread.
            {
                mqThread = new Thread(new ThreadStart(BDBoxDtcActivityLoop));
                mqThread.Name = "mq domestic";
                mqThread.Start();

                Log.Write("Start command issued with new mq thread. [BDBoxInformationMain.Start]", 2);
            }
            else
            {
                Log.Write("Start command issued with mq thread already running. [BDBoxInformationMain.Start]", 2);
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

            if (mqThread == null)
            {
                Log.Write("Stop command issued, mq thread never started. [BDBoxInformationMain.Stop]", 2);
            }
            else if (mqThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
            {
                mqThread.Abort();
                Log.Write("Stop command issued, sleeping mq thread aborted. [BDBoxInformationMain.Stop]", 2);
            }
            else
            {
                Log.Write("Stop command issued, mq thread is still active. [BDBoxInformationMain.Stop]", 2);
            }
		}

        private void BDBoxDtcActivityLoop()
        {
            mqCycle = int.Parse(Standard.ConfigValue("DoDtcMqCycleTime", "36000"));

            if (!bool.Parse(Standard.ConfigValue("LoadMQChannel")))
            {
                Log.Write("MQ Channel not loaded. [BDBoxInformationMain.BDBoxDtcActivityLoop]", 1);
                return;
            }

            while (!isStopped) 
            {
                Master.BizDate = KeyValue.Get("BizDate", "", dbCnStr);
                Master.BizDatePrior = KeyValue.Get("BizDatePrior", "", dbCnStr);
                Master.BizDateExchange = KeyValue.Get("BizDateExchange", "", dbCnStr);
                Master.BizDatePriorExchange = KeyValue.Get("BizDatePriorExchange", "", dbCnStr);
                Master.ContractsBizDate = KeyValue.Get("ContractsBizDate", "", dbCnStr);
                Master.BizDateNext = KeyValue.Get("BizDateNext", "", dbCnStr);

                

                if (bool.Parse(Standard.ConfigValue("LoadDtcMQMessages")))
                {
                    LoadDtcInformation(); 
                }
                else
                {
                    Log.Write("Load DTCC mq messages is disabled. [BDBoxInformationMain.BDBoxDtcActivityLoop]", 1);
                }
                if (isStopped) { break; }

                if (bool.Parse(Standard.ConfigValue("ProcessDtcMQMessages")))
                {
                    ProcessDtcInformation();
                }
                else
                {
                    Log.Write("Process DTCC mq messages is disabled. [BDBoxInformationMain.BDBoxDtcActivityLoop]", 1);
                }
                if (isStopped) { break; }

                if (bool.Parse(Standard.ConfigValue("DoDtcIntradayCalculations")))
                {
                    LoadIntraDayBoxPosition();
                }
                else
                {
                    Log.Write("Intra-day DTCC calculations is disabled. [BDBoxInformationMain.BDBoxDtcActivityLoop]", 1);
                }
                if (isStopped) { break; }

                if (!isStopped)
                {
                    Thread.Sleep(36000);
                }
            }
        }

        private void BDBoxInformationMainLoop()
        {
            while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
            {               
                Log.Write("Start-of-cycle. [BDBoxInformationMain.BDBoxInformationMainLoop]", 2);
                KeyValue.Set("BDBoxInformationMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);

                bookGroup = "0158";

                Master.BizDate = KeyValue.Get("BizDate", "", dbCnStr);
                Master.BizDatePrior = KeyValue.Get("BizDatePrior", "", dbCnStr);
                Master.BizDateExchange = KeyValue.Get("BizDateExchange", "", dbCnStr);
                Master.BizDatePriorExchange = KeyValue.Get("BizDatePriorExchange", "", dbCnStr);
                Master.ContractsBizDate = KeyValue.Get("ContractsBizDate", "", dbCnStr);
                Master.BizDateNext = KeyValue.Get("BizDateNext", "", dbCnStr);
                
                sodWaitTime = Standard.ConfigValue("BroadRidgeSodWaitTime", "05:00");
                eodWaitTime = Standard.ConfigValue("BroadRidgeEodWaitTime", "23:00");
                
                if (TimeCheck(sodWaitTime, "Start Of Day Load"))
                {
                    if (bool.Parse(Standard.ConfigValue("LoadSecMaster")))
                    {
                        LoadSecMaster();
                    }
                    else
                    {
                        Log.Write("Load Security Master is disabled. [BDBoxInformationMain.BDBoxInformationMainLoop]", 1);
                    }
                    
                    LoadFails();
                    LoadSegregation();
                    LoadBankLoan();
                    LoadExpend();
                    LoadScanq();
                    LoadCustomerPositions();
                    LoadStockRecord();
                    LoadBoxPosition();
                }
                if (isStopped) { break; }

                EasyBorrowDistribute();
                if (isStopped) { break; }

                LoadCnsTesting();
                if (isStopped) { break; }

                if (TimeCheck(eodWaitTime, "End Of Day Load"))
                {
                    if (bool.Parse(Standard.ConfigValue("DoBoxPositionAgeFails")))
                    {
                        LoadBoxPositionAgeFails();
                    }
                    else
                    {
                        Log.Write("Do Box Position Age Fails is disabled. [BDBoxInformationMain.BDBoxInformationMainLoop]", 1);
                    }
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

        private void LoadDtcInformation()
        {
            try
            {
                LoadDtcAcitivty loadDtcActy = new LoadDtcAcitivty(Master.BizDate, dbCnStr);
                loadDtcActy.Load();
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ".[BDBoxInformationMain.LoadDtcInformation]", 3);
            } 
        }

        private void ProcessDtcInformation()
        {
            try
            {
                ProcessDtcAcitivty processDtcActy = new ProcessDtcAcitivty(Master.BizDate, dbCnStr);
                processDtcActy.DtcMessageLoad();
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ".[BDBoxInformationMain.ProcessDtcInformation]", 3);
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
                    ParseSecMaster parseSecMaster = new ParseSecMaster(dbCnStr, bookGroup);
                    parseSecMaster.Load(secMasterSQL, Master.BizDatePrior);
                    
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
                Log.Write("All security master information has been loaded for : " + keyValueSecMasterDate + ".[BDBoxInformationMain.LoadSecMaster]", 1);
            }
        }

        void parseSecMaster_ProgressChanged(object sender, ProgressEventArgs e)
        {
            Log.Write("Progress: " + e.Progress.ToString("#,##0"), 1);
        }

        private void LoadStockRecord()
        {
            string filePath = Standard.ConfigValue("FilePathStockRecord", "");

            if (KeyValue.Get("PensonSecurityStaticBizDate", "", dbCnStr).Equals(Master.BizDatePrior))
            {                
                try
                {
                    if (!KeyValue.Get("BoxInformationStockRecordDate", "", dbCnStr).Equals(Master.ContractsBizDate))
                    {
                        ParseStockRecord stockRecordParser = new ParseStockRecord(dbCnStr, bookGroup);
                        stockRecordParser.Load(filePath, Master.BizDatePrior, Master.BizDate);

                        KeyValue.Set("BoxInformationStockRecordDate", Master.ContractsBizDate, dbCnStr);

                        Email.Send(
                            Standard.ConfigValue("SupportMail"),
                            Standard.ConfigValue("SystemMail"),
                            "System Status " + Master.BizDate,
                            "Stock Record loaded For " + Master.BizDate + "; BookGroup = " + bookGroup,
                            dbCnStr);
                    }
                    else
                    {
                        Log.Write("Already loaded broadridge stock record file for  " + Master.ContractsBizDate + ". [BDBoxInformationMain.LoadStockRecord]", 1);
                    }
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [BDBoxInformationMain.LoadStockRecord]", 1);
                }
            }
            else
            {
                Log.Write("Must wait until after Penson Security Data Load completes. [BDBoxInformationMain.LoadStockRecord]", 1);
            }
        }

        private void LoadBoxPosition()
        {
            string keyValueBoxPositionDate = KeyValue.Get("BoxInformationBoxPositionLoadDate", "", dbCnStr);

            if (!keyValueBoxPositionDate.Equals(Master.BizDate))
            {               
                try
                {
                    if (BoxPositionCheckList())
                    {
                        LoadBoxPosition loadBoxPosition = new LoadBoxPosition(dbCnStr);
                        loadBoxPosition.Purge(Master.BizDate, bookGroup);
                        loadBoxPosition.Load(Master.BizDatePrior, Master.BizDate, bookGroup);

                        KeyValue.Set("BroadRidgeBoxPositionLoadDate", Master.BizDate, dbCnStr);
                        KeyValue.Set("BoxInformationBoxPositionLoadDate", Master.BizDate, dbCnStr);


                        Email.Send(
                            Standard.ConfigValue("SupportMail"),
                            Standard.ConfigValue("SystemMail"),
                            "System Status " + Master.BizDate,
                            "Box Position loaded For " + Master.BizDate + "; BookGroup = " + bookGroup,
                            dbCnStr);
                    }
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadBoxPosition]", 3);
                }
            }
            else
            {
                Log.Write("All box position has been loaded for : " + keyValueBoxPositionDate + ".[BDBoxInformationMain.LoadBoxPosition]", 1);
            }
        }

        private void LoadCnsTesting()
        {
            string keyValueCnsTestingDate = KeyValue.Get("BoxInformationCnsTestingLoadDate", "", dbCnStr);

            if (!keyValueCnsTestingDate.Equals(Master.BizDate))
            {
                try
                {
                    if (BoxPositionCheckList())
                    {
                        LoadCnsTesting loadCnsTesting = new LoadCnsTesting(dbCnStr);
                        loadCnsTesting.Load(Master.BizDatePrior, Master.BizDate, bookGroup);

                        KeyValue.Set("BoxInformationCnsTestingLoadDate", Master.BizDate, dbCnStr);


                        Email.Send(
                            Standard.ConfigValue("SupportMail"),
                            KeyValue.Get("BoxInformationDeskMail", "mbattaini@penson.com,bhall@penson.com,lwetzig@penson.com", dbCnStr),
                            "System Status " + Master.BizDate,
                            "Cns 204 testing loaded For " + Master.BizDate + "; BookGroup = " + bookGroup,
                            dbCnStr);
                    }
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadCnsTesting]", 3);
                }
            }
            else
            {
                Log.Write("Cns testing has been loaded for : " + keyValueCnsTestingDate + ".[BDBoxInformationMain.LoadCnsTesting]", 1);
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

                    Email.Send(
                        Standard.ConfigValue("SupportMail"),
                        Standard.ConfigValue("SystemMail"),
                        "System Status " + Master.BizDate,
                        "Aged fails For " + Master.BizDate + "; BookGroup = " + bookGroup,
                        dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadBoxPositionAgeFails]", 3);
                }
            }
            else
            {
                Log.Write("All box position fails has been aged loaded for : " + keyValueBoxPositionDate + ".[BDBoxInformationMain.LoadBoxPositionAgeFails]", 1);
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

                    Email.Send(
                        Standard.ConfigValue("SupportMail"),
                        Standard.ConfigValue("SystemMail"),
                        "System Status " + Master.BizDate,
                        "CNS Fails loaded for " + Master.BizDate + "; BookGroup = " + bookGroup + "; Items = " + parseClearing.Count.ToString("#,##0"),
                        dbCnStr);
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

                    Email.Send(
                        Standard.ConfigValue("SupportMail"),
                        Standard.ConfigValue("SystemMail"),
                        "System Status " + Master.BizDate,
                        "DVP And Broker Fails loaded for " + Master.BizDate + "; BookGroup = " + bookGroup + "; Items = " + parseFails.Count.ToString("#,##0"),
                        dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadFails]", 3);
                    throw;
                }
            }
            else
            {
                Log.Write("All fails information has been loaded for : " + keyValueClearingFailDate + " and " + keyValueOtherFailDate + ".[BDBoxInformationMain.LoadFails]", 1);
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

                    Email.Send(
                      Standard.ConfigValue("SupportMail"),
                      Standard.ConfigValue("SystemMail"),
                      "System Status " + Master.BizDate,
                      "Scanq loaded for " + Master.BizDate + "; BookGroup = " + bookGroup + "; Items = " + parseScanq.Count.ToString("#,##0"),
                      dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write("Line Number: " + parseScanq.LineNumber.ToString() + ", Error: "  + error.Message + ".[BDBoxInformationMain.LoadScanq]", 3);
                    throw;
                }
            }
            else
            {
                Log.Write("All scanq information has been loaded for : " + keyValueScanqDate + ".[BDBoxInformationMain.LoadScanq]", 1);
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

                    Email.Send(
                        Standard.ConfigValue("SupportMail"),
                        Standard.ConfigValue("SystemMail"),
                        "System Status " + Master.BizDate,
                        "Segregation loaded for " + Master.BizDate + "; BookGroup = " + bookGroup + "; Items = " + parseSeg.Count.ToString("#,##0"),
                        dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadSegregation]", 3);
                    throw;
                }
            }
            else
            {
                Log.Write("All seg information has been loaded for : " + keyValueSegregationDate + ".[BDBoxInformationMain.LoadSegregation]", 1);
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

                    Email.Send(
                       Standard.ConfigValue("SupportMail"),
                       Standard.ConfigValue("SystemMail"),
                       "System Status " + Master.BizDate,
                       "BankLoan loaded for " + Master.BizDate + "; BookGroup = " + bookGroup + "; Items = " + parseBankLoan.Count.ToString("#,##0"),
                       dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadBankLoan]", 3);
                    throw;
                }
            }
            else
            {
                Log.Write("All bank loan information has been loaded for : " + keyValueBankLoanDate + ".[BDBoxInformationMain.LoadBankLoan]", 1);
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


                    Email.Send(
                       Standard.ConfigValue("SupportMail"),
                       Standard.ConfigValue("SystemMail"),
                       "System Status " + Master.BizDate,
                       "Expend loaded for " + Master.BizDate + "; BookGroup = " + bookGroup + "; Items = " + parseExpend.Count.ToString("#,##0"),
                       dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadExpend]", 3);
                    throw;
                }
            }
            else
            {
                Log.Write("All expend information has been loaded for : " + keyValueExpendDate + ".[BDBoxInformationMain.LoadExpend]", 1);
            }
        }

        private void LoadCustomerPositions()
        {
            string positionSQL = "";

            string keyValueCustomerLoadDate = KeyValue.Get("BoxInformationCustomerPositionsDate", "", dbCnStr);

            if (!keyValueCustomerLoadDate.Equals(Master.BizDate))
            {
                try
                {
                    TextReader textReader = new StreamReader(Standard.ConfigValue("CustomerPositionSQL"));
                    positionSQL = textReader.ReadToEnd();

                    ParsePositions parsePos = new ParsePositions(dbCnStr, dbCnStr, bookGroup);
                    parsePos.Load(positionSQL, Master.BizDate);

                    Log.Write("Processed : " + parsePos.Count.ToString("#,##0") + " customer positions Items.[BDBoxInformationMain.LoadCustomerPositions]", 3);


                    KeyValue.Set("BoxInformationCustomerPositionsDate", Master.BizDate, dbCnStr);


                    Email.Send(
                       Standard.ConfigValue("SupportMail"),
                       Standard.ConfigValue("SystemMail"),
                       "System Status " + Master.BizDate,
                       "Customer Positions loaded for " + Master.BizDate + "; BookGroup = " + bookGroup + "; Items = " + parsePos.Count.ToString("#,##0"),
                       dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + ".[BDBoxInformationMain.LoadCustomerPositions]", 3);
                }
            }
            else
            {
                Log.Write("All customer positions have been loaded for : " + keyValueCustomerLoadDate + ".[BDBoxInformationMain.LoadSegregation]", 1);
            }
        }              
       
        void progressChanged(object sender, ProgressEventArgs e)
        {
            Log.Write("Progress: " + e.Progress.ToString("#,##0"), 1);
        }

        private void EasyBorrowDistribute()
        {

            if (KeyValue.Get("EasyBorrowFileDate", "", dbCnStr).Equals(Master.BizDate))
            {
                try
                {
                    if (!KeyValue.Get("BroadRidgeEasyBorrowFileDate", "", dbCnStr).Equals(Master.ContractsBizDate))
                    {
                        EasyBorrowFormat easyBorrowFormat = new EasyBorrowFormat(Master.ContractsBizDate,
                               Standard.ConfigValue("BroadRidgeETBHost", ""),
                               Standard.ConfigValue("BroadRidgeETBFilePath", ""),
                               Standard.ConfigValue("BroadRidgeETBUserId", ""),
                               Standard.ConfigValue("BroadRidgeETBPassword", ""),
                               "XXXX",
                               dbCnStr);

                        easyBorrowFormat.EasyBorrowBroadRidgeFileList();

                        Log.Write("Uploaded ETB list to " + Standard.ConfigValue("BroadRidgeETBHost", "") + ". [BDBoxInformationMain.EasyBorrowDistribute]", 1);
                        KeyValue.Set("BroadRidgeEasyBorrowFileDate", Master.ContractsBizDate, dbCnStr);
                    }
                    else
                    {
                        Log.Write("Already distributed etb lsit to broadridge for  " + Master.ContractsBizDate + ". [BDBoxInformationMain.EasyBorrowDistribute]", 1);
                    }
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [BDDataMain.EasyBorrowDistribute]", 1);
                }
            }
            else
            {
                Log.Write("Must wait until after penson etb list is created. [BDDataMain.EasyBorrowDistribute]", 1);
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

        public bool BoxPositionCheckList()
        {
            bool load = false;

            string keyValueClearingFailDate = KeyValue.Get("BoxInformationClearingFailDate", "", dbCnStr);
            string keyValueOtherFailDate = KeyValue.Get("BoxInformationOtherFailDate", "", dbCnStr);
            string keyValueScanqDate = KeyValue.Get("BoxInformationScanqDate", "", dbCnStr);
            string keyValueSegregationDate = KeyValue.Get("BoxInformationSegregationDate", "", dbCnStr);
            string keyValueBankLoanDate = KeyValue.Get("BoxInformationBankLoanDate", "", dbCnStr);
            string keyValueExpendDate = KeyValue.Get("BoxInformationExpendDate", "", dbCnStr);
            string keyValueCustomerLoadDate = KeyValue.Get("BoxInformationCustomerPositionsDate", "", dbCnStr);

            try
            {
                load = (keyValueBankLoanDate.Equals(Master.BizDate)) ? true : CheckListError("Bank Loan File");
                load = (keyValueClearingFailDate.Equals(Master.BizDate)) ? true : CheckListError("Clearing Fail File");
                load = (keyValueOtherFailDate.Equals(Master.BizDate)) ? true : CheckListError("Other Fail File");
                load = (keyValueScanqDate.Equals(Master.BizDate)) ? true : CheckListError("ScanQ File");
                load = (keyValueSegregationDate.Equals(Master.BizDate)) ? true : CheckListError("Segregation File");
                load = (keyValueExpendDate.Equals(Master.BizDate)) ? true : CheckListError("Expend File");
                load = (keyValueCustomerLoadDate.Equals(Master.BizDate)) ? true : CheckListError("Customer Positions");
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
                load = false;
            }
                        
            return load;
        }

        public bool CheckListError(string file)
        {
            throw (new Exception(file + "is not for today."));
            
            return false;
        }
	}
}
