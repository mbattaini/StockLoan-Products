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

using DashApplication.ServicePosition;

namespace DashApplication
{
    public partial class CashAndCollateralControl : UserControl
    {
        public PositionClient posCLient;
        public DataSet dsContractsDomestic;
        public DataSet dsContractsInternational;
		
        public CashAndCollateralControl()
        {
            InitializeComponent();

            posCLient = new PositionClient();

            posCLient.ContractsExcessCollateralSummaryLoadCompleted += new EventHandler<ContractsExcessCollateralSummaryLoadCompletedEventArgs>(posCLient_ContractsExcessCollateralSummaryLoadCompleted);
            posCLient.ContractSummaryByCashLoadCompleted += new EventHandler<ContractSummaryByCashLoadCompletedEventArgs>(posCLient_ContractSummaryByCashLoadCompleted);

            C1MouseHelper mouseHelperExcessCollateralGrid = new C1MouseHelper(ExcessCollateralGrid);
            mouseHelperExcessCollateralGrid.MouseDoubleClick += new MouseButtonEventHandler(mouseHelperExcessCollateralGrid_MouseDoubleClick);

            C1MouseHelper mouseHelperCashCounterPartyGrid = new C1MouseHelper(CounterPartyCashGrid);
            mouseHelperCashCounterPartyGrid.MouseDoubleClick += new MouseButtonEventHandler(mouseHelperCashCounterPartyGrid_MouseDoubleClick);

            ExcessCollateralLoad();
            CounterpartyCashLoad();
        }

        private void mouseHelperExcessCollateralGrid_MouseDoubleClick(object sender, MouseEventArgs eArgs)
        {
            ChildWindowWrapper chldWndWrapper = new ChildWindowWrapper(ContractsDatePicker.DateTime.ToString(),
                ExcessCollateralGrid[ExcessCollateralGrid.SelectedIndex, 0].Text,
                ExcessCollateralGrid[ExcessCollateralGrid.SelectedIndex, 1].Text,
                ExcessCollateralGrid[ExcessCollateralGrid.SelectedIndex, 2].Text,
                (((bool)RadioDomestic.IsChecked.Value) ? Locale.Domestic : Locale.International));
        }

        private void mouseHelperCashCounterPartyGrid_MouseDoubleClick(object sender, MouseEventArgs eArgs)
        {
            ChildWindowWrapper chldWndWrapper = new ChildWindowWrapper(ContractsDatePicker.DateTime.ToString(),
                    CounterPartyCashGrid[CounterPartyCashGrid.SelectedIndex, 0].Text,
                    CounterPartyCashGrid[CounterPartyCashGrid.SelectedIndex, 1].Text,
                    CounterPartyCashGrid[CounterPartyCashGrid.SelectedIndex, 4].Text,
                    (((bool)RadioDomestic.IsChecked.Value) ? Locale.Domestic : Locale.International));
        }

        private void posCLient_ContractsExcessCollateralSummaryLoadCompleted(object sender, ContractsExcessCollateralSummaryLoadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            ExcessCollateralGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, "ContractSummary").DefaultView;
        }

        private void posCLient_ContractSummaryByCashLoadCompleted(object sender, ContractSummaryByCashLoadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            CounterPartyCashGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, "ContractSummary").DefaultView;
        }

        private void ExcessCollateralLoad()
        {
            if ((bool)RadioDomestic.IsChecked.Value)
            {
                posCLient.ContractsExcessCollateralSummaryLoadAsync(ContractsDatePicker.DateTime.ToString(), "", Locale.Domestic, UserInformation.UserId, "Reporting");
            }
            else
            {
                posCLient.ContractsExcessCollateralSummaryLoadAsync(ContractsDatePicker.DateTime.ToString(), "", Locale.International, UserInformation.UserId, "Reporting");
            }
        }

        private void CounterpartyCashLoad()
        {
            if ((bool)RadioDomestic.IsChecked.Value)
            {
                posCLient.ContractSummaryByCashLoadAsync(ContractsDatePicker.DateTime.ToString(), "", Locale.Domestic, UserInformation.UserId, "Reporting");
            }
            else
            {
                posCLient.ContractSummaryByCashLoadAsync(ContractsDatePicker.DateTime.ToString(), "", Locale.International, UserInformation.UserId, "Reporting");
            }
        }

        private void ContractsDatePicker_DateTimeChanged(object sender, C1.Silverlight.DateTimeEditors.NullablePropertyChangedEventArgs<System.DateTime> e)
        {
            try
            {
                ExcessCollateralLoad();
                CounterpartyCashLoad();
            }
            catch { }
        }

        private void RadioInternational_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ExcessCollateralLoad();
                CounterpartyCashLoad();
            }
            catch { }
        }

        private void RadioDomestic_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ExcessCollateralLoad();
                CounterpartyCashLoad();
            }
            catch { }
        }


        private void CashExcelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Export.Excel(CounterPartyCashGrid);
        }

        private void ExcessExcelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Export.Excel(ExcessCollateralGrid);
        }
    }
}
