using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using C1.C1Pdf;
using StockLoan.Common;

namespace Golden
{
    public partial class ShortSaleNegativeRebateBillingReportForm : Form
    {
        private MainForm mainForm = null;
        private string groupCode;
        private string tempPath;
        private string fileName;
        private DataSet dsTradingGroups;
        private bool isLoading;

        public ShortSaleNegativeRebateBillingReportForm(MainForm mainForm, string fromDate, string toDate, string groupCode)
        {
            InitializeComponent();
            try
            {
                isLoading = true;
                this.mainForm = mainForm;
                this.FromDatePicker.Text = fromDate;
                this.ToDatePicker.Text = toDate;
                this.groupCode = groupCode;
                this.MessageListBox.Items.Clear();

                tempPath = mainForm.RebateAgent.KeyValueGet("ShortSaleBillsLocation", @"\\penson.com\Shares\Apps\Sendero\Bills");
            }
            catch (Exception error)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(error.Message);
            }
            finally
            {
                isLoading = false;
            }
        }

        private void ShortSaleNegativeRebateBillingReportForm_Load(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;

            DateTime fromDate = (DateTime.Parse(this.FromDatePicker.Text.ToString()));
            DateTime toDate = (DateTime.Parse(this.ToDatePicker.Text.ToString()));
            GroupCodeComboFill(fromDate, toDate);

        }

        private void CreateBillButton_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            long groupCodeCount = 0;
            long secIdCount = 0;
            decimal totalCharges = 0;

            fileName = "";

            try
            {
                if (PensonRadio.Checked)
                {
                    tempPath = mainForm.RebateAgent.KeyValueGet("ShortSaleBillsLocationPenson", @"C:\Bills\Penson");
                    tempPath += "_" + FromDatePicker.Value.ToString("MMddyy") + "_" + ToDatePicker.Value.ToString("MMddyy") + @"\";
                }
                else
                {
                    tempPath = mainForm.RebateAgent.KeyValueGet("ShortSaleBillsLocationRidge", @"C:\Bills\Ridge");
                    tempPath += "_" + FromDatePicker.Value.ToString("MMddyy") + "_" + ToDatePicker.Value.ToString("MMddyy") + @"\";
                }

                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }

