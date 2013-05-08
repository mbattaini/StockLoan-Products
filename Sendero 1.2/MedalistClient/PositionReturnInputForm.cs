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
  public class PositionReturnInputForm : System.Windows.Forms.Form
  {
    private long available; 
    private long returnQuantity;
    private decimal amount;
    
    private MainForm mainForm;
    
    private ArrayList contractsArray;
    
    private C1.Win.C1Input.C1Label SecDescriptionLabel;

    private C1.Win.C1Input.C1Label ReturnQuantityLabel;
    private C1.Win.C1Input.C1TextBox ReturnQuantityTextBox;
    
    private C1.Win.C1Input.C1Label StatusLabel;
    private C1.Win.C1Input.C1Label StatusMessageLabel;
    
    private C1.Win.C1Input.C1Label ReturnAvailableLabel;
    private C1.Win.C1Input.C1Label ReturnAvaiableQuantityLabel;
    
    private System.Windows.Forms.Button SubmitButton;
    private System.Windows.Forms.Button CloseButton;    

    private System.ComponentModel.Container components = null;

    private delegate void SendContractsDelegate(ArrayList contractsArray);    

    public PositionReturnInputForm(MainForm mainForm, ArrayList contractsArray)
    {    
      InitializeComponent();
      this.mainForm = mainForm;         
      
      try
      {        
        this.contractsArray = contractsArray;        
         
        DataRow dataRow = (DataRow)contractsArray[0];
        mainForm.SecId = dataRow["SecId"].ToString();
        SecDescriptionLabel.Text = mainForm.Description.Split('|')[0];   
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionReturnInputForm.PositionReturnInputForm]", Log.Error, 1);
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

    private void SendContracts(ArrayList contractsArray)
    {
      int n = 0;
      int errorCount = 0;
      string alert;

      foreach (DataRow dataRow in contractsArray)
      {
        try
        {
          available = long.Parse(dataRow["Quantity"].ToString());          
          
          if ((available < returnQuantity) && (available > 0))
          {
            returnQuantity -= available;
            amount = (decimal.Parse(dataRow["Amount"].ToString()) / long.Parse(dataRow["Quantity"].ToString())) * available;
          }
          else if (available > 0)
          {
            available = returnQuantity;
            amount = (decimal.Parse(dataRow["Amount"].ToString()) / long.Parse(dataRow["Quantity"].ToString())) * returnQuantity;
            returnQuantity = 0;
          }
        
          if (available == 0)
          {
            try
            {
              mainForm.positionOpenContractsForm.StatusFlagSet(
                dataRow["BookGroup"].ToString(), 
                dataRow["ContractId"].ToString(), 
                dataRow["ContractType"].ToString(),  
                "");
            }
            catch (Exception ee)
            {
              mainForm.Alert(ee.Message, PilotState.RunFault);          
            }
          
            continue;
          }
          
          alert = "Submitting a return of " + available.ToString("#,##0") + 
            " for borrow contract ID " + dataRow["ContractId"].ToString() + ".";
          
          mainForm.Alert(alert, PilotState.Unknown);

          mainForm.PositionAgent.Return(                        
            dataRow["BookGroup"].ToString(),
            dataRow["ContractType"].ToString(), 
            dataRow["ContractId"].ToString(),
						mainForm.SecId,
            available,            
            amount,
            "",
            "",
            "",
            mainForm.UserId);
        }      
        catch (Exception e)
        {
          try
          {
            mainForm.positionOpenContractsForm.StatusFlagSet(
              dataRow["BookGroup"].ToString(), 
              dataRow["ContractId"].ToString(), 
              dataRow["ContractType"].ToString(),  
              "E");
          }
          catch (Exception ee)
          {
            mainForm.Alert(ee.Message, PilotState.RunFault);          
          }
          
          Log.Write(e.Message + " [PositionReturnInputForm.SendContracts]", Log.Error, 1);
          mainForm.Alert(e.Message, PilotState.RunFault);

          errorCount ++;
        }

        StatusMessageLabel.Text = "Submited " + (++n).ToString() + " return" + (n.Equals((int)1) ? "" : "s") +
          " of " + contractsArray.Count + " with " + errorCount.ToString() + " initial error" +
          (errorCount.Equals((int)1) ? "" : "s") + ".";
      }                      

      StatusMessageLabel.Text = "Done!  Submitted total of " + n.ToString() + " return" + (n.Equals((int)1) ? "" : "s") +
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
      this.ReturnAvailableLabel = new C1.Win.C1Input.C1Label();
      this.ReturnQuantityLabel = new C1.Win.C1Input.C1Label();
      this.ReturnQuantityTextBox = new C1.Win.C1Input.C1TextBox();
      this.SubmitButton = new System.Windows.Forms.Button();
      this.CloseButton = new System.Windows.Forms.Button();
      this.StatusLabel = new C1.Win.C1Input.C1Label();
      this.StatusMessageLabel = new C1.Win.C1Input.C1Label();
      this.ReturnAvaiableQuantityLabel = new C1.Win.C1Input.C1Label();
      ((System.ComponentModel.ISupportInitialize)(this.SecDescriptionLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReturnAvailableLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReturnQuantityLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReturnQuantityTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReturnAvaiableQuantityLabel)).BeginInit();
      this.SuspendLayout();
      // 
      // SecDescriptionLabel
      // 
      this.SecDescriptionLabel.Font = new System.Drawing.Font("Verdana", 9F);
      this.SecDescriptionLabel.ForeColor = System.Drawing.Color.Maroon;
      this.SecDescriptionLabel.Location = new System.Drawing.Point(12, 8);
      this.SecDescriptionLabel.Name = "SecDescriptionLabel";
      this.SecDescriptionLabel.Size = new System.Drawing.Size(268, 28);
      this.SecDescriptionLabel.TabIndex = 2;
      this.SecDescriptionLabel.Tag = null;
      this.SecDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.SecDescriptionLabel.TextDetached = true;
      // 
      // ReturnAvailableLabel
      // 
      this.ReturnAvailableLabel.Location = new System.Drawing.Point(24, 80);
      this.ReturnAvailableLabel.Name = "ReturnAvailableLabel";
      this.ReturnAvailableLabel.Size = new System.Drawing.Size(116, 16);
      this.ReturnAvailableLabel.TabIndex = 3;
      this.ReturnAvailableLabel.Tag = null;
      this.ReturnAvailableLabel.Text = "Quantity Available:";
      this.ReturnAvailableLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
      this.ReturnAvailableLabel.TextDetached = true;
      // 
      // ReturnQuantityLabel
      // 
      this.ReturnQuantityLabel.Location = new System.Drawing.Point(24, 48);
      this.ReturnQuantityLabel.Name = "ReturnQuantityLabel";
      this.ReturnQuantityLabel.Size = new System.Drawing.Size(116, 20);
      this.ReturnQuantityLabel.TabIndex = 4;
      this.ReturnQuantityLabel.Tag = null;
      this.ReturnQuantityLabel.Text = "Return Quantity:";
      this.ReturnQuantityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ReturnQuantityLabel.TextDetached = true;
      // 
      // ReturnQuantityTextBox
      // 
      this.ReturnQuantityTextBox.AutoSize = false;
      this.ReturnQuantityTextBox.CustomFormat = "###,###,###,##0";
      this.ReturnQuantityTextBox.DataType = typeof(long);
      this.ReturnQuantityTextBox.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
      this.ReturnQuantityTextBox.Location = new System.Drawing.Point(144, 48);
      this.ReturnQuantityTextBox.MaxLength = 15;
      this.ReturnQuantityTextBox.Name = "ReturnQuantityTextBox";
      this.ReturnQuantityTextBox.Size = new System.Drawing.Size(108, 20);
      this.ReturnQuantityTextBox.TabIndex = 5;
      this.ReturnQuantityTextBox.Tag = null;
      this.ReturnQuantityTextBox.TrimEnd = false;
      this.ReturnQuantityTextBox.TextChanged += new System.EventHandler(this.ReturnQuantityTextBox_TextChanged);
      this.ReturnQuantityTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ReturnQuantityTextBox_KeyPress);
      // 
      // SubmitButton
      // 
      this.SubmitButton.Location = new System.Drawing.Point(32, 192);
      this.SubmitButton.Name = "SubmitButton";
      this.SubmitButton.Size = new System.Drawing.Size(84, 24);
      this.SubmitButton.TabIndex = 11;
      this.SubmitButton.Text = "Submit";
      this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
      // 
      // CloseButton
      // 
      this.CloseButton.Location = new System.Drawing.Point(176, 192);
      this.CloseButton.Name = "CloseButton";
      this.CloseButton.Size = new System.Drawing.Size(84, 24);
      this.CloseButton.TabIndex = 12;
      this.CloseButton.Text = "Close";
      this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
      // 
      // StatusLabel
      // 
      this.StatusLabel.Location = new System.Drawing.Point(24, 120);
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
      this.StatusMessageLabel.Location = new System.Drawing.Point(76, 120);
      this.StatusMessageLabel.Name = "StatusMessageLabel";
      this.StatusMessageLabel.Size = new System.Drawing.Size(184, 56);
      this.StatusMessageLabel.TabIndex = 14;
      this.StatusMessageLabel.Tag = null;
      this.StatusMessageLabel.TextDetached = true;
      // 
      // ReturnAvaiableQuantityLabel
      // 
      this.ReturnAvaiableQuantityLabel.CustomFormat = "#,##0";
      this.ReturnAvaiableQuantityLabel.DataType = typeof(System.Decimal);
      this.ReturnAvaiableQuantityLabel.ForeColor = System.Drawing.Color.Navy;
      this.ReturnAvaiableQuantityLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
      this.ReturnAvaiableQuantityLabel.Location = new System.Drawing.Point(144, 80);
      this.ReturnAvaiableQuantityLabel.Name = "ReturnAvaiableQuantityLabel";
      this.ReturnAvaiableQuantityLabel.Size = new System.Drawing.Size(108, 16);
      this.ReturnAvaiableQuantityLabel.TabIndex = 24;
      this.ReturnAvaiableQuantityLabel.Tag = null;
      // 
      // PositionReturnInputForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
      this.ClientSize = new System.Drawing.Size(290, 227);
      this.Controls.Add(this.ReturnAvaiableQuantityLabel);
      this.Controls.Add(this.StatusMessageLabel);
      this.Controls.Add(this.StatusLabel);
      this.Controls.Add(this.CloseButton);
      this.Controls.Add(this.SubmitButton);
      this.Controls.Add(this.ReturnQuantityTextBox);
      this.Controls.Add(this.ReturnQuantityLabel);
      this.Controls.Add(this.ReturnAvailableLabel);
      this.Controls.Add(this.SecDescriptionLabel);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PositionReturnInputForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Position - Return Input";
      this.Load += new System.EventHandler(this.PositionReturnInputForm_Load);
      this.Closed += new System.EventHandler(this.PositionReturnInputForm_Closed);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.PositionReturnInputForm_Paint);
      ((System.ComponentModel.ISupportInitialize)(this.SecDescriptionLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReturnAvailableLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReturnQuantityLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReturnQuantityTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReturnAvaiableQuantityLabel)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion
 
    private void PositionReturnInputForm_Load(object sender, System.EventArgs e)
    {
      long quantity, quantityAvailable = 0;

      this.WindowState = FormWindowState.Normal;
      
      try
      {                               
        foreach (DataRow dataRow in contractsArray)
        {          
          quantity = long.Parse(dataRow["Quantity"].ToString());             
          quantityAvailable += quantity;
        }
        
        ReturnAvaiableQuantityLabel.Value = quantityAvailable;                                      
        
        if (mainForm.ReturnQuantity > quantityAvailable)
        {
          ReturnQuantityTextBox.Value = quantityAvailable.ToString();
        }
        else
        {
          ReturnQuantityTextBox.Value = (mainForm.ReturnQuantity == 0) ? "" : mainForm.ReturnQuantity.ToString();
        }
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionReturnInputForm.PositionReturnInputForm_Load]", Log.Error, 1);
        mainForm.Alert(error.Message, PilotState.RunFault);
      }
    }

    private void PositionReturnInputForm_Closed(object sender, System.EventArgs e)
    {
      mainForm.Refresh();    
    }

    private void PositionReturnInputForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
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
      available = 0; 
      returnQuantity = 0;
      amount = 0;

      if (ReturnQuantityTextBox.Text == "")
      {
        StatusMessageLabel.Text = "Please enter the quantity to return.";
        ReturnQuantityTextBox.Focus();
        return;
      }      
      
      try
      {
        returnQuantity = long.Parse(ReturnQuantityTextBox.Value.ToString());
        available = long.Parse(ReturnAvaiableQuantityLabel.Value.ToString());
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }    
      
      if (returnQuantity > available)
      {
        StatusMessageLabel.Text = "Sorry... Unable to return more than total quantity available in the selected range.";
        return;
      }
      
      try
      {
       /*DataRow temp = (DataRow)contractsArray[0];

				for (int i = 0; i < contractsArray.Count - 1; i++)
				{
					for (int j = 0; j < contractsArray.Count -1 - i; j++)
					{
						DataRow dataRow     = (DataRow)contractsArray[j];
						DataRow dataRowNext = (DataRow)contractsArray[j + 1];
          
						if (decimal.Parse(dataRow["RebateRate"].ToString()) < decimal.Parse(dataRowNext["RebateRate"].ToString()))
						{              
							if (long.Parse(dataRow["Quantity"].ToString()) > long.Parse(dataRowNext["Quantity"].ToString()))
							{
								contractsArray.RemoveAt(j);
								contractsArray.Insert(j, dataRowNext);
								contractsArray.RemoveAt(j + 1);
								contractsArray.Insert(j + 1, dataRow);
							}
						}                    
					}
				}*/

        StatusMessageLabel.Text = "Will check to submit " + contractsArray.Count.ToString() + " returns for a total quantity of " +
          returnQuantity.ToString("#,##0") + ".";

        mainForm.positionOpenContractsForm.ClearSelectedRange();

        foreach (DataRow dataRow in contractsArray)
        {
          try
          {
            mainForm.positionOpenContractsForm.StatusFlagSet(
              dataRow["BookGroup"].ToString(), 
              dataRow["ContractId"].ToString(), 
              dataRow["ContractType"].ToString(),  
              "S");          
          }
          catch (Exception ee)
          {
            mainForm.Alert(ee.Message, PilotState.RunFault);          
          }
        }  
       
        SendContractsDelegate sendContractsDelegate = new SendContractsDelegate(SendContracts);
        sendContractsDelegate.BeginInvoke(contractsArray, null, null);             

        SubmitButton.Enabled = false;
        ReturnQuantityTextBox.Enabled = false;
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionReturnInputForm.SubmitButton_Click]", Log.Error, 1);
        StatusMessageLabel.Text = error.Message;        
      }          
    }

    private void CloseButton_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }
  }
}
