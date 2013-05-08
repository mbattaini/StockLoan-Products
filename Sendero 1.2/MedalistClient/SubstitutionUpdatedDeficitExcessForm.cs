using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class SubstitutionDeficitExcessForm : System.Windows.Forms.Form
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
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid SubstitutionGrid;

    private System.ComponentModel.Container components = null;

    public SubstitutionDeficitExcessForm(MainForm mainForm)
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
				System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SubstitutionDeficitExcessForm));
				this.SubstitutionGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
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
				((System.ComponentModel.ISupportInitialize)(this.SubstitutionGrid)).BeginInit();
				((System.ComponentModel.ISupportInitialize)(this.EffectDateLabel)).BeginInit();
				((System.ComponentModel.ISupportInitialize)(this.DateEditor)).BeginInit();
				((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
				this.SuspendLayout();
				// 
				// SubstitutionGrid
				// 
				this.SubstitutionGrid.AllowColSelect = false;
				this.SubstitutionGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
				this.SubstitutionGrid.AllowUpdate = false;
				this.SubstitutionGrid.AllowUpdateOnBlur = false;
				this.SubstitutionGrid.AlternatingRows = true;
				this.SubstitutionGrid.CaptionHeight = 17;
				this.SubstitutionGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
				this.SubstitutionGrid.Dock = System.Windows.Forms.DockStyle.Fill;
				this.SubstitutionGrid.EmptyRows = true;
				this.SubstitutionGrid.ExtendRightColumn = true;
				this.SubstitutionGrid.FilterBar = true;
				this.SubstitutionGrid.GroupByCaption = "Drag a column header here to group by that column";
				this.SubstitutionGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
				this.SubstitutionGrid.Location = new System.Drawing.Point(1, 32);
				this.SubstitutionGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
				this.SubstitutionGrid.Name = "SubstitutionGrid";
				this.SubstitutionGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
				this.SubstitutionGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
				this.SubstitutionGrid.PreviewInfo.ZoomFactor = 75;
				this.SubstitutionGrid.RecordSelectorWidth = 16;
				this.SubstitutionGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
				this.SubstitutionGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
				this.SubstitutionGrid.RowHeight = 15;
				this.SubstitutionGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
				this.SubstitutionGrid.Size = new System.Drawing.Size(466, 381);
				this.SubstitutionGrid.TabIndex = 0;
				this.SubstitutionGrid.Text = "Inventory Substitutions";
				this.SubstitutionGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.InventorySubstitutionsGrid_RowColChange);
				this.SubstitutionGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.InventorySubstitutionsGrid_FormatText);
				this.SubstitutionGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Security ID" +
						"\" DataField=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
						"l=\"0\" Caption=\"Total Quantity\" DataField=\"DeficitExcessQuantity\" NumberFormat=\"F" +
						"ormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"" +
						"0\" Caption=\"Symbol\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn" +
						"><C1DataColumn Level=\"0\" Caption=\"\" DataField=\"\"><ValueItems /><GroupInfo /></C1" +
						"DataColumn><C1DataColumn Level=\"0\" Caption=\"Day Count\" DataField=\"DeficitDayCoun" +
						"t\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn></D" +
						"ataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>RecordSel" +
						"ector{AlignImage:Center;}Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25pt;}" +
						"Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style18{}Style19{}" +
						"Style14{AlignHorz:Near;}Style15{AlignHorz:Near;ForeColor:DarkRed;}Style16{}Style" +
						"17{}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style44{AlignHorz:Near;}S" +
						"tyle47{}Style7{}Style38{AlignHorz:Near;}Style37{}Style2{}OddRow{}Style29{}Style2" +
						"8{}Style27{AlignHorz:Center;}Style26{AlignHorz:Center;}Style25{}Footer{}Style23{" +
						"}Style22{}Style21{AlignHorz:Center;}Style20{AlignHorz:Near;}Group{BackColor:Cont" +
						"rolDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Inactive{ForeColor:InactiveCap" +
						"tionText;BackColor:InactiveCaption;}EvenRow{BackColor:LightCyan;}Heading{Wrap:Tr" +
						"ue;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Co" +
						"ntrol;}Style49{}Style48{}Style24{}Style9{}Style6{}Style1{}Style3{}Style41{}Style" +
						"40{}Style43{AlignHorz:Far;}FilterBar{ForeColor:WindowText;BackColor:SeaShell;}St" +
						"yle45{AlignHorz:Near;ForeColor:DarkRed;}Style42{}Style4{}Style46{}Style8{}Style3" +
						"9{AlignHorz:Far;ForeColor:DarkSlateGray;}Style36{}Style5{}Style34{}Style35{}Styl" +
						"e32{}Style33{}Style30{}Style31{}HighlightRow{ForeColor:HighlightText;BackColor:H" +
						"ighlight;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None" +
						"\" VBarStyle=\"Always\" AllowColSelect=\"False\" Name=\"\" AllowRowSizing=\"None\" Altern" +
						"atingRowStyle=\"True\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHei" +
						"ght=\"17\" ExtendRightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedRowBorder" +
						"\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" Horizonta" +
						"lScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle paren" +
						"t=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBar" +
						"Style parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\"" +
						" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"" +
						"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle " +
						"parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><Reco" +
						"rdSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Se" +
						"lected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1Disp" +
						"layColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent=\"Style1\" me" +
						"=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"St" +
						"yle5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><GroupFoot" +
						"erStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><ColumnDivider>Gai" +
						"nsboro,Single</ColumnDivider><Width>87</Width><Height>15</Height><Locked>True</L" +
						"ocked><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
						"tyle2\" me=\"Style44\" /><Style parent=\"Style1\" me=\"Style45\" /><FooterStyle parent=" +
						"\"Style3\" me=\"Style46\" /><EditorStyle parent=\"Style5\" me=\"Style47\" /><GroupHeader" +
						"Style parent=\"Style1\" me=\"Style49\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
						"48\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Wid" +
						"th>73</Width><Height>15</Height><Locked>True</Locked><DCIdx>2</DCIdx></C1Display" +
						"Column><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style38\" /><Style pare" +
						"nt=\"Style1\" me=\"Style39\" /><FooterStyle parent=\"Style3\" me=\"Style40\" /><EditorSt" +
						"yle parent=\"Style5\" me=\"Style41\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style43" +
						"\" /><GroupFooterStyle parent=\"Style1\" me=\"Style42\" /><Visible>True</Visible><Col" +
						"umnDivider>Gainsboro,Single</ColumnDivider><Width>144</Width><Height>15</Height>" +
						"<FooterDivider>False</FooterDivider><DCIdx>1</DCIdx></C1DisplayColumn><C1Display" +
						"Column><HeadingStyle parent=\"Style2\" me=\"Style26\" /><Style parent=\"Style1\" me=\"S" +
						"tyle27\" /><FooterStyle parent=\"Style3\" me=\"Style28\" /><EditorStyle parent=\"Style" +
						"5\" me=\"Style29\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style31\" /><GroupFooterS" +
						"tyle parent=\"Style1\" me=\"Style30\" /><Visible>True</Visible><ColumnDivider>Gainsb" +
						"oro,None</ColumnDivider><Height>15</Height><HeaderDivider>False</HeaderDivider><" +
						"DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" " +
						"me=\"Style20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3" +
						"\" me=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle p" +
						"arent=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><" +
						"Visible>True</Visible><ColumnDivider>DarkGray,None</ColumnDivider><Height>15</He" +
						"ight><HeaderDivider>False</HeaderDivider><FooterDivider>False</FooterDivider><DC" +
						"Idx>3</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 462, 377</Client" +
						"Rect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedSt" +
						"yles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style" +
						" parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style pa" +
						"rent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style par" +
						"ent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style par" +
						"ent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"" +
						"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style pa" +
						"rent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>" +
						"1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidt" +
						"h><ClientArea>0, 0, 462, 377</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Sty" +
						"le36\" /><PrintPageFooterStyle parent=\"\" me=\"Style37\" /></Blob>";
				// 
				// EffectDateLabel
				// 
				this.EffectDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
				this.EffectDateLabel.Location = new System.Drawing.Point(272, 8);
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
				this.DateEditor.Location = new System.Drawing.Point(368, 4);
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
				// SubstitutionDeficitExcessForm
				// 
				this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
				this.ClientSize = new System.Drawing.Size(468, 433);
				this.ContextMenu = this.MainContextMenu;
				this.Controls.Add(this.StatusLabel);
				this.Controls.Add(this.DateEditor);
				this.Controls.Add(this.EffectDateLabel);
				this.Controls.Add(this.SubstitutionGrid);
				this.DockPadding.Bottom = 20;
				this.DockPadding.Left = 1;
				this.DockPadding.Right = 1;
				this.DockPadding.Top = 32;
				this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
				this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
				this.Name = "SubstitutionDeficitExcessForm";
				this.Text = "Substitution - Deficit / Excess";
				this.Load += new System.EventHandler(this.SubstitutionDeficitExcessForm_Load);
				this.Closed += new System.EventHandler(this.SubstitutionDeficitExcessForm_Closed);
				((System.ComponentModel.ISupportInitialize)(this.SubstitutionGrid)).EndInit();
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
				mainForm.Alert("Please wait... Loading substitutions data...", PilotState.Unknown);
				this.Cursor = Cursors.WaitCursor;
				this.Refresh();

				dataSet = mainForm.SubstitutionAgent.SubstitutionUpdatedDeficitExcessDataGet(effectDate);

				SubstitutionGrid.SetDataBinding(dataSet.Tables["SubstitutionUpdatedDeficitExcess"], null, true);
				StatusSet();    

				mainForm.Alert("Loading substitution updated deficit/excess data... Done!", PilotState.Normal);
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
			}

      this.Cursor = Cursors.Default;
    }

    private void SendToClipboard()
    {
      string gridData = "";

      foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionGrid.SelectedCols)
      {
        gridData += dataColumn.Caption + "\t";
      }
      gridData += "\n";

      foreach (int row in SubstitutionGrid.SelectedRows)
      {
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionGrid.SelectedCols)
        {
          gridData += dataColumn.CellText(row) + "\t";
        }
        gridData += "\n";
      }

      Clipboard.SetDataObject(gridData, true);
      mainForm.Alert("Total: " + SubstitutionGrid.SelectedRows.Count + " items copied to clipboard.", PilotState.Normal);
    }

    private void StatusSet()
    {
      if (SubstitutionGrid.SelectedRows.Count > 0)
      {
        StatusLabel.Text = "Selected " + SubstitutionGrid.SelectedRows.Count.ToString("#,##0") + " items of "
          + dataSet.Tables["SubstitutionUpdatedDeficitExcess"].Rows.Count.ToString("#,##0") + " shown in grid.";
      }
      else
      {
        StatusLabel.Text = "Showing " + dataSet.Tables["SubstitutionUpdatedDeficitExcess"].Rows.Count.ToString("#,##0") + " items in grid.";
      }
    }

			private void SubstitutionDeficitExcessForm_Load(object sender, System.EventArgs e)
			{
					this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
					this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
					this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "500"));
					this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "750"));

					DateEditor.Text = mainForm.ServiceAgent.BizDatePriorExchange();

					this.Show();
					Application.DoEvents();
      
					ListLoad(DateEditor.Text);
			}

    private void SubstitutionDeficitExcessForm_Closed(object sender, System.EventArgs e)
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
      if(!e.LastRow.Equals(SubstitutionGrid.Row))
      {
        this.Cursor = Cursors.WaitCursor;  
        this.Refresh();
        
        mainForm.SecId = SubstitutionGrid.Columns["SecId"].Text;
        
        this.Cursor = Cursors.Default;
      }
    }

    private void InventorySubstitutionsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      switch (e.KeyChar)
      {
        case (char)3 :
          if (SubstitutionGrid.SelectedRows.Count > 0)
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
          if (!SubstitutionGrid.EditActive && SubstitutionGrid.DataChanged)
          {
            SubstitutionGrid.DataChanged = false;
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
      if (e.X <= SubstitutionGrid.RecordSelectorWidth && e.Y <= SubstitutionGrid.RowHeight)
      {
        if (SubstitutionGrid.SelectedRows.Count.Equals(0))
        {
          for (int i = 0; i < SubstitutionGrid.Splits[0,0].Rows.Count; i++)
          {
            SubstitutionGrid.SelectedRows.Add(i);
          }

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionGrid.Columns)
          {
            SubstitutionGrid.SelectedCols.Add(dataColumn);
          }
        }
        else
        {
          SubstitutionGrid.SelectedRows.Clear();
          SubstitutionGrid.SelectedCols.Clear();
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
        SubstitutionGrid.Focus();
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

      if (SubstitutionGrid.SelectedCols.Count.Equals(0))
      {
        mainForm.Alert("You have not selected any rows to copy.", PilotState.RunFault);
        return;
      }

      try
      {
        maxTextLength = new int[SubstitutionGrid.SelectedCols.Count];

        // Get the caption length for each column.
        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionGrid.SelectedCols)
        {
          maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
        }

        // Get the maximum item length for each row in each column.
        foreach (int rowIndex in SubstitutionGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionGrid.SelectedCols)
          {
            if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
            {
              maxTextLength[columnIndex] = textLength;
            }
          }
        }

        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionGrid.SelectedCols)
        {
          gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
        }
        gridData += "\n";
        
        columnIndex = -1;

        foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionGrid.SelectedCols)
        {
          gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
        }
        gridData += "\n";
        
        foreach (int rowIndex in SubstitutionGrid.SelectedRows)
        {
          columnIndex = -1;

          foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SubstitutionGrid.SelectedCols)
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

        mainForm.Alert("Total: " + SubstitutionGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
      }
      catch (Exception error)
      {       
        mainForm.Alert(error.Message, PilotState.RunFault);
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
				excel.ExportGridToExcel(ref SubstitutionGrid);
			}
			catch {}
		}

		private void InventorySubstitutionsGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
				switch (e.Column.DataField)
				{
						case "DeficitExcessQuantity":
						case "TotalQuantity":
						case "DeficitDayCount":
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