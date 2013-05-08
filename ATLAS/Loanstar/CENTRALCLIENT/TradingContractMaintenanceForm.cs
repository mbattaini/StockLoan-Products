using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;
using StockLoan.MainBusiness;

namespace CentralClient
{
    public partial class TradingContractMaintenanceForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private string contractId;
        private MainForm mainForm;
        
        private DataSet dsBooks;
        private DataSet dsContracts;
        private DataSet dsCashDepots;
        private DataSet dsSecurityDepots;
        private DataSet dsCurrencyIso;
        private DataSet dsFunds;
        private DataSet dsDeliveryTypes;
        private DataSet dsCollateralCodes;
        private DataSet dsContractCurrencies;
        private DataSet dsFeeCurrencies;
        private DataSet dsClearingInstructions;

        private DataView dvBooks;

        private int row = 0;
        private int counter = 0;

        private bool isUpdating;
        private bool isSettled = true;
        private bool closeTrade = false;

        public TradingContractMaintenanceForm(MainForm mainForm, string contractId)
        {
            InitializeComponent();

            this.contractId = contractId;
            this.mainForm = mainForm;
        }

        private void TradingContractMaintenanceForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsContracts = mainForm.PositionAgent.ContractResearchDataGet("", "", "", contractId, "", "", "");
                dsContracts.Tables["Contracts"].Columns.Add("ProcessStatus");
                dsContracts.AcceptChanges();
                
                dsDeliveryTypes = mainForm.PositionAgent.ContractDeliveryTypesGet();
                dsCollateralCodes = mainForm.PositionAgent.ContractCollateralCodes();
                dsFunds = mainForm.PositionAgent.FundsGet();
                
                dsBooks = mainForm.AdminAgent.BookGet("", "");
                
                dsContractCurrencies = mainForm.ServiceAgent.CurrenciesGet();
                dsFeeCurrencies = mainForm.ServiceAgent.CurrenciesGet();                

                dsCashDepots = mainForm.ServiceAgent.CountriesGet("");
                dsSecurityDepots = mainForm.ServiceAgent.CountriesGet("");
                dsCurrencyIso = mainForm.ServiceAgent.CurrenciesGet();                

                ContractGrid.SetDataBinding(dsContracts, "Contracts", true);
                
                CashDepotCombo.HoldFields();
                CashDepotCombo.DataSource = dsCashDepots.Tables["Countries"];

                SecurityDepotCombo.HoldFields();                
                SecurityDepotCombo.DataSource = dsSecurityDepots.Tables["Countries"];

                CurrencyIsoCombo.HoldFields();
                CurrencyIsoCombo.DataSource = dsCurrencyIso.Tables["Currencies"];



                dvBooks = new DataView(dsBooks.Tables["Books"], "BookGroup ='" + ContractGrid.Columns["BookGroup"].Text + "'", "", DataViewRowState.CurrentRows);
                
                BookCombo.HoldFields();
                BookCombo.DataSource = dvBooks;

                DeliveryTypeCombo.HoldFields();
                DeliveryTypeCombo.DataSource = dsDeliveryTypes.Tables["DeliveryTypes"];

                CollateralCodeCombo.HoldFields();
                CollateralCodeCombo.DataSource = dsCollateralCodes.Tables["CollateralCodes"];

                FeeCurrencyIsoCombo.HoldFields();
                FeeCurrencyIsoCombo.DataSource = dsFeeCurrencies.Tables["Currencies"];

                FundCombo.HoldFields();
                FundCombo.DataSource = dsFunds.Tables["Funds"];
                
                Quantity.Visible = false;
                QuantityTextBox.ReadOnly = true;

                Amount.Visible = false;
                AmountTextBox.ReadOnly = true;
                
                ContractLoadRow();
                dsClearingInstructions = mainForm.AdminAgent.BookClearingInstructionGet(BookGroupCombo.Text, BookCombo.Text, "");
                
