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

using C1.Silverlight;
using C1.Silverlight.Data;
using C1.Silverlight.Chart;
using DashApplication.ServicePosition;

namespace DashApplication
{
    public partial class ReportViewerControl : UserControl
    {
        public PositionClient posClient = null;
        private DataTable dtReports = null;
		
        public ReportViewerControl()
        {
            InitializeComponent();

            posClient = new PositionClient();
            posClient.FileReaderCompleted += new EventHandler<FileReaderCompletedEventArgs>(posClient_FileReaderCompleted);
            posClient.WebReportsGetCompleted += new EventHandler<WebReportsGetCompletedEventArgs>(posClient_WebReportsGetCompleted);

            posClient.WebReportsGetAsync();
        }

        void posClient_WebReportsGetCompleted(object sender, WebReportsGetCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            dtReports = Functions.ConvertToDataTable(e.Result, "WebReports");

            foreach (DataRow drReport in dtReports.Rows)
            {
                ReportComboBox.Items.Add(drReport["ReportName"].ToString());
            }
        }


        void posClient_FileReaderCompleted(object sender, FileReaderCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ReportViewerTextBox.Text = "Could not load file.";
                return;

            }

            ReportViewerTextBox.TextWrapping = TextWrapping.Wrap;
            ReportViewerTextBox.Text = e.Result.ToString();
        }

        private void LookupButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "";

            ReportViewerTextBox.Text = "";

            foreach (DataRow drReport in dtReports.Rows)
            {
                if (drReport["ReportName"].ToString().Equals(ReportComboBox.Text))
                {
                    filePath = drReport["FilePath"].ToString();
                }
            }

            filePath = filePath.Replace(@"{yyyy\MM\dd}", ReportDatePicker.DateTime.Value.ToString("yyyy") + @"\" + ReportDatePicker.DateTime.Value.ToString("MM") + @"\" + ReportDatePicker.DateTime.Value.ToString("dd"));
            filePath = filePath.Replace(@"{yyyy-MM-dd}", ReportDatePicker.DateTime.Value.ToString(@"yyyy-MM-dd"));

            posClient.FileReaderAsync(filePath);
        }
    }
}
