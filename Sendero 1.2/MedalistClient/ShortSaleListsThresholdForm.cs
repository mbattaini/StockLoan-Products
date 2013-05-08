using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class ShortSaleListsThresholdForm : System.Windows.Forms.Form
  {
    private DataSet dataSet;
    private DataView dataView;

    private MainForm mainForm;

    private C1.Win.C1TrueDBGrid.C1TrueDBGrid ThresholdGrid;
    private C1.Win.C1Input.C1DateEdit DateEditor;
    private C1.Win.C1Input.C1Label EffectDateLabel;
    
    private System.Windows.Forms.ContextMenu MainContextMenu;
    private System.Windows.Forms.MenuItem SendToMenuItem;
    private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
    private System.Windows.Forms.MenuItem SendToEmailMenuItem;
    private System.Windows.Forms.MenuItem Sep1MenuItem;
    private System.Windows.Forms.MenuItem ExitMenuItem;

    private C1.Win.C1Input.C1Label StatusLabel;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;

    private System.ComponentModel.Container components = null;

    public ShortSaleListsThresholdForm(MainForm mainForm)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleListsThresholdForm));
			this.ThresholdGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.EffectDateLabel = new C1.Win.C1Input.C1Label();
			this.DateEditor = new C1.Win.C1Input.C1DateEdit();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.ThresholdGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DateEditor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// ThresholdGrid
			// 
			this.ThresholdGrid.AllowColSelect = false;
			this.ThresholdGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.ThresholdGrid.AllowUpdate = false;
			this.ThresholdGrid.AllowUpdateOnBlur = false;
			this.ThresholdGrid.AlternatingRows = true;
			this.ThresholdGrid.CaptionHeight = 17;
			this.ThresholdGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
			this.ThresholdGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ThresholdGrid.EmptyRows = true;
			this.ThresholdGrid.ExtendRightColumn = true;
			this.ThresholdGrid.FilterBar = true;
			this.ThresholdGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ThresholdGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.ThresholdGrid.Location = new System.Drawing.Point(1, 32);
			this.ThresholdGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.ThresholdGrid.Name = "ThresholdGrid";
			this.ThresholdGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ThresholdGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ThresholdGrid.PreviewInfo.ZoomFactor = 75;
			this.ThresholdGrid.RecordSelectorWidth = 16;
			this.ThresholdGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.ThresholdGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.ThresholdGrid.RowHeight = 15;
			this.ThresholdGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.ThresholdGrid.Size = new System.Drawing.Size(726, 381);
			this.ThresholdGrid.TabIndex = 0;
			this.ThresholdGrid.Text = "Threshold";
			this.ThresholdGrid.AfterFilter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.ThresholdGrid_AfterFilter);
			this.ThresholdGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.ThresholdGrid_RowColChange);
			this.ThresholdGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ThresholdGrid_SelChange);
			this.ThresholdGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ThresholdGrid_MouseDown);
			this.ThresholdGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ThresholdGrid_FormatText);
			this.ThresholdGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ThresholdGrid_KeyPress);
			this.ThresholdGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
				"\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
				"l=\"0\" Caption=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"Description\" DataField=\"Description\"><Value" +
				"Items /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"List Date\" " +
				"DataField=\"ListDate\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Lev" +
				"el=\"0\" Caption=\"SRO\" DataField=\"Exchange\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"Days\" DataField=\"DayCount\"><ValueItems /><G" +
				"roupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Box Position\" DataFie" +
				"ld=\"NetPositionSettled\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo" +
				" /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWra" +
				"pper\"><Data>Style58{}Style59{}Style50{AlignHorz:Near;}Style51{AlignHorz:Center;F" +
				"oreColor:DarkSlateGray;}Style52{}Style53{}Style54{}Caption{AlignHorz:Center;}Sty" +
				"le56{AlignHorz:Near;}Normal{Font:Verdana, 8.25pt;}Style25{}Selected{ForeColor:Hi" +
				"ghlightText;BackColor:Highlight;}Editor{}Style18{}Style19{}Style27{AlignHorz:Nea" +
				"r;ForeColor:DarkSlateGray;}Style14{AlignHorz:Near;}Style15{AlignHorz:Near;ForeCo" +
				"lor:DarkRed;}Style16{}Style17{}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{" +
				"}Style46{}Style61{}Style60{}Style38{AlignHorz:Near;}Style36{}Style37{}Style4{}St" +
				"yle3{}Style29{}Style28{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight" +
				";}Style26{AlignHorz:Near;}RecordSelector{AlignImage:Center;}Footer{}Style23{}Sty" +
				"le22{}Style21{AlignHorz:Near;ForeColor:DarkRed;}Style55{}Group{BackColor:Control" +
				"Dark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style57{AlignHorz:Far;ForeColor:D" +
				"arkSlateGray;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}" +
				"EvenRow{BackColor:LightCyan;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1" +
				", 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style49{}Style48{}Style24{}St" +
				"yle7{}Style6{}Style1{}Style20{AlignHorz:Near;}Style5{}Style41{}Style40{}Style43{" +
				"}Style42{}Style45{AlignHorz:Center;ForeColor:DarkSlateGray;}Style44{AlignHorz:Ne" +
				"ar;}Style47{}Style9{}Style8{}Style39{AlignHorz:Center;ForeColor:DarkSlateGray;}F" +
				"ilterBar{ForeColor:WindowText;BackColor:SeaShell;}Style12{}Style34{}Style35{}Sty" +
				"le32{}Style33{}Style30{}Style31{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueD" +
				"BGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSelect=\"False\" Name=" +
				"\"\" AllowRowSizing=\"None\" AlternatingRowStyle=\"True\" CaptionHeight=\"17\" ColumnCap" +
				"tionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FilterBar=\"True" +
				"\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" Ve" +
				"rticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me" +
				"=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"Ev" +
				"enRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterSt" +
				"yle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><Hea" +
				"dingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow" +
				"\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle pare" +
				"nt=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style" +
				"11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"" +
				"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style" +
				"38\" /><Style parent=\"Style1\" me=\"Style39\" /><FooterStyle parent=\"Style3\" me=\"Sty" +
				"le40\" /><EditorStyle parent=\"Style5\" me=\"Style41\" /><GroupHeaderStyle parent=\"St" +
				"yle1\" me=\"Style43\" /><GroupFooterStyle parent=\"Style1\" me=\"Style42\" /><Visible>T" +
				"rue</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>85</Width><He" +
				"ight>15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle" +
				" parent=\"Style2\" me=\"Style44\" /><Style parent=\"Style1\" me=\"Style45\" /><FooterSty" +
				"le parent=\"Style3\" me=\"Style46\" /><EditorStyle parent=\"Style5\" me=\"Style47\" /><G" +
				"roupHeaderStyle parent=\"Style1\" me=\"Style49\" /><GroupFooterStyle parent=\"Style1\"" +
				" me=\"Style48\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDi" +
				"vider><Width>50</Width><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style2\" me=\"Style50\" /><Style parent=\"Style1\"" +
				" me=\"Style51\" /><FooterStyle parent=\"Style3\" me=\"Style52\" /><EditorStyle parent=" +
				"\"Style5\" me=\"Style53\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style55\" /><GroupF" +
				"ooterStyle parent=\"Style1\" me=\"Style54\" /><Visible>True</Visible><ColumnDivider>" +
				"LightGray,Single</ColumnDivider><Width>45</Width><Height>15</Height><DCIdx>5</DC" +
				"Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14" +
				"\" /><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style" +
				"16\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Styl" +
				"e1\" me=\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>Tru" +
				"e</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>95</Width><Heig" +
				"ht>15</Height><Locked>True</Locked><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayC" +
				"olumn><HeadingStyle parent=\"Style2\" me=\"Style20\" /><Style parent=\"Style1\" me=\"St" +
				"yle21\" /><FooterStyle parent=\"Style3\" me=\"Style22\" /><EditorStyle parent=\"Style5" +
				"\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style25\" /><GroupFooterSt" +
				"yle parent=\"Style1\" me=\"Style24\" /><Visible>True</Visible><ColumnDivider>LightGr" +
				"ay,Single</ColumnDivider><Width>75</Width><Height>15</Height><Locked>True</Locke" +
				"d><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style" +
				"2\" me=\"Style56\" /><Style parent=\"Style1\" me=\"Style57\" /><FooterStyle parent=\"Sty" +
				"le3\" me=\"Style58\" /><EditorStyle parent=\"Style5\" me=\"Style59\" /><GroupHeaderStyl" +
				"e parent=\"Style1\" me=\"Style61\" /><GroupFooterStyle parent=\"Style1\" me=\"Style60\" " +
				"/><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>1" +
				"18</Width><Height>15</Height><Locked>True</Locked><DCIdx>6</DCIdx></C1DisplayCol" +
				"umn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style26\" /><Style parent=" +
				"\"Style1\" me=\"Style27\" /><FooterStyle parent=\"Style3\" me=\"Style28\" /><EditorStyle" +
				" parent=\"Style5\" me=\"Style29\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style31\" /" +
				"><GroupFooterStyle parent=\"Style1\" me=\"Style30\" /><Visible>True</Visible><Column" +
				"Divider>LightGray,None</ColumnDivider><Width>75</Width><Height>15</Height><Locke" +
				"d>True</Locked><DCIdx>2</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0" +
				", 722, 377</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView" +
				"></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=" +
				"\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Ca" +
				"ption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Sele" +
				"cted\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"Highligh" +
				"tRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\"" +
				" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"Filt" +
				"erBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertS" +
				"plits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16<" +
				"/DefaultRecSelWidth><ClientArea>0, 0, 722, 377</ClientArea><PrintPageHeaderStyle" +
				" parent=\"\" me=\"Style36\" /><PrintPageFooterStyle parent=\"\" me=\"Style37\" /></Blob>" +
				"";
			// 
			// EffectDateLabel
			// 
			this.EffectDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EffectDateLabel.Location = new System.Drawing.Point(460, 6);
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
			this.DateEditor.Location = new System.Drawing.Point(556, 6);
			this.DateEditor.Name = "DateEditor";
			this.DateEditor.Size = new System.Drawing.Size(96, 20);
			this.DateEditor.TabIndex = 2;
			this.DateEditor.Tag = null;
			this.DateEditor.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			this.DateEditor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DateEditor_KeyPress);
			this.DateEditor.Validated += new System.EventHandler(this.DateEditor_Validated);
            this.DateEditor.DropDownClosed += new C1.Win.C1Input.DropDownClosedEventHandler(this.DateEditor_Validated);
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
			// ShortSaleListsThresholdForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(728, 433);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.DateEditor);
			this.Controls.Add(this.EffectDateLabel);
			this.Controls.Add(this.ThresholdGrid);
			this.DockPadding.Bottom = 20;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 32;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ShortSaleListsThresholdForm";
			this.Text = "ShortSale - Lists - Threshold";
			this.Load += new System.EventHandler(this.ShortSaleListsThresholdForm_Load);
			this.Closed += new System.EventHandler(this.ShortSaleListsThresholdForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.ThresholdGrid)).EndInit();
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
        mainForm.Alert("Please wait... Loading threshold list data...", PilotState.Unknown);
        this.Cursor = Cursors.WaitCursor;
        this.Refresh();

        dataSet = mainForm.ShortSaleAgent.ThresholdList(effectDate);

        dataView = new DataView(dataSet.Tables["ThresholdList"]);
        dataView.Sort = "Symbol";
        
        ThresholdGrid.SetDataBinding(dataView, null, true);
        StatusSet();    

        mainForm.Alert("Loading threshold list data... Done!", PilotState.Normal);
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

      foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ThresholdGrid.SelectedCols)
      {
        gridData += dataColumn.Caption + "\t";
      }
      gridData += "\n";

      foreach (int row in ThresholdGrid.SelectedRows)
      {
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ThresholdGrid.SelectedCols)
        {
          gridData += dataColumn.CellText(row) + "\t";
        }
        gridData += "\n";
      }

      Clipboard.SetDataObject(gridData, true);
      mainForm.Alert("Total: " + ThresholdGrid.SelectedRows.Count + " items copied to clipboard.", PilotState.Normal);
    }

    private void StatusSet()
    {
      if (ThresholdGrid.SelectedRows.Count > 0)
      {
        StatusLabel.Text = "Selected " + ThresholdGrid.SelectedRows.Count.ToString("#,##0") + " items of "
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

      DateEditor.Text = DateTime.Today.ToString(Standard.DateFormat);

      this.Show();
      Application.DoEvents();
      
      ListLoad(null);
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

    private void ThresholdGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
    {
      if(!e.LastRow.Equals(ThresholdGrid.Row))
      {
        this.Cursor = Cursors.WaitCursor;  
        this.Refresh();
        
        mainForm.SecId = ThresholdGrid.Columns["SecId"].Text;
        
        this.Cursor = Cursors.Default;
      }
    }

    private void ThresholdGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      switch (e.KeyChar)
      {
        case (char)3 :
          if (ThresholdGrid.SelectedRows.Count > 0)
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
          if (!ThresholdGrid.EditActive && ThresholdGrid.DataChanged)
          {
            ThresholdGrid.DataChanged = false;
          }
          break;
      }
    }

    private void ThresholdGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
    {
      StatusSet();
    }

    private void ThresholdGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.X <= ThresholdGrid.RecordSelectorWidth && e.Y <= ThresholdGrid.RowHeight)
      {
        if (ThresholdGrid.SelectedRows.Count.Equals(0))
        {
          for (int i = 0; i < ThresholdGrid.Splits[0,0].Rows.Count; i++)
          {
            ThresholdGrid.SelectedRows.Add(i);
          }

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ThresholdGrid.Columns)
          {
            ThresholdGrid.SelectedCols.Add(dataColumn);
          }
        }
        else
        {
          ThresholdGrid.SelectedRows.Clear();
          ThresholdGrid.SelectedCols.Clear();
        }
      }    
    }

    private void ThresholdGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      StatusSet();    
    }

    private void DateEditor_Validated(object sender, System.EventArgs e)
    {
      if (!DateEditor.Text.Equals(""))
      {
        ListLoad(DateEditor.Text);
      }
    }

    private void DateEditor_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals((char)13))
      {
        ThresholdGrid.Focus();
        e.Handled = true;
      }
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

      if (ThresholdGrid.SelectedCols.Count.Equals(0))
      {
        mainForm.Alert("You have not selected any rows to copy.", PilotState.RunFault);
        return;
      }

      try
      {
        maxTextLength = new int[ThresholdGrid.SelectedCols.Count];

        // Get the caption length for each column.
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ThresholdGrid.SelectedCols)
        {
          maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
        }

        // Get the maximum item length for each row in each column.
        foreach (int rowIndex in ThresholdGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ThresholdGrid.SelectedCols)
          {
            if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
            {
              maxTextLength[columnIndex] = textLength;
            }
          }
        }

        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ThresholdGrid.SelectedCols)
        {
          gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
        }
        gridData += "\n";
        
        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ThresholdGrid.SelectedCols)
        {
          gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
        }
        gridData += "\n";
        
        foreach (int rowIndex in ThresholdGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ThresholdGrid.SelectedCols)
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

        mainForm.Alert("Total: " + ThresholdGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
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
			try
			{
				Excel excel = new Excel();
				excel.ExportGridToExcel(ref ThresholdGrid);
			}
			catch {}
		}

		private void ThresholdGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "NetPositionSettled":
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch {}
					break;
			}
		}
  }
}