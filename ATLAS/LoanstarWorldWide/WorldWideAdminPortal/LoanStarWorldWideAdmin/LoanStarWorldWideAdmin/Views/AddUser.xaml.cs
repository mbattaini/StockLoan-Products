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

using LoanStarWorldWideAdmin.SVR_UserAdminService;
using LoanStarWorldWideAdmin.SVR_BooksService;
using LoanStarWorldWideAdmin.SVR_SecurityService;

using StockLoan.Encryption;

namespace LoanStarWorldWideAdmin.Views
{
    public partial class AddUser : Page
    {
        public UserAdminServiceClient userSvc = new UserAdminServiceClient();
        public BooksServiceClient booksSvc = new BooksServiceClient();
        public SecurityServiceClient securitySvc = new SecurityServiceClient();

        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();

        private static string _userIdSet;
        private static string _shortName; 
        private static string _ePwd;
        private static string _eMail; 
        private static string _title;
        private static string _comment;
        private static string _functionPathSet;
        private static string _roleComment;
        private static string _roleName;
        private static bool _changePWD;
        private static bool _isDisabled;

        public string RoleComment
        {
            get { return _roleComment; }
            set { _roleComment = value; }
        }
        public string UserIdSet
        {
            get { return _userIdSet; }
            set { _userIdSet = value; }
        }
        public string ShortName
        {
            get { return _shortName; }
            set { _shortName = value; }
        }
        public string ePwd
        {
            get { return _ePwd; }
            set { _ePwd = value; }
        }
        public string EMail
        {
            get { return _eMail; }
            set { _eMail = value; }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        public string FunctionPathSet
        {
            get { return _functionPathSet; }
            set { _functionPathSet = value; }
        }
        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }
        public bool ChangePWD
        {
            get { return _changePWD; }
            set { _changePWD = value; }
        }
        public bool IsDisabled
        {
            get { return _isDisabled; }
            set { _isDisabled = value; }
        }

        
        public AddUser()
        {
            InitializeComponent();
            try
            {
                functions.SetFunctionPath(this.Name.Trim());

                userSvc.UserGetCompleted += new EventHandler<UserGetCompletedEventArgs>(userSvc_UserGetCompleted);
                userSvc.UserSetCompleted += new EventHandler<UserSetCompletedEventArgs>(userSvc_UserSetCompleted);

                userSvc.UserRolesGetCompleted += new EventHandler<UserRolesGetCompletedEventArgs>(userSvc_UserRolesGetCompleted);
                userSvc.UserRolesSetCompleted += new EventHandler<UserRolesSetCompletedEventArgs>(userSvc_UserRolesSetCompleted);

                userSvc.RolesGetCompleted += new EventHandler<RolesGetCompletedEventArgs>(userSvc_RolesGetCompleted);

                securitySvc.UserPasswordResetCompleted += new EventHandler<UserPasswordResetCompletedEventArgs>(securitySvc_UserPasswordResetCompleted);

                userSvc.RoleFunctionsBookGroupGetCompleted += new EventHandler<RoleFunctionsBookGroupGetCompletedEventArgs>(userSvc_RoleFunctionsBookGroupGetCompleted);
                userSvc.RoleFunctionsBookGroupSetCompleted += new EventHandler<RoleFunctionsBookGroupSetCompletedEventArgs>(userSvc_RoleFunctionsBookGroupSetCompleted);

                userSvc.RoleFunctionsGetCompleted += new EventHandler<RoleFunctionsGetCompletedEventArgs>(userSvc_RoleFunctionsGetCompleted);
                userSvc.RoleFunctionSetCompleted += new EventHandler<RoleFunctionSetCompletedEventArgs>(userSvc_RoleFunctionSetCompleted);

                booksSvc.BookGroupsGetAllCompleted += new EventHandler<BookGroupsGetAllCompletedEventArgs>(booksSvc_BookGroupsGetAllCompleted);

            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            appInfo.UserId = ((App)App.Current).UserId;
            appInfo.Password = ((App)App.Current).Password;
            appInfo.FunctionPath = ((App)App.Current).FunctionPath;
            appInfo.LoggedOnBookGroup = ((App)App.Current).LoggedOnBookGroup;

            StatusLabel.Visibility = Visibility.Collapsed;
            UpdateLabel.Visibility = Visibility.Collapsed;
            ClearUserFields();
            FillRolesCombo();
            ExistUserRadioButton.IsChecked = true;

        }


        void userSvc_RolesGetCompleted(object sender, RolesGetCompletedEventArgs e)
        {
            RoleCombo.Items.Clear();
            if (e.Error != null)
            {
                return;
            }

            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "RoleNames");
            if (dtTemp == null)
            {
                StatusLabel.Content = " Not authorized for User Maintenance for Book Group " + appInfo.LoggedOnBookGroup;
                StatusLabel.Visibility = Visibility.Visible;
                DisableUserFields();
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["RoleName"].ToString();
                    RoleCombo.Items.Add(temp);
                }
            }

        }

