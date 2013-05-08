using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight.DataGrid.Summaries;
using StockLoan_LocatesClient.ServiceLocates;



namespace StockLoan_LocatesClient
{
	public partial class InventoryLookupControl : UserControl
	{
        private LocatesServiceClient lsClient;

		public InventoryLookupControl()
		{
			// Required to initialize variables
			InitializeComponent();
            lsClient = new LocatesServiceClient();

            lsClient.InventoryGetCompleted += new EventHandler<InventoryGetCompletedEventArgs>(lsClient_InventoryGetCompleted);
			DataGridAggregate.SetAggregateFunctions(InventoryGrid.Columns[4], new DataGridAggregatesCollection { new DataGridAggregateSum { ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}") } });			       			
		}

        void lsClient_InventoryGetCompleted(object sender, InventoryGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            InventoryGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, 0).DefaultView;

            InventoryGrid.IsLoading = false;
        }

        private void FindToolbarButton_Click(object sender, RoutedEventArgs e)
        {
            lsClient.InventoryGetAsync("", SecurityIdTextBox.Text, 0);
            InventoryGrid.IsLoading = true;
        }
	}
}