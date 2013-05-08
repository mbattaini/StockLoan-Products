using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using StockLoan.Common;

namespace PreborrowClient
{
  public partial class PreBorrowInputFrom : Form
  {
    private PreborrowMainForm pbMainForm = null;
    private DataSet ds = null;
    private DataSet dsContacts = null;
    private DataSet dsTradingGroups = null;

    private string contactName = "";
    private string contactPhoneNumber = "";
    private string contactEmailAddress = "";
    public PreBorrowInputFrom(PreborrowMainForm pbMainForm)
    {
      InitializeComponent();

      this.pbMainForm = pbMainForm;
    }

    private void PreBorrowInputFrom_FormClosed(object sender, FormClosedEventArgs e)
    {
      pbMainForm.preBorrowInputForm = null;
    }

    private void PreBorrowInputFrom_Load(object sender, EventArgs e)
    {
      try
      {        
        dsTradingGroups = pbMainForm.PreBorrowAgent.TradingGroupsGet("", (short)pbMainForm.UtcOffset);

        GroupCodeCombo.HoldFields();
        GroupCodeCombo.Rebind();
        GroupCodeCombo.DataSource = dsTradingGroups.Tables["TradingGroups"];

        ContactsCombo.HoldFields();
        ContactsCombo.Rebind();

        if (dsTradingGroups.Tables["TradingGroups"].Rows.Count > 0)
        {
          GroupCodeCombo.Row = 1;
        }
        
        ds = new DataSet();
        ds.Tables.Add("Input");
        ds.Tables["Input"].Columns.Add("SecId");
        ds.Tables["Input"].Columns.Add("Quantity");
        ds.Tables["Input"].AcceptChanges();
        
        InputGrid.SetDataBinding(ds, "Input");

      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }

    private void ParseListButton_Click(object sender, EventArgs e)
    {
      Input input = new Input(ListTextBox.Text);

      StatusMessageLabel.Text = input.Status;

      try
      {
        for (int i = 0; i < input.Count; i++)
        {
          DataRow dataRow = ds.Tables["Input"].NewRow();
          dataRow["SecId"] = input.SecId(i);

          if (Tools.IsNumeric(input.Quantity(i)))
          {
            dataRow["Quantity"] = Tools.ParseLong(input.Quantity(i));
          }
          else
          {
            dataRow["Quantity"] = DBNull.Value;
          }

          ds.Tables["Input"].Rows.Add(dataRow);
        }

        InputGrid.Row = InputGrid.FirstRow;
      }
      catch (Exception ee)
      {
        StatusMessageLabel.Text = ee.Message;
      }
    }

    private void ClearListButton_Click(object sender, EventArgs e)
    {
      ListTextBox.Clear();      
    }

    private void SubmitListButton_Click(object sender, EventArgs e)
    {
      int count = 0;

      if (!pbMainForm.PreBorrowAgent.KeyValueGet("PreBorrowBillingSnapShotBizDate", "").Equals(pbMainForm.PreBorrowAgent.BizDate()))
      {
        StatusMessageLabel.Text = "SOD Snapshot not taken yet.";
        return;
      }
      if (ContactNameTextBox.Text.Equals("") ||
          (ContactPhoneNumberTextBox.Text.Equals("") &&
          ContactEmailAddressTextBox.Text.Equals("")))
      {
        StatusMessageLabel.Text = "Contact Name and Phone / Email Address Required.";
        return;
      }

      foreach (DataRow dr in ds.Tables["Input"].Rows)
      {
        try
        {
          pbMainForm.PreBorrowAgent.PreBorrowRequest(
            GroupCodeCombo.Text,
            dr["SecId"].ToString(),
            long.Parse(dr["Quantity"].ToString()),
            ContactNameTextBox.Text,
            ContactPhoneNumberTextBox.Text,
            ContactEmailAddressTextBox.Text,
            pbMainForm.UserId);

          count++;
        }
        catch (Exception error)
        {
          StatusMessageLabel.Text = error.Message;
        }
      }

      StatusMessageLabel.Text = "Submitted " + count.ToString() + " preborrows.";
    }

    private void PreBorrowInputFrom_DoubleClick(object sender, EventArgs e)
    {
      if (this.Dock == DockStyle.Left)
      {
        this.Dock = DockStyle.None;
      }
      else
      {
        this.Dock = DockStyle.Left;
      }
    }
   
    private void ContactsCombo_RowChange(object sender, EventArgs e)
    {
      try
      {
        if (ContactsCombo.Row > -1)
        {
          contactName = ContactsCombo.Columns["FirstName"].CellText(ContactsCombo.Row) + " " + ContactsCombo.Columns["LastName"].CellText(ContactsCombo.Row);
          contactPhoneNumber = ContactsCombo.Columns["PhoneNumber"].CellText(ContactsCombo.Row);
          contactEmailAddress = ContactsCombo.Columns["EmailAddress"].CellText(ContactsCombo.Row);
        }
        else
        {
          contactName = "";
          contactPhoneNumber = "";
          contactEmailAddress = "";
        }
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }

    private void GroupCodeCombo_TextChanged(object sender, EventArgs e)
    {
      try
      {

        dsContacts = pbMainForm.PreBorrowAgent.PreBorrowContactGet(GroupCodeCombo.Text, 0);

        ContactsCombo.DataSource = dsContacts.Tables["Contacts"];

        ContactNameTextBox.Text = "";
        ContactPhoneNumberTextBox.Text = "";
        ContactEmailAddressTextBox.Text = "";

        contactName = "";
        contactPhoneNumber = "";
        contactEmailAddress = "";
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
    }

    private void ContactsCombo_Close(object sender, EventArgs e)
    {
      ContactNameTextBox.Text = contactName;
      ContactPhoneNumberTextBox.Text = contactPhoneNumber;
      ContactEmailAddressTextBox.Text = contactEmailAddress;
    }
  }

  public class Input
  {
    private string status = "";

    private ArrayList items;
    private ColumnIndex columnIndex;

    public Input() : this("") { }
    public Input(string list)
    {
      items = new ArrayList();
      columnIndex = new ColumnIndex();

      if (!list.Equals(""))
      {
        Parse(list);
      }
    }

    public string Parse(string list)
    {
      string[] records;
      string[] fields;

      char[] delimiter = new Char[1];

      items.Clear();
      columnIndex.Clear();

      try
      {
        delimiter[0] = '\n';

        records = list.Split(delimiter); // Do the split on new-line character; trim balance of white space later.

        for (int i = 0; i < records.Length; i++) // Parse list items.
        {
          string record = records[i].Trim(); // Taking a copy to trim just once.

          if (record.IndexOf(":") > 0) // Use ':' as delimiter for this record.
          {
            delimiter[0] = ':';
          }
          else if (record.IndexOf(";") > 0) // Use ';' as delimiter for this record.
          {
            delimiter[0] = ';';
          }
          else if (record.IndexOf("|") > 0) // Use '|' as delimiter for this record.
          {
            delimiter[0] = '|';
          }
          else if (record.IndexOf("\t") > 0) // Use tab as delimiter for this record.
          {
            delimiter[0] = '\t';
          }
          else // Must use ' ' as delimiter for this record.
          {
            delimiter[0] = ' ';

            for (int j = 25; j > 0; j--) // Replace multiple instances of space with just one.
            {
              records[i] = records[i].Replace(new String(delimiter[0], j), delimiter[0].ToString());
            }
          }

          fields = records[i].Split(delimiter);

          // ToDo: Any field manipulation.

          if (fields.Length > 3) // Hack to concatenate last two fields.
          {
            fields[2] += fields[3];
          }

          string[] values = new string[5] { "", "", "", "", "" };

          columnIndex.HaveSecId = false;

          for (int j = 0; (j < fields.Length) && (j < 4); j++)
          {
            values[j + 1] = columnIndex.Set(fields[j], j);
          }

          if (columnIndex.HaveSecId)
          {
            items.Add(values);
          }
        }

        return status = "OK";
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [Input.Parse]", 2);

        return status = "Error: Unable to parse your list.";
      }
    }

    public string SecId(int index)
    {
      string[] values = (string[])items[index];
      return values[columnIndex.SecId];
    }

    public string Quantity(int index)
    {
      string[] values = (string[])items[index];
      return values[columnIndex.Quantity];
    }

    public string Price(int index)
    {
      string[] values = (string[])items[index];
      return values[columnIndex.Price];
    }

    public string Rate(int index)
    {
      string[] values = (string[])items[index];
      return values[columnIndex.Rate];
    }

    public string Status
    {
      get
      {
        return status;
      }
    }

    public int Count
    {
      get
      {
        return items.Count;
      }
    }

    private class ColumnIndex
    {
      private int[] secIdCount = new int[4] { 0, 0, 0, 0 };
      private int[] quantityCount = new int[4] { 0, 0, 0, 0 };
      private int[] priceCount = new int[4] { 0, 0, 0, 0 };
      private int[] rateCount = new int[4] { 0, 0, 0, 0 };

      private bool haveSecId = false;

      public string Set(string field, int index)
      {
        if (index > 3)
        {
          throw new Exception("Value for index, " + index + ",  must not be greater than 3. [ListStats.Set]");
        }

        if (index < 0)
        {
          throw new Exception("Value for index, " + index + ",  must not be less than 0. [ListStats.Set]");
        }

        field = field.ToUpper().Replace(" ", "").Replace(",", "").Trim();

        if (field.StartsWith("(") && field.EndsWith(")"))
        {
          field = field.Replace("(", "-").Replace(")", "");
        }

        if (Security.IsCusip(field) || Security.IsSymbol(field))
        {
          haveSecId = true;

          secIdCount[index]++;

          return field;
        }

        if (Tools.IsNumeric(field))
        {
          decimal fieldValue = decimal.Parse(field);

          if (fieldValue < 100M)
          {
            rateCount[index]++;
          }
          else
          {
            quantityCount[index]++;
          }

          return field;
        }

        field = field.Replace("M", "000");
        field = field.Replace("K", "000");
        field = field.Replace("C", "00");
        field = field.Replace("H", "00");

        if (Tools.IsNumeric(field))
        {
          quantityCount[index]++;

          return field;
        }

        if (field.StartsWith("NEG") || field.EndsWith("NEG"))
        {
          field = "-" + field.Replace("NEG", "");

          if (Tools.IsNumeric(field))
          {
            rateCount[index]++;

            return field;
          }
        }

        if (field.EndsWith("%"))
        {
          field = field.Replace("%", "");

          if (Tools.IsNumeric(field))
          {
            rateCount[index]++;

            return field;
          }
        }

        if (field.StartsWith("N") || field.EndsWith("N"))
        {
          field = "-" + field.Replace("N", "");

          if (Tools.IsNumeric(field))
          {
            rateCount[index]++;

            return field;
          }
        }

        if (field.StartsWith("P") || field.EndsWith("P"))
        {
          field.Replace("P", "");

          if (Tools.IsNumeric(field))
          {
            priceCount[index]++;
          }
        }

        return "";
      }

      public void Clear()
      {
        for (int i = 0; i < 4; i++)
        {
          secIdCount[i] = 0;
          quantityCount[i] = 0;
          priceCount[i] = 0;
          rateCount[i] = 0;
        }
      }

      public bool HaveSecId
      {
        set
        {
          haveSecId = value;
        }

        get
        {
          return haveSecId;
        }
      }

      public int SecId
      {
        get
        {
          int index = 0;
          int count = 0;

          for (int i = 0; i < 4; i++)
          {
            if (secIdCount[i] > count)
            {
              count = secIdCount[i];
              index = i + 1;
            }
          }

          return index;
        }
      }

      public int Quantity
      {
        get
        {
          int index = 0;
          int count = 0;

          for (int i = 0; i < 4; i++)
          {
            if (quantityCount[i] > count)
            {
              count = quantityCount[i];
              index = i + 1;
            }
          }

          return index;
        }
      }

      public int Price
      {
        get
        {
          int index = 0;
          int count = 0;

          for (int i = 0; i < 4; i++)
          {
            if (priceCount[i] > count)
            {
              count = priceCount[i];
              index = i + 1;
            }
          }

          return index;
        }
      }

      public int Rate
      {
        get
        {
          int index = 0;
          int count = 0;

          for (int i = 0; i < 4; i++)
          {
            if (rateCount[i] > count)
            {
              count = rateCount[i];
              index = i + 1;
            }
          }

          return index;
        }
      }
    }
  }
}