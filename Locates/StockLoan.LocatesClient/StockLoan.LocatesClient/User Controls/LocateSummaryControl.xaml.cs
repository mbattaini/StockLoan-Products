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
	public partial class LocateSummaryControl : UserControl
	{
        private LocatesServiceClient lsClient;
		public LocateSummaryControl()
		{
			// Required to initialize variables
			InitializeComponent();

            lsClient = new LocatesServiceClient();
            lsClient.LocateSummaryGetCompleted += new EventHandler<LocateSummaryGetCompletedEventArgs>(lsClient_LocateSummaryGetCompleted);

            CustomEvents.CustomerChanged += new EventHandler<CustomerEventArgs>(CustomEvents_CustomerChanged);
			
			DataGridAggregate.SetAggregateFunctions(LocateSummaryGrid.Columns[1], new DataGridAggregatesCollection { new DataGridAggregateSum { ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}") } });			       			
			DataGridAggregate.SetAggregateFunctions(LocateSummaryGrid.Columns[2], new DataGridAggregatesCollection { new DataGridAggregateSum { ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}") } });			       						
		}

        void CustomEvents_CustomerChanged(object sender, CustomerEventArgs e)
        {
            lsClient.LocateSummaryGetAsync(UserInformation.CurrentDate, e.SecId);
            LocateSummaryGrid.IsLoading = true;
        }

        void lsClient_LocateSummaryGetCompleted(object sender, LocateSummaryGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            LocateSummaryGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, 0).DefaultView;
            LocateSummaryGrid.IsLoading = false;
        }
	}
}