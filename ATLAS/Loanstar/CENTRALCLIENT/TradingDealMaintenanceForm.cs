using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CentralClient
{
    public partial class TradingDealMaintenanceForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private MainForm mainForm = null;

        private DataSet dsDeliveryTypes;
        private DataSet dsCollateralCodes;
        private DataSet dsFunds;
        private DataSet dsContractCurrencies;
        private DataSet dsFeeCurrencies;
        private DataSet dsCashCountries;
        private DataSet dsSettleCountries;
        private DataSet dsBookGroups;
        private DataSet dsClearingInstructions;
        private DataSet dsBooks;

        private DataView dvBooks;

        private string dealId;
        private string bizDate;

        public TradingDealMaintenanceForm(MainForm mainForm, string bizDate, string dealId)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            this.dealId = dealId;
            this.bizDate = bizDate;
        }

        private void TradingDealMaintenanceForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsDeliveryTypes = mainForm.PositionAgent.ContractDeliveryTypesGet();
                dsCollateralCodes = mainForm.PositionAgent.ContractCollateralCodes();
                dsFunds = mainForm.PositionAgent.FundsGet();

                dsBookGroups = mainForm.ServiceAgent.BookGroupGet(mainForm.UserId);
                dsBooks = mainForm.AdminAgent.BookGet("", "");

                dsContractCurrencies = mainForm.ServiceAgent.CurrenciesGet();
                dsFeeCurrencies = mainForm.ServiceAgent.CurrenciesGet();
                dsCashCountries = mainForm.ServiceAgent.CountriesGet("");
                dsSettleCountries = dsCashCountries.Copy();

                BookGroupCombo.HoldFields();
                BookGroupCombo.DataSource = dsBookGroups.Tables["BookGroups"];

                dvBooks = new DataView(dsBooks.Tables["Books"], "BookGroup = ''", "", DataViewRowState.CurrentRows);
                
                BookCombo.HoldFields();
                BookCombo.DataSource = dvBooks;
                
                DeliveryTypeCombo.HoldFields();
                DeliveryTypeCombo.DataSource = dsDeliveryTypes.Tables["DeliveryTypes"];

                CollateralCodeCombo.HoldFields();
                CollateralCodeCombo.DataSource = dsCollateralCodes.Tables["CollateralCodes"];

                FundCombo.HoldFields();
                FundCombo.DataSource = dsFunds.Tables["Funds"];

                CurrencyIsoCombo.HoldFields();
                CurrencyIsoCombo.DataSource = dsContractCurrencies.Tables["Currencies"];

                FeeCurrencyIsoCombo.HoldFields();
                FeeCurrencyIsoCombo.DataSource = dsFeeCurrencies.Tables["Currencies"];

                CashDepotCombo.HoldFields();
                CashDepotCombo.DataSource = dsCashCountries.Tables["Countries"];

                SecurityDepotCombo.HoldFields();
                SecurityDepotCombo.DataSource = dsSettleCountries.Tables["Countries"];

                if (!dealId.Equals(""))
                {
                    this.Text += " - " + dealId;
                    LoadDealDataRow(dealId);
                }
                else
                {
                    this.Text += " - New Deal" ;
                    
                    RadioBorrow.Checked = true;
                    MarginTextBox.Text = "1.05";
                    MarginCodeTextBox.Text = "%";
                    TradeDateEdit.Text = mainForm.ServiceAgent.BizDate();

                    DivRateTextBox.Text = "100";
                    DivRateCodeTextBox.Text = "%";

                    SubmitRibbonButton.Enabled = true;
                    FlipTradeRibbonButton.Enabled = true;
                    UnlockRibbonButton.Enabled = false;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void BookGroupCombo_TextChanged(object sender, EventArgs e)
        {
            dvBooks.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
        }

        private void LoadDealDataRow(string dealId)
        {
            DataSet dsDeal;

            dsDeal = mainForm.PositionAgent.DealItemGet(bizDate, dealId, mainForm.UtcOffset);

            BookGroupCombo.Text = dsDeal.Tables["Deal"].Rows[0]["BookGroup"].ToString();
            BookCombo.Text = dsDeal.Tables["Deal"].Rows[0]["Book"].ToString();

            if (dsDeal.Tables["Deal"].Rows[0]["DealType"].ToString().Equals("B"))
            {
                RadioBorrow.Checked = true;
            }
            else
            {
                RadioLoan.Checked = true;
            }

            DealIdTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["DealId"].ToString();
            SecIdTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["Secid"].ToString();
            SymbolTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["Symbol"].ToString();
            IsinTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["Isin"].ToString();
            QuantityTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["Quantity"].ToString();
            AmountTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["Amount"].ToString();

            if (!dsDeal.Tables["Deal"].Rows[0]["ValueDate"].ToString().Equals(""))
            {
                TradeDateEdit.Value = dsDeal.Tables["Deal"].Rows[0]["ValueDate"].ToString();
            }

            if (!dsDeal.Tables["Deal"].Rows[0]["SettleDate"].ToString().Equals(""))
            {
                SettleDateEdit.Value = dsDeal.Tables["Deal"].Rows[0]["SettleDate"].ToString();
            }

            if (!dsDeal.Tables["Deal"].Rows[0]["TermDate"].ToString().Equals(""))
            {
                TermDateEdit.Value = dsDeal.Tables["Deal"].Rows[0]["TermDate"].ToString();
            }

            PoolCodeTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["PoolCode"].ToString();
            RateTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["Rate"].ToString();
            RateCodeTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["RateCode"].ToString();            
            CollateralCodeCombo.Text = dsDeal.Tables["Deal"].Rows[0]["CollateralCode"].ToString();
            MarginTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["Margin"].ToString();
            MarginCodeTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["MarginCode"].ToString();
            CurrencyIsoCombo.Text = dsDeal.Tables["Deal"].Rows[0]["CurrencyIso"].ToString();
            CashDepotCombo.Text = dsDeal.Tables["Deal"].Rows[0]["CashDepot"].ToString();
            SecurityDepotCombo.Text = dsDeal.Tables["Deal"].Rows[0]["SecurityDepot"].ToString();
            FundCombo.Text = dsDeal.Tables["Deal"].Rows[0]["Fund"].ToString();
            DeliveryTypeCombo.Text = dsDeal.Tables["Deal"].Rows[0]["DeliveryType"].ToString();

            if (!dsDeal.Tables["Deal"].Rows[0]["PrepayDate"].ToString().Equals(""))
            {
                PrepayDateEdit.Value = dsDeal.Tables["Deal"].Rows[0]["PrepayDate"].ToString();
            }

            PrepayRateTextBox.Value = dsDeal.Tables["Deal"].Rows[0]["PrepayRate"].ToString();
            TradeRouteCombo.Text = dsDeal.Tables["Deal"].Rows[0]["TradeRoute"].ToString();
            FeeAmountTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["FeeAmount"].ToString();
            FeeCurrencyIsoCombo.Text = dsDeal.Tables["Deal"].Rows[0]["FeeCurrencyIso"].ToString();
            FeeTypeTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["FeeType"].ToString();
            DivRateTextBox.Text = dsDeal.Tables["Deal"].Rows[0]["DivRate"].ToString();
        }

        private void SubmitRibbonButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
               DealIdTextBox.Text =  mainForm.PositionAgent.DealSet(
                    DealIdTextBox.Text,
                    BookGroupCombo.Text,
                    (RadioBorrow.Checked) ? "B" : "L",
                    BookCombo.Text,
                    "",
                    "",
                    SecIdTextBox.Text,
                    QuantityTextBox.Text,
                    AmountTextBox.Text,
                    CollateralCodeCombo.Text,
                    TradeDateEdit.Text,
                    SettleDateEdit.Text,
                    TermDateEdit.Text,
                    RateTextBox.Text,
                    RateCodeTextBox.Text,
                    PoolCodeTextBox.Text,
                    DivRateTextBox.Text,
                    true.ToString(),
                    true.ToString(),
                    MarginTextBox.Text,
                    MarginCodeTextBox.Text,
                    CurrencyIsoCombo.Text,
                    SecurityDepotCombo.Text,
                    CashDepotCombo.Text,
                    CommentTextBox.Text,
                    FundCombo.Text,
                    "D",
                    mainForm.UserId,
                    true,
                    FeeAmountTextBox.Text,
                    FeeCurrencyIsoCombo.Text,
                    FeeTypeTextBox.Text,
                   DeliveryTypeCombo.Text,
                   TradeRouteCombo.Text,                   
                   PrepayRateTextBox.Text,
                   PrepayDateEdit.Text);

               StatusRibbonLabel.Text = "Successfully updated deal id: " + DealIdTextBox.Text;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, "Error submitting deal : " + error.Message);
            }


            this.Cursor = Cursors.Default;
        }

        private void FlipTradeRibbonButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                DealIdTextBox.Text = "";
                BookCombo.Text = "";

                if (RadioBorrow.Checked)
                {
                    RadioLoan.Checked = true;
                }
                else
                {
                    RadioBorrow.Checked = true;
                }

                TradeRouteCombo.Text = "";

                SubmitRibbonButton.Enabled = true;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void UnlockRibbonButton_Click(object sender, EventArgs e)
        {
            SubmitRibbonButton.Enabled = true;
            FlipTradeRibbonButton.Enabled = true;
            UnlockRibbonButton.Enabled = false;
        }

        private void SecIdTextBox_Leave(object sender, EventArgs e)
        {
            DataSet dsSecId = new DataSet();

            try
            {
                if (!SecIdTextBox.Text.Equals(""))
                {
                    dsSecId = mainForm.ServiceAgent.SecMasterLookup(SecIdTextBox.Text);

                    if (dsSecId.Tables["SecMasterItem"].Rows.Count > 0)
                    {
                        SecIdTextBox.Text = dsSecId.Tables["SecMasterItem"].Rows[0]["Sedol"].ToString();
                        SymbolTextBox.Text = dsSecId.Tables["SecMasterItem"].Rows[0]["Symbol"].ToString();
                        IsinTextBox.Text = dsSecId.Tables["SecMasterItem"].Rows[0]["Isin"].ToString();
                        PriceTextBox.Text = dsSecId.Tables["SecMasterItem"].Rows[0]["Price"].ToString();
                    }
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void Calculate_Price()
        {
            decimal amount = 0;
            decimal quantity = 0;
            decimal price = 0;
            decimal margin = 0;

            if (!QuantityTextBox.Text.Equals("") &&
                !MarginTextBox.Text.Equals("") &&
                !PriceTextBox.Text.Equals(""))
            {
                if (Decimal.TryParse(QuantityTextBox.Text, out quantity) &&
                    Decimal.TryParse(MarginTextBox.Text, out margin) &&
                    Decimal.TryParse(PriceTextBox.Text, out price))
                {
                    amount = quantity * price * margin;
                }
            }

            AmountTextBox.Text = amount.ToString("#,##0.00");
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            Calculate_Price();
        }

        private void PriceMarginTextBox_Leave(object sender, EventArgs e)
        {           
            decimal priceMargin = 0;
            decimal margin = 0;

            if (!PriceMarginTextBox.Text.Equals(""))
            {
                if (Decimal.TryParse(PriceMarginTextBox.Text, out priceMargin) &&
                    Decimal.TryParse(MarginTextBox.Text, out margin))
                {

                    PriceTextBox.Text = (priceMargin / margin).ToString("#,##0.0000000");
                }
            }
        }

        private void BookCombo_TextChanged(object sender, EventArgs e)
        {
            dsClearingInstructions = mainForm.AdminAgent.BookClearingInstructionGet(BookGroupCombo.Text, BookCombo.Text, "");
            TradeRouteCombo.HoldFields();
            TradeRouteCombo.DataSource = dsClearingInstructions.Tables["BookInstructions"];
        }
    }
}