        void userSvc_RoleFunctionsGetCompleted(object sender, RoleFunctionsGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "RoleFunctions");
            if (dtTemp == null)
            {
                StatusLabel.Content = " Not authorized for User Maintenance for Book Group " + appInfo.LoggedOnBookGroup;
                StatusLabel.Visibility = Visibility.Visible;
                DisableUserFields();
            }
            else
            {
                FunctionCombo.Items.Clear();

                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["FunctionPath"].ToString();
                    FunctionCombo.Items.Add(temp);
                }
            }
        }

        void userSvc_RoleFunctionsBookGroupGetCompleted(object sender, RoleFunctionsBookGroupGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            if (StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                DataTable dtTemp = new DataTable();
                dtTemp = Functions.ConvertToDataTable(e.Result, "RoleFunctionsBookGroup");

                if (dtTemp == null)
                {
                }
                else
                {
                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        string temp = dr["UserId"].ToString();

                        UserIdTextBox.Text = dr["UserId"].ToString();
                        ShortNameTextBox.Text = dr["ShortName"].ToString();
                        if (dr["Email"] == null)
                        {
                            UserEmailTextBox.Text = "No Email";
                        }
                        else
                        {
                            UserEmailTextBox.Text = dr["Email"].ToString();
                        }
                        if (dr["Title"] == null)
                        {
                            UserTitleTextBox.Text = "";
                        }
                        else
                        {
                            UserTitleTextBox.Text = dr["Title"].ToString();
                        }
                    }
                    RolesGrid.ItemsSource = dtTemp.DefaultView;
                    
                    ExistUserRadioButton.IsChecked = true;
                }
            }

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
            }
            BookGroupCombo.SelectedIndex = 0;
        }

        void userSvc_UserRolesGetCompleted(object sender, UserRolesGetCompletedEventArgs e)
        {
            RoleCombo.Items.Clear();
            if (e.Error != null)
            {
                return;
            }

            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "UserRoles");
            if (dtTemp == null)
            {
                StatusLabel.Content = " Not authorized for User Maintenance for Book Group " + appInfo.LoggedOnBookGroup;
                StatusLabel.Visibility = Visibility.Visible;
                DisableUserFields();
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["RoleName"].ToString();
                    RoleCombo.Items.Add(temp);
                }
            }

        }

        void userSvc_UserGetCompleted(object sender, UserGetCompletedEventArgs e)
        {
            ClearUserFields();
            if (e.Error != null)
            {
                return;
            }

            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "Users");
            if (dtTemp == null)
            {
                StatusLabel.Content = " Not authorized for User Maintenance for Book Group " + appInfo.LoggedOnBookGroup;
                StatusLabel.Visibility = Visibility.Visible;
                DisableUserFields();
            }

            if (appInfo.UserMaintType.Equals("FILL"))
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["UserId"].ToString();
                    UserListCombo.Items.Add(temp);
                }

                if (ExistUserRadioButton.IsChecked.Value)
                {
                    appInfo.UserMaintType = "EXIST";
                    return;
                }

                if (NewUserRadioButton.IsChecked.Value)
                {
                    appInfo.UserMaintType = "NEW";
                    return;
                }
                UserListCombo.SelectedIndex = 0;
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    UserIdTextBox.Text = dr["UserId"].ToString();
                    if (dr["ShortName"] == null)
                    {
                        ShortNameTextBox.Text = "";
                    }
                    else
                    {
                        ShortNameTextBox.Text = dr["ShortName"].ToString();
                    }

                    if (dr["Email"] == null)
                    {
                        UserEmailTextBox.Text = "";
                    }
                    else
                    {
                        UserEmailTextBox.Text = dr["Email"].ToString();
                    }
                    if (dr["Title"] == null)
                    {
                        UserTitleTextBox.Text = "";
                    }
                    else
                    {
                        UserTitleTextBox.Text = dr["Title"].ToString();
                    }

                    if (dr["Comment"] == null)
                    {
                        UserCommentsTextBox.Text = "";
                    }
                    else
                    {
                        UserCommentsTextBox.Text = dr["Comment"].ToString().Trim();
                    }
                    ActiveCheckBox.IsChecked = bool.Parse(dr["IsActive"].ToString());
                    DisabledCheckBox.IsChecked = bool.Parse(dr["IsLocked"].ToString());
                }   

            }
        }

