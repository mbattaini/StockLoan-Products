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
	public class PositionBankLoanForm : System.Windows.Forms.Form
	{
		private const string TEXT = "Position - BankLoan";
		
		private MainForm mainForm;
		
		private ArrayList bankLoanActivityEventArgsArray;
		private BankLoanActivityEventHandler bankLoanActivityEventHandler = null;
		private BankLoanActivityWrapper bankLoanActivityWrapper = null;

		private DataView bankLoanActivityDataView = null;
		private DataView bankLoansDataView = null;
		private DataView bankDataView = null;

		private string bankLoanActiivtyDataViewRowFilter = "";
		private string bankLoansDataViewRowFilter = "";
		private string bankDataViewRowFilter = "";		
		private string tempPath = "";
	
		private DataSet dataSet;

		private string bank = "";
		private string loanDate = "";
		private string secId = "";

		private bool isReady = false;
		
		private System.Windows.Forms.Panel TopPanel;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid LoansGrid;		
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid BanksGrid;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ActivityGrid;
		private C1.Win.C1Input.C1Label BookGroupNameLabel;
		private C1.Win.C1Input.C1Label BookGroupLabel;
		private C1.Win.C1List.C1Combo BookGroupCombo;	
		private System.Windows.Forms.Splitter vSplitter;
		private System.Windows.Forms.Splitter hSplitter;		
		private System.ComponentModel.Container components = null;
		private PositionBankLoanPledgeForm positionBankLoanPledgeForm = null;
		private PositionBankLoanReleaseForm positionBankLoanReleaseForm = null;
		private C1.Win.C1TrueDBGrid.C1TrueDBDropdown LoanTypeDropdown;
		private C1.Win.C1TrueDBGrid.C1TrueDBDropdown ActivityTypeDropDown;
		private System.Windows.Forms.MenuItem PledgeMenuItem;
		private System.Windows.Forms.MenuItem ReleaseMenuItem;
		private System.Windows.Forms.MenuItem SeperatorMenuItem;
		private System.Windows.Forms.MenuItem DockMenuItem;
		private System.Windows.Forms.MenuItem DockTopMenuItem;
		private System.Windows.Forms.MenuItem DockBottomMenuItem;
		private System.Windows.Forms.MenuItem DockSeperatorMenuItem;
		private System.Windows.Forms.MenuItem DockNoneMenuItem;
		private System.Windows.Forms.MenuItem GenerateLoanDocumentMenuItem;
		private System.Windows.Forms.MenuItem Seperator2MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.Windows.Forms.MenuItem GenerateFailedPledgesDocumentMenuItem;
		private C1.Win.C1Input.C1Label BankLoanResetLabel;
		private System.Windows.Forms.ContextMenu ActivityContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToMailRecipientMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem ViewAllActivityMenuItem;
		private System.Windows.Forms.MenuItem PledgeInputMenuItem;
		private System.Windows.Forms.MenuItem ViewReleaseReportMenuItem;		
		private System.Windows.Forms.ContextMenu MainContextMenu;
		
		public PositionBankLoanForm(MainForm mainForm)
		{
			try
			{
				this.mainForm = mainForm;
				InitializeComponent();		
			         
				bankLoanActivityEventHandler = new BankLoanActivityEventHandler(DoBankLoanActivityEvent);
				bankLoanActivityEventArgsArray = new ArrayList();

				bankLoanActivityWrapper = new BankLoanActivityWrapper();
				bankLoanActivityWrapper.BankLoanActivityEvent += new BankLoanActivityEventHandler(OnBankLoanActivityEvent);
		
				tempPath = Standard.ConfigValue("TempPath", @"C:\");
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanForm.PositionBankLoanForm]", Log.Error, 1); 
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

		public bool IsReady
		{
			get
			{
				return isReady;
			}

			set
			{
				if (value && (bankLoanActivityEventArgsArray.Count > 0))
				{
					isReady = false;

					bankLoanActivityEventHandler.BeginInvoke((BankLoanActivityEventArgs)bankLoanActivityEventArgsArray[0], null, null);
					bankLoanActivityEventArgsArray.RemoveAt(0);
				}
				else
				{
					isReady = value;
				}       
			}
		}
		
		private void OnBankLoanActivityEvent(BankLoanActivityEventArgs bankLoanActivityEventArgs)
		{
			bankLoanActivityEventArgsArray.Add(bankLoanActivityEventArgs);    
      
			if (IsReady)
			{
				this.IsReady = true;
			}
		}

		private void DoBankLoanActivityEvent(BankLoanActivityEventArgs bankLoanActivityEventArgs)
		{  
			DataConfig(bankLoanActivityEventArgs);

			this.IsReady = true;
		}

		public void DataConfig(BankLoanActivityEventArgs bankLoanActivityEventArgs)
		{
			try
			{
				dataSet.Tables["Activity"].BeginLoadData();
				bankLoanActivityEventArgs.UtcOffset = mainForm.UtcOffset;                
				dataSet.Tables["Activity"].LoadDataRow(bankLoanActivityEventArgs.Values, true);              
				dataSet.Tables["Activity"].EndLoadData();     
				LoanActivitySummary();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanForm.DataConfig]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionBankLoanForm));
			this.LoansGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.TopPanel = new System.Windows.Forms.Panel();
			this.ActivityGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.ActivityContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToMailRecipientMenuItem = new System.Windows.Forms.MenuItem();
			this.ViewAllActivityMenuItem = new System.Windows.Forms.MenuItem();
			this.vSplitter = new System.Windows.Forms.Splitter();
			this.BanksGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
			this.BookGroupLabel = new C1.Win.C1Input.C1Label();
			this.BookGroupCombo = new C1.Win.C1List.C1Combo();
			this.hSplitter = new System.Windows.Forms.Splitter();
			this.LoanTypeDropdown = new C1.Win.C1TrueDBGrid.C1TrueDBDropdown();
			this.ActivityTypeDropDown = new C1.Win.C1TrueDBGrid.C1TrueDBDropdown();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.PledgeMenuItem = new System.Windows.Forms.MenuItem();
			this.PledgeInputMenuItem = new System.Windows.Forms.MenuItem();
			this.ReleaseMenuItem = new System.Windows.Forms.MenuItem();
			this.GenerateLoanDocumentMenuItem = new System.Windows.Forms.MenuItem();
			this.GenerateFailedPledgesDocumentMenuItem = new System.Windows.Forms.MenuItem();
			this.ViewReleaseReportMenuItem = new System.Windows.Forms.MenuItem();
			this.SeperatorMenuItem = new System.Windows.Forms.MenuItem();
			this.DockMenuItem = new System.Windows.Forms.MenuItem();
			this.DockTopMenuItem = new System.Windows.Forms.MenuItem();
			this.DockBottomMenuItem = new System.Windows.Forms.MenuItem();
			this.DockSeperatorMenuItem = new System.Windows.Forms.MenuItem();
			this.DockNoneMenuItem = new System.Windows.Forms.MenuItem();
			this.Seperator2MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.BankLoanResetLabel = new C1.Win.C1Input.C1Label();
			((System.ComponentModel.ISupportInitialize)(this.LoansGrid)).BeginInit();
			this.TopPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ActivityGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BanksGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LoanTypeDropdown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ActivityTypeDropDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BankLoanResetLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// LoansGrid
			// 
			this.LoansGrid.AllowAddNew = true;
			this.LoansGrid.AllowColMove = false;
			this.LoansGrid.AllowColSelect = false;
			this.LoansGrid.AllowDelete = true;
			this.LoansGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.LoansGrid.Caption = "Loans";
			this.LoansGrid.CaptionHeight = 17;
			this.LoansGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.LoansGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.LoansGrid.EmptyRows = true;
			this.LoansGrid.ExtendRightColumn = true;
			this.LoansGrid.FilterBar = true;
			this.LoansGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.LoansGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.LoansGrid.Location = new System.Drawing.Point(0, 453);
			this.LoansGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.LoansGrid.Name = "LoansGrid";
			this.LoansGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.LoansGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.LoansGrid.PreviewInfo.ZoomFactor = 75;
			this.LoansGrid.RecordSelectorWidth = 16;
			this.LoansGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.LoansGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.LoansGrid.RowHeight = 15;
			this.LoansGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.LoansGrid.Size = new System.Drawing.Size(1304, 256);
			this.LoansGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.LoansGrid.TabIndex = 0;
			this.LoansGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.LoansGrid_Paint);
			this.LoansGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.LoansGrid_BeforeUpdate);
			this.LoansGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.LoansGrid_BeforeDelete);
			this.LoansGrid.OnAddNew += new System.EventHandler(this.LoansGrid_OnAddNew);
			this.LoansGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.LoansGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoansGrid_KeyPress);
			this.LoansGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book\" DataF" +
				"ield=\"Book\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Loan Date\" DataField=\"LoanDate\" NumberFormat=\"FormatText Event\"><ValueIte" +
				"ms /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"T\" DataField=\"" +
				"LoanType\" DropDownCtl=\"LoanTypeDropdown\"><ValueItems /><GroupInfo /></C1DataColu" +
				"mn><C1DataColumn Level=\"0\" Caption=\"Haircut (%)\" DataField=\"HairCut\"><ValueItems" +
				" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Price Min\" DataF" +
				"ield=\"PriceMin\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1D" +
				"ataColumn><C1DataColumn Level=\"0\" Caption=\"Loan Amt\" DataField=\"LoanAmount\" Numb" +
				"erFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataCol" +
				"umn Level=\"0\" Caption=\"Actor\" DataField=\"ActUserShortName\"><ValueItems /><GroupI" +
				"nfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataField=\"ActTi" +
				"me\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C" +
				"1DataColumn Level=\"0\" Caption=\"Comment\" DataField=\"Comment\"><ValueItems /><Group" +
				"Info /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"PR Amt\" DataField=\"Pledge" +
				"AmountRequested\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"PM Amt\" DataField=\"PledgedAmount\" Nu" +
				"mberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
				"olumn Level=\"0\" Caption=\"BookGroup\" DataField=\"BookGroup\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"A\" DataField=\"ActivityType\"" +
				" DropDownCtl=\"ActivityTypeDropDown\"><ValueItems /><GroupInfo /></C1DataColumn><C" +
				"1DataColumn Level=\"0\" Caption=\"Sp Min\" DataField=\"SpMin\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Moody Min\" DataField=\"MoodyM" +
				"in\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"L" +
				"oan Amt COB Yesterday\" DataField=\"LoanAmountCOB\" NumberFormat=\"FormatText Event\"" +
				"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Prev" +
				"iousLoanAmount\" DataField=\"PreviousLoanAmount\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Da" +
				"ta>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Inactive{ForeColor:" +
				"InactiveCaptionText;BackColor:InactiveCaption;}Style78{}Style79{}Style85{}Editor" +
				"{}Style117{}Style116{}Style72{}Style73{}Style70{AlignHorz:Near;}Style71{AlignHor" +
				"z:Near;}Style76{AlignHorz:Near;}Style77{Font:Verdana, 8.25pt, style=Bold;AlignHo" +
				"rz:Far;ForeColor:HotTrack;}Style74{}Style75{}Style84{}Style87{}Style86{}Style81{" +
				"}Style80{}Style83{Font:Verdana, 8.25pt, style=Bold;AlignHorz:Far;}Style82{AlignH" +
				"orz:Near;}Style89{AlignHorz:Center;}Style88{AlignHorz:Center;}Style108{}Style109" +
				"{}Style104{}Style105{}Style106{AlignHorz:Near;}Style107{AlignHorz:Far;}Style100{" +
				"AlignHorz:Near;}Style101{AlignHorz:Near;}Style102{}Style103{}Style94{AlignHorz:N" +
				"ear;}Style95{AlignHorz:Near;}Style96{}Style97{}Style90{}Style91{}Style92{}Style9" +
				"3{}Style98{}Style99{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1," +
				" 1;ForeColor:ControlText;AlignVert:Center;}Style18{}Style19{}Style14{}Style15{}S" +
				"tyle16{AlignHorz:Near;}Style17{AlignHorz:Near;BackColor:WhiteSmoke;}Style10{Alig" +
				"nHorz:Near;}Style11{}Style12{}Style13{BackColor:SeaShell;}Selected{ForeColor:Hig" +
				"hlightText;BackColor:Highlight;}Style22{AlignHorz:Near;}Style27{}Style28{AlignHo" +
				"rz:Center;}Style24{}Style9{}Style8{}Style26{}Style29{AlignHorz:Center;}Style5{}S" +
				"tyle4{}Style7{}Style6{}Style25{}Style23{Font:Verdana, 8.25pt;AlignHorz:Near;Fore" +
				"Color:Black;BackColor:WhiteSmoke;}Style3{}Style2{}Style21{}Style20{}OddRow{}Styl" +
				"e38{}Style39{}Style36{}FilterBar{}Style37{}Style34{AlignHorz:Near;}Style35{Align" +
				"Horz:Far;}Style32{}Style30{}Style49{}Style48{}Style31{}Style33{}Normal{Font:Verd" +
				"ana, 8.25pt;}Style41{AlignHorz:Near;}Style40{AlignHorz:Near;}Style43{}Style42{}S" +
				"tyle45{}Style44{}Style47{AlignHorz:Far;}Style46{AlignHorz:Near;}EvenRow{BackColo" +
				"r:Aqua;}Style59{AlignHorz:Near;}Style58{AlignHorz:Near;}RecordSelector{AlignImag" +
				"e:Center;}Style51{}Style50{}Footer{}Style52{AlignHorz:Near;}Style53{AlignHorz:Fa" +
				"r;}Style54{}Style55{}Style56{}Style57{}Style115{}Caption{AlignHorz:Center;}Style" +
				"112{AlignHorz:Near;}Style69{}Style68{}Group{AlignVert:Center;Border:None,,0, 0, " +
				"0, 0;BackColor:ControlDark;}Style1{}Style63{}Style62{}Style61{}Style60{}Style67{" +
				"}Style66{}Style65{AlignHorz:Near;}Style64{AlignHorz:Near;}Style114{}Style111{}St" +
				"yle110{}Style113{AlignHorz:Far;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.Mer" +
				"geView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColMove=\"False\" AllowColSelect=\"" +
				"False\" Name=\"\" AllowRowSizing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\"" +
				" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=" +
				"\"SolidCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGro" +
				"up=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><E" +
				"ditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Styl" +
				"e8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Foo" +
				"ter\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle paren" +
				"t=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /" +
				"><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=" +
				"\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><Selected" +
				"Style parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><inte" +
				"rnalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style pa" +
				"rent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><Editor" +
				"Style parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style" +
				"27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><C" +
				"olumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>1</DCIdx><" +
				"/C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><" +
				"Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /" +
				"><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" m" +
				"e=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Vi" +
				"sible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><Locked>T" +
				"rue</Locked><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><Group" +
				"HeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=" +
				"\"Style32\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider" +
				"><Width>35</Width><Height>15</Height><Button>True</Button><DCIdx>2</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style88\" /><Style" +
				" parent=\"Style1\" me=\"Style89\" /><FooterStyle parent=\"Style3\" me=\"Style90\" /><Edi" +
				"torStyle parent=\"Style5\" me=\"Style91\" /><GroupHeaderStyle parent=\"Style1\" me=\"St" +
				"yle93\" /><GroupFooterStyle parent=\"Style1\" me=\"Style92\" /><Visible>True</Visible" +
				"><ColumnDivider>DarkGray,Single</ColumnDivider><Width>35</Width><Height>15</Heig" +
				"ht><Button>True</Button><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
				"dingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><" +
				"FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Styl" +
				"e37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent" +
				"=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</" +
				"ColumnDivider><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayCol" +
				"umn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Styl" +
				"e41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" " +
				"me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyl" +
				"e parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>DarkGray," +
				"Single</ColumnDivider><Width>80</Width><Height>15</Height><DCIdx>13</DCIdx></C1D" +
				"isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style100\" /><Sty" +
				"le parent=\"Style1\" me=\"Style101\" /><FooterStyle parent=\"Style3\" me=\"Style102\" />" +
				"<EditorStyle parent=\"Style5\" me=\"Style103\" /><GroupHeaderStyle parent=\"Style1\" m" +
				"e=\"Style105\" /><GroupFooterStyle parent=\"Style1\" me=\"Style104\" /><Visible>True</" +
				"Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>80</Width><Height>1" +
				"5</Height><DCIdx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pare" +
				"nt=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><FooterStyle pa" +
				"rent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupH" +
				"eaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"" +
				"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider>" +
				"<Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSt" +
				"yle parent=\"Style2\" me=\"Style106\" /><Style parent=\"Style1\" me=\"Style107\" /><Foot" +
				"erStyle parent=\"Style3\" me=\"Style108\" /><EditorStyle parent=\"Style5\" me=\"Style10" +
				"9\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style111\" /><GroupFooterStyle parent=" +
				"\"Style1\" me=\"Style110\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</" +
				"ColumnDivider><Width>169</Width><Height>15</Height><Locked>True</Locked><DCIdx>1" +
				"5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=" +
				"\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><Visibl" +
				"e>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height" +
				"><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"\" me=\"Style112\" /><Style parent=\"Style1\" me=\"Style113\" /><FooterStyle parent=\"St" +
				"yle3\" me=\"Style114\" /><EditorStyle parent=\"Style5\" me=\"Style115\" /><GroupHeaderS" +
				"tyle parent=\"Style1\" me=\"Style117\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
				"116\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>136</Width><Height>1" +
				"5</Height><Locked>True</Locked><DCIdx>16</DCIdx></C1DisplayColumn><C1DisplayColu" +
				"mn><HeadingStyle parent=\"Style2\" me=\"Style76\" /><Style parent=\"Style1\" me=\"Style" +
				"77\" /><FooterStyle parent=\"Style3\" me=\"Style78\" /><EditorStyle parent=\"Style5\" m" +
				"e=\"Style79\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style81\" /><GroupFooterStyle" +
				" parent=\"Style1\" me=\"Style80\" /><Visible>True</Visible><ColumnDivider>DarkGray,S" +
				"ingle</ColumnDivider><Width>120</Width><Height>15</Height><Locked>True</Locked><" +
				"DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" " +
				"me=\"Style82\" /><Style parent=\"Style1\" me=\"Style83\" /><FooterStyle parent=\"Style3" +
				"\" me=\"Style84\" /><EditorStyle parent=\"Style5\" me=\"Style85\" /><GroupHeaderStyle p" +
				"arent=\"Style1\" me=\"Style87\" /><GroupFooterStyle parent=\"Style1\" me=\"Style86\" /><" +
				"Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>120</" +
				"Width><Height>15</Height><Locked>True</Locked><DCIdx>10</DCIdx></C1DisplayColumn" +
				"><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"St" +
				"yle1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle pa" +
				"rent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><G" +
				"roupFooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDiv" +
				"ider>DarkGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCI" +
				"dx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style64\" /><Style parent=\"Style1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style66\" /><EditorStyle parent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style69\" /><GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><Vis" +
				"ible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>125</Wid" +
				"th><Height>15</Height><Locked>True</Locked><DCIdx>7</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style70\" /><Style parent=\"Style1" +
				"\" me=\"Style71\" /><FooterStyle parent=\"Style3\" me=\"Style72\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style73\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style75\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style74\" /><Visible>True</Visible><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>8</DCIdx></C1DisplayCo" +
				"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style94\" /><Style parent" +
				"=\"Style1\" me=\"Style95\" /><FooterStyle parent=\"Style3\" me=\"Style96\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style97\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style99\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style98\" /><ColumnDivider>DarkGray,Singl" +
				"e</ColumnDivider><Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn></interna" +
				"lCols><ClientRect>0, 17, 1300, 235</ClientRect><BorderSide>0</BorderSide></C1.Wi" +
				"n.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><S" +
				"tyle parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style" +
				" parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style " +
				"parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style pare" +
				"nt=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style par" +
				"ent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style " +
				"parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedSty" +
				"les><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout" +
				"><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 1300, 252</ClientA" +
				"rea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=" +
				"\"\" me=\"Style15\" /></Blob>";
			// 
			// TopPanel
			// 
			this.TopPanel.Controls.Add(this.ActivityGrid);
			this.TopPanel.Controls.Add(this.vSplitter);
			this.TopPanel.Controls.Add(this.BanksGrid);
			this.TopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TopPanel.Location = new System.Drawing.Point(0, 40);
			this.TopPanel.Name = "TopPanel";
			this.TopPanel.Size = new System.Drawing.Size(1304, 413);
			this.TopPanel.TabIndex = 1;
			// 
			// ActivityGrid
			// 
			this.ActivityGrid.AllowColSelect = false;
			this.ActivityGrid.AllowFilter = false;
			this.ActivityGrid.AllowRowSelect = false;
			this.ActivityGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.ActivityGrid.AllowUpdate = false;
			this.ActivityGrid.Caption = "Activity";
			this.ActivityGrid.CaptionHeight = 17;
			this.ActivityGrid.ContextMenu = this.ActivityContextMenu;
			this.ActivityGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ActivityGrid.EmptyRows = true;
			this.ActivityGrid.ExtendRightColumn = true;
			this.ActivityGrid.FetchRowStyles = true;
			this.ActivityGrid.FilterBar = true;
			this.ActivityGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ActivityGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.ActivityGrid.Location = new System.Drawing.Point(475, 0);
			this.ActivityGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.ActivityGrid.Name = "ActivityGrid";
			this.ActivityGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ActivityGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ActivityGrid.PreviewInfo.ZoomFactor = 75;
			this.ActivityGrid.RecordSelectorWidth = 16;
			this.ActivityGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.ActivityGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ActivityGrid.RowHeight = 15;
			this.ActivityGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.ActivityGrid.Size = new System.Drawing.Size(829, 413);
			this.ActivityGrid.TabIndex = 1;
			this.ActivityGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.ActivityGrid_Paint);
			this.ActivityGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.ActivityGrid_FetchRowStyle);
			this.ActivityGrid.FilterChange += new System.EventHandler(this.ActivityGrid_FilterChange);
			this.ActivityGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.ActivityGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
				"\" DataField=\"SecId\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Symbol\" DataField=\"Symbol\"><Value" +
				"Items /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Quantity\" D" +
				"ataField=\"Quantity\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Status\" DataField=\"Status\"><Value" +
				"Items /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Book\" DataF" +
				"ield=\"Book\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Loan Date\" DataField=\"LoanDate\" NumberFormat=\"FormatText Event\"><ValueIte" +
				"ms /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Actor\" DataFie" +
				"ld=\"ActUserShortName\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"Act Time\" DataField=\"ActTime\" NumberFormat=\"FormatText Event\"><" +
				"ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Amount" +
				"\" DataField=\"Amount\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo />" +
				"</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"ProcessId\" DataField=\"ProcessId\"" +
				"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Flag" +
				"\" DataField=\"Flag\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles " +
				"type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:Hi" +
				"ghlightText;BackColor:Highlight;}Inactive{ForeColor:InactiveCaptionText;BackColo" +
				"r:InactiveCaption;}Style78{}Style79{}Selected{ForeColor:HighlightText;BackColor:" +
				"Highlight;}Editor{}Style72{}Style73{}Style70{AlignHorz:Near;}Style71{AlignHorz:F" +
				"ar;}Style76{AlignHorz:Near;}Style77{AlignHorz:Near;}Style74{}Style75{}Style81{}S" +
				"tyle80{}FilterBar{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;" +
				"ForeColor:ControlText;BackColor:Control;}Style18{}Style19{}Style14{}Style15{}Sty" +
				"le16{AlignHorz:Near;}Style17{AlignHorz:Near;BackColor:WhiteSmoke;}Style10{AlignH" +
				"orz:Near;}Style11{}Style12{}Style13{BackColor:SeaShell;}Style29{AlignHorz:Far;}S" +
				"tyle27{}Style22{AlignHorz:Near;}Style28{AlignHorz:Near;}Style26{}Style9{}Style8{" +
				"}Style25{}Style24{}Style5{}Style4{}Style7{}Style6{}Style1{}Style23{AlignHorz:Nea" +
				"r;BackColor:WhiteSmoke;}Style3{}Style2{}Style21{}Style20{}OddRow{}Style38{}Style" +
				"39{}Style36{}Style37{}Style34{AlignHorz:Center;}Style35{AlignHorz:Center;}Style3" +
				"2{}Style33{}Style30{}Style49{}Style48{}Style31{}Normal{Font:Verdana, 8.25pt;}Sty" +
				"le41{AlignHorz:Near;}Style40{AlignHorz:Near;}Style43{}Style42{}Style45{}Style44{" +
				"}Style47{AlignHorz:Near;}Style46{AlignHorz:Near;}EvenRow{BackColor:Aqua;}Style59" +
				"{AlignHorz:Near;}Style58{AlignHorz:Near;}RecordSelector{AlignImage:Center;}Style" +
				"51{}Style50{}Footer{}Style52{AlignHorz:Near;}Style53{AlignHorz:Near;}Style54{}St" +
				"yle55{}Style56{}Style57{}Caption{AlignHorz:Center;}Style69{}Style68{}Style63{}St" +
				"yle62{}Style61{}Style60{}Style67{}Style66{}Style65{AlignHorz:Near;}Style64{Align" +
				"Horz:Near;}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;" +
				"}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarSty" +
				"le=\"Always\" AllowColSelect=\"False\" AllowRowSelect=\"False\" Name=\"\" AllowRowSizing" +
				"=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Exte" +
				"ndRightColumn=\"True\" FetchRowStyles=\"True\" FilterBar=\"True\" MarqueeStyle=\"SolidC" +
				"ellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" " +
				"HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorSt" +
				"yle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><" +
				"FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me" +
				"=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Head" +
				"ing\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><Inact" +
				"iveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9" +
				"\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle p" +
				"arent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCol" +
				"s><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"S" +
				"tyle1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle p" +
				"arent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><" +
				"GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><ColumnDivider>DarkGray,Single</" +
				"ColumnDivider><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayCol" +
				"umn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Styl" +
				"e41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" " +
				"me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyl" +
				"e parent=\"Style1\" me=\"Style44\" /><ColumnDivider>DarkGray,Single</ColumnDivider><" +
				"Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterS" +
				"tyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" />" +
				"<GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style" +
				"1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Width>95</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1" +
				"\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Width>75</Width><Height>15</Height><DCIdx>1</DC" +
				"Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28" +
				"\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style" +
				"30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Styl" +
				"e1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>Tru" +
				"e</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCI" +
				"dx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style70\" /><Style parent=\"Style1\" me=\"Style71\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style72\" /><EditorStyle parent=\"Style5\" me=\"Style73\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style75\" /><GroupFooterStyle parent=\"Style1\" me=\"Style74\" /><Vis" +
				"ible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Hei" +
				"ght><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
				"le2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"S" +
				"tyle3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderSt" +
				"yle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38" +
				"\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>" +
				"50</Width><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn>" +
				"<HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\"" +
				" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"" +
				"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle pa" +
				"rent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><ColumnDivider>DarkGray,Sing" +
				"le</ColumnDivider><Width>106</Width><Height>15</Height><DCIdx>6</DCIdx></C1Displ" +
				"ayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style pa" +
				"rent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><Editor" +
				"Style parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style" +
				"63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible><C" +
				"olumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>7</DCIdx><" +
				"/C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style76\" /><" +
				"Style parent=\"Style1\" me=\"Style77\" /><FooterStyle parent=\"Style3\" me=\"Style78\" /" +
				"><EditorStyle parent=\"Style5\" me=\"Style79\" /><GroupHeaderStyle parent=\"Style1\" m" +
				"e=\"Style81\" /><GroupFooterStyle parent=\"Style1\" me=\"Style80\" /><ColumnDivider>Da" +
				"rkGray,Single</ColumnDivider><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColum" +
				"n><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"S" +
				"tyle1\" me=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle p" +
				"arent=\"Style5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><" +
				"GroupFooterStyle parent=\"Style1\" me=\"Style68\" /><ColumnDivider>DarkGray,Single</" +
				"ColumnDivider><Height>15</Height><DCIdx>10</DCIdx></C1DisplayColumn></internalCo" +
				"ls><ClientRect>0, 17, 825, 392</ClientRect><BorderSide>0</BorderSide></C1.Win.C1" +
				"TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style" +
				" parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style par" +
				"ent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style pare" +
				"nt=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"" +
				"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=" +
				"\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style pare" +
				"nt=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles>" +
				"<vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><De" +
				"faultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 825, 409</ClientArea><" +
				"PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me" +
				"=\"Style15\" /></Blob>";
			// 
			// ActivityContextMenu
			// 
			this.ActivityContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																												this.SendToMenuItem,
																																												this.ViewAllActivityMenuItem});
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
			// ViewAllActivityMenuItem
			// 
			this.ViewAllActivityMenuItem.Enabled = false;
			this.ViewAllActivityMenuItem.Index = 1;
			this.ViewAllActivityMenuItem.Text = "View All Activity";
			this.ViewAllActivityMenuItem.Click += new System.EventHandler(this.ViewAllActivityMenuItem_Click);
			// 
			// vSplitter
			// 
			this.vSplitter.Location = new System.Drawing.Point(472, 0);
			this.vSplitter.Name = "vSplitter";
			this.vSplitter.Size = new System.Drawing.Size(3, 413);
			this.vSplitter.TabIndex = 2;
			this.vSplitter.TabStop = false;
			// 
			// BanksGrid
			// 
			this.BanksGrid.AllowAddNew = true;
			this.BanksGrid.AllowDelete = true;
			this.BanksGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.BanksGrid.Caption = "Banks";
			this.BanksGrid.CaptionHeight = 17;
			this.BanksGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.BanksGrid.Dock = System.Windows.Forms.DockStyle.Left;
			this.BanksGrid.EmptyRows = true;
			this.BanksGrid.ExtendRightColumn = true;
			this.BanksGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.BanksGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.BanksGrid.Location = new System.Drawing.Point(0, 0);
			this.BanksGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.BanksGrid.Name = "BanksGrid";
			this.BanksGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.BanksGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.BanksGrid.PreviewInfo.ZoomFactor = 75;
			this.BanksGrid.RecordSelectorWidth = 16;
			this.BanksGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.BanksGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.BanksGrid.RowHeight = 15;
			this.BanksGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.BanksGrid.Size = new System.Drawing.Size(472, 413);
			this.BanksGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.BanksGrid.TabIndex = 0;
			this.BanksGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.BanksGrid_Paint);
			this.BanksGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.BanksGrid_BeforeUpdate);
			this.BanksGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.BanksGrid_BeforeDelete);
			this.BanksGrid.OnAddNew += new System.EventHandler(this.BanksGrid_OnAddNew);
			this.BanksGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.BanksGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BanksGrid_KeyPress);
			this.BanksGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Book\" DataF" +
				"ield=\"Book\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Name\" DataField=\"Name\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
				"olumn Level=\"0\" Caption=\"Phone\" DataField=\"Phone\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn><C1DataColumn Level=\"0\" Caption=\"Fax\" DataField=\"Fax\"><ValueItems />" +
				"<GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Actor\" DataField=\"A" +
				"ctUserShortName\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"" +
				"0\" Caption=\"Act Time\" DataField=\"ActTime\" NumberFormat=\"FormatText Event\"><Value" +
				"Items /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"BookGroup\" " +
				"DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"Contact\" DataField=\"Contact\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Dat" +
				"a>Style58{AlignHorz:Near;}Style59{AlignHorz:Near;}Style50{}Style51{}Style52{Alig" +
				"nHorz:Near;}Style53{AlignHorz:Near;}Style54{}Caption{AlignHorz:Center;}Style56{}" +
				"Normal{Font:Verdana, 8.25pt;}Style25{}Selected{ForeColor:HighlightText;BackColor" +
				":Highlight;}Editor{}Style31{}Style18{}Style19{}Style14{}Style15{}Style16{AlignHo" +
				"rz:Near;}Style17{AlignHorz:Near;BackColor:WhiteSmoke;}Style10{AlignHorz:Near;}St" +
				"yle11{}Style12{}Style13{}Style47{AlignHorz:Near;}Style63{}Style62{}Style61{}Styl" +
				"e60{}Style38{}Style37{}Style34{AlignHorz:Near;}Style35{AlignHorz:Near;}Style32{}" +
				"Style33{}Style2{}OddRow{}Style29{AlignHorz:Near;}Style28{AlignHorz:Near;}Style27" +
				"{}Style26{}RecordSelector{AlignImage:Center;}Footer{}Style23{AlignHorz:Near;Back" +
				"Color:WhiteSmoke;}Style22{AlignHorz:Near;}Style21{}Style55{}Group{AlignVert:Cent" +
				"er;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style57{}Inactive{ForeColor:In" +
				"activeCaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wra" +
				"p:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVe" +
				"rt:Center;}Style49{}Style48{}Style24{}Style9{}Style20{}Style41{AlignHorz:Near;}S" +
				"tyle40{AlignHorz:Near;}Style43{}FilterBar{}Style42{}Style45{}Style44{}Style46{Al" +
				"ignHorz:Near;}Style8{}Style39{}Style36{}Style5{}Style4{}Style7{}Style6{}Style1{}" +
				"Style30{}Style3{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}</Dat" +
				"a></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Al" +
				"ways\" Name=\"\" AllowRowSizing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" " +
				"ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" MarqueeStyle=\"SolidCellBorder\" " +
				"RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalS" +
				"crollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=" +
				"\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarSt" +
				"yle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /" +
				"><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"St" +
				"yle2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle pa" +
				"rent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><Record" +
				"SelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Sele" +
				"cted\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1Displa" +
				"yColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"" +
				"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Styl" +
				"e5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooter" +
				"Style parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivider>DarkG" +
				"ray,Single</ColumnDivider><Width>75</Width><Height>15</Height><DCIdx>0</DCIdx></" +
				"C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><S" +
				"tyle parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" />" +
				"<EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me" +
				"=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Vis" +
				"ible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>1</" +
				"DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style" +
				"58\" /><Style parent=\"Style1\" me=\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Sty" +
				"le60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><GroupHeaderStyle parent=\"St" +
				"yle1\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1\" me=\"Style62\" /><Visible>T" +
				"rue</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><D" +
				"CIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
				"e=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\"" +
				" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><V" +
				"isible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</H" +
				"eight><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
				"tyle2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=" +
				"\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeader" +
				"Style parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
				"38\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Heig" +
				"ht>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
				"arent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle" +
				" parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><Gro" +
				"upHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" m" +
				"e=\"Style44\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivid" +
				"er><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
				"gStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><Foo" +
				"terStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49" +
				"\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"S" +
				"tyle1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Col" +
				"umnDivider><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn" +
				"><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53" +
				"\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=" +
				"\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle p" +
				"arent=\"Style1\" me=\"Style56\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Hei" +
				"ght>15</Height><DCIdx>6</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 1" +
				"7, 468, 392</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeVie" +
				"w></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me" +
				"=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"C" +
				"aption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Sel" +
				"ected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"Highlig" +
				"htRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow" +
				"\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"Fil" +
				"terBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vert" +
				"Splits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16" +
				"</DefaultRecSelWidth><ClientArea>0, 0, 468, 409</ClientArea><PrintPageHeaderStyl" +
				"e parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob" +
				">";
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
				"pper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center" +
				";}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackCo" +
				"lor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive" +
				"{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignH" +
				"orz:Center;}Normal{BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackCo" +
				"lor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}" +
				"Style13{Font:Verdana, 8.25pt;AlignHorz:Near;}Heading{Wrap:True;AlignVert:Center;" +
				"Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style" +
				"10{}Style11{}Style14{}Style15{AlignHorz:Near;}Style16{Font:Verdana, 8.25pt;Align" +
				"Horz:Near;}Style17{}Style1{Font:Verdana, 8.25pt;}</Data></Styles><Splits><C1.Win" +
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
			this.hSplitter.Location = new System.Drawing.Point(0, 450);
			this.hSplitter.Name = "hSplitter";
			this.hSplitter.Size = new System.Drawing.Size(1304, 3);
			this.hSplitter.TabIndex = 10;
			this.hSplitter.TabStop = false;
			// 
			// LoanTypeDropdown
			// 
			this.LoanTypeDropdown.AllowColMove = true;
			this.LoanTypeDropdown.AllowColSelect = true;
			this.LoanTypeDropdown.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows;
			this.LoanTypeDropdown.AlternatingRows = false;
			this.LoanTypeDropdown.CaptionHeight = 17;
			this.LoanTypeDropdown.ColumnCaptionHeight = 17;
			this.LoanTypeDropdown.ColumnFooterHeight = 17;
			this.LoanTypeDropdown.EmptyRows = true;
			this.LoanTypeDropdown.ExtendRightColumn = true;
			this.LoanTypeDropdown.FetchRowStyles = false;
			this.LoanTypeDropdown.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LoanTypeDropdown.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
			this.LoanTypeDropdown.Location = new System.Drawing.Point(288, 552);
			this.LoanTypeDropdown.Name = "LoanTypeDropdown";
			this.LoanTypeDropdown.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.LoanTypeDropdown.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.LoanTypeDropdown.RowHeight = 15;
			this.LoanTypeDropdown.RowSubDividerColor = System.Drawing.Color.Gainsboro;
			this.LoanTypeDropdown.ScrollTips = false;
			this.LoanTypeDropdown.Size = new System.Drawing.Size(224, 144);
			this.LoanTypeDropdown.TabIndex = 12;
			this.LoanTypeDropdown.TabStop = false;
			this.LoanTypeDropdown.Visible = false;
			this.LoanTypeDropdown.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.LoanTypeDropdown_SelChange);
			this.LoanTypeDropdown.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Loan Type\" " +
				"DataField=\"LoanType\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Lev" +
				"el=\"0\" Caption=\"Loan Type Name\" DataField=\"LoanTypeName\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWr" +
				"apper\"><Data>Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;}Style25{}Sel" +
				"ected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style18{}Style19{}Sty" +
				"le14{AlignHorz:Near;}Style15{AlignHorz:Near;BackColor:WhiteSmoke;}Style16{}Style" +
				"17{}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}Style12{}Footer{}Highlight" +
				"Row{ForeColor:HighlightText;BackColor:Highlight;}RecordSelector{AlignImage:Cente" +
				"r;}Style24{}Style23{}Style22{}Style21{AlignHorz:Near;}Style20{AlignHorz:Near;}In" +
				"active{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}EvenRow{BackColo" +
				"r:Aqua;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:" +
				"ControlText;AlignVert:Center;}FilterBar{}Style4{}Style9{}Style8{}Style5{}Group{A" +
				"lignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style7{}Style6{}S" +
				"tyle1{}Style3{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.DropdownView" +
				" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Ext" +
				"endRightColumn=\"True\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"16\" D" +
				"efRecSelWidth=\"16\" RecordSelectors=\"False\" VerticalScrollGroup=\"1\" HorizontalScr" +
				"ollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"E" +
				"ditor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyl" +
				"e parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><" +
				"GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Styl" +
				"e2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle pare" +
				"nt=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSe" +
				"lectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Select" +
				"ed\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayC" +
				"olumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent=\"Style1\" me=\"St" +
				"yle15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"Style5" +
				"\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><GroupFooterSt" +
				"yle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><ColumnDivider>DarkGra" +
				"y,Single</ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style20\" /><Style parent=\"Style1" +
				"\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Style22\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style25\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style24\" /><Visible>True</Visible><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C1DisplayCo" +
				"lumn></internalCols><ClientRect>0, 0, 220, 140</ClientRect><BorderSide>0</Border" +
				"Side></C1.Win.C1TrueDBGrid.DropdownView></Splits><NamedStyles><Style parent=\"\" m" +
				"e=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"F" +
				"ooter\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inac" +
				"tive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor" +
				"\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRo" +
				"w\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSele" +
				"ctor\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Grou" +
				"p\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>M" +
				"odified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 220" +
				", 140</ClientArea></Blob>";
			// 
			// ActivityTypeDropDown
			// 
			this.ActivityTypeDropDown.AllowColMove = true;
			this.ActivityTypeDropDown.AllowColSelect = true;
			this.ActivityTypeDropDown.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows;
			this.ActivityTypeDropDown.AlternatingRows = false;
			this.ActivityTypeDropDown.CaptionHeight = 17;
			this.ActivityTypeDropDown.ColumnCaptionHeight = 17;
			this.ActivityTypeDropDown.ColumnFooterHeight = 17;
			this.ActivityTypeDropDown.EmptyRows = true;
			this.ActivityTypeDropDown.ExtendRightColumn = true;
			this.ActivityTypeDropDown.FetchRowStyles = false;
			this.ActivityTypeDropDown.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ActivityTypeDropDown.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
			this.ActivityTypeDropDown.Location = new System.Drawing.Point(56, 552);
			this.ActivityTypeDropDown.Name = "ActivityTypeDropDown";
			this.ActivityTypeDropDown.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.ActivityTypeDropDown.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ActivityTypeDropDown.RowHeight = 15;
			this.ActivityTypeDropDown.RowSubDividerColor = System.Drawing.Color.Gainsboro;
			this.ActivityTypeDropDown.ScrollTips = false;
			this.ActivityTypeDropDown.Size = new System.Drawing.Size(224, 144);
			this.ActivityTypeDropDown.TabIndex = 13;
			this.ActivityTypeDropDown.TabStop = false;
			this.ActivityTypeDropDown.Visible = false;
			this.ActivityTypeDropDown.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ActivityTypeDropDown_SelChange);
			this.ActivityTypeDropDown.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Activity Ty" +
				"pe\" DataField=\"ActivityType\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataCo" +
				"lumn Level=\"0\" Caption=\"Activity Type Name\" DataField=\"ActivityTypeName\"><ValueI" +
				"tems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid." +
				"Design.ContextWrapper\"><Data>Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25" +
				"pt;}Style25{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style" +
				"18{}Style19{}Style14{AlignHorz:Near;}Style15{AlignHorz:Near;BackColor:WhiteSmoke" +
				";}Style16{}Style17{}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}Style12{}F" +
				"ooter{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}RecordSelector{" +
				"AlignImage:Center;}Style24{}Style23{}Style22{}Style21{AlignHorz:Near;}Style20{Al" +
				"ignHorz:Near;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}" +
				"EvenRow{BackColor:Aqua;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, " +
				"1, 1;ForeColor:ControlText;BackColor:Control;}FilterBar{}Style4{}Style9{}Style8{" +
				"}Style5{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}S" +
				"tyle7{}Style6{}Style1{}Style3{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBG" +
				"rid.DropdownView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoote" +
				"rHeight=\"17\" ExtendRightColumn=\"True\" MarqueeStyle=\"DottedCellBorder\" RecordSele" +
				"ctorWidth=\"16\" DefRecSelWidth=\"16\" RecordSelectors=\"False\" VerticalScrollGroup=\"" +
				"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><Edito" +
				"rStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" " +
				"/><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\"" +
				" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"H" +
				"eading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><In" +
				"activeStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Sty" +
				"le9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyl" +
				"e parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internal" +
				"Cols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent" +
				"=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><Colum" +
				"nDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1D" +
				"isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style20\" /><Styl" +
				"e parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Style22\" /><Ed" +
				"itorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style1\" me=\"S" +
				"tyle25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Visible>True</Visibl" +
				"e><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>1</DCI" +
				"dx></C1DisplayColumn></internalCols><ClientRect>0, 0, 220, 140</ClientRect><Bord" +
				"erSide>0</BorderSide></C1.Win.C1TrueDBGrid.DropdownView></Splits><NamedStyles><S" +
				"tyle parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent" +
				"=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"H" +
				"eading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"No" +
				"rmal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"No" +
				"rmal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading" +
				"\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"C" +
				"aption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horz" +
				"Splits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><Clie" +
				"ntArea>0, 0, 220, 140</ClientArea></Blob>";
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.PledgeMenuItem,
																																										this.PledgeInputMenuItem,
																																										this.ReleaseMenuItem,
																																										this.GenerateLoanDocumentMenuItem,
																																										this.GenerateFailedPledgesDocumentMenuItem,
																																										this.ViewReleaseReportMenuItem,
																																										this.SeperatorMenuItem,
																																										this.DockMenuItem,
																																										this.Seperator2MenuItem,
																																										this.ExitMenuItem});
			// 
			// PledgeMenuItem
			// 
			this.PledgeMenuItem.Enabled = false;
			this.PledgeMenuItem.Index = 0;
			this.PledgeMenuItem.Text = "Pledge";
			this.PledgeMenuItem.Click += new System.EventHandler(this.PledgeMenuItem_Click);
			// 
			// PledgeInputMenuItem
			// 
			this.PledgeInputMenuItem.Enabled = false;
			this.PledgeInputMenuItem.Index = 1;
			this.PledgeInputMenuItem.Text = "Pledge Input";
			this.PledgeInputMenuItem.Click += new System.EventHandler(this.PledgeInputMenuItem_Click);
			// 
			// ReleaseMenuItem
			// 
			this.ReleaseMenuItem.Enabled = false;
			this.ReleaseMenuItem.Index = 2;
			this.ReleaseMenuItem.Text = "Release";
			this.ReleaseMenuItem.Click += new System.EventHandler(this.ReleaseMenuItem_Click);
			// 
			// GenerateLoanDocumentMenuItem
			// 
			this.GenerateLoanDocumentMenuItem.Enabled = false;
			this.GenerateLoanDocumentMenuItem.Index = 3;
			this.GenerateLoanDocumentMenuItem.Text = "Generate Loan Document";
			this.GenerateLoanDocumentMenuItem.Click += new System.EventHandler(this.GenerateLoanDocumentMenuItem_Click);
			// 
			// GenerateFailedPledgesDocumentMenuItem
			// 
			this.GenerateFailedPledgesDocumentMenuItem.Enabled = false;
			this.GenerateFailedPledgesDocumentMenuItem.Index = 4;
			this.GenerateFailedPledgesDocumentMenuItem.Text = "Generate Failed Pledges Document";
			this.GenerateFailedPledgesDocumentMenuItem.Click += new System.EventHandler(this.GenerateFailedPledgesDocumentMenuItem_Click);
			// 
			// ViewReleaseReportMenuItem
			// 
			this.ViewReleaseReportMenuItem.Enabled = false;
			this.ViewReleaseReportMenuItem.Index = 5;
			this.ViewReleaseReportMenuItem.Text = "View Release Report";
			this.ViewReleaseReportMenuItem.Click += new System.EventHandler(this.ViewReleaseReportMenuItem_Click);
			// 
			// SeperatorMenuItem
			// 
			this.SeperatorMenuItem.Index = 6;
			this.SeperatorMenuItem.Text = "-";
			// 
			// DockMenuItem
			// 
			this.DockMenuItem.Index = 7;
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
			// Seperator2MenuItem
			// 
			this.Seperator2MenuItem.Index = 8;
			this.Seperator2MenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 9;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// BankLoanResetLabel
			// 
			this.BankLoanResetLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BankLoanResetLabel.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BankLoanResetLabel.ForeColor = System.Drawing.Color.Navy;
			this.BankLoanResetLabel.Location = new System.Drawing.Point(920, 8);
			this.BankLoanResetLabel.Name = "BankLoanResetLabel";
			this.BankLoanResetLabel.Size = new System.Drawing.Size(376, 20);
			this.BankLoanResetLabel.TabIndex = 14;
			this.BankLoanResetLabel.Tag = null;
			this.BankLoanResetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BankLoanResetLabel.TextDetached = true;
			// 
			// PositionBankLoanForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1304, 709);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.BankLoanResetLabel);
			this.Controls.Add(this.ActivityTypeDropDown);
			this.Controls.Add(this.LoanTypeDropdown);
			this.Controls.Add(this.hSplitter);
			this.Controls.Add(this.BookGroupNameLabel);
			this.Controls.Add(this.BookGroupLabel);
			this.Controls.Add(this.BookGroupCombo);
			this.Controls.Add(this.TopPanel);
			this.Controls.Add(this.LoansGrid);
			this.DockPadding.Top = 40;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PositionBankLoanForm";
			this.Text = "Position - BankLoan";
			this.Load += new System.EventHandler(this.PositionBankLoanForm_Load);
			this.Closed += new System.EventHandler(this.PositionBankLoanForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.LoansGrid)).EndInit();
			this.TopPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ActivityGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BanksGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LoanTypeDropdown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ActivityTypeDropDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BankLoanResetLabel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void LoanActivitySummary()
		{
			try
			{
				decimal pledgeRequestedAmount = 0, pledgedAmount = 0;
			
				dataSet.Tables["Loans"].BeginLoadData();
				foreach (DataRow dataRowLoan in dataSet.Tables["Loans"].Rows)
				{
					pledgeRequestedAmount = 0;
					pledgedAmount = 0;

					foreach (DataRow dataRowActivity in dataSet.Tables["Activity"].Rows)
					{
						if (dataRowLoan["BookGroup"].ToString().Equals(dataRowActivity["BookGroup"].ToString()) &&
							dataRowLoan["Book"].ToString().Equals(dataRowActivity["Book"].ToString()) && 
							dataRowLoan["LoanDate"].ToString().Equals(dataRowActivity["LoanDate"].ToString()) &&
							!dataRowActivity["Amount"].ToString().Equals("") &&
							!dataRowLoan["HairCut"].ToString().Equals(""))
						{
							switch(dataRowActivity["Status"].ToString())
							{					
								case "RR":
									pledgeRequestedAmount -= decimal.Parse(dataRowActivity["Amount"].ToString()) - (decimal.Parse(dataRowActivity["Amount"].ToString()) *(decimal.Parse(dataRowLoan["HairCut"].ToString())/100));
									break;

								case "PR":					
									pledgeRequestedAmount += decimal.Parse(dataRowActivity["Amount"].ToString()) - (decimal.Parse(dataRowActivity["Amount"].ToString()) *(decimal.Parse(dataRowLoan["HairCut"].ToString())/100));
									break;

								case "RM":
									pledgedAmount -= decimal.Parse(dataRowActivity["Amount"].ToString()) - (decimal.Parse(dataRowActivity["Amount"].ToString()) *(decimal.Parse(dataRowLoan["HairCut"].ToString())/100));
									break;												 	

								case "PM":
									pledgedAmount += decimal.Parse(dataRowActivity["Amount"].ToString()) - (decimal.Parse(dataRowActivity["Amount"].ToString()) *(decimal.Parse(dataRowLoan["HairCut"].ToString())/100));
									break;												 	
							}
						}
					}
				
					dataRowLoan["PledgeAmountRequested"] = pledgeRequestedAmount;
					
					dataRowLoan.AcceptChanges();
				}

				dataSet.Tables["Loans"].EndLoadData();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionBankLoanForm.LoanActivitySummary]", Log.Error, 1); 				
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void PositionBankLoanForm_Load(object sender, System.EventArgs e)
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
				dataSet = mainForm.PositionAgent.BankLoanDataGet(mainForm.UtcOffset, mainForm.UserId, "PositionBankLoan");
				mainForm.PositionAgent.BankLoanActivityEvent += new BankLoanActivityEventHandler(bankLoanActivityWrapper.DoEvent);        
				
				BookGroupCombo.HoldFields();
				BookGroupCombo.DataSource = dataSet.Tables["BookGroups"];
				BookGroupCombo.SelectedIndex = -1;								    
				
				LoanActivitySummary();
				
				bankDataViewRowFilter = "BookGroup = ''";
				bankDataView = new DataView(dataSet.Tables["Banks"], bankDataViewRowFilter, "", DataViewRowState.CurrentRows);

				bankLoanActiivtyDataViewRowFilter = "BookGroup = ''";
				bankLoanActivityDataView = new DataView(dataSet.Tables["Activity"], bankLoanActiivtyDataViewRowFilter, "ActTime", DataViewRowState.CurrentRows);
				
				LoansGrid.AllowAddNew = false;
				LoansGrid.AllowUpdate = false;

				BanksGrid.AllowAddNew = false;
				BanksGrid.AllowUpdate = false;

				PledgeMenuItem.Enabled = false;
				ReleaseMenuItem.Enabled = false;

				GenerateLoanDocumentMenuItem.Enabled = false;

				bankLoansDataViewRowFilter = "BookGroup = ''";
				bankLoansDataView = new DataView(dataSet.Tables["Loans"], bankLoansDataViewRowFilter, "", DataViewRowState.CurrentRows);
				
				LoansGrid.SetDataBinding(bankLoansDataView, "", true);
				ActivityGrid.SetDataBinding(bankLoanActivityDataView, "", true);
				BanksGrid.SetDataBinding(bankDataView, "", true);
				
				LoanTypeDropdown.SetDataBinding(dataSet, "LoanTypes", true);
				ActivityTypeDropDown.SetDataBinding(dataSet, "ActivityTypes", true);								
				
				BookGroupCombo.Text = RegistryValue.Read(this.Name, "BookGroup", ""); 
				IsReady = true;
			
				mainForm.Alert("Please wait ... loading current bank loan data ... Done!.", PilotState.Idle);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [PositionBankLoanForm.PositionBankLoanForm_Load]", Log.Error, 1); 				
				mainForm.Alert("Please wait ... loading current bank loan data ... Error!.", PilotState.RunFault);
			}			
			
			this.Cursor = Cursors.Default;
		}

		private void PositionBankLoanForm_Closed(object sender, System.EventArgs e)
		{
			RegistryValue.Write(this.Name, "BookGroup", BookGroupCombo.Text);

			if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
			}
 
			if (positionBankLoanPledgeForm != null)
			{
				positionBankLoanPledgeForm.Close();
			}

			if (positionBankLoanReleaseForm != null)
			{
				positionBankLoanReleaseForm.Close();
			}

			mainForm.positionBankLoanForm = null;
		}

		private void BookGroupCombo_RowChange(object sender, System.EventArgs e)
		{
			mainForm.GridFilterClear(ref BanksGrid);
			mainForm.GridFilterClear(ref LoansGrid);
			mainForm.GridFilterClear(ref ActivityGrid);
			
			if (!(BookGroupCombo.Text.Equals("")))
			{		  												
				try
				{
					if (bool.Parse(mainForm.ServiceAgent.KeyValueGet("BankLoanDtcReset" + BookGroupCombo.Text, "False")))
					{
						BankLoanResetLabel.ForeColor = System.Drawing.Color.Navy;
						BankLoanResetLabel.Text = "BankLoan DTC reset for " + BookGroupCombo.Text + " occured for : " + mainForm.ServiceAgent.KeyValueGet("BankLoanBizDate" + BookGroupCombo.Text, "2000-01-01");
					}
					else
					{
						BankLoanResetLabel.ForeColor = System.Drawing.Color.Maroon;
						BankLoanResetLabel.Text = "BankLoan DTC reset for " + BookGroupCombo.Text + " did not occur for : " + mainForm.ServiceAgent.KeyValueGet("BizDateBank", "2000-01-01");
					}
											
					if (bool.Parse(dataSet.Tables["BookGroups"].Rows[BookGroupCombo.WillChangeToIndex]["MayView"].ToString()))
					{  						  			  									
						BookGroupNameLabel.DataSource = dataSet.Tables["BookGroups"];
						BookGroupNameLabel.DataField = "BookName"; 												

						bankDataViewRowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
						bankDataView.RowFilter = bankDataViewRowFilter;

						bankLoansDataViewRowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND Book = '" + BanksGrid.Columns["Book"].Text + "'";
						bankLoansDataView.RowFilter = bankLoansDataViewRowFilter;

						bankLoanActiivtyDataViewRowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND LoanDate = '" + LoansGrid.Columns["LoanDate"].Value.ToString() + "'";
						bankLoanActivityDataView.RowFilter = bankLoanActiivtyDataViewRowFilter;
						
						if (bool.Parse(dataSet.Tables["BookGroups"].Rows[BookGroupCombo.WillChangeToIndex]["MayEdit"].ToString()))
						{				
							BanksGrid.AllowAddNew = true;
							BanksGrid.AllowUpdate = true;

							PledgeMenuItem.Enabled = true;
							PledgeInputMenuItem.Enabled = true;
							ReleaseMenuItem.Enabled = true;

							GenerateLoanDocumentMenuItem.Enabled = true;
							GenerateFailedPledgesDocumentMenuItem.Enabled = true;		
							ViewAllActivityMenuItem.Enabled = true;
							ViewReleaseReportMenuItem.Enabled = true;

							if (bankDataView.Table.Rows.Count > 0)
							{
								LoansGrid.AllowAddNew = true;
								LoansGrid.AllowUpdate = true;
							}
							else
							{						
								LoansGrid.AllowAddNew = false;
								LoansGrid.AllowUpdate = false;
							}				
						}
						else
						{
							mainForm.Alert("User: " + mainForm.UserId + ", Permission to EDIT denied.");
							
							BanksGrid.AllowAddNew = false;
							BanksGrid.AllowUpdate = false;

							PledgeMenuItem.Enabled = false;
							PledgeInputMenuItem.Enabled = false;
							ReleaseMenuItem.Enabled = false;

							GenerateLoanDocumentMenuItem.Enabled = false;
							GenerateFailedPledgesDocumentMenuItem.Enabled = false;
							ViewAllActivityMenuItem.Enabled = false;
							ViewReleaseReportMenuItem.Enabled = false;
						}		  
					}
					else
					{	
						mainForm.Alert("User: " + mainForm.UserId + ", Permission to VIEW denied.");
				
						bankDataViewRowFilter = "BookGroup = ''";
						bankDataView.RowFilter = bankDataViewRowFilter;

						bankLoansDataViewRowFilter = "BookGroup = ''";
						bankLoansDataView.RowFilter = bankLoansDataViewRowFilter;

						bankLoanActiivtyDataViewRowFilter = "BookGroup = ''";
						bankLoanActivityDataView.RowFilter = bankLoanActiivtyDataViewRowFilter;
					}
				}		
				catch (Exception error)
				{
					Log.Write(error.Message + ". [PositionBankLoanForm.BookGroupCombo_RowChange]", Log.Error, 1); 				
					mainForm.Alert(error.Message, PilotState.RunFault);
				}
			}
		}

		private void BanksGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				mainForm.PositionAgent.BankSet(
					BanksGrid.Columns["BookGroup"].Text,
					BanksGrid.Columns["Book"].Text,
					BanksGrid.Columns["Name"].Text,
					BanksGrid.Columns["Contact"].Text,
					BanksGrid.Columns["Phone"].Text,
					BanksGrid.Columns["Fax"].Text,
					mainForm.UserId,
					true);

				BanksGrid.Columns["ActUserShortName"].Text = mainForm.UserId;
				BanksGrid.Columns["ActTime"].Text = DateTime.Now.ToString(Standard.DateTimeShortFormat);
				
				LoansGrid.AllowAddNew = true;
				LoansGrid.AllowUpdate = true;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanForm.BanksGrid_BeforeUpdate]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
				e.Cancel = true;
				return;
			}
		}

		private void BanksGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				mainForm.PositionAgent.BankSet(
					BanksGrid.Columns["BookGroup"].Text,
					BanksGrid.Columns["Book"].Text,
					BanksGrid.Columns["Name"].Text,
					BanksGrid.Columns["Contact"].Text,
					BanksGrid.Columns["Phone"].Text,
					BanksGrid.Columns["Fax"].Text,
					mainForm.UserId,
					false);				
							
				if (BanksGrid.Splits[0].Rows.Count == 2)
				{
					LoansGrid.AllowAddNew = false;
					LoansGrid.AllowUpdate = false;
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanForm.BanksGrid_BeforeDelete]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
				e.Cancel = true;
				return;
			}
		}

		private void FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch(e.Column.DataField)
			{
				case "LoanDate":						
					e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateFormat);
					break;

				case "ActTime":
					e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeShortFormat);
					break;
					
				case "Quantity":
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch {}
					break;							

				case "PriceMin":
				case "Amount":
				case "LoanAmount":
				case "LoanAmountCOB":
				case "PledgeAmountRequested":
				case "PledgedAmount":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.00");
					}
					catch {}
					break;
			}
		}

		private void LoansGrid_OnAddNew(object sender, System.EventArgs e)
		{
			if (BanksGrid.Splits[0].Rows.Count > 0)
			{
				LoansGrid.Columns["BookGroup"].Text = BookGroupCombo.Text;
				LoansGrid.Columns["Book"].Text = BanksGrid.Columns["Book"].Text;
				LoansGrid.Columns["LoanDate"].Text = mainForm.ServiceAgent.BizDateBank();
				LoansGrid.Columns["PledgeAmountRequested"].Value = "0";			
				LoansGrid.Columns["PledgedAmount"].Value = "0";
				LoansGrid.Columns["PreviousLoanAmount"].Value = "0";
				LoansGrid.Columns["LoanAmountCOB"].Value = 0;
			}
			else
			{
				mainForm.Alert("Please enter at least one bank.", PilotState.RunFault);
				return;
			}
		}

		private void LoansGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{					
				if (LoansGrid.Columns["Book"].Text.Equals(""))
				{
					mainForm.Alert("Book missing", PilotState.RunFault);
					e.Cancel = true;
					LoansGrid.Col = 1;
					return;
				}
				else if (LoansGrid.Columns["LoanDate"].Text.Equals(""))
				{
					mainForm.Alert("Loan Date missing", PilotState.RunFault);
					e.Cancel = true;
					LoansGrid.Col = 0;
					return;
				}
				else if (LoansGrid.Columns["LoanType"].Text.Equals(""))
				{
					mainForm.Alert("Loan Type missing", PilotState.RunFault);
					e.Cancel = true;
					LoansGrid.Col = 2;
					return;
				}
				else if (LoansGrid.Columns["ActivityType"].Text.Equals(""))
				{
					mainForm.Alert("Activity Type missing", PilotState.RunFault);
					e.Cancel = true;
					LoansGrid.Col = 3;
					return;
				}
				else if (LoansGrid.Columns["HairCut"].Value.ToString().Equals(""))
				{
					mainForm.Alert("HairCut missing", PilotState.RunFault);
					e.Cancel = true;
					LoansGrid.Col = 4;
					return;
				}
				else if (LoansGrid.Columns["PriceMin"].Value.ToString().Equals(""))
				{
					mainForm.Alert("Price min missing", PilotState.RunFault);
					e.Cancel = true;
					LoansGrid.Col = 7;
					return;
				}
				else if (LoansGrid.Columns["LoanAmount"].Value.ToString().Equals(""))
				{
					mainForm.Alert("Loan amount missing", PilotState.RunFault);					
					e.Cancel = true;
					LoansGrid.Col = 9;
					return;
				}
							
				mainForm.PositionAgent.BankLoanSet(
					LoansGrid.Columns["BookGroup"].Text,
					LoansGrid.Columns["Book"].Text,
					LoansGrid.Columns["LoanDate"].Text,
					LoansGrid.Columns["LoanType"].Text,
					LoansGrid.Columns["ActivityType"].Text,
					LoansGrid.Columns["HairCut"].Value.ToString(),
					LoansGrid.Columns["SpMin"].Text,
					LoansGrid.Columns["MoodyMin"].Text,
					LoansGrid.Columns["PriceMin"].Value.ToString(),
					LoansGrid.Columns["LoanAmount"].Value.ToString(),
					LoansGrid.Columns["Comment"].Text,
					mainForm.UserId,
					true);

				LoansGrid.Columns["ActUserShortName"].Text = mainForm.UserId;
				LoansGrid.Columns["ActTime"].Text = DateTime.Now.ToString(Standard.DateTimeShortFormat);

				mainForm.Alert("Updated a Loan for: " + LoansGrid.Columns["LoanDate"].Text + " with " + LoansGrid.Columns["Book"].Text, PilotState.Idle);				
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanForm.LoansGrid_BeforeUpdate]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
				e.Cancel = true;
				return;
			}
		}

		private void LoansGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{				
				mainForm.PositionAgent.BankLoanSet(
					BookGroupCombo.Text,
					LoansGrid.Columns["Book"].Text,
					LoansGrid.Columns["LoanDate"].Text,
					LoansGrid.Columns["LoanType"].Text,
					LoansGrid.Columns["ActivityType"].Text,
					LoansGrid.Columns["HairCut"].Value.ToString(),
					LoansGrid.Columns["SpMin"].Text,
					LoansGrid.Columns["MoodyMin"].Text,
					LoansGrid.Columns["PriceMin"].Value.ToString(),
					LoansGrid.Columns["LoanAmount"].Value.ToString(),
					LoansGrid.Columns["Comment"].Text,
					mainForm.UserId,
					false);				
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanForm.LoansGrid_BeforeDelete]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
				e.Cancel = true;
				return;
			}
		}

		private void BanksGrid_OnAddNew(object sender, System.EventArgs e)
		{
			BanksGrid.Columns["BookGroup"].Text = BookGroupCombo.Text;
		}
	
		private void LoanTypeDropdown_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			LoansGrid.Columns["LoanType"].Text = LoanTypeDropdown.Columns["LoanType"].Text;	
		}
		
		private void ActivityTypeDropDown_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			LoansGrid.Columns["ActivityType"].Text = ActivityTypeDropDown.Columns["ActivityType"].Text;
		}
		
		private void PledgeMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (!LoansGrid.Columns["LoanType"].Text.Equals("*") || !LoansGrid.Columns["ActivityType"].Text.Equals("*"))
				{
				
					positionBankLoanPledgeForm = new PositionBankLoanPledgeForm(mainForm,
						LoansGrid.Columns["BookGroup"].Text, 
						LoansGrid.Columns["Book"].Text, 
						LoansGrid.Columns["LoanDate"].Text, 
						LoansGrid.Columns["LoanType"].Text,
						LoansGrid.Columns["ActivityType"].Text,
						LoansGrid.Columns["PriceMin"].Value.ToString(),
						LoansGrid.Columns["SpMin"].Text,
						LoansGrid.Columns["MoodyMin"].Text);
				
					positionBankLoanPledgeForm.MdiParent = mainForm;
					positionBankLoanPledgeForm.Show();
				}
				else
				{
					mainForm.Alert("Invalid LoanType: " + LoansGrid.Columns["LoanType"].Text + "and Invalid ActivityType: " + LoansGrid.Columns["ActivityType"].Text);
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanForm.PledgeMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);	
			}
		}

		private void ReleaseMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				positionBankLoanReleaseForm = new PositionBankLoanReleaseForm(
					mainForm, 
					LoansGrid.Columns["BookGroup"].Text,
					LoansGrid.Columns["Book"].Text,
					LoansGrid.Columns["LoanDate"].Text,
					LoansGrid.Columns["LoanType"].Text,
					LoansGrid.Columns["ActivityType"].Text);

				positionBankLoanReleaseForm.MdiParent = mainForm;
				positionBankLoanReleaseForm.Show();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanForm.ReleaseMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);	
			}
		}

		private void GenerateMovementMenuItem_Click(object sender, System.EventArgs e)
		{
			StreamWriter sr = null;

			try
			{
				string fileName = 	tempPath + BookGroupCombo.Text + "_" + mainForm.ServiceAgent.BizDate() + ".txt";

				PositionBankLoanDocuments document = new PositionBankLoanDocuments(mainForm);
																
				sr = File.CreateText(fileName);
				sr.Write(document.CreateMovementDocument(BookGroupCombo.Text));				

				mainForm.Alert("Wrote movement file to : " + fileName, PilotState.RunFault);
			
				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.EnableRaisingEvents=false;
				proc.StartInfo.FileName="notepad";
				proc.StartInfo.Arguments= fileName;
				proc.Start();				
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionBankLoanForm.GenerateMovementMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
			finally
			{
				if (sr != null)
				{
					sr.Close();
				}
			}
		}

		private void LoansGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)13) && LoansGrid.DataChanged)
			{
		    LoansGrid.UpdateData();
				e.Handled = true;
			}
			else
			{
				e.Handled = false;
			}
		}

		private void ActivityGrid_FilterChange(object sender, System.EventArgs e)
		{
			string gridFilter;

			try
			{
				gridFilter = mainForm.GridFilterGet(ref ActivityGrid);

				if (gridFilter.Equals(""))
				{
					bankLoanActivityDataView.RowFilter = bankLoanActiivtyDataViewRowFilter;
				}
				else
				{
					bankLoanActivityDataView.RowFilter = bankLoanActiivtyDataViewRowFilter + " AND " + gridFilter;
				}				
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void ActivityGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{		
			if (ActivityGrid.Columns["Flag"].CellText(e.Row).Equals("P"))
			{
				e.CellStyle.BackColor = System.Drawing.Color.SkyBlue;
			}
			else if (ActivityGrid.Columns["Status"].CellText(e.Row).Equals("PF") || ActivityGrid.Columns["Status"].CellText(e.Row).Equals("RF"))
			{
				e.CellStyle.BackColor = System.Drawing.Color.Thistle;		
			}			
			else if (ActivityGrid.Columns["Book"].CellText(e.Row).Equals("0000"))
			{
				e.CellStyle.BackColor = System.Drawing.Color.LightGray;
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

		private void GenerateLoanDocumentMenuItem_Click(object sender, System.EventArgs e)
		{
			StreamWriter sr = null;

			try
			{				
				string fileName = tempPath + BookGroupCombo.Text + "_" + mainForm.ServiceAgent.BizDate() + "_" + BanksGrid.Columns["Book"].Text + ".txt";
				
				PositionBankLoanDocuments document = new PositionBankLoanDocuments(mainForm);
				
				sr = File.CreateText(fileName);
				sr.Write(document.CreateLoanAdjustmentDocument(
					BookGroupCombo.Text,
					BanksGrid.Columns["Book"].Text,
					BanksGrid.Columns["Name"].Text,
					BanksGrid.Columns["Contact"].Text,
					BanksGrid.Columns["Fax"].Text));				
			
				mainForm.Alert("Wrote loan document file to : " + fileName, PilotState.RunFault);

				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.EnableRaisingEvents=false;
				proc.StartInfo.FileName="notepad";
				proc.StartInfo.Arguments= fileName;
				proc.Start();		
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionBankLoanForm.GenerateLoanDocumentMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
			finally
			{
				if (sr != null)
				{
					sr.Close();
				}
			}
		}

		private void BanksGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)13) && BanksGrid.DataChanged)
			{
				BanksGrid.UpdateData();
				e.Handled = true;
			}
			else
			{
				e.Handled = false;
			}
		}

		private void BanksGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{					
			try
			{
				if (!bank.Equals(BanksGrid.Columns["Book"].Text) && !BanksGrid.Columns["Book"].Text.Equals(""))
				{
					bank = BanksGrid.Columns["Book"].Text;

					bankLoansDataViewRowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND Book = '" + BanksGrid.Columns["Book"].Text + "'";
					bankLoansDataView.RowFilter = bankLoansDataViewRowFilter;									
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanForm.BanksGrid_Paint]", Log.Error, 1); 				
				mainForm.Alert(error.Message, PilotState.Idle);
			}
		}

		private void LoansGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{				
			try
			{
				if (!loanDate.Equals(LoansGrid.Columns["LoanDate"].Text))
				{
					mainForm.GridFilterClear(ref ActivityGrid);

					loanDate = LoansGrid.Columns["LoanDate"].Text;

					bankLoanActiivtyDataViewRowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND LoanDate = '" + LoansGrid.Columns["LoanDate"].Value.ToString() + "'";
					bankLoanActivityDataView.RowFilter = bankLoanActiivtyDataViewRowFilter;									
					
					if (LoansGrid.Columns["LoanType"].Text.Equals("U"))
					{
						PledgeMenuItem.Enabled = false;
						ReleaseMenuItem.Enabled = false;
					}
					else
					{
						PledgeMenuItem.Enabled = true;
						ReleaseMenuItem.Enabled = true;
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanForm.LoansGrid_Paint]", Log.Error, 1); 				
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void GenerateFailedPledgesDocumentMenuItem_Click(object sender, System.EventArgs e)
		{
			StreamWriter sr = null;

			try
			{				
				string fileName = tempPath + BookGroupCombo.Text + "_" + mainForm.ServiceAgent.BizDate() + "_" + BanksGrid.Columns["Book"].Text + ".txt";
				
				PositionBankLoanDocuments document = new PositionBankLoanDocuments(mainForm);
				
				sr = File.CreateText(fileName);
				sr.Write(document.CreateFailedPledgesDocument(BookGroupCombo.Text));				
			
				mainForm.Alert("Wrote loan document file to : " + fileName, PilotState.RunFault);

				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.EnableRaisingEvents=false;
				proc.StartInfo.FileName="notepad";
				proc.StartInfo.Arguments= fileName;
				proc.Start();		
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionBankLoanForm.GenerateFailedPledgesDocumentMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
			finally
			{
				if (sr != null)
				{
					sr.Close();
				}
			}
		}

		private void ActivityGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{
				if (!ActivityGrid.Columns["SecId"].Text.Equals(secId))
				{
					secId = ActivityGrid.Columns["SecId"].Text;

					this.Cursor = Cursors.WaitCursor;                
     
					mainForm.SecId = secId;

					this.Cursor = Cursors.Default;
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanForm.ActivityGrid_Paint]", Log.Error, 1); 				
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void ViewAllActivityMenuItem_Click(object sender, System.EventArgs e)
		{
			ViewAllActivityMenuItem.Checked = !ViewAllActivityMenuItem.Checked;
			
			if (ViewAllActivityMenuItem.Checked)
			{
				bankLoanActivityDataView.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
				
				ActivityGrid.Splits[0, 0].DisplayColumns["Book"].Visible = true;
				ActivityGrid.Splits[0, 0].DisplayColumns["LoanDate"].Visible = true;
			}
			else
			{
				bankLoanActivityDataView.RowFilter = bankLoanActiivtyDataViewRowFilter;
				
				ActivityGrid.Splits[0, 0].DisplayColumns["Book"].Visible = false;
				ActivityGrid.Splits[0, 0].DisplayColumns["LoanDate"].Visible = false;				
			}
		}				

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			
			Excel excel = new Excel();
			excel.ExportGridToExcel(ref ActivityGrid);
		
			if (ActivityGrid.SelectedRows.Count > 0)
			{
				foreach( int rowIndex in ActivityGrid.SelectedRows)
				{					
					if (!ActivityGrid.Columns["Flag"].CellText(rowIndex).Equals("P"))
					{
						mainForm.PositionAgent.BankLoanPledgeSet(BookGroupCombo.Text,
							"",
							"",
							ActivityGrid.Columns["ProcessId"].CellText(rowIndex),
							"",
							"",
							"",
							"",
							"P",
							"",
							"");
					}
				}
			}
			else
			{
				for (int rowIndex = 0; rowIndex < ActivityGrid.Splits[0].Rows.Count; rowIndex++)
				{
					if (!ActivityGrid.Columns["Flag"].CellText(rowIndex).Equals("P"))
					{
						mainForm.PositionAgent.BankLoanPledgeSet(BookGroupCombo.Text,
							"",
							"",
							ActivityGrid.Columns["ProcessId"].CellText(rowIndex),
							"",
							"",
							"",
							"",
							"P",
							"",
							"");
					}			
				}
			}

			this.Cursor = Cursors.Default;
		}

		private void SendToMailRecipientMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (ActivityGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
				return;
			}

			try
			{
				maxTextLength = new int[ActivityGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
				{					
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in ActivityGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in ActivityGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
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

				mainForm.Alert("Total: " + ActivityGrid.SelectedRows.Count + " items added to clipboard.");
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionBankLoanForm.SendToMailRecipientMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (ActivityGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
				return;
			}

			try
			{
				maxTextLength = new int[ActivityGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
				{					
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in ActivityGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in ActivityGrid.SelectedRows)
				{
					columnIndex = -1;				

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
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

				mainForm.Alert("Total: " + ActivityGrid.SelectedRows.Count + " items added to clipboard.");
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionBankLoanForm.SendToClipboardMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void PledgeInputMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				PositionBankLoanPledgeInputForm positionBankLoanPledgeInputForm = new PositionBankLoanPledgeInputForm(mainForm);			
				positionBankLoanPledgeInputForm.MdiParent = mainForm;
				positionBankLoanPledgeInputForm.Show();
			}
			catch (Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
			}
		}

		private void ViewReleaseReportMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				PositionBankLoanReleaseReportsDataForm positionBankLoanReleaseReportForm = new PositionBankLoanReleaseReportsDataForm(mainForm, BookGroupCombo.Text);			
				positionBankLoanReleaseReportForm.MdiParent = mainForm;
				positionBankLoanReleaseReportForm.Show();
			}
			catch (Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
			}
		}
	}
}