                if (GroupCodeCombo.Text.Equals("**ALL**"))
                {
                    // Create Bill for ALL Group Codes...  
                    string startDate = FromDatePicker.Value.ToString("MM-dd-yyyy");
                    string stopDate = ToDatePicker.Value.ToString("MM-dd-yyyy");
                    string platForm = (PensonRadio.Checked) ? "Penson" : "BroadRidge";
                    string billType = (PdfRadio.Checked ? "pdf" : "xls");

                    foreach (DataRow dr in dsTradingGroups.Tables["CorrespondentSummary"].Rows)
                    {
                        if (dr["GroupCode"].ToString().Equals("**ALL**"))
                        {
                            continue;
                        }
                        else
                        {
                            groupCodeCount = 0;
                            secIdCount = 0;
                            totalCharges = 0;
                            string groupCode = dr["GroupCode"].ToString().Trim();

                            mainForm.RebateAgent.ShortSaleBillingSummaryBillingReportGet(
                                FromDatePicker.Text.ToString(),
                                ToDatePicker.Text.ToString(),
                                dr["GroupCode"].ToString(),
                                ref groupCodeCount,
                                ref secIdCount,
                                ref totalCharges,
                                (PensonRadio.Checked) ? "Penson" : "BroadRidge");

                            if (secIdCount > 0) 
                            {
                                fileName = tempPath + mainForm.RebateAgent.KeyValueGet("FileName", "Bill") + FromDatePicker.Value.ToString("MMddyy") + "_" + ToDatePicker.Value.ToString("MMddyy") + "_" + groupCode + (PdfRadio.Checked ? ".pdf" : ".xls");	
                                if (File.Exists(fileName))
                                {
                                    File.Delete(fileName);
                                }

                                RebateBillingDocument.ShortSaleSummaryCorrespondentBillCreate(mainForm, fileName, billType, platForm, startDate, stopDate, groupCode);

                                if (billType.ToString().Trim().ToLower().Equals("pdf"))
                                {
                                    ListBoxWrite("Wrote bill PDF file to : " + fileName);
                                }
                                else
                                {
                                    ListBoxWrite("Wrote bill Excel file to : " + fileName);
                                }
                            }	

                        }	

                    }	
                    this.Cursor = Cursors.Default;

                    Log.Write("Create bill for all GroupCodes Done. [ShortSaleNegativeRebateBillingReportForm.CreateBillButton_Click]", Log.Information, 2);
                }
                else  
                {
                    //Create Bill for individually selected Group Code in GroupCodeCombo... 
                    groupCodeCount = 0;
                    secIdCount = 0;
                    totalCharges = 0;

                    string startDate = FromDatePicker.Value.ToString("MM-dd-yyyy");
                    string stopDate = ToDatePicker.Value.ToString("MM-dd-yyyy");
                    string platForm = (PensonRadio.Checked) ? "Penson" : "BroadRidge";
                    string billType = (PdfRadio.Checked ? "pdf" : "xls");
                    string groupCode = GroupCodeCombo.Text.ToString().Trim();

                    mainForm.RebateAgent.ShortSaleBillingSummaryBillingReportGet(
                        FromDatePicker.Text.ToString(),
                        ToDatePicker.Text.ToString(),
                        GroupCodeCombo.Text,
                            ref groupCodeCount,
                            ref secIdCount,
                            ref totalCharges,
                            platForm);

                    if (secIdCount > 0)
                    {
                        fileName = tempPath + mainForm.RebateAgent.KeyValueGet("FileName", "Bill") + FromDatePicker.Value.ToString("MMddyy") + "_" + ToDatePicker.Value.ToString("MMddyy") + "_" + groupCode + (PdfRadio.Checked ? ".pdf" : ".xls");		
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }

                        RebateBillingDocument.ShortSaleSummaryCorrespondentBillCreate(mainForm, fileName, billType, platForm, startDate, stopDate, groupCode);

                        if (billType.ToString().Trim().ToLower().Equals("pdf"))
                        {
                            ListBoxWrite("Wrote bill PDF file to : " + fileName);
                        }
                        else
                        {
                            ListBoxWrite("Wrote bill Excel file to : " + fileName);
                        }
                    }
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception error)
            {
                ListBoxWrite(error.Message);
                Log.Write(error.Message + "  [ShortSaleNegativeRebateBillingReportForm.CreateBillButton_Click]", Log.Error, 1);
            }

            // Now both correspondent bills should be taken care of. Time to look at the Master
            if (!ExcludeMasterBillCheckBox.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                try						// Switch output file between PDF or Excel based on RadioButton 
                {
                    string startDate = FromDatePicker.Value.ToString("MM-dd-yyyy");
                    string stopDate = ToDatePicker.Value.ToString("MM-dd-yyyy");
                    string platForm = (PensonRadio.Checked) ? "Penson" : "BroadRidge";
                    string billType = (PdfRadio.Checked ? "pdf" : "xls");

                    fileName = tempPath + mainForm.RebateAgent.KeyValueGet("FileName", "Bill") + FromDatePicker.Value.ToString("MMddyy") + "_" + ToDatePicker.Value.ToString("MMddyy") + "_MASTERBILL" + (PdfRadio.Checked ? ".pdf" : ".xls");	
                    //fileName = tempPath + "ShortSaleNegativeBill_" + FromDatePicker.Value.ToString("MMddyy") + "_" + ToDatePicker.Value.ToString("MMddyy") + "_MASTERBILL" + (PdfRadio.Checked ? ".pdf" : ".xls");	
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    RebateBillingDocument.ShortSaleSummaryMasterBillCreate(mainForm, fileName, billType, platForm, startDate, stopDate);
                    
                    if (billType.ToString().Trim().ToLower().Equals("pdf"))
                    {
                        ListBoxWrite("Wrote Master Bill PDF file to : " + fileName);
                        ListBoxWrite("Master Bill Done.");
                    }
                    else
                    {
                        ListBoxWrite("Wrote Master Bill Excel file to : " + fileName);
                        ListBoxWrite("Master Bill Done.");
                    }

                    Log.Write("Master Bill Done. [ShortSaleNegativeRebateBillingReportForm.CreateBillButton_Click]", Log.Information, 2);
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [ShortSaleNegativeRebateBillingReportForm.CreateBillButton_Click]", Log.Error, 1);
                    ListBoxWrite(error.Message);
                }
                this.Cursor = Cursors.Default;
            }
            Process.Start(tempPath);
        }

        private void ListBoxWrite(string message)
        {
            try
            {
                MessageListBox.Items.Add(DateTime.Now.ToString(Standard.DateTimeShortFormat) + " " + message);
                MessageListBox.Refresh();
            }
            catch { }
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dsTradingGroups = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
                     this.FromDatePicker.Text,
                     this.ToDatePicker.Text,
                    "",
                    (PensonRadio.Checked) ? "Penson" : "BroadRidge");

                DataRow tempRow = dsTradingGroups.Tables["CorrespondentSummary"].NewRow();
                tempRow["GroupCode"] = "**ALL**";

                dsTradingGroups.Tables["CorrespondentSummary"].Rows.Add(tempRow);

                GroupCodeCombo.DataSource = dsTradingGroups;
                GroupCodeCombo.DataMember = "CorrespondentSummary";
                GroupCodeCombo.HoldFields();
                GroupCodeCombo.Text = "**ALL**";
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FromDatePicker_ValueChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DateTime fromDate;
            DateTime toDate;

            if (this.FromDatePicker.Text.ToString().Equals(""))
            {
                fromDate = DateTime.Now;
            }
            else
            {
                fromDate = (DateTime.Parse(this.FromDatePicker.Text.ToString()));
            }

            if (this.ToDatePicker.Text.ToString().Equals(""))
            {
                toDate = DateTime.Now;
            }
            else
            {
                toDate = (DateTime.Parse(this.ToDatePicker.Text.ToString()));
            }

            GroupCodeComboFill(fromDate, toDate);

            this.Cursor = Cursors.Default;
        }    

        private void ToDatePicker_ValueChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DateTime fromDate;
            DateTime toDate;

            if (this.FromDatePicker.Text.ToString().Equals(""))
            {
                fromDate = DateTime.Now;
            }
            else
            {
                fromDate = (DateTime.Parse(this.FromDatePicker.Text.ToString()));
            }

            if (this.ToDatePicker.Text.ToString().Equals(""))
            {
                toDate = DateTime.Now;
            }
            else
            {
                toDate = (DateTime.Parse(this.ToDatePicker.Text.ToString()));
            }

            GroupCodeComboFill(fromDate, toDate);
            isLoading = false;
            this.Cursor = Cursors.Default;
        }

        private void GroupCodeComboFill(DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (isLoading == false)
                {
                    if (fromDate.CompareTo(toDate) == 1) //toDate greater than or equal to fromDate - not allowed
                    {
                        MessageBox.Show("You must select a FromDate prior to the ToDate");
                        GroupCodeCombo.ClearFields();
                    }
                    else
                    {
                        dsTradingGroups = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
                             fromDate.ToString(),
                             toDate.ToString(),
                            "",
                            (PensonRadio.Checked) ? "Penson" : "BroadRidge");

                        DataRow tempRow = dsTradingGroups.Tables["CorrespondentSummary"].NewRow();
                        tempRow["GroupCode"] = "**ALL**";

                        dsTradingGroups.Tables["CorrespondentSummary"].Rows.Add(tempRow);

                        GroupCodeCombo.DataSource = dsTradingGroups;
                        GroupCodeCombo.DataMember = "CorrespondentSummary";
                        GroupCodeCombo.HoldFields();
                        GroupCodeCombo.Text = "**ALL**";
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

    }
}
