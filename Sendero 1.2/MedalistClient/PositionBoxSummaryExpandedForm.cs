using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Anetics.Common;

namespace Anetics.Medalist
{
    public enum ActivityType
    {
        Borrows,
        Recalls,
        Availability,
        Default
    };

    public partial class PositionBoxSummaryExpandedForm : Form
    {
        public MainForm mainForm;
        public DataSet dsBoxSummary;
        public DataSet dsBookGroup;
        public DataSet dsCombinedSummary;

        public DataView dvBookGroup1;
        public DataView dvBookGroup2;
        public DataView dvBookGroup1Combo;
        public DataView dvBookGroup2Combo;

        public DataView dvBookGroupCombined;

        private string dvBookGroup1Filter;
        private string dvBookGroup2Filter;
        private string dvBookGroupCombinedFilter;

        private string secId;
        private bool isReady = false;

        private void ChildDataLoad()
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            mainForm.Alert("Please wait... Loading current box summary.", PilotState.Idle);

            try
            {
                dsBoxSummary = mainForm.PositionAgent.BoxSummaryDataGet(mainForm.UtcOffset, mainForm.UserId, "PositionBoxSummary");
                Application.DoEvents();

                dsBoxSummary.Tables["BoxSummary"].Columns.Add("AvailableModified", typeof(long));
                dsBoxSummary.Tables["BoxSummary"].Columns.Add("AvailableAmount", typeof(long));
                dsBoxSummary.Tables["BoxSummary"].Columns.Add("PledgeUndo", typeof(long));
                dsBoxSummary.Tables["BoxSummary"].Columns.Add("Borrow", typeof(long));
                dsBoxSummary.Tables["BoxSummary"].Columns.Add("BorrowAmount", typeof(long));
                dsBoxSummary.Tables["BoxSummary"].Columns.Add("Recall", typeof(long));
                dsBoxSummary.Tables["BoxSummary"].Columns.Add("LockupAmount", typeof(long));

                dvBookGroup1 = new DataView(dsBoxSummary.Tables["BoxSummary"], "BookGroup = '" + BookGroup1Combo.Text + "'", "SecId", DataViewRowState.CurrentRows);
                dvBookGroup2 = new DataView(dsBoxSummary.Tables["BoxSummary"], "BookGroup = '" + BookGroup2Combo.Text + "'", "SecId", DataViewRowState.CurrentRows);

                BookGroup1Grid.SetDataBinding(dvBookGroup1, null, true);
                BookGroup2Grid.SetDataBinding(dvBookGroup2, null, true);

                dsBookGroup = new DataSet();
                dsBookGroup = dsBoxSummary.Copy();

                dvBookGroup1Combo = new DataView(dsBoxSummary.Tables["BookGroups"], "", "", DataViewRowState.CurrentRows);
                dvBookGroup2Combo = new DataView(dsBookGroup.Tables["BookGroups"], "", "", DataViewRowState.CurrentRows);

                BookGroup1Combo.HoldFields();
                BookGroup1Combo.DataSource = dvBookGroup1Combo;
                BookGroup1Combo.SelectedIndex = -1;

                BookGroup2Combo.HoldFields();
                BookGroup2Combo.DataSource = dvBookGroup2Combo;
                BookGroup2Combo.SelectedIndex = -1;


                BookGroup1NameLabel.DataSource = dvBookGroup1Combo;
                BookGroup1NameLabel.DataField = "BookName";

                BookGroup2NameLabel.DataSource = dvBookGroup2Combo;
                BookGroup2NameLabel.DataField = "BookName";

                ChildDataConfig();

                if (RegistryValue.Read(this.Name, "BookGroup1").Equals("") && RegistryValue.Read(this.Name, "BookGroup2").Equals(""))
                {
                    BookGroup1Combo.SelectedIndex = 0;
                    BookGroup2Combo.SelectedIndex = 0;
                }
                else
                {
                    BookGroup1Combo.Text = RegistryValue.Read(this.Name, "BookGroup1", "");
                    BookGroup2Combo.Text = RegistryValue.Read(this.Name, "BookGroup2", "");
                }
                secId = "";
                isReady = true;
                
                this.Cursor = Cursors.Default;
            }
            catch (Exception ee)
            {
                this.Cursor = Cursors.Default;
                mainForm.Alert(ee.Message, PilotState.RunFault);
                Log.Write(ee.Message + " [PositionBoxSummaryForm.PositionBoxSummaryForm_Load]", Log.Error, 1);
            }
        }

