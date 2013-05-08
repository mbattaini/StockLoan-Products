using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using C1.Silverlight.Data;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight;

using LoanStarWorldWideAdmin;
using LoanStarWorldWideAdmin.SVR_BooksService;
using LoanStarWorldWideAdmin.SVR_InventoryService;
using LoanStarWorldWideAdmin.SVR_AdminService;

namespace LoanStarWorldWideAdmin.Views
{
    public partial class SubscriptionMaintWindow : UserControl
    {
        public BooksServiceClient booksSvc = new BooksServiceClient();
        public InventoryServiceClient inventorySvc = new InventoryServiceClient();
        public AdminServiceClient adminSvc = new AdminServiceClient();

        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();

        private bool _DisableFeed;
        private string _FirmCode; 
        private string _CountryCode; 
        private string _DeskTypeCode;

        public bool DisableFeed
        {
            get { return _DisableFeed; }
            set { _DisableFeed = value; }
        }

        public SubscriptionMaintWindow()
        {
            InitializeComponent();
            try
            {
                functions.SetFunctionPath(this.Name.Trim());

                inventorySvc.FirmsGetCompleted += new EventHandler<FirmsGetCompletedEventArgs>(inventorySvc_FirmsGetCompleted);
                inventorySvc.DeskTypesGetCompleted += new EventHandler<DeskTypesGetCompletedEventArgs>(inventorySvc_DeskTypesGetCompleted);
                adminSvc.CountriesGetCompleted += new EventHandler<CountriesGetCompletedEventArgs>(adminSvc_CountriesGetCompleted);

                inventorySvc.InventorySubscriptionSetCompleted += new EventHandler<InventorySubscriptionSetCompletedEventArgs>(inventorySvc_InventorySubscriptionSetCompleted);

                DisableFeed = false;

                StatusLabel.Visibility = Visibility.Collapsed;

                appInfo.UserId = ((App)App.Current).UserId;
                appInfo.Password = ((App)App.Current).Password;
                appInfo.FunctionPath = ((App)App.Current).FunctionPath;
                appInfo.LoggedOnBookGroup = ((App)App.Current).LoggedOnBookGroup;
                appInfo.SelectedBookGroup = ((App)App.Current).SelectedBookGroup;
                appInfo.CurrentDesk = ((App)App.Current).CurrentDesk;

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

                FillFirmCombo();
                FillCountryCombo();
                FillDeskTypeCombo();

                FirmCombo.Visibility = Visibility.Visible;
                CountryCombo.Visibility = Visibility.Visible;
                DeskTypeCombo.Visibility = Visibility.Visible;

                firmLabel.Visibility = Visibility.Visible;
                countryLabel.Visibility = Visibility.Visible;
                deskTypeLabel.Visibility = Visibility.Visible;
                SubscriptionConfirmButton.Visibility = Visibility.Visible;

                if (((App)App.Current).EditSubscription.ToUpper().Equals("ADD"))
                {
                    ClearEditControls();
                }
                else
                {
                    SetEditControls();
                }
            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
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
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["Firm"].ToString() + "  | " + dr["FirmCode"].ToString() + "     ";
                    this.FirmCombo.Items.Add(temp);
                }
                this.FirmCombo.SelectedIndex = 0;
            }
        }

