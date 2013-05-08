using System; 
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using StockLoan.DataAccess;
using System.Collections.ObjectModel;       //for ReadOnlyCollection <T>
using System.Net;                           //for hostName and IP address 
using System.IO;                            //DChen for StreamReader class 

namespace StockLoan.DataAccess
{
	public partial class DBAccessTestForm : Form
	{
		public DBAccessTestForm()
		{
			InitializeComponent();
		}

//=========================================================================================================

		private void btnSecMasterGet_Click(object sender, EventArgs e)
		{
			//DBSecMaster secMaster = new DBSecMaster();
			//secMaster.SecMasterGet("", "", "", "");

			int rowCount = 0;
			int displayCount = 0;
			string displayStr = "";
			DataSet dsSecMaster = new DataSet();

			dsSecMaster = DBSecMaster.SecMasterGet("","","","","");
			rowCount = dsSecMaster.Tables["SecMaster"].Rows.Count;

			labelProc.Text = "Proc: SecMasterGet";
			txtResult.Text = "Result: " + rowCount.ToString() + " rows." + "\r\n";

			foreach (DataRow dr in dsSecMaster.Tables["SecMaster"].Rows)
			{
				//if  (float.Parse(dr["Price"].ToString()) > 0.0)
				if (displayCount < 200) 
				{
					displayStr += "----------------------------\r\n"
									+ "itemSecId:\t\t" + dr["itemSecId"].ToString() + "\r\n"
									+ "Description:\t" + dr["Description"].ToString() + "\r\n"
									+ "itemSecId (Sedol)\t" + dr["Sedol"].ToString() + "\r\n"
									+ "ISIN\t\t" + dr["ISIN"].ToString() + "\r\n"
									+ "Symbol\t\t" + dr["Symbol"].ToString() + "\r\n"
									+ "Cusip:\t\t" + dr["Cusip"].ToString() + "\r\n"
									+ "Quick:\t\t" + dr["Quick"].ToString() + "\r\n"
									+ "BaseType\t" + dr["BaseType"].ToString() + "\r\n"
									+ "ClassGroup:\t" + dr["ClassGroup"].ToString() + "\r\n"
									+ "CountryCode:\t" + dr["CountryCode"].ToString() + "\r\n"
									+ "CurrencyIso:\t" + dr["CurrencyIso"].ToString() + "\r\n"
									+ "Price:\t\t" + dr["Price"].ToString() + "\r\n"
									+ "PriceDate:\t" + dr["PriceDate"].ToString() + "\r\n"
									+ "AccruedInterest:\t" + dr["AccruedInterest"].ToString() + "\r\n"
									+ "RecordDateCash:\t" + dr["RecordDateCash"].ToString() + "\r\n"
									+ "BorrowRate:\t" + dr["BorrowRate"].ToString() + "\r\n"
									+ "LoanRate:\t" + dr["LoanRate"].ToString() + "\r\n"
									+ "DividendRate:\t" + dr["DividendRate"].ToString() + "\r\n"
									+ "SecIdGroup:\t" + dr["SecIdGroup"].ToString() + "\r\n"
									+ "IsActive:\t\t" + dr["IsActive"].ToString() + "\r\n";
					displayCount++;
				}
				else
				{
					break;
				}
			}	// foreach DataRow loop 

			txtResult.Text = txtResult.Text + displayStr;
		}

		private void btnSecMasterSet_Click(object sender, EventArgs e)
		{   //Tested Okay 

			labelProc.Text = "Proc: SecMasterSet";
			try
			{
				// Test: Insert Symbol 'DCX.001' Into tbSecMaster, tbSecIdAlias, tbPrices
				DBSecMaster.SecMasterSet("DC-SEDOL-001", "DChen Test Security Insert", "E", "EQ", "US", "USD", "", "", "", "",
										 "DCX.001", "DC-ISIN-001", "DC-CUSIP-001", "99.56", "2010-10-19", false);

				// Test: Update Symbol 'DCX.001' into tbSecMaster, tbSecIdAlias, tbPrices
				DBSecMaster.SecMasterSet("DC-SEDOL-001", "DChen Test Security Update", "E", "EQ", "US", "USD", "", "", "", "",
										 "DCX.002", "DC-ISIN-002", "DC-CUSIP-002", "87.69", DateTime.Now.ToString("yyyy-MM-dd"), true);

				txtResult.Text = "Result: Completed without error";
			}
			catch { throw; }
		}

