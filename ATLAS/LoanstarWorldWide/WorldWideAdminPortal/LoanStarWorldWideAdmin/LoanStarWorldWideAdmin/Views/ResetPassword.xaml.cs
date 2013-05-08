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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StockLoan.Encryption;
using C1.Silverlight.Data;
using LoanStarWorldWideAdmin;
using LoanStarWorldWideAdmin.SVR_BooksService;
using LoanStarWorldWideAdmin.SVR_SecurityService;

namespace LoanStarWorldWideAdmin
{
    public partial class ResetPassword : UserControl
    {
        public BooksServiceClient booksSvc = new BooksServiceClient();

        public SecurityServiceClient securitySvc = new SecurityServiceClient();
        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();

        public ResetPassword()
        {
            InitializeComponent();

            try
            {
                functions.SetFunctionPath(this.Name.Trim());

                buttonClose.Visibility = Visibility.Collapsed;

                this.Visibility = Visibility.Visible;

                this.UserIdTextBox.Focus();
            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            string userId = UserIdTextBox.Text.ToString();

            appInfo.PWChange = true;
            securitySvc.UserPasswordResetCompleted += new EventHandler<UserPasswordResetCompletedEventArgs>(securitySvc_UserPasswordResetCompleted);
            try
            {   string newEPwd = "";
                securitySvc.UserPasswordResetAsync(userId, newEPwd);
                appInfo.UserId = userId;

                functions.SetFunctionPath("AdminPortalMain");
                    
            }
            catch (Exception ex)
            {
                ChildWindow errorWin = new ErrorWindow(ex);
                errorWin.Show();
            }
        }

        void securitySvc_UserPasswordResetCompleted(object sender, UserPasswordResetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                StatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " password reset failed.  Please confirm ID and try again";
                this.UserIdTextBox.Focus();

                return;
            }

            string result = e.Result.ToString();

            switch (result.ToUpper())
            {
                case "TRUE":
                    StatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " password reset success. Notice will be mailed to user";
                    break;
                case "FALSE":
                    StatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " password reset failed. Please confirm entries and try again";
                    break;
                default:
                    StatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " password reset failed. Please confirm entries and try again";
                    break;
            }

        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {

            functions.SetFunctionPath("ADMINPORTALMAIN");

            this.Visibility = Visibility.Collapsed;

            return;           
            
        }


    }
}