// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using C1.C1Pdf;
using Anetics.Common;


namespace Anetics.Medalist
{
	public class ShortSaleBillingBillsAccountsForm : System.Windows.Forms.Form
	{
		private MainForm mainForm;
		private string groupCode = "";
		private string paintGroupCode = "";
		private DataSet dataSet = null;		
		private System.Windows.Forms.Panel BackgroundPanel;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid AccountsGrid;
		private System.Windows.Forms.RadioButton EmailRadio;
		private System.Windows.Forms.RadioButton AccountRadio;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid EmailGrid;
		private System.ComponentModel.Container components = null;
    
		public ShortSaleBillingBillsAccountsForm(MainForm mainForm, string groupCode)
		{    
			InitializeComponent();     
    
			try
			{  
				this.groupCode = groupCode;
				this.mainForm = mainForm;                          			
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleBillingRateChangeInputForm.ShortSaleBillingRateChangeInputForm]", Log.Error, 1);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleBillingBillsAccountsForm));
			this.BackgroundPanel = new System.Windows.Forms.Panel();
			this.AccountsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.EmailRadio = new System.Windows.Forms.RadioButton();
			this.AccountRadio = new System.Windows.Forms.RadioButton();
			this.EmailGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.BackgroundPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AccountsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.EmailGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// BackgroundPanel
			// 
			this.BackgroundPanel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.BackgroundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.BackgroundPanel.Controls.Add(this.EmailGrid);
			this.BackgroundPanel.Controls.Add(this.AccountsGrid);
			this.BackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BackgroundPanel.DockPadding.All = 1;
			this.BackgroundPanel.Location = new System.Drawing.Point(1, 1);
			this.BackgroundPanel.Name = "BackgroundPanel";
			this.BackgroundPanel.Size = new System.Drawing.Size(712, 456);
			this.BackgroundPanel.TabIndex = 59;
			// 
			// AccountsGrid
			// 
			this.AccountsGrid.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.AccountsGrid.CaptionHeight = 17;
			this.AccountsGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.AccountsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AccountsGrid.EmptyRows = true;
			this.AccountsGrid.ExtendRightColumn = true;
			this.AccountsGrid.FilterBar = true;
			this.AccountsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.AccountsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.AccountsGrid.Location = new System.Drawing.Point(1, 1);
			this.AccountsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.AccountsGrid.Name = "AccountsGrid";
			this.AccountsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.AccountsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.AccountsGrid.PreviewInfo.ZoomFactor = 75;
			this.AccountsGrid.RecordSelectorWidth = 16;
			this.AccountsGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.AccountsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.AccountsGrid.RowHeight = 15;
			this.AccountsGrid.RowSubDividerColor = System.Drawing.Color.LightGray;
			this.AccountsGrid.Size = new System.Drawing.Size(708, 452);
			this.AccountsGrid.TabIndex = 0;
			this.AccountsGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.AccountsGrid_Paint);
			this.AccountsGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.AccountsGrid_BeforeUpdate);
			this.AccountsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.AccountsGrid_FormatText);
			this.AccountsGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AccountsGrid_KeyPress);
			this.AccountsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Group Code\"" +
				" DataField=\"GroupCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"Account Number\" DataField=\"AccountNumber\"><ValueItems /><Group" +
				"Info /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"OSI Billing\" DataField=\"I" +
				"sOSIBilling\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><" +
				"C1DataColumn Level=\"0\" Caption=\"Paper Billing\" DataField=\"IsPaperBilling\"><Value" +
				"Items Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"Actor\" DataField=\"ActUserId\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataField=\"ActTime\" NumberFormat=" +
				"\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"EmailAddress\" DataField=\"EmailAddress\"><ValueItems /><GroupInfo />" +
				"</C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrappe" +
				"r\"><Data>Style12{}RecordSelector{AlignImage:Center;}Style50{}Style51{}Style52{Al" +
				"ignHorz:Near;}Style53{AlignHorz:Near;}Style54{}Caption{AlignHorz:Center;}Style27" +
				"{}Normal{Font:Verdana, 8.25pt;}Selected{ForeColor:HighlightText;BackColor:Highli" +
				"ght;}Editor{}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}Style17" +
				"{AlignHorz:Near;BackColor:Snow;}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13" +
				"{}Style44{}Style45{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVer" +
				"t:Center;}Style39{}Style4{}Style29{AlignHorz:Center;BackColor:251, 254, 255;}Sty" +
				"le28{AlignHorz:Center;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;" +
				"}Style26{}Style25{}Footer{}Style23{AlignHorz:Near;BackColor:Snow;}Style22{AlignH" +
				"orz:Near;}Style21{}Style55{}Style56{}Style57{}Inactive{ForeColor:InactiveCaption" +
				"Text;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Style6{}Heading{Wrap:Tru" +
				"e;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Con" +
				"trol;}Style49{}Style48{}Style24{}Style7{}Style8{}Style1{}Style20{}Style3{}Style4" +
				"1{AlignHorz:Near;BackColor:255, 251, 242;}Style40{AlignHorz:Near;}Style43{}Filte" +
				"rBar{BackColor:SeaShell;}Style42{}Style5{}Style47{AlignHorz:Near;BackColor:255, " +
				"251, 242;}Style9{}Style38{}Style46{AlignHorz:Near;}Style36{}Style37{}Style34{Ali" +
				"gnHorz:Center;}Style35{AlignHorz:Center;BackColor:251, 254, 255;}Style32{}Style3" +
				"3{}Style30{}Style31{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeV" +
				"iew Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" " +
				"ExtendRightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedCellBorder\" Record" +
				"SelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollG" +
				"roup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Edito" +
				"r\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle pa" +
				"rent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><Grou" +
				"pStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" " +
				"/><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"" +
				"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelect" +
				"orStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" " +
				"me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColum" +
				"n><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style1" +
				"7\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me" +
				"=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle " +
				"parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivider>LightGray,S" +
				"ingle</ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1Dis" +
				"playColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" m" +
				"e=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"S" +
				"tyle5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFoo" +
				"terStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>Li" +
				"ghtGray,Single</ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColu" +
				"mn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"" +
				"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle " +
				"parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" />" +
				"<GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnD" +
				"ivider>LightGray,Single</ColumnDivider><Height>15</Height><DCIdx>2</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style" +
				" parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><Edi" +
				"torStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"St" +
				"yle39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible" +
				"><ColumnDivider>LightGray,Single</ColumnDivider><Height>15</Height><DCIdx>3</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style40\"" +
				" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style4" +
				"2\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visible>True" +
				"</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Height>15</Height><DCI" +
				"dx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"Style50\" /><Vis" +
				"ible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Height>15</He" +
				"ight><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
				"yle2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"" +
				"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHeaderS" +
				"tyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Style5" +
				"6\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15</Height><DCIdx>6</" +
				"DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 704, 448</ClientRect><B" +
				"orderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><S" +
				"tyle parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent" +
				"=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"H" +
				"eading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"No" +
				"rmal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"No" +
				"rmal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading" +
				"\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"C" +
				"aption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horz" +
				"Splits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><Clie" +
				"ntArea>0, 0, 704, 448</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /" +
				"><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// EmailRadio
			// 
			this.EmailRadio.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.EmailRadio.Location = new System.Drawing.Point(4, 460);
			this.EmailRadio.Name = "EmailRadio";
			this.EmailRadio.Size = new System.Drawing.Size(140, 20);
			this.EmailRadio.TabIndex = 60;
			this.EmailRadio.Text = "Email Addresses";
			this.EmailRadio.CheckedChanged += new System.EventHandler(this.EmailRadio_CheckedChanged);
			// 
			// AccountRadio
			// 
			this.AccountRadio.Enabled = false;
			this.AccountRadio.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AccountRadio.Location = new System.Drawing.Point(144, 460);
			this.AccountRadio.Name = "AccountRadio";
			this.AccountRadio.Size = new System.Drawing.Size(132, 20);
			this.AccountRadio.TabIndex = 61;
			this.AccountRadio.Text = "OSI Account(s)";
			this.AccountRadio.CheckedChanged += new System.EventHandler(this.AccountRadio_CheckedChanged);
			// 
			// EmailGrid
			// 
			this.EmailGrid.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.EmailGrid.CaptionHeight = 17;
			this.EmailGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.EmailGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.EmailGrid.EmptyRows = true;
			this.EmailGrid.ExtendRightColumn = true;
			this.EmailGrid.FilterBar = true;
			this.EmailGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.EmailGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.EmailGrid.Location = new System.Drawing.Point(1, 1);
			this.EmailGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.EmailGrid.Name = "EmailGrid";
			this.EmailGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.EmailGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.EmailGrid.PreviewInfo.ZoomFactor = 75;
			this.EmailGrid.RecordSelectorWidth = 16;
			this.EmailGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.EmailGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.EmailGrid.RowHeight = 15;
			this.EmailGrid.RowSubDividerColor = System.Drawing.Color.LightGray;
			this.EmailGrid.Size = new System.Drawing.Size(708, 452);
			this.EmailGrid.TabIndex = 1;
			this.EmailGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.EmailGrid_BeforeUpdate);
			this.EmailGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.AccountsGrid_FormatText);
			this.EmailGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EmailGrid_KeyPress);
			this.EmailGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Group Code\"" +
				" DataField=\"GroupCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"Actor\" DataField=\"ActUserId\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataField=\"ActTime\" NumberFo" +
				"rmat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
				"Level=\"0\" Caption=\"EmailAddress\" DataField=\"EmailAddress\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Email Address\" DataField=\"E" +
				"mailAddress\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"" +
				"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:Highligh" +
				"tText;BackColor:Highlight;}Style50{}Style51{}Style52{AlignHorz:Near;}Style53{Ali" +
				"gnHorz:Near;}Style54{}Caption{AlignHorz:Center;}Style56{}Normal{Font:Verdana, 8." +
				"25pt;}Style25{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Sty" +
				"le18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}Style17{AlignHorz:Near;" +
				"BackColor:Snow;}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}Style46{AlignH" +
				"orz:Near;}Style27{}Style26{}RecordSelector{AlignImage:Center;}Footer{}Style23{Al" +
				"ignHorz:Near;}Style22{AlignHorz:Near;}Style21{}Style55{}Group{AlignVert:Center;B" +
				"order:None,,0, 0, 0, 0;BackColor:ControlDark;}Style57{}Inactive{ForeColor:Inacti" +
				"veCaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:Tr" +
				"ue;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:C" +
				"enter;}Style49{}Style48{}Style24{}Style20{}Style5{}Style41{AlignHorz:Near;BackCo" +
				"lor:255, 251, 242;}Style40{AlignHorz:Near;}Style43{}Style42{}Style45{}Style44{}S" +
				"tyle47{AlignHorz:Near;BackColor:255, 251, 242;}Style9{}Style8{}FilterBar{BackCol" +
				"or:SeaShell;}Style12{}Style4{}Style7{}Style6{}Style1{}Style3{}Style2{}</Data></S" +
				"tyles><Splits><C1.Win.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCa" +
				"ptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FilterBar=\"Tru" +
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
				"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible" +
				">True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Height>15</Height" +
				"><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Styl" +
				"e3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle" +
				" parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /" +
				"><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>240" +
				"</Width><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /" +
				"><FooterStyle parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"St" +
				"yle43\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle pare" +
				"nt=\"Style1\" me=\"Style44\" /><Visible>True</Visible><ColumnDivider>LightGray,Singl" +
				"e</ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1Display" +
				"Column><HeadingStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"S" +
				"tyle47\" /><FooterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style" +
				"5\" me=\"Style49\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterS" +
				"tyle parent=\"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>LightG" +
				"ray,Single</ColumnDivider><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><" +
				"C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style parent=\"Styl" +
				"e1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorStyle pare" +
				"nt=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57\" /><Gro" +
				"upFooterStyle parent=\"Style1\" me=\"Style56\" /><ColumnDivider>DarkGray,Single</Col" +
				"umnDivider><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn></internalCols><" +
				"ClientRect>0, 0, 704, 448</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueD" +
				"BGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style pare" +
				"nt=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"" +
				"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"N" +
				"ormal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Norma" +
				"l\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Norm" +
				"al\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"N" +
				"ormal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vert" +
				"Splits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><Default" +
				"RecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 704, 448</ClientArea><Print" +
				"PageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Sty" +
				"le15\" /></Blob>";
			// 
			// ShortSaleBillingBillsAccountsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(714, 479);
			this.Controls.Add(this.AccountRadio);
			this.Controls.Add(this.EmailRadio);
			this.Controls.Add(this.BackgroundPanel);
			this.DockPadding.Bottom = 22;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 1;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ShortSaleBillingBillsAccountsForm";
			this.Text = "ShortSale - Billing - Bills - Methods";
			this.Load += new System.EventHandler(this.ShortSaleBillingBillsAccountsForm_Load);
			this.Closed += new System.EventHandler(this.ShortSaleBillingBillsForm_Closed);
			this.BackgroundPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.AccountsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.EmailGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

    

		private void ShortSaleBillingBillsAccountsForm_Load(object sender, System.EventArgs e)
		{       
			this.WindowState = FormWindowState.Normal;
      
			try
			{			
				EmailRadio.Checked = true;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleBillingBillsAccountsForm.ShortSaleBillingRateChangeInputForm_Load]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}
	
		private void ShortSaleBillingBillsForm_Closed(object sender, System.EventArgs e)
		{
			mainForm.Refresh();
		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void AccountsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{	
				mainForm.RebateAgent.ShortSaleBillingSummaryAccountMethodSet(
					AccountsGrid.Columns["GroupCode"].Text,
					AccountsGrid.Columns["AccountNumber"].Text,
					bool.Parse(AccountsGrid.Columns["IsOSIBilling"].Value.ToString()),
					bool.Parse(AccountsGrid.Columns["IsPaperBilling"].Value.ToString()),
					mainForm.UserId);
				
				AccountsGrid.Columns["ActUserId"].Text  = mainForm.UserId;
				AccountsGrid.Columns["ActTime"].Value  = DateTime.Now.ToString();
						
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				e.Cancel = true;
			}
		}

		private void AccountsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{
				AccountsGrid.UpdateData();
			}
		}

		private void AccountsGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "ActTime":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeFileFormat);
					}
					catch {}
					break;
			}
		}

		private void EmailTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			try
			{
				if (e.KeyChar.Equals((char) 13))
				{
					mainForm.RebateAgent.ShortSaleBillingSummaryAccountMethodSet(
						AccountsGrid.Columns["GroupCode"].Text,
						AccountsGrid.Columns["AccountNumber"].Text,
						bool.Parse(AccountsGrid.Columns["IsOSIBilling"].Value.ToString()),
						bool.Parse(AccountsGrid.Columns["IsPaperBilling"].Value.ToString()),						
						mainForm.UserId);
							
					AccountsGrid.Columns["ActUserId"].Text  = mainForm.UserId;
					AccountsGrid.Columns["ActTime"].Value  = DateTime.Now.ToString();
				
					AccountsGrid.UpdateData();
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void AccountsGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (!paintGroupCode.Equals(AccountsGrid.Columns["GroupCode"].Text))
			{
				paintGroupCode = AccountsGrid.Columns["GroupCode"].Text;
			}
		}

		private void EmailRadio_CheckedChanged(object sender, System.EventArgs e)
		{
			EmailGrid.Visible = true;
			AccountsGrid.Visible = false;

			try
			{
				dataSet = mainForm.RebateAgent.ShortSaleBillingSummaryTradingGroupEmailGet("", mainForm.UtcOffset);
				EmailGrid.SetDataBinding(dataSet, "TradingGroupEmails", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void AccountRadio_CheckedChanged(object sender, System.EventArgs e)
		{
			EmailGrid.Visible = false;
			AccountsGrid.Visible = true;
		}

		private void EmailGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				mainForm.RebateAgent.ShortSaleBillingSummaryTradingGroupEmailSet (
					EmailGrid.Columns["GroupCode"].Text,
					EmailGrid.Columns["EmailAddress"].Text,
					mainForm.UserId);
			
				EmailGrid.Columns["ActUserId"].Text = mainForm.UserId;
				EmailGrid.Columns["ActTime"].Text = DateTime.Now.ToString();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void EmailGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{
				EmailGrid.UpdateData();
			}
		}
	}
}
