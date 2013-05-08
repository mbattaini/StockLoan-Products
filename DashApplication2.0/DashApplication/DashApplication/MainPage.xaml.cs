using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

using C1.Silverlight;

using DashApplication.CustomClasses;

namespace DashApplication
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();

            CustomEvents.PageChanged += CustomEvents_PageChanged;
            CustomEvents.DetailChanged += new EventHandler<DetailEventArgs>(CustomEvents_DetailChanged);

            CustomEvents.UpdatePageNavigation("Available Applications", @"/DashApplication;component/UserPages/PageApps.xaml");
        }

        void CustomEvents_PageChanged(object sender, PageEventArgs e)
        {
            ApplicationNameTextBlock.Text = e.Title;
            ContentFrame.Navigate(new Uri(e.Uri, UriKind.Relative));       
        }

        void CustomEvents_DetailChanged(object sender, DetailEventArgs e)
        {	
		}         

        private void ContentFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {            
            foreach (UIElement child in LinksStackPanel.Children)
            {
                HyperlinkButton hb = child as HyperlinkButton;
                if (hb != null && hb.NavigateUri != null)
                {
                    if (ContentFrame.UriMapper.MapUri(e.Uri).ToString().Equals(ContentFrame.UriMapper.MapUri(hb.NavigateUri).ToString()))
                    {
                        VisualStateManager.GoToState(hb, "ActiveLink", true);
                    }
                    else
                    {
                        VisualStateManager.GoToState(hb, "InactiveLink", true);
                    }
                }
            }
        }

        private void ContentFrame_NavigationFailed(object sender, System.Windows.Navigation.NavigationFailedEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(e.Exception.Message);            
        }

        private void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            CustomEvents.UpdatePageNavigation("Available Applications", @"/DashApplication;component/UserPages/PageApps.xaml");
        }

        private void BrandingBorder_MouseEnter(object sender, MouseEventArgs e)
        {         
        }        
	}
}