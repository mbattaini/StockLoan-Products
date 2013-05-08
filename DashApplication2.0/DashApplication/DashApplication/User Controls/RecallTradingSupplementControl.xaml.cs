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
using C1.Silverlight.DataGrid;
using C1.Silverlight.Chart;
using DashApplication.ServicePosition;

namespace DashApplication
{
    public partial class RecallTradingSupplementControl : UserControl
    {

        private PositionClient posClient;

        public RecallTradingSupplementControl()
        {
            InitializeComponent();

            posClient = new PositionClient();

            posClient.BookGroupGetCompleted += new EventHandler<BookGroupGetCompletedEventArgs>(posCLient_BookGroupGetCompleted);
            posClient.BookGroupGetAsync(UserInformation.UserId, "Reporting");

            posClient.RecallTradingSupplementGetCompleted += posClient_RecallTradingSupplementGetCompleted;
        }

        void posClient_RecallTradingSupplementGetCompleted(object sender, RecallTradingSupplementGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            InfoGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, "RecallTradingSupplement").DefaultView;

            if (InfoGrid.Rows.Count > 0)
            {
                InfoGrid.SelectedIndex = 1;
            }

            InfoGrid.IsLoading = false;
        }

        private void ExcelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
          
        }



        void posCLient_BookGroupGetCompleted(object sender, BookGroupGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtBookGroups = Functions.ConvertToDataTable(e.Result, "BookGroup");

            foreach (DataRow dr in dtBookGroups.Rows)
            {
                BookGroupCombo.Items.Add(dr["BookGroup"].ToString());
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!BookGroupCombo.Text.Equals("") && !BizDatePicker.DateTime.Value.ToString().Equals("") && !BizDatePickerPrior.DateTime.Value.ToString().Equals(""))
            {
                posClient.RecallTradingSupplementGetAsync(BizDatePickerPrior.DateTime.Value.ToString("yyyy-MM-dd"), BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd"), BookGroupCombo.Text);
                InfoGrid.IsLoading = true;
            }
        }

        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            Export.Excel(InfoGrid);
        }            
    }
}
