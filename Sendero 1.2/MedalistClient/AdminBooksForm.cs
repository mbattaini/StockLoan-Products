// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.Remoting;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class AdminBooksForm : System.Windows.Forms.Form
  {
    bool mayEdit = false;
    
    private MainForm mainForm;
    
    private DataSet dataSet;
    private DataView bookGroupDataView, bookParentsDataView, bookDataView;

    private C1.Win.C1Input.C1Label BookGroupLabel;
    private C1.Win.C1Input.C1Label BookGroupNameLabel;
    
    private C1.Win.C1List.C1Combo BookGroupCombo;

    private System.Windows.Forms.Label BookNameLabel;
    private System.Windows.Forms.Label BookLabel;
    
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid BookParentGrid;
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid BookGrid;
    
    private System.Windows.Forms.Label Address1Label;
    private System.Windows.Forms.Label Address2Label;
    private System.Windows.Forms.Label Address3Label;
    
    private System.Windows.Forms.Label DtcDeliverLabel;
    private System.Windows.Forms.Label DtcMarkLabel;
    
    private System.Windows.Forms.TextBox DtcDeliverTextBox;
    private System.Windows.Forms.TextBox DtcMarkTextBox;
    private System.Windows.Forms.TextBox AddressLine1TextBox;
    private System.Windows.Forms.TextBox AddressLine2TextBox;
    private System.Windows.Forms.TextBox AddressLine3TextBox;
    private System.Windows.Forms.RadioButton YesterdayRadioButton;
    private System.Windows.Forms.RadioButton NowTodayRadioButton;
    private C1.Win.C1TrueDBGrid.C1TrueDBDropdown DesksDropDown;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.Windows.Forms.MenuItem Sep1;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToMailRecipientMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
    
    private System.ComponentModel.Container components = null;

    public AdminBooksForm(MainForm mainForm)
    {
      this.mainForm = mainForm;

      InitializeComponent();
    }

    protected override void Dispose( bool disposing )
    {
      if(disposing)
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AdminBooksForm));
		this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
		this.BookGroupLabel = new C1.Win.C1Input.C1Label();
		this.BookGroupCombo = new C1.Win.C1List.C1Combo();
		this.BookParentGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
		this.MainContextMenu = new System.Windows.Forms.ContextMenu();
		this.SendToMenuItem = new System.Windows.Forms.MenuItem();
		this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
		this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
		this.SendToMailRecipientMenuItem = new System.Windows.Forms.MenuItem();
		this.Sep1 = new System.Windows.Forms.MenuItem();
		this.ExitMenuItem = new System.Windows.Forms.MenuItem();
		this.BookGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
		this.BookNameLabel = new System.Windows.Forms.Label();
		this.DesksDropDown = new C1.Win.C1TrueDBGrid.C1TrueDBDropdown();
		this.BookLabel = new System.Windows.Forms.Label();
		this.Address1Label = new System.Windows.Forms.Label();
		this.Address2Label = new System.Windows.Forms.Label();
		this.Address3Label = new System.Windows.Forms.Label();
		this.AddressLine1TextBox = new System.Windows.Forms.TextBox();
		this.AddressLine2TextBox = new System.Windows.Forms.TextBox();
		this.AddressLine3TextBox = new System.Windows.Forms.TextBox();
		this.DtcDeliverLabel = new System.Windows.Forms.Label();
		this.DtcMarkLabel = new System.Windows.Forms.Label();
		this.DtcDeliverTextBox = new System.Windows.Forms.TextBox();
		this.DtcMarkTextBox = new System.Windows.Forms.TextBox();
		this.YesterdayRadioButton = new System.Windows.Forms.RadioButton();
		this.NowTodayRadioButton = new System.Windows.Forms.RadioButton();
		((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.BookParentGrid)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.BookGrid)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.DesksDropDown)).BeginInit();
		this.SuspendLayout();
		// 
		// BookGroupNameLabel
		// 
		this.BookGroupNameLabel.ForeColor = System.Drawing.Color.Navy;
		this.BookGroupNameLabel.Location = new System.Drawing.Point(240, 6);
		this.BookGroupNameLabel.Name = "BookGroupNameLabel";
		this.BookGroupNameLabel.Size = new System.Drawing.Size(300, 18);
		this.BookGroupNameLabel.TabIndex = 8;
		this.BookGroupNameLabel.Tag = null;
		this.BookGroupNameLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
		// 
		// BookGroupLabel
		// 
		this.BookGroupLabel.Location = new System.Drawing.Point(52, 6);
		this.BookGroupLabel.Name = "BookGroupLabel";
		this.BookGroupLabel.Size = new System.Drawing.Size(80, 18);
		this.BookGroupLabel.TabIndex = 7;
		this.BookGroupLabel.Tag = null;
		this.BookGroupLabel.Text = "Book Group:";
		this.BookGroupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.BookGroupLabel.TextDetached = true;
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
		this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
		this.BookGroupCombo.ItemHeight = 15;
		this.BookGroupCombo.KeepForeColor = true;
		this.BookGroupCombo.LimitToList = true;
		this.BookGroupCombo.Location = new System.Drawing.Point(136, 6);
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
		this.BookGroupCombo.TabIndex = 6;
		this.BookGroupCombo.RowChange += new System.EventHandler(this.BookGroupCombo_RowChange);
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
			"vider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>80</Wid" +
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
			"t><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
		// 
		// BookParentGrid
		// 
		this.BookParentGrid.AllowColMove = false;
		this.BookParentGrid.AllowColSelect = false;
		this.BookParentGrid.AllowUpdate = false;
		this.BookParentGrid.CaptionHeight = 17;
		this.BookParentGrid.ColumnFooters = true;
		this.BookParentGrid.ContextMenu = this.MainContextMenu;
		this.BookParentGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.BookParentGrid.EmptyRows = true;
		this.BookParentGrid.ExtendRightColumn = true;
		this.BookParentGrid.FetchRowStyles = true;
		this.BookParentGrid.GroupByCaption = "Drag a column header here to group by that column";
		this.BookParentGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
		this.BookParentGrid.Location = new System.Drawing.Point(1, 50);
		this.BookParentGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
		this.BookParentGrid.Name = "BookParentGrid";
		this.BookParentGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
		this.BookParentGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
		this.BookParentGrid.PreviewInfo.ZoomFactor = 75;
		this.BookParentGrid.RecordSelectorWidth = 17;
		this.BookParentGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
		this.BookParentGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
		this.BookParentGrid.RowHeight = 15;
		this.BookParentGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
		this.BookParentGrid.Size = new System.Drawing.Size(1218, 379);
		this.BookParentGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
		this.BookParentGrid.TabIndex = 9;
		this.BookParentGrid.Text = "BookParentGrid";
		this.BookParentGrid.WrapCellPointer = true;
		this.BookParentGrid.Resize += new System.EventHandler(this.BookParentGrid_Resize);
		this.BookParentGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.BookParentGrid_RowColChange);
		this.BookParentGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.BookParentGrid_FetchRowStyle);
		this.BookParentGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BookParentGrid_MouseDown);
		this.BookParentGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.BookParentGrid_BeforeUpdate);
		this.BookParentGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.BookParentGrid_FormatText);
		this.BookParentGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BookParentGrid_KeyPress);
		this.BookParentGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book Parent" +
			"\" DataField=\"BookParent\"><ValueItems Translate=\"True\" /><GroupInfo /></C1DataCol" +
			"umn><C1DataColumn Level=\"0\" Caption=\"Borrow Limit\" DataField=\"AmountLimitBorrow\"" +
			" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1Da" +
			"taColumn Level=\"0\" Caption=\"Loan Limit\" DataField=\"AmountLimitLoan\" NumberFormat" +
			"=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
			"l=\"0\" Caption=\"Total Limit\" DataField=\"AmountLimitTotal\" NumberFormat=\"FormatTex" +
			"t Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capti" +
			"on=\"Borrows COB\" DataField=\"AmountBorrowPriorDay\" NumberFormat=\"FormatText Event" +
			"\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Loa" +
			"ns COB\" DataField=\"AmountLoanPriorDay\" NumberFormat=\"FormatText Event\"><ValueIte" +
			"ms /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Total COB\" Dat" +
			"aField=\"AmountTotalPriorDay\" NumberFormat=\"FormatText Event\"><ValueItems /><Grou" +
			"pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Borrows Today\" DataField" +
			"=\"AmountBorrowToday\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo />" +
			"</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Loans Today\" DataField=\"AmountLo" +
			"anToday\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColu" +
			"mn><C1DataColumn Level=\"0\" Caption=\"Total Today\" DataField=\"AmountTotalToday\" Nu" +
			"mberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
			"olumn Level=\"0\" Caption=\"Actor\" DataField=\"ActUserShortName\"><ValueItems /><Grou" +
			"pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataField=\"Act" +
			"Time\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn>" +
			"<C1DataColumn Level=\"0\" Caption=\"Comment\" DataField=\"Comment\"><ValueItems /><Gro" +
			"upInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Ratio\" DataField=\"Ratio" +
			"BorrowPriorDay\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1D" +
			"ataColumn><C1DataColumn Level=\"0\" Caption=\"Ratio\" DataField=\"RatioLoanPriorDay\" " +
			"NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
			"aColumn Level=\"0\" Caption=\"Ratio\" DataField=\"RatioTotalPriorDay\" NumberFormat=\"F" +
			"ormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"" +
			"0\" Caption=\"Ratio\" DataField=\"RatioBorrowToday\" NumberFormat=\"FormatText Event\">" +
			"<ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Ratio" +
			"\" DataField=\"RatioLoanToday\" NumberFormat=\"FormatText Event\"><ValueItems /><Grou" +
			"pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Ratio\" DataField=\"RatioT" +
			"otalToday\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataCo" +
			"lumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>H" +
			"ighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Inactive{ForeColor:Inac" +
			"tiveCaptionText;BackColor:InactiveCaption;}Style119{Font:Verdana, 8.25pt;AlignHo" +
			"rz:Far;BackColor:Honeydew;}Style118{AlignHorz:Near;}Style78{BackColor:WhiteSmoke" +
			";}Style79{}Style85{}Editor{}Style117{}Style116{}Style72{BackColor:WhiteSmoke;}St" +
			"yle73{}Style70{AlignHorz:Near;}Style71{Font:Verdana, 8.25pt;AlignHorz:Near;BackC" +
			"olor:WhiteSmoke;}Style76{AlignHorz:Near;}Style77{Font:Verdana, 8.25pt;AlignHorz:" +
			"Near;BackColor:WhiteSmoke;}Style74{}Style75{}Style84{BackColor:Ivory;}Style87{}S" +
			"tyle86{}Style81{}Style80{}Style83{Font:Verdana, 8.25pt, style=Bold;AlignHorz:Far" +
			";ForeColor:Navy;BackColor:Ivory;}Style82{AlignHorz:Near;}Footer{}Style89{Font:Ve" +
			"rdana, 8.25pt, style=Bold;AlignHorz:Far;ForeColor:Navy;BackColor:Honeydew;}Style" +
			"88{AlignHorz:Near;}Style108{BackColor:AliceBlue;}Style109{}Style104{}Style105{}S" +
			"tyle106{AlignHorz:Near;}Style107{Font:Verdana, 8.25pt;AlignHorz:Far;BackColor:Al" +
			"iceBlue;}Style100{AlignHorz:Near;}Style101{Font:Verdana, 8.25pt;AlignHorz:Far;Ba" +
			"ckColor:Honeydew;}Style102{BackColor:Honeydew;}Style103{}Style94{AlignHorz:Near;" +
			"}Style95{Font:Verdana, 8.25pt;AlignHorz:Near;BackColor:WhiteSmoke;}Style96{BackC" +
			"olor:WhiteSmoke;}Style97{}Style90{BackColor:Honeydew;}Style91{}Style92{}Style93{" +
			"}RecordSelector{AlignImage:Center;}Style98{}Style99{}Heading{Wrap:True;AlignVert" +
			":Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style" +
			"18{BackColor:WhiteSmoke;}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}Styl" +
			"e17{Font:Verdana, 8.25pt;AlignHorz:Near;BackColor:WhiteSmoke;}Style10{AlignHorz:" +
			"Near;}Style11{}Style12{}Style13{}Style128{}Style129{}Style126{BackColor:AliceBlu" +
			"e;}Style127{}Style124{AlignHorz:Near;}Style122{}Style120{BackColor:Honeydew;}Sty" +
			"le29{Font:Verdana, 8.25pt;AlignHorz:Far;BackColor:AliceBlue;}Style9{}Style8{}Sty" +
			"le28{AlignHorz:Near;}Style27{}Style26{}Style125{Font:Verdana, 8.25pt;AlignHorz:F" +
			"ar;BackColor:AliceBlue;}Style25{}Style123{}Style24{BackColor:Ivory;}Style121{}St" +
			"yle23{Font:Verdana, 8.25pt;AlignHorz:Far;BackColor:Ivory;}Style22{AlignHorz:Near" +
			";}Style21{}Style20{}OddRow{}Style38{}Style39{}Style36{BackColor:Ivory;}FilterBar" +
			"{}Style37{}Style34{AlignHorz:Near;}Style35{Font:Verdana, 8.25pt;AlignHorz:Far;Ba" +
			"ckColor:Ivory;}Style32{}Style33{}Style49{}Style48{BackColor:AliceBlue;}Style30{B" +
			"ackColor:AliceBlue;}Style31{}Normal{Font:Verdana, 8.25pt;Padding:0, 0, 0, 0;}Sty" +
			"le41{Font:Verdana, 8.25pt;AlignHorz:Far;BackColor:Honeydew;}Style40{AlignHorz:Ne" +
			"ar;}Style43{}Style42{BackColor:Honeydew;}Style45{}Style44{}Style47{Font:Verdana," +
			" 8.25pt;AlignHorz:Far;BackColor:AliceBlue;}Style46{AlignHorz:Near;}Selected{Fore" +
			"Color:HighlightText;BackColor:Highlight;}EvenRow{BackColor:Aqua;}Style5{}Style4{" +
			"}Style7{}Style6{}Style58{AlignHorz:Near;}Style59{Font:Verdana, 8.25pt;AlignHorz:" +
			"Far;BackColor:Honeydew;}Style3{}Style2{}Style50{}Style51{}Style52{AlignHorz:Near" +
			";}Style53{Font:Verdana, 8.25pt;AlignHorz:Far;BackColor:Ivory;}Style54{BackColor:" +
			"Ivory;}Style55{}Style56{}Style57{}Caption{AlignHorz:Center;}Style64{AlignHorz:Ne" +
			"ar;}Style112{AlignHorz:Near;}Style69{}Style68{}Group{BackColor:ControlDark;Borde" +
			"r:None,,0, 0, 0, 0;AlignVert:Center;}Style1{}Style63{}Style62{}Style61{}Style60{" +
			"BackColor:Honeydew;}Style67{}Style66{BackColor:AliceBlue;}Style65{Font:Verdana, " +
			"8.25pt;AlignHorz:Far;BackColor:AliceBlue;}Style115{}Style114{BackColor:Ivory;}St" +
			"yle111{}Style110{}Style113{Font:Verdana, 8.25pt;AlignHorz:Far;BackColor:Ivory;}<" +
			"/Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView VBarStyle=\"Always\" AllowCo" +
			"lMove=\"False\" AllowColSelect=\"False\" Name=\"Split[0,1]\" CaptionHeight=\"18\" Column" +
			"CaptionHeight=\"18\" ColumnFooterHeight=\"18\" ExtendRightColumn=\"True\" FetchRowStyl" +
			"es=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"17\" DefRecSelWidth" +
			"=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"St" +
			"yle2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle pa" +
			"rent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><" +
			"FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12" +
			"\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"High" +
			"lightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowSt" +
			"yle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" m" +
			"e=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Norm" +
			"al\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
			"e=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\"" +
			" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle pa" +
			"rent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><V" +
			"isible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>85</Wi" +
			"dth><Height>15</Height><Locked>True</Locked><DCIdx>0</DCIdx></C1DisplayColumn><C" +
			"1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style82\" /><Style parent=\"Style" +
			"1\" me=\"Style83\" /><FooterStyle parent=\"Style3\" me=\"Style84\" /><EditorStyle paren" +
			"t=\"Style5\" me=\"Style85\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style87\" /><Grou" +
			"pFooterStyle parent=\"Style1\" me=\"Style86\" /><Visible>True</Visible><ColumnDivide" +
			"r>DarkGray,Single</ColumnDivider><Width>110</Width><Height>15</Height><DCIdx>1</" +
			"DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style" +
			"34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Sty" +
			"le36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"St" +
			"yle1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>T" +
			"rue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><L" +
			"ocked>True</Locked><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSt" +
			"yle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><Footer" +
			"Style parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /" +
			"><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Styl" +
			"e1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Column" +
			"Divider><Width>50</Width><Height>15</Height><DCIdx>13</DCIdx></C1DisplayColumn><" +
			"C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Styl" +
			"e1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle pare" +
			"nt=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><Gro" +
			"upFooterStyle parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><ColumnDivid" +
			"er>DarkGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx" +
			">7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
			"tyle112\" /><Style parent=\"Style1\" me=\"Style113\" /><FooterStyle parent=\"Style3\" m" +
			"e=\"Style114\" /><EditorStyle parent=\"Style5\" me=\"Style115\" /><GroupHeaderStyle pa" +
			"rent=\"Style1\" me=\"Style117\" /><GroupFooterStyle parent=\"Style1\" me=\"Style116\" />" +
			"<Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>50</" +
			"Width><Height>15</Height><DCIdx>16</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
			"adingStyle parent=\"Style2\" me=\"Style88\" /><Style parent=\"Style1\" me=\"Style89\" />" +
			"<FooterStyle parent=\"Style3\" me=\"Style90\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
			"le91\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style93\" /><GroupFooterStyle paren" +
			"t=\"Style1\" me=\"Style92\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single<" +
			"/ColumnDivider><Width>110</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayC" +
			"olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style paren" +
			"t=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorSty" +
			"le parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\"" +
			" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><Colu" +
			"mnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked" +
			"><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
			"\" me=\"Style100\" /><Style parent=\"Style1\" me=\"Style101\" /><FooterStyle parent=\"St" +
			"yle3\" me=\"Style102\" /><EditorStyle parent=\"Style5\" me=\"Style103\" /><GroupHeaderS" +
			"tyle parent=\"Style1\" me=\"Style105\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
			"104\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
			"th>50</Width><Height>15</Height><DCIdx>14</DCIdx></C1DisplayColumn><C1DisplayCol" +
			"umn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Styl" +
			"e59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" " +
			"me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyl" +
			"e parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider>DarkGray," +
			"Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx>8</DCIdx></" +
			"C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style118\" /><" +
			"Style parent=\"Style1\" me=\"Style119\" /><FooterStyle parent=\"Style3\" me=\"Style120\"" +
			" /><EditorStyle parent=\"Style5\" me=\"Style121\" /><GroupHeaderStyle parent=\"Style1" +
			"\" me=\"Style123\" /><GroupFooterStyle parent=\"Style1\" me=\"Style122\" /><Visible>Tru" +
			"e</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>50</Width><Heigh" +
			"t>15</Height><DCIdx>17</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
			"arent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle" +
			" parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><Gro" +
			"upHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" m" +
			"e=\"Style32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivid" +
			"er><Height>15</Height><Locked>True</Locked><DCIdx>3</DCIdx></C1DisplayColumn><C1" +
			"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1" +
			"\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent" +
			"=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><Group" +
			"FooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider" +
			">DarkGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx>6" +
			"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
			"le106\" /><Style parent=\"Style1\" me=\"Style107\" /><FooterStyle parent=\"Style3\" me=" +
			"\"Style108\" /><EditorStyle parent=\"Style5\" me=\"Style109\" /><GroupHeaderStyle pare" +
			"nt=\"Style1\" me=\"Style111\" /><GroupFooterStyle parent=\"Style1\" me=\"Style110\" /><V" +
			"isible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>50</Wi" +
			"dth><Height>15</Height><FetchStyle>True</FetchStyle><DCIdx>15</DCIdx></C1Display" +
			"Column><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style pare" +
			"nt=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorSt" +
			"yle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69" +
			"\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visible><Col" +
			"umnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locke" +
			"d><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
			"2\" me=\"Style124\" /><Style parent=\"Style1\" me=\"Style125\" /><FooterStyle parent=\"S" +
			"tyle3\" me=\"Style126\" /><EditorStyle parent=\"Style5\" me=\"Style127\" /><GroupHeader" +
			"Style parent=\"Style1\" me=\"Style129\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
			"e128\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wi" +
			"dth>50</Width><Height>15</Height><DCIdx>18</DCIdx></C1DisplayColumn><C1DisplayCo" +
			"lumn><HeadingStyle parent=\"Style2\" me=\"Style70\" /><Style parent=\"Style1\" me=\"Sty" +
			"le71\" /><FooterStyle parent=\"Style3\" me=\"Style72\" /><EditorStyle parent=\"Style5\"" +
			" me=\"Style73\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style75\" /><GroupFooterSty" +
			"le parent=\"Style1\" me=\"Style74\" /><Visible>True</Visible><ColumnDivider>DarkGray" +
			",Single</ColumnDivider><Width>75</Width><Height>15</Height><Locked>True</Locked>" +
			"<DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
			"\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterStyle parent=\"Styl" +
			"e3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><GroupHeaderStyle" +
			" parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=\"Style80\" /" +
			"><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>130" +
			"</Width><Height>15</Height><Locked>True</Locked><DCIdx>11</DCIdx></C1DisplayColu" +
			"mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style94\" /><Style parent=\"" +
			"Style1\" me=\"Style95\" /><FooterStyle parent=\"Style3\" me=\"Style96\" /><EditorStyle " +
			"parent=\"Style5\" me=\"Style97\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style99\" />" +
			"<GroupFooterStyle parent=\"Style1\" me=\"Style98\" /><Visible>True</Visible><ColumnD" +
			"ivider>DarkGray,Single</ColumnDivider><Width>75</Width><Height>15</Height><DCIdx" +
			">12</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 1214, 375</ClientR" +
			"ect><BorderSide>Left</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><Named" +
			"Styles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Sty" +
			"le parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style " +
			"parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style p" +
			"arent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style p" +
			"arent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent" +
			"=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style " +
			"parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplit" +
			"s>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWi" +
			"dth><ClientArea>0, 0, 1214, 375</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"" +
			"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
		// 
		// MainContextMenu
		// 
		this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.SendToMenuItem,
																						this.Sep1,
																						this.ExitMenuItem});
		// 
		// SendToMenuItem
		// 
		this.SendToMenuItem.Index = 0;
		this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.SendToClipboardMenuItem,
																					   this.SendToExcelMenuItem,
																					   this.SendToMailRecipientMenuItem});
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
		// SendToMailRecipientMenuItem
		// 
		this.SendToMailRecipientMenuItem.Index = 2;
		this.SendToMailRecipientMenuItem.Text = "Mail Recipient";
		this.SendToMailRecipientMenuItem.Click += new System.EventHandler(this.SendToMailRecipientMenuItem_Click);
		// 
		// Sep1
		// 
		this.Sep1.Index = 1;
		this.Sep1.Text = "-";
		// 
		// ExitMenuItem
		// 
		this.ExitMenuItem.Index = 2;
		this.ExitMenuItem.Text = "Exit";
		this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
		// 
		// BookGrid
		// 
		this.BookGrid.AllowUpdate = false;
		this.BookGrid.CaptionHeight = 17;
		this.BookGrid.EmptyRows = true;
		this.BookGrid.ExtendRightColumn = true;
		this.BookGrid.GroupByCaption = "Drag a column header here to group by that column";
		this.BookGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
		this.BookGrid.Location = new System.Drawing.Point(28, 80);
		this.BookGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
		this.BookGrid.Name = "BookGrid";
		this.BookGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
		this.BookGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
		this.BookGrid.PreviewInfo.ZoomFactor = 75;
		this.BookGrid.RecordSelectorWidth = 17;
		this.BookGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
		this.BookGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
		this.BookGrid.RowHeight = 15;
		this.BookGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
		this.BookGrid.Size = new System.Drawing.Size(944, 69);
		this.BookGrid.TabIndex = 27;
		this.BookGrid.TabStop = false;
		this.BookGrid.Text = "BookGrid";
		this.BookGrid.Visible = false;
		this.BookGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BookGrid_MouseDown);
		this.BookGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.BookGrid_BeforeUpdate);
		this.BookGrid.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.BookGrid_AfterColUpdate);
		this.BookGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.BookGrid_FormatText);
		this.BookGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BookGrid_KeyPress);
		this.BookGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Desk\" DataF" +
			"ield=\"Desk\" DropDownCtl=\"DesksDropDown\"><ValueItems Translate=\"True\" /><GroupInf" +
			"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"FaxNumber\" DataField=\"FaxNum" +
			"ber\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"" +
			"Book Name\" DataField=\"BookName\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
			"aColumn Level=\"0\" Caption=\"Book\" DataField=\"Book\"><ValueItems /><GroupInfo /></C" +
			"1DataColumn><C1DataColumn Level=\"0\" Caption=\"Actor\" DataField=\"ActUserShortName\"" +
			"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act " +
			"Time\" DataField=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupIn" +
			"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Comment\" DataField=\"Comment" +
			"\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Fir" +
			"m\" DataField=\"Firm\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
			"l=\"0\" Caption=\"Country\" DataField=\"Country\"><ValueItems /><GroupInfo /></C1DataC" +
			"olumn><C1DataColumn Level=\"0\" Caption=\"DeskType\" DataField=\"DeskType\"><ValueItem" +
			"s /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Des" +
			"ign.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlig" +
			"ht;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Selected{F" +
			"oreColor:HighlightText;BackColor:Highlight;}Editor{}Style72{}Style73{}Style70{Al" +
			"ignHorz:Near;}Style71{AlignHorz:Near;}Style74{}Style75{}FilterBar{}Heading{Wrap:" +
			"True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert" +
			":Center;}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}Style17{Ali" +
			"gnHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style27{}Style29" +
			"{AlignHorz:Near;}Style28{AlignHorz:Near;}Style26{}Style25{}Style9{}Style8{}Style" +
			"24{}Style23{AlignHorz:Near;}Style5{}Style4{}Style7{}Style6{}Style1{}Style22{Alig" +
			"nHorz:Near;}Style3{}Style2{}Style21{}Style20{}OddRow{}Style38{}Style39{}Style36{" +
			"}Style37{}Style34{AlignHorz:Near;}Style35{AlignHorz:Near;}Style32{}Style33{}Styl" +
			"e30{}Style49{}Style48{}Style31{}Normal{Font:Verdana, 8.25pt;}Style41{AlignHorz:N" +
			"ear;}Style40{AlignHorz:Near;}Style43{}Style42{}Style45{}Style44{}Style47{AlignHo" +
			"rz:Near;}Style46{AlignHorz:Near;}EvenRow{BackColor:Aqua;}Style59{AlignHorz:Near;" +
			"}Style58{AlignHorz:Near;}RecordSelector{AlignImage:Center;}Style51{}Style50{}Foo" +
			"ter{}Style52{AlignHorz:Near;}Style53{AlignHorz:Near;}Style54{}Style55{}Style56{}" +
			"Style57{}Caption{AlignHorz:Center;}Style69{}Style68{}Style63{}Style62{}Style61{}" +
			"Style60{}Style67{}Style66{}Style65{AlignHorz:Near;}Style64{AlignHorz:Near;}Group" +
			"{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}</Data></Styles" +
			"><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" Name" +
			"=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRi" +
			"ghtColumn=\"True\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"17\" DefRec" +
			"SelWidth=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle pa" +
			"rent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRow" +
			"Style parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Styl" +
			"e13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=" +
			"\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle pare" +
			"nt=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><" +
			"OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSel" +
			"ector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style pare" +
			"nt=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"S" +
			"tyle2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=" +
			"\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHeader" +
			"Style parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
			"50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Widt" +
			"h>60</Width><Height>15</Height><Locked>True</Locked><DCIdx>3</DCIdx></C1DisplayC" +
			"olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style paren" +
			"t=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorSty" +
			"le parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\"" +
			" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><Colu" +
			"mnDivider>DarkGray,Single</ColumnDivider><Width>229</Width><Height>15</Height><L" +
			"ocked>True</Locked><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSt" +
			"yle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><Footer" +
			"Style parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /" +
			"><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Styl" +
			"e1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Column" +
			"Divider><Width>125</Width><Height>15</Height><Button>True</Button><DCIdx>0</DCId" +
			"x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" " +
			"/><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36" +
			"\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1" +
			"\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True<" +
			"/Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>150</Width><Height" +
			">15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
			"ent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle p" +
			"arent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><Group" +
			"HeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=" +
			"\"Style56\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider" +
			"><Width>75</Width><Height>15</Height><Locked>True</Locked><DCIdx>4</DCIdx></C1Di" +
			"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style" +
			" parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><Edi" +
			"torStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"St" +
			"yle63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible" +
			"><ColumnDivider>DarkGray,Single</ColumnDivider><Width>130</Width><Height>15</Hei" +
			"ght><Locked>True</Locked><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
			"dingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\" me=\"Style65\" /><" +
			"FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"Styl" +
			"e67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupFooterStyle parent" +
			"=\"Style1\" me=\"Style68\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</" +
			"ColumnDivider><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayCol" +
			"umn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Styl" +
			"e23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" " +
			"me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyl" +
			"e parent=\"Style1\" me=\"Style26\" /><ColumnDivider>DarkGray,Single</ColumnDivider><" +
			"Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
			"le parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterS" +
			"tyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" />" +
			"<GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style" +
			"1\" me=\"Style32\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Heig" +
			"ht><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Styl" +
			"e2\" me=\"Style70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"St" +
			"yle3\" me=\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderSty" +
			"le parent=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\"" +
			" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>9</DC" +
			"Idx></C1DisplayColumn></internalCols><ClientRect>0, 0, 940, 65</ClientRect><Bord" +
			"erSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Styl" +
			"e parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"H" +
			"eading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Head" +
			"ing\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Norma" +
			"l\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Norma" +
			"l\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" m" +
			"e=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Capt" +
			"ion\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSpl" +
			"its><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientA" +
			"rea>0, 0, 940, 65</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><Pr" +
			"intPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
		// 
		// BookNameLabel
		// 
		this.BookNameLabel.Location = new System.Drawing.Point(4, 28);
		this.BookNameLabel.Name = "BookNameLabel";
		this.BookNameLabel.Size = new System.Drawing.Size(128, 18);
		this.BookNameLabel.TabIndex = 10;
		this.BookNameLabel.Text = "Book Parent Name:";
		this.BookNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// DesksDropDown
		// 
		this.DesksDropDown.AllowColMove = false;
		this.DesksDropDown.AllowColSelect = false;
		this.DesksDropDown.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows;
		this.DesksDropDown.AlternatingRows = false;
		this.DesksDropDown.CaptionHeight = 17;
		this.DesksDropDown.ColumnCaptionHeight = 17;
		this.DesksDropDown.ColumnFooterHeight = 17;
		this.DesksDropDown.ExtendRightColumn = true;
		this.DesksDropDown.FetchRowStyles = false;
		this.DesksDropDown.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
		this.DesksDropDown.Location = new System.Drawing.Point(364, 148);
		this.DesksDropDown.Name = "DesksDropDown";
		this.DesksDropDown.RowDivider.Color = System.Drawing.Color.DarkGray;
		this.DesksDropDown.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
		this.DesksDropDown.RowHeight = 15;
		this.DesksDropDown.RowSubDividerColor = System.Drawing.Color.DarkGray;
		this.DesksDropDown.ScrollTips = false;
		this.DesksDropDown.Size = new System.Drawing.Size(376, 248);
		this.DesksDropDown.TabIndex = 25;
		this.DesksDropDown.TabStop = false;
		this.DesksDropDown.ValueTranslate = true;
		this.DesksDropDown.Visible = false;
		this.DesksDropDown.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Desk\" DataF" +
			"ield=\"Desk\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
			"ption=\"Firm\" DataField=\"Firm\"><ValueItems /><GroupInfo /></C1DataColumn></DataCo" +
			"ls><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Caption{AlignH" +
			"orz:Center;}Normal{Font:Verdana, 8.25pt;}Style25{}Selected{ForeColor:HighlightTe" +
			"xt;BackColor:Highlight;}Editor{}Style18{}Style19{}Style14{AlignHorz:Near;}Style1" +
			"5{AlignHorz:Near;}Style16{}Style17{}Style10{AlignHorz:Near;}Style11{}OddRow{}Sty" +
			"le13{}Style12{}Footer{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;" +
			"}RecordSelector{AlignImage:Center;}Style24{}Style23{}Style22{}Style21{AlignHorz:" +
			"Near;}Style20{AlignHorz:Near;}Inactive{ForeColor:InactiveCaptionText;BackColor:I" +
			"nactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;AlignVert:Center;Borde" +
			"r:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}FilterBar{}Style4{" +
			"}Style9{}Style8{}Style5{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;Ali" +
			"gnVert:Center;}Style7{}Style6{}Style1{}Style3{}Style2{}</Data></Styles><Splits><" +
			"C1.Win.C1TrueDBGrid.DropdownView HBarStyle=\"None\" AllowColMove=\"False\" AllowColS" +
			"elect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHe" +
			"ight=\"17\" ExtendRightColumn=\"True\" MarqueeStyle=\"DottedCellBorder\" RecordSelecto" +
			"rWidth=\"17\" DefRecSelWidth=\"17\" RecordSelectors=\"False\" VerticalScrollGroup=\"1\" " +
			"HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorSt" +
			"yle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><" +
			"FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me" +
			"=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Head" +
			"ing\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><Inact" +
			"iveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9" +
			"\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle p" +
			"arent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCol" +
			"s><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent=\"S" +
			"tyle1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle p" +
			"arent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><" +
			"GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><ColumnDi" +
			"vider>DarkGray,Single</ColumnDivider><Width>125</Width><Height>15</Height><DCIdx" +
			">0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
			"tyle20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=" +
			"\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent" +
			"=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Visib" +
			"le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>350</Width" +
			"><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn></internalCols><ClientRect" +
			">0, 0, 372, 244</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.Drop" +
			"downView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Nor" +
			"mal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading" +
			"\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" " +
			"me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"" +
			"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=" +
			"\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" " +
			"me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>" +
			"1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelW" +
			"idth>17</DefaultRecSelWidth><ClientArea>0, 0, 372, 244</ClientArea></Blob>";
		// 
		// BookLabel
		// 
		this.BookLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.BookLabel.ForeColor = System.Drawing.Color.Maroon;
		this.BookLabel.Location = new System.Drawing.Point(136, 28);
		this.BookLabel.Name = "BookLabel";
		this.BookLabel.Size = new System.Drawing.Size(404, 18);
		this.BookLabel.TabIndex = 26;
		this.BookLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		// 
		// Address1Label
		// 
		this.Address1Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.Address1Label.Location = new System.Drawing.Point(24, 444);
		this.Address1Label.Name = "Address1Label";
		this.Address1Label.Size = new System.Drawing.Size(72, 18);
		this.Address1Label.TabIndex = 28;
		this.Address1Label.Text = "Address 1:";
		this.Address1Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
		// 
		// Address2Label
		// 
		this.Address2Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.Address2Label.Location = new System.Drawing.Point(24, 476);
		this.Address2Label.Name = "Address2Label";
		this.Address2Label.Size = new System.Drawing.Size(72, 18);
		this.Address2Label.TabIndex = 29;
		this.Address2Label.Text = "Address 2:";
		this.Address2Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
		// 
		// Address3Label
		// 
		this.Address3Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.Address3Label.Location = new System.Drawing.Point(24, 508);
		this.Address3Label.Name = "Address3Label";
		this.Address3Label.Size = new System.Drawing.Size(72, 18);
		this.Address3Label.TabIndex = 30;
		this.Address3Label.Text = "Address 3:";
		this.Address3Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
		// 
		// AddressLine1TextBox
		// 
		this.AddressLine1TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.AddressLine1TextBox.Location = new System.Drawing.Point(100, 444);
		this.AddressLine1TextBox.Name = "AddressLine1TextBox";
		this.AddressLine1TextBox.ReadOnly = true;
		this.AddressLine1TextBox.Size = new System.Drawing.Size(208, 21);
		this.AddressLine1TextBox.TabIndex = 31;
		this.AddressLine1TextBox.Text = "";
		// 
		// AddressLine2TextBox
		// 
		this.AddressLine2TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.AddressLine2TextBox.Location = new System.Drawing.Point(100, 476);
		this.AddressLine2TextBox.Name = "AddressLine2TextBox";
		this.AddressLine2TextBox.ReadOnly = true;
		this.AddressLine2TextBox.Size = new System.Drawing.Size(208, 21);
		this.AddressLine2TextBox.TabIndex = 32;
		this.AddressLine2TextBox.Text = "";
		// 
		// AddressLine3TextBox
		// 
		this.AddressLine3TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.AddressLine3TextBox.Location = new System.Drawing.Point(100, 508);
		this.AddressLine3TextBox.Name = "AddressLine3TextBox";
		this.AddressLine3TextBox.ReadOnly = true;
		this.AddressLine3TextBox.Size = new System.Drawing.Size(208, 21);
		this.AddressLine3TextBox.TabIndex = 33;
		this.AddressLine3TextBox.Text = "";
		// 
		// DtcDeliverLabel
		// 
		this.DtcDeliverLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.DtcDeliverLabel.Location = new System.Drawing.Point(372, 444);
		this.DtcDeliverLabel.Name = "DtcDeliverLabel";
		this.DtcDeliverLabel.Size = new System.Drawing.Size(80, 18);
		this.DtcDeliverLabel.TabIndex = 34;
		this.DtcDeliverLabel.Text = "DTC Deliver:";
		this.DtcDeliverLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
		// 
		// DtcMarkLabel
		// 
		this.DtcMarkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.DtcMarkLabel.Location = new System.Drawing.Point(372, 476);
		this.DtcMarkLabel.Name = "DtcMarkLabel";
		this.DtcMarkLabel.Size = new System.Drawing.Size(80, 18);
		this.DtcMarkLabel.TabIndex = 35;
		this.DtcMarkLabel.Text = "DTC Mark:";
		this.DtcMarkLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
		// 
		// DtcDeliverTextBox
		// 
		this.DtcDeliverTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.DtcDeliverTextBox.Location = new System.Drawing.Point(456, 444);
		this.DtcDeliverTextBox.Name = "DtcDeliverTextBox";
		this.DtcDeliverTextBox.ReadOnly = true;
		this.DtcDeliverTextBox.TabIndex = 36;
		this.DtcDeliverTextBox.Text = "";
		// 
		// DtcMarkTextBox
		// 
		this.DtcMarkTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		this.DtcMarkTextBox.Location = new System.Drawing.Point(456, 476);
		this.DtcMarkTextBox.Name = "DtcMarkTextBox";
		this.DtcMarkTextBox.ReadOnly = true;
		this.DtcMarkTextBox.TabIndex = 37;
		this.DtcMarkTextBox.Text = "";
		// 
		// YesterdayRadioButton
		// 
		this.YesterdayRadioButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.YesterdayRadioButton.Location = new System.Drawing.Point(572, 20);
		this.YesterdayRadioButton.Name = "YesterdayRadioButton";
		this.YesterdayRadioButton.Size = new System.Drawing.Size(116, 16);
		this.YesterdayRadioButton.TabIndex = 38;
		this.YesterdayRadioButton.Text = "COB Yesterday";
		// 
		// NowTodayRadioButton
		// 
		this.NowTodayRadioButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.NowTodayRadioButton.Location = new System.Drawing.Point(712, 20);
		this.NowTodayRadioButton.Name = "NowTodayRadioButton";
		this.NowTodayRadioButton.Size = new System.Drawing.Size(116, 16);
		this.NowTodayRadioButton.TabIndex = 39;
		this.NowTodayRadioButton.Text = "Now Today";
		this.NowTodayRadioButton.CheckedChanged += new System.EventHandler(this.NowTodayRadioButton_CheckedChanged);
		// 
		// AdminBooksForm
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
		this.ClientSize = new System.Drawing.Size(1220, 549);
		this.Controls.Add(this.NowTodayRadioButton);
		this.Controls.Add(this.YesterdayRadioButton);
		this.Controls.Add(this.DtcMarkTextBox);
		this.Controls.Add(this.DtcDeliverTextBox);
		this.Controls.Add(this.AddressLine3TextBox);
		this.Controls.Add(this.AddressLine2TextBox);
		this.Controls.Add(this.AddressLine1TextBox);
		this.Controls.Add(this.BookGrid);
		this.Controls.Add(this.BookParentGrid);
		this.Controls.Add(this.DtcMarkLabel);
		this.Controls.Add(this.DtcDeliverLabel);
		this.Controls.Add(this.Address3Label);
		this.Controls.Add(this.Address2Label);
		this.Controls.Add(this.Address1Label);
		this.Controls.Add(this.BookLabel);
		this.Controls.Add(this.DesksDropDown);
		this.Controls.Add(this.BookNameLabel);
		this.Controls.Add(this.BookGroupNameLabel);
		this.Controls.Add(this.BookGroupLabel);
		this.Controls.Add(this.BookGroupCombo);
		this.DockPadding.Bottom = 120;
		this.DockPadding.Left = 1;
		this.DockPadding.Right = 1;
		this.DockPadding.Top = 50;
		this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.Name = "AdminBooksForm";
		this.Text = "Admin - Books";
		this.Load += new System.EventHandler(this.AdminBooksForm_Load);
		this.Closed += new System.EventHandler(this.AdminBooksForm_Closed);
		((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.BookParentGrid)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.BookGrid)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.DesksDropDown)).EndInit();
		this.ResumeLayout(false);

	}
    #endregion


    private void DataConfig()
    {
      long amountLimitBorrowTotal = 0;
      decimal amountBorrowPriorDayTotal = 0;
      decimal amountBorrowTodayTotal = 0;

      long amountLimitLoanTotal = 0;
      decimal amountLoanPriorDayTotal = 0;
      decimal amountLoanTodayTotal = 0;

      long amountLimitTotalTotal = 0;
      decimal amountTotalPriorDayTotal = 0;
      decimal amountTotalTodayTotal = 0;

      float f;

      try
      {
        foreach (DataRowView row in bookParentsDataView)
        {

					row["AmountLimitTotal"] = (long)row["AmountLimitBorrow"] + (long)row["AmountLimitLoan"]; // In case of update.
						
          amountLimitBorrowTotal += (long)row["AmountLimitBorrow"];
          
					amountBorrowPriorDayTotal += (decimal)row["AmountBorrowPriorDay"];
          amountBorrowTodayTotal += (decimal)row["AmountBorrowToday"];
          amountLimitLoanTotal += (long)row["AmountLimitLoan"];
          amountLoanPriorDayTotal += (decimal)row["AmountLoanPriorDay"];
          amountLoanTodayTotal += (decimal)row["AmountLoanToday"];

          amountLimitTotalTotal += (long)row["AmountLimitTotal"];
          amountTotalPriorDayTotal += (decimal)row["AmountTotalPriorDay"];
          amountTotalTodayTotal += (decimal)row["AmountTotalToday"];

          if ((long)row["AmountLimitBorrow"] > 0)
          {
            row["RatioBorrowPriorDay"] = 100F * (float)(decimal)row["AmountBorrowPriorDay"] / (float)(long)row["AmountLimitBorrow"];
            row["RatioBorrowToday"] = 100F * (float)(decimal)row["AmountBorrowToday"] / (float)(long)row["AmountLimitBorrow"];
          }
          else
          {
            row["RatioBorrowPriorDay"] = DBNull.Value;
            row["RatioBorrowToday"] = DBNull.Value;
          }

          if ((long)row["AmountLimitLoan"] > 0)
          {
            row["RatioLoanPriorDay"] = 100F * (float)(decimal)row["AmountLoanPriorDay"] / (float)(long)row["AmountLimitLoan"];
            row["RatioLoanToday"] = 100F * (float)(decimal)row["AmountLoanToday"] / (float)(long)row["AmountLimitLoan"];
          }
          else
          {
            row["RatioLoanPriorDay"] = DBNull.Value;
            row["RatioLoanToday"] = DBNull.Value;
          }

          if ((long)row["AmountLimitTotal"] > 0)
          {
            row["RatioTotalPriorDay"] = 100F * (float)(decimal)row["AmountTotalPriorDay"] / (float)(long)row["AmountLimitTotal"];
            row["RatioTotalToday"] = 100F * (float)(decimal)row["AmountTotalToday"] / (float)(long)row["AmountLimitTotal"];
          }
          else
          {
            row["RatioTotalPriorDay"] = DBNull.Value;
            row["RatioTotalToday"] = DBNull.Value;
          }
        }      

        BookParentGrid.Columns["AmountLimitBorrow"].FooterText = amountLimitBorrowTotal.ToString("#,##0");
        BookParentGrid.Columns["AmountBorrowPriorDay"].FooterText = amountBorrowPriorDayTotal.ToString("#,##0");
        BookParentGrid.Columns["AmountBorrowToday"].FooterText = amountBorrowTodayTotal.ToString("#,##0");
        BookParentGrid.Columns["AmountLimitLoan"].FooterText = amountLimitLoanTotal.ToString("#,##0");
        BookParentGrid.Columns["AmountLoanPriorDay"].FooterText = amountLoanPriorDayTotal.ToString("#,##0");
        BookParentGrid.Columns["AmountLoanToday"].FooterText = amountLoanTodayTotal.ToString("#,##0");
        BookParentGrid.Columns["AmountLimitTotal"].FooterText = amountLimitTotalTotal.ToString("#,##0");
        BookParentGrid.Columns["AmountTotalPriorDay"].FooterText = amountTotalPriorDayTotal.ToString("#,##0");
        BookParentGrid.Columns["AmountTotalToday"].FooterText = amountTotalTodayTotal.ToString("#,##0");

        if (amountLimitBorrowTotal > 0)
        {
          f = 100F * (float)amountBorrowPriorDayTotal / (float)amountLimitBorrowTotal;
          BookParentGrid.Columns["RatioBorrowPriorDay"].FooterText = f.ToString("0.0");

          f = 100F * (float)amountBorrowTodayTotal / (float)amountLimitBorrowTotal;
          BookParentGrid.Columns["RatioBorrowToday"].FooterText = f.ToString("0.0");
        }
        else
        {
          BookParentGrid.Columns["RatioBorrowPriorDay"].FooterText = "";
          BookParentGrid.Columns["RatioBorrowToday"].FooterText = "";
        }

        if (amountLimitLoanTotal > 0)
        {
          f = 100F * (float)amountLoanPriorDayTotal / (float)amountLimitLoanTotal;
          BookParentGrid.Columns["RatioLoanPriorDay"].FooterText = f.ToString("0.0");

          f = 100F * (float)amountLoanTodayTotal / (float)amountLimitLoanTotal;
          BookParentGrid.Columns["RatioLoanToday"].FooterText = f.ToString("0.0");
        }
        else
        {
          BookParentGrid.Columns["RatioLoanPriorDay"].FooterText = "";
          BookParentGrid.Columns["RatioLoanToday"].FooterText = "";
        }

        if (amountLimitTotalTotal > 0)
        {
          f = 100F * (float)amountTotalPriorDayTotal / (float)amountLimitTotalTotal;
          BookParentGrid.Columns["RatioTotalPriorDay"].FooterText = f.ToString("0.0");

          f = 100F * (float)amountTotalTodayTotal / (float)amountLimitTotalTotal;
          BookParentGrid.Columns["RatioTotalToday"].FooterText = f.ToString("0.0");
        }
        else
        {
          BookParentGrid.Columns["RatioTotalPriorDay"].FooterText = "";
          BookParentGrid.Columns["RatioTotalToday"].FooterText = "";
        }

        BookParentGrid.Columns["BookParent"].FooterText = "Totals:";
      }
      catch (Exception e)
      {
        mainForm.Alert(e.Message, PilotState.RunFault);
        Log.Write(e.Message + " [AdminBooksForm.DataConfig]", Log.Error, 1); 
      }
    }

    private void AdminBooksForm_Load(object sender, System.EventArgs e)
    {
      int height  = mainForm.ClientSize.Height - 275;
      int width   = mainForm.ClientSize.Width - 75;

      this.Top    = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
      this.Left   = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
      this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
      this.Width  = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

      this.Show();
      this.Cursor = Cursors.WaitCursor;

      Application.DoEvents();
      
      mainForm.Alert("Please wait... Loading current book data...", PilotState.Unknown);
      
      try
      {
        mayEdit = mainForm.AdminAgent.MayEdit(mainForm.UserId, "AdminBooks");
 
        BookParentGrid.AllowUpdate = mayEdit;
        BookGrid.AllowUpdate = mayEdit;
        
        dataSet = mainForm.AdminAgent.BookDataGet(mainForm.UtcOffset);
        dataSet.Merge(mainForm.ServiceAgent.DeskGet().Tables["Desks"]);

        dataSet.Tables["BookParents"].Columns.Add("RatioBorrowPriorDay", typeof(float));
        dataSet.Tables["BookParents"].Columns.Add("RatioLoanPriorDay", typeof(float));
        dataSet.Tables["BookParents"].Columns.Add("RatioTotalPriorDay", typeof(float));

        dataSet.Tables["BookParents"].Columns.Add("RatioBorrowToday", typeof(float));
        dataSet.Tables["BookParents"].Columns.Add("RatioLoanToday", typeof(float));
        dataSet.Tables["BookParents"].Columns.Add("RatioTotalToday", typeof(float));

        dataSet.Tables["Books"].Columns.Add("Desk", typeof(string));

        foreach (DataRow dataRow in dataSet.Tables["Books"].Rows)
        {
          if (!dataRow["Firm"].ToString().Equals("") && !dataRow["Country"].ToString().Equals("") && !dataRow["DeskType"].ToString().Equals(""))
          {
            dataRow["Desk"] = dataRow["Firm"].ToString() + "." + dataRow["Country"].ToString() + "." + dataRow["DeskType"].ToString();
          }
        }
        dataSet.Tables["Books"].AcceptChanges();

        DataRow booksRow = dataSet.Tables["Books"].NewRow();
        
        booksRow["BookGroup"] = "****";
        booksRow["Book"] = "****";
        booksRow["BookParent"] = "****";
        booksRow["BookName"] = "ALL BOOK GROUPS COMBINED";
        booksRow["AmountLimitBorrow"] = 0;
        booksRow["AmountLimitLoan"] = 0;
        booksRow["IsActive"] = 1;
        
        dataSet.Tables["Books"].Rows.Add(booksRow);
        dataSet.Tables["Books"].AcceptChanges();

        bookParentsDataView = new DataView(dataSet.Tables["BookParents"]);
        bookParentsDataView.RowFilter = "BookGroup <> '****' And BookGroup <> BookParent";
        //bookParentsDataView.Sort = "BookParent";
				bookParentsDataView.Sort = "BookGroup";
        
        DataRow bookParentsRow = null;

        foreach (DataRowView dataRowView in bookParentsDataView)
        {
					if ((bookParentsRow == null) || 
	 					 (bookParentsRow["BookParent"].ToString() != dataRowView["BookParent"].ToString()) )
					{
						
						if (bookParentsRow != null) // There is a row to insert.
            {
              dataSet.Tables["BookParents"].Rows.Add(bookParentsRow);
            }

            bookParentsRow = dataSet.Tables["BookParents"].NewRow();

            bookParentsRow["BookGroup"] = "****";
            bookParentsRow["BookParent"] = dataRowView["BookParent"];
            bookParentsRow["BookName"] = dataRowView["BookName"];
	  				bookParentsRow["AmountLimitBorrow"] = dataRowView["AmountLimitBorrow"];
            bookParentsRow["AmountLimitLoan"] = dataRowView["AmountLimitLoan"];
            bookParentsRow["AmountLimitTotal"] = dataRowView["AmountLimitTotal"];
						bookParentsRow["AmountBorrowPriorDay"] = dataRowView["AmountBorrowPriorDay"];
            bookParentsRow["AmountLoanPriorDay"] = dataRowView["AmountLoanPriorDay"];
            bookParentsRow["AmountTotalPriorDay"] = dataRowView["AmountTotalPriorDay"];
            bookParentsRow["AmountBorrowToday"] = dataRowView["AmountBorrowToday"];
            bookParentsRow["AmountLoanToday"] = dataRowView["AmountLoanToday"];
            bookParentsRow["AmountTotalToday"] = dataRowView["AmountTotalToday"];

           }
          else
          {
						bookParentsRow["AmountLimitBorrow"] = (long)dataRowView["AmountLimitBorrow"] + (long)bookParentsRow["AmountLimitBorrow"];
            bookParentsRow["AmountLimitLoan"] = (long)dataRowView["AmountLimitLoan"] + (long)bookParentsRow["AmountLimitLoan"];
            bookParentsRow["AmountLimitTotal"] = (long)dataRowView["AmountLimitTotal"] + (long)bookParentsRow["AmountLimitTotal"];
						bookParentsRow["AmountBorrowPriorDay"] = (decimal)dataRowView["AmountBorrowPriorDay"] + (decimal)bookParentsRow["AmountBorrowPriorDay"];
            bookParentsRow["AmountLoanPriorDay"] = (decimal)dataRowView["AmountLoanPriorDay"] + (decimal)bookParentsRow["AmountLoanPriorDay"];
            bookParentsRow["AmountTotalPriorDay"] = (decimal)dataRowView["AmountTotalPriorDay"] + (decimal)bookParentsRow["AmountTotalPriorDay"];
            bookParentsRow["AmountBorrowToday"] = (decimal)dataRowView["AmountBorrowToday"] + (decimal)bookParentsRow["AmountBorrowToday"];
            bookParentsRow["AmountLoanToday"] = (decimal)dataRowView["AmountLoanToday"] + (decimal)bookParentsRow["AmountLoanToday"];
            bookParentsRow["AmountTotalToday"] = (decimal)dataRowView["AmountTotalToday"] + (decimal)bookParentsRow["AmountTotalToday"];

          }
        }

        if (bookParentsRow != null) // There is a row to insert.
        {
          dataSet.Tables["BookParents"].Rows.Add(bookParentsRow);
        }
        dataSet.Tables["BookParents"].AcceptChanges();

        BookParentGrid.SetDataBinding(bookParentsDataView, null, true);
        
        BookLabel.DataBindings.Add("Text", bookParentsDataView, "BookName");
        
        bookDataView = new DataView(dataSet.Tables["Books"]);        
        BookGrid.SetDataBinding(bookDataView, null, true);

        AddressLine1TextBox.DataBindings.Add("Text", bookDataView, "AddressLine1");
        AddressLine2TextBox.DataBindings.Add("Text", bookDataView, "AddressLine2");
        AddressLine3TextBox.DataBindings.Add("Text", bookDataView, "AddressLine3");

        DtcDeliverTextBox.DataBindings.Add("Text", bookDataView, "DtcDeliver");
        DtcMarkTextBox.DataBindings.Add("Text", bookDataView, "DtcMark");

        BookParentGrid.ChildGrid = BookGrid;
        
        bookGroupDataView = new DataView(dataSet.Tables["Books"], "BookGroup = Book", "BookGroup", DataViewRowState.CurrentRows);
        
        BookGroupNameLabel.DataSource = bookGroupDataView;
        BookGroupNameLabel.DataField  = "BookName";

        BookGroupCombo.HoldFields();
        BookGroupCombo.DataSource = bookGroupDataView;
        BookGroupCombo.SelectedIndex = -1;
        BookGroupCombo.SelectedIndex = 0;

        NowTodayRadioButton.Checked = true;
              
        DesksDropDown.SetDataBinding(dataSet, "Desks", true);

        mainForm.Alert("Loading current book data... Done!", PilotState.Normal);      
      }
      catch(Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminBooksForm.AdminBooksForm_Load]", Log.Error, 1);
      }

      this.Cursor = Cursors.Default;
    }

    private void AdminBooksForm_Closed(object sender, System.EventArgs e)
    {
      if(this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
        RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
        RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
        RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
      }     
    }

    private void BookGroupCombo_RowChange(object sender, System.EventArgs e)
    {
      if (bookParentsDataView != null)
      {
        bookParentsDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND BookParent <> '" + BookGroupCombo.Text + "'";
      }

      DataConfig();
    }

    private void BookParentGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
    {
      if (bookDataView != null)
      {
        bookDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND BookParent = '"
          + BookParentGrid.Columns["BookParent"].Text + "'";
      }
    }

    private void BookParentGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        mainForm.Alert("Please wait... Updating book parent data for " + BookParentGrid.Columns["BookParent"].Text + "...", PilotState.Unknown);

        mainForm.AdminAgent.BookDataSet(BookGroupCombo.Text,
          BookParentGrid.Columns["BookParent"].Text,
          long.Parse(BookParentGrid.Columns["AmountLimitBorrow"].Value.ToString()),
          long.Parse(BookParentGrid.Columns["AmountLimitLoan"].Value.ToString()),
          BookGrid.Columns["FaxNumber"].Text,
          BookGrid.Columns["Firm"].Text,
          BookGrid.Columns["Country"].Text,
          BookGrid.Columns["DeskType"].Text,
          mainForm.UserId,
          BookParentGrid.Columns["Comment"].Text);

        BookParentGrid.Columns["ActUserShortName"].Text = "me";
        BookParentGrid.Columns["ActTime"].Text = DateTime.Now.ToString(Standard.DateTimeFileFormat);

        DataConfig();

        mainForm.Alert("Updating book parent data for " + BookParentGrid.Columns["BookParent"].Text + "... Done!", PilotState.Normal);
      }
      catch(Exception error)
      {
        e.Cancel = true;

        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminBooksForm.BookParentGrid_BeforeUpdate]", Log.Error, 1);
      }
    }

    private void BookParentGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {
      if(e.Value.Equals(""))
      {
        return;
      }

      try
      {
        switch(BookParentGrid.Columns[e.ColIndex].DataField)
        {
          case "ActTime" :
            e.Value = DateTime.Parse(e.Value).ToString(Standard.DateTimeFileFormat);
            break;
          case "AmountLimitBorrow" :
          case "AmountLimitLoan" :
          case "AmountLimitTotal" :
          case "AmountBorrowPriorDay" :
          case "AmountLoanPriorDay" :
          case "AmountTotalPriorDay" :
          case "AmountBorrowToday" :
          case "AmountLoanToday" :
          case "AmountTotalToday" :
            e.Value = decimal.Parse(e.Value).ToString("#,##0");
            break;
          case "RatioBorrowPriorDay" :
          case "RatioLoanPriorDay" :
          case "RatioTotalPriorDay" :
          case "RatioBorrowToday" :
          case "RatioLoanToday" :
          case "RatioTotalToday" :
            e.Value = float.Parse(e.Value).ToString("#0.0");
            break;
        }
      }
      catch {}
    }

    private void BookParentGrid_Resize(object sender, System.EventArgs e)
    {
      BookGrid.Width = BookParentGrid.Width - 65;    
    }

    private void BookParentGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      string gridData = "";

      try
      {
        if (e.KeyChar.Equals((char)13) && BookParentGrid.DataChanged)
        {
          BookParentGrid.UpdateData();
          e.Handled = true;
        }

        if (e.KeyChar.Equals((char)27))
        {
          if (!BookParentGrid.EditActive && BookParentGrid.DataChanged)
          {
            BookParentGrid.DataChanged = false;
          }
        }

        if (e.KeyChar.Equals((char)3) && BookParentGrid.SelectedRows.Count > 0)
        {
          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BookParentGrid.SelectedCols)
          {
            gridData += dataColumn.Caption + "\t";
          }

          gridData += "\n";

          foreach (int row in BookParentGrid.SelectedRows)
          {
            foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BookParentGrid.SelectedCols)
            {
              gridData += dataColumn.CellText(row) + "\t";
            }

            gridData += "\n";            
          }

          Clipboard.SetDataObject(gridData, true);
          mainForm.Alert("Copied " + BookParentGrid.SelectedRows.Count.ToString("#,##0") + " rows to the clipboard.");
          e.Handled = true;
        }

      }
      catch (Exception ee)
      {
        mainForm.Alert(ee.Message);
      }
    }

    private void BookParentGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.X <= BookParentGrid.RecordSelectorWidth && e.Y <= BookParentGrid.RowHeight)
      {
        if (BookParentGrid.SelectedRows.Count.Equals(0))
        {
          for (int i = 0; i < BookParentGrid.Splits[0,0].Rows.Count; i++)
          {
            BookParentGrid.SelectedRows.Add(i);
          }

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BookParentGrid.Columns)
          {
            BookParentGrid.SelectedCols.Add(dataColumn);
          }
        }
        else
        {
          BookParentGrid.SelectedRows.Clear();
          BookParentGrid.SelectedCols.Clear();
        }
      }
    }

    private void BookGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        mainForm.Alert("Please wait... Updating data for book " + BookGrid.Columns["Book"].Text + "...", PilotState.Unknown);

        mainForm.AdminAgent.BookDataSet(BookGroupCombo.Text,
          BookGrid.Columns["Book"].Text,
          long.Parse(BookParentGrid.Columns["AmountLimitBorrow"].Value.ToString()),
          long.Parse(BookParentGrid.Columns["AmountLimitLoan"].Value.ToString()),
          BookGrid.Columns["FaxNumber"].Text,
          BookGrid.Columns["Firm"].Text,
          BookGrid.Columns["Country"].Text,
          BookGrid.Columns["DeskType"].Text,
          mainForm.UserId,
          BookGrid.Columns["Comment"].Text);

        mainForm.Alert("Updating data for book " + BookGrid.Columns["Book"].Text + "... Done!", PilotState.Normal);
      }
      catch(Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminbooksForm.BookGrid_BeforeUpdate]", Log.Error, 1);
      }
    }

    private void BookGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {
      if(e.Value.Equals(""))
      {
        return;
      }

      try
      {
        switch(BookGrid.Columns[e.ColIndex].DataField)
        {
          case "ActTime" :
            break;
        } 
      }
      catch {}
    }

    private void BookGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      string gridData = "";

      try
      {
        if (e.KeyChar.Equals((char)13) && BookGrid.DataChanged)
        {
          BookGrid.UpdateData();
          e.Handled = true;
        }

        if (e.KeyChar.Equals((char)27))
        {
          if (!BookGrid.EditActive && BookGrid.DataChanged)
          {
            BookGrid.DataChanged = false;
          }
        }

        if (e.KeyChar.Equals((char)3) && BookGrid.SelectedRows.Count > 0)
        {
          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BookGrid.SelectedCols)
          {
            gridData += dataColumn.Caption + "\t";
          }

          gridData += "\n";

          foreach (int row in BookGrid.SelectedRows)
          {
            foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BookGrid.SelectedCols)
            {
              gridData += dataColumn.CellText(row) + "\t";
            }

            gridData += "\n";

            if ((row % 100) == 0)
            {
              mainForm.Alert("Please wait: " + row.ToString("#,##0") + " rows copied so far...");
            }
          }

          Clipboard.SetDataObject(gridData, true);
          mainForm.Alert("Copied " + BookGrid.SelectedRows.Count.ToString("#,##0") + " rows to the clipboard.");
          e.Handled = true;
        }

      }
      catch (Exception ee)
      {
        mainForm.Alert(ee.Message);
      }
    }

    private void BookGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.X <= BookGrid.RecordSelectorWidth && e.Y <= BookGrid.RowHeight)
      {
        if (BookGrid.SelectedRows.Count.Equals(0))
        {
          for (int i = 0; i < BookGrid.Splits[0,0].Rows.Count; i++)
          {
            BookGrid.SelectedRows.Add(i);
          }

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BookGrid.Columns)
          {
            BookGrid.SelectedCols.Add(dataColumn);
          }
        }
        else
        {
          BookGrid.SelectedRows.Clear();
          BookGrid.SelectedCols.Clear();
        }
      }
    }

    private void NowTodayRadioButton_CheckedChanged(object sender, System.EventArgs e)
    {
      BookParentGrid.Splits[0,0].DisplayColumns["AmountBorrowPriorDay"].Visible = !NowTodayRadioButton.Checked;
      BookParentGrid.Splits[0,0].DisplayColumns["RatioBorrowPriorDay"].Visible = !NowTodayRadioButton.Checked;
      BookParentGrid.Splits[0,0].DisplayColumns["AmountLoanPriorDay"].Visible = !NowTodayRadioButton.Checked;
      BookParentGrid.Splits[0,0].DisplayColumns["RatioLoanPriorDay"].Visible = !NowTodayRadioButton.Checked;
      BookParentGrid.Splits[0,0].DisplayColumns["AmountTotalPriorDay"].Visible = !NowTodayRadioButton.Checked;
      BookParentGrid.Splits[0,0].DisplayColumns["RatioTotalPriorDay"].Visible = !NowTodayRadioButton.Checked;
 
      BookParentGrid.Splits[0,0].DisplayColumns["AmountBorrowToday"].Visible = NowTodayRadioButton.Checked;
      BookParentGrid.Splits[0,0].DisplayColumns["RatioBorrowToday"].Visible = NowTodayRadioButton.Checked;
      BookParentGrid.Splits[0,0].DisplayColumns["AmountLoanToday"].Visible = NowTodayRadioButton.Checked;
      BookParentGrid.Splits[0,0].DisplayColumns["RatioLoanToday"].Visible = NowTodayRadioButton.Checked;
      BookParentGrid.Splits[0,0].DisplayColumns["AmountTotalToday"].Visible = NowTodayRadioButton.Checked;
      BookParentGrid.Splits[0,0].DisplayColumns["RatioTotalToday"].Visible = NowTodayRadioButton.Checked;
    }

    private void BookGrid_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
    {
      string[] desk = BookGrid.Columns["Desk"].Text.Split('.');

      if (desk.Length.Equals(3))
      {
        BookGrid.Columns["Firm"].Text = desk[0];
        BookGrid.Columns["Country"].Text = desk[1];
        BookGrid.Columns["DeskType"].Text = desk[2];
      }
    }

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private string BookListGet()
		{
			string gridData = "";

			try
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BookParentGrid.SelectedCols)
				{
					gridData += dataColumn.Caption + "\t";
				}

				gridData += "\r\n";

				foreach (int row in BookParentGrid.SelectedRows)
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in BookParentGrid.SelectedCols)
					{
						gridData += dataColumn.CellText(row) + "\t";
					}
					
					gridData += "\r\n";
				}
				
				return gridData;			
			}
			catch (Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
			}
			
			return "Sorry... Error loading the list.";
		}
		
		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{			                			
			Clipboard.SetDataObject(BookListGet(), true);			
		}

		private void SendToMailRecipientMenuItem_Click(object sender, System.EventArgs e)
		{
			Email email = new Email();
			email.Send(BookListGet());		
		}

		private void BookParentGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)		
		{
			if (NowTodayRadioButton.Checked)
			{
				if ( decimal.Parse(BookParentGrid.Columns["AmountTotalToday"].CellValue(e.Row).ToString()) > decimal.Parse(BookParentGrid.Columns["AmountLimitTotal"].CellValue(e.Row).ToString()))
				{
					e.CellStyle.ForeColor = System.Drawing.Color.Maroon;
				}
			}			
			else
			{
				if ( decimal.Parse(BookParentGrid.Columns["AmountTotalPriorDay"].CellValue(e.Row).ToString()) > decimal.Parse(BookParentGrid.Columns["AmountLimitTotal"].CellValue(e.Row).ToString()))
				{
					e.CellStyle.ForeColor = System.Drawing.Color.Maroon;
				}
			}
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

      Excel excel = new Excel();
			excel.ExportGridToExcel(ref BookParentGrid);

      this.Cursor = Cursors.Default;
		}

  }
}
