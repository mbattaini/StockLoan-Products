using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;

namespace Anetics.S3
{
		
	public class S3_Seg_Entries
	{
		private string dbCnStr;
		private string bizDate;
		private string bizDatePrior;
		private SqlConnection dbCn = null;
		private SqlCommand dbCmd = null;
		private SqlDataAdapter dataAdapter = null;

		public S3_Seg_Entries (string dbCnStr, string bizDate, string bizDatePrior)
		{
			this.dbCnStr = dbCnStr;
			this.bizDate = bizDate;
			this.bizDatePrior = bizDatePrior;
		}

		public void Load(string fileName, string hostName, string userName, string password, string timeOfDay)
		{			
			int lineIndex = 0;
			int itemCount = 0;
			int fileCount = 0;
			int dataCount	= 0;

			string line = "";
			string subLine = "";
			string fileDate = "";
			
	
			bool done;
			int lineLength;

			Filer filer = new Filer();
			
			if (!filer.FileExists(fileName,	hostName,	userName,password))
			{
				throw new Exception("File " + fileName + " not found.  [S3_Seg_Entries.Load]");
			}
					
			try 
			{				
				DataSet dataSet = new DataSet("Seg_Entries");
				dataSet.Tables.Add("DetailRecords");
				
				DataRow dataRow;
						
				dataSet.Tables["DetailRecords"].Columns.Add("ProcessId");
				dataSet.Tables["DetailRecords"].Columns.Add("AccountNumber");
				dataSet.Tables["DetailRecords"].Columns.Add("AccountType");
				dataSet.Tables["DetailRecords"].Columns.Add("SecId");
				dataSet.Tables["DetailRecords"].Columns.Add("EntryQty");
				dataSet.Tables["DetailRecords"].Columns.Add("Indicator");

				done = false;
				lineLength = 60;

				using (StreamReader sr = new StreamReader(filer.StreamGet(fileName,	hostName,	userName,password)))
				{
					lineIndex = 0;
					itemCount = 0;
					fileCount = 0;
					dataCount = 0;

					line = sr.ReadLine();
					subLine = "";
					
					while (!done) 
					{
						subLine = line.Substring(lineIndex * lineLength, lineLength);
						
						switch (subLine.Substring(0, 1))
						{
							case ("1"):							
								fileDate = DateTime.ParseExact(subLine.Substring(11, 8), "yyyyMMdd", null).ToString("yyyy-MM-dd");
								
								if (!fileDate.Equals(bizDatePrior))
								{
									Log.Write("File is not for today", 3);
									throw new Exception("File is not for today");
								}
								break;
							
							case ("5"):
								dataRow	= dataSet.Tables["DetailRecords"].NewRow();
								
								dataRow["ProcessId"] = Standard.ProcessId();
								dataRow["AccountNumber"] = subLine.Substring(1, 8);
								dataRow["AccountType"] = subLine.Substring(10, 1);
								dataRow["SecId"] = subLine.Substring(26, 15);
								dataRow["EntryQty"] = subLine.Substring(41, 15);
								dataRow["Indicator"] = subLine.Substring(56, 1);
								
								dataSet.Tables["DetailRecords"].Rows.Add(dataRow);
								
								itemCount ++;
								break;
							
							case ("9"):
								fileCount = Convert.ToInt32(subLine.Substring(1, 15));
								done = true;
								break;
						}

						if ((lineIndex % 1000) == 0)
						{
							Log.Write("Prcessed: " + lineIndex + " lines. [S3_Seg_Entries.Load]", 3);
						}
						
						lineIndex ++;
					}
				}
				
				if ((itemCount + 2) != fileCount)
				{
					throw new Exception("Read: " + itemCount.ToString("#,##0") + " records out of " +  fileCount.ToString("#,##0") + " items. [S3_Seg_Entries.Load]");
				}
				else
				{
					Log.Write("Read: " + itemCount.ToString("#,##0") + " items. [S3_Seg_Entries.Load]", 3);
				}

				
				
				dbCn = new SqlConnection(dbCnStr);

				dbCmd = new SqlCommand("spSegEntrySet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 16);
				SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar, 8);
				SqlParameter paramAccountType = dbCmd.Parameters.Add("@AccountType", SqlDbType.VarChar, 1);
				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				SqlParameter paramEntryQty = dbCmd.Parameters.Add("@EntryQty", SqlDbType.BigInt);
				SqlParameter paramIndicator = dbCmd.Parameters.Add("@Indicator", SqlDbType.VarChar, 1);
				SqlParameter paramTimeOfDay = dbCmd.Parameters.Add("@TimeOfDay", SqlDbType.VarChar, 1);
				SqlParameter paramIsRequested = dbCmd.Parameters.Add("@IsRequested", SqlDbType.Bit);
				SqlParameter paramIsProcessed = dbCmd.Parameters.Add("@IsProcessed", SqlDbType.Bit);
				SqlParameter paramIsFailed = dbCmd.Parameters.Add("@IsFailed", SqlDbType.Bit);
				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);

				paramTimeOfDay.Value = timeOfDay;
				paramActUserId.Value = "ADMIN";
				
				
				
				dbCn.Open();

				foreach (DataRow dr in dataSet.Tables["DetailRecords"].Rows)
				{										
					paramProcessId.Value = dr["ProcessId"].ToString();
					paramAccountNumber.Value = dr["AccountNumber"].ToString();
					paramAccountType.Value = dr["AccountType"].ToString();
					paramSecId.Value = dr["SecId"].ToString();
					paramEntryQty.Value = dr["EntryQty"].ToString();
					paramIndicator.Value = dr["Indicator"].ToString();
					paramIsRequested.Value = 0;
					paramIsProcessed.Value = 0;
					paramIsFailed.Value = 0;
					
					dbCmd.ExecuteNonQuery();
					dataCount ++;
				
					if ((dataCount % 1000) == 0)
					{
						Log.Write("Processed: " + dataCount.ToString("#,##0") + " lines. [S3_Seg_Entries.Load]", 3);
					}
				
				}

				Log.Write("Wrote: " + dataCount.ToString("#,##0") + " items. [S3_Seg_Entries.Load]", 3);
			}
			catch (Exception error) 
			{
				throw new Exception(error.Message);
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed && dbCn != null)
				{
					dbCn.Close();
				}
			}
		}
		

		public string Create(string timeOfDay)
		{	
			DataSet dataSet = new DataSet();
			string fromLocation = "";
			string toLocation = "";
			string header = "";
			string detail = "";
			string trailer = "";
			int recordCount = 0;
			
			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spSegEntriesGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				SqlParameter paramTimeOfDay = dbCmd.Parameters.Add("@TimeOfDay", SqlDbType.VarChar, 1);
				paramTimeOfDay.Value = timeOfDay;
			
				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "SegEntries");
			
				header = "0007" + DateTime.Parse(bizDate).ToString("yyyyMMdd") + (new string(' ', 88)) + "\r\n";

				foreach (DataRow dr in dataSet.Tables["SegEntries"].Rows)
				{
					switch (dr["Indicator"].ToString())
					{
						case "D":
							fromLocation = "S";
							toLocation = "C";
							break;
						case "I":
							fromLocation = "C";
							toLocation = "S";
							break;
						default:
							fromLocation = "";
							toLocation = "";
							break;
					}
			
					detail += "01" +
						fromLocation.PadRight(2, ' ').Substring(0, 2) + 
						toLocation.PadRight(2, ' ').Substring(0, 2) + 
						dr["AccountNumber"].ToString().PadRight(8, ' ').Substring(0, 8) +
						dr["AccountType"].ToString().PadRight(1, ' ').Substring(0, 1) +
						dr["Quantity"].ToString().PadLeft(18, ' ').Substring(0, 18) +
						dr["SecId"].ToString().PadRight(9, ' ').Substring(0, 9) +
						"S3 SOD SEG ENTRY".PadRight(25, ' ').Substring(0, 25) + 
						(new string(' ', 33)) + "\r\n";				
				
					SegEntrySet(
						dr["ProcessId"].ToString(),
						dr["AccountNumber"].ToString(),
						dr["AccountType"].ToString(),
						dr["SecId"].ToString(),
						"",
						dr["Indicator"].ToString(),
						dr["TimeOfDay"].ToString(),
						true,
						true,
						false,
						"ADMIN");
					
					
					recordCount ++;
				}
				recordCount += 2;

				trailer = "99" + recordCount.ToString().PadLeft(18, ' ') + (new string(' ', 80)) + "\r\n";
			}
			catch (Exception error)
			{
				throw new Exception(error.Message);
			}
		
			return (header + detail + trailer);
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
				Log.Write(e.Message + " [S3_Seg_Entries.SegEntrySet]", Log.Error, 1);
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
	}	
}
