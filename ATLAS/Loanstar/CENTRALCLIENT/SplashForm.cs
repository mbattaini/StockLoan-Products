using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CentralClient
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
            RibbonProgressBar.Value = 0;
            RibbonProgressBar.Value = 25;
            RibbonProgressBar.Value = 50;
            RibbonProgressBar.Value = 100;
        }

		private void SplashForm_Load(object sender, EventArgs e)
		{

		}
    }
}