using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight;
using C1.Silverlight.Imaging;
using C1.Silverlight.TileView;

using DashApplication.ServicePosition;

namespace DashApplication
{
    public partial class StockLoanDashboardControl : UserControl
    {
        public StockLoanDashboardControl()
        {
            InitializeComponent();

         
        }

        private void TabItemAdd(string header, UIElement item)
        {
            C1TabItem _tabItem = new C1TabItem();
            _tabItem.Content = item;
            _tabItem.Header = header;
            CtrlTabControl.Items.Add(_tabItem);
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            TabItemAdd("Stock Borrow Balances", new SLDashboardBalancesCntrl(BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd"), CustomTypes.ContractTypes.Borrows, "Stock Borrow Balances"));
            TabItemAdd("Stock Loan Balances", new SLDashboardBalancesCntrl(BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd"), CustomTypes.ContractTypes.Loans, "Stock Loan Balances"));
            TabItemAdd("Credit Balances", new SLDashBoardCreditBalancesCntrl(BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd")));

        }        
    }
}
