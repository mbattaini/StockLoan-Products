using System;
using System.Collections.Generic;
using System.Windows;
using C1.Silverlight.Data;
using System.Windows.Controls;
using C1.Silverlight;

using LoanStarWorldWideAdmin;
using LoanStarWorldWideAdmin.SVR_InventoryService;

namespace LoanStarWorldWideAdmin.Views
{
    public partial class FirmMaintAddWindow : UserControl
    {
        public InventoryServiceClient inventorySvc = new InventoryServiceClient();
        public AppInformation appInfo = new AppInformation();
        public Functions functions = new Functions();

        public FirmMaintAddWindow()
        {
            InitializeComponent();

            functions.SetFunctionPath(this.Name.Trim());

            StatusLabel.Visibility = Visibility.Collapsed;

            inventorySvc.FirmSetCompleted += new EventHandler<FirmSetCompletedEventArgs>(inventorySvc_FirmSetCompleted);

            appInfo.UserId = ((App)App.Current).UserId;
            appInfo.Password = ((App)App.Current).Password;
            appInfo.FunctionPath = ((App)App.Current).FunctionPath;
            appInfo.LoggedOnBookGroup = ((App)App.Current).LoggedOnBookGroup;

            string firmCode = ((App)App.Current).FirmCode;
            string firmName = ((App)App.Current).FirmName;
            string isActive = ((App)App.Current).IsActive;

            firmCodeText.Text = firmCode;
            firmNameText.Text = firmName;
            if (isActive.ToLower().ToString().Equals("false"))
            {
                isActivecheckBox.IsChecked = false;
            }
            else
            { 
                isActivecheckBox.IsChecked = true; 
            }
        }

        void inventorySvc_FirmSetCompleted(object sender, FirmSetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                StatusLabel.Content = "Firm " + firmNameText.Text + " Add/Update Failed";
                return;
            }
            else
            {
                this.StatusLabel.Content = "Firm " + firmNameText.Text + " Added/Updated Successfully";
                this.StatusLabel.Visibility = Visibility.Visible;
                this.FirmAddButton.Visibility = Visibility.Collapsed;
            }
        }

        private void FirmAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDetails())
            {
                bool isActive = isActivecheckBox.IsChecked.Value;
                string firmCode = firmCodeText.Text;
                string firmName = firmNameText.Text;

                string userId = appInfo.UserId;
                string userPassword = appInfo.Password;
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
                   
                inventorySvc.FirmSetAsync(firmCode, firmName, isActive.ToString(), 
                        userId, userPassword, bookGroup, functionPath);
            }
        }

        private bool ValidateDetails()
        {
            bool isValid = false;
            if (!firmCodeText.Text.Trim().Equals("") && !firmNameText.Text.Trim().Equals(""))
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
