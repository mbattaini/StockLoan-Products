using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Browser;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StockLoan.Encryption;
using C1.Silverlight.Data;
using LoanStarWorldWideAdmin;
using LoanStarWorldWideAdmin.SVR_SecurityService;
using LoanStarWorldWideAdmin.SVR_BooksService;
using LoanStarWorldWideAdmin.SVR_UserAdminService;

namespace LoanStarWorldWideAdmin
{
    public partial class MainPage : UserControl
    {
        public BooksServiceClient booksSvc = new BooksServiceClient();
        public UserAdminServiceClient adminSvc = new UserAdminServiceClient();
        public SecurityServiceClient securitySvc = new SecurityServiceClient();

        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();

        private static bool _logoff = false;

        public static bool LogOff
        {
            get { return _logoff; }
            set { _logoff = value; }
        }


        public MainPage()
        {
            InitializeComponent();

            try
            {
                functions.SetFunctionPath(this.Name.Trim());
                ScreenTextBox.Text = ((App)App.Current).ScreenNameText;

                ClearAppInformation();
                C1_MainMenu.Visibility = Visibility.Collapsed;
                PwdChangeGrid.Visibility = Visibility.Collapsed;

                ShowBookGroup();

                booksSvc.BookGroupsGetAllCompleted += new EventHandler<BookGroupsGetAllCompletedEventArgs>(booksSvc_BookGroupsGetAllCompleted);

                securitySvc.UserValidateCompleted += new EventHandler<UserValidateCompletedEventArgs>(securitySvc_UserValidateCompleted);
                securitySvc.UserPasswordChangeCompleted += new EventHandler<UserPasswordChangeCompletedEventArgs>(securitySvc_UserPasswordChangeCompleted);

                adminSvc.UserRolesGetCompleted += new EventHandler<UserRolesGetCompletedEventArgs>(adminSvc_UserRolesGetCompleted);
                adminSvc.UserSetCompleted += new EventHandler<UserSetCompletedEventArgs>(adminSvc_UserSetCompleted);
                this.UserIdTextBox.Focus();
            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
 
        }
         
        void adminSvc_UserSetCompleted(object sender, UserSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                StatusLabel.Visibility = Visibility.Visible;
                StatusLabel.Content = " User Set did not complete successfully. Please check information and try again";
                return;
            }
            StatusLabel.Visibility = Visibility.Collapsed;
            if (LogOff == false)
            {
                StatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " logged in Successfully.  Password change is NOT required";
                C1_MainMenu.Visibility = Visibility.Visible;
                UserLoginGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                CancelButton.Content = "Log Off";

                this.Visibility = Visibility.Collapsed;

                HtmlPage.Window.Invoke("doCloseLocal", "");
            }
        }

        void adminSvc_UserRolesGetCompleted(object sender, UserRolesGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "UserRoles");

            if ((dtTemp == null) || (dtTemp.Rows.Count == 0))     //NOT and ADMIN role
            {
                string msg = "Invalid Login Attempt";
                string details = "User " + UserIdTextBox.Text.ToString() + " is not authorized for Administration.  Please contact your manager if you think this is not correct";
                string status = "User " + UserIdTextBox.Text.ToString() + " is not authorized for Administration.";
                StatusLabel.Content = status;

                ChildWindow errorWin = new ErrorWindow(msg, details);
                errorWin.Show();

                UserIdTextBox.Text = "";
                PasswordTextBox.Password = "";
                BookGroupCombo.SelectedIndex = 0;

                UserIdTextBox.Focus();

            }
            else
            {
                if (this.Name.Trim().Equals("AdminPortalMain"))
                {
                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        string temp = dr["roleName"].ToString();
                        if (temp.ToUpper().Equals("ADMIN"))
                        {
                            ValidateAdmin();
                            return;
                        }
                    }


                }
                else
                {
                    string userId = appInfo.UserId.ToString();
                    string userPwd = appInfo.Password.ToString();
                    int resetReq = 0;

                    securitySvc.UserValidateAsync(userId, userPwd, "10.64.6.79", resetReq);
                    UserIdTextBox.Focus();
                }
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