        void inventorySvc_DeskTypesGetCompleted(object sender, DeskTypesGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "DeskTypes");
            if (dtTemp == null)
            {
            }
            else
            {

                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["DeskType"].ToString() + "  | " + dr["DeskTypeCode"].ToString() + "     ";
                    this.DeskTypeCombo.Items.Add(temp);
                }
                this.DeskTypeCombo.SelectedIndex = 0;
            }
        }

        void adminSvc_CountriesGetCompleted(object sender, CountriesGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "Countries");

            if (dtTemp == null)
            {
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    if (!dr["CountryCode"].ToString().Trim().Equals("**"))
                    {
                        string temp = dr["Country"].ToString() + "  | " + dr["CountryCode"].ToString() + "     ";
                        this.CountryCombo.Items.Add(temp);
                    }
                }
                this.CountryCombo.SelectedIndex = 0;
            }
        }

        void inventorySvc_InventorySubscriptionSetCompleted(object sender, InventorySubscriptionSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                StatusLabel.Content = "The subscription update failed";
                return;
            }

            if (e.Result)
            {
                StatusLabel.Content = "The subscription updated successfully";
                SubscriptionConfirmButton.Visibility = Visibility.Collapsed;
            }
        }

        private void BuildDeskName()
        {
            int first = 0;
            int last = 0;
            string temp = "";
            string testString = "";
            string firmCode = "";
            string countryCode = "";
            string deskTypeCode = "";

            string subscriptionInfo = "";

            testString = FirmCombo.Text.ToString();

            first = testString.IndexOf("|");
            last = testString.LastIndexOf("|");
            temp = testString.Substring(first + 1, 3);

            firmCode = temp.ToString();


            testString = CountryCombo.SelectedItem.ToString();
            first = testString.IndexOf("|");
            last = testString.LastIndexOf("|");
            temp = testString.Substring(first + 1, 2);

            countryCode = temp.ToString();

            testString = DeskTypeCombo.Text.ToString();

            first = testString.IndexOf("|");
            last = testString.LastIndexOf("|");
            temp = testString.Substring(first + 1, 3);

            deskTypeCode = temp.ToString();

            subscriptionInfo = firmCode + "." + countryCode + "." + deskTypeCode;
            subscriptionInfoLabel.Content = "DESK:  " + subscriptionInfo;
            appInfo.CurrentDesk = subscriptionInfo;
        }

        void FillFirmCombo()
        {
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

            inventorySvc.FirmsGetAsync("", userId, userPwd, bookGroup, functionPath);
        }

        void FillCountryCombo()
        {
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
            adminSvc.CountriesGetAsync("", userId, userPwd, bookGroup, functionPath);
        }

        void FillDeskTypeCombo()
        {
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

            inventorySvc.DeskTypesGetAsync("", "", "", bookGroup, userId, userPwd, functionPath);

        }

        //private void hyperlinkFileLayouts_Click(object sender, RoutedEventArgs e)
        //{
        //    ((App)App.Current).UserId = appInfo.UserId;
        //    ((App)App.Current).Password = appInfo.Password;
        //    ((App)App.Current).FunctionPath = appInfo.FunctionPath;
        //    ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;
        //    ((App)App.Current).SelectedBookGroup = appInfo.SelectedBookGroup;
        //    ((App)App.Current).CurrentDesk = appInfo.CurrentDesk;
        //    ((App)App.Current).InventoryType = appInfo.InventoryType;

        //    C1Window win = new C1Window();

        //    win.CenterOnScreen();
        //    win.Content = new InventoryFileLayout();

        //    win.ShowModal();
        //    win.Closed += new EventHandler(bookGroupWin_Closed);
        //}

        //void bookGroupWin_Closed(object sender, EventArgs e)
        //{
        //    //ChangeBookGroupSelection();
        //}

        private void DisableCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string bookGroup = appInfo.SelectedBookGroup;
            string desk = appInfo.CurrentDesk;

            MessageBoxResult result = MessageBox.Show("You have selected the Disable check box.  Are you sure you wish to disable " + desk + " for Book Group " + bookGroup,
            "DISABLE SUBSCRIPTION FEED", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                DisableFeed = true;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            string bookGroup = appInfo.SelectedBookGroup;
            string desk = appInfo.CurrentDesk;

            //MessageBoxResult result = MessageBox.Show("You have selected Cancel. This will lose any changes. Are you sure you wish to Cancel? " + desk + " for Book Group " + bookGroup,
            //"CANCEL SUBSCRIPTION UPDATES", MessageBoxButton.OKCancel);

            //if (result == MessageBoxResult.OK)
            //{
                ClearEditControls();
                SubscriptionConfirmButton.Visibility = Visibility.Collapsed;
                //hyperlinkFileLayouts.Visibility = Visibility.Collapsed;
                buttonFileLayouts.Visibility = Visibility.Collapsed;
                cancelButton.Visibility = Visibility.Collapsed;

            //}

        }

        void ClearEditControls()
        {
            fileNameText.Text = "";
            fileHostText.Text = "";
            userNameText.Text = "";
            userPasswordText.Text = "";
            addressText.Text = "";
            subjectText.Text = "";
            commentText.Text = "";
            radioButtonInventory.IsChecked = false;
            radioButtonRates.IsChecked = false;
            DisableCheckBox.IsChecked = false;

            FirmCombo.SelectedIndex = 0;
            CountryCombo.SelectedIndex = 0;
            DeskTypeCombo.SelectedIndex = 0;
        }

        void SetEditControls()
        {
            fileNameText.Text = ((App)App.Current).FileName;
            fileHostText.Text = ((App)App.Current).FileHost;
            userNameText.Text = ((App)App.Current).FileUser;
            userPasswordText.Text = ((App)App.Current).FilePWD;
            addressText.Text = ((App)App.Current).EMailAddress;
            subjectText.Text = ((App)App.Current).EMailSubject;
            commentText.Text = ((App)App.Current).SubscribeComment;

            string temp = ((App)App.Current).InventoryType;
            if (temp.ToUpper().Equals("I"))
            {
                radioButtonInventory.IsChecked = true;
            }
            else
            {
                radioButtonRates.IsChecked = true;
            }
            DisableCheckBox.IsChecked = false;

            string subscriptionInfo = ((App)App.Current).CurrentDesk;
            subscriptionInfoLabel.Content = "DESK:  " + subscriptionInfo;
            appInfo.CurrentDesk = subscriptionInfo;

        }

        private bool ValidateDetails()
        {
            bool isValid = false;

            try
            {
                if (radioButtonRates.IsChecked.Equals(true))
                { isValid = true; }
                if (radioButtonInventory.IsChecked.Equals(true))
                { isValid = true; }

                if (fileNameText.Text.Trim().Equals(""))
                { isValid = false; }
                if (fileHostText.Text.Trim().Equals(""))
                { isValid = false; }

                return isValid;
            }
            catch
            {
                return false;
            }
        }

        private void confirmSubscription_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDetails())
            {
                string currentDesk = ((App)App.Current).CurrentDesk;
                string inventoryType = "";
                bool isActive = true;

                if(radioButtonRates.IsChecked.Value)
                {
                    inventoryType = "R";
                }
                else
                {
                    inventoryType = "I";
                }
                ((App)App.Current).InventoryType = inventoryType;
                if(DisableCheckBox.IsChecked.Value)
                {
                    isActive = false;
                }


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

                inventorySvc.InventorySubscriptionSetAsync(bookGroup, currentDesk, inventoryType, "", "", "", "", "", "", "", "", "", 
                    "Test Setup or Update", fileNameText.Text.Trim(), fileHostText.Text.Trim(), userNameText.Text.Trim(), userPasswordText.Text.Trim(),
                    addressText.Text.Trim(), subjectText.Text.Trim(), commentText.Text.Trim(), userId, isActive, bookGroup, userId, userPwd, 
                    functionPath);

            }
            else
            {
                string Msg = "You have not entered required information for this Subscription. Click the OK Button  \r\n" +
                            "to return and finish the updates.\r\n";

                string Caption = "CONFIRMING SUBSCRIPTION ADD";

                MessageBox.Show(Msg, Caption, MessageBoxButton.OK);
            }
        }

        private void DeskTypeCombo_SelectedItemChanged(object sender, PropertyChangedEventArgs<object> e)
        {
            if (((App)App.Current).EditSubscription.ToUpper().Equals("ADD"))
            {
                int first = 0;
                string temp = "";
                string testString = "";

                testString = DeskTypeCombo.Text.ToString();

                first = testString.IndexOf("|");
                temp = testString.Substring(first + 1, 5);

                _DeskTypeCode = temp.ToString().Trim();

                subscriptionInfoLabel.Content = "DESK:  " + _FirmCode + "." + _CountryCode + "." + _DeskTypeCode;
                appInfo.CurrentDesk = _FirmCode + "." + _CountryCode + "." + _DeskTypeCode;
            }
        }

        private void CountryCombo_SelectedItemChanged(object sender, PropertyChangedEventArgs<object> e)
        {
            if (((App)App.Current).EditSubscription.ToUpper().Equals("ADD"))
            {
                int first = 0;
                string temp = "";
                string testString = "";

                testString = CountryCombo.Text.ToString();

                first = testString.IndexOf("|");
                temp = testString.Substring(first + 1, 5);

                _CountryCode = temp.ToString().Trim();

                subscriptionInfoLabel.Content = "DESK:  " + _FirmCode + "." + _CountryCode + "." + _DeskTypeCode;
                appInfo.CurrentDesk = _FirmCode + "." + _CountryCode + "." + _DeskTypeCode;
            }
        }

        private void FirmCombo_SelectedItemChanged(object sender, PropertyChangedEventArgs<object> e)
        {
            if (((App)App.Current).EditSubscription.ToUpper().Equals("ADD"))
            {
                int first = 0;
                string temp = "";
                string testString = "";

                testString = FirmCombo.Text.ToString();

                first = testString.IndexOf("|");
                temp = testString.Substring(first + 1, 5);

                _FirmCode = temp.ToString().Trim();

                subscriptionInfoLabel.Content = "DESK:  " + _FirmCode + "." + _CountryCode + "." + _DeskTypeCode;
                appInfo.CurrentDesk = _FirmCode + "." + _CountryCode + "." + _DeskTypeCode;
            }
        }
        
        private void radioButtonInventory_Checked(object sender, RoutedEventArgs e)
        {
            appInfo.InventoryType = "I";
            appInfo.SubscriptionUpdatesReq = true;
        }

        private void radioButtonRates_Checked(object sender, RoutedEventArgs e)
        {
            appInfo.InventoryType = "R";
            appInfo.SubscriptionUpdatesReq = true;
        }

        private void buttonFileLayouts_Click(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).UserId = appInfo.UserId;
            ((App)App.Current).Password = appInfo.Password;
            ((App)App.Current).FunctionPath = appInfo.FunctionPath;
            ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;
            ((App)App.Current).SelectedBookGroup = appInfo.SelectedBookGroup;
            ((App)App.Current).CurrentDesk = appInfo.CurrentDesk;
            ((App)App.Current).InventoryType = appInfo.InventoryType;

            C1Window win = new C1Window();

            win.CenterOnScreen();
            win.Content = new InventoryFileLayout();

            win.ShowModal();
            win.Closed += new EventHandler(win_Closed);
        }

        void win_Closed(object sender, EventArgs e)
        {
            //ChangeBookGroupSelection();
        }

    }
}
