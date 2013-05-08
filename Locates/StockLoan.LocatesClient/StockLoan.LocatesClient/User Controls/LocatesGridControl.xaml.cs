using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;
using C1.Silverlight.Data;
using C1.Silverlight.DataGrid;

using StockLoan_LocatesClient.ServiceLocates;

namespace StockLoan_LocatesClient
{
	public partial class LocatesGridControl : UserControl
	{        
        private LocatesServiceClient lsClient;
        private DataTable dtLocates;
        private string secId;

		public LocatesGridControl()
		{
			InitializeComponent();

            secId = "";
            
            lsClient = new LocatesServiceClient();

            lsClient.LocatesGetCompleted += new EventHandler<LocatesGetCompletedEventArgs>(lsClient_LocatesGetCompleted);
            lsClient.LocateItemGetCompleted += new EventHandler<LocateItemGetCompletedEventArgs>(lsClient_LocateItemGetCompleted);
            CustomEvents.GroupCodeFilterChanged += new EventHandler<GroupCodeFilterEventArgs>(CustomEvents_GroupCodeFilterChanged);              
        }

        void lsClient_LocateItemGetCompleted(object sender, LocateItemGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            DataTable dtLocateItems = Functions.ConvertToDataTable(e.Result, 0);

           try
            {
                if ((!LocatesGrid.DataSourceView.IsEmpty) || dtLocates != null)
                {
                    foreach (DataRow drRow in dtLocateItems.Rows)
                    {
                        ((DataView)LocatesGrid.ItemsSource).Table.Rows.Add(drRow);
                    }
                }
            }
            catch (Exception error)
            {
                SysWindow.Show(error.Message, "lsClient_LocateItemGetCompleted");
            }

            LocatesGrid.Reload(false);
        }

