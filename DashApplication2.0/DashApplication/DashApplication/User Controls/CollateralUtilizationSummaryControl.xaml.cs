using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

using DashApplication.ServicePosition;

using C1.Silverlight.Data;
using C1.Silverlight.Chart;
using C1.Silverlight.Chart.Extended;

using DashApplication.CustomClasses;

namespace DashApplication
{
    public partial class CollateralUtilizationSummaryControl : UserControl
    {
        private PositionClient psClient;
        private PositionClient psClient2;

        public CollateralUtilizationSummaryControl()
        {
            InitializeComponent();
                      
            psClient = new PositionClient();            
            psClient.KeyValueGetCompleted += psClient_KeyValueGetCompleted; // Percent Keyvlaue
            psClient.KeyValueSetCompleted += psClient_KeyValueSetCompleted;

            psClient2 = new PositionClient();
            psClient2.KeyValueGetCompleted += psClient2_KeyValueGetCompleted; // Price Threshold
            psClient2.KeyValueSetCompleted += psClient2_KeyValueSetCompleted;        

            UtilChart.Reset(true);
            psClient.KeyValueGetAsync("CollateralUtilPercentThreshold");
            psClient2.KeyValueGetAsync("CollateralUtilPriceThreshold");

            CustomEvents.CollateralDetailChanged += CustomEvents_CollateralDetailChanged;
            CustomEvents.CheckChanged += CustomEvents_CheckChanged;
            CustomEvents.RemoveControlChanged += CustomEvents_RemoveControlChanged;

            TotalCollateralUtilRadioButton.IsChecked = true;
            StartOfDayRadioButton.IsChecked = true;
        }

        void CustomEvents_RemoveControlChanged(object sender, RemoveControlEventArgs e)
        {
            MainStackPanel.Children.Remove(e.Item);
            ChartChange();
        }

        void CustomEvents_CheckChanged(object sender, CheckEventArgs e)
        {
            ChartChange();
        }

        void CustomEvents_CollateralDetailChanged(object sender, CollateralDetailEventArgs e)
        {
            MainTabControl.Items.Add(CustomCollateralTabItem.Create(new CollateralUtilizationShellControl(e.CollateralDetail), "Collateral Detail-" + e.Header,  true));
        }
    
