using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

using StockLoan.Common;
using RebateBillingBusiness;

namespace Golden
{
    public partial class ShortSaleNegativeRebateHistoryLookupForm : Form
    {
        private MainForm mainForm;
        private DataSet dsBillingSummary;

        private string groupCode;
        private string accountNumber;
        private DataSet dsTradingGroups;


        public ShortSaleNegativeRebateHistoryLookupForm(MainForm mainForm)
        {
            InitializeComponent();
            try
            {
                this.mainForm = mainForm;

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

        }

        private void ShortSaleNegativeRebateHistoryLookupForm_Load(object sender, EventArgs e)
        {
            int height = mainForm.Height / 2;
            int width = mainForm.Width / 2;

            try
            {
                this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
                this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
                this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
                this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

                groupCode = "";
                accountNumber = "";

                dsTradingGroups = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
                                    DateTime.Parse(this.FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                    DateTime.Parse(this.ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                    "",
                                    (PensonRadio.Checked) ? "Penson" : "BroadRidge");

                DataRow tempRow = dsTradingGroups.Tables["CorrespondentSummary"].NewRow();
                tempRow["GroupCode"] = "**ALL**";
                dsTradingGroups.Tables["CorrespondentSummary"].Rows.Add(tempRow);

                GroupCodeCombo.HoldFields();
                GroupCodeCombo.DataSource = dsTradingGroups;
                GroupCodeCombo.DataMember = "CorrespondentSummary";
                GroupCodeCombo.Text = "**ALL**";

                RadioDetail.Checked = true;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void ShortSaleNegativeRebateHistoryLookupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

            mainForm.rebateBillingHistoryLookupForm = null;
        }

        private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                Excel excel = new Excel();
                Excel.ExportGridToExcel(ref SummaryGrid, mainForm.TempPath + Standard.ProcessId() + ".xls" ,0, null);
                /*string fileName = mainForm.TempPath + Standard.ProcessId() + ".xls";
                SummaryGrid.ExportToExcel(fileName);
                Process.Start(fileName);*/
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void LookupButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                groupCode = GroupCodeCombo.Text.Trim().Equals("**ALL**") ? "" : GroupCodeCombo.Text;				
                accountNumber = AccountNumberTextBox.Text.Trim().Equals("") ? "" : AccountNumberTextBox.Text;		

                dsBillingSummary = mainForm.RebateAgent.ShortSaleBillingSummaryGet(
                                  DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                  DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                  groupCode,																		
                                  accountNumber,
                                  ((PensonRadio.Checked) ? "Penson" : "BroadRidge"));


                if (RadioByAccountNumber.Checked)
                {
                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryByAccountNumber(dsBillingSummary), "Rebate", true);
                }

                if (RadioByGroupCode.Checked)
                {
                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryByGroupCode(dsBillingSummary), "Rebate", true);
                }

                if (RadioBySecurity.Checked)
                {
                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryBySecurity(dsBillingSummary), "Rebate", true);
                }

                if (RadioDetail.Checked)
                {
                    SummaryGrid.SetDataBinding(dsBillingSummary, "Rebate", true);
                }

                if (RadioByCharges.Checked)
                {
                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryByCharges(dsBillingSummary), "Rebate", true);
                }

                SummaryFooterSet();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            this.Cursor = Cursors.Default;
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