        private void CombinedDataConfig()
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsCombinedSummary = new DataSet();
                dsCombinedSummary.Tables.Add("CombinedSummary");

                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("BookGroup", typeof(string));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("SecId", typeof(string));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("Symbol", typeof(string));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("BaseType", typeof(string));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("ClassGroup", typeof(string));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("Available", typeof(long));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("ExDeficit", typeof(long));                
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("Borrow", typeof(long));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("BorrowAmount", typeof(long));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("DeliveryFails", typeof(long));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("ReceiveFails", typeof(long));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("LockUp", typeof(long));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("Loans", typeof(long));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("Recall", typeof(long));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("IsEasy", typeof(bool));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("IsHard", typeof(bool));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("IsNoLend", typeof(bool));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("IsThreshold", typeof(bool));
                dsCombinedSummary.Tables["CombinedSummary"].Columns.Add("LastPrice", typeof(decimal));
                
                dsCombinedSummary.Tables["CombinedSummary"].AcceptChanges();

                CombineSummaries(dsCombinedSummary.Tables["CombinedSummary"], dsBoxSummary.Tables["BoxSummary"], BookGroup1Combo.Text);
                CombineSummaries(dsCombinedSummary.Tables["CombinedSummary"], dsBoxSummary.Tables["BoxSummary"], BookGroup2Combo.Text);

                dvBookGroupCombinedFilter = "BookGroup = 'CMBD'";
                dvBookGroupCombined = new DataView(dsCombinedSummary.Tables["CombinedSummary"], dvBookGroupCombinedFilter, "", DataViewRowState.CurrentRows);

