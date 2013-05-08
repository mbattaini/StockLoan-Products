using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using StockLoan.Encryption;

namespace SLEncryptTester
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            btnDecrypt.IsEnabled = true;
            string s = EncryptDecrypt.Encrypt(txtPassword.Text.ToString(), "Password");
            txtEPwd.Text = s;

        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            btnEncrypt.IsEnabled = true;
            string s = EncryptDecrypt.Decrypt(txtEPwd.Text.ToString(), "Password");
            lblDecryptedPwd.Content = s;

        }
    }
}
