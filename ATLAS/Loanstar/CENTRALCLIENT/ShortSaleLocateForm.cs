// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class ShortSaleLocateForm : System.Windows.Forms.Form
	{ 
		const int QUANTITY = 9;
		const int SOURCE = 10;
		const int FEE_RATE = 12;
		const int PRE_BORROW = 13;

		private bool mayEdit = false;		
		private string secId = "";		
		private string groupCode = "";
		private MainForm mainForm;
	
		private ArrayList locateEventArgsArray;

		private DataSet mainDataSet = null;
		private DataSet inventoryDataSet = null;

		private DataView locatesDataView, locateSummaryDataView, tradingGroupDataView;
    
		private System.Windows.Forms.Label ClientCommentLabel;
		private System.Windows.Forms.TextBox ClientCommentText;
		private System.Windows.Forms.CheckBox EnableResearchCheck;
    
		private System.Windows.Forms.Button SubmitButton;

		private C1.Win.C1Input.C1Label TradeDateLabel;
		private C1.Win.C1List.C1Combo TradeDateCombo;
    
		private C1.Win.C1Input.C1Label CommentLabel;
		private C1.Win.C1Input.C1TextBox CommentTextBox;

		private C1.Win.C1Input.C1Label RequestLabel;
		private C1.Win.C1Input.C1TextBox RequestTextBox;

		private C1.Win.C1List.C1Combo TradingGroupCombo;
    
		private C1.Win.C1Input.C1Label SecIdLabel;
		private C1.Win.C1Input.C1TextBox SecIdTextBox;
    
		private C1.Win.C1Input.C1Label YearLabel;
		private C1.Win.C1Input.C1NumericEdit YearNumericEdit;
    
		private C1.Win.C1Input.C1Label QuarterLabel;
		private C1.Win.C1Input.C1NumericEdit QuarterNumericEdit;

		private C1.Win.C1Input.C1Label StatusLabel;
		private C1.Win.C1Input.C1TextBox StatusTextBox;

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid LocatesGrid;
    
		private C1.Win.C1List.C1List InventoryList;
		private C1.Win.C1List.C1List LocateSummaryList;

		private System.Windows.Forms.Label GroupNameLabel;
		private System.Windows.Forms.TextBox GroupNameText;

		private System.Windows.Forms.ContextMenu MainContextMenu;
    
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep1MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.Timers.Timer RefreshTimer;
		private System.Windows.Forms.CheckBox AutoUpdateCheckBox;
		private System.Windows.Forms.Button ShowPendingButton;
		private System.Windows.Forms.Button ShowAllButton;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		internal System.Windows.Forms.MenuItem AttachedMenuItem;
		private System.Windows.Forms.CheckBox AutoUpdateAllCheckBox;
		private System.Timers.Timer UpdateAllRefreshTimer;
		private System.Windows.Forms.MenuItem ActionMenuItem;
		private System.Windows.Forms.MenuItem ActionZeroLocatesMenuItem;
		private System.Windows.Forms.MenuItem ShowMenuItem;
		private System.Windows.Forms.MenuItem ShowPreBorrowsMenuItem;
		public ShortSaleLocatesPreBorrowForm shortSaleLocatesPreBorrowForm;

		private System.ComponentModel.IContainer components;

		public ShortSaleLocateForm(MainForm mainForm)
		{
			this.mainForm = mainForm;

			locateEventArgsArray = new ArrayList();

			InitializeComponent();

			try
			{
				TradeDateCombo.Tag = mainForm.ShortSaleAgent.TradeDate();

				if (mayEdit = mainForm.AdminAgent.MayEdit(mainForm.UserId, "ShortSaleLocates"))
				{
					LocatesGrid.AllowUpdate = true;
					CommentTextBox.ReadOnly = false;
					RequestTextBox.ReadOnly = false;
					TradingGroupCombo.ReadOnly = false;
				}

				RefreshTimer.Interval = double.Parse(Standard.ConfigValue("ShortSaleRefreshInterval", "30")) * 1000;
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
			}
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

		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleLocateForm));
			this.LocatesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.ActionMenuItem = new System.Windows.Forms.MenuItem();
			this.ActionZeroLocatesMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowPreBorrowsMenuItem = new System.Windows.Forms.MenuItem();
			this.AttachedMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.ClientCommentText = new System.Windows.Forms.TextBox();
			this.ClientCommentLabel = new System.Windows.Forms.Label();
			this.CommentTextBox = new C1.Win.C1Input.C1TextBox();
			this.CommentLabel = new C1.Win.C1Input.C1Label();
			this.RequestTextBox = new C1.Win.C1Input.C1TextBox();
			this.RequestLabel = new C1.Win.C1Input.C1Label();
			this.SubmitButton = new System.Windows.Forms.Button();
			this.GroupNameLabel = new System.Windows.Forms.Label();
			this.GroupNameText = new System.Windows.Forms.TextBox();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.StatusTextBox = new C1.Win.C1Input.C1TextBox();
			this.EnableResearchCheck = new System.Windows.Forms.CheckBox();
			this.SecIdTextBox = new C1.Win.C1Input.C1TextBox();
			this.SecIdLabel = new C1.Win.C1Input.C1Label();
			this.YearNumericEdit = new C1.Win.C1Input.C1NumericEdit();
			this.YearLabel = new C1.Win.C1Input.C1Label();
			this.QuarterLabel = new C1.Win.C1Input.C1Label();
			this.QuarterNumericEdit = new C1.Win.C1Input.C1NumericEdit();
			this.TradeDateLabel = new C1.Win.C1Input.C1Label();
			this.InventoryList = new C1.Win.C1List.C1List();
			this.TradeDateCombo = new C1.Win.C1List.C1Combo();
			this.TradingGroupCombo = new C1.Win.C1List.C1Combo();
			this.LocateSummaryList = new C1.Win.C1List.C1List();
			this.RefreshTimer = new System.Timers.Timer();
			this.AutoUpdateCheckBox = new System.Windows.Forms.CheckBox();
			this.ShowPendingButton = new System.Windows.Forms.Button();
			this.ShowAllButton = new System.Windows.Forms.Button();
			this.AutoUpdateAllCheckBox = new System.Windows.Forms.CheckBox();
			this.UpdateAllRefreshTimer = new System.Timers.Timer();
			((System.ComponentModel.ISupportInitialize)(this.LocatesGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CommentTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CommentLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RequestTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RequestLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SecIdTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SecIdLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.YearNumericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.YearLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.QuarterLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.QuarterNumericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TradeDateLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.InventoryList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TradeDateCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LocateSummaryList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RefreshTimer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UpdateAllRefreshTimer)).BeginInit();
			this.SuspendLayout();
			// 
			// LocatesGrid
			// 
			this.LocatesGrid.AllowColSelect = false;
			this.LocatesGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.LocatesGrid.AllowUpdate = false;
			this.LocatesGrid.AlternatingRows = true;
			this.LocatesGrid.BackColor = System.Drawing.SystemColors.ControlDark;
			this.LocatesGrid.CaptionHeight = 17;
			this.LocatesGrid.ContextMenu = this.MainContextMenu;
			this.LocatesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
			this.LocatesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LocatesGrid.EmptyRows = true;
			this.LocatesGrid.ExtendRightColumn = true;
			this.LocatesGrid.FilterBar = true;
			this.LocatesGrid.GroupByAreaVisible = false;
			this.LocatesGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.LocatesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.LocatesGrid.Location = new System.Drawing.Point(1, 55);
			this.LocatesGrid.MaintainRowCurrency = true;
			this.LocatesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.LocatesGrid.Name = "LocatesGrid";
			this.LocatesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.LocatesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.LocatesGrid.PreviewInfo.ZoomFactor = 75;
			this.LocatesGrid.RecordSelectorWidth = 16;
			this.LocatesGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.LocatesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.LocatesGrid.RowHeight = 17;
			this.LocatesGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.LocatesGrid.Size = new System.Drawing.Size(1214, 234);
			this.LocatesGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
			this.LocatesGrid.TabIndex = 1;
			this.LocatesGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.LocatesGrid_Paint);
			this.LocatesGrid.AfterUpdate += new System.EventHandler(this.LocatesGrid_AfterUpdate);
			this.LocatesGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LocatesGrid_MouseDown);
			this.LocatesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.LocatesGrid_BeforeUpdate);
			this.LocatesGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.LocatesGrid_BeforeColUpdate);
			this.LocatesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.LocatesGrid_FormatText);
			this.LocatesGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LocatesGrid_KeyPress);
			this.LocatesGrid.Error += new C1.Win.C1TrueDBGrid.ErrorEventHandler(this.LocatesGrid_Error);
			this.LocatesGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Locate ID\" " +
				"DataField=\"LocateId\" SortDirection=\"Descending\"><ValueItems /><GroupInfo /></C1D" +
				"ataColumn><C1DataColumn Level=\"0\" Caption=\"ID\" DataField=\"LocateIdTail\" NumberFo" +
				"rmat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
				"Level=\"0\" Caption=\"Security ID\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn><C1DataColumn Level=\"0\" Caption=\"Symbol\" DataField=\"Symbol\"><ValueIt" +
				"ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Open At\" Data" +
				"Field=\"OpenTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"From\" DataField=\"ClientId\"><ValueIte" +
				"ms /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Group\" DataFie" +
				"ld=\"GroupCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
				" Caption=\"Request\" DataField=\"ClientQuantity\" NumberFormat=\"FormatText Event\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Status\"" +
				" DataField=\"Status\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
				"l=\"0\" Caption=\"Located\" DataField=\"Quantity\" NumberFormat=\"FormatText Event\"><Va" +
				"lueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Source\" " +
				"DataField=\"Source\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Comment\" DataField=\"Comment\"><ValueItems /><GroupInfo /></C1DataCo" +
				"lumn><C1DataColumn Level=\"0\" Caption=\"Fee\" DataField=\"FeeRate\"><ValueItems /><Gr" +
				"oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"PB\" DataField=\"PreBorr" +
				"ow\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataCol" +
				"umn Level=\"0\" Caption=\"Done At\" DataField=\"ActTime\" NumberFormat=\"FormatText Eve" +
				"nt\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"B" +
				"y\" DataField=\"ActUserShortName\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
				"aColumn Level=\"0\" Caption=\"Client Comment\" DataField=\"ClientComment\"><ValueItems" +
				" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Group Name\" Data" +
				"Field=\"GroupName\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles t" +
				"ype=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:Hig" +
				"hlightText;BackColor:Highlight;}Inactive{ForeColor:InactiveCaptionText;BackColor" +
				":InactiveCaption;}Style119{}Style118{}Style78{}Style79{}Style85{}Editor{ForeColo" +
				"r:WindowText;}Style117{AlignHorz:Far;BackColor:White;}Style116{AlignHorz:Near;}S" +
				"tyle72{}Style73{}Style70{}Style71{}Style76{}Style77{}Style74{}Style75{}Style84{}" +
				"Style87{}Style86{}Style81{}Style80{}Style83{}Style82{}Footer{}Style89{}Style88{}" +
				"Style104{}Style105{}Style100{}Style101{}Style102{}Style103{}Style94{}Style95{}St" +
				"yle96{}Style97{}Style90{}Style91{}Style92{}Style93{}Style131{}RecordSelector{Ali" +
				"gnImage:Center;}Style98{AlignHorz:Center;}Style99{ForegroundImagePos:LeftOfText;" +
				"AlignHorz:Center;BackColor:WhiteSmoke;}Heading{Wrap:True;BackColor:Control;Borde" +
				"r:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style19{Locked:Fals" +
				"e;AlignHorz:Near;ForeColor:0, 0, 64;BackColor:Honeydew;}Style18{AlignHorz:Near;}" +
				"Style17{}Style14{AlignHorz:Near;}Style15{Locked:False;AlignHorz:Center;ForeColor" +
				":Black;BackColor:WhiteSmoke;}Style133{}Style132{}Style16{}Style130{}Style10{Alig" +
				"nHorz:Near;}Style11{}Style12{}Style13{}Style126{}Style127{}Style124{}Style120{}S" +
				"tyle121{}Style29{}Style128{AlignHorz:Near;}Style129{AlignHorz:Center;BackColor:I" +
				"vory;}Style28{}Style27{Locked:False;AlignHorz:Far;ForeColor:Black;BackColor:Ivor" +
				"y;}Style26{AlignHorz:Center;}Style125{}Style122{AlignHorz:Near;}Style123{AlignHo" +
				"rz:Near;}Style25{}Style24{}Style23{Locked:False;AlignHorz:Near;ForeColor:0, 0, 6" +
				"4;BackColor:Honeydew;}Style22{AlignHorz:Near;}Style21{}Style20{}OddRow{}Style38{" +
				"AlignHorz:Center;}Style39{Locked:False;AlignHorz:Far;ForeColor:Black;BackColor:I" +
				"vory;}Style36{}FilterBar{BackColor:SeaShell;}Style37{}Style34{AlignHorz:Near;}St" +
				"yle35{Locked:False;AlignHorz:Near;ForeColor:0, 0, 64;BackColor:Ivory;}Style32{}S" +
				"tyle33{}Style49{}Style48{}Style30{AlignHorz:Near;}Style31{AlignHorz:Near;}Normal" +
				"{Font:Verdana, 8.25pt;}Style41{}Style40{}Style43{Locked:False;AlignHorz:Far;Fore" +
				"Color:Black;BackColor:White;}Style42{AlignHorz:Center;}Style45{}Style44{}Style47" +
				"{Locked:False;AlignHorz:Near;ForeColor:0, 0, 64;BackColor:White;}Style46{AlignHo" +
				"rz:Near;}Selected{ForeColor:HighlightText;BackColor:Highlight;}EvenRow{BackColor" +
				":235, 235, 255;}Style9{}Style8{}Style5{}Style4{}Style7{}Style6{}Style58{}Style59" +
				"{}Style3{}Style2{}Style50{AlignHorz:Center;}Style51{AlignHorz:Far;BackColor:Whit" +
				"eSmoke;}Style52{}Style53{}Style54{AlignHorz:Near;}Style55{AlignHorz:Near;}Style5" +
				"6{}Style57{}Caption{AlignHorz:Center;}Style64{}Style112{}Style69{}Style68{}Group" +
				"{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style1{}Style63" +
				"{AlignHorz:Near;BackColor:White;}Style62{AlignHorz:Near;}Style61{}Style60{}Style" +
				"67{AlignHorz:Near;ForeColor:0, 0, 64;BackColor:WhiteSmoke;}Style66{AlignHorz:Nea" +
				"r;}Style65{}Style115{}Style114{}Style111{AlignHorz:Center;AlignVert:Center;BackC" +
				"olor:White;}Style110{AlignHorz:Center;}Style113{}</Data></Styles><Splits><C1.Win" +
				".C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSelect=\"Fals" +
				"e\" Name=\"\" AllowRowSizing=\"None\" AlternatingRowStyle=\"True\" CaptionHeight=\"17\" C" +
				"olumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FilterB" +
				"ar=\"True\" MarqueeStyle=\"SolidCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth" +
				"=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"St" +
				"yle2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle pa" +
				"rent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><" +
				"FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12" +
				"\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"High" +
				"lightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowSt" +
				"yle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" m" +
				"e=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Norm" +
				"al\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
				"e=\"Style30\" /><Style parent=\"Style1\" me=\"Style31\" /><FooterStyle parent=\"Style3\"" +
				" me=\"Style32\" /><EditorStyle parent=\"Style5\" me=\"Style33\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=\"Style80\" /><C" +
				"olumnDivider>Maroon,Single</ColumnDivider><Width>75</Width><Height>15</Height><D" +
				"CIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
				"e=\"Style14\" /><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\"" +
				" me=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style71\" /><GroupFooterStyle parent=\"Style1\" me=\"Style70\" /><V" +
				"isible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>55</Wi" +
				"dth><Height>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx" +
				">1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle18\" /><Style parent=\"Style1\" me=\"Style19\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style20\" /><EditorStyle parent=\"Style5\" me=\"Style21\" /><GroupHeaderStyle parent" +
				"=\"Style1\" me=\"Style73\" /><GroupFooterStyle parent=\"Style1\" me=\"Style72\" /><Visib" +
				"le>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>85</Width" +
				"><Height>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx>2<" +
				"/DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Styl" +
				"e22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"St" +
				"yle24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"S" +
				"tyle1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Visible>" +
				"True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>55</Width><He" +
				"ight>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx>3</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style26\"" +
				" /><Style parent=\"Style1\" me=\"Style27\" /><FooterStyle parent=\"Style3\" me=\"Style2" +
				"8\" /><EditorStyle parent=\"Style5\" me=\"Style29\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style77\" /><GroupFooterStyle parent=\"Style1\" me=\"Style76\" /><Visible>True" +
				"</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>65</Width><Heigh" +
				"t>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx>4</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" />" +
				"<Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" " +
				"/><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" " +
				"me=\"Style79\" /><GroupFooterStyle parent=\"Style1\" me=\"Style78\" /><Visible>True</V" +
				"isible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>85</Width><Height>1" +
				"5</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx>5</DCIdx></C" +
				"1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style128\" /><S" +
				"tyle parent=\"Style1\" me=\"Style129\" /><FooterStyle parent=\"Style3\" me=\"Style130\" " +
				"/><EditorStyle parent=\"Style5\" me=\"Style131\" /><GroupHeaderStyle parent=\"Style1\"" +
				" me=\"Style133\" /><GroupFooterStyle parent=\"Style1\" me=\"Style132\" /><Visible>True" +
				"</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>50</Width><Heigh" +
				"t>18</Height><AllowFocus>False</AllowFocus><DCIdx>6</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style38\" /><Style parent=\"Style1" +
				"\" me=\"Style39\" /><FooterStyle parent=\"Style3\" me=\"Style40\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style41\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style83\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style82\" /><Visible>True</Visible><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Width>75</Width><Height>15</Height><AllowFocus>" +
				"False</AllowFocus><Locked>True</Locked><DCIdx>7</DCIdx></C1DisplayColumn><C1Disp" +
				"layColumn><HeadingStyle parent=\"Style2\" me=\"Style98\" /><Style parent=\"Style1\" me" +
				"=\"Style99\" /><FooterStyle parent=\"Style3\" me=\"Style100\" /><EditorStyle parent=\"S" +
				"tyle5\" me=\"Style101\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style103\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style102\" /><Visible>True</Visible><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Width>65</Width><Height>15</Height><AllowFocus>" +
				"False</AllowFocus><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style2\" me=\"Style42\" /><Style parent=\"Style1\" me=\"Style43\" /><FooterS" +
				"tyle parent=\"Style3\" me=\"Style44\" /><EditorStyle parent=\"Style5\" me=\"Style45\" />" +
				"<GroupHeaderStyle parent=\"Style1\" me=\"Style85\" /><GroupFooterStyle parent=\"Style" +
				"1\" me=\"Style84\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Column" +
				"Divider><Width>75</Width><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn><C" +
				"1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style62\" /><Style parent=\"Style" +
				"1\" me=\"Style63\" /><FooterStyle parent=\"Style3\" me=\"Style64\" /><EditorStyle paren" +
				"t=\"Style5\" me=\"Style65\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style97\" /><Grou" +
				"pFooterStyle parent=\"Style1\" me=\"Style96\" /><Visible>True</Visible><ColumnDivide" +
				"r>Gainsboro,Single</ColumnDivider><Height>15</Height><DCIdx>10</DCIdx></C1Displa" +
				"yColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style par" +
				"ent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorS" +
				"tyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style8" +
				"7\" /><GroupFooterStyle parent=\"Style1\" me=\"Style86\" /><Visible>True</Visible><Co" +
				"lumnDivider>Gainsboro,Single</ColumnDivider><Width>150</Width><Height>15</Height" +
				"><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
				"2\" me=\"Style116\" /><Style parent=\"Style1\" me=\"Style117\" /><FooterStyle parent=\"S" +
				"tyle3\" me=\"Style118\" /><EditorStyle parent=\"Style5\" me=\"Style119\" /><GroupHeader" +
				"Style parent=\"Style1\" me=\"Style121\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e120\" /><Visible>True</Visible><ColumnDivider>Gainsboro,None</ColumnDivider><Wid" +
				"th>40</Width><Height>15</Height><HeaderDivider>False</HeaderDivider><DCIdx>12</D" +
				"CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style1" +
				"10\" /><Style parent=\"Style1\" me=\"Style111\" /><FooterStyle parent=\"Style3\" me=\"St" +
				"yle112\" /><EditorStyle parent=\"Style5\" me=\"Style113\" /><GroupHeaderStyle parent=" +
				"\"Style1\" me=\"Style115\" /><GroupFooterStyle parent=\"Style1\" me=\"Style114\" /><Visi" +
				"ble>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>25</Width" +
				"><Height>15</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1DisplayColumn><Heading" +
				"Style parent=\"Style2\" me=\"Style50\" /><Style parent=\"Style1\" me=\"Style51\" /><Foot" +
				"erStyle parent=\"Style3\" me=\"Style52\" /><EditorStyle parent=\"Style5\" me=\"Style53\"" +
				" /><GroupHeaderStyle parent=\"Style1\" me=\"Style89\" /><GroupFooterStyle parent=\"St" +
				"yle1\" me=\"Style88\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Col" +
				"umnDivider><Width>65</Width><Height>18</Height><AllowFocus>False</AllowFocus><DC" +
				"Idx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
				"e=\"Style66\" /><Style parent=\"Style1\" me=\"Style67\" /><FooterStyle parent=\"Style3\"" +
				" me=\"Style68\" /><EditorStyle parent=\"Style5\" me=\"Style69\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style91\" /><GroupFooterStyle parent=\"Style1\" me=\"Style90\" /><V" +
				"isible>True</Visible><ColumnDivider>DarkGray,None</ColumnDivider><Width>85</Widt" +
				"h><Height>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx>1" +
				"5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle54\" /><Style parent=\"Style1\" me=\"Style55\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style56\" /><EditorStyle parent=\"Style5\" me=\"Style57\" /><GroupHeaderStyle parent=" +
				"\"Style1\" me=\"Style59\" /><GroupFooterStyle parent=\"Style1\" me=\"Style58\" /><Column" +
				"Divider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>16</DCIdx></C1D" +
				"isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style122\" /><Sty" +
				"le parent=\"Style1\" me=\"Style123\" /><FooterStyle parent=\"Style3\" me=\"Style124\" />" +
				"<EditorStyle parent=\"Style5\" me=\"Style125\" /><GroupHeaderStyle parent=\"Style1\" m" +
				"e=\"Style127\" /><GroupFooterStyle parent=\"Style1\" me=\"Style126\" /><ColumnDivider>" +
				"DarkGray,Single</ColumnDivider><Height>18</Height><DCIdx>17</DCIdx></C1DisplayCo" +
				"lumn></internalCols><ClientRect>0, 0, 1210, 230</ClientRect><BorderSide>0</Borde" +
				"rSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=" +
				"\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Foo" +
				"ter\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inacti" +
				"ve\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" " +
				"/><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\"" +
				" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelect" +
				"or\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\"" +
				" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Mod" +
				"ified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 1210," +
				" 230</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style104\" /><PrintPageFoote" +
				"rStyle parent=\"\" me=\"Style105\" /></Blob>";
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.ActionMenuItem,
																																										this.SendToMenuItem,
																																										this.ShowMenuItem,
																																										this.AttachedMenuItem,
																																										this.Sep1MenuItem,
																																										this.ExitMenuItem});
			// 
			// ActionMenuItem
			// 
			this.ActionMenuItem.Index = 0;
			this.ActionMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.ActionZeroLocatesMenuItem});
			this.ActionMenuItem.Text = "Action";
			// 
			// ActionZeroLocatesMenuItem
			// 
			this.ActionZeroLocatesMenuItem.Index = 0;
			this.ActionZeroLocatesMenuItem.Text = "Zero Locate(s)";
			this.ActionZeroLocatesMenuItem.Click += new System.EventHandler(this.ActionZeroLocatesMenuItem_Click);
			// 
			// SendToMenuItem
			// 
			this.SendToMenuItem.Index = 1;
			this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.SendToExcelMenuItem,
																																									 this.SendToEmailMenuItem});
			this.SendToMenuItem.Text = "Send To";
			// 
			// SendToExcelMenuItem
			// 
			this.SendToExcelMenuItem.Index = 0;
			this.SendToExcelMenuItem.Text = "Excel";
			this.SendToExcelMenuItem.Click += new System.EventHandler(this.SendToExcelMenuItem_Click);
			// 
			// SendToEmailMenuItem
			// 
			this.SendToEmailMenuItem.Index = 1;
			this.SendToEmailMenuItem.Text = "Mail Recipient";
			this.SendToEmailMenuItem.Click += new System.EventHandler(this.SendToEmailMenuItem_Click);
			// 
			// ShowMenuItem
			// 
			this.ShowMenuItem.Index = 2;
			this.ShowMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.ShowPreBorrowsMenuItem});
			this.ShowMenuItem.Text = "Show";
			// 
			// ShowPreBorrowsMenuItem
			// 
			this.ShowPreBorrowsMenuItem.Index = 0;
			this.ShowPreBorrowsMenuItem.Text = "PreBorrows";
			this.ShowPreBorrowsMenuItem.Click += new System.EventHandler(this.ShowPreBorrowsMenuItem_Click);
			// 
			// AttachedMenuItem
			// 
			this.AttachedMenuItem.Index = 3;
			this.AttachedMenuItem.Text = "Attach";
			this.AttachedMenuItem.Click += new System.EventHandler(this.AttachMenuItem_Click);
			// 
			// Sep1MenuItem
			// 
			this.Sep1MenuItem.Index = 4;
			this.Sep1MenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 5;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// ClientCommentText
			// 
			this.ClientCommentText.BackColor = System.Drawing.SystemColors.Control;
			this.ClientCommentText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ClientCommentText.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ClientCommentText.ForeColor = System.Drawing.Color.MidnightBlue;
			this.ClientCommentText.Location = new System.Drawing.Point(344, 10);
			this.ClientCommentText.Name = "ClientCommentText";
			this.ClientCommentText.ReadOnly = true;
			this.ClientCommentText.Size = new System.Drawing.Size(424, 16);
			this.ClientCommentText.TabIndex = 13;
			this.ClientCommentText.TabStop = false;
			this.ClientCommentText.Text = "";
			// 
			// ClientCommentLabel
			// 
			this.ClientCommentLabel.Location = new System.Drawing.Point(234, 10);
			this.ClientCommentLabel.Name = "ClientCommentLabel";
			this.ClientCommentLabel.Size = new System.Drawing.Size(104, 16);
			this.ClientCommentLabel.TabIndex = 12;
			this.ClientCommentLabel.Text = "Client Comment:";
			this.ClientCommentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// CommentTextBox
			// 
			this.CommentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CommentTextBox.Label = this.CommentLabel;
			this.CommentTextBox.Location = new System.Drawing.Point(584, 304);
			this.CommentTextBox.Name = "CommentTextBox";
			this.CommentTextBox.ReadOnly = true;
			this.CommentTextBox.Size = new System.Drawing.Size(288, 21);
			this.CommentTextBox.TabIndex = 3;
			this.CommentTextBox.Tag = null;
			this.CommentTextBox.TextDetached = true;
			this.CommentTextBox.TextChanged += new System.EventHandler(this.CommentText_TextChanged);
			// 
			// CommentLabel
			// 
			this.CommentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CommentLabel.Location = new System.Drawing.Point(512, 301);
			this.CommentLabel.Name = "CommentLabel";
			this.CommentLabel.Size = new System.Drawing.Size(72, 24);
			this.CommentLabel.TabIndex = 17;
			this.CommentLabel.Tag = null;
			this.CommentLabel.Text = "Comment:";
			this.CommentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CommentLabel.TextDetached = true;
			// 
			// RequestTextBox
			// 
			this.RequestTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.RequestTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.RequestTextBox.DateTimeInput = false;
			this.RequestTextBox.Label = this.RequestLabel;
			this.RequestTextBox.Location = new System.Drawing.Point(656, 336);
			this.RequestTextBox.Multiline = true;
			this.RequestTextBox.Name = "RequestTextBox";
			this.RequestTextBox.ReadOnly = true;
			this.RequestTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.RequestTextBox.Size = new System.Drawing.Size(216, 124);
			this.RequestTextBox.TabIndex = 4;
			this.RequestTextBox.Tag = null;
			this.RequestTextBox.TextDetached = true;
			this.RequestTextBox.TextChanged += new System.EventHandler(this.RequestText_TextChanged);
			// 
			// RequestLabel
			// 
			this.RequestLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.RequestLabel.Location = new System.Drawing.Point(544, 333);
			this.RequestLabel.Name = "RequestLabel";
			this.RequestLabel.Size = new System.Drawing.Size(112, 24);
			this.RequestLabel.TabIndex = 18;
			this.RequestLabel.Tag = null;
			this.RequestLabel.Text = "Request Item List:";
			this.RequestLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.RequestLabel.TextDetached = true;
			// 
			// SubmitButton
			// 
			this.SubmitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SubmitButton.Enabled = false;
			this.SubmitButton.Location = new System.Drawing.Point(528, 376);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(96, 24);
			this.SubmitButton.TabIndex = 8;
			this.SubmitButton.Text = "SUBMIT ";
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			// 
			// GroupNameLabel
			// 
			this.GroupNameLabel.Location = new System.Drawing.Point(234, 28);
			this.GroupNameLabel.Name = "GroupNameLabel";
			this.GroupNameLabel.Size = new System.Drawing.Size(104, 16);
			this.GroupNameLabel.TabIndex = 14;
			this.GroupNameLabel.Text = "Group Name:";
			this.GroupNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// GroupNameText
			// 
			this.GroupNameText.BackColor = System.Drawing.SystemColors.Control;
			this.GroupNameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.GroupNameText.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GroupNameText.ForeColor = System.Drawing.Color.MidnightBlue;
			this.GroupNameText.Location = new System.Drawing.Point(344, 28);
			this.GroupNameText.Name = "GroupNameText";
			this.GroupNameText.ReadOnly = true;
			this.GroupNameText.Size = new System.Drawing.Size(424, 16);
			this.GroupNameText.TabIndex = 15;
			this.GroupNameText.TabStop = false;
			this.GroupNameText.Text = "";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StatusLabel.Location = new System.Drawing.Point(512, 468);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(48, 16);
			this.StatusLabel.TabIndex = 19;
			this.StatusLabel.Tag = null;
			this.StatusLabel.Text = "Status:";
			this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.StatusLabel.TextDetached = true;
			// 
			// StatusTextBox
			// 
			this.StatusTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StatusTextBox.AutoSize = false;
			this.StatusTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.StatusTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.StatusTextBox.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.StatusTextBox.ForeColor = System.Drawing.Color.Maroon;
			this.StatusTextBox.Label = this.StatusLabel;
			this.StatusTextBox.Location = new System.Drawing.Point(568, 468);
			this.StatusTextBox.Multiline = true;
			this.StatusTextBox.Name = "StatusTextBox";
			this.StatusTextBox.Size = new System.Drawing.Size(304, 36);
			this.StatusTextBox.TabIndex = 20;
			this.StatusTextBox.TabStop = false;
			this.StatusTextBox.Tag = null;
			this.StatusTextBox.TextDetached = true;
			// 
			// EnableResearchCheck
			// 
			this.EnableResearchCheck.Location = new System.Drawing.Point(1088, 8);
			this.EnableResearchCheck.Name = "EnableResearchCheck";
			this.EnableResearchCheck.Size = new System.Drawing.Size(128, 16);
			this.EnableResearchCheck.TabIndex = 10;
			this.EnableResearchCheck.Text = "Enable Research";
			this.EnableResearchCheck.CheckedChanged += new System.EventHandler(this.EnableResearchCheck_CheckedChanged);
			// 
			// SecIdTextBox
			// 
			this.SecIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SecIdTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.SecIdTextBox.Label = this.SecIdLabel;
			this.SecIdTextBox.Location = new System.Drawing.Point(760, 440);
			this.SecIdTextBox.Name = "SecIdTextBox";
			this.SecIdTextBox.Size = new System.Drawing.Size(104, 21);
			this.SecIdTextBox.TabIndex = 7;
			this.SecIdTextBox.Tag = null;
			this.SecIdTextBox.TextDetached = true;
			this.SecIdTextBox.Visible = false;
			this.SecIdTextBox.TextChanged += new System.EventHandler(this.SecIdTextBox_TextChanged);
			// 
			// SecIdLabel
			// 
			this.SecIdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SecIdLabel.Location = new System.Drawing.Point(672, 440);
			this.SecIdLabel.Name = "SecIdLabel";
			this.SecIdLabel.Size = new System.Drawing.Size(80, 24);
			this.SecIdLabel.TabIndex = 21;
			this.SecIdLabel.Tag = null;
			this.SecIdLabel.Text = "Security ID:";
			this.SecIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.SecIdLabel.TextDetached = true;
			this.SecIdLabel.Visible = false;
			// 
			// YearNumericEdit
			// 
			this.YearNumericEdit.AllowDbNull = false;
			this.YearNumericEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.YearNumericEdit.DataType = typeof(int);
			this.YearNumericEdit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.YearNumericEdit.FormatType = C1.Win.C1Input.FormatTypeEnum.Integer;
			this.YearNumericEdit.Label = this.YearLabel;
			this.YearNumericEdit.Location = new System.Drawing.Point(760, 408);
			this.YearNumericEdit.MaxLength = 4;
			this.YearNumericEdit.Name = "YearNumericEdit";
			this.YearNumericEdit.ShowContextMenu = false;
			this.YearNumericEdit.Size = new System.Drawing.Size(64, 21);
			this.YearNumericEdit.TabIndex = 6;
			this.YearNumericEdit.Tag = null;
			this.YearNumericEdit.TrimStart = true;
			this.YearNumericEdit.Value = 2005;
			this.YearNumericEdit.Visible = false;
			this.YearNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
			// 
			// YearLabel
			// 
			this.YearLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.YearLabel.Location = new System.Drawing.Point(672, 408);
			this.YearLabel.Name = "YearLabel";
			this.YearLabel.Size = new System.Drawing.Size(80, 24);
			this.YearLabel.TabIndex = 22;
			this.YearLabel.Tag = null;
			this.YearLabel.Text = "Year:";
			this.YearLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.YearLabel.TextDetached = true;
			this.YearLabel.Visible = false;
			// 
			// QuarterLabel
			// 
			this.QuarterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.QuarterLabel.Location = new System.Drawing.Point(672, 376);
			this.QuarterLabel.Name = "QuarterLabel";
			this.QuarterLabel.Size = new System.Drawing.Size(80, 24);
			this.QuarterLabel.TabIndex = 23;
			this.QuarterLabel.Tag = null;
			this.QuarterLabel.Text = "Quarter:";
			this.QuarterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.QuarterLabel.TextDetached = true;
			this.QuarterLabel.Visible = false;
			// 
			// QuarterNumericEdit
			// 
			this.QuarterNumericEdit.AllowDbNull = false;
			this.QuarterNumericEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.QuarterNumericEdit.DataType = typeof(short);
			this.QuarterNumericEdit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.QuarterNumericEdit.FormatType = C1.Win.C1Input.FormatTypeEnum.Integer;
			this.QuarterNumericEdit.Label = this.QuarterLabel;
			this.QuarterNumericEdit.Location = new System.Drawing.Point(760, 376);
			this.QuarterNumericEdit.MaxLength = 1;
			this.QuarterNumericEdit.Name = "QuarterNumericEdit";
			this.QuarterNumericEdit.ShowContextMenu = false;
			this.QuarterNumericEdit.Size = new System.Drawing.Size(40, 21);
			this.QuarterNumericEdit.TabIndex = 5;
			this.QuarterNumericEdit.Tag = null;
			this.QuarterNumericEdit.TrimStart = true;
			this.QuarterNumericEdit.Value = ((short)(1));
			this.QuarterNumericEdit.Visible = false;
			this.QuarterNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
			// 
			// TradeDateLabel
			// 
			this.TradeDateLabel.Location = new System.Drawing.Point(8, 20);
			this.TradeDateLabel.Name = "TradeDateLabel";
			this.TradeDateLabel.Size = new System.Drawing.Size(96, 16);
			this.TradeDateLabel.TabIndex = 11;
			this.TradeDateLabel.Tag = null;
			this.TradeDateLabel.Text = "For Trade Date:";
			this.TradeDateLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			this.TradeDateLabel.TextDetached = true;
			// 
			// InventoryList
			// 
			this.InventoryList.AddItemSeparator = ';';
			this.InventoryList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.InventoryList.BackColor = System.Drawing.Color.Honeydew;
			this.InventoryList.Caption = "Available Inventory";
			this.InventoryList.CaptionHeight = 17;
			this.InventoryList.ColumnCaptionHeight = 17;
			this.InventoryList.ColumnFooterHeight = 17;
			this.InventoryList.DeadAreaBackColor = System.Drawing.Color.DarkGray;
			this.InventoryList.EmptyRows = true;
			this.InventoryList.ExtendRightColumn = true;
			this.InventoryList.FetchRowStyles = true;
			this.InventoryList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.InventoryList.ItemHeight = 15;
			this.InventoryList.Location = new System.Drawing.Point(16, 304);
			this.InventoryList.MatchEntryTimeout = ((long)(2000));
			this.InventoryList.Name = "InventoryList";
			this.InventoryList.PartialRightColumn = false;
			this.InventoryList.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.InventoryList.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.InventoryList.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.InventoryList.Size = new System.Drawing.Size(472, 190);
			this.InventoryList.TabIndex = 24;
			this.InventoryList.TabStop = false;
			this.InventoryList.FetchRowStyle += new C1.Win.C1List.FetchRowStyleEventHandler(this.InventoryList_FetchRowStyle);
			this.InventoryList.FormatText += new C1.Win.C1List.FormatTextEventHandler(this.InventoryList_FormatText);
			this.InventoryList.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
				"\" DataField=\"SecId\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" Caption" +
				"=\"Received\" DataField=\"ScribeTime\" NumberFormat=\"FormatText Event\"><ValueItems /" +
				"></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"For\" DataField=\"BizDate\" Number" +
				"Format=\"FormatText Event\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" C" +
				"aption=\"Desk\" DataField=\"Desk\"><ValueItems /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"Book\" DataField=\"Account\"><ValueItems /></C1DataColumn><C1DataColum" +
				"n Level=\"0\" Caption=\"\" DataField=\"ModeCode\"><ValueItems /></C1DataColumn><C1Data" +
				"Column Level=\"0\" Caption=\"Quantity\" DataField=\"Quantity\" NumberFormat=\"FormatTex" +
				"t Event\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Des" +
				"ign.ContextWrapper\"><Data>Caption{AlignHorz:Center;}Style27{AlignHorz:Near;}Norm" +
				"al{Font:Verdana, 8.25pt;BackColor:Honeydew;}Style25{AlignHorz:Near;}Selected{For" +
				"eColor:HighlightText;BackColor:Highlight;}Style18{AlignHorz:Near;}Style19{AlignH" +
				"orz:Near;}Style14{}Style15{AlignHorz:Near;}Style16{AlignHorz:Near;}Style17{}Styl" +
				"e10{}Style11{}OddRow{}Style13{AlignHorz:Near;}Style12{AlignHorz:Near;}Style32{}S" +
				"tyle31{AlignHorz:Far;}Footer{}Style29{}Style28{AlignHorz:Center;}HighlightRow{Fo" +
				"reColor:HighlightText;BackColor:Highlight;}Style26{}RecordSelector{AlignImage:Ce" +
				"nter;}Style24{AlignHorz:Near;}Style23{}Style22{AlignHorz:Near;}Style21{AlignHorz" +
				":Near;}Style20{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Ce" +
				"nter;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}EvenRow{" +
				"BackColor:Aqua;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;For" +
				"eColor:ControlText;BackColor:Control;}Style9{AlignHorz:Near;}Style8{}Style5{}Sty" +
				"le4{}Style7{}Style6{}Style1{}Style30{AlignHorz:Near;}Style3{}Style2{}</Data></St" +
				"yles><Splits><C1.Win.C1List.ListBoxView Name=\"\" CaptionHeight=\"17\" ColumnCaption" +
				"Height=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FetchRowStyles=\"Tru" +
				"e\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 17, 468, 169" +
				"</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style14\" /><Visible>False</Visible><ColumnDivider><Color>DarkGray</Color><Style>" +
				"Single</Style></ColumnDivider><Width>95</Width><Height>15</Height><DCIdx>0</DCId" +
				"x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style15\" " +
				"/><Style parent=\"Style1\" me=\"Style16\" /><FooterStyle parent=\"Style3\" me=\"Style17" +
				"\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><" +
				"Width>110</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1Display" +
				"Column><HeadingStyle parent=\"Style2\" me=\"Style18\" /><Style parent=\"Style1\" me=\"S" +
				"tyle19\" /><FooterStyle parent=\"Style3\" me=\"Style20\" /><ColumnDivider><Color>Dark" +
				"Gray</Color><Style>Single</Style></ColumnDivider><Width>75</Width><Height>15</He" +
				"ight><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
				"yle2\" me=\"Style21\" /><Style parent=\"Style1\" me=\"Style22\" /><FooterStyle parent=\"" +
				"Style3\" me=\"Style23\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Styl" +
				"e></ColumnDivider><Width>85</Width><Height>15</Height><DCIdx>3</DCIdx></C1Displa" +
				"yColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style24\" /><Style par" +
				"ent=\"Style1\" me=\"Style25\" /><FooterStyle parent=\"Style3\" me=\"Style26\" /><ColumnD" +
				"ivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>60</Wi" +
				"dth><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style27\" /><Style parent=\"Style1\" me=\"Style28\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style29\" /><ColumnDivider><Color>DarkGray</Color><" +
				"Style>None</Style></ColumnDivider><Width>20</Width><Height>15</Height><HeaderDiv" +
				"ider>False</HeaderDivider><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style2\" me=\"Style30\" /><Style parent=\"Style1\" me=\"Style31\" />" +
				"<FooterStyle parent=\"Style3\" me=\"Style32\" /><ColumnDivider><Color>DarkGray</Colo" +
				"r><Style>Single</Style></ColumnDivider><Width>95</Width><Height>15</Height><DCId" +
				"x>6</DCIdx></C1DisplayColumn></internalCols><VScrollBar><Width>16</Width><Style>" +
				"Always</Style></VScrollBar><HScrollBar><Height>16</Height><Style>None</Style></H" +
				"ScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"Even" +
				"Row\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent" +
				"=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightR" +
				"owStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=" +
				"\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle paren" +
				"t=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /" +
				"><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><Name" +
				"dStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><St" +
				"yle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style" +
				" parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style " +
				"parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style" +
				" parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><St" +
				"yle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzS" +
				"plits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecS" +
				"elWidth></Blob>";
			// 
			// TradeDateCombo
			// 
			this.TradeDateCombo.AddItemSeparator = ';';
			this.TradeDateCombo.AllowColMove = false;
			this.TradeDateCombo.AutoCompletion = true;
			this.TradeDateCombo.AutoSize = false;
			this.TradeDateCombo.Caption = "";
			this.TradeDateCombo.CaptionHeight = 17;
			this.TradeDateCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.TradeDateCombo.ColumnCaptionHeight = 17;
			this.TradeDateCombo.ColumnFooterHeight = 17;
			this.TradeDateCombo.ColumnHeaders = false;
			this.TradeDateCombo.ColumnWidth = 100;
			this.TradeDateCombo.ContentHeight = 14;
			this.TradeDateCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.TradeDateCombo.EditorBackColor = System.Drawing.SystemColors.Window;
			this.TradeDateCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TradeDateCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.TradeDateCombo.EditorHeight = 14;
			this.TradeDateCombo.Enabled = false;
			this.TradeDateCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TradeDateCombo.GapHeight = 2;
			this.TradeDateCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.TradeDateCombo.ItemHeight = 15;
			this.TradeDateCombo.Location = new System.Drawing.Point(112, 19);
			this.TradeDateCombo.MatchEntryTimeout = ((long)(2000));
			this.TradeDateCombo.MaxDropDownItems = ((short)(5));
			this.TradeDateCombo.MaxLength = 32767;
			this.TradeDateCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.TradeDateCombo.Name = "TradeDateCombo";
			this.TradeDateCombo.PartialRightColumn = false;
			this.TradeDateCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.TradeDateCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.TradeDateCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.TradeDateCombo.Size = new System.Drawing.Size(104, 20);
			this.TradeDateCombo.TabIndex = 25;
			this.TradeDateCombo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.TradeDateCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TradeDateCombo_KeyPress);
			this.TradeDateCombo.RowChange += new System.EventHandler(this.TradeDateCombo_RowChange);
			this.TradeDateCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Trade Dates" +
				"\" DataField=\"TradeDate\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1" +
				".Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, " +
				"0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}S" +
				"tyle7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColo" +
				"r:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCa" +
				"ption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;BackColor:W" +
				"indow;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{" +
				"}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Ra" +
				"ised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style1" +
				"1{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Spli" +
				"ts><C1.Win.C1List.ListBoxView AllowColMove=\"False\" AllowColSelect=\"False\" Name=\"" +
				"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalSc" +
				"rollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><" +
				"internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Styl" +
				"e parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><Co" +
				"lumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>" +
				"15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><VScrollBar><Width>1" +
				"6</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle " +
				"parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><Foot" +
				"erStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" />" +
				"<HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"Highligh" +
				"tRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle " +
				"parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"S" +
				"tyle10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" " +
				"me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\"" +
				" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=" +
				"\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"In" +
				"active\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"High" +
				"lightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"Odd" +
				"Row\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=" +
				"\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Lay" +
				"out>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			// 
			// TradingGroupCombo
			// 
			this.TradingGroupCombo.AddItemSeparator = ';';
			this.TradingGroupCombo.AllowColMove = false;
			this.TradingGroupCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.TradingGroupCombo.AutoCompletion = true;
			this.TradingGroupCombo.AutoDropDown = true;
			this.TradingGroupCombo.AutoSize = false;
			this.TradingGroupCombo.Caption = "";
			this.TradingGroupCombo.CaptionHeight = 17;
			this.TradingGroupCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.TradingGroupCombo.ColumnCaptionHeight = 17;
			this.TradingGroupCombo.ColumnFooterHeight = 17;
			this.TradingGroupCombo.ContentHeight = 14;
			this.TradingGroupCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.TradingGroupCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown;
			this.TradingGroupCombo.DropDownWidth = 350;
			this.TradingGroupCombo.EditorBackColor = System.Drawing.SystemColors.Control;
			this.TradingGroupCombo.EditorFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TradingGroupCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.TradingGroupCombo.EditorHeight = 14;
			this.TradingGroupCombo.ExtendRightColumn = true;
			this.TradingGroupCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TradingGroupCombo.GapHeight = 2;
			this.TradingGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
			this.TradingGroupCombo.ItemHeight = 15;
			this.TradingGroupCombo.Location = new System.Drawing.Point(536, 416);
			this.TradingGroupCombo.MatchEntryTimeout = ((long)(2000));
			this.TradingGroupCombo.MaxDropDownItems = ((short)(10));
			this.TradingGroupCombo.MaxLength = 32767;
			this.TradingGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.TradingGroupCombo.Name = "TradingGroupCombo";
			this.TradingGroupCombo.PartialRightColumn = false;
			this.TradingGroupCombo.ReadOnly = true;
			this.TradingGroupCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.TradingGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.TradingGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.TradingGroupCombo.Size = new System.Drawing.Size(80, 20);
			this.TradingGroupCombo.TabIndex = 26;
			this.TradingGroupCombo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.TradingGroupCombo.ItemChanged += new System.EventHandler(this.TradingGroupCombo_ItemChanged);
			this.TradingGroupCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Group\" Data" +
				"Field=\"GroupCode\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"" +
				"Group Name\" DataField=\"GroupName\"><ValueItems /></C1DataColumn></DataCols><Style" +
				"s type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border" +
				":None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{" +
				"}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightTex" +
				"t;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:" +
				"InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;B" +
				"ackColor:Window;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style" +
				"1{}OddRow{}RecordSelector{AlignImage:Center;}Style13{AlignHorz:Center;}Heading{W" +
				"rap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;Align" +
				"Vert:Center;}Style8{}Style10{}Style11{}Style14{}Style15{AlignHorz:Near;}Style16{" +
				"AlignHorz:Near;}Style17{}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win." +
				"C1List.ListBoxView AllowColMove=\"False\" AllowColSelect=\"False\" Name=\"\" AllowRowS" +
				"izing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\"" +
				" ExtendRightColumn=\"True\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><Cli" +
				"entRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle " +
				"parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyl" +
				"e parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Si" +
				"ngle</Style></ColumnDivider><Width>65</Width><Height>15</Height><DCIdx>0</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style15\" />" +
				"<Style parent=\"Style1\" me=\"Style16\" /><FooterStyle parent=\"Style3\" me=\"Style17\" " +
				"/><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Wi" +
				"dth>250</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn></internalCo" +
				"ls><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height><Sty" +
				"le>None</Style></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRow" +
				"Style parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" />" +
				"<GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Sty" +
				"le2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle par" +
				"ent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordS" +
				"electorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selec" +
				"ted\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxV" +
				"iew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" " +
				"me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=" +
				"\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"S" +
				"elected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=" +
				"\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Rec" +
				"ordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1<" +
				"/vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWid" +
				"th>16</DefaultRecSelWidth></Blob>";
			// 
			// LocateSummaryList
			// 
			this.LocateSummaryList.AddItemSeparator = ';';
			this.LocateSummaryList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LocateSummaryList.BackColor = System.Drawing.Color.Ivory;
			this.LocateSummaryList.Caption = "Locate Summary";
			this.LocateSummaryList.CaptionHeight = 17;
			this.LocateSummaryList.ColumnCaptionHeight = 17;
			this.LocateSummaryList.ColumnFooterHeight = 17;
			this.LocateSummaryList.DeadAreaBackColor = System.Drawing.Color.DarkGray;
			this.LocateSummaryList.EmptyRows = true;
			this.LocateSummaryList.ExtendRightColumn = true;
			this.LocateSummaryList.FetchRowStyles = true;
			this.LocateSummaryList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
			this.LocateSummaryList.ItemHeight = 15;
			this.LocateSummaryList.Location = new System.Drawing.Point(896, 304);
			this.LocateSummaryList.MatchEntryTimeout = ((long)(2000));
			this.LocateSummaryList.Name = "LocateSummaryList";
			this.LocateSummaryList.PartialRightColumn = false;
			this.LocateSummaryList.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.LocateSummaryList.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.LocateSummaryList.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.LocateSummaryList.Size = new System.Drawing.Size(264, 190);
			this.LocateSummaryList.TabIndex = 28;
			this.LocateSummaryList.TabStop = false;
			this.LocateSummaryList.FormatText += new C1.Win.C1List.FormatTextEventHandler(this.LocateSummaryList_FormatText);
			this.LocateSummaryList.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Group\" Data" +
				"Field=\"GroupCode\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"" +
				"Request\" DataField=\"ClientQuantity\" NumberFormat=\"FormatText Event\"><ValueItems " +
				"/></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Located\" DataField=\"Quantity\" " +
				"NumberFormat=\"FormatText Event\"><ValueItems /></C1DataColumn></DataCols><Styles " +
				"type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:N" +
				"one,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}S" +
				"tyle4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;" +
				"BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:In" +
				"activeCaption;}Style30{AlignHorz:Near;}Style32{}Footer{BackColor:Window;}Caption" +
				"{AlignHorz:Center;}Style31{AlignHorz:Far;}Normal{Font:Verdana, 8.25pt;BackColor:" +
				"Ivory;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}Style23" +
				"{}Style22{AlignHorz:Near;}Style21{AlignHorz:Near;}OddRow{}RecordSelector{AlignIm" +
				"age:Center;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeCo" +
				"lor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{Ali" +
				"gnHorz:Far;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBo" +
				"xView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17" +
				"\" ExtendRightColumn=\"True\" FetchRowStyles=\"True\" VerticalScrollGroup=\"1\" Horizon" +
				"talScrollGroup=\"1\"><ClientRect>0, 17, 260, 169</ClientRect><internalCols><C1Disp" +
				"layColumn><HeadingStyle parent=\"Style2\" me=\"Style21\" /><Style parent=\"Style1\" me" +
				"=\"Style22\" /><FooterStyle parent=\"Style3\" me=\"Style23\" /><ColumnDivider><Color>D" +
				"arkGray</Color><Style>Single</Style></ColumnDivider><Width>65</Width><Height>15<" +
				"/Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
				"\"Style2\" me=\"Style30\" /><Style parent=\"Style1\" me=\"Style31\" /><FooterStyle paren" +
				"t=\"Style3\" me=\"Style32\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</S" +
				"tyle></ColumnDivider><Width>85</Width><Height>15</Height><DCIdx>1</DCIdx></C1Dis" +
				"playColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style " +
				"parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><Colu" +
				"mnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>85<" +
				"/Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn></internalCols><VScr" +
				"ollBar><Width>16</Width><Style>Always</Style></VScrollBar><HScrollBar><Height>16" +
				"</Height><Style>None</Style></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style" +
				"9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" m" +
				"e=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Hea" +
				"ding\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><Inac" +
				"tiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style" +
				"8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle " +
				"parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1" +
				"List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style par" +
				"ent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=" +
				"\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"" +
				"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent" +
				"=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Hea" +
				"ding\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><" +
				"vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><Def" +
				"aultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			// 
			// RefreshTimer
			// 
			this.RefreshTimer.Interval = 20000;
			this.RefreshTimer.SynchronizingObject = this;
			this.RefreshTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.RefreshTimer_Elapsed);
			// 
			// AutoUpdateCheckBox
			// 
			this.AutoUpdateCheckBox.Checked = true;
			this.AutoUpdateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.AutoUpdateCheckBox.Location = new System.Drawing.Point(936, 8);
			this.AutoUpdateCheckBox.Name = "AutoUpdateCheckBox";
			this.AutoUpdateCheckBox.Size = new System.Drawing.Size(152, 16);
			this.AutoUpdateCheckBox.TabIndex = 29;
			this.AutoUpdateCheckBox.Text = "Auto Update Pending";
			this.AutoUpdateCheckBox.CheckedChanged += new System.EventHandler(this.AutoUpdateCheckBox_CheckedChanged);
			// 
			// ShowPendingButton
			// 
			this.ShowPendingButton.Location = new System.Drawing.Point(816, 8);
			this.ShowPendingButton.Name = "ShowPendingButton";
			this.ShowPendingButton.Size = new System.Drawing.Size(96, 16);
			this.ShowPendingButton.TabIndex = 30;
			this.ShowPendingButton.Text = "Show Pending";
			this.ShowPendingButton.Click += new System.EventHandler(this.ShowPendingButton_Click);
			// 
			// ShowAllButton
			// 
			this.ShowAllButton.Location = new System.Drawing.Point(816, 32);
			this.ShowAllButton.Name = "ShowAllButton";
			this.ShowAllButton.Size = new System.Drawing.Size(96, 16);
			this.ShowAllButton.TabIndex = 31;
			this.ShowAllButton.Text = "Show All";
			this.ShowAllButton.Click += new System.EventHandler(this.ShowAllButton_Click);
			// 
			// AutoUpdateAllCheckBox
			// 
			this.AutoUpdateAllCheckBox.Location = new System.Drawing.Point(936, 32);
			this.AutoUpdateAllCheckBox.Name = "AutoUpdateAllCheckBox";
			this.AutoUpdateAllCheckBox.Size = new System.Drawing.Size(152, 16);
			this.AutoUpdateAllCheckBox.TabIndex = 32;
			this.AutoUpdateAllCheckBox.Text = "Auto Update All";
			this.AutoUpdateAllCheckBox.CheckedChanged += new System.EventHandler(this.AutoUpdateAllCheckBox_CheckedChanged);
			// 
			// UpdateAllRefreshTimer
			// 
			this.UpdateAllRefreshTimer.Interval = 20000;
			this.UpdateAllRefreshTimer.SynchronizingObject = this;
			this.UpdateAllRefreshTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateAllRefreshTimer_Elapsed);
			// 
			// ShortSaleLocateForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1216, 509);
			this.Controls.Add(this.AutoUpdateAllCheckBox);
			this.Controls.Add(this.ShowAllButton);
			this.Controls.Add(this.ShowPendingButton);
			this.Controls.Add(this.AutoUpdateCheckBox);
			this.Controls.Add(this.LocateSummaryList);
			this.Controls.Add(this.InventoryList);
			this.Controls.Add(this.GroupNameText);
			this.Controls.Add(this.ClientCommentText);
			this.Controls.Add(this.LocatesGrid);
			this.Controls.Add(this.TradingGroupCombo);
			this.Controls.Add(this.TradeDateCombo);
			this.Controls.Add(this.TradeDateLabel);
			this.Controls.Add(this.SecIdLabel);
			this.Controls.Add(this.QuarterLabel);
			this.Controls.Add(this.QuarterNumericEdit);
			this.Controls.Add(this.YearLabel);
			this.Controls.Add(this.YearNumericEdit);
			this.Controls.Add(this.SecIdTextBox);
			this.Controls.Add(this.EnableResearchCheck);
			this.Controls.Add(this.StatusTextBox);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.GroupNameLabel);
			this.Controls.Add(this.RequestLabel);
			this.Controls.Add(this.CommentLabel);
			this.Controls.Add(this.SubmitButton);
			this.Controls.Add(this.RequestTextBox);
			this.Controls.Add(this.CommentTextBox);
			this.Controls.Add(this.ClientCommentLabel);
			this.DockPadding.Bottom = 220;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 55;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = System.Windows.Forms.ImeMode.On;
			this.Name = "ShortSaleLocateForm";
			this.Text = "Short Sale - Locates";
			this.Resize += new System.EventHandler(this.ShortSaleLocateForm_Resize);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ShortSaleLocateForm_Closing);
			this.Load += new System.EventHandler(this.ShortSaleLocateForm_Load);
			this.Deactivate += new System.EventHandler(this.ShortSaleLocateForm_Deactivate);
			((System.ComponentModel.ISupportInitialize)(this.LocatesGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CommentTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CommentLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RequestTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RequestLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SecIdTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SecIdLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.YearNumericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.YearLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.QuarterLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.QuarterNumericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TradeDateLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.InventoryList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TradeDateCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LocateSummaryList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RefreshTimer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UpdateAllRefreshTimer)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		private void InventoryListFill(string groupCode, string secId)
		{
			try
			{
				if (inventoryDataSet != null)
				{
					inventoryDataSet.Tables["Inventory"].Clear();
					inventoryDataSet.Tables["Inventory"].AcceptChanges(); // Clear list of potentially incorrect data.
				}

				if (secId != null)
				{
					InventoryList.Caption = "Available Inventory [" + secId + "]";
          
					inventoryDataSet = mainForm.ShortSaleAgent.InventoryGet(groupCode, secId, mainForm.UtcOffset); // Get new list for current security.
        
					InventoryList.HoldFields();
					InventoryList.DataSource = inventoryDataSet;
					InventoryList.DataMember = "Inventory";        
					InventoryList.Rebind();
				}

				if ((inventoryDataSet != null) && (inventoryDataSet.Tables["Inventory"].Rows.Count > 0))
				{
					InventoryList.DeadAreaBackColor = Color.Honeydew;
				}
				else
				{
					InventoryList.DeadAreaBackColor = Color.DarkGray;
				}
			}
			catch (Exception e)
			{
				InventoryList.DeadAreaBackColor = Color.RosyBrown;

				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [ShortSaleLocateForm.InventoryListFill]", Log.Error, 1);
			}
		}
    
		private void LocatesGridFill()
		{
			LocatesGridFill(false);
		}
		
		private void LocatesGridFill(bool returnPending)
		{
			ClientCommentText.Text = "";
			GroupNameText.Text = "";

			mainForm.Alert("Please wait... Loading locates for " + TradeDateCombo.Text + "...", PilotState.Unknown);
			this.Cursor = Cursors.WaitCursor;

			LocateSummaryListFill("");			
			Application.DoEvents();

			try
			{
				if (TradeDateCombo.Text.Equals(TradeDateCombo.Tag)) // Load request is for the current trade date.
				{
					AutoUpdateCheckBox.Enabled = true;					

					LocatesGrid.AllowUpdate = mayEdit;
					LocatesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;	
				}
				else
				{					
					AutoUpdateCheckBox.Checked = false;
					AutoUpdateCheckBox.Enabled = false;

					LocatesGrid.AllowUpdate = false;
					LocatesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
				}

				DataRow [] rows = null;
				DataSet dataSet = null;
				
				if (returnPending)
				{
					dataSet = mainForm.ShortSaleAgent.LocatesGet(TradeDateCombo.Text, null, "Pending", mainForm.UtcOffset);
					rows = dataSet.Tables["Table"].Select();
				}
				else
				{
					dataSet = mainForm.ShortSaleAgent.LocatesGet(TradeDateCombo.Text, null, null, mainForm.UtcOffset);
					rows = dataSet.Tables["Table"].Select();
				}								
				
				if (returnPending && (rows.Length > 0))
				{
					NotifyWindow nw = new NotifyWindow("Locates @ " + DateTime.Now.ToString(Standard.TimeShortFormat), 
						rows.Length.ToString("#,##0") + " pending locate(s).");
					nw.Notify();
				}

				mainDataSet.Tables["Locates"].Rows.Clear();
				mainDataSet.Tables["Locates"].BeginLoadData();
      
				foreach (DataRow row in rows)
				{
					mainDataSet.Tables["Locates"].ImportRow(row);          
				}
		
				mainDataSet.Tables["Locates"].EndLoadData();        				
				
				mainDataSet.Tables["LocateSummary"].Rows.Clear();				
				mainDataSet.Tables["LocateSummary"].BeginLoadData();
				
				foreach (DataRow row in dataSet.Tables["Table1"].Rows)
				{
					mainDataSet.Tables["LocateSummary"].ImportRow(row);          
				}
				
				mainDataSet.Tables["LocateSummary"].EndLoadData();        

				mainForm.Alert("Loading locates for " + TradeDateCombo.Text + "... Done!", PilotState.Normal);
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [ShortSaleLocateForm.LocateGridFill]", 1);
			}			
			
			if (LocatesGrid.Splits[0,0].Rows.Count > 0)
			{
				LocateSummaryListFill(LocatesGrid.Columns["SecId"].Text);
			}		

			this.Cursor = Cursors.Default;
		}

		private void LocatesGridFill(string tradeDateMin, string tradeDateMax, string groupCode)
		{
			ClientCommentText.Text = "";
			GroupNameText.Text = "";

			mainForm.Alert("Please wait... Loading Locates research for Q" +
				QuarterNumericEdit.Text  + " " + YearNumericEdit.Text  + "...", PilotState.Unknown);
			this.Cursor = Cursors.WaitCursor;

			LocateSummaryListFill("");			
			Application.DoEvents();

			try
			{				
				DataRow[] rows = mainForm.ShortSaleAgent.LocatesGet(
					tradeDateMin, tradeDateMax, groupCode, SecIdTextBox.Text.Trim(), mainForm.UtcOffset).Tables["Locates"].Select();
    
				mainDataSet.Tables["Locates"].Rows.Clear();
				mainDataSet.Tables["Locates"].BeginLoadData();
      
				foreach (DataRow row in rows)
				{
					mainDataSet.Tables["Locates"].ImportRow(row);          
				}

				mainDataSet.Tables["Locates"].EndLoadData();        
				mainForm.Alert("Loading Locates research for Q" +
					QuarterNumericEdit.Text  + " " + YearNumericEdit.Text  + "... Done!", PilotState.Normal);
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [ShortSaleLocateForm.LocateGridFill]", Log.Error, 1);
			}			

			if (LocatesGrid.Splits[0,0].Rows.Count > 0)
			{
				LocateSummaryListFill(LocatesGrid.Columns["SecId"].Text);
			}

			this.Cursor = Cursors.Default;
		}		

		private void LocateSummaryListFill(string secId)
		{
			long clientQuantity = 0;
			long quantity = 0;
        
			LocateSummaryList.Caption = "Locate Summary [" + secId + "]";
      
			locateSummaryDataView.RowFilter = "SecId = '" + secId + "'";

			if (locateSummaryDataView.Count > 1)
			{
				LocateSummaryList.ColumnFooters = true;

				foreach (DataRowView row in locateSummaryDataView)
				{
					if (!row["ClientQuantity"].ToString().Equals(""))
					{
						clientQuantity += (long) row["ClientQuantity"];
					}
					
					if (!row["Quantity"].ToString().Equals(""))
					{					
						quantity += (long) row["Quantity"]; 
					}
				}

				LocateSummaryList.Columns["ClientQuantity"].FooterText = clientQuantity.ToString("#,##0");
				LocateSummaryList.Columns["Quantity"].FooterText = quantity.ToString("#,##0");
			}
			else
			{
				LocateSummaryList.ColumnFooters = false;
			}
		}

		private void ShortSaleLocateForm_Load(object sender, System.EventArgs e)
		{
			int height = mainForm.Height - 250;
			int width = 1220;

			this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "5"));
			this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "5"));
			this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
			this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

			this.Show();
			this.Cursor = Cursors.WaitCursor;
			Application.DoEvents();

			mainForm.Alert("Please wait... Loading current data...", PilotState.Unknown);

			try
			{				
				mainDataSet = mainForm.ShortSaleAgent.LocateDataGet("Pending", mainForm.UtcOffset);

				mainDataSet.Tables.Add("LocateSummary");
				mainDataSet.Tables["LocateSummary"].Columns.Add("SecId", typeof(string));
				mainDataSet.Tables["LocateSummary"].Columns.Add("GroupCode", typeof(string));
				mainDataSet.Tables["LocateSummary"].Columns.Add("ClientQuantity", typeof(long));
				mainDataSet.Tables["LocateSummary"].Columns.Add("Quantity", typeof(long));

				mainDataSet.Tables["LocateSummary"].PrimaryKey = new DataColumn[2] {
																																						 mainDataSet.Tables["LocateSummary"].Columns["SecId"],
																																						 mainDataSet.Tables["LocateSummary"].Columns["GroupCode"]};				
        
				locateSummaryDataView = new DataView(mainDataSet.Tables["LocateSummary"]);
				locateSummaryDataView.RowFilter = "SecId = ''";
				locateSummaryDataView.Sort = "GroupCode";

				LocateSummaryList.HoldFields();
				LocateSummaryList.DataSource = locateSummaryDataView;
        
				locatesDataView = new DataView(mainDataSet.Tables["Locates"]);
				locatesDataView.Sort = "LocateId DESC";				

				LocatesGrid.SetDataBinding(locatesDataView, null, true);
      
				tradingGroupDataView = new DataView(mainDataSet.Tables["TradingGroups"]);
				tradingGroupDataView.RowFilter = "IsActive = 1";
				tradingGroupDataView.Sort = "GroupName";
        
				TradingGroupCombo.HoldFields();
				TradingGroupCombo.DataSource = tradingGroupDataView;
				TradingGroupCombo.DataMember = "TradingGroups";
				TradingGroupCombo.SelectedIndex = 0;

				TradeDateCombo.DataSource = mainDataSet;      
				TradeDateCombo.DataMember = "TradeDates";
				TradeDateCombo.SelectedIndex = 0;

				InventoryListFill("", ""); // A necessary hack to force initialization.				
				LocatesGridFill(true);
				mainForm.Alert("Loading current data... Done!", PilotState.Normal);
								
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [ShortSaleLocateForm.ShortSaleLocateForm_Load]", Log.Error, 1);
			}

			TradeDateCombo.Enabled = true;
    
			this.Cursor = Cursors.Default;
		}

		private void ShortSaleLocateForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
			} 			

			RefreshTimer.Enabled = false;
			UpdateAllRefreshTimer.Enabled = false;

			mainForm.shortSaleLocateForm = null;
		}

		private void LocatesGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			int i = LocatesGrid.Col;
			string gridData = "";

			if ((e.KeyChar == 9) && (LocatesGrid.Col == PRE_BORROW)) // User has tabbed from the last column.
			{
				if (LocatesGrid.Splits[0].Rows.Count > (LocatesGrid.Row + 1))
				{
					LocatesGrid.Row += 1;
					e.Handled = true;
				}

				return;
			}

			if (e.KeyChar.Equals((char)32) && LocatesGrid.Col.Equals(QUANTITY)
				&& LocatesGrid.Columns[QUANTITY].Text.Trim().Equals("") && LocatesGrid.EditActive) // User wishes to copy client quantity.
			{
				LocatesGrid.Columns[QUANTITY].Text = LocatesGrid.Columns["Request"].Text;
				LocatesGrid.Col = SOURCE;

				return;
			}

			if (e.KeyChar.Equals((char)13))
			{
				if ((LocatesGrid.Splits[0].Rows.Count == 1) && (LocatesGrid.DataChanged))
				{
					LocatesGrid.UpdateData();      
				}
			}

			if (e.KeyChar.Equals((char)3) && LocatesGrid.SelectedRows.Count > 0)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
				{
					gridData += dataColumn.Caption + "\t";
				}
				gridData += "\n";

				foreach (int row in LocatesGrid.SelectedRows)
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
					{
						gridData += dataColumn.CellText(row) + "\t";
					}
					gridData += "\n";
				}

				Clipboard.SetDataObject(gridData, true);
				mainForm.Alert("Copied " + LocatesGrid.SelectedRows.Count + " rows to the clipboard.", PilotState.Normal);
				e.Handled = true;
			}
		}

		private void LocatesGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			DataSet tempDataSet = new DataSet();
			
			long locateQuantity = 0;
			float rate;
		
				
			if (!LocatesGrid.Columns[QUANTITY].Text.Equals(""))
			{
				try
				{
					locateQuantity = (long)LocatesGrid.Columns[QUANTITY].Value;
				}
				catch
				{
					mainForm.Alert("Entry for the quantity located must be a numeric value!", PilotState.RunFault);
					LocatesGrid.Col = QUANTITY;
					e.Cancel = true;
					return;
				}
			}

			if (!LocatesGrid.Columns["FeeRate"].Text.Equals(""))
			{
				try
				{
					rate = float.Parse(LocatesGrid.Columns["FeeRate"].Text);
				}
				catch
				{
					mainForm.Alert("Entry for a fee must be a numeric value!", PilotState.RunFault);
					LocatesGrid.Col = FEE_RATE;
					e.Cancel = true;
					return;
				}
			}

			LocatesGrid.Columns["Source"].Text = LocatesGrid.Columns["Source"].Text.Trim();

			try
			{
				if (bool.Parse(LocatesGrid.Columns[PRE_BORROW].Value.ToString()) == true)
				{		
					mainForm.ShortSaleAgent.LocatePreBorrowSubmit(
						(long) LocatesGrid.Columns["LocateId"].Value,
						LocatesGrid.Columns["GroupCode"].Text,
						LocatesGrid.Columns["SecId"].Text,
						LocatesGrid.Columns["Quantity"].Value.ToString(),					
						LocatesGrid.Columns["FeeRate"].Value.ToString(),
						mainForm.UserId
						);
				}
				else
				{
					
					mainForm.ShortSaleAgent.LocateItemSet(
						(long) LocatesGrid.Columns["LocateId"].Value,
						LocatesGrid.Columns["Located"].Value.ToString(),
						LocatesGrid.Columns["Source"].Text,
						LocatesGrid.Columns["FeeRate"].Value.ToString(),
						LocatesGrid.Columns["PreBorrow"].Value.ToString(),
						LocatesGrid.Columns["Comment"].Text,
						mainForm.UserId
						);
				}

				LocatesGrid.Columns["ActUserShortName"].Text = "me";
				LocatesGrid.Columns["ActTime"].Text = DateTime.Now.ToString("HH:mm:ss");

				if (!LocatesGrid.Columns[QUANTITY].Text.Equals(""))
				{
					if ((long)LocatesGrid.Columns["Request"].Value <= (long)LocatesGrid.Columns["Located"].Value)
					{
						LocatesGrid.Columns["Status"].Text = "FullOK";
					}
					else if (locateQuantity > 0)
					{
						LocatesGrid.Columns["Status"].Text = "Partial";       
					}
					else if (locateQuantity == 0)
					{
						LocatesGrid.Columns["Status"].Text = "None";       
					}
				}
								
				tempDataSet = mainForm.ShortSaleAgent.LocateItemGet(LocatesGrid.Columns["LocateId"].Value.ToString(), mainForm.UtcOffset);
				
				foreach (DataRow dr in tempDataSet.Tables["Table"].Rows)
				{
					mainDataSet.Tables["Locates"].LoadDataRow(dr.ItemArray, true);	
				}

				
				foreach (DataRow dr in tempDataSet.Tables["Table1"].Rows)
				{
					Log.Write(dr["ClientQuantity"].ToString(), 1);
					mainDataSet.Tables["LocateSummary"].LoadDataRow(dr.ItemArray, true);	
				}

				LocateSummaryListFill(secId);				
			}			
			catch(Exception ee)
			{
				mainForm.Alert("Error processing the update to ID " + LocatesGrid.Columns["LocateIdTail"].Text + ".", PilotState.RunFault);
				Log.Write(ee.Message + " [ShortSaleLocateForm.LocatesGrid_BeforeUpdate]", Log.Error, 1);
        
				e.Cancel = true;
				return;
			}
		}

		private void LocatesGrid_AfterUpdate(object sender, System.EventArgs e)
		{
			if(LocatesGrid.Splits[0].Rows.Count != LocatesGrid.Row)
			{
				LocatesGrid.Col = QUANTITY;
			}
		}

		private void LocatesGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0)
			{
				return;
			}
  
			try
			{
				switch(LocatesGrid.Columns[e.ColIndex].DataField)
				{
					case ("LocateIdTail"):
						e.Value = e.Value.ToString().PadLeft(5, '0');
						break;
					case ("OpenTime"):
						if (EnableResearchCheck.Checked)
						{
							e.Value = DateTime.Parse(e.Value).ToString(Standard.DateTimeShortFormat);
						}
						else
						{
							e.Value = DateTime.Parse(e.Value).ToString(Standard.TimeFileFormat);
						}
						break;
					case ("ClientQuantity"):
					case ("Quantity"):
						e.Value = long.Parse(e.Value).ToString("#,##0");
						break;
					case ("ActTime"):
						e.Value = Tools.FormatDate(e.Value.ToString(), "HH:mm:ss");          
						break;
				}
			}
			catch {}
		}

		private void LocatesGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{				
				if (!LocatesGrid.Columns["SecId"].Text.Equals(secId) || !LocatesGrid.Columns["GroupCode"].Text.Trim().Equals(groupCode.Trim()))
				{
					secId = LocatesGrid.Columns["SecId"].Text;
					groupCode = LocatesGrid.Columns["GroupCode"].Text;
					
					this.Cursor = Cursors.WaitCursor;      				
     
					ClientCommentText.Text = LocatesGrid.Columns["ClientComment"].Text;
					GroupNameText.Text = LocatesGrid.Columns["GroupName"].Text;
      
					mainForm.SecId = secId;

					InventoryListFill(groupCode, secId);
					LocateSummaryListFill(secId);

					this.Cursor = Cursors.Default;
				}
			}
			catch 
			{
				secId = "";

				this.Cursor = Cursors.WaitCursor;
      
				ClientCommentText.Text = "";
				GroupNameText.Text = "";
      
				mainForm.SecId = "";

				InventoryListFill("", null);
				LocateSummaryListFill("");

				this.Cursor = Cursors.Default;
			}    
		}

		private void LocatesGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.X <= LocatesGrid.RecordSelectorWidth && e.Y <= LocatesGrid.RowHeight)
			{
				if (LocatesGrid.SelectedRows.Count.Equals(0))
				{
					for (int i = 0; i < LocatesGrid.Splits[0,0].Rows.Count; i++)
					{
						LocatesGrid.SelectedRows.Add(i);
					}

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.Columns)
					{
						LocatesGrid.SelectedCols.Add(dataColumn);
					}
				}
				else
				{
					LocatesGrid.SelectedRows.Clear();
					LocatesGrid.SelectedCols.Clear();
				}
			}
		}

		private void RequestText_TextChanged(object sender, System.EventArgs e)
		{
			if (!RequestTextBox.Text.Equals(""))
			{
				SubmitButton.Enabled = true;
				StatusTextBox.Text = "";
			}
		}

		private void CommentText_TextChanged(object sender, System.EventArgs e)
		{
			if (!CommentTextBox.Text.Equals(""))
			{
				SubmitButton.Enabled = true;
				StatusTextBox.Text = "";
			}
		}

		private void SubmitButton_Click(object sender, System.EventArgs e)
		{
			string tradeDateMin = "";
			string tradeDateMax = "";
			string groupCode = "";

			this.Cursor = Cursors.WaitCursor;

			if (EnableResearchCheck.Checked)
			{
				switch ((short)QuarterNumericEdit.Value)
				{
					case 1 :
						tradeDateMin = YearNumericEdit.Text + "-01-01";
						tradeDateMax = YearNumericEdit.Text + "-03-31";
						break;
					case 2 :
						tradeDateMin = YearNumericEdit.Text + "-04-01";
						tradeDateMax = YearNumericEdit.Text + "-06-30";
						break;
					case 3 :
						tradeDateMin = YearNumericEdit.Text + "-07-01";
						tradeDateMax = YearNumericEdit.Text + "-09-30";
						break;
					case 4 :
						tradeDateMin = YearNumericEdit.Text + "-10-01";
						tradeDateMax = YearNumericEdit.Text + "-12-31";
						break;
				}

				if (!TradingGroupCombo.Text.Equals("***"))
				{
					groupCode = TradingGroupCombo.Text;
				}

				LocatesGridFill(tradeDateMin, tradeDateMax, groupCode);
			}
			else
			{
				if(!RequestTextBox.Text.Equals(""))
				{
					try
					{
						StatusTextBox.Text = mainForm.ShortSaleAgent.LocateListSubmit(mainForm.UserId,
							TradingGroupCombo.Text, CommentTextBox.Text, RequestTextBox.Text);
    
						RequestTextBox.Text = "";
						CommentTextBox.Text = "";
					}
					catch (Exception ee)
					{
						StatusTextBox.Text = ee.Message;             
					}
				}

				SubmitButton.Enabled = false;
			}

			this.Cursor = Cursors.Default;
		}

		private void EnableResearchCheck_CheckedChanged(object sender, System.EventArgs e)
		{
			ClientCommentText.Text = "";
			GroupNameText.Text = "";

			SecIdTextBox.Text = "";
			CommentTextBox.Text = "";
			RequestTextBox.Text = "";

			SecIdLabel.Visible = EnableResearchCheck.Checked;
			SecIdTextBox.Visible = EnableResearchCheck.Checked;
      
			YearLabel.Visible = EnableResearchCheck.Checked;
			YearNumericEdit.Visible = EnableResearchCheck.Checked;
      
			QuarterLabel.Visible = EnableResearchCheck.Checked;
			QuarterNumericEdit.Visible = EnableResearchCheck.Checked;

			CommentLabel.Visible = !EnableResearchCheck.Checked;
			CommentTextBox.Visible = !EnableResearchCheck.Checked;

			RequestLabel.Visible = !EnableResearchCheck.Checked;
			RequestTextBox.Visible = !EnableResearchCheck.Checked;
			
			StatusLabel.Visible = !EnableResearchCheck.Checked;
			//StatusTextBox.Visible = !EnableResearchCheck.Checked;
			
			ShowPendingButton.Enabled = !EnableResearchCheck.Checked;			
			ShowAllButton.Enabled = !EnableResearchCheck.Checked;
			
			AutoUpdateCheckBox.Checked = !EnableResearchCheck.Checked;
			AutoUpdateCheckBox.Enabled = !EnableResearchCheck.Checked;
			
			AutoUpdateAllCheckBox.Checked = !EnableResearchCheck.Checked;
			AutoUpdateAllCheckBox.Enabled = !EnableResearchCheck.Checked;
			
			RefreshTimer.Enabled = !EnableResearchCheck.Checked;
			
			locatesDataView.RowFilter = "";
			
			TradingGroupCombo.ReadOnly = (!EnableResearchCheck.Checked && RequestTextBox.ReadOnly);

			TradeDateLabel.Enabled = !EnableResearchCheck.Checked;
			TradeDateCombo.Enabled = !EnableResearchCheck.Checked;
			
			SubmitButton.Enabled = false;
      			
			if (EnableResearchCheck.Checked)
			{
				mainDataSet.Tables["Locates"].Clear();
				mainDataSet.Tables["Locates"].AcceptChanges();  
        
				LocatesGrid.Splits[0].DisplayColumns["OpenTime"].Width = 115;
				StatusTextBox.Text = "";				
			}
			else
			{
				LocatesGridFill();

				LocatesGrid.Splits[0].DisplayColumns["OpenTime"].Width = 65;
			}
		}

		private void TradingGroupCombo_ItemChanged(object sender, System.EventArgs e)
		{
			StatusTextBox.Text = TradingGroupCombo.GetItemText(TradingGroupCombo.WillChangeToIndex, 1);
      
			if (EnableResearchCheck.Checked)
			{
				SubmitButton.Enabled = !(TradingGroupCombo.GetItemText(TradingGroupCombo.WillChangeToIndex, 0).Equals("***")
					&& SecIdTextBox.Text.Trim().Equals(""));
			}
		}

		private void SecIdTextBox_TextChanged(object sender, System.EventArgs e)
		{
			SubmitButton.Enabled = !(TradingGroupCombo.Text.Equals("***") && SecIdTextBox.Text.Trim().Equals(""));   
		}

		private void InventoryList_FetchRowStyle(object sender, C1.Win.C1List.FetchRowStyleEventArgs e)
		{
			try
			{
				if (DateTime.Parse(InventoryList.GetItemText(e.Row, "Received")).ToString(Standard.DateFormat).CompareTo(TradeDateCombo.Tag) >= 0)
				{
					if (long.Parse(InventoryList.Columns["Quantity"].CellValue(e.Row).ToString()) < 0)
					{
						e.CellStyle.ForeColor = Color.Red;
					}
					else
					{
						e.CellStyle.ForeColor = Color.Navy;
					}
				}
				else
				{
					e.CellStyle.ForeColor = Color.Gray;
				}
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void InventoryList_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0) // Then nothing to do.
			{
				return;
			}
  
			try
			{
				switch(InventoryList.Columns[e.ColIndex].DataField)
				{
					case ("ScribeTime"):
						e.Value = DateTime.Parse(e.Value).ToString(Standard.DateTimeShortFormat);
						break;
					case ("BizDate"):
						e.Value = DateTime.Parse(e.Value).ToString(Standard.DateFormat);
						break;
					case ("Quantity"):
						e.Value = long.Parse(e.Value).ToString("#,##0");
						break;
				}        
			}
			catch{}
		}

		private void TradeDateCombo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)13))
			{
				TradeDateCombo.Text = Tools.FormatDate(TradeDateCombo.Text, Standard.DateFormat);
				e.Handled = true;
			}
		}

		private void TradeDateCombo_RowChange(object sender, System.EventArgs e)
		{
			if (TradeDateCombo.Enabled && TradeDateCombo.SelectedIndex > -1)
			{
				LocatesGridFill(); 
			}
		}

		private void LocateSummaryList_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0)
			{
				return;
			}
  
			try
			{
				switch(LocateSummaryList.Columns[e.ColIndex].DataField)
				{
					case ("ClientQuantity"):
					case ("Quantity"):
						e.Value = long.Parse(e.Value).ToString("#,##0");
						break;
				}
			}
			catch {}    
		}

		private void LocatesGrid_Error(object sender, C1.Win.C1TrueDBGrid.ErrorEventArgs e)
		{
			mainForm.Alert(e.Exception.Message, PilotState.RunFault); 
			e.Handled = true;
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (LocatesGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.", PilotState.RunFault);
				return;
			}

			try
			{
				maxTextLength = new int[LocatesGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in LocatesGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in LocatesGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
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

				mainForm.Alert("Total: " + LocatesGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + " [ShortSaleLocateForm.SendToEmailMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void RefreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (RefreshTimer.Enabled)
			{
				RefreshTimer.Enabled = false;			
			
				if (AutoUpdateAllCheckBox.Checked)
				{
					LocatesGridFill(false);
				}
				else if (AutoUpdateCheckBox.Checked)
				{
					LocatesGridFill(true);
				}				
		
				InventoryListFill("", "");
				RefreshTimer.Enabled = true;
			}
		}

		private void AutoUpdateCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{																
			if (AutoUpdateAllCheckBox.Checked)
			{
				AutoUpdateAllCheckBox.Checked = false;
			}
								
			RefreshTimer.Enabled = AutoUpdateCheckBox.Checked;			
		}

		private void ShowPendingButton_Click(object sender, System.EventArgs e)
		{		
			if (AutoUpdateCheckBox.Checked)
			{
				AutoUpdateCheckBox.Checked = false;			
			}
			
			if (AutoUpdateAllCheckBox.Checked)
			{
				AutoUpdateAllCheckBox.Checked = false;			
			}

			LocatesGridFill(true);						
		}

		private void ShowAllButton_Click(object sender, System.EventArgs e)
		{
			if (AutoUpdateCheckBox.Checked)
			{
				AutoUpdateCheckBox.Checked = false;
			}

			if (AutoUpdateAllCheckBox.Checked)
			{
				AutoUpdateAllCheckBox.Checked = false;			
			}

			LocatesGridFill();
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			Excel excel = new Excel();
			excel.ExportGridToExcel(ref LocatesGrid);
		
			this.Cursor = Cursors.Default;
		}

		private void AttachMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (!AttachedMenuItem.Checked)
				{
					mainForm.shortSaleLocateForm.MdiParent = mainForm;
					AttachedMenuItem.Checked = true;
				}
				else
				{
					mainForm.shortSaleLocateForm.MdiParent = null;
					AttachedMenuItem.Checked = false;
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void AutoUpdateAllCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			if (AutoUpdateCheckBox.Checked)
			{
				AutoUpdateCheckBox.Checked = false;
			}
						
			UpdateAllRefreshTimer.Enabled = AutoUpdateAllCheckBox.Checked;				
		}

		private void UpdateAllRefreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if ((UpdateAllRefreshTimer.Enabled) && (!RefreshTimer.Enabled))
			{
				UpdateAllRefreshTimer.Enabled = false;			
							
				LocatesGridFill(false);
						
				UpdateAllRefreshTimer.Enabled = true;				
			}
		}

		private void ActionZeroLocatesMenuItem_Click(object sender, System.EventArgs e)
		{
			DataSet tempDataSet = new DataSet();

			try
			{		
				for ( int count = 0; count < LocatesGrid.SelectedRows.Count; count++)
				{
					LocatesGrid[LocatesGrid.SelectedRows[count], "Located"] = 0;

					mainForm.ShortSaleAgent.LocateItemSet(
						(long) LocatesGrid.Columns["LocateId"].CellValue(LocatesGrid.SelectedRows[count]),
						LocatesGrid.Columns["Located"].CellValue(LocatesGrid.SelectedRows[count]).ToString(),
						LocatesGrid.Columns["Source"].CellText(LocatesGrid.SelectedRows[count]),
						LocatesGrid.Columns["FeeRate"].CellValue(LocatesGrid.SelectedRows[count]).ToString(),
						LocatesGrid.Columns["PreBorrow"].CellValue(LocatesGrid.SelectedRows[count]).ToString(),
						LocatesGrid.Columns["Comment"].CellText(LocatesGrid.SelectedRows[count]),
						mainForm.UserId
						);

					LocatesGrid[LocatesGrid.SelectedRows[count], "ActUserShortName"] = "me";
					LocatesGrid[LocatesGrid.SelectedRows[count], "ActTime"] = DateTime.Now.ToString("HH:mm:ss");					
								
					tempDataSet = mainForm.ShortSaleAgent.LocateItemGet(LocatesGrid.Columns["LocateId"].CellValue(LocatesGrid.SelectedRows[count]).ToString(), mainForm.UtcOffset);
				
					foreach (DataRow dr in tempDataSet.Tables["Table"].Rows)
					{
						mainDataSet.Tables["Locates"].LoadDataRow(dr.ItemArray, true);	
					}

					foreach (DataRow dr in tempDataSet.Tables["Table1"].Rows)
					{
						mainDataSet.Tables["LocateSummary"].LoadDataRow(dr.ItemArray, true);	
					}
				}
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);				
			}
		}

		void NotificationWindowTest ()
		{
			NotifyWindow nw = new NotifyWindow("Lcoate Notification", "Locate Notification");
			nw.Show();
		}

		private void ShortSaleLocateForm_Resize(object sender, System.EventArgs e)
		{
			if (this.WindowState.Equals(FormWindowState.Minimized))
			{
				if (!EnableResearchCheck.Checked)
				{
					AutoUpdateCheckBox.Checked = true;
				}
			}
		}

		private void ShortSaleLocateForm_Deactivate(object sender, System.EventArgs e)
		{
			if (!EnableResearchCheck.Checked)
			{
				AutoUpdateCheckBox.Checked = true;
			}
		}

		private void ShowPreBorrowsMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (shortSaleLocatesPreBorrowForm == null)
				{
					shortSaleLocatesPreBorrowForm = new ShortSaleLocatesPreBorrowForm(mainForm);
					shortSaleLocatesPreBorrowForm.MdiParent = mainForm;
					shortSaleLocatesPreBorrowForm.Show();
				}
				else
				{
					shortSaleLocatesPreBorrowForm.Activate();
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void LocatesGrid_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			try
			{
				if (e.Column.DataColumn.DataField.Equals("PreBorrow"))
				{
					if ((bool.Parse(e.OldValue.ToString()) == true) && (bool.Parse(LocatesGrid.Columns["PreBorrow"].Value.ToString()) == false))
					{
						LocatesGrid.Columns["Source"].Text = "";
						LocatesGrid.Columns["FeeRate"].Text = "";
					}
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}		
	}
}
