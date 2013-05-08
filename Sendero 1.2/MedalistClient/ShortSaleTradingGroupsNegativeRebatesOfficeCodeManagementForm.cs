// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{ 
	public class ShortSaleTradingGroupsNegativeRebatesOfficeCodeManagementForm : System.Windows.Forms.Form
	{
		private DataSet dataSet = null;
		private MainForm mainForm;    
		private string tradingGroup;

		private C1.Win.C1Input.C1Label TradingGroupNameLabel;
		private C1.Win.C1Input.C1Label TradingGroupLabel;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid OfficeCodeGrid;    

		private System.ComponentModel.Container components = null;

		public ShortSaleTradingGroupsNegativeRebatesOfficeCodeManagementForm(MainForm mainForm, string tradingGroup)
		{    
			InitializeComponent();
			this.mainForm = mainForm;         
      
			try
			{     
				this.tradingGroup = tradingGroup;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleTradingGroupsOfficeCodeManagementForm.ShortSaleTradingGroupsOfficeCodeManagementForm]", Log.Error, 1);
			}  
		}
    
		protected override void Dispose( bool disposing )
		{
			if (disposing)
			{
				if (components != null)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleTradingGroupsNegativeRebatesOfficeCodeManagementForm));
			this.TradingGroupNameLabel = new C1.Win.C1Input.C1Label();
			this.TradingGroupLabel = new C1.Win.C1Input.C1Label();
			this.OfficeCodeGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupNameLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.OfficeCodeGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// TradingGroupNameLabel
			// 
			this.TradingGroupNameLabel.Font = new System.Drawing.Font("Verdana", 9F);
			this.TradingGroupNameLabel.ForeColor = System.Drawing.SystemColors.Highlight;
			this.TradingGroupNameLabel.Location = new System.Drawing.Point(112, 8);
			this.TradingGroupNameLabel.Name = "TradingGroupNameLabel";
			this.TradingGroupNameLabel.Size = new System.Drawing.Size(268, 20);
			this.TradingGroupNameLabel.TabIndex = 2;
			this.TradingGroupNameLabel.Tag = null;
			this.TradingGroupNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TradingGroupNameLabel.TextDetached = true;
			// 
			// TradingGroupLabel
			// 
			this.TradingGroupLabel.Font = new System.Drawing.Font("Verdana", 9F);
			this.TradingGroupLabel.ForeColor = System.Drawing.SystemColors.Highlight;
			this.TradingGroupLabel.Location = new System.Drawing.Point(8, 8);
			this.TradingGroupLabel.Name = "TradingGroupLabel";
			this.TradingGroupLabel.Size = new System.Drawing.Size(100, 20);
			this.TradingGroupLabel.TabIndex = 7;
			this.TradingGroupLabel.Tag = null;
			this.TradingGroupLabel.Text = "Trading Group:";
			this.TradingGroupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.TradingGroupLabel.TextDetached = true;
			// 
			// OfficeCodeGrid
			// 
			this.OfficeCodeGrid.AllowAddNew = true;
			this.OfficeCodeGrid.AllowDelete = true;
			this.OfficeCodeGrid.CaptionHeight = 17;
			this.OfficeCodeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OfficeCodeGrid.EmptyRows = true;
			this.OfficeCodeGrid.ExtendRightColumn = true;
			this.OfficeCodeGrid.FilterBar = true;
			this.OfficeCodeGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.OfficeCodeGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.OfficeCodeGrid.Location = new System.Drawing.Point(1, 40);
			this.OfficeCodeGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.OfficeCodeGrid.Name = "OfficeCodeGrid";
			this.OfficeCodeGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.OfficeCodeGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.OfficeCodeGrid.PreviewInfo.ZoomFactor = 75;
			this.OfficeCodeGrid.RecordSelectorWidth = 16;
			this.OfficeCodeGrid.RowDivider.Color = System.Drawing.Color.WhiteSmoke;
			this.OfficeCodeGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.OfficeCodeGrid.RowHeight = 15;
			this.OfficeCodeGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.OfficeCodeGrid.Size = new System.Drawing.Size(492, 282);
			this.OfficeCodeGrid.TabIndex = 9;
			this.OfficeCodeGrid.Text = "c1TrueDBGrid1";
			this.OfficeCodeGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.OfficeCodeGrid_BeforeUpdate);
			this.OfficeCodeGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OfficeCodeGrid_KeyPress);
			this.OfficeCodeGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Office Code" +
				"\" DataField=\"OfficeCode\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn" +
				" Level=\"0\" Caption=\"Positive Rebate MarkUp\" DataField=\"PositiveRebateMarkUp\"><Va" +
				"lueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Negative" +
				" Rebate MarkUp\" DataField=\"NegativeRebateMarkUp\"><ValueItems /><GroupInfo /></C1" +
				"DataColumn><C1DataColumn Level=\"0\" Caption=\"Fed Funds MarkUp\" DataField=\"FedFund" +
				"sMarkUp\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Capti" +
				"on=\"Libor Funds MarkUp\" DataField=\"LiborFundsMarkUp\"><ValueItems /><GroupInfo />" +
				"</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"\" DataField=\"NegativeRebateMarkU" +
				"pCode\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win" +
				".C1TrueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;" +
				"BackColor:Highlight;}Style50{}Style51{}Caption{AlignHorz:Center;}Normal{Font:Ver" +
				"dana, 8.25pt;}Style25{}Selected{ForeColor:HighlightText;BackColor:Highlight;}Edi" +
				"tor{}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}Style17{AlignHo" +
				"rz:Near;BackColor:WhiteSmoke;}Style10{AlignHorz:Near;}Style11{}OddRow{}Style13{}" +
				"Style45{}Style36{}Style37{}Style4{}Style29{AlignHorz:Far;BackColor:GhostWhite;}S" +
				"tyle28{AlignHorz:Near;}Style27{}Style26{}RecordSelector{AlignImage:Center;}Foote" +
				"r{}Style23{AlignHorz:Far;BackColor:GhostWhite;}Style22{AlignHorz:Near;}Style21{}" +
				"Style20{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}S" +
				"tyle3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}EvenRow" +
				"{BackColor:Aqua;}Style6{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1," +
				" 1, 1;ForeColor:ControlText;BackColor:Control;}Style49{}Style48{}Style24{}Style7" +
				"{}Style8{}Style1{}Style5{}Style41{AlignHorz:Far;BackColor:GhostWhite;}Style9{}St" +
				"yle40{AlignHorz:Near;}Style43{}Style42{}Style44{}Style47{AlignHorz:Near;BackColo" +
				"r:GhostWhite;}Style46{AlignHorz:Near;}Style38{}Style39{}FilterBar{}Style12{}Styl" +
				"e34{AlignHorz:Near;}Style35{AlignHorz:Far;BackColor:GhostWhite;}Style32{}Style33" +
				"{}Style30{}Style31{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeVi" +
				"ew Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" E" +
				"xtendRightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedCellBorder\" RecordS" +
				"electorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScrollGr" +
				"oup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Editor" +
				"\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle par" +
				"ent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><Group" +
				"Style parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /" +
				"><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=\"I" +
				"nactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelecto" +
				"rStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\" m" +
				"e=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColumn" +
				"><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style17" +
				"\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" me=" +
				"\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle p" +
				"arent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Si" +
				"ngle</ColumnDivider><Width>170</Width><Height>15</Height><DCIdx>0</DCIdx></C1Dis" +
				"playColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Style " +
				"parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><Edit" +
				"orStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"Sty" +
				"le27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><ColumnDivider>Gainsbor" +
				"o,Single</ColumnDivider><Width>170</Width><Height>15</Height><DCIdx>1</DCIdx></C" +
				"1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style28\" /><St" +
				"yle parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=\"Style3\" me=\"Style30\" /><" +
				"EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeaderStyle parent=\"Style1\" me=" +
				"\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style32\" /><Visible>True</Visi" +
				"ble><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>257</Width><Height>15<" +
				"/Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=" +
				"\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"Style35\" /><FooterStyle paren" +
				"t=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5\" me=\"Style37\" /><GroupHead" +
				"erStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterStyle parent=\"Style1\" me=\"Sty" +
				"le38\" /><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>170</Width><Height" +
				">15</Height><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle par" +
				"ent=\"Style2\" me=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle p" +
				"arent=\"Style3\" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><Group" +
				"HeaderStyle parent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=" +
				"\"Style44\" /><ColumnDivider>Gainsboro,Single</ColumnDivider><Width>170</Width><He" +
				"ight>15</Height><DCIdx>4</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle" +
				" parent=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><FooterSty" +
				"le parent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" /><G" +
				"roupHeaderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Style1\"" +
				" me=\"Style50\" /><Visible>True</Visible><ColumnDivider>Gainsboro,Single</ColumnDi" +
				"vider><Width>25</Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn></in" +
				"ternalCols><ClientRect>0, 0, 488, 278</ClientRect><BorderSide>0</BorderSide></C1" +
				".Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /" +
				"><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><St" +
				"yle parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Sty" +
				"le parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style p" +
				"arent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style " +
				"parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Sty" +
				"le parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></Named" +
				"Styles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Lay" +
				"out><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 488, 278</Clien" +
				"tArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle paren" +
				"t=\"\" me=\"Style15\" /></Blob>";
			// 
			// ShortSaleTradingGroupsNegativeRebatesOfficeCodeManagementForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(494, 323);
			this.Controls.Add(this.OfficeCodeGrid);
			this.Controls.Add(this.TradingGroupLabel);
			this.Controls.Add(this.TradingGroupNameLabel);
			this.DockPadding.Bottom = 1;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 40;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ShortSaleTradingGroupsNegativeRebatesOfficeCodeManagementForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Short Sale - Trading Groups - Negative Rebates - Office Codes Management";
			this.Load += new System.EventHandler(this.ShortSaleTradingGroupsOfficeCodeManagementForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupNameLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.OfficeCodeGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
 	
		
		private void ShortSaleTradingGroupsOfficeCodeManagementForm_Load(object sender, System.EventArgs e)
		{
			this.WindowState = FormWindowState.Normal;
      
			try
			{                               				
				TradingGroupNameLabel.Text = tradingGroup;
				dataSet = mainForm.ShortSaleAgent.TradingGroupsOfficeCodeMarkGet(tradingGroup, mainForm.UtcOffset);				
				OfficeCodeGrid.SetDataBinding(dataSet,"TradingGroupOfficeCodeMarks", true);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleTradingGroupsOfficeCodeManagementForm.ShortSaleTradingGroupsOfficeCodeManagementForm_Load]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void OfficeCodeGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{				
				mainForm.RebateAgent.ShortSaleBillingSummaryTradingGroupsOfficeCodeMarkSet(
					tradingGroup,
					OfficeCodeGrid.Columns["OfficeCode"].Text,
					OfficeCodeGrid.Columns["NegativeRebateMarkUp"].Text,
					OfficeCodeGrid.Columns["NegativeRebateMarkUpCode"].Text,
					mainForm.UserId);

				dataSet = mainForm.ShortSaleAgent.TradingGroupsOfficeCodeMarkGet(tradingGroup, mainForm.UtcOffset);			
				OfficeCodeGrid.SetDataBinding(dataSet,"TradingGroupOfficeCodeMarks", true);		
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void OfficeCodeGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			try
			{
				if (e.KeyChar.Equals((char) 13))
				{
					OfficeCodeGrid.UpdateData();
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}	
	}
}
