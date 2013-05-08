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
	public partial class AdminSecMasterForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private DataSet dsSecMaster;
		private DataView dvSecMaster;

        private AdminSecMasterUploadPricingForm adminSecMasterUploadPricingForm = null;

		private MainForm mainForm = null;

		public AdminSecMasterForm(MainForm mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;
		}

		private void AdminSecMasterForm_Load(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			int height = this.Height;
			int width = this.Width;

			this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
			this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
			this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
			this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

          
			this.Cursor = Cursors.Default;
		}

		private void AdminSecMasterForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());
			}

			mainForm.adminSecMasterForm = null;
		}

		private void SecMasterGrid_BeforeOpen(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			dvSecMaster.RowFilter = "SecId = '" + SecMasterGrid.Columns["SecId"].Text + "' AND CountryCode = '" + SecMasterGrid.Columns["CountryCode"].Text + "'";
		}

		private void SecMasterGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
		{
			StatusMessageLabel.Text = "Showing: " + SecMasterGrid.RowCount.ToString("#,##0") + " items.";
		}	

        private void SendToClipboardCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (SecMasterGrid.Focused)
                {
                    mainForm.SendToClipboard(ref SecMasterGrid);
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
                if (SecMasterGrid.Focused)
                {
                    mainForm.SendToExcel(ref SecMasterGrid, true);
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
                if (SecMasterGrid.Focused)
                {
                    mainForm.SendToEmail(ref SecMasterGrid);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

        }

        private void UploadPricingCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            adminSecMasterUploadPricingForm = new AdminSecMasterUploadPricingForm(mainForm);
            adminSecMasterUploadPricingForm.Show();
        }

        private void SecMasterLoadBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            SecMasterLoadBackgroundWorker.ReportProgress(25);

            dsSecMaster = mainForm.ServiceAgent.SecMasterGet();

            SecMasterLoadBackgroundWorker.ReportProgress(50);

            dvSecMaster = new DataView(dsSecMaster.Tables["SecMaster"], "SecId = ''", "", DataViewRowState.CurrentRows);

            SecMasterLoadBackgroundWorker.ReportProgress(75);

            SecMasterGrid.SetDataBinding(dsSecMaster, "SecMaster", true);
            SecMasterDetailsGrid.SetDataBinding(dvSecMaster, "", true);

            SecMasterLoadBackgroundWorker.ReportProgress(100);
            
        }

        private void SecMasterLoadBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SecMasterRibbonProgressBar.Value = e.ProgressPercentage;
        }

        private void SecMasterLoadBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                StatusMessageLabel.Text = "Showing: " + SecMasterGrid.RowCount.ToString("#,##0") + " items.";
                SecMasterRibbonProgressBar.Visible = false;
            }
        }

        private void LoadRibbonButton_Click(object sender, EventArgs e)
        {
            this.StatusMessageLabel.Text = "Loading..";
            this.SecMasterLoadBackgroundWorker.RunWorkerAsync();
        }


	}
}