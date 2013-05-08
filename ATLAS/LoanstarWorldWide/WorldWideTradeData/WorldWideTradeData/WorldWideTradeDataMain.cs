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
using StockLoan.MqSeries;

namespace StockLoan.WorldWideTradeDataService
{   
    public class WorldWideTradeDataMain                
	{
		private string dbCnStr;
        private DataSet dsBookGroupTrade;
		private Thread workerThread = null;
        private Thread mqThread = null;
		private static bool isStopped = true;
        private static string tempPath;
        
        private MqActivity mqActivity;

        private SqlConnection dbCn;

		public WorldWideTradeDataMain(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;			
						
			try
			{
				dbCn = new SqlConnection(dbCnStr);    				
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [WorldWideTradeDataMain.WorldWideTradeDataMain]", Log.Error, 1);
			}

			if (Standard.ConfigValueExists("TempPath"))
			{
				tempPath = Standard.ConfigValue("TempPath");

				if (!Directory.Exists(tempPath))
				{
                    Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [WorldWideTradeDataMain.WorldWideTradeDataMain]", Log.Error, 1);
					tempPath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
                Log.Write("A configuration value for TempPath has not been provided. [WorldWideTradeDataMain.WorldWideTradeDataMain]", Log.Information, 1);
				tempPath = Directory.GetCurrentDirectory();
			}

            Log.Write("Temporary files will be staged at " + tempPath + ". [WorldWideTradeDataMain.WorldWideTradeDataMain]", 2);
		}

        ~WorldWideTradeDataMain()
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
				workerThread = new Thread(new ThreadStart(WorldWideTradeDataMainLoop));
				workerThread.Name = "Worker";
				workerThread.Start();

                Log.Write("Start command issued with new worker thread. [WorldWideTradeDataMain.Start]", 2);
            }
            else // Old thread will be just fine.
            {
                Log.Write("Start command issued with worker thread already running. [WorldWideTradeDataMain.Start]", 2);
            }

            if ((mqThread == null) || (!mqThread.IsAlive)) // Must create new thread.
			{
                mqThread = new Thread(new ThreadStart(MqActivityLoop));
                mqThread.Name = "mqThread";
                mqThread.Start();

                Log.Write("Start command issued with new mq thread. [WorldWideTradeDataMain.Start]", 2);            
            }
			else // Old thread will be just fine.
			{
                Log.Write("Start command issued with mq thread already running. [WorldWideTradeDataMain.Start]", 2);
			}
		}

		public void Stop()
		{
			isStopped = true;

			if (workerThread == null)
			{
				Log.Write("Stop command issued, worker thread never started. [WorldWideTradeDataMain.Stop]", 2);
			}
			else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				workerThread.Abort();
				Log.Write("Stop command issued, sleeping worker thread aborted. [WorldWideTradeDataMain.Stop]", 2);
			}
			else
			{
				Log.Write("Stop command issued, worker thread is still active. [WorldWideTradeDataMain.Stop]", 2);
			}


            if (mqThread == null)
            {
                Log.Write("Stop command issued, mq thread never started. [WorldWideTradeDataMain.Stop]", 2);
            }
            else if (mqThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
            {
                mqThread.Abort();
                Log.Write("Stop command issued, sleeping mq thread aborted. [WorldWideTradeDataMain.Stop]", 2);
            }
            else
            {
                Log.Write("Stop command issued, mq thread is still active. [WorldWideTradeDataMain.Stop]", 2);
            }
		}

