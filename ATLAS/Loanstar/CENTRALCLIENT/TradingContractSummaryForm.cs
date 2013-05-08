using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;
using StockLoan.Main;
using StockLoan.MainBusiness;

namespace CentralClient
{
    public struct TextBoxObj
    {
        private C1.Win.C1Input.C1TextBox textBox;
        private InformationType infoType;
        private SettlementType settleType;
        private ContractType contractType;

        public TextBoxObj(C1.Win.C1Input.C1TextBox textBox, InformationType infoType, SettlementType settleType, ContractType contractType)
        {
            this.textBox = textBox;
            this.infoType = infoType;
            this.settleType = settleType;
            this.contractType = contractType;
        }

        public C1.Win.C1Input.C1TextBox TextBox
        {
            get
            {
                return textBox;
            }

            set
            {
                textBox = value;
            }
        }

        public InformationType InfoType
        {
            get
            {
                return infoType;
            }
        }

        public SettlementType SettleType
        {
            get
            {
                return settleType;
            }
        }

        public ContractType ContractType
        {
            get
            {
                return contractType;
            }
        }
    }

    public partial class TradingContractSummaryForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private DataSet dsContractSummary;
        private DataSet dsContracts;
        private DataSet dsReturns;
        private DataSet dsBilling;
        private DataSet dsRecalls;
        private DataSet dsMarks;
        private DataSet dsSummary;
        private DataSet dsMarksSummary;
        private DataSet dsBookGroups;
        private DataSet dsBooks;
        private DataSet dsContractResearch;
        private DataSet dsLogicOperators;
        private DataSet dsCurrencyIso;
        private DataSet dsCountryCode;
        private DataSet dsDeliveryTypeCode;
        private DataSet dsContractsBizDatePrior;
        private DataSet dsTransactions;
        private DataSet dsCash;

        private DataView dvBorrows = null;
        private DataView dvLoans = null;
        private DataView dvReturns = null;
        private DataView dvBilling = null;
        private DataView dvRecalls = null;
        private DataView dvMarks = null;
        private DataView dvMarksSummary = null;
        private DataView dvSummary = null;
        private DataView dvContracts = null;
        private DataView dvContractSummary = null;
        private DataView dvBorrowDetails = null;
        private DataView dvLoanDetails = null;
        private DataView dvCreditDebitDetailNewContract = null;
        private DataView dvCreditDebitDetailMarks = null;
        private DataView dvCreditDebitDetailReturns = null;
        private DataView dvTransactions = null;
        private DataView dvCash = null;

        private MainForm mainForm = null;

        private ArrayList textBoxList;

        private System.Drawing.Color textBoxColorDefault;
        private System.Drawing.Color textBoxColorHighLight;

        private string secId = "";
        private string currencyIso = "";
        private string borrowContractId = "";
        private string loanContractId = "";
        private long lastRow = -1;

        private string Required = "Required";
        private string Optional = "Optional";
        private string Disabled = "Disabled";

        public TradingContractSummaryForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void TradingContractSummaryForm_Load(object sender, EventArgs e)
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
                dsBookGroups = mainForm.ServiceAgent.BookGroupGet(mainForm.UserId);

                BookGroupNameLabel.DataSource = dsBookGroups.Tables["BookGroups"];
                BookGroupNameLabel.DataField = "BookGroupName";

                BookGroupCombo.HoldFields();
                BookGroupCombo.DataSource = dsBookGroups.Tables["BookGroups"];
                BookGroupCombo.SelectedIndex = -1;

                if (dsBookGroups.Tables["BookGroups"].Rows.Count > 0)
                {
                    BookGroupCombo.SelectedIndex = 0;
                }

                DateTimePicker.Text = mainForm.ServiceAgent.BizDate();

                ContractSummaryDockingTab.SelectedIndex = 0;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void TradingContractSummaryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

            mainForm.tradingContractSummaryForm = null;
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                switch (ContractSummaryDockingTab.SelectedIndex)
                {

                    case 0:
                        ContractSummaryLoad();
                        break;

                    case 1:
                        BillingSummaryLoad();
                        break;

                    case 2:
                        ReturnSummaryLoad();
                        break;

                    case 3:
                        RecallSummaryLoad();
                        break;

                    case 4:
                        MarkSummaryLoad();
                        break;

                    case 5:
                        AsOfSummaryLoad();
                        break;

                    case 6:
                        break;

                    case 7:
                        break;

                    case 8:
                        break;

                    case 9:
                        TransactionsDailyLoad();
                        break;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void ContractSummary(string bookGroup)
        {
            Contracts.SummaryBySecurity(dsContracts, ref dsContractSummary, bookGroup, true);

        }

        private void ContractSummaryFooterLoad()
        {
            long borrowQuantity = 0;
            long loanQuantity = 0;

            decimal borrowRate = 0;
            decimal loanRate = 0;

            int borrowCounter = 1;
            int loanCounter = 1;

            try
            {
                foreach (DataRowView dr in dvContractSummary)
                {
                    borrowQuantity += (long)dr["BorrowQuantity"];
                    borrowRate += (decimal)dr["BorrowRate"];
                    borrowCounter++;

                    loanQuantity += (long)dr["LoanQuantity"];
                    loanRate += (decimal)dr["LoanRate"];
                    loanCounter++;
                }

                try
                {
                    borrowRate = borrowRate / borrowCounter;
                }
                catch { }

                try
                {
                    loanRate = loanRate / loanRate;
                }
                catch { }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            ContractSummaryGrid.Columns["BorrowQuantity"].FooterText = borrowQuantity.ToString("#,##0");
            ContractSummaryGrid.Columns["LoanQuantity"].FooterText = loanQuantity.ToString("#,##0");

            ContractSummaryGrid.Columns["BorrowRate"].FooterText = borrowRate.ToString("0.000");
            ContractSummaryGrid.Columns["LoanRate"].FooterText = loanRate.ToString("0.000");
        }

        public void ContractSummaryLoad()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsContracts = mainForm.PositionAgent.ContractDataGet(mainForm.UtcOffset, DateTimePicker.Text, "", "");
                dsCurrencyIso = mainForm.ServiceAgent.CurrenciesGet();

                ContractSummary(BookGroupCombo.Text);
                
                dvContractSummary = new DataView(dsContractSummary.Tables["ContractSummary"], "BookGroup = '" + BookGroupCombo.Text + "'", "", DataViewRowState.CurrentRows);

                dvBorrows = new DataView(dsContracts.Tables["Contracts"], "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B'", "ContractId DESC", DataViewRowState.CurrentRows);		
                dvLoans = new DataView(dsContracts.Tables["Contracts"], "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L'", "ContractId DESC", DataViewRowState.CurrentRows);		

                dvBorrowDetails = new DataView(dsContracts.Tables["Contracts"], "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B' AND ContractId = ''", "", DataViewRowState.CurrentRows);
                dvLoanDetails = new DataView(dsContracts.Tables["Contracts"], "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L' AND ContractId = ''", "", DataViewRowState.CurrentRows);

                ContractSummaryGrid.SetDataBinding(dvContractSummary, "", true);

                BorrowsGrid.SetDataBinding(dvBorrows, "", true);
                LoansGrid.SetDataBinding(dvLoans, "", true);

                BorrowsDetailGrid.SetDataBinding(dvBorrowDetails, "", true);
                LoansDetailsGrid.SetDataBinding(dvLoanDetails, "", true);

                ContractSummaryFooterLoad();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void ReturnSummaryLoad()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsReturns = mainForm.PositionAgent.ReturnsGet("", DateTimePicker.Text, "", "", mainForm.UtcOffset);

                dvReturns = new DataView(dsReturns.Tables["Returns"], "BookGroup = '" + BookGroupCombo.Text + "'", "ContractId DESC", DataViewRowState.CurrentRows);  //DC added Order-By parameter to flost new Returns to top of Grid

                ReturnsGrid.SetDataBinding(dvReturns, "", true);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void RecallSummaryLoad()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsRecalls = mainForm.PositionAgent.RecallsGet("", DateTimePicker.Text, "", mainForm.UtcOffset);

                dvRecalls = new DataView(dsRecalls.Tables["Recalls"], "BookGroup = '" + BookGroupCombo.Text + "'", "", DataViewRowState.CurrentRows);

                RecallsGrid.SetDataBinding(dvRecalls, "", true);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void BillingSummaryLoad()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsBilling = new DataSet();

                dsBilling = mainForm.PositionAgent.ContractBillingGet(DateTimePicker.Text);

                dvBilling = new DataView(dsBilling.Tables["Billing"], "BookGroup = '" + BookGroupCombo.Text + "'", "", DataViewRowState.CurrentRows);

                BillingGrid.SetDataBinding(dvBilling, "", true);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void MarkSummaryLoad()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsMarks = mainForm.PositionAgent.MarksGet("", DateTimePicker.Text, "", "", mainForm.UtcOffset);
                Marks.MarkSummary(dsMarks, ref dsMarksSummary, DateTimePicker.Text, "", Standard.DateFormat);

                dvMarksSummary = new DataView(dsMarksSummary.Tables["MarkSummary"], "BookGroup = '" + BookGroupCombo.Text + "'", "", DataViewRowState.CurrentRows);
                MarksSummaryGrid.SetDataBinding(dvMarksSummary, "", true);

                dvMarks = new DataView(dsMarks.Tables["Marks"], "BookGroup = '" + BookGroupCombo.Text + "' AND Book = '" + MarksSummaryGrid.Columns["Book"].Text + "' AND CurrencyIso = '" + MarksSummaryGrid.Columns["CurrencyIso"].Text + "'", "Symbol, SecId, ContractId", DataViewRowState.CurrentRows);		//DC added CurrencyIso to Where clause, added Order-By clause, to sum-up money with different currency, thus maintain consistency in sum-of-money between MarksSummaryGrid and MarksGrid. 
                MarksGrid.SetDataBinding(dvMarks, "", true);

                dsDeliveryTypeCode = mainForm.PositionAgent.DeliveryTypesGet();
                DeliveryTypeDropdown.SetDataBinding(dsDeliveryTypeCode, "DeliveryTypes", true);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void BookGroupCombo_TextChanged(object sender, EventArgs e)
        {
            switch (ContractSummaryDockingTab.SelectedIndex)
            {
                case 0:
                    ContractSummary(BookGroupCombo.Text);

                    if (dvContractSummary != null)
                    {
                        dvContractSummary.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
                    }

                    if (dvLoans != null)
                    {
                        dvLoans.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L'";
                    }

                    if (dvBorrows != null)
                    {
                        dvBorrows.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B'";
                    }
                    ContractSummaryFooterLoad();
                    break;

                case 1:
                    if (dvBilling != null)
                    {
                        dvBilling.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
                    }
                    break;

                case 2:

                    if (dvReturns != null)
                    {
                        dvReturns.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
                    }
                    break;

                case 3:

                    if (dvRecalls != null)
                    {
                        dvRecalls.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
                    }
                    break;

                case 4:

                    if (dvMarksSummary != null)
                    {
                        dvMarksSummary.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
                    }
                    break;

                case 7:
                    BookBalancingLoad();
                    break;

                case 8:
                    if (dvTransactions != null)
                    {
                        dvTransactions.RowFilter = "BookGroup ='" + BookGroupCombo.Text + "'";
                    }
                    break;


                case 9:
                    if (dvCash != null)
                    {
                        dvCash.RowFilter = "BookGroup ='" + BookGroupCombo.Text + "'";
                    }
                    break;
            }
        }

        private void BorrowsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Contracts.ValidateData((DataRowView)BorrowsGrid[BorrowsGrid.Row]);

                /*mainForm.PositionAgent.ContractSet(
                    BorrowsGrid.Columns["BizDate"].Text,
                    BorrowsGrid.Columns["BookGroup"].Text,
                    BorrowsGrid.Columns["ContractId"].Text,
                    BorrowsGrid.Columns["ContractType"].Text,
                    BorrowsGrid.Columns["Book"].Text,
                    "",
                    BorrowsGrid.Columns["Quantity"].Value.ToString(),
                    BorrowsGrid.Columns["QuantitySettled"].Value.ToString(),
                    BorrowsGrid.Columns["Amount"].Value.ToString(),
                    BorrowsGrid.Columns["AmountSettled"].Value.ToString(),
                    "",
                    BorrowsGrid.Columns["ValueDate"].Text,
                    BorrowsGrid.Columns["SettleDate"].Text,
                    BorrowsGrid.Columns["TermDate"].Text,
                    BorrowsGrid.Columns["Rate"].Text,
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    true,
                    BorrowsGrid.Columns["FeeAmount"].Text,
                    BorrowsGrid.Columns["FeeCurrencyIso"].Text,
                    BorrowsGrid.Columns["FeeType"].Text,
                    BorrowsGrid.Columns["Fund"].Text,
                    BorrowsGrid.Columns["TradeRefId"].Text);*/

                BorrowsGrid.Columns["IsActive"].Value = true;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
                e.Cancel = true;
            }

            this.Cursor = Cursors.Default;
        }

