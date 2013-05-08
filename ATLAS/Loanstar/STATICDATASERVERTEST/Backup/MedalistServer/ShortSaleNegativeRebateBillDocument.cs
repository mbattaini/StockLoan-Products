using System;
using System.Data;
using System.Data.SqlClient;
using Anetics.Common;

namespace Anetics.Medalist
{
	/// <summary>
	/// Generates a loan Termination notice.
	/// </summary>
	public class ShortSaleNegativeRebateBillDocument
	{
		const int LINES_PER_PAGE = 30;
		
		SqlConnection dbCn = null;
		
		string dbCnStr;
		string bizDate;
		string startDate;
		string stopDate;
		string groupCode;		
		string header = "";
		string page = "";		

		public ShortSaleNegativeRebateBillDocument(string dbCnStr, string startDate, string stopDate, string groupCode)
		{
			this.dbCnStr = dbCnStr;
			this.dbCn = new SqlConnection(dbCnStr);
      
			this.startDate = startDate;
			this.stopDate = stopDate;
			this.groupCode = groupCode;			
		
			this.bizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);		
		}		
			
		public string AccountsBorrowedEmailBill()
		{		
			SqlDataReader dataReader = null;        
			int itemCount = 0;			
			string mailContent = "";
	
			try
			{
				SqlCommand sqlCmd = new SqlCommand("spShortSaleBillingSummaryGet", dbCn);
				sqlCmd.CommandType = CommandType.StoredProcedure;
				sqlCmd.CommandTimeout = 600;    
        
				SqlParameter paramStartDate = sqlCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
				paramStartDate.Value = bizDate;
				
				SqlParameter paramStopDate = sqlCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
				paramStopDate.Value = bizDate;

				mailContent = "[SECURITY BY ACCOUNT]\n\n";

				mailContent += 
					"Group Code".PadRight(12, ' ') 
					+ "Account Number".PadRight(16, ' ') 
					+ "SecId".PadRight(15, ' ') 
					+ "Symbol".PadRight(12, ' ')
					+	"Quantity Shorted".PadLeft(15, ' ')
					+	"Quantity Covered".PadLeft(17, ' ') + "\n";					

				mailContent += 
					"----------".PadRight(12, ' ') 
					+ "--------------".PadRight(16, ' ') 
					+ "-----".PadRight(15, ' ') 
					+ "------".PadRight(12, ' ')
					+	"----------------".PadLeft(15, ' ')
					+	"----------------".PadLeft(17, ' ') + "\n";					

				string	groupCode = "";
				string	secId = "";
				string	symbol = "";
				string	accountNumber = "";
				string	quantityShorted = "";
				string	quantityCovered = "";				

				decimal totalCharge = 0;
				
				try
				{        
					dbCn.Open();
					dataReader = sqlCmd.ExecuteReader();

					while (dataReader.Read())
					{						
						groupCode				= dataReader.GetValue(2).ToString();
						accountNumber		= dataReader.GetValue(3).ToString();
						secId						= dataReader.GetValue(4).ToString();
						symbol					= dataReader.GetValue(5).ToString();
						quantityShorted = dataReader.GetValue(6).ToString();
						quantityCovered = dataReader.GetValue(7).ToString();						
						
						mailContent += groupCode.PadRight(12, ' ')
							+ accountNumber.PadRight(16, ' ') 
							+ secId.PadRight(15, ' ') 
							+ symbol.PadRight(12, ' ')
							+ quantityShorted.PadLeft(15, ' ')
							+ quantityCovered.PadLeft(17, ' ') + "\n";
				
						++itemCount;
					}

					mailContent +=  ("Total Charges: " + totalCharge.ToString("#,##0.00")).PadLeft(102, ' ') + "\n\n\n\n";
				
					mailContent += "[ACCOUNT BY SECURITY]\n\n";

					mailContent += "Group Code".PadRight(12, ' ') 
						+ "SecId".PadRight(15, ' ') 
						+ "Symbol".PadRight(12, ' ')
						+ "Account Number".PadRight(16, ' ') 												
						+	"Quantity Shorted".PadLeft(15, ' ')
						+	"Quantity Covered".PadLeft(17, ' ') + "\n" ;

					mailContent += "----------".PadRight(12, ' ') 
						+ "-----".PadRight(15, ' ') 
						+ "------".PadRight(12, ' ')
						+ "--------------".PadRight(16, ' ') 
						+	"----------------".PadLeft(15, ' ')
						+	"----------------".PadLeft(17, ' ') + "\n";						

					dbCn.Close();
					dataReader.Close();
					
					sqlCmd = new SqlCommand("spShortSaleBillingSummaryBySecurityAccountGet", dbCn);
					sqlCmd.CommandType = CommandType.StoredProcedure;
					sqlCmd.CommandTimeout = 600;    
        
					paramStartDate = sqlCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
					paramStartDate.Value = startDate;
				
					paramStopDate = sqlCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
					paramStopDate.Value = stopDate;

					dbCn.Open();
					dataReader = sqlCmd.ExecuteReader();
					
					totalCharge = 0;

					while (dataReader.Read())
					{						
						groupCode = dataReader.GetValue(1).ToString();
						accountNumber = dataReader.GetValue(4).ToString();
						secId = dataReader.GetValue(2).ToString();
						symbol = dataReader.GetValue(3).ToString();
						quantityShorted = dataReader.GetValue(5).ToString();
						quantityCovered = dataReader.GetValue(6).ToString();
						
						
						mailContent += groupCode.PadRight(12, ' ')
							+ secId.PadRight(15, ' ') 
							+ symbol.PadRight(12, ' ')								
							+ accountNumber.PadRight(16, ' ') 
							+ quantityShorted.PadLeft(15, ' ')
							+ quantityCovered.PadLeft(17, ' ') + "\n";
										
						++itemCount;
					}
				
					mailContent +=  ("Total Charges: " + totalCharge.ToString("#,##0.00")).PadLeft(102, ' ') + "\n\n\n\n";
				}
				catch (Exception error)
				{
					Log.Write(error.Message + " [ShortSaleNegativeRebateBillDocument.MasterBill]", Log.Error, 1); 
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleNegativeRebateBillDocument.MasterBill]", Log.Error, 1); 
			}
			finally
			{
				if ((dataReader != null) && (!dataReader.IsClosed))
				{
					dataReader.Close();
				}
          
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}

			if (itemCount == 0)
			{
				mailContent += "[None for today.]\n";
			}
				
			Log.Write("Listed " + itemCount + " short sale accounts covered as added in e-mail notification. [MedalistMain.MasterBill]", 2);     


			return mailContent;
		}
			
