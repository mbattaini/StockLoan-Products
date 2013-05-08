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
	public partial class LocatesTradingGroupsForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private DataSet dsTradingGroups = null;
		private MainForm mainForm = null;
		private DataView dvTradingGroupsParameters = null;
		private string parametersRowFilter = "";
		private string groupCode = "";

		public LocatesTradingGroupsForm(MainForm mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;
		}

		private void LocatesTradingGroupsForm_Load(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				DefaultPriceMinimumTextBox.Text = mainForm.ServiceAgent.KeyValueGet("ShortSaleLocatePriceThreshold", "2.00");
				DefaultAutoApprovalTextBox.Text = mainForm.ServiceAgent.KeyValueGet("ShortSaleLocateAutoApprovalMax", "50000");

				PremiumMinTextBox.Text = mainForm.ServiceAgent.KeyValueGet("ShortSaleLocatePremiumMin", "100");
				PremiumMaxTextBox.Text = mainForm.ServiceAgent.KeyValueGet("ShortSaleLocatePremiumMax", "2500");

				dsTradingGroups = mainForm.ShortSaleAgent.TradingGroupsGet(DateTimePicker.Text, mainForm.UtcOffset);

				parametersRowFilter = "GroupCode = ''";
				dvTradingGroupsParameters = new DataView(dsTradingGroups.Tables["TradingGroups"], parametersRowFilter, "", DataViewRowState.CurrentRows);

				TradingGroupsGrid.SetDataBinding(dsTradingGroups, "TradingGroups", true);
				TradingGroupsParametersGrid.SetDataBinding(dvTradingGroupsParameters, "", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void LocatesTradingGroupsForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.mainForm.locatesTradingGroupsForm = null;
		}

		private void TradingGroupsGrid_Paint(object sender, PaintEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				if (!groupCode.Equals(TradingGroupsGrid.Columns["GroupCode"].Text))
				{
					groupCode = TradingGroupsGrid.Columns["GroupCode"].Text;
					parametersRowFilter = "GroupCode = '" + groupCode + "'";
					dvTradingGroupsParameters.RowFilter = parametersRowFilter;
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void DateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				dsTradingGroups = mainForm.ShortSaleAgent.TradingGroupsGet(DateTimePicker.Text, mainForm.UtcOffset);

				dvTradingGroupsParameters.Table = dsTradingGroups.Tables["TradingGroups"];

				TradingGroupsGrid.SetDataBinding(dsTradingGroups, "TradingGroups", true);
				TradingGroupsParametersGrid.SetDataBinding(dvTradingGroupsParameters, "", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "PremiumMin":
				case "PremiumMax":
				case "LocatesRequested":
				case "LocatesFilled":
				case "AutoApprovalMax":
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch { }
					break;

				case "MinPrice":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.00");
					}
					catch { }
					break;

				case "LastEmailDate":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
					}
					catch { }
					break;

				case "ActTime":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeFormat);
					}
					catch { }
					break;
			}
		}

		private void TradingGroupsParametersGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				mainForm.ShortSaleAgent.TradingGroupSet(
					TradingGroupsGrid.Columns["GroupCode"].Text,
					TradingGroupsGrid.Columns["GroupName"].Text,
					TradingGroupsParametersGrid.Columns["MinPrice"].Value.ToString(),
					TradingGroupsParametersGrid.Columns["AutoApprovalMax"].Value.ToString(),
					TradingGroupsParametersGrid.Columns["PremiumMin"].Value.ToString(),
					TradingGroupsParametersGrid.Columns["PremiumMax"].Value.ToString(),
					bool.Parse(TradingGroupsParametersGrid.Columns["AutoEmail"].Value.ToString()),
					TradingGroupsParametersGrid.Columns["EmailAddress"].Text,
					TradingGroupsParametersGrid.Columns["LastEmailDate"].Text,
					mainForm.UserId);

				TradingGroupsGrid.Columns["ActUserId"].Text = mainForm.UserId;
				TradingGroupsGrid.Columns["ActTime"].Value = DateTime.Now;
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void TradingGroupsGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{
			try
			{
				if (!bool.Parse(TradingGroupsGrid.Columns["IsActive"].CellValue(e.Row).ToString()))
				{
					e.CellStyle.BackColor = System.Drawing.Color.Gainsboro;
				}
			}
			catch { }
		}

		private void TradingGroupsParametersGrid_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)13))
			{
				TradingGroupsParametersGrid.UpdateData();
			}
		}

		private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				if (!e.KeyChar.Equals((char)13))
				{
					mainForm.ServiceAgent.KeyValueSet("ShortSaleLocatePriceThreshold", DefaultPriceMinimumTextBox.Value.ToString());
					mainForm.ServiceAgent.KeyValueSet("ShortSaleLocateAutoApprovalMax", DefaultAutoApprovalTextBox.Value.ToString());
					mainForm.ServiceAgent.KeyValueSet("ShortSaleLocatePremiumMin", PremiumMinTextBox.Value.ToString());
					mainForm.ServiceAgent.KeyValueSet("ShortSaleLocatePremiumMax", PremiumMaxTextBox.Value.ToString());
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
				e.Handled = false;
			}

			this.Cursor = Cursors.Default;
		}
	}
}