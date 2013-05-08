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
	public partial class AdminCurrenciesForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private DataSet dsCurrencies = null;
		private MainForm mainForm = null;

		public AdminCurrenciesForm(MainForm mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;
		}

		private void AdminCurrenciesForm_Load(object sender, EventArgs e)
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
                CurrenciesBackgroundWorker.RunWorkerAsync();
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name,error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void AdminCurrenciesForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());
			}

			mainForm.adminCurrenciesForm = null;
		}

        private void SendToClipboardCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (CurrenciesGrid.Focused)
                {
                    mainForm.SendToClipboard(ref CurrenciesGrid);
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
                if (CurrenciesGrid.Focused)
                {
                    mainForm.SendToExcel(ref CurrenciesGrid, true);
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
                if (CurrenciesGrid.Focused)
                {
                    mainForm.SendToEmail(ref CurrenciesGrid);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void CurrenciesBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CurrenciesBackgroundWorker.ReportProgress(33);
            dsCurrencies = mainForm.ServiceAgent.CurrenciesGet();
            
            CurrenciesBackgroundWorker.ReportProgress(66);
            CurrenciesGrid.SetDataBinding(dsCurrencies, "Currencies", true);
            
            CurrenciesBackgroundWorker.ReportProgress(100);
        }

        private void CurrenciesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                StatusMessageLabel.Text = "Showing: " + CurrenciesGrid.RowCount.ToString("#,##0") + " items.";
                CurrenciesRibbonProgressBar.Visible = false;
            }
        }

        private void CurrenciesBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrenciesRibbonProgressBar.Value = e.ProgressPercentage;
        }


	}
}