                    case ("SumOfCharges"):
                        e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
                        break;

                }
            }
            catch { }
        }

        private void SummaryFooterSet()
        {
            decimal originalCharge = 0;
            decimal modifiedCharge = 0;
            decimal sumOfCharge = 0;

            for (int index = 0; index < SummaryGrid.Splits[0].Rows.Count; index++)
            {
                try
                {
                    if (!SummaryGrid.Columns["OriginalCharge"].CellValue(index).ToString().Equals(""))
                        originalCharge += decimal.Parse(SummaryGrid.Columns["OriginalCharge"].CellValue(index).ToString());
                }
                catch { }
                try
                {
                    if (!SummaryGrid.Columns["ModifiedCharge"].CellValue(index).ToString().Equals(""))
                        modifiedCharge += decimal.Parse(SummaryGrid.Columns["ModifiedCharge"].CellValue(index).ToString());
                }
                catch { }
                try
                {   //DC  2010-12-28
                    if (!SummaryGrid.Columns["SumOfCharges"].CellValue(index).ToString().Equals(""))
                        sumOfCharge += decimal.Parse(SummaryGrid.Columns["SumOfCharges"].CellValue(index).ToString());
                }
                catch { }
            }


            SummaryGrid.Columns["OriginalCharge"].FooterText = originalCharge.ToString("#,##0.00");
            SummaryGrid.Columns["ModifiedCharge"].FooterText = modifiedCharge.ToString("#,##0.00");
            SummaryGrid.Columns["SumOfCharges"].FooterText = sumOfCharge.ToString("#,##0.00");    
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {	// Code copied from ShortSaleNegativeRebateBillingReportForm 

            this.Cursor = Cursors.WaitCursor;

            try
            {	// Reload GroupCodeCombo 
                dsTradingGroups = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
                                    DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                    DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                    "",
                                    (PensonRadio.Checked) ? "Penson" : "BroadRidge");

                DataRow tempRow = dsTradingGroups.Tables["CorrespondentSummary"].NewRow();
                tempRow["GroupCode"] = "**ALL**";

                dsTradingGroups.Tables["CorrespondentSummary"].Rows.Add(tempRow);

                GroupCodeCombo.DataSource = dsTradingGroups;
                GroupCodeCombo.DataMember = "CorrespondentSummary";
                GroupCodeCombo.HoldFields();
                GroupCodeCombo.Text = "**ALL**";

                // Reload SummaryGrid 
                groupCode = GroupCodeCombo.Text.Trim().Equals("**ALL**") ? "" : GroupCodeCombo.Text;
                accountNumber = "";

                dsBillingSummary = mainForm.RebateAgent.ShortSaleBillingSummaryGet(
                                  DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                  DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                  groupCode,															
                                  accountNumber,													
                                  ((PensonRadio.Checked) ? "Penson" : "BroadRidge"));

                if (RadioByAccountNumber.Checked)
                {
                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryByAccountNumber(dsBillingSummary), "Rebate", true);
                }

                if (RadioByGroupCode.Checked)
                {
                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryByGroupCode(dsBillingSummary), "Rebate", true);
                }

                if (RadioBySecurity.Checked)
                {
                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryBySecurity(dsBillingSummary), "Rebate", true);
                }

                if (RadioDetail.Checked)
                {
                    SummaryGrid.SetDataBinding(dsBillingSummary, "Rebate", true);
                }

                if (RadioByCharges.Checked)
                {   //DC
                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryByCharges(dsBillingSummary), "Rebate", true);
                }

                SummaryFooterSet();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {	// Code copied from ShortSaleNegativeRebateBillingReportForm 

            this.Cursor = Cursors.WaitCursor;

            try
            {
                groupCode = GroupCodeCombo.Text.Trim().Equals("**ALL**") ? "" : GroupCodeCombo.Text;				
                accountNumber = AccountNumberTextBox.Text.Trim().Equals("") ? "" : AccountNumberTextBox.Text;		

                // Reload GroupCodeCombo with new dates 
                dsTradingGroups = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
                                    DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                    DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                    "",									
                                    (PensonRadio.Checked) ? "Penson" : "BroadRidge");

                DataRow tempRow = dsTradingGroups.Tables["CorrespondentSummary"].NewRow();
                tempRow["GroupCode"] = "**ALL**";

                dsTradingGroups.Tables["CorrespondentSummary"].Rows.Add(tempRow);

                GroupCodeCombo.DataSource = dsTradingGroups;
                GroupCodeCombo.DataMember = "CorrespondentSummary";
                GroupCodeCombo.HoldFields();
                GroupCodeCombo.Text = groupCode.Equals("") ? "**ALL**" : groupCode;		


                // Reload SummaryGrid with new dates 
                dsBillingSummary = mainForm.RebateAgent.ShortSaleBillingSummaryGet(
                                  DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                  DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                  groupCode,															
                                  accountNumber,														
                                  ((PensonRadio.Checked) ? "Penson" : "BroadRidge"));

                if (RadioByAccountNumber.Checked)
                {
                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryByAccountNumber(dsBillingSummary), "Rebate", true);
                }

                if (RadioByGroupCode.Checked)
                {
                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryByGroupCode(dsBillingSummary), "Rebate", true);
                }

                if (RadioBySecurity.Checked)
                {
                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryBySecurity(dsBillingSummary), "Rebate", true);
                }

                if (RadioDetail.Checked)
                {
                    SummaryGrid.SetDataBinding(dsBillingSummary, "Rebate", true);
                }

                if (RadioByCharges.Checked)
                {   //DC    2010-12-28
                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryByCharges(dsBillingSummary), "Rebate", true);
                }

                SummaryFooterSet();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            this.Cursor = Cursors.Default;

        }

        private void RadioDetail_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (RadioDetail.Checked)
                {
                    SummaryGrid.Splits[0].DisplayColumns["BizDate"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Group Code"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Security ID"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Symbol"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Account Number"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Quantity Shorted"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Quantity Covered"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Quantity Uncovered"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Charge Before Mark"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Markup Rate"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Charge After Mark"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Sum of Charges"].Visible = false;

                    groupCode = GroupCodeCombo.Text.Trim().Equals("**ALL**") ? "" : GroupCodeCombo.Text;
                    accountNumber = AccountNumberTextBox.Text.Trim().Equals("") ? "" : AccountNumberTextBox.Text;


                    dsBillingSummary = mainForm.RebateAgent.ShortSaleBillingSummaryGet(
                                  DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                  DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                  groupCode,
                                  accountNumber,
                                  ((PensonRadio.Checked) ? "Penson" : "BroadRidge"));
                                       
                    
                    SummaryGrid.SetDataBinding(dsBillingSummary, "Rebate", true);                   
                }

                SummaryFooterSet();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void RadioBySecurity_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (RadioBySecurity.Checked)
                {
                    SummaryGrid.Splits[0].DisplayColumns["BizDate"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Group Code"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Security ID"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Symbol"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Account Number"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Quantity Shorted"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Quantity Covered"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Quantity Uncovered"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Charge Before Mark"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Markup Rate"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Charge After Mark"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Sum of Charges"].Visible = false;

                    groupCode = GroupCodeCombo.Text.Trim().Equals("**ALL**") ? "" : GroupCodeCombo.Text;
                    accountNumber = AccountNumberTextBox.Text.Trim().Equals("") ? "" : AccountNumberTextBox.Text;


                    dsBillingSummary = mainForm.RebateAgent.ShortSaleBillingSummaryGet(
                                  DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                  DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                  groupCode,
                                  accountNumber,
                                  ((PensonRadio.Checked) ? "Penson" : "BroadRidge"));


                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryBySecurity(dsBillingSummary), "Rebate", true);                   
                }

                SummaryFooterSet();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void RadioByAccountNumber_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (RadioByAccountNumber.Checked)
                {
                    SummaryGrid.Splits[0].DisplayColumns["BizDate"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Group Code"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Security ID"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Symbol"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Account Number"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Quantity Shorted"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Quantity Covered"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Quantity Uncovered"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Charge Before Mark"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Markup Rate"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Charge After Mark"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Sum of Charges"].Visible = false;


                    groupCode = GroupCodeCombo.Text.Trim().Equals("**ALL**") ? "" : GroupCodeCombo.Text;
                    accountNumber = AccountNumberTextBox.Text.Trim().Equals("") ? "" : AccountNumberTextBox.Text;


                    dsBillingSummary = mainForm.RebateAgent.ShortSaleBillingSummaryGet(
                                  DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                  DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                  groupCode,
                                  accountNumber,
                                  ((PensonRadio.Checked) ? "Penson" : "BroadRidge"));

                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryByAccountNumber(dsBillingSummary), "Rebate", true);
                }

                SummaryFooterSet();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void RadioByGroupCode_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (RadioByGroupCode.Checked)
                {
                    SummaryGrid.Splits[0].DisplayColumns["BizDate"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Group Code"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Security ID"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Symbol"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Account Number"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Quantity Shorted"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Quantity Covered"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Quantity Uncovered"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Charge Before Mark"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Markup Rate"].Visible = false;
                    SummaryGrid.Splits[0].DisplayColumns["Charge After Mark"].Visible = true;
                    SummaryGrid.Splits[0].DisplayColumns["Sum of Charges"].Visible = false;

                    groupCode = GroupCodeCombo.Text.Trim().Equals("**ALL**") ? "" : GroupCodeCombo.Text;
                    accountNumber = AccountNumberTextBox.Text.Trim().Equals("") ? "" : AccountNumberTextBox.Text;


                    dsBillingSummary = mainForm.RebateAgent.ShortSaleBillingSummaryGet(
                                  DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                  DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                  groupCode,
                                  accountNumber,
                                  ((PensonRadio.Checked) ? "Penson" : "BroadRidge"));


                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryByGroupCode(dsBillingSummary), "Rebate", true);
                }

                SummaryFooterSet();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void RadioByCharges_CheckedChanged(object sender, EventArgs e)
        {   //DC new 2010-12-27

            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (RadioByCharges.Checked)
                {
                   SummaryGrid.Splits[0].DisplayColumns["BizDate"].Visible = false;
                   SummaryGrid.Splits[0].DisplayColumns["Group Code"].Visible = true;
                   SummaryGrid.Splits[0].DisplayColumns["Security ID"].Visible = false;
                   SummaryGrid.Splits[0].DisplayColumns["Symbol"].Visible = false;
                   SummaryGrid.Splits[0].DisplayColumns["Account Number"].Visible = true;
                   SummaryGrid.Splits[0].DisplayColumns["Quantity Shorted"].Visible = false;
                   SummaryGrid.Splits[0].DisplayColumns["Quantity Covered"].Visible = false;
                   SummaryGrid.Splits[0].DisplayColumns["Quantity Uncovered"].Visible = false;
                   SummaryGrid.Splits[0].DisplayColumns["Charge Before Mark"].Visible = false;
                   SummaryGrid.Splits[0].DisplayColumns["Markup Rate"].Visible = false;
                   SummaryGrid.Splits[0].DisplayColumns["Charge After Mark"].Visible = false;
                   SummaryGrid.Splits[0].DisplayColumns["Sum of Charges"].Visible = true;

                    groupCode = GroupCodeCombo.Text.Trim().Equals("**ALL**") ? "" : GroupCodeCombo.Text;
                    accountNumber = AccountNumberTextBox.Text.Trim().Equals("") ? "" : AccountNumberTextBox.Text;


                    dsBillingSummary = mainForm.RebateAgent.ShortSaleBillingSummaryGet(
                                  DateTime.Parse(FromDatePicker.Text).ToString("yyyy-MM-dd"),
                                  DateTime.Parse(ToDatePicker.Text).ToString("yyyy-MM-dd"),
                                  groupCode,
                                  accountNumber,
                                  ((PensonRadio.Checked) ? "Penson" : "BroadRidge"));


                    SummaryGrid.SetDataBinding(BillingBySummary.SummaryByCharges(dsBillingSummary), "Rebate", true);
                }

                SummaryFooterSet();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            this.Cursor = Cursors.Default;

        }


    }
}
