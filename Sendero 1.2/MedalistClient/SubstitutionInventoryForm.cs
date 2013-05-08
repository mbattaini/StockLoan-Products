using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class SubstitutionInventoryForm : System.Windows.Forms.Form
  {
    private DataSet dataSet;

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
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid SubstitutionInventoryGrid;
		private System.Windows.Forms.Timer RefreshTimer;
		private System.ComponentModel.IContainer components;

    public SubstitutionInventoryForm(MainForm mainForm)
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SubstitutionInventoryForm));
			this.SubstitutionInventoryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
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
			this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.SubstitutionInventoryGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DateEditor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// SubstitutionInventoryGrid
			// 
			this.SubstitutionInventoryGrid.AllowColSelect = false;
			this.SubstitutionInventoryGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.SubstitutionInventoryGrid.AllowUpdate = false;
			this.SubstitutionInventoryGrid.AllowUpdateOnBlur = false;
			this.SubstitutionInventoryGrid.AlternatingRows = true;
			this.SubstitutionInventoryGrid.CaptionHeight = 17;
			this.SubstitutionInventoryGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
			this.SubstitutionInventoryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SubstitutionInventoryGrid.EmptyRows = true;
			this.SubstitutionInventoryGrid.ExtendRightColumn = true;
			this.SubstitutionInventoryGrid.FilterBar = true;
			this.SubstitutionInventoryGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.SubstitutionInventoryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.SubstitutionInventoryGrid.Location = new System.Drawing.Point(1, 32);
			this.SubstitutionInventoryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.SubstitutionInventoryGrid.Name = "SubstitutionInventoryGrid";
			this.SubstitutionInventoryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.SubstitutionInventoryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.SubstitutionInventoryGrid.PreviewInfo.ZoomFactor = 75;
			this.SubstitutionInventoryGrid.RecordSelectorWidth = 16;
			this.SubstitutionInventoryGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.SubstitutionInventoryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.SubstitutionInventoryGrid.RowHeight = 15;
			this.SubstitutionInventoryGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.SubstitutionInventoryGrid.Size = new System.Drawing.Size(934, 381);
			this.SubstitutionInventoryGrid.TabIndex = 0;
			this.SubstitutionInventoryGrid.Text = "Inventory Substitutions";
			this.SubstitutionInventoryGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.InventorySubstitutionsGrid_RowColChange);
			this.SubstitutionInventoryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.SubstitutionInventoryGrid_FormatText);
			this.SubstitutionInventoryGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
				"\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
				"l=\"0\" Caption=\"Potentially Available Quantity\" DataField=\"PotentiallyAvailableQu" +
				"antity\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColum" +
				"n><C1DataColumn Level=\"0\" Caption=\"Psr Quantity\" DataField=\"PsrQuantity\" NumberF" +
				"ormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
				" Level=\"0\" Caption=\"Total Quantity\" DataField=\"TotalQuantity\" NumberFormat=\"Form" +
				"atText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
				"Caption=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn><C" +
				"1DataColumn Level=\"0\" Caption=\"\" DataField=\"\"><ValueItems /><GroupInfo /></C1Dat" +
				"aColumn><C1DataColumn Level=\"0\" Caption=\"Reserved Excess Quantity\" DataField=\"Re" +
				"servedExcessQuantity\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /" +
				"></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapp" +
				"er\"><Data>Style58{}Style59{}Style35{}RecordSelector{AlignImage:Center;}Style50{A" +
				"lignHorz:Near;}Style51{ForegroundImagePos:LeftOfText;AlignHorz:Far;ForeColor:Dar" +
				"kSlateGray;}Style52{}Style53{}Style54{}Caption{AlignHorz:Center;}Style56{AlignHo" +
				"rz:Near;}Normal{Font:Verdana, 8.25pt;}Selected{ForeColor:HighlightText;BackColor" +
				":Highlight;}Editor{}Style31{}Style18{}Style19{}Style14{AlignHorz:Near;}Style15{A" +
				"lignHorz:Near;ForeColor:DarkRed;}Style16{}Style17{}Style10{AlignHorz:Near;}Style" +
				"11{}Style12{}Style13{}Style47{}Style46{}Style61{}Style60{}FilterBar{ForeColor:Wi" +
				"ndowText;BackColor:SeaShell;}Style32{}Style33{}Style4{}OddRow{}Style29{}Style28{" +
				"}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style26{AlignHorz:Nea" +
				"r;}Style25{}Footer{}Style23{}Style22{}Style21{AlignHorz:Near;}Style55{}Group{Ali" +
				"gnVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style57{AlignHorz:F" +
				"ar;ForeColor:DarkSlateGray;}Inactive{ForeColor:InactiveCaptionText;BackColor:Ina" +
				"ctiveCaption;}EvenRow{BackColor:LightCyan;}Style27{AlignHorz:Far;ForeColor:DarkS" +
				"lateGray;}Style49{}Style48{}Style24{}Style8{}Style20{AlignHorz:Near;}Style5{}Sty" +
				"le41{}Style40{}Heading{Wrap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;For" +
				"eColor:ControlText;AlignVert:Center;}Style43{AlignHorz:Far;}Style42{}Style44{Ali" +
				"gnHorz:Near;}Style45{AlignHorz:Near;ForeColor:DarkRed;}Style9{}Style38{AlignHorz" +
				":Near;}Style39{AlignHorz:Far;ForeColor:DarkSlateGray;}Style36{}Style37{}Style34{" +
				"}Style7{}Style6{}Style1{}Style30{}Style3{}Style2{}</Data></Styles><Splits><C1.Wi" +
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
				"me=\"Style14\" /><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3" +
				"\" me=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle p" +
				"arent=\"Style1\" me=\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><" +
				"Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>87</" +
				"Width><Height>15</Height><Locked>True</Locked><DCIdx>0</DCIdx></C1DisplayColumn>" +
				"<C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style44\" /><Style parent=\"Sty" +
				"le1\" me=\"Style45\" /><FooterStyle parent=\"Style3\" me=\"Style46\" /><EditorStyle par" +
				"ent=\"Style5\" me=\"Style47\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style49\" /><Gr" +
				"oupFooterStyle parent=\"Style1\" me=\"Style48\" /><Visible>True</Visible><ColumnDivi" +
				"der>Gainsboro,Single</ColumnDivider><Width>73</Width><Height>15</Height><Locked>" +
				"True</Locked><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pa" +
				"rent=\"Style2\" me=\"Style38\" /><Style parent=\"Style1\" me=\"Style39\" /><FooterStyle " +
				"parent=\"Style3\" me=\"Style40\" /><EditorStyle parent=\"Style5\" me=\"Style41\" /><Grou" +
				"pHeaderStyle parent=\"Style1\" me=\"Style43\" /><GroupFooterStyle parent=\"Style1\" me" +
				"=\"Style42\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivid" +
				"er><Width>120</Width><Height>15</Height><FooterDivider>False</FooterDivider><DCI" +
				"dx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style50\" /><Style parent=\"Style1\" me=\"Style51\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style52\" /><EditorStyle parent=\"Style5\" me=\"Style53\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style55\" /><GroupFooterStyle parent=\"Style1\" me=\"Style54\" /><Vis" +
				"ible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>180</Wi" +
				"dth><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style26\" /><Style parent=\"Style1\" me=\"Style27\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style28\" /><EditorStyle parent=\"Style5\" me=\"Style2" +
				"9\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style31\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style30\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</C" +
				"olumnDivider><Width>120</Width><Height>15</Height><Locked>True</Locked><DCIdx>2<" +
				"/DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Styl" +
				"e56\" /><Style parent=\"Style1\" me=\"Style57\" /><FooterStyle parent=\"Style3\" me=\"St" +
				"yle58\" /><EditorStyle parent=\"Style5\" me=\"Style59\" /><GroupHeaderStyle parent=\"S" +
				"tyle1\" me=\"Style61\" /><GroupFooterStyle parent=\"Style1\" me=\"Style60\" /><Visible>" +
				"True</Visible><ColumnDivider>Gainsboro,None</ColumnDivider><Width>192</Width><He" +
				"ight>15</Height><Locked>True</Locked><HeaderDivider>False</HeaderDivider><DCIdx>" +
				"1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=" +
				"\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Visibl" +
				"e>True</Visible><ColumnDivider>DarkGray,None</ColumnDivider><Height>15</Height><" +
				"HeaderDivider>False</HeaderDivider><FooterDivider>False</FooterDivider><DCIdx>5<" +
				"/DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 930, 377</ClientRect><" +
				"BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><" +
				"Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style paren" +
				"t=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"" +
				"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"N" +
				"ormal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"N" +
				"ormal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Headin" +
				"g\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"" +
				"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</hor" +
				"zSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><Cli" +
				"entArea>0, 0, 930, 377</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style36\" " +
				"/><PrintPageFooterStyle parent=\"\" me=\"Style37\" /></Blob>";
			// 
			// EffectDateLabel
			// 
			this.EffectDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EffectDateLabel.Location = new System.Drawing.Point(740, 8);
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
			this.DateEditor.Location = new System.Drawing.Point(836, 4);
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
			// SubstitutionInventoryForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(936, 433);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.DateEditor);
			this.Controls.Add(this.EffectDateLabel);
			this.Controls.Add(this.SubstitutionInventoryGrid);
			this.DockPadding.Bottom = 20;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 32;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SubstitutionInventoryForm";
			this.Text = "Substitution - Inventory";
			this.Load += new System.EventHandler(this.SubstitutionInventoryForm_Load);
			this.Closed += new System.EventHandler(this.SubstitutionInventoryForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.SubstitutionInventoryGrid)).EndInit();
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
        mainForm.Alert("Please wait... Loading inventory substitutions data...", PilotState.Unknown);
        this.Cursor = Cursors.WaitCursor;
        this.Refresh();
				this.RefreshTimer.Enabled = false;

        dataSet = mainForm.SubstitutionAgent.SubstitutionInventoryDataGet(effectDate);

        SubstitutionInventoryGrid.SetDataBinding(dataSet.Tables["SubstitutionInventory"], null, true);
        StatusSet();    

				this.RefreshTimer.Enabled = true;
        mainForm.Alert("Loading substitution inventory data... Done!", PilotState.Normal);
      }
      catch(Exception e)
      {
        mainForm.Alert(e.Message, PilotState.RunFault);
        Log.Write(e.Message + " [SubstitutionInventoryForm.ListLoad]", Log.Error, 1); 
      }

      this.Cursor = Cursors.Default;
    }

    private void SendToClipboard()
    {
      string gridData = "";

      foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionInventoryGrid.SelectedCols)
      {
        gridData += dataColumn.Caption + "\t";
      }
      gridData += "\n";

      foreach (int row in SubstitutionInventoryGrid.SelectedRows)
      {
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionInventoryGrid.SelectedCols)
        {
          gridData += dataColumn.CellText(row) + "\t";
        }
        gridData += "\n";
      }

      Clipboard.SetDataObject(gridData, true);
      mainForm.Alert("Total: " + SubstitutionInventoryGrid.SelectedRows.Count + " items copied to clipboard.", PilotState.Normal);
    }

    private void StatusSet()
    {
      if (SubstitutionInventoryGrid.SelectedRows.Count > 0)
      {
        StatusLabel.Text = "Selected " + SubstitutionInventoryGrid.SelectedRows.Count.ToString("#,##0") + " items of "
          + dataSet.Tables["SubstitutionInventory"].Rows.Count.ToString("#,##0") + " shown in grid.";
      }
      else
      {
        StatusLabel.Text = "Showing " + dataSet.Tables["SubstitutionInventory"].Rows.Count.ToString("#,##0") + " items in grid.";
      }
    }

    private void SubstitutionInventoryForm_Load(object sender, System.EventArgs e)
    {
      this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
      this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
      this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "500"));
      this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "750"));

			this.RefreshTimer.Enabled = false;

      DateEditor.Text = DateTime.Today.ToString(Standard.DateFormat);
			
			RefreshTimer.Interval = int.Parse(mainForm.ServiceAgent.KeyValueGet("SubstitutionRefreshTimer", "1500"));

      this.Show();
      Application.DoEvents();
      
      ListLoad(DateEditor.Text);
    
			this.RefreshTimer.Enabled = true;
		}

    private void SubstitutionInventoryForm_Closed(object sender, System.EventArgs e)
    {
      if(this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
        RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
        RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
        RegistryValue.Write(this.Name, "Width", this.Width.ToString());        
      }
    }

    private void InventorySubstitutionsGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
    {
      if(!e.LastRow.Equals(SubstitutionInventoryGrid.Row))
      {
        this.Cursor = Cursors.WaitCursor;  
        this.Refresh();
        
        mainForm.SecId = SubstitutionInventoryGrid.Columns["SecId"].Text;
        
        this.Cursor = Cursors.Default;
      }
    }

    private void InventorySubstitutionsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      switch (e.KeyChar)
      {
        case (char)3 :
          if (SubstitutionInventoryGrid.SelectedRows.Count > 0)
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
          if (!SubstitutionInventoryGrid.EditActive && SubstitutionInventoryGrid.DataChanged)
          {
            SubstitutionInventoryGrid.DataChanged = false;
          }
          break;
      }
    }

    private void SubstitutionInventoryGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
    {
      StatusSet();
    }

    private void SubstitutionInventoryGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.X <= SubstitutionInventoryGrid.RecordSelectorWidth && e.Y <= SubstitutionInventoryGrid.RowHeight)
      {
        if (SubstitutionInventoryGrid.SelectedRows.Count.Equals(0))
        {
          for (int i = 0; i < SubstitutionInventoryGrid.Splits[0,0].Rows.Count; i++)
          {
            SubstitutionInventoryGrid.SelectedRows.Add(i);
          }

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionInventoryGrid.Columns)
          {
            SubstitutionInventoryGrid.SelectedCols.Add(dataColumn);
          }
        }
        else
        {
          SubstitutionInventoryGrid.SelectedRows.Clear();
          SubstitutionInventoryGrid.SelectedCols.Clear();
        }
      }    
    }

    private void SubstitutionInventoryGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
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
        SubstitutionInventoryGrid.Focus();
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

      if (SubstitutionInventoryGrid.SelectedCols.Count.Equals(0))
      {
        mainForm.Alert("You have not selected any rows to copy.", PilotState.RunFault);
        return;
      }

      try
      {
        maxTextLength = new int[SubstitutionInventoryGrid.SelectedCols.Count];

        // Get the caption length for each column.
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionInventoryGrid.SelectedCols)
        {
          maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
        }

        // Get the maximum item length for each row in each column.
        foreach (int rowIndex in SubstitutionInventoryGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionInventoryGrid.SelectedCols)
          {
            if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
            {
              maxTextLength[columnIndex] = textLength;
            }
          }
        }

        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionInventoryGrid.SelectedCols)
        {
          gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
        }
        gridData += "\n";
        
        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionInventoryGrid.SelectedCols)
        {
          gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
        }
        gridData += "\n";
        
        foreach (int rowIndex in SubstitutionInventoryGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionInventoryGrid.SelectedCols)
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

        mainForm.Alert("Total: " + SubstitutionInventoryGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
      }
      catch (Exception error)
      {       
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [SubstitutionInventoryForm.SendToEmailMenuItem_Click]", Log.Error, 1); 
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
				excel.ExportGridToExcel(ref SubstitutionInventoryGrid);
			}
			catch {}
		}

		private void SubstitutionInventoryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "PsrQuantity":
				case "PotentiallyAvailableQuantity":
				case "ReservedExcessQuantity":
				case "TotalQuantity":
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