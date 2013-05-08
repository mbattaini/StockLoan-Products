using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoanetTransactionReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            DataSet dsContracts = new DataSet();
            dsContracts.Tables.Add("Contracts");
            dsContracts.Tables["Contracts"].Columns.Add(@"Credit / Debit");
            dsContracts.Tables["Contracts"].Columns.Add("Quantity");
            dsContracts.Tables["Contracts"].Columns.Add("Cusip");
            dsContracts.Tables["Contracts"].Columns.Add("ADP Entry Desc");
            dsContracts.Tables["Contracts"].Columns.Add("ADP Account Number");
            dsContracts.Tables["Contracts"].Columns.Add("Spin Code");
            dsContracts.Tables["Contracts"].Columns.Add("Reg Indicator");
            dsContracts.Tables["Contracts"].Columns.Add("Amount");
            dsContracts.Tables["Contracts"].Columns.Add("Loanet Comments");
            dsContracts.Tables["Contracts"].Columns.Add("Loanet Contract ID");
            dsContracts.AcceptChanges();


            string[] fileArray = File.ReadAllLines(FilePathTextBox.Text);

            foreach (string line in fileArray)
            {
                if (line.Length > 70)
                {
                    DataRow drRow = dsContracts.Tables["Contracts"].NewRow();
                    drRow["Credit / Debit"] = line.Substring(6, 1);
                    drRow["Quantity"] = line.Substring(7, 6);
                    drRow["Cusip"] = line.Substring(13, 9);
                    drRow["ADP Entry Desc"] = line.Substring(22, 3);
                    drRow["ADP Account Number"] = line.Substring(30, 10);
                    drRow["Spin Code"] = line.Substring(40, 1);
                    drRow["Reg Indicator"] = line.Substring(41, 1);
                    drRow["Amount"] = decimal.Parse(line.Substring(42, 8) + "." + line.Substring(50, 2));
                    drRow["Loanet Comments"] = line.Substring(52, 11);
                    drRow["Loanet Contract ID"] = line.Substring(64, 9);

                    dsContracts.Tables["Contracts"].Rows.Add(drRow);
                }
            }

            c1TrueDBGrid1.SetDataBinding(dsContracts, "Contracts");
        }
    }
}