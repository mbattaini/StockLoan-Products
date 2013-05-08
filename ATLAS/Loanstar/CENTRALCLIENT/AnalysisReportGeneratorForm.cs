using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using StockLoan.Main;

namespace CentralClient
{
    public partial class AnalysisReportGeneratorForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private MainForm mainForm;
        private DataSet dsBookGroups;
        private DataSet dsUsers;
        private DataSet dsReports;
        private DataSet dsReportTypes;
        private DataSet dsReportRequests;

        private DataView dvReportRequests;
        private C1.C1Report.C1Report reportTemp;

        public AnalysisReportGeneratorForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
             
        private void AnalysisReportGeneratorForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            reportTemp = new C1.C1Report.C1Report();

            // load report into control
            string reportFile = System.Environment.CurrentDirectory + @"\Layouts\MarksReport.xml";
            reportTemp.Load(reportFile, "MarksReport");
            
//            ReportPrintPreviewControl.PreviewPane.Document = reportTemp;
            this.Cursor = Cursors.Default;
        }
    }
}
