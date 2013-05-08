using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using WorldWideClient.ServiceSecurity;
using StockLoan.Encryption;

namespace WorldWideClient
{
	public partial class UserMaintenanceControl : UserControl
	{
		private SecurityServiceClient securityServiceClient;
		
		public UserMaintenanceControl()
		{
			// Required to initialize variables
			InitializeComponent();			
			securityServiceClient = new SecurityServiceClient();
            securityServiceClient.UserPasswordChangeCompleted += new EventHandler<UserPasswordChangeCompletedEventArgs>(securityServiceClient_UserPasswordChangeCompleted);
						
			UserIdTextBox.Text = UserInformation.UserId;
			PasswordTextBox.Password = UserInformation.Password;
		}

        void securityServiceClient_UserPasswordChangeCompleted(object sender, UserPasswordChangeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
				return;
			}

            if (e.Result)
            {
				StatusLabel.Foreground = new SolidColorBrush(Colors.Green);
				StatusLabel.Content = "Password changed succesfully!";
        		UserInformation.Password = EncryptDecrypt.Encrypt(PasswordChangeTextBox.Password);
			}
            else
            {
				StatusLabel.Foreground = new SolidColorBrush(Colors.Red);
				StatusLabel.Content = "Password change was unsuccesful. :(";
            }
        }

		private void PasswordChangeCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
				PasswordChangeTextBox.IsEnabled = ! (bool)PasswordChangeCheckBox.IsChecked;			
		}

		private void Savebutton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            securityServiceClient.UserPasswordChangeAsync(UserIdTextBox.Text, PasswordTextBox.Password, EncryptDecrypt.Encrypt(PasswordChangeTextBox.Password));
		}
	}
}