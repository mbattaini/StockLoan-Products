using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Anetics.Common;
using C1.C1Excel;
using System.Text;
using System.Threading.Tasks;


namespace Anetics.Ares
{
	public class AresMain
	{
		private string notificationList;
		private string emailForm;
		private string dbCnStr;
		private string worldwideDbCnStr;			

		private SqlConnection dbCn = null;

		private string previousEmail = "";

		private Thread workerThread = null;
		private static bool isStopped = true;
		private static string tempPath;

		private Email email;

		
    
		public AresMain(string dbCnStr, string worldwideDbCnStr)
		{
			this.dbCnStr = dbCnStr;
			this.worldwideDbCnStr = worldwideDbCnStr;
						
			try
			{
				dbCn = new SqlConnection(dbCnStr);
      
				email = new Email(dbCnStr);							
							
				emailForm =  KeyValue.Get("AresEmailFrom", "stockloan@penson.com", dbCnStr);
				notificationList = KeyValue.Get("AresEmailTo", "", dbCnStr);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [AresMain.AresMain]", Log.Error, 1);
			}

			if (Standard.ConfigValueExists("TempPath"))
			{
				tempPath = Standard.ConfigValue("TempPath");

				if (!Directory.Exists(tempPath))
				{
					Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [AresMain.AresMain]", Log.Error, 1);
					tempPath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
				Log.Write("A configuration value for TempPath has not been provided. [AresMain.AresMain]", Log.Information, 1);
				tempPath = Directory.GetCurrentDirectory();
			}

			Log.Write("Temporary files will be staged at " + tempPath + ". [AresMain.AresMain]", 2);
		}

		~AresMain()
		{
			if (dbCn != null)
			{
				dbCn.Dispose();
			}
		}

		public void Start()
		{
			isStopped = false;

			if ((workerThread == null)||(!workerThread.IsAlive)) 
			{
				workerThread = new Thread(new ThreadStart(AresMainLoop));
				workerThread.Name = "Worker";
				workerThread.Start();

				Log.Write("Start command issued with new worker thread. [AresMain.Start]", 2);
			}
			else 
			{
				Log.Write("Start command issued with worker thread already running. [AresMain.Start]", 2);
			}
		}

		public void Stop()
		{
			isStopped = true;

			if (workerThread == null)
			{
				Log.Write("Stop command issued, worker thread never started. [AresMain.Stop]", 2);
			}
			else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				workerThread.Abort();
				Log.Write("Stop command issued, sleeping worker thread aborted. [AresMain.Stop]", 2);
			}
			else
			{
				Log.Write("Stop command issued, worker thread is still active. [AresMain.Stop]", 2);
			}
		}
        
		private void AresMainLoop()
		{
			while (!isStopped) 
			{
				Log.Write("Start-of-cycle. [AresMain.AresMainLoop]", 2);
				KeyValue.Set("AresMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        
				Master.BizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);  
				Master.BizDatePrior = KeyValue.Get("BizDatePrior", "0001-01-01", dbCn);
				Master.BizDateExchange = KeyValue.Get("BizDateExchange", "0001-01-01", dbCn);
				Master.BizDatePriorExchange = KeyValue.Get("BizDatePriorExchange", "0001-01-01", dbCn);
				Master.ContractsBizDate =	KeyValue.Get("ContractsBizDate", "0001-01-01", dbCn);
				Master.BizDateNext = KeyValue.Get("BizDateNext", "0001-01-01", dbCn);


                if (KeyValue.Get("BroadRidgeBoxPositionLoadDate", "", dbCnStr).Equals(Master.BizDate) && 
                    KeyValue.Get("BroadRidgeEasyBorrowFileDate", "", dbCnStr).Equals(Master.BizDate) )
                {
                    PenaltyBoxExceptionsMail("0158");

                    KeyValue.Set("S3StartOfDayDate", Master.BizDate, dbCnStr);  //Retired S3 Service

                    RateDifferencesEmail();
                    FailsEmail();
                    FMRAvailabilityFile();

                    try
                    {
                        StockBorrowUploadFile();
                    }
                    catch (Exception error)
                    {
                        Log.Write(error.Message, 1);
                    }                               
                }
                
                
                EasyBorrowRatesMail("ETB_Rates", "EasyBorrowRates_", "Easy Borrow Rates");
                EasyBorrowRatesMail("ETB_Symbols", "ETB_", "Easy Borrow List"); 
                EasyBorrowRatesFile();

                AztecInventoryLoad();

                PremiumAutoSet();
                                
                HTBAccountsBPSMail();                
                ShortsVsFtdMail();

                ThresholdRatesMail();
                HardRatesMail();
                RecordDateContractsMail();
                
                HardToBorrowChargesCSV("SWIM", true, "");
                HardToBorrowChargesCSV("TORC", false, @"\\Rsdmz001\reports\Firm10\TORP\{yyyyMMdd}");

                SchonfeldShortSaleNegativeRebateBillingFileDo();
                HardToBorrowRatesFile();
                HardToBorrowProfitabilityMail();

                AvailabilityFeed();
                HardToBorrowNotBilledMail();                

                FINRABalancesMail();                
                FINRABorrowsMail("0158");

                StockBorrowFileDownload(DateTime.Parse(Master.BizDate).ToString("MM-d-yyyy") + "-1.log","1");
                StockBorrowFileDownload(DateTime.Parse(Master.BizDate).ToString("MM-d-yyyy") + "-2.log","2");
                StockBorrowFileDownload(DateTime.Parse(Master.BizDate).ToString("MM-d-yyyy") + "-3.log","3");
                StockBorrowFileDownload(DateTime.Parse(Master.BizDate).ToString("MM-d-yyyy") + "-4.log","4");

                KeyValue.Set("AresMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
				Log.Write("End-of-cycle. [AresMain.AresMainLoop]", 2);

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

            if (!KeyValue.Get("AztecFileLoadDate", "", dbCnStr).Equals(Master.ContractsBizDate))
            {
                try
                {
                    AztecInventoryFile aztecFile = new AztecInventoryFile(dbCnStr);
                    aztecFile.Load();

                    KeyValue.Set("AztecFileLoadDate", Master.ContractsBizDate, dbCnStr);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [AresMain.AztecInventoryLoad]", 1);
                }
            }
        }

        public void StockBorrowFileDownload(string fileName, string runNum)
        {
            if (!(KeyValue.Get("StockBorrowRun[" + runNum+"]Date", "", dbCn).Equals(Master.BizDate)))
            {

                try
                {
                    Log.Write("Stock Borrow Feed - downloading file " + fileName, 1);

                    Filer filer = new Filer();
                    filer.FileGet(fileName, Standard.ConfigValue("StockBorrowFtpServer"), Standard.ConfigValue("StockBorrowFtpUser"), Standard.ConfigValue("StockBorrowFtpPassword"), TempPath + fileName);

                    Log.Write("Stock Borrow Feed - downloaded file " + fileName, 1);
                }
                catch (Exception error)
                {
                    Log.Write("Stock Borrow Feed - Error -  ftp process failed! - " + error.Message, 1);
                    return;
                }

                try
                {
                    StockBorrowParser stkParser = new StockBorrowParser(TempPath + fileName);
                    DataSet dsInventory = stkParser.Parse();

                    Log.Write("Stock Borrow Feed - parsed file " + fileName, 1);

                    var x = (from r in dsInventory.Tables["Inventory"].AsEnumerable()
                             select r["Source"]).Distinct().ToList();

                    string body = "";

                    foreach (string line in x)
                    {
                        body = "";

                        foreach (DataRow dr in dsInventory.Tables["Inventory"].Rows)
                        {
                            if (line.Equals(dr["Source"].ToString()))
                            {
                                body += dr["Cusip"].ToString() + "|" + dr["Quantity"].ToString() + "\r\n";
                            }                           
                        }

                        string fileNameTemp = Standard.ConfigValue("StockBorrowFileDestination") + line.Substring(0, 5).Trim() + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".dat";
                        File.WriteAllText(fileNameTemp, body);
                        Log.Write("Stock Borrow Feed - Created file " + fileNameTemp, 1);
                    }

                    KeyValue.Set("StockBorrowRun[" + runNum + "]Date", Master.BizDate, dbCn);
                }
                catch (Exception error)
                {
                    Log.Write("Stock Borrow Feed - Error -  Parse process failed! - " + error.Message, 1);
                }
            }
            else
            {
                Log.Write("Stock Borrow Feed - Information -  File Already processed - " + fileName, 1);
            }
        }

        public void BankLoanDtcReset(string bookGroup)
        {
            // 2012-03-06 Automatically set the KeyIds [BankLoanBizDate0234] and [BankLoanDtcReset0234]
            // 2012-05-03 BD Data Service has been retired, it no longer loads APIBALBPS BankLoan(0158) file. 
            // 2012-05-07 Retired this function which sets (0234) APIBAL KeyIds:[BankLoanBizDate0234],[BankLoanDtcReset0234] for BD Data Service. 

            try
            {
                if (KeyValue.Get("BankLoanBizDate" + bookGroup, "2000-01-01", dbCnStr).Equals(Master.BizDatePrior))  
                {
                    Log.Write("Bank loan DTC reset for " + bookGroup + " is current for " + Master.BizDatePrior + ". [AresMain.BankLoanDtcReset]", 2);
                }
                else
                {
                    KeyValue.Set("BankLoanBizDate" + bookGroup, Master.BizDatePrior, dbCnStr);
                    KeyValue.Set("BankLoanDtcReset" + bookGroup, true.ToString(), dbCnStr);

                    Log.Write("Retired BankLoanDTCReset" + bookGroup + " (APIBAL.TXT): Key[BankLoanBizDate" + bookGroup + "] updated to " + Master.BizDatePrior + " (BizDatePrior).   [AresMain.BankLoanDtcReset]", 2);
                    Log.Write("Retired BankLoanDTCReset" + bookGroup + " (APIBAL.TXT): Key[BankLoanDtcReset" + bookGroup + "] updated to True.   [AresMain.BankLoanDtcReset]", 2);
                    Email mail = new Email(dbCnStr);
                    mail.Send("dchen@penson.com;support.stockloan@penson.com;",
                              "sendero@penson.com", "BankLoanDtcReset" + bookGroup + " done, for " + Master.BizDatePrior,
                              "Retired BankLoanDTCReset" + bookGroup + " (APIBAL.TXT), \r\n" +
                              "KeyId [BankLoanBizDate" + bookGroup + "] set to " + Master.BizDatePrior + ", \r\n" +
                              "KeyId [BankLoanDtcReset" + bookGroup + "] set to True." );
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + "  [AresMain.BankLoanDtcReset]", Log.Error, 1);
            }
        }

        public void ClearingFailToNoLendList(string bookGroup)
        {
            DataSet dsList = new DataSet();
            DataSet dsReportRecipients = new DataSet();

            SqlConnection dbCn = new SqlConnection(dbCnStr);

            if (!KeyValue.Get("Ares_" +bookGroup +"_ClearingFailToNoLendListDate", "", dbCnStr).Equals(Master.BizDate))
            {
                try
                {
                    SqlCommand dbCmd = new SqlCommand("spClearingRule204ListGet", dbCn);    // Retired
                    dbCmd.CommandType = CommandType.StoredProcedure;
                    dbCmd.CommandTimeout = 900;

                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = Master.BizDate;

                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                    dataAdapter.Fill(dsList, "ClearingList");
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [AresMain.ClearingFailToNoLendList]", 1);
                    throw;
                }

                string fileContent = "";

                foreach (DataRow dr in dsList.Tables["ClearingList"].Rows)
                {
                    fileContent += dr["SecId"].ToString() + "\t" + dr["Symbol"].ToString() + "\r\n";
                }

                dsReportRecipients = ReportRecipientsGet("Clearing_List" + bookGroup);

                Email mail = new Email(dbCnStr);                

                foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
                {
                    try
                    {
                        if (dr["LastDeliveredDate"].ToString().Equals(""))
                        {
                            dr["LastDeliveredDate"] = Master.BizDatePrior;
                        }

                        if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
                        {

                            if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
                            {
                                mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "CNS Items Added To No Lend List for " + bookGroup +", Date: " + Master.ContractsBizDate, fileContent, "");
                            }

                            ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.ContractsBizDate);
                        }
                    }
                    catch (Exception error)
                    {
                        Log.Write("ERROR (foreach loop): " + error.Message.Replace("\r\n", "") + ".  [AresMain.EasyBorrowDayTradingListMail]", Log.Error, 1);
                    }
                }

                KeyValue.Set("Ares_" + bookGroup +"_ClearingFailToNoLendListDate", Master.BizDate, dbCnStr);
            }
            else
            {
                Log.Write("Clearing Fail To No Lend List has already been sent for the day. [AresMain.ClearingFailToNoLendList]", 1);
            }
        }