                TradeRouteCombo.HoldFields();
                TradeRouteCombo.DataSource = dsClearingInstructions.Tables["BookInstructions"];

                AllTradeHistoryCheckBox.Checked = true;

                isUpdating = false;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;            
        }

        private void ContractLoadRow()
        {
            if (!isUpdating)
            {
                BookGroupCombo.Text = ContractGrid.Columns["BookGroup"].Text;
                BookGroupCombo.ReadOnly = true;

                BookCombo.Text = ContractGrid.Columns["Book"].Text;
                ContractIdTextBox.Text = ContractGrid.Columns["ContractId"].Text;
             
                if (ContractGrid.Columns["ContractType"].Text.Equals("B"))
                {
                    RadioBorrow.Checked = true;
                }
                else
                {
                    RadioLoan.Checked = true;
                }

                SecIdTextBox.Text = ContractGrid.Columns["SecId"].Text;
                SymbolTextBox.Text = ContractGrid.Columns["Symbol"].Text;
                IsinTextBox.Text = ContractGrid.Columns["Isin"].Text;
                QuantityTextBox.Text = ContractGrid.Columns["Quantity"].Text;
                AmountTextBox.Text = ContractGrid.Columns["Amount"].Text;
				PoolCodeTextBox.Text = ContractGrid.Columns["PoolCode"].Text;
                RateTextBox.Text = ContractGrid.Columns["RebateRate"].Text;
                RateCodeTextBox.Text = ContractGrid.Columns["RateCode"].Text;
                MarginTextBox.Text = ContractGrid.Columns["Margin"].Text;
                MarginCodeTextBox.Text = ContractGrid.Columns["MarginCode"].Text;

                if (!ContractGrid.Columns["ValueDate"].Text.Equals(""))
                {
                    TradeDateEdit.Text = ContractGrid.Columns["ValueDate"].Text;
                }
                if (!ContractGrid.Columns["SettleDate"].Text.Equals(""))
                {
                    SettleDateEdit.Text = ContractGrid.Columns["SettleDate"].Text;
                }
                if (!ContractGrid.Columns["TermDate"].Text.Equals(""))
                {
                    TermDateEdit.Text = ContractGrid.Columns["TermDate"].Text;
                }

                PoolCodeTextBox.Text =  ContractGrid.Columns["PoolCode"].Text;
                CollateralCodeCombo.Text =  ContractGrid.Columns["CollateralCode"].Text;
                DivRateTextBox.Text = ContractGrid.Columns["DivRate"].Text;
                DivRateCodeTextBox.Text = "%";
                CurrencyIsoCombo.Text =  ContractGrid.Columns["CurrencyIso"].Text;
                CashDepotCombo.Text =  ContractGrid.Columns["CashDepot"].Text;
                SecurityDepotCombo.Text =  ContractGrid.Columns["SecurityDepot"].Text;
                FundCombo.Text =  ContractGrid.Columns["Fund"].Text;
                DeliveryTypeCombo.Text =  ContractGrid.Columns["DeliveryType"].Text;

                if (! ContractGrid.Columns["PrePayDate"].Text.Equals(""))
                {
                    PrepayDateEdit.Value =  ContractGrid.Columns["PrePayDate"].Text;
                }

                PrepayRateTextBox.Text =  ContractGrid.Columns["PrePayRate"].Text;
                TradeRouteCombo.Text =  ContractGrid.Columns["TradeRoute"].Text;
                FeeAmountTextBox.Text =  ContractGrid.Columns["FeeAmount"].Text;
                FeeCurrencyIsoCombo.Text =  ContractGrid.Columns["FeeCurrencyIso"].Text;
                FeeTypeTextBox.Text =  ContractGrid.Columns["FeeType"].Text;
                DivRateTextBox.Text =  ContractGrid.Columns["DivRate"].Text;
                CommentTextBox.Text = ContractGrid.Columns["Comment"].Text;
                TradeRefIdTextBox.Text = ContractGrid.Columns["TradeRefId"].Text;

                 if (DateTime.Parse(SettleDateEdit.Text) > DateTime.Parse(mainForm.ServiceAgent.BizDate()))
                {
                    isSettled = false;
                    
                    Quantity.Visible = true;
                    QuantityTextBox.ReadOnly = false;

                    Amount.Visible = true;
                    AmountTextBox.ReadOnly = false;
                }
            }
        }

        private void ContractGrid_Paint(object sender, PaintEventArgs e)
        {
            if (ContractGrid.Row != row)
            {
                ContractLoadRow();
                row = ContractGrid.Row;
            }
        }
         
        private void ContractGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            e.Value = mainForm.Format(e.Column.DataField, e.Value.ToString());
        }

        private void AllTradeHistoryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AllTradeHistoryCheckBox.Checked)
            {
                StartDateTimeEdit.Enabled = false;
                StopDateTimeEdit.Enabled = false;
            }
            else
            {
                StartDateTimeEdit.Enabled = true;
                StopDateTimeEdit.Enabled = true;
            }
        }

        private void SubmitRibbonButton_Click(object sender, EventArgs e)
        {            
            this.ContractBackgroundWorker.RunWorkerAsync();
            counter = 0;
            isUpdating = true;
        }

        private void ContractGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            switch (ContractGrid[e.Row, "ProcessStatus"].ToString())
            {
                case ("P"):
                    e.CellStyle.BackColor = Color.LightYellow;
                    break;

                case ("E"):
                    e.CellStyle.BackColor = Color.LightCoral;
                    break;

                case ("S"):
                    e.CellStyle.BackColor = Color.LawnGreen;
                    break;
            }
        }

        private void ContractBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            StatusLabel.Text = "Working...";            
        }

        private void ContractBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int index = 0;

            if (AllTradeHistoryCheckBox.Checked)
            {
                for (int count = 0; count < ContractGrid.RowCount; count++)
                {
                    try
                    {
                        ContractGrid[count, "ProcessStatus"] = "P";

                        mainForm.PositionAgent.ContractSet(
                       ContractGrid.Columns["BizDate"].CellText(count),
                       ContractGrid.Columns["BookGroup"].CellText(count),
                       ContractGrid.Columns["ContractId"].CellText(count),
                       ContractGrid.Columns["ContractType"].CellText(count),
                       (Book.Checked)? BookCombo.Text : ContractGrid.Columns["Book"].CellText(count),
                        "",
                       (Quantity.Checked) ? QuantityTextBox.Text : ContractGrid.Columns["Quantity"].CellValue(count).ToString(),
                       (Quantity.Checked) ? QuantityTextBox.Text: ContractGrid.Columns["QuantitySettled"].CellValue(count).ToString(),                      
                       (Amount.Checked) ? AmountTextBox.Text : ContractGrid.Columns["Amount"].CellValue(count).ToString(),
                       (Amount.Checked) ? AmountTextBox.Text : ContractGrid.Columns["AmountSettled"].CellValue(count).ToString(),
                       (CollateralCode.Checked) ? CollateralCodeCombo.Text : ContractGrid.Columns["CollateralCode"].CellText(count),
                       (TradeDate.Checked) ? TradeDateEdit.Text : ContractGrid.Columns["ValueDate"].CellText(count),
                       (SettleDate.Checked) ? SettleDateEdit.Text : ContractGrid.Columns["SettleDate"].CellText(count),
                       (TermDate.Checked) ? TermDateEdit.Text : ContractGrid.Columns["TermDate"].CellText(count),
                       (Rate.Checked) ? RateTextBox.Text : ContractGrid.Columns["RebateRate"].CellText(count),
                       (Rate.Checked) ? RateCodeTextBox.Text : ContractGrid.Columns["RateCode"].CellText(count),
                       "",
					   (PoolCode.Checked) ? PoolCodeTextBox.Text : ContractGrid.Columns["PoolCode"].CellText(count), 
                       (DividendRate.Checked) ? DivRateCodeTextBox.Text : ContractGrid.Columns["DivRate"].CellText(count),    
                       "",
                       "",
                       "",
                       (Margin.Checked) ? MarginTextBox.Text : ContractGrid.Columns["Margin"].CellText(count),
                       (CurrencyIso.Checked) ? CurrencyIsoCombo.Text : ContractGrid.Columns["CurrencyIso"].CellText(count),
                       (SecurityDepot.Checked) ? SecurityDepotCombo.Text : ContractGrid.Columns["SecurityDepot"].CellText(count),
                       (CashDepot.Checked) ? CashDepotCombo.Text : ContractGrid.Columns["CashDepot"].CellText(count),
                       "",
                       (Comment.Checked) ? CommentTextBox.Text : ContractGrid.Columns["Comment"].CellText(count),
                      !closeTrade,
                       (FeeAmount.Checked)? FeeAmountTextBox.Text : ContractGrid.Columns["FeeAmount"].CellText(count),
                       (FeeCurrencyIso.Checked)?FeeCurrencyIsoCombo.Text : ContractGrid.Columns["FeeCurrencyIso"].CellText(count),
                       (FeeAmount.Checked)? FeeTypeTextBox.Text: ContractGrid.Columns["FeeType"].CellText(count),
                       (Fund.Checked) ? FundCombo.Text :  ContractGrid.Columns["Fund"].CellText(count),
                       (TradeRefId.Checked) ? TradeRefIdTextBox.Text : ContractGrid.Columns["TradeRefId"].CellText(count),
                       (DeliveryType.Checked) ? DeliveryTypeCombo.Text : ContractGrid.Columns["DeliveryType"].CellText(count),
                       (TradeRoute.Checked)?TradeRouteCombo.Text : ContractGrid.Columns["TradeRoute"].CellText(count),
                       (PrepayRate.Checked) ? PrepayRateTextBox.Text : ContractGrid.Columns["PrepayRate"].CellText(count),
                       (PrepayDate.Checked) ? PrepayDateEdit.Text : ContractGrid.Columns["PrepayDate"].CellText(count)
                       );

                        ContractGrid[count, "ProcessStatus"] = "S";
                        counter++;
                    }
                    catch
                    {
                        ContractGrid[count, "ProcessStatus"] = "E";
                    }

                    ContractBackgroundWorker.ReportProgress(index);

                    if (index == 100)
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            else
            {
                for (int count = 0; count < ContractGrid.RowCount; count++)
                {

                    if (DateTime.Parse(ContractGrid.Columns["BizDate"].CellText(count)) >= DateTime.Parse(StartDateTimeEdit.Text) &&
                        DateTime.Parse(ContractGrid.Columns["BizDate"].CellText(count)) <= DateTime.Parse(StopDateTimeEdit.Text))
                    {
                        try
                        {
                            ContractGrid[count, "ProcessStatus"] = "P";

                              mainForm.PositionAgent.ContractSet(
                       ContractGrid.Columns["BizDate"].CellText(count),
                       ContractGrid.Columns["BookGroup"].CellText(count),
                       ContractGrid.Columns["ContractId"].CellText(count),
                       ContractGrid.Columns["ContractType"].CellText(count),
                        (Book.Checked) ? BookCombo.Text : ContractGrid.Columns["Book"].CellText(count),
                       "",
                       (Quantity.Checked) ? QuantityTextBox.Text : ContractGrid.Columns["Quantity"].CellValue(count).ToString(),
                       (Quantity.Checked) ? QuantityTextBox.Text : ContractGrid.Columns["QuantitySettled"].CellValue(count).ToString(),
                       (Amount.Checked) ? AmountTextBox.Text : ContractGrid.Columns["Amount"].CellValue(count).ToString(),
                       (Amount.Checked) ? AmountTextBox.Text : ContractGrid.Columns["AmountSettled"].CellValue(count).ToString(),
                       (CollateralCode.Checked) ? CollateralCodeCombo.Text : ContractGrid.Columns["CollateralCode"].CellText(count),
                       (TradeDate.Checked) ? TradeDateEdit.Text : ContractGrid.Columns["ValueDate"].CellText(count),
                       (SettleDate.Checked) ? SettleDateEdit.Text : ContractGrid.Columns["SettleDate"].CellText(count),
                       (TermDate.Checked) ? TermDateEdit.Text : ContractGrid.Columns["TermDate"].CellText(count),
                       (Rate.Checked) ? RateTextBox.Text : ContractGrid.Columns["RebateRate"].CellText(count),
                       (Rate.Checked) ? RateCodeTextBox.Text : ContractGrid.Columns["RateCode"].CellText(count),
                       "",
					   (PoolCode.Checked) ? PoolCodeTextBox.Text : ContractGrid.Columns["PoolCode"].CellText(count), 
                       (DividendRate.Checked) ? DivRateCodeTextBox.Text : ContractGrid.Columns["DivRate"].CellText(count),    
                       "",
                       "",
                       "",
                       (Margin.Checked) ? MarginTextBox.Text : ContractGrid.Columns["Margin"].CellText(count),
                       (CurrencyIso.Checked) ? CurrencyIsoCombo.Text : ContractGrid.Columns["CurrencyIso"].CellText(count),
                       (SecurityDepot.Checked) ? SecurityDepotCombo.Text : ContractGrid.Columns["SecurityDepot"].CellText(count),
                       (CashDepot.Checked) ? CashDepotCombo.Text : ContractGrid.Columns["CashDepot"].CellText(count),
                       "",
                       (Comment.Checked) ? CommentTextBox.Text : ContractGrid.Columns["Comment"].CellText(count),
                       true,
                       (FeeAmount.Checked)? FeeAmountTextBox.Text : ContractGrid.Columns["FeeAmount"].CellText(count),
                       (FeeCurrencyIso.Checked)?FeeCurrencyIsoCombo.Text : ContractGrid.Columns["FeeCurrencyIso"].CellText(count),
                       (FeeAmount.Checked)? FeeTypeTextBox.Text: ContractGrid.Columns["FeeType"].CellText(count),
                       (Fund.Checked) ? FundCombo.Text :  ContractGrid.Columns["Fund"].CellText(count),
                       (TradeRefId.Checked) ? TradeRefIdTextBox.Text : ContractGrid.Columns["TradeRefId"].CellText(count),
                       (DeliveryType.Checked) ? DeliveryTypeCombo.Text : ContractGrid.Columns["DeliveryType"].CellText(count),
                       (TradeRoute.Checked)?TradeRouteCombo.Text : ContractGrid.Columns["TradeRoute"].CellText(count),
                       (PrepayRate.Checked) ? PrepayRateTextBox.Text : ContractGrid.Columns["PrepayRate"].CellText(count),
                       (PrepayDate.Checked) ? PrepayDateEdit.Text : ContractGrid.Columns["PrepayDate"].CellText(count)
                       );
                        
                            ContractGrid[count, "ProcessStatus"] = "S";
                            counter++;

                        }
                        catch
                        {
                            ContractGrid[count, "ProcessStatus"] = "E";
                        }

                        index++;
                    }
                    else
                    {
                        ContractGrid[count, "ProcessStatus"] = "";
                    }

                    ContractBackgroundWorker.ReportProgress(index);


                    if (index == 100)
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
        }

        private void ContractBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusLabel.Text = "Completed! Processed : " + counter.ToString("#,##0") + " contract days.";
            isUpdating = false;
        }

        private void UnlockTradeRibbonButton_Click(object sender, EventArgs e)
        {
            SubmitRibbonButton.Enabled = true;
            CloseRibbonButton.Enabled = true;
            UnlockTradeRibbonButton.Enabled = false;            
        }

        private void CloseRibbonButton_Click(object sender, EventArgs e)
        {
            closeTrade = true;

            this.ContractBackgroundWorker.RunWorkerAsync();
            counter = 0;
            isUpdating = true;
        }
    }
}
