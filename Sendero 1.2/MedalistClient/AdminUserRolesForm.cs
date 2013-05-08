// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Anetics.Common;
using Anetics.Medalist;

namespace Anetics.Medalist
{
  public class AdminUserRolesForm : System.Windows.Forms.Form
  {
    private MainForm mainForm;
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid RolesGrid;
    private System.Windows.Forms.RadioButton FunctionsRadio;
    private System.ComponentModel.Container components = null;
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid RolesUsersGrid;
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid RolesFunctionsGrid;
    private System.Windows.Forms.RadioButton UserRolesRadio;

    private int xRoleCode = 0;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep2MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
    private DataSet ds;

    public AdminUserRolesForm(MainForm mainForm)
    {
      this.mainForm = mainForm;
      InitializeComponent();
  
      try
      {
        if(mainForm.AdminAgent.MayEdit(mainForm.UserId, "AdminUser"))
        {
          RolesGrid.AllowUpdate   = true;
          RolesGrid.AllowAddNew   = true;
          RolesGrid.AllowDelete   = true;          
        }
      }
      catch (Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminUserRolesForm.AdminUserRolesForm]", 1);
      }
    }

    protected override void Dispose(bool disposing)
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
   
    private void InitializeComponent()
    {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AdminUserRolesForm));
			this.RolesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.RolesFunctionsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.FunctionsRadio = new System.Windows.Forms.RadioButton();
			this.RolesUsersGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.UserRolesRadio = new System.Windows.Forms.RadioButton();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.RolesGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RolesFunctionsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RolesUsersGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// RolesGrid
			// 
			this.RolesGrid.AllowColMove = false;
			this.RolesGrid.AllowColSelect = false;
			this.RolesGrid.AllowDrag = true;
			this.RolesGrid.AllowUpdate = false;
			this.RolesGrid.CaptionHeight = 17;
			this.RolesGrid.ChildGrid = this.RolesFunctionsGrid;
			this.RolesGrid.ContextMenu = this.MainContextMenu;
			this.RolesGrid.DefColWidth = 200;
			this.RolesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.RolesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RolesGrid.EmptyRows = true;
			this.RolesGrid.ExtendRightColumn = true;
			this.RolesGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.RolesGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.RolesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.RolesGrid.Location = new System.Drawing.Point(1, 1);
			this.RolesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.RolesGrid.Name = "RolesGrid";
			this.RolesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.RolesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.RolesGrid.PreviewInfo.ZoomFactor = 75;
			this.RolesGrid.RecordSelectorWidth = 16;
			this.RolesGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.RolesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.RolesGrid.RowHeight = 15;
			this.RolesGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
			this.RolesGrid.ScrollTrack = false;
			this.RolesGrid.Size = new System.Drawing.Size(862, 428);
			this.RolesGrid.TabAcrossSplits = true;
			this.RolesGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.RolesGrid.TabIndex = 0;
			this.RolesGrid.TabStop = false;
			this.RolesGrid.WrapCellPointer = true;
			this.RolesGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.RolesGrid_BeforeColEdit);
			this.RolesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.RolesGrid_BeforeUpdate);
			this.RolesGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.RolesGrid_BeforeDelete);
			this.RolesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.RolesGrid_FormatText);
			this.RolesGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RolesGrid_KeyPress);
			this.RolesGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"User Id\" Da" +
				"taField=\"UserId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"" +
				"0\" Caption=\"Role Code\" DataField=\"RoleCode\"><ValueItems /><GroupInfo /></C1DataC" +
				"olumn><C1DataColumn Level=\"0\" Caption=\"Comment\" DataField=\"Comment\"><ValueItems " +
				"/><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataFie" +
				"ld=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1Data" +
				"Column><C1DataColumn Level=\"0\" Caption=\"Actor\" DataField=\"ActUserShortName\"><Val" +
				"ueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Role\" Dat" +
				"aField=\"Role\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=" +
				"\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Style12{}Style50{}Style51{}Cap" +
				"tion{AlignHorz:Center;}Style27{}Normal{Font:Verdana, 8.25pt;}Style25{}Selected{F" +
				"oreColor:HighlightText;BackColor:Highlight;}Editor{}Style18{}Style19{}Style14{Al" +
				"ignHorz:Near;}Style15{AlignHorz:Near;}Style16{}Style17{}Style10{AlignHorz:Near;}" +
				"Style11{}OddRow{}Style13{}Style45{AlignHorz:Near;}Style44{AlignHorz:Near;}Style3" +
				"8{AlignHorz:Near;}Style39{AlignHorz:Near;}Style36{}Style34{}Style35{}Style32{Ali" +
				"gnHorz:Near;}Style33{AlignHorz:Near;}Style31{}Style29{AlignHorz:Near;}Style28{Al" +
				"ignHorz:Near;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style26{" +
				"}RecordSelector{AlignImage:Center;}Footer{}Style23{}Style22{}Style21{AlignHorz:N" +
				"ear;}Style20{AlignHorz:Near;}Inactive{ForeColor:InactiveCaptionText;BackColor:In" +
				"activeCaption;}EvenRow{BackColor:LightCyan;}Heading{Wrap:True;BackColor:Control;" +
				"Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style49{}Style" +
				"48{}Style24{}Style4{}Style41{}Style40{}Style43{}Style42{}Style5{}Style47{}Style9" +
				"{}Style8{}Style46{}FilterBar{}Style37{}Group{AlignVert:Center;Border:None,,0, 0," +
				" 0, 0;BackColor:ControlDark;}Style7{}Style6{}Style1{}Style30{}Style3{}Style2{}</" +
				"Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=" +
				"\"Always\" AllowColMove=\"False\" AllowColSelect=\"False\" Name=\"\" CaptionHeight=\"17\" " +
				"ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" Marque" +
				"eStyle=\"SolidCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalSc" +
				"rollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style1" +
				"0\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" m" +
				"e=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle pare" +
				"nt=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyl" +
				"e parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"St" +
				"yle7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddR" +
				"ow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><S" +
				"electedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" " +
				"/><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><S" +
				"tyle parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" />" +
				"<EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me" +
				"=\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><ColumnDivider>Dar" +
				"kGray,Single</ColumnDivider><Height>15</Height><AllowSizing>False</AllowSizing><" +
				"DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" " +
				"me=\"Style20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3" +
				"\" me=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle p" +
				"arent=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><" +
				"Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>150<" +
				"/Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" />" +
				"<FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
				"le31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle paren" +
				"t=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single" +
				"</ColumnDivider><Width>135</Width><Height>15</Height><DCIdx>5</DCIdx></C1Display" +
				"Column><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style44\" /><Style pare" +
				"nt=\"Style1\" me=\"Style45\" /><FooterStyle parent=\"Style3\" me=\"Style46\" /><EditorSt" +
				"yle parent=\"Style5\" me=\"Style47\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style49" +
				"\" /><GroupFooterStyle parent=\"Style1\" me=\"Style48\" /><Visible>True</Visible><Col" +
				"umnDivider>Gainsboro,Single</ColumnDivider><Width>150</Width><Height>15</Height>" +
				"<AllowFocus>False</AllowFocus><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn" +
				"><HeadingStyle parent=\"Style2\" me=\"Style38\" /><Style parent=\"Style1\" me=\"Style39" +
				"\" /><FooterStyle parent=\"Style3\" me=\"Style40\" /><EditorStyle parent=\"Style5\" me=" +
				"\"Style41\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style43\" /><GroupFooterStyle p" +
				"arent=\"Style1\" me=\"Style42\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Si" +
				"ngle</ColumnDivider><Width>120</Width><Height>15</Height><AllowFocus>False</Allo" +
				"wFocus><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style32\" /><Style parent=\"Style1\" me=\"Style33\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style34\" /><EditorStyle parent=\"Style5\" me=\"Style35\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style37\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e36\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Wi" +
				"dth>150</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn></internalCo" +
				"ls><ClientRect>0, 0, 858, 424</ClientRect><BorderSide>0</BorderSide></C1.Win.C1T" +
				"rueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style " +
				"parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style pare" +
				"nt=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style paren" +
				"t=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"N" +
				"ormal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"" +
				"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style paren" +
				"t=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><" +
				"vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><Def" +
				"aultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 858, 424</ClientArea><P" +
				"rintPageHeaderStyle parent=\"\" me=\"Style26\" /><PrintPageFooterStyle parent=\"\" me=" +
				"\"Style27\" /></Blob>";
			// 
			// RolesFunctionsGrid
			// 
			this.RolesFunctionsGrid.AllowColMove = false;
			this.RolesFunctionsGrid.AllowColSelect = false;
			this.RolesFunctionsGrid.CaptionHeight = 17;
			this.RolesFunctionsGrid.DefColWidth = 150;
			this.RolesFunctionsGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.RolesFunctionsGrid.EmptyRows = true;
			this.RolesFunctionsGrid.ExtendRightColumn = true;
			this.RolesFunctionsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.RolesFunctionsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.RolesFunctionsGrid.ImeMode = System.Windows.Forms.ImeMode.On;
			this.RolesFunctionsGrid.Location = new System.Drawing.Point(24, 72);
			this.RolesFunctionsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.RolesFunctionsGrid.Name = "RolesFunctionsGrid";
			this.RolesFunctionsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.RolesFunctionsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.RolesFunctionsGrid.PreviewInfo.ZoomFactor = 75;
			this.RolesFunctionsGrid.RecordSelectorWidth = 16;
			this.RolesFunctionsGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.RolesFunctionsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.RolesFunctionsGrid.RowHeight = 15;
			this.RolesFunctionsGrid.RowSubDividerColor = System.Drawing.Color.WhiteSmoke;
			this.RolesFunctionsGrid.Size = new System.Drawing.Size(824, 112);
			this.RolesFunctionsGrid.TabAcrossSplits = true;
			this.RolesFunctionsGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.RolesFunctionsGrid.TabIndex = 1;
			this.RolesFunctionsGrid.TabStop = false;
			this.RolesFunctionsGrid.Text = "Role Function Grid";
			this.RolesFunctionsGrid.Visible = false;
			this.RolesFunctionsGrid.WrapCellPointer = true;
			this.RolesFunctionsGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.RolesFunctionsGrid_BeforeColEdit);
			this.RolesFunctionsGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.RolesFunctionsGrid_BeforeUpdate);
			this.RolesFunctionsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.RolesFunctionsGrid_FormatText);
			this.RolesFunctionsGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RolesFunctionsGrid_KeyPress);
			this.RolesFunctionsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Role Code\" " +
				"DataField=\"RoleCode\" NumberFormat=\"FormatText Event\"><ValueItems Presentation=\"C" +
				"heckBox\" Translate=\"True\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
				"Caption=\"Function Path\" DataField=\"FunctionPath\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"May View\" DataField=\"MayView\"><Value" +
				"Items Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"May Edit\" DataField=\"MayEdit\"><ValueItems Presentation=\"CheckBox\" /" +
				"><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Comment\" DataField" +
				"=\"Comment\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
				"tion=\"Actor\" DataField=\"ActUserShortName\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataField=\"ActTime\" NumberFormat=" +
				"\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"Book Group List\" DataField=\"BookGroupList\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWr" +
				"apper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Inactive{" +
				"ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Selected{ForeColor:High" +
				"lightText;BackColor:Highlight;}Editor{}FilterBar{}Heading{Wrap:True;BackColor:Co" +
				"ntrol;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style18{" +
				"}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}Style17{AlignHorz:Near;}Styl" +
				"e10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style27{}Style29{AlignHorz:Center" +
				";}Style28{AlignHorz:Center;}Style26{}Style25{}Style9{}Style8{}Style24{}Style23{A" +
				"lignHorz:Near;}Style5{}Style4{}Style7{}Style6{}Style1{}Style22{AlignHorz:Near;}S" +
				"tyle3{}Style2{}Style21{}Style20{}OddRow{}Style38{}Style39{}Style36{}Style37{}Sty" +
				"le34{AlignHorz:Center;}Style35{AlignHorz:Center;}Style32{}Style33{}Style30{}Styl" +
				"e49{}Style48{}Style31{}Normal{Font:Verdana, 8.25pt;BackColor:Window;}Style41{Ali" +
				"gnHorz:Near;}Style40{AlignHorz:Near;}Style43{}Style42{}Style45{}Style44{}Style47" +
				"{AlignHorz:Near;}Style46{AlignHorz:Near;}EvenRow{BackColor:Aqua;}Style59{}Style5" +
				"8{}RecordSelector{AlignImage:Center;}Style51{}Style50{}Footer{}Style52{AlignHorz" +
				":Near;}Style53{AlignHorz:Near;}Style54{}Style55{}Style56{}Style57{}Caption{Align" +
				"Horz:Center;}Style63{}Style62{}Style61{AlignHorz:Near;}Style60{AlignHorz:Near;}S" +
				"tyle65{}Style64{}Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:Contro" +
				"lDark;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" V" +
				"BarStyle=\"Always\" AllowColMove=\"False\" AllowColSelect=\"False\" Name=\"\" CaptionHei" +
				"ght=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"Tru" +
				"e\" MarqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" " +
				"VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" " +
				"me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"" +
				"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><Footer" +
				"Style parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><H" +
				"eadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightR" +
				"ow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle pa" +
				"rent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Sty" +
				"le11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me" +
				"=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
				"le16\" /><Style parent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"S" +
				"tyle18\" /><EditorStyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"" +
				"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><ColumnD" +
				"ivider>DarkGray,Single</ColumnDivider><Width>120</Width><Height>15</Height><Allo" +
				"wFocus>False</AllowFocus><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
				"dingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><" +
				"FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Styl" +
				"e25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent" +
				"=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single<" +
				"/ColumnDivider><Width>150</Width><Height>15</Height><AllowFocus>False</AllowFocu" +
				"s><Locked>True</Locked><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style3" +
				"1\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</C" +
				"olumnDivider><Width>80</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColu" +
				"mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"" +
				"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle " +
				"parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" />" +
				"<GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnD" +
				"ivider>Gainsboro,Single</ColumnDivider><Width>80</Width><Height>15</Height><DCId" +
				"x>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"" +
				"Style60\" /><Style parent=\"Style1\" me=\"Style61\" /><FooterStyle parent=\"Style3\" me" +
				"=\"Style62\" /><EditorStyle parent=\"Style5\" me=\"Style63\" /><GroupHeaderStyle paren" +
				"t=\"Style1\" me=\"Style65\" /><GroupFooterStyle parent=\"Style1\" me=\"Style64\" /><Visi" +
				"ble>True</Visible><ColumnDivider>DarkGray,None</ColumnDivider><Width>150</Width>" +
				"<Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSt" +
				"yle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><Footer" +
				"Style parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /" +
				"><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Styl" +
				"e1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Colum" +
				"nDivider><Width>150</Width><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn>" +
				"<C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Sty" +
				"le1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle par" +
				"ent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><Gr" +
				"oupFooterStyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivi" +
				"der>Gainsboro,Single</ColumnDivider><Width>150</Width><Height>15</Height><AllowF" +
				"ocus>False</AllowFocus><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style5" +
				"5\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style56\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</C" +
				"olumnDivider><Width>120</Width><Height>15</Height><AllowFocus>False</AllowFocus>" +
				"<DCIdx>6</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 820, 108</Cli" +
				"entRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><Name" +
				"dStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><St" +
				"yle parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style" +
				" parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style " +
				"parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style " +
				"parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style paren" +
				"t=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style" +
				" parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSpli" +
				"ts>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelW" +
				"idth><ClientArea>0, 0, 820, 108</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"" +
				"Style58\" /><PrintPageFooterStyle parent=\"\" me=\"Style59\" /></Blob>";
			// 
			// FunctionsRadio
			// 
			this.FunctionsRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.FunctionsRadio.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FunctionsRadio.Location = new System.Drawing.Point(200, 440);
			this.FunctionsRadio.Name = "FunctionsRadio";
			this.FunctionsRadio.Size = new System.Drawing.Size(144, 24);
			this.FunctionsRadio.TabIndex = 3;
			this.FunctionsRadio.Text = "Functions For Roles";
			this.FunctionsRadio.Click += new System.EventHandler(this.FunctionsRadio_Click);
			// 
			// RolesUsersGrid
			// 
			this.RolesUsersGrid.AllowColMove = false;
			this.RolesUsersGrid.AllowColSelect = false;
			this.RolesUsersGrid.CaptionHeight = 17;
			this.RolesUsersGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.RolesUsersGrid.EmptyRows = true;
			this.RolesUsersGrid.ExtendRightColumn = true;
			this.RolesUsersGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.RolesUsersGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.RolesUsersGrid.Location = new System.Drawing.Point(24, 216);
			this.RolesUsersGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.RolesUsersGrid.Name = "RolesUsersGrid";
			this.RolesUsersGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.RolesUsersGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.RolesUsersGrid.PreviewInfo.ZoomFactor = 75;
			this.RolesUsersGrid.RecordSelectorWidth = 16;
			this.RolesUsersGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.RolesUsersGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.RolesUsersGrid.RowHeight = 15;
			this.RolesUsersGrid.RowSubDividerColor = System.Drawing.Color.WhiteSmoke;
			this.RolesUsersGrid.Size = new System.Drawing.Size(696, 112);
			this.RolesUsersGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.RolesUsersGrid.TabIndex = 4;
			this.RolesUsersGrid.Visible = false;
			this.RolesUsersGrid.WrapCellPointer = true;
			this.RolesUsersGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.RolesUsersGrid_BeforeUpdate);
			this.RolesUsersGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.RolesUsersGrid_FormatText);
			this.RolesUsersGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RolesUsersGrid_KeyPress);
			this.RolesUsersGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"User Id\" Da" +
				"taField=\"UserId\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"Role Code\" DataField=\"RoleCode\"><Val" +
				"ueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Comment\" " +
				"DataField=\"Comment\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
				"l=\"0\" Caption=\"Actor\" DataField=\"ActUserShortName\"><ValueItems /><GroupInfo /></" +
				"C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataField=\"ActTime\" Numb" +
				"erFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols" +
				"><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Caption{AlignHor" +
				"z:Center;}Style27{AlignHorz:Near;}Normal{Font:Verdana, 8.25pt;}Style25{}Style24{" +
				"}Editor{}Style18{}Style19{}Style14{AlignHorz:Near;}Style15{AlignHorz:Near;}Style" +
				"16{}Style17{}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}Style45{}Style12{" +
				"}Style29{}Style28{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Sty" +
				"le26{AlignHorz:Near;}RecordSelector{AlignImage:Center;}Footer{}Style23{}Style22{" +
				"}Style21{AlignHorz:Near;}Style20{AlignHorz:Near;}Group{BackColor:ControlDark;Bor" +
				"der:None,,0, 0, 0, 0;AlignVert:Center;}Inactive{ForeColor:InactiveCaptionText;Ba" +
				"ckColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Style6{}Heading{Wrap:True;Align" +
				"Vert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}S" +
				"tyle3{}Style4{}Style7{}Style8{}Style1{}Style5{}Style41{}Style40{}Style43{}Filter" +
				"Bar{}Style42{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style44{}Sty" +
				"le9{}Style38{AlignHorz:Near;}Style39{AlignHorz:Near;}Style36{}Style37{}Style34{}" +
				"Style35{}Style32{AlignHorz:Near;}Style33{AlignHorz:Near;}Style30{}Style31{}Style" +
				"2{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarS" +
				"tyle=\"Always\" AllowColMove=\"False\" AllowColSelect=\"False\" Name=\"\" CaptionHeight=" +
				"\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" M" +
				"arqueeStyle=\"DottedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" Vert" +
				"icalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"" +
				"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"Even" +
				"Row\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyl" +
				"e parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><Headi" +
				"ngStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" " +
				"me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent" +
				"=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11" +
				"\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"St" +
				"yle1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14" +
				"\" /><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style" +
				"16\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Styl" +
				"e1\" me=\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>Tru" +
				"e</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>150</Width><Hei" +
				"ght>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx>0</DCId" +
				"x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style20\" " +
				"/><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Style22" +
				"\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style1" +
				"\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><ColumnDivider" +
				">Gainsboro,Single</ColumnDivider><Width>169</Width><Height>15</Height><Locked>Tr" +
				"ue</Locked><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pare" +
				"nt=\"Style2\" me=\"Style26\" /><Style parent=\"Style1\" me=\"Style27\" /><FooterStyle pa" +
				"rent=\"Style3\" me=\"Style28\" /><EditorStyle parent=\"Style5\" me=\"Style29\" /><GroupH" +
				"eaderStyle parent=\"Style1\" me=\"Style31\" /><GroupFooterStyle parent=\"Style1\" me=\"" +
				"Style30\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider" +
				"><Width>200</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style2\" me=\"Style32\" /><Style parent=\"Style1\" me=" +
				"\"Style33\" /><FooterStyle parent=\"Style3\" me=\"Style34\" /><EditorStyle parent=\"Sty" +
				"le5\" me=\"Style35\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style37\" /><GroupFoote" +
				"rStyle parent=\"Style1\" me=\"Style36\" /><Visible>True</Visible><ColumnDivider>Gain" +
				"sboro,Single</ColumnDivider><Width>150</Width><Height>15</Height><AllowFocus>Fal" +
				"se</AllowFocus><Locked>True</Locked><DCIdx>3</DCIdx></C1DisplayColumn><C1Display" +
				"Column><HeadingStyle parent=\"Style2\" me=\"Style38\" /><Style parent=\"Style1\" me=\"S" +
				"tyle39\" /><FooterStyle parent=\"Style3\" me=\"Style40\" /><EditorStyle parent=\"Style" +
				"5\" me=\"Style41\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style43\" /><GroupFooterS" +
				"tyle parent=\"Style1\" me=\"Style42\" /><Visible>True</Visible><ColumnDivider>Gainsb" +
				"oro,Single</ColumnDivider><Width>120</Width><Height>15</Height><AllowFocus>False" +
				"</AllowFocus><Locked>True</Locked><DCIdx>4</DCIdx></C1DisplayColumn></internalCo" +
				"ls><ClientRect>0, 0, 692, 108</ClientRect><BorderSide>0</BorderSide></C1.Win.C1T" +
				"rueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style " +
				"parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style pare" +
				"nt=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style paren" +
				"t=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"N" +
				"ormal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"" +
				"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style paren" +
				"t=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><" +
				"vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><Def" +
				"aultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 692, 108</ClientArea><P" +
				"rintPageHeaderStyle parent=\"\" me=\"Style44\" /><PrintPageFooterStyle parent=\"\" me=" +
				"\"Style45\" /></Blob>";
			// 
			// UserRolesRadio
			// 
			this.UserRolesRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.UserRolesRadio.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.UserRolesRadio.Location = new System.Drawing.Point(40, 440);
			this.UserRolesRadio.Name = "UserRolesRadio";
			this.UserRolesRadio.Size = new System.Drawing.Size(144, 24);
			this.UserRolesRadio.TabIndex = 5;
			this.UserRolesRadio.Text = "Users For Roles";
			this.UserRolesRadio.Click += new System.EventHandler(this.UserRolesRadio_Click);
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
			// AdminUserRolesForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(864, 469);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.UserRolesRadio);
			this.Controls.Add(this.RolesUsersGrid);
			this.Controls.Add(this.RolesFunctionsGrid);
			this.Controls.Add(this.RolesGrid);
			this.Controls.Add(this.FunctionsRadio);
			this.DockPadding.Bottom = 40;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 1;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.HelpButton = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AdminUserRolesForm";
			this.Text = "Admin - User - Roles";
			this.Resize += new System.EventHandler(this.AdminUserRolesForm_Resize);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.AdminUserRolesForm_Closing);
			this.Load += new System.EventHandler(this.AdminUserRolesForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.RolesGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RolesFunctionsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RolesUsersGrid)).EndInit();
			this.ResumeLayout(false);

		}
    #endregion
   
    private void RolesGridLoad()
    {
      try
      {
        ds = mainForm.AdminAgent.UserRoleFunctionsGet((short) mainForm.UtcOffset);
        RolesGrid.SetDataBinding(ds, "Roles", true);
        RolesGrid.ChildGrid  = RolesFunctionsGrid;
        RolesFunctionsGrid.SetDataBinding(ds, "Roles.RolesRoleFunctions", true);
        FunctionsRadio.Checked = true;
      }
      catch
      {      
        throw;
      }
    }
    
    private void AdminUserRolesForm_Load(object sender, System.EventArgs e)
    {
      int height = mainForm.Height / 2;
      int width  = mainForm.Width - 350;
    
      mainForm.Alert("Please wait... Loading current user role data...", PilotState.Unknown);
      this.Cursor = Cursors.WaitCursor;
      
      try
      {
        this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
        this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
        this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
        this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

        this.Show();
        this.Refresh();               

        RolesUsersGrid.Width = RolesGrid.Width - 65;
        RolesFunctionsGrid.Width = RolesGrid.Width - 65;                        

        RolesGridLoad();

        mainForm.Alert("Loading current user role data... Done!", PilotState.Normal); 
      }
      catch (Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminUserRolesForm.AdminUserRolesForm_Load]", 1);
      }

      this.Cursor = Cursors.Default;
    }
    
    #region RolesGrid
    private void RolesGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
    {
      if (RolesGrid.Columns[e.ColIndex].DataField.Equals("RoleCode"))
      {
        if (!RolesGrid.Columns[e.ColIndex].Text.Equals(""))
        {
          mainForm.Alert("You may not edit this cell.", PilotState.RunFault);
          RolesGrid.Col = e.ColIndex + 1;
          e.Cancel = true;
          return;
        }
      }
    }

    private void RolesGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {
      switch(RolesGrid.Columns[e.ColIndex].DataField)
      {
        case("ActTime"):          
          e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeShortFormat);
          break;
      }
    }

    private void RolesGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      if(RolesGrid.Row == (RolesGrid.Splits[0].Rows.Count-1))
      {
        RolesGrid.Col = 0;
        RolesGrid.Row = 0;
        e.Cancel = true;
        return;
      }

      if(RolesGrid.Columns["RoleCode"].Text.Equals(""))
      {
        mainForm.Alert("RoleCode may not be blank.", PilotState.RunFault);
        RolesGrid.Col = xRoleCode;
        e.Cancel = true;
        return;
      }
  
      try
      {
        mainForm.AdminAgent.RoleSet(
          RolesGrid.Columns["RoleCode"].Text,  
          RolesGrid.Columns["Role"].Text, 
          RolesGrid.Columns["Comment"].Text,  
          mainForm.UserId,
          false
          );

        RolesGrid.Columns["ActUserShortName"].Text = mainForm.UserId;
        RolesGrid.Columns["ActTime"].Text = DateTime.Now.ToString();
        
        ds = mainForm.AdminAgent.UserRolesGet((short) mainForm.UtcOffset);
        
        RolesGrid.SetDataBinding(ds, "Roles", true);
        RolesFunctionsGrid.SetDataBinding(ds, "Roles.FunctionsForRoles", true);
        RolesUsersGrid.SetDataBinding(ds, "Roles.RolesForUsers", true);
        
        mainForm.Alert("Role " + RolesGrid.Columns["RoleCode"].Text + " has been updated.", PilotState.Normal); 
      }
      catch (Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminUserRolesForm.RolesGrid_BeforeUpdate]", 1);
        return;
      }
    }

    #endregion

    #region RolesFunctionsGrid Grid
    private void RolesFunctionsGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {    
      switch (RolesFunctionsGrid.Columns[e.ColIndex].DataField)
      {
        case("ActTime"):
          e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeShortFormat);          
          break;
      }
    }

    private void FunctionsRadio_Click(object sender, System.EventArgs e)
    {
      bool exapndChild = false;

      try
      {
        RolesFunctionsGrid.SetDataBinding(ds, "Roles.FunctionsForRoles", true);
        FunctionsRadio.Checked = true;
      }
      catch (Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminUserRolesForm.FunctionsRadio_Click]",  Log.Error,  1);        
      }
      
      if(RolesUsersGrid.Visible)
      {
        RolesGrid.CollapseChild();
        exapndChild = true;
      }
      
      RolesGrid.ChildGrid  = RolesFunctionsGrid;
      RolesFunctionsGrid.SetDataBinding(ds, "Roles.FunctionsForRoles", true);
      
      RolesFunctionsGrid.Height = RolesGrid.Height - 70;

      if(exapndChild)
      {
        RolesGrid.ExpandChild();
      }
    }

    private void RolesFunctionsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      { 
        mainForm.AdminAgent.RoleFunctionSet(
          RolesFunctionsGrid.Columns["RoleCode"].Text, 
          RolesFunctionsGrid.Columns["FunctionPath"].Text, 
      (RolesFunctionsGrid.Columns["MayView"].Value != DBNull.Value) ? bool.Parse(RolesFunctionsGrid.Columns["MayView"].Text) : false,
      (RolesFunctionsGrid.Columns["MayEdit"].Value != DBNull.Value) ? bool.Parse(RolesFunctionsGrid.Columns["MayEdit"].Text) : false,
          RolesFunctionsGrid.Columns["BookGroupList"].Text,
      RolesFunctionsGrid.Columns["Comment"].Text, 
          mainForm.UserId
          );
      
        RolesFunctionsGrid.Columns["ActUserShortName"].Text = mainForm.UserId;
        RolesFunctionsGrid.Columns["ActTime"].Text = DateTime.Now.ToString();     

        mainForm.Alert("Role " +  RolesFunctionsGrid.Columns["RoleCode"].Text +" Function " + RolesFunctionsGrid.Columns["FunctionPath"].Text+ " was updated.", PilotState.Normal);
      }
      
      catch(Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminUserRolesForm.RolesFunctionsGrid_BeforeUpdate]", 1);

        e.Cancel = true;
        return;
      }
    }

    #endregion

    #region UserRoles Grid
    private void RolesUsersGrid_FormatText(object sender,  C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {
      switch (RolesUsersGrid.Columns[e.ColIndex].DataField)
      {
        case("ActTime"):
          e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeShortFormat);
          break;
      }
    }

    private void UserRolesRadio_Click(object sender,  System.EventArgs e)
    {
      bool expandChild = false;
      
      if(RolesFunctionsGrid.Visible)
      {
        RolesGrid.CollapseChild();
        expandChild = true;
      }
      
      RolesGrid.ChildGrid = RolesUsersGrid;
      RolesUsersGrid.SetDataBinding(ds, "Roles.RolesUserRoles", true);
      
      RolesUsersGrid.Height = RolesGrid.Height - 70;

      if(expandChild)
      {
        RolesGrid.ExpandChild();
      }
    }

    private void RolesUsersGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        mainForm.AdminAgent.UserRoleSet(
          RolesUsersGrid.Columns["UserId"].Text, 
          RolesUsersGrid.Columns["RoleCode"].Text,          
          RolesUsersGrid.Columns["Comment"].Text, 
          mainForm.UserId,
          false
          );

        mainForm.Alert("Role User " +  RolesUsersGrid.Columns["UserId"].Text + " updated", PilotState.Normal);

        RolesUsersGrid.Columns["ActUserShortName"].Text = mainForm.UserId;
        RolesUsersGrid.Columns["ActTime"].Text = DateTime.Now.ToString();

      }
      catch(Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminUserRolesForm.RolesUsersGrid_BeforeUpdate]", 1);
        e.Cancel = true;
      }
    }
  
    #endregion

    private void AdminUserRolesForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if(this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
        RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
        RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
        RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
      }
    }

    private void AdminUserRolesForm_Resize(object sender, System.EventArgs e)
    {
    RolesUsersGrid.Width = RolesGrid.Width - 65;
    RolesUsersGrid.Height = RolesGrid.Height - 70;

    RolesFunctionsGrid.Width = RolesGrid.Width - 65;      
    RolesFunctionsGrid.Height = RolesGrid.Height - 70;
  }

    private void RolesGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      if ((e.KeyChar == 13))
      {
        if ((RolesGrid.DataChanged))
        {
          RolesGrid.UpdateData();
        }

        if ((RolesGrid.Splits[0].Rows.Count) > RolesGrid.Row)
        {
          RolesGrid.Col = 0;
        }

        return;
      }

			string gridData = "";

			if (e.KeyChar.Equals((char)3) && RolesGrid.SelectedRows.Count > 0)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RolesGrid.SelectedCols)
				{
					gridData += dataColumn.Caption + "\t";
				}

				gridData += "\n";

				foreach (int row in RolesGrid.SelectedRows)
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RolesGrid.SelectedCols)
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
				mainForm.Alert("Copied " + RolesGrid.SelectedRows.Count.ToString("#,##0") + " rows to the clipboard.");
				e.Handled = true;
			}
    }

    private void RolesFunctionsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      if ((e.KeyChar == 13))
      {
        if ((RolesFunctionsGrid.DataChanged))
        {
          RolesFunctionsGrid.UpdateData();
        }

        if ((RolesFunctionsGrid.Splits[0].Rows.Count) > RolesFunctionsGrid.Row)
        {
          RolesFunctionsGrid.Col = 0;
        }

        return;
      }
    }

    private void RolesUsersGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      if ((e.KeyChar == 13))
      {
        if ((RolesUsersGrid.DataChanged))
        {
          RolesUsersGrid.UpdateData();
        }

        if ((RolesUsersGrid.Splits[0].Rows.Count) > RolesUsersGrid.Row)
        {
          RolesUsersGrid.Row += 1;
          RolesUsersGrid.Col = 0;
        }
        return;
      }
    }

    private void RolesGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        mainForm.Alert("Role " +  RolesUsersGrid.Columns["RoleCode"].Text + " removed", PilotState.Normal);

        mainForm.AdminAgent.RoleSet(
          RolesGrid.Columns["RoleCode"].Text,  
          RolesGrid.Columns["Role"].Text, 
          RolesGrid.Columns["Comment"].Text,  
          mainForm.UserId,
          true
          );              
      }
      catch(Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminUserRolesForm.RolesUsersGrid_BeforeUpdate]", 1);
        e.Cancel = true;
      }
    }

		private void RolesFunctionsGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
		{
			if (e.Column.DataColumn.DataField.Equals("MayEdit"))
			{
				if(RolesFunctionsGrid.Columns["MayEdit"].CellValue(RolesFunctionsGrid.Row) == DBNull.Value)
				{
					e.Cancel = true;
					return;
				}
			}
			else if (e.Column.DataColumn.DataField.Equals("MayView"))
			{
				if(RolesFunctionsGrid.Columns["MayView"].CellValue(RolesFunctionsGrid.Row) == DBNull.Value)
				{
					e.Cancel = true;
					return;
				}
			}
		}

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RolesGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\r\n";

			foreach (int row in RolesGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RolesGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\r\n";
			}

			Clipboard.SetDataObject(gridData, true);

			mainForm.Alert("Total: " + RolesGrid.SelectedRows.Count + " items copied to the clipboard.", PilotState.Normal);
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n";

			if (RolesGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows to copy.", PilotState.Normal);
				return;
			}

			try
			{
				maxTextLength = new int[RolesGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RolesGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in RolesGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RolesGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RolesGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RolesGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in RolesGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RolesGrid.SelectedCols)
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

				mainForm.Alert("Total: " + RolesGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
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
			excel.ExportGridToExcel(ref RolesGrid);

			this.Cursor = Cursors.Default;
		}
  }
}