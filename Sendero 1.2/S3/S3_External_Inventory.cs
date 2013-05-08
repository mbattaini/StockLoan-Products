using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;

namespace Anetics.S3
{
	public class S3_External_Inventory
	{
		private string dbCnStr;
		private string bizDate;
		private string bizDatePrior;

		public S3_External_Inventory (string dbCnStr, string bizDate, string bizDatePrior)
		{
			this.dbCnStr = dbCnStr;
			this.bizDate = bizDate;
			this.bizDatePrior = bizDatePrior;
		}

		public void Load(string FileName)
		{
			int lineIndex = 0;
			int itemCount = 0;
			int fileCount = 0;
			int dataCount = 0;

			string line = "";
			string subLine = "";
			string fileDate = "";
			
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			bool done;
			int lineLength;

			if (!File.Exists(FileName))
			{
				throw new FileNotFoundException("File " + FileName + " not found");
			}

			try 
			{
				DataSet dataSet = new DataSet("External_Inventory");
				dataSet.Tables.Add("DetailRecords");

				DataRow dataRow;

				dataSet.Tables["DetailRecords"].Columns.Add("ProcessId");
				dataSet.Tables["DetailRecords"].Columns.Add("SecId");
				dataSet.Tables["DetailRecords"].Columns.Add("PsrQuantity");
				dataSet.Tables["DetailRecords"].Columns.Add("PotentiallyAvailableQuantity");
				dataSet.Tables["DetailRecords"].Columns.Add("ReservedExcessQuantity");
				
				done = false;
				lineLength = 65;
				
				using (StreamReader sr = new StreamReader(FileName)) 
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
									throw new Exception("File is not for today");
								}
								break;

							case ("5"):
								dataRow	= dataSet.Tables["DetailRecords"].NewRow();
								
								dataRow["ProcessId"] = Standard.ProcessId();
								dataRow["SecId"] = subLine.Substring(1, 15);
								dataRow["PsrQuantity"] = subLine.Substring(18, 15);
								dataRow["PotentiallyAvailableQuantity"] = subLine.Substring(33, 15);
								dataRow["ReservedExcessQuantity"] = subLine.Substring(48, 15);

								dataSet.Tables["DetailRecords"].Rows.Add(dataRow);

								itemCount ++;
								break;

							case ("9"):
								fileCount = int.Parse(subLine.Substring(1, 15));
								done = true;
								break;
						}
					
						if ((lineIndex % 1000) == 0)
						{
							Log.Write("Processed: " + lineIndex + " lines. [S3_External_Inventory.Load]", 3);
						}
                        
						lineIndex ++;
					}
				}

				if ((itemCount + 2) != fileCount) 
				{
					throw new Exception("Read: " + itemCount.ToString("#,##0") + " records out of " +  fileCount.ToString("#,##0") + " items. [S3_External_Inventory.Load]");
				}
				else
				{
					Log.Write("Read: " + itemCount.ToString("#,##0") + " items. [S3_External_Inventory.Load]", 3);
				}

				dbCn = new SqlConnection(dbCnStr);


				dbCmd = new SqlCommand("spInventorySubstitutionPurge", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;
				
				dbCn.Open();
				dbCmd.ExecuteNonQuery();
				dbCn.Close();

				dbCmd.Parameters.Remove(paramBizDate);

				dbCmd.CommandText = "spInventorySubstitutionSet";
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 16);
				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 15);
				SqlParameter paramPsrQuantity = dbCmd.Parameters.Add("@PsrQuantity", SqlDbType.BigInt);
				SqlParameter paramPotentiallyAvailableQuantity = dbCmd.Parameters.Add("@PotentiallyAvailableQuantity", SqlDbType.BigInt);
				SqlParameter paramReservedExcessQuantity  = dbCmd.Parameters.Add("@ReservedExcessQuantity ", SqlDbType.BigInt);
			
				dbCn.Open();

				foreach (DataRow dr in dataSet.Tables["DetailRecords"].Rows)
				{
					paramProcessId.Value = dr["ProcessId"].ToString();
					paramSecId.Value = dr["SecId"].ToString();
					paramPsrQuantity.Value = dr["PsrQuantity"].ToString();
					paramPotentiallyAvailableQuantity.Value = dr["PotentiallyAvailableQuantity"].ToString();
					paramReservedExcessQuantity.Value = dr["ReservedExcessQuantity"].ToString();
					
					dbCmd.ExecuteNonQuery();
					
					dataCount ++;
					if ((dataCount % 1000) == 0)
					{
						Log.Write("Prcessed: " + dataCount.ToString("#,##0") + " lines. [S3_External_Inventory.Load]", 3);
					}					
				}

				Log.Write("Wrote: " + dataCount.ToString("#,##0") + " items. [S3_External_Inventory.Load]", 3);
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
