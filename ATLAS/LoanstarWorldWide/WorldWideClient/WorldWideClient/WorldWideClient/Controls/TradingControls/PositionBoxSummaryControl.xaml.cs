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
using C1.Silverlight.Data;
using C1.Silverlight.DataGrid;
using C1.Silverlight.DataGrid.Filters;
using WorldWideClient.ServicePositions;


namespace WorldWideClient
{
	public partial class PositionBoxSummaryControl : UserControl
	{
		private PositionsServiceClient positionsClient;
        private int controlHndle;

		public PositionBoxSummaryControl()
		{		
			InitializeComponent();

            controlHndle = Functions.CreateControlHandle();

            BoxSummaryBookGroupToolBar.ShowBusinessDate = true;
			BoxSummaryBookGroupToolBar.ShowBookCombo = false;
            BoxSummaryBookGroupToolBar.ParentControlHandle = controlHndle;
            BoxSummaryBookGroupToolBar.FunctionName = System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name;
			
            positionsClient = new PositionsServiceClient();
            positionsClient.BoxSummaryDataConfigGetCompleted += new EventHandler<BoxSummaryDataConfigGetCompletedEventArgs>(positionsClient_BoxSummaryDataConfigGetCompleted);

            CustomEvents.ToolBarInformationChanged += new EventHandler<ToolBarChangeEventArgs>(CustomEvents_ToolBarInformationChanged);
            
        }

        void CustomEvents_ToolBarInformationChanged(object sender, ToolBarChangeEventArgs e)
        {
            if ((!e.BizDate.Equals("") && !e.BookGroup.ToString().Equals("")) && (controlHndle == e.ControlHandle))
            {
                positionsClient.BoxSummaryDataConfigGetAsync(e.BizDate, e.BookGroup, false, "10", UserInformation.UserId, UserInformation.Password, System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name);
            	InputGrid.IsLoading = true;
			}
        }

        void positionsClient_BoxSummaryDataConfigGetCompleted(object sender, BoxSummaryDataConfigGetCompletedEventArgs e)
        {
			InputGrid.IsLoading = false;
			
            if (e.Error != null)
            {
                SystemEventWindow.Show(e.Error.Message);
                return;
            }

            InputGrid.ItemsSource = Functions.ConvertToDataTable(e.Result, "BoxSummary").DefaultView;
        }
	
		  private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {       				
			InputGrid.TopRows.Clear();
			InputGrid.TopRows.Add(new DataGridFilterRow());			
			InputGrid.Reload(false);           
        }        
    }
}