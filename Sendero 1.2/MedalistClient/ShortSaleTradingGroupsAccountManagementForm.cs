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
	public class ShortSaleTradingGroupsAccountManagementForm : System.Windows.Forms.Form
	{
		private DataSet dataSet = null;
		private MainForm mainForm;    
		private string tradingGroup;

		private C1.Win.C1Input.C1Label TradingGroupNameLabel;
		private C1.Win.C1Input.C1Label TradingGroupLabel;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid AccountsGrid;    

		private System.ComponentModel.Container components = null;

		public ShortSaleTradingGroupsAccountManagementForm(MainForm mainForm, string tradingGroup)
		{    
			InitializeComponent();
			this.mainForm = mainForm;         
      
			try
			{     
				this.tradingGroup = tradingGroup;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleTradingGroupsAccountManagementForm.ShortSaleTradingGroupsAccountManagementForm]", Log.Error, 1);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleTradingGroupsAccountManagementForm));
			this.TradingGroupNameLabel = new C1.Win.C1Input.C1Label();
			this.TradingGroupLabel = new C1.Win.C1Input.C1Label();
			this.AccountsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupNameLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.AccountsGrid)).BeginInit();
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
			// AccountsGrid
			// 
			this.AccountsGrid.AllowAddNew = true;
			this.AccountsGrid.AllowDelete = true;
			this.AccountsGrid.CaptionHeight = 17;
			this.AccountsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AccountsGrid.EmptyRows = true;
			this.AccountsGrid.ExtendRightColumn = true;
			this.AccountsGrid.FilterBar = true;
			this.AccountsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.AccountsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.AccountsGrid.Location = new System.Drawing.Point(1, 40);
			this.AccountsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedCellBorder;
			this.AccountsGrid.Name = "AccountsGrid";
			this.AccountsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.AccountsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.AccountsGrid.PreviewInfo.ZoomFactor = 75;
			this.AccountsGrid.RecordSelectorWidth = 16;
			this.AccountsGrid.RowDivider.Color = System.Drawing.Color.WhiteSmoke;
			this.AccountsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.AccountsGrid.RowHeight = 15;
			this.AccountsGrid.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.AccountsGrid.Size = new System.Drawing.Size(884, 282);
			this.AccountsGrid.TabIndex = 9;
			this.AccountsGrid.Text = "c1TrueDBGrid1";
			this.AccountsGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.AccountsGrid_BeforeUpdate);
			this.AccountsGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.AccountsGrid_BeforeDelete);
			this.AccountsGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AccountsGrid_KeyPress);
			this.AccountsGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Account\" Da" +
				"taField=\"AccountNumber\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
				"Level=\"0\" Caption=\"Positive Rebate MarkUp\" DataField=\"PositiveRebateMarkUp\"><Val" +
				"ueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Negative " +
				"Rebate MarkUp\" DataField=\"NegativeRebateMarkUp\"><ValueItems /><GroupInfo /></C1D" +
				"ataColumn><C1DataColumn Level=\"0\" Caption=\"Fed Funds MarkUp\" DataField=\"FedFunds" +
				"MarkUp\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Captio" +
				"n=\"Libor Funds MarkUp\" DataField=\"LiborFundsMarkUp\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn></DataCols><Styles type=\"C1.Win.C1TrueDBGrid.Design.ContextWrapper" +
				"\"><Data>Caption{AlignHorz:Center;}Style27{}Normal{Font:Verdana, 8.25pt;}Style25{" +
				"}Style24{}Editor{}Style18{}Style19{}Style14{}Style15{}Style16{AlignHorz:Near;}St" +
				"yle17{AlignHorz:Near;BackColor:WhiteSmoke;}Style10{AlignHorz:Near;}Style11{}OddR" +
				"ow{}Style13{}Style12{}Style7{}Style29{AlignHorz:Far;BackColor:GhostWhite;}Style2" +
				"8{AlignHorz:Near;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Styl" +
				"e26{}RecordSelector{AlignImage:Center;}Footer{}Style23{AlignHorz:Far;BackColor:G" +
				"hostWhite;}Style22{AlignHorz:Near;}Style21{}Style20{}Group{BackColor:ControlDark" +
				";Border:None,,0, 0, 0, 0;AlignVert:Center;}Inactive{ForeColor:InactiveCaptionTex" +
				"t;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Style6{}Heading{Wrap:True;A" +
				"lignVert:Center;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;BackColor:Contro" +
				"l;}Style3{}Style4{}Style9{}Style8{}Style1{}Style5{}Style41{AlignHorz:Far;BackCol" +
				"or:GhostWhite;}Style40{AlignHorz:Near;}Style43{}FilterBar{}Style45{}Selected{For" +
				"eColor:HighlightText;BackColor:Highlight;}Style42{}Style44{}Style38{}Style39{}St" +
				"yle36{}Style37{}Style34{AlignHorz:Near;}Style35{AlignHorz:Far;BackColor:GhostWhi" +
				"te;}Style32{}Style33{}Style30{}Style31{}Style2{}</Data></Styles><Splits><C1.Win." +
				"C1TrueDBGrid.MergeView Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" Colum" +
				"nFooterHeight=\"17\" ExtendRightColumn=\"True\" FilterBar=\"True\" MarqueeStyle=\"Dotte" +
				"dCellBorder\" RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1" +
				"\" HorizontalScrollGroup=\"1\"><CaptionStyle parent=\"Style2\" me=\"Style10\" /><Editor" +
				"Style parent=\"Editor\" me=\"Style5\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style8\" /" +
				"><FilterBarStyle parent=\"FilterBar\" me=\"Style13\" /><FooterStyle parent=\"Footer\" " +
				"me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style12\" /><HeadingStyle parent=\"He" +
				"ading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style7\" /><Ina" +
				"ctiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Styl" +
				"e9\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style11\" /><SelectedStyle" +
				" parent=\"Selected\" me=\"Style6\" /><Style parent=\"Normal\" me=\"Style1\" /><internalC" +
				"ols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style16\" /><Style parent=" +
				"\"Style1\" me=\"Style17\" /><FooterStyle parent=\"Style3\" me=\"Style18\" /><EditorStyle" +
				" parent=\"Style5\" me=\"Style19\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style21\" /" +
				"><GroupFooterStyle parent=\"Style1\" me=\"Style20\" /><Visible>True</Visible><Column" +
				"Divider>LightGray,Single</ColumnDivider><Width>170</Width><Height>15</Height><DC" +
				"Idx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me" +
				"=\"Style22\" /><Style parent=\"Style1\" me=\"Style23\" /><FooterStyle parent=\"Style3\" " +
				"me=\"Style24\" /><EditorStyle parent=\"Style5\" me=\"Style25\" /><GroupHeaderStyle par" +
				"ent=\"Style1\" me=\"Style27\" /><GroupFooterStyle parent=\"Style1\" me=\"Style26\" /><Vi" +
				"sible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>170</W" +
				"idth><Height>15</Height><DCIdx>1</DCIdx></C1DisplayColumn><C1DisplayColumn><Head" +
				"ingStyle parent=\"Style2\" me=\"Style28\" /><Style parent=\"Style1\" me=\"Style29\" /><F" +
				"ooterStyle parent=\"Style3\" me=\"Style30\" /><EditorStyle parent=\"Style5\" me=\"Style" +
				"31\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style33\" /><GroupFooterStyle parent=" +
				"\"Style1\" me=\"Style32\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</" +
				"ColumnDivider><Width>170</Width><Height>15</Height><DCIdx>2</DCIdx></C1DisplayCo" +
				"lumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style34\" /><Style parent" +
				"=\"Style1\" me=\"Style35\" /><FooterStyle parent=\"Style3\" me=\"Style36\" /><EditorStyl" +
				"e parent=\"Style5\" me=\"Style37\" /><GroupHeaderStyle parent=\"Style1\" me=\"Style39\" " +
				"/><GroupFooterStyle parent=\"Style1\" me=\"Style38\" /><Visible>True</Visible><Colum" +
				"nDivider>LightGray,Single</ColumnDivider><Width>170</Width><Height>15</Height><D" +
				"CIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" m" +
				"e=\"Style40\" /><Style parent=\"Style1\" me=\"Style41\" /><FooterStyle parent=\"Style3\"" +
				" me=\"Style42\" /><EditorStyle parent=\"Style5\" me=\"Style43\" /><GroupHeaderStyle pa" +
				"rent=\"Style1\" me=\"Style45\" /><GroupFooterStyle parent=\"Style1\" me=\"Style44\" /><V" +
				"isible>True</Visible><ColumnDivider>DarkGray,Single</ColumnDivider><Width>170</W" +
				"idth><Height>15</Height><DCIdx>4</DCIdx></C1DisplayColumn></internalCols><Client" +
				"Rect>0, 0, 880, 278</ClientRect><BorderSide>0</BorderSide></C1.Win.C1TrueDBGrid." +
				"MergeView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"No" +
				"rmal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Headin" +
				"g\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\"" +
				" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=" +
				"\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me" +
				"=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\"" +
				" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits" +
				">1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSel" +
				"Width>16</DefaultRecSelWidth><ClientArea>0, 0, 880, 278</ClientArea><PrintPageHe" +
				"aderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" " +
				"/></Blob>";
			// 
			// ShortSaleTradingGroupsAccountManagementForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(886, 323);
			this.Controls.Add(this.AccountsGrid);
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
			this.Name = "ShortSaleTradingGroupsAccountManagementForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Short Sale - Trading Groups - Account Management";
			this.Load += new System.EventHandler(this.ShortSaleTradingGroupsAccountManagementForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupNameLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.AccountsGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
 	
		
		private void ShortSaleTradingGroupsAccountManagementForm_Load(object sender, System.EventArgs e)
		{
			this.WindowState = FormWindowState.Normal;
      
			try
			{                               				
				TradingGroupNameLabel.Text = tradingGroup;
				dataSet = mainForm.ShortSaleAgent.TradingGroupsAccountMarkGet(tradingGroup, mainForm.UtcOffset);				
				AccountsGrid.SetDataBinding(dataSet,"TradingGroupAccountMarks", true);
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleTradingGroupsAccountManagementForm.ShortSaleTradingGroupsAccountManagementForm]", Log.Error, 1);
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}


		private void AccountsGrid_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{
				mainForm.ShortSaleAgent.TradingGroupsAccountMarkSet(
					tradingGroup,
					AccountsGrid.Columns["AccountNumber"].Text,
					AccountsGrid.Columns["NegativeRebateMarkUp"].Text,
					AccountsGrid.Columns["PositiveRebateMarkUp"].Text,
					AccountsGrid.Columns["FedFundsMarkUp"].Text,
					AccountsGrid.Columns["LiborFundsMarkUp"].Text,
					false,
					mainForm.UserId);

				e.Cancel = false;
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				e.Cancel = true;
			}
		}
		
		private void AccountsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			try
			{				
				mainForm.ShortSaleAgent.TradingGroupsAccountMarkSet(
					tradingGroup,
					AccountsGrid.Columns["AccountNumber"].Text,
					AccountsGrid.Columns["NegativeRebateMarkUp"].Text,
					AccountsGrid.Columns["PositiveRebateMarkUp"].Text,
					AccountsGrid.Columns["FedFundsMarkUp"].Text,
					AccountsGrid.Columns["LiborFundsMarkUp"].Text,
					false,
					mainForm.UserId);

				dataSet = mainForm.ShortSaleAgent.TradingGroupsAccountMarkGet(tradingGroup, mainForm.UtcOffset);			
				AccountsGrid.SetDataBinding(dataSet,"TradingGroupAccountMarks", true);		
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void AccountsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			try
			{
				if (e.KeyChar.Equals((char) 13))
				{
					AccountsGrid.UpdateData();
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}	
	}
}
