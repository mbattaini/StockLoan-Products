using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;

namespace CentralClient
{
	public partial class SplashStartUpInfoForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private DataSet dsHolidays;		
    	private MainForm mainForm;

		private string contractsBizDate = "";
		private string settlememtsSystem = "";
		private string marksBizDate = "";
		private string pricingBizDate = "";

		public SplashStartUpInfoForm(MainForm mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;
		}

		private void SplashStartUpInfoForm_Load(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			InfoLabel.Text = "Status For " + mainForm.ServiceAgent.BizDate();
			
			try
			{
				dsHolidays = new DataSet();
				dsHolidays = mainForm.AdminAgent.HolidaysGet(mainForm.ServiceAgent.BizDate(), mainForm.UtcOffset);				
           		
				contractsBizDate = mainForm.ServiceAgent.KeyValueGet("ContractsBizDate", "");
				settlememtsSystem = (mainForm.ServiceAgent.UseSystemSettlementEngine()) ? "Internal" : "Loanet";

				if (contractsBizDate.Equals(mainForm.ServiceAgent.BizDate()))
				{
					ContractsBizDateInfoLabel.ForeColor = System.Drawing.Color.MediumSeaGreen;
					ContractsBizDateInfoLabel.Text = "Contracts Rolled For " + mainForm.ServiceAgent.BizDate();
				}
				else
				{
					ContractsBizDateInfoLabel.ForeColor = System.Drawing.Color.Red;
					ContractsBizDateInfoLabel.Text = "Contracts Have Not Rolled For " + mainForm.ServiceAgent.BizDate();
				}

				//PricingUpdateInfoLabel.Text = "Not Applicable";
                PricingUpdateInfoLabel.Text = DateTime.Parse(mainForm.ServiceAgent.KeyValueGet("StaticDataCrestPriceLoadDate", "")).ToString(Standard.DateFormat);
                
                MarksAppliedInfoLabel.Text = "Not Applicable";
				
				SettlementsSystemInfoLabel.Text = settlememtsSystem;
				HolidaysGrid.SetDataBinding(dsHolidays, "Holidays", true);

                mainForm.StaticInformation.Refresh();
                BookGroupCombo.HoldFields();
                BookGroupCombo.DataSource = mainForm.StaticInformation.BookGroupData;
				BookGroupCombo.SelectedIndex = -1;

                mainForm.FundingRates.Refresh();
                FundDropdown.DataSource = mainForm.FundingRates.FundsData;
                FundingGrid.SetDataBinding(mainForm.FundingRates.FundingRatesData, "", true);

                if (BookGroupCombo.ListCount > 0)
                {
					BookGroupCombo.SelectedIndex = 0;
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void BookGroupCombo_TextChanged(object sender, EventArgs e)
		{
			try
			{
				DataSet dsDetails = new DataSet();

				dsDetails = mainForm.PositionAgent.ContractDetailsInfo(mainForm.ServiceAgent.BizDate(), BookGroupCombo.Text);

				if (dsDetails.Tables["ContractDetails"].Rows.Count > -1)
				{
					ContractSettledTodayLabel.Text = long.Parse(dsDetails.Tables["ContractDetails"].Rows[0]["Contracts"].ToString()).ToString("#,##0");
					ReturnSettledTodayLabel.Text = long.Parse(dsDetails.Tables["ContractDetails"].Rows[0]["Returns"].ToString()).ToString("#,##0");
					RecallSettledTodayLabel.Text = long.Parse(dsDetails.Tables["ContractDetails"].Rows[0]["Recalls"].ToString()).ToString("#,##0");
					MarkSettledTodayLabel.Text = long.Parse(dsDetails.Tables["ContractDetails"].Rows[0]["Marks"].ToString()).ToString("#,##0");
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}
      
        private void FundingGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            e.Value = mainForm.Format(e.Column.DataField, e.Value.ToString());
        }

        private void SplashStartUpInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.splashStartupInfoForm = null;
        }	
	}
}