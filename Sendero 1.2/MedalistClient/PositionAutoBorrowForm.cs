using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class PositionAutoBorrowForm : System.Windows.Forms.Form
	{
		private const string TEXT = "Position - AutoBorrow";
		
		private MainForm mainForm;

		private string tempPath = "";
	
		private DataSet dataSet;

		private string autoBorrowListsViewRowFilter;
		private string autoBorrowItemsViewRowFilter;
		private string autoBorrowResultsViewRowFilter;
		
		private string listName = "";
		private string secId = "";

		private DataView autoBorrowListsView;
		private DataView autoBorrowItemsView;
		private DataView autoBorrowResultsView;
		
		
		private System.Windows.Forms.Panel TopPanel;
		private C1.Win.C1Input.C1Label BookGroupNameLabel;
		private C1.Win.C1Input.C1Label BookGroupLabel;
		private C1.Win.C1List.C1Combo BookGroupCombo;	
		private System.Windows.Forms.Splitter vSplitter;
		private System.Windows.Forms.Splitter hSplitter;		
		private System.ComponentModel.Container components = null;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ResultsGrid;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ItemsGrid;
		private System.Windows.Forms.Button RefreshButton;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToProcessAgentMenuItem;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ListsGrid;
		
		public PositionAutoBorrowForm(MainForm mainForm)
		{
			try
			{
				this.mainForm = mainForm;
				InitializeComponent();		
			         			
				tempPath = Standard.ConfigValue("TempPath", @"C:\");
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionAutoBorrowForm.PositionAutoBorrowForm]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionAutoBorrowForm));
			this.ResultsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.TopPanel = new System.Windows.Forms.Panel();
			this.ItemsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.vSplitter = new System.Windows.Forms.Splitter();
			this.ListsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToProcessAgentMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
			this.BookGroupLabel = new C1.Win.C1Input.C1Label();
			this.BookGroupCombo = new C1.Win.C1List.C1Combo();
			this.hSplitter = new System.Windows.Forms.Splitter();
			this.RefreshButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).BeginInit();
			this.TopPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ItemsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ListsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
			this.SuspendLayout();
			// 
			// ResultsGrid
			// 
			this.ResultsGrid.AllowDelete = true;
			this.ResultsGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.ResultsGrid.AllowUpdate = false;
			this.ResultsGrid.Caption = "Results";
			this.ResultsGrid.CaptionHeight = 17;
			this.ResultsGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.ResultsGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ResultsGrid.EmptyRows = true;
			this.ResultsGrid.ExtendRightColumn = true;
			this.ResultsGrid.FilterBar = true;
			this.ResultsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ResultsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.ResultsGrid.Location = new System.Drawing.Point(0, 533);
			this.ResultsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.ResultsGrid.Name = "ResultsGrid";
			this.ResultsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ResultsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ResultsGrid.PreviewInfo.ZoomFactor = 75;
			this.ResultsGrid.RecordSelectorWidth = 16;
			this.ResultsGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.ResultsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ResultsGrid.RowHeight = 15;
			this.ResultsGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.ResultsGrid.Size = new System.Drawing.Size(1024, 232);
			this.ResultsGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.ResultsGrid.TabIndex = 0;
			this.ResultsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.ResultsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"BizDate\" Da" +
				"taField=\"BizDate\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"BookGroup\" DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"ListName\" DataField=\"ListName\"><ValueIt" +
				"ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Security ID\" " +
				"DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"Book\" DataField=\"Book\"><ValueItems /><GroupInfo /></C1DataColumn><C" +
				"1DataColumn Level=\"0\" Caption=\"Quantity\" DataField=\"Quantity\" NumberFormat=\"Form" +
				"atText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
				"Caption=\"Quantity Delivered\" DataField=\"QuantityDelivered\" NumberFormat=\"FormatT" +
				"ext Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
				"tion=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn><C1Da" +
				"taColumn Level=\"0\" Caption=\"Status\" DataField=\"Status\"><ValueItems /><GroupInfo " +
				"/></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrap" +
				"per\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Inactive{Fo" +
				"reColor:InactiveCaptionText;BackColor:InactiveCaption;}Selected{ForeColor:Highli" +
				"ghtText;BackColor:Highlight;}Editor{}FilterBar{}Heading{Wrap:True;AlignVert:Cent" +
				"er;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style18{}S" +
				"tyle19{}Style14{}Style15{}Style16{AlignHorz:Near;}Style17{AlignHorz:Near;}Style1" +
				"0{AlignHorz:Near;}Style11{}Style12{}Style13{}Style27{}Style29{AlignHorz:Near;}St" +
				"yle28{AlignHorz:Near;}Style26{}Style25{}Style9{}Style8{}Style24{}Style23{AlignHo" +
				"rz:Near;}Style5{}Style4{}Style7{}Style6{}Style1{}Style22{AlignHorz:Near;}Style3{" +
				"}Style2{}Style21{}Style20{}OddRow{}Style49{}Style38{}Style39{}Style36{}Style37{}" +
				"Style34{AlignHorz:Near;}Style35{AlignHorz:Near;}Style32{}Style33{}Style30{}Style" +
				"31{}Style48{}Normal{Font:Verdana, 8.25pt;}Style41{AlignHorz:Near;}Style40{AlignH" +
				"orz:Near;}Style43{}Style42{}Style45{}Style44{}Style47{AlignHorz:Far;}Style46{Ali" +
				"gnHorz:Near;}EvenRow{BackColor:Aqua;}Style59{AlignHorz:Near;}Style58{AlignHorz:N" +
				"ear;}RecordSelector{AlignImage:Center;}Style51{}Style50{}Footer{}Style52{AlignHo" +
				"rz:Near;}Style53{AlignHorz:Far;}Style54{}Style55{}Style56{}Style57{}Caption{Alig" +
				"nHorz:Center;}Style69{}Style68{}Style63{}Style62{}Style61{}Style60{}Style67{}Sty" +
				"le66{}Style65{AlignHorz:Near;}Style64{AlignHorz:Near;}Group{BackColor:ControlDar" +
				"k;Border:None,,0, 0, 0, 0;AlignVert:Center;}</Data></Styles><Splits><C1.Win.C1Tr" +
				"ueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" Name=\"\" AllowRowSizing=\"N" +
				"one\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendR" +
				"ightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=\"SolidCellBorder\" RecordSelector" +
				"Width=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"" +
				"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"S" +
				"tyle5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"Fi" +
				"lterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle p" +
				"arent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighL" +
				"ightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive" +
				"\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle " +
				"parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Styl" +
				"e6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style1" +
				"9\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style20\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15<" +
				"/Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
				"\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle paren" +
				"t=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHead" +
				"erStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Sty" +
				"le26\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>" +
				"1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=" +
				"\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Column" +
				"Divider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>2</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style" +
				" parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><Edi" +
				"torStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"St" +
				"yle39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible" +
				"><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>3</DCId" +
				"x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" " +
				"/><Style parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60" +
				"\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1" +
				"\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True<" +
				"/Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx" +
				">7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent" +
				"=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visib" +
				"le>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Heigh" +
				"t><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
				"2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Sty" +
				"le3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyl" +
				"e parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" " +
				"/><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>1" +
				"5</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle paren" +
				"t=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle par" +
				"ent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHe" +
				"aderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"S" +
				"tyle56\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><" +
				"Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\" me=\"Style65\" /><FooterS" +
				"tyle parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"Style67\" />" +
				"<GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupFooterStyle parent=\"Style" +
				"1\" me=\"Style68\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Height>15</Height><DCIdx>8</DCIdx></C1DisplayColumn></internalCols><Clie" +
				"ntRect>0, 17, 1020, 211</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBG" +
				"rid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent" +
				"=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"He" +
				"ading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Nor" +
				"mal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\"" +
				" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal" +
				"\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Nor" +
				"mal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSp" +
				"lits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRe" +
				"cSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 1020, 228</ClientArea><PrintP" +
				"ageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Styl" +
				"e15\" /></Blob>";
			// 
			// TopPanel
			// 
			this.TopPanel.Controls.Add(this.ItemsGrid);
			this.TopPanel.Controls.Add(this.vSplitter);
			this.TopPanel.Controls.Add(this.ListsGrid);
			this.TopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TopPanel.Location = new System.Drawing.Point(0, 40);
			this.TopPanel.Name = "TopPanel";
			this.TopPanel.Size = new System.Drawing.Size(1024, 493);
			this.TopPanel.TabIndex = 1;
			// 
			// ItemsGrid
			// 
			this.ItemsGrid.AllowAddNew = true;
			this.ItemsGrid.AllowColSelect = false;
			this.ItemsGrid.AllowFilter = false;
			this.ItemsGrid.AllowRowSelect = false;
			this.ItemsGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.ItemsGrid.Caption = "Items";
			this.ItemsGrid.CaptionHeight = 17;
			this.ItemsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ItemsGrid.EmptyRows = true;
			this.ItemsGrid.ExtendRightColumn = true;
			this.ItemsGrid.FetchRowStyles = true;
			this.ItemsGrid.FilterBar = true;
			this.ItemsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ItemsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.ItemsGrid.Location = new System.Drawing.Point(0, 219);
			this.ItemsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.ItemsGrid.Name = "ItemsGrid";
			this.ItemsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ItemsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ItemsGrid.PreviewInfo.ZoomFactor = 75;
			this.ItemsGrid.RecordSelectorWidth = 16;
			this.ItemsGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.ItemsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ItemsGrid.RowHeight = 15;
			this.ItemsGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.ItemsGrid.Size = new System.Drawing.Size(1024, 274);
			this.ItemsGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
			this.ItemsGrid.TabIndex = 1;
			this.ItemsGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.ItemsGrid_Paint);
			this.ItemsGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ItemsGrid_BeforeUpdate);
			this.ItemsGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.ItemsGrid_BeforeColUpdate);
			this.ItemsGrid.FilterChange += new System.EventHandler(this.ActivityGrid_FilterChange);
			this.ItemsGrid.OnAddNew += new System.EventHandler(this.ItemsGrid_OnAddNew);
			this.ItemsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.ItemsGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ItemsGrid_KeyPress);
			this.ItemsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"BizDate\" Da" +
				"taField=\"BizDate\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"BookGroup\" DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"List Name\" DataField=\"ListName\"><ValueI" +
				"tems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Security ID\"" +
				" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Quantity\" DataField=\"Quantity\" NumberFormat=\"FormatText Event\"><Va" +
				"lueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"C\" DataF" +
				"ield=\"CollateralCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"Rate Min\" DataField=\"RateMin\" NumberFormat=\"FormatText Event\"><" +
				"ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"\" Data" +
				"Field=\"RateMinCode\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Price Max\" DataField=\"PriceMax\" N" +
				"umberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1Data" +
				"Column Level=\"0\" Caption=\"I\" DataField=\"IncomeTracked\"><ValueItems Presentation=" +
				"\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Margin" +
				"\" DataField=\"Margin\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo />" +
				"</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"\" DataField=\"MarginCode\"><ValueI" +
				"tems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Comment\" Dat" +
				"aField=\"Comment\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"" +
				"0\" Caption=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn" +
				"><C1DataColumn Level=\"0\" Caption=\"Actor\" DataField=\"ActUserShortName\"><ValueItem" +
				"s /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"ActTime\" DataFi" +
				"eld=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"DivRate\" DataField=\"DivRate\"><ValueItem" +
				"s /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Des" +
				"ign.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlig" +
				"ht;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Style78{}S" +
				"tyle79{}Style85{}Editor{}Style117{}Style116{}Style72{}Style73{}Style70{AlignHorz" +
				":Near;}Style71{AlignHorz:Far;}Style76{AlignHorz:Center;}Style77{AlignHorz:Center" +
				";AlignVert:Top;}Style74{}Style75{}Style84{}Style87{}Style86{}Style81{}Style80{}S" +
				"tyle83{AlignHorz:Far;}Style82{AlignHorz:Near;}Footer{}Style89{AlignHorz:Center;}" +
				"Style88{AlignHorz:Center;}Style108{}Style109{}Style104{}Style105{}Style106{Align" +
				"Horz:Near;}Style107{AlignHorz:Near;}Style100{AlignHorz:Near;}Style101{AlignHorz:" +
				"Near;}Style102{}Style103{}Style94{AlignHorz:Near;}Style95{AlignHorz:Near;}Style9" +
				"6{}Style97{}Style90{}Style91{}Style92{}Style93{}RecordSelector{AlignImage:Center" +
				";}Style98{}Style99{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, " +
				"1;ForeColor:ControlText;AlignVert:Center;}Style18{}Style19{}Style14{}Style15{}St" +
				"yle16{AlignHorz:Near;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}S" +
				"tyle12{}Style13{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style22{A" +
				"lignHorz:Near;}Style27{}Style29{AlignHorz:Near;}Style28{AlignHorz:Near;}Style9{}" +
				"Style8{}Style26{}Style25{}Style5{}Style4{}Style7{}Style6{}Style24{}Style23{Align" +
				"Horz:Near;}Style3{}Style2{}Style21{}Style20{}OddRow{}Style110{}Style38{}Style39{" +
				"}Style36{}FilterBar{}Style34{AlignHorz:Near;}Style35{AlignHorz:Near;}Style32{}St" +
				"yle33{}Style30{}Style49{}Style48{}Style31{}Style37{}Normal{Font:Verdana, 8.25pt;" +
				"}Style41{AlignHorz:Near;}Style40{AlignHorz:Near;}Style43{}Style42{}Style45{}Styl" +
				"e44{}Style47{AlignHorz:Far;}Style46{AlignHorz:Near;}EvenRow{BackColor:Aqua;}Styl" +
				"e58{AlignHorz:Near;}Style59{AlignHorz:Far;}Style50{}Style51{}Style52{AlignHorz:C" +
				"enter;}Style53{AlignHorz:Center;}Style54{}Style55{}Style56{}Style57{}Style115{}C" +
				"aption{AlignHorz:Center;}Style112{AlignHorz:Near;}Style69{}Style68{}Style1{}Styl" +
				"e63{}Style62{}Style61{}Style60{}Style67{}Style66{}Style65{AlignHorz:Far;}Style64" +
				"{AlignHorz:Near;}Style114{}Style111{}Group{AlignVert:Center;Border:None,,0, 0, 0" +
				", 0;BackColor:ControlDark;}Style113{AlignHorz:Near;}</Data></Styles><Splits><C1." +
				"Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSelect=\"F" +
				"alse\" AllowRowSelect=\"False\" Name=\"\" AllowRowSizing=\"None\" CaptionHeight=\"17\" Co" +
				"lumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FetchRow" +
				"Styles=\"True\" FilterBar=\"True\" MarqueeStyle=\"SolidCellBorder\" RecordSelectorWidt" +
				"h=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><Ca" +
				"ptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style" +
				"5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"Filter" +
				"Bar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle paren" +
				"t=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLight" +
				"RowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me" +
				"=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle pare" +
				"nt=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" " +
				"/><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingSt" +
				"yle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><Footer" +
				"Style parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /" +
				"><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Styl" +
				"e1\" me=\"Style20\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Hei" +
				"ght><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
				"le2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"S" +
				"tyle3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderSt" +
				"yle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26" +
				"\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>1</D" +
				"CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style2" +
				"8\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Styl" +
				"e30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Sty" +
				"le1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><ColumnDivi" +
				"der>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>2</DCIdx></C1Displa" +
				"yColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style par" +
				"ent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorS" +
				"tyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style4" +
				"5\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><Co" +
				"lumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>3</DCIdx></" +
				"C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style100\" /><" +
				"Style parent=\"Style1\" me=\"Style101\" /><FooterStyle parent=\"Style3\" me=\"Style102\"" +
				" /><EditorStyle parent=\"Style5\" me=\"Style103\" /><GroupHeaderStyle parent=\"Style1" +
				"\" me=\"Style105\" /><GroupFooterStyle parent=\"Style1\" me=\"Style104\" /><Visible>Tru" +
				"e</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><All" +
				"owFocus>False</AllowFocus><Locked>True</Locked><DCIdx>13</DCIdx></C1DisplayColum" +
				"n><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"S" +
				"tyle1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle p" +
				"arent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><" +
				"GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDi" +
				"vider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>4</DCIdx></C1Disp" +
				"layColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style p" +
				"arent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><Edito" +
				"rStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Styl" +
				"e57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><" +
				"ColumnDivider>DarkGray,Single</ColumnDivider><Width>40</Width><Height>15</Height" +
				"><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Styl" +
				"e3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle" +
				" parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /" +
				"><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15" +
				"</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent" +
				"=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\" me=\"Style65\" /><FooterStyle pare" +
				"nt=\"Style3\" me=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"Style67\" /><GroupHea" +
				"derStyle parent=\"Style1\" me=\"Style69\" /><GroupFooterStyle parent=\"Style1\" me=\"St" +
				"yle68\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><W" +
				"idth>20</Width><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayCo" +
				"lumn><HeadingStyle parent=\"Style2\" me=\"Style70\" /><Style parent=\"Style1\" me=\"Sty" +
				"le71\" /><FooterStyle parent=\"Style3\" me=\"Style72\" /><EditorStyle parent=\"Style5\"" +
				" me=\"Style73\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style75\" /><GroupFooterSty" +
				"le parent=\"Style1\" me=\"Style74\" /><Visible>True</Visible><ColumnDivider>DarkGray" +
				",Single</ColumnDivider><Height>15</Height><DCIdx>8</DCIdx></C1DisplayColumn><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\"" +
				" me=\"Style77\" /><FooterStyle parent=\"Style3\" me=\"Style78\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style79\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style80\" /><Visible>True</Visible><ColumnDivider>" +
				"DarkGray,Single</ColumnDivider><Width>40</Width><Height>15</Height><DCIdx>9</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style82\"" +
				" /><Style parent=\"Style1\" me=\"Style83\" /><FooterStyle parent=\"Style3\" me=\"Style8" +
				"4\" /><EditorStyle parent=\"Style5\" me=\"Style85\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style87\" /><GroupFooterStyle parent=\"Style1\" me=\"Style86\" /><Visible>True" +
				"</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCId" +
				"x>10</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style88\" /><Style parent=\"Style1\" me=\"Style89\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style90\" /><EditorStyle parent=\"Style5\" me=\"Style91\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style93\" /><GroupFooterStyle parent=\"Style1\" me=\"Style92\" /><Vis" +
				"ible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>20</Widt" +
				"h><Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
				"gStyle parent=\"Style2\" me=\"Style94\" /><Style parent=\"Style1\" me=\"Style95\" /><Foo" +
				"terStyle parent=\"Style3\" me=\"Style96\" /><EditorStyle parent=\"Style5\" me=\"Style97" +
				"\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style99\" /><GroupFooterStyle parent=\"S" +
				"tyle1\" me=\"Style98\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Col" +
				"umnDivider><Height>15</Height><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayColum" +
				"n><HeadingStyle parent=\"Style2\" me=\"Style106\" /><Style parent=\"Style1\" me=\"Style" +
				"107\" /><FooterStyle parent=\"Style3\" me=\"Style108\" /><EditorStyle parent=\"Style5\"" +
				" me=\"Style109\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style111\" /><GroupFooterS" +
				"tyle parent=\"Style1\" me=\"Style110\" /><Visible>True</Visible><ColumnDivider>DarkG" +
				"ray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx>14</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style112" +
				"\" /><Style parent=\"Style1\" me=\"Style113\" /><FooterStyle parent=\"Style3\" me=\"Styl" +
				"e114\" /><EditorStyle parent=\"Style5\" me=\"Style115\" /><GroupHeaderStyle parent=\"S" +
				"tyle1\" me=\"Style117\" /><GroupFooterStyle parent=\"Style1\" me=\"Style116\" /><Visibl" +
				"e>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height" +
				"><Locked>True</Locked><DCIdx>15</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style3" +
				"7\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style38\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15<" +
				"/Height><DCIdx>16</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 17, 102" +
				"0, 253</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></S" +
				"plits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Hea" +
				"ding\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Captio" +
				"n\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected" +
				"\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow" +
				"\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><" +
				"Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBa" +
				"r\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplit" +
				"s><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</Def" +
				"aultRecSelWidth><ClientArea>0, 0, 1020, 270</ClientArea><PrintPageHeaderStyle pa" +
				"rent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// vSplitter
			// 
			this.vSplitter.Dock = System.Windows.Forms.DockStyle.Top;
			this.vSplitter.Location = new System.Drawing.Point(0, 216);
			this.vSplitter.Name = "vSplitter";
			this.vSplitter.Size = new System.Drawing.Size(1024, 3);
			this.vSplitter.TabIndex = 2;
			this.vSplitter.TabStop = false;
			// 
			// ListsGrid
			// 
			this.ListsGrid.AllowAddNew = true;
			this.ListsGrid.AllowDelete = true;
			this.ListsGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.ListsGrid.Caption = "Lists";
			this.ListsGrid.CaptionHeight = 17;
			this.ListsGrid.ContextMenu = this.contextMenu1;
			this.ListsGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.ListsGrid.Dock = System.Windows.Forms.DockStyle.Top;
			this.ListsGrid.EmptyRows = true;
			this.ListsGrid.ExtendRightColumn = true;
			this.ListsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ListsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.ListsGrid.Location = new System.Drawing.Point(0, 0);
			this.ListsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.ListsGrid.Name = "ListsGrid";
			this.ListsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ListsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ListsGrid.PreviewInfo.ZoomFactor = 75;
			this.ListsGrid.RecordSelectorWidth = 16;
			this.ListsGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.ListsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ListsGrid.RowHeight = 15;
			this.ListsGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.ListsGrid.Size = new System.Drawing.Size(1024, 216);
			this.ListsGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
			this.ListsGrid.TabIndex = 0;
			this.ListsGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.ListsGrid_Paint);
			this.ListsGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ListsGrid_BeforeUpdate);
			this.ListsGrid.OnAddNew += new System.EventHandler(this.ListsGrid_OnAddNew);
			this.ListsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.ListsGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListsGrid_KeyPress);
			this.ListsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"BookGroup\" " +
				"DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"List Name\" DataField=\"ListName\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"Books\" DataField=\"Books\"><ValueItems" +
				" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"WT (Mins)\" DataF" +
				"ield=\"WaitTime\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
				"\" Caption=\"B\" DataField=\"BookContract\"><ValueItems Presentation=\"CheckBox\" /><Gr" +
				"oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"BC\" DataField=\"BatchCo" +
				"de\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"P" +
				"C\" DataField=\"PoolCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
				"Level=\"0\" Caption=\"Item Count\" DataField=\"ItemCount\"><ValueItems /><GroupInfo />" +
				"</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Filled\" DataField=\"Filled\"><Valu" +
				"eItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"S\" DataFie" +
				"ld=\"ListStatus\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
				"\" Caption=\"Actor\" DataField=\"ActUserShortName\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"ActTime\" DataField=\"ActTime\" NumberFor" +
				"mat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"BizDate\" DataField=\"BizDate\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Da" +
				"ta>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style85{}Inactive{F" +
				"oreColor:InactiveCaptionText;BackColor:InactiveCaption;}Style78{}Style79{}Select" +
				"ed{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style72{}Style73{}Style7" +
				"0{AlignHorz:Center;}Style71{AlignHorz:Center;}Style76{AlignHorz:Near;}Style77{Al" +
				"ignHorz:Near;}Style74{}Style75{}Style84{}Style87{}Style86{}Style81{}Style80{}Sty" +
				"le83{AlignHorz:Near;}Style82{AlignHorz:Near;}Footer{}Style89{AlignHorz:Near;}Sty" +
				"le88{AlignHorz:Near;}Style90{}Style91{}Style92{}Style93{}RecordSelector{AlignIma" +
				"ge:Center;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColo" +
				"r:ControlText;BackColor:Control;}Style18{}Style19{}Style14{}Style15{}Style16{Ali" +
				"gnHorz:Near;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12{}S" +
				"tyle13{}Style29{AlignHorz:Near;}Style28{AlignHorz:Near;}Style27{}Style22{AlignHo" +
				"rz:Near;}Style26{}Style9{}Style8{}Style25{}Style24{}Style5{}Style4{}Style7{}Styl" +
				"e6{}Style1{}Style23{AlignHorz:Near;}Style3{}Style2{}Style21{}Style20{}OddRow{}St" +
				"yle49{}Style38{}Style39{}Style36{}FilterBar{}Style34{AlignHorz:Center;}Style35{A" +
				"lignHorz:Center;AlignVert:Top;}Style32{}Style33{}Style30{}Style37{}Style48{}Styl" +
				"e31{}Normal{Font:Verdana, 8.25pt;}Style41{AlignHorz:Center;}Style40{AlignHorz:Ce" +
				"nter;}Style43{}Style42{}Style45{}Style44{}Style47{AlignHorz:Center;}Style46{Alig" +
				"nHorz:Center;}EvenRow{BackColor:Aqua;}Style58{AlignHorz:Near;}Style59{AlignHorz:" +
				"Far;}Style50{}Style51{}Style52{AlignHorz:Near;}Style53{AlignHorz:Near;}Style54{}" +
				"Style55{}Style56{}Style57{}Caption{AlignHorz:Center;}Style69{}Style68{}Style63{}" +
				"Style62{}Style61{}Style60{}Style67{}Style66{}Style65{AlignHorz:Far;}Style64{Alig" +
				"nHorz:Near;}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center" +
				";}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarSt" +
				"yle=\"Always\" Name=\"\" AllowRowSizing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeigh" +
				"t=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" MarqueeStyle=\"SolidCellB" +
				"order\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" Hori" +
				"zontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle " +
				"parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><Filt" +
				"erBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"St" +
				"yle3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\"" +
				" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveS" +
				"tyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" />" +
				"<RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle paren" +
				"t=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C" +
				"1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style88\" /><Style parent=\"Style" +
				"1\" me=\"Style89\" /><FooterStyle parent=\"Style3\" me=\"Style90\" /><EditorStyle paren" +
				"t=\"Style5\" me=\"Style91\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style93\" /><Grou" +
				"pFooterStyle parent=\"Style1\" me=\"Style92\" /><ColumnDivider>DarkGray,Single</Colu" +
				"mnDivider><Height>15</Height><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayColumn" +
				"><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53" +
				"\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=" +
				"\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle p" +
				"arent=\"Style1\" me=\"Style56\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Hei" +
				"ght>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
				"parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyl" +
				"e parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><Gr" +
				"oupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" " +
				"me=\"Style20\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivi" +
				"der><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style2" +
				"5\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Co" +
				"lumnDivider><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColum" +
				"n><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style2" +
				"9\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me" +
				"=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle " +
				"parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Si" +
				"ngle</ColumnDivider><Width>70</Width><Height>15</Height><DCIdx>3</DCIdx></C1Disp" +
				"layColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style p" +
				"arent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><Edito" +
				"rStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Styl" +
				"e39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><" +
				"ColumnDivider>DarkGray,Single</ColumnDivider><Width>29</Width><Height>15</Height" +
				"><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Styl" +
				"e3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle" +
				" parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /" +
				"><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>40<" +
				"/Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" />" +
				"<FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
				"le49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle paren" +
				"t=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single<" +
				"/ColumnDivider><Width>40</Width><Height>15</Height><DCIdx>6</DCIdx></C1DisplayCo" +
				"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent" +
				"=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible><Colum" +
				"nDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>7</DCIdx></C1D" +
				"isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Styl" +
				"e parent=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><Ed" +
				"itorStyle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"S" +
				"tyle69\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visibl" +
				"e><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>8</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style70\"" +
				" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" me=\"Style7" +
				"2\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Visible>True" +
				"</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>40</Width><Height" +
				">15</Height><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><Group" +
				"HeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=" +
				"\"Style80\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider" +
				"><Height>15</Height><Locked>True</Locked><DCIdx>10</DCIdx></C1DisplayColumn><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style82\" /><Style parent=\"Style1\"" +
				" me=\"Style83\" /><FooterStyle parent=\"Style3\" me=\"Style84\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style85\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style87\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style86\" /><Visible>True</Visible><ColumnDivider>" +
				"DarkGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx>11" +
				"</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 17, 1020, 195</ClientRec" +
				"t><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyle" +
				"s><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style pa" +
				"rent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style paren" +
				"t=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent" +
				"=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent" +
				"=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Hea" +
				"ding\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style paren" +
				"t=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</" +
				"horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><" +
				"ClientArea>0, 0, 1020, 212</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style" +
				"14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.SendToMenuItem,
																																								 this.menuItem3,
																																								 this.ExitMenuItem});
			// 
			// SendToMenuItem
			// 
			this.SendToMenuItem.Index = 0;
			this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.SendToProcessAgentMenuItem});
			this.SendToMenuItem.Text = "Send To";
			// 
			// SendToProcessAgentMenuItem
			// 
			this.SendToProcessAgentMenuItem.Index = 0;
			this.SendToProcessAgentMenuItem.Text = "Process Agent";
			this.SendToProcessAgentMenuItem.Click += new System.EventHandler(this.SendToProcessAgentMenuItem_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 2;
			this.ExitMenuItem.Text = "Exit";
			// 
			// BookGroupNameLabel
			// 
			this.BookGroupNameLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BookGroupNameLabel.ForeColor = System.Drawing.Color.Navy;
			this.BookGroupNameLabel.Location = new System.Drawing.Point(200, 8);
			this.BookGroupNameLabel.Name = "BookGroupNameLabel";
			this.BookGroupNameLabel.Size = new System.Drawing.Size(348, 20);
			this.BookGroupNameLabel.TabIndex = 9;
			this.BookGroupNameLabel.Tag = null;
			this.BookGroupNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// BookGroupLabel
			// 
			this.BookGroupLabel.Location = new System.Drawing.Point(8, 8);
			this.BookGroupLabel.Name = "BookGroupLabel";
			this.BookGroupLabel.Size = new System.Drawing.Size(92, 20);
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
			this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
			this.BookGroupCombo.ItemHeight = 15;
			this.BookGroupCombo.KeepForeColor = true;
			this.BookGroupCombo.LimitToList = true;
			this.BookGroupCombo.Location = new System.Drawing.Point(104, 8);
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
			this.BookGroupCombo.TabIndex = 8;
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
			// hSplitter
			// 
			this.hSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.hSplitter.Location = new System.Drawing.Point(0, 530);
			this.hSplitter.Name = "hSplitter";
			this.hSplitter.Size = new System.Drawing.Size(1024, 3);
			this.hSplitter.TabIndex = 10;
			this.hSplitter.TabStop = false;
			// 
			// RefreshButton
			// 
			this.RefreshButton.Location = new System.Drawing.Point(608, 8);
			this.RefreshButton.Name = "RefreshButton";
			this.RefreshButton.Size = new System.Drawing.Size(128, 24);
			this.RefreshButton.TabIndex = 11;
			this.RefreshButton.Text = "Refresh";
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// PositionAutoBorrowForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1024, 765);
			this.Controls.Add(this.RefreshButton);
			this.Controls.Add(this.hSplitter);
			this.Controls.Add(this.BookGroupNameLabel);
			this.Controls.Add(this.BookGroupLabel);
			this.Controls.Add(this.BookGroupCombo);
			this.Controls.Add(this.TopPanel);
			this.Controls.Add(this.ResultsGrid);
			this.DockPadding.Top = 40;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PositionAutoBorrowForm";
			this.Text = "Position - AutoBorrow";
			this.Load += new System.EventHandler(this.PositionAutoBorrowForm_Load);
			this.Closed += new System.EventHandler(this.PositionAutoBorrowForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).EndInit();
			this.TopPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ItemsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ListsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void PositionAutoBorrowForm_Load(object sender, System.EventArgs e)
		{
			int  height  = mainForm.Height - 275;
			int  width   = mainForm.Width  - 45;                  
      
			this.Top    = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
			this.Left   = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
			this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
			this.Width  = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));      
    
			this.Show();
			this.Cursor = Cursors.WaitCursor;
			Application.DoEvents();

			mainForm.Alert("Please wait ... loading current bank loan data.", PilotState.Idle);

			try
			{
				dataSet = mainForm.PositionAgent.AutoBorrowDataGet(mainForm.ServiceAgent.ContractsBizDate(),mainForm.UtcOffset, mainForm.UserId, "PositionAuto");				
				
				BookGroupCombo.HoldFields();
				BookGroupCombo.DataSource = dataSet.Tables["BookGroups"];
				BookGroupCombo.SelectedIndex = -1;								    										
				
				autoBorrowListsViewRowFilter = "BookGroup = ''";
				autoBorrowItemsViewRowFilter = "BookGroup = ''";;
				autoBorrowResultsViewRowFilter = "BookGroup = ''";;
				
				autoBorrowListsView = new DataView(dataSet.Tables["AutoBorrowLists"], autoBorrowListsViewRowFilter, "", DataViewRowState.CurrentRows);
				autoBorrowItemsView = new DataView(dataSet.Tables["AutoBorrowItems"], autoBorrowItemsViewRowFilter, "", DataViewRowState.CurrentRows);
				autoBorrowResultsView = new DataView(dataSet.Tables["AutoBorrowResults"], autoBorrowResultsViewRowFilter, "", DataViewRowState.CurrentRows);
				
				ListsGrid.SetDataBinding(autoBorrowListsView, "", true);	
				ItemsGrid.SetDataBinding(autoBorrowItemsView, "", true);
				ResultsGrid.SetDataBinding(autoBorrowResultsView, "", true);

				BookGroupCombo.Text = RegistryValue.Read(this.Name, "BookGroup", ""); 							
				mainForm.Alert("Please wait ... loading current autoborrow data ... Done!.", PilotState.Idle);			
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionAutoBorrowForm.PositionAutoBorrowForm_Load]", Log.Error, 1); 				
				mainForm.Alert("Please wait ... loading current autoborrow data ... Error!.", PilotState.RunFault);
			}			
			
			this.Cursor = Cursors.Default;
		}

		private void PositionAutoBorrowForm_Closed(object sender, System.EventArgs e)
		{
			RegistryValue.Write(this.Name, "BookGroup", BookGroupCombo.Text);

			if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
			}

			///mainForm.positionAutoBorrowForm = null;
		}

		private void BookGroupCombo_RowChange(object sender, System.EventArgs e)
		{
			if (!(BookGroupCombo.Text.Equals("")))
			{		  												
				try
				{											
					if (bool.Parse(dataSet.Tables["BookGroups"].Rows[BookGroupCombo.WillChangeToIndex]["MayView"].ToString()))
					{  						  			  									
						BookGroupNameLabel.DataSource = dataSet.Tables["BookGroups"];
						BookGroupNameLabel.DataField = "BookName"; 												
				
						autoBorrowListsViewRowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
						autoBorrowListsView.RowFilter = autoBorrowListsViewRowFilter;

						if (bool.Parse(dataSet.Tables["BookGroups"].Rows[BookGroupCombo.WillChangeToIndex]["MayEdit"].ToString()))
						{				
											
						}
						else
						{
							mainForm.Alert("User: " + mainForm.UserId + ", Permission to EDIT denied.");
						}		  
					}
					else
					{	
						mainForm.Alert("User: " + mainForm.UserId + ", Permission to VIEW denied.");				
					}
				}		
				catch (Exception error)
				{
					Log.Write(error.Message + ". [PositionAutoBorrowForm.BookGroupCombo_RowChange]", Log.Error, 1); 				
					mainForm.Alert(error.Message, PilotState.RunFault);
				}
			}
		}

		

		private void FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch(e.Column.DataField)
			{				
				case "ActTime":
					e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeShortFormat);
					break;
					
				case "Quantity":
				case "QuantityMin":
				case "QuantityDelivered":
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch {}
					break;							

				case "PriceMax":
				case "RateMin":
				case "Margin":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.00");
					}
					catch {}
					break;
			}
		}

		private void ActivityGrid_FilterChange(object sender, System.EventArgs e)
		{
			string gridFilter = "";

			try
			{//				gridFilter = mainForm.GridFilterGet(ref ActivityGrid);

				if (gridFilter.Equals(""))
				{

				}
				else
				{

				}				
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		
		private void DockTopMenuItem_Click(object sender, System.EventArgs e)
		{
			this.FormBorderStyle = FormBorderStyle.Sizable;
			this.Dock = DockStyle.Top;
			this.ControlBox = false;
			this.Text = "";
		}

		private void DockBottomMenuItem_Click(object sender, System.EventArgs e)
		{
			this.FormBorderStyle = FormBorderStyle.Sizable;
			this.Dock = DockStyle.Bottom;
			this.ControlBox = false;
			this.Text = "";
		}

		private void DockNoneMenuItem_Click(object sender, System.EventArgs e)
		{
			this.FormBorderStyle = FormBorderStyle.Sizable;
			this.Dock = DockStyle.None;
			this.ControlBox = true;
			this.Text = TEXT;
		}

		
		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void ListsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{			
			try
			{
				mainForm.PositionAgent.AutoBorrowListSet(
					ListsGrid.Columns["BookGroup"].Text,
					ListsGrid.Columns["ListName"].Text,
					ListsGrid.Columns["Books"].Text,
					ListsGrid.Columns["WaitTime"].Text,
					bool.Parse(ListsGrid.Columns["BookContract"].Value.ToString()),
					ListsGrid.Columns["BatchCode"].Text,
					ListsGrid.Columns["PoolCode"].Text,
					int.Parse(ListsGrid.Columns["ItemCount"].Text),
					int.Parse(ListsGrid.Columns["Filled"].Text),
					ListsGrid.Columns["ListStatus"].Text,
					mainForm.UserId);
			
				ListsGrid.Columns["Actor"].Text = mainForm.UserId;
				ListsGrid.Columns["ActTime"].Value = DateTime.Now.ToString();				
				
				listName = ListsGrid.Columns["ListName"].Text;
				autoBorrowItemsViewRowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ListName = '" + listName + "'";
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
				mainForm.Alert(error.Message);
				e.Cancel = true;
			}
		}		

		private void ListsGrid_OnAddNew(object sender, System.EventArgs e)
		{
			ListsGrid.Columns["BizDate"].Text = mainForm.ServiceAgent.ContractsBizDate();
			ListsGrid.Columns["BookGroup"].Text = BookGroupCombo.Text;
			ListsGrid.Columns["WaitTime"].Text = mainForm.ServiceAgent.KeyValueGet("AutoBorrowWaitTime", "30");
			ListsGrid.Columns["BookContract"].Value = true;
			ListsGrid.Columns["BatchCode"].Text = mainForm.ServiceAgent.KeyValueGet("AutoBorrowBatchCode", "A");
			ListsGrid.Columns["PoolCode"].Text = mainForm.ServiceAgent.KeyValueGet("AutoBorrowPoolCode", "L");			
			ListsGrid.Columns["Filled"].Text = "0";
			ListsGrid.Columns["ListStatus"].Text = "N";
		}

		private void RefreshButton_Click(object sender, System.EventArgs e)
		{
			int lastRow = ListsGrid.Row;
			mainForm.Alert("Please wait ... refreshing current bank loan data.", PilotState.Idle);
			
			
			try
			{
				dataSet = mainForm.PositionAgent.AutoBorrowDataGet(mainForm.ServiceAgent.ContractsBizDate(), mainForm.UtcOffset, mainForm.UserId, "PositionAuto");				
				
				BookGroupCombo.HoldFields();
				BookGroupCombo.DataSource = dataSet.Tables["BookGroups"];
				BookGroupCombo.SelectedIndex = -1;								    										
				
				autoBorrowListsView.Table  = dataSet.Tables["AutoBorrowLists"];
				autoBorrowListsView.RowFilter = autoBorrowListsViewRowFilter;

				autoBorrowItemsView.Table  = dataSet.Tables["AutoBorrowItems"];
				autoBorrowItemsView.RowFilter = autoBorrowItemsViewRowFilter;
				
				autoBorrowResultsView.Table  = dataSet.Tables["AutoBorrowResults"];
				autoBorrowResultsView.RowFilter = autoBorrowResultsViewRowFilter;
			
				ListsGrid.SetDataBinding(autoBorrowListsView, "", true);	
				ItemsGrid.SetDataBinding(autoBorrowItemsView, "", true);
				ResultsGrid.SetDataBinding(autoBorrowResultsView, "", true);

				BookGroupCombo.Text = RegistryValue.Read(this.Name, "BookGroup", ""); 							

				mainForm.Alert("Please wait ... refreshing current autoborrow data ... Done!.", PilotState.Idle);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionAutoBorrowForm.PositionAutoBorrowForm_Load]", Log.Error, 1); 				
				mainForm.Alert("Please wait ... refreshing current autoborrow data ... Error!.", PilotState.RunFault);
			}
			
			ListsGrid.Row = lastRow;
			this.Cursor = Cursors.Default;			
		}

		private void ListsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char) 13)
			{
				ListsGrid.UpdateData();
			}
		}

		private void ListsGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(!listName.Equals(ListsGrid.Columns["ListName"].Text))
			{
				listName = ListsGrid.Columns["ListName"].Text;
				autoBorrowItemsViewRowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ListName = '" + listName + "'";
				autoBorrowItemsView.RowFilter = autoBorrowItemsViewRowFilter;

				autoBorrowResultsViewRowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND ListName = '" + listName + "'";
				autoBorrowResultsView.RowFilter = autoBorrowResultsViewRowFilter;
			}
		}

		private void ItemsGrid_OnAddNew(object sender, System.EventArgs e)
		{
			ItemsGrid.Columns["BizDate"].Text = mainForm.ServiceAgent.ContractsBizDate();
			ItemsGrid.Columns["BookGroup"].Text = BookGroupCombo.Text;
			ItemsGrid.Columns["ListName"].Text = listName;
			ItemsGrid.Columns["CollateralCode"].Value = "C";
			ItemsGrid.Columns["IncomeTracked"].Value = true;
			ItemsGrid.Columns["PriceMax"].Value= "0.00";			
			ItemsGrid.Columns["RateMin"].Value = "0";
			ItemsGrid.Columns["RateMinCode"].Value = "T";
			ItemsGrid.Columns["Margin"].Value = "1.05";
			ItemsGrid.Columns["MarginCode"].Value = "%";
			ItemsGrid.Columns["DivRate"].Value = "100";
		}

		private void ItemsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				if (ItemsGrid.Columns["Quantity"].Text.Equals(""))
				{
					ItemsGrid.Row = 5;
					mainForm.Alert("Please enter a quantity", PilotState.RunFault);
					e.Cancel = true;
					return;
				}
									
					mainForm.PositionAgent.AutoBorrowItemSet(
					ItemsGrid.Columns["BookGroup"].Text,
					ItemsGrid.Columns["ListName"].Text,					
					ItemsGrid.Columns["SecId"].Text,
					ItemsGrid.Columns["Quantity"].Value.ToString(),
					ItemsGrid.Columns["CollateralCode"].Text,
					ItemsGrid.Columns["RateMin"].Text,
					ItemsGrid.Columns["RateMinCode"].Text,
					ItemsGrid.Columns["PriceMax"].Text,
					bool.Parse(ItemsGrid.Columns["IncomeTracked"].Value.ToString()),
					ItemsGrid.Columns["Margin"].Text,
					ItemsGrid.Columns["MarginCode"].Text,
					ItemsGrid.Columns["DivRate"].Value.ToString(),
					ItemsGrid.Columns["Comment"].Text,
					mainForm.UserId);
			
				ItemsGrid.Columns["Actor"].Text = mainForm.UserId;
				ItemsGrid.Columns["ActTime"].Value = DateTime.Now.ToString();								
			}
			catch (Exception error)
			{
				Log.Write(error.Message, 1);
				mainForm.Alert(error.Message);
				e.Cancel = true;
			}
		}

		private void ItemsGrid_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			if (!mainForm.Price.Equals(""))
			{
				if (e.Column.DataColumn.DataField.Equals("SecId") || e.Column.DataColumn.DataField.Equals("Margin"))
				{										
					ItemsGrid.Columns["PriceMax"].Value  = (decimal.Parse(mainForm.Price) * decimal.Parse(ItemsGrid.Columns["Margin"].Text)).ToString();											
				}
			
				ItemsGrid.Columns["SecId"].Text = mainForm.SecId;
				ItemsGrid.Columns["Symbol"].Text = mainForm.Symbol;
			}			
		}

		private void ItemsGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (!secId.Equals(ItemsGrid.Columns["SecId"].Text))
			{
				secId = ItemsGrid.Columns["SecId"].Text;
				mainForm.SecId = secId;		
			}
		}

		private void ItemsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char) 13)
			{
				ItemsGrid.UpdateData();
			}
		}

		private void SendToProcessAgentMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				ListsGrid.Columns["ListStatus"].Text = "P";
				
				ListsGrid.Columns["ListStatus"].Text = mainForm.PositionAgent.AutoBorrowListSend(
					ListsGrid.Columns["BizDate"].Text,
					ListsGrid.Columns["BookGroup"].Text,
					ListsGrid.Columns["ListName"].Text);			
				
				ListsGrid.UpdateData();
			}
			catch (Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
				ListsGrid.Columns["ListStatus"].Text = "E";
			}
		}
	}
}