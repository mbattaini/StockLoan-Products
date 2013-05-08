using System;
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
    public partial class TradingReportingForm :  C1.Win.C1Ribbon.C1RibbonForm
    {
        private DataSet dsBookGroups;
        private DataSet dsContractSummary;
        private DataSet dsCurrencyIso;
        private DataSet dsContracts;
        private DataSet dsReturns;
        private DataSet dsMarks;
        private DataSet dsTransactionsDaily;

        private DataView dvContractSummary;
        private DataView dvContracts;
        private DataView dvCreditDebitDetailNewContract;
        private DataView dvCreditDebitDetailMarks;
        private DataView dvCreditDebitDetailReturns;
        private DataView dvTransactionsDaily;

        private MainForm mainForm;
        private string currencyIso = "";

        public TradingReportingForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }


        private void CreditDebitLoad()
        {
            if (!DateTimePicker.Text.Equals(""))
            {
                DataSet dscdBillingSummaryPrior = mainForm.PositionAgent.ContractBillingGet(DateTime.Parse(DateTimePicker.Text).AddDays(-1.0).ToString(Standard.DateFormat));
                DataSet dscdBillingSummary = mainForm.PositionAgent.ContractBillingGet(DateTimePicker.Text);
                DataSet dsCreditDebitNewContracts = new DataSet();
                DataSet dsCreditDebitMarks = new DataSet();
                DataSet dsCreditDebitReturns = new DataSet();

                dsContracts = mainForm.PositionAgent.ContractResearchDataGet(DateTimePicker.Text, "", "", "", "", "", "");
                dsMarks = mainForm.PositionAgent.MarksGet("", DateTimePicker.Text, "", "", 0);
                dsReturns = mainForm.PositionAgent.ReturnsGet("", DateTimePicker.Text, "", "", 0);

                Contracts.SummaryByCredtisDebits(dsContracts, dscdBillingSummaryPrior, dscdBillingSummary, dsMarks, dsReturns, ref dsContractSummary);
                Contracts.SummaryByCashContracts(dsContracts, ref dsCreditDebitNewContracts, SettlementType.SettledToday);

                Marks.SummaryByCashMarks(dsMarks, ref dsCreditDebitMarks);
                Returns.SummaryByCashReturns(dsReturns, ref dsCreditDebitReturns);

                dvCreditDebitDetailNewContract = new DataView(dsCreditDebitNewContracts.Tables["CreditDebitSummary"], "CurrencyIso = ''", "", DataViewRowState.CurrentRows);
                dvCreditDebitDetailMarks = new DataView(dsCreditDebitMarks.Tables["CreditDebitSummary"], "CurrencyIso = ''", "", DataViewRowState.CurrentRows);
                dvCreditDebitDetailReturns = new DataView(dsCreditDebitReturns.Tables["CreditDebitSummary"], "CurrencyIso= ''", "", DataViewRowState.CurrentRows);

                currencyIso = "";

                CreditDebitGrid.SetDataBinding(dsContractSummary, "CreditDebitSummary", true);
                CreditDebitNewContractsGrid.SetDataBinding(dvCreditDebitDetailNewContract, "", true);
                CreditDebitMarksGrid.SetDataBinding(dvCreditDebitDetailMarks, "", true);
                CreditDebitReturnsGrid.SetDataBinding(dvCreditDebitDetailReturns, "", true);
            }
        }

        private void CreditDebitGrid_Paint(object sender, PaintEventArgs e)
        {
            if (!CreditDebitGrid.Columns["CurrencyIso"].Text.Equals(currencyIso))
            {
                if (dvCreditDebitDetailNewContract != null)
                {
                    dvCreditDebitDetailNewContract.RowFilter = "CurrencyIso ='" + CreditDebitGrid.Columns["CurrencyIso"].Text + "'";
                    CreditDebitDetailFooterLoad(CreditDebitNewContractsGrid, dvCreditDebitDetailNewContract);

                    dvCreditDebitDetailMarks.RowFilter = "CurrencyIso ='" + CreditDebitGrid.Columns["CurrencyIso"].Text + "'";
                    CreditDebitDetailFooterLoad(CreditDebitMarksGrid, dvCreditDebitDetailMarks);

                    dvCreditDebitDetailReturns.RowFilter = "CurrencyIso=  '" + CreditDebitGrid.Columns["CurrencyIso"].Text + "'";
                    CreditDebitDetailFooterLoad(CreditDebitReturnsGrid, dvCreditDebitDetailReturns);
                }

                currencyIso = CreditDebitGrid.Columns["CurrencyIso"].Text;
            }
        }

        private void CreditDebitDetailFooterLoad(C1.Win.C1TrueDBGrid.C1TrueDBGrid DataGrid, DataView dvData)
        {
            decimal debit = 0;
            decimal credit = 0;

            try
            {
                foreach (DataRowView dr in dvData)
                {
                    credit += (decimal)dr["Credit"];
                    debit += (decimal)dr["Debit"];
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            DataGrid.Columns["Debit"].FooterText = debit.ToString(Formats.Cash);
            DataGrid.Columns["Credit"].FooterText = credit.ToString(Formats.Cash);
        }

        private void CreditDebitGrid_FetchCellStyle(object sender, C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs e)
        {
            try
            {
                switch (e.Column.DataColumn.DataField)
                {
                    default:
                        try
                        {
                            if (decimal.Parse(((C1.Win.C1TrueDBGrid.C1TrueDBGrid)sender).Columns[e.Column.DataColumn.DataField].CellValue(e.Row).ToString()) >= 0)
                            {
                                e.CellStyle.ForeColor = Color.Navy;
                                break;
                            }
                            else
                            {
                                e.CellStyle.ForeColor = Color.Red;
                            }
                        }
                        catch { }
                        break;
                }
            }
            catch { }
        }

        private void ProfitLossLoad()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {                             
                ContractChart.Header.Text = "Profit And Loss";                

                C1.Win.C1Chart.ChartDataSeriesCollection dsc = ProfitLossChart.ChartGroups[0].ChartData.SeriesList;
                dsc.Clear();

                ProfitLossChart.ChartArea.AxisX.Text = "Book Name";
                ProfitLossChart.ChartArea.AxisY.Text = "Total Rebate";

                C1.Win.C1Chart.ChartDataSeries ds = dsc.AddNewSeries();
                ds.LegendEntry = true;
                ds.X.DataField = "BookName";
                ds.Y.DataField = "TotalRebate";
                ds.Label = "Total Rebate";

                StartPLDateEdit.Text = mainForm.ServiceAgent.BizDate();
                StopPLDateEdit.Text = mainForm.ServiceAgent.BizDate();                 
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void ProfitLossCurrencyIsoCombo_TextChanged(object sender, EventArgs e)
        {
            if (!ProfitLossCurrencyIsoCombo.Text.Equals("") && dvContractSummary != null)
            {
                dvContractSummary.RowFilter = "CurrencyIso = '" + ProfitLossCurrencyIsoCombo.Text + "'";
                dvContracts.RowFilter = "CurrencyIso = '" + ProfitLossCurrencyIsoCombo.Text + "'";
            }
        }

        private void SummaryBookCashRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SummaryBookCashRadioButton.Checked)
            {
                ContractChart.Header.Text = "Summary By " + SummaryBookCashRadioButton.Text;

                dsContracts = mainForm.PositionAgent.ContractDataGet(mainForm.UtcOffset);
                Contracts.SummaryByBookCash(dsContracts, ref dsContractSummary, BookGroupCombo.Text);

                dvContractSummary = new DataView(dsContractSummary.Tables["ContractSummary"], "CurrencyIso = '" + SummaryCurrencyIsoCombo.Text + "'", "", DataViewRowState.CurrentRows);
                ContractChart.DataSource = dvContractSummary;

                C1.Win.C1Chart.ChartDataSeriesCollection dsc = ContractChart.ChartGroups[0].ChartData.SeriesList;
                dsc.Clear();


                ContractChart.ChartArea.AxisX.Text = "Book Name";
                ContractChart.ChartArea.AxisY.Text = "Amount";

                C1.Win.C1Chart.ChartDataSeries ds = dsc.AddNewSeries();
                ds.LegendEntry = true;
                ds.X.DataField = "BookName";
                ds.Y.DataField = "BorrowAmount";
                ds.Label = "Borrow Amount";

                ds = dsc.AddNewSeries();
                ds.LegendEntry = true;
                ds.X.DataField = "BookName";
                ds.Y.DataField = "LoanAmount";
                ds.Label = "Loan Amount";
            }
        }

        private void SummarySecurityRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SummarySecurityRadioButton.Checked)
            {
                ContractChart.Header.Text = "Summary By " + SummarySecurityRadioButton.Text;

                dsContracts = mainForm.PositionAgent.ContractDataGet(mainForm.UtcOffset);
                Contracts.SummaryBySecurity(dsContracts, ref dsContractSummary, BookGroupCombo.Text, false);

                dvContractSummary = new DataView(dsContractSummary.Tables["ContractSummary"], "CurrencyIso = '" + SummaryCurrencyIsoCombo.Text + "'", "", DataViewRowState.CurrentRows);
                ContractChart.DataSource = dvContractSummary;

                C1.Win.C1Chart.ChartDataSeriesCollection dsc = ContractChart.ChartGroups[0].ChartData.SeriesList;
                dsc.Clear();

                ContractChart.ChartArea.AxisX.Text = "Symbol";
                ContractChart.ChartArea.AxisY.Text = "Amount";

                C1.Win.C1Chart.ChartDataSeries ds = dsc.AddNewSeries();
                ds.LegendEntry = true;
                ds.X.DataField = "Symbol";
                ds.Y.DataField = "BorrowAmount";
                ds.Label = "Borrow Amount";

                ds = dsc.AddNewSeries();
                ds.LegendEntry = true;
                ds.X.DataField = "Symbol";
                ds.Y.DataField = "LoanAmount";
                ds.Label = "Loan Amount";
            }
        }

        private void SummaryPercentagesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SummaryPercentagesRadioButton.Checked)
            {
                ContractChart.Header.Text = "Summary By " + SummaryPercentagesRadioButton.Text;

                dsContracts = mainForm.PositionAgent.ContractDataGet(mainForm.UtcOffset);
                Contracts.SummaryByBookHypothication(dsContracts, ref dsContractSummary, BookGroupCombo.Text);

                dvContractSummary = new DataView(dsContractSummary.Tables["ContractSummary"], "CurrencyIso = '" + SummaryCurrencyIsoCombo.Text + "'", "", DataViewRowState.CurrentRows);
                ContractChart.DataSource = dvContractSummary;

                C1.Win.C1Chart.ChartDataSeriesCollection dsc = ContractChart.ChartGroups[0].ChartData.SeriesList;
                dsc.Clear();

                ContractChart.ChartArea.AxisX.Text = "Book Name";
                ContractChart.ChartArea.AxisY.Text = "Percentage";

                C1.Win.C1Chart.ChartDataSeries ds = dsc.AddNewSeries();
                ds.LegendEntry = true;
                ds.X.DataField = "BookName";
                ds.Y.DataField = "BorrowAmountPercent";
                ds.Label = "Borrow Amount %";


                ds = dsc.AddNewSeries();
                ds.LegendEntry = true;
                ds.X.DataField = "BookName";
                ds.Y.DataField = "LoanAmountPercent";
                ds.Label = "Loan Amount %";
            }
        }
        private void SummaryLoad()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsContracts = mainForm.PositionAgent.ContractDataGet(mainForm.UtcOffset);
                Contracts.DistinctCurrencies(dsContracts, ref dsCurrencyIso, BookGroupCombo.Text);

                SummaryCurrencyIsoCombo.HoldFields();
                SummaryCurrencyIsoCombo.DataSource = dsCurrencyIso.Tables["CurrencyIso"];

                SummarySecurityRadioButton.Checked = true;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }
        private void TradingReportingForm_Load(object sender, EventArgs e)
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

                ReportingDockingTab.SelectedIndex = 0;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void TradingReportingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

            mainForm.tradingReportingForm = null;
        }

        private void ReportingDockingTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ReportingDockingTab.SelectedIndex)
            {
                case 0:
                    SummaryLoad();
                    break;

                case 1:
                    CreditDebitLoad();
                    break;

                case 2:
                    ProfitLossLoad();
                    break;
            }
        }
        private void SummaryCurrencyIsoCombo_TextChanged(object sender, EventArgs e)
        {
            if (!SummaryCurrencyIsoCombo.Text.Equals("") && dvContractSummary != null)
            {
                dvContractSummary.RowFilter = "CurrencyIso = '" + SummaryCurrencyIsoCombo.Text + "'";
            }
        }

        private void Grid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            e.Value = mainForm.Format(e.Column.DataField, e.Value.ToString());
        }

        private void BookGroupCombo_TextChanged(object sender, EventArgs e)
        {
            switch (ReportingDockingTab.SelectedIndex)
            {
                case 0:
                    SummaryLoad();
                    break;

                case 1:
                    CreditDebitLoad();
                    break;

                case 2:
                    ProfitLossLoad();
                    ProfitLossReload();
                    break;
            }
        }

        private void HistoryOnDemandCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            string contractId = "";

            try
            {
                switch (ReportingDockingTab.SelectedIndex)
                {
                    case 0:
                    case 1:
                        contractId = "";
                        break;

                    case 2:
                        contractId = ProfitLossDetailGrid.Columns["ContractId"].Text;
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

        private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
        
            try
            {
                switch (ReportingDockingTab.SelectedIndex)
                {
                    case 0:
                    case 1:
                        if (CreditDebitGrid.Focused)
                        {
                            mainForm.SendToExcel(ref CreditDebitGrid, 0, false);
                            mainForm.SendToExcel(ref CreditDebitGrid, 1, false);
                        }
                        else if (CreditDebitNewContractsGrid.Focused)
                        {
                            mainForm.SendToExcel(ref CreditDebitNewContractsGrid, false);
                        }
                        else if (CreditDebitMarksGrid.Focused)
                        {
                            mainForm.SendToExcel(ref CreditDebitMarksGrid, false);
                        }
                        else if (CreditDebitReturnsGrid.Focused)
                        {
                            mainForm.SendToExcel(ref CreditDebitReturnsGrid, false);
                        }
                        break;

                    case 2:
                        if (ProfitLossGrid.Focused)
                        {
                            mainForm.SendToExcel(ref ProfitLossGrid, false);
                        }
                        else if (ProfitLossDetailGrid.Focused)
                        {
                            mainForm.SendToExcel(ref ProfitLossDetailGrid, false);
                        }
                        break;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void SendToEmailCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                switch (ReportingDockingTab.SelectedIndex)
                {
                    case 0:
                    case 1:
                        if (CreditDebitGrid.Focused)
                        {
                            mainForm.SendToEmail(ref CreditDebitGrid);
                        }
                        else if (CreditDebitNewContractsGrid.Focused)
                        {
                            mainForm.SendToEmail(ref CreditDebitNewContractsGrid);
                        }
                        else if (CreditDebitMarksGrid.Focused)
                        {
                            mainForm.SendToEmail(ref CreditDebitMarksGrid);
                        }
                        else if (CreditDebitReturnsGrid.Focused)
                        {
                            mainForm.SendToEmail(ref CreditDebitReturnsGrid);
                        }
                        break;

                    case 2:
                        if (ProfitLossGrid.Focused)
                        {
                            mainForm.SendToEmail(ref ProfitLossGrid);
                        }
                        else if (ProfitLossDetailGrid.Focused)
                        {
                            mainForm.SendToEmail(ref ProfitLossDetailGrid);
                        }
                        break;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void ProfitLossReload()
        {
            dsContracts = mainForm.PositionAgent.ContractResearchDataGet(StartPLDateEdit.Text, StopPLDateEdit.Text, BookGroupCombo.Text, "", "", "");
            Contracts.DistinctCurrencies(dsContracts, ref dsCurrencyIso, BookGroupCombo.Text);

            ProfitLossCurrencyIsoCombo.HoldFields();
            ProfitLossCurrencyIsoCombo.DataSource = dsCurrencyIso.Tables["CurrencyIso"];

            Contracts.SummaryByBookProfitLoss(ref dsContracts, ref dsContractSummary, BookGroupCombo.Text);

            dvContracts = new DataView(dsContracts.Tables["Contracts"], "CurrencyIso = '" + ProfitLossCurrencyIsoCombo.Text + "'", "", DataViewRowState.CurrentRows);
            dvContractSummary = new DataView(dsContractSummary.Tables["ContractSummary"], "CurrencyIso = '" + ProfitLossCurrencyIsoCombo.Text + "'", "", DataViewRowState.CurrentRows);
            ProfitLossChart.DataSource = dvContractSummary;

            ProfitLossGrid.SetDataBinding(dvContractSummary, "", true);
            ProfitLossDetailGrid.SetDataBinding(dvContracts, "", true);
        }

        private void StartPLDateEdit_TextChanged(object sender, EventArgs e)
        {
            ProfitLossReload();
        }

        private void TransactionsDailyLoad()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                //dsTransactionsDaily = mainForm.PositionAgent.(mainForm.UtcOffset);
                

                SummaryCurrencyIsoCombo.HoldFields();
                SummaryCurrencyIsoCombo.DataSource = dsCurrencyIso.Tables["Transactions"];

                SummarySecurityRadioButton.Checked = true;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

    }
}
