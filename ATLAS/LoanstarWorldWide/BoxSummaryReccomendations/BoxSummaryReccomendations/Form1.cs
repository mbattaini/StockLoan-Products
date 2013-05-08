using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BoxSummaryReccomendations;

namespace BoxSummaryReccomendations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LookupButton_Click(object sender, EventArgs e)
        {
            DataSet dsData = new DataSet();
            dsData = BoxSummaryReccomendations.Reccomendations(BizDateEdit.Text);

            foreach (DataRow dr in dsData.Tables["BoxSummary"].Rows)
            {
                if (long.Parse(dr["Recall"].ToString()) < 1)
                {
                    dr.Delete();
                }
            }
            dsData.AcceptChanges();

            RecallGrid.SetDataBinding(dsData, "BoxSummary", true);
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            RecallGrid.ExportToExcel(@"C:\temp\" + BizDateEdit.Text + "_recalls.xls");
        }
    }
}