		private void btnSecIdSymbolLookup_Click(object sender, EventArgs e)
		{   //Tested Okay 

			labelProc.Text = "Proc:  SecIdSymbolLookup";

			string secId = null, 
				   symbol = null, 
				   sedol = null, 
				   isin = null, 
				   cusip = null, 
				   ibes_ticker = null;

			DBSecMaster.SecIdSymbolLookup("Dc-sedol-001", ref secId, ref symbol, ref sedol, ref isin, ref cusip, ref ibes_ticker);

			txtResult.Text = "itemSecId:\t'" + secId + "\r\nSymbol:\t" + symbol + "\r\nSEDOL:\t" + sedol + "\r\nISIN:\t" + isin + "\r\nCUSIP:\t" + cusip + "\r\nibes_ticker:\t" + ibes_ticker;
		}

		private void btnSecIdAliasSet_Click(object sender, EventArgs e)
		{   //Tested Okay 

			try
			{
				labelProc.Text = "Proc:  SecIdAliasSet";

				DBSecMaster.SecIdAliasSet("DC-SEDOL-001", "0", "DC-SEDOL-001");
				DBSecMaster.SecIdAliasSet("DC-SEDOL-001", "1", "DC-Isin-003");
				DBSecMaster.SecIdAliasSet("DC-SEDOL-001", "2", "DCX.003");
				DBSecMaster.SecIdAliasSet("DC-SEDOL-001", "3", "DC-Cusip-003");
				DBSecMaster.SecIdAliasSet("DC-SEDOL-001", "4", "DC-IBES-003");

				txtResult.Text = "Completed without error.";
			}
			catch { throw; }
		}

		private void btnPriceGet_Click(object sender, EventArgs e)
		{   //Tested Okay 

			int rowCount = 0;
			DataSet dsPrices = new DataSet();
			labelProc.Text = "Proc: spPricesGet";

			try
			{
				dsPrices = DBSecMaster.PricesGet("2010-10-19", "DC-Sedol-001", "USD");
				rowCount = dsPrices.Tables["Prices"].Rows.Count;
				txtResult.Text = "Result: " + rowCount.ToString() + " rows.\r\n";

				foreach (DataRow dr in dsPrices.Tables["Prices"].Rows)
				{
					txtResult.Text += "----------------\r\n" 
								+ "PriceDate:\t" + dr["PriceDate"].ToString() + "\r\n"
								+ "PriceDate(Short):\t" + DateTime.Parse(dr["PriceDate"].ToString()).ToString("yyyy-MM-dd") + "\r\n"
								+ "itemSecId:\t\t" + dr["itemSecId"].ToString() + "\r\n"
								+ "CountryCode:\t" + dr["CountryCode"].ToString() + "\r\n"
								+ "CurrencyIso:\t" + dr["CurrencyIso"].ToString() + "\r\n"
								+ "Price:\t\t" + float.Parse(dr["Price"].ToString()).ToString("0.000") + "\r\n";
				}

			}
			catch { throw; }
		}

		private void btnPriceSet_Click(object sender, EventArgs e)
		{   //Tested Okay 

			try
			{
				labelProc.Text = "Proc: spPricesSet";
				DBSecMaster.PriceSet(DateTime.Now.ToString("yyyy-MM-dd"), "DC-Sedol-001", "US", "USD", "123.998", DateTime.Now.ToString());
				txtResult.Text = "\r\n PriceSet completed without error.";
			}
			catch { throw; }
		}


//=========================================================================================================


