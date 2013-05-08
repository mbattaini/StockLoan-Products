using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight;

using WorldWideClient.ServiceSecurity;
using StockLoan.Encryption;

namespace WorldWideClient
{
	public partial class LoginControl : UserControl
	{		
		private	SecurityServiceClient securityServiceClient;
		
		public LoginControl()
		{
			InitializeComponent();			
			
			securityServiceClient = new SecurityServiceClient();

            securityServiceClient.UserValidateCompleted += new EventHandler<UserValidateCompletedEventArgs>(securityServiceClient_UserValidateCompleted);
		}

        void securityServiceClient_UserValidateCompleted(object sender, UserValidateCompletedEventArgs e)
        {
			this.Cursor = Cursors.Arrow;
			
            if (e.Error != null)
			{
				SystemEventWindow.Show(e.Error.Message);
				return;
			}
			
			if (bool.Parse(e.Result.ToString()))
			{
				UserInformation.UserId = UserIdTextBox.Text;
				UserInformation.Password = EncryptDecrypt.Encrypt(PasswordTextBox.Password);
                						
				StatusLabel.Foreground = new SolidColorBrush(Colors.Green);
				StatusLabel.Content = "User Logged In!";
				
			}				
			else
			{
				StatusLabel.Foreground = new SolidColorBrush(Colors.Red);
				StatusLabel.Content = "User ID / Password Incorrect.";
			}
        }

		private void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Cursor = Cursors.Wait;			
			securityServiceClient.UserValidateAsync(UserIdTextBox.Text, EncryptDecrypt.Encrypt(PasswordTextBox.Password), false);
		}
	}
}