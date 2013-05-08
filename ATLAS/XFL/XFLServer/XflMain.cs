using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

using StockLoan.Common;

namespace StockLoan.Xfl
{
    public class XflMain
    {
        private bool isStopped;
        private string senderoDbCnStr;
        private string xflDbCnStr;
        private string countryCode;
        private Thread mainThread;

        // private DataSet dsShortInterest;    //DChen originally named: ShortInterestdataSet 

        public XflMain(string senderoDbCnStr, string xflDbCnStr)
        {
            this.senderoDbCnStr = senderoDbCnStr;
            this.xflDbCnStr = xflDbCnStr;

            countryCode = Standard.ConfigValue("CountryCode", "US");
            Log.Write("Using country code: " + countryCode + " [XflMain.XflMain]", 2);      
        }

        public void Start()
        {
            isStopped = false;

            if ((mainThread == null) || (!mainThread.IsAlive)) // Must create new thread.
            {
                mainThread = new Thread(new ThreadStart(XflMainLoop));
                mainThread.Name = "Main";
                mainThread.Start();

                Log.Write("Start command issued with new main thread. [XflMain.Start]", 3);
            }
            else // Old thread will be just fine.
            {
                Log.Write("Start command issued with main thread already running. [XflMain.Start]", 3);
            }
        }

