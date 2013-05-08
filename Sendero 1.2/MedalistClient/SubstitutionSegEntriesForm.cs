using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class SubstitutionSegEntriesForm : System.Windows.Forms.Form
	{
		private DataSet dataSet;
		private DataView dataView;

		private MainForm mainForm;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep1MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;

		private C1.Win.C1Input.C1Label StatusLabel;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid SegEntriesGrid;
		private System.Windows.Forms.RadioButton SegEntriesRadioButton;
		private System.Windows.Forms.RadioButton MemoSegEntriesRadioButton;

		private System.ComponentModel.Container components = null;

		public SubstitutionSegEntriesForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
      
			InitializeComponent();
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
				System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SubstitutionSegEntriesForm));
				this.SegEntriesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
				this.StatusLabel = new C1.Win.C1Input.C1Label();
				this.MainContextMenu = new System.Windows.Forms.ContextMenu();
				this.SendToMenuItem = new System.Windows.Forms.MenuItem();
				this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
				this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
				this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
				this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
				this.ExitMenuItem = new System.Windows.Forms.MenuItem();
				this.SegEntriesRadioButton = new System.Windows.Forms.RadioButton();
				this.MemoSegEntriesRadioButton = new System.Windows.Forms.RadioButton();
				((System.ComponentModel.ISupportInitialize)(this.SegEntriesGrid)).BeginInit();
				((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
				this.SuspendLayout();
				// 
				// SegEntriesGrid
				// 
				this.SegEntriesGrid.AllowColSelect = false;
				this.SegEntriesGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
				this.SegEntriesGrid.AllowUpdateOnBlur = false;
				this.SegEntriesGrid.CaptionHeight = 17;
				this.SegEntriesGrid.ColumnFooters = true;
				this.SegEntriesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
				this.SegEntriesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
				this.SegEntriesGrid.EmptyRows = true;
				this.SegEntriesGrid.ExtendRightColumn = true;
				this.SegEntriesGrid.FilterBar = true;
				this.SegEntriesGrid.GroupByCaption = "Drag a column header here to group by that column";
				this.SegEntriesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
				this.SegEntriesGrid.Location = new System.Drawing.Point(1, 32);
				this.SegEntriesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
				this.SegEntriesGrid.Name = "SegEntriesGrid";
				this.SegEntriesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
				this.SegEntriesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
				this.SegEntriesGrid.PreviewInfo.ZoomFactor = 75;
				this.SegEntriesGrid.RecordSelectorWidth = 16;
				this.SegEntriesGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
				this.SegEntriesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
				this.SegEntriesGrid.RowHeight = 15;
				this.SegEntriesGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
				this.SegEntriesGrid.Size = new System.Drawing.Size(862, 381);
				this.SegEntriesGrid.TabIndex = 0;
				this.SegEntriesGrid.Text = "Seg Entries";
				this.SegEntriesGrid.AfterFilter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.SegEntriesGrid_AfterFilter);
				this.SegEntriesGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.SegEntriesGrid_RowColChange);
				this.SegEntriesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.SegEntriesGrid_BeforeUpdate);
				this.SegEntriesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.SegEntriesGrid_FormatText);
				this.SegEntriesGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
						"\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
						"l=\"0\" Caption=\"Quantity\" DataField=\"Quantity\" NumberFormat=\"FormatText Event\"><V" +
						"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Account" +
						" Number\" DataField=\"AccountNumber\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
						"DataColumn Level=\"0\" Caption=\"T\" DataField=\"AccountType\" NumberFormat=\"FormatTex" +
						"t Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capti" +
						"on=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn><C1Data" +
						"Column Level=\"0\" Caption=\"R\" DataField=\"IsRequested\"><ValueItems Presentation=\"C" +
						"heckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"P\" DataF" +
						"ield=\"IsProcessed\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataCo" +
						"lumn><C1DataColumn Level=\"0\" Caption=\"ProcessId\" DataField=\"ProcessId\"><ValueIte" +
						"ms /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"I\" DataField=\"" +
						"Indicator\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
						"tion=\"TOD\" DataField=\"TimeOfDay\"><ValueItems /><GroupInfo /></C1DataColumn><C1Da" +
						"taColumn Level=\"0\" Caption=\"\" DataField=\"\"><ValueItems /><GroupInfo /></C1DataCo" +
						"lumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>H" +
						"ighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style85{}Inactive{ForeC" +
						"olor:InactiveCaptionText;BackColor:InactiveCaption;}Style78{}Style79{}Selected{F" +
						"oreColor:HighlightText;BackColor:Highlight;}Editor{}Style72{}Style73{}Style70{Ba" +
						"ckColor:255, 251, 242;}Style71{}Style76{BackColor:255, 251, 242;}Style77{}Style7" +
						"4{AlignHorz:Center;}Style75{AlignHorz:Center;}Style84{}Style87{AlignHorz:Near;}S" +
						"tyle86{AlignHorz:Near;}Style81{AlignHorz:Center;}Style80{AlignHorz:Center;}Style" +
						"83{}Style82{BackColor:255, 251, 242;}Style89{}Style88{BackColor:255, 251, 242;}S" +
						"tyle90{}Style91{}FilterBar{ForeColor:WindowText;BackColor:SeaShell;}RecordSelect" +
						"or{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1" +
						", 1;ForeColor:ControlText;AlignVert:Center;}Style18{}Style19{}Style14{AlignHorz:" +
						"Near;}Style15{AlignHorz:Near;ForeColor:DarkRed;}Style16{BackColor:255, 251, 242;" +
						"}Style17{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style29{}Style28{Ba" +
						"ckColor:255, 251, 242;}Style9{}Style8{}Style27{AlignHorz:Near;ForeColor:DarkRed;" +
						"}Style26{AlignHorz:Near;AlignVert:Center;}Style5{}Style4{}Style7{}Style6{}Style1" +
						"{}Style3{}Style2{}OddRow{}Style38{AlignHorz:Center;AlignVert:Center;}Style39{Ali" +
						"gnHorz:Center;}Style36{}Style37{}Style34{}Style35{}Style32{}Style33{}Style30{}St" +
						"yle49{}Style48{}Style31{}Normal{Font:Verdana, 8.25pt;}Style41{}Style40{BackColor" +
						":255, 251, 242;}Style43{AlignHorz:Far;}Style42{}Style45{AlignHorz:Near;ForeColor" +
						":DarkRed;}Style44{AlignHorz:Near;}Style47{}Style46{BackColor:255, 251, 242;}Even" +
						"Row{BackColor:LightCyan;}Style58{BackColor:255, 251, 242;}Style59{}Style51{Align" +
						"Horz:Center;}Style50{AlignHorz:Center;AlignVert:Center;}Footer{}Style52{BackColo" +
						"r:255, 251, 242;}Style53{}Style54{}Style55{}Style56{AlignHorz:Near;AlignVert:Cen" +
						"ter;}Style57{AlignHorz:Far;ForeColor:DarkSlateGray;}Caption{AlignHorz:Center;}St" +
						"yle69{AlignHorz:Near;}Style68{AlignHorz:Near;}Style63{AlignHorz:Center;}Style62{" +
						"AlignHorz:Center;}Style61{}Style60{}Style67{}Style66{}Style65{}Style64{BackColor" +
						":255, 251, 242;}Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:Control" +
						"Dark;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VB" +
						"arStyle=\"Always\" AllowColSelect=\"False\" Name=\"\" AllowRowSizing=\"None\" CaptionHei" +
						"ght=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"Tru" +
						"e\" FilterBar=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefR" +
						"ecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle " +
						"parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenR" +
						"owStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"St" +
						"yle13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" m" +
						"e=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle pa" +
						"rent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /" +
						"><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordS" +
						"elector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style pa" +
						"rent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=" +
						"\"Style2\" me=\"Style14\" /><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle paren" +
						"t=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHead" +
						"erStyle parent=\"Style1\" me=\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"Sty" +
						"le18\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><W" +
						"idth>95</Width><Height>15</Height><Locked>True</Locked><FooterDivider>False</Foo" +
						"terDivider><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pare" +
						"nt=\"Style2\" me=\"Style44\" /><Style parent=\"Style1\" me=\"Style45\" /><FooterStyle pa" +
						"rent=\"Style3\" me=\"Style46\" /><EditorStyle parent=\"Style5\" me=\"Style47\" /><GroupH" +
						"eaderStyle parent=\"Style1\" me=\"Style49\" /><GroupFooterStyle parent=\"Style1\" me=\"" +
						"Style48\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider" +
						"><Width>86</Width><Height>15</Height><Locked>True</Locked><FooterDivider>False</" +
						"FooterDivider><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
						"arent=\"Style2\" me=\"Style26\" /><Style parent=\"Style1\" me=\"Style27\" /><FooterStyle" +
						" parent=\"Style3\" me=\"Style28\" /><EditorStyle parent=\"Style5\" me=\"Style29\" /><Gro" +
						"upHeaderStyle parent=\"Style1\" me=\"Style31\" /><GroupFooterStyle parent=\"Style1\" m" +
						"e=\"Style30\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivi" +
						"der><Width>112</Width><Height>15</Height><Locked>True</Locked><FooterDivider>Fal" +
						"se</FooterDivider><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
						"le parent=\"Style2\" me=\"Style38\" /><Style parent=\"Style1\" me=\"Style39\" /><FooterS" +
						"tyle parent=\"Style3\" me=\"Style40\" /><EditorStyle parent=\"Style5\" me=\"Style41\" />" +
						"<GroupHeaderStyle parent=\"Style1\" me=\"Style43\" /><GroupFooterStyle parent=\"Style" +
						"1\" me=\"Style42\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Column" +
						"Divider><Width>50</Width><Height>15</Height><Locked>True</Locked><FooterDivider>" +
						"False</FooterDivider><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><Heading" +
						"Style parent=\"Style2\" me=\"Style56\" /><Style parent=\"Style1\" me=\"Style57\" /><Foot" +
						"erStyle parent=\"Style3\" me=\"Style58\" /><EditorStyle parent=\"Style5\" me=\"Style59\"" +
						" /><GroupHeaderStyle parent=\"Style1\" me=\"Style61\" /><GroupFooterStyle parent=\"St" +
						"yle1\" me=\"Style60\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Col" +
						"umnDivider><Width>143</Width><Height>15</Height><Locked>True</Locked><FooterDivi" +
						"der>False</FooterDivider><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
						"dingStyle parent=\"Style2\" me=\"Style74\" /><Style parent=\"Style1\" me=\"Style75\" /><" +
						"FooterStyle parent=\"Style3\" me=\"Style76\" /><EditorStyle parent=\"Style5\" me=\"Styl" +
						"e77\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style79\" /><GroupFooterStyle parent" +
						"=\"Style1\" me=\"Style78\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single<" +
						"/ColumnDivider><Height>15</Height><FooterDivider>False</FooterDivider><DCIdx>8</" +
						"DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style" +
						"80\" /><Style parent=\"Style1\" me=\"Style81\" /><FooterStyle parent=\"Style3\" me=\"Sty" +
						"le82\" /><EditorStyle parent=\"Style5\" me=\"Style83\" /><GroupHeaderStyle parent=\"St" +
						"yle1\" me=\"Style85\" /><GroupFooterStyle parent=\"Style1\" me=\"Style84\" /><Visible>T" +
						"rue</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>60</Width><He" +
						"ight>15</Height><FooterDivider>False</FooterDivider><DCIdx>9</DCIdx></C1DisplayC" +
						"olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style50\" /><Style paren" +
						"t=\"Style1\" me=\"Style51\" /><FooterStyle parent=\"Style3\" me=\"Style52\" /><EditorSty" +
						"le parent=\"Style5\" me=\"Style53\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style55\"" +
						" /><GroupFooterStyle parent=\"Style1\" me=\"Style54\" /><Visible>True</Visible><Colu" +
						"mnDivider>Gainsboro,Single</ColumnDivider><Width>50</Width><Height>15</Height><F" +
						"ooterDivider>False</FooterDivider><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayCo" +
						"lumn><HeadingStyle parent=\"Style2\" me=\"Style62\" /><Style parent=\"Style1\" me=\"Sty" +
						"le63\" /><FooterStyle parent=\"Style3\" me=\"Style64\" /><EditorStyle parent=\"Style5\"" +
						" me=\"Style65\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style67\" /><GroupFooterSty" +
						"le parent=\"Style1\" me=\"Style66\" /><Visible>True</Visible><ColumnDivider>Black,No" +
						"ne</ColumnDivider><Width>50</Width><Height>15</Height><HeaderDivider>False</Head" +
						"erDivider><FooterDivider>False</FooterDivider><DCIdx>6</DCIdx></C1DisplayColumn>" +
						"<C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style68\" /><Style parent=\"Sty" +
						"le1\" me=\"Style69\" /><FooterStyle parent=\"Style3\" me=\"Style70\" /><EditorStyle par" +
						"ent=\"Style5\" me=\"Style71\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style73\" /><Gr" +
						"oupFooterStyle parent=\"Style1\" me=\"Style72\" /><ColumnDivider>Gainsboro,Single</C" +
						"olumnDivider><Height>15</Height><FooterDivider>False</FooterDivider><DCIdx>7</DC" +
						"Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style86" +
						"\" /><Style parent=\"Style1\" me=\"Style87\" /><FooterStyle parent=\"Style3\" me=\"Style" +
						"88\" /><EditorStyle parent=\"Style5\" me=\"Style89\" /><GroupHeaderStyle parent=\"Styl" +
						"e1\" me=\"Style91\" /><GroupFooterStyle parent=\"Style1\" me=\"Style90\" /><Visible>Tru" +
						"e</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>143</Width><Hei" +
						"ght>15</Height><FooterDivider>False</FooterDivider><DCIdx>10</DCIdx></C1DisplayC" +
						"olumn></internalCols><ClientRect>0, 0, 858, 377</ClientRect><BorderSide>0</Borde" +
						"rSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=" +
						"\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Foo" +
						"ter\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inacti" +
						"ve\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" " +
						"/><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\"" +
						" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelect" +
						"or\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\"" +
						" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Mod" +
						"ified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 858, " +
						"377</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style36\" /><PrintPageFooterS" +
						"tyle parent=\"\" me=\"Style37\" /></Blob>";
				// 
				// StatusLabel
				// 
				this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
				this.StatusLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.StatusLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
				this.StatusLabel.Location = new System.Drawing.Point(20, 416);
				this.StatusLabel.Name = "StatusLabel";
				this.StatusLabel.Size = new System.Drawing.Size(800, 16);
				this.StatusLabel.TabIndex = 8;
				this.StatusLabel.Tag = null;
				this.StatusLabel.TextDetached = true;
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
				// SegEntriesRadioButton
				// 
				this.SegEntriesRadioButton.Checked = true;
				this.SegEntriesRadioButton.Location = new System.Drawing.Point(4, 4);
				this.SegEntriesRadioButton.Name = "SegEntriesRadioButton";
				this.SegEntriesRadioButton.TabIndex = 9;
				this.SegEntriesRadioButton.TabStop = true;
				this.SegEntriesRadioButton.Text = "Seg Entries";
				this.SegEntriesRadioButton.CheckedChanged += new System.EventHandler(this.Radio_CheckChanged);
				// 
				// MemoSegEntriesRadioButton
				// 
				this.MemoSegEntriesRadioButton.Location = new System.Drawing.Point(108, 4);
				this.MemoSegEntriesRadioButton.Name = "MemoSegEntriesRadioButton";
				this.MemoSegEntriesRadioButton.Size = new System.Drawing.Size(136, 24);
				this.MemoSegEntriesRadioButton.TabIndex = 10;
				this.MemoSegEntriesRadioButton.Text = "Memo Seg Entries";
				this.MemoSegEntriesRadioButton.CheckedChanged += new System.EventHandler(this.Radio_CheckChanged);
				// 
				// SubstitutionSegEntriesForm
				// 
				this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
				this.ClientSize = new System.Drawing.Size(864, 433);
				this.ContextMenu = this.MainContextMenu;
				this.Controls.Add(this.MemoSegEntriesRadioButton);
				this.Controls.Add(this.SegEntriesRadioButton);
				this.Controls.Add(this.SegEntriesGrid);
				this.Controls.Add(this.StatusLabel);
				this.DockPadding.Bottom = 20;
				this.DockPadding.Left = 1;
				this.DockPadding.Right = 1;
				this.DockPadding.Top = 32;
				this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
				this.Name = "SubstitutionSegEntriesForm";
				this.Text = "Substitution - Seg Entries";
				this.Load += new System.EventHandler(this.SubstitutionSegEntriesForm_Load);
				((System.ComponentModel.ISupportInitialize)(this.SegEntriesGrid)).EndInit();
				((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
				this.ResumeLayout(false);

		}
		#endregion
    
		private void SegEntriesListLoad()
		{
			try
			{
				mainForm.Alert("Please wait... Loading seg entries data...", PilotState.Unknown);
				this.Cursor = Cursors.WaitCursor;
				this.Refresh();
				
				SegEntriesGrid.Splits[0].DisplayColumns["Indicator"].Visible = true;
				SegEntriesGrid.Splits[0].DisplayColumns["TimeOfDay"].Visible = true;
				SegEntriesGrid.Splits[0].DisplayColumns["AccountNumber"].Visible = true;
				SegEntriesGrid.Splits[0].DisplayColumns["AccountType"].Visible = true;

				dataSet = mainForm.SubstitutionAgent.SubstitutionSegEntriesDataGet(mainForm.ServiceAgent.BizDateExchange(), mainForm.UtcOffset);
				dataView = new DataView(dataSet.Tables["SubstitutionSegEntries"]);

				SegEntriesGrid.SetDataBinding(dataView, null, true);
				StatusSet(); 
				FooterSet();   

				mainForm.Alert("Loading seg entries data... Done!", PilotState.Normal);
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [SubstitutionSegEntriesForm.SegEntriesListLoad]", Log.Error, 1); 
			}

			this.Cursor = Cursors.Default;
		}

		private void MemoSegEntriesListLoad()
		{
			try
			{
				mainForm.Alert("Please wait... Loading meom seg entries data...", PilotState.Unknown);
				this.Cursor = Cursors.WaitCursor;
				this.Refresh();

				SegEntriesGrid.Splits[0].DisplayColumns["Indicator"].Visible = false;
				SegEntriesGrid.Splits[0].DisplayColumns["TimeOfDay"].Visible = false;
				SegEntriesGrid.Splits[0].DisplayColumns["AccountNumber"].Visible = false;
				SegEntriesGrid.Splits[0].DisplayColumns["AccountType"].Visible = false;

				dataSet = mainForm.SubstitutionAgent.SubstitutionMemoSegEntriesDataGet(mainForm.ServiceAgent.BizDateExchange(), mainForm.UtcOffset);
				dataView = new DataView(dataSet.Tables["SubstitutionMemoSegEntries"]);

				SegEntriesGrid.SetDataBinding(dataView, null, true);
				StatusSet(); 
				FooterSet();

				mainForm.Alert("Loading seg entries data... Done!", PilotState.Normal);
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [SubstitutionSegEntriesForm.MemoSegEntriesListLoad]", Log.Error, 1); 
			}

			this.Cursor = Cursors.Default;
		}

		private void SendToClipboard()
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SegEntriesGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\n";

			foreach (int row in SegEntriesGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SegEntriesGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\n";
			}

			Clipboard.SetDataObject(gridData, true);
			mainForm.Alert("Total: " + SegEntriesGrid.SelectedRows.Count + " items copied to clipboard.", PilotState.Normal);
		}

			private void FooterSet()
			{
					long quantity = 0;

					try
					{
							if (SegEntriesRadioButton.Checked)
							{
									for (int index = 0; index < SegEntriesGrid.Splits[0].Rows.Count; index++)
									{
											switch(SegEntriesGrid.Columns["Indicator"].CellText(index).ToString())
											{
													case "I":
															quantity += long.Parse(SegEntriesGrid.Columns["Quantity"].CellValue(index).ToString());
															break;

													case "D":
															quantity -= long.Parse(SegEntriesGrid.Columns["Quantity"].CellValue(index).ToString());
															break;

											}
									}
							}
							else
							{
									for (int index = 0; index < SegEntriesGrid.Splits[0].Rows.Count; index++)
									{												
											quantity += long.Parse(SegEntriesGrid.Columns["Quantity"].CellValue(index).ToString());
									}
							}

							SegEntriesGrid.Columns["Quantity"].FooterText = quantity.ToString("#,##0");		
					}
					catch (Exception error)
					{
							mainForm.Alert(error.Message, PilotState.RunFault);
					}
			}


		private void StatusSet()
		{      
			try
			{
				if (SegEntriesGrid.SelectedRows.Count > 0)
				{
					StatusLabel.Text = "Selected " + SegEntriesGrid.SelectedRows.Count.ToString("#,##0") + " items of "
						+ dataView.Count.ToString("#,##0") + " shown in grid.";
				}
				else
				{
					StatusLabel.Text = "Showing " + dataView.Count.ToString("#,##0") + " items in grid.";
				}
			}
			catch {}
		}

		private void SubstitutionSegEntriesForm_Load(object sender, System.EventArgs e)
		{
			this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
			this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
			this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "500"));
			this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "750"));

			this.Show();
			Application.DoEvents();
      
			SegEntriesListLoad();
		}

		private void SubstitutionSegEntriesForm_Closed(object sender, System.EventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());        
			}
		}

		private void SegEntriesGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
		{
			if(!e.LastRow.Equals(SegEntriesGrid.Row))
			{
				this.Cursor = Cursors.WaitCursor;  
				this.Refresh();
        
				mainForm.SecId = SegEntriesGrid.Columns["SecId"].Text;
        
				this.Cursor = Cursors.Default;
			}
		}

		private void SegEntriesGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			switch (e.KeyChar)
			{
				case (char)3 :
					if (SegEntriesGrid.SelectedRows.Count > 0)
					{
						SendToClipboard();
						e.Handled = true;
					}
					else
					{
						mainForm.Alert("You have not selected any rows to copy.", PilotState.RunFault);
					}
					break;

				case (char)27 :
					if (!SegEntriesGrid.EditActive && SegEntriesGrid.DataChanged)
					{
						SegEntriesGrid.DataChanged = false;
					}
					break;
			}
		}

		private void SegEntriesGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
		{
			StatusSet(); 
			FooterSet();
		}

		private void SegEntriesGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.X <= SegEntriesGrid.RecordSelectorWidth && e.Y <= SegEntriesGrid.RowHeight)
			{
				if (SegEntriesGrid.SelectedRows.Count.Equals(0))
				{
					for (int i = 0; i < SegEntriesGrid.Splits[0,0].Rows.Count; i++)
					{
						SegEntriesGrid.SelectedRows.Add(i);
					}

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SegEntriesGrid.Columns)
					{
						SegEntriesGrid.SelectedCols.Add(dataColumn);
					}
				}
				else
				{
					SegEntriesGrid.SelectedRows.Clear();
					SegEntriesGrid.SelectedCols.Clear();
				}
			}    
		}

		private void SegEntriesGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			StatusSet(); 
			FooterSet();    
		}


		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			SendToClipboard();
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (SegEntriesGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows to copy.", PilotState.RunFault);
				return;
			}

			try
			{
				maxTextLength = new int[SegEntriesGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SegEntriesGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in SegEntriesGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SegEntriesGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SegEntriesGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SegEntriesGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in SegEntriesGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SegEntriesGrid.SelectedCols)
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

				mainForm.Alert("Total: " + SegEntriesGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
			}
			catch (Exception error)
			{       
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [SubstitutionSegEntriesForm.SendToEmailMenuItem_Click]", Log.Error, 1); 
			}
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				Excel excel = new Excel();
				excel.ExportGridToExcel(ref SegEntriesGrid);
			}
			catch {}
		}

		private void SegEntriesGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "Quantity":
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch {}
					break;
				case("ActTime"):
					e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeShortFormat);          
					break;
			}
		}

		private void SegEntriesGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{ 
				mainForm.SubstitutionAgent.SubstitutionSegEntryFlagSet(
					SegEntriesGrid.Columns["ProcessId"].Text, 
					bool.Parse(SegEntriesGrid.Columns["IsRequested"].Text),
					bool.Parse(SegEntriesGrid.Columns["IsProcessed"].Text));
			 
				mainForm.Alert("Seg Entry for Account Number " +  SegEntriesGrid.Columns["AccountNumber"].Text + " was updated.", PilotState.Normal);
			}      
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [SubstitutionSegEntriesForm.SegEntriesGrid_BeforeUpdate]", 1);

				e.Cancel = true;				
			}
		}

		private void Radio_CheckChanged(object sender, System.EventArgs e)
		{
			if (SegEntriesRadioButton.Checked)
			{
				mainForm.GridFilterClear(ref SegEntriesGrid);
				SegEntriesListLoad();
			}
			else
			{
				mainForm.GridFilterClear(ref SegEntriesGrid);
				MemoSegEntriesListLoad();
			}
		}
	}
}