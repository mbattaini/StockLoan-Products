using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockLoanPlatformTester
{
    public partial class SetTesterForm : Form
    {
        public string testEnv
        {
            get
            {
                return ConfigurationManager.AppSettings["WCFServiceTestEnv"];
            }
        }

        public SetTesterForm()
        {
            InitializeComponent();
            lblItemToTest.Text = "";
            lblItemToTest.Text = "";
        }

        private void SetTesterForm_Load(object sender, EventArgs e)
        {

            //this.ShowDialog();
        }

        public string userPassword { get; set; }

        public string userId { get; set; }

        private void ClearText()
        {
            this.c1TrueDBGrid1.ClearFields();
            lblParam1.Text = "";
            txtParam1.Text = "";
            lblParam1.Visible = true;
            txtParam1.Visible = true;
            lblParam2.Text = "";
            txtParam2.Text = "";
            lblParam2.Visible = true;
            txtParam2.Visible = true;
            lblParam3.Text = "";
            txtParam3.Text = "";
            lblParam3.Visible = true;
            txtParam3.Visible = true;
            lblParam4.Text = "";
            txtParam4.Text = "";
            lblParam4.Visible = true;
            txtParam4.Visible = true;
            lblParam5.Text = "";
            txtParam5.Text = "";
            lblParam5.Visible = true;
            txtParam5.Visible = true;
            lblParam6.Text = "";
            txtParam6.Text = "";
            lblParam6.Visible = true;
            txtParam6.Visible = true;
            lblParam7.Text = "";
            txtParam7.Text = "";
            lblParam7.Visible = true;
            txtParam7.Visible = true;
            lblParam8.Text = "";
            txtParam8.Text = "";
            lblParam8.Visible = true;
            txtParam8.Visible = true;
            lblParam9.Text = "";
            txtParam9.Text = "";
            lblParam9.Visible = true;
            txtParam9.Visible = true;
            lblParam10.Text = "";
            txtParam10.Text = "";
            lblParam10.Visible = true;
            txtParam10.Visible = true;
            lblParam11.Text = "";
            txtParam11.Text = "";
            lblParam11.Visible = true;
            txtParam11.Visible = true;
            lblParam12.Text = "";
            txtParam12.Text = "";
            lblParam12.Visible = true;
            txtParam12.Visible = true;
            lblParam13.Text = "";
            txtParam13.Text = "";
            lblParam13.Visible = true;
            txtParam13.Visible = true;
            lblParam14.Text = "";
            txtParam14.Text = "";
            lblParam14.Visible = true;
            txtParam14.Visible = true;
            lblParam15.Text = "";
            txtParam15.Text = "";
            lblParam15.Visible = true;
            txtParam15.Visible = true;
            lblParam16.Text = "";
            txtParam16.Text = "";
            lblParam16.Visible = true;
            txtParam16.Visible = true;
            lblParam17.Text = "";
            txtParam17.Text = "";
            lblParam17.Visible = true;
            txtParam17.Visible = true;
            lblParam18.Text = "";
            txtParam18.Text = "";
            lblParam18.Visible = true;
            txtParam18.Visible = true;
            lblParam19.Text = "";
            txtParam19.Text = "";
            lblParam19.Visible = true;
            txtParam19.Visible = true;
            lblParam20.Text = "";
            txtParam20.Text = "";
            lblParam20.Visible = true;
            txtParam20.Visible = true;
            lblParam21.Text = "";
            txtParam21.Text = "";
            lblParam21.Visible = true;
            txtParam21.Visible = true;
            lblParam22.Text = "";
            txtParam22.Text = "";
            lblParam22.Visible = true;
            txtParam22.Visible = true;
            lblParam23.Text = "";
            txtParam23.Text = "";
            lblParam23.Visible = true;
            txtParam23.Visible = true;
        }
        private void countriesGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Country Code";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Enter Function Name(path)";
            lblParam4.Visible = false;
            txtParam4.Visible = false;
            lblParam5.Visible = false;
            txtParam5.Visible = false;
            lblParam6.Visible = false;
            txtParam6.Visible = false;
            lblParam7.Visible = false;
            txtParam7.Visible = false;
            lblParam8.Visible = false;
            txtParam8.Visible = false;
            lblParam9.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "CountriesGet";
        }

        private void toolStripCountryCConversionsGet_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = "Enter Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = "Enter Function Name(Path)";
            lblParam3.Visible = false;
            lblParam4.Visible = false;
            lblParam5.Visible = false;
            lblParam6.Visible = false;
            lblParam7.Visible = false;
            lblParam8.Visible = false;
            lblParam9.Visible = false;
            lblParam10.Visible = false;
            lblParam11.Visible = false;
            lblParam12.Visible = false;
            lblParam13.Visible = false;
            lblParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam1.Visible = true;
            txtParam2.Visible = true;
            txtParam3.Visible = false;
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "CountryCodeISOConversionsGet";
        }

        private void countrySetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Country Code";
            lblParam2.Text = " Country Name";
            lblParam3.Text = " Settle Days";
            lblParam4.Text = " IsActive (Yes/No)";
            lblParam5.Text = " Book Group";
            txtParam5.Text = "PFSI";
            lblParam6.Text = " Functon Name(Path)";
            lblParam7.Visible = false;
            lblParam8.Visible = false;
            lblParam9.Visible = false;
            lblParam10.Visible = false;
            lblParam11.Visible = false;
            lblParam12.Visible = false;
            lblParam13.Visible = false;
            lblParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam1.Visible = true;
            txtParam2.Visible = true;
            txtParam3.Visible = true;
            txtParam4.Visible = true;
            txtParam5.Visible = true;
            txtParam6.Visible = true;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "CountrySet";
        }

        private void currenciesGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Currency ISO";
            lblParam2.Text = " Book Group ";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Function Name(path)";
            userId = lblUserId.Text;
            userPassword = lblPassword.Text;
            txtParam4.Visible = false;
            lblParam4.Visible = false;
            lblParam5.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            lblParam7.Visible = false;
            txtParam7.Visible = false;
            lblParam8.Visible = false;
            txtParam8.Visible = false;
            lblParam9.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "CurrenciesGet";
        }

        private void toolStripCurrencyConversionsGet_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Currency ISO - From";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Function Name(path)";
            lblParam4.Visible = false;
            txtParam4.Visible = false;
            lblParam5.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            lblParam7.Visible = false;
            txtParam7.Visible = false;
            lblParam8.Visible = false;
            txtParam8.Visible = false;
            lblParam9.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "CurrencyConversionsGet";
        }

        private void currencyConversionSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Currency ISO - From";
            lblParam2.Text = " Currency ISO - To";
            lblParam3.Text = " Currency Convert Rate";
            lblParam4.Text = " Book Group";
            txtParam4.Text = "PFSI";
            lblParam5.Text = " Function Path(Name)";
            txtParam6.Visible = false;
            lblParam7.Visible = false;
            txtParam7.Visible = false;
            lblParam8.Visible = false;
            txtParam8.Visible = false;
            lblParam9.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "CurrencyConversionSet";

        }

        private void currencySetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Currency ISO";
            lblParam2.Text = " Currency ";
            lblParam3.Text = " Is Active (true/false)";
            lblParam4.Text = " Book Group";
            txtParam4.Text = "PFSI";
            lblParam5.Text = " Function Name(path)";
            lblParam6.Visible = false;
            txtParam7.Visible = false;
            lblParam7.Visible = false;
            txtParam7.Visible = false;
            lblParam8.Visible = false;
            txtParam8.Visible = false;
            lblParam9.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "CurrencySet";
        }

        private void deliveryTypesGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Function Path(Name)";
            txtParam3.Visible = false;
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            lblParam7.Visible = false;
            txtParam7.Visible = false;
            lblParam8.Visible = false;
            txtParam8.Visible = false;
            lblParam9.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "DeliveryTypesGet";
        }
        private void bookDataLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Book";
            lblParam3.Text = " Function Path";
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            lblParam7.Visible = false;
            txtParam7.Visible = false;
            lblParam8.Visible = false;
            txtParam8.Visible = false;
            lblParam9.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "BooksGet";
        }

        private void bookClearingInstructionsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Book";
            lblParam3.Text = " Function Path";
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            lblParam7.Visible = false;
            txtParam7.Visible = false;
            lblParam8.Visible = false;
            txtParam8.Visible = false;
            lblParam9.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "BookClearingInstructionsGet";
        }

        private void bookClearingInstructionsSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Book";
            lblParam3.Text = " Country Code";
            lblParam4.Text = " Currency ISO";
            lblParam5.Text = " Div Rate";
            lblParam6.Text = " Cash Instructions";
            lblParam7.Text = " Security Instructions";
            lblParam8.Text = " Is Active (Yes or No)";
            lblParam9.Text = " Function Path";
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "BookClearingInstructionSet";
        }

        private void bookContactsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Book ";
            lblParam3.Text = " UTC Offset";
            lblParam4.Text = " Function Path";
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            lblParam7.Visible = false;
            txtParam7.Visible = false;
            lblParam8.Visible = false;
            txtParam8.Visible = false;
            lblParam9.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "BookContactsGet";
        }

        private void bookContactsSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Book";
            lblParam3.Text = " First Name";
            lblParam4.Text = " LastName";
            lblParam5.Text = " Function";
            lblParam6.Text = " Phone Number";
            lblParam7.Text = " Fax Number";
            lblParam8.Text = " Comment";
            lblParam9.Text = " Is Active (Yes or No)";
            lblParam10.Text = " Function Path";
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "BookContactSet";
        }

        private void bookCreditLimitSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Book Parent";
            lblParam4.Text = " Book";
            lblParam5.Text = " Borrow Limit Amount";
            lblParam6.Text = " Loan Limit Amount";
            lblParam7.Text = " Function Path";
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "BookCreditLimitSet";
        }

        private void bookCreditLimitsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Book Parent";
            lblParam4.Text = " Book ";
            lblParam5.Text = " UTC Offset";
            lblParam6.Text = " Function Path";
            lblParam7.Visible = false;
            txtParam7.Visible = false;
            lblParam8.Visible = false;
            txtParam8.Visible = false;
            lblParam9.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "BookCreditLimitsGet";
        }

        private void bookFundSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Book";
            lblParam3.Text = " Currency ISO";
            lblParam4.Text = " Fund";
            lblParam5.Text = " Is Active (Yes/No)";
            lblParam6.Text = " Function Path";
            lblParam7.Visible = false;
            txtParam7.Visible = false;
            lblParam8.Visible = false;
            txtParam8.Visible = false;
            lblParam9.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "BookFundSet";
        }

        private void bookFundsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Book";
            lblParam3.Text = " Currency ISO";
            lblParam4.Text = " Function Path";
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            lblParam7.Visible = false;
            txtParam7.Visible = false;
            lblParam8.Visible = false;
            txtParam8.Visible = false;
            lblParam9.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "BookFundsGet";
        }

        private void bookGroupGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Biz Date";
            lblParam3.Text = " Function Path";
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            lblParam7.Visible = false;
            txtParam7.Visible = false;
            lblParam8.Visible = false;
            txtParam8.Visible = false;
            lblParam9.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "BookGroupsGet";
        }

        private void bookGroupRollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Biz Date Prior";
            lblParam3.Text = " Book Group";
            txtParam3.Text = "PFSI";
            lblParam4.Text = " Function Path";
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "BookGroupRoll";
        }

        private void bookSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Book Parent";
            lblParam3.Text = " Book ";
            lblParam4.Text = " Book Name";
            lblParam5.Text = " Address Line 1";
            lblParam6.Text = " Address Line 2";
            lblParam7.Text = " Address Line 3";
            lblParam8.Text = " Phone Number";
            lblParam9.Text = " Fax Number";
            lblParam10.Text = " Margin Borrow";
            lblParam11.Text = " Margin Loan";
            lblParam12.Text = " Mark Round House";
            lblParam13.Text = " Round Institution";
            lblParam14.Text = " Rate Stock Borrow";
            lblParam15.Text = " Rate Stock Loan";
            lblParam16.Text = " Rate Bond Borrow";
            lblParam17.Text = " Rate Bond Loan";
            lblParam18.Text = " Country Code";
            lblParam19.Text = " Fund Default";
            lblParam20.Text = " Price Min";
            lblParam21.Text = " Amount Min";
            lblParam22.Text = " Is Active (Yes/No)";
            lblParam23.Text = " Function Path";

            lblItemToTest.Text = "BookSet";
        }

        private void bookGroupSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Book Name";
            lblParam3.Text = " Time Zone ID";
            lblParam4.Text = " Biz Date";
            lblParam5.Text = " Biz Date Contract";
            lblParam6.Text = " Biz Date Bank";
            lblParam7.Text = " Biz Date Exchange";
            lblParam8.Text = " Biz Date Prior";
            lblParam9.Text = " Biz Date Prior Bank";
            lblParam10.Text = " Biz Date Prior Exchange";
            lblParam11.Text = " Biz Date Next";
            lblParam12.Text = " Biz Date Next Bank";
            lblParam13.Text = " Biz Date Next Exchange";
            lblParam14.Text = " Use Weekends (Yes/No)";
            lblParam15.Text = " Settlement Type";
            lblParam16.Text = " Function Path";
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "BookGroupSet";
        }

        private void contractBillingGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Function Path";
            txtParam3.Text = "";
            lblParam4.Text = "";
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "ContractBillingsGet";
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Function Path";
            txtParam3.Text = "";
            lblParam4.Text = "";
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "ContractDetailsGet";
        }
        private void contractRateChangeAsOfSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Start Date";
            lblParam2.Text = " End Date";
            lblParam3.Text = " Book Group";
            txtParam3.Text = "PFSI";
            lblParam4.Text = " Book";
            lblParam5.Text = " Contract ID";
            lblParam6.Text = " Old Rate";
            lblParam7.Text = " New Rate";
            lblParam8.Text = " Function Path";

            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "ContractRateChangeAsOfSet";
        }
        private void contractSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Contract ID";
            lblParam4.Text = " Contract Type";
            lblParam5.Text = " Book - Optional";
            lblParam6.Text = " Sec ID - Optional";
            lblParam7.Text = " Amount";
            lblParam8.Text = " Collateral Code";
            lblParam9.Text = " Pool Code";
            lblParam10.Text = " Div Callable (Yes/No)";
            lblParam11.Text = " Income Tracked(Yes/No)";
            lblParam12.Text = " Margin Code";
            lblParam13.Text = " Currency ISO";
            lblParam14.Text = " Security Depot";
            lblParam15.Text = " Cash Depot";
            lblParam16.Text = " Other Book";
            lblParam17.Text = " Return Data (Yes/No)";
            lblParam18.Text = " Is Incremental (Yes/No)";
            lblParam19.Text = " Is Active (Yes/No)";
            lblParam20.Text = " Function Path";
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "ContractSet";
        }
        private void contractsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Contract ID";
            lblParam4.Text = " Contract Type";
            lblParam5.Text = " Function Path";
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "ContractsGet";
        }
        private void contractSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Start Date";
            lblParam3.Text = " Stop Date";
            lblParam4.Text = " Book Group";
            lblParam5.Text = " Book";
            lblParam6.Text = " Contract ID";
            lblParam7.Text = " Sec ID";
            lblParam8.Text = " Amount";
            lblParam9.Text = " Logic Id";
            lblParam10.Text = " Function Path";            
            txtParam10.Text = "PFSI";
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "ContractsResearchGet";
        }
        private void dealSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Deal ID";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Deal Type";
            lblParam4.Text = " Book";
            lblParam5.Text = " Book Contact";
            lblParam6.Text = " Contract ID";
            lblParam7.Text = " Div Is Callable(Yes/No)";
            lblParam8.Text = " Income Is Tracked(Yes/No)";
            lblParam9.Text = " Security Depot";
            lblParam10.Text = " Cash Depot";
            lblParam11.Text = " Deal Status(C or D)";
            lblParam12.Text = " Is Active(Yes/No)";
            lblParam13.Text = " Return Data(Yes/No)";
            lblParam14.Text = " Function Path";
            txtParam14.Text = "AdminBooks";
            lblParam15.Text = " Security ID - REQ";

            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "DealSet";
        }
        private void dealGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Deal ID";
            lblParam3.Text = " Deal ID Prefix";
            lblParam4.Text = " Is Active(Yes/No)";
            lblParam5.Text = " UTC Offset";
            txtParam5.Text = "1";
            lblParam6.Text = " Book Group";
            txtParam6.Text = "PFSI";
            lblParam7.Text = " Function Path";
            txtParam7.Text = "AdminBooks";
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "DealsGet";
        }
        private void dealValidateDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Deal ID";
            lblParam2.Text = " Biz Date";
            lblParam3.Text = " Book Group";
            txtParam3.Text = "PFSI";
            lblParam4.Text = " Function Path";
            txtParam4.Text = "AdminBooks";
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;

            lblItemToTest.Text = "DealToContract";
        }
        private void fundsGetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Function Path";
            txtParam2.Text = "AdminBooks";
            txtParam3.Visible = false;
            txtParam3.Visible = false;
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "FundsGet";
        }
        private void fundingRatesResearchGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Start Date";
            lblParam2.Text = " Stop Date";
            lblParam3.Text = " Fund";
            lblParam4.Text = " Utc Offset";
            lblParam5.Text = " Book Group";
            txtParam5.Text = "PFSI";
            lblParam6.Text = " Function Path";
            txtParam6.Text = "AdminBooks";
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "FundingRateResearchGet";
        }
        private void fundingRateSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Fund";
            lblParam3.Text = " Funding Rate";
            lblParam4.Text = " Book Group";
            txtParam4.Text = "PFSI";
            lblParam5.Text = " Function Path";
            txtParam5.Text = "AdminBooks";
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "FundingRateSet";
        }
        private void fundingRatesGetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " UTC OffSet";
            lblParam3.Text = " Book Group";
            txtParam3.Text = "PFSI";
            lblParam4.Text = " Function Path";
            txtParam4.Text = "AdminBooks";
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "FundingRatesGet";
        }
        private void fundingRatesRollToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Biz Date Prior";
            lblParam3.Text = " Book Group";
            txtParam3.Text = "PFSI";
            lblParam4.Text = " Function Path";
            txtParam4.Text = "AdminBooks";
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "FundingRatesRoll";
        }
        private void holidaySetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Holiday Date";
            lblParam3.Text = " Country Code";
            lblParam4.Text = " Description";
            lblParam5.Text = " Is Bank Holiday(Yes/No)"; 
            lblParam6.Text = " Is Exchange Holiday(Yes/No)";
            lblParam7.Text = " Is Active (Yes/No)";
            lblParam8.Text = " Function Path";
            txtParam8.Text = "AdminBooks";
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "HolidaySet";
        }
        private void holidaysGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group to Check";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Country Code";
            lblParam3.Text = " Description";
            lblParam4.Text = " Utc Offset (req)";
            lblParam5.Text = " Operator Book Group";
            txtParam5.Text = "PFSI";
            lblParam6.Text = " Function Path";
            txtParam6.Text = "AdminBooks";
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "HolidaysListGet";
        }
        private void isBankHolidayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group Check";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Country Code";
            lblParam3.Text = " Holiday Date";
            lblParam4.Text = " Utc Offset (Req)";
            lblParam5.Text = " Operator Book Group";
            txtParam5.Text = "PFSI";
            lblParam6.Text = " Function Path";
            txtParam6.Text = "AdminBooks";

            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "IsBankHoliday";
        }
        private void isExchangeHolidayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group Check";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Country Code (Optional)";
            lblParam3.Text = " Holiday Date (Req)";
            lblParam4.Text = " Utc Offset (Req)";
            lblParam5.Text = " Operator Book Group";
            txtParam5.Text = "PFSI";
            lblParam6.Text = " Function Path";
            txtParam6.Text = "AdminBooks";
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "IsExchangeHoliday";
        }
        private void keyValuesSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Key ID";
            lblParam2.Text = " Key Value";
            lblParam3.Text = " Book Group";
            txtParam3.Text = "PFSI";
            lblParam4.Text = " Function Path";
            txtParam4.Text = "AdminBooks";
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "KeyValueSet";
        }
        private void keyValuesGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Key ID";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Function Path";
            txtParam3.Text = "AdminBooks";
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "KeyValuesGet";
        }
        private void keyValuesListGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
                ClearText();
                lblParam1.Text = " Book Group";
                txtParam1.Text = "PFSI";
                lblParam2.Text = " Function Path";
                txtParam2.Text = "AdminBooks";
                txtParam3.Visible = false;
                txtParam4.Visible = false;
                txtParam5.Visible = false;
                txtParam6.Visible = false;
                txtParam7.Visible = false;
                txtParam8.Visible = false;
                txtParam9.Visible = false;
                lblParam10.Visible = false;
                txtParam10.Visible = false;
                lblParam11.Visible = false;
                txtParam11.Visible = false;
                lblParam12.Visible = false;
                txtParam12.Visible = false;
                lblParam13.Visible = false;
                txtParam13.Visible = false;
                lblParam14.Visible = false;
                txtParam14.Visible = false;
                lblParam15.Visible = false;
                txtParam15.Visible = false;
                lblParam16.Visible = false;
                txtParam16.Visible = false;
                lblParam17.Visible = false;
                txtParam17.Visible = false;
                lblParam18.Visible = false;
                txtParam18.Visible = false;
                lblParam19.Visible = false;
                txtParam19.Visible = false;
                lblParam20.Visible = false;
                txtParam20.Visible = false;
                lblParam21.Visible = false;
                txtParam21.Visible = false;
                lblParam22.Visible = false;
                txtParam22.Visible = false;
                lblParam23.Visible = false;
                txtParam23.Visible = false;
                lblItemToTest.Text = "KeyValuesListGet";
            }
        
        private void logicOperatorGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Book Group";
            txtParam1.Text = "PFSI";
            lblParam2.Text = " Function Path";
            txtParam2.Text = "AdminBooks";
            txtParam3.Visible = false;
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "LogicOperatorsGet";
        }
        private void settlementSystemProcessSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Function Path";
            txtParam3.Text = "AdminBooks";
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            lblParam10.Visible = false;
            txtParam10.Visible = false;
            lblParam11.Visible = false;
            txtParam11.Visible = false;
            lblParam12.Visible = false;
            txtParam12.Visible = false;
            lblParam13.Visible = false;
            txtParam13.Visible = false;
            lblParam14.Visible = false;
            txtParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "SettlementSystemProcessSet";
        }
        private void inventoriesGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Desk";
            lblParam2.Text = " Sec ID";
            lblParam3.Text = " Utc OffSet";
            lblParam4.Text = " Age Day Count (#)";
            lblParam5.Text = "Enter Book Group";
            txtParam5.Text = "PFSI";
            lblParam6.Text = "Enter Function Name(Path)";
            lblParam7.Visible = false;
            lblParam8.Visible = false;
            lblParam9.Visible = false;
            lblParam10.Visible = false;
            lblParam11.Visible = false;
            lblParam12.Visible = false;
            lblParam13.Visible = false;
            lblParam14.Visible = false;
            lblParam15.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            lblParam16.Visible = false;
            txtParam16.Visible = false;
            lblParam17.Visible = false;
            txtParam17.Visible = false;
            lblParam18.Visible = false;
            txtParam18.Visible = false;
            lblParam19.Visible = false;
            txtParam19.Visible = false;
            lblParam20.Visible = false;
            txtParam20.Visible = false;
            lblParam21.Visible = false;
            txtParam21.Visible = false;
            lblParam22.Visible = false;
            txtParam22.Visible = false;
            lblParam23.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "InventoriesGet";
        }
        private void inventoryControlsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Function Name(Path)";
            txtParam3.Text = "AdminBooks";
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "InventoryControlGet";
        }
        private void inventorySetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Desk";
            lblParam3.Text = " Sec ID";
            lblParam4.Text = " Rate";
            lblParam5.Text = " Mode Code";
            lblParam6.Text = " Quantity";
            lblParam7.Text = " Increment Currenty Quantity";
            lblParam8.Text = " Book Group";
            txtParam8.Text = "PFSI";
            lblParam9.Text = " Function Name(Path)";
            txtParam9.Text = "AdminBooks";
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "InventorySet";
        }
        private void marksGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Mark ID";
            lblParam2.Text = " Biz Date";
            lblParam3.Text = " Contract ID";
            lblParam4.Text = " UTC OffSet";
            lblParam5.Text = " Book Group";
            txtParam5.Text = "PFSI";
            lblParam6.Text = " Function Name(Path)";
            txtParam6.Text = "AdminBooks";
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "MarksGet";
        }
        private void markSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Mark ID";
            lblParam2.Text = " Biz Date";
            lblParam3.Text = " Book";
            lblParam4.Text = " Contract ID";
            lblParam5.Text = " Contract Type";
            lblParam6.Text = " Sec ID";
            lblParam7.Text = " Amount";
            lblParam8.Text = " Open Date";
            lblParam9.Text = " Settle Date";
            lblParam10.Text = " Delivery Code";
            lblParam11.Text = " Is Active(Yes/No)";
            lblParam12.Text = " Book Group";
            txtParam12.Text = "PFSI";
            lblParam13.Text = " Function Name(Path)";
            txtParam13.Text = "AdminBooks";
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "MarkSet";
        }
        private void markRetroSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Trade Date";
            lblParam2.Text = " Settle Date";
            lblParam3.Text = " Book";
            lblParam4.Text = " Contract Id";
            lblParam5.Text = " Contract Type";
            lblParam6.Text = " Price";
            lblParam7.Text = " Mark Id";
            lblParam8.Text = " Delivery Code";
            lblParam9.Text = " Book Group";
            txtParam9.Text = "PFSI";
            lblParam10.Text = " Function Name(Path)";
            txtParam10.Text = "AdminBooks";
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "MarkAsOfSet";
        }
        private void markIsExistGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Book";
            lblParam3.Text = " Contract ID";
            lblParam4.Text = " Contract Type";
            lblParam5.Text = " Sec ID";
            lblParam6.Text = " Amount";
            lblParam7.Text = " Book Group";
            txtParam7.Text = "PFSI";
            lblParam8.Text = " Function Name(Path)";
            txtParam8.Text = "AdminBooks";
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "MarkIsExistGet";
        }
        private void markSummmaryByCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Mark ID";
            lblParam2.Text = " Biz Date";
            lblParam3.Text = " Contract ID";
            lblParam4.Text = " Book Group";
            txtParam4.Text = "PFSI";
            lblParam5.Text = " Function Name(Path)";
            txtParam5.Text = "AdminBooks";
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "MarksSummaryByCashGet";
        }
        private void markSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Mark ID";
            lblParam2.Text = " Biz Date";
            lblParam3.Text = " Biz Date Format";
            lblParam4.Text = " Contract ID";
            lblParam5.Text = " Book Group";
            txtParam5.Text = "PFSI";
            lblParam6.Text = " Function Name(Path)";
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "MarksSummaryGet";
        }
        private void boxPositionGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Sec ID";
            lblParam3.Text = " Book Group";
            txtParam3.Text = "PFSI";
            lblParam4.Text = " Function Name(Path)";
            txtParam4.Text = "AdminBooks";
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "BoxPositionGet";
        }
        private void recallSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Recall ID";
            lblParam2.Text = " BizDate";
            lblParam3.Text = " Contract ID";
            lblParam4.Text = " Contract Type";
            lblParam5.Text = " Book";
            lblParam6.Text = " Sec ID";
            lblParam7.Text = " Quantity";
            lblParam8.Text = " Open DateTime";
            lblParam9.Text = " Reason Code";
            lblParam10.Text = " Status";
            lblParam11.Text = " Sequence Number";
            lblParam12.Text = " Move To Date";
            lblParam13.Text = " Buy In Date";
            lblParam14.Text = " Comment";
            lblParam15.Text = " Is Active(Yes/No)";
            lblParam16.Text = " Book Group";
            txtParam16.Text = "PFSI";
            lblParam17.Text = " Function Name(Path)";
            txtParam17.Text = "AdminBooks";
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "RecallSet";
        }
        private void recallsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " BizDate";
            lblParam2.Text = " Recall ID";
            lblParam3.Text = " UTC OffSet";
            lblParam4.Text = " Book Group";
            txtParam4.Text = "PFSI";
            lblParam5.Text = " Function Name(Path)";
            txtParam5.Text = "AdminBooks";
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "RecallsGet";
        }
        private void returnAsOfSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Trade Date";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Book";
            lblParam4.Text = " Contract ID";
            lblParam5.Text = " Contract Type";
            lblParam6.Text = " Return ID";
            lblParam7.Text = " Quantity";
            lblParam8.Text = " Settle Date";
            lblParam9.Text = " Function Name(Path)";
            txtParam9.Text = "AdminBooks";
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "ReturnAsOfSet";
        }
        private void returnSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Return ID";
            lblParam2.Text = " Biz Date";
            lblParam3.Text = " Book Group";
            txtParam3.Text = "PFSI";
            lblParam4.Text = " Book";
            lblParam5.Text = " Contract ID";
            lblParam6.Text = " Contract Type";
            lblParam7.Text = " Quantity";
            lblParam8.Text = " Settlement Date Projected";
            lblParam9.Text = " Settlemenht Date Actual";
            lblParam10.Text = " Is Active(Yes/No)";
            lblParam11.Text = " Function Name(Path)";
            txtParam11.Text = "AdminBooks";
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "ReturnSet";
        }
        private void retrunsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Return ID";
            lblParam2.Text = " Biz Date";
            lblParam3.Text = " Book Group";
            txtParam3.Text = "PFSI";
            lblParam4.Text = " Contract ID";
            lblParam5.Text = " UTC OffSet";
            lblParam6.Text = " Function Name(Path)";
            txtParam6.Text = "AdminBooks";
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "ReturnsGet";
        }
        private void returnsSummaryByCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Return ID";
            lblParam2.Text = " Biz Date";
            lblParam3.Text = " Book Group";
            txtParam3.Text = "PFSI";
            lblParam4.Text = " Contract ID";
            lblParam5.Text = " UTC OffSet";
            lblParam6.Text = " Function Name(Path)";
            txtParam6.Text = "AdminBooks";
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "ReturnsSummaryByCashGet";
        }
        private void priceSetToolStripMenuItem_Click_1(object sender, EventArgs e)
        { 
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Sec ID";
            lblParam3.Text = " Country Code";
            lblParam4.Text = " Currency ISO";
            lblParam5.Text = " Price";
            lblParam6.Text = " Price Date";
            lblParam7.Text = " Book Group";
            txtParam7.Text = "PFSI";
            lblParam8.Text = " Function Name(Path)";
            txtParam8.Text = "AdminBooks";
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "PriceSet";
        }
        private void priceGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Biz Date";
            lblParam2.Text = " Sec ID";
            lblParam3.Text = " Currency ISO";
            lblParam4.Text = " Book Group";
            txtParam4.Text = "PFSI";
            lblParam5.Text = " Function Name(Path)";
            txtParam5.Text = "AdminBooks";
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "PricesGet";
        }
        private void secIdAliasSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Sec ID";
            lblParam2.Text = " Sec ID Type Index";
            lblParam3.Text = " Sec ID Alias";
            lblParam4.Text = " Book Group";
            txtParam4.Text = "PFSI";
            lblParam5.Text = " Function Name(Path)";
            txtParam5.Text = "AdminBooks";
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "SecIDAliasSet";
        }
        private void secMasterGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Sec ID";
            lblParam2.Text = " Country Code";
            lblParam3.Text = " Currency ISO";
            lblParam4.Text = " Book Group";
            txtParam4.Text = "PFSI";
            lblParam5.Text = " Lookup Criteria";
            lblParam6.Text = " Function Name(Path)";
            txtParam6.Text = "AdminBooks";
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "SecMasterGet";
        }
        private void secMasterSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Sec ID";
            lblParam2.Text = " Description";
            lblParam3.Text = " Base Type";
            lblParam4.Text = " Class Group";
            lblParam5.Text = " Country Code";
            lblParam6.Text = " Currency ISO";
            lblParam7.Text = " Accrued Interest";
            lblParam8.Text = " Record Date Cash";
            lblParam9.Text = " Divident Rate";
            lblParam10.Text = " Sec ID Group";
            lblParam11.Text = " Symbol";
            lblParam12.Text = " ISIN";
            lblParam13.Text = " Cusip";
            lblParam14.Text = " Price";
            lblParam15.Text = " Price Date";
            lblParam16.Text = " Is Active";
            lblParam17.Text = " Book Group";
            txtParam17.Text = "PFSI";
            lblParam18.Text = " Function Name(Path)";
            txtParam18.Text = "AdminBooks";
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "SecMasterSet";
        }
        private void functionSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " New Function Path";
            lblParam2.Text = " May View(Yes/No)";
            lblParam3.Text = " May Edit(Yes/No)";
            lblParam4.Text = " Book Group";
            txtParam4.Text = "PFSI";
            lblParam5.Text = " Function Name(Path)";
            txtParam5.Text = "AdminRoles";
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "FunctionSet";
        }
        private void functionsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Function Path to Get";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Function Name (Path)";
            txtParam3.Text = "AdminRoles";
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "FunctionsGet";
        }
        private void roleFunctionSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Role Name";
            lblParam2.Text = " New Function Path to Set";
            lblParam3.Text = " May View (Yes/No)";
            lblParam4.Text = " May Edit (Yes/No)";
            lblParam5.Text = " Comment";
            lblParam6.Text = " Delete (Yes/No)";
            lblParam7.Text = " Book Group";
            txtParam7.Text = "PFSI";
            lblParam8.Text = " Function Name(Path)";
            txtParam8.Text = "AdminRoles";
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "RoleFunctionSet";
        }
        private void roleSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Role ID";
            lblParam2.Text = " Role Name";
            lblParam3.Text = " Comment";
            lblParam4.Text = " Delete(Yes/No)";
            lblParam5.Text = " Book Group";
            txtParam5.Text = "PFSI";
            lblParam6.Text = " Function Name(Path)";
            txtParam6.Text = "AdminRoles";
            txtParam7.Visible = false; 
            txtParam8.Visible = false; 
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "RoleSet";
        }

        private void userRolesGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " Role Name";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Function Name(Path)";
            txtParam3.Text = "AdminRoles";
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "RolesGet";
        }
        private void userGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " User ID";
            lblParam2.Text = " Book Group";
            txtParam2.Text = "PFSI";
            lblParam3.Text = " Function Name(Path)";
            txtParam3.Text = "AdminRoles";
            txtParam4.Visible = false;
            txtParam5.Visible = false;
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "UserGet";
        }
        private void roleFunctionsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            ClearText();
            lblParam1.Text = " Role Name";
            lblParam2.Text = " Function Path To Get";
            lblParam3.Text = " Utc OffSet";
            txtParam3.Text = "0";
            lblParam4.Text = " Book Group";
            txtParam4.Text = "PFSI";
            lblParam5.Text = " Function Name(Path)";
            txtParam5.Text = "AdminRoles";
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "RoleFunctionsGet";
        }
        private void userRolesGetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " User Id";
            lblParam2.Text = " Role Name";
            lblParam3.Text = " Utc Offset";
            txtParam3.Text = "0";
            lblParam4.Text = " Book Group";
            txtParam4.Text = "PFSI";
            lblParam5.Text = " Function Name(Path)";
            txtParam5.Text = "AdminRoles";
            txtParam6.Visible = false;
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "UserRolesGet";
        }
        private void userRoleSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " User Id";
            lblParam2.Text = " Role Name";
            lblParam3.Text = " Comment"; 
            lblParam4.Text = " Delete(Yes/No)"; 
            lblParam5.Text = " Book Group";
            txtParam5.Text = "PFSI";
            lblParam6.Text = " Function Name(Path)";
            txtParam6.Text = "AdminRoles";
            txtParam7.Visible = false;
            txtParam8.Visible = false;
            txtParam9.Visible = false;
            txtParam10.Visible = false;
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "UserRolesSet";
        }
        private void userSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearText();
            lblParam1.Text = " User ID to add";
            lblParam2.Text = " User Short Name";
            lblParam3.Text = " Password";
            lblParam4.Text = " Email";
            lblParam5.Text = " Title";
            lblParam6.Text = " Comment";
            lblParam7.Text = " Is Locked(Yes/No)";
            lblParam8.Text = " Is Active(Yes/No)";
            lblParam9.Text = " Book Group";
            txtParam9.Text = "PFSI";
            lblParam10.Text = " Function Name(Path)";
            txtParam10.Text = "AdminRoles";
            txtParam11.Visible = false;
            txtParam12.Visible = false;
            txtParam13.Visible = false;
            txtParam14.Visible = false;
            txtParam15.Visible = false;
            txtParam16.Visible = false;
            txtParam17.Visible = false;
            txtParam18.Visible = false;
            txtParam19.Visible = false;
            txtParam20.Visible = false;
            txtParam21.Visible = false;
            txtParam22.Visible = false;
            txtParam23.Visible = false;
            lblItemToTest.Text = "UserSet";
        }


        
        public void cmdSubmit_Click(object sender, EventArgs e)
        {
            string param1, param2, param3, param4, param5, param6, param7, param8, param9;
            string param10, param11, param12, param13, param14, param15, param16, param17, param18;
            string param19, param20, param21, param22, param23;

            string caseSwitch = lblItemToTest.Text.ToUpper().ToString();

            switch (caseSwitch)
            {
                case "COUNTRIESGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    this.userId = lblUserId.Text.ToString();
                    this.userPassword = lblPassword.Text.ToString();
                    CountriesGet(param1, param2, param3, this.userId, this.userPassword);
                    break;
                case "COUNTRYCODEISOCONVERSIONSGET":
                    this.userId = lblUserId.Text.ToString();
                    param2 = txtParam1.Text.ToString();
                    param3 = txtParam2.Text.ToString();
                    this.userPassword = lblPassword.Text.ToString();

                    CountryCodeIsoConversionsGet(userId, userPassword, param2, param3);
                    break;
                case "COUNTRYSET":
                    param1 = txtParam1.Text.ToString(); //CountryCode
                    param2 = txtParam2.Text.ToString(); //Country
                    param3 = txtParam3.Text.ToString(); //SettleDays
                    param4 = txtParam4.Text.ToString(); //IsActive
                    userId = lblUserId.Text.ToString(); //userID
                    param5 = txtParam5.Text.ToString(); // Book Group
                    param6 = txtParam6.Text.ToString(); // FunctionPath
                    userPassword = lblPassword.Text.ToString(); //userPassword

                    CountrySet(param1, param2, param3, param4, userId, userPassword, param5, param6);
                    break;
                case "CURRENCIESGET":
                    param1 = txtParam1.Text.ToString(); //currencyISO
                    userId = lblUserId.Text.ToString(); // UserId
                    param2 = txtParam2.Text.ToString(); // Book Group
                    param3 = txtParam3.Text.ToString(); // FunctionPath
                    userPassword = lblPassword.Text.ToString();

                    CurrenciesGet(param1, this.userId, this.userPassword, param2, param3);
                    break;
                case "CURRENCYCONVERSIONSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    CurrencyConversionsGet(param1, userId, userPassword, param2, param3);
                    break;
                case "CURRENCYCONVERSIONSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    CurrencyConversionSet(param1, param2, param3, userId, userPassword, param4, param5);
                    break;
                case "CURRENCYSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    CurrencySet(param1, param2, param3, userId, userPassword, param4, param5);
                    break;
                case "DELIVERYTYPESGET":
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    DeliveryTypesGet(userId, userPassword, param1, param2);
                    break;

                case "BOOKCLEARINGINSTRUCTIONSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();

                    BookClearingInstructionSet(param1, param2, param3, param4, param5, param6, param7, userId, param8, userId, userPassword, param9);
                    break;
                case "BOOKCLEARINGINSTRUCTIONSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    BookClearingInstructionsGet(param1, param2, userId, userPassword, param3);
                    break;
                case "BOOKCONTACTSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    param10 = txtParam10.Text.ToString();
                    BookContactSet(param1, param2, param3, param4, param5, param6, param7, param8, userId, param9, userId, userPassword, param10);
                    break;
                case "BOOKCONTACTSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    short utcOffset = short.Parse(param3);
                    BookContactsGet(param1, param2, utcOffset, userId, userPassword, param4);
                    break;
                case "BOOKCREDITLIMITSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    BookCreditLimitSet(param1, param2, param3, param4, param5, param6, userId, userId, userPassword, param7);
                    break;
                case "BOOKCREDITLIMITSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    short utcOffSet = short.Parse(param5);
                    userPassword = lblPassword.Text.ToString();
                    BookCreditLimitsGet(param1, param2, param3, param4, utcOffSet, userId, userPassword, param6);
                    break;
                case "BOOKFUNDSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    BookFundSet(param1, param2, userId, param3, param4, param5, userId, userPassword, param6);
                    break;
                case "BOOKFUNDSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    BookFundsGet(param1, param2, param3, userId, userPassword, param4);
                    break;
                case "BOOKGROUPROLL":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    BookGroupRoll(param1, param2, userId, param3, userPassword, param4);
                    break;
                case "BOOKGROUPSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    param10 = txtParam10.Text.ToString();
                    param11 = txtParam11.Text.ToString();
                    param12 = txtParam12.Text.ToString();
                    param13 = txtParam13.Text.ToString();
                    param14 = txtParam14.Text.ToString();
                    param15 = txtParam15.Text.ToString();
                    param16 = txtParam16.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    BookGroupSet(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13,
                            param14, param15, userId, userPassword, param16);
                    break;
                case "BOOKGROUPSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    BookGroupsGet(param1, param2, userId, userPassword, param3);
                    break;
                case "BOOKSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    BooksGet(param1, param2, userId, userPassword, param3);
                    break;
                case "BOOKSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    param10 = txtParam10.Text.ToString();
                    param11 = txtParam11.Text.ToString();
                    param12 = txtParam12.Text.ToString();
                    param13 = txtParam13.Text.ToString();
                    param14 = txtParam14.Text.ToString();
                    param15 = txtParam15.Text.ToString();
                    param16 = txtParam16.Text.ToString();
                    param17 = txtParam17.Text.ToString();
                    param18 = txtParam18.Text.ToString();
                    param19 = txtParam19.Text.ToString();
                    param20 = txtParam20.Text.ToString();
                    param21 = txtParam21.Text.ToString();
                    param22 = txtParam22.Text.ToString();
                    param23 = txtParam23.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    BookSet(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, 
                            param15, param16, param17, param18, param19, param20, param21, userId, param22, userId, userPassword, param23);
                    break;
                case "CONTRACTBILLINGSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    ContractBillingsGet(param1, param2, userId, userPassword, param3);
                    break;
                case "CONTRACTDETAILSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    ContractDetailsGet(param1, param2, userId, userPassword, param3);
                    break;
                case "CONTRACTRATECHANGEASOFSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    ContractRateChangeAsOfSet(param1, param2, param3, param4, param5, param6, param7, userId, userId, userPassword, param8);
                    break;
                case "CONTRACTSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    param10 = txtParam10.Text.ToString();
                    param11 = txtParam11.Text.ToString();
                    param12 = txtParam12.Text.ToString();
                    param13 = txtParam13.Text.ToString();
                    param14 = txtParam14.Text.ToString();
                    param15 = txtParam15.Text.ToString();
                    param16 = txtParam16.Text.ToString();
                    param17 = txtParam17.Text.ToString();
                    param18 = txtParam18.Text.ToString();
                    param19 = txtParam19.Text.ToString();
                    param20 = txtParam20.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    ContractSet(param1, param2, param3, param4, param5, param6, "", "", param7, "", param8, "", "", "", "", "", "", param9, "", param10, 
                            param11, param12, "", param13, param14, param15, param16, "", "", "", "", "", "", param17, param18, param19, userId, 
                            userPassword, param20);
                    break;
                case "CONTRACTSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    ContractsGet(param1, param2, param3, param4, userId, userPassword, param5);
                    break;
                case "CONTRACTSRESEARCHGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    param10 = txtParam10.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    ContractsResearchGet(param1, param2, param3, param4, param5, param6, param7, param8, param9,userId, userPassword, param10);
                    break;
                case "DEALSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    param10 = txtParam10.Text.ToString();
                    param11 = txtParam11.Text.ToString();
                    param12 = txtParam12.Text.ToString();
                    param13 = txtParam13.Text.ToString();
                    param14 = txtParam14.Text.ToString();
                    param15 = txtParam15.Text.ToString();     //Security ID is out of sequence, but testing showed it is required
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    DealSet(param1, param2, param3, param4, param5, param6, param15, "", "", "", "", "", "", "", "", "", "", param7, param8, 
                            "", "", "", param9, param10, "", "", param11, param12, userId, param13, "", "", "", userId, userPassword, param14);
                    break;
                case "DEALSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    DealsGet(param1, param2, param3, param4, param5, userId, userPassword, param6, param7);
                    break;
                case "DEALTOCONTRACT":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    DealToContract(param1, param2, userId, userPassword, param3, param4);
                    break;
                case "FUNDINGRATERESEARCHGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    FundingRateResearchGet(param1, param2, param3, param4, userId, userPassword, param5, param6);
                    break;
                case "FUNDINGRATESET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    FundingRateSet(param1, param2, param3, userId, userId, userPassword, param4, param5);
                    break;
                case "FUNDINGRATESGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    FundingRatesGet(param1, param2, userId, userPassword, param3, param4);
                    break;
                case "FUNDINGRATESROLL":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    FundingRatesRoll(param1, param2, userId, userPassword, param3, param4);
                    break;
                case "FUNDSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    FundsGet(userId, userPassword, param1, param2);
                    break;
                case "HOLIDAYSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    HolidaySet(param1, param2, param3, param4, param5, param6, userId, param7, userId, userPassword, param8);
                    break;
                case "HOLIDAYSLISTGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    HolidaysGet(param1, param2, param3, param4, userId, userPassword, param5, param6);
                    break;
                case "ISBANKHOLIDAY":
                    param1 = txtParam1.Text.ToString();  //BookGroup to check
                    param2 = txtParam2.Text.ToString(); // Country code
                    param3 = txtParam3.Text.ToString(); // holiday date
                    param4 = txtParam4.Text.ToString(); // UtcOffset
                    param5 = txtParam5.Text.ToString(); // Operator BookGroup
                    param6 = txtParam6.Text.ToString(); // Operator Function Path
                    userId = lblUserId.Text.ToString(); 
                    userPassword = lblPassword.Text.ToString();
                    IsBankHoliday(param1, param2, param3, param4, userId, userPassword, param5, param6);
                    break;
                case "ISEXCHANGEHOLIDAY":
                    param1 = txtParam1.Text.ToString();  //BookGroup to check
                    param2 = txtParam2.Text.ToString(); // Country code
                    param3 = txtParam3.Text.ToString(); // holiday date
                    param4 = txtParam4.Text.ToString(); // UtcOffset
                    param5 = txtParam5.Text.ToString(); // Operator BookGroup
                    param6 = txtParam6.Text.ToString(); // Operator Function Path
                    userId = lblUserId.Text.ToString(); 
                    userPassword = lblPassword.Text.ToString();
                    IsExchangeHoliday(param1, param2, param3, param4, userId, userPassword, param5, param6);
                    break;
                case "KEYVALUESET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    KeyValueSet(param1, param2, userId, userPassword, param3, param4);
                    break;
                case "KEYVALUESGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    KeyValuesGet(param1, userId, userPassword, param2, param3);
                    break;
	            case "KEYVALUESLISTGET":
	                param1 = txtParam1.Text.ToString();
	                param2 = txtParam2.Text.ToString();
	                userId = lblUserId.Text.ToString();
	                userPassword = lblPassword.Text.ToString();
	                KeyValuesListGet(userId, userPassword, param1, param2);
	                break;
                case "LOGICOPERATORSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    LogicOperatorsGet(userId, userPassword, param1, param2);
                    break;
                case "SETTLEMENTSYSTEMPROCESSSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    SettlementSystemProcessSet(param1, userId, userPassword, param2, param3);
                    break;
                case "INVENTORIESGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    InventoriesGet(param1, param2, param3, param4, userId, userPassword, param5, param6);
                    break;
                case "INVENTORYCONTROLGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    InventoryControlGet(param1, userId, userPassword, param2, param3);
                    break;
                case "INVENTORYSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    InventorySet(param1, param2, param3, param4, param5, param6, param7, userId, userPassword, param8, param9);
                    break;
                case "MARKASOFSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    param10 = txtParam10.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    MarkAsOfSet(param1, param2, param3, param4, param5, param6, param7, param8, userId, userId, userPassword, param9, param10);
                    break;
                case "MARKISEXISTGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    MarkIsExistGet(param1, param2, param3, param4, param5, param6, userId, userPassword, param7, param8);
                    break;
                case "MARKSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    param10 = txtParam10.Text.ToString();
                    param11 = txtParam11.Text.ToString();
                    param12 = txtParam12.Text.ToString();
                    param13 = txtParam13.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    MarkSet(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, userId, param11, userId, userPassword, param12, param13);
                    break;
                case "MARKSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    MarksGet(param1, param2, param3, param5, param4, userId, userPassword, param6);
                    break;
                case "MARKSSUMMARYBYCASHGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    MarksSummaryByCashGet(param1, param2, param3, param4, userId, userPassword, param5);
                    break;
                case "MARKSSUMMARYGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    MarksSummaryGet(param1, param2, param3, param4, param5, userId, userPassword, param6);
                    break;
                case "BOXPOSITIONGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    BoxPositionGet(param1, param2, userId, userPassword, param3, param4);
                    break;
                case "RECALLSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    param10 = txtParam10.Text.ToString();
                    param11 = txtParam11.Text.ToString();
                    param12 = txtParam12.Text.ToString();
                    param13 = txtParam13.Text.ToString();
                    param14 = txtParam14.Text.ToString();
                    param15 = txtParam15.Text.ToString();
                    param16 = txtParam16.Text.ToString();
                    param17 = txtParam17.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    RecallSet(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, userId, param11, param12,
                             param13, param14, param15, userId, userPassword, param16, param17);
                    break;
                case "RECALLSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    RecallsGet(param1, param2, param3, userId, userPassword, param4, param5);
                    break;
                case "RETURNASOFSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    param10 = txtParam10.Text.ToString();
                    param11 = txtParam11.Text.ToString();
                    param12 = txtParam12.Text.ToString();
                    param13 = txtParam13.Text.ToString();
                    param14 = txtParam14.Text.ToString();
                    param15 = txtParam15.Text.ToString();
                    param16 = txtParam16.Text.ToString();
                    param17 = txtParam17.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    ReturnAsOfSet(param1, param2, param3, param4, param5, param6, param7, userId, param8, userId, userPassword, param9);
                    break;
                case "RETURNSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    param10 = txtParam10.Text.ToString();
                    param11 = txtParam11.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    ReturnSet(param1, param2, param3, param4, param5, param6, param7, userId, param8, param9, param10, userId, userPassword, param11);
                    break;
                case "RETURNSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    ReturnsGet(param1, param2, param3, param4, param5, userId, userPassword, param6);
                    break;
                case "RETURNSSUMMARYBYCASHGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    ReturnsSummaryByCashGet(param1, param2, param3, param4, param5, userId, userPassword, param6);
                    break;
                case "PRICESET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    PriceSet(param1, param2, param3, param4, param5, param6, userId, userPassword, param7, param8);
                    break;
                case "PRICESGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    PricesGet(param1, param2, param3, userId, userPassword, param4, param5);
                    break;
                case "SECIDALIASSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    SecIdAliasSet(param1, param2, param3, userId, userPassword, param4, param5);
                    break;
                case "SECMASTERGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    SecMasterGet(param1, param2, param3, param4, param5,userId,userPassword, param6);
                    break;
                case "SECMASTERSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    param10 = txtParam10.Text.ToString();
                    param11 = txtParam11.Text.ToString();
                    param12 = txtParam12.Text.ToString();
                    param13 = txtParam13.Text.ToString();
                    param14 = txtParam14.Text.ToString();
                    param15 = txtParam15.Text.ToString();
                    param16 = txtParam16.Text.ToString();
                    param17 = txtParam17.Text.ToString();
                    param18 = txtParam18.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    SecMasterSet(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14,
                            param15, param16, userId, userPassword, param17, param18);
                    break;

                case "FUNCTIONSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    FunctionSet(param1, param2, param3, param4, param5, userId, userPassword);
                    break;
                case "FUNCTIONSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    FunctionsGet(param1, param2, param3, userId, userPassword);
                    break;
                case "ROLEFUNCTIONSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    RoleFunctionSet(param1, param2, param3, param4, param5, param6, userId, userPassword, param7, param8);
                    break;
                case "ROLESET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    RoleSet(param1, param2, param3, param4, param5, param6, userId, userPassword);       
                    break;
                case "ROLESGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    RolesGet(param1, userId, userPassword, param2, param3);
                    break;
                case "USERGET":
                    param1 = txtParam1.Text.ToString();     
                    param2 = txtParam2.Text.ToString();     
                    param3 = txtParam3.Text.ToString();     
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    UserGet(param1, userId, userPassword, param2, param3);
                    break;
                case "ROLEFUNCTIONSGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    RoleFunctionsGet(param1, param2, param3, userId, userPassword, param4, param5);
                    break;
                case "USERROLESGET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    UserRolesGet(param1, param2, param3, userId, userPassword, param4, param5);
                    break;
                case "USERROLESSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    UserRolesSet(param1, param2, param3, param4, userId, userPassword, param5, param6);
                    break;
                case "USERSET":
                    param1 = txtParam1.Text.ToString();
                    param2 = txtParam2.Text.ToString();
                    param3 = txtParam3.Text.ToString();
                    param4 = txtParam4.Text.ToString();
                    param5 = txtParam5.Text.ToString();
                    param6 = txtParam6.Text.ToString();
                    param7 = txtParam7.Text.ToString();
                    param8 = txtParam8.Text.ToString();
                    param9 = txtParam9.Text.ToString();
                    param10 = txtParam10.Text.ToString();
                    userId = lblUserId.Text.ToString();
                    userPassword = lblPassword.Text.ToString();
                    UserSet(param1, param2, param3, param4, param5, param6, param7, param8, userId, userPassword, param9, param10);
                    break;

                
                default:
                    Console.WriteLine("Default case");
                    break;
            }

        }

        public static DataSet ConvertToDataTable(byte[] e, string tableName)
        {
            DataSet dsTemp = new DataSet();
            try
            {
                var ms = new System.IO.MemoryStream(e);
                dsTemp = new DataSet();
                dsTemp.ReadXml(ms, XmlReadMode.ReadSchema);
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
            return dsTemp;
        }

        public void CountriesGet(string param1, string param2, string param3, string userId, string userPassword)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "Countries";

            if (testEnv.Equals("Local"))
            {
                LocalAdminService.AdminServiceClient adminClient = new LocalAdminService.AdminServiceClient();
                byte[] strTemp = adminClient.CountriesGet(param1, userId, userPassword, param2, param3);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                AdminService.AdminServiceClient adminClient = new AdminService.AdminServiceClient();
                byte[] strTemp = adminClient.CountriesGet(param1, userId, userPassword, param2, param3);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }

            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            string tblName = tableName;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tblName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        
        public void CountryCodeIsoConversionsGet(string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "ISOCountryConversions";
            
            if (testEnv.Equals("Local"))
            {
                LocalAdminService.AdminServiceClient adminClient = new LocalAdminService.AdminServiceClient();
                byte[] strTemp = adminClient.CountryCodeIsoConversionsGet(userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                AdminService.AdminServiceClient adminClient = new AdminService.AdminServiceClient();
                byte[] strTemp = adminClient.CountryCodeIsoConversionsGet(userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();

        }
        public void CountrySet(string countryCode, string country, string settledays, string isActive, string userId, string userPassword, 
                    string bookGroup, string functionPath)
        {
            bool bIsActive = false;
            try
            {
                if (isActive.Equals("Yes"))
                {
                    bIsActive = true;
                }
                DataSet dsTemp = new DataSet();
                if (testEnv.Equals("Local"))
                {
                    LocalAdminService.AdminServiceClient adminClient = new LocalAdminService.AdminServiceClient();
                    adminClient.CountrySet(countryCode, country, settledays, bIsActive, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    AdminService.AdminServiceClient adminClient = new AdminService.AdminServiceClient();
                    adminClient.CountrySet(countryCode, country, settledays, bIsActive, userId, userPassword, bookGroup, functionPath);
                }
                lblTestResults.Text = "Country Set Success";
            }
            catch
            {
                lblTestResults.Text = "Country Set Failed";
                throw;
            }
        }
        public void CurrenciesGet(string currencyIso, string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "Currencies";
            if (testEnv.Equals("Local"))
            {
                LocalAdminService.AdminServiceClient adminClient = new LocalAdminService.AdminServiceClient();
                byte[] strTemp = adminClient.CurrenciesGet(currencyIso, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                AdminService.AdminServiceClient adminClient = new AdminService.AdminServiceClient();
                byte[] strTemp = adminClient.CurrenciesGet(currencyIso, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void CurrencyConversionsGet(string currencyIsoFrom, string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "CurrencyConversions";
            if (testEnv.Equals("Local"))
            {
                LocalAdminService.AdminServiceClient adminClient = new LocalAdminService.AdminServiceClient();
                byte[] strTemp = adminClient.CurrencyConversionsGet(currencyIsoFrom, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                AdminService.AdminServiceClient adminClient = new AdminService.AdminServiceClient();
                byte[] strTemp = adminClient.CurrencyConversionsGet(currencyIsoFrom, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void CurrencyConversionSet(string isoFrom, string isoTo, string conversionRate, string userId, string userPassword, 
                string bookGroup, string functionPath)
        {
            try
            {
                bool results = false;
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalAdminService.AdminServiceClient adminClient = new LocalAdminService.AdminServiceClient();
                    results = adminClient.CurrencyConversionSet(isoFrom, isoTo, conversionRate, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    AdminService.AdminServiceClient adminClient = new AdminService.AdminServiceClient();
                    results = adminClient.CurrencyConversionSet(isoFrom, isoTo, conversionRate, userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                {
                    lblTestResults.Text = "Currency Conversion Set Success";
                }
                else
                { lblTestResults.Text = "Currency Conversion Set Failed";  }
            }
            catch
            {
                lblTestResults.Text = "Currency Conversion Set Failed";
                throw;
            }
        }
        public void CurrencySet(string currencyIso, string currency, string param3, string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                bool results = false;
                bool isActive = true;
                if (param3.ToLower().Equals("no")) // || param3.Equals("1"))
                { isActive = false; }
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalAdminService.AdminServiceClient adminClient = new LocalAdminService.AdminServiceClient();
                    results = adminClient.CurrencySet(currencyIso, currency, isActive, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    AdminService.AdminServiceClient adminClient = new AdminService.AdminServiceClient();
                    results = adminClient.CurrencySet(currencyIso, currency, isActive, userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                    { lblTestResults.Text = "Currency Set Success"; }
                else
                    { lblTestResults.Text = "Currency Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Currency Set Failed";
                throw;
            }
        }
        public void DeliveryTypesGet(string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "DeliveryTypes";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalAdminService.AdminServiceClient adminClient = new LocalAdminService.AdminServiceClient();
                byte[] strTemp = adminClient.DeliveryTypesGet(userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                AdminService.AdminServiceClient adminClient = new AdminService.AdminServiceClient();
                byte[] strTemp = adminClient.DeliveryTypesGet(userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void BookClearingInstructionSet(string bookGroup, string book, string countryCode, string currencyIso, string divRate,
                string cashInstructions, string securityInstructions, string actUserId, string isActive, string userId, string userPassword,
                        string functionPath)
        {
            try
            {
                bool results = false;
                bool bisActive = false;
                if (isActive.ToLower().Equals("yes"))
                { bisActive = true; }
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalBooksService.BooksServiceClient booksClient = new LocalBooksService.BooksServiceClient();
                    results = booksClient.BookClearingInstructionSet(bookGroup, book, countryCode, currencyIso, divRate, cashInstructions,
                            securityInstructions, actUserId, bisActive, userId, userPassword, functionPath);
                }
                else
                {
                    BooksService.BooksServiceClient booksClient = new BooksService.BooksServiceClient();
                    results = booksClient.BookClearingInstructionSet(bookGroup, book, countryCode, currencyIso, divRate, cashInstructions,
                            securityInstructions, actUserId, bisActive, userId, userPassword, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Book Clearing Instruction Set Success"; }
                else
                { lblTestResults.Text = "Book Clearing Instruction Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Book Clearing Instruction Set Failed";
                throw;
            }
        }
        public void BookClearingInstructionsGet(string bookGroup, string book, string userId, string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "BookClearingInstructions";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalBooksService.BooksServiceClient booksClient = new LocalBooksService.BooksServiceClient();
                byte[] strTemp = booksClient.BookClearingInstructionsGet(bookGroup, book, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                BooksService.BooksServiceClient booksClient = new BooksService.BooksServiceClient();
                byte[] strTemp = booksClient.BookClearingInstructionsGet(bookGroup, book, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void BookContactSet(string bookGroup, string book, string firstName, string lastName, string function, string phoneNumber, 
                string faxNumber, string comment, string actUserId, string isActive, string userId, string userPassword, string functionPath)
        {
            try
            {
                bool results = false;
                bool bisActive = false;
                if (isActive.ToLower().Equals("yes"))
                { bisActive = true; }
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalBooksService.BooksServiceClient booksClient = new LocalBooksService.BooksServiceClient();
                    results = booksClient.BookContactSet(bookGroup, book, firstName, lastName, function, phoneNumber, faxNumber,
                            comment, actUserId, bisActive, userId, userPassword, functionPath);
                }
                else
                {
                    BooksService.BooksServiceClient booksClient = new BooksService.BooksServiceClient();
                    results = booksClient.BookContactSet(bookGroup, book, firstName, lastName, function, phoneNumber, faxNumber,
                            comment, actUserId, bisActive, userId, userPassword, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Book Contact Set Success"; }
                else
                { lblTestResults.Text = "Book Contact Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Book Contact Set Failed";
                throw;
            }
        }
        public void BookContactsGet(string bookGroup, string book, short utcOffSet, string userId, string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "BookContacts";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalBooksService.BooksServiceClient booksClient = new LocalBooksService.BooksServiceClient();
                byte[] strTemp = booksClient.BookContactsGet(bookGroup, book, utcOffSet, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                BooksService.BooksServiceClient booksClient = new BooksService.BooksServiceClient();
                byte[] strTemp = booksClient.BookContactsGet(bookGroup, book, utcOffSet, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void BookCreditLimitsGet(string bizDate, string bookGroup, string bookParent, string book, short utcOffSet, string userId, 
                    string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "BookCreditLimits";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalBooksService.BooksServiceClient booksClient = new LocalBooksService.BooksServiceClient();
                byte[] strTemp = booksClient.BookCreditLimitsGet(bizDate, bookGroup, bookParent, book, utcOffSet, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                BooksService.BooksServiceClient booksClient = new BooksService.BooksServiceClient();
                byte[] strTemp = booksClient.BookCreditLimitsGet(bizDate, bookGroup, bookParent, book, utcOffSet, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void BookFundSet(string bookGroup, string book, string actUserId, string currencyIso, string fund, string isActive, 
                string userId, string userPassword, string functionPath)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                bool results = false;
                bool bisActive = false;
                if (isActive.ToLower().Equals("yes"))
                { bisActive = true; }

                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalBooksService.BooksServiceClient booksClient = new LocalBooksService.BooksServiceClient();
                    results = booksClient.BookFundSet(bookGroup, book, actUserId, currencyIso, fund, bisActive, userId, userPassword, functionPath);
                }
                else
                {
                    BooksService.BooksServiceClient booksClient = new BooksService.BooksServiceClient();
                    results = booksClient.BookFundSet(bookGroup, book, actUserId, currencyIso, fund, bisActive, userId, userPassword, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Book Fund Set Success"; }
                else
                { lblTestResults.Text = "Book Fund Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Book Fund Set Failed";
                throw;
            }
        }
        public void BookFundsGet(string bookGroup, string book, string currencyIso, string userId, string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "BookFunds";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalBooksService.BooksServiceClient booksClient = new LocalBooksService.BooksServiceClient();
                byte[] strTemp = booksClient.BookFundsGet(bookGroup, book, currencyIso, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                BooksService.BooksServiceClient booksClient = new BooksService.BooksServiceClient();
                byte[] strTemp = booksClient.BookFundsGet(bookGroup, book, currencyIso, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void BookGroupRoll(string bizDate, string bizDatePrior, string userId, string bookGroup, string userPassword, string functionPath)
        {
            try
            {
                bool results = false;
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalBooksService.BooksServiceClient booksClient = new LocalBooksService.BooksServiceClient();
                    results = booksClient.BookGroupRoll(bizDate, bizDatePrior, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    BooksService.BooksServiceClient booksClient = new BooksService.BooksServiceClient();
                    results = booksClient.BookGroupRoll(bizDate, bizDatePrior, userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Book Group Roll Success"; }
                else
                { lblTestResults.Text = "Book Group Roll Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Book Group Roll Failed";
                throw;
            }
        }
        public void BookGroupSet(string bookGroup, string bookName, string timeZoneId, string bizDate, string bizDateContract, string bizDateBank,
            string bizDateExchange, string bizDatePrior, string bizDatePriorBank, string bizDatePriorExchange, string bizDateNext, 
            string bizDateNextBank, string bizDateNextExchange, string useWeekends, string settlementType,  
                string userId, string userPassword, string functionPath)
        {
            try
            {
                bool bUseWeekends = false;
                if (useWeekends.ToLower().Equals("yes"))
                { bUseWeekends = true; }
                bool results = false;
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalBooksService.BooksServiceClient booksClient = new LocalBooksService.BooksServiceClient();
                    results = booksClient.BookGroupSet(bookGroup, bookName, timeZoneId, bizDate, bizDateContract, bizDateBank, bizDateExchange,
                            bizDatePrior, bizDatePriorBank, bizDatePriorExchange, bizDateNext, bizDateNextBank, bizDateNextExchange, bUseWeekends,
                            settlementType, userId, userPassword, functionPath);
                }
                else
                {
                    BooksService.BooksServiceClient booksClient = new BooksService.BooksServiceClient();
                    results = booksClient.BookGroupSet(bookGroup, bookName, timeZoneId, bizDate, bizDateContract, bizDateBank, bizDateExchange,
                            bizDatePrior, bizDatePriorBank, bizDatePriorExchange, bizDateNext, bizDateNextBank, bizDateNextExchange, bUseWeekends,
                            settlementType, userId, userPassword, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Book Group Set Success"; }
                else
                { lblTestResults.Text = "Book Group Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Book Group Set Failed";
                throw;
            }
        }
        public void BookGroupsGet(string bookGroup, string bizDate, string userId, string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "BookGroups";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalBooksService.BooksServiceClient booksClient = new LocalBooksService.BooksServiceClient();
                byte[] strTemp = booksClient.BookGroupsGet(bookGroup, bizDate, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                BooksService.BooksServiceClient booksClient = new BooksService.BooksServiceClient();
                byte[] strTemp = booksClient.BookGroupsGet(bookGroup, bizDate, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void BooksGet(string bookGroup, string book, string userId, string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "Books";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalBooksService.BooksServiceClient booksClient = new LocalBooksService.BooksServiceClient();
                byte[] strTemp = booksClient.BooksGet(bookGroup, book, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                BooksService.BooksServiceClient booksClient = new BooksService.BooksServiceClient();
                byte[] strTemp = booksClient.BooksGet(bookGroup, book, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void BookSet(string bookGroup, string bookParent, string book, string bookName, string addressLine1, string addressLine2, 
                string addressLine3, string phoneNumber, string faxNumber, string marginBorrow, string marginLoan, string markRoundHouse, 
                string roundInstitution, string rateStockBorrow, string rateStockLoan, string rateBondBorrow, string rateBondLoan, string countryCode,
                string fundDefault, string priceMin, string amountMin, string actUserId, string isActive, 
                string userId, string userPassword, string functionPath)
        { 
            try
            {
                bool results = false;
                bool bIsActive = false;
                DataSet dsTemp = new DataSet();
                if (isActive.ToLower().Equals("yes"))
                { bIsActive = true; }
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalBooksService.BooksServiceClient booksClient = new LocalBooksService.BooksServiceClient();
                    results = booksClient.BookSet(bookGroup, bookParent, book, bookName, addressLine1, addressLine2, addressLine3, phoneNumber,
                        faxNumber, marginBorrow, marginLoan, markRoundHouse, roundInstitution, rateStockBorrow, rateStockLoan, rateBondBorrow,
                        rateBondLoan, countryCode, fundDefault, priceMin, amountMin, actUserId, bIsActive, userId, userPassword, functionPath);
                }
                else
                {
                    BooksService.BooksServiceClient booksClient = new BooksService.BooksServiceClient();
                    results = booksClient.BookSet(bookGroup, bookParent, book, bookName, addressLine1, addressLine2, addressLine3, phoneNumber,
                        faxNumber, marginBorrow, marginLoan, markRoundHouse, roundInstitution, rateStockBorrow, rateStockLoan, rateBondBorrow,
                        rateBondLoan, countryCode, fundDefault, priceMin, amountMin, actUserId, bIsActive, userId, userPassword, functionPath);
                }
                if(results.Equals(true))
                { lblTestResults.Text = "Book Set Success"; }
                else
                { lblTestResults.Text = "Book Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Book Set Failed";
                throw;
            }
        }

        private void testAdminBusinessClassToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void BookCreditLimitSet(string bizDate, string bookGroup, string bookParent, string book, string borrowLimitAmount, string loanLimitAmount,
                string actUserId, string userId, string userPassword, string functionPath)
        {
            try
            {
                bool results = false;
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalBooksService.BooksServiceClient booksClient = new LocalBooksService.BooksServiceClient();
                    results = booksClient.BookCreditLimitSet(bizDate, bookGroup, bookParent, book, borrowLimitAmount, loanLimitAmount,
                            actUserId, userId, userPassword, functionPath);
                }
                else
                {
                    BooksService.BooksServiceClient booksClient = new BooksService.BooksServiceClient();
                    results = booksClient.BookCreditLimitSet(bizDate, bookGroup, bookParent, book, borrowLimitAmount, loanLimitAmount,
                            actUserId, userId, userPassword, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Book CreditLimit Set Success"; }
                else
                { lblTestResults.Text = "Book CreditLimit Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Book CreditLimit Set Failed";
                throw;
            }
        }

        public void ContractBillingsGet(string bizDate, string bookGroup, string userId, string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "ContractBillings";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalContractsService.ContractsServiceClient contractsClient = new LocalContractsService.ContractsServiceClient();
                byte[] strTemp = contractsClient.ContractBillingsGet(bizDate, bookGroup, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                ContractsService.ContractsServiceClient contractsClient = new ContractsService.ContractsServiceClient();
                byte[] strTemp = contractsClient.ContractBillingsGet(bizDate, bookGroup, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void ContractDetailsGet(string bizDate, string bookGroup, string userId, string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "ContractDetails";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalContractsService.ContractsServiceClient contractsClient = new LocalContractsService.ContractsServiceClient();
                byte[] strTemp = contractsClient.ContractDetailsGet(bizDate, bookGroup, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                ContractsService.ContractsServiceClient contractsClient = new ContractsService.ContractsServiceClient();
                byte[] strTemp = contractsClient.ContractDetailsGet(bizDate, bookGroup, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void ContractRateChangeAsOfSet(string startDate, string endDate, string bookGroup, string book, string contractId, 
                string oldRate, string newRate, string actUserId, string userId, string userPassword, string functionPath)
        {
            try
            {
                bool results = false; 
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalContractsService.ContractsServiceClient contractsClient = new LocalContractsService.ContractsServiceClient();
                    results = contractsClient.ContractRateChangeAsOfSet(startDate, endDate, bookGroup, book, contractId, oldRate, newRate,
                                actUserId, userId, userPassword, functionPath);
                }
                else
                {
                    ContractsService.ContractsServiceClient contractsClient = new ContractsService.ContractsServiceClient();
                    results = contractsClient.ContractRateChangeAsOfSet(startDate, endDate, bookGroup, book, contractId, oldRate, newRate,
                                actUserId, userId, userPassword, functionPath);
                }
                if(results.Equals(true))
                    { lblTestResults.Text = "Contract RateAsOfChange Set Success"; }
                else
                    { lblTestResults.Text = "Contract RateAsOfChange Set Failed"; }

                }
            catch
            {
                lblTestResults.Text = "Contract RateAsOfChange Set Failed";
                throw;
            }
        }
        public void ContractSet(string bizDate, string bookGroup, string contractId, string contractType, string book, string secId, string quantity,
                string quantitySettled, string amount, string amountSettled, string collateralCode, string valueDate, string settleDate, string termDate,
                string rate, string rateCode, string statusFlag, string poolCode, string divRate, string sDivCallable, string sIncomeTracked,
                string marginCode, string margin, string currencyIso, string securityDepot, string cashDepot, string otherBook, string comment, string fund,
                string tradeRefId, string feeAmount, string feeCurrencyIso, string feeType, string sReturnData, string sIsIncremental, string sIsActive, 
                string userId, string userPassword, string functionPath)
        {
            try
            {
                bool isActive = false;
                bool divCallable = false;
                bool incomeTracked = false;
                bool returnData = false;
                bool isIncremental = false;
                bool results = false; 
                if (sIsActive.ToLower().Equals("yes"))
                    { isActive = true; }
                if (sDivCallable.ToLower().Equals("yes"))
                    { divCallable = true; }
                if (sIncomeTracked.ToLower().Equals("yes"))
                    { incomeTracked = true; }
                if (sReturnData.ToLower().Equals("yes"))
                    { returnData = true; }
                if (sIsIncremental.ToLower().Equals("yes"))
                    { isIncremental = true; }
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalContractsService.ContractsServiceClient contractsClient = new LocalContractsService.ContractsServiceClient();
                    results = contractsClient.ContractSet(bizDate, bookGroup, contractId, contractType, book, secId, quantity, quantitySettled,
                            amount, amountSettled, collateralCode, valueDate, settleDate, termDate, rate, rateCode, statusFlag, poolCode, divRate,
                            divCallable, incomeTracked, marginCode, margin, currencyIso, securityDepot, cashDepot, otherBook, comment, fund, tradeRefId,
                            feeAmount, feeCurrencyIso, feeType, returnData, isIncremental, isActive, userId, userPassword, functionPath);
                }
                else
                {
                    ContractsService.ContractsServiceClient contractsClient = new ContractsService.ContractsServiceClient();
                    results = contractsClient.ContractSet(bizDate, bookGroup, contractId, contractType, book, secId, quantity, quantitySettled,
                            amount, amountSettled, collateralCode, valueDate, settleDate, termDate, rate, rateCode, statusFlag, poolCode, divRate,
                            divCallable, incomeTracked, marginCode, margin, currencyIso, securityDepot, cashDepot, otherBook, comment, fund, tradeRefId,
                            feeAmount, feeCurrencyIso, feeType, returnData, isIncremental, isActive, userId, userPassword, functionPath);
                }
                if (results.Equals(true))
                    { lblTestResults.Text = "Contract Set Success"; }
                else
                    { lblTestResults.Text = "Contract Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Contract Set Failed";
                throw;
            }
        }
        public void ContractsGet(string bizDate, string bookGroup, string contractId, string contractType, string userId, string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "Contracts";

            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalContractsService.ContractsServiceClient contractsClient = new LocalContractsService.ContractsServiceClient();
                byte[] strTemp = contractsClient.ContractsGet(bizDate, bookGroup, contractId, contractType, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                ContractsService.ContractsServiceClient contractsClient = new ContractsService.ContractsServiceClient();
                byte[] strTemp = contractsClient.ContractsGet(bizDate, bookGroup, contractId, contractType, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void ContractsResearchGet(string bizDate, string startDate, string stopDate, string bookGroup, string book, string contractId,
                string secId, string amount, string logicId, string userId, string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "ContractResearch";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalContractsService.ContractsServiceClient contractsClient = new LocalContractsService.ContractsServiceClient();
                byte[] strTemp = contractsClient.ContractsResearchGet(bizDate, startDate, stopDate, bookGroup, book, contractId, secId, amount,
                        logicId, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                ContractsService.ContractsServiceClient contractsClient = new ContractsService.ContractsServiceClient();
                byte[] strTemp = contractsClient.ContractsResearchGet(bizDate, startDate, stopDate, bookGroup, book, contractId, secId, amount,
                        logicId, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void DealSet(string dealId, string bookGroup, string dealType, string book, string bookContact, string contractId, string secId, 
                string quantity, string amount, string collateralCode, string valueDate, string settleDate, string termDate, string rate, 
                string rateCode, string poolCode, string divRate, string sDivCallable, string sIncomeTracked, string marginCode, string margin, 
                string currencyIso, string securityDepot, string cashDepot, string comment, string fund, string dealStatus, string sIsActive, 
                string actUserId, string sReturnData, string feeAmount, string feeCurrencyIso, string feeType, string userId, string userPassword, 
                string functionPath)
        {
            try
            {
                bool isActive = false;
                bool divCallable = false;
                bool incomeTracked = false;
                bool returnData = false;
                bool results = false;
 
                if (sIsActive.ToLower().Equals("yes"))
                { isActive = true; }
                if (sDivCallable.ToLower().Equals("yes"))
                { divCallable = true; }
                if (sIncomeTracked.ToLower().Equals("yes"))
                { incomeTracked = true; }
                if (sReturnData.ToLower().Equals("yes"))
                { returnData = true; }
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalDealsService.DealsServiceClient dealsClient = new LocalDealsService.DealsServiceClient();
                    results = dealsClient.DealSet(dealId, bookGroup, dealType, book, bookContact, contractId, secId, quantity, amount,
                            collateralCode, valueDate, settleDate, termDate, rate, rateCode, poolCode, divRate, divCallable, incomeTracked, marginCode,
                            margin, currencyIso, securityDepot, cashDepot, comment, fund, dealStatus, isActive, actUserId, returnData, feeAmount,
                            feeCurrencyIso, feeType, userId, userPassword, functionPath);
                }
                else
                {
                    DealsService.DealsServiceClient dealsClient = new DealsService.DealsServiceClient();
                    results = dealsClient.DealSet(dealId, bookGroup, dealType, book, bookContact, contractId, secId, quantity, amount,
                            collateralCode, valueDate, settleDate, termDate, rate, rateCode, poolCode, divRate, divCallable, incomeTracked, marginCode,
                            margin, currencyIso, securityDepot, cashDepot, comment, fund, dealStatus, isActive, actUserId, returnData, feeAmount,
                            feeCurrencyIso, feeType, userId, userPassword, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Deal Set Success"; }
                else
                { lblTestResults.Text = "Deal Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Contract Set Failed";
                throw;
            }
        }
        public void DealsGet(string bizDate, string dealId, string dealIdPrefix, string sIsActive, string sUtcOffset, string userId,
                string userPassword, string bookGroup, string functionPath)
        {
            bool isActive = false;
            short utcOffSet = short.Parse(sUtcOffset);
            if (sIsActive.ToLower().Equals("yes"))
            { isActive = true; }
            string tableName = "Deals";
            DataSet dsTemp = new DataSet();
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalDealsService.DealsServiceClient dealsClient = new LocalDealsService.DealsServiceClient();
                byte[] strTemp = dealsClient.DealsGet(bizDate, dealId, dealIdPrefix, isActive, utcOffSet, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                DealsService.DealsServiceClient dealsClient = new DealsService.DealsServiceClient();
                byte[] strTemp = dealsClient.DealsGet(bizDate, dealId, dealIdPrefix, isActive, utcOffSet, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void DealToContract(string dealId, string bizDate, string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            bool results = false;
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalDealsService.DealsServiceClient dealsClient = new LocalDealsService.DealsServiceClient();
                results = dealsClient.DealToContract(dealId, bizDate, userId, userPassword, bookGroup, functionPath);
            }
            else
            {
                DealsService.DealsServiceClient dealsClient = new DealsService.DealsServiceClient();
                results = dealsClient.DealToContract(dealId, bizDate, userId, userPassword, bookGroup, functionPath);
            }
            if (results.Equals(true))
                { lblTestResults.Text = "Deal To Contract Success"; }
            else
                { lblTestResults.Text = "Deal To Contract Failed"; }
        }

        public void FundingRateSet(string bizDate, string fund, string fundingRate, string actUserId,
                           string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                bool results = false;
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalFundsService.FundsServiceClient fundsClient = new LocalFundsService.FundsServiceClient();
                    results = fundsClient.FundingRateSet(bizDate, fund, fundingRate, actUserId, userId, userPassword, bookGroup,functionPath);
                }
                else
                {
                    FundsService.FundsServiceClient fundsClient = new FundsService.FundsServiceClient();
                    results = fundsClient.FundingRateSet(bizDate, fund, fundingRate, actUserId, userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Funding Rate Set Success"; }
                else
                { lblTestResults.Text = "Funding Rate Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Funding Rate Set Failed";
                throw;
            }
        }
        public void FundsGet(string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "Funds";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalFundsService.FundsServiceClient fundsClient = new LocalFundsService.FundsServiceClient();
                byte[] strTemp = fundsClient.FundsGet(userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                FundsService.FundsServiceClient fundsClient = new FundsService.FundsServiceClient();
                byte[] strTemp = fundsClient.FundsGet(userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void FundingRatesRoll(string bizDate, string bizDatePrior, string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                bool results = false;
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalFundsService.FundsServiceClient fundsClient = new LocalFundsService.FundsServiceClient();
                    results = fundsClient.FundingRatesRoll(bizDate, bizDatePrior, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    FundsService.FundsServiceClient fundsClient = new FundsService.FundsServiceClient();
                    results = fundsClient.FundingRatesRoll(bizDate, bizDatePrior, userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Funding Rates Roll Success"; }
                else
                { lblTestResults.Text = "Funding Rates Roll Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Funding Rates Roll Failed";
                throw;
            }
        }
        public void FundingRateResearchGet(string startDate, string stopDate, string fund, string utcOffSet, 
                    string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                string tableName = "FundingRateResearch";
                short sUtcOffSet = short.Parse(utcOffSet);
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalFundsService.FundsServiceClient fundsClient = new LocalFundsService.FundsServiceClient();
                    byte[] strTemp = fundsClient.FundingRateResearchGet(startDate, stopDate, fund, sUtcOffSet, userId, userPassword,
                            bookGroup, functionPath);
                    dsTemp = ConvertToDataTable(strTemp, tableName);
                }
                else
                {
                    FundsService.FundsServiceClient fundsClient = new FundsService.FundsServiceClient();
                    byte[] strTemp = fundsClient.FundingRateResearchGet(startDate, stopDate, fund, sUtcOffSet, userId, userPassword,
                            bookGroup, functionPath);
                    dsTemp = ConvertToDataTable(strTemp, tableName);
                }
                this.c1TrueDBGrid1.ClearFields();
                this.c1TrueDBGrid1.DataSource = dsTemp;
                this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
                this.c1TrueDBGrid1.Update();
                Show();
            }
            catch
            {
                throw;
            }
        }
        public void FundingRatesGet(string bizDate, string utcOffSet, string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "FundingRates";
            short sUtcOffSet = short.Parse(utcOffSet);
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalFundsService.FundsServiceClient fundsClient = new LocalFundsService.FundsServiceClient();
                byte[] strTemp = fundsClient.FundingRatesGet(bizDate, sUtcOffSet, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                FundsService.FundsServiceClient fundsClient = new FundsService.FundsServiceClient();
                byte[] strTemp = fundsClient.FundingRatesGet(bizDate, sUtcOffSet, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void HolidaySet(string bookGroup, string holidayDate, string countryCode, string description, string sIsBankHoliday, 
                string sIsExchangeHoliday, string actUserId, string sIsActive, string userId, string userPassword, string functionPath)
        {
            try
            {
                bool results = false;
                bool isBankHoliday = false;
                bool isExchangeHoliday = false;
                bool isActive = false;

                if (sIsBankHoliday.ToLower().Equals("yes"))
                { isBankHoliday = true; }
                if (sIsExchangeHoliday.ToLower().Equals("yes"))
                { isExchangeHoliday = true; }
                if (sIsActive.ToLower().Equals("yes"))
                { isActive = true; }

                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalFunctionsService.FunctionsServiceClient functionsClient = new LocalFunctionsService.FunctionsServiceClient();
                    results = functionsClient.HolidaySet(bookGroup, holidayDate, countryCode, description, isBankHoliday, isExchangeHoliday,
                                actUserId, isActive, userId, userPassword, functionPath);
                }
                else
                {
                    FunctionsService.FunctionsServiceClient functionsClient = new FunctionsService.FunctionsServiceClient();
                    results = functionsClient.HolidaySet(bookGroup, holidayDate, countryCode, description, isBankHoliday, isExchangeHoliday,
                                actUserId, isActive, userId, userPassword, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Holiday Set Success"; }
                else
                { lblTestResults.Text = "Holiday Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Holiday Set Failed";
                throw;
            }
        }
        public void SettlementSystemProcessSet(string bizDate, string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                bool results = false;
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalFunctionsService.FunctionsServiceClient functionsClient = new LocalFunctionsService.FunctionsServiceClient();
                    results = functionsClient.SettlementSystsemProcessSet(bizDate, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    FunctionsService.FunctionsServiceClient functionsClient = new FunctionsService.FunctionsServiceClient();
                    results = functionsClient.SettlementSystsemProcessSet(bizDate, userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Settlement System Process Set Success"; }
                else
                { lblTestResults.Text = "Settlement System Process Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Settlement System Process Set Failed";
                throw;
            }
        }
        public void KeyValueSet(string keyId, string keyValue, string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                bool results = false;
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalFunctionsService.FunctionsServiceClient functionsClient = new LocalFunctionsService.FunctionsServiceClient();
                    results = functionsClient.KeyValueSet(keyId, keyValue, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    FunctionsService.FunctionsServiceClient functionsClient = new FunctionsService.FunctionsServiceClient();
                    results = functionsClient.KeyValueSet(keyId, keyValue, userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Key Value Set Success"; }
                else
                { lblTestResults.Text = "Key Value Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Key Value Set Failed";
                throw;
            }
        }

        public void HolidaysGet(string bookGroupToCheck, string countryCode, string description, string utcOffSet,
                    string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "Holidays";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalFunctionsService.FunctionsServiceClient functionsClient = new LocalFunctionsService.FunctionsServiceClient();
                byte[] strTemp = functionsClient.HolidaysGet(bookGroupToCheck, countryCode, description, utcOffSet, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                FunctionsService.FunctionsServiceClient functionsClient = new FunctionsService.FunctionsServiceClient();
                byte[] strTemp = functionsClient.HolidaysGet(bookGroupToCheck, countryCode, description, utcOffSet, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        
        public void IsBankHoliday(string bookGroupToCheck, string countryCode, string holidayDate, string utcOffSet, 
                                    string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                bool results = false;
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalFunctionsService.FunctionsServiceClient functionsClient = new LocalFunctionsService.FunctionsServiceClient();
                    results = functionsClient.IsBankHoliday(bookGroupToCheck, countryCode, holidayDate, utcOffSet, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    FunctionsService.FunctionsServiceClient functionsClient = new FunctionsService.FunctionsServiceClient();
                    results = functionsClient.IsBankHoliday(bookGroupToCheck, countryCode, holidayDate, utcOffSet, userId, userPassword, bookGroup, functionPath);
                }
                lblTestResults.Text = "IsBankHoliday = " + results.ToString();
            }
            catch
            {
                throw;
            }
        }
        public void IsExchangeHoliday(string bookGroupToCheck, string countryCode, string holidayDate, string utcOffSet,
                                    string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                bool results = false;
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalFunctionsService.FunctionsServiceClient functionsClient = new LocalFunctionsService.FunctionsServiceClient();
                    results = functionsClient.IsExchangeHoliday(bookGroupToCheck,countryCode, holidayDate, utcOffSet, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    FunctionsService.FunctionsServiceClient functionsClient = new FunctionsService.FunctionsServiceClient();
                    results = functionsClient.IsExchangeHoliday(bookGroupToCheck, countryCode, holidayDate, utcOffSet, userId, userPassword, bookGroup, functionPath);
                }
                lblTestResults.Text = "IsExchangeHoliday = " + results.ToString();
            }
            catch
            {
                throw;
            }
        }
        
        public void KeyValuesGet(string keyId, string userId, string userPassword, string bookGroup, string functionPath)
        {
            string strTemp = "";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalFunctionsService.FunctionsServiceClient functionsClient = new LocalFunctionsService.FunctionsServiceClient();
                strTemp = functionsClient.KeyValuesGet(keyId, userId, userPassword, bookGroup, functionPath);
            }
            else
            {
                FunctionsService.FunctionsServiceClient functionsClient = new FunctionsService.FunctionsServiceClient();
                strTemp = functionsClient.KeyValuesGet(keyId, userId, userPassword, bookGroup, functionPath);
            }
            lblTestResults.Text = "Key Value Returned Is: " + strTemp;
        }
        
        public void KeyValuesListGet(string userId, string userPassword, string bookGroup, string functionPathSet)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "KeyValues";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalFunctionsService.FunctionsServiceClient functionsClient = new LocalFunctionsService.FunctionsServiceClient();
                byte[] strTemp = functionsClient.KeyValuesListGet(userId, userPassword, bookGroup, functionPathSet);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                FunctionsService.FunctionsServiceClient functionsClient = new FunctionsService.FunctionsServiceClient();
 //               byte[] strTemp = functionsClient.KeyValuesListGet(userId, userPassword, bookGroup, functionPathSet);
 //               dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }

        public void LogicOperatorsGet(string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "LogicOperators";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalFunctionsService.FunctionsServiceClient functionsClient = new LocalFunctionsService.FunctionsServiceClient();
                byte[] strTemp = functionsClient.LogicOperatorsGet(userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                FunctionsService.FunctionsServiceClient functionsClient = new FunctionsService.FunctionsServiceClient();
                byte[] strTemp = functionsClient.LogicOperatorsGet(userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void InventoriesGet(string desk, string secId, string sUtcOffSet, string ageDayCount, 
                    string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "Inventory";
            short utcOffSet = short.Parse(sUtcOffSet);
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalInventoryService.InventoryServiceClient inventoryClient = new LocalInventoryService.InventoryServiceClient();
                byte[] strTemp = inventoryClient.InventoriesGet(desk, secId, utcOffSet, ageDayCount, userId, userPassword, bookGroup,functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                InventoryService.InventoryServiceClient inventoryClient = new InventoryService.InventoryServiceClient();
                byte[] strTemp = inventoryClient.InventoriesGet(desk, secId, utcOffSet, ageDayCount, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void InventoryControlGet(string bizDate, string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "InventoryControl";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalInventoryService.InventoryServiceClient inventoryClient = new LocalInventoryService.InventoryServiceClient();
                byte[] strTemp = inventoryClient.InventoryControlGet(bizDate, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                InventoryService.InventoryServiceClient inventoryClient = new InventoryService.InventoryServiceClient();
                byte[] strTemp = inventoryClient.InventoryControlGet(bizDate, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void InventorySet(string bizDate, string desk, string secId, string rate, string modeCode, string quantity, string incrementCurrentQuantity,
                string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                bool results = false;
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalInventoryService.InventoryServiceClient inventoryClient = new LocalInventoryService.InventoryServiceClient();
                    results = inventoryClient.InventorySet(bizDate, desk, secId, rate, modeCode, quantity, incrementCurrentQuantity, 
                        userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    InventoryService.InventoryServiceClient inventoryClient = new InventoryService.InventoryServiceClient();
                    results = inventoryClient.InventorySet(bizDate, desk, secId, rate, modeCode, quantity, incrementCurrentQuantity,
                        userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Inventory Set Success"; }
                else
                { lblTestResults.Text = "Inventory Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Inventory Set Failed";
                throw;
            }
        }
        public void MarkAsOfSet(string tradeDate, string settleDate, string book, string contractId, string contractType, string price, 
                string markId, string deliveryCode, string actUserId, string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                int iResults = 0;
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalMarksService.MarksServiceClient marksClient = new LocalMarksService.MarksServiceClient();
                    iResults = marksClient.MarkAsOfSet(tradeDate, settleDate, bookGroup, book, contractId, contractType, price, markId, deliveryCode,
                        actUserId, userId, userPassword, functionPath);
                }
                else
                {
                    MarksService.MarksServiceClient marksClient = new MarksService.MarksServiceClient();
                    iResults = marksClient.MarkAsOfSet(tradeDate, settleDate, bookGroup, book, contractId, contractType, price, markId, deliveryCode,
                        actUserId, userId, userPassword, functionPath);
                }
                if (iResults > 0)
                { lblTestResults.Text = "Mark As Of Set Success"; }
                else
                { lblTestResults.Text = "Mark As Of Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Mark As Of Set Failed";
                throw;
            }
        }
        public void MarkIsExistGet(string bizDate, string book, string contractId, string contractType, string secId, string amount,
            string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                bool results = false;
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalMarksService.MarksServiceClient marksClient = new LocalMarksService.MarksServiceClient();
                    results = marksClient.MarkIsExistGet(bizDate, bookGroup, book, contractId, contractType, secId, amount, 
                            userId, userPassword, functionPath);
                }
                else
                {
                    MarksService.MarksServiceClient marksClient = new MarksService.MarksServiceClient();
                    results = marksClient.MarkIsExistGet(bizDate, bookGroup, book, contractId, contractType, secId, amount,
                            userId, userPassword, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Mark DOES Exist"; }
                else
                { lblTestResults.Text = "Mark DOES NOT Exist"; }
            }
            catch
            {
                lblTestResults.Text = "Mark DOES NOT Exist";
                throw;
            }
        }
        public void MarkSet(string markId, string bizDate, string book, string contractId, string contractType, string secId, string amount,
                string openDate, string settleDate, string deliveryCode, string actUserId, string sIsActive, string userId, 
                string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                bool results = false;
                bool isActive = false;
                if (sIsActive.ToLower().Equals("yes"))
                { isActive = true; }
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalMarksService.MarksServiceClient marksClient = new LocalMarksService.MarksServiceClient();
                    results = marksClient.MarkSet(markId, bizDate, bookGroup, book, contractId, contractType, secId, amount, openDate, settleDate,
                            deliveryCode, actUserId, isActive, userId, userPassword, functionPath);
                }
                else
                {
                    MarksService.MarksServiceClient marksClient = new MarksService.MarksServiceClient();
                    results = marksClient.MarkSet(markId, bizDate, bookGroup, book, contractId, contractType, secId, amount, openDate, settleDate,
                            deliveryCode, actUserId, isActive, userId, userPassword, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Mark Set Success"; }
                else
                { lblTestResults.Text = "Mark Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Mark Set Failed";
                throw;
            }
        }
        public void MarksGet(string markId, string bizDate, string contractId, string bookGroup, string sUtcOffSet,
                    string userId, string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "Marks";
            short utcOffSet = short.Parse(sUtcOffSet);
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalMarksService.MarksServiceClient marksClient = new LocalMarksService.MarksServiceClient();
                byte[] strTemp = marksClient.MarksGet(markId, bizDate, contractId, bookGroup, utcOffSet, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                MarksService.MarksServiceClient marksClient = new MarksService.MarksServiceClient();
                byte[] strTemp = marksClient.MarksGet(markId, bizDate, contractId, bookGroup, utcOffSet, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void MarksSummaryByCashGet(string markId, string bizDate, string contractId, string bookGroup, string userId, 
                        string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "CreditDebitSummary";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalMarksService.MarksServiceClient marksClient = new LocalMarksService.MarksServiceClient();
                byte[] strTemp = marksClient.MarksSummaryByCashGet(markId, bizDate, contractId, bookGroup, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                MarksService.MarksServiceClient marksClient = new MarksService.MarksServiceClient();
                byte[] strTemp = marksClient.MarksSummaryByCashGet(markId, bizDate, contractId, bookGroup, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void MarksSummaryGet(string markId, string bizDate, string bizDateFormat, string contractId, string bookGroup, string userId,
                string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "MarkSummary";
            if (testEnv.ToUpper().Equals("LOCAL"))
            {
                LocalMarksService.MarksServiceClient marksClient = new LocalMarksService.MarksServiceClient();
                byte[] strTemp = marksClient.MarksSummaryGet(markId, bizDate, bizDateFormat, contractId, bookGroup, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                MarksService.MarksServiceClient marksClient = new MarksService.MarksServiceClient();
                byte[] strTemp = marksClient.MarksSummaryGet(markId, bizDate, bizDateFormat, contractId, bookGroup, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void BoxPositionGet(string bizDate, string secId, string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "BoxPositions";
            if (testEnv.Equals("Local"))
            {
                LocalPositionsService.PositionsServiceClient positionsClient = new LocalPositionsService.PositionsServiceClient();
                byte[] strTemp = positionsClient.BoxPositionGet(bizDate, bookGroup, secId, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                PositionsService.PositionsServiceClient positionsClient = new PositionsService.PositionsServiceClient();
                byte[] strTemp = positionsClient.BoxPositionGet(bizDate, bookGroup, secId, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void RecallSet(string recallId, string bizDate, string contractId, string contractType, string book, string secId, string quantity,
                string openDateTime, string reasonCode, string status, string actUserId, string sequenceNumber, string moveToDate, string buyInDate, 
                string comment, string sIsActive, string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                bool results = false;
                bool isActive = false;
                if (sIsActive.ToLower().Equals("yes"))
                { isActive = true; }
                if (testEnv.ToUpper().Equals("LOCAL"))
                {
                    LocalRecallsService.RecallsServiceClient recallsClient = new LocalRecallsService.RecallsServiceClient();
                    results = recallsClient.RecallSet(recallId, bizDate, bookGroup, contractId, contractType, book, secId, quantity, openDateTime, 
                          reasonCode, status, actUserId, sequenceNumber, moveToDate, buyInDate, comment, isActive, 
                            userId, userPassword, functionPath);
                }
                else
                {
                    RecallsService.RecallsServiceClient recallsClient = new RecallsService.RecallsServiceClient();
                    results = recallsClient.RecallSet(recallId, bizDate, bookGroup, contractId, contractType, book, secId, quantity, openDateTime,
                          reasonCode, status, actUserId, sequenceNumber, moveToDate, buyInDate, comment, isActive,
                            userId, userPassword, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Recall Set Success"; }
                else
                { lblTestResults.Text = "Recall Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Recall Set Failed";
                throw;
            }
        }
        public void RecallsGet(string bizDate, string recallId, string sUtcOffset, string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "Recalls";
            short utcOffSet = short.Parse(sUtcOffset.ToString());
            if (testEnv.Equals("Local"))
            {
                LocalRecallsService.RecallsServiceClient recallsClient = new LocalRecallsService.RecallsServiceClient();
                byte[] strTemp = recallsClient.RecallsGet(bizDate, recallId, utcOffSet, bookGroup, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                RecallsService.RecallsServiceClient recallsClient = new RecallsService.RecallsServiceClient();
                byte[] strTemp = recallsClient.RecallsGet(bizDate, recallId, utcOffSet, bookGroup, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void ReturnAsOfSet(string tradeDate, string bookGroup, string book, string contractId, string contractType, string returnId, 
                string quantity, string actUserId, string settleDate, string userId, string userPassword, string functionPath)
        {
            try
            {
                bool results = false;
                if (testEnv.Equals("Local"))
                {
                    LocalReturnsService.ReturnsServiceClient returnsClient = new LocalReturnsService.ReturnsServiceClient();
                    results = returnsClient.ReturnAsOfSet(tradeDate, bookGroup, book, contractId, contractType, returnId, quantity, actUserId,
                          settleDate, userId, userPassword, functionPath);
                }
                else
                {
                    ReturnsService.ReturnsServiceClient returnsClient = new ReturnsService.ReturnsServiceClient();
                    results = returnsClient.ReturnAsOfSet(tradeDate, bookGroup, book, contractId, contractType, returnId, quantity, actUserId,
                          settleDate, userId, userPassword, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Returns As Of Set Success"; }
                else
                { lblTestResults.Text = "Returns As Of Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Returns As Of Set Failed";
                throw;
            }
        }
        public void ReturnSet(string returnId, string bizDate, string bookGroup, string book, string contractId, string contractType, 
                string quantity, string actUserId, string settleDateProjected, string settleDateActual, string sIsActive, 
                string userId, string userPassword, string functionPath)
        {
            try
            {
                bool results = false;
                bool isActive = false;
                if (sIsActive.ToLower().Equals("yes"))
                { isActive = true; }
                if (testEnv.Equals("Local"))
                {
                    LocalReturnsService.ReturnsServiceClient returnsClient = new LocalReturnsService.ReturnsServiceClient();
                    results = returnsClient.ReturnSet(returnId, bizDate, bookGroup, book, contractId, contractType, quantity, actUserId,
                          settleDateProjected, settleDateActual, isActive, userId, userPassword, functionPath);
                }
                else
                {
                    ReturnsService.ReturnsServiceClient returnsClient = new ReturnsService.ReturnsServiceClient();
                    results = returnsClient.ReturnSet(returnId, bizDate, bookGroup, book, contractId, contractType, quantity, actUserId,
                          settleDateProjected, settleDateActual, isActive, userId, userPassword, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Returns Set Success"; }
                else
                { lblTestResults.Text = "Returns Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Returns Set Failed";
                throw;
            }
        }
        public void ReturnsGet(string returnId, string bizDate, string bookGroup, string contractId, string sUtcOffSet,
                            string userId, string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "Returns";
            short utcOffSet = short.Parse(sUtcOffSet.ToString());
            if (testEnv.Equals("Local"))
            {
                LocalReturnsService.ReturnsServiceClient returnsClient = new LocalReturnsService.ReturnsServiceClient();
                byte[] strTemp = returnsClient.ReturnsGet(returnId, bizDate, bookGroup, contractId, utcOffSet, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                ReturnsService.ReturnsServiceClient returnsClient = new ReturnsService.ReturnsServiceClient();
                byte[] strTemp = returnsClient.ReturnsGet(returnId, bizDate, bookGroup, contractId, utcOffSet, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void ReturnsSummaryByCashGet(string returnId, string bizDate, string bookGroup, string contractId, string sUtcOffSet, 
                            string userId, string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "Returns";
            short utcOffSet = short.Parse(sUtcOffSet.ToString());
            if (testEnv.Equals("Local"))
            {
                LocalReturnsService.ReturnsServiceClient returnsClient = new LocalReturnsService.ReturnsServiceClient();
                byte[] strTemp = returnsClient.ReturnsSummaryByCashGet(returnId, bizDate, bookGroup, contractId, utcOffSet, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                ReturnsService.ReturnsServiceClient returnsClient = new ReturnsService.ReturnsServiceClient();
                byte[] strTemp = returnsClient.ReturnsGet(returnId, bizDate, bookGroup, contractId, utcOffSet, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void PriceSet(string bizDate, string secId, string countryCode, string currencyIso, string price, string priceDate,
                string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                bool results = false;
                if (testEnv.Equals("Local"))
                {
                    LocalSecMasterService.SecMasterServiceClient secMasterClient = new LocalSecMasterService.SecMasterServiceClient();
                    results = secMasterClient.PriceSet(bizDate, secId, countryCode, currencyIso, price, priceDate, userId,
                          userPassword, bookGroup, functionPath);
                }
                else
                {
                    SecMasterService.SecMasterServiceClient secMasterClient = new SecMasterService.SecMasterServiceClient();
                    results = secMasterClient.PriceSet(bizDate, secId, countryCode, currencyIso, price, priceDate, userId,
                          userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Sec Master Price Set Success"; }
                else
                { lblTestResults.Text = "Sec Master Price Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Sec Master Price Set Failed";
                throw;
            }
        }
        public void PricesGet(string bizDate, string secId, string currencyIso, string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "Prices";
            if (testEnv.Equals("Local"))
            {
                LocalSecMasterService.SecMasterServiceClient secMasterClient = new LocalSecMasterService.SecMasterServiceClient();
                byte[] strTemp = secMasterClient.PricesGet(bizDate, secId, currencyIso, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                SecMasterService.SecMasterServiceClient secMasterClient = new SecMasterService.SecMasterServiceClient();
                byte[] strTemp = secMasterClient.PricesGet(bizDate, secId, currencyIso, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void SecIdAliasSet(string secId, string secIdTypeIndex, string secIdAlias, 
                    string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                bool results = false;
                if (testEnv.Equals("Local"))
                {
                    LocalSecMasterService.SecMasterServiceClient secMasterClient = new LocalSecMasterService.SecMasterServiceClient();
                    results = secMasterClient.SecIdAliasSet(secId, secIdTypeIndex, secIdAlias, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    SecMasterService.SecMasterServiceClient secMasterClient = new SecMasterService.SecMasterServiceClient();
                    results = secMasterClient.SecIdAliasSet(secId, secIdTypeIndex, secIdAlias, userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Sec ID Alias Set Success"; }
                else
                { lblTestResults.Text = "Sec ID Alias Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Sec ID Alias Set Failed";
                throw;
            }
        }
        public void SecMasterGet(string secId, string countryCode, string currencyIso, string bookGroup, string lookUpCriteria, 
                    string userId, string userPassword, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "SecMaster";
            if (testEnv.Equals("Local"))
            {
                LocalSecMasterService.SecMasterServiceClient secMasterClient = new LocalSecMasterService.SecMasterServiceClient();
                byte[] strTemp = secMasterClient.SecMasterGet(secId, countryCode, currencyIso, bookGroup, lookUpCriteria, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                SecMasterService.SecMasterServiceClient secMasterClient = new SecMasterService.SecMasterServiceClient();
                byte[] strTemp = secMasterClient.SecMasterGet(secId, countryCode, currencyIso, bookGroup, lookUpCriteria, userId, userPassword, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void SecMasterSet(string secId, string description, string baseType, string classGroup, string countryCode, string currencyIso,
                    string accruedInterest, string recordDateCash, string dividendRate, string secIdGroup, string symbol, string isin, string cusip,
                    string price, string priceDate, string sIsActive, string userId, string userPassword, string bookGroup, string functionPath)
        {
            try
            {
                bool results = false;
                bool isActive = false;
                if(sIsActive.ToLower().Equals("yes"))
                    { isActive = true; }
                if (testEnv.Equals("Local"))
                {
                    LocalSecMasterService.SecMasterServiceClient secMasterClient = new LocalSecMasterService.SecMasterServiceClient();
                    results = secMasterClient.SecMasterSet(secId, description, baseType, classGroup, countryCode, currencyIso, accruedInterest,
                            recordDateCash, dividendRate, secIdGroup, symbol, isin, cusip, price, priceDate, isActive, 
                            userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    SecMasterService.SecMasterServiceClient secMasterClient = new SecMasterService.SecMasterServiceClient();
                    results = secMasterClient.SecMasterSet(secId, description, baseType, classGroup, countryCode, currencyIso, accruedInterest,
                            recordDateCash, dividendRate, secIdGroup, symbol, isin, cusip, price, priceDate, isActive,
                            userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Sec Master Set Success"; }
                else
                { lblTestResults.Text = "Sec Master Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Sec Master Set Failed";
                throw;
            }
        }
        public void FunctionSet(string functionPathSet, string sMayView, string sMayEdit, string bookGroup, string functionPath, string userId, string userPassword)
        {   
            try
            {
                bool results = false;
                bool mayView = false;
                if (sMayView.ToLower().Equals("yes"))
                { mayView = true; }
                bool mayEdit = false;
                if (sMayEdit.ToLower().Equals("yes"))
                { mayEdit = true; }

                if (testEnv.Equals("Local"))
                {
                    LocalUserAdminService.UserAdminServiceClient userAdminClient = new LocalUserAdminService.UserAdminServiceClient();
                    results = userAdminClient.FunctionSet(functionPathSet, mayView, mayEdit, bookGroup, functionPath, userId, userPassword);
                }
                else
                {
                    UserAdminService.UserAdminServiceClient userAdminClient = new UserAdminService.UserAdminServiceClient();
                    results = userAdminClient.FunctionSet(functionPathSet, mayView, mayEdit, bookGroup, functionPath, userId, userPassword);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Function Set Success"; }
                else
                { lblTestResults.Text = "Function Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Function Set Failed";
                throw;
            }
        }
        public void FunctionsGet(string functionPathGet, string bookGroup, string functionPath, string userId, string userPassword)
        {         
            DataSet dsTemp = new DataSet();
            string tableName = "Functions";
            if (testEnv.Equals("Local"))
            {
                LocalUserAdminService.UserAdminServiceClient userAdminClient = new LocalUserAdminService.UserAdminServiceClient();
                byte[] strTemp = userAdminClient.FunctionsGet(functionPathGet, bookGroup, functionPath, userId, userPassword);
            }
            else
            {
                UserAdminService.UserAdminServiceClient userAdminClient = new UserAdminService.UserAdminServiceClient();
                byte[] strTemp = userAdminClient.FunctionsGet(functionPathGet, bookGroup, functionPath, userId, userPassword);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void RoleFunctionSet(string roleName, string functionPathSet, string sMayView, string sMayEdit, string comment, string sDelete, 
                                    string userId, string userPassword, string bookGroup, string functionPath)
        {   
            try
            {
                bool results = false;
                bool mayView = false;
                if (sMayView.ToLower().Equals("yes"))
                { mayView = true; }
                bool mayEdit = false;
                if (sMayEdit.ToLower().Equals("yes"))
                { mayEdit = true; }
                bool delete = false;
                if (sDelete.ToLower().Equals("yes"))
                { delete = true; }

                if (testEnv.Equals("Local"))
                {
                    LocalUserAdminService.UserAdminServiceClient userAdminClient = new LocalUserAdminService.UserAdminServiceClient();
                    results = userAdminClient.RoleFunctionSet(roleName, functionPathSet, mayView, mayEdit, comment, delete, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    UserAdminService.UserAdminServiceClient userAdminClient = new UserAdminService.UserAdminServiceClient();
                    results = userAdminClient.RoleFunctionSet(roleName, functionPathSet, mayView, mayEdit, comment, delete, userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Role Function Set Success"; }
                else
                { lblTestResults.Text = "Role Function Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Role Function Set Failed";
                throw;
            }
        }
        
        public void RoleSet(string roleId, string roleName, string comment, string sDelete, string bookGroup,
                            string functionPath, string userId, string userPassword )
        {   
            try
            {
                bool results = false;
                bool delete = false;
                if (sDelete.ToLower().Equals("yes"))
                { delete = true; }
                if (testEnv.Equals("Local"))
                {
                    LocalUserAdminService.UserAdminServiceClient userAdminClient = new LocalUserAdminService.UserAdminServiceClient();
                    results = userAdminClient.RoleSet(roleId, roleName, comment, delete, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    UserAdminService.UserAdminServiceClient userAdminClient = new UserAdminService.UserAdminServiceClient();
                    results = userAdminClient.RoleSet(roleId, roleName, comment, delete, userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "Role Set Success"; }
                else
                { lblTestResults.Text = "Role Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "Role Set Failed";
                throw;
            }
        }
        
        public void RolesGet(string roleName, string userId, string userPassword, string bookGroup, string functionPath)
        {   
            DataSet dsTemp = new DataSet();
            string tableName = "RoleNames";
            if (testEnv.Equals("Local"))
            {
                LocalUserAdminService.UserAdminServiceClient userAdminClient = new LocalUserAdminService.UserAdminServiceClient();
                byte[] strTemp = userAdminClient.RolesGet(roleName, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                UserAdminService.UserAdminServiceClient userAdminClient = new UserAdminService.UserAdminServiceClient();
                byte[] strTemp = userAdminClient.RolesGet(roleName, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        
        public void UserGet(string userIdGet, string userId, string userPassword, string bookGroup, string functionPath)
        {   
            DataSet dsTemp = new DataSet();
            string tableName = "Users";
            if (testEnv.Equals("Local"))
            {
                LocalUserAdminService.UserAdminServiceClient userAdminClient = new LocalUserAdminService.UserAdminServiceClient();
                byte[] strTemp = userAdminClient.UserGet(userIdGet, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                UserAdminService.UserAdminServiceClient userAdminClient = new UserAdminService.UserAdminServiceClient();
                byte[] strTemp = userAdminClient.UserGet(userIdGet, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }
        public void RoleFunctionsGet(string roleName, string functionPathGet, string sUtcOffSet, string userId, string userPassword, string bookGroup, string functionPath)
        {
            DataSet dsTemp = new DataSet();
            string tableName = "RoleFunctions";
            short utcOffSet = short.Parse(sUtcOffSet.ToString());
            if (testEnv.Equals("Local"))
            {
                LocalUserAdminService.UserAdminServiceClient userAdminClient = new LocalUserAdminService.UserAdminServiceClient();
                byte[] strTemp = userAdminClient.RoleFunctionsGet(roleName, functionPathGet, utcOffSet, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                UserAdminService.UserAdminServiceClient userAdminClient = new UserAdminService.UserAdminServiceClient();
                byte[] strTemp = userAdminClient.RoleFunctionsGet(roleName, functionPathGet, utcOffSet, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }

        public void UserRolesGet(string userIdGet, string roleName, string sUtcOffSet, string userId, string userPassword, string bookGroup, string functionPath)
        {    
        	
            DataSet dsTemp = new DataSet();
            string tableName = "UserRoles";
            short utcOffSet = short.Parse(sUtcOffSet.ToString());
            if (testEnv.Equals("Local"))
            {
                LocalUserAdminService.UserAdminServiceClient userAdminClient = new LocalUserAdminService.UserAdminServiceClient();
                byte[] strTemp = userAdminClient.UserRolesGet(userIdGet, roleName, utcOffSet, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            else
            {
                UserAdminService.UserAdminServiceClient userAdminClient = new UserAdminService.UserAdminServiceClient();
                byte[] strTemp = userAdminClient.UserRolesGet(userIdGet, roleName, utcOffSet, userId, userPassword, bookGroup, functionPath);
                dsTemp = ConvertToDataTable(strTemp, tableName);
            }
            this.c1TrueDBGrid1.ClearFields();
            this.c1TrueDBGrid1.DataSource = dsTemp;
            this.c1TrueDBGrid1.SetDataBinding(dsTemp, tableName);
            this.c1TrueDBGrid1.Update();
            Show();
        }

        public void UserRolesSet(string userIdSet, string roleName, string comment, string sDelete, string userId, string userPassword, string bookGroup, string functionPath)
        {   
            try
            {
                bool results = false;
                bool delete = false;
                if (sDelete.ToLower().Equals("yes"))
                { delete = true; }
                if (testEnv.Equals("Local"))
                {
                    LocalUserAdminService.UserAdminServiceClient userAdminClient = new LocalUserAdminService.UserAdminServiceClient();
                    results = userAdminClient.UserRolesSet(userIdSet, roleName, comment, delete, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    UserAdminService.UserAdminServiceClient userAdminClient = new UserAdminService.UserAdminServiceClient();
                    results = userAdminClient.UserRolesSet(userIdSet, roleName, comment, delete, userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "User Roles Set Success"; }
                else
                { lblTestResults.Text = "User Roles Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "User Roles Set Failed";
                throw;
            }
        }
        
        public void UserSet(string userIdSet, string shortName, string newPassword, string email, string title, string comment,
                 string sIsLocked, string sIsActive, string userId, string userPassword, string bookGroup, string functionPath)
        {   
            try
            {
                bool results = false;
                bool isLocked = false;
                if (sIsLocked.ToLower().Equals("yes"))
                { isLocked = true; }
                bool isActive = false;
                if (sIsActive.ToLower().Equals("yes"))
                { isActive = true; }
                if (testEnv.Equals("Local"))
                {
                    LocalUserAdminService.UserAdminServiceClient userAdminClient = new LocalUserAdminService.UserAdminServiceClient();
                    results = userAdminClient.UserSet(userIdSet, shortName, newPassword, email, title, comment, isLocked, isActive, userId, userPassword, bookGroup, functionPath);
                }
                else
                {
                    UserAdminService.UserAdminServiceClient userAdminClient = new UserAdminService.UserAdminServiceClient();
                    results = userAdminClient.UserSet(userIdSet, shortName, newPassword, email, title, comment, isLocked, isActive, userId, userPassword, bookGroup, functionPath);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "User Set Success"; }
                else
                { lblTestResults.Text = "User Set Failed"; }
            }
            catch
            {
                lblTestResults.Text = "User Set Failed";
                throw;
            }
        }

        private void userAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            try
            {
                bool results = false;
                string userId = txtUid.Text.ToString();
                string oldPwd = txtOldPwd.Text.Trim().ToString();
                string newPwd = txtNewPwd.Text.Trim().ToString();
                if (userId.Equals(""))
                {
                    MessageBox.Show("You must enter a valid user ID");
                }
                if (oldPwd.Equals(""))
                {
                    MessageBox.Show("You must enter a valid old password");
                }
                if (newPwd.Equals(""))
                {
                    MessageBox.Show("You must enter new password");
                }

                if (testEnv.Equals("Local"))
                {
                    LocalSecurityService.SecurityServiceClient securityClient = new LocalSecurityService.SecurityServiceClient();
                    results = securityClient.UserPasswordChange(userId, oldPwd, newPwd);
                }
                else
                {
                    SecurityService.SecurityServiceClient securityClient = new SecurityService.SecurityServiceClient();
                    results = securityClient.UserPasswordChange(userId, oldPwd, newPwd);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "User Password Change Required"; }
                else
                { lblTestResults.Text = "User Password Change NOT Required"; }
            }
            catch
            {
                lblTestResults.Text = "User Password Change Failed";
                throw;
            }

        }

        private void tasksGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.Refresh();
        }

        private void btnPwdReset_Click(object sender, EventArgs e)
        {
            try
            {
                bool results = false;
                string userId = txtUid.Text.ToString();
                string newPwd = "P3ns0n";
                if (userId.Equals(""))
                {
                    MessageBox.Show("You must enter a valid user ID");
                }

                if (testEnv.Equals("Local"))
                {
                    LocalSecurityService.SecurityServiceClient securityClient = new LocalSecurityService.SecurityServiceClient();
                    results = securityClient.UserPasswordReset(userId, newPwd);
                }
                else
                {
                    SecurityService.SecurityServiceClient securityClient = new SecurityService.SecurityServiceClient();
                    results = securityClient.UserPasswordReset(userId, newPwd);
                }
                if (results.Equals(true))
                { lblTestResults.Text = "User Password Change Required"; }
                else
                { lblTestResults.Text = "User Password Change NOT Required"; }
            }
            catch
            {
                lblTestResults.Text = "User Password Change Failed";
                throw;
            }

        }

        private void txtUid_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnVaidateUser_Click(object sender, EventArgs e)
        {
            try
            {
                string userId = txtUserToValidate.Text.ToString();
                string pwd = txtValidatePWD.Text.ToString();
                string sourceAddress = "fe80::a4cc:cfe0:64ac:3f43%12";
                int resetReq = 0;
                if (userId.Equals(""))
                {
                    MessageBox.Show("You must enter a valid user ID");
                }
                if (pwd.Equals(""))
                {
                    MessageBox.Show("You must enter a valid user password");
                }
                if (testEnv.Equals("Local"))
                {
                    LocalSecurityService.SecurityServiceClient securityClient = new LocalSecurityService.SecurityServiceClient();
                    resetReq = securityClient.UserValidate(userId, pwd, sourceAddress, resetReq);
                }
                else
                {
                    SecurityService.SecurityServiceClient securityClient = new SecurityService.SecurityServiceClient();
                    resetReq = securityClient.UserValidate(userId, pwd, sourceAddress, resetReq);
                }
                if (resetReq.Equals(0))
                { lblTestResults.Text = "User Is Valid"; }
                else
                { lblTestResults.Text = "User Is Valid. PWD Change Required"; }
            }
            catch
            {
                lblTestResults.Text = "User Is NOT Valid"; 
            }

        }




    }





        //BS; Add Next





}

