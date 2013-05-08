using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using C1.Silverlight;


using DashApplication.ServicePosition;

namespace DashApplication
{
	public class ContractsChildWindowWrapper
	{
		private string bizDate;
		private string bookGroup;
		private string secId;
		private string currencyIso;

		public PositionClient posClient;
		public C1.Silverlight.DataGrid.C1DataGrid ContractDetailsGrid;
		
        public ContractsChildWindowWrapper(string bizDate, string bookGroup, string secId, string currencyIso, Locale localType)
        {            			
			this.bizDate = bizDate;
			this.bookGroup = bookGroup;
			this.secId = secId;
			this.currencyIso = currencyIso;
			
			posClient = new PositionClient();
			posClient.ContractDataGetCompleted +=new EventHandler<ContractDataGetCompletedEventArgs>(posClient_ContractDataGetCompleted);
         					
			posClient.ContractDataGetAsync(bizDate, bookGroup, secId,  UserInformation.UserId, "Reporting");        			
		}

		public void ContractsDetailsGridSetup()
		{           
            ContractDetailsGrid = new C1.Silverlight.DataGrid.C1DataGrid();
            
			ContractDetailsGrid.AutoGenerateColumns = false;
            ContractDetailsGrid.IsReadOnly = true;
						
            C1.Silverlight.DataGrid.DataGridBoundColumn dc = new C1.Silverlight.DataGrid.DataGridBoundColumn();
            dc.Header = "Business Date";
            dc.Binding = new System.Windows.Data.Binding("BizDate");
            dc.Format = "yyyy-MM-dd";			
            ContractDetailsGrid.Columns.Add(dc);

            dc = new C1.Silverlight.DataGrid.DataGridBoundColumn();
            dc.Header = "Book Group";
            dc.Binding = new System.Windows.Data.Binding("BookGroup");            
            ContractDetailsGrid.Columns.Add(dc);

            dc = new C1.Silverlight.DataGrid.DataGridBoundColumn();
            dc.Header = "Book";
            dc.Binding = new System.Windows.Data.Binding("Book");            
            ContractDetailsGrid.Columns.Add(dc);

            dc = new C1.Silverlight.DataGrid.DataGridBoundColumn();
            dc.Header = "Security ID";            
            dc.Binding = new System.Windows.Data.Binding("SecId");            
            ContractDetailsGrid.Columns.Add(dc);

            dc = new C1.Silverlight.DataGrid.DataGridBoundColumn();
            dc.Header = "Symbol";
            dc.Binding = new System.Windows.Data.Binding("Symbol");            
            ContractDetailsGrid.Columns.Add(dc);

			   dc = new C1.Silverlight.DataGrid.DataGridBoundColumn();
            dc.Header = "T";
            dc.Binding = new System.Windows.Data.Binding("ContractType");
            dc.Format = "#,##0";
            dc.HorizontalAlignment = HorizontalAlignment.Center;
            ContractDetailsGrid.Columns.Add(dc);
			
            dc = new C1.Silverlight.DataGrid.DataGridBoundColumn();
            dc.Header = "Quantity";
            dc.Binding = new System.Windows.Data.Binding("Quantity");
            dc.Format = "#,##0";
            dc.HorizontalAlignment = HorizontalAlignment.Right;
            ContractDetailsGrid.Columns.Add(dc);

            dc = new C1.Silverlight.DataGrid.DataGridBoundColumn();
            dc.Header = "Amount";
            dc.Binding = new System.Windows.Data.Binding("Amount");
            dc.Format = "#,##0.00";
            dc.HorizontalAlignment = HorizontalAlignment.Right;
            ContractDetailsGrid.Columns.Add(dc);		
			
            dc = new C1.Silverlight.DataGrid.DataGridBoundColumn();
            dc.Header = "Rate";
            dc.Binding = new System.Windows.Data.Binding("Rate");
            dc.Format = "00.000";
            dc.HorizontalAlignment = HorizontalAlignment.Right;
            ContractDetailsGrid.Columns.Add(dc);
			
			  dc = new C1.Silverlight.DataGrid.DataGridBoundColumn();
            dc.Header = "Margin";
            dc.Binding = new System.Windows.Data.Binding("Margin");
            dc.Format = "00.00";
            dc.HorizontalAlignment = HorizontalAlignment.Right;
            ContractDetailsGrid.Columns.Add(dc);
		}
		
        public void posClient_ContractDataGetCompleted(object sender, ContractDataGetCompletedEventArgs eArgs)
        {
            if (eArgs.Error != null)
            {
                return;
            }
            
            ContractsDetailsGridSetup();
            ContractDetailsGrid.ItemsSource  = Functions.ConvertToDataTable(eArgs.Result, "Contracts").DefaultView;
            
			FloatableWindow fltWindow = new FloatableWindow();
			fltWindow.Title = "Contract Details For " + DateTime.Parse(bizDate).ToString("yyyy-MM-dd");			
			fltWindow.Content = ContractDetailsGrid;		
			fltWindow.Height = 300;			
			ContractDetailsGrid.Visibility = Visibility.Visible;
            
			fltWindow.Show();
        }
	}
}