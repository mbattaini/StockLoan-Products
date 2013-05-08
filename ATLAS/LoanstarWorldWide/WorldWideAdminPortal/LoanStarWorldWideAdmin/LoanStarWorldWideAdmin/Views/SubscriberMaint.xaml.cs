using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

using C1.Silverlight;
using C1.Silverlight.Data;
using C1.Silverlight.Chart;

namespace LoanStarWorldwideAdmin.Views
{
    public partial class SubscriptionMaint : Page
    {
    //		public PositionClient posCLient;
		
        public SubscriptionMaint()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //posCLient = new PositionClient();

            //posCLient.InventoryHistoryGetCompleted += new EventHandler<InventoryHistoryGetCompletedEventArgs>(posCLient_InventoryHistoryGetCompleted);
            //posCLient.InventoryHistorySummaryGetCompleted += new EventHandler<InventoryHistorySummaryGetCompletedEventArgs>(posCLient_InventoryHistorySummaryGetCompleted);
            //posCLient.InventoryRateGetCompleted += new EventHandler<InventoryRateGetCompletedEventArgs>(posCLient_InventoryRateGetCompleted);

			
			C1MouseHelper mouseHelperInventoryRateHistory = new C1MouseHelper(RateLabel);
			mouseHelperInventoryRateHistory.MouseDoubleClick +=new MouseEventHandler(mouseHelperInventoryRateHistory_MouseDoubleClick);
        }

		private void mouseHelperInventoryRateHistory_MouseDoubleClick(object sender, MouseEventArgs eArgs)
        {
            if (!SecurityIdTextBox.Text.Equals(""))
            {
                //InventoryChildWindowWrapper chldWndWrapper = new InventoryChildWindowWrapper(SecurityIdTextBox.Text);
            }
        }
		
        //void posCLient_InventoryRateGetCompleted(object sender, InventoryRateGetCompletedEventArgs e)
        //{
        //    if (e.Error != null)
        //    {
        //        return;
        //    }

        //    RateTextBox.Text = e.Result.ToString();
        //}

        //void posCLient_InventoryHistorySummaryGetCompleted(object sender, InventoryHistorySummaryGetCompletedEventArgs e)
        //{
        //    if (e.Error != null)
        //    {
        //        return;
        //    }
			
        //    InventoryChart.Reset(true);
        //    Axis valueAxis = InventoryChart.View.AxisY;
        //    Axis labelAxis = InventoryChart.View.AxisX;

            
        //    labelAxis.AutoMin = true;
        //    labelAxis.ItemsValueBinding = new System.Windows.Data.Binding("BizDate");
        //    labelAxis.ItemsSource = Functions.ConvertToDataTable(e.Result, "InventorySummary").Columns["BizDate"].ToString();
        //    labelAxis.AnnoFormat = "yyyy-MM-dd";
        //    labelAxis.AnnoAngle = 45;

        //    valueAxis.AutoMin = false;
        //    valueAxis.ItemsValueBinding = new System.Windows.Data.Binding("Quantity");
        //    valueAxis.Min = 0;
            
			
        //    InventoryChart.Data.ItemsSource = Functions.ConvertToDataTable(e.Result, "InventorySummary").DefaultView;	
        //    InventoryChart.Data.ItemNameBinding = new System.Windows.Data.Binding("Quantity");

        //    var ds = new DataSeries();
        //    ds.Label = "Quantity";
        //    ds.ValueBinding = new System.Windows.Data.Binding("Quantity");
        //    InventoryChart.Data.Children.Add(ds);    
        //}

        //void posCLient_InventoryHistoryGetCompleted(object sender, InventoryHistoryGetCompletedEventArgs e)
        //{
        //    if (e.Error != null)
        //    {
        //        return;
        //    }

			
        //    InventoryGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, "Inventory").DefaultView;	        						  
			
			
        //}

        private void LookupButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!SecurityIdTextBox.Text.Equals(""))
            {
                //posCLient.InventoryHistoryGetAsync(SecurityIdTextBox.Text);
                //posCLient.InventoryHistorySummaryGetAsync(SecurityIdTextBox.Text);
                //posCLient.InventoryRateGetAsync(SecurityIdTextBox.Text);
            }			
        }
    
    }
}
