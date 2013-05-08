using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using StockLoan.Business;
using System.Configuration;

using System.Data.SqlClient;
using System.Data.SqlTypes;
using StockLoan.Encryption;
using StockLoan.Transport;


//using C1.Win.C1FlexGrid;

namespace StockLoanPlatformTester
{
    public partial class GetTesterForm : Form
    {
        public GetTesterForm()
        {
            InitializeComponent();

            //string strDate = "21-07-2006";
            //DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            //dtfi.ShortDatePattern = "dd-MM-yyyy";
            //dtfi.DateSeparator = "-";
            //DateTime objDate = Convert.ToDateTime(strDate, dtfi);
            //MessageBox.Show(objDate.ToShortDateString());
        }

        private void testAdminBusinessClassToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void testBooksClassToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void countriesGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();
            
            DataSet ds = new DataSet();

            ds = Admin.CountriesGet("US");
            
            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName) ;
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();
        }

        private void bookContactsSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {

                Books.BookContactSet("PFSL", "1234", "David", "Chen", "Tech", "214-953-3535", "", "Davids test", "BStone", true);

                DataSet ds = new DataSet();

                ds = Books.BookContactsGet("PFSL", "1234", 0);

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { }

        }

        private void GetTesterForm_Load(object sender, EventArgs e)
        {
            //DataSet ds = new DataSet();

            //ds = Functions.LogicOperatorsGet();

            string currentAddress = GetIpAddress();

            lblCurrentAddress.Text = "IP Address: " + currentAddress;

            this.c1TrueDBGrid1.AllowUpdate.Equals(true) ;

        }

        private string GetIpAddress()
        {
            string hostName = System.Net.Dns.GetHostName();
            string currAddress = "";
            System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostName);  //System.Net.Dns.GetHostByName (hostName);
            System.Net.IPAddress [] addr = ipEntry.AddressList;
          
            for (int i = 0; i < addr.Length; i++)
            {
                currAddress = (addr[i].ToString());
            }
            return currAddress;
        }

        private void currenciesGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Admin.CurrenciesGet("");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void countryCodeConversionsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {

            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Admin.CountryCodeIsoConversionsGet();

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void currencyConversionsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Admin.CurrencyConversionsGet("BSS");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void userRolesGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            //ds = DBSecurity.UserRolesGet("", "", 0);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void deliveryTypesGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Admin.DeliveryTypesGet();

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();
        }

        private void c1TrueDBGrid1_Click(object sender, EventArgs e)
        {

        }

        private void userValidateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();
            
            DataSet ds = new DataSet();
            int resetReq = 0;
            Security.UserValidate("DChen", "password", "78.10.99.99", resetReq);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void userRoleFunctionsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            //ds = DBSecurity.UserRoleFunctionsGet("", "", 0);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void bookDataLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Books.BooksGet("", "");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void bookClearingInstructionsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Books.BookClearingInstructionsGet("PFSI", "MER INT");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void bookContactsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Books.BookContactsGet("PFSL", "1234", 0);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void bookCreditLimitsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Books.BookCreditLimitsGet("2010-10-01", "PFSI", "PFSI", "", 0);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void bookFundsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Books.BookFundsGet("", "", "");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void bookGroupGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Books.BookGroupsGet("", "");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void contractBillingGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Contracts.ContractBillingsGet("09-29-2010");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void contractsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Contracts.ContractsGet("9-29-2010", "", "", "");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void contractDetailsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Contracts.ContractDetailsGet("2010-09-29","PFSI");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void contractSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Contracts.ContractsResearchGet("", "", "", "", "", "", "", "", "");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void dealGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Deals.DealsGet("", "", "", true, 0);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void fundsGetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Funds.FundsGet();

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void fundingRatesGetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Funds.FundingRatesGet("", 0);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void fundingRatesResearchGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Funds.FundingRateResearchGet("2010-09-28", "2010-09-30", "US0O/N", 0);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void inventoriesGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Inventory.InventoriesGet("", "", 0, "");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void inventoryControlsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Inventory.InventoryControlGet("");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void marksGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Marks.MarksGet("", "", "", "", 0);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void boxPositionGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Positions.BoxPositionGet("9/28/2010", "", "");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void recallsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Recalls.RecallsGet("2010-09-29", "", "", 0);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void returnsGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Returns.ReturnsGet("","" , "", "", 0);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void priceGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = SecMaster.PricesGet("", "", "");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void secMasterGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = SecMaster.SecMasterGet("", "", "", "", "");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void secIdSymbolLookupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            string SecId = "";
            string Symbol = "";
            string Sedol = "";
            string Isin = "";
            string Cusip = "";
            string IBES_Ticker = "";

            SecMaster.SecIdSymbolLookup("ABT", ref SecId, ref Symbol, ref Sedol, ref Isin, ref Cusip, ref IBES_Ticker);

            string message = "ABT Alias lookup Security ID = " + SecId + "; Symbol = " + Symbol + "; Sedol = " + Sedol + 
                    "; ISIN = " + Isin + "; CUSIP = " + Cusip + "; IBES Ticker = " + IBES_Ticker;

            MessageBox.Show(message);

        }

        private void holidaysGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

