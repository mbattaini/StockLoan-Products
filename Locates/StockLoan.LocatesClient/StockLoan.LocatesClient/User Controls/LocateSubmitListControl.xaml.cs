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
	public partial class LocateSubmitListControl : UserControl
	{
        private LocatesServiceClient lsClient;

		public LocateSubmitListControl()
		{	
			InitializeComponent();

            lsClient = new LocatesServiceClient();

            lsClient.TradingGroupsGetCompleted += new EventHandler<TradingGroupsGetCompletedEventArgs>(lsClient_TradingGroupsGetCompleted);
            lsClient.LocateListSubmitCompleted += new EventHandler<LocateListSubmitCompletedEventArgs>(lsClient_LocateListSubmitCompleted);
            lsClient.TradingGroupsGetAsync("2012-02-01", 0);            
        }

        void lsClient_LocateListSubmitCompleted(object sender, LocateListSubmitCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            StatusLabel.Content = e.Result;
        }

        void lsClient_TradingGroupsGetCompleted(object sender, TradingGroupsGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTradingGroups = Functions.ConvertToDataTable(e.Result, "TradingGroups");

            foreach (DataRow drTemp in dtTradingGroups.Rows)
            {
                GroupCodeComboBox.Items.Add(drTemp["GroupCode"].ToString());
            }
        }

        private void SubmitListButton_Click(object sender, RoutedEventArgs e)
        {
            lsClient.LocateListSubmitAsync(UserInformation.UserId, GroupCodeComboBox.Text, "", ListTextBox.Text);
        }
	}
}