		private void btnDealsGet_Click(object sender, EventArgs e)
		{	//Tested Okay

			//DataSet DealsGet(string dealId, string dealIdPrefix, string bizDate, bool isActive, short utcOffset)
			int rowCount = 0;
			int displayCount = 0;
			string displayStr = "";
			DataSet dsDeals = new DataSet();

			labelProc.Text = "Proc: spDealsGet";
			txtResult.Text = "";

			try
			{
				dsDeals = DBDeals.DealsGet("", "", "", true, 0);
				rowCount = dsDeals.Tables["Deals"].Rows.Count;
				displayStr = "Result: " + rowCount.ToString() + " rows.\r\n";

				foreach (DataRow dr in dsDeals.Tables["Deals"].Rows)
				{
					if (displayCount < 200)
					{
						displayStr += "----------------\r\n"
									+ "DealId:\t\t" + dr["DealId"].ToString() + "\r\n"
									+ "itemBookGroup:\t\t" + dr["itemBookGroup"].ToString() + "\r\n"
									+ "DealType:\t\t" + dr["DealType"].ToString() + "\r\n"
									+ "Book:\t\t" + dr["Book"].ToString() + "\r\n"
									+ "BookName:\t\t" + dr["BookName"].ToString() + "\r\n"
									+ "BookContact:\t\t" + dr["BookContact"].ToString() + "\r\n"
									+ "ContractId:\t\t" + dr["ContractId"].ToString() + "\r\n"
									+ "itemSecId:\t\t" + dr["itemSecId"].ToString() + "\r\n"
									+ "ISIN:\t\t" + dr["ISIN"].ToString() + "\r\n"
									+ "Symbol:\t\t" + dr["Symbol"].ToString() + "\r\n"
									+ "itemQuantity:\t\t" + dr["itemQuantity"].ToString() + "\r\n"
									+ "Amount:\t\t" + dr["Amount"].ToString() + "\r\n"
									+ "CollateralCode:\t\t" + dr["CollateralCode"].ToString() + "\r\n"
									+ "ValueDate:\t\t" + dr["ValueDate"].ToString() + "\r\n"
									+ "SettleDate:\t\t" + dr["SettleDate"].ToString() + "\r\n"
									+ "TermDate:\t\t" + dr["TermDate"].ToString() + "\r\n"
									+ "itemRate:\t\t" + dr["itemRate"].ToString() + "\r\n"
									+ "RateCode:\t\t" + dr["RateCode"].ToString() + "\r\n"
									+ "PoolCode:\t\t" + dr["PoolCode"].ToString() + "\r\n"
									+ "DivRate:\t\t" + dr["DivRate"].ToString() + "\r\n"
									+ "DivCallable:\t\t" + dr["DivCallable"].ToString() + "\r\n"
									+ "IncomeTracked:\t\t" + dr["IncomeTracked"].ToString() + "\r\n"
									+ "Margin:\t\t" + dr["Margin"].ToString() + "\r\n"
									+ "MarginCode:\t\t" + dr["MarginCode"].ToString() + "\r\n"
									+ "CurrencyIso:\t\t" + dr["CurrencyIso"].ToString() + "\r\n"
									+ "SecurityDepot:\t\t" + dr["SecurityDepot"].ToString() + "\r\n"
									+ "CashDepot:\t\t" + dr["CashDepot"].ToString() + "\r\n"
									+ "Comment:\t\t" + dr["Comment"].ToString() + "\r\n"
									+ "Fund:\t\t" + dr["Fund"].ToString() + "\r\n"
									+ "DealStatus:\t\t" + dr["DealStatus"].ToString() + "\r\n"
									+ "Actor:\t\t" + dr["Actor"].ToString() + "\r\n"
									+ "ActTime:\t\t" + dr["ActTime"].ToString() + "\r\n"
									+ "IsActive:\t\t" + dr["IsActive"].ToString() + "\r\n"
									+ "itemBizDate:\t\t" + dr["itemBizDate"].ToString() + "\r\n"
									+ "FeeAmount:\t\t" + dr["FeeAmount"].ToString() + "\r\n"
									+ "FeeCurrencyIso:\t\t" + dr["FeeCurrencyIso"].ToString() + "\r\n"
									+ "FeeType:\t\t" + dr["FeeType"].ToString() + "\r\n";
						displayCount++;
					}
					else
					{
						break;
					}
				}	//foreach data row Loop 

				txtResult.Text = txtResult.Text + displayStr;
			}
			catch { throw; }
		}

