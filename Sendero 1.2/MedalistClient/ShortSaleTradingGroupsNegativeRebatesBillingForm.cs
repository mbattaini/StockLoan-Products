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
	public class ShortSaleTradingGroupsNegativeRebatesBillingForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ShortSaleTradingGroupsGrid;
		private MainForm mainForm;

		private string tradingGroup = "";

		private DataSet dataSet = null;		
		private System.Windows.Forms.ContextMenu MainMenu;
		private System.Windows.Forms.MenuItem Seperator;
		private System.Windows.Forms.MenuItem ExitMenuItem;		
		private System.Windows.Forms.MenuItem TradingGroupsSendToEmailMenuItem;
		private System.Windows.Forms.MenuItem TradingGroupsSendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem TradingGroupsSendToExcelMenuItem;
		private System.Windows.Forms.MenuItem TradingGroupsSendToMenuItem;
		private C1.Win.C1Input.C1Label NegativeRebateMarkUpDefaultLabel;
		private C1.Win.C1Input.C1NumericEdit NegativeRebateMarkUpDefaultNumericEdit;
		private System.Windows.Forms.MenuItem ManageMenuItem;
		private System.Windows.Forms.MenuItem ManageAccountMenuItem;
		private System.Windows.Forms.MenuItem ManageOfficeCodesMenuItem;
		private ShortSaleTradingGroupsNegativeRebatesAccountManagementForm shortSaleTradingGroupsAccountManagementForm = null;
		private ShortSaleTradingGroupsNegativeRebatesOfficeCodeManagementForm shortSaleTradingGroupsOfficeCodeManagementForm = null;

		public ShortSaleTradingGroupsNegativeRebatesBillingForm(MainForm mainForm)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleTradingGroupsNegativeRebatesBillingForm));
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
			this.NegativeRebateMarkUpDefaultNumericEdit = new C1.Win.C1Input.C1NumericEdit();
			this.NegativeRebateMarkUpDefaultLabel = new C1.Win.C1Input.C1Label();
			((System.ComponentModel.ISupportInitialize)(this.ShortSaleTradingGroupsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NegativeRebateMarkUpDefaultNumericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NegativeRebateMarkUpDefaultLabel)).BeginInit();
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
			this.ShortSaleTradingGroupsGrid.Location = new System.Drawing.Point(1, 40);
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
			this.ShortSaleTradingGroupsGrid.Size = new System.Drawing.Size(774, 432);
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
				"C1DataColumn Level=\"0\" Caption=\"\" DataField=\"NegativeRebateMarkUpCode\"><ValueIte" +
				"ms /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.De" +
				"sign.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highli" +
				"ght;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Style119{" +
				"}Style118{}Style78{AlignHorz:Center;}Style79{AlignHorz:Center;}Style85{AlignHorz" +
				":Center;}Editor{}Style117{}Style116{}Style72{AlignHorz:Near;}Style73{AlignHorz:F" +
				"ar;}Style70{}Style71{}Style76{}Style77{}Style74{}Style75{}Style84{AlignHorz:Cent" +
				"er;}Style87{}Style86{}Style81{}Style80{}Style83{}Style82{}Footer{}Style89{}Style" +
				"88{}Style108{AlignHorz:Near;}Style109{AlignHorz:Near;}Style132{AlignHorz:Near;}S" +
				"tyle104{}Style105{}Style106{}Style107{}Style100{}Style101{}Style102{AlignHorz:Ne" +
				"ar;}Style103{AlignHorz:Near;}Style94{}Style95{}Style96{AlignHorz:Near;}Style97{A" +
				"lignHorz:Near;}Style90{AlignHorz:Near;}Style91{AlignHorz:Near;}Style92{}Style93{" +
				"}Style131{}RecordSelector{AlignImage:Center;}Style98{}Style99{}Heading{Wrap:True" +
				";AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Cont" +
				"rol;}Style18{}Style14{AlignHorz:Near;}Style19{}Style137{}Style136{}Style135{}Sty" +
				"le17{}Style133{AlignHorz:Near;}Style15{Font:Verdana, 8.25pt, style=Bold;AlignHor" +
				"z:Near;ForeColor:Highlight;}Style16{}Style130{}Style10{AlignHorz:Near;}Style11{}" +
				"Style12{}Style13{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style124" +
				"{}Style125{}Style122{}Style123{}Style24{}Style25{}Style128{}Style129{}Style126{A" +
				"lignHorz:Near;}Style127{AlignHorz:Near;}Style29{}Style28{}Style27{}Style26{}Styl" +
				"e120{AlignHorz:Near;}Style121{AlignHorz:Near;}Style23{}Style22{}Style21{AlignHor" +
				"z:Near;}Style20{AlignHorz:Near;}OddRow{}Style110{}Style38{}Style39{}Style36{Alig" +
				"nHorz:Near;}FilterBar{BackColor:SeaShell;}Style37{AlignHorz:Far;}Style34{}Style3" +
				"5{}Style32{}Style33{}Style49{AlignHorz:Near;}Style48{AlignHorz:Near;}Style30{Ali" +
				"gnHorz:Near;}Style31{AlignHorz:Far;}Normal{Font:Verdana, 8.25pt;}Style41{}Style4" +
				"0{}Style43{AlignHorz:Near;}Style42{AlignHorz:Near;}Style45{}Style44{}Style47{}St" +
				"yle46{}EvenRow{BackColor:Aqua;}Style9{}Style8{}Style5{}Style4{}Style7{}Style6{}S" +
				"tyle58{}Style59{}Style3{}Style2{}Style50{}Style51{}Style52{}Style53{}Style54{Ali" +
				"gnHorz:Near;}Style55{AlignHorz:Near;}Style56{}Style57{}Style115{AlignHorz:Near;}" +
				"Style63{}Caption{AlignHorz:Center;}Style112{}Style69{}Style68{}Style1{}Style134{" +
				"}Style62{}Style61{AlignHorz:Far;}Style60{AlignHorz:Near;}Style67{AlignHorz:Near;" +
				"}Style66{AlignHorz:Near;}Style65{}Style64{}Style114{AlignHorz:Near;}Style111{}Gr" +
				"oup{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style113{}</" +
				"Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=" +
				"\"Always\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=" +
				"\"17\" ExtendRightColumn=\"True\" FetchRowStyles=\"True\" FilterBar=\"True\" MarqueeStyl" +
				"e=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollG" +
				"roup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" />" +
				"<EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"St" +
				"yle8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"F" +
				"ooter\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle par" +
				"ent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\"" +
				" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" m" +
				"e=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><Select" +
				"edStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><in" +
				"ternalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style " +
				"parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><Edit" +
				"orStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Sty" +
				"le19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible>" +
				"<ColumnDivider>LightGray,Single</ColumnDivider><Height>16</Height><Locked>True</" +
				"Locked><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style2\" me=\"Style20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent" +
				"=\"Style3\" me=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeade" +
				"rStyle parent=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e24\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Wi" +
				"dth>168</Width><Height>16</Height><Locked>True</Locked><DCIdx>1</DCIdx></C1Displ" +
				"ayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style78\" /><Style pa" +
				"rent=\"Style1\" me=\"Style79\" /><FooterStyle parent=\"Style3\" me=\"Style80\" /><Editor" +
				"Style parent=\"Style5\" me=\"Style81\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style" +
				"83\" /><GroupFooterStyle parent=\"Style1\" me=\"Style82\" /><Visible>True</Visible><C" +
				"olumnDivider>LightGray,Single</ColumnDivider><Width>21</Width><Height>16</Height" +
				"><DCIdx>10</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
				"2\" me=\"Style30\" /><Style parent=\"Style1\" me=\"Style31\" /><FooterStyle parent=\"Sty" +
				"le3\" me=\"Style32\" /><EditorStyle parent=\"Style5\" me=\"Style33\" /><GroupHeaderStyl" +
				"e parent=\"Style1\" me=\"Style35\" /><GroupFooterStyle parent=\"Style1\" me=\"Style34\" " +
				"/><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>1" +
				"90</Width><Height>16</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn>" +
				"<HeadingStyle parent=\"Style2\" me=\"Style36\" /><Style parent=\"Style1\" me=\"Style37\"" +
				" /><FooterStyle parent=\"Style3\" me=\"Style38\" /><EditorStyle parent=\"Style5\" me=\"" +
				"Style39\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style41\" /><GroupFooterStyle pa" +
				"rent=\"Style1\" me=\"Style40\" /><ColumnDivider>Gainsboro,Single</ColumnDivider><Wid" +
				"th>190</Width><Height>16</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayCol" +
				"umn><HeadingStyle parent=\"Style2\" me=\"Style84\" /><Style parent=\"Style1\" me=\"Styl" +
				"e85\" /><FooterStyle parent=\"Style3\" me=\"Style86\" /><EditorStyle parent=\"Style5\" " +
				"me=\"Style87\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style89\" /><GroupFooterStyl" +
				"e parent=\"Style1\" me=\"Style88\" /><ColumnDivider>Gainsboro,Single</ColumnDivider>" +
				"<Width>21</Width><Height>16</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1Displa" +
				"yColumn><HeadingStyle parent=\"Style2\" me=\"Style60\" /><Style parent=\"Style1\" me=\"" +
				"Style61\" /><FooterStyle parent=\"Style3\" me=\"Style62\" /><EditorStyle parent=\"Styl" +
				"e5\" me=\"Style63\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style65\" /><GroupFooter" +
				"Style parent=\"Style1\" me=\"Style64\" /><ColumnDivider>Gainsboro,Single</ColumnDivi" +
				"der><Width>171</Width><Height>16</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1Di" +
				"splayColumn><HeadingStyle parent=\"Style2\" me=\"Style72\" /><Style parent=\"Style1\" " +
				"me=\"Style73\" /><FooterStyle parent=\"Style3\" me=\"Style74\" /><EditorStyle parent=\"" +
				"Style5\" me=\"Style75\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style77\" /><GroupFo" +
				"oterStyle parent=\"Style1\" me=\"Style76\" /><ColumnDivider>Gainsboro,Single</Column" +
				"Divider><Width>171</Width><Height>16</Height><DCIdx>9</DCIdx></C1DisplayColumn><" +
				"C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style132\" /><Style parent=\"Sty" +
				"le1\" me=\"Style133\" /><FooterStyle parent=\"Style3\" me=\"Style134\" /><EditorStyle p" +
				"arent=\"Style5\" me=\"Style135\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style137\" /" +
				"><GroupFooterStyle parent=\"Style1\" me=\"Style136\" /><Visible>True</Visible><Colum" +
				"nDivider>Gainsboro,Single</ColumnDivider><Width>25</Width><Height>16</Height><DC" +
				"Idx>19</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
				"e=\"Style48\" /><Style parent=\"Style1\" me=\"Style49\" /><FooterStyle parent=\"Style3\"" +
				" me=\"Style50\" /><EditorStyle parent=\"Style5\" me=\"Style51\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style53\" /><GroupFooterStyle parent=\"Style1\" me=\"Style52\" /><V" +
				"isible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Height>16</" +
				"Height><Locked>True</Locked><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style2\" me=\"Style54\" /><Style parent=\"Style1\" me=\"Style55\" " +
				"/><FooterStyle parent=\"Style3\" me=\"Style56\" /><EditorStyle parent=\"Style5\" me=\"S" +
				"tyle57\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style59\" /><GroupFooterStyle par" +
				"ent=\"Style1\" me=\"Style58\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Sing" +
				"le</ColumnDivider><Height>16</Height><Locked>True</Locked><DCIdx>3</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style42\" /><Style" +
				" parent=\"Style1\" me=\"Style43\" /><FooterStyle parent=\"Style3\" me=\"Style44\" /><Edi" +
				"torStyle parent=\"Style5\" me=\"Style45\" /><GroupHeaderStyle parent=\"Style1\" me=\"St" +
				"yle47\" /><GroupFooterStyle parent=\"Style1\" me=\"Style46\" /><ColumnDivider>DarkGra" +
				"y,Single</ColumnDivider><Height>16</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style66\" /><Style parent=\"Style1" +
				"\" me=\"Style67\" /><FooterStyle parent=\"Style3\" me=\"Style68\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style69\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style71\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style70\" /><ColumnDivider>DarkGray,Single</Colum" +
				"nDivider><Height>16</Height><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style2\" me=\"Style90\" /><Style parent=\"Style1\" me=\"Style91\" " +
				"/><FooterStyle parent=\"Style3\" me=\"Style92\" /><EditorStyle parent=\"Style5\" me=\"S" +
				"tyle93\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style95\" /><GroupFooterStyle par" +
				"ent=\"Style1\" me=\"Style94\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Heigh" +
				"t>16</Height><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
				"arent=\"Style2\" me=\"Style96\" /><Style parent=\"Style1\" me=\"Style97\" /><FooterStyle" +
				" parent=\"Style3\" me=\"Style98\" /><EditorStyle parent=\"Style5\" me=\"Style99\" /><Gro" +
				"upHeaderStyle parent=\"Style1\" me=\"Style101\" /><GroupFooterStyle parent=\"Style1\" " +
				"me=\"Style100\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>16</Height" +
				"><DCIdx>13</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
				"2\" me=\"Style102\" /><Style parent=\"Style1\" me=\"Style103\" /><FooterStyle parent=\"S" +
				"tyle3\" me=\"Style104\" /><EditorStyle parent=\"Style5\" me=\"Style105\" /><GroupHeader" +
				"Style parent=\"Style1\" me=\"Style107\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
				"e106\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>" +
				"14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"S" +
				"tyle108\" /><Style parent=\"Style1\" me=\"Style109\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style110\" /><EditorStyle parent=\"Style5\" me=\"Style111\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style113\" /><GroupFooterStyle parent=\"Style1\" me=\"Style112\" />" +
				"<ColumnDivider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>15</DCId" +
				"x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style114\"" +
				" /><Style parent=\"Style1\" me=\"Style115\" /><FooterStyle parent=\"Style3\" me=\"Style" +
				"116\" /><EditorStyle parent=\"Style5\" me=\"Style117\" /><GroupHeaderStyle parent=\"St" +
				"yle1\" me=\"Style119\" /><GroupFooterStyle parent=\"Style1\" me=\"Style118\" /><ColumnD" +
				"ivider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>16</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style120\" /><Styl" +
				"e parent=\"Style1\" me=\"Style121\" /><FooterStyle parent=\"Style3\" me=\"Style122\" /><" +
				"EditorStyle parent=\"Style5\" me=\"Style123\" /><GroupHeaderStyle parent=\"Style1\" me" +
				"=\"Style125\" /><GroupFooterStyle parent=\"Style1\" me=\"Style124\" /><ColumnDivider>D" +
				"arkGray,Single</ColumnDivider><Height>16</Height><DCIdx>17</DCIdx></C1DisplayCol" +
				"umn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style126\" /><Style parent" +
				"=\"Style1\" me=\"Style127\" /><FooterStyle parent=\"Style3\" me=\"Style128\" /><EditorSt" +
				"yle parent=\"Style5\" me=\"Style129\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style1" +
				"31\" /><GroupFooterStyle parent=\"Style1\" me=\"Style130\" /><ColumnDivider>DarkGray," +
				"Single</ColumnDivider><Height>16</Height><DCIdx>18</DCIdx></C1DisplayColumn></in" +
				"ternalCols><ClientRect>0, 0, 770, 428</ClientRect><BorderSide>0</BorderSide></C1" +
				".Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /" +
				"><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><St" +
				"yle parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Sty" +
				"le parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style p" +
				"arent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style " +
				"parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Sty" +
				"le parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></Named" +
				"Styles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Lay" +
				"out><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 770, 428</Clien" +
				"tArea><PrintPageHeaderStyle parent=\"\" me=\"Style28\" /><PrintPageFooterStyle paren" +
				"t=\"\" me=\"Style29\" /></Blob>";
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
			this.ManageOfficeCodesMenuItem.Enabled = false;
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
			// NegativeRebateMarkUpDefaultNumericEdit
			// 
			this.NegativeRebateMarkUpDefaultNumericEdit.Location = new System.Drawing.Point(200, 9);
			this.NegativeRebateMarkUpDefaultNumericEdit.Name = "NegativeRebateMarkUpDefaultNumericEdit";
			this.NegativeRebateMarkUpDefaultNumericEdit.Size = new System.Drawing.Size(152, 21);
			this.NegativeRebateMarkUpDefaultNumericEdit.TabIndex = 4;
			this.NegativeRebateMarkUpDefaultNumericEdit.Tag = null;
			this.NegativeRebateMarkUpDefaultNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.NegativeRebateMarkUpDefaultNumericEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NegativeRebateMarkUpDefaultNumericEdit_KeyPress);
			// 
			// NegativeRebateMarkUpDefaultLabel
			// 
			this.NegativeRebateMarkUpDefaultLabel.Location = new System.Drawing.Point(4, 8);
			this.NegativeRebateMarkUpDefaultLabel.Name = "NegativeRebateMarkUpDefaultLabel";
			this.NegativeRebateMarkUpDefaultLabel.Size = new System.Drawing.Size(196, 23);
			this.NegativeRebateMarkUpDefaultLabel.TabIndex = 5;
			this.NegativeRebateMarkUpDefaultLabel.Tag = null;
			this.NegativeRebateMarkUpDefaultLabel.Text = "Default Negative Rebate MarkUp:";
			this.NegativeRebateMarkUpDefaultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.NegativeRebateMarkUpDefaultLabel.TextDetached = true;
			// 
			// ShortSaleTradingGroupsNegativeRebatesBillingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(776, 473);
			this.Controls.Add(this.NegativeRebateMarkUpDefaultNumericEdit);
			this.Controls.Add(this.NegativeRebateMarkUpDefaultLabel);
			this.Controls.Add(this.ShortSaleTradingGroupsGrid);
			this.DockPadding.Bottom = 1;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 40;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ShortSaleTradingGroupsNegativeRebatesBillingForm";
			this.Text = "ShortSale - Trading Groups  - Negative Rebates Billing";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ShortSaleTradingGroupsNegativeRebateBillingForm_Closing);
			this.Load += new System.EventHandler(this.ShortSaleTradingGroupsNegativeRebateBillingForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.ShortSaleTradingGroupsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NegativeRebateMarkUpDefaultNumericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NegativeRebateMarkUpDefaultLabel)).EndInit();
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
			
				NegativeRebateMarkUpDefaultNumericEdit.Value = mainForm.ServiceAgent.KeyValueGet("ShortSaleNegativeDefaultMarkUp", "0.25");			
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [ShortSaleTradingGroupsNegativeRebateBillingForm.ListLoad]", Log.Error, 1); 
			}

			this.Cursor = Cursors.Default;
		}
		
		
		private void ShortSaleTradingGroupsNegativeRebateBillingForm_Load(object sender, System.EventArgs e)
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

		private void ShortSaleTradingGroupsNegativeRebateBillingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
				RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
				RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
				RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
			}

			try {shortSaleTradingGroupsAccountManagementForm.Close();} catch {}		
			try {shortSaleTradingGroupsOfficeCodeManagementForm.Close();} catch {}			
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
				shortSaleTradingGroupsAccountManagementForm = new ShortSaleTradingGroupsNegativeRebatesAccountManagementForm(mainForm, ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text);
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
				mainForm.RebateAgent.ShortSaleBillingSummaryTradingGroupsSet(
					ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text,
					ShortSaleTradingGroupsGrid.Columns["NegativeRebateMarkUp"].Text,
					ShortSaleTradingGroupsGrid.Columns["NegativeRebateMarkUpCode"].Text,
				  ShortSaleTradingGroupsGrid.Columns["NegativeRebateBill"].Text,
					mainForm.UserId);

				ShortSaleTradingGroupsGrid.Columns["ActUserId"].Text = mainForm.UserId;
				ShortSaleTradingGroupsGrid.Columns["ActTime"].Value = DateTime.Now; 
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);  
			}
		}

		private void NegativeRebateMarkUpDefaultNumericEdit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{					
				mainForm.ServiceAgent.KeyValueSet("ShortSaleNegativeDefaultMarkUp", NegativeRebateMarkUpDefaultNumericEdit.Value.ToString());								
				mainForm.Alert("Set ShortSale Negative Default MarkUp: " + NegativeRebateMarkUpDefaultNumericEdit.Value.ToString(), PilotState.Normal);
			}
		}

		private void ManageOfficeCodesMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				shortSaleTradingGroupsOfficeCodeManagementForm = new ShortSaleTradingGroupsNegativeRebatesOfficeCodeManagementForm(mainForm, ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text);
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
	}
}
