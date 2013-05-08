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
using C1.Silverlight.Data;
using C1.Silverlight.DataGrid;
using C1.Silverlight.DataGrid.Filters;
using WorldWideClient.ServicePositions;

namespace WorldWideClient
{
	public partial class PositionDeskInputControl : UserControl
	{        
        private PositionsServiceClient positionsClient;

		public PositionDeskInputControl()
		{			
			InitializeComponent();

            DeskInputBookGroupToolBar.ShowBusinessDate = false;
			DeskInputBookGroupToolBar.ShowBookCombo = true;
            DeskInputBookGroupToolBar.FunctionName = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name;

			positionsClient = new PositionsServiceClient();
            positionsClient.BoxPositionLookupGetCompleted += new EventHandler<BoxPositionLookupGetCompletedEventArgs>(positionsClient_BoxPositionLookupGetCompleted);
        }        

        void positionsClient_BoxPositionLookupGetCompleted(object sender, BoxPositionLookupGetCompletedEventArgs e)
        {
            InputGrid.IsLoading = false;

            if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
                return;
            }

            DataTable dtBox = Functions.ConvertToDataTable(e.Result,"Box");

            InputGrid.ItemsSource = dtBox.DefaultView;
        }


		private void ParseListButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			
			
		}       
		
		private void contextMenu_ItemClick(object sender, SourcedEventArgs e)
		{
			switch(((C1MenuItem)e.Source).Header.ToString())
			{
				case ("Paste"):
					if (System.Windows.Clipboard.ContainsText())
						ListTextBox.Text= System.Windows.Clipboard.GetText();
						break;
					
				case ("Copy"):
					System.Windows.Clipboard.SetText(ListTextBox.Text);					
					break;
					
				case ("Cut"):
					System.Windows.Clipboard.SetText(ListTextBox.Text);
					ListTextBox.Text = "";
					break;				
			}			
		}

		private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{            
			InputGrid.TopRows.Clear();
			InputGrid.TopRows.Add(new DataGridFilterRow());			
			InputGrid.Reload(false);           
		}	
        
        private void InventorySubmitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
        }

        private void ProcessListButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	if (!ListTextBox.Text.Equals(""))
            {
				positionsClient.BoxPositionLookupGetAsync(ListTextBox.Text, DeskInputBookGroupToolBar.BookGroupSelected, true, UserInformation.UserId);
                InputGrid.IsLoading = true;
            }
			else
			{
				SystemEventWindow.Show("Unable to parse list.");
			}
        }

        private void BookTradesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	
        }

        private void Toggle_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((bool)ToggleInventory.IsChecked)
            {
                ToggleBorrow.IsChecked = false;
                ToggleLoan.IsChecked = false;
            }
            else if ((bool)ToggleBorrow.IsChecked)
            {
                ToggleInventory.IsChecked = false;
                ToggleLoan.IsChecked = false;
            }
            else if ((bool)ToggleLoan.IsChecked)
            {
                ToggleInventory.IsChecked = false;
                ToggleBorrow.IsChecked = false;
            }
        }
	}
}