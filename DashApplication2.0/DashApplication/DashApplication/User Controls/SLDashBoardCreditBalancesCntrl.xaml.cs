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

namespace DashApplication
{
    public partial class SLDashBoardCreditBalancesCntrl : UserControl
    {
        private PositionClient psCleint = null;
        private DataTable dtBalances = null;

        public SLDashBoardCreditBalancesCntrl(string bizDate)
        {
            InitializeComponent();

            psCleint = new PositionClient();
            psCleint.BookCreditLBookBalancesGetCompleted += psCleint_BookCreditLBookBalancesGetCompleted;

            psCleint.BookCreditLBookBalancesGetAsync(bizDate, "0158");

            BalancesGrid.IsLoading = true;
        }

        void psCleint_BookCreditLBookBalancesGetCompleted(object sender, BookCreditLBookBalancesGetCompletedEventArgs e)
        {
            BalancesGrid.IsLoading = false;

            if (e.Error == null)
            {
                dtBalances = Functions.ConvertToDataTable(e.Result, "CreditBalances");
                BalancesGrid.ItemsSource = dtBalances.DefaultView;
            }
        }
    }
}
