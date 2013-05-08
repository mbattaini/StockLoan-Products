using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;

using StockLoan.Common;
using StockLoan.Transport;

namespace StockLoan.TradingData
{
    public class TradingData
    {
        private string countryCode;

        private string dbCnStr;
        private SqlConnection dbCn = null;

        private Thread mainThread = null;

        private static bool isStopped = true;
        private static string tempPath;

        public TradingData(string dbCnStr)
        {
            this.dbCnStr = dbCnStr;

            try
            {
                dbCn = new SqlConnection(dbCnStr);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [TradingData.TradingData]", Log.Error, 1);
            }

            countryCode = Standard.ConfigValue("CountryCode", "US");
            Log.Write("Using country code: " + countryCode + " [TradingData.TradingData]", 2);

            if (Standard.ConfigValueExists("TempPath"))
            {
                tempPath = Standard.ConfigValue("TempPath");

                if (!Directory.Exists(tempPath))
                {
                    Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [TradingData.TradingData]", Log.Error, 1);
                    tempPath = Directory.GetCurrentDirectory();
                }
            }
            else
            {
                Log.Write("A configuration value for TempPath has not been provided. [TradingData.TradingData]", Log.Information, 1);
                tempPath = Directory.GetCurrentDirectory();
            }

            Log.Write("Temporary files will be staged at " + tempPath + ". [TradingData.TradingData]", 2);
        }

        ~TradingData()
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
                mainThread = new Thread(new ThreadStart(TradingDataLoop));
                mainThread.Name = "Main";
                mainThread.Start();

