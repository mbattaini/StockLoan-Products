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
    public partial class HistoryFundingRateResearchForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private DataSet dsFunds;
        private DataSet dsFundingRates;

        private MainForm mainForm;
        private string fund = "";

        public HistoryFundingRateResearchForm(MainForm mainForm)
        {
            InitializeComponent();

            StartDateEdit.Value = mainForm.ServiceAgent.BizDate();
            StopDateEdit.Value = mainForm.ServiceAgent.BizDate();

            this.mainForm = mainForm;
        }

        private void HistoryFundingRateResearchForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsFunds = mainForm.PositionAgent.FundsGet();
                FundsGrid.SetDataBinding(dsFunds, "Funds", true);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void FundsGrid_Paint(object sender, PaintEventArgs e)
        {
            if (!fund.Equals(FundsGrid.Columns["Fund"].Text))
            {
                try
                {
                    LoadData();
                    fund = FundsGrid.Columns["Fund"].Text;
                }
                catch (Exception error)
                {
                    mainForm.Alert(this.Name, error.Message);
                }
            }
        }


        private void LoadData()
        {
            dsFundingRates = mainForm.PositionAgent.FundingRateResearchGet(StartDateEdit.Text, StopDateEdit.Text, FundsGrid.Columns["Fund"].Text);
            FundingRateResearchGrid.SetDataBinding(dsFundingRates, "FundingRates", true);

            FundChart.DataSource = dsFundingRates.Tables["FundingRates"];


            C1.Win.C1Chart.ChartDataSeriesCollection dsc = FundChart.ChartGroups[0].ChartData.SeriesList;
            dsc.Clear();

            FundChart.ChartArea.AxisX.Text = "BizDate";
            FundChart.ChartArea.AxisY.Text = "Fund Rate";

            C1.Win.C1Chart.ChartDataSeries ds = dsc.AddNewSeries();
            ds.LegendEntry = true;
            ds.X.DataField = "BizDate";
            ds.Y.DataField = "FundingRate";
            ds.Label = "Funding Rates";
        }

        private void FundingRateResearchGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            try
            {
                mainForm.PositionAgent.FundingRateSet(
                  FundingRateResearchGrid.Columns["BizDate"].Text,
                  FundingRateResearchGrid.Columns["Fund"].Text,
                  FundingRateResearchGrid.Columns["FundingRate"].Text,
                  mainForm.UserId);

                FundingRateResearchGrid.Columns["Actor"].Text = mainForm.UserId;
                FundingRateResearchGrid.Columns["ActTime"].Value = DateTime.Now;
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void StartDateEdit_ValueChanged(object sender, EventArgs e)
        {       
            try
            {
                if (!StartDateEdit.Text.Equals("") && dsFunds != null)
                {
                    LoadData();                        
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void FundingRateResearchGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
        {
            e.Value = mainForm.Format(e.Column.DataField, e.Value);   
        }


    }
}
