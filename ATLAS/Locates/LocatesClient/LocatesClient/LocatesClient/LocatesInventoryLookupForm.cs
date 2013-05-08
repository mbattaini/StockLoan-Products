using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;

namespace LocatesClient
{
	public partial class LocatesInventoryLookupForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm = null;
		private DataSet dsInventory = null;

		public LocatesInventoryLookupForm(MainForm mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;			
		}

		private void InventoryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "ScribeTime":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString("HH:mm");
					}
					catch { }
					break;
				case "BizDate":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
					}
					catch { }
					break;
				case "Quantity":
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch { }
					break;
			}
		}

		private void SearchButton_Click(object sender, EventArgs e)
		{
			
		}

		private void InventoryGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{
			try
			{
				if (!InventoryGrid.Columns["BizDate"].CellText(e.Row).Equals(mainForm.ServiceAgent.BizDate()))
				{
					e.CellStyle.ForeColor = System.Drawing.Color.LightGray;
				}
			}
			catch { }
		}

		private void LocatesInventoryLookupForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			mainForm.locatesInventoryLookupForm = null;
		}

		private void SecurityIdTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!SecurityIdTextBox.Text.Equals("") && e.KeyChar.Equals((char) 13))
			{
				this.Cursor = Cursors.WaitCursor;

				try
				{
					dsInventory = mainForm.ShortSaleAgent.InventoryGet(SecurityIdTextBox.Text, mainForm.UtcOffset);
					InventoryGrid.SetDataBinding(dsInventory, "Inventory", true);

					mainForm.SecId = SecurityIdTextBox.Text;

					SecurityIdTextBox.Text = mainForm.SecId;
				}
				catch (Exception error)
				{
					mainForm.Alert(this.Name, error.Message);
				}

				this.Cursor = Cursors.Default;
			}
		}

		private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				SaveDialog.AddExtension = true;				
				SaveDialog.ShowDialog();
				Excel.ExportGridToExcel(ref InventoryGrid, SaveDialog.FileName, 0, null);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}
	}
}