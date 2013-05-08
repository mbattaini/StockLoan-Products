using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;

namespace PreborrowClient
{
  public partial class PreBorrowMarksForm : Form
  {
    private DataSet marksDataSet = null;
    private DataSet tradingGroupsDataSet = null;

    private PreborrowMainForm pbMainForm = null;

    public PreBorrowMarksForm(PreborrowMainForm pbMainForm)
    {
      InitializeComponent();

      this.pbMainForm = pbMainForm;
    }

    private void PreBorrowMarksForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      pbMainForm.preBorrowMarksForm = null;
    }

    private void PreBorrowMarksForm_Load(object sender, EventArgs e)
    {
      try
      {
        marksDataSet = pbMainForm.PreBorrowAgent.PreBorrowGroupCodeMarkupGet("", (short)pbMainForm.UtcOffset);
        MarksGrid.SetDataBinding(marksDataSet, "Marks", true);

        tradingGroupsDataSet = pbMainForm.PreBorrowAgent.TradingGroupsGet("", 0);
        TradingGroupDropdown.SetDataBinding(tradingGroupsDataSet, "TradingGroups", true);

        DefaultMarkUpTextBox.Value = pbMainForm.PreBorrowAgent.KeyValueGet("ShortSalePreBorrowDefaultMarkup", "");
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;        
      }
    }

    private void DefaultMarkUpTextBox_Formatting(object sender, C1.Win.C1Input.FormatEventArgs e)
    {
      try
      {
        e.Text = decimal.Parse(e.Value.ToString()).ToString("##0.00");
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;        
      }
    }

    private void MarksGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {
      switch (e.Column.DataField)
      {
        case "Markup":
          try
          {
            e.Value = decimal.Parse(e.Value.ToString()).ToString("##0.000");
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
      }
    }

    private void MarksGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        pbMainForm.PreBorrowAgent.PreBorrowGroupCodeMarkupSet(
         MarksGrid.Columns["GroupCode"].Text,
         MarksGrid.Columns["Markup"].Text,
         pbMainForm.UserId,
         false
         );

        StatusMessageLabel.Text = "Deleted markup for : " + MarksGrid.Columns["GroupCode"].Text;
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;        
        e.Cancel = true;
      }
    }

    private void DefaultMarkUpTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      try
      {
        if (e.KeyChar.Equals((char)13))
        {
          pbMainForm.PreBorrowAgent.KeyValueSet("ShortSalePreBorrowDefaultMarkup", DefaultMarkUpTextBox.Text);
          StatusMessageLabel.Text = "Default markup set to: " + DefaultMarkUpTextBox.Text;
        }
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;        
      }
    }

    private void MarksGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        if (MarksGrid.Columns["GroupCode"].Text.Equals("") ||
            MarksGrid.Columns["Markup"].Text.Equals(""))
        {
          StatusMessageLabel.Text = "GroupCode and Markup required!";
          
          e.Cancel = true;
          return;
        }                    
        
        pbMainForm.PreBorrowAgent.PreBorrowGroupCodeMarkupSet(
        MarksGrid.Columns["GroupCode"].Text,
        MarksGrid.Columns["Markup"].Text,
        pbMainForm.UserId,
        true
        );

        MarksGrid.Columns["ActUserId"].Value = pbMainForm.UserId;
        MarksGrid.Columns["ActTime"].Value = DateTime.Now;
        MarksGrid.Columns["IsActive"].Value = true;

        StatusMessageLabel.Text = "Updated markup for : " + MarksGrid.Columns["GroupCode"].Text;
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;        
        e.Cancel = true;
      }
    }

    private void MarksGrid_KeyPress(object sender, KeyPressEventArgs e)
    {
      try
      {
        if (e.KeyChar.Equals((char)13))
        {
          MarksGrid.UpdateData();
        }
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;        
      }
    }
  }
}