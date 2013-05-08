using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


using C1.Silverlight;
using C1.Silverlight.Excel;
using C1.Silverlight.Data;
using C1.Silverlight.DataGrid;
using C1.Silverlight.DataGrid.Summaries;

using DashApplication.ServicePosition;

namespace DashApplication
{
    public partial class BorrowsMorningControl : UserControl
    {
        private PositionClient psClient;

        public BorrowsMorningControl()
        {
            InitializeComponent();

            psClient = new PositionClient();
            psClient.BorrowsMorningSetCompleted += psClient_BorrowsMorningSetCompleted;
            psClient.BorrowsMorningGetCompleted += psClient_BorrowsMorningGetCompleted;
            psClient.BorrowsMorningPurgeCompleted += psClient_BorrowsMorningPurgeCompleted;
            BizDatePicker.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        void psClient_BorrowsMorningPurgeCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            C1MessageBox.Show("Sucessfulyl removed list for : " + BizDatePicker.Text);

            psClient.BorrowsMorningGetAsync(BizDatePicker.Text);
            BorrowsGrid.IsLoading = true;
        }

        void psClient_BorrowsMorningGetCompleted(object sender, BorrowsMorningGetCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                DataTable dtBorrows = Functions.ConvertToDataTable(e.Result, "Borrows");
                long tem = dtBorrows.Rows.Count;
                BorrowsGrid.ItemsSource = dtBorrows.DefaultView;
            }

            BorrowsGrid.IsLoading = false;
        }

        void psClient_BorrowsMorningSetCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }

        private void UploadListButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog oDialog1 = new OpenFileDialog();
                oDialog1.Filter = "Excel Files (.xlsx)|*.xlsx|All Files (*.*)|*.*";
                oDialog1.Multiselect = false;
                oDialog1.FilterIndex = 1;

                
                
                bool? usrClickedOk = oDialog1.ShowDialog();

                
                
                if (usrClickedOk == true)
                {
                    BorrowsGrid.IsLoading = true;

                    DataTable dtList = new DataTable();
                    dtList.Columns.Add("SecId");
                    dtList.Columns.Add("Symbol");
                    dtList.Columns.Add("Quantity");
                    dtList.AcceptChanges();

                    C1XLBook xlBook = new C1XLBook();
                    xlBook.Load(oDialog1.File.OpenRead());

                    for (int counter = 0; counter < xlBook.Sheets[0].Rows.Count; counter++)
                    {
                        DataRow dr = dtList.NewRow();
                        dr["SecId"] = xlBook.Sheets[0][counter, 0].Text;
                        dr["Symbol"] = xlBook.Sheets[0][counter, 1].Text;
                        dr["Quantity"] = xlBook.Sheets[0][counter, 2].Text;
                        dtList.Rows.Add(dr);
                    }

                    dtList.AcceptChanges();

                    foreach (DataRow drRow in dtList.Rows)
                    {
                        psClient.BorrowsMorningSetAsync(BizDatePicker.Text, "0158", drRow["SecId"].ToString(), drRow["Quantity"].ToString());
                    }

                    psClient.BorrowsMorningGetAsync(BizDatePicker.Text);
                    BorrowsGrid.IsLoading = false;
                }
            }
            catch (Exception error)
            {
                C1MessageBox.Show(error.Message);
                BorrowsGrid.IsLoading = false;
            }
           
        }

        private void UploadFileButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void LoadListButton_Click(object sender, RoutedEventArgs e)
        {
            psClient.BorrowsMorningGetAsync(BizDatePicker.Text);
        }

        private void PurgeListButton_Click(object sender, RoutedEventArgs e)
        {
            psClient.BorrowsMorningPurgeAsync(BizDatePicker.Text);
        }       
    }
}
