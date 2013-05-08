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

using C1.Silverlight;
using C1.Silverlight.Data;
using C1.Silverlight.Chart;
using DashApplication.ServicePosition;

namespace DashApplication
{
    public partial class HardToBorrowBillingItemControl : UserControl
    {
        public PositionClient posClient = null;
        
        public HardToBorrowBillingItemControl()
        {
            InitializeComponent();

            posClient = new PositionClient();
            posClient.ShortSaleBillingBPSItemSetCompleted += new EventHandler<ShortSaleBillingBPSItemSetCompletedEventArgs>(posClient_ShortSaleBillingBPSItemSetCompleted);		
        }

        void posClient_ShortSaleBillingBPSItemSetCompleted(object sender, ShortSaleBillingBPSItemSetCompletedEventArgs e)
        {
            BillingGrid.IsLoading = false;

            if (e.Error != null)
            {
                StatusTextBox.Text = "Invalid paramaters.";
                return;
            }

            StatusTextBox.Text = "Success!";
            BillingGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, "Item").DefaultView;
        }

        private void SubmitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            posClient.ShortSaleBillingBPSItemSetAsync(
                BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd"),
                AccountNumberTextBox.Text,
                SecIdTextBox.Text,
                QuantityShortedNumericBox.Value.ToString(),
                QuantityCoveredNumericBox.Value.ToString(),
                SettleDatePicker.DateTime.Value.ToString("yyyy-MM-dd"),
                PriceNumericBox.Value.ToString("0.00"),
                RateNumericBox.Value.ToString("0.00"),
                UserInformation.UserId);
        }
    }
}
