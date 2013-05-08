using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Remoting;
using StockLoan.Common;
using StockLoan.Golden;

namespace Golden
{
	public partial class MainForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		public  IRebate RebateAgent = null;
        public ShortSaleNegativeRebateBillingForm rebateBillingForm = null;
        public ShortSaleNegativeRebateHistoryLookupForm rebateBillingHistoryLookupForm = null;

        public string tempPath = "";

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

                tempPath = Standard.ConfigValue("TempFilePath", @"C:\");

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

				RebateAgent = (IRebate)RemotingTools.ObjectGet(typeof(IRebate));
                if (RebateAgent == null)
				{
					Log.Write("Could not find Rebate agent. [MainForm.MainForm_Load]", 2);					
				}						
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[MainForm.MainForm_Load]", 1);
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
				saveFileDialog.DefaultExt = "xls";
				saveFileDialog.ShowDialog();
				Excel.ExportGridToExcel(ref grid, saveFileDialog.FileName, 0, null);
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
				saveFileDialog.DefaultExt = "xls";
				saveFileDialog.ShowDialog();
				Excel.ExportGridToExcel(ref grid, saveFileDialog.FileName, split, null);
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
				saveFileDialog.DefaultExt = "xls";
				saveFileDialog.ShowDialog();
				Excel.ExportGridToExcel(ref grid, saveFileDialog.FileName, split, excelCellStyleDictionary);
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
				email.Send("", "", grid.Text + " _ " + DateTime.Now.ToString("yyyy-MM-dd"), gridData, null);

			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [MainForm.SendToEmail]", 1);
			}
		}

        public string TempPath
        {
            get
            {
                return tempPath;
            }
        }

        private void ShortSaleBillingButton_Click(object sender, EventArgs e)
        {
            if (rebateBillingForm == null)
            {
                rebateBillingForm = new ShortSaleNegativeRebateBillingForm(this);
                rebateBillingForm.MdiParent = this;
                rebateBillingForm.Show();
            }
            else
            {
                rebateBillingForm.Activate();
            }
        }

        private void LookupButton_Click(object sender, EventArgs e)
        {
            if (rebateBillingHistoryLookupForm == null)
            {
                rebateBillingHistoryLookupForm = new ShortSaleNegativeRebateHistoryLookupForm(this);
                rebateBillingHistoryLookupForm.MdiParent = this;
                rebateBillingHistoryLookupForm.Show();
            }
            else
            {
                rebateBillingHistoryLookupForm.Activate();
            }
        }

        public string UserId
        {
            get
                {
                    return SystemInformation.UserName.ToLower();
                }
        }

        public short UtcOffset
        {
            get
            {
                TimeSpan timeSpan = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
                return (short)(timeSpan.Hours * 60);
            }
        }

    }
}