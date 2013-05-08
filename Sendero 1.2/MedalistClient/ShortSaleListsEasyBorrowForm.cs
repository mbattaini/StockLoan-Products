using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class ShortSaleListsEasyBorrowForm : System.Windows.Forms.Form
  {
    private DataSet dataSet;
    private DataView dataView;

    private MainForm mainForm;

		private bool mayEdit = false;
    
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid EasyBorrowGrid;
    
    private System.Windows.Forms.ContextMenu MainContextMenu;
    private System.Windows.Forms.MenuItem SendToMenuItem;
    private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
    private System.Windows.Forms.MenuItem SendToEmailMenuItem;
    private System.Windows.Forms.MenuItem Sep1MenuItem;
    private System.Windows.Forms.MenuItem ExitMenuItem;
    
    private C1.Win.C1Input.C1Label StatusLabel;
		private System.Windows.Forms.MenuItem RemoveItemsMenuItem;
		private C1.Win.C1Input.C1DateEdit DateEditor;
		private C1.Win.C1Input.C1Label EffectDateLabel;

    private System.ComponentModel.Container components = null;

    public ShortSaleListsEasyBorrowForm(MainForm mainForm)
    {
      this.mainForm = mainForm;
      
			try
			{
				mayEdit = mainForm.AdminAgent.MayEdit(mainForm.UserId, "ShortSaleLists");
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [ShortSaleListsEasyBorrowForm.ShortSaleListsEasyBorrowForm]", Log.Error, 1); 
			}

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleListsEasyBorrowForm));
			this.EasyBorrowGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.RemoveItemsMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.DateEditor = new C1.Win.C1Input.C1DateEdit();
			this.EffectDateLabel = new C1.Win.C1Input.C1Label();
			((System.ComponentModel.ISupportInitialize)(this.EasyBorrowGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DateEditor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// EasyBorrowGrid
			// 
			this.EasyBorrowGrid.AllowColSelect = false;
			this.EasyBorrowGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.EasyBorrowGrid.AllowUpdateOnBlur = false;
			this.EasyBorrowGrid.AlternatingRows = true;
			this.EasyBorrowGrid.CaptionHeight = 17;
			this.EasyBorrowGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
			this.EasyBorrowGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.EasyBorrowGrid.EmptyRows = true;
			this.EasyBorrowGrid.ExtendRightColumn = true;
			this.EasyBorrowGrid.FilterBar = true;
			this.EasyBorrowGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.EasyBorrowGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.EasyBorrowGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.EasyBorrowGrid.Location = new System.Drawing.Point(1, 32);
			this.EasyBorrowGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.EasyBorrowGrid.Name = "EasyBorrowGrid";
			this.EasyBorrowGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.EasyBorrowGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.EasyBorrowGrid.PreviewInfo.ZoomFactor = 75;
			this.EasyBorrowGrid.RecordSelectorWidth = 16;
			this.EasyBorrowGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.EasyBorrowGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.EasyBorrowGrid.RowHeight = 15;
			this.EasyBorrowGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.EasyBorrowGrid.Size = new System.Drawing.Size(662, 273);
			this.EasyBorrowGrid.TabIndex = 0;
			this.EasyBorrowGrid.Text = "EasyBorrow";
			this.EasyBorrowGrid.AfterFilter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.EasyBorrowGrid_AfterFilter);
			this.EasyBorrowGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.EasyBorrowGrid_RowColChange);
			this.EasyBorrowGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.EasyBorrowGrid_SelChange);
			this.EasyBorrowGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EasyBorrowGrid_MouseDown);
			this.EasyBorrowGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.EasyBorrowGrid_BeforeUpdate);
			this.EasyBorrowGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.EasyBorrowGrid_FormatText);
			this.EasyBorrowGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EasyBorrowGrid_KeyPress);
			this.EasyBorrowGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
				"\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
				"l=\"0\" Caption=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"Quantity\" DataField=\"Quantity\" NumberFormat" +
				"=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
				"l=\"0\" Caption=\"SSE\" DataField=\"IsShortSaleEasy\"><ValueItems Presentation=\"CheckB" +
				"ox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Actor\" DataFi" +
				"eld=\"ActUserId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
				"\" Caption=\"Act Time\" DataField=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueI" +
				"tems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Dvp\" DataFie" +
				"ld=\"DvpFailOutDayCount\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
				"Level=\"0\" Caption=\"Clearing\" DataField=\"ClearingFailOutDayCount\"><ValueItems /><" +
				"GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Broker\" DataField=\"B" +
				"rokerFailOutDayCount\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"Price\" DataField=\"Price\" NumberFormat=\"FormatText Event\"><Value" +
				"Items /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid" +
				".Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;BackColor:Hig" +
				"hlight;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Select" +
				"ed{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style72{AlignHorz:Near;}" +
				"Style73{AlignHorz:Far;}Style70{}Style71{}Style76{}Style77{}Style74{}Style75{}Fil" +
				"terBar{BackColor:SeaShell;}Heading{Wrap:True;BackColor:Control;Border:Raised,,1," +
				" 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style18{}Style19{}Style14{Align" +
				"Horz:Near;}Style15{AlignHorz:Near;ForeColor:Navy;}Style16{}Style17{}Style10{Alig" +
				"nHorz:Near;}Style11{}Style12{}Style13{}Style27{AlignHorz:Far;ForeColor:ControlTe" +
				"xt;}Style29{}Style28{}Style26{AlignHorz:Center;}Style25{}Style9{}Style8{}Style24" +
				"{}Style23{}Style5{}Style4{}Style7{}Style6{}Style1{}Style22{}Style3{}Style2{}Styl" +
				"e21{AlignHorz:Near;ForeColor:Navy;}Style20{AlignHorz:Near;}OddRow{}Style38{}Styl" +
				"e39{}Style36{AlignHorz:Center;AlignImage:Center;}Style37{AlignHorz:Center;}Style" +
				"34{}Style35{}Style32{}Style33{}Style30{}Style49{AlignHorz:Near;}Style48{AlignHor" +
				"z:Near;}Style31{}Normal{Font:Tahoma, 11world;}Style41{}Style40{}Style43{AlignHor" +
				"z:Near;}Style42{AlignHorz:Near;}Style45{}Style44{}Style47{}Style46{}EvenRow{Back" +
				"Color:LightCyan;}Style59{}Style58{}RecordSelector{AlignImage:Center;}Style51{}St" +
				"yle50{}Footer{}Style52{}Style53{}Style54{AlignHorz:Center;}Style55{AlignHorz:Cen" +
				"ter;}Style56{}Style57{}Caption{AlignHorz:Center;}Style69{}Style68{}Style63{}Styl" +
				"e62{}Style61{AlignHorz:Center;}Style60{AlignHorz:Center;}Style67{AlignHorz:Cente" +
				"r;}Style66{AlignHorz:Center;}Style65{}Style64{}Group{AlignVert:Center;Border:Non" +
				"e,,0, 0, 0, 0;BackColor:ControlDark;}</Data></Styles><Splits><C1.Win.C1TrueDBGri" +
				"d.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSelect=\"False\" Name=\"\" A" +
				"llowRowSizing=\"None\" AlternatingRowStyle=\"True\" CaptionHeight=\"17\" ColumnCaption" +
				"Height=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FilterBar=\"True\" Ma" +
				"rqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" Vertic" +
				"alScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"St" +
				"yle10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRo" +
				"w\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle " +
				"parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><Heading" +
				"Style parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me" +
				"=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"" +
				"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" " +
				"/><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Styl" +
				"e1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" " +
				"/><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16" +
				"\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1" +
				"\" me=\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True<" +
				"/Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>95</Width><Height" +
				">15</Height><Locked>True</Locked><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayCol" +
				"umn><HeadingStyle parent=\"Style2\" me=\"Style20\" /><Style parent=\"Style1\" me=\"Styl" +
				"e21\" /><FooterStyle parent=\"Style3\" me=\"Style22\" /><EditorStyle parent=\"Style5\" " +
				"me=\"Style23\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style25\" /><GroupFooterStyl" +
				"e parent=\"Style1\" me=\"Style24\" /><Visible>True</Visible><ColumnDivider>Gainsboro" +
				",Single</ColumnDivider><Width>75</Width><Height>15</Height><Locked>True</Locked>" +
				"<DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\"" +
				" me=\"Style72\" /><Style parent=\"Style1\" me=\"Style73\" /><FooterStyle parent=\"Style" +
				"3\" me=\"Style74\" /><EditorStyle parent=\"Style5\" me=\"Style75\" /><GroupHeaderStyle " +
				"parent=\"Style1\" me=\"Style77\" /><GroupFooterStyle parent=\"Style1\" me=\"Style76\" />" +
				"<Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>70<" +
				"/Width><Height>15</Height><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style2\" me=\"Style26\" /><Style parent=\"Style1\" me=\"Style27\" />" +
				"<FooterStyle parent=\"Style3\" me=\"Style28\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
				"le29\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style31\" /><GroupFooterStyle paren" +
				"t=\"Style1\" me=\"Style30\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single" +
				"</ColumnDivider><Width>75</Width><Height>15</Height><Locked>True</Locked><DCIdx>" +
				"2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle36\" /><Style parent=\"Style1\" me=\"Style37\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style38\" /><EditorStyle parent=\"Style5\" me=\"Style39\" /><GroupHeaderStyle parent=" +
				"\"Style1\" me=\"Style41\" /><GroupFooterStyle parent=\"Style1\" me=\"Style40\" /><Visibl" +
				"e>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>38</Width>" +
				"<Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSt" +
				"yle parent=\"Style2\" me=\"Style54\" /><Style parent=\"Style1\" me=\"Style55\" /><Footer" +
				"Style parent=\"Style3\" me=\"Style56\" /><EditorStyle parent=\"Style5\" me=\"Style57\" /" +
				"><GroupHeaderStyle parent=\"Style1\" me=\"Style59\" /><GroupFooterStyle parent=\"Styl" +
				"e1\" me=\"Style58\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</Colum" +
				"nDivider><Width>50</Width><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><" +
				"C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style60\" /><Style parent=\"Styl" +
				"e1\" me=\"Style61\" /><FooterStyle parent=\"Style3\" me=\"Style62\" /><EditorStyle pare" +
				"nt=\"Style5\" me=\"Style63\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style65\" /><Gro" +
				"upFooterStyle parent=\"Style1\" me=\"Style64\" /><Visible>True</Visible><ColumnDivid" +
				"er>Gainsboro,Single</ColumnDivider><Width>50</Width><Height>15</Height><DCIdx>7<" +
				"/DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Styl" +
				"e66\" /><Style parent=\"Style1\" me=\"Style67\" /><FooterStyle parent=\"Style3\" me=\"St" +
				"yle68\" /><EditorStyle parent=\"Style5\" me=\"Style69\" /><GroupHeaderStyle parent=\"S" +
				"tyle1\" me=\"Style71\" /><GroupFooterStyle parent=\"Style1\" me=\"Style70\" /><Visible>" +
				"True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>50</Width><H" +
				"eight>15</Height><DCIdx>8</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyl" +
				"e parent=\"Style2\" me=\"Style42\" /><Style parent=\"Style1\" me=\"Style43\" /><FooterSt" +
				"yle parent=\"Style3\" me=\"Style44\" /><EditorStyle parent=\"Style5\" me=\"Style45\" /><" +
				"GroupHeaderStyle parent=\"Style1\" me=\"Style47\" /><GroupFooterStyle parent=\"Style1" +
				"\" me=\"Style46\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnD" +
				"ivider><Height>15</Height><Locked>True</Locked><DCIdx>4</DCIdx></C1DisplayColumn" +
				"><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style48\" /><Style parent=\"St" +
				"yle1\" me=\"Style49\" /><FooterStyle parent=\"Style3\" me=\"Style50\" /><EditorStyle pa" +
				"rent=\"Style5\" me=\"Style51\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style53\" /><G" +
				"roupFooterStyle parent=\"Style1\" me=\"Style52\" /><Visible>True</Visible><ColumnDiv" +
				"ider>Gainsboro,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DC" +
				"Idx>5</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 658, 269</Client" +
				"Rect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedSt" +
				"yles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style" +
				" parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style pa" +
				"rent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style par" +
				"ent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style par" +
				"ent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"" +
				"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style pa" +
				"rent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>" +
				"1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidt" +
				"h><ClientArea>0, 0, 658, 269</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Sty" +
				"le34\" /><PrintPageFooterStyle parent=\"\" me=\"Style35\" /></Blob>";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.StatusLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.StatusLabel.ForeColor = System.Drawing.Color.DarkSlateGray;
			this.StatusLabel.Location = new System.Drawing.Point(16, 309);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(600, 16);
			this.StatusLabel.TabIndex = 9;
			this.StatusLabel.Tag = null;
			this.StatusLabel.TextDetached = true;
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.RemoveItemsMenuItem,
																																										this.SendToMenuItem,
																																										this.Sep1MenuItem,
																																										this.ExitMenuItem});
			// 
			// RemoveItemsMenuItem
			// 
			this.RemoveItemsMenuItem.Index = 0;
			this.RemoveItemsMenuItem.Text = "Remove Item(s)";
			this.RemoveItemsMenuItem.Click += new System.EventHandler(this.RemoveItemsMenuItem_Click);
			// 
			// SendToMenuItem
			// 
			this.SendToMenuItem.Index = 1;
			this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.SendToClipboardMenuItem,
																																									 this.SendToEmailMenuItem});
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
			// Sep1MenuItem
			// 
			this.Sep1MenuItem.Index = 2;
			this.Sep1MenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 3;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
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
			this.DateEditor.Location = new System.Drawing.Point(500, 8);
			this.DateEditor.Name = "DateEditor";
			this.DateEditor.Size = new System.Drawing.Size(96, 20);
			this.DateEditor.TabIndex = 11;
			this.DateEditor.Tag = null;
			this.DateEditor.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.DateEditor.TextChanged += new System.EventHandler(this.DateEditor_TextChanged);			
			this.DateEditor.Validated += new System.EventHandler(this.DateEditor_Validated);
			// 
			// EffectDateLabel
			// 
			this.EffectDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EffectDateLabel.Location = new System.Drawing.Point(404, 8);
			this.EffectDateLabel.Name = "EffectDateLabel";
			this.EffectDateLabel.Size = new System.Drawing.Size(92, 16);
			this.EffectDateLabel.TabIndex = 10;
			this.EffectDateLabel.Tag = null;
			this.EffectDateLabel.Text = "In Effect For:";
			this.EffectDateLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			this.EffectDateLabel.TextDetached = true;
			// 
			// ShortSaleListsEasyBorrowForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(664, 325);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.DateEditor);
			this.Controls.Add(this.EffectDateLabel);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.EasyBorrowGrid);
			this.DockPadding.Bottom = 20;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 32;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ShortSaleListsEasyBorrowForm";
			this.Text = "Short Sale - Lists - Easy Borrow";
			this.Load += new System.EventHandler(this.ShortSaleListsEasyBorrowForm_Load);
			this.Closed += new System.EventHandler(this.ShortSaleListsEasyBorrowForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.EasyBorrowGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DateEditor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).EndInit();
			this.ResumeLayout(false);

		}
    #endregion

    
		private void ListLoad(string effectDate)
		{
			try
			{
				mainForm.Alert("Please wait... Loading easy borrow list data...", PilotState.Unknown);
				this.Cursor = Cursors.WaitCursor;
				this.Refresh();

				dataSet = mainForm.ShortSaleAgent.BorrowEasyList(effectDate, mainForm.UtcOffset);

				dataView = new DataView(dataSet.Tables["BorrowEasyList"]);
				dataView.Sort = "SecId";
        
				EasyBorrowGrid.SetDataBinding(dataView, null, true);
				StatusSet();    

				mainForm.Alert("Loading easy borrow list data... Done!", PilotState.Normal);
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [ShortSaleListsThresholdForm.ListLoad]", Log.Error, 1); 
			}

			this.Cursor = Cursors.Default;
		}

    private void SendToClipboard()
    {
      string gridData = "";

      foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in EasyBorrowGrid.SelectedCols)
      {
        gridData += dataColumn.Caption + "\t";
      }
      gridData += "\n";

      foreach (int row in EasyBorrowGrid.SelectedRows)
      {
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in EasyBorrowGrid.SelectedCols)
        {
          gridData += dataColumn.CellText(row) + "\t";
        }
        gridData += "\n";
      }

      Clipboard.SetDataObject(gridData, true);
      mainForm.Alert("Total: " + EasyBorrowGrid.SelectedRows.Count + " items copied to clipboard.", PilotState.Normal);
    }

    private void StatusSet()
    {
      if (EasyBorrowGrid.SelectedRows.Count > 0)
      {
        StatusLabel.Text = "Selected " + EasyBorrowGrid.SelectedRows.Count.ToString("#,##0") + " items of "
          + dataView.Count.ToString("#,##0") + " shown in grid.";
      }
      else
      {
        StatusLabel.Text = "Showing " + dataView.Count.ToString("#,##0") + " items in grid.";
      }
    }

		private void ShortSaleListsEasyBorrowForm_Load(object sender, System.EventArgs e)
		{
			this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
			this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
			this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "550"));
			this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "350"));

			this.Show();
			Application.DoEvents();
      
			DateEditor.Text = mainForm.ServiceAgent.BizDate();
			ListLoad(DateEditor.Text);
		}

    private void ShortSaleListsEasyBorrowForm_Closed(object sender, System.EventArgs e)
    {
      if(this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
        RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
        RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
        RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
      }
    }

		private void EasyBorrowGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0)
			{
				return;
			}

			try
			{
				switch(EasyBorrowGrid.Columns[e.ColIndex].DataField)
				{
					case("Quantity"):
						e.Value = long.Parse(e.Value).ToString("#,##0");
						break;

					case("Price"):
						e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
						break;

					case("ActTime"):
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeShortFormat);
						break;
				}
			}
			catch {}
		}

    private void EasyBorrowGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
    {
      if(!e.LastRow.Equals(EasyBorrowGrid.Row)) // We're on a new row.
      {
        this.Cursor = Cursors.WaitCursor;    
        this.Refresh();
    
        mainForm.SecId = EasyBorrowGrid.Columns["SecId"].Text;
        
        this.Cursor = Cursors.Default;
      }        
    }

    private void EasyBorrowGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      switch (e.KeyChar)
      {
        case (char)3 :
          if (EasyBorrowGrid.SelectedRows.Count > 0)
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
          if (!EasyBorrowGrid.EditActive && EasyBorrowGrid.DataChanged)
          {
            EasyBorrowGrid.DataChanged = false;
          }
          break;
      }
    }

    private void EasyBorrowGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
    {
      StatusSet();
    }

    private void EasyBorrowGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.X <= EasyBorrowGrid.RecordSelectorWidth && e.Y <= EasyBorrowGrid.RowHeight)
      {
        if (EasyBorrowGrid.SelectedRows.Count.Equals(0))
        {
          for (int i = 0; i < EasyBorrowGrid.Splits[0,0].Rows.Count; i++)
          {
            EasyBorrowGrid.SelectedRows.Add(i);
          }

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in EasyBorrowGrid.Columns)
          {
            EasyBorrowGrid.SelectedCols.Add(dataColumn);
          }
        }
        else
        {
          EasyBorrowGrid.SelectedRows.Clear();
          EasyBorrowGrid.SelectedCols.Clear();
        }
      }
    }

    private void EasyBorrowGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
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

      if (EasyBorrowGrid.SelectedCols.Count.Equals(0))
      {
        mainForm.Alert("You have not selected any rows to copy.", PilotState.RunFault);
        return;
      }

      try
      {
        maxTextLength = new int[EasyBorrowGrid.SelectedCols.Count];

        // Get the caption length for each column.
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in EasyBorrowGrid.SelectedCols)
        {
          maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
        }

        // Get the maximum item length for each row in each column.
        foreach (int rowIndex in EasyBorrowGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in EasyBorrowGrid.SelectedCols)
          {
            if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
            {
              maxTextLength[columnIndex] = textLength;
            }
          }
        }

        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in EasyBorrowGrid.SelectedCols)
        {
          gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
        }
        gridData += "\n";
        
        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in EasyBorrowGrid.SelectedCols)
        {
          gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
        }
        gridData += "\n";
        
        foreach (int rowIndex in EasyBorrowGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in EasyBorrowGrid.SelectedCols)
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

        mainForm.Alert("Total: " + EasyBorrowGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
      }
      catch (Exception error)
      {       
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [ShortSaleListsEasyBorrowForm.SendToEmailMenuItem_Click]", Log.Error, 1); 
      }
    }

    private void ExitMenuItem_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }

		private void EasyBorrowGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				mainForm.ShortSaleAgent.BorrowEasyListSet(
					EasyBorrowGrid.Columns["SecId"].Text,
					mainForm.UserId,
					bool.Parse(EasyBorrowGrid.Columns["IsShortSaleEasy"].Value.ToString()));
			
				EasyBorrowGrid.Columns["ActUserId"].Text = "me";
				EasyBorrowGrid.Columns["ActTime"].Value = DateTime.Now.ToString();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [ShortSaleListsEasyBorrowForm.EasyBorrowGrid_BeforeUpdate]", Log.Error, 1); 
				
				e.Cancel = true;
			}			
		}

		private void RemoveItemsMenuItem_Click(object sender, System.EventArgs e)
		{
			foreach(int i in EasyBorrowGrid.SelectedRows)
			{
				EasyBorrowGrid.Row = i;

				try
				{
					if (bool.Parse(EasyBorrowGrid.Columns["IsShortSaleEasy"].Value.ToString()))
					{
						mainForm.ShortSaleAgent.BorrowEasyListSet(
							EasyBorrowGrid.Columns["SecId"].Text,
							mainForm.UserId,
							false);
				
						EasyBorrowGrid.Columns["IsShortSaleEasy"].Value = false;
						EasyBorrowGrid.Columns["ActUserId"].Text = "me";
						EasyBorrowGrid.Columns["ActTime"].Value = DateTime.Now.ToString();
				
						dataSet.AcceptChanges();
					}
				}
				catch (Exception error)
				{
					mainForm.Alert(error.Message, PilotState.RunFault);
					Log.Write(error.Message + " [ShortSaleListsEasyBorrowForm.RemoveItemsMenuItem_Click]", Log.Error, 1); 													
				}

				EasyBorrowGrid.DataChanged = false;
			}			
		}

		private void DateEditor_Validated(object sender, System.EventArgs e)
		{
			if (!DateEditor.Text.Equals(""))
			{
				ListLoad(DateEditor.Text);
			}
		}
	

		private void DateEditor_TextChanged(object sender, System.EventArgs e)
		{
			if (!DateEditor.Text.Equals(""))
			{
				ListLoad(DateEditor.Text);
			}
		}
  }
}
