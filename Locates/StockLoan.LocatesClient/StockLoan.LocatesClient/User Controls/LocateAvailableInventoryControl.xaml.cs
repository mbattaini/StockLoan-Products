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
	public partial class LocateAvailableInventoryControl : UserControl
	{
        private LocatesServiceClient lsClient;

		public LocateAvailableInventoryControl()
		{			
			InitializeComponent();

            lsClient = new LocatesServiceClient();
            lsClient.InventoryGetCompleted += new EventHandler<InventoryGetCompletedEventArgs>(lsClient_InventoryGetCompleted);
         	CustomEvents.CustomerChanged += new EventHandler<CustomerEventArgs>(CustomEvents_CustomerChanged);
			DataGridAggregate.SetAggregateFunctions(InventoryGrid.Columns[4], new DataGridAggregatesCollection { new DataGridAggregateSum { ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}") } });			       						
        }

        void CustomEvents_CustomerChanged(object sender, CustomerEventArgs e)
        {
            lsClient.InventoryGetAsync(e.GroupCode, e.SecId, 0);
            InventoryGrid.IsLoading = true;
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
	}
}