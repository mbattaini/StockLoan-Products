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
    public partial class AdminKeyValuesForm : C1.Win.C1Ribbon.C1RibbonForm
    {
        private MainForm mainForm = null;
        private DataSet dsKeyValues = null;

        public AdminKeyValuesForm(MainForm mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;
        }

        private void AdminKeyValuesForm_Load(object sender, EventArgs e)
	    {
            int height = this.Height;
            int width = this.Width;

            this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
            this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
            this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
            this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

          try
          {
            dsKeyValues = mainForm.ServiceAgent.KeyValueGet();

            KeyValuesGrid.SetDataBinding(dsKeyValues, "KeyValues", true);
          }
          catch (Exception error)
          {
            mainForm.Alert(this.Name, error.Message);
          }
        }

        private void KeyValuesGrid_AUpdate(object sender, EventArgs e)
        {
         
        }

        private void KeyValuesGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            try
            {
                mainForm.ServiceAgent.KeyValueSet(
                KeyValuesGrid.Columns["KeyId"].Text,
                KeyValuesGrid.Columns["KeyValue"].Text);
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void KeyValuesGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals((char)13))
                {
                    KeyValuesGrid.UpdateData();
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void AdminKeyValuesForm_FormClosed(object sender, FormClosedEventArgs e)
          {
              if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
              {
                  RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                  RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                  RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                  RegistryValue.Write(this.Name, "Width", this.Width.ToString());
              }

              mainForm.adminKeyValuesForm = null;

          }

        private void SendToClipboardCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if ( KeyValuesGrid.Focused )
                {
                    mainForm.SendToClipboard(ref KeyValuesGrid);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (KeyValuesGrid.Focused)
                {
                    mainForm.SendToExcel(ref KeyValuesGrid, true);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

        private void SendToEmailCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            try
            {
                if (KeyValuesGrid.Focused)
                {
                    mainForm.SendToEmail(ref KeyValuesGrid);
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

    
    }
}