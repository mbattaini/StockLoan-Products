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
using C1.Silverlight.DataGrid;
using C1.Silverlight.DataGrid.Summaries;

using DashApplication.ServicePosition;

namespace DashApplication
{
	public partial class CollateralUtilizationDetailControl : UserControl
	{
        public PositionClient psClient;

		public CollateralUtilizationDetailControl(string bizDate, string classGroup)
		{
			// Required to initialize variables
			InitializeComponent();

            psClient = new PositionClient();
            psClient.CollateralizationUtilDetailGetCompleted += new EventHandler<CollateralizationUtilDetailGetCompletedEventArgs>(psClient_CollateralizationUtilDetailGetCompleted);
            						
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[3], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[4], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[5], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[6], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[7], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[8], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[9], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[10], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[11], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[12], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[13], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[14], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[15], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[16], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[17], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });								
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[18], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });								
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[19], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });								
			
			psClient.CollateralizationUtilDetailGetAsync(bizDate, classGroup);
            
            CollateralGrid.IsLoading = true;
		}

        void psClient_CollateralizationUtilDetailGetCompleted(object sender, CollateralizationUtilDetailGetCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                CollateralGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, "CollateralDetail").DefaultView;
            }

            CollateralGrid.IsLoading = false;
        }

        private void ExportToExcelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Export.Excel(CollateralGrid);
        }
	}
}