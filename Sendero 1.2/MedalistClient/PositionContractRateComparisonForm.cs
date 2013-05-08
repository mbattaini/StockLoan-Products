// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Remoting;
using System.ComponentModel;
using System.Windows.Forms;
using Anetics.Common;
using Anetics.Medalist;

namespace Anetics.Medalist
{
	public class PositionContractRateComparisonForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
    
		private MainForm mainForm;
		private DataSet dataSet = null;
		private DataSet dataSet2 = null;
		private DataView bookView = null;
		private DataView ratesView = null;
		private string bookViewFilter = "";
		private string ratesViewFilter = "";
		private string secId = "";		
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep2MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private C1.Win.C1List.C1Combo BookGroupCombo;
		private System.Windows.Forms.RadioButton BorrowsRadio;
		private System.Windows.Forms.RadioButton LoansRadio;
		private C1.Win.C1Input.C1Label BookGroupNameLabel;
		private C1.Win.C1Input.C1Label BookGroupLabel;
		private C1.Win.C1List.C1Combo BookCombo;
		private C1.Win.C1Input.C1Label BookNameLabel;
		private C1.Win.C1Input.C1Label BookLabel;
		private C1.Win.C1Input.C1DateEdit DateEditor;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid PositionContractRateComparisonGrid;
		private C1.Win.C1Input.C1Label EffectDateLabel;

