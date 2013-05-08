using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class SegEntriesForm : System.Windows.Forms.Form
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

    private System.ComponentModel.Container components = null;

    public SegEntriesForm(MainForm mainForm)
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
		System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SegEntriesForm));
		this.SegEntriesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
		this.StatusLabel = new C1.Win.C1Input.C1Label();
		this.MainContextMenu = new System.Windows.Forms.ContextMenu();
		this.SendToMenuItem = new System.Windows.Forms.MenuItem();
		this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
		this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
		this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
		this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
		this.ExitMenuItem = new System.Windows.Forms.MenuItem();
		((System.ComponentModel.ISupportInitialize)(this.SegEntriesGrid)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
		this.SuspendLayout();
		// 
		// SegEntriesGrid
		// 
		this.SegEntriesGrid.AllowColSelect = false;
		this.SegEntriesGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
		this.SegEntriesGrid.AllowUpdateOnBlur = false;
		this.SegEntriesGrid.AlternatingRows = true;
		this.SegEntriesGrid.CaptionHeight = 17;
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
		this.SegEntriesGrid.RecordSelectorWidth = 17;
		this.SegEntriesGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
		this.SegEntriesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
		this.SegEntriesGrid.RowHeight = 15;
		this.SegEntriesGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
		this.SegEntriesGrid.Size = new System.Drawing.Size(726, 381);
		this.SegEntriesGrid.TabIndex = 0;
		this.SegEntriesGrid.Text = "Seg Entries";
		this.SegEntriesGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.SegEntriesGrid_RowColChange);
		this.SegEntriesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.SegEntriesGrid_BeforeUpdate);
		this.SegEntriesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.SegEntriesGrid_FormatText);
		this.SegEntriesGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
			"\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
			"l=\"0\" Caption=\"Quantity\" DataField=\"Quantity\" NumberFormat=\"FormatText Event\"><V" +
			"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Account" +
			" Number\" DataField=\"AccountNumber\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
			"DataColumn Level=\"0\" Caption=\"Account Type\" DataField=\"AccountType\" NumberFormat" +
			"=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
			"l=\"0\" Caption=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataCol" +
			"umn><C1DataColumn Level=\"0\" Caption=\"Indicator\" DataField=\"Indicator\"><ValueItem" +
			"s /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"R\" DataField=\"I" +
			"sRequested\"><ValueItems Presentation=\"CheckBox\" /><GroupInfo /></C1DataColumn><C" +
			"1DataColumn Level=\"0\" Caption=\"P\" DataField=\"IsProcessed\"><ValueItems Presentati" +
			"on=\"CheckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Pro" +
			"cessId\" DataField=\"ProcessId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataC" +
			"olumn Level=\"0\" Caption=\"\" DataField=\"\"><ValueItems /><GroupInfo /></C1DataColum" +
			"n></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>High" +
			"lightRow{ForeColor:HighlightText;BackColor:Highlight;}Inactive{ForeColor:Inactiv" +
			"eCaptionText;BackColor:InactiveCaption;}Style78{}Style79{}Selected{ForeColor:Hig" +
			"hlightText;BackColor:Highlight;}Editor{}Style72{}Style73{}Style70{}Style71{}Styl" +
			"e76{}Style77{}Style74{AlignHorz:Near;}Style75{AlignHorz:Near;}FilterBar{ForeColo" +
			"r:WindowText;BackColor:SeaShell;}Heading{Wrap:True;AlignVert:Center;Border:Raise" +
			"d,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Control;}Style18{}Style19{}Style14" +
			"{AlignHorz:Near;}Style15{AlignHorz:Near;ForeColor:DarkRed;}Style16{}Style17{}Sty" +
			"le10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style27{AlignHorz:Far;ForeColor:" +
			"DarkRed;}Style22{}Style29{}Style28{}Style26{AlignHorz:Far;AlignVert:Center;}Styl" +
			"e9{}Style8{}Style25{}Style24{}Style5{}Style4{}Style7{}Style6{}Style1{}Style23{}S" +
			"tyle3{}Style2{}Style21{AlignHorz:Center;ForeColor:DarkSlateGray;}Style20{AlignHo" +
			"rz:Far;AlignVert:Center;}OddRow{}Style38{AlignHorz:Far;AlignVert:Center;}Style39" +
			"{AlignHorz:Far;ForeColor:DarkSlateGray;}Style36{}Style37{}Style34{}Style35{}Styl" +
			"e32{}Style33{}Style30{}Style49{}Style48{}Style31{}Normal{Font:Verdana, 8.25pt;}S" +
			"tyle41{}Style40{}Style43{AlignHorz:Far;}Style42{}Style45{AlignHorz:Near;ForeColo" +
			"r:DarkRed;}Style44{AlignHorz:Near;}Style47{}Style46{}EvenRow{BackColor:LightCyan" +
			";}Style59{}Style58{}RecordSelector{AlignImage:Center;}Style51{AlignHorz:Center;}" +
			"Style50{AlignHorz:Center;AlignVert:Center;}Footer{}Style52{}Style53{}Style54{}St" +
			"yle55{AlignHorz:Center;}Style56{AlignHorz:Far;AlignVert:Center;}Style57{AlignHor" +
			"z:Far;ForeColor:DarkSlateGray;}Caption{AlignHorz:Center;}Style69{AlignHorz:Near;" +
			"}Style68{AlignHorz:Near;}Style63{AlignHorz:Center;}Style62{AlignHorz:Center;}Sty" +
			"le61{}Style60{}Style67{AlignHorz:Center;}Style66{}Style65{}Style64{}Group{BackCo" +
			"lor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}</Data></Styles><Split" +
			"s><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSel" +
			"ect=\"False\" Name=\"\" AllowRowSizing=\"None\" AlternatingRowStyle=\"True\" CaptionHeig" +
			"ht=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True" +
			"\" FilterBar=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"17\" DefRe" +
			"cSelWidth=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle p" +
			"arent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRo" +
			"wStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Sty" +
			"le13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me" +
			"=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle par" +
			"ent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" />" +
			"<OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSe" +
			"lector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style par" +
			"ent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"" +
			"Style2\" me=\"Style14\" /><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle parent" +
			"=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeade" +
			"rStyle parent=\"Style1\" me=\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"Styl" +
			"e18\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Wi" +
			"dth>95</Width><Height>15</Height><Locked>True</Locked><DCIdx>0</DCIdx></C1Displa" +
			"yColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style44\" /><Style par" +
			"ent=\"Style1\" me=\"Style45\" /><FooterStyle parent=\"Style3\" me=\"Style46\" /><EditorS" +
			"tyle parent=\"Style5\" me=\"Style47\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style4" +
			"9\" /><GroupFooterStyle parent=\"Style1\" me=\"Style48\" /><Visible>True</Visible><Co" +
			"lumnDivider>LightGray,Single</ColumnDivider><Width>75</Width><Height>15</Height>" +
			"<Locked>True</Locked><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><Heading" +
			"Style parent=\"Style2\" me=\"Style26\" /><Style parent=\"Style1\" me=\"Style27\" /><Foot" +
			"erStyle parent=\"Style3\" me=\"Style28\" /><EditorStyle parent=\"Style5\" me=\"Style29\"" +
			" /><GroupHeaderStyle parent=\"Style1\" me=\"Style31\" /><GroupFooterStyle parent=\"St" +
			"yle1\" me=\"Style30\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</Col" +
			"umnDivider><Width>107</Width><Height>15</Height><Locked>True</Locked><DCIdx>2</D" +
			"CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style3" +
			"8\" /><Style parent=\"Style1\" me=\"Style39\" /><FooterStyle parent=\"Style3\" me=\"Styl" +
			"e40\" /><EditorStyle parent=\"Style5\" me=\"Style41\" /><GroupHeaderStyle parent=\"Sty" +
			"le1\" me=\"Style43\" /><GroupFooterStyle parent=\"Style1\" me=\"Style42\" /><Visible>Tr" +
			"ue</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>97</Width><Hei" +
			"ght>15</Height><Locked>True</Locked><DCIdx>3</DCIdx></C1DisplayColumn><C1Display" +
			"Column><HeadingStyle parent=\"Style2\" me=\"Style56\" /><Style parent=\"Style1\" me=\"S" +
			"tyle57\" /><FooterStyle parent=\"Style3\" me=\"Style58\" /><EditorStyle parent=\"Style" +
			"5\" me=\"Style59\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style61\" /><GroupFooterS" +
			"tyle parent=\"Style1\" me=\"Style60\" /><Visible>True</Visible><ColumnDivider>LightG" +
			"ray,Single</ColumnDivider><Width>101</Width><Height>15</Height><Locked>True</Loc" +
			"ked><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Sty" +
			"le2\" me=\"Style20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"S" +
			"tyle3\" me=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderSt" +
			"yle parent=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24" +
			"\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width" +
			">61</Width><Height>15</Height><Locked>True</Locked><DCIdx>5</DCIdx></C1DisplayCo" +
			"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style50\" /><Style parent" +
			"=\"Style1\" me=\"Style51\" /><FooterStyle parent=\"Style3\" me=\"Style52\" /><EditorStyl" +
			"e parent=\"Style5\" me=\"Style53\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style55\" " +
			"/><GroupFooterStyle parent=\"Style1\" me=\"Style54\" /><Visible>True</Visible><Colum" +
			"nDivider>LightGray,Single</ColumnDivider><Width>30</Width><Height>15</Height><DC" +
			"Idx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
			"=\"Style62\" /><Style parent=\"Style1\" me=\"Style63\" /><FooterStyle parent=\"Style3\" " +
			"me=\"Style64\" /><EditorStyle parent=\"Style5\" me=\"Style65\" /><GroupHeaderStyle par" +
			"ent=\"Style1\" me=\"Style67\" /><GroupFooterStyle parent=\"Style1\" me=\"Style66\" /><Vi" +
			"sible>True</Visible><ColumnDivider>LightGray,None</ColumnDivider><Width>30</Widt" +
			"h><Height>15</Height><HeaderDivider>False</HeaderDivider><FooterDivider>False</F" +
			"ooterDivider><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pa" +
			"rent=\"Style2\" me=\"Style68\" /><Style parent=\"Style1\" me=\"Style69\" /><FooterStyle " +
			"parent=\"Style3\" me=\"Style70\" /><EditorStyle parent=\"Style5\" me=\"Style71\" /><Grou" +
			"pHeaderStyle parent=\"Style1\" me=\"Style73\" /><GroupFooterStyle parent=\"Style1\" me" +
			"=\"Style72\" /><ColumnDivider>DarkGray,None</ColumnDivider><Height>15</Height><Hea" +
			"derDivider>False</HeaderDivider><FooterDivider>False</FooterDivider><DCIdx>8</DC" +
			"Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style74" +
			"\" /><Style parent=\"Style1\" me=\"Style75\" /><FooterStyle parent=\"Style3\" me=\"Style" +
			"76\" /><EditorStyle parent=\"Style5\" me=\"Style77\" /><GroupHeaderStyle parent=\"Styl" +
			"e1\" me=\"Style79\" /><GroupFooterStyle parent=\"Style1\" me=\"Style78\" /><Visible>Tru" +
			"e</Visible><ColumnDivider>DarkGray,None</ColumnDivider><Height>15</Height><Heade" +
			"rDivider>False</HeaderDivider><FooterDivider>False</FooterDivider><DCIdx>9</DCId" +
			"x></C1DisplayColumn></internalCols><ClientRect>0, 0, 722, 377</ClientRect><Borde" +
			"rSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style" +
			" parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"He" +
			"ading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Headi" +
			"ng\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal" +
			"\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal" +
			"\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me" +
			"=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Capti" +
			"on\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSpli" +
			"ts><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth><ClientAr" +
			"ea>0, 0, 722, 377</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style36\" /><Pr" +
			"intPageFooterStyle parent=\"\" me=\"Style37\" /></Blob>";
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
		// SegEntriesForm
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
		this.ClientSize = new System.Drawing.Size(728, 433);
		this.ContextMenu = this.MainContextMenu;
		this.Controls.Add(this.StatusLabel);
		this.Controls.Add(this.SegEntriesGrid);
		this.DockPadding.Bottom = 20;
		this.DockPadding.Left = 1;
		this.DockPadding.Right = 1;
		this.DockPadding.Top = 32;
		this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.Name = "SegEntriesForm";
		this.Text = "Seg Entries";
		this.Load += new System.EventHandler(this.SegEntriesForm_Load);
		((System.ComponentModel.ISupportInitialize)(this.SegEntriesGrid)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
		this.ResumeLayout(false);

	}
    #endregion
    
    private void ListLoad()
    {
      try
      {
        mainForm.Alert("Please wait... Loading seg entries data...", PilotState.Unknown);
        this.Cursor = Cursors.WaitCursor;
        this.Refresh();

        dataSet = mainForm.PositionAgent.SegEntriesDataGet();
		dataView = new DataView(dataSet.Tables["SegEntries"]);

        SegEntriesGrid.SetDataBinding(dataView, null, true);
        StatusSet();    

        mainForm.Alert("Loading seg entries data... Done!", PilotState.Normal);
      }
      catch(Exception e)
      {
        mainForm.Alert(e.Message, PilotState.RunFault);
        Log.Write(e.Message + " [SegEntriesForm.ListLoad]", Log.Error, 1); 
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

    private void StatusSet()
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

    private void SegEntriesForm_Load(object sender, System.EventArgs e)
    {
      this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
      this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
      this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "500"));
      this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "750"));

      this.Show();
      Application.DoEvents();
      
      ListLoad();
    }

    private void SegEntriesForm_Closed(object sender, System.EventArgs e)
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
        Log.Write(error.Message + " [SegEntriesForm.SendToEmailMenuItem_Click]", Log.Error, 1); 
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
			  mainForm.PositionAgent.SegEntryFlagSet(
				  SegEntriesGrid.Columns["ProcessId"].Text, 
				  (SegEntriesGrid.Columns["IsRequested"].Value != DBNull.Value) ? bool.Parse(SegEntriesGrid.Columns["IsRequested"].Text) : false,
				  (SegEntriesGrid.Columns["IsProcessed"].Value != DBNull.Value) ? bool.Parse(SegEntriesGrid.Columns["IsProcessed"].Text) : false
				  );
			 
			  mainForm.Alert("Seg Entry for Account Number " +  SegEntriesGrid.Columns["AccountNumber"].Text + " was updated.", PilotState.Normal);
		  }
      
		  catch(Exception error)
		  {
			  mainForm.Alert(error.Message, PilotState.RunFault);
			  Log.Write(error.Message + " [SegEntriesForm.SegEntriesGrid_BeforeUpdate]", 1);

			  e.Cancel = true;
			  return;
		  }
	  }

  }
}