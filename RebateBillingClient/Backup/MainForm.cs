using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Remoting;
using StockLoan.Common;
using StockLoan.NorthStar;

namespace NorthStarClient
{
	public partial class MainForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		public IShortInterest ShortInterestAgent = null;
		public ITrade TradeAgent = null;

		public ShortInterestReportingForm shortInterestReportForm = null;
		public MarginCollateralReportForm marginCollateralReportForm = null;		//DC new 
		public CashAccountReportForm cashAccountReportForm = null;					//DC new


		public MainForm()
		{
			InitializeComponent();

			try
			{                
				Log.Name = Standard.ConfigValue("ApplicationName");
				Log.Level = Standard.ConfigValue("LogLevel");
				Log.FilePath = Standard.ConfigValue("LogFilePath");
				Log.Write("", 1);
				Log.Write("Running version " + Application.ProductVersion + " [MainForm.MainForm]", 1);

				RegistryValue.Name = Standard.ConfigValue("ApplicationName");
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [MainForm.MainForm]",  1);
			}
		}
        
		private void MainForm_Load(object sender, EventArgs e)
		{
			try
			{
				this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "0"));
				this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "0"));
				this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "747"));
				this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "1542"));

				ShortInterestAgent = (IShortInterest)RemotingTools.ObjectGet(typeof(IShortInterest));
				if (ShortInterestAgent == null)
				{
					Log.Write("Could not find ShortInterest agent. [MainForm.MainForm_Load]", 2);

					ShortInterestReportingCommand.Enabled = false;
				}

				TradeAgent = (ITrade)RemotingTools.ObjectGet(typeof(ITrade));
				if (TradeAgent == null)
				{
					Log.Write("Could not find Trade agent. [MainForm.MainForm_Load]", 2);
				}

				SecMaster.MainForm = this;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[MainForm.MainForm_Load]", 1);
			}
		}

		private void ShortInterestReportingCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{
				if (shortInterestReportForm == null)
				{
					shortInterestReportForm = new ShortInterestReportingForm(this);
					shortInterestReportForm.MdiParent = this;
					shortInterestReportForm.Show();
				}
				else
				{
					shortInterestReportForm.Activate();
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[MainForm.ShortInterestReportingCommand_Click]", 1);
			}
		}

		public string SecId
		{
			set
			{
				SecMaster.SecId = value;
			}
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());
			}
		}

		private void MarginCollateralReportingCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{
				if (marginCollateralReportForm == null)
				{
					marginCollateralReportForm = new MarginCollateralReportForm(this);
					marginCollateralReportForm.MdiParent = this;
					marginCollateralReportForm.Show();
				}
				else
				{
					marginCollateralReportForm.Activate();
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[MainForm.MarginCollateralReportingCommand_Click]", 1);
			}
			 
		}

		private void CashAccountReportingCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{
				if (cashAccountReportForm == null)
				{
					cashAccountReportForm = new CashAccountReportForm(this);
					cashAccountReportForm.MdiParent = this;
					cashAccountReportForm.Show();
				}
				else
				{
					cashAccountReportForm.Activate();
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[MainForm.CashAccountReportingCommand_Click]", 1);
			}

		}

		//------ Send To Clipboard, Excel, Email code are taken from LoanStar's CentralClient App.

		public string GridToText(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid)
		{
			this.Cursor = Cursors.WaitCursor;
			string gridData = "";

			try
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
				{
					gridData += dataColumn.Caption + "\t";
				}

				gridData += "\r\n";

				foreach (int row in grid.SelectedRows)
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
					{
						gridData += dataColumn.CellText(row) + "\t";
					}

					gridData += "\r\n";
				}

				return gridData;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [MainForm.GridToText]", 1);
			}

			this.Cursor = Cursors.Default;
			return "Sorry... Error loading the list.";
		}

		public void SendToClipboard(string data)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				if ((data == "") || (data == null))
				{
					MessageBox.Show("You have not selected any data.", "Alert");
					return;
				}

				Clipboard.SetDataObject(data, true);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [MainForm.SendToClipboard]", 1);
			}
			this.Cursor = Cursors.Default;
		}

		public void SendToClipboard(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid)
		{
			this.Cursor = Cursors.WaitCursor;
			string data = "";

			try
			{
				if (grid.Focused)
				{
					data = GridToText(ref grid);
				}

				Clipboard.SetDataObject(data, true);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [MainForm.SendToClipboard]", 1);
			}
			this.Cursor = Cursors.Default;
		}
		
		public void SendToExcel(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				SaveFileDialog.DefaultExt = "xls";
				SaveFileDialog.ShowDialog();
				Excel.ExportGridToExcel(ref grid, SaveFileDialog.FileName, 0, null);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [MainForm.SendToExcel]", 1);
			}
			this.Cursor = Cursors.Default;
		}

		public void SendToExcel(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid, int split)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				SaveFileDialog.DefaultExt = "xls";
				SaveFileDialog.ShowDialog();
				Excel.ExportGridToExcel(ref grid, SaveFileDialog.FileName, split, null);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [MainForm.SendToExcel]", 1);
			}
			this.Cursor = Cursors.Default;
		}

		public void SendToExcel(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid, int split, Dictionary<string, ExcelCellStyle> excelCellStyleDictionary)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				SaveFileDialog.DefaultExt = "xls";
				SaveFileDialog.ShowDialog();
				Excel.ExportGridToExcel(ref grid, SaveFileDialog.FileName, split, excelCellStyleDictionary);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [MainForm.SendToExcel]", 1);
			}
			this.Cursor = Cursors.Default;
		}
		
		public void SendToEmail(string body)
		{
			SendToEmail("", "", "", body, "");
		}

		public void SendToEmail(string recipientList, string ccList, string subject, string body, string attachmentList)
		{
			try
			{
				Email email = new Email();
				email.Send(recipientList, ccList, subject, body, attachmentList);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [MainForm.SendToEmail]", 1);
			}

		}

		public void SendToEmail(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid)
		{
			int textLength;
			int[] maxTextLength;
			int columnIndex = -1;
			string gridData = "\n\n";

			if (grid.SelectedCols.Count.Equals(0))
			{
				MessageBox.Show("You have not selected any rows to send.", "Alert");
				return;
			}

			try
			{
				maxTextLength = new int[grid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in grid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				// Read grid caption 
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";

				columnIndex = -1;

				// Create field dash lines with same width as max field length
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";

				// Read grid selected data 
				foreach (int rowIndex in grid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
					{
						if (dataColumn.Value.GetType().Equals(typeof(System.String)))
						{
							//gridData += dataColumn.CellText(rowIndex).PadRight(maxTextLength[++columnIndex] + 2);			//original
							gridData += dataColumn.CellText(rowIndex).Trim().PadRight(maxTextLength[++columnIndex] + 2);	//new
						}
						else
						{
							gridData += dataColumn.CellText(rowIndex).PadLeft(maxTextLength[++columnIndex]) + "  ";
						}
					}

					gridData += "\n";
				}

				//Load the space formatted grid data into Outlook email 
				Email email = new Email();

				//DC email.Send("", "", grid.Text + " _ " + DateTime.Parse(ServiceAgent.BizDate()).ToString("yyyy-MM-dd"), gridData, null);
				email.Send("", "", grid.Text + " _ " + DateTime.Now.ToString("yyyy-MM-dd"), gridData, null);

			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [MainForm.SendToEmail]", 1);
			}
		}




	}
}