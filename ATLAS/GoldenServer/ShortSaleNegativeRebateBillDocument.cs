using System;
using System.Data;
using System.Data.SqlClient;
using StockLoan.Common;

namespace StockLoan.Golden
{

	/// Generates a loan Termination notice.

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
        string platForm = "";

		public ShortSaleNegativeRebateBillDocument(string dbCnStr, string startDate, string stopDate, string groupCode, string platForm)
		{
			this.dbCnStr = dbCnStr;
			this.dbCn = new SqlConnection(dbCnStr);
      
			this.startDate = startDate;
			this.stopDate = stopDate;
			this.groupCode = groupCode;
            this.platForm = platForm;
			this.bizDate = KeyValue.Get("BizDate", "0001-01-01", dbCn);		
		}		
			
			
		public string MasterBill()
		{
            SqlParameter paramStartDate;
            SqlParameter paramStopDate;
            SqlParameter paramGroupCode;

			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();
            string spProc = "";
			string masterBill = "";
			string temp = "";
			int counter = 0;

			try
			{

                if (platForm.ToLower().Equals("penson"))
                {
                    spProc = "spShortSaleBillingCorrespondentSummaryGet";
                }
                else
                {
                    spProc = "spShortSaleBillingCorrespondentBPSSummaryGet";
                }

				SqlCommand dbCmd = new SqlCommand(spProc, dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

                paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                paramStartDate.Value = startDate;

                paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
                paramStopDate.Value = stopDate;
										
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "TradingGroups");				
				

				groupCode	= "";
				masterBill	 = "Master Bill - Hard To Borrow Charges\r\n";
				masterBill	+= "Billing for " + DateTime.Parse(startDate).ToString(Standard.DateFormat) + " to " + DateTime.Parse(stopDate).ToString(Standard.DateFormat) + "\r\n\r\n";
				masterBill	+= "!";
				masterBill	+= "Master Bill\r\n";
				masterBill	+= MasterCorrespondentSummary(startDate, stopDate, "");


				foreach (DataRow dr in dataSet.Tables["TradingGroups"].Rows)
                {
                  
                    DataSet tempDataSet = new DataSet();

                    try
                    {
                        groupCode = dr["GroupCode"].ToString();	
                        string page = "";

						if (platForm.ToLower().Equals("penson"))
                        {
                            spProc = "spShortSaleBillingSummaryGet";
                        }
                        else
                        {
                            spProc = "spShortSaleBillingBPSSummaryGet";
                        }

						dbCmd = new SqlCommand(spProc, dbCn);
                        dbCmd.CommandType = CommandType.StoredProcedure;
                        dbCmd.CommandTimeout = 900;

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

                        if (tempDataSet.Tables["BillingSummary"].Rows.Count == 0)
                        {
                            masterBill += "";
                        }
                        else
                        {                          
                            if (platForm.ToLower().Equals("penson"))		
                            {
                                header = groupCode.ToUpper() + " - " + TradingGroupNameGet(groupCode) + " - Hard To Borrow Charges\r\n";
                            }
                            else
                            {
                                header = groupCode.ToUpper() + " - " + TradingGroupBpsNameGet(groupCode) + " - Hard To Borrow Charges\r\n";
                            }
                            
                            masterBill += header;
                            counter++;
                            if (counter > LINES_PER_PAGE)
                            {
                                page += "!";
                                page += header + "\r\n";
                                counter = 2;
                            }


                            masterBill += "\r\nAccount(s) Summary \r\n\r\n";

                            counter += 2;
                            if (counter > LINES_PER_PAGE)
                            {
                                page += "!";
                                page += header + "\r\n";
                                counter = 2;
                            }

                            masterBill += "Summary for " + DateTime.Parse(startDate).ToString(Standard.DateFormat) + " to " + DateTime.Parse(stopDate).ToString(Standard.DateFormat) + "\r\n\r\n";

                            counter += 2;
                            if (counter > LINES_PER_PAGE)
                            {
                                page += "!";
                                page += header + "\r\n";
                                counter = 2;
                            }

                            masterBill += "Group Code".PadRight(15, ' ')
                                + "Account Number".PadRight(16, ' ')
                                + "Symbol".PadRight(9, ' ')
                                + "Charge".PadLeft(10, ' ') + "\r\n";
                            counter++;
                            if (counter > LINES_PER_PAGE)
                            {
                                page += "!";
                                page += header + "\r\n";
                                counter = 2;
                            }

                            masterBill += "----------".PadRight(15, ' ')
                                + "--------------".PadRight(16, ' ')
                                + "------".PadRight(9, ' ')
                                + "------".PadLeft(10, ' ') + "\r\n";
                            counter++;
                            if (counter > LINES_PER_PAGE)
                            {
                                page += "!";
                                page += header + "\r\n";
                                counter = 2;
                            }

                            int index = 0;
                            decimal totalModifiedCharge = 0;

							//Detail loop forEach GroupCode 
                            foreach (DataRow tempDr in tempDataSet.Tables["BillingSummary"].Rows)  
                            {

								try
                                {
                                    page += tempDr["GroupCode"].ToString().PadRight(15, ' ') +
                                        tempDr["AccountNumber"].ToString().PadRight(16, ' ') +
                                        tempDr["Symbol"].ToString().PadRight(9, ' ') +
                                        decimal.Parse(tempDr["ModifiedCharge"].ToString()).ToString("#,##0.00").PadLeft(10, ' ') + "\r\n";

                                    totalModifiedCharge += decimal.Parse(tempDr["ModifiedCharge"].ToString());

                                    index++;
                                    counter++;

                                    if (counter > LINES_PER_PAGE)
                                    {
                                        page += "!";
                                        page += header + "\r\n";
                                        counter = 2;
                                    }
                                }
                                catch { }

                            }  //Detail page line items foreach loop

                            temp = "Total Charge: " + totalModifiedCharge.ToString("#,##0.00");
                            page += "\r\n" + new string(' ', 50 - temp.Length) + temp + "\r\n\r\n";

                            counter += 2;
                            if (counter > LINES_PER_PAGE)
                            {
                                page += "!";
                                page += header + "\r\n";
                                counter = 2;
                            }
                            masterBill += page;
                        }

                    }	//Detail page 
                    catch (Exception error)
                    {
						Log.Write(error.Message + " [ShortSaleNegativeRebateBillDocument.MasterBill]", Log.Error, 1);
                    }
                }
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ShortSaleNegativeRebateBillDocument.MasterBill]", Log.Error, 1);
			}

