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
	public class PositionDeficitBuyInsAccountsBorrowedForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
    
		private MainForm mainForm;
		private DataSet dataSet = null;
		private	DataView dataView = null;
		private string secId = "";

		private C1.Win.C1Input.C1Label c1Label1;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep2MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.Windows.Forms.DateTimePicker ForDatePicker;
		private C1.Win.C1Input.C1Label c1Label3;
		private C1.Win.C1Input.C1Label AccountsBorrowedLoadTimeLabel;		
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid PositionAccountsGrid;

		public PositionDeficitBuyInsAccountsBorrowedForm(MainForm mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;
		
			try
			{

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PositionDeficitBuyInsAccountsBorrowedForm));
			this.PositionAccountsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.ForDatePicker = new System.Windows.Forms.DateTimePicker();
			this.c1Label1 = new C1.Win.C1Input.C1Label();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.AccountsBorrowedLoadTimeLabel = new C1.Win.C1Input.C1Label();
			this.c1Label3 = new C1.Win.C1Input.C1Label();
			((System.ComponentModel.ISupportInitialize)(this.PositionAccountsGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.AccountsBorrowedLoadTimeLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label3)).BeginInit();
			this.SuspendLayout();
			// 
			// PositionAccountsGrid
			// 
			this.PositionAccountsGrid.AllowUpdate = false;
			this.PositionAccountsGrid.CaptionHeight = 17;
			this.PositionAccountsGrid.ColumnFooters = true;
			this.PositionAccountsGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
			this.PositionAccountsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PositionAccountsGrid.EmptyRows = true;
			this.PositionAccountsGrid.ExtendRightColumn = true;
			this.PositionAccountsGrid.FetchRowStyles = true;
			this.PositionAccountsGrid.FilterBar = true;
			this.PositionAccountsGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.PositionAccountsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.PositionAccountsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.PositionAccountsGrid.Location = new System.Drawing.Point(1, 35);
			this.PositionAccountsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.PositionAccountsGrid.Name = "PositionAccountsGrid";
			this.PositionAccountsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.PositionAccountsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.PositionAccountsGrid.PreviewInfo.ZoomFactor = 75;
			this.PositionAccountsGrid.RecordSelectorWidth = 16;
			this.PositionAccountsGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.PositionAccountsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.PositionAccountsGrid.RowHeight = 16;
			this.PositionAccountsGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.PositionAccountsGrid.Size = new System.Drawing.Size(1214, 777);
			this.PositionAccountsGrid.TabIndex = 0;
			this.PositionAccountsGrid.Text = "Accounts";
			this.PositionAccountsGrid.AfterFilter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.PositionAccountsGrid_AfterFilter);
			this.PositionAccountsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.PositionAccountsGrid_FormatText);
			this.PositionAccountsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Corresponde" +
				"nt\" DataField=\"GroupCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColum" +
				"n Level=\"0\" Caption=\"Account Number\" DataField=\"AccountNumber\"><ValueItems /><Gr" +
				"oupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Security ID\" DataField" +
				"=\"SecId\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capti" +
				"on=\"\" DataField=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
				" Level=\"0\" Caption=\"Quantity Covered\" DataField=\"QuantityCovered\" NumberFormat=\"" +
				"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=" +
				"\"0\" Caption=\"Quantity Shorted\" DataField=\"QuantityShorted\" NumberFormat=\"FormatT" +
				"ext Event\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1" +
				".Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Style50{Border:Raised,,1, 1, 1, 1" +
				";}Style51{}Style52{}Style53{}Caption{AlignHorz:Center;}Normal{Font:Verdana, 8.25" +
				"pt;}Style25{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style" +
				"18{}Style19{}Style14{AlignHorz:Near;}Style15{AlignHorz:Near;BackColor:Snow;}Styl" +
				"e16{Border:Raised,,1, 1, 1, 1;}Style17{}Style10{AlignHorz:Near;}Style11{}OddRow{" +
				"}Style13{}Style46{}Style38{Border:Raised,,1, 1, 1, 1;}Style2{}Style37{AlignHorz:" +
				"Near;BackColor:GhostWhite;}Style35{}Style32{Border:Raised,,1, 1, 1, 1;}Style33{}" +
				"Style4{}Style31{AlignHorz:Near;BackColor:GhostWhite;}Style29{}Style28{}Style27{}" +
				"Style26{}RecordSelector{AlignImage:Center;}Footer{Locked:True;Font:Verdana, 8.25" +
				"pt, style=Bold;BackColor:255, 251, 242;}Style23{}Style22{Border:Raised,,1, 1, 1," +
				" 1;}Style21{AlignHorz:Near;BackColor:Snow;}Style20{AlignHorz:Near;}Group{AlignVe" +
				"rt:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Inactive{ForeColor:Inac" +
				"tiveCaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wrap:" +
				"True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert" +
				":Center;}Style49{AlignHorz:Far;BackColor:251, 251, 255;}Style48{AlignHorz:Near;}" +
				"Style24{}Style9{}Style5{}Style41{}Style40{}Style43{AlignHorz:Far;BackColor:251, " +
				"251, 255;}FilterBar{BackColor:SeaShell;}Style42{AlignHorz:Near;}Style44{}Style47" +
				"{}Style45{}Style8{}Style39{}Style36{AlignHorz:Near;}Style12{}Style34{}Style7{}St" +
				"yle6{}Style1{}Style30{AlignHorz:Near;}Style3{}HighlightRow{ForeColor:HighlightTe" +
				"xt;BackColor:Highlight;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView H" +
				"BarStyle=\"None\" VBarStyle=\"Always\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeigh" +
				"t=\"17\" ColumnFooterHeight=\"17\" ExtendRightColumn=\"True\" FetchRowStyles=\"True\" Fi" +
				"lterBar=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSel" +
				"Width=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><CaptionStyle paren" +
				"t=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor\" me=\"Style5\" /><EvenRowSty" +
				"le parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style13" +
				"\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"St" +
				"yle12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=" +
				"\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><Odd" +
				"RowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelectorStyle parent=\"RecordSelect" +
				"or\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" me=\"Style6\" /><Style parent=" +
				"\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Styl" +
				"e2\" me=\"Style30\" /><Style parent=\"Style1\" me=\"Style31\" /><FooterStyle parent=\"St" +
				"yle3\" me=\"Style32\" /><EditorStyle parent=\"Style5\" me=\"Style33\" /><GroupHeaderSty" +
				"le parent=\"Style1\" me=\"Style35\" /><GroupFooterStyle parent=\"Style1\" me=\"Style34\"" +
				" /><Visible>True</Visible><ColumnDivider>Gainsboro,None</ColumnDivider><Height>1" +
				"6</Height><FooterDivider>False</FooterDivider><DCIdx>2</DCIdx></C1DisplayColumn>" +
				"<C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style36\" /><Style parent=\"Sty" +
				"le1\" me=\"Style37\" /><FooterStyle parent=\"Style3\" me=\"Style38\" /><EditorStyle par" +
				"ent=\"Style5\" me=\"Style39\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style41\" /><Gr" +
				"oupFooterStyle parent=\"Style1\" me=\"Style40\" /><Visible>True</Visible><ColumnDivi" +
				"der>Gainsboro,Single</ColumnDivider><Width>63</Width><Height>16</Height><FooterD" +
				"ivider>False</FooterDivider><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style2\" me=\"Style14\" /><Style parent=\"Style1\" me=\"Style15\" " +
				"/><FooterStyle parent=\"Style3\" me=\"Style16\" /><EditorStyle parent=\"Style5\" me=\"S" +
				"tyle17\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style19\" /><GroupFooterStyle par" +
				"ent=\"Style1\" me=\"Style18\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Sing" +
				"le</ColumnDivider><Height>16</Height><FooterDivider>False</FooterDivider><DCIdx>" +
				"0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"St" +
				"yle20\" /><Style parent=\"Style1\" me=\"Style21\" /><FooterStyle parent=\"Style3\" me=\"" +
				"Style22\" /><EditorStyle parent=\"Style5\" me=\"Style23\" /><GroupHeaderStyle parent=" +
				"\"Style1\" me=\"Style25\" /><GroupFooterStyle parent=\"Style1\" me=\"Style24\" /><Visibl" +
				"e>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>120</Width" +
				"><Height>16</Height><FooterDivider>False</FooterDivider><DCIdx>1</DCIdx></C1Disp" +
				"layColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style42\" /><Style p" +
				"arent=\"Style1\" me=\"Style43\" /><FooterStyle parent=\"Style3\" me=\"Style44\" /><Edito" +
				"rStyle parent=\"Style5\" me=\"Style45\" /><GroupHeaderStyle parent=\"Style1\" me=\"Styl" +
				"e47\" /><GroupFooterStyle parent=\"Style1\" me=\"Style46\" /><Visible>True</Visible><" +
				"ColumnDivider>Gainsboro,Single</ColumnDivider><Width>160</Width><Height>16</Heig" +
				"ht><FooterDivider>False</FooterDivider><DCIdx>5</DCIdx></C1DisplayColumn><C1Disp" +
				"layColumn><HeadingStyle parent=\"Style2\" me=\"Style48\" /><Style parent=\"Style1\" me" +
				"=\"Style49\" /><FooterStyle parent=\"Style3\" me=\"Style50\" /><EditorStyle parent=\"St" +
				"yle5\" me=\"Style51\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style53\" /><GroupFoot" +
				"erStyle parent=\"Style1\" me=\"Style52\" /><Visible>True</Visible><ColumnDivider>Gai" +
				"nsboro,Single</ColumnDivider><Width>194</Width><Height>16</Height><FooterDivider" +
				">False</FooterDivider><DCIdx>4</DCIdx></C1DisplayColumn></internalCols><ClientRe" +
				"ct>0, 0, 1210, 773</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.M" +
				"ergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Nor" +
				"mal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading" +
				"\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" " +
				"me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"" +
				"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=" +
				"\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" " +
				"me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>" +
				"1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelW" +
				"idth>16</DefaultRecSelWidth><ClientArea>0, 0, 1210, 773</ClientArea><PrintPageHe" +
				"aderStyle parent=\"\" me=\"Style28\" /><PrintPageFooterStyle parent=\"\" me=\"Style29\" " +
				"/></Blob>";
			// 
			// ForDatePicker
			// 
			this.ForDatePicker.Location = new System.Drawing.Point(40, 6);
			this.ForDatePicker.Name = "ForDatePicker";
			this.ForDatePicker.Size = new System.Drawing.Size(216, 21);
			this.ForDatePicker.TabIndex = 2;
			// 
			// c1Label1
			// 
			this.c1Label1.Location = new System.Drawing.Point(-24, 8);
			this.c1Label1.Name = "c1Label1";
			this.c1Label1.Size = new System.Drawing.Size(64, 16);
			this.c1Label1.TabIndex = 3;
			this.c1Label1.Tag = null;
			this.c1Label1.Text = "For:";
			this.c1Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.c1Label1.TextDetached = true;
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
			// 
			// AccountsBorrowedLoadTimeLabel
			// 
			this.AccountsBorrowedLoadTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AccountsBorrowedLoadTimeLabel.ForeColor = System.Drawing.Color.Navy;
			this.AccountsBorrowedLoadTimeLabel.Location = new System.Drawing.Point(1032, 5);
			this.AccountsBorrowedLoadTimeLabel.Name = "AccountsBorrowedLoadTimeLabel";
			this.AccountsBorrowedLoadTimeLabel.Size = new System.Drawing.Size(176, 23);
			this.AccountsBorrowedLoadTimeLabel.TabIndex = 4;
			this.AccountsBorrowedLoadTimeLabel.Tag = null;
			this.AccountsBorrowedLoadTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.AccountsBorrowedLoadTimeLabel.TextDetached = true;
			// 
			// c1Label3
			// 
			this.c1Label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.c1Label3.Location = new System.Drawing.Point(952, 5);
			this.c1Label3.Name = "c1Label3";
			this.c1Label3.Size = new System.Drawing.Size(72, 23);
			this.c1Label3.TabIndex = 5;
			this.c1Label3.Tag = null;
			this.c1Label3.Text = "Load Time:";
			this.c1Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.c1Label3.TextDetached = true;
			// 
			// PositionDeficitBuyInsAccountsBorrowedForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1216, 813);
			this.ContextMenu = this.MainContextMenu;
			this.Controls.Add(this.c1Label3);
			this.Controls.Add(this.AccountsBorrowedLoadTimeLabel);
			this.Controls.Add(this.c1Label1);
			this.Controls.Add(this.ForDatePicker);
			this.Controls.Add(this.PositionAccountsGrid);
			this.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.DockPadding.Bottom = 1;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 35;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PositionDeficitBuyInsAccountsBorrowedForm";
			this.Text = "Position - Deficit \\ BuyIns - Accounts Borrowed";
			this.Load += new System.EventHandler(this.PositionDeficitBuyInsAccountsBorrowedForm_Load);
			this.Closed += new System.EventHandler(this.PositionDeficitBuyInsAccountsBorrowedForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.PositionAccountsGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.AccountsBorrowedLoadTimeLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.c1Label3)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

	
		private void PositionDeficitBuyInsAccountsBorrowedForm_Load(object sender, System.EventArgs e)
		{
			int height = mainForm.Height / 2;
			int width  = mainForm.Width / 2;
      
			try
			{
				this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
				this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
				this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
				this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));
        							
				dataSet = mainForm.RebateAgent.ShortSaleBillingSummaryGet(ForDatePicker.Text, ForDatePicker.Text, "", "");

				dataView = new DataView(dataSet.Tables["BillingSummary"], "", "SecId ASC", DataViewRowState.CurrentRows);

				PositionAccountsGrid.SetDataBinding(dataView, "", true);
			
				AccountsBorrowedLoadTimeLabel.Text = mainForm.ServiceAgent.KeyValueGet("ShortSaleNegRebateBillingSnapShotDateTime","");

				FooterSet();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void PositionDeficitBuyInsAccountsBorrowedForm_Closed(object sender, System.EventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
				RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
				RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
				RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
			}
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				Excel excel = new Excel();
				excel.ExportGridToExcel(ref PositionAccountsGrid);
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (PositionAccountsGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
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
                
				Clipboard.SetDataObject(gridData, true);
				
				mainForm.Alert("Total: " + PositionAccountsGrid.SelectedRows.Count + " items added to clipboard.");
			}
			catch (Exception error)
			{       				
				mainForm.Alert(error.Message, PilotState.RunFault);
			}		
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (PositionAccountsGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
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

				mainForm.Alert("Total: " + PositionAccountsGrid.SelectedRows.Count + " items added to e-mail.");
			}
			catch (Exception error)
			{       				
				mainForm.Alert(error.Message, PilotState.RunFault);
			}		
		}

		private void PositionAccountsGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch(e.Column.DataField)
			{
				case "QuantityShorted":
					try
					{
						e.Value = long.Parse(PositionAccountsGrid.Columns["QuantityShorted"].CellValue(e.Row).ToString()).ToString("#,##0");
					}
					catch {}
					break;

				case "QuantityCovered":
					try
					{
						e.Value = long.Parse(PositionAccountsGrid.Columns["QuantityCovered"].CellValue(e.Row).ToString()).ToString("#,##0");
					}
					catch {}
					break;
			}
		}

		private void FooterSet()
		{
			long quantityCovered = 0;
		
			for(int index = 0; index < PositionAccountsGrid.Splits[0].Rows.Count; index++)
			{				
				try
				{					
					if (!PositionAccountsGrid.Columns["QuantityCovered"].CellValue(index).ToString().Equals(""))
						quantityCovered += (long) PositionAccountsGrid.Columns["QuantityCovered"].CellValue(index);
				} 
				catch {}	
			}
			
			PositionAccountsGrid.Columns["QuantityCovered"].FooterText = quantityCovered.ToString("#,##0");			
		}

		private void PositionAccountsGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
		{
			FooterSet();
		}

		
	}
}
