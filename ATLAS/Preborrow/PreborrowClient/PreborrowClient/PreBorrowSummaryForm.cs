using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;

namespace PreborrowClient
{
  public partial class PreBorrowSummaryForm : Form
  {
    private PreborrowMainForm pbMainForm = null;

    private DataSet dsPreBorrowRecords = null;
    private DataSet dsContracts = null;
    private DataSet dsFails = null;
    private DataSet dsNeeds = null;
    private DataSet dsSecMasterItem = null; 
    private DataSet dsExcess = null;
    private DataSet dsInventory = null;

    private String secId = "";

    public PreBorrowSummaryForm(PreborrowMainForm pbMainForm)
    {
      InitializeComponent();

      this.pbMainForm = pbMainForm;
    }


    private void LoadContractData()
    {
      try
      {
        dsContracts = pbMainForm.PreBorrowAgent.ContractDataGet((short)pbMainForm.UtcOffset, pbMainForm.PreBorrowAgent.BizDate(), "", "");

        StatusMessageLabel.Text = "Contracts updated at : " + DateTime.Now.ToString("HH:mm:ss");
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }

    private void CalculateFails()
    {
      ArrayList secId = new ArrayList();
      DataRow tempDr = null;

      long deliveryFails = 0;
      long recieveFails = 0;
      long netPositionSettled = 0;

      string symbol;

      if (StartDatePicker.Text != StopDatePicker.Text)
      {       
        return;
      }

      try
      {
        dsFails = new DataSet();
        dsFails.Tables.Add("Fails");
        dsFails.Tables["Fails"].Columns.Add("SecId");
        dsFails.Tables["Fails"].Columns.Add("Quantity");
        dsFails.AcceptChanges();

        foreach (DataRow dr in dsPreBorrowRecords.Tables["PreBorrowRecords"].Rows)
        {
          if (secId.IndexOf(dr["SecId"].ToString()) == -1)
          {
            secId.Add(dr["SecId"].ToString());
          }
        }


        for (int i = 0; i < secId.Count; i++)
        {
          tempDr = pbMainForm.PreBorrowAgent.SecMasterLookup(secId[i].ToString(), true, false, 0, "").Tables["BoxPosition"].Rows[0];

          deliveryFails = (long)tempDr["ClearingFailInSettled"] + (long)tempDr["BrokerFailInSettled"] + (long)tempDr["DvpFailInSettled"];
          recieveFails = (long)tempDr["ClearingFailOutSettled"] + (long)tempDr["BrokerFailOutSettled"] + (long)tempDr["DvpFailOutSettled"];
          netPositionSettled = (long)tempDr["NetPositionSettled"];
                   
          symbol = "";

          foreach (DataRow dr1 in dsPreBorrowRecords.Tables["PreBorrowRecords"].Select("SecId = '" + secId[i].ToString() + "'"))
          {         
            symbol = dr1["Symbol"].ToString();
          }

          if ((deliveryFails + recieveFails) > 0)
          {
            DataRow dataRow = dsFails.Tables["Fails"].NewRow();


            dataRow["SecId"] = symbol;
            dataRow["Quantity"] = netPositionSettled;
            dsFails.Tables["Fails"].Rows.Add(dataRow);
          }
        }

        FailsGrid.SetDataBinding(dsFails, "Fails", true);
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }
    
    private void CalculateExcess()
    {
      ArrayList secId = new ArrayList();

      long totalPreBorrows = 0;
      long allocatedBorrows = 0;      
      long exDeficitSettled = 0;
      long excess = 0;

      string poolCode = "P";
      string bookGroup = "0234";
      string symbol = "";

      if (StartDatePicker.Text != StopDatePicker.Text)
      {
        return;
      }

      try
      {
        dsExcess = new DataSet();
        dsExcess.Tables.Add("Excess");
        dsExcess.Tables["Excess"].Columns.Add("SecId");
        dsExcess.Tables["Excess"].Columns.Add("Quantity");
        dsExcess.AcceptChanges();

        foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
        {
          if ((secId.IndexOf(dr["SecId"].ToString()) == -1) && (dr["PoolCode"].ToString().Equals(poolCode)) && (dr["BookGroup"].ToString().Equals(bookGroup)))
          {
            secId.Add(dr["SecId"].ToString());
          }
        }

        for (int i = 0; i < secId.Count; i++)
        {         
          allocatedBorrows = 0;

          exDeficitSettled = (long)pbMainForm.PreBorrowAgent.SecMasterLookup(secId[i].ToString(), true, false, 0, "").Tables["BoxPosition"].Rows[0]["ExDeficitSettled"];          
          
          allocatedBorrows = 0;         
          symbol = "";

          foreach (DataRow dr1 in dsPreBorrowRecords.Tables["PreBorrowRecords"].Select("SecId = '" + secId[i].ToString() + "'"))
          {
            allocatedBorrows += (long)dr1["CoveredQuantity"];
            symbol = dr1["Symbol"].ToString();
          }

          totalPreBorrows = 0;

          foreach (DataRow dr in dsContracts.Tables["Contracts"].Select("SecId = '" + secId[i].ToString() + "'"))
          {
            if ((dr["PoolCode"].ToString().Equals(poolCode)) && (dr["BookGroup"].ToString().Equals(bookGroup)))
            {
              totalPreBorrows += (long)dr["Quantity"];
              symbol = dr["Symbol"].ToString();
            }
          }

          if (totalPreBorrows > 0)
          {
            if (exDeficitSettled >= 0)
            {
              excess = totalPreBorrows - allocatedBorrows;
            }
            else
            {
              excess = (totalPreBorrows - Math.Abs(exDeficitSettled)) - allocatedBorrows;
            }

            if (excess > 0)
            {      
              DataRow dataRow = dsExcess.Tables["Excess"].NewRow();


              dataRow["SecId"] = symbol;
              dataRow["Quantity"] = excess;
              dsExcess.Tables["Excess"].Rows.Add(dataRow);
            }
          }
        }

        ExcessGrid.SetDataBinding(dsExcess, "Excess", true);
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }
    
    
    private void CalculateNeeds()
    {
      ArrayList secId = new ArrayList();
      string symbol;
      long coveredQuantity = 0;
      long requestedQuantity = 0;
      long netPositionSettled = 0;
      long totalPreBorrows = 0;
      long needs = 0;

      string bookGroup = "0234";
      string poolCode = "P";

      if (StartDatePicker.Text != StopDatePicker.Text)
      {        
        return;
      }

      try
      {
        dsNeeds = new DataSet();
        dsNeeds.Tables.Add("Needs");
        dsNeeds.Tables["Needs"].Columns.Add("SecId");
        dsNeeds.Tables["Needs"].Columns.Add("Quantity");
        dsNeeds.AcceptChanges();

        foreach (DataRow dr in dsPreBorrowRecords.Tables["PreBorrowRecords"].Rows)
        {
          if (secId.IndexOf(dr["SecId"].ToString()) == -1)
          {
            secId.Add(dr["SecId"].ToString());
          }
        }


        for (int i = 0; i < secId.Count; i++)
        {
          netPositionSettled = (long)pbMainForm.PreBorrowAgent.SecMasterLookup(secId[i].ToString(), true, false, 0, "").Tables["BoxPosition"].Rows[0]["NetPositionSettled"];

          coveredQuantity = 0;
          requestedQuantity = 0;
          symbol = "";

          foreach (DataRow dr1 in dsPreBorrowRecords.Tables["PreBorrowRecords"].Select("SecId = '" + secId[i].ToString() + "'"))
          {
            coveredQuantity += (long)dr1["CoveredQuantity"];
            requestedQuantity += (long)dr1["RequestedQuantity"];
            symbol = dr1["Symbol"].ToString();
          }         

          totalPreBorrows = 0;

          foreach (DataRow dr in dsContracts.Tables["Contracts"].Select("SecId = '" + secId[i].ToString() + "'"))
          {
            if ((dr["PoolCode"].ToString().Equals(poolCode)) && (dr["BookGroup"].ToString().Equals(bookGroup)))
            {
              totalPreBorrows += (long)dr["Quantity"];
              symbol = dr["Symbol"].ToString();
            }
          }

          if (netPositionSettled < 0)
          {
            needs = netPositionSettled + (requestedQuantity - coveredQuantity);
          }
          else
          {
            if (coveredQuantity > totalPreBorrows)
            {
              needs = Math.Abs(coveredQuantity - totalPreBorrows);
            }
            else
            {
              needs = (requestedQuantity - coveredQuantity);
            }
          }

          if (needs > 0)
          {
            needs = (needs - (needs % 100));

            DataRow dataRow = dsNeeds.Tables["Needs"].NewRow();


            dataRow["SecId"] = symbol;
            dataRow["Quantity"] = needs;
            dsNeeds.Tables["Needs"].Rows.Add(dataRow);
          }
        }

        NeedsGrid.SetDataBinding(dsNeeds, "Needs", true);
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }
    
    private void SecMasterFill(string secId)
    {
      long totalBorrows = 0;
      long totalPreBorrows = 0;
      long allocatedBorrows = 0;
      long netPositionSettled = 0;

      string bookGroup = "0234";
      string poolCode = "P";
      string secIdName = "";
      
      if (StartDatePicker.Text != StopDatePicker.Text)
      {
        return;
      }

      try
      {
        dsInventory = pbMainForm.PreBorrowAgent.InventoryGet(secId, (short)pbMainForm.UtcOffset, false);        
        dsSecMasterItem = pbMainForm.PreBorrowAgent.SecMasterLookup(secId.ToString(), true, false, 0, "");


        netPositionSettled = (long)dsSecMasterItem.Tables["BoxPosition"].Rows[0]["NetPositionSettled"];
        secIdName = dsSecMasterItem.Tables["SecMasterItem"].Rows[0]["Description"].ToString();

        foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
        {
          if ((dr["BookGroup"].ToString().Equals(bookGroup)) && (secId.Equals(dr["SecId"].ToString())))
          {
            totalBorrows += (long)dr["Quantity"];

            if (dr["PoolCode"].ToString().Equals(poolCode))
            {
              totalPreBorrows += (long)dr["Quantity"];
            }
          }
        }

        foreach (DataRow dr in dsPreBorrowRecords.Tables["PreBorrowRecords"].Rows)
        {
          if (dr["SecId"].ToString().Equals(secId))
          {
            allocatedBorrows += (long)dr["CoveredQuantity"];
          }
        }

        NetPositionSettledTextBox.Value = netPositionSettled;
        TotalBorrowsTextBox.Value = totalBorrows;
        TotalPreBorrowsTextBox.Value = totalPreBorrows;
        AllocatedBorrowsTextBox.Value = allocatedBorrows;

        SecIdTextBox.Value = secId;
        SecurityNameLabel.Text = secIdName;

        InventoryGrid.SetDataBinding(dsInventory, "Inventory", true);
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }

    public void CalculateTotals()
    {
      if (StartDatePicker.Text != StopDatePicker.Text)
      {
        return;
      }

      decimal totalModifiedCharge = 0;
      decimal totalOrignalCharge = 0;

      try
      {
        for (int i = 0; i < SummaryGrid.Splits[0].Rows.Count; i++)
        {
          try
          {
            totalOrignalCharge += (decimal)SummaryGrid.Columns["Charge"].CellValue(i);
          }
          catch { }

          try
          {
            totalModifiedCharge += (decimal)SummaryGrid.Columns["ModifiedCharge"].CellValue(i);
          }
          catch { }
        }

        SummaryGrid.Columns["Charge"].FooterText = totalOrignalCharge.ToString("#,##0.00");
        SummaryGrid.Columns["ModifiedCharge"].FooterText = totalModifiedCharge.ToString("#,##0.00");
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }
    
    
    private void PreBorrowSummaaryForm_Load(object sender, EventArgs e)
    {
      try
      {
        StartDatePicker.Value = DateTime.Now;
        StopDatePicker.Value = DateTime.Now;


        dsPreBorrowRecords = pbMainForm.PreBorrowAgent.PreBorrowRecordGet("", StartDatePicker.Value.ToString(Standard.DateFormat), StopDatePicker.Value.ToString(Standard.DateFormat), "", "", "", (short)pbMainForm.UtcOffset);
        SummaryGrid.SetDataBinding(dsPreBorrowRecords, "PreBorrowRecords", true);

        LoadContractData();
        CalculateNeeds();
        CalculateExcess();
        CalculateFails();
        SecMasterFill(SummaryGrid.Columns["SecId"].Text);
        CalculateTotals();
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }

    private void PreBorrowSummaryForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      pbMainForm.preBorrowSummaryForm = null;
    }

 
    private void SummaryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {
      switch (e.Column.DataField)
      {
        case "BizDate":
          try
          {
            e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
          }
          catch { }
          break;
        
        
        case "OpenTime":
          try
          {
            e.Value = DateTime.Parse(e.Value.ToString()).ToString("HH:mm:ss.ff");
          }
          catch { }
          break;
        
        case "ActTime":
          try
          {
            e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeFormat);
          }
          catch { }
          break;

        case "LocateQuantity":
        case "RequestedQuantity":
        case "CoveredQuantity":
        case  "TradeDateShortQuantity":
          try
          {
            e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
          }
          catch { }
          break;

        case "CoveredAmount":
        case "Charge":
        case "ModifiedCharge":
          try
          {
            e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.00");
          }
          catch { }
          break;
      
        case "Rate":
        case "ModifiedRate":      
          try
          {
            e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.000");
          }
          catch { }
          break;


      }
    }

    private void FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {
      switch (e.Column.DataField)
      {
        case "Quantity":
          try
          {
            e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
          }
          catch { }
          break;
      }
    }

    private void Formatting(object sender, C1.Win.C1Input.FormatEventArgs e)
    {
      try
      {
        e.Text = long.Parse(e.Value.ToString()).ToString("#,##0");
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }

    private void RefreshButton_Click(object sender, EventArgs e)
    {
      try
      {
        dsPreBorrowRecords = pbMainForm.PreBorrowAgent.PreBorrowRecordGet("", StartDatePicker.Value.ToString(Standard.DateFormat), StopDatePicker.Value.ToString(Standard.DateFormat), "", "", "", (short)pbMainForm.UtcOffset);
        SummaryGrid.SetDataBinding(dsPreBorrowRecords, "PreBorrowRecords", true);

        LoadContractData();
        CalculateNeeds();
        CalculateExcess();
        CalculateFails();
        SecMasterFill(SummaryGrid.Columns["SecId"].Text);
        CalculateTotals();
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }

    private void SummaryGrid_Paint(object sender, PaintEventArgs e)
    {
      try
      {
        if (!SummaryGrid.Columns["SecId"].Text.Equals(secId))
        {
          secId = SummaryGrid.Columns["SecId"].Text;
          SecMasterFill(secId);
        }

        ContactEmailAddressTextBox.Text = SummaryGrid.Columns["ContactEmailAddress"].Text;
        ContactNameTextBox.Text = SummaryGrid.Columns["ContactName"].Text;
        ContactPhoneNumberTextBox.Text = SummaryGrid.Columns["ContactPhoneNumber"].Text;
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }


    private void SummaryGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        pbMainForm.PreBorrowAgent.PreBorrowRecordSet(
          SummaryGrid.Columns["Identity"].Text,
          SummaryGrid.Columns["BizDate"].Text,
          SummaryGrid.Columns["OpenTime"].Value.ToString(),
          SummaryGrid.Columns["GroupCode"].Text,
          SummaryGrid.Columns["SecId"].Text,
          SummaryGrid.Columns["TradeDateShortQuantity"].Value.ToString(),
          SummaryGrid.Columns["RequestedQuantity"].Value.ToString(),
          SummaryGrid.Columns["CoveredQuantity"].Value.ToString(),
          SummaryGrid.Columns["CoveredAmount"].Value.ToString(),
          SummaryGrid.Columns["Rate"].Value.ToString(),
          SummaryGrid.Columns["ModifiedRate"].Value.ToString(),
          SummaryGrid.Columns["Charge"].Value.ToString(),
          SummaryGrid.Columns["ModifiedCharge"].Value.ToString(),
          SummaryGrid.Columns["ContactName"].Text,
          SummaryGrid.Columns["ContactPhoneNumber"].Text,
          SummaryGrid.Columns["ContactEmailAddress"].Text,
          pbMainForm.UserId,
         bool.Parse(SummaryGrid.Columns["IsContacted"].Value.ToString()));

        DataSet dsTemp = pbMainForm.PreBorrowAgent.PreBorrowRecordGet(
          SummaryGrid.Columns["Identity"].Text,
          SummaryGrid.Columns["BizDate"].Text,
          SummaryGrid.Columns["BizDate"].Text,
          SummaryGrid.Columns["OpenTime"].Value.ToString(),
          SummaryGrid.Columns["GroupCode"].Text,
          SummaryGrid.Columns["SecId"].Text,
          (short) pbMainForm.UtcOffset);

        foreach (DataRow dr in dsTemp.Tables["PreBorrowRecords"].Rows)
        {
          dsPreBorrowRecords.Tables["PreBorrowRecords"].LoadDataRow(dr.ItemArray, true);
        }

        dsPreBorrowRecords.AcceptChanges();
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;        
      }
    }

    private void ContactNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals((char)13))
      {
        try
        {
          SummaryGrid.UpdateData();
        }
        catch (Exception error)
        {
          StatusMessageLabel.Text = error.Message;          
        }
      }
    }

    private void SummaryGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
    {
      try
      {
        if ((bool)SummaryGrid.Columns["IsContacted"].CellValue(e.Row))
        {
          e.CellStyle.BackColor = System.Drawing.Color.GhostWhite;
        }
        else
        {
          e.CellStyle.BackColor = System.Drawing.Color.LightYellow;
        }
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }

    private void SummaryGrid_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals((char)13))
      {
        try
        {
          SummaryGrid.UpdateData();
        }
        catch (Exception error)
        {
          StatusMessageLabel.Text = error.Message;          
        }
      }
    }

    private void SnapShotButton_Click(object sender, EventArgs e)
    {
      try
      {
        if (!pbMainForm.PreBorrowAgent.KeyValueGet("PreBorrowBillingSnapShotBizDate", "").Equals(pbMainForm.PreBorrowAgent.BizDate()))
        {
          pbMainForm.PreBorrowAgent.PreBorrowStartOfDaySnapShot();

          dsPreBorrowRecords = pbMainForm.PreBorrowAgent.PreBorrowRecordGet("", StartDatePicker.Value.ToString(Standard.DateFormat), StopDatePicker.Value.ToString(Standard.DateFormat), "", "", "", (short)pbMainForm.UtcOffset);
          SummaryGrid.SetDataBinding(dsPreBorrowRecords, "PreBorrowRecords", true);

          LoadContractData();
          CalculateNeeds();
          CalculateExcess();
          CalculateFails();
          SecMasterFill(SummaryGrid.Columns["SecId"].Text);
          CalculateTotals();
        }
        else
        {
          StatusMessageLabel.Text = "SOD Snapshot already taken.";
        }
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }

    private void ContractRefreshTimer_Tick(object sender, EventArgs e)
    {
      try
      {
        LoadContractData();
        CalculateNeeds();
        CalculateExcess();
        CalculateFails();
        SecMasterFill(SummaryGrid.Columns["SecId"].Text);
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;        
      }
    }

    private void ShowOriginalRateCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
    {
      SummaryGrid.Splits[0].DisplayColumns["Rate"].Visible = !SummaryGrid.Splits[0].DisplayColumns["Rate"].Visible;
      ShowOriginalRateCommand.Checked = SummaryGrid.Splits[0].DisplayColumns["Rate"].Visible;
    }

    private void ShowOriginalChargeCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
    {
      SummaryGrid.Splits[0].DisplayColumns["Charge"].Visible = !SummaryGrid.Splits[0].DisplayColumns["Charge"].Visible;
      ShowOriginalChargeCommand.Checked = SummaryGrid.Splits[0].DisplayColumns["Charge"].Visible;
    }
  }
}