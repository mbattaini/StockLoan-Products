using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CNSProjection0234
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder dbBuilder = new SqlConnectionStringBuilder();
            dbBuilder.DataSource = "Zeus";
            dbBuilder.InitialCatalog = "Sendero";
            dbBuilder.IntegratedSecurity = true;


            CnsProjectionParse cnsParse = new CnsProjectionParse(FileTextBox.Text,dbBuilder.ConnectionString);
            CnsGrid.SetDataBinding(cnsParse.Load(), "Cns");
        }

        private void Projections0158Button_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder dbBuilder = new SqlConnectionStringBuilder();
            dbBuilder.DataSource = "Zeus";
            dbBuilder.InitialCatalog = "Sendero";
            dbBuilder.IntegratedSecurity = true;

            SqlConnectionStringBuilder dbBuilderEx = new SqlConnectionStringBuilder();
            dbBuilderEx.DataSource = "Dalsqldev50";
            dbBuilderEx.InitialCatalog = "Loanstar";
            dbBuilderEx.IntegratedSecurity = true;


            CnsProjectionDBLoad cnsLoad = new CnsProjectionDBLoad(dbBuilder.ConnectionString, dbBuilderEx.ConnectionString);
            DataBaseGrid.SetDataBinding(cnsLoad.Load(), "Items");
        }

        private void FileTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
