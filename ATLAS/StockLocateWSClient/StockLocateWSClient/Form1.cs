using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CAPICOM;
using StockLocateWSClient.StockLocate;

namespace StockLocateWSClient
{
    public partial class LocatesTestForm : Form
    {
        private DateTime startTime;
        private DateTime stopTime;
        private StockLocateSoapClient slClient;

        public LocatesTestForm()
        {
            InitializeComponent();
            slClient = new StockLocateSoapClient();
            
        }

        private void SubmitLocatesButton_Click(object sender, EventArgs e)
        {
            TimeLabel.Text = "";

            

            CAPICOM.EncryptedData encode = new CAPICOM.EncryptedData();
            encode.Content = DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm");
            encode.SetSecret(EncryptionKeyTextBox.Text, CAPICOM_SECRET_TYPE.CAPICOM_SECRET_PASSWORD);
            encode.Algorithm.Name = CAPICOM_ENCRYPTION_ALGORITHM.CAPICOM_ENCRYPTION_ALGORITHM_3DES;
            encode.Algorithm.KeyLength = CAPICOM_ENCRYPTION_KEY_LENGTH.CAPICOM_ENCRYPTION_KEY_LENGTH_128_BITS;
            string sDateTime = encode.Encrypt(CAPICOM_ENCODING_TYPE.CAPICOM_ENCODE_BASE64);

            slClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(AddressTextBox.Text);          

            startTime = DateTime.Now;
            System.Net.ServicePointManager.Expect100Continue = false;
            AnswersTextBox.Text = slClient.submitStockLocate(ClientIdTextBox.Text, sDateTime, "SL DEV TEST", SubmitTextBox.Text);
            stopTime = DateTime.Now;

            TimeLabel.Text = "Seconds: " + (stopTime - startTime).Seconds.ToString();
        }

        private void ViewLocatesButton_Click(object sender, EventArgs e)
        {
            TimeLabel.Text = "";

            CAPICOM.EncryptedData encode = new CAPICOM.EncryptedData();
            encode.Content = DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm");
            encode.SetSecret(EncryptionKeyTextBox.Text, CAPICOM_SECRET_TYPE.CAPICOM_SECRET_PASSWORD);
            encode.Algorithm.Name = CAPICOM_ENCRYPTION_ALGORITHM.CAPICOM_ENCRYPTION_ALGORITHM_3DES;
            encode.Algorithm.KeyLength = CAPICOM_ENCRYPTION_KEY_LENGTH.CAPICOM_ENCRYPTION_KEY_LENGTH_128_BITS;
            string sDateTime = encode.Encrypt(CAPICOM_ENCODING_TYPE.CAPICOM_ENCODE_BASE64);

            slClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(AddressTextBox.Text); 

            startTime = DateTime.Now;
            AnswersTextBox.Text = slClient.viewStockLocate(ClientIdTextBox.Text, sDateTime, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), "");
            stopTime = DateTime.Now;

            TimeLabel.Text = "Seconds: " + (stopTime - startTime).Seconds.ToString();
        }
    }
}