		public PositionContractRateComparisonForm(MainForm mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;	
			dataSet = new DataSet();
			dataSet2 = new DataSet();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionContractRateComparisonForm));
			this.PositionContractRateComparisonGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.BookGroupCombo = new C1.Win.C1List.C1Combo();
			this.BorrowsRadio = new System.Windows.Forms.RadioButton();
			this.LoansRadio = new System.Windows.Forms.RadioButton();
			this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
			this.BookGroupLabel = new C1.Win.C1Input.C1Label();
			this.BookCombo = new C1.Win.C1List.C1Combo();
			this.BookNameLabel = new C1.Win.C1Input.C1Label();
			this.BookLabel = new C1.Win.C1Input.C1Label();
			this.DateEditor = new C1.Win.C1Input.C1DateEdit();
			this.EffectDateLabel = new C1.Win.C1Input.C1Label();
			((System.ComponentModel.ISupportInitialize)(this.PositionContractRateComparisonGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookNameLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DateEditor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// PositionContractRateComparisonGrid
			// 
			this.PositionContractRateComparisonGrid.CaptionHeight = 17;
			this.PositionContractRateComparisonGrid.ContextMenu = this.MainContextMenu;
			this.PositionContractRateComparisonGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PositionContractRateComparisonGrid.EmptyRows = true;
			this.PositionContractRateComparisonGrid.ExtendRightColumn = true;
			this.PositionContractRateComparisonGrid.FetchRowStyles = true;
			this.PositionContractRateComparisonGrid.FilterBar = true;
			this.PositionContractRateComparisonGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.PositionContractRateComparisonGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.PositionContractRateComparisonGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.PositionContractRateComparisonGrid.Location = new System.Drawing.Point(1, 60);
			this.PositionContractRateComparisonGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.PositionContractRateComparisonGrid.Name = "PositionContractRateComparisonGrid";
			this.PositionContractRateComparisonGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.PositionContractRateComparisonGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.PositionContractRateComparisonGrid.PreviewInfo.ZoomFactor = 75;
			this.PositionContractRateComparisonGrid.RecordSelectorWidth = 16;
			this.PositionContractRateComparisonGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.PositionContractRateComparisonGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.PositionContractRateComparisonGrid.RowHeight = 16;
			this.PositionContractRateComparisonGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.PositionContractRateComparisonGrid.Size = new System.Drawing.Size(710, 368);
			this.PositionContractRateComparisonGrid.TabIndex = 0;
			this.PositionContractRateComparisonGrid.Text = "Accounts";
			this.PositionContractRateComparisonGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.PositionContractRateComparisonGrid_Paint);
			this.PositionContractRateComparisonGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.PositionContractRateComparisonGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"BookGroup\" " +
				"DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"Book\" DataField=\"Book\"><ValueItems /><GroupInfo /></C1DataColum" +
				"n><C1DataColumn Level=\"0\" Caption=\"Book Average Weighted Rate\" DataField=\"BookAv" +
				"erageWeightedRate\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></" +
				"C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Average Weighted Rate\" DataField=\"" +
				"AverageWeightedRate\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo />" +
				"</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Security ID\" DataField=\"SecId\"><" +
				"ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"\" Data" +
				"Field=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type" +
				"=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>RecordSelector{AlignImage:Cen" +
				"ter;}Style50{}Style51{}Style52{}Style53{}Caption{AlignHorz:Center;}Normal{Font:V" +
				"erdana, 8.25pt;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}St" +
				"yle18{}Style19{}Style14{AlignHorz:Near;}Style15{AlignHorz:Near;}Style16{}Style17" +
				"{}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}Style47{}Style12{}HighlightR" +
				"ow{ForeColor:HighlightText;BackColor:Highlight;}Style4{}Style7{}Style29{}Style28" +
				"{}Style27{}Style26{}Style25{}Footer{}Style23{}Style22{}Style21{AlignHorz:Near;Ba" +
				"ckColor:WhiteSmoke;}Style20{AlignHorz:Near;}Group{AlignVert:Center;Border:None,," +
				"0, 0, 0, 0;BackColor:ControlDark;}Style3{}Inactive{ForeColor:InactiveCaptionText" +
				";BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Style6{}Heading{Wrap:True;Ba" +
				"ckColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center" +
				";}Style49{AlignHorz:Near;}Style48{AlignHorz:Near;}Style24{}Style9{}Style8{}Style" +
				"1{}Style5{}Style41{}Style40{}Style43{AlignHorz:Near;}FilterBar{BackColor:SeaShel" +
				"l;}Style42{AlignHorz:Near;}Style44{}Style45{}Style46{}Style38{}Style39{}Style36{" +
				"AlignHorz:Near;}Style37{AlignHorz:Far;BackColor:White;}Style34{}Style35{}Style32" +
				"{}Style33{}Style30{AlignHorz:Near;}Style31{AlignHorz:Far;BackColor:White;}Style2" +
				"{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarSt" +
				"yle=\"Always\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHei" +
				"ght=\"17\" ExtendRightColumn=\"True\" FetchRowStyles=\"True\" FilterBar=\"True\" Marquee" +
				"Style=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScr" +
				"ollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10" +
				"\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me" +
				"=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle paren" +
				"t=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle" +
				" parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Sty" +
				"le7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRo" +
				"w\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><Se" +
				"lectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /" +
				"><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><St" +
				"yle parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><" +
				"EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=" +
				"\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><ColumnDivider>Dark" +
				"Gray,Single</ColumnDivider><Height>16</Height><DCIdx>0</DCIdx></C1DisplayColumn>" +
				"<C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style20\" /><Style parent=\"Sty" +
				"le1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Style22\" /><EditorStyle par" +
				"ent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style25\" /><Gr" +
				"oupFooterStyle parent=\"Style1\" me=\"Style24\" /><ColumnDivider>Gainsboro,Single</C" +
				"olumnDivider><Height>16</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColu" +
				"mn><HeadingStyle parent=\"Style2\" me=\"Style42\" /><Style parent=\"Style1\" me=\"Style" +
				"43\" /><FooterStyle parent=\"Style3\" me=\"Style44\" /><EditorStyle parent=\"Style5\" m" +
				"e=\"Style45\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style47\" /><GroupFooterStyle" +
				" parent=\"Style1\" me=\"Style46\" /><Visible>True</Visible><ColumnDivider>Gainsboro," +
				"None</ColumnDivider><Width>119</Width><Height>16</Height><HeaderDivider>False</H" +
				"eaderDivider><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pa" +
				"rent=\"Style2\" me=\"Style48\" /><Style parent=\"Style1\" me=\"Style49\" /><FooterStyle " +
				"parent=\"Style3\" me=\"Style50\" /><EditorStyle parent=\"Style5\" me=\"Style51\" /><Grou" +
				"pHeaderStyle parent=\"Style1\" me=\"Style53\" /><GroupFooterStyle parent=\"Style1\" me" +
				"=\"Style52\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivid" +
				"er><Height>16</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
				"gStyle parent=\"Style2\" me=\"Style30\" /><Style parent=\"Style1\" me=\"Style31\" /><Foo" +
				"terStyle parent=\"Style3\" me=\"Style32\" /><EditorStyle parent=\"Style5\" me=\"Style33" +
				"\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style35\" /><GroupFooterStyle parent=\"S" +
				"tyle1\" me=\"Style34\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Co" +
				"lumnDivider><Width>200</Width><Height>16</Height><DCIdx>2</DCIdx></C1DisplayColu" +
				"mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style36\" /><Style parent=\"" +
				"Style1\" me=\"Style37\" /><FooterStyle parent=\"Style3\" me=\"Style38\" /><EditorStyle " +
				"parent=\"Style5\" me=\"Style39\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style41\" />" +
				"<GroupFooterStyle parent=\"Style1\" me=\"Style40\" /><Visible>True</Visible><ColumnD" +
				"ivider>Gainsboro,Single</ColumnDivider><Width>200</Width><Height>16</Height><DCI" +
				"dx>3</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 706, 364</ClientR" +
				"ect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedSty" +
				"les><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style " +
				"parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style par" +
				"ent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style pare" +
				"nt=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style pare" +
				"nt=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"H" +
				"eading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style par" +
				"ent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1" +
				"</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth" +
				"><ClientArea>0, 0, 706, 364</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Styl" +
				"e28\" /><PrintPageFooterStyle parent=\"\" me=\"Style29\" /></Blob>";
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.SendToMenuItem,
																																										this.Sep2MenuItem,
																																										this.ExitMenuItem});
			// 
			// SendToMenuItem
			// 
			this.SendToMenuItem.Index = 0;
			this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.SendToClipboardMenuItem,
																																									 this.SendToExcelMenuItem,
																																									 this.SendToEmailMenuItem});
			this.SendToMenuItem.Text = "Send To";
			// 
			// SendToClipboardMenuItem
			// 
			this.SendToClipboardMenuItem.Index = 0;
			this.SendToClipboardMenuItem.Text = "Clipboard";
			this.SendToClipboardMenuItem.Click += new System.EventHandler(this.SendToClipboardMenuItem_Click);
			// 
			// SendToExcelMenuItem
			// 
			this.SendToExcelMenuItem.Index = 1;
			this.SendToExcelMenuItem.Text = "Excel";
			this.SendToExcelMenuItem.Click += new System.EventHandler(this.SendToExcelMenuItem_Click);
			// 
			// SendToEmailMenuItem
			// 
			this.SendToEmailMenuItem.Index = 2;
			this.SendToEmailMenuItem.Text = "Mail Recipient";
			this.SendToEmailMenuItem.Click += new System.EventHandler(this.SendToEmailMenuItem_Click);
			// 
			// Sep2MenuItem
			// 
			this.Sep2MenuItem.Index = 1;
			this.Sep2MenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 2;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// BookGroupCombo
			// 
			this.BookGroupCombo.AddItemSeparator = ';';
			this.BookGroupCombo.AutoCompletion = true;
			this.BookGroupCombo.AutoDropDown = true;
			this.BookGroupCombo.AutoSelect = true;
			this.BookGroupCombo.AutoSize = false;
			this.BookGroupCombo.Caption = "";
			this.BookGroupCombo.CaptionHeight = 17;
			this.BookGroupCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.BookGroupCombo.ColumnCaptionHeight = 17;
			this.BookGroupCombo.ColumnFooterHeight = 17;
			this.BookGroupCombo.ContentHeight = 14;
			this.BookGroupCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.BookGroupCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
			this.BookGroupCombo.DropDownWidth = 425;
			this.BookGroupCombo.EditorBackColor = System.Drawing.SystemColors.Window;
			this.BookGroupCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BookGroupCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.BookGroupCombo.EditorHeight = 14;
			this.BookGroupCombo.ExtendRightColumn = true;
			this.BookGroupCombo.GapHeight = 2;
			this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.BookGroupCombo.ItemHeight = 15;
			this.BookGroupCombo.KeepForeColor = true;
			this.BookGroupCombo.LimitToList = true;
			this.BookGroupCombo.Location = new System.Drawing.Point(96, 8);
			this.BookGroupCombo.MatchEntryTimeout = ((long)(2000));
			this.BookGroupCombo.MaxDropDownItems = ((short)(10));
			this.BookGroupCombo.MaxLength = 15;
			this.BookGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Arrow;
			this.BookGroupCombo.Name = "BookGroupCombo";
			this.BookGroupCombo.PartialRightColumn = false;
			this.BookGroupCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.BookGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.BookGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.BookGroupCombo.Size = new System.Drawing.Size(96, 20);
			this.BookGroupCombo.TabIndex = 28;
			this.BookGroupCombo.TextChanged += new System.EventHandler(this.Combo_TextChanged);
			this.BookGroupCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book Group\"" +
				" DataField=\"BookGroup\" DataWidth=\"100\"><ValueItems /></C1DataColumn><C1DataColum" +
				"n Level=\"0\" Caption=\"Book Group Name\" DataField=\"BookName\" DataWidth=\"350\"><Valu" +
				"eItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWra" +
				"pper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark" +
				";}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackCo" +
				"lor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive" +
				"{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignH" +
				"orz:Center;}Normal{BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackCo" +
				"lor:Highlight;}Style1{Font:Verdana, 8.25pt;}OddRow{}RecordSelector{AlignImage:Ce" +
				"nter;}Style15{AlignHorz:Near;}Heading{Wrap:True;BackColor:Control;Border:Raised," +
				",1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}St" +
				"yle14{}Style13{Font:Verdana, 8.25pt;AlignHorz:Near;}Style16{Font:Verdana, 8.25pt" +
				";AlignHorz:Near;}Style17{}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win" +
				".C1List.ListBoxView AllowColSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCapt" +
				"ionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" VerticalScrollGr" +
				"oup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><intern" +
				"alCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style pare" +
				"nt=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDi" +
				"vider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>75</Wid" +
				"th><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
				"gStyle parent=\"Style2\" me=\"Style15\" /><Style parent=\"Style1\" me=\"Style16\" /><Foo" +
				"terStyle parent=\"Style3\" me=\"Style17\" /><ColumnDivider><Color>DarkGray</Color><S" +
				"tyle>Single</Style></ColumnDivider><Width>250</Width><Height>15</Height><DCIdx>1" +
				"</DCIdx></C1DisplayColumn></internalCols><VScrollBar><Width>16</Width></VScrollB" +
				"ar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=" +
				"\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Foo" +
				"ter\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle paren" +
				"t=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /" +
				"><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=" +
				"\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><Selected" +
				"Style parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1." +
				"Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Sty" +
				"le parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style p" +
				"arent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style pa" +
				"rent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style " +
				"parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style paren" +
				"t=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedSt" +
				"yles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layou" +
				"t><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			// 
			// BorrowsRadio
			// 
			this.BorrowsRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BorrowsRadio.Checked = true;
			this.BorrowsRadio.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BorrowsRadio.Location = new System.Drawing.Point(528, 32);
			this.BorrowsRadio.Name = "BorrowsRadio";
			this.BorrowsRadio.Size = new System.Drawing.Size(80, 20);
			this.BorrowsRadio.TabIndex = 26;
			this.BorrowsRadio.TabStop = true;
			this.BorrowsRadio.Text = "Borrows";
			this.BorrowsRadio.CheckedChanged += new System.EventHandler(this.Combo_TextChanged);
			// 
			// LoansRadio
			// 
			this.LoansRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LoansRadio.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LoansRadio.Location = new System.Drawing.Point(608, 32);
			this.LoansRadio.Name = "LoansRadio";
			this.LoansRadio.Size = new System.Drawing.Size(68, 20);
			this.LoansRadio.TabIndex = 27;
			this.LoansRadio.Text = "Loans";
			this.LoansRadio.CheckedChanged += new System.EventHandler(this.Combo_TextChanged);
			// 
			// BookGroupNameLabel
			// 
			this.BookGroupNameLabel.ForeColor = System.Drawing.Color.Navy;
			this.BookGroupNameLabel.Location = new System.Drawing.Point(200, 8);
			this.BookGroupNameLabel.Name = "BookGroupNameLabel";
			this.BookGroupNameLabel.Size = new System.Drawing.Size(240, 18);
			this.BookGroupNameLabel.TabIndex = 25;
			this.BookGroupNameLabel.Tag = null;
			this.BookGroupNameLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// BookGroupLabel
			// 
			this.BookGroupLabel.Location = new System.Drawing.Point(8, 8);
			this.BookGroupLabel.Name = "BookGroupLabel";
			this.BookGroupLabel.Size = new System.Drawing.Size(80, 18);
			this.BookGroupLabel.TabIndex = 24;
			this.BookGroupLabel.Tag = null;
			this.BookGroupLabel.Text = "BookGroup:";
			this.BookGroupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BookGroupLabel.TextDetached = true;
			// 
			// BookCombo
			// 
			this.BookCombo.AddItemSeparator = ';';
			this.BookCombo.AutoCompletion = true;
			this.BookCombo.AutoDropDown = true;
			this.BookCombo.AutoSelect = true;
			this.BookCombo.AutoSize = false;
			this.BookCombo.Caption = "";
			this.BookCombo.CaptionHeight = 17;
			this.BookCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.BookCombo.ColumnCaptionHeight = 17;
			this.BookCombo.ColumnFooterHeight = 17;
			this.BookCombo.ContentHeight = 14;
			this.BookCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.BookCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
			this.BookCombo.DropDownWidth = 425;
			this.BookCombo.EditorBackColor = System.Drawing.SystemColors.Window;
			this.BookCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BookCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.BookCombo.EditorHeight = 14;
			this.BookCombo.ExtendRightColumn = true;
			this.BookCombo.GapHeight = 2;
			this.BookCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.BookCombo.ItemHeight = 15;
			this.BookCombo.KeepForeColor = true;
			this.BookCombo.LimitToList = true;
			this.BookCombo.Location = new System.Drawing.Point(96, 32);
			this.BookCombo.MatchEntryTimeout = ((long)(2000));
			this.BookCombo.MaxDropDownItems = ((short)(10));
			this.BookCombo.MaxLength = 15;
			this.BookCombo.MouseCursor = System.Windows.Forms.Cursors.Arrow;
			this.BookCombo.Name = "BookCombo";
			this.BookCombo.PartialRightColumn = false;
			this.BookCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.BookCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.BookCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.BookCombo.Size = new System.Drawing.Size(96, 20);
			this.BookCombo.TabIndex = 31;
			this.BookCombo.TextChanged += new System.EventHandler(this.Combo_TextChanged);
			this.BookCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book\" DataF" +
				"ield=\"Book\" DataWidth=\"100\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\"" +
				" Caption=\"Book Name\" DataField=\"BookName\" DataWidth=\"350\"><ValueItems /></C1Data" +
				"Column></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group" +
				"{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHo" +
				"rz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selecte" +
				"d{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:Inacti" +
				"veCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Norma" +
				"l{BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}St" +
				"yle9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Style13{Font:Verd" +
				"ana, 8.25pt;AlignHorz:Near;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1," +
				" 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Styl" +
				"e14{}Style15{AlignHorz:Near;}Style16{Font:Verdana, 8.25pt;AlignHorz:Near;}Style1" +
				"7{}Style1{Font:Verdana, 8.25pt;}</Data></Styles><Splits><C1.Win.C1List.ListBoxVi" +
				"ew AllowColSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" Co" +
				"lumnFooterHeight=\"17\" ExtendRightColumn=\"True\" VerticalScrollGroup=\"1\" Horizonta" +
				"lScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1Display" +
				"Column><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"S" +
				"tyle13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>Dark" +
				"Gray</Color><Style>Single</Style></ColumnDivider><Width>75</Width><Height>15</He" +
				"ight><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
				"yle2\" me=\"Style15\" /><Style parent=\"Style1\" me=\"Style16\" /><FooterStyle parent=\"" +
				"Style3\" me=\"Style17\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Styl" +
				"e></ColumnDivider><Width>250</Width><Height>15</Height><DCIdx>1</DCIdx></C1Displ" +
				"ayColumn></internalCols><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><H" +
				"eight>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenR" +
				"owStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" " +
				"/><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"S" +
				"tyle2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle p" +
				"arent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><Recor" +
				"dSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Sel" +
				"ected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBo" +
				"xView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal" +
				"\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" m" +
				"e=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=" +
				"\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" m" +
				"e=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"R" +
				"ecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>" +
				"1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelW" +
				"idth>16</DefaultRecSelWidth></Blob>";
			// 
			// BookNameLabel
			// 
			this.BookNameLabel.ForeColor = System.Drawing.Color.Navy;
			this.BookNameLabel.Location = new System.Drawing.Point(200, 32);
			this.BookNameLabel.Name = "BookNameLabel";
			this.BookNameLabel.Size = new System.Drawing.Size(240, 18);
			this.BookNameLabel.TabIndex = 30;
			this.BookNameLabel.Tag = null;
			this.BookNameLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// BookLabel
			// 
			this.BookLabel.Location = new System.Drawing.Point(8, 32);
			this.BookLabel.Name = "BookLabel";
			this.BookLabel.Size = new System.Drawing.Size(80, 18);
			this.BookLabel.TabIndex = 29;
			this.BookLabel.Tag = null;
			this.BookLabel.Text = "Book:";
			this.BookLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BookLabel.TextDetached = true;
			// 
			// DateEditor
			// 
			this.DateEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DateEditor.AutoSize = false;
			// 
			// DateEditor.Calendar
			// 
			this.DateEditor.Calendar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DateEditor.CustomFormat = "yyyy-MM-dd";
			this.DateEditor.DateTimeInput = false;
			this.DateEditor.DisplayFormat.CustomFormat = "yyyy-MM-dd";
			this.DateEditor.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.DateEditor.DisplayFormat.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
			this.DateEditor.DisplayFormat.TrimStart = true;
			this.DateEditor.DropDownFormAlign = C1.Win.C1Input.DropDownFormAlignmentEnum.Right;
			this.DateEditor.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.DateEditor.EditFormat.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat;
			this.DateEditor.ErrorInfo.BeepOnError = true;
			this.DateEditor.ErrorInfo.CanLoseFocus = true;
			this.DateEditor.ErrorInfo.ShowErrorMessage = false;
			this.DateEditor.Location = new System.Drawing.Point(608, 8);
			this.DateEditor.Name = "DateEditor";
			this.DateEditor.Size = new System.Drawing.Size(96, 20);
			this.DateEditor.TabIndex = 33;
			this.DateEditor.Tag = null;
			this.DateEditor.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.DateEditor.TextChanged += new System.EventHandler(this.Combo_TextChanged);
			this.DateEditor.ValueChanged += new System.EventHandler(this.Combo_TextChanged);
			// 
			// EffectDateLabel
			// 
			this.EffectDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EffectDateLabel.Location = new System.Drawing.Point(520, 8);
			this.EffectDateLabel.Name = "EffectDateLabel";
			this.EffectDateLabel.Size = new System.Drawing.Size(80, 16);
			this.EffectDateLabel.TabIndex = 32;
			this.EffectDateLabel.Tag = null;
			this.EffectDateLabel.Text = "In Effect For:";
			this.EffectDateLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			this.EffectDateLabel.TextDetached = true;
			// 
			// PositionContractRateComparisonForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(712, 429);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.DateEditor);
			this.Controls.Add(this.EffectDateLabel);
			this.Controls.Add(this.BookCombo);
			this.Controls.Add(this.BookNameLabel);
			this.Controls.Add(this.BookLabel);
			this.Controls.Add(this.BookGroupCombo);
			this.Controls.Add(this.BorrowsRadio);
			this.Controls.Add(this.LoansRadio);
			this.Controls.Add(this.BookGroupNameLabel);
			this.Controls.Add(this.BookGroupLabel);
			this.Controls.Add(this.PositionContractRateComparisonGrid);
			this.DockPadding.Bottom = 1;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 60;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PositionContractRateComparisonForm";
			this.Text = "Position - Contract Rate Comparison";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.PositionContractRateComparisonForm_Closing);
			this.Load += new System.EventHandler(this.PositionContractRateComparisonForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.PositionContractRateComparisonGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookNameLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DateEditor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void PositionContractRateComparisonForm_Load(object sender, System.EventArgs e)
		{
			int height = mainForm.Height / 2;
			int width  = mainForm.Width / 2;
      
			try
			{
				this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
				this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
				this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
				this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));
        				
				dataSet = mainForm.PositionAgent.ContractRateComparisonDataGet(mainForm.UserId, "PositionContractRateComparison");																		 
				
				BookGroupCombo.HoldFields();
				BookGroupCombo.DataSource = dataSet;
				BookGroupCombo.DataMember = "BookGroups";
		   
				BookGroupNameLabel.DataSource = dataSet.Tables["BookGroups"];
				BookGroupNameLabel.DataField = "BookName";               
									
				bookView = new DataView(dataSet.Tables["Books"], bookViewFilter, "", DataViewRowState.CurrentRows);

				ratesView  = new DataView();
			
				BookCombo.HoldFields();
				BookCombo.DataSource = bookView;			
				
				BookNameLabel.DataSource = bookView;
				BookNameLabel.DataField = "BookName";               
									
				BookGroupCombo.Text = RegistryValue.Read(this.Name,  "BookGroup", "0234");    
				BookCombo.Text = RegistryValue.Read(this.Name,  "Book", "7369");    
		
				DateEditor.Text = mainForm.ServiceAgent.ContractsBizDate();							
			}
			catch(Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);        
			}
		}

		private void PositionContractRateComparisonForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
				RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
				RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
				RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
			}

			RegistryValue.Write(this.Name,  "BookGroup",  BookGroupCombo.Text);    
			RegistryValue.Write(this.Name,  "Book",  BookCombo.Text);    
		}
   

		private void FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "BookAverageWeightedRate":
				case "AverageWeightedRate":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("00.000");
					}
					catch {}
					break;
			}	
		}
		
		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionContractRateComparisonGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\r\n";

			foreach (int row in PositionContractRateComparisonGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionContractRateComparisonGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\r\n";
			}

			Clipboard.SetDataObject(gridData, true);

			mainForm.Alert("Total: " + PositionContractRateComparisonGrid.SelectedRows.Count + " items copied to the clipboard.", PilotState.Normal);
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n";

			if (PositionContractRateComparisonGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows to copy.", PilotState.Normal);
				return;
			}

			try
			{
				maxTextLength = new int[PositionContractRateComparisonGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionContractRateComparisonGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in PositionContractRateComparisonGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionContractRateComparisonGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionContractRateComparisonGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionContractRateComparisonGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in PositionContractRateComparisonGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionContractRateComparisonGrid.SelectedCols)
					{
						if (dataColumn.Value.GetType().Equals(typeof(System.String)))
						{
							gridData += dataColumn.CellText(rowIndex).PadRight(maxTextLength[++columnIndex] + 2);
						}
						else
						{
							gridData += dataColumn.CellText(rowIndex).PadLeft(maxTextLength[++columnIndex]) + "  ";
						}
					}
  
					gridData += "\n";
				}
                
				Email email = new Email();
				email.Send(gridData);

				mainForm.Alert("Total: " + PositionContractRateComparisonGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
			}
			catch (Exception error)
			{       
				mainForm.Alert(error.Message, PilotState.Normal);
			}
		}
		
		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			Excel excel = new Excel();	
			excel.ExportGridToExcel(ref PositionContractRateComparisonGrid);

			this.Cursor = Cursors.Default;
		}

		private void Combo_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				int row = -1;

				foreach(DataRow dataRow in dataSet.Tables["BookGroups"].Rows)
				{
					row ++;

					if(dataRow["BookGroup"].ToString().Equals(BookGroupCombo.Text))
					{
						break;
					}        
				}

				if (bool.Parse(dataSet.Tables["BookGroups"].Rows[row]["MayView"].ToString()))
				{
					ratesViewFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
					ratesView.RowFilter = ratesViewFilter;
					
					bookViewFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
					bookView.RowFilter = bookViewFilter;						
				}
				else
				{
					ratesViewFilter = "BookGroup = ''";
					ratesView.RowFilter = ratesViewFilter;
					
					bookViewFilter = "BookGroup = ''";
					bookView.RowFilter = bookViewFilter;											
				
					mainForm.Alert("UserId: " + mainForm.UserId + "  VIEW denied.", PilotState.Normal);
				}

				if (!(BookCombo.Text.Equals("")) &&
					!(BookGroupCombo.Text.Equals("")) &&
					!(DateEditor.Text.Equals("")))
				{
					mainForm.Alert("Loading contract rates for " + ((BorrowsRadio.Checked)?"Borrows...":"Loans..."), PilotState.Normal);
					this.Cursor = Cursors.WaitCursor;

					dataSet2 = mainForm.PositionAgent.ContractRateComparisonGet(
						DateEditor.Text,
						BookGroupCombo.Text,
						BookCombo.Text,
						(BorrowsRadio.Checked)?"B":"L");
			
					ratesView.RowStateFilter = DataViewRowState.CurrentRows;
					ratesView.Table = dataSet2.Tables["RateComparison"];
					ratesView.RowFilter = ratesViewFilter;

					PositionContractRateComparisonGrid.SetDataBinding(ratesView, "", true);

					mainForm.Alert("Loading contract rates for " + ((BorrowsRadio.Checked)?"Borrows...Done!":"Loans...Done!"), PilotState.Normal);
					this.Cursor = Cursors.Default;
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void Radio_CheckedChanged(object sender, System.EventArgs e)
		{
			bookViewFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
			bookView.RowFilter = bookViewFilter;

			if (!(BookCombo.Text.Equals("")) &&
				!(BookGroupCombo.Text.Equals("")) &&
				!(DateEditor.Text.Equals("")))
			{
				dataSet2 = mainForm.PositionAgent.ContractRateComparisonGet(
					DateEditor.Text,
					BookGroupCombo.Text,
					BookCombo.Text,
					(BorrowsRadio.Checked)?"B":"L");
			
				PositionContractRateComparisonGrid.SetDataBinding(dataSet2, "RateComparison", true);
			}
		}

		private void PositionContractRateComparisonGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (!secId.Equals(PositionContractRateComparisonGrid.Columns["SecId"].Text))
			{			
				secId = PositionContractRateComparisonGrid.Columns["SecId"].Text;
				mainForm.SecId = secId;
			}
		}
	}
}
