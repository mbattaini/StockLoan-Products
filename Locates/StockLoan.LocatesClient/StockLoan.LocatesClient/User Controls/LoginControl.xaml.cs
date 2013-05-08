using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using StockLoan_LocatesClient.ServiceLocates;

namespace StockLoan_LocatesClient
{
	public partial class LoginControl : UserControl
	{
        private LocatesServiceClient lsClient;

        public LoginControl()
        {
            InitializeComponent();

            lsClient = new LocatesServiceClient();
            lsClient.WebUserAuthorizeCompleted += new EventHandler<WebUserAuthorizeCompletedEventArgs>(lsClient_WebUserAuthorizeCompleted);
        }

        void lsClient_WebUserAuthorizeCompleted(object sender, WebUserAuthorizeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                StatusLabel.Content = "Error in the system";
            }

            if ((bool)e.Result)
            {
                UserInformation.UserId = UserIdTextBox.Text;
                UserInformation.Password = PasswordTextBox.Password;
                UserInformation.AllowUser = true;

                StatusLabel.Content = "Login Successful!";
                CustomEvents.UpdateUserInformation(UserIdTextBox.Text, false);

                ((C1.Silverlight.C1Window)this.Parent).Close();
            }
            else
            {
                StatusLabel.Content = "Login Failed :(";
                CustomEvents.UpdateUserInformation("", true);
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            lsClient.WebUserAuthorizeAsync(UserIdTextBox.Text, PasswordTextBox.Password);
        }
	}
}