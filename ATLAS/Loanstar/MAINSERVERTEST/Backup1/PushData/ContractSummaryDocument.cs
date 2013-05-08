using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace StockLoan.PushData
{
	public class ContractSummaryDocument
	{
		private string dbCnStr = "";
		private string emailRecipients = "";

		private string bookGroup = "";
		private string bizDate = "";

		
		private const int QUANTITY_WIDTH = 13;
		private const int AMOUNT_WIDTH = 13;
		private const int RATE_WIDTH = 6;

		DataSet dsContractSecId;
		DataSet dsContracts;
		DataSet dsContractBook;

		public ContractSummaryDocument(string dbCnStr, string emailRecipients, string bizDate, string bookGroup)
		{
			this.dbCnStr = dbCnStr;

			this.bizDate = bizDate;
			this.bookGroup = bookGroup;
		}

		public void DataGet()
		{
			dsContractSecId = new DataSet();
			dsContracts = new DataSet();
			dsContractBook = new DataSet();

			SqlConnection dbCn = new SqlConnection(dbCnStr);

			SqlCommand dbCmd = new SqlCommand("spReportContractSecIdGet", dbCn);
			dbCmd.CommandType = CommandType.StoredProcedure;

			SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
			paramBizDate.Value = bizDate;

			SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
			dataAdapter.Fill(dsContractSecId, "SecId");

			dbCmd.CommandText = "spReportContractBookGet";
			dataAdapter.Fill(dsContractBook, "Book");
			
			dbCmd.CommandText = "spContractGet";

			SqlParameter paramBooKGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
			paramBooKGroup.Value = bookGroup;

			dataAdapter.SelectCommand = dbCmd;
			dataAdapter.Fill(dsContracts, "Contracts");
		
		}

		public string ContentGet_ContractBySecurity()
		{
			int maxLength;

			string fileName = "";
			string spacer = "   ";
			string columns = "Contra Party".PadRight(20, ' ') + spacer +
									"Contract#".PadRight(16, ' ') + spacer +
									"Trd-Dt \\ Stl-Dt".PadRight(15, ' ') + spacer +
									"Rate".PadRight(RATE_WIDTH, ' ') + spacer +
									"Borr Quantity".PadRight(QUANTITY_WIDTH, ' ') + spacer +
									"Loan Quantity".PadRight(QUANTITY_WIDTH, ' ') + spacer +
									"Borr Amount".PadRight(AMOUNT_WIDTH, ' ') + spacer +
									"Loan Amount".PadRight(AMOUNT_WIDTH, ' ') + spacer +
									"".PadRight(1, ' ') + " " +
									"".PadRight(4, ' ') + spacer +
									"MKT Val";

			string body = "";
			string secIdHeader = "";
			string secIdFooter = "";
			string contractBody = "";

			string borrowQuantity = "";
			string loanQuantity = "";

			string borrowAmount = "";
			string loanAmount = "";

			long borrQuantityQ = 0;
			long loanQuantityQ = 0;

			decimal borrAmountA = 0;
			decimal loanAmountA = 0;

			maxLength = columns.Length;

			foreach (DataRow secIdRow in dsContractSecId.Tables["SecId"].Rows)
			{
				secIdHeader = " *** " + secIdRow["SecId"].ToString().PadRight(15, ' ') + " - " + secIdRow["Description"].ToString() + " CC: " + secIdRow["CurrencyIso"].ToString();
				secIdHeader = secIdHeader.PadRight(maxLength, ' ') + "#";

				contractBody = "";

				borrQuantityQ = 0;
				loanQuantityQ = 0;

				borrAmountA = 0;
				loanAmountA = 0;
				
				foreach (DataRow contractRow in dsContracts.Tables["Contracts"].Rows)
				{
					if (contractRow["SecId"].ToString().Trim().Equals(secIdRow["SecId"].ToString().Trim()) && contractRow["CurrencyIso"].ToString().Equals(secIdRow["CurrencyIso"].ToString()))
					{
						contractBody += contractRow["BookName"].ToString().PadRight(20, ' ').Substring(0, 20) + spacer +
											   contractRow["ContractId"].ToString().PadRight(16, ' ') + spacer +
											   (((contractRow["ValueDate"].ToString().Equals("")) ? contractRow["ValueDate"].ToString() : DateTime.Parse(contractRow["ValueDate"].ToString()).ToString("ddMMyy")) + "\\" + ((contractRow["SettleDate"].ToString().Equals("")) ? contractRow["SettleDate"].ToString() : DateTime.Parse(contractRow["SettleDate"].ToString()).ToString("ddMMyy"))).PadRight(15, ' ') + spacer +
											   decimal.Parse(contractRow["RebateRate"].ToString()).ToString("00.000").PadRight(RATE_WIDTH, ' ') + spacer;

						if (contractRow["ContractType"].ToString().Equals("B"))
						{
							borrowQuantity = long.Parse(contractRow["Quantity"].ToString()).ToString("#,##0").PadLeft(QUANTITY_WIDTH, ' ');
							loanQuantity = "0".PadLeft(QUANTITY_WIDTH, ' ');

							borrowAmount = decimal.Parse(contractRow["Amount"].ToString()).ToString("#,##0.00").PadLeft(AMOUNT_WIDTH, ' ');
							loanAmount = "0.00".PadLeft(AMOUNT_WIDTH, ' ');

							borrQuantityQ += long.Parse(contractRow["Quantity"].ToString());
							borrAmountA += decimal.Parse(contractRow["Amount"].ToString());
						}
						else
						{
							loanQuantity = long.Parse(contractRow["Quantity"].ToString()).ToString("#,##0").PadLeft(QUANTITY_WIDTH, ' ');
							borrowQuantity = "0".PadLeft(QUANTITY_WIDTH, ' ');

							loanAmount = decimal.Parse(contractRow["Amount"].ToString()).ToString("#,##0.00").PadLeft(AMOUNT_WIDTH, ' ');
							borrowAmount = "0.00".PadLeft(AMOUNT_WIDTH, ' ');

							loanQuantityQ += long.Parse(contractRow["Quantity"].ToString());
							loanAmountA += decimal.Parse(contractRow["Amount"].ToString());
						}

						contractBody += borrowQuantity + spacer + loanQuantity + spacer + borrowAmount + spacer + loanAmount + spacer;

						if (contractRow["SettleDate"].ToString().Equals(""))
						{
							contractBody += "P" + " ";
						}
						else
						{
							contractBody += " " + " ";
						}

						contractBody += (decimal.Parse(contractRow["Margin"].ToString()) < 100) ? (decimal.Parse(contractRow["Margin"].ToString()) * 100).ToString("000") : decimal.Parse(contractRow["Margin"].ToString()).ToString("000");
						contractBody += contractRow["MarginCode"].ToString().PadRight(6, ' ') + spacer;
						contractBody += "0.00".PadLeft(4, ' ') + spacer;						
						contractBody = contractBody.PadRight(maxLength, ' ');
						contractBody += "#";
						contractBody += contractRow["Book"].ToString() + "#";
					}

					secIdFooter  = "".PadRight(20, ' ') + spacer +
									"".PadRight(16, ' ') + spacer +
									"".PadRight(15, ' ') + spacer +
									"".PadRight(RATE_WIDTH, ' ') + spacer +
									"".PadLeft(QUANTITY_WIDTH, '_') + spacer +
									"".PadLeft(QUANTITY_WIDTH, '_') + spacer +
									"".PadLeft(AMOUNT_WIDTH, '_') + spacer +
									"".PadLeft(AMOUNT_WIDTH, '_') + spacer +
									"".PadRight(1, ' ') + " " +
									"".PadRight(4, ' ') + spacer + "#";		


					secIdFooter += "".PadRight(20, ' ') + spacer +
									"".PadRight(16, ' ') + spacer +
									"".PadRight(15, ' ') + spacer +
									"".PadRight(RATE_WIDTH, ' ') + spacer +
									borrQuantityQ.ToString("#,##0").PadLeft(QUANTITY_WIDTH, ' ') + spacer +
									loanQuantityQ.ToString("#,##0").PadLeft(QUANTITY_WIDTH, ' ') + spacer +
									borrAmountA.ToString("#,##0.00").PadLeft(AMOUNT_WIDTH, ' ') + spacer +
									loanAmountA.ToString("#,##0.00").PadLeft(AMOUNT_WIDTH, ' ') + spacer +
									"".PadRight(1, ' ') + " " +
									"".PadRight(4, ' ') + spacer + "#";				
				}

				body += secIdHeader + contractBody + secIdFooter;
				body += " # #";
			}			
			
			StockLoan.PushData.PdfDoucment pdfDocument = new StockLoan.PushData.PdfDoucment();


			fileName = @"contract_summary_security_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
			pdfDocument.Create(@"C:\Loanstar\Reports\" + fileName, "Contract Summary By Security", "", columns, body);
		
			return @"\\sendero1\reports\" + fileName;
		}

		public string ContentGet_ContractByCounterParty()
		{
			int maxLength;

			string fileName = "";
			string spacer = "   ";
			string columns = "Security ID".PadRight(20, ' ') + spacer +
									"Contract#".PadRight(16, ' ') + spacer +
									"Trd-Dt \\ Stl-Dt".PadRight(15, ' ') + spacer +
									"Rate".PadRight(RATE_WIDTH, ' ') + spacer +
									"Borr Quantity".PadRight(QUANTITY_WIDTH, ' ') + spacer +
									"Loan Quantity".PadRight(QUANTITY_WIDTH, ' ') + spacer +
									"Borr Amount".PadRight(AMOUNT_WIDTH, ' ') + spacer +
									"Loan Amount".PadRight(AMOUNT_WIDTH, ' ') + spacer +
									"".PadRight(1, ' ') + " " +
									"".PadRight(4, ' ') + spacer +
									"MKT Val".PadRight(AMOUNT_WIDTH, ' ');

			string body = "";
			string bookHeader = "";
			string bookFooter = "";
			string contractBody = "";

			string borrowQuantity = "";
			string loanQuantity = "";

			string borrowAmount = "";
			string loanAmount = "";

			long borrQuantityQ = 0;
			long loanQuantityQ = 0;

			decimal borrAmountA = 0;
			decimal loanAmountA = 0;


			maxLength = columns.Length;

			foreach (DataRow bookRow in dsContractBook.Tables["Book"].Rows)
			{
				bookHeader = " *** " + bookRow["BookName"].ToString().PadRight(15, ' ') + " - " + bookRow["Book"].ToString() + " CC: " + bookRow["CurrencyIso"].ToString();
				bookHeader = bookHeader.PadRight(maxLength, ' ') + "#";

				contractBody = "";

				borrQuantityQ = 0;
				loanQuantityQ = 0;

				borrAmountA = 0;
				loanAmountA = 0;

				foreach (DataRow contractRow in dsContracts.Tables["Contracts"].Rows)
				{
					if (bookRow["BookGroup"].ToString().Trim().Equals(contractRow["BookGroup"].ToString().Trim()) &&
						bookRow["Book"].ToString().Trim().Equals(contractRow["Book"].ToString().Trim()) &&
						 bookRow["CurrencyIso"].ToString().Equals(contractRow["CurrencyIso"].ToString()))
					{
						contractBody += contractRow["SecId"].ToString().PadRight(20, ' ').Substring(0, 20) + spacer +
											   contractRow["ContractId"].ToString().PadRight(16, ' ') + spacer +
											   (((contractRow["ValueDate"].ToString().Equals("")) ? contractRow["ValueDate"].ToString() : DateTime.Parse(contractRow["ValueDate"].ToString()).ToString("ddMMyy")) + "\\" + ((contractRow["SettleDate"].ToString().Equals("")) ? contractRow["SettleDate"].ToString() : DateTime.Parse(contractRow["SettleDate"].ToString()).ToString("ddMMyy"))).PadRight(15, ' ') + spacer +
											   decimal.Parse(contractRow["RebateRate"].ToString()).ToString("00.000").PadRight(RATE_WIDTH, ' ') + spacer;

						if (contractRow["ContractType"].ToString().Equals("B"))
						{
							borrowQuantity = long.Parse(contractRow["Quantity"].ToString()).ToString("#,##0").PadLeft(QUANTITY_WIDTH, ' ');
							loanQuantity = "0".PadLeft(QUANTITY_WIDTH, ' ');

							borrowAmount = decimal.Parse(contractRow["Amount"].ToString()).ToString("#,##0.00").PadLeft(AMOUNT_WIDTH, ' ');
							loanAmount = "0.00".PadLeft(AMOUNT_WIDTH, ' ');

							borrQuantityQ += long.Parse(contractRow["Quantity"].ToString());
							borrAmountA += decimal.Parse(contractRow["Amount"].ToString());
						}
						else
						{
							loanQuantity = long.Parse(contractRow["Quantity"].ToString()).ToString("#,##0").PadLeft(QUANTITY_WIDTH, ' ');
							borrowQuantity = "0".PadLeft(QUANTITY_WIDTH, ' ');

							loanAmount = decimal.Parse(contractRow["Amount"].ToString()).ToString("#,##0.00").PadLeft(AMOUNT_WIDTH, ' ');
							borrowAmount = "0.00".PadLeft(AMOUNT_WIDTH, ' ');

							loanQuantityQ += long.Parse(contractRow["Quantity"].ToString());
							loanAmountA += decimal.Parse(contractRow["Amount"].ToString());
						}

						contractBody += borrowQuantity + spacer + loanQuantity + spacer + borrowAmount + spacer + loanAmount + spacer;

						if (contractRow["SettleDate"].ToString().Equals(""))
						{
							contractBody += "P" + " ";
						}
						else
						{
							contractBody += " " + " ";
						}

						contractBody += (decimal.Parse(contractRow["Margin"].ToString()) < 100) ? (decimal.Parse(contractRow["Margin"].ToString()) * 100).ToString("000") : decimal.Parse(contractRow["Margin"].ToString()).ToString("000");
						contractBody += contractRow["MarginCode"].ToString().PadRight(6, ' ') + spacer;
						contractBody += "0.00".PadLeft(AMOUNT_WIDTH, ' ') + spacer;
						contractBody = contractBody.PadRight(maxLength, ' ');
						contractBody += "#";						
					}
				}


				bookFooter = "".PadRight(20, ' ') + spacer +
								"".PadRight(16, ' ') + spacer +
								"".PadRight(15, ' ') + spacer +
								"".PadRight(RATE_WIDTH, ' ') + spacer +
								"".PadLeft(QUANTITY_WIDTH, '_') + spacer +
								"".PadLeft(QUANTITY_WIDTH, '_') + spacer +
								"".PadLeft(AMOUNT_WIDTH, '_') + spacer +
								"".PadLeft(AMOUNT_WIDTH, '_') + spacer +
								"".PadRight(1, ' ') + " " +
								"".PadRight(4, ' ') + spacer + "#";	

				bookFooter += "".PadRight(20, ' ') + spacer +
								"".PadRight(16, ' ') + spacer +
								"".PadRight(15, ' ') + spacer +
								"".PadRight(RATE_WIDTH, ' ') + spacer +
								borrQuantityQ.ToString("#,##0").PadLeft(QUANTITY_WIDTH, ' ') + spacer +
								loanQuantityQ.ToString("#,##0").PadLeft(QUANTITY_WIDTH, ' ') + spacer +
								borrAmountA.ToString("#,##0.00").PadLeft(AMOUNT_WIDTH, ' ') + spacer +
								loanAmountA.ToString("#,##0.00").PadLeft(AMOUNT_WIDTH, ' ') + spacer +
								"".PadRight(1, ' ') + " " +
								"".PadRight(AMOUNT_WIDTH, ' ') + spacer + "#";				

				body += bookHeader + contractBody + bookFooter;
				body += " # #";
			}						

			StockLoan.PushData.PdfDoucment pdfDocument = new StockLoan.PushData.PdfDoucment();

			fileName = @"contract_summary_counterparty_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
			
			pdfDocument.Create(@"C:\Loanstar\Reports\" + fileName, "Contract Summary By Counter Party", "", columns, body);

			return @"\\sendero1\reports\" + fileName;
		}
	}
}
