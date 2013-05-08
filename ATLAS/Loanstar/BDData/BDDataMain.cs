using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using System.Collections;
using System.Xml;
using StockLoan.Common;

using BroadRidge.BusinessFiles;
using BroadRidge.MqSeries;

namespace StockLoan.BDData
{
	public class BDDataMain
	{
		private string notificationList;		
		private string dbCnStr;
		private string worldwideDbCnStr;			
	
		private Thread workerThread = null;
		private static bool isStopped = true;
		private static string tempPath;		
        
        private SqlConnection triggerDbCn;
        private SqlConnection dbCn;


		public BDDataMain(string dbCnStr, string worldwideDbCnStr)
		{
			this.dbCnStr = dbCnStr;
			this.worldwideDbCnStr = worldwideDbCnStr;
						
			try
			{
				dbCn = new SqlConnection(dbCnStr);    				
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [BDDataMain.BDDataMain]", Log.Error, 1);
			}

			if (Standard.ConfigValueExists("TempPath"))
			{
				tempPath = Standard.ConfigValue("TempPath");

				if (!Directory.Exists(tempPath))
				{
					Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [BDDataMain.BDDataMain]", Log.Error, 1);
					tempPath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
				Log.Write("A configuration value for TempPath has not been provided. [BDDataMain.BDDataMain]", Log.Information, 1);
				tempPath = Directory.GetCurrentDirectory();
			}

			Log.Write("Temporary files will be staged at " + tempPath + ". [BDDataMain.BDDataMain]", 2);
		}

		~BDDataMain()
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
				workerThread = new Thread(new ThreadStart(BDDataMainLoop));
				workerThread.Name = "Worker";
				workerThread.Start();

				Log.Write("Start command issued with new worker thread. [BDDataMain.Start]", 2);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Start command issued with worker thread already running. [BDDataMain.Start]", 2);
			}
		}

