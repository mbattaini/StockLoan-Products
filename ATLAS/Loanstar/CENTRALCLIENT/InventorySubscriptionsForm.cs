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
	public partial class InventorySubscriptionsForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm;

		private DataSet dsSubscriptions = null;
		private DataSet dsFeedUpload = null;

		public InventorySubscriptionsForm(MainForm mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;
		}

		private void InventorySubscriptionsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			mainForm.inventorySubscriptionForm = null;
		}

		private void DockingTab_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (DockingTab.SelectedIndex)
			{
				case 0:
					SubscriptionsLoad();
					break;

				case 1:
					FeedUploadLoad();
					break;
			}
		}

		private void SubscriptionsLoad()
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{

			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void FeedUploadLoad()
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				dsFeedUpload = mainForm.InventoryAgent.InventoryControlGet(mainForm.ServiceAgent.BizDate());

				FeedUploadGrid.SetDataBinding(dsFeedUpload, "InventoryControl", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void FeedUploadGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case ("BizDate"):
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
					}
					catch { }
					break;

				case ("ItemCount"):
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch { }
					break;
			}
		}

        private void OkRibbonButton_Click(object sender, EventArgs e)
        {

        }

        private void CancelRibbonButton_Click(object sender, EventArgs e)
        {

        }
	}
}