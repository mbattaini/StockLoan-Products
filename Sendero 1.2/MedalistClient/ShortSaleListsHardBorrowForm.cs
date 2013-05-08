using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class ShortSaleListsHardBorrowForm : System.Windows.Forms.Form
  {
    private const int xSecId = 0;
    
    private DataSet dataSet;
    private DataView dataView;

    private MainForm mainForm;    

    private System.Windows.Forms.CheckBox ShowHistoryCheck;
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid HardBorrowGrid;
    private System.Windows.Forms.TextBox DescriptionTextBox;

    private System.Windows.Forms.ContextMenu MainContextMenu;
    private System.Windows.Forms.MenuItem SendToMenuItem;
    private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
    private System.Windows.Forms.MenuItem SendToEmailMenuItem;
    private System.Windows.Forms.MenuItem Sep1MenuItem;
    private System.Windows.Forms.MenuItem ExitMenuItem;

    private C1.Win.C1Input.C1Label StatusLabel;
    
    private System.ComponentModel.Container components = null;
    
    public ShortSaleListsHardBorrowForm(MainForm mainForm)
    {
      this.mainForm = mainForm;
      
      InitializeComponent();
    
      try
      {
        bool mayEdit = mainForm.AdminAgent.MayEdit(mainForm.UserId, "ShortSaleLists");
        
        HardBorrowGrid.AllowUpdate = mayEdit;
        HardBorrowGrid.AllowAddNew = mayEdit;
        HardBorrowGrid.AllowDelete = mayEdit;
      }
      catch(Exception e)
      {
        mainForm.Alert(e.Message, PilotState.RunFault);
        Log.Write(e.Message + " [ShortSaleListsHardBorrowForm.ShortSaleListsHardBorrowForm]", Log.Error, 1); 
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleListsHardBorrowForm));
      this.HardBorrowGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
      this.DescriptionTextBox = new System.Windows.Forms.TextBox();
      this.ShowHistoryCheck = new System.Windows.Forms.CheckBox();
      this.MainContextMenu = new System.Windows.Forms.ContextMenu();
      this.SendToMenuItem = new System.Windows.Forms.MenuItem();
      this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
      this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
      this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
      this.ExitMenuItem = new System.Windows.Forms.MenuItem();
      this.StatusLabel = new C1.Win.C1Input.C1Label();
      ((System.ComponentModel.ISupportInitialize)(this.HardBorrowGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
      this.SuspendLayout();
      // 
      // HardBorrowGrid
      // 
      this.HardBorrowGrid.AllowColSelect = false;
      this.HardBorrowGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
      this.HardBorrowGrid.AllowUpdate = false;
      this.HardBorrowGrid.AllowUpdateOnBlur = false;
      this.HardBorrowGrid.AlternatingRows = true;
      this.HardBorrowGrid.CaptionHeight = 17;
      this.HardBorrowGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
      this.HardBorrowGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.HardBorrowGrid.EmptyRows = true;
      this.HardBorrowGrid.ExtendRightColumn = true;
      this.HardBorrowGrid.FilterBar = true;
      this.HardBorrowGrid.ForeColor = System.Drawing.SystemColors.ControlText;
      this.HardBorrowGrid.GroupByCaption = "Drag a column header here to group by that column";
      this.HardBorrowGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
      this.HardBorrowGrid.Location = new System.Drawing.Point(1, 26);
      this.HardBorrowGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
      this.HardBorrowGrid.Name = "HardBorrowGrid";
      this.HardBorrowGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
      this.HardBorrowGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
      this.HardBorrowGrid.PreviewInfo.ZoomFactor = 75;
      this.HardBorrowGrid.RecordSelectorWidth = 16;
      this.HardBorrowGrid.RowDivider.Color = System.Drawing.Color.WhiteSmoke;
      this.HardBorrowGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
      this.HardBorrowGrid.RowHeight = 15;
      this.HardBorrowGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
      this.HardBorrowGrid.Size = new System.Drawing.Size(630, 303);
      this.HardBorrowGrid.TabIndex = 0;
      this.HardBorrowGrid.Text = "HardBorrow";
      this.HardBorrowGrid.AfterFilter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.HardBorrowGrid_AfterFilter);
      this.HardBorrowGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.HardBorrowGrid_RowColChange);
      this.HardBorrowGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.HardBorrowGrid_BeforeColEdit);
      this.HardBorrowGrid.SelChange += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.HardBorrowGrid_SelChange);
      this.HardBorrowGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HardBorrowGrid_MouseDown);
      this.HardBorrowGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.HardBorrowGrid_BeforeUpdate);
      this.HardBorrowGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.HardBorrowGrid_BeforeDelete);
      this.HardBorrowGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.HardBorrowGrid_FormatText);
      this.HardBorrowGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HardBorrowGrid_KeyPress);
      this.HardBorrowGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
        "\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
        "l=\"0\" Caption=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataCol" +
        "umn><C1DataColumn Level=\"0\" Caption=\"Start Time\" DataField=\"StartTime\" NumberFor" +
        "mat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
        "evel=\"0\" Caption=\"End Time\" DataField=\"EndTime\" NumberFormat=\"FormatText Event\">" +
        "<ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Actor" +
        "\" DataField=\"ActUserShortName\"><ValueItems /><GroupInfo /></C1DataColumn></DataC" +
        "ols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{" +
        "ForeColor:HighlightText;BackColor:Highlight;}Caption{AlignHorz:Center;}Normal{Fo" +
        "nt:Verdana, 8.25pt;}Style25{}Selected{ForeColor:HighlightText;BackColor:Highligh" +
        "t;}Editor{}Style18{}Style19{}Style14{AlignHorz:Near;}Style15{AlignHorz:Near;Fore" +
        "Color:Maroon;}Style16{}Style17{}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13" +
        "{}Style47{}FilterBar{Font:Verdana, 8.25pt;BackColor:SeaShell;}Style12{}Style7{}S" +
        "tyle27{}Style26{}RecordSelector{AlignImage:Center;}Footer{}Style23{}Style22{}Sty" +
        "le21{AlignHorz:Near;ForeColor:Maroon;}Style20{AlignHorz:Near;}Group{BackColor:Co" +
        "ntrolDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Inactive{ForeColor:InactiveC" +
        "aptionText;BackColor:InactiveCaption;}EvenRow{Font:Verdana, 8.25pt;BackColor:Lig" +
        "htCyan;}Style6{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;For" +
        "eColor:ControlText;BackColor:Control;}Style49{}Style48{}Style24{}Style9{}Style8{" +
        "}Style1{}Style5{Trimming:None;}Style41{}Style40{}Style43{}Style42{}Style45{Align" +
        "Horz:Near;ForeColor:ControlText;}Style44{AlignHorz:Near;}Style4{}Style46{}Style3" +
        "8{AlignHorz:Near;}Style39{AlignHorz:Near;ForeColor:ControlText;}Style36{}Style37" +
        "{}Style34{}Style35{}Style32{AlignHorz:Near;}Style33{AlignHorz:Near;ForeColor:Con" +
        "trolText;}Style3{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView" +
        " HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSelect=\"False\" Name=\"\" AllowRowSizi" +
        "ng=\"None\" AlternatingRowStyle=\"True\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\"" +
        " ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=" +
        "\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGro" +
        "up=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><E" +
        "ditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Styl" +
        "e8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Foo" +
        "ter\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle paren" +
        "t=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /" +
        "><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=" +
        "\"Style9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><Selected" +
        "Style parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><inte" +
        "rnalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style pa" +
        "rent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><Editor" +
        "Style parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style" +
        "19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><C" +
        "olumnDivider>Gainsboro,Single</ColumnDivider><Width>95</Width><Height>15</Height" +
        "><FetchStyle>True</FetchStyle><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn" +
        "><HeadingStyle parent=\"Style2\" me=\"Style20\" /><Style parent=\"Style1\" me=\"Style21" +
        "\" /><FooterStyle parent=\"Style3\" me=\"Style22\" /><EditorStyle parent=\"Style5\" me=" +
        "\"Style23\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style25\" /><GroupFooterStyle p" +
        "arent=\"Style1\" me=\"Style24\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Si" +
        "ngle</ColumnDivider><Width>75</Width><Height>15</Height><FetchStyle>True</FetchS" +
        "tyle><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
        "yle2\" me=\"Style32\" /><Style parent=\"Style1\" me=\"Style33\" /><FooterStyle parent=\"" +
        "Style3\" me=\"Style34\" /><EditorStyle parent=\"Style5\" me=\"Style35\" /><GroupHeaderS" +
        "tyle parent=\"Style1\" me=\"Style37\" /><GroupFooterStyle parent=\"Style1\" me=\"Style3" +
        "6\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Widt" +
        "h>115</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColu" +
        "mn><HeadingStyle parent=\"Style2\" me=\"Style38\" /><Style parent=\"Style1\" me=\"Style" +
        "39\" /><FooterStyle parent=\"Style3\" me=\"Style40\" /><EditorStyle parent=\"Style5\" m" +
        "e=\"Style41\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style43\" /><GroupFooterStyle" +
        " parent=\"Style1\" me=\"Style42\" /><Visible>True</Visible><ColumnDivider>Gainsboro," +
        "Single</ColumnDivider><Width>115</Width><Height>15</Height><DCIdx>3</DCIdx></C1D" +
        "isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style44\" /><Styl" +
        "e parent=\"Style1\" me=\"Style45\" /><FooterStyle parent=\"Style3\" me=\"Style46\" /><Ed" +
        "itorStyle parent=\"Style5\" me=\"Style47\" /><GroupHeaderStyle parent=\"Style1\" me=\"S" +
        "tyle49\" /><GroupFooterStyle parent=\"Style1\" me=\"Style48\" /><Visible>True</Visibl" +
        "e><ColumnDivider>Gainsboro,None</ColumnDivider><Width>75</Width><Height>15</Heig" +
        "ht><Locked>True</Locked><DCIdx>4</DCIdx></C1DisplayColumn></internalCols><Client" +
        "Rect>0, 0, 626, 299</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid." +
        "MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"No" +
        "rmal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Headin" +
        "g\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\"" +
        " me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=" +
        "\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me" +
        "=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\"" +
        " me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits" +
        ">1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSel" +
        "Width>16</DefaultRecSelWidth><ClientArea>0, 0, 626, 299</ClientArea><PrintPageHe" +
        "aderStyle parent=\"\" me=\"Style26\" /><PrintPageFooterStyle parent=\"\" me=\"Style27\" " +
        "/></Blob>";
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
      this.DescriptionTextBox.TabIndex = 4;
      this.DescriptionTextBox.Text = "";
      // 
      // ShowHistoryCheck
      // 
      this.ShowHistoryCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.ShowHistoryCheck.Location = new System.Drawing.Point(496, 6);
      this.ShowHistoryCheck.Name = "ShowHistoryCheck";
      this.ShowHistoryCheck.Size = new System.Drawing.Size(108, 16);
      this.ShowHistoryCheck.TabIndex = 3;
      this.ShowHistoryCheck.Text = "&Show History";
      this.ShowHistoryCheck.CheckedChanged += new System.EventHandler(this.ShowHistoryCheck_CheckedChanged);
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
      this.Sep1MenuItem.Index = 1;
      this.Sep1MenuItem.Text = "-";
      // 
      // ExitMenuItem
      // 
      this.ExitMenuItem.Index = 2;
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
      this.StatusLabel.TabIndex = 8;
      this.StatusLabel.Tag = null;
      this.StatusLabel.TextDetached = true;
      // 
      // ShortSaleListsHardBorrowForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
      this.ClientSize = new System.Drawing.Size(632, 349);
      this.ContextMenu = this.MainContextMenu;
      this.Controls.Add(this.StatusLabel);
      this.Controls.Add(this.DescriptionTextBox);
      this.Controls.Add(this.HardBorrowGrid);
      this.Controls.Add(this.ShowHistoryCheck);
      this.DockPadding.Bottom = 20;
      this.DockPadding.Left = 1;
      this.DockPadding.Right = 1;
      this.DockPadding.Top = 26;
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "ShortSaleListsHardBorrowForm";
      this.Text = "ShortSale - Lists - Premium Borrow";
      this.Resize += new System.EventHandler(this.ShortSaleListsHardBorrowForm_Resize);
      this.Load += new System.EventHandler(this.ShortSaleHardBorrow_Load);
      this.Closed += new System.EventHandler(this.ShortSaleListsHardBorrowForm_Closed);
      ((System.ComponentModel.ISupportInitialize)(this.HardBorrowGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion
    
    private void BorrowHardGet()
    {
      try
      {
        mainForm.Alert("Please wait... Loading current premium borrow data...", PilotState.Unknown);
        this.Cursor = Cursors.WaitCursor;
        this.Refresh();

        dataSet = mainForm.ShortSaleAgent.BorrowHardGet(ShowHistoryCheck.Checked, (short)mainForm.UtcOffset);
  
        dataView = new DataView(dataSet.Tables["BorrowHard"]);
        dataView.Sort = "SecId";
        
        HardBorrowGrid.SetDataBinding(dataView, null, true);        
        StatusSet();

        mainForm.Alert("Loading current premium borrow data... Done!", PilotState.Normal);
      }
      catch(Exception e)
      {
        mainForm.Alert(e.Message, PilotState.RunFault);
        Log.Write(e.Message + " [ShortSaleListsHardBorrowForm.BorrowHardGet]", Log.Error, 1); 
      }

      this.Cursor = Cursors.Default;
    }

    private void SendToClipboard()
    {
      string gridData = "";

      foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HardBorrowGrid.SelectedCols)
      {
        gridData += dataColumn.Caption + "\t";
      }
      gridData += "\n";

      foreach (int row in HardBorrowGrid.SelectedRows)
      {
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HardBorrowGrid.SelectedCols)
        {
          gridData += dataColumn.CellText(row) + "\t";
        }
        gridData += "\n";
      }

      Clipboard.SetDataObject(gridData, true);
      mainForm.Alert("Total: " + HardBorrowGrid.SelectedRows.Count + " items copied to clipboard.", PilotState.Normal);
    }

    private void StatusSet()
    {
      if (HardBorrowGrid.SelectedRows.Count > 0)
      {
        StatusLabel.Text = "Selected " + HardBorrowGrid.SelectedRows.Count.ToString("#,##0") + " items of "
          + dataView.Count.ToString("#,##0") + " shown in grid.";
      }
      else
      {
        StatusLabel.Text = "Showing " + dataView.Count.ToString("#,##0") + " items in grid.";
      }
    }

    private void ShortSaleHardBorrow_Load(object sender, System.EventArgs e)
    {
      this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
      this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
      this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "450"));
      this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "645"));

      this.Show();
      Application.DoEvents();
      
      BorrowHardGet();
    }

    private void ShortSaleListsHardBorrowForm_Closed(object sender, System.EventArgs e)
    {
      if(this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
        RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
        RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
        RegistryValue.Write(this.Name, "Width", this.Width.ToString());        
      }
    }

    private void ShortSaleListsHardBorrowForm_Resize(object sender, System.EventArgs e)
    {
      DescriptionTextBox.Width = ShowHistoryCheck.Left - 32;    
    }

    private void ShowHistoryCheck_CheckedChanged(object sender, System.EventArgs e)
    {
      BorrowHardGet();
    }

    private void HardBorrowGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
    {
      if (e.Value.Length == 0)
      {
        return;
      }

      try
      {
        switch(HardBorrowGrid.Columns[e.ColIndex].DataField)
        {
          case("StartTime"):
          case("EndTime"):
            e.Value = DateTime.Parse(e.Value).ToString(Standard.DateTimeShortFormat);
            break;
        }
      }
      catch {}
    }

    private void HardBorrowGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        if(HardBorrowGrid.Columns["SecId"].Text.Equals(""))
        {
          mainForm.Alert("The value for Security ID may not be blank.", PilotState.RunFault);
          e.Cancel = true;

          HardBorrowGrid.Col = xSecId;
          return;
        }
      
        mainForm.Alert(HardBorrowGrid.Columns["SecId"].Text
          + " will be added to 'Premium Borrow' status.", PilotState.Unknown);

        mainForm.ShortSaleAgent.BorrowHardSet(HardBorrowGrid.Columns["SecId"].Text.ToUpper(), false, mainForm.UserId);
        
        HardBorrowGrid.Columns["SecId"].Text = HardBorrowGrid.Columns["SecId"].Text.ToUpper();
        HardBorrowGrid.Columns["StartTime"].Text = DateTime.UtcNow.ToString();
        HardBorrowGrid.Columns["ActUserShortName"].Text = "me";

        mainForm.Alert(HardBorrowGrid.Columns["SecId"].Text
          + " has been added to 'Premium Borrow' status.", PilotState.Normal);

        HardBorrowGrid.Col = xSecId;
      }
      catch(Exception ee)
      {
        mainForm.Alert(ee.Message, PilotState.RunFault);
        Log.Write(ee.Message + " [ShortSaleListsHardBorrow.HardBorrowGrid_BeforeUpdate]", Log.Error, 1); 

        e.Cancel = true;
      }
    }

    private void HardBorrowGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      if (!HardBorrowGrid.Columns["EndTime"].Text.Equals(""))
      {
        mainForm.Alert(HardBorrowGrid.Columns["SecId"].Text + " [" + HardBorrowGrid.Columns["Symbol"].Text
          + "] has already been removed from 'Premium Borrow' status.", PilotState.RunFault);
      }
      else
      {
        try
        {
          mainForm.Alert(HardBorrowGrid.Columns["SecId"].Text + " [" + HardBorrowGrid.Columns["Symbol"].Text
            + "] will be removed from 'Premium Borrow' status.", PilotState.Unknown);

          mainForm.ShortSaleAgent.BorrowHardSet(HardBorrowGrid.Columns["SecId"].Text, true, mainForm.UserId);
        
          HardBorrowGrid.Columns["EndTime"].Text = DateTime.Now.ToString(Standard.DateTimeShortFormat);
          HardBorrowGrid.Columns["ActUserShortName"].Text = "me";

          dataSet.AcceptChanges();

          mainForm.Alert(HardBorrowGrid.Columns["SecId"].Text + " [" + HardBorrowGrid.Columns["Symbol"].Text
            + "] has been removed from 'Premium Borrow' status.", PilotState.Normal);

          HardBorrowGrid.Col = xSecId;
        }
        catch(Exception ee)
        {
          mainForm.Alert(ee.Message, PilotState.RunFault);
          Log.Write(ee.Message + "[" + this.Name + ".HardBorrowGrid_BeforeDelete]", Log.Error, 1); 
        }
      }

      HardBorrowGrid.DataChanged = false;
      e.Cancel = true;
    }

    private void HardBorrowGrid_BeforeColEdit(object sender, C1.Win.C1TrueDBGrid.BeforeColEditEventArgs e)
    {
      if (HardBorrowGrid.FilterActive) // The user is filtering rows.
      {
        return;
      }

      if(!HardBorrowGrid.Columns["StartTIme"].Text.Equals("")) // This record is closed.
      {
        e.Cancel = true;
      }

      if(!HardBorrowGrid.Col.Equals(xSecId)) // This column is not editable.
      {
        e.Cancel = true;
      }    
    }

    private void HardBorrowGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
    {
      if(!e.LastRow.Equals(HardBorrowGrid.Row)) // We're on a new row. 
      {
        this.Cursor = Cursors.WaitCursor;
        this.Refresh();

        mainForm.SecId = HardBorrowGrid.Columns["SecId"].Text;
        DescriptionTextBox.Text = mainForm.Description;
        
        this.Cursor = Cursors.Default;
      }    
    }

    private void HardBorrowGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      switch (e.KeyChar)
      {
        case (char)3 :
          if (HardBorrowGrid.SelectedRows.Count > 0)
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
          if (!HardBorrowGrid.EditActive && HardBorrowGrid.DataChanged)
          {
            HardBorrowGrid.DataChanged = false;
          }
          break;
      }
    }

    private void HardBorrowGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      if (e.X <= HardBorrowGrid.RecordSelectorWidth && e.Y <= HardBorrowGrid.RowHeight)
      {
        if (HardBorrowGrid.SelectedRows.Count.Equals(0))
        {
          for (int i = 0; i < HardBorrowGrid.Splits[0,0].Rows.Count; i++)
          {
            HardBorrowGrid.SelectedRows.Add(i);
          }

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HardBorrowGrid.Columns)
          {
            HardBorrowGrid.SelectedCols.Add(dataColumn);
          }
        }
        else
        {
          HardBorrowGrid.SelectedRows.Clear();
          HardBorrowGrid.SelectedCols.Clear();
        }
      }
    }

    private void HardBorrowGrid_SelChange(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      StatusSet();
    }

    private void HardBorrowGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
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

      if (HardBorrowGrid.SelectedCols.Count.Equals(0))
      {
        mainForm.Alert("You have not selected any rows to copy.", PilotState.RunFault);
        return;
      }

      try
      {
        maxTextLength = new int[HardBorrowGrid.SelectedCols.Count];

        // Get the caption length for each column.
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HardBorrowGrid.SelectedCols)
        {
          maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
        }

        // Get the maximum item length for each row in each column.
        foreach (int rowIndex in HardBorrowGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HardBorrowGrid.SelectedCols)
          {
            if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
            {
              maxTextLength[columnIndex] = textLength;
            }
          }
        }

        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HardBorrowGrid.SelectedCols)
        {
          gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
        }
        gridData += "\n";
        
        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HardBorrowGrid.SelectedCols)
        {
          gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
        }
        gridData += "\n";
        
        foreach (int rowIndex in HardBorrowGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in HardBorrowGrid.SelectedCols)
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

        mainForm.Alert("Total: " + HardBorrowGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
      }
      catch (Exception error)
      {       
        mainForm.Alert(error.Message, PilotState.RunFault);
        Log.Write(error.Message + " [ShortSaleListsHardBorrowForm.SendToEmailMenuItem_Click]", Log.Error, 1); 
      }
    }

    private void ExitMenuItem_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }
  }
}
