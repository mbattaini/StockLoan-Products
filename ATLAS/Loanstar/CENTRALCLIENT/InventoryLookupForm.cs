using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;
using StockLoan.Main;

namespace CentralClient
{
	public partial class InventoryLookupForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm = null;
		private DataSet dsInventory = null;

		public InventoryLookupForm(MainForm mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;
		}

		private void SecIdLookupTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			dsInventory = null;

			if (e.KeyChar.Equals((char)13))
			{
				if (DockingTab.SelectedIndex == 0)
				{
					mainForm.SecId = SecIdLookupTextBox.Text;
	
					dsInventory = mainForm.InventoryAgent.InventoryGet("", SecIdLookupTextBox.Text, mainForm.UtcOffset);
					SecIdLookupGrid.SetDataBinding(dsInventory, "Inventory", true);

					SecIdLookupTextBox.Text = mainForm.SecId;
				}
				else if (DockingTab.SelectedIndex == 1)
				{
					dsInventory = mainForm.InventoryAgent.InventoryGet(SecIdLookupTextBox.Text, "", mainForm.UtcOffset);
					DeskLookupGrid.SetDataBinding(dsInventory, "Inventory", true);
				}
			}
		}

		private void InventoryLookupForm_Load(object sender, EventArgs e)
		{

		}

		private void InventoryLookupForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			mainForm.inventoryLookupForm = null;
		}

		private void Grid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case ("Quantity"):
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch { }
					break;

				case ("BizDate"):
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
					}
					catch { }
					break;

				case ("ScribeTime"):
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeShortFormat);
					}
					catch { }
					break;
			}
		}
	}
}