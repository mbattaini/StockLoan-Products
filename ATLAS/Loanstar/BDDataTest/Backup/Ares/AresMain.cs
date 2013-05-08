using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using System.Collections;
using System.Xml;
using StockLoan.Common;

namespace StockLoan.Ares
{
    public struct Report
    {      
        public string reportName;
        public string reportType;
        public string databaseServer;
        public string database;
        public bool trustedConnection;
        public string databaseUsername;
        public string databasePassword;

        public string triggerDatabaseServer;
        public string triggerDatabase;
        public string triggerTrustedConnection;
        public string triggerDatabaseUsername;
        public string triggerDatabasePassword;
        public string triggerDateType;
        public string triggerDateMethod;
        public string triggerDate;

        public string creationTime;
        public string spOperationDate;
        public string sp;
        public string spType;
        public string[] spExportFormat;
        public string filterResults;        
    }

	public class AresMain
	{
		private string notificationList;
		private string emailForm;
		private string dbCnStr;
		private string worldwideDbCnStr;			

		private string previousEmail = "";

		private Thread workerThread = null;
		private static bool isStopped = true;
		private static string tempPath;

		private Email email;
        
        private SqlConnection triggerDbCn;
        private SqlConnection dbCn;

        private DataSet dsRecipients;
        private DataSet dsReports;

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

			if ((workerThread == null)||(!workerThread.IsAlive)) // Must create new thread.
			{
				workerThread = new Thread(new ThreadStart(AresMainLoop));
				workerThread.Name = "Worker";
				workerThread.Start();

				Log.Write("Start command issued with new worker thread. [AresMain.Start]", 2);
			}
			else // Old thread will be just fine.
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
			while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
			{
				Log.Write("Start-of-cycle. [AresMain.AresMainLoop]", 2);
				KeyValue.Set("AresMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
        
				Master.BizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);
				Master.BizDatePrior = KeyValue.Get("BizDatePrior", "0001-01-01", dbCn);
				Master.BizDateExchange = KeyValue.Get("BizDateExchange", "0001-01-01", dbCn);
				Master.BizDatePriorExchange = KeyValue.Get("BizDatePriorExchange", "0001-01-01", dbCn);
				Master.ContractsBizDate =	KeyValue.Get("ContractsBizDate", "0001-01-01", dbCn);
				Master.BizDateNext = KeyValue.Get("BizDateNext", "0001-01-01", dbCn);

                DataView dvReportData = null;

                dsReports = ReportsGet();

                foreach (DataRow dr in dsReports.Tables["Reports"].Rows)
                {
                    Report report = new Report();

                    dsRecipients = ReportsRecipientsGet((long)dr["ReportId"]);


                    report.spExportFormat = new string[3];

                    LoadFile(dr["ReportFilePath"].ToString(), ref report);
                    TestTrigger(report);
                    
                    if (TestCreationTime(report))
                    {
                        Log.Write("Loading report data for report: " + dr["ReportName"].ToString() + ". [AresMain.AresMainLoop]", 1);                        
                        dvReportData = LoadReportData(ref report);

                        foreach (DataRow dr2 in dsRecipients.Tables["Recipients"].Rows)
                        {
                            try
                            {
                                string temp = FormatCustomer(ref report, dvReportData, (long)dr["ReportExportFormatId"], dr2["Path"].ToString());
                                Log.Write(temp, 1);
                            }
                            catch
                            {
                                Log.Write("Customer: " + dr2["Recipient"].ToString() + ", Report Name: " + dr["ReportName"].ToString() + ", Format Number: " + long.Parse(dr["ReportExportFormatId"].ToString()) + " error.", 1);
                            }
                        }
                    }
                    else
                    {
                        Log.Write(dr["ReportName"].ToString() + " did not pass creation time trigger. [AresMain.AresMainLoop]", 1);
                    }
                }

				KeyValue.Set("AresMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
				Log.Write("End-of-cycle. [AresMain.AresMainLoop]", 2);

				if (!isStopped)
				{
					Thread.Sleep(RecycleInterval());
				}
			}
		}

