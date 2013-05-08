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
using C1.Silverlight.Data;
using C1.Silverlight;
using LoanStarWorldWideAdmin.SVR_InventoryService;

namespace LoanStarWorldWideAdmin.Views
{
    public partial class FirmMaint : Page
    {
        public InventoryServiceClient inventorySvc = new InventoryServiceClient();
        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();
		
		public FirmMaint()
        {
            InitializeComponent();
            functions.SetFunctionPath(this.Name.Trim());

            StatusLabel.Visibility = Visibility.Collapsed;

            inventorySvc.FirmsGetCompleted += new EventHandler<FirmsGetCompletedEventArgs>(inventorySvc_FirmsGetCompleted);
        }

        void inventorySvc_FirmsGetCompleted(object sender, FirmsGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "Firms");

            if (dtTemp == null)
            {
                StatusLabel.Content = "    Not authorized for Book Group " + appInfo.LoggedOnBookGroup;
                StatusLabel.Visibility = Visibility.Visible;
            }
            else
            {

                FirmGrid.ItemsSource = null;
                FirmGrid.IsLoading = true;

                FirmGrid.ItemsSource = dtTemp.DefaultView;
                FirmGrid.Visibility = Visibility.Visible;

                FirmGrid.IsLoading = false;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            appInfo.UserId = ((App)App.Current).UserId;
            appInfo.Password = ((App)App.Current).Password;
            appInfo.FunctionPath = ((App)App.Current).FunctionPath;
            appInfo.LoggedOnBookGroup = ((App)App.Current).LoggedOnBookGroup;

            LoadDetailGrid();        
        }

        private void LoadDetailGrid()
        {
            string userId = appInfo.UserId.ToString();
            string userPwd = appInfo.Password.ToString();
            string bookGroup = appInfo.LoggedOnBookGroup;
            string functionPath = appInfo.FunctionPath;

            if ((functionPath == null) || (functionPath.Equals("")))
            {
                functionPath = appInfo.DefaultFunctionPath;
                appInfo.FunctionPath = functionPath;
            }
            if ((bookGroup == null) || (bookGroup.Equals("")))
            {
                bookGroup = appInfo.DefaultBookGroup;
                appInfo.LoggedOnBookGroup = bookGroup;
            }

            inventorySvc.FirmsGetAsync("", userId, userPwd, appInfo.DefaultBookGroup, functionPath);

            C1MouseHelper mouseHelperSubscriptionGrid = new C1MouseHelper(FirmGrid);
            mouseHelperSubscriptionGrid.MouseDoubleClick += new MouseEventHandler(mouseHelperFirmGrid_MouseDoubleClick);
        }

        private void mouseHelperFirmGrid_MouseDoubleClick(object sender, MouseEventArgs eArgs)
        {
            string firmCode = FirmGrid[FirmGrid.SelectedIndex, 0].Text.ToString();
            string firmName = FirmGrid[FirmGrid.SelectedIndex, 1].Text.ToString();
            string isActive = FirmGrid[FirmGrid.SelectedIndex, 2].Text.ToString();

            ((App)App.Current).FirmCode = firmCode;
            ((App)App.Current).FirmName = firmName;
            ((App)App.Current).IsActive = isActive;

            C1Window win = new C1Window();

            win.CenterOnScreen();
            win.Content = new FirmMaintAddWindow();

            win.ShowModal();
            win.Closed += new EventHandler(bookGroupWin_Closed);
        }

        void bookGroupWin_Closed(object sender, EventArgs e)
        {
            LoadDetailGrid();
        }

        
        private void FirmGrid_SelectionChanged(object sender, C1.Silverlight.DataGrid.DataGridSelectionChangedEventArgs e)
        {
            try
            {
            }
            catch { }
        }

        private void FirmMaintenance_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}




