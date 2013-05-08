using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class ExternalInventoryForm : System.Windows.Forms.Form
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
  	private C1.Win.C1TrueDBGrid.C1TrueDBGrid ExternalInventoryGrid;

    private System.ComponentModel.Container components = null;

    public ExternalInventoryForm(MainForm mainForm)
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExternalInventoryForm));
		this.ExternalInventoryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
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
		((System.ComponentModel.ISupportInitialize)(this.ExternalInventoryGrid)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.DateEditor)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
		this.SuspendLayout();
		// 
		// ExternalInventoryGrid
		// 
		this.ExternalInventoryGrid.AllowColSelect = false;
		this.ExternalInventoryGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
		this.ExternalInventoryGrid.AllowUpdate = false;
		this.ExternalInventoryGrid.AllowUpdateOnBlur = false;
		this.ExternalInventoryGrid.AlternatingRows = true;
		this.ExternalInventoryGrid.CaptionHeight = 17;
		this.ExternalInventoryGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
		this.ExternalInventoryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ExternalInventoryGrid.EmptyRows = true;
		this.ExternalInventoryGrid.ExtendRightColumn = true;
		this.ExternalInventoryGrid.FilterBar = true;
		this.ExternalInventoryGrid.GroupByCaption = "Drag a column header here to group by that column";
		this.ExternalInventoryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
		this.ExternalInventoryGrid.Location = new System.Drawing.Point(1, 32);
		this.ExternalInventoryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
		this.ExternalInventoryGrid.Name = "ExternalInventoryGrid";
		this.ExternalInventoryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
		this.ExternalInventoryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
		this.ExternalInventoryGrid.PreviewInfo.ZoomFactor = 75;
		this.ExternalInventoryGrid.RecordSelectorWidth = 17;
		this.ExternalInventoryGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
		this.ExternalInventoryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
		this.ExternalInventoryGrid.RowHeight = 15;
		this.ExternalInventoryGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
		this.ExternalInventoryGrid.Size = new System.Drawing.Size(726, 381);
		this.ExternalInventoryGrid.TabIndex = 0;
		this.ExternalInventoryGrid.Text = "External Inventory";
		this.ExternalInventoryGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.ExternalInventoryGrid_RowColChange);
		this.ExternalInventoryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ExternalInventoryGrid_FormatText);
		this.ExternalInventoryGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
			"\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
			"l=\"0\" Caption=\"Potentially Available Quantity\" DataField=\"PotentiallyAvailableQu" +
			"antity\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColum" +
			"n><C1DataColumn Level=\"0\" Caption=\"Psr Quantity\" DataField=\"PsrQuantity\" NumberF" +
			"ormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
			" Level=\"0\" Caption=\"Reserved Excess Quantity\" DataField=\"ReservedExcessQuantity\"" +
			" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1Da" +
			"taColumn Level=\"0\" Caption=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo " +
			"/></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrap" +
			"per\"><Data>Style58{}Style59{}Caption{AlignHorz:Center;}Style27{AlignHorz:Far;}No" +
			"rmal{Font:Verdana, 8.25pt;}Selected{ForeColor:HighlightText;BackColor:Highlight;" +
			"}Editor{}Style18{}Style19{}Style14{AlignHorz:Near;}Style15{AlignHorz:Near;ForeCo" +
			"lor:DarkRed;}Style16{}Style17{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13" +
			"{}Style42{}Style46{}Style61{}Style60{}Style38{AlignHorz:Far;}Group{BackColor:Con" +
			"trolDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style37{}OddRow{}Style29{}Sty" +
			"le28{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style26{AlignHor" +
			"z:Far;}RecordSelector{AlignImage:Center;}Footer{}Style56{AlignHorz:Far;}Style57{" +
			"AlignHorz:Far;ForeColor:DarkSlateGray;}Inactive{ForeColor:InactiveCaptionText;Ba" +
			"ckColor:InactiveCaption;}EvenRow{BackColor:LightCyan;}Heading{Wrap:True;AlignVer" +
			"t:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Styl" +
			"e49{}Style48{}Style4{}Style7{}Style6{}Style1{}Style3{}Style41{}Style40{}Style43{" +
			"AlignHorz:Far;}FilterBar{ForeColor:WindowText;BackColor:SeaShell;}Style45{AlignH" +
			"orz:Near;ForeColor:DarkRed;}Style44{AlignHorz:Near;}Style47{}Style9{}Style8{}Sty" +
			"le39{AlignHorz:Far;}Style36{}Style5{}Style34{}Style35{}Style32{}Style33{}Style30" +
			"{}Style31{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarSt" +
			"yle=\"None\" VBarStyle=\"Always\" AllowColSelect=\"False\" Name=\"\" AllowRowSizing=\"Non" +
			"e\" AlternatingRowStyle=\"True\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" Column" +
			"FooterHeight=\"17\" ExtendRightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=\"Dotted" +
			"RowBorder\" RecordSelectorWidth=\"17\" DefRecSelWidth=\"17\" VerticalScrollGroup=\"1\" " +
			"HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorSt" +
			"yle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><" +
			"FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me" +
			"=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Head" +
			"ing\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><Inact" +
			"iveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9" +
			"\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle p" +
			"arent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCol" +
			"s><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent=\"S" +
			"tyle1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle p" +
			"arent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><" +
			"GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><ColumnDi" +
			"vider>LightGray,Single</ColumnDivider><Width>95</Width><Height>15</Height><Locke" +
			"d>True</Locked><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle " +
			"parent=\"Style2\" me=\"Style44\" /><Style parent=\"Style1\" me=\"Style45\" /><FooterStyl" +
			"e parent=\"Style3\" me=\"Style46\" /><EditorStyle parent=\"Style5\" me=\"Style47\" /><Gr" +
			"oupHeaderStyle parent=\"Style1\" me=\"Style49\" /><GroupFooterStyle parent=\"Style1\" " +
			"me=\"Style48\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDiv" +
			"ider><Height>15</Height><Locked>True</Locked><DCIdx>4</DCIdx></C1DisplayColumn><" +
			"C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style26\" /><Style parent=\"Styl" +
			"e1\" me=\"Style27\" /><FooterStyle parent=\"Style3\" me=\"Style28\" /><EditorStyle pare" +
			"nt=\"Style5\" me=\"Style29\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style31\" /><Gro" +
			"upFooterStyle parent=\"Style1\" me=\"Style30\" /><Visible>True</Visible><ColumnDivid" +
			"er>LightGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCId" +
			"x>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"" +
			"Style56\" /><Style parent=\"Style1\" me=\"Style57\" /><FooterStyle parent=\"Style3\" me" +
			"=\"Style58\" /><EditorStyle parent=\"Style5\" me=\"Style59\" /><GroupHeaderStyle paren" +
			"t=\"Style1\" me=\"Style61\" /><GroupFooterStyle parent=\"Style1\" me=\"Style60\" /><Visi" +
			"ble>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>200</Wid" +
			"th><Height>15</Height><Locked>True</Locked><DCIdx>1</DCIdx></C1DisplayColumn><C1" +
			"DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style38\" /><Style parent=\"Style1" +
			"\" me=\"Style39\" /><FooterStyle parent=\"Style3\" me=\"Style40\" /><EditorStyle parent" +
			"=\"Style5\" me=\"Style41\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style43\" /><Group" +
			"FooterStyle parent=\"Style1\" me=\"Style42\" /><Visible>True</Visible><ColumnDivider" +
			">LightGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx>" +
			"3</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 722, 377</ClientRect" +
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
		// ExternalInventoryForm
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
		this.ClientSize = new System.Drawing.Size(728, 433);
		this.ContextMenu = this.MainContextMenu;
		this.Controls.Add(this.StatusLabel);
		this.Controls.Add(this.DateEditor);
		this.Controls.Add(this.EffectDateLabel);
		this.Controls.Add(this.ExternalInventoryGrid);
		this.DockPadding.Bottom = 20;
		this.DockPadding.Left = 1;
		this.DockPadding.Right = 1;
		this.DockPadding.Top = 32;
		this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.Name = "ExternalInventoryForm";
		this.Text = "External Inventory";
		this.Load += new System.EventHandler(this.ExternalInventoryForm_Load);
		((System.ComponentModel.ISupportInitialize)(this.ExternalInventoryGrid)).EndInit();
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
        mainForm.Alert("Please wait... Loading external inventory data...", PilotState.Unknown);
        this.Cursor = Cursors.WaitCursor;
        this.Refresh();

        dataSet = mainForm.PositionAgent.ExternalInventoryDataGet(effectDate);

        ExternalInventoryGrid.SetDataBinding(dataSet.Tables["ExternalInventory"], null, true);
        StatusSet();    

        mainForm.Alert("Loading external inventory data... Done!", PilotState.Normal);
      }
      catch(Exception e)
      {
        mainForm.Alert(e.Message, PilotState.RunFault);
        Log.Write(e.Message + " [ExternalInventoryForm.ListLoad]", Log.Error, 1); 
      }

      this.Cursor = Cursors.Default;
    }

    private void SendToClipboard()
    {
      string gridData = "";

      foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ExternalInventoryGrid.SelectedCols)
      {
        gridData += dataColumn.Caption + "\t";
      }
      gridData += "\n";

      foreach (int row in ExternalInventoryGrid.SelectedRows)
      {
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ExternalInventoryGrid.SelectedCols)
        {
          gridData += dataColumn.CellText(row) + "\t";
        }
        gridData += "\n";
      }

      Clipboard.SetDataObject(gridData, true);
      mainForm.Alert("Total: " + ExternalInventoryGrid.SelectedRows.Count + " items copied to clipboard.", PilotState.Normal);
    }

    private void StatusSet()
    {
      if (ExternalInventoryGrid.SelectedRows.Count > 0)
      {
        StatusLabel.Text = "Selected " + ExternalInventoryGrid.SelectedRows.Count.ToString("#,##0") + " items of "
          + dataSet.Tables["ExternalInventory"].Rows.Count.ToString("#,##0") + " shown in grid.";
      }
      else
      {
        StatusLabel.Text = "Showing " + dataSet.Tables["ExternalInventory"].Rows.Count.ToString("#,##0") + " items in grid.";
      }
    }

    private void ExternalInventoryForm_Load(object sender, System.EventArgs e)
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

    private void ExternalInventoryForm_Closed(object sender, System.EventArgs e)
    {
      if(this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
        RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
        RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
        RegistryValue.Write(this.Name, "Width", this.Width.ToString());        
      }
    }

    private void ExternalInventoryGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
    {
      if(!e.LastRow.Equals(ExternalInventoryGrid.Row))
      {
        this.Cursor = Cursors.WaitCursor;  
        this.Refresh();
        
        mainForm.SecId = ExternalInventoryGrid.Columns["SecId"].Text;
        
        this.Cursor = Cursors.Default;
      }
    }

    private void ExternalInventoryGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      switch (e.KeyChar)
      {
        case (char)3 :
          if (ExternalInventoryGrid.SelectedRows.Count > 0)
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
          if (!ExternalInventoryGrid.EditActive && ExternalInventoryGrid.DataChanged)
          {
            ExternalInventoryGrid.DataChanged = false;
          }
          break;
      }
    }

    private void ExternalInventoryGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
    {
      StatusSet();
    }

    private void ExternalInventoryGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.X <= ExternalInventoryGrid.RecordSelectorWidth && e.Y <= ExternalInventoryGrid.RowHeight)
      {
        if (ExternalInventoryGrid.SelectedRows.Count.Equals(0))
        {
          for (int i = 0; i < ExternalInventoryGrid.Splits[0,0].Rows.Count; i++)
          {
            ExternalInventoryGrid.SelectedRows.Add(i);
          }

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ExternalInventoryGrid.Columns)
          {
            ExternalInventoryGrid.SelectedCols.Add(dataColumn);
          }
        }
        else
        {
          ExternalInventoryGrid.SelectedRows.Clear();
          ExternalInventoryGrid.SelectedCols.Clear();
        }
      }    
    }

    private void ExternalInventoryGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
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
        ExternalInventoryGrid.Focus();
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

      if (ExternalInventoryGrid.SelectedCols.Count.Equals(0))
      {
        mainForm.Alert("You have not selected any rows to copy.", PilotState.RunFault);
        return;
      }

      try
      {
        maxTextLength = new int[ExternalInventoryGrid.SelectedCols.Count];

        // Get the caption length for each column.
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ExternalInventoryGrid.SelectedCols)
        {
          maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
        }

        // Get the maximum item length for each row in each column.
        foreach (int rowIndex in ExternalInventoryGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ExternalInventoryGrid.SelectedCols)
          {
            if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
            {
              maxTextLength[columnIndex] = textLength;
            }
          }
        }

        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ExternalInventoryGrid.SelectedCols)
        {
          gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
        }
        gridData += "\n";
        
        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ExternalInventoryGrid.SelectedCols)
        {
          gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
        }
        gridData += "\n";
        
        foreach (int rowIndex in ExternalInventoryGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ExternalInventoryGrid.SelectedCols)
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

        mainForm.Alert("Total: " + ExternalInventoryGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
      }
      catch (Exception error)
      {       
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [ExternalInventoryForm.SendToEmailMenuItem_Click]", Log.Error, 1); 
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
				excel.ExportGridToExcel(ref ExternalInventoryGrid);
			}
			catch {}
		}

		private void ExternalInventoryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "PsrQuantity":
				case "PotentiallyAvailableQuantity":
				case "ReservedExcessQuantity":
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