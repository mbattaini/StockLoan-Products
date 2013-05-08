using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Anetics.Common;

namespace Anetics.S3
{
	/// <summary>
	/// Creates the Open Items data
	/// </summary>

	public class ManualPriorities
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

		public DataSet BoxSummary
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

		public ManualPriorities(string dbCnStr, string bizDate, string bizDatePrior, string bookGroup)
		{
			this.dbCnStr		= dbCnStr;
			this.bizDate		= bizDate;
			this.bizDatePrior		= bizDatePrior;
			this.bookGroup	= bookGroup;
		}

		public void DataSetMake()
		{
			dbItemCount = 0;
			dataSet = new DataSet("Rates");

			dataSet.Tables.Add("Rates");
			dataSet.Tables["Rates"].Columns.Add("SecId");
			dataSet.Tables["Rates"].Columns.Add("Rate");
			dataSet.Tables["Rates"].AcceptChanges();
			
			SqlConnection dbCnPenson = new SqlConnection(dbCnStr);			
			SqlDataReader dataReader = null;
			DataRow dr = null;

			try
			{		
				string rateSql =
						"Select	Distinct(C.SecId),"
					+ "Avg(C.Rate) as Rate\n"
					+ "From	dbo.tbContracts C (nolock)\n"
					+ "Where	C.Bizdate=  '" + bizDatePrior+ "'\n"
					+ "And	C.Bookgroup = '7380'\n"
					+ "And	C.ContractType = 'L'\n"
					+ "And	C.Rate < 0\n"
					+ "And	C.SecId Not In (Select	Distinct(SecId)\n"
					+ "											From	tbContracts\n"
					+ "											Where	Bizdate=  '" + bizDatePrior +"'\n"
					+ "											And	Bookgroup = '0234'\n"
					+ "											And	SecId = C.SecId)\n"
					+ "Group By SecId\n"
					+ "Order By Rate\n\n";
		
				Log.Write(rateSql, 3);
				SqlCommand dbCmd = new SqlCommand(rateSql, dbCnPenson);
				dbCmd.CommandType = CommandType.Text;				

				dbCnPenson.Open();
				dataReader = dbCmd.ExecuteReader();	 
				
				while (dataReader.Read())
				{
					dr = dataSet.Tables["Rates"].NewRow();
					
					dr["SecId"] = dataReader.GetValue(0);
					dr["Rate"] = decimal.Parse(dataReader.GetValue(1).ToString());				
			
					dataSet.Tables["Rates"].LoadDataRow(dr.ItemArray, true);
				
					dbItemCount++;
				}		
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [S3.ManualPriorities]", 3);
			}
			finally
			{
				if ((dataReader != null) && (!dataReader.IsClosed))
				{
					dataReader.Close();
				}
       		
				if (dbCnPenson.State != ConnectionState.Closed)
				{
					dbCnPenson.Close();
				}
			}		
		}

		public void ListMake()
		{			
			DataRow dr = null;

			fileItemCount = 0;

			if (start == 0)
			{
				stringItem = "1MAN PRI   " + DateTime.Parse(bizDatePrior).ToString("yyyyMMdd") + new string(' ',31);
			}
			
			for (int index = start; index < stop; index++)
			{

				dr = dataSet.Tables["Rates"].Rows[index];
				
				stringItem += "5" 
					+ dr["SecId"].ToString().PadRight(15, ' ').Substring(0,15) 
					+ "MANPRI".PadRight(10, ' ').Substring(0, 10) 
					+ "0".PadLeft(15, '0').Substring(0, 15) 
					+	((decimal.Parse(dr["Rate"].ToString()) < 0)? "-":"+")
					+	Math.Abs(decimal.Parse(dr["Rate"].ToString()) * 1000).ToString("0000000") + " ";
				
				fileItemCount ++;		
			}
			
			isFinished = true;
		}
	}
}
