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
		private string bookGroup	= "";
		
		public OpenItems(string dbCnStr, string bizDate, string bookGroup)
		{
			this.dbCnStr		= dbCnStr;
			this.bizDate		= bizDate;
			this.bookGroup	= bookGroup;
		}

		public DataSet DataSetMake()
		{
			DataSet dataSet = new DataSet("BoxPosition");

			dataSet.Tables.Add("Box");
			dataSet.Tables["Box"].Columns.Add("SecId");
			dataSet.Tables["Box"].Columns.Add("CustomerLongSettled");
			dataSet.Tables["Box"].Columns.Add("CustomerShortSettled");
			dataSet.Tables["Box"].Columns.Add("FirmLongSettled");
			dataSet.Tables["Box"].Columns.Add("FirmShortSettled");
			dataSet.Tables["Box"].Columns.Add("CustomerPledgeSettled");
			dataSet.Tables["Box"].Columns.Add("FirmPledgeSettled");
			dataSet.Tables["Box"].Columns.Add("DvpFailInSettled");
			dataSet.Tables["Box"].Columns.Add("DvpFailOutSettled");
			dataSet.Tables["Box"].Columns.Add("BrokerFailInSettled");
			dataSet.Tables["Box"].Columns.Add("BrokerFailOutSettled");
			dataSet.Tables["Box"].Columns.Add("ClearingFailInSettled");
			dataSet.Tables["Box"].Columns.Add("ClearingFailOutSettled");
			dataSet.Tables["Box"].Columns.Add("OtherFailInSettled");
			dataSet.Tables["Box"].Columns.Add("OtherFailOutSettled");
			
			dataSet.Tables["Box"].AcceptChanges();

			int n= 0;
			SqlConnection dbCnPenson = new SqlConnection(dbCnStr);			
			SqlDataReader dataReader = null;
			DataRow dr = null;

			try
			{
				string sql = 				
					"Select SecId, \n" +
					"				CustomerLongSettled,\n" +					
					"				CustomerShortSettled,\n" +					
					"				FirmLongSettled,\n" +
					"				FirmShortSettled,\n" +					
					"				CustomerPledgeSettled,\n" +					
					"				FirmPledgeSettled,\n" +					
					"				DvpFailInSettled,\n" +					
					"				DvpFailOutSettled,\n" +					
					"				BrokerFailInSettled,\n" +					
					"				BrokerFailOutSettled,\n" +					
					"				ClearingFailInSettled,\n" +					
					"				ClearingFailOutSettled,\n" +					
					"				OtherFailInSettled,\n" +					
					"				OtherFailOutSettled\n" +					
					"From		dbo.tbBoxPosition (nolock)\n" +
					"Where	BizDate = '" + bizDate + "'\n" + 
					"And		BookGroup = '" + bookGroup + "'\n" +
					"Order  By 1 Desc\n";

				SqlCommand pensonDbCmd = new SqlCommand(sql, dbCnPenson);
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
					dr["CustomerPledgeSettled"] = Tools.ParseLong(dataReader.GetValue(5).ToString());
					dr["FirmPledgeSettled"] = Tools.ParseLong(dataReader.GetValue(6).ToString());
					dr["DvpFailInSettled"] = Tools.ParseLong(dataReader.GetValue(7).ToString());
					dr["DvpFailOutSettled"] = Tools.ParseLong(dataReader.GetValue(8).ToString());
					dr["BrokerFailInSettled"] = Tools.ParseLong(dataReader.GetValue(9).ToString());
					dr["BrokerFailOutSettled"] = Tools.ParseLong(dataReader.GetValue(10).ToString());
					dr["ClearingFailInSettled"] = Tools.ParseLong(dataReader.GetValue(11).ToString());
					dr["ClearingFailOutSettled"] = Tools.ParseLong(dataReader.GetValue(12).ToString());
					dr["OtherFailInSettled"] = Tools.ParseLong(dataReader.GetValue(13).ToString());
					dr["OtherFailOutSettled"] = Tools.ParseLong(dataReader.GetValue(14).ToString());					
					
					dataSet.Tables["Box"].LoadDataRow(dr.ItemArray, true);

					if (((++n % 100) == 0))
					{
						break;
					}

					if ((n % 5000) == 0)
					{
						Log.Write("Interim box position insert/update count: " + n + " [StaticData.BoxPositionLoad]", 2);            
					}
			
					Log.Write(n.ToString(), 3);
				}

				Log.Write("ROws: " + dataSet.Tables["Box"].Rows.Count, 3);

			}
			catch (Exception e)
			{
				Log.Write(e.Message, 3);
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
		
			return dataSet;
		}

		public string ListMake(DataSet dataSet)
		{
			string opeItemsString = "";
			int itemCount = 0;

			opeItemsString = "1OPEN ITEMS" + DateTime.Parse(bizDate).ToString("yyyyMMdd") + new string(' ',61)+"\n";

			foreach (DataRow dr in dataSet.Tables["Box"].Rows)
			{
				// DVP Fail To Deliver
				opeItemsString += "5" + dr["SecId"].ToString().PadLeft(15, ' ').Substring(0,15) 
					+ "DVP".PadRight(10, ' ').Substring(0, 10) + dr["DvpFailOutSettled"].ToString().PadLeft(15, '0').Substring(0, 15) + (new string(' ', 8)) + (new string('0', 18)) 
					+ "+0000000" + ' ' + (new string(' ' , 4)) + "\n";

				// Broker Fail To Deliver
				opeItemsString += "5" + dr["SecId"].ToString().PadLeft(15, ' ').Substring(0,15) 
					+ "FR".PadRight(10, ' ').Substring(0, 10) + dr["BrokerFailInSettled"].ToString().PadLeft(15, '0').Substring(0, 15) + (new string(' ', 8)) + (new string('0', 18)) 
					+ "+0000000" + ' ' + (new string(' ' , 4)) + "\n";

				// Broker Fail To Recieve
				opeItemsString += "5" + dr["SecId"].ToString().PadLeft(15, ' ').Substring(0,15) 
					+ "FD".PadRight(10, ' ').Substring(0, 10) + dr["BrokerFailOutSettled"].ToString().PadLeft(15, '0').Substring(0, 15) + (new string(' ', 8)) + (new string('0', 18)) 
					+ "+0000000" + ' ' + (new string(' ' , 4)) + "\n";

				// DTC Customer Bank Loan
				opeItemsString += "5" + dr["SecId"].ToString().PadLeft(15, ' ').Substring(0,15) 
					+ "DTCBLCUST".PadRight(10, ' ').Substring(0, 10) + dr["CustomerPledgeSettled"].ToString().PadLeft(15, '0').Substring(0, 15) + (new string(' ', 8)) + (new string('0', 18)) 
					+ "+0000000" + ' ' + (new string(' ' , 4)) + "\n";

				// DTC Firm Bank Loan
				opeItemsString += "5" + dr["SecId"].ToString().PadLeft(15, ' ').Substring(0,15) 
					+ "DTCBLFIRM".PadRight(10, ' ').Substring(0, 10) + dr["FirmPledgeSettled"].ToString().PadLeft(15, '0').Substring(0, 15) + (new string(' ', 8)) + (new string('0', 18)) 
					+ "+0000000" + ' ' + (new string(' ' , 4)) + "\n";
				
				itemCount ++;
			}

			itemCount += 2;
			opeItemsString += "9" + itemCount.ToString().PadLeft(15, '0').Substring(0, 15) + (new string(' ', 64));

			return opeItemsString;
		}
	}
}
