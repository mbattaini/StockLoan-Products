using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

using C1.Silverlight;
using C1.Silverlight.Data;
using C1.Silverlight.DataGrid;
using C1.Silverlight.DataGrid.Summaries;

using DashApplication.ServicePosition;
using DashApplication.CustomClasses;

namespace DashApplication
{
	public partial class CollateralUtilizationShellControl : UserControl
	{

        public PositionClient psClient;

        public CollateralUtilizationShellControl(DataTable dtCollateral)
		{
			// Required to initialize variables
			InitializeComponent();

    		DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[1], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
			DataGridAggregate.SetAggregateFunctions(CollateralGrid.Columns[2], new DataGridAggregatesCollection { new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("{0}")} });
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


            CollateralGrid.ItemsSource = dtCollateral.DefaultView;
        }
  
        private void CollateralGrid_AutoGeneratingColumn(object sender, C1.Silverlight.DataGrid.DataGridAutoGeneratingColumnEventArgs e)
        {
        	if (e.Property.Name == "MarginDebitBalance")
            {
                DataGridAggregate.SetAggregateFunctions(e.Column,
                    new DataGridAggregatesCollection
                    { 
                        new DataGridAggregateSum{ ResultTemplate = DataGridAggregate.GetDataTemplateFromString("SUM = {0}")}                        
                    });
            }
        }

        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            Export.Excel(CollateralGrid);
        }
	}
}