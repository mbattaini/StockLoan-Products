using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class BankLoanReports
  {
    private SqlConnection dbCn;
		private DataSet dataSet = null;

    public BankLoanReports(string dbCnStr) : this(new SqlConnection(dbCnStr)) {}
    public BankLoanReports(SqlConnection dbCn)
    {
      this.dbCn = dbCn;
    }

		public void Load(
			string bookGroup, 			
			string reportHost, 
			string reportHostUserId, 
			string reportHostPassword, 					
			string reportPath, 
			string reportName)		
		{
			string secId;
			string quantity;
			string activity;
			string fileContents;
			string line;
			string reportDate = "";
			string fileTime  = "";

			Purge(bookGroup, reportName);
			
			Common.Filer filer = new Common.Filer(MedalistMain.TempPath + "bankLoanreport" + bookGroup + ".txt");
			filer.Timeout = 90000;
			
			StreamReader streamReader = null;

			Log.Write("Opening file " + reportPath + " on host " + reportHost + ". [BankLoanReports.Load]", 2);

			//load data per report
			SqlCommand dbCmd = new SqlCommand("spBankLoanReportDataSet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;
			
			SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);			
			SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
			SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
			SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
			SqlParameter paramActivity = dbCmd.Parameters.Add("@Activity", SqlDbType.VarChar, 10);

			try
			{							
				fileTime = filer.FileTime(
					reportPath + reportName,
					reportHost,
					reportHostUserId,
					reportHostPassword);
				
				streamReader = new StreamReader(
					filer.StreamGet(
					reportPath + reportName,
					reportHost,
					reportHostUserId,
					reportHostPassword),
					System.Text.Encoding.ASCII);     
				streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

				BankLoanReportDataMaskGet(bookGroup, reportName);
				
				if (dataSet == null || dataSet.Tables["BankLoanReportDataMask"].Rows.Count == 0)
				{
					Log.Write("Report " + reportName + " has no data mask. [BankLoanReports.Load]", 2);
					return;
				}		
							
				// begin loading all data mask options
				string headerFlag = dataSet.Tables["BankLoanReportDataMask"].Rows[0]["HeaderFlag"].ToString();
				string dataFlag = dataSet.Tables["BankLoanReportDataMask"].Rows[0]["DataFlag"].ToString();
				string trailerFlag = dataSet.Tables["BankLoanReportDataMask"].Rows[0]["TrailerFlag"].ToString();
				
				int reportNamePosition = int.Parse( dataSet.Tables["BankLoanReportDataMask"].Rows[0]["ReportNamePosition"].ToString());
				int reportNameLength = int.Parse( dataSet.Tables["BankLoanReportDataMask"].Rows[0]["ReportNameLength"].ToString());

				int reportDatePosition = int.Parse( dataSet.Tables["BankLoanReportDataMask"].Rows[0]["ReportDatePosition"].ToString());
				int	reportDateLength = int.Parse( dataSet.Tables["BankLoanReportDataMask"].Rows[0]["ReportDateLength"].ToString());

				int secIdPosition = int.Parse( dataSet.Tables["BankLoanReportDataMask"].Rows[0]["SecIdPosition"].ToString());
				int	secIdLength = int.Parse( dataSet.Tables["BankLoanReportDataMask"].Rows[0]["SecIdLength"].ToString());
				int quantityPosition = int.Parse( dataSet.Tables["BankLoanReportDataMask"].Rows[0]["QuantityPosition"].ToString());
				int quantityLength = int.Parse( dataSet.Tables["BankLoanReportDataMask"].Rows[0]["QuantityLength"].ToString());

				int activityPosition = int.Parse( dataSet.Tables["BankLoanReportDataMask"].Rows[0]["ActivityPosition"].ToString());
				int activityLength = int.Parse( dataSet.Tables["BankLoanReportDataMask"].Rows[0]["ActivityLength"].ToString());

				string ignoreItems = dataSet.Tables["BankLoanReportDataMask"].Rows[0]["IgnoreItems"].ToString();				
				
				int lineLength = int.Parse( dataSet.Tables["BankLoanReportDataMask"].Rows[0]["LineLength"].ToString());
				// done loading all data mask options
				
				int lineCount = 1;
				int itemCount = 0;

				fileContents = streamReader.ReadToEnd();
				
				Log.Write("Loading report " + reportName + ". [BankLoanReports.Load]", 2);
				
				dbCn.Open();

				while ( fileContents.Length > (lineLength * (lineCount-1)))
				{          
					line = fileContents.Substring(lineLength * (lineCount - 1), lineLength).Replace("\r\n", "");	
					
					if (line.Substring(0, 6).Equals(headerFlag))
					{						
						reportDate = DateTime.Parse(line.Substring(reportDatePosition, reportDateLength)).ToString(Standard.DateFormat);
						
						if (!reportDate.Equals(Master.ContractsBizDate))
						{
							Log.Write("File is not for current date " +  Master.ContractsBizDate + ". [BankLoanReports.Load]", 2);
							return;
						}
					}
					else if (line.Substring(0, 6).Equals(dataFlag))
					{											
						if ( (line.IndexOf(ignoreItems) == -1)  && (line.Trim().Length > 6))
						{						
							secId = line.Substring(secIdPosition, secIdLength);			

							if (!secId.Trim().Equals("") && (secId.Trim().Length == 9))
							{
								quantity = line.Substring(quantityPosition, quantityLength);
								quantity = Tools.ParseLong(quantity).ToString();
																						
								activity = line.Substring(activityPosition, activityLength);
															
								if (!activity.Trim().Equals("") && !quantity.Equals("0"))
								{
									paramBookGroup.Value = bookGroup;												
									paramReportName.Value = reportName;				
									paramSecId.Value = secId;				
									paramQuantity.Value = quantity;				
									paramActivity.Value = activity;
						
									dbCmd.ExecuteNonQuery();
					
									itemCount ++;
								}
							}
						}
					}
					else if (line.Substring(0, 6).Equals(trailerFlag))
					{}			
				
					lineCount++;
				}							
				
				Log.Write("Loaded " + itemCount + " items(s). [BankLoanReports.Load]", 2);										
				BankLoanReportUpdate(bookGroup, reportName, reportDate, fileTime);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [BankLoanReports.Load]", 2);				
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}			
		}

		public void Purge(string bookGroup, string reportName)
		{	//purge all data in table					
			try
			{
				Log.Write("Bank Loan reports purge starting for: " + Master.ContractsBizDate + ". [BankLoanReports.Purge]", Log.Information, 2);
				
				SqlCommand dbCmd = new SqlCommand("spBankLoanReportsPurge", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				if (!bookGroup.Equals(""))
				{
					paramBookGroup.Value = bookGroup;
				}

				SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
				if (!reportName.Equals(""))
				{
					paramReportName.Value = reportName;
				}

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			
				Log.Write("Bank Loan reports purge done for: " + Master.ContractsBizDate + ". [BankLoanReports.Purge]", Log.Information, 2);		
			}
			catch (Exception e)
			{
				Log.Write(e.Message + ". [BankLoanReports.Purge]", 2);
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

		private void BankLoanReportUpdate (string bookGroup, string reportName, string fileLoadDate, string fileLoadTime)
		{
			SqlConnection dbCnLocal = null;

			try
			{
				dbCnLocal = new SqlConnection(dbCn.ConnectionString);
				
				SqlCommand dbCmd = new SqlCommand("spBankLoanReportSet", dbCnLocal);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);				
				SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
				SqlParameter paramFileLoadDate = dbCmd.Parameters.Add("@FileLoadDate", SqlDbType.DateTime);
				SqlParameter paramFileLoadTime = dbCmd.Parameters.Add("@FileLoadTime", SqlDbType.DateTime);

				paramBookGroup.Value = bookGroup;								
				paramReportName.Value = reportName;			
				paramFileLoadDate.Value = fileLoadDate;
				paramFileLoadTime.Value = fileLoadTime;

				dbCnLocal.Open();

				dbCmd.ExecuteNonQuery();			
			}
			catch (Exception e)
			{
				Log.Write(e.Message + ". [BankLoanReports.BankLoanReportUpdate]", 2);
				throw;
			}
			finally
			{
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCnLocal.Close();
				}
			}
		}
		
		private void BankLoanReportDataMaskGet(string bookGroup, string reportName)
		{			
			SqlDataAdapter dataAdapter = null;
		
			dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spBankLoanReportsDataMasksGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);		
				SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);

				paramBookGroup.Value = bookGroup;								
				paramReportName.Value = reportName;			

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "BankLoanReportDataMask");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + ". [BankLoanReports.LoadBankLoanReportDataMaskGet]", 2);
				throw;
			}
		}
  }
}
