using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class ShortSaleListsNoLendForm : System.Windows.Forms.Form
	{
		private const int xSecId = 0;
    
		private DataSet dataSet;
		private DataView dataView;
		private string dataViewRowFilter;

		private MainForm mainForm;

		private System.Windows.Forms.CheckBox ShowHistoryCheck;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid NoLendGrid;
		private System.Windows.Forms.TextBox DescriptionTextBox;

		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep1MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private C1.Win.C1Input.C1Label StatusLabel;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem ShowMenuItem;
		private System.Windows.Forms.MenuItem ShowBoxPositionMenuItem;
		private System.Windows.Forms.MenuItem ShowInventoryMenuItem;
		private System.Windows.Forms.MenuItem RemoveItemsMenuItem;
    
		private System.ComponentModel.Container components = null;

		public ShortSaleListsNoLendForm(MainForm mainForm)
		{
			this.mainForm = mainForm;

			InitializeComponent();
      
			try
			{
				bool mayEdit = mainForm.AdminAgent.MayEdit(mainForm.UserId, "ShortSaleLists");
        
				NoLendGrid.AllowUpdate = mayEdit;
				NoLendGrid.AllowAddNew = mayEdit;
				NoLendGrid.AllowDelete = mayEdit;
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [ShortSaleListsNoLendForm.ShortSaleListsNoLendForm]", Log.Error, 1); 
			}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleListsNoLendForm));
			this.NoLendGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.ShowHistoryCheck = new System.Windows.Forms.CheckBox();
			this.DescriptionTextBox = new System.Windows.Forms.TextBox();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.ShowMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowBoxPositionMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowInventoryMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.RemoveItemsMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			((System.ComponentModel.ISupportInitialize)(this.NoLendGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// NoLendGrid
			// 
			this.NoLendGrid.AllowColSelect = false;
			this.NoLendGrid.AllowFilter = false;
			this.NoLendGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.NoLendGrid.AllowUpdate = false;
			this.NoLendGrid.AllowUpdateOnBlur = false;
			this.NoLendGrid.AlternatingRows = true;
			this.NoLendGrid.CaptionHeight = 17;
			this.NoLendGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
			this.NoLendGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.NoLendGrid.EmptyRows = true;
			this.NoLendGrid.ExtendRightColumn = true;
			this.NoLendGrid.FilterBar = true;
			this.NoLendGrid.ForeColor = System.Drawing.SystemColors.ControlText;
			this.NoLendGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.NoLendGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.NoLendGrid.Location = new System.Drawing.Point(1, 26);
			this.NoLendGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.NoLendGrid.Name = "NoLendGrid";
			this.NoLendGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.NoLendGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.NoLendGrid.PreviewInfo.ZoomFactor = 75;
			this.NoLendGrid.RecordSelectorWidth = 16;
			this.NoLendGrid.RowDivider.Color = System.Drawing.Color.WhiteSmoke;
			this.NoLendGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.NoLendGrid.RowHeight = 15;
			this.NoLendGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.NoLendGrid.Size = new System.Drawing.Size(630, 303);
			this.NoLendGrid.TabIndex = 0;
			this.NoLendGrid.Text = "NoLend";
			this.NoLendGrid.AfterFilter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.NoLendGrid_AfterFilter);
			this.NoLendGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.NoLendGrid_RowColChange);
			this.NoLendGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.NoLendGrid_BeforeColEdit);
			this.NoLendGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.NoLendGrid_SelChange);
			this.NoLendGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NoLendGrid_MouseDown);
			this.NoLendGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.NoLendGrid_BeforeUpdate);
			this.NoLendGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.NoLendGrid_BeforeDelete);
			this.NoLendGrid.FilterChange += new System.EventHandler(this.NoLendGrid_FilterChange);
			this.NoLendGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.NoLendGrid_FormatText);
			this.NoLendGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NoLendGrid_KeyPress);
			this.NoLendGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Sec ID\" Dat" +
				"aField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\"" +
				" Caption=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn><" +
				"C1DataColumn Level=\"0\" Caption=\"Start Time\" DataField=\"StartTime\" NumberFormat=\"" +
				"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"End Time\" DataField=\"EndTime\" NumberFormat=\"FormatText Event\"><Valu" +
				"eItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Actor\" Dat" +
				"aField=\"ActUserShortName\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColum" +
				"n Level=\"0\" Caption=\"Box Position\" DataField=\"BoxPosition\" NumberFormat=\"FormatT" +
				"ext Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Cap" +
				"tion=\"Inventory Position\" DataField=\"InventoryPosition\" NumberFormat=\"FormatText" +
				" Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Captio" +
				"n=\"Price\" DataField=\"Price\" NumberFormat=\"FormatText Event\"><ValueItems /><Group" +
				"Info /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.Contex" +
				"tWrapper\"><Data>Style58{AlignHorz:Near;}Style59{AlignHorz:Far;}RecordSelector{Al" +
				"ignImage:Center;}Style50{}Style51{}Style52{AlignHorz:Near;}Style53{AlignHorz:Far" +
				";}Style54{}Caption{AlignHorz:Center;}Style56{}Normal{Font:Verdana, 8.25pt;}Selec" +
				"ted{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style31{}Style18{}Style" +
				"19{}Style27{}Style14{AlignHorz:Near;}Style15{AlignHorz:Near;ForeColor:Maroon;}St" +
				"yle16{}Style17{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style47{}Styl" +
				"e46{}Style63{}Style62{}Style61{}Style60{}FilterBar{BackColor:SeaShell;}OddRow{}S" +
				"tyle29{AlignHorz:Far;}Style28{AlignHorz:Near;}HighlightRow{ForeColor:HighlightTe" +
				"xt;BackColor:Highlight;}Style26{}Style25{}Footer{}Style23{}Style22{}Style21{Alig" +
				"nHorz:Near;ForeColor:Maroon;}Style55{}Group{AlignVert:Center;Border:None,,0, 0, " +
				"0, 0;BackColor:ControlDark;}Style57{}Inactive{ForeColor:InactiveCaptionText;Back" +
				"Color:InactiveCaption;}EvenRow{BackColor:LightCyan;}Style6{}Heading{Wrap:True;Ba" +
				"ckColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center" +
				";}Style49{}Style48{}Style24{}Style7{}Style8{}Style1{}Style20{AlignHorz:Near;}Sty" +
				"le5{Trimming:None;}Style41{}Style40{}Style43{}Style42{}Style45{AlignHorz:Near;Fo" +
				"reColor:ControlText;}Style44{AlignHorz:Near;}Style4{}Style9{}Style38{AlignHorz:N" +
				"ear;}Style39{AlignHorz:Near;ForeColor:ControlText;}Style36{}Style37{}Style34{}St" +
				"yle35{}Style32{AlignHorz:Near;}Style33{AlignHorz:Near;ForeColor:ControlText;}Sty" +
				"le30{}Style3{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBa" +
				"rStyle=\"None\" VBarStyle=\"Always\" AllowColSelect=\"False\" Name=\"\" AllowRowSizing=\"" +
				"None\" AlternatingRowStyle=\"True\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" Col" +
				"umnFooterHeight=\"17\" ExtendRightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=\"Dot" +
				"tedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"" +
				"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><Edito" +
				"rStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" " +
				"/><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\"" +
				" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"H" +
				"eading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><In" +
				"activeStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Sty" +
				"le9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyl" +
				"e parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internal" +
				"Cols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent" +
				"=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><Colum" +
				"nDivider>Gainsboro,Single</ColumnDivider><Width>95</Width><Height>15</Height><DC" +
				"Idx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
				"=\"Style20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" " +
				"me=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle par" +
				"ent=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Vi" +
				"sible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>65</Wi" +
				"dth><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style3" +
				"1\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style50\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Height>15<" +
				"/Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
				"\"Style2\" me=\"Style52\" /><Style parent=\"Style1\" me=\"Style53\" /><FooterStyle paren" +
				"t=\"Style3\" me=\"Style54\" /><EditorStyle parent=\"Style5\" me=\"Style55\" /><GroupHead" +
				"erStyle parent=\"Style1\" me=\"Style57\" /><GroupFooterStyle parent=\"Style1\" me=\"Sty" +
				"le56\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width>129</Width><Height>" +
				"15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pare" +
				"nt=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=\"Style59\" /><FooterStyle pa" +
				"rent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Style5\" me=\"Style61\" /><GroupH" +
				"eaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFooterStyle parent=\"Style1\" me=\"" +
				"Style62\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider" +
				"><Height>15</Height><Locked>True</Locked><DCIdx>7</DCIdx></C1DisplayColumn><C1Di" +
				"splayColumn><HeadingStyle parent=\"Style2\" me=\"Style32\" /><Style parent=\"Style1\" " +
				"me=\"Style33\" /><FooterStyle parent=\"Style3\" me=\"Style34\" /><EditorStyle parent=\"" +
				"Style5\" me=\"Style35\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style37\" /><GroupFo" +
				"oterStyle parent=\"Style1\" me=\"Style36\" /><Visible>True</Visible><ColumnDivider>G" +
				"ainsboro,Single</ColumnDivider><Width>115</Width><Height>15</Height><DCIdx>2</DC" +
				"Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style38" +
				"\" /><Style parent=\"Style1\" me=\"Style39\" /><FooterStyle parent=\"Style3\" me=\"Style" +
				"40\" /><EditorStyle parent=\"Style5\" me=\"Style41\" /><GroupHeaderStyle parent=\"Styl" +
				"e1\" me=\"Style43\" /><GroupFooterStyle parent=\"Style1\" me=\"Style42\" /><Visible>Tru" +
				"e</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>115</Width><Hei" +
				"ght>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
				"parent=\"Style2\" me=\"Style44\" /><Style parent=\"Style1\" me=\"Style45\" /><FooterStyl" +
				"e parent=\"Style3\" me=\"Style46\" /><EditorStyle parent=\"Style5\" me=\"Style47\" /><Gr" +
				"oupHeaderStyle parent=\"Style1\" me=\"Style49\" /><GroupFooterStyle parent=\"Style1\" " +
				"me=\"Style48\" /><Visible>True</Visible><ColumnDivider>Gainsboro,None</ColumnDivid" +
				"er><Width>150</Width><Height>15</Height><Locked>True</Locked><DCIdx>4</DCIdx></C" +
				"1DisplayColumn></internalCols><ClientRect>0, 0, 626, 299</ClientRect><BorderSide" +
				">0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style pare" +
				"nt=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading" +
				"\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" m" +
				"e=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=" +
				"\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=" +
				"\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"Rec" +
				"ordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" m" +
				"e=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><L" +
				"ayout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0," +
				" 0, 626, 299</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style26\" /><PrintPa" +
				"geFooterStyle parent=\"\" me=\"Style27\" /></Blob>";
			// 
			// ShowHistoryCheck
			// 
			this.ShowHistoryCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ShowHistoryCheck.Location = new System.Drawing.Point(496, 6);
			this.ShowHistoryCheck.Name = "ShowHistoryCheck";
			this.ShowHistoryCheck.Size = new System.Drawing.Size(108, 16);
			this.ShowHistoryCheck.TabIndex = 1;
			this.ShowHistoryCheck.Text = "&Show History";
			this.ShowHistoryCheck.CheckedChanged += new System.EventHandler(this.ShowHistoryCheck_CheckedChanged);
			// 
			// DescriptionTextBox
			// 
			this.DescriptionTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.DescriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.DescriptionTextBox.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DescriptionTextBox.ForeColor = System.Drawing.Color.Maroon;
			this.DescriptionTextBox.Location = new System.Drawing.Point(24, 6);
			this.DescriptionTextBox.Name = "DescriptionTextBox";
			this.DescriptionTextBox.ReadOnly = true;
			this.DescriptionTextBox.Size = new System.Drawing.Size(456, 15);
			this.DescriptionTextBox.TabIndex = 2;
			this.DescriptionTextBox.Text = "";
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.ShowMenuItem,
																																										this.SendToMenuItem,
																																										this.RemoveItemsMenuItem,
																																										this.Sep1MenuItem,
																																										this.ExitMenuItem});
			// 
			// ShowMenuItem
			// 
			this.ShowMenuItem.Index = 0;
			this.ShowMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.ShowBoxPositionMenuItem,
																																								 this.ShowInventoryMenuItem});
			this.ShowMenuItem.Text = "Show";
			// 
			// ShowBoxPositionMenuItem
			// 
			this.ShowBoxPositionMenuItem.Index = 0;
			this.ShowBoxPositionMenuItem.Text = "Box Position";
			this.ShowBoxPositionMenuItem.Click += new System.EventHandler(this.ShowBoxPositionMenuItem_Click);
			// 
			// ShowInventoryMenuItem
			// 
			this.ShowInventoryMenuItem.Index = 1;
			this.ShowInventoryMenuItem.Text = "Inventory";
			this.ShowInventoryMenuItem.Click += new System.EventHandler(this.ShowInventoryMenuItem_Click);
			// 
			// SendToMenuItem
			// 
			this.SendToMenuItem.Index = 1;
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
			// RemoveItemsMenuItem
			// 
			this.RemoveItemsMenuItem.Index = 2;
			this.RemoveItemsMenuItem.Text = "Remove Item(s)";
			this.RemoveItemsMenuItem.Click += new System.EventHandler(this.RemoveItemsMenuItem_Click);
			// 
			// Sep1MenuItem
			// 
			this.Sep1MenuItem.Index = 3;
			this.Sep1MenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 4;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StatusLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.StatusLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.StatusLabel.Location = new System.Drawing.Point(16, 333);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(600, 16);
			this.StatusLabel.TabIndex = 9;
			this.StatusLabel.Tag = null;
			this.StatusLabel.TextDetached = true;
			// 
			// ShortSaleListsNoLendForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(632, 349);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.DescriptionTextBox);
			this.Controls.Add(this.NoLendGrid);
			this.Controls.Add(this.ShowHistoryCheck);
			this.DockPadding.Bottom = 20;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 26;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ShortSaleListsNoLendForm";
			this.Text = "ShortSale - Lists - No Lend";
			this.Resize += new System.EventHandler(this.ShortSaleListsNoLendForm_Resize);
			this.Load += new System.EventHandler(this.ShortSaleListsNoLend_Load);
			this.Closed += new System.EventHandler(this.ShortSaleListsNoLendForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.NoLendGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void NoLendGet()
		{
			try
			{
				mainForm.Alert("Please wait... Loading current no-lend data...", PilotState.Unknown);
				this.Cursor = Cursors.WaitCursor;
				this.Refresh();

				dataSet = mainForm.ShortSaleAgent.BorrowNoGet(ShowHistoryCheck.Checked, mainForm.UtcOffset);

				dataViewRowFilter = "";
				
				dataView = new DataView(dataSet.Tables["BorrowNo"], dataViewRowFilter, "", DataViewRowState.CurrentRows);				
				dataView.Sort = "SecId";
        
				NoLendGrid.SetDataBinding(dataView, null, true);
				StatusSet();    

				mainForm.Alert("Loading current no-lend data... Done!", PilotState.Normal);
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [ShortSaleListsNoLendForm.BorrowNoGet]", Log.Error, 1); 
			}

			this.Cursor = Cursors.Default;
		}

		private void SendToClipboard()
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in NoLendGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\n";

			foreach (int row in NoLendGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in NoLendGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\n";
			}

			Clipboard.SetDataObject(gridData, true);
			mainForm.Alert("Total: " + NoLendGrid.SelectedRows.Count + " items copied to clipboard.", PilotState.Normal);
		}

		private void StatusSet()
		{
			if (NoLendGrid.SelectedRows.Count > 0)
			{
				StatusLabel.Text = "Selected " + NoLendGrid.SelectedRows.Count.ToString("#,##0") + " items of "
					+ dataView.Count.ToString("#,##0") + " shown in grid.";
			}
			else
			{
				StatusLabel.Text = "Showing " + dataView.Count.ToString("#,##0") + " items in grid.";
			}
		}

		private void ShortSaleListsNoLend_Load(object sender, System.EventArgs e)
		{
			this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
			this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
			this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "450"));
			this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "645"));

			this.Show();
			Application.DoEvents();

			NoLendGet();
		}

		private void ShortSaleListsNoLendForm_Closed(object sender, System.EventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());            
			}
		}

		private void ShortSaleListsNoLendForm_Resize(object sender, System.EventArgs e)
		{
			DescriptionTextBox.Width = ShowHistoryCheck.Left - 32;
		}

		private void ShowHistoryCheck_CheckedChanged(object sender, System.EventArgs e)
		{
			NoLendGet();
		}
    
		private void NoLendGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0)
			{
				return;
			}

			try
			{
				switch(NoLendGrid.Columns[e.ColIndex].DataField)
				{
					case("StartTime"):
					case("EndTime"):
						e.Value = DateTime.Parse(e.Value).ToString(Standard.DateTimeShortFormat);
						break;
					
					case("InventoryPosition"):
					case("BoxPosition"):
						e.Value = long.Parse(e.Value).ToString("#,##0");
						break;
				
					case("Price"):
						e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
						break;
				}
			}
			catch {}
		}

		private void NoLendGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				if (NoLendGrid.Columns["SecId"].Text.Equals(""))
				{
					mainForm.Alert("The value for Security ID may not be blank.", PilotState.RunFault);
					e.Cancel = true;

					NoLendGrid.Col = xSecId;
					return;
				}

				mainForm.Alert(NoLendGrid.Columns["SecId"].Text
					+ " will be added to 'No-Lend' status.", PilotState.Unknown);

				mainForm.ShortSaleAgent.BorrowNoSet(NoLendGrid.Columns["SecId"].Text.ToUpper(), false, mainForm.UserId);

				NoLendGrid.Columns["SecId"].Text = NoLendGrid.Columns["SecId"].Text.ToUpper();
				NoLendGrid.Columns["StartTime"].Text = DateTime.Now.ToString(Standard.DateTimeShortFormat);
				NoLendGrid.Columns["ActUserShortName"].Text = "me";

				mainForm.Alert(NoLendGrid.Columns["SecId"].Text
					+ " has been added to 'No-Lend' status.", PilotState.Normal);

				NoLendGrid.Col = xSecId;
			}
			catch(Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
				Log.Write(ee.Message + " [ShortSaleListsNoLendForm.NoLendGrid_BeforeUpdate]", Log.Error, 1);

				e.Cancel = true;
			}
		}

		private void NoLendGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			if (!NoLendGrid.Columns["EndTime"].Text.Equals(""))
			{
				mainForm.Alert(NoLendGrid.Columns["SecId"].Text + " [" + NoLendGrid.Columns["Symbol"].Text
					+ "] has already been removed from 'No-Lend' status.", PilotState.RunFault);
			}
			else
			{
				try
				{
					mainForm.Alert(NoLendGrid.Columns["SecId"].Text + " [" + NoLendGrid.Columns["Symbol"].Text
						+ "] will be removed from 'No-Lend' status.", PilotState.Unknown);

					mainForm.ShortSaleAgent.BorrowNoSet(NoLendGrid.Columns["SecId"].Text, true, mainForm.UserId);

					NoLendGrid.Columns["EndTime"].Text = DateTime.Now.ToString(Standard.DateTimeShortFormat);
					NoLendGrid.Columns["ActUserShortName"].Text = "me";

					dataSet.AcceptChanges();

					mainForm.Alert(NoLendGrid.Columns["SecId"].Text + " [" + NoLendGrid.Columns["Symbol"].Text
						+ "] has been removed from 'No-Lend' status.", PilotState.Normal);

					NoLendGrid.Col = xSecId;
				}
				catch(Exception ee)
				{
					mainForm.Alert(ee.Message, PilotState.RunFault);
					Log.Write(ee.Message + " [ShortSaleListsNoLendForm.NoLendGrid_BeforeDelete]", Log.Error, 1); 
				}
				
				NoLendGrid.DataChanged = false;
				e.Cancel = true;
			}
		}

    private void NoLendGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
    {
      if (NoLendGrid.FilterActive) // The user is filtering rows.
      {
        return;
      }

      if(!NoLendGrid.Columns["StartTIme"].Text.Equals("")) // This record is closed.
      {
        e.Cancel = true;
      }

      if(!NoLendGrid.Col.Equals(xSecId)) // This column is not editable.
      {
        e.Cancel = true;
      }    
    }

    private void NoLendGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
    {
      if(!e.LastRow.Equals(NoLendGrid.Row)) // We're on a new row. 
      {
        this.Cursor = Cursors.WaitCursor;
        this.Refresh();
        
        mainForm.SecId = NoLendGrid.Columns["SecId"].Text;
        DescriptionTextBox.Text = mainForm.Description;
        
        this.Cursor = Cursors.Default;
      }    
    }

    private void NoLendGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      switch (e.KeyChar)
      {
        case (char)3 :
          if (NoLendGrid.SelectedRows.Count > 0)
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
          if (!NoLendGrid.EditActive && NoLendGrid.DataChanged)
          {
            NoLendGrid.DataChanged = false;
          }
          break;
      }
    }

    private void NoLendGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.X <= NoLendGrid.RecordSelectorWidth && e.Y <= NoLendGrid.RowHeight)
      {
        if (NoLendGrid.SelectedRows.Count.Equals(0))
        {
          for (int i = 0; i < NoLendGrid.Splits[0,0].Rows.Count; i++)
          {
            NoLendGrid.SelectedRows.Add(i);
          }

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in NoLendGrid.Columns)
          {
            NoLendGrid.SelectedCols.Add(dataColumn);
          }
        }
        else
        {
          NoLendGrid.SelectedRows.Clear();
          NoLendGrid.SelectedCols.Clear();
        }
      }    
    }

    private void NoLendGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      StatusSet();    
    }

    private void NoLendGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
    {
      StatusSet();        
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

      if (NoLendGrid.SelectedCols.Count.Equals(0))
      {
        mainForm.Alert("You have not selected any rows to copy.", PilotState.RunFault);
        return;
      }

      try
      {
        maxTextLength = new int[NoLendGrid.SelectedCols.Count];

        // Get the caption length for each column.
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in NoLendGrid.SelectedCols)
        {
          maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
        }

        // Get the maximum item length for each row in each column.
        foreach (int rowIndex in NoLendGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in NoLendGrid.SelectedCols)
          {
            if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
            {
              maxTextLength[columnIndex] = textLength;
            }
          }
        }

        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in NoLendGrid.SelectedCols)
        {
          gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
        }
        gridData += "\n";
        
        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in NoLendGrid.SelectedCols)
        {
          gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
        }
        gridData += "\n";
        
        foreach (int rowIndex in NoLendGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in NoLendGrid.SelectedCols)
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

        mainForm.Alert("Total: " + NoLendGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
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

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			Excel excel = new Excel();
			excel.ExportGridToExcel(ref NoLendGrid);
		}

		private void ShowBoxPositionMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowBoxPositionMenuItem.Checked = !ShowBoxPositionMenuItem.Checked;
			NoLendGrid.Splits[0, 0].DisplayColumns["BoxPosition"].Visible = ShowBoxPositionMenuItem.Checked;
		}

		private void ShowInventoryMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowInventoryMenuItem.Checked = !ShowInventoryMenuItem.Checked;
			NoLendGrid.Splits[0, 0].DisplayColumns["InventoryPosition"].Visible = ShowInventoryMenuItem.Checked;
		}

		private void RemoveItemsMenuItem_Click(object sender, System.EventArgs e)
		{
			foreach (int index in NoLendGrid.SelectedRows)
			{
				NoLendGrid.Row = index;

				if (!NoLendGrid.Columns["EndTime"].Text.Equals(""))
				{
					mainForm.Alert(NoLendGrid.Columns["SecId"].Text + " [" + NoLendGrid.Columns["Symbol"].Text
						+ "] has already been removed from 'No-Lend' status.", PilotState.RunFault);
				}
				else
				{
					try
					{
						mainForm.Alert(NoLendGrid.Columns["SecId"].Text + " [" + NoLendGrid.Columns["Symbol"].Text
							+ "] will be removed from 'No-Lend' status.", PilotState.Unknown);

						mainForm.ShortSaleAgent.BorrowNoSet(NoLendGrid.Columns["SecId"].Text, true, mainForm.UserId);

						NoLendGrid.Columns["EndTime"].Text = DateTime.Now.ToString(Standard.DateTimeShortFormat);
						NoLendGrid.Columns["ActUserShortName"].Text = "me";

						dataSet.AcceptChanges();

						mainForm.Alert(NoLendGrid.Columns["SecId"].Text + " [" + NoLendGrid.Columns["Symbol"].Text
							+ "] has been removed from 'No-Lend' status.", PilotState.Normal);

						NoLendGrid.Col = xSecId;
					}
					catch(Exception ee)
					{
						mainForm.Alert(ee.Message, PilotState.RunFault);
						Log.Write(ee.Message + " [ShortSaleListsNoLendForm.RemoveItemsMenuItem_Click]", Log.Error, 1); 
					}
				}			

				NoLendGrid.DataChanged = false;
			}
		}

		private void NoLendGrid_FilterChange(object sender, System.EventArgs e)
		{
			string gridFilter;

			try
			{
				gridFilter = mainForm.GridFilterGet(ref NoLendGrid);

				if (gridFilter.Equals(""))
				{
					dataView.RowFilter = dataViewRowFilter;
				}
				else
				{
					if (!dataViewRowFilter.Equals(""))
					{
						dataView.RowFilter = dataViewRowFilter + " AND " + gridFilter;
					}
					else
					{
						dataView.RowFilter = gridFilter;
					}
				}

				StatusSet();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}
  }
}