        void psClient2_KeyValueSetCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                C1.Silverlight.C1MessageBox.Show("Useless Collateral Price Threshold Updated");
            }
        }

        void psClient_KeyValueSetCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                C1.Silverlight.C1MessageBox.Show("Utilized Collateral Prcent Threshold Updated");

                ChartChange();
                DrawThresholdLine();
            }
        }

        void psClient2_KeyValueGetCompleted(object sender, KeyValueGetCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                double nPrice = 0;

                if (double.TryParse(e.Result, out nPrice))
                {
                    PriceUselessThresholdNumericBox.Value = nPrice;
                    CustomAnimations.Shimmy(PriceUselessThresholdNumericBox);                
                }
            }
        }

        void psClient_KeyValueGetCompleted(object sender, KeyValueGetCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                double nPrice = 0;

                if (double.TryParse(e.Result, out nPrice))
                {
                    PrecentThresholdNumericBox.Value = nPrice;
                    CustomAnimations.Shimmy(PrecentThresholdNumericBox);
                }
            }
        }

        private void BizDatePicker_DateTimeChanged(object sender, C1.Silverlight.DateTimeEditors.NullablePropertyChangedEventArgs<DateTime> e)
        {
            if (BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd").Equals(DateTime.Today.ToString("yyyy-MM-dd")))
            {
                LookUpHyperLink.IsEnabled = true;
                IntraDayRadioButton.IsEnabled = true;
                
                StartOfDayRadioButton.IsChecked = true;

                AddChild("", (bool)StartOfDayRadioButton.IsChecked, true);
            }
            else
            {
                LookUpHyperLink.IsEnabled = false;
                IntraDayRadioButton.IsEnabled = false;

                StartOfDayRadioButton.IsChecked = true;

                AddChild("", (bool)StartOfDayRadioButton.IsChecked, false);
            }
        }

        private void AddChild(string priceFilter, bool startOfDay, bool isToday)
        {
            if (priceFilter.Equals(""))
            {
                CollateralUtilizationSummaryDetailControl _cDetail = new CollateralUtilizationSummaryDetailControl(BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd"), priceFilter, "Total Collateral Utilization", true, startOfDay, isToday);
                MainStackPanel.Children.Add(_cDetail);
            }
            else
            {
                CollateralUtilizationSummaryDetailControl _cDetail = new CollateralUtilizationSummaryDetailControl(BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd"), priceFilter, "Collateral Under " + priceFilter, true, startOfDay, isToday);
                MainStackPanel.Children.Add(_cDetail);
            }
        }

        void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)TotalCollateralUtilRadioButton.IsChecked)
            {
                var cli = (sender as CheckBox).DataContext as LegendItem;
                if (cli != null)
                {
                    var ds = cli.Item as XYDataSeries;
                    if (ds != null)
                    {
                        ds.Tag = true;
                        ds.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
            else
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
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((bool)TotalCollateralUtilRadioButton.IsChecked)
            {
                var cli = (sender as CheckBox).DataContext as LegendItem;
                if (cli != null)
                {
                    var ds = cli.Item as XYDataSeries;
                    if (ds != null)
                    {
                        ds.Tag = false;
                        ds.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
            else
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

        private decimal BankLoanAmount(DataTable dtCollateral)
        {
            decimal amount = 0;

            foreach (DataRow dr in dtCollateral.Rows)
            {
                amount += (decimal)dr["LoanAmount"] + (decimal)dr["OCCAmt"] + (decimal)dr["HarrisBankAmt"] + (decimal)dr["USBankAmt"] + (decimal)dr["JPMBankAmt"] + (decimal)dr["BonyBankAmt"];
            }

            return amount;
        }

        private decimal MarginDebitAmount(DataTable dtCollateral)
        {
            decimal amount = 0;
            
            foreach (DataRow dr in dtCollateral.Rows)
            {
                amount += (decimal)dr["MarginDebitBalance140Amt"] + (decimal)dr["BorrowAmount"];
            }

            return amount;
        }

        private decimal HighestAmount()
        {
            decimal amount = 0;
            decimal currentAmount = 0;

            foreach (UIElement obj in MainStackPanel.Children)
            {
                if (((CollateralUtilizationSummaryDetailControl)obj).ApplyToChart)
                {
                    currentAmount = MarginDebitAmount(((CollateralUtilizationSummaryDetailControl)obj).CollateralTable);                    
                }

                if (currentAmount > amount)
                {
                    amount = currentAmount;
                }
            }

            return amount;
        }
   
        private void TotalCollateralUtilizationChart()
        {                        
            UtilChart.Reset(true);
            UtilChart.ChartType = ChartType.LineSymbolsSmoothed;
            UtilChart.View.AxisY.Title = "Percent (%)";
            UtilChart.View.AxisX.Title = "Amount $ (MM)";
            decimal totalAmount = 0;
            decimal utilizedAmount = 0;
            decimal maximumAmount = HighestAmount();

            //Chart Highest Value

            XYDataSeries maximumItem = new XYDataSeries();
            maximumItem.Tag = true;

            DoubleCollection dblCll = new DoubleCollection();
            dblCll.Add(0.0);
            dblCll.Add(100.0);

            DoubleCollection xDblCll = new DoubleCollection();
            xDblCll.Add(0.0);
            xDblCll.Add((double)(maximumAmount / 1000000));

            maximumItem.XValues = xDblCll;
            maximumItem.Values = dblCll;

            maximumItem.Label = "Total Collateral ($)";
            maximumItem.PointLabelTemplate = UtilChart.Resources["XYSeries"] as DataTemplate;
            
            //Chart End

            foreach (UIElement obj in MainStackPanel.Children)
            {
                if (((CollateralUtilizationSummaryDetailControl)obj).ApplyToChart)
                {

                    totalAmount = 0;
                    utilizedAmount = 0;

                    DataTable dtCollateral = ((CollateralUtilizationSummaryDetailControl)obj).CollateralTable;
                    string priceFilter = ((CollateralUtilizationSummaryDetailControl)obj).Price;

                    XYDataSeries totalItem = new XYDataSeries();
                    totalItem.Tag = true;

                    totalAmount = MarginDebitAmount(dtCollateral);
                    utilizedAmount = BankLoanAmount(dtCollateral);

                    xDblCll = new DoubleCollection();
                    xDblCll.Add(0.0);
                    xDblCll.Add((double)(totalAmount / 1000000));

                    dblCll = new DoubleCollection();
                    dblCll.Add(0.0);
                    dblCll.Add(Functions.Percent(maximumAmount, totalAmount));

                    totalItem.XValues = xDblCll;
                    totalItem.Values = dblCll;
                    totalItem.Label = "Total Collateral ($" + priceFilter + ")";
                    totalItem.PointLabelTemplate = UtilChart.Resources["XYSeries"] as DataTemplate;


                    UtilChart.Data.Children.Add(totalItem);

                    XYDataSeries utilizationItem = new XYDataSeries();
                    utilizationItem.Tag = true;

                    double percentage = Functions.Percent(maximumAmount, utilizedAmount);

                    dblCll = new DoubleCollection();
                    dblCll.Add(0.0);
                    dblCll.Add(percentage);

                    double temp1 = (double)(utilizedAmount / 1000000);
                    xDblCll = new DoubleCollection();
                    xDblCll.Add(0.0);
                    xDblCll.Add(temp1);

                    utilizationItem.XValues = xDblCll;
                    utilizationItem.Values = dblCll;

                    if (percentage >= PrecentThresholdNumericBox.Value)
                    {
                        utilizationItem.PointLabelTemplate = UtilChart.Resources["XYSeriesOverThreshold"] as DataTemplate;
                    }
                    else
                    {
                        utilizationItem.PointLabelTemplate = UtilChart.Resources["XYSeries"] as DataTemplate;
                    }
                    utilizationItem.Label = "Utilized Collateral ($" + priceFilter + ")";
                    UtilChart.Data.Children.Add(utilizationItem);
                }
            }
                                                 
            DrawThresholdLine();            
        }

        private void DrawThresholdLine()
        {
            if ((bool)TotalCollateralUtilRadioButton.IsChecked)
            {
                PrecentThresholdNumericBox.IsReadOnly = false;

                UtilChart.BeginUpdate();
                ChartPanel cPanel = new ChartPanel();
                ChartPanelObject cPanelObject = new ChartPanelObject();

                Border bdr = new Border()
                {
                    BorderBrush = Background = new SolidColorBrush(Colors.Red),
                    Padding = new Thickness(2),
                };

                bdr.BorderThickness = new Thickness(0, 2, 0, 0);
                bdr.Margin = new Thickness(0, -1, 0, 0);
                cPanelObject.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                cPanelObject.DataPoint = new Point(double.NaN, PrecentThresholdNumericBox.Value);
                cPanel.Tag = "Max";
                bdr.IsHitTestVisible = false;
                cPanel.Background = null;
                cPanelObject.Content = bdr;
                cPanel.Children.Add(cPanelObject);
                cPanelObject.Action = ChartPanelAction.None;

                UtilChart.View.Layers.Clear();
                UtilChart.View.Layers.Add(cPanel);
                UtilChart.EndUpdate();
            }
            else
            {
                UtilChart.BeginUpdate();
                UtilChart.View.Layers.Clear();
                PrecentThresholdNumericBox.IsReadOnly = true;
                UtilChart.EndUpdate();
            }
        }     
                
        private void BaseTypeCollateralLevelChart()
        {
            double bondAmount = 0.0;
            double equityAmount = 0.0;
            double unknownAmount = 0.0;

            UtilChart.Reset(true);
            UtilChart.ChartType = ChartType.Column;
            UtilChart.View.AxisY.Title = "Amount $ (MM)";
            DrawThresholdLine();

            foreach (UIElement obj in MainStackPanel.Children)
            {
                if (((CollateralUtilizationSummaryDetailControl)obj).ApplyToChart)
                {
                    DataTable dtCollateral = ((CollateralUtilizationSummaryDetailControl)obj).CollateralTable;
                    string priceFilter = ((CollateralUtilizationSummaryDetailControl)obj).Price;
                    
                    DataSeries bondItem = new DataSeries();
                    DataSeries equityItem = new DataSeries();
                    DataSeries unknownItem = new DataSeries();

                    if (priceFilter.Equals(""))
                    {
                        bondItem.Label = "Bond ($)";
                        equityItem.Label = "Equity ($)";
                        unknownItem.Label = "Unknown ($)";
                    }
                    else
                    {
                        bondItem.Label = "Bond ($"+priceFilter+")";
                        equityItem.Label = "Equity ($"+priceFilter+")";
                        unknownItem.Label = "Unknown ($"+priceFilter+")";
                    }

                    bondItem.Tag = true;
                    bondItem.PointTooltipTemplate = UtilChart.Resources["DataSeries"] as DataTemplate;
                    bondItem.Values = new DoubleCollection();

                    equityItem.Tag = true;
                    equityItem.PointTooltipTemplate = UtilChart.Resources["DataSeries"] as DataTemplate;
                    equityItem.Values = new DoubleCollection();

                    unknownItem.Tag = true;
                    unknownItem.PointTooltipTemplate = UtilChart.Resources["DataSeries"] as DataTemplate;
                    unknownItem.Values = new DoubleCollection();


                    bondAmount = (double)(Functions.DataTableColumnSum(dtCollateral, "MarginDebitBalance140Amt", "BaseType", "bond") / 1000000);
                    equityAmount = (double)(Functions.DataTableColumnSum(dtCollateral, "MarginDebitBalance140Amt", "BaseType", "equity") / 1000000);
                    unknownAmount = (double)(Functions.DataTableColumnSum(dtCollateral, "MarginDebitBalance140Amt", "BaseType", "unknown") / 1000000);

                    bondItem.Values.Add(bondAmount);
                    equityItem.Values.Add(equityAmount);
                    unknownItem.Values.Add(unknownAmount);

                    UtilChart.Data.Children.Add(bondItem);
                    UtilChart.Data.Children.Add(equityItem);
                    UtilChart.Data.Children.Add(unknownItem);
                }
            }
        }

        private void ProductCollateralLevelChart()
        {
            UtilChart.Reset(true);
            UtilChart.ChartType = ChartType.Column;
            UtilChart.View.AxisY.Title = "Amount $ (MM)";
            DrawThresholdLine();


            foreach (UIElement obj in MainStackPanel.Children)
            {
                if (((CollateralUtilizationSummaryDetailControl)obj).ApplyToChart)
                {
                    DataTable dtCollateral = ((CollateralUtilizationSummaryDetailControl)obj).CollateralTable;
                    string priceFilter = ((CollateralUtilizationSummaryDetailControl)obj).Price;

                    foreach (DataRow dr in dtCollateral.Rows)
                    {
                        DataSeries item = new DataSeries();

                        if (priceFilter.Equals(""))
                        {
                            item.Label = dr["Class"].ToString() + "($)";
                        }
                        else
                        {
                            item.Label = dr["Class"].ToString() + " ($" + priceFilter +")";
                        }

                        decimal amount = (decimal)dr["MarginDebitBalance140Amt"];

                        double totalAmount = (double)(amount / 1000000);

                        DoubleCollection dblCll = new DoubleCollection();
                        dblCll.Add(totalAmount);

                        item.Values = dblCll;
                        item.Tag = true;
                        item.PointTooltipTemplate = UtilChart.Resources["DataSeries"] as DataTemplate;

                        UtilChart.Data.Children.Add(item);
                    }
                }
            }
        }

        private void ChartChange()
        {
            if ((bool)TotalCollateralUtilRadioButton.IsChecked)
            {
                CustomAnimations.FadeOut(UtilChart);

                TotalCollateralUtilizationChart();

                CustomAnimations.FadeIn(UtilChart);
            }
            else if ((bool)ProductCollateralLevelRadioButton.IsChecked)
            {
                CustomAnimations.FadeOut(UtilChart);

                ProductCollateralLevelChart();

                CustomAnimations.FadeIn(UtilChart);
            }
            else if ((bool)BaseTypeCollateralRadioButton.IsChecked)
            {
                CustomAnimations.FadeOut(UtilChart);

                BaseTypeCollateralLevelChart();

                CustomAnimations.FadeIn(UtilChart);
            }
        }

        private void ChartCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ChartChange();
        }

        private void PriceUselessThresholdNumericBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                psClient2.KeyValueSetAsync("CollateralUtilPriceThreshold", PriceUselessThresholdNumericBox.Value.ToString());
            }
        }

        private void PrecentThresholdNumericBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                psClient.KeyValueSetAsync("CollateralUtilPercentThreshold", PrecentThresholdNumericBox.Value.ToString());
            }
        }
                      
        private void ExportToImageButton_Click(object sender, RoutedEventArgs e)
        {            
            var dialog = new System.Windows.Controls.SaveFileDialog()
            {
                DefaultExt = "*.jpg",
                Filter = "Jpeg .jpg (*.jpg)|*.jpg|All files (*.*)|*.*",
            };

            if (dialog.ShowDialog() == false) return;

            using (var stream = dialog.OpenFile())
            {
                UtilChart.SaveImage(stream,ImageFormat.Jpeg);
            }
        }

        private void LookUpHyperLink_Click(object sender, RoutedEventArgs e)
        {
            AddChild(PriceUselessThresholdNumericBox.Value.ToString(), false, true);
        }
    }
}