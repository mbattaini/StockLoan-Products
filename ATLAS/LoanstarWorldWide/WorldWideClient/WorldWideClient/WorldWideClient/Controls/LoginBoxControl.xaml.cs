using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight.Data;

using WorldWideClient.ServiceAdmin;
using WorldWideClient.ServiceSecurity;
using WorldWideClient.ServiceBooks;

using StockLoan.Encryption;

namespace WorldWideClient
{
	public partial class LoginBoxControl : UserControl
	{
		private string userId;
		private string password;
		private string bookGroup;

		private DataSet dsBookGroups;
		
		private ServiceAdmin.AdminServiceClient adminClient;
		private ServiceBooks.BooksServiceClient bookClient;		
		private ServiceSecurity.SecurityServiceClient securityClient;

        public LoginBoxControl()
        {
            InitializeComponent();

			dsBookGroups = new DataSet();
            
			adminClient = new ServiceAdmin.AdminServiceClient();
            bookClient = new ServiceBooks.BooksServiceClient();
            securityClient = new ServiceSecurity.SecurityServiceClient();

            bookClient.BookGroupsGetAllCompleted += new EventHandler<BookGroupsGetAllCompletedEventArgs>(bookClient_BookGroupsGetAllCompleted);
            bookClient.BookGroupsGetAllAsync(
                "",
                "",
                "",
                "",
                "");
        }

        void bookClient_BookGroupsGetAllCompleted(object sender, BookGroupsGetAllCompletedEventArgs e)
        {
            if (e.Error != null)
            {
				return;
            }
			else
			{
				dsBookGroups.Tables.Add(Functions.ConvertToDataTable(e.Result, "BookGroups"));
        		BookGroupDropDown.DataContext = dsBookGroups.Tables["BooKGroups"];
			}
		}

		private void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			userId = UserIdTextBox.Text;
			password =  EncryptDecrypt.Encrypt(PasswordTextBox.Text);
			bookGroup = BookGroupDropDown.Content.ToString();

            securityClient.UserValidateCompleted += new EventHandler<UserValidateCompletedEventArgs>(securityClient_UserValidateCompleted);
			securityClient.UserValidateAsync(userId, password, "", 0);
		}

        void securityClient_UserValidateCompleted(object sender, UserValidateCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }
			else
			{			
            	UserInformation.UserId = userId;
            	UserInformation.Password = password;
            	UserInformation.BookGroup = bookGroup;

            	this.Visibility = System.Windows.Visibility.Collapsed;
			}
        }		
	}
}