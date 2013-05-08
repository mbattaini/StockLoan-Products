using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight.Data;
using C1.Silverlight.Chart;

using StockLoan_LocatesClient.ServiceLocates;

namespace StockLoan_LocatesClient
{
    public partial class SecurityMasterControl : UserControl
    {
        public bool isMinimized = false;

        private LocatesServiceClient lsClient;

        public SecurityMasterControl()
        {
            InitializeComponent();

            lsClient = new LocatesServiceClient();            
            lsClient.BoxPositionItemGetCompleted += new EventHandler<BoxPositionItemGetCompletedEventArgs>(lsClient_BoxPositionItemGetCompleted);
            lsClient.SecMasterItemGetCompleted += new EventHandler<SecMasterItemGetCompletedEventArgs>(lsClient_SecMasterItemGetCompleted);

            CustomEvents.SecIdChanged += new EventHandler<SecIdEventArgs>(CustomEvents_SecIdChanged);            
        }

        void CustomEvents_SecIdChanged(object sender, SecIdEventArgs e)
        {         
                this.Cursor = Cursors.Wait;
         
				SecurityIdTextBox.Text = e.SecId;

                lsClient.SecMasterItemGetAsync(SecurityIdTextBox.Text);

                lsClient.BoxPositionItemGetAsync("0158", SecurityIdTextBox.Text);
        }

        private void lsClient_SecMasterItemGetCompleted(object sender, SecMasterItemGetCompletedEventArgs e)
        {
            DataTable dtSecMasterItem = new DataTable();

            if (e.Error != null)
            {
                return;
            }

            dtSecMasterItem = Functions.ConvertToDataTable(e.Result, "SecMasterItem");

            if (dtSecMasterItem.Rows.Count > 0)
                PopulateSecMaster(dtSecMasterItem.Rows[0]);

            this.Cursor = Cursors.Arrow;
        }

        private void lsClient_BoxPositionItemGetCompleted(object sender, BoxPositionItemGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtBoxPosition = Functions.ConvertToDataTable(e.Result, "BoxPositionItem");

            if (dtBoxPosition.Rows.Count > 0)
                PopulateBoxPosition(dtBoxPosition.Rows[0]);

            this.Cursor = Cursors.Arrow;
        }      

        private void PopulateSecMaster(DataRow dr)
        {
            SecurityIdTextBox.Text = dr["SecId"].ToString();                       
            SecDescTextBox.Text = dr["Description"].ToString();                       
            SymbolTextBox.Text = dr["Symbol"].ToString();
            PriceTexBox.Text = Decimal.Parse(dr["LastPrice"].ToString()).ToString("#,##0.00") + " | " + DateTime.Parse(dr["LastPriceDate"].ToString()).ToString("yyyy-MM-dd");
            SecDetailsTextBox.Text = dr["BaseType"].ToString() + " | " + dr["ClassGroup"].ToString() + " | " + dr["CountryCode"].ToString();
            try
            {
                RecordDateTextBox.Text = DateTime.Parse(dr["RecordDateCash"].ToString()).ToString("yyyy-MM-dd");
            }
            catch { }            
        }

        private void FindToolbarButton_Click(object sender, RoutedEventArgs e)
        {
            
                this.Cursor = Cursors.Wait;

                lsClient.SecMasterItemGetAsync(SecurityIdTextBox.Text);

                lsClient.BoxPositionItemGetAsync("0158", SecurityIdTextBox.Text);            
        }

        private void PopulateBoxPosition(DataRow dr)
        {
            try
            {
                BorrowSettledTextBox.Text = long.Parse(dr["Borrow"].ToString()).ToString("#,##0");
            }
            catch {BorrowSettledTextBox.Text = "0"; }

            try
            {
                LoanSettledTextBox.Text = long.Parse(dr["Loan"].ToString()).ToString("#,##0");
            }
            catch { LoanSettledTextBox.Text = "0"; }
            
            LongSettledTextBox.Text = long.Parse(dr["LongsSettled"].ToString()).ToString("#,##0");
            LongTradedTextBox.Text = long.Parse(dr["LongsTraded"].ToString()).ToString("#,##0");

            ShortSettledTextBox.Text = long.Parse(dr["ShortsSettled"].ToString()).ToString("#,##0");
            ShortTradedTextBox.Text = long.Parse(dr["ShortsTraded"].ToString()).ToString("#,##0");

            PledgeSettledTextBox.Text = long.Parse(dr["PledgeSettled"].ToString()).ToString("#,##0");
            PledgeTradedTextBox.Text = long.Parse(dr["PledgeTraded"].ToString()).ToString("#,##0");

            SegSettledTextBox.Text = long.Parse(dr["SegReqSettled"].ToString()).ToString("#,##0");
            SegTradedTextBox.Text = long.Parse(dr["SegReqSettled"].ToString()).ToString("#,##0");

            ExcessSettledTextBox.Text = long.Parse(dr["ExDeficitSettled"].ToString()).ToString("#,##0");
            ExcessTradedTextBox.Text = long.Parse(dr["ExDeficitTraded"].ToString()).ToString("#,##0");

            ClearingFTDSettledTextBox.Text = long.Parse(dr["ClearingFailOutSettled"].ToString()).ToString("#,##0");
            ClearingFTRSettledTextBox.Text = long.Parse(dr["ClearingFailInSettled"].ToString()).ToString("#,##0");
            ClearingFTDTradedTextBox.Text = long.Parse(dr["ClearingFailOutTraded"].ToString()).ToString("#,##0");
            ClearingFTRTradedTextBox.Text = long.Parse(dr["ClearingFailInTraded"].ToString()).ToString("#,##0");
            ClearingFTDDayCount.Text = long.Parse(dr["ClearingFailInDayCount"].ToString()).ToString("#,##0");
            ClearingFTRDayCount.Text = long.Parse(dr["ClearingFailOutDayCount"].ToString()).ToString("#,##0");

            DvpFTDSettledTextBox.Text = long.Parse(dr["DvpFailOutSettled"].ToString()).ToString("#,##0");
            DvpFTRSettledTextBox.Text = long.Parse(dr["DvpFailInSettled"].ToString()).ToString("#,##0");
            DvpFTDTradedTextBox.Text = long.Parse(dr["DvpFailOutTraded"].ToString()).ToString("#,##0");
            DvpFTRTradedTextBox.Text = long.Parse(dr["DvpFailInTraded"].ToString()).ToString("#,##0");
            DvpFTDDayCount.Text = long.Parse(dr["DvpFailInDayCount"].ToString()).ToString("#,##0");
            DvpFTRDayCount.Text = long.Parse(dr["DvpFailOutDayCount"].ToString()).ToString("#,##0");

            BrokerFTDSettledTextBox.Text = long.Parse(dr["BrokerFailOutSettled"].ToString()).ToString("#,##0");
            BrokerFTRSettledTextBox.Text = long.Parse(dr["BrokerFailInSettled"].ToString()).ToString("#,##0");
            BrokerFTDTradedTextBox.Text = long.Parse(dr["BrokerFailOutTraded"].ToString()).ToString("#,##0");
            BrokerFTRTradedTextBox.Text = long.Parse(dr["BrokerFailInTraded"].ToString()).ToString("#,##0");
            BrokerFTDDayCount.Text = long.Parse(dr["BrokerFailInDayCount"].ToString()).ToString("#,##0");
            BrokerFTRDayCount.Text = long.Parse(dr["BrokerFailOutDayCount"].ToString()).ToString("#,##0");
        }
    }
}