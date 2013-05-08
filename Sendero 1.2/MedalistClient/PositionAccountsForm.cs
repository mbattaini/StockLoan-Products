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
  public class PositionAccountsForm : System.Windows.Forms.Form
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
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid PositionAccountsGrid;

    public PositionAccountsForm(MainForm mainForm)
    {
      InitializeComponent();
      this.mainForm = mainForm;
			
			dataSet = new DataSet();

			try
			{
				PositionAccountsGrid.AllowUpdate = mainForm.AdminAgent.MayEdit(mainForm.UserId,"PositionAccounts");
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionAccountsForm));
			this.PositionAccountsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.PositionAccountsGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// PositionAccountsGrid
			// 
			this.PositionAccountsGrid.CaptionHeight = 17;
			this.PositionAccountsGrid.ContextMenu = this.MainContextMenu;
			this.PositionAccountsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PositionAccountsGrid.ExtendRightColumn = true;
			this.PositionAccountsGrid.FetchRowStyles = true;
			this.PositionAccountsGrid.FilterBar = true;
			this.PositionAccountsGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.PositionAccountsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.PositionAccountsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.PositionAccountsGrid.Location = new System.Drawing.Point(0, 0);
			this.PositionAccountsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.PositionAccountsGrid.Name = "PositionAccountsGrid";
			this.PositionAccountsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.PositionAccountsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.PositionAccountsGrid.PreviewInfo.ZoomFactor = 75;
			this.PositionAccountsGrid.RecordSelectorWidth = 16;
			this.PositionAccountsGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.PositionAccountsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.PositionAccountsGrid.RowHeight = 16;
			this.PositionAccountsGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.PositionAccountsGrid.Size = new System.Drawing.Size(496, 293);
			this.PositionAccountsGrid.TabIndex = 0;
			this.PositionAccountsGrid.Text = "Accounts";
			this.PositionAccountsGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.PositionAccountsForm_BeforeUpdate);
			this.PositionAccountsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.PositionAccountsGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PositionAccountsGrid_KeyPress);
			this.PositionAccountsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Account\" Da" +
				"taField=\"AccountNumber\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
				"Level=\"0\" Caption=\"Firm\" DataField=\"Firm\"><ValueItems /><GroupInfo /></C1DataCol" +
				"umn><C1DataColumn Level=\"0\" Caption=\"ActUserId\" DataField=\"ActUserId\"><ValueItem" +
				"s /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Act Time\" DataF" +
				"ield=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1Da" +
				"taColumn><C1DataColumn Level=\"0\" Caption=\"LocMemo\" DataField=\"LocMemo\" NumberFor" +
				"mat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"AccountType\" DataField=\"AccountType\" NumberFormat=\"FormatText " +
				"Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption" +
				"=\"IsActive\" DataField=\"IsActive\"><ValueItems /><GroupInfo /></C1DataColumn><C1Da" +
				"taColumn Level=\"0\" Caption=\"A\" DataField=\"IsActive\"><ValueItems Presentation=\"Ch" +
				"eckBox\" /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"CurrencyC" +
				"ode\" DataField=\"CurrencyCode\"><ValueItems /><GroupInfo /></C1DataColumn></DataCo" +
				"ls><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{F" +
				"oreColor:HighlightText;BackColor:Highlight;}Inactive{ForeColor:InactiveCaptionTe" +
				"xt;BackColor:InactiveCaption;}Selected{ForeColor:HighlightText;BackColor:Highlig" +
				"ht;}Editor{}Style70{}Style71{}FilterBar{BackColor:SeaShell;}Heading{Wrap:True;Ba" +
				"ckColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center" +
				";}Style18{}Style19{}Style14{AlignHorz:Near;}Style15{Font:Verdana, 8.25pt, style=" +
				"Bold;AlignHorz:Near;ForeColor:Highlight;}Style16{}Style17{}Style10{AlignHorz:Nea" +
				"r;}Style11{}Style12{}Style13{}Style27{}Style29{}Style28{}Style26{}Style25{}Style" +
				"9{}Style8{}Style24{}Style23{}Style5{}Style4{}Style7{}Style6{}Style1{}Style22{}St" +
				"yle3{}Style2{}Style21{AlignHorz:Near;}Style20{AlignHorz:Near;}OddRow{}Style38{}S" +
				"tyle39{}Style36{AlignHorz:Near;}Style37{Font:Verdana, 8.25pt, style=Bold;AlignHo" +
				"rz:Far;}Style34{}Style35{}Style32{}Style33{}Style30{AlignHorz:Near;}Style49{Alig" +
				"nHorz:Near;}Style48{AlignHorz:Near;}Style31{Font:Verdana, 8.25pt, style=Bold;Ali" +
				"gnHorz:Far;ForeColor:HotTrack;}Normal{Font:Verdana, 8.25pt;}Style41{}Style40{}St" +
				"yle43{AlignHorz:Near;}Style42{AlignHorz:Near;}Style45{}Style44{}Style47{}Style46" +
				"{}EvenRow{BackColor:Aqua;}Style59{}Style58{}RecordSelector{AlignImage:Center;}St" +
				"yle51{}Style50{}Footer{}Style52{}Style53{}Style54{AlignHorz:Near;}Style55{AlignH" +
				"orz:Near;}Style56{}Style57{}Caption{AlignHorz:Center;}Style69{}Style68{}Style63{" +
				"}Style62{}Style61{AlignHorz:Center;}Style60{AlignHorz:Center;AlignImage:Center;}" +
				"Style67{AlignHorz:Near;}Style66{AlignHorz:Near;}Style65{}Style64{}Group{AlignVer" +
				"t:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}</Data></Styles><Splits>" +
				"<C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" Name=\"\" Capti" +
				"onHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn" +
				"=\"True\" FetchRowStyles=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedRowBorder\" Re" +
				"cordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScr" +
				"ollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"E" +
				"ditor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyl" +
				"e parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><" +
				"GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Styl" +
				"e2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle pare" +
				"nt=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSe" +
				"lectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Select" +
				"ed\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayC" +
				"olumn><HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent=\"Style1\" me=\"St" +
				"yle15\" /><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"Style5" +
				"\" me=\"Style17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><GroupFooterSt" +
				"yle parent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><ColumnDivider>Gainsbo" +
				"ro,Single</ColumnDivider><Width>149</Width><Height>16</Height><Locked>True</Lock" +
				"ed><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Styl" +
				"e2\" me=\"Style20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"St" +
				"yle3\" me=\"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderSty" +
				"le parent=\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\"" +
				" /><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>240</Width><Height>16</" +
				"Height><Locked>True</Locked><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style2\" me=\"Style30\" /><Style parent=\"Style1\" me=\"Style31\" " +
				"/><FooterStyle parent=\"Style3\" me=\"Style32\" /><EditorStyle parent=\"Style5\" me=\"S" +
				"tyle33\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style35\" /><GroupFooterStyle par" +
				"ent=\"Style1\" me=\"Style34\" /><ColumnDivider>DarkGray,Single</ColumnDivider><Width" +
				">130</Width><Height>16</Height><Locked>True</Locked><DCIdx>4</DCIdx></C1DisplayC" +
				"olumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style36\" /><Style paren" +
				"t=\"Style1\" me=\"Style37\" /><FooterStyle parent=\"Style3\" me=\"Style38\" /><EditorSty" +
				"le parent=\"Style5\" me=\"Style39\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style41\"" +
				" /><GroupFooterStyle parent=\"Style1\" me=\"Style40\" /><ColumnDivider>DarkGray,Sing" +
				"le</ColumnDivider><Width>130</Width><Height>16</Height><Locked>True</Locked><DCI" +
				"dx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=" +
				"\"Style48\" /><Style parent=\"Style1\" me=\"Style49\" /><FooterStyle parent=\"Style3\" m" +
				"e=\"Style50\" /><EditorStyle parent=\"Style5\" me=\"Style51\" /><GroupHeaderStyle pare" +
				"nt=\"Style1\" me=\"Style53\" /><GroupFooterStyle parent=\"Style1\" me=\"Style52\" /><Vis" +
				"ible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Height>16</He" +
				"ight><Locked>True</Locked><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style2\" me=\"Style60\" /><Style parent=\"Style1\" me=\"Style61\" />" +
				"<FooterStyle parent=\"Style3\" me=\"Style62\" /><EditorStyle parent=\"Style5\" me=\"Sty" +
				"le63\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style65\" /><GroupFooterStyle paren" +
				"t=\"Style1\" me=\"Style64\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single<" +
				"/ColumnDivider><Width>20</Width><Height>16</Height><DCIdx>7</DCIdx></C1DisplayCo" +
				"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style54\" /><Style parent" +
				"=\"Style1\" me=\"Style55\" /><FooterStyle parent=\"Style3\" me=\"Style56\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style57\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style59\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style58\" /><Visible>True</Visible><Colum" +
				"nDivider>Gainsboro,Single</ColumnDivider><Height>16</Height><Locked>True</Locked" +
				"><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2" +
				"\" me=\"Style42\" /><Style parent=\"Style1\" me=\"Style43\" /><FooterStyle parent=\"Styl" +
				"e3\" me=\"Style44\" /><EditorStyle parent=\"Style5\" me=\"Style45\" /><GroupHeaderStyle" +
				" parent=\"Style1\" me=\"Style47\" /><GroupFooterStyle parent=\"Style1\" me=\"Style46\" /" +
				"><ColumnDivider>DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>6</DCId" +
				"x></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style66\" " +
				"/><Style parent=\"Style1\" me=\"Style67\" /><FooterStyle parent=\"Style3\" me=\"Style68" +
				"\" /><EditorStyle parent=\"Style5\" me=\"Style69\" /><GroupHeaderStyle parent=\"Style1" +
				"\" me=\"Style71\" /><GroupFooterStyle parent=\"Style1\" me=\"Style70\" /><ColumnDivider" +
				">DarkGray,Single</ColumnDivider><Height>16</Height><DCIdx>8</DCIdx></C1DisplayCo" +
				"lumn></internalCols><ClientRect>0, 0, 492, 289</ClientRect><BorderSide>0</Border" +
				"Side></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"" +
				"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Foot" +
				"er\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactiv" +
				"e\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /" +
				"><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" " +
				"/><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelecto" +
				"r\" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" " +
				"/></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modi" +
				"fied</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 492, 2" +
				"89</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style28\" /><PrintPageFooterSt" +
				"yle parent=\"\" me=\"Style29\" /></Blob>";
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
			// PositionAccountsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(496, 293);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.PositionAccountsGrid);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PositionAccountsForm";
			this.Text = "Position - Accounts";
			this.Load += new System.EventHandler(this.PositionAccountsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.PositionAccountsGrid)).EndInit();
			this.ResumeLayout(false);

		}
    #endregion

    private void PositionAccountsForm_Load(object sender, System.EventArgs e)
    {
      int height = mainForm.Height / 2;
      int width  = mainForm.Width / 2;
      
      try
      {
        this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
        this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
        this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
        this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));
        
        dataSet = mainForm.PositionAgent.AccountsGet(mainForm.UtcOffset);
				
				PositionAccountsGrid.SetDataBinding(dataSet, "Accounts", true);								
			}
      catch(Exception ee)
      {
        mainForm.Alert(ee.Message, PilotState.RunFault);        
      }
    }

    private void PositionAccountsForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if(this.WindowState.Equals(FormWindowState.Normal))
      {
        RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
        RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
        RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
        RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
      }
    }

    private void PositionAccountsForm_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
    {
			try
			{
				mainForm.PositionAgent.AccountSet(
					PositionAccountsGrid.Columns["Firm"].Text,
					PositionAccountsGrid.Columns["LocMemo"].Text,
					PositionAccountsGrid.Columns["AccountType"].Text,
					PositionAccountsGrid.Columns["AccountNumber"].Text.Trim(),
					PositionAccountsGrid.Columns["CurrencyCode"].Text.Trim(),
					mainForm.UserId,
					bool.Parse(PositionAccountsGrid.Columns["IsActive"].Value.ToString()));
							
				PositionAccountsGrid.Columns["ActUserId"].Text = mainForm.UserId;
				PositionAccountsGrid.Columns["ActTime"].Value = DateTime.Now;       							
			}
			catch(Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);
				e.Cancel = true;
				return;
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

		private void PositionAccountsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar.Equals((char) 13))
			{
				PositionAccountsGrid.UpdateData();
			}
		}

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionAccountsGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\r\n";

			foreach (int row in PositionAccountsGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionAccountsGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\r\n";
			}

			Clipboard.SetDataObject(gridData, true);

			mainForm.Alert("Total: " + PositionAccountsGrid.SelectedRows.Count + " items copied to the clipboard.", PilotState.Normal);
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n";

			if (PositionAccountsGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows to copy.", PilotState.Normal);
				return;
			}

			try
			{
				maxTextLength = new int[PositionAccountsGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionAccountsGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in PositionAccountsGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionAccountsGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionAccountsGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionAccountsGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in PositionAccountsGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in PositionAccountsGrid.SelectedCols)
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

				mainForm.Alert("Total: " + PositionAccountsGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
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
			excel.ExportGridToExcel(ref PositionAccountsGrid);

			this.Cursor = Cursors.Default;
		}

	}
}
