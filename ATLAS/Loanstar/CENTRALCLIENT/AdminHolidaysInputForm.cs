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
	public partial class AdminHolidaysInputForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm = null;
		private DataSet dsCountries = null;

		private string date = "";
		private string countryCode = "";

		public AdminHolidaysInputForm(MainForm mainForm, string date, string countryCode)
		{
			InitializeComponent();

			this.mainForm = mainForm;
			this.date = date;
			this.countryCode = countryCode;
		}

		private void AdminHolidaysInputForm_Load(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				dsCountries = mainForm.ServiceAgent.CountriesGet("");

				CountryCodeCombo.HoldFields();
				CountryCodeCombo.DataSource = dsCountries.Tables["Countries"];
				CountryCodeCombo.DataMember = "CountryCode";

				if (dsCountries.Tables["Countries"].Rows.Count > 0)
				{
					CountryCodeCombo.SelectedIndex = 0;
				}

				HolidayDetailsLoad();

				HolidayDateTextBox.Text = date;
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void HolidayDetailsLoad()
		{
			try
			{
				DataSet dsHolidays = mainForm.AdminAgent.HolidaysGet(mainForm.UtcOffset);

				foreach (DataRow drHoliday in dsHolidays.Tables["Holidays"].Rows)
				{
					if (DateTime.Parse(drHoliday["HolidayDate"].ToString()).ToString(Standard.DateFormat).Equals(date) && drHoliday["CountryCode"].ToString().Equals(countryCode))
					{
						IsBizDateHolidayCheckBox.Checked = bool.Parse(drHoliday["IsBizDateHoliday"].ToString());
						IsBankHolidayCheckBox.Checked = bool.Parse(drHoliday["IsBankHoliday"].ToString());
						IsExchangeHolidayCheckBox.Checked = bool.Parse(drHoliday["IsExchangeHoliday"].ToString());

						if (!IsBizDateHolidayCheckBox.Checked)
						{
							IsSystemNotificationCheckBox.Checked = true;
						}

						LastModifiedByValueLabel.Text = drHoliday["ActUserId"].ToString();
						LastModifiedValueLabel.Text = drHoliday["ActTime"].ToString();
					}
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}
		}

	private void IsSystemNotificationCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			IsBankHolidayCheckBox.Checked = !IsSystemNotificationCheckBox.Checked;
			IsExchangeHolidayCheckBox.Checked = !IsSystemNotificationCheckBox.Checked;
			IsBizDateHolidayCheckBox.Checked =  !IsSystemNotificationCheckBox.Checked;
		}

		private void IsBizDateHolidayCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			IsSystemNotificationCheckBox.Checked = !IsBizDateHolidayCheckBox.Checked;
		}

        private void SaevRibbonButton_Click(object sender, EventArgs e)
        {
            try
            {
                mainForm.AdminAgent.HolidaysSet(
                    HolidayDateTextBox.Text,
                    CountryCodeCombo.Text,
                    DescriptionTextBox.Text,
                    IsBankHolidayCheckBox.Checked,
                    IsExchangeHolidayCheckBox.Checked,
                    IsBizDateHolidayCheckBox.Checked,
                    mainForm.UserId,
                    true);

                LastModifiedByValueLabel.Text = mainForm.UserId;
                LastModifiedValueLabel.Text = DateTime.Now.ToString(Standard.DateTimeShortFormat);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void CancelRibbonButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   
	}
}