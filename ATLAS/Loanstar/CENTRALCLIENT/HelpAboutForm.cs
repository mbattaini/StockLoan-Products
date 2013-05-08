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
    public partial class HelpAboutForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private MainForm mainForm;

        public HelpAboutForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void HelpAboutForm_Load(object sender, EventArgs e)
        {
            UserIdLabel.Text = mainForm.UserId;
            VersionIdLabel.Text = Application.ProductVersion;
        }

        private void HelpAboutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.helpAboutForm = null;
        }
    }
}