#region Set Completed

        void userSvc_RoleFunctionsBookGroupSetCompleted(object sender, RoleFunctionsBookGroupSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " RoleFunctionsBookGroup Set did not complete successfully. Please check information and try again";

                return;
            }

            UpdateLabel.Visibility = Visibility.Collapsed;

            FillRoleFunctionsBookGroup();

        }

        void userSvc_RoleFunctionSetCompleted(object sender, RoleFunctionSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " RoleFunctions Set did not complete successfully. Please check information and try again";
                return;
            }
            UpdateLabel.Visibility = Visibility.Collapsed;

            SetRoleFunctionsBookGroup();

        }

        void userSvc_UserRolesSetCompleted(object sender, UserRolesSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " UserRoles Set did not complete successfully. Please check information and try again";
                return;
            }
            UpdateLabel.Visibility = Visibility.Collapsed;
            
            SetRoleFunctions();

        }

        void userSvc_UserSetCompleted(object sender, UserSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " User Set did not complete successfully. Please check information and try again";
                return;
            }

            if (ChangePWD)            
            {
                securitySvc.UserPasswordResetAsync(UserIdSet, ePwd);
            }
            UpdateLabel.Visibility = Visibility.Collapsed;
        }

        
#endregion

        private void ExistUserRadioButton_Checked(object sender, RoutedEventArgs e)
        {

            UserListCombo.Visibility = Visibility.Visible;
            UserListLabel.Visibility = Visibility.Visible;
            RolesGrid.ItemsSource = "";

            appInfo.UserMaintType = "EXIST";

            FillBookGroupCombo();

            FillUserList();

        }

        private void NewUserRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }

            UserListCombo.Visibility = Visibility.Collapsed;
            UserListLabel.Visibility = Visibility.Collapsed;
            RolesGrid.ItemsSource = "";

            appInfo.UserMaintType = "NEW";

            ClearUserFields();

            BookGroupCombo.SelectedIndex = 0;

            PasswordTextBox.Password = "Password";
        }

        private void ClearUserFields()
        {
            UserIdTextBox.Text = "";
            ShortNameTextBox.Text = "";
            PasswordTextBox.Password = "";
            UserEmailTextBox.Text = "";
            UserTitleTextBox.Text = "";
            UserCommentsTextBox.Text = "";
            ActiveCheckBox.IsChecked = true;
            DisabledCheckBox.IsChecked = false;

            //RoleCombo.SelectedIndex = -1;
            //FunctionCombo.SelectedIndex = -1;
            //RoleCommentsTextBox.Text = "";
        }

        private void DisableUserFields()
        {
            UserIdTextBox.IsEnabled = false;
            ShortNameTextBox.IsEnabled = false;
            PasswordTextBox.IsEnabled = false;
            UserEmailTextBox.IsEnabled = false;
            UserTitleTextBox.IsEnabled = false;
            UserCommentsTextBox.IsEnabled = false;
            ActiveCheckBox.IsEnabled = false;
            DisabledCheckBox.IsEnabled = false;
            BookGroupCombo.IsEnabled = false;
            RoleCombo.IsEnabled = false;
        }

        private void UserListCombo_SelectedItemChanged(object sender, C1.Silverlight.PropertyChangedEventArgs<object> e)
        {
            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }

            if (!appInfo.UserMaintType.Equals("NEW"))
            {
                string userIdGet = "";
                if (UserListCombo.Text == null)
                {
                    userIdGet = "";
                }
                else
                {
                    userIdGet = UserListCombo.Text;
                }
                string userId = appInfo.UserId.ToString();
                string userPwd = appInfo.Password.ToString();
                string bookGroup = appInfo.LoggedOnBookGroup;
                string functionPath = appInfo.FunctionPath;
                
                ClearUserFields();

                RolesGrid.ItemsSource = "" ;
                userSvc.UserGetAsync(userIdGet, userId, userPwd, bookGroup, functionPath);

                FillRoleFunctionsBookGroup();
            }

        }

        private void BookGroupCombo_SelectedItemChanged(object sender, C1.Silverlight.PropertyChangedEventArgs<object> e)
        {

            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            if (NewUserRadioButton.IsChecked.Value == true)
            {
                ClearUserFields();
            }
            FillRoleFunctionsBookGroup();

        }

        private void BookGroupCombo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            if (UserListCombo.Text == null)
            {
                return;
            }
            if (!UserListCombo.Text.Equals("")) 
            {
                if (BookGroupCombo.Text != null )    
                {
                    FillRoleFunctionsBookGroup();
                }
            }
        }

        private void FillUserList()
        {
            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }

            string userId = appInfo.UserId;
            string userPwd = appInfo.Password;
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
            UserListCombo.Items.Clear();
            appInfo.UserMaintType = "FILL";
            userSvc.UserGetAsync("", userId, userPwd, bookGroup, functionPath);


        }


        private void FillBookGroupCombo()
        {
            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
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

            booksSvc.BookGroupsGetAllAsync(bookGroup, "", userId, userPwd, functionPath);
        }
        
        private void RoleCombo_SelectedItemChanged(object sender, C1.Silverlight.PropertyChangedEventArgs<object> e)
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

            string roleName = RoleCombo.Text;
            short utcOffSet = 0;

            userSvc.RoleFunctionsGetAsync(roleName, "", utcOffSet, userId, userPwd, bookGroup, functionPath);

            FunctionLabel.Visibility = Visibility.Visible;
            FunctionCombo.Visibility = Visibility.Visible;
            AddNewFunctionButton.Visibility = Visibility.Visible;
            AddNewFunctionHyperlink.Visibility = Visibility.Visible;

        }

        private bool ValidateUser(string ValidateType)
        {
            bool bTemp = true;

            if (BookGroupCombo.Text == null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " You must select a book group before you can save";
                bTemp = false;
            }
            if (UserIdTextBox.Text.Trim().ToString().Equals(""))
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " You must enter a User ID before you can save";
                bTemp = false;
            }
            if (UserEmailTextBox.Text.Trim().ToString().Equals(""))
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " You must enter a valid E-Mail address for the User before you can save";
                bTemp = false;
            }
            if (ValidateType.ToUpper().Equals("NEW"))
            {
                if (PasswordTextBox.Password.Trim().ToString().Equals(""))
                {
                    UpdateLabel.Visibility = Visibility.Visible;
                    UpdateLabel.Content = " You must enter a password for a new User before you can save";
                    bTemp = false;
                }
                int rIndex = RoleCombo.SelectedIndex;
                if (rIndex.Equals(-1))
                {
                    UpdateLabel.Visibility = Visibility.Visible;
                    UpdateLabel.Content = " You must assign a role for a new User before you can save";
                    bTemp = false;
                }
                int fIndex = FunctionCombo.SelectedIndex;
                if (fIndex.Equals(-1))
                {
                    UpdateLabel.Visibility = Visibility.Visible;
                    UpdateLabel.Content = " You must assign a function for a new User before you can save";
                    bTemp = false;
                }
            }
            if (ActiveCheckBox.IsChecked.Equals(false) && DisabledCheckBox.IsChecked.Equals(false))
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " You must indicate a User is Either Active or Disabled before you can save";
                bTemp = false;
            }

            return bTemp;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            var result = MessageBox.Show("Cancel will lose any changes you may have made. Are you sure this is what you want to do?",
                    "CANCEL NOTICE", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                ClearUserFields();
                RolesGrid.ItemsSource = "";
                //this.Visibility = Visibility.Collapsed;
                //this.NavigationService.GoBack(); //Navigate(new Uri(@"/Views/SubscriptionMaint.xaml", UriKind.Relative));
            }
        }

        private void SetUserRoles(string userIdToSet, string roleName, string roleComment, string functionPathSet)
        {
            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }

            string bookGroupSet = BookGroupCombo.Text.Trim(); ;
            bool delInd = false;
            short isActive = 0;
            if (ActiveCheckBox.IsChecked.Equals(true))
            {
                isActive = 1;
            }

            string userId = appInfo.UserId.ToString();
            string userPwd = appInfo.Password.ToString();
            string bookGroup = appInfo.LoggedOnBookGroup;
            string functionPath = appInfo.FunctionPath;

            bookGroupSet = BookGroupCombo.Text;
               
            userSvc.UserRolesSetAsync(userIdToSet, roleName, roleComment, delInd, userId, userPwd, bookGroup, functionPath);

        }

        private void SetRoleFunctions()
        {
            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }

            string bookGroupSet = BookGroupCombo.Text.Trim(); ;
            bool delInd = false;
            short isActive = 0;
            if (ActiveCheckBox.IsChecked.Equals(true))
            {
                isActive = 1;
            }

            string userId = appInfo.UserId.ToString();
            string userPwd = appInfo.Password.ToString();
            string bookGroup = appInfo.LoggedOnBookGroup;
            string functionPath = appInfo.FunctionPath;

            userSvc.RoleFunctionSetAsync(RoleName, FunctionPathSet, false, false, RoleComment, delInd, userId, userPwd, bookGroup, functionPath);

        }

        private void SetRoleFunctionsBookGroup()
        {
            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }

            string bookGroupSet = BookGroupCombo.Text.Trim(); ;
            short isActive = 0;
            if (ActiveCheckBox.IsChecked.Equals(true))
            {
                isActive = 1;
            }

            string userId = appInfo.UserId.ToString();
            string userPwd = appInfo.Password.ToString();
            string bookGroup = appInfo.LoggedOnBookGroup;
            string functionPath = appInfo.FunctionPath;

            bookGroupSet = BookGroupCombo.Text;

            userSvc.RoleFunctionsBookGroupSetAsync(RoleName, FunctionPathSet, bookGroupSet, UserIdSet, isActive, 0, bookGroup, functionPath, userId, userPwd);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!StatusLabel.Content.Equals("Status") || StatusLabel.Content.Equals(""))
            {
                return;
            }
            
            bool isValid = false;
            
            UserIdSet = UserIdTextBox.Text;
            ShortName = ShortNameTextBox.Text;
            EMail = UserEmailTextBox.Text;
            Title = UserTitleTextBox.Text;
            Comment = UserCommentsTextBox.Text;
            ePwd = "";
            RoleComment = RoleCommentsTextBox.Text.Trim();
            IsDisabled = DisabledCheckBox.IsChecked.Value;

            bool isActive = ActiveCheckBox.IsChecked.Value;

            if (RoleCombo.SelectedIndex > -1)
            { 
                RoleName = RoleCombo.SelectedItem.ToString(); 
            }
            else
            { 
                RoleName = ""; 
            }
            if (FunctionCombo.SelectedIndex > -1)
            { 
                FunctionPathSet = FunctionCombo.SelectedItem.ToString(); 
            }
            else
            { 
                FunctionPathSet = ""; 
            }

            string userId = appInfo.UserId.ToString();
            string userPwd = appInfo.Password.ToString();
            string bookGroup = appInfo.LoggedOnBookGroup;
            string functionPath = appInfo.FunctionPath;

            if (NewUserRadioButton.IsChecked.Equals(true))
            {
                isValid = ValidateUser("NEW");
                appInfo.UserMaintType = "NEW";
                if (isValid.Equals(true))
                {
                    ePwd = EncryptDecrypt.Encrypt(PasswordTextBox.Password.ToString());
                    
                    ChangePWD = true;            // with this set, UserSetCompleted will execute UserPasswordReset 
                    bool IsLoggedIn = false;
                    userSvc.UserSetAsync(UserIdSet, ShortName, ePwd, EMail, Title, Comment, IsLoggedIn, IsDisabled, isActive, userId, userPwd, bookGroup, functionPath);
                                        
                    SetUserRoles(UserIdSet, RoleName, RoleComment, FunctionPathSet);
                    
                }
            }
            else
            {
                isValid = ValidateUser("EXIST");
                appInfo.UserMaintType = "EXIST";
                if (isValid.Equals(true))
                {
                    bool IsLoggedIn = false; 
                    userSvc.UserSetAsync(UserIdSet, ShortName, "", EMail, Title, Comment, IsLoggedIn, IsDisabled, isActive, userId, userPwd, bookGroup, functionPath);

                    if (RoleCombo.Text != null)
                    {
                        if (isActive.Equals(true))
                        {
                            SetUserRoles(UserIdSet, RoleName, RoleComment, FunctionPathSet);
                        }
                    }
                }
                else
                { 
                    UpdateLabel.Visibility = Visibility.Visible; 
                }
            }
            
            FillUserList();
            ExistUserRadioButton.IsChecked = true;

        }

        void securitySvc_UserPasswordResetCompleted(object sender, UserPasswordResetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                UpdateLabel.Visibility = Visibility.Visible;
                UpdateLabel.Content = " New User PasswordReSet did not complete successfully. Please check information and try again";
                return;
            }
            ChangePWD = false;
            UpdateLabel.Visibility = Visibility.Collapsed;
        }

        private void UserListCombo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void FillRoleFunctionsBookGroup()
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
            if ((FunctionCombo.Text == null) || (FunctionCombo.Text.Trim().Equals("")))
            {
                if ((functionPath == null) || (functionPath.Equals("")))
                {
                    functionPath = appInfo.DefaultFunctionPath;
                    appInfo.FunctionPath = functionPath;
                }
            }
            else
            {
                functionPath = FunctionCombo.Text;
            }
            
            string bookGroupGet = "";
            
            if (BookGroupCombo.SelectedIndex > -1)
            {
                bookGroupGet = BookGroupCombo.SelectedItem.ToString();
            }

            string userIdGet = "";

            if (NewUserRadioButton.IsChecked == true)
            {
                if (UserIdTextBox.Text == null)
                {
                    userIdGet = "";
                }
                else
                {
                    userIdGet = UserIdTextBox.Text;
                }
            }
            else
            {
                if (UserListCombo.Text == null)
                {
                    userIdGet = "";
                }
                else
                {
                    userIdGet = UserListCombo.Text;
                }

                RoleCombo.SelectedIndex = -1;
                FunctionCombo.SelectedIndex = -1;
                RoleCommentsTextBox.Text = "";

                userSvc.RoleFunctionsBookGroupGetAsync(userIdGet, bookGroupGet, bookGroup, functionPath, userId, userPwd);
            }
        }
        
        private void FillRolesCombo()
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

            userSvc.RolesGetAsync("", userId, userPwd, bookGroup, functionPath);

        }

        private void BookGroupCombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Content == null)
            {
                return;
            }
            if (!this.Content.Equals(""))
            {
            }
        }

        private void BookGroupCombo_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Content == null)
            {
                return;
            }
            if (!this.Content.Equals(""))
            {
            }

        }

        private void FunctionCombo_SelectedItemChanged(object sender, PropertyChangedEventArgs<object> e)
        {
            string roleName = RoleCombo.Text.Trim();
            string functionName = FunctionCombo.Text.Trim();

            RoleCommentsTextBox.Text = roleName + " + " + functionName;
        }

        private void RolesGrid_DeletingRows(object sender, C1.Silverlight.DataGrid.DataGridDeletingRowsEventArgs e)
        {
            bool deletingAccepted = false;
            if (!deletingAccepted)
            {
                int uidCol = 0;
                int bookGrpCol = 1;
                int roleCol = 2;
                int functionCol = 3;
                int currRow = 0;

                string uid = "";
                string bookGrp = "";
                string role = "";
                string functn = "";
                string msg = "";

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

                currRow = RolesGrid.CurrentRow.Index;
                
                uid = RolesGrid[currRow, uidCol].Text.Trim();
                bookGrp = RolesGrid[currRow, bookGrpCol].Text.Trim();
                role = RolesGrid[currRow, roleCol].Text.Trim();
                functn = RolesGrid[currRow, functionCol].Text.Trim();

                msg = "You have pressed Delete. This will delete the " + functn.Trim() + " function for the " + role.Trim() +
                    " /r/n Role within the " + bookGrp.Trim() + " Book Group. Are you sure this is what you want to do?";
                e.Cancel = true;
                C1MessageBox.Show(msg, "DELETE NOTICE", C1MessageBoxButton.YesNo, C1MessageBoxIcon.Question, (result) =>
                {
                        if (result == MessageBoxResult.Yes)
                        {
                            userSvc.RoleFunctionsBookGroupSetAsync(role, functn, bookGrp, uid, 1, 1, bookGroup, functionPath, userId, userPwd);
                            
                            deletingAccepted = true;
                            deletingAccepted = false;
                        }
                    });
            }

        }

        private void AddNewBookGroupHyperlink_Click(object sender, RoutedEventArgs e)
        {
            ((App)App.Current).UserId = appInfo.UserId;
            ((App)App.Current).Password = appInfo.Password;
            ((App)App.Current).FunctionPath = appInfo.FunctionPath;
            ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;

        }

        private void ActiveCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ActiveCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveCheckBox.IsChecked.Value == true)
            {
                DisabledCheckBox.IsChecked = false;
            }
            if (ActiveCheckBox.IsChecked.Value == false)
            { 
                DisabledCheckBox.IsChecked = true; 
            }
        }
    }
}