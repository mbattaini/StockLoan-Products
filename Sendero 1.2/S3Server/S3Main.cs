using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Collections;
using Anetics.Common;
using Anetics.S3;

namespace Anetics.S3
{
	public class S3Main
	{
		private string dbCnStr;
		private string pensonDbCnStr;

		private SqlConnection dbCn = null;
		private SqlConnection pensonDbCn = null;

		private Thread workerThread = null;
		private static bool isStopped = true;
		private static string tempPath;
		private static string originalFileArchivePath;
		private static string fileArchivePath;

		private int workerThreads = 0;
		
		private ArrayList manpThreadsList;
		private ArrayList manpList;
		    	
		public S3Main(string dbCnStr, string pensonDbCnStr)
		{
			this.dbCnStr = dbCnStr;
			this.pensonDbCnStr = pensonDbCnStr;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				pensonDbCn = new SqlConnection(pensonDbCnStr);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [S3Main.S3Main]", Log.Error, 1);
			}
		
			if (Standard.ConfigValueExists("TempPath"))
			{
				workerThreads = int.Parse(Standard.ConfigValue("WorkerThreads", "1"));
				tempPath = Standard.ConfigValue("TempPath");

				if (!Directory.Exists(tempPath))
				{
					Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [S3Main.S3Main]", Log.Error, 1);
					tempPath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
				Log.Write("A configuration value for TempPath has not been provided. [S3Main.S3Main]", Log.Information, 1);
				tempPath = Directory.GetCurrentDirectory();
				workerThreads = 1;
			}

			if (Standard.ConfigValueExists("FileArchivePath"))
			{				
				originalFileArchivePath = Standard.ConfigValue("FileArchivePath");

				if (!Directory.Exists(originalFileArchivePath))
				{
					Log.Write("The configuration value for FileArchivePath, " + fileArchivePath + ", does not exist. [S3Main.S3Main]", Log.Error, 1);
					originalFileArchivePath = Directory.GetCurrentDirectory();
				}
			}
			else
			{
				Log.Write("A configuration value for FileArchivePath has not been provided. [S3Main.S3Main]", Log.Information, 1);
				originalFileArchivePath = Directory.GetCurrentDirectory();
			}
	     			
			Log.Write("Temporary files will be staged at " + tempPath + ". [S3Main.S3Main]", 2);
		}

		~S3Main()
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
				workerThread = new Thread(new ThreadStart(S3MainLoop));
				workerThread.Name = "Worker";
				workerThread.Start();							
				
