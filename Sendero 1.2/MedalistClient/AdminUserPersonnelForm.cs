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
  public class AdminUserPersonnelForm : System.Windows.Forms.Form
  {
    private MainForm mainForm;    
    private int xRoleCode = 0;
    private int xUserId = 0;
    private int xShortName = 1;    
    
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid UserGrid;
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid UserRolesGrid;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep2MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;

    private System.ComponentModel.Container components = null;
    
    public AdminUserPersonnelForm(MainForm mainForm)
    {
      this.mainForm = mainForm;
      InitializeComponent();

      try
      {
        if (mainForm.AdminAgent.MayEdit(mainForm.UserId, "AdminUser"))
        {
          UserGrid.AllowUpdate = true;
          UserGrid.AllowDelete = true;
          UserGrid.AllowAddNew = true;

          UserRolesGrid.AllowUpdate = true;
          UserRolesGrid.AllowDelete = true;
          UserRolesGrid.AllowAddNew = true;
        }
      }
      catch (Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminUserPersonnelForm.AdminUserPersonnelForm]", 1);
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
  
    private void InitializeComponent()
    {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AdminUserPersonnelForm));
			this.UserGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.UserRolesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.UserGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UserRolesGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// UserGrid
			// 
			this.UserGrid.AllowColMove = false;
			this.UserGrid.AllowColSelect = false;
			this.UserGrid.AllowUpdate = false;
			this.UserGrid.CaptionHeight = 18;
			this.UserGrid.ChildGrid = this.UserRolesGrid;
			this.UserGrid.ContextMenu = this.MainContextMenu;
			this.UserGrid.Cursor = System.Windows.Forms.Cursors.Default;
			this.UserGrid.DefColWidth = 183;
			this.UserGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.UserGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.UserGrid.EmptyRows = true;
			this.UserGrid.ExtendRightColumn = true;
			this.UserGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.UserGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.UserGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.UserGrid.Location = new System.Drawing.Point(0, 0);
			this.UserGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.UserGrid.Name = "UserGrid";
			this.UserGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.UserGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.UserGrid.PreviewInfo.ZoomFactor = 75;
			this.UserGrid.RecordSelectorWidth = 16;
			this.UserGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.UserGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.UserGrid.RowHeight = 15;
			this.UserGrid.RowSubDividerColor = System.Drawing.Color.WhiteSmoke;
			this.UserGrid.Size = new System.Drawing.Size(1192, 373);
			this.UserGrid.TabAcrossSplits = true;
			this.UserGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.UserGrid.TabIndex = 0;
			this.UserGrid.Text = "UserGrid";
			this.UserGrid.WrapCellPointer = true;
			this.UserGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.UserGrid_BeforeColEdit);
			this.UserGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.UserGrid_BeforeUpdate);
			this.UserGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.UserGrid_BeforeDelete);
			this.UserGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.UserGrid_FormatText);
			this.UserGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserGrid_KeyPress);
			this.UserGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"User ID\" Da" +
				"taField=\"UserId\" DataWidth=\"50\" SortDirection=\"Descending\"><ValueItems /><GroupI" +
				"nfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Short Name\" DataField=\"Sho" +
				"rtName\" DataWidth=\"12\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"Email\" DataField=\"Email\"><ValueItems /><GroupInfo /></C1DataCo" +
				"lumn><C1DataColumn Level=\"0\" Caption=\"Comment\" DataField=\"Comment\"><ValueItems /" +
				"><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Actor\" DataField=\"" +
				"ActUserShortName\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"Act Time\" DataField=\"ActTime\" NumberFormat=\"FormatText Event\"><Valu" +
				"eItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Last Acces" +
				"s\" DataField=\"LastAccess\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Usage Count\" DataField=\"Usa" +
				"geCount\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.W" +
				"in.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightTex" +
				"t;BackColor:Highlight;}Inactive{ForeColor:InactiveCaptionText;BackColor:Inactive" +
				"Caption;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{Locked:Tru" +
				"e;}FilterBar{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;Fore" +
				"Color:ControlText;AlignVert:Center;}Style18{}Style19{}Style14{}Style15{}Style16{" +
				"}Style17{}Style10{}Style11{}Style12{}Style13{AlignHorz:Near;}Style27{}Style29{Al" +
				"ignHorz:Near;}Style28{}Style26{}Style25{AlignHorz:Near;}Style9{}Style8{AlignHorz" +
				":Near;}Style24{}Style23{}Style5{}Style4{}Style7{AlignHorz:Near;}Style6{}Style1{A" +
				"lignHorz:Near;}Style22{}Style3{}Style2{AlignHorz:Near;}Style21{}Style20{}OddRow{" +
				"}Style38{}Style39{}Style36{}Style37{}Style34{AlignHorz:Near;}Style35{AlignHorz:N" +
				"ear;}Style32{}Style33{}Style30{}Style49{}Style48{}Style31{}Normal{Font:Verdana, " +
				"8.25pt;}Style41{AlignHorz:Near;}Style40{AlignHorz:Near;}Style43{}Style42{}Style4" +
				"5{}Style44{}Style47{AlignHorz:Near;}Style46{AlignHorz:Near;}EvenRow{BackColor:Li" +
				"ghtCyan;}Style59{AlignHorz:Near;}Style58{AlignHorz:Near;}RecordSelector{AlignIma" +
				"ge:Center;}Style51{}Style50{}Footer{}Style52{AlignHorz:Near;}Style53{AlignHorz:N" +
				"ear;}Style54{}Style55{}Style56{}Style57{}Caption{AlignHorz:Center;}Style63{}Styl" +
				"e62{}Style61{}Style60{}Style65{}Style64{}Group{AlignVert:Center;Border:None,,0, " +
				"0, 0, 0;BackColor:ControlDark;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.Merg" +
				"eView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColMove=\"False\" AllowColSelect=\"F" +
				"alse\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17" +
				"\" ExtendRightColumn=\"True\" MarqueeStyle=\"SolidCellBorder\" RecordSelectorWidth=\"1" +
				"6\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><Captio" +
				"nStyle parent=\"Heading\" me=\"Style25\" /><EditorStyle parent=\"Editor\" me=\"Style17\"" +
				" /><EvenRowStyle parent=\"EvenRow\" me=\"Style23\" /><FilterBarStyle parent=\"FilterB" +
				"ar\" me=\"Style28\" /><FooterStyle parent=\"Footer\" me=\"Style19\" /><GroupStyle paren" +
				"t=\"Group\" me=\"Style27\" /><HeadingStyle parent=\"Heading\" me=\"Style18\" /><HighLigh" +
				"tRowStyle parent=\"HighlightRow\" me=\"Style22\" /><InactiveStyle parent=\"Inactive\" " +
				"me=\"Style21\" /><OddRowStyle parent=\"OddRow\" me=\"Style24\" /><RecordSelectorStyle " +
				"parent=\"RecordSelector\" me=\"Style26\" /><SelectedStyle parent=\"Selected\" me=\"Styl" +
				"e20\" /><Style parent=\"Normal\" me=\"Style16\" /><internalCols><C1DisplayColumn><Hea" +
				"dingStyle parent=\"Style18\" me=\"Style1\" /><Style parent=\"Style16\" me=\"Style2\" /><" +
				"FooterStyle parent=\"Style19\" me=\"Style3\" /><EditorStyle parent=\"Style17\" me=\"Sty" +
				"le4\" /><GroupHeaderStyle parent=\"Style16\" me=\"Style6\" /><GroupFooterStyle parent" +
				"=\"Style16\" me=\"Style5\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single<" +
				"/ColumnDivider><Width>200</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayC" +
				"olumn><C1DisplayColumn><HeadingStyle parent=\"Style18\" me=\"Style13\" /><Style pare" +
				"nt=\"Style16\" me=\"Style29\" /><FooterStyle parent=\"Style19\" me=\"Style30\" /><Editor" +
				"Style parent=\"Style17\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style16\" me=\"Sty" +
				"le33\" /><GroupFooterStyle parent=\"Style16\" me=\"Style32\" /><Visible>True</Visible" +
				"><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>150</Width><Height>15</He" +
				"ight><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
				"yle18\" me=\"Style34\" /><Style parent=\"Style16\" me=\"Style35\" /><FooterStyle parent" +
				"=\"Style19\" me=\"Style36\" /><EditorStyle parent=\"Style17\" me=\"Style37\" /><GroupHea" +
				"derStyle parent=\"Style16\" me=\"Style39\" /><GroupFooterStyle parent=\"Style16\" me=\"" +
				"Style38\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider" +
				"><Width>150</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style18\" me=\"Style40\" /><Style parent=\"Style16\" m" +
				"e=\"Style41\" /><FooterStyle parent=\"Style19\" me=\"Style42\" /><EditorStyle parent=\"" +
				"Style17\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style16\" me=\"Style45\" /><Group" +
				"FooterStyle parent=\"Style16\" me=\"Style44\" /><Visible>True</Visible><ColumnDivide" +
				"r>Gainsboro,Single</ColumnDivider><Width>150</Width><Height>15</Height><DCIdx>3<" +
				"/DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style18\" me=\"Sty" +
				"le46\" /><Style parent=\"Style16\" me=\"Style47\" /><FooterStyle parent=\"Style19\" me=" +
				"\"Style48\" /><EditorStyle parent=\"Style17\" me=\"Style49\" /><GroupHeaderStyle paren" +
				"t=\"Style16\" me=\"Style51\" /><GroupFooterStyle parent=\"Style16\" me=\"Style50\" /><Vi" +
				"sible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>150</W" +
				"idth><Height>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCId" +
				"x>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style18\" me=" +
				"\"Style52\" /><Style parent=\"Style16\" me=\"Style53\" /><FooterStyle parent=\"Style19\"" +
				" me=\"Style54\" /><EditorStyle parent=\"Style17\" me=\"Style55\" /><GroupHeaderStyle p" +
				"arent=\"Style16\" me=\"Style57\" /><GroupFooterStyle parent=\"Style16\" me=\"Style56\" /" +
				"><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>12" +
				"0</Width><Height>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><" +
				"DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style18\"" +
				" me=\"Style7\" /><Style parent=\"Style16\" me=\"Style8\" /><FooterStyle parent=\"Style1" +
				"9\" me=\"Style9\" /><EditorStyle parent=\"Style17\" me=\"Style10\" /><GroupHeaderStyle " +
				"parent=\"Style16\" me=\"Style12\" /><GroupFooterStyle parent=\"Style16\" me=\"Style11\" " +
				"/><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>1" +
				"20</Width><Height>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked>" +
				"<DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style18" +
				"\" me=\"Style58\" /><Style parent=\"Style16\" me=\"Style59\" /><FooterStyle parent=\"Sty" +
				"le19\" me=\"Style60\" /><EditorStyle parent=\"Style17\" me=\"Style61\" /><GroupHeaderSt" +
				"yle parent=\"Style16\" me=\"Style63\" /><GroupFooterStyle parent=\"Style16\" me=\"Style" +
				"62\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Hei" +
				"ght>15</Height><AllowFocus>False</AllowFocus><Locked>True</Locked><DCIdx>7</DCId" +
				"x></C1DisplayColumn></internalCols><ClientRect>0, 0, 1188, 369</ClientRect><Bord" +
				"erSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Styl" +
				"e parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"H" +
				"eading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Head" +
				"ing\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Norma" +
				"l\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Norma" +
				"l\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" m" +
				"e=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Capt" +
				"ion\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSpl" +
				"its><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientA" +
				"rea>0, 0, 1188, 369</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style64\" /><" +
				"PrintPageFooterStyle parent=\"\" me=\"Style65\" /></Blob>";
			// 
			// UserRolesGrid
			// 
			this.UserRolesGrid.AllowColMove = false;
			this.UserRolesGrid.AllowColSelect = false;
			this.UserRolesGrid.AllowUpdate = false;
			this.UserRolesGrid.CaptionHeight = 17;
			this.UserRolesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.UserRolesGrid.EmptyRows = true;
			this.UserRolesGrid.ExtendRightColumn = true;
			this.UserRolesGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.UserRolesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.UserRolesGrid.Location = new System.Drawing.Point(24, 48);
			this.UserRolesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.UserRolesGrid.Name = "UserRolesGrid";
			this.UserRolesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.UserRolesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.UserRolesGrid.PreviewInfo.ZoomFactor = 75;
			this.UserRolesGrid.RecordSelectorWidth = 16;
			this.UserRolesGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.UserRolesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.UserRolesGrid.RowHeight = 15;
			this.UserRolesGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
			this.UserRolesGrid.Size = new System.Drawing.Size(616, 112);
			this.UserRolesGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.UserRolesGrid.TabIndex = 3;
			this.UserRolesGrid.TabStop = false;
			this.UserRolesGrid.Text = "User Roles Grid";
			this.UserRolesGrid.WrapCellPointer = true;
			this.UserRolesGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.UserRolesGrid_BeforeColEdit);
			this.UserRolesGrid.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.UserRolesGrid_AfterColEdit);
			this.UserRolesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.UserRolesGrid_BeforeUpdate);
			this.UserRolesGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.UserRolesGrid_BeforeDelete);
			this.UserRolesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.UserRolesGrid_FormatText);
			this.UserRolesGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserRolesGrid_KeyPress);
			this.UserRolesGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"User Id\" Da" +
				"taField=\"UserId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"" +
				"0\" Caption=\"Role Code\" DataField=\"RoleCode\"><ValueItems /><GroupInfo /></C1DataC" +
				"olumn><C1DataColumn Level=\"0\" Caption=\"Comment\" DataField=\"Comment\"><ValueItems " +
				"/><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataFie" +
				"ld=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1Data" +
				"Column><C1DataColumn Level=\"0\" Caption=\"Actor\" DataField=\"ActUserShortName\"><Val" +
				"ueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGr" +
				"id.Design.ContextWrapper\"><Data>RecordSelector{AlignImage:Center;}Caption{AlignH" +
				"orz:Center;}Normal{Font:Verdana, 8.25pt;}Selected{ForeColor:HighlightText;BackCo" +
				"lor:Highlight;}Editor{}Style18{}Style19{}Style14{AlignHorz:Near;}Style15{AlignHo" +
				"rz:Near;}Style16{}Style17{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}St" +
				"yle44{AlignHorz:Near;}Style47{}Style38{AlignHorz:Near;}Style37{}Style34{}Style35" +
				"{}Style32{AlignHorz:Near;}Style33{AlignHorz:Near;}OddRow{}Style2{}Style27{}Style" +
				"26{}Style25{}Footer{}Style23{}Style22{}Style21{AlignHorz:Near;}Style20{AlignHorz" +
				":Near;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}EvenRow" +
				"{BackColor:LightCyan;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1" +
				", 1;ForeColor:ControlText;AlignVert:Center;}Style49{}Style48{}Style24{}Style9{}S" +
				"tyle41{}Style40{}Style43{}FilterBar{}Style45{AlignHorz:Near;}Style42{}Style4{}St" +
				"yle46{}Style8{}Style39{AlignHorz:Near;}Style36{}Style5{}Group{AlignVert:Center;B" +
				"order:None,,0, 0, 0, 0;BackColor:ControlDark;}Style7{}Style6{}Style1{}Style3{}Hi" +
				"ghlightRow{ForeColor:HighlightText;BackColor:Highlight;}</Data></Styles><Splits>" +
				"<C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" AllowColMove=\"False\" AllowColSel" +
				"ect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeig" +
				"ht=\"17\" ExtendRightColumn=\"True\" MarqueeStyle=\"SolidCellBorder\" RecordSelectorWi" +
				"dth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><" +
				"CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Sty" +
				"le5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"Filt" +
				"erBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle par" +
				"ent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLig" +
				"htRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" " +
				"me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle pa" +
				"rent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6" +
				"\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><Heading" +
				"Style parent=\"Style2\" me=\"Style14\" /><Style parent=\"Style1\" me=\"Style15\" /><Foot" +
				"erStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"Style17\"" +
				" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><GroupFooterStyle parent=\"St" +
				"yle1\" me=\"Style18\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</H" +
				"eight><AllowSizing>False</AllowSizing><DCIdx>0</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style2\" me=\"Style20\" /><Style parent=\"Style1\" me=" +
				"\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Style22\" /><EditorStyle parent=\"Sty" +
				"le5\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style25\" /><GroupFoote" +
				"rStyle parent=\"Style1\" me=\"Style24\" /><Visible>True</Visible><ColumnDivider>Gain" +
				"sboro,Single</ColumnDivider><Width>150</Width><Height>15</Height><DCIdx>1</DCIdx" +
				"></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style32\" /" +
				"><Style parent=\"Style1\" me=\"Style33\" /><FooterStyle parent=\"Style3\" me=\"Style34\"" +
				" /><EditorStyle parent=\"Style5\" me=\"Style35\" /><GroupHeaderStyle parent=\"Style1\"" +
				" me=\"Style37\" /><GroupFooterStyle parent=\"Style1\" me=\"Style36\" /><Visible>True</" +
				"Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>150</Width><Height" +
				">15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style44\" /><Style parent=\"Style1\" me=\"Style45\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style46\" /><EditorStyle parent=\"Style5\" me=\"Style47\" /><Group" +
				"HeaderStyle parent=\"Style1\" me=\"Style49\" /><GroupFooterStyle parent=\"Style1\" me=" +
				"\"Style48\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivide" +
				"r><Width>150</Width><Height>15</Height><AllowFocus>False</AllowFocus><DCIdx>4</D" +
				"CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style3" +
				"8\" /><Style parent=\"Style1\" me=\"Style39\" /><FooterStyle parent=\"Style3\" me=\"Styl" +
				"e40\" /><EditorStyle parent=\"Style5\" me=\"Style41\" /><GroupHeaderStyle parent=\"Sty" +
				"le1\" me=\"Style43\" /><GroupFooterStyle parent=\"Style1\" me=\"Style42\" /><Visible>Tr" +
				"ue</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>120</Width><He" +
				"ight>15</Height><AllowFocus>False</AllowFocus><DCIdx>3</DCIdx></C1DisplayColumn>" +
				"</internalCols><ClientRect>0, 0, 612, 108</ClientRect><BorderSide>0</BorderSide>" +
				"</C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Norma" +
				"l\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /" +
				"><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" />" +
				"<Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Sty" +
				"le parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><St" +
				"yle parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" />" +
				"<Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></N" +
				"amedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified<" +
				"/Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 612, 108</C" +
				"lientArea><PrintPageHeaderStyle parent=\"\" me=\"Style26\" /><PrintPageFooterStyle p" +
				"arent=\"\" me=\"Style27\" /></Blob>";
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
			// AdminUserPersonnelForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1192, 373);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.UserRolesGrid);
			this.Controls.Add(this.UserGrid);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.HelpButton = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AdminUserPersonnelForm";
			this.Text = "Admin  - User - Personnel";
			this.Resize += new System.EventHandler(this.AdminUserPersonnelForm_Resize);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.AdminUserPersonnelForm_Closing);
			this.Load += new System.EventHandler(this.AdminUserPersonnelForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.UserGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UserRolesGrid)).EndInit();
			this.ResumeLayout(false);

		}
    #endregion
      
    private void UserGridLoad()
    {    
      try 
      {
        DataSet ds = mainForm.AdminAgent.UserRolesGet((short)mainForm.UtcOffset);
        
        UserGrid.SetDataBinding(ds, "Users", true);
        UserRolesGrid.SetDataBinding(ds, "Users.UsersUserRoles", true);
        
        DataSet rolesSet = mainForm.AdminAgent.UserRoleFunctionsGet((short) mainForm.UtcOffset);

        foreach (DataRow row in rolesSet.Tables["Roles"].Rows)
        {
          C1.Win.C1TrueDBGrid.ValueItem temp = new C1.Win.C1TrueDBGrid.ValueItem(row[0].ToString(), row[0].ToString());
          UserRolesGrid.Columns["RoleCode"].ValueItems.Values.Add(temp);
        }
      }
      catch
      {
        throw;
      }
    }    
    
    #region UserGrid

    private void UserGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      string gridData = "";

      if (e.KeyChar.Equals((char)13) && !UserGrid.DataChanged)
      {        
          UserGrid.UpdateData();
          e.Handled = true;       
      }
      
      if (e.KeyChar.Equals((char)13) && !UserGrid.DataChanged)
      {
        UserGrid.MoveLast();
        UserGrid.Row += 1;
        UserGrid.Col = 0;

        e.Handled = true;
      }

      if (e.KeyChar.Equals((char)3) && UserGrid.SelectedRows.Count > 0)
      {
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in UserGrid.SelectedCols)
        {
          gridData += dataColumn.Caption + "\t";
        }

        gridData += "\n";

        foreach (int row in UserGrid.SelectedRows)
        {
          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in UserGrid.SelectedCols)
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
        mainForm.Alert("Copied " + UserGrid.SelectedRows.Count.ToString("#,##0") + " rows to the clipboard.");
        e.Handled = true;
      }
    }

    private void UserGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        mainForm.AdminAgent.UserSet(
          UserGrid.Columns["UserId"].Text, 
          UserGrid.Columns["ShortName"].Text,
          UserGrid.Columns["Email"].Text,
          UserGrid.Columns["Comment"].Text,
          mainForm.UserId,
          false);

        mainForm.Alert("User " + UserGrid.Columns["UserId"].Text + " has been set to inactive", PilotState.Normal);      
      }
      catch (Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminUserPersonnelForm.UserGrid_BeforeDelete]", 1);

        e.Cancel = true;
        return;
      }
    }

    private void UserGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {    
      switch (UserGrid.Columns[e.ColIndex].DataField)
      {
        case("ActTime"):
        case("LastAccess"):
          e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeShortFormat);
          break;
      }
    }

    private void UserGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        if (UserGrid.Row == (UserGrid.Splits[0].Rows.Count - 1))
        {
          UserGrid.Col = xUserId;
          UserGrid.Row = 0;
          e.Cancel = true;
          return;
        }
        
        if (UserGrid.Columns["UserId"].Text.Equals(""))
        {
          mainForm.Alert("User ID does not exist.", PilotState.RunFault);;
          UserGrid.Col = xUserId;
          e.Cancel = true;
          return;
        }

        if (UserGrid.Columns["ShortName"].Text.Equals(""))
        {
          mainForm.Alert("Short Name does not exist.", PilotState.RunFault);;
          UserGrid.Col = xShortName;
          e.Cancel = true;
          return;
        }

        mainForm.AdminAgent.UserSet(
          UserGrid.Columns["UserId"].Text,
          UserGrid.Columns["ShortName"].Text,
          UserGrid.Columns["Email"].Text,
          UserGrid.Columns["Comment"].Text,
          mainForm.UserId,
          true);
      
        UserGrid.Columns["ActUserShortName"].Text = mainForm.UserId;
        UserGrid.Columns["ActTime"].Text = DateTime.Now.ToString();

        mainForm.Alert("User " + UserGrid.Columns["UserId"].Text + " has been updated", PilotState.Normal);              
        
        UserGrid.Col = xUserId;
      
        UserGridLoad();
      }
      catch (Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminUserPersonnelForm.UserGrid_BeforeUpdate]", 1);
        
        e.Cancel = true;
        return;
      }
    }
    
    private void UserGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
    {
      if ((UserGrid.Columns[e.ColIndex].DataField.Equals("UserId")))
      {
        if (!UserGrid.Columns[e.ColIndex].Text.Equals(""))
        {
          e.Cancel = true;
          mainForm.Alert("You may not edit this cell", PilotState.RunFault);;
          UserGrid.Col = e.ColIndex + 1;
          return;
        }
      }
    }
    #endregion   

    #region UserRolesGrid
    
    private void UserRolesGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      string gridData = "";

      if (e.KeyChar.Equals((char)13) && !UserRolesGrid.DataChanged)
      {        
        UserRolesGrid.UpdateData();
        e.Handled = true;       
      }
      
      if (e.KeyChar.Equals((char)13) && !UserRolesGrid.DataChanged)
      {
        UserRolesGrid.MoveLast();
        UserRolesGrid.Row += 1;
        UserRolesGrid.Col = 0;

        e.Handled = true;
      }

      if (e.KeyChar.Equals((char)3) && UserRolesGrid.SelectedRows.Count > 0)
      {
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in UserRolesGrid.SelectedCols)
        {
          gridData += dataColumn.Caption + "\t";
        }

        gridData += "\n";

        foreach (int row in UserRolesGrid.SelectedRows)
        {
          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in UserRolesGrid.SelectedCols)
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
        mainForm.Alert("Copied " + UserRolesGrid.SelectedRows.Count.ToString("#,##0") + " rows to the clipboard.");
        e.Handled = true;
      }
    }

    private void UserRolesGrid_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
    {
      UserRolesGrid.Columns["RoleCode"].ValueItems.Presentation  = C1.Win.C1TrueDBGrid.PresentationEnum.Normal;
      UserRolesGrid.Columns["RoleCode"].ValueItems.Translate = false;
    }

    private void UserRolesGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {      
      switch (UserRolesGrid.Columns[e.ColIndex].DataField)
      {
        case("ActTime"):
          e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeShortFormat);
          break;
      }
    }
   
    private void UserRolesGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
    {
      if (UserRolesGrid.Columns[e.ColIndex].DataField.Equals("RoleCode"))
      {
        if (UserRolesGrid.Columns[e.ColIndex].Text.Equals(""))
        {
          UserRolesGrid.Columns["RoleCode"].ValueItems.Presentation  = C1.Win.C1TrueDBGrid.PresentationEnum.SortedComboBox;
          UserRolesGrid.Columns["RoleCode"].ValueItems.DefaultItem = 0;
          UserRolesGrid.Columns["RoleCode"].ValueItems.Validate = true;
        }
        else
        {         
          UserRolesGrid.Col += 1;
          e.Cancel = true;
        }
      }
    }

    private void UserRolesGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      { 
        if (UserRolesGrid.Row == (UserRolesGrid.Splits[0].Rows.Count))
        {
          UserRolesGrid.Col  = 0;
          UserRolesGrid.Row += 1;
          e.Cancel = true;
          return;
        }
        
        if (UserRolesGrid.Columns["RoleCode"].Text.Equals(""))
        {
          UserRolesGrid.Col = xRoleCode;
          mainForm.Alert("Role code cannot be left blank", PilotState.RunFault);
          e.Cancel = true;
          return;
        }

        mainForm.AdminAgent.UserRoleSet(
          UserRolesGrid.Columns["UserId"].Text,
          UserRolesGrid.Columns["RoleCode"].Text,
          UserRolesGrid.Columns["Comment"].Text,
          mainForm.UserId,
          false
          );

        UserRolesGrid.Columns["ActUserShortName"].Text = mainForm.UserId;
        UserRolesGrid.Columns["ActTime"].Text = DateTime.Now.ToString();

        mainForm.Alert("User " + UserRolesGrid.Columns["UserId"].Text + " has " + UserRolesGrid.Columns["RoleCode"].Text + " privledges", PilotState.Normal);
      
        UserRolesGrid.Col = xRoleCode;        
      }
      catch (Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminUserPersonnelForm.UserRolesGrid_BeforeUpdate]", 1);

        e.Cancel = true;
        return;
      }
    }

    #endregion

    private void AdminUserPersonnelForm_Load(object sender,  System.EventArgs e)
    {
      mainForm.Alert("Please wait... Loading current user data...", PilotState.Unknown);
      this.Cursor = Cursors.WaitCursor;
      
      try
       {
        this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
        this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
        this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", this.Height.ToString()));
        this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", this.Width.ToString()));

        UserRolesGrid.Width = UserGrid.Width - 65;
          
        UserGridLoad();
        
        mainForm.Alert("Loading current user data... Done!", PilotState.Normal); 
      }      
      catch (Exception error)
      {
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [AdminUserPersonnelForm.AdminUserPersonnelForm_Load]" , Log.Error, 1);
      }

      this.Cursor = Cursors.Default;
    }

    private void AdminUserPersonnelForm_Closing(object sender,  System.ComponentModel.CancelEventArgs e)
    {
      if(this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
        RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
        RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
        RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
      }
    }

    private void AdminUserPersonnelForm_Resize(object sender, System.EventArgs e)
    {
      UserRolesGrid.Width = UserGrid.Width - 65;
      UserRolesGrid.Height = UserGrid.Height - 70;
    }

		private void UserRolesGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				mainForm.AdminAgent.UserRoleSet(
					UserRolesGrid.Columns["UserId"].Text,
					UserRolesGrid.Columns["RoleCode"].Text,          
					UserRolesGrid.Columns["Comment"].Text,
					mainForm.UserId,
					true
					);

				mainForm.Alert("User " + UserRolesGrid.Columns["UserId"].Text + " no longer has " + UserRolesGrid.Columns["RoleCode"].Text + " privledges", PilotState.Normal);
      
				UserRolesGrid.Col = xRoleCode;              
			}
			catch (Exception error)
			{  
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [AdminUserPersonnelForm.UserRolesGrid_BeforeDelete]", 1);
     
				e.Cancel = true;
				return;
			}
		}
  
		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in UserGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\r\n";

			foreach (int row in UserGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in UserGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\r\n";
			}

			Clipboard.SetDataObject(gridData, true);

			mainForm.Alert("Total: " + UserGrid.SelectedRows.Count + " items copied to the clipboard.", PilotState.Normal);
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n";

			if (UserGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows to copy.", PilotState.Normal);
				return;
			}

			try
			{
				maxTextLength = new int[UserGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in UserGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in UserGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in UserGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in UserGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in UserGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in UserGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in UserGrid.SelectedCols)
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

				mainForm.Alert("Total: " + UserGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
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
			excel.ExportGridToExcel(ref UserGrid);

			this.Cursor = Cursors.Default;
		}
  }
}