                Log.Write("Start command issued with new main thread. [TradingData.Start]", 3);
            }
            else // Old thread will be just fine.
            {
                Log.Write("Start command issued with main thread already running. [TradingData.Start]", 3);
            }
        }

        public void Stop()
        {
            isStopped = true;

            if (mainThread == null)
            {
                Log.Write("Stop command issued, main thread never started. [TradingData.Stop]", 3);
            }
            else if (mainThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
            {
                mainThread.Abort();
                Log.Write("Stop command issued, sleeping main thread aborted. [TradingData.Stop]", 3);
            }
            else
            {
                Log.Write("Stop command issued, main thread is still active. [TradingData.Stop]", 3);
            }
        }

        private void TradingDataLoop()
        {
            while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
            {
                Log.Write("Start of cycle. [TradingData.TradingDataLoop]", 2);
                KeyValue.Set("TradingDataCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);

                Master.BizDate = KeyValue.Get("BizDate", "", dbCnStr);
                Master.BizDatePrior = KeyValue.Get("BizDatePrior", "", dbCnStr);

                GatewayResponsesGet();
                if (isStopped)
                { break; }

                ReportGet();
                if (isStopped)
                { break; }

                PirumFileUpload();
                if (isStopped)
                { break; }

                KeyValue.Set("TradingDataCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
                Log.Write("End of cycle. [TradingData.TradingDataLoop]", 2);

                if (!isStopped)
                {
                    Thread.Sleep(RecycleInterval());
                }
            }
        }

        private void PirumFileUpload()
        {
            string dayTime = KeyValue.Get("TradingDataPirumWaitTime", "19:00", dbCnStr);

            if (Master.BizDate.Equals(DateTime.Now.ToString("yyyy-MM-dd")) && (dayTime.CompareTo(DateTime.Now.ToString("HH:mm")) < 0))
            {
                if (!KeyValue.Get("TradingDataPirumOpenTradesDate", "", dbCnStr).Equals(Master.BizDate))
                {
                    DataSet dsContracts = new DataSet();
                    DataSet dsReturns = new DataSet();

                    string fileName = "";
                    string tempDirectory = @"C:\Temp\";


                    dsContracts = TgDatabaseFunctions.FunctionsGet.ContractsGet(Master.BizDate, "PFSI", dbCnStr);
                    dsReturns = TgDatabaseFunctions.FunctionsGet.ReturnsGet(Master.BizDate, "PFSI", dbCnStr);

                    PirumContractCompare pCC = new PirumContractCompare(Master.BizDate, dsContracts, dsReturns);

                    fileName = CreateFile(tempDirectory, pCC.Parse(), true);

                    GnuPG gpg = new GnuPG(Standard.ConfigValue("HomePath"));
                    gpg.Timeout = 56000000;
                    gpg.UserId = Standard.ConfigValue("UserId");
                    gpg.Recipient = "stockloan@pirum.com";
                    gpg.Verbosity = VerbosityLevels.None;
                    gpg.Command = Commands.EncryptSign;
                    gpg.DoCommand(tempDirectory + fileName);

                    fileName = fileName + ".gpg";

                    FileTransfer fileTransfer = new FileTransfer(Standard.ConfigValue("Database"));
                    fileTransfer.FilePut(@"ftp://xfr.pirum.com/" + fileName, "pensonuk", "clw11ths", tempDirectory + fileName);
                    //fileTransfer.FilePut(@"ftp://files.penson.com/upload/" + fileName, "gsco", "mAr1s-gsCo", tempDirectory + fileName);

                    Email.Send(KeyValue.Get("TradingDataPirumOpenTradeTo", "mbattaini@penson.com", dbCnStr),
                        KeyValue.Get("TradingDataPirumOpenTradeFrom", "sendero@penson.com", dbCnStr),
                        "Pirum File uploaded for " + Master.BizDate,
                        "",
                        dbCnStr);

                    KeyValue.Set("TradingDataPirumOpenTradesDate", Master.BizDate, dbCnStr);
                }
            }
            else
            {
                Log.Write("Must wait until " + dayTime + " to produce Pirum Open Trades File. [TradingData.PirumFileUpload]", 1);
            }
        }

        private string CreateFile(string tempDirectory, string body, bool encrypt)
        {
            string fileName = "live_penson_uk_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".dat";

            TextWriter textWriter = new StreamWriter(tempDirectory + fileName);

            textWriter.Write(body);
            textWriter.Close();

            return fileName;
        }


        private void GatewayResponsesGet()
        {
            string message = "";
            string messageId = "";

            try
            {
                MqSeries.MqActivity mqActivity = new MqSeries.MqActivity(dbCnStr);

                while (true)
                {
                    message = mqActivity.PullMessage();

                    messageId = TGTradeFormats.TradeFunctions.StarsTradeReferenceGet(message);

                    TgDatabaseFunctions.FunctionsSet.TradeMessageSet(Master.BizDate, messageId, "", message, dbCnStr);

                    Log.Write("Processed- " + messageId + " - " + message, 1);
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
            }
        }

        private void ReportGet()
        {
            string body = "";
            string dayTime = KeyValue.Get("TradingDataReportDateTime", "19:00", dbCnStr);

            if (Master.BizDate.Equals(DateTime.Now.ToString("yyyy-MM-dd")) && (dayTime.CompareTo(DateTime.Now.ToString("HH:mm")) < 0))
            {
                if (!KeyValue.Get("TradingDataReportDate", "", dbCnStr).Equals(Master.BizDate))
                {
                    body = TGTradeFormats.TradeDataReport.FormatGet(TgDatabaseFunctions.FunctionsGet.TradeMessagesGet(Master.BizDate, dbCnStr));

                    Log.Write("Created- " + body, 1);
                    Email.Send(KeyValue.Get("TradingDataReportTo", "mbattaini@penson.com", dbCnStr), "sendero@penson.com", "Trade Report - " + Master.BizDate, body, dbCnStr);

                    KeyValue.Set("TradingDataReportDate", Master.BizDate, dbCnStr);
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
                recycleInterval = KeyValue.Get("TradingDataLoopRecycleIntervalBizDay", "0:15", dbCn);
            }
            else
            {
                recycleInterval = KeyValue.Get("TradingDataLoopRecycleIntervalNonBizDay", "4:00", dbCn);
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
                    KeyValue.Set("TradingDataRecycleIntervalBizDay", "0:15", dbCn);
                    hours = 0;
                    minutes = 30;
                    timeSpan = new TimeSpan(hours, minutes, 0);
                    Log.Write("TradingDataRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [TradingData.RecycleInterval]", Log.Error, 1);
                }
                else
                {
                    KeyValue.Set("TradingDataRecycleIntervalNonBizDay", "4:00", dbCn);
                    hours = 6;
                    minutes = 0;
                    timeSpan = new TimeSpan(hours, minutes, 0);
                    Log.Write("TradingDataRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [TradingData.RecycleInterval]", Log.Error, 1);
                }
            }

            Log.Write("TradingData will recycle in " + hours + " hours, " + minutes + " minutes. [TradingData.RecycleInterval]", 2);
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