        private void WorldWideTradeDataMainLoop()
        {
            DataSet dsBookGroupCheck = new DataSet();

            while (!isStopped)
            {
                Log.Write("Start-of-cycle. [WorldWideTradeDataMain.WorldWideTradeDataMainLoop]", 2);

                dsBookGroupTrade = DBTradeData.TradeDataSettingsGet("");

                Log.Write("Loaded : " + dsBookGroupTrade.Tables["BookGroups"].Rows.Count.ToString("#,##0") + " bookgroups.", 1);

                foreach (DataRow drRow in dsBookGroupTrade.Tables["BookGroups"].Rows)
                {
                    Log.Write("Procesing trading information for : " + drRow["BookGroup"].ToString() + ".", 1);

                    Master.BookGroup = drRow["BookGroup"].ToString();
                    Master.Fund = drRow["DefaultFund"].ToString();
                    Master.System = drRow["SettlementSystem"].ToString();

                    Master.ContractsBizDate = "";
                    Master.BizDate = "";
                    Master.BizDateBank = "";
                    Master.BizDateExchange = "";
                    Master.BizDatePrior = "";
                    Master.BizDatePriorBank = "";
                    Master.BizDatePriorExchange = "";
                    Master.BizDateNext = "";
                    Master.BizDateNextBank = "";
                    Master.BizDateNextExchange = "";

                    LoadBookGroupBizDates();
                    if (isStopped) break;

                    if (!Master.ContractsBizDate.Equals(Master.BizDate))
                    {

                        LoadTradesData(drRow["FileHost"].ToString() + drRow["FileNameTrades"].ToString(),
                                      drRow["UserId"].ToString(),
                                      drRow["Password"].ToString(),
                                      Tools.FormatDate(drRow["FileNameTradesDate"].ToString(), Standard.DateFormat));
                        if (isStopped) break;

                        LoadRecallsData(drRow["FileHost"].ToString() + drRow["FileNameRecalls"].ToString(),
                                       drRow["UserId"].ToString(),
                                       drRow["Password"].ToString(),
                                       Tools.FormatDate(drRow["FileNameRecallsDate"].ToString(), Standard.DateFormat));
                        if (isStopped) break;

                        LoadMarksData(drRow["FileHost"].ToString() + drRow["FileNameMarks"].ToString(),
                                      drRow["UserId"].ToString(),
                                      drRow["Password"].ToString(),
                                      Tools.FormatDate(drRow["FileNameMarksDate"].ToString(), Standard.DateFormat));
                        if (isStopped) break;


                        LoadClientsData(drRow["FileHost"].ToString() + drRow["FileNameClients"].ToString(),
                                      drRow["UserId"].ToString(),
                                      drRow["Password"].ToString(),
                                      Tools.FormatDate(drRow["FileNameClientsDate"].ToString(), Standard.DateFormat));
                        if (isStopped) break;
                        
                        dsBookGroupCheck = DBTradeData.TradeDataSettingsGet(Master.BookGroup);

                        if ((dsBookGroupCheck.Tables["BookGroups"].Rows.Count == 1))
                        {
                            if (!Master.ContractsBizDate.Equals(Master.BizDate))
                            {
                                DateTime tradeDataContracts;
                                string tradeDataFileDate;

                                DateTime tradeDataMarks;
                                string tradeDataMarkFileDate;


                                if (DateTime.TryParse(dsBookGroupCheck.Tables["BookGroups"].Rows[0]["FileNameTradesDate"].ToString(), out tradeDataContracts))
                                {
                                    tradeDataFileDate = tradeDataContracts.ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    tradeDataFileDate = "";
                                }

                                if (DateTime.TryParse(dsBookGroupCheck.Tables["BookGroups"].Rows[0]["FileNameMarksDate"].ToString(), out tradeDataMarks))
                                {
                                    tradeDataMarkFileDate = tradeDataMarks.ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    tradeDataMarkFileDate = "";
                                }


                                if (tradeDataFileDate.Equals(Master.BizDatePrior) && tradeDataMarkFileDate.Equals(Master.BizDate))
                                {
                                    DBTradeData.TradeDataContractsBizDateRoll(Master.BizDatePrior, Master.BizDate, Master.BookGroup);
                                    DBStandardFunctions.BookGroupSet(Master.BookGroup, "", "", "", Master.BizDate, "", "", "", "", "", "", "", "", null, "");
                                }
                            }
                        }
                        else
                        {
                            Log.Write("Multiple BookGroups returned for singular check.", 1);
                        }

                    }
                    else
                    {
                        Log.Write("Contracts data already rolled for : " + Master.ContractsBizDate + " ; BookGrouup : " + Master.BookGroup + ".", 1);
                    }
                }
                Log.Write("End-of-cycle. [WorldWideTradeDataMain.WorldWideTradeDataMainLoop]", 2);

                if (!isStopped)
                {
                    Thread.Sleep(RecycleInterval());
                }
            }
        }

