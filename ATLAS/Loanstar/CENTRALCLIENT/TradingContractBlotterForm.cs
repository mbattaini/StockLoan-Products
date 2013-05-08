using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;
using StockLoan.MainBusiness;

namespace CentralClient
{
	public partial class TradingContractBlotterForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm;
        private DataSet dsBookGroups = null;
		private DataSet dsDeals = null;
		
		private DataView dvLoans = null;
		private DataView dvBorrows = null;

		public TradingContractBlotterForm(MainForm mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;
		}

		private void TradingContractBlotterForm_Load(object sender, EventArgs e)
		{
            this.Cursor = Cursors.WaitCursor;

			try
			{
                int height = this.Height;
                int width = this.Width;

                this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
                this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
                this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
                this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

                DateTimePicker.Text = mainForm.ServiceAgent.BizDate();

                dsBookGroups = mainForm.ServiceAgent.BookGroupGet(mainForm.UserId);
				
				BookGroupNameLabel.DataSource = dsBookGroups.Tables["BookGroups"];
				BookGroupNameLabel.DataField = "BookGroupName";

				BookGroupCombo.HoldFields();
				BookGroupCombo.DataSource = dsBookGroups.Tables["BookGroups"];
				BookGroupCombo.SelectedIndex = -1;

				if (dsBookGroups.Tables["BookGroups"].Rows.Count > 0)
				{
					BookGroupCombo.SelectedIndex = 0;
				}
			
				dsDeals = mainForm.PositionAgent.DealDataGet((short)mainForm.UtcOffset, DateTimePicker.Text, true.ToString());

				dvLoans = new DataView(dsDeals.Tables["Deals"], "BookGroup = '" + BookGroupCombo.Text + "' AND DealType = 'L' AND IsActive = 1 AND DealStatus <> 'C'", "", DataViewRowState.CurrentRows);
				dvBorrows = new DataView(dsDeals.Tables["Deals"], "BookGroup = '" + BookGroupCombo.Text + "' AND DealType = 'B' AND IsActive = 1 AND DealStatus <> 'C'", "", DataViewRowState.CurrentRows);

				LoansGrid.SetDataBinding(dvLoans, "", true);
				BorrowsGrid.SetDataBinding(dvBorrows, "", true);

			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
            this.Cursor = Cursors.Default;
		}

		private void BookGroupCombo_TextChanged(object sender, EventArgs e)
		{
			if (dvLoans != null)
			{
				dvLoans.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND DealType = 'L' AND IsActive = 1 AND DealStatus <> 'C'";
			}

			if (dvBorrows != null)
			{
				dvBorrows.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND DealType = 'B' AND IsActive = 1 AND DealStatus <> 'C'";
			}		
		}

		private void TradingContractBlotterForm_FormClosed(object sender, FormClosedEventArgs e)
		{
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

			mainForm.tradingContractBlotterForm = null;
		}

		private void FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
            e.Value = mainForm.Format(e.Column.DataField, e.Value);
		}

		private void SendToContractCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			long dealCount = 0;

			if (LoansGrid.Focused)
			{
				foreach (int i in LoansGrid.SelectedRows)
				{
					switch (LoansGrid.Columns["DealStatus"].CellText(i))
					{
						case "D":                            
					        mainForm.PositionAgent.DealSet(LoansGrid.Columns["DealId"].CellText(i), "S", mainForm.UserId, true);
							dealCount++;
							break;
					}
				}

				mainForm.Alert(this.Name, "Sent " + dealCount.ToString("#,##0") + " loan deals to contract");
			}
			else if (BorrowsGrid.Focused)
			{
				foreach (int i in BorrowsGrid.SelectedRows)
				{
                    switch (BorrowsGrid.Columns["DealStatus"].CellText(i))
                    {
                        case "D":                            
                            mainForm.PositionAgent.DealSet(BorrowsGrid.Columns["DealId"].CellText(i), "S", mainForm.UserId, true);
                            dealCount++;
                            break;
                    }
				}

				mainForm.Alert(this.Name, "Sent " + dealCount.ToString("#,##0") + " borrow deals to contract");
			}

