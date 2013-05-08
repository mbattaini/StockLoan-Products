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
using StockLoan.Transport;                              


namespace StockLoan.InventoryService
{   
    public class InventoryServiceMain  
	{
		private string dbCnStr;
        private SqlConnection dbCn;

        private Thread workerThread = null;
        private static bool isStopped = true;
        private static string tempPath;
        private static string fileArchivePath;          //Archive Root Path

		public InventoryServiceMain(string dbCnStr) 
		{
			this.dbCnStr = dbCnStr;
						
			try
			{
				dbCn = new SqlConnection(dbCnStr);    				
			}
			catch (Exception e) 
			{
				Log.Write(e.Message + " [InventoryMain.InventoryServiceMain]", Log.Error, 1);
			}

			if (Standard.ConfigValueExists("TempPath"))
			{
				tempPath = Standard.ConfigValue("TempPath");

				if (!Directory.Exists(tempPath))
				{
                    Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [InventoryMain.InventoryServiceMain]", Log.Error, 1);
					tempPath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
                Log.Write("A configuration value for TempPath has not been provided. [InventoryMain.InventoryServiceMain]", Log.Information, 1);
				tempPath = Directory.GetCurrentDirectory();
			}

            if (Standard.ConfigValueExists("FileArchivePath"))
            {
                fileArchivePath = Standard.ConfigValue("FileArchivePath");

                if (!Directory.Exists(fileArchivePath))
                {
                    Log.Write("The configuration value for FileArchivePath, " + fileArchivePath + ", does not exist. [InventoryMain.InventoryServiceMain]", Log.Error, 1);
                    fileArchivePath = Directory.GetCurrentDirectory();
                }
            }
            else
            {
                Log.Write("A configuration value for FileArchivePath has not been provided. [S3Main.S3Main]", Log.Information, 1);
                fileArchivePath = Directory.GetCurrentDirectory();
            }

            Log.Write("Temporary files will be staged at " + tempPath + "  [InventoryMain.InventoryServiceMain]", Log.Information, 2);
            Log.Write("Inventory files will be archived under " + fileArchivePath + "  [InventoryMain.InventoryServiceMain]", Log.Information, 2);
		}

		~InventoryServiceMain()
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
				workerThread = new Thread(new ThreadStart(InventoryServiceMainLoop));
				workerThread.Name = "Worker";
				workerThread.Start();

                Log.Write("Start command issued with new worker thread. [InventoryMain.Start]", 2);
			}
			else // Old thread will be just fine.
			{
                Log.Write("Start command issued with worker thread already running. [InventoryMain.Start]", 2);
			}
		}