		private void btnDealSet_Click(object sender, EventArgs e)
		{	//Tested Okay 
			try
			{
				labelProc.Text = "Proc: DealSet";

				DBDeals.DealSet("20101019DEA12345", "PFSI", "B", "CIT INT", "", "", "DC-SEDOL-001", "100", "", "C", "2010-10-19", "2010-10-20",
					"", "-3.80", "", "R", "100.000", false, true, "%", "1.05", "USD", "JP", "JP", "Test record created by DChen 2010-10-19",
					"REBATE", "C", true, "dchen", false, "", "", "");

				txtResult.Text = "Result: Completed without error";
			}
			catch { throw; }

		}

		private void btnDealToContract_Click(object sender, EventArgs e)
		{	//Tested Okay 
			try
			{
				labelProc.Text = "Proc: spDealToContract";
				DBDeals.DealToContract("20101019DEA12345", "2010-10-19");
				txtResult.Text = "\r\n DealToContract completed without error.";
			}
			catch { throw; }

		}


//=========================================================================================================

		private void btnContractsGet_Click(object sender, EventArgs e)
		{	//Tested Okay 
			int rowCount = 0;
			DataSet dsContracts = new DataSet();
			labelProc.Text = "Proc: spContractsGet";

			try
			{
				dsContracts = DBContracts.ContractsGet("2010-09-29", "", "", "");
				rowCount = dsContracts.Tables["Contracts"].Rows.Count;
				txtResult.Text = "Result: " + rowCount.ToString() + " rows.\r\n";
			}
			catch { throw; }

		}

		private void btnContractBillingsGet_Click(object sender, EventArgs e)
		{	//Tested Okay
			int rowCount = 0;
			DataSet dsContractBillingsGet = new DataSet();
			labelProc.Text = "Proc: spContractBillingsGet";

			try
			{
				dsContractBillingsGet = DBContracts.ContractBillingsGet("2010-09-29");
				rowCount = dsContractBillingsGet.Tables["ContractBillings"].Rows.Count;
				txtResult.Text = "Result: " + rowCount.ToString() + " rows.\r\n";
			}
			catch { throw; }
		}

		private void btnContractDetailsGet_Click(object sender, EventArgs e)
		{	//Tested Okay 
			int rowCount = 0;
			DataSet dsContractDetailsGet = new DataSet();
			labelProc.Text = "Proc: spContractDetailsGet";

			try
			{
				dsContractDetailsGet = DBContracts.ContractDetailsGet("2010-09-29", "PFSI");
				rowCount = dsContractDetailsGet.Tables["ContractDetails"].Rows.Count;
				txtResult.Text = "Result: " + rowCount.ToString() + " rows.\r\n";
			}
			catch { throw; }
		}

		private void btnContractResearchGet_Click(object sender, EventArgs e)
		{   //Tested Okay

			int rowCount = 0;
			DataSet dsContractResearchGet = new DataSet();
			labelProc.Text = "Proc: spContractResearchGetGet";

			try
			{
				dsContractResearchGet = DBContracts.ContractResearchGet("2010-09-29", "", "", "", "", "", "", "", "");
				rowCount = dsContractResearchGet.Tables["ContractResearch"].Rows.Count;
				txtResult.Text = "Result: " + rowCount.ToString() + " rows.\r\n";
			}
			catch { throw; }

		}

