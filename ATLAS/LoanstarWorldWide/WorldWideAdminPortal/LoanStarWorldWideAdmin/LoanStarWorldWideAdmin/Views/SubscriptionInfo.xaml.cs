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
using LoanStarWorldWideAdmin.SVR_InventoryService;
using LoanStarWorldWideAdmin.SVR_BooksService;
using C1.Silverlight;
using C1.Silverlight.Data;

namespace LoanStarWorldWideAdmin.Views
{
    public partial class SubscriptionInfo : Page
    {
        public InventoryServiceClient inventorySvc = new InventoryServiceClient();
        public BooksServiceClient booksSvc = new BooksServiceClient();

        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();

		public SubscriptionInfo()
        {
            InitializeComponent();
            functions.SetFunctionPath(this.Name.Trim());
            
            booksSvc.BookGroupsGetAllCompleted += new EventHandler<BookGroupsGetAllCompletedEventArgs>(booksSvc_BookGroupsGetAllCompleted);
        }

        private void inventorySvc_InventorySubscriptionsGetCompleted(object sender, InventorySubscriptionsGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "InventorySubscriptions");

            if (dtTemp == null)
            {
                StatusLabel.Content = "    Not authorized for Book Group " + appInfo.LoggedOnBookGroup;
                StatusLabel.Visibility = Visibility.Visible;
            }
            else
            {

                InventoryGrid.ItemsSource = null;
                InventoryGrid.IsLoading = true;

                InventoryGrid.ItemsSource = dtTemp.DefaultView; 
                InventoryGrid.Visibility = Visibility.Visible;

                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["BookGroup"].ToString();
                    TextBookGroup.Text = "Book Group:  " + temp;
                }

                InventoryGrid.IsLoading = false;
            }
        }

        void inventorySvc_InventoryRatesGetCompleted(object sender, InventorySubscriptionsGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "InventorySubscriptions");

            if (dtTemp == null)
            {
                StatusLabel.Content = "    Not authorized for Book Group " + appInfo.LoggedOnBookGroup;
                StatusLabel.Visibility = Visibility.Visible;
            }
            else
            {
                RateGrid.ItemsSource = null;
                RateGrid.IsLoading = true;

                RateGrid.ItemsSource = dtTemp.DefaultView;
                RateGrid.Visibility = Visibility.Visible;

                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["BookGroup"].ToString();
                    TextBookGroup.Text = "Book Group:  " + temp;
                }

                RateGrid.IsLoading = false;
            }
        }
       
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            appInfo.UserId = ((App)App.Current).UserId;
            appInfo.Password = ((App)App.Current).Password;
            appInfo.FunctionPath = ((App)App.Current).FunctionPath;
            appInfo.LoggedOnBookGroup = ((App)App.Current).LoggedOnBookGroup;

            StatusLabel.Visibility = Visibility.Collapsed;
            FillBookGroups();
            LoadInventoryGrid();
        }

        private void FillBookGroups()
        {
            StatusLabel.Visibility = Visibility.Collapsed;

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

        }

        void booksSvc_BookGroupsGetAllCompleted(object sender, BookGroupsGetAllCompletedEventArgs e)
        {
            BookGroupCombo.Items.Clear();

            if (e.Error != null)
            {
                return;
            }

            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "BookGroups");

            if (dtTemp == null)
            {
                StatusLabel.Content = "    Not authorized for Book Group " + appInfo.LoggedOnBookGroup;
                StatusLabel.Visibility = Visibility.Visible;
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["BookGroup"].ToString();
                    BookGroupCombo.Items.Add(temp);
                }
                BookGroupCombo.SelectedItem = appInfo.LoggedOnBookGroup;
            }
        }


        private void LoadInventoryGrid()
        {
            string userId = appInfo.UserId.ToString();
            string userPwd = appInfo.Password.ToString();
            string bookGroup = appInfo.LoggedOnBookGroup;
            string functionPath = appInfo.FunctionPath;
            string bookGroupGet = "" ;

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

            if (BookGroupCombo.Text != null)
            {
                bookGroupGet = BookGroupCombo.SelectedItem.ToString();
            }
            else
            {
                bookGroupGet = bookGroup;
            }

            inventorySvc.InventorySubscriptionsGetCompleted +=new EventHandler<InventorySubscriptionsGetCompletedEventArgs>(inventorySvc_InventorySubscriptionsGetCompleted);

            if(bookGroupGet.Trim().Equals(""))
            {
                inventorySvc.InventorySubscriptionsGetAsync(bookGroup, "", "I", 0, bookGroup, userId, userPwd, functionPath);
            }
            else
            {
                inventorySvc.InventorySubscriptionsGetAsync(bookGroupGet, "", "I", 0, bookGroup, userId, userPwd, functionPath);
            }
            
        }

        private void LoadRatesGrid()
        {
            string userId = appInfo.UserId.ToString();
            string userPwd = appInfo.Password.ToString();
            string bookGroup = appInfo.LoggedOnBookGroup;
            string functionPath = appInfo.FunctionPath;
            string bookGroupGet = BookGroupCombo.SelectedItem.ToString();

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

            inventorySvc.InventorySubscriptionsGetCompleted += new EventHandler<InventorySubscriptionsGetCompletedEventArgs>(inventorySvc_InventoryRatesGetCompleted);

            if(bookGroupGet.Trim().Equals(""))
            {
                inventorySvc.InventorySubscriptionsGetAsync(bookGroup, "", "R", 0, bookGroup, userId, userPwd, functionPath);
            }
            else
            {
                inventorySvc.InventorySubscriptionsGetAsync(bookGroupGet, "", "R", 0, bookGroup, userId, userPwd, functionPath);
            }
    
            C1MouseHelper mouseHelperRateGrid = new C1MouseHelper(RateGrid);
            mouseHelperRateGrid.MouseDoubleClick += new MouseEventHandler(mouseHelperRateGrid_MouseDoubleClick);
        }

        private void mouseHelperInventoryGrid_MouseDoubleClick(object sender, MouseEventArgs eArgs)
        {

        }

        private void mouseHelperRateGrid_MouseDoubleClick(object sender, MouseEventArgs eArgs)
        {

        }

        private void DeskDatePicker_DateTimeChanged(object sender, C1.Silverlight.DateTimeEditors.NullablePropertyChangedEventArgs<System.DateTime> e)
        {
            InventoryGrid.ItemsSource = null;
        }

        private void C1TabItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LoadInventoryGrid();
        }

        private void C1TabItem_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            LoadRatesGrid();
        }

        private void SubscriptionInformation_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void BookGroupCombo_SelectedIndexChanged(object sender, PropertyChangedEventArgs<int> e)
        {
            string bookGroupGet = BookGroupCombo.SelectedItem.ToString().Trim().ToUpper();
            appInfo.SelectedBookGroup = bookGroupGet;
            string userId = appInfo.UserId.ToString();
            string userPwd = appInfo.Password.ToString();
            string bookGroup = appInfo.LoggedOnBookGroup;
            string functionPath = appInfo.FunctionPath;
            string inventoryType = "";

            inventoryType = "I"; 
            
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

            inventorySvc.InventorySubscriptionsGetAsync(bookGroupGet, "", inventoryType, 0, bookGroup, userId, userPwd, functionPath);

        }

        }
    }





