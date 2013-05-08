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
	public partial class LocatesSummaryForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm = null;
		private static int QUANTITY = 9;
		private DataSet dsLocates = null;
		private DataSet dsInventory = null;
		private DataSet dsLocatesGroupCodeSummary = null;
		private DataSet dsLocatesSecIdSummary = null;
		private DataSet dsLocatesSupport = null;

		private DataView dvLocates = null;
		private DataView dvLocatesSummary = null;
        private DataView dvLocatesPending = null;

		private string dvFilter = "";
		private string groupCode = "";
		private string secId = "";
		private string locatesSecId = "";

		private delegate void LocatesSupportDelagate();
		

		public LocatesSummaryForm(MainForm mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;
		}

		private void LocatesSummaryForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
				{
					RegistryValue.Write(this.Name, "Top", this.Top.ToString());
					RegistryValue.Write(this.Name, "Left", this.Left.ToString());
					RegistryValue.Write(this.Name, "Height", this.Height.ToString());
					RegistryValue.Write(this.Name, "Width", this.Width.ToString());
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			mainForm.locatesSummaryForm = null;
		}

		private void LocatesSummaryForm_Load(object sender, EventArgs e)
		{
			try
			{
				this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "100"));
				this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "100"));
				this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "480"));
				this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "1176"));

				InitialLocatesSecIdSummaryLoad();
				InitialLocatesLoad();				
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		private void InitialLocatesLoad()
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				dsLocates = mainForm.ShortSaleAgent.LocatesGet(DateTimePicker.Text, "", LocatesSecIdGrid.Columns["SecId"].Text, 0);

				dsLocates.Tables["Table"].PrimaryKey = new DataColumn[1] {
                                                                        dsLocates.Tables["Table"].Columns["LocateId"]};

				dvLocates = new DataView(dsLocates.Tables["Table"], "SecId = ''", "", DataViewRowState.CurrentRows);
				dvLocatesSummary = new DataView(dsLocates.Tables["Table1"], "SecId = ''", "", DataViewRowState.CurrentRows);

				LocatesGrid.SetDataBinding(dvLocates, "", true);
				LocatesSummaryGrid.SetDataBinding(dvLocatesSummary, "", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void InitialLocatesSecIdSummaryLoad()
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				dsLocatesSecIdSummary = mainForm.ShortSaleAgent.LocatesSecIdSummaryGet(DateTimePicker.Text);

                dvLocatesPending = new DataView(dsLocatesSecIdSummary.Tables["SecIdSummary"], "LocatesPending > 0", "LocatesPending ASC", DataViewRowState.CurrentRows);
                
                LocatesSecIdGrid.SetDataBinding(dvLocatesPending, "", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void InitialLocatesGroupCodeSummaryLoad()
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				dsLocatesGroupCodeSummary = mainForm.ShortSaleAgent.TradingGroupsGet(DateTimePicker.Text, mainForm.UtcOffset);

                dvLocatesPending = new DataView(dsLocatesGroupCodeSummary.Tables["TradingGroups"], "LocatesPending > 0", "LocatesPending ASC", DataViewRowState.CurrentRows);
    
				LocatesSecIdGrid.SetDataBinding(dsLocatesGroupCodeSummary, "TradingGroups", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void LocatesGroupCodeSummaryUpdate()
		{
			this.Cursor = Cursors.WaitCursor;

			DataSet dsTemp = new DataSet();
			string groupCodeMemory = "";
			int row = -1;

			try
			{
				groupCodeMemory = LocatesGroupCodeGrid.Columns["GroupCode"].Text;

				dsTemp = mainForm.ShortSaleAgent.TradingGroupsGet(DateTimePicker.Text, mainForm.UtcOffset);

				LocatesGroupCodeGrid.SetDataBinding(dsTemp, "TradingGroups", true);

				for (int i = 0; i < dsTemp.Tables["TradingGroups"].Rows.Count; i++)
				{
					if (LocatesGroupCodeGrid.Columns["GroupCode"].CellText(i).Equals(groupCodeMemory))
					{
						row = i;
						break;
					}
				}

				LocatesGroupCodeGrid.Row = row;
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void LocatesSecIdSummaryUpdate()
		{
			this.Cursor = Cursors.WaitCursor;

			DataSet dsTemp = new DataSet();
			string securityMemory = "";
			int row = -1;

			try
			{
				securityMemory = LocatesSecIdGrid.Columns["SecId"].Text;

				dsTemp = mainForm.ShortSaleAgent.LocatesSecIdSummaryGet(DateTimePicker.Text);

				LocatesSecIdGrid.SetDataBinding(dsTemp, "SecIdSummary", true);

				for (int i = 0; i < dsTemp.Tables["SecIdSummary"].Rows.Count; i++)
				{
					if (LocatesSecIdGrid.Columns["SecId"].CellText(i).Equals(securityMemory))
					{
						row = i;
						break;
					}
				}

				LocatesSecIdGrid.Row = row;
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void LocatesSummaryUpdate(string filter)
		{
			dsLocates = new DataSet();

			this.Cursor = Cursors.WaitCursor;

			try
			{
				if (MainDockingTab.SelectedIndex == 0)
				{
					dsLocates = mainForm.ShortSaleAgent.LocatesByStatusGet(DateTimePicker.Text, "", filter, "Pending", mainForm.UtcOffset);
				}
				else
				{
                    dsLocates = mainForm.ShortSaleAgent.LocatesByStatusGet(DateTimePicker.Text, filter, "", "Pending", mainForm.UtcOffset);
				}

				dvLocates = new DataView(dsLocates.Tables["Table"], "", "", DataViewRowState.CurrentRows);
				dvLocatesSummary = new DataView(dsLocates.Tables["Table1"], "", "", DataViewRowState.CurrentRows);

				LocatesSummaryGrid.SetDataBinding(dvLocatesSummary, "", true);
				LocatesGrid.SetDataBinding(dvLocates, "", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void LocatesGroupCodeGrid_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				if (!groupCode.Equals(LocatesGroupCodeGrid.Columns["GroupCode"].Text))
				{
					LocatesSummaryUpdate(LocatesGroupCodeGrid.Columns["GroupCode"].Text);
					groupCode = LocatesGroupCodeGrid.Columns["GroupCode"].Text;
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		private void LocatesGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			try
			{
				switch (LocatesGrid.Columns[e.ColIndex].DataField)
				{
					case ("LocateIdTail"):
						e.Value = e.Value.ToString().PadLeft(5, '0');
						break;
					case ("OpenTime"):
						e.Value = DateTime.Parse(e.Value).ToString(Standard.TimeFileFormat);
						break;
					case ("ClientQuantity"):
					case ("Quantity"):
						e.Value = long.Parse(e.Value).ToString("#,##0");
						break;
					case ("ActTime"):
						e.Value = Tools.FormatDate(e.Value.ToString(), "HH:mm:ss");
						break;
				}
			}
			catch { }
		}

		private void LocatesSecIdGrid_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				if (!secId.Equals(LocatesSecIdGrid.Columns["SecId"].Text))
				{					
					LocatesSummaryUpdate(LocatesSecIdGrid.Columns["SecId"].Text);
					secId = LocatesSecIdGrid.Columns["SecId"].Text;					
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		private void RefreshTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				GridRefresh();
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		private void MainDockingTab_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				GridRefresh();
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		private void ShowPendingCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (ShowPendingCheckBox.Checked)
				{
					dvFilter = "Status = 'Pending'";
				}
				else
				{
					dvFilter = "";
				}

				if (dvLocates != null)
				{
					dvLocates.RowFilter = dvFilter;
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		private void GridRefresh()
		{
			if (!LocatesSummaryGrid.EditActive)
			{
				if (MainDockingTab.SelectedIndex == 0)
				{					
					LocatesSecIdSummaryUpdate();
				}
				else
				{					
					LocatesGroupCodeSummaryUpdate();				
				}
			}
		}

		private void LocatesGrid_Paint(object sender, PaintEventArgs e)
		{
			if ((!secId.Equals(LocatesGrid.Columns["SecId"].Text)) || (MainDockingTab.SelectedIndex == 0))
			{
				if ((dvLocatesSummary != null) && !LocatesGrid.Columns["SecId"].Text.Equals(""))
				{
					LocatesSummaryGrid.Splits[0].Caption = "Locates Summary For " + LocatesGrid.Columns["Symbol"].Text + " [" + LocatesGrid.Columns["SecId"].Text + "]";
					dvLocatesSummary.RowFilter = "SecId = '" + LocatesGrid.Columns["SecId"].Text + "'";

					LocatesInventorySummaryUpdate();
					LocatesSummaryFooterCalc();
				}
			}

			if (!locatesSecId.Equals(LocatesGrid.Columns["SecId"].Text))
			{
				locatesSecId = LocatesGrid.Columns["SecId"].Text;
				mainForm.SecId = locatesSecId;				
			}
		}


		private void LocatesInventorySummaryUpdate()
		{
			try
			{
				dsInventory = mainForm.ShortSaleAgent.InventoryGet(LocatesGrid.Columns["GroupCode"].Text, LocatesGrid.Columns["SecId"].Text, 0);
				InventoryGrid.SetDataBinding(dsInventory, "Inventory", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		private void LocatesSummaryFooterCalc()
		{
			long requestedQuantity = 0;
			long allocatedQuantity = 0;

			try
			{
				if (dvLocatesSummary != null)
				{
					for (int i = 0; i < dvLocatesSummary.Count; i++)
					{
						requestedQuantity += long.Parse(dvLocatesSummary[i]["ClientQuantity"].ToString());
						allocatedQuantity += long.Parse(dvLocatesSummary[i]["Quantity"].ToString());
					}
				}

				LocatesSummaryGrid.Columns["ClientQuantity"].FooterText = requestedQuantity.ToString("#,##0");
				LocatesSummaryGrid.Columns["Quantity"].FooterText = allocatedQuantity.ToString("#,##0");
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
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

		private void LocatesSummaryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "ClientQuantity":
				case "Quantity":
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch { }
					break;
			}
		}

		private void LocatesGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			try
			{
				mainForm.ShortSaleAgent.LocatesBeginEdit(LocatesGrid.Columns["LocateId"].Text, "ADMIN");
			}
			catch
			{
				e.Cancel = true;
			}
		}

		private void LocatesGrid_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
		{
			try
			{
				mainForm.ShortSaleAgent.LocatesEndEdit(LocatesGrid.Columns["LocateId"].Text, "ADMIN");
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		private void LocatesSupportUpdate()
		{
			try
			{
				dsLocatesSupport = mainForm.ShortSaleAgent.LocatesEditing();
				LocatesGrid.Refresh();
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		private bool LocatesSupportCheck(string locateId)
		{
			try
			{
				if (dsLocatesSupport != null)
				{
					for (int i = 0; i < dsLocatesSupport.Tables["Editing"].Rows.Count; i++)
					{
						if (locateId.Equals(dsLocatesSupport.Tables["Editing"].Rows[i]["LocateId"].ToString()))
						{
							if ((LocatesGrid.EditActive) && (LocatesGrid.Columns["LocateId"].Text.Equals(locateId)))
							{
								return false;
							}
							else
							{
								return true;
							}
						}
					}
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			return false;
		}

		private void LocatesGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{
			try
			{
				if (LocatesSupportCheck(LocatesGrid.Columns["LocateId"].CellText(e.Row)))
				{
					e.CellStyle.BackColor = System.Drawing.Color.LightYellow;
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
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

		private void LocatesGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			DataSet dsTemp = new DataSet();

			this.Cursor = Cursors.WaitCursor;

			try
			{
				mainForm.ShortSaleAgent.LocateItemSet(
						(long)LocatesGrid.Columns["LocateId"].Value,
						LocatesGrid.Columns["Located"].Value.ToString(),
						LocatesGrid.Columns["Source"].Text,
						LocatesGrid.Columns["FeeRate"].Value.ToString(),
						LocatesGrid.Columns["PreBorrow"].Value.ToString(),
						LocatesGrid.Columns["Comment"].Text,
						mainForm.UserId
						);
				dsTemp = mainForm.ShortSaleAgent.LocateItemGet(LocatesGrid.Columns["LocateId"].Text, mainForm.UtcOffset);

				for (int i = 0; i < dsTemp.Tables["Table"].Rows.Count; i++)
				{
					dsLocates.Tables["Table"].LoadDataRow(dsTemp.Tables["Table"].Rows[i].ItemArray, true);
				}

				dsLocates.AcceptChanges();
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
				e.Cancel = true;
			}

			dsTemp = null;

			this.Cursor = Cursors.Default;
		}

        private void LocatesGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals((char)32) && LocatesGrid.Col.Equals(QUANTITY) && LocatesGrid.Columns[QUANTITY].Text.Trim().Equals("") && LocatesGrid.EditActive)
                {
                    LocatesGrid.Columns[QUANTITY].Text = LocatesGrid.Columns["Request"].Text;
                    return;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }
	}
}