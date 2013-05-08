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
using System.Windows.Shapes;
using System.Windows.Navigation;

using ShortSaleLocatesClient.ServiceLocate;

namespace ShortSaleLocatesClient
{
    public partial class MainPage : Page
    {
        public LocateServiceClient locateServiceClient;

        public MainPage()
        {
            InitializeComponent();

            locateServiceClient = new LocateServiceClient();
            locateServiceClient.LocateResearchGetCompleted += new EventHandler<LocateResearchGetCompletedEventArgs>(locateServiceClient_LocateResearchGetCompleted);
        }

        void locateServiceClient_LocateResearchGetCompleted(object sender, LocateResearchGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

        //    LocateGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, "Locates").DefaultView;
        }
       
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {            
        }

        private void LayoutRoot_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
        	
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
        ToolFrame.Navigate(new Uri(@"/ResearchControl.xaml", UriKind.Relative));
        }
    }
}
