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
	public class ShortSaleTradingGroupsBillingForm : System.Windows.Forms.Form
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
		private C1.Win.C1Input.C1Label PositiveRebateMarkUpDefaultLabel;
		private C1.Win.C1Input.C1Label FedFundsMarkUpDefaultLabel;
		private C1.Win.C1Input.C1NumericEdit NegativeRebateMarkUpDefaultNumericEdit;
		private C1.Win.C1Input.C1NumericEdit PositiveRebateMarkUpDefaultNumericEdit;
		private C1.Win.C1Input.C1NumericEdit FedFundsMarkUpDefaultNumericEdit;
		private C1.Win.C1Input.C1NumericEdit LiborFundsMarkUpDefaultNumericEdit;
		private C1.Win.C1Input.C1Label LiborMarkUpDefaultLabel;
		private C1.Win.C1Input.C1Label DefaultHouseRateLabel;
		private C1.Win.C1Input.C1NumericEdit HouseRateDefaultNumbericEdit;
		private System.Windows.Forms.MenuItem ManageMenuItem;
		private System.Windows.Forms.MenuItem ManageAccountMenuItem;
		private System.Windows.Forms.MenuItem ManageOfficeCodesMenuItem;
		private ShortSaleTradingGroupsAccountManagementForm shortSaleTradingGroupsAccountManagementForm = null;
		private ShortSaleTradingGroupsOfficeCodeManagementForm shortSaleTradingGroupsOfficeCodeManagementForm = null;

		public ShortSaleTradingGroupsBillingForm(MainForm mainForm)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleTradingGroupsBillingForm));
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
			this.PositiveRebateMarkUpDefaultNumericEdit = new C1.Win.C1Input.C1NumericEdit();
			this.PositiveRebateMarkUpDefaultLabel = new C1.Win.C1Input.C1Label();
			this.FedFundsMarkUpDefaultNumericEdit = new C1.Win.C1Input.C1NumericEdit();
			this.FedFundsMarkUpDefaultLabel = new C1.Win.C1Input.C1Label();
			this.LiborFundsMarkUpDefaultNumericEdit = new C1.Win.C1Input.C1NumericEdit();
			this.LiborMarkUpDefaultLabel = new C1.Win.C1Input.C1Label();
			this.HouseRateDefaultNumbericEdit = new C1.Win.C1Input.C1NumericEdit();
			this.DefaultHouseRateLabel = new C1.Win.C1Input.C1Label();
			((System.ComponentModel.ISupportInitialize)(this.ShortSaleTradingGroupsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NegativeRebateMarkUpDefaultNumericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NegativeRebateMarkUpDefaultLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PositiveRebateMarkUpDefaultNumericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PositiveRebateMarkUpDefaultLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.FedFundsMarkUpDefaultNumericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.FedFundsMarkUpDefaultLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LiborFundsMarkUpDefaultNumericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LiborMarkUpDefaultLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HouseRateDefaultNumbericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultHouseRateLabel)).BeginInit();
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
			this.ShortSaleTradingGroupsGrid.Location = new System.Drawing.Point(1, 60);
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
			this.ShortSaleTradingGroupsGrid.Size = new System.Drawing.Size(1306, 412);
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
				"/DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Highlig" +
				"htRow{ForeColor:HighlightText;BackColor:Highlight;}Inactive{ForeColor:InactiveCa" +
				"ptionText;BackColor:InactiveCaption;}Style119{}Style118{}Style78{AlignHorz:Cente" +
				"r;}Style79{AlignHorz:Center;}Style85{AlignHorz:Center;}Editor{}Style117{}Style11" +
				"6{}Style72{AlignHorz:Near;}Style73{AlignHorz:Far;}Style70{}Style71{}Style76{}Sty" +
				"le77{}Style74{}Style75{}Style84{AlignHorz:Center;}Style87{}Style86{}Style81{}Sty" +
				"le80{}Style83{}Style82{}Style89{}Style88{}Style108{AlignHorz:Near;}Style109{Alig" +
				"nHorz:Near;}Style104{}Style105{}Style106{}Style107{}Style100{}Style101{}Style102" +
				"{AlignHorz:Near;}Style103{AlignHorz:Near;}Style94{}Style95{}Style96{AlignHorz:Ne" +
				"ar;}Style97{AlignHorz:Near;}Style90{AlignHorz:Near;}Style91{AlignHorz:Near;}Styl" +
				"e92{}Style93{}Style131{}Style98{}Style99{}Heading{Wrap:True;BackColor:Control;Bo" +
				"rder:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style18{}Style19" +
				"{}Style17{}Style14{AlignHorz:Near;}Style15{Font:Verdana, 8.25pt, style=Bold;Alig" +
				"nHorz:Near;ForeColor:Highlight;}Style16{}Style130{}Style10{AlignHorz:Near;}Style" +
				"11{}Style12{}Style13{}Style127{AlignHorz:Near;}Style124{}Style122{}Style24{}Styl" +
				"e25{}Style26{}Style128{}Style129{}Style126{AlignHorz:Near;}Style29{}Style28{}Sty" +
				"le125{}Style27{}Style123{}Style120{AlignHorz:Near;}Style121{AlignHorz:Near;}Styl" +
				"e23{}Style22{}Style21{AlignHorz:Near;}Style20{AlignHorz:Near;}OddRow{}Style49{Al" +
				"ignHorz:Near;}Style38{}Style39{}Style36{AlignHorz:Near;}FilterBar{BackColor:SeaS" +
				"hell;}Style37{AlignHorz:Far;}Style34{}Style35{}Style32{}Style33{}Style30{AlignHo" +
				"rz:Near;}Style48{AlignHorz:Near;}Style31{AlignHorz:Far;}Normal{Font:Verdana, 8.2" +
				"5pt;}Style41{}Style40{}Style43{AlignHorz:Near;}Style42{AlignHorz:Near;}Style45{}" +
				"Style44{}Style47{}Style46{}Selected{ForeColor:HighlightText;BackColor:Highlight;" +
				"}EvenRow{BackColor:Aqua;}Style51{}Style9{}Style8{}Style58{}Style59{}Style5{}Styl" +
				"e4{}Style7{}Style6{}Style1{}RecordSelector{AlignImage:Center;}Style3{}Style2{}St" +
				"yle50{}Footer{}Style52{}Style53{}Style54{AlignHorz:Near;}Style55{AlignHorz:Near;" +
				"}Style56{}Style57{}Caption{AlignHorz:Center;}Style112{}Style69{}Style68{}Group{A" +
				"lignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style64{}Style63{" +
				"}Style62{}Style61{AlignHorz:Far;}Style60{AlignHorz:Near;}Style67{AlignHorz:Near;" +
				"}Style66{AlignHorz:Near;}Style65{}Style115{AlignHorz:Near;}Style114{AlignHorz:Ne" +
				"ar;}Style111{}Style110{}Style113{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.M" +
				"ergeView HBarStyle=\"None\" VBarStyle=\"Always\" Name=\"\" CaptionHeight=\"17\" ColumnCa" +
				"ptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FetchRowStyles" +
				"=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\"" +
				" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionS" +
				"tyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><" +
				"EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" m" +
				"e=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Gro" +
				"up\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowSty" +
				"le parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Styl" +
				"e4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"Re" +
				"cordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Sty" +
				"le parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle pa" +
				"rent=\"Style2\" me=\"Style14\" /><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle " +
				"parent=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><Grou" +
				"pHeaderStyle parent=\"Style1\" me=\"Style19\" /><GroupFooterStyle parent=\"Style1\" me" +
				"=\"Style18\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivid" +
				"er><Height>16</Height><Locked>True</Locked><DCIdx>0</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style20\" /><Style parent=\"Style1" +
				"\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Style22\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style25\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style24\" /><Visible>True</Visible><ColumnDivider" +
				">LightGray,Single</ColumnDivider><Width>168</Width><Height>16</Height><Locked>Tr" +
				"ue</Locked><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pare" +
				"nt=\"Style2\" me=\"Style30\" /><Style parent=\"Style1\" me=\"Style31\" /><FooterStyle pa" +
				"rent=\"Style3\" me=\"Style32\" /><EditorStyle parent=\"Style5\" me=\"Style33\" /><GroupH" +
				"eaderStyle parent=\"Style1\" me=\"Style35\" /><GroupFooterStyle parent=\"Style1\" me=\"" +
				"Style34\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider" +
				"><Width>190</Width><Height>16</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style2\" me=\"Style78\" /><Style parent=\"Style1\" me=" +
				"\"Style79\" /><FooterStyle parent=\"Style3\" me=\"Style80\" /><EditorStyle parent=\"Sty" +
				"le5\" me=\"Style81\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style83\" /><GroupFoote" +
				"rStyle parent=\"Style1\" me=\"Style82\" /><Visible>True</Visible><ColumnDivider>Ligh" +
				"tGray,Single</ColumnDivider><Width>21</Width><Height>16</Height><DCIdx>10</DCIdx" +
				"></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style36\" /" +
				"><Style parent=\"Style1\" me=\"Style37\" /><FooterStyle parent=\"Style3\" me=\"Style38\"" +
				" /><EditorStyle parent=\"Style5\" me=\"Style39\" /><GroupHeaderStyle parent=\"Style1\"" +
				" me=\"Style41\" /><GroupFooterStyle parent=\"Style1\" me=\"Style40\" /><Visible>True</" +
				"Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>190</Width><Height" +
				">16</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style84\" /><Style parent=\"Style1\" me=\"Style85\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style86\" /><EditorStyle parent=\"Style5\" me=\"Style87\" /><Group" +
				"HeaderStyle parent=\"Style1\" me=\"Style89\" /><GroupFooterStyle parent=\"Style1\" me=" +
				"\"Style88\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivide" +
				"r><Width>21</Width><Height>16</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1Disp" +
				"layColumn><HeadingStyle parent=\"Style2\" me=\"Style60\" /><Style parent=\"Style1\" me" +
				"=\"Style61\" /><FooterStyle parent=\"Style3\" me=\"Style62\" /><EditorStyle parent=\"St" +
				"yle5\" me=\"Style63\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style65\" /><GroupFoot" +
				"erStyle parent=\"Style1\" me=\"Style64\" /><Visible>True</Visible><ColumnDivider>Lig" +
				"htGray,Single</ColumnDivider><Width>171</Width><Height>16</Height><DCIdx>7</DCId" +
				"x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style72\" " +
				"/><Style parent=\"Style1\" me=\"Style73\" /><FooterStyle parent=\"Style3\" me=\"Style74" +
				"\" /><EditorStyle parent=\"Style5\" me=\"Style75\" /><GroupHeaderStyle parent=\"Style1" +
				"\" me=\"Style77\" /><GroupFooterStyle parent=\"Style1\" me=\"Style76\" /><Visible>True<" +
				"/Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>171</Width><Heigh" +
				"t>16</Height><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pa" +
				"rent=\"Style2\" me=\"Style48\" /><Style parent=\"Style1\" me=\"Style49\" /><FooterStyle " +
				"parent=\"Style3\" me=\"Style50\" /><EditorStyle parent=\"Style5\" me=\"Style51\" /><Grou" +
				"pHeaderStyle parent=\"Style1\" me=\"Style53\" /><GroupFooterStyle parent=\"Style1\" me" +
				"=\"Style52\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivid" +
				"er><Height>16</Height><Locked>True</Locked><DCIdx>2</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style54\" /><Style parent=\"Style1" +
				"\" me=\"Style55\" /><FooterStyle parent=\"Style3\" me=\"Style56\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style57\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style59\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style58\" /><Visible>True</Visible><ColumnDivider" +
				">LightGray,Single</ColumnDivider><Height>16</Height><Locked>True</Locked><DCIdx>" +
				"3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle42\" /><Style parent=\"Style1\" me=\"Style43\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style44\" /><EditorStyle parent=\"Style5\" me=\"Style45\" /><GroupHeaderStyle parent=" +
				"\"Style1\" me=\"Style47\" /><GroupFooterStyle parent=\"Style1\" me=\"Style46\" /><Column" +
				"Divider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>4</DCIdx></C1Di" +
				"splayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style66\" /><Style" +
				" parent=\"Style1\" me=\"Style67\" /><FooterStyle parent=\"Style3\" me=\"Style68\" /><Edi" +
				"torStyle parent=\"Style5\" me=\"Style69\" /><GroupHeaderStyle parent=\"Style1\" me=\"St" +
				"yle71\" /><GroupFooterStyle parent=\"Style1\" me=\"Style70\" /><ColumnDivider>DarkGra" +
				"y,Single</ColumnDivider><Height>16</Height><DCIdx>8</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style90\" /><Style parent=\"Style1" +
				"\" me=\"Style91\" /><FooterStyle parent=\"Style3\" me=\"Style92\" /><EditorStyle parent" +
				"=\"Style5\" me=\"Style93\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style95\" /><Group" +
				"FooterStyle parent=\"Style1\" me=\"Style94\" /><ColumnDivider>DarkGray,Single</Colum" +
				"nDivider><Height>16</Height><DCIdx>12</DCIdx></C1DisplayColumn><C1DisplayColumn>" +
				"<HeadingStyle parent=\"Style2\" me=\"Style96\" /><Style parent=\"Style1\" me=\"Style97\"" +
				" /><FooterStyle parent=\"Style3\" me=\"Style98\" /><EditorStyle parent=\"Style5\" me=\"" +
				"Style99\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style101\" /><GroupFooterStyle p" +
				"arent=\"Style1\" me=\"Style100\" /><ColumnDivider>DarkGray,Single</ColumnDivider><He" +
				"ight>16</Height><DCIdx>13</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyl" +
				"e parent=\"Style2\" me=\"Style102\" /><Style parent=\"Style1\" me=\"Style103\" /><Footer" +
				"Style parent=\"Style3\" me=\"Style104\" /><EditorStyle parent=\"Style5\" me=\"Style105\"" +
				" /><GroupHeaderStyle parent=\"Style1\" me=\"Style107\" /><GroupFooterStyle parent=\"S" +
				"tyle1\" me=\"Style106\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>16<" +
				"/Height><DCIdx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent" +
				"=\"Style2\" me=\"Style108\" /><Style parent=\"Style1\" me=\"Style109\" /><FooterStyle pa" +
				"rent=\"Style3\" me=\"Style110\" /><EditorStyle parent=\"Style5\" me=\"Style111\" /><Grou" +
				"pHeaderStyle parent=\"Style1\" me=\"Style113\" /><GroupFooterStyle parent=\"Style1\" m" +
				"e=\"Style112\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>16</Height>" +
				"<DCIdx>15</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"\" me=\"Style114\" /><Style parent=\"Style1\" me=\"Style115\" /><FooterStyle parent=\"St" +
				"yle3\" me=\"Style116\" /><EditorStyle parent=\"Style5\" me=\"Style117\" /><GroupHeaderS" +
				"tyle parent=\"Style1\" me=\"Style119\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
				"118\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>1" +
				"6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle120\" /><Style parent=\"Style1\" me=\"Style121\" /><FooterStyle parent=\"Style3\" me" +
				"=\"Style122\" /><EditorStyle parent=\"Style5\" me=\"Style123\" /><GroupHeaderStyle par" +
				"ent=\"Style1\" me=\"Style125\" /><GroupFooterStyle parent=\"Style1\" me=\"Style124\" /><" +
				"ColumnDivider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>17</DCIdx" +
				"></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style126\" " +
				"/><Style parent=\"Style1\" me=\"Style127\" /><FooterStyle parent=\"Style3\" me=\"Style1" +
				"28\" /><EditorStyle parent=\"Style5\" me=\"Style129\" /><GroupHeaderStyle parent=\"Sty" +
				"le1\" me=\"Style131\" /><GroupFooterStyle parent=\"Style1\" me=\"Style130\" /><ColumnDi" +
				"vider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>18</DCIdx></C1Dis" +
				"playColumn></internalCols><ClientRect>0, 0, 1302, 408</ClientRect><BorderSide>0<" +
				"/BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=" +
				"\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" m" +
				"e=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"" +
				"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Ed" +
				"itor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"Ev" +
				"enRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Record" +
				"Selector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"" +
				"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layo" +
				"ut>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0," +
				" 1302, 408</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style28\" /><PrintPage" +
				"FooterStyle parent=\"\" me=\"Style29\" /></Blob>";
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
			// PositiveRebateMarkUpDefaultNumericEdit
			// 
			this.PositiveRebateMarkUpDefaultNumericEdit.Location = new System.Drawing.Point(200, 33);
			this.PositiveRebateMarkUpDefaultNumericEdit.Name = "PositiveRebateMarkUpDefaultNumericEdit";
			this.PositiveRebateMarkUpDefaultNumericEdit.Size = new System.Drawing.Size(152, 21);
			this.PositiveRebateMarkUpDefaultNumericEdit.TabIndex = 2;
			this.PositiveRebateMarkUpDefaultNumericEdit.Tag = null;
			this.PositiveRebateMarkUpDefaultNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.PositiveRebateMarkUpDefaultNumericEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PositiveRebateMarkUpDefaultNumericEdit_KeyPress);
			// 
			// PositiveRebateMarkUpDefaultLabel
			// 
			this.PositiveRebateMarkUpDefaultLabel.Location = new System.Drawing.Point(8, 32);
			this.PositiveRebateMarkUpDefaultLabel.Name = "PositiveRebateMarkUpDefaultLabel";
			this.PositiveRebateMarkUpDefaultLabel.Size = new System.Drawing.Size(192, 23);
			this.PositiveRebateMarkUpDefaultLabel.TabIndex = 3;
			this.PositiveRebateMarkUpDefaultLabel.Tag = null;
			this.PositiveRebateMarkUpDefaultLabel.Text = "Default Positive Rebate MarkUp:";
			this.PositiveRebateMarkUpDefaultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.PositiveRebateMarkUpDefaultLabel.TextDetached = true;
			// 
			// FedFundsMarkUpDefaultNumericEdit
			// 
			this.FedFundsMarkUpDefaultNumericEdit.Location = new System.Drawing.Point(552, 9);
			this.FedFundsMarkUpDefaultNumericEdit.Name = "FedFundsMarkUpDefaultNumericEdit";
			this.FedFundsMarkUpDefaultNumericEdit.Size = new System.Drawing.Size(108, 21);
			this.FedFundsMarkUpDefaultNumericEdit.TabIndex = 0;
			this.FedFundsMarkUpDefaultNumericEdit.Tag = null;
			this.FedFundsMarkUpDefaultNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.FedFundsMarkUpDefaultNumericEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FedFundsMarkUpDefaultNumericEdit_KeyPress);
			// 
			// FedFundsMarkUpDefaultLabel
			// 
			this.FedFundsMarkUpDefaultLabel.Location = new System.Drawing.Point(392, 8);
			this.FedFundsMarkUpDefaultLabel.Name = "FedFundsMarkUpDefaultLabel";
			this.FedFundsMarkUpDefaultLabel.Size = new System.Drawing.Size(164, 23);
			this.FedFundsMarkUpDefaultLabel.TabIndex = 1;
			this.FedFundsMarkUpDefaultLabel.Tag = null;
			this.FedFundsMarkUpDefaultLabel.Text = "Default Fed Funds MarkUp:";
			this.FedFundsMarkUpDefaultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.FedFundsMarkUpDefaultLabel.TextDetached = true;
			// 
			// LiborFundsMarkUpDefaultNumericEdit
			// 
			this.LiborFundsMarkUpDefaultNumericEdit.Location = new System.Drawing.Point(552, 33);
			this.LiborFundsMarkUpDefaultNumericEdit.Name = "LiborFundsMarkUpDefaultNumericEdit";
			this.LiborFundsMarkUpDefaultNumericEdit.Size = new System.Drawing.Size(108, 21);
			this.LiborFundsMarkUpDefaultNumericEdit.TabIndex = 6;
			this.LiborFundsMarkUpDefaultNumericEdit.Tag = null;
			this.LiborFundsMarkUpDefaultNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.LiborFundsMarkUpDefaultNumericEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LiborFundsMarkUpDefaultNumericEdit_KeyPress);
			// 
			// LiborMarkUpDefaultLabel
			// 
			this.LiborMarkUpDefaultLabel.Location = new System.Drawing.Point(384, 32);
			this.LiborMarkUpDefaultLabel.Name = "LiborMarkUpDefaultLabel";
			this.LiborMarkUpDefaultLabel.Size = new System.Drawing.Size(172, 23);
			this.LiborMarkUpDefaultLabel.TabIndex = 7;
			this.LiborMarkUpDefaultLabel.Tag = null;
			this.LiborMarkUpDefaultLabel.Text = "Default Libor Funds MarkUp:";
			this.LiborMarkUpDefaultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LiborMarkUpDefaultLabel.TextDetached = true;
			// 
			// HouseRateDefaultNumbericEdit
			// 
			this.HouseRateDefaultNumbericEdit.Location = new System.Drawing.Point(836, 9);
			this.HouseRateDefaultNumbericEdit.Name = "HouseRateDefaultNumbericEdit";
			this.HouseRateDefaultNumbericEdit.Size = new System.Drawing.Size(108, 21);
			this.HouseRateDefaultNumbericEdit.TabIndex = 8;
			this.HouseRateDefaultNumbericEdit.Tag = null;
			this.HouseRateDefaultNumbericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
			this.HouseRateDefaultNumbericEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HouseRateDefaultNumbericEdit_KeyPress);
			// 
			// DefaultHouseRateLabel
			// 
			this.DefaultHouseRateLabel.Location = new System.Drawing.Point(672, 8);
			this.DefaultHouseRateLabel.Name = "DefaultHouseRateLabel";
			this.DefaultHouseRateLabel.Size = new System.Drawing.Size(164, 23);
			this.DefaultHouseRateLabel.TabIndex = 9;
			this.DefaultHouseRateLabel.Tag = null;
			this.DefaultHouseRateLabel.Text = "Default House Rate:";
			this.DefaultHouseRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DefaultHouseRateLabel.TextDetached = true;
			// 
			// ShortSaleTradingGroupsBillingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1308, 473);
			this.Controls.Add(this.HouseRateDefaultNumbericEdit);
			this.Controls.Add(this.DefaultHouseRateLabel);
			this.Controls.Add(this.LiborFundsMarkUpDefaultNumericEdit);
			this.Controls.Add(this.LiborMarkUpDefaultLabel);
			this.Controls.Add(this.FedFundsMarkUpDefaultNumericEdit);
			this.Controls.Add(this.FedFundsMarkUpDefaultLabel);
			this.Controls.Add(this.PositiveRebateMarkUpDefaultNumericEdit);
			this.Controls.Add(this.PositiveRebateMarkUpDefaultLabel);
			this.Controls.Add(this.NegativeRebateMarkUpDefaultNumericEdit);
			this.Controls.Add(this.NegativeRebateMarkUpDefaultLabel);
			this.Controls.Add(this.ShortSaleTradingGroupsGrid);
			this.DockPadding.Bottom = 1;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 60;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ShortSaleTradingGroupsBillingForm";
			this.Text = "ShortSale - Trading Groups Billing";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ShortSaleTradingGroupsForm_Closing);
			this.Load += new System.EventHandler(this.ShortSaleTradingGroupsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.ShortSaleTradingGroupsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NegativeRebateMarkUpDefaultNumericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NegativeRebateMarkUpDefaultLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PositiveRebateMarkUpDefaultNumericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PositiveRebateMarkUpDefaultLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.FedFundsMarkUpDefaultNumericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.FedFundsMarkUpDefaultLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LiborFundsMarkUpDefaultNumericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LiborMarkUpDefaultLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HouseRateDefaultNumbericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultHouseRateLabel)).EndInit();
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
				PositiveRebateMarkUpDefaultNumericEdit.Value = mainForm.ServiceAgent.KeyValueGet("ShortSalePositiveDefaultMarkUp", "0");
				FedFundsMarkUpDefaultNumericEdit.Value =  mainForm.ServiceAgent.KeyValueGet("ShortSaleFedFundsDefaultMarkUp", "0");			
				LiborFundsMarkUpDefaultNumericEdit.Value =  mainForm.ServiceAgent.KeyValueGet("ShortSaleLiborFundsDefaultMarkUp", "0");			
				HouseRateDefaultNumbericEdit.Value =   mainForm.ServiceAgent.KeyValueGet("ShortSaleHouseRateDefault", "0");			
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [ShortSaleTradingGroupsForm.ListLoad]", Log.Error, 1); 
			}

			this.Cursor = Cursors.Default;
		}
		
		
		private void ShortSaleTradingGroupsForm_Load(object sender, System.EventArgs e)
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

		private void ShortSaleTradingGroupsForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
				RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
				RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
				RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
			}

			if (shortSaleTradingGroupsAccountManagementForm != null)
			{
				shortSaleTradingGroupsAccountManagementForm.Close();
			}

			if (shortSaleTradingGroupsOfficeCodeManagementForm != null)
			{
				shortSaleTradingGroupsOfficeCodeManagementForm.Close();
			}
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
				shortSaleTradingGroupsAccountManagementForm = new ShortSaleTradingGroupsAccountManagementForm(mainForm, ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text);
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
				mainForm.ShortSaleAgent.TradingGroupSet(
					ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text,
					ShortSaleTradingGroupsGrid.Columns["GroupName"].Text,
					ShortSaleTradingGroupsGrid.Columns["MinPrice"].Value.ToString(),
					ShortSaleTradingGroupsGrid.Columns["AutoApprovalMax"].Value.ToString(),
					ShortSaleTradingGroupsGrid.Columns["PremiumMin"].Value.ToString(),
					ShortSaleTradingGroupsGrid.Columns["PremiumMax"].Value.ToString(),
					bool.Parse(ShortSaleTradingGroupsGrid.Columns["AutoEmail"].Value.ToString()),
					ShortSaleTradingGroupsGrid.Columns["EmailAddress"].Text,
					ShortSaleTradingGroupsGrid.Columns["LastEmailDate"].Text,
					ShortSaleTradingGroupsGrid.Columns["NegativeRebateMarkUp"].Value.ToString(),
					ShortSaleTradingGroupsGrid.Columns["PositiveRebateMarkUp"].Value.ToString(),
					ShortSaleTradingGroupsGrid.Columns["FedFundsMarkUp"].Value.ToString(),
					ShortSaleTradingGroupsGrid.Columns["LiborFundsMarkUp"].Value.ToString(),
					ShortSaleTradingGroupsGrid.Columns["PositiveRebateBill"].Value.ToString(),
					ShortSaleTradingGroupsGrid.Columns["NegativeRebateBill"].Value.ToString(),
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

		private void HouseRateDefaultNumbericEdit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{
				mainForm.ServiceAgent.KeyValueSet("ShortSaleHouseRateDefault", HouseRateDefaultNumbericEdit.Value.ToString());			
				mainForm.Alert("Set ShortSale House Rate Default: " + HouseRateDefaultNumbericEdit.Value.ToString(),  PilotState.Normal);
			}
		}

		private void ManageOfficeCodesMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				shortSaleTradingGroupsOfficeCodeManagementForm = new ShortSaleTradingGroupsOfficeCodeManagementForm(mainForm, ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text);
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
