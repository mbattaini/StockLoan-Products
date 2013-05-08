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
  public class AdminHolidaysForm : System.Windows.Forms.Form
  {
    private MainForm mainForm;
    private int xHolidayDate = 0;
    private int xCountryCode = 1;
    private DataSet countriesDataSet;

    private C1.Win.C1TrueDBGrid.C1TrueDBDropdown countriesDropdown;
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid HolidaysGrid;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep2MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
    
    private System.ComponentModel.Container components = null;

    public AdminHolidaysForm(MainForm mainForm)
    {
      this.mainForm = mainForm;
      InitializeComponent();
    
      try
      {
        if (mainForm.AdminAgent.MayEdit(mainForm.UserId, "AdminHolidays") && !mainForm.AdminAgent.HolidayAutoUpdate())
        {
          HolidaysGrid.AllowUpdate = true;
          HolidaysGrid.AllowAddNew = true;
          HolidaysGrid.AllowDelete = true;
        }
      }
      catch (Exception error)
      {        
        Log.Write(error.Message + " [AdminHolidaysForm.AdminHolidaysForm]", 1);      
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AdminHolidaysForm));
			this.HolidaysGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.countriesDropdown = new C1.Win.C1TrueDBGrid.C1TrueDBDropdown();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.HolidaysGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.countriesDropdown)).BeginInit();
			this.SuspendLayout();
			// 
			// HolidaysGrid
			// 
			this.HolidaysGrid.AllowColMove = false;
			this.HolidaysGrid.AllowColSelect = false;
			this.HolidaysGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.HolidaysGrid.AllowUpdate = false;
			this.HolidaysGrid.CaptionHeight = 18;
			this.HolidaysGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.HolidaysGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.HolidaysGrid.EmptyRows = true;
			this.HolidaysGrid.ExtendRightColumn = true;
			this.HolidaysGrid.FetchRowStyles = true;
			this.HolidaysGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.HolidaysGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.HolidaysGrid.Location = new System.Drawing.Point(0, 0);
			this.HolidaysGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
			this.HolidaysGrid.Name = "HolidaysGrid";
			this.HolidaysGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.HolidaysGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.HolidaysGrid.PreviewInfo.ZoomFactor = 75;
			this.HolidaysGrid.RecordSelectorWidth = 16;
			this.HolidaysGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.HolidaysGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.HolidaysGrid.RowHeight = 15;
			this.HolidaysGrid.RowSubDividerColor = System.Drawing.Color.WhiteSmoke;
			this.HolidaysGrid.Size = new System.Drawing.Size(368, 433);
			this.HolidaysGrid.TabAcrossSplits = true;
			this.HolidaysGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.HolidaysGrid.TabIndex = 0;
			this.HolidaysGrid.Text = "Admin Holidays";
			this.HolidaysGrid.WrapCellPointer = true;
			this.HolidaysGrid.AfterUpdate += new System.EventHandler(this.HolidaysGrid_AfterUpdate);
			this.HolidaysGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.HolidaysGrid_BeforeColEdit);
			this.HolidaysGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.HolidaysGrid_FetchRowStyle);
			this.HolidaysGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.HolidaysGrid_BeforeUpdate);
			this.HolidaysGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.HolidaysGrid_BeforeDelete);
			this.HolidaysGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.HolidaysGrid_FormatText);
			this.HolidaysGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HolidaysGrid_KeyPress);
			this.HolidaysGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Holiday Dat" +
				"e\" DataField=\"HolidayDate\" DataWidth=\"10\" NumberFormat=\"FormatText Event\"><Value" +
				"Items /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Country\" Da" +
				"taField=\"CountryCode\" DataWidth=\"2\"><ValueItems /><GroupInfo /></C1DataColumn><C" +
				"1DataColumn Level=\"0\" Caption=\"Exchange\" DataField=\"IsExchangeHoliday\" DefaultVa" +
				"lue=\"true\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"Bank\" DataField=\"IsBankHoliday\" DefaultValue=\"true" +
				"\"><ValueItems Presentation=\"CheckBox\" Translate=\"True\" /><GroupInfo /></C1DataCo" +
				"lumn><C1DataColumn Level=\"0\" Caption=\"Valid\" DataField=\"Valid\" DefaultValue=\"tru" +
				"e\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"\" " +
				"DataField=\"\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"" +
				"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:Highligh" +
				"tText;BackColor:Highlight;}Style50{}Style51{}Style52{}Style53{}Style54{}Caption{" +
				"AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;}Style25{}Selected{ForeColor:Highl" +
				"ightText;BackColor:Highlight;}Editor{Font:Verdana, 8.25pt;BackColor:Honeydew;}St" +
				"yle18{}Style19{Locked:True;}Style14{}Style15{}Style16{Font:Verdana, 8.25pt;Align" +
				"Horz:Near;}Style17{Locked:False;Font:Verdana, 8.25pt;AlignHorz:Far;BackColor:Hon" +
				"eydew;}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}Style47{}Style38{}Style" +
				"37{}Style34{Font:Verdana, 8.25pt;AlignHorz:Center;}Style35{Font:Verdana, 8.25pt;" +
				"AlignHorz:Center;AlignVert:Center;BackColor:Honeydew;}Style32{}Style33{Font:Verd" +
				"ana, 8.25pt;}Style31{}Style29{Font:Verdana, 8.25pt;AlignHorz:Center;AlignVert:Ce" +
				"nter;BackColor:Honeydew;}Style28{Font:Verdana, 8.25pt;AlignHorz:Center;}Style27{" +
				"Font:Verdana, 8.25pt;}Style26{}RecordSelector{Font:Verdana, 8.25pt;AlignImage:Ce" +
				"nter;}Footer{}Style23{Font:Verdana, 8.25pt;AlignHorz:Center;BackColor:Honeydew;}" +
				"Style22{Font:Verdana, 8.25pt;AlignHorz:Near;}Style21{Font:Verdana, 8.25pt;}Style" +
				"55{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Inacti" +
				"ve{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Li" +
				"ghtCyan;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:" +
				"ControlText;BackColor:Control;}Style49{AlignHorz:Near;}Style48{AlignHorz:Near;}S" +
				"tyle24{}Style9{}Style20{}Style5{}Style41{AlignHorz:Near;}Style40{AlignHorz:Near;" +
				"}Style43{}FilterBar{}Style42{}Style44{}Style45{}Style46{}Style8{}Style39{Font:Ve" +
				"rdana, 8.25pt;}Style36{}Style12{}Style4{}Style7{}Style6{}Style1{ForeColor:Black;" +
				"}Style30{}Style3{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView" +
				" HBarStyle=\"None\" VBarStyle=\"Always\" AllowColMove=\"False\" AllowColSelect=\"False\"" +
				" Name=\"\" AllowRowSizing=\"None\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" Colum" +
				"nFooterHeight=\"17\" ExtendRightColumn=\"True\" FetchRowStyles=\"True\" MarqueeStyle=\"" +
				"SolidCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGrou" +
				"p=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><Ed" +
				"itorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style" +
				"8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Foot" +
				"er\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent" +
				"=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" />" +
				"<InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"" +
				"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedS" +
				"tyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><inter" +
				"nalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style par" +
				"ent=\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorS" +
				"tyle parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style2" +
				"1\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><Co" +
				"lumnDivider>Gainsboro,Single</ColumnDivider><Height>15</Height><FetchStyle>True<" +
				"/FetchStyle><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><Group" +
				"HeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=" +
				"\"Style26\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivide" +
				"r><Width>70</Width><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=" +
				"\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Sty" +
				"le5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFoote" +
				"rStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>Gain" +
				"sboro,Single</ColumnDivider><Width>80</Width><Height>15</Height><DCIdx>2</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" />" +
				"<Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" " +
				"/><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" " +
				"me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</V" +
				"isible><ColumnDivider>Gainsboro,None</ColumnDivider><Width>80</Width><Height>15<" +
				"/Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
				"\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle paren" +
				"t=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHead" +
				"erStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Sty" +
				"le44\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><AllowF" +
				"ocus>False</AllowFocus><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style48\" /><Style parent=\"Style1\" me=\"Style49\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style50\" /><EditorStyle parent=\"Style5\" me=\"Style5" +
				"1\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style53\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style52\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</Co" +
				"lumnDivider><Height>15</Height><AllowFocus>False</AllowFocus><DCIdx>5</DCIdx></C" +
				"1DisplayColumn></internalCols><ClientRect>0, 0, 364, 429</ClientRect><BorderSide" +
				">0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style pare" +
				"nt=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading" +
				"\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" m" +
				"e=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=" +
				"\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=" +
				"\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Rec" +
				"ordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" m" +
				"e=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><L" +
				"ayout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0," +
				" 0, 364, 429</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style54\" /><PrintPa" +
				"geFooterStyle parent=\"\" me=\"Style55\" /></Blob>";
			// 
			// countriesDropdown
			// 
			this.countriesDropdown.AllowColMove = true;
			this.countriesDropdown.AllowColSelect = true;
			this.countriesDropdown.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows;
			this.countriesDropdown.AlternatingRows = false;
			this.countriesDropdown.CaptionHeight = 17;
			this.countriesDropdown.ColumnCaptionHeight = 17;
			this.countriesDropdown.ColumnFooterHeight = 17;
			this.countriesDropdown.EmptyRows = true;
			this.countriesDropdown.FetchRowStyles = false;
			this.countriesDropdown.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.countriesDropdown.Location = new System.Drawing.Point(96, 104);
			this.countriesDropdown.Name = "countriesDropdown";
			this.countriesDropdown.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.countriesDropdown.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.countriesDropdown.RowHeight = 15;
			this.countriesDropdown.RowSubDividerColor = System.Drawing.Color.WhiteSmoke;
			this.countriesDropdown.ScrollTips = false;
			this.countriesDropdown.Size = new System.Drawing.Size(208, 200);
			this.countriesDropdown.TabIndex = 1;
			this.countriesDropdown.TabStop = false;
			this.countriesDropdown.Text = "CountriesDropdown";
			this.countriesDropdown.Visible = false;
			this.countriesDropdown.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Country Cod" +
				"e\" DataField=\"CountryCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColu" +
				"mn Level=\"0\" Caption=\"Country\" DataField=\"Country\"><ValueItems /><GroupInfo /></" +
				"C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"" +
				"><Data>Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;}Style25{}Selected{" +
				"ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style10{AlignHorz:Near;}Sty" +
				"le11{}OddRow{}Style13{}Style12{}Footer{}Style29{}Style28{}Style27{AlignHorz:Near" +
				";}Style26{AlignHorz:Near;}RecordSelector{AlignImage:Center;}Style24{}Style23{}St" +
				"yle22{}Style21{AlignHorz:Near;}Style20{AlignHorz:Near;}Inactive{ForeColor:Inacti" +
				"veCaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:Tr" +
				"ue;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:C" +
				"enter;}Style2{}FilterBar{}Style3{}Style4{}Style9{}Style8{}Style5{}Group{AlignVer" +
				"t:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style7{}Style6{}Style1{}" +
				"Style30{}Style31{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}</Da" +
				"ta></Styles><Splits><C1.Win.C1TrueDBGrid.DropdownView Name=\"\" CaptionHeight=\"17\"" +
				" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" MarqueeStyle=\"DottedCellBorder" +
				"\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" RecordSelectors=\"False\" VerticalS" +
				"crollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style" +
				"10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" " +
				"me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle par" +
				"ent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingSty" +
				"le parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"S" +
				"tyle7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"Odd" +
				"Row\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><" +
				"SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\"" +
				" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style20\" /><" +
				"Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Style22\" /" +
				"><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style1\" m" +
				"e=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Visible>True</Vi" +
				"sible><ColumnDivider>Gainsboro,Single</ColumnDivider><Height>15</Height><DCIdx>0" +
				"</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Sty" +
				"le26\" /><Style parent=\"Style1\" me=\"Style27\" /><FooterStyle parent=\"Style3\" me=\"S" +
				"tyle28\" /><EditorStyle parent=\"Style5\" me=\"Style29\" /><GroupHeaderStyle parent=\"" +
				"Style1\" me=\"Style31\" /><GroupFooterStyle parent=\"Style1\" me=\"Style30\" /><Visible" +
				">True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height>" +
				"<DCIdx>1</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 204, 196</Cli" +
				"entRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.DropdownView></Splits><N" +
				"amedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" />" +
				"<Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><St" +
				"yle parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Sty" +
				"le parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Sty" +
				"le parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style pa" +
				"rent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><St" +
				"yle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzS" +
				"plits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecS" +
				"elWidth><ClientArea>0, 0, 204, 196</ClientArea></Blob>";
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
			// AdminHolidaysForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(368, 433);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.countriesDropdown);
			this.Controls.Add(this.HolidaysGrid);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.HelpButton = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AdminHolidaysForm";
			this.Text = "Admin - Holidays";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.AdminHolidaysForm_Closing);
			this.Load += new System.EventHandler(this.AdminHolidaysForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.HolidaysGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.countriesDropdown)).EndInit();
			this.ResumeLayout(false);

		}
    #endregion

  private int DateStatus(string date)
  {
    DateTime dateTime = new DateTime();
    DateTime dateTimeToday = DateTime.Now;

    try
    {
      if(!date.Equals(""))
      { 
        dateTime = DateTime.Parse(date);
        dateTimeToday = DateTime.Parse(mainForm.ServiceAgent.BizDate());
      }
    }
    catch (FormatException error)
    {        
      Log.Write(error.Message + " [AdminHolidaysForm.DateStatus]", 1);      
      mainForm.Alert(error.Message, PilotState.RunFault);
      return -1;
    }

    if (dateTime < dateTimeToday)
    {
      return (-1);
    }
    else if (dateTime == dateTimeToday)
    {
      return 0;
    }
    else
    {
      return 1;
    }
  }
    
    private void AdminHolidaysForm_Load(object sender, System.EventArgs e)
    {
      int height = (mainForm.Height / 2);
      int width = (mainForm.Width / 4);
      
      mainForm.Alert("Please wait... Loading current holiday data...", PilotState.Unknown);
      this.Cursor = Cursors.WaitCursor;
      
      try
      {
        this.Top    = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
        this.Left   = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
        this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
        this.Width  = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));
                  
        HolidaysGrid.SetDataBinding(mainForm.AdminAgent.HolidaysGet(), "Holidays",true);
        HolidaysGrid.Show();

        countriesDataSet = mainForm.ServiceAgent.CountryGet();
      
        mainForm.Alert("Loading current holiday data... Done!", PilotState.Normal); 
      }     
      catch (Exception error)
      {        
        Log.Write(error.Message + " [AdminHolidaysForm.AdminHolidaysForm_Load]", 1);      
        mainForm.Alert(error.Message, PilotState.RunFault);
      }

      this.Cursor = Cursors.Default;                 
    }

    private void AdminHolidaysForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
        RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
        RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
        RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
      }
    }

    private void HolidaysGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
    if (HolidaysGrid.Columns["HolidayDate"].Text.Equals("")) 
      {
        mainForm.Alert("The Date is missing.", PilotState.RunFault);
        HolidaysGrid.Col = xHolidayDate;
        e.Cancel = true;
        return;
      }
     
      if (HolidaysGrid.Columns["CountryCode"].Text.Equals(""))
      {
        mainForm.Alert("The Country Code is missing.", PilotState.RunFault);
        HolidaysGrid.Col = xCountryCode;
        e.Cancel = true;
        return;
      }

    switch(DateStatus(HolidaysGrid.Columns["HolidayDate"].Text))
    {
      case (-1):
        mainForm.Alert("Date is invalid: Entered Date is before current date.", PilotState.RunFault);
        HolidaysGrid.Col = xHolidayDate;
        e.Cancel = true;
        return;
      case 0:
        mainForm.Alert("Date is invalid: Entered Date cannot be current date.", PilotState.RunFault);
        HolidaysGrid.Col = xHolidayDate;
        e.Cancel = true;
        return;
      case 1:  
        break;          
    }          
  
      try 
      {
        mainForm.AdminAgent.HolidaysSet(
          DateTime.Parse(HolidaysGrid.Columns["HolidayDate"].Text).ToString(Standard.DateTimeFormat),
          HolidaysGrid.Columns["CountryCode"].Text, 
          bool.Parse(HolidaysGrid.Columns["IsBankHoliday"].Text), 
          bool.Parse(HolidaysGrid.Columns["IsExchangeHoliday"].Text),
          true
          );

        mainForm.Alert("Holiday " +  HolidaysGrid.Columns["HolidayDate"].Text + " has been added", PilotState.Normal);
      }
      catch (Exception error)
      {        
        Log.Write(error.Message + " [AdminHolidaysForm.HolidaysGrid_BeforeUpdate]", 1);    
        mainForm.Alert(error.Message, PilotState.RunFault);
        HolidaysGrid.Col = xCountryCode;
        e.Cancel = true;        
        return;
      } 
    }
   
    private void HolidaysGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {            
      if  (e.Value.ToString().Length == 0)
      {
        return;
      }

      switch (HolidaysGrid.Columns[e.ColIndex].DataField)
      {
        case("HolidayDate"):
          e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateFormat);                    
          break;
      }
    }

    private void HolidaysGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
    {       
      if ((DateStatus(HolidaysGrid.Columns["HolidayDate"].Text) < 1) && (!HolidaysGrid.Columns["HolidayDate"].Text.Equals("")))
      {
        mainForm.Alert("This holiday has already expired.");
        e.Cancel = true;
        return;
      }

      if (HolidaysGrid.Columns[e.ColIndex].DataField.Equals("HolidayDate"))
      {
        if (!HolidaysGrid.Columns["HolidayDate"].Text.Equals(""))
        {
          mainForm.Alert("You are not allowed to edit this cell.");
          HolidaysGrid.Col = e.ColIndex + 1;
          e.Cancel = true;
          return;
        }
      }

      if (HolidaysGrid.Columns[e.ColIndex].DataField.Equals("CountryCode"))
      {
        if (HolidaysGrid.Columns["Country"].Text.Equals("") && HolidaysGrid.DataChanged)
        {
            HolidaysGrid.Columns["CountryCode"].DropDown = countriesDropdown;
            countriesDropdown.SetDataBinding(countriesDataSet, "Countries", true);
            countriesDropdown.DataField = "Country";
            countriesDropdown.ListField = "CountryCode";
            countriesDropdown.ValueTranslate = true;          
        }
        else
        {
          mainForm.Alert("You are not allowed to edit this cell.");          
          e.Cancel = true;
          return;
        }
      }
    }

    private void HolidaysGrid_AfterUpdate(object sender, System.EventArgs e)
    {
      HolidaysGrid.Col = xHolidayDate;
      HolidaysGrid.Columns["CountryCode"].DropDown = null;
    }

    private void HolidaysGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      string gridData = "";

      if (e.KeyChar == 27)
      {
        if (HolidaysGrid.Columns["HolidayDate"].Text.Equals("") || HolidaysGrid.Columns["CountryCode"].Text.Equals(""))
        {
          HolidaysGrid.Delete();
          HolidaysGrid.Columns["CountryCode"].DropDown = null;
          HolidaysGrid.DataChanged = false;
        
           e.Handled = true;
        }
      }
      
      if (e.KeyChar.Equals((char)3) && HolidaysGrid.SelectedRows.Count > 0)
      {
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HolidaysGrid.SelectedCols)
        {
          gridData += dataColumn.Caption + "\t";
        }

        gridData += "\n";

        foreach (int row in HolidaysGrid.SelectedRows)
        {
          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HolidaysGrid.SelectedCols)
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
        mainForm.Alert("Copied " + HolidaysGrid.SelectedRows.Count.ToString("#,##0") + " rows to the clipboard.");
        e.Handled = true;
      }
    }

    private void HolidaysGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
    {
      if ((DateStatus(HolidaysGrid.Columns["HolidayDate"].CellText(e.Row)) == 1) || HolidaysGrid.Columns["HolidayDate"].CellText(e.Row).Equals(""))
      {
        e.CellStyle.BackColor = System.Drawing.Color.White;
      }
      else
      {
        e.CellStyle.BackColor = System.Drawing.Color.LightSalmon;
      }
    }

		private void HolidaysGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{                               
				mainForm.AdminAgent.HolidaysSet(
					DateTime.Parse(HolidaysGrid.Columns["HolidayDate"].Text).ToString(Standard.DateTimeFormat),
					HolidaysGrid.Columns["CountryCode"].Text, 
					bool.Parse(HolidaysGrid.Columns["IsBankHoliday"].Text), 
					bool.Parse(HolidaysGrid.Columns["IsExchangeHoliday"].Text),
					false
					);
      
				mainForm.Alert("Removed date: " + DateTime.Parse(HolidaysGrid.Columns["HolidayDate"].Text).ToString(Standard.DateTimeFormat) + " from the holiday list", PilotState.Normal);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [AdminHolidaysForm.HolidaysGrid_BeforeDelete]", 1);      
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HolidaysGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\r\n";

			foreach (int row in HolidaysGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HolidaysGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\r\n";
			}

			Clipboard.SetDataObject(gridData, true);

			mainForm.Alert("Total: " + HolidaysGrid.SelectedRows.Count + " items copied to the clipboard.", PilotState.Normal);
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n";

			if (HolidaysGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows to copy.", PilotState.Normal);
				return;
			}

			try
			{
				maxTextLength = new int[HolidaysGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HolidaysGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in HolidaysGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HolidaysGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HolidaysGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HolidaysGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in HolidaysGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HolidaysGrid.SelectedCols)
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

				mainForm.Alert("Total: " + HolidaysGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
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
			excel.ExportGridToExcel(ref HolidaysGrid);

			this.Cursor = Cursors.Default;
		}
  }
}

  