		private void btnContractBizDateProcessRoll_Click(object sender, EventArgs e)
		{   //Tested Okay 

			int recordCount = 0;
			labelProc.Text = "Proc: spContractBizDateProcessRoll";

			try
			{
				recordCount = DBContracts.ContractBizDateProcessRoll("2010-08-18", "2010-08-17");
				txtResult.Text = "Result: " + recordCount.ToString() + " rows.\r\n";
			}
			catch { throw; }
		}

		private void btnContractBizDateSystemRoll_Click(object sender, EventArgs e)
		{   //Tested Okay 

			int recordCount = 0;
            labelProc.Text = "Proc: spContractBizDateSystemRoll";

			try
			{
				recordCount = DBContracts.ContractBizDateSystemRoll("2010-08-18", "2010-08-17"); 
				txtResult.Text = "Result: " + recordCount.ToString() + " rows.\r\n";
			}
			catch { throw; }
		}

		private void btnContractRateChangeAsOfSet_Click(object sender, EventArgs e)
		{   //Tested Okay

			int recordCount = 0;
			labelProc.Text = "Proc: spRetroRateChangeSet";

			try
			{
                recordCount = DBContracts.ContractRateChangeAsOfSet("2010-10-19", "2010-10-19", "PFSI", "CIT INT", "20101019DEA12345", "-5.0", "-2.9", "DChen");
				txtResult.Text = "Result: " + recordCount.ToString() + " rows.\r\n";
			}
			catch { throw; }
		}

		private void btnContractSet_Click(object sender, EventArgs e)
		{   //Tested Okay 

			labelProc.Text = "Proc: spContractSet";

			try
			{
				DBContracts.ContractSet("2010-10-19", "PFSI", "20101019CON12345", "B", "CIT INT", "DC-SEDOL-001", "500", "500", "4999.88", "4999.88",
					"", "", "", "", "", "", "", "", "", true, true, "", "", "USD", "", "", "", "Test Insert by DCHEN", "", "", "", "USD", "", true, true, true);
				txtResult.Text = "spContractSet completed without error.";
			}
			catch { throw; }

		}

//=========================================================================================================

		private void btnReturnsGet_Click(object sender, EventArgs e)
		{   //Tested Okay

			int rowCount = 0;
			DataSet dsReturnsGet = new DataSet();
			labelProc.Text = "Proc: spReturnsGet";

			try
			{
				dsReturnsGet = DBReturns.ReturnsGet("", "", "", "", 0);
				rowCount = dsReturnsGet.Tables["Returns"].Rows.Count;
				txtResult.Text = "Result: " + rowCount.ToString() + " rows.\r\n";
			}
			catch { throw; }
		}

		private void btnReturnSet_Click(object sender, EventArgs e)
		{   //Tested Okay

			labelProc.Text = "Proc: spReturnSet";
			try
			{
				DBReturns.ReturnSet("20101019RET12345", "2010-10-19", "PFSI", "CIT INT", "20101019CON12345", "B", "500", "", "", "DChen", true);
				txtResult.Text = "spReturnSet completed without error.";
			}
			catch { throw; }
		}

		private void btnReturnAsOfSet_Click(object sender, EventArgs e)
		{   //Tested OKAY, yes.

			int recordCount = 0;
			labelProc.Text = "Proc: spRetroReturnSet";

			try
			{
				recordCount = DBReturns.ReturnAsOfSet("2010-10-19", "2010-10-22", "PFSI", "CIT INT", "20101019CON12345", "B", "20101019RET12346", "880", "DChen");
				txtResult.Text = "spRetroReturnSet Result: " + recordCount.ToString() + " rows.\r\n";
			}
			catch { throw; }
		}

//=========================================================================================================

