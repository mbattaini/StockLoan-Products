using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using StockLoan.Common;
using StockLoan.MainBusiness;

namespace StockLoan.PushData
{
	public class PushData
	{
		private string countryCode;

		private string dbCnStr;
		private SqlConnection dbCn = null;

		private string externalDbCnStr;
		private SqlConnection externalDbCn = null;

		private Thread mainThread = null;

		private static bool isStopped = true;
		private static string tempPath;

        private Reports reports;

		public PushData(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;			

			try
			{
				dbCn = new SqlConnection(dbCnStr);				
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [PushData.PushData]", Log.Error, 1);
			}

			countryCode = Standard.ConfigValue("CountryCode", "US");
			Log.Write("Using country code: " + countryCode + " [PushData.PushData]", 2);

			if (Standard.ConfigValueExists("TempPath"))
			{
				tempPath = Standard.ConfigValue("TempPath");

				if (!Directory.Exists(tempPath))
				{
					Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [PushData.PushData]", Log.Error, 1);
					tempPath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
				Log.Write("A configuration value for TempPath has not been provided. [PushData.PushData]", Log.Information, 1);
				tempPath = Directory.GetCurrentDirectory();
			}

			Log.Write("Temporary files will be staged at " + tempPath + ". [PushData.PushData]", 2);
            reports = new Reports(dbCnStr);
		}

		~PushData()
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
				mainThread = new Thread(new ThreadStart(PushDataLoop));
				mainThread.Name = "Main";
				mainThread.Start();

				Log.Write("Start command issued with new main thread. [PushData.Start]", 3);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Start command issued with main thread already running. [PushData.Start]", 3);
			}
		}

		public void Stop()
		{
			isStopped = true;

			if (mainThread == null)
			{
				Log.Write("Stop command issued, main thread never started. [PushData.Stop]", 3);
			}
			else if (mainThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				mainThread.Abort();
				Log.Write("Stop command issued, sleeping main thread aborted. [PushData.Stop]", 3);
			}
			else
			{
				Log.Write("Stop command issued, main thread is still active. [PushData.Stop]", 3);
			}
		}

