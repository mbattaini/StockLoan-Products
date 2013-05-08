using System;
using System.Data;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class PositionBankLoanDocuments
	{
		private int		pageWidth = 80;
		private string	header = "";

		private MainForm mainForm;		

		public PositionBankLoanDocuments(MainForm mainForm)
		{
			try
			{
				this.header	  = Standard.ConfigValue("Licensee", "");	
				this.mainForm = mainForm;											
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanDocuments.PositionBankLoanDocuments]", Log.Error, 1); 		
			}
		}

		public string CreateMovementDocument(string bookGroup)
		{
			string gridData = "";
			string bizDateBank = "";
				
			try
			{	
				DataSet dataSet = mainForm.PositionAgent.BankLoanActivityGet(mainForm.UtcOffset);
			
				DataView bankLoanActivityView = new DataView(dataSet.Tables["Activity"], "", "Status, SecId", DataViewRowState.CurrentRows);

				gridData += "LoanDate".PadRight(12, ' ');
				gridData += "BkGp".PadRight(6,  ' ');
				gridData += "Bank".PadRight(6,  ' ');
				gridData += "Security ID".PadRight(14, ' ');
				gridData += "Symbol".PadRight(8,  ' ');
				gridData += "Quantity".PadRight(16,  ' ');
				gridData += "Amount".PadRight(16,  ' ');
				gridData += "Status".PadRight(12,  ' ');
				gridData += "Act Time".PadRight(10,  ' ');
				gridData += "\r\n";

				gridData += "----------".PadRight(12, ' ');
				gridData += "----".PadRight(6,  ' ');
				gridData += "----".PadRight(6,  ' ');
				gridData += "-----------".PadRight(14, ' ');
				gridData += "------".PadRight(8,  ' ');
				gridData += "--------------".PadRight(16,  ' ');
				gridData += "--------------".PadRight(16,  ' ');
				gridData += "-------".PadRight(12,  ' ');
				gridData += "--------".PadRight(10,  ' ');
				gridData += "\r\n";
				
				bizDateBank = mainForm.ServiceAgent.BizDateBank();
				
				foreach(DataRowView row in bankLoanActivityView)
				{						
					if ((row["Status"].ToString().Equals("PM") || row["Status"].ToString().Equals("RM")) && row["BookGroup"].ToString().Equals(bookGroup))
					{						
						gridData += Tools.FormatDate(row["LoanDate"].ToString(), Standard.DateFormat).PadRight(12, ' ');
						gridData += row["BookGroup"].ToString().PadRight(6, ' ').Substring(0, 6);
						gridData += row["Book"].ToString() .PadRight(6, ' ').Substring(0, 6);
						gridData += row["SecId"].ToString().PadRight(14, ' ').Substring(0, 14);
						gridData += row["Symbol"].ToString().PadRight(8, ' ').Substring(0, 8);
						gridData += long.Parse(row["Quantity"].ToString()).ToString("#,##0").PadLeft(14, ' ').Substring(0, 14) + "  ";
						gridData += decimal.Parse(row["Amount"].ToString()).ToString("#,##0.00").PadLeft(14, ' ').Substring(0, 14) + "  ";					
						
						if (row["Status"].ToString().Equals("PM"))
						{
							gridData += "Pledged".PadRight(12, ' ');
						}
						else if (row["Status"].ToString().Equals("RM"))
						{
							gridData += "Released".PadRight(12, ' ');
						}

						gridData += Tools.FormatDate(row["ActTime"].ToString(), Standard.TimeShortFormat).PadRight(10, ' ');
						gridData += "\r\n";
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanDocuments.CreateMovementDocument]", 3);
				throw;
			}

			return gridData;
		}
		
		public string CreateFailedPledgesDocument(string bookGroup)
		{
			string gridData = "";
			string bizDateBank = "";
				
			try
			{	
				DataSet dataSet = mainForm.PositionAgent.BankLoanActivityGet(mainForm.UtcOffset);
			
				DataView bankLoanActivityView = new DataView(dataSet.Tables["Activity"], "", "Status, SecId", DataViewRowState.CurrentRows);

				gridData += "LoanDate".PadRight(12, ' ');
				gridData += "BkGp".PadRight(6,  ' ');
				gridData += "Bank".PadRight(6,  ' ');
				gridData += "Security ID".PadRight(14, ' ');
				gridData += "Symbol".PadRight(8,  ' ');
				gridData += "Quantity".PadRight(16,  ' ');
				gridData += "Amount".PadRight(16,  ' ');
				gridData += "Status".PadRight(16,  ' ');
				gridData += "Act Time".PadRight(10,  ' ');
				gridData += "\r\n";

				gridData += "----------".PadRight(12, ' ');
				gridData += "----".PadRight(6,  ' ');
				gridData += "----".PadRight(6,  ' ');
				gridData += "-----------".PadRight(14, ' ');
				gridData += "------".PadRight(8,  ' ');
				gridData += "--------------".PadRight(16,  ' ');
				gridData += "--------------".PadRight(16,  ' ');
				gridData += "-------".PadRight(16,  ' ');
				gridData += "--------".PadRight(10,  ' ');
				gridData += "\r\n";

				
				bizDateBank = mainForm.ServiceAgent.BizDateBank();
				
				foreach(DataRowView row in bankLoanActivityView)
				{						
					if ( ((row["Status"].ToString().Equals("PF")) 
						|| (row["Status"].ToString().Equals("RF")) 
						|| (row["Status"].ToString().Equals("RD")))
						&& row["BookGroup"].ToString().Equals(bookGroup))
					{						
						gridData += Tools.FormatDate(row["LoanDate"].ToString(), Standard.DateFormat).PadRight(12, ' ');
						gridData += row["BookGroup"].ToString().PadRight(6, ' ').Substring(0, 6);
						gridData += row["Book"].ToString() .PadRight(6, ' ').Substring(0, 6);
						gridData += row["SecId"].ToString().PadRight(14, ' ').Substring(0, 14);
						gridData += row["Symbol"].ToString().PadRight(8, ' ').Substring(0, 8);
						gridData += long.Parse(row["Quantity"].ToString()).ToString("#,##0").PadLeft(14, ' ').Substring(0, 14) + "  ";
						gridData += decimal.Parse(row["Amount"].ToString()).ToString("#,##0.00").PadLeft(14, ' ').Substring(0, 14) + "  ";					
						
						if (row["Status"].ToString().Equals("PF"))
						{
							gridData += "Pledge Failed".PadRight(16, ' ');
						}
						else if (row["Status"].ToString().Equals("RF"))
						{
							gridData += "Release Failed".PadRight(16, ' ');
						}
						else if (row["Status"].ToString().Equals("RD"))
						{
							gridData += "Release Dropped".PadRight(16, ' ');
						}

						gridData += Tools.FormatDate(row["ActTime"].ToString(), Standard.TimeShortFormat).PadRight(10, ' ');
						gridData += "\r\n";
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanDocuments.CreateMovementDocument]", 3);
				throw;
			}

			return gridData;
		}

		public string CreateLoanAdjustmentDocument(string bookGroup, string book, string name, string contact, string faxNumber)
		{
			string document = "";
			string type = "";
			string direction = "";
			string directionAmount = "";
			
			try
			{
				DataSet dataSet = mainForm.PositionAgent.BankLoansGet(mainForm.UtcOffset);
			
				document += header.PadLeft(header.Length + ((pageWidth - header.Length)/2), ' ') + "\r\n\r\n\r\n\r\n";			
				document += "Date: " + mainForm.ServiceAgent.BizDate()+ "\r\n\r\n\r\n";
				document += name + "\r\n\r\n";
				document += "Via Fax: " + faxNumber + "\r\n\r\n\r\n";
				document += "Attn: " + contact + "\r\n\r\n\r\n";			
			
				foreach(DataRow dataRow in dataSet.Tables["Loans"].Rows)
				{
					if (dataRow["Book"].ToString().Equals(book) && dataRow["BookGroup"].ToString().Equals(bookGroup))
					{					
						switch (dataRow["LoanType"].ToString())
						{
							case "F":
								type = "FIRM";
								break;

							case "C":
								type = "CUSTOMER";
								break;

							case "S":
								type = "SPECIAL";
								break;

							case "U":
								type = "UNSECURED";
								break;

							default:
								type = "UNKNOWN";
								break;
						}
					
						if(long.Parse(dataRow["PreviousLoanAmount"].ToString()) > long.Parse(dataRow["LoanAmount"].ToString()))
						{
							direction = "Decrease";					
							directionAmount = (long.Parse(dataRow["PreviousLoanAmount"].ToString()) - long.Parse(dataRow["LoanAmount"].ToString())).ToString("#,##0.00");
						}
						else if(long.Parse(dataRow["PreviousLoanAmount"].ToString()) < long.Parse(dataRow["LoanAmount"].ToString()))
						{
							direction = "Increase";
							directionAmount = (long.Parse(dataRow["LoanAmount"].ToString()) - long.Parse(dataRow["PreviousLoanAmount"].ToString())).ToString("#,##0.00");
						}
						else if(long.Parse(dataRow["PreviousLoanAmount"].ToString()) == long.Parse(dataRow["LoanAmount"].ToString()))
						{
							direction = "";
							directionAmount = "0";
						}

			
						if (!direction.Equals(""))
						{
							document += "Please " + direction + "\r\n";
						}

						document += (type + " LOAN (" + Tools.FormatDate(dataRow["LoanDate"].ToString(), Standard.DateFormat) + "):").PadRight(28, ' ');
						document += directionAmount.PadLeft(20, ' ').Substring(0, 20) + "  Balance:" + long.Parse(dataRow["LoanAmount"].ToString()).ToString("#,##0.00").PadLeft(20,  ' ').Substring(0, 20) + "\r\n\r\n";									
					}
				}
			
				document += "\r\n\r\n\r\n\r\n";

				document += "Wire Instructions:";
				document += "\r\n\r\n\r\n\r\n\r\n\r\n";

				document += "Thank you,\r\n\r\n\r\n\r\n";;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanDocuments.CreateMovementDocument]", 3);
				throw;
			}


			return document;
		}
	}
}