		private void btnRecallsGet_Click(object sender, EventArgs e)
		{   //Tested Okay 

			int rowCount = 0;
			DataSet dsRecallsGet = new DataSet();
			labelProc.Text = "Proc: spRecallsGet";

			try
			{
				dsRecallsGet = DBRecalls.RecallsGet("2010-09-29", "", "", 0);
				rowCount = dsRecallsGet.Tables["Recalls"].Rows.Count;
				txtResult.Text = "spRecallsGet Result: " + rowCount.ToString() + " rows.\r\n";
			}
			catch { throw; }

		}

		private void btnRecallSet_Click(object sender, EventArgs e)
		{   //Tested Okay

			labelProc.Text = "Proc: spRecallSet";

			try
			{
				DBRecalls.RecallSet("20101019REC12345", "2010-10-19", "PFSI", "20101019CON12345", "B", "CIT INT", "DC-SEDOL-001", "500", "2010-10-19",
					"", "", "XY", "", "DChen Test", "X", "DChen", true); 

				txtResult.Text = "spRecallSet completed without error.";
			}
			catch { throw; }

		}

//=========================================================================================================

		private void btnMarksGet_Click(object sender, EventArgs e)
		{	//Tested Okay 

			int rowCount = 0;
			DataSet dsMarksGet = new DataSet();
			labelProc.Text = "Proc: spMarksGet";

			try
			{
				dsMarksGet = DBMarks.MarksGet("", "", "", "", 0);
				rowCount = dsMarksGet.Tables["Marks"].Rows.Count;
				txtResult.Text = "spMarksGet Result: " + rowCount.ToString() + " rows.\r\n";
			}
			catch { throw; }
		}

		private void btnMarkSet_Click(object sender, EventArgs e)
		{   //Tested Okay

			labelProc.Text = "Proc: spMarkSet";
			try
			{
				DBMarks.MarkSet("20101019MAR12345", "2010-10-19", "PFSI", "CIT INT", "20101019CON12345", "B", "DC-SEDOL-001", 
                                "886.38", "2010-10-18", "", "", "DChen", true);

                txtResult.Text = "spMarkSet completed without error.";
			}
			catch { throw; }
		}

		private void btnRetroMarkSet_Click(object sender, EventArgs e)
		{   //Tested Okay 

			int recordCount = 0;
			labelProc.Text = "Proc: spRetroMarkSet";

			try
			{
				recordCount = DBMarks.RetroMarkSet("2010-10-19", "", "PFSI", "CIT INT", "20101019CON12345", "B", 
                                                   "102.88", "20101019MAR12346", "X", "DChen"); 

				txtResult.Text = "spRetroMarkSet Result: " + recordCount.ToString() + " rows.\r\n";
			}
			catch { throw; }
		}

//=========================================================================================================

        private void btnBoxPositionsGet_Click(object sender, EventArgs e)
        {   //Tested Okay

            int rowCount = 0;
            DataSet dsBoxPositionsGet = new DataSet();
            labelProc.Text = "Proc: spBoxPositionsGet";

            try
            {
                dsBoxPositionsGet = DBPositions.BoxPositionsGet("2010-10-19", "", "");

                rowCount = dsBoxPositionsGet.Tables["BoxPositions"].Rows.Count;
                txtResult.Text = "spBoxPositionsGet Result: " + rowCount.ToString() + " rows.\r\n";
            }
            catch { throw; }

        }

//=========================================================================================================

        private void btnFundsGet_Click(object sender, EventArgs e)
        {   //Tested Okay

            int rowCount = 0;
            DataSet dsFundsGet = new DataSet();
            labelProc.Text = "Proc: spFundsGet";

            try
            {
                dsFundsGet = DBFunds.FundsGet();

                rowCount = dsFundsGet.Tables["Funds"].Rows.Count;
                txtResult.Text = "spFundsGet Result: " + rowCount.ToString() + " rows.\r\n";
            }
            catch { throw; }
        }

