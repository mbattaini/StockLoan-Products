using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace StockLoan_LocatesClient
{
	public partial class LocateProcessingControl : UserControl
	{
		public LocateProcessingControl(int locatesTotal)
		{			
			InitializeComponent();
			
			LocatesTotalLabel.Content = locatesTotal.ToString("#,##0");
			StatusLabel.Content = "Locates Processing...";
            
            LocatesProgressbar.Minimum = 0;
            LocatesProgressbar.Maximum = locatesTotal;			         						
        }

        void CustomEvents_LocateCountChanged(object sender, LocateCountChangeEventArgs e)
        {
            LocatesProcessedLabel.Content = e.Index.ToString("#,##0");
            LocatesProgressbar.Value = e.Index;

            if (LocatesProgressbar.Value == LocatesProgressbar.Maximum)
            {
                StatusLabel.Content = "Locates Processing Completed";
            }
        }

        private void Grid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
        	CustomEvents.LocateCountChanged += new EventHandler<LocateCountChangeEventArgs>(CustomEvents_LocateCountChanged);
        }				
	}
}