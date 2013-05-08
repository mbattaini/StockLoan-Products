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
	public class PositionBankLoanReleaseForm : System.Windows.Forms.Form
	{				
		private const string TEXT = "Position - BankLoan Release";
		private string bankReleasesDataViewRowFilter = "";
		private string bookGroup = "";
		private string book = "";
		private string loanDate = "";
		private string loanType = "";		
		private string activityType = "";
		private string secId = "";
		
		private MainForm mainForm;
				
		private DataSet dataSet;		

		private DataView bankReleasesDataView = null;		
		
		private ArrayList releasesArray;
		
		private System.Windows.Forms.Panel TopPanel;
		private System.Windows.Forms.ContextMenu MainContextMenu;				
		private System.Windows.Forms.MenuItem DockMenuItem;
		private System.Windows.Forms.MenuItem DockTopMenuItem;
		private System.Windows.Forms.MenuItem DockBottomMenuItem;
		private System.Windows.Forms.MenuItem DockNoneMenuItem;
		private System.Windows.Forms.MenuItem SeperatorMenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.ComponentModel.Container components = null;
		private C1.Win.C1Input.C1Label StatusLabel;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ReleaseGrid;
		private System.Windows.Forms.MenuItem ReleaseMenuItem;
		private System.Windows.Forms.MenuItem DockSeperatorMenuItem;
		private System.Windows.Forms.MenuItem RefreshMenuItem;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToMailRecipientMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private C1.Win.C1Input.C1Label AmountLabel;
					
		private delegate void SendReleasesDelegate(ArrayList releasesArray);  

		public PositionBankLoanReleaseForm(MainForm mainForm, string bookGroup, string book, string loanDate, string loanType, string activityType)
		{
			try
			{
				this.mainForm	= mainForm;
				this.bookGroup	= bookGroup;
				this.book = book;
				this.loanDate = loanDate;
				this.loanType = loanType;
				this.activityType = activityType;

				releasesArray	= new ArrayList();

				InitializeComponent();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanReleaseForm.PositionBankLoanReleaseForm]", Log.Error, 1); 				
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionBankLoanReleaseForm));
			this.TopPanel = new System.Windows.Forms.Panel();
			this.ReleaseGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.ReleaseMenuItem = new System.Windows.Forms.MenuItem();
			this.RefreshMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToMailRecipientMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.DockMenuItem = new System.Windows.Forms.MenuItem();
			this.DockTopMenuItem = new System.Windows.Forms.MenuItem();
			this.DockBottomMenuItem = new System.Windows.Forms.MenuItem();
			this.DockSeperatorMenuItem = new System.Windows.Forms.MenuItem();
			this.DockNoneMenuItem = new System.Windows.Forms.MenuItem();
			this.SeperatorMenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.AmountLabel = new C1.Win.C1Input.C1Label();
			this.TopPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ReleaseGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.AmountLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// TopPanel
			// 
			this.TopPanel.Controls.Add(this.ReleaseGrid);
			this.TopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TopPanel.Location = new System.Drawing.Point(0, 0);
			this.TopPanel.Name = "TopPanel";
			this.TopPanel.Size = new System.Drawing.Size(1232, 321);
			this.TopPanel.TabIndex = 1;
			// 
			// ReleaseGrid
			// 
			this.ReleaseGrid.AllowColSelect = false;
			this.ReleaseGrid.AllowFilter = false;
			this.ReleaseGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.ReleaseGrid.AllowUpdateOnBlur = false;
			this.ReleaseGrid.Caption = "Available To Release";
			this.ReleaseGrid.CaptionHeight = 17;
			this.ReleaseGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.ReleaseGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ReleaseGrid.EmptyRows = true;
			this.ReleaseGrid.ExtendRightColumn = true;
			this.ReleaseGrid.FilterBar = true;
			this.ReleaseGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ReleaseGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.ReleaseGrid.Location = new System.Drawing.Point(0, 0);
			this.ReleaseGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.ReleaseGrid.Name = "ReleaseGrid";
			this.ReleaseGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ReleaseGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ReleaseGrid.PreviewInfo.ZoomFactor = 75;
			this.ReleaseGrid.RecordSelectorWidth = 16;
			this.ReleaseGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.ReleaseGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ReleaseGrid.RowHeight = 15;
			this.ReleaseGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
			this.ReleaseGrid.Size = new System.Drawing.Size(1232, 321);
			this.ReleaseGrid.TabIndex = 1;
			this.ReleaseGrid.Text = "c1TrueDBGrid2";
			this.ReleaseGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.ReleaseGrid_Paint);
			this.ReleaseGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.ReleaseGrid_RowColChange);
			this.ReleaseGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ReleaseGrid_SelChange);
			this.ReleaseGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.ReleaseGrid_FetchRowStyle);
			this.ReleaseGrid.FilterChange += new System.EventHandler(this.ReleaseGrid_FilterChange);
			this.ReleaseGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ReleaseGrid_FormatText);
			this.ReleaseGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ReleaseGrid_KeyPress);
			this.ReleaseGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
				"\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
				"l=\"0\" Caption=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"On Pledge\" DataField=\"Quantity\"><ValueItems" +
				" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Loan Type\" DataF" +
				"ield=\"LoanType\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
				"\" Caption=\"Activity Type\" DataField=\"ActivityType\"><ValueItems /><GroupInfo /></" +
				"C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Loan Date\" DataField=\"LoanDate\" Nu" +
				"mberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
				"olumn Level=\"0\" Caption=\"Amount\" DataField=\"Amount\" NumberFormat=\"FormatText Eve" +
				"nt\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"B" +
				"ook\" DataField=\"Book\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"BookGroup\" DataField=\"BookGroup\"><ValueItems /><GroupInfo /></C" +
				"1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\">" +
				"<Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style199{Font:Ve" +
				"rdana, 8.25pt;AlignHorz:Near;ForeColor:Black;BackColor:WhiteSmoke;}Style198{Font" +
				":Verdana, 8.25pt;AlignHorz:Near;}Inactive{ForeColor:InactiveCaptionText;BackColo" +
				"r:InactiveCaption;}Style203{}Style202{}Style201{}Style200{}Selected{ForeColor:Hi" +
				"ghlightText;BackColor:Highlight;}Editor{}Style184{}Style185{}Style186{}Style187{" +
				"}Style180{}Style181{}Style182{}Style183{}Style188{}Style189{AlignHorz:Near;}Filt" +
				"erBar{BackColor:SeaShell;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1" +
				", 1, 1;ForeColor:ControlText;BackColor:Control;}Style18{}Style19{AlignHorz:Near;" +
				"}Style14{AlignHorz:Near;}Style15{}Style16{}Style17{}Style13{AlignHorz:Near;}Styl" +
				"e27{}Style29{}Style28{}Style26{AlignHorz:Near;}Style25{AlignHorz:Near;}Style5{}S" +
				"tyle4{}Style24{}Style23{}Style1{AlignHorz:Near;}Style22{}Style3{}Style2{AlignHor" +
				"z:Far;}Style21{}Style20{AlignHorz:Near;}OddRow{}Style38{}Style39{}Style36{AlignH" +
				"orz:Far;}Style37{}Style34{}Style35{}Style32{}Style33{}Style30{}Style31{AlignHorz" +
				":Near;}Style354{}Normal{Font:Verdana, 8.25pt;}Style41{AlignHorz:Near;}Style40{}S" +
				"tyle43{}Style42{AlignHorz:Near;}Style45{}Style44{}Style46{}EvenRow{BackColor:Aqu" +
				"a;}Style6{}Style58{}RecordSelector{AlignImage:Center;}Footer{}Style53{AlignHorz:" +
				"Near;}Style54{AlignHorz:Near;}Style55{}Style56{}Style57{}Caption{AlignHorz:Cente" +
				"r;}Style195{}Style194{}Style197{}Style196{}Style191{}Style190{}Style193{Font:Ver" +
				"dana, 8.25pt;AlignHorz:Near;ForeColor:Black;BackColor:WhiteSmoke;}Style192{Font:" +
				"Verdana, 8.25pt;AlignHorz:Near;}Group{BackColor:ControlDark;Border:None,,0, 0, 0" +
				", 0;AlignVert:Center;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBa" +
				"rStyle=\"None\" VBarStyle=\"Always\" AllowColSelect=\"False\" Name=\"Split[0,0]\" AllowR" +
				"owSizing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"" +
				"17\" ExtendRightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedRowBorder\" Rec" +
				"ordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScro" +
				"llGroup=\"2\"><CaptionStyle parent=\"Heading\" me=\"Style189\" /><EditorStyle parent=\"" +
				"Editor\" me=\"Style181\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style187\" /><FilterBa" +
				"rStyle parent=\"FilterBar\" me=\"Style354\" /><FooterStyle parent=\"Footer\" me=\"Style" +
				"183\" /><GroupStyle parent=\"Group\" me=\"Style191\" /><HeadingStyle parent=\"Heading\"" +
				" me=\"Style182\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style186\" /><Inact" +
				"iveStyle parent=\"Inactive\" me=\"Style185\" /><OddRowStyle parent=\"OddRow\" me=\"Styl" +
				"e188\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style190\" /><SelectedSt" +
				"yle parent=\"Selected\" me=\"Style184\" /><Style parent=\"Normal\" me=\"Style180\" /><in" +
				"ternalCols><C1DisplayColumn><HeadingStyle parent=\"Style182\" me=\"Style53\" /><Styl" +
				"e parent=\"Style180\" me=\"Style54\" /><FooterStyle parent=\"Style183\" me=\"Style55\" /" +
				"><EditorStyle parent=\"Style181\" me=\"Style56\" /><GroupHeaderStyle parent=\"Style18" +
				"0\" me=\"Style58\" /><GroupFooterStyle parent=\"Style180\" me=\"Style57\" /><ColumnDivi" +
				"der>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>8</DCIdx></C1Displa" +
				"yColumn><C1DisplayColumn><HeadingStyle parent=\"Style182\" me=\"Style25\" /><Style p" +
				"arent=\"Style180\" me=\"Style26\" /><FooterStyle parent=\"Style183\" me=\"Style27\" /><E" +
				"ditorStyle parent=\"Style181\" me=\"Style28\" /><GroupHeaderStyle parent=\"Style180\" " +
				"me=\"Style30\" /><GroupFooterStyle parent=\"Style180\" me=\"Style29\" /><Visible>True<" +
				"/Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><Locke" +
				"d>True</Locked><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
				"parent=\"Style182\" me=\"Style41\" /><Style parent=\"Style180\" me=\"Style42\" /><Footer" +
				"Style parent=\"Style183\" me=\"Style43\" /><EditorStyle parent=\"Style181\" me=\"Style4" +
				"4\" /><GroupHeaderStyle parent=\"Style180\" me=\"Style46\" /><GroupFooterStyle parent" +
				"=\"Style180\" me=\"Style45\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single" +
				"</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx>7</DCIdx></C1Disp" +
				"layColumn><C1DisplayColumn><HeadingStyle parent=\"Style182\" me=\"Style13\" /><Style" +
				" parent=\"Style180\" me=\"Style14\" /><FooterStyle parent=\"Style183\" me=\"Style15\" />" +
				"<EditorStyle parent=\"Style181\" me=\"Style16\" /><GroupHeaderStyle parent=\"Style180" +
				"\" me=\"Style18\" /><GroupFooterStyle parent=\"Style180\" me=\"Style17\" /><ColumnDivid" +
				"er>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>3</DCIdx></C1Display" +
				"Column><C1DisplayColumn><HeadingStyle parent=\"Style182\" me=\"Style19\" /><Style pa" +
				"rent=\"Style180\" me=\"Style20\" /><FooterStyle parent=\"Style183\" me=\"Style21\" /><Ed" +
				"itorStyle parent=\"Style181\" me=\"Style22\" /><GroupHeaderStyle parent=\"Style180\" m" +
				"e=\"Style24\" /><GroupFooterStyle parent=\"Style180\" me=\"Style23\" /><ColumnDivider>" +
				"DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>4</DCIdx></C1DisplayCol" +
				"umn><C1DisplayColumn><HeadingStyle parent=\"Style182\" me=\"Style192\" /><Style pare" +
				"nt=\"Style180\" me=\"Style193\" /><FooterStyle parent=\"Style183\" me=\"Style194\" /><Ed" +
				"itorStyle parent=\"Style181\" me=\"Style195\" /><GroupHeaderStyle parent=\"Style180\" " +
				"me=\"Style197\" /><GroupFooterStyle parent=\"Style180\" me=\"Style196\" /><Visible>Tru" +
				"e</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>95</Width><Heigh" +
				"t>15</Height><Locked>True</Locked><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayCo" +
				"lumn><HeadingStyle parent=\"Style182\" me=\"Style198\" /><Style parent=\"Style180\" me" +
				"=\"Style199\" /><FooterStyle parent=\"Style183\" me=\"Style200\" /><EditorStyle parent" +
				"=\"Style181\" me=\"Style201\" /><GroupHeaderStyle parent=\"Style180\" me=\"Style203\" />" +
				"<GroupFooterStyle parent=\"Style180\" me=\"Style202\" /><Visible>True</Visible><Colu" +
				"mnDivider>DarkGray,Single</ColumnDivider><Width>75</Width><Height>15</Height><Lo" +
				"cked>True</Locked><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style182\" me=\"Style1\" /><Style parent=\"Style180\" me=\"Style2\" /><Foote" +
				"rStyle parent=\"Style183\" me=\"Style3\" /><EditorStyle parent=\"Style181\" me=\"Style4" +
				"\" /><GroupHeaderStyle parent=\"Style180\" me=\"Style6\" /><GroupFooterStyle parent=\"" +
				"Style180\" me=\"Style5\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</C" +
				"olumnDivider><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColu" +
				"mn><HeadingStyle parent=\"Style182\" me=\"Style31\" /><Style parent=\"Style180\" me=\"S" +
				"tyle36\" /><FooterStyle parent=\"Style183\" me=\"Style37\" /><EditorStyle parent=\"Sty" +
				"le181\" me=\"Style38\" /><GroupHeaderStyle parent=\"Style180\" me=\"Style40\" /><GroupF" +
				"ooterStyle parent=\"Style180\" me=\"Style39\" /><Visible>True</Visible><ColumnDivide" +
				"r>DarkGray,Single</ColumnDivider><Width>120</Width><Height>15</Height><Locked>Tr" +
				"ue</Locked><DCIdx>6</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 17, 1" +
				"228, 300</ClientRect><BorderSide>Right</BorderSide></C1.Win.C1TrueDBGrid.MergeVi" +
				"ew></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" m" +
				"e=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"" +
				"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Se" +
				"lected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"Highli" +
				"ghtRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRo" +
				"w\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"Fi" +
				"lterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</ver" +
				"tSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>1" +
				"6</DefaultRecSelWidth><ClientArea>0, 0, 1228, 317</ClientArea><PrintPageHeaderSt" +
				"yle parent=\"\" me=\"Style34\" /><PrintPageFooterStyle parent=\"\" me=\"Style35\" /></Bl" +
				"ob>";
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.ReleaseMenuItem,
																																										this.RefreshMenuItem,
																																										this.SendToMenuItem,
																																										this.DockMenuItem,
																																										this.SeperatorMenuItem,
																																										this.ExitMenuItem});
			// 
			// ReleaseMenuItem
			// 
			this.ReleaseMenuItem.Index = 0;
			this.ReleaseMenuItem.Text = "Release";
			this.ReleaseMenuItem.Click += new System.EventHandler(this.ReleaseMenuItem_Click);
			// 
			// RefreshMenuItem
			// 
			this.RefreshMenuItem.Index = 1;
			this.RefreshMenuItem.Text = "Refresh";
			this.RefreshMenuItem.Click += new System.EventHandler(this.RefreshMenuItem_Click);
			// 
			// SendToMenuItem
			// 
			this.SendToMenuItem.Index = 2;
			this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.SendToClipboardMenuItem,
																																									 this.SendToExcelMenuItem,
																																									 this.SendToMailRecipientMenuItem});
			this.SendToMenuItem.Text = "Send To";
			// 
			// SendToMailRecipientMenuItem
			// 
			this.SendToMailRecipientMenuItem.Index = 2;
			this.SendToMailRecipientMenuItem.Text = "Mail Recipient";
			this.SendToMailRecipientMenuItem.Click += new System.EventHandler(this.SendToMailRecipientMenuItem_Click);
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
			// DockMenuItem
			// 
			this.DockMenuItem.Index = 3;
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
			this.SeperatorMenuItem.Index = 4;
			this.SeperatorMenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 5;
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
			this.AmountLabel.Location = new System.Drawing.Point(752, 328);
			this.AmountLabel.Name = "AmountLabel";
			this.AmountLabel.Size = new System.Drawing.Size(472, 16);
			this.AmountLabel.TabIndex = 34;
			this.AmountLabel.Tag = null;
			this.AmountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.AmountLabel.TextDetached = true;
			// 
			// PositionBankLoanReleaseForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1232, 341);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.AmountLabel);
			this.Controls.Add(this.TopPanel);
			this.Controls.Add(this.StatusLabel);
			this.DockPadding.Bottom = 20;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PositionBankLoanReleaseForm";
			this.Text = "Position - BankLoan Release";
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PositionBankLoanReleaseForm_KeyPress);
			this.Load += new System.EventHandler(this.PositionBankLoanReleaseForm_Load);
			this.Closed += new System.EventHandler(this.PositionBankLoanReleaseForm_Closed);
			this.TopPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ReleaseGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.AmountLabel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void PositionBankLoanReleaseForm_Load(object sender, System.EventArgs e)
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

			mainForm.Alert("Please wait ... loading current bank loan release data.", PilotState.Idle);

			try
			{										
				dataSet = mainForm.PositionAgent.BankLoanReleaseSummaryGet("");				
				
				bankReleasesDataViewRowFilter = "BookGroup = '" + bookGroup + "' AND Quantity > 0";
				bankReleasesDataView = new DataView(dataSet.Tables["BankLoanReleaseSummary"], bankReleasesDataViewRowFilter, "", DataViewRowState.CurrentRows);
				
				ReleaseGrid.SetDataBinding(bankReleasesDataView, "", true);
							
				FormStatusSet();
				FormAmountSet();

				mainForm.Alert("Please wait ... loading current bank loan release data ... Done!.", PilotState.Idle);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanReleaseForm.PositionBankLoanReleaseForm_Load]", Log.Error, 1); 				
				mainForm.Alert("Please wait ... loading current bank loan release data ... Error!.", PilotState.RunFault);
			}

			this.Cursor = Cursors.Default;
		}

		private void PositionBankLoanReleaseForm_Closed(object sender, System.EventArgs e)
		{
			if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
			}		
		}

		private void ReleaseGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch(e.Column.DataField)
			{
				case "Price":
				case "Amount":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.00");
					}
					catch {}
					break;
				
				case "LoanDate":
					e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateFormat);
					break;

				case "ActTime":
					e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeShortFormat);
					break;

				case "Quantity":
				case "QuantitySettled":			
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch {}
					break;
			}
		}
		
		private void ReleaseMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				releasesArray.Clear();

				if (ReleaseGrid.SelectedRows.Count == 0)	
				{
					releasesArray.Add(ReleaseGrid[ReleaseGrid.Row]);
				}
				else
				{
					foreach (int rowIndex in ReleaseGrid.SelectedRows)
					{					
						releasesArray.Add(ReleaseGrid[rowIndex]);					
					}
				}

				SendReleasesDelegate sendReleasesDelegate = new SendReleasesDelegate(SendReleases);
				sendReleasesDelegate.BeginInvoke(releasesArray, null, null);             
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanReleaseForm.ReleaseMenuItem_Click]", Log.Error, 1); 				
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		public void SendReleases(ArrayList releasesArray)
		{
			foreach (DataRowView row in releasesArray)
			{
				try
				{											
					mainForm.PositionAgent.BankLoanReleaseSet(
						row["BookGroup"].ToString(),						
						row["Book"].ToString(),
						"",
						row["LoanDate"].ToString(),
						row["LoanType"].ToString(),
						row["ActivityType"].ToString(),
						row["SecId"].ToString(),
						row["Quantity"].ToString(),
						"",
						"RR",
						mainForm.UserId);
				}			
				catch (Exception error)
				{
					Log.Write(error.Message + ". [PositionBankLoanReleaseForm.SendReleases]", Log.Error, 1); 				
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

		private void ReleaseGrid_FilterChange(object sender, System.EventArgs e)
		{
			string gridFilter;

			try
			{
				gridFilter = mainForm.GridFilterGet(ref ReleaseGrid);

				if (gridFilter.Equals(""))
				{
					bankReleasesDataView.RowFilter = bankReleasesDataViewRowFilter;
				}
				else
				{
					bankReleasesDataView.RowFilter = bankReleasesDataViewRowFilter + " AND " + gridFilter;
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
			if (ReleaseGrid.SelectedRows.Count > 0)
			{
				StatusLabel.Text = "Selecting " + ReleaseGrid.SelectedRows.Count.ToString("#,##0") + " items of " + bankReleasesDataView.Count.ToString("#,##0") + " shown in grid.";
			}
			else
			{
				StatusLabel.Text = "Showing " + bankReleasesDataView.Count.ToString("#,##0") + " items in grid.";
			}
		}

		private void ReleaseGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			FormStatusSet();
			FormAmountSet();
		}
		
		private void FormAmountSet()
		{
			decimal amount = 0;
			string amountTemp = "";

			if (ReleaseGrid.SelectedRows.Count > 0)
			{				
				foreach (int row in ReleaseGrid.SelectedRows)
				{
					try
					{
						if (ReleaseGrid.Columns["Amount"].CellValue(row).ToString().Equals(""))
						{
							amountTemp = "0";
						}
						else
						{
							amountTemp = ReleaseGrid.Columns["Amount"].CellValue(row).ToString();
						}
						
						amount += decimal.Parse(amountTemp);						
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
					if (ReleaseGrid.Columns["Amount"].Value.ToString().Equals(""))
					{
						amountTemp = "0";
					}
					else
					{
						amountTemp = ReleaseGrid.Columns["Amount"].Value.ToString();
					}
					
					AmountLabel.Text = "$" + decimal.Parse(amountTemp).ToString("#,##0.00");
				}
				catch (Exception error)
				{
					mainForm.Alert(error.Message, PilotState.RunFault);
				}
			}
		}

		private void ReleaseGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
		{
			FormAmountSet();
		}

		private void ReleaseGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{
				if (!ReleaseGrid.Columns["SecId"].Text.Equals(secId))
				{
					secId = ReleaseGrid.Columns["SecId"].Text;

					this.Cursor = Cursors.WaitCursor;                
     
					mainForm.SecId = secId;

					this.Cursor = Cursors.Default;
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [PositionBankLoanReleaseForm.ReleaseGrid_Paint]", Log.Error, 1); 				
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void PositionBankLoanReleaseForm_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)13) && ReleaseGrid.DataChanged)
			{
				ReleaseGrid.UpdateData();
				e.Handled = true;
			}
			else
			{
				e.Handled = false;
			}
		}

		private void ReleaseGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{
			if (ReleaseGrid.Columns["Account"].CellText(e.Row).Equals("0000"))
			{
				e.CellStyle.BackColor = System.Drawing.Color.LightGray;
			}
			else
			{
				e.CellStyle.BackColor = System.Drawing.Color.White;
			}
		}

		private void RefreshMenuItem_Click(object sender, System.EventArgs e)
		{
			mainForm.GridFilterClear(ref ReleaseGrid);
			
			try
			{
				DataRow [] rows = mainForm.PositionAgent.BankLoanReleaseSummaryGet("").Tables["BankLoanReleaseSummary"].Select();      
        
				this.Cursor = Cursors.WaitCursor;				
          
				dataSet.Tables["BankLoanReleaseSummary"].Clear();                                                        
          
				dataSet.Tables["BankLoanReleaseSummary"].BeginLoadData();                
        
				foreach (DataRow row in rows)
				{
					dataSet.Tables["BankLoanReleaseSummary"].ImportRow(row);          
				}

				dataSet.Tables["BankLoanReleaseSummary"].EndLoadData();
                				          				
				this.Cursor = Cursors.Default;        
			}                 				
			catch (Exception error)
			{				
				Log.Write(error.Message + ". [PositionBankLoanReleaseForm.RefreshMenuItem_Click]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}			
		}

		private void ReleaseGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			string gridData = "";

			if (e.KeyChar.Equals((char)3) && ReleaseGrid.SelectedRows.Count > 0)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ReleaseGrid.SelectedCols)
				{
					gridData += dataColumn.Caption + "\t";
				}

				gridData += "\n";

				foreach (int row in ReleaseGrid.SelectedRows)
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ReleaseGrid.SelectedCols)
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
				mainForm.Alert("Copied " + ReleaseGrid.SelectedRows.Count.ToString("#,##0") + " rows to the clipboard.");
				e.Handled = true;
			}
		}

		private void SendToMailRecipientMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (ReleaseGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
				return;
			}

			try
			{
				maxTextLength = new int[ReleaseGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ReleaseGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in ReleaseGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ReleaseGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ReleaseGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ReleaseGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in ReleaseGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ReleaseGrid.SelectedCols)
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

				mainForm.Alert("Total: " + ReleaseGrid.SelectedRows.Count + " items added to e-mail.");
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionBankLoanReleaseForm.MailRecipientMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (ReleaseGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
				return;
			}

			try
			{
				maxTextLength = new int[ReleaseGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ReleaseGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in ReleaseGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ReleaseGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ReleaseGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ReleaseGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in ReleaseGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ReleaseGrid.SelectedCols)
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

				mainForm.Alert("Total: " + ReleaseGrid.SelectedRows.Count + " items added to clipboard.");
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + ". [PositionBankLoanReleaseForm.SendToClipboardMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			Excel excel = new Excel();
			excel.ExportGridToExcel(ref ReleaseGrid);
		}
	
	}
}