        private void btnFundingRatesGet_Click(object sender, EventArgs e)
        {   //Tested Okay

            int rowCount = 0;
            DataSet dsFundingRatesGet = new DataSet();
            labelProc.Text = "Proc: spFundingRatesGet";

            try
            {
                dsFundingRatesGet = DBFunds.FundingRatesGet("", 0);

                rowCount = dsFundingRatesGet.Tables["FundingRates"].Rows.Count;
                txtResult.Text = "spFundingRatesGet Result: " + rowCount.ToString() + " rows.\r\n";
            }
            catch { throw; }
        }

        private void btnFundingRateResearchGet_Click(object sender, EventArgs e)
        {   //Tested Okay

            int rowCount = 0;
            DataSet dsFundingRateResearchGet = new DataSet();
            labelProc.Text = "Proc: spFundingRateResearchGet";

            try
            {
                dsFundingRateResearchGet = DBFunds.FundingRateResearchGet("2010-08-17", "2010-08-18", "FED", 0);

                rowCount = dsFundingRateResearchGet.Tables["FundingRateResearch"].Rows.Count;
                txtResult.Text = "spFundingRateResearchGet Result: " + rowCount.ToString() + " rows.\r\n";
            }
            catch { throw; }        
        }

        private void btnFundingRateSet_Click(object sender, EventArgs e)
        {   //Tested Okay

            labelProc.Text = "Proc: spFundingRateSet";

            try
            {
                DBFunds.FundingRateSet("2010-10-19", "FED", "-0.380", "DChen");
                txtResult.Text = "spFundingRateSet Completed without error";
            }
            catch { throw; }
        
        }

        private void btnFundingRatesRoll_Click(object sender, EventArgs e)
        {   //Tested ? 

            labelProc.Text = "Proc: spFundingRateRoll";

            try
            {
                DBFunds.FundingRatesRoll("2010-08-18", "2010-08-17");

                txtResult.Text = "spFundingRatesRoll Completed without error";
            }
            catch { throw; }
        
        }

//====== TIME ZONE INFO ===================================================================================================

        private void btnTimeZoneInfo_Click(object sender, EventArgs e)
        {//DC tested ? 

            labelProc.Text = "Proc: spTimeZoneSet";

            try
            {
                //Get basic machine and DNS info 
                string localMachineName = System.Environment.MachineName;
                string localHostName = Dns.GetHostName();
                IPAddress[] ips = Dns.GetHostAddresses(localHostName);

                Console.WriteLine("-----------------------------");
                Console.WriteLine("Computer Name = " + localMachineName);
                Console.WriteLine("Local Host Name = " + localHostName);
                foreach (IPAddress ip in ips)
                {
                    Console.WriteLine("Local IP Address = " + ip.ToString());
                }

                // Get full array of TimeZoneInfo object on this computer 
                ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();

                Console.WriteLine("-----------------------------");
                Console.WriteLine("Local system has the following {0} time zones:", timeZones.Count);
                Console.WriteLine("-----------------------------");

                foreach (TimeZoneInfo tZone in timeZones)
                {
                    //Write to console 
                    Console.WriteLine("Zone Id \t= " + tZone.Id);
                    Console.WriteLine("DisplayName = " + tZone.DisplayName);
                    Console.WriteLine("BaseUtcOffset (datetime) = " + tZone.BaseUtcOffset);
                    Console.WriteLine("BaseUtcOffset (Total Hours) = {0}", tZone.BaseUtcOffset.TotalHours.ToString());
                    Console.WriteLine("{0} Zone's Current UtcOffset (datetime) = {1}", tZone.Id, tZone.GetUtcOffset(System.DateTime.UtcNow));
                    Console.WriteLine("{0} Zone's Current UtcOffset (Total Hours) = {1}", tZone.Id, tZone.GetUtcOffset(System.DateTime.UtcNow).TotalHours.ToString()      );
                    Console.WriteLine("SupportsDaylightSavingTime = " + tZone.SupportsDaylightSavingTime);
                    Console.WriteLine("IsDaylightSavingTime = " + (tZone.IsDaylightSavingTime(TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, tZone)) ? "Yes" : "No"));
                    Console.WriteLine("IsDaylightSavingTime = " + (tZone.IsDaylightSavingTime(TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, tZone)) ? "Yes" : "No"));
                    Console.WriteLine("Current [{0}] zone DateTime = {1}", tZone.DisplayName, TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, tZone));
                    Console.WriteLine("Current UTC DateTime = " + System.DateTime.UtcNow);
                    //Console.WriteLine("StandardName = " + zone.StandardName);
                    //Console.WriteLine("DaylightName = " + zone.DaylightName);
                    //Console.WriteLine("ToSerializedString = " + zone.ToSerializedString());
                    Console.WriteLine("-----------------------------");

