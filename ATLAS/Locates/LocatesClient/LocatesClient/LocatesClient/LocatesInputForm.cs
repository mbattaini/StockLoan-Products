using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LocatesClient
{
  public partial class LocatesInputForm : C1.Win.C1Ribbon.C1RibbonForm
  {
	  private DataSet dsTradingGroups = null;
	  private MainForm mainForm = null;

    public LocatesInputForm(MainForm mainForm)
    {
      InitializeComponent();
	  this.mainForm = mainForm;
    }

	  private void SubmitButton_Click(object sender, EventArgs e)
	  {
		  this.Cursor = Cursors.WaitCursor;

		  SubmitButton.Enabled = false;
		  ClearButton.Enabled = false;

		  try
		  {
			  StatusMessageLabel.Text = mainForm.ShortSaleAgent.LocateListSubmit(mainForm.UserId,
				  TradingGroupCombo.Text, "", InputTextBox.Text);
		  }
		  catch (Exception error)
		  {
			  StatusMessageLabel.Text = error.Message;
		  }

		  this.Cursor = Cursors.Default;

		  SubmitButton.Enabled = true;
		  ClearButton.Enabled = true;
	  }

	  private void LocatesInputForm_Load(object sender, EventArgs e)
	  {
		  this.Cursor = Cursors.WaitCursor;

		  try
		  {
			  dsTradingGroups = mainForm.ShortSaleAgent.TradingGroupsGet(mainForm.ServiceAgent.BizDate(), mainForm.UtcOffset);
			  TradingGroupCombo.HoldFields();
			  TradingGroupCombo.DataSource = dsTradingGroups.Tables["TradingGroups"];

			  if (dsTradingGroups.Tables["TradingGroups"].Rows.Count > 0)
			  {
				  TradingGroupCombo.Row = 1;
			  }
		  }
		  catch (Exception error)
		  {
			  mainForm.Alert(this.Name, error.Message);
		  }

		  this.Cursor = Cursors.Default;
	  }

	  private void ClearButton_Click(object sender, EventArgs e)
	  {
		  InputTextBox.Clear();
	  }

	  private void LocatesInputForm_FormClosed(object sender, FormClosedEventArgs e)
	  {
		  mainForm.locatesInputForm = null;
	  }
  }
}