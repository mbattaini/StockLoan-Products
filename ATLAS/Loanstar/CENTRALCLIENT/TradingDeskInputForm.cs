using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;
using StockLoan.Main;

namespace CentralClient
{
    public partial class TradingDeskInputForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private MainForm mainForm = null;

        public TradingDeskInputForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void TradingDeskInputForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                int height = this.Height;
                int width = this.Width;

                this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
                this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
                this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
                this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
            this.Cursor = Cursors.Default;
        }



        private void TradingDeskInputForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
              RegistryValue.Write(this.Name, "Top", this.Top.ToString());
              RegistryValue.Write(this.Name, "Left", this.Left.ToString());
              RegistryValue.Write(this.Name, "Height", this.Height.ToString());
              RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }
            mainForm.tradingDeskInputForm = null;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}