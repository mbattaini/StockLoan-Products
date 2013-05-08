// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Remoting;
using System.ComponentModel;
using System.Windows.Forms;
using Anetics.Common;
using Anetics.Medalist;

namespace Anetics.Medalist
{
  public class ShortSaleBillingSummaryActivityForm : System.Windows.Forms.Form
  {
    private System.ComponentModel.Container components = null;
    
		private MainForm mainForm;
		private DataSet dataSet = null;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep2MenuItem;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ActivityGrid;
		private System.Windows.Forms.MenuItem ShowMenuItem;
		private System.Windows.Forms.MenuItem ShowAllGroupsMenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;

		private string groupCode = "";

    public ShortSaleBillingSummaryActivityForm(string groupCode, MainForm mainForm)
    {
      InitializeComponent();
      this.mainForm = mainForm;
			this.groupCode = groupCode;
			dataSet = new DataSet();			
    }

    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if(components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleBillingSummaryActivityForm));
			this.ActivityGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.ShowMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowAllGroupsMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.ActivityGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// ActivityGrid
			// 
			this.ActivityGrid.CaptionHeight = 17;
			this.ActivityGrid.ContextMenu = this.MainContextMenu;
			this.ActivityGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ActivityGrid.ExtendRightColumn = true;
			this.ActivityGrid.FetchRowStyles = true;
			this.ActivityGrid.FilterBar = true;
			this.ActivityGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ActivityGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ActivityGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.ActivityGrid.Location = new System.Drawing.Point(0, 0);
			this.ActivityGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.ActivityGrid.Name = "ActivityGrid";
			this.ActivityGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ActivityGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ActivityGrid.PreviewInfo.ZoomFactor = 75;
			this.ActivityGrid.RecordSelectorWidth = 16;
			this.ActivityGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.ActivityGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.ActivityGrid.RowHeight = 16;
			this.ActivityGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.ActivityGrid.Size = new System.Drawing.Size(912, 293);
			this.ActivityGrid.TabIndex = 0;
			this.ActivityGrid.Text = "Activity";
			this.ActivityGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.ActivityGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Act Time\" D" +
				"ataField=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></" +
				"C1DataColumn><C1DataColumn Level=\"0\" Caption=\"ActUserId\" DataField=\"ActUserId\"><" +
				"ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"GroupC" +
				"ode\" DataField=\"GroupCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColu" +
				"mn Level=\"0\" Caption=\"Activity\" DataField=\"Activity\"><ValueItems /><GroupInfo />" +
				"</C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrappe" +
				"r\"><Data>Caption{AlignHorz:Center;}Style27{}Normal{Font:Verdana, 8.25pt;}Style25" +
				"{}Style24{}Editor{}Style18{}Style19{}Style14{AlignHorz:Near;}Style15{AlignHorz:N" +
				"ear;}Style16{}Style17{}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}Style12" +
				"{}Style29{}Style28{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}St" +
				"yle26{}RecordSelector{AlignImage:Center;}Footer{}Style23{}Style22{}Style21{Align" +
				"Horz:Near;}Style20{AlignHorz:Near;}Group{AlignVert:Center;Border:None,,0, 0, 0, " +
				"0;BackColor:ControlDark;}Inactive{ForeColor:InactiveCaptionText;BackColor:Inacti" +
				"veCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:True;BackColor:Control;Border:Ra" +
				"ised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style3{}Style7{}Style6{" +
				"}Style1{}Style5{}Style41{}Style40{}Style8{}FilterBar{BackColor:SeaShell;}Selecte" +
				"d{ForeColor:HighlightText;BackColor:Highlight;}Style4{}Style9{}Style38{}Style39{" +
				"}Style36{AlignHorz:Near;}Style37{AlignHorz:Near;}Style34{}Style35{}Style32{}Styl" +
				"e33{}Style30{AlignHorz:Near;}Style31{AlignHorz:Near;}Style2{}</Data></Styles><Sp" +
				"lits><C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" Name=\"\" " +
				"CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightC" +
				"olumn=\"True\" FetchRowStyles=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedRowBorde" +
				"r\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" Horizont" +
				"alScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle pare" +
				"nt=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBa" +
				"rStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3" +
				"\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=" +
				"\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle" +
				" parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><Rec" +
				"ordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"S" +
				"elected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1Dis" +
				"playColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent=\"Style1\" m" +
				"e=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"S" +
				"tyle5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><GroupFoo" +
				"terStyle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><ColumnDivider>Ga" +
				"insboro,Single</ColumnDivider><Width>150</Width><Height>16</Height><DCIdx>0</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style20\"" +
				" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"Style2" +
				"2\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=\"Style" +
				"1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Visible>True" +
				"</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Height>16</Height><DCI" +
				"dx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style30\" /><Style parent=\"Style1\" me=\"Style31\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style32\" /><EditorStyle parent=\"Style5\" me=\"Style33\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style35\" /><GroupFooterStyle parent=\"Style1\" me=\"Style34\" /><Vis" +
				"ible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Height>16</He" +
				"ight><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"St" +
				"yle2\" me=\"Style36\" /><Style parent=\"Style1\" me=\"Style37\" /><FooterStyle parent=\"" +
				"Style3\" me=\"Style38\" /><EditorStyle parent=\"Style5\" me=\"Style39\" /><GroupHeaderS" +
				"tyle parent=\"Style1\" me=\"Style41\" /><GroupFooterStyle parent=\"Style1\" me=\"Style4" +
				"0\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Heig" +
				"ht>16</Height><DCIdx>3</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0," +
				" 908, 289</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView>" +
				"</Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"" +
				"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Cap" +
				"tion\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selec" +
				"ted\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"Highlight" +
				"Row\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" " +
				"/><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"Filte" +
				"rBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSp" +
				"lits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</" +
				"DefaultRecSelWidth><ClientArea>0, 0, 908, 289</ClientArea><PrintPageHeaderStyle " +
				"parent=\"\" me=\"Style28\" /><PrintPageFooterStyle parent=\"\" me=\"Style29\" /></Blob>";
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.ShowMenuItem,
																																										this.SendToMenuItem,
																																										this.Sep2MenuItem,
																																										this.ExitMenuItem});
			// 
			// ShowMenuItem
			// 
			this.ShowMenuItem.Index = 0;
			this.ShowMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.ShowAllGroupsMenuItem});
			this.ShowMenuItem.Text = "Show";
			// 
			// ShowAllGroupsMenuItem
			// 
			this.ShowAllGroupsMenuItem.Index = 0;
			this.ShowAllGroupsMenuItem.Text = "All Groups";
			this.ShowAllGroupsMenuItem.Click += new System.EventHandler(this.ShowAllGroupsMenuItem_Click);
			// 
			// SendToMenuItem
			// 
			this.SendToMenuItem.Index = 1;
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
			// Sep2MenuItem
			// 
			this.Sep2MenuItem.Index = 2;
			this.Sep2MenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 3;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// ShortSaleBillingSummaryActivityForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(912, 293);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.ActivityGrid);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.HelpButton = true;
			this.Name = "ShortSaleBillingSummaryActivityForm";
			this.Text = "Short Sale - Billing - Negative Rebates Summary - Activity";
			this.Load += new System.EventHandler(this.ShortSaleBillingSummaryActivityForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.ActivityGrid)).EndInit();
			this.ResumeLayout(false);

		}
    #endregion

    private void ShortSaleBillingSummaryActivityForm_Load(object sender, System.EventArgs e)
    {
      int height = mainForm.Height / 2;
      int width  = mainForm.Width / 2;
      
      try
      {
        this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
        this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
        this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
        this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));
        
        dataSet = mainForm.RebateAgent.ShortSaleBillingSummaryActivityGet(
					groupCode,
					mainForm.UtcOffset);
				
				ActivityGrid.SetDataBinding(dataSet, "Activity", true);								
			}
      catch(Exception ee)
      {
        mainForm.Alert(ee.Message, PilotState.RunFault);        
      }
    }

    private void ShortSaleBillingSummaryActivityForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if(this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
        RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
        RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
        RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
      }
    }

		private void FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "ActTime":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeFormat);
					}
					catch {}
					break;
			}	
		}

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\r\n";

			foreach (int row in ActivityGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\r\n";
			}

			Clipboard.SetDataObject(gridData, true);

			mainForm.Alert("Total: " + ActivityGrid.SelectedRows.Count + " items copied to the clipboard.", PilotState.Normal);
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n";

			if (ActivityGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows to copy.", PilotState.Normal);
				return;
			}

			try
			{
				maxTextLength = new int[ActivityGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in ActivityGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in ActivityGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ActivityGrid.SelectedCols)
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

				mainForm.Alert("Total: " + ActivityGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
			}
			catch (Exception error)
			{       
				mainForm.Alert(error.Message, PilotState.Normal);
			}
		}
		
		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			Excel excel = new Excel();	
			excel.ExportGridToExcel(ref ActivityGrid);

			this.Cursor = Cursors.Default;
		}

		private void ShowAllGroupsMenuItem_Click(object sender, System.EventArgs e)
		{
			ShowAllGroupsMenuItem.Checked = !ShowAllGroupsMenuItem.Checked;

			try
			{
				dataSet = mainForm.RebateAgent.ShortSaleBillingSummaryActivityGet(
					(ShowAllGroupsMenuItem.Checked)?"":groupCode,
					mainForm.UtcOffset);
				
				ActivityGrid.SetDataBinding(dataSet, "Activity", true);			
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.Normal);
			}
		}

	}
}
