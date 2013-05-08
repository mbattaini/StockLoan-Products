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
  public class PositionPcChangeInputForm : System.Windows.Forms.Form
  {
    private MainForm mainForm;
    
    private ArrayList contractsArray, bizDateArray;
    
    private C1.Win.C1Input.C1Label StatusLabel;
    private C1.Win.C1Input.C1Label StatusMessageLabel;
    
    private C1.Win.C1Input.C1Label EffectiveDateLabel;
    private C1.Win.C1List.C1Combo EffectiveDateCombo;
    
    private C1.Win.C1Input.C1Label NewCodeLabel;
    private C1.Win.C1Input.C1TextBox NewCodeTextBox;

    private System.Windows.Forms.Button SubmitButton;
    private System.Windows.Forms.Button CloseButton;
    
    private System.ComponentModel.Container components = null;

    private delegate void SendContractsDelegate(ArrayList contractsArray);    

    public PositionPcChangeInputForm(MainForm mainForm, ArrayList contractsArray, ArrayList bizDateArray)
    {    
      InitializeComponent();
      
      try
      {        
        this.mainForm = mainForm;    
        this.bizDateArray = bizDateArray;
        this.contractsArray = contractsArray;                        
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionPcChangeInputForm.PositionPcChangeInputForm]", Log.Error, 1);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if(components != null)
        {
          components.Dispose();
        }
      }
    
      base.Dispose(disposing);
    }


    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionPcChangeInputForm));
      this.SubmitButton = new System.Windows.Forms.Button();
      this.CloseButton = new System.Windows.Forms.Button();
      this.NewCodeTextBox = new C1.Win.C1Input.C1TextBox();
      this.StatusMessageLabel = new C1.Win.C1Input.C1Label();
      this.StatusLabel = new C1.Win.C1Input.C1Label();
      this.NewCodeLabel = new C1.Win.C1Input.C1Label();
      this.EffectiveDateLabel = new C1.Win.C1Input.C1Label();
      this.EffectiveDateCombo = new C1.Win.C1List.C1Combo();
      ((System.ComponentModel.ISupportInitialize)(this.NewCodeTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.NewCodeLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.EffectiveDateLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.EffectiveDateCombo)).BeginInit();
      this.SuspendLayout();
      // 
      // SubmitButton
      // 
      this.SubmitButton.Location = new System.Drawing.Point(32, 160);
      this.SubmitButton.Name = "SubmitButton";
      this.SubmitButton.Size = new System.Drawing.Size(84, 24);
      this.SubmitButton.TabIndex = 3;
      this.SubmitButton.Text = "Submit";
      this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
      // 
      // CloseButton
      // 
      this.CloseButton.Location = new System.Drawing.Point(152, 160);
      this.CloseButton.Name = "CloseButton";
      this.CloseButton.Size = new System.Drawing.Size(84, 24);
      this.CloseButton.TabIndex = 4;
      this.CloseButton.Text = "Close";
      this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
      // 
      // NewCodeTextBox
      // 
      this.NewCodeTextBox.AutoSize = false;
      this.NewCodeTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
      this.NewCodeTextBox.Location = new System.Drawing.Point(120, 48);
      this.NewCodeTextBox.MaxLength = 1;
      this.NewCodeTextBox.Name = "NewCodeTextBox";
      this.NewCodeTextBox.Size = new System.Drawing.Size(22, 20);
      this.NewCodeTextBox.TabIndex = 2;
      this.NewCodeTextBox.Tag = null;
      this.NewCodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.NewCodeTextBox.TextDetached = true;
      this.NewCodeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewCodeTextBox_KeyPress);
      // 
      // StatusMessageLabel
      // 
      this.StatusMessageLabel.ForeColor = System.Drawing.Color.Maroon;
      this.StatusMessageLabel.Location = new System.Drawing.Point(76, 88);
      this.StatusMessageLabel.Name = "StatusMessageLabel";
      this.StatusMessageLabel.Size = new System.Drawing.Size(168, 60);
      this.StatusMessageLabel.TabIndex = 29;
      this.StatusMessageLabel.Tag = null;
      this.StatusMessageLabel.TextDetached = true;
      // 
      // StatusLabel
      // 
      this.StatusLabel.Location = new System.Drawing.Point(24, 88);
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(48, 16);
      this.StatusLabel.TabIndex = 28;
      this.StatusLabel.Tag = null;
      this.StatusLabel.Text = "Status:";
      this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
      this.StatusLabel.TextDetached = true;
      // 
      // NewCodeLabel
      // 
      this.NewCodeLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.NewCodeLabel.Location = new System.Drawing.Point(24, 48);
      this.NewCodeLabel.Name = "NewCodeLabel";
      this.NewCodeLabel.Size = new System.Drawing.Size(92, 20);
      this.NewCodeLabel.TabIndex = 26;
      this.NewCodeLabel.Tag = null;
      this.NewCodeLabel.Text = "New Code:";
      this.NewCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.NewCodeLabel.TextDetached = true;
      // 
      // EffectiveDateLabel
      // 
      this.EffectiveDateLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.EffectiveDateLabel.Location = new System.Drawing.Point(24, 20);
      this.EffectiveDateLabel.Name = "EffectiveDateLabel";
      this.EffectiveDateLabel.Size = new System.Drawing.Size(92, 20);
      this.EffectiveDateLabel.TabIndex = 30;
      this.EffectiveDateLabel.Tag = null;
      this.EffectiveDateLabel.Text = "Effective Date:";
      this.EffectiveDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.EffectiveDateLabel.TextDetached = true;
      // 
      // EffectiveDateCombo
      // 
      this.EffectiveDateCombo.AddItemSeparator = ';';
      this.EffectiveDateCombo.AutoSize = false;
      this.EffectiveDateCombo.Caption = "";
      this.EffectiveDateCombo.CaptionHeight = 17;
      this.EffectiveDateCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
      this.EffectiveDateCombo.ColumnCaptionHeight = 17;
      this.EffectiveDateCombo.ColumnFooterHeight = 17;
      this.EffectiveDateCombo.ColumnWidth = 100;
      this.EffectiveDateCombo.ContentHeight = 14;
      this.EffectiveDateCombo.DataMode = C1.Win.C1List.DataModeEnum.AddItem;
      this.EffectiveDateCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
      this.EffectiveDateCombo.EditorBackColor = System.Drawing.SystemColors.Window;
      this.EffectiveDateCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.EffectiveDateCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
      this.EffectiveDateCombo.EditorHeight = 14;
      this.EffectiveDateCombo.ExtendRightColumn = true;
      this.EffectiveDateCombo.GapHeight = 2;
      this.EffectiveDateCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
      this.EffectiveDateCombo.ItemHeight = 15;
      this.EffectiveDateCombo.LimitToList = true;
      this.EffectiveDateCombo.Location = new System.Drawing.Point(120, 18);
      this.EffectiveDateCombo.MatchEntryTimeout = ((long)(2000));
      this.EffectiveDateCombo.MaxDropDownItems = ((short)(10));
      this.EffectiveDateCombo.MaxLength = 32767;
      this.EffectiveDateCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
      this.EffectiveDateCombo.Name = "EffectiveDateCombo";
      this.EffectiveDateCombo.PartialRightColumn = false;
      this.EffectiveDateCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
      this.EffectiveDateCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
      this.EffectiveDateCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
      this.EffectiveDateCombo.Size = new System.Drawing.Size(100, 20);
      this.EffectiveDateCombo.TabIndex = 31;
      this.EffectiveDateCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Date\" DataF" +
        "ield=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Desi" +
        "gn.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;Ali" +
        "gnVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}E" +
        "venRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Sty" +
        "le3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}C" +
        "aption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;}HighlightRow{ForeColor:Hig" +
        "hlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{Al" +
        "ignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;Fo" +
        "reColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style1" +
        "3{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView All" +
        "owColSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFo" +
        "oterHeight=\"17\" ExtendRightColumn=\"True\" VerticalScrollGroup=\"1\" HorizontalScrol" +
        "lGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn" +
        "><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13" +
        "\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</" +
        "Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></" +
        "C1DisplayColumn></internalCols><VScrollBar><Width>16</Width><Style>Always</Style" +
        "></VScrollBar><HScrollBar><Height>16</Height><Style>None</Style></HScrollBar><Ca" +
        "ptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Styl" +
        "e7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"" +
        "Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle paren" +
        "t=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><O" +
        "ddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSele" +
        "ctor\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style paren" +
        "t=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Styl" +
        "e parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"H" +
        "eading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Head" +
        "ing\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Norma" +
        "l\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Norm" +
        "al\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"C" +
        "aption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horz" +
        "Splits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blo" +
        "b>";
      // 
      // PositionPcChangeInputForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
      this.ClientSize = new System.Drawing.Size(266, 195);
      this.Controls.Add(this.EffectiveDateCombo);
      this.Controls.Add(this.EffectiveDateLabel);
      this.Controls.Add(this.StatusMessageLabel);
      this.Controls.Add(this.StatusLabel);
      this.Controls.Add(this.NewCodeTextBox);
      this.Controls.Add(this.NewCodeLabel);
      this.Controls.Add(this.CloseButton);
      this.Controls.Add(this.SubmitButton);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PositionPcChangeInputForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Position - P/C Change Input";
      this.Load += new System.EventHandler(this.PositionPcChangeInputForm_Load);
      this.Closed += new System.EventHandler(this.PositionPcChangeInputForm_Closed);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.PositionPcChangeInputForm_Paint);
      ((System.ComponentModel.ISupportInitialize)(this.NewCodeTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.NewCodeLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.EffectiveDateLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.EffectiveDateCombo)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion


    private void SendContracts(ArrayList contractsArray)
    {
      int n = 0;
      int errorCount = 0;
      string alert;

      foreach (DataRow dataRow in contractsArray)
      {
        try
        {
          alert = "Submitting a P/C change to '" + NewCodeTextBox.Text + "' for " +
            (dataRow["ContractType"].ToString().Equals("B") ? "borrow contract ID " : "loan contract ID ") + 
            dataRow["ContractId"].ToString() + " effective " + EffectiveDateCombo.Text + ".";
          
          mainForm.Alert(alert, PilotState.Unknown);

          mainForm.PositionAgent.ContractMaintenance(
            dataRow["BookGroup"].ToString(), 
            dataRow["ContractId"].ToString(), 
            dataRow["ContractType"].ToString(),  
            dataRow["Book"].ToString(),           
            NewCodeTextBox.Text, 
            EffectiveDateCombo.Text,
            "",
            "",
            "",
            "",
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

          Log.Write(e.Message + " [PositionPcChangeInputForm.SendContracts]", Log.Error, 1);
          mainForm.Alert(e.Message, PilotState.RunFault);

          errorCount++;
        }

        StatusMessageLabel.Text = "Submited " + (++n).ToString() + " P/C change" + (n.Equals((int)1) ? "" : "s") +
          " of " + contractsArray.Count + " with " + errorCount.ToString() + " initial error" +
          (errorCount.Equals((int)1) ? "" : "s") + ".";
      }
      
      StatusMessageLabel.Text = "Done!  Submitted total of " + n.ToString() + " P/C change" + (n.Equals((int)1) ? "" : "s") +
        " with " + errorCount.ToString() + " initial error" + (errorCount.Equals((int)1) ? "" : "s") + ".";
    }    
    
    private void PositionPcChangeInputForm_Load(object sender, System.EventArgs e)
    {
      this.WindowState = FormWindowState.Normal;
      
      try
      {
        if (bizDateArray.Count > 0)
        {
          foreach (string date in bizDateArray)
          {
            EffectiveDateCombo.AddItem(date);
          }
        }
        else
        {
          EffectiveDateCombo.AddItem(mainForm.ServiceAgent.ContractsBizDate());
        }

        EffectiveDateCombo.SelectedIndex = 0;
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionPcChangeInputForm.PositionPcChangeInputForm_Load]", Log.Error, 1);
        mainForm.Alert(error.Message, PilotState.RunFault);
      }
    }

    private void PositionPcChangeInputForm_Closed(object sender, System.EventArgs e)
    {
      mainForm.Refresh();    
    }

    private void PositionPcChangeInputForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
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

    private void NewCodeTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals((char)13) && SubmitButton.Enabled)
      {
        SubmitButton_Click(this, new System.EventArgs());
        e.Handled = true;
      }
    }

    private void SubmitButton_Click(object sender, System.EventArgs e)
    {        
      try
      {
        if (NewCodeTextBox.Text.Equals(""))
        {
          NewCodeTextBox.Text = " ";
        }

        StatusMessageLabel.Text = "Will submit " + contractsArray.Count.ToString() + " P/C change" +
          (contractsArray.Equals((int)1) ? "" : "s") + " to '" + NewCodeTextBox.Text + "' effective " + EffectiveDateCombo.Text + ".";

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
        NewCodeTextBox.Enabled = false;
        EffectiveDateCombo.Enabled = false;
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionPcChangeInputForm.SubmitButton_Click]", Log.Error, 1);
        StatusMessageLabel.Text = error.Message;              
      }      
    }

    private void CloseButton_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }
  }
}
