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
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void CollateralHyperLink_Click(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdatePageNavigation("/DashApplication;component/User Pages/PageCollateralUtilization.xaml");
        }

        private void SbreqHyperLink_Click(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdatePageNavigation("/DashApplication;component/User Pages/PageBorrowsMorning.xaml");
        }

        private void CollateralHyperLink_MouseEnter(object sender, MouseEventArgs e)
        {            
        }

    }
}
