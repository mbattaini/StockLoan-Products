using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;

namespace CentralClient
{
    public partial class ErrorMessageForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private long index = 0;
        private DataSet dataSet = null;

        private MainForm mainForm;

        public ErrorMessageForm(MainForm mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            
            dataSet = new DataSet();
            dataSet.Tables.Add("Errors");

            dataSet.Tables["Errors"].Columns.Add("Index", typeof(long));
            dataSet.Tables["Errors"].Columns.Add("FormName", typeof(string));
            dataSet.Tables["Errors"].Columns.Add("FormError", typeof(string));

            dataSet.Tables["Errors"].AcceptChanges();

            ErrorGrid.SetDataBinding(dataSet, "Errors", true);

            mainForm.ErrorCount(index);
        }

        public void Add(string formName, string formError)
        {
            index++;

            DataRow dr = dataSet.Tables["Errors"].NewRow();

            dr["Index"] = index;
            dr["FormName"] = formName;
            dr["FormError"] = formError;

            dataSet.Tables["Errors"].Rows.Add(dr);
            dataSet.Tables["Errors"].AcceptChanges();

            mainForm.ErrorCount(index);
        }

        private void ErrorMessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }
    }
}