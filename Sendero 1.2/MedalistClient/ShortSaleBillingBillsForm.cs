// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using C1.C1Pdf;
using Anetics.Common;


namespace Anetics.Medalist
{
	public class ShortSaleBillingBillsForm : System.Windows.Forms.Form
	{
		private struct TradingGroupFile
		{
			private string groupCode;
			private	string filePath;
			private string emailAddress;
		
			public TradingGroupFile(string groupCode, string filePath, string emailAddress)
			{
				this.groupCode = groupCode;
				this.filePath = filePath;
				this.emailAddress = emailAddress;
			}
		
			public string GroupCode()
			{
				return  groupCode;
			}
			
			public string FilePath ()
			{
				return filePath;
			}

			public string EmailAddress ()
			{
				return emailAddress;
			}
		}
		
		private MainForm mainForm;
		private DataSet tradingGroupsDataSet;
		private	DataSet tradingGroupMethodsDataSet;
		private string groupCode;
		private string tempPath;
		private int pageNumber = 0;
		private ShortSaleBillingBillsAccountsForm  shortSaleBillingBillsAccountsForm = null;		

		private C1.Win.C1Input.C1Label StatusLabel;
		private System.Windows.Forms.Button CloseButton;
		private C1.Win.C1Input.C1Label ToLabel;
		private C1.Win.C1Input.C1Label FromLabel;
		private System.Windows.Forms.DateTimePicker FromDatePicker;
		private System.Windows.Forms.DateTimePicker ToDatePicker;
		private C1.Win.C1List.C1Combo GroupCodeCombo;
		private System.Windows.Forms.Button CreateBillButton;
		private System.Windows.Forms.Button ChangeAccountsButton;
		private C1.Win.C1Input.C1Label c1Label1;
		private C1.Win.C1Input.C1Label c1Label2;
		
		private C1.Win.C1Input.C1Label CorrespondentsBilledNumberLabel;
		private C1.Win.C1Input.C1Label TotalAmountBilledLabel;
		private C1.Win.C1Input.C1Label TotalAmountBeingBilledLabel;
		private C1.Win.C1Input.C1Label TotalSecuritiesItemsBilledLabel;
		private C1.Win.C1Input.C1Label TotalSecuritiesBilledLabel;		
		private System.Windows.Forms.Panel BackgroundPanel;
		private C1.Win.C1Input.C1Label AccountsBillingMethodLabel;
		private C1.Win.C1Input.C1Label c1Label3;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.CheckBox AutoEmailCheck;
		private C1.Win.C1Input.C1Label c1Label4;
		private System.Windows.Forms.CheckBox AutoEmailMasterBillCheck;
		private C1.Win.C1Input.C1Label c1Label5;
		private System.Windows.Forms.CheckBox AutoEmailAccountsBorrowedCheck;
		private System.Windows.Forms.ListBox MessageListBox;
		private System.Windows.Forms.Button ViewButton;
		private C1.Win.C1Input.C1Label AccountsCreditDebitsLabel;
		private C1.Win.C1Input.C1Label GroupCodeLabel;
		private C1.Win.C1Input.C1Label GroupCodesBeingBilledLabel;
    
		ArrayList files = null;		
		