				Log.Write("Start command issued with new worker thread. [S3Main.Start]", 2);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Start command issued with worker thread already running. [S3Main.Start]", 2);
			}
		}

		public void Stop()
		{
			isStopped = true;

			if (workerThread == null)
			{
				Log.Write("Stop command issued, worker thread never started. [S3Main.Stop]", 2);
			}
			else if (workerThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				workerThread.Abort();
				Log.Write("Stop command issued, sleeping worker thread aborted. [S3Main.Stop]", 2);
			}
			else
			{
				Log.Write("Stop command issued, worker thread is still active. [S3Main.Stop]", 2);
			}
		}

		private void S3MainLoop()
		{
			while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
			{
				Log.Write("Start-of-cycle. [S3.S3MainLoop]", 2);
				KeyValue.Set("S3MainCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
				
				Master.BizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);
				Master.BizDatePrior = KeyValue.Get("BizDatePrior", "0001-01-01", dbCn);       			
				Master.BizDateExchange = KeyValue.Get("BizDateExchange", "0001-01-01", dbCn);       			
				Master.BizDatePriorExchange = KeyValue.Get("BizDatePriorExchange", "0001-01-01", dbCn);       			
				Master.ContractsBizDate = KeyValue.Get("ContractsBizDate", "0001-01-01", dbCn);       			
							
				fileArchivePath = originalFileArchivePath + Master.BizDateExchange + @"\";
				
				OpenItemsGet();
				if (isStopped) break;
				
				PnsGet();
				if (isStopped) break; 
             
				ManualPrioritiesGet();
				if (isStopped) break; 
									
				StartOfDaySegEntryLoad();
				if (isStopped) break;

				StartOfDayStockSegActivity();
				if (isStopped) break;

				StartOfDayMemoSegActivity();
				if (isStopped) break;

				StartOfDayCompletionCheck();
				if (isStopped) break;
				
				ExternalInventoryLoad();
				if (isStopped) break;

				UpdatedDeficitExcessLoad();
				if (isStopped) break;

				ProcessFailedSegActivity();
				if (isStopped) break;
			
				EndOfDaySegEntryLoad();
				if (isStopped) break;												

				EndOfDayStockSegActivity();
				if (isStopped) break;

				EndOfDayMemoSegActivity();
				if (isStopped) break;

				ArchiveFilesGet();
				if (isStopped) break;

				ArchiveFilesUpload();
				if (isStopped) break;

				EodReportsGet();
				if (isStopped) break;			
		
				KeyValue.Set("S3MainCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
				Log.Write("End-of-cycle. [S3.S3MainLoop]", 2);

				if (!isStopped)
				{
					Thread.Sleep(RecycleInterval());
				}
			}
		}

		public void StartOfDayCompletionCheck()
		{
			if(KeyValue.Get("S3SODSegEntriesActivityFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange))
			{
				if(KeyValue.Get("S3SODSegEntriesFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange))
				{
					if(KeyValue.Get("S3SODMemoSegEntriesDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange))
					{					
						KeyValue.Set("S3StartOfDayDate", Master.BizDateExchange, dbCn);
					}
				}
			}
			else
			{
				Log.Write("Start Of Day has not yet completed. [S3Main.StartOfDayCompletionCheck]", 1);
			}
		}
		
		public void ProcessFailedSegActivity()
		{

			
			S3Server.STKSEGModule.STKSEGModule webService = new S3Server.STKSEGModule.STKSEGModule();
			
			string accountNumber = "";
			string indicator = "";
			string processId = "";
			string fromLocation = "";
			string toLocation = "";
			string successCode = "";

			SqlConnection dbCn = null;	
			SqlDataAdapter dataAdapter = null;
			DataSet dataSet = new DataSet();

			try
			{			
				successCode = KeyValue.Get("STKSEGSuccessCode", "ok", dbCnStr);

				dbCn = new SqlConnection(dbCnStr);
				SqlCommand dbCmd = new SqlCommand("spSegEntriesGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = Master.BizDateExchange;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "SegEntries");
				
				Log.Write("Starting to process failed seg movements. [S3Main.ProcessFailedSegActivity]", 2);
				
				foreach (DataRow dr in dataSet.Tables["SegEntries"].Rows)
				{												
					if (KeyValue.Get("S3StartOfDayDate", "2001-01-01", dbCnStr).Equals(Master.BizDateExchange))
					{																			
						if (bool.Parse(dr["IsFailed"].ToString()))
						{
							S3_SegEntry_Datagram segEntryDatagram = new S3_SegEntry_Datagram(dbCnStr);
									
							SegEntrySet(
								dr["ProcessId"].ToString(),
								dr["AccountNumber"].ToString(),
								dr["AccountType"].ToString(),
								dr["SecId"].ToString(),
								dr["Quantity"].ToString(),
								dr["Indicator"].ToString(),
								dr["TimeOfDay"].ToString(),
								true,
								false,
								false,
								"ADMIN");

							accountNumber = dr["AccountNumber"].ToString() + dr["AccountType"].ToString();

							switch(dr["Indicator"].ToString())
							{
								case "I":
									fromLocation = "C";
									toLocation = "S";
									break;

								case "D":
									fromLocation = "S";
									toLocation = "C";
									break;

								default:
									fromLocation = "";
									toLocation = "";
									break;
							}

				
							webService.Url = Standard.ConfigValue("STKSEGWebService", "");
						
						
							try
							{
								S3Server.STKSEGModule.STKSEGTaskInfo objStatus = webService.STKSEGTask(fromLocation, toLocation, accountNumber, dr["Quantity"].ToString(),  dr["SecId"].ToString(), Standard.ConfigValue("STKSEGDescription", ""));								
				
								SegEntrySet(
									dr["ProcessId"].ToString(),
									"",
									"",
									"",
									"",
									"",					
									"",
									((objStatus.status.Equals(successCode))? true: false),
									((objStatus.status.Equals(successCode))? true: false),
									((objStatus.status.Equals(successCode))? false: true),
									"ADMIN");

								Log.Write("Processed seg entry: " + dr["ProcessId"].ToString() + ". [S3Main.ProcessFailedSegActivity]", 2);
							}
							catch 
							{
								SegEntrySet(
									dr["ProcessId"].ToString(),
									"",
									"",
									"",
									"",
									"",					
									"",
									false,
									false,
									true,
									"ADMIN");

								Log.Write("Error processing seg entry: " + dr["ProcessId"].ToString() + ". [S3Main.ProcessFailedSegActivity]", 2);
							}		
						}
					}		
					else
					{
						Log.Write("S3 Start Of Day has not completed yet. [S3Agent.ProcessFailedSegActivity]", Log.Warning, 1);						
					}
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [S3Main.ProcessFailedSegActivity]", Log.Error, 1);    				
			}			
		}

		
		public void SegEntrySet(string processid, string accountNumber, string accountType, string secId, string quantity, string indicator, string timeOfDay,  bool isrequested, bool isprocessed, bool isFailed, string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
      
			try
			{
				SqlCommand dbCmd = new SqlCommand("spSegEntrySet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
          
				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 16);
				paramProcessId.Value = processid;
        
				if (!accountNumber.Equals(""))
				{
					SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar, 8);
					paramAccountNumber.Value = accountNumber;
				}

				if (!accountType.Equals(""))
				{
					SqlParameter paramAccountType = dbCmd.Parameters.Add("@AccountType", SqlDbType.VarChar, 1);
					paramAccountType.Value = accountType;
				}
					
				if (!secId.Equals(""))
				{
				
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}

				if (!quantity.Equals(""))
				{				
					SqlParameter paramEntryQty = dbCmd.Parameters.Add("@EntryQty", SqlDbType.BigInt);
					paramEntryQty.Value = quantity;
				}
				if (!indicator.Equals(""))
				{
				
					SqlParameter paramIndicator = dbCmd.Parameters.Add("@Indicator", SqlDbType.VarChar, 1);
					paramIndicator.Value = indicator;
				}
				
				if (!timeOfDay.Equals(""))
				{				
					SqlParameter paramTimeOfDay = dbCmd.Parameters.Add("@TimeOfDay", SqlDbType.VarChar, 1);
					paramTimeOfDay.Value = timeOfDay;
				}

				SqlParameter paramRequested = dbCmd.Parameters.Add("@IsRequested", SqlDbType.Bit, 1);
				paramRequested.Value = isrequested;
				
				SqlParameter paramProcessed = dbCmd.Parameters.Add("@IsProcessed", SqlDbType.Bit, 1);
				paramProcessed.Value = isprocessed;  
   
				SqlParameter paramIsFailed = dbCmd.Parameters.Add("@IsFailed", SqlDbType.Bit, 1);
				paramIsFailed.Value = isFailed;   

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId; 

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [S3Main.SegEntrySet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed) 
				{
					dbCn.Close();
				}
			}
		}

		private void StartOfDayStockSegActivity()
		{
			Filer filer = new Filer();
			string fileData = "";

			try
			{
				if(!KeyValue.Get("S3SODSegEntriesActivityFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange))
				{
					if(KeyValue.Get("S3SODSegEntriesFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange))
					{
						Log.Write("Creating Start Of Day Seg Entries File for : " + Master.BizDateExchange + " [S3.StartOfDayStockSegActivity]", 3);
					
						S3_Seg_Entries segEntriesFileCreate = new S3_Seg_Entries(dbCnStr, Master.BizDateExchange, Master.BizDatePriorExchange);
						fileData = segEntriesFileCreate.Create("S");																	
						
						FileWrite(Standard.ConfigValue("S3SODSegEntriesActivityFile", ""), fileData);

						filer.FilePut(Standard.ConfigValue("S3SODSegEntriesActivityRemoteFile", ""),
							Standard.ConfigValue("S3SODSegEntriesActivityFileHostName"),
							Standard.ConfigValue("S3SODSegEntriesActivityFileUserName"),
							Standard.ConfigValue("S3SODSegEntriesActivityFilePassword"),
							fileArchivePath + Standard.ConfigValue("S3SODSegEntriesActivityFile"));

						KeyValue.Set("S3SODSegEntriesActivityFileDate", Master.BizDateExchange,dbCn);
					}
					else
					{
						Log.Write("Start Of Day Seg Entries File not loaded for : " + Master.BizDateExchange + " [S3.StartOfDayStockSegActivity]", 3);
					}
				}
				else
				{
					Log.Write("Start Of Day Seg entries activity file created for : " + Master.BizDateExchange + " [S3.StartOfDayStockSegActivity]", 3);	
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [S3Main.StartOfDayStockSegActivity]", 3);
			}
		}

		private void EndOfDayStockSegActivity()
		{			
			Filer filer = new Filer();
			string fileName = "";
			string fileData = "";

			try
			{
				if(!KeyValue.Get("S3EODSegEntriesActivityFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange))
				{
					if(KeyValue.Get("S3EODSegEntriesFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange))
					{
						Log.Write("Creating End Of Day Seg Entries File for : " + Master.BizDateExchange + " [S3.EndOfDayStockSegActivity]", 3);
	
						S3_Seg_Entries segEntriesFileCreate = new S3_Seg_Entries(dbCnStr, Master.BizDateExchange, Master.BizDatePriorExchange);
						fileData = segEntriesFileCreate.Create("E");																																	
												
						fileName = FileWrite(Standard.ConfigValue("S3EODSegEntriesActivityFile", ""), fileData);
						
						filer.FilePut(Standard.ConfigValue("S3EODSegEntriesActivityRemoteFile", ""),
													Standard.ConfigValue("S3EODSegEntriesActivityFileHostName"),
													Standard.ConfigValue("S3EODSegEntriesActivityFileUserName"),
													Standard.ConfigValue("S3EODSegEntriesActivityFilePassword"),
													fileArchivePath + Standard.ConfigValue("S3EODSegEntriesActivityFile"));
						
						KeyValue.Set("S3EODSegEntriesActivityFileDate", Master.BizDateExchange,dbCn);
					}
					else
					{
						Log.Write("End Of Day Seg Entries File not loaded for : " + Master.BizDateExchange + " [S3.EndOfDayStockSegActivity]", 3);
					}
				}
				else
				{
					Log.Write("End Of Day Seg entries activity file created for : " + Master.BizDateExchange + " [S3.EndOfDayStockSegActivity]", 3);	
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [S3Mian.EndOfDayStockSegActivity]", 3);
			}
		}

		private void StartOfDayMemoSegActivity()
		{
			SqlDataAdapter dataAdapter = null;
			DataSet dataSet = new DataSet();
			long dataCount = 0;
			
			if(KeyValue.Get("S3SODSegEntriesActivityFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange))
			{
				if(KeyValue.Get("S3SODSegEntriesFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange))
				{
					if(!KeyValue.Get("S3SODMemoSegEntriesDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange))
					{

						try
						{
							SqlCommand dbCmd	= new SqlCommand("spMemoSegEntriesCreate", dbCn);
							dbCmd.CommandType = CommandType.StoredProcedure;

							SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
							paramBizDate.Value = Master.BizDateExchange;

							SqlParameter paramTimeOfDay = dbCmd.Parameters.Add("@TimeOfDay", SqlDbType.VarChar, 1);
							paramTimeOfDay.Value = "S";

							dataAdapter = new SqlDataAdapter(dbCmd);
							dataAdapter.Fill(dataSet, "MemoSeg");

							foreach (DataRow dr in dataSet.Tables["MemoSeg"].Rows)
							{
								MemoSegEntrySet(
									Standard.ProcessId("M"),
									dr["SecId"].ToString(),
									dr["Quantity"].ToString(),
									false,
									false);
						
								dataCount ++;

								if ((dataCount % 1000) == 0)
								{
									Log.Write("Processed : " + dataCount.ToString("#,##0") + " start of day memo seg entries. [S3Main.StartOfDayMemoSegActivity]", 1);
								}
							}
							
							KeyValue.Set("S3SODMemoSegEntriesDate", Master.BizDateExchange, dbCn);					
						}
						catch (Exception error)
						{
							Log.Write(error.Message + " [S3Mian.StartOfDayMemoSegActivity]", 3);
						}
					}
					else
					{
						Log.Write("Start Of Day Memo Seg entries activity created for : " + Master.BizDateExchange + " [S3.StartOfDayMemoSegActivity]", 3);	
					}
				}
			}
		}

		private void EndOfDayMemoSegActivity()
		{
			SqlDataAdapter dataAdapter = null;
			DataSet dataSet = new DataSet();
			long dataCount = 0;
			
			if(KeyValue.Get("S3EODSegEntriesActivityFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange))
			{
				if(KeyValue.Get("S3EODSegEntriesFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange))
				{
					if(!KeyValue.Get("S3EODMemoSegEntriesDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange))
					{

						try
						{
							SqlCommand dbCmd	= new SqlCommand("spMemoSegEntriesCreate", dbCn);
							dbCmd.CommandType = CommandType.StoredProcedure;

							SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
							paramBizDate.Value = Master.BizDateExchange;

							SqlParameter paramTimeOfDay = dbCmd.Parameters.Add("@TimeOfDay", SqlDbType.VarChar, 1);
							paramTimeOfDay.Value = "E";

							dataAdapter = new SqlDataAdapter(dbCmd);
							dataAdapter.Fill(dataSet, "MemoSeg");

							foreach (DataRow dr in dataSet.Tables["MemoSeg"].Rows)
							{
								MemoSegEntrySet(
									Standard.ProcessId("M"),
									dr["SecId"].ToString(),
									dr["Quantity"].ToString(),
									false,
									false);
						
								dataCount ++;

								if ((dataCount % 1000) == 0)
								{
									Log.Write("Processed : " + dataCount.ToString("#,##0") + " start of day memo seg entries. [S3Main.StartOfDayMemoSegActivity]", 1);
								}
							}
							
							KeyValue.Set("S3EODMemoSegEntriesDate", Master.BizDateExchange, dbCn);					
						}
						catch (Exception error)
						{
							Log.Write(error.Message + " [S3Mian.StartOfDayMemoSegActivity]", 3);
						}
					}
					else
					{
						Log.Write("End Of Day Memo Seg entries activity created for : " + Master.BizDateExchange + " [S3.StartOfDayMemoSegActivity]", 3);	
					}
				}
			}
		}

		private void MemoSegEntrySet(string processId, string secId, string quantity, bool isRequested, bool isProcessed)
		{						
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			SqlCommand dbCmd  = null;

			try
			{
			
				dbCmd = new SqlCommand("spMemoSegEntrySet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 16);
				paramProcessId.Value = processId;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);				
				paramQuantity.Value = quantity;

				SqlParameter paramIsRequested = dbCmd.Parameters.Add("@IsRequested", SqlDbType.Bit);
				paramIsRequested.Value = isRequested;

				SqlParameter paramIsProcessed = dbCmd.Parameters.Add("@IsProcessed", SqlDbType.Bit);
				paramIsProcessed.Value = isProcessed;
		
				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch
			{
				throw;
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}
		}

		private void OpenItemsGet()
		{			
			string trailer = "";

			try
			{
				if (Master.ContractsBizDate.Equals(Master.BizDate))
				{
					if (!(KeyValue.Get("S3OpenItemsFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange)))
					{
						OpenItems openItems = new OpenItems(dbCnStr, Master.BizDateExchange, Master.BizDatePriorExchange, Standard.ConfigValue("BookGroup"));	
					
						openItems.DataSetMake();
						openItems.stop = (int) openItems.DbItemCount;
						openItems.ListMake();				
					
						trailer = "9" + (openItems.FileItemCount + 2).ToString().PadLeft(15, '0').Substring(0, 15) + (new string(' ', 64));	

						FileWrite(Standard.ConfigValue("OpenItemsFile").ToString(), (openItems.StringItem + trailer));
					
						Log.Write("Successfully wrote file: " + fileArchivePath + Standard.ConfigValue("OpenItemsFile").ToString()+ " [S3.OpenItemsGet]", 3);
						Log.Write("Finished creating open items file. [S3.OpenItemsGet]", 3);
					
						KeyValue.Set("S3OpenItemsFileDate", Master.BizDateExchange,dbCn);
					}
					else
					{
						Log.Write("Open Items File already created for : " + Master.BizDate + " [S3.OpenItemsGet]", 3);
					}
				}
				else
				{
					Log.Write("Waiting for contracts to roll to: " + Master.BizDate + " [S3.OpenItemsGet]", 3);
				}
			}
			catch(Exception error)
			{
				Log.Write(error.Message + " [S3.OpenItemsGet]", 3);
			}
		}

		private void PnsGet()
		{					
			string trailer = "";
						
			try
			{	
				if (!Master.BizDateExchange.Equals(DateTime.UtcNow.ToString(Standard.DateTimeShortFormat)))
				{
					if (!(KeyValue.Get("S3SelectedPnsFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange)))
					{				
			
						string dayTime = KeyValue.Get("S3SelectedPnsFileWaitTime", "04:00", dbCn);
				
						if (dayTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0)
						{						
						
							SelectedPnS slps = new SelectedPnS(pensonDbCnStr, Master.BizDateExchange, Master.BizDatePriorExchange, Standard.ConfigValue("BookGroup"));
					
							Log.Write("Starting to create selected pns file. [S3.PnsFileGet]", 3);
					
							slps.DataSetMake();
							slps.stop = (int) slps.DbItemCount;
							slps.ListMake();

							trailer = "9" + (slps.FileItemCount + 2).ToString().PadLeft(15, '0').Substring(0, 15) + (new string(' ', 74));							
					
							FileWrite(Standard.ConfigValue("SelectedPnsFile").ToString(), (slps.StringItem + trailer));
					
							Log.Write("Successfully wrote file: " + fileArchivePath + Standard.ConfigValue("SelectedPnsFile").ToString()+ " [S3.PnsGet]", 2);
							Log.Write("Finished creating selected pns file. [S3.PnsGet]", 2);
					
							KeyValue.Set("S3SelectedPnsFileDate", Master.BizDateExchange, dbCn);																		
						}
						else
						{
							Log.Write("Must wait until " + dayTime + " to create selected pns file [S3.PnsGet]", Log.Information, 1);
						}
					}
					else
					{
						Log.Write("Selected Pns File already created for : " + Master.BizDateExchange + " [S3.PnsGet]", Log.Information ,1);
					}
				}
				else
				{
					Log.Write("Waiting until current buisness date to create selected pns file [S3.PnsGet]", Log.Information, 1);
				}
			}
			catch(Exception error)
			{
				Log.Write(error.Message + " [S3.PnsGet]", Log.Error,1);
			}
		}

		private void ManualPrioritiesGet()
		{						
			string trailer = "";
	
			try
			{					
				if (Master.ContractsBizDate.Equals(Master.BizDate))
				{
					if (!(KeyValue.Get("S3ManualPrioritiesFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange)))
					{
						manpThreadsList = new ArrayList();
						manpList = new ArrayList();

						ManualPriorities manp = new ManualPriorities(dbCnStr, Master.BizDateExchange, Master.BizDatePriorExchange, Standard.ConfigValue("BookGroup"));
					
						manp.DataSetMake();
						manp.stop = (int) manp.DbItemCount;
						manp.ListMake();

						trailer = "9" + (manp.FileItemCount + 2).ToString().PadLeft(15, '0').Substring(0, 15) + (new string(' ', 34));							
					
						FileWrite(Standard.ConfigValue("ManualPrioritiesFile").ToString(), (manp.StringItem + trailer));
					
						Log.Write("Successfully wrote file: " + fileArchivePath + Standard.ConfigValue("ManualPrioritiesFile").ToString()+ " [S3.ManualPrioritiesGet]", 3);
						Log.Write("Finished creating manual priorities items file. [S3.ManualPrioritiesGet]", 3);
					
						KeyValue.Set("S3ManualPrioritiesFileDate", Master.BizDateExchange,dbCn);
					}
					else
					{
						Log.Write("Manual Priorities File already created for : " + Master.BizDate + " [S3.ManualPrioritiesGet]", 3);
					}			
				}
				else
				{
					Log.Write("Waiting for contracts to roll to: " + Master.BizDate + " [S3.ManualPrioritiesGet]", 3);
				}
			}
			catch(Exception error)
			{
				Log.Write(error.Message + " [S3.ManualPrioritiesGet]", 3);
			}
		}

		private string FileWrite(string fileName, string fileData)
		{
			try
			{
				fileArchivePath = Standard.ConfigValue("FileArchivePath", @"C:\") + Master.BizDateExchange +  @"\";
				
				if (!Directory.Exists(fileArchivePath))
				{
					Directory.CreateDirectory(fileArchivePath);
					Log.Write("Created Path: " + fileArchivePath + ". [S3.FileWrite]", 3);							
				}
				else
				{						
					Log.Write("Path: " + fileArchivePath + " already exists. [S3.FileWrite]", 3);	
				}
		
				if (!fileName.Equals(""))
				{
					StreamWriter streamWriter = new StreamWriter(fileArchivePath + fileName);
					streamWriter.Write(fileData);	
					streamWriter.Close();		
				}
			}
			catch(Exception error)
			{
				Log.Write(error.Message + " [S3.FileWrite]", 3);
				throw;
			}
			
			return fileArchivePath + fileName;
		}
					
		private void ArchiveFilesGet()
		{
			string zipFilePath = Standard.ConfigValue("ZipFilePath");
			bool isReady = true;
			
			try
			{
				if (KeyValue.Get("S3ArchiveFilesGetDate", "2001-01-01", dbCn).Equals(Master.BizDateExchange))
				{
					Log.Write("S3 archive files copy process already completed for " + Master.BizDateExchange + ". [S3.ArchiveFilesGet]", 2);
				}
				else
				{
					Log.Write("Starting the S3 archive files copy process for " + Master.BizDateExchange + ". [S3.ArchiveFilesGet]", 2);

					int numberOfFiles = Convert.ToInt32(Standard.ConfigValue("NumberOfFiles"));
					string[,] FileNames = new string[numberOfFiles, 4];
					
					for (int i = 0; i < numberOfFiles; i++)
					{
						string[] keyvalue = Standard.ConfigValue("File[" + i + "]").Split(';');
						
						if (keyvalue.Length > 0)
						{
							FileNames.SetValue(keyvalue[0], i, 0);
							FileNames.SetValue((fileArchivePath + keyvalue[1]), i, 1);
							FileNames.SetValue(keyvalue[2], i, 2);
							FileNames.SetValue(keyvalue[3], i, 3);
						}
					}
					
					for (int filedata = 0; filedata < (int)FileNames.GetLength(0); filedata++)
					{
						if (File.Exists(FileNames[filedata, 0]))
						{
							if (!Directory.Exists(zipFilePath + "\\" + Master.BizDateExchange))
								Directory.CreateDirectory(zipFilePath + "\\" + Master.BizDateExchange);

							if (File.Exists(FileNames[filedata, 1]))
								File.Delete(FileNames[filedata, 1]);

							Log.Write("Copying " + FileNames[filedata, 0] + " to " +  FileNames[filedata, 1] + ". [S3.ArchiveFilesGet]", 3);
							File.Copy(FileNames[filedata, 0], FileNames[filedata, 1]);

							using (StreamReader sr = new StreamReader(FileNames[filedata, 1]))
							{
								string line = sr.ReadLine();
								string strBizDatePrior = line.Substring(Convert.ToInt32(FileNames[filedata, 2].ToString()), Convert.ToInt32(FileNames[filedata, 3].ToString()));
								
								strBizDatePrior = DateTime.ParseExact(strBizDatePrior,"yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd");								
								
								if (!strBizDatePrior.Equals(Master.BizDatePriorExchange))
								{
									Log.Write("File " + FileNames[filedata, 1] + " date not equal to Master.BizDatePriorExchange. [S3.ArchiveFilesGet]", 3);
									
									isReady = false;
									break;
								}
							}
						}
					}
					
					if (isReady == true)
					{
						Log.Write("All files are in for today. [S3.ArchiveFilesGet]", 3);
						KeyValue.Set("S3ArchiveFilesGetDate", Master.BizDateExchange, dbCn);
					}
					else
					{
						Log.Write("Not all files are in for today. [S3.ArchiveFilesGet]", 3);
					}
				}
			}
			catch (Exception error)
			{				
				Log.Write(error + " [S3.ArchiveFilesGet]", 3);
			}
		}
		
		private void ArchiveFilesUpload()
		{						
			int lastIndexExt;
			string zipFileName;		
			string zipFilePath = Standard.ConfigValue("ZipFilePath");
			
			string remoteFtpHost; 
			string remoteFtpUserName;
			string remoteFtpPassword;
			
			int numOfServers = int.Parse(Standard.ConfigValue("S3FtpServers"));

			bool isReady = true;
			bool transferAsZip = Convert.ToBoolean(Standard.ConfigValue("TransferAsZip"));
		
		
				
				if (DateTime.Now.ToString("yyyy-MM-dd").Equals(Master.BizDateExchange))
				{
						for (int index = 0; index < numOfServers; index ++)
						{
								try
								{
										remoteFtpHost = Standard.ConfigValue("S3FtpHost[" + index + "]");
										remoteFtpUserName = Standard.ConfigValue("S3FtpUserName[" + index + "]");
										remoteFtpPassword = Standard.ConfigValue("S3FtpPassword[" + index + "]");			
	
										if (KeyValue.Get("S3ArchiveFilesUploadDate[" + remoteFtpHost + "]", "2001-01-01", dbCn).Equals(Master.BizDate))
										{
												Log.Write("S3 archive files ftp[ " + remoteFtpHost + "] process already completed for " + Master.BizDate + ". [S3.ArchiveFilesUpload]", 2);
										}
										else if (KeyValue.Get("S3ArchiveFilesGetDate", "2001-01-01", dbCn).Equals(Master.BizDate))
										{
												Log.Write("Starting the S3 archive files upload process for to ftp["+ remoteFtpHost + "] for " + Master.BizDate + ". [S3.ArchiveFilesUpload]", 2);					

												string[] fileNames = Standard.ConfigValue("RequiredFiles").Split(';');
					
												foreach (string filename in fileNames)
												{
														if ((!File.Exists(fileArchivePath + filename)) && filename.Length > 0)
														{
																Log.Write("File " + fileArchivePath + filename + " not found. [S3.ArchiveFilesUpload]", 2);
																isReady = false;
																break;
														}
												}
					
												Filer filer = new Filer();

												if (isReady == true)
												{
														if (transferAsZip)
														{
																Zip zip = new Zip();
																zip.CreateZip(fileNames, fileArchivePath, 9);
						
																foreach (string filename in fileNames)
																{
																		if (!filename.Equals(""))
																		{
																				lastIndexExt = filename.LastIndexOf(".");
																				zipFileName = filename.Substring(0, lastIndexExt) + ".zip";
									
																				Log.Write("Sending  " + fileArchivePath + zipFileName + " to " +  remoteFtpHost + ". [S3.ArchiveFilesUpload]", 3);
																				filer.FilePut(zipFileName, remoteFtpHost, remoteFtpUserName, remoteFtpPassword, fileArchivePath + @"\" + zipFileName);
																		}
																}

																KeyValue.Set("S3ArchiveFilesUploadDate[" + remoteFtpHost + "]"   , Master.BizDate, dbCn);
														}
														else
														{							
																foreach (string filename in fileNames)
																{
																		lastIndexExt = filename.LastIndexOf(".");
																		zipFileName = fileArchivePath + filename.Substring(0, lastIndexExt) + ".zip";

																		Log.Write("Sending  " + filename + " to " +  remoteFtpHost + ". [S3.ArchiveFilesUpload]", 3);
																		filer.FilePut(filename, remoteFtpHost, remoteFtpUserName, remoteFtpPassword, fileArchivePath + "\\" + filename);								
																}

																KeyValue.Set("S3ArchiveFilesUploadDate[" + remoteFtpHost + "]"   , Master.BizDateExchange, dbCn);
														}
												}					
										}
								}
								catch (Exception error)
								{				
										Log.Write(error + " [S3.ArchiveFilesUpload]", 3);									
								}
						}					
				}
				else
				{
						Log.Write("Must wait until current buisness date. [S3.ArchiveFilesUpload]", 3);		
				}
		}

		private void EodReportsGet()
		{
			string dayTime;
			
			try
			{
				if (Master.BizDateExchange.Equals(DateTime.UtcNow.ToString(Standard.DateFormat))) // Today is the business date.
				{			
					dayTime = KeyValue.Get("S3EodReportsGetTime", "23:00", dbCn);
				
					if (dayTime.CompareTo(DateTime.UtcNow.ToString("HH:mm")) < 0)
					{
						Log.Write("Starting the EodReportsGet process. [S3.EodReportsGet]", 3);
						Log.Write("Getting files from server... [S3.EodReportsGet]", 3);

						string pensonFtpServerHost = Standard.ConfigValue("PensonFtpServerHost");
						string pensonFtpServerUserName = Standard.ConfigValue("PensonFtpServerUserName");
						string pensonFtpServerPassword = Standard.ConfigValue("PensonFtpServerPassword");
						
						Log.Write("Starting the end of day zip files get process for " + Master.BizDate + ". [S3.EodReportsGet]", 2);
					
						Filer filer = new Filer();
						string[] FileNames = filer.DirectoryListGet(@"/", pensonFtpServerHost, pensonFtpServerUserName, pensonFtpServerPassword);

						if (FileNames.Length > 0)
						{
							Zip zip = new Zip();
							foreach (string filename in FileNames)
							{
								if (!filename.Equals(""))
								{
									int lastIndexExt = filename.LastIndexOf(".");
									if (filename.Substring(lastIndexExt + 1, 3) == "ZIP")
									{
											if (DateTime.Parse(filer.FileTime(@"/" + filename, pensonFtpServerHost, pensonFtpServerUserName, pensonFtpServerPassword)).ToString("yyyy-MM-dd").Equals(Master.BizDateExchange))
											{
													if (File.Exists(fileArchivePath + @"\" + filename))
													{
															string serverTimeDate = filer.FileTime(@"/" + filename, pensonFtpServerHost, pensonFtpServerUserName, pensonFtpServerPassword);
															string localTimeDate = File.GetLastWriteTime(fileArchivePath + @"\" + filename).ToString("yyyy-MM-dd HH:mm:ss");

															if (!serverTimeDate.Equals(localTimeDate))
															{
																	Log.Write(filename + " already exists but with a different timestamp. [S3.EodReportsGet]", 3);
																	Log.Write("Getting " + filename + " from " +  pensonFtpServerHost + ". [S3.EodReportsGet]", 3);

																	filer.FileGet(filename, pensonFtpServerHost, pensonFtpServerUserName, pensonFtpServerPassword, fileArchivePath + @"\" + filename);
																	File.SetLastWriteTime(fileArchivePath + @"\" + filename, Convert.ToDateTime(filer.FileTime(@"/" + filename, pensonFtpServerHost, pensonFtpServerUserName, pensonFtpServerPassword)));
												
																	Log.Write("Timestamp : " + File.GetLastWriteTime(fileArchivePath + @"\" + filename).ToString("yyyy-MM-dd HH:mm:ss") + ". [EodReportsGet]", 3);
																	zip.UnZipFile(fileArchivePath + filename, fileArchivePath);
															}
															else
															{
																	Log.Write("File exists on the local system with the same timestamp. [S3.EodReportsGet]", 3);
															}
													}
													else
													{
															Log.Write(filename + " does not exists on the local system. [S3.EodReportsGet]", 3);
															Log.Write("Getting " + filename + " from " +  pensonFtpServerHost + ". [S3.EodReportsGet]", 3);

															filer.FileGet(filename, pensonFtpServerHost, pensonFtpServerUserName, pensonFtpServerPassword, fileArchivePath + @"\" + filename);
															File.SetLastWriteTime(fileArchivePath + @"\" + filename, Convert.ToDateTime(filer.FileTime(@"/" + filename, pensonFtpServerHost, pensonFtpServerUserName, pensonFtpServerPassword)));

															zip.UnZipFile(fileArchivePath + filename, fileArchivePath);
											
													}
											}
											else
											{
													Log.Write(filename + " on server " + pensonFtpServerHost + " is not for today. [S3Main.EodReportsGet]", 1);
											}
									}
								}								
							}
		
						}
						else
						{
							Log.Write("No zip files found. [S3.EodReportsGet]", 3);
						}
					}
					else
					{
						Log.Write("EodReportsGet process is not allowed to run at this time. [S3.EodReportsGet]", 3);
					}
				}
				else
				{
					Log.Write("BizDate is not equal to the current date. [S3.EodReportsGet]", 3);
				}
		
			}
			catch (Exception error)
			{
				Log.Write(error + " [S3.EodReportsGet]", 3);
			}
		}

		private void ExternalInventoryLoad()
		{						
			try
			{
				if (!(KeyValue.Get("S3ExternalInventoryFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange)))
				{
					Log.Write("Starting the External Inventory process. [S3.ExternalInventoryLoad]", 3);

					S3_External_Inventory externalInventoryImport = new S3_External_Inventory(dbCnStr, Master.BizDateExchange, Master.BizDatePriorExchange);
					
					Filer filer = new Filer();
			
					FileWrite("", "");

					filer.FileGet(Standard.ConfigValue("S3ExternalInventoryZipFile", ""),
						Standard.ConfigValue("S3ExternalInventoryFileHostName", ""),
						Standard.ConfigValue("S3ExternalInventoryFileUserName", ""),
						Standard.ConfigValue("S3ExternalInventoryFilePassword", ""),
						fileArchivePath + Standard.ConfigValue("S3ExternalInventoryZipFile", ""));

					Zip zipFile = new Zip();
					zipFile.UnZipFile(fileArchivePath + Standard.ConfigValue("S3ExternalInventoryZipFile", ""), fileArchivePath);															
					
					Log.Write("Unzipped " + fileArchivePath + Standard.ConfigValue("S3ExternalInventoryZipFile", "") + ". [S3.ExternalInventoryLoad]", 2);

					externalInventoryImport.Load(fileArchivePath + Standard.ConfigValue("S3ExternalInventoryFile", ""));

					KeyValue.Set("S3ExternalInventoryFileDate", Master.BizDateExchange,dbCn);
					Log.Write("External Inventory Load process successfully completed. [S3.ExternalInventoryLoad]", 3);
				}
				else
				{
					Log.Write("External Inventory File already loaded for : " + Master.BizDate + " [S3.ExternalInventoryLoad]", 3);
				}
			}
			catch(Exception error)
			{
				Log.Write(error.Message + " [S3.ExternalInventoryLoad]", 3);
			}
		}

		private void UpdatedDeficitExcessLoad()
		{						
			try
			{
				if (!(KeyValue.Get("S3UpdatedDeficitExcessFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange)))
				{
					Log.Write("Starting the Updated Deficit Excess Load process. [S3.StartOfDaySegEntryLoad]", 3);

					S3_Updated_Deficit_Excess updatedDeficitExcessFileImport = new S3_Updated_Deficit_Excess(dbCnStr, Master.BizDateExchange, Master.BizDatePriorExchange);
					
					Filer filer = new Filer();
					
					FileWrite("","");

					filer.FileGet(Standard.ConfigValue("S3UpdatedDeficitExcessZipFile", ""),
						Standard.ConfigValue("S3UpdatedDeficitExcessFileHostName", ""),
						Standard.ConfigValue("S3UpdatedDeficitExcessFileUserName", ""),
						Standard.ConfigValue("S3UpdatedDeficitExcessFilePassword", ""),
						fileArchivePath + Standard.ConfigValue("S3UpdatedDeficitExcessZipFile", ""));

					Zip zipFile = new Zip();
					zipFile.UnZipFile(fileArchivePath + Standard.ConfigValue("S3UpdatedDeficitExcessZipFile", ""), fileArchivePath);															
					
					Log.Write("Unzipped " + fileArchivePath + Standard.ConfigValue("S3UpdatedDeficitExcessZipFile", "") + ". [S3.UpdatedDeficitExcessLoad]", 2);

					updatedDeficitExcessFileImport.Load(fileArchivePath + Standard.ConfigValue("S3UpdatedDeficitExcessFile", ""), 
					"localHost",
					"", 
					"");

					KeyValue.Set("S3UpdatedDeficitExcessFileDate", Master.BizDateExchange,dbCn);
					Log.Write("Updated Deficit Excess Load process successfully completed. [S3.UpdatedDeficitExcessLoad]", 3);
				}
				else
				{
					Log.Write("Updated Deficit Excess File already loaded for : " + Master.BizDateExchange + " [S3.UpdatedDeficitExcessLoad]", 3);
				}
			}
			catch(Exception error)
			{
				Log.Write(error.Message + " [S3.UpdatedDeficitExcessLoad]", 3);
			}
		}

		private void UnZipFiles(string directory)
		{
			try
			{
				Log.Write("Starting the UnZipFiles process. [S3.UnZipFiles]", 3);
				string[] FileNames = Directory.GetFiles(directory, "*.zip");
				Zip zip = new Zip();

				foreach (string filename in FileNames)
				{
					if (!filename.Equals(""))
					{
						zip.UnZipFile(filename, fileArchivePath);
					}
				}
			}
			catch(Exception error)
			{
				Log.Write(error + " [S3.UnZipFiles]", 3);
			}
		}

		private void StartOfDaySegEntryLoad()
		{						
			try
			{
				if (!(KeyValue.Get("S3SODSegEntriesFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange)))
				{
					Log.Write("Starting the Start of Day Seg Entry Load process. [S3.StartOfDaySegEntryLoad]", 3);

					S3_Seg_Entries segEntriesFileImport = new S3_Seg_Entries(dbCnStr, Master.BizDateExchange, Master.BizDatePriorExchange);
					
					Filer filer = new Filer();
					
					FileWrite("","");

					filer.FileGet(Standard.ConfigValue("S3SODSegEntriesZipFile", ""),
						Standard.ConfigValue("S3SODSegEntriesFileHostName", ""),
						Standard.ConfigValue("S3SODSegEntriesFileUserName", ""),
						Standard.ConfigValue("S3SODSegEntriesFilePassword", ""),
						fileArchivePath + Standard.ConfigValue("S3SODSegEntriesZipFile", ""));

					Zip zipFile = new Zip();
					zipFile.UnZipFile(fileArchivePath + Standard.ConfigValue("S3SODSegEntriesZipFile", ""), fileArchivePath);															
					
					Log.Write("Unzipped " + fileArchivePath + Standard.ConfigValue("S3SODSegEntriesZipFile", "") + ". [S3.StartOfDaySegEntryLoad]", 2);

					segEntriesFileImport.Load(fileArchivePath + Standard.ConfigValue("S3SODSegEntriesFile", ""),
						"localHost",
						"",
						"",
						"S");

					KeyValue.Set("S3SODSegEntriesFileDate", Master.BizDateExchange,dbCn);
					Log.Write("Start Of Day Seg Entry Load process successfully completed. [S3.StartOfDaySegEntryLoad]", 3);
				}
				else
				{
					Log.Write("Start Of Day Seg Entries File already loaded for : " + Master.BizDateExchange + " [S3.StartOfDaySegEntryLoad]", 3);
				}
			}
			catch(Exception error)
			{
				Log.Write(error.Message + " [S3.SegEntrySet]", 3);
			}
		}

		private void EndOfDaySegEntryLoad()
		{						
			try
			{
				if (!(KeyValue.Get("S3EODSegEntriesFileDate", "2001-01-01",dbCn).Equals(Master.BizDateExchange)))
				{
					Log.Write("Starting the end of day Seg Entry Load process. [S3.EndOfSegEntryLoad]", 3);

					S3_Seg_Entries segEntriesFileImport = new S3_Seg_Entries(dbCnStr, Master.BizDateExchange, Master.BizDatePriorExchange);
					
					Filer filer = new Filer();
					
					FileWrite("", "");

					filer.FileGet(Standard.ConfigValue("S3EODSegEntriesZipFile", ""),
						Standard.ConfigValue("S3EODSegEntriesFileHostName", ""),
						Standard.ConfigValue("S3EODSegEntriesFileUserName", ""),
						Standard.ConfigValue("S3EODSegEntriesFilePassword", ""),
						fileArchivePath + Standard.ConfigValue("S3EODSegEntriesZipFile", ""));

					Zip zipFile = new Zip();
					zipFile.UnZipFile(fileArchivePath + Standard.ConfigValue("S3EODSegEntriesZipFile", ""), fileArchivePath);															
					
					Log.Write("Unzipped " + fileArchivePath + Standard.ConfigValue("S3EODSegEntriesZipFile", "") + ". [S3.EndOfSegEntryLoad]", 2);

					segEntriesFileImport.Load(fileArchivePath + Standard.ConfigValue("S3EODSegEntriesFile", ""),
						"localHost",
						"",
						"",
						"E");

					KeyValue.Set("S3EODSegEntriesFileDate", Master.BizDate,dbCn);
					Log.Write("End Of Day Seg Entry Load process successfully completed. [S3.EndOfSegEntryLoad]", 3);
				}
				else
				{
					Log.Write("End Of Day Seg Entries File already loaded for : " + Master.BizDate + " [S3.EndOfSegEntryLoad]", 3);
				}
			}
			catch(Exception error)
			{
				Log.Write(error.Message + " [S3.EndOfSegEntryLoad]", 3);
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
				recycleInterval = KeyValue.Get("S3MainRecycleIntervalBizDay", "0:20", dbCn);
			}
			else
			{
				recycleInterval = KeyValue.Get("S3MainRecycleIntervalNonBizDay", "6:00", dbCn);
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
					KeyValue.Set("S3MainRecycleIntervalBizDay", "0:20", dbCn);
					hours = 0;
					minutes = 20;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [S3Main.RecycleInterval]", Log.Error, 1);
				}
				else
				{
					KeyValue.Set("S3MainRecycleIntervalNonBizDay", "6:00", dbCn);
					hours = 6;
					minutes = 0;
					timeSpan = new TimeSpan (hours, minutes, 0);
					Log.Write("MainRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [S3Main.RecycleInterval]", Log.Error, 1);
				}
			}

			Log.Write("S3Main will recycle in " + hours + " hours, " + minutes + " minutes. [S3Main.RecycleInterval]", 2);
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