        public string FormatCustomer(ref Report report, DataView dvReportData, long formatNumber, string recipientPath)
        {
            string body = "";
            string format = "";

            foreach (DataRowView drv in dvReportData)
            {
                format = report.spExportFormat[formatNumber];

                format = format.Replace("%BOOKGROUP", drv["BOOKGROUP"].ToString());
                format = format.Replace("%BOOK", drv["BOOK"].ToString());
                format = format.Replace("%CONTRACTID", drv["CONTRACTID"].ToString());
                format = format.Replace("%SECID", drv["SecId"].ToString());
                format = format.Replace("%SYMBOL", drv["SYMBOL"].ToString());
                format = format.Replace("%QUANTITY", drv["QUANTITY"].ToString());
                format = format.Replace("%QUANTITYSETTLED", drv["QUANTITYSETTLED"].ToString());
                format = format.Replace("%AMOUNT", drv["AMOUNT"].ToString());
                format = format.Replace("%AMOUNTSETTLED", drv["AMOUNTSETTLED"].ToString());
                //format = format.Replace("%RATE", drv["RATE"].ToString());
                format = format.Replace("%AMOUNTMKT", drv["AMOUNTMKT"].ToString());
                format = format.Replace("%MARGIN", drv["MARGIN"].ToString());

                body += format + "\r\n";
            }

            return body;
        }

        public DataSet ReportsGet()
        {
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spReportsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Reports");
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
            }

            return dsTemp;
        }

        public DataSet ReportsRecipientsGet(long reportId)
        {
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spReportsRecipientsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramReportId = dbCmd.Parameters.Add("@ReportId", SqlDbType.BigInt);
                paramReportId.Value = reportId;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Recipients");
            }
            catch (Exception error)
            {
                Log.Write(error.Message, 1);
            }