		private void PushDataLoop()
		{
			while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
			{
				Log.Write("Start of cycle. [PushData.PushDataLoop]", 2);
				KeyValue.Set("PushDataCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);

                Master.BizDate = KeyValue.Get("BizDate", "", dbCn);
                Master.BizDatePrior = KeyValue.Get("BizDatePrior", "", dbCn);
                Master.ContractsBizDate = KeyValue.Get("ContractsBizDate", "", dbCn);

                ReportsRoll();
                if (isStopped) break;

                ReportsProcessing();
                if (isStopped) break;

				KeyValue.Set("PushDataCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
				Log.Write("End of cycle. [PushData.PushDataLoop]", 2);

				if (!isStopped)
				{
					Thread.Sleep(RecycleInterval());
				}
			}
		}


        private void ReportsRoll()
        {
            int count = 0;

            try
            {
                if (!KeyValue.Get("PushReportBizDateRoll", "", dbCnStr).Equals(Master.ContractsBizDate))
                {                    
                    count = reports.ReportsRoll(Master.BizDatePrior, Master.ContractsBizDate);
                    KeyValue.Set("PushReportBizDateRoll", Master.ContractsBizDate, dbCnStr);

                    Log.Write("Rolled " + count.ToString("#,##0") + " reports. [PushData.ReportsRoll]", 1);
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message + "[PushData.ReportsRoll]", 1);
            }
        }
        
        private void ReportsProcessing()
        {
            DataSet dsReportsRequests = new DataSet();
            ReportItem reportItem = new ReportItem();
            ReportRequestItem reportRequestItem = new ReportRequestItem();

            string fileName;

            dsReportsRequests = reports.ReportRequestsGet(Master.ContractsBizDate, 0);

            foreach (DataRow dr in dsReportsRequests.Tables["ReportRequests"].Rows)
            {
                reportRequestItem.bizDate = dr["BizDate"].ToString();
                reportRequestItem.reportId = dr["ReportId"].ToString();
                reportRequestItem.recipient = dr["Recipient"].ToString();
                reportRequestItem.reportType = dr["ReportType"].ToString();
                reportRequestItem.recipientEmail = dr["RecipientEmail"].ToString();
                reportRequestItem.bizDateStartParam = dr["BizDateStartParam"].ToString();
                reportRequestItem.bizDateStopParam = dr["BizDateStopParam"].ToString();
                reportRequestItem.bookGroupParam = dr["BookGroupParam"].ToString();
                reportRequestItem.emailDate = dr["EmailDate"].ToString();
                reportRequestItem.status = dr["Status"].ToString();
                reportRequestItem.statusMessage = dr["StatusMessage"].ToString();
                
                if (dr["EmailDate"].ToString().Equals(""))
                {
                    reportItem = ReportsParameters(dr["ReportId"].ToString());
                    fileName = ReportsGenerate(reportItem, reportRequestItem);                    
                    
                }
            }
        }


        private ReportItem ReportsParameters(string ReportId)
        {
            DataSet dsReports = new DataSet();
            ReportItem reportItem = new ReportItem();

            dsReports = reports.ReportsGet();

            foreach (DataRow dr in dsReports.Tables["Reports"].Rows)
            {
                if (dr["ReportId"].ToString().Equals(ReportId))
                {
                    reportItem.reportId = dr["ReportId"].ToString();
                    reportItem.reportName = dr["ReportName"].ToString();
                    reportItem.isBizDateStartParamUsed = bool.Parse(dr["IsBizDateStartParamUsed"].ToString());
                    reportItem.isBizDateStopParamUsed = bool.Parse(dr["IsBizDateStopParamUsed"].ToString());
                    reportItem.isBookGroupParamUsed = bool.Parse(dr["IsBookGroupParamUsed"].ToString());
                    reportItem.report_sp = dr["Report_sp"].ToString();
                }
            }

            return reportItem;
        }

        private string ReportsGenerate(ReportItem reportItem, ReportRequestItem reportRequestItem)
        {
            string fileName;
            ContractSummaryDocument cs;

            switch (reportRequestItem.reportId)
            {
                case "CRT_SECURITY":
                    cs = new ContractSummaryDocument(dbCnStr, "", reportRequestItem.bizDateStartParam, reportRequestItem.bookGroupParam);
                    cs.DataGet();
                    fileName = cs.ContentGet_ContractBySecurity();
                    break;

                case "CRT_CNTRPARTY":
                    cs = new ContractSummaryDocument(dbCnStr, "", reportRequestItem.bizDateStartParam, reportRequestItem.bookGroupParam);
                    cs.DataGet();
                    fileName = cs.ContentGet_ContractByCounterParty();
                    break;

                default:
                    fileName = "";
                    break;
            }

            return fileName;
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
				recycleInterval = KeyValue.Get("PushDataLoopRecycleIntervalBizDay", "0:15", dbCn);
			}
			else
			{
				recycleInterval = KeyValue.Get("PushDataLoopRecycleIntervalNonBizDay", "4:00", dbCn);
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
					KeyValue.Set("PushDataRecycleIntervalBizDay", "0:15", dbCn);
					hours = 0;
					minutes = 30;
					timeSpan = new TimeSpan(hours, minutes, 0);
					Log.Write("PushDataRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [PushData.RecycleInterval]", Log.Error, 1);
				}
				else
				{
					KeyValue.Set("PushDataRecycleIntervalNonBizDay", "4:00", dbCn);
					hours = 6;
					minutes = 0;
					timeSpan = new TimeSpan(hours, minutes, 0);
					Log.Write("PushDataRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [PushData.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("PushData will recycle in " + hours + " hours, " + minutes + " minutes. [PushData.RecycleInterval]", 2);
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

