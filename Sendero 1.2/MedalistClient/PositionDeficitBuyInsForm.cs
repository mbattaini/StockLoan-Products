// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Remoting;
using System.ComponentModel;
using System.Windows.Forms;
using Anetics.Common;
using Anetics.Medalist;

namespace Anetics.Medalist
{
  public class PositionDeficitBuyInsForm : System.Windows.Forms.Form
  {
    private System.ComponentModel.Container components = null;
    
		private MainForm mainForm;
		private DataSet dataSet = null;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private C1.Win.C1Input.C1Label c1Label1;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep2MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.Windows.Forms.MenuItem menuItem1;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid PositionAccountsGrid;

    public PositionDeficitBuyInsForm(MainForm mainForm)
    {
      InitializeComponent();
      this.mainForm = mainForm;
		
			try
			{

			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionDeficitBuyInsForm));
			this.PositionAccountsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.c1Label1 = new C1.Win.C1Input.C1Label();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.PositionAccountsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
			this.SuspendLayout();
			// 
			// PositionAccountsGrid
			// 
			this.PositionAccountsGrid.CaptionHeight = 17;
			this.PositionAccountsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PositionAccountsGrid.EmptyRows = true;
			this.PositionAccountsGrid.ExtendRightColumn = true;
			this.PositionAccountsGrid.FetchRowStyles = true;
			this.PositionAccountsGrid.FilterBar = true;
			this.PositionAccountsGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.PositionAccountsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.PositionAccountsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.PositionAccountsGrid.Location = new System.Drawing.Point(1, 50);
			this.PositionAccountsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.PositionAccountsGrid.Name = "PositionAccountsGrid";
			this.PositionAccountsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.PositionAccountsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.PositionAccountsGrid.PreviewInfo.ZoomFactor = 75;
			this.PositionAccountsGrid.RecordSelectorWidth = 16;
			this.PositionAccountsGrid.RowDivider.Color = System.Drawing.Color.WhiteSmoke;
			this.PositionAccountsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.PositionAccountsGrid.RowHeight = 16;
			this.PositionAccountsGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.PositionAccountsGrid.Size = new System.Drawing.Size(1214, 762);
			this.PositionAccountsGrid.TabIndex = 0;
			this.PositionAccountsGrid.Text = "Accounts";
			this.PositionAccountsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Firm\" DataF" +
				"ield=\"Firm\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Ca" +
				"ption=\"LocMemo\" DataField=\"LocMemo\" NumberFormat=\"FormatText Event\"><ValueItems " +
				"/><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"AccountType\" Data" +
				"Field=\"AccountType\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"CurrencyCode\" DataField=\"Currency" +
				"Code\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=" +
				"\"Security ID\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1Dat" +
				"aColumn Level=\"0\" Caption=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /" +
				"></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Total Quantity\" DataField=\"Tota" +
				"lQuantity\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
				"tion=\"Rate\" DataField=\"Rate\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataCo" +
				"lumn Level=\"0\" Caption=\"Charge\" DataField=\"Charge\"><ValueItems /><GroupInfo /></" +
				"C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Covered Quantity\" DataField=\"Cover" +
				"ed Quantity\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" C" +
				"aption=\"Uncovered Quantity\" DataField=\"Uncovered Quantity\"><ValueItems /><GroupI" +
				"nfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"ISIN\" DataField=\"ISIN\"><Va" +
				"lueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBG" +
				"rid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:" +
				"Highlight;}Style85{AlignHorz:Far;BackColor:WhiteSmoke;}Inactive{ForeColor:Inacti" +
				"veCaptionText;BackColor:InactiveCaption;}Style78{AlignHorz:Near;}Style79{AlignHo" +
				"rz:Far;BackColor:WhiteSmoke;}Selected{ForeColor:HighlightText;BackColor:Highligh" +
				"t;}Editor{}Style72{AlignHorz:Near;}Style73{AlignHorz:Far;BackColor:LightYellow;}" +
				"Style70{}Style71{}Style76{}Style77{}Style74{}Style75{}Style84{AlignHorz:Near;}St" +
				"yle87{}Style86{}Style81{}Style80{}Style83{}Style82{}Footer{}Style89{}Style88{}Fi" +
				"lterBar{BackColor:SeaShell;}RecordSelector{AlignImage:Center;}Heading{Wrap:True;" +
				"AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Contr" +
				"ol;}Style18{}Style19{}Style14{AlignHorz:Near;}Style15{AlignHorz:Near;}Style16{}S" +
				"tyle17{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style29{}Style28{}Sty" +
				"le27{}Style22{}Style26{}Style9{}Style8{}Style25{}Style24{}Style5{}Style4{}Style7" +
				"{}Style6{}Style1{}Style23{}Style3{}Style2{}Style21{AlignHorz:Near;}Style20{Align" +
				"Horz:Near;}OddRow{}Style38{}Style39{}Style36{AlignHorz:Near;}Style37{Font:Verdan" +
				"a, 8.25pt, style=Bold;AlignHorz:Far;}Style34{}Style35{}Style32{}Style33{}Style30" +
				"{AlignHorz:Near;}Style49{AlignHorz:Near;BackColor:AliceBlue;}Style48{AlignHorz:N" +
				"ear;}Style31{Font:Verdana, 8.25pt, style=Bold;AlignHorz:Far;ForeColor:HotTrack;}" +
				"Normal{Font:Verdana, 8.25pt;}Style41{}Style40{}Style43{AlignHorz:Near;BackColor:" +
				"AliceBlue;}Style42{AlignHorz:Near;}Style45{}Style44{}Style47{}Style46{}EvenRow{B" +
				"ackColor:Aqua;}Style58{}Style59{}Style50{}Style51{}Style52{}Style53{}Style54{Ali" +
				"gnHorz:Near;}Style55{AlignHorz:Far;BackColor:Lavender;}Style56{}Style57{}Caption" +
				"{AlignHorz:Center;}Style69{}Style68{}Style63{}Style62{}Style61{AlignHorz:Far;Bac" +
				"kColor:WhiteSmoke;}Style60{AlignHorz:Near;}Style67{AlignHorz:Near;}Style66{Align" +
				"Horz:Near;}Style65{}Style64{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0" +
				";AlignVert:Center;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarSt" +
				"yle=\"None\" VBarStyle=\"Always\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17" +
				"\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FetchRowStyles=\"True\" FilterB" +
				"ar=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth" +
				"=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"St" +
				"yle2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle pa" +
				"rent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><" +
				"FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12" +
				"\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"High" +
				"lightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowSt" +
				"yle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" m" +
				"e=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Norm" +
				"al\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
				"e=\"Style20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\"" +
				" me=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><C" +
				"olumnDivider>Gainsboro,Single</ColumnDivider><Width>240</Width><Height>16</Heigh" +
				"t><Locked>True</Locked><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style30\" /><Style parent=\"Style1\" me=\"Style31\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style32\" /><EditorStyle parent=\"Style5\" me=\"Style3" +
				"3\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style35\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style34\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>130<" +
				"/Width><Height>16</Height><Locked>True</Locked><DCIdx>1</DCIdx></C1DisplayColumn" +
				"><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style36\" /><Style parent=\"St" +
				"yle1\" me=\"Style37\" /><FooterStyle parent=\"Style3\" me=\"Style38\" /><EditorStyle pa" +
				"rent=\"Style5\" me=\"Style39\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style41\" /><G" +
				"roupFooterStyle parent=\"Style1\" me=\"Style40\" /><ColumnDivider>DarkGray,Single</C" +
				"olumnDivider><Width>130</Width><Height>16</Height><Locked>True</Locked><DCIdx>2<" +
				"/DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Styl" +
				"e66\" /><Style parent=\"Style1\" me=\"Style67\" /><FooterStyle parent=\"Style3\" me=\"St" +
				"yle68\" /><EditorStyle parent=\"Style5\" me=\"Style69\" /><GroupHeaderStyle parent=\"S" +
				"tyle1\" me=\"Style71\" /><GroupFooterStyle parent=\"Style1\" me=\"Style70\" /><ColumnDi" +
				"vider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>3</DCIdx></C1Disp" +
				"layColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style p" +
				"arent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><Edito" +
				"rStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Styl" +
				"e19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><" +
				"ColumnDivider>WhiteSmoke,Single</ColumnDivider><Height>16</Height><DCIdx>11</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style42\"" +
				" /><Style parent=\"Style1\" me=\"Style43\" /><FooterStyle parent=\"Style3\" me=\"Style4" +
				"4\" /><EditorStyle parent=\"Style5\" me=\"Style45\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style47\" /><GroupFooterStyle parent=\"Style1\" me=\"Style46\" /><Visible>True" +
				"</Visible><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Width>116</Width><Hei" +
				"ght>16</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
				"parent=\"Style2\" me=\"Style48\" /><Style parent=\"Style1\" me=\"Style49\" /><FooterStyl" +
				"e parent=\"Style3\" me=\"Style50\" /><EditorStyle parent=\"Style5\" me=\"Style51\" /><Gr" +
				"oupHeaderStyle parent=\"Style1\" me=\"Style53\" /><GroupFooterStyle parent=\"Style1\" " +
				"me=\"Style52\" /><Visible>True</Visible><ColumnDivider>WhiteSmoke,Single</ColumnDi" +
				"vider><Height>16</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
				"dingStyle parent=\"Style2\" me=\"Style54\" /><Style parent=\"Style1\" me=\"Style55\" /><" +
				"FooterStyle parent=\"Style3\" me=\"Style56\" /><EditorStyle parent=\"Style5\" me=\"Styl" +
				"e57\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style59\" /><GroupFooterStyle parent" +
				"=\"Style1\" me=\"Style58\" /><Visible>True</Visible><ColumnDivider>WhiteSmoke,Single" +
				"</ColumnDivider><Width>150</Width><Height>16</Height><DCIdx>6</DCIdx></C1Display" +
				"Column><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style78\" /><Style pare" +
				"nt=\"Style1\" me=\"Style79\" /><FooterStyle parent=\"Style3\" me=\"Style80\" /><EditorSt" +
				"yle parent=\"Style5\" me=\"Style81\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style83" +
				"\" /><GroupFooterStyle parent=\"Style1\" me=\"Style82\" /><Visible>True</Visible><Col" +
				"umnDivider>WhiteSmoke,Single</ColumnDivider><Width>150</Width><Height>16</Height" +
				"><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"\" me=\"Style84\" /><Style parent=\"Style1\" me=\"Style85\" /><FooterStyle parent=\"Styl" +
				"e3\" me=\"Style86\" /><EditorStyle parent=\"Style5\" me=\"Style87\" /><GroupHeaderStyle" +
				" parent=\"Style1\" me=\"Style89\" /><GroupFooterStyle parent=\"Style1\" me=\"Style88\" /" +
				"><Visible>True</Visible><ColumnDivider>WhiteSmoke,Single</ColumnDivider><Width>1" +
				"50</Width><Height>16</Height><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn" +
				"><HeadingStyle parent=\"Style2\" me=\"Style60\" /><Style parent=\"Style1\" me=\"Style61" +
				"\" /><FooterStyle parent=\"Style3\" me=\"Style62\" /><EditorStyle parent=\"Style5\" me=" +
				"\"Style63\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style65\" /><GroupFooterStyle p" +
				"arent=\"Style1\" me=\"Style64\" /><Visible>True</Visible><ColumnDivider>WhiteSmoke,S" +
				"ingle</ColumnDivider><Height>16</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1Dis" +
				"playColumn><HeadingStyle parent=\"Style2\" me=\"Style72\" /><Style parent=\"Style1\" m" +
				"e=\"Style73\" /><FooterStyle parent=\"Style3\" me=\"Style74\" /><EditorStyle parent=\"S" +
				"tyle5\" me=\"Style75\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style77\" /><GroupFoo" +
				"terStyle parent=\"Style1\" me=\"Style76\" /><Visible>True</Visible><ColumnDivider>Wh" +
				"iteSmoke,Single</ColumnDivider><Height>16</Height><DCIdx>8</DCIdx></C1DisplayCol" +
				"umn></internalCols><ClientRect>0, 0, 1210, 758</ClientRect><BorderSide>0</Border" +
				"Side></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"" +
				"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Foot" +
				"er\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactiv" +
				"e\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /" +
				"><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" " +
				"/><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelecto" +
				"r\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" " +
				"/></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modi" +
				"fied</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 1210, " +
				"758</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style28\" /><PrintPageFooterS" +
				"tyle parent=\"\" me=\"Style29\" /></Blob>";
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Location = new System.Drawing.Point(40, 14);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(216, 21);
			this.dateTimePicker2.TabIndex = 2;
			// 
			// c1Label1
			// 
			this.c1Label1.Location = new System.Drawing.Point(-24, 16);
			this.c1Label1.Name = "c1Label1";
			this.c1Label1.Size = new System.Drawing.Size(64, 16);
			this.c1Label1.TabIndex = 3;
			this.c1Label1.Tag = null;
			this.c1Label1.Text = "From:";
			this.c1Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.c1Label1.TextDetached = true;
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.menuItem1,
																																										this.SendToMenuItem,
																																										this.Sep2MenuItem,
																																										this.ExitMenuItem});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "C reate";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// SendToMenuItem
			// 
			this.SendToMenuItem.Index = 1;
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
			// SendToExcelMenuItem
			// 
			this.SendToExcelMenuItem.Index = 1;
			this.SendToExcelMenuItem.Text = "Excel";
			// 
			// SendToEmailMenuItem
			// 
			this.SendToEmailMenuItem.Index = 2;
			this.SendToEmailMenuItem.Text = "Mail Recipient";
			// 
			// Sep2MenuItem
			// 
			this.Sep2MenuItem.Index = 2;
			this.Sep2MenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 3;
			this.ExitMenuItem.Text = "Exit";
			// 
			// PositionDeficitBuyInsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1216, 813);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.c1Label1);
			this.Controls.Add(this.dateTimePicker2);
			this.Controls.Add(this.PositionAccountsGrid);
			this.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.DockPadding.Bottom = 1;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 50;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PositionDeficitBuyInsForm";
			this.Text = "Position - Accounts";
			((System.ComponentModel.ISupportInitialize)(this.PositionAccountsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
			this.ResumeLayout(false);

		}
    #endregion

		private void PositionDeficitBuyInsForm_Load(object sender, System.EventArgs e)
		{
			int height = mainForm.Height / 2;
			int width  = mainForm.Width / 2;
      
			try
			{
				this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
				this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
				this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
				this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));
        
			}
			catch(Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);        
			}
		}

		private void PositionDeficitBuyInsForm_Closed(object sender, System.EventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
				RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
				RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
				RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
		
		}
    
	}
}