        public void Stop()
        {
            isStopped = true;

            if (mainThread == null)
            {
                Log.Write("Stop command issued, main thread never started. [XflMain.Stop]", 3);
            }
            else if (mainThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
            {
                mainThread.Abort();
                Log.Write("Stop command issued, sleeping main thread aborted. [XflMain.Stop]", 3);
            }
            else
            {
                Log.Write("Stop command issued, main thread is still active. [XflMain.Stop]", 3);
            }
        }


        private void XflMainLoop()
        {

            while (!isStopped)  // Loop through this block (otherwise exit method and thread dies).
            {
                Log.Write("Start of cycle. [XflMain.XflMainLoop]", 2);
                KeyValue.Set("XflMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), senderoDbCnStr);

                try
                {
                    BizDatesSet();

                    // Check CompuStat Xpressfeed Loader Daily Transaction Process 
                    XpressFeedLoaderTransactionCheck();
                    if (isStopped) break;

                    // Import Short Interest Data from XFL dB to Sendero dB 
                    ShortInterestDataImport();
                    if (isStopped) break;

                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [XflMain.XflMainLoop]", Log.Error, 1);
                }


                KeyValue.Set("XflMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), senderoDbCnStr);
                Log.Write("End of cycle. [XflMain.XflMainLoop]", 2);

                try
                {
                    if (!isStopped)
                    {
                        Thread.Sleep(RecycleInterval());
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [XflMain.XflMainLoop]", Log.Error, 1);
                }
            }   // While
        }


        private void BizDatesSet()
        {
            Master.BizDate = KeyValue.Get("BizDate", "2001-01-01", senderoDbCnStr);
            Master.BizDatePrior = KeyValue.Get("BizDatePrior", "2001-01-01", senderoDbCnStr);
        }


        private void XpressFeedLoaderTransactionCheck()
        {
            // Check CompuStat Xpressfeed Loader Daily Transaction Process Status for current BizDate 

            string transactionCheckTime = null;
            SqlConnection dbCn = null;
            SqlCommand dbCmd = null;

            if ( KeyValue.Get("XflLoaderTransactionDate", "", senderoDbCnStr).Equals(Master.BizDate) &&
                 KeyValue.Get("XflLoaderTransactionStatus", "", senderoDbCnStr).Equals("OK") )
            {
                Log.Write("XFL Loader daily transaction already completed, for BizDate " + Master.BizDate + ". [XflMain.XpressFeedLoaderTransactionCheck]", 2);
                return;
            }

            transactionCheckTime = KeyValue.Get("XflLoaderTransactionCheckTime", "10:00", senderoDbCnStr);

            try
            {
                if (transactionCheckTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) < 0)
                {
                    Log.Write("Checking XFL Loader daily transaction process status for BizDate " + Master.BizDate + ". [XflMain.XpressFeedLoaderTransactionCheck]", 2);

                    dbCn = new SqlConnection(xflDbCnStr);
                    dbCmd = new SqlCommand("dbo.spXflDailyTransactionStatusGet", dbCn);
                    dbCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = Master.BizDate;

                    SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.Bit);
                    paramStatus.Direction = ParameterDirection.Output;

                    dbCn.Open();
                    dbCmd.ExecuteNonQuery();
                    dbCn.Close();

                    if (paramStatus.Value!=null && (bool)paramStatus.Value)
                    {
                        KeyValue.Set("XflLoaderTransactionDate", Master.BizDate, senderoDbCnStr);
                        KeyValue.Set("XflLoaderTransactionStatus", "OK", senderoDbCnStr);
                        Log.Write("XFL Loader daily transaction process completed for BizDate " + Master.BizDate + ". [XflMain.XpressFeedLoaderTransactionCheck]", 2);
                    }
                    else
                    {
                        KeyValue.Set("XflLoaderTransactionDate", Master.BizDate, senderoDbCnStr);
                        KeyValue.Set("XflLoaderTransactionStatus", "WAIT", senderoDbCnStr);
                        Log.Write("XFL Loader daily transaction process not yet complete for BizDate " + Master.BizDate + ". [XflMain.XpressFeedLoaderTransactionCheck]", 2);
                    }
                }
                else
                {
                    Log.Write("Not ready to check XFL Loader transaction process yet, must wait until after " + transactionCheckTime + "UTC time. [XflMain.XpressFeedLoaderTransactionCheck]", 2);
                }

            }
            catch (Exception ex)
            {
                KeyValue.Set("XflLoaderTransactionDate", Master.BizDate, senderoDbCnStr);
                KeyValue.Set("XflLoaderTransactionStatus", ex.Message, senderoDbCnStr);
                Log.Write(ex.Message + "  [XflMain.XpressFeedLoaderTransactionCheck]", Log.Error, 1);
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                    dbCn.Close();
            }

        }


        private void ShortInterestDataImport()
        {   
            string importMakeTime = null;               //DChen 
            SqlConnection dbCn = null;                  //DChen 
            SqlCommand dbCmd = null;                    //DChen 
            SqlDataAdapter dataAdapter = null;          //DChen 
            SqlBulkCopy dbBulk = null;                  //DChen 
            DataSet dsShortInterest = null;             //DChen 

             
            // Check if Short Interest data already imported, from XFL dB to Sendero dB 
            if (KeyValue.Get("XflShortInterestDataDate", "", senderoDbCnStr).Equals(Master.BizDatePrior ) &&
                KeyValue.Get("XflShortInterestImportDate", "", senderoDbCnStr).Equals(Master.BizDate) &&
                KeyValue.Get("XflShortInterestImportStatus", "", senderoDbCnStr).Equals("OK"))
            {
                Log.Write("Xfl Short Interest data already imported from XFL dB to Sendero dB, for BizDate " + Master.BizDate + ". [XflMain.ShortInterestDataImport]", 2);
                return;
            }

            importMakeTime = KeyValue.Get("XflShortInterestImportMakeTime", "10:30", senderoDbCnStr);

            // Check if Short Interest data is ready to be import from XFL dB to Sendero dB 
            if (KeyValue.Get("XflLoaderTransactionDate", "", senderoDbCnStr).Equals(Master.BizDate) &&
                KeyValue.Get("XflLoaderTransactionStatus", "", senderoDbCnStr).Equals("OK") && 
                importMakeTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) < 0) 
            {
                Log.Write("Start importing Xfl Short Interest data for BizDate " + Master.BizDate + ". [XflMain.ShortInterestDataImport]", 2);

                dsShortInterest = new DataSet();

                // Get Short Interest data from XFL database 
                try
                {
                    KeyValue.Set("XflShortInterestDataDate", Master.BizDatePrior, senderoDbCnStr);
                    KeyValue.Set("XflShortInterestImportDate", Master.BizDate, senderoDbCnStr);

                    dbCn = new SqlConnection(xflDbCnStr);
                    dbCmd = new SqlCommand("dbo.spXflShortInterestGet", dbCn);
                    dbCmd.CommandType = CommandType.StoredProcedure;
                    dbCmd.CommandTimeout = 600;         //DChen give the proc up to 10 minutes to execute, typically done within 3 min.

                    SqlParameter paramBizDatePrior = dbCmd.Parameters.Add("@BizDatePrior", SqlDbType.DateTime);
                    paramBizDatePrior.Value = Master.BizDatePrior;

                    dataAdapter = new SqlDataAdapter(dbCmd);
                    dataAdapter.Fill(dsShortInterest, "ShortInterest");

                    if (dsShortInterest == null || dsShortInterest.Tables.Count == 0 || dsShortInterest.Tables["ShortInterest"].Rows.Count == 0)
                    {
                        KeyValue.Set("XflShortInterestImportStatus", "No Data", senderoDbCnStr);
                        Log.Write("No Short Interest data returned.  [XflMain.ShortInterestDataImport]", 2);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    KeyValue.Set("XflShortInterestImportStatus", ex.Message, senderoDbCnStr);
                    Log.Write(ex.Message + "  [XflMain.ShortInterestDataImport]", Log.Error, 1);
                    throw;
                }
                finally
                {
                    if (dbCn.State != ConnectionState.Closed)
                        dbCn.Close();
                }

                // Bulk Insert Short Interest data into Sendero database 
                try
                {
                    Log.Write("Short Interest data date is   " + Master.BizDatePrior + ". [XflMain.ShortInterestDataImport]", 2);
                    Log.Write("Short Interest import date is " + Master.BizDate + ". [XflMain.ShortInterestDataImport]", 2);

                    // Reset the tbNSShortInterest (current) and tbNSSHortInterestHistory tables in Sendero 
                    dbCn = new SqlConnection(senderoDbCnStr);
                    dbCmd = new SqlCommand("dbo.spNSShortInterestPurge", dbCn);
                    dbCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                    paramTradeDate.Value = Master.BizDatePrior;

                    dbCn.Open();
                    dbCmd.ExecuteNonQuery();
                    dbCn.Close();

                    // Bulk Copy to Current table 
                    dbBulk = new SqlBulkCopy(senderoDbCnStr);
                    dbBulk.DestinationTableName = "dbo.tbNSShortInterest";
                    dbBulk.WriteToServer(dsShortInterest.Tables["ShortInterest"]);
                    Log.Write("Total of " + dsShortInterest.Tables["ShortInterest"].Rows.Count.ToString() + " rows inserted into " + dbBulk.DestinationTableName + ".  [XflMain.ShortInterestDataImport]", 2);
                    dbBulk.Close();

                    // Bulk Copy to History table 
                    dbBulk = new SqlBulkCopy(senderoDbCnStr);
                    dbBulk.DestinationTableName = "dbo.tbNSShortInterestHistory";
                    dbBulk.WriteToServer(dsShortInterest.Tables["ShortInterest"]);
                    Log.Write("Total of " + dsShortInterest.Tables["ShortInterest"].Rows.Count.ToString() + " rows inserted into " + dbBulk.DestinationTableName + ".  [XflMain.ShortInterestDataImport]", 2);
                    dbBulk.Close();

                    KeyValue.Set("XflShortInterestImportStatus", "OK", senderoDbCnStr);
                    Log.Write("Short Interest import completed, for BizDate " + Master.BizDate + ". [XflMain.ShortInterestDataImport]", 2);
                }
                catch (Exception ex)
                {
                    KeyValue.Set("XflShortInterestImportStatus", ex.Message, senderoDbCnStr);
                    Log.Write(ex.Message + "  [XflMain.ShortInterestDataImport]", Log.Error, 1);
                    throw;
                }
                finally
                {
                    if (dbCn.State != ConnectionState.Closed)
                        dbCn.Close();
                }

            } // if ShortInterest data ready for import 

        }



		private TimeSpan RecycleInterval()
		{
			string recycleInterval;
			string [] values;

			int hours;
			int minutes;

            bool isBizDay = Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Any, senderoDbCnStr);
			TimeSpan timeSpan;

			char [] delimiter = new char[1];
			delimiter[0] = ':';

			if (isBizDay)
			{
                recycleInterval = KeyValue.Get("XflMainLoopRecycleIntervalBizDay", "0:15", senderoDbCnStr);
			}
			else
			{
                recycleInterval = KeyValue.Get("XflMainLoopRecycleIntervalNonBizDay", "4:00", senderoDbCnStr);
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
                    KeyValue.Set("XflMainLoopRecycleIntervalBizDay", "0:15", senderoDbCnStr);
					hours = 0;
					minutes = 30;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("XflMainLoopRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [XflMain.RecycleInterval]", Log.Error, 1);
				}
				else
				{
                    KeyValue.Set("XflMainLoopRecycleIntervalNonBizDay", "4:00", senderoDbCnStr);
					hours = 6;
					minutes = 0;
					timeSpan = new TimeSpan (hours, minutes, 0);
                    Log.Write("XflMainLoopRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [XflMain.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("XflMain will recycle in " + hours + " hours, " + minutes + " minutes. [XflMain.RecycleInterval]", 2);
			return timeSpan;
		}




        private void XflMainLoop_Original_by_Lwu()
        {  /* 
            countryCode = Standard.ConfigValue("CountryCode", "US");

            while (!isStopped)  // Loop through this block (otherwise exit method and thread dies).
            {
                Log.Write("Start of cycle. [XflMain.XflMainLoop]", 2);
                KeyValue.Set("XflMainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), senderoDbCnStr);

                try
                {
                    BizDatesSet();
                    if (KeyValue.Get("XflShortInterestImportDate", "", senderoDbCnStr).Equals(Master.BizDate) == false)
                    {
                        string dataMakeTime = KeyValue.Get("XflShortInterestMakeTime", "10:00", senderoDbCnStr);
                        if (dataMakeTime.CompareTo(DateTime.UtcNow.ToString(Standard.TimeShortFormat)) < 0)
                        {
                            if (IsDataReady())
                            {
                                Log.Write("Xfl Loader daily transaction process is completed on " + Master.BizDate + ". [XflMain.XflMainLoop]", 2);
                                Log.Write("Will import Short Interest data for " + Master.BizDatePrior + ". [XflMain.XflMainLoop]", 2);

                                dsShortInterest = new DataSet();
                                GetShortInterestData();
                                if (ImportShortInterestData())
                                {
                                    KeyValue.Set("XflShortInterestDataDate", Master.BizDatePrior, senderoDbCnStr);
                                    KeyValue.Set("XflShortInterestImportDate", Master.BizDate, senderoDbCnStr);
                                    Log.Write("Short Interest Data Date is set to " + Master.BizDatePrior + ". [XflMain.XflMainLoop]", 2);
                                    Log.Write("Short Interest Import Date is set to " + Master.BizDate + ". [XflMain.XflMainLoop]", 2);
                                }
                            }
                            else
                            {
                                Log.Write("Xfl Loader daily transaction process is not completed on " + Master.BizDate + "" + ". [XflMain.XflMainLoop]", 2);
                            }
                        }
                        else
                        {
                            Log.Write("Must wait until after " + dataMakeTime + " UTC to import Short Interest data for " + Master.BizDatePrior + ". [XflMain.XflMainLoop]", Log.Information, 2);
                        }
                    }
                    else
                    {
                        Log.Write("Short Interest Data Date is " + Master.BizDatePrior + ". [XflMain.XflMainLoop]", 2);
                        Log.Write("Short Interest Import already done for " + Master.BizDate + ". [XflMain.XflMainLoop]", 2);
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [XflMain.XflMainLoop]", Log.Error, 1);
                }


                KeyValue.Set("XflMainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), senderoDbCnStr);
                Log.Write("End of cycle. [XflMain.XflMainLoop]", 2);

                try
                {
                    Thread.Sleep(RecycleInterval());
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message + " [XflMain.XflMainLoop]", Log.Error, 1);
                }

            } //while loop 
        */
        }
        
        private bool ImportShortInterestData_Original_by_Lwu()
        { 
            bool isImport = false;
            /* 
            try
            {
                if (shortInterestDataSet == null || shortInterestDataSet.Tables.Count == 0)
                {
                    return false;
                }

                if( shortInterestDataSet.Tables["ShortInterest"].Rows.Count == 0)
                {
                    Log.Write("ShortInterest table has zero rows. [XflMain.ImportShortInterestData]", 2);
                    return false;
                }

                // Cleaning the tbNSShortInterest table
                using (SqlConnection dbCn = new SqlConnection(senderoDbCnStr))
                {
                    using (SqlCommand dbCmd = new SqlCommand("dbo.spNSShortInterestPurge", dbCn))
                    {
                        dbCmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter paramTradeDate = new SqlParameter("@TradeDate", SqlDbType.DateTime);
                        paramTradeDate.Value = Master.BizDatePrior;

                        dbCmd.Parameters.Add(paramTradeDate);

                        dbCn.Open();
                        dbCmd.ExecuteNonQuery();
                        dbCn.Close();
                    }
                }

                using (SqlBulkCopy bulk = new SqlBulkCopy(senderoDbCnStr))
                {
                    bulk.DestinationTableName = "dbo.tbNSShortInterest";
                    bulk.WriteToServer(shortInterestDataSet.Tables["ShortInterest"]);
                    Log.Write("Total "+shortInterestDataSet.Tables["ShortInterest"].Rows.Count.ToString()+" rows insert into "+bulk.DestinationTableName + ".[XflMain.ImportShortInterestData]", 2);
                }
                                
                using (SqlBulkCopy bulk = new SqlBulkCopy(senderoDbCnStr))
                {
                    bulk.DestinationTableName = "dbo.tbNSShortInterestHistory";
                    bulk.WriteToServer(shortInterestDataSet.Tables["ShortInterest"]);
                    Log.Write("Total " + shortInterestDataSet.Tables["ShortInterest"].Rows.Count.ToString() + " rows insert into " + bulk.DestinationTableName + ".[XflMain.ImportShortInterestData]", 2);
                }
                isImport = true;

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + "[XflMain.ImportShortInterestData]", Log.Error, 1);
            }

            */
            return isImport;
        }

        private bool IsDataReady_Original_by_Lwu()
        {
            bool isReady = false;
            /* //DChen
            try
            {
                using (SqlConnection dbCn = new SqlConnection(this.xflDbCnStr))
                {
                    using (SqlCommand dbCmd = new SqlCommand("dbo.spXflDailyTransactionStatusGet", dbCn))
                    {
                        dbCmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter paramBizDate = new SqlParameter("@BizDate", SqlDbType.DateTime);
                        paramBizDate.Value = Master.BizDate;
                        dbCmd.Parameters.Add(paramBizDate);

                        SqlParameter paramStatus = new SqlParameter("@Status", SqlDbType.Bit);
                        paramStatus.Direction = ParameterDirection.Output;
                        dbCmd.Parameters.Add(paramStatus);

                        dbCn.Open();
                        dbCmd.ExecuteNonQuery();
                        dbCn.Close();
                        if (paramStatus.Value.Equals(DBNull.Value) == false)
                            isReady = (bool)paramStatus.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [XflMain.IsDataReady]", Log.Error, 1);
            }
            */ 
            return isReady;
        }

        private void GetShortInterestData_Original_by_Lwu()
        {   /* //DChen 
            try
            {
                using (SqlConnection dbCn = new SqlConnection(xflDbCnStr))
                {
                    using (SqlCommand dbCmd = new SqlCommand("dbo.spXflShortInterestGet", dbCn))
                    {
                        dbCmd.CommandType = CommandType.StoredProcedure;
                        dbCmd.CommandTimeout = 600;

                        SqlParameter paramBizDatePrior = new SqlParameter("@BizDatePrior", SqlDbType.DateTime);
                        paramBizDatePrior.Value = Master.BizDatePrior;
                        dbCmd.Parameters.Add(paramBizDatePrior);

                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
                        dataAdapter.SelectCommand = dbCmd;
                        dataAdapter.Fill(shortInterestDataSet, "ShortInterest");
                        if (shortInterestDataSet == null|| shortInterestDataSet.Tables.Count == 0)
                        {
                            Log.Write("No Short Interest data returned. [XflMain.GetShortInterestData]", 2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [XflMain.GetShortInterestData]", Log.Error, 1);
            }
            */
        }
 
    }
}