        private void LoadBookGroupBizDates()
        {
            try
            {
                Master.ContractsBizDate = DBStandardFunctions.BizDateStrGet(Master.BookGroup, "BIZDATECONTRACT");

                Master.BizDate = DBStandardFunctions.BizDateStrGet(Master.BookGroup, "BIZDATE");
                Master.BizDateBank = DBStandardFunctions.BizDateStrGet(Master.BookGroup, "BIZDATEBANK");
                Master.BizDateExchange = DBStandardFunctions.BizDateStrGet(Master.BookGroup, "BIZDATEEXCHANGE");

                Master.BizDatePrior = DBStandardFunctions.BizDateStrGet(Master.BookGroup, "BIZDATEPRIOR");
                Master.BizDatePriorBank = DBStandardFunctions.BizDateStrGet(Master.BookGroup, "BIZDATEPRIORBANK");
                Master.BizDatePriorExchange = DBStandardFunctions.BizDateStrGet(Master.BookGroup, "BIZDATEPRIOREXCHANGE");

                Master.BizDateNext = DBStandardFunctions.BizDateStrGet(Master.BookGroup, "BIZDATENEXT");
                Master.BizDateNextBank = DBStandardFunctions.BizDateStrGet(Master.BookGroup, "BIZDATENEXTBANK");
                Master.BizDateNextExchange = DBStandardFunctions.BizDateStrGet(Master.BookGroup, "BIZDATENEXTEXCHANGE");
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
            }
        }

        private void LoadTradesData(string fileName, string userId, string password, string lastDateTime)
        {
            Contracts contracts = new Contracts();

            contracts.Load(fileName, userId, password);

            if (contracts.BizDate.Equals(lastDateTime))
            {
                Log.Write("Trade Data already loaded for : " + contracts.BizDate + ".", 1);
            }
            else
            {
                Log.Write("Trade Data is for Contracts BizDate : " + lastDateTime +"; File Date : " + contracts.BizDate + "; Will Load Trade Data.", 1);
                
                Log.Write("Loaded: " + contracts.Count.ToString("#,##0") + " Contracts.", 1);          
            
                UpdateTradeData(contracts);
                                              
                DBTradeData.TradeDataSettingSet(Master.BookGroup,  contracts.BizDate, "", "", "");
            }
        }

        private void UpdateTradeData(Contracts contracts)
        {
            DBTradeData.TradeDataContractsPurge(contracts.BizDate, contracts.ClientId);
            Log.Write("Purged trade data for :  " + contracts.BizDate + " for : " + contracts.ClientId, 1);

            for (int index = 0; index < contracts.Count; index++)
            {
                ContractItem contractItem = contracts.Contract(index);

                DBContracts.ContractSet(
                    contracts.BizDate,
                    contracts.ClientId,
                    contractItem.ContractId,
                    contractItem.ContractType,
                    contractItem.ContraClientId,
                    contractItem.SecId,
                    contractItem.Quantity.ToString(),
                    contractItem.Quantity.ToString(),
                    contractItem.Amount.ToString(),
                    contractItem.Amount.ToString(),
                    contractItem.CollateralCode,
                    contractItem.ValueDate,
                    contractItem.SettleDate,
                    contractItem.TermDate,
                    contractItem.Rate.ToString(),
                    contractItem.RateCode,
                    "",
                    contractItem.PoolCode,
                    contractItem.DivRate.ToString(),
                    contractItem.DivCallable,
                    contractItem.IncomeTracked,
                    contractItem.MarginCode,
                    contractItem.Margin.ToString(),
                    contractItem.CurrencyIso,
                    contractItem.SecurityDepot,
                    contractItem.CashDepot,
                    contractItem.OtherClientId,
                    contractItem.Comment,
                    Master.Fund,
                    "",
                    "",
                    "",
                    "",
                    false,
                    false,
                    true);
            }
        }

