using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StockLoan.FileParsing;

namespace RDSweepParserApp
{
    public partial class RDSweepParserForm : Form
    {
        public RDSweepParserForm()
        {
            InitializeComponent();
        }

        private void RDSweepParserForm_Load(object sender, EventArgs e)
        {
            /*RDSweepItem rdSweep = new RDSweepItem();
            rdSweep.ParseRdSweep();            */
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            RDSweepItem rdSweep = new RDSweepItem();
            rdSweep.ParseRdSweep();
             
            
        }
    }
}
