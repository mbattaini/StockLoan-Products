using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using StockLoan_LocatesClient.ServiceLocates;

namespace StockLoan_LocatesClient
{
	public partial class LocateTradingGroupsControl : UserControl
	{
		private LocatesServiceClient lsClient;
		
		public LocateTradingGroupsControl()
		{			
			InitializeComponent();

            lsClient = new LocatesServiceClient();
            
            lsClient.TradingGroupsGetCompleted += new EventHandler<TradingGroupsGetCompletedEventArgs>(lsClient_TradingGroupsGetCompleted);

            lsClient.TradingGroupsGetAsync("", 0);            
        }

        void lsClient_TradingGroupsGetCompleted(object sender, TradingGroupsGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            TradingGroupsGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, 0).DefaultView;
        }
	}
}