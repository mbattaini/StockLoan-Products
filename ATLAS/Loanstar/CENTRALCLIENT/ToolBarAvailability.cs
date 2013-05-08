using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;

namespace CentralClient
{
    public partial class AvailabilityToolBar : C1.Win.C1Ribbon.C1RibbonForm
   {
        public AvailabilityToolBar()
        {
            InitializeComponent();
        }

      private void ShellAppBar_DockingEdgeChanged(object sender, EventArgs e)
      {
        if (this.ShellAppBar.DockingEdge != LogicNP.ShellObjects.DockingEdges.UnDocked)
        {
          this.ControlBox = false;
          this.FormBorderStyle = FormBorderStyle.None;
        }
        else
        {
          this.ControlBox = true;
          this.FormBorderStyle = FormBorderStyle.Sizable;
        }
      }
    }
}