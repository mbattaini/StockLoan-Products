// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2001-2005  All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
    public partial class PositionOpenContracts_TestForm : System.Windows.Forms.Form
    {
        private const string TEXT = "Position - Open Contracts";
        private const int MINIMUM_SUMMARY_HEIGHT_BUFFER = 180;
        private const decimal E = 0.000000000000000001M;

        private bool isReady = false;
        private string secId = "";
        private string rowIdentity = "";

        private MainForm mainForm;

        private DataSet dataSet;
        private DataView summaryDataView, borrowsDataView, loansDataView;

        /*private C1.Win.C1Input.C1Label BookGroupLabel, BookGroupNameLabel, BizDateLabel;
        private C1.Win.C1List.C1Combo BookGroupCombo, BizDateCombo;

        private System.Windows.Forms.CheckBox SummaryCheckBox, ClassCodeCheckBox, PoolCodeCheckBox;

        private System.Windows.Forms.GroupBox PositionGroupBox;
        private System.Windows.Forms.RadioButton SecurityRadioButton, BookRadioButton, BookParentRadioButton;

        private System.Windows.Forms.Panel ContractsPanel;
        private System.Windows.Forms.Splitter ContractsSplitter, SummarySplitter;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid BorrowsGrid, LoansGrid;

        private C1.Win.C1Input.C1Label FundingRateLabel;
        private C1.Win.C1Input.C1TextBox FundingRateTextBox;
        private System.Windows.Forms.RadioButton FeeRadioButton, RebateRadioButton;

        private System.Windows.Forms.ContextMenu MainContextMenu;

        private System.Windows.Forms.MenuItem RateChangeMenuItem;
        private System.Windows.Forms.MenuItem ReturnMenuItem;
        private System.Windows.Forms.MenuItem RecallMenuItem;
        private System.Windows.Forms.MenuItem PcChangeMenuItem;
        private System.Windows.Forms.MenuItem ShowMenuItem;
        private System.Windows.Forms.MenuItem DockMenuItem;
        private System.Windows.Forms.MenuItem DockTopMenuItem;
        private System.Windows.Forms.MenuItem DockBottomMenuItem;
        private System.Windows.Forms.MenuItem DockNoneMenuItem;*/

        private ContractEventWrapper contractEventWrapper = null;
        private ContractEventHandler contractEventHandler = null;

        private ArrayList contractEventArgsArray, bizDateArray;

        /*private System.Windows.Forms.CheckBox AutoFilterCheckBox;
        private System.Windows.Forms.MenuItem Sep1MenuItem;
        private System.Windows.Forms.MenuItem Sep2MenuItem;
        private System.Windows.Forms.MenuItem ExitMenuItem;
        private System.Windows.Forms.MenuItem ShowIncomeMenuItem;
        private System.Windows.Forms.MenuItem ShowCurrencyMenuItem;
        private System.Windows.Forms.MenuItem ShowTermDateMenuItem;
        private System.Windows.Forms.MenuItem ShowRecallQuantityMenuItem;
        private System.Windows.Forms.MenuItem ShowDividendReclaimMenuItem;
        private System.Windows.Forms.MenuItem ShowSettlementDetailMenuItem;
        private System.Windows.Forms.MenuItem ShowValueRatioMenuItem;
        private System.Windows.Forms.MenuItem DockSep1MenuItem;
        private System.Windows.Forms.MenuItem SendToMenuItem;
        private System.Windows.Forms.MenuItem SendToExcelMenuItem;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid SummaryGrid;*/

        //private System.ComponentModel.Container components = null;

        public PositionOpenContracts_TestForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();

            bizDateArray = new ArrayList();
            contractEventArgsArray = new ArrayList();

            contractEventWrapper = new ContractEventWrapper();
            contractEventWrapper.ContractEvent += new ContractEventHandler(ContractOnEvent);

            contractEventHandler = new ContractEventHandler(ContractDoEvent);

            ContractsPanel.Tag = ContractsPanel.Height;
        }

        public void ClearSelectedRange()
        {
            SummaryGrid.SelectedCols.Clear();
            SummaryGrid.SelectedRows.Clear();

            BorrowsGrid.SelectedCols.Clear();
            BorrowsGrid.SelectedRows.Clear();

            LoansGrid.SelectedCols.Clear();
            LoansGrid.SelectedRows.Clear();
        }

        public void StatusFlagSet(string bookGroup, string contractId, string contractType, string statusFlag)
        {
            foreach (DataRow dataRow in dataSet.Tables["Contracts"].Rows)
            {
                if (dataRow["BookGroup"].ToString().Equals(bookGroup) &&
                  dataRow["ContractId"].ToString().Equals(contractId) &&
                  dataRow["ContractType"].ToString().Equals(contractType))
                {
                    if ((statusFlag.Equals("E") && dataRow["StatusFlag"].ToString().Equals("S")) || !statusFlag.Equals("E"))
                    {
                        dataRow["StatusFlag"] = statusFlag;
                        dataRow.AcceptChanges();
                    }
                    break;
                }
            }
        }

        public string SecId
        {
            set
            {
                if (!secId.Equals(value) && AutoFilterCheckBox.Checked)
                {
                    mainForm.GridFilterClear(ref SummaryGrid);
                    mainForm.GridFilterClear(ref BorrowsGrid);
                    mainForm.GridFilterClear(ref LoansGrid);

                    SummaryGrid.Columns["SecId"].FilterText = value;
                    BorrowsGrid.Columns["SecId"].FilterText = value;
                    LoansGrid.Columns["SecId"].FilterText = value;
                }
            }

            get
            {
                return secId;
            }
        }

        public bool IsReady
        {
            get
            {
                return isReady;
            }

            set
            {
                ContractEventArgs contractEventArgs;

                try
                {
                    lock (this)
                    {
                        if (value && (contractEventArgsArray.Count > 0))
                        {
                            isReady = false;

                            contractEventArgs = (ContractEventArgs)contractEventArgsArray[0];
                            contractEventArgsArray.RemoveAt(0);

                            contractEventHandler.BeginInvoke(contractEventArgs, null, null);
                        }
                        else
                        {
                            isReady = value;
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Write(e.Message + ". [PositionOpenContractsForm.IsReady(set)]", Log.Error, 1);
                }
            }
        }

        private void ContractOnEvent(ContractEventArgs contractEventArgs)
        {
            lock (this)
            {
                contractEventArgsArray.Add(contractEventArgs);

                if (this.IsReady) // Force reset to trigger handling of event.
                {
                    this.IsReady = true;
                }
            }
        }

        private void ContractDoEvent(ContractEventArgs contractEventArgs)
        {
            Log.Write("Setting a contract for " + contractEventArgs.BookGroup + " on " +
              contractEventArgs.ContractId + "-" + contractEventArgs.ContractType + " for " +
              contractEventArgs.Quantity + " of " + contractEventArgs.SecId + ". [PositionOpenContractsForm.ContractDoEvent]", 3);

            try
            {
                lock (this)
                {
                    DataConfig(contractEventArgs);
                    this.IsReady = true;
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [PositionOpenContractsForm.ContractDoEvent]", Log.Error, 1);
            }
        }

        private void DataConfig(ContractEventArgs contractEventArgs)
        {
            decimal rebateYieldBorrowed = 0M;
            decimal feeYieldBorrowed = 0M;

            decimal rebateYieldLent = 0M;
            decimal feeYieldLent = 0M;

            string rowKey = "";
            string poolCode = "";
            string baseType = "";

            DataRow summaryRow = null;

            dataSet.Tables["Contracts"].BeginLoadData();
            dataSet.Tables["Summary"].BeginLoadData();

            try
            {
                dataSet.Tables["Contracts"].LoadDataRow(contractEventArgs.Values, true);

                if (SecurityRadioButton.Checked)
                {
                    DataRow[] contractRows = dataSet.Tables["Contracts"].Select(
                      "BookGroup = '" + BookGroupCombo.Text + "' AND SecId = '" + contractEventArgs.SecId + "'", "SecId, PoolCode, BaseType");

                    foreach (DataRow contractRow in contractRows)
                    {
                        if (rowKey.Equals(contractRow["SecId"].ToString())
                          && (poolCode.Equals(contractRow["PoolCode"].ToString()) || !PoolCodeCheckBox.Checked)
                          && (baseType.Equals(contractRow["BaseType"].ToString()) || !ClassCodeCheckBox.Checked))
                        {
                            CalcsByContract(false, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                        else
                        {
                            if (summaryRow != null)
                            {
                                CalcsBySecurity(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                                dataSet.Tables["Summary"].LoadDataRow(summaryRow.ItemArray, true);
                            }

                            rowKey = contractRow["SecId"].ToString();
                            summaryRow = dataSet.Tables["Summary"].NewRow();

                            summaryRow["BookGroup"] = contractRow["BookGroup"];
                            summaryRow["BookParent"] = "*";
                            summaryRow["Book"] = "*";
                            summaryRow["SecId"] = contractRow["SecId"];
                            summaryRow["Symbol"] = contractRow["Symbol"];
                            summaryRow["BaseType"] = contractRow["BaseType"];
                            summaryRow["ClassGroup"] = contractRow["ClassGroup"];
                            summaryRow["IsEasy"] = contractRow["IsEasy"];
                            summaryRow["IsHard"] = contractRow["IsHard"];
                            summaryRow["IsNoLend"] = contractRow["IsNoLend"];
                            summaryRow["IsThreshold"] = contractRow["IsThreshold"];

                            if (PoolCodeCheckBox.Checked)
                            {
                                poolCode = contractRow["PoolCode"].ToString();
                                summaryRow["PoolCode"] = poolCode;
                            }
                            else
                            {
                                summaryRow["PoolCode"] = "*";
                            }

                            if (ClassCodeCheckBox.Checked)
                            {
                                baseType = contractRow["BaseType"].ToString();
                                summaryRow["BaseType"] = baseType;
                            }
                            else
                            {
                                summaryRow["BaseType"] = "*";
                            }

                            CalcsByContract(true, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                    }

                    if (summaryRow != null)
                    {
                        CalcsBySecurity(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        dataSet.Tables["Summary"].LoadDataRow(summaryRow.ItemArray, true);
                    }
                }

                if (BookRadioButton.Checked)
                {
                    DataRow[] contractRows = dataSet.Tables["Contracts"].Select(
                      "BookGroup = '" + BookGroupCombo.Text + "' AND Book = '" + contractEventArgs.Book + "'", "Book, PoolCode, BaseType");

                    foreach (DataRow contractRow in contractRows)
                    {
                        if (rowKey.Equals(contractRow["Book"].ToString())
                          && (poolCode.Equals(contractRow["PoolCode"].ToString()) || !PoolCodeCheckBox.Checked)
                          && (baseType.Equals(contractRow["BaseType"].ToString()) || !ClassCodeCheckBox.Checked))
                        {
                            CalcsByContract(false, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                        else
                        {
                            if (summaryRow != null)
                            {
                                CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                                dataSet.Tables["Summary"].LoadDataRow(summaryRow.ItemArray, true);
                            }

                            rowKey = contractRow["Book"].ToString();
                            summaryRow = dataSet.Tables["Summary"].NewRow();

                            summaryRow["BookGroup"] = contractRow["BookGroup"];
                            summaryRow["BookParent"] = contractRow["BookParent"];
                            summaryRow["Book"] = contractRow["Book"];
                            summaryRow["SecId"] = "*";

                            if (PoolCodeCheckBox.Checked)
                            {
                                poolCode = contractRow["PoolCode"].ToString();
                                summaryRow["PoolCode"] = poolCode;
                            }
                            else
                            {
                                summaryRow["PoolCode"] = "*";
                            }

                            if (ClassCodeCheckBox.Checked)
                            {
                                baseType = contractRow["BaseType"].ToString();
                                summaryRow["BaseType"] = baseType;
                            }
                            else
                            {
                                summaryRow["BaseType"] = "*";
                            }

                            CalcsByContract(true, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                    }

                    if (summaryRow != null)
                    {
                        CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        dataSet.Tables["Summary"].LoadDataRow(summaryRow.ItemArray, true);
                    }
                }

                if (BookParentRadioButton.Checked)
                {
                    DataRow[] contractRows = dataSet.Tables["Contracts"].Select(
                      "BookGroup = '" + BookGroupCombo.Text + "' AND BookParent = '" + contractEventArgs.BookParent + "'", "BookParent, PoolCode, BaseType");

                    foreach (DataRow contractRow in contractRows)
                    {
                        if (rowKey.Equals(contractRow["BookParent"].ToString())
                          && (poolCode.Equals(contractRow["PoolCode"].ToString()) || !PoolCodeCheckBox.Checked)
                          && (baseType.Equals(contractRow["BaseType"].ToString()) || !ClassCodeCheckBox.Checked))
                        {
                            CalcsByContract(false, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                        else
                        {
                            if (summaryRow != null)
                            {
                                CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                                dataSet.Tables["Summary"].LoadDataRow(summaryRow.ItemArray, true);
                            }

                            rowKey = contractRow["BookParent"].ToString();
                            summaryRow = dataSet.Tables["Summary"].NewRow();

                            summaryRow["BookGroup"] = contractRow["BookGroup"];
                            summaryRow["BookParent"] = contractRow["BookParent"];
                            summaryRow["Book"] = "*";
                            summaryRow["SecId"] = "*";

                            if (PoolCodeCheckBox.Checked)
                            {
                                poolCode = contractRow["PoolCode"].ToString();
                                summaryRow["PoolCode"] = contractRow["PoolCode"].ToString();
                            }
                            else
                            {
                                summaryRow["PoolCode"] = "*";
                            }

                            if (ClassCodeCheckBox.Checked)
                            {
                                baseType = contractRow["BaseType"].ToString();
                                summaryRow["BaseType"] = baseType;
                            }
                            else
                            {
                                summaryRow["BaseType"] = "*";
                            }

                            CalcsByContract(true, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                    }

                    if (summaryRow != null)
                    {
                        CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        dataSet.Tables["Summary"].LoadDataRow(summaryRow.ItemArray, true);
                    }
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + ". [PositionOpenContractsForm.DataConfig]", 1);
                return;
            }

            //dataSet.Tables["Summary"].AcceptChanges();

            if (BookParentRadioButton.Checked)
            {
                CreditUsageSet(contractEventArgs.BookGroup, contractEventArgs.BookParent);
            }

            dataSet.Tables["Contracts"].EndLoadData();
            dataSet.Tables["Summary"].EndLoadData();

            CalcFooterSums();
        }

        private void DataConfig()
        {
            decimal rebateYieldBorrowed = 0M;
            decimal feeYieldBorrowed = 0M;

            decimal rebateYieldLent = 0M;
            decimal feeYieldLent = 0M;

            string rowKey = "";
            string poolCode = "";
            string baseType = "";

            string sortOrder;

            DataRow summaryRow = null;

            dataSet.Tables["Summary"].Rows.Clear();

            ScreenConfig();

            try
            {
                if (SecurityRadioButton.Checked)
                {
                    sortOrder = "SecId" + (PoolCodeCheckBox.Checked ? ", PoolCode" : "") + (ClassCodeCheckBox.Checked ? ", BaseType" : "");
                    DataRow[] contractRows = dataSet.Tables["Contracts"].Select("BookGroup = '" + BookGroupCombo.Text + "'", sortOrder);

                    foreach (DataRow contractRow in contractRows)
                    {
                        if (rowKey.Equals(contractRow["SecId"].ToString())
                          && (poolCode.Equals(contractRow["PoolCode"].ToString()) || !PoolCodeCheckBox.Checked)
                          && (baseType.Equals(contractRow["BaseType"].ToString()) || !ClassCodeCheckBox.Checked))
                        {
                            CalcsByContract(false, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                        else
                        {
                            if (summaryRow != null)
                            {
                                CalcsBySecurity(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                                dataSet.Tables["Summary"].Rows.Add(summaryRow);
                            }

                            rowKey = contractRow["SecId"].ToString();
                            summaryRow = dataSet.Tables["Summary"].NewRow();

                            summaryRow["BookGroup"] = contractRow["BookGroup"];
                            summaryRow["BookParent"] = "*";
                            summaryRow["Book"] = "*";
                            summaryRow["SecId"] = contractRow["SecId"];
                            summaryRow["Symbol"] = contractRow["Symbol"];
                            summaryRow["BaseType"] = contractRow["BaseType"];
                            summaryRow["ClassGroup"] = contractRow["ClassGroup"];
                            summaryRow["IsEasy"] = contractRow["IsEasy"];
                            summaryRow["IsHard"] = contractRow["IsHard"];
                            summaryRow["IsNoLend"] = contractRow["IsNoLend"];
                            summaryRow["IsThreshold"] = contractRow["IsThreshold"];

                            if (PoolCodeCheckBox.Checked)
                            {
                                poolCode = contractRow["PoolCode"].ToString();
                                summaryRow["PoolCode"] = poolCode;
                            }
                            else
                            {
                                summaryRow["PoolCode"] = "*";
                            }

                            if (ClassCodeCheckBox.Checked)
                            {
                                baseType = contractRow["BaseType"].ToString();
                                summaryRow["BaseType"] = baseType;
                            }
                            else
                            {
                                summaryRow["BaseType"] = "*";
                            }

                            CalcsByContract(true, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                    }

                    if (summaryRow != null)
                    {
                        CalcsBySecurity(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        dataSet.Tables["Summary"].Rows.Add(summaryRow);
                    }
                }

                if (BookRadioButton.Checked)
                {
                    sortOrder = "Book" + (PoolCodeCheckBox.Checked ? ", PoolCode" : "") + (ClassCodeCheckBox.Checked ? ", BaseType" : "");
                    DataRow[] contractRows = dataSet.Tables["Contracts"].Select("BookGroup = '" + BookGroupCombo.Text + "'", sortOrder);

                    foreach (DataRow contractRow in contractRows)
                    {
                        if (rowKey.Equals(contractRow["Book"].ToString())
                          && (poolCode.Equals(contractRow["PoolCode"].ToString()) || !PoolCodeCheckBox.Checked)
                          && (baseType.Equals(contractRow["BaseType"].ToString()) || !ClassCodeCheckBox.Checked))
                        {
                            CalcsByContract(false, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                        else
                        {
                            if (summaryRow != null)
                            {
                                CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                                dataSet.Tables["Summary"].Rows.Add(summaryRow);
                            }

                            rowKey = contractRow["Book"].ToString();
                            summaryRow = dataSet.Tables["Summary"].NewRow();

                            summaryRow["BookGroup"] = contractRow["BookGroup"];
                            summaryRow["Book"] = contractRow["Book"];
                            summaryRow["BookParent"] = contractRow["BookParent"];
                            summaryRow["SecId"] = "*";

                            if (PoolCodeCheckBox.Checked)
                            {
                                poolCode = contractRow["PoolCode"].ToString();
                                summaryRow["PoolCode"] = poolCode;
                            }
                            else
                            {
                                summaryRow["PoolCode"] = "*";
                            }

                            if (ClassCodeCheckBox.Checked)
                            {
                                baseType = contractRow["BaseType"].ToString();
                                summaryRow["BaseType"] = baseType;
                            }
                            else
                            {
                                summaryRow["BaseType"] = "*";
                            }

                            CalcsByContract(true, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                    }

                    if (summaryRow != null)
                    {
                        CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        dataSet.Tables["Summary"].Rows.Add(summaryRow);
                    }
                }

                if (BookParentRadioButton.Checked)
                {
                    sortOrder = "BookParent" + (PoolCodeCheckBox.Checked ? ", PoolCode" : "") + (ClassCodeCheckBox.Checked ? ", BaseType" : "");
                    DataRow[] contractRows
                      = dataSet.Tables["Contracts"].Select("BookGroup = '" + BookGroupCombo.Text + "'", sortOrder);

                    foreach (DataRow contractRow in contractRows)
                    {
                        if (rowKey.Equals(contractRow["BookParent"].ToString())
                          && (poolCode.Equals(contractRow["PoolCode"].ToString()) || !PoolCodeCheckBox.Checked)
                          && (baseType.Equals(contractRow["BaseType"].ToString()) || !ClassCodeCheckBox.Checked))
                        {
                            CalcsByContract(false, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                        else
                        {
                            if (summaryRow != null)
                            {
                                CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                                dataSet.Tables["Summary"].Rows.Add(summaryRow);
                            }

                            rowKey = contractRow["BookParent"].ToString();
                            summaryRow = dataSet.Tables["Summary"].NewRow();

                            summaryRow["BookGroup"] = contractRow["BookGroup"];
                            summaryRow["BookParent"] = contractRow["BookParent"];
                            summaryRow["Book"] = "*";
                            summaryRow["SecId"] = "*";

                            if (PoolCodeCheckBox.Checked)
                            {
                                poolCode = contractRow["PoolCode"].ToString();
                                summaryRow["PoolCode"] = poolCode;
                            }
                            else
                            {
                                summaryRow["PoolCode"] = "*";
                            }

                            if (ClassCodeCheckBox.Checked)
                            {
                                baseType = contractRow["BaseType"].ToString();
                                summaryRow["BaseType"] = baseType;
                            }
                            else
                            {
                                summaryRow["BaseType"] = "*";
                            }

                            CalcsByContract(true, contractRow, ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        }
                    }

                    if (summaryRow != null)
                    {
                        CalcsByBook(ref summaryRow, ref rebateYieldBorrowed, ref feeYieldBorrowed, ref rebateYieldLent, ref feeYieldLent);
                        dataSet.Tables["Summary"].Rows.Add(summaryRow);
                    }
                }

                dataSet.Tables["Summary"].AcceptChanges();

                if (BookParentRadioButton.Checked)
                {
                    CreditUsageSet();
                }

                dataSet.Tables["Summary"].EndLoadData();
                this.Refresh();
            }
            catch (Exception e)
            {
                mainForm.Alert(e.Message, PilotState.RunFault);
                Log.Write(e.Message + " [PositionOpenContractsForm.DataConfig]", 1);
            }

            SummaryGrid.RefetchRow();
            CalcFooterSums();
        }

        private void ScreenConfig()
        {
            try
            {
                SummaryGrid.Splits[0, 0].DisplayColumns["BookGroup"].Visible = false;
                SummaryGrid.Splits[0, 0].DisplayColumns["BookParent"].Visible = (BookRadioButton.Checked || BookParentRadioButton.Checked);
                SummaryGrid.Splits[0, 0].DisplayColumns["Book"].Visible = BookRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["SecId"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["Symbol"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["ClassGroup"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["IsEasy"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["IsHard"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["IsNoLend"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["IsThreshold"].Visible = SecurityRadioButton.Checked;

                SummaryGrid.Splits[0, 0].DisplayColumns["PoolCode"].Visible = PoolCodeCheckBox.Checked;
                SummaryGrid.Splits[0, 0].DisplayColumns["BaseType"].Visible = ClassCodeCheckBox.Checked;

                SummaryGrid.Splits[0, 1].DisplayColumns["QuantityBorrowed"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 1].DisplayColumns["ValueBorrowed"].Visible = !SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 1].DisplayColumns["ValueBorrowedRatio"].Visible = !SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 2].DisplayColumns["QuantityLent"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 2].DisplayColumns["ValueLent"].Visible = !SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 2].DisplayColumns["ValueLentRatio"].Visible = !SecurityRadioButton.Checked;

                SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitBorrow"].Visible = BookParentRadioButton.Checked;
                SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitBorrowUsage"].Visible = BookParentRadioButton.Checked;
                SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitLoan"].Visible = BookParentRadioButton.Checked;
                SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitLoanUsage"].Visible = BookParentRadioButton.Checked;
                SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitTotal"].Visible = BookParentRadioButton.Checked;
                SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitTotalUsage"].Visible = BookParentRadioButton.Checked;

                SummaryGrid.Splits[0, 4].DisplayColumns["MatchIncome"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 4].DisplayColumns["CashIncome"].Visible = SecurityRadioButton.Checked;
                SummaryGrid.Splits[0, 4].DisplayColumns["CashFlow"].Visible = SecurityRadioButton.Checked;

                SummaryGrid.Splits[0, 0].SplitSize = 0;
                SummaryGrid.Splits[0, 1].SplitSize = 0;
                SummaryGrid.Splits[0, 2].SplitSize = 0;
                SummaryGrid.Splits[0, 3].SplitSize = 0;

                if (SecurityRadioButton.Checked)
                {
                    SummaryGrid.Splits[0, 0].SplitSize = 9;
                    SummaryGrid.Splits[0, 0].MinWidth = SummaryGrid.Splits[0, 0].DisplayColumns["Security"].Width
                      + SummaryGrid.Splits[0, 0].RecordSelectorWidth;

                    SummaryGrid.Splits[0, 1].SplitSize = 3;
                    SummaryGrid.Splits[0, 1].MinWidth = SummaryGrid.Splits[0, 1].DisplayColumns["QuantityBorrowed"].Width;

                    SummaryGrid.Splits[0, 2].SplitSize = 3;
                    SummaryGrid.Splits[0, 2].MinWidth = SummaryGrid.Splits[0, 2].DisplayColumns["QuantityLent"].Width;

                    SummaryGrid.Splits[0, 3].SplitSizeMode = C1.Win.C1TrueDBGrid.SizeModeEnum.Exact;
                    SummaryGrid.Splits[0, 3].SplitSize = 0;
                    SummaryGrid.Splits[0, 3].MinWidth = 50;
                }

                if (BookRadioButton.Checked)
                {
                    SummaryGrid.Splits[0, 0].SplitSize = 5;
                    SummaryGrid.Splits[0, 0].MinWidth = SummaryGrid.Splits[0, 0].DisplayColumns["Book"].Width
                      + SummaryGrid.Splits[0, 0].RecordSelectorWidth;

                    SummaryGrid.Splits[0, 1].SplitSize = 4;
                    SummaryGrid.Splits[0, 1].MinWidth = SummaryGrid.Splits[0, 1].DisplayColumns["AmountBorrowed"].Width;

                    SummaryGrid.Splits[0, 2].SplitSize = 4;
                    SummaryGrid.Splits[0, 2].MinWidth = SummaryGrid.Splits[0, 2].DisplayColumns["AmountLent"].Width;

                    SummaryGrid.Splits[0, 3].SplitSizeMode = C1.Win.C1TrueDBGrid.SizeModeEnum.Exact;
                    SummaryGrid.Splits[0, 3].SplitSize = 0;
                    SummaryGrid.Splits[0, 3].MinWidth = 50;
                }

                if (BookParentRadioButton.Checked)
                {
                    SummaryGrid.Splits[0, 0].SplitSize = 4;
                    SummaryGrid.Splits[0, 0].MinWidth = SummaryGrid.Splits[0, 0].DisplayColumns["BookParent"].Width
                      + SummaryGrid.Splits[0, 0].RecordSelectorWidth;

                    SummaryGrid.Splits[0, 1].SplitSize = 4;
                    SummaryGrid.Splits[0, 1].MinWidth = SummaryGrid.Splits[0, 1].DisplayColumns["AmountBorrowed"].Width;

                    SummaryGrid.Splits[0, 2].SplitSize = 4;
                    SummaryGrid.Splits[0, 2].MinWidth = SummaryGrid.Splits[0, 2].DisplayColumns["AmountLent"].Width;

                    SummaryGrid.Splits[0, 3].SplitSizeMode = C1.Win.C1TrueDBGrid.SizeModeEnum.NumberOfColumns;
                    SummaryGrid.Splits[0, 3].SplitSize = 6;
                    SummaryGrid.Splits[0, 3].MinWidth = SummaryGrid.Splits[0, 3].DisplayColumns["AmountLimitBorrow"].Width;
                }

                BorrowsGrid.Splits[0, 0].DisplayColumns["BookGroup"].Visible = false;
                BorrowsGrid.Splits[0, 0].DisplayColumns["BookParent"].Visible = (SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["Book"].Visible = (SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["SecId"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["Symbol"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["ClassGroup"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["IsEasy"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["IsHard"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["IsNoLend"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["IsThreshold"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 1].DisplayColumns["RebateRate"].Visible = RebateRadioButton.Checked;
                BorrowsGrid.Splits[0, 1].DisplayColumns["FeeRate"].Visible = FeeRadioButton.Checked;

                BorrowsGrid.Splits[0, 0].DisplayColumns["PoolCode"].Visible = (!PoolCodeCheckBox.Checked || !SummaryCheckBox.Checked);
                BorrowsGrid.Splits[0, 0].DisplayColumns["BaseType"].Visible = (!ClassCodeCheckBox.Checked || !SummaryCheckBox.Checked);

                LoansGrid.Splits[0, 0].DisplayColumns["BookGroup"].Visible = false;
                LoansGrid.Splits[0, 0].DisplayColumns["BookParent"].Visible = (SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["Book"].Visible = (SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["SecId"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["Symbol"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["ClassGroup"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["IsEasy"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["IsHard"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["IsNoLend"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["IsThreshold"].Visible = (!SecurityRadioButton.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 1].DisplayColumns["FeeRate"].Visible = FeeRadioButton.Checked;
                LoansGrid.Splits[0, 1].DisplayColumns["RebateRate"].Visible = RebateRadioButton.Checked;

                LoansGrid.Splits[0, 0].DisplayColumns["PoolCode"].Visible = (!PoolCodeCheckBox.Checked || !SummaryCheckBox.Checked);
                LoansGrid.Splits[0, 0].DisplayColumns["BaseType"].Visible = (!ClassCodeCheckBox.Checked || !SummaryCheckBox.Checked);

                BorrowsGrid.Splits[0, 0].SplitSize = 0;
                LoansGrid.Splits[0, 0].SplitSize = 0;

                if (SummaryCheckBox.Checked)
                {
                    if (SecurityRadioButton.Checked)
                    {
                        BorrowsGrid.Splits[0, 0].SplitSize = 4;
                        LoansGrid.Splits[0, 0].SplitSize = 4;

                        BorrowsGrid.Splits[0, 0].MinWidth = BorrowsGrid.Splits[0, 0].DisplayColumns["Parent"].Width
                          + BorrowsGrid.Splits[0, 0].RecordSelectorWidth;
                        LoansGrid.Splits[0, 0].MinWidth = LoansGrid.Splits[0, 0].DisplayColumns["Parent"].Width
                          + BorrowsGrid.Splits[0, 0].RecordSelectorWidth;
                    }
                    else
                    {
                        BorrowsGrid.Splits[0, 0].SplitSize = 9;
                        LoansGrid.Splits[0, 0].SplitSize = 9;

                        BorrowsGrid.Splits[0, 0].MinWidth = BorrowsGrid.Splits[0, 0].DisplayColumns["Security"].Width
                          + BorrowsGrid.Splits[0, 0].RecordSelectorWidth;
                        LoansGrid.Splits[0, 0].MinWidth = LoansGrid.Splits[0, 0].DisplayColumns["Security"].Width
                          + BorrowsGrid.Splits[0, 0].RecordSelectorWidth;
                    }
                }
                else
                {
                    BorrowsGrid.Splits[0, 0].SplitSize = 11;
                    LoansGrid.Splits[0, 0].SplitSize = 11;

                    BorrowsGrid.Splits[0, 0].MinWidth = BorrowsGrid.Splits[0, 0].DisplayColumns["Parent"].Width
                      + BorrowsGrid.Splits[0, 0].RecordSelectorWidth;
                    LoansGrid.Splits[0, 0].MinWidth = LoansGrid.Splits[0, 0].DisplayColumns["Parent"].Width
                      + BorrowsGrid.Splits[0, 0].RecordSelectorWidth;
                }
            }
            catch (Exception e)
            {
                mainForm.Alert(e.Message, PilotState.RunFault);
                Log.Write(e.Message + ". [PositionOpenContractsForm.ScreenConfig]", 1);
            }
        }

        private void CalcsByContract(bool isNewSummaryRow, DataRow contractRow, ref DataRow summaryRow,
          ref decimal rebateYieldBorrowed, ref decimal feeYieldBorrowed, ref decimal rebateYieldLent, ref decimal feeYieldLent)
        {
            if (isNewSummaryRow)
            {
                switch (contractRow["ContractType"].ToString())
                {
                    case "B":
                        summaryRow["QuantityBorrowed"] = contractRow["Quantity"];
                        summaryRow["AmountBorrowed"] = contractRow["Amount"];
                        summaryRow["ValueBorrowed"] = contractRow["Value"];

                        rebateYieldBorrowed = rebateYieldBorrowed + ((decimal)contractRow["Amount"] * (decimal)contractRow["RebateRate"]);
                        feeYieldBorrowed = feeYieldBorrowed + ((decimal)contractRow["Amount"] * (decimal)contractRow["FeeRate"]);

                        summaryRow["QuantityLent"] = 0L;
                        summaryRow["AmountLent"] = 0M;
                        summaryRow["ValueLent"] = 0M;

                        break;
                    case "L":
                        summaryRow["QuantityBorrowed"] = 0L;
                        summaryRow["AmountBorrowed"] = 0M;
                        summaryRow["ValueBorrowed"] = 0M;

                        rebateYieldLent = rebateYieldLent + ((decimal)contractRow["Amount"] * (decimal)contractRow["RebateRate"]);
                        feeYieldLent = feeYieldLent + ((decimal)contractRow["Amount"] * (decimal)contractRow["FeeRate"]);

                        summaryRow["QuantityLent"] = contractRow["Quantity"];
                        summaryRow["AmountLent"] = contractRow["Amount"];
                        summaryRow["ValueLent"] = contractRow["Value"];

                        break;
                }
            }
            else
            {
                switch (contractRow["ContractType"].ToString())
                {
                    case "B":
                        summaryRow["QuantityBorrowed"] = (long)summaryRow["QuantityBorrowed"] + (long)contractRow["Quantity"];
                        summaryRow["AmountBorrowed"] = (decimal)summaryRow["AmountBorrowed"] + (decimal)contractRow["Amount"];
                        summaryRow["ValueBorrowed"] = (decimal)summaryRow["ValueBorrowed"] + (decimal)contractRow["Value"];

                        rebateYieldBorrowed = rebateYieldBorrowed + ((decimal)contractRow["Amount"] * (decimal)contractRow["RebateRate"]);
                        feeYieldBorrowed = feeYieldBorrowed + ((decimal)contractRow["Amount"] * (decimal)contractRow["FeeRate"]);

                        break;
                    case "L":
                        summaryRow["QuantityLent"] = (long)summaryRow["QuantityLent"] + (long)contractRow["Quantity"];
                        summaryRow["AmountLent"] = (decimal)summaryRow["AmountLent"] + (decimal)contractRow["Amount"];
                        summaryRow["ValueLent"] = (decimal)summaryRow["ValueLent"] + (decimal)contractRow["Value"];

                        rebateYieldLent = rebateYieldLent + ((decimal)contractRow["Amount"] * (decimal)contractRow["RebateRate"]);
                        feeYieldLent = feeYieldLent + ((decimal)contractRow["Amount"] * (decimal)contractRow["FeeRate"]);

                        break;
                }
            }
        }

        private void CalcsBySecurity(ref DataRow summaryRow,
          ref decimal rebateYieldBorrowed, ref decimal feeYieldBorrowed, ref decimal rebateYieldLent, ref decimal feeYieldLent)
        {
            decimal quantityBorrowed = (decimal)(long)summaryRow["QuantityBorrowed"];
            decimal amountBorrowed = (decimal)summaryRow["AmountBorrowed"];

            decimal quantityLent = (decimal)(long)summaryRow["QuantityLent"];
            decimal amountLent = (decimal)summaryRow["AmountLent"];

            // Borrow rate.
            if (amountBorrowed == 0M)
            {
                summaryRow["RateBorrowed"] = 0D;
            }
            else
            {
                if (RebateRadioButton.Checked)
                {
                    summaryRow["RateBorrowed"] = rebateYieldBorrowed / amountBorrowed;
                }

                if (FeeRadioButton.Checked)
                {
                    summaryRow["RateBorrowed"] = feeYieldBorrowed / amountBorrowed;
                }
            }

            // Loan rate.
            if (amountLent == 0M)
            {
                summaryRow["RateLent"] = 0D;
            }
            else
            {
                if (RebateRadioButton.Checked)
                {
                    summaryRow["RateLent"] = rebateYieldLent / amountLent;
                }

                if (FeeRadioButton.Checked)
                {
                    summaryRow["RateLent"] = feeYieldLent / amountLent;
                }
            }

            // Income.
            if (quantityBorrowed > quantityLent)
            {
                summaryRow["Income"] = -((quantityBorrowed - quantityLent) / (quantityBorrowed + E)) * amountBorrowed * (feeYieldBorrowed / (amountBorrowed + E)) / 36000M;
            }
            else if (quantityBorrowed < quantityLent)
            {
                summaryRow["Income"] = ((quantityLent - quantityBorrowed) / (quantityLent + E)) * amountLent * (feeYieldLent / (amountLent + E)) / 36000M;
            }
            else if (quantityBorrowed.Equals(0) && quantityLent.Equals(0))
            {
                summaryRow["Income"] = -(amountBorrowed * (feeYieldBorrowed / (amountBorrowed + E)) / 36000M) + (amountLent * (feeYieldLent / (amountLent + E)) / 36000M);
            }
            else
            {
                summaryRow["Income"] = 0D;
            }

            // Match P/L.
            decimal d1 = 0M;
            decimal d2 = 0M;
            decimal d3 = 0M;

            if ((amountLent / (quantityLent + E)) < (amountBorrowed / (quantityBorrowed + E)))
            {
                d1 = amountLent / (quantityLent + E);
            }
            else
            {
                d1 = amountBorrowed / (quantityBorrowed + E);
            }

            if ((quantityBorrowed > 0) && (quantityLent > 0))
            {
                if (quantityBorrowed < quantityLent)
                {
                    d2 = quantityBorrowed;
                }
                else
                {
                    d2 = quantityLent;
                }

                d2 = d2 * (feeYieldLent / (amountLent + E) - feeYieldBorrowed / (amountBorrowed + E)) / 36000M;

                d3 = (feeYieldLent / (amountLent + E) - feeYieldBorrowed / (amountBorrowed + E));
            }
            else
            {
                d2 = 0M;
            }

            summaryRow["MatchIncome"] = d1 * d2;
            //summaryRow["Spread"] = d3 * 100;

            // Cash P/L.
            d1 = 0M;
            d2 = 0M;
            d3 = 0M;

            d1 = ((amountLent / (quantityLent + E)) - (amountBorrowed / (quantityBorrowed + E)));

            if ((quantityBorrowed > 0) && (quantityLent > 0))
            {
                if (quantityBorrowed < quantityLent)
                {
                    d2 = quantityBorrowed;
                }
                else
                {
                    d2 = quantityLent;
                }

                if ((amountLent / (quantityLent + E)) - (amountBorrowed / (quantityBorrowed + E)) < 0)
                {
                    d3 = feeYieldBorrowed / (amountBorrowed + E) / 36000M;
                }
                else
                {
                    d3 = feeYieldLent / (amountLent + E) / 36000M;
                }
            }
            else
            {
                d2 = 0M;
                d3 = 0M;
            }

            summaryRow["CashIncome"] = d1 * d2 * d3;

            // Cash flow.
            d1 = 0M;
            d2 = 0M;
            d3 = 0M;

            d1 = ((amountLent / (quantityLent + E)) - (amountBorrowed / (quantityBorrowed + E)));

            if ((quantityBorrowed > 0) && (quantityLent > 0))
            {
                if (quantityBorrowed < quantityLent)
                {
                    d2 = quantityBorrowed;
                }
                else
                {
                    d2 = quantityLent;
                }
            }
            else
            {
                d2 = 0M;
            }

            summaryRow["CashFlow"] = d1 * d2;

            rebateYieldBorrowed = 0M;
            feeYieldBorrowed = 0M;
            rebateYieldLent = 0M;
            feeYieldLent = 0M;
        }

        private void CalcsByBook(ref DataRow summaryRow,
          ref decimal rebateYieldBorrowed, ref decimal feeYieldBorrowed, ref decimal rebateYieldLent, ref decimal feeYieldLent)
        {
            decimal quantityBorrowed = (decimal)(long)summaryRow["QuantityBorrowed"];
            decimal amountBorrowed = (decimal)summaryRow["AmountBorrowed"];

            decimal quantityLent = (decimal)(long)summaryRow["QuantityLent"];
            decimal amountLent = (decimal)summaryRow["AmountLent"];

            // Borrow rate.
            if (amountBorrowed == 0M)
            {
                summaryRow["RateBorrowed"] = 0D;
            }
            else
            {
                if (RebateRadioButton.Checked)
                {
                    summaryRow["RateBorrowed"] = rebateYieldBorrowed / amountBorrowed;
                }

                if (FeeRadioButton.Checked)
                {
                    summaryRow["RateBorrowed"] = feeYieldBorrowed / amountBorrowed;
                }
            }

            // Borrow value ratio.
            if ((decimal)summaryRow["ValueBorrowed"] > 0M)
            {
                summaryRow["ValueBorrowedRatio"] = (decimal)summaryRow["AmountBorrowed"] / (decimal)summaryRow["ValueBorrowed"];
            }

            // Loan rate.
            if (amountLent == 0M)
            {
                summaryRow["RateLent"] = 0D;
            }
            else
            {
                if (RebateRadioButton.Checked)
                {
                    summaryRow["RateLent"] = rebateYieldLent / amountLent;
                }

                if (FeeRadioButton.Checked)
                {
                    summaryRow["RateLent"] = feeYieldLent / amountLent;
                }
            }

            // Loan value ratio.
            if ((decimal)summaryRow["ValueLent"] > 0M)
            {
                summaryRow["ValueLentRatio"] = (decimal)summaryRow["AmountLent"] / (decimal)summaryRow["ValueLent"];
            }

            // Income.
            summaryRow["Income"] = (amountLent * (feeYieldLent / (amountLent + E)) / 36000M) - (amountBorrowed * (feeYieldBorrowed / (amountBorrowed + E)) / 36000M);

            // Match P/L.
            summaryRow["MatchIncome"] = 0M;

            //summaryRow["Spread"] = d3 * 100;

            // Cash P/L.
            summaryRow["CashIncome"] = 0M;

            // Cash flow.
            summaryRow["CashFlow"] = 0M;

            rebateYieldBorrowed = 0M;
            feeYieldBorrowed = 0M;
            rebateYieldLent = 0M;
            feeYieldLent = 0M;
        }

        private void CreditUsageSet()
        {
            decimal amountBorrowedTotal = 0M;
            decimal amountLentTotal = 0M;

            foreach (DataRow summaryRow in dataSet.Tables["Summary"].Rows)
            {
                DataRow[] bookRow = dataSet.Tables["Books"].Select("BookGroup = '" + summaryRow["BookGroup"] + "' AND Book = '" + summaryRow["BookParent"] + "'");

                AmountTotalGet(summaryRow["BookGroup"].ToString(), summaryRow["BookParent"].ToString(), ref amountBorrowedTotal, ref amountLentTotal);

                try
                {
                    summaryRow["AmountLimitBorrow"] = (long)bookRow[0]["AmountLimitBorrow"] * ((decimal)summaryRow["AmountBorrowed"] / amountBorrowedTotal);
                    summaryRow["AmountLimitLoan"] = (long)bookRow[0]["AmountLimitLoan"] * ((decimal)summaryRow["AmountLent"] / amountLentTotal);
                    summaryRow["AmountLimitTotal"] = ((long)bookRow[0]["AmountLimitBorrow"] + (long)bookRow[0]["AmountLimitLoan"])
                      * (((decimal)summaryRow["AmountBorrowed"] + (decimal)summaryRow["AmountLent"]) / (amountBorrowedTotal + amountLentTotal));

                    if ((decimal)summaryRow["AmountLimitBorrow"] > 0M)
                    {
                        summaryRow["AmountLimitBorrowUsage"] = 100M * (decimal)summaryRow["AmountBorrowed"] / (decimal)summaryRow["AmountLimitBorrow"];
                    }

                    if ((decimal)summaryRow["AmountLimitLoan"] > 0M)
                    {
                        summaryRow["AmountLimitLoanUsage"] = 100M * (decimal)summaryRow["AmountLent"] / (decimal)summaryRow["AmountLimitLoan"];
                    }

                    if ((decimal)summaryRow["AmountLimitTotal"] > 0M)
                    {
                        summaryRow["AmountLimitTotalUsage"] = 100M * ((decimal)summaryRow["AmountBorrowed"] + (decimal)summaryRow["AmountLent"]) / (decimal)summaryRow["AmountLimitTotal"];
                    }

                    summaryRow.AcceptChanges();
                }
                catch { }
            }

            dataSet.Tables["Summary"].AcceptChanges();
            SummaryGrid.Rebind(true);
        }

        private void CreditUsageSet(string bookGroup, string bookParent)
        {
            decimal amountBorrowedTotal = 0M;
            decimal amountLentTotal = 0M;

            DataRow[] bookRow = dataSet.Tables["Books"].Select("BookGroup = '" + bookGroup + "' AND Book = '" + bookParent + "'");

            AmountTotalGet(bookGroup, bookParent, ref amountBorrowedTotal, ref amountLentTotal);

            foreach (DataRow summaryRow in dataSet.Tables["Summary"].Rows)
            {
                try
                {
                    summaryRow["AmountLimitBorrow"] = (long)bookRow[0]["AmountLimitBorrow"] * ((decimal)summaryRow["AmountBorrowed"] / amountBorrowedTotal);
                    summaryRow["AmountLimitLoan"] = (long)bookRow[0]["AmountLimitLoan"] * ((decimal)summaryRow["AmountLent"] / amountLentTotal);
                    summaryRow["AmountLimitTotal"] = ((long)bookRow[0]["AmountLimitBorrow"] + (long)bookRow[0]["AmountLimitLoan"])
                      * (((decimal)summaryRow["AmountBorrowed"] + (decimal)summaryRow["AmountLent"]) / (amountBorrowedTotal + amountLentTotal));

                    if ((decimal)summaryRow["AmountLimitBorrow"] > 0M)
                    {
                        summaryRow["AmountLimitBorrowUsage"] = 100M * (decimal)summaryRow["AmountBorrowed"] / (decimal)summaryRow["AmountLimitBorrow"];
                    }

                    if ((decimal)summaryRow["AmountLimitLoan"] > 0M)
                    {
                        summaryRow["AmountLimitLoanUsage"] = 100M * (decimal)summaryRow["AmountLent"] / (decimal)summaryRow["AmountLimitLoan"];
                    }

                    if ((decimal)summaryRow["AmountLimitTotal"] > 0M)
                    {
                        summaryRow["AmountLimitTotalUsage"] = 100M * ((decimal)summaryRow["AmountBorrowed"] + (decimal)summaryRow["AmountLent"]) / (decimal)summaryRow["AmountLimitTotal"];
                    }

                    summaryRow.AcceptChanges();
                }
                catch { }
            }

            dataSet.Tables["Summary"].AcceptChanges();

            SummaryGrid.Rebind(true);
        }

        private void AmountTotalGet(string bookParent, ref decimal amountBorrowedTotal, ref decimal amountLentTotal)
        {
            amountBorrowedTotal = 0M;
            amountLentTotal = 0M;

            foreach (DataRow summaryRow in dataSet.Tables["Summary"].Select("BookParent = '" + bookParent + "'"))
            {
                amountBorrowedTotal += (decimal)summaryRow["AmountBorrowed"];
                amountLentTotal += (decimal)summaryRow["AmountLent"];
            }
        }

        private void AmountTotalGet(string bookGroup, string bookParent, ref decimal amountBorrowedTotal, ref decimal amountLentTotal)
        {
            amountBorrowedTotal = 0M;
            amountLentTotal = 0M;

            foreach (DataRow summaryRow in dataSet.Tables["Summary"].Select("BookGroup = '" + bookGroup + "' AND BookParent = '" + bookParent + "'"))
            {
                amountBorrowedTotal += (decimal)summaryRow["AmountBorrowed"];
                amountLentTotal += (decimal)summaryRow["AmountLent"];
            }
        }

        private void CalcFooterSums()
        {
            decimal rateYieldBorrowed = 0M;
            decimal rateYieldLent = 0M;

            decimal amountBorrowed = 0M;
            decimal amountLent = 0M;

            decimal income = 0M;
            decimal matchIncome = 0M;
            decimal cashIncome = 0M;
            decimal cashFlow = 0M;

            decimal rate;

            foreach (DataRowView summaryRow in summaryDataView)
            {
                rateYieldBorrowed += (decimal)summaryRow["RateBorrowed"] * (decimal)summaryRow["AmountBorrowed"];
                amountBorrowed += (decimal)summaryRow["AmountBorrowed"];

                rateYieldLent += (decimal)summaryRow["RateLent"] * (decimal)summaryRow["AmountLent"];
                amountLent += (decimal)summaryRow["AmountLent"];

                income += (decimal)summaryRow["Income"];
                matchIncome += (decimal)summaryRow["MatchIncome"];
                cashIncome += (decimal)summaryRow["CashIncome"];
                cashFlow += (decimal)summaryRow["CashFlow"];
            }

            rate = rateYieldBorrowed / (amountBorrowed + E);
            SummaryGrid.Columns["RateBorrowed"].FooterText = rate.ToString("0.000");

            rate = rateYieldLent / (amountLent + E);
            SummaryGrid.Columns["RateLent"].FooterText = rate.ToString("0.000");

            SummaryGrid.Columns["AmountBorrowed"].FooterText = amountBorrowed.ToString("#,##0");
            SummaryGrid.Columns["AmountLent"].FooterText = amountLent.ToString("#,##0");
            SummaryGrid.Columns["Income"].FooterText = income.ToString("#,##0.00");
            SummaryGrid.Columns["MatchIncome"].FooterText = matchIncome.ToString("#,##0.00");
            SummaryGrid.Columns["CashIncome"].FooterText = cashIncome.ToString("#,##0.00");
            SummaryGrid.Columns["CashFlow"].FooterText = cashFlow.ToString("#,##0");
        }

        private void FilterConfig()
        {
            string codeFilter = "";

            if (PoolCodeCheckBox.Checked)
            {
                codeFilter = " AND PoolCode = '" + SummaryGrid.Columns["PoolCode"].Text + "'";
            }

            if (ClassCodeCheckBox.Checked)
            {
                codeFilter += " AND BaseType = '" + SummaryGrid.Columns["BaseType"].Text + "'";
            }

            try
            {
                mainForm.GridFilterClear(ref BorrowsGrid);
                mainForm.GridFilterClear(ref LoansGrid);
            }
            catch { }

            if (!SummaryCheckBox.Checked)
            {
                borrowsDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B'";
                borrowsDataView.Sort = "SecId";

                loansDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L'";
                loansDataView.Sort = "SecId";
            }
            else if (SecurityRadioButton.Checked)
            {
                borrowsDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B' AND SecId = '"
                  + SummaryGrid.Columns["SecId"].Text + "'" + codeFilter;
                borrowsDataView.Sort = "BookParent, Book";

                loansDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L' AND SecId = '"
                  + SummaryGrid.Columns["SecId"].Text + "'" + codeFilter;
                loansDataView.Sort = "BookParent, Book";

                mainForm.SecId = SummaryGrid.Columns["SecId"].Text;
            }
            else if (BookRadioButton.Checked)
            {
                borrowsDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B' AND Book = '"
                  + SummaryGrid.Columns["Book"].Text + "'" + codeFilter;
                borrowsDataView.Sort = "SecId";

                loansDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L' AND Book = '"
                  + SummaryGrid.Columns["Book"].Text + "'" + codeFilter;
                loansDataView.Sort = "SecId";
            }
            else if (BookParentRadioButton.Checked)
            {
                borrowsDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'B' AND BookParent = '"
                  + SummaryGrid.Columns["BookParent"].Text + "'" + codeFilter;
                borrowsDataView.Sort = "SecId, Book";

                loansDataView.RowFilter
                  = "BookGroup = '" + BookGroupCombo.Text + "' AND ContractType = 'L' AND BookParent = '"
                  + SummaryGrid.Columns["BookParent"].Text + "'" + codeFilter;
                loansDataView.Sort = "SecId, Book";
            }
        }

        private void PositionOpenContractsForm_Load(object sender, System.EventArgs e)
        {
            int height = this.MinimumSize.Height;
            int width = mainForm.Width - 75;

            this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
            this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
            this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
            this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

            BorrowsGrid.Splits[0, 1].DisplayColumns["QuantitySettled"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["SecurityDepot"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["IsSettledQuantity"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["AmountSettled"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["CashDepot"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["IsSettledAmount"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["ValueDate"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["QuantityRecalled"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["CurrencyIso"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["Income"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["IncomeTracked"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["DivRate"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["DivCallable"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["TermDate"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["Value"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["ValueRatio"].Visible = false;
            BorrowsGrid.Splits[0, 1].DisplayColumns["ValueIsEstimate"].Visible = false;

            LoansGrid.Splits[0, 1].DisplayColumns["QuantitySettled"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["SecurityDepot"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["IsSettledQuantity"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["AmountSettled"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["CashDepot"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["IsSettledAmount"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["ValueDate"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["QuantityRecalled"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["CurrencyIso"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["Income"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["IncomeTracked"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["DivRate"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["DivCallable"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["TermDate"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["Value"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["ValueRatio"].Visible = false;
            LoansGrid.Splits[0, 1].DisplayColumns["ValueIsEstimate"].Visible = false;

            SummaryGrid.Splits[0, 4].MinWidth = 100;
            BorrowsGrid.Splits[0, 1].MinWidth = 150;
            LoansGrid.Splits[0, 1].MinWidth = 150;

            this.Show();
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            mainForm.Alert("Please wait... loading current contract data...", PilotState.Unknown);

            try
            {
                mainForm.PositionAgent.ContractEvent += new ContractEventHandler(contractEventWrapper.DoEvent);

                dataSet = mainForm.PositionAgent.ContractDataGet(mainForm.UtcOffset, null, mainForm.UserId, "PositionOpenContracts");

                dataSet.Tables.Add("Summary");
                dataSet.Tables["Summary"].Columns.Add("BookGroup", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("BookParent", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("Book", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("PoolCode", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("SecId", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("Symbol", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("BaseType", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("ClassGroup", typeof(string));
                dataSet.Tables["Summary"].Columns.Add("IsEasy", typeof(bool));
                dataSet.Tables["Summary"].Columns.Add("IsHard", typeof(bool));
                dataSet.Tables["Summary"].Columns.Add("IsNoLend", typeof(bool));
                dataSet.Tables["Summary"].Columns.Add("IsThreshold", typeof(bool));
                dataSet.Tables["Summary"].Columns.Add("QuantityBorrowed", typeof(long));
                dataSet.Tables["Summary"].Columns.Add("AmountBorrowed", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("RateBorrowed", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("QuantityLent", typeof(long));
                dataSet.Tables["Summary"].Columns.Add("AmountLent", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("RateLent", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("AmountLimitBorrow", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("AmountLimitBorrowUsage", typeof(float));
                dataSet.Tables["Summary"].Columns.Add("AmountLimitLoan", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("AmountLimitLoanUsage", typeof(float));
                dataSet.Tables["Summary"].Columns.Add("AmountLimitTotal", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("AmountLimitTotalUsage", typeof(float));
                dataSet.Tables["Summary"].Columns.Add("Income", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("MatchIncome", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("CashIncome", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("CashFlow", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("ValueBorrowed", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("ValueBorrowedRatio", typeof(float));
                dataSet.Tables["Summary"].Columns.Add("ValueLent", typeof(decimal));
                dataSet.Tables["Summary"].Columns.Add("ValueLentRatio", typeof(float));

                dataSet.Tables["Summary"].PrimaryKey = new DataColumn[6]
          {
            dataSet.Tables["Summary"].Columns["BookGroup"],
            dataSet.Tables["Summary"].Columns["BookParent"],
            dataSet.Tables["Summary"].Columns["Book"],
            dataSet.Tables["Summary"].Columns["PoolCode"],
            dataSet.Tables["Summary"].Columns["SecId"],
            dataSet.Tables["Summary"].Columns["BaseType"] };

                summaryDataView = new DataView(dataSet.Tables["Summary"]);
                SummaryGrid.SetDataBinding(dataSet.Tables["Summary"], null, true);

                borrowsDataView = new DataView(dataSet.Tables["Contracts"]);
                BorrowsGrid.SetDataBinding(borrowsDataView, null, true);

                loansDataView = new DataView(dataSet.Tables["Contracts"]);
                LoansGrid.SetDataBinding(loansDataView, null, true);

                BookGroupCombo.HoldFields();
                BookGroupCombo.ValueMember = "BookGroup";
                BookGroupCombo.DataMember = "BookGroups";
                BookGroupCombo.DataSource = dataSet;

                BookGroupCombo.Text = dataSet.Tables["BookGroups"].Rows[0]["BookGroup"].ToString();

                DataConfig();
                FilterConfig();

                BookGroupNameLabel.DataSource = dataSet.Tables["BookGroup"];
                BookGroupNameLabel.DataField = "BookName";

                BizDateCombo.HoldFields();
                BizDateCombo.ValueMember = "BizDate";
                BizDateCombo.DataMember = "BizDates";
                BizDateCombo.DataSource = dataSet;

                BizDateCombo.Text = dataSet.Tables["BizDates"].Rows[0]["BizDate"].ToString();

                foreach (DataRow dataRow in dataSet.Tables["BizDates"].Rows)
                {
                    bizDateArray.Add(dataRow["BizDate"].ToString());
                }

                SummaryGrid.Visible = true;
                BorrowsGrid.Visible = true;
                LoansGrid.Visible = true;

                Enabled = true;
                this.IsReady = true;

                BookGroupCombo.Text = RegistryValue.Read(this.Name, "BookGroup", "");
                mainForm.Alert("Loading current contract data... Done!", PilotState.Normal);
                this.Cursor = Cursors.Default;
            }
            catch (Exception error)
            {
                Enabled = true;
                mainForm.Alert(error.Message, PilotState.RunFault);
                Log.Write(error.Message + " [PositionOpenContractsForm.PositionOpenContractsForm_Load]", Log.Error, 1);
            }
        }

        void SummaryGrid_MouseWheel(object sender, MouseEventArgs e)
        {
        }

        private void PositionOpenContractsForm_Closed(object sender, System.EventArgs e)
        {
            RegistryValue.Write(this.Name, "BookGroup", BookGroupCombo.Text);

            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

            try
            {
                if (contractEventWrapper != null)
                {
                    mainForm.PositionAgent.ContractEvent -= new ContractEventHandler(contractEventWrapper.DoEvent);
                    contractEventWrapper.ContractEvent -= new ContractEventHandler(ContractOnEvent);
                    contractEventWrapper = null;
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [PositionOpenContractsForm.PositionDealBlotterForm_Closed]", Log.Error, 1);
            }

            mainForm.positionOpenContractsForm = null;
        }

        private void PositionOpenContractsForm_Resize(object sender, System.EventArgs e)
        {
            if ((this.Height - ContractsPanel.Height) < MINIMUM_SUMMARY_HEIGHT_BUFFER)
            {
                ContractsPanel.Height = this.Height - MINIMUM_SUMMARY_HEIGHT_BUFFER;
            }
        }

        private void BookGroupCombo_RowChange(object sender, System.EventArgs e)
        {
            if (!BookGroupCombo.Text.Equals(""))
            {
                DataConfig();
                FilterConfig();
            }
        }

        private void SummaryCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            SummaryGrid.Visible = SummaryCheckBox.Checked;
            SummarySplitter.Visible = SummaryCheckBox.Checked;

            PositionGroupBox.Enabled = SummaryCheckBox.Checked;
            PoolCodeCheckBox.Enabled = SummaryCheckBox.Checked;
            ClassCodeCheckBox.Enabled = SummaryCheckBox.Checked;

            BorrowsGrid.FilterBar = !SummaryCheckBox.Checked;
            LoansGrid.FilterBar = !SummaryCheckBox.Checked;

            if (SummaryCheckBox.Checked)
            {
                ContractsPanel.Dock = DockStyle.Bottom;
            }
            else
            {
                ContractsPanel.Dock = DockStyle.Fill;
            }

            DataConfig();
            FilterConfig();

            this.Cursor = Cursors.Default;
        }

        private void ContractsPanel_Resize(object sender, System.EventArgs e)
        {
            ContractsSplitter.SplitPosition = ContractsSplitter.SplitPosition + ((ContractsPanel.Height - (int)ContractsPanel.Tag) / 2);
            ContractsPanel.Tag = ContractsPanel.Height;
        }

        private void BorrowsGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            if (borrowsDataView.RowFilter.Equals(""))
            {
                borrowsDataView.RowFilter = "ContractType = 'B' AND BookGroup = '" + BookGroupCombo.Text + "'";
            }
            else
            {
                borrowsDataView.RowFilter = "ContractType = 'B' AND BookGroup = '" + BookGroupCombo.Text + "' AND " + borrowsDataView.RowFilter;
            }
        }

        private void BorrowsGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (BorrowsGrid.Focused)
            {
                try
                {
                    string s = BorrowsGrid.Columns["SecId"].Text;

                    if (!s.Equals(secId))
                    {
                        secId = s;
                        mainForm.SecId = s;
                    }
                }
                catch
                {
                    secId = "";
                }
            }
        }

        private void BorrowsGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            switch (BorrowsGrid.Columns["StatusFlag"].CellText(e.Row))
            {
                case "S":
                    e.CellStyle.BackColor = System.Drawing.Color.LightSteelBlue;
                    break;
                case "E":
                    e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                    break;
                default:
                    e.CellStyle.BackColor = System.Drawing.Color.Ivory;
                    break;
            }
        }

        private void LoansGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            if (loansDataView.RowFilter.Equals(""))
            {
                loansDataView.RowFilter = "ContractType = 'L' AND BookGroup = '" + BookGroupCombo.Text + "'";
            }
            else
            {
                loansDataView.RowFilter = "ContractType = 'L' AND BookGroup = '" + BookGroupCombo.Text + "' AND " + loansDataView.RowFilter;
            }
        }

        private void LoansGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (LoansGrid.Focused)
            {
                try
                {
                    string s = LoansGrid.Columns["SecId"].Text;

                    if (!s.Equals(secId))
                    {
                        secId = s;
                        mainForm.SecId = s;
                    }
                }
                catch
                {
                    secId = "";
                }
            }
        }

        private void LoansGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            switch (LoansGrid.Columns["StatusFlag"].CellText(e.Row))
            {
                case "S":
                    e.CellStyle.BackColor = System.Drawing.Color.LightSteelBlue;
                    break;
                case "E":
                    e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                    break;
                default:
                    e.CellStyle.BackColor = System.Drawing.Color.Honeydew;
                    break;
            }
        }

        private void SummaryParameterChanged(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            mainForm.GridFilterClear(ref SummaryGrid);
            mainForm.GridFooterClear(ref SummaryGrid);

            DataConfig();
            FilterConfig();

            this.Cursor = Cursors.Default;
        }

        private void BizDateCombo_RowChange(object sender, System.EventArgs e)
        {
            if (!BizDateCombo.SelectedText.Equals("")) // Already initialized; will need to load data for a new business date.
            {
                mainForm.Alert("Please wait... Loading contract data for " + BizDateCombo.Text + "...", PilotState.Unknown);
                this.Cursor = Cursors.WaitCursor;

                mainForm.PositionAgent.ContractEvent -= new ContractEventHandler(contractEventWrapper.DoEvent);
                contractEventArgsArray.Clear();
                this.IsReady = false;

                dataSet.Tables["Summary"].Rows.Clear();
                dataSet.Tables["Contracts"].Rows.Clear();

                mainForm.GridFooterClear(ref SummaryGrid);

                mainForm.GridFilterClear(ref SummaryGrid);
                mainForm.GridFilterClear(ref BorrowsGrid);
                mainForm.GridFilterClear(ref LoansGrid);

                Application.DoEvents();

                try
                {
                    if (BizDateCombo.SelectedIndex.Equals(0))
                    {
                        mainForm.PositionAgent.ContractEvent += new ContractEventHandler(contractEventWrapper.DoEvent);
                    }

                    DataRow[] rows = mainForm.PositionAgent.ContractDataGet(mainForm.UtcOffset, BizDateCombo.Text, "", "").Tables["Contracts"].Select();

                    dataSet.Tables["Contracts"].BeginLoadData();
                    foreach (DataRow row in rows)
                    {
                        dataSet.Tables["Contracts"].ImportRow(row);
                    }
                    dataSet.Tables["Contracts"].EndLoadData();

                    int rowIndex = -1;

                    foreach (DataRow dataRowTemp in dataSet.Tables["BookGroups"].Rows)
                    {
                        rowIndex++;

                        if (dataRowTemp["BookGroup"].ToString().Equals(BookGroupCombo.Text))
                        {
                            break;
                        }
                    }

                    if (bool.Parse(dataSet.Tables["BookGroups"].Rows[rowIndex]["MayView"].ToString()))
                    {

                        RateChangeMenuItem.Enabled = false;
                        PcChangeMenuItem.Enabled = false;
                        ReturnMenuItem.Enabled = false;
                        RecallMenuItem.Enabled = false;

                        if (bool.Parse(dataSet.Tables["BookGroups"].Rows[rowIndex]["MayEdit"].ToString()))
                        {
                            RateChangeMenuItem.Enabled = true;
                            PcChangeMenuItem.Enabled = true;
                            ReturnMenuItem.Enabled = true;

                            RecallMenuItem.Enabled = mainForm.AdminAgent.MayEditBookGroup(mainForm.UserId, "PositionRecalls", BookGroupCombo.Text);
                        }
                        else
                        {
                            mainForm.Alert("User: " + mainForm.UserId + ", Permission to EDIT denied.");
                        }
                    }
                    else
                    {
                        loansDataView.RowFilter = "BookGroup = ''";
                        borrowsDataView.RowFilter = "BookGroup = ''";
                        mainForm.Alert("User: " + mainForm.UserId + ", Permission to VIEW denied.");
                    }

                    mainForm.Alert("Loading contract data for " + BizDateCombo.Text + "... Done!", PilotState.Normal);
                }
                catch (Exception error)
                {
                    mainForm.Alert(error.Message, PilotState.RunFault);
                    Log.Write(error.Message + " [PositionOpenContractsForm.BizDateCombo_RowChange]", 1);
                }

                DataConfig();

                this.Cursor = Cursors.Default;
            }
        }

        private void BizDateBookGroup_TextChanged(object sender, System.EventArgs e)
        {
            DataRow[] dataRow = dataSet.Tables["BookGroups"].Select("BookGroup = '" + BookGroupCombo.Text + "'");

            if (dataRow.Length.Equals(1))
            {
                FundingRateTextBox.Text = dataRow[0]["FundingRate"].ToString() + "% [" + dataRow[0]["DayCount"].ToString() + "]";
            }

            int rowIndex = -1;

            foreach (DataRow dataRowTemp in dataSet.Tables["BookGroups"].Rows)
            {
                rowIndex++;

                if (dataRowTemp["BookGroup"].ToString().Equals(BookGroupCombo.Text))
                {
                    break;
                }
            }

            if (bool.Parse(dataSet.Tables["BookGroups"].Rows[rowIndex]["MayView"].ToString()))
            {

                RateChangeMenuItem.Enabled = false;
                PcChangeMenuItem.Enabled = false;
                ReturnMenuItem.Enabled = false;
                RecallMenuItem.Enabled = false;
                SummaryGrid.FilterBar = true;
                LoansGrid.FilterBar = true;
                BorrowsGrid.FilterBar = true;
                SummaryCheckBox.Enabled = true;
                PoolCodeCheckBox.Enabled = true;
                ClassCodeCheckBox.Enabled = true;
                AutoFilterCheckBox.Enabled = true;

                loansDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
                borrowsDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
                //summaryDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";


                if (bool.Parse(dataSet.Tables["BookGroups"].Rows[rowIndex]["MayEdit"].ToString()))
                {
                    RateChangeMenuItem.Enabled = true;
                    PcChangeMenuItem.Enabled = true;
                    ReturnMenuItem.Enabled = true;

                    RecallMenuItem.Enabled = mainForm.AdminAgent.MayEditBookGroup(mainForm.UserId, "PositionRecalls", BookGroupCombo.Text);
                }
                else
                {
                    mainForm.Alert("User: " + mainForm.UserId + ", Permission to EDIT denied.");
                }
            }
            else
            {
                loansDataView.RowFilter = "BookGroup = ''";
                borrowsDataView.RowFilter = "BookGroup = ''";
                //summaryDataView.RowFilter = "BookGroup = ''";
                SummaryGrid.FilterBar = false;
                LoansGrid.FilterBar = false;
                BorrowsGrid.FilterBar = false;
                SummaryCheckBox.Enabled = false;
                PoolCodeCheckBox.Enabled = false;
                ClassCodeCheckBox.Enabled = false;
                AutoFilterCheckBox.Enabled = false;

                mainForm.Alert("User: " + mainForm.UserId + ", Permission to VIEW denied.");
            }

            if (!BookGroupCombo.Text.Equals(""))
            {
                DataConfig();
                FilterConfig();
            }
        }

        private void AutoFilterCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (!AutoFilterCheckBox.Checked)
            {
                mainForm.GridFilterClear(ref SummaryGrid);
                mainForm.GridFilterClear(ref BorrowsGrid);
                mainForm.GridFilterClear(ref LoansGrid);
            }
        }

        private void SummaryGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                string s = SummaryGrid.Columns["BookGroup"].Text + "|"
                  + SummaryGrid.Columns["BookParent"].Text + "|"
                  + SummaryGrid.Columns["Book"].Text + "|"
                  + SummaryGrid.Columns["PoolCode"].Text + "|"
                  + SummaryGrid.Columns["SecId"].Text + "|"
                  + SummaryGrid.Columns["BaseType"].Text;

                if (!s.Equals(rowIdentity))
                {
                    rowIdentity = s;

                    FilterConfig();
                }
            }
            catch { }
        }

        private void SummaryGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            CalcFooterSums();
        }

        private void SummaryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            if (e.Value.Length == 0) // Nothing to format.
            {
                return;
            }

            switch (e.Column.DataField)
            {
                case ("QuantityBorrowed"):
                case ("AmountBorrowed"):
                case ("ValueBorrowed"):
                case ("QuantityLent"):
                case ("AmountLent"):
                case ("ValueLent"):
                case ("AmountLimitBorrow"):
                case ("AmountLimitLoan"):
                case ("AmountLimitTotal"):
                case ("CashFlow"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("#,##0");
                    }
                    catch { }
                    break;

                case ("RateBorrowed"):
                case ("RateLent"):
                case ("ValueBorrowedRatio"):
                case ("ValueLentRatio"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.000");
                    }
                    catch { }
                    break;

                case ("AmountLimitBorrowUsage"):
                case ("AmountLimitLoanUsage"):
                case ("AmountLimitTotalUsage"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.0");
                    }
                    catch { }
                    break;

                case ("Income"):
                case ("MatchIncome"):
                case ("CashIncome"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("#0.00");
                    }
                    catch { }
                    break;
            }
        }

        private void ContractsGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            if (e.Value.Length == 0) // Nothing to format.
            {
                return;
            }

            switch (e.Column.DataField)
            {
                case ("Quantity"):
                case ("QuantitySettled"):
                case ("QuantityRecalled"):
                case ("Amount"):
                case ("AmountSettled"):
                case ("Value"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("#,##0");
                    }
                    catch { }
                    break;

                case ("Margin"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.00");
                    }
                    catch { }
                    break;

                case ("Rate"):
                case ("FeeRate"):
                case ("RebateRate"):
                case ("ValueRatio"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.000");
                    }
                    catch { }
                    break;

                case ("Income"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.00");
                    }
                    catch { }
                    break;

                case ("DivRate"):
                    try
                    {
                        e.Value = decimal.Parse(e.Value).ToString("0.0");
                    }
                    catch { }
                    break;

                case ("ValueDate"):
                case ("SettleDate"):
                case ("TermDate"):
                    try
                    {
                        e.Value = DateTime.Parse(e.Value).ToString(Standard.DateFormat);
                    }
                    catch { }
                    break;
            }
        }

        private void RecallMenuItem_Click(object sender, System.EventArgs e)
        {
            string secId = "";
            bool hasError = false;
            ArrayList dataRows = new ArrayList();

            DataRowView dataRowView;
            DataRow dataRow;

            if (BorrowsGrid.Focused)
            {
                if (BorrowsGrid.SelectedRows.Count == 0)
                {
                    dataRowView = (DataRowView)BorrowsGrid[BorrowsGrid.Row];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;

                    dataRows.Add(dataRow);
                }
                else if (BorrowsGrid.SelectedRows.Count == 1)
                {
                    dataRowView = (DataRowView)BorrowsGrid[0];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;

                    dataRows.Add(dataRow);
                }
                else
                {
                    hasError = true;
                }
            }

            if (LoansGrid.Focused)
            {
                if (LoansGrid.SelectedRows.Count == 0)
                {
                    dataRowView = (DataRowView)LoansGrid[LoansGrid.Row];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;
                    dataRows.Add(dataRow);
                }
                else
                {
                    foreach (int i in LoansGrid.SelectedRows)
                    {
                        if (secId.Equals(""))
                        {
                            secId = LoansGrid[i, "SecId"].ToString();
                        }

                        if (secId.Equals(LoansGrid[i, "SecId"].ToString()))
                        {
                            dataRowView = (DataRowView)LoansGrid[i];
                            dataRow = dataRowView.Row.Table.NewRow();
                            dataRow.ItemArray = dataRowView.Row.ItemArray;

                            dataRows.Add(dataRow);
                        }
                        else
                        {
                            hasError = true;
                        }
                    }
                }
            }

            if (!hasError && (dataRows.Count > 0))
            {
                try
                {
                    PositionRecallInputForm positionRecallInputForm = new PositionRecallInputForm(mainForm, dataRows);
                    positionRecallInputForm.MdiParent = mainForm;
                    positionRecallInputForm.Show();

                    mainForm.Refresh();
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [PositionOpenContractsForm.RecallMenuItem_Click]", Log.Error, 1);
                }
            }
            else
            {
                if ((BorrowsGrid.SelectedRows.Count > 1) && BorrowsGrid.Focused)
                {
                    mainForm.Alert("Must select just one contract for enter a borrow recall.", PilotState.RunFault);
                }
                else if (dataRows.Count == 0)
                {
                    mainForm.Alert("Must select at least one contract for recall.", PilotState.RunFault);
                }
                else
                {
                    mainForm.Alert("Must have just one security in the range for recall.", PilotState.RunFault);
                }
            }
        }

        private void ReturnMenuItem_Click(object sender, System.EventArgs e)
        {
            string secId = "";
            bool hasError = false;
            ArrayList dataRows = new ArrayList();

            DataRowView dataRowView;
            DataRow dataRow;

            if (BorrowsGrid.SelectedRows.Count == 0)
            {
                dataRowView = (DataRowView)BorrowsGrid[BorrowsGrid.Row];
                dataRow = dataRowView.Row.Table.NewRow();
                dataRow.ItemArray = dataRowView.Row.ItemArray;

                dataRows.Add(dataRow);
            }
            else
            {
                BorrowsGrid.Row = BorrowsGrid.SelectedRows[0];

                foreach (int i in BorrowsGrid.SelectedRows)
                {
                    if (secId.Equals(""))
                    {
                        secId = BorrowsGrid[i, "SecId"].ToString();
                    }

                    if (secId.Equals(BorrowsGrid[i, "SecId"].ToString()))
                    {
                        dataRowView = (DataRowView)BorrowsGrid[i];
                        dataRow = dataRowView.Row.Table.NewRow();
                        dataRow.ItemArray = dataRowView.Row.ItemArray;

                        dataRows.Add(dataRow);
                    }
                    else
                    {
                        hasError = true;
                    }
                }
            }

            if (!hasError && (dataRows.Count > 0))
            {
                try
                {
                    PositionReturnInputForm positionReturnInputForm = new PositionReturnInputForm(mainForm, dataRows);
                    positionReturnInputForm.MdiParent = mainForm;
                    positionReturnInputForm.Show();

                    mainForm.Refresh();
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [PositionOpenContractsForm.ReturnMenuItem_Click]", Log.Error, 1);
                }
            }
            else
            {
                if (dataRows.Count == 0)
                {
                    mainForm.Alert("Must select at least one contract for return.", PilotState.RunFault);
                }
                else
                {
                    mainForm.Alert("Must have just one security in the range for return.", PilotState.RunFault);
                }
            }
        }

        private void RateChangeMenuItem_Click(object sender, System.EventArgs e)
        {
            string book = "";
            bool hasError = false;
            ArrayList dataRows = new ArrayList();

            DataRowView dataRowView;
            DataRow dataRow;

            if (BorrowsGrid.Focused)
            {
                if (BorrowsGrid.SelectedRows.Count == 0)
                {
                    dataRowView = (DataRowView)BorrowsGrid[BorrowsGrid.Row];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;

                    dataRows.Add(dataRow);
                }
                else
                {
                    BorrowsGrid.Row = BorrowsGrid.SelectedRows[0];

                    foreach (int i in BorrowsGrid.SelectedRows)
                    {
                        if (book.Equals("")) // Initialize.
                        {
                            book = BorrowsGrid[i, "Book"].ToString();
                        }

                        if (book.Equals(BorrowsGrid[i, "Book"].ToString()))
                        {
                            dataRowView = (DataRowView)BorrowsGrid[i];
                            dataRow = dataRowView.Row.Table.NewRow();
                            dataRow.ItemArray = dataRowView.Row.ItemArray;

                            dataRows.Add(dataRow);
                        }
                        else
                        {
                            hasError = true;
                        }
                    }
                }
            }
            else if (LoansGrid.Focused)
            {
                if (LoansGrid.SelectedRows.Count == 0)
                {
                    dataRowView = (DataRowView)LoansGrid[LoansGrid.Row];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;

                    dataRows.Add(dataRow);
                }
                else
                {
                    LoansGrid.Row = LoansGrid.SelectedRows[0];

                    foreach (int i in LoansGrid.SelectedRows)
                    {
                        if (book.Equals(""))
                        {
                            book = LoansGrid[i, "Book"].ToString();
                        }

                        if (book.Equals(LoansGrid[i, "Book"].ToString()))
                        {
                            dataRowView = (DataRowView)LoansGrid[i];
                            dataRow = dataRowView.Row.Table.NewRow();
                            dataRow.ItemArray = dataRowView.Row.ItemArray;

                            dataRows.Add(dataRow);
                        }
                        else
                        {
                            hasError = true;
                        }
                    }
                }
            }

            if (!hasError && (dataRows.Count > 0))
            {
                try
                {
                    PositionRateChangeInputForm positionRateChangeInputForm = new PositionRateChangeInputForm(mainForm, dataRows, bizDateArray);
                    positionRateChangeInputForm.MdiParent = mainForm;
                    positionRateChangeInputForm.Show();

                    mainForm.Refresh();
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [PositionOpenContractsForm.RateChangeMenuItem_Click]", Log.Error, 1);
                }
            }
            else
            {
                if (dataRows.Count == 0)
                {
                    mainForm.Alert("Must select at least one contract for rate change.", PilotState.RunFault);
                }
                else
                {
                    mainForm.Alert("Must have just one book in the range for rate change.", PilotState.RunFault);
                }
            }
        }

        private void PcChangeMenuItem_Click(object sender, System.EventArgs e)
        {
            ArrayList dataRows = new ArrayList();

            DataRowView dataRowView;
            DataRow dataRow;

            if (BorrowsGrid.Focused)
            {
                if (BorrowsGrid.SelectedRows.Count == 0)
                {
                    dataRowView = (DataRowView)BorrowsGrid[BorrowsGrid.Row];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;

                    dataRows.Add(dataRow);
                }
                else
                {
                    BorrowsGrid.Row = BorrowsGrid.SelectedRows[0];

                    foreach (int i in BorrowsGrid.SelectedRows)
                    {
                        dataRowView = (DataRowView)BorrowsGrid[i];
                        dataRow = dataRowView.Row.Table.NewRow();
                        dataRow.ItemArray = dataRowView.Row.ItemArray;

                        dataRows.Add(dataRow);
                    }
                }
            }
            else if (LoansGrid.Focused)
            {
                if (LoansGrid.SelectedRows.Count == 0)
                {
                    dataRowView = (DataRowView)LoansGrid[LoansGrid.Row];
                    dataRow = dataRowView.Row.Table.NewRow();
                    dataRow.ItemArray = dataRowView.Row.ItemArray;

                    dataRows.Add(dataRow);
                }
                else
                {
                    LoansGrid.Row = LoansGrid.SelectedRows[0];

                    foreach (int i in LoansGrid.SelectedRows)
                    {
                        dataRowView = (DataRowView)LoansGrid[i];
                        dataRow = dataRowView.Row.Table.NewRow();
                        dataRow.ItemArray = dataRowView.Row.ItemArray;

                        dataRows.Add(dataRow);
                    }
                }
            }

            if (dataRows.Count > 0)
            {
                try
                {
                    PositionPcChangeInputForm positionPcChangeInputForm = new PositionPcChangeInputForm(mainForm, dataRows, bizDateArray);
                    positionPcChangeInputForm.MdiParent = mainForm;
                    positionPcChangeInputForm.Show();

                    mainForm.Refresh();
                }
                catch (Exception error)
                {
                    Log.Write(error.Message + " [PositionOpenContractsForm.PcChangeMenuItem_Click]", Log.Error, 1);
                }
            }
            else
            {
                mainForm.Alert("Must select at least one contract for P/C change.", PilotState.RunFault);
            }
        }

        private void ShowIncomeMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowIncomeMenuItem.Checked = !ShowIncomeMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["Income"].Visible = ShowIncomeMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["IncomeTracked"].Visible = ShowIncomeMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["Income"].Visible = ShowIncomeMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["IncomeTracked"].Visible = ShowIncomeMenuItem.Checked;
        }

        private void ShowCurrencyMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowCurrencyMenuItem.Checked = !ShowCurrencyMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["CurrencyIso"].Visible = ShowCurrencyMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["CurrencyIso"].Visible = ShowCurrencyMenuItem.Checked;
        }

        private void ShowTermDateMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowTermDateMenuItem.Checked = !ShowTermDateMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["TermDate"].Visible = ShowTermDateMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["TermDate"].Visible = ShowTermDateMenuItem.Checked;
        }

        private void ShowRecallQuantityMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowRecallQuantityMenuItem.Checked = !ShowRecallQuantityMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["QuantityRecalled"].Visible = ShowRecallQuantityMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["QuantityRecalled"].Visible = ShowRecallQuantityMenuItem.Checked;
        }

        private void ShowDividendReclaimMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowDividendReclaimMenuItem.Checked = !ShowDividendReclaimMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["DivRate"].Visible = ShowDividendReclaimMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["DivCallable"].Visible = ShowDividendReclaimMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["DivRate"].Visible = ShowDividendReclaimMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["DivCallable"].Visible = ShowDividendReclaimMenuItem.Checked;
        }

        private void ShowSettlementDetailMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowSettlementDetailMenuItem.Checked = !ShowSettlementDetailMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["QuantitySettled"].Visible = ShowSettlementDetailMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["SecurityDepot"].Visible = ShowSettlementDetailMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["IsSettledQuantity"].Visible = ShowSettlementDetailMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["AmountSettled"].Visible = ShowSettlementDetailMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["CashDepot"].Visible = ShowSettlementDetailMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["IsSettledAmount"].Visible = ShowSettlementDetailMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["ValueDate"].Visible = ShowSettlementDetailMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["QuantitySettled"].Visible = ShowSettlementDetailMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["SecurityDepot"].Visible = ShowSettlementDetailMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["IsSettledQuantity"].Visible = ShowSettlementDetailMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["AmountSettled"].Visible = ShowSettlementDetailMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["CashDepot"].Visible = ShowSettlementDetailMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["IsSettledAmount"].Visible = ShowSettlementDetailMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["ValueDate"].Visible = ShowSettlementDetailMenuItem.Checked;
        }

        private void ShowValueRatioMenuItem_Click(object sender, System.EventArgs e)
        {
            ShowValueRatioMenuItem.Checked = !ShowValueRatioMenuItem.Checked;

            BorrowsGrid.Splits[0, 1].DisplayColumns["Value"].Visible = ShowValueRatioMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["ValueRatio"].Visible = ShowValueRatioMenuItem.Checked;
            BorrowsGrid.Splits[0, 1].DisplayColumns["ValueIsEstimate"].Visible = ShowValueRatioMenuItem.Checked;

            LoansGrid.Splits[0, 1].DisplayColumns["Value"].Visible = ShowValueRatioMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["ValueRatio"].Visible = ShowValueRatioMenuItem.Checked;
            LoansGrid.Splits[0, 1].DisplayColumns["ValueIsEstimate"].Visible = ShowValueRatioMenuItem.Checked;
        }

        private void DockTopMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Dock = DockStyle.Top;
            this.ControlBox = false;
            this.Text = "";
        }

        private void DockBottomMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Dock = DockStyle.Bottom;
            this.ControlBox = false;
            this.Text = "";
        }

        private void DockNoneMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Dock = DockStyle.None;
            this.ControlBox = true;
            this.Text = TEXT;
        }

        private void ExitMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void MainContextMenu_Popup(object sender, System.EventArgs e)
        {
            RateChangeMenuItem.Enabled = ((BorrowsGrid.Focused || LoansGrid.Focused) && BizDateCombo.SelectedIndex.Equals(0));
            ReturnMenuItem.Enabled = ((BorrowsGrid.Focused) && BizDateCombo.SelectedIndex.Equals(0));
            RecallMenuItem.Enabled = ((BorrowsGrid.Focused || LoansGrid.Focused) && BizDateCombo.SelectedIndex.Equals(0));
            PcChangeMenuItem.Enabled = ((BorrowsGrid.Focused || LoansGrid.Focused) && BizDateCombo.SelectedIndex.Equals(0));
        }

        private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
        {
            if (LoansGrid.Focused)
            {
                this.Cursor = Cursors.WaitCursor;

                Excel excel = new Excel();
                excel.ExportGridToExcel(ref LoansGrid);

                this.Cursor = Cursors.Default;
            }

            if (BorrowsGrid.Focused)
            {
                this.Cursor = Cursors.WaitCursor;

                Excel excel = new Excel();
                excel.ExportGridToExcel(ref BorrowsGrid);

                this.Cursor = Cursors.Default;
            }

            if (SummaryGrid.Focused)
            {
                this.Cursor = Cursors.WaitCursor;

                Excel excel = new Excel();
                excel.ExportGridToExcel(ref SummaryGrid);

                this.Cursor = Cursors.Default;
            }
        }

        private void SummaryGrid_Error(object sender, C1.Win.C1TrueDBGrid.ErrorEventArgs e)
        {
            Log.Write(e.Exception.Message, 1);
            e.Handled = true;
        }

        private void BorrowsGrid_Error(object sender, C1.Win.C1TrueDBGrid.ErrorEventArgs e)
        {
            Log.Write(e.Exception.Message, 1);
            e.Handled = true;
        }

        private void LoansGrid_Error(object sender, C1.Win.C1TrueDBGrid.ErrorEventArgs e)
        {
            Log.Write(e.Exception.Message, 1);
            e.Handled = true;
        }

        private void PositionOpenContracts_TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}