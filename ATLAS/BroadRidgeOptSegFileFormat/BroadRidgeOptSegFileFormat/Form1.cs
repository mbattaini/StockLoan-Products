using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BroadRidgeOptSegFileFormat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            DataSet dsContracts = new DataSet();

            SqlConnectionStringBuilder strBuilder = new SqlConnectionStringBuilder();
            strBuilder.DataSource = "Zeus";
            strBuilder.InitialCatalog = "Sendero";
            strBuilder.IntegratedSecurity = true;



            SqlConnection dbCn = new SqlConnection(strBuilder.ConnectionString);

            SqlCommand dbCmd = new SqlCommand("spContractAdpIdGet", dbCn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.CommandTimeout = 300;

            SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
            paramBizDate.Value = BizDateEdit.Text;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
            dataAdapter.Fill(dsContracts, "Contracts");

            FileTextBox.Text = BDOptSegFileFormat.Format(BizDateEdit.Text, dsContracts, "0234;0158;");

        }
    }
}
