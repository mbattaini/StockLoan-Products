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
	public class PositionBankLoanReleaseReportForm : System.Windows.Forms.Form
	{
		private const string TEXT = "Position - BankLoan - Release Report";		
		private MainForm mainForm;					
		private System.ComponentModel.Container components = null;
		private string bookGroup, reportName;
		
		private DataSet dataSet, dataSetDataMasks;
		private DataView dataView;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ReportsGrid;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ReportsDataMaskGrid;
		private string dataViewRowFilter;

		public PositionBankLoanReleaseReportForm(MainForm mainForm, string bookGroup)
		{
			try
			{
				this.mainForm = mainForm;
				InitializeComponent();		

				this.bookGroup = bookGroup;	
				this.reportName = "";
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanReleaseReportForm.PositionBankLoanReleaseReportForm]", Log.Error, 1); 
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionBankLoanReleaseReportForm));
			this.ReportsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.ReportsDataMaskGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.ReportsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ReportsDataMaskGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// ReportsGrid
			// 
			this.ReportsGrid.AllowAddNew = true;
			this.ReportsGrid.AllowColSelect = false;
			this.ReportsGrid.AllowFilter = false;
			this.ReportsGrid.AllowRowSelect = false;
			this.ReportsGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.ReportsGrid.Caption = "Reports";
			this.ReportsGrid.CaptionHeight = 17;
			this.ReportsGrid.ChildGrid = this.ReportsDataMaskGrid;
			this.ReportsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ReportsGrid.EmptyRows = true;
			this.ReportsGrid.ExtendRightColumn = true;
			this.ReportsGrid.FetchRowStyles = true;
			this.ReportsGrid.FilterBar = true;
			this.ReportsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ReportsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.ReportsGrid.Location = new System.Drawing.Point(0, 0);
			this.ReportsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.ReportsGrid.Name = "ReportsGrid";
			this.ReportsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ReportsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ReportsGrid.PreviewInfo.ZoomFactor = 75;
			this.ReportsGrid.RecordSelectorWidth = 16;
			this.ReportsGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.ReportsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ReportsGrid.RowHeight = 15;
			this.ReportsGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.ReportsGrid.Size = new System.Drawing.Size(1072, 421);
			this.ReportsGrid.TabIndex = 11;
			this.ReportsGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.ReportsGrid_Paint);
			this.ReportsGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ReportsGrid_BeforeUpdate);
			this.ReportsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ReleaseReportGrid_FormatText);
			this.ReportsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"BookGroup\" " +
				"DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"ReportName\" DataField=\"ReportName\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Book Group\" DataField=\"BookGroup\"" +
				"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Repo" +
				"rt Name\" DataField=\"ReportName\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
				"aColumn Level=\"0\" Caption=\"Report Type\" DataField=\"ReportType\"><ValueItems /><Gr" +
				"oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Actor\" DataField=\"ActU" +
				"serId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption" +
				"=\"Act Time\" DataField=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /><G" +
				"roupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Report Host\" DataFiel" +
				"d=\"ReportHost\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
				" Caption=\"Report Host UserId\" DataField=\"ReportHostUserId\"><ValueItems /><GroupI" +
				"nfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Report Host Password\" Data" +
				"Field=\"ReportHostPassword\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColu" +
				"mn Level=\"0\" Caption=\"Report Path\" DataField=\"ReportPath\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"File Load Date\" DataField=\"" +
				"FileLoadDate\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"File Load Time\" DataField=\"FileLoadTime" +
				"\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn></Da" +
				"taCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightR" +
				"ow{ForeColor:HighlightText;BackColor:Highlight;}Inactive{ForeColor:InactiveCapti" +
				"onText;BackColor:InactiveCaption;}Style78{}Style79{}Selected{ForeColor:Highlight" +
				"Text;BackColor:Highlight;}Editor{}Style72{}Style73{}Style70{AlignHorz:Near;}Styl" +
				"e71{AlignHorz:Near;}Style76{AlignHorz:Near;}Style77{AlignHorz:Near;}Style74{}Sty" +
				"le75{}Style81{}Style80{}FilterBar{}Heading{Wrap:True;BackColor:Control;Border:Ra" +
				"ised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style18{}Style19{}Style" +
				"139{}Style138{}Style137{AlignHorz:Near;}Style136{AlignHorz:Near;}Style14{}Style1" +
				"5{}Style16{AlignHorz:Near;}Style17{AlignHorz:Near;}Style10{AlignHorz:Near;}Style" +
				"11{}Style12{}Style13{BackColor:SeaShell;}Style9{}Style8{}Style29{AlignHorz:Near;" +
				"BackColor:WhiteSmoke;}Style28{AlignHorz:Near;}Style27{}Style26{}Style25{}Style24" +
				"{}Style23{AlignHorz:Near;}Style22{AlignHorz:Near;}Style21{}Style20{}OddRow{}Styl" +
				"e144{}Style42{}Style38{}Style39{}Style36{}Style37{}Style34{AlignHorz:Near;}Style" +
				"35{AlignHorz:Near;BackColor:WhiteSmoke;}Style32{}Style33{}Style30{}Style49{}Styl" +
				"e48{}Style40{AlignHorz:Near;}Style43{}Style140{}Style141{}Style142{AlignHorz:Nea" +
				"r;}Style143{AlignHorz:Near;}Style41{AlignHorz:Near;}Style145{}Style146{}Style147" +
				"{}Style45{}Style44{}Style47{AlignHorz:Near;}Style46{AlignHorz:Near;}Normal{Font:" +
				"Verdana, 8.25pt;}Style31{}EvenRow{BackColor:Aqua;}Style51{}Style59{AlignHorz:Nea" +
				"r;}Style5{}Style4{}Style7{}Style6{}Style58{AlignHorz:Near;}RecordSelector{AlignI" +
				"mage:Center;}Style3{}Style2{}Style50{}Footer{}Style52{AlignHorz:Near;}Style53{Al" +
				"ignHorz:Near;BackColor:WhiteSmoke;}Style54{}Style55{}Style56{}Style57{}Caption{A" +
				"lignHorz:Center;}Style69{}Style68{}Style1{}Style63{}Style62{}Style61{}Style60{}S" +
				"tyle67{}Style66{}Style65{AlignHorz:Near;}Style64{AlignHorz:Near;}Group{AlignVert" +
				":Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}</Data></Styles><Splits><" +
				"C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSelect" +
				"=\"False\" AllowRowSelect=\"False\" Name=\"\" AllowRowSizing=\"None\" CaptionHeight=\"17\"" +
				" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" Fetch" +
				"RowStyles=\"True\" FilterBar=\"True\" MarqueeStyle=\"SolidCellBorder\" RecordSelectorW" +
				"idth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\">" +
				"<CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"St" +
				"yle5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"Fil" +
				"terBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle pa" +
				"rent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLi" +
				"ghtRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\"" +
				" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle p" +
				"arent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style" +
				"6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><Headin" +
				"gStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><Foo" +
				"terStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19" +
				"\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"S" +
				"tyle1\" me=\"Style20\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</" +
				"Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e26\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>1" +
				"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
				"le28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"S" +
				"tyle30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"" +
				"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible" +
				">True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height>" +
				"<DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\"" +
				" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style" +
				"3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle " +
				"parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" />" +
				"<Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15<" +
				"/Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
				"\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle paren" +
				"t=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHead" +
				"erStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Sty" +
				"le56\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><He" +
				"ight>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle" +
				" parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterSty" +
				"le parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><G" +
				"roupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\"" +
				" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDiv" +
				"ider><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><Head" +
				"ingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><F" +
				"ooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style" +
				"49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=" +
				"\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</C" +
				"olumnDivider><Width>192</Width><Height>15</Height><DCIdx>8</DCIdx></C1DisplayCol" +
				"umn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=" +
				"\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle" +
				" parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /" +
				"><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible><Column" +
				"Divider>DarkGray,Single</ColumnDivider><Width>189</Width><Height>15</Height><DCI" +
				"dx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style64\" /><Style parent=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style69\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Vis" +
				"ible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Hei" +
				"ght><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
				"yle2\" me=\"Style70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"" +
				"Style3\" me=\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderS" +
				"tyle parent=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style7" +
				"4\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Heigh" +
				"t>15</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
				"arent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><FooterStyle" +
				" parent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style79\" /><Gro" +
				"upHeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"Style1\" m" +
				"e=\"Style80\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivid" +
				"er><Height>15</Height><Locked>True</Locked><DCIdx>12</DCIdx></C1DisplayColumn><C" +
				"1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style136\" /><Style parent=\"Styl" +
				"e1\" me=\"Style137\" /><FooterStyle parent=\"Style3\" me=\"Style138\" /><EditorStyle pa" +
				"rent=\"Style5\" me=\"Style139\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style141\" />" +
				"<GroupFooterStyle parent=\"Style1\" me=\"Style140\" /><Visible>True</Visible><Column" +
				"Divider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>5</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style142\" /><Styl" +
				"e parent=\"Style1\" me=\"Style143\" /><FooterStyle parent=\"Style3\" me=\"Style144\" /><" +
				"EditorStyle parent=\"Style5\" me=\"Style145\" /><GroupHeaderStyle parent=\"Style1\" me" +
				"=\"Style147\" /><GroupFooterStyle parent=\"Style1\" me=\"Style146\" /><Visible>True</V" +
				"isible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>6" +
				"</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 17, 1068, 400</ClientRec" +
				"t><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyle" +
				"s><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style pa" +
				"rent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style paren" +
				"t=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent" +
				"=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent" +
				"=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Hea" +
				"ding\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style paren" +
				"t=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</" +
				"horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><" +
				"ClientArea>0, 0, 1068, 417</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style" +
				"14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// ReportsDataMaskGrid
			// 
			this.ReportsDataMaskGrid.AllowAddNew = true;
			this.ReportsDataMaskGrid.AllowColSelect = false;
			this.ReportsDataMaskGrid.AllowFilter = false;
			this.ReportsDataMaskGrid.AllowRowSelect = false;
			this.ReportsDataMaskGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.ReportsDataMaskGrid.Caption = "DataMasks";
			this.ReportsDataMaskGrid.CaptionHeight = 17;
			this.ReportsDataMaskGrid.EmptyRows = true;
			this.ReportsDataMaskGrid.ExtendRightColumn = true;
			this.ReportsDataMaskGrid.FetchRowStyles = true;
			this.ReportsDataMaskGrid.FilterBar = true;
			this.ReportsDataMaskGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ReportsDataMaskGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.ReportsDataMaskGrid.Location = new System.Drawing.Point(40, 96);
			this.ReportsDataMaskGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.ReportsDataMaskGrid.Name = "ReportsDataMaskGrid";
			this.ReportsDataMaskGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ReportsDataMaskGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ReportsDataMaskGrid.PreviewInfo.ZoomFactor = 75;
			this.ReportsDataMaskGrid.RecordSelectorWidth = 16;
			this.ReportsDataMaskGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.ReportsDataMaskGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ReportsDataMaskGrid.RowHeight = 15;
			this.ReportsDataMaskGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.ReportsDataMaskGrid.Size = new System.Drawing.Size(984, 208);
			this.ReportsDataMaskGrid.TabIndex = 12;
			this.ReportsDataMaskGrid.TabStop = false;
			this.ReportsDataMaskGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ReportsDataMaskGrid_BeforeUpdate);
			this.ReportsDataMaskGrid.OnAddNew += new System.EventHandler(this.ReportsDataMaskGrid_OnAddNew);
			this.ReportsDataMaskGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"BookGroup\" " +
				"DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"ReportName\" DataField=\"ReportName\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"BookGroup\" DataField=\"BookGroup\">" +
				"<ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Repor" +
				"t Name\" DataField=\"ReportName\"><ValueItems /><GroupInfo /></C1DataColumn><C1Data" +
				"Column Level=\"0\" Caption=\"Data Flag\" DataField=\"DataFlag\" NumberFormat=\"FormatTe" +
				"xt Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capt" +
				"ion=\"Trailer Flag\" DataField=\"TrailerFlag\"><ValueItems /><GroupInfo /></C1DataCo" +
				"lumn><C1DataColumn Level=\"0\" Caption=\"ReportType\" DataField=\"ReportType\"><ValueI" +
				"tems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Header Flag\"" +
				" DataField=\"HeaderFlag\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo" +
				" /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Report Name Position\" DataFie" +
				"ld=\"ReportNamePosition\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
				"Level=\"0\" Caption=\"Report Name Length\" DataField=\"ReportNameLength\"><ValueItems " +
				"/><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Report Date Posit" +
				"ion\" DataField=\"ReportDatePosition\"><ValueItems /><GroupInfo /></C1DataColumn><C" +
				"1DataColumn Level=\"0\" Caption=\"Report Date Length\" DataField=\"ReportDateLength\">" +
				"<ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"SecId" +
				" Position\" DataField=\"SecIdPosition\"><ValueItems /><GroupInfo /></C1DataColumn><" +
				"C1DataColumn Level=\"0\" Caption=\"SecId Length\" DataField=\"SecIdLength\"><ValueItem" +
				"s /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Quantity Positi" +
				"on\" DataField=\"QuantityPosition\"><ValueItems /><GroupInfo /></C1DataColumn><C1Da" +
				"taColumn Level=\"0\" Caption=\"Quantity Length\" DataField=\"QuantityLength\"><ValueIt" +
				"ems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Activity Posi" +
				"tion\" DataField=\"ActivityPosition\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"Activity Length\" DataField=\"ActivityLength\"><Value" +
				"Items /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Ignore Item" +
				"s\" DataField=\"IgnoreItems\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColu" +
				"mn Level=\"0\" Caption=\"Line Length\" DataField=\"LineLength\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Actor\" DataField=\"ActUserId" +
				"\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act" +
				" Time\" DataField=\"ActTime\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols>" +
				"<Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{Fore" +
				"Color:HighlightText;BackColor:Highlight;}Inactive{ForeColor:InactiveCaptionText;" +
				"BackColor:InactiveCaption;}Style119{AlignHorz:Near;}Style118{AlignHorz:Near;}Sty" +
				"le78{}Style79{}Style85{}Editor{}Style117{}Style116{}Style72{}Style73{}Style70{Al" +
				"ignHorz:Near;}Style71{AlignHorz:Near;}Style76{AlignHorz:Near;}Style77{AlignHorz:" +
				"Near;}Style74{}Style75{}Style84{}Style87{}Style86{}Style81{}Style80{}Style83{Ali" +
				"gnHorz:Near;}Style82{AlignHorz:Near;}Footer{}Style89{AlignHorz:Near;}Style88{Ali" +
				"gnHorz:Near;}Style108{}Style109{}Style132{}Style104{}Style105{}Style106{AlignHor" +
				"z:Near;}Style107{AlignHorz:Near;}Style100{AlignHorz:Near;}Style101{AlignHorz:Nea" +
				"r;}Style102{}Style103{}Style94{AlignHorz:Near;}Style95{AlignHorz:Near;}Style96{}" +
				"Style97{}Style90{}Style91{}Style92{}Style93{}Style131{AlignHorz:Near;}RecordSele" +
				"ctor{AlignImage:Center;}Style98{}Style99{}Heading{Wrap:True;AlignVert:Center;Bor" +
				"der:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style18{}Style19" +
				"{}Style14{}Style17{AlignHorz:Near;}Style139{}Style138{}Style137{AlignHorz:Near;}" +
				"Style136{AlignHorz:Near;}Style135{}Style134{}Style133{}Style15{}Style16{AlignHor" +
				"z:Near;}Style130{AlignHorz:Near;}Style10{AlignHorz:Near;}Style11{}Style12{}Style" +
				"13{BackColor:SeaShell;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Sty" +
				"le24{}Style25{}Style26{}Style27{}Style28{AlignHorz:Near;}Style29{AlignHorz:Near;" +
				"}Style128{}Style129{}Style126{}Style127{}Style124{AlignHorz:Near;}Style125{Align" +
				"Horz:Near;}Style122{}Style123{}Style120{}Style121{}Style23{AlignHorz:Near;}Style" +
				"22{AlignHorz:Near;}Style21{}Style20{}OddRow{}Style41{AlignHorz:Far;}Style40{Alig" +
				"nHorz:Near;}Style43{}Style42{}Style38{}Style39{}Style36{}FilterBar{}Style37{}Sty" +
				"le34{AlignHorz:Near;}Style35{AlignHorz:Near;}Style32{}Style33{}Style49{}Style48{" +
				"}Style30{}Style31{}Style140{}Style141{}Style142{AlignHorz:Near;}Style143{AlignHo" +
				"rz:Near;}Style144{}Style145{}Style146{}Style147{}Style45{}Style44{}Style47{Align" +
				"Horz:Near;}Style46{AlignHorz:Near;}Normal{Font:Verdana, 8.25pt;}EvenRow{BackColo" +
				"r:Aqua;}Style9{}Style8{}Style5{}Style4{}Style7{}Style6{}Style58{AlignHorz:Near;}" +
				"Style59{AlignHorz:Far;}Style3{}Style2{}Style50{}Style51{}Style52{AlignHorz:Near;" +
				"}Style53{AlignHorz:Near;}Style54{}Style55{}Style56{}Style57{}Caption{AlignHorz:C" +
				"enter;}Style64{AlignHorz:Near;}Style112{AlignHorz:Near;}Style69{}Style68{}Group{" +
				"BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style1{}Style63{" +
				"}Style62{}Style61{}Style60{}Style67{}Style66{}Style65{AlignHorz:Near;}Style115{}" +
				"Style114{}Style111{}Style110{}Style113{AlignHorz:Near;}</Data></Styles><Splits><" +
				"C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSelect" +
				"=\"False\" AllowRowSelect=\"False\" Name=\"\" AllowRowSizing=\"None\" CaptionHeight=\"17\"" +
				" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" Fetch" +
				"RowStyles=\"True\" FilterBar=\"True\" MarqueeStyle=\"SolidCellBorder\" RecordSelectorW" +
				"idth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\">" +
				"<CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"St" +
				"yle5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"Fil" +
				"terBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle pa" +
				"rent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLi" +
				"ghtRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\"" +
				" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle p" +
				"arent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style" +
				"6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><Headin" +
				"gStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><Foo" +
				"terStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19" +
				"\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"S" +
				"tyle1\" me=\"Style20\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</" +
				"Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e26\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>1" +
				"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
				"le52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"S" +
				"tyle54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"" +
				"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><ColumnD" +
				"ivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>6</DCIdx></C1Dis" +
				"playColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style " +
				"parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><Edit" +
				"orStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Sty" +
				"le33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><ColumnDivider>DarkGray" +
				",Single</ColumnDivider><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\"" +
				" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style38\" /><ColumnDivider>DarkGray,Single</Column" +
				"Divider><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /" +
				"><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"St" +
				"yle61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle pare" +
				"nt=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single" +
				"</ColumnDivider><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayC" +
				"olumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"St" +
				"yle41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5" +
				"\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterSt" +
				"yle parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>DarkGra" +
				"y,Single</ColumnDivider><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1" +
				"\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>5</DCIdx></C1DisplayCo" +
				"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent" +
				"=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visible><Colum" +
				"nDivider>DarkGray,Single</ColumnDivider><Width>146</Width><Height>15</Height><DC" +
				"Idx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
				"=\"Style70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" " +
				"me=\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle par" +
				"ent=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Vi" +
				"sible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>128</Wi" +
				"dth><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style77\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" me=\"Style7" +
				"9\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style80\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Co" +
				"lumnDivider><Width>144</Width><Height>15</Height><DCIdx>10</DCIdx></C1DisplayCol" +
				"umn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style82\" /><Style parent=" +
				"\"Style1\" me=\"Style83\" /><FooterStyle parent=\"Style3\" me=\"Style84\" /><EditorStyle" +
				" parent=\"Style5\" me=\"Style85\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style87\" /" +
				"><GroupFooterStyle parent=\"Style1\" me=\"Style86\" /><Visible>True</Visible><Column" +
				"Divider>DarkGray,Single</ColumnDivider><Width>150</Width><Height>15</Height><DCI" +
				"dx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
				"=\"Style88\" /><Style parent=\"Style1\" me=\"Style89\" /><FooterStyle parent=\"Style3\" " +
				"me=\"Style90\" /><EditorStyle parent=\"Style5\" me=\"Style91\" /><GroupHeaderStyle par" +
				"ent=\"Style1\" me=\"Style93\" /><GroupFooterStyle parent=\"Style1\" me=\"Style92\" /><Vi" +
				"sible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</He" +
				"ight><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
				"tyle2\" me=\"Style94\" /><Style parent=\"Style1\" me=\"Style95\" /><FooterStyle parent=" +
				"\"Style3\" me=\"Style96\" /><EditorStyle parent=\"Style5\" me=\"Style97\" /><GroupHeader" +
				"Style parent=\"Style1\" me=\"Style99\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
				"98\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Heig" +
				"ht>15</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
				"parent=\"Style2\" me=\"Style100\" /><Style parent=\"Style1\" me=\"Style101\" /><FooterSt" +
				"yle parent=\"Style3\" me=\"Style102\" /><EditorStyle parent=\"Style5\" me=\"Style103\" /" +
				"><GroupHeaderStyle parent=\"Style1\" me=\"Style105\" /><GroupFooterStyle parent=\"Sty" +
				"le1\" me=\"Style104\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colu" +
				"mnDivider><Width>141</Width><Height>15</Height><DCIdx>14</DCIdx></C1DisplayColum" +
				"n><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style106\" /><Style parent=\"" +
				"Style1\" me=\"Style107\" /><FooterStyle parent=\"Style3\" me=\"Style108\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style109\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style111" +
				"\" /><GroupFooterStyle parent=\"Style1\" me=\"Style110\" /><Visible>True</Visible><Co" +
				"lumnDivider>DarkGray,Single</ColumnDivider><Width>125</Width><Height>15</Height>" +
				"<DCIdx>15</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"\" me=\"Style112\" /><Style parent=\"Style1\" me=\"Style113\" /><FooterStyle parent=\"St" +
				"yle3\" me=\"Style114\" /><EditorStyle parent=\"Style5\" me=\"Style115\" /><GroupHeaderS" +
				"tyle parent=\"Style1\" me=\"Style117\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
				"116\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Wid" +
				"th>104</Width><Height>15</Height><DCIdx>16</DCIdx></C1DisplayColumn><C1DisplayCo" +
				"lumn><HeadingStyle parent=\"Style2\" me=\"Style118\" /><Style parent=\"Style1\" me=\"St" +
				"yle119\" /><FooterStyle parent=\"Style3\" me=\"Style120\" /><EditorStyle parent=\"Styl" +
				"e5\" me=\"Style121\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style123\" /><GroupFoot" +
				"erStyle parent=\"Style1\" me=\"Style122\" /><Visible>True</Visible><ColumnDivider>Da" +
				"rkGray,Single</ColumnDivider><Height>15</Height><DCIdx>17</DCIdx></C1DisplayColu" +
				"mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style124\" /><Style parent=" +
				"\"Style1\" me=\"Style125\" /><FooterStyle parent=\"Style3\" me=\"Style126\" /><EditorSty" +
				"le parent=\"Style5\" me=\"Style127\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style12" +
				"9\" /><GroupFooterStyle parent=\"Style1\" me=\"Style128\" /><Visible>True</Visible><C" +
				"olumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>18</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style130\" /" +
				"><Style parent=\"Style1\" me=\"Style131\" /><FooterStyle parent=\"Style3\" me=\"Style13" +
				"2\" /><EditorStyle parent=\"Style5\" me=\"Style133\" /><GroupHeaderStyle parent=\"Styl" +
				"e1\" me=\"Style135\" /><GroupFooterStyle parent=\"Style1\" me=\"Style134\" /><Visible>T" +
				"rue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><D" +
				"CIdx>19</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" " +
				"me=\"Style136\" /><Style parent=\"Style1\" me=\"Style137\" /><FooterStyle parent=\"Styl" +
				"e3\" me=\"Style138\" /><EditorStyle parent=\"Style5\" me=\"Style139\" /><GroupHeaderSty" +
				"le parent=\"Style1\" me=\"Style141\" /><GroupFooterStyle parent=\"Style1\" me=\"Style14" +
				"0\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Heigh" +
				"t>15</Height><DCIdx>20</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
				"arent=\"Style2\" me=\"Style142\" /><Style parent=\"Style1\" me=\"Style143\" /><FooterSty" +
				"le parent=\"Style3\" me=\"Style144\" /><EditorStyle parent=\"Style5\" me=\"Style145\" />" +
				"<GroupHeaderStyle parent=\"Style1\" me=\"Style147\" /><GroupFooterStyle parent=\"Styl" +
				"e1\" me=\"Style146\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Colum" +
				"nDivider><Height>15</Height><DCIdx>21</DCIdx></C1DisplayColumn></internalCols><C" +
				"lientRect>0, 17, 980, 187</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueD" +
				"BGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style pare" +
				"nt=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"" +
				"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"N" +
				"ormal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Norma" +
				"l\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Norm" +
				"al\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"N" +
				"ormal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vert" +
				"Splits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><Default" +
				"RecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 980, 204</ClientArea><Print" +
				"PageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Sty" +
				"le15\" /></Blob>";
			// 
			// PositionBankLoanReleaseReportForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1072, 421);
			this.Controls.Add(this.ReportsDataMaskGrid);
			this.Controls.Add(this.ReportsGrid);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PositionBankLoanReleaseReportForm";
			this.Text = "Position - BankLoan - Release Reports";
			this.Load += new System.EventHandler(this.PositionBankLoanReleaseReportForm_Load);
			this.Closed += new System.EventHandler(this.PositionBankLoanReleaseReportForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.ReportsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ReportsDataMaskGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		

		private void PositionBankLoanReleaseReportForm_Load(object sender, System.EventArgs e)
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

			mainForm.Alert("Please wait ... loading current bank loan release report data.", PilotState.Idle);			

			try
			{
				dataViewRowFilter = "BookGroup = ''";

				dataSet = new DataSet();
				dataSetDataMasks = new DataSet();
				
				dataSet = mainForm.PositionAgent.BankLoanReportsGet(mainForm.UtcOffset);
				dataSetDataMasks = mainForm.PositionAgent.BankLoanReportsDataMaskGet("", "", mainForm.UtcOffset);
								
				dataView = new DataView(dataSetDataMasks.Tables["BankLoanReportsDataMask"], dataViewRowFilter, "", DataViewRowState.CurrentRows);
				
				ReportsGrid.SetDataBinding(dataSet, "BankLoanReports", true);
				ReportsDataMaskGrid.SetDataBinding(dataView, "", true);
				
				mainForm.Alert("Please wait ... loading current bank loan release report data ... Done!.", PilotState.Idle);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionBankLoanReleaseReportForm.PositionBankLoanReleaseReportForm_Load]", Log.Error, 1); 				
				mainForm.Alert("Please wait ... loading current bank loan release report data ... Error!.", PilotState.RunFault);
			}			
			
			this.Cursor = Cursors.Default;
		}

		private void PositionBankLoanReleaseReportForm_Closed(object sender, System.EventArgs e)
		{
			if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
			}
		}

		private void ReleaseReportGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch(e.Column.DataField)
			{
				case "FileLoadDate":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
					}
					catch {}
					break;

				case	"ActTime":
				case "FileLoadTime":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeFormat);
					}
					catch {}
					break;
			}
		}


		private void ReportsGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{
				if ((bookGroup != ReportsGrid.Columns["BookGroup"].Text) || (reportName != ReportsGrid.Columns["ReportName"].Text))
				{
					bookGroup = ReportsGrid.Columns["BookGroup"].Text;
					reportName = ReportsGrid.Columns["ReportName"].Text;
					dataViewRowFilter = "BookGroup = '" + bookGroup + "' AND ReportName = '" + reportName + "'";
					dataView.RowFilter = dataViewRowFilter;
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void ReportsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				mainForm.PositionAgent.BankLoanReportSet(
					ReportsGrid.Columns["BookGroup"].Text,
					ReportsGrid.Columns["ReportName"].Text,
					ReportsGrid.Columns["ReportType"].Text,
					ReportsGrid.Columns["ReportHost"].Text,
					ReportsGrid.Columns["ReportHostUserId"].Text,
					ReportsGrid.Columns["ReportHostPassword"].Text,
					ReportsGrid.Columns["ReportPath"].Text,
					ReportsGrid.Columns["FileLoadDate"].Text,
					mainForm.UserId);
			
				ReportsGrid.Columns["ActUserId"].Text = mainForm.UserId;
				ReportsGrid.Columns["ActTime"].Text = DateTime.Now.ToString(Standard.DateFormat);
			}
			catch (Exception error)
			{				
				mainForm.Alert(error.Message, PilotState.RunFault);
				e.Cancel = true;
			}
		}

		private void ReportsDataMaskGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				mainForm.PositionAgent.BankLoanReportsDataMaskSet(
					bookGroup,
					reportName,
					ReportsDataMaskGrid.Columns["HeaderFlag"].Text,
					ReportsDataMaskGrid.Columns["DataFlag"].Text,
					ReportsDataMaskGrid.Columns["TrailerFlag"].Text,
					int.Parse(ReportsDataMaskGrid.Columns["ReportNamePosition"].Text),
					int.Parse(ReportsDataMaskGrid.Columns["ReportNameLength"].Text),
					int.Parse(ReportsDataMaskGrid.Columns["ReportDatePosition"].Text),
					int.Parse(ReportsDataMaskGrid.Columns["ReportDateLength"].Text),
					int.Parse(ReportsDataMaskGrid.Columns["SecIdPosition"].Text),
					int.Parse(ReportsDataMaskGrid.Columns["SecIdLength"].Text),
					int.Parse(ReportsDataMaskGrid.Columns["QuantityPosition"].Text),
					int.Parse(ReportsDataMaskGrid.Columns["QuantityLength"].Text),
					int.Parse(ReportsDataMaskGrid.Columns["ActivityPosition"].Text),
					int.Parse(ReportsDataMaskGrid.Columns["ActivityLength"].Text),
					ReportsDataMaskGrid.Columns["IgnoreItems"].Text,
					int.Parse(ReportsDataMaskGrid.Columns["LineLength"].Text),
					mainForm.UserId);

				ReportsDataMaskGrid.Columns["ActUserId"].Text = mainForm.UserId;
				ReportsDataMaskGrid.Columns["ActTime"].Text = DateTime.Now.ToString(Standard.DateFormat);
			}
			catch (Exception error)
			{				
				mainForm.Alert(error.Message, PilotState.RunFault);
				e.Cancel = true;
			}
		}

		private void ReportsDataMaskGrid_OnAddNew(object sender, System.EventArgs e)
		{
			ReportsDataMaskGrid.Columns["BookGroup"].Text = bookGroup;
			ReportsDataMaskGrid.Columns["ReportName"].Text = reportName;
		}
	}
}