        private void LoadRecallsData(string fileName, string userId, string password, string lastDateTime)
        {
            Recalls recalls = new Recalls();

            recalls.Load(fileName, userId, password);

            if (recalls.BizDate.Equals(lastDateTime))
            {
                Log.Write("Trade Data already loaded for : " + recalls.BizDate + ".", 1);
            }
            else
            {

                DBTradeData.TradeDataMarksPurge(recalls.BizDate, recalls.ClientId);
                Log.Write("Purging Recalls For: " + recalls.BizDate + "; Book Group: " + recalls.ClientId + ".", 1);                
                
                Log.Write("Trade Data is for Contracts BizDate : " + lastDateTime + "; File Date : " + recalls.BizDate + "; Will Load Recalls.", 1);
                UpdateRecallsData(recalls);

                DBTradeData.TradeDataSettingSet(Master.BookGroup, "", "", recalls.BizDate, "");
            }

            Log.Write("Loaded: " + recalls.Count.ToString("#,##0") + " Recalls.", 1);
        }

        private void UpdateRecallsData(Recalls recalls)
        {

            int errors = 0;

            for (int index = 0; index < recalls.Count; index++)
            {
                try
                {
                    RecallItem recallItem = recalls.RecallItem(index);

                    DBTradeData.TradeDataRecallItemSet(
                        recalls.BizDate,
                        recalls.ClientId,
                        recallItem.ContractType,
                        recallItem.ContractId,
                        recallItem.ContraClientId,
                        recallItem.SecId,
                        recallItem.Quantity.ToString(),
                        recallItem.RecallDate,
                        recallItem.BuyInDate,
                        recallItem.Status,
                        recallItem.ReasonCode,
                        recallItem.RecallId,
                        recallItem.SequenceNumber.ToString(),
                        recallItem.Comment);
                        
                }
                catch (Exception error)
                {
                    Log.Write(error.Message, 1);
                    errors++;
                }
            }

            Log.Write("Wrote : " + (recalls.Count - errors).ToString("#,##0") + " recalls, with : " + errors.ToString("#,##0") + " errors.", 1);
        }        

        private void LoadMarksData(string fileName, string userId, string password, string lastDateTime)
        {
            Marks marks = new Marks();

            marks.Load(fileName, userId, password);

            if (marks.BizDate.Equals(lastDateTime))
            {
                Log.Write("Trade Data already loaded for : " + marks.BizDate + ".", 1);
            }
            else
            {
                Log.Write("Trade Data is for Marks BizDate : " + lastDateTime + "; File Date : " + marks.BizDate + "; Will Load Marks.", 1);

                DBTradeData.TradeDataMarksPurge(marks.BizDate, marks.ClientId);
                Log.Write("Purging Marks For: " + marks.BizDate + "; Book Group: " + marks.ClientId + ".", 1);

                UpdateMarksData(marks);

                DBTradeData.TradeDataSettingSet(Master.BookGroup, "", marks.BizDate, "", "");
            }

            Log.Write("Loaded: " + marks.Count.ToString("#,##0") + " Marks.", 1);
        }

        private void UpdateMarksData(Marks marks)
        {

             int errors = 0;

             for (int index = 0; index < marks.Count; index++)
             {
                 try
                 {
                     MarkItem markItem = marks.MarkItem(index);

                     DBTradeData.TradeDataMarkItemSet(
                         marks.BizDate,
                         marks.ClientId,
                         markItem.ContractId,
                         markItem.ContractType,
                         (markItem.Direction.Equals("D") ? (markItem.Amount * -1).ToString("#,##0.00") : markItem.Amount.ToString("#,##0.00")));
                 }
                 catch (Exception error)
                 {
                     Log.Write(error.Message, 1);
                     errors++;
                 }
             }

            Log.Write("Wrote : " + (marks.Count - errors).ToString("#,##0") + " marks, with : " + errors.ToString("#,##0") + " errors.", 1);
        }        

        private void LoadClientsData(string fileName, string userId, string password, string lastDateTime)
        {
            Clients clients = new Clients();

            clients.Load(fileName, userId, password);

            if (clients.BizDate.Equals(lastDateTime))
            {
                Log.Write("Clients already loaded for : " + clients.BizDate + ".", 1);
            }
            else
            {
                Log.Write("Clients Data is for Contracts BizDate : " + lastDateTime + "; File Date : " + clients.BizDate + "; Will Load Clients.", 1);

                UpdateClientsData(clients);

                DBTradeData.TradeDataSettingSet(Master.BookGroup, "", "", "", clients.BizDate);               
            }

            Log.Write("Loaded: " + clients.Count.ToString("#,##0") + " Clients.", 1);
        }

