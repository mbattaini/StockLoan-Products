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


using C1.Silverlight.Data;
using DashApplication.ServicePosition;
using DashApplication.CustomClasses;

namespace DashApplication
{
    public partial class CollateralUtilizationSummaryDetailControl : UserControl
    {
        private PositionClient psClient;
        private DataTable dtCollateral;
        private bool startOfDay;


        Storyboard storyBoard;
        DoubleAnimation dblAnimation;
        RotateTransform rtransform;

        private decimal MarginDebitTotalNoPrice = 0;

        private string priceFilter;
        private string bizDate;

        private bool canUserClose;

        private void BeginLoad()
        {
            CustomAnimations.FadeOut(CollateralLayout);
            CollateralLayout.Visibility = Visibility.Collapsed;

            CustomAnimations.FadeIn(LoadingLayout);
            LoadingLayout.Visibility = Visibility.Visible;

            LoadingLabel.Content = "Loading " + HeaderTitleBarLabel.Content.ToString();
            
            RotateImage(false);
        }

        private void EndLoad()
        {
            RotateImage(true);

            CustomAnimations.FadeOut(LoadingLayout);
            LoadingLayout.Visibility = Visibility.Collapsed;
            
            CustomAnimations.FadeIn(CollateralLayout);
            CollateralLayout.Visibility = Visibility.Visible;
        }

        private void RotateImage(bool isStop)
        {
            if (!isStop)
            {
                storyBoard = new Storyboard();
                dblAnimation = new DoubleAnimation();
                rtransform = new RotateTransform();
                rtransform.Angle = 0;
                
                dblAnimation.From = 0;
                dblAnimation.To = 360;
                dblAnimation.RepeatBehavior = RepeatBehavior.Forever;

                LoadImage.RenderTransform = rtransform;

                Storyboard.SetTarget(dblAnimation, rtransform);
                Storyboard.SetTargetProperty(dblAnimation, new PropertyPath(RotateTransform.AngleProperty));
                storyBoard.Children.Add(dblAnimation);
                storyBoard.Begin();
            }
            else
            {
                if (storyBoard.Children.Count > 0)
                {
                    storyBoard.Stop();
                }
            }
        }

        public CollateralUtilizationSummaryDetailControl(string bizDate, string price, string utilizationTitle, bool canUserClose, bool startOfDay, bool isToday)
        {
            InitializeComponent();

            psClient = new PositionClient();
            psClient.CollateralizationUtilStartOfDayGetCompleted += psClient_CollateralizationUtilStartOfDayGetCompleted;
            psClient.CollateralizationUtilIntraDayGetCompleted += psClient_CollateralizationUtilIntraDayGetCompleted;
            
            
            HeaderTitleBarLabel.Content = utilizationTitle + " (" + ((startOfDay)?"SOD":"Intra")+")" + ((!isToday)?"-"+ bizDate : "");
            
            this.bizDate = bizDate;         
            this.canUserClose = canUserClose;
            this.startOfDay = startOfDay;
            this.Price = price;

            CloseHyperLink.IsEnabled = canUserClose;                    
        }

        void psClient_CollateralizationUtilIntraDayGetCompleted(object sender, CollateralizationUtilIntraDayGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }

            try
            {
                dtCollateral = Functions.ConvertToDataTable(e.Result, "Collateral");

                TotalAmounts();

                EndLoad();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            this.Cursor = Cursors.Arrow;   
        }

        void psClient_CollateralizationUtilStartOfDayGetCompleted(object sender, CollateralizationUtilStartOfDayGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }

            try
            {
                dtCollateral = Functions.ConvertToDataTable(e.Result, "Collateral");

                TotalAmounts();

                EndLoad();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            this.Cursor = Cursors.Arrow;   
        }
   
