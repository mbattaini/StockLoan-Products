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
  public partial class PreborrowMainForm : Form
  {
    public IPreBorrow PreBorrowAgent = null;

    public PreBorrowInputFrom preBorrowInputForm = null;
    public PreBorrowMarksForm preBorrowMarksForm = null;
    public PreBorrowSummaryForm preBorrowSummaryForm = null;
    public PreBorrowContactsForm preBorrowContactsForm = null;

    public PreborrowMainForm()
    {
      InitializeComponent();
    }

    private void PreborrowMainForm_Load(object sender, EventArgs e)
    {
      try
      {
        Log.Level = Standard.ConfigValue("LogLevel");
        Log.FilePath = Standard.ConfigValue("LogFilePath");

        PreBorrowAgent = (IPreBorrow)RemotingTools.ObjectGet(typeof(IPreBorrow));

        if (PreBorrowAgent == null)
        {
          Log.Write("Remoting config values for the admin agent (IAdmin) are not correct. [MainForm.MainForm_Load]", Log.Error, 1);
        }
      }
      catch (Exception error)
      {
        Log.Write(error.Message, 3);
      }
    }

    private void PreBorrowInputFormCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
    {
      if (preBorrowInputForm == null)
      {
        preBorrowInputForm = new PreBorrowInputFrom(this);
        preBorrowInputForm.MdiParent = this;
        preBorrowInputForm.Show();
      }
      else
      {
        preBorrowInputForm.Activate();
      }
    }

    private void PreBorrowSummaryFormCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
    {
      if (preBorrowSummaryForm == null)
      {
        preBorrowSummaryForm = new PreBorrowSummaryForm(this);
        preBorrowSummaryForm.MdiParent = this;
        preBorrowSummaryForm.Show();
      }
      else
      {
        preBorrowSummaryForm.Activate();
      }
    }

    private void PreBorrowGroupCodeMarksFormCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
    {
      if (preBorrowMarksForm == null)
      {
        preBorrowMarksForm = new PreBorrowMarksForm(this);
        preBorrowMarksForm.MdiParent = this;
        preBorrowMarksForm.Show();
      }
      else
      {
        preBorrowMarksForm.Activate();
      }
    }

    public int UtcOffset
    {
      get
      {
        return TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Minutes;
      }
    }

    public string UserId
    {
      get
      {
        if (Standard.ConfigValue("UseDomainInUserId", @"true").ToLower().Equals("true"))
        {
          return SystemInformation.UserDomainName.ToUpper() + @"\" + SystemInformation.UserName.ToLower();
        }
        else
        {
          return SystemInformation.UserName.ToLower();
        }
      }
    }

    private void PreBorrowContactsFormCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
    {
      if (preBorrowContactsForm == null)
      {
        preBorrowContactsForm = new PreBorrowContactsForm(this);
        preBorrowContactsForm.MdiParent = this;
        preBorrowContactsForm.Show();
      }
      else
      {
        preBorrowContactsForm.Activate();
      }
    }
  }
}