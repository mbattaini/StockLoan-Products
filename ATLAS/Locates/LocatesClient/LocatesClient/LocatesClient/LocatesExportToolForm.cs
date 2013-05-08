using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;
using Compression;

namespace LocatesClient
{
  public partial class LocatesExportToolForm : C1.Win.C1Ribbon.C1RibbonForm
  {
    private DataSet dsLocates = null;
    private MainForm mainForm = null;


    public LocatesExportToolForm(MainForm mainForm)
    {
      InitializeComponent();

      this.mainForm = mainForm;
	  dsLocates = new DataSet();
    }

	  private void LocatesGridUpdate()
	  {
		  this.Cursor = Cursors.WaitCursor;

		  try
		  {
			  dsLocates = mainForm.ShortSaleAgent.LocatesGet(TradeDateMinPicker.Text, TradeDateMaxPicker.Text, GroupCodeTextBox.Text, SecurityIdTextBox.Text, 0);

			  LocatesGrid.SetDataBinding(dsLocates, "Table", true);
		  }
		  catch (Exception error)
		  {
			  mainForm.Alert(this.Name, error.Message);
		  }

		  this.Cursor = Cursors.Default;
	  }


    private void TradeDatePicker_TextChanged(object sender, EventArgs e)
    {
		LocatesGridUpdate();
    }

    private void LocatesExportToolForm_Load(object sender, EventArgs e)
    {
      try
      {   
      }
      catch (Exception error)
      {
        mainForm.Alert(this.Name, error.Message);
      }
    }

    private void TradeDatePicker_Formatting(object sender, C1.Win.C1Input.FormatEventArgs e)
    {
      try
      {
        e.Text = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
      }
      catch { }
    }

    private void GenerateButton_Click(object sender, EventArgs e)
    {
      string fileName = "";

	  this.Cursor = Cursors.WaitCursor;

      try
      {
        if (ExportToExcelRadio.Checked)
        {
          fileName = Standard.ConfigValue("TempFilePath") + Standard.ProcessId() + " _EXCEL.xls";
          LocatesGrid.ExportToExcel(fileName);
        }
        else if (ExportToPdfRadio.Checked)
        {
          fileName = Standard.ConfigValue("TempFilePath") + Standard.ProcessId() + " _PDF.pdf";
          LocatesGrid.ExportToPDF(fileName);
        }

		System.Diagnostics.Process.Start(fileName);
      }
      catch (Exception error)
      {
        mainForm.Alert(this.Name, error.Message);
      }

	  this.Cursor = Cursors.Default;
    }

    private void LookUpButton_Click(object sender, EventArgs e)
    {
      LocatesGridUpdate();
    }

    private void LocatesGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {
		try
		{
			switch (LocatesGrid.Columns[e.ColIndex].DataField)
			{
				case ("TradeDate"):
					e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
					break;
				case ("LocateIdTail"):
					e.Value = e.Value.ToString().PadLeft(5, '0');
					break;
				case ("OpenTime"):
					e.Value = DateTime.Parse(e.Value).ToString(Standard.TimeFileFormat);
					break;
				case ("ClientQuantity"):
				case ("Quantity"):
					e.Value = long.Parse(e.Value).ToString("#,##0");
					break;
				case ("ActTime"):
					e.Value = Tools.FormatDate(e.Value.ToString(), "HH:mm:ss");
					break;
			}
		}
		catch { }
    }

    private void LocatesExportToolForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      mainForm.locatesExportToolForm = null;
    }

    private void CheckBox_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        LocatesGrid.Splits[0].DisplayColumns["TradeDate"].Visible = ShowTradeDateCheckBox.Checked;
        LocatesGrid.Splits[0].DisplayColumns["LocateId"].Visible = ShowLocateIdCheckBox.Checked;
        LocatesGrid.Splits[0].DisplayColumns["GroupCode"].Visible = ShowGroupCodeCheckBox.Checked;
        LocatesGrid.Splits[0].DisplayColumns["OpenTime"].Visible = ShowOpenTimeCheckBox.Checked;
        LocatesGrid.Splits[0].DisplayColumns["Status"].Visible = ShowStatusCheckBox.Checked;
        LocatesGrid.Splits[0].DisplayColumns["ActTime"].Visible = ShowDoneAtCheckBox.Checked;
        LocatesGrid.Splits[0].DisplayColumns["ActUserShortName"].Visible = ShowDoneByCheckBox.Checked;
      }
      catch (Exception error)
      {
        mainForm.Alert(this.Name, error.Message);
      }
    }
  }
}