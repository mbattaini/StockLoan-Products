using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight.Data;
using C1.Silverlight.Chart;



using WorldWideClient.ServiceInventory;
using WorldWideClient.ServiceUserAdmin;

namespace WorldWideClient
{
    public partial class InventoryLookupControl : UserControl
    {
        private InventoryServiceClient inventoryClient;
        private UserAdminServiceClient userAdminClient;

        public InventoryLookupControl()
        {
            InitializeComponent();

            InventoryBookGroupToolBar.ShowBusinessDate = false;
			InventoryBookGroupToolBar.ShowBookCombo = false;
            InventoryBookGroupToolBar.FunctionName = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name;
            
            inventoryClient = new ServiceInventory.InventoryServiceClient();
            inventoryClient.InventoryGetCompleted += new EventHandler<InventoryGetCompletedEventArgs>(inventoryClient_InventoryGetCompleted);
            inventoryClient.InventoryHistoryGetCompleted += new EventHandler<InventoryHistoryGetCompletedEventArgs>(inventoryClient_InventoryHistoryGetCompleted);
        }

        void inventoryClient_InventoryHistoryGetCompleted(object sender, InventoryHistoryGetCompletedEventArgs e)
        {
            InventoryGrid.IsLoading = false;

            if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
                return;
            }

			DataTable dtInventory = Functions.ConvertToDataTable(e.Result, "InventoryHistory");
			
			InventoryChart.BeginUpdate();
			
			
			XYDataSeries ds = new XYDataSeries();
			ds.XValueBinding = new Binding("BizDate");
			ds.ValueBinding = new Binding("Quantity");
			
			InventoryChart.Data.Children.Add(ds);
            InventoryChart.Data.ItemsSource = dtInventory.DefaultView;
			InventoryChart.ChartType = ChartType.Line;
			InventoryChart.EndUpdate();
		}

        void inventoryClient_InventoryGetCompleted(object sender, InventoryGetCompletedEventArgs e)
        {
            InventoryGrid.IsLoading = false;

            if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
                return;
			}
			
            InventoryGrid.ItemsSource = (Functions.ConvertToDataTable(e.Result, "Inventory").DefaultView);
		}

        private void LookupButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!InventoryBookGroupToolBar.BookGroupSelected.Equals(""))
            {
                inventoryClient.InventoryGetAsync(
                        DateTime.Now.ToString("yyyy-MM-dd"),
                        InventoryBookGroupToolBar.BookGroupSelected,
                        "",
                        SecurityIDTextBox.Text,
                        "",
                        "",
                        "",
                        UserInformation.UserId,
                        UserInformation.Password,
                        System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name);

                inventoryClient.InventoryHistoryGetAsync(
                        InventoryBookGroupToolBar.BookGroupSelected,
                        SecurityIDTextBox.Text,
                        UserInformation.UserId,
                        UserInformation.Password,
                        System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name);

                InventoryGrid.IsLoading = true;
                InventoryChart.Data.Children.Clear();
            }
            else
            {
                SystemEventWindow.Show("Please select a book group!");
            }
        }

        private void ExportToExcelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Export.Excel(InventoryGrid);
        }

        private void Grid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
        	InventoryGrid.Reload(false);
        }
    }
}