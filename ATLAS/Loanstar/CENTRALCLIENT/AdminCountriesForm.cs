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
	public partial class AdminCountriesForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private DataSet dsCountries = null;
		private MainForm mainForm = null;

		public AdminCountriesForm(MainForm mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;
		}

		private void AdminCountriesForm_Load(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			int height = this.Height;
			int width = this.Width;

			this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
			this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
			this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
			this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

			try
			{
                StatusMessageLabel.Text = "Loading..";
                CountryCodeBackgroundWorker.RunWorkerAsync();
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void AdminCountriesForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());
			}

			mainForm.adminCountriesForm = null;
		}

		private void CountriesGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				mainForm.ServiceAgent.CountrySet(
					CountriesGrid.Columns["CountryCode"].Text,
					CountriesGrid.Columns["Country"].Text,
					CountriesGrid.Columns["SettleDays"].Text,
					true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void CountriesGrid_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)13))
			{
				CountriesGrid.UpdateData();
			}
		}

        private void SendToClipboardCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (CountriesGrid.Focused)
                {
                    mainForm.SendToClipboard(ref CountriesGrid);
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
                if (CountriesGrid.Focused)     
                {
                    mainForm.SendToExcel(ref CountriesGrid, true);
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
                if (CountriesGrid.Focused)
                {
                    mainForm.SendToEmail(ref CountriesGrid);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void CountryCodeBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CountryCodeBackgroundWorker.ReportProgress(33);
            dsCountries = mainForm.ServiceAgent.CountriesGet("");
            
            CountryCodeBackgroundWorker.ReportProgress(66);
            CountriesGrid.SetDataBinding(dsCountries, "Countries", true);
            
            CountryCodeBackgroundWorker.ReportProgress(100);
        }

        private void CountryCodeBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CountryCodeRibbonProgressBar.Value = e.ProgressPercentage;
        }

        private void CountryCodeBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                StatusMessageLabel.Text = "Download Canceled.";
            }
            else if (e.Error != null)
            {
                StatusMessageLabel.Text = "Download Error.";
                mainForm.Alert(this.Name, e.Error.Message);
            }
            else
            {
                StatusMessageLabel.Text = "Showing: " + CountriesGrid.RowCount.ToString("#,##0") + " items.";
                CountryCodeRibbonProgressBar.Visible = false;
            }
        }

        
	}
}