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
  public class ShortSaleTradingGroupsForm : System.Windows.Forms.Form
  {
    private System.ComponentModel.Container components = null;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ShortSaleTradingGroupsGrid;
    private MainForm mainForm;

		private string tradingGroup = "";

		private DataSet dataSet = null;
		private string attributesRowFilter  = "";
		private System.Windows.Forms.ContextMenu MainMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem Seperator;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private C1.Win.C1Input.C1DateEdit DateEditor;
		private C1.Win.C1Input.C1Label EffectDateLabel;
		private C1.Win.C1Input.C1Label DefaultPriceLabel;
		private C1.Win.C1Input.C1Label DefaultAutoApprovalLabel;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ShortSaleTradingGroupAttributesGrid;
		private C1.Win.C1Input.C1NumericEdit DefaultPriceNumericEdit;
		private C1.Win.C1Input.C1NumericEdit DefaultAutoApprovalNumericEdit;
		private C1.Win.C1Input.C1TextBox PremiumMinTextBox;
		private C1.Win.C1Input.C1TextBox PremiumMaxTextBox;
		private DataView attributesDataView = null;
		private System.Windows.Forms.MenuItem LocatesSendToEmailMenuItem;
		private System.Windows.Forms.MenuItem LocatesSendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem TradingGroupsSendToEmailMenuItem;
		private System.Windows.Forms.MenuItem TradingGroupsSendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem TradingGroupsSendToExcelMenuItem;
		private C1.Win.C1Input.C1Label DefaultPremiumThresholdLabel;
		private System.Windows.Forms.MenuItem TradingGroupsSendToMenuItem;

    public ShortSaleTradingGroupsForm(MainForm mainForm)
    {
      InitializeComponent();
      this.mainForm = mainForm;
			
			dataSet = new DataSet();

			try
			{
				ShortSaleTradingGroupsGrid.AllowUpdate = mainForm.AdminAgent.MayEdit(mainForm.UserId,"ShortSaleLists");
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
			}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleTradingGroupsForm));
			this.ShortSaleTradingGroupsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.ShortSaleTradingGroupAttributesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainMenu = new System.Windows.Forms.ContextMenu();
			this.TradingGroupsSendToMenuItem = new System.Windows.Forms.MenuItem();
			this.TradingGroupsSendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.TradingGroupsSendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.TradingGroupsSendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.LocatesSendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.LocatesSendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.Seperator = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.DateEditor = new C1.Win.C1Input.C1DateEdit();
			this.EffectDateLabel = new C1.Win.C1Input.C1Label();
			this.DefaultPriceLabel = new C1.Win.C1Input.C1Label();
			this.DefaultAutoApprovalLabel = new C1.Win.C1Input.C1Label();
			this.DefaultPriceNumericEdit = new C1.Win.C1Input.C1NumericEdit();
			this.PremiumMinTextBox = new C1.Win.C1Input.C1TextBox();
			this.DefaultAutoApprovalNumericEdit = new C1.Win.C1Input.C1NumericEdit();
			this.PremiumMaxTextBox = new C1.Win.C1Input.C1TextBox();
			this.DefaultPremiumThresholdLabel = new C1.Win.C1Input.C1Label();
			((System.ComponentModel.ISupportInitialize)(this.ShortSaleTradingGroupsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ShortSaleTradingGroupAttributesGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DateEditor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultPriceLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultAutoApprovalLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultPriceNumericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PremiumMinTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultAutoApprovalNumericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PremiumMaxTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultPremiumThresholdLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// ShortSaleTradingGroupsGrid
			// 
			this.ShortSaleTradingGroupsGrid.CaptionHeight = 17;
			this.ShortSaleTradingGroupsGrid.ChildGrid = this.ShortSaleTradingGroupAttributesGrid;
			this.ShortSaleTradingGroupsGrid.ContextMenu = this.MainMenu;
			this.ShortSaleTradingGroupsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ShortSaleTradingGroupsGrid.ExtendRightColumn = true;
			this.ShortSaleTradingGroupsGrid.FetchRowStyles = true;
			this.ShortSaleTradingGroupsGrid.FilterBar = true;
			this.ShortSaleTradingGroupsGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ShortSaleTradingGroupsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ShortSaleTradingGroupsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.ShortSaleTradingGroupsGrid.Location = new System.Drawing.Point(1, 60);
			this.ShortSaleTradingGroupsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.ShortSaleTradingGroupsGrid.Name = "ShortSaleTradingGroupsGrid";
			this.ShortSaleTradingGroupsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ShortSaleTradingGroupsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ShortSaleTradingGroupsGrid.PreviewInfo.ZoomFactor = 75;
			this.ShortSaleTradingGroupsGrid.RecordSelectorWidth = 16;
			this.ShortSaleTradingGroupsGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.ShortSaleTradingGroupsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ShortSaleTradingGroupsGrid.RowHeight = 16;
			this.ShortSaleTradingGroupsGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.ShortSaleTradingGroupsGrid.Size = new System.Drawing.Size(1074, 412);
			this.ShortSaleTradingGroupsGrid.TabIndex = 0;
			this.ShortSaleTradingGroupsGrid.Text = "Trading Groups";
			this.ShortSaleTradingGroupsGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.ShortSaleTradingGroupsGrid_Paint);
			this.ShortSaleTradingGroupsGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.ShortSaleTradingGroupsGrid_FetchRowStyle);
			this.ShortSaleTradingGroupsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.ShortSaleTradingGroupsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Group Code\"" +
				" DataField=\"GroupCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"Group Name\" DataField=\"GroupName\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"ActUserId\" DataField=\"ActUserId\">" +
				"<ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act T" +
				"ime\" DataField=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Locates Requested\" DataField" +
				"=\"LocatesRequested\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Locates Filled\" DataField=\"Locate" +
				"sFilled\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColu" +
				"mn><C1DataColumn Level=\"0\" Caption=\"IsActive\" DataField=\"IsActive\"><ValueItems /" +
				"><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design" +
				".ContextWrapper\"><Data>Style58{}Style59{}HighlightRow{ForeColor:HighlightText;Ba" +
				"ckColor:Highlight;}Style50{}Style51{}Style52{}Style53{}Style54{AlignHorz:Near;}C" +
				"aption{AlignHorz:Center;}Style56{}Normal{Font:Verdana, 8.25pt;}Style25{}Selected" +
				"{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style31{Font:Verdana, 8.25" +
				"pt, style=Bold;AlignHorz:Far;ForeColor:HotTrack;}Style18{}Style19{}Style14{Align" +
				"Horz:Near;}Style15{Font:Verdana, 8.25pt, style=Bold;AlignHorz:Near;ForeColor:Hig" +
				"hlight;}Style16{}Style17{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Sty" +
				"le46{}FilterBar{BackColor:SeaShell;}Style38{}Style37{Font:Verdana, 8.25pt, style" +
				"=Bold;AlignHorz:Far;}Style34{}Style35{}Style32{}Style33{}OddRow{}Style29{}Style2" +
				"8{}Style27{}Style26{}RecordSelector{AlignImage:Center;}Footer{}Style23{}Style22{" +
				"}Style21{AlignHorz:Near;}Style55{AlignHorz:Near;}Group{AlignVert:Center;Border:N" +
				"one,,0, 0, 0, 0;BackColor:ControlDark;}Style57{}Inactive{ForeColor:InactiveCapti" +
				"onText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;Back" +
				"Color:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}" +
				"Style49{AlignHorz:Near;}Style48{AlignHorz:Near;}Style24{}Style20{AlignHorz:Near;" +
				"}Style41{}Style40{}Style43{AlignHorz:Near;}Style42{AlignHorz:Near;}Style45{}Styl" +
				"e44{}Style47{}Style9{}Style8{}Style39{}Style36{AlignHorz:Near;}Style5{}Style4{}S" +
				"tyle7{}Style6{}Style1{}Style30{AlignHorz:Near;}Style3{}Style2{}</Data></Styles><" +
				"Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" Name=\"" +
				"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRigh" +
				"tColumn=\"True\" FetchRowStyles=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedRowBor" +
				"der\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" Horizo" +
				"ntalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle pa" +
				"rent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><Filter" +
				"BarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Styl" +
				"e3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" m" +
				"e=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveSty" +
				"le parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><R" +
				"ecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=" +
				"\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent=\"Style1\"" +
				" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><ColumnDivider>" +
				"Gainsboro,Single</ColumnDivider><Height>16</Height><Locked>True</Locked><DCIdx>0" +
				"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
				"le20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"S" +
				"tyle22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"" +
				"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Visible" +
				">True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>240</Width>" +
				"<Height>16</Height><Locked>True</Locked><DCIdx>1</DCIdx></C1DisplayColumn><C1Dis" +
				"playColumn><HeadingStyle parent=\"Style2\" me=\"Style30\" /><Style parent=\"Style1\" m" +
				"e=\"Style31\" /><FooterStyle parent=\"Style3\" me=\"Style32\" /><EditorStyle parent=\"S" +
				"tyle5\" me=\"Style33\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style35\" /><GroupFoo" +
				"terStyle parent=\"Style1\" me=\"Style34\" /><Visible>True</Visible><ColumnDivider>Da" +
				"rkGray,Single</ColumnDivider><Width>130</Width><Height>16</Height><Locked>True</" +
				"Locked><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style36\" /><Style parent=\"Style1\" me=\"Style37\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style38\" /><EditorStyle parent=\"Style5\" me=\"Style39\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style41\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e40\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
				"th>130</Width><Height>16</Height><Locked>True</Locked><DCIdx>5</DCIdx></C1Displa" +
				"yColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style48\" /><Style par" +
				"ent=\"Style1\" me=\"Style49\" /><FooterStyle parent=\"Style3\" me=\"Style50\" /><EditorS" +
				"tyle parent=\"Style5\" me=\"Style51\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style5" +
				"3\" /><GroupFooterStyle parent=\"Style1\" me=\"Style52\" /><Visible>True</Visible><Co" +
				"lumnDivider>Gainsboro,Single</ColumnDivider><Height>16</Height><Locked>True</Loc" +
				"ked><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
				"le2\" me=\"Style54\" /><Style parent=\"Style1\" me=\"Style55\" /><FooterStyle parent=\"S" +
				"tyle3\" me=\"Style56\" /><EditorStyle parent=\"Style5\" me=\"Style57\" /><GroupHeaderSt" +
				"yle parent=\"Style1\" me=\"Style59\" /><GroupFooterStyle parent=\"Style1\" me=\"Style58" +
				"\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Heigh" +
				"t>16</Height><Locked>True</Locked><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayCo" +
				"lumn><HeadingStyle parent=\"Style2\" me=\"Style42\" /><Style parent=\"Style1\" me=\"Sty" +
				"le43\" /><FooterStyle parent=\"Style3\" me=\"Style44\" /><EditorStyle parent=\"Style5\"" +
				" me=\"Style45\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style47\" /><GroupFooterSty" +
				"le parent=\"Style1\" me=\"Style46\" /><ColumnDivider>DarkGray,Single</ColumnDivider>" +
				"<Height>16</Height><DCIdx>6</DCIdx></C1DisplayColumn></internalCols><ClientRect>" +
				"0, 0, 1070, 408</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.Merg" +
				"eView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal" +
				"\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" m" +
				"e=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=" +
				"\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"Hig" +
				"hlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"Od" +
				"dRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=" +
				"\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</" +
				"vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidt" +
				"h>16</DefaultRecSelWidth><ClientArea>0, 0, 1070, 408</ClientArea><PrintPageHeade" +
				"rStyle parent=\"\" me=\"Style28\" /><PrintPageFooterStyle parent=\"\" me=\"Style29\" /><" +
				"/Blob>";
			// 
			// ShortSaleTradingGroupAttributesGrid
			// 
			this.ShortSaleTradingGroupAttributesGrid.CaptionHeight = 17;
			this.ShortSaleTradingGroupAttributesGrid.ExtendRightColumn = true;
			this.ShortSaleTradingGroupAttributesGrid.FilterBar = true;
			this.ShortSaleTradingGroupAttributesGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ShortSaleTradingGroupAttributesGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ShortSaleTradingGroupAttributesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.ShortSaleTradingGroupAttributesGrid.Location = new System.Drawing.Point(52, 140);
			this.ShortSaleTradingGroupAttributesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.ShortSaleTradingGroupAttributesGrid.Name = "ShortSaleTradingGroupAttributesGrid";
			this.ShortSaleTradingGroupAttributesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ShortSaleTradingGroupAttributesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ShortSaleTradingGroupAttributesGrid.PreviewInfo.ZoomFactor = 75;
			this.ShortSaleTradingGroupAttributesGrid.RecordSelectorWidth = 16;
			this.ShortSaleTradingGroupAttributesGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.ShortSaleTradingGroupAttributesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ShortSaleTradingGroupAttributesGrid.RowHeight = 16;
			this.ShortSaleTradingGroupAttributesGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.ShortSaleTradingGroupAttributesGrid.Size = new System.Drawing.Size(996, 88);
			this.ShortSaleTradingGroupAttributesGrid.TabIndex = 1;
			this.ShortSaleTradingGroupAttributesGrid.TabStop = false;
			this.ShortSaleTradingGroupAttributesGrid.Text = "Trading Groups";
			this.ShortSaleTradingGroupAttributesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ShortSaleTradingGroupAttributesGrid_BeforeUpdate);
			this.ShortSaleTradingGroupAttributesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.ShortSaleTradingGroupAttributesGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ShortSaleTradingGroupAttributesGrid_KeyPress);
			this.ShortSaleTradingGroupAttributesGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Group Code\"" +
				" DataField=\"GroupCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"Group Name\" DataField=\"GroupName\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Auto Approval\" DataField=\"AutoApp" +
				"rovalMax\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"Email Address\" DataField=\"EmailAddress\"><Va" +
				"lueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"ActUserI" +
				"d\" DataField=\"ActUserId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
				" Level=\"0\" Caption=\"Act Time\" DataField=\"ActTime\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn><C1DataColumn Level=\"0\" Caption=\"Email Date\" DataField=\"LastEmailDat" +
				"e\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"AE\" DataField=\"AutoEmail\"><ValueItems Presentation" +
				"=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Minim" +
				"um Price\" DataField=\"MinPrice\" NumberFormat=\"FormatText Event\"><ValueItems /><Gr" +
				"oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Premium Min\" DataField" +
				"=\"PremiumMin\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"Premium Max\" DataField=\"PremiumMax\" Num" +
				"berFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataCo" +
				"lumn Level=\"0\" Caption=\"Negative Rebate MarkUp\" DataField=\"NegativeRebateMarkUp\"" +
				"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Nega" +
				"tiveRebateBill\" DataField=\"NegativeRebateBill\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"PositiveRebateMarkUp\" DataField=\"Posit" +
				"iveRebateMarkUp\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"" +
				"0\" Caption=\"PositiveRebateBill\" DataField=\"PositiveRebateBill\"><ValueItems /><Gr" +
				"oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"FedFundsMarkUp\" DataFi" +
				"eld=\"FedFundsMarkUp\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Lev" +
				"el=\"0\" Caption=\"LiborFundsMarkUp\" DataField=\"LiborFundsMarkUp\"><ValueItems /><Gr" +
				"oupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.Con" +
				"textWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Ina" +
				"ctive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Style119{}Style11" +
				"8{}Style78{AlignHorz:Near;}Style79{AlignHorz:Far;}Style85{AlignHorz:Near;}Editor" +
				"{}Style117{}Style116{}Style72{AlignHorz:Near;}Style73{AlignHorz:Far;}Style70{}St" +
				"yle71{}Style76{}Style77{}Style74{}Style75{}Style84{AlignHorz:Near;}Style87{}Styl" +
				"e86{}Style81{}Style80{}Style83{}Style82{}Style89{}Style88{}Style108{AlignHorz:Ne" +
				"ar;}Style109{AlignHorz:Near;}Style104{}Style105{}Style106{}Style107{}Style100{}S" +
				"tyle101{}Style102{AlignHorz:Near;}Style103{AlignHorz:Near;}Style94{}Style95{}Sty" +
				"le96{AlignHorz:Near;}Style97{AlignHorz:Near;}Style90{AlignHorz:Near;}Style91{Ali" +
				"gnHorz:Near;}Style92{}Style93{}RecordSelector{AlignImage:Center;}Style98{}Style9" +
				"9{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:Contro" +
				"lText;BackColor:Control;}Style18{}Style19{}Style14{AlignHorz:Near;}Style15{Font:" +
				"Verdana, 8.25pt, style=Bold;AlignHorz:Near;ForeColor:Highlight;}Style16{}Style17" +
				"{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Selected{ForeColor:Highligh" +
				"tText;BackColor:Highlight;}Style27{}Style28{}Style24{}Style9{}Style26{}Style25{}" +
				"Style29{}Style5{}Style4{}Style7{}Style6{}Style1{}Style23{}Style3{}Style22{}Style" +
				"21{AlignHorz:Near;}Style20{AlignHorz:Near;}OddRow{}Style49{AlignHorz:Near;}Style" +
				"38{}Style39{}Style36{AlignHorz:Near;}FilterBar{BackColor:SeaShell;}Style37{Align" +
				"Horz:Far;}Style34{}Style35{}Style32{}Style33{}Style30{AlignHorz:Near;}Style48{Al" +
				"ignHorz:Near;}Style31{AlignHorz:Far;}Normal{Font:Verdana, 8.25pt;}Style41{}Style" +
				"40{}Style43{AlignHorz:Near;}Style42{AlignHorz:Near;}Style45{}Style44{}Style47{}S" +
				"tyle46{}EvenRow{BackColor:Aqua;}Style8{}Style51{}Style58{}Style59{}Style2{}Style" +
				"50{}Footer{}Style52{}Style53{}Style54{AlignHorz:Near;}Style55{AlignHorz:Near;}St" +
				"yle56{}Style57{}Caption{AlignHorz:Center;}Style112{}Style69{}Style68{}Group{Back" +
				"Color:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style64{}Style63{}St" +
				"yle62{}Style61{AlignHorz:Near;}Style60{AlignHorz:Center;}Style67{AlignHorz:Cente" +
				"r;}Style66{AlignHorz:Center;}Style65{}Style115{AlignHorz:Near;}Style114{AlignHor" +
				"z:Near;}Style111{}Style110{}Style113{}</Data></Styles><Splits><C1.Win.C1TrueDBGr" +
				"id.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" Name=\"\" CaptionHeight=\"17\" Colu" +
				"mnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FilterBar=" +
				"\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"1" +
				"6\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style" +
				"2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle paren" +
				"t=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><Foo" +
				"terStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /" +
				"><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"Highlig" +
				"htRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle" +
				" parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"" +
				"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\"" +
				" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"" +
				"Style14\" /><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me" +
				"=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle paren" +
				"t=\"Style1\" me=\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Colu" +
				"mnDivider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>0</DCIdx></C1" +
				"DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style20\" /><Sty" +
				"le parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Style22\" /><E" +
				"ditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style1\" me=\"" +
				"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><ColumnDivider>DarkG" +
				"ray,Single</ColumnDivider><Height>16</Height><DCIdx>1</DCIdx></C1DisplayColumn><" +
				"C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style72\" /><Style parent=\"Styl" +
				"e1\" me=\"Style73\" /><FooterStyle parent=\"Style3\" me=\"Style74\" /><EditorStyle pare" +
				"nt=\"Style5\" me=\"Style75\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style77\" /><Gro" +
				"upFooterStyle parent=\"Style1\" me=\"Style76\" /><Visible>True</Visible><ColumnDivid" +
				"er>DarkGray,Single</ColumnDivider><Width>135</Width><Height>16</Height><DCIdx>8<" +
				"/DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Styl" +
				"e30\" /><Style parent=\"Style1\" me=\"Style31\" /><FooterStyle parent=\"Style3\" me=\"St" +
				"yle32\" /><EditorStyle parent=\"Style5\" me=\"Style33\" /><GroupHeaderStyle parent=\"S" +
				"tyle1\" me=\"Style35\" /><GroupFooterStyle parent=\"Style1\" me=\"Style34\" /><Visible>" +
				"True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>129</Width><" +
				"Height>16</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style2\" me=\"Style36\" /><Style parent=\"Style1\" me=\"Style37\" /><FooterS" +
				"tyle parent=\"Style3\" me=\"Style38\" /><EditorStyle parent=\"Style5\" me=\"Style39\" />" +
				"<GroupHeaderStyle parent=\"Style1\" me=\"Style41\" /><GroupFooterStyle parent=\"Style" +
				"1\" me=\"Style40\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Height>16</Height><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style2\" me=\"Style78\" /><Style parent=\"Style1\" me=\"Style79\" />" +
				"<FooterStyle parent=\"Style3\" me=\"Style80\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
				"le81\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style83\" /><GroupFooterStyle paren" +
				"t=\"Style1\" me=\"Style82\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single<" +
				"/ColumnDivider><Height>16</Height><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayC" +
				"olumn><HeadingStyle parent=\"Style2\" me=\"Style66\" /><Style parent=\"Style1\" me=\"St" +
				"yle67\" /><FooterStyle parent=\"Style3\" me=\"Style68\" /><EditorStyle parent=\"Style5" +
				"\" me=\"Style69\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style71\" /><GroupFooterSt" +
				"yle parent=\"Style1\" me=\"Style70\" /><Visible>True</Visible><ColumnDivider>Gainsbo" +
				"ro,Single</ColumnDivider><Width>30</Width><Height>16</Height><DCIdx>7</DCIdx></C" +
				"1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style42\" /><St" +
				"yle parent=\"Style1\" me=\"Style43\" /><FooterStyle parent=\"Style3\" me=\"Style44\" /><" +
				"EditorStyle parent=\"Style5\" me=\"Style45\" /><GroupHeaderStyle parent=\"Style1\" me=" +
				"\"Style47\" /><GroupFooterStyle parent=\"Style1\" me=\"Style46\" /><Visible>True</Visi" +
				"ble><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>190</Width><Height>16<" +
				"/Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
				"\"Style2\" me=\"Style48\" /><Style parent=\"Style1\" me=\"Style49\" /><FooterStyle paren" +
				"t=\"Style3\" me=\"Style50\" /><EditorStyle parent=\"Style5\" me=\"Style51\" /><GroupHead" +
				"erStyle parent=\"Style1\" me=\"Style53\" /><GroupFooterStyle parent=\"Style1\" me=\"Sty" +
				"le52\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>" +
				"4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle54\" /><Style parent=\"Style1\" me=\"Style55\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style56\" /><EditorStyle parent=\"Style5\" me=\"Style57\" /><GroupHeaderStyle parent=" +
				"\"Style1\" me=\"Style59\" /><GroupFooterStyle parent=\"Style1\" me=\"Style58\" /><Column" +
				"Divider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>5</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style60\" /><Style" +
				" parent=\"Style1\" me=\"Style61\" /><FooterStyle parent=\"Style3\" me=\"Style62\" /><Edi" +
				"torStyle parent=\"Style5\" me=\"Style63\" /><GroupHeaderStyle parent=\"Style1\" me=\"St" +
				"yle65\" /><GroupFooterStyle parent=\"Style1\" me=\"Style64\" /><Visible>True</Visible" +
				"><ColumnDivider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>6</DCId" +
				"x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style84\" " +
				"/><Style parent=\"Style1\" me=\"Style85\" /><FooterStyle parent=\"Style3\" me=\"Style86" +
				"\" /><EditorStyle parent=\"Style5\" me=\"Style87\" /><GroupHeaderStyle parent=\"Style1" +
				"\" me=\"Style89\" /><GroupFooterStyle parent=\"Style1\" me=\"Style88\" /><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>11</DCIdx></C1DisplayC" +
				"olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style90\" /><Style paren" +
				"t=\"Style1\" me=\"Style91\" /><FooterStyle parent=\"Style3\" me=\"Style92\" /><EditorSty" +
				"le parent=\"Style5\" me=\"Style93\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style95\"" +
				" /><GroupFooterStyle parent=\"Style1\" me=\"Style94\" /><ColumnDivider>DarkGray,Sing" +
				"le</ColumnDivider><Height>16</Height><DCIdx>12</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style2\" me=\"Style96\" /><Style parent=\"Style1\" me=" +
				"\"Style97\" /><FooterStyle parent=\"Style3\" me=\"Style98\" /><EditorStyle parent=\"Sty" +
				"le5\" me=\"Style99\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style101\" /><GroupFoot" +
				"erStyle parent=\"Style1\" me=\"Style100\" /><ColumnDivider>DarkGray,Single</ColumnDi" +
				"vider><Height>16</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style2\" me=\"Style102\" /><Style parent=\"Style1\" me=\"Style103\" " +
				"/><FooterStyle parent=\"Style3\" me=\"Style104\" /><EditorStyle parent=\"Style5\" me=\"" +
				"Style105\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style107\" /><GroupFooterStyle " +
				"parent=\"Style1\" me=\"Style106\" /><ColumnDivider>DarkGray,Single</ColumnDivider><H" +
				"eight>16</Height><DCIdx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style2\" me=\"Style108\" /><Style parent=\"Style1\" me=\"Style109\" /><Foote" +
				"rStyle parent=\"Style3\" me=\"Style110\" /><EditorStyle parent=\"Style5\" me=\"Style111" +
				"\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style113\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style112\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>16" +
				"</Height><DCIdx>15</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle paren" +
				"t=\"Style2\" me=\"Style114\" /><Style parent=\"Style1\" me=\"Style115\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style116\" /><EditorStyle parent=\"Style5\" me=\"Style117\" /><Gro" +
				"upHeaderStyle parent=\"Style1\" me=\"Style119\" /><GroupFooterStyle parent=\"Style1\" " +
				"me=\"Style118\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>16</Height" +
				"><DCIdx>16</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 992, 84</Cl" +
				"ientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><Nam" +
				"edStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><S" +
				"tyle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Styl" +
				"e parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style" +
				" parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style" +
				" parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style pare" +
				"nt=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Styl" +
				"e parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSpl" +
				"its>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSel" +
				"Width><ClientArea>0, 0, 992, 84</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"" +
				"Style28\" /><PrintPageFooterStyle parent=\"\" me=\"Style29\" /></Blob>";
			// 
			// MainMenu
			// 
			this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																						 this.TradingGroupsSendToMenuItem,
																																						 this.SendToMenuItem,
																																						 this.Seperator,
																																						 this.ExitMenuItem});
			// 
			// TradingGroupsSendToMenuItem
			// 
			this.TradingGroupsSendToMenuItem.Index = 0;
			this.TradingGroupsSendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																																this.TradingGroupsSendToEmailMenuItem,
																																																this.TradingGroupsSendToClipboardMenuItem,
																																																this.TradingGroupsSendToExcelMenuItem});
			this.TradingGroupsSendToMenuItem.Text = "Trading Groups Send To";
			// 
			// TradingGroupsSendToEmailMenuItem
			// 
			this.TradingGroupsSendToEmailMenuItem.Index = 0;
			this.TradingGroupsSendToEmailMenuItem.Text = "Email";
			this.TradingGroupsSendToEmailMenuItem.Click += new System.EventHandler(this.TradingGroupsSendToEmailMenuItem_Click);
			// 
			// TradingGroupsSendToClipboardMenuItem
			// 
			this.TradingGroupsSendToClipboardMenuItem.Index = 1;
			this.TradingGroupsSendToClipboardMenuItem.Text = "Clipboard";
			this.TradingGroupsSendToClipboardMenuItem.Click += new System.EventHandler(this.TradingGroupsSendToClipboardMenuItem_Click);
			// 
			// TradingGroupsSendToExcelMenuItem
			// 
			this.TradingGroupsSendToExcelMenuItem.Index = 2;
			this.TradingGroupsSendToExcelMenuItem.Text = "Excel";
			this.TradingGroupsSendToExcelMenuItem.Click += new System.EventHandler(this.TradingGroupsSendToExcelMenuItem_Click);
			// 
			// SendToMenuItem
			// 
			this.SendToMenuItem.Index = 1;
			this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.LocatesSendToEmailMenuItem,
																																									 this.LocatesSendToClipboardMenuItem});
			this.SendToMenuItem.Text = "Locates Send To";
			// 
			// LocatesSendToEmailMenuItem
			// 
			this.LocatesSendToEmailMenuItem.Index = 0;
			this.LocatesSendToEmailMenuItem.Text = "Email";
			this.LocatesSendToEmailMenuItem.Click += new System.EventHandler(this.SendToEmailMenuItem_Click);
			// 
			// LocatesSendToClipboardMenuItem
			// 
			this.LocatesSendToClipboardMenuItem.Index = 1;
			this.LocatesSendToClipboardMenuItem.Text = "Clipboard";
			this.LocatesSendToClipboardMenuItem.Click += new System.EventHandler(this.LocatesSendToClipboardMenuItem_Click);
			// 
			// Seperator
			// 
			this.Seperator.Index = 2;
			this.Seperator.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 3;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
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
			this.DateEditor.Location = new System.Drawing.Point(908, 8);
			this.DateEditor.Name = "DateEditor";
			this.DateEditor.Size = new System.Drawing.Size(160, 20);
			this.DateEditor.TabIndex = 4;
			this.DateEditor.Tag = null;
			this.DateEditor.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.DateEditor.TextChanged += new System.EventHandler(this.DateEditor_TextChanged);
			this.DateEditor.Validated += new System.EventHandler(this.DateEditor_Validated);
			// 
			// EffectDateLabel
			// 
			this.EffectDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EffectDateLabel.Location = new System.Drawing.Point(812, 12);
			this.EffectDateLabel.Name = "EffectDateLabel";
			this.EffectDateLabel.Size = new System.Drawing.Size(92, 16);
			this.EffectDateLabel.TabIndex = 3;
			this.EffectDateLabel.Tag = null;
			this.EffectDateLabel.Text = "In Effect For:";
			this.EffectDateLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			this.EffectDateLabel.TextDetached = true;
			// 
			// DefaultPriceLabel
			// 
			this.DefaultPriceLabel.Location = new System.Drawing.Point(88, 8);
			this.DefaultPriceLabel.Name = "DefaultPriceLabel";
			this.DefaultPriceLabel.Size = new System.Drawing.Size(88, 23);
			this.DefaultPriceLabel.TabIndex = 8;
			this.DefaultPriceLabel.Tag = null;
			this.DefaultPriceLabel.Text = "Default Price:";
			this.DefaultPriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DefaultPriceLabel.TextDetached = true;
			// 
			// DefaultAutoApprovalLabel
			// 
			this.DefaultAutoApprovalLabel.Location = new System.Drawing.Point(328, 8);
			this.DefaultAutoApprovalLabel.Name = "DefaultAutoApprovalLabel";
			this.DefaultAutoApprovalLabel.Size = new System.Drawing.Size(132, 23);
			this.DefaultAutoApprovalLabel.TabIndex = 10;
			this.DefaultAutoApprovalLabel.Tag = null;
			this.DefaultAutoApprovalLabel.Text = "Default AutoApproval:";
			this.DefaultAutoApprovalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DefaultAutoApprovalLabel.TextDetached = true;
			// 
			// DefaultPriceNumericEdit
			// 
			this.DefaultPriceNumericEdit.Location = new System.Drawing.Point(180, 9);
			this.DefaultPriceNumericEdit.Name = "DefaultPriceNumericEdit";
			this.DefaultPriceNumericEdit.Size = new System.Drawing.Size(116, 21);
			this.DefaultPriceNumericEdit.TabIndex = 11;
			this.DefaultPriceNumericEdit.Tag = null;
			this.DefaultPriceNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.DefaultPriceNumericEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DefaultPriceNumericEdit_KeyPress);
			// 
			// PremiumMinTextBox
			// 
			this.PremiumMinTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.PremiumMinTextBox.Location = new System.Drawing.Point(180, 32);
			this.PremiumMinTextBox.Name = "PremiumMinTextBox";
			this.PremiumMinTextBox.NullText = "1";
			this.PremiumMinTextBox.Size = new System.Drawing.Size(52, 21);
			this.PremiumMinTextBox.TabIndex = 7;
			this.PremiumMinTextBox.Tag = null;
			this.PremiumMinTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PremiumMinTextBox_KeyPress);
			// 
			// DefaultAutoApprovalNumericEdit
			// 
			this.DefaultAutoApprovalNumericEdit.Location = new System.Drawing.Point(464, 9);
			this.DefaultAutoApprovalNumericEdit.Name = "DefaultAutoApprovalNumericEdit";
			this.DefaultAutoApprovalNumericEdit.Size = new System.Drawing.Size(116, 21);
			this.DefaultAutoApprovalNumericEdit.TabIndex = 12;
			this.DefaultAutoApprovalNumericEdit.Tag = null;
			this.DefaultAutoApprovalNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.DefaultAutoApprovalNumericEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DefaultAutoApprovalNumericEdit_KeyPress);
			// 
			// PremiumMaxTextBox
			// 
			this.PremiumMaxTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.PremiumMaxTextBox.Location = new System.Drawing.Point(232, 32);
			this.PremiumMaxTextBox.Name = "PremiumMaxTextBox";
			this.PremiumMaxTextBox.NullText = "1";
			this.PremiumMaxTextBox.Size = new System.Drawing.Size(52, 21);
			this.PremiumMaxTextBox.TabIndex = 13;
			this.PremiumMaxTextBox.Tag = null;
			this.PremiumMaxTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PremiumMaxTextBox_KeyPress);
			// 
			// DefaultPremiumThresholdLabel
			// 
			this.DefaultPremiumThresholdLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DefaultPremiumThresholdLabel.Location = new System.Drawing.Point(4, 32);
			this.DefaultPremiumThresholdLabel.Name = "DefaultPremiumThresholdLabel";
			this.DefaultPremiumThresholdLabel.NullText = "1";
			this.DefaultPremiumThresholdLabel.Size = new System.Drawing.Size(172, 23);
			this.DefaultPremiumThresholdLabel.TabIndex = 9;
			this.DefaultPremiumThresholdLabel.Tag = null;
			this.DefaultPremiumThresholdLabel.Text = "Default Premium Threshold:";
			this.DefaultPremiumThresholdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DefaultPremiumThresholdLabel.TextDetached = true;
			// 
			// ShortSaleTradingGroupsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1076, 473);
			this.Controls.Add(this.PremiumMaxTextBox);
			this.Controls.Add(this.DefaultAutoApprovalNumericEdit);
			this.Controls.Add(this.DefaultPriceNumericEdit);
			this.Controls.Add(this.DefaultAutoApprovalLabel);
			this.Controls.Add(this.DefaultPremiumThresholdLabel);
			this.Controls.Add(this.DefaultPriceLabel);
			this.Controls.Add(this.PremiumMinTextBox);
			this.Controls.Add(this.DateEditor);
			this.Controls.Add(this.EffectDateLabel);
			this.Controls.Add(this.ShortSaleTradingGroupAttributesGrid);
			this.Controls.Add(this.ShortSaleTradingGroupsGrid);
			this.DockPadding.Bottom = 1;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 60;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ShortSaleTradingGroupsForm";
			this.Text = "ShortSale - Trading Groups";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ShortSaleTradingGroupsForm_Closing);
			this.Load += new System.EventHandler(this.ShortSaleTradingGroupsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.ShortSaleTradingGroupsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ShortSaleTradingGroupAttributesGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DateEditor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultPriceLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultAutoApprovalLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultPriceNumericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PremiumMinTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultAutoApprovalNumericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PremiumMaxTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultPremiumThresholdLabel)).EndInit();
			this.ResumeLayout(false);

		}
    #endregion

    
		public void ListLoad(string effectDate)
		{
			try
			{
				mainForm.Alert("Please wait... Loading trading groups data...", PilotState.Unknown);
				this.Cursor = Cursors.WaitCursor;
				this.Refresh();

				DefaultPriceNumericEdit.Value = mainForm.ServiceAgent.KeyValueGet("ShortSaleLocatePriceThreshold", "2.00");
				DefaultAutoApprovalNumericEdit.Value = mainForm.ServiceAgent.KeyValueGet("ShortSaleLocateAutoApprovalMax", "50000");
				
				PremiumMinTextBox.Value = mainForm.ServiceAgent.KeyValueGet("ShortSaleLocatePremiumMin", "100");
				PremiumMaxTextBox.Value = mainForm.ServiceAgent.KeyValueGet("ShortSaleLocatePremiumMax", "2500");								

				dataSet = mainForm.ShortSaleAgent.TradingGroupsGet(effectDate, mainForm.UtcOffset);								
				
				attributesRowFilter = "GroupCode = ''";
				attributesDataView = new DataView(dataSet.Tables["TradingGroups"], attributesRowFilter, "", DataViewRowState.CurrentRows);				
												
				ShortSaleTradingGroupsGrid.SetDataBinding(dataSet, "TradingGroups", true);						
				ShortSaleTradingGroupAttributesGrid.SetDataBinding(attributesDataView, "", true);								
			
				mainForm.Alert("Loading trading groups data... Done!", PilotState.Normal);
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [ShortSaleTradingGroupsForm.ListLoad]", Log.Error, 1); 
			}

			this.Cursor = Cursors.Default;
		}
		
		
		private void ShortSaleTradingGroupsForm_Load(object sender, System.EventArgs e)
    {
      int height = mainForm.Height / 2;
      int width  = mainForm.Width / 2;
      
      try
      {
        this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
        this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
        this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
        this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));
        
        DateEditor.Text = mainForm.ServiceAgent.BizDateExchange();
			}
      catch(Exception ee)
      {
        mainForm.Alert(ee.Message, PilotState.RunFault);        
      }
    }

    private void ShortSaleTradingGroupsForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if(this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
        RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
        RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
        RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
      }			
		}

    private void ShortSaleTradingGroupAttributesGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
			try
			{
				mainForm.ShortSaleAgent.TradingGroupSet(
					ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text,
					ShortSaleTradingGroupsGrid.Columns["GroupName"].Text,
					ShortSaleTradingGroupAttributesGrid.Columns["MinPrice"].Value.ToString(),
					ShortSaleTradingGroupAttributesGrid.Columns["AutoApprovalMax"].Value.ToString(),
					ShortSaleTradingGroupAttributesGrid.Columns["PremiumMin"].Value.ToString(),
					ShortSaleTradingGroupAttributesGrid.Columns["PremiumMax"].Value.ToString(),
					bool.Parse(ShortSaleTradingGroupAttributesGrid.Columns["AutoEmail"].Value.ToString()),
					ShortSaleTradingGroupAttributesGrid.Columns["EmailAddress"].Text,
					ShortSaleTradingGroupAttributesGrid.Columns["LastEmailDate"].Text,	
					mainForm.UserId);

				ShortSaleTradingGroupsGrid.Columns["ActUserId"].Text = mainForm.UserId;
				ShortSaleTradingGroupsGrid.Columns["ActTime"].Value = DateTime.Now;       							
			}
			catch(Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
				e.Cancel = true;
				return;
			}
    }

		private void FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "PremiumMin":
				case "PremiumMax":
				case "LocatesRequested":
				case "LocatesFilled":
				case "AutoApprovalMax": 
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch {}
					break;

				case "MinPrice":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.00");
					}
					catch {}
					break;

				case "LastEmailDate":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
					}
					catch {}
					break;				
				
				case "ActTime":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeFormat);
					}
					catch {}
					break;
			}		
		}

		private void ShortSaleTradingGroupsGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{
				if (!tradingGroup.Equals(ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text))
				{
					attributesRowFilter = "GroupCode = '" + ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text + "'";
					attributesDataView.RowFilter = attributesRowFilter;
					tradingGroup = ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text;
				}
			}
			catch {}
		}

		private void ShortSaleTradingGroupAttributesGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{				
				ShortSaleTradingGroupAttributesGrid.UpdateData();
				ShortSaleTradingGroupsGrid.UpdateData();
			}
		}

		private void ShortSaleTradingGroupsGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{			
			if (!bool.Parse(ShortSaleTradingGroupsGrid.Columns["IsActive"].CellValue(e.Row).ToString()))
			{
				e.CellStyle.BackColor = System.Drawing.Color.LightGray;
			}
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			DataSet dataSet = new DataSet();
			string locates = "";

			this.Cursor = Cursors.WaitCursor;
					
			try
			{
				mainForm.Alert("Getting short sale locate data for " + DateEditor.Text, PilotState.Normal);

				dataSet = mainForm.ShortSaleAgent.LocatesGet(DateEditor.Text, mainForm.UtcOffset);
			
				locates = "Locate ID" + new string(' ', 2) + "Security ID" +  new string(' ', 2) + "Client Requested" +  new string(' ', 2) + "Filled Quantity" +  new string(' ', 2) + "Fee Rate\t\n";
				locates += "---------" + new string(' ', 2) + "-----------" + new string(' ', 2) + "----------------" +  new string(' ', 2) + "---------------" +  new string(' ', 2) + "--------\t\n";
																
				foreach (DataRow dataRowLocate in dataSet.Tables["Locates"].Rows)
				{										
					if (dataRowLocate["GroupCode"].ToString().Trim().Equals(ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text.Trim()))
					{
						locates += dataRowLocate["LocateId"].ToString().PadLeft(9, ' ') + new string(' ', 2) + dataRowLocate["SecId"].ToString().PadLeft(11, ' ') + new string(' ', 2)
							+ dataRowLocate["ClientQuantity"].ToString().PadLeft(16, ' ') + new string(' ', 2) + dataRowLocate["Quantity"].ToString().PadLeft(15, ' ') + new string(' ', 2) + dataRowLocate["FeeRate"].ToString().PadLeft(8, ' ') + "\t\n";
					}
				}
				
				Email email = new Email();
				email.Send(ShortSaleTradingGroupAttributesGrid.Columns["EmailAddress"].Text, "", "Short Sale Locates, " + ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text + " , " + DateEditor.Text, locates);
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}

			this.Cursor = Cursors.Default;
		}

		private void DateEditor_TextChanged(object sender, System.EventArgs e)
		{
			if (!DateEditor.Text.Equals(""))
			{
				ListLoad(DateEditor.Text);
			}
		}

		private void DateEditor_Validated(object sender, System.EventArgs e)
		{
			if (!DateEditor.Text.Equals(""))
			{
				ListLoad(DateEditor.Text);
			}
		}

		private void DefaultPriceNumericEdit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{
				mainForm.ServiceAgent.KeyValueSet("ShortSaleLocatePriceThreshold", DefaultPriceNumericEdit.Value.ToString());
			}
		}

		private void DefaultAutoApprovalNumericEdit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{
				mainForm.ServiceAgent.KeyValueSet("ShortSaleLocateAutoApprovalMax", DefaultAutoApprovalNumericEdit.Value.ToString());
			}
		}

		private void PremiumMinTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{
				mainForm.ServiceAgent.KeyValueSet("ShortSaleLocatePremiumMin", PremiumMinTextBox.Value.ToString());
				e.Handled = true;
			}
		}

		private void PremiumMaxTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{
				mainForm.ServiceAgent.KeyValueSet("ShortSaleLocatePremiumMax", PremiumMaxTextBox.Value.ToString());
				e.Handled = true;
			}
		}

		private void LocatesSendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			DataSet dataSet = new DataSet();
			string locates = "";
			int count = 0;

			this.Cursor = Cursors.WaitCursor;
					
			try
			{
				mainForm.Alert("Getting short sale locate data for " + DateEditor.Text + " ...", PilotState.Normal);

				dataSet = mainForm.ShortSaleAgent.LocatesGet(DateEditor.Text, mainForm.UtcOffset);
			
				locates = "Locate ID" + new string(' ', 2) + "Security ID" +  new string(' ', 2) + "Client Requested" +  new string(' ', 2) + "Filled Quantity" +  new string(' ', 2) + "Fee Rate\t\n";
				locates += "---------" + new string(' ', 2) + "-----------" + new string(' ', 2) + "----------------" +  new string(' ', 2) + "---------------" +  new string(' ', 2) + "--------\t\n";
																
				foreach (DataRow dataRowLocate in dataSet.Tables["Locates"].Rows)
				{										
					if (dataRowLocate["GroupCode"].ToString().Trim().Equals(ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text.Trim()))
					{
						locates += dataRowLocate["LocateId"].ToString().PadLeft(9, ' ') + new string(' ', 2) + dataRowLocate["SecId"].ToString().PadLeft(11, ' ') + new string(' ', 2)
							+ dataRowLocate["ClientQuantity"].ToString().PadLeft(16, ' ') + new string(' ', 2) + dataRowLocate["Quantity"].ToString().PadLeft(15, ' ') + new string(' ', 2) + dataRowLocate["FeeRate"].ToString().PadLeft(8, ' ') + "\t\n";
						
						count ++;
					}
				}
				
				Clipboard.SetDataObject(locates, true);
					
				mainForm.Alert("Total: " + count + " items copied to the clipboard.", PilotState.Normal);
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}

			this.Cursor = Cursors.Default;
		}

		private void TradingGroupsSendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n";

			if (ShortSaleTradingGroupsGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows to copy.", PilotState.Normal);
				return;
			}

			try
			{
				maxTextLength = new int[ShortSaleTradingGroupsGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in ShortSaleTradingGroupsGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in ShortSaleTradingGroupsGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
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

				mainForm.Alert("Total: " + ShortSaleTradingGroupsGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
			}
			catch (Exception error)
			{       
				mainForm.Alert(error.Message, PilotState.Normal);
			}
		}

		private void TradingGroupsSendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\r\n";

			foreach (int row in ShortSaleTradingGroupsGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\r\n";
			}

			Clipboard.SetDataObject(gridData, true);

			mainForm.Alert("Total: " + ShortSaleTradingGroupsGrid.SelectedRows.Count + " items copied to the clipboard.", PilotState.Normal);
	
		}

		private void TradingGroupsSendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			Excel excel = new Excel();	
			excel.ExportGridToExcel(ref ShortSaleTradingGroupsGrid);

			this.Cursor = Cursors.Default;
		}
  }
}
