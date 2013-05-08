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
	public partial class ProcessStatusForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm;
		private DataSet dsProcessStatus;

		public ProcessStatusForm(MainForm mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;
		}

		private void ProcessStatusForm_Load(object sender, EventArgs e)
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
				//dsProcessStatus = mainForm.ProcessAgent.ProcessStatusGet(mainForm.ServiceAgent.BizDate(), mainForm.UtcOffset);
				ProcessStatusGrid.SetDataBinding(dsProcessStatus, "ProcessStatuses", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void ProcessStatusForm_FormClosed(object sender, FormClosedEventArgs e)
		{
				if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());
			}

			mainForm.processStatusForm = null;
		}

		private void ProcessStatusGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (ProcessStatusGrid.Columns[e.ColIndex].DataField)
			{
				case ("ActTime"):
				case ("StatusTime"):
					try
					{
						e.Value = DateTime.Parse(e.Value).ToString(Standard.TimeFileFormat);
					}
					catch { }
					break;

				case ("Quantity"):
					try
					{
						e.Value = long.Parse(e.Value).ToString("#,##0");
					}
					catch { }
					break;

				case ("Amount"):
					try
					{
						e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
					}
					catch { }
					break;
			}
		}
	}
}