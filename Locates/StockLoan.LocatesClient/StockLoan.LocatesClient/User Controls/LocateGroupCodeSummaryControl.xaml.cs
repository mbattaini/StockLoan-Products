using System;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

using C1.Silverlight.DataGrid.Summaries;
using StockLoan_LocatesClient.ServiceLocates;

namespace StockLoan_LocatesClient
{
	public partial class LocateGroupCodeSummaryControl : UserControl
	{
		private LocatesServiceClient lsClient;
        private DispatcherTimer dTimer;
        private string currentDate = "";

        public LocateGroupCodeSummaryControl()
        {            
            InitializeComponent();

            lsClient = new LocatesServiceClient();
            lsClient.LocateGroupCodeSummaryGetCompleted += new EventHandler<LocateGroupCodeSummaryGetCompletedEventArgs>(lsClient_LocateGroupCodeSummaryGetCompleted);

			DataGridAggregate.SetAggregateFunctions(GroupCodeGrid.Columns[1], new DataGridAggregatesCollection { new DataGridAggregateSum { ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}") } });			       			
			DataGridAggregate.SetAggregateFunctions(GroupCodeGrid.Columns[2], new DataGridAggregatesCollection { new DataGridAggregateSum { ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}") } });			       			
			
            currentDate = UserInformation.CurrentDate;

            if (!UserInformation.CurrentDate.Equals(""))
            {
                lsClient.LocateGroupCodeSummaryGetAsync(UserInformation.CurrentDate);
                GroupCodeGrid.IsLoading = true;
            }

            dTimer = new DispatcherTimer();
            dTimer.Tick += new EventHandler(dTimer_Tick);
            dTimer.Interval = new TimeSpan(0, 0, 5);
            dTimer.Start();
        }

        void dTimer_Tick(object sender, EventArgs e)
        {

            if (!currentDate.Equals(UserInformation.CurrentDate))
            {
                if (!UserInformation.CurrentDate.Equals(""))
                {
                    lsClient.LocateGroupCodeSummaryGetAsync(UserInformation.CurrentDate);
                    GroupCodeGrid.IsLoading = true;
                }

                currentDate = UserInformation.CurrentDate;
            }
        }

        void lsClient_LocateGroupCodeSummaryGetCompleted(object sender, LocateGroupCodeSummaryGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            GroupCodeGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, 0).DefaultView;
            GroupCodeGrid.IsLoading = false;
            GroupCodeGrid.SelectedIndex = 1;
        }

        private void GroupCodeGrid_SelectionChanged(object sender, C1.Silverlight.DataGrid.DataGridSelectionChangedEventArgs e)
        {            
            UserInformation.GlobalFilterGroupCode = GroupCodeGrid[GroupCodeGrid.SelectedIndex, 0].Text;                
        }
	}
}