        private void UpdateClientsData(Clients clients)
        {
            int errors = 0;

            for (int index = 0; index < clients.Count; index++)
            {
                try
                {
                    ClientItem clientItem = clients.ClientItem(index);

                    DBBooks.BookSet(
                        clients.ClientId,
                        (clientItem.ParentAccount.Trim().Equals("") ? clientItem.ContraClientId : clientItem.ParentAccount),
                        clientItem.ContraClientId,
                        clientItem.AccountName,
                        clientItem.AddressLine1,
                        clientItem.AddressLine2,
                        clientItem.AddressLine3,
                        clientItem.Phone,
                        "",
                        clientItem.BorrowCollateral,
                        clientItem.LoanCollateral,
                        clientItem.MarkValueHouse,
                        clientItem.MarkValueInstitution,
                        clientItem.StockBorrowRate,
                        clientItem.StockLoanRate,
                        clientItem.BondBorrowRate,
                        clientItem.BondLoanRate,
                        "**",
                        Master.Fund,
                        clientItem.MinMarkPrice,
                        clientItem.MinMarkAmount,
                        Master.System,
                        true);

                    DBBooks.BookCreditLimitSet(
                        clients.BizDate,
                        clients.ClientId,
                         (clientItem.ParentAccount.Trim().Equals("") ? clientItem.ContraClientId : clientItem.ParentAccount),
                        clientItem.ContraClientId,
                        clientItem.BorrowLimit,
                        clientItem.LoanLimit,
                        Master.System);

                }
                catch (Exception error)
                {
                    Log.Write(error.Message, 1);
                    errors++;
                }
            }

            Log.Write("Wrote : " + (clients.Count - errors).ToString("#,##0") + " clients, with : " + errors.ToString("#,##0") + " errors.", 1);
        }

        private void UpdateLoanetTransactions()
        {
            DataSet dsMessages = DBTradeData.LoanetActivityMessagesGet(Master.BizDate);


            foreach (DataRow drTrans in dsMessages.Tables["Messages"].Rows)
            {

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
                Log.Write( e.Message + ".  [WorldWideTradeDataMain.UtcOffsetTimeSpan]", Log.Error, 1);
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
					KeyValue.Set("WorldWideTradeDataMainRecycleIntervalBizDay", "0:15", dbCn);
					hours = 0;
					minutes = 15;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [WorldWideTradeDataMain.RecycleInterval]", Log.Error, 1);
				}
				else
				{
					KeyValue.Set("WorldWideTradeDataMainRecycleIntervalNonBizDay", "4:00", dbCn);
					hours = 4;
					minutes = 0;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [WorldWideTradeDataMain.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("WorldWideTradeDataMain will recycle in " + hours + " hours, " + minutes + " minutes. [WorldWideTradeDataMain.RecycleInterval]", 2);

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

        private void MqActivityLoop()
        {
            mqActivity = new MqActivity("Activity", dbCnStr);
            
            while (!isStopped)
            {
                try
                {
                    MqActivityGet();
                    if (isStopped) break;
                }
                catch (Exception error)
                {
                    Log.Write(error.Message, 1);                    
                    Thread.Sleep(int.Parse(KeyValue.Get("ServiceMqSleep", "2000", dbCnStr)));
                }
            }

            mqActivity.MqActivityClose();
        }

        private void MqActivityGet()
        {
            MqMessage mqMessage = new MqMessage();

            if (mqActivity != null)
            {
                while(true)
                {
                    mqMessage = mqActivity.PullMessage();

                    Log.Write(mqMessage.message, 1);

                    MqActivityMessageSet(
                        mqMessage.bizDate,
                        mqMessage.message);
                }
            }
        }

        private void MqActivityMessageSet(string bizDate, string message)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spLoanetActivityMessagesInsert", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramMessage = dbCmd.Parameters.Add("@Message", SqlDbType.VarChar, 2000);
                paramMessage.Value = message;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
            }
        }
	}
}