        private void TotalAmounts()
        {
            decimal totalMarginDebit = 0;
            decimal totalStockBorrow = 0;
            decimal totalDebitAmount = 0;
            decimal totalBankLoan = 0;
            decimal totalStockLoan = 0;
            decimal totalCreditAmount = 0;
            decimal totalMarginDebitNoPrice = 0;

            foreach (DataRow dr in dtCollateral.Rows)
            {
                MarginDebitTotalNoPrice = decimal.Parse(dr["MarginDebitTotalNoPrice"].ToString());
                totalMarginDebit += decimal.Parse(dr["MarginDebitBalance140Amt"].ToString());
                totalStockBorrow += decimal.Parse(dr["BorrowAmount"].ToString());
                totalBankLoan += decimal.Parse(dr["OCCAmt"].ToString()) + decimal.Parse(dr["HarrisBankAmt"].ToString()) + decimal.Parse(dr["USBankAmt"].ToString()) + decimal.Parse(dr["JPMBankAmt"].ToString()) + decimal.Parse(dr["BonyBankAmt"].ToString());
                totalStockLoan += decimal.Parse(dr["LoanAmount"].ToString());
            }

            totalDebitAmount = totalMarginDebit + totalStockBorrow;
            totalCreditAmount = totalStockLoan + totalBankLoan;

            MarginDebitNumericBox.Value = (double)totalMarginDebit;
            StockBorrowNumericBox.Value = (double)totalStockBorrow;
            TotalBankLoanAmount.Value = (double)totalBankLoan;
            StockLoanNumericBox.Value = (double)totalStockLoan;
            TotalDebitNumericBox.Value = (double)totalDebitAmount;
            TotalUtilAmount.Value = (double)totalCreditAmount;

            TotalPecentageNumericBox.Value = Functions.Percent(totalDebitAmount, totalCreditAmount) / 100;

            if (TotalPecentageNPriceNumericBox.Visibility == Visibility.Visible)
            {
                TotalPecentageNumericBox.Value = Functions.Percent(MarginDebitTotalNoPrice, totalDebitAmount) / 100;
                TotalPecentageNPriceNumericBox.Value = Functions.Percent(totalDebitAmount, totalCreditAmount) / 100;
            }
            else
            {
                TotalPecentageNumericBox.Value = Functions.Percent(totalDebitAmount, totalCreditAmount) / 100;
            }

            
            CustomAnimations.Shimmy(TotalPecentageNumericBox);
        }

        public DataTable CollateralTable
        {
            get
            {
                return dtCollateral;
            }
        }

        public string Price
        {
            get
            {
                return priceFilter;
            }

            set
            {
                priceFilter = value;

                if (priceFilter.Equals(""))
                {
                    TotalPecentageNPriceNumericBox.Visibility = Visibility.Collapsed;
                    HeaderUsedCollateralLabel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    TotalPecentageNPriceNumericBox.Visibility = Visibility.Visible;
                    HeaderUsedCollateralLabel.Visibility = Visibility.Visible;
                }


                BeginLoad();


                if (startOfDay && priceFilter.Equals(""))
                {
                    psClient.CollateralizationUtilStartOfDayGetAsync(bizDate);
                }
                else if (!priceFilter.Equals(""))
                {
                    psClient.CollateralizationUtilIntraDayGetAsync(bizDate, priceFilter);
                }
            }
        }

        public bool ApplyToChart
        {
            get
            {
                return (bool)CollateralToggleBUtton.IsChecked;
            }
        }

        public string HeaderTitle
        {
            get
            {
                return HeaderTitleLabel.Content.ToString();
            }
        }

        public string BizDate
        {
            set
            {
                bizDate = value;

                if (startOfDay && priceFilter.Equals(""))
                {
                    psClient.CollateralizationUtilStartOfDayGetAsync(bizDate);
                }
                else if (!priceFilter.Equals(""))
                {
                    psClient.CollateralizationUtilIntraDayGetAsync(bizDate, priceFilter);
                }
            }
        }

        private void RefreshHyperLink_Click(object sender, RoutedEventArgs e)
        {
            BeginLoad();

            if (startOfDay && priceFilter.Equals(""))
            {
                psClient.CollateralizationUtilStartOfDayGetAsync(bizDate);
            }            
            else if (!priceFilter.Equals(""))
            {
                psClient.CollateralizationUtilIntraDayGetAsync(bizDate, priceFilter);
            }
        }

        private void CollateralHyperLink_Click(object sender, RoutedEventArgs e)
        {
            if (priceFilter.Equals(""))
            {
                CustomEvents.UpdateCollateralDetailInformation("All", dtCollateral);
            }
            else
            {
                CustomEvents.UpdateCollateralDetailInformation(priceFilter, dtCollateral);
            }
        }

        private void CloseHyperLink_Click(object sender, RoutedEventArgs e)
        {
            if (canUserClose)
            {
                CustomAnimations.FadeOut(LayoutRoot);
                CustomEvents.UpdateRemoveControlInformation(this);
            }
        }

        private void CollateralToggleBUtton_Click(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdateCheckInformation();
        }
    }
}
