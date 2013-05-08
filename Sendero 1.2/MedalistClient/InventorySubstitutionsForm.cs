using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class InventorySubstitutionsForm : System.Windows.Forms.Form
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
  	private C1.Win.C1TrueDBGrid.C1TrueDBGrid InventorySubstitutionsGrid;

    private System.ComponentModel.Container components = null;

    public InventorySubstitutionsForm(MainForm mainForm)
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InventorySubstitutionsForm));
		this.InventorySubstitutionsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
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
		((System.ComponentModel.ISupportInitialize)(this.InventorySubstitutionsGrid)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.DateEditor)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
		this.SuspendLayout();
		// 
		// InventorySubstitutionsGrid
		// 
		this.InventorySubstitutionsGrid.AllowColSelect = false;
		this.InventorySubstitutionsGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
		this.InventorySubstitutionsGrid.AllowUpdate = false;
		this.InventorySubstitutionsGrid.AllowUpdateOnBlur = false;
		this.InventorySubstitutionsGrid.AlternatingRows = true;
		this.InventorySubstitutionsGrid.CaptionHeight = 17;
		this.InventorySubstitutionsGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
		this.InventorySubstitutionsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.InventorySubstitutionsGrid.EmptyRows = true;
		this.InventorySubstitutionsGrid.ExtendRightColumn = true;
		this.InventorySubstitutionsGrid.FilterBar = true;
		this.InventorySubstitutionsGrid.GroupByCaption = "Drag a column header here to group by that column";
		this.InventorySubstitutionsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
		this.InventorySubstitutionsGrid.Location = new System.Drawing.Point(1, 32);
		this.InventorySubstitutionsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
		this.InventorySubstitutionsGrid.Name = "InventorySubstitutionsGrid";
		this.InventorySubstitutionsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
		this.InventorySubstitutionsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
		this.InventorySubstitutionsGrid.PreviewInfo.ZoomFactor = 75;
		this.InventorySubstitutionsGrid.RecordSelectorWidth = 17;
		this.InventorySubstitutionsGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
		this.InventorySubstitutionsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
		this.InventorySubstitutionsGrid.RowHeight = 15;
		this.InventorySubstitutionsGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
		this.InventorySubstitutionsGrid.Size = new System.Drawing.Size(726, 381);
		this.InventorySubstitutionsGrid.TabIndex = 0;
		this.InventorySubstitutionsGrid.Text = "Inventory Substitutions";
		this.InventorySubstitutionsGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.InventorySubstitutionsGrid_RowColChange);
		this.InventorySubstitutionsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.InventorySubstitutionsGrid_FormatText);
		this.InventorySubstitutionsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
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
			"er\"><Data>Style58{}Style59{}RecordSelector{AlignImage:Center;}Style50{AlignHorz:" +
			"Center;}Style51{ForegroundImagePos:LeftOfText;AlignHorz:Far;ForeColor:DarkSlateG" +
			"ray;}Style52{}Style53{}Style54{}Caption{AlignHorz:Center;}Style56{AlignHorz:Cent" +
			"er;}Normal{Font:Verdana, 8.25pt;}Selected{ForeColor:HighlightText;BackColor:High" +
			"light;}Editor{}Style31{}Style18{}Style19{}Style27{AlignHorz:Far;ForeColor:DarkSl" +
			"ateGray;}Style14{AlignHorz:Near;}Style15{AlignHorz:Near;ForeColor:DarkRed;}Style" +
			"16{}Style17{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style47{}Style61" +
			"{}Style60{}Style36{}Style35{}Style32{}Style33{}Style4{}OddRow{}Style29{}Style28{" +
			"}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style26{AlignHorz:Far" +
			";}Style25{}Footer{}Style23{}Style22{}Style21{AlignHorz:Near;}Style55{}Group{Alig" +
			"nVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style57{AlignHorz:Fa" +
			"r;ForeColor:DarkSlateGray;}Inactive{ForeColor:InactiveCaptionText;BackColor:Inac" +
			"tiveCaption;}EvenRow{BackColor:LightCyan;}Heading{Wrap:True;BackColor:Control;Bo" +
			"rder:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style49{}Style48" +
			"{}Style24{}Style8{}Style20{AlignHorz:Near;}Style5{}Style41{}Style9{}Style40{}Sty" +
			"le43{AlignHorz:Far;}Style45{AlignHorz:Near;ForeColor:DarkRed;}Style42{}Style44{A" +
			"lignHorz:Near;}Style46{}Style38{AlignHorz:Center;}Style39{AlignHorz:Far;ForeColo" +
			"r:DarkSlateGray;}FilterBar{ForeColor:WindowText;BackColor:SeaShell;}Style37{}Sty" +
			"le34{}Style7{}Style6{}Style1{}Style30{}Style3{}Style2{}</Data></Styles><Splits><" +
			"C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSelect" +
			"=\"False\" Name=\"\" AllowRowSizing=\"None\" AlternatingRowStyle=\"True\" CaptionHeight=" +
			"\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" F" +
			"ilterBar=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"17\" DefRecSe" +
			"lWidth=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle pare" +
			"nt=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowSt" +
			"yle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style1" +
			"3\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"S" +
			"tyle12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent" +
			"=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><Od" +
			"dRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelec" +
			"tor\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent" +
			"=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Sty" +
			"le2\" me=\"Style14\" /><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"S" +
			"tyle3\" me=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderSt" +
			"yle parent=\"Style1\" me=\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18" +
			"\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width" +
			">87</Width><Height>15</Height><Locked>True</Locked><DCIdx>0</DCIdx></C1DisplayCo" +
			"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style44\" /><Style parent" +
			"=\"Style1\" me=\"Style45\" /><FooterStyle parent=\"Style3\" me=\"Style46\" /><EditorStyl" +
			"e parent=\"Style5\" me=\"Style47\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style49\" " +
			"/><GroupFooterStyle parent=\"Style1\" me=\"Style48\" /><Visible>True</Visible><Colum" +
			"nDivider>LightGray,Single</ColumnDivider><Width>73</Width><Height>15</Height><Lo" +
			"cked>True</Locked><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingSty" +
			"le parent=\"Style2\" me=\"Style26\" /><Style parent=\"Style1\" me=\"Style27\" /><FooterS" +
			"tyle parent=\"Style3\" me=\"Style28\" /><EditorStyle parent=\"Style5\" me=\"Style29\" />" +
			"<GroupHeaderStyle parent=\"Style1\" me=\"Style31\" /><GroupFooterStyle parent=\"Style" +
			"1\" me=\"Style30\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</Column" +
			"Divider><Width>83</Width><Height>15</Height><Locked>True</Locked><DCIdx>2</DCIdx" +
			"></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style56\" /" +
			"><Style parent=\"Style1\" me=\"Style57\" /><FooterStyle parent=\"Style3\" me=\"Style58\"" +
			" /><EditorStyle parent=\"Style5\" me=\"Style59\" /><GroupHeaderStyle parent=\"Style1\"" +
			" me=\"Style61\" /><GroupFooterStyle parent=\"Style1\" me=\"Style60\" /><Visible>True</" +
			"Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>173</Width><Height" +
			">15</Height><Locked>True</Locked><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayCol" +
			"umn><HeadingStyle parent=\"Style2\" me=\"Style50\" /><Style parent=\"Style1\" me=\"Styl" +
			"e51\" /><FooterStyle parent=\"Style3\" me=\"Style52\" /><EditorStyle parent=\"Style5\" " +
			"me=\"Style53\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style55\" /><GroupFooterStyl" +
			"e parent=\"Style1\" me=\"Style54\" /><Visible>True</Visible><ColumnDivider>DarkGray," +
			"Single</ColumnDivider><Width>157</Width><Height>15</Height><DCIdx>6</DCIdx></C1D" +
			"isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style38\" /><Styl" +
			"e parent=\"Style1\" me=\"Style39\" /><FooterStyle parent=\"Style3\" me=\"Style40\" /><Ed" +
			"itorStyle parent=\"Style5\" me=\"Style41\" /><GroupHeaderStyle parent=\"Style1\" me=\"S" +
			"tyle43\" /><GroupFooterStyle parent=\"Style1\" me=\"Style42\" /><Visible>True</Visibl" +
			"e><ColumnDivider>LightGray,None</ColumnDivider><Width>91</Width><Height>15</Heig" +
			"ht><HeaderDivider>False</HeaderDivider><FooterDivider>False</FooterDivider><DCId" +
			"x>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"" +
			"Style20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me" +
			"=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle paren" +
			"t=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Visi" +
			"ble>True</Visible><ColumnDivider>DarkGray,None</ColumnDivider><Height>15</Height" +
			"><HeaderDivider>False</HeaderDivider><FooterDivider>False</FooterDivider><DCIdx>" +
			"5</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 722, 377</ClientRect" +
			"><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles" +
			"><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style par" +
			"ent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent" +
			"=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=" +
			"\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=" +
			"\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Head" +
			"ing\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent" +
			"=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</h" +
			"orzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><C" +
			"lientArea>0, 0, 722, 377</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style36" +
			"\" /><PrintPageFooterStyle parent=\"\" me=\"Style37\" /></Blob>";
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
		this.DateEditor.DropDownClosed += new System.EventHandler(this.DateEditor_Validated);
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
		// InventorySubstitutionsForm
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
		this.ClientSize = new System.Drawing.Size(728, 433);
		this.ContextMenu = this.MainContextMenu;
		this.Controls.Add(this.StatusLabel);
		this.Controls.Add(this.DateEditor);
		this.Controls.Add(this.EffectDateLabel);
		this.Controls.Add(this.InventorySubstitutionsGrid);
		this.DockPadding.Bottom = 20;
		this.DockPadding.Left = 1;
		this.DockPadding.Right = 1;
		this.DockPadding.Top = 32;
		this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.Name = "InventorySubstitutionsForm";
		this.Text = "Inventory Substitutions";
		this.Load += new System.EventHandler(this.InventorySubstitutionsForm_Load);
		((System.ComponentModel.ISupportInitialize)(this.InventorySubstitutionsGrid)).EndInit();
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

        dataSet = mainForm.PositionAgent.InventorySubstitutionsDataGet(effectDate);

        InventorySubstitutionsGrid.SetDataBinding(dataSet.Tables["InventorySubstitutions"], null, true);
        StatusSet();    

        mainForm.Alert("Loading inventory substitutions data... Done!", PilotState.Normal);
      }
      catch(Exception e)
      {
        mainForm.Alert(e.Message, PilotState.RunFault);
        Log.Write(e.Message + " [InventorySubstitutionsForm.ListLoad]", Log.Error, 1); 
      }

      this.Cursor = Cursors.Default;
    }

    private void SendToClipboard()
    {
      string gridData = "";

      foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventorySubstitutionsGrid.SelectedCols)
      {
        gridData += dataColumn.Caption + "\t";
      }
      gridData += "\n";

      foreach (int row in InventorySubstitutionsGrid.SelectedRows)
      {
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventorySubstitutionsGrid.SelectedCols)
        {
          gridData += dataColumn.CellText(row) + "\t";
        }
        gridData += "\n";
      }

      Clipboard.SetDataObject(gridData, true);
      mainForm.Alert("Total: " + InventorySubstitutionsGrid.SelectedRows.Count + " items copied to clipboard.", PilotState.Normal);
    }

    private void StatusSet()
    {
      if (InventorySubstitutionsGrid.SelectedRows.Count > 0)
      {
        StatusLabel.Text = "Selected " + InventorySubstitutionsGrid.SelectedRows.Count.ToString("#,##0") + " items of "
          + dataSet.Tables["InventorySubstitutions"].Rows.Count.ToString("#,##0") + " shown in grid.";
      }
      else
      {
        StatusLabel.Text = "Showing " + dataSet.Tables["InventorySubstitutions"].Rows.Count.ToString("#,##0") + " items in grid.";
      }
    }

    private void InventorySubstitutionsForm_Load(object sender, System.EventArgs e)
    {
      this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
      this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
      this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "500"));
      this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "750"));

      DateEditor.Text = DateTime.Today.ToString(Standard.DateFormat);

      this.Show();
      Application.DoEvents();
      
      ListLoad(DateEditor.Text);
    }

    private void InventorySubstitutionsForm_Closed(object sender, System.EventArgs e)
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
      if(!e.LastRow.Equals(InventorySubstitutionsGrid.Row))
      {
        this.Cursor = Cursors.WaitCursor;  
        this.Refresh();
        
        mainForm.SecId = InventorySubstitutionsGrid.Columns["SecId"].Text;
        
        this.Cursor = Cursors.Default;
      }
    }

    private void InventorySubstitutionsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      switch (e.KeyChar)
      {
        case (char)3 :
          if (InventorySubstitutionsGrid.SelectedRows.Count > 0)
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
          if (!InventorySubstitutionsGrid.EditActive && InventorySubstitutionsGrid.DataChanged)
          {
            InventorySubstitutionsGrid.DataChanged = false;
          }
          break;
      }
    }

    private void InventorySubstitutionsGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
    {
      StatusSet();
    }

    private void InventorySubstitutionsGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.X <= InventorySubstitutionsGrid.RecordSelectorWidth && e.Y <= InventorySubstitutionsGrid.RowHeight)
      {
        if (InventorySubstitutionsGrid.SelectedRows.Count.Equals(0))
        {
          for (int i = 0; i < InventorySubstitutionsGrid.Splits[0,0].Rows.Count; i++)
          {
            InventorySubstitutionsGrid.SelectedRows.Add(i);
          }

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventorySubstitutionsGrid.Columns)
          {
            InventorySubstitutionsGrid.SelectedCols.Add(dataColumn);
          }
        }
        else
        {
          InventorySubstitutionsGrid.SelectedRows.Clear();
          InventorySubstitutionsGrid.SelectedCols.Clear();
        }
      }    
    }

    private void InventorySubstitutionsGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
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
        InventorySubstitutionsGrid.Focus();
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

      if (InventorySubstitutionsGrid.SelectedCols.Count.Equals(0))
      {
        mainForm.Alert("You have not selected any rows to copy.", PilotState.RunFault);
        return;
      }

      try
      {
        maxTextLength = new int[InventorySubstitutionsGrid.SelectedCols.Count];

        // Get the caption length for each column.
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventorySubstitutionsGrid.SelectedCols)
        {
          maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
        }

        // Get the maximum item length for each row in each column.
        foreach (int rowIndex in InventorySubstitutionsGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventorySubstitutionsGrid.SelectedCols)
          {
            if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
            {
              maxTextLength[columnIndex] = textLength;
            }
          }
        }

        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventorySubstitutionsGrid.SelectedCols)
        {
          gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
        }
        gridData += "\n";
        
        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventorySubstitutionsGrid.SelectedCols)
        {
          gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
        }
        gridData += "\n";
        
        foreach (int rowIndex in InventorySubstitutionsGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in InventorySubstitutionsGrid.SelectedCols)
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

        mainForm.Alert("Total: " + InventorySubstitutionsGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
      }
      catch (Exception error)
      {       
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [InventorySubstitutionsForm.SendToEmailMenuItem_Click]", Log.Error, 1); 
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
				excel.ExportGridToExcel(ref InventorySubstitutionsGrid);
			}
			catch {}
		}

		private void InventorySubstitutionsGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
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