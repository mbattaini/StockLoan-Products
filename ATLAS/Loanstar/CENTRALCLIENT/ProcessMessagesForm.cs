using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Medalist;
using StockLoan.Common;

namespace CentralClient
{
	public partial class ProcessMessagesForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private MainForm mainForm;
		private DataSet dsProcessMessages;

		public ProcessMessagesForm(MainForm mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;
		}

		private void ProcessMessagesForm_Load(object sender, EventArgs e)
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
				dsProcessMessages = mainForm.ProcessAgent.ProcessMessageGet(mainForm.ServiceAgent.BizDate(), mainForm.UtcOffset);
				ProcessMessagesGrid.SetDataBinding(dsProcessMessages, "ProcessMessages", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

		private void ProcessMessagesGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "PostDateTime":			
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeShortFormat);
					}
					catch { }
					break;
			
				default:
					break;
			}
		}

		private void ProcessMessagesForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());
			}

			mainForm.processMessagesForm = null;
		}
	}
}