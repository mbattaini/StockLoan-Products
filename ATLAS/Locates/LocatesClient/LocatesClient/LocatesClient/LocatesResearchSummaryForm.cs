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
	public partial class LocatesResearchSummaryForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm = null;
		private DataSet dsLocates = null;
		private DataSet dsLocatesSummary = null;
		private DataSet dsGroupCodeSummary = null;
		private DataView dvGroupCodeSummary = null;
		private DataView dvLocatesSummary = null;

		private string secId = "";
		private string groupCode = "";

		public LocatesResearchSummaryForm(MainForm mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;
		}

		private void LocatesResearchSummaryForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.mainForm.locatesResearchSummaryForm = null;
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

		private void LocatesResearchSummaryForm_Load(object sender, EventArgs e)
		{
			DockingTab.SelectedIndex = 0;
		}

		private void LocatesSummaryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			try
			{
				switch (LocatesSummaryGrid.Columns[e.ColIndex].DataField)
				{
					case ("ClientQuantity"):
					case ("Quantity"):
					case ("RequestedCount"):
						e.Value = long.Parse(e.Value).ToString("#,##0");
						break;
					case ("FeeRate"):
						e.Value = decimal.Parse(e.Value).ToString("##0.000");
						break;
				}
			}
			catch { }
		}

		private void LocatesSummaryGrid_Paint(object sender, PaintEventArgs e)
		{
			if (!secId.Equals(LocatesSummaryGrid.Columns["SecId"].Text))
			{
				secId = LocatesSummaryGrid.Columns["SecId"].Text;

				mainForm.SecId = secId;

				LocatesGridFill();
			}
		}

		private void GroupCodeSummaryGridFill()
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				dsGroupCodeSummary = mainForm.ShortSaleAgent.TradingGroupsGet(DateTimePicker.Text, mainForm.UtcOffset);

				dvGroupCodeSummary = new DataView(dsGroupCodeSummary.Tables["TradingGroups"], "", "LocatesRequested Desc", DataViewRowState.CurrentRows);
				
				GroupCodeSummaryGrid.SetDataBinding(dvGroupCodeSummary, "", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			GroupCodeSummaryFooterSet();
			
			this.Cursor = Cursors.Default;
		}


		private void LocatesSummaryGridFill()
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				dsLocatesSummary = mainForm.ShortSaleAgent.LocatesSummaryGet(DateTimePicker.Text);

				dvLocatesSummary = new DataView(dsLocatesSummary.Tables["LocatesSummary"], "", "ClientQuantity Desc", DataViewRowState.CurrentRows);

				LocatesSummaryGrid.SetDataBinding(dvLocatesSummary, "", true);			
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			LocatesSummaryFooterSet();

			this.Cursor = Cursors.Default;
		}


		private void LocatesGridFill()
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				if (DockingTab.SelectedIndex == 0)
				{
					dsLocates = mainForm.ShortSaleAgent.LocatesGet(DateTimePicker.Text, "", secId, mainForm.UtcOffset);
				}
				else
				{
					dsLocates = mainForm.ShortSaleAgent.LocatesGet(DateTimePicker.Text, groupCode, "", mainForm.UtcOffset);
				}

				LocatesGrid.SetDataBinding(dsLocates, "Table", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				if (DockingTab.SelectedIndex == 0)
				{
					LocatesSummaryGridFill();
				}
				else
				{
					GroupCodeSummaryGridFill();
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void GroupCodeSummaryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			try
			{
				switch (GroupCodeSummaryGrid.Columns[e.ColIndex].DataField)
				{
					case ("LocatesRequested"):
					case ("LocatesFilled"):
					case ("LocatesZero"):
					case ("LocatesFull"):
					case ("LocatesPartial"):
					case ("LocatesInvalid"):
					case ("LocatesPending"):
					case("LocatesEasy"):
						e.Value = long.Parse(e.Value).ToString("#,##0");
						break;
				}
			}
			catch { }
		}

		private void DockingTab_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				if (DockingTab.SelectedIndex == 0)
				{
					LocatesSummaryGridFill();
				}
				else
				{
					GroupCodeSummaryGridFill();
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void LocatesSummaryFooterSet()
		{
			long clientRequested = 0;
			long quantity = 0;
			long requestedCount = 0;
			
			for (int index = 0; index < dvLocatesSummary.Count; index++)
			{
				try
				{
					clientRequested += long.Parse(dvLocatesSummary[index]["ClientQuantity"].ToString());
				}
				catch (Exception error)
				{
					mainForm.Alert(this.Name, error.Message);
				}

				try
				{
					quantity += long.Parse(dvLocatesSummary[index]["Quantity"].ToString());
				}
				catch { }

				try
				{
					requestedCount += long.Parse(dvLocatesSummary[index]["RequestedCount"].ToString());
				}
				catch { }		
			}

			LocatesSummaryGrid.Columns["ClientQuantity"].FooterText = clientRequested.ToString("#,##0");
			LocatesSummaryGrid.Columns["Quantity"].FooterText = quantity.ToString("#,##0");
			LocatesSummaryGrid.Columns["RequestedCount"].FooterText = requestedCount.ToString("#,##0");		
		}
		
		
		private void GroupCodeSummaryFooterSet()
		{
			long locatesFullCount = 0;
			long locatesPartialCount = 0;
			long locatesZeroCount = 0;
			long locatesInvalidCount = 0;
			long locatesRequestedCount = 0;
			long locatesFilledCount = 0;
			long locatesPendingCount = 0;
			long locatesEasyCount = 0;

			for (int index = 0; index < dvGroupCodeSummary.Count; index++)
			{
				try
				{
					locatesFullCount += long.Parse(dvGroupCodeSummary[index]["LocatesFull"].ToString());
				}
				catch (Exception error)
				{
					mainForm.Alert(this.Name, error.Message);
				}

				try
				{
					locatesPartialCount += long.Parse(dvGroupCodeSummary[index]["LocatesPartial"].ToString());
				}
				catch { }

				try
				{
					locatesZeroCount += long.Parse(dvGroupCodeSummary[index]["LocatesZero"].ToString());
				}
				catch { }

				try
				{
					locatesInvalidCount += long.Parse(dvGroupCodeSummary[index]["LocatesInvalid"].ToString());
				}
				catch { }

				try
				{
					locatesRequestedCount += long.Parse(dvGroupCodeSummary[index]["LocatesRequested"].ToString());
				}
				catch { }

				try
				{
					locatesFilledCount += long.Parse(dvGroupCodeSummary[index]["LocatesFilled"].ToString());
				}
				catch { }

				try
				{
					locatesPendingCount += long.Parse(dvGroupCodeSummary[index]["LocatesPending"].ToString());
				}
				catch { }

				try
				{
					locatesEasyCount += long.Parse(dvGroupCodeSummary[index]["LocatesEasy"].ToString());
				}
				catch { }
			}

			GroupCodeSummaryGrid.Columns["LocatesFull"].FooterText = locatesFullCount.ToString("#,##0");
			GroupCodeSummaryGrid.Columns["LocatesPartial"].FooterText = locatesPartialCount.ToString("#,##0");
			GroupCodeSummaryGrid.Columns["LocatesZero"].FooterText = locatesZeroCount.ToString("#,##0");
			GroupCodeSummaryGrid.Columns["LocatesInvalid"].FooterText = locatesInvalidCount.ToString("#,##0");
			GroupCodeSummaryGrid.Columns["LocatesRequested"].FooterText = locatesRequestedCount.ToString("#,##0");
			GroupCodeSummaryGrid.Columns["LocatesFilled"].FooterText = locatesFilledCount.ToString("#,##0");
			GroupCodeSummaryGrid.Columns["LocatesPending"].FooterText = locatesPendingCount.ToString("#,##0");
			GroupCodeSummaryGrid.Columns["LocatesEasy"].FooterText = locatesEasyCount.ToString("#,##0");	
		}

		private void DateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				if (DockingTab.SelectedIndex == 0)
				{
					LocatesSummaryGridFill();
				}
				else
				{
					GroupCodeSummaryGridFill();
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				SaveFileDialog.ShowDialog();

				if (DockingTab.SelectedIndex == 0)
				{
					LocatesSummaryGrid.ExportToExcel(SaveFileDialog.FileName);
				}
				else 
				{
					GroupCodeSummaryGrid.ExportToExcel(SaveFileDialog.FileName);
				}		
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void GroupCodeSummaryGrid_Paint(object sender, PaintEventArgs e)
		{
			if (!groupCode.Equals(GroupCodeSummaryGrid.Columns["GroupCode"].Text))
			{
				groupCode = GroupCodeSummaryGrid.Columns["GroupCode"].Text;

				LocatesGridFill();
			}
		}

		private void SendToLocatesExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			Excel excel = new Excel();

			try
			{
				SaveFileDialog.ShowDialog();

				if (DockingTab.SelectedIndex == 0)
				{
					Excel.ExportGridToExcel(ref LocatesSummaryGrid, SaveFileDialog.FileName, 0, null);					
				}
				else
				{
					Excel.ExportGridToExcel(ref GroupCodeSummaryGrid, SaveFileDialog.FileName, 0, null);					
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
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
                if (e.KeyChar.Equals((char)32) && LocatesGrid.Col.Equals(9) && LocatesGrid.Columns["Quantity"].Text.Trim().Equals("") && LocatesGrid.EditActive)
                {
                    LocatesGrid.Columns["Quantity"].Text = LocatesGrid.Columns["Request"].Text;
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