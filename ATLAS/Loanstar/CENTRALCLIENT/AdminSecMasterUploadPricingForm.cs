using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using StockLoan.Common;
using StockLoan.MainBusiness;

namespace CentralClient
{
    public partial class AdminSecMasterUploadPricingForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private MainForm mainForm;

        public AdminSecMasterUploadPricingForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                DataSet dsItems = new DataSet();

                DataTable dtitems = new DataTable("Items");
                dtitems.Columns.Add("Isin");
                dtitems.Columns.Add("Sedol");
                dtitems.Columns.Add("Desc");
                dtitems.Columns.Add("Price");
                dtitems.Columns.Add("CurrencyIso");
                dtitems.Columns.Add("CountryCode");
                dtitems.Columns.Add("Symbol");
                dtitems.Columns.Add("SecurityType");
                dtitems.AcceptChanges();

                dsItems.Tables.Add(dtitems);

                DataSet dsBook = new DataSet();

                OleDbConnection dbCn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SecurityBaseFilePathTextBox.Text + ";Extended Properties=\"Excel 8.0;HDR=YES\"");
                OleDbDataAdapter sqlAdapter = new OleDbDataAdapter("SELECT * FROM [SHEET1$]", dbCn);

                sqlAdapter.Fill(dsBook, "Test");

                foreach (DataRow dr in dsBook.Tables["Test"].Rows)
                {
                    if (dr[0].ToString().Equals(""))
                    {
                        dr.Delete();
                    }
                }

                dsBook.AcceptChanges();

                foreach (DataRow dr in dsBook.Tables["Test"].Rows)
                {
                    DataRow tempRow = dsItems.Tables["Items"].NewRow();

                    if (IsinOrdinalTextBox.Text.Equals(""))
                    {
                        tempRow["Isin"] = dr[int.Parse(IsinPositionTextBox.Text)].ToString();
                    }
                    else
                    {
                        tempRow["Isin"] = Tools.SplitItem(dr[int.Parse(IsinPositionTextBox.Text)].ToString(), " ", int.Parse(IsinOrdinalTextBox.Text));
                    }

                    if (SedolOrdinalTextBox.Text.Equals(""))
                    {
                        tempRow["Sedol"] = dr[int.Parse(SedolPositionTextBox.Text)].ToString();
                    }
                    else
                    {
                        tempRow["Sedol"] = Tools.SplitItem(dr[int.Parse(SedolPositionTextBox.Text)].ToString(), " ", int.Parse(SedolOrdinalTextBox.Text));
                    }

                    if (PriceOrdinalTextBox.Text.Equals(""))
                    {
                        tempRow["price"] = dr[int.Parse(PricePositionTextBox.Text)].ToString();
                    }
                    else
                    {
                        tempRow["price"] = Tools.SplitItem(dr[int.Parse(PricePositionTextBox.Text)].ToString(), " ", int.Parse(PriceOrdinalTextBox.Text));
                    }

                    if (CurrIsoOrdinalTextBox.Text.Equals(""))
                    {
                        tempRow["CurrencyIso"] = dr[int.Parse(CurrIsoPositionTextBox.Text)].ToString();
                    }
                    else
                    {
                        tempRow["CurrencyIso"] = Tools.SplitItem(dr[int.Parse(CurrIsoPositionTextBox.Text)].ToString(), " ", int.Parse(CurrIsoOrdinalTextBox.Text));
                    }

                    if (SymbolOrdinalTextBox.Text.Equals(""))
                    {
                        tempRow["Symbol"] = dr[int.Parse(SymbolPositionTextBox.Text)].ToString();
                    }
                    else
                    {
                        tempRow["Symbol"] = Tools.SplitItem(dr[int.Parse(SymbolPositionTextBox.Text)].ToString(), " ", int.Parse(SymbolOrdinalTextBox.Text));
                    }

                    if (CountryOrdinalTextBox.Text.Equals(""))
                    {
                        tempRow["CountryCode"] = dr[int.Parse(CountryPositionTextBox.Text)].ToString();
                    }
                    else
                    {
                        tempRow["CountryCode"] = Tools.SplitItem(dr[int.Parse(CountryPositionTextBox.Text)].ToString(), " ", int.Parse(CountryOrdinalTextBox.Text));
                    }

                    if (SecTypeOrdinalTextBox.Text.Equals(""))
                    {
                        tempRow["SecurityType"] = dr[int.Parse(SecTypePositionTextBox.Text)].ToString();
                    }
                    else
                    {
                        tempRow["SecurityType"] = Tools.SplitItem(dr[int.Parse(SecTypePositionTextBox.Text)].ToString(), " ", int.Parse(SecTypeOrdinalTextBox.Text));
                    }

                    if (DescOrdinalTextBox.Text.Equals(""))
                    {
                        tempRow["Desc"] = dr[int.Parse(DescPositionTextBox.Text)].ToString();
                    }
                    else
                    {
                        tempRow["Desc"] = Tools.SplitItem(dr[int.Parse(DescPositionTextBox.Text)].ToString(), " ", int.Parse(DescOrdinalTextBox.Text));
                    }

                    dsItems.AcceptChanges();

                    dsItems.Tables["Items"].Rows.Add(tempRow);
                    dsItems.AcceptChanges();
                }


                foreach (DataRow dr2 in dsItems.Tables["Items"].Rows)
                {
                    if (dr2[0].ToString().Trim().Equals(""))
                    {
                        dr2.Delete();
                    }
                }

                PricingGrid.SetDataBinding(dsItems, "Items");
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }    

        private void LocateFilePathButton_Click(object sender, EventArgs e)
        {
            SecurityBaseFileDialog.ShowDialog();
            SecurityBaseFilePathTextBox.Text = SecurityBaseFileDialog.FileName;
        }

        private void CommitButton_Click(object sender, EventArgs e)
        {
            SecMasterBackgroundWorker.RunWorkerAsync();
        }

        private void SecMasterBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int progressCount = 0;
      
            for (int count = 0; count < PricingGrid.RowCount; count++)
            {
                mainForm.ServiceAgent.SecMasterItemSet(
                    PricingGrid.Columns["Sedol"].CellText(count),
                    PricingGrid.Columns["Desc"].CellText(count),
                    PricingGrid.Columns["SecurityType"].CellText(count).Substring(0, 1),
                    PricingGrid.Columns["SecurityType"].CellText(count),
                    PricingGrid.Columns["CountryCode"].CellText(count),
                    PricingGrid.Columns["CurrencyIso"].CellText(count),
                    "",
                    "",
                    "",
                    "",
                    PricingGrid.Columns["Symbol"].CellText(count),
                    PricingGrid.Columns["Isin"].CellText(count),
                    "",
                    PricingGrid.Columns["Price"].CellText(count));

                if (progressCount < 100)
                {
                    progressCount = progressCount + 1;
                }
                else
                {
                    progressCount = 1;
                }

                SecMasterBackgroundWorker.ReportProgress(progressCount, (PricingGrid.Columns["Sedol"].CellText(count) + " : " + PricingGrid.Columns["Symbol"].CellText(count)));
            }

            progressCount = 100;
            SecMasterBackgroundWorker.ReportProgress(progressCount, "");        
        }

        private void SecMasterBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                StatusMessageLabel.Text = e.UserState.ToString();
            }

            SecMasterRibbonProgressBar.Value = e.ProgressPercentage;
        }

        private void SecMasterBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusMessageLabel.Text = "Done.";             
        }
    }
}
