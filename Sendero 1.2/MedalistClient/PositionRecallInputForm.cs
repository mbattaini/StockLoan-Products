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
  public class PositionRecallInputForm : System.Windows.Forms.Form
  {
    private const string FAX_STATUS = "Send Pending";

    private long available;
    private long recall;    

    private MainForm mainForm;

    private ArrayList contractsArray;
    
    private C1.Win.C1Input.C1Label SecDescriptionLabel;

    private C1.Win.C1Input.C1Label RecallQuantityLabel;
    private C1.Win.C1Input.C1TextBox RecallQuantityTextBox;
    
    private C1.Win.C1Input.C1Label ReasonCodeLabel;
    private C1.Win.C1List.C1Combo ReasonCodeCombo;
    private C1.Win.C1List.C1Combo IndicatorCombo;
    
    private C1.Win.C1Input.C1Label RecallDateLabel;
    private System.Windows.Forms.DateTimePicker OpenDateEdit;
    
    private C1.Win.C1Input.C1Label BuyInDateLabel;
    private System.Windows.Forms.DateTimePicker ReturnDueDateEdit;
    
    private C1.Win.C1Input.C1Label CommentLabel;
    private C1.Win.C1Input.C1TextBox CommentTextBox;
    private System.Windows.Forms.CheckBox SendFaxCheck;
    
    private C1.Win.C1Input.C1Label RecallAvailableValueLabel;
    private C1.Win.C1Input.C1Label RecallAvailableLabel;
    
    private C1.Win.C1Input.C1Label StatusLabel;
    private C1.Win.C1Input.C1Label StatusMessageLabel;
    
    private System.Windows.Forms.Button SubmitButton;
    private System.Windows.Forms.Button CloselButton;    
    
    private System.ComponentModel.Container components = null;
    
    private delegate void SendContractsDelegate(ArrayList contractsArray);    
      
    public PositionRecallInputForm(MainForm mainForm, ArrayList contractsArray)
    {    
      InitializeComponent();
      this.mainForm = mainForm;    
    
      OpenDateEdit.CustomFormat = Standard.DateFormat;
      ReturnDueDateEdit.CustomFormat = Standard.DateFormat;
      
      try
      {        
        this.contractsArray = contractsArray;

        DataRow row = (DataRow)contractsArray[0];
        mainForm.SecId = row["SecId"].ToString();
        SecDescriptionLabel.Text = mainForm.Description.Split('|')[0];                 
        
        SendFaxCheck.Enabled = mainForm.PositionAgent.FaxEnabled() && row["ContractType"].ToString().Equals("L");
      }   
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionRecallInputForm.PositionRecallInputForm]", Log.Error, 1);
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionRecallInputForm));
      this.RecallAvailableLabel = new C1.Win.C1Input.C1Label();
      this.RecallQuantityLabel = new C1.Win.C1Input.C1Label();
      this.RecallQuantityTextBox = new C1.Win.C1Input.C1TextBox();
      this.BuyInDateLabel = new C1.Win.C1Input.C1Label();
      this.RecallDateLabel = new C1.Win.C1Input.C1Label();
      this.SubmitButton = new System.Windows.Forms.Button();
      this.CloselButton = new System.Windows.Forms.Button();
      this.StatusLabel = new C1.Win.C1Input.C1Label();
      this.OpenDateEdit = new System.Windows.Forms.DateTimePicker();
      this.ReturnDueDateEdit = new System.Windows.Forms.DateTimePicker();
      this.ReasonCodeLabel = new C1.Win.C1Input.C1Label();
      this.ReasonCodeCombo = new C1.Win.C1List.C1Combo();
      this.CommentLabel = new C1.Win.C1Input.C1Label();
      this.CommentTextBox = new C1.Win.C1Input.C1TextBox();
      this.IndicatorCombo = new C1.Win.C1List.C1Combo();
      this.SendFaxCheck = new System.Windows.Forms.CheckBox();
      this.RecallAvailableValueLabel = new C1.Win.C1Input.C1Label();
      this.SecDescriptionLabel = new C1.Win.C1Input.C1Label();
      this.StatusMessageLabel = new C1.Win.C1Input.C1Label();
      ((System.ComponentModel.ISupportInitialize)(this.RecallAvailableLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecallQuantityLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecallQuantityTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.BuyInDateLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecallDateLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReasonCodeLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReasonCodeCombo)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.CommentLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.CommentTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.IndicatorCombo)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecallAvailableValueLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.SecDescriptionLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).BeginInit();
      this.SuspendLayout();
      // 
      // RecallAvailableLabel
      // 
      this.RecallAvailableLabel.Location = new System.Drawing.Point(24, 80);
      this.RecallAvailableLabel.Name = "RecallAvailableLabel";
      this.RecallAvailableLabel.Size = new System.Drawing.Size(116, 16);
      this.RecallAvailableLabel.TabIndex = 3;
      this.RecallAvailableLabel.Tag = null;
      this.RecallAvailableLabel.Text = "Recall Available:";
      this.RecallAvailableLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.RecallAvailableLabel.TextDetached = true;
      // 
      // RecallQuantityLabel
      // 
      this.RecallQuantityLabel.Location = new System.Drawing.Point(24, 48);
      this.RecallQuantityLabel.Name = "RecallQuantityLabel";
      this.RecallQuantityLabel.Size = new System.Drawing.Size(116, 20);
      this.RecallQuantityLabel.TabIndex = 4;
      this.RecallQuantityLabel.Tag = null;
      this.RecallQuantityLabel.Text = "Recall Quantity:";
      this.RecallQuantityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.RecallQuantityLabel.TextDetached = true;
      // 
      // RecallQuantityTextBox
      //      
      this.RecallQuantityTextBox.AutoSize = false;
      this.RecallQuantityTextBox.CustomFormat = "##,###,##0";
      this.RecallQuantityTextBox.DataType = typeof(long);
      this.RecallQuantityTextBox.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
      this.RecallQuantityTextBox.Location = new System.Drawing.Point(144, 48);
      this.RecallQuantityTextBox.MaxLength = 8;
      this.RecallQuantityTextBox.Name = "RecallQuantityTextBox";
      this.RecallQuantityTextBox.PostValidation.Validation = C1.Win.C1Input.PostValidationTypeEnum.PostValidatingEvent;
      this.RecallQuantityTextBox.Size = new System.Drawing.Size(108, 20);
      this.RecallQuantityTextBox.TabIndex = 1;
      this.RecallQuantityTextBox.Tag = null;
      this.RecallQuantityTextBox.TrimEnd = false;
      this.RecallQuantityTextBox.TextChanged += new System.EventHandler(this.RecallQuantityTextBox_TextChanged);
      this.RecallQuantityTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
      // 
      // BuyInDateLabel
      // 
      this.BuyInDateLabel.Location = new System.Drawing.Point(24, 144);
      this.BuyInDateLabel.Name = "BuyInDateLabel";
      this.BuyInDateLabel.Size = new System.Drawing.Size(116, 20);
      this.BuyInDateLabel.TabIndex = 10;
      this.BuyInDateLabel.Tag = null;
      this.BuyInDateLabel.Text = "Buy-In Date:";
      this.BuyInDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.BuyInDateLabel.TextDetached = true;
      // 
      // RecallDateLabel
      // 
      this.RecallDateLabel.Location = new System.Drawing.Point(24, 116);
      this.RecallDateLabel.Name = "RecallDateLabel";
      this.RecallDateLabel.Size = new System.Drawing.Size(116, 20);
      this.RecallDateLabel.TabIndex = 9;
      this.RecallDateLabel.Tag = null;
      this.RecallDateLabel.Text = "Recall Date:";
      this.RecallDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.RecallDateLabel.TextDetached = true;
      // 
      // SubmitButton
      // 
      this.SubmitButton.Location = new System.Drawing.Point(32, 360);
      this.SubmitButton.Name = "SubmitButton";
      this.SubmitButton.Size = new System.Drawing.Size(84, 24);
      this.SubmitButton.TabIndex = 10;
      this.SubmitButton.Text = "Submit";
      this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
      // 
      // CloselButton
      // 
      this.CloselButton.Location = new System.Drawing.Point(176, 360);
      this.CloselButton.Name = "CloselButton";
      this.CloselButton.Size = new System.Drawing.Size(84, 24);
      this.CloselButton.TabIndex = 11;
      this.CloselButton.Text = "Close";
      this.CloselButton.Click += new System.EventHandler(this.CloseButton_Click);
      // 
      // StatusLabel
      // 
      this.StatusLabel.Location = new System.Drawing.Point(24, 288);
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(48, 16);
      this.StatusLabel.TabIndex = 13;
      this.StatusLabel.Tag = null;
      this.StatusLabel.Text = "Status:";
      this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.StatusLabel.TextDetached = true;
      // 
      // OpenDateEdit
      // 
      this.OpenDateEdit.CustomFormat = "yyyy-MM-dd";
      this.OpenDateEdit.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.OpenDateEdit.Location = new System.Drawing.Point(144, 112);
      this.OpenDateEdit.Name = "OpenDateEdit";
      this.OpenDateEdit.Size = new System.Drawing.Size(108, 21);
      this.OpenDateEdit.TabIndex = 3;
      this.OpenDateEdit.ValueChanged += new System.EventHandler(this.OpenDateEdit_ValueChanged);
      // 
      // ReturnDueDateEdit
      // 
      this.ReturnDueDateEdit.CustomFormat = "yyyy-MM-dd";
      this.ReturnDueDateEdit.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.ReturnDueDateEdit.Location = new System.Drawing.Point(144, 144);
      this.ReturnDueDateEdit.Name = "ReturnDueDateEdit";
      this.ReturnDueDateEdit.Size = new System.Drawing.Size(108, 21);
      this.ReturnDueDateEdit.TabIndex = 4;
      // 
      // ReasonCodeLabel
      // 
      this.ReasonCodeLabel.Location = new System.Drawing.Point(12, 184);
      this.ReasonCodeLabel.Name = "ReasonCodeLabel";
      this.ReasonCodeLabel.Size = new System.Drawing.Size(128, 20);
      this.ReasonCodeLabel.TabIndex = 17;
      this.ReasonCodeLabel.Tag = null;
      this.ReasonCodeLabel.Text = "Reason|Type Codes:";
      this.ReasonCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ReasonCodeLabel.TextDetached = true;
      // 
      // ReasonCodeCombo
      // 
      this.ReasonCodeCombo.AddItemSeparator = ';';
      this.ReasonCodeCombo.AutoSize = false;
      this.ReasonCodeCombo.Caption = "";
      this.ReasonCodeCombo.CaptionHeight = 17;
      this.ReasonCodeCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
      this.ReasonCodeCombo.ColumnCaptionHeight = 17;
      this.ReasonCodeCombo.ColumnFooterHeight = 17;
      this.ReasonCodeCombo.ContentHeight = 14;
      this.ReasonCodeCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
      this.ReasonCodeCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
      this.ReasonCodeCombo.DropDownWidth = 350;
      this.ReasonCodeCombo.DropMode = C1.Win.C1List.DropModeEnum.Manual;
      this.ReasonCodeCombo.EditorBackColor = System.Drawing.SystemColors.Window;
      this.ReasonCodeCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.ReasonCodeCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
      this.ReasonCodeCombo.EditorHeight = 14;
      this.ReasonCodeCombo.ExtendRightColumn = true;
      this.ReasonCodeCombo.GapHeight = 2;
      this.ReasonCodeCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
      this.ReasonCodeCombo.ItemHeight = 15;
      this.ReasonCodeCombo.LimitToList = true;
      this.ReasonCodeCombo.Location = new System.Drawing.Point(144, 184);
      this.ReasonCodeCombo.MatchEntryTimeout = ((long)(2000));
      this.ReasonCodeCombo.MaxDropDownItems = ((short)(10));
      this.ReasonCodeCombo.MaxLength = 2;
      this.ReasonCodeCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
      this.ReasonCodeCombo.Name = "ReasonCodeCombo";
      this.ReasonCodeCombo.PartialRightColumn = false;
      this.ReasonCodeCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
      this.ReasonCodeCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
      this.ReasonCodeCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
      this.ReasonCodeCombo.Size = new System.Drawing.Size(52, 20);
      this.ReasonCodeCombo.TabIndex = 5;
      this.ReasonCodeCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Code\" DataF" +
        "ield=\"ReasonCode\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"" +
        "Reason\" DataField=\"ReasonName\"><ValueItems /></C1DataColumn></DataCols><Styles t" +
        "ype=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Bord" +
        "er:None,,0, 0, 0, 0;AlignVert:Center;}Style2{}Style5{}Style4{}Style7{}Style6{}Ev" +
        "enRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Styl" +
        "e3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Ca" +
        "ption{AlignHorz:Center;}Style20{}Normal{Font:Verdana, 8.25pt;BackColor:Window;}H" +
        "ighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}" +
        "Style18{AlignHorz:Near;}Style19{AlignHorz:Near;}OddRow{}RecordSelector{AlignImag" +
        "e:Center;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor" +
        ":ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style15{AlignHorz:Near" +
        ";}Style16{AlignHorz:Near;}Style17{}Style1{}</Data></Styles><Splits><C1.Win.C1Lis" +
        "t.ListBoxView AllowColSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHei" +
        "ght=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" VerticalScrollGroup=\"1" +
        "\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols" +
        "><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style15\" /><Style parent=\"St" +
        "yle1\" me=\"Style16\" /><FooterStyle parent=\"Style3\" me=\"Style17\" /><ColumnDivider>" +
        "<Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>52</Width><He" +
        "ight>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle" +
        " parent=\"Style2\" me=\"Style18\" /><Style parent=\"Style1\" me=\"Style19\" /><FooterSty" +
        "le parent=\"Style3\" me=\"Style20\" /><ColumnDivider><Color>DarkGray</Color><Style>S" +
        "ingle</Style></ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColum" +
        "n></internalCols><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>1" +
        "6</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle" +
        " parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><Grou" +
        "pStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" " +
        "/><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"" +
        "Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelect" +
        "orStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" " +
        "me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView><" +
        "/Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"H" +
        "eading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Capt" +
        "ion\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Select" +
        "ed\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"Even" +
        "Row\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSe" +
        "lector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vert" +
        "Splits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16" +
        "</DefaultRecSelWidth></Blob>";
      // 
      // CommentLabel
      // 
      this.CommentLabel.Location = new System.Drawing.Point(20, 216);
      this.CommentLabel.Name = "CommentLabel";
      this.CommentLabel.Size = new System.Drawing.Size(68, 20);
      this.CommentLabel.TabIndex = 19;
      this.CommentLabel.Tag = null;
      this.CommentLabel.Text = "Comment:";
      this.CommentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.CommentLabel.TextDetached = true;
      // 
      // CommentTextBox
      // 
      this.CommentTextBox.AutoSize = false;
      this.CommentTextBox.Location = new System.Drawing.Point(92, 216);
      this.CommentTextBox.MaxLength = 20;
      this.CommentTextBox.Multiline = true;
      this.CommentTextBox.Name = "CommentTextBox";
      this.CommentTextBox.NumericInput = false;
      this.CommentTextBox.Size = new System.Drawing.Size(160, 20);
      this.CommentTextBox.TabIndex = 8;
      this.CommentTextBox.Tag = null;
      this.CommentTextBox.TextDetached = true;
      this.CommentTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
      // 
      // IndicatorCombo
      // 
      this.IndicatorCombo.AddItemSeparator = ';';
      this.IndicatorCombo.AutoSize = false;
      this.IndicatorCombo.Caption = "";
      this.IndicatorCombo.CaptionHeight = 17;
      this.IndicatorCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
      this.IndicatorCombo.ColumnCaptionHeight = 17;
      this.IndicatorCombo.ColumnFooterHeight = 17;
      this.IndicatorCombo.ContentHeight = 14;
      this.IndicatorCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
      this.IndicatorCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
      this.IndicatorCombo.DropDownWidth = 350;
      this.IndicatorCombo.DropMode = C1.Win.C1List.DropModeEnum.Manual;
      this.IndicatorCombo.EditorBackColor = System.Drawing.SystemColors.Window;
      this.IndicatorCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.IndicatorCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
      this.IndicatorCombo.EditorHeight = 14;
      this.IndicatorCombo.ExtendRightColumn = true;
      this.IndicatorCombo.GapHeight = 2;
      this.IndicatorCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
      this.IndicatorCombo.ItemHeight = 15;
      this.IndicatorCombo.LimitToList = true;
      this.IndicatorCombo.Location = new System.Drawing.Point(208, 184);
      this.IndicatorCombo.MatchEntryTimeout = ((long)(2000));
      this.IndicatorCombo.MaxDropDownItems = ((short)(2));
      this.IndicatorCombo.MaxLength = 1;
      this.IndicatorCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
      this.IndicatorCombo.Name = "IndicatorCombo";
      this.IndicatorCombo.PartialRightColumn = false;
      this.IndicatorCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
      this.IndicatorCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
      this.IndicatorCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
      this.IndicatorCombo.Size = new System.Drawing.Size(44, 20);
      this.IndicatorCombo.TabIndex = 6;
      this.IndicatorCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Code\" DataF" +
        "ield=\"IndicatorCode\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" Captio" +
        "n=\"Type\" DataField=\"IndicatorName\"><ValueItems /></C1DataColumn></DataCols><Styl" +
        "es type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;" +
        "Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5" +
        "{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightTe" +
        "xt;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor" +
        ":InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;" +
        "BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Styl" +
        "e9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Style15{AlignHorz:N" +
        "ear;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:Cont" +
        "rolText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:" +
        "Near;}Style16{AlignHorz:Near;}Style17{}Style1{}</Data></Styles><Splits><C1.Win.C" +
        "1List.ListBoxView AllowColSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptio" +
        "nHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" VerticalScrollGrou" +
        "p=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internal" +
        "Cols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent" +
        "=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivi" +
        "der><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>44</Width" +
        "><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
        "tyle parent=\"Style2\" me=\"Style15\" /><Style parent=\"Style1\" me=\"Style16\" /><Foote" +
        "rStyle parent=\"Style3\" me=\"Style17\" /><ColumnDivider><Color>DarkGray</Color><Sty" +
        "le>Single</Style></ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C1DisplayC" +
        "olumn></internalCols><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Heig" +
        "ht>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowS" +
        "tyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><" +
        "GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Styl" +
        "e2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle pare" +
        "nt=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSe" +
        "lectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Select" +
        "ed\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxVi" +
        "ew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" m" +
        "e=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"" +
        "Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Se" +
        "lected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"" +
        "EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Reco" +
        "rdSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</" +
        "vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidt" +
        "h>16</DefaultRecSelWidth></Blob>";
      // 
      // SendFaxCheck
      // 
      this.SendFaxCheck.Location = new System.Drawing.Point(24, 248);
      this.SendFaxCheck.Name = "SendFaxCheck";
      this.SendFaxCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.SendFaxCheck.Size = new System.Drawing.Size(82, 16);
      this.SendFaxCheck.TabIndex = 9;
      this.SendFaxCheck.Text = ":Send Fax";
      // 
      // RecallAvailableValueLabel
      // 
      this.RecallAvailableValueLabel.CustomFormat = "#,##0";
      this.RecallAvailableValueLabel.DataType = typeof(System.Decimal);
      this.RecallAvailableValueLabel.ForeColor = System.Drawing.Color.Navy;
      this.RecallAvailableValueLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
      this.RecallAvailableValueLabel.Location = new System.Drawing.Point(144, 80);
      this.RecallAvailableValueLabel.Name = "RecallAvailableValueLabel";
      this.RecallAvailableValueLabel.Size = new System.Drawing.Size(108, 16);
      this.RecallAvailableValueLabel.TabIndex = 27;
      this.RecallAvailableValueLabel.Tag = null;
      this.RecallAvailableValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // SecDescriptionLabel
      // 
      this.SecDescriptionLabel.Font = new System.Drawing.Font("Verdana", 9F);
      this.SecDescriptionLabel.ForeColor = System.Drawing.Color.Maroon;
      this.SecDescriptionLabel.Location = new System.Drawing.Point(12, 8);
      this.SecDescriptionLabel.Name = "SecDescriptionLabel";
      this.SecDescriptionLabel.Size = new System.Drawing.Size(268, 28);
      this.SecDescriptionLabel.TabIndex = 28;
      this.SecDescriptionLabel.Tag = null;
      this.SecDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.SecDescriptionLabel.TextDetached = true;
      // 
      // StatusMessageLabel
      // 
      this.StatusMessageLabel.ForeColor = System.Drawing.Color.Maroon;
      this.StatusMessageLabel.Location = new System.Drawing.Point(76, 288);
      this.StatusMessageLabel.Name = "StatusMessageLabel";
      this.StatusMessageLabel.Size = new System.Drawing.Size(184, 56);
      this.StatusMessageLabel.TabIndex = 29;
      this.StatusMessageLabel.Tag = null;
      this.StatusMessageLabel.TextDetached = true;
      // 
      // PositionRecallInputForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
      this.ClientSize = new System.Drawing.Size(290, 395);
      this.Controls.Add(this.StatusMessageLabel);
      this.Controls.Add(this.SecDescriptionLabel);
      this.Controls.Add(this.RecallAvailableValueLabel);
      this.Controls.Add(this.SendFaxCheck);
      this.Controls.Add(this.IndicatorCombo);
      this.Controls.Add(this.CommentTextBox);
      this.Controls.Add(this.CommentLabel);
      this.Controls.Add(this.ReasonCodeCombo);
      this.Controls.Add(this.ReasonCodeLabel);
      this.Controls.Add(this.ReturnDueDateEdit);
      this.Controls.Add(this.OpenDateEdit);
      this.Controls.Add(this.StatusLabel);
      this.Controls.Add(this.CloselButton);
      this.Controls.Add(this.SubmitButton);
      this.Controls.Add(this.BuyInDateLabel);
      this.Controls.Add(this.RecallDateLabel);
      this.Controls.Add(this.RecallQuantityTextBox);
      this.Controls.Add(this.RecallQuantityLabel);
      this.Controls.Add(this.RecallAvailableLabel);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PositionRecallInputForm";
      this.Text = "Position - Recall Input";
      this.Load += new System.EventHandler(this.PositionRecallInputForm_Load);
      this.Closed += new System.EventHandler(this.PositionRecallInputForm_Closed);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.PositionRecallInputForm_Paint);
      ((System.ComponentModel.ISupportInitialize)(this.RecallAvailableLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecallQuantityLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecallQuantityTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.BuyInDateLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecallDateLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReasonCodeLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReasonCodeCombo)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.CommentLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.CommentTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.IndicatorCombo)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.RecallAvailableValueLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.SecDescriptionLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    private void SendContracts(ArrayList contractsArray)
    {
      int n = 0;
      int errorCount = 0;
      string alert;

      foreach (DataRow row in contractsArray)
      {
        try
        {
          available = (long.Parse(row["Quantity"].ToString()) - long.Parse(row["QuantityRecalled"].ToString())); 
          
          if (available <= recall)
          {
            recall -= available;
          }
          else
          {
            available = recall;
            recall = 0;
          }

          if (available == 0)
          {
            try
            {
              mainForm.positionOpenContractsForm.StatusFlagSet(
                row["BookGroup"].ToString(), 
                row["ContractId"].ToString(), 
                row["ContractType"].ToString(),  
                "");
            }
            catch (Exception ee)
            {
              mainForm.Alert(ee.Message, PilotState.RunFault);          
            }
          
            continue;
          }
          
          alert = "Submitting a recall of " + available.ToString("#,##0") + " for " + 
            (row["ContractType"].ToString().Equals("B") ? "borrow contract ID " : "loan contract ID ") + 
            row["ContractId"].ToString() + ".";
          
          mainForm.Alert(alert, PilotState.Unknown);

          mainForm.PositionAgent.RecallNew(                        
            row["BookGroup"].ToString(),
            row["ContractId"].ToString(), 
            row["ContractType"].ToString(), 
            row["Book"].ToString(),
						"",
            row["SecId"].ToString(),
            available.ToString(),
            IndicatorCombo.Text,            
            ReturnDueDateEdit.Value.ToString(),
            "",
            OpenDateEdit.Value.ToString(),
            ReasonCodeCombo.Text,
            "1",
            (SendFaxCheck.Checked) ? FAX_STATUS : "",
            CommentTextBox.Text,
            mainForm.UserId);
        }
        catch (Exception e)
        {
          try
          {
            mainForm.positionOpenContractsForm.StatusFlagSet(
              row["BookGroup"].ToString(), 
              row["ContractId"].ToString(), 
              row["ContractType"].ToString(),  
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
  
        StatusMessageLabel.Text = "Submited " + (++n).ToString() + " recall" + (n.Equals((int)1) ? "" : "s") +
          " of " + contractsArray.Count + " with " + errorCount.ToString() + " initial error" +
          (errorCount.Equals((int)1) ? "" : "s") + ".";
      }                      

      StatusMessageLabel.Text = "Done!  Submitted total of " + n.ToString() + " recall" + (n.Equals((int)1) ? "" : "s") +
        " with " + errorCount.ToString() + " initial error" + (errorCount.Equals((int)1) ? "" : "s") + ".";
    }    

    private void PositionRecallInputForm_Load(object sender, System.EventArgs e)
    {
      long quantity = 0, quantityRecalled = 0, quantityAvailable = 0;
      DataSet dataSet;           
      
      this.WindowState = FormWindowState.Normal;
      
      try
      {
        OpenDateEdit.Text       = mainForm.PositionAgent.FutureBizDate(0);   
        ReturnDueDateEdit.Text  = mainForm.PositionAgent.FutureBizDate(3);                           
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionRecallInputForm.PositionRecallInputForm_Load]", Log.Error, 1);
      } 
   
      try
      {                
        foreach (DataRow row in contractsArray)
        {                                        
          quantity = long.Parse(row["Quantity"].ToString());
          quantityRecalled = long.Parse(row["QuantityRecalled"].ToString());     
        
          quantityAvailable += (quantity - quantityRecalled);
        }

        RecallAvailableValueLabel.Value = quantityAvailable;            
        
        dataSet = mainForm.PositionAgent.RecallReasonsGet();             

        ReasonCodeCombo.HoldFields();
        ReasonCodeCombo.DataSource = dataSet.Tables["Reasons"];                
      
        if(dataSet.Tables["Reasons"].Rows.Count > 4)
        {
          ReasonCodeCombo.SelectedIndex = 0;
        }        
        
        dataSet = mainForm.PositionAgent.RecallIndicatorsGet();
        
        IndicatorCombo.HoldFields();
        IndicatorCombo.DataSource = dataSet.Tables["Indicators"];
        
        if(dataSet.Tables["Indicators"].Rows.Count > 1)
        {
          IndicatorCombo.SelectedIndex = 1;
        }        
        
        if (mainForm.RecallQuantity > quantityAvailable)
        {
          RecallQuantityTextBox.Value = quantityAvailable.ToString();
        }
        else
        {
          RecallQuantityTextBox.Value = (mainForm.RecallQuantity == 0) ? "" : mainForm.RecallQuantity.ToString();
        }

        if (quantityAvailable <= 0)
        {
          RecallQuantityTextBox.ReadOnly = true;          
          SubmitButton.Enabled = false;

          StatusMessageLabel.Text = "No available quantity in the selected range.";
        }        
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionRecallInputForm.PositionRecallInputForm_Load]", Log.Error, 1);
      }
    }

    private void PositionRecallInputForm_Closed(object sender, System.EventArgs e)
    {
      mainForm.Refresh();
    }  

    private void PositionRecallInputForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
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

    private void RecallQuantityTextBox_TextChanged(object sender, System.EventArgs e)
    {
      StatusMessageLabel.Text = "";    
    }

    private void TextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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
      recall = 0;    
      
      if (RecallQuantityTextBox.Text == "")
      {
        StatusMessageLabel.Text = "Please enter the quantity to recall.";
        RecallQuantityTextBox.Focus();
        return;
      }      
     
      try
      {
        recall = long.Parse(RecallQuantityTextBox.Value.ToString());
        available = long.Parse(RecallAvailableValueLabel.Value.ToString());
      }
      catch (Exception error)
      {
        StatusMessageLabel.Text = error.Message;
      }
      
      if (recall > available)
      {
        StatusMessageLabel.Text = "Sorry... Unable to recall more than total quantity available in the selected range.";
        return;
      }     
      
			try
			{
				DataRow temp = (DataRow) contractsArray[0];

				//sort by lowest rate and then largest quantity							
				
				for (int i = 0; i < contractsArray.Count - 1; i++)
				{
					for (int j = 0; j < contractsArray.Count -1 - i; j++)
					{
						
						DataRow row     = (DataRow) contractsArray[j];
						DataRow rowNext = (DataRow) contractsArray[j + 1];
          						
						if (decimal.Parse(row["RebateRate"].ToString()) < decimal.Parse(rowNext["RebateRate"].ToString()))
						{              																												
							contractsArray.RemoveAt(j);
							contractsArray.Insert(j, rowNext);

							contractsArray.RemoveAt(j+1);
							contractsArray.Insert(j+1, row);														
						}
						else if (decimal.Parse(row["RebateRate"].ToString()) == decimal.Parse(rowNext["RebateRate"].ToString()))
						{
							if (long.Parse(row["Quantity"].ToString()) < long.Parse(rowNext["Quantity"].ToString()))
							{
								contractsArray.RemoveAt(j);
								contractsArray.Insert(j, rowNext);

								contractsArray.RemoveAt(j+1);
								contractsArray.Insert(j+1, row);		
							}
						}
					}
				}		
				
				StatusMessageLabel.Text = "Will check to submit " + contractsArray.Count.ToString() + " recalls for a total quantity of " +
          recall.ToString("#,##0") + ".";

        mainForm.positionOpenContractsForm.ClearSelectedRange();

				foreach (DataRow row in contractsArray)
				{
					try
					{
						mainForm.positionOpenContractsForm.StatusFlagSet(
							row["BookGroup"].ToString(), 
							row["ContractId"].ToString(), 
							row["ContractType"].ToString(),  
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
        SendFaxCheck.Enabled = false;
        CommentTextBox.Enabled = false;
        ReasonCodeCombo.Enabled = false;
        IndicatorCombo.Enabled = false;
        ReturnDueDateEdit.Enabled = false;
        OpenDateEdit.Enabled = false;
        RecallQuantityTextBox.Enabled = false;
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionRecallInputForm.SubmitButton_Click]", Log.Error, 1);
        StatusMessageLabel.Text = error.Message;
      }          
    }

    private void CloseButton_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }

    private void OpenDateEdit_ValueChanged(object sender, System.EventArgs e)
    {
      try
      {
        ReturnDueDateEdit.Text    = mainForm.PositionAgent.FutureBizDate(4);
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " [PositionRecallInputForm.OpenDateEdit_ValueChanged]", Log.Error, 1);
      } 
    }
  }
}
