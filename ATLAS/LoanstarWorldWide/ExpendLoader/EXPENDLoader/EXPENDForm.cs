using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StockLoan.InventoryService;
using StockLoan.Common;

namespace EXPENDLoader
{
    public partial class EXPENDForm : Form
    {
        public EXPENDForm()
        {
            InitializeComponent();
        }

        private void LoadExpendFileButton_Click(object sender, EventArgs e)
        {
            string filePath = FilePathTextBox.Text + @"\";
            string fileName = FileNameTextBox.Text;
            string bizDate = FileDateTextBox.Text;
            string bizDatePrior = DateTime.Parse(bizDate).AddDays(-1).ToString("yyyy-MM-dd");
            int loadCount = 0;

            try
            {
                ExpendLoader expend = new ExpendLoader(filePath, fileName, bizDate, bizDatePrior);
                loadCount = expend.LoadFile();

                MessageLabel.Text = "EXPEND file load count = " + loadCount.ToString("#,###,##0");
            }
            catch
            {
                throw;
            }
        }

        private void EXPENDForm_Load(object sender, EventArgs e)
        {

            FileDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            FilePathTextBox.Text = @"\\ftpen003\shrNDMReportTransfer\Ridge\Prod\";  
            FileNameTextBox.Text = "EXPEND";
            MessageLabel.Text = "";

        }

    }
}