            if (appInfo.PWChange)    //BS; Controls which Combo box gets populated
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["BookGroup"].ToString(); 
                }
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    string temp = dr["BookGroup"].ToString(); 
                    this.BookGroupCombo.Items.Add(temp);
                }
                this.BookGroupCombo.SelectedIndex = 0;
                UserIdTextBox.Focus();
            }
            LoginButton.IsEnabled = true;
            UserIdTextBox.Focus();

        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ChildWindow errorWin = new ErrorWindow(e.Uri);
            errorWin.Show();
        }

        private void ClearAppInformation()
        {
            appInfo.LoggedOnBookGroup = "";
            appInfo.CurrentDesk = "";
            appInfo.FullName = "";
            appInfo.InventoryType = "";
            appInfo.Password = "";
            appInfo.Title = "";
            appInfo.UserId = "";
            appInfo.LayoutsValid = false;
            appInfo.LayoutsLoaded = false;
            appInfo.SubscriptionUpdatesReq = false;

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int resetReq = 0;
                string ePwd = EncryptDecrypt.Encrypt(PasswordTextBox.Password.ToString());
                string userId = UserIdTextBox.Text.ToString();
                string bookGroup = BookGroupCombo.Text;

                appInfo.UserId = UserIdTextBox.Text.ToString();
                ((App)App.Current).UserId = UserIdTextBox.Text.ToString();
                appInfo.Password = ePwd.ToString();
                ((App)App.Current).Password = ePwd.ToString();
                appInfo.LoggedOnBookGroup = BookGroupCombo.SelectedItem.ToString();
                ((App)App.Current).LoggedOnBookGroup = BookGroupCombo.SelectedItem.ToString();

                if (this.Name.Trim().Equals("AdminPortalMain"))
                {
                    adminSvc.UserRolesGetAsync(userId, "ADMIN", 0, userId, ePwd, bookGroup, appInfo.DefaultFunctionPath);
                }
                else
                {
                    securitySvc.UserValidateAsync(userId, ePwd, "10.64.6.79", resetReq);
                }
            }

            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
        }

        private void securitySvc_UserValidateCompleted(object sender, UserValidateCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            int resetReq = int.Parse(e.Result.ToString());

            switch (resetReq)
            {
                case 0:
                    {
                        if (this.Name.Trim().Equals("AdminPortalMain"))
                        {
                            string userId = UserIdTextBox.Text.ToString();
                            string bookGroup = BookGroupCombo.Text;
                            string userPassword = appInfo.Password;
                            string functionPath = appInfo.DefaultFunctionPath;
                            bool isLoggedIn = true;
                            bool isLocked = false;
                            bool isActive = true;

                            LogOff = false;
                            adminSvc.UserSetAsync(userId, "", "", "", "", "",isLocked, isActive, isLoggedIn, userId, userPassword, bookGroup, functionPath);
                        }

                    }
                    break;
                case 1:
                    StatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " logged in Successfully.  Password change is required";

                    System.Threading.Thread.Sleep(2500);

                    UserLoginGrid.Visibility = Visibility.Collapsed;
                    PwdChangeGrid.Visibility = Visibility.Visible;

                    break;

                default:
                    StatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " log in Failed.  Please try again";
                    break;
            }
            this.UserIdTextBox.Focus();
        }

        private void ValidateAdmin()
        {
            adminSvc.UserGetCompleted += new EventHandler<UserGetCompletedEventArgs>(adminSvc_UserGetCompleted);
            string uid = UserIdTextBox.Text.ToString();
            adminSvc.UserGetAsync(uid, appInfo.DefaultUserId, appInfo.DefaultPassword, appInfo.DefaultBookGroup, appInfo.DefaultFunctionPath);

        }

        void adminSvc_UserGetCompleted(object sender, UserGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtTemp = new DataTable();
            dtTemp = Functions.ConvertToDataTable(e.Result, "Users");

            if (dtTemp == null)
            {
                string msg = "Invalid Login Attempt";
                string details = "User " + UserIdTextBox.Text.ToString() + " is not authorized for Administration.  Please contact your manager if you think this is not correct";
                string status = "User " + UserIdTextBox.Text.ToString() + " is not authorized for Administration.";
                StatusLabel.Content = status;

                ChildWindow errorWin = new ErrorWindow(msg, details);
                errorWin.Show();

                UserIdTextBox.Text = "";
                PasswordTextBox.Password = "";
                BookGroupCombo.SelectedIndex = 0;

                UserIdTextBox.Focus();
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {

                    string userId = UserIdTextBox.Text;
                    string userPwd = appInfo.Password;
                    int resetReq = 0;

                    DateTime lastAccess = DateTime.Parse(dr["LastAccess"].ToString()).Date;
                    DateTime todayDate = DateTime.Now.Date;
                    
                    int result = DateTime.Compare(lastAccess, todayDate);

                    switch (result)
                    {   //Logged in before today 
                        case -1:
                            securitySvc.UserValidateAsync(userId, userPwd, "10.64.6.79", resetReq);
                            break;

                        case 0:    //Logged in today 
                            string temp = dr["IsLoggedIn"].ToString();
                            if (temp.Equals("True"))
                            {
                                string msg = "User has Exceeded Logins Allowed";
                                string details = "User " + UserIdTextBox.Text.ToString() + " shows already logged in for Administration. " +
                                            " Please contact your manager if you think this is not correct";
                                string status = "User " + UserIdTextBox.Text.ToString() + " is already logged in for Administration.";
                                StatusLabel.Content = status;

                                ChildWindow errorWin = new ErrorWindow(msg, details);
                                errorWin.Show();
                            }
                            else
                            {
                                securitySvc.UserValidateAsync(userId, userPwd, "10.64.6.79", resetReq);
                            }
                            break;

                        default:
                            UserIdTextBox.Text = "";
                            PasswordTextBox.Password = "";
                            BookGroupCombo.SelectedIndex = 0;

                            UserIdTextBox.Focus();
                            break;
                    }    
                }
    
            }

        }

        private void ShowBookGroup()
        {
            try
            {
                bookGroupLabel.Visibility = Visibility.Visible;
                
                BookGroupCombo.Visibility = Visibility.Visible;
                BookGroupCombo.Items.Clear();
                BookGroupCombo.SelectedIndex = -1; 
               
                string bookGroup = appInfo.DefaultBookGroup;

                booksSvc.BookGroupsGetAllAsync(bookGroup, "", appInfo.DefaultUserId, appInfo.DefaultPassword,
                        appInfo.DefaultFunctionPath);
                LoginButton.IsEnabled = false;
                UserIdTextBox.Focus();

            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
                UserIdTextBox.Focus();
            }

        }

        private void LoadPwdChangeGrid()
        {
            ShowPwdChangeBookGroup();
        }

        private void ShowPwdChangeBookGroup()
        {
            try
            {
                UserIdText.Text = appInfo.UserId;

                string bookGroup = appInfo.DefaultBookGroup;

                booksSvc.BookGroupsGetAllAsync(bookGroup, "", appInfo.DefaultUserId, appInfo.DefaultPassword,
                        appInfo.DefaultFunctionPath);
            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userId = UserIdText.Text.ToString();
                string oldEPwd = EncryptDecrypt.Encrypt(OldPasswordBox.Password.ToString());
                string newEPwd = EncryptDecrypt.Encrypt(NewPasswordBox.Password.ToString());
                string newEPwdVerify = EncryptDecrypt.Encrypt(NewPasswordBoxVerify.Password.ToString());

                if (newEPwd.Trim().ToString().Equals(newEPwdVerify.Trim().ToString()))
                {
                    appInfo.PWChange = true;
                    try
                    {
                        securitySvc.UserPasswordChangeAsync(userId, oldEPwd, newEPwd);
                        appInfo.UserId = UserIdTextBox.Text.ToString();
                        appInfo.Password = newEPwd.ToString();

                        functions.SetFunctionPath("AdminPortalMain");
                    }
                    catch (Exception ex)
                    {
                        ChildWindow errorWin = new ErrorWindow(ex);
                        errorWin.Show();
                    }

                }
                else
                {
                    MessageBox.Show("The passwords do not match. Please try again");
                }
            }
            catch(Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }

        }

        void securitySvc_UserPasswordChangeCompleted(object sender, UserPasswordChangeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                PWChangeStatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " password change failed.  Please try again or contact your administrator";
                PwdChangeGrid.Visibility = Visibility.Visible;
                UserIdTextBox.Focus();

                return;
            }

            string result = e.Result.ToString();
       
            switch (result.ToUpper())
            {
                case "TRUE":
                    PWChangeStatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " password changed successfully. Use this for your next login";
                    PwdChangeGrid.Visibility = Visibility.Collapsed;
                    C1_MainMenu.Visibility = Visibility.Visible;

                    break;

                case "FALSE":
                    PWChangeStatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " password change failed.  Please try again or contact your administrator";
                
                    break;
                default:
                    PWChangeStatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " password change failed.  Please try again or contact your administrator";

                    break;
            }
            this.UserIdTextBox.Focus();
        }

        private void BookGroupCombo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string bookGroup = BookGroupCombo.Text.ToString();
            if (!bookGroup.Equals(""))
            {
                appInfo.LoggedOnBookGroup = bookGroup;
            }

        }

        private void BookGroupCombo_SelectedItemChanged(object sender, C1.Silverlight.PropertyChangedEventArgs<object> e)
        {
            string bookGroup = BookGroupCombo.Text.ToString();
            if (!bookGroup.Equals(""))
            {
                appInfo.LoggedOnBookGroup = bookGroup;
            }

        }

        private void C1MenuItem_PasswordReset_Click(object sender, C1.Silverlight.SourcedEventArgs e)
        {
            try
            {
                functions.SetFunctionPath("PASSWORDRESET");
                 ScreenTextBox.Text = ((App)App.Current).ScreenNameText;
                ContentFrame.Navigate(new Uri("/ResetPassword", UriKind.Relative));

            }
            catch (Exception ex)
            {
                functions.SetFunctionPath("ADMINPORTALMAIN");
                ScreenTextBox.Text = ((App)App.Current).ScreenNameText; 
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }

        }

        private void UserMaintenance_Click(object sender, C1.Silverlight.SourcedEventArgs e)
        {
            ((App)App.Current).UserId = appInfo.UserId;
            ((App)App.Current).Password = appInfo.Password;
            ((App)App.Current).FunctionPath = appInfo.FunctionPath;
            ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;

            appInfo.UserMaintType = "NEW";
            functions.SetFunctionPath("MAINTAINUSERS");

            ScreenTextBox.Text = ((App)App.Current).ScreenNameText;

            ContentFrame.Navigate(new Uri("/AddUser", UriKind.RelativeOrAbsolute));
            this.Visibility = Visibility.Visible;
        }

        private void C1MenuItem_RoleFunctionsMaintenance_Click(object sender, C1.Silverlight.SourcedEventArgs e)
        {
            ((App)App.Current).UserId = appInfo.UserId;
            ((App)App.Current).Password = appInfo.Password;
            ((App)App.Current).FunctionPath = appInfo.FunctionPath;
            ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;

            appInfo.UserMaintType = "NEW";
            functions.SetFunctionPath("MAINTAINROLES");

            ScreenTextBox.Text = ((App)App.Current).ScreenNameText;
            ContentFrame.Navigate(new Uri("/RolesMaintain", UriKind.Relative));
        }

        private void C1MenuItem_CurrencyConversionAdmin_Click(object sender, C1.Silverlight.SourcedEventArgs e)
        {
            ((App)App.Current).UserId = appInfo.UserId;
            ((App)App.Current).Password = appInfo.Password;
            ((App)App.Current).FunctionPath = appInfo.FunctionPath;
            ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;

            functions.SetFunctionPath("COUNTRYCURRENCYMAINTENANCE");

            ScreenTextBox.Text = ((App)App.Current).ScreenNameText;
            ContentFrame.Navigate(new Uri("/CountryCurrencyMaintain", UriKind.Relative));
        }

        private void C1MenuItem_HolidaysAdmin_Click(object sender, C1.Silverlight.SourcedEventArgs e)
        {
            ((App)App.Current).UserId = appInfo.UserId;
            ((App)App.Current).Password = appInfo.Password;
            ((App)App.Current).FunctionPath = appInfo.FunctionPath;
            ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;

            functions.SetFunctionPath("HOLIDAYMAINTENANCE");
            ScreenTextBox.Text = ((App)App.Current).ScreenNameText;
            ContentFrame.Navigate(new Uri("/HolidaysMaintain", UriKind.Relative));
        }

        private void C1MenuItem_SubscriptionMaintenance_Click(object sender, C1.Silverlight.SourcedEventArgs e)
        {
            ((App)App.Current).UserId = appInfo.UserId;
            ((App)App.Current).Password = appInfo.Password;
            ((App)App.Current).FunctionPath = appInfo.FunctionPath;
            ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;

            appInfo.UserMaintType = "NEW";
            functions.SetFunctionPath("SUBSCRIPTIONMAINTENANCE");

            ScreenTextBox.Text = ((App)App.Current).ScreenNameText;
            ContentFrame.Navigate(new Uri("/SubscriptionMaint", UriKind.Relative));
        }

        private void C1MenuItem_SubscriptionInformation_Click(object sender, C1.Silverlight.SourcedEventArgs e)
        {
            ((App)App.Current).UserId = appInfo.UserId;
            ((App)App.Current).Password = appInfo.Password;
            ((App)App.Current).FunctionPath = appInfo.FunctionPath;
            ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;

            appInfo.UserMaintType = "NEW";
            functions.SetFunctionPath("SUBSCRIPTIONINFORMATION");

            ScreenTextBox.Text = ((App)App.Current).ScreenNameText;
            ContentFrame.Navigate(new Uri("/SubscriptionInfo", UriKind.Relative));
        }

        private void C1MenuItem_FirmMaintenance_Click(object sender, C1.Silverlight.SourcedEventArgs e)
        {
            ((App)App.Current).UserId = appInfo.UserId;
            ((App)App.Current).Password = appInfo.Password;
            ((App)App.Current).FunctionPath = appInfo.FunctionPath;
            ((App)App.Current).LoggedOnBookGroup = appInfo.LoggedOnBookGroup;

            appInfo.UserMaintType = "NEW";
            functions.SetFunctionPath("FIRMMAINTENANCE");

            ScreenTextBox.Text = ((App)App.Current).ScreenNameText;
            ContentFrame.Navigate(new Uri("/FirmMaint", UriKind.Relative));
        }

        private void ChangePwdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                functions.SetFunctionPath("PASSWORDCHANGE");
                ScreenTextBox.Text = ((App)App.Current).ScreenNameText;

                UserLoginGrid.Visibility = Visibility.Collapsed;

                appInfo.PWChange = true;

                PwdChangeGrid.Visibility = Visibility.Visible;

                LoadPwdChangeGrid();
            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
        }
            
        private void CancelPwdChange_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                functions.SetFunctionPath(this.Name.Trim());
                ScreenTextBox.Text = ((App)App.Current).ScreenNameText;

                ClearAppInformation();
                C1_MainMenu.Visibility = Visibility.Collapsed;
                PwdChangeGrid.Visibility = Visibility.Collapsed;
                UserLoginGrid.Visibility = Visibility.Visible;

                ShowBookGroup();
                UserIdTextBox.Focus();
            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
        }

        private void LogOut_Click(object sender, C1.Silverlight.SourcedEventArgs e)
        {
            try
            {
                if (this.Name.Trim().Equals("AdminPortalMain"))
                {
                    string userId = appInfo.UserId;
                    string bookGroup = appInfo.DefaultBookGroup;
                    string userPassword = appInfo.Password;
                    string functionPath = appInfo.DefaultFunctionPath;
                    bool isLoggedIn = false;
                    bool isLocked = false;
                    bool isActive = true;
                    LogOff = true;
                    adminSvc.UserSetAsync(userId, "", "", "", "", "", isLocked, isActive, isLoggedIn, userId, userPassword, bookGroup, functionPath);
                }

            }
            catch { }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            if (this.Name.Trim().Equals("AdminPortalMain"))
            {
                string userId = appInfo.UserId;
                string bookGroup = appInfo.DefaultBookGroup;
                string userPassword = appInfo.Password;
                string functionPath = appInfo.DefaultFunctionPath;
                bool isLoggedIn = false;
                bool isLocked = false;
                bool isActive = true;

                adminSvc.UserSetAsync(userId, "", "", "", "", "", isLocked, isActive, isLoggedIn, userId, userPassword, bookGroup, functionPath);
            }

            HtmlPage.Window.Invoke("doCloseLocal", "");

        }
        
    }
}