                CombinedBookGroupGrid.SetDataBinding(dvBookGroupCombined, "", true);
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + " [PositionBoxSummaryForm.CombinedDataConfig]", Log.Error, 1);
            }

            this.Cursor = Cursors.Default;
        }

        private void CombineSummaries(DataTable dtBookGroupDest, DataTable dtBookGroupSource, string bookGroupSource)
        {
            bool isFound = false;

            foreach (DataRow drSourceItem in dtBookGroupSource.Rows)
            {
                if (drSourceItem["BookGroup"].ToString().Equals(bookGroupSource))
                {
                    isFound = false;

                    foreach (DataRow drDestItem in dtBookGroupDest.Rows)
                    {
                        if (drSourceItem["SecId"].ToString().Trim().Equals(drDestItem["SecId"].ToString().Trim()))
                        {
                            drDestItem["Available"] = long.Parse(drSourceItem["Available"].ToString()) + long.Parse(drDestItem["Available"].ToString());
                            drDestItem["ExDeficit"] = long.Parse(drSourceItem["ExDeficit"].ToString()) + long.Parse(drDestItem["ExDeficit"].ToString());
                            drDestItem["Borrow"] = long.Parse(drSourceItem["Borrow"].ToString()) + long.Parse(drDestItem["Borrow"].ToString());                            
                            drDestItem["ReceiveFails"] = long.Parse(drSourceItem["ReceiveFails"].ToString()) + long.Parse(drDestItem["ReceiveFails"].ToString());
                            drDestItem["DeliveryFails"] = long.Parse(drSourceItem["DeliveryFails"].ToString()) + long.Parse(drDestItem["DeliveryFails"].ToString());
                            drDestItem["BorrowAmount"] = long.Parse(drSourceItem["BorrowAmount"].ToString()) + long.Parse(drDestItem["BorrowAmount"].ToString());            
                            drDestItem["Recall"] = long.Parse(drSourceItem["Recall"].ToString()) + long.Parse(drDestItem["Recall"].ToString());
                            drDestItem["LockUp"] = long.Parse(drSourceItem["LockUp"].ToString()) + long.Parse(drDestItem["LockUp"].ToString());
                            isFound = true;
                        }
                    }

                    if (!isFound)
                    {
                        DataRow drRowNew = dtBookGroupDest.NewRow();

                        drRowNew["BookGroup"] = "CMBD";
                        drRowNew["SecId"] = drSourceItem["SecId"];
                        drRowNew["Symbol"] = drSourceItem["Symbol"];
                        drRowNew["BaseType"] = drSourceItem["BaseType"];
                        drRowNew["ClassGroup"] = drSourceItem["ClassGroup"];
                        drRowNew["Available"] = drSourceItem["Available"];
                        drRowNew["ExDeficit"] = drSourceItem["ExDeficit"];                        
                        drRowNew["ReceiveFails"] = drSourceItem["ReceiveFails"];
                        drRowNew["DeliveryFails"] = drSourceItem["DeliveryFails"];
                        drRowNew["Borrow"] = drSourceItem["Borrow"];
                        drRowNew["BorrowAmount"] = drSourceItem["BorrowAmount"];
                        drRowNew["Recall"] = drSourceItem["Recall"];
                        drRowNew["IsEasy"] = drSourceItem["IsEasy"];
                        drRowNew["IsHard"] = drSourceItem["IsHard"];
                        drRowNew["IsNoLend"] = drSourceItem["IsNoLend"];
                        drRowNew["IsThreshold"] = drSourceItem["IsThreshold"];
                        drRowNew["LastPrice"] = drSourceItem["LastPrice"];
                        drRowNew["LockUp"] = drSourceItem["LockUp"];

                        dtBookGroupDest.Rows.Add(drRowNew);
                    }
                }

                dtBookGroupDest.AcceptChanges();
            }
        }



        private void ChildDataConfig()
        {
            string secId = "";

            long available;
            long recallBalance;
            long deliveryBalance = 0;

            double hairCut = 0;

            try
            {
                hairCut = double.Parse(mainForm.ServiceAgent.KeyValueGet("InventoryHairCut", "10"));
                hairCut = hairCut / 100;
            }
            catch
            {
                hairCut = 0;
            }

            try
            {
                foreach (DataRow row in dsBoxSummary.Tables["BoxSummary"].Rows)
                {
                    secId = "[" + row["SecId"].ToString().Trim() + "] "; // Value captured here for error reporting only.

                    recallBalance = (long)row["LoanRecalls"] - (long)row["BorrowRecalls"];

                    if (secId.Equals("[001630102] "))
                    {
                        Log.Write("test", 1);
                    }

                    available = long.Parse(row["Available"].ToString());
                    available = available + deliveryBalance;

                    if (available <= 0) // Must move securities in.
                    {
                        if ((available + long.Parse(row["OnPledge"].ToString())) >= 0)
                        {
                            row["PledgeUndo"] = -available;
                            //available = 0;
                        }
                        else
                        {
                            row["PledgeUndo"] =  long.Parse(row["OnPledge"].ToString());
                            //available += (long)row["PledgeUndo"];
                        }

                        if ((bool)row["DoNotRecall"])
                        {
                            row["Recall"] = 0;
                        }
                        else
                        {
                            if ((available + ((long.Parse(row["Loans"].ToString()) - (long.Parse(row["LoanRecalls"].ToString())))) >= 0))
                            {
                                row["Recall"] = -(available);
                                //available = 0;
                            }
                            else if (available < 0)
                            {
                                row["Recall"] = (long.Parse(row["Loans"].ToString()) - (long.Parse(row["LoanRecalls"].ToString())));
                                //available += (long)row["Recall"]; 
                            }
                            else
                            {
                                row["Recall"] = 0;
                            }
                        }

                        row["Borrow"] = -(available);
                    }
                    else // Make returns and/or cancell recalls.
                    {
                        if (available >= (long.Parse(row["LoanRecalls"].ToString())))
                        {
                            row["Recall"] = -long.Parse(row["LoanRecalls"].ToString());

                        }
                        else if (available < (long.Parse(row["LoanRecalls"].ToString())))
                        {
                            row["Recall"] = -available;

                            if (row["BaseType"].ToString().Equals("B"))
                            {
                                row["Recall"] = ((long.Parse(row["Recall"].ToString()) - ((long.Parse(row["Recall"].ToString()) % 1000))));
                            }
                            else
                            {
                                row["Recall"] = (long.Parse(row["Recall"].ToString()) - ((long.Parse(row["Recall"].ToString()) % 100)));
                            }
                        }

                        if (available > long.Parse(row["Borrows"].ToString()))
                        {
                            row["Borrow"] = long.Parse(row["Borrows"].ToString()) * -1;
                        }
                        else if (available < long.Parse(row["Borrows"].ToString()))
                        {
                            row["Borrow"] = -available;
                            //available = 0;
                        }
                        else
                        {
                            row["Borrow"] = 0; 
                        }
                    }

                    if (long.Parse(row["Recall"].ToString()) > 0)
                    {
                        if (long.Parse(row["LoanRecalls"].ToString()) >= long.Parse(row["Recall"].ToString()))
                        {
                            row["Recall"] = 0;
                        }
                        else
                        {
                            row["Recall"] = long.Parse(row["Recall"].ToString()) - long.Parse(row["LoanRecalls"].ToString());
                        }
                    }
                    else if (long.Parse(row["Recall"].ToString()) < 0)
                    {
                        if (available > 0)
                        {
                            row["Recall"] = long.Parse(row["LoanRecalls"].ToString()) * -1;
                        }
                        else
                        {
                            row["Recall"] = available + long.Parse(row["Recall"].ToString());
                        }
                    }

                    if (long.Parse(row["Borrow"].ToString()) > 0)
                    {
                        if (row["BaseType"].ToString().Equals("B") && ((long.Parse(row["Borrow"].ToString()) % 1000) >= 0))
                        {
                            row["Borrow"] = long.Parse(row["Borrow"].ToString()) - (long.Parse(row["Borrow"].ToString()) % 1000) + 1000;
                        }
                        else if ((long.Parse(row["Borrow"].ToString()) % 100) >= 0)
                        {
                            row["Borrow"] = long.Parse(row["Borrow"].ToString()) - (long.Parse(row["Borrow"].ToString()) % 100) + 100;
                        }
                    }
                    else if (long.Parse(row["Borrow"].ToString()) < 0)
                    {
                        if (row["BaseType"].ToString().Equals("B"))
                        {
                            row["Borrow"] = long.Parse(row["Borrow"].ToString()) - (long.Parse(row["Borrow"].ToString()) % 1000);
                        }
                        else
                        {
                            row["Borrow"] = long.Parse(row["Borrow"].ToString()) - (long.Parse(row["Borrow"].ToString()) % 100);
                        }
                    }



                    row["AvailableModified"] = long.Parse(row["Available"].ToString()) - (long.Parse(row["Available"].ToString()) % 100);


                    if (!row["LastPrice"].Equals(DBNull.Value))
                    {
                        if (row["BaseType"].ToString().Equals("B"))
                        {
                            row["AvailableAmount"] = (long)(long.Parse(row["Available"].ToString()) * (double)row["LastPrice"] / 100.0);
                            row["BorrowAmount"] = (long)(long.Parse(row["Borrow"].ToString()) * (double)row["LastPrice"] / 100.0);
                            row["LockupAmount"] = (long.Parse(row["LockUp"].ToString()) * (double)row["LastPrice"] / 100.0);
                        }
                        else
                        {
                            row["AvailableAmount"] = (long)(long.Parse(row["Available"].ToString()) * (double)row["LastPrice"]);
                            row["BorrowAmount"] = (long)(long.Parse(row["Borrow"].ToString()) * (double)row["LastPrice"]);
                            row["LockupAmount"] = long.Parse(row["LockUp"].ToString()) * (double)row["LastPrice"];
                        }
                    }
                    else
                    {
                        row["AvailableAmount"] = DBNull.Value;
                        row["BorrowAmount"] = DBNull.Value;
                    }

                    


                }
            
                
            }
            catch (Exception e)
            {
                mainForm.Alert("[" + secId + "] " + e.Message, PilotState.RunFault);
                Log.Write("[" + secId + e.Message + "] " + " [PositionBoxSummaryForm.DataConfig]", Log.Error, 1);
            }
        }

        public PositionBoxSummaryExpandedForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void BookGroupCombo_TextChanged(object sender, EventArgs e)
        {
            dvBookGroup1Combo.RowFilter = "BookGroup <> '" + BookGroup2Combo.Text + "'";
            dvBookGroup2Combo.RowFilter = "BookGroup <> '" + BookGroup1Combo.Text + "'";
            
            dvBookGroup1Filter = "BookGroup = '" + BookGroup1Combo.Text + "'";
            dvBookGroup1.RowFilter = dvBookGroup1Filter;
            
            BookGroup1Grid.Caption = "Book Group " + BookGroup1Combo.Text;

            dvBookGroup2Filter = "BookGroup = '" + BookGroup2Combo.Text + "'";
            dvBookGroup2.RowFilter = dvBookGroup2Filter;

            BookGroup2Grid.Caption = "Book Group " + BookGroup2Combo.Text;

            CombinedDataConfig();            
        }

        private void PositionBoxSummaryExpandedForm_Load(object sender, EventArgs e)
        {
            ChildDataLoad();
        }
      
        private void CombinedBookGroupGrid_FilterChange(object sender, EventArgs e)
        {
            string gridFilter;

            if (!syncFiltersToolStripMenuItem.Checked)
            {
                try
                {
                    gridFilter = mainForm.GridFilterGet(ref CombinedBookGroupGrid);

                    if (gridFilter.Equals(""))
                    {
                        dvBookGroupCombined.RowFilter = dvBookGroupCombinedFilter;
                    }
                    else
                    {
                        dvBookGroupCombined.RowFilter = dvBookGroupCombinedFilter + " AND " + gridFilter;
                    }
                }
                catch (Exception ee)
                {
                    mainForm.Alert(ee.Message, PilotState.RunFault);
                }
            }
            else
            {
                try
                {
                    gridFilter = mainForm.GridFilterGet(ref CombinedBookGroupGrid);

                    if (gridFilter.Equals(""))
                    {
                        dvBookGroup1.RowFilter = dvBookGroup1Filter;
                        dvBookGroup2.RowFilter = dvBookGroup2Filter;
                        dvBookGroupCombined.RowFilter = dvBookGroupCombinedFilter;
                    }
                    else
                    {
                        dvBookGroup1.RowFilter = dvBookGroup1Filter + " AND " + gridFilter;
                        dvBookGroup2.RowFilter = dvBookGroup2Filter + " AND " + gridFilter;
                        dvBookGroupCombined.RowFilter = dvBookGroupCombinedFilter + " AND " + gridFilter;
                    }
                }
                catch (Exception ee)
                {
                    mainForm.Alert(ee.Message, PilotState.RunFault);
                }
            }
        }

        private void BookGroup1Grid_FilterChange(object sender, EventArgs e)
        {
            string gridFilter;

            if (!syncFiltersToolStripMenuItem.Checked)
            {
                try
                {
                    gridFilter = mainForm.GridFilterGet(ref BookGroup1Grid);

                    if (gridFilter.Equals(""))
                    {
                        dvBookGroup1.RowFilter = dvBookGroup1Filter;
                    }
                    else
                    {
                        dvBookGroup1.RowFilter = dvBookGroup1Filter + " AND " + gridFilter;
                    }
                }
                catch (Exception ee)
                {
                    mainForm.Alert(ee.Message, PilotState.RunFault);
                }
            }
            else
            {
                try
                {
                    gridFilter = mainForm.GridFilterGet(ref BookGroup1Grid);

                    if (gridFilter.Equals(""))
                    {
                        dvBookGroup1.RowFilter = dvBookGroup1Filter;
                        dvBookGroup2.RowFilter = dvBookGroup2Filter;
                        dvBookGroupCombined.RowFilter = dvBookGroupCombinedFilter;
                    }
                    else
                    {
                        dvBookGroup1.RowFilter = dvBookGroup1Filter + " AND " + gridFilter;
                        dvBookGroup2.RowFilter = dvBookGroup2Filter + " AND " + gridFilter;
                        dvBookGroupCombined.RowFilter = dvBookGroupCombinedFilter + " AND " + gridFilter;
                    }
                }
                catch (Exception ee)
                {
                    mainForm.Alert(ee.Message, PilotState.RunFault);
                }
            }
        }

        private void BookGroup2Grid_FilterChange(object sender, EventArgs e)
        {
            string gridFilter;

            if (!syncFiltersToolStripMenuItem.Checked)
            {
                try
                {
                    gridFilter = mainForm.GridFilterGet(ref BookGroup2Grid);

                    if (gridFilter.Equals(""))
                    {
                        dvBookGroup2.RowFilter = dvBookGroup2Filter;
                    }
                    else
                    {
                        dvBookGroup2.RowFilter = dvBookGroup2Filter + " AND " + gridFilter;
                    }
                }
                catch (Exception ee)
                {
                    mainForm.Alert(ee.Message, PilotState.RunFault);
                }
            }
            else
            {
                try
                {
                    gridFilter = mainForm.GridFilterGet(ref BookGroup2Grid);

                    if (gridFilter.Equals(""))
                    {
                        dvBookGroup1.RowFilter = dvBookGroup1Filter;
                        dvBookGroup2.RowFilter = dvBookGroup2Filter;
                        dvBookGroupCombined.RowFilter = dvBookGroupCombinedFilter;
                    }
                    else
                    {
                        dvBookGroup1.RowFilter = dvBookGroup1Filter + " AND " + gridFilter;
                        dvBookGroup2.RowFilter = dvBookGroup2Filter + " AND " + gridFilter;
                        dvBookGroupCombined.RowFilter = dvBookGroupCombinedFilter + " AND " + gridFilter;
                    }
                }
                catch (Exception ee)
                {
                    mainForm.Alert(ee.Message, PilotState.RunFault);
                }
            }
        }

        private void CombinedViewStripMenuItem_Click(object sender, EventArgs e)
        {
            CombinedBookGroupGrid.Visible = !CombinedViewStripMenuItem.Checked;
            Vsplitter.Visible = !CombinedViewStripMenuItem.Checked;
            CombinedViewStripMenuItem.Checked = !CombinedViewStripMenuItem.Checked;
        }

        private void BookGroup1StripMenuItem_Click(object sender, EventArgs e)
        {
            BookGroup1Grid.Visible = !BookGroup1StripMenuItem.Checked;
            Hsplitter.Visible = !BookGroup1StripMenuItem.Checked;
            BookGroup1StripMenuItem.Checked = !BookGroup1StripMenuItem.Checked;

            lrPanel.Visible = (BookGroup2StripMenuItem.Checked | BookGroup1StripMenuItem.Checked);

            if (lrPanel.Visible)
            {
                CombinedBookGroupGrid.Dock = DockStyle.Top;
            }
            else
            {
                CombinedBookGroupGrid.Dock = DockStyle.Fill;
            }

        }

        private void BookGroup2StripMenuItem_Click(object sender, EventArgs e)
        {
            BookGroup2Grid.Visible = !BookGroup2StripMenuItem.Checked;
            Hsplitter.Visible = !BookGroup2StripMenuItem.Checked;
            BookGroup2StripMenuItem.Checked = !BookGroup2StripMenuItem.Checked;

            lrPanel.Visible = (BookGroup2StripMenuItem.Checked | BookGroup1StripMenuItem.Checked);

            if (lrPanel.Visible)
            {
                CombinedBookGroupGrid.Dock = DockStyle.Top;
            }
            else
            {
                CombinedBookGroupGrid.Dock = DockStyle.Fill;
            }

            if (!BookGroup2StripMenuItem.Checked && BookGroup1Grid.Visible)
            {
                BookGroup1Grid.Dock = DockStyle.Fill;
            }
            else
            {
                BookGroup1Grid.Dock = DockStyle.Top;
            }
        }

        private void Grid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            if (e.Value.Equals(""))
            {
                return;
            }

            switch (e.Column.DataField)
            {
                case ("ActTime"):
                    try
                    {
                        e.Value = DateTime.Parse(e.Value).ToString(Standard.TimeFileFormat);
                    }
                    catch { }
                    break;
                case ("LastPrice"):               
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.000");
                    }
                    catch { }
                    break;

        
                case ("Available"):
                case ("RockAvailableModified"):
                case ("PensonAvailableModified"):
                    try
                    {
                        if (long.Parse(e.Value.ToString()) < 0)
                        {
                            e.Value = "0";
                        }
                        else
                        {
                            e.Value = long.Parse(e.Value).ToString("#,##0");
                        }
                    }
                    catch { }
                    break;

                case ("LockUp"):
                case ("ReceiveFails"):
                case ("DeliveryFails"):
                case ("Borrows"):
                case ("Loans"):
                case ("BorrowRecalls"):
                case ("LoanRecalls"):
                case ("OnPledge"):
                case ("PledgeUndo"):
                case ("LockupAmount"):
                case ("AvailableAmount"):
                case ("PensonAvailableAmount"):
                case ("RockAvailableAmount"):
                case ("ExDeficitAmount"):
                case ("Borrow"):
                case ("BorrowAmount"):
                case ("PensonBorrowModified"):
                case ("PensonBorrowAmount"):
                case ("RockBorrowModified"):
                case ("RockBorrowAmount"):
                case ("Recall"):
                case ("ExDeficit"):
                    try
                    {
                        e.Value = long.Parse(e.Value).ToString("#,##0");
                    }
                    catch { }
                    break;
            }
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Excel excel = new Excel();

            if (CombinedBookGroupGrid.Focused)
            {
                excel.ExportGridToExcel(ref CombinedBookGroupGrid);
            }
            else if (BookGroup1Grid.Focused)
            {
                excel.ExportGridToExcel(ref BookGroup1Grid);
            }
            else if (BookGroup2Grid.Focused)
            {
                excel.ExportGridToExcel(ref BookGroup2Grid);
            }
        }

        private void DisplayColumns(ActivityType type)
        {
            BookGroup1Grid.Splits[0].DisplayColumns["Recall"].Visible = (type == ActivityType.Recalls) ? true : false;
            BookGroup1Grid.Splits[0].DisplayColumns["ReceiveFails"].Visible = (type == ActivityType.Borrows) ? true : false;
            BookGroup1Grid.Splits[0].DisplayColumns["DeliveryFails"].Visible = (type == ActivityType.Borrows) ? true : false;
            BookGroup1Grid.Splits[0].DisplayColumns["Borrow"].Visible = (type == ActivityType.Borrows) ? true : false;
            BookGroup1Grid.Splits[0].DisplayColumns["BorrowAmount"].Visible = (type == ActivityType.Borrows) ? true : false;
            BookGroup1Grid.Splits[0].DisplayColumns["Available"].Visible = (type == ActivityType.Availability) ? true : false;

            BookGroup2Grid.Splits[0].DisplayColumns["Recall"].Visible = (type == ActivityType.Recalls) ? true : false;
            BookGroup2Grid.Splits[0].DisplayColumns["ReceiveFails"].Visible = (type == ActivityType.Borrows) ? true : false;
            BookGroup2Grid.Splits[0].DisplayColumns["DeliveryFails"].Visible = (type == ActivityType.Borrows) ? true : false;
            BookGroup2Grid.Splits[0].DisplayColumns["Borrow"].Visible = (type == ActivityType.Borrows) ? true : false;
            BookGroup2Grid.Splits[0].DisplayColumns["BorrowAmount"].Visible = (type == ActivityType.Borrows) ? true : false;
            BookGroup2Grid.Splits[0].DisplayColumns["Available"].Visible = (type == ActivityType.Availability) ? true : false;

            CombinedBookGroupGrid.Splits[0].DisplayColumns["Recall"].Visible = (type == ActivityType.Recalls) ? true : false;
            CombinedBookGroupGrid.Splits[0].DisplayColumns["ReceiveFails"].Visible = (type == ActivityType.Borrows) ? true : false;
            CombinedBookGroupGrid.Splits[0].DisplayColumns["DeliveryFails"].Visible = (type == ActivityType.Borrows) ? true : false;
            CombinedBookGroupGrid.Splits[0].DisplayColumns["Borrow"].Visible = (type == ActivityType.Borrows) ? true : false;
            CombinedBookGroupGrid.Splits[0].DisplayColumns["BorrowAmount"].Visible = (type == ActivityType.Borrows) ? true : false;
            CombinedBookGroupGrid.Splits[0].DisplayColumns["Available"].Visible = (type == ActivityType.Availability) ? true : false;
        }

        private void BookGroupGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
        {
            string _secId = "";

            if ((isReady))
            {
                if (CombinedBookGroupGrid.Focused)
                {
                    _secId = CombinedBookGroupGrid.Columns["SecId"].Text;
                }
                else if (BookGroup1Grid.Focused)
                {
                    _secId = BookGroup1Grid.Columns["SecId"].Text;
                }
                else if (BookGroup2Grid.Focused)
                {
                    _secId = BookGroup2Grid.Columns["SecId"].Text;
                }

                if (!secId.Equals(_secId))
                {
                    mainForm.SecId = _secId;
                    secId = _secId;
                }
            }
        }       

        private void Lockup(DataTable dtBoxSummary, bool isRollup)
        {
            if (isRollup)
            {
                foreach (DataRow drRow in dtBoxSummary.Rows)
                {
                    drRow["ExDeficit"] = long.Parse(drRow["ExDeficit"].ToString()) + long.Parse(drRow["LockUp"].ToString());
                    drRow["Available"] = long.Parse(drRow["ExDeficit"].ToString());

                }
                dtBoxSummary.AcceptChanges();
                
                ChildDataConfig();
                CombinedDataConfig();
            }
            else
            {
                ChildDataLoad();
                ChildDataConfig();
                CombinedDataConfig();
            }
        }

        private void availabilityStripMenuItem_Click(object sender, EventArgs e)
        {
            availabilityStripMenuItem.Checked = !availabilityStripMenuItem.Checked;

            if (availabilityStripMenuItem.Checked)
            {
                returnsStripMenuItem.Checked = false;
                cancelStripMenuItem.Checked = false;

                DisplayColumns(ActivityType.Availability);
            }
            else
            {
                DisplayColumns(ActivityType.Default);
            }
        }

        private void returnsStripMenuItem_Click(object sender, EventArgs e)
        {
            returnsStripMenuItem.Checked = !returnsStripMenuItem.Checked;

            if (returnsStripMenuItem.Checked)
            {
                availabilityStripMenuItem.Checked = false;
                cancelStripMenuItem.Checked = false;

                DisplayColumns(ActivityType.Borrows);
            }
            else
            {
                DisplayColumns(ActivityType.Default);
            }
        }

        private void cancelStripMenuItem_Click(object sender, EventArgs e)
        {
            cancelStripMenuItem.Checked = !cancelStripMenuItem.Checked;

            if (cancelStripMenuItem.Checked)
            {
                availabilityStripMenuItem.Checked = false;
                returnsStripMenuItem.Checked = false;

                DisplayColumns(ActivityType.Recalls);
            }
            else
            {
                DisplayColumns(ActivityType.Default);
            }
        }

        private void syncFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            syncFiltersToolStripMenuItem.Checked = !syncFiltersToolStripMenuItem.Checked;

            if (!syncFiltersToolStripMenuItem.Checked)
            {
                dvBookGroup1.RowFilter = dvBookGroup1Filter;
                dvBookGroup2.RowFilter = dvBookGroup2Filter;
                dvBookGroupCombined.RowFilter = dvBookGroupCombinedFilter;

                mainForm.GridFilterClear(ref CombinedBookGroupGrid);
                mainForm.GridFilterClear(ref BookGroup1Grid);
                mainForm.GridFilterClear(ref BookGroup2Grid);
            }
        }

        private void lockupRollupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lockupRollupToolStripMenuItem.Checked = !lockupRollupToolStripMenuItem.Checked;

            Lockup(dsBoxSummary.Tables["BoxSummary"], lockupRollupToolStripMenuItem.Checked);
            ChildDataConfig();

            CombinedBookGroupGrid.Splits[0].DisplayColumns["LockUp"].Visible = !lockupRollupToolStripMenuItem.Checked;
            BookGroup1Grid.Splits[0].DisplayColumns["LockUp"].Visible = !lockupRollupToolStripMenuItem.Checked;
            BookGroup2Grid.Splits[0].DisplayColumns["LockUp"].Visible = !lockupRollupToolStripMenuItem.Checked;
        }    
    }
}