			return masterBill;
		}

		public DataSet MasterBillExcel()
		{	

			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dsMasterBillExcel = new DataSet();

            SqlParameter paramStartDate;
            SqlParameter paramStopDate;
			SqlParameter paramTradeDate;

            string spProc1 = "";
			string spProc2 = "";
			string spProc3 = ""; 

			try
			{
				if (platForm.ToLower().Equals("penson"))
				{
					spProc1 = "dbo.spShortSaleBillingCorrespondentSummaryGet";
					spProc2 = "dbo.spShortSaleBillingSummaryGet";
					spProc3 = "dbo.spTradingGroupGet";
				}
				else
				{
					spProc1 = "dbo.spShortSaleBillingCorrespondentBPSSummaryGet";
					spProc2 = "dbo.spShortSaleBillingBPSSummaryGet";
					spProc3 = "dbo.spTradingGroupBPSGet";
				}

				// Create First table in the same dataset 
				SqlCommand dbCmd = new SqlCommand(spProc1, dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				dbCmd.CommandTimeout = 900;

				paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
				paramStartDate.Value = startDate;

				paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
				paramStopDate.Value = stopDate;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsMasterBillExcel, "TradingGroups");

				// Create second table in the same dataset 
				dbCmd = new SqlCommand(spProc2, dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				dbCmd.CommandTimeout = 900;

				paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);	
				paramStartDate.Value = startDate;

				paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);		
				paramStopDate.Value = stopDate;

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsMasterBillExcel, "BillingSummary");

				// Create second table in the same dataset 
				dbCmd = new SqlCommand(spProc3, dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				dbCmd.CommandTimeout = 600;
                if (platForm.ToLower().Equals("penson"))
                {
                    paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                    paramTradeDate.Value = bizDate;		//Base class attribute 
                }
   				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsMasterBillExcel, "TradingGroupNames");

				return dsMasterBillExcel;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleNegativeRebateBillDocument.MasterBillExcel]", Log.Error, 1);
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

		public string CorresspondentBill(bool coverPage, bool accountSummary)
		{
			DataSet dataSet = new DataSet();
			
			int counter = 0;
			bool summaryDone = false;
			decimal totalModifiedCharge = 0;
			string accountNumber = "";
			string temp = "";
			string header = "";
            string spProc = "";

			try
			{
                if (platForm.ToLower().Equals("penson"))
                {
                    spProc = "spShortSaleBillingSummaryGet";
                }
                else
                {
                    spProc = "spShortSaleBillingBPSSummaryGet";
                }


				SqlCommand dbCmd = new SqlCommand(spProc, dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				
				SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);			
				paramStartDate.Value = startDate;
				
				SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);			
				paramStopDate.Value = stopDate;
						
				SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);			
				paramGroupCode.Value = groupCode;
				
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);   
				dataAdapter.Fill(dataSet, "BillingSummaryByAccount");

                if (platForm.ToLower().Equals("penson"))
                {
                    spProc = "spShortSaleBillingSummaryBySecurityGet";
                }
                else
                {
                    spProc = "spShortSaleBillingSummaryBySecurityBPSGet";
                }

                dbCmd = new SqlCommand(spProc, dbCn);
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

            if (platForm.Equals("penson"))
            {
                header = groupCode.ToUpper() + " - " + TradingGroupNameGet(groupCode) + " - Hard To Borrow Charges\r\n";
            }
            else
            {
                header = groupCode.ToUpper() + " - " + TradingGroupBpsNameGet(groupCode) + " - Hard To Borrow Charges\r\n";
            }
			
			
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


        // Wrapping Excel Correspondent Bill data calls with Rebate Agent and call to ShortSaleNegativeRebateBillDocument class
		

        internal DataSet ExcelCorresspondentBill(string startDate, string stopDate, string groupCode, string platForm)
        {

            DataSet dataSet = new DataSet();
            string spProc = "";

            try
            {
                // First recordset to be returned 

                if (platForm.ToLower().Equals("penson"))
                {
                    spProc = "spShortSaleBillingSummaryAccountsSummary";
                }
                else
                {
                    spProc = "spShortSaleBillingSummaryAccountsBPSSummary";
                }

                SqlCommand dbCmd = new SqlCommand(spProc, dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramStartDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                paramStartDate.Value = startDate;

                SqlParameter paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
                paramStopDate.Value = stopDate;

                SqlParameter paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                paramGroupCode.Value = groupCode;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "ExcelAccountsSummary");


                // Second recordset to be returned //
                if (platForm.ToLower().Equals("penson"))
                {
                    spProc = "spShortSaleBillingSummaryBySecurityGet";
                }
                else
                {
                    spProc = "spShortSaleBillingSummaryBySecurityBPSGet";
                }

                SqlParameter paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                paramTradeDate.Value = startDate;

                dbCmd = new SqlCommand(spProc, dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                paramTradeDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                paramTradeDate.Value = startDate;

                paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
                paramStopDate.Value = stopDate;

                paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                paramGroupCode.Value = groupCode;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "ExcelSummaryBySecurity");

                // Third recordset to be returned //
                if (platForm.ToLower().Equals("penson"))
                {
                    spProc = "spShortSaleBillingSummaryGet";
                }
                else
                {
                    spProc = "spShortSaleBillingSummaryBPSGet";
                }

                dbCmd = new SqlCommand(spProc, dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                paramTradeDate = dbCmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
                paramTradeDate.Value = startDate;

                paramStopDate = dbCmd.Parameters.Add("@StopDate", SqlDbType.DateTime);
                paramStopDate.Value = stopDate;

                paramGroupCode = dbCmd.Parameters.Add("@GroupCode", SqlDbType.VarChar, 5);
                paramGroupCode.Value = groupCode;

                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "ExcelBillingSummary");

                if (platForm.ToLower().Equals("penson"))
                {
                    spProc = "dbo.spTradingGroupGet";
                }
                else
                {
                    spProc = "dbo.spTradingGroupBPSGet";
                }

                dbCmd = new SqlCommand(spProc, dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = 600;
                if (platForm.ToLower().Equals("penson"))
                {
                    paramTradeDate = dbCmd.Parameters.Add("@TradeDate", SqlDbType.DateTime);
                    paramTradeDate.Value = bizDate;		//Base class attribute 
                }
                dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "TradingGroupNames");

                return dataSet;

            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ShortSaleNegativeRebateBillDocument.ExcelCorresspondentBill]", Log.Error, 1);
            }

            if (dataSet.Tables["ExcelBillingSummary"].Rows.Count == 0)
            {
                return null;
            }

            return dataSet;
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

                foreach (DataRow dr in dataSet.Tables["TradingGroups"].Rows)
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

        public string TradingGroupBpsNameGet(string groupCode)
        {
            SqlConnection localDbCn = new SqlConnection(dbCnStr);
            DataSet dataSet = new DataSet();
            string groupCodeName = "";

            try
            {

                SqlCommand dbCmd = new SqlCommand("spTradingGroupBpsGet", localDbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;


                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dataSet, "TradingGroups");

                foreach (DataRow dr in dataSet.Tables["TradingGroups"].Rows)
                {
                    if (dr["GroupCode"].ToString().Equals(groupCode))
                    {
                        groupCodeName = dr["GroupName"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [ShortSaleNegativeRebateBillDocument.TradingGroupBpsNameGet]", Log.Error, 1);
            }

            return groupCodeName;
        }

    

		public string MasterCorrespondentSummary(string startDate, string stopDate, string header)
		{
			string page = "";
			string temp = "";
            string spProc = "";

			SqlCommand dbCmd;
			SqlParameter paramStartDate;
			SqlParameter paramStopDate;
			SqlParameter paramGroupCode;
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
                if (platForm.ToLower().Equals("penson"))
                {
                    spProc = "spShortSaleBillingCorrespondentSummaryGet";
                }
                else
                {
                    spProc = "spShortSaleBillingCorrespondentBPSSummaryGet";
                }
                                
                dbCmd = new SqlCommand(spProc, dbCn);
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
                    if (!dr["ClientCharge"].ToString().Equals(""))
					{
						page +=	dr["GroupCode"].ToString().PadRight(15, ' ') +													
							decimal.Parse(dr["ClientCharge"].ToString()).ToString("#,##0.00").PadLeft(14, ' ') + "\r\n";								
					
						totalModifiedCharge += decimal.Parse(dr["ClientCharge"].ToString());			
					
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
            string spProc = "";

			SqlCommand dbCmd;
			SqlParameter paramStartDate;
			SqlParameter paramStopDate;
			SqlParameter paramGroupCode;
			SqlDataAdapter dataAdapter;
			DataSet dataSet = new DataSet();

			try
			{
                if (platForm.ToLower().Equals("penson"))
                {
                    spProc = "spShortSaleBillingSummaryAccountsSummary";
                }
                else
                {
                    spProc = "spShortSaleBillingSummaryAccountsBPSSummary";
                }

				dbCmd = new SqlCommand(spProc, dbCn);
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
	}
}