		public ShortSaleBillingBillsForm(MainForm mainForm, string FromDate, string ToDate, string groupCode)
		{    
			InitializeComponent();     
    
			try
			{  
				this.mainForm = mainForm;                          
				this.FromDatePicker.Text = FromDate;
				this.ToDatePicker.Text = ToDate;				
				this.groupCode = groupCode;
		
				tempPath = mainForm.ServiceAgent.KeyValueGet("ShortSaleBillsLocation", @"\\penson.com\Shares\Apps\Sendero\Bills");								
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleBillingRateChangeInputForm.ShortSaleBillingRateChangeInputForm]", Log.Error, 1);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleBillingBillsForm));
			this.CreateBillButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.ToLabel = new C1.Win.C1Input.C1Label();
			this.FromLabel = new C1.Win.C1Input.C1Label();
			this.FromDatePicker = new System.Windows.Forms.DateTimePicker();
			this.ToDatePicker = new System.Windows.Forms.DateTimePicker();
			this.GroupCodeCombo = new C1.Win.C1List.C1Combo();
			this.GroupCodeLabel = new C1.Win.C1Input.C1Label();
			this.c1Label1 = new C1.Win.C1Input.C1Label();
			this.c1Label2 = new C1.Win.C1Input.C1Label();
			this.ChangeAccountsButton = new System.Windows.Forms.Button();
			this.CorrespondentsBilledNumberLabel = new C1.Win.C1Input.C1Label();
			this.GroupCodesBeingBilledLabel = new C1.Win.C1Input.C1Label();
			this.TotalAmountBilledLabel = new C1.Win.C1Input.C1Label();
			this.TotalAmountBeingBilledLabel = new C1.Win.C1Input.C1Label();
			this.TotalSecuritiesItemsBilledLabel = new C1.Win.C1Input.C1Label();
			this.TotalSecuritiesBilledLabel = new C1.Win.C1Input.C1Label();
			this.BackgroundPanel = new System.Windows.Forms.Panel();
			this.ViewButton = new System.Windows.Forms.Button();
			this.AccountsCreditDebitsLabel = new C1.Win.C1Input.C1Label();
			this.MessageListBox = new System.Windows.Forms.ListBox();
			this.AutoEmailAccountsBorrowedCheck = new System.Windows.Forms.CheckBox();
			this.c1Label5 = new C1.Win.C1Input.C1Label();
			this.AutoEmailMasterBillCheck = new System.Windows.Forms.CheckBox();
			this.c1Label4 = new C1.Win.C1Input.C1Label();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.AutoEmailCheck = new System.Windows.Forms.CheckBox();
			this.c1Label3 = new C1.Win.C1Input.C1Label();
			this.AccountsBillingMethodLabel = new C1.Win.C1Input.C1Label();
			((System.ComponentModel.ISupportInitialize)(this.ToLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.FromLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CorrespondentsBilledNumberLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodesBeingBilledLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TotalAmountBilledLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TotalAmountBeingBilledLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TotalSecuritiesItemsBilledLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TotalSecuritiesBilledLabel)).BeginInit();
			this.BackgroundPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AccountsCreditDebitsLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.AccountsBillingMethodLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// CreateBillButton
			// 
			this.CreateBillButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.CreateBillButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CreateBillButton.Location = new System.Drawing.Point(112, 328);
			this.CreateBillButton.Name = "CreateBillButton";
			this.CreateBillButton.Size = new System.Drawing.Size(84, 24);
			this.CreateBillButton.TabIndex = 6;
			this.CreateBillButton.Text = "Create &Bill";
			this.CreateBillButton.Click += new System.EventHandler(this.CreateBillButton_Click);
			// 
			// CloseButton
			// 
			this.CloseButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.CloseButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CloseButton.Location = new System.Drawing.Point(700, 328);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(84, 24);
			this.CloseButton.TabIndex = 7;
			this.CloseButton.Text = "&Close";
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// ToLabel
			// 
			this.ToLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ToLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ToLabel.Location = new System.Drawing.Point(84, 38);
			this.ToLabel.Name = "ToLabel";
			this.ToLabel.Size = new System.Drawing.Size(28, 22);
			this.ToLabel.TabIndex = 40;
			this.ToLabel.Tag = null;
			this.ToLabel.Text = "To:";
			this.ToLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ToLabel.TextDetached = true;
			// 
			// FromLabel
			// 
			this.FromLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.FromLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FromLabel.Location = new System.Drawing.Point(60, 10);
			this.FromLabel.Name = "FromLabel";
			this.FromLabel.Size = new System.Drawing.Size(52, 22);
			this.FromLabel.TabIndex = 39;
			this.FromLabel.Tag = null;
			this.FromLabel.Text = "From:";
			this.FromLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.FromLabel.TextDetached = true;
			// 
			// FromDatePicker
			// 
			this.FromDatePicker.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FromDatePicker.Location = new System.Drawing.Point(112, 11);
			this.FromDatePicker.Name = "FromDatePicker";
			this.FromDatePicker.Size = new System.Drawing.Size(224, 21);
			this.FromDatePicker.TabIndex = 38;
			this.FromDatePicker.ValueChanged += new System.EventHandler(this.ToDatePicker_ValueChanged);
			// 
			// ToDatePicker
			// 
			this.ToDatePicker.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ToDatePicker.Location = new System.Drawing.Point(112, 39);
			this.ToDatePicker.Name = "ToDatePicker";
			this.ToDatePicker.Size = new System.Drawing.Size(224, 21);
			this.ToDatePicker.TabIndex = 37;
			this.ToDatePicker.ValueChanged += new System.EventHandler(this.ToDatePicker_ValueChanged);
			// 
			// GroupCodeCombo
			// 
			this.GroupCodeCombo.AddItemSeparator = ';';
			this.GroupCodeCombo.Caption = "";
			this.GroupCodeCombo.CaptionHeight = 17;
			this.GroupCodeCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
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
			this.GroupCodeCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GroupCodeCombo.GapHeight = 2;
			this.GroupCodeCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.GroupCodeCombo.ItemHeight = 15;
			this.GroupCodeCombo.Location = new System.Drawing.Point(112, 67);
			this.GroupCodeCombo.MatchEntryTimeout = ((long)(2000));
			this.GroupCodeCombo.MaxDropDownItems = ((short)(5));
			this.GroupCodeCombo.MaxLength = 32767;
			this.GroupCodeCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.GroupCodeCombo.Name = "GroupCodeCombo";
			this.GroupCodeCombo.PartialRightColumn = false;
			this.GroupCodeCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.GroupCodeCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.GroupCodeCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.GroupCodeCombo.Size = new System.Drawing.Size(224, 22);
			this.GroupCodeCombo.TabIndex = 46;
			this.GroupCodeCombo.TextChanged += new System.EventHandler(this.GroupCodeCombo_TextChanged);
			this.GroupCodeCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Corresponde" +
				"nt\" DataField=\"GroupCode\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" C" +
				"aption=\"Correspondent Name\" DataField=\"GroupName\"><ValueItems /></C1DataColumn><" +
				"/DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackCol" +
				"or:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;" +
				"}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeCo" +
				"lor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptio" +
				"nText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:V" +
				"erdana, 8.25pt;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9" +
				"{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Style15{AlignHorz:Nea" +
				"r;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:Contro" +
				"lText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Ne" +
				"ar;}Style16{AlignHorz:Near;}Style17{}Style1{}</Data></Styles><Splits><C1.Win.C1L" +
				"ist.ListBoxView AllowColSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionH" +
				"eight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" VerticalScrollGroup=" +
				"\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCo" +
				"ls><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"" +
				"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivide" +
				"r><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height" +
				"><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"\" me=\"Style15\" /><Style parent=\"Style1\" me=\"Style16\" /><FooterStyle parent=\"Styl" +
				"e3\" me=\"Style17\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></" +
				"ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn></internalCol" +
				"s><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HSc" +
				"rollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRo" +
				"w\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"" +
				"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRow" +
				"Style parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"S" +
				"tyle4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=" +
				"\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><" +
				"Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedS" +
				"tyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Styl" +
				"e parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style p" +
				"arent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style pa" +
				"rent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style p" +
				"arent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Styl" +
				"e parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSpl" +
				"its>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSel" +
				"Width></Blob>";
			// 
			// GroupCodeLabel
			// 
			this.GroupCodeLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.GroupCodeLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GroupCodeLabel.Location = new System.Drawing.Point(12, 68);
			this.GroupCodeLabel.Name = "GroupCodeLabel";
			this.GroupCodeLabel.Size = new System.Drawing.Size(100, 21);
			this.GroupCodeLabel.TabIndex = 45;
			this.GroupCodeLabel.Tag = null;
			this.GroupCodeLabel.Text = "Group Code:";
			this.GroupCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.GroupCodeLabel.TextDetached = true;
			// 
			// c1Label1
			// 
			this.c1Label1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.c1Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.c1Label1.Enabled = false;
			this.c1Label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.c1Label1.ForeColor = System.Drawing.SystemColors.Highlight;
			this.c1Label1.Location = new System.Drawing.Point(217, 240);
			this.c1Label1.Name = "c1Label1";
			this.c1Label1.Size = new System.Drawing.Size(144, 24);
			this.c1Label1.TabIndex = 50;
			this.c1Label1.Tag = null;
			this.c1Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.c1Label1.TextDetached = true;
			// 
			// c1Label2
			// 
			this.c1Label2.BackColor = System.Drawing.SystemColors.ControlLight;
			this.c1Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.c1Label2.Enabled = false;
			this.c1Label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.c1Label2.ForeColor = System.Drawing.SystemColors.Highlight;
			this.c1Label2.Location = new System.Drawing.Point(217, 272);
			this.c1Label2.Name = "c1Label2";
			this.c1Label2.Size = new System.Drawing.Size(144, 21);
			this.c1Label2.TabIndex = 49;
			this.c1Label2.Tag = null;
			this.c1Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.c1Label2.TextDetached = true;
			// 
			// ChangeAccountsButton
			// 
			this.ChangeAccountsButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ChangeAccountsButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ChangeAccountsButton.Location = new System.Drawing.Point(680, 104);
			this.ChangeAccountsButton.Name = "ChangeAccountsButton";
			this.ChangeAccountsButton.Size = new System.Drawing.Size(84, 24);
			this.ChangeAccountsButton.TabIndex = 51;
			this.ChangeAccountsButton.Text = "&Modify";
			this.ChangeAccountsButton.Click += new System.EventHandler(this.ChangeAccountsButton_Click);
			// 
			// CorrespondentsBilledNumberLabel
			// 
			this.CorrespondentsBilledNumberLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.CorrespondentsBilledNumberLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CorrespondentsBilledNumberLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CorrespondentsBilledNumberLabel.ForeColor = System.Drawing.Color.Navy;
			this.CorrespondentsBilledNumberLabel.Location = new System.Drawing.Point(642, 8);
			this.CorrespondentsBilledNumberLabel.Name = "CorrespondentsBilledNumberLabel";
			this.CorrespondentsBilledNumberLabel.Size = new System.Drawing.Size(144, 24);
			this.CorrespondentsBilledNumberLabel.TabIndex = 53;
			this.CorrespondentsBilledNumberLabel.Tag = null;
			this.CorrespondentsBilledNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.CorrespondentsBilledNumberLabel.TextDetached = true;
			// 
			// GroupCodesBeingBilledLabel
			// 
			this.GroupCodesBeingBilledLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.GroupCodesBeingBilledLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GroupCodesBeingBilledLabel.Location = new System.Drawing.Point(496, 8);
			this.GroupCodesBeingBilledLabel.Name = "GroupCodesBeingBilledLabel";
			this.GroupCodesBeingBilledLabel.Size = new System.Drawing.Size(140, 24);
			this.GroupCodesBeingBilledLabel.TabIndex = 52;
			this.GroupCodesBeingBilledLabel.Tag = null;
			this.GroupCodesBeingBilledLabel.Text = "Group Codes Billed:";
			this.GroupCodesBeingBilledLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.GroupCodesBeingBilledLabel.TextDetached = true;
			// 
			// TotalAmountBilledLabel
			// 
			this.TotalAmountBilledLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TotalAmountBilledLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TotalAmountBilledLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TotalAmountBilledLabel.ForeColor = System.Drawing.Color.Navy;
			this.TotalAmountBilledLabel.Location = new System.Drawing.Point(642, 72);
			this.TotalAmountBilledLabel.Name = "TotalAmountBilledLabel";
			this.TotalAmountBilledLabel.Size = new System.Drawing.Size(144, 24);
			this.TotalAmountBilledLabel.TabIndex = 55;
			this.TotalAmountBilledLabel.Tag = null;
			this.TotalAmountBilledLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.TotalAmountBilledLabel.TextDetached = true;
			// 
			// TotalAmountBeingBilledLabel
			// 
			this.TotalAmountBeingBilledLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TotalAmountBeingBilledLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TotalAmountBeingBilledLabel.Location = new System.Drawing.Point(496, 72);
			this.TotalAmountBeingBilledLabel.Name = "TotalAmountBeingBilledLabel";
			this.TotalAmountBeingBilledLabel.Size = new System.Drawing.Size(140, 24);
			this.TotalAmountBeingBilledLabel.TabIndex = 54;
			this.TotalAmountBeingBilledLabel.Tag = null;
			this.TotalAmountBeingBilledLabel.Text = "Total Amount Billed:";
			this.TotalAmountBeingBilledLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.TotalAmountBeingBilledLabel.TextDetached = true;
			// 
			// TotalSecuritiesItemsBilledLabel
			// 
			this.TotalSecuritiesItemsBilledLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TotalSecuritiesItemsBilledLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TotalSecuritiesItemsBilledLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TotalSecuritiesItemsBilledLabel.ForeColor = System.Drawing.Color.Navy;
			this.TotalSecuritiesItemsBilledLabel.Location = new System.Drawing.Point(642, 40);
			this.TotalSecuritiesItemsBilledLabel.Name = "TotalSecuritiesItemsBilledLabel";
			this.TotalSecuritiesItemsBilledLabel.Size = new System.Drawing.Size(144, 24);
			this.TotalSecuritiesItemsBilledLabel.TabIndex = 57;
			this.TotalSecuritiesItemsBilledLabel.Tag = null;
			this.TotalSecuritiesItemsBilledLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.TotalSecuritiesItemsBilledLabel.TextDetached = true;
			// 
			// TotalSecuritiesBilledLabel
			// 
			this.TotalSecuritiesBilledLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TotalSecuritiesBilledLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TotalSecuritiesBilledLabel.Location = new System.Drawing.Point(496, 40);
			this.TotalSecuritiesBilledLabel.Name = "TotalSecuritiesBilledLabel";
			this.TotalSecuritiesBilledLabel.Size = new System.Drawing.Size(140, 24);
			this.TotalSecuritiesBilledLabel.TabIndex = 56;
			this.TotalSecuritiesBilledLabel.Tag = null;
			this.TotalSecuritiesBilledLabel.Text = "Total Securities Billed:";
			this.TotalSecuritiesBilledLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.TotalSecuritiesBilledLabel.TextDetached = true;
			// 
			// BackgroundPanel
			// 
			this.BackgroundPanel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.BackgroundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.BackgroundPanel.Controls.Add(this.ViewButton);
			this.BackgroundPanel.Controls.Add(this.AccountsCreditDebitsLabel);
			this.BackgroundPanel.Controls.Add(this.MessageListBox);
			this.BackgroundPanel.Controls.Add(this.AutoEmailAccountsBorrowedCheck);
			this.BackgroundPanel.Controls.Add(this.c1Label5);
			this.BackgroundPanel.Controls.Add(this.AutoEmailMasterBillCheck);
			this.BackgroundPanel.Controls.Add(this.c1Label4);
			this.BackgroundPanel.Controls.Add(this.StatusLabel);
			this.BackgroundPanel.Controls.Add(this.CreateBillButton);
			this.BackgroundPanel.Controls.Add(this.AutoEmailCheck);
			this.BackgroundPanel.Controls.Add(this.c1Label3);
			this.BackgroundPanel.Controls.Add(this.CorrespondentsBilledNumberLabel);
			this.BackgroundPanel.Controls.Add(this.TotalSecuritiesItemsBilledLabel);
			this.BackgroundPanel.Controls.Add(this.TotalAmountBilledLabel);
			this.BackgroundPanel.Controls.Add(this.TotalSecuritiesBilledLabel);
			this.BackgroundPanel.Controls.Add(this.TotalAmountBeingBilledLabel);
			this.BackgroundPanel.Controls.Add(this.GroupCodesBeingBilledLabel);
			this.BackgroundPanel.Controls.Add(this.ChangeAccountsButton);
			this.BackgroundPanel.Controls.Add(this.AccountsBillingMethodLabel);
			this.BackgroundPanel.Controls.Add(this.CloseButton);
			this.BackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BackgroundPanel.DockPadding.All = 1;
			this.BackgroundPanel.Location = new System.Drawing.Point(1, 1);
			this.BackgroundPanel.Name = "BackgroundPanel";
			this.BackgroundPanel.Size = new System.Drawing.Size(880, 369);
			this.BackgroundPanel.TabIndex = 59;
			// 
			// ViewButton
			// 
			this.ViewButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ViewButton.Enabled = false;
			this.ViewButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ViewButton.Location = new System.Drawing.Point(680, 136);
			this.ViewButton.Name = "ViewButton";
			this.ViewButton.Size = new System.Drawing.Size(84, 24);
			this.ViewButton.TabIndex = 68;
			this.ViewButton.Text = "&View";
			this.ViewButton.Click += new System.EventHandler(this.ViewButton_Click);
			// 
			// AccountsCreditDebitsLabel
			// 
			this.AccountsCreditDebitsLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.AccountsCreditDebitsLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AccountsCreditDebitsLabel.Location = new System.Drawing.Point(480, 136);
			this.AccountsCreditDebitsLabel.Name = "AccountsCreditDebitsLabel";
			this.AccountsCreditDebitsLabel.Size = new System.Drawing.Size(164, 24);
			this.AccountsCreditDebitsLabel.TabIndex = 67;
			this.AccountsCreditDebitsLabel.Tag = null;
			this.AccountsCreditDebitsLabel.Text = "Account Credit/Debits:";
			this.AccountsCreditDebitsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.AccountsCreditDebitsLabel.TextDetached = true;
			// 
			// MessageListBox
			// 
			this.MessageListBox.Location = new System.Drawing.Point(112, 208);
			this.MessageListBox.Name = "MessageListBox";
			this.MessageListBox.Size = new System.Drawing.Size(672, 108);
			this.MessageListBox.TabIndex = 66;
			// 
			// AutoEmailAccountsBorrowedCheck
			// 
			this.AutoEmailAccountsBorrowedCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.AutoEmailAccountsBorrowedCheck.Location = new System.Drawing.Point(320, 136);
			this.AutoEmailAccountsBorrowedCheck.Name = "AutoEmailAccountsBorrowedCheck";
			this.AutoEmailAccountsBorrowedCheck.Size = new System.Drawing.Size(16, 24);
			this.AutoEmailAccountsBorrowedCheck.TabIndex = 65;
			this.AutoEmailAccountsBorrowedCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// c1Label5
			// 
			this.c1Label5.BackColor = System.Drawing.SystemColors.ControlLight;
			this.c1Label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.c1Label5.Location = new System.Drawing.Point(36, 136);
			this.c1Label5.Name = "c1Label5";
			this.c1Label5.Size = new System.Drawing.Size(216, 24);
			this.c1Label5.TabIndex = 64;
			this.c1Label5.Tag = null;
			this.c1Label5.Text = "Auto Email Accounts Borrowed Bill:";
			this.c1Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.c1Label5.TextDetached = true;
			// 
			// AutoEmailMasterBillCheck
			// 
			this.AutoEmailMasterBillCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.AutoEmailMasterBillCheck.Location = new System.Drawing.Point(320, 168);
			this.AutoEmailMasterBillCheck.Name = "AutoEmailMasterBillCheck";
			this.AutoEmailMasterBillCheck.Size = new System.Drawing.Size(16, 24);
			this.AutoEmailMasterBillCheck.TabIndex = 61;
			this.AutoEmailMasterBillCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// c1Label4
			// 
			this.c1Label4.BackColor = System.Drawing.SystemColors.ControlLight;
			this.c1Label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.c1Label4.Location = new System.Drawing.Point(88, 168);
			this.c1Label4.Name = "c1Label4";
			this.c1Label4.Size = new System.Drawing.Size(164, 24);
			this.c1Label4.TabIndex = 60;
			this.c1Label4.Tag = null;
			this.c1Label4.Text = "Auto Email Master Bill:";
			this.c1Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.c1Label4.TextDetached = true;
			// 
			// StatusLabel
			// 
			this.StatusLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.StatusLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.StatusLabel.Location = new System.Drawing.Point(60, 208);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(52, 16);
			this.StatusLabel.TabIndex = 30;
			this.StatusLabel.Tag = null;
			this.StatusLabel.Text = "Status:";
			this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.StatusLabel.TextDetached = true;
			// 
			// AutoEmailCheck
			// 
			this.AutoEmailCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.AutoEmailCheck.Location = new System.Drawing.Point(320, 104);
			this.AutoEmailCheck.Name = "AutoEmailCheck";
			this.AutoEmailCheck.Size = new System.Drawing.Size(16, 24);
			this.AutoEmailCheck.TabIndex = 59;
			this.AutoEmailCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// c1Label3
			// 
			this.c1Label3.BackColor = System.Drawing.SystemColors.ControlLight;
			this.c1Label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.c1Label3.Location = new System.Drawing.Point(88, 104);
			this.c1Label3.Name = "c1Label3";
			this.c1Label3.Size = new System.Drawing.Size(164, 24);
			this.c1Label3.TabIndex = 58;
			this.c1Label3.Tag = null;
			this.c1Label3.Text = "Auto Email Bills:";
			this.c1Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.c1Label3.TextDetached = true;
			// 
			// AccountsBillingMethodLabel
			// 
			this.AccountsBillingMethodLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.AccountsBillingMethodLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AccountsBillingMethodLabel.Location = new System.Drawing.Point(480, 104);
			this.AccountsBillingMethodLabel.Name = "AccountsBillingMethodLabel";
			this.AccountsBillingMethodLabel.Size = new System.Drawing.Size(164, 24);
			this.AccountsBillingMethodLabel.TabIndex = 48;
			this.AccountsBillingMethodLabel.Tag = null;
			this.AccountsBillingMethodLabel.Text = "Accounts Billing Method:";
			this.AccountsBillingMethodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.AccountsBillingMethodLabel.TextDetached = true;
			// 
			// ShortSaleBillingBillsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(882, 371);
			this.Controls.Add(this.GroupCodeCombo);
			this.Controls.Add(this.GroupCodeLabel);
			this.Controls.Add(this.ToLabel);
			this.Controls.Add(this.FromLabel);
			this.Controls.Add(this.FromDatePicker);
			this.Controls.Add(this.ToDatePicker);
			this.Controls.Add(this.BackgroundPanel);
			this.DockPadding.All = 1;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ShortSaleBillingBillsForm";
			this.Text = "ShortSale - Billing - Bills";
			this.Load += new System.EventHandler(this.ShortSaleBillingBillsForm_Load);
			this.Closed += new System.EventHandler(this.ShortSaleBillingBillsForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.ToLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.FromLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CorrespondentsBilledNumberLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodesBeingBilledLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TotalAmountBilledLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TotalAmountBeingBilledLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TotalSecuritiesItemsBilledLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TotalSecuritiesBilledLabel)).EndInit();
			this.BackgroundPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.AccountsCreditDebitsLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.AccountsBillingMethodLabel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

    