        public void MemoSegExDeficitsMail(string bookGroup)
        {
            //2012-03-06: MemeSeg Excess Deficit files: DEFEXP3  DEFEXBPS  has been RETIRED by BroadRidge
            //            MemoSegData Service automatically set the keys: [MemoSegEdDeficit0158Date] and [MemoSegEdDeficit0234Date] to <BizDate> 
            //2012-05-07: MemoSeg Excess Deficit data now loads into [tbBoxExpend], table:[tbMemoSegExDeficits] table has been retired.

            string sql = "";
			string filePath = "";
			int rowCount = 0;

            SqlConnection localDbCn = new SqlConnection(dbCnStr);
			DataSet dsMemoSegExDeficits = new DataSet();		

            try
            {
                if (Master.BizDate.Equals(Master.ContractsBizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
                {
                    if (KeyValue.Get("AresMemoSegExDeficitsMailDate(" + bookGroup + ")", "", dbCnStr).Equals(Master.ContractsBizDate))
                    {
                        Log.Write("Memo Seg ExDeficits (" + bookGroup + ") Email has already occured for " + Master.ContractsBizDate + ".  [AresMain.MemoSegExDeficitsMail]", Log.Information, 2);
                    }
                    else
                    {
                        if (KeyValue.Get("MemoSegEdDeficit"+bookGroup+"Date", "", dbCnStr).Equals(Master.ContractsBizDate))
                        {
                            sql = "Create Table dbo.#ExDeficits ( \r\n" + 
                                "    [System] varchar(5), \r\n" + 
                                "    SecId varchar(12), \r\n" + 
                                "    Symbol varchar(12), \r\n" + 
                                "    BaseType varchar(1), \r\n" + 
                                "    ExDeficit decimal(18,0), \r\n" + 
                                "    Price decimal(19,10) \r\n" + 
                                ") \r\n" + 
                                "Insert Into dbo.#Exdeficits \r\n" +
                                "Select M.[System], \r\n" +                                 //column: tbMemoSegExDeficits.System => tbBpxExpend.BookGroup
                                "    M.SecId, \r\n" + 
                                "    (Select Symbol From dbo.SecurityBase With (NOLOCK) Where CUSIP = M.SecId And Firm = '10' \r\n" +  
                                "    )As Symbol, \r\n" + 
                                "    (Select SubString([Description],1,1) From dbo.SecurityType With (NOLOCK)  \r\n" + 
                                "     Where Firm = '10' \r\n" +
                                "     And SecurityTypeCode = (Select SecurityTypeCode From dbo.SecurityBase With (NOLOCK)  \r\n" + 
                                "                               Where CUSIP = M.SecId And Firm = '10') \r\n" +  
                                "    ) As BaseType, \r\n" +
                                "    M.ExDeficit, \r\n" +                                   // tbMemoSegExDeficits.ExDeficit => tbBpxExpend.Excess
                                "    (Select ClosingPrice FROM dbo.SecurityPrice With (NOLOCK)  \r\n" + 
                                "     Where CUSIP = M.SecId And FIRM = '10' And ProcessDate = '" + Master.BizDatePrior + "' \r\n" +
                                "    ) As Price \r\n" + 
                                "From  dbo.tbMemoSegExDeficits M With (NOLOCK) \r\n" +      //New table: [tbBoxExpend]
                                "Where M.BizDate = '" + Master.BizDate + "' \r\n" +
                                "And   M.ExDeficit < 0 \r\n" + 
                                "Select [System],  \r\n" + 
                                "    SecId, \r\n" + 
                                "    Symbol, \r\n" + 
                                "    ExDeficit, \r\n" + 
                                "    BaseType, \r\n" + 
                                "   (Case When BaseType = 'B' Then (ExDeficit * (Price/100))  \r\n" +
                                "    Else (ExDeficit * Price) End \r\n" + 
                                "   )As Amount \r\n" + 
                                "From  dbo.#ExDeficits With (NOLOCK) \r\n" + 
                                "Where [System] = '" + bookGroup + "' \r\n" +
                                "Order By [System], SecId  \r\n" +
                                "Drop Table dbo.#ExDeficits \r\n";

                            SqlCommand dbCmd = new SqlCommand(sql, localDbCn);
                            dbCmd.CommandType = CommandType.Text;
                            dbCmd.CommandTimeout = 900;

                            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                            dataAdapter.Fill(dsMemoSegExDeficits, "MemoSegExDeficits");

                            rowCount = dsMemoSegExDeficits.Tables["MemoSegExDeficits"].Rows.Count;

                            Email mail = new Email(dbCnStr);
                            Filer filer = new Filer();

                            filePath = Excel.ExportDataSetToExcel(ref dsMemoSegExDeficits, "MemoSegExDeficits", "MemoSegExDeficits("+bookGroup+")", true);

                            string to = KeyValue.Get("AresMemoSegExDeficitsMailTo", "support.stockloan@penson.com;MBattaini@Penson.com;dchen@penson.com", dbCnStr);

                            if (!to.Equals(""))
                            {
                                mail.Send(to, 
                                          KeyValue.Get("AresMemoSegExDeficitsMailFrom", "stockloan@penson.com", dbCnStr),
                                          KeyValue.Get("AresMemoSegExDeficitsMailSubject", "Memo Seg ExDeficits", dbCnStr) + " (" + bookGroup + ") For: " + Master.BizDate,
                                          "For ProcessDate = " + Master.BizDatePrior, 
                                          filePath);
                                KeyValue.Set("AresMemoSegExDeficitsMailDate(" + bookGroup + ")", Master.ContractsBizDate, dbCnStr);
                                Log.Write("MemoSegExDeficits ("+bookGroup+") Email completed for " + Master.ContractsBizDate + ".  [AresMain.MemoSegExDeficitsMail]", Log.Information, 2);
                            }
                        }
                    }
                }
            }
			catch (Exception error)	
			{
				Log.Write("ERROR: " + error.Message + ".  [AresMain.MemoSegExDeficitsMail]", Log.Error, 1);
				return;
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}
        }

        public void BankLoanReleaseMail(string book)
        { 

            string sql = "";
            string filePath = "";
            int rowCount = 0;

            SqlConnection localDbCn = new SqlConnection(dbCnStr);
            DataSet dsBankLoanRelease = new DataSet();

            try
            {
                if (Master.BizDate.Equals(Master.ContractsBizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
                {
                    if (KeyValue.Get("AresBankLoanReleaseMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
                    {
                        Log.Write("Bank Loan Release Email has already occured for " + Master.ContractsBizDate + ".  [AresMain.BankLoanReleaseMail]", Log.Information, 2);
                    }
                    else
                    {
                        if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresBankLoanReleaseMakeTime", "09:00", dbCnStr), "HH:mm", null))
                        {
                            sql = "Create Table dbo.#BankLoanRelease ( \r\n" +
                                "  BookGroup varchar(10), \r\n" +
                                "  SecId varchar(12), \r\n" +
                                "  Symbol varchar(12), \r\n" +
                                "  OCC_Pledge bigint, \r\n" +
                                "  Pledged  bigint, \r\n" +
                                "  CNS_FTD  bigint, \r\n" +
                                "  BRK_FTD  bigint, \r\n" +
                                "  DVP_FTD  bigint, \r\n" +
                                "  ExDeficit bigint, \r\n" +
                                "  BankLoanRelease bigint, \r\n" +
                                "  BankLoanLeftover bigint, \r\n" +
                                "  OCC_Release bigint, \r\n" +
                                "  OCC_Leftover bigint ) \r\n" +
                                "Insert Into dbo.#BankLoanRelease \r\n" +
                                "Select  B.BookGroup, \r\n" +
                                "  B.SecId, \r\n" +
                                "  (Select Symbol From dbo.SecurityBase With (NOLOCK) Where CUSIP = B.Secid And Firm = '10' \r\n" +  
                                "  ) As Symbol, \r\n" +
                                "  B.Quantity As OCC_Pledge, \r\n" +
                                "  IsNull(( Select Sum(Quantity) From dbo.tbBankLoanPosition With (NOLOCK)  \r\n" +
                                "     Where BookGroup = B.BookGroup And Book <> B.book And SecId = B.SecId \r\n" +
                                "    ), 0) AS Pledged, \r\n" +
                                "  (Select ClearingFailOutSettled From dbo.tbBoxPosition With (NOLOCK)  \r\n" +
                                "     Where BookGroup = B.BookGroup And SecId = B.SecId \r\n" +
                                "     And   BizDate = '" + Master.BizDate + "' \r\n" +
                                "   ) As CNS_FTD, \r\n" +
                                "  (Select BrokerFailOutSettled From dbo.tbBoxPosition With (NOLOCK)  \r\n" +
                                "     Where BookGroup = B.BookGroup And SecId = B.SecId \r\n" +
                                "     And   BizDate = '" + Master.BizDate + "' \r\n" +
                                "   ) As BRK_FTD, \r\n" +
                                "  (Select DvpFailOutSettled From dbo.tbBoxPosition With (NOLOCK)  \r\n" +
                                "     Where BookGroup = B.BookGroup And SecId = B.SecId \r\n" +
                                "     And   BizDate = '" + Master.BizDate + "' \r\n" +
                                "   ) As DVP_FTD, \r\n" +
                                "  (Select ExDeficitSettled From dbo.tbBoxPosition With (NOLOCK)  \r\n" +
                                "     Where BookGroup = B.BookGroup And SecId = B.SecId \r\n" +
                                "     And   BizDate = '" + Master.BizDate + "' \r\n" +
                                "   ) As ExDeficit, \r\n" +
                                "  0 As BankLoanRelease, \r\n" +
                                "  0 As BankLoanLeftover, \r\n" +
                                "  0 AS OCC_Release, \r\n" +
                                "  0 As OCC_Leftover \r\n" +
                                "From  dbo.tbBankLoanPosition B With (NOLOCK) \r\n" +
                                "Where B.Book = '" + book + "' \r\n" +
                                "Order by 2 \r\n" +
                                " \r\n" +
                                "Update  dbo.#BankLoanRelease \r\n" +
                                "Set  BankLoanRelease = (Case When((CNS_FTD + BRK_FTD + DVP_FTD) + (Case When ExDeficit > 0 Then 0 Else ABS(ExDeficit) END) \r\n" +
                                "           ) > Pledged  \r\n" +
                                "        Then Pledged  \r\n" +
                                "        Else ((CNS_FTD + BRK_FTD + DVP_FTD) + (Case when ExDeficit > 0 Then 0 Else ABS(ExDeficit) END) \r\n" +
                                "        )  \r\n" +
                                "         END) \r\n" +
                                "Where  Pledged > 0 \r\n" +
                                " \r\n" +
                                "Update  dbo.#BankLoanRelease \r\n" +
                                "Set  BankLoanLeftover = (CNS_FTD + BRK_FTD + DVP_FTD)  \r\n" +
                                "         + (Case when ExDeficit > 0 Then 0 Else ABS(ExDeficit) END)  \r\n" +
                                "         - BankLoanRelease \r\n" +
                                " \r\n" +
                                "Update  dbo.#BankLoanRelease \r\n" +
                                "Set  OCC_Release = (Case When(BankLoanRelease) > OCC_Pledge  \r\n" +
                                "        Then OCC_Pledge  \r\n" +
                                "        Else BankLoanLeftOver END ) \r\n" +
                                "Where  OCC_Pledge > 0 \r\n" +
                                " \r\n" +
                                "Update  dbo.#BankLoanRelease \r\n" +
                                "Set  OCC_Leftover = OCC_Pledge - OCC_Release \r\n" +
                                " \r\n" +
                                "Update  dbo.#BankLoanRelease \r\n" +
                                "Set  OCC_Release = OCC_Release + OCC_Leftover \r\n" +
                                "Where  OCC_Release > OCC_Pledge \r\n" +
                                " \r\n" +
                                "Select  BookGroup, SecId, Symbol, \r\n" + 
                                "  OCC_Pledge, Pledged, CNS_FTD, BRK_FTD, DVP_FTD, \r\n" + 
                                "  ExDeficit, BankLoanRelease, OCC_Release \r\n" + 
                                "From dbo.#BankLoanRelease With (NOLOCK)  \r\n" +
                                "Order By BookGroup, SecId \r\n" +
                                " \r\n" +
                                "Drop Table dbo.#BankLoanRelease  \r\n";
                            
                            SqlCommand dbCmd = new SqlCommand(sql, localDbCn);
                            dbCmd.CommandType = CommandType.Text;
                            dbCmd.CommandTimeout = 900;

                            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                            dataAdapter.Fill(dsBankLoanRelease, "BankLoanRelease");

                            rowCount = dsBankLoanRelease.Tables["BankLoanRelease"].Rows.Count;

                            Email mail = new Email(dbCnStr);
                            Filer filer = new Filer();

                            filePath = Excel.ExportDataSetToExcel(ref dsBankLoanRelease, "BankLoanRelease", "BankLoanRelease_(Book" + book + ")", true);

                            string to = KeyValue.Get("AresBankLoanReleaseMailTo", "support.stockloan@penson.com;MBattaini@Penson.com;dchen@penson.com", dbCnStr);

                            if (!to.Equals(""))
                            {
                                mail.Send(to,
                                          KeyValue.Get("AresBankLoanReleaseMailFrom", "stockloan@penson.com", dbCnStr),
                                          KeyValue.Get("AresBankLoanReleaseMailSubject", "Bank Loan Release", dbCnStr) + " For: " + Master.BizDate,
                                          "For ProcessDate = " + Master.BizDatePrior + "\r\nBook =" + book,
                                          filePath);
                                KeyValue.Set("AresBankLoanReleaseMailDate", Master.ContractsBizDate, dbCnStr);
                                Log.Write("Bank Loan Release Email completed for " + Master.ContractsBizDate + ".  [AresMain.BankLoanReleaseMail]", Log.Information, 2);
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Log.Write("ERROR: " + error.Message + ".  [AresMain.BankLoanReleaseMail]", Log.Error, 1);
                return;
            }
            finally
            {
                if (localDbCn.State != ConnectionState.Closed)
                {
                    localDbCn.Close();
                }
            }
        }

		public void WebServerHealthCheck()
		{
            if ((DateTime.Now.DayOfWeek != DayOfWeek.Saturday) && (DateTime.UtcNow.DayOfWeek != DayOfWeek.Sunday))
            {
                if (!KeyValue.Get("AresStockLoanSVRCheckDate", "", dbCnStr).Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
                {
                    if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresCheckSiteSetTime", "06:00", dbCnStr), "HH:mm", null))
                    {
                        Log.Write("Web Server Health Check Starting:  [AresMain.WebServerHealthCheck]", 2);

                        int serverCount = int.Parse(Standard.ConfigValue("ActiveServerCount"));
                        string serverPrefix = "ServerList-";
                        string messagePrefix = "ServerDescr-";
                        string serverToGet = "";
                        string descriptionToGet = "";
                        string server = "";
                        string message = "";
                        string url = "";
                        string sReturn = "";

                        for (int i = 0; i < serverCount; i++)
                        {
                            serverToGet = serverPrefix + i;
                            descriptionToGet = messagePrefix + i;

                            server = Standard.ConfigValue(serverToGet);
                            message = Standard.ConfigValue(descriptionToGet);

                            url = server;
                            try
                            {
                                string webProxy = Standard.ConfigValue("HttpWebProxy");
                                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://" + url);

                                if (!webProxy.Equals(""))
                                {
                                    httpWebRequest.Proxy = new System.Net.WebProxy(webProxy, true);
                                    httpWebRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                                }
                                httpWebRequest.KeepAlive = false; 

                                System.Net.HttpWebResponse httpWebResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();

                                if (httpWebResponse.StatusCode.ToString().ToUpper().Equals("OK"))
                                {
                                    sReturn += message + " " + httpWebResponse.StatusCode.ToString().ToUpper() + " " + "\r\n\r\n"; //BS; Add extra \r\n
                                }

                            }
                            catch (Exception e)
                            {
                                Log.Write("An Error Occurred:  [AresMain.WebServerHealthCheck] - Message: " + e.Message, 2);
                                sReturn += message + " ERROR! Message = " + e.Message.ToString() + "\r\n";
                            }

                        }


                        {
                            Email email = new Email(dbCnStr);
                            email.Send( //"bstone@penson.com", "support.stockloan@penson.com", "TEST Daily Web Server Health Checks", sReturn);
                                  KeyValue.Get("AresStockLoanEmailTo", "support.stockloan@penson.com", dbCnStr),
                                  KeyValue.Get("AresEmailFrom", "sendero@penson.com", dbCnStr),
                                  KeyValue.Get("AresStockLoanSVRCheckSubject", "Daily Web Server Health Checks", dbCnStr), sReturn);
                        }
                        KeyValue.Set("AresStockLoanSVRCheckDate", Master.BizDate, dbCnStr);
                        Log.Write("Web Server Health Check Completed:  [AresMain.WebServerHealthCheck]", 2);
                    }            
                }
                else
                {
                    Log.Write("Web Server Health Check did not run. Not correct time:  [AresMain.WebServerHealthCheck]", 2);
                }
			}
            else
            {
                Log.Write("Web Server Health Check does not run on Saturday or Sunday:  [AresMain.WebServerHealthCheck]", 2);
            }
        }      

        private void FINRABalancesMail()
        {	
            SqlConnection wwDbCn = null;
            SqlDataAdapter wwDataAdapter = null;
            DataSet dsFinraBalances = new DataSet();
            DataSet dsReportRecipients = new DataSet();

            string sql = "";
            string fileMakeTime = null;
            string filePath = "";
            string emailBody = "";

            int rptCount = 0;
            int rowCount = 0;
            bool errFlag = false;

            if (!Master.ContractsBizDate.Equals(Master.BizDate) || !Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
            {                
                return;
            }
            if (KeyValue.Get("AresFinraBalancesMailDate", Master.BizDatePrior, dbCnStr).Equals(Master.BizDate))
            {
                Log.Write("FINRA Balances file email has already occured, for BizDate: " + Master.BizDatePrior + ".  [AresMain.FINRABallancesMail]", 2);
                return;
            }

            fileMakeTime = KeyValue.Get("AresFinraBalancesMakeTime", "11:00", dbCnStr);
            if (fileMakeTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) > 0)
            {
                return;
            }

            try
            {
                wwDbCn = new SqlConnection(dbCnStr);

                sql =   "Select  BookGroup as 'Book Group',\n" +
                        "PoolCode as 'PC',\n" +
                        "sum(case when contracttype = 'B' then amountsettled else 0 end) as 'Borrow Total',\n" +
                        "sum(case when contracttype = 'L' then amountsettled else 0 end) as 'Loan Total'\n" +
                        "from    dbo.tbContracts \n" +
                        "Where   bizdate = '" + Master.BizDatePrior + "'\n" +
                            "And BookGroup in ('7380','0234','0158') \n" +
                        "group by bookgroup, poolcode \n" +
                        "order by bookgroup \n";

                SqlCommand wwDbCmd = new SqlCommand(sql, wwDbCn);
                wwDbCmd.CommandType = CommandType.Text;
                wwDbCmd.CommandTimeout = 900;

                wwDataAdapter = new SqlDataAdapter(wwDbCmd);
                wwDataAdapter.Fill(dsFinraBalances, "FinraBalances");

                if (dsFinraBalances.Tables["FinraBalances"].Rows.Count == 0)
                {
                    Log.Write("ERROR: No data returned from [Sendero].[Contracts] table.  [AresMain.FINRABalancesMail]", 2);
                    return;
                }

                Email mail = new Email(dbCnStr);

                rowCount = dsFinraBalances.Tables["FinraBalances"].Rows.Count;
                
                emailBody = "FINRA Balances for " + Master.BizDatePrior + "\r\n \r\n";                           
                
                dsReportRecipients = ReportRecipientsGet("FINRA_Balances");

                filePath = Excel.ExportDataSetToExcel(ref dsFinraBalances, "FinraBalances", "FINRA_Balances", true);
                
                string from = KeyValue.Get("AresFinraBalancesMailFrom", "mbattaini@penson.com", dbCnStr);                
                string subject = "FINRA_" + Master.BizDatePrior + " - Balances";

                foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
                {
                    try
                    {
                        if (dr["LastDeliveredDate"].ToString().Equals(""))
                        {
                            dr["LastDeliveredDate"] = Master.BizDatePrior;
                        }

                        if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
                        {
                            if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
                            {
                                mail.Send(dr["ReportRecipient"].ToString(), from, subject, emailBody, filePath);
                                Log.Write("FINRA Balancs email sent to: " + dr["ReportRecipient"].ToString() + "  [AresMain.FINRABalancesMail]", 1);
                            }

                            ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.BizDate);
                            rptCount++;
                        }
                    }
                    catch (Exception error)
                    {
                        errFlag = true;
                        Log.Write("ERROR (foreach loop): " + error.Message.Replace("\r\n", "") + ".  [AresMain.FINRABalancesMail]", Log.Error, 1);
                    }
                }

                if (!errFlag)
                {
                    KeyValue.Set("AresFinraBalancesMailDate", Master.BizDate, dbCnStr);
                    Log.Write("Daily FINRA_Balances file mailed, for BizDate: " + Master.BizDatePrior + ". Data RowCount = " + rowCount.ToString() + ", Recipient Count = " + rptCount.ToString() + ". [AresMain.FINRABalancesMail]", 2);
                }
            }
            catch (Exception error)
            {
                Log.Write("ERROR: " + error.Message + ".  [AresMain.FINRABalancesMail]", Log.Error, 1);
                return;
            }
            finally
            {
                if (wwDbCn.State != ConnectionState.Closed)
                {
                    wwDbCn.Close();
                }
            }

        }

        private void FINRABorrowsMail(string bookGroup)
        {	
            SqlConnection wwDbCn = null;
            SqlDataAdapter wwDataAdapter = null;
            DataSet dsFinraBorrows = new DataSet();
            DataSet dsReportRecipients = new DataSet();

            string sql = "";
            string filePath = "";
            string fileMakeTime = null;

            int rptCount = 0;
            int rowCount = 0;
            bool errFlag = false;

            if (!Master.ContractsBizDate.Equals(Master.BizDate) || !Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
            {              
                return;
            }
            if (KeyValue.Get("AresFinraBorrowsMailDate(" + bookGroup + ")", Master.BizDatePrior, dbCnStr).Equals(Master.BizDate))
            {
                Log.Write("FINRA Borrows (" + bookGroup + ") file email has already occured, for BizDate: " + Master.BizDatePrior + ".  [AresMain.FINRABorrowsMail]", 2);
                return;
            }
            
            fileMakeTime = KeyValue.Get("AresFinraBorrowsMakeTime", "11:00", dbCnStr);
            if (fileMakeTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) > 0)
            {
                return;
            }

            try
            {
                wwDbCn = new SqlConnection(dbCnStr);

                sql = "Select  BE.SecId, \n" +
                        "      (Select Symbol from dbo.SecurityBase With (NOLOCK) \n" +
                        "       Where Cusip = BE.secid and Firm = '10' ) As Symbol, \n" +
                        "      BE.Excess As ExDeficit, \n" +
                        "      IsNull(Sum(QuantitySettled), 0) As Quantity, \n" +
                        "      IsNull(Sum(AmountSettled), 0) As Amount \n" +
                        "Into  dbo.#Finra \n" +
                        "From  dbo.tbBoxExpend BE, \n" +
                        "      dbo.tbContracts C \n" +
                        "Where BE.BizDate = '" + Master.BizDatePrior + "' \n" +
                        "And   BE.BookGroup = '" + bookGroup + "' \n" +
                        "And   BE.Excess < 0 \n" +
                        "And   C.BizDate = BE.BizDate \n" +
                        "And   C.BookGroup = BE.BookGroup \n" +
                        "And   C.ContractType = 'B' \n" +
                        "And   C.SecId =* BE.SecId \n" +
                        "And   C.SettleDate = C.BizDate \n" +
                        "And   C.SecId Is Not Null \n" +
                        "Group by BE.SecId, BE.Excess \n" +
                        "Select SecId, \n" +
                        "       Symbol, \n" +
                        "       Abs(ExDeficit)As Deficit, \n" +
                        "       (Case When Abs(ExDeficit) > Quantity Then Quantity Else Abs(ExDeficit) End) As Covered, \n" +
                        "       (Case When Amount > 0 And Quantity > 0 Then IsNull((Amount/Quantity), 0) Else 0 End) As Price \n" +
                        "Into   dbo.#Finra2 \n" +
                        "From   dbo.#Finra \n" +
                        "Order by symbol \n" +
                        "Select SecId, \n" +
                        "       Symbol, \n" +
                        "       Convert(bigint, Deficit) as Deficit, \n" +
                        "       Convert(bigint, Covered) as Covered, \n" +
                        "       Price, \n" +
                        "       (Covered * Price) 'Borrow Value' \n" +
                        "From dbo.#Finra2 \n" +
                        "Drop Table dbo.#FInra \n" +
                        "Drop Table dbo.#FInra2 ";

                SqlCommand wwDbCmd = new SqlCommand(sql, wwDbCn);
                wwDbCmd.CommandType = CommandType.Text;
                wwDbCmd.CommandTimeout = 900;

                wwDataAdapter = new SqlDataAdapter(wwDbCmd);
                wwDataAdapter.Fill(dsFinraBorrows, "FinraBorrows");

                if (dsFinraBorrows.Tables["FinraBorrows"].Rows.Count == 0)
                {
                    Log.Write("ERROR: No data returned from [Sendero].[MemoSegDeficits] table, (System = " + bookGroup + ").  [AresMain.FINRABorrowsMail]", 2);
                    return;
                }

                rowCount = dsFinraBorrows.Tables["FinraBorrows"].Rows.Count;

                dsReportRecipients = ReportRecipientsGet("FINRA_Borrows("+bookGroup+")"); 

                Email mail = new Email(dbCnStr);
                Filer filer = new Filer();

                string subject = "FINRA Borrows (" + bookGroup + ") - " + Master.BizDatePrior;

                string from = KeyValue.Get("AresFinraBorrowsMailFrom", "stockloan@penson.com", dbCnStr);                
                string sendTo = "";

                filePath = Excel.ExportDataSetToExcel(ref dsFinraBorrows, "FinraBorrows", "FINRA_Borrows("+bookGroup+")", true);

                foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
                {
                    try
                    {
                        if (dr["LastDeliveredDate"].ToString().Equals(""))
                        {
                            dr["LastDeliveredDate"] = Master.BizDatePrior;
                        }

                        if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
                        {
                            if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
                            {
                                sendTo = dr["ReportRecipient"].ToString();
                                mail.Send(sendTo, from, subject, "", filePath);
                                Log.Write("FINRA Borrows (" + bookGroup + ") email sent to: " + dr["ReportRecipient"].ToString() + "  [AresMain.FINRABorrowsMail]", 1);
                            }

                            ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.BizDate);
                            rptCount++;
                        }
                    }
                    catch (Exception error)
                    {
                        errFlag = true;
                        Log.Write("ERROR (foreach loop): " + error.Message.Replace("\r\n", "") + ".  [AresMain.FINRABorrowsMail]", Log.Error, 1);
                    }
                }

                if (!errFlag)
                {
                    KeyValue.Set("AresFinraBorrowsMailDate(" + bookGroup + ")", Master.BizDate, dbCnStr);
                    Log.Write("Daily FINRA_Borrows(" + bookGroup + ") file mailed, for BizDate: " + Master.BizDatePrior + ". Data RowCount = " + rowCount.ToString() + ", Recipient Count = " + rptCount.ToString() + ". [AresMain.FINRABorrowsMail]", 2);
                }

            }
            catch (Exception error)
            {
                Log.Write("ERROR: " + error.Message + ".  [AresMain.FINRABorrowsMail]", Log.Error, 1);
                return;
            }
            finally
            {
                if (wwDbCn.State != ConnectionState.Closed)
                {
                    wwDbCn.Close();
                }
            }

        }

        private void PenaltyBoxExceptionsMail(string bookGroup)
        {
            SqlConnection wwDbCn = null;
            SqlDataAdapter wwDataAdapter = null;

            DataSet dsPbExceptions = new DataSet();
            DataSet dsReportRecipients = new DataSet();
           
            string filePath = "";
            string fileMakeTime = null;

            int rptCount = 0;
            bool errFlag = false;
             
            if (!Master.ContractsBizDate.Equals(Master.BizDate) || 
                !Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)) ||
                !KeyValue.Get("BroadRidgeBoxPositionLoadDate", "", dbCnStr).Equals(Master.BizDate) )                
            {
                return;
            }
            if (KeyValue.Get("AresPenaltyBoxExceptionsMailDate(" + bookGroup + ")", Master.BizDatePrior, dbCnStr).Equals(Master.BizDate))
            {
                Log.Write("Penalty Box Exceptions (" + bookGroup + ") file email has Already Occured, Data from BizDatePrior: " + Master.BizDatePrior + ".  [AresMain.PenaltyBoxExceptionsMail]", 2);
                return;
            }

            fileMakeTime = KeyValue.Get("AresPenaltyBoxExceptionsMailDateTime", "11:30", dbCnStr);  
            if (fileMakeTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) > 0)
            {
                Log.Write("Penalty Box Exceptions (" + bookGroup + ") Must Wait till " + fileMakeTime.ToString() + " to Run.   [AresMain.PenaltyBoxExceptionsMail]", 2);
                return;
            }

            try
            {
                wwDbCn = new SqlConnection(dbCnStr);

               SqlCommand wwDbCmd = new SqlCommand("spBorrowPenaltyTradeExceptionsGet", wwDbCn);
               wwDbCmd.CommandType = CommandType.StoredProcedure;

               SqlParameter paramBizDatePrior = wwDbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
               paramBizDatePrior.Value = Master.BizDatePrior;

               wwDataAdapter = new SqlDataAdapter(wwDbCmd);
                wwDataAdapter.Fill(dsPbExceptions, "Exceptions");


               filePath = Excel.ExportDataSetToExcel(ref dsPbExceptions, "Exceptions", "Exceptions", true);

                Email mail = new Email(dbCnStr);
                string subject = "Penalty Box Exceptions (" + bookGroup + "), for " + Master.BizDatePrior;
                string from = KeyValue.Get("AresPenaltyBoxExceptionsMailFrom", "stockloan@penson.com", dbCnStr);
                string sendTo = "";

                dsReportRecipients = ReportRecipientsGet("PenaltyBoxExceptions(" + bookGroup + ")");

                foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
                {
                    try
                    {
                        if (dr["LastDeliveredDate"].ToString().Equals(""))
                        {
                            dr["LastDeliveredDate"] = Master.BizDatePrior;
                        }

                        if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
                        {
                            if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
                            {
                                sendTo = dr["ReportRecipient"].ToString();
                                mail.Send(sendTo, from, subject, "For ProcessDate = " + Master.BizDatePrior, filePath);
                                Log.Write("PenaltyBoxException (" + bookGroup + ") email sent to: " + dr["ReportRecipient"].ToString() + "  [AresMain.PenaltyBoxExceptionsMail]", 1);
                            }

                            ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.BizDate);
                            rptCount++;
                        }
                    }
                    catch (Exception error)
                    {
                        errFlag = true;
                        Log.Write("ERROR (foreach loop): " + error.Message.Replace("\r\n", " ") + ".  [AresMain.PenaltyBoxExceptionsMail]", Log.Error, 1);
                    }
                }

                if (!errFlag)
                {
                    KeyValue.Set("AresPenaltyBoxExceptionsMailDate(" + bookGroup + ")", Master.BizDate, dbCnStr);
                    Log.Write("Penalty Box Exceptions (" + bookGroup + ") file mailed, Data from BizDatePrior: " + Master.BizDatePrior, 1);
                }

            }
            catch (Exception error)
            {
                Log.Write("ERROR: " + error.Message.Replace("\r\n", " ") + ".  [AresMain.PenaltyBoxExceptionsMail]", Log.Error, 1);
                return;
            }
            finally
            {
                if (wwDbCn.State != ConnectionState.Closed)
                {
                    wwDbCn.Close();
                }
            }

        }

        public void CreditDebitBalances()
		{
			if (Master.ContractsBizDate.Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresCreditDebitBalancesSetTime", "06:30", dbCnStr), "HH:mm", null))									
				{
					if (!KeyValue.Get("AresCreditDebitBalancesMailDate", "", dbCnStr).Equals(Master.BizDate))
					{
						try
						{
							string sql = "Select	P.processDate,\r\n" +
								"Sum(Case When S.SettlementDateQuantity >= 0 And S.LocMemo = 'C' Then S.SettlementDateQuantity* P.ClosingPrice  Else 0 End ) As LongAmt,\r\n" +
								"Sum(Case When S.SettlementDateQuantity < 0 And S.LocMemo = 'C' Then S.SettlementDateQuantity * P.ClosingPrice Else 0 End ) As ShortAmt\r\n" +
								"From	dbo.StockLocationCurrent S,\r\n" +
								"		dbo.SecurityPrice P, \r\n" +
								"		dbo.SecurityBase B \r\n" +
								"Where	P.ProcessDate= '" + Master.BizDatePrior + "'\r\n" +
								"And		S.Firm = '10' \r\n" +                                   
								"And		P.Firm = S.Firm\r\n" +
								"And		B.Firm = S.Firm\r\n" +
								"And		P.Cusip = S.Cusip\r\n" +
								"And		B.Cusip = S.Cusip\r\n" +
								"And		B.SecurityTypeCode In ('1', '4', 'A', 'B', 'C')\r\n" +
								"Group By P.processDate";
						
							DataSet dsTemp = new DataSet();
							string emailBody = "";

							SqlConnection localDbCn = new SqlConnection(dbCnStr);
							SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
							dbCmd.CommandType = CommandType.Text;
                            dbCmd.CommandTimeout = 9000;
							
							SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
							dataAdapter.Fill(dsTemp, "CreditDebit");				
						

							foreach(DataRow dr in dsTemp.Tables["CreditDebit"].Rows)
							{
								emailBody += DateTime.Parse(dr["ProcessDate"].ToString()).ToString(Standard.DateFormat) + " " + decimal.Parse(dr["LongAmt"].ToString()).ToString("#,##0.00") + " " + decimal.Parse(dr["ShortAmt"].ToString()).ToString("#,##0.00") + "\r\n";
							}

							string to = KeyValue.Get("AresCreditDebitBalancesMailTo", "mbattaini@penson.com", dbCnStr);							
						
							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresCreditDebitBalancesMailFrom", "stockloan@penson.com", dbCnStr),
									KeyValue.Get("AresCreditDebitBalancesMailSubject", "Credit / Debit Balances For  ", dbCnStr)  + Master.BizDate,
									emailBody);						
							}
	
							KeyValue.Set("AresCreditDebitBalancesMailDate", Master.ContractsBizDate, dbCnStr);
							
						}
						catch (Exception error)
						{
							Log.Write(error.Message + "[AresMain.CreditDebitBalances]", 1);
						}
					}
				}
			}
		}

		public void LondonContractsMail(string bookGroup)
		{
			DataSet dsContracts = new DataSet();
			SqlConnection localDbCn = null;
			SqlDataAdapter dataAdapter = null;

			string numOfRecords = "";
			string body = "";
			string sql = "";			
			
			if (Master.ContractsBizDate.Equals(Master.BizDate))
			{
				if (Master.ContractsBizDate.Equals(KeyValue.Get("AresLondonContractsMailDate("+bookGroup +")", "", dbCnStr)))
				{
					Log.Write("London Contracts Email has already occured for " + Master.ContractsBizDate + ".  [AresMain.LondonContractsMail]", 2); 
				}
				else
				{
					try
					{												
						Log.Write("Preparing London Contracts Email (" + bookGroup + ") for " + Master.ContractsBizDate + ".  [AresMain.LondonContractsMail]", 2);
						sql = "select	c.bookgroup,\r\n" +
							"c.book,\r\n" +
							"(Select BookName From dbo.tbBooks (nolock) Where BookGroup = '" + bookGroup + "' And Book = C.Book) As BookName,\r\n" +
							"c.contracttype,\r\n" +
							"c.secid,\r\n" +
							"s.symbol,\r\n" +
							"c.quantity,\r\n" +
							"c.amount,\r\n" +
							"c.rate,\r\n" +
							"c.margin\r\n" +
							"from	dbo.tbContracts C, \r\n" +
							"       dbo.SecurityBase S \r\n" +
							"where	c.bizdate = '" + Master.ContractsBizDate + "'\r\n" +
							"and		c.bookgroup = '" + bookGroup + "' \r\n" +
							"and		c.poolcode = 'L'\r\n" +
							"and		s.cusip = c.secid\r\n" +
                            "and		S.Firm = '10' \r\n" +                       
							"Order By BookGroup";
																	
						localDbCn = new SqlConnection(dbCnStr);
						
						SqlCommand dbCmd = new SqlCommand(sql, localDbCn);
						dbCmd.CommandType = CommandType.Text;
						
						dataAdapter = new SqlDataAdapter(dbCmd);
						dataAdapter.Fill(dsContracts, "Contracts" + bookGroup);	
						
						body = "London Contracts\r\n\r\n";
						body += "BookGroup".PadRight(11, ' ') + "Book".PadRight(11, ' ') + "Name".PadRight(18, ' ') + "T".PadRight(3, ' ') + "Security ID".PadRight(15, ' ') + "Symbol".PadRight(8, ' ') +  "Quantity".PadRight(10, ' ') + "Amount".PadRight(15, ' ') + "Rate".PadRight(8) + "Margin".PadRight(8) +"\r\n";
						body += "---------".PadRight(11, ' ') + "----".PadRight(11, ' ') + "----".PadRight(18, ' ') +  "-".PadRight(3, ' ') + "-----------".PadRight(15, ' ') + "------".PadRight(8, ' ') +  "--------".PadRight(10, ' ') + "------".PadRight(15, ' ') + "----".PadRight(8) + "------".PadRight(8) +"\r\n";
						
						foreach (DataRow dr in dsContracts.Tables["Contracts" + bookGroup].Rows)
						{
							body += dr["BookGroup"].ToString().PadRight(11, ' ') +
								dr["Book"].ToString().PadRight(11, ' ') + 
								dr["BookName"].ToString().PadRight(18).Substring(0, 18) +
								dr["ContractType"].ToString().PadRight(3, ' ') +								
								dr["SecId"].ToString().PadRight(15, ' ') +
								dr["Symbol"].ToString().PadRight(8, ' ').Substring(0, 8) +
								long.Parse(dr["Quantity"].ToString()).ToString("#,##0").PadRight(12, ' ').Substring(0, 12)  +
								decimal.Parse(dr["Amount"].ToString()).ToString("#,##0.00").PadRight(15, ' ').Substring(0, 15) +
								decimal.Parse(dr["Rate"].ToString()).ToString("0.00").PadRight(8, ' ').Substring(0, 8) +
								decimal.Parse(dr["Margin"].ToString()).ToString("0.00").PadRight(8, ' ').Substring(0, 8) + "\r\n";
						}

						string to = KeyValue.Get("AresLondonContractsMailTo", "mbattaini@penson.com", dbCnStr);							
						
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresLondonContractsMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresLondonContractsMailSubject", "London Contarcts ", dbCnStr) + "(" + bookGroup + ") For " + Master.ContractsBizDate,
								body);						
						}

						//------ Both Book groups are done. --------------------------------------------
                        KeyValue.Set("AresLondonContractsMailDate(" + bookGroup + ")", Master.ContractsBizDate, dbCnStr);
						Log.Write("London Contracts (" +bookGroup + ") Email completed for " + Master.ContractsBizDate + ".  [AresMain.LondonContractsMail]", 2); 
					}
					catch (Exception error)
					{
						Log.Write(error.Message + "[AresMain.AresLondonContractsMail]", 1);
					}
					finally
					{
						if (localDbCn.State != ConnectionState.Closed)
						{
							localDbCn.Close();
						}
					}
				}				
			}
		}

		public void HardToBorrowProfitabilityMail()
		{
			DataSet dsHardToBorrow = new DataSet();
			SqlConnection localDbCn = null;
			SqlDataAdapter dataAdapter = null;

			string numOfRecords = "";
			string body = "";
			
			
			if (Master.ContractsBizDate.Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				if (Master.ContractsBizDate.Equals(KeyValue.Get("AresHardToBorrowProfitabilityMailDate", "", dbCnStr)))
				{
					Log.Write("Hard To Borrow Profitability has already occured. [AresMain.AresHardToBorrowProfitabilityMail]", 1);					
				}
				else if (KeyValue.Get("ShortSaleNegRebateBillingSnapShotBizDate", "", dbCnStr).Equals(DateTime.Now.AddDays(-1.0).ToString(Standard.DateFormat)))
				{
					try
					{		
						numOfRecords = KeyValue.Get("AresHardToBorrowProfitabilityNumOfRecords", "15", dbCnStr);
						
						string sql = "select top(" + numOfRecords+ ") s.Secid,\r\n"+
							"b.symbol,\r\n"+
							"sum(isnull(s.originalcharge, 0)) as cost,\r\n" +
							"sum(isnull(s.modifiedcharge, 0)) as charge\r\n" +
							"from	dbo.tbshortsalebillingsummary s, \r\n" +
							"       dbo.securitybase b \r\n" +
							"where	s.bizdate = '" + Master.BizDatePrior + "'\r\n" +
							"and	b.cusip = s.secid \r\n" +
                            "and    b.Firm = '10' \r\n" +                   
							"Group By s.SecId, b.symbol\r\n" +
							"Order By charge DESC";						
						
						localDbCn = new SqlConnection(dbCnStr);
						
						SqlCommand dbCmd = new SqlCommand(sql, localDbCn);
						dbCmd.CommandType = CommandType.Text;
                        dbCmd.CommandTimeout = 900;
						
						dataAdapter = new SqlDataAdapter(dbCmd);
						dataAdapter.Fill(dsHardToBorrow, "HTB");	
																		
						
						body = "Most Profitable Securities\r\n\r\n";
						body += "Cusip".PadRight(11, ' ') + "Symbol".PadRight(10, ' ') + "Cost".PadRight(15, ' ') + "Charge".PadRight(15, ' ') + "\r\n";
						body += "-----".PadRight(11, ' ') + "------".PadRight(10, ' ') + "----".PadRight(15, ' ') + "------".PadRight(15, ' ') + "\r\n";
						
						foreach (DataRow dr in dsHardToBorrow.Tables["HTB"].Rows)
						{
							body += dr["SecId"].ToString().PadRight(11, ' ') +
								dr["Symbol"].ToString().PadRight(10, ' ') +
								decimal.Parse(dr["Cost"].ToString()).ToString("#,##0.00").PadLeft(15, ' ').Substring(0, 15) +
								decimal.Parse(dr["Charge"].ToString()).ToString("#,##0.00").PadLeft(15, ' ').Substring(0, 15) + "\r\n";
						}


						body += "\r\n\r\n\r\n";
						body += "Deepest Hard To Borrow Rates\r\n";

						sql = "select	distinct(s.Secid),\r\n" +
							"B.symbol,\r\n" +
							"s.rate\r\n" +
							"from	dbo.tbshortsalebillingsummary s, \r\n" + 
							"		dbo.securitybase b \r\n" +
							"where	s.bizdate = '" + Master.BizDatePrior +"'\r\n" +
							"and	b.cusip = s.secid\r\n" +
							"and	s.rate is not null \r\n" +
                            "and	b.Firm = '10' \r\n" +
							"Group By s.SecId, b.symbol, s.rate\r\n" +
							"Order By s.rate asc";

						
						dbCmd.CommandText = sql;
						dataAdapter.Fill(dsHardToBorrow, "HTBRates");	

						body += "Cusip".PadRight(11, ' ') + "Symbol".PadRight(10, ' ') + "Rate".PadRight(8, ' ') + "\r\n";
						body += "-----".PadRight(11, ' ') + "------".PadRight(10, ' ') + "----".PadRight(8, ' ') + "\r\n";					

						int counter = int.Parse(numOfRecords);

						foreach (DataRow dr in dsHardToBorrow.Tables["HTBRates"].Rows)
						{
							if (counter == 0) break;

							body += dr["SecId"].ToString().PadRight(11, ' ') +
								dr["Symbol"].ToString().PadRight(10, ' ') +
								float.Parse(dr["Rate"].ToString()).ToString("0.000").PadLeft(8, ' ') + "\r\n";
							
							counter --;
						}

						string to = KeyValue.Get("AresHardToBorrowProfitabilityMailTo", "mbattaini@penson.com", dbCnStr);							
						
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresHardToBorrowProfitabilityMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresHardToBorrowProfitabilityMailSubject", "Hard To Borrow Profitability For ", dbCnStr) + Master.BizDatePrior,
								body);						
						}
	
						KeyValue.Set("AresHardToBorrowProfitabilityMailDate", Master.ContractsBizDate, dbCnStr);
					}
					catch (Exception error)
					{
						Log.Write(error.Message + "[AresMain.HardToBorrowProfitabilityMail]", 1);
					}
					finally
					{
						if (localDbCn.State != ConnectionState.Closed)
						{
							localDbCn.Close();
						}
					}
				}				
			}
		}