		public void Stop()
		{
			isStopped = true;

			if (workerThread == null)
			{
				Log.Write("Stop command issued, worker thread never started. [BDDataMain.Stop]", 2);
			}
			else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				workerThread.Abort();
				Log.Write("Stop command issued, sleeping worker thread aborted. [BDDataMain.Stop]", 2);
			}
			else
			{
				Log.Write("Stop command issued, worker thread is still active. [BDDataMain.Stop]", 2);
			}
		}
		
        private void BDDataMainLoop()
		{            
			while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
			{
                string waitTime = KeyValue.Get("BroadRidgeWaitTime", "04:00", dbCnStr);
 
                Log.Write("Start-of-cycle. [BDDataMain.BDDataMainLoop]", 2);
				KeyValue.Set("BDDataMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        
				Master.BizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);
				Master.BizDatePrior = KeyValue.Get("BizDatePrior", "0001-01-01", dbCn);
				Master.BizDateExchange = KeyValue.Get("BizDateExchange", "0001-01-01", dbCn);
				Master.BizDatePriorExchange = KeyValue.Get("BizDatePriorExchange", "0001-01-01", dbCn);
				Master.ContractsBizDate =	KeyValue.Get("ContractsBizDate", "0001-01-01", dbCn);
				Master.BizDateNext = KeyValue.Get("BizDateNext", "0001-01-01", dbCn);

                DateTime timeUntil = DateTime.ParseExact(waitTime, "HH:mm", null);
                
                if ((DateTime.Now >= timeUntil) && (Master.ContractsBizDate.Equals(DateTime.Now.ToString(Standard.DateFormat))))
                {
                    /*StockRecordLoad();
                    if (isStopped) break;

                    SecurityMasterLoad();
                    if (isStopped) break;                    */

                    BankLoanLoad();
                    if (isStopped) break;

                    /*EasyBorrowDistribute();
                    if (isStopped) break;

                    BoxPositionBPSLoad();
                    if (isStopped) break;

                    DtcIntraDayMessageLoad();
                    if (isStopped) break;

                    BoxPositionBPSIntraDayLoad();
                    if (isStopped) break;

                    ShortSaleNegativeRebateBillingSnapShot();
                    if (isStopped) break;*/
                }
                else
                {
                    Log.Write("Must wait until after " + waitTime + " to begin processing. [BDDataMain.BDDataMainLoop]", 1);
                }

                KeyValue.Set("BDDataMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
				Log.Write("End-of-cycle. [BDDataMain.BDDataMainLoop]", 2);

				if (!isStopped)
				{
					Thread.Sleep(RecycleInterval());
				}
			}
		}

        private void BoxPositionBPSIntraDayLoad()
        {
            if (KeyValue.Get("BroadRidgeBoxPositionLoadDate", "", dbCnStr).Equals(Master.ContractsBizDate))
            {
                DatabaseFunctions.SenderoDatabaseFunctions.BoxPositionIntradayCalc(Master.ContractsBizDate, "0158", false, dbCnStr);
                Log.Write("Loaded intraday box position for 0158. [BDDataMain.BoxPositionBPSLoad]", 1);
            }
        }

        private void DtcIntraDayMessageLoad()
        {
            try
            {
                DtcMqActivity dtcActivityLoad = new DtcMqActivity(dbCnStr);
                dtcActivityLoad.DtcRunCycle();               
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [BDDataMain.DtcIntraDayMessageLoad]", 1);
            }
        }

        
        private void BoxPositionBPSLoad()
        {

            if (KeyValue.Get("BroadRidgeStockRecordFileDate", "", dbCnStr).Equals(Master.ContractsBizDate))
            {
                int n = 0;

                try
                {
                    if (!KeyValue.Get("BroadRidgeBoxPositionLoadDate", "", dbCnStr).Equals(Master.ContractsBizDate))
                    {
                        DatabaseFunctions.SenderoDatabaseFunctions.BoxPositionBPSPurge(dbCnStr);
                        Log.Write("Purged box position bps items. [BDDataMain.BoxPositionBPSLoad]", 1);

                        DataSet dsSiac = new DataSet();
                        DataSet dsExDeficit = new DataSet();
                        DataSet dsExpend = new DataSet();
                        DataSet dsRdSweep = new DataSet();
                        DataSet dsTransfer = new DataSet();

                        Log.Write("Loading box position items. [BDDataMain.BoxPositionBPSLoad]", 1);

                       /* ExpendParser expend = new ExpendParser(Standard.ConfigValue("BroadRidgeExpendFilePath"), Master.BizDate, Master.BizDatePrior);
                        dsExpend = expend.LoadExpendRecords();

                        if (!KeyValue.Get("MemoSegEdDeficit0234Date", "", dbCnStr).Equals(Master.BizDate))
                        {
                            Log.Write("Phase3 deficit excess file has not loaded yet. [BDDataMain.BoxPositionBPSLoad]", 1);
                            throw new Exception("Phase3 deficit excess file has not loaded yet.");
                        }

                        dsExDeficit = DatabaseFunctions.MemoSegDatabaseFunctions.MemoSegStartOfDayExDeficitGet(Master.BizDate, "0234", dbCnStr);
                        Log.Write("Loaded " + dsExDeficit.Tables["ExDeficit"].Rows.Count.ToString("#,##0") + " p3 excess/deficit items. [BDDataMain.BoxPositionBPSLoad]", 1);

                        foreach (DataRow drExpend in dsExpend.Tables["Expend"].Rows)
                        {
                            foreach (DataRow drExDeficit in dsExDeficit.Tables["ExDeficit"].Rows)
                            {
                                if (drExpend["Cusip"].ToString().Equals(drExDeficit["SecId"].ToString()))
                                {
                                    drExpend["Excess"] = long.Parse(drExpend["Excess"].ToString()) + Math.Abs(long.Parse(drExDeficit["ExDeficit"].ToString()));
                                    n++;
                                }
                            }
                        }
                        
                        dsExpend.AcceptChanges();
                        Log.Write("Updated " + n.ToString("#,##0") + " excess/deficit items. [BDDataMain.BoxPositionBPSLoad]", 1);


                        n = 0;

                        /*foreach (DataRow drTemp in dsExpend.Tables["Expend"].Rows)
                        {
                            DatabaseFunctions.SenderoDatabaseFunctions.BoxPositionItemSet(
                                Master.BizDatePrior,
                                Master.BizDate,
                                "0158",
                                drTemp["Cusip"].ToString(),
                                drTemp["Excess"].ToString(),
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                dbCnStr);

                            n++;
                        }

                        Log.Write("Loaded " + n.ToString("#,##0") + " expend items. [BDDataMain.BoxPositionBPSLoad]", 1);

                        n = 0;

                        SiacParser siac = new SiacParser(Standard.ConfigValue("BroadRidgeSiac2022FilePath", ""), Master.BizDate, Master.BizDatePrior);
                        dsSiac = siac.LoadSiacReecords();

                        foreach (DataRow drTemp in dsSiac.Tables["Siac"].Rows)
                        {
                            DatabaseFunctions.SenderoDatabaseFunctions.BoxPositionItemSet(
                                Master.BizDatePrior,
                                Master.BizDate,
                                "0158",
                                drTemp["Cusip"].ToString(),
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                drTemp["CnsFTR"].ToString(),
                                drTemp["CnsFTR"].ToString(),
                                drTemp["CnsFTD"].ToString(),
                                drTemp["CnsFTD"].ToString(),
                                dbCnStr);

                            n++;
                        }

                        Log.Write("Loaded " + n.ToString("#,##0") + " siac2022 items. [BDDataMain.BoxPositionBPSLoad]", 1);

                        RdSweepParser rdSweep = new RdSweepParser(Standard.ConfigValue("BroadRidgeRdSweepFilePath", ""), Master.BizDate, Master.BizDatePrior);
                        dsRdSweep = rdSweep.LoadRdSweepRecords();

                        n = 0;

                        foreach (DataRow drTemp in dsRdSweep.Tables["RdSweepSummary"].Rows)
                        {
                            DatabaseFunctions.SenderoDatabaseFunctions.BoxPositionItemSet(
                                Master.BizDatePrior,
                                Master.BizDate,
                                "0158",
                                drTemp["Cusip"].ToString(),
                                "",
                                "",
                                drTemp["COR-S"].ToString(),
                                drTemp["COR-P"].ToString(),
                                drTemp["COD-S"].ToString(),
                                drTemp["COD-P"].ToString(),
                                drTemp["FTR-S"].ToString(),
                                drTemp["FTR-P"].ToString(),
                                drTemp["FTD-S"].ToString(),
                                drTemp["FTD-P"].ToString(),
                                "",
                                "",
                                "",
                                "",
                                dbCnStr);

                            n++;
                        }

                        Log.Write("Loaded " + n.ToString("#,##0") + " rdsweep items. [BDDataMain.BoxPositionBPSLoad]", 1);

                        */
                        TransferParser transfer = new TransferParser(Standard.ConfigValue("BroadRidgeTransferFilePath", ""), Master.BizDate, Master.BizDatePrior);
                        dsTransfer = transfer.LoadTransferRecords();

                        n = 0;

                        foreach (DataRow drTemp in dsTransfer.Tables["Transfer"].Rows)
                        {
                            DatabaseFunctions.SenderoDatabaseFunctions.BoxPositionItemSet(
                                Master.BizDatePrior,
                                Master.BizDate,
                                "0158",
                                drTemp["Cusip"].ToString(),
                                "",
                                drTemp["Quantity"].ToString(),
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                dbCnStr);

                            n++;
                        }

                        Log.Write("Loaded " + n.ToString("#,##0") + " transfer items. [BDDataMain.BoxPositionBPSLoad]", 1);


                        DatabaseFunctions.SenderoDatabaseFunctions.BoxPositionBPSSegReqCalc(dbCnStr);
                        KeyValue.Set("BPSSegReqCalc", Master.BizDate, dbCnStr);
                        Log.Write("Calculated segregation requirement. [BDDataMain.BoxPositionBPSLoad]", 1);

                        DatabaseFunctions.SenderoDatabaseFunctions.BoxPositionBPSLongShortCalc(dbCnStr);
                        KeyValue.Set("BPSLongShortCalc", Master.BizDate, dbCnStr);
                        Log.Write("Calculated long / short positions. [BDDataMain.BoxPositionBPSLoad]", 1);

                        DatabaseFunctions.SenderoDatabaseFunctions.BoxPositionBPSCustomerLongCalc(Master.ContractsBizDate, "0158", dbCnStr);
                        KeyValue.Set("BPSCustomerLongCalc", Master.BizDate, dbCnStr);
                        Log.Write("Calculated customer long positions. [BDDataMain.BoxPositionBPSLoad]", 1);

                        DatabaseFunctions.SenderoDatabaseFunctions.BoxPositionPurge(Master.ContractsBizDate, "0158", dbCnStr);
                        KeyValue.Set("BPSBoxPositionPurge", Master.BizDate, dbCnStr);
                        Log.Write("Purged box position for 0158. [BDDataMain.BoxPositionBPSLoad]", 1);

                        DatabaseFunctions.SenderoDatabaseFunctions.BoxPositionIntradayCalc(Master.ContractsBizDate, "0158", true, dbCnStr);
                        KeyValue.Set("BPSBoxPositionIntradayCalc", Master.BizDate, dbCnStr);
                        Log.Write("Inital box position load for 0158 completed. [BDDataMain.BoxPositionBPSLoad]", 1);

                        DatabaseFunctions.SenderoDatabaseFunctions.BoxPositionBPSDayCountCalc(Master.BizDatePrior, Master.BizDate, "0158", dbCnStr);
                        KeyValue.Set("BPSBoxPositionDayCountCalc", Master.BizDate, dbCnStr);

                        KeyValue.Set("BroadRidgeBoxPositionLoadDate", Master.ContractsBizDate, dbCnStr);
                    }
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + "[BDDataMain.BoxPositionBPSLoad]", 1);
                }
            }
        }

        private void ShortSaleNegativeRebateBillingSnapShot()
        {
            string listMakeTime;

            if (!Standard.IsBizDate(DateTime.UtcNow.Date, "US", Standard.HolidayType.Bank, dbCn))
            {
                if (KeyValue.Get("ShortSaleNegRebateBillingBPSSnapShotBizDate", "", dbCn).Equals(DateTime.UtcNow.ToString(Standard.DateFormat)))
                {
                    Log.Write("Short Sale Negative Rebate Billing Weekend Snapshot is current for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
                }
                else // do weekend snapshot
                {
                    SqlCommand sqlCommand;

                    Log.Write("Doing Short Sale Negative Rebate Billing Weekend Snapshot for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);

                    try
                    {
                        sqlCommand = new SqlCommand("spShortSaleBillingSummaryBPSOvernightWeekendSnapShot", dbCn);
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        SqlParameter paramBizDatePrior = sqlCommand.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                        paramBizDatePrior.Value = Master.BizDatePrior;

                        SqlParameter paramBizDate = sqlCommand.Parameters.Add("@BizDate", SqlDbType.DateTime);
                        paramBizDate.Value = DateTime.UtcNow.ToString(Standard.DateFormat);

                        dbCn.Open();
                        sqlCommand.ExecuteNonQuery();
                        dbCn.Close();

                        KeyValue.Set("ShortSaleNegRebateBillingBPSSnapShotBizDate", DateTime.UtcNow.ToString(Standard.DateFormat), dbCn);
                    }
                    catch (Exception e)
                    {
                        Log.Write(e.Message + " [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", Log.Error, 1);
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
                if (KeyValue.Get("ShortSaleNegRebateBillingBPSSnapShotBizDate", "", dbCn).Equals(Master.BizDate))
                {
                    Log.Write("Short Sale Negative Rebate Billing Snapshot is current for " + Master.ContractsBizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
                }
                else if (!Master.ContractsBizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
                {
                    Log.Write("Contracts data has not been loaded for " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
                }
                else if (!KeyValue.Get("PensonSecurityStaticBizDate", "", dbCn).Equals(Master.BizDatePrior))
                {
                    Log.Write("Penson security static data has not loaded yet. [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
                }
                else if (!DateTime.Parse(KeyValue.Get("BroadRidgeBoxPositionLoadDate", "", dbCn)).ToString(Standard.DateFormat).Equals(Master.ContractsBizDate))
                {
                    Log.Write("BroadRidge box position data has not been loaded yet. [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
                }
                else
                {
                    if (Master.ContractsBizDate.Equals(DateTime.UtcNow.ToString(Standard.DateFormat)))
                    {
                        listMakeTime = KeyValue.Get("ShortSaleNegRebateBillingSnapShotTime", "23:00", dbCn);

                        if (listMakeTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0)
                        {
                            Log.Write("Doing Short Sale Negative Rebate Billing  Buisness Day Snapshot for " + DateTime.UtcNow.ToString(Standard.DateFormat) + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);

                            SqlCommand sqlDbCmd = new SqlCommand("spShortSaleBillingSummaryBPSOvernightSnapShotControl", dbCn);
                            sqlDbCmd.CommandType = CommandType.StoredProcedure;
                            sqlDbCmd.CommandTimeout = int.Parse(KeyValue.Get("ShortSaleNegRebateBillingSnapShotTimeOut", "300", dbCn));

                            try
                            {
                                dbCn.Open();
                                sqlDbCmd.ExecuteNonQuery();
                                dbCn.Close();

                                KeyValue.Set("ShortSaleNegRebateBillingBPSSnapShotBizDate", Master.BizDate, dbCn);
                                KeyValue.Set("ShortSaleNegRebateBillingBPSSnapShotDateTime", DateTime.Now.ToString(Standard.DateTimeFormat), dbCn);
                                Log.Write("ShortSale Negative Rebate Billing Snap Shot done for:  " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
                            }
                            catch (Exception e)
                            {
                                Log.Write(e.Message + " [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", Log.Error, 1);
                            }
                            finally
                            {
                                if (dbCn.State != ConnectionState.Closed)
                                {
                                    dbCn.Close();
                                }
                            }
                        }
                        else
                        {
                            Log.Write("Must wait until after " + listMakeTime + " UTC to do snapshopt for " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
                        }
                    }
                    else
                    {
                        Log.Write("Must wait until trade date, " + Master.BizDate + ". [MedalistMain.ShortSaleNegativeRebateBillingSnapShot]", 2);
                    }
                }
            }
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
                        
                        Log.Write("Uploaded ETB list to " + Standard.ConfigValue("BroadRidgeETBHost", "") + ". [BDDataMain.EasyBorrowDistribute]", 1);
                        KeyValue.Set("BroadRidgeEasyBorrowFileDate", Master.ContractsBizDate, dbCnStr);
                    }
                    else
                    {
                        Log.Write("Already distributed etb lsit to broadridge for  " + Master.ContractsBizDate + ". [BDDataMain.EasyBorrowDistribute]", 1);
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
        
        private void BankLoanLoad()
        {
            DataSet dsPendingTrades = new DataSet();
            long counter = 0;

            string filePath = Standard.ConfigValue("BroadRidgeApibalFilePath", "");

            /*if (KeyValue.Get("BankLoanBizDate0234", "", dbCnStr).Equals(Master.BizDatePrior))
            {
                try
                {
                    if (!KeyValue.Get("BroadRidgeApibalFileDate", "", dbCnStr).Equals(Master.ContractsBizDate))
                    {*/
                        BankLoanApibalParser bankLoanApibalParser = new BankLoanApibalParser(filePath, Master.BizDatePrior, Master.BizDate, "0158", dbCnStr);
                        
                        bankLoanApibalParser.Load();
                        bankLoanApibalParser.LoadDatabase();

                        KeyValue.Set("BroadRidgeApibalFileDate", Master.ContractsBizDate, dbCnStr);
                        KeyValue.Set("BankLoanBizDate0158", Master.BizDatePrior, dbCnStr);
                        KeyValue.Set("BankLoanDtcReset0158", "true", dbCnStr);
            /*        }
                    else
                    {
                        Log.Write("Already loaded broadridge apibal file for  " + Master.ContractsBizDate + ". [BDDataMain.BankLoanLoad]", 1);
                    }
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [BDDataMain.BankLoanLoad]", 1);
                }
            }
            else
            {
                Log.Write("Must wait until after penson bank loan Load completes. [BDDataMain.BankLoanLoad]", 1);
            }*/
        }

        private void SecurityMasterLoad()
        {
            DataSet dsSecurityMaster = new DataSet();
            long counter = 0;

            string filePath = Standard.ConfigValue("BroadRidgeSecurityMasterFilePath", "");

            if (KeyValue.Get("PensonSecurityStaticBizDate", "", dbCnStr).Equals(Master.BizDatePrior))
            {
                try
                {
                    if (!KeyValue.Get("BroadRidgeSecurityMasterFileDate", "", dbCnStr).Equals(Master.ContractsBizDate))
                    {
                        SecMasterParser secMasterParser = new SecMasterParser(filePath, Master.BizDatePrior, dbCnStr);
                        secMasterParser.Load();

                        KeyValue.Set("BroadRidgeSecurityMasterFileDate", Master.ContractsBizDate, dbCnStr);
                    }
                    else
                    {
                        Log.Write("Already loaded broadridge security master file for  " + Master.ContractsBizDate + ". [BDDataMain.SecurityMasterLoad]", 1);
                    }
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [BDDataMain.SecurityMasterLoad]", 1);
                }
            }
            else
            {
                Log.Write("Must wait until after Penson Security Data Load completes. [BDDataMain.SecurityMasterLoad]", 1);
            }
        }

        private void PendingTradesLoad()
        {
            DataSet dsPendingTrades = new DataSet();
            long counter = 0;

            string filePath = Standard.ConfigValue("BroadRidgePendingTradesFilePath", "");

            if (KeyValue.Get("PensonSecurityStaticBizDate", "", dbCnStr).Equals(Master.BizDatePrior))
            {
                if (KeyValue.Get("BroadRidgeStockRecordFileDate", "", dbCnStr).Equals(Master.BizDate))
                {
                    try
                    {
                        if (!KeyValue.Get("BroadRidgePendingTradesFileDate", "", dbCnStr).Equals(Master.ContractsBizDate))
                        {                            
                            PendingTradesParser pendingTradesParser = new PendingTradesParser(filePath, Master.BizDatePrior, dbCnStr);
                            pendingTradesParser.LoadPendingTrades();                            
                            
                            KeyValue.Set("BroadRidgePendingTradesFileDate", Master.ContractsBizDate, dbCnStr);
                        }
                        else
                        {
                            Log.Write("Already loaded broadridge pending trades file for  " + Master.ContractsBizDate + ". [BDDataMain.PendingTradesLoad]", 1);
                        }
                    }
                    catch (Exception error)
                    {
                        Log.Write(error.Message + " [BDDataMain.PendingTradesLoad]", 1);
                    }
                }
                else
                {
                    Log.Write("Must wait until after BPS Stock Record Load completes. [BDDataMain.PendingTradesLoad]", 1);
                }
            }
            else
            {
                Log.Write("Must wait until after Penson Security Data Load completes. [BDDataMain.PendingTradesLoad]", 1);
            }
        }

        private void StockRecordLoad()
        {
            DataSet dsStockRecord = new DataSet();          

            string filePath = Standard.ConfigValue("BroadRidgeStockRecordFilePath", "");

            if (KeyValue.Get("PensonSecurityStaticBizDate", "", dbCnStr).Equals(Master.BizDatePrior))
            {
                try
                {
                    if (!KeyValue.Get("BroadRidgeStockRecordFileDate", "", dbCnStr).Equals(Master.ContractsBizDate))
                    {
                        StockRecordParser stockRecordParser = new StockRecordParser(filePath, Master.BizDatePrior, Master.BizDate, dbCnStr);
                        stockRecordParser.Load();

                        KeyValue.Set("BroadRidgeStockRecordFileDate", Master.ContractsBizDate, dbCnStr);
                    }
                    else
                    {
                        Log.Write("Already loaded broadridge stock record file for  " + Master.ContractsBizDate + ". [BDDataMain.StockRecordLoad]", 1);
                    }
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [BDDataMain.StockRecordLoad]", 1);
                }
            }
            else
            {
                Log.Write("Must wait until after Penson Security Data Load completes. [BDDataMain.StockRecordLoad]", 1);
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
				recycleInterval = KeyValue.Get("BDDataMainRecycleIntervalBizDay", "0:20", dbCn);
			}
			else
			{
				recycleInterval = KeyValue.Get("BDDataMainRecycleIntervalNonBizDay", "6:00", dbCn);
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
					KeyValue.Set("BDDataMainRecycleIntervalBizDay", "0:20", dbCn);
					hours = 0;
					minutes = 20;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [BDDataMain.RecycleInterval]", Log.Error, 1);
				}
				else
				{
					KeyValue.Set("BDDataMainRecycleIntervalNonBizDay", "6:00", dbCn);
					hours = 6;
					minutes = 0;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [BDDataMain.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("BDDataMain will recycle in " + hours + " hours, " + minutes + " minutes. [BDDataMain.RecycleInterval]", 2);
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
