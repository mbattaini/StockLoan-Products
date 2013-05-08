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
    public partial class TradingFundingRatesForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private MainForm mainForm = null;
        private DataSet dsFunds;
        private DataSet dsFundsRates;        

        public TradingFundingRatesForm(MainForm mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;
        }

        private void TradingFundingRatesForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                int height = this.Height;
                int width = this.Width;

                this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
                this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
                this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
                this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

                dsFunds = mainForm.PositionAgent.FundsGet();
                FundDropdown.SetDataBinding(dsFunds, "Funds", true);

                dsFundsRates = mainForm.PositionAgent.FundingRatesGet(mainForm.ServiceAgent.BizDate());
                FundingGrid.SetDataBinding(dsFundsRates, "FundingRates", true);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void TradingFundingRatesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }

            mainForm.tradingFundingRatesForm = null;
        }

        private void FundingGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            switch (e.Column.DataField)
            {
                case "BizDate":
                    try
                    {
                        e.Value = DateTime.Parse(e.Value).ToString(Standard.DateFormat);
                    }
                    catch { }
                    break;

                case "FundingRate":
                    try
                    {
                        e.Value = decimal.Parse(e.Value.ToString()).ToString(Formats.Rate);
                    }
                    catch { }
                    break;

                case "ActTime":
                    try
                    {
                        e.Value = DateTime.Parse(e.Value).ToString(Standard.DateTimeFormat);
                    }
                    catch { }
                    break;
            }
        }

        private void FundingGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            try
            {
                mainForm.PositionAgent.FundingRateSet(
                  FundingGrid.Columns["BizDate"].Text,
                  FundingGrid.Columns["Fund"].Text,
                  FundingGrid.Columns["FundingRate"].Text,
                  mainForm.UserId);

                FundingGrid.Columns["Actor"].Text = mainForm.UserId;
                FundingGrid.Columns["ActTime"].Value = DateTime.Now;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void FundingGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals((char)13))
                {
                    FundingGrid.UpdateData();
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void FundingGrid_OnAddNew(object sender, EventArgs e)
        {
            FundingGrid.Columns["BizDate"].Value = mainForm.ServiceAgent.BizDate();
        }

        private void SendToClipboardCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (FundingGrid.Focused)
                {
                    mainForm.SendToClipboard(ref FundingGrid);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (FundingGrid.Focused)
                {
                    mainForm.SendToExcel(ref FundingGrid, true);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void SendToEmailCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (FundingGrid.Focused)
                {
                    mainForm.SendToEmail(ref FundingGrid);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }


        private void HistoryFundingRateResearchCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                HistoryFundingRateResearchForm historyFundingRateHistoryForm = new HistoryFundingRateResearchForm(mainForm);
                historyFundingRateHistoryForm.Show();
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
            
            this.Cursor = Cursors.Default;
        }
    }
}