		public string MasterBill()
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();
			string masterBill = "";
			string temp = "";
			int counter = 0;

			try
			{
				SqlCommand dbCmd = new SqlCommand("spTradingGroupGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				
				SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);			
				paramTradeDate.Value = bizDate;
										
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "TradingGroups");				
				
				dbCmd.Parameters.Remove(paramTradeDate);

				groupCode	= "";
				masterBill	 = "Master Bill - Hard To Borrow Charges\r\n";
				masterBill	+= "Billing for " + DateTime.Parse(startDate).ToString(Standard.DateFormat) + " to " + DateTime.Parse(stopDate).ToString(Standard.DateFormat) + "\r\n\r\n";
				masterBill	+= "!";
				masterBill	+= "Master Bill\r\n";
				masterBill	+= MasterCorrespondentSummary(startDate, stopDate, "");
				masterBill  += PreBorrowSummary(startDate, stopDate, "");

				foreach (DataRow dr in dataSet.Tables["TradingGroups"].Rows)
				{	
					if (bool.Parse(dr["NegativeRebateBill"].ToString()))
					{
						SqlParameter paramStartDate;
						SqlParameter paramStopDate;
						SqlParameter paramGroupCode;					
						DataSet tempDataSet = new DataSet();

						try
						{
							groupCode		= dr["GroupCode"].ToString();											
							string page = "";

							dbCmd = new SqlCommand("spShortSaleBillingSummaryGet", dbCn);
							dbCmd.CommandType = CommandType.StoredProcedure;
							dbCmd.CommandTimeout = 600;    
        
							paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
							paramStartDate.Value = startDate;
				
							paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
							paramStopDate.Value = stopDate;

							if (!groupCode.Equals(""))
							{
								paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
								paramGroupCode.Value = groupCode;
							}

							dataAdapter = new SqlDataAdapter(dbCmd);   
							dataAdapter.Fill(tempDataSet, "BillingSummary");				
										
							
							if (tempDataSet.Tables["BillingSummary"].Rows.Count  == 0)
							{
								masterBill += "";
							}
							else
							{
								header = groupCode.ToUpper() + " - " + TradingGroupNameGet(groupCode) + " - Hard To Borrow Charges\r\n";
								masterBill	+= header;
								counter ++;
								if (counter > LINES_PER_PAGE)
								{
									page+="!";
									page += header + "\r\n";
									counter = 2;
								}					
								
								
								masterBill	+= "\r\nAccount(s) Summary \r\n\r\n";

								counter += 2;
								if (counter > LINES_PER_PAGE)
								{
									page+="!";
									page += header + "\r\n";
									counter = 2;
								}					
								
								masterBill += "Summary for " + DateTime.Parse(startDate).ToString(Standard.DateFormat) + " to " + DateTime.Parse(stopDate).ToString(Standard.DateFormat) + "\r\n\r\n";
								
								counter += 2;
								if (counter > LINES_PER_PAGE)
								{
									page+="!";
									page += header + "\r\n";
									counter = 2;
								}					
								
								masterBill	+="Group Code".PadRight(15, ' ') 
									+ "Account Number".PadRight(16, ' ') 	
									+ "Symbol".PadRight(9, ' ')		
									+	"Charge".PadLeft(10, ' ')+ "\r\n" ;
								counter ++;
								if (counter > LINES_PER_PAGE)
								{
									page+="!";
									page += header + "\r\n";
									counter = 2;
								}		

								masterBill	+="----------".PadRight(15, ' ') 
									+ "--------------".PadRight(16, ' ') 			
									+ "------".PadRight(9, ' ')
									+	"------".PadLeft(10, ' ')+ "\r\n" ;
								counter ++;
								if (counter > LINES_PER_PAGE)
								{
									page+="!";
									page += header + "\r\n";
									counter = 2;
								}		

								int index  = 0;							
								decimal totalModifiedCharge = 0;

								foreach (DataRow tempDr in tempDataSet.Tables["BillingSummary"].Rows)
								{
									page +=	tempDr["GroupCode"].ToString().PadRight(15, ' ') +										
										tempDr["AccountNumber"].ToString().PadRight(16, ' ') + 			
										tempDr["Symbol"].ToString().PadRight(9, ' ') + 
										decimal.Parse(tempDr["ModifiedCharge"].ToString()).ToString("#,##0.00").PadLeft(10, ' ') + "\r\n";								
					
									totalModifiedCharge += decimal.Parse(tempDr["ModifiedCharge"].ToString());			
					
									index++;
									counter ++;	
					
									if (counter > LINES_PER_PAGE)
									{
										page+="!";
										page += header + "\r\n";
										counter = 2;
									}								
								}																					
			 
								temp = "Total Charge: " + totalModifiedCharge.ToString("#,##0.00");
								page += "\r\n" + new string(' ', 50 - temp.Length) + temp + "\r\n\r\n";

								counter += 2;
								if (counter > LINES_PER_PAGE)
								{
									page+="!";
									page += header + "\r\n";
									counter = 2;
								}																					
								masterBill += page;						
							}						
						}
						catch (Exception error)
						{
							Log.Write(error.Message + " [ShortSaleNegativeRebateBillDocument.AccountSummary]", Log.Error, 1);
						}					
					}
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleNegativeRebateBillDocument.MasterBill]", Log.Error, 1);
			}

			return masterBill;
		}

		public string CorresspondentBill(bool coverPage, bool accountSummary)
		{
			DataSet dataSet = new DataSet();
			
			int counter = 0;
			bool summaryDone = false;
			decimal totalModifiedCharge = 0;
			string accountNumber = "";
			string temp = "";
			string header = "";

			try
			{		
				SqlCommand dbCmd = new SqlCommand("spShortSaleBillingSummaryGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				
				SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);			
				paramStartDate.Value = startDate;
				
				SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);			
				paramStopDate.Value = stopDate;
						
				SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
				paramGroupCode.Value = groupCode;
				
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);   
				dataAdapter.Fill(dataSet, "BillingSummaryByAccount");

				dbCmd = new SqlCommand("spShortSaleBillingSummaryBySecurityGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				
				paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);			
				paramStartDate.Value = startDate;
				
				paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);			
				paramStopDate.Value = stopDate;
				
				paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
				paramGroupCode.Value = groupCode;
				
				dataAdapter = new SqlDataAdapter(dbCmd);   
				dataAdapter.Fill(dataSet, "BillingSummaryBySecurity");			
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleNegativeRebateBillDocument.CorresspondentBill]", Log.Error, 1);
			}

			if (dataSet.Tables["BillingSummaryByAccount"].Rows.Count == 0)
			{
				return "";
			}
			
			page = "";
			header = groupCode.ToUpper() + " - " + TradingGroupNameGet(groupCode) + " - Hard To Borrow Charges\r\n";
			
			
			if (coverPage)
			{
				page += header;				
				page += "Billing for " + DateTime.Parse(startDate).ToString(Standard.DateFormat) + " to " + DateTime.Parse(stopDate).ToString(Standard.DateFormat) + "\r\n\r\n";
				page += "!";
			}
										
			page += header;
			page += "\r\nSummary for " + DateTime.Parse(startDate).ToString(Standard.DateFormat) + " to " + DateTime.Parse(stopDate).ToString(Standard.DateFormat) + "\r\n\r\n";
					
		
			page += "Date".PadRight(13, ' ') + 
				"Group Code".PadRight(12, ' ') +											
				"CUSIP".PadRight(12, ' ') + 
				"Symbol".PadRight(8, ' ') + 
				"Quantity".PadLeft(11, ' ') +
				"Rate".PadLeft(9, ' ') +
				"Charge".PadLeft(10, ' ') + "\r\n";
			
			
			page += "----------".PadRight(13, ' ') + 
				"----------".PadRight(12, ' ') +		
				"---------".PadRight(12, ' ') + 
				"------".PadRight(8, ' ') + 
				"--------".PadLeft(11, ' ') +
				"----".PadLeft(9, ' ') +
				"------".PadLeft(10, ' ') + "\r\n";
			
			foreach (DataRow dr in dataSet.Tables["BillingSummaryBySecurity"].Rows)
			{
				if (!dr["Charge"].ToString().Equals(""))
				{
					page +=	DateTime.Parse(dr["BizDate"].ToString()).ToString(Standard.DateFormat).PadRight(13, ' ') + 
						dr["GroupCode"].ToString().PadRight(12, ' ') +																
						dr["SecId"].ToString().PadRight(12, ' ') + 
						dr["Symbol"].ToString().PadRight(8, ' ') + 
						dr["QuantityCovered"].ToString().PadLeft(11, ' ') +						
						((!dr["MarkupRate"].ToString().Equals(""))?decimal.Parse(dr["MarkupRate"].ToString()).ToString("0.000"): "").PadLeft(9, ' ') +
						decimal.Parse(dr["Charge"].ToString()).ToString("#,##0.00").PadLeft(10, ' ').PadLeft(10, ' ') + "\r\n";		
		
					totalModifiedCharge += decimal.Parse(dr["Charge"].ToString());			

					counter ++;	
					
					if (counter > LINES_PER_PAGE)
					{
						page+="!";
						page += header + "\r\n";
						counter = 0;
					}
				}
			}																					
			 
			temp = "Total Charge: " + totalModifiedCharge.ToString("#,##0.00");
			page	+= "\r\n" + new string(' ', 75 - temp.Length) + temp + "\r\n";
			page	+= "!";

			
			if (accountSummary)
			{
				page += AccountSummary(startDate, stopDate, header);
			}

			page += PreBorrowSummary(startDate, stopDate, header);

			accountNumber = "";
			totalModifiedCharge = 0;

			counter = 0;

			foreach (DataRow dr in dataSet.Tables["BillingSummaryByAccount"].Rows)
			{
				if (!dr["ModifiedCharge"].ToString().Equals(""))
				{
					if (!accountNumber.Equals(dr["AccountNumber"].ToString()))
					{
						
						if (!summaryDone)
						{
							summaryDone = true;
							accountNumber = dr["AccountNumber"].ToString();
							page += header;
							page += "\r\n" + groupCode + " - " + dr["AccountNumber"].ToString() + "\r\n\r\n";
						
							page += "Date".PadRight(13, ' ') + 
								"Group Code".PadRight(12, ' ') +							
								"Account Number".PadRight(17, ' ') + 
								"CUSIP".PadRight(12, ' ') + 
								"Symbol".PadRight(8, ' ') + 
								"Quantity".PadLeft(11, ' ') +
								"Rate".PadLeft(9, ' ') +
								"Charge".PadLeft(10, ' ') + "\r\n";
			
							page += "----------".PadRight(13, ' ') + 
								"----------".PadRight(12, ' ') +
								"--------------".PadRight(17, ' ') + 
								"---------".PadRight(12, ' ') + 
								"------".PadRight(8, ' ') + 
								"--------".PadLeft(11, ' ') +
								"----".PadLeft(9, ' ') +
								"------".PadLeft(10, ' ') + "\r\n";
						}
						else
						{
							accountNumber = dr["AccountNumber"].ToString();
							temp = "Total Charge: " + totalModifiedCharge.ToString("#,##0.00");
							page += "\r\n" + new string(' ', 92 - temp.Length) + temp + "\r\n\r\n";		
							page += "!";
							page += header;
							page += "\r\n" + groupCode + " - " + dr["AccountNumber"].ToString() + "\r\n\r\n";
						
											
							page += "Date".PadRight(13, ' ') + 
								"Group Code".PadRight(12, ' ') +							
								"Account Number".PadRight(17, ' ') + 
								"CUSIP".PadRight(12, ' ') + 
								"Symbol".PadRight(8, ' ') + 
								"Quantity".PadLeft(11, ' ') +
								"Rate".PadLeft(9, ' ') +
								"Charge".PadLeft(10, ' ') + "\r\n";
			
							page += "----------".PadRight(13, ' ') + 
								"----------".PadRight(12, ' ') +
								"--------------".PadRight(17, ' ') + 
								"---------".PadRight(12, ' ') + 
								"------".PadRight(8, ' ') + 
								"--------".PadLeft(11, ' ') +
								"----".PadLeft(9, ' ') +
								"------".PadLeft(10, ' ') + "\r\n";
							
							totalModifiedCharge = 0;
							counter = 0;
						}
					}	

					page +=	DateTime.Parse(dr["BizDate"].ToString()).ToString(Standard.DateFormat).PadRight(13, ' ') + 
						dr["GroupCode"].ToString().PadRight(12, ' ') +										
						dr["AccountNumber"].ToString().PadRight(17, ' ') + 
						dr["SecId"].ToString().PadRight(12, ' ') + 
						dr["Symbol"].ToString().PadRight(8, ' ') + 
						dr["QuantityCovered"].ToString().PadLeft(11, ' ') +						
						((!dr["MarkupRate"].ToString().Equals(""))?decimal.Parse(dr["MarkupRate"].ToString()).ToString("0.000"): "").PadLeft(9, ' ') +
						decimal.Parse(dr["ModifiedCharge"].ToString()).ToString("#,##0.00").PadLeft(10, ' ') + "\r\n";	
					
					totalModifiedCharge += decimal.Parse(dr["ModifiedCharge"].ToString());			
				
					counter ++;	
					
					if (counter > LINES_PER_PAGE)
					{
						page+="!";
						page += header;
						page += "\r\n" + groupCode + " - " + dr["AccountNumber"].ToString() + "\r\n\r\n";
										
						page += "Date".PadRight(13, ' ') + 
							"Group Code".PadRight(12, ' ') +							
							"Account Number".PadRight(17, ' ') + 
							"CUSIP".PadRight(12, ' ') + 
							"Symbol".PadRight(8, ' ') + 
							"Quantity".PadLeft(11, ' ') +
							"Rate".PadLeft(9, ' ') +
							"Charge".PadLeft(10, ' ') + "\r\n";
			
						page += "----------".PadRight(13, ' ') + 
							"----------".PadRight(12, ' ') +
							"--------------".PadRight(17, ' ') + 
							"---------".PadRight(12, ' ') + 
							"------".PadRight(8, ' ') + 
							"--------".PadLeft(11, ' ') +
							"----".PadLeft(9, ' ') +
							"------".PadLeft(10, ' ') + "\r\n";
						counter = 0;
					}			
				}
			}						

			temp = "Total Charge: " + totalModifiedCharge.ToString("#,##0.00");
			page += "\r\n" + new string(' ', 92 - temp.Length) + temp + "\r\n\r\n";		
			page += "!";			
			
			return page;
		}

		public string TradingGroupNameGet(string groupCode)
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();
			string groupCodeName = "";

			try
			{
				SqlCommand dbCmd = new SqlCommand("spTradingGroupGet", localDbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				
				SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);			
				paramTradeDate.Value = bizDate;
      	
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "TradingGroups");				
			
				foreach(DataRow dr in dataSet.Tables["TradingGroups"].Rows)
				{
					if (dr["GroupCode"].ToString().Equals(groupCode))
					{
						groupCodeName = dr["GroupName"].ToString();
					}
				}			
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleNegativeRebateBillDocument.TradingGroupNameGet]", Log.Error, 1);
			}

			return groupCodeName;
		}
	
		
		public string MasterCorrespondentSummary(string startDate, string stopDate, string header)
		{
			string page = "";
			string temp = "";

			SqlCommand dbCmd;
			SqlParameter paramStartDate;
			SqlParameter paramStopDate;
			SqlParameter paramGroupCode;
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCmd = new SqlCommand("spShortSaleBillingSummaryCorrespondentSummary", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				dbCmd.CommandTimeout = 600;    
        
				paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
				paramStartDate.Value = startDate;
				
				paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
				paramStopDate.Value = stopDate;

				if (!groupCode.Equals(""))
				{
					paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
					paramGroupCode.Value = groupCode;
				}

				dataAdapter = new SqlDataAdapter(dbCmd);   
				dataAdapter.Fill(dataSet, "BillingCorrespondentSummary");				
			
				if (dataSet.Tables["BillingCorrespondentSummary"].Rows.Count  == 0)
				{
					return "";
				}
				
				page	+= header;
				page	+= "\r\nCorrespondent(s) Summary\r\n\r\n";
				page += "Summary for " + DateTime.Parse(startDate).ToString(Standard.DateFormat) + " to " + DateTime.Parse(stopDate).ToString(Standard.DateFormat) + "\r\n\r\n";
		
				page	+="Group Code".PadRight(15, ' ') 																	
					+	"Charge".PadLeft(14, ' ')+ "\n" ;

				page	+="----------".PadRight(15, ' ') 						
					+	"------".PadLeft(14, ' ')+ "\n" ;

				int index  = 0;
				int counter = 0;
				decimal totalModifiedCharge = 0;

				foreach (DataRow dr in dataSet.Tables["BillingCorrespondentSummary"].Rows)
				{
					if (!dr["Charge"].ToString().Equals(""))
					{
						page +=	dr["GroupCode"].ToString().PadRight(15, ' ') +													
							decimal.Parse(dr["Charge"].ToString()).ToString("#,##0.00").PadLeft(14, ' ') + "\r\n";								
					
						totalModifiedCharge += decimal.Parse(dr["Charge"].ToString());			
					
						index++;
						counter ++;	
					
						if (counter > LINES_PER_PAGE)
						{
							page+="!";
							page += header + "\r\n";
							counter = 0;
						}
					}
				}																					
			 
				temp = "Total Charge: " + totalModifiedCharge.ToString("#,##0.00");
				page += "\r\n"+ new string(' ', 29 - temp.Length) + temp +"\r\n";
				page += "!";									
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleNegativeRebateBillDocument.MasterCorrespondentSummary]", Log.Error, 1);
			}

			return page;
		}

		public string AccountSummary(string startDate, string stopDate, string header)
		{
			string page = "";
			string temp = "";

			SqlCommand dbCmd;
			SqlParameter paramStartDate;
			SqlParameter paramStopDate;
			SqlParameter paramGroupCode;
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCmd = new SqlCommand("spShortSaleBillingSummaryAccountsSummary", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				dbCmd.CommandTimeout = 600;    
        
				paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
				paramStartDate.Value = startDate;
				
				paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
				paramStopDate.Value = stopDate;

				if (!groupCode.Equals(""))
				{
					paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
					paramGroupCode.Value = groupCode;
				}

				dataAdapter = new SqlDataAdapter(dbCmd);   
				dataAdapter.Fill(dataSet, "BillingAccountsSummary");				
			
				if (dataSet.Tables["BillingAccountsSummary"].Rows.Count  == 0)
				{
					return "";
				}
				
				page	+= header;
				page	+= "\r\nAccount(s) Summary\r\n\r\n";
				page += "Summary for " + DateTime.Parse(startDate).ToString(Standard.DateFormat) + " to " + DateTime.Parse(stopDate).ToString(Standard.DateFormat) + "\r\n\r\n";
		
				page	+="Group Code".PadRight(15, ' ') 
					+ "Account Number".PadRight(16, ' ') 												
					+	"Charge".PadLeft(14, ' ')+ "\n" ;

				page	+="----------".PadRight(15, ' ') 
					+ "--------------".PadRight(16, ' ') 												
					+	"------".PadLeft(14, ' ')+ "\n" ;
	
				int index  = 0;
				int counter = 0;
				decimal totalModifiedCharge = 0;

				foreach (DataRow dr in dataSet.Tables["BillingAccountsSummary"].Rows)
				{
					if (!dr["Charge"].ToString().Equals(""))
					{
						page +=	dr["GroupCode"].ToString().PadRight(15, ' ') +										
							dr["AccountNumber"].ToString().PadRight(16, ' ') + 						
							decimal.Parse(dr["Charge"].ToString()).ToString("#,##0.00").PadLeft(14, ' ') + "\r\n";								
					
						totalModifiedCharge += decimal.Parse(dr["Charge"].ToString());			
					
						index++;
						counter ++;	
					
						if (counter > LINES_PER_PAGE)
						{
							page+="!";
							page += header + "\r\n";
							counter = 0;
						}
					}
				}																					
			 
				temp = "Total Charge: " + totalModifiedCharge.ToString("#,##0.00");
				page += "\r\n" + new string(' ', 45 - temp.Length) + temp +"\r\n";
				page += "!";									
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleNegativeRebateBillDocument.AccountSummary]", Log.Error, 1);
			}

			return page;
		}

		public string PreBorrowSummary(string startDate, string stopDate, string header)
		{
			string page = "";
			string temp = "";

			SqlCommand dbCmd;
			SqlParameter paramStartDate;
			SqlParameter paramStopDate;
			SqlParameter paramGroupCode;
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
				dbCmd = new SqlCommand("spShortSaleBillingPreBorrowSummary", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				dbCmd.CommandTimeout = 600;    
        
				paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
				paramStartDate.Value = startDate;
				
				paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
				paramStopDate.Value = stopDate;

				if (!groupCode.Equals(""))
				{
					paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
					paramGroupCode.Value = groupCode;
				}

				dataAdapter = new SqlDataAdapter(dbCmd);   
				dataAdapter.Fill(dataSet, "BillingPreBorrowSummary");				
			
				if (dataSet.Tables["BillingPreBorrowSummary"].Rows.Count  == 0)
				{
					return "";
				}
				
				page	+= header;
				page	+= "\r\nPreBorrow (s) Summary\r\n\r\n";
				page += "Summary for " + DateTime.Parse(startDate).ToString(Standard.DateFormat) + " to " + DateTime.Parse(stopDate).ToString(Standard.DateFormat) + "\r\n\r\n";
		
				page += "Date".PadRight(13, ' ') + 
					"Group Code".PadRight(12, ' ') +							
					"CUSIP".PadRight(12, ' ') + 
					"Symbol".PadRight(8, ' ') + 
					"Quantity".PadLeft(11, ' ') +					
					"Charge".PadLeft(15, ' ') + "\r\n";
			
				page += "----------".PadRight(13, ' ') + 
					"----------".PadRight(12, ' ') +					
					"---------".PadRight(12, ' ') + 
					"------".PadRight(8, ' ') + 
					"--------".PadLeft(11, ' ') +					
					"------".PadLeft(15, ' ') + "\r\n";
	
				int index  = 0;
				int counter = 0;
				decimal totalModifiedCharge = 0;

				foreach (DataRow dr in dataSet.Tables["BillingPreBorrowSummary"].Rows)
				{
					if (!dr["Charge"].ToString().Equals(""))
					{
						page += DateTime.Parse(dr["BizDate"].ToString()).ToString("yyyy-MM-dd").PadRight(13, ' ') + 
							dr["GroupCode"].ToString().PadRight(12, ' ') +														
							dr["SecId"].ToString().PadRight(12, ' ') + 
							dr["Symbol"].ToString().PadRight(8, ' ') + 
							dr["QuantityCovered"].ToString().PadLeft(11, ' ') +					
							dr["Charge"].ToString().PadLeft(15, ' ') + "\r\n";
											
						totalModifiedCharge += decimal.Parse(dr["Charge"].ToString());			
					
						index++;
						counter ++;	
					
						if (counter > LINES_PER_PAGE)
						{
							page+="!";
							page += header + "\r\n";
							counter = 0;
						}
					}
				}																					
			 
				temp = "Total Charge: " + totalModifiedCharge.ToString("#,##0.00");
				page += "\r\n" + new string(' ', 71 - temp.Length) + temp +"\r\n";
				page += "!";									
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleNegativeRebateBillDocument.PreBorrowSummary]", Log.Error, 1);
			}

			return page;
		}
	}
}
