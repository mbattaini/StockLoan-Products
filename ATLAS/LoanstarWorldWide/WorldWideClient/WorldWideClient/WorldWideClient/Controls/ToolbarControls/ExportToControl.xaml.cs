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

namespace WorldWideClient
{
	public partial class ExportToControl : UserControl
	{
		public static C1DataGrid _dataGrid = null;				
		
		public ExportToControl()
		{
			// Required to initialize variables
			InitializeComponent();			
		}			

		public void ApplyGrid(C1DataGrid _grid)
		{
			_dataGrid = _grid;
		}
		
		private void ExportToExcelButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (_dataGrid == null)
            {
                SystemEventWindow.Show("DataGrid attribute not set..sorry yell at David Chen!");
                return;
            }
            else
            {
                Export.Excel(_dataGrid);
            }
		}

		
		private void ExportToPdfButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (_dataGrid == null)
			{
				SystemEventWindow.Show("DataGrid attribute not set..sorry yell at David Chen!");
				return;
			}
			else
			{
				Export.Pdf(_dataGrid);
			}
		}

		private void ExportToEmailButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (_dataGrid == null)
			{
				SystemEventWindow.Show("DataGrid attribute not set..sorry yell at David Chen!");
				return;
			}
		}
	}
}