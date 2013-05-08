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
	public class ShortSaleBillingDatesLockInputForm : System.Windows.Forms.Form
	{
		private string groupCode;
		private string accountNumber;

		private MainForm mainForm;
		private DataSet tradingGroupsDataSet;

		private C1.Win.C1Input.C1Label StatusLabel;    
		private C1.Win.C1Input.C1Label StatusMessageLabel;
    
		private System.Windows.Forms.Button SubmitButton;
		private System.Windows.Forms.Button CloseButton;
		private C1.Win.C1Input.C1Label ToLabel;
		private C1.Win.C1Input.C1Label FromLabel;
		private System.Windows.Forms.DateTimePicker FromDatePicker;
		private System.Windows.Forms.DateTimePicker ToDatePicker;
		private C1.Win.C1List.C1Combo GroupCodeCombo;
		private C1.Win.C1List.C1Combo AccountCombo;
		private C1.Win.C1Input.C1Label AccountLabel;
		private System.Windows.Forms.GroupBox ShortSaleBox;
		private System.Windows.Forms.RadioButton LockRadioButton;
		private System.Windows.Forms.RadioButton UnlockRadioButton;
		private System.Windows.Forms.Panel BackgroundPanel;
		private System.Windows.Forms.Button ChangeAccountsButton;
		private C1.Win.C1Input.C1Label GroupCodeLabel;
    
		private System.ComponentModel.Container components = null;
    
		public ShortSaleBillingDatesLockInputForm(MainForm mainForm, string FromDate, string ToDate, string groupCode, string accountNumber)
		{    
			InitializeComponent();     
    
			try
			{  
				this.mainForm = mainForm;     
				this.FromDatePicker.Text = FromDate;
				this.ToDatePicker.Text = ToDate;
        this.groupCode = groupCode;  
        this.accountNumber = accountNumber;   
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleBillingDatesLockInputForm.ShortSaleBillingDatesLockInputForm]", Log.Error, 1);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleBillingDatesLockInputForm));
			this.SubmitButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.StatusMessageLabel = new C1.Win.C1Input.C1Label();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.ToLabel = new C1.Win.C1Input.C1Label();
			this.FromLabel = new C1.Win.C1Input.C1Label();
			this.FromDatePicker = new System.Windows.Forms.DateTimePicker();
			this.ToDatePicker = new System.Windows.Forms.DateTimePicker();
			this.GroupCodeCombo = new C1.Win.C1List.C1Combo();
			this.GroupCodeLabel = new C1.Win.C1Input.C1Label();
			this.AccountCombo = new C1.Win.C1List.C1Combo();
			this.AccountLabel = new C1.Win.C1Input.C1Label();
			this.ShortSaleBox = new System.Windows.Forms.GroupBox();
			this.LockRadioButton = new System.Windows.Forms.RadioButton();
			this.UnlockRadioButton = new System.Windows.Forms.RadioButton();
			this.BackgroundPanel = new System.Windows.Forms.Panel();
			this.ChangeAccountsButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ToLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.FromLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.AccountCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.AccountLabel)).BeginInit();
			this.ShortSaleBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// SubmitButton
			// 
			this.SubmitButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.SubmitButton.Location = new System.Drawing.Point(84, 236);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(84, 24);
			this.SubmitButton.TabIndex = 6;
			this.SubmitButton.Text = "&Submit";
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			// 
			// CloseButton
			// 
			this.CloseButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.CloseButton.Location = new System.Drawing.Point(260, 236);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(84, 24);
			this.CloseButton.TabIndex = 7;
			this.CloseButton.Text = "&Close";
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// StatusMessageLabel
			// 
			this.StatusMessageLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.StatusMessageLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.StatusMessageLabel.ForeColor = System.Drawing.Color.Maroon;
			this.StatusMessageLabel.Location = new System.Drawing.Point(79, 176);
			this.StatusMessageLabel.Name = "StatusMessageLabel";
			this.StatusMessageLabel.Size = new System.Drawing.Size(272, 44);
			this.StatusMessageLabel.TabIndex = 29;
			this.StatusMessageLabel.Tag = null;
			this.StatusMessageLabel.TextDetached = true;
			// 
			// StatusLabel
			// 
			this.StatusLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.StatusLabel.Location = new System.Drawing.Point(20, 176);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(52, 16);
			this.StatusLabel.TabIndex = 28;
			this.StatusLabel.Tag = null;
			this.StatusLabel.Text = "Status:";
			this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.StatusLabel.TextDetached = true;
			// 
			// ToLabel
			// 
			this.ToLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ToLabel.Location = new System.Drawing.Point(76, 38);
			this.ToLabel.Name = "ToLabel";
			this.ToLabel.Size = new System.Drawing.Size(32, 16);
			this.ToLabel.TabIndex = 40;
			this.ToLabel.Tag = null;
			this.ToLabel.Text = "To:";
			this.ToLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ToLabel.TextDetached = true;
			// 
			// FromLabel
			// 
			this.FromLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.FromLabel.Location = new System.Drawing.Point(52, 10);
			this.FromLabel.Name = "FromLabel";
			this.FromLabel.Size = new System.Drawing.Size(56, 16);
			this.FromLabel.TabIndex = 39;
			this.FromLabel.Tag = null;
			this.FromLabel.Text = "From:";
			this.FromLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.FromLabel.TextDetached = true;
			// 
			// FromDatePicker
			// 
			this.FromDatePicker.Location = new System.Drawing.Point(107, 8);
			this.FromDatePicker.Name = "FromDatePicker";
			this.FromDatePicker.Size = new System.Drawing.Size(216, 21);
			this.FromDatePicker.TabIndex = 38;
			// 
			// ToDatePicker
			// 
			this.ToDatePicker.Location = new System.Drawing.Point(107, 36);
			this.ToDatePicker.Name = "ToDatePicker";
			this.ToDatePicker.Size = new System.Drawing.Size(216, 21);
			this.ToDatePicker.TabIndex = 37;
			// 
			// GroupCodeCombo
			// 
			this.GroupCodeCombo.AddItemSeparator = ';';
			this.GroupCodeCombo.Caption = "";
			this.GroupCodeCombo.CaptionHeight = 17;
			this.GroupCodeCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.GroupCodeCombo.ColumnCaptionHeight = 17;
			this.GroupCodeCombo.ColumnFooterHeight = 17;
			this.GroupCodeCombo.ColumnWidth = 100;
			this.GroupCodeCombo.ContentHeight = 16;
			this.GroupCodeCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.GroupCodeCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
			this.GroupCodeCombo.DropDownWidth = 300;
			this.GroupCodeCombo.EditorBackColor = System.Drawing.SystemColors.Window;
			this.GroupCodeCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GroupCodeCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.GroupCodeCombo.EditorHeight = 16;
			this.GroupCodeCombo.ExtendRightColumn = true;
			this.GroupCodeCombo.GapHeight = 2;
			this.GroupCodeCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.GroupCodeCombo.ItemHeight = 15;
			this.GroupCodeCombo.Location = new System.Drawing.Point(107, 68);
			this.GroupCodeCombo.MatchEntryTimeout = ((long)(2000));
			this.GroupCodeCombo.MaxDropDownItems = ((short)(5));
			this.GroupCodeCombo.MaxLength = 32767;
			this.GroupCodeCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.GroupCodeCombo.Name = "GroupCodeCombo";
			this.GroupCodeCombo.PartialRightColumn = false;
			this.GroupCodeCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.GroupCodeCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.GroupCodeCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.GroupCodeCombo.Size = new System.Drawing.Size(216, 22);
			this.GroupCodeCombo.TabIndex = 46;
			this.GroupCodeCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Corresponde" +
				"nt\" DataField=\"GroupCode\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" C" +
				"aption=\"Correspondent Name\" DataField=\"GroupName\"><ValueItems /></C1DataColumn><" +
				"/DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVe" +
				"rt:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;" +
				"}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeCo" +
				"lor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptio" +
				"nText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}Highl" +
				"ightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelec" +
				"tor{AlignImage:Center;}Style13{AlignHorz:Near;}Heading{Wrap:True;BackColor:Contr" +
				"ol;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Sty" +
				"le10{}Style11{}Style14{}Style15{AlignHorz:Near;}Style16{AlignHorz:Near;}Style17{" +
				"}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView Allow" +
				"ColSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoot" +
				"erHeight=\"17\" ExtendRightColumn=\"True\" VerticalScrollGroup=\"1\" HorizontalScrollG" +
				"roup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" " +
				"/><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Co" +
				"lor><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1" +
				"DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style15\" /><Sty" +
				"le parent=\"Style1\" me=\"Style16\" /><FooterStyle parent=\"Style3\" me=\"Style17\" /><C" +
				"olumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height" +
				">15</Height><DCIdx>1</DCIdx></C1DisplayColumn></internalCols><VScrollBar><Width>" +
				"16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle" +
				" parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><Foo" +
				"terStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /" +
				"><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"Highlig" +
				"htRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle" +
				" parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"" +
				"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\"" +
				" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"" +
				"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me" +
				"=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"I" +
				"nactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Hig" +
				"hlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"Od" +
				"dRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me" +
				"=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><La" +
				"yout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			// 
			// GroupCodeLabel
			// 
			this.GroupCodeLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.GroupCodeLabel.Location = new System.Drawing.Point(4, 71);
			this.GroupCodeLabel.Name = "GroupCodeLabel";
			this.GroupCodeLabel.Size = new System.Drawing.Size(104, 16);
			this.GroupCodeLabel.TabIndex = 45;
			this.GroupCodeLabel.Tag = null;
			this.GroupCodeLabel.Text = "Group Code:";
			this.GroupCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.GroupCodeLabel.TextDetached = true;
			// 
			// AccountCombo
			// 
			this.AccountCombo.AddItemSeparator = ';';
			this.AccountCombo.Caption = "";
			this.AccountCombo.CaptionHeight = 17;
			this.AccountCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.AccountCombo.ColumnCaptionHeight = 17;
			this.AccountCombo.ColumnFooterHeight = 17;
			this.AccountCombo.ContentHeight = 16;
			this.AccountCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.AccountCombo.EditorBackColor = System.Drawing.SystemColors.Window;
			this.AccountCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AccountCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.AccountCombo.EditorHeight = 16;
			this.AccountCombo.ExtendRightColumn = true;
			this.AccountCombo.GapHeight = 2;
			this.AccountCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.AccountCombo.ItemHeight = 15;
			this.AccountCombo.Location = new System.Drawing.Point(107, 96);
			this.AccountCombo.MatchEntryTimeout = ((long)(2000));
			this.AccountCombo.MaxDropDownItems = ((short)(5));
			this.AccountCombo.MaxLength = 32767;
			this.AccountCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.AccountCombo.Name = "AccountCombo";
			this.AccountCombo.PartialRightColumn = false;
			this.AccountCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.AccountCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.AccountCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.AccountCombo.Size = new System.Drawing.Size(216, 22);
			this.AccountCombo.TabIndex = 48;
			this.AccountCombo.BeforeOpen += new System.ComponentModel.CancelEventHandler(this.AccountCombo_BeforeOpen);
			this.AccountCombo.PropBag = "<?xml version=\"1.0\"?><Blob><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Da" +
				"ta>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style2{" +
				"}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}HighlightRow{" +
				"ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{Alig" +
				"nImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;For" +
				"eColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style9{AlignHorz:" +
				"Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" " +
				"Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Exte" +
				"ndRightColumn=\"True\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRe" +
				"ct>0, 0, 116, 156</ClientRect><VScrollBar><Width>16</Width></VScrollBar><HScroll" +
				"Bar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" />" +
				"<EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"St" +
				"yle3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\"" +
				" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveS" +
				"tyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" />" +
				"<RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle paren" +
				"t=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List." +
				"ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"" +
				"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Head" +
				"ing\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Norma" +
				"l\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Nor" +
				"mal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\"" +
				" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertS" +
				"plits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultR" +
				"ecSelWidth>16</DefaultRecSelWidth></Blob>";
			// 
			// AccountLabel
			// 
			this.AccountLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.AccountLabel.Location = new System.Drawing.Point(4, 100);
			this.AccountLabel.Name = "AccountLabel";
			this.AccountLabel.Size = new System.Drawing.Size(104, 16);
			this.AccountLabel.TabIndex = 47;
			this.AccountLabel.Tag = null;
			this.AccountLabel.Text = "Account:";
			this.AccountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.AccountLabel.TextDetached = true;
			// 
			// ShortSaleBox
			// 
			this.ShortSaleBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ShortSaleBox.Controls.Add(this.LockRadioButton);
			this.ShortSaleBox.Controls.Add(this.UnlockRadioButton);
			this.ShortSaleBox.Location = new System.Drawing.Point(77, 128);
			this.ShortSaleBox.Name = "ShortSaleBox";
			this.ShortSaleBox.Size = new System.Drawing.Size(276, 40);
			this.ShortSaleBox.TabIndex = 49;
			this.ShortSaleBox.TabStop = false;
			this.ShortSaleBox.Text = "Action";
			// 
			// LockRadioButton
			// 
			this.LockRadioButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.LockRadioButton.Location = new System.Drawing.Point(176, 20);
			this.LockRadioButton.Name = "LockRadioButton";
			this.LockRadioButton.Size = new System.Drawing.Size(48, 12);
			this.LockRadioButton.TabIndex = 2;
			this.LockRadioButton.Text = "Lock";
			// 
			// UnlockRadioButton
			// 
			this.UnlockRadioButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.UnlockRadioButton.Checked = true;
			this.UnlockRadioButton.Location = new System.Drawing.Point(56, 20);
			this.UnlockRadioButton.Name = "UnlockRadioButton";
			this.UnlockRadioButton.Size = new System.Drawing.Size(64, 12);
			this.UnlockRadioButton.TabIndex = 0;
			this.UnlockRadioButton.TabStop = true;
			this.UnlockRadioButton.Text = "Unlock";
			// 
			// BackgroundPanel
			// 
			this.BackgroundPanel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.BackgroundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.BackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BackgroundPanel.DockPadding.All = 1;
			this.BackgroundPanel.Location = new System.Drawing.Point(1, 1);
			this.BackgroundPanel.Name = "BackgroundPanel";
			this.BackgroundPanel.Size = new System.Drawing.Size(428, 277);
			this.BackgroundPanel.TabIndex = 50;
			// 
			// ChangeAccountsButton
			// 
			this.ChangeAccountsButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ChangeAccountsButton.Enabled = false;
			this.ChangeAccountsButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ChangeAccountsButton.Location = new System.Drawing.Point(0, 236);
			this.ChangeAccountsButton.Name = "ChangeAccountsButton";
			this.ChangeAccountsButton.Size = new System.Drawing.Size(84, 24);
			this.ChangeAccountsButton.TabIndex = 53;
			this.ChangeAccountsButton.Text = "&Modify";
			// 
			// ShortSaleBillingDatesLockInputForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(430, 279);
			this.Controls.Add(this.ShortSaleBox);
			this.Controls.Add(this.AccountCombo);
			this.Controls.Add(this.AccountLabel);
			this.Controls.Add(this.GroupCodeCombo);
			this.Controls.Add(this.GroupCodeLabel);
			this.Controls.Add(this.ToLabel);
			this.Controls.Add(this.FromLabel);
			this.Controls.Add(this.FromDatePicker);
			this.Controls.Add(this.ToDatePicker);
			this.Controls.Add(this.StatusMessageLabel);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.SubmitButton);
			this.Controls.Add(this.BackgroundPanel);
			this.Controls.Add(this.ChangeAccountsButton);
			this.DockPadding.All = 1;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ShortSaleBillingDatesLockInputForm";
			this.Text = "ShortSale - Billing - Dates";
			this.Load += new System.EventHandler(this.ShortSaleBillingDatesLockInputForm_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ShortSaleBillingDatesLockInputForm_Paint);
			((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ToLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.FromLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.AccountCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.AccountLabel)).EndInit();
			this.ShortSaleBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

    

		private void ShortSaleBillingDatesLockInputForm_Load(object sender, System.EventArgs e)
		{       
			this.WindowState = FormWindowState.Normal;
      
			try
			{
				tradingGroupsDataSet = mainForm.ShortSaleAgent.TradingGroupsGet(mainForm.ShortSaleAgent.TradeDate(), mainForm.UtcOffset);
				GroupCodeCombo.DataSource = tradingGroupsDataSet;
				GroupCodeCombo.DataMember = "TradingGroups";
				GroupCodeCombo.Text = "[Please select...]";

				AccountCombo.Text = "[Please select...]";
				
				if (!groupCode.Equals(""))
				{
					GroupCodeCombo.Text = groupCode;       
				}

				if (!accountNumber.Equals(""))
				{
					AccountCombo.Text = accountNumber;
				}

				mainForm.Refresh();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleBillingDatesLockInputForm.ShortSaleBillingDatesLockInputForm_Load]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void SubmitButton_Click(object sender, System.EventArgs e)
		{
			int recordsUpdated = 0;

			try
			{
				recordsUpdated  = mainForm.RebateAgent.ShortSaleBillingSummaryDatesLock(
					DateTime.Parse(FromDatePicker.Text).ToString(Standard.DateFormat),
					DateTime.Parse(ToDatePicker.Text).ToString(Standard.DateFormat),
					((GroupCodeCombo.Text.Equals("[Please select...]"))? "" : GroupCodeCombo.Text),
					((AccountCombo.Text.Equals("[Please select...]"))? "" : AccountCombo.Text),
					((LockRadioButton.Checked)?true:false),
					mainForm.UserId);				
			
				StatusMessageLabel.Text = "Updated " + recordsUpdated + " records for " +((GroupCodeCombo.Text.Equals("[Please select...]"))? "All Correspondents." : GroupCodeCombo.Text + ".");
			}
			catch (Exception error)
			{			
				StatusMessageLabel.Text = error.Message;
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void AccountCombo_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
		{
			DataSet accountsDataSet = null;

			try
			{
				if ( !(GroupCodeCombo.Text.Equals("") || GroupCodeCombo.Text.Equals("[Please select...]")))
				{
					AccountCombo.ClearItems();
					accountsDataSet = mainForm.PositionAgent.AccountsGet(GroupCodeCombo.Text);

					AccountCombo.HoldFields();
					AccountCombo.DataSource = accountsDataSet;
					AccountCombo.DataMember = "Accounts";
				}
			}
			catch {}
		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void ShortSaleBillingDatesLockInputForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Pen pen = new Pen(Color.DimGray, 1.8F);
      
			float x1 = 10;
			float x2 = this.Size.Width - 10;
      
			float y0 = StatusLabel.Location.Y - 5;
			float y1 = StatusMessageLabel.Location.Y  + StatusMessageLabel.Size.Height + 5;
			
			e.Graphics.DrawLine(pen, x1, y0, x2, y0);   
			e.Graphics.DrawLine(pen, x1, y1, x2, y1);   

			e.Graphics.Dispose();
		}
	}
}
