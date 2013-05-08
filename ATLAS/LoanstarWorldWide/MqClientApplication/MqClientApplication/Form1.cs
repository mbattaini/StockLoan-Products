using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StockLoan.Common;
using StockLoan.MqSeries;

namespace MqClientApplication
{
    public partial class Form1 : Form
    {
        private MqActivity mqActivity;
        private StreamWriter stmWriter;
        public Form1()
        {
            InitializeComponent();
            mqActivity = new MqActivity("");

            stmWriter = new StreamWriter(@"C:\temp\temp_msg.txt");
        }

        private void GetMsgButton_Click(object sender, EventArgs e)
        {
            DataSet dsMessages = new DataSet();
            dsMessages.Tables.Add("Messages");
            dsMessages.Tables["Messages"].Columns.Add("Msg");
            dsMessages.AcceptChanges();

            stmWriter.AutoFlush = true;

            while (true)
            {
                try
                {

                    /* DataRow drTemp = dsMessages.Tables["Messages"].NewRow();
                     drTemp["Msg"] = mqActivity.PullMessage();
                     dsMessages.Tables["Messages"].Rows.Add(drTemp);
                     dsMessages.AcceptChanges();*/
                    stmWriter.WriteLine(mqActivity.PullMessage());
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    break;
                }
            }

            
            //DataGridMsg.DataSource = dsMessages.Tables["Messages"];
        }
    }
}
