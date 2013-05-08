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

	public class OpenItems
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

		public OpenItems(string dbCnStr, string bizDate, string bizDatePrior, string bookGroup)
		{
			this.dbCnStr		= dbCnStr;
			this.bizDate		= bizDate;
			this.bizDatePrior		= bizDatePrior;
			this.bookGroup	= bookGroup;
		}

		public void DataSetMake()
		{
			dbItemCount = 0;
			dataSet = new DataSet("BoxPosition");

			dataSet.Tables.Add("Box");
			dataSet.Tables["Box"].Columns.Add("SecId");
			dataSet.Tables["Box"].Columns.Add("CustomerLongSettled");
			dataSet.Tables["Box"].Columns.Add("CustomerShortSettled");
			dataSet.Tables["Box"].Columns.Add("FirmLongSettled");
			dataSet.Tables["Box"].Columns.Add("FirmShortSettled");
			dataSet.Tables["Box"].Columns.Add("DvpFailInSettled");
			dataSet.Tables["Box"].Columns.Add("DvpFailOutSettled");
			dataSet.Tables["Box"].Columns.Add("BrokerFailInSettled");
			dataSet.Tables["Box"].Columns.Add("BrokerFailOutSettled");
			
			dataSet.Tables["Box"].AcceptChanges();
			
			SqlConnection dbCnPenson = new SqlConnection(dbCnStr);			
			SqlDataReader dataReader = null;
			DataRow dr = null;

			try
			{		
				string boxSummarySql = 				
					"Select B.SecId, \n" +
					"				B.CustomerLongSettled,\n" +					
					"				B.CustomerShortSettled,\n" +					
					"				B.FirmLongSettled,\n" +
					"				B.FirmShortSettled,\n" +					
					"				B.DvpFailInSettled,\n" +					
					"				B.DvpFailOutSettled,\n" +					
					"				B.BrokerFailInSettled,\n" +					
					"				B.BrokerFailOutSettled\n" +					
					"From		dbo.tbBoxPosition B (nolock)\n" +
					"Where	B.BizDate = '" + bizDatePrior + "'\n" + 
					"And		B.BookGroup = '" + bookGroup + "'\n" +
					"And    (DvpFailInSettled > 0 \n" +
					"OR			DvpFailOutSettled > 0 \n" + 
					"OR			BrokerFailInSettled > 0\n" + 
					"OR			BrokerFailOutSettled > 0)\n" + 
					"Order  By 1 Desc\n";
		
				Log.Write(boxSummarySql, 3);
				SqlCommand pensonDbCmd = new SqlCommand(boxSummarySql, dbCnPenson);
				pensonDbCmd.CommandType = CommandType.Text;				

				dbCnPenson.Open();
				dataReader = pensonDbCmd.ExecuteReader();	 
				
				while (dataReader.Read())
				{
					dr = dataSet.Tables["Box"].NewRow();
					
					dr["SecId"] = dataReader.GetValue(0);
					dr["CustomerLongSettled"] = Tools.ParseLong(dataReader.GetValue(1).ToString());
					dr["CustomerShortSettled"] = Tools.ParseLong(dataReader.GetValue(2).ToString());
					dr["FirmLongSettled"] = Tools.ParseLong(dataReader.GetValue(3).ToString());
					dr["FirmShortSettled"] = Tools.ParseLong(dataReader.GetValue(4).ToString());
					dr["DvpFailInSettled"] = Tools.ParseLong(dataReader.GetValue(5).ToString());
					dr["DvpFailOutSettled"] = Tools.ParseLong(dataReader.GetValue(6).ToString());
					dr["BrokerFailInSettled"] = Tools.ParseLong(dataReader.GetValue(7).ToString());
					dr["BrokerFailOutSettled"] = Tools.ParseLong(dataReader.GetValue(8).ToString());
			
					dataSet.Tables["Box"].LoadDataRow(dr.ItemArray, true);
				
					dbItemCount++;
				}		
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [S3.OpenItems]", 3);
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
				stringItem = "1OPEN ITEMS" + DateTime.Parse(bizDatePrior).ToString("yyyyMMdd") + new string(' ',61);
			}
			
			for (int index = start; index < stop; index++)
			{

				dr = dataSet.Tables["Box"].Rows[index];

				// DVP Fail To Deliver
				if (long.Parse(dr["DvpFailOutSettled"].ToString()) > 0)
				{
					stringItem += "5" + dr["SecId"].ToString().PadRight(15, ' ').Substring(0,15) 
						+ "DVP".PadRight(10, ' ').Substring(0, 10) + dr["DvpFailOutSettled"].ToString().PadLeft(15, '0').Substring(0, 15) + (DateTime.Parse(bizDate).ToString("yyyyMMdd")) + (new string('0', 18)) 
						+ "+0000000" + ' ' + (new string(' ' , 4));
					fileItemCount ++;
				}

				// DVP Fail To Receive
				if (long.Parse(dr["DvpFailInSettled"].ToString()) > 0)
				{
					stringItem += "5" + dr["SecId"].ToString().PadRight(15, ' ').Substring(0,15) 
						+ "RVP".PadRight(10, ' ').Substring(0, 10) + dr["DvpFailInSettled"].ToString().PadLeft(15, '0').Substring(0, 15) + (DateTime.Parse(bizDate).ToString("yyyyMMdd")) + (new string('0', 18)) 
						+ "+0000000" + ' ' + (new string(' ' , 4));
					fileItemCount ++;
				}

				// Broker Fail To Deliver
				if (long.Parse(dr["BrokerFailInSettled"].ToString()) > 0)
				{
					stringItem += "5" + dr["SecId"].ToString().PadRight(15, ' ').Substring(0,15) 
						+ "FR".PadRight(10, ' ').Substring(0, 10) + dr["BrokerFailInSettled"].ToString().PadLeft(15, '0').Substring(0, 15) + (DateTime.Parse(bizDate).ToString("yyyyMMdd")) + (new string('0', 18)) 
						+ "+0000000" + ' ' + (new string(' ' , 4));
					fileItemCount ++;
				}

				// Broker Fail To Recieve
				if (long.Parse(dr["BrokerFailOutSettled"].ToString()) > 0)
				{
					stringItem += "5" + dr["SecId"].ToString().PadRight(15, ' ').Substring(0,15) 
						+ "FD".PadRight(10, ' ').Substring(0, 10) + dr["BrokerFailOutSettled"].ToString().PadLeft(15, '0').Substring(0, 15) + (DateTime.Parse(bizDate).ToString("yyyyMMdd")) + (new string('0', 18)) 
						+ "+0000000" + ' ' + (new string(' ' , 4));
					fileItemCount ++;
				}		
			}
			
			isFinished = true;
		}
	}
}
