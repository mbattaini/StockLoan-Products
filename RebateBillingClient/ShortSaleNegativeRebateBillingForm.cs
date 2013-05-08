using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using StockLoan.Common;

namespace Golden
{
    public partial class ShortSaleNegativeRebateBillingForm : Form
    {
        private MainForm mainForm;
        private DataSet dsRebateSummary = new DataSet();
        private DataSet dsSummary = new DataSet();
        private DataSet dsTradingGroups = new DataSet();
        private DataSet dsAccounts = new DataSet();	

        public ShortSaleNegativeRebateBillingForm(MainForm  mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;
        }

        private void ShortSaleNegativeRebateBillingForm_Load(object sender, EventArgs e)
        {
            int height = mainForm.Height / 2;
            int width = mainForm.Width / 2;

            try
            {
                this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
                this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
                this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
                this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));
                
                dsRebateSummary = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
                    DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                    DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                    "",
                    ((PensonRadio.Checked)?"Penson":"BroadRidge"));

                RebateSummaryGrid.SetDataBinding(dsRebateSummary, "CorrespondentSummary", true);
                RebateSummaryGrid.Update();

                RebateSumaryFooterSet();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void RebateSumaryFooterSet()
        {
            decimal borrowCharge = 0;
            decimal clientCharge = 0;

            for (int index = 0; index < RebateSummaryGrid.Splits[0].Rows.Count; index++)
            {
                try
                {
                    if (!RebateSummaryGrid.Columns["BorrowCharge"].CellValue(index).ToString().Equals(""))
                        borrowCharge += (decimal)RebateSummaryGrid.Columns["BorrowCharge"].CellValue(index);
                }
                catch { }
                try
                {
                    if (!RebateSummaryGrid.Columns["ClientCharge"].CellValue(index).ToString().Equals(""))
                        clientCharge += (decimal)RebateSummaryGrid.Columns["ClientCharge"].CellValue(index);
                }
                catch { }
            }

            RebateSummaryGrid.Columns["BorrowCharge"].FooterText = borrowCharge.ToString("#,##0.00");
            RebateSummaryGrid.Columns["ClientCharge"].FooterText = clientCharge.ToString("#,##0.00");
        }

        private void SummaryFooterSet()
        {
            decimal originalCharge = 0;
            decimal modifiedCharge = 0;

            for (int index = 0; index < SummaryGrid.Splits[0].Rows.Count; index++)
            {
                try
                {
                    if (!SummaryGrid.Columns["OriginalCharge"].CellValue(index).ToString().Equals(""))
                        originalCharge += (decimal)SummaryGrid.Columns["OriginalCharge"].CellValue(index);
                }
                catch { }
                try
                {
                    if (!SummaryGrid.Columns["ModifiedCharge"].CellValue(index).ToString().Equals(""))
                        modifiedCharge += (decimal)SummaryGrid.Columns["ModifiedCharge"].CellValue(index);
                }
                catch { }
            }


            SummaryGrid.Columns["OriginalCharge"].FooterText = originalCharge.ToString("#,##0.00");
            SummaryGrid.Columns["ModifiedCharge"].FooterText = modifiedCharge.ToString("#,##0.00");
        }

        private void SummaryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            if (e.Value.Length == 0)
            {
                return;
            }

            try
            {
                switch (SummaryGrid.Columns[e.ColIndex].DataField)
                {
                    case ("QuantityCovered"):
                    case ("QuantityShorted"):
                    case ("QuantityUncovered"):
                        e.Value = long.Parse(e.Value).ToString("#,##0");
                        break;

                    case ("MarkupRate"):
                    case ("Rate"):
                        e.Value = decimal.Parse(e.Value).ToString("#0.000");
                        break;

                    case ("Price"):
                        e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
                        break;

                    case ("BizDate"):
                    case ("SettlementDate"):
                        e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateFormat);
                        break;

                    case ("OriginalCharge"):
                        e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
                        break;

                    case ("ModifiedCharge"):
                        e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
                        break;
                }
            }
            catch { }
        }

        private void RebateSummaryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            if (e.Value.Length == 0)
            {
                return;
            }

            try
            {
                switch (RebateSummaryGrid.Columns[e.ColIndex].DataField)
                {
                    case ("ShortedAmount"):
                    case ("BorrowCharge"):
                    case ("ClientCharge"):
                        e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
                        break;
                }
            }
            catch { }
        }

        private void SummaryGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            if (SummaryGrid.Columns["IsLocked"].CellValue(e.Row).ToString().Equals("True"))
            {
                e.CellStyle.ForeColor = System.Drawing.Color.Gray;
            }
            else
            {
                e.CellStyle.ForeColor = System.Drawing.Color.Black;
            }

            if (SummaryGrid.Columns["PreBorrow"].CellValue(e.Row).ToString().Equals("True"))
            {
                e.CellStyle.BackColor = System.Drawing.Color.BlanchedAlmond;
            }
        }
      

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsRebateSummary = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
                                  DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                  DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                  "",
                                  ((PensonRadio.Checked) ? "Penson" : "BroadRidge"));

                RebateSummaryGrid.SetDataBinding(dsRebateSummary, "CorrespondentSummary", true);
                RebateSumaryFooterSet();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsRebateSummary = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
                                  DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                  DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                  "",
                                  ((PensonRadio.Checked) ? "Penson" : "BroadRidge"));

                RebateSummaryGrid.SetDataBinding(dsRebateSummary, "CorrespondentSummary", true);
                RebateSumaryFooterSet();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void RebateSummaryGrid_BeforeOpen(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            SummaryGrid.Width = RebateSummaryGrid.Width - 50;

            try
            {
                dsSummary = mainForm.RebateAgent.ShortSaleBillingSummaryGet(
                    FromDatePicker.Text, 
                    ToDatePicker.Text, 
                    RebateSummaryGrid.Columns["GroupCode"].Text, 
                    "",
                     ((PensonRadio.Checked) ? "Penson" : "BroadRidge"));

                SummaryGrid.SetDataBinding(dsSummary, "Rebate", true);
                SummaryFooterSet();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }


        private void CreateBillingCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                ShortSaleNegativeRebateBillingReportForm reportForm = new ShortSaleNegativeRebateBillingReportForm(
                    mainForm,
                    FromDatePicker.Text,
                    ToDatePicker.Text,
                    "");
                reportForm.MdiParent = mainForm;
                reportForm.Show();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void mnuSetMarkups_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                ShortSaleNegativeRebateMarkUpForm tradingForm = new ShortSaleNegativeRebateMarkUpForm(
                    mainForm);
                tradingForm.MdiParent = mainForm;
                tradingForm.Show();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {            
            if (RebateSummaryGrid.Focused)
            {
                RebateSummaryGrid.ExportToExcel("RebateGrid" + Standard.ProcessId() + ".xls");                
            }
            else if (SummaryGrid.Focused)
            {
                SummaryGrid.ExportToExcel("SummaryGrid" + Standard.ProcessId() + ".xls");
            }
        }

        private void ShortSaleNegativeRebateBillingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

            mainForm.rebateBillingForm = null;
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsRebateSummary = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
                                  DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                  DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                  "",
                                  ((PensonRadio.Checked) ? "Penson" : "BroadRidge"));

                RebateSummaryGrid.SetDataBinding(dsRebateSummary, "CorrespondentSummary", true);
                RebateSumaryFooterSet();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            this.Cursor = Cursors.Default;
        }
    }
}
