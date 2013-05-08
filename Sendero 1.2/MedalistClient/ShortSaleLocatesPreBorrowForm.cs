using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class ShortSaleLocatesPreBorrowForm : System.Windows.Forms.Form
	{
		private const int xSecId = 0;
    
		private DataSet groupCodeDataSet, preBorrowDataSet;
		
		private MainForm mainForm;
		private System.Windows.Forms.TextBox DescriptionTextBox;

		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep1MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;

		private C1.Win.C1Input.C1Label StatusLabel;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid PreBorrowGrid;
		private C1.Win.C1TrueDBGrid.C1TrueDBDropdown GroupCodeDropdown;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
    
		private System.ComponentModel.Container components = null;
    
		public ShortSaleLocatesPreBorrowForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
      
			InitializeComponent();
    
			try
			{
				bool mayEdit = mainForm.AdminAgent.MayEdit(mainForm.UserId, "ShortSaleLocatesPreBorrow");
        
				PreBorrowGrid.AllowUpdate = mayEdit;
				PreBorrowGrid.AllowAddNew = mayEdit;       
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [ShortSaleLocatesPreBorrowForm.ShortSaleLocatesPreBorrowForm]", Log.Error, 1); 
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleLocatesPreBorrowForm));
			this.PreBorrowGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.DescriptionTextBox = new System.Windows.Forms.TextBox();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.GroupCodeDropdown = new C1.Win.C1TrueDBGrid.C1TrueDBDropdown();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.PreBorrowGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeDropdown)).BeginInit();
			this.SuspendLayout();
			// 
			// PreBorrowGrid
			// 
			this.PreBorrowGrid.AllowAddNew = true;
			this.PreBorrowGrid.AllowColSelect = false;
			this.PreBorrowGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.PreBorrowGrid.AllowUpdateOnBlur = false;
			this.PreBorrowGrid.AlternatingRows = true;
			this.PreBorrowGrid.CaptionHeight = 17;
			this.PreBorrowGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.PreBorrowGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PreBorrowGrid.EmptyRows = true;
			this.PreBorrowGrid.ExtendRightColumn = true;
			this.PreBorrowGrid.FilterBar = true;
			this.PreBorrowGrid.ForeColor = System.Drawing.SystemColors.ControlText;
			this.PreBorrowGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.PreBorrowGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.PreBorrowGrid.Location = new System.Drawing.Point(1, 26);
			this.PreBorrowGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.PreBorrowGrid.Name = "PreBorrowGrid";
			this.PreBorrowGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.PreBorrowGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.PreBorrowGrid.PreviewInfo.ZoomFactor = 75;
			this.PreBorrowGrid.RecordSelectorWidth = 16;
			this.PreBorrowGrid.RowDivider.Color = System.Drawing.Color.WhiteSmoke;
			this.PreBorrowGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.PreBorrowGrid.RowHeight = 15;
			this.PreBorrowGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.PreBorrowGrid.Size = new System.Drawing.Size(934, 303);
			this.PreBorrowGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
			this.PreBorrowGrid.TabIndex = 0;
			this.PreBorrowGrid.Text = "ShortSaleLocatesPreBorrows";
			this.PreBorrowGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.PreBorrowGrid_BeforeUpdate);
			this.PreBorrowGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.PreBorrowGrid_BeforeColUpdate);
			this.PreBorrowGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.PreBorrowGrid_FormatText);
			this.PreBorrowGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PreBorrowGrid_KeyPress);
			this.PreBorrowGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"BizDate\" Da" +
				"taField=\"BizDate\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"GroupCode\" DataField=\"GroupCode\" DropDownCtl=\"GroupCodeDropdown\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Securit" +
				"y ID\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
				"Level=\"0\" Caption=\"Quantity\" DataField=\"Quantity\" NumberFormat=\"FormatText Event" +
				"\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act" +
				"UserId\" DataField=\"ActUserId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
				"olumn Level=\"0\" Caption=\"Act Time\" DataField=\"ActTime\" NumberFormat=\"FormatText " +
				"Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption" +
				"=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataCo" +
				"lumn Level=\"0\" Caption=\"Rebate Rate\" DataField=\"RebateRate\" NumberFormat=\"Format" +
				"Text Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"Allocated Quantity\" DataField=\"AllocatedQuantity\" NumberFormat=\"FormatTex" +
				"t Event\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.W" +
				"in.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightTex" +
				"t;BackColor:Highlight;}Inactive{ForeColor:InactiveCaptionText;BackColor:Inactive" +
				"Caption;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}FilterBar" +
				"{Font:Verdana, 8.25pt;BackColor:SeaShell;}Heading{Wrap:True;AlignVert:Center;Bor" +
				"der:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style18{}Style19" +
				"{}Style14{AlignHorz:Near;}Style15{AlignHorz:Near;}Style16{}Style17{}Style10{Alig" +
				"nHorz:Near;}Style11{}Style12{}Style13{}Style27{}Style29{AlignHorz:Near;}Style28{" +
				"AlignHorz:Near;}Style26{}Style25{}Style9{}Style8{}Style24{}Style23{}Style5{Trimm" +
				"ing:None;}Style4{}Style7{}Style6{}Style1{}Style22{}Style3{}Style2{}Style21{Align" +
				"Horz:Near;}Style20{AlignHorz:Near;}OddRow{}Style38{}Style39{}Style36{}Style37{}S" +
				"tyle34{AlignHorz:Near;}Style35{AlignHorz:Far;}Style32{}Style33{}Style30{}Style49" +
				"{}Style48{}Style31{}Normal{Font:Verdana, 8.25pt;}Style41{AlignHorz:Near;}Style40" +
				"{AlignHorz:Near;}Style43{}Style42{}Style45{}Style44{}Style47{AlignHorz:Near;}Sty" +
				"le46{AlignHorz:Near;}EvenRow{Font:Verdana, 8.25pt;BackColor:LightCyan;}Style59{A" +
				"lignHorz:Far;}Style58{AlignHorz:Near;}RecordSelector{AlignImage:Center;}Style51{" +
				"}Style50{}Footer{}Style52{AlignHorz:Near;}Style53{AlignHorz:Near;}Style54{}Style" +
				"55{}Style56{}Style57{}Caption{AlignHorz:Center;}Style69{}Style68{}Style63{}Style" +
				"62{}Style61{}Style60{}Style67{}Style66{}Style65{AlignHorz:Far;}Style64{AlignHorz" +
				":Near;}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}</D" +
				"ata></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"" +
				"Always\" AllowColSelect=\"False\" Name=\"\" AllowRowSizing=\"None\" AlternatingRowStyle" +
				"=\"True\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" Exte" +
				"ndRightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedCellBorder\" RecordSele" +
				"ctorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup" +
				"=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" m" +
				"e=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent" +
				"=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupSty" +
				"le parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><H" +
				"ighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inac" +
				"tive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorSt" +
				"yle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"" +
				"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent=\"Style1\" me=\"Style15\" /" +
				"><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"St" +
				"yle17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><GroupFooterStyle pare" +
				"nt=\"Style1\" me=\"Style18\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height" +
				">15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><Group" +
				"HeaderStyle parent=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=" +
				"\"Style24\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivide" +
				"r><Height>15</Height><Button>True</Button><DCIdx>1</DCIdx></C1DisplayColumn><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\"" +
				" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>" +
				"Gainsboro,Single</ColumnDivider><Height>15</Height><DCIdx>2</DCIdx></C1DisplayCo" +
				"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent" +
				"=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><Visible>True</Visible><Colum" +
				"nDivider>Gainsboro,Single</ColumnDivider><Height>15</Height><AllowFocus>False</A" +
				"llowFocus><Locked>True</Locked><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColum" +
				"n><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style3" +
				"5\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me" +
				"=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle " +
				"parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivider>Gainsboro,S" +
				"ingle</ColumnDivider><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1Dis" +
				"playColumn><HeadingStyle parent=\"Style2\" me=\"Style64\" /><Style parent=\"Style1\" m" +
				"e=\"Style65\" /><FooterStyle parent=\"Style3\" me=\"Style66\" /><EditorStyle parent=\"S" +
				"tyle5\" me=\"Style67\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style69\" /><GroupFoo" +
				"terStyle parent=\"Style1\" me=\"Style68\" /><Visible>True</Visible><ColumnDivider>Ga" +
				"insboro,Single</ColumnDivider><Width>146</Width><Height>15</Height><AllowFocus>F" +
				"alse</AllowFocus><Locked>True</Locked><DCIdx>8</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=" +
				"\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Sty" +
				"le5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFoote" +
				"rStyle parent=\"Style1\" me=\"Style62\" /><Visible>True</Visible><ColumnDivider>Gain" +
				"sboro,Single</ColumnDivider><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn" +
				"><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"St" +
				"yle1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle pa" +
				"rent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><G" +
				"roupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visible>True</Visible><ColumnDiv" +
				"ider>Gainsboro,Single</ColumnDivider><Height>15</Height><AllowFocus>False</Allow" +
				"Focus><Locked>True</Locked><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /" +
				"><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"St" +
				"yle49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle pare" +
				"nt=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Singl" +
				"e</ColumnDivider><Height>15</Height><AllowFocus>False</AllowFocus><Locked>True</" +
				"Locked><DCIdx>5</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 930, 2" +
				"99</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Split" +
				"s><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading" +
				"\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /" +
				"><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" />" +
				"<Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" />" +
				"<Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Styl" +
				"e parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /" +
				"><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><h" +
				"orzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</Default" +
				"RecSelWidth><ClientArea>0, 0, 930, 299</ClientArea><PrintPageHeaderStyle parent=" +
				"\"\" me=\"Style26\" /><PrintPageFooterStyle parent=\"\" me=\"Style27\" /></Blob>";
			// 
			// DescriptionTextBox
			// 
			this.DescriptionTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.DescriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.DescriptionTextBox.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DescriptionTextBox.ForeColor = System.Drawing.Color.Maroon;
			this.DescriptionTextBox.Location = new System.Drawing.Point(24, 6);
			this.DescriptionTextBox.Name = "DescriptionTextBox";
			this.DescriptionTextBox.ReadOnly = true;
			this.DescriptionTextBox.Size = new System.Drawing.Size(456, 15);
			this.DescriptionTextBox.TabIndex = 4;
			this.DescriptionTextBox.Text = "";
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.SendToMenuItem,
																																										this.Sep1MenuItem,
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
			// 
			// SendToEmailMenuItem
			// 
			this.SendToEmailMenuItem.Index = 2;
			this.SendToEmailMenuItem.Text = "Mail Recipient";
			this.SendToEmailMenuItem.Click += new System.EventHandler(this.SendToEmailMenuItem_Click);
			// 
			// Sep1MenuItem
			// 
			this.Sep1MenuItem.Index = 1;
			this.Sep1MenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 2;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StatusLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.StatusLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.StatusLabel.Location = new System.Drawing.Point(16, 333);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(600, 16);
			this.StatusLabel.TabIndex = 8;
			this.StatusLabel.Tag = null;
			this.StatusLabel.TextDetached = true;
			// 
			// GroupCodeDropdown
			// 
			this.GroupCodeDropdown.AllowColMove = true;
			this.GroupCodeDropdown.AllowColSelect = true;
			this.GroupCodeDropdown.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows;
			this.GroupCodeDropdown.AlternatingRows = false;
			this.GroupCodeDropdown.CaptionHeight = 17;
			this.GroupCodeDropdown.ColumnCaptionHeight = 17;
			this.GroupCodeDropdown.ColumnFooterHeight = 17;
			this.GroupCodeDropdown.ExtendRightColumn = true;
			this.GroupCodeDropdown.FetchRowStyles = false;
			this.GroupCodeDropdown.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.GroupCodeDropdown.Location = new System.Drawing.Point(40, 64);
			this.GroupCodeDropdown.Name = "GroupCodeDropdown";
			this.GroupCodeDropdown.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.GroupCodeDropdown.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.GroupCodeDropdown.RowHeight = 15;
			this.GroupCodeDropdown.RowSubDividerColor = System.Drawing.Color.Gainsboro;
			this.GroupCodeDropdown.ScrollTips = false;
			this.GroupCodeDropdown.Size = new System.Drawing.Size(408, 120);
			this.GroupCodeDropdown.TabIndex = 9;
			this.GroupCodeDropdown.TabStop = false;
			this.GroupCodeDropdown.Text = "c1TrueDBDropdown1";
			this.GroupCodeDropdown.Visible = false;
			this.GroupCodeDropdown.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Group Code\"" +
				" DataField=\"GroupCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"Group Name\" DataField=\"GroupName\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper" +
				"\"><Data>Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;}Style25{}Selected" +
				"{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style18{}Style19{}Style14{" +
				"AlignHorz:Near;}Style15{AlignHorz:Near;}Style16{}Style17{}Style10{AlignHorz:Near" +
				";}Style11{}OddRow{}Style13{}Style12{}Footer{}HighlightRow{ForeColor:HighlightTex" +
				"t;BackColor:Highlight;}RecordSelector{AlignImage:Center;}Style24{}Style23{}Style" +
				"22{}Style21{AlignHorz:Near;}Style20{AlignHorz:Near;}Inactive{ForeColor:InactiveC" +
				"aptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;" +
				"BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Cent" +
				"er;}FilterBar{}Style4{}Style9{}Style8{}Style5{}Group{AlignVert:Center;Border:Non" +
				"e,,0, 0, 0, 0;BackColor:ControlDark;}Style7{}Style6{}Style1{}Style3{}Style2{}</D" +
				"ata></Styles><Splits><C1.Win.C1TrueDBGrid.DropdownView Name=\"\" CaptionHeight=\"17" +
				"\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" Marq" +
				"ueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" RecordS" +
				"electors=\"False\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle" +
				" parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><Even" +
				"RowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"S" +
				"tyle13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" " +
				"me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle p" +
				"arent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" " +
				"/><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"Record" +
				"Selector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style p" +
				"arent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent" +
				"=\"Style2\" me=\"Style14\" /><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle pare" +
				"nt=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHea" +
				"derStyle parent=\"Style1\" me=\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"St" +
				"yle18\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><" +
				"Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
				"le parent=\"Style2\" me=\"Style20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterS" +
				"tyle parent=\"Style3\" me=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" />" +
				"<GroupHeaderStyle parent=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style" +
				"1\" me=\"Style24\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnD" +
				"ivider><Width>300</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn></" +
				"internalCols><ClientRect>0, 0, 404, 116</ClientRect><BorderSide>0</BorderSide></" +
				"C1.Win.C1TrueDBGrid.DropdownView></Splits><NamedStyles><Style parent=\"\" me=\"Norm" +
				"al\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" " +
				"/><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /" +
				"><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><St" +
				"yle parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><S" +
				"tyle parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /" +
				"><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></" +
				"NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified" +
				"</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 404, 116</" +
				"ClientArea></Blob>";
			// 
			// SendToExcelMenuItem
			// 
			this.SendToExcelMenuItem.Index = 1;
			this.SendToExcelMenuItem.Text = "Excel";
			this.SendToExcelMenuItem.Click += new System.EventHandler(this.SendToExcelMenuItem_Click);
			// 
			// ShortSaleLocatesPreBorrowForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(936, 349);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.GroupCodeDropdown);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.DescriptionTextBox);
			this.Controls.Add(this.PreBorrowGrid);
			this.DockPadding.Bottom = 20;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 26;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ShortSaleLocatesPreBorrowForm";
			this.Text = "ShortSale - Locates - PreBorrow";
			this.Load += new System.EventHandler(this.ShortSaleLocatesPreBorrowForm_Load);
			this.Closed += new System.EventHandler(this.ShortSaleLocatesPreBorrowForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.PreBorrowGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeDropdown)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion    

		private void SendToClipboard()
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PreBorrowGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\n";

			foreach (int row in PreBorrowGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PreBorrowGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\n";
			}

			Clipboard.SetDataObject(gridData, true);
			mainForm.Alert("Total: " + PreBorrowGrid.SelectedRows.Count + " items copied to clipboard.", PilotState.Normal);
		}

		private void StatusSet()
		{
			if (PreBorrowGrid.SelectedRows.Count > 0)
			{
				StatusLabel.Text = "Selected " + PreBorrowGrid.SelectedRows.Count.ToString("#,##0") + " items of "
					+ PreBorrowGrid.Splits[0].Rows.Count.ToString("#,##0") + " shown in grid.";
			}
			else
			{
				StatusLabel.Text = "Showing " + PreBorrowGrid.Splits[0].Rows.Count.ToString("#,##0") + " items in grid.";
			}
		}

		private void ShortSaleLocatesPreBorrowForm_Load(object sender, System.EventArgs e)
		{
			this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
			this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
			this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "450"));
			this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "645"));

			this.Show();
			Application.DoEvents();
      

			try
			{
				groupCodeDataSet = mainForm.ShortSaleAgent.TradingGroupsGet("", 0);				
				
				preBorrowDataSet = mainForm.ShortSaleAgent.LocatesPreBorrowGet(mainForm.ServiceAgent.KeyValueGet("BizDate", ""), "", mainForm.UtcOffset);
				PreBorrowGrid.SetDataBinding(preBorrowDataSet, "PreBorrows", true);
							
				GroupCodeDropdown.SetDataBinding(groupCodeDataSet, "TradingGroups", true);
			
				StatusSet();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}      
		}

		private void ShortSaleLocatesPreBorrowForm_Closed(object sender, System.EventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());        
			}

			try
			{
				mainForm.shortSaleLocateForm.shortSaleLocatesPreBorrowForm = null;
			}
			catch {}
		}

		private void PreBorrowGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0)
			{
				return;
			}

			try
			{
				switch(PreBorrowGrid.Columns[e.ColIndex].DataField)
				{
					case("ActTime"):          
						e.Value = DateTime.Parse(e.Value).ToString(Standard.DateTimeShortFormat);
						break;

					case("AllocatedQuantity"):          
					case("Quantity"):          
						e.Value = long.Parse(e.Value).ToString("#,##0");
						break;
					
					case("RebateRate"):          					
						e.Value = decimal.Parse(e.Value).ToString("00.000");
						break;
				}
			}
			catch {}
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (PreBorrowGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows to copy.", PilotState.RunFault);
				return;
			}

			try
			{
				maxTextLength = new int[PreBorrowGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PreBorrowGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in PreBorrowGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PreBorrowGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PreBorrowGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PreBorrowGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in PreBorrowGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PreBorrowGrid.SelectedCols)
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

				mainForm.Alert("Total: " + PreBorrowGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
			}
			catch (Exception error)
			{       
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [ShortSaleListsHardBorrowForm.SendToEmailMenuItem_Click]", Log.Error, 1); 
			}
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void PreBorrowGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				if (PreBorrowGrid.Columns["GroupCode"].Text.Equals(""))
				{
					mainForm.Alert("Please select a group code.", PilotState.RunFault);				
					e.Cancel = true;
				}
				else if (PreBorrowGrid.Columns["SecId"].Text.Equals(""))
				{
					mainForm.Alert("Please enter a security id.", PilotState.RunFault);
					e.Cancel = true;
				}
				else if (PreBorrowGrid.Columns["Quantity"].Text.Equals(""))
				{
					mainForm.Alert("Please enter a quantity.", PilotState.RunFault);					
					e.Cancel = true;
				}				
				else
				{
					mainForm.ShortSaleAgent.LocatesPreBorrowSet(
						mainForm.ServiceAgent.BizDate(),
						PreBorrowGrid.Columns["GroupCode"].Value.ToString(),
						PreBorrowGrid.Columns["SecId"].Value.ToString(),
						PreBorrowGrid.Columns["Quantity"].Value.ToString(),
						PreBorrowGrid.Columns["RebateRate"].Value.ToString(),
						mainForm.UserId);

					PreBorrowGrid.Columns["ActUserId"].Text = mainForm.UserShortName;
					PreBorrowGrid.Columns["ActTime"].Value = DateTime.Now.ToString();
				
					e.Cancel = false;
				}
				StatusSet();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				e.Cancel = true;
			}
		}

		private void PreBorrowGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char) 13)
			{
				PreBorrowGrid.UpdateData();
			}
		}

		private void PreBorrowGrid_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			try
			{
				switch (e.Column.DataColumn.DataField)
				{
					case "SecId" :
						mainForm.SecId = PreBorrowGrid.Columns["SecId"].Text;            
						PreBorrowGrid.Columns["SecId"].Text = mainForm.SecId;       
						PreBorrowGrid.Columns["Symbol"].Text = mainForm.Symbol;
						break;

					case "Quantity" :						
						PreBorrowGrid.Columns["AllocatedQuantity"].Text = "0";       						
						break;
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				e.Cancel = true;
			}
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				Excel excel = new Excel();
				excel.ExportGridToExcel(ref PreBorrowGrid);
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}

		}		
	}
}
