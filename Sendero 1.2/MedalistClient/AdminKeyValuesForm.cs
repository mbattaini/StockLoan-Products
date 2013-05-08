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
  public class AdminKeyValuesForm : System.Windows.Forms.Form
  {
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid AdminKeyValuesGrid;
    private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep2MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
    private MainForm mainForm;

    public AdminKeyValuesForm(MainForm mainForm)
    {
      InitializeComponent();
      this.mainForm = mainForm;
      
      try
      {
        AdminKeyValuesGrid.AllowUpdate = mainForm.AdminAgent.MayEdit(mainForm.UserId,"AdminKeyValues");;
      }
      catch(Exception e)
      {
        mainForm.Alert(e.Message, PilotState.RunFault);
      }
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AdminKeyValuesForm));
			this.AdminKeyValuesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.AdminKeyValuesGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// AdminKeyValuesGrid
			// 
			this.AdminKeyValuesGrid.AllowColMove = false;
			this.AdminKeyValuesGrid.CaptionHeight = 17;
			this.AdminKeyValuesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AdminKeyValuesGrid.ExtendRightColumn = true;
			this.AdminKeyValuesGrid.FilterBar = true;
			this.AdminKeyValuesGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.AdminKeyValuesGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.AdminKeyValuesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.AdminKeyValuesGrid.Location = new System.Drawing.Point(0, 0);
			this.AdminKeyValuesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.AdminKeyValuesGrid.Name = "AdminKeyValuesGrid";
			this.AdminKeyValuesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.AdminKeyValuesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.AdminKeyValuesGrid.PreviewInfo.ZoomFactor = 75;
			this.AdminKeyValuesGrid.RecordSelectorWidth = 16;
			this.AdminKeyValuesGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.AdminKeyValuesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.AdminKeyValuesGrid.RowHeight = 16;
			this.AdminKeyValuesGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.AdminKeyValuesGrid.Size = new System.Drawing.Size(528, 317);
			this.AdminKeyValuesGrid.TabIndex = 0;
			this.AdminKeyValuesGrid.Text = "Key Values";
			this.AdminKeyValuesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.AdminKeyValuesGrid_BeforeUpdate);
			this.AdminKeyValuesGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AdminKeyValuesGrid_KeyPress);
			this.AdminKeyValuesGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Key Value\" " +
				"DataField=\"KeyValue\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Lev" +
				"el=\"0\" Caption=\"Key Id\" DataField=\"KeyId\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Hi" +
				"ghlightRow{ForeColor:HighlightText;BackColor:Highlight;}Caption{AlignHorz:Center" +
				";}Normal{Font:Verdana, 8.25pt;}Style25{}Selected{ForeColor:HighlightText;BackCol" +
				"or:Highlight;}Editor{}Style18{}Style19{}Style14{AlignHorz:Center;}Style15{Font:V" +
				"erdana, 8.25pt, style=Bold;AlignHorz:Near;}Style16{}Style17{}Style10{AlignHorz:N" +
				"ear;}Style11{}OddRow{}Style13{}Footer{}Style29{}Style28{}Style27{}Style26{}Recor" +
				"dSelector{AlignImage:Center;}Style24{}Style23{}Style22{}Style21{Font:Verdana, 8." +
				"25pt;AlignHorz:Far;ForeColor:DarkSlateGray;}Style20{AlignHorz:Center;}Inactive{F" +
				"oreColor:InactiveCaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}" +
				"Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlTe" +
				"xt;BackColor:Control;}FilterBar{BackColor:SeaShell;}Style5{}Style4{}Style9{}Styl" +
				"e8{}Style12{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Cente" +
				"r;}Style7{}Style6{}Style1{}Style3{}Style2{}</Data></Styles><Splits><C1.Win.C1Tru" +
				"eDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColMove=\"False\" Name=" +
				"\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRig" +
				"htColumn=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWi" +
				"dth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><" +
				"CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Sty" +
				"le5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"Filt" +
				"erBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle par" +
				"ent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLig" +
				"htRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" " +
				"me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle pa" +
				"rent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6" +
				"\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><Heading" +
				"Style parent=\"Style2\" me=\"Style20\" /><Style parent=\"Style1\" me=\"Style21\" /><Foot" +
				"erStyle parent=\"Style3\" me=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\"" +
				" /><GroupHeaderStyle parent=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"St" +
				"yle1\" me=\"Style24\" /><Visible>True</Visible><ColumnDivider>DarkGray,None</Column" +
				"Divider><Width>280</Width><Height>15</Height><Locked>True</Locked><DCIdx>1</DCId" +
				"x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style14\" " +
				"/><Style parent=\"Style1\" me=\"Style15\" /><FooterStyle parent=\"Style3\" me=\"Style16" +
				"\" /><EditorStyle parent=\"Style5\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1" +
				"\" me=\"Style19\" /><GroupFooterStyle parent=\"Style1\" me=\"Style18\" /><Visible>True<" +
				"/Visible><ColumnDivider>DarkGray,None</ColumnDivider><Width>195</Width><Height>1" +
				"5</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 524" +
				", 313</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Sp" +
				"lits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Head" +
				"ing\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption" +
				"\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\"" +
				" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\"" +
				" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><S" +
				"tyle parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar" +
				"\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits" +
				"><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</Defa" +
				"ultRecSelWidth><ClientArea>0, 0, 524, 313</ClientArea><PrintPageHeaderStyle pare" +
				"nt=\"\" me=\"Style28\" /><PrintPageFooterStyle parent=\"\" me=\"Style29\" /></Blob>";
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.SendToMenuItem,
																																										this.Sep2MenuItem,
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
			// Sep2MenuItem
			// 
			this.Sep2MenuItem.Index = 1;
			this.Sep2MenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 2;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// AdminKeyValuesForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(528, 317);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.AdminKeyValuesGrid);
			this.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AdminKeyValuesForm";
			this.Text = "Admin - Key Values";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.AdminKeyValues_Closing);
			this.Load += new System.EventHandler(this.AdminKeyValues_Load);
			((System.ComponentModel.ISupportInitialize)(this.AdminKeyValuesGrid)).EndInit();
			this.ResumeLayout(false);

		}
    #endregion

    private void AdminKeyValues_Load(object sender, System.EventArgs e)
    {
      int height = mainForm.Height / 2;
      int width  = mainForm.Width / 2;
      
      try
      {
        this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
        this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
        this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
        this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));
        
        AdminKeyValuesGrid.SetDataBinding(mainForm.ServiceAgent.KeyValueGet(), "KeyValues", true);
      }
      catch(Exception ee)
      {
        mainForm.Alert(ee.Message, PilotState.RunFault);        
      }
    }

    private void AdminKeyValues_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if(this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
        RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
        RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
        RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
      }
    }

    private void AdminKeyValuesGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
      try
      {
        mainForm.ServiceAgent.KeyValueSet(
          AdminKeyValuesGrid.Columns["KeyId"].Text, 
          AdminKeyValuesGrid.Columns["KeyValue"].Text
          );
      
        mainForm.Alert("Key '"+ AdminKeyValuesGrid.Columns["KeyId"].Text
          + "' was updated with this value: " + AdminKeyValuesGrid.Columns["KeyValue"].Text, PilotState.Normal);
      }
      catch(Exception ee)
      {
        mainForm.Alert(ee.Message, PilotState.RunFault);
        e.Cancel = true;
        return;
      }
    }

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in AdminKeyValuesGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\r\n";

			foreach (int row in AdminKeyValuesGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in AdminKeyValuesGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\r\n";
			}

			Clipboard.SetDataObject(gridData, true);

			mainForm.Alert("Total: " + AdminKeyValuesGrid.SelectedRows.Count + " items copied to the clipboard.", PilotState.Normal);
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n";

			if (AdminKeyValuesGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows to copy.", PilotState.Normal);
				return;
			}

			try
			{
				maxTextLength = new int[AdminKeyValuesGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in AdminKeyValuesGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in AdminKeyValuesGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in AdminKeyValuesGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in AdminKeyValuesGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in AdminKeyValuesGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in AdminKeyValuesGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in AdminKeyValuesGrid.SelectedCols)
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

				mainForm.Alert("Total: " + AdminKeyValuesGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
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
			excel.ExportGridToExcel(ref AdminKeyValuesGrid);

			this.Cursor = Cursors.Default;
		}

		private void AdminKeyValuesGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			string gridData = "";

			if (e.KeyChar.Equals((char)3) && AdminKeyValuesGrid.SelectedRows.Count > 0)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in AdminKeyValuesGrid.SelectedCols)
				{
					gridData += dataColumn.Caption + "\t";
				}

				gridData += "\n";

				foreach (int row in AdminKeyValuesGrid.SelectedRows)
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in AdminKeyValuesGrid.SelectedCols)
					{
						gridData += dataColumn.CellText(row) + "\t";
					}

					gridData += "\n";

					if ((row % 100) == 0)
					{
						mainForm.Alert("Please wait: " + row.ToString("#,##0") + " rows copied so far...");
					}
				}

				Clipboard.SetDataObject(gridData, true);
				mainForm.Alert("Copied " + AdminKeyValuesGrid.SelectedRows.Count.ToString("#,##0") + " rows to the clipboard.");
				e.Handled = true;
			}
		}
  }
}
