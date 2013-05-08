// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using C1.C1PrintDocument.Export;
using C1.C1Pdf;
using Anetics.Common;


namespace Anetics.Medalist
{
	public class ShortSaleBillingBillsAccountsStatusForm : System.Windows.Forms.Form
	{
		private MainForm mainForm;
		private string groupCode;
		private DataSet dataSet = null;		
		private System.Windows.Forms.Panel BackgroundPanel;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid EntriesGrid;
		private System.Windows.Forms.MenuItem SendToLedgerMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem SendToMailRecipientMenuItem;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private C1.Win.C1Input.C1Label BizDateLabel;
		private System.Windows.Forms.DateTimePicker BuisnessDatePicker;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.Windows.Forms.Button RefreshButton;
		private System.ComponentModel.IContainer components;
    
		public ShortSaleBillingBillsAccountsStatusForm(MainForm mainForm, string groupCode)
		{    
			InitializeComponent();     
    
			try
			{  
				this.groupCode = groupCode;
				this.mainForm = mainForm;                          
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleBillingBillsAccountsStatusForm.ShortSaleBillingBillsAccountsStatusForm]", Log.Error, 1);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleBillingBillsAccountsStatusForm));
			this.BackgroundPanel = new System.Windows.Forms.Panel();
			this.EntriesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToLedgerMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToMailRecipientMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.BizDateLabel = new C1.Win.C1Input.C1Label();
			this.BuisnessDatePicker = new System.Windows.Forms.DateTimePicker();
			this.RefreshButton = new System.Windows.Forms.Button();
			this.BackgroundPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.EntriesGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BizDateLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// BackgroundPanel
			// 
			this.BackgroundPanel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.BackgroundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.BackgroundPanel.Controls.Add(this.EntriesGrid);
			this.BackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BackgroundPanel.DockPadding.All = 1;
			this.BackgroundPanel.Location = new System.Drawing.Point(1, 30);
			this.BackgroundPanel.Name = "BackgroundPanel";
			this.BackgroundPanel.Size = new System.Drawing.Size(712, 448);
			this.BackgroundPanel.TabIndex = 59;
			// 
			// EntriesGrid
			// 
			this.EntriesGrid.AllowUpdate = false;
			this.EntriesGrid.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.EntriesGrid.CaptionHeight = 17;
			this.EntriesGrid.ContextMenu = this.MainContextMenu;
			this.EntriesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.EntriesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.EntriesGrid.EmptyRows = true;
			this.EntriesGrid.ExtendRightColumn = true;
			this.EntriesGrid.FilterBar = true;
			this.EntriesGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.EntriesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.EntriesGrid.Location = new System.Drawing.Point(1, 1);
			this.EntriesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.EntriesGrid.Name = "EntriesGrid";
			this.EntriesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.EntriesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.EntriesGrid.PreviewInfo.ZoomFactor = 75;
			this.EntriesGrid.RecordSelectorWidth = 16;
			this.EntriesGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.EntriesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.EntriesGrid.RowHeight = 15;
			this.EntriesGrid.RowSubDividerColor = System.Drawing.Color.LightGray;
			this.EntriesGrid.Size = new System.Drawing.Size(708, 444);
			this.EntriesGrid.TabIndex = 0;
			this.EntriesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.EntriesGrid_FormatText);
			this.EntriesGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Group Code\"" +
				" DataField=\"GroupCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn L" +
				"evel=\"0\" Caption=\"Account Number\" DataField=\"AccountNumber\"><ValueItems /><Group" +
				"Info /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Actor\" DataField=\"ActUser" +
				"Id\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"A" +
				"ct Time\" DataField=\"ActTime\" NumberFormat=\"FormatText Event\"><ValueItems /><Grou" +
				"pInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Charge\" DataField=\"Amoun" +
				"t\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"S\"" +
				" DataField=\"Status\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Leve" +
				"l=\"0\" Caption=\"BizDate\" DataField=\"BizDate\"><ValueItems /><GroupInfo /></C1DataC" +
				"olumn><C1DataColumn Level=\"0\" Caption=\"Index\" DataField=\"Index\"><ValueItems /><G" +
				"roupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.Co" +
				"ntextWrapper\"><Data>Style58{AlignHorz:Near;}Style59{AlignHorz:Near;}RecordSelect" +
				"or{AlignImage:Center;}Style50{}Style51{}Style52{AlignHorz:Near;}Style53{AlignHor" +
				"z:Near;}Style54{}Caption{AlignHorz:Center;}Style56{}Normal{Font:Verdana, 8.25pt;" +
				"}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style18{BackColor" +
				":255, 251, 242;}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}Style17{Align" +
				"Horz:Near;BackColor:Snow;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Sty" +
				"le47{AlignHorz:Near;BackColor:255, 251, 242;}Style63{}Style62{}Style61{}Style60{" +
				"}Style38{}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style37{}Sty" +
				"le34{AlignHorz:Center;}Style35{AlignHorz:Center;BackColor:GhostWhite;}OddRow{}St" +
				"yle29{AlignHorz:Far;BackColor:253, 253, 253;}Style28{AlignHorz:Near;}Style27{}St" +
				"yle26{}Style25{}Footer{Locked:True;Border:Flat,ControlDark,1, 1, 1, 1;BackColor:" +
				"255, 251, 242;}Style23{AlignHorz:Near;BackColor:Snow;}Style22{AlignHorz:Near;}St" +
				"yle21{}Style55{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Ce" +
				"nter;}Style57{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;" +
				"}EvenRow{BackColor:Aqua;}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1," +
				" 1, 1;ForeColor:ControlText;BackColor:Control;}Style49{}Style48{}Style24{}Style9" +
				"{}Style6{}Style1{}Style20{}Style3{}Style41{AlignHorz:Near;BackColor:255, 251, 24" +
				"2;}Style40{AlignHorz:Near;}Style43{}FilterBar{BackColor:SeaShell;}Style42{}Style" +
				"45{}Style44{}Style46{AlignHorz:Near;}Style8{}Style39{}Style36{}Style5{}Style4{}S" +
				"tyle7{}Style32{}Style33{}Style30{}Style31{}Style2{}</Data></Styles><Splits><C1.W" +
				"in.C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" Co" +
				"lumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=\"Do" +
				"ttedCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup" +
				"=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><Edi" +
				"torStyle parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8" +
				"\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Foote" +
				"r\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=" +
				"\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><" +
				"InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"S" +
				"tyle9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedSt" +
				"yle parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><intern" +
				"alCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style52\" /><Style pare" +
				"nt=\"Style1\" me=\"Style53\" /><FooterStyle parent=\"Style3\" me=\"Style54\" /><EditorSt" +
				"yle parent=\"Style5\" me=\"Style55\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style57" +
				"\" /><GroupFooterStyle parent=\"Style1\" me=\"Style56\" /><ColumnDivider>DarkGray,Sin" +
				"gle</ColumnDivider><Height>15</Height><DCIdx>6</DCIdx></C1DisplayColumn><C1Displ" +
				"ayColumn><HeadingStyle parent=\"Style2\" me=\"Style58\" /><Style parent=\"Style1\" me=" +
				"\"Style59\" /><FooterStyle parent=\"Style3\" me=\"Style60\" /><EditorStyle parent=\"Sty" +
				"le5\" me=\"Style61\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style63\" /><GroupFoote" +
				"rStyle parent=\"Style1\" me=\"Style62\" /><ColumnDivider>DarkGray,Single</ColumnDivi" +
				"der><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=\"Style1" +
				"9\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</C" +
				"olumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColu" +
				"mn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style parent=\"Style1\" me=\"Style" +
				"23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><EditorStyle parent=\"Style5\" m" +
				"e=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style27\" /><GroupFooterStyle" +
				" parent=\"Style1\" me=\"Style26\" /><Visible>True</Visible><ColumnDivider>LightGray," +
				"Single</ColumnDivider><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1Di" +
				"splayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" " +
				"me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"" +
				"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFo" +
				"oterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>L" +
				"ightGray,Single</ColumnDivider><Width>111</Width><Height>15</Height><DCIdx>4</DC" +
				"Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34" +
				"\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style" +
				"36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Styl" +
				"e1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>Tru" +
				"e</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>30</Width><Heig" +
				"ht>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle p" +
				"arent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle" +
				" parent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><Gro" +
				"upHeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" m" +
				"e=\"Style44\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivi" +
				"der><Height>15</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><Headi" +
				"ngStyle parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><Fo" +
				"oterStyle parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style4" +
				"9\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"" +
				"Style1\" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</C" +
				"olumnDivider><Height>15</Height><DCIdx>3</DCIdx></C1DisplayColumn></internalCols" +
				"><ClientRect>0, 0, 704, 440</ClientRect><BorderSide>0</BorderSide></C1.Win.C1Tru" +
				"eDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style pa" +
				"rent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent" +
				"=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=" +
				"\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Nor" +
				"mal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"No" +
				"rmal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=" +
				"\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><ve" +
				"rtSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><Defau" +
				"ltRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 704, 440</ClientArea><Pri" +
				"ntPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"S" +
				"tyle15\" /></Blob>";
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.SendToMenuItem,
																																										this.menuItem1,
																																										this.ExitMenuItem});
			// 
			// SendToMenuItem
			// 
			this.SendToMenuItem.Index = 0;
			this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																									 this.SendToLedgerMenuItem,
																																									 this.SendToExcelMenuItem,
																																									 this.SendToMailRecipientMenuItem});
			this.SendToMenuItem.Text = "Send To";
			// 
			// SendToLedgerMenuItem
			// 
			this.SendToLedgerMenuItem.Index = 0;
			this.SendToLedgerMenuItem.Text = "Ledger";
			this.SendToLedgerMenuItem.Click += new System.EventHandler(this.SendToLedgerMenuItem_Click);
			// 
			// SendToExcelMenuItem
			// 
			this.SendToExcelMenuItem.Index = 1;
			this.SendToExcelMenuItem.Text = "Excel";
			this.SendToExcelMenuItem.Click += new System.EventHandler(this.SendToExcelMenuItem_Click);
			// 
			// SendToMailRecipientMenuItem
			// 
			this.SendToMailRecipientMenuItem.Enabled = false;
			this.SendToMailRecipientMenuItem.Index = 2;
			this.SendToMailRecipientMenuItem.Text = "Mail Recipient";
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 2;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click_1);
			// 
			// BizDateLabel
			// 
			this.BizDateLabel.Location = new System.Drawing.Point(4, 4);
			this.BizDateLabel.Name = "BizDateLabel";
			this.BizDateLabel.Size = new System.Drawing.Size(92, 18);
			this.BizDateLabel.TabIndex = 60;
			this.BizDateLabel.Tag = null;
			this.BizDateLabel.Text = "Business Date:";
			this.BizDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BizDateLabel.TextDetached = true;
			// 
			// BuisnessDatePicker
			// 
			this.BuisnessDatePicker.Location = new System.Drawing.Point(96, 3);
			this.BuisnessDatePicker.Name = "BuisnessDatePicker";
			this.BuisnessDatePicker.Size = new System.Drawing.Size(216, 21);
			this.BuisnessDatePicker.TabIndex = 62;
			this.BuisnessDatePicker.ValueChanged += new System.EventHandler(this.BuisnessDatePicker_ValueChanged);
			// 
			// RefreshButton
			// 
			this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RefreshButton.Location = new System.Drawing.Point(632, 4);
			this.RefreshButton.Name = "RefreshButton";
			this.RefreshButton.TabIndex = 63;
			this.RefreshButton.Text = "&Refresh";
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// ShortSaleBillingBillsAccountsStatusForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(714, 479);
			this.Controls.Add(this.RefreshButton);
			this.Controls.Add(this.BuisnessDatePicker);
			this.Controls.Add(this.BizDateLabel);
			this.Controls.Add(this.BackgroundPanel);
			this.DockPadding.Bottom = 1;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 30;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ShortSaleBillingBillsAccountsStatusForm";
			this.Text = "ShortSale - Billing - Bills - Accounts Status";
			this.Load += new System.EventHandler(this.ShortSaleBillingBillsAccountsStatusForm_Load);
			this.Closed += new System.EventHandler(this.ShortSaleBillingBillsAccountsStatusForm_Closed);
			this.BackgroundPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.EntriesGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BizDateLabel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

    

		private void ShortSaleBillingBillsAccountsStatusForm_Load(object sender, System.EventArgs e)
		{       
			this.WindowState = FormWindowState.Normal;
      
			try
			{			
				//dataSet = mainForm.RebateAgent.ShortSaleBillingSumaryGeneralLedgerEntriesGet(mainForm.ServiceAgent.BizDate(), groupCode, "", mainForm.UtcOffset);						
				//EntriesGrid.SetDataBinding(dataSet,  "GeneralLedgerEntries", true);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleBillingBillsAccountsStatusForm.ShortSaleBillingBillsAccountsStatusForm_Load]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}
	
		private void ShortSaleBillingBillsAccountsStatusForm_Closed(object sender, System.EventArgs e)
		{
			mainForm.Refresh();
		}

		private void EntriesGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "Amount":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.00");
					}
					catch {}
					break;

				case "ActTime":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeFileFormat);
					}
					catch {}
					break;
			}
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			Excel excel = new Excel();

			excel.ExportGridToExcel(ref EntriesGrid);
		}

		private void SendToLedgerMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{				
				/*foreach (int row in EntriesGrid.SelectedRows)
				{
					if (!EntriesGrid.Columns["Status"].CellText(row).Equals("S"))
					{
						mainForm.RebateAgent.ShortSaleBillingSumaryGeneralLedgerEntryStatusSet(
							EntriesGrid.Columns["Index"].CellText(row),
							EntriesGrid.Columns["BizDate"].CellText(row),
							EntriesGrid.Columns["GroupCode"].CellText(row),
							EntriesGrid.Columns["AccountNumber"].CellText(row),
							"S",
							mainForm.UserId);
					
						EntriesGrid[row, "Status"] = "S";
						EntriesGrid[row, "ActUserId"] = mainForm.UserId;
						EntriesGrid[row, "ActTime"] = DateTime.Now.ToString(Standard.DateTimeFormat);
					}
				}*/
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void BuisnessDatePicker_ValueChanged(object sender, System.EventArgs e)
		{
			try
			{
				//dataSet = mainForm.RebateAgent.ShortSaleBillingSumaryGeneralLedgerEntriesGet(BuisnessDatePicker.Text, groupCode, "", mainForm.UtcOffset);						
				//EntriesGrid.SetDataBinding(dataSet,  "GeneralLedgerEntries", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void ExitMenuItem_Click_1(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void RefreshButton_Click(object sender, System.EventArgs e)
		{
			try
			{
			//	dataSet = mainForm.RebateAgent.ShortSaleBillingSumaryGeneralLedgerEntriesGet(BuisnessDatePicker.Text, groupCode, "", mainForm.UtcOffset);						
			//	EntriesGrid.SetDataBinding(dataSet,  "GeneralLedgerEntries", true);
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}
	}
}