            return dsTemp;
        }

        public void LoadFile(string path, ref Report report)
        {
            XmlTextReader xmlTextReader = new XmlTextReader(new StreamReader(path));

            string xmlElement = ""; ;

            while (xmlTextReader.Read())
            {
                switch (xmlTextReader.NodeType)
                {
                    case XmlNodeType.Element:
                        xmlElement = xmlTextReader.Name;
                        break;

                    case XmlNodeType.Text:
                        switch (xmlElement.ToUpper())
                        {
                            case ("REPORTNAME"):
                                report.reportName = xmlTextReader.Value.ToString();
                                break;

                            case ("REPORTTYPE"):
                                report.reportType = xmlTextReader.Value.ToString();
                                break;

                            case ("DATABASESERVER"):
                                report.databaseServer = xmlTextReader.Value.ToString();
                                break;

                            case ("DATABASE"):
                                report.database = xmlTextReader.Value.ToString();
                                break;

                            case ("TRUSTEDCONNECTION"):
                                report.trustedConnection = bool.Parse(xmlTextReader.Value.ToString());
                                break;

                            case ("DATABASEUSERNAME"):
                                report.databaseUsername = xmlTextReader.Value.ToString();
                                break;

                            case ("DATABASEPASSWORD"):
                                report.databasePassword = xmlTextReader.Value.ToString();
                                break;

                            case ("TRIGGER_DATABASESERVER"):
                                report.triggerDatabaseServer = xmlTextReader.Value.ToString();
                                break;

                            case ("TRIGGER_DATABASE"):
                                report.triggerDatabase = xmlTextReader.Value.ToString();
                                break;

                            case ("TRIGGER_TRUSTEDCONNECTION"):
                                report.triggerTrustedConnection = xmlTextReader.Value.ToString();
                                break;

                            case ("TRIGGER_DATABASEUSERNAME"):
                                report.triggerDatabaseUsername = xmlTextReader.Value.ToString();
                                break;

                            case ("TRIGGER_DATABASEPASSWORD"):
                                report.triggerDatabasePassword = xmlTextReader.Value.ToString();
                                break;

                            case ("TRIGGER_DATE_METHOD"):
                                report.triggerDateMethod = xmlTextReader.Value.ToString();
                                break;

                            case ("TRIGGER_DATE"):
                                report.triggerDate = xmlTextReader.Value.ToString();
                                break;

                            case ("CREATIONTIME"):
                                report.creationTime = xmlTextReader.Value.ToString();
                                break;

                            case ("TRIGGER_DATETYPE"):
                                report.triggerDateType = xmlTextReader.Value.ToString();
                                break;

                            case ("SP_OPERATION_DATE"):
                                report.spOperationDate = xmlTextReader.Value.ToString();
                                break;

                            case ("SP"):
                                report.sp = xmlTextReader.Value.ToString();
                                break;

                            case ("SP_TYPE"):
                                report.spType = xmlTextReader.Value.ToString();
                                break;

                            case ("SP_EXPORT_FORMAT_0"):
                                report.spExportFormat[0] = xmlTextReader.Value.ToString();
                                break;

                            case ("SP_EXPORT_FORMAT_1"):
                                report.spExportFormat[1] = xmlTextReader.Value.ToString();
                                break;

                            case ("SP_EXPORT_FORMAT_2"):
                                report.spExportFormat[2] = xmlTextReader.Value.ToString();
                                break;

                            case ("FILTERRESEULTS"):
                                report.filterResults = xmlTextReader.Value.ToString();
                                break;
                        }
                        break;

                }
            }
        }
        
        public string CreateTriggerConnection(Report report)
        {
            string triggerDbCnStr;

            if (report.triggerDatabaseUsername != null) // User credentials have been specified.
            {
                triggerDbCnStr =
                  "User ID=" + report.triggerDatabaseUsername + "; " +
                  "Password=" + report.triggerDatabasePassword + "; " +
                  "Data Source=" + report.triggerDatabaseServer + "; " +
                  "Initial Catalog=" + report.triggerDatabase + ";";
            }
            else  // Application user context is trusted, no need for user credentials.
            {
                triggerDbCnStr =
                  "Trusted_Connection=yes; " +
                  "Data Source=" + report.triggerDatabaseServer + "; " +
                  "Initial Catalog=" + report.triggerDatabase + ";";
            }

            return triggerDbCnStr;
        }
        
        public string CreateConnection(Report report)
        {
            string dbCnStr;

            if (report.databaseUsername != null) // User credentials have been specified.
            {
                dbCnStr =
                  "User ID=" + report.databaseUsername + "; " +
                  "Password=" + report.databasePassword + "; " +
                  "Data Source=" + report.databaseServer + "; " +
                  "Initial Catalog=" + report.database + ";";
            }
            else  // Application user context is trusted, no need for user credentials.
            {
                dbCnStr =
                  "Trusted_Connection=yes; " +
                  "Data Source=" + report.databaseServer + "; " +
                  "Initial Catalog=" + report.database + ";";
            }

            return dbCnStr;
        }

        public bool TestTrigger(Report report)
        {
            string triggerValue1 = "";
            string triggerValue2 = "";

            triggerDbCn = new SqlConnection(CreateTriggerConnection(report));

            switch (report.triggerDateMethod)
            {
                case ("KEYVALUE"):
                    triggerValue1 = KeyValue.Get(report.triggerDate, "", triggerDbCn);
                    break;

                default:
                    break;
            }

            switch (report.triggerDateType)
            {
                case ("TODAY"):
                    triggerValue2 = KeyValue.Get("BizDate", "", triggerDbCn);
                    break;
            }


            if (triggerValue1.Equals(triggerValue2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool TestCreationTime(Report report)
        {
            if (report.creationTime != null)
            {
                if (DateTime.Now >= DateTime.Parse(report.creationTime))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public DataView LoadReportData(ref Report report)
        {
            DataSet dsResults = new DataSet();
            SqlConnection localDbCn = new SqlConnection(CreateConnection(report));
            SqlCommand dbCmd = null;


            switch (report.spType.ToUpper())
            {
                case ("STOREDPROCEDURE"):
                    dbCmd = new SqlCommand(report.sp, localDbCn);
                    dbCmd.CommandType = CommandType.StoredProcedure;
                    break;

                case ("TEXT"):
                    Replace(ref report.sp);
                    dbCmd = new SqlCommand(report.sp, localDbCn);
                    dbCmd.CommandType = CommandType.Text;
                    break;

                default:
                    break;
            }

            SqlDataAdapter dAdapter = new SqlDataAdapter(dbCmd);
            dAdapter.Fill(dsResults, "Results");

            FormatFilter(ref report.filterResults);

            DataView dvResults = new DataView(dsResults.Tables["Results"], report.filterResults.ToUpper(), "", DataViewRowState.CurrentRows);
            return dvResults;
        }

        public void FormatFilter(ref string filterResults)
        {
            filterResults = filterResults.Replace("LessThan", "<");
            filterResults = filterResults.Replace("LessThanEqual", "<=");
            filterResults = filterResults.Replace("GreaterThan", ">");
            filterResults = filterResults.Replace("GreaterThanEqual", ">=");
            filterResults = filterResults.Replace("Equal", "=");
        }

        public void Replace(ref string text)
        {
            text = text.Replace("%ContractsBizDate", Master.ContractsBizDate);
            text = text.Replace("%BizDatePrior%", Master.BizDatePrior);
            text = text.Replace("%BizDate%", Master.BizDate);
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
