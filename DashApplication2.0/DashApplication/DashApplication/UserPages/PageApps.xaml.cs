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
using System.Windows.Navigation;

using DashApplication.CustomClasses;

namespace DashApplication.UserPages
{
    public partial class PageApps : Page
    {
        public PageApps()
        {
            InitializeComponent();

            CustomEvents.UserIdChanged += CustomEvents_UserIdChanged;
            UpdateTiles();
        }

        void CustomEvents_UserIdChanged(object sender, UserIdEventArgs e)
        {
            UpdateTiles();
            
            if (UserInformation.UserId.Equals(""))
            {
                CustomEvents.UpdatePageNavigation("Available Applications", @"/DashApplication;component/UserPages/PageApps.xaml");
            }
        }

        private void UpdateTiles ()
        {
            CollateralHyperLink.Visibility = (UserInformation.ReturnRoleView("Collateral")?Visibility.Visible: Visibility.Collapsed);
            SbreqHyperLink.Visibility = (UserInformation.ReturnRoleView("StockLoan")?Visibility.Visible: Visibility.Collapsed);
            CollateralSummaryHuperLink.Visibility = (UserInformation.ReturnRoleView("Collateral")?Visibility.Visible: Visibility.Collapsed);
            Recall204HyperLink.Visibility = (UserInformation.ReturnRoleView("StockLoan")?Visibility.Visible: Visibility.Collapsed);
            ReportViewerHyperLink.Visibility = (UserInformation.ReturnRoleView("Reporting") ? Visibility.Visible : Visibility.Collapsed);
            PenaltyBoxHyperLink.Visibility = (UserInformation.ReturnRoleView("StockLoan") ? Visibility.Visible : Visibility.Collapsed);
            StockLoanDashboardHyperLink.Visibility = (UserInformation.ReturnRoleView("StockLoan") ? Visibility.Visible : Visibility.Collapsed);
            CashAndCollateralHyperLink.Visibility = (UserInformation.ReturnRoleView("Reporting") ? Visibility.Visible : Visibility.Collapsed);
            ContractsBySecurityHyperLink.Visibility = (UserInformation.ReturnRoleView("Reporting") ? Visibility.Visible : Visibility.Collapsed);
            HardToBorrowItemHyperLink.Visibility = (UserInformation.ReturnRoleView("StockLoan") ? Visibility.Visible : Visibility.Collapsed);
            Recall204SupplementHyperLink.Visibility = (UserInformation.ReturnRoleView("StockLoan") ? Visibility.Visible : Visibility.Collapsed);

            AppsGrid.InvalidateMeasure();         
        }
                
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!UserInformation.UserId.Equals(""))
            {
                UpdateTiles();      
            }
        }

        private void CollateralHyperLink_Click(object sender, RoutedEventArgs e)
        {            
            CustomEvents.UpdatePageNavigation("Collateral Utilization", @"/DashApplication;component/UserPages/PageCollateralUtilization.xaml");
        }

        private void SbreqHyperLink_Click(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdatePageNavigation("SBREQ Check", @"/DashApplication;component/UserPages/PageBorrowsMorning.xaml");
        }

        private void CollateralSummaryHuperLink_Click(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdatePageNavigation("Collateral Summary", @"/DashApplication;component/UserPages/PageCollateralSummary.xaml");
        }

        private void HyperLink_MouseEnter(object sender, MouseEventArgs e)
        {
            CustomAnimations.Shake(sender);
        }

        private void Recall204HyperLink_Click(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdatePageNavigation("204 Recall Test", @"/DashApplication;component/UserPages/PageRecallTrading.xaml");
        }

        private void ReportViewerHyperLink_Click(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdatePageNavigation("Report Viewer", @"/DashApplication;component/UserPages/PageReportViewer.xaml");
        }

        private void PenaltyBoxHyperLink_Click(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdatePageNavigation("Penalty Box", @"/DashApplication;component/UserPages/PagePenaltyBox.xaml");
        }

        private void StockLoanDashboardHyperLink_Click(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdatePageNavigation("StockLoan Dashboard", @"/DashApplication;component/UserPages/PageStockLoanDashboard.xaml");
        }

        private void CashAndCollateralHyperLink_Click(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdatePageNavigation("Cash And Collateral Reporting", @"/DashApplication;component/UserPages/PageCashAndCollateral.xaml");
        }

        private void ContractsBySecurityHyperLink_Click(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdatePageNavigation("Contracts By Security", @"/DashApplication;component/UserPages/PageContractsBySecurity.xaml");
        }

        private void HardToBorrowItemHyperLink_Click(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdatePageNavigation("HTB Item Add", @"/DashApplication;component/UserPages/PageHardToBorrowBillingItem.xaml");
        }

        private void Recall204SupplementHyperLink_Click(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdatePageNavigation("204 Recall Supplement Test", @"/DashApplication;component/UserPages/PageRcallTradingSupplement.xaml");
        }
    }
}