		private void ShortSaleBillingBillsForm_Load(object sender, System.EventArgs e)
		{       
			this.WindowState = FormWindowState.Normal;
      
			try
			{
				tradingGroupsDataSet = mainForm.ShortSaleAgent.TradingGroupsGet(mainForm.ShortSaleAgent.TradeDate(), mainForm.UtcOffset);				
								
				DataRow tempRow = 		tradingGroupsDataSet.Tables["TradingGroups"].NewRow();
				tempRow["GroupCode"] = "**ALL**";
				tempRow["GroupName"] = "**ALL**";

				tradingGroupsDataSet.Tables["TradingGroups"].Rows.Add(tempRow);							
				
				GroupCodeCombo.DataSource = tradingGroupsDataSet;
				GroupCodeCombo.DataMember = "TradingGroups";
				GroupCodeCombo.Text = "**ALL**";

				if (!groupCode.Equals(""))
				{
					this.GroupCodeCombo.Text = groupCode;
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleBillingRateChangeInputForm.ShortSaleBillingRateChangeInputForm_Load]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}
	
		private void ShortSaleBillingBillsForm_Closed(object sender, System.EventArgs e)
		{
			if (shortSaleBillingBillsAccountsForm != null)
			{
				shortSaleBillingBillsAccountsForm.Close();
			}

			mainForm.Refresh();
		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void CreateBillButton_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			long groupCodeCount = 0;
			long secIdCount = 0;
			decimal totalCharges = 0;

			string bill = "";						
			string page = "";
			string fileName = "";
			int index = 0;
		

			try
			{	
			
			TradingGroupFile tradingGroupFile;
			
			files = new ArrayList();		
			
			tempPath = mainForm.ServiceAgent.KeyValueGet("ShortSaleBillsLocation", @"\\penson.com\Shares\Apps\Sendero\Bills");												
			tempPath += "_" + mainForm.ServiceAgent.BizDate() + @"\";

			if (!Directory.Exists(tempPath))
			{	
				Directory.CreateDirectory(tempPath);
			}

					
				if (GroupCodeCombo.Text.Equals("**ALL**"))			
				{
					foreach (DataRow dr in tradingGroupsDataSet.Tables["TradingGroups"].Rows)
					{
						
						
							if (dr["GroupCode"].ToString().Equals("**ALL**"))
							{
								continue;
							}
							else
							{
								if (bool.Parse(dr["NegativeRebateBill"].ToString()))
								{
								groupCodeCount = 0;
								secIdCount = 0;
								totalCharges = 0;
						
								mainForm.RebateAgent.ShortSaleBillingSummaryBillingReportGet(
									FromDatePicker.Text.ToString(), 
									ToDatePicker.Text.ToString(), 
									dr["GroupCode"].ToString(), 
									ref groupCodeCount, 
									ref secIdCount, 
									ref totalCharges);
						
								if (secIdCount > 0)
								{
									index = 0;
									bill = mainForm.RebateAgent.ShortSaleBillingSummaryBillGet(FromDatePicker.Text.ToString(), ToDatePicker.Text.ToString(), dr["GroupCode"].ToString());
									C1PdfDocument doc = new C1PdfDocument(System.Drawing.Printing.PaperKind.Legal, true);								
									page = Tools.SplitItem(bill, "!", 0);								
							
									Font font = new Font("Courier New", 10);
									RectangleF rc = doc.PageRectangle;
									rc.Inflate(-72, -72);
									pageNumber = doc.CurrentPage + 1;
									doc.DrawString(pageNumber.ToString(), font, Brushes.Black, new RectangleF(rc.Left, rc.Bottom, 30, 30));

									while(true)
									{																																		
										doc.DrawString(page, font, Brushes.Black, rc);
										index ++;
						
										page = Tools.SplitItem(bill, "!", index);
							
										if (page.Equals(""))
										{
											break;
										}
										else
										{
											doc.NewPage();	
											pageNumber = doc.CurrentPage + 1;
											doc.DrawString(pageNumber.ToString(), font, Brushes.Black, new RectangleF(rc.Left, rc.Bottom, 30, 30));
										}			
									}		

									fileName = tempPath + "ShortSaleNegativeBill_" + FromDatePicker.Value.ToString("MMddyy") + ToDatePicker.Value.ToString("MMddyy")  + "_" + dr["GroupCode"].ToString() + ".pdf";				
				
									if (File.Exists(fileName))
									{
										File.Delete(fileName);
									}

									doc.Save(fileName);										

									tradingGroupFile = new TradingGroupFile(dr["GroupCode"].ToString(), fileName, TradingGroupEmailAddress(dr["GroupCode"].ToString()));
							
									files.Add(tradingGroupFile);

									ListBoxWrite("Wrote bill document file to : " + fileName);		
									mainForm.Alert("Wrote bill document file to : " + fileName, PilotState.Normal);																																																																
								}
							}
						}
					}
				}	
				else 
				{						
					groupCodeCount = 0;
					secIdCount = 0;
					totalCharges = 0;					

					mainForm.RebateAgent.ShortSaleBillingSummaryBillingReportGet(
						FromDatePicker.Text.ToString(), 
						ToDatePicker.Text.ToString(), 
						GroupCodeCombo.Text,
						ref groupCodeCount, 
						ref secIdCount, 
						ref totalCharges);
						
					if (secIdCount > 0)
					{
						index = 0;
						bill = mainForm.RebateAgent.ShortSaleBillingSummaryBillGet(FromDatePicker.Text.ToString(), ToDatePicker.Text.ToString(), GroupCodeCombo.Text);
						C1PdfDocument doc = new C1PdfDocument(System.Drawing.Printing.PaperKind.Legal, true);
						
						page = Tools.SplitItem(bill, "!", 0);						

						Font font = new Font("Courier New", 10);
						RectangleF rc = doc.PageRectangle;
						rc.Inflate(-72, -72);
						
						pageNumber = doc.CurrentPage + 1;
						doc.DrawString(pageNumber.ToString(), font, Brushes.Black, new RectangleF(rc.Left, rc.Bottom, 30, 30));

						while(true)
						{																										
							doc.DrawString(page, font, Brushes.Black, rc);
							index ++;
						
							page = Tools.SplitItem(bill, "!", index);
							
							if (page.Equals(""))
							{
								break;
							}
							else
							{
								doc.NewPage();								
								pageNumber = doc.CurrentPage + 1;
								doc.DrawString(pageNumber.ToString(), font, Brushes.Black, new RectangleF(rc.Left, rc.Bottom, 30, 30));
							}						
						}		
						
						fileName = tempPath + "ShortSaleNegativeBill_" + FromDatePicker.Value.ToString("MMddyy") + ToDatePicker.Value.ToString("MMddyy") + "_" + GroupCodeCombo.Text + ".pdf";								
						
						if (File.Exists(fileName))
						{
							File.Delete(fileName);
						}

						doc.Save(fileName);										

						tradingGroupFile = new TradingGroupFile(GroupCodeCombo.Text, fileName, TradingGroupEmailAddress(GroupCodeCombo.Text));							
						files.Add(tradingGroupFile);

						ListBoxWrite("Wrote bill document file to : " + fileName);						
						mainForm.Alert("Wrote bill document file to : " + fileName, PilotState.RunFault);								
					}
				}
			
			 // Show directory with files
				
				if (AutoEmailCheck.Checked) //AutoEmail Files?
				{
					AutoEmailDo(); //Auto Email Files
				}
							
				AutoEmailMasterDo();
				
				if (AutoEmailAccountsBorrowedCheck.Checked)
				{
					AutoEmailAccountsBorrowedDo();
				}
				
				Process.Start(tempPath);
				
				mainForm.RebateAgent.ShortSaleBillingSummaryDatesLock(
					FromDatePicker.Text, 
					ToDatePicker.Text, 
					(GroupCodeCombo.Text.Equals("**ALL**")? "":GroupCodeCombo.Text), 
					"", 
					true, 
					mainForm.UserId);
			}		
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}	
	
			this.Cursor = Cursors.Default;
		}

