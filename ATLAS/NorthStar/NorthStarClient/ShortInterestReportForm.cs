using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

using C1.C1Pdf;
using C1.Win.C1Ribbon;
using C1.Win.C1TrueDBGrid;

using StockLoan.Common;
using StockLoan.NorthStar;

namespace NorthStarClient
{
  public partial class ShortInterestReportingForm : C1RibbonForm
  {
    private MainForm mainForm = null;
    private DataSet dsShortInterest = null;
    private DataView dvShortInterest = null;
    private string secId = "";
    
    private Dictionary<string, ExcelCellStyle> excelCellStyleDictionary = null;

    public ShortInterestReportingForm(MainForm mainForm)
    {
      this.mainForm = mainForm;
      this.dsShortInterest = new DataSet();

      InitializeExcelCellStyleDictionary();

      InitializeComponent();
    }


    private void StatusRibbonLabelUpdate()
    {
      try
      {
        this.StatusRibbonLabel.Text = "Result: " + dvShortInterest.Count.ToString("#,##0") + " rows in grid";
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [ShortInterestReportingForm.StatusRibbonLabelUpdate]", 1);
      }
    }

    private void FindButton_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      if (DataValidation() == false)
        return;

      try
      {       
        dsShortInterest = mainForm.ShortInterestAgent.ShortInterestGet(
                                                 this.MpidTextBox.Text.Trim(),
                                                 this.CusipTextBox.Text.Trim(),
                                                 this.QuantityGreaterThanTextBox.Text.Trim(),
                                                 this.QuantityLessThanTextBox.Text.Trim(),
                                                 this.PriceLessThanTextBox.Text.Trim(),
                                                 this.ShortInterestMidMonthGreaterThanTextBox.Text.Trim(),
                                                 this.ShortInterestMonthEndGreaterThanTextBox.Text.Trim()
                                                 );

        dvShortInterest = new DataView(dsShortInterest.Tables["ShortInterest"], "", "Symbol", DataViewRowState.CurrentRows);
        
        this.ShortInterestGrid.SetDataBinding(dvShortInterest, "", true);
       
        StatusRibbonLabelUpdate();
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [ShortInterestReportingForm.buttonFind_Click]", 1);
      }

      this.Cursor = Cursors.Default;
    }

    private void ClearResultsMenuItem_Click(object sender, EventArgs e)
    {
      this.dsShortInterest.Clear();
      this.StatusRibbonLabel.Text = "";
    }

    private void ShortInterestGrid_FetchCellTips(object sender, C1.Win.C1TrueDBGrid.FetchCellTipsEventArgs e)
    {
      string toolTipString = "";

      if (e.Row == -2)
      {
        switch (e.Column.DataColumn.DataField)
        {
          case "AccountNumber":
            toolTipString = "Account Number";
            break;
          case "AccountName":
            toolTipString = "Account Name";
            break;
          case "CUSIP":
            toolTipString = "Security ID";
            break;
          case "Symbol":
            toolTipString = "Symbol";
            break;
          case "TradeDateSharesShort":
            toolTipString = "Total Shares Short On Trade Date";
            break;
          case "SettleDateSharesShort":
            toolTipString = "Total Shares Short On Settlement Date";
            break;
          case "TotalEquity":
            toolTipString = "Total Equity";
            break;
          case "TwentyDaysAvgVolume":
            toolTipString = "Twenty Days Average Moving Volume";
            break;
          case "DaysToLiquidity":
            toolTipString = "Days To Liquidity";
            break;
          case "PensonPercentOfFloat":
            toolTipString = "Penson Percent Of Float";
            break;
          case "TotalShortInterestMidMonth_MIL":
            toolTipString = "Total Short Interest at Middle Of Month";
            break;
          case "TotalShortInterestMidMonthDate":
            toolTipString = "Date of Total Short Interest at Middle Of Month";
            break;
          case "TotalShortInterestMonthEnd_MIL":
            toolTipString = "Total Short Interest at End Of Month";
            break;
          case "TotalShortInterestMonthEndDate":
            toolTipString = "Date of Total Short Interest at End Of Month";
            break;
          case "ExchangeDescription":
            toolTipString = "Exchange";
            break;
          case "CurrentPrice":
            toolTipString = "Current Price";
            break;
          default:
            toolTipString = "";
            break;
        }

        e.CellTip = toolTipString;
      }
      else
      {
        e.CellTip = "";
      }
    }

