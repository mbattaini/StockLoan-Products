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
	public class PositionBankLoanPledgeForm : System.Windows.Forms.Form
	{	
		private const string TEXT = "Position - BankLoan Pledge";
	
		private string bankPledgesDataViewRowFilter = "";
		private string bookGroup = "";
		private string book = "";
		private string loanDate = "";
		private string loanType = "";
		private string price = "";
		private string activityType = "";
		private string amountColumnName = "";
		private string quantityColumnName = "";
		private string secId = "";
		
		private decimal pledgeHairCut = 0;

		private MainForm mainForm;
				
		private DataSet dataSetGrid, dataSetTable;

		private DataView bankPledgesDataView = null;								

		private ArrayList pledgesArray;

		private System.Windows.Forms.Panel TopPanel;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid PledgeGrid;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem PledgeMenuItem;		
		private System.Windows.Forms.MenuItem DockMenuItem;
		private System.Windows.Forms.MenuItem DockTopMenuItem;
		private System.Windows.Forms.MenuItem DockBottomMenuItem;
		private System.Windows.Forms.MenuItem DockNoneMenuItem;
		private System.Windows.Forms.MenuItem SeperatorMenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.ComponentModel.Container components = null;
		private C1.Win.C1Input.C1Label StatusLabel;
		private System.Windows.Forms.MenuItem DockSeperatorMenuItem;
		private C1.Win.C1Input.C1Label AmountLabel;
		private C1.Win.C1Input.C1Label PercentLabel;
		private C1.Win.C1Input.C1TextBox PledgeHairCutTextBox;
		private C1.Win.C1Input.C1Label PledgeHairCutLabel;
		private System.Windows.Forms.MenuItem HairCutMenuItem;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToMailRecipientMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem RefreshMenuItem;

		private delegate void SendPledgesDelegate(ArrayList pledgesArray);  

		public PositionBankLoanPledgeForm(MainForm mainForm, string bookGroup, string book, string loanDate, string loanType, string activityType, string price, string spRating, string moodyRating)
		{
			this.mainForm		= mainForm;
			this.bookGroup	= bookGroup;
			this.book				= book;
			this.loanDate		= loanDate;
			this.loanType		= loanType;
			this.activityType	= activityType;
			this.price			= price;
			pledgesArray		= new ArrayList();

			InitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionBankLoanPledgeForm));
			this.TopPanel = new System.Windows.Forms.Panel();
			this.PledgeGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.HairCutMenuItem = new System.Windows.Forms.MenuItem();
			this.PledgeMenuItem = new System.Windows.Forms.MenuItem();
			this.RefreshMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToMailRecipientMenuItem = new System.Windows.Forms.MenuItem();
			this.DockMenuItem = new System.Windows.Forms.MenuItem();
			this.DockTopMenuItem = new System.Windows.Forms.MenuItem();
			this.DockBottomMenuItem = new System.Windows.Forms.MenuItem();
			this.DockSeperatorMenuItem = new System.Windows.Forms.MenuItem();
			this.DockNoneMenuItem = new System.Windows.Forms.MenuItem();
			this.SeperatorMenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.AmountLabel = new C1.Win.C1Input.C1Label();
			this.PledgeHairCutLabel = new C1.Win.C1Input.C1Label();
			this.PledgeHairCutTextBox = new C1.Win.C1Input.C1TextBox();
			this.PercentLabel = new C1.Win.C1Input.C1Label();
			this.TopPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PledgeGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.AmountLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PledgeHairCutLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PledgeHairCutTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PercentLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// TopPanel
			// 
			this.TopPanel.Controls.Add(this.PledgeGrid);
			this.TopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TopPanel.Location = new System.Drawing.Point(0, 30);
			this.TopPanel.Name = "TopPanel";
			this.TopPanel.Size = new System.Drawing.Size(944, 291);
			this.TopPanel.TabIndex = 1;
			// 
			// PledgeGrid
			// 
			this.PledgeGrid.AllowColSelect = false;
			this.PledgeGrid.AllowFilter = false;
			this.PledgeGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.PledgeGrid.AllowUpdateOnBlur = false;
			this.PledgeGrid.Caption = "Available To Pledge";
			this.PledgeGrid.CaptionHeight = 17;
			this.PledgeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PledgeGrid.EmptyRows = true;
			this.PledgeGrid.ExtendRightColumn = true;
			this.PledgeGrid.FetchRowStyles = true;
			this.PledgeGrid.FilterBar = true;
			this.PledgeGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.PledgeGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.PledgeGrid.Location = new System.Drawing.Point(0, 0);
			this.PledgeGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.PledgeGrid.Name = "PledgeGrid";
			this.PledgeGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.PledgeGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.PledgeGrid.PreviewInfo.ZoomFactor = 75;
			this.PledgeGrid.RecordSelectorWidth = 16;
			this.PledgeGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.PledgeGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.PledgeGrid.RowHeight = 15;
			this.PledgeGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
			this.PledgeGrid.Size = new System.Drawing.Size(944, 291);
			this.PledgeGrid.TabIndex = 1;
			this.PledgeGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.PledgeGrid_Paint);
			this.PledgeGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.PledgeGrid_RowColChange);
			this.PledgeGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.PledgeGrid_SelChange);
			this.PledgeGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.PledgeGrid_BeforeColUpdate);
			this.PledgeGrid.FilterChange += new System.EventHandler(this.PledgeGrid_FilterChange);
			this.PledgeGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.PledgeGrid_FormatText);
			this.PledgeGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PledgeGrid_KeyPress);
			this.PledgeGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Symbol\" Dat" +
				"aField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
				"\" Caption=\"Class Group\" DataField=\"ClassGroup\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"Price\" DataField=\"Price\" NumberFormat=" +
				"\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Customer Quantity\" DataField=\"CustomerQuantityAvailable\" NumberFor" +
				"mat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"Firm Quantity\" DataField=\"FirmQuantityAvailable\" NumberFormat=" +
				"\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Customer Amount\" DataField=\"CustomerAmountAvailable\" NumberFormat=" +
				"\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Firm Amount\" DataField=\"FirmAmountAvailable\" NumberFormat=\"FormatT" +
				"ext Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
				"tion=\"BookGroup\" DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C1DataColumn" +
				"><C1DataColumn Level=\"0\" Caption=\"Sp\" DataField=\"Sp\"><ValueItems /><GroupInfo />" +
				"</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Moody\" DataField=\"Moody\"><ValueI" +
				"tems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Security ID\"" +
				" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Firm Quantity Pledged\" DataField=\"FirmQuantityPledged\" NumberForma" +
				"t=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Lev" +
				"el=\"0\" Caption=\"Customer Quantity Pledged\" DataField=\"CustomerQuantityPledged\" N" +
				"umberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1Data" +
				"Column Level=\"0\" Caption=\"T\" DataField=\"BaseType\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn><C1DataColumn Level=\"0\" Caption=\"Special Quantity\" DataField=\"Specia" +
				"lQuantityAvailable\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Special Amount\" DataField=\"Specia" +
				"lAmountAvailable\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn><C1DataColumn Level=\"0\" Caption=\"Special Quantity Pledged\" DataField" +
				"=\"SpecialQuantityPledged\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextW" +
				"rapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style199" +
				"{Font:Verdana, 8.25pt;AlignHorz:Near;ForeColor:Black;BackColor:WhiteSmoke;}Style" +
				"198{Font:Verdana, 8.25pt;AlignHorz:Near;}Inactive{ForeColor:InactiveCaptionText;" +
				"BackColor:InactiveCaption;}Style203{}Style202{}Style201{}Style200{}Style78{Align" +
				"Horz:Far;}Style79{}Style85{}Style84{AlignHorz:Far;}Style87{}Style86{}Style72{Ali" +
				"gnHorz:Far;}Style73{}Style70{}Style71{AlignHorz:Near;}Style76{}Style77{AlignHorz" +
				":Near;}Style74{}Style75{}Style181{}Style182{}Style183{}Style81{}Style80{}Style83" +
				"{AlignHorz:Near;}Style82{}Style188{}Style189{AlignHorz:Near;}Footer{}Style88{}Od" +
				"dRow{}Editor{}FilterBar{BackColor:SeaShell;}RecordSelector{AlignImage:Center;}He" +
				"ading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText" +
				";BackColor:Control;}Style18{}Style19{AlignHorz:Near;}Style14{AlignHorz:Near;}Sty" +
				"le15{}Style16{}Style17{}Style10{}Style11{}Style12{}Style13{AlignHorz:Near;}Selec" +
				"ted{ForeColor:HighlightText;BackColor:Highlight;}Style29{}Style28{}Style27{}Styl" +
				"e26{AlignHorz:Far;}Style9{}Style8{AlignHorz:Far;}Style22{}Style21{}Style5{}Style" +
				"4{}Style25{AlignHorz:Near;}Style24{}Style1{AlignHorz:Near;}Style23{}Style3{}Styl" +
				"e2{AlignHorz:Far;}Style245{}Style20{AlignHorz:Near;}Style243{}Style242{}Style241" +
				"{Font:Verdana, 8.25pt;AlignHorz:Far;ForeColor:Black;BackColor:WhiteSmoke;}Style2" +
				"40{Font:Verdana, 8.25pt;AlignHorz:Near;}Style244{}Style38{}Style39{}Style36{Alig" +
				"nHorz:Far;}Style37{}Style34{}Style35{}Style32{}Style33{}Style30{}Style49{}Style4" +
				"8{AlignHorz:Near;}Style31{AlignHorz:Near;}Style354{}Normal{Font:Verdana, 8.25pt;" +
				"}Style41{AlignHorz:Center;}Style40{}Style43{}Style42{AlignHorz:Center;BackColor:" +
				"White;}Style45{}Style44{}Style47{AlignHorz:Near;}Style46{}EvenRow{BackColor:Aqua" +
				";}Style214{}Style215{}Style212{}Style213{}Style210{Font:Verdana, 8.25pt;AlignHor" +
				"z:Near;}Style211{Font:Verdana, 8.25pt;AlignHorz:Near;ForeColor:Black;BackColor:W" +
				"hite;}Style7{AlignHorz:Near;}Style6{}Style58{}Style59{AlignHorz:Near;}Style50{}S" +
				"tyle51{}Style52{}Style53{AlignHorz:Near;}Style54{AlignHorz:Near;BackColor:WhiteS" +
				"moke;}Style55{}Style56{}Style57{}Style191{}Style190{}Caption{AlignHorz:Center;}S" +
				"tyle69{}Style68{}Style61{}Style63{}Style62{}Style64{}Style60{AlignHorz:Far;}Styl" +
				"e67{}Style66{AlignHorz:Far;}Style65{AlignHorz:Near;}Style184{}Style185{}Style186" +
				"{}Style187{}Style180{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignV" +
				"ert:Center;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"No" +
				"ne\" VBarStyle=\"Always\" AllowColSelect=\"False\" Name=\"Split[0,0]\" AllowRowSizing=\"" +
				"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Extend" +
				"RightColumn=\"True\" FetchRowStyles=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedRo" +
				"wBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" Ho" +
				"rizontalScrollGroup=\"2\"><CaptionStyle parent=\"Heading\" me=\"Style189\" /><EditorSt" +
				"yle parent=\"Editor\" me=\"Style181\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style187\"" +
				" /><FilterBarStyle parent=\"FilterBar\" me=\"Style354\" /><FooterStyle parent=\"Foote" +
				"r\" me=\"Style183\" /><GroupStyle parent=\"Group\" me=\"Style191\" /><HeadingStyle pare" +
				"nt=\"Heading\" me=\"Style182\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style1" +
				"86\" /><InactiveStyle parent=\"Inactive\" me=\"Style185\" /><OddRowStyle parent=\"OddR" +
				"ow\" me=\"Style188\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style190\" /" +
				"><SelectedStyle parent=\"Selected\" me=\"Style184\" /><Style parent=\"Normal\" me=\"Sty" +
				"le180\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style182\" me=\"Styl" +
				"e13\" /><Style parent=\"Style180\" me=\"Style14\" /><FooterStyle parent=\"Style183\" me" +
				"=\"Style15\" /><EditorStyle parent=\"Style181\" me=\"Style16\" /><GroupHeaderStyle par" +
				"ent=\"Style180\" me=\"Style18\" /><GroupFooterStyle parent=\"Style180\" me=\"Style17\" /" +
				"><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>7</DCId" +
				"x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style182\" me=\"Style53" +
				"\" /><Style parent=\"Style180\" me=\"Style54\" /><FooterStyle parent=\"Style183\" me=\"S" +
				"tyle55\" /><EditorStyle parent=\"Style181\" me=\"Style56\" /><GroupHeaderStyle parent" +
				"=\"Style180\" me=\"Style58\" /><GroupFooterStyle parent=\"Style180\" me=\"Style57\" /><V" +
				"isible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</H" +
				"eight><Locked>True</Locked><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style182\" me=\"Style198\" /><Style parent=\"Style180\" me=\"Styl" +
				"e199\" /><FooterStyle parent=\"Style183\" me=\"Style200\" /><EditorStyle parent=\"Styl" +
				"e181\" me=\"Style201\" /><GroupHeaderStyle parent=\"Style180\" me=\"Style203\" /><Group" +
				"FooterStyle parent=\"Style180\" me=\"Style202\" /><Visible>True</Visible><ColumnDivi" +
				"der>DarkGray,Single</ColumnDivider><Width>75</Width><Height>15</Height><Locked>T" +
				"rue</Locked><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style182\" me=\"Style41\" /><Style parent=\"Style180\" me=\"Style42\" /><FooterSty" +
				"le parent=\"Style183\" me=\"Style43\" /><EditorStyle parent=\"Style181\" me=\"Style44\" " +
				"/><GroupHeaderStyle parent=\"Style180\" me=\"Style46\" /><GroupFooterStyle parent=\"S" +
				"tyle180\" me=\"Style45\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</C" +
				"olumnDivider><Width>25</Width><Height>15</Height><DCIdx>13</DCIdx></C1DisplayCol" +
				"umn><C1DisplayColumn><HeadingStyle parent=\"Style182\" me=\"Style210\" /><Style pare" +
				"nt=\"Style180\" me=\"Style211\" /><FooterStyle parent=\"Style183\" me=\"Style212\" /><Ed" +
				"itorStyle parent=\"Style181\" me=\"Style213\" /><GroupHeaderStyle parent=\"Style180\" " +
				"me=\"Style215\" /><GroupFooterStyle parent=\"Style180\" me=\"Style214\" /><Visible>Tru" +
				"e</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><Loc" +
				"ked>True</Locked><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyl" +
				"e parent=\"Style182\" me=\"Style19\" /><Style parent=\"Style180\" me=\"Style20\" /><Foot" +
				"erStyle parent=\"Style183\" me=\"Style21\" /><EditorStyle parent=\"Style181\" me=\"Styl" +
				"e22\" /><GroupHeaderStyle parent=\"Style180\" me=\"Style24\" /><GroupFooterStyle pare" +
				"nt=\"Style180\" me=\"Style23\" /><Visible>True</Visible><ColumnDivider>DarkGray,Sing" +
				"le</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx>8</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style182\" me=\"Style47\" /><Sty" +
				"le parent=\"Style180\" me=\"Style48\" /><FooterStyle parent=\"Style183\" me=\"Style49\" " +
				"/><EditorStyle parent=\"Style181\" me=\"Style50\" /><GroupHeaderStyle parent=\"Style1" +
				"80\" me=\"Style52\" /><GroupFooterStyle parent=\"Style180\" me=\"Style51\" /><Visible>T" +
				"rue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><L" +
				"ocked>True</Locked><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSt" +
				"yle parent=\"Style182\" me=\"Style240\" /><Style parent=\"Style180\" me=\"Style241\" /><" +
				"FooterStyle parent=\"Style183\" me=\"Style242\" /><EditorStyle parent=\"Style181\" me=" +
				"\"Style243\" /><GroupHeaderStyle parent=\"Style180\" me=\"Style245\" /><GroupFooterSty" +
				"le parent=\"Style180\" me=\"Style244\" /><Visible>True</Visible><ColumnDivider>DarkG" +
				"ray,Single</ColumnDivider><Width>65</Width><Height>15</Height><Locked>True</Lock" +
				"ed><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Styl" +
				"e182\" me=\"Style65\" /><Style parent=\"Style180\" me=\"Style66\" /><FooterStyle parent" +
				"=\"Style183\" me=\"Style67\" /><EditorStyle parent=\"Style181\" me=\"Style68\" /><GroupH" +
				"eaderStyle parent=\"Style180\" me=\"Style70\" /><GroupFooterStyle parent=\"Style180\" " +
				"me=\"Style69\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>166</Width><" +
				"Height>15</Height><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSt" +
				"yle parent=\"Style182\" me=\"Style1\" /><Style parent=\"Style180\" me=\"Style2\" /><Foot" +
				"erStyle parent=\"Style183\" me=\"Style3\" /><EditorStyle parent=\"Style181\" me=\"Style" +
				"4\" /><GroupHeaderStyle parent=\"Style180\" me=\"Style6\" /><GroupFooterStyle parent=" +
				"\"Style180\" me=\"Style5\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>12" +
				"0</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style182\" me=\"Style25\" /><Style parent=\"Style180\" me=\"Style" +
				"26\" /><FooterStyle parent=\"Style183\" me=\"Style27\" /><EditorStyle parent=\"Style18" +
				"1\" me=\"Style28\" /><GroupHeaderStyle parent=\"Style180\" me=\"Style30\" /><GroupFoote" +
				"rStyle parent=\"Style180\" me=\"Style29\" /><ColumnDivider>DarkGray,Single</ColumnDi" +
				"vider><Width>126</Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style182\" me=\"Style59\" /><Style parent=\"Styl" +
				"e180\" me=\"Style60\" /><FooterStyle parent=\"Style183\" me=\"Style61\" /><EditorStyle " +
				"parent=\"Style181\" me=\"Style62\" /><GroupHeaderStyle parent=\"Style180\" me=\"Style64" +
				"\" /><GroupFooterStyle parent=\"Style180\" me=\"Style63\" /><ColumnDivider>DarkGray,S" +
				"ingle</ColumnDivider><Width>148</Width><Height>15</Height><DCIdx>11</DCIdx></C1D" +
				"isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style182\" me=\"Style7\" /><Sty" +
				"le parent=\"Style180\" me=\"Style8\" /><FooterStyle parent=\"Style183\" me=\"Style9\" />" +
				"<EditorStyle parent=\"Style181\" me=\"Style10\" /><GroupHeaderStyle parent=\"Style180" +
				"\" me=\"Style12\" /><GroupFooterStyle parent=\"Style180\" me=\"Style11\" /><ColumnDivid" +
				"er>DarkGray,Single</ColumnDivider><Width>120</Width><Height>15</Height><DCIdx>4<" +
				"/DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style182\" me=\"St" +
				"yle31\" /><Style parent=\"Style180\" me=\"Style36\" /><FooterStyle parent=\"Style183\" " +
				"me=\"Style37\" /><EditorStyle parent=\"Style181\" me=\"Style38\" /><GroupHeaderStyle p" +
				"arent=\"Style180\" me=\"Style40\" /><GroupFooterStyle parent=\"Style180\" me=\"Style39\"" +
				" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>6</DC" +
				"Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style182\" me=\"Style" +
				"71\" /><Style parent=\"Style180\" me=\"Style72\" /><FooterStyle parent=\"Style183\" me=" +
				"\"Style73\" /><EditorStyle parent=\"Style181\" me=\"Style74\" /><GroupHeaderStyle pare" +
				"nt=\"Style180\" me=\"Style76\" /><GroupFooterStyle parent=\"Style180\" me=\"Style75\" />" +
				"<ColumnDivider>DarkGray,Single</ColumnDivider><Width>140</Width><Height>15</Heig" +
				"ht><DCIdx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
				"le182\" me=\"Style77\" /><Style parent=\"Style180\" me=\"Style78\" /><FooterStyle paren" +
				"t=\"Style183\" me=\"Style79\" /><EditorStyle parent=\"Style181\" me=\"Style80\" /><Group" +
				"HeaderStyle parent=\"Style180\" me=\"Style82\" /><GroupFooterStyle parent=\"Style180\"" +
				" me=\"Style81\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>140</Width>" +
				"<Height>15</Height><DCIdx>15</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
				"tyle parent=\"Style182\" me=\"Style83\" /><Style parent=\"Style180\" me=\"Style84\" /><F" +
				"ooterStyle parent=\"Style183\" me=\"Style85\" /><EditorStyle parent=\"Style181\" me=\"S" +
				"tyle86\" /><GroupHeaderStyle parent=\"Style180\" me=\"Style88\" /><GroupFooterStyle p" +
				"arent=\"Style180\" me=\"Style87\" /><ColumnDivider>DarkGray,Single</ColumnDivider><W" +
				"idth>160</Width><Height>15</Height><DCIdx>16</DCIdx></C1DisplayColumn></internal" +
				"Cols><ClientRect>0, 17, 940, 270</ClientRect><BorderSide>Right</BorderSide></C1." +
				"Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" />" +
				"<Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Sty" +
				"le parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Styl" +
				"e parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style pa" +
				"rent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style p" +
				"arent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Styl" +
				"e parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedS" +
				"tyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layo" +
				"ut><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 940, 287</Client" +
				"Area><PrintPageHeaderStyle parent=\"\" me=\"Style34\" /><PrintPageFooterStyle parent" +
				"=\"\" me=\"Style35\" /></Blob>";
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.HairCutMenuItem,
																																										this.PledgeMenuItem,
																																										this.RefreshMenuItem,
																																										this.SendToMenuItem,
																																										this.DockMenuItem,
																																										this.SeperatorMenuItem,
																																										this.ExitMenuItem});
			// 
			// HairCutMenuItem
			// 
			this.HairCutMenuItem.Index = 0;
			this.HairCutMenuItem.Text = "HairCut";
			this.HairCutMenuItem.Click += new System.EventHandler(this.HairCutMenuItem_Click);
			// 
			// PledgeMenuItem
			// 
			this.PledgeMenuItem.Index = 1;
			this.PledgeMenuItem.Text = "Pledge";
			this.PledgeMenuItem.Click += new System.EventHandler(this.PledgeMenuItem_Click);
			// 
			// RefreshMenuItem
			// 
			this.RefreshMenuItem.Index = 2;
			this.RefreshMenuItem.Text = "Refresh";
			this.RefreshMenuItem.Click += new System.EventHandler(this.RefreshMenuItem_Click);
			// 
			// SendToMenuItem
			// 
			this.SendToMenuItem.Index = 3;
			this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.SendToClipboardMenuItem,
																																									 this.SendToExcelMenuItem,
																																									 this.SendToMailRecipientMenuItem});
			this.SendToMenuItem.Text = "Send To";
			// 
			// SendToClipboardMenuItem
			// 
			this.SendToClipboardMenuItem.Index = 0;
			this.SendToClipboardMenuItem.Text = "ClipBoard";
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
			// DockMenuItem
			// 
			this.DockMenuItem.Index = 4;
			this.DockMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.DockTopMenuItem,
																																								 this.DockBottomMenuItem,
																																								 this.DockSeperatorMenuItem,
																																								 this.DockNoneMenuItem});
			this.DockMenuItem.Text = "Dock";
			// 
			// DockTopMenuItem
			// 
			this.DockTopMenuItem.Index = 0;
			this.DockTopMenuItem.Text = "Top";
			this.DockTopMenuItem.Click += new System.EventHandler(this.DockTopMenuItem_Click);
			// 
			// DockBottomMenuItem
			// 
			this.DockBottomMenuItem.Index = 1;
			this.DockBottomMenuItem.Text = "Bottom";
			this.DockBottomMenuItem.Click += new System.EventHandler(this.DockBottomMenuItem_Click);
			// 
			// DockSeperatorMenuItem
			// 
			this.DockSeperatorMenuItem.Index = 2;
			this.DockSeperatorMenuItem.Text = "-";
			// 
			// DockNoneMenuItem
			// 
			this.DockNoneMenuItem.Index = 3;
			this.DockNoneMenuItem.Text = "None";
			this.DockNoneMenuItem.Click += new System.EventHandler(this.DockNoneMenuItem_Click);
			// 
			// SeperatorMenuItem
			// 
			this.SeperatorMenuItem.Index = 5;
			this.SeperatorMenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 6;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StatusLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.StatusLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.StatusLabel.Location = new System.Drawing.Point(8, 328);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(448, 16);
			this.StatusLabel.TabIndex = 32;
			this.StatusLabel.Tag = null;
			this.StatusLabel.TextDetached = true;
			// 
			// AmountLabel
			// 
			this.AmountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.AmountLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AmountLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.AmountLabel.Location = new System.Drawing.Point(464, 328);
			this.AmountLabel.Name = "AmountLabel";
			this.AmountLabel.Size = new System.Drawing.Size(472, 16);
			this.AmountLabel.TabIndex = 33;
			this.AmountLabel.Tag = null;
			this.AmountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.AmountLabel.TextDetached = true;
			// 
			// PledgeHairCutLabel
			// 
			this.PledgeHairCutLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.PledgeHairCutLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.PledgeHairCutLabel.Location = new System.Drawing.Point(8, 8);
			this.PledgeHairCutLabel.Name = "PledgeHairCutLabel";
			this.PledgeHairCutLabel.Size = new System.Drawing.Size(112, 16);
			this.PledgeHairCutLabel.TabIndex = 34;
			this.PledgeHairCutLabel.Tag = null;
			this.PledgeHairCutLabel.Text = "Pledge HairCut:";
			this.PledgeHairCutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.PledgeHairCutLabel.TextDetached = true;
			// 
			// PledgeHairCutTextBox
			// 
			this.PledgeHairCutTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PledgeHairCutTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
			this.PledgeHairCutTextBox.Location = new System.Drawing.Point(120, 8);
			this.PledgeHairCutTextBox.Name = "PledgeHairCutTextBox";
			this.PledgeHairCutTextBox.Size = new System.Drawing.Size(72, 19);
			this.PledgeHairCutTextBox.TabIndex = 35;
			this.PledgeHairCutTextBox.Tag = null;
			this.PledgeHairCutTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PledgeHairCutTextBox.Leave += new System.EventHandler(this.PledgeHairCutTextBox_Leave);
			// 
			// PercentLabel
			// 
			this.PercentLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.PercentLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.PercentLabel.Location = new System.Drawing.Point(192, 8);
			this.PercentLabel.Name = "PercentLabel";
			this.PercentLabel.Size = new System.Drawing.Size(32, 16);
			this.PercentLabel.TabIndex = 36;
			this.PercentLabel.Tag = null;
			this.PercentLabel.Text = "%";
			this.PercentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.PercentLabel.TextDetached = true;
			// 
			// PositionBankLoanPledgeForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(944, 341);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.PercentLabel);
			this.Controls.Add(this.PledgeHairCutTextBox);
			this.Controls.Add(this.PledgeHairCutLabel);
			this.Controls.Add(this.AmountLabel);
			this.Controls.Add(this.TopPanel);
			this.Controls.Add(this.StatusLabel);
			this.DockPadding.Bottom = 20;
			this.DockPadding.Top = 30;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PositionBankLoanPledgeForm";
			this.Text = "Position - BankLoan Pledge";
			this.Load += new System.EventHandler(this.PositionBankLoanPledgeForm_Load);
			this.TopPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PledgeGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.AmountLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PledgeHairCutLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PledgeHairCutTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PercentLabel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void PositionBankLoanPledgeForm_Load(object sender, System.EventArgs e)
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

			mainForm.Alert("Please wait ... loading current bank loan pledge data.", PilotState.Idle);

			try
			{										
				dataSetGrid = mainForm.PositionAgent.BankLoanPledgeSummaryGet(mainForm.ServiceAgent.ContractsBizDate());
				dataSetTable = dataSetGrid;

				bankPledgesDataViewRowFilter = "BookGroup = '" + bookGroup + "' AND Price >= " + price;
				bankPledgesDataView = new DataView(dataSetGrid.Tables["BankLoanPledgeSummary"], bankPledgesDataViewRowFilter, "", DataViewRowState.CurrentRows);
				
				PledgeGrid.SetDataBinding(bankPledgesDataView, "", true);
				
				pledgeHairCut = decimal.Parse(mainForm.ServiceAgent.KeyValueGet("BankLoanPledgeHairCut", "75"));

				PledgeHairCutTextBox.Value = pledgeHairCut.ToString();
				
				if (loanType.Equals("C"))
				{
					quantityColumnName	= "CustomerQuantityAvailable";
					amountColumnName	= "CustomerAmountAvailable";
					
					bankPledgesDataViewRowFilter += " AND " + quantityColumnName + " > 0";
					bankPledgesDataView.RowFilter = bankPledgesDataViewRowFilter;

					PledgeGrid.Splits[0,0].DisplayColumns["CustomerQuantityAvailable"].Visible = true;
					PledgeGrid.Splits[0,0].DisplayColumns["CustomerAmountAvailable"].Visible = true;
					PledgeGrid.Splits[0,0].DisplayColumns["CustomerQuantityPledged"].Visible = true;								
				}
				else if (loanType.Equals("F"))
				{			
					quantityColumnName	= "FirmQuantityAvailable";
					amountColumnName	= "FirmAmountAvailable";
			
					bankPledgesDataViewRowFilter += " AND " + quantityColumnName + " > 0";
					bankPledgesDataView.RowFilter = bankPledgesDataViewRowFilter;

					PledgeGrid.Splits[0,0].DisplayColumns["FirmQuantityAvailable"].Visible = true;
					PledgeGrid.Splits[0,0].DisplayColumns["FirmAmountAvailable"].Visible = true;
					PledgeGrid.Splits[0,0].DisplayColumns["FirmQuantityPledged"].Visible = true;
				}							
				else if (loanType.Equals("S"))
				{			
					quantityColumnName	= "SpecialQuantityAvailable";
					amountColumnName	= "SpecialAmountAvailable";
			
					bankPledgesDataViewRowFilter += " AND " + quantityColumnName + " > 0";
					bankPledgesDataView.RowFilter = bankPledgesDataViewRowFilter;

					PledgeGrid.Splits[0,0].DisplayColumns["SpecialQuantityAvailable"].Visible = true;
					PledgeGrid.Splits[0,0].DisplayColumns["SpecialAmountAvailable"].Visible = true;
					PledgeGrid.Splits[0,0].DisplayColumns["SpecialQuantityPledged"].Visible = true;
				}							

				FormStatusSet();
				FormAmountSet();
			
				mainForm.Alert("Please wait ... loading current bank loan pledge data ... Done!.", PilotState.Idle);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanPledgeForm.PositionBankLoanPledgeForm_Load]", Log.Error, 1); 				
				mainForm.Alert("Please wait ... loading current bank loan pledge data ... Error!.", PilotState.RunFault);
			}			
			
			this.Cursor = Cursors.Default;
		}

		private void PositionBankLoanPledgeForm_Closed(object sender, System.EventArgs e)
		{
			if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
			}
 
			mainForm.positionBankLoanForm = null;
		}

		private void PledgeGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch(e.Column.DataField)
			{
				case "Price":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.00");
					}
					catch {}
					break;

				case "FirmAmountAvailable":
				case "CustomerAmountAvailable":
				case "SpecialAmountAvailable":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.00");
					}
					catch {}
					break;

				case "CustomerQuantityPledged":
				case "FirmQuantityPledged":
				case "CustomerQuantityAvailable":				
				case "FirmQuantityAvailable":
				case "SpecialQuantityPledged":
				case "SpecialQuantityAvailable":				
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch {}
					break;
			}
		}

		private void PledgeGrid_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			string quantity = "";
			string amount = "";
			decimal price = 0;
			
			try
			{	
				foreach (DataRow dataRow in dataSetTable.Tables["BankLoanPledgeSummary"].Rows)
				{
					if (dataRow["BookGroup"].ToString().Equals(PledgeGrid.Columns["BookGroup"].Text) &&
						dataRow["SecId"].ToString().Equals(PledgeGrid.Columns["SecId"].Text))
					{
						if (long.Parse(dataRow[quantityColumnName].ToString()) < long.Parse(PledgeGrid.Columns[quantityColumnName].Text))
						{
							mainForm.Alert("Quantity specified is more then quantity available", PilotState.Idle);
							e.Cancel = true;

							return;
						}
					}
				}
				
				quantity = PledgeGrid.Columns[quantityColumnName].Value.ToString();
				
				price = decimal.Parse(PledgeGrid.Columns["Price"].Value.ToString());

				if (mainForm.IsBond)
				{
					price = price / 100;
				}
				
				amount = (long.Parse(quantity) * price).ToString();
			
				PledgeGrid.Columns[amountColumnName].Value = amount;				
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanPledgeForm.PledgeGrid_BeforeColUpdate]", Log.Error, 1); 				
				mainForm.Alert(error.Message, PilotState.RunFault);
				e.Cancel = true;
				return;
			}
		}

		private void PledgeMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				pledgesArray = new ArrayList();

				DataRowView dataRowView;
				DataRow	dataRow;

				if (PledgeGrid.SelectedRows.Count == 0)	
				{										
					dataRowView = (DataRowView) PledgeGrid[PledgeGrid.Row];
					dataRow = dataRowView.Row.Table.NewRow(); 												
					dataRow.ItemArray = dataRowView.Row.ItemArray;
					pledgesArray.Add(dataRow);				
				}
				else
				{
					foreach (int rowIndex in PledgeGrid.SelectedRows)
					{												
						dataRowView = (DataRowView) PledgeGrid[rowIndex];
						dataRow = dataRowView.Row.Table.NewRow(); 												
						dataRow.ItemArray = dataRowView.Row.ItemArray;
						pledgesArray.Add(dataRow);							
					}
				}

				PledgeGrid.SelectedRows.Clear();
				PledgeGrid.SelectedCols.Clear();

				SendPledgesDelegate sendPledgesDelegate = new SendPledgesDelegate(SendPledges);
				sendPledgesDelegate.BeginInvoke(pledgesArray, null, null);             
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanPledgeForm.PledgeMenuItem_Click]", Log.Error, 1); 				
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		public void SendPledges(ArrayList pledgesArray)
		{
			string quantity = "";
			
			foreach (DataRow row in pledgesArray)
			{
				try
				{			
					quantity = row[quantityColumnName].ToString();
										
					mainForm.PositionAgent.BankLoanPledgeSet(
						row["BookGroup"].ToString(),
						this.book,
						this.loanDate,
						"",						
						this.loanType,
						this.activityType,
						row["SecId"].ToString(),
						quantity,
						"",
						"PR",
						mainForm.UserId);
				}			
				catch (Exception error)
				{
					Log.Write(error.Message + ". [PositionBankLoanPledgeForm.SendPledges]", Log.Error, 1); 				
					mainForm.Alert(error.Message, PilotState.RunFault);
				}
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

		private void PledgeGrid_FilterChange(object sender, System.EventArgs e)
		{
			string gridFilter;

			try
			{
				gridFilter = mainForm.GridFilterGet(ref PledgeGrid);

				if (gridFilter.Equals(""))
				{
					bankPledgesDataView.RowFilter = bankPledgesDataViewRowFilter;
				}
				else
				{
					bankPledgesDataView.RowFilter = bankPledgesDataViewRowFilter + " AND " + gridFilter;
				}

				FormStatusSet();
				FormAmountSet();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void FormStatusSet()
		{
			if (PledgeGrid.SelectedRows.Count > 0)
			{
				StatusLabel.Text = "Selecting " + PledgeGrid.SelectedRows.Count.ToString("#,##0") + " items of " + bankPledgesDataView.Count.ToString("#,##0") + " shown in grid.";
			}
			else
			{
				StatusLabel.Text = "Showing " + bankPledgesDataView.Count.ToString("#,##0") + " items in grid.";
			}
		}

		private void FormAmountSet()
		{
			if (!amountColumnName.Equals("") && !quantityColumnName.Equals(""))
			{
			
				if (PledgeGrid.SelectedRows.Count > 0)
				{
					decimal amount = 0;

					foreach (int row in PledgeGrid.SelectedRows)
					{
						try
						{
							amount += decimal.Parse(PledgeGrid.Columns[amountColumnName].CellValue(row).ToString());						
						}
						catch (Exception error)
						{
							mainForm.Alert(error.Message, PilotState.RunFault);
						}
					}

					AmountLabel.Text = "Selected: $" + amount.ToString("#,##0.00");
				}
				else
				{
					try
					{
						if (!PledgeGrid.Columns[amountColumnName].Value.ToString().Equals(""))
						{				
							AmountLabel.Text = "$" + decimal.Parse(PledgeGrid.Columns[amountColumnName].Value.ToString()).ToString("#,##0.00");
						}
						else
						{
							AmountLabel.Text = "$0";
						}
					}
					catch (Exception error)
					{
						mainForm.Alert(error.Message, PilotState.RunFault);
					}
				}
			}
		}

		private void PledgeGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			FormStatusSet();
			FormAmountSet();
		}

		private void PledgeGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
		{
			FormAmountSet();
		}

		private void PledgeHairCutTextBox_Leave(object sender, System.EventArgs e)
		{
			
			try
			{								
				pledgeHairCut = decimal.Parse(PledgeHairCutTextBox.Value.ToString());
				mainForm.ServiceAgent.KeyValueSet("BankLoanPledgeHairCut", pledgeHairCut.ToString());				
				
				mainForm.Alert("Pledge HairCut changed to: " + pledgeHairCut.ToString() + "%", PilotState.Normal);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanPledgeForm.PledgeHairCutTextBox_Leave]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void HairCutMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (PledgeGrid.SelectedRows.Count == 0)
				{
					PledgeGrid.Columns[quantityColumnName].Value = (long.Parse(PledgeGrid.Columns[quantityColumnName].Value.ToString()) * (pledgeHairCut/100)).ToString();
					PledgeGrid.Columns[amountColumnName].Value	 = (long.Parse(PledgeGrid.Columns[quantityColumnName].Value.ToString()) * decimal.Parse(PledgeGrid.Columns["Price"].Value.ToString())).ToString(); 				
				}
				else
				{				
					foreach(int i  in PledgeGrid.SelectedRows)
					{
						PledgeGrid[i, quantityColumnName]	= long.Parse(PledgeGrid.Columns[quantityColumnName].CellValue(i).ToString()) * (pledgeHairCut/100);
						PledgeGrid[i, amountColumnName]		= long.Parse(PledgeGrid.Columns[quantityColumnName].CellValue(i).ToString()) * decimal.Parse(PledgeGrid.Columns["Price"].CellValue(i).ToString()); 
					}
				}

				FormStatusSet();
				FormAmountSet();
			}
			catch (Exception error)
			{				
				Log.Write(error.Message + ". [PositionBankLoanPledgeForm.HairCutMenuItem_Click]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}			
		}

		private void RefreshMenuItem_Click(object sender, System.EventArgs e)
		{
			mainForm.GridFilterClear(ref PledgeGrid);
			
			try
			{
				DataRow [] rows = mainForm.PositionAgent.BankLoanPledgeSummaryGet(mainForm.ServiceAgent.ContractsBizDate()).Tables["BankLoanPledgeSummary"].Select();      
        
				this.Cursor = Cursors.WaitCursor;				
          
				dataSetGrid.Tables["BankLoanPledgeSummary"].Clear();                                                        
          
				dataSetGrid.Tables["BankLoanPledgeSummary"].BeginLoadData();                
        
				foreach (DataRow row in rows)
				{
					dataSetGrid.Tables["BankLoanPledgeSummary"].ImportRow(row);          
				}

				dataSetGrid.Tables["BankLoanPledgeSummary"].EndLoadData();
                
				dataSetTable = dataSetGrid;
          				
				this.Cursor = Cursors.Default;        
			}                 				
			catch (Exception error)
			{				
				Log.Write(error.Message + ". [PositionBankLoanPledgeForm.RefreshMenuItem_Click]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}			
		}

		private void PledgeGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{
				if (!PledgeGrid.Columns["SecId"].Text.Equals(secId))
				{
					secId = PledgeGrid.Columns["SecId"].Text;

					this.Cursor = Cursors.WaitCursor;                
     
					mainForm.SecId = secId;

					this.Cursor = Cursors.Default;
				}
			}
			catch (Exception error)
			{				
				Log.Write(error.Message + ". [PositionBankLoanPledgeForm.PledgeGrid_Paint]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}			
		}

		private void PledgeGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			string gridData = "";

			if (e.KeyChar.Equals((char)3) && PledgeGrid.SelectedRows.Count > 0)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PledgeGrid.SelectedCols)
				{
					gridData += dataColumn.Caption + "\t";
				}

				gridData += "\n";

				foreach (int row in PledgeGrid.SelectedRows)
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PledgeGrid.SelectedCols)
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
				mainForm.Alert("Copied " + PledgeGrid.SelectedRows.Count.ToString("#,##0") + " rows to the clipboard.");
				e.Handled = true;
			}
		}

		private void SendToMailRecipientMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (PledgeGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
				return;
			}

			try
			{
				maxTextLength = new int[PledgeGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PledgeGrid.SelectedCols)
				{					
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in PledgeGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PledgeGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PledgeGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PledgeGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in PledgeGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PledgeGrid.SelectedCols)
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

				mainForm.Alert("Total: " + PledgeGrid.SelectedRows.Count + " items added to e-mail.");
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionBankLoanPledgeForm.MailRecipientMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (PledgeGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
				return;
			}

			try
			{
				maxTextLength = new int[PledgeGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PledgeGrid.SelectedCols)
				{					
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in PledgeGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PledgeGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PledgeGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PledgeGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in PledgeGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PledgeGrid.SelectedCols)
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
                				
				Clipboard.SetDataObject(gridData, true);

				mainForm.Alert("Total: " + PledgeGrid.SelectedRows.Count + " items added to clipboard.");
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionBankLoanPledgeForm.SendToClipboardMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			Excel excel = new Excel();
			excel.ExportGridToExcel(ref PledgeGrid);
		}
	}
}
