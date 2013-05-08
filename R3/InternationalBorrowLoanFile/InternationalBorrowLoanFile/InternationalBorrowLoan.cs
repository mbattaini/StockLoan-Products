using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StockLoan.Common;

namespace InternationalBorrowLoanFile
{
    public partial class InternationalBorrowLoan : Form
    {
        private DataSet dsContracts = new DataSet();

        public InternationalBorrowLoan()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = InternationalBorrowLoanFormat.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd"), ContractsGet());
            
        }

        public DataSet ContractsGet()
        {
            try
            {
                SqlConnection sqlCn = new SqlConnection(Standard.ConfigValue("Database"));

                SqlCommand dbCmd = new SqlCommand("spContractGet", sqlCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = "PFSI";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsContracts, "Contracts");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            return dsContracts;
        }
        
    }
}
