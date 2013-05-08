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
        
        }

        // After the Frame navigates, ensure the HyperlinkButton representing the current page is selected
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            /*foreach (UIElement child in LinksStackPanel.Children)
            {
                HyperlinkButton hb = child as HyperlinkButton;
                if (hb != null && hb.NavigateUri != null)
                {
                    if (hb.NavigateUri.ToString().Equals(e.Uri.ToString()))
                    {
                        VisualStateManager.GoToState(hb, "ActiveLink", true);
                    }
                    else
                    {
                        VisualStateManager.GoToState(hb, "InactiveLink", true);
                    }
                }
            }*/
        }
		
		void CustomEvents_PageChanged(object sender, PageEventArgs e)
        {
            ContentFrame.Navigate(new Uri(e.Uri, UriKind.Relative));
        }

        void CustomEvents_DetailChanged(object sender, DetailEventArgs e)
        {			
            /*C1TileViewItem tileViewItem = new C1TileViewItem();
            tileViewItem.Content = new CollateralUtilizationDetailControl(e.BizDate, e.ClassGroup);
            tileViewItem.Header = "Collateral Detail - " + e.ClassGroup;            */
		}
       
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            
        }


        private void ImageApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContentFrame.Navigate(new Uri(@"/DashApplication;component/User Pages/PageApps.xaml", UriKind.Relative));
        }

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double widthNew = e.NewSize.Width * .75;
            double heightNew = e.NewSize.Height * .75;

            ContentFrame.Width = widthNew;
            ContentFrame.Height = heightNew;

            NavigationGrid.Width = widthNew;
        }


    }
}