// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{ 
  public class PositionBankLoanReleaseInputForm : System.Windows.Forms.Form
  {    
    private MainForm mainForm;
    
		private int		releaseCount;
		
		private	long	customerQuantityOnPledge;
		private	long	firmQuantityOnPledge;
		private long	specialQuantityOnPledge;
		private long	quantityOnPledge;
		private long	quantityActivityPending;
		private long	releaseQuantity;
    
		private	string secId;
		private string bookGroup;
    
		private DataSet dataSet, dataSetActivity;

    private C1.Win.C1Input.C1Label SecDescriptionLabel;
    
    private C1.Win.C1Input.C1Label StatusLabel;
    private C1.Win.C1Input.C1Label StatusMessageLabel;
    
    private System.Windows.Forms.Button SubmitButton;
    private System.Windows.Forms.Button CloseButton;
		private C1.Win.C1Input.C1Label ReleaseQuantityLabel;
		private C1.Win.C1Input.C1TextBox ReleaseQuantityTextBox;
		private C1.Win.C1Input.C1Label c1Label1;
		private System.Windows.Forms.CheckBox LoanTypeCustomerCheckBox;
		private System.Windows.Forms.CheckBox LoanTypeFirmCheckBox;
		private System.Windows.Forms.CheckBox LoanTypeSpecialCheckBox;    

    private System.ComponentModel.Container components = null;

    private delegate void SendReleasessDelegate(DataSet dataSet, string bookGroup, string secId, long releaseQuantity, long releaseCount);    

    public PositionBankLoanReleaseInputForm(MainForm mainForm, string bookGroup, string secId)
    {    
      InitializeComponent();
      this.mainForm = mainForm;         
      
      try
      {        
        this.secId = secId;
				this.bookGroup = bookGroup;

        mainForm.SecId = secId;
        SecDescriptionLabel.Text = mainForm.Description.Split('|')[0];   
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionBankLoanReleaseInputForm.PositionBankLoanReleaseInputForm]", Log.Error, 1);
      }  
    }
    
    protected override void Dispose( bool disposing )
    {
      if (disposing)
      {
        if (components != null)
        {
          components.Dispose();
        }
      }

      base.Dispose(disposing);
    }

		private void SendReleases(DataSet dataSet, string bookGroup, string secId, long releaseQuantity, long releaseCount)
		{
			int n = 0;
			int errorCount = 0;			
			long currentQuantity = 0;			

			foreach (DataRow dataRow in dataSet.Tables["BankLoanReleaseSummary"].Rows)			
			{
				if (releaseQuantity > 0)
				{
					try
					{											
						if (dataRow["BookGroup"].ToString().Equals(bookGroup) 
							&& dataRow["SecId"].ToString().Equals(secId))
						{							
							currentQuantity = long.Parse(dataRow["Quantity"].ToString());

							if ((dataRow["LoanType"].ToString().Equals("F") && LoanTypeFirmCheckBox.Checked)
								|| (dataRow["LoanType"].ToString().Equals("C") && LoanTypeCustomerCheckBox.Checked)
								|| (dataRow["LoanType"].ToString().Equals("S") && LoanTypeSpecialCheckBox.Checked))
							{
								if (currentQuantity > releaseQuantity)
								{
									currentQuantity = releaseQuantity;
								}
								
								n++;
								mainForm.PositionAgent.BankLoanReleaseSet(
									dataRow["BookGroup"].ToString(),
									dataRow["Book"].ToString(),
									"",
									dataRow["LoanDate"].ToString(),
									dataRow["LoanType"].ToString(),
									dataRow["ActivityType"].ToString(),
									dataRow["SecId"].ToString(),
									currentQuantity.ToString(),
									"",
									"RR",
									mainForm.UserId);
			
								releaseQuantity -= currentQuantity;								
							}				
						}							
					}
					catch (Exception e)
					{          
						Log.Write(e.Message + " [PositionBankLoanReleaseInputForm.SendContracts]", Log.Error, 1);
						mainForm.Alert(e.Message, PilotState.RunFault);

						errorCount ++;
					}
					
					StatusMessageLabel.Text = "Submited " + n + " release" + (n.Equals((int)1) ? "" : "s") +
						" of " + releaseCount.ToString() + " with " + errorCount.ToString() + " initial error" +
						(errorCount.Equals((int)1) ? "" : "s") + ".";
				}         
			}
		
			StatusMessageLabel.Text = "Done!  Submitted total of " + n + " release" + (n.Equals((int)1) ? "" : "s") +
				" with " + errorCount.ToString() + " initial error" + (errorCount.Equals((int)1) ? "" : "s") + ".";
		}

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			this.SecDescriptionLabel = new C1.Win.C1Input.C1Label();
			this.ReleaseQuantityLabel = new C1.Win.C1Input.C1Label();
			this.ReleaseQuantityTextBox = new C1.Win.C1Input.C1TextBox();
			this.SubmitButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.StatusMessageLabel = new C1.Win.C1Input.C1Label();
			this.LoanTypeCustomerCheckBox = new System.Windows.Forms.CheckBox();
			this.LoanTypeFirmCheckBox = new System.Windows.Forms.CheckBox();
			this.c1Label1 = new C1.Win.C1Input.C1Label();
			this.LoanTypeSpecialCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.SecDescriptionLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ReleaseQuantityLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ReleaseQuantityTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
			this.SuspendLayout();
			// 
			// SecDescriptionLabel
			// 
			this.SecDescriptionLabel.Font = new System.Drawing.Font("Verdana", 9F);
			this.SecDescriptionLabel.ForeColor = System.Drawing.Color.Maroon;
			this.SecDescriptionLabel.Location = new System.Drawing.Point(28, 8);
			this.SecDescriptionLabel.Name = "SecDescriptionLabel";
			this.SecDescriptionLabel.Size = new System.Drawing.Size(268, 28);
			this.SecDescriptionLabel.TabIndex = 2;
			this.SecDescriptionLabel.Tag = null;
			this.SecDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.SecDescriptionLabel.TextDetached = true;
			// 
			// ReleaseQuantityLabel
			// 
			this.ReleaseQuantityLabel.Location = new System.Drawing.Point(52, 52);
			this.ReleaseQuantityLabel.Name = "ReleaseQuantityLabel";
			this.ReleaseQuantityLabel.Size = new System.Drawing.Size(116, 20);
			this.ReleaseQuantityLabel.TabIndex = 4;
			this.ReleaseQuantityLabel.Tag = null;
			this.ReleaseQuantityLabel.Text = "Release Quantity:";
			this.ReleaseQuantityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ReleaseQuantityLabel.TextDetached = true;
			// 
			// ReleaseQuantityTextBox
			// 
			this.ReleaseQuantityTextBox.AutoSize = false;
			this.ReleaseQuantityTextBox.CustomFormat = "###,###,###,##0";
			this.ReleaseQuantityTextBox.DataType = typeof(long);
			this.ReleaseQuantityTextBox.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.ReleaseQuantityTextBox.Location = new System.Drawing.Point(180, 52);
			this.ReleaseQuantityTextBox.MaxLength = 15;
			this.ReleaseQuantityTextBox.Name = "ReleaseQuantityTextBox";
			this.ReleaseQuantityTextBox.Size = new System.Drawing.Size(108, 20);
			this.ReleaseQuantityTextBox.TabIndex = 5;
			this.ReleaseQuantityTextBox.Tag = null;
			this.ReleaseQuantityTextBox.TrimEnd = false;
			this.ReleaseQuantityTextBox.TextChanged += new System.EventHandler(this.ReturnQuantityTextBox_TextChanged);
			this.ReleaseQuantityTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ReturnQuantityTextBox_KeyPress);
			// 
			// SubmitButton
			// 
			this.SubmitButton.Location = new System.Drawing.Point(44, 260);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(84, 24);
			this.SubmitButton.TabIndex = 11;
			this.SubmitButton.Text = "Submit";
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			// 
			// CloseButton
			// 
			this.CloseButton.Location = new System.Drawing.Point(204, 260);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(84, 24);
			this.CloseButton.TabIndex = 12;
			this.CloseButton.Text = "Close";
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// StatusLabel
			// 
			this.StatusLabel.Location = new System.Drawing.Point(36, 192);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(48, 16);
			this.StatusLabel.TabIndex = 13;
			this.StatusLabel.Tag = null;
			this.StatusLabel.Text = "Status:";
			this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.StatusLabel.TextDetached = true;
			// 
			// StatusMessageLabel
			// 
			this.StatusMessageLabel.ForeColor = System.Drawing.Color.Maroon;
			this.StatusMessageLabel.Location = new System.Drawing.Point(104, 192);
			this.StatusMessageLabel.Name = "StatusMessageLabel";
			this.StatusMessageLabel.Size = new System.Drawing.Size(184, 56);
			this.StatusMessageLabel.TabIndex = 14;
			this.StatusMessageLabel.Tag = null;
			this.StatusMessageLabel.TextDetached = true;
			// 
			// LoanTypeCustomerCheckBox
			// 
			this.LoanTypeCustomerCheckBox.Checked = true;
			this.LoanTypeCustomerCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.LoanTypeCustomerCheckBox.Location = new System.Drawing.Point(184, 80);
			this.LoanTypeCustomerCheckBox.Name = "LoanTypeCustomerCheckBox";
			this.LoanTypeCustomerCheckBox.TabIndex = 15;
			this.LoanTypeCustomerCheckBox.Text = "Customer";
			this.LoanTypeCustomerCheckBox.CheckedChanged += new System.EventHandler(this.LoanTypeCustomerCheckBox_CheckedChanged);
			// 
			// LoanTypeFirmCheckBox
			// 
			this.LoanTypeFirmCheckBox.Checked = true;
			this.LoanTypeFirmCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.LoanTypeFirmCheckBox.Location = new System.Drawing.Point(184, 112);
			this.LoanTypeFirmCheckBox.Name = "LoanTypeFirmCheckBox";
			this.LoanTypeFirmCheckBox.TabIndex = 16;
			this.LoanTypeFirmCheckBox.Text = "Firm";
			this.LoanTypeFirmCheckBox.CheckedChanged += new System.EventHandler(this.LoanTypeCustomerCheckBox_CheckedChanged);
			// 
			// c1Label1
			// 
			this.c1Label1.Location = new System.Drawing.Point(16, 80);
			this.c1Label1.Name = "c1Label1";
			this.c1Label1.Size = new System.Drawing.Size(152, 20);
			this.c1Label1.TabIndex = 17;
			this.c1Label1.Tag = null;
			this.c1Label1.Text = "Release From Loan Type:";
			this.c1Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.c1Label1.TextDetached = true;
			// 
			// LoanTypeSpecialCheckBox
			// 
			this.LoanTypeSpecialCheckBox.Checked = true;
			this.LoanTypeSpecialCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.LoanTypeSpecialCheckBox.Location = new System.Drawing.Point(184, 144);
			this.LoanTypeSpecialCheckBox.Name = "LoanTypeSpecialCheckBox";
			this.LoanTypeSpecialCheckBox.TabIndex = 18;
			this.LoanTypeSpecialCheckBox.Text = "Special";
			// 
			// PositionBankLoanReleaseInputForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(334, 295);
			this.Controls.Add(this.LoanTypeSpecialCheckBox);
			this.Controls.Add(this.c1Label1);
			this.Controls.Add(this.LoanTypeFirmCheckBox);
			this.Controls.Add(this.LoanTypeCustomerCheckBox);
			this.Controls.Add(this.StatusMessageLabel);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.SubmitButton);
			this.Controls.Add(this.ReleaseQuantityTextBox);
			this.Controls.Add(this.ReleaseQuantityLabel);
			this.Controls.Add(this.SecDescriptionLabel);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PositionBankLoanReleaseInputForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Position - BankLoan - Release Input";
			this.Load += new System.EventHandler(this.PositionBankLoanReleaseInputForm_Load);
			this.Closed += new System.EventHandler(this.PositionBankLoanReleaseInputForm_Closed);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.PositionBankLoanReleaseInputForm_Paint);
			((System.ComponentModel.ISupportInitialize)(this.SecDescriptionLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ReleaseQuantityLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ReleaseQuantityTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
			this.ResumeLayout(false);

		}
    #endregion
 
    private void PositionBankLoanReleaseInputForm_Load(object sender, System.EventArgs e)
    {      
      this.WindowState = FormWindowState.Normal;
      
			try
			{                               
				dataSet = mainForm.PositionAgent.BankLoanReleaseSummaryGet(secId);
				dataSetActivity = mainForm.PositionAgent.BankLoanActivityGet(0);

				quantityOnPledge = 0;
				quantityActivityPending = 0;
				releaseCount = 0;

				
				foreach (DataRow dataRow in dataSetActivity.Tables["Activity"].Rows)
				{
					if (dataRow["SecId"].ToString().Equals(secId) && dataRow["Status"].ToString().Equals("RR"))
					{					
						quantityActivityPending += long.Parse(dataRow["Quantity"].ToString());	
					}
				}
				
				foreach (DataRow dataRow in dataSet.Tables["BankLoanReleaseSummary"].Rows)
				{
					if (dataRow["SecId"].ToString().Equals(secId))
					{
						if (dataRow["LoanType"].ToString().Equals("F") && LoanTypeFirmCheckBox.Checked)
						{
							firmQuantityOnPledge += long.Parse(dataRow["Quantity"].ToString());							
							releaseCount = releaseCount + 1;
						}
						else if (dataRow["LoanType"].ToString().Equals("C") && LoanTypeCustomerCheckBox.Checked)
						{
							customerQuantityOnPledge += long.Parse(dataRow["Quantity"].ToString());							
							releaseCount = releaseCount + 1;
						}		
						else if (dataRow["LoanType"].ToString().Equals("S") && LoanTypeSpecialCheckBox.Checked)
						{
							specialQuantityOnPledge += long.Parse(dataRow["Quantity"].ToString());							
							releaseCount = releaseCount + 1;
						}		
					}
				}			
							
				quantityOnPledge = customerQuantityOnPledge + firmQuantityOnPledge + specialQuantityOnPledge;
				quantityOnPledge -= quantityActivityPending;
				
				if (quantityOnPledge < 0)
				{
					quantityOnPledge = 0;
				}

				ReleaseQuantityTextBox.Value = quantityOnPledge.ToString();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionBankLoanReleaseInputForm.PositionReturnInputForm_Load]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
    }

    private void PositionBankLoanReleaseInputForm_Closed(object sender, System.EventArgs e)
    {
      mainForm.Refresh();    
    }

    private void PositionBankLoanReleaseInputForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {
      Pen pen = new Pen(Color.DimGray, 1.8F);
      
      float x1 = 20.0F;
      float x2 = this.ClientSize.Width - 20.0F;

      float y0 = SubmitButton.Location.Y - 10;
      float y1 = StatusLabel.Location.Y - 10;

      e.Graphics.DrawLine(pen, x1, y0, x2, y0);   
      e.Graphics.DrawLine(pen, x1, y1, x2, y1);   

      e.Graphics.Dispose();
    }

    private void ReturnQuantityTextBox_TextChanged(object sender, System.EventArgs e)
    {
      StatusMessageLabel.Text = "";
    }

    private void ReturnQuantityTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals((char)13) && SubmitButton.Enabled)
      {
        SubmitButton_Click(this, new System.EventArgs());
        e.Handled = true;
      }
    }

    private void SubmitButton_Click(object sender, System.EventArgs e)
    {          
      releaseQuantity = 0;      

      if (ReleaseQuantityTextBox.Text == "")
      {
        StatusMessageLabel.Text = "Please enter the quantity to release.";
        ReleaseQuantityTextBox.Focus();
        return;
      }      
      
			try
			{
				releaseQuantity = long.Parse(ReleaseQuantityTextBox.Value.ToString());     
			}
			catch (Exception error)
			{
				StatusMessageLabel.Text = error.Message;
				ReleaseQuantityTextBox.Focus();
			}    
      
			if (releaseQuantity > quantityOnPledge)
			{
				StatusMessageLabel.Text = "Sorry... Unable to release more than total quantity available.";
				return;
			}
      
      try
      {       
        StatusMessageLabel.Text = "Will check to submit " + releaseCount.ToString() + " releases for a total quantity of " + releaseQuantity.ToString("#,##0") + ".";       
       
        SendReleasessDelegate sendReleasessDelegate = new SendReleasessDelegate(SendReleases);
				sendReleasessDelegate.BeginInvoke(dataSet, bookGroup, secId, releaseQuantity, releaseCount, null,  null);             

        SubmitButton.Enabled = false;
        ReleaseQuantityTextBox.Enabled = false;
				LoanTypeCustomerCheckBox.Enabled = false;
				LoanTypeFirmCheckBox.Enabled = false;
				LoanTypeSpecialCheckBox.Enabled = false;
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionBankLoanReleaseInputForm.SubmitButton_Click]", Log.Error, 1);
        StatusMessageLabel.Text = error.Message;        
      }          
    }

    private void CloseButton_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }

		private void LoanTypeCustomerCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			quantityOnPledge = ((LoanTypeCustomerCheckBox.Checked) ? customerQuantityOnPledge: 0) + ((LoanTypeFirmCheckBox.Checked) ? firmQuantityOnPledge: 0) + ((LoanTypeSpecialCheckBox.Checked) ? specialQuantityOnPledge: 0); 
			ReleaseQuantityTextBox.Value = quantityOnPledge;
		}
  }
}
