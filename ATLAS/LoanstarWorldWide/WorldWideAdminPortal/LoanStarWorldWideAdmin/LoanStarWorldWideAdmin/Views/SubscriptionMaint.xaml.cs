using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using System.Windows;
using System.Windows.Data;
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
using LoanStarWorldWideAdmin.SVR_BooksService;
using LoanStarWorldWideAdmin.SVR_InventoryService;
using LoanStarWorldWideAdmin.SVR_AdminService;

namespace LoanStarWorldWideAdmin.Views
{
    public partial class SubscriptionMaint : Page
    {
    	public BooksServiceClient booksSvc = new BooksServiceClient();
        public InventoryServiceClient inventorySvc = new InventoryServiceClient();
        public AdminServiceClient adminSvc = new AdminServiceClient();

        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();

        private string _EditSubscription;

        private bool _DisableFeed;

        public bool DisableFeed
        {
            get { return _DisableFeed; }
            set { _DisableFeed = value; }
        }

        public string EditSubscription
        {
            get { return _EditSubscription; }
            set { _EditSubscription = value; }
        }

        public SubscriptionMaint()
        {
            InitializeComponent();
            try
            {
                functions.SetFunctionPath(this.Name.Trim());

                booksSvc.BookGroupsGetAllCompleted += new EventHandler<BookGroupsGetAllCompletedEventArgs>(booksSvc_BookGroupsGetAllCompleted);
        
                inventorySvc.InventorySubscriptionsGetCompleted += new EventHandler<InventorySubscriptionsGetCompletedEventArgs>(inventorySvc_InventorySubscriptionsGetCompleted);
                //inventorySvc.FirmsGetCompleted += new EventHandler<FirmsGetCompletedEventArgs>(inventorySvc_FirmsGetCompleted);
                //inventorySvc.DeskTypesGetCompleted += new EventHandler<DeskTypesGetCompletedEventArgs>(inventorySvc_DeskTypesGetCompleted);
                inventorySvc.InventorySubscriptionSetCompleted += new EventHandler<InventorySubscriptionSetCompletedEventArgs>(inventorySvc_InventorySubscriptionSetCompleted);

                //adminSvc.CountriesGetCompleted += new EventHandler<CountriesGetCompletedEventArgs>(adminSvc_CountriesGetCompleted);
                DisableFeed = false;

                StatusLabel.Visibility = Visibility.Collapsed;

                appInfo.UserId = ((App)App.Current).UserId;
                appInfo.Password = ((App)App.Current).Password;
                appInfo.FunctionPath = ((App)App.Current).FunctionPath;
                appInfo.LoggedOnBookGroup = ((App)App.Current).LoggedOnBookGroup;

                string userId = appInfo.UserId.ToString();
                string userPwd = appInfo.Password.ToString();
                string bookGroup = appInfo.LoggedOnBookGroup;
                string functionPath = appInfo.FunctionPath;

                if ((bookGroup == null) || (bookGroup.Equals("")))
                {
                    bookGroup = appInfo.DefaultBookGroup;
                    appInfo.LoggedOnBookGroup = bookGroup;
                }

                if ((functionPath == null) || (functionPath.Equals("")))
                {
                    functionPath = appInfo.DefaultFunctionPath;
                    appInfo.FunctionPath = functionPath;
                }

                booksSvc.BookGroupsGetAllAsync(bookGroup, "", userId, userPwd, functionPath);

                C1MouseHelper mouseHelperSubscriptionGrid = new C1MouseHelper(SubscriptionGrid);
                mouseHelperSubscriptionGrid.MouseDoubleClick += new MouseEventHandler(mouseHelperSubscriptionGrid_MouseDoubleClick);
            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
        }

        void inventorySvc_InventorySubscriptionSetCompleted(object sender, InventorySubscriptionSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            if (e.Result)
            {
                ChangeBookGroupSelection();
                if (DisableFeed == true)
                { this.Visibility = Visibility.Collapsed; }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void mouseHelperSubscriptionGrid_MouseDoubleClick(object sender, MouseEventArgs eArgs)
        {
            string bookGroupGet = BookGroupCombo.Text;
            appInfo.SelectedBookGroup = bookGroupGet;
            ((App)App.Current).SelectedBookGroup = bookGroupGet;
            string desk = SubscriptionGrid[SubscriptionGrid.SelectedIndex, 1].Text.ToString();
            ((App)App.Current).CurrentDesk = desk;
            string userId = appInfo.UserId.ToString();
            string userPwd = appInfo.Password.ToString();
            string bookGroup = appInfo.LoggedOnBookGroup;
            string functionPath = appInfo.FunctionPath;
            string inventoryType = appInfo.InventoryType;

            if ((inventoryType == null) || (inventoryType.Equals("")))
            {
                inventoryType = SubscriptionGrid[SubscriptionGrid.SelectedIndex, 0].Text.ToString();
                appInfo.InventoryType = inventoryType;
            }

            if ((bookGroup == null) || (bookGroup.Equals("")))
            {
                bookGroup = appInfo.DefaultBookGroup;
                appInfo.LoggedOnBookGroup = bookGroup;
            }

            if ((functionPath == null) || (functionPath.Equals("")))
            {
                functionPath = appInfo.DefaultFunctionPath;
                appInfo.FunctionPath = functionPath;
            }

            EditSubscription = "UPDATE";

            SubscriptionGrid.ItemsSource = null;

            inventorySvc.InventorySubscriptionsGetAsync(bookGroupGet, desk, inventoryType, 0, bookGroup, userId, userPwd, functionPath);

        }

        void inventorySvc_InventorySubscriptionsGetCompleted(object sender, InventorySubscriptionsGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            SubscriptionGrid.ItemsSource = null;
            SubscriptionGrid.IsLoading = true;
            DataTable dtTemp = new DataTable();

            appInfo.SelectedBookGroup = BookGroupCombo.SelectedItem.ToString().Trim().ToUpper();
            ((App)App.Current).SelectedBookGroup = appInfo.SelectedBookGroup;
            dtTemp = Functions.ConvertToDataTable(e.Result, "InventorySubscriptions");

            if (dtTemp == null)
            {

            }
            else
            {
                SubscriptionGrid.ItemsSource = dtTemp.DefaultView;
                //SubscriptionGrid.Visibility = Visibility.Collapsed;
                string temp = "";

                foreach (DataRow dr in dtTemp.Rows)
                {
                    temp = dr["Desk"].ToString();
                    ((App)App.Current).CurrentDesk = temp;

                    temp = dr["FileName"].ToString();
                    ((App)App.Current).FileName = temp;

                    if (dr["FileHost"] == null)
                    { }
                    else
                    {
                        temp = dr["FileHost"].ToString();
                        ((App)App.Current).FileHost = temp;
                    }
                    if (dr["FileUserId"] == null)
                    { }
                    else
                    {
                        temp = dr["FileUserId"].ToString();
                        ((App)App.Current).FileUser = temp;
                    }
                    if (dr["FilePassword"] == null)
                    { }
                    else
                    {
                        temp = dr["FilePassword"].ToString();
                        ((App)App.Current).FilePWD = temp;
                    }
                    if (dr["MailAddress"] == null)
                    { }
                    else
                    {
                        temp = dr["MailAddress"].ToString();
                        ((App)App.Current).EMailAddress = temp;
                    }
                    if (dr["MailSubject"] == null)
                    { }
                    else
                    {
                        temp = dr["MailSubject"].ToString();
                        ((App)App.Current).EMailSubject = temp;
                    }
                    if (dr["Comment"] == null)
                    { }
                    else
                    {
                        temp = dr["Comment"].ToString();
                        ((App)App.Current).SubscribeComment = temp;
                    }
                    if (dr["InventoryType"] == null)
                    { }
                    else
                    {
                        temp = dr["InventoryType"].ToString();
                        ((App)App.Current).InventoryType = temp;
                    }
                    ((App)App.Current).Disable = "false";
                
                }
            }

            SubscriptionGrid.IsLoading = false;
            if (EditSubscription.ToUpper().Equals(""))
            {
                SubscriptionGrid.Visibility = Visibility.Visible;
            }
            else
            {
                LoadMaintWindow();
            }

        }

        void booksSvc_BookGroupsGetAllCompleted(object sender, BookGroupsGetAllCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
			dtTemp = Functions.ConvertToDataTable(e.Result, "BookGroups");

            if (dtTemp == null)
            {

            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["BookGroup"].ToString();
                    this.BookGroupCombo.Items.Add(temp);
                }
                this.BookGroupCombo.SelectedItem = appInfo.SelectedBookGroup;
                ((App)App.Current).SelectedBookGroup = appInfo.SelectedBookGroup;
    			BookGroupCombo.SelectedIndex = 0;
			}
        }

        private void BookGroupCombo_SelectedIndexChanged(object sender, PropertyChangedEventArgs<int> e)
        {
            ChangeBookGroupSelection();
        }

        private void ChangeBookGroupSelection()
        {
            string bookGroupGet = BookGroupCombo.SelectedItem.ToString().Trim().ToUpper();
            appInfo.SelectedBookGroup = bookGroupGet;
            ((App)App.Current).SelectedBookGroup = bookGroupGet;
            string userId = appInfo.UserId.ToString();
            string userPwd = appInfo.Password.ToString();
            string bookGroup = appInfo.LoggedOnBookGroup;
            string functionPath = appInfo.FunctionPath;
            ((App)App.Current).SelectedBookGroup = bookGroupGet;

            if ((bookGroup == null) || (bookGroup.Equals("")))
            {
                bookGroup = appInfo.DefaultBookGroup;
                appInfo.LoggedOnBookGroup = bookGroup;
            }

            if ((functionPath == null) || (functionPath.Equals("")))
            {
                functionPath = appInfo.DefaultFunctionPath;
                appInfo.FunctionPath = functionPath;
            }
            EditSubscription = "";
            inventorySvc.InventorySubscriptionsGetAsync(bookGroupGet, "", "", 0, bookGroup, userId, userPwd, functionPath);

        }

        private void SubscriptionGrid_SelectionChanged(object sender, C1.Silverlight.DataGrid.DataGridSelectionChangedEventArgs e)
        {
            try
            {
               
                string bookGroup = BookGroupCombo.SelectedItem.ToString().Trim().ToUpper();
                appInfo.SelectedBookGroup = bookGroup;
                ((App)App.Current).SelectedBookGroup = bookGroup;
                appInfo.CurrentDesk = SubscriptionGrid[SubscriptionGrid.SelectedIndex, 1].Text;
                ((App)App.Current).CurrentDesk = appInfo.CurrentDesk;
                appInfo.InventoryType = SubscriptionGrid[SubscriptionGrid.SelectedIndex, 0].Text;
            }
            catch { }
        }

        private void addSubscriptionButton_Click(object sender, RoutedEventArgs e)
        {

            EditSubscription = "ADD";
            LoadMaintWindow();

            appInfo.SelectedBookGroup = BookGroupCombo.Text.Trim();
            ((App)App.Current).SelectedBookGroup = BookGroupCombo.Text.Trim();

        }

        void LoadMaintWindow()
        {
            ((App)App.Current).EditSubscription = EditSubscription;
            ((App)App.Current).InventoryType = appInfo.InventoryType;

            C1Window win = new C1Window();

            win.CenterOnScreen();
            win.Content = new SubscriptionMaintWindow();

            win.Show();
            win.Closed += new EventHandler(bookGroupWin_Closed);
        }

        void bookGroupWin_Closed(object sender, EventArgs e)
        {
            ChangeBookGroupSelection();
        }

        private void BookGroupCombo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void SubscriptionMaintenance_LostFocus(object sender, RoutedEventArgs e)
        {
            //this.Visibility = Visibility.Collapsed;
        }

        private void fileLayoutsButton_Click(object sender, RoutedEventArgs e)
        {

            if (SubscriptionGrid.Selection.SelectedRows.Count >= 1)
            {
                ((App)App.Current).UserId = appInfo.UserId;
                ((App)App.Current).Password = appInfo.Password;
                ((App)App.Current).FunctionPath = appInfo.FunctionPath;
                ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;
                ((App)App.Current).SelectedBookGroup = BookGroupCombo.Text;
                ((App)App.Current).InventoryType = SubscriptionGrid[SubscriptionGrid.SelectedIndex, 0].Text.ToString();
                ((App)App.Current).EditSubscription = "UPDATE";
                string desk = SubscriptionGrid[SubscriptionGrid.SelectedIndex, 1].Text.ToString();
                ((App)App.Current).CurrentDesk = desk;

                C1Window win = new C1Window();

                win.CenterOnScreen();
                win.Content = new InventoryFileLayout();

                win.ShowModal();
                win.Closed += new EventHandler(win_Closed);
            }
            else
            {
                MessageBox.Show("You must select a subscription row to check file layouts for");
                return;
            }
        }

        void win_Closed(object sender, EventArgs e)
        {
            ChangeBookGroupSelection();
        }




    }
}
