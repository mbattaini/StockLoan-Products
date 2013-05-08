using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Forms;

using StockLoan.Common;
using StockLoan.NorthStar;

namespace NorthStarClient
{
    public partial class MainForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        public IShortInterest ShortInterestAgent = null;
        public ITrade TradeAgent = null;

        public ShortInterestReportingForm shortInterestReportForm = null;

        public MainForm()
        {
            InitializeComponent();

            try
            {                
                Log.Name = Standard.ConfigValue("ApplicationName");
                Log.Level = Standard.ConfigValue("LogLevel");
                Log.FilePath = Standard.ConfigValue("LogFilePath");
                Log.Write("", 1);
                Log.Write("Running version " + Application.ProductVersion + " [MainForm.MainForm]", 1);

                RegistryValue.Name = Standard.ConfigValue("ApplicationName");
            }
            catch (Exception error)
            {
                Log.Write(error.Message + " [MainForm.MainForm]",  1);
            }
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
          try
          {
            ShortInterestAgent = (IShortInterest)RemotingTools.ObjectGet(typeof(IShortInterest));
            if (ShortInterestAgent == null)
            {
              Log.Write("Could not find ShortInterest agent. [MainForm.MainForm_Load]", 2);

              ShortInterestReportingCommand.Enabled = false;
            }

            TradeAgent = (ITrade)RemotingTools.ObjectGet(typeof(ITrade));
            if (TradeAgent == null)
            {
              Log.Write("Could not find Trade agent. [MainForm.MainForm_Load]", 2);
            }

            SecMaster.MainForm = this;
          }
          catch (Exception error)
          {
            Log.Write(error.Message + "[MainForm.MainForm_Load]", 1);
          }
        }

      private void ShortInterestReportingCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
      {
        try
        {
          if (shortInterestReportForm == null)
          {
            shortInterestReportForm = new ShortInterestReportingForm(this);
            shortInterestReportForm.MdiParent = this;
            shortInterestReportForm.Show();
          }
          else
          {
            shortInterestReportForm.Activate();
          }
        }
        catch (Exception error)
        {
          Log.Write(error.Message + "[MainForm.ShortInterestReportingCommand_Click]", 1);
        }
      }

      public string SecId
      {
        set
        {
          SecMaster.SecId = value;
        }
      }
    }
}