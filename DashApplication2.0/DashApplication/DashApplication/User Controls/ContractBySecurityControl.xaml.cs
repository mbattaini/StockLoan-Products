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
    public partial class ContractBySecurityControl : UserControl
    {
        public PositionClient posCLient;

        public ContractBySecurityControl()
        {
            InitializeComponent();

            posCLient = new PositionClient();

            posCLient.BookGroupGetCompleted += new EventHandler<BookGroupGetCompletedEventArgs>(posCLient_BookGroupGetCompleted);
            posCLient.ContractSummaryBySecurityCompleted += new EventHandler<ContractSummaryBySecurityCompletedEventArgs>(posCLient_ContractSummaryBySecurityCompleted);
            posCLient.BookGroupGetAsync(UserInformation.UserId, "Reporting");
        }

        private void mouseHelperContractsGrid_MouseDoubleClick(object sender, MouseEventArgs eArgs)
        {
            ContractsChildWindowWrapper chldWndWrapper = new ContractsChildWindowWrapper(ContractsDatePicker.DateTime.ToString(),
                 ContractsGrid[ContractsGrid.SelectedIndex, 0].Text,
                 ContractsGrid[ContractsGrid.SelectedIndex, 1].Text,
                 "",
                 Locale.Domestic);
        }

        void posCLient_ContractSummaryBySecurityCompleted(object sender, ContractSummaryBySecurityCompletedEventArgs e)
        {
            ContractsGrid.IsLoading = false;

            if (e.Error != null)
            {
                return;
            }

            ContractsGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, "ContractSummary").DefaultView;
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

        private void ContractsDatePicker_DateTimeChanged(object sender, C1.Silverlight.DateTimeEditors.NullablePropertyChangedEventArgs<DateTime> e)
        {
            try
            {
                posCLient.ContractSummaryBySecurityAsync(ContractsDatePicker.DateTime.ToString(), BookGroupCombo.Text, Locale.Domestic, UserInformation.UserId, "Reporting");
                ContractsGrid.IsLoading = true;
            }
            catch { }   
        }

        private void BookGroupCombo_SelectedIndexChanged(object sender, PropertyChangedEventArgs<int> e)
        {
            try
            {
                posCLient.ContractSummaryBySecurityAsync(ContractsDatePicker.DateTime.ToString(), BookGroupCombo.Items[e.NewValue].ToString(), Locale.Domestic, UserInformation.UserId, "Reporting");
                ContractsGrid.IsLoading = true;
            }
            catch { }
        }

        private void BookGroupCombo_SelectedValueChanged(object sender, C1.Silverlight.PropertyChangedEventArgs<object> e)
        {
            // TODO: Add event handler implementation here.
        }

        private void ExcelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Export.Excel(ContractsGrid);
        }

        private void ContractsGrid_LoadedCellPresenter(object sender, C1.Silverlight.DataGrid.DataGridCellEventArgs e)
        {
            /*C1MouseHelper mh = C1MouseHelper(e.Cell.Presenter);
            e.Cell.Presenter.Tag = mh;
            mh.MouseDoubleClick += mh_MouseDoubleClick;*/


            C1MouseHelper mouseHelperCashCounterPartyGrid = new C1MouseHelper(e.Cell.Presenter);
            mouseHelperCashCounterPartyGrid.MouseDoubleClick += new MouseButtonEventHandler(mouseHelperContractsGrid_MouseDoubleClick);
        }
    }
}
