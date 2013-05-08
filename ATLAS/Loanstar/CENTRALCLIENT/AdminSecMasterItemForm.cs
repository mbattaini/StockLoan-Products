using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CentralClient
{
	public partial class AdminSecMasterItemForm : C1.Win.C1Ribbon.C1RibbonForm
	{
        private MainForm mainForm;
        private string secId;
        private DataSet dsSecMasterItem;

		public AdminSecMasterItemForm(MainForm mainForm, string secId)
		{
			InitializeComponent();
            this.secId = secId;
            this.mainForm = mainForm;
		}

        private void AdminSecMasterItemForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
               dsSecMasterItem = mainForm.ServiceAgent.SecMasterLookup(secId);

               
                SecIdTextBox.Text = dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["SecId"].ToString();
               SymbolTextBox.Text = dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["Symbol"].ToString();
               IsinTextBox.Text = dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["Isin"].ToString();
               CusipTextBox.Text = dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["Cusip"].ToString();
               DescriptionTextBox.Text = dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["Description"].ToString();
               CountryCodeTextBox.Text = dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["CountryCode"].ToString();
               CurrencyIsoTextBox.Text = dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["CurrencyCode"].ToString();
               PriceTextBox.Text = dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["Price"].ToString();
            }
            catch
            {
               SecIdTextBox.Text = secId;
               CurrencyIsoTextBox.Text = "USD";
            }

            this.Cursor = Cursors.Default;
        }

        private void SubmitRibbonButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mainForm.ServiceAgent.SecMasterItemSet(secId,
                    DescriptionTextBox.Text,
                    "U",
                    "",
                   CountryCodeTextBox.Text,
                   CurrencyIsoTextBox.Text,
                   "",
                   "",
                   "",
                   "",
                   SymbolTextBox.Text,
                   IsinTextBox.Text,
                   CusipTextBox.Text,
                   "");

                StatusTextBox.Text = "Successfully Updated! Security ID: " + secId + ".";
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
                StatusTextBox.Text = error.Message;
            }



            this.Cursor = Cursors.Default;
        }
	}
}