    private void ShortInterestGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {

      switch (e.Column.DataField)
      {
        case "SettleDateSharesShort":
        case "TradeDateSharesShort":
          try
          {
            e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
          }
          catch { }
          break;
        case "CurrentPrice":
        case "TotalEquity":
        case "TotalShortInterestMidMonth_MIL":
        case "TotalShortInterestMonthEnd_MIL":
        case "TwentyDaysAvgVolume":
          try
          {
            e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.000");
          }
          catch { }
          break;
        case "DaysToLiquidity":
        case "PensonPercentOfFloat":
          try
          {
            e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.00000000");
          }
          catch { }
          break;

        case "TotalShortInterestMidMonthDate":
        case "TotalShortInterestMonthEndDate":
          try
          {
            e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateFormat);
          }
          catch { }
          break;
      }
    }

    private void ShortInterestReportingForm_Load(object sender, EventArgs e)
    {
      try
      {
        this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "0"));
        this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "0"));
        this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "570"));
        this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "1530"));

        if (mainForm.ShortInterestAgent == null)
        {
          this.ShortInterestDataStatusLabel.Text = "NorthStar server is not available.";
        }
        else
        {
          string dataDate = mainForm.ShortInterestAgent.ShortInterestDataDateGet();

          if (dataDate.Equals(""))
          {
            this.ShortInterestDataStatusLabel.Text = "XFL data is not available.";
          }
          else
          {
            this.ShortInterestDataStatusLabel.Text = "XFL Data is current for " + dataDate;
          }
        }
      }
      catch (Exception error)
      {
        Log.Write(error.Message + "[ShortInterestReportForm.ShortInterestReportForm_Load]", 1);
        this.ShortInterestDataStatusLabel.Text = "NorthStar server is not available.";
      }
    }

    private void ShortInterestReportingForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      if (this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name, "Top", this.Top.ToString());
        RegistryValue.Write(this.Name, "Left", this.Left.ToString());
        RegistryValue.Write(this.Name, "Height", this.Height.ToString());
        RegistryValue.Write(this.Name, "Width", this.Width.ToString());
      }
      mainForm.shortInterestReportForm = null;
    }

    private bool DataValidation()
    {
      decimal value = 0.0M;

      try
      {
        if (!QuantityGreaterThanTextBox.Text.Trim().Equals(""))
        {
          if (decimal.TryParse(QuantityGreaterThanTextBox.Text, out value) == false)
          {
            MessageBox.Show("value is incorrect", "Error");
            this.QuantityGreaterThanTextBox.Focus();
            return false;
          }
          else if (value < 0)
          {
            MessageBox.Show("value can not be negative", "Error");
            this.QuantityGreaterThanTextBox.Focus();
            return false;
          }
        }

        if (!QuantityLessThanTextBox.Text.Trim().Equals(""))
        {
          if (decimal.TryParse(QuantityLessThanTextBox.Text, out value) == false)
          {
            MessageBox.Show("value is incorrect", "Error");
            this.QuantityLessThanTextBox.Focus();
            return false;
          }
          else if (value <= 0)
          {
            MessageBox.Show("value must be greater than zero", "Error");
            this.QuantityGreaterThanTextBox.Focus();
            return false;
          }
        }

        if (!this.PriceLessThanTextBox.Text.Trim().Equals(""))
        {
          if (decimal.TryParse(this.PriceLessThanTextBox.Text, out value) == false)
          {
            MessageBox.Show("value is incorrect", "Error");
            this.PriceLessThanTextBox.Focus();
            return false;
          }
          else if (value <= 0)
          {
            MessageBox.Show("value must be greater than zero", "Error");
            this.PriceLessThanTextBox.Focus();
            return false;
          }
        }

        if (!this.ShortInterestMidMonthGreaterThanTextBox.Text.Trim().Equals(""))
        {
          if (decimal.TryParse(this.ShortInterestMidMonthGreaterThanTextBox.Text, out value) == false)
          {
            MessageBox.Show("value is incorrect", "Error");
            this.ShortInterestMidMonthGreaterThanTextBox.Focus();
            return false;
          }
          else if (value < 0)
          {
            MessageBox.Show("value can not be negative", "Error");
            this.ShortInterestMidMonthGreaterThanTextBox.Focus();
            return false;
          }
        }

        if (!this.ShortInterestMonthEndGreaterThanTextBox.Text.Trim().Equals(""))
        {
          if (decimal.TryParse(this.ShortInterestMonthEndGreaterThanTextBox.Text, out value) == false)
          {
            MessageBox.Show("value is incorrect", "Error");
            this.ShortInterestMonthEndGreaterThanTextBox.Focus();
            return false;
          }
          else if (value < 0)
          {
            MessageBox.Show("value can not be negative", "Error");
            this.ShortInterestMonthEndGreaterThanTextBox.Focus();
            return false;
          }
        }
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [ShortInterestReportingForm.Datavalidation]", 1);
        return false;
      }

      return true;
    }

    private void InitializeExcelCellStyleDictionary()
    {
      try
      {
        excelCellStyleDictionary = new Dictionary<string, ExcelCellStyle>();

        ExcelCellStyle style = new ExcelCellStyle();
        style.DataField = "TotalEquity";
        style.DataType = typeof(decimal);
        style.StringFormat = "#,##0.000";
        excelCellStyleDictionary.Add(style.DataField, style);

        style = new ExcelCellStyle();
        style.DataField = "CurrentPrice";
        style.DataType = typeof(decimal);
        style.StringFormat = "#,##0.000";
        excelCellStyleDictionary.Add(style.DataField, style);

        style = new ExcelCellStyle();
        style.DataField = "TotalShortInterestMidMonth_MIL";
        style.DataType = typeof(decimal);
        style.StringFormat = "#,##0.000";
        excelCellStyleDictionary.Add(style.DataField, style);

        style = new ExcelCellStyle();
        style.DataField = "TotalShortInterestMonthEnd_MIL";
        style.DataType = typeof(decimal);
        style.StringFormat = "#,##0.000";
        excelCellStyleDictionary.Add(style.DataField, style);

        style = new ExcelCellStyle();
        style.DataField = "TwentyDaysAvgVolume";
        style.DataType = typeof(decimal);
        style.StringFormat = "#,##0.000";
        excelCellStyleDictionary.Add(style.DataField, style);

        style = new ExcelCellStyle();
        style.DataField = "DaysToLiquidity";
        style.DataType = typeof(decimal);
        style.StringFormat = "#,##0.00000000";
        excelCellStyleDictionary.Add(style.DataField, style);

        style = new ExcelCellStyle();
        style.DataField = "PensonPercentOfFloat";
        style.DataType = typeof(decimal);
        style.StringFormat = "#,##0.000000000";
        excelCellStyleDictionary.Add(style.DataField, style);

        style = new ExcelCellStyle();
        style.DataField = "SettleDateSharesShort";
        style.DataType = typeof(int);
        style.StringFormat = "#,##0";
        excelCellStyleDictionary.Add(style.DataField, style);

        style = new ExcelCellStyle();
        style.DataField = "TradeDateSharesShort";
        style.DataType = typeof(int);
        style.StringFormat = "#,##0";
        excelCellStyleDictionary.Add(style.DataField, style);
      }
      catch (Exception error)
      {
        Log.Write(error.Message + "[ShortInterestReportingForm.InitializeExcelCellStyleDictionary]", 1);
      }
    }

    private string GetSelectedContent(ref C1TrueDBGrid grid)
    {
      int textLength;
      int[] maxTextLength;

      int columnIndex = -1;
      StringBuilder builder = new StringBuilder();
      bool isWholeRowSelected = false;

      try
      {

        if (grid.SelectedCols.Count == 0 && grid.SelectedRows.Count > 0)
        {
          isWholeRowSelected = true;
          maxTextLength = new int[grid.Columns.Count];

          foreach (C1DataColumn dataColumn in grid.Columns)
          {
            maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
          }
        }
        else
        {
          maxTextLength = new int[grid.SelectedCols.Count];

          foreach (C1DataColumn dataColumn in grid.SelectedCols)
          {
            maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
          }
        }



        foreach (int rowIndex in grid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1DataColumn dataColumn in isWholeRowSelected ? grid.Columns : grid.SelectedCols)
          {
            if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
            {
              maxTextLength[columnIndex] = textLength;
            }
          }
        }

        columnIndex = -1;

        foreach (C1DataColumn dataColumn in isWholeRowSelected ? grid.Columns : grid.SelectedCols)
        {
          builder.Append(dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' '));
        }

        builder.AppendLine();

        columnIndex = -1;

        for (int i = 0; i < maxTextLength.Length; i++)
        {
          builder.Append(new string('-', maxTextLength[++columnIndex]) + "  ");
        }
        builder.AppendLine();

        foreach (int rowIndex in grid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1DataColumn dataColumn in isWholeRowSelected ? grid.Columns : grid.SelectedCols)
          {
            if (dataColumn.Value.GetType().Equals(typeof(string)))
            {
              builder.Append(dataColumn.CellText(rowIndex).Trim().PadRight(maxTextLength[++columnIndex] + 2));
            }
            else
            {
              builder.Append(dataColumn.CellText(rowIndex).Trim().PadLeft(maxTextLength[++columnIndex]) + "  ");
            }
          }

          builder.AppendLine();
        }
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [ShortInterestReportingForm.GetSelectedContent]", 1);
        builder = new StringBuilder();
      }

      return builder.ToString();
    }

    private void ShortInterestGrid_Paint(object sender, PaintEventArgs e)
    {
      if (!secId.Equals(ShortInterestGrid.Columns["CUSIP"].Text))
      {
        secId = ShortInterestGrid.Columns["CUSIP"].Text;
        mainForm.SecId = secId;        
      }

      StatusRibbonLabelUpdate();
    }

    private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      Excel.ExportGridToExcel(ref ShortInterestGrid, 0, excelCellStyleDictionary);
      this.Cursor = Cursors.Default;
    }

    private void SendToClipboardCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        Clipboard.SetDataObject(GetSelectedContent(ref ShortInterestGrid));
      }
      catch { }

      this.Cursor = Cursors.Default;
    }

    private void SendToMailCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        Clipboard.SetDataObject(GetSelectedContent(ref ShortInterestGrid));
      }
      catch { }

      this.Cursor = Cursors.Default;
    }   
  }
}