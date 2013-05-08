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
    public partial class TradingReportingBillingForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private MainForm mainForm;
        private DataSet dsContracts;
        private DataSet dsContractSummary;
        private DataSet dsBookGroups;
        private DataSet dsBooks;
        private DataSet dsCurrencyIso;
        private DataView dvContracts;
        private DataView dvContractSummary;
        private string contractId = "";

        public TradingReportingBillingForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

      
        private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (BillingSummaryGrid.Focused)
                {
                    mainForm.SendToExcel(ref BillingSummaryGrid, false);
                }
                else
                {
                    mainForm.SendToExcel(ref BillingGrid, false);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void SubmitRibbonButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsContracts = mainForm.PositionAgent.ContractResearchDataGet(
                    StartPLDateEdit.Text,
                    StopPLDateEdit.Text,
                    BookGroupCombo.Text,
                    BookCombo.Text,
                    "",
                    "");

                Contracts.SummaryByBookProfitLoss(ref dsContracts, ref dsContractSummary, BookGroupCombo.Text);
                Contracts.PopulateTermDate(ref dsContracts);

                dsContractSummary = new DataSet();

                Contracts.SummaryByContractsBilling(ref dsContracts, ref dsContractSummary);
                
                dvContracts = new DataView(dsContracts.Tables["Contracts"], "CurrencyIso = '' AND ContractId = ''", "", DataViewRowState.CurrentRows);
                dvContractSummary = new DataView(dsContractSummary.Tables["ContractSummary"], "CurrencyIso = '" +  CurrencyIsoCombo.Text + "'", "", DataViewRowState.CurrentRows);
                
                BillingGrid.SetDataBinding(dvContracts, "", true);
                BillingSummaryGrid.SetDataBinding(dvContractSummary, "", true);

                SummaryFooterLoad();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void TradingReportingBillingForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsBookGroups = mainForm.ServiceAgent.BookGroupGet(mainForm.UserId);
                dsCurrencyIso = mainForm.ServiceAgent.CurrenciesGet();

                BookGroupCombo.HoldFields();
                BookGroupCombo.DataSource = dsBookGroups.Tables["BookGroups"];

                CurrencyIsoCombo.HoldFields();
                CurrencyIsoCombo.DataSource = dsCurrencyIso.Tables["Currencies"];

                StartPLDateEdit.Text = mainForm.ServiceAgent.BizDate();
                StopPLDateEdit.Text = mainForm.ServiceAgent.BizDate();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void BookGroupCombo_TextChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsBooks = mainForm.AdminAgent.BookGet(BookGroupCombo.Text, "");

                BookCombo.HoldFields();
                BookCombo.DataSource = dsBooks.Tables["Books"];
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void BillingGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            e.Value = mainForm.Format(e.Column.DataField, e.Value.ToString());
        }

        private void TradingReportingBillingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

            mainForm.tradingBillingForm = null;
        }

        private void BillingGrid_FetchCellStyle(object sender, C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs e)
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

        private void BillingSummaryGrid_Paint(object sender, PaintEventArgs e)
        {
            if (contractId != BillingSummaryGrid.Columns["ContractId"].Text)
            {
                dvContracts.RowFilter = "CurrencyIso = '" + CurrencyIsoCombo.Text + "' AND ContractId = '" + BillingSummaryGrid.Columns["ContractId"].Text + "'";
                contractId = BillingSummaryGrid.Columns["ContractId"].Text;
                FooterLoad();
            }
        }

        private void SummaryFooterLoad()
        {
            decimal totalRebate = 0;


            try
            {
                foreach (DataRowView dr in dvContractSummary)
                {
                    totalRebate += (decimal)dr["TotalRebate"];
                }

                BillingSummaryGrid.Columns["TotalRebate"].FooterText = totalRebate.ToString("#,##0.00");
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void FooterLoad()
        {
            decimal totalRebate = 0;


            try
            {
                foreach (DataRowView dr in dvContracts)
                {
                    totalRebate += (decimal)dr["PL"];
                }

                BillingGrid.Columns["PL"].FooterText = totalRebate.ToString("#,##0.00");
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void HistoryOnDemandCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            string contractId = "";

            try
            {
                contractId = BillingGrid.Columns["ContractId"].Text;

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

        private void CreateBillingCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                string fileName = Standard.ConfigValue("TempPath") + BookCombo.Text + "_" + Standard.ProcessId() + ".xls";
                
                ExcelBillingFormat excelBillingFormat = new ExcelBillingFormat();
                excelBillingFormat.ExcelBillingBookOpen(fileName);

                for (int index = 0; index < BillingSummaryGrid.RowCount; index++)
                {
                    dvContracts.RowFilter = "CurrencyIso = '" + CurrencyIsoCombo.Text + "' AND ContractId = '" + BillingSummaryGrid.Columns["ContractId"].CellText(index) + "'";
                    FooterLoad();
                    excelBillingFormat.ExcelBillingBookAddSheet(((DataRowView)BillingSummaryGrid[index]).Row, ref BillingGrid);
                }

                excelBillingFormat.ExcelBillingBookSave();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void MainContextMenu_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {

        }
    }
}
