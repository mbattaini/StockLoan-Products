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
	public class ShortSaleTradingGroupsPositiveRebatesOfficeCodeManagementForm : System.Windows.Forms.Form
	{
		private DataSet dataSet = null;
		private MainForm mainForm;    
		private string tradingGroup;

		private C1.Win.C1Input.C1Label TradingGroupNameLabel;
		private C1.Win.C1Input.C1Label TradingGroupLabel;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid OfficeCodeGrid;    

		private System.ComponentModel.Container components = null;

		public ShortSaleTradingGroupsPositiveRebatesOfficeCodeManagementForm(MainForm mainForm, string tradingGroup)
		{    
			InitializeComponent();
			this.mainForm = mainForm;         
      
			try
			{     
				this.tradingGroup = tradingGroup;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleTradingGroupsPositiveRebatesOfficeCodeManagementForm.ShortSaleTradingGroupsPositiveRebatesOfficeCodeManagementForm]", Log.Error, 1);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleTradingGroupsPositiveRebatesOfficeCodeManagementForm));
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
			this.OfficeCodeGrid.Size = new System.Drawing.Size(876, 282);
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
				"</C1DataColumn><C1DataColumn Level=\"0\" Caption=\"\" DataField=\"PositiveRebateMarkU" +
				"pCode\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win" +
				".C1TrueDBGrid.Design.ContextWrapper\"><Data>RecordSelector{AlignImage:Center;}Sty" +
				"le50{}Style51{}Caption{AlignHorz:Center;}Style27{}Normal{Font:Verdana, 8.25pt;}S" +
				"elected{ForeColor:HighlightText;BackColor:Highlight;}Editor{}Style18{}Style19{}S" +
				"tyle14{}Style15{}Style16{AlignHorz:Near;}Style17{AlignHorz:Near;BackColor:WhiteS" +
				"moke;}Style10{AlignHorz:Near;}Style11{}Style12{}Style13{}Style44{}Style7{}Style3" +
				"7{}Style4{}OddRow{}Style29{AlignHorz:Far;BackColor:GhostWhite;}Style28{AlignHorz" +
				":Near;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style26{}Style2" +
				"5{}Footer{}Style23{AlignHorz:Far;BackColor:GhostWhite;}Style22{AlignHorz:Near;}S" +
				"tyle21{}Style20{}Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:C" +
				"enter;}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}EvenRow" +
				"{BackColor:Aqua;}Style6{}Heading{Wrap:True;AlignVert:Center;Border:Raised,,1, 1," +
				" 1, 1;ForeColor:ControlText;BackColor:Control;}Style49{}Style48{}Style24{}Style9" +
				"{}Style8{}Style1{}Style3{}Style41{AlignHorz:Far;BackColor:GhostWhite;}Style40{Al" +
				"ignHorz:Near;}Style43{}FilterBar{}Style42{}Style45{}Style47{AlignHorz:Center;Bac" +
				"kColor:GhostWhite;}Style46{AlignHorz:Near;}Style38{}Style39{}Style36{}Style5{}St" +
				"yle34{AlignHorz:Near;}Style35{AlignHorz:Far;BackColor:GhostWhite;}Style32{}Style" +
				"33{}Style30{}Style31{}Style2{}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.Merge" +
				"View Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\"" +
				" ExtendRightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedCellBorder\" Recor" +
				"dSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" HorizontalScroll" +
				"Group=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><EditorStyle parent=\"Edit" +
				"or\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /><FilterBarStyle p" +
				"arent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><Gro" +
				"upStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"Heading\" me=\"Style2\"" +
				" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><InactiveStyle parent=" +
				"\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style9\" /><RecordSelec" +
				"torStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle parent=\"Selected\"" +
				" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalCols><C1DisplayColu" +
				"mn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=\"Style1\" me=\"Style" +
				"17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle parent=\"Style5\" m" +
				"e=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /><GroupFooterStyle" +
				" parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><ColumnDivider>LightGray," +
				"Single</ColumnDivider><Width>170</Width><Height>15</Height><DCIdx>0</DCIdx></C1D" +
				"isplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style22\" /><Styl" +
				"e parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" me=\"Style24\" /><Ed" +
				"itorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle parent=\"Style1\" me=\"S" +
				"tyle27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Visible>True</Visibl" +
				"e><ColumnDivider>LightGray,Single</ColumnDivider><Width>170</Width><Height>15</H" +
				"eight><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"S" +
				"tyle2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><FooterStyle parent=" +
				"\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style31\" /><GroupHeader" +
				"Style parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=\"Style1\" me=\"Style" +
				"32\" /><ColumnDivider>LightGray,Single</ColumnDivider><Width>170</Width><Height>1" +
				"5</Height><DCIdx>2</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle paren" +
				"t=\"Style2\" me=\"Style46\" /><Style parent=\"Style1\" me=\"Style47\" /><FooterStyle par" +
				"ent=\"Style3\" me=\"Style48\" /><EditorStyle parent=\"Style5\" me=\"Style49\" /><GroupHe" +
				"aderStyle parent=\"Style1\" me=\"Style51\" /><GroupFooterStyle parent=\"Style1\" me=\"S" +
				"tyle50\" /><Visible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><" +
				"Width>25</Width><Height>15</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayC" +
				"olumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent=\"Style1\" me=\"St" +
				"yle35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyle parent=\"Style5" +
				"\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" /><GroupFooterSt" +
				"yle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><ColumnDivider>LightGr" +
				"ay,Single</ColumnDivider><Width>170</Width><Height>15</Height><DCIdx>3</DCIdx></" +
				"C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style40\" /><S" +
				"tyle parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\" me=\"Style42\" />" +
				"<EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle parent=\"Style1\" me" +
				"=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><Visible>True</Vis" +
				"ible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>170</Width><Height>15<" +
				"/Height><DCIdx>4</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 872, " +
				"278</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Spli" +
				"ts><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Headin" +
				"g\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" " +
				"/><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /" +
				"><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /" +
				"><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Sty" +
				"le parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" " +
				"/><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><" +
				"horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</Defaul" +
				"tRecSelWidth><ClientArea>0, 0, 872, 278</ClientArea><PrintPageHeaderStyle parent" +
				"=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// ShortSaleTradingGroupsPositiveRebatesOfficeCodeManagementForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(878, 323);
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
			this.Name = "ShortSaleTradingGroupsPositiveRebatesOfficeCodeManagementForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Short Sale - Trading Groups - Positive Rebates - Office Codes Management";
			this.Load += new System.EventHandler(this.ShortSaleTradingGroupsPositiveRebatesOfficeCodeManagementForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupNameLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.OfficeCodeGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
 	
		
		private void ShortSaleTradingGroupsPositiveRebatesOfficeCodeManagementForm_Load(object sender, System.EventArgs e)
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
				Log.Write(error.Message + " [ShortSaleTradingGroupsPositiveRebatesOfficeCodeManagementForm.ShortSaleTradingGroupsPositiveRebatesOfficeCodeManagementForm_Load]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void OfficeCodeGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{				
				mainForm.RebateAgent.ShortSaleBillingPositiveRebatesSummaryTradingGroupsOfficeCodeMarkSet(
					tradingGroup,
					OfficeCodeGrid.Columns["OfficeCode"].Text,					
					OfficeCodeGrid.Columns["PositiveRebateMarkUp"].Text,
					OfficeCodeGrid.Columns["PositiveRebateMarkUpCode"].Text,
					OfficeCodeGrid.Columns["FedFundsMarkUp"].Text,
					OfficeCodeGrid.Columns["LiborFundsMarkUp"].Text,				
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
