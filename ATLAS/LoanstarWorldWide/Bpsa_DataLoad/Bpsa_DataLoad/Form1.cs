using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bpsa_DataLoad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            ParseSegregation parseSeg = new ParseSegregation();

            parseSeg.Load(Scanq5LoadTextBox.Text, "2011-08-12", "2011-08-15");                        
        }

        private void RdsweepLoadButton_Click(object sender, EventArgs e)
        {
            ParseNonClearingFails parseFails = new ParseNonClearingFails();

            parseFails.Load(RdsweepLoadTextBox.Text, "2011-08-15", "2011-08-15");
        }

        private void SiacLoadButton_Click(object sender, EventArgs e)
        {
            ParseClearingFails parseClearing = new ParseClearingFails();
            parseClearing.Load(SiacLoadTextBox.Text, "2011-08-15", "2011-08-15");
        }
    }
}
