using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight.Chart;
using C1.Silverlight.Data;

using DashApplication;
using DashApplication.ServicePosition;

namespace DashApplication
{
    public partial class SLDashboardBalancesCntrl : UserControl
    {
        private string bizDate;
        private string header;

        private PositionClient psClient;
        private DataTable dtBalances;

        private CustomTypes.ContractTypes cType;

        public SLDashboardBalancesCntrl(string bizDate, CustomTypes.ContractTypes cType, string header)
        {
            InitializeComponent();

            this.cType = cType;
            this.header = header;

            psClient = new PositionClient();
            psClient.BookBalancesGetCompleted += psClient_BookBalancesGetCompleted;

            this.BizDate = bizDate;
        }

        void psClient_BookBalancesGetCompleted(object sender, BookBalancesGetCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                dtBalances = Functions.ConvertToDataTable(e.Result, "Balances");
                BalancesLevelChart();
            }
        }

        private void BalancesLevelChart()
        {
            UtilChart.Reset(true);
            UtilChart.View.AxisY.Title = "Amount $ (MM)";
            UtilChart.Data.ItemNameBinding = new Binding("Name");
            
            switch (cType)
            {
                case CustomTypes.ContractTypes.Borrows:
                    UtilChart.Data.Children.Add(new DataSeries { ValueBinding = new Binding("BorrowPercent"), PointTooltipTemplate = UtilChart.Resources["DataSeries"] as DataTemplate, Label = "Borrows" });
                    break;
                case CustomTypes.ContractTypes.Loans:
                    UtilChart.Data.Children.Add(new DataSeries { ValueBinding = new Binding("LoanPercent"), PointTooltipTemplate = UtilChart.Resources["DataSeries"] as DataTemplate, Label = "Loans" });
                    break;
            }

            
            UtilChart.Data.ItemsSource = dtBalances.DefaultView;        
        }


        public string Header
        {
            get
            {
                return header;
            }

            set
            {
                header = value;
            }
        }

        public string BizDate
        {
            get
            {
                return bizDate;
            }

            set
            {
                bizDate = value;
                psClient.BookBalancesGetAsync(bizDate, "0158");
            }
        }

        void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            var cli = (sender as CheckBox).DataContext as LegendItem;
            if (cli != null)
            {
                var ds = cli.Item as DataSeries;
                if (ds != null)
                {
                    ds.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            var cli = (sender as CheckBox).DataContext as LegendItem;
            if (cli != null)
            {
                var ds = cli.Item as DataSeries;
                if (ds != null)
                {
                    ds.Tag = false;
                    ds.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }
    }
}