        private void BorrowsGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (e.KeyChar.Equals((char)13))
                {
                    BorrowsGrid.Update();
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void LoansGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                Contracts.ValidateData((DataRowView)LoansGrid[LoansGrid.Row]);

                mainForm.PositionAgent.ContractSet(
                    LoansGrid.Columns["BizDate"].Text,
                    LoansGrid.Columns["BookGroup"].Text,
                    LoansGrid.Columns["ContractId"].Text,
                    LoansGrid.Columns["ContractType"].Text,
                    LoansGrid.Columns["Book"].Text,
                    "",
                    LoansGrid.Columns["Quantity"].Value.ToString(),
                    LoansGrid.Columns["Quantity"].Value.ToString(),
                    LoansGrid.Columns["Amount"].Value.ToString(),
                    LoansGrid.Columns["Amount"].Value.ToString(),
                    "",
                    LoansGrid.Columns["ValueDate"].Text,
                    LoansGrid.Columns["SettleDate"].Text,
                    LoansGrid.Columns["TermDate"].Text,
                    LoansGrid.Columns["Rate"].Text,
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    true,
                    LoansGrid.Columns["FeeAmount"].Text,
                    LoansGrid.Columns["FeeCurrencyIso"].Text,
                    LoansGrid.Columns["FeeType"].Text,
                    LoansGrid.Columns["Fund"].Text,
                    LoansGrid.Columns["TradeRefId"].Text,
                     "",
                     "",
                     "",
                     "");

                LoansGrid.Columns["IsActive"].Value = true;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
                e.Cancel = true;
            }