		private void GroupCodeCombo_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				long groupCodeCount = 0;
				long secIdCount = 0;
				decimal totalCharges = 0;

				mainForm.RebateAgent.ShortSaleBillingSummaryBillingReportGet(
					FromDatePicker.Text.ToString(), 
					ToDatePicker.Text.ToString(), 
					(GroupCodeCombo.Text.Equals("**ALL**")? "":GroupCodeCombo.Text), 
					ref groupCodeCount, 
					ref secIdCount, 
					ref totalCharges);

				CorrespondentsBilledNumberLabel.Text = groupCodeCount.ToString("#,##0");
				TotalSecuritiesItemsBilledLabel.Text = secIdCount.ToString("#,##0");
				TotalAmountBilledLabel.Text = totalCharges.ToString("#,##0.00");
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void ChangeAccountsButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				shortSaleBillingBillsAccountsForm = new ShortSaleBillingBillsAccountsForm(mainForm, (GroupCodeCombo.Text.Equals("**ALL**")? "":GroupCodeCombo.Text));
				shortSaleBillingBillsAccountsForm.MdiParent = mainForm;
				shortSaleBillingBillsAccountsForm.Show();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private string TradingGroupEmailAddress (string groupCode)
		{
			string emailAddress = "";
			tradingGroupMethodsDataSet = mainForm.RebateAgent.ShortSaleBillingSummaryAccountsMethodGet(groupCode, 0);

			try
			{
				foreach (DataRow dr in tradingGroupMethodsDataSet.Tables["Accounts"].Rows)
				{
					mainForm.Alert(dr["GroupCode"].ToString());
					if (dr["GroupCode"].ToString().Trim().Equals(groupCode.Trim()) && bool.Parse(dr["IsPaperBilling"].ToString()))
					{
						emailAddress = dr["EmailAddress"].ToString();
						return emailAddress;
					}
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			ListBoxWrite(error.Message);
			}

			return emailAddress;
		}

		private void AutoEmailDo()
		{
			try
			{
				if (files.Count == 0)
				{
					mainForm.Alert("No emails sent.", PilotState.Normal);
				}
			
				Email email = new Email();

				foreach (TradingGroupFile tg in files)
				{
					if (!tg.EmailAddress().Equals(""))
					{				
						email.Send(tg.EmailAddress(), "", "Negative Rebate Bill For " + mainForm.ServiceAgent.BizDate(),"",tg.FilePath(), true);			
					}
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			ListBoxWrite(error.Message);
			}
		}

		private void AutoEmailAccountsBorrowedDo()
		{
			try
			{
				string mailContent = mainForm.RebateAgent.ShortSaleBillingSummaryAccountsBorrowedBillGet(FromDatePicker.Text, ToDatePicker.Text);
				Email email = new Email();

				email.Send(
					mainForm.ServiceAgent.KeyValueGet("ShortSaleAccountsCoveredMailTo", "mbattaini@penson.com"),
					"",
					"Covered Shorts Do Not Buyin! For " + Tools.FormatDate(mainForm.ServiceAgent.ContractsBizDate(), "dddd, d MMMM yyyy"),
					mailContent,  
					"",
					true);				
        
				ListBoxWrite("Short Sale Accouts Covered e-mail notice has been sent for " + mainForm.ServiceAgent.BizDate());
				Log.Write("Short Sale Accouts Covered e-mail notice has been sent for " + mainForm.ServiceAgent.BizDate() + ". [MedalistMain.MailSendShortSaleAccountsCovered]", 2);
			}
			catch (Exception error)
			{				
				Log.Write(error.Message + " [MedalistMain.MailSendShortSaleAccountsCharged]", Log.Error, 1);
				ListBoxWrite(error.Message);
			}
		
			ListBoxWrite("Accounts Borrowed Email Sent.");		
		}	

		private void AutoEmailMasterDo()
		{
			int index = 0;

			string fileName = "";
			string page = "";
			string bill = "";
			TradingGroupFile tradingGroupFile;
													
			try
			{				
				fileName = tempPath + "ShortSaleNegativeBill_" + FromDatePicker.Value.ToString("MMddyy") + ToDatePicker.Value.ToString("MMddyy")  + "_MASTERBILL.pdf";											
				C1PdfDocument doc = new C1PdfDocument(System.Drawing.Printing.PaperKind.Legal, true);
				
				if (File.Exists(fileName))
				{
					File.Delete(fileName);
				}
								
				bill = mainForm.RebateAgent.ShortSaleBillingSummaryMasterBillGet(FromDatePicker.Text.ToString(), ToDatePicker.Text.ToString());

				index = 0;				
							
				page = Tools.SplitItem(bill, "!", 0);
							
				Font font = new Font("Courier New", 10);
				RectangleF rc = doc.PageRectangle;
				rc.Inflate(-72, -72);
												
				while(true)
				{																																		
					doc.DrawString(page, font, Brushes.Black, rc);
					index ++;
						
					page = Tools.SplitItem(bill, "!", index);
							
					if (page.Equals(""))
					{
						break;
					}
					else
					{
						doc.NewPage();								
						pageNumber = doc.CurrentPage + 1;
						doc.DrawString(pageNumber.ToString(), font, Brushes.Black, new RectangleF(rc.Left, rc.Bottom, 30, 30));
					}										
				}		
														
				doc.NewPage();																											
				pageNumber = doc.CurrentPage + 1;
				doc.DrawString(pageNumber.ToString(), font, Brushes.Black, new RectangleF( rc.Left, rc.Bottom, 30, 30));																		
				
				ListBoxWrite("Wrote bill document file to : " + fileName);								
				mainForm.Alert("Wrote bill document file to : " + fileName, PilotState.RunFault);		
				
				doc.Save(fileName);										
				
				files.Clear();
				tradingGroupFile = new TradingGroupFile("MASTERBILL", fileName, mainForm.ServiceAgent.KeyValueGet("ShortSaleBillingMasterEmailList", "mBattaini@penson.com"));
				files.Add(tradingGroupFile);

				if (AutoEmailMasterBillCheck.Checked)
				{
					AutoEmailDo();
				}
			}
			catch (Exception error)
			{				
				Log.Write(error.Message + " [MedalistMain.AutoEmailMasterDo]", Log.Error, 1);
				ListBoxWrite(error.Message);				
			}

			ListBoxWrite("Master Bill Sent");			
		}	

		private void ToDatePicker_ValueChanged(object sender, System.EventArgs e)
		{
			try
			{
				long groupCodeCount = 0;
				long secIdCount = 0;
				decimal totalCharges = 0;

				if ((!FromDatePicker.Text.Equals("")) && (!ToDatePicker.Text.Equals("")))
				{
				
					mainForm.RebateAgent.ShortSaleBillingSummaryBillingReportGet(
						FromDatePicker.Text.ToString(), 
						ToDatePicker.Text.ToString(), 
						(GroupCodeCombo.Text.Equals("**ALL**")? "":GroupCodeCombo.Text), 
						ref groupCodeCount, 
						ref secIdCount, 
						ref totalCharges);

					CorrespondentsBilledNumberLabel.Text = groupCodeCount.ToString("#,##0");
					TotalSecuritiesItemsBilledLabel.Text = secIdCount.ToString("#,##0");
					TotalAmountBilledLabel.Text = totalCharges.ToString("#,##0.00");
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void ListBoxWrite(string message)
		{
			try
			{
				MessageListBox.Items.Add( DateTime.Now.ToString(Standard.DateTimeShortFormat) + " " + message);
			}
			catch {}
		}

		private void ViewButton_Click(object sender, System.EventArgs e)
		{
			try
			{
			//	shortSaleBillingBillsAccountsStatusForm = new ShortSaleBillingBillsAccountsStatusForm(mainForm, (GroupCodeCombo.Text.Equals("**ALL**")? "":GroupCodeCombo.Text));
			//	shortSaleBillingBillsAccountsStatusForm.MdiParent = mainForm;
			//	shortSaleBillingBillsAccountsStatusForm.Show();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}	
	}
}
