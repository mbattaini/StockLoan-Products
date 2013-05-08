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

using StockLoan.Common;
using StockLoan.Transport;

namespace PirumWindowsFormsApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void QueryButton_Click(object sender, EventArgs e)
        {
            DataSet dsContracts = new DataSet();
            DataSet dsReturns = new DataSet();

            string fileName = "";
            string tempDirectory = @"C:\Temp\";


            dsContracts = ContractsGet(BizDateEdit.Text, BookGroupTextBox.Text);
            dsReturns = ReturnsGet(BizDateEdit.Text, BookGroupTextBox.Text);

            PirumContractCompare pCC = new PirumContractCompare(BizDateEdit.Text ,dsContracts, dsReturns);
            StatusTextBox.Text = pCC.Parse();            

            fileName = CreateFile(tempDirectory, true);

            GnuPG gpg = new GnuPG(Standard.ConfigValue("HomePath"));
            gpg.Timeout = 56000000;
            gpg.UserId = Standard.ConfigValue("UserId");
            gpg.Recipient = "stockloan@pirum.com";
            gpg.Verbosity = VerbosityLevels.None;
            gpg.Command = Commands.EncryptSign;
            gpg.DoCommand(tempDirectory + fileName);

            fileName = fileName + ".gpg";

            FileTransfer fileTransfer = new FileTransfer(Standard.ConfigValue("Database"));
            //fileTransfer.FilePut(@"ftp://xfr.pirum.com/" + fileName, "pensonuk", "clw11ths", tempDirectory + fileName);
            fileTransfer.FilePut(@"ftp://files.penson.com/upload/" + fileName, "gsco", "mAr1s-gsCo", tempDirectory + fileName);
        }

        private string CreateFile(string tempDirectory, bool encrypt)
        {
            string fileName = "live_penson_uk_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".dat";

            TextWriter textWriter = new StreamWriter(tempDirectory + fileName);

            textWriter.Write(StatusTextBox.Text);
            textWriter.Close();

            return fileName;
        }

        private DataSet ContractsGet(string bizDate, string bookGroup)
        {
            DataSet dsContracts = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spContractGet", new SqlConnection(Standard.ConfigValue("Database")));
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsContracts, "Contracts");
            }
            catch (Exception error)
            {
                StatusTextBox.Text = error.Message;
            }

            return dsContracts;
        }

        private DataSet ReturnsGet(string bizDate, string bookGroup)
        {
            DataSet dsReturns = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("spReturnGet", new SqlConnection(Standard.ConfigValue("Database")));
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                paramBookGroup.Value = bookGroup;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsReturns, "Returns");
            }
            catch (Exception error)
            {
                StatusTextBox.Text = error.Message;
            }

            return dsReturns;
        }
    }
}