        void CustomEvents_GroupCodeFilterChanged(object sender, GroupCodeFilterEventArgs e)
        {
            if ((bool)CheckBoxFilterGroupCode.IsChecked)
            {
                LocatesGrid.IsLoading = true;
                lsClient.LocatesGetAsync(BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd"), (((bool)CheckBoxFilterGroupCode.IsChecked)?UserInformation.GlobalFilterGroupCode:""), "", 0, (((bool)CheckFilterPending.IsChecked)?"Pending":""));
            }
        }

        void lsClient_LocatesGetCompleted(object sender, LocatesGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            try
            {
                dtLocates = Functions.ConvertToDataTable(e.Result, 0);
                
                LocatesGrid.ItemsSource = dtLocates.DefaultView;                
                LocatesGrid.IsLoading = false;
                LocatesGrid.SelectedIndex = 1;
            }
            catch 
            {
                LocatesGrid.IsLoading = false;
            }
        }

        private void BizDatePicker_DateTimeChanged(object sender, C1.Silverlight.DateTimeEditors.NullablePropertyChangedEventArgs<DateTime> e)
        {
            try
            {
                if (!LocatesGrid.IsLoading)
                {
                    UserInformation.CurrentDate = BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd");
                    lsClient.LocatesGetAsync(BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd"), "", "", 0, "");
                    LocatesGrid.IsLoading = true;
                }
            }
            catch (Exception error)
            {
                SysWindow.Show(error.Message, "BizDatePicker_DateTimeChanged");
            }
        }

        private void LocatesGrid_SelectionChanged(object sender, C1.Silverlight.DataGrid.DataGridSelectionChangedEventArgs e)
        {
            try
            {
                if (!LocatesGrid.IsLoading)
                {

                    secId = LocatesGrid[LocatesGrid.SelectedIndex, 2].Text;
                    CustomEvents.UpdateSecIdInformation(secId);
                    UserInformation.GlobalFilterSecId = secId;
                    UserInformation.GroupCode = LocatesGrid[LocatesGrid.SelectedIndex, 5].Text;
                }
            }
            catch (Exception error)
            {
                //SysWindow.Show(error.Message, "LocatesGrid_SelectionChanged");
            }
        }

        private void ButtonCheckSelected_Click(object sender, System.Windows.RoutedEventArgs e)
        {        
        }

        private void CheckFilterPending_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((!LocatesGrid.IsLoading) && (bool)CheckFilterPending.IsChecked)
            {
       		    UserInformation.CurrentDate = BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd");
               	lsClient.LocatesGetAsync(BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd"), (((bool)CheckBoxFilterGroupCode.IsChecked)?UserInformation.GlobalFilterGroupCode:""), "", 0, (((bool)CheckFilterPending.IsChecked)?"Pending":""));
                LocatesGrid.IsLoading = true;
            }
      
        }

        private void ButtonShowAll_Click(object sender, RoutedEventArgs e)
        {
            if ((!LocatesGrid.IsLoading))
            {
                lsClient.LocatesGetAsync(BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd"), (((bool)CheckBoxFilterGroupCode.IsChecked)?UserInformation.GlobalFilterGroupCode:""), "", 0, (((bool)CheckFilterPending.IsChecked)?"Pending":""));
                LocatesGrid.IsLoading = true;
            }
        }

        private void ButtonProcessSelected_Click(object sender, System.Windows.RoutedEventArgs e)
        {
			
			int count = 0;

			foreach (DataGridRow drRow in LocatesGrid.Selection.SelectedRows)
			{
                if((bool)((DataView)LocatesGrid.ItemsSource).Table.Rows[drRow.Index - 1]["IsFill"] == true)
				{
					count ++;
                }                
			}
								
			count = 0;
			
			foreach (DataGridRow drRow in LocatesGrid.Selection.SelectedRows)
			{
                if((bool) ((DataView)LocatesGrid.ItemsSource).Table.Rows[drRow.Index - 1]["IsFill"] == true)
				{
					count ++;
					CustomEvents.UpdateLocateCountChangeInformation(count);			
					Thread.Sleep(2000);
                }                
			}					
        }

        private void LocatesGrid_CommittedRowEdit(object sender, C1.Silverlight.DataGrid.DataGridRowEventArgs e)
        {
            try
            {
                long locateId = long.Parse(LocatesGrid[LocatesGrid.SelectedIndex, 0].Value.ToString());
                string groupCode = LocatesGrid[LocatesGrid.SelectedIndex, 6].Value.ToString();
                string quantity = LocatesGrid[LocatesGrid.SelectedIndex, 9].Value.ToString();

                lsClient.LocateItemSetAsync(
                   locateId,
                   quantity,
                   "",
                   "",
                   "",
                   "",
                   UserInformation.UserId);

                lsClient.LocateItemGetAsync(groupCode, locateId.ToString(), 0);
            }
            catch (Exception error)
            {
                SysWindow.Show(error.Message, "LocatesGrid_CommittedRowEdit");
            }
        }

        private void LocatesGrid_LoadedCellPresenter(object sender, DataGridCellEventArgs e)
        {
            try
            {
                if ((LocatesGrid.Rows.Count > 0) && !LocatesGrid.IsLoading)
                {
					if (!secId.Equals(LocatesGrid[LocatesGrid.SelectedIndex, 1].Text))
                    {
                        secId = LocatesGrid[LocatesGrid.SelectedIndex, 1].Text;
                        CustomEvents.UpdateSecIdInformation(LocatesGrid[LocatesGrid.SelectedIndex, 1].Text);
                    }
                }
            }
            catch (Exception error)
			{
                //SysWindow.Show(error.Message, "LocatesGrid_LoadedCellPresenter");
			}
        }

        private void LocatesGrid_BeginningRowEdit(object sender, DataGridEditingRowEventArgs e)
        {
            if (LocatesGrid[LocatesGrid.SelectedIndex, 9].Text.Equals("") || LocatesGrid[LocatesGrid.SelectedIndex, 9].Text.Equals("0"))
            {
				e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void CheckFilterPending_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
       	    if ((!LocatesGrid.IsLoading))
            {
                lsClient.LocatesGetAsync(BizDatePicker.DateTime.Value.ToString("yyyy-MM-dd"), (((bool)CheckBoxFilterGroupCode.IsChecked)?UserInformation.GlobalFilterGroupCode:""), "", 0, (((bool)CheckFilterPending.IsChecked)?"Pending":""));
                LocatesGrid.IsLoading = true;
            }
        }

        private void ButtonExportToExcel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
			string exportFileName = "";

            this.Cursor = Cursors.Wait;

        	exportFileName = Export.Excel(LocatesGrid);
			
			if (!exportFileName.Equals(""))
			{
				SysWindow.Show("File Export to : " + exportFileName + " was successful.", "");
			}
			else
			{
				SysWindow.Show("File Export failed.", "");
			}
			
            this.Cursor = Cursors.Arrow;
        }   
    }
}