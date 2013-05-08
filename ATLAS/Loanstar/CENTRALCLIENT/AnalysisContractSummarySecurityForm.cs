using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Main;

namespace CentralClient
{
	public partial class AnalysisContractSummarySecurityForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm;
		private DataSet dsBookGroups;

		public AnalysisContractSummarySecurityForm(MainForm mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;
		}

		private void GenerateButton_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			string fileName = "";

			try
			{
				fileName = mainForm.PositionAgent.Report_ContractBySecurity(DateTimePicker.Text, BookGroupCombo.Text);

				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.EnableRaisingEvents = false;
				proc.StartInfo.FileName = fileName;
				proc.Start();
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void AnalysisContractSummarySecurityForm_Load(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			
			try
			{
				dsBookGroups = mainForm.ServiceAgent.BookGroupGet();

				BookGroupCombo.HoldFields();
				BookGroupCombo.DataSource = dsBookGroups.Tables["BookGroups"];
				BookGroupCombo.SelectedIndex = -1;
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}
	}
}