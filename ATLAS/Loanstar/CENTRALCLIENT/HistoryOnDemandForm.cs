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
    public partial class HistoryOnDemandForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        public MainForm mainForm = null;
        public string bookGroup = "";
        public string contractId = "";

        private DataSet dsMarks;
        private DataSet dsReturns;
        private DataSet dsContracts;
        private DataSet dsCash;

        public HistoryOnDemandForm(MainForm mainForm, string bookGroup, string contractId)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            this.bookGroup = bookGroup;
            this.contractId = contractId;

            this.Text = this.Text + " Contract ID: " + contractId;
        }

        private void HistoryToMarksForm_Load(object sender, EventArgs e)
        {            
            try
            {
                dsMarks = mainForm.PositionAgent.MarksGet("", "", bookGroup, contractId, mainForm.UtcOffset);
                MarksGrid.SetDataBinding(dsMarks, "Marks", true);

                dsReturns = mainForm.PositionAgent.ReturnsGet("", "", bookGroup, contractId, mainForm.UtcOffset);
                ReturnsGrid.SetDataBinding(dsReturns, "Returns", true);

                dsContracts = mainForm.PositionAgent.ContractResearchDataGet("", bookGroup, "", contractId, "", "", "");
                ContractsGrid.SetDataBinding(dsContracts, "Contracts", true);

                dsCash = mainForm.PositionAgent.CashGet(bookGroup, contractId, mainForm.UtcOffset);
                CashGrid.SetDataBinding(dsCash, "Cash", true);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void Grid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            e.Value = mainForm.Format(e.Column.DataField, e.Value);
        }

        private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            switch (HistoryDockingTab.SelectedIndex)
            {
                case 0:
                    mainForm.SendToExcel(ref MarksGrid, false);
                    break;

                case 1:
                    mainForm.SendToExcel(ref ReturnsGrid, false);
                    break;

                case 2:
                    mainForm.SendToExcel(ref ContractsGrid, false);
                    break;
                
                case 3:
                    mainForm.SendToExcel(ref CashGrid, false);
                    break;
            }
        }


        private void Grid_FetchCellStyle(object sender, C1.Win.C1TrueDBGrid.FetchCellStyleEventArgs e)
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
    }
}