//            ds = Functions.HolidaysGet();

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void countrySetToolStripMenuItem_Click(object sender, EventArgs e)
        {

            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            Admin.CountrySet("WA", "Test for Dave", "3", true);

            ds = Admin.CountriesGet("");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void currencyConversionSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            Admin.currencyConversionSet("BSS", "USD", "99.99");

            DataSet ds = new DataSet();

            ds = Admin.CurrencyConversionsGet("BSS");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void currencySetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            Admin.CurrencySet("BSS", "DCH", true);

            DataSet ds = new DataSet();

            ds = Admin.CurrenciesGet("BSS");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void roleSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();
            try
            {
               // DBAdmin.RoleSet("TEST", "Only Testing", "This is a test of the DataAccess Layer", "BStone", false);

               DataSet ds = new DataSet();

                //ds = Security.UserRolesGet("", "", 0);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();
            }
            catch (Exception exc)
            {
                  
            }

            frm1.Close();

        }

        private void roleFunctionSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

           // Security.RoleFunctionSet("TEST", "AdminSecMaster", true, true, "Test", "This is a test by Bill Stone", "BStone"); 

            DataSet ds = new DataSet();

            //ds = Security.UserRoleFunctionsGet("", "", 0);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void bookClearingInstructionsSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                Books.BookClearingInstructionSet("PFSL", "MER INT", "YE", "AFA", "100.00", "David Chen Test ", "David's Security Instructions", "BStone", true);

                DataSet ds = new DataSet();


                ds = Books.BookClearingInstructionsGet("PFSL", "MER INT");

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { }
        }

        private void bookCreditLimitSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                Books.BookCreditLimitSet("2010-10-01", "PFSI", "PFSI", "PFSI", "BStone", "9999", "5555");

                DataSet ds = new DataSet();

                ds = Books.BookCreditLimitsGet("", "", "", "", 0);

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { }
        }

        private void bookFundSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                Books.BookFundSet("PFSI", "Test1", "DChen", "USD", "Test1", true);

                DataSet ds = new DataSet();

                ds = Books.BookFundsGet("PFSI", "Test1", "USD");

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { }
            
        }

        private void bookSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();
            try
            {
                Books.BookSet("PFSI", "14714", "14714", "David Chen Testing", "1700 Pacific", "Suite 1400", "", "214-953-3535", "999-9999", "", "", "", "", "", "", "", "", "US", "", "", "", "BStone", true);
                DataSet ds = new DataSet();

                ds = Books.BooksGet("", "");

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { }

        }

        private void testContractsClassToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contractSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();
            try
            {
                Contracts.ContractSet("2010-09-29", "PFSI", "2009100811284250", "L", "PFSI", "", "", "", "330", "660", "", "", "", "", "", "", "", "", "", true, true, "", "", "USD", "", "", "", "Test Comment", "", "", "", "", "", false, false, false);

                DataSet ds = new DataSet();

                ds = Contracts.ContractsGet("2010-09-29", "PFSI", "2009100811284250", "L");

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { }

        }

        private void dealSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();
            try
            {
                Deals.DealSet("2010092915043826", "PFSI", "B", "PFSI", "BStone", "", "", "66", "33", "", "", "", "", "", "", "", "", true, true, "", "", "USD", "", "", "Test Comment Update", "", "C", true, "BStone", false, "", "USD", "");

                DataSet ds = new DataSet();

                ds = Deals.DealsGet("2010092915043826", "", "", true, 0);

                //ds = Deals.DealsGet("2010092915043826", "20100929", "2010-09-29", true, 0);

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { }


        }

        private void fundingRateSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();
            try
            {
                Funds.FundingRateSet("2010-09-30", "US0O/N", "2.5", "BStone");
                
                DataSet ds = new DataSet();

                ds = Funds.FundingRatesGet("2010-09-30", 0);

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { }
        }

        private void inventorySetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();
            try
            {
                Inventory.InventorySet("2010-09-30", "Test2", "0002041", "7", "L", "7", 1);

                DataSet ds = new DataSet();

                ds = Inventory.InventoriesGet("Test2", "0002041", 0, "");

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { }

        }

        private void markSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                Marks.MarkSet("2010092916581982", "2010-09-29", "PFSI", "0123", "", "", "", "", "", "", "", "BStone", true);

                DataSet ds = new DataSet();

                ds = Marks.MarksGet("2010092916581982", "2010-09-29", "", "", 0);

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { }
        }

        private void markRetroSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                int recordsUpdated;

                //recordsUpdated = Marks.RetroMarkSet("2010-09-25", "2010-09-30", "PFSI", "PFSI", "2009100811284250", "L", ".99", "201009250809587", "F", "BStone");
                //MessageBox.Show("Records Updated = " + recordsUpdated);

                //DataSet ds = new DataSet();

                //ds = Marks.MarksGet("201009250809585", "2010-09-29", "", "", 0);

                //frm1.c1TrueDBGrid1.DataSource = ds;
                //string tblName = ds.Tables[0].ToString();

                //frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                //frm1.c1TrueDBGrid1.Update();
                //frm1.ShowDialog();

                //frm1.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }

        }

        private void recallSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();
            try
            {
                Recalls.RecallSet("201010190003", "2010-09-29", "PFSI", "20101019831001", "L", "PFSI", "123", "1", "2010-08-30", "OH", "O", "BStone",
                    "", "", "", "Another test record", true);

                DataSet ds = new DataSet();

                ds = Recalls.RecallsGet("2010-09-29", "201010190003", "", 0);

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { }

        }

        private void returnsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void returnSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();
            try
            {
                Returns.ReturnSet("201010190001", "2010-09-29", "PFSI", "PFSI", "2009042315223129", "L", "1", "BStone", "", "", true);

                DataSet ds = new DataSet();

                ds = Returns.ReturnsGet("201010190001", "2010-09-29", "PFSI", "2009042315223129", 0);

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { }

        }

        private void returnAsOfSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();
            try
            {
                Returns.ReturnAsOfSet("2010-09-29", "PFSI", "PFSI", "2010070712133133", "B", "2010093014462180", "6000", "BStone", "2010-09-30");

                DataSet ds = new DataSet();

                ds = Returns.ReturnsGet("2010093014462180", "2010-09-29", "PFSI", "2010070712133133", 0);

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { }


        }

        private void priceSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                SecMaster.PriceSet("9/29/2010", "123", "US", "USD", "9.99", "9/29/2010");

                DataSet ds = new DataSet();

                ds = SecMaster.PricesGet("9/29/2010", "123", "USD");

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }


        }

        private void secIdAliasSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                SecMaster.SecIdAliasSet("0004422", "2", "BStone-Test");

                DataSet ds = new DataSet();

                ds = SecMaster.SecMasterGet("0004422", "GB", "USD", "", "");

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }

        }

        private void secMasterSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                SecMaster.SecMasterSet("123", "Test by Bill Stone", "C", "EQ", "US", "USD", "", "", "", "", "BST", "", "", "", "", true);

                DataSet ds = new DataSet();

                ds = SecMaster.SecMasterGet("123", "US", "USD", "", "");

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }


        }

        private void holidaySetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
               // Functions.HolidaySet("0234", "2010-09-30", "US", "BStone", "Bill Stones Holiday", true, false, false, true);

                DataSet ds = new DataSet();

                bool isBizDate = false;
                bool isBank = false;
                bool isExchange = false;

              //  Functions.HolidaysGet("0234", "2010-09-30", "US", "Test", ref isBizDate, ref isBank, ref isExchange, 0);

                string message = "2010-09-30 isBizDate = " + isBizDate + "; Is Bank Holiday = " + isBank + "; Is Exchange Holiday = " + isExchange;

                MessageBox.Show(message);
                
              //  ds = Functions.HolidaysGet();

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();


            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }



        }

        private void keyValuesGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {

                DataSet ds = new DataSet();

                ds = Functions.KeyValuesGet();

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }



        }

        private void keyValuesSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                Functions.KeyValueSet("BizDate", "12/30/2010");

                string sKeyValue = "";

                Functions.KeyValuesGet("BizDate"); //, ref sKeyValue);

                MessageBox.Show(sKeyValue);
            }
            catch
            { }

        }

        private void logicOperatorGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                
                DataSet ds = new DataSet();

                ds = Functions.LogicOperatorsGet();

                frm1.c1TrueDBGrid1.DataSource = ds;
                string tblName = ds.Tables[0].ToString();

                frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
                frm1.c1TrueDBGrid1.Update();
                frm1.ShowDialog();

                frm1.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }



        }

        private void settlementSystemProcessSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                Functions.SettlementSystsemProcessSet("2010-09-29");

                MessageBox.Show("Settlement System Process Success");
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }


        }

        private void taskSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string date1 = "2010-09-21";
            string date2 = "2010-09-23";

            int results = 0;

            results = Functions.CompareDates(date1, date2);

            MessageBox.Show("Compare Dates results: Date1 = " + date1 + " Date2 = " + date2 + " Results = " + results.ToString());

        }

        private void tasksGetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("No tbTasks table in LoanStar Database");
        }

        private void connectionStringToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void holidaysGetReturnIndicatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            bool isBizDate = false;
            bool isBank = false;
            bool isExchange = false;

          //  Functions.HolidaysGet("0234", "2010-09-30", "US", "", ref isBank, ref isExchange, 0);

            string message = "2010-09-30 isBizDate = " + isBizDate + "; Is Bank Holiday = " + isBank + "; Is Exchange Holiday = " + isExchange;

            MessageBox.Show(message);

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {

                string keyValue = "";

                Functions.KeyValuesGet("BizDate"); //, ref keyValue);

                MessageBox.Show(keyValue);

            }
            catch
            { }


        }

        private void bookGroupRollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                MessageBox.Show("This will roll book groups information for BizDate 9/29/2010 to 9/30/2010");

                Books.BookGroupRoll("9/30/2010", "9/29/2010");

                MessageBox.Show("Current (Max) BizDate in tbBookGroups table should now be 9/30/2010");

            }
            catch
            { }


        }

        private void contractProcessRollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                MessageBox.Show("This will roll Contracts BizDate forward from 9/29/2010 to 9/30/2010");
                    
                int rowCount = 0;
    
                rowCount = Contracts.ContractBizDateProcessRoll("9/30/2010", "9/29/2010");

                MessageBox.Show(rowCount.ToString() + "Rows Updated.  Current (Max) BizDate in tbContracts table should now be 9/30/2010");

            }
            catch
            { }



        }

        private void contractSystemRollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                MessageBox.Show("This will roll Contracts BizDate forward from 9/29/2010 to 9/30/2010 AND Insert Returns records into tbReturns");

                int rowCount = 0;

                rowCount = Contracts.ContractBizDateSystemRoll("9/30/2010", "9/29/2010");

                MessageBox.Show(rowCount.ToString() + " Rows Updated. Current (Max) BizDate in tbContracts table should now be 9/30/2010");

            }
            catch
            { }


        }

        private void contractRateChangeAsOfSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();


            Contracts.ContractRateChangeAsOfSet("2010-09-28", "2010-09-29", "PFSL", "SEY UK", "2010092315012576", "5.25", "500.00", "BStone");

            ds = Contracts.ContractsGet("2010-09-29", "PFSL", "2010092315012576", "L");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void dealValidateDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();


            Deals.DealToContract("2010092713573666", "2010-09-27");

            ds = Deals.DealsGet("2010-09-27", "2010092713573666", "", true, 0);

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void testDealsClassToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fundingRatesRollToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            try
            {
                MessageBox.Show("This will roll FundingRates BizDate forward from 9/30/2010 to 10/01/2010");

                int rowCount = 0;

                Funds.FundingRatesRoll("10/01/2010", "9/30/2010");

                MessageBox.Show(rowCount.ToString() + "Rows Updated.  Current (Max) BizDate in tbFundingRates table should now be 10/01/2010");

            }
            catch
            { }



        }

        private void contractDistinctCurrenciesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet dsContracts = new DataSet();
      //BS; Get a dataset of Contracts
            dsContracts = Contracts.ContractsGet("2010-09-29", "", "", "");
            //dsContracts.Tables[0].Rows.Count;
     //BS; Use PFSI as the Book Group Name
            string bookGroup = "PFSI";

    //BS; Call Contracts.ContractDistinctCurrencies
            DataSet dsDistinctCurrency = new DataSet();

            dsDistinctCurrency = Contracts.ContractDistinctCurrencies(dsContracts, bookGroup);
            
            frm1.c1TrueDBGrid1.DataSource = dsDistinctCurrency;
            string tblName = dsDistinctCurrency.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(dsDistinctCurrency, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet dsContracts = new DataSet();
            //BS; Get a dataset of Contracts
            dsContracts = Contracts.ContractsGet("2010-09-29", "", "", "");
            //dsContracts.Tables[0].Rows.Count;
            //BS; Use PFSI as the Book Group Name
            string bookGroup = "PFSI";

            //BS; Call Contracts.ContractDistinctCurrencies
            DataSet dsContractSummary = new DataSet();

            dsContractSummary = Contracts.ContractSummary(dsContracts, bookGroup, false);

            frm1.c1TrueDBGrid1.DataSource = dsContractSummary;
            string tblName = dsContractSummary.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(dsContractSummary, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            
            DataSet dsContracts = new DataSet();
            DataSet dsBillings = new DataSet();

            //BS; Get a dataset of Contracts
            dsContracts = Contracts.ContractsGet("2010-09-29", "", "", "");
            //dsBillings = Contracts.ContractBillingsGet("09-29-2010");

            dsBillings = Contracts.ContractsSummaryByBillings("2010-09-29", "2010-09-28", "2010-09-29", "PFSI", "", "2009100810122635", "", "", "");

            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();


            frm1.c1TrueDBGrid1.DataSource = dsBillings;
            string tblName = dsBillings.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(dsBillings, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {
            DataSet dsContracts = new DataSet();
            DataSet dsBillings = new DataSet();

            //BS; Get a dataset of Contracts
            dsContracts = Contracts.ContractsGet("2010-09-29", "", "", "");
            //dsBillings = Contracts.ContractBillingsGet("09-29-2010");

            dsBillings = Contracts.ContractSummaryByBookCash(dsContracts, "PFSI");

            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();


            frm1.c1TrueDBGrid1.DataSource = dsBillings;
            string tblName = dsBillings.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(dsBillings, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void contractSummaryBySecurityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsContracts = new DataSet();
            DataSet dsSecSummary = new DataSet();

            //BS; Get a dataset of Contracts
            dsContracts = Contracts.ContractsGet("2010-09-29", "", "", "");
            //dsBillings = Contracts.ContractBillingsGet("09-29-2010");

            dsSecSummary = Contracts.ContractSummaryBySecurity(dsContracts, "PFSI", false);

            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();


            frm1.c1TrueDBGrid1.DataSource = dsSecSummary;
            string tblName = dsSecSummary.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(dsSecSummary, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void contractSummaryByHypothicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsContracts = new DataSet();
            DataSet dsSummary = new DataSet();

            //BS; Get a dataset of Contracts
            dsContracts = Contracts.ContractsResearchGet("2010-09-29", "", "", "", "", "", "", "", "");
            //dsBillings = Contracts.ContractBillingsGet("09-29-2010");
            dsContracts.Tables[0].TableName = "Contracts";

            dsSummary = Contracts.ContractSummaryByHypothication(dsContracts, "PFSI");

            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();


            frm1.c1TrueDBGrid1.DataSource = dsSummary;
            string tblName = dsSummary.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(dsSummary, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void contractSummaryByBookDebitsCreditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsSummary = new DataSet();

            dsSummary = Contracts.ContractSummaryByCreditsDebits("09-29-2010");

            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();


            frm1.c1TrueDBGrid1.DataSource = dsSummary;
            string tblName = dsSummary.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(dsSummary, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void testFunctionsClassToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void markIsExistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string markId = "2010092916581982";
            DataSet dsMarks = new DataSet();
            dsMarks = Marks.MarksGet(markId, "", "", "", 0);

            //DataSet dsTemp = new DataSet();
            bool results = false;

            results = Marks.MarkIsExist("PFSI", "NEW INT", "2010070708022037", "L", "0469742", "-75000.00", dsMarks);

            MessageBox.Show("Results are: Mark ID " + markId + " Exists = " + results.ToString());


        }

        private void marksSummaryByCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsSummary = new DataSet();

            DataSet dsMarks = new DataSet();

            dsMarks = Marks.MarksGet("", "9-29-2010", "", "", 0);

            dsSummary = Marks.MarksSummaryByCash(dsMarks);

            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();


            frm1.c1TrueDBGrid1.DataSource = dsSummary;
            string tblName = dsSummary.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(dsSummary, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void returnsSummaryByCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsSummary = new DataSet();

            DataSet dsReturns = new DataSet();
            DateTime startTime = DateTime.Now;

            dsReturns = Returns.ReturnsGet("", "2010-09-29", "PFSI", "", 0);

            dsSummary = Returns.ReturnsSummaryByCash(dsReturns);

            DateTime endTime = DateTime.Now;

            MessageBox.Show("Start Time: " + startTime.ToString() + " End Time: " + endTime.ToString());
            
            GetTesterForm frm1 = new GetTesterForm();

            frm1.c1TrueDBGrid1.ClearFields();

            frm1.c1TrueDBGrid1.DataSource = dsSummary;
            string tblName = dsSummary.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(dsSummary, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void contractSummaryByMarketValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Summary by Market Value came from Dash Business");
        }

        private void contractSummaryByBookProfitLossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dsContracts = new DataSet();
            DataSet dsSummary = new DataSet();

            //BS; Get a dataset of Contracts
            dsContracts = Contracts.ContractsResearchGet("2010-09-29", "", "", "", "", "", "", "", "");
            //dsBillings = Contracts.ContractBillingsGet("09-29-2010");
            dsContracts.Tables[0].TableName = "Contracts";

           // Contracts.ContractSummaryByBookProfitLoss(ref dsContracts, ref dsSummary, "PFSI");

            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();


            frm1.c1TrueDBGrid1.DataSource = dsContracts;
            string tblName = dsContracts.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(dsContracts, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();


        }

        private void contractSummaryByPriorBillingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetTesterForm frm1 = new GetTesterForm();
            frm1.c1TrueDBGrid1.ClearFields();

            DataSet ds = new DataSet();

            ds = Contracts.ContractBillingsGet("09-28-2010");

            frm1.c1TrueDBGrid1.DataSource = ds;
            string tblName = ds.Tables[0].ToString();

            frm1.c1TrueDBGrid1.SetDataBinding(ds, tblName);
            frm1.c1TrueDBGrid1.Update();
            frm1.ShowDialog();

            frm1.Close();

        }

        private void timeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string currentUtcTime = DateTime.UtcNow.ToString();
            string currentDateTime = DateTime.Now.ToString();

            MessageBox.Show("Current UTC Time = " + currentUtcTime + " Local Time = " + currentDateTime);

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text.ToString();

            string newEPassword = EncryptDecrypt.EncryptString(txtNewPassword.Text.ToString());
            string userId = txtUserId.Text.ToString();
            bool changed = false;

            changed = Security.UserPasswordReset(userId, newEPassword);
            MessageBox.Show("Changed = " + changed);

         }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text.ToString();

            string userId = txtUserId.Text.ToString();

            string newEPassword = EncryptDecrypt.EncryptString(txtNewPassword.Text.ToString());
            bool reset = false;

            reset = Security.UserPasswordChange(userId, oldPassword, newEPassword);
            MessageBox.Show("Changed = " + reset);

            Security.UserPasswordChange(userId, oldPassword, newEPassword);


        }

        private void sendEmailTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Transport.SendEMail("Test Send", "This is a test for sending an attachment", "BStone@penson.com", "bstone@penson.com", "dchen@penson.com;bstone@penson.com", "C:\\BStone\\AttachmentTest.txt");

        }


    }

  
   
    //        mainForm.SecId = SecIdLookupTextBox.Text;
	
                //    dsInventory = mainForm.InventoryAgent.InventoryGet("", SecIdLookupTextBox.Text, mainForm.UtcOffset);
                //    SecIdLookupGrid.SetDataBinding(dsInventory, "Inventory", true);

                //    SecIdLookupTextBox.Text = mainForm.SecId;
                //}
                //else if (DockingTab.SelectedIndex == 1)
                //{
                //    dsInventory = mainForm.InventoryAgent.InventoryGet(SecIdLookupTextBox.Text, "", mainForm.UtcOffset);
                //    DeskLookupGrid.SetDataBinding(dsInventory, "Inventory", true);





            
            
    //        string value;
    //        decimal number;
    //        // Parse an integer with thousands separators. 
    //        value = "16,523,421";
    //        number = Decimal.Parse(value);
    //        //MessageBox.Show(value + "Converted to: " + number);
    //        // Displays: 
    //        //    16,523,421' converted to 16523421.

    //        // Parse a floating point value with thousands separators
    //        value = "25,162.1378";
    //        number = Decimal.Parse(value);
    //        //MessageBox.Show(value + "Converted to: " + number);
    //        // Displays: 
    //        //    25,162.1378' converted to 25162.1378.

    //        // Parse a floating point value with no separators
    //        value = "25162.1378";
    //        number = Decimal.Parse(value);
    //        //MessageBox.Show(value + "Converted to: " + number);
    //        // Displays: 
    //        //    25162.1378' converted to 25162.1378.

            
    //        // Parse a floating point number with US currency symbol.
    //        value = "$16,321,421.75";
    //        try
    //        {
    //            number = Decimal.Parse(value);
    //            MessageBox.Show(value + "Converted to: " + number);
    //        }
    //        catch (FormatException)
    //        {
    //            MessageBox.Show("Unable to parse: " + value);
    //        }
    //        // Displays:
    //        //    Unable to parse '$16,321,421.75'.   

    //        // Parse a number in exponential notation
    //        value = "1.62345e-02";
    //        try
    //        {
    //            number = Decimal.Parse(value);
    //            MessageBox.Show(value + "Converted to: " + number);
    //        }
    //        catch (FormatException)
    //        {
    //            MessageBox.Show("Unable to parse: " + value);
    //        }
    //        // Displays: 
    //        //    Unable to parse '1.62345e-02'. 
        
    //    }
    }

