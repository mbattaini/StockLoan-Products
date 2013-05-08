using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;

using StockLoan.Transport;

namespace FTPClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileTransfer fileTransfer = new FileTransfer("");
            FileResponse fResponse = new FileResponse();

           

            #region external_test
            {
                string remotePath = "ftp://ftp.loanet.com/out/D01O0164.txt";
                StatusTextBox.Text += "External Network Test\r\n";

                fResponse = fileTransfer.FileTime(remotePath, "0234", "rid7bwk3bs");
                if (fResponse.status.Equals(FileStatus.OK))
                {
                    StatusTextBox.Text += fResponse.lastWriteTime + "\r\n";
                }
                else
                {
                    StatusTextBox.Text += fResponse.comment + "\r\n";
                }

                fResponse = fileTransfer.FileGet(remotePath, "0234", "rid7bwk3bs", @"c:\temp\D01O0164.txt", true);
                if (fResponse.status.Equals(FileStatus.OK))
                {
                    StatusTextBox.Text += fResponse.fileName + "\r\n";
                }
                else
                {
                    StatusTextBox.Text += fResponse.comment + "\r\n";
                }

                fResponse = fileTransfer.FileExists(remotePath, "0234", "rid7bwk3bs");
                if (fResponse.status.Equals(FileStatus.OK))
                {
                    StatusTextBox.Text += fResponse.isExist.ToString() + "\r\n";
                }
                else
                {
                    StatusTextBox.Text += fResponse.comment + "\r\n";
                }


                fResponse = fileTransfer.FileContentsGet(remotePath, "0234", "rid7bwk3bs");
                if (fResponse.status.Equals(FileStatus.OK))
                {
                    StatusTextBox.Text += fResponse.fileContents + "\r\n";
                }
                else
                {
                    StatusTextBox.Text += fResponse.comment + "\r\n";
                }


                fResponse = FileArchive.Get("PFSI", "MI.US.S", "03:00", @"ftp://ftp.loanet.com/out/", @"D01O0164.txt", "0234", "rid7bwk3bs", @"c:\temp\", "D01O0164.txt", 0, "");
                if (fResponse.status.Equals(FileStatus.OK))
                {
                    StatusTextBox.Text += fResponse.fileName + "\r\n";
                }
                else
                {
                    StatusTextBox.Text += fResponse.comment + "\r\n";
                }

            }
            #endregion

            #region internal_test
            {
                StatusTextBox.Text += "Internal Network Test\r\n";

                fResponse = fileTransfer.FileTime(@"\\dalrptprd01\reports\ftpupload\stockloan\anetics\0234-0695.20110324.dat", "", "");
                if (fResponse.status.Equals(FileStatus.OK))
                {
                    StatusTextBox.Text += fResponse.lastWriteTime + "\r\n";
                }
                else
                {
                    StatusTextBox.Text += fResponse.comment + "\r\n";
                }

                fResponse = fileTransfer.FileGet(@"\\dalrptprd01\reports\ftpupload\stockloan\anetics\0234-0695.20110324.dat", "", "", @"c:\temp\0234-0695.20110325.dat", true);
                if (fResponse.status.Equals(FileStatus.OK))
                {
                    StatusTextBox.Text += fResponse.fileName + "\r\n";
                }
                else
                {
                    StatusTextBox.Text += fResponse.comment + "\r\n";
                }

                fResponse = fileTransfer.FileExists(@"\\dalrptprd01\reports\ftpupload\stockloan\anetics\0234-0695.20110324.dat", "", "");
                if (fResponse.status.Equals(FileStatus.OK))
                {
                    StatusTextBox.Text += fResponse.isExist.ToString() + "\r\n";
                }
                else
                {
                    StatusTextBox.Text += fResponse.comment + "\r\n";
                }


                fResponse = fileTransfer.FileContentsGet(@"\\dalrptprd01\reports\ftpupload\stockloan\anetics\0234-0695.20110324.dat", "", "");
                if (fResponse.status.Equals(FileStatus.OK))
                {
                    StatusTextBox.Text += fResponse.fileContents + "\r\n";
                }
                else
                {
                    StatusTextBox.Text += fResponse.comment + "\r\n";
                }
            }

            fResponse = FileArchive.Get("PFSI", "ML.US.S", "03:00", "", @"\\dalrptprd01\reports\ftpupload\stockloan\anetics\0234-0695.20110324.dat", "", "", @"c:\temp\", "0234-0695.20110324.dat", 1, "");
            if (fResponse.status.Equals(FileStatus.OK))
            {
                StatusTextBox.Text += fResponse.fileName + "\r\n";
            }
            else
            {
                StatusTextBox.Text += fResponse.comment + "\r\n";
            }

            #endregion
        }
   }
}
