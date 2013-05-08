using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StockLoan.Encryption;

namespace EncryptionTeter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string pwd = txtPassword.Text.ToString();

            string encPwd = EncryptDecrypt.Encrypt(pwd);

            txtEncryptedPwd.Text = encPwd.ToString();

        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string encPwd = txtEncryptedPwd.Text.ToString();

            string pwd = EncryptDecrypt.Decrypt(encPwd);

            txtDecryptedPwd.Text = pwd.ToString();

        }
    }
}
