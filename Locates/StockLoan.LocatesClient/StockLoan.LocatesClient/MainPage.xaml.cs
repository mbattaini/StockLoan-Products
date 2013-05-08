using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace StockLoan_LocatesClient
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{			
			InitializeComponent();

            CustomEvents.UserInformationChanged += new EventHandler<UserEventArgs>(CustomEvents_UserInformationChanged);
			ClientWindow clntWindow = new ClientWindow();
			clntWindow.Content = new LoginControl();
			clntWindow.Show();
			clntWindow.CenterOnScreen();

            LocatesButton.Visibility = System.Windows.Visibility.Collapsed;
            SubmitListButton.Visibility = System.Windows.Visibility.Collapsed;
			TradingGroupsButton.Visibility = System.Windows.Visibility.Collapsed;
            MainDockControl.Visibility = System.Windows.Visibility.Collapsed;
        }

        void CustomEvents_UserInformationChanged(object sender, UserEventArgs e)
        {
            try
            {
                if (!e.LogOffUser)
                {
                    LocatesButton.Visibility = System.Windows.Visibility.Visible;
                    SubmitListButton.Visibility = System.Windows.Visibility.Visible;
                    TradingGroupsButton.Visibility = System.Windows.Visibility.Visible;
                    MainDockControl.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    LocatesButton.Visibility = System.Windows.Visibility.Collapsed;
                    SubmitListButton.Visibility = System.Windows.Visibility.Collapsed;
                    TradingGroupsButton.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            catch
            {
                LocatesButton.Visibility = System.Windows.Visibility.Collapsed;
                SubmitListButton.Visibility = System.Windows.Visibility.Collapsed;
                TradingGroupsButton.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

		private void LocatesButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ClientItem _item1 = new ClientItem();
            _item1.Load("Locates Grid", new LocatesGridControl());
            UpperMainDockTabControl.Items.Add(_item1.Item);
			
			ClientItem _item2 = new ClientItem();
            _item2.Load("Inventory Availability", new LocateAvailableInventoryControl());
            LowerLeftDockTabControl.Items.Add(_item2.Item);
			
			ClientItem _item3 = new ClientItem();
            _item3.Load("Locates Summary", new LocateSummaryControl());
            LowerMiddleDockTabControl.Items.Add(_item3.Item);												
			
			ClientItem _item4 = new ClientItem();
            _item4.Load("Group Code Summary", new LocateGroupCodeSummaryControl());
            LowerRightDockTabControl.Items.Add(_item4.Item);												
		
			ClientItem _item5 = new ClientItem();
			_item5.Load("Security Master", new SecurityMasterControl());			
			RightDockTabControl.Items.Add(_item5.Item);
			
			ClientItem _item6 = new ClientItem();
			_item6.Load("Inventory", new InventoryLookupControl());
			RightLowerDockTabControl.Items.Add(_item6.Item);
		}

		private void SubmitListButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ClientWindow clntWindow = new ClientWindow();
			clntWindow.Content = new LocateSubmitListControl();
			clntWindow.Show();
			clntWindow.CenterOnScreen();
		}

		private void TradingGroupsButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			ClientWindow clntWindow = new ClientWindow();
			clntWindow.Content = new LocateTradingGroupsControl();
			clntWindow.Show();
			clntWindow.CenterOnScreen();
		}
	}
}