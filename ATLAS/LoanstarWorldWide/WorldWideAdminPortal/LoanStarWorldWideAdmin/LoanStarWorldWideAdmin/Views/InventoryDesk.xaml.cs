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

using C1.Silverlight;
//using C1.Silverlight.Data;

namespace DashApplication
{
    public partial class InventoryDesk : Page
    {
		
		public InventoryDesk()
        {
            InitializeComponent();

            DeskDatePicker.DateTime = DateTime.Now;
            DeskLoad();
        }

       
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void DeskLoad()
        {
			try
			{
			}
			catch {}
        }

        private void DeskDatePicker_DateTimeChanged(object sender, C1.Silverlight.DateTimeEditors.NullablePropertyChangedEventArgs<System.DateTime> e)
        {
            DeskLoad();
            InventoryGrid.ItemsSource = null;
        }

        private void DeskGrid_SelectionChanged(object sender, C1.Silverlight.DataGrid.DataGridSelectionChangedEventArgs e)
        {
            try
            {
            }
            catch { }
        }
    }
}




