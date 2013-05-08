using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using C1.Silverlight;
using C1.Silverlight.Data;
using C1.Silverlight.DataGrid;
using C1.Silverlight.DataGrid.Filters;
using WorldWideClient.ServiceDeals;

namespace WorldWideClient
{
	public partial class PositionContractBlotterControl : UserControl
	{
        private DealsServiceClient dealClient;

		public PositionContractBlotterControl()
		{
			InitializeComponent();

            dealClient = new DealsServiceClient();
            dealClient.DealsGetCompleted += new EventHandler<DealsGetCompletedEventArgs>(dealClient_DealsGetCompleted); 
			
			contractBlotterBookGroupToolBar.FunctionName = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name;

            CustomEvents.UserBookGroupInformationChanged += new EventHandler<UserBookGroupEventArgs>(CustomEvents_UserBookGroupInformationChanged);
        }

        void CustomEvents_UserBookGroupInformationChanged(object sender, UserBookGroupEventArgs e)
        {
            DealGrid.IsLoading = true;

            dealClient.DealsGetAsync(
                contractBlotterBookGroupToolBar.BusinessDate,
                "", 
                "",
                true,
                0,
                UserInformation.UserId,
                UserInformation.Password,
                contractBlotterBookGroupToolBar.BookGroup,
                System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name);
        }

        void dealClient_DealsGetCompleted(object sender, DealsGetCompletedEventArgs e)
        {
            DealGrid.IsLoading = false;

            if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
                return;
            }

            DataTable dtDeals = Functions.ConvertToDataTable(e.Result, "Deals");
            DealGrid.ItemsSource = dtDeals.DefaultView;
        }
		
		private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {       				
			DealGrid.TopRows.Clear();
			DealGrid.TopRows.Add(new DataGridFilterRow());			
			DealGrid.Reload(false);           
        }
	}
}