		public void Stop()
		{
			isStopped = true;

			if (workerThread == null)
			{
				Log.Write("Stop command issued, worker thread never started. [InventoryMain.Stop]", 2);
			}
			else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				workerThread.Abort();
				Log.Write("Stop command issued, sleeping worker thread aborted. [InventoryMain.Stop]", 2);
			}
			else
			{
				Log.Write("Stop command issued, worker thread is still active. [InventoryMain.Stop]", 2);
			}
		}
		
        private void InventoryServiceMainLoop()
		{
			while (!isStopped) 
			{
                Log.Write("Start-of-cycle. [InventoryMain.InventoryServiceMainLoop]", 2);


                InventorySubscriptionsLoad("", "");         //param: BooGroup, BizDate
                if (isStopped) break;

                Log.Write("End-of-cycle. [InventoryMain.InventoryServiceMainLoop]", 2);

                if (!isStopped)
				{
					Thread.Sleep(RecycleInterval());
				}
			}
		}

        private void InventorySubscriptionsLoad(string currBookGroup, string currBizDate)
        {
            int rowCountBookGroups = 0;
            int rowCountInventorySubscriptions = 0;
            int rowCountInventoryItemLoaded = 0;

            string currBizDatePrior = "";
            string currBizDateNext = "";
            string currBizDateDesk = "";                //BizDate for each specific desk (differs depending on [LoadBizDatePrior] flag, for JPM.US.C BizDatePrior handling) 

            string currDesk = "";
            string currInventoryType = "";
            string lastLoadedBizDate = "";
            string currLoadTime = "";
            string currLoadStatus = "";
            int    currInventoryItemCount = 0;        
            string currLastLoadedTime = "";
            int    currLastLoadedVersion = 0;
            bool   currLoadBizDatePrior;
            string currFileTime = "";
            string currFileCheckTime = "";
            string currFileStatus = "";
            string currFileName = "";
            string currFileHost = "";
            string currFileUserId = "";
            string currFilePassword = "";
            string currFileNameOnly = "";               

            DataSet dsBookGroups = new DataSet();
            DataSet dsInventorySubscriptions = new DataSet();
            FileTransfer fileTransfer = new FileTransfer("");       
            FileResponse fResponse = new FileResponse();            

            Log.Write("Loading Inventory Subscriptions...   [InventoryMain.InventorySubsciptionsLoad]", Log.Information, 2);  

            try
            {
                dsBookGroups = DBBooks.BookGroupsGet(currBookGroup, currBizDate);
                rowCountBookGroups = dsBookGroups.Tables["BookGroups"].Rows.Count;
                Log.Write("BookGroups to Load = " + rowCountBookGroups.ToString() + "  [InventoryMain.InventorySubscriptionsLoad]", Log.Information, 2);

                foreach (DataRow drBG in dsBookGroups.Tables["BookGroups"].Rows)
                {
                    try
                    {
                        currBookGroup = drBG["BookGroup"].ToString();
                        currBizDate = DateTime.Parse(drBG["BizDate"].ToString()).ToString("yyyy-MM-dd");
                        currBizDateNext = DateTime.Parse(drBG["BizDateNext"].ToString()).ToString("yyyy-MM-dd");
                        currBizDatePrior = DateTime.Parse(drBG["BizDatePrior"].ToString()).ToString("yyyy-MM-dd");
                        Log.Write("BookGroup[" + currBookGroup + "] BizDate: " + currBizDate + ", BizDateNext: " + currBizDateNext + ", BizDatePrior: " + currBizDatePrior + ".  [InventoryMain.InventorySubscriptionsLoad]", Log.Information, 2);

                        if (currBizDate.Equals(DateTime.Now.ToString("yyyy-MM-dd")))
                        {
                            dsInventorySubscriptions = DBInventory.InventorySubscriptionsGet(currBookGroup, "", "", 0);
                            rowCountInventorySubscriptions = dsInventorySubscriptions.Tables["InventorySubscriptions"].Rows.Count;
                            Log.Write("BookGroup[" + currBookGroup + "] Inventory Subscription Count = " + rowCountInventorySubscriptions.ToString() + "   [InventoryMain.InventorySubscriptionsLoad]", Log.Information, 2);

                            foreach (DataRow drIS in dsInventorySubscriptions.Tables["InventorySubscriptions"].Rows)
                            {
                                if (drIS["IsActive"].Equals(true))
                                {
                                    currDesk = drIS["Desk"].ToString();
                                    currInventoryType = drIS["InventoryType"].ToString();
                                    lastLoadedBizDate = drIS["BizDate"].ToString().Equals("") ? "2001-01-01" : drIS["BizDate"].ToString();
                                    currLoadTime = drIS["LoadTime"].ToString();
                                    currLoadStatus = drIS["LoadStatus"].ToString();
                                    currInventoryItemCount = int.Parse(drIS["Items"].ToString().Equals("") ? "0" : drIS["Items"].ToString());           //tbInventorySubscriptions.Items 
                                    currLastLoadedTime = drIS["LastLoadedTime"].ToString().Equals("") ? "2001-01-01" : drIS["LastLoadedTime"].ToString();
                                    currLastLoadedVersion = int.Parse(drIS["LastLoadedVersion"].ToString().Equals("") ? "0" : drIS["LastLoadedVersion"].ToString());
                                    currLoadBizDatePrior = bool.Parse(drIS["LoadBizDatePrior"].ToString());
                                    currFileTime = drIS["FileTime"].ToString();
                                    currFileCheckTime = drIS["FileCheckTime"].ToString();
                                    currFileStatus = drIS["FileStatus"].ToString();
                                    currFileName = drIS["FileName"].ToString();
                                    currFileHost = drIS["FileHost"].ToString();
                                    currFileUserId = drIS["FileUserId"].ToString();
                                    currFilePassword = drIS["FilePassword"].ToString();

                                    if (currLoadBizDatePrior.Equals(true))
                                    {
                                        currBizDateDesk = currBizDatePrior;
                                        currLastLoadedVersion = (DateTime.Parse(lastLoadedBizDate) < DateTime.Parse(currBizDatePrior)) ? 0 : currLastLoadedVersion;
                                    }
                                    else
                                    {
                                        currBizDateDesk = currBizDate;
                                        currLastLoadedVersion = (DateTime.Parse(lastLoadedBizDate) < DateTime.Parse(currBizDate)) ? 0 : currLastLoadedVersion;
                                    }

                                    currFileHost = currFileHost.ToUpper().Equals("LOCALHOST") ? "" : currFileHost;
                                    currFileName = currFileName.Replace("{yyyyMMdd}", currBizDate.Replace("-", "").Replace("/", ""));
                                    char[] charPath = { '/', '\\' };
                                    currFileNameOnly = currFileName.Substring((currFileName.LastIndexOfAny(charPath) + 1));
                                    Log.Write("Loading [" + currBookGroup + "].[" + currDesk + "].[" + currInventoryType + "].[BizDate: " + currBizDateDesk + "] Inventory (fileHost + fileName): " + currFileHost + currFileName + "   [InventoryMain.InventorySubscriptionsLoad]", Log.Information, 2);

                                    fResponse = FileArchive.Get(currBookGroup, currDesk, currLastLoadedTime, currFileHost, currFileName, currFileUserId, currFilePassword, fileArchivePath, currFileNameOnly, currLastLoadedVersion, dbCnStr);
                                    if (fResponse.comment == null) { fResponse.comment = ""; }

                                    if (fResponse.status.Equals(FileStatus.OK))
                                    {  
                                        rowCountInventoryItemLoaded = InventoryItem.LoadData(currBizDate, currBizDateDesk, currBookGroup, currDesk, currInventoryType, fResponse.fileName, currFileUserId, currFilePassword, dbCnStr);

                                        if (rowCountInventoryItemLoaded > 0)
                                        {
                                            DBInventory.InventorySubscriptionSet(currBookGroup,
                                                                                 currDesk,
                                                                                 currInventoryType,
                                                                                 currBizDateDesk,                                   // depending on [LoadBizDatePrior] flag
                                                                                 DateTime.Now.ToString(),
                                                                                 fResponse.status.ToString(),
                                                                                 rowCountInventoryItemLoaded.ToString(),
                                                                                 DateTime.Now.ToString(),
                                                                                 (currLastLoadedVersion + 1).ToString(),        // Increment LastVersionLoaded 
                                                                                 "",
                                                                                 fResponse.lastWriteTime,
                                                                                 DateTime.Now.ToString(),
                                                                                 fResponse.status.ToString(),
                                                                                 "", "", "", "", "", "", "",
                                                                                 "InventoryService",
                                                                                 true);
                                            Log.Write("Loaded " + rowCountInventoryItemLoaded.ToString() + " inventory items, Version = " + (currLastLoadedVersion + 1).ToString() + "   [InventoryMain.InventorySubscriptionsLoad]", Log.Information, 2);
                                        }
                                        else
                                        {
                                            Log.Write("No inventory item loaded for [" + currBookGroup + "].[" + currDesk + "].[" + currInventoryType + "].[" + currBizDateDesk + "]    [InventoryMain.InventorySubscriptionsLoad]", Log.Information, 2);
                                        }
                                    }
                                    else if (fResponse.status.Equals(FileStatus.Aborted) && fResponse.comment.ToUpper().Contains("FILE ALREADY DOWNLOADED"))
                                    {
                                        DBInventory.InventorySubscriptionSet(currBookGroup,
                                                                             currDesk,
                                                                             currInventoryType,
                                                                             "",                                                // currBizDateDesk: depending on [LoadBizDatePrior] flag
                                                                             "",
                                                                             "",                                                // LoadStatus: File already loaded, so maintain previous load status
                                                                             "", "", "", "", "",
                                                                             DateTime.Now.ToString(),                           // Just update FileCheckTime to reflect latest file check time
                                                                             "",                                                // FileStatus: File already loaded, so maintain previous File status
                                                                             "", "", "", "", "", "",
                                                                             fResponse.comment,
                                                                             "InventoryService",
                                                                             true);
                                        Log.Write("No Inventory Imported. Inventory File Status: " + fResponse.status.ToString() + ". File Comment: " + fResponse.comment + ".  [InventoryMain.InventorySubscriptionsLoad]", Log.Information, 2);
                                    }
                                    else
                                    {
                                        Log.Write("No Inventory Imported. Inventory File Status: " + fResponse.status.ToString() + ". File Comment: " + fResponse.comment + ".  [InventoryMain.InventorySubscriptionsLoad]", Log.Information, 2);
                                    }

                                }

                            }

                        }
                        else
                        {
                            Log.Write("BookGroup[" + currBookGroup + "] BizDate: " + currBizDate + " is Not equal to Current System BizDate: " + DateTime.Now.ToString("yyyy-MM-dd") + ".  No Inventory Import.  [InventoryMain.InventorySubscriptionsLoad]", Log.Information, 2);
                        }
                    }
                    catch { } 
                } //for Each drBG

            }
            catch (Exception err)
            {
                Log.Write("ERROR: BookGroup[" + currBookGroup + "].[" + currDesk + "].[" + currInventoryType + "].[" + currBizDate + "]: " + err.Message + ".   [InventoryMain.InventorySubscriptionsLoad]", Log.Error, 1);
            }
            finally
            {
                Log.Write("Done. Inventory Subscriptions Load process completed.   [InventoryMain.InventorySubsciptionsLoad]", Log.Information, 2);
            }

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

        private static TimeSpan UtcOffsetTimeSpan(double utcOffsetInterval)
        {
            TimeSpan timeSpan = new TimeSpan();

            try
            {
                timeSpan = TimeSpan.FromHours(utcOffsetInterval);
            }
            catch (Exception e)
            {
                Log.Write( e.Message + ".  [InventoryMain.UtcOffsetTimeSpan]", Log.Error, 1);
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
				recycleInterval = KeyValue.Get("InventoryServiceMainRecycleIntervalBizDay", "0:10", dbCn);
			}
			else
			{
                recycleInterval = KeyValue.Get("InventoryServiceMainRecycleIntervalNonBizDay", "1:00", dbCn);
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
					KeyValue.Set("InventoryServiceMainRecycleIntervalBizDay", "0:10", dbCn);
					hours = 0;
					minutes = 10;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("InventoryServiceMainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [InventoryMain.RecycleInterval]", Log.Error, 1);
				}
				else
				{
					KeyValue.Set("InventoryServiceMainRecycleIntervalNonBizDay", "1:00", dbCn);
					hours = 1;
					minutes = 0;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("InventoryServiceMainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [InventoryMain.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("InventoryServiceMain will recycle in " + hours + " hours, " + minutes + " minutes. [InventoryMain.RecycleInterval]", 2);

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