            this.Cursor = Cursors.Default;
        }

        private void LoansGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (e.KeyChar.Equals((char)13))
                {
                    LoansGrid.Update();
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void ContractToReturnCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            string bookGroup;
            string book;
            string secId;
            string contractId;
            string contractType;

            long contractQuantity;

            try
            {
                if (BorrowsGrid.Focused)
                {
                    bookGroup = BorrowsGrid.Columns["BookGroup"].Text;
                    book = BorrowsGrid.Columns["Book"].Text;
                    secId = BorrowsGrid.Columns["SecId"].Text;
                    contractId = BorrowsGrid.Columns["ContractId"].Text;
                    contractType = BorrowsGrid.Columns["ContractType"].Text;
                    contractQuantity = (long)BorrowsGrid.Columns["Quantity"].Value;
                }
                else
                {
                    bookGroup = LoansGrid.Columns["BookGroup"].Text;
                    book = LoansGrid.Columns["Book"].Text;
                    secId = LoansGrid.Columns["SecId"].Text;
                    contractId = LoansGrid.Columns["ContractId"].Text;
                    contractType = LoansGrid.Columns["ContractType"].Text;
                    contractQuantity = (long)LoansGrid.Columns["Quantity"].Value;
                }

                TradingReturnInputForm tradingReturnInputForm = new TradingReturnInputForm(mainForm, bookGroup, book, secId, contractId, contractType, contractQuantity);
                tradingReturnInputForm.Show();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void ContractSummaryDockingTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (ContractSummaryDockingTab.SelectedIndex)
                {
                    case 0:
                        ContractSummaryLoad();
                        DateTimePicker.Enabled = true;
                        SubmitButton.Visible = false;
                        RecallToCommand.Enabled = false;

                        ApplyCommand.Enabled = true;
                        ContractToCommand.Enabled = true;
                        RecallToCommand.Enabled = true;
                        GenerateCOBCommand.Enabled = true;
                        break;

                    case 1:
                        BillingSummaryLoad();
                        DateTimePicker.Enabled = true;
                        SubmitButton.Visible = false;

                        ApplyCommand.Enabled = false;
                        ContractToCommand.Enabled = false;
                        RecallToCommand.Enabled = false;
                        GenerateCOBCommand.Enabled = false;
                        break;

                    case 2:
                        ReturnSummaryLoad();
                        DateTimePicker.Enabled = true;
                        SubmitButton.Visible = false;

                        ApplyCommand.Enabled = false;
                        ContractToCommand.Enabled = true;
                        RecallToCommand.Enabled = false;
                        GenerateCOBCommand.Enabled = false;
                        break;

                    case 3:
                        RecallSummaryLoad();
                        DateTimePicker.Enabled = true;
                        SubmitButton.Visible = false;

                        ApplyCommand.Enabled = false;
                        ContractToCommand.Enabled = false;
                        RecallToCommand.Enabled = false;
                        GenerateCOBCommand.Enabled = false;
                        break;

                    case 4:
                        MarkSummaryLoad();
                        DateTimePicker.Enabled = true;
                        SubmitButton.Visible = false;

                        ApplyCommand.Enabled = false;
                        ContractToCommand.Enabled = false;
                        RecallToCommand.Enabled = false;
                        GenerateCOBCommand.Enabled = false;
                        break;

                    case 5:
                        AsOfSummaryLoad();
                        DateTimePicker.Enabled = false;
                        SubmitButton.Visible = true;

                        ApplyCommand.Enabled = false;
                        ContractToCommand.Enabled = false;
                        RecallToCommand.Enabled = false;
                        GenerateCOBCommand.Enabled = false;
                        break;

                    case 6:
                        ContractResearchLoad();
                        DateTimePicker.Enabled = false;
                        SubmitButton.Visible = false;

                        ApplyCommand.Enabled = false;
                        ContractToCommand.Enabled = false;
                        RecallToCommand.Enabled = false;
                        GenerateCOBCommand.Enabled = false;
                        break;

                    case 7:
                        BookBalancingLoad();
                        DateTimePicker.Enabled = false;
                        SubmitButton.Visible = false;

                        ApplyCommand.Enabled = false;
                        ContractToCommand.Enabled = false;
                        RecallToCommand.Enabled = false;
                        GenerateCOBCommand.Enabled = false;
                        break;

                    case 8:
                        TransactionsDailyLoad();
                        DateTimePicker.Enabled = true;
                        SubmitButton.Enabled = false;
                        
                        ApplyCommand.Enabled = false;
                        ContractToCommand.Enabled = false;
                        RecallToCommand.Enabled = false;
                        GenerateCOBCommand.Enabled = false;
                        break;

                    case 9:
                        CashLoad();
                        DateTimePicker.Enabled = true;
                        SubmitButton.Enabled = false;

                        ApplyCommand.Enabled = false;
                        ContractToCommand.Enabled = false;
                        RecallToCommand.Enabled = false;
                        GenerateCOBCommand.Enabled = false;
                        break;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void TransactionsDailyLoad()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsTransactions = mainForm.PositionAgent.TransactionsDailiyGet(DateTimePicker.Text);
                dvTransactions = new DataView(dsTransactions.Tables["Transactions"], "BookGroup = '" + BookGroupCombo.Text + "'", "", DataViewRowState.CurrentRows);
                TransactionsGrid.SetDataBinding(dvTransactions, "", true);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void CashLoad()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsCash = mainForm.PositionAgent.CashGet("", "", mainForm.UtcOffset);
                dvCash = new DataView(dsCash.Tables["Cash"], "BookGroup = '" + BookGroupCombo.Text + "'", "", DataViewRowState.CurrentRows);
                CashGrid.SetDataBinding(dvCash, "", true);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void AsOfSummaryLoad()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsBooks = mainForm.AdminAgent.BookGet(BookGroupCombo.Text, "");
                dsCountryCode = mainForm.ServiceAgent.CountriesGet("");
                dsCurrencyIso = mainForm.ServiceAgent.CurrenciesGet();
                dsDeliveryTypeCode = mainForm.PositionAgent.DeliveryTypesGet();

                AsOfBookCombo.HoldFields();
                AsOfBookCombo.DataSource = dsBooks.Tables["Books"];
                AsOfBookCombo.SelectedIndex = -1;

                ContractAsOfBookCombo.HoldFields();
                ContractAsOfBookCombo.DataSource = dsBooks.Tables["Books"];
                ContractAsOfBookCombo.SelectedIndex = -1;

                ContractAsOfCashDepotCombo.HoldFields();
                ContractAsOfCashDepotCombo.DataSource = dsCountryCode.Tables["Countries"];
                ContractAsOfCashDepotCombo.SelectedIndex = -1;

                ContractAsOfSecurityDepotCombo.HoldFields();
                ContractAsOfSecurityDepotCombo.DataSource = dsCountryCode.Tables["Countries"];
                ContractAsOfSecurityDepotCombo.SelectedIndex = -1;

                ContractAsOfCurrencyIsoCombo.HoldFields();
                ContractAsOfCurrencyIsoCombo.DataSource = dsCurrencyIso.Tables["Currencies"];
                ContractAsOfCurrencyIsoCombo.SelectedIndex = -1;

                MarkAsOfDeliveryTypeCombo.HoldFields();
                MarkAsOfDeliveryTypeCombo.DataSource = dsDeliveryTypeCode.Tables["DeliveryTypes"];
                MarkAsOfDeliveryTypeCombo.SelectedIndex = -1;

            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void Grid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            e.Value = mainForm.Format(e.Column.DataField, e.Value);
        }

        private void ReturnsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (!ReturnsGrid.Columns["SettleDateActual"].Text.Equals(ReturnsGrid.Columns[""].Text))
                {
                    if ((!ReturnsGrid.Columns["SettleDateActual"].Text.Equals(ReturnsGrid.Columns["BizDate"].Text)) &&
                        (!ReturnsGrid.Columns["SettleDateProjected"].Text.Equals("")))
                    {
                        throw new Exception("Settle Date Actual cannot be before or after the current business date.");
                    }
                }

                mainForm.PositionAgent.ReturnSet(
                    ReturnsGrid.Columns["ReturnId"].Text,
                    ReturnsGrid.Columns["BookGroup"].Text,
                    ReturnsGrid.Columns["Book"].Text,
                    ReturnsGrid.Columns["ContractId"].Text,
                    ReturnsGrid.Columns["ContractType"].Text,
                    ReturnsGrid.Columns["Quantity"].Value.ToString(),
                    ReturnsGrid.Columns["SettleDateProjected"].Text,
                    ReturnsGrid.Columns["SettleDateActual"].Text,
                    mainForm.UserId,
                    true);

                ReturnsGrid.Columns["ActUserId"].Text = mainForm.UserId;
                ReturnsGrid.Columns["ActTime"].Value = DateTime.Now.ToString();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void ReturnsGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (e.KeyChar.Equals((char)13))
                {
                    ReturnsGrid.Update();
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void BorrowsGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            if (!bool.Parse(BorrowsGrid.Columns["IsActive"].CellText(e.Row)))
            {
                e.CellStyle.ForeColor = System.Drawing.Color.LightGray;
            }
            else if (BorrowsGrid.Columns["ValueDate"].CellText(e.Row).ToString().Equals(mainForm.ServiceAgent.BizDate()))
            {
                e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
            }
            else if (long.Parse(BorrowsGrid.Columns["QuantityReturned"].CellText(e.Row)) > 0)
            {
                e.CellStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            }
        }

        private void LoansGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            if (!bool.Parse(LoansGrid.Columns["IsActive"].CellText(e.Row)))
            {
                e.CellStyle.ForeColor = System.Drawing.Color.LightGray;
            }
            else if (LoansGrid.Columns["ValueDate"].CellText(e.Row).ToString().Equals(mainForm.ServiceAgent.BizDate()))		//DC 
            {
                e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
            }
            else if (long.Parse(LoansGrid.Columns["QuantityReturned"].CellText(e.Row)) > 0)
            {
                e.CellStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            }
        }

        private void BorrowsGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {

        }

        private void LoansGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {

        }

        private void ReturnsGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            if (!bool.Parse(ReturnsGrid.Columns["IsActive"].CellText(e.Row)))
            {
                e.CellStyle.ForeColor = System.Drawing.Color.LightGray;
            }
            else if (ReturnsGrid.Columns["ReturnId"].CellText(e.Row).Substring(0, 8).Equals(mainForm.ServiceAgent.BizDate().Replace("-", "")))
            {
                e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
            }

        }

        private void ReturnsGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mainForm.PositionAgent.ReturnSet(
                    ReturnsGrid.Columns["ReturnId"].Text,
                    ReturnsGrid.Columns["BookGroup"].Text,
                    ReturnsGrid.Columns["Book"].Text,
                    ReturnsGrid.Columns["ContractId"].Text,
                    ReturnsGrid.Columns["ContractType"].Text,
                    ReturnsGrid.Columns["Quantity"].Value.ToString(),
                    ReturnsGrid.Columns["SettleDateProjected"].Text,
                    ReturnsGrid.Columns["SettleDateActual"].Text,
                    mainForm.UserId,
                    false);


                ReturnsGrid.Columns["IsActive"].Value = false;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void BillingGrid_FetchCellStyle(object sender, C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs e)
        {
            try
            {
                switch (e.Column.DataColumn.DataField)
                {
                    case ("CashInBalance"):
                    case ("CashInRecieveable"):
                    case ("CashOutBalance"):
                    case ("CashOutPayable"):
                    case ("CashNet"):
                    case ("CashNetReceivePayable"):
                        if ((decimal)BillingGrid.Columns[e.Column.DataColumn.DataField].CellValue(e.Row) >= 0)
                        {
                            e.CellStyle.ForeColor = Color.Navy;
                            break;
                        }
                        else
                        {
                            e.CellStyle.ForeColor = Color.Red;
                        }
                        break;
                }
            }
            catch { }
        }

        private void ContractToRecallCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            string bookGroup;
            string book;
            string secId;
            string contractId;
            string contractType;

            long contractQuantity;

            try
            {
                bookGroup = LoansGrid.Columns["BookGroup"].Text;
                book = LoansGrid.Columns["Book"].Text;
                secId = LoansGrid.Columns["SecId"].Text;
                contractId = LoansGrid.Columns["ContractId"].Text;
                contractQuantity = (long)LoansGrid.Columns["Quantity"].Value;
                contractType = "L";

                TradingRecallInputForm tradingRecallInputForm = new TradingRecallInputForm(mainForm, bookGroup, book, secId, contractId, contractType, contractQuantity);
                tradingRecallInputForm.Show();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void RecallsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mainForm.PositionAgent.RecallSet(
                    RecallsGrid.Columns["RecallId"].Text,
                    RecallsGrid.Columns["BookGroup"].Text,
                    RecallsGrid.Columns["ContractId"].Text,
                    RecallsGrid.Columns["ContractType"].Text,
                    RecallsGrid.Columns["Book"].Text,
                    RecallsGrid.Columns["SecId"].Text,
                    RecallsGrid.Columns["Quantity"].Value.ToString(),
                    RecallsGrid.Columns["OpenDateTime"].Text,
                    RecallsGrid.Columns["MoveToDate"].Text,
                    RecallsGrid.Columns["BuyInDate"].Text,
                    RecallsGrid.Columns["ReasonCode"].Text,
                    "",
                    RecallsGrid.Columns["Comment"].Text,
                    RecallsGrid.Columns["Status"].Text,
                    mainForm.UserId,
                    true);

                RecallsGrid.Columns["ActUserId"].Text = mainForm.UserId;
                RecallsGrid.Columns["ActTime"].Value = DateTime.Now.ToString();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void RecallsGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mainForm.PositionAgent.RecallSet(
                    RecallsGrid.Columns["RecallId"].Text,
                    RecallsGrid.Columns["BookGroup"].Text,
                    RecallsGrid.Columns["ContractId"].Text,
                    RecallsGrid.Columns["ContractType"].Text,
                    RecallsGrid.Columns["Book"].Text,
                    RecallsGrid.Columns["SecId"].Text,
                    RecallsGrid.Columns["Quantity"].Value.ToString(),
                    RecallsGrid.Columns["OpenDateTime"].Text,
                    RecallsGrid.Columns["MoveToDate"].Text,
                    RecallsGrid.Columns["BuyInDate"].Text,
                    RecallsGrid.Columns["ReasonCode"].Text,
                    "",
                    RecallsGrid.Columns["Comment"].Text,
                    RecallsGrid.Columns["Status"].Text,
                    mainForm.UserId,
                    false);

                RecallsGrid.Columns["ActUserId"].Text = mainForm.UserId;
                RecallsGrid.Columns["ActTime"].Value = DateTime.Now.ToString();
                RecallsGrid.Columns["IsActive"].Value = false;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void RecallsGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            if (!bool.Parse(RecallsGrid.Columns["IsActive"].CellText(e.Row)))
            {
                e.CellStyle.ForeColor = System.Drawing.Color.LightGray;
            }
        }

        private void RecallsGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (e.KeyChar.Equals((char)13))
                {
                    RecallsGrid.Update();
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void RecallToBuyInCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mainForm.PositionAgent.RecallSet(
                    RecallsGrid.Columns["RecallId"].Text,
                    RecallsGrid.Columns["BookGroup"].Text,
                    RecallsGrid.Columns["ContractId"].Text,
                    RecallsGrid.Columns["ContractType"].Text,
                    RecallsGrid.Columns["Book"].Text,
                    RecallsGrid.Columns["SecId"].Text,
                    RecallsGrid.Columns["Quantity"].Value.ToString(),
                    RecallsGrid.Columns["OpenDateTime"].Text,
                    RecallsGrid.Columns["MoveToDate"].Text,
                    RecallsGrid.Columns["BuyInDate"].Text,
                    RecallsGrid.Columns["ReasonCode"].Text,
                    "",
                    RecallsGrid.Columns["Comment"].Text,
                    "B",
                    mainForm.UserId,
                    true);

                RecallsGrid.Columns["ActUserId"].Text = mainForm.UserId;
                RecallsGrid.Columns["ActTime"].Value = DateTime.Now.ToString();
                RecallsGrid.Columns["Status"].Value = "B";
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void RecallToMoveCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mainForm.PositionAgent.RecallSet(
                    RecallsGrid.Columns["RecallId"].Text,
                    RecallsGrid.Columns["BookGroup"].Text,
                    RecallsGrid.Columns["ContractId"].Text,
                    RecallsGrid.Columns["ContractType"].Text,
                    RecallsGrid.Columns["Book"].Text,
                    RecallsGrid.Columns["SecId"].Text,
                    RecallsGrid.Columns["Quantity"].Value.ToString(),
                    RecallsGrid.Columns["OpenDateTime"].Text,
                    RecallsGrid.Columns["MoveToDate"].Text,
                    RecallsGrid.Columns["BuyInDate"].Text,
                    RecallsGrid.Columns["ReasonCode"].Text,
                    "",
                    RecallsGrid.Columns["Comment"].Text,
                    "M",
                    mainForm.UserId,
                    true);

                RecallsGrid.Columns["ActUserId"].Text = mainForm.UserId;
                RecallsGrid.Columns["ActTime"].Value = DateTime.Now.ToString();
                RecallsGrid.Columns["Status"].Value = "M";
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void RecallToCloseCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mainForm.PositionAgent.RecallSet(
                    RecallsGrid.Columns["RecallId"].Text,
                    RecallsGrid.Columns["BookGroup"].Text,
                    RecallsGrid.Columns["ContractId"].Text,
                    RecallsGrid.Columns["ContractType"].Text,
                    RecallsGrid.Columns["Book"].Text,
                    RecallsGrid.Columns["SecId"].Text,
                    RecallsGrid.Columns["Quantity"].Value.ToString(),
                    RecallsGrid.Columns["OpenDateTime"].Text,
                    RecallsGrid.Columns["MoveToDate"].Text,
                    RecallsGrid.Columns["BuyInDate"].Text,
                    RecallsGrid.Columns["ReasonCode"].Text,
                    "",
                    RecallsGrid.Columns["Comment"].Text,
                    "C",
                    mainForm.UserId,
                    true);

                RecallsGrid.Columns["ActUserId"].Text = mainForm.UserId;
                RecallsGrid.Columns["ActTime"].Value = DateTime.Now.ToString();
                RecallsGrid.Columns["Status"].Value = "C";
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void ApplyMarksCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                TradingContractsMarksForm tradingContractsMarksForm = new TradingContractsMarksForm(mainForm);
                tradingContractsMarksForm.Show();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void MarksGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mainForm.PositionAgent.MarkSet(
                    MarksGrid.Columns["MarkId"].Text,
                    MarksGrid.Columns["BookGroup"].Text,
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    MarksGrid.Columns["SettleDate"].Text,
                    MarksGrid.Columns["DeliveryCode"].Text,
                    mainForm.UserId,
                    true);

                MarksGrid.Columns["ActUserId"].Text = mainForm.UserId;
                MarksGrid.Columns["ActTime"].Value = DateTime.Now.ToString();

            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void MarksGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            if (!bool.Parse(MarksGrid.Columns["IsActive"].CellText(e.Row)))
            {
                e.CellStyle.ForeColor = System.Drawing.Color.LightGray;
            }
        }

        private void MarksGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
         
            if (DateTimePicker.Text != mainForm.ServiceAgent.BizDate())
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            try
            {
                mainForm.PositionAgent.MarkSet(
                    MarksGrid.Columns["MarkId"].Text,                    
                    MarksGrid.Columns["BookGroup"].Text,
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    MarksGrid.Columns["SettleDate"].Text,
                    MarksGrid.Columns["DeliveryCode"].Text,
                    mainForm.UserId,
                    false);

                MarksGrid.Columns["ActUserId"].Text = mainForm.UserId;				
                MarksGrid.Columns["ActTime"].Value = DateTime.Now.ToString();		
                MarksGrid.Columns["IsActive"].Value = false;						
                
                dsContractsBizDatePrior = mainForm.PositionAgent.ContractResearchDataGet(
                                                                mainForm.ServiceAgent.BizDatePrior(),
                                                                MarksGrid.Columns["BookGroup"].Text,
                                                                MarksSummaryGrid.Columns["Book"].Text,
                                                                MarksGrid.Columns["ContractId"].Text,
                                                                MarksGrid.Columns["SecId"].Text,
                                                                "", "");
                if (dsContractsBizDatePrior != null)
                {
                    mainForm.PositionAgent.ContractSet(
                                    mainForm.ServiceAgent.BizDate(),
                                    MarksGrid.Columns["BookGroup"].Text,
                                    MarksGrid.Columns["ContractId"].Text,
                                    MarksGrid.Columns["ContractType"].Text,
                                    MarksSummaryGrid.Columns["Book"].Text,
                                    MarksGrid.Columns["SecId"].Text,
                                    "",
                                    "",
                                    dsContractsBizDatePrior.Tables["Contracts"].Rows[0]["Amount"].ToString(),
                                    dsContractsBizDatePrior.Tables["Contracts"].Rows[0]["AmountSettled"].ToString(),
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    true,				// IsActive 
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                     "",
                            "",
                            "",
                            "");
                }	//DC


            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void MarksSummaryGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
        {
            try
            {
                if (dvMarksSummary != null)
                {
                    dvMarks.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND Book = '" + MarksSummaryGrid.Columns["Book"].Text + "' AND CurrencyIso = '" + MarksSummaryGrid.Columns["CurrencyIso"].Text + "'";
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void MarksSummaryGrid_FetchCellStyle(object sender, C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs e)
        {
            try
            {
                switch (e.Column.DataColumn.DataField)
                {
                    case ("Amount"):
                    case ("SettledCash"):
                    case ("UnsettledCash"):
                        if ((decimal)MarksSummaryGrid.Columns[e.Column.DataColumn.DataField].CellValue(e.Row) >= 0)
                        {
                            e.CellStyle.ForeColor = Color.Navy;
                            break;
                        }
                        else
                        {
                            e.CellStyle.ForeColor = Color.Red;
                        }
                        break;
                }
            }
            catch { }
        }

        private void MarksGrid_FetchCellStyle(object sender, C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs e)
        {
            try
            {
                switch (e.Column.DataColumn.DataField)
                {
                    case ("Amount"):
                        if ((decimal)MarksGrid.Columns[e.Column.DataColumn.DataField].CellValue(e.Row) >= 0)
                        {
                            e.CellStyle.ForeColor = Color.Navy;
                            break;
                        }
                        else
                        {
                            e.CellStyle.ForeColor = Color.Red;
                        }
                        break;
                }
            }
            catch { }
        }

        private void BorrowsGrid_FetchCellStyle(object sender, C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs e)
        {
            try
            {
                switch (e.Column.DataColumn.DataField)
                {
                    case ("Amount"):
                        if ((decimal)BorrowsGrid.Columns[e.Column.DataColumn.DataField].CellValue(e.Row) >= 0)
                        {
                            e.CellStyle.ForeColor = Color.Navy;
                            break;
                        }
                        else
                        {
                            e.CellStyle.ForeColor = Color.Red;
                        }
                        break;
                }
            }
            catch { }
        }

        private void LoansGrid_FetchCellStyle(object sender, C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs e)
        {
            try
            {
                switch (e.Column.DataColumn.DataField)
                {
                    case ("Amount"):
                        if ((decimal)LoansGrid.Columns[e.Column.DataColumn.DataField].CellValue(e.Row) >= 0)
                        {
                            e.CellStyle.ForeColor = Color.Navy;
                            break;
                        }
                        else
                        {
                            e.CellStyle.ForeColor = Color.Red;
                        }
                        break;
                }
            }
            catch { }
        }

        private void SendToClipboardCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                switch (ContractSummaryDockingTab.SelectedIndex)
                {
                    case 0:
                        if (BorrowsGrid.Focused)
                        {
                            mainForm.SendToClipboard(ref BorrowsGrid);
                        }
                        else if (LoansGrid.Focused)
                        {
                            mainForm.SendToClipboard(ref LoansGrid);
                        }
                        break;

                    case 1:
                        mainForm.SendToClipboard(ref BillingGrid);
                        break;

                    case 2:
                        mainForm.SendToClipboard(ref ReturnsGrid);
                        break;

                    case 3:
                        mainForm.SendToClipboard(ref RecallsGrid);
                        break;

                    case 4:
                        if (MarksSummaryGrid.Focused)
                        {
                            mainForm.SendToClipboard(ref MarksSummaryGrid);
                        }
                        else if (MarksGrid.Focused)
                        {
                            mainForm.SendToClipboard(ref MarksGrid);
                        }
                        break;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                switch (ContractSummaryDockingTab.SelectedIndex)
                {
                    case 0:
                        if (BorrowsGrid.Focused)
                        {
                            mainForm.SendToExcel(ref BorrowsGrid, false);
                        }
                        else if (LoansGrid.Focused)
                        {
                            mainForm.SendToExcel(ref LoansGrid, false);
                        }
                        break;

                    case 1:
                        mainForm.SendToExcel(ref BillingGrid, false);
                        break;

                    case 2:
                        mainForm.SendToExcel(ref ReturnsGrid, false);
                        break;

                    case 3:
                        mainForm.SendToExcel(ref RecallsGrid, false);
                        break;

                    case 4:
                        if (MarksSummaryGrid.Focused)
                        {
                            mainForm.SendToExcel(ref MarksSummaryGrid, false);
                        }
                        else if (MarksGrid.Focused)
                        {
                            mainForm.SendToExcel(ref MarksGrid, false);
                        }
                        break;

                    case 8:
                        mainForm.SendToExcel(ref TransactionsGrid, false);
                        break;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void SendToEmailCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                switch (ContractSummaryDockingTab.SelectedIndex)
                {
                    case 0:
                        if (BorrowsGrid.Focused)
                        {
                            mainForm.SendToEmail(ref BorrowsGrid);
                        }
                        else if (LoansGrid.Focused)
                        {
                            mainForm.SendToEmail(ref LoansGrid);
                        }
                        break;

                    case 1:
                        mainForm.SendToEmail(ref BillingGrid);
                        break;

                    case 2:
                        mainForm.SendToEmail(ref ReturnsGrid);
                        break;

                    case 3:
                        mainForm.SendToEmail(ref RecallsGrid);
                        break;

                    case 4:
                        if (MarksSummaryGrid.Focused)
                        {
                            mainForm.SendToEmail(ref MarksSummaryGrid);
                        }
                        else if (MarksGrid.Focused)
                        {
                            mainForm.SendToEmail(ref MarksGrid);
                        }
                        break;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

        }

        private void ContractSummaryGrid_Paint(object sender, PaintEventArgs e)
        {
            if (ContractSummaryGrid.Visible)
            {
                if (!secId.Equals(ContractSummaryGrid.Columns["SecId"].Text) || !currencyIso.Equals(ContractSummaryGrid.Columns["CurrencyIso"].Text))
                {
                    secId = ContractSummaryGrid.Columns["SecId"].Text;
                    currencyIso = ContractSummaryGrid.Columns["CurrencyIso"].Text;

                    try
                    {
                        if (dvBorrows != null)
                        {
                            dvBorrows.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND SecId = '" + secId + "' AND CurrencyIso = '" + currencyIso + "' AND ContractType = 'B'";
                        }

                        if (dvLoans != null)
                        {
                            dvLoans.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND SecId = '" + secId + "' AND CurrencyIso = '" + currencyIso + "' AND ContractType = 'L'";
                        }
                    }
                    catch (Exception error)
                    {
                        mainForm.Alert(this.Name, error.Message);
                    }
                }
            }
            else
            {
                try
                {
                    if (dvBorrows != null)
                    {
                        dvBorrows.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B'";
                    }

                    if (dvLoans != null)
                    {
                        dvLoans.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L'";
                    }
                }
                catch (Exception error)
                {
                    mainForm.Alert(this.Name, error.Message);
                }
            }
        }

        private void ContractResearchLoad()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsMarks = null;
                dsContracts = null;
                dsReturns = null;

                dsBooks = mainForm.AdminAgent.BookGet(BookGroupCombo.Text, "");
                dsLogicOperators = mainForm.PositionAgent.LogicOperatorsGet();

                BookCombo.HoldFields();
                BookCombo.DataSource = dsBooks.Tables["Books"];
                BookCombo.SelectedIndex = -1;

                LogicOperatorCombo.HoldFields();
                LogicOperatorCombo.DataSource = dsLogicOperators.Tables["LogicOperators"];
                LogicOperatorCombo.SelectedIndex = -1;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsContractResearch = new DataSet();

                dsContractResearch = mainForm.PositionAgent.ContractResearchDataGet(
                    "",
                    BookGroupCombo.Text,
                    BookCombo.Text,
                    ContractIdTextBox.Text,
                    SecIdTextBox.Text,
                    AmountTextBox.Text,
                    LogicOperatorCombo.Text);

                ResearchContractsGrid.SetDataBinding(dsContractResearch, "Contracts", true);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void ResearchContractsGrid_Paint(object sender, PaintEventArgs e)
        {
            if (ResearchContractsGrid.Row != lastRow)
            {
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    if (!ResearchContractsGrid.Columns["BizDate"].Text.Equals(""))
                    {
                        dsReturns = mainForm.PositionAgent.ReturnsGet("", ResearchContractsGrid.Columns["BizDate"].Text, BookGroupCombo.Text, ResearchContractsGrid.Columns["ContractId"].Text, mainForm.UtcOffset);
                        dsMarks = mainForm.PositionAgent.MarksGet("", ResearchContractsGrid.Columns["BizDate"].Text, BookGroupCombo.Text, ResearchContractsGrid.Columns["ContractId"].Text, mainForm.UtcOffset);

                        ResearchReturnsGrid.SetDataBinding(dsReturns, "Returns", true);
                        ResearchMarksGrid.SetDataBinding(dsMarks, "Marks", true);
                    }
                }
                catch (Exception error)
                {
                    mainForm.Alert(this.Name, error.Message);
                }

                this.Cursor = Cursors.Default;

                lastRow = ResearchContractsGrid.Row;
            }
        }

        private void RateChangeAsOfSet()
        {
            this.Cursor = Cursors.WaitCursor;

            string message = "";

            try
            {
                message = mainForm.PositionAgent.RateChangeAsOfSet(
                    StartDateEdit.Text,
                    EndDateEdit.Text,
                    BookGroupCombo.Text,
                    AsOfBookCombo.Text,
                    AsOfContractIdTextBox.Text,
                    OldRateTextBox.Text,
                    NewRateTextBox.Text,
                    mainForm.UserId);

                RateChangeAsOfStatusTextBox.Text = "Records Updated : " + message;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
                RateChangeAsOfStatusTextBox.Text = error.Message;
            }

            this.Cursor = Cursors.Default;
        }

        private bool RateChangeAsOfErrorCheck()
        {
            if (AsOfContractIdTextBox.Text.Equals(""))
            {
                RateChangeAsOfStatusTextBox.Text = "Contract ID required.";
            }
            else if (StartDateEdit.Text.Equals(""))
            {
                RateChangeAsOfStatusTextBox.Text = "Start Date required.";
            }
            else if (EndDateEdit.Text.Equals(""))
            {
                RateChangeAsOfStatusTextBox.Text = "End Date required.";
            }
            else if (OldRateTextBox.Text.Equals(""))
            {
                RateChangeAsOfStatusTextBox.Text = "Old Rate required.";
            }
            else if (NewRateTextBox.Text.Equals(""))
            {
                RateChangeAsOfStatusTextBox.Text = "New Rate required.";
            }
            else
            {
                return true;
            }

            return false;
        }

        private void ContractAsOfSet()
        {
            this.Cursor = Cursors.WaitCursor;

            string message = "";
            DataSet dsContracts = new DataSet();

            try
            {
                message = mainForm.PositionAgent.ContractAsOfSet(
                    BookGroupCombo.Text,
                    "",
                    (ContractAsOfBorrowRadio.Checked) ? "B" : "L",
                    ContractAsOfBookCombo.Text,
                    ContractAsOfSecIdTextBox.Text,
                    ContractAsOfQuantityTextBox.Text,
                    ContractAsOfQuantityTextBox.Text,
                    ((long.Parse(ContractAsOfQuantityTextBox.Text) * (decimal.Parse(ContractAsOfPriceTextBox.Text)) * decimal.Parse(ContractAsOfMarginTextBox.Text))).ToString(),
                    ((long.Parse(ContractAsOfQuantityTextBox.Text) * (decimal.Parse(ContractAsOfPriceTextBox.Text)) * decimal.Parse(ContractAsOfMarginTextBox.Text))).ToString(),
                    "C",
                    ContractAsOfTradeDateEdit.Text,
                    ContractAsOfSettleDateEdit.Text,
                    ContractAsOfTermDateEdit.Text,
                    ContractAsOfRateTextBox.Text,
                    "",
                    "",
                    "",
                    "100.0",
                    false.ToString(),
                    false.ToString(),
                    "%",
                    ContractAsOfMarginTextBox.Text,
                    ContractAsOfCurrencyIsoCombo.Text,
                    ContractAsOfSecurityDepotCombo.Text,
                    ContractAsOfCashDepotCombo.Text,
                    " ",
                    "",
                    true,
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "", 
                    "");

                dsContracts = mainForm.PositionAgent.ContractResearchDataGet("", BookGroupCombo.Text, BookCombo.Text, message, "", "", "");

                AsOfContractGrid.SetDataBinding(dsContracts, "Contracts", true);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
                ContractAsOfStatusTextBox.Text = error.Message;
            }

            this.Cursor = Cursors.Default;
        }

        private bool ContractAsOfErrorCheck()
        {
            if (ContractAsOfBookCombo.Text.Equals(""))
            {
                ContractAsOfStatusTextBox.Text = "Book required.";
            }
            else if (ContractAsOfSecIdTextBox.Text.Equals(""))
            {
                ContractAsOfStatusTextBox.Text = "Security ID required.";
            }
            else if (ContractAsOfQuantityTextBox.Text.Equals(""))
            {
                ContractAsOfStatusTextBox.Text = "Quantity required.";
            }
            else if (ContractAsOfPriceTextBox.Text.Equals(""))
            {
                ContractAsOfStatusTextBox.Text = "Price required.";
            }
            else if (ContractAsOfTradeDateEdit.Text.Equals(""))
            {
                ContractAsOfStatusTextBox.Text = "Trade Date required.";
            }
            else if (ContractAsOfCurrencyIsoCombo.Text.Equals(""))
            {
                ContractAsOfStatusTextBox.Text = "Currency Iso required.";
            }
            else if (ContractAsOfCashDepotCombo.Text.Equals(""))
            {
                ContractAsOfStatusTextBox.Text = "Cash Depot required.";
            }
            else if (ContractAsOfSecurityDepotCombo.Text.Equals(""))
            {
                ContractAsOfStatusTextBox.Text = "Security Depot required.";
            }
            else if (ContractAsOfRateTextBox.Text.Equals(""))
            {
                ContractAsOfStatusTextBox.Text = "Rate required.";
            }
            else if (ContractAsOfMarginTextBox.Text.Equals(""))
            {
                ContractAsOfMarginTextBox.Text = "Margin required.";
            }
            else
            {
                return true;
            }

            return false;
        }

        private void AsOfFunctionsDockingTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (AsOfFunctionsDockingTab.SelectedIndex)
                {
                    case 0:
                        AsOfBookCombo.Enabled = true;
                        AsOfContractIdTextBox.Enabled = true;
                        BookRequiredLabel.Text = Optional;
                        ContractIdRequiredLabel.Text = Optional;
                        break;

                    case 1:
                        AsOfBookCombo.Enabled = true;
                        AsOfContractIdTextBox.Enabled = true;
                        BookRequiredLabel.Text = Required;
                        ContractIdRequiredLabel.Text = Required;
                        break;

                    case 2:
                        AsOfBookCombo.Enabled = true;
                        AsOfContractIdTextBox.Enabled = true;
                        BookRequiredLabel.Text = Required;
                        ContractIdRequiredLabel.Text = Required;
                        MarkAsOfPriceTextBox.CustomFormat = Formats.Price;
                        break;

                    case 3:
                        AsOfBookCombo.Enabled = false;
                        AsOfContractIdTextBox.Enabled = false;
                        BookRequiredLabel.Text = Disabled;
                        ContractIdRequiredLabel.Text = Disabled;
                        break;

                    case 4:
                        AsOfBookCombo.Enabled = false;
                        AsOfContractIdTextBox.Enabled = false;
                        BookRequiredLabel.Text = Disabled;
                        ContractIdRequiredLabel.Text = Disabled;
                        break;

                    case 5:
                        AsOfBookCombo.Enabled = false;
                        AsOfContractIdTextBox.Enabled = false;
                        BookRequiredLabel.Text = Disabled;
                        ContractIdRequiredLabel.Text = Disabled;
                        break;

                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void MarkAsOfSet()
        {
            this.Cursor = Cursors.WaitCursor;

            string message = "";

            try
            {
                message = mainForm.PositionAgent.MarkAsOfSet(
                    MarkAsOfOpenDateEdit.Text,
                    MarkAsOfSettleDateEdit.Text,
                    BookGroupCombo.Text,
                    AsOfBookCombo.Text,
                    AsOfContractIdTextBox.Text,
                    (MarkAsOfBorrowRadio.Checked) ? "B" : "L",
                    MarkAsOfPriceTextBox.Text,
                    "",
                    MarkAsOfDeliveryTypeCombo.Text,
                    mainForm.UserId);

                dsContracts = mainForm.PositionAgent.ContractResearchDataGet("", BookGroupCombo.Text, BookCombo.Text, AsOfContractIdTextBox.Text, "", "", "");

                AsOfContractGrid.SetDataBinding(dsContracts, "Contracts", true);

                MarkAsOfStatusTextBox.Text = "Records Updated : " + message;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
                MarkAsOfStatusTextBox.Text = error.Message;
            }

            this.Cursor = Cursors.Default;
        }

        private bool MarkAsOfErrorCheck()
        {
            if (AsOfBookCombo.Text.Equals(""))
            {
                MarkAsOfStatusTextBox.Text = "Book required.";
            }
            else if (AsOfContractIdTextBox.Text.Equals(""))
            {
                MarkAsOfStatusTextBox.Text = "Contract ID required.";
            }
            else if (MarkAsOfOpenDateEdit.Text.Equals(""))
            {
                MarkAsOfStatusTextBox.Text = "Open Date required.";
            }
            else if (MarkAsOfSettleDateEdit.Text.Equals(""))
            {
                MarkAsOfStatusTextBox.Text = "Settle Date required.";
            }
            else if (MarkAsOfPriceTextBox.Text.Equals(""))
            {
                MarkAsOfStatusTextBox.Text = "Price required.";
            }
            else
            {
                return true;
            }

            return false;
        }

        private void BookBalancingLoad()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsContracts = mainForm.PositionAgent.ContractResearchDataGet(mainForm.ServiceAgent.BizDatePrior(), BookGroupCombo.Text, "", "", "", "", "");
                dsReturns = mainForm.PositionAgent.ReturnsGet("", mainForm.ServiceAgent.BizDate(), BookGroupCombo.Text, "", mainForm.UtcOffset);
                dsMarks = mainForm.PositionAgent.MarksGet("", mainForm.ServiceAgent.BizDate(), BookGroupCombo.Text, "", mainForm.UtcOffset);
                dsRecalls = mainForm.PositionAgent.RecallsGet("", mainForm.ServiceAgent.BizDate(), BookGroupCombo.Text, mainForm.UtcOffset);

                Contracts.DistinctCurrencies(dsContracts, ref dsCurrencyIso, BookGroupCombo.Text);

                BalancingCurrencyIsoCombo.HoldFields();
                BalancingCurrencyIsoCombo.DataSource = dsCurrencyIso.Tables["CurrencyIso"];
                BalancingCurrencyIsoCombo.SelectedIndex = -1;

                textBoxList = new ArrayList();
                textBoxList.Add(new TextBoxObj(BorrowsContractsPendSettleTextBox, InformationType.Contracts, SettlementType.Pending, ContractType.Borrows));
                textBoxList.Add(new TextBoxObj(BorrowsContractsSettledTextBox, InformationType.Contracts, SettlementType.Settled, ContractType.Borrows));
                textBoxList.Add(new TextBoxObj(LoansContractsPendSettleTextBox, InformationType.Contracts, SettlementType.Pending, ContractType.Loans));
                textBoxList.Add(new TextBoxObj(LoansContractsSettledTextBox, InformationType.Contracts, SettlementType.Settled, ContractType.Loans));

                textBoxList.Add(new TextBoxObj(BorrowsMarksPendSettleTextBox, InformationType.Marks, SettlementType.Pending, ContractType.Borrows));
                textBoxList.Add(new TextBoxObj(BorrowsMarksSettledTextBox, InformationType.Marks, SettlementType.Settled, ContractType.Borrows));
                textBoxList.Add(new TextBoxObj(LoansMarksPendSettleTextBox, InformationType.Marks, SettlementType.Pending, ContractType.Loans));
                textBoxList.Add(new TextBoxObj(LoansMarksSettledTextBox, InformationType.Marks, SettlementType.Settled, ContractType.Loans));

                textBoxList.Add(new TextBoxObj(BorrowsReturnsPendSettleTextBox, InformationType.Returns, SettlementType.Pending, ContractType.Borrows));
                textBoxList.Add(new TextBoxObj(BorrowsReturnsSettledTextBox, InformationType.Returns, SettlementType.SettledToday, ContractType.Borrows));
                textBoxList.Add(new TextBoxObj(LoansReturnsPendSettleTextBox, InformationType.Returns, SettlementType.Pending, ContractType.Loans));
                textBoxList.Add(new TextBoxObj(LoansReturnsSettledTextBox, InformationType.Returns, SettlementType.SettledToday, ContractType.Loans));

                textBoxList.Add(new TextBoxObj(BorrowsRecallsPendSettleTextBox, InformationType.Recalls, SettlementType.Pending, ContractType.Borrows));
                textBoxList.Add(new TextBoxObj(BorrowsRecallsSettledTextBox, InformationType.Recalls, SettlementType.Settled, ContractType.Borrows));
                textBoxList.Add(new TextBoxObj(LoansRecallsPendSettleTextBox, InformationType.Recalls, SettlementType.Pending, ContractType.Loans));
                textBoxList.Add(new TextBoxObj(LoansRecallsSettledTextBox, InformationType.Recalls, SettlementType.Settled, ContractType.Loans));

                textBoxColorDefault = BorrowsContractsPendSettleTextBox.BackColor;
                textBoxColorHighLight = System.Drawing.Color.LightBlue;

                InfoGridLoad(InformationType.None, SettlementType.None, ContractType.None);
                HighLightTextBox("");
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void BookBalanceContractLoad()
        {
            decimal borrowPendContract = 0.0M;
            decimal LoanPendContract = 0.0M;

            decimal borrowSettleContract = 0.0M;
            decimal loanSettleContract = 0.0M;


            try
            {
                foreach (DataRow drContract in dsContracts.Tables["Contracts"].Rows)
                {
                    if (drContract["CurrencyIso"].ToString().Equals(BalancingCurrencyIsoCombo.Text))
                    {
                        if (drContract["SettleDate"].ToString().Equals(""))
                        {
                            switch (drContract["ContractType"].ToString())
                            {
                                case "B":
                                    borrowPendContract += decimal.Parse(drContract["Amount"].ToString());
                                    break;

                                case "L":
                                    LoanPendContract += decimal.Parse(drContract["Amount"].ToString());
                                    break;
                            }
                        }
                        else
                        {
                            switch (drContract["ContractType"].ToString())
                            {
                                case "B":
                                    borrowSettleContract += decimal.Parse(drContract["Amount"].ToString());
                                    break;

                                case "L":
                                    loanSettleContract += decimal.Parse(drContract["Amount"].ToString());
                                    break;
                            }
                        }
                    }
                }

            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            BorrowsContractsPendSettleTextBox.Value = borrowPendContract * -1;
            LoansContractsPendSettleTextBox.Value = LoanPendContract;

            BorrowsContractsSettledTextBox.Value = borrowSettleContract * -1;
            LoansContractsSettledTextBox.Value = loanSettleContract;
        }

        private void BookBalanceReturnLoad()
        {
            decimal borrowPendReturn = 0.0M;
            decimal loanPendReturn = 0.0M;

            decimal borrowSettleReturn = 0.0M;
            decimal loanSettleReturn = 0.0M;

            try
            {
                foreach (DataRow drReturn in dsReturns.Tables["Returns"].Rows)
                {
                    if (drReturn["CurrencyIso"].ToString().Equals(BalancingCurrencyIsoCombo.Text))
                    {
                        if (drReturn["SettleDateActual"].ToString().Equals(mainForm.ServiceAgent.BizDate()))
                        {
                            switch (drReturn["ContractType"].ToString())
                            {
                                case "B":
                                    borrowSettleReturn += decimal.Parse(drReturn["CashReturn"].ToString());
                                    break;

                                case "L":
                                    loanSettleReturn += decimal.Parse(drReturn["CashReturn"].ToString());
                                    break;
                            }
                        }
                        else
                        {
                            switch (drReturn["ContractType"].ToString())
                            {
                                case "B":
                                    borrowPendReturn += decimal.Parse(drReturn["CashReturn"].ToString());
                                    break;

                                case "L":
                                    loanPendReturn += decimal.Parse(drReturn["CashReturn"].ToString());
                                    break;
                            }
                        }
                    }
                }

            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            BorrowsReturnsPendSettleTextBox.Value = borrowPendReturn * -1;
            LoansReturnsPendSettleTextBox.Value = loanPendReturn;

            BorrowsReturnsSettledTextBox.Value = borrowSettleReturn * -1;
            LoansReturnsSettledTextBox.Value = loanSettleReturn;
        }

        private void BookBalancesMarkLoad()
        {
            decimal borrowPendMark = 0.0M;
            decimal loanPendMark = 0.0M;

            decimal borrowSettleMark = 0.0M;
            decimal loanSettleMark = 0.0M;

            try
            {
                foreach (DataRow drMark in dsMarks.Tables["Marks"].Rows)
                {
                    if (drMark["CurrencyIso"].ToString().Equals(BalancingCurrencyIsoCombo.Text))
                    {
                        if (drMark["OpenDate"].ToString().Equals(mainForm.ServiceAgent.BizDate()))
                        {
                            switch (drMark["ContractType"].ToString())
                            {
                                case "B":
                                    borrowSettleMark += decimal.Parse(drMark["Amount"].ToString());
                                    break;

                                case "L":
                                    loanSettleMark += decimal.Parse(drMark["Amount"].ToString());
                                    break;
                            }
                        }
                        else
                        {
                            switch (drMark["ContractType"].ToString())
                            {
                                case "B":
                                    borrowPendMark += decimal.Parse(drMark["Amount"].ToString());
                                    break;

                                case "L":
                                    loanPendMark += decimal.Parse(drMark["Amount"].ToString());
                                    break;
                            }
                        }
                    }
                }

            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            BorrowsMarksPendSettleTextBox.Value = borrowPendMark * -1;
            LoansMarksPendSettleTextBox.Value = loanPendMark;

            BorrowsMarksSettledTextBox.Value = borrowSettleMark * -1;
            LoansMarksSettledTextBox.Value = loanSettleMark;
        }

        private void BookBalancesRecallsLoad()
        {
            BorrowsRecallsPendSettleTextBox.Value = 0;
            LoansRecallsPendSettleTextBox.Value = 0;

            BorrowsRecallsSettledTextBox.Value = 0;
            LoansRecallsSettledTextBox.Value = 0;
        }

        private void BookBalancesBookTotalLoad()
        {
            decimal borrowsPend = 0;
            decimal loansPend = 0;

            decimal borrowsSettle = 0;
            decimal loansSettle = 0;

            try
            {
                borrowsPend += decimal.Parse(BorrowsContractsPendSettleTextBox.Value.ToString());
                loansPend += decimal.Parse(LoansContractsPendSettleTextBox.Value.ToString());
            }
            catch { }

            try
            {
                borrowsSettle += decimal.Parse(BorrowsContractsSettledTextBox.Value.ToString());
                loansSettle += decimal.Parse(LoansContractsSettledTextBox.Value.ToString());
            }
            catch { }

            try
            {
                borrowsPend += decimal.Parse(BorrowsReturnsPendSettleTextBox.Value.ToString());
                loansPend += decimal.Parse(LoansReturnsPendSettleTextBox.Value.ToString());
            }
            catch { }

            try
            {
                borrowsSettle += decimal.Parse(BorrowsReturnsSettledTextBox.Value.ToString());
                loansSettle += decimal.Parse(LoansReturnsSettledTextBox.Value.ToString());
            }
            catch { }


            try
            {
                borrowsPend += decimal.Parse(BorrowsMarksPendSettleTextBox.Value.ToString());
                loansPend += decimal.Parse(LoansMarksPendSettleTextBox.Value.ToString());
            }
            catch { }

            try
            {
                borrowsSettle += decimal.Parse(BorrowsMarksSettledTextBox.Value.ToString());
                loansSettle += decimal.Parse(LoansMarksSettledTextBox.Value.ToString());
            }
            catch { }


            BorrowsTotalPendSettleTextBox.Value = borrowsPend;
            LoansTotalPendSettleTextBox.Value = loansPend;

            BorrowsTotalSettledTextBox.Value = borrowsSettle;
            LoansTotalSettledTextBox.Value = loansSettle;

            BookTotalPendSettleTextBox.Value = borrowsPend + loansPend;
            BookTotalSettledTextBox.Value = borrowsSettle + loansSettle;
        }

        private void BalancingCurrencyIsoCombo_TextChanged(object sender, EventArgs e)
        {
            InfoGridLoad(InformationType.None, SettlementType.None, ContractType.None);
            HighLightTextBox("");

            BookBalanceContractLoad();
            BookBalanceReturnLoad();
            BookBalancesMarkLoad();
            BookBalancesRecallsLoad();
            BookBalancesBookTotalLoad();
        }

        private void DecimalTextBox_Formatting(object sender, C1.Win.C1Input.FormatEventArgs e)
        {
            try
            {
                e.Text = decimal.Parse(e.Value.ToString()).ToString("#,##0.00");
            }
            catch { }
        }

        private void ReturnAsOfSet()
        {
            this.Cursor = Cursors.WaitCursor;

            string message = "";

            try
            {
                message = mainForm.PositionAgent.ReturnAsOfSet(
                    ReturnAsOfTradeDateEdit.Text,
                    ReturnAsOfSettleDateEdit.Text,
                    BookGroupCombo.Text,
                    AsOfBookCombo.Text,
                    AsOfContractIdTextBox.Text,
                    (ReturnAsOfBorrowRadio.Checked) ? "B" : "L",
                    ReturnAsOfQuantityTextBox.Text,
                    "",
                    mainForm.UserId);

                dsContracts = mainForm.PositionAgent.ContractResearchDataGet("", BookGroupCombo.Text, BookCombo.Text, AsOfContractIdTextBox.Text, "", "", "");

                AsOfContractGrid.SetDataBinding(dsContracts, "Contracts", true);

                ReturnAsOfStatusTextBox.Text = "Records Updated : " + message;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
                ReturnAsOfStatusTextBox.Text = error.Message;
            }

            this.Cursor = Cursors.Default;
        }

        private bool ReturnAsOfErrorCheck()
        {
            if (AsOfBookCombo.Text.Equals(""))
            {
                ReturnAsOfStatusTextBox.Text = "Book required.";
            }
            else if (AsOfContractIdTextBox.Text.Equals(""))
            {
                ReturnAsOfStatusTextBox.Text = "Contract ID required.";
            }
            else if (ReturnAsOfTradeDateEdit.Text.Equals(""))
            {
                ReturnAsOfStatusTextBox.Text = "Trade Date required.";
            }
            else if (ReturnAsOfQuantityTextBox.Text.Equals(""))
            {
                ReturnAsOfStatusTextBox.Text = "Quantity required.";
            }
            else
            {
                return true;
            }

            return false;
        }

        private void HighLightTextBox_Click(object sender, EventArgs e)
        {
            HighLightTextBox(((C1.Win.C1Input.C1TextBox)sender).Name);
        }

        private void InfoGridLoad(InformationType infoType, SettlementType settleType, ContractType contractType)
        {
            this.Cursor = Cursors.WaitCursor;

            DataSet dsInfo = new DataSet();
            DataView dvInfo;
            string rowFilterInfo = "";
            string settlementColumnName = "";

            try
            {
                switch (infoType)
                {
                    case InformationType.Contracts:
                        dsInfo = mainForm.PositionAgent.ContractResearchDataGet(mainForm.ServiceAgent.BizDatePrior(), BookGroupCombo.Text, "", "", "", "", "");
                        settlementColumnName = "SettleDate";
                        InfoGrid.LoadLayout(System.Environment.CurrentDirectory + @"\Layouts\contract-summary.xml");
                        break;

                    case InformationType.Marks:
                        dsInfo = mainForm.PositionAgent.MarksGet("", mainForm.ServiceAgent.BizDate(), BookGroupCombo.Text, "", mainForm.UtcOffset);
                        settlementColumnName = "SettleDate";
                        InfoGrid.LoadLayout(System.Environment.CurrentDirectory + @"\Layouts\mark-summary.xml");
                        break;

                    case InformationType.Returns:
                        dsInfo = mainForm.PositionAgent.ReturnsGet("", mainForm.ServiceAgent.BizDate(), BookGroupCombo.Text, "", mainForm.UtcOffset);
                        settlementColumnName = "SettleDateActual";
                        InfoGrid.LoadLayout(System.Environment.CurrentDirectory + @"\Layouts\return-summary.xml");
                        break;

                    case InformationType.Recalls:
                        dsInfo = mainForm.PositionAgent.RecallsGet("", mainForm.ServiceAgent.BizDate(), BookGroupCombo.Text, mainForm.UtcOffset);
                        settlementColumnName = "OpenDateTime";
                        InfoGrid.LoadLayout(System.Environment.CurrentDirectory + @"\Layouts\recall-summary.xml");
                        break;

                    case InformationType.None:
                        InfoGrid.Splits[0].Caption = "";
                        InfoGrid.ClearFields();
                        break;
                }

                rowFilterInfo = "BookGroup = '" + BookGroupCombo.Text + "'";

                switch (settleType)
                {
                    case SettlementType.Pending:
                        rowFilterInfo += " AND " + settlementColumnName + " IS NULL";
                        break;

                    case SettlementType.Settled:
                        rowFilterInfo += " AND " + settlementColumnName + " IS NOT NULL";
                        break;

                    case SettlementType.SettledToday:
                        rowFilterInfo += " AND " + settlementColumnName + " = '" + mainForm.ServiceAgent.BizDate() + "'";
                        break;

                }

                switch (contractType)
                {
                    case ContractType.Loans:
                        rowFilterInfo += " AND ContractType = 'L'";
                        break;
                    case ContractType.Borrows:
                        rowFilterInfo += " AND ContractType = 'B'";
                        break;
                }

                if (infoType != InformationType.None)
                {
                    rowFilterInfo += " AND CurrencyIso = '" + BalancingCurrencyIsoCombo.Text + "'";
                    dvInfo = new DataView(dsInfo.Tables[0], rowFilterInfo, "", DataViewRowState.CurrentRows);
                    InfoGrid.SetDataBinding(dvInfo, "", true);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void HighLightTextBox(string name)
        {
            TextBoxObj tempBox;

            foreach (object tempObj in textBoxList)
            {
                tempBox = (TextBoxObj)tempObj;

                if (tempBox.TextBox.Name.Equals(name))
                {
                    tempBox.TextBox.BackColor = textBoxColorHighLight;
                    InfoGridLoad(tempBox.InfoType, tempBox.SettleType, tempBox.ContractType);
                }
                else
                {
                    tempBox.TextBox.BackColor = textBoxColorDefault;
                }
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            switch (AsOfFunctionsDockingTab.SelectedIndex)
            {
                case 0:
                    if (RateChangeAsOfErrorCheck())
                    {
                        RateChangeAsOfSet();
                    }
                    break;

                case 1:
                    if (ReturnAsOfErrorCheck())
                    {
                        ReturnAsOfSet();
                    }
                    break;

                case 2:
                    if (MarkAsOfErrorCheck())
                    {
                        MarkAsOfSet();
                    }
                    break;

                case 3:
                    //MarginAsOfSet
                    break;
                case 4:
                    if (ContractAsOfErrorCheck())
                    {
                        ContractAsOfSet();
                    }
                    break;
                case 5:
                    //RecallAsOfSet
                    break;
            }
        }

        private void BorrowsGrid_Paint(object sender, PaintEventArgs e)
        {
            if (!borrowContractId.Equals(BorrowsGrid.Columns["ContractId"].Text))
            {
                try
                {
                    if (dvBorrowDetails != null)
                    {
                        dvBorrowDetails.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND SecId = '" + BorrowsGrid.Columns["SecId"].Text + "' AND ContractType = 'B' AND ContractId ='" + BorrowsGrid.Columns["ContractId"].Text + "'";
                        borrowContractId = BorrowsGrid.Columns["ContractId"].Text;
                    }
                }
                catch (Exception error)
                {
                    mainForm.Alert(this.Name, error.Message);
                }
            }
        }

        private void LoansGrid_Paint(object sender, PaintEventArgs e)
        {
            if (!loanContractId.Equals(LoansGrid.Columns["ContractId"].Text))
            {
                try
                {
                    if (dvLoanDetails != null)
                    {
                        dvLoanDetails.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND SecId = '" + LoansGrid.Columns["SecId"].Text + "' AND ContractType = 'L' AND ContractId ='" + LoansGrid.Columns["ContractId"].Text + "'";
                        loanContractId = LoansGrid.Columns["ContractId"].Text;
                    }
                }
                catch (Exception error)
                {
                    mainForm.Alert(this.Name, error.Message);
                }
            }
        }

        private void ShowTotalsCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            if (ShowTotalsCommand.Checked)
            {
                ContractSummaryGrid.Splits[0, 3].SplitSize = 3;
            }
            else
            {
                ContractSummaryGrid.Splits[0, 3].SplitSize = 0;
            }
        }

        private void ShowSummaryCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            ContractSummaryGrid.Visible = ShowSummaryCommand.Checked;
            BorrowsGrid.Splits[0].DisplayColumns["ContractType"].Visible = !ContractSummaryGrid.Visible;
            LoansGrid.Splits[0].DisplayColumns["ContractType"].Visible = !ContractSummaryGrid.Visible;
            BorrowsGrid.Splits[0].DisplayColumns["ContractId"].Visible = !ContractSummaryGrid.Visible;
            LoansGrid.Splits[0].DisplayColumns["ContractId"].Visible = !ContractSummaryGrid.Visible;

            BorrowsGrid.Splits[0].DisplayColumns["Isin"].Visible = !ContractSummaryGrid.Visible;
            LoansGrid.Splits[0].DisplayColumns["Isin"].Visible = !ContractSummaryGrid.Visible;

            BorrowsGrid.Splits[0].DisplayColumns["PrePayDate"].Visible = !ContractSummaryGrid.Visible;
            LoansGrid.Splits[0].DisplayColumns["PrePayDate"].Visible = !ContractSummaryGrid.Visible;

            BorrowsGrid.Splits[0].DisplayColumns["PrePayRate"].Visible = !ContractSummaryGrid.Visible;
            LoansGrid.Splits[0].DisplayColumns["PrePayRate"].Visible = !ContractSummaryGrid.Visible;            
            
            BorrowsGrid.FilterBar = !ContractSummaryGrid.Visible;
            LoansGrid.FilterBar = !ContractSummaryGrid.Visible;

            if (!ContractSummaryGrid.Visible)
            {
                try
                {
                    if (dvBorrows != null)
                    {
                        dvBorrows.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B'";
                    }

                    if (dvLoans != null)
                    {
                        dvLoans.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L'";
                    }
                }
                catch (Exception error)
                {
                    mainForm.Alert(this.Name, error.Message);
                }
            }
        }

        private void HistoryOnDemandCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            string contractId = "";

            try
            {
                switch (ContractSummaryDockingTab.SelectedIndex)
                {
                    case 0:
                        if (BorrowsGrid.Focused)
                        {
                            contractId = BorrowsGrid.Columns["ContractId"].Text;
                        }
                        else
                        {
                            contractId = LoansGrid.Columns["ContractId"].Text;
                        }
                        break;

                    case 2:
                        contractId = ReturnsGrid.Columns["ContractId"].Text;
                        break;


                    case 4:
                        contractId = MarksGrid.Columns["ContractId"].Text;
                        break;

                    case 5:
                        contractId = AsOfContractGrid.Columns["ContractId"].Text;
                        break;

                    case 6:
                        contractId = ResearchContractsGrid.Columns["ContractId"].Text;
                        break;
                }

                if (!contractId.Equals(""))
                {
                    HistoryOnDemandForm historyOnDemand = new HistoryOnDemandForm(mainForm, BookGroupCombo.Text, contractId);
                    historyOnDemand.Show();
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void BorrowsGrid_Filter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            if (e.Condition.ToString().Equals(""))
            {
                dvBorrows.RowFilter = dvBorrows.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B'";
            }
            else
            {
                dvBorrows.RowFilter = dvBorrows.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B' AND" + e.Condition.ToString();
            }

        }

        private void LoansGrid_Filter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            if (e.Condition.ToString().Equals(""))
            {
                dvLoans.RowFilter = dvLoans.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L'";
            }
            else
            {
                dvLoans.RowFilter = dvLoans.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L' AND" + e.Condition.ToString();
            }
        }

        private void BillingGrid_Filter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            if (e.Condition.ToString().Equals(""))
            {
                dvBilling.RowFilter = dvBilling.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
            }
            else
            {
                dvBilling.RowFilter = dvBilling.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND " + e.Condition.ToString();
            }
        }

        private void ReturnsGrid_Filter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            if (e.Condition.ToString().Equals(""))
            {
                dvReturns.RowFilter = dvReturns.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
            }
            else
            {
                dvReturns.RowFilter = dvReturns.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND " + e.Condition.ToString();
            }
        }

        private void MarksGrid_Filter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            if (e.Condition.ToString().Equals(""))
            {
                dvMarks.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND Book = '" + MarksSummaryGrid.Columns["Book"].Text + "'";
            }
            else
            {
                dvMarks.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND Book = '" + MarksSummaryGrid.Columns["Book"].Text + "' AND " + e.Condition.ToString();
            }
        }

        private void ContractSummaryGrid_Filter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            if (e.Condition.ToString().Equals(""))
            {
                dvContractSummary.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
            }
            else
            {
                dvContractSummary.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND " + e.Condition.ToString();
            }

            ContractSummaryFooterLoad();
        }

        private void MarksSummaryGrid_Filter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            if (e.Condition.ToString().Equals(""))
            {
                dvMarksSummary.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
            }
            else
            {
                dvMarksSummary.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND " + e.Condition.ToString();
            }
        }

        private void ContractMaintenanceCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            string contractId = "";

            try
            {
                switch (ContractSummaryDockingTab.SelectedIndex)
                {
                    case 0:
                        if (BorrowsGrid.Focused)
                        {
                            contractId = BorrowsGrid.Columns["ContractId"].Text;
                        }
                        else
                        {
                            contractId = LoansGrid.Columns["ContractId"].Text;
                        }
                        break;

                    case 2:
                        contractId = ReturnsGrid.Columns["ContractId"].Text;
                        break;


                    case 4:
                        contractId = MarksGrid.Columns["ContractId"].Text;
                        break;

                    case 5:
                        contractId = AsOfContractGrid.Columns["ContractId"].Text;
                        break;

                    case 6:
                        contractId = ResearchContractsGrid.Columns["ContractId"].Text;
                        break;
                }

                if (!contractId.Equals(""))
                {
                    TradingContractMaintenanceForm tradingContractMaintenanceForm = new TradingContractMaintenanceForm(mainForm, contractId);
                    tradingContractMaintenanceForm.Show();
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void ShowSettledItemsOnlyCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            ShowSettledItemsOnlyCommand.Checked = !ShowSettledItemsOnlyCommand.Checked;

            if (ShowSettledItemsOnlyCommand.Checked)
            {
                if (ContractSummaryGrid.Visible)
                {
                    if (!secId.Equals(ContractSummaryGrid.Columns["SecId"].Text) || !currencyIso.Equals(ContractSummaryGrid.Columns["CurrencyIso"].Text))
                    {
                        secId = ContractSummaryGrid.Columns["SecId"].Text;
                        currencyIso = ContractSummaryGrid.Columns["CurrencyIso"].Text;

                        try
                        {
                            if (dvBorrows != null)
                            {
                                dvBorrows.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND SecId = '" + secId + "' AND CurrencyIso = '" + currencyIso + "' AND ContractType = 'B' AND IsSettled = 1";
                            }

                            if (dvLoans != null)
                            {
                                dvLoans.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND SecId = '" + secId + "' AND CurrencyIso = '" + currencyIso + "' AND ContractType = 'L' AND IsSettled = 1";
                            }
                        }
                        catch (Exception error)
                        {
                            mainForm.Alert(this.Name, error.Message);
                        }
                    }
                }
                else
                {
                    try
                    {
                        if (dvBorrows != null)
                        {
                            dvBorrows.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B' AND IsSettled = 1";
                        }

                        if (dvLoans != null)
                        {
                            dvLoans.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L' AND IsSettled = 1";
                        }
                    }
                    catch (Exception error)
                    {
                        mainForm.Alert(this.Name, error.Message);
                    }
                }
            }
            else
            {
                if (ContractSummaryGrid.Visible)
                {
                    if (!secId.Equals(ContractSummaryGrid.Columns["SecId"].Text) || !currencyIso.Equals(ContractSummaryGrid.Columns["CurrencyIso"].Text))
                    {
                        secId = ContractSummaryGrid.Columns["SecId"].Text;
                        currencyIso = ContractSummaryGrid.Columns["CurrencyIso"].Text;

                        try
                        {
                            if (dvBorrows != null)
                            {
                                dvBorrows.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND SecId = '" + secId + "' AND CurrencyIso = '" + currencyIso + "' AND ContractType = 'B'";
                            }

                            if (dvLoans != null)
                            {
                                dvLoans.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND SecId = '" + secId + "' AND CurrencyIso = '" + currencyIso + "' AND ContractType = 'L'";
                            }
                        }
                        catch (Exception error)
                        {
                            mainForm.Alert(this.Name, error.Message);
                        }
                    }
                }
                else
                {
                    try
                    {
                        if (dvBorrows != null)
                        {
                            dvBorrows.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B'";
                        }

                        if (dvLoans != null)
                        {
                            dvLoans.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L'";
                        }
                    }
                    catch (Exception error)
                    {
                        mainForm.Alert(this.Name, error.Message);
                    }
                }
            }
        }

        private void GenerateCOBCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                string fileName = Standard.ConfigValue("TempPath") + BookGroupCombo.Text + "_COB_" + Standard.ProcessId() + ".xls";

                ContractCOBFormat contractReport = new ContractCOBFormat();
                contractReport.ExcelBillingBookOpen(fileName, DateTimePicker.Text);
                contractReport.CloseOfBusinessGenerate(BookGroupCombo.Text, DateTimePicker.Text, mainForm.ServiceAgent.CurrenciesGet(), mainForm.PositionAgent.ContractDataGet(0, DateTimePicker.Text, "", ""));
                contractReport.ExcelBillingBookSave();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void CashGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                mainForm.PositionAgent.CashSet(
                    CashGrid.Columns["BookGroup"].Text,
                    CashGrid.Columns["Book"].Text,
                    CashGrid.Columns["ContractId"].Text,
                    CashGrid.Columns["ContractType"].Text,
                    CashGrid.Columns["AmountActual"].Text,
                    CashGrid.Columns["SettleDate"].Text,
                    CashGrid.Columns["ActUserId"].Text);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void ContractToExecuteCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {

                switch (ContractSummaryDockingTab.SelectedIndex)
                {
                    case 0:
                        if (BorrowsGrid.Focused)
                        {
                            mainForm.TradingAgent.TradeRequest(
                                BorrowsGrid.Columns["BizDate"].Text,
                                BorrowsGrid.Columns["BookGroup"].Text,
                                BorrowsGrid.Columns["Book"].Text,
                               BorrowsGrid.Columns["ContractId"].Text);
                        }
                        else
                        {
                            mainForm.TradingAgent.TradeRequest(
                                LoansGrid.Columns["BizDate"].Text,
                                LoansGrid.Columns["BookGroup"].Text,
                                LoansGrid.Columns["Book"].Text,
                                LoansGrid.Columns["ContractId"].Text);
                        }
                        break;

                    case 2:
                        mainForm.TradingAgent.ReturnRequest(
                            ReturnsGrid.Columns["BizDate"].Text,
                            ReturnsGrid.Columns["BookGroup"].Text,
                            ReturnsGrid.Columns["Book"].Text,
                           ReturnsGrid.Columns["ContractId"].Text);
                        break;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void OtherSecurityIdEditCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                AdminSecMasterItemForm adminSecMasterItemForm = new AdminSecMasterItemForm(mainForm, ContractSummaryGrid.Columns["SecId"].Text);
                adminSecMasterItemForm.Show();                    
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }
    }
}