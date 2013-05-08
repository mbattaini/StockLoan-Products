using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;


namespace BPSA_Data
{
    public partial class Form1 : Form
    {
        public SqlConnectionStringBuilder strBuilder;
        public string sqlQuery = "";
        public string secid = "H574621";

        public Form1()
        {
            InitializeComponent();

            InitializeComponent();
            strBuilder = new SqlConnectionStringBuilder();
            strBuilder.DataSource = @"HOOD\BPSA";
            strBuilder.InitialCatalog = "Fbidb";
            strBuilder.IntegratedSecurity = true;
            strBuilder.UserID = @"pendal_nt\mbattaini";
            strBuilder.Password = "Dropkick2001";
        }

        private void LookupButton_Click(object sender, EventArgs e)
        {
            DataSet dsData = new DataSet();

            LoadSqlQuery();
            sqlQuery = sqlQuery.Replace("<security-id>", "'" + secid + "'");

            SqlConnection sqlConn = new SqlConnection(strBuilder.ConnectionString);

            SqlCommand dbCmd = new SqlCommand(sqlQuery, sqlConn);
            dbCmd.CommandType = CommandType.Text;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
            dataAdapter.Fill(dsData, "Box");

            BoxGrid.SetDataBinding(dsData, "Box", false);
        }


        private void LoadSqlQuery()
        {
            TextReader file = new StreamReader(@"C:\temp\query.txt");
            sqlQuery = file.ReadToEnd();
        }

    }
}
