using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Anetics.Common;

namespace Anetics.S3
{
	/// <summary>
	/// Summary description for SelectedPnS.
	/// </summary>
	public class SelectedPnS
	{
		private string dbCnStr		= "";
		private string bizDate		= "";
		private string bizDatePrior	= "";
		private string bookGroup	= "";
		private DataSet dataSet = null;
		private long fileItemCount = 0;
		private long dbItemCount = 0;
		private bool isFinished = false;
		private	string stringItem = "";

		public int start = 0;
		public int stop = 0;

		public DataSet PnsSummary
		{
			get
			{
				return dataSet;
			}

			set
			{
				dataSet = value;
			}
		}

		public long FileItemCount
		{
			get
			{
				return fileItemCount;
			}
		}
		
		public long DbItemCount
		{
			get
			{
				return dbItemCount;
			}
		}

		public bool IsFinished
		{
			get
			{
				return  isFinished;
			}
		}

		public string StringItem
		{
			get
			{
				return stringItem;
			}
		}

		public SelectedPnS(string dbCnStr, string bizDate, string bizDatePrior, string bookGroup)
		{
			this.dbCnStr		= dbCnStr;
			this.bizDate		= bizDate;
			this.bizDatePrior		= bizDatePrior;
			this.bookGroup	= bookGroup;
		}

		public void DataSetMake()
		{
			dbItemCount = 0;
			dataSet = new DataSet();
			
			SqlConnection dbCn = new SqlConnection(dbCnStr);			
			SqlDataAdapter dataAdapter = null;

			try
			{						
				SqlCommand dbCmd = new SqlCommand("prcSelectedPNSGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;				
				dbCmd.CommandTimeout = 6000;


				SqlParameter paramFirm = dbCmd.Parameters.Add("@Firm", SqlDbType.VarChar, 2);
				paramFirm.Value = "07";

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDatePrior;

				SqlParameter paramSettlementDate = dbCmd.Parameters.Add("@SettlementDate", SqlDbType.DateTime);
				paramSettlementDate.Value = bizDate;
								
				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "PNS");
				
				dbItemCount = dataSet.Tables["PNS"].Rows.Count;		
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [S3.SelectedPns.DataSetMake]", 3);
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

		public void ListMake()
		{		
			string accountNature = "";
			DataRow dr = null;

			try
			{
					
				fileItemCount = 0;

				if (start == 0)
				{					
					stringItem = "1" + ("SEL PS".PadRight(10, ' ').Substring(0, 10))+ DateTime.Parse(bizDatePrior).ToString("yyyyMMdd") + new string(' ',71);				
				}
			
				for (int index = start; index < stop; index++)
				{

					dr = dataSet.Tables["PNS"].Rows[index];

					switch(dr[4].ToString())
					{
						case "0": accountNature = "FIRM";
							break;
						case "1": accountNature = "CASH";
							break;
						case "2": accountNature = "MARGIN";
							break;
						case "3":	accountNature = "SHRTSALE";
							break;
					}
									
					if ((!dr[0].ToString().Equals("")) &&(!dr[1].ToString().Equals("")))
					{						
						switch (dr[5].ToString().Trim())
						{
							case "B":
							case "S":
								stringItem += "5" + dr[0].ToString().PadRight(15, ' ').Substring(0,15) 
									+ accountNature.ToUpper().PadRight(10, ' ').Substring(0,10) 
									+ Math.Abs(decimal.Parse(dr[2].ToString())).ToString("0000").PadLeft(15, '0').Substring(0,15) 
									+ DateTime.Parse(dr[1].ToString()).ToString("yyyyMMdd")
									+ dr[3].ToString().PadRight(25, ' ').Substring(0,25) 
									+ dr[4].ToString().PadRight(3, ' ').Substring(0, 3) 
									+ dr[5].ToString().PadRight(1, ' ').Substring(0,1)
									+ " " + (new string(' ', 11));
			
								fileItemCount ++;
								break;
						
							default:
								break;
						}
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [S3.SelectedPns.ListMake]", 3);
				throw;
			}

			isFinished = true;
		}
	}
}
