using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StockLoan.FileTransfer;
using StockLoan.Transport;
namespace StockLoanGPGEncryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            //VendorEncryption.GnuGPGImportKey(@"C:\Program Files (x86)\GNU\GnuPG\gpg.exe", @"C:\temp\stockloan_key.txt:");
            //VendorEncryption.GnuGPGEncrypt(@"C:\Program Files (x86)\GNU\GnuPG\gpg.exe", @"C:\temp\test.txt");

            GnuPG gpg = new GnuPG(@"C:\Program Files (x86)\GNU\GnuPG\");
            gpg.Recipient = "stockloan@pirum.com";
            gpg.Command = Commands.Encrypt;
            gpg.DoCommand(@"C:\Temp\Test.txt");
            
        }
    }
}
