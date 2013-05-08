using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.PreBorrow;
using StockLoan.Common;

namespace PreborrowClient
{
  public partial class PreBorrowContactsForm : Form
  {

    private DataSet dsContacts = null;
    private DataSet dsTradingGroups = null;
    private PreborrowMainForm pbMainForm = null;

    public PreBorrowContactsForm(PreborrowMainForm pbMainForm)
    {
      InitializeComponent();

      this.pbMainForm = pbMainForm;
    }

    private void PreBorrowContactsForm_Load(object sender, EventArgs e)
    {
      try
      {
        dsContacts = pbMainForm.PreBorrowAgent.PreBorrowContactGet("", (short)pbMainForm.UtcOffset);
        ContactsGrid.SetDataBinding(dsContacts, "Contacts", true);

        dsTradingGroups = pbMainForm.PreBorrowAgent.TradingGroupsGet("", 0);
        TradingGroupDropdown.SetDataBinding(dsTradingGroups, "TradingGroups", true);
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }

    private void PreBorrowContactsForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      pbMainForm.preBorrowContactsForm = null;
    }

    private void ContactsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        if (ContactsGrid.Columns["GroupCode"].Text.Equals("") ||
            ContactsGrid.Columns["FirstName"].Text.Equals("") ||
            ContactsGrid.Columns["LastName"].Text.Equals(""))
        {
          StatusMessageLabel.Text = "GroupCode, First Name, and Last Name required!";

          e.Cancel = true;
          return;
        }

        pbMainForm.PreBorrowAgent.PreBorrowContactSet(
        ContactsGrid.Columns["GroupCode"].Text,
        ContactsGrid.Columns["FirstName"].Text,
        ContactsGrid.Columns["LastName"].Text,
        ContactsGrid.Columns["PhoneNumber"].Text,
        ContactsGrid.Columns["EmailAddress"].Text,
        pbMainForm.UserId,
        true
        );

        ContactsGrid.Columns["ActUserId"].Value = pbMainForm.UserId;
        ContactsGrid.Columns["ActTime"].Value = DateTime.Now;
        ContactsGrid.Columns["IsActive"].Value = true;

        StatusMessageLabel.Text = "Updated contact for : " + ContactsGrid.Columns["GroupCode"].Text;
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
        e.Cancel = true;
      }
    }

    private void ContactsGrid_KeyPress(object sender, KeyPressEventArgs e)
    {
      try
      {
        if (e.KeyChar.Equals((char)13))
        {
          ContactsGrid.UpdateData();
        }
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }

    private void ContactsGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {
      switch (e.Column.DataField)
      {
        case "ActTime":
          try
          {
            e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeFormat);
          }
          catch { }
          break;
      }
    }
  }
}