using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight.Data;
using C1.Silverlight.DataGrid;
using C1.Silverlight.DataGrid.Filters;
using C1.Silverlight.DataGrid.Summaries;
using C1.Silverlight;
using WorldWideClient.ServiceContracts;

namespace WorldWideClient
{
    public partial class PositionContractSummaryControl : UserControl
    {
        private ContractsServiceClient csClient = null;
        private DataTable dtContractSummary;
        private C1MouseHelper mouseHelper;
        private int controlHandle;


        public PositionContractSummaryControl()
        {
            InitializeComponent();

            controlHandle = Functions.CreateControlHandle();
            
            dtContractSummary = new DataTable();

            csClient = new ContractsServiceClient();
            csClient.ContractSummaryBySecurityGetCompleted += new EventHandler<ContractSummaryBySecurityGetCompletedEventArgs>(csClient_ContractSummaryBySecurityGetCompleted);

            ContractSummaryBookGroupToolBar.ShowBookCombo = false;
            ContractSummaryBookGroupToolBar.ShowBusinessDate = true;
            ContractSummaryBookGroupToolBar.ShowExportToCombo = true;
            ContractSummaryBookGroupToolBar.DataGrid = ContractsGrid;
            ContractSummaryBookGroupToolBar.ParentControlHandle = controlHandle;

            ContractSummaryBookGroupToolBar.FunctionName = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name;
            CustomEvents.ToolBarInformationChanged += new EventHandler<ToolBarChangeEventArgs>(CustomEvents_ToolBarInformationChanged);          
        }

        void CustomEvents_ToolBarInformationChanged(object sender, ToolBarChangeEventArgs e)
        {
            if ((!e.BizDate.Equals("") && !e.BookGroup.ToString().Equals("")) &&(controlHandle == e.ControlHandle))
            {
                csClient.ContractSummaryBySecurityGetAsync(e.BizDate, e.BookGroup, false, UserInformation.UserId, UserInformation.Password, System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name);
                ContractsGrid.IsLoading = true;
            }
        }

        void csClient_ContractSummaryBySecurityGetCompleted(object sender, ContractSummaryBySecurityGetCompletedEventArgs e)
        {
            ContractsGrid.IsLoading = false;

            if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
                return;
            }

            dtContractSummary = Functions.ConvertToDataTable(e.Result, "ContractSummary");

            ContractsGrid.ItemsSource = dtContractSummary.DefaultView;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ContractsGrid.TopRows.Clear();
            ContractsGrid.TopRows.Add(new DataGridFilterRow());
            ContractsGrid.Reload(false);
        }
  
        void mHelper_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CustomEvents.UpdateContractChangeInformation(
               ContractSummaryBookGroupToolBar.BusinessDate,
               ContractSummaryBookGroupToolBar.BookGroup,
                "",
               ContractsGrid[ContractsGrid.SelectedIndex, 0].Text,
               System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name,
               controlHandle);            
        }

        private void ContractsGrid_LoadedCellPresenter(object sender, C1.Silverlight.DataGrid.DataGridCellEventArgs e)
        {
        	C1MouseHelper mHelper = new C1MouseHelper(e.Cell.Presenter);
            mHelper.MouseDoubleClick += new MouseButtonEventHandler(mHelper_MouseDoubleClick);            
        }

        private void ContractsGrid_LoadedColumnHeaderPresenter(object sender, C1.Silverlight.DataGrid.DataGridColumnEventArgs e)
        {
            if ((e.Column.GetType()).IsNumeric())
            {
                DataGridAggregate.SetAggregateFunctions(e.Column, new DataGridAggregatesCollection { new DataGridAggregateSum() });
            }
        }
    }
}