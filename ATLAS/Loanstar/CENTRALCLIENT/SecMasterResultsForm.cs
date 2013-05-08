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
  public partial class SecMasterResultsForm : C1.Win.C1Ribbon.C1RibbonForm
  {
    private string secId = null;
    private MainForm mainForm = null;
    private DataSet dsSearchResults = null;
    

    public SecMasterResultsForm(MainForm mainForm, string secId)
    {
      InitializeComponent();
      this.mainForm = mainForm;
      this.secId = secId;
    }

    private void SecMasterResultsForm_Load(object sender, EventArgs e)
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

            dsSearchResults = mainForm.ServiceAgent.SecMasterSearch(secId);
            ResultsGrid.SetDataBinding(dsSearchResults, "SecMasterResults", true);
        }
        catch (Exception error)
        {
            mainForm.Alert(this.Name, error.Message);
        }
        this.Cursor = Cursors.Default;
    }

    private void ResultsGrid_Paint(object sender, PaintEventArgs e)
    {
      if (!secId.Equals(ResultsGrid.Columns["SecId"].Text))
      {
        mainForm.SecId = ResultsGrid.Columns["SecId"].Text;
        secId = ResultsGrid.Columns["SecId"].Text;
      }
    }

    private void ResultsGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {
      switch (e.Column.DataField)
      {
        case "Price":
          try
          {
            e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.00");
          }
          catch { }
          break;

        case "PriceDate":
          try
          {
            e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
          }
          catch { }
          break;

      }
    }

      private void SecMasterResultsForm_FormClosed(object sender, FormClosedEventArgs e)
      {
          if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
          {
              RegistryValue.Write(this.Name, "Top", this.Top.ToString());
              RegistryValue.Write(this.Name, "Left", this.Left.ToString());
              RegistryValue.Write(this.Name, "Height", this.Height.ToString());
              RegistryValue.Write(this.Name, "Width", this.Width.ToString());
          }

      }

      private void SendToClipboardCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
      {
          try
          {
              if (ResultsGrid.Focused)
              {
                  mainForm.SendToClipboard(ref ResultsGrid);
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
              if (ResultsGrid.Focused)
              {
                  mainForm.SendToExcel(ref ResultsGrid, true);
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
              if (ResultsGrid.Focused)
              {
                  mainForm.SendToEmail(ref ResultsGrid);
              }
          }
          catch (Exception error)
          {
              mainForm.Alert(this.Name, error.Message);
          }
      }


  }
}