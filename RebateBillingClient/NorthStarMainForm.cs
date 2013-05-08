using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.Remoting;
using System.Text;
using System.Windows.Forms;

using C1.Win.C1Ribbon;
using StockLoan.Common;

namespace NorthStar
{
    public partial class NorthStarMainForm : C1RibbonForm
    {
        public IShortInterest shortInterestAgent = null;
        public ShortInterestReportForm shortInterestReportForm = null;

        public NorthStarMainForm()
        {
            InitializeComponent();

            try
            {
                //Initialize Log information
                Log.Name = Standard.ConfigValue("ApplicationName");
                Log.Level = Standard.ConfigValue("LogLevel");
                Log.FilePath = Standard.ConfigValue("LogFilePath");
                Log.Write("", 1);
                Log.Write("Running version " + Application.ProductVersion + " [NorthStarMainForm.NorthStarMainForm]", Log.SuccessAudit, 1);

                RegistryValue.Name = Standard.ConfigValue("ApplicationName");
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [NorthStarMainForm.NorthStarMainForm]", Log.Error, 1);
            }
        }
        
        private void NorthStarMainForm_Load(object sender, EventArgs e)
        {
            try
            {
                shortInterestAgent = (IShortInterest)RemotingTools.ObjectGet(typeof(IShortInterest));
                if (shortInterestAgent == null)
                {
                    Log.Write("Could not find ShortInterest agent. [NorthStarMainForm.NorthStarMainForm_Load]", 2);
                }
                // if there only have on button in C1Toolbar, the button is disappear after form opened.
                // If we resize window, the button will show up. So we change form size to display 
                // this button.
                this.Size = new Size(this.Size.Width + 1, this.Size.Height);
            }
            catch (Exception ex) 
            {
                Log.Write(ex.Message + "[NorthStarMainForm.NorthStarMainForm_Load]", Log.Error, 1);
            }
        }

        private void ShortsShortInterestReportCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (shortInterestReportForm == null)
                {
                    shortInterestReportForm = new ShortInterestReportForm(this);
                    shortInterestReportForm.MdiParent = this;
                    shortInterestReportForm.Show();

                }
                else
                {
                    shortInterestReportForm.WindowState = FormWindowState.Normal;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + "[NorthStarMainForm.ShortsShortInterestReportCommand_Click]", Log.Error, 1);
            }
        }
    }
}