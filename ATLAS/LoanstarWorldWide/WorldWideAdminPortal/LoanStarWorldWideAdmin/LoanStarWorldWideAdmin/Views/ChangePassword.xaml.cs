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
using LoanStarWorldWideAdmin.SVR_SecurityService;
using LoanStarWorldWideAdmin.SVR_BooksService;

namespace LoanStarWorldWideAdmin
{
    public partial class ChangePassword : UserControl
    {
        public BooksServiceClient booksSvc = new BooksServiceClient();

        public ChangePassword()
        {
            InitializeComponent();

            try
            {
                Functions.SetFunctionPath(this.Name.Trim()); 

                UserIdTextBox.Text = AppInformation.UserId;
                this.UserIdTextBox.Focus();

                booksSvc.BookGroupsGetAllCompleted += new EventHandler<BookGroupsGetAllCompletedEventArgs>(booksSvc_BookGroupsGetAllCompleted);
                ShowBookGroup();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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

            foreach (DataRow dr in dtTemp.Rows)
            {
                string temp = dr["BookGroup"].ToString(); // +" | " + dr["BookName"].ToString();
                this.BookGroupCombo.Items.Add(temp);
            }
            this.BookGroupCombo.SelectedIndex = 0;
        }

        // If an error occurs during navigation, show an error window
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ChildWindow errorWin = new ErrorWindow(e.Uri);
            errorWin.Show();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            string userId = UserIdTextBox.Text.ToString();
            string oldEPwd = EncryptDecrypt.Encrypt(OldPasswordBox.Password.ToString());
            string newEPwd = EncryptDecrypt.Encrypt(NewPasswordBox.Password.ToString());
            string newEPwdVerify = EncryptDecrypt.Encrypt(NewPasswordBoxVerify.Password.ToString());

            if (newEPwd.Trim().ToString().Equals(newEPwdVerify.Trim().ToString()))
            {
                SecurityServiceClient securitySvc = new SecurityServiceClient();
                securitySvc.UserPasswordChangeCompleted += new EventHandler<UserPasswordChangeCompletedEventArgs>(securitySvc_UserPasswordChangeCompleted);
                try
                {
                    securitySvc.UserPasswordChangeAsync(userId, oldEPwd, newEPwd);
                    AppInformation.UserId = UserIdTextBox.Text.ToString();
                    AppInformation.Password = newEPwd.ToString();
                    this.Visibility = Visibility.Collapsed;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

            }
            else
            {
                MessageBox.Show("The passwords do not match. Please try again");
            }

        }

        void securitySvc_UserPasswordChangeCompleted(object sender, UserPasswordChangeCompletedEventArgs e)
        {
            string result = e.Result.ToString();

            switch (result.ToUpper())
            {
                case "TRUE":
                    StatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " password changed successfully. Use this for your next login";
                    PwdResetGrid.Visibility = Visibility.Collapsed;

                    break;
                case "FALSE":
                    StatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " password change failed.  Please try again or contact your administrator";

                    break;
                default:
                    StatusLabel.Content = "User " + UserIdTextBox.Text.ToString() + " password change failed.  Please try again or contact your administrator";
                    break;

            }

        }


        private void ShowBookGroup()
        {
            bookGroupLabel.Visibility = Visibility.Visible;
            BookGroupCombo.Visibility = Visibility.Visible;
            string bookGroup = AppInformation.DefaultBookGroup;

            booksSvc.BookGroupsGetAllAsync(bookGroup, "", AppInformation.DefaultUserId, AppInformation.DefaultPassword, 
                    AppInformation.DefaultFunctionPath);

        }


    }
}