using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight;

namespace WorldWideClient
{
	public partial class MainPage : UserControl
	{
		private UserIdControl _userIdControl = new UserIdControl();
        private ClientWindow _loginWindow = new ClientWindow();

		public MainPage()
		{			
			InitializeComponent();									
			MainDockControl.Visibility = Visibility.Collapsed;
			
			UserInformation.ResourceSet(this.Resources);
            CustomEvents.UserInformationChanged += new EventHandler<UserEventArgs>(UserInformation_UserInformationChanged);
            CustomEvents.ContractInformationChanged += new EventHandler<ContractChangeEventArgs>(CustomEvents_ContractInformationChanged);


            InventoryLookupButton.Visibility = Visibility.Collapsed;
            QuickTicketButton.Visibility = Visibility.Collapsed;
            SecurityMasterButton.Visibility = Visibility.Collapsed;
            PositionDeskInputButton.Visibility = Visibility.Collapsed;
            PositionBooksButton.Visibility = Visibility.Collapsed;
            PositionBoxSummaryButton.Visibility = Visibility.Collapsed;
            PositionContractSummaryButton.Visibility = Visibility.Collapsed;       
        }

        void CustomEvents_ContractInformationChanged(object sender, ContractChangeEventArgs e)
        {
            ClientItem _item = new ClientItem();

            _item.Load("Contract Information", new PositionContractItemsControl(e.BizDate, e.BookGroup, e.Book, e.SecId, e.FunctionPath), false);

            LowerMainDockTabControl.Items.Clear();
            LowerMainDockTabControl.Items.Add(_item.Item);

            if (LowerMainDockTabControl.Items.Count > 1)
                LowerMainDockTabControl.SelectedIndex = LowerMainDockTabControl.Items.Count - 1;
        }
		
		private void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            _loginWindow.Content = new LoginControl();
            _loginWindow.WindowHeader = "User Login";
            _loginWindow.Resize = false;
            _loginWindow.Show();
            _loginWindow.CenterOnScreen();			
		}

		private void InventoryLookupButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            ClientItem _item = new ClientItem();
            
            _item.Load("Inventory Lookup", new InventoryLookupControl());
            
            UpperLeftDockTabControl.Items.Add(_item.Item);
			
            if (UpperLeftDockTabControl.Items.Count > 1)
                UpperLeftDockTabControl.SelectedIndex = UpperLeftDockTabControl.Items.Count - 1;
			
		}

        void UserInformation_UserInformationChanged(object sender, UserEventArgs e)
        {
            if (!e.LogOffUser)
            {
                _loginWindow.Close();
                MainDockControl.Visibility = Visibility.Visible;

                LoginButton.Visibility = Visibility.Collapsed;
				
				InventoryLookupButton.Visibility = Visibility.Visible;
                QuickTicketButton.Visibility = Visibility.Visible;
                SecurityMasterButton.Visibility = Visibility.Visible;
                PositionDeskInputButton.Visibility = Visibility.Visible;
                PositionBooksButton.Visibility = Visibility.Visible;                
                PositionBoxSummaryButton.Visibility = Visibility.Visible;
				PositionContractSummaryButton.Visibility = Visibility.Visible;                          
			}
        }

        private void QuickTicketButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            /*ClientItem _item = new ClientItem();

            _item.Load("Quick Ticket", new TradingQuickTicketControl());

            UpperLeftDockTabControl.Items.Add(_item.Item);
         
            if (UpperLeftDockTabControl.Items.Count > 1)
                UpperLeftDockTabControl.SelectedIndex = UpperLeftDockTabControl.Items.Count - 1;*/
        }

        private void SecurityMasterButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ClientItem _item = new ClientItem();

            _item.Load("Security Master", new SecurityMasterControl());

            RightDockTabControl.Items.Add(_item.Item);

            if (RightDockTabControl.Items.Count > 1)
                RightDockTabControl.SelectedIndex = RightDockTabControl.Items.Count - 1;

        }

        private void PositionBooksButton_Click(object sender, RoutedEventArgs e)
        {
            ClientItem _item = new ClientItem();

            _item.Load("Position Books", new PositionBooksControl());

            UpperMainDockTabControl.Items.Add(_item.Item);

            if (UpperMainDockTabControl.Items.Count > 1)
                UpperMainDockTabControl.SelectedIndex = UpperMainDockTabControl.Items.Count - 1;
        }

        private void PositionDeskInputButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ClientItem _item = new ClientItem();

            _item.Load("Position Desk Input", new PositionDeskInputControl());

            UpperMainDockTabControl.Items.Add(_item.Item);

            if (UpperMainDockTabControl.Items.Count > 1)
                UpperMainDockTabControl.SelectedIndex = UpperMainDockTabControl.Items.Count - 1;
        }

        private void PositionBoxSummaryButton_Click(object sender, RoutedEventArgs e)
        {
            ClientItem _item = new ClientItem();

            _item.Load("Position Box Summary", new PositionBoxSummaryControl());

            UpperMainDockTabControl.Items.Add(_item.Item);

            if (UpperMainDockTabControl.Items.Count > 1)
                UpperMainDockTabControl.SelectedIndex = UpperMainDockTabControl.Items.Count - 1;
        }

        private void PositionContractSummaryButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	ClientItem _item = new ClientItem();

            _item.Load("Position Contract Summary", new PositionContractSummaryControl());

            UpperMainDockTabControl.Items.Add(_item.Item);

            if (UpperMainDockTabControl.Items.Count > 1)
                UpperMainDockTabControl.SelectedIndex = UpperMainDockTabControl.Items.Count - 1;
        }             
	}
}