                    // Insert/Update [tbTimeZoneInfo] data 
                    DBStandardFunctions.TimeZoneSet( tZone.Id, tZone.DisplayName, tZone.BaseUtcOffset.TotalHours.ToString(), 
                                                     tZone.GetUtcOffset(System.DateTime.UtcNow).TotalHours.ToString(), tZone.SupportsDaylightSavingTime, 
                                                     tZone.IsDaylightSavingTime(TimeZoneInfo.ConvertTimeFromUtc(System.DateTime.UtcNow, tZone)), 
                                                     "DChen");
                }

                txtResult.Text = "spTimeZoneSet Completed without error";

            }
            catch
            {   throw;  }

        }

//====== INVENTORY ===================================================================================================

        private void btnInventoryGet_Click(object sender, EventArgs e)
        {   //Tested Okay

            int rowCount = 0;
            DataSet dsInventoryControlGet = new DataSet();
            labelProc.Text = "Proc: spInventoryControlGet";

            try
            {
                dsInventoryControlGet = DBInventory.InventoryGet("", "", "", "", "", "", "");

                rowCount = dsInventoryControlGet.Tables["Inventory"].Rows.Count;
                txtResult.Text = "spInventoryControlGet Result: " + rowCount.ToString() + " rows.\r\n";
            }
            catch { throw; }

        }

        private void btnInventoryItemSet_Click(object sender, EventArgs e)
        {   //Tested Okay

            labelProc.Text = "Proc: spInventoryItemSet";

            try
            {
                //itemSecId	    SecIdTypeIndex	SecIdAlias
                //2383130	2	            MSFT

                DBInventory.InventoryItemSet("2011-03-21", "PFSI", "AB.US.S", "2383130", "500", "-2.0", "WinForm_Client_Test", "DChen_Test");

                txtResult.Text = "spInventoryItemSet Completed without error";
            }
            catch { throw; }

        }

        private void btnInventoryLoad_Click(object sender, EventArgs e)
        {
            int loadCount = 0; 
            labelProc.Text = "Loading Inventory file (with FileLayoutsGet)... ";

            //loadCount = StockLoan.InventoryService.InventoryItem.LoadData("2011-03-21", "PFSI", "AB.US.S", "I", @"C:\LoanStarWorldWide\Archive\0064-5143.20110321.dat");    //DC 
            //loadCount = StockLoan.InventoryService.InventoryItem.LoadData("2011-03-21", "PFSI", "ML.US.S", "I", @"C:\LoanStarWorldWide\Archive\0234-0695.20110322.dat");    //DC
            //loadCount = StockLoan.InventoryService.InventoryItem.LoadData("2011-03-23", "PFSI", "AZTEC.US.S", "R", @"C:\LoanStarWorldWide\Archive\aro0234");    //DC
            loadCount = StockLoan.InventoryService.InventoryItem.LoadData("2011-03-23", "PFSI", "AZTEC.US.S", "R", @"C:\LoanStarWorldWide\Archive\aro0234");    //DC

            txtResult.Text = "Total number of inventory items loaded to database = " + loadCount.ToString() + ".\r\n";
            labelProc.Text = "End Inventory file (TEST)";

        }

        
//====== ADMIN ===================================================================================================

//====== BOOKS ===================================================================================================

//====== Standard Functions ======================================================================================

	}



}
