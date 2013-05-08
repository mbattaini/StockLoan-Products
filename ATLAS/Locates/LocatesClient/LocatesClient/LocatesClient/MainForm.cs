using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Locates;
using StockLoan.Common;

namespace LocatesClient
{
	public partial class MainForm : Form
	{
		public IShortSale ShortSaleAgent = null;
		public IAdmin AdminAgent = null;
		public IService ServiceAgent = null;
		public ITrade TradeAgent = null;

		public LocatesSummaryForm locatesSummaryForm = null;
		public LocatesExportToolForm locatesExportToolForm = null;
		public LocatesInventoryLookupForm locatesInventoryLookupForm = null;
		public LocatesTradingGroupsForm locatesTradingGroupsForm = null;
		public LocatesInputForm locatesInputForm = null;
		public LocatesResearchSummaryForm locatesResearchSummaryForm = null;
		public SecurityMasterForm MainSecMasterForm = null;

		public MainForm()
		{
			InitializeComponent();
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void LocatesSummaryCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			if (locatesSummaryForm == null)
			{
				locatesSummaryForm = new LocatesSummaryForm(this);
				locatesSummaryForm.Show();
			}
			else
			{
				locatesSummaryForm.Activate();
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			try
			{
				AdminAgent = (IAdmin)RemotingTools.ObjectGet(typeof(IAdmin));
				ServiceAgent = (IService)RemotingTools.ObjectGet(typeof(IService));
				ShortSaleAgent = (IShortSale)RemotingTools.ObjectGet(typeof(IShortSale));
				TradeAgent = (ITrade)RemotingTools.ObjectGet(typeof(ITrade));

				MainSecMasterForm = new SecurityMasterForm(this);
				MainSecMasterForm.Show();
			}
			catch (Exception error)
			{
				Alert(this.Name, error.Message);
			}
		}



		private void LocatesExportToolCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			if (locatesExportToolForm == null)
			{
				locatesExportToolForm = new LocatesExportToolForm(this);
				locatesExportToolForm.Show();
			}
			else
			{
				locatesExportToolForm.Activate();
			}
		}

		private void LocatesTradingGroupCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			if (locatesTradingGroupsForm == null)
			{
				locatesTradingGroupsForm = new LocatesTradingGroupsForm(this);
				locatesTradingGroupsForm.Show();
			}
			else
			{
				locatesTradingGroupsForm.Activate();
			}
		}

		public void Alert(string formName, string message)
		{
			try
			{
				MessageForm messageForm = new MessageForm();
				messageForm.Set(formName, message);
			}
			catch { }
		}

		public string SecId
		{
			set
			{
				MainSecMasterForm.SecId = value;
			}

			get
			{
				return MainSecMasterForm.SecId;
			}
		}

		public short UtcOffset
		{
			get
			{
				TimeSpan timeSpan = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
				return (short)(timeSpan.Hours * 60);
			}
		}

		public string UserId
		{
			get
			{
				if (Standard.ConfigValue("UseDomainInUserId", @"true").ToLower().Equals("true"))
				{
					return SystemInformation.UserDomainName.ToUpper() + @"\" + SystemInformation.UserName.ToLower();
				}
				else
				{
					return SystemInformation.UserName.ToLower();
				}
			}
		}

		private void LocatesInputCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			if (locatesInputForm == null)
			{
				locatesInputForm = new LocatesInputForm(this);
				locatesInputForm.Show();
			}
			else
			{
				locatesInputForm.Activate();
			}
		}

		private void LocatesResearchCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			if (locatesResearchSummaryForm == null)
			{
				locatesResearchSummaryForm = new LocatesResearchSummaryForm(this);
				locatesResearchSummaryForm.Show();
			}
			else
			{
				locatesResearchSummaryForm.Activate();
			}
		}

		private void LocatesInventoryLookupCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			if (locatesInventoryLookupForm == null)
			{
				locatesInventoryLookupForm = new LocatesInventoryLookupForm(this);
				locatesInventoryLookupForm.Show();
			}
			else
			{
				locatesInventoryLookupForm.Activate();
			}
		}
	}
}