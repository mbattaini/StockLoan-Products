using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        DataSet dsContracts = new DataSet();

        public Form1()
        {
            InitializeComponent();
        }



        public void parsefile()
        {
            string line = "";
            BinaryReader binaryReader;
            char[] c = new char[140];

            binaryReader = new BinaryReader(File.Open(@"C:\temp\newpos", FileMode.Open), Encoding.ASCII);
            binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);

            try
            {

                while (binaryReader.Read(c, 0, 140) > 0)
                {
                    line = "";
                    if (c[0].Equals('B') || c[0].Equals('L'))
                    {
                        line += new String(c, 52, 9) + ",";
                        line += new String(c, 0, 1) + ",";
                        line += new String(c, 1, 4) + ",";
                        line += new String(c, 5, 9).Trim() + ",";
                        line += long.Parse(new String(c, 18, 9)) + ",";
                        line += ((double)(long.Parse(new String(c, 30, 12)) / 100D)).ToString() + ",";
                        line += new String(c, 69, 1).Trim() + ",";
                        line += new String(c, 110, 4) + "-" + new String(c, 106, 2) + "-" + new String(c, 108, 2) + ",";
                        line += new String(c, 48, 4) + "-" + new String(c, 44, 2) + "-" + new String(c, 46, 2) + ",";
                        line += new String(c, 74, 4) + "-" + new String(c, 70, 2) + "-" + new String(c, 72, 2) + ",";
                        line += ((double)(int.Parse(new String(c, 61, 5)) / 1000D)).ToString() + ",";
                        line += c[68].ToString() + ";";

                        if (c[68].Equals('N'))
                        {
                            //  line += (contract.Rate * (-1)).ToString() + ",";
                        }

                        line += new String(c, 68, 1).Trim() + ",";
                        line += new String(c, 78, 1).Trim() + ",";
                        line += new String(c, 79, 1).Trim() + ",";
                        line += ((decimal)(int.Parse(new String(c, 80, 6)) / 1000D)).ToString() + ",";
                        line += c[86].Equals('C').ToString() + ",";
                        line += (!c[94].Equals('N')).ToString() + ",";
                        line += new String(c, 95, 1).Trim() + ",";

                        if (c[96].Equals(' '))
                        {
                            line += 1.00D + ",";
                        }
                        else
                        {
                            line += (double)(int.Parse(new String(c, 96, 3)) / 100D) + ",";
                        }

                        if (c[99].Equals(' '))
                        {
                            line += "USD" + ",";
                        }
                        else
                        {
                            line += new String(c, 99, 3) + ",";
                        }

                        line += new String(c, 102, 2).Trim() + ",";
                        line += new String(c, 104, 2).Trim() + ",";
                        line += new String(c, 134, 4).Trim() + ",";
                        line += new String(c, 114, 20).Trim();

                    }
                    else if (c[0].Equals('*') && c[1].Equals('T'))
                    {
                        //contractCount = int.Parse(new String(c, 8, 6));
                    }


                    ContractsTetBox.Text += line + "\r\n";
                }
            }
            catch
            {
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parseRecalls();
        }


        public void parseRecalls()
        {
            string line = "";

            BinaryReader binaryReader;
            char[] c = new char[80];

            binaryReader = new BinaryReader(File.Open(@"C:\temp\newpro", FileMode.Open), Encoding.ASCII);
            binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);

            try
            {
                while (binaryReader.Read(c, 0, 80) > 0)
                {
                    if (c[0].Equals('B') || c[0].Equals('L'))
                    {
                        line = "";
                        line += new String(c, 20, 9) + ",";
                        line += new String(c, 0, 1) + ",";
                        line += new String(c, 1, 4) + ",";
                        line += new String(c, 5, 9) + ",";
                        line += long.Parse(new String(c, 29, 9)).ToString() + ",";
                        line += ("20" + new String(c, 18, 2) + "-" + new String(c, 14, 2) + "-" + new String(c, 16, 2)).ToString() + ",";
                        line += ("20" + new String(c, 42, 2) + "-" + new String(c, 38, 2) + "-" + new String(c, 40, 2)).ToString() + ",";
                        line += new String(c, 44, 1) + ",";
                        line += new String(c, 45, 2) + ",";
                        line += new String(c, 47, 16) + ",";
                        line += (short.Parse(new String(c, 63, 6))).ToString() + ",";
                        line += new String(c, 69, 11).Trim() + ",";

                        ContractsTetBox.Text += line + "\r\n";
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                binaryReader.Close();
            }
        }
    }
}         