		public void HardToBorrowNotBilledMail()
		{
			DataSet dsHardToBorrow = new DataSet();
			SqlConnection localDbCn = null;
			SqlDataAdapter dataAdapter = null;
	
			string body = "";
			
			
			if (Master.ContractsBizDate.Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				if (Master.ContractsBizDate.Equals(KeyValue.Get("AresHardToBorrowNotBilledMailDate", "", dbCnStr)))
				{
					Log.Write("Hard To Borrow Profitability has already occured. [AresMain.AresHardToBorrowProfitabilityMail]", 1);					
				}
				else if (KeyValue.Get("ShortSaleNegRebateBillingSnapShotBizDate", "", dbCnStr).Equals(Master.BizDate))
				{
					try
					{		
						
						string sql = "SELECT	DISTINCT(C.SECID) AS SECID,\r\n"+
																	"ISNULL(SUM(C.QUANTITYSETTLED), 0) AS QUANTITY,\r\n"+
																	"AVG(C.RATE) AS RATE,\r\n"+
																	"ISNULL((SELECT CUSTOMERSHORTSETTLED FROM TBBOXPOSITION WHERE BIZDATE = C.BIZDATE AND SECID = C.SECID AND BOOKGROUP = C.BOOKGROUP), 0) AS SHORT,\r\n"+
																	"ISNULL((SELECT  IsNull(ClearingFailOutSettled, 0) + IsNull(DvpFailOutSettled, 0) + IsNull(BrokerFailOutSettled, 0) FROM TBBOXPOSITION WHERE BIZDATE = C.BIZDATE AND SECID = C.SECID AND BOOKGROUP = C.BOOKGROUP), 0) AS FAILS\r\n"+
													"INTO		#SHORTS\r\n"+
													"FROM		TBCONTRACTS C\r\n"+
													"WHERE	C.BIZDATE = '" + Master.BizDate + "'\r\n"+
													"AND			C.CONTRACTTYPE = 'B'\r\n"+
													"AND			C.BOOKGROUP = '0158' \r\n" +     
													"AND			(C.RATE < 0 OR C.POOLCODE = '6')\r\n"+
													"AND			C.SECID NOT IN (SELECT SECID FROM TBSHORTSALEBILLINGSUMMARY WHERE	BIZDATE = C.BIZDATE)\r\n"+
													"GROUP BY C.SECID, C.BIZDATE, C.BOOKGROUP\r\n"+
													"DELETE	FROM	#SHORTS WHERE SHORT = 0\r\n"+
													"SELECT	S.SECID,\r\n"+
													"				B.SYMBOL,\r\n"+
													"				S.QUANTITY,\r\n"+
													"				S.RATE,\r\n"+
													"				S.SHORT,\r\n"+
													"				S.FAILS\r\n"+
													"FROM		#SHORTS S,\r\n"+
													"			SECURITYBASE B \r\n"+	
													"WHERE	B.CUSIP = S.SECID \r\n"+
                                                    "AND	B.FIRM = '10' \r\n" +
													"ORDER BY B.SYMBOL\r\n";

						localDbCn = new SqlConnection(dbCnStr);
						
						SqlCommand dbCmd = new SqlCommand(sql, localDbCn);
						dbCmd.CommandType = CommandType.Text;
                        dbCmd.CommandTimeout = 900;
						
						dataAdapter = new SqlDataAdapter(dbCmd);
						dataAdapter.Fill(dsHardToBorrow, "HTB");	
																		
					
						body = "Cusip".PadRight(11, ' ') + "Symbol".PadRight(10, ' ') + "Quantity".PadRight(15, ' ') + "Rate".PadRight(8, ' ') + "Short".PadRight(15, ' ') + "Fails".PadRight(15, ' ') + "\r\n";
						body += "-----".PadRight(11, ' ') + "------".PadRight(10, ' ') + "--------".PadRight(15, ' ') + "----".PadRight(8, ' ') + "-----".PadRight(15, ' ') + "-----".PadRight(15, ' ') + "\r\n";
						
						foreach (DataRow dr in dsHardToBorrow.Tables["HTB"].Rows)
						{
							body += dr["SecId"].ToString().PadRight(11, ' ') +
								dr["Symbol"].ToString().PadRight(10, ' ') +
								long.Parse(dr["Quantity"].ToString()).ToString("#,##0").PadLeft(15, ' ').Substring(0, 15) +
								decimal.Parse(dr["Rate"].ToString()).ToString("0.00").PadLeft(8, ' ').Substring(0, 8) +
								long.Parse(dr["Short"].ToString()).ToString("#,##0").PadLeft(15, ' ').Substring(0, 15) +
								long.Parse(dr["Fails"].ToString()).ToString("#,##0").PadLeft(15, ' ').Substring(0, 15)  + "\r\n";
						}
										

						string to = KeyValue.Get("AresHardToBorrowNotBilledMailTo", "mbattaini@penson.com", dbCnStr);							
						
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresHardToBorrowNotBilledMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresHardToBorrowNotBilledMailSubject", "Hard To Borrow Not Billed For ", dbCnStr) + Master.BizDate,
								body);						
						}
	
						KeyValue.Set("AresHardToBorrowNotBilledMailDate", Master.ContractsBizDate, dbCnStr);
					}
					catch (Exception error)
					{
						Log.Write(error.Message + "[AresMain.HardToBorrowNotBilledMail]", 1);
					}
					finally
					{
						if (localDbCn.State != ConnectionState.Closed)
						{
							localDbCn.Close();
						}
					}
				}				
			}
		}
		
		private void PremiumAutoSet()
		{
			SqlConnection localDbCn = null;

			if (Master.ContractsBizDate.Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				if (Master.ContractsBizDate.Equals(KeyValue.Get("AresPremiumAutoSetDate", "", dbCnStr)))
				{
					Log.Write("Premium Auto Set has already occured. [AresMain.PremiumAutoSet]", 1);					
				}
				else 	if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresPremiumAutoSetTime", "04:30", dbCnStr), "HH:mm", null))									
				{
					try
					{						
						localDbCn = new SqlConnection(dbCnStr);
						SqlCommand dbCmd = new SqlCommand("spPremiumAutoSet",localDbCn);
						dbCmd.CommandType = CommandType.StoredProcedure;
                        dbCmd.CommandTimeout = 9000;
						
						SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
						paramBizDate.Value = Master.BizDate;

						SqlParameter paramRecordCount = dbCmd.Parameters.Add("@RecordCount", SqlDbType.BigInt);
						paramRecordCount.Direction = ParameterDirection.Output;
							
						localDbCn.Open();
						dbCmd.ExecuteNonQuery();
												
						string emailBody = "Premium Auto Set " + long.Parse(paramRecordCount.Value.ToString()).ToString("#,###0") + " item(s).";
						
						string to = KeyValue.Get("AresPremiumAutoSetMailTo", "mbattaini@penson.com", dbCnStr);							
						
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresPremiumAutoSetMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresPremiumAutoSetMailSubject", "Premium Auto  ", dbCnStr)  + Master.BizDate,
								emailBody);						
						}
	
						KeyValue.Set("AresPremiumAutoSetDate", Master.ContractsBizDate, dbCnStr);
						Log.Write("PremiumAutoSet completed for " + Master.ContractsBizDate + "  [AresMain.PremiumAutoSet]", Log.Information, 1);
					}
					catch (Exception error)
					{
						Log.Write(error.Message + " [AresMain.PremiumAutoSet]", 1);
					}
					finally
					{
						if (localDbCn.State != ConnectionState.Closed)
						{
							localDbCn.Close();
						}
					}
				}
				else
				{
					Log.Write("Must wait until " + KeyValue.Get("AresPremiumAutoSetTime", "04:30", dbCnStr) + ". [AresMain.PremiumAutoSet]", 1);
				}
			}
		}

		private void HardToBorrowChargesCSV(string groupCode, bool isEmail, string remoteFilePath)
		{
			if (Master.ContractsBizDate.Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				if (Master.ContractsBizDate.Equals(KeyValue.Get("AresHtbChargesCSV_" + groupCode + "_Date", "", dbCnStr)))
				{
					Log.Write("HTB Charges CSV has been sent for " + groupCode + ". [AresMain.PremiumAutoSet]", 1);					
				}
				else 	if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresHtbChargesCSVTime", "04:30", dbCnStr), "HH:mm", null))									
				{
						
					StreamWriter sr = null;
				
					string fileName = "";
					string tempFileName = "";
					string fileContent = "";				
					
					int index = 0;

					string tempPath = KeyValue.Get("AresHtbChargesCSVFilePath", @"C:\", dbCnStr);

					try
					{
                        string sql = "";

                        if (groupCode.Equals(""))
                        {   
                            // Both Firm07 and Firm10 
                            sql = "Select groupcode,\r\n" +
                                "accountnumber,\r\n" +
                                "Sum(IsNull(modifiedcharge, 0)) As Charge\r\n" +
                                "From    dbo.tbShortSaleBillingSummary with (NOLOCK) \r\n" +
                                "Where   bizdate >= '" + Master.BizDatePrior + "'\r\n" +
                                (groupCode.Equals("") ? "" : "And GroupCode = '" + groupCode + "'\r\n") +
                                "Group by groupcode, accountnumber\r\n" +
                                "Union \r\n" +
                                "Select groupcode,\r\n" +
                                "accountnumber,\r\n" +
                                "Sum(IsNull(modifiedcharge, 0)) As Charge \r\n" +
                                "From    dbo.tbShortSaleBillingSummaryBPS with (NOLOCK) \r\n" +
                                "Where   bizdate >= '" + Master.BizDatePrior + "'\r\n" + 
                                (groupCode.Equals("") ? "" : "And GroupCode = '" + groupCode + "'\r\n") +
                                "Group by groupcode, accountnumber\r\n" +
                                "Order by groupcode, accountnumber\r\n";
                        }
                        else
                        {   // Firm10 only 
                            sql = "select groupcode,\r\n" +
                                "accountnumber,\r\n" +
                                "sum(isnull(modifiedcharge, 0)) As Charge\r\n" +
                                "from  dbo.tbshortsalebillingsummaryBPS with (NOLOCK) \r\n" +
                                "where  bizdate >= '" + Master.BizDatePrior + "'\r\n" +
                                (groupCode.Equals("") ? "" : "And GroupCode = '" + groupCode + "'\r\n") +
                                "Group by groupcode, accountnumber\r\n" +
                                "Order by groupcode, accountnumber\r\n";
                        }		
			
						DataSet dsTemp = new DataSet();

						SqlConnection localDbCn = new SqlConnection(dbCnStr);
						SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
						dbCmd.CommandType = CommandType.Text;
                        dbCmd.CommandTimeout = 900;
                        							
						SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
						dataAdapter.Fill(dsTemp, "Charges");				
						

						foreach(DataRow dr in dsTemp.Tables["Charges"].Rows)
						{
							fileContent += dr["GroupCode"].ToString() + "," + dr["AccountNumber"].ToString() + "," + dr["Charge"].ToString() + "\r\n";
						}
						
						
						tempFileName = @"\HHTB_ChargesCSV_" + groupCode + "_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyyMMdd") + ".csv";					
						fileName = tempPath + tempFileName;
						sr = File.CreateText(fileName);
						sr.Write(fileContent);
						sr.Close();

						if (isEmail)
						{																							
							string to = KeyValue.Get("AresHtbCharges_" + groupCode + "_To", "mbattaini@penson.com", dbCnStr);

							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresHtbCharges_" + groupCode + "_From", "sendero@penson.com", dbCnStr),
									"HTB Charges For " + groupCode.ToUpper() + " " + Master.ContractsBizDate,
									"",
									fileName);						
							}
						}
						else
						{
							Filer filer = new Filer();

							remoteFilePath = DatePartSet(remoteFilePath, Master.BizDate);

							filer.FileNoTempPut(remoteFilePath + tempFileName, "localHost", "", "", fileName);
						}
	
						KeyValue.Set("AresHtbChargesCSV_" + groupCode + "_Date", Master.ContractsBizDate, dbCnStr);
						Log.Write("HardToBorrowChargesCSV_" + groupCode + " completed for " + Master.ContractsBizDate + "   [AresMain.HardToBorrowChargesCSV]",Log.Information, 1);  
					}
					catch (Exception error)
					{
						Log.Write(error.Message + "  [AresMain.HardToBorrowChargesCSV]", 1);
					}
				}
				else
				{
					Log.Write("Must wait until " + KeyValue.Get("AresHtbChargesCSVTime", "04:30", dbCnStr) + ". [AresMain.HardToBorrowChargesCSV]", 1);
				}
			}
			else
			{
				Log.Write("Must wait until current buisness date. [AresMain.HardToBorrowChargesCSV]", 1);
			}
		}

		private void SchonfeldShortSaleNegativeRebateBillingFileDo()
		{
            if (KeyValue.Get("ShortSaleNegRebateBillingBPSSnapShotBizDate", "", dbCnStr).Equals(DateTime.Now.ToString(Standard.DateFormat)))   
			{
				if (!KeyValue.Get("AresSchonfeldShortSaleNegRebateFileDate", "", dbCnStr).Equals(DateTime.Now.ToString(Standard.DateFormat)))
				{

					StreamWriter sr = null;

					try
					{
						SqlConnection dbCn = null;

						SqlCommand dbCmd = null;
						SqlParameter paramStartDate = null;
						SqlParameter paramStopDate = null;
						SqlDataAdapter dataAdapter = null;

						DataSet dataSet = new DataSet();
						
						string groupCodeText = "";
						string filePath = "";
				
						groupCodeText = KeyValue.Get("AresSchonfeldGroupCodeList", @"LSPS;STRD;SCHO;OPUS;QTSL;FNYS;WHEA;TRIL;WHNE;TORB;MAXT;FIRS;", dbCnStr);
                        filePath = KeyValue.Get("AresSchonfeldFilePath", @"\\Rsdmz001\Reports\Firm10\Schonfeld\NEG_REBATES", dbCnStr);

						dataSet = new DataSet();

						dbCn = new SqlConnection(dbCnStr);
                        dbCmd = new SqlCommand("spShortSaleBillingBPSSummaryGet", dbCn);       
						dbCmd.CommandType = CommandType.StoredProcedure;
						dbCmd.CommandTimeout = 900;

						paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
						paramStartDate.Value = DateTime.Now.ToString(Standard.DateFormat);

						paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
						paramStopDate.Value = DateTime.Now.ToString(Standard.DateFormat);

						dataAdapter = new SqlDataAdapter(dbCmd);
						dataAdapter.Fill(dataSet, "BillingSummary");

						string page = "";

						page = "BizDate|GroupCode|Account|CUSIP|Symbol|QuantityCovered|Rate|Charge\r\n";

						foreach (DataRow dr in dataSet.Tables["BillingSummary"].Rows)
						{
							if (groupCodeText.IndexOf(dr["GroupCode"].ToString().Trim() + ";") > -1)
							{
								if (long.Parse(dr["QuantityCovered"].ToString()) > -1)
								{
									page += DateTime.Parse(dr["BizDate"].ToString()).ToString("yyyy-MM-dd") + "|" +
										dr["GroupCode"].ToString() + "|" +
										dr["AccountNumber"].ToString() + "|" +
										dr["SecId"].ToString() + "|" +
										dr["Symbol"].ToString() + "|" +
										dr["QuantityCovered"].ToString() + "|" +
										((!dr["MarkupRate"].ToString().Equals("")) ? decimal.Parse(dr["MarkupRate"].ToString()).ToString("0.000") : "") + "|" +
										decimal.Parse(dr["ModifiedCharge"].ToString()).ToString("#,##0.00") + "\r\n";
								}
							}
						}

						sr = File.CreateText(@filePath + @"\HardToBorrowExtract_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
						sr.Write(page);
						sr.Close();

                        email.Send("mbattaini@penson.com;dchen@penson.com", "stockloan@penson.com", "Schonfeld Neg Rebate File Uplaoded " + DateTime.Now.ToString(Standard.DateFormat), "\r\nFile Uploaded To: " + @filePath + @"\HardToBorrowExtract_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
						KeyValue.Set("AresSchonfeldShortSaleNegRebateFileDate", DateTime.Now.ToString(Standard.DateFormat), dbCnStr);
					}   
					catch (Exception error)
					{
						Log.Write(error.Message + " [AresMain.SchonfeldShortSaleNegativeRebateBillingFileDate]", 1);
					}
				}			
			}
		}

		private void StockBorrowUploadFile()
		{
			DataSet dsBoxSummary = new DataSet();		
			DataSet dsReportRecipients = new DataSet();
				
			StreamWriter sr = null;
				
			string fileName = "";
			string fileContent = "";													

			string tempPath = KeyValue.Get("AresStockBorrowUploadFilePath", @"C:\", dbCnStr);

			try
			{
				if (!KeyValue.Get("AresStockBorrowUploadFileDate", "", dbCnStr).Equals(Master.ContractsBizDate))
				{
					if (Master.BizDate.Equals(Master.ContractsBizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
					{
						if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresStockBorrowSetTime", "06:15", dbCnStr), "HH:mm", null))
						{
							SqlConnection dbCn = new SqlConnection(dbCnStr);

							SqlCommand dbCmd = new SqlCommand("dbo.spBoxSummaryGet", dbCn);
							dbCmd.CommandType = CommandType.StoredProcedure;
                            dbCmd.CommandTimeout = 900;

							SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
							paramBizDate.Value = Master.BizDate;

							SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
							dataAdapter.Fill(dsBoxSummary, "BoxSummary");

							foreach (DataRow dr in dsBoxSummary.Tables["BoxSummary"].Rows)
							{
								if (((dr["BookGroup"].ToString().Equals("0158")) &&     
									(dr["BaseType"].ToString().Equals("E")) &&
									(long.Parse(dr["Available"].ToString()) * double.Parse(dr["LastPrice"].ToString())) >= long.Parse(KeyValue.Get("AresStockBorrowUploadAmountMin", "1000000", dbCnStr)) &&
									(double.Parse(dr["LastPrice"].ToString()) >= double.Parse(KeyValue.Get("AresStockBorrowUploadPriceMin", "5.00", dbCnStr))) &&
									(bool.Parse(dr["IsNoLend"].ToString()) == false) &&
									(bool.Parse(dr["IsThreshold"].ToString()) == false)))
								{
									fileContent += dr["SecId"].ToString().Trim() + "," + dr["Available"].ToString() + ",,,\r\n";
								}
							}

							fileName = tempPath + @"\D" + DateTime.Parse(Master.ContractsBizDate).ToString("MMddyy") + "0234.txt";
							sr = File.CreateText(fileName);
							sr.Write(fileContent);
							sr.Close();

							Filer filer = new Filer(@"C:\temp.filer");
							filer.FileNoTempPut(@"/stkbw-penson-upload/581-1.txt", "ftp.americaneagle.com", "stkbw-penson-upload", @"d$R2sl1pp3rY", fileName);

							email.Send("mbattaini@apexclearing.com", "sendero@apexclearing.com", "Stock borrow file successfuly uploaded for " + Master.BizDate, "");
							KeyValue.Set("AresStockBorrowUploadFileDate", Master.ContractsBizDate, dbCnStr);
						}
					}
				}
			}
			catch (Exception error) 
			{
				Log.Write(error.Message + " [AresMain.StockBorrowUploadFile]", 1);
			}
		}
					
		private DataSet ReportRecipientsGet(string reportName)
		{
			DataSet dsReportRecipients = new DataSet();
			
			try
			{
				SqlCommand dbCmd = new SqlCommand("spReportValuesGet", new SqlConnection(dbCnStr));   
				dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 300;

				SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
				paramReportName.Value = reportName;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsReportRecipients, "ReportRecipients");		
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[AresMain.ReportRecipientsGet]", 1);
				throw;
			}
		
			return dsReportRecipients;
		}
		
		private void ReportRecipientSet(string reportName, string reportRecipientNumber, string dateTime)
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spReportValueSet", localDbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.Char, 50);
				paramReportName.Value = reportName;

				SqlParameter paramReportRecipientNumber = dbCmd.Parameters.Add("@ReportRecipientNumber", SqlDbType.BigInt);
				paramReportRecipientNumber.Value = reportRecipientNumber;

				SqlParameter paramLastDeliveredDate = dbCmd.Parameters.Add("@LastDeliveredDate", SqlDbType.DateTime);
				paramLastDeliveredDate.Value = dateTime;
				
				localDbCn.Open();
				dbCmd.ExecuteNonQuery();				
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[AresMain.ReportRecipientSet]", 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}
		}

		private DataSet AvailabilityFeedDataGet(string bookGroup, string minPrice, string minAvail, string minRate)
		{
			DataSet dsAvailability = new DataSet();
			try
			{
				string sql = "select	b.secid,\r\n" +
					"l.secidlink As Symbol,\r\n" +
					"b.netpositionsettled As Avail,\r\n" +
					"IsNull(IsNull((select avg(rate) from dbo.tbcontracts where bizdate = b.bizdate and bookgroup = b.bookgroup and contracttype = 'L' and secid = b.secid ), (select rate from tbinventoryratecontrol where bizdate = b.bizdate and secid = b.secid and rate < 5)), 0) As Rate\r\n" +
					"from		tbboxposition b,\r\n" +
					"			tbsecmaster s,\r\n" +
					"			tbsecidlinks l\r\n" +
					"where	b.bizdate = '" + Master.BizDate + "'\r\n" +
					"and		b.bookgroup = '" + bookGroup + "'\r\n" +
					"and		b.netpositionsettled <= b.netpositiontraded\r\n" +
					"and		b.netpositionsettled >= " + minAvail + "\r\n" +
					"and		s.secid = b.secid\r\n" +
					"and		s.lastprice >= " + minPrice +"\r\n" +
					"and		l.secid = s.secid\r\n" +
					"and		l.secidtypeindex = 2\r\n" +
					"and		s.basetype <> 'B'\r\n" +
					"Order By l.SecidLink";

				SqlCommand dbCmd = new SqlCommand(sql, new SqlConnection(dbCnStr));
				dbCmd.CommandType = CommandType.Text;
                dbCmd.CommandTimeout = 9000;
				
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsAvailability, "Avail");			
			
				foreach (DataRow dr in dsAvailability.Tables["Avail"].Rows)
				{
					if (decimal.Parse(dr["Rate"].ToString()) <= decimal.Parse(minRate))
					{
						dr.Delete();
					}
				}

				dsAvailability.AcceptChanges();
			}
			catch {}

			return dsAvailability;
		}
		
		private void AvailabilityFeed()
		{				
			if (Master.ContractsBizDate.Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresAvailFeedSetTime", "06:15", dbCnStr), "HH:mm", null))									
				{	
					try
					{
						DataSet dsReportRecipients = null;
						DataSet dsAvailability = null;
						StreamWriter sr = null;

						dsReportRecipients = ReportRecipientsGet("AVAIL_Feed");
			
						dsAvailability = AvailabilityFeedDataGet(
                            KeyValue.Get("AresAvailBookGroup", "0158", dbCnStr),    
							KeyValue.Get("AresAvailMinPrice", "3.00", dbCnStr),
							KeyValue.Get("AresAvailMinAvail", "5000", dbCnStr),
							KeyValue.Get("AresAvailMinRate", "-5", dbCnStr));
			
						string format = "";	
						string body = "";
        
						Email mail = new Email(dbCnStr);
						Filer filer = new Filer();

						string tempPath = KeyValue.Get("AresAvailFeedFilePath", @"C:\", dbCnStr);
						string fileName = "";								

						foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
						{				
							if (dr["LastDeliveredDate"].ToString().Equals(""))
							{
								dr["LastDeliveredDate"] = Master.BizDatePrior;
							}

							if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
							{							
								format = "";
								body = "";

								foreach(DataRow dr2 in dsAvailability.Tables["Avail"].Rows)
								{
									format = dr["format"].ToString();
					
									if (dr["Justify"].ToString().Trim().ToUpper().Equals("Y"))
									{
										format = format.Replace("%secid", dr2["SecId"].ToString().Trim());
										format = format.Replace("%symbol", dr2["Symbol"].ToString().Trim().PadRight(6, ' '));
										format = format.Replace("%quantity", dr2["Avail"].ToString().PadLeft(15, ' '));
										format = format.Replace("%rate", decimal.Parse(dr2["Rate"].ToString()).ToString("00.000").PadLeft(8, ' '));
									}
									else
									{
										format = format.Replace("%secid", dr2["SecId"].ToString().Trim());
										format = format.Replace("%symbol", dr2["Symbol"].ToString().Trim());
										format = format.Replace("%quantity", dr2["Avail"].ToString());
										format = format.Replace("%rate", dr2["Rate"].ToString());
									}
					
									Log.Write(format, 1);
									body += format + "\r\n";

								} // foreach dr2 Loop 
	
								fileName = tempPath + @"\AVAIL_Feed_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyyMMdd") + "_" + dr["ReportRecipientNumber"].ToString() +".csv";
								
								sr = File.CreateText(fileName);
								sr.Write(body);
								sr.Close();

								if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
								{
									mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "AVAIL_ " + Master.ContractsBizDate, body, fileName);
								}
								else
								{																			
									mail.Send("mbattaini@penson.com", "sendero@penson.com", "AVAIL Feed", "AVAIL Feed uploaded to " + @dr["ReportRecipient"].ToString(), fileName);						
									filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\AVAIL_Feed_" + DateTime.Now.ToString("yyyyMMdd") + ".csv", "localHost", "", "", fileName);
								}
				
								ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.BizDate);
							}
						}	
					}				
					catch (Exception error)
					{
						Log.Write(error.Message, 1);
					}
				}
			}
		}	

		private void HardToBorrowRatesFile()
		{
			DataSet dsHardToBorrow = new DataSet();		
			DataSet dsReportRecipients = new DataSet();
				
			StreamWriter sr = null;
				
			string fileName = "";
			string fileContent = "";				
					
			int index = 0;
			bool errFlag = false;

			string tempPath = KeyValue.Get("AresHardToBorrowRatesFilePath", @"C:\", dbCnStr);

			if (!KeyValue.Get("AresHardToBorrowRatesMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				if (KeyValue.Get("AztecFileLoadDate", "", dbCnStr).Equals(Master.ContractsBizDate))
				{
					try
					{
						SqlConnection dbCn = new SqlConnection(dbCnStr);

						SqlCommand dbCmd = new SqlCommand("dbo.spBorrowHardListRatesGet", dbCn);
						dbCmd.CommandType = CommandType.StoredProcedure;
                        dbCmd.CommandTimeout = 900;

						SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
						paramBizDate.Value = Master.ContractsBizDate;

						SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
						dataAdapter.Fill(dsHardToBorrow, "HardToBorrow");    

						foreach (DataRow dr in dsHardToBorrow.Tables["HardToBorrow"].Rows)
						{
							fileContent += dr["SecId"].ToString() + "," + dr["Symbol"].ToString() + "," + decimal.Parse(dr["Rate"].ToString()).ToString("0.000") + "\r\n";
						}

						fileName = tempPath + @"\HardToBorrowRates_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyyMMdd") + ".csv";
						sr = File.CreateText(fileName);
						sr.Write(fileContent);
						sr.Close();

						dbCmd.Parameters.Remove(paramBizDate);

						dsReportRecipients = ReportRecipientsGet("HTB_Rates");			// Using Matt's new ReportRecipient dataset function 

						Email mail = new Email(dbCnStr);
						Filer filer = new Filer();

						foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows) 
						{
							try
							{
								if (dr["LastDeliveredDate"].ToString().Equals(""))
								{
									dr["LastDeliveredDate"] = Master.BizDatePrior;
								}

								if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
								{

									if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
									{
										mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "Hard To Borrow File", "", fileName);
									}
									else
									{
										filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\HTB_Rates_" + DateTime.Now.ToString("yyyyMMdd") + ".csv", "localHost", "", "", fileName);
										mail.Send("mbattaini@penson.com", "sendero@penson.com", "Hard To Borrow File Update", "HTB Rates uploaded to " + @dr["ReportRecipient"].ToString(), fileName);
									}

									ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.ContractsBizDate);
									index++;
								}
							}
							catch (Exception error)
							{
								errFlag = true;
								Log.Write("ERROR (foreach loop): " + error.Message.Replace("\r\n", "") + "  [AresMain.HardToBorrowRatesFile]", Log.Error, 1);
							}
						}

						if (!errFlag)
						{
							Log.Write("Daily HTB Rates file mailed, for BizDate: " + Master.BizDate + ". File: " + fileName + ", Recipient Count = " + index.ToString() + ".  [AresMain.HardToBorrowRatesFile]", 2);
							KeyValue.Set("AresHardToBorrowRatesMailDate", Master.ContractsBizDate, dbCnStr);
						}
					}
					catch (Exception error)
					{
						Log.Write("ERROR: " + error.Message + ".  [AresMain.HardToBorrowRatesFile]", Log.Error, 1);
						return;
					}

				}				
			}
		}

        private string EasyBorrowRatesFormat(DataSet dsEasyBorrow, string format, string justify, string fileName)
        {
            StreamWriter sr = null;
            
            tempPath = Standard.ConfigValue("TempPath", @"C:\Sendero\Temp\");	

            string fileContent = "";
            string format_ = "";
            
            foreach (DataRow dr in dsEasyBorrow.Tables["EasyBorrow"].Rows)
            {
                format_ = format;

                if (justify.Trim().ToUpper().Equals("Y"))
                {
                    format_ = format_.Replace("%secid", dr["CUSIP"].ToString().Trim());
                    format_ = format_.Replace("%symbol", dr["Symbol"].ToString().Trim().PadRight(6, ' '));
                    format_ = format_.Replace("%rate", decimal.Parse(dr["LowestRate"].ToString()).ToString("00.000").PadLeft(8, ' '));

                    fileContent += format_ + "\r\n";
                }
                else
                {
                    fileContent += dr["CUSIP"].ToString() + "," + dr["Symbol"].ToString() + "," + decimal.Parse(dr["LowestRate"].ToString()).ToString("0.000") + "\r\n";
                }                
            }

            sr = File.CreateText(tempPath + fileName);
            sr.Write(fileContent);
            sr.Close();

            return fileContent;
        }
        
        private void EasyBorrowRatesMail(string reportName, string fileName, string processName)
        {
            DataSet dsEasyBorrow = new DataSet();
            DataSet dsReportRecipients = new DataSet();
            
            string fileContent = "";
            string fileDisclaimer = "";

            string filerHost = "";      
            string filerFilePath = "";  
            string filerUserId = "";    
            string filerPassword = "";  

            int index = 0;
            bool errFlag = false;

            tempPath = Standard.ConfigValue("TempPath", @"C:\Sendero\Temp\");	

            try
            {
                if (!KeyValue.Get("AresEasyBorrowRatesMailDate" + reportName, "", dbCnStr).Equals(Master.ContractsBizDate))
                {
                    if (KeyValue.Get("EasyBorrowFileDate", "", dbCnStr).Equals(Master.ContractsBizDate))
                    {
                        SqlConnection dbCn = new SqlConnection(dbCnStr);

                        SqlCommand dbCmd = new SqlCommand("dbo.spBorrowEasyListRatesGet", dbCn);
                        dbCmd.CommandType = CommandType.StoredProcedure;
                        dbCmd.CommandTimeout = 900;

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                        dataAdapter.Fill(dsEasyBorrow, "EasyBorrow");
                        

                        dsReportRecipients = ReportRecipientsGet(reportName);			

                        Email mail = new Email(dbCnStr);
                        Filer filer = new Filer();
                       
                        fileDisclaimer = "\r\n \r\n \r\n" + Standard.ConfigValue("ETBRatesEmailDisclaimer");
                        fileName = fileName + DateTime.Parse(Master.ContractsBizDate).ToString("yyyyMMdd") + ".csv";

                        foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
                        {
                            try
                            {
                                if (dr["LastDeliveredDate"].ToString().Equals(""))
                                {
                                    dr["LastDeliveredDate"] = Master.BizDatePrior;
                                }

                                if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
                                {
                                    fileContent = EasyBorrowRatesFormat(dsEasyBorrow, dr["Format"].ToString(), dr["Justify"].ToString(), fileName);                                    
                                    
                                    if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
                                    {   
                                        mail.Send(dr["ReportRecipient"].ToString(), "sendero@apexclearing.com", processName + " For " + Master.ContractsBizDate, fileContent + fileDisclaimer, tempPath + fileName);
                                    }
                                    else if (dr["ReportRecipient"].ToString().Substring(0, 3).Equals("ftp"))    
                                    {   
                                        filerHost = dr["ReportRecipient"].ToString().Substring(0, dr["ReportRecipient"].ToString().IndexOf("#"));
                                        filerHost = (filerHost.IndexOf("/") > 0) ? filerHost.Remove(filerHost.IndexOf("/")) : filerHost; 
                                        filerFilePath = dr["ReportRecipient"].ToString().Substring(dr["ReportRecipient"].ToString().IndexOf("/") + 1, dr["ReportRecipient"].ToString().IndexOf("#") - dr["ReportRecipient"].ToString().IndexOf("/") - 1);  
                                        filerFilePath += "/";
                                        filerUserId = dr["ReportRecipient"].ToString().Substring(dr["ReportRecipient"].ToString().IndexOf("#") + 1, dr["ReportRecipient"].ToString().IndexOf(":") - dr["ReportRecipient"].ToString().IndexOf("#") - 1);
                                        filerPassword = dr["ReportRecipient"].ToString().Substring(dr["ReportRecipient"].ToString().IndexOf(":") + 1);

                                        filer.FilePut(filerFilePath + fileName, filerHost, filerUserId, filerPassword, tempPath + fileName);
                                        mail.Send("mbattaini@apexclearing.com", "sendero@apexclearing.com", processName + " File Update", "ETB Rates file uploaded to " + @dr["ReportRecipient"].ToString() + fileDisclaimer, tempPath + fileName);  
                                    }
                                    else
                                    {
                                        string remoteFilePath = DatePartSet(dr["ReportRecipient"].ToString(), Master.BizDate);  //2012-03-06 Allow {yyyyMMdd} in folder path
                                        if (!Directory.Exists(remoteFilePath)) { Directory.CreateDirectory(remoteFilePath); }  
                                        filer.FileNoTempPut(remoteFilePath + @"\" + fileName, "localHost", "", "", tempPath + fileName);
                                        mail.Send("mbattaini@apexclearing.com", "sendero@apexclearing.com", "Easy To Borrow Rate File Update", "ETB Rates file uploaded to " + remoteFilePath + fileDisclaimer, tempPath + fileName);
                                        Log.Write("ETB Rates file uploaded to: " + dr["ReportRecipient"].ToString() + "   [AresMain.EasyBorrowRatesMail]", 2);
                                    }

                                    ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.ContractsBizDate);
                                    index++;
                                }
                            }
                            catch (Exception error)
                            {
                                errFlag = true;                                
                                Log.Write("ERROR (foreach loop): " + error.Message.Replace("\r\n", "") + ".  [AresMain.EasyBorrowRatesMail : "+ processName + "]", Log.Error, 1);
                            }
                        }

                        if (!errFlag)
                        {                             
                            KeyValue.Set("AresEasyBorrowRatesMailDate" + reportName, Master.ContractsBizDate, dbCnStr);
                            Log.Write("Daily " + processName + " file mailed, for BizDate: " + Master.BizDate + ". File: " + fileName + ", Recipient Count = " + index.ToString() + ".   [AresMain.EasyBorrowRatesMail : " + processName + "]", 2);
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Log.Write("ERROR: " + error.Message + ".   [AresMain.EasyBorrowRatesMail : "+ processName + "]", Log.Error, 1);
                return;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }

        }


        private void EasyBorrowRatesFile()
        {
            DataSet dsEasyBorrow = new DataSet();
            DataSet dsReportRecipients = new DataSet();

            StreamWriter sr = null;

            string fileName = "";
            string fileContent = "";
            string fileDisclaimer = "";

            string filerHost = "";
            string filerFilePath = "";
            string filerUserId = "";
            string filerPassword = "";

            int index = 0;
            bool errFlag = false;

            tempPath = Standard.ConfigValue("TempPath", @"C:\Sendero\Temp\");	

            try
            {
                if (!KeyValue.Get("AresEasyBorrowRatesFileDate", "", dbCnStr).Equals(Master.ContractsBizDate))
                {
                    if (KeyValue.Get("EasyBorrowFileDate", "", dbCnStr).Equals(Master.ContractsBizDate))
                    {
                        SqlConnection dbCn = new SqlConnection(dbCnStr);

                        SqlCommand dbCmd = new SqlCommand("dbo.spBorrowEasyListRatesGet", dbCn);
                        dbCmd.CommandType = CommandType.StoredProcedure;
                        dbCmd.CommandTimeout = 900;

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                        dataAdapter.Fill(dsEasyBorrow, "EasyBorrow");

                        foreach (DataRow dr in dsEasyBorrow.Tables["EasyBorrow"].Rows)
                        {
                            fileContent += dr["CUSIP"].ToString() + "," + dr["Symbol"].ToString() + "," + decimal.Parse(dr["LowestRate"].ToString()).ToString("0.000") + "\r\n";
                        }

                       
                        fileName = "ebr_XXXX_" + DateTime.Parse(Master.ContractsBizDate).ToString("MMddyy") + ".dat";
                        sr = File.CreateText(tempPath + fileName);
                        sr.Write(fileContent);
                        sr.Close();

                        dsReportRecipients = ReportRecipientsGet("ETB_RatesB");				// Using Matt's new ReportRecipient dataset function 

                        Email mail = new Email(dbCnStr);
                        Filer filer = new Filer();
                        fileDisclaimer = "\r\n \r\n \r\n" + Standard.ConfigValue("ETBRatesEmailDisclaimer");

                        foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
                        {
                            try
                            {

                                if (dr["LastDeliveredDate"].ToString().Equals(""))
                                {
                                    dr["LastDeliveredDate"] = Master.BizDatePrior;
                                }

                                if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
                                {
                                    if (dr["ReportRecipient"].ToString().Substring(0, 3).Equals("ftp"))
                                    {
                                        filerHost = dr["ReportRecipient"].ToString().Substring(0, dr["ReportRecipient"].ToString().IndexOf("#"));
                                        filerFilePath = "/";                                  
                                        filerUserId = dr["ReportRecipient"].ToString().Substring(dr["ReportRecipient"].ToString().IndexOf("#") + 1, dr["ReportRecipient"].ToString().IndexOf(":") - dr["ReportRecipient"].ToString().IndexOf("#") - 1);
                                        filerPassword = dr["ReportRecipient"].ToString().Substring(dr["ReportRecipient"].ToString().IndexOf(":") + 1);

                                        filer.FilePut(filerFilePath + fileName, filerHost, filerUserId, filerPassword, tempPath + fileName);
                                        mail.Send("mbattaini@apexclearing.com", "sendero@apexclearing.com", "Easy To Borrow Rate File Update", "ETB Rates file uploaded to " + @dr["ReportRecipient"].ToString() + fileDisclaimer, tempPath + fileName);
                                    }
                                    
                                    ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.ContractsBizDate);
                                    index++;
                                }
                            }
                            catch (Exception error)
                            {
                                errFlag = true;
                                Log.Write("ERROR (foreach loop): " + error.Message.Replace("\r\n", "") + ".  [AresMain.EasyBorrowRatesMail]", Log.Error, 1);
                            }
                        }

                        if (!errFlag)
                        {
                            // Easy Borrow Rates file mailing/ftp-uploading completed 
                            KeyValue.Set("AresEasyBorrowRatesFileDate", Master.ContractsBizDate, dbCnStr);
                            Log.Write("Daily Easy Borrow Rates file mailed, for BizDate: " + Master.BizDate + ". File: " + fileName + ", Recipient Count = " + index.ToString() + ".  [AresMain.EasyBorrowRatesMail]", 2);
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Log.Write("ERROR: " + error.Message + ".  [AresMain.EasyBorrowRatesMail]", Log.Error, 1);
                return;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }

        }

		private void FMRAvailabilityFile()
		{
			DataSet dsAvail = new DataSet();

			StreamWriter sr = null;
			string fileContent = "";
			string fileMakeTime = null;

			string filePath = "";
			string remotePathName = "";
			string hostName = "";
			string userName = "";
			string passWord = "";
			int rowCount = 0;

			try
			{
				tempPath = Standard.ConfigValue("TempPath", @"C:\Sendero\Temp\");
				hostName = Standard.ConfigValue("FMRFtpHost", @"localhost");
				remotePathName = Standard.ConfigValue("FMRFtpPath", tempPath);
				userName = Standard.ConfigValue("FMRFtpUserId", "");
				passWord = Standard.ConfigValue("FMRFtpPassword", "");

				if (!Master.ContractsBizDate.Equals(Master.BizDate) || !Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
				{		
					return;
				}

				if (KeyValue.Get("AresFMRAvailabilityFeedBizDate", "", dbCnStr).Equals(Master.ContractsBizDate))
				{
					Log.Write("FMR Availablity Feed file upload has already occured, for BizDate: " + Master.ContractsBizDate + ".  [AresMain.FMRAvailbilityFile]", 2);
					return;
				}

				fileMakeTime = KeyValue.Get("AresFMRAvailabilityFeedSetTime", "11:00", dbCnStr);    
				if (fileMakeTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) > 0 )
				{
					// Must wait till 6:00 AM 
					return;
				}				

				SqlConnection dbCn = new SqlConnection(dbCnStr);
				SqlCommand dbCmd = new SqlCommand("dbo.spBoxSummaryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 900;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDate;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsAvail, "Availability");

				rowCount = dsAvail.Tables["Availability"].Rows.Count;

				Email mail = new Email(dbCnStr);
				Filer filer = new Filer();
				AvailabilityFileFormat fileAvail = new AvailabilityFileFormat();

                //Note: Only FileName kept "0234", data content has already changed to "0158", see fileAvail.Create(,) function for detail.
				filePath = tempPath + @"\invax0234_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyyMMdd") + ".dat";
				fileContent = fileAvail.Create(Master.BizDate, dsAvail); 
                
				sr = File.CreateText(filePath);
				sr.Write(fileContent);
				sr.Close();

				filer.FileNoTempPut(remotePathName, hostName, userName, passWord, filePath);
                mail.Send("sendero@apexclearing.com", "sendero@apexclearing.com", "FMR Availability Feed File " + Master.BizDate, "FMR Availablility Feed File: \t" + filePath + "\r\nUploaded to: \t" + hostName + "\r\nUpload Path: \t" + remotePathName, filePath);
				KeyValue.Set("AresFMRAvailabilityFeedBizDate", Master.ContractsBizDate, dbCnStr);
				Log.Write("FMR Availability Feed file uploaded to: " + hostName + ", for BizDate: " + Master.BizDate + "   [AresMain.FMRAvailabilityFile]", 2);

			}
			catch (Exception error)
			{
				Log.Write("ERROR: " + error.Message + ".  [AresMain.FMRAvailabilityFile]", Log.Error, 1);
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}

		}

		private void CNSFailsMail()
		{		
			SqlConnection wwDbCn = null;
			SqlDataAdapter wwDataAdapter = null;
			DataSet dsCNSFails = new DataSet();		
			DataSet dsReportRecipients = new DataSet();
			
			string sql = "";
			string filePath = "";
			string fileMakeTime = null; 
			string worldwideProcessDateStr = "";
			string ShortSellFinalProcessDateStr = "";

			int rptCount = 0;
			int rowCount = 0;
			bool errFlag = false;

			if (!Master.ContractsBizDate.Equals(Master.BizDate) || !Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{				
				return;
			}

			if (KeyValue.Get("AresCNSFailsMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				Log.Write("Daily CNS_Fails file email has already occured, for BizDate: " + Master.BizDate + ".  [AresMain.CNSFailsMail]", 2);
				return;
			}

			fileMakeTime = KeyValue.Get("AresCNSFailsMakeTime", "11:05", dbCnStr);
			if (fileMakeTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) > 0 )
			{
				return;
			}				

			try
			{
				// Verify Prerequisite: WorldWide ProcessDates must equal to Sendero BizDatePrior

				wwDbCn = new SqlConnection(worldwideDbCnStr);

				sql = "Use WorldWide \r\n" +
					"Declare @BizDatePrior datetime \r\n" +
					"Set     @BizDatePrior = '" + Master.BizDatePrior + "' \r\n" +
					"Select  Top 1 CPD.WorldWideProcessDate, SSF.ProcessDate As ShortSellFinalProcessDate \r\n" +
					"From    dbo.CurrentProcessDate CPD With (NOLOCK) \r\n" +
					"Inner   Join dbo.SecShortSellFinal  SSF With (NOLOCK) \r\n" +
					"On      CPD.ProcessDate = SSF.ProcessDate \r\n" +
					"Where   CPD.Firm = '07' \r\n" +
					"And     CPD.ProcessDate = CPD.WorldWideProcessDate \r\n" +
					"And     CPD.ProcessDate = IsNull(@BizDatePrior, CPD.ProcessDate) \r\n" ;

				SqlCommand wwDbCmd = new SqlCommand(sql, wwDbCn);
				wwDbCmd.CommandType = CommandType.Text;
                wwDbCmd.CommandTimeout = 900;

				wwDataAdapter = new SqlDataAdapter(wwDbCmd);
				wwDataAdapter.Fill(dsCNSFails, "ProcessDates");

				if ( dsCNSFails.Tables["ProcessDates"].Rows.Count == 0)
				{
					Log.Write("ERROR: No ProcessDate data returned from [WorldWide].[CurrentProcessDate] table.  [AresMain.CNSFailsMail]", 2);
					return;
				}

				foreach (DataRow dr in dsCNSFails.Tables["ProcessDates"].Rows)
				{
					worldwideProcessDateStr = DateTime.Parse(dr["WorldWideProcessDate"].ToString()).ToString("yyyy-MM-dd");   
					ShortSellFinalProcessDateStr = DateTime.Parse(dr["ShortSellFinalProcessDate"].ToString()).ToString("yyyy-MM-dd"); 
				}	

				if ((worldwideProcessDateStr == ShortSellFinalProcessDateStr) && 
					(ShortSellFinalProcessDateStr == DateTime.Parse( Master.BizDatePrior).ToString("yyyy-MM-dd")) )
				{		
					
					//------ Prerequisite Dates are good, now get the report dataset ------------------ 
					sql = "Use WorldWide \r\n" +
						"Select S.Cusip, \r\n" +
						"       B.Symbol, \r\n" +
						"       Convert(bigint, Sum(S.OriginalSECShortSellQuantity)) As SUM_OriginalSECShortSellQuantity \r\n" +
						"From  	dbo.SecShortSellFinal S With (Nolock) \r\n" +
						"Inner  Join dbo.SecurityBase B With (Nolock) \r\n" +
						"On     S.Firm = B.Firm \r\n" +
						"And    S.Cusip = B.Cusip  \r\n" +
						"Where	S.Firm = '10' \r\n" +                                   
						"And    S.ProcessDate = '" + Master.BizDatePrior + "' \r\n" +
						"Group By S.Cusip, B.Symbol \r\n" +
						"Order By B.Symbol \r\n" ;

					wwDbCmd.CommandText = sql;
					wwDataAdapter.Fill(dsCNSFails,"CNSFails");

					rowCount = dsCNSFails.Tables["CNSFails"].Rows.Count;

                    dsReportRecipients = ReportRecipientsGet("CNS_Fails");
                    										
					Email mail = new Email(dbCnStr);
					Filer filer = new Filer();

                    filePath = Excel.ExportDataSetToExcel(ref dsCNSFails, "CNSFails", "CNS_Fails", false);

                    foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
                    {
                        try
                        {
                            if (dr["LastDeliveredDate"].ToString().Equals(""))
                            {
                                dr["LastDeliveredDate"] = Master.BizDatePrior;
                            }

                            if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
                            {
								if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
								{
									mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "CNS Fails " + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd"), "For ProcessDate = " + Master.BizDatePrior, filePath);
									Log.Write("CNS Fails email sent to: " + dr["ReportRecipient"].ToString() + "  [AresMain.CNSFailsMail]", 1);
								}
								else
								{	
									filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\CNS_FAILS_" + DateTime.Now.ToString("yyyyMMdd") + ".xls", "localHost", "", "", filePath);
									Log.Write("CNS Fails file uploaded to: " + dr["ReportRecipient"].ToString() + "  [AresMain.CNSFailsMail]", 1);
								}

                                ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.ContractsBizDate);
                                rptCount++;
                            }
                        }
                        catch (Exception error)
                        {
							errFlag = true;
							Log.Write("ERROR (foreach loop): " + error.Message.Replace("\r\n", "") + ".  [AresMain.CNSFailsMail]", Log.Error, 1);
                        }
                    }

					if (!errFlag)
					{
						KeyValue.Set("AresCNSFailsMailDate", Master.BizDate, dbCnStr);
						Log.Write("Daily CNS_Fails file mailed, for BizDate: " + Master.BizDate + ". Data RowCount = " + rowCount.ToString() + ", Recipient Count = " + rptCount.ToString() + ". [AresMain.CNSFailsMail]", 2);
					}
				} 
				else 							
				{						
					Log.Write("ERROR: CNS Fails PrecessDates does Not match: BizDatePrior(" + Master.BizDatePrior + "), WorldWide ProcessDate(" + worldwideProcessDateStr + "), ShortSellFinal ProcessDate(" + ShortSellFinalProcessDateStr +").  [AresMain.CNSFailsMail]", 2);
					return; 
				}
			}
			catch (Exception error)
			{
				Log.Write("ERROR: " + error.Message + ".  [AresMain.CNSFailsMail]", Log.Error, 1);
				return;
			}
			finally
			{
				if (wwDbCn.State != ConnectionState.Closed)
				{
					wwDbCn.Close();
				}
			}

		}

		private void LocatesStatisticMail()
		{
			DataSet dsLocatesStatistic = new DataSet();		
			DataSet dsReportRecipients = new DataSet();

			string sql = "";
			string emailBody = "";
			string totalLocates = "";
			string totalETB = "";
			int rowCount = 0;
			bool errFlag = false;

			if (!Master.ContractsBizDate.Equals(Master.BizDate) || !Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{		
				return;
			}

			if (KeyValue.Get("AresLocatesStatisticMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				Log.Write("Daily Short Sale Locates statistic email has already been sent, for BizDate: " + Master.BizDate + ".  [AresMain.LocatesStatisticMail]", 2);
				return;
			}

			if (KeyValue.Get("AresLocatesStatisticMakeTime", "12:55", dbCnStr).CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) > 0)    // 12:55 UTC = 7:55AM CST
			{
				return;
			}

			try
			{
				dsReportRecipients = ReportRecipientsGet("Locates_Statistic");		

				//------ Get the Bulk Locates dataset 
				SqlConnection dbCn = new SqlConnection(dbCnStr);

				sql = "Use Sendero \r\n" +
					  "Select SSL.GroupCode, Count(*) As [Total_Locates],  \r\n" +
					  "  (Select Count(*) \r\n" +
					  "   From dbo.tbShortSaleLocates_1 With (NOLOCK)  \r\n" +
					  "   Where GroupCode = SSL.GroupCode \r\n" +
					  "   And   TradeDate = SSL.TradeDate \r\n" +
					  "   And   Source = 'EB' \r\n" +
					  "  ) As ETB_Count  \r\n" +
					  "From   dbo.tbShortSaleLocates_1 SSL With (NOLOCK)  \r\n" +
					  "Where  SSL.TradeDate = '" + Master.BizDate + "' \r\n" +
					  "Group By SSL.TradeDate, SSL.GroupCode Order By SSL.GroupCode";

				SqlCommand dbCmd = new SqlCommand(sql, dbCn);
				dbCmd.CommandType = CommandType.Text;
                dbCmd.CommandTimeout = 900;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsLocatesStatistic, "ShortSaleLocates");

				rowCount = dsLocatesStatistic.Tables["ShortSaleLocates"].Rows.Count;
				emailBody = "Short Sale Locates statistic for " + Master.BizDate + "\r\n \r\n" +
							"GroupCode \tTotal_Locates \t\tETB_Count \r\n";

				foreach (DataRow dr in dsLocatesStatistic.Tables["ShortSaleLocates"].Rows)
				{
					totalLocates = int.Parse(dr["Total_Locates"].ToString().Trim()).ToString("#,##0");
					totalETB = int.Parse(dr["ETB_Count"].ToString().Trim()).ToString("#,##0");
					emailBody += dr["GroupCode"].ToString().Trim() + "\t\t" + 
								 totalLocates.PadLeft(25 - totalLocates.Length, ' ') + "\t\t" +
								 totalETB.PadLeft(18 - totalETB.Length, ' ') + "\r\n";
				}

				Email mail = new Email(dbCnStr);

				foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
				{
					try
					{
						if (dr["LastDeliveredDate"].ToString().Equals(""))
						{
							dr["LastDeliveredDate"] = Master.BizDatePrior;
						}

						if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
						{
							if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
							{
								mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "Short Sale Locates statistic " + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd"), emailBody);
								Log.Write("Short Sale Locates statistic email sent to: " + dr["ReportRecipient"].ToString() + "  [AresMain.LocatesStatisticMail]", 2);
							}
							else
							{	// Email only, no file generated and no need to post to ftp site or copy file remote folder
								//filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\Bulk_Locates_" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "localHost", "", "", filePath);
								//Log.Write("Short Sale Locates statistic file uploaded to: " + dr["ReportRecipient"].ToString() + "   [AresMain.LocatesStatisticMail]", 2);
							}

							ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.ContractsBizDate);
						}
					}
					catch (Exception error)
					{
						errFlag = true;
						Log.Write("ERROR (foreach loop): " + error.Message.Replace("\r\n", "") + ".  [AresMain.LocatesStatisticMail]", Log.Error, 1);
					}
				}

				if (!errFlag)
				{
					Log.Write("Daily Short Sale Locates statistic mailed, for BizDate: " + Master.BizDate + ".  Correspondent (GroupCode) Count = " + rowCount.ToString() + ".  [AresMain.LocatesStatisticMail]", 2);
					KeyValue.Set("AresLocatesStatisticMailDate", Master.BizDate, dbCnStr);
				}
			}
			catch (Exception error)
			{
				Log.Write("ERROR: " + error.Message + ".  [AresMain.LocatesStatisticMail]", Log.Error, 1);
				return;
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}

		private void HTBAccountsBPSMail()
		{	
			DataSet dsHTBAccountsBPS = new DataSet();			
			DataSet dsReportRecipients = new DataSet();

			string sql = "";
			string filePath = "";
			string fileMakeTime = null;

			int rptCount = 0;
			int rowCount = 0;
			bool errFlag = false;

			if (!Master.ContractsBizDate.Equals(Master.BizDate) || !Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				//Not ready for current BizDate
				return;
			}

			if (!KeyValue.Get("ShortSaleNegRebateBillingBPSSnapShotBizDate", "", dbCnStr).Equals(Master.BizDate))
			{
				Log.Write("BroadRidge HTB_Accounts file must wait for [ShortSaleNegRebateBillingBPSSnapShotBizDate] to occured for BizDate: " + Master.BizDate + ".  [AresMain.HTBAccountsBPSMail]", 2);
				return;
			}

			if (KeyValue.Get("AresHTBAccountsBPSMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				Log.Write("BroadRidge HTB_Accounts file email has already occured, for BizDate: " + Master.BizDate + ".  [AresMain.HTBAccountsBPSMail]", 2);
				return;
			}

			fileMakeTime = KeyValue.Get("AresHTBAccountsBPSMakeTime", "11:05", dbCnStr);
			if (fileMakeTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) > 0)
			{
				return;
			}

			try
			{
				dsReportRecipients = ReportRecipientsGet("HTB_Accounts_BPS");		// BPS

				SqlConnection dbCn = new SqlConnection(dbCnStr);

				sql = "Select SBS.GroupCode, \r\n" +
							 "SBS.AccountNumber, \r\n" +
							 "SBS.SecId, \r\n" +
							 "SIL.SecIdLink As Symbol, \r\n" +
							 "SBS.QuantityShorted, \r\n" +
							 "SBS.QuantityCovered \r\n" +
							 "From   dbo.tbShortSaleBillingSummaryBPS SBS With (NOLOCK) \r\n" +
							 "       Left Outer Join dbo.tbSecIdLinks SIL With (NOLOCK) \r\n" +
							 "       On  SBS.SecId = SIL.SecId  \r\n" +
							 "       And SIL.SecIdTypeIndex = 2 \r\n" +
							 "Where  SBS.BizDate  = '" + Master.BizDate + "' \r\n" +
							 "Order By SBS.GroupCode, SBS.AccountNumber, SBS.SecId";

				SqlCommand dbCmd = new SqlCommand(sql, dbCn);
				dbCmd.CommandType = CommandType.Text;
                dbCmd.CommandTimeout = 900;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsHTBAccountsBPS, "HardToBorrowAccounts");

				rowCount = dsHTBAccountsBPS.Tables["HardToBorrowAccounts"].Rows.Count;

				Email mail = new Email(dbCnStr);
				Filer filer = new Filer();

				// Matt's new Excel routine... using Dataset to generate the Excel file
				filePath = Excel.ExportDataSetToExcel(ref dsHTBAccountsBPS, "HardToBorrowAccounts", "HTB_Accounts_BPS", true);

				foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
				{
					try
					{
						if (dr["LastDeliveredDate"].ToString().Equals(""))
						{
							dr["LastDeliveredDate"] = Master.BizDatePrior;
						}

						if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
						{
							if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
							{
								mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "BroadRidge HTB Accounts " + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd"), "For ProcessDate = " + Master.BizDatePrior, filePath);
								Log.Write("BroadRidge HTB Accounts email sent to: " + dr["ReportRecipient"].ToString() + "  [AresMain.HTBAccountsBPSMail]", 1);
							}
							else
							{	
								filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\HTB_ACCOUNTS_BPS_" + DateTime.Now.ToString("yyyyMMdd") + ".xls", "localHost", "", "", filePath);
								Log.Write("BroadRidge HTB Accounts file uploaded to: " + dr["ReportRecipient"].ToString() + "   [AresMain.HTBAccountsBPSMail]", 2);
							}

							ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.ContractsBizDate);
							rptCount++;
						}
					}
					catch (Exception error)
					{
						errFlag = true;
						Log.Write("ERROR (foreach loop): " + error.Message.Replace("\r\n", "") + ".  [AresMain.HTBAccountsBPSMail]", Log.Error, 1);
					}
				}

				if (!errFlag)
				{
					KeyValue.Set("AresHTBAccountsBPSMailDate", Master.BizDate, dbCnStr);
					Log.Write("BroadRidge HTB_Accounts file mailed, for BizDate: " + Master.BizDate + ". File: " + filePath + ", Data RowCount = " + rowCount.ToString() + ", Recipient Count = " + rptCount.ToString() + ".  [AresMain.HTBAccountsBPSMail]", 2);
				}
			}
			catch (Exception error)
			{
				Log.Write("ERROR: " + error.Message + ".  [AresMain.HTBAccountsBPSMail]", Log.Error, 1);
				return;
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}

		private void HTBAccountsMail()
		{	
            DataSet dsHTBAccounts = new DataSet();		
			DataSet dsReportRecipients = new DataSet();
            //C1.C1Excel.C1XLBook htbBook = new C1XLBook();
	
			string sql = "";
			string filePath = "";
			string fileMakeTime = null; 
			
			int rptCount = 0;
			int rowCount = 0;
			bool errFlag = false;

			if (!Master.ContractsBizDate.Equals(Master.BizDate) || !Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
			{
				return;
			}

			if (KeyValue.Get("AresHTBAccountsMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
			{
				Log.Write("HTB_Accounts file email has already occured, for BizDate: " + Master.BizDate + ".  [AresMain.HTBAccountsMail]", 2);
				return;
			}

			fileMakeTime = KeyValue.Get("AresHTBAccountsMakeTime", "11:05", dbCnStr);
			if (fileMakeTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) > 0 )
			{
				return;
			}

			try 
			{
				dsReportRecipients = ReportRecipientsGet("HTB_Accounts");		// Using Matt's new Dataset function 

				SqlConnection dbCn = new SqlConnection(dbCnStr);

				sql = "Select SBS.GroupCode, \r\n" +
							 "SBS.AccountNumber, \r\n" +
							 "SBS.SecId, \r\n" +
							 "SIL.SecIdLink As Symbol, \r\n" +
							 "SBS.QuantityShorted, \r\n" +
							 "SBS.QuantityCovered \r\n" +
							 "From   dbo.tbShortSaleBillingSummary SBS (nolock) \r\n" +
							 "       Left Outer Join dbo.tbSecIdLinks SIL (nolock) \r\n" +
							 "       On  SBS.SecId = SIL.SecId  \r\n" +
							 "       And SIL.SecIdTypeIndex = 2 \r\n" +
							 "Where  SBS.BizDate  = '" + Master.BizDatePrior + "' \r\n" +
							 "Order By SBS.GroupCode, SBS.AccountNumber, SBS.SecId";
								
				SqlCommand dbCmd = new SqlCommand(sql, dbCn);
				dbCmd.CommandType = CommandType.Text;
                dbCmd.CommandTimeout = 900;
										
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsHTBAccounts, "HardToBorrowAccounts");

				rowCount = dsHTBAccounts.Tables["HardToBorrowAccounts"].Rows.Count;

                Email mail = new Email(dbCnStr);
				Filer filer = new Filer();    

				// Matt's new Excel routine... using Dataset to generate the Excel file
				filePath = Excel.ExportDataSetToExcel(ref dsHTBAccounts, "HardToBorrowAccounts", "HTB_Accounts", true);

				foreach (DataRow dr in  dsReportRecipients.Tables["ReportRecipients"].Rows)  
				{
					try
					{
						if (dr["LastDeliveredDate"].ToString().Equals(""))
						{
							dr["LastDeliveredDate"] = Master.BizDatePrior;
						}

						if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
						{
							if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
							{
								mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "HTB Accounts " + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd"), "For ProcessDate = " + Master.BizDatePrior, filePath);
								Log.Write("HTB Accounts email sent to: " + dr["ReportRecipient"].ToString() + "  [AresMain.HTBAccountsMail]", 1);
							}
							else
							{	
								filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\HTB_ACCOUNTS_" + DateTime.Now.ToString("yyyyMMdd") + ".xls", "localHost", "", "", filePath);
								Log.Write("HTB Accounts file uploaded to: " + dr["ReportRecipient"].ToString() + "   [AresMain.HTBAccountsMail]", 2);
							}

							ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.ContractsBizDate);
							rptCount++;
						}
					}
					catch (Exception error)
					{
						errFlag = true;
						Log.Write("ERROR (foreach loop): " + error.Message.Replace("\r\n", "") + ".  [AresMain.HTBAccountsMail]", Log.Error, 1); 
					}
                }

				if (!errFlag)
				{
					KeyValue.Set("AresHTBAccountsMailDate", Master.BizDate, dbCnStr);
					Log.Write("HTB_Accounts file mailed, for BizDate: " + Master.BizDate + ". File: " + filePath + ", Data RowCount = " + rowCount.ToString() + ", Recipient Count = " + rptCount.ToString() + ".  [AresMain.HTBAccountsMail]", 2);
				}
            }
			catch (Exception error)	
			{
				Log.Write("ERROR: " + error.Message + ".  [AresMain.HTBAccountsMail]", Log.Error, 1);
				return;
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}

		private void ShortsVsFtdMail()
		{
			DataSet dsShortsVsFTD = new DataSet();		
			DataSet dsReportRecipients = new DataSet();
				
			StreamWriter sr = null;

            string tempPath = "";  
			string fileName = "";
			string lineFormat = ""; 
			string fileContent = "";		
			string fileMakeTime = null; 

			int rptCount = 0;
			int rowCount = 0;
			bool errFlag = false;

			// Skip if already done for current BizDate 
            if (!KeyValue.Get("AresShortsVsFTDMailDate", "", dbCnStr).Equals(Master.BizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
            {
                // Check if MakeTime is ready for ShortsVsFTD data to be send out for today 
                fileMakeTime = KeyValue.Get("AresShortsVsFtdMakeTime", "20:00", dbCnStr);
                if (fileMakeTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) > 0)
                {
                    KeyValue.Set("AresShortsVsFTDMailStatus", "WAIT -- " + Master.ContractsBizDate, dbCnStr);
                    return;
                }

                try
                {
                    //------ Initialize report file and email object 
                    //tempPath = Standard.ConfigValue("TempPath");  //KeyValue.Get("AresTempPath", @"C:\", dbCnStr);			// C:\Sendero\DailyReports_Archive\
                    Email mail = new Email(dbCnStr);
                    Filer filer = new Filer();

                    //------ PART ONE ------ ShortsVsFTD EXCEPTIONS File --------------------------------------------

                    //------ Get the list of report recipients for ShortsVsFTD_EXCEPTIONS list 
                    dsReportRecipients = ReportRecipientsGet("ShortsVsFTD_Exceptions");				// Using Matt's new Dataset function 

                    //------ Get the Exceptions List Dataset 
                    SqlConnection dbCn = new SqlConnection(dbCnStr);

                    SqlCommand dbCmd = new SqlCommand("dbo.spShortsVsFtdGet", dbCn);
                    dbCmd.CommandType = CommandType.StoredProcedure;
                    dbCmd.CommandTimeout = 900;

                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = Master.BizDate;

                    SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                    paramBizDatePrior.Value = Master.BizDatePrior;

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                    dataAdapter.Fill(dsShortsVsFTD, "Exceptions");

                    //rowCount = dsShortsVsFTD.Tables["Exceptions"].Rows.Count;   

                    //Outer Loop of ReportRecipients for EXCEPTIONS file 
                    foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
                    {
                        try
                        {
                            if (dr["LastDeliveredDate"].ToString().Equals(""))
                            {
                                dr["LastDeliveredDate"] = Master.BizDatePrior;
                            }

                            if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
                            {
                                //------ Create the ShortsVsFTD EXCEPTIONS report filecontent for this recipient's specific file format (attribute in tbReportValues) ------------------ 
                                rptCount = 0;
                                rowCount = 0;
                                lineFormat = "";
                                fileContent = "CUSIP,SYMBOL,PRICE,CNS FTD,EX/DEF,SHORTS,NET BORROW,BUY IN,BUY IN AMT ($),PLEDGE RELEASE" + "\r\n";	// File has header columns 

                                foreach (DataRow drExceptions in dsShortsVsFTD.Tables["Exceptions"].Rows)
                                {
                                    lineFormat = dr["Format"].ToString();

                                    if (dr["Justify"].ToString().Trim().ToUpper().Equals("Y"))
                                    {
                                        lineFormat = lineFormat.Replace("%CUSIP", drExceptions["CUSIP"].ToString().Trim().PadRight(12, ' '));
                                        lineFormat = lineFormat.Replace("%Symbol", drExceptions["Symbol"].ToString().Trim().PadRight(6, ' '));
                                        lineFormat = lineFormat.Replace("%Price", drExceptions["Price"].ToString().Trim().PadLeft(18, ' '));
                                        lineFormat = lineFormat.Replace("%CNS_FTD", drExceptions["CNS_FTD"].ToString().Trim().PadLeft(15, ' '));
                                        lineFormat = lineFormat.Replace("%EX_DEF", drExceptions["EX_DEF"].ToString().Trim().PadLeft(15, ' '));
                                        lineFormat = lineFormat.Replace("%Shorts", drExceptions["Shorts"].ToString().Trim().PadLeft(15, ' '));
                                        lineFormat = lineFormat.Replace("%NetBorrow", drExceptions["NetBorrow"].ToString().Trim().PadLeft(15, ' '));
                                        lineFormat = lineFormat.Replace("%BuyIn", drExceptions["BuyIn"].ToString().Trim().PadLeft(15, ' '));
                                        lineFormat = lineFormat.Replace("%BuyInAmount", drExceptions["BuyInAmount"].ToString().Trim().PadLeft(18, ' '));
                                        lineFormat = lineFormat.Replace("%PledgeRelease", drExceptions["PledgeRelease"].ToString().Trim().PadLeft(15, ' '));
                                    }
                                    else
                                    {
                                        lineFormat =
                                            drExceptions["CUSIP"].ToString().Trim() + "," +
                                            drExceptions["Symbol"].ToString().Trim() + "," +
                                            decimal.Parse(drExceptions["Price"].ToString()).ToString("0.####") + "," +
                                            decimal.Parse(drExceptions["CNS_FTD"].ToString()).ToString("0") + "," +
                                            decimal.Parse(drExceptions["EX_DEF"].ToString()).ToString("0") + "," +
                                            decimal.Parse(drExceptions["Shorts"].ToString()).ToString("0") + "," +
                                            decimal.Parse(drExceptions["NetBorrow"].ToString()).ToString("0") + "," +
                                            decimal.Parse(drExceptions["BuyIn"].ToString()).ToString("0") + "," +
                                            decimal.Parse(drExceptions["BuyInAmount"].ToString()).ToString("0.####") + "," +
                                            decimal.Parse(drExceptions["PledgeRelease"].ToString()).ToString("0");
                                    }

                                    fileContent += lineFormat + "\r\n";
                                    rowCount++;

                                }	// foreach inner drExceptions File Loop 

                                //------ Create the file 

                                // Matt's new Excel routine... using Dataset to generate the Excel file
                                tempPath = Excel.ExportDataSetToExcel(ref dsShortsVsFTD, "Exceptions", "Shorts-Vs-FTD", true);

                                fileName = tempPath;

                                //------ Send the email with Shorts file 
                                if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
                                {
                                    mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "Shorts Vs FTD " + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd"), "", fileName);
                                    // mail.Send("bstone@penson.com", "sendero@penson.com", "Shorts Vs FTD " + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd"), "", fileName); 
                                }
                                else
                                {	// Not an Email address, upload to a ftp site 					
                                    mail.Send("support.stockloan@penson.com", "sendero@penson.com", "Short-Vs-FTD Exceptions File Update", "Short-Vs-FTD-Exceptions File Uploaded to " + @dr["ReportRecipient"].ToString(), fileName);
                                    //filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\Shorts-Vs-FTD-" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd") + ".csv", "localHost", "", "", fileName);
                                }

                                //------ Report has been mailed/uploaded, now Update tbReportValues for this report/recipient to new date. 
                                ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.BizDate);
                                rptCount++;
                            }

                        }
                        catch (Exception error)
                        {
							errFlag = true;
							KeyValue.Set("AresShortsVsFTDMailStatus", "EXCEPTIONS File ERROR (" + DateTime.Now.ToString("yyyy-MM-dd") + "): " + error.Message.Replace("\r\n", ""), dbCnStr);
                            Log.Write("ERROR (foreach loop): " + error.Message + ".  [AresMain.ShortsVsFTDMail]", Log.Error, 1);
                        }

                    }	// foreach Outer Loop:  dsReportRecipients for EXCEPTIONS report 


                    //------ PART TWO ------ ShortsVsFTD Fails-To-BUY-IN File --------------------------------------------

                    //------ Get the list of report recipients for ShortsVsFTD FAILS-To-BUY-IN list 
                    dsReportRecipients = ReportRecipientsGet("ShortsVsFTD_FailsToBuyIn");				// Using Matt's new ReportRecipient dataset function 
                    tempPath = Standard.ConfigValue("TempPath");
                    //------ Get the Fails-to-Buy-Ins List Dataset 
                    SqlParameter paramFailsBuyInList = dbCmd.Parameters.Add("@FailsBuyInList", SqlDbType.Bit);
                    paramFailsBuyInList.Value = 1;

                    dataAdapter.Fill(dsShortsVsFTD, "FailsBuyIn");
                    //rowCount = dsShortsVsFTD.Tables["FailsBuyIn"].Rows.Count;   

                    //Outer Loop of ReportRecipients for BUY-INS file 
                    foreach (DataRow dr in dsReportRecipients.Tables["ReportRecipients"].Rows)
                    {
                        try
                        {
                            if (dr["LastDeliveredDate"].ToString().Equals(""))
                            {
                                dr["LastDeliveredDate"] = Master.BizDatePrior;
                            }

                            if (!DateTime.Parse(dr["LastDeliveredDate"].ToString()).ToString(Standard.DateFormat).Equals(Master.BizDate))
                            {
                                //------ Create the ShortsVsFTD BUY-INS report filecontent for this recipient's specific file format (attribute in tbReportValues) ------------------ 
                                rptCount = 0;
                                rowCount = 0;
                                lineFormat = "";
                                fileContent = "";    //CUSIP,SYMBOL,BUYIN	// File does Not need header columns 

                                foreach (DataRow drFails in dsShortsVsFTD.Tables["FailsBuyIn"].Rows)
                                {
                                    lineFormat = dr["Format"].ToString();

                                    if (dr["Justify"].ToString().Trim().ToUpper().Equals("Y"))
                                    {
                                        lineFormat = lineFormat.Replace("%CUSIP", drFails["CUSIP"].ToString().Trim().PadRight(12, ' '));
                                        lineFormat = lineFormat.Replace("%Symbol", drFails["Symbol"].ToString().Trim().PadRight(6, ' '));
                                        lineFormat = lineFormat.Replace("%BuyIn", drFails["BuyIn"].ToString().Trim().PadLeft(15, ' '));
                                    }
                                    else
                                    {
                                        lineFormat =
                                            drFails["CUSIP"].ToString().Trim() + "," +
                                            drFails["Symbol"].ToString().Trim() + "," +
                                            decimal.Parse(drFails["BuyIn"].ToString()).ToString("0");
                                    }

                                    fileContent += lineFormat + "\r\n";
                                    rowCount++;

                                }	// foreach inner drFails File Loop 

                                //------ Create the file 
                                fileName = tempPath + @"\FAILS_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy_MM_dd") + ".txt";
                                sr = File.CreateText(fileName);
                                sr.Write(fileContent);
                                sr.Close();

                                //------ Send the email with FAILS-To-Buy_In file 
                                if (dr["ReportRecipient"].ToString().IndexOf("@", 0) > 0)
                                {
                                    //mail.Send("bstone@penson.com", "sendero@penson.com", "EOD FAILS File " + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd"), "FAILS_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy_MM_dd") + @".txt to be copy to \\RSPen001\Fails folder.", fileName); 
                                    mail.Send(dr["ReportRecipient"].ToString(), "sendero@penson.com", "EOD FAILS File " + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy-MM-dd"), "FAILS_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy_MM_dd") + @".txt to be copy to \\RSPen001\Fails folder.", fileName);
                                }
                                else
                                {	// Not an Email address, upload to a ftp site 
                                    mail.Send("support.stockloan@penson.com", "sendero@penson.com", "EOD FAILS File Update", "EOD FAILS_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy_MM_dd") + ".txt  File Uploaded to " + @dr["ReportRecipient"].ToString(), fileName);
                                    filer.FileNoTempPut(dr["ReportRecipient"].ToString() + @"\FAILS_" + DateTime.Parse(Master.ContractsBizDate).ToString("yyyy_MM_dd") + ".txt", "localHost", "", "", fileName);
                                }

                                //------ EOD FAILS file has been mailed/uploaded, now Update tbReportValues for this report/recipient to new date. 
                                ReportRecipientSet(dr["ReportName"].ToString(), dr["ReportRecipientNumber"].ToString(), Master.BizDate);
                                rptCount++;
                            }

                        }
                        catch (Exception error)
                        {
							errFlag = true;
							KeyValue.Set("AresShortsVsFTDMailStatus", "FAILS File ERROR (" + DateTime.Now.ToString("yyyy-MM-dd") + "): " + error.Message.Replace("\r\n", ""), dbCnStr);
							Log.Write("ERROR: " + error.Message.Replace("\r\n", "") + ".  [AresMain.ShortsVsFTDMail]", Log.Error, 1);
                        }

                    }	// foreach outer Loop:  dsReportRecipients for Buy-Ins report 

					if (!errFlag)
					{
						//------ Successfully completed BOTH ShortsVsFTD Exceptions and FAILS-BuyIn files creating and mailing 
						KeyValue.Set("AresShortsVsFTDMailDate", Master.ContractsBizDate, dbCnStr);
						KeyValue.Set("AresShortsVsFTDMailStatus", "OK", dbCnStr);
						Log.Write("Daily Shorts-Vs-FTD Exception files and Fails files mailed/uploaded, for BizDate: " + Master.BizDate + ".  [AresMain.ShortsVsFtdMail]", 2);
					}

                }
                catch (Exception error)
                {
                    KeyValue.Set("AresShortsVsFTDMailStatus", "ERROR (" + DateTime.Now.ToString("yyyy-MM-dd") + "): " + error.Message, dbCnStr);
                    Log.Write("ERROR: " + error.Message + ".  [AresMain.ShortsVsFtdMail]", Log.Error, 1);
                    return;
                }
                finally
                {		// Graceful Exit 
                    if (dbCn.State != ConnectionState.Closed)
                    {
                        dbCn.Close();
                    }
                }
            }
            else
            {
                KeyValue.Set("AresShortsVsFTDMailStatus", "WAIT -- " + Master.ContractsBizDate, dbCnStr);
                return;
            }
		}

		private void FailedS3MovementsEmail()
		{
			DataSet dsTempMvts = new DataSet();
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			SqlDataAdapter dataAdapter = null;
				
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{											
					string sql ="select	accountnumber,\r\n" +
						"accounttype,\r\n" + 
						"secid,\r\n" +
						"quantity,\r\n" +
						"indicator\r\n" +
						"from	tbsegentries with (nolock)\r\n" +
						"where	bizdate = '" + Master.BizDate + "'\r\n" +
						"and		((isfailed = 1) or (isRequested = 1 and isProcessed = 0))\r\n" +
						"and		TimeOfDay = 'I'";


					string emailBody = "";

					SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
					dbCmd.CommandType = CommandType.Text;
                    dbCmd.CommandTimeout = 900;
							
					dataAdapter = new SqlDataAdapter(dbCmd);
					dataAdapter.Fill(dsTempMvts, "FailedMovements");

					
					emailBody +=  "AccountNumber".PadRight(15, ' ') + "T".PadRight(2, ' ') + "SecId".PadRight(10, ' ') +  "Quantity".PadLeft(15, ' ').Substring(0, 15) + "I".PadLeft(3, ' ') +	"\r\n";

					foreach (DataRow dr in dsTempMvts.Tables["FailedMovements"].Rows)
					{
						emailBody +=  dr["AccountNumber"].ToString().PadRight(15, ' ') + dr["AccountType"].ToString().PadRight(2, ' ') + dr["SecId"].ToString().PadRight(10, ' ') +  dr["Quantity"].ToString().PadLeft(15, ' ').Substring(0, 15) + dr["Indicator"].ToString().PadLeft(3, ' ') +	"\r\n";
					}
							
					if (!previousEmail.Equals(emailBody))
					{	
						string to = KeyValue.Get("AresS3FailMovementsMailTo", "mbattaini@penson.com", dbCnStr);
							
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresS3FailMovementsMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresS3FailMovementsMailSubject", "Failed Intra-day S3 Movements", dbCnStr),
								emailBody);															
					
							previousEmail = emailBody;
						}								
					}
				}				
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}							
		}
		
		private void BankLoanLocationExceptions()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();
				
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresBankLoanLocationExceptionsEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{							
						if (KeyValue.Get("BankLoanDtcReset0234", "", dbCnStr).Equals("True"))
						{							
							string emailBody = "";

							SqlCommand dbCmd = new SqlCommand("spBankLoanPledgeSummaryGet",localDbCn);
							dbCmd.CommandType = CommandType.StoredProcedure;
							dbCmd.CommandTimeout = 1600;

							SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
							paramBizDate.Value = Master.ContractsBizDate;

							SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);											
							dataAdapter.Fill(dataSet, "BankLoanPledgeSummary");
				
							emailBody = "Special Loan Exceptions\r\n";
							emailBody += "-----------------------\r\n";
								
							foreach(DataRow dr in dataSet.Tables["BankLoanPledgeSummary"].Rows)
							{
								if ((((long) dr["SpecialQuantityAvailable"]) < 0) && ((((long) dr["SpecialQuantityPledged"]) > 0) ))
								{
									emailBody += dr["SecId"].ToString() + " " +  dr["Symbol"].ToString().PadRight(10, ' ') + " " + dr["SpecialQuantityAvailable"].ToString() + "\r\n";
								}
							}
								
							emailBody += "\r\n\r\nCustomer Loan Exceptions\r\n";
							emailBody += "-----------------------\r\n";
								
							foreach(DataRow dr in dataSet.Tables["BankLoanPledgeSummary"].Rows)
							{
								if ((((long) dr["CustomerQuantityAvailable"]) < 0)  && ((((long) dr["CustomerQuantityPledged"]) > 0) ))
								{
									emailBody += dr["SecId"].ToString() + " " +  dr["Symbol"].ToString().PadRight(10, ' ') + " " + dr["CustomerQuantityAvailable"].ToString() + "\r\n";
								}
							}
								
							emailBody += "\r\n\r\nFirm Loan Exceptions\r\n";
							emailBody += "-----------------------\r\n";

							foreach(DataRow dr in dataSet.Tables["BankLoanPledgeSummary"].Rows)
							{
								if ((((long) dr["FirmQuantityAvailable"]) < 0)  && ((((long) dr["FirmQuantityPledged"]) > 0) ))
								{
									emailBody += dr["SecId"].ToString() + " " + dr["Symbol"].ToString().PadRight(10, ' ') + " " + dr["FirmQuantityAvailable"].ToString() + "\r\n";
								}
							}																																																
								
							string to = KeyValue.Get("AresBankLoanLocationExceptionsMailTo", "", dbCnStr);
							
							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresBankLoanLocationExceptionsMailFrom", "stockloan@penson.com", dbCnStr),
									KeyValue.Get("AresBankLoanLocationExceptionsMailSubject", "BankLoan Location Exceptions", dbCnStr),
									emailBody);															
					
								KeyValue.Set("AresBankLoanLocationExceptionsEmailDate", Master.ContractsBizDate, dbCnStr);
							}								
						}						
					}					
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}		

		private void Straddles()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresStraddlesEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{							
						string sql ="select	a.accountnumber, \r\n" +
							"a.cusip,\r\n" +
							"a.settlementdatequantity\r\n" +
							"from	stocklocationcurrent a,\r\n" +
							"stocklocationcurrent b\r\n" +
							"where	a.firm = '07'\r\n" +
							"and		b.firm = a.firm\r\n" +
							"and		a.cusip = b.cusip\r\n" +
							"and		a.accountnumber = b.accountnumber\r\n" +
							"and		a.accounttype = 3\r\n" +
							"and		b.accounttype = 3\r\n" +
							"and		a.settlementdatequantity > 0\r\n" +
							"and		b.settlementdatequantity < 0\r\n" +
							"and		a.locmemo  ='C'\r\n" +
							"and		b.locmemo = 'S'\r\n";


						string emailBody = "";

						SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
						dbCmd.CommandType = CommandType.Text;

						SqlDataReader dataReader = null;
					
						localDbCn.Open();
						dataReader = dbCmd.ExecuteReader();				
							
						int count = 0;

						emailBody = "Account".PadRight(15, ' ') +  "CUSIP".PadRight(10, ' ') +  "Quantity".PadLeft(15, ' ') + "\r\n";

						while (dataReader.Read())
						{
							emailBody += dataReader.GetValue(0).ToString().PadRight(15, ' ') +  dataReader.GetValue(1).ToString().PadRight(10, ' ') +  dataReader.GetValue(2).ToString().PadLeft(15, ' ') + "\r\n";
							count ++;
						}
						if (count == 0)
						{
							emailBody += "[No straddles to report]";
						}
				
						string to = KeyValue.Get("AresStraddlesMailTo", "mbattaini@penson.com", dbCnStr);
							
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresStraddlesMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresStraddlesMailSubject", "Straddles Account Type 3", dbCnStr),
								emailBody);															
					
							KeyValue.Set("AresStraddlesEmailDate", Master.ContractsBizDate, dbCnStr);
						}

						dataReader.Close();
					}						
				}									
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}		

		private void BankLoanExceptions()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresBankLoanExceptionsEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{							
						if (KeyValue.Get("BankLoanDtcReset0234", "", dbCnStr).Equals("True"))
						{
							string sql ="select	distinct(b.secid), \r\n" +
								"sil.secidlink as symbol, \r\n" +
								"sum (b.CustomerPledgeSettled + b.FirmPledgeSettled)\r\n" +
								"from	tbBoxPosition b,\r\n" +
								"			tbsecidlinks sil\r\n" +
								"Where	b.secid not in (select secid from tbBankLoanposition where secid = b.secid)\r\n" +
								"and		b.bookgroup = '0234' and b.bizdate = '" + Master.ContractsBizDate + "' and (b.CustomerPledgeSettled + b.FirmPledgeSettled) > 0	\r\n" +
								"and		sil.secid = b.secid\r\n" +
								"and		sil.secidtypeindex = 2\r\n" +
								"group by b.secid, sil.secidlink\r\n" +
								"order by sil.secidlink\r\n";


							string emailBody = "";

							SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
							dbCmd.CommandType = CommandType.Text;
                            dbCmd.CommandTimeout = 9000;

							SqlDataReader dataReader = null;
					
							localDbCn.Open();
							dataReader = dbCmd.ExecuteReader();				
				
							while (dataReader.Read())
							{
								emailBody += dataReader.GetValue(0).ToString().PadRight(12, ' ') +  dataReader.GetValue(1).ToString().PadRight(10, ' ') +  dataReader.GetValue(2).ToString().PadLeft(15, ' ') + "\r\n";
							}
				
							string to = KeyValue.Get("AresBankLoanExceptionsMailTo", "", dbCnStr);
							
							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresBankLoanExceptionsMailFrom", "stockloan@penson.com", dbCnStr),
									KeyValue.Get("AresBankLoanExceptionsMailSubject", "BankLoan Exceptions", dbCnStr),
									emailBody);															
					
								KeyValue.Set("AresBankLoanExceptionsEmailDate", Master.ContractsBizDate, dbCnStr);
							}

							dataReader.Close();
						}						
					}					
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}		

		private void RateDifferencesEmail()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresRateDifferencesEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{

                        //if (KeyValue.Get("BankLoanDtcReset0234", "", dbCnStr).Equals("True"))
                        if (KeyValue.Get("BroadRidgeBoxPositionLoadDate", "", dbCnStr).Equals(Master.BizDate) &&
                            KeyValue.Get("BroadRidgeEasyBorrowFileDate", "", dbCnStr).Equals(Master.BizDate))
						{
							string sql ="Select	C.ContractId,\r\n" +
								"C.SecId,\r\n" +
								"C.Rate,\r\n" +
								"S.ContractId,\r\n" +
								"S.SecId,\r\n" +
								"S.Rate\r\n" +
								"from	tbContracts C,\r\n" +
								"			tbContracts S\r\n" +
								"Where	C.BizDate = '" + Master.ContractsBizDate + "'\r\n" +
								"And		C.BookGroup = '0158' \r\n" +
								"And		S.BizDate = C.BizDate\r\n" +
								"And		S.BookGroup = C.BookGroup\r\n" +
								"And		S.SecId = C.SecId\r\n" +
								"And		S.Rate > 0\r\n" +
								"And		C.Rate < 0\r\n" +
								"And		S.ContractId <> C.ContractId\r\n" +
								"And		C.ContractType = 'L'\r\n" +
								"And		S.ContractType = C.ContractType\r\n";


							string emailBody = "";

							SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
							dbCmd.CommandType = CommandType.Text;
                            dbCmd.CommandTimeout = 900;

							SqlDataReader dataReader = null;
					
							localDbCn.Open();
							dataReader = dbCmd.ExecuteReader();				
				
							while (dataReader.Read())
							{
								emailBody += dataReader.GetValue(0).ToString().PadRight(12, ' ') +  dataReader.GetValue(1).ToString().PadRight(10, ' ') +  dataReader.GetValue(2).ToString().PadLeft(15, ' ') + 
									dataReader.GetValue(3).ToString().PadRight(12, ' ') +  dataReader.GetValue(4).ToString().PadRight(10, ' ') +  dataReader.GetValue(5).ToString().PadLeft(15, ' ') + "\r\n";
							}
				
							string to = KeyValue.Get("AresRateDifferencesMailTo", "", dbCnStr);
							
							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresRateDifferencessMailFrom", "stockloan@penson.com", dbCnStr),
									KeyValue.Get("AresRateDifferencesMailSubject", "Rate Differences", dbCnStr),
									emailBody);															
					
								KeyValue.Set("AresRateDifferencesEmailDate", Master.ContractsBizDate, dbCnStr);
							}

							dataReader.Close();
						}						
					}					
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}		

		private void CnsDeliveriesMail ()
		{	//Retired 2009-03-18
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
	
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresCNSDeliveriesEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{
						if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresCNSDeliveriesCreateTime", "06:00", dbCnStr), "HH:mm", null))
						{
						
							string sql = "Select	S.CUSIP,\r\n" +
								"	M.SecIdLink,\r\n" +
								"	S.SettlementDateQuantityCurrent\r\n" +
								"From	StockLocationCurrent S,\r\n" +
								"			tbSecIdLinks M\r\n" +
								"Where	S.AccountNumber = '93111128'\r\n" +
								"And		S.CUSIP IN (	Select distinct(secid)\r\n" +
								"									from	tbContracts\r\n" +
								"							WHere	BizDate = (select keyvalue from tbkeyvalues where keyid = 'ContractsBizDate')\r\n" +
								"And		BookGroup  ='0234'					\r\n" +
								"And		Rate < 0)\r\n" +
								"And	S.SettlementDateQuantityCurrent < 0\r\n" +
								"And	M.SecId = S.CUSIP\r\n" +
								"And	M.SecIdTypeIndex = 2\r\n" +
								"Order  by M.SecIdLink\r\n";

							string emailBody = "";
		
							SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
							dbCmd.CommandType = CommandType.Text;

							SqlDataReader dataReader = null;
					
							localDbCn.Open();
							dataReader = dbCmd.ExecuteReader();				
				
							while (dataReader.Read())
							{
								emailBody += dataReader.GetValue(0).ToString().PadRight(12, ' ') +  dataReader.GetValue(1).ToString().PadRight(10, ' ') +  dataReader.GetValue(2).ToString().PadLeft(15, ' ') + "\r\n";
							}
				
							string to = KeyValue.Get("AresCnsDeliveriesMailTo", "", dbCnStr);
							
							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresCnsDeliveriesMailFrom", "stockloan@penson.com", dbCnStr),
									KeyValue.Get("AresCnsDeliveriesMailSubject", "CNS", dbCnStr),
									emailBody);															
					
								KeyValue.Set("AresCNSDeliveriesEmailDate", Master.ContractsBizDate, dbCnStr);
							}

							dataReader.Close();
						}						
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}

		public void RecordDateContractsMail()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
	
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresRecordDateContractsEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{
						if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresRecordDateContractsCreateTime", "06:00", dbCnStr), "HH:mm", null))
						{
						
							string sql =	"Select	C.ContractId, \r\n"+
								"C.SecId, \r\n" +
								"SIL.SecIdLink,\r\n" +
								"C.Rate,\r\n" + 
								"S.RecordDateCash\r\n" +
								"From	tbContracts C,\r\n" +
								"			tbSecMaster S,\r\n" +
								"			tbSecIdLinks SIL\r\n" +
								"Where	C.BizDate = '" + Master.ContractsBizDate + "'\r\n" +
								"And		C.BookGroup = '0158' \r\n" +        //2012-02-09 changed from '0234' to '0158'
								"And		C.ContractType = 'L'\r\n" +
								"And		C.Rate > 0\r\n" +
								"And		S.SecId = C.SecId\r\n" +
								"And		S.RecordDateCash = '" + Master.BizDateNext + "'\r\n" +
								"And		SIL.SecId = C.SecId\r\n" +
								"And		SIL.SecIdTypeIndex = 2";

							string emailBody = "";
						
							SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
							dbCmd.CommandType = CommandType.Text;
                            dbCmd.CommandTimeout = 900;

							SqlDataReader dataReader = null;
					
							localDbCn.Open();
							dataReader = dbCmd.ExecuteReader();				
				
							while (dataReader.Read())
							{
								emailBody += dataReader.GetValue(0).ToString().PadRight(12, ' ') 
									+		dataReader.GetValue(1).ToString().PadLeft(12, ' ') 
									+		dataReader.GetValue(2).ToString().PadLeft(15, ' ') 
									+		dataReader.GetValue(3).ToString().PadLeft(8, ' ') 
									+		dataReader.GetValue(4).ToString().PadLeft(15, ' ') +"\r\n";
							}
				
							string to = KeyValue.Get("AresRecordDateContractsMailTo", "", dbCnStr);
							
							if (!to.Equals(""))
							{
								Email email = new Email(dbCnStr);
								email.Send( to,
									KeyValue.Get("AresRecordDateContractsMailFrom", "stockloan@penson.com", dbCnStr),
									KeyValue.Get("AresRecordDateContractsMailSubject", "Contracts with record date tomorrow", dbCnStr),
									emailBody);															
					
								KeyValue.Set("AresRecordDateContractsEmailDate", Master.ContractsBizDate, dbCnStr);
							}

							dataReader.Close();
						}						
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}

		public void ThresholdRatesMail()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
            DataSet dsRatesList = new DataSet();

			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresThresholdRatesMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{
						if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresThresholdRatesCreateTime", "06:00", dbCnStr), "HH:mm", null))
						{						
							if (KeyValue.Get("AztecFileLoadDate", "", dbCnStr).Equals(Master.ContractsBizDate))								
							{
                                SqlCommand dbCmd = new SqlCommand("dbo.spThresholdListRatesGet", localDbCn);
                                dbCmd.CommandType = CommandType.StoredProcedure;
                                dbCmd.CommandTimeout = 900;

                                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                                dataAdapter.Fill(dsRatesList, "ThresholdRatesList");

                                string emailBody = "";

                                foreach (DataRow dr in dsRatesList.Tables["ThresholdRatesList"].Rows)
                                {
                                    emailBody += dr["SecId"].ToString().PadRight(12, ' ') + " "
                                               + dr["Symbol"].ToString().PadLeft(12, ' ') + " "
                                               + decimal.Parse(dr["Rate"].ToString()).ToString("0.000").PadLeft(15, ' ') + "\r\n";
                                }

                                string to = KeyValue.Get("AresThresholdRatesMailTo", "", dbCnStr);
							
								if (!to.Equals(""))
								{
									Email email = new Email(dbCnStr);
									email.Send( to,
										KeyValue.Get("AresThresholdRatesMailFrom", "stockloan@penson.com", dbCnStr),
										KeyValue.Get("AresThresholdRatesMailSubject", "Penson Threshold Rates For: " + Master.BizDate , dbCnStr),
										emailBody);															
					
									KeyValue.Set("AresThresholdRatesMailDate", Master.ContractsBizDate, dbCnStr);
									Log.Write("ThresholdRatesMail completed for " + Master.ContractsBizDate + "  [AresMain.ThresholdRatesMail]", Log.Information, 1);
								}

							}						
						}
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [AresMain.ThresholdRatesMail]", 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}			
		}

		public void HardRatesMail()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
            DataSet dsRatesList = new DataSet();
	
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresHardRatesMailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{
						if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresHardRatesCreateTime", "06:00", dbCnStr), "HH:mm", null))
						{						
							if (KeyValue.Get("AztecFileLoadDate", "", dbCnStr).Equals(Master.ContractsBizDate))								
							{
                                SqlCommand dbCmd = new SqlCommand("dbo.spHardRatesGet", localDbCn);
                                dbCmd.CommandType = CommandType.StoredProcedure;
                                dbCmd.CommandTimeout = 900;

                                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                                dataAdapter.Fill(dsRatesList, "HardRatesList");

                                string emailBody = "";

                                foreach (DataRow dr in dsRatesList.Tables["HardRatesList"].Rows)
                                {
                                    emailBody += dr["SecId"].ToString().PadRight(12, ' ') + " "
                                               + dr["Symbol"].ToString().PadLeft(12, ' ') + " "
                                               + decimal.Parse(dr["Rate"].ToString()).ToString("0.000").PadLeft(15, ' ') + "\r\n";
                                }
				
								string to = KeyValue.Get("AresHardRatesMailTo", "", dbCnStr);
							
								if (!to.Equals(""))
								{
									Email email = new Email(dbCnStr);
									email.Send( to,
										KeyValue.Get("AresHardRatesMailFrom", "stockloan@penson.com", dbCnStr),
										KeyValue.Get("AresHardRatesMailSubject", "Penson Hard Rates For: " + Master.BizDate , dbCnStr),
										emailBody);															
					
									KeyValue.Set("AresHardRatesMailDate", Master.ContractsBizDate, dbCnStr);
									Log.Write("HardRatesMail completed for " + Master.ContractsBizDate + "  [AresMain.HardRatesMail]", Log.Information, 1);
								}

							}						
						}
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [AresMain.HardRatesMail]", 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}
			
		private void LikeTrades ()
		{   //Retired 2012-06-12
			try
			{
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresLikeTradesEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{
						string to =  KeyValue.Get("AresLikeTradesMailTo", "", dbCnStr);
						string emailBody  = 
							"BookGroup	T ContractId  Security ID   Quantity\r\r" +
							"---------  - ----------  -----------   -----------\r\n" +

							"[No like contracts for 7380]\r\n";


						Email email = new Email(dbCnStr);

						if (!to.Equals(""))
						{
							email.Send( to,
								KeyValue.Get("AresLikeTradesMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresLikeTradesMailSubject", "Borrow / Loan Comparison For 7380", dbCnStr),
								emailBody);		
				
							KeyValue.Set("AresLikeTradesEmailDate", Master.ContractsBizDate, dbCnStr);
						}
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [AresMain.LikeTrades]", 1);
			}
		}

		private void DeficitReport()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			string emailBody = "";

			try
			{
				if (!KeyValue.Get("AresDeficitReportEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate) && Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
				{
					if (DateTime.Now >= DateTime.ParseExact(KeyValue.Get("AresDeficitReportCreateTime", "06:00", dbCnStr), "HH:mm", null))
					{							
						Common.Filer filer = new Common.Filer(Standard.ConfigValue("TempPath") + "deficitreport.txt");
						filer.Timeout = 90000;
								
						StreamReader streamReader = null;
								
						streamReader = new StreamReader(
							filer.StreamGet(
							Standard.ConfigValue("Phase3DeficitReportName", ""),
							Standard.ConfigValue("Phase3DeficitReportHost", ""),
							Standard.ConfigValue("Phase3DeficitReportUserName", ""),
							Standard.ConfigValue("Phase3DeficitReportPassword", "")),
							System.Text.Encoding.ASCII);     
						streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
				
						string fileContents = "";
						int lineCount = 1;
						int itemCount = 0;
						string line = "-";


						//fileContents = streamReader.ReadToEnd();

						//Log.Write(streamReader.ReadToEnd(), 1);
						int lineLength = 140;
								
						Log.Write("Filtering report: " + Standard.ConfigValue("Phase3DeficitReportName", ""), 1);

						while (line!= null)
						{  							
							line = streamReader.ReadLine();
							if (line != null)
							{
								line = line.Replace("\r", "");
														 
								try
								{
									if (line.Substring(0, 6).Equals("07960H"))
									{						
										string reportDate = "";
										reportDate = DateTime.Parse(line.Substring(116, 8)).ToString(Standard.DateFormat);
						
										if (!reportDate.Equals(Master.ContractsBizDate))
										{
											Log.Write("File " + Standard.ConfigValue("Phase3DeficitReportName", "") + " is Not for current date " +  Master.ContractsBizDate + ". [DeficitReport.Load]", 2);
											return;
										}
									}
									else if (line.Substring(0, 6).Equals("079604"))
									{																																		
										line = line.Replace("079604", "");
										emailBody += line + "\r\n";
										
										itemCount ++;																			
									}
							
									lineCount ++;
								}
								catch (Exception error)
								{
									Log.Write(error.Message + "  [AresMain.DeficitReport]", 1);
								}
							}
						}
						

						string to = KeyValue.Get("AresDeficitReportMailTo", "", dbCnStr);
							
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresDeficitReportMailFrom", "stockloan@penson.com", dbCnStr),
								KeyValue.Get("AresDeficitReportMailSubject", "CNS", dbCnStr),
								emailBody);															
					
							KeyValue.Set("AresDeficitReportEmailDate", Master.ContractsBizDate, dbCnStr);
							Log.Write("DeficitReport completed for " + Master.ContractsBizDate + "  [AresMain.DeficitReport]", Log.Information, 1);
						}
					}
				}
			}			
			catch (Exception error)
			{
				Log.Write(error.Message + "  [AresMain.DeficitReport]", 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}

		private void FailsEmail()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresFailsEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{							
						string sql ="select	B.SecId,\r\n" +
							"S.SecIdLink,\r\n" +
							"SB.LastPrice, \r\n" +
							"B.ExDeficitSettled,\r\n" +
							"B.DeficitDayCount,\r\n" +
							"DvpFailOutSettled,\r\n" +
							"DvpFailOutDayCount,\r\n" +
							"ClearingFailOutSettled,\r\n" +
							"ClearingFailOutDayCount,\r\n" +
							"BrokerFailOutSettled,\r\n" +
							"BrokerFailOutDayCount,\r\n" +
							"(Select Isnull(Sum(quantitysettled), 0) from tbContracts (NOLOCK) where BookGroup = '0158' and BizDate = '" + Master.ContractsBizDate + "' and secid = b.secid and contracttype = 'L') As Loans \r\n" +
							"from	tbBoxPosition B,\r\n" +
							"		tbSecIdLinks S,\r\n" +
							"		tbSecMaster SB\r\n" +
							"where	B.BookGroup = '0158' \r\n" +
							"And		B.BizDate = '"+ Master.ContractsBizDate +"'\r\n" +
							"And		B.SecId = S.SecId\r\n" +
							"And		S.SecIdTypeIndex = 2\r\n" +
							"And		SB.SecId = S.SecId\r\n" +
							"and		((B.DeficitDayCount > 4 AND B.ExDeficitSettled < 0) OR	DvpFailOutDayCount > 4 OR ClearingFailOutDayCount > 4 OR BrokerFailOutDayCount > 4)\r\n" +
							"and		(Select	Isnull(Sum(quantitysettled), 0) from tbContracts (Nolock) where BookGroup = '0158' and BizDate = '" + Master.ContractsBizDate +"' and secid = b.secid and contracttype = 'L') > 0\r\n" +
							"Group By B.SecId,\r\n" +
							"		S.SecIdLink,\r\n" +
							"		SB.LastPrice,\r\n" +
							"		B.ExDeficitSettled,\r\n" +
							"		B.DeficitDayCount,\r\n" +
							"		DvpFailOutSettled,\r\n" +
							"		DvpFailOutDayCount,\r\n" +
							"		ClearingFailOutSettled,\r\n" +
							"		ClearingFailOutDayCount,\r\n" +
							"		BrokerFailOutSettled,\r\n" +
							"		BrokerFailOutDayCount\r\n" +
							"Order by loans desc \r\n";


						string emailBody = "";

						SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
						dbCmd.CommandType = CommandType.Text;
                        dbCmd.CommandTimeout = 9000;

						SqlDataReader dataReader = null;
					
						localDbCn.Open();
						dataReader = dbCmd.ExecuteReader();				

						emailBody = "SecId".PadRight(12, ' ') +
							"Symbol".PadRight(12, ' ') + 
							"Price".PadRight(12, ' ') + 
							"ExDeficit".PadRight(12, ' ') +  
							"Days".PadRight(12, ' ')  +
							"DvpFTD".PadRight(12, ' ') +  
							"Days".PadRight(12, ' ')  +
							"CnsFTD".PadRight(12, ' ') +  
							"Days".PadRight(12, ' ') +
							"BrokerFTD".PadRight(12, ' ') +
							"Days".PadRight(12, ' ')  +
							"Loans".PadRight(12, ' ') + "\r\n";
				
						while (dataReader.Read())
						{
							emailBody +=	dataReader.GetValue(0).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(1).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(2).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(3).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(4).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(5).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(6).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(7).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(8).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(9).ToString().PadRight(12, ' ')	+  
								dataReader.GetValue(10).ToString().PadRight(12, ' ')	+ 
								dataReader.GetValue(11).ToString().PadRight(12, ' ') + "\r\n";
						}
				
						string to = KeyValue.Get("AresFailsMailTo", "", dbCnStr);
							
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresFailsMailFrom", "sendero@penson.com", dbCnStr),
								KeyValue.Get("AresFailsMailSubject", "Fails Email", dbCnStr),
								emailBody);															
					
							KeyValue.Set("AresFailsEmailDate", Master.ContractsBizDate, dbCnStr);
						}

						dataReader.Close();
					}						
				}					
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [AresMain.FailsEmail]", 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}				
		}

		private void RedsFailsEmail()
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			try
			{				
				if (Master.BizDate.Equals(Master.ContractsBizDate))
				{
					if (!KeyValue.Get("AresRedsFailsEmailDate", "", dbCnStr).Equals(Master.ContractsBizDate))
					{							
						string sql ="select	B.SecId,\r\n" +
							"S.SecIdLink,\r\n" +
							"SB.LastPrice, \r\n" +
							"B.ExDeficitSettled,\r\n" +
							"B.DeficitDayCount,\r\n" +
							"DvpFailOutSettled,\r\n" +
							"DvpFailOutDayCount,\r\n" +
							"ClearingFailOutSettled,\r\n" +
							"ClearingFailOutDayCount,\r\n" +
							"BrokerFailOutSettled,\r\n" +
							"BrokerFailOutDayCount\r\n" +								
							"from	tbBoxPosition B,\r\n" +
							"		tbSecIdLinks S,\r\n" +
							"		tbSecMaster SB\r\n" +
							"where	B.BookGroup = '0234'\r\n" +
							"And		B.BizDate = '"+ Master.ContractsBizDate +"'\r\n" +
							"And		B.SecId = S.SecId\r\n" +
							"And		S.SecIdTypeIndex = 2\r\n" +
							"And		SB.LastPrice >= 0.50\r\n" +
							"And		SB.BaseType = 'E'\r\n" + 
							"And		SB.SecId = S.SecId\r\n" +
							"and		((B.DeficitDayCount > 10 AND B.ExDeficitSettled < 0) OR	DvpFailOutDayCount > 10 OR ClearingFailOutDayCount > 10 OR BrokerFailOutDayCount > 10)\r\n" +
							"Group By B.SecId,\r\n" +
							"		S.SecIdLink,\r\n" +
							"		SB.LastPrice,\r\n" +
							"		B.ExDeficitSettled,\r\n" +
							"		B.DeficitDayCount,\r\n" +
							"		DvpFailOutSettled,\r\n" +
							"		DvpFailOutDayCount,\r\n" +
							"		ClearingFailOutSettled,\r\n" +
							"		ClearingFailOutDayCount,\r\n" +
							"		BrokerFailOutSettled,\r\n" +
							"		BrokerFailOutDayCount\r\n";								


						string emailBody = "";

						SqlCommand dbCmd = new SqlCommand(sql,localDbCn);
						dbCmd.CommandType = CommandType.Text;

						SqlDataReader dataReader = null;
					
						localDbCn.Open();
						dataReader = dbCmd.ExecuteReader();				

						emailBody = "SecId".PadRight(12, ' ') +
							"Symbol".PadRight(12, ' ') + 
							"Price".PadRight(12, ' ') + 
							"ExDeficit".PadRight(12, ' ') +  
							"Days".PadRight(12, ' ')  +
							"DvpFTD".PadRight(12, ' ') +  
							"Days".PadRight(12, ' ')  +
							"CnsFTD".PadRight(12, ' ') +  
							"Days".PadRight(12, ' ') +
							"BrokerFTD".PadRight(12, ' ') +
							"Days".PadRight(12, ' ')  + "\r\n";
				
						while (dataReader.Read())
						{
							emailBody +=	dataReader.GetValue(0).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(1).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(2).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(3).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(4).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(5).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(6).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(7).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(8).ToString().PadRight(12, ' ') +  
								dataReader.GetValue(9).ToString().PadRight(12, ' ')	+  
								dataReader.GetValue(10).ToString().PadRight(12, ' ') + "\r\n";
						}
				
						string to = KeyValue.Get("AresRedsFailsMailTo", "", dbCnStr);
							
						if (!to.Equals(""))
						{
							Email email = new Email(dbCnStr);
							email.Send( to,
								KeyValue.Get("AresRedsFailsMailFrom", "sendero@penson.com", dbCnStr),
								KeyValue.Get("AresRedsFailsMailSubject", "Fails Email Beyond 10 days", dbCnStr),
								emailBody);															
					
							KeyValue.Set("AresRedsFailsEmailDate", Master.ContractsBizDate, dbCnStr);
						}

						dataReader.Close();
					}						
				}					
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [AresMain.RedsFailsEmail]", 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
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
				recycleInterval = KeyValue.Get("AresMainRecycleIntervalBizDay", "0:20", dbCn);
			}
			else
			{
				recycleInterval = KeyValue.Get("AresMainRecycleIntervalNonBizDay", "6:00", dbCn);
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
					KeyValue.Set("AresMainRecycleIntervalBizDay", "0:20", dbCn);
					hours = 0;
					minutes = 20;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [AresMain.RecycleInterval]", Log.Error, 1);
				}
				else
				{
					KeyValue.Set("AresMainRecycleIntervalNonBizDay", "6:00", dbCn);
					hours = 6;
					minutes = 0;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [AresMain.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("AresMain will recycle in " + hours + " hours, " + minutes + " minutes. [AresMain.RecycleInterval]", 2);
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
