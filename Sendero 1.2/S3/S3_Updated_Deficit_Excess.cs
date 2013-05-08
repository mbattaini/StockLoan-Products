using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;
using Anetics.SmartSeg;

namespace Anetics.S3
{
	
	public class S3_Updated_Deficit_Excess
	{
		private string dbCnStr;
		private string bizDate;
		private string bizDatePrior;
		private SqlConnection dbCn = null;
		private SqlCommand dbCmd = null;		
		
		public S3_Updated_Deficit_Excess (string dbCnStr, string bizDate, string bizDatePrior)
		{
			this.dbCnStr = dbCnStr;
			this.bizDate = bizDate;
			this.bizDatePrior = bizDatePrior;
		}

		public void Load(string fileName, string hostName, string userName, string password)
		{
			int lineIndex = 0;
			int itemCount = 0;
			int fileCount = 0;
			int dataCount	= 0;

			string line = "";
			string subLine = "";
			string fileDate = "";

			DataRow dataRow;
			
	
			bool done;
			int lineLength;

			Filer filer = new Filer();
			
			if (!filer.FileExists(fileName,	hostName,	userName,password))
			{
				throw new Exception("File " + fileName + " not found. [S3_Updated_Deficit_Excess.Load]");
			}
			
			try 
			{			
				DataSet dataSet = new DataSet("UpdatedDefEx");
				dataSet.Tables.Add("DetailRecords");
					
				dataSet.Tables["DetailRecords"].Columns.Add("SecId");
				dataSet.Tables["DetailRecords"].Columns.Add("DeficitExcessQuantity");
				dataSet.Tables["DetailRecords"].Columns.Add("TotalReqsQuantity");				
				
				done = false;
				lineLength = 60;

				using (StreamReader sr = new StreamReader(filer.StreamGet(fileName,	hostName,	userName,password)))
				{
					lineIndex = 0;
					itemCount = 0;
					fileCount = 0;
					dataCount = 0;
					
					line		= sr.ReadLine();
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

								dataRow["SecId"] = subLine.Substring(1, 15);
								dataRow["DeficitExcessQuantity"] = subLine.Substring(16, 18);
								dataRow["TotalReqsQuantity"] = subLine.Substring(35, 18);
								
								dataSet.Tables["DetailRecords"].Rows.Add(dataRow);
								
								itemCount++;
								break;
							
							case ("9"):
								fileCount = Convert.ToInt32(subLine.Substring(1, 15));
								done = true;
								break;
						}
						
						if ((lineIndex % 1000) == 0)
						{
							Log.Write("Prcessed: " + lineIndex + " lines. [S3_Updated_Deficit_Excess.Load]", 3);
						}
						
						lineIndex ++;
					}
				}

				if ((itemCount + 2) != fileCount)
				{
					throw new Exception("Read: " + itemCount.ToString("#,##0") + " records out of " +  fileCount.ToString("#,##0") + " items. [S3_Updated_Deficit_Excess.Load]");
				}
				else
				{
					Log.Write("Read: " + itemCount.ToString("#,##0") + " items. [S3_Updated_Deficit_Excess.Load]", 3);
				}

				dbCn = new SqlConnection(dbCnStr);

				dbCmd = new SqlCommand("spUpdatedDeficitExcessPurge", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				
				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;				
				
				dbCn.Open();
				dbCmd.ExecuteNonQuery();
											
				dbCmd = new SqlCommand("spUpdatedDeficitExcessSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;			
				
				
				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 15);
				SqlParameter paramDefExcQty = dbCmd.Parameters.Add("@DeficitExcessQuantity", SqlDbType.BigInt);
				SqlParameter paramTotalReqsQty = dbCmd.Parameters.Add("@TotalReqsQuantity", SqlDbType.BigInt);
						
				foreach (DataRow dr in dataSet.Tables["DetailRecords"].Rows)
				{		
					paramBizDate.Value = bizDate;
					paramSecId.Value = dr["SecId"].ToString();
					paramDefExcQty.Value = dr["DeficitExcessQuantity"].ToString();
					paramTotalReqsQty.Value = dr["TotalReqsQuantity"].ToString();
					
					dbCmd.ExecuteNonQuery();
				
					dataCount ++;
				
					if ((dataCount % 1000) == 0)
					{
						Log.Write("Processed: " + dataCount.ToString("#,##0") + " lines. [S3_Updated_Deficit_Excess.Load]", 3);
					}
				}
			
				Log.Write("Wrote: " + dataCount.ToString("#,##0") + " items. [S3_Updated_Deficit_Excess.Load]", 3);
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
	}
}
