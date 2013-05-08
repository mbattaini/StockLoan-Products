using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;


namespace BPSA_Viewer
{
    public partial class Form1 : Form
    {
        public SqlConnectionStringBuilder strBuilder;
        public string sqlQuery = "";

        public Form1()
        {
            InitializeComponent();
            strBuilder = new SqlConnectionStringBuilder();
            strBuilder.DataSource = @"HOOD\BPSA";
            strBuilder.InitialCatalog = "Fbidb";
            strBuilder.IntegratedSecurity = true;
            strBuilder.UserID = @"pendal_nt\mbattaini";
            strBuilder.Password = "Dropkick2001";
        }

        private void LoadSqlQuery()
        {
            TextReader file = new StreamReader(@"C:\temp\query.txt");
            sqlQuery = file.ReadToEnd();
        }

        private void LookupButton_Click(object sender, EventArgs e)
        {
            DataSet dsData = new DataSet();

            LoadSqlQuery();
            sqlQuery = sqlQuery.Replace("<security-id>", "'" + SecurityIdTextBox.Text + "'");

            SqlConnection sqlConn = new SqlConnection(strBuilder.ConnectionString);

            SqlCommand dbCmd = new SqlCommand(sqlQuery, sqlConn);
            dbCmd.CommandType = CommandType.Text;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
            dataAdapter.Fill(dsData, "Box");

            //BoxGrid.SetDataBinding(dsData, "Box");
        }
    }
}
