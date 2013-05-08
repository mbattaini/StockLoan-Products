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
  public class PositionRateChangeInputForm : System.Windows.Forms.Form
  {
    private MainForm mainForm;

    private ArrayList contractsArray, bizDateArray;
    
    private C1.Win.C1Input.C1Label BookLabel;
    private C1.Win.C1Input.C1Label BookRateChangeLabel;
    private C1.Win.C1Input.C1Label BorrowLoanLabel;
    
    private C1.Win.C1Input.C1Label StatusLabel;    
    private C1.Win.C1Input.C1Label StatusMessageLabel;
    
    private System.Windows.Forms.RadioButton BoxRateChangeRadio;
    private System.Windows.Forms.RadioButton ContractsRateChangeRadio;

    private System.Windows.Forms.GroupBox SecurityTypeGroupBox;
    private System.Windows.Forms.RadioButton StockRateRadio;
    private System.Windows.Forms.RadioButton BondRateRadio;
    
    private C1.Win.C1Input.C1Label BoxRateChangeLabel;
    private C1.Win.C1Input.C1TextBox NewBoxRateChangeTextBox;
    
    private C1.Win.C1Input.C1Label EffectiveDateLabel;    
    private C1.Win.C1List.C1Combo EffectiveDateCombo;

    private C1.Win.C1Input.C1Label ContractsRateChangeLabel;
    private C1.Win.C1Input.C1TextBox ContractsRateChangeTextBox;
    
    private System.Windows.Forms.Button SubmitButton;
    private System.Windows.Forms.Button CloseButton;
    private C1.Win.C1Input.C1Label BoxNegotiatedLabel;
    
    private System.ComponentModel.Container components = null;
    
    private delegate void SendContractsDelegate(ArrayList contractsArray, string rateChangeText, string effectiveDateText);

    public PositionRateChangeInputForm(MainForm mainForm, ArrayList contractsArray, ArrayList bizDateArray)
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
        Log.Write(error.Message + " [PositionRateChangeInputForm.PositionRateChangeInputForm]", Log.Error, 1);
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionRateChangeInputForm));
      this.SubmitButton = new System.Windows.Forms.Button();
      this.CloseButton = new System.Windows.Forms.Button();
      this.BoxRateChangeRadio = new System.Windows.Forms.RadioButton();
      this.ContractsRateChangeRadio = new System.Windows.Forms.RadioButton();
      this.BookLabel = new C1.Win.C1Input.C1Label();
      this.BoxRateChangeLabel = new C1.Win.C1Input.C1Label();
      this.ContractsRateChangeLabel = new C1.Win.C1Input.C1Label();
      this.SecurityTypeGroupBox = new System.Windows.Forms.GroupBox();
      this.BondRateRadio = new System.Windows.Forms.RadioButton();
      this.StockRateRadio = new System.Windows.Forms.RadioButton();
      this.StatusMessageLabel = new C1.Win.C1Input.C1Label();
      this.StatusLabel = new C1.Win.C1Input.C1Label();
      this.EffectiveDateLabel = new C1.Win.C1Input.C1Label();
      this.EffectiveDateCombo = new C1.Win.C1List.C1Combo();
      this.BookRateChangeLabel = new C1.Win.C1Input.C1Label();
      this.ContractsRateChangeTextBox = new C1.Win.C1Input.C1TextBox();
      this.NewBoxRateChangeTextBox = new C1.Win.C1Input.C1TextBox();
      this.BoxNegotiatedLabel = new C1.Win.C1Input.C1Label();
      this.BorrowLoanLabel = new C1.Win.C1Input.C1Label();
      ((System.ComponentModel.ISupportInitialize)(this.BookLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BoxRateChangeLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ContractsRateChangeLabel)).BeginInit();
      this.SecurityTypeGroupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.EffectiveDateLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.EffectiveDateCombo)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BookRateChangeLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ContractsRateChangeTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.NewBoxRateChangeTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BoxNegotiatedLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BorrowLoanLabel)).BeginInit();
      this.SuspendLayout();
      // 
      // SubmitButton
      // 
      this.SubmitButton.Location = new System.Drawing.Point(32, 328);
      this.SubmitButton.Name = "SubmitButton";
      this.SubmitButton.Size = new System.Drawing.Size(84, 24);
      this.SubmitButton.TabIndex = 6;
      this.SubmitButton.Text = "Submit";
      this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
      // 
      // CloseButton
      // 
      this.CloseButton.Location = new System.Drawing.Point(260, 328);
      this.CloseButton.Name = "CloseButton";
      this.CloseButton.Size = new System.Drawing.Size(84, 24);
      this.CloseButton.TabIndex = 7;
      this.CloseButton.Text = "Close";
      this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
      // 
      // BoxRateChangeRadio
      // 
      this.BoxRateChangeRadio.ForeColor = System.Drawing.Color.MidnightBlue;
      this.BoxRateChangeRadio.Location = new System.Drawing.Point(28, 48);
      this.BoxRateChangeRadio.Name = "BoxRateChangeRadio";
      this.BoxRateChangeRadio.Size = new System.Drawing.Size(196, 16);
      this.BoxRateChangeRadio.TabIndex = 1;
      this.BoxRateChangeRadio.Text = "Box Rate Change";
      this.BoxRateChangeRadio.CheckedChanged += new System.EventHandler(this.BoxRateChangeRadio_CheckedChanged);
      // 
      // ContractsRateChangeRadio
      // 
      this.ContractsRateChangeRadio.Checked = true;
      this.ContractsRateChangeRadio.ForeColor = System.Drawing.Color.MidnightBlue;
      this.ContractsRateChangeRadio.Location = new System.Drawing.Point(28, 136);
      this.ContractsRateChangeRadio.Name = "ContractsRateChangeRadio";
      this.ContractsRateChangeRadio.Size = new System.Drawing.Size(196, 16);
      this.ContractsRateChangeRadio.TabIndex = 3;
      this.ContractsRateChangeRadio.TabStop = true;
      this.ContractsRateChangeRadio.Text = "Contract Rate Change";
      // 
      // BookLabel
      // 
      this.BookLabel.Location = new System.Drawing.Point(48, 12);
      this.BookLabel.Name = "BookLabel";
      this.BookLabel.Size = new System.Drawing.Size(44, 16);
      this.BookLabel.TabIndex = 19;
      this.BookLabel.Tag = null;
      this.BookLabel.Text = "Book: ";
      this.BookLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.BookLabel.TextDetached = true;
      // 
      // BoxRateChangeLabel
      // 
      this.BoxRateChangeLabel.Enabled = false;
      this.BoxRateChangeLabel.Location = new System.Drawing.Point(32, 84);
      this.BoxRateChangeLabel.Name = "BoxRateChangeLabel";
      this.BoxRateChangeLabel.Size = new System.Drawing.Size(100, 20);
      this.BoxRateChangeLabel.TabIndex = 20;
      this.BoxRateChangeLabel.Tag = null;
      this.BoxRateChangeLabel.Text = "New Box Rate:";
      this.BoxRateChangeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.BoxRateChangeLabel.TextDetached = true;
      // 
      // ContractsRateChangeLabel
      // 
      this.ContractsRateChangeLabel.Location = new System.Drawing.Point(40, 200);
      this.ContractsRateChangeLabel.Name = "ContractsRateChangeLabel";
      this.ContractsRateChangeLabel.Size = new System.Drawing.Size(120, 20);
      this.ContractsRateChangeLabel.TabIndex = 26;
      this.ContractsRateChangeLabel.Tag = null;
      this.ContractsRateChangeLabel.Text = "New Contract Rate:";
      this.ContractsRateChangeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ContractsRateChangeLabel.TextDetached = true;
      // 
      // SecurityTypeGroupBox
      // 
      this.SecurityTypeGroupBox.Controls.Add(this.BondRateRadio);
      this.SecurityTypeGroupBox.Controls.Add(this.StockRateRadio);
      this.SecurityTypeGroupBox.Enabled = false;
      this.SecurityTypeGroupBox.Location = new System.Drawing.Point(236, 48);
      this.SecurityTypeGroupBox.Name = "SecurityTypeGroupBox";
      this.SecurityTypeGroupBox.Size = new System.Drawing.Size(104, 60);
      this.SecurityTypeGroupBox.TabIndex = 27;
      this.SecurityTypeGroupBox.TabStop = false;
      this.SecurityTypeGroupBox.Text = "Security Type";
      // 
      // BondRateRadio
      // 
      this.BondRateRadio.Location = new System.Drawing.Point(16, 36);
      this.BondRateRadio.Name = "BondRateRadio";
      this.BondRateRadio.Size = new System.Drawing.Size(80, 20);
      this.BondRateRadio.TabIndex = 2;
      this.BondRateRadio.Text = "Bonds";
      // 
      // StockRateRadio
      // 
      this.StockRateRadio.Checked = true;
      this.StockRateRadio.Location = new System.Drawing.Point(16, 16);
      this.StockRateRadio.Name = "StockRateRadio";
      this.StockRateRadio.Size = new System.Drawing.Size(80, 20);
      this.StockRateRadio.TabIndex = 1;
      this.StockRateRadio.TabStop = true;
      this.StockRateRadio.Text = "Stocks";
      // 
      // StatusMessageLabel
      // 
      this.StatusMessageLabel.ForeColor = System.Drawing.Color.Maroon;
      this.StatusMessageLabel.Location = new System.Drawing.Point(76, 268);
      this.StatusMessageLabel.Name = "StatusMessageLabel";
      this.StatusMessageLabel.Size = new System.Drawing.Size(272, 44);
      this.StatusMessageLabel.TabIndex = 29;
      this.StatusMessageLabel.Tag = null;
      this.StatusMessageLabel.TextDetached = true;
      // 
      // StatusLabel
      // 
      this.StatusLabel.Location = new System.Drawing.Point(24, 268);
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(48, 16);
      this.StatusLabel.TabIndex = 28;
      this.StatusLabel.Tag = null;
      this.StatusLabel.Text = "Status:";
      this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
      this.StatusLabel.TextDetached = true;
      // 
      // EffectiveDateLabel
      // 
      this.EffectiveDateLabel.Location = new System.Drawing.Point(68, 168);
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
      this.EffectiveDateCombo.Location = new System.Drawing.Point(164, 168);
      this.EffectiveDateCombo.MatchEntryTimeout = ((long)(2000));
      this.EffectiveDateCombo.MaxDropDownItems = ((short)(5));
      this.EffectiveDateCombo.MaxLength = 32767;
      this.EffectiveDateCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
      this.EffectiveDateCombo.Name = "EffectiveDateCombo";
      this.EffectiveDateCombo.PartialRightColumn = false;
      this.EffectiveDateCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
      this.EffectiveDateCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
      this.EffectiveDateCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
      this.EffectiveDateCombo.Size = new System.Drawing.Size(100, 20);
      this.EffectiveDateCombo.TabIndex = 32;
      this.EffectiveDateCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Date\" DataF" +
        "ield=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Desi" +
        "gn.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColo" +
        "r:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}E" +
        "venRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Sty" +
        "le3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}C" +
        "aption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;}HighlightRow{ForeColor:Hig" +
        "hlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center" +
        ";}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:Contro" +
        "lText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{Font:Verdana," +
        " 8.25pt;AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C" +
        "1List.ListBoxView AllowColSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptio" +
        "nHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" VerticalScrollGrou" +
        "p=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internal" +
        "Cols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent" +
        "=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivi" +
        "der><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Heig" +
        "ht><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><VScrollBar><Width>16</Width" +
        "><Style>Always</Style></VScrollBar><HScrollBar><Height>16</Height><Style>None</S" +
        "tyle></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle pare" +
        "nt=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyl" +
        "e parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><Hi" +
        "ghLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inact" +
        "ive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorSty" +
        "le parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"S" +
        "tyle5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Spli" +
        "ts><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Headin" +
        "g\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" " +
        "/><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /" +
        "><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" " +
        "/><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelecto" +
        "r\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplit" +
        "s><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</Def" +
        "aultRecSelWidth></Blob>";
      // 
      // BookRateChangeLabel
      // 
      this.BookRateChangeLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.BookRateChangeLabel.ForeColor = System.Drawing.Color.MidnightBlue;
      this.BookRateChangeLabel.Location = new System.Drawing.Point(96, 12);
      this.BookRateChangeLabel.Name = "BookRateChangeLabel";
      this.BookRateChangeLabel.Size = new System.Drawing.Size(136, 16);
      this.BookRateChangeLabel.TabIndex = 33;
      this.BookRateChangeLabel.Tag = null;
      this.BookRateChangeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.BookRateChangeLabel.TextDetached = true;
      // 
      // ContractsRateChangeTextBox
      // 
      this.ContractsRateChangeTextBox.AutoSize = false;
      this.ContractsRateChangeTextBox.CustomFormat = "#0.000";
      this.ContractsRateChangeTextBox.DataType = typeof(System.Decimal);
      this.ContractsRateChangeTextBox.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
      this.ContractsRateChangeTextBox.Location = new System.Drawing.Point(164, 200);
      this.ContractsRateChangeTextBox.MaxLength = 8;
      this.ContractsRateChangeTextBox.Name = "ContractsRateChangeTextBox";
      this.ContractsRateChangeTextBox.Size = new System.Drawing.Size(80, 20);
      this.ContractsRateChangeTextBox.TabIndex = 34;
      this.ContractsRateChangeTextBox.Tag = null;
      this.ContractsRateChangeTextBox.TrimEnd = false;
      this.ContractsRateChangeTextBox.TextChanged += new System.EventHandler(this.RateChangeTextBox_TextChanged);
      this.ContractsRateChangeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RateChangeTextBox_KeyPress);
      // 
      // NewBoxRateChangeTextBox
      // 
      this.NewBoxRateChangeTextBox.AutoSize = false;
      this.NewBoxRateChangeTextBox.CustomFormat = "#0.000";
      this.NewBoxRateChangeTextBox.DataType = typeof(System.Decimal);
      this.NewBoxRateChangeTextBox.Enabled = false;
      this.NewBoxRateChangeTextBox.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
      this.NewBoxRateChangeTextBox.Location = new System.Drawing.Point(136, 84);
      this.NewBoxRateChangeTextBox.MaxLength = 8;
      this.NewBoxRateChangeTextBox.Name = "NewBoxRateChangeTextBox";
      this.NewBoxRateChangeTextBox.Size = new System.Drawing.Size(76, 20);
      this.NewBoxRateChangeTextBox.TabIndex = 35;
      this.NewBoxRateChangeTextBox.Tag = null;
      this.NewBoxRateChangeTextBox.TrimEnd = false;
      this.NewBoxRateChangeTextBox.TextChanged += new System.EventHandler(this.RateChangeTextBox_TextChanged);
      this.NewBoxRateChangeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RateChangeTextBox_KeyPress);
      // 
      // BoxNegotiatedLabel
      // 
      this.BoxNegotiatedLabel.ForeColor = System.Drawing.Color.Maroon;
      this.BoxNegotiatedLabel.Location = new System.Drawing.Point(8, 232);
      this.BoxNegotiatedLabel.Name = "BoxNegotiatedLabel";
      this.BoxNegotiatedLabel.Size = new System.Drawing.Size(360, 16);
      this.BoxNegotiatedLabel.TabIndex = 36;
      this.BoxNegotiatedLabel.Tag = null;
      this.BoxNegotiatedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.BoxNegotiatedLabel.TextDetached = true;
      // 
      // BorrowLoanLabel
      // 
      this.BorrowLoanLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.BorrowLoanLabel.ForeColor = System.Drawing.Color.Maroon;
      this.BorrowLoanLabel.Location = new System.Drawing.Point(252, 12);
      this.BorrowLoanLabel.Name = "BorrowLoanLabel";
      this.BorrowLoanLabel.Size = new System.Drawing.Size(84, 16);
      this.BorrowLoanLabel.TabIndex = 37;
      this.BorrowLoanLabel.Tag = null;
      this.BorrowLoanLabel.Text = "Borrows";
      this.BorrowLoanLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.BorrowLoanLabel.TextDetached = true;
      // 
      // PositionRateChangeInputForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
      this.ClientSize = new System.Drawing.Size(374, 363);
      this.Controls.Add(this.BorrowLoanLabel);
      this.Controls.Add(this.BoxNegotiatedLabel);
      this.Controls.Add(this.NewBoxRateChangeTextBox);
      this.Controls.Add(this.ContractsRateChangeTextBox);
      this.Controls.Add(this.BookRateChangeLabel);
      this.Controls.Add(this.EffectiveDateCombo);
      this.Controls.Add(this.EffectiveDateLabel);
      this.Controls.Add(this.StatusMessageLabel);
      this.Controls.Add(this.StatusLabel);
      this.Controls.Add(this.SecurityTypeGroupBox);
      this.Controls.Add(this.ContractsRateChangeLabel);
      this.Controls.Add(this.BoxRateChangeLabel);
      this.Controls.Add(this.BookLabel);
      this.Controls.Add(this.ContractsRateChangeRadio);
      this.Controls.Add(this.BoxRateChangeRadio);
      this.Controls.Add(this.CloseButton);
      this.Controls.Add(this.SubmitButton);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PositionRateChangeInputForm";
      this.Text = "Position - Rate Change Input";
      this.Load += new System.EventHandler(this.PositionRateChangeInputForm_Load);
      this.Closed += new System.EventHandler(this.PositionRateChangeInputForm_Closed);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.PositionRateChangeInputForm_Paint);
      ((System.ComponentModel.ISupportInitialize)(this.BookLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BoxRateChangeLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ContractsRateChangeLabel)).EndInit();
      this.SecurityTypeGroupBox.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.EffectiveDateLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.EffectiveDateCombo)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BookRateChangeLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ContractsRateChangeTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.NewBoxRateChangeTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BoxNegotiatedLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BorrowLoanLabel)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    private void Disable()
    {
      SubmitButton.Enabled = false;
        
      EffectiveDateCombo.Enabled = false;
      ContractsRateChangeTextBox.Enabled = false;
        
      NewBoxRateChangeTextBox.Enabled = false;
      SecurityTypeGroupBox.Enabled = false;
      
      BoxRateChangeRadio.Enabled = false;
      ContractsRateChangeRadio.Enabled = false;
    }
    
    private void SendContracts(ArrayList contractsArray, string rateChangeText, string effectiveDateText)
    {
      int n = 0;
      int errorCount = 0;
      string alert;

      foreach (DataRow dataRow in contractsArray)
      {
        try
        {
          string  rateCode        = dataRow["RateCode"].ToString();
          decimal contractRate    = decimal.Parse(dataRow["Rate"].ToString());

          if (rateChangeText.Equals("") && !rateCode.Equals("T"))
          {                        
            alert = "Submitting a change to tabled rate for " +
              (dataRow["ContractType"].ToString().Equals("B") ? "borrow contract ID " : "loan contract ID ") + 
              dataRow["ContractId"].ToString() + " effective " + effectiveDateText + ".";
          
            mainForm.Alert(alert, PilotState.Unknown);

            mainForm.PositionAgent.RateChange(
              dataRow["BookGroup"].ToString(),
              dataRow["ContractType"].ToString(),
              dataRow["Book"].ToString(),
              "",
              dataRow["ContractId"].ToString(),
              contractRate,
              rateCode,
              0.0M,
              "T",
              "",
              "",
              mainForm.UserId);
          }
          else if (rateChangeText.Equals("") && rateCode.Equals("T"))
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
          }
          else
          {
            decimal newContractRate = decimal.Parse(rateChangeText);  

            alert = "Submitting a rate change to " + newContractRate.ToString("#0.000") + " for " +
              (dataRow["ContractType"].ToString().Equals("B") ? "borrow contract ID " : "loan contract ID ") + 
              dataRow["ContractId"].ToString() + " effective " + effectiveDateText + ".";
          
            mainForm.Alert(alert, PilotState.Unknown);

            mainForm.PositionAgent.RateChange(
              dataRow["BookGroup"].ToString(),
              dataRow["ContractType"].ToString(),
              dataRow["Book"].ToString(),
              "",
              dataRow["ContractId"].ToString(),
              contractRate,
              rateCode,
              newContractRate,
              (newContractRate < 0) ? "N" : " ",
              dataRow["PoolCode"].ToString(),              
              effectiveDateText,
              mainForm.UserId);                        
          }
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

          Log.Write(e.Message + " [SendContracts.SubmitButton_Click]", Log.Error, 1);
          mainForm.Alert(e.Message, PilotState.RunFault);          
          errorCount++;
        }

        StatusMessageLabel.Text = "Submitted " + (++n).ToString() + " rate change" + (n.Equals((int)1) ? "" : "s") +
          " of " + contractsArray.Count + " with " + errorCount.ToString() + " initial error" +
          (errorCount.Equals((int)1) ? "" : "s") + ".";
      }
      
      StatusMessageLabel.Text = "Done!  Submitted total of " + n.ToString() + " rate change" + (n.Equals((int)1) ? "" : "s") +
        " with " + errorCount.ToString() + " initial error" + (errorCount.Equals((int)1) ? "" : "s") + ".";
    }      
    
    private void BookRateChanges()
    {
      DataRow dataRow = (DataRow)contractsArray[0];

      if (NewBoxRateChangeTextBox.Text.Equals(""))
      {
        StatusMessageLabel.Text = "Please enter the new box rate.";    
        return;
      }
          
      mainForm.positionOpenContractsForm.ClearSelectedRange();

      try
      {            
        StatusMessageLabel.Text = "Will submit a box rate change for " + (StockRateRadio.Checked ? "stock " : "bond ") +
          (dataRow["ContractType"].ToString().Equals("B") ? "borrow with " : "loan with ") + BookRateChangeLabel.Text +
          " to " + NewBoxRateChangeTextBox.Text + "%.";        
        StatusMessageLabel.Refresh();

        Disable();

        mainForm.PositionAgent.RateChange(
          dataRow["BookGroup"].ToString(), 
          dataRow["ContractType"].ToString(), 
          dataRow["Book"].ToString(),
          (StockRateRadio.Checked ? "S" : "B"),
          "",
          0.0M,
          "",
          decimal.Parse(NewBoxRateChangeTextBox.Text),
          "T",
          "",
          "",  
          mainForm.UserId);
        
        StatusMessageLabel.Text =  "Done!  Processed one box rate change with no initial error.";
      }
      catch (Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        StatusMessageLabel.Text =  "Done!  Processed one box rate change with one initial error.";
        Log.Write(error.Message + " [PositionRateChangeInputForm.BookRateChanges]", Log.Error, 1); 
      }
    }
    
   private void ContractRateChanges()
    {
     if (ContractsRateChangeTextBox.Text.Equals(""))
     {
       StatusMessageLabel.Text = "Will submit " + contractsArray.Count.ToString() + 
         " rate changes to a tabled rate.";
     }
     else
     {
       StatusMessageLabel.Text = "Will submit " + contractsArray.Count.ToString() + 
         " rate changes to the new rate of " + ContractsRateChangeTextBox.Text + "%.";
     }
     StatusMessageLabel.Refresh();
     
     mainForm.positionOpenContractsForm.ClearSelectedRange();

     try
      {
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
        sendContractsDelegate.BeginInvoke(contractsArray, ContractsRateChangeTextBox.Text, EffectiveDateCombo.Text, null, null);   
     
        Disable();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [PositionRateChangeInputForm.SubmitButton_Click]", Log.Error, 1);
        StatusMessageLabel.Text = e.Message;
      }        
    }

    private void PositionRateChangeInputForm_Load(object sender, System.EventArgs e)
    {   
      bool haveTabledRate = false;
    
      this.WindowState = FormWindowState.Normal;
      
      try
      {
        foreach (DataRow dataRow in contractsArray)
        {
          BookRateChangeLabel.Text = dataRow["Book"].ToString();
          BorrowLoanLabel.Text = (dataRow["ContractType"].ToString().Equals("B") ? "Borrows" : "Loans");

          if (dataRow["RateCode"].ToString().Equals("T")) // Must not allow a retroactive effective date.
          {
            haveTabledRate = true;
            break;
          }
        }  

        if (bizDateArray.Count > 0)
        {
          foreach (string date in bizDateArray)
          {
            EffectiveDateCombo.AddItem(date);

            if (haveTabledRate)
            {
              break;
            }
          }
        }
        else
        {
          EffectiveDateCombo.AddItem(mainForm.ServiceAgent.ContractsBizDate());
        }

        EffectiveDateCombo.SelectedIndex = 0;

        RateChangeTextBox_TextChanged(this, new System.EventArgs());
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionRateChangeInputForm.PositionRateChangeInputForm_Load]", Log.Error, 1);
        mainForm.Alert(error.Message, PilotState.RunFault);
      }
    }

    private void PositionRateChangeInputForm_Closed(object sender, System.EventArgs e)
    {
      mainForm.Refresh();
    }

    private void PositionRateChangeInputForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {
      Pen pen = new Pen(Color.DimGray, 1.8F);
      
      float x1 = 20.0F;
      float x2 = this.ClientSize.Width - 20.0F;
      
      float y0 = BoxRateChangeRadio.Location.Y - 10;
      float y1 = ContractsRateChangeRadio.Location.Y - 10;
      float y2 = StatusLabel.Location.Y - 10;
      float y3 = SubmitButton.Location.Y - 10;

      e.Graphics.DrawLine(pen, x1, y0, x2, y0);   
      e.Graphics.DrawLine(pen, x1, y1, x2, y1);
      e.Graphics.DrawLine(pen, x1, y2, x2, y2);
      e.Graphics.DrawLine(pen, x1, y3, x2, y3);

      e.Graphics.Dispose();
    }

    private void SubmitButton_Click(object sender, System.EventArgs e)
    {
      if (BoxRateChangeRadio.Checked)
      {
        BookRateChanges();
      }

      if (ContractsRateChangeRadio.Checked)
      {
        ContractRateChanges();                                    
      }       
    }

    private void CloseButton_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }

    private void BoxRateChangeRadio_CheckedChanged(object sender, System.EventArgs e)
    {
      SecurityTypeGroupBox.Enabled = BoxRateChangeRadio.Checked;
      BoxRateChangeLabel.Enabled = BoxRateChangeRadio.Checked;
      NewBoxRateChangeTextBox.Enabled = BoxRateChangeRadio.Checked;

      EffectiveDateLabel.Enabled = ContractsRateChangeRadio.Checked;
      EffectiveDateCombo.Enabled = ContractsRateChangeRadio.Checked;
      ContractsRateChangeLabel.Enabled = ContractsRateChangeRadio.Checked;
      ContractsRateChangeTextBox.Enabled = ContractsRateChangeRadio.Checked;
      BoxNegotiatedLabel.Enabled = ContractsRateChangeRadio.Checked;
    }

    private void RateChangeTextBox_TextChanged(object sender, System.EventArgs e)
    {
      StatusMessageLabel.Text = "";

      if (ContractsRateChangeTextBox.Text.Equals(""))
      {
        BoxNegotiatedLabel.Text = "Count " + contractsArray.Count + " contract" +
          (contractsArray.Count.Equals((int)1) ? "" : "s") + " to be tabled at the box rate.";
      }
      else
      {
        BoxNegotiatedLabel.Text = "Count " + contractsArray.Count + " contract" +
          (contractsArray.Count.Equals((int)1) ? "" : "s") + " to be rate-changed to " + 
          ContractsRateChangeTextBox.Text + "%.";
      }
    }

    private void RateChangeTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals((char)13) && SubmitButton.Enabled)
      {
        SubmitButton_Click(this, new System.EventArgs());
        e.Handled = true;
      }
    }
  }
}
