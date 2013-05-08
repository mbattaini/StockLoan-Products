using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;

using C1.Silverlight;
using C1.Silverlight.Imaging;

namespace WorldWideClient
{
	public partial class UserIdControl : UserControl
	{				
		private string activeImage = "/WorldWideClient;component/Images/Status-online.png";
		private string unactiveImage = "/WorldWideClient;component/Images/Status-Offline.png";

        public UserIdControl()
		{
			InitializeComponent();			
            		            
            CustomEvents.UserInformationChanged += new EventHandler<UserEventArgs>(UserInformation_UserInformationChanged);
        }

        void UserInformation_UserInformationChanged(object sender, UserEventArgs e)
        {
			UserIdTextBox.Text = e.UserId;
			
            if (e.LogOffUser)
			{							    					
            	StatusImage.Source = new C1.Silverlight.Imaging.ImageSourceConverter().ConvertFromString(unactiveImage) as ImageSource; 
			}
			else
			{
				StatusImage.Source = new C1.Silverlight.Imaging.ImageSourceConverter().ConvertFromString(activeImage) as ImageSource; 
			}			
        }

        private void StatusImage_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	ClientWindow _window = new ClientWindow();
			_window.Resize = false;
			_window.WindowHeader = "User Maintenance";
			_window.Content = new UserMaintenanceControl();
			_window.Show();
			_window.CenterOnScreen();
        }

        private void FontSizeNumericBox_ValueChanged(object sender, C1.Silverlight.PropertyChangedEventArgs<double> e)
        {
        	
        }
	}
}