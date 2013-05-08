using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class InventoryRatesForm : System.Windows.Forms.Form
	{
		private DataSet dataSet;
		private DataSet fundingRatesDataSet;
		private DataView dataView;

		private MainForm mainForm;
		private C1.Win.C1Input.C1DateEdit DateEditor;
		private C1.Win.C1Input.C1Label EffectDateLabel;
    
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep1MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;

		private C1.Win.C1Input.C1Label StatusLabel;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid InventoryRatesGrid;

		private string secId;
		private string ratesRowFilter;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;		

		private System.ComponentModel.Container components = null;

		public InventoryRatesForm(MainForm mainForm)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InventoryRatesForm));
			this.InventoryRatesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.EffectDateLabel = new C1.Win.C1Input.C1Label();
			this.DateEditor = new C1.Win.C1Input.C1DateEdit();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.InventoryRatesGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DateEditor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// InventoryRatesGrid
			// 
			this.InventoryRatesGrid.AllowColSelect = false;
			this.InventoryRatesGrid.AllowFilter = false;
			this.InventoryRatesGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.InventoryRatesGrid.AllowUpdate = false;
			this.InventoryRatesGrid.AllowUpdateOnBlur = false;
			this.InventoryRatesGrid.AlternatingRows = true;
			this.InventoryRatesGrid.CaptionHeight = 17;
			this.InventoryRatesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.InventoryRatesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InventoryRatesGrid.EmptyRows = true;
			this.InventoryRatesGrid.ExtendRightColumn = true;
			this.InventoryRatesGrid.FilterBar = true;
			this.InventoryRatesGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.InventoryRatesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.InventoryRatesGrid.Location = new System.Drawing.Point(1, 32);
			this.InventoryRatesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.InventoryRatesGrid.Name = "InventoryRatesGrid";
			this.InventoryRatesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.InventoryRatesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.InventoryRatesGrid.PreviewInfo.ZoomFactor = 75;
			this.InventoryRatesGrid.RecordSelectorWidth = 16;
			this.InventoryRatesGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.InventoryRatesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.InventoryRatesGrid.RowHeight = 15;
			this.InventoryRatesGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.InventoryRatesGrid.Size = new System.Drawing.Size(1134, 381);
			this.InventoryRatesGrid.TabIndex = 0;
			this.InventoryRatesGrid.Text = "Inventory Rates";
			this.InventoryRatesGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.InventoryRatesGrid_Paint);
			this.InventoryRatesGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.InventoryRatesGrid_SelChange);
			this.InventoryRatesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.InventoryRatesGrid_BeforeUpdate);
			this.InventoryRatesGrid.FilterChange += new System.EventHandler(this.InventoryRatesGrid_FilterChange);
			this.InventoryRatesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.InventoryRatesGrid_FormatText);
			this.InventoryRatesGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InventoryRatesGrid_KeyPress);
			this.InventoryRatesGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
				"\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
				"l=\"0\" Caption=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"List Date\" DataField=\"ListDate\"><ValueItems" +
				" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Inventory Rate\" " +
				"DataField=\"RateInventory\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupIn" +
				"fo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"ActUserId\" DataField=\"ActUs" +
				"erId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=" +
				"\"Act Time\" DataField=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /><Gr" +
				"oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Borrow Rate\" DataField" +
				"=\"RateBorrow\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"Loan Rate\" DataField=\"RateLoan\" NumberF" +
				"ormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
				" Level=\"0\" Caption=\"\" DataField=\"RateBorrowDifference\" NumberFormat=\"FormatText " +
				"Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption" +
				"=\"\" DataField=\"RateLoanDifference\" NumberFormat=\"FormatText Event\"><ValueItems /" +
				"><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design" +
				".ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Highlight;" +
				"}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Style78{}Styl" +
				"e79{}Style85{}Editor{}Style72{}Style73{}Style70{}Style71{}Style76{}Style77{}Styl" +
				"e74{AlignHorz:Center;}Style75{AlignHorz:Center;}Style84{}Style81{AlignHorz:Cente" +
				"r;}Style80{AlignHorz:Center;}Style83{}Style82{}FilterBar{ForeColor:WindowText;Ba" +
				"ckColor:SeaShell;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;F" +
				"oreColor:ControlText;BackColor:Control;}Style18{}Style19{}Style14{AlignHorz:Near" +
				";}Style15{AlignHorz:Near;ForeColor:DarkRed;}Style16{}Style17{}Style10{AlignHorz:" +
				"Near;}Style11{}Style12{}Style13{}Selected{ForeColor:HighlightText;BackColor:High" +
				"light;}Style29{}Style28{}Style27{AlignHorz:Far;}Style9{}Style8{}Style22{}Style24" +
				"{}Style5{}Style26{AlignHorz:Near;}Style25{}Style6{}Style1{}Style23{}Style3{}Styl" +
				"e2{}Style21{AlignHorz:Near;ForeColor:DarkRed;}Style20{AlignHorz:Near;}OddRow{}St" +
				"yle38{AlignHorz:Near;}Style39{AlignHorz:Center;ForeColor:DarkSlateGray;}Style36{" +
				"}Style37{}Style34{}Style35{}Style32{}Style33{}Style30{}Style49{}Style48{}Style31" +
				"{}Normal{Font:Verdana, 8.25pt;}Style41{}Style40{}Style43{}Style42{}Style45{Align" +
				"Horz:Near;}Style44{AlignHorz:Near;}Style47{}Style46{}EvenRow{BackColor:LightCyan" +
				";}Style4{}Style7{}RecordSelector{AlignImage:Center;}Style51{AlignHorz:Near;}Styl" +
				"e50{AlignHorz:Near;}Footer{}Style52{}Style53{}Style54{}Style55{}Caption{AlignHor" +
				"z:Center;}Style69{AlignHorz:Far;}Style68{AlignHorz:Near;}Style63{AlignHorz:Far;}" +
				"Style62{AlignHorz:Near;}Style67{}Style66{}Style65{}Style64{}Group{BackColor:Cont" +
				"rolDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}</Data></Styles><Splits><C1.Wi" +
				"n.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSelect=\"Fal" +
				"se\" Name=\"\" AllowRowSizing=\"None\" AlternatingRowStyle=\"True\" CaptionHeight=\"17\" " +
				"ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" Filter" +
				"Bar=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidt" +
				"h=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"S" +
				"tyle2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle p" +
				"arent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" />" +
				"<FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style1" +
				"2\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"Hig" +
				"hlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowS" +
				"tyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" " +
				"me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Nor" +
				"mal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" " +
				"me=\"Style38\" /><Style parent=\"Style1\" me=\"Style39\" /><FooterStyle parent=\"Style3" +
				"\" me=\"Style40\" /><EditorStyle parent=\"Style5\" me=\"Style41\" /><GroupHeaderStyle p" +
				"arent=\"Style1\" me=\"Style43\" /><GroupFooterStyle parent=\"Style1\" me=\"Style42\" /><" +
				"ColumnDivider>LightGray,Single</ColumnDivider><Width>85</Width><Height>15</Heigh" +
				"t><Locked>True</Locked><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style14\" /><Style parent=\"Style1\" me=\"Style15\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"Style1" +
				"7\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style18\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</C" +
				"olumnDivider><Width>95</Width><Height>15</Height><Locked>True</Locked><DCIdx>0</" +
				"DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style" +
				"20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Sty" +
				"le22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"St" +
				"yle1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Visible>T" +
				"rue</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>75</Width><He" +
				"ight>15</Height><Locked>True</Locked><DCIdx>1</DCIdx></C1DisplayColumn><C1Displa" +
				"yColumn><HeadingStyle parent=\"Style2\" me=\"Style26\" /><Style parent=\"Style1\" me=\"" +
				"Style27\" /><FooterStyle parent=\"Style3\" me=\"Style28\" /><EditorStyle parent=\"Styl" +
				"e5\" me=\"Style29\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style31\" /><GroupFooter" +
				"Style parent=\"Style1\" me=\"Style30\" /><Visible>True</Visible><ColumnDivider>Light" +
				"Gray,Single</ColumnDivider><Width>130</Width><Height>15</Height><DCIdx>3</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style62\" />" +
				"<Style parent=\"Style1\" me=\"Style63\" /><FooterStyle parent=\"Style3\" me=\"Style64\" " +
				"/><EditorStyle parent=\"Style5\" me=\"Style65\" /><GroupHeaderStyle parent=\"Style1\" " +
				"me=\"Style67\" /><GroupFooterStyle parent=\"Style1\" me=\"Style66\" /><Visible>True</V" +
				"isible><ColumnDivider>LightGray,Single</ColumnDivider><Width>130</Width><Height>" +
				"15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pare" +
				"nt=\"Style2\" me=\"Style74\" /><Style parent=\"Style1\" me=\"Style75\" /><FooterStyle pa" +
				"rent=\"Style3\" me=\"Style76\" /><EditorStyle parent=\"Style5\" me=\"Style77\" /><GroupH" +
				"eaderStyle parent=\"Style1\" me=\"Style79\" /><GroupFooterStyle parent=\"Style1\" me=\"" +
				"Style78\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider" +
				"><Width>50</Width><Height>15</Height><DCIdx>8</DCIdx></C1DisplayColumn><C1Displa" +
				"yColumn><HeadingStyle parent=\"Style2\" me=\"Style68\" /><Style parent=\"Style1\" me=\"" +
				"Style69\" /><FooterStyle parent=\"Style3\" me=\"Style70\" /><EditorStyle parent=\"Styl" +
				"e5\" me=\"Style71\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style73\" /><GroupFooter" +
				"Style parent=\"Style1\" me=\"Style72\" /><Visible>True</Visible><ColumnDivider>Light" +
				"Gray,Single</ColumnDivider><Width>130</Width><Height>15</Height><DCIdx>7</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style80\" />" +
				"<Style parent=\"Style1\" me=\"Style81\" /><FooterStyle parent=\"Style3\" me=\"Style82\" " +
				"/><EditorStyle parent=\"Style5\" me=\"Style83\" /><GroupHeaderStyle parent=\"Style1\" " +
				"me=\"Style85\" /><GroupFooterStyle parent=\"Style1\" me=\"Style84\" /><Visible>True</V" +
				"isible><ColumnDivider>LightGray,Single</ColumnDivider><Width>50</Width><Height>1" +
				"5</Height><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle paren" +
				"t=\"Style2\" me=\"Style44\" /><Style parent=\"Style1\" me=\"Style45\" /><FooterStyle par" +
				"ent=\"Style3\" me=\"Style46\" /><EditorStyle parent=\"Style5\" me=\"Style47\" /><GroupHe" +
				"aderStyle parent=\"Style1\" me=\"Style49\" /><GroupFooterStyle parent=\"Style1\" me=\"S" +
				"tyle48\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider>" +
				"<Height>15</Height><Locked>True</Locked><DCIdx>4</DCIdx></C1DisplayColumn><C1Dis" +
				"playColumn><HeadingStyle parent=\"Style2\" me=\"Style50\" /><Style parent=\"Style1\" m" +
				"e=\"Style51\" /><FooterStyle parent=\"Style3\" me=\"Style52\" /><EditorStyle parent=\"S" +
				"tyle5\" me=\"Style53\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style55\" /><GroupFoo" +
				"terStyle parent=\"Style1\" me=\"Style54\" /><Visible>True</Visible><ColumnDivider>Li" +
				"ghtGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx>5</" +
				"DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 1130, 377</ClientRect><" +
				"BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><" +
				"Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style paren" +
				"t=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"" +
				"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"N" +
				"ormal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"N" +
				"ormal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Headin" +
				"g\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"" +
				"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</hor" +
				"zSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><Cli" +
				"entArea>0, 0, 1130, 377</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style36\"" +
				" /><PrintPageFooterStyle parent=\"\" me=\"Style37\" /></Blob>";
			// 
			// EffectDateLabel
			// 
			this.EffectDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EffectDateLabel.Location = new System.Drawing.Point(868, 7);
			this.EffectDateLabel.Name = "EffectDateLabel";
			this.EffectDateLabel.Size = new System.Drawing.Size(92, 16);
			this.EffectDateLabel.TabIndex = 1;
			this.EffectDateLabel.Tag = null;
			this.EffectDateLabel.Text = "In Effect For:";
			this.EffectDateLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			this.EffectDateLabel.TextDetached = true;
			// 
			// DateEditor
			// 
			this.DateEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DateEditor.AutoSize = false;
			// 
			// DateEditor.Calendar
			// 
			this.DateEditor.Calendar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DateEditor.CustomFormat = "yyyy-MM-dd";
			this.DateEditor.DateTimeInput = false;
			this.DateEditor.DisplayFormat.CustomFormat = "yyyy-MM-dd";
			this.DateEditor.DisplayFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.DateEditor.DisplayFormat.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.None;
			this.DateEditor.DisplayFormat.TrimStart = true;
			this.DateEditor.DropDownFormAlign = C1.Win.C1Input.DropDownFormAlignmentEnum.Right;
			this.DateEditor.EditFormat.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.DateEditor.EditFormat.Inherit = C1.Win.C1Input.FormatInfoInheritFlags.CustomFormat;
			this.DateEditor.ErrorInfo.BeepOnError = true;
			this.DateEditor.ErrorInfo.CanLoseFocus = true;
			this.DateEditor.ErrorInfo.ShowErrorMessage = false;
			this.DateEditor.Location = new System.Drawing.Point(964, 5);
			this.DateEditor.Name = "DateEditor";
			this.DateEditor.Size = new System.Drawing.Size(96, 20);
			this.DateEditor.TabIndex = 2;
			this.DateEditor.Tag = null;
			this.DateEditor.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.DateEditor.TextChanged += new System.EventHandler(this.DateEditor_TextChanged);
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
																																									 this.SendToEmailMenuItem,
																																									 this.SendToExcelMenuItem});
			this.SendToMenuItem.Text = "Send To";
			// 
			// SendToClipboardMenuItem
			// 
			this.SendToClipboardMenuItem.Index = 0;
			this.SendToClipboardMenuItem.Text = "Clipboard";
			this.SendToClipboardMenuItem.Click += new System.EventHandler(this.SendToClipboardMenuItem_Click);
			// 
			// SendToEmailMenuItem
			// 
			this.SendToEmailMenuItem.Index = 1;
			this.SendToEmailMenuItem.Text = "Mail Recipient";
			this.SendToEmailMenuItem.Click += new System.EventHandler(this.SendToEmailMenuItem_Click);
			// 
			// SendToExcelMenuItem
			// 
			this.SendToExcelMenuItem.Index = 2;
			this.SendToExcelMenuItem.Text = "Excel";
			this.SendToExcelMenuItem.Click += new System.EventHandler(this.SendToExcelMenuItem_Click);
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
			// InventoryRatesForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1136, 433);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.DateEditor);
			this.Controls.Add(this.EffectDateLabel);
			this.Controls.Add(this.InventoryRatesGrid);
			this.DockPadding.Bottom = 20;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 32;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "InventoryRatesForm";
			this.Text = "Inventory - Rates";
			this.Load += new System.EventHandler(this.ShortSaleListsThresholdForm_Load);
			this.Closed += new System.EventHandler(this.ShortSaleListsThresholdForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.InventoryRatesGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DateEditor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
    
		private void ListLoad(string effectDate)
		{    
			try
			{      
				mainForm.Alert("Please wait... Loading inventory rates data...", PilotState.Unknown);
				this.Cursor = Cursors.WaitCursor;
				this.Refresh();

				ratesRowFilter = "";
				
				dataSet = mainForm.ShortSaleAgent.InventoryRatesGet(effectDate);

				dataView = new DataView(dataSet.Tables["InventoryRates"], ratesRowFilter, "Symbol", DataViewRowState.CurrentRows);        
        
				InventoryRatesGrid.SetDataBinding(dataView, null, true);
				StatusSet();    				

				mainForm.Alert("Loading inventory rates data... Done!", PilotState.Normal);
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [InventoryRatesForm.ListLoad]", Log.Error, 1); 
			}				
			this.Cursor = Cursors.Default;
		}

		private void SendToClipboard()
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventoryRatesGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\n";

			foreach (int row in InventoryRatesGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventoryRatesGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\n";
			}

			Clipboard.SetDataObject(gridData, true);
			mainForm.Alert("Total: " + InventoryRatesGrid.SelectedRows.Count + " items copied to clipboard.", PilotState.Normal);
		}

		private void StatusSet()
		{
			if (InventoryRatesGrid.SelectedRows.Count > 0)
			{
				StatusLabel.Text = "Selected " + InventoryRatesGrid.SelectedRows.Count.ToString("#,##0") + " items of "
					+ dataView.Count.ToString("#,##0") + " shown in grid.";
			}
			else
			{
				StatusLabel.Text = "Showing " + dataView.Count.ToString("#,##0") + " items in grid.";
			}
		}

		private void ShortSaleListsThresholdForm_Load(object sender, System.EventArgs e)
		{
			this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
			this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
			this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "500"));
			this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "750"));
		
			InventoryRatesGrid.AllowUpdate = mainForm.AdminAgent.MayEdit(mainForm.UserId, "InventorySubscriber");
			
			secId = "";

			DateEditor.Text = mainForm.ServiceAgent.BizDate();			
		}

		private void ShortSaleListsThresholdForm_Closed(object sender, System.EventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());        
			}
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (InventoryRatesGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows to copy.", PilotState.RunFault);
				return;
			}

			try
			{
				maxTextLength = new int[InventoryRatesGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventoryRatesGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in InventoryRatesGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventoryRatesGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventoryRatesGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventoryRatesGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in InventoryRatesGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventoryRatesGrid.SelectedCols)
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

				mainForm.Alert("Total: " + InventoryRatesGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
			}
			catch (Exception error)
			{       
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [ShortSaleListsNoLendForm.SendToEmailMenuItem_Click]", Log.Error, 1); 
			}
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			SendToClipboard();
		}

		private void DateEditor_TextChanged(object sender, System.EventArgs e)
		{
			if (!DateEditor.Text.Equals(""))
			{
				ListLoad(DateEditor.Text);
			}
		}

		private void InventoryRatesGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch(InventoryRatesGrid.Columns[e.ColIndex].DataField)
			{
				case "RateBorrow":
				case "RateLoan":
				case "RateBorrowDifference":
				case "RateLoanDifference":
				case "RateInventory":
					try
					{
						e.Value = float.Parse(e.Value.ToString()).ToString("#0.###");
					}
					catch{}
					break;
					
				case "ActTime":
					try
					{
						e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateTimeFormat);
					}
					catch{}
					break;
			}
		}

		private void InventoryRatesGrid_FilterChange(object sender, System.EventArgs e)
		{
			string gridFilter;

			try
			{
				gridFilter = mainForm.GridFilterGet(ref InventoryRatesGrid);

				if (gridFilter.Equals(""))
				{
					dataView.RowFilter = ratesRowFilter;
				}
				else
				{
					dataView.RowFilter = gridFilter;
				}
			}
			catch (Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
			}

			StatusSet();
		}

		private void InventoryRatesGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{ 	
			if (!secId.Equals(InventoryRatesGrid.Columns["SecId"].Text))
			{
				this.Cursor = Cursors.WaitCursor;
				secId = InventoryRatesGrid.Columns["SecId"].Text;
				mainForm.SecId = secId;
				this.Cursor = Cursors.Default;
			}
		}

		private void InventoryRatesGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				mainForm.ShortSaleAgent.InventoryRateSet(
					InventoryRatesGrid.Columns["SecId"].Text,
					InventoryRatesGrid.Columns["RateInventory"].Text,
					mainForm.UserId);
			
				InventoryRatesGrid.Columns["ActUserId"].Text = mainForm.UserId;
				InventoryRatesGrid.Columns["ActTime"].Text = DateTime.Now.ToString();
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [InventoryRatesForm.InventoryRatesGrid_BeforeUpdate]", Log.Error, 1); 
			}
		}

		private void InventoryRatesGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char) 13))
			{
				InventoryRatesGrid.UpdateData();
			}
		}

		private void InventoryRatesGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			StatusSet();
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			Excel excel = new Excel();
			excel.ExportGridToExcel(ref InventoryRatesGrid);			
		}		
	}
}