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
	public class ShortSaleTradingGroupsPositiveRebateBillingForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ShortSaleTradingGroupsGrid;
		private MainForm mainForm;

		private string tradingGroup = "";

		private DataSet dataSet = null;		
		private DataSet fundingRatesDataSet = null;
		private System.Windows.Forms.ContextMenu MainMenu;
		private System.Windows.Forms.MenuItem Seperator;
		private System.Windows.Forms.MenuItem ExitMenuItem;		
		private System.Windows.Forms.MenuItem TradingGroupsSendToEmailMenuItem;
		private System.Windows.Forms.MenuItem TradingGroupsSendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem TradingGroupsSendToExcelMenuItem;
		private System.Windows.Forms.MenuItem TradingGroupsSendToMenuItem;
		private C1.Win.C1Input.C1Label PositiveRebateMarkUpDefaultLabel;
		private C1.Win.C1Input.C1Label FedFundsMarkUpDefaultLabel;
		private C1.Win.C1Input.C1NumericEdit PositiveRebateMarkUpDefaultNumericEdit;
		private C1.Win.C1Input.C1NumericEdit FedFundsMarkUpDefaultNumericEdit;
		private C1.Win.C1Input.C1NumericEdit LiborFundsMarkUpDefaultNumericEdit;
		private C1.Win.C1Input.C1Label LiborMarkUpDefaultLabel;
		private System.Windows.Forms.MenuItem ManageMenuItem;
		private System.Windows.Forms.MenuItem ManageAccountMenuItem;
		private System.Windows.Forms.MenuItem ManageOfficeCodesMenuItem;
		private ShortSaleTradingGroupsPositiveRebatesAccountManagementForm shortSaleTradingGroupsAccountManagementForm = null;
		private C1.Win.C1Input.C1Label LiborFundingOpenRate;
		private C1.Win.C1Input.C1Label FedFundingOpenRateLabel;
		private C1.Win.C1Input.C1DropDownControl FedFundingOpenRateDropDown;
		private C1.Win.C1Input.C1DropDownControl LiborFundingRateDropDown;
		private ShortSaleTradingGroupsPositiveRebatesOfficeCodeManagementForm shortSaleTradingGroupsOfficeCodeManagementForm = null;

		public ShortSaleTradingGroupsPositiveRebateBillingForm(MainForm mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			
			dataSet = new DataSet();

			try
			{
				ShortSaleTradingGroupsGrid.AllowUpdate = mainForm.AdminAgent.MayEdit(mainForm.UserId,"ShortSaleLists");
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleTradingGroupsPositiveRebateBillingForm));
			this.ShortSaleTradingGroupsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainMenu = new System.Windows.Forms.ContextMenu();
			this.ManageMenuItem = new System.Windows.Forms.MenuItem();
			this.ManageAccountMenuItem = new System.Windows.Forms.MenuItem();
			this.ManageOfficeCodesMenuItem = new System.Windows.Forms.MenuItem();
			this.TradingGroupsSendToMenuItem = new System.Windows.Forms.MenuItem();
			this.TradingGroupsSendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.TradingGroupsSendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.TradingGroupsSendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.Seperator = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.PositiveRebateMarkUpDefaultNumericEdit = new C1.Win.C1Input.C1NumericEdit();
			this.PositiveRebateMarkUpDefaultLabel = new C1.Win.C1Input.C1Label();
			this.FedFundsMarkUpDefaultNumericEdit = new C1.Win.C1Input.C1NumericEdit();
			this.FedFundsMarkUpDefaultLabel = new C1.Win.C1Input.C1Label();
			this.LiborFundsMarkUpDefaultNumericEdit = new C1.Win.C1Input.C1NumericEdit();
			this.LiborMarkUpDefaultLabel = new C1.Win.C1Input.C1Label();
			this.LiborFundingOpenRate = new C1.Win.C1Input.C1Label();
			this.FedFundingOpenRateLabel = new C1.Win.C1Input.C1Label();
			this.FedFundingOpenRateDropDown = new C1.Win.C1Input.C1DropDownControl();
			this.LiborFundingRateDropDown = new C1.Win.C1Input.C1DropDownControl();
			((System.ComponentModel.ISupportInitialize)(this.ShortSaleTradingGroupsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PositiveRebateMarkUpDefaultNumericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PositiveRebateMarkUpDefaultLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.FedFundsMarkUpDefaultNumericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.FedFundsMarkUpDefaultLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LiborFundsMarkUpDefaultNumericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LiborMarkUpDefaultLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LiborFundingOpenRate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.FedFundingOpenRateLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.FedFundingOpenRateDropDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LiborFundingRateDropDown)).BeginInit();
			this.SuspendLayout();
			// 
			// ShortSaleTradingGroupsGrid
			// 
			this.ShortSaleTradingGroupsGrid.CaptionHeight = 17;
			this.ShortSaleTradingGroupsGrid.ContextMenu = this.MainMenu;
			this.ShortSaleTradingGroupsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ShortSaleTradingGroupsGrid.ExtendRightColumn = true;
			this.ShortSaleTradingGroupsGrid.FetchRowStyles = true;
			this.ShortSaleTradingGroupsGrid.FilterBar = true;
			this.ShortSaleTradingGroupsGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ShortSaleTradingGroupsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ShortSaleTradingGroupsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.ShortSaleTradingGroupsGrid.Location = new System.Drawing.Point(1, 80);
			this.ShortSaleTradingGroupsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.ShortSaleTradingGroupsGrid.Name = "ShortSaleTradingGroupsGrid";
			this.ShortSaleTradingGroupsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ShortSaleTradingGroupsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ShortSaleTradingGroupsGrid.PreviewInfo.ZoomFactor = 75;
			this.ShortSaleTradingGroupsGrid.RecordSelectorWidth = 16;
			this.ShortSaleTradingGroupsGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.ShortSaleTradingGroupsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ShortSaleTradingGroupsGrid.RowHeight = 16;
			this.ShortSaleTradingGroupsGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.ShortSaleTradingGroupsGrid.Size = new System.Drawing.Size(1106, 392);
			this.ShortSaleTradingGroupsGrid.TabIndex = 0;
			this.ShortSaleTradingGroupsGrid.Text = "Trading Groups";
			this.ShortSaleTradingGroupsGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.ShortSaleTradingGroupsGrid_FetchRowStyle);
			this.ShortSaleTradingGroupsGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ShortSaleTradingGroupsGrid_BeforeUpdate);
			this.ShortSaleTradingGroupsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.ShortSaleTradingGroupsGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ShortSaleTradingGroupsGrid_KeyPress);
			this.ShortSaleTradingGroupsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Group Code\"" +
				" DataField=\"GroupCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"Group Name\" DataField=\"GroupName\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"ActUserId\" DataField=\"ActUserId\">" +
				"<ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act T" +
				"ime\" DataField=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInf" +
				"o /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"IsActive\" DataField=\"IsActiv" +
				"e\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Ne" +
				"gative Rebate MarkUp\" DataField=\"NegativeRebateMarkUp\" NumberFormat=\"FormatText " +
				"Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption" +
				"=\"Positive Rebate MarkUp\" DataField=\"PositiveRebateMarkUp\" NumberFormat=\"FormatT" +
				"ext Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
				"tion=\"Fed Funds MarkUp\" DataField=\"FedFundsMarkUp\" NumberFormat=\"FormatText Even" +
				"t\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Au" +
				"toEmail\" DataField=\"AutoEmail\"><ValueItems /><GroupInfo /></C1DataColumn><C1Data" +
				"Column Level=\"0\" Caption=\"Libor Funds MarkUp\" DataField=\"LiborFundsMarkUp\" Numbe" +
				"rFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColu" +
				"mn Level=\"0\" Caption=\"B\" DataField=\"NegativeRebateBill\"><ValueItems Presentation" +
				"=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"B\" Da" +
				"taField=\"PositiveRebateBill\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo />" +
				"</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"MinPrice\" DataField=\"MinPrice\"><" +
				"ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"AutoAp" +
				"provalMax\" DataField=\"AutoApprovalMax\"><ValueItems /><GroupInfo /></C1DataColumn" +
				"><C1DataColumn Level=\"0\" Caption=\"PremiumMin\" DataField=\"PremiumMin\"><ValueItems" +
				" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"PremiumMax\" Data" +
				"Field=\"PremiumMax\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level" +
				"=\"0\" Caption=\"AutoEmail\" DataField=\"AutoEmail\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"EmailAddress\" DataField=\"EmailAddress\"" +
				"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Last" +
				"EmailDate\" DataField=\"LastEmailDate\"><ValueItems /><GroupInfo /></C1DataColumn><" +
				"C1DataColumn Level=\"0\" Caption=\"\" DataField=\"PositiveRebateMarkUpCode\"><ValueIte" +
				"ms /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.De" +
				"sign.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highli" +
				"ght;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Style119{" +
				"}Style118{}Style78{AlignHorz:Center;}Style79{AlignHorz:Center;}Style85{AlignHorz" +
				":Center;}Editor{}Style117{}Style116{}Style72{AlignHorz:Near;}Style73{AlignHorz:F" +
				"ar;}Style70{}Style71{}Style76{}Style77{}Style74{}Style75{}Style84{AlignHorz:Cent" +
				"er;}Style87{}Style86{}Style81{}Style80{}Style83{}Style82{}Style89{}Style88{}Styl" +
				"e108{AlignHorz:Near;}Style109{AlignHorz:Near;}Style132{AlignHorz:Center;}Style10" +
				"4{}Style105{}Style106{}Style107{}Style100{}Style101{}Style102{AlignHorz:Near;}St" +
				"yle103{AlignHorz:Near;}Style94{}Style95{}Style96{AlignHorz:Near;}Style97{AlignHo" +
				"rz:Near;}Style90{AlignHorz:Near;}Style91{AlignHorz:Near;}Style92{}Style93{}Style" +
				"131{}Style98{}Style99{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, " +
				"1, 1;ForeColor:ControlText;AlignVert:Center;}Style18{}Style14{AlignHorz:Near;}St" +
				"yle19{}Style137{}Style136{}Style135{}Style17{}Style133{AlignHorz:Center;}Style15" +
				"{Font:Verdana, 8.25pt, style=Bold;AlignHorz:Near;ForeColor:Highlight;}Style16{}S" +
				"tyle130{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Selected{ForeColor:H" +
				"ighlightText;BackColor:Highlight;}Style125{}Style122{}Style123{}Style121{AlignHo" +
				"rz:Near;}Style25{}Style29{}Style128{}Style129{}Style126{AlignHorz:Near;}Style127" +
				"{AlignHorz:Near;}Style124{}Style28{}Style27{}Style26{}Style120{AlignHorz:Near;}S" +
				"tyle24{}Style23{}Style22{}Style21{AlignHorz:Near;}Style20{AlignHorz:Near;}OddRow" +
				"{}Style38{}Style39{}Style36{AlignHorz:Near;}FilterBar{BackColor:SeaShell;}Style3" +
				"7{AlignHorz:Far;}Style34{}Style35{}Style32{}Style33{}Style49{AlignHorz:Near;}Sty" +
				"le48{AlignHorz:Near;}Style30{AlignHorz:Near;}Style31{AlignHorz:Far;}Normal{Font:" +
				"Verdana, 8.25pt;}Style41{}Style40{}Style43{AlignHorz:Near;}Style42{AlignHorz:Nea" +
				"r;}Style45{}Style44{}Style47{}Style46{}EvenRow{BackColor:Aqua;}Style51{}Style9{}" +
				"Style8{}Style59{}Style5{}Style4{}Style7{}Style6{}Style58{}RecordSelector{AlignIm" +
				"age:Center;}Style3{}Style2{}Style50{}Footer{}Style52{}Style53{}Style54{AlignHorz" +
				":Near;}Style55{AlignHorz:Near;}Style56{}Style57{}Style63{}Caption{AlignHorz:Cent" +
				"er;}Style64{}Style112{}Style69{}Style68{}Group{AlignVert:Center;Border:None,,0, " +
				"0, 0, 0;BackColor:ControlDark;}Style1{}Style134{}Style62{}Style61{AlignHorz:Far;" +
				"}Style60{AlignHorz:Near;}Style67{AlignHorz:Near;}Style66{AlignHorz:Near;}Style65" +
				"{}Style115{AlignHorz:Near;}Style114{AlignHorz:Near;}Style111{}Style110{}Style113" +
				"{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarSt" +
				"yle=\"Always\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHei" +
				"ght=\"17\" ExtendRightColumn=\"True\" FetchRowStyles=\"True\" FilterBar=\"True\" Marquee" +
				"Style=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScr" +
				"ollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10" +
				"\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me" +
				"=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle paren" +
				"t=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle" +
				" parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Sty" +
				"le7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRo" +
				"w\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><Se" +
				"lectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /" +
				"><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><St" +
				"yle parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><" +
				"EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=" +
				"\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visi" +
				"ble><ColumnDivider>LightGray,Single</ColumnDivider><Height>16</Height><Locked>Tr" +
				"ue</Locked><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pare" +
				"nt=\"Style2\" me=\"Style20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle pa" +
				"rent=\"Style3\" me=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupH" +
				"eaderStyle parent=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"" +
				"Style24\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider" +
				"><Width>168</Width><Height>16</Height><Locked>True</Locked><DCIdx>1</DCIdx></C1D" +
				"isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style30\" /><Styl" +
				"e parent=\"Style1\" me=\"Style31\" /><FooterStyle parent=\"Style3\" me=\"Style32\" /><Ed" +
				"itorStyle parent=\"Style5\" me=\"Style33\" /><GroupHeaderStyle parent=\"Style1\" me=\"S" +
				"tyle35\" /><GroupFooterStyle parent=\"Style1\" me=\"Style34\" /><ColumnDivider>LightG" +
				"ray,Single</ColumnDivider><Width>190</Width><Height>16</Height><DCIdx>5</DCIdx><" +
				"/C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style78\" /><" +
				"Style parent=\"Style1\" me=\"Style79\" /><FooterStyle parent=\"Style3\" me=\"Style80\" /" +
				"><EditorStyle parent=\"Style5\" me=\"Style81\" /><GroupHeaderStyle parent=\"Style1\" m" +
				"e=\"Style83\" /><GroupFooterStyle parent=\"Style1\" me=\"Style82\" /><ColumnDivider>Li" +
				"ghtGray,Single</ColumnDivider><Width>21</Width><Height>16</Height><DCIdx>10</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style84\"" +
				" /><Style parent=\"Style1\" me=\"Style85\" /><FooterStyle parent=\"Style3\" me=\"Style8" +
				"6\" /><EditorStyle parent=\"Style5\" me=\"Style87\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style89\" /><GroupFooterStyle parent=\"Style1\" me=\"Style88\" /><Visible>True" +
				"</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>21</Width><Heigh" +
				"t>16</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
				"arent=\"Style2\" me=\"Style36\" /><Style parent=\"Style1\" me=\"Style37\" /><FooterStyle" +
				" parent=\"Style3\" me=\"Style38\" /><EditorStyle parent=\"Style5\" me=\"Style39\" /><Gro" +
				"upHeaderStyle parent=\"Style1\" me=\"Style41\" /><GroupFooterStyle parent=\"Style1\" m" +
				"e=\"Style40\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivi" +
				"der><Width>190</Width><Height>16</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1Di" +
				"splayColumn><HeadingStyle parent=\"Style2\" me=\"Style132\" /><Style parent=\"Style1\"" +
				" me=\"Style133\" /><FooterStyle parent=\"Style3\" me=\"Style134\" /><EditorStyle paren" +
				"t=\"Style5\" me=\"Style135\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style137\" /><Gr" +
				"oupFooterStyle parent=\"Style1\" me=\"Style136\" /><Visible>True</Visible><ColumnDiv" +
				"ider>Gainsboro,Single</ColumnDivider><Width>25</Width><Height>16</Height><DCIdx>" +
				"19</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle60\" /><Style parent=\"Style1\" me=\"Style61\" /><FooterStyle parent=\"Style3\" me=" +
				"\"Style62\" /><EditorStyle parent=\"Style5\" me=\"Style63\" /><GroupHeaderStyle parent" +
				"=\"Style1\" me=\"Style65\" /><GroupFooterStyle parent=\"Style1\" me=\"Style64\" /><Visib" +
				"le>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>171</Widt" +
				"h><Height>16</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><Heading" +
				"Style parent=\"Style2\" me=\"Style72\" /><Style parent=\"Style1\" me=\"Style73\" /><Foot" +
				"erStyle parent=\"Style3\" me=\"Style74\" /><EditorStyle parent=\"Style5\" me=\"Style75\"" +
				" /><GroupHeaderStyle parent=\"Style1\" me=\"Style77\" /><GroupFooterStyle parent=\"St" +
				"yle1\" me=\"Style76\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</Col" +
				"umnDivider><Width>171</Width><Height>16</Height><DCIdx>9</DCIdx></C1DisplayColum" +
				"n><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style48\" /><Style parent=\"S" +
				"tyle1\" me=\"Style49\" /><FooterStyle parent=\"Style3\" me=\"Style50\" /><EditorStyle p" +
				"arent=\"Style5\" me=\"Style51\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style53\" /><" +
				"GroupFooterStyle parent=\"Style1\" me=\"Style52\" /><Visible>True</Visible><ColumnDi" +
				"vider>LightGray,Single</ColumnDivider><Height>16</Height><Locked>True</Locked><D" +
				"CIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
				"e=\"Style54\" /><Style parent=\"Style1\" me=\"Style55\" /><FooterStyle parent=\"Style3\"" +
				" me=\"Style56\" /><EditorStyle parent=\"Style5\" me=\"Style57\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style59\" /><GroupFooterStyle parent=\"Style1\" me=\"Style58\" /><V" +
				"isible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Height>16</" +
				"Height><Locked>True</Locked><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style2\" me=\"Style42\" /><Style parent=\"Style1\" me=\"Style43\" " +
				"/><FooterStyle parent=\"Style3\" me=\"Style44\" /><EditorStyle parent=\"Style5\" me=\"S" +
				"tyle45\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style47\" /><GroupFooterStyle par" +
				"ent=\"Style1\" me=\"Style46\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Heigh" +
				"t>16</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pa" +
				"rent=\"Style2\" me=\"Style66\" /><Style parent=\"Style1\" me=\"Style67\" /><FooterStyle " +
				"parent=\"Style3\" me=\"Style68\" /><EditorStyle parent=\"Style5\" me=\"Style69\" /><Grou" +
				"pHeaderStyle parent=\"Style1\" me=\"Style71\" /><GroupFooterStyle parent=\"Style1\" me" +
				"=\"Style70\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>16</Height><D" +
				"CIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
				"e=\"Style90\" /><Style parent=\"Style1\" me=\"Style91\" /><FooterStyle parent=\"Style3\"" +
				" me=\"Style92\" /><EditorStyle parent=\"Style5\" me=\"Style93\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style95\" /><GroupFooterStyle parent=\"Style1\" me=\"Style94\" /><C" +
				"olumnDivider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>12</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style96\" />" +
				"<Style parent=\"Style1\" me=\"Style97\" /><FooterStyle parent=\"Style3\" me=\"Style98\" " +
				"/><EditorStyle parent=\"Style5\" me=\"Style99\" /><GroupHeaderStyle parent=\"Style1\" " +
				"me=\"Style101\" /><GroupFooterStyle parent=\"Style1\" me=\"Style100\" /><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>13</DCIdx></C1DisplayC" +
				"olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style102\" /><Style pare" +
				"nt=\"Style1\" me=\"Style103\" /><FooterStyle parent=\"Style3\" me=\"Style104\" /><Editor" +
				"Style parent=\"Style5\" me=\"Style105\" /><GroupHeaderStyle parent=\"Style1\" me=\"Styl" +
				"e107\" /><GroupFooterStyle parent=\"Style1\" me=\"Style106\" /><ColumnDivider>DarkGra" +
				"y,Single</ColumnDivider><Height>16</Height><DCIdx>14</DCIdx></C1DisplayColumn><C" +
				"1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style108\" /><Style parent=\"Styl" +
				"e1\" me=\"Style109\" /><FooterStyle parent=\"Style3\" me=\"Style110\" /><EditorStyle pa" +
				"rent=\"Style5\" me=\"Style111\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style113\" />" +
				"<GroupFooterStyle parent=\"Style1\" me=\"Style112\" /><ColumnDivider>DarkGray,Single" +
				"</ColumnDivider><Height>16</Height><DCIdx>15</DCIdx></C1DisplayColumn><C1Display" +
				"Column><HeadingStyle parent=\"Style2\" me=\"Style114\" /><Style parent=\"Style1\" me=\"" +
				"Style115\" /><FooterStyle parent=\"Style3\" me=\"Style116\" /><EditorStyle parent=\"St" +
				"yle5\" me=\"Style117\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style119\" /><GroupFo" +
				"oterStyle parent=\"Style1\" me=\"Style118\" /><ColumnDivider>DarkGray,Single</Column" +
				"Divider><Height>16</Height><DCIdx>16</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style2\" me=\"Style120\" /><Style parent=\"Style1\" me=\"Style121" +
				"\" /><FooterStyle parent=\"Style3\" me=\"Style122\" /><EditorStyle parent=\"Style5\" me" +
				"=\"Style123\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style125\" /><GroupFooterStyl" +
				"e parent=\"Style1\" me=\"Style124\" /><ColumnDivider>DarkGray,Single</ColumnDivider>" +
				"<Height>16</Height><DCIdx>17</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingS" +
				"tyle parent=\"Style2\" me=\"Style126\" /><Style parent=\"Style1\" me=\"Style127\" /><Foo" +
				"terStyle parent=\"Style3\" me=\"Style128\" /><EditorStyle parent=\"Style5\" me=\"Style1" +
				"29\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style131\" /><GroupFooterStyle parent" +
				"=\"Style1\" me=\"Style130\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>" +
				"16</Height><DCIdx>18</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 1" +
				"102, 388</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView><" +
				"/Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"H" +
				"eading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Capt" +
				"ion\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Select" +
				"ed\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightR" +
				"ow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /" +
				"><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"Filter" +
				"Bar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSpl" +
				"its><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</D" +
				"efaultRecSelWidth><ClientArea>0, 0, 1102, 388</ClientArea><PrintPageHeaderStyle " +
				"parent=\"\" me=\"Style28\" /><PrintPageFooterStyle parent=\"\" me=\"Style29\" /></Blob>";
			// 
			// MainMenu
			// 
			this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																						 this.ManageMenuItem,
																																						 this.TradingGroupsSendToMenuItem,
																																						 this.Seperator,
																																						 this.ExitMenuItem});
			// 
			// ManageMenuItem
			// 
			this.ManageMenuItem.Index = 0;
			this.ManageMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.ManageAccountMenuItem,
																																									 this.ManageOfficeCodesMenuItem});
			this.ManageMenuItem.Text = "Manage";
			// 
			// ManageAccountMenuItem
			// 
			this.ManageAccountMenuItem.Index = 0;
			this.ManageAccountMenuItem.Text = "Account (s)";
			this.ManageAccountMenuItem.Click += new System.EventHandler(this.ManageAccountsMenuItem_Click);
			// 
			// ManageOfficeCodesMenuItem
			// 
			this.ManageOfficeCodesMenuItem.Index = 1;
			this.ManageOfficeCodesMenuItem.Text = "Office Code (s)";
			this.ManageOfficeCodesMenuItem.Click += new System.EventHandler(this.ManageOfficeCodesMenuItem_Click);
			// 
			// TradingGroupsSendToMenuItem
			// 
			this.TradingGroupsSendToMenuItem.Index = 1;
			this.TradingGroupsSendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																																this.TradingGroupsSendToEmailMenuItem,
																																																this.TradingGroupsSendToClipboardMenuItem,
																																																this.TradingGroupsSendToExcelMenuItem});
			this.TradingGroupsSendToMenuItem.Text = "Trading Groups Send To";
			// 
			// TradingGroupsSendToEmailMenuItem
			// 
			this.TradingGroupsSendToEmailMenuItem.Index = 0;
			this.TradingGroupsSendToEmailMenuItem.Text = "Email";
			this.TradingGroupsSendToEmailMenuItem.Click += new System.EventHandler(this.TradingGroupsSendToEmailMenuItem_Click);
			// 
			// TradingGroupsSendToClipboardMenuItem
			// 
			this.TradingGroupsSendToClipboardMenuItem.Index = 1;
			this.TradingGroupsSendToClipboardMenuItem.Text = "Clipboard";
			this.TradingGroupsSendToClipboardMenuItem.Click += new System.EventHandler(this.TradingGroupsSendToClipboardMenuItem_Click);
			// 
			// TradingGroupsSendToExcelMenuItem
			// 
			this.TradingGroupsSendToExcelMenuItem.Index = 2;
			this.TradingGroupsSendToExcelMenuItem.Text = "Excel";
			this.TradingGroupsSendToExcelMenuItem.Click += new System.EventHandler(this.TradingGroupsSendToExcelMenuItem_Click);
			// 
			// Seperator
			// 
			this.Seperator.Index = 2;
			this.Seperator.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 3;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// PositiveRebateMarkUpDefaultNumericEdit
			// 
			this.PositiveRebateMarkUpDefaultNumericEdit.Location = new System.Drawing.Point(176, 9);
			this.PositiveRebateMarkUpDefaultNumericEdit.Name = "PositiveRebateMarkUpDefaultNumericEdit";
			this.PositiveRebateMarkUpDefaultNumericEdit.Size = new System.Drawing.Size(152, 21);
			this.PositiveRebateMarkUpDefaultNumericEdit.TabIndex = 2;
			this.PositiveRebateMarkUpDefaultNumericEdit.Tag = null;
			this.PositiveRebateMarkUpDefaultNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.PositiveRebateMarkUpDefaultNumericEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PositiveRebateMarkUpDefaultNumericEdit_KeyPress);
			// 
			// PositiveRebateMarkUpDefaultLabel
			// 
			this.PositiveRebateMarkUpDefaultLabel.Location = new System.Drawing.Point(8, 8);
			this.PositiveRebateMarkUpDefaultLabel.Name = "PositiveRebateMarkUpDefaultLabel";
			this.PositiveRebateMarkUpDefaultLabel.Size = new System.Drawing.Size(168, 23);
			this.PositiveRebateMarkUpDefaultLabel.TabIndex = 3;
			this.PositiveRebateMarkUpDefaultLabel.Tag = null;
			this.PositiveRebateMarkUpDefaultLabel.Text = "House Borrow Average Less:";
			this.PositiveRebateMarkUpDefaultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.PositiveRebateMarkUpDefaultLabel.TextDetached = true;
			// 
			// FedFundsMarkUpDefaultNumericEdit
			// 
			this.FedFundsMarkUpDefaultNumericEdit.Location = new System.Drawing.Point(476, 9);
			this.FedFundsMarkUpDefaultNumericEdit.Name = "FedFundsMarkUpDefaultNumericEdit";
			this.FedFundsMarkUpDefaultNumericEdit.Size = new System.Drawing.Size(108, 21);
			this.FedFundsMarkUpDefaultNumericEdit.TabIndex = 0;
			this.FedFundsMarkUpDefaultNumericEdit.Tag = null;
			this.FedFundsMarkUpDefaultNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.FedFundsMarkUpDefaultNumericEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FedFundsMarkUpDefaultNumericEdit_KeyPress);
			// 
			// FedFundsMarkUpDefaultLabel
			// 
			this.FedFundsMarkUpDefaultLabel.Location = new System.Drawing.Point(348, 8);
			this.FedFundsMarkUpDefaultLabel.Name = "FedFundsMarkUpDefaultLabel";
			this.FedFundsMarkUpDefaultLabel.Size = new System.Drawing.Size(132, 23);
			this.FedFundsMarkUpDefaultLabel.TabIndex = 1;
			this.FedFundsMarkUpDefaultLabel.Tag = null;
			this.FedFundsMarkUpDefaultLabel.Text = "Daily Fed Funds Less:";
			this.FedFundsMarkUpDefaultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.FedFundsMarkUpDefaultLabel.TextDetached = true;
			// 
			// LiborFundsMarkUpDefaultNumericEdit
			// 
			this.LiborFundsMarkUpDefaultNumericEdit.Location = new System.Drawing.Point(744, 9);
			this.LiborFundsMarkUpDefaultNumericEdit.Name = "LiborFundsMarkUpDefaultNumericEdit";
			this.LiborFundsMarkUpDefaultNumericEdit.Size = new System.Drawing.Size(108, 21);
			this.LiborFundsMarkUpDefaultNumericEdit.TabIndex = 6;
			this.LiborFundsMarkUpDefaultNumericEdit.Tag = null;
			this.LiborFundsMarkUpDefaultNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.LiborFundsMarkUpDefaultNumericEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LiborFundsMarkUpDefaultNumericEdit_KeyPress);
			// 
			// LiborMarkUpDefaultLabel
			// 
			this.LiborMarkUpDefaultLabel.Location = new System.Drawing.Point(608, 8);
			this.LiborMarkUpDefaultLabel.Name = "LiborMarkUpDefaultLabel";
			this.LiborMarkUpDefaultLabel.Size = new System.Drawing.Size(140, 23);
			this.LiborMarkUpDefaultLabel.TabIndex = 7;
			this.LiborMarkUpDefaultLabel.Tag = null;
			this.LiborMarkUpDefaultLabel.Text = "Daily Libor Funds Less:";
			this.LiborMarkUpDefaultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LiborMarkUpDefaultLabel.TextDetached = true;
			// 
			// LiborFundingOpenRate
			// 
			this.LiborFundingOpenRate.Location = new System.Drawing.Point(672, 43);
			this.LiborFundingOpenRate.Name = "LiborFundingOpenRate";
			this.LiborFundingOpenRate.Size = new System.Drawing.Size(72, 23);
			this.LiborFundingOpenRate.TabIndex = 16;
			this.LiborFundingOpenRate.Tag = null;
			this.LiborFundingOpenRate.Text = "Daily Libor:";
			this.LiborFundingOpenRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LiborFundingOpenRate.TextDetached = true;
			// 
			// FedFundingOpenRateLabel
			// 
			this.FedFundingOpenRateLabel.Location = new System.Drawing.Point(376, 43);
			this.FedFundingOpenRateLabel.Name = "FedFundingOpenRateLabel";
			this.FedFundingOpenRateLabel.TabIndex = 15;
			this.FedFundingOpenRateLabel.Tag = null;
			this.FedFundingOpenRateLabel.Text = "Daily Fed Funds:";
			this.FedFundingOpenRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.FedFundingOpenRateLabel.TextDetached = true;
			// 
			// FedFundingOpenRateDropDown
			// 
			this.FedFundingOpenRateDropDown.DropDownFormAlign = C1.Win.C1Input.DropDownFormAlignmentEnum.Right;
			this.FedFundingOpenRateDropDown.DropDownFormClassName = "Anetics.Medalist.InventoryFundingRatesHistoryDropDown";
			this.FedFundingOpenRateDropDown.ForeColor = System.Drawing.SystemColors.WindowText;
			this.FedFundingOpenRateDropDown.Location = new System.Drawing.Point(476, 44);
			this.FedFundingOpenRateDropDown.Name = "FedFundingOpenRateDropDown";
			this.FedFundingOpenRateDropDown.Size = new System.Drawing.Size(108, 21);
			this.FedFundingOpenRateDropDown.TabIndex = 58;
			this.FedFundingOpenRateDropDown.Tag = null;
			this.FedFundingOpenRateDropDown.TextDetached = true;
			this.FedFundingOpenRateDropDown.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.FedFundingOpenRateDropDown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FedFundingOpenRateDropDown_KeyPress);
			this.FedFundingOpenRateDropDown.DropDownOpened += new System.EventHandler(this.FedFundingOpenRateDropDown_DropDownOpened);
			// 
			// LiborFundingRateDropDown
			// 
			this.LiborFundingRateDropDown.DropDownFormAlign = C1.Win.C1Input.DropDownFormAlignmentEnum.Right;
			this.LiborFundingRateDropDown.DropDownFormClassName = "Anetics.Medalist.InventoryFundingRatesHistoryDropDown";
			this.LiborFundingRateDropDown.Location = new System.Drawing.Point(744, 44);
			this.LiborFundingRateDropDown.Name = "LiborFundingRateDropDown";
			this.LiborFundingRateDropDown.Size = new System.Drawing.Size(108, 21);
			this.LiborFundingRateDropDown.TabIndex = 59;
			this.LiborFundingRateDropDown.Tag = null;
			this.LiborFundingRateDropDown.TextDetached = true;
			this.LiborFundingRateDropDown.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.LiborFundingRateDropDown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FedFundingOpenRateDropDown_KeyPress);
			this.LiborFundingRateDropDown.DropDownOpened += new System.EventHandler(this.LiborFundingRateDropDown_DropDownOpened);
			// 
			// ShortSaleTradingGroupsPositiveRebateBillingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1108, 473);
			this.Controls.Add(this.LiborFundingRateDropDown);
			this.Controls.Add(this.FedFundingOpenRateDropDown);
			this.Controls.Add(this.LiborFundingOpenRate);
			this.Controls.Add(this.FedFundingOpenRateLabel);
			this.Controls.Add(this.LiborFundsMarkUpDefaultNumericEdit);
			this.Controls.Add(this.LiborMarkUpDefaultLabel);
			this.Controls.Add(this.FedFundsMarkUpDefaultNumericEdit);
			this.Controls.Add(this.FedFundsMarkUpDefaultLabel);
			this.Controls.Add(this.PositiveRebateMarkUpDefaultNumericEdit);
			this.Controls.Add(this.PositiveRebateMarkUpDefaultLabel);
			this.Controls.Add(this.ShortSaleTradingGroupsGrid);
			this.DockPadding.Bottom = 1;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 80;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ShortSaleTradingGroupsPositiveRebateBillingForm";
			this.Text = "ShortSale - Trading Groups - Positive Rebate Billing";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ShortSaleTradingGroupsPositiveRebateBillingForm_Closing);
			this.Load += new System.EventHandler(this.ShortSaleTradingGroupsPositiveRebateBillingForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.ShortSaleTradingGroupsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PositiveRebateMarkUpDefaultNumericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PositiveRebateMarkUpDefaultLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.FedFundsMarkUpDefaultNumericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.FedFundsMarkUpDefaultLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LiborFundsMarkUpDefaultNumericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LiborMarkUpDefaultLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LiborFundingOpenRate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.FedFundingOpenRateLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.FedFundingOpenRateDropDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LiborFundingRateDropDown)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

    
		public void ListLoad(string effectDate)
		{
			try
			{
				mainForm.Alert("Please wait... Loading trading groups data...", PilotState.Unknown);
				this.Cursor = Cursors.WaitCursor;
				this.Refresh();

				dataSet = mainForm.ShortSaleAgent.TradingGroupsGet(effectDate, mainForm.UtcOffset);								
																
				ShortSaleTradingGroupsGrid.SetDataBinding(dataSet, "TradingGroups", true);						
			
				mainForm.Alert("Loading trading groups data... Done!", PilotState.Normal);			
			
				PositiveRebateMarkUpDefaultNumericEdit.Value = mainForm.ServiceAgent.KeyValueGet("ShortSalePositiveDefaultMarkUp", "0");
				FedFundsMarkUpDefaultNumericEdit.Value =  mainForm.ServiceAgent.KeyValueGet("ShortSaleFedFundsDefaultMarkUp", "0");			
				LiborFundsMarkUpDefaultNumericEdit.Value =  mainForm.ServiceAgent.KeyValueGet("ShortSaleLiborFundsDefaultMarkUp", "0");					
				
				fundingRatesDataSet = mainForm.ShortSaleAgent.InventoryFundingRatesGet(effectDate, mainForm.UtcOffset);								
				
				try
				{
					FedFundingOpenRateDropDown.Text = decimal.Parse(fundingRatesDataSet.Tables["InventoryFundingRates"].Rows[0]["FedFundingOpenRate"].ToString()).ToString("##0.000");				
					LiborFundingRateDropDown.Text = decimal.Parse(fundingRatesDataSet.Tables["InventoryFundingRates"].Rows[0]["LiborFundingRate"].ToString()).ToString("##0.000");
				}
				catch
				{
					FedFundingOpenRateDropDown.Text = "0";
					LiborFundingRateDropDown.Text = "0";
				}
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [ShortSaleTradingGroupsPositiveRebateBillingForm.ListLoad]", Log.Error, 1); 
			}

			this.Cursor = Cursors.Default;
		}
		
		
		private void ShortSaleTradingGroupsPositiveRebateBillingForm_Load(object sender, System.EventArgs e)
		{
			int height = mainForm.Height / 2;
			int width  = mainForm.Width / 2;
      
			try
			{
				this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
				this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
				this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
				this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));
				ListLoad(mainForm.ServiceAgent.BizDate());      
			}
			catch(Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);        
			}
		}

		private void ShortSaleTradingGroupsPositiveRebateBillingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
				RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
				RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
				RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
			}

			try
			{
				shortSaleTradingGroupsAccountManagementForm.Close();
			} 
			catch{}

			try
			{
				shortSaleTradingGroupsOfficeCodeManagementForm.Close();
			}
			catch {}

		}

		private void KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			/*if (e.KeyChar.Equals((char) 13))
			{
				try
				{
					mainForm.ShortSaleAgent.InventoryFundingRateSet(
						FedFundingOpenRateCombo.Text,
						LiborFundingRateCombo.Text);
				}
				catch (Exception error)
				{
					mainForm.Alert(error.Message, PilotState.RunFault);
					Log.Write(error.Message + " [ShortSaleTradingGroupsPositiveRebateBillingForm.KeyPress]", Log.Error, 1); 
				}
			}		*/
		}

   
		private void FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "NegativeRebateMarkUp":
				case "PositiveRebateMarkUp":
				case "FedFundsMarkUp":
				case "LiborFundsMarkUp":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.000");
					}
					catch {}
					break;			
				
				case "ActTime":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeFormat);
					}
					catch {}
					break;
			}		
		}

		private void FedFundingOpenRateDropDown_DropDownOpened(object sender, System.EventArgs e)
		{			
			mainForm.ShowFedFunds = true;
		}

		/*private void LiborFundingRateCombo_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
		{
			DataSet liborFundingDataSet = new DataSet();

			try
			{
				liborFundingDataSet = mainForm.ShortSaleAgent.InventoryFundingRatesHistoryGet(int.Parse(mainForm.ServiceAgent.KeyValueGet("InventoryFundingRatesHistoryDayCount", "10")));				
				LiborFundingRateCombo.HoldFields();
				LiborFundingRateCombo.DataSource = liborFundingDataSet.Tables["InventoryFundingRatesHistory"];				
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void LiborFundingRateCombo_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
		{			
				switch (LiborFundingRateCombo.Columns[e.ColIndex].DataField)
				{					
					case "LiborFundingRate":
						try
						{
							e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.000");
						}
						catch {}
						break;			
				
					case "BizDate":
						try
						{
							e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
						}
						catch {}
						break;
				}			
		}

		private void FedFundingOpenRateCombo_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
		{
			switch (FedFundingOpenRateCombo.Columns[e.ColIndex].DataField)
			{
					case "FedFundingOpenRate":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.000");
					}
					catch {}
					break;			
				
				case "BizDate":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateFormat);
					}
					catch {}
					break;
			}		
		}

		private void FedFundingOpenRateCombo_BeforeOpen(object sender, System.ComponentModel.CancelEventArgs e)
		{
			DataSet fedFundingDataSet = new DataSet();

			try
			{
				fedFundingDataSet = mainForm.ShortSaleAgent.InventoryFundingRatesHistoryGet(int.Parse(mainForm.ServiceAgent.KeyValueGet("InventoryFundingRatesHistoryDayCount", "10")));				
				FedFundingOpenRateCombo.HoldFields();
				FedFundingOpenRateCombo.DataSource = fedFundingDataSet.Tables["InventoryFundingRatesHistory"];				
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}*/

	
		private void ShortSaleTradingGroupsGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{			
			if (!bool.Parse(ShortSaleTradingGroupsGrid.Columns["IsActive"].CellValue(e.Row).ToString()))
			{
				e.CellStyle.BackColor = System.Drawing.Color.LightGray;
			}
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void ManageAccountsMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				shortSaleTradingGroupsAccountManagementForm = new ShortSaleTradingGroupsPositiveRebatesAccountManagementForm(mainForm, ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text);
				shortSaleTradingGroupsAccountManagementForm.MdiParent = mainForm;
				shortSaleTradingGroupsAccountManagementForm.Show();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void TradingGroupsSendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n";

			if (ShortSaleTradingGroupsGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows to copy.", PilotState.Normal);
				return;
			}

			try
			{
				maxTextLength = new int[ShortSaleTradingGroupsGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in ShortSaleTradingGroupsGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in ShortSaleTradingGroupsGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
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

				mainForm.Alert("Total: " + ShortSaleTradingGroupsGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
			}
			catch (Exception error)
			{       
				mainForm.Alert(error.Message, PilotState.Normal);
			}
		}

		private void TradingGroupsSendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\r\n";

			foreach (int row in ShortSaleTradingGroupsGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\r\n";
			}

			Clipboard.SetDataObject(gridData, true);

			mainForm.Alert("Total: " + ShortSaleTradingGroupsGrid.SelectedRows.Count + " items copied to the clipboard.", PilotState.Normal);
	
		}

		private void TradingGroupsSendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			Excel excel = new Excel();	
			excel.ExportGridToExcel(ref ShortSaleTradingGroupsGrid);

			this.Cursor = Cursors.Default;
		}

		private void ShortSaleTradingGroupsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				mainForm.RebateAgent.ShortSaleBillingPositiveRebatesSummaryTradingGroupsSet(
					ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text,
					ShortSaleTradingGroupsGrid.Columns["PositiveRebateMarkUp"].Value.ToString(),
					ShortSaleTradingGroupsGrid.Columns["PositiveRebateMarkUpCode"].Value.ToString(),
					ShortSaleTradingGroupsGrid.Columns["FedFundsMarkUp"].Value.ToString(),
					ShortSaleTradingGroupsGrid.Columns["LiborFundsMarkUp"].Value.ToString(),
					ShortSaleTradingGroupsGrid.Columns["PositiveRebateBill"].Value.ToString(),
					mainForm.UserId);

				ShortSaleTradingGroupsGrid.Columns["ActUserId"].Text = mainForm.UserId;
				ShortSaleTradingGroupsGrid.Columns["ActTime"].Value = DateTime.Now; 
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);  
			}
		}

		private void PositiveRebateMarkUpDefaultNumericEdit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{
				mainForm.ServiceAgent.KeyValueSet("ShortSalePositiveDefaultMarkUp",	PositiveRebateMarkUpDefaultNumericEdit.Value.ToString());
				mainForm.Alert("Set ShortSale Positive Default MarkUp: " + PositiveRebateMarkUpDefaultNumericEdit.Value.ToString(),  PilotState.Normal);
			}
		}

		private void FedFundsMarkUpDefaultNumericEdit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{
				mainForm.ServiceAgent.KeyValueSet("ShortSaleFedFundsDefaultMarkUp", FedFundsMarkUpDefaultNumericEdit.Value.ToString());			
				mainForm.Alert("Set ShortSale Fed Funds Default MarkUp: " + FedFundsMarkUpDefaultNumericEdit.Value.ToString(),  PilotState.Normal);
			}
		}

		private void LiborFundsMarkUpDefaultNumericEdit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{
				mainForm.ServiceAgent.KeyValueSet("ShortSaleLiborFundsDefaultMarkUp", LiborFundsMarkUpDefaultNumericEdit.Value.ToString());			
				mainForm.Alert("Set ShortSale Libor Funds Default MarkUp: " + LiborFundsMarkUpDefaultNumericEdit.Value.ToString(),  PilotState.Normal);
			}
		}
		
		private void ManageOfficeCodesMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				shortSaleTradingGroupsOfficeCodeManagementForm = new ShortSaleTradingGroupsPositiveRebatesOfficeCodeManagementForm(mainForm, ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text);
				shortSaleTradingGroupsOfficeCodeManagementForm.MdiParent = mainForm;
				shortSaleTradingGroupsOfficeCodeManagementForm.Show();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void ShortSaleTradingGroupsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)13))
			{
				ShortSaleTradingGroupsGrid.UpdateData();
			}
		}

		private void LiborFundingRateDropDown_DropDownOpened(object sender, System.EventArgs e)
		{
			mainForm.ShowLiborFunds = true;
		}

		private void FedFundingOpenRateDropDown_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{
				try
				{
					mainForm.ShortSaleAgent.InventoryFundingRateSet(
						FedFundingOpenRateDropDown.Text,
						LiborFundingRateDropDown.Text,
						mainForm.UserId);
					
					mainForm.Alert("Updated funding rates.", PilotState.Normal);
					e.Handled = true;
				}
				catch (Exception error)
				{
					mainForm.Alert(error.Message, PilotState.RunFault);
				}
			}
		}
	}
}