			this.Cursor = Cursors.Default;
		}

		private void DateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			try
			{				
				dsDeals = mainForm.PositionAgent.DealDataGet((short)mainForm.UtcOffset, DateTimePicker.Text, true.ToString());

				dvLoans = new DataView(dsDeals.Tables["Deals"], "BookGroup = '" + BookGroupCombo.Text + "' AND DealType = 'L' AND IsActive = 1 AND DealStatus <> 'C'", "", DataViewRowState.CurrentRows);
				dvBorrows = new DataView(dsDeals.Tables["Deals"], "BookGroup = '" + BookGroupCombo.Text + "' AND DealType = 'B' AND IsActive = 1 AND DealStatus <> 'C'", "", DataViewRowState.CurrentRows);

				LoansGrid.SetDataBinding(dvLoans, "", true);
				BorrowsGrid.SetDataBinding(dvBorrows, "", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

		private void BorrowsGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{
			switch (BorrowsGrid.Columns["DealStatus"].CellText(e.Row))
			{
				case ("E"):
					e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
					break;
			}
		}

		private void LoansGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{
			switch (LoansGrid.Columns["DealStatus"].CellText(e.Row))
			{
				case ("E"):
					e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
					break;
			}
		}	

        private void SendToClipboardCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (BorrowsGrid.Focused)
                {                    
                    mainForm.SendToClipboard(ref BorrowsGrid);
                }
                else if(LoansGrid.Focused)
                {                 
                    mainForm.SendToClipboard(ref LoansGrid);
                }                
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (BorrowsGrid.Focused)
                {                 
                    mainForm.SendToExcel(ref BorrowsGrid, true);
                }
                else if(LoansGrid.Focused)
                {                   
                    mainForm.SendToExcel(ref LoansGrid, true);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void SendToEmailCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (BorrowsGrid.Focused)
                {
                    mainForm.SendToEmail(ref BorrowsGrid);
                }
                else if (LoansGrid.Focused)
                {
                    mainForm.SendToEmail(ref LoansGrid);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

        }

		private void RefreshCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				dsDeals = mainForm.PositionAgent.DealDataGet((short)mainForm.UtcOffset, DateTimePicker.Text, true.ToString());

				dvLoans = new DataView(dsDeals.Tables["Deals"], "BookGroup = '" + BookGroupCombo.Text + "' AND DealType = 'L' AND IsActive = 1 AND DealStatus <> 'C'", "", DataViewRowState.CurrentRows);
				dvBorrows = new DataView(dsDeals.Tables["Deals"], "BookGroup = '" + BookGroupCombo.Text + "' AND DealType = 'B' AND IsActive = 1 AND DealStatus <> 'C'", "", DataViewRowState.CurrentRows);

				LoansGrid.SetDataBinding(dvLoans, "", true);
				BorrowsGrid.SetDataBinding(dvBorrows, "", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

        private void NewDealCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            TradingDealMaintenanceForm dealMaintenanceForm = new TradingDealMaintenanceForm(mainForm,"", "");
            dealMaintenanceForm.Show();
        }

        private void BorrowsGrid_DoubleClick(object sender, EventArgs e)
        {
            TradingDealMaintenanceForm dealMaintenanceForm = new TradingDealMaintenanceForm(mainForm, DateTimePicker.Text, BorrowsGrid.Columns["DealId"].Text);
            dealMaintenanceForm.Show();
        }

        private void LoansGrid_DoubleClick(object sender, EventArgs e)
        {
            TradingDealMaintenanceForm dealMaintenanceForm = new TradingDealMaintenanceForm(mainForm, DateTimePicker.Text, LoansGrid.Columns["DealId"].Text);
            dealMaintenanceForm.Show();
        }

        private void LoansGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mainForm.PositionAgent.DealSet(LoansGrid.Columns["DealId"].Text, LoansGrid.Columns["DealStatus"].Text, mainForm.UserId, false);
                LoansGrid.Columns["IsActive"].Value = false;
                LoansGrid.UpdateData();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void BorrowsGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mainForm.PositionAgent.DealSet(BorrowsGrid.Columns["DealId"].Text, BorrowsGrid.Columns["DealStatus"].Text, mainForm.UserId, false);
                BorrowsGrid.Columns["IsActive"].Value = false;
                BorrowsGrid.UpdateData();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void LoansGrid_Error(object sender, C1.Win.C1TrueDBGrid.ErrorEventArgs e)
        {
            e.Handled = true;
            e.Continue = true;
        }

        private void BorrowsGrid_Error(object sender, C1.Win.C1TrueDBGrid.ErrorEventArgs e)
        {
            e.Handled = true;
            e.Continue = true;
        }
	}
}