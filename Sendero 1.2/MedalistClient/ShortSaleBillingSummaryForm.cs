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
	public class ShortSaleBillingForm : System.Windows.Forms.Form
	{
		private const string TEXT = "ShortSale - Negative Rebate Billing Summary";    

		private System.ComponentModel.Container components = null;
    
		private MainForm mainForm;
		private DataSet rebateSummaryDataSet = null;
		private DataSet summaryDataSet = null;
		private DataSet tradingGroupsDataSet = null;
		private DataSet accountsDataSet = null;		
		private ShortSaleBillingMarksInputForm shortSaleBillingMarksInputForm  = null;
		private ShortSaleBillingDatesLockInputForm shortSaleBillingDatesLockInputForm = null;
		private ShortSaleBillingBillsForm shortSaleBillingBillsForm = null;		
		private ShortSaleTradingGroupsNegativeRebatesBillingForm shortSaleTradingGroupsNegativeRebatesBillingForm = null;		

		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep2MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.Windows.Forms.DateTimePicker ToDatePicker;
		private System.Windows.Forms.DateTimePicker FromDatePicker;
		private C1.Win.C1Input.C1Label FromLabel;
		private C1.Win.C1Input.C1Label ToLabel;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem ActionRerunMarksMenuItem;
		private C1.Win.C1List.C1Combo GroupCodeCombo;
		private System.Windows.Forms.MenuItem ActionDatesMenuItem;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid SummaryGrid;
		private System.Windows.Forms.Button RefreshButton;
		private System.Windows.Forms.MenuItem ShowMenuItem;
		private System.Windows.Forms.MenuItem ShowSettlementDateMenuItem;
		private System.Windows.Forms.MenuItem ShowPriceMenuItem;
		private System.Windows.Forms.MenuItem ShowCommentMenuItem;
		private System.Windows.Forms.MenuItem ShowOriginalChargeMenuItem;
		private System.Windows.Forms.MenuItem CreditDebitAccountsMenuItem;		
		private string secId;
		private bool editable = false;
		private System.Windows.Forms.MenuItem ShowMarkupRateMenuItem;
		private C1.Win.C1Input.C1Label GroupCodeLabel;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid RebateSummaryGrid;
		private System.Windows.Forms.ContextMenu ChildContextMenu;
		private System.Windows.Forms.ContextMenu MainContextMenu;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem24;
		private System.Windows.Forms.MenuItem MainActionsMenuItem;
		private System.Windows.Forms.MenuItem MainActionsCreditDebitAccountsMenuItem;
		private System.Windows.Forms.MenuItem MainSendToMenuItem;
		private System.Windows.Forms.MenuItem MainSendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem MainSendToExcelMenuItem;
		private System.Windows.Forms.MenuItem MainSendToMailRecipientMenuItem;
		private System.Windows.Forms.MenuItem MainDockMenuItem;
		private System.Windows.Forms.MenuItem MainDockTopMenuItem;
		private System.Windows.Forms.MenuItem MainDockBottomMenuItem;
		private System.Windows.Forms.MenuItem MainDockNoneMenuItem;
		private System.Windows.Forms.MenuItem MainExitMenuItem;
		private System.Windows.Forms.MenuItem ActionCreateBillsMenuItem;
		private System.Windows.Forms.MenuItem ActionSetMarkUpsMenuItem;
		private System.Windows.Forms.MenuItem MainActionCreateBillsMenuItem;
		private System.Windows.Forms.MenuItem MainActionDatesMenuItem;
		private System.Windows.Forms.MenuItem MainActionRerunMarksMenuItem;
		private System.Windows.Forms.MenuItem MainActionSetMarkUpsMenuItem;
		private System.Windows.Forms.MenuItem ShowRateMenuItem;		
		private bool editableMaster = false;

		public ShortSaleBillingForm(MainForm mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;
		
			try
			{
				editable = mainForm.AdminAgent.MayEdit(mainForm.UserId, "ShortSaleBilling");
				editableMaster = mainForm.AdminAgent.MayEdit(mainForm.UserId, "ShortSaleBillingPowerUser"); //use for running billing, marksup, ...
															
				SummaryGrid.AllowUpdate = editable;		
				
				ActionCreateBillsMenuItem.Enabled = editableMaster;
				ActionRerunMarksMenuItem.Enabled = editableMaster;
				ActionDatesMenuItem.Enabled = editableMaster;
				//CreditDebitAccountsMenuItem.Enabled = editableMaster;
				ActionSetMarkUpsMenuItem.Enabled = editableMaster;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ShortSaleBillingForm));
			this.ToDatePicker = new System.Windows.Forms.DateTimePicker();
			this.FromDatePicker = new System.Windows.Forms.DateTimePicker();
			this.FromLabel = new C1.Win.C1Input.C1Label();
			this.ToLabel = new C1.Win.C1Input.C1Label();
			this.GroupCodeLabel = new C1.Win.C1Input.C1Label();
			this.ChildContextMenu = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.ActionCreateBillsMenuItem = new System.Windows.Forms.MenuItem();
			this.ActionDatesMenuItem = new System.Windows.Forms.MenuItem();
			this.ActionRerunMarksMenuItem = new System.Windows.Forms.MenuItem();
			this.CreditDebitAccountsMenuItem = new System.Windows.Forms.MenuItem();
			this.ActionSetMarkUpsMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowSettlementDateMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowPriceMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowOriginalChargeMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowCommentMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowRateMenuItem = new System.Windows.Forms.MenuItem();
			this.ShowMarkupRateMenuItem = new System.Windows.Forms.MenuItem();
			this.Sep2MenuItem = new System.Windows.Forms.MenuItem();
			this.ExitMenuItem = new System.Windows.Forms.MenuItem();
			this.SummaryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.GroupCodeCombo = new C1.Win.C1List.C1Combo();
			this.RefreshButton = new System.Windows.Forms.Button();
			this.RebateSummaryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.MainContextMenu = new System.Windows.Forms.ContextMenu();
			this.MainActionsMenuItem = new System.Windows.Forms.MenuItem();
			this.MainActionCreateBillsMenuItem = new System.Windows.Forms.MenuItem();
			this.MainActionDatesMenuItem = new System.Windows.Forms.MenuItem();
			this.MainActionRerunMarksMenuItem = new System.Windows.Forms.MenuItem();
			this.MainActionsCreditDebitAccountsMenuItem = new System.Windows.Forms.MenuItem();
			this.MainActionSetMarkUpsMenuItem = new System.Windows.Forms.MenuItem();
			this.MainSendToMenuItem = new System.Windows.Forms.MenuItem();
			this.MainSendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
			this.MainSendToExcelMenuItem = new System.Windows.Forms.MenuItem();
			this.MainSendToMailRecipientMenuItem = new System.Windows.Forms.MenuItem();
			this.MainDockMenuItem = new System.Windows.Forms.MenuItem();
			this.MainDockTopMenuItem = new System.Windows.Forms.MenuItem();
			this.MainDockBottomMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.MainDockNoneMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem24 = new System.Windows.Forms.MenuItem();
			this.MainExitMenuItem = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.FromLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ToLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SummaryGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RebateSummaryGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// ToDatePicker
			// 
			this.ToDatePicker.Location = new System.Drawing.Point(291, 14);
			this.ToDatePicker.Name = "ToDatePicker";
			this.ToDatePicker.Size = new System.Drawing.Size(216, 21);
			this.ToDatePicker.TabIndex = 1;
			this.ToDatePicker.ValueChanged += new System.EventHandler(this.ToDatePicker_ValueChanged);
			// 
			// FromDatePicker
			// 
			this.FromDatePicker.Location = new System.Drawing.Point(41, 14);
			this.FromDatePicker.Name = "FromDatePicker";
			this.FromDatePicker.Size = new System.Drawing.Size(216, 21);
			this.FromDatePicker.TabIndex = 2;
			this.FromDatePicker.ValueChanged += new System.EventHandler(this.FromDatePicker_ValueChanged);
			// 
			// FromLabel
			// 
			this.FromLabel.Location = new System.Drawing.Point(-24, 16);
			this.FromLabel.Name = "FromLabel";
			this.FromLabel.Size = new System.Drawing.Size(64, 16);
			this.FromLabel.TabIndex = 3;
			this.FromLabel.Tag = null;
			this.FromLabel.Text = "From:";
			this.FromLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.FromLabel.TextDetached = true;
			// 
			// ToLabel
			// 
			this.ToLabel.Location = new System.Drawing.Point(258, 16);
			this.ToLabel.Name = "ToLabel";
			this.ToLabel.Size = new System.Drawing.Size(32, 16);
			this.ToLabel.TabIndex = 4;
			this.ToLabel.Tag = null;
			this.ToLabel.Text = "To:";
			this.ToLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ToLabel.TextDetached = true;
			// 
			// GroupCodeLabel
			// 
			this.GroupCodeLabel.Location = new System.Drawing.Point(508, 16);
			this.GroupCodeLabel.Name = "GroupCodeLabel";
			this.GroupCodeLabel.Size = new System.Drawing.Size(104, 16);
			this.GroupCodeLabel.TabIndex = 6;
			this.GroupCodeLabel.Tag = null;
			this.GroupCodeLabel.Text = "Group Code:";
			this.GroupCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.GroupCodeLabel.TextDetached = true;
			// 
			// ChildContextMenu
			// 
			this.ChildContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										 this.menuItem1,
																																										 this.SendToMenuItem,
																																										 this.ShowMenuItem,
																																										 this.Sep2MenuItem,
																																										 this.ExitMenuItem});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.ActionCreateBillsMenuItem,
																																							this.ActionDatesMenuItem,
																																							this.ActionRerunMarksMenuItem,
																																							this.CreditDebitAccountsMenuItem,
																																							this.ActionSetMarkUpsMenuItem});
			this.menuItem1.Text = "Actions";
			// 
			// ActionCreateBillsMenuItem
			// 
			this.ActionCreateBillsMenuItem.Index = 0;
			this.ActionCreateBillsMenuItem.Text = "Create Bill(s)";
			this.ActionCreateBillsMenuItem.Click += new System.EventHandler(this.ActionCreateBillsMenuItem_Click);
			// 
			// ActionDatesMenuItem
			// 
			this.ActionDatesMenuItem.Index = 1;
			this.ActionDatesMenuItem.Text = "Dates";
			this.ActionDatesMenuItem.Click += new System.EventHandler(this.ActionDatesMenuItem_Click);
			// 
			// ActionRerunMarksMenuItem
			// 
			this.ActionRerunMarksMenuItem.Index = 2;
			this.ActionRerunMarksMenuItem.Text = "Rerun MarkUps";
			this.ActionRerunMarksMenuItem.Click += new System.EventHandler(this.ActionRerunMarksMenuItem_Click);
			// 
			// CreditDebitAccountsMenuItem
			// 
			this.CreditDebitAccountsMenuItem.Enabled = false;
			this.CreditDebitAccountsMenuItem.Index = 3;
			this.CreditDebitAccountsMenuItem.Text = "Credit/Debit Accounts";
			this.CreditDebitAccountsMenuItem.Click += new System.EventHandler(this.CreditDebitAccountsMenuItem_Click);
			// 
			// ActionSetMarkUpsMenuItem
			// 
			this.ActionSetMarkUpsMenuItem.Index = 4;
			this.ActionSetMarkUpsMenuItem.Text = "Set MarkUps";
			this.ActionSetMarkUpsMenuItem.Click += new System.EventHandler(this.ActionSetMarkUpsMenuItem_Click);
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
			// ShowMenuItem
			// 
			this.ShowMenuItem.Index = 2;
			this.ShowMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																								 this.ShowSettlementDateMenuItem,
																																								 this.ShowPriceMenuItem,
																																								 this.ShowOriginalChargeMenuItem,
																																								 this.ShowCommentMenuItem,
																																								 this.ShowRateMenuItem,
																																								 this.ShowMarkupRateMenuItem});
			this.ShowMenuItem.Text = "Show";
			// 
			// ShowSettlementDateMenuItem
			// 
			this.ShowSettlementDateMenuItem.Index = 0;
			this.ShowSettlementDateMenuItem.Text = "Settlement Date";
			this.ShowSettlementDateMenuItem.Click += new System.EventHandler(this.ShowSettlementDateMenuItem_Click);
			// 
			// ShowPriceMenuItem
			// 
			this.ShowPriceMenuItem.Index = 1;
			this.ShowPriceMenuItem.Text = "Price";
			this.ShowPriceMenuItem.Click += new System.EventHandler(this.ShowPriceMenuItem_Click);
			// 
			// ShowOriginalChargeMenuItem
			// 
			this.ShowOriginalChargeMenuItem.Index = 2;
			this.ShowOriginalChargeMenuItem.Text = "Original Charge";
			this.ShowOriginalChargeMenuItem.Click += new System.EventHandler(this.ShowOriginalCharge_Click);
			// 
			// ShowCommentMenuItem
			// 
			this.ShowCommentMenuItem.Index = 3;
			this.ShowCommentMenuItem.Text = "Comment";
			this.ShowCommentMenuItem.Click += new System.EventHandler(this.ShowCommentMenuItem_Click);
			// 
			// ShowRateMenuItem
			// 
			this.ShowRateMenuItem.Index = 4;
			this.ShowRateMenuItem.Text = "Rate";
			this.ShowRateMenuItem.Click += new System.EventHandler(this.ShowRateMenuItem_Click);
			// 
			// ShowMarkupRateMenuItem
			// 
			this.ShowMarkupRateMenuItem.Checked = true;
			this.ShowMarkupRateMenuItem.Index = 5;
			this.ShowMarkupRateMenuItem.Text = "Markup Rate";
			this.ShowMarkupRateMenuItem.Click += new System.EventHandler(this.ShowMarkupRateMenuItem_Click);
			// 
			// Sep2MenuItem
			// 
			this.Sep2MenuItem.Index = 3;
			this.Sep2MenuItem.Text = "-";
			// 
			// ExitMenuItem
			// 
			this.ExitMenuItem.Index = 4;
			this.ExitMenuItem.Text = "Exit";
			this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// SummaryGrid
			// 
			this.SummaryGrid.AllowColSelect = false;
			this.SummaryGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.SummaryGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SummaryGrid.CaptionHeight = 17;
			this.SummaryGrid.ColumnFooters = true;
			this.SummaryGrid.ContextMenu = this.ChildContextMenu;
			this.SummaryGrid.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.SummaryGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.SummaryGrid.EmptyRows = true;
			this.SummaryGrid.ExtendRightColumn = true;
			this.SummaryGrid.FetchRowStyles = true;
			this.SummaryGrid.FilterBar = true;
			this.SummaryGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.SummaryGrid.GroupByAreaVisible = false;
			this.SummaryGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.SummaryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.SummaryGrid.Location = new System.Drawing.Point(34, 158);
			this.SummaryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.SummaryGrid.Name = "SummaryGrid";
			this.SummaryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.SummaryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.SummaryGrid.PreviewInfo.ZoomFactor = 75;
			this.SummaryGrid.RecordSelectorWidth = 16;
			this.SummaryGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.SummaryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.SummaryGrid.RowHeight = 15;
			this.SummaryGrid.RowSubDividerColor = System.Drawing.Color.LightGray;
			this.SummaryGrid.Size = new System.Drawing.Size(1098, 246);
			this.SummaryGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.SummaryGrid.TabIndex = 12;
			this.SummaryGrid.TabStop = false;
			this.SummaryGrid.WrapCellPointer = true;
			this.SummaryGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.SummaryGrid_Paint);
			this.SummaryGrid.AfterFilter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.SummaryGrid_AfterFilter);
			this.SummaryGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.SummaryGrid_FetchRowStyle);
			this.SummaryGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.SummaryGrid_BeforeUpdate);
			this.SummaryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.SummaryGrid_FormatText);
			this.SummaryGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SummaryGrid_KeyPress);
			this.SummaryGrid.Error += new C1.Win.C1TrueDBGrid.ErrorEventHandler(this.SummaryGrid_Error);
			this.SummaryGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Account Num" +
				"ber\" DataField=\"AccountNumber\"><ValueItems /><GroupInfo /></C1DataColumn><C1Data" +
				"Column Level=\"0\" Caption=\"Quantity Shorted\" DataField=\"QuantityShorted\" NumberFo" +
				"rmat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn " +
				"Level=\"0\" Caption=\"Quantity Covered\" DataField=\"QuantityCovered\" NumberFormat=\"F" +
				"ormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"" +
				"0\" Caption=\"Quantity Uncovered\" DataField=\"QuantityUncovered\" NumberFormat=\"Form" +
				"atText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
				"Caption=\"Price\" DataField=\"Price\" NumberFormat=\"FormatText Event\"><ValueItems />" +
				"<GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Rate\" DataField=\"Ra" +
				"te\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C" +
				"1DataColumn Level=\"0\" Caption=\"Group Code\" DataField=\"GroupCode\"><ValueItems /><" +
				"GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Charge Before Mark\" " +
				"DataField=\"OriginalCharge\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupI" +
				"nfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Charge After Mark\" DataFie" +
				"ld=\"ModifiedCharge\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /><" +
				"/C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Security ID\" DataField=\"SecId\"><V" +
				"alueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"\" DataF" +
				"ield=\"Symbol\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" " +
				"Caption=\"IsLocked\" DataField=\"IsLocked\"><ValueItems /><GroupInfo /></C1DataColum" +
				"n><C1DataColumn Level=\"0\" Caption=\"BizDate\" DataField=\"BizDate\" NumberFormat=\"Fo" +
				"rmatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0" +
				"\" Caption=\"Settle Date\" DataField=\"SettlementDate\" NumberFormat=\"FormatText Even" +
				"t\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Pr" +
				"ocessId\" DataField=\"ProcessId\"><ValueItems /><GroupInfo /></C1DataColumn><C1Data" +
				"Column Level=\"0\" Caption=\"Comment\" DataField=\"Comment\"><ValueItems /><GroupInfo " +
				"/></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Markup Rate\" DataField=\"Markup" +
				"Rate\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn>" +
				"<C1DataColumn Level=\"0\" Caption=\"PB\" DataField=\"PreBorrow\"><ValueItems Presentat" +
				"ion=\"CheckBox\" /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1T" +
				"rueDBGrid.Design.ContextWrapper\"><Data>HighlightRow{ForeColor:HighlightText;Back" +
				"Color:Highlight;}Style787{}Style654{}Inactive{ForeColor:InactiveCaptionText;Back" +
				"Color:InactiveCaption;}Style74{}Style71{}Style72{}Style73{}Style70{AlignHorz:Cen" +
				"ter;}Style665{}Style664{}Style667{Font:Verdana, 8.25pt;AlignHorz:Near;}Style666{" +
				"}Style661{Font:Verdana, 8.25pt;AlignHorz:Near;}Style660{}Style663{Font:Verdana, " +
				"8.25pt, style=Bold;BackColor:255, 251, 242;}Style662{AlignHorz:Far;BackColor:251" +
				", 251, 255;}Style639{}Style631{Font:Verdana, 8.25pt;}Style632{}Style633{}Style63" +
				"4{Font:Verdana, 8.25pt;BackColor:WhiteSmoke;}Style635{}Style636{}Style637{}Foote" +
				"r{Border:None,,0, 0, 0, 0;BackColor:Window;}Style676{}Editor{}FilterBar{BackColo" +
				"r:SeaShell;AlignVert:Center;}RecordSelector{AlignImage:Center;}Heading{Wrap:True" +
				";BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Cen" +
				"ter;}Style19{}Style18{}Style649{Font:Verdana, 8.25pt;AlignHorz:Near;}Style14{}St" +
				"yle15{}Style16{AlignHorz:Far;BackColor:249, 249, 249;}Style17{Font:Verdana, 8.25" +
				"pt, style=Bold;BackColor:255, 251, 242;}Style10{}Style11{}Style12{}Style13{Align" +
				"Horz:Near;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style27{AlignHo" +
				"rz:Near;}Style22{AlignHorz:Near;BackColor:GhostWhite;}Style29{Font:Verdana, 8.25" +
				"pt, style=Bold;BackColor:255, 251, 242;}Style8{AlignHorz:Far;BackColor:Snow;}Sty" +
				"le28{AlignHorz:Near;BackColor:GhostWhite;}Style26{}Style5{}Style25{}Style24{}Sty" +
				"le6{}Style1{AlignHorz:Near;}Style23{Font:Verdana, 8.25pt, style=Bold;BackColor:2" +
				"55, 251, 242;}Style3{BackColor:255, 251, 242;}Style2{Font:Verdana, 8.25pt, style" +
				"=Bold;AlignHorz:Near;BackColor:Snow;}Style21{AlignHorz:Near;}Style20{}OddRow{}St" +
				"yle669{Font:Verdana, 8.25pt, style=Bold;BackColor:255, 251, 242;}Style668{AlignH" +
				"orz:Far;BackColor:251, 251, 255;}Style723{Font:Verdana, 8.25pt, style=Bold;BackC" +
				"olor:255, 251, 242;}Style638{}Style47{BackColor:255, 251, 242;}Style41{BackColor" +
				":255, 251, 242;}Style43{}Style38{}Style39{AlignHorz:Near;}Style36{}Style37{}Styl" +
				"e34{AlignHorz:Near;BackColor:WhiteSmoke;}Style35{}Style32{}Style33{AlignHorz:Nea" +
				"r;}Style30{}Style49{}Style48{}Style31{}Style678{}Style42{}Style45{AlignHorz:Near" +
				";}Style44{}Style674{AlignHorz:Far;BackColor:255, 244, 255;}Style675{Font:Verdana" +
				", 8.25pt, style=Bold;BackColor:255, 251, 242;}Style40{AlignHorz:Near;BackColor:2" +
				"53, 253, 253;}Style677{}Style670{}Style671{}Style672{}Style673{Font:Verdana, 8.2" +
				"5pt;AlignHorz:Center;}Style46{AlignHorz:Near;BackColor:254, 249, 237;}Normal{Fon" +
				"t:Verdana, 8.25pt;}EvenRow{BackColor:Aqua;}Style9{Font:Verdana, 8.25pt, style=Bo" +
				"ld;BackColor:255, 251, 242;}Style4{}Style7{AlignHorz:Near;}Style58{AlignHorz:Nea" +
				"r;BackColor:WhiteSmoke;}Style59{}Style51{AlignHorz:Near;}Style54{}Style50{}Style" +
				"57{AlignHorz:Near;}Style52{AlignHorz:Near;}Style53{}Style721{AlignHorz:Near;}Sty" +
				"le55{}Style56{}Style724{}Style725{}Style726{}Style65{BackColor:255, 251, 242;}St" +
				"yle642{}Style641{}Style640{AlignHorz:Near;}Caption{AlignHorz:Center;}Style69{Ali" +
				"gnHorz:Center;}Style68{}Style62{}Style658{}Style659{}Style656{AlignHorz:Far;Back" +
				"Color:251, 251, 255;}Style63{AlignHorz:Near;}Style722{AlignHorz:Far;BackColor:25" +
				"5, 251, 255;}Style61{}Style60{}Style67{}Style66{}Style655{Font:Verdana, 8.25pt;A" +
				"lignHorz:Near;}Style64{AlignHorz:Far;BackColor:255, 251, 255;}Style651{Font:Verd" +
				"ana, 8.25pt, style=Bold;BackColor:255, 251, 242;}Style652{}Style653{}Style650{Fo" +
				"nt:Verdana, 8.25pt, style=Bold;AlignHorz:Near;ForeColor:Black;BackColor:Snow;}Gr" +
				"oup{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style657{Fon" +
				"t:Verdana, 8.25pt, style=Bold;BackColor:255, 251, 242;}</Data></Styles><Splits><" +
				"C1.Win.C1TrueDBGrid.MergeView HBarStyle=\"None\" VBarStyle=\"Always\" AllowColSelect" +
				"=\"False\" Name=\"Split[0,0]\" AllowRowSizing=\"None\" AllowVerticalSizing=\"False\" Cap" +
				"tionHeight=\"18\" ColumnCaptionHeight=\"18\" ColumnFooterHeight=\"18\" ExtendRightColu" +
				"mn=\"True\" FetchRowStyles=\"True\" FilterBar=\"True\" MarqueeStyle=\"DottedRowBorder\" " +
				"RecordSelectorWidth=\"16\" DefRecSelWidth=\"16\" VerticalScrollGroup=\"1\" MinWidth=\"1" +
				"00\" HorizontalScrollGroup=\"5\"><Caption>Borrow Summary</Caption><CaptionStyle par" +
				"ent=\"Heading\" me=\"Style640\" /><EditorStyle parent=\"Editor\" me=\"Style632\" /><Even" +
				"RowStyle parent=\"EvenRow\" me=\"Style638\" /><FilterBarStyle parent=\"FilterBar\" me=" +
				"\"Style787\" /><FooterStyle parent=\"Footer\" me=\"Style634\" /><GroupStyle parent=\"Gr" +
				"oup\" me=\"Style642\" /><HeadingStyle parent=\"Heading\" me=\"Style633\" /><HighLightRo" +
				"wStyle parent=\"HighlightRow\" me=\"Style637\" /><InactiveStyle parent=\"Inactive\" me" +
				"=\"Style636\" /><OddRowStyle parent=\"OddRow\" me=\"Style639\" /><RecordSelectorStyle " +
				"parent=\"RecordSelector\" me=\"Style641\" /><SelectedStyle parent=\"Selected\" me=\"Sty" +
				"le635\" /><Style parent=\"Normal\" me=\"Style631\" /><internalCols><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style633\" me=\"Style51\" /><Style parent=\"Style631\" me=\"Style" +
				"52\" /><FooterStyle parent=\"Style634\" me=\"Style53\" /><EditorStyle parent=\"Style63" +
				"2\" me=\"Style54\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style56\" /><GroupFoote" +
				"rStyle parent=\"Style631\" me=\"Style55\" /><ColumnDivider>DarkGray,Single</ColumnDi" +
				"vider><Height>15</Height><DCIdx>14</DCIdx></C1DisplayColumn><C1DisplayColumn><He" +
				"adingStyle parent=\"Style633\" me=\"Style39\" /><Style parent=\"Style631\" me=\"Style40" +
				"\" /><FooterStyle parent=\"Style634\" me=\"Style41\" /><EditorStyle parent=\"Style632\"" +
				" me=\"Style42\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style44\" /><GroupFooterS" +
				"tyle parent=\"Style631\" me=\"Style43\" /><Visible>True</Visible><ColumnDivider>Ligh" +
				"tGray,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx>12</D" +
				"CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Styl" +
				"e1\" /><Style parent=\"Style631\" me=\"Style2\" /><FooterStyle parent=\"Style634\" me=\"" +
				"Style3\" /><EditorStyle parent=\"Style632\" me=\"Style4\" /><GroupHeaderStyle parent=" +
				"\"Style631\" me=\"Style6\" /><GroupFooterStyle parent=\"Style631\" me=\"Style5\" /><Visi" +
				"ble>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Height>15</Hei" +
				"ght><Locked>True</Locked><DCIdx>6</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
				"dingStyle parent=\"Style633\" me=\"Style649\" /><Style parent=\"Style631\" me=\"Style65" +
				"0\" /><FooterStyle parent=\"Style634\" me=\"Style651\" /><EditorStyle parent=\"Style63" +
				"2\" me=\"Style652\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style654\" /><GroupFoo" +
				"terStyle parent=\"Style631\" me=\"Style653\" /><Visible>True</Visible><ColumnDivider" +
				">LightGray,Single</ColumnDivider><Width>120</Width><Height>15</Height><Locked>Tr" +
				"ue</Locked><FooterDivider>False</FooterDivider><DCIdx>0</DCIdx></C1DisplayColumn" +
				"><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style21\" /><Style parent=\"" +
				"Style631\" me=\"Style22\" /><FooterStyle parent=\"Style634\" me=\"Style23\" /><EditorSt" +
				"yle parent=\"Style632\" me=\"Style24\" /><GroupHeaderStyle parent=\"Style631\" me=\"Sty" +
				"le26\" /><GroupFooterStyle parent=\"Style631\" me=\"Style25\" /><Visible>True</Visibl" +
				"e><ColumnDivider>LightGray,None</ColumnDivider><Height>15</Height><Locked>True</" +
				"Locked><DCIdx>9</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style633\" me=\"Style27\" /><Style parent=\"Style631\" me=\"Style28\" /><FooterStyle pa" +
				"rent=\"Style634\" me=\"Style29\" /><EditorStyle parent=\"Style632\" me=\"Style30\" /><Gr" +
				"oupHeaderStyle parent=\"Style631\" me=\"Style32\" /><GroupFooterStyle parent=\"Style6" +
				"31\" me=\"Style31\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</Colum" +
				"nDivider><Width>63</Width><Height>15</Height><Locked>True</Locked><DCIdx>10</DCI" +
				"dx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style4" +
				"5\" /><Style parent=\"Style631\" me=\"Style46\" /><FooterStyle parent=\"Style634\" me=\"" +
				"Style47\" /><EditorStyle parent=\"Style632\" me=\"Style48\" /><GroupHeaderStyle paren" +
				"t=\"Style631\" me=\"Style50\" /><GroupFooterStyle parent=\"Style631\" me=\"Style49\" /><" +
				"ColumnDivider>LightGray,Single</ColumnDivider><Width>130</Width><Height>15</Heig" +
				"ht><Locked>True</Locked><DCIdx>13</DCIdx></C1DisplayColumn><C1DisplayColumn><Hea" +
				"dingStyle parent=\"Style633\" me=\"Style655\" /><Style parent=\"Style631\" me=\"Style65" +
				"6\" /><FooterStyle parent=\"Style634\" me=\"Style657\" /><EditorStyle parent=\"Style63" +
				"2\" me=\"Style658\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style660\" /><GroupFoo" +
				"terStyle parent=\"Style631\" me=\"Style659\" /><Visible>True</Visible><ColumnDivider" +
				">LightGray,Single</ColumnDivider><Width>150</Width><Height>15</Height><Locked>Tr" +
				"ue</Locked><FooterDivider>False</FooterDivider><DCIdx>1</DCIdx></C1DisplayColumn" +
				"><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style661\" /><Style parent=" +
				"\"Style631\" me=\"Style662\" /><FooterStyle parent=\"Style634\" me=\"Style663\" /><Edito" +
				"rStyle parent=\"Style632\" me=\"Style664\" /><GroupHeaderStyle parent=\"Style631\" me=" +
				"\"Style666\" /><GroupFooterStyle parent=\"Style631\" me=\"Style665\" /><Visible>True</" +
				"Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>130</Width><Height" +
				">15</Height><Locked>True</Locked><FooterDivider>False</FooterDivider><DCIdx>2</D" +
				"CIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Styl" +
				"e667\" /><Style parent=\"Style631\" me=\"Style668\" /><FooterStyle parent=\"Style634\" " +
				"me=\"Style669\" /><EditorStyle parent=\"Style632\" me=\"Style670\" /><GroupHeaderStyle" +
				" parent=\"Style631\" me=\"Style672\" /><GroupFooterStyle parent=\"Style631\" me=\"Style" +
				"671\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Wi" +
				"dth>130</Width><Height>15</Height><Locked>True</Locked><FooterDivider>False</Foo" +
				"terDivider><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle pare" +
				"nt=\"Style633\" me=\"Style673\" /><Style parent=\"Style631\" me=\"Style674\" /><FooterSt" +
				"yle parent=\"Style634\" me=\"Style675\" /><EditorStyle parent=\"Style632\" me=\"Style67" +
				"6\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style678\" /><GroupFooterStyle paren" +
				"t=\"Style631\" me=\"Style677\" /><ColumnDivider>LightGray,Single</ColumnDivider><Wid" +
				"th>90</Width><Height>15</Height><FooterDivider>False</FooterDivider><DCIdx>4</DC" +
				"Idx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style" +
				"721\" /><Style parent=\"Style631\" me=\"Style722\" /><FooterStyle parent=\"Style634\" m" +
				"e=\"Style723\" /><EditorStyle parent=\"Style632\" me=\"Style724\" /><GroupHeaderStyle " +
				"parent=\"Style631\" me=\"Style726\" /><GroupFooterStyle parent=\"Style631\" me=\"Style7" +
				"25\" /><ColumnDivider>LightGray,Single</ColumnDivider><Width>90</Width><Height>15" +
				"</Height><DCIdx>5</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent" +
				"=\"Style633\" me=\"Style7\" /><Style parent=\"Style631\" me=\"Style8\" /><FooterStyle pa" +
				"rent=\"Style634\" me=\"Style9\" /><EditorStyle parent=\"Style632\" me=\"Style10\" /><Gro" +
				"upHeaderStyle parent=\"Style631\" me=\"Style12\" /><GroupFooterStyle parent=\"Style63" +
				"1\" me=\"Style11\" /><ColumnDivider>LightGray,Single</ColumnDivider><Width>140</Wid" +
				"th><Height>15</Height><DCIdx>7</DCIdx></C1DisplayColumn><C1DisplayColumn><Headin" +
				"gStyle parent=\"Style633\" me=\"Style63\" /><Style parent=\"Style631\" me=\"Style64\" />" +
				"<FooterStyle parent=\"Style634\" me=\"Style65\" /><EditorStyle parent=\"Style632\" me=" +
				"\"Style66\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style68\" /><GroupFooterStyle" +
				" parent=\"Style631\" me=\"Style67\" /><Visible>True</Visible><ColumnDivider>Gainsbor" +
				"o,Single</ColumnDivider><Height>15</Height><Locked>True</Locked><DCIdx>16</DCIdx" +
				"></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style13\"" +
				" /><Style parent=\"Style631\" me=\"Style16\" /><FooterStyle parent=\"Style634\" me=\"St" +
				"yle17\" /><EditorStyle parent=\"Style632\" me=\"Style18\" /><GroupHeaderStyle parent=" +
				"\"Style631\" me=\"Style20\" /><GroupFooterStyle parent=\"Style631\" me=\"Style19\" /><Vi" +
				"sible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>140</W" +
				"idth><Height>15</Height><Locked>True</Locked><DCIdx>8</DCIdx></C1DisplayColumn><" +
				"C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style33\" /><Style parent=\"St" +
				"yle631\" me=\"Style34\" /><FooterStyle parent=\"Style634\" me=\"Style35\" /><EditorStyl" +
				"e parent=\"Style632\" me=\"Style36\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style" +
				"38\" /><GroupFooterStyle parent=\"Style631\" me=\"Style37\" /><ColumnDivider>DarkGray" +
				",Single</ColumnDivider><Height>15</Height><DCIdx>11</DCIdx></C1DisplayColumn><C1" +
				"DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style57\" /><Style parent=\"Styl" +
				"e631\" me=\"Style58\" /><FooterStyle parent=\"Style634\" me=\"Style59\" /><EditorStyle " +
				"parent=\"Style632\" me=\"Style60\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style62" +
				"\" /><GroupFooterStyle parent=\"Style631\" me=\"Style61\" /><ColumnDivider>LightGray," +
				"Single</ColumnDivider><Height>15</Height><DCIdx>15</DCIdx></C1DisplayColumn><C1D" +
				"isplayColumn><HeadingStyle parent=\"Style633\" me=\"Style69\" /><Style parent=\"Style" +
				"631\" me=\"Style70\" /><FooterStyle parent=\"Style634\" me=\"Style71\" /><EditorStyle p" +
				"arent=\"Style632\" me=\"Style72\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style74\"" +
				" /><GroupFooterStyle parent=\"Style631\" me=\"Style73\" /><ColumnDivider>Gainsboro,S" +
				"ingle</ColumnDivider><Width>32</Width><Height>15</Height><Locked>True</Locked><D" +
				"CIdx>17</DCIdx></C1DisplayColumn></internalCols><ClientRect>0, 0, 1096, 244</Cli" +
				"entRect><BorderSide>Right</BorderSide></C1.Win.C1TrueDBGrid.MergeView></Splits><" +
				"NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /" +
				"><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><S" +
				"tyle parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><St" +
				"yle parent=\"Normal\" me=\"Editor\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><St" +
				"yle parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style p" +
				"arent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Normal\" me=\"FilterBar\" /><S" +
				"tyle parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horz" +
				"Splits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRec" +
				"SelWidth><ClientArea>0, 0, 1096, 244</ClientArea><PrintPageHeaderStyle parent=\"\"" +
				" me=\"Style14\" /><PrintPageFooterStyle parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// GroupCodeCombo
			// 
			this.GroupCodeCombo.AddItemSeparator = ';';
			this.GroupCodeCombo.Caption = "";
			this.GroupCodeCombo.CaptionHeight = 17;
			this.GroupCodeCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			this.GroupCodeCombo.ColumnCaptionHeight = 17;
			this.GroupCodeCombo.ColumnFooterHeight = 17;
			this.GroupCodeCombo.ColumnWidth = 100;
			this.GroupCodeCombo.ContentHeight = 16;
			this.GroupCodeCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
			this.GroupCodeCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
			this.GroupCodeCombo.DropDownWidth = 300;
			this.GroupCodeCombo.EditorBackColor = System.Drawing.SystemColors.Window;
			this.GroupCodeCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GroupCodeCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
			this.GroupCodeCombo.EditorHeight = 16;
			this.GroupCodeCombo.ExtendRightColumn = true;
			this.GroupCodeCombo.GapHeight = 2;
			this.GroupCodeCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.GroupCodeCombo.ItemHeight = 15;
			this.GroupCodeCombo.Location = new System.Drawing.Point(613, 13);
			this.GroupCodeCombo.MatchEntryTimeout = ((long)(2000));
			this.GroupCodeCombo.MaxDropDownItems = ((short)(5));
			this.GroupCodeCombo.MaxLength = 32767;
			this.GroupCodeCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.GroupCodeCombo.Name = "GroupCodeCombo";
			this.GroupCodeCombo.PartialRightColumn = false;
			this.GroupCodeCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.GroupCodeCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
			this.GroupCodeCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.GroupCodeCombo.Size = new System.Drawing.Size(128, 22);
			this.GroupCodeCombo.TabIndex = 13;
			this.GroupCodeCombo.TextChanged += new System.EventHandler(this.GroupCodeCombo_TextChanged);
			this.GroupCodeCombo.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Corresponde" +
				"nt\" DataField=\"GroupCode\"><ValueItems /></C1DataColumn><C1DataColumn Level=\"0\" C" +
				"aption=\"Correspondent Name\" DataField=\"GroupName\"><ValueItems /></C1DataColumn><" +
				"/DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVe" +
				"rt:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;" +
				"}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeCo" +
				"lor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptio" +
				"nText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{}Highl" +
				"ightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelec" +
				"tor{AlignImage:Center;}Style13{AlignHorz:Near;}Heading{Wrap:True;BackColor:Contr" +
				"ol;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Sty" +
				"le10{}Style11{}Style14{}Style15{AlignHorz:Near;}Style16{AlignHorz:Near;}Style17{" +
				"}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView Allow" +
				"ColSelect=\"False\" Name=\"\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFoot" +
				"erHeight=\"17\" ExtendRightColumn=\"True\" VerticalScrollGroup=\"1\" HorizontalScrollG" +
				"roup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><" +
				"HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" " +
				"/><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Co" +
				"lor><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1" +
				"DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style15\" /><Sty" +
				"le parent=\"Style1\" me=\"Style16\" /><FooterStyle parent=\"Style3\" me=\"Style17\" /><C" +
				"olumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height" +
				">15</Height><DCIdx>1</DCIdx></C1DisplayColumn></internalCols><VScrollBar><Width>" +
				"16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle" +
				" parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><Foo" +
				"terStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /" +
				"><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"Highlig" +
				"htRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle" +
				" parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"" +
				"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\"" +
				" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"" +
				"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me" +
				"=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"I" +
				"nactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Hig" +
				"hlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"Od" +
				"dRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me" +
				"=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><La" +
				"yout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			// 
			// RefreshButton
			// 
			this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RefreshButton.Location = new System.Drawing.Point(1192, 13);
			this.RefreshButton.Name = "RefreshButton";
			this.RefreshButton.TabIndex = 15;
			this.RefreshButton.Text = "&Refresh";
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// RebateSummaryGrid
			// 
			this.RebateSummaryGrid.AllowColSelect = false;
			this.RebateSummaryGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.RebateSummaryGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RebateSummaryGrid.CaptionHeight = 17;
			this.RebateSummaryGrid.ChildGrid = this.SummaryGrid;
			this.RebateSummaryGrid.ColumnFooters = true;
			this.RebateSummaryGrid.ContextMenu = this.MainContextMenu;
			this.RebateSummaryGrid.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.RebateSummaryGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.RebateSummaryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RebateSummaryGrid.EmptyRows = true;
			this.RebateSummaryGrid.ExtendRightColumn = true;
			this.RebateSummaryGrid.FetchRowStyles = true;
			this.RebateSummaryGrid.FilterBar = true;
			this.RebateSummaryGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.RebateSummaryGrid.GroupByAreaVisible = false;
			this.RebateSummaryGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.RebateSummaryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.RebateSummaryGrid.Location = new System.Drawing.Point(1, 55);
			this.RebateSummaryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.RebateSummaryGrid.Name = "RebateSummaryGrid";
			this.RebateSummaryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.RebateSummaryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.RebateSummaryGrid.PreviewInfo.ZoomFactor = 75;
			this.RebateSummaryGrid.RecordSelectorWidth = 16;
			this.RebateSummaryGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.RebateSummaryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.RebateSummaryGrid.RowHeight = 15;
			this.RebateSummaryGrid.RowSubDividerColor = System.Drawing.Color.LightGray;
			this.RebateSummaryGrid.Size = new System.Drawing.Size(1282, 729);
			this.RebateSummaryGrid.TabIndex = 17;
			this.RebateSummaryGrid.WrapCellPointer = true;
			this.RebateSummaryGrid.BeforeOpen += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.RebateSummaryGrid_BeforeOpen);
			this.RebateSummaryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.RebateSummaryGrid_FormatText);
			this.RebateSummaryGrid.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Group Code\"" +
				" DataField=\"GroupCode\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo " +
				"/></C1DataColumn><C1DataColumn Level=\"0\" Caption=\"Charge\" DataField=\"ClientCharg" +
				"e\" NumberFormat=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1" +
				"DataColumn Level=\"0\" Caption=\"Borrow Charge\" DataField=\"BorrowCharge\" NumberForm" +
				"at=\"FormatText Event\"><ValueItems /><GroupInfo /></C1DataColumn><C1DataColumn Le" +
				"vel=\"0\" Caption=\"Shorted Amount\" DataField=\"ShortedAmount\" NumberFormat=\"FormatT" +
				"ext Event\"><ValueItems /><GroupInfo /></C1DataColumn></DataCols><Styles type=\"C1" +
				".Win.C1TrueDBGrid.Design.ContextWrapper\"><Data>Style787{}Style638{}Caption{Align" +
				"Horz:Center;}Style639{}Normal{Font:Verdana, 8.25pt;}Style25{}Selected{ForeColor:" +
				"HighlightText;BackColor:Highlight;}Editor{}Style632{}Style18{}Style19{}Style14{}" +
				"Style15{}Style16{AlignHorz:Far;BackColor:White;}Style17{Font:Microsoft Sans Seri" +
				"f, 8.25pt, style=Bold;BackColor:255, 251, 242;}Style10{}Style11{}OddRow{}Style13" +
				"{AlignHorz:Near;}Style642{}Style641{}Style640{AlignHorz:Near;}Footer{Border:None" +
				",,0, 0, 0, 0;BackColor:Window;}HighlightRow{ForeColor:HighlightText;BackColor:Hi" +
				"ghlight;}Style26{}RecordSelector{AlignImage:Center;}Style24{}Style23{BackColor:2" +
				"55, 251, 242;}Style22{AlignHorz:Far;}Style21{AlignHorz:Near;}Style20{}Group{Alig" +
				"nVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Inactive{ForeColor:I" +
				"nactiveCaptionText;BackColor:InactiveCaption;}EvenRow{BackColor:Aqua;}Heading{Wr" +
				"ap:True;BackColor:Control;Border:Raised,,1, 1, 1, 1;ForeColor:ControlText;AlignV" +
				"ert:Center;}Style631{Font:Verdana, 8.25pt;}Style633{}Style634{Font:Verdana, 8.25" +
				"pt;BackColor:WhiteSmoke;}Style635{}Style636{}Style637{}FilterBar{BackColor:SeaSh" +
				"ell;AlignVert:Center;}Style5{}Style9{Font:Microsoft Sans Serif, 8.25pt, style=Bo" +
				"ld;BackColor:255, 251, 242;}Style8{AlignHorz:Far;}Style12{}Style4{}Style7{AlignH" +
				"orz:Near;}Style6{}Style1{AlignHorz:Near;}Style3{Font:Microsoft Sans Serif, 8.25p" +
				"t, style=Bold;BackColor:255, 251, 242;}Style2{Font:Verdana, 8.25pt;AlignHorz:Nea" +
				"r;BackColor:Snow;}</Data></Styles><Splits><C1.Win.C1TrueDBGrid.MergeView HBarSty" +
				"le=\"None\" VBarStyle=\"Always\" AllowColSelect=\"False\" Name=\"Split[0,0]\" AllowRowSi" +
				"zing=\"None\" AllowVerticalSizing=\"False\" CaptionHeight=\"18\" ColumnCaptionHeight=\"" +
				"18\" ColumnFooterHeight=\"18\" ExtendRightColumn=\"True\" FetchRowStyles=\"True\" Filte" +
				"rBar=\"True\" MarqueeStyle=\"DottedRowBorder\" RecordSelectorWidth=\"16\" DefRecSelWid" +
				"th=\"16\" VerticalScrollGroup=\"1\" MinWidth=\"100\" HorizontalScrollGroup=\"5\"><Captio" +
				"n>Rebate Summary</Caption><CaptionStyle parent=\"Heading\" me=\"Style640\" /><Editor" +
				"Style parent=\"Editor\" me=\"Style632\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style63" +
				"8\" /><FilterBarStyle parent=\"FilterBar\" me=\"Style787\" /><FooterStyle parent=\"Foo" +
				"ter\" me=\"Style634\" /><GroupStyle parent=\"Group\" me=\"Style642\" /><HeadingStyle pa" +
				"rent=\"Heading\" me=\"Style633\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Styl" +
				"e637\" /><InactiveStyle parent=\"Inactive\" me=\"Style636\" /><OddRowStyle parent=\"Od" +
				"dRow\" me=\"Style639\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style641\"" +
				" /><SelectedStyle parent=\"Selected\" me=\"Style635\" /><Style parent=\"Normal\" me=\"S" +
				"tyle631\" /><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"St" +
				"yle1\" /><Style parent=\"Style631\" me=\"Style2\" /><FooterStyle parent=\"Style634\" me" +
				"=\"Style3\" /><EditorStyle parent=\"Style632\" me=\"Style4\" /><GroupHeaderStyle paren" +
				"t=\"Style631\" me=\"Style6\" /><GroupFooterStyle parent=\"Style631\" me=\"Style5\" /><Vi" +
				"sible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Height>15</H" +
				"eight><Locked>True</Locked><DCIdx>0</DCIdx></C1DisplayColumn><C1DisplayColumn><H" +
				"eadingStyle parent=\"Style633\" me=\"Style21\" /><Style parent=\"Style631\" me=\"Style2" +
				"2\" /><FooterStyle parent=\"Style634\" me=\"Style23\" /><EditorStyle parent=\"Style632" +
				"\" me=\"Style24\" /><GroupHeaderStyle parent=\"Style631\" me=\"Style26\" /><GroupFooter" +
				"Style parent=\"Style631\" me=\"Style25\" /><Visible>True</Visible><ColumnDivider>Lig" +
				"htGray,Single</ColumnDivider><Width>295</Width><Height>15</Height><Locked>True</" +
				"Locked><DCIdx>3</DCIdx></C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"" +
				"Style633\" me=\"Style7\" /><Style parent=\"Style631\" me=\"Style8\" /><FooterStyle pare" +
				"nt=\"Style634\" me=\"Style9\" /><EditorStyle parent=\"Style632\" me=\"Style10\" /><Group" +
				"HeaderStyle parent=\"Style631\" me=\"Style12\" /><GroupFooterStyle parent=\"Style631\"" +
				" me=\"Style11\" /><Visible>True</Visible><ColumnDivider>LightGray,Single</ColumnDi" +
				"vider><Width>479</Width><Height>15</Height><Locked>True</Locked><DCIdx>2</DCIdx>" +
				"</C1DisplayColumn><C1DisplayColumn><HeadingStyle parent=\"Style633\" me=\"Style13\" " +
				"/><Style parent=\"Style631\" me=\"Style16\" /><FooterStyle parent=\"Style634\" me=\"Sty" +
				"le17\" /><EditorStyle parent=\"Style632\" me=\"Style18\" /><GroupHeaderStyle parent=\"" +
				"Style631\" me=\"Style20\" /><GroupFooterStyle parent=\"Style631\" me=\"Style19\" /><Vis" +
				"ible>True</Visible><ColumnDivider>LightGray,Single</ColumnDivider><Width>250</Wi" +
				"dth><Height>15</Height><Locked>True</Locked><DCIdx>1</DCIdx></C1DisplayColumn></" +
				"internalCols><ClientRect>0, 0, 1280, 727</ClientRect><BorderSide>Right</BorderSi" +
				"de></C1.Win.C1TrueDBGrid.MergeView></Splits><NamedStyles><Style parent=\"\" me=\"No" +
				"rmal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer" +
				"\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\"" +
				" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"Editor\" /><" +
				"Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" />" +
				"<Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\"" +
				" /><Style parent=\"Normal\" me=\"FilterBar\" /><Style parent=\"Caption\" me=\"Group\" />" +
				"</NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modifi" +
				"ed</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth><ClientArea>0, 0, 1280, 72" +
				"7</ClientArea><PrintPageHeaderStyle parent=\"\" me=\"Style14\" /><PrintPageFooterSty" +
				"le parent=\"\" me=\"Style15\" /></Blob>";
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										this.MainActionsMenuItem,
																																										this.MainSendToMenuItem,
																																										this.MainDockMenuItem,
																																										this.menuItem24,
																																										this.MainExitMenuItem});
			// 
			// MainActionsMenuItem
			// 
			this.MainActionsMenuItem.Index = 0;
			this.MainActionsMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																												this.MainActionCreateBillsMenuItem,
																																												this.MainActionDatesMenuItem,
																																												this.MainActionRerunMarksMenuItem,
																																												this.MainActionsCreditDebitAccountsMenuItem,
																																												this.MainActionSetMarkUpsMenuItem});
			this.MainActionsMenuItem.Text = "Actions";
			// 
			// MainActionCreateBillsMenuItem
			// 
			this.MainActionCreateBillsMenuItem.Index = 0;
			this.MainActionCreateBillsMenuItem.Text = "Create Bill(s)";
			this.MainActionCreateBillsMenuItem.Click += new System.EventHandler(this.ActionCreateBillsMenuItem_Click);
			// 
			// MainActionDatesMenuItem
			// 
			this.MainActionDatesMenuItem.Index = 1;
			this.MainActionDatesMenuItem.Text = "Dates";
			this.MainActionDatesMenuItem.Click += new System.EventHandler(this.ActionDatesMenuItem_Click);
			// 
			// MainActionRerunMarksMenuItem
			// 
			this.MainActionRerunMarksMenuItem.Index = 2;
			this.MainActionRerunMarksMenuItem.Text = "Rerun MarkUps";
			this.MainActionRerunMarksMenuItem.Click += new System.EventHandler(this.ActionRerunMarksMenuItem_Click);
			// 
			// MainActionsCreditDebitAccountsMenuItem
			// 
			this.MainActionsCreditDebitAccountsMenuItem.Enabled = false;
			this.MainActionsCreditDebitAccountsMenuItem.Index = 3;
			this.MainActionsCreditDebitAccountsMenuItem.Text = "Credit/Debit Accounts";
			// 
			// MainActionSetMarkUpsMenuItem
			// 
			this.MainActionSetMarkUpsMenuItem.Index = 4;
			this.MainActionSetMarkUpsMenuItem.Text = "Set MarkUps";
			this.MainActionSetMarkUpsMenuItem.Click += new System.EventHandler(this.ActionSetMarkUpsMenuItem_Click);
			// 
			// MainSendToMenuItem
			// 
			this.MainSendToMenuItem.Index = 1;
			this.MainSendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																											 this.MainSendToClipboardMenuItem,
																																											 this.MainSendToExcelMenuItem,
																																											 this.MainSendToMailRecipientMenuItem});
			this.MainSendToMenuItem.Text = "Send To";
			// 
			// MainSendToClipboardMenuItem
			// 
			this.MainSendToClipboardMenuItem.Index = 0;
			this.MainSendToClipboardMenuItem.Text = "Clipboard";
			this.MainSendToClipboardMenuItem.Click += new System.EventHandler(this.MainSendToClipboardMenuItem_Click);
			// 
			// MainSendToExcelMenuItem
			// 
			this.MainSendToExcelMenuItem.Index = 1;
			this.MainSendToExcelMenuItem.Text = "Excel";
			this.MainSendToExcelMenuItem.Click += new System.EventHandler(this.MainSendToExcelMenuItem_Click);
			// 
			// MainSendToMailRecipientMenuItem
			// 
			this.MainSendToMailRecipientMenuItem.Index = 2;
			this.MainSendToMailRecipientMenuItem.Text = "Mail Recipient";
			this.MainSendToMailRecipientMenuItem.Click += new System.EventHandler(this.MainSendToMailRecipientMenuItem_Click);
			// 
			// MainDockMenuItem
			// 
			this.MainDockMenuItem.Index = 2;
			this.MainDockMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																										 this.MainDockTopMenuItem,
																																										 this.MainDockBottomMenuItem,
																																										 this.menuItem16,
																																										 this.MainDockNoneMenuItem});
			this.MainDockMenuItem.Text = "Dock";
			// 
			// MainDockTopMenuItem
			// 
			this.MainDockTopMenuItem.Index = 0;
			this.MainDockTopMenuItem.Text = "Top";
			this.MainDockTopMenuItem.Click += new System.EventHandler(this.DockTopMenuItem_Click);
			// 
			// MainDockBottomMenuItem
			// 
			this.MainDockBottomMenuItem.Index = 1;
			this.MainDockBottomMenuItem.Text = "Bottom";
			this.MainDockBottomMenuItem.Click += new System.EventHandler(this.DockBottomMenuItem_Click);
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 2;
			this.menuItem16.Text = "-";
			// 
			// MainDockNoneMenuItem
			// 
			this.MainDockNoneMenuItem.Index = 3;
			this.MainDockNoneMenuItem.Text = "None";
			this.MainDockNoneMenuItem.Click += new System.EventHandler(this.DockNoneMenuItem_Click);
			// 
			// menuItem24
			// 
			this.menuItem24.Index = 3;
			this.menuItem24.Text = "-";
			// 
			// MainExitMenuItem
			// 
			this.MainExitMenuItem.Index = 4;
			this.MainExitMenuItem.Text = "Exit";
			this.MainExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
			// 
			// ShortSaleBillingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1284, 785);
			this.ContextMenu = this.ChildContextMenu;
			this.Controls.Add(this.RefreshButton);
			this.Controls.Add(this.GroupCodeCombo);
			this.Controls.Add(this.SummaryGrid);
			this.Controls.Add(this.RebateSummaryGrid);
			this.Controls.Add(this.GroupCodeLabel);
			this.Controls.Add(this.ToLabel);
			this.Controls.Add(this.FromLabel);
			this.Controls.Add(this.FromDatePicker);
			this.Controls.Add(this.ToDatePicker);
			this.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.DockPadding.Bottom = 1;
			this.DockPadding.Left = 1;
			this.DockPadding.Right = 1;
			this.DockPadding.Top = 55;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ShortSaleBillingForm";
			this.Text = "ShortSale - Negative Rebate Billing Summary";
			this.Resize += new System.EventHandler(this.ShortSaleBillingForm_Resize);
			this.Load += new System.EventHandler(this.ShortSaleBillingForm_Load);
			this.Closed += new System.EventHandler(this.ShortSaleBillingForm_Closed);
			((System.ComponentModel.ISupportInitialize)(this.FromLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ToLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SummaryGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GroupCodeCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RebateSummaryGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ShortSaleBillingForm_Load(object sender, System.EventArgs e)
		{
			int height = mainForm.Height / 2;
			int width  = mainForm.Width / 2;
      
			try
			{
				this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
				this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
				this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
				this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));
       
				tradingGroupsDataSet = mainForm.ShortSaleAgent.TradingGroupsGet(mainForm.ShortSaleAgent.TradeDate(), mainForm.UtcOffset);

				GroupCodeCombo.Text = "[Please select...]";				
								
				rebateSummaryDataSet = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
					FromDatePicker.Text, 
					ToDatePicker.Text, 
					"");
		
				RebateSummaryGrid.SetDataBinding(rebateSummaryDataSet, "CorrespondentSummary", true);				
				
				GroupCodeCombo.HoldFields();
				GroupCodeCombo.DataSource = tradingGroupsDataSet;
				GroupCodeCombo.DataMember = "TradingGroups";
			
				RebateSumaryFooterSet();
			}
			catch(Exception ee)
			{
				mainForm.Alert(ee.Message, PilotState.RunFault);        
			}
		}

		private void ShortSaleBillingForm_Closed(object sender, System.EventArgs e)
		{
			if  (shortSaleBillingDatesLockInputForm != null)
			{
				shortSaleBillingDatesLockInputForm.Close();
			}

			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
				RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
				RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
				RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
			}

		//	try { shortSaleBillingBillsAccountsStatusForm.Close();} catch {}																											 
			try { shortSaleBillingDatesLockInputForm.Close();} catch {}
			try { shortSaleBillingMarksInputForm.Close();} catch{}
			try { shortSaleBillingBillsForm.Close();} catch{}
			try { shortSaleTradingGroupsNegativeRebatesBillingForm.Close();} catch {}			
		}

		private void RebateSumaryFooterSet()
		{					
			decimal borrowCharge = 0;
			decimal clientCharge = 0;

			for(int index = 0; index < RebateSummaryGrid.Splits[0].Rows.Count; index++)
			{				
				try
				{					
					if (!RebateSummaryGrid.Columns["BorrowCharge"].CellValue(index).ToString().Equals(""))
						borrowCharge += (decimal) RebateSummaryGrid.Columns["BorrowCharge"].CellValue(index);
				} 
				catch {}	
				try
				{					
					if (!RebateSummaryGrid.Columns["ClientCharge"].CellValue(index).ToString().Equals(""))
						clientCharge += (decimal) RebateSummaryGrid.Columns["ClientCharge"].CellValue(index);					
				} 
				catch {}	
			}
	
		
			RebateSummaryGrid.Columns["BorrowCharge"].FooterText = borrowCharge.ToString("#,##0.00");
			RebateSummaryGrid.Columns["ClientCharge"].FooterText = clientCharge.ToString("#,##0.00");			
		}

		private void SummaryFooterSet()
		{					
			decimal originalCharge = 0;
			decimal modifiedCharge = 0;

			for(int index = 0; index < SummaryGrid.Splits[0].Rows.Count; index++)
			{				
				try
				{					
					if (!SummaryGrid.Columns["OriginalCharge"].CellValue(index).ToString().Equals(""))
						originalCharge += (decimal) SummaryGrid.Columns["OriginalCharge"].CellValue(index);
				} 
				catch {}	
				try
				{					
					if (!SummaryGrid.Columns["ModifiedCharge"].CellValue(index).ToString().Equals(""))
						modifiedCharge += (decimal) SummaryGrid.Columns["ModifiedCharge"].CellValue(index);					
				} 
				catch {}	
			}
	
		
			SummaryGrid.Columns["OriginalCharge"].FooterText = originalCharge.ToString("#,##0.00");
			SummaryGrid.Columns["ModifiedCharge"].FooterText = modifiedCharge.ToString("#,##0.00");			
		}
		
		private void DockTopMenuItem_Click(object sender, System.EventArgs e)
		{
			this.FormBorderStyle = FormBorderStyle.Sizable;
			this.Dock = DockStyle.Top;
			this.ControlBox = false;
			this.Text = "";
		}

		private void DockBottomMenuItem_Click(object sender, System.EventArgs e)
		{
			this.FormBorderStyle = FormBorderStyle.Sizable;
			this.Dock = DockStyle.Bottom;
			this.ControlBox = false;
			this.Text = "";
		}

		private void DockNoneMenuItem_Click(object sender, System.EventArgs e)
		{
			this.FormBorderStyle = FormBorderStyle.Sizable;
			this.Dock = DockStyle.None;
			this.ControlBox = true;
			this.Text = TEXT;
		}

		private void SummaryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0)
			{
				return;
			}
  
			try
			{
				switch(SummaryGrid.Columns[e.ColIndex].DataField)
				{					
					case ("QuantityCovered"):
					case ("QuantityShorted"):
					case ("QuantityUncovered"):
						e.Value = long.Parse(e.Value).ToString("#,##0");
						break;					
				
					case ("MarkupRate"):
					case ("Rate"):
						e.Value = decimal.Parse(e.Value).ToString("#0.000");
						break;					
									
					case ("Price"):
						e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
						break;					
					
					case ("BizDate"):
					case ("SettlementDate"):
						e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateFormat);
						break;

					case ("OriginalCharge"):
						e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
						break;					
					
					case ("ModifiedCharge"):
						e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
						break;														
				}
			}
			catch {}
		}		
		

		private void RebateSummaryGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0)
			{
				return;
			}
  
			try
			{
				switch(RebateSummaryGrid.Columns[e.ColIndex].DataField)
				{										
					case ("ShortedAmount"):
					case ("BorrowCharge"):
					case ("ClientCharge"):
						e.Value = decimal.Parse(e.Value).ToString("#,##0.00");
						break;																		
				}
			}
			catch {}
		}

		private void SummaryGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{			
			if (SummaryGrid.Columns["IsLocked"].CellValue(e.Row).ToString().Equals("True"))
			{
				e.CellStyle.ForeColor = System.Drawing.Color.Gray;
			}
			else
			{
				e.CellStyle.ForeColor = System.Drawing.Color.Black;
			}

			if (SummaryGrid.Columns["PreBorrow"].CellValue(e.Row).ToString().Equals("True"))
			{
				e.CellStyle.BackColor = System.Drawing.Color.BlanchedAlmond;
			}						
		}

		private void GroupCodeCombo_TextChanged(object sender, System.EventArgs e)
		{
			
			this.Cursor = Cursors.WaitCursor;

			try
			{
				rebateSummaryDataSet = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
					FromDatePicker.Text, 
					ToDatePicker.Text, 
					GroupCodeCombo.Text);
		
				RebateSummaryGrid.SetDataBinding(summaryDataSet, "CorrespondentSummary", true);				
				RebateSumaryFooterSet();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);	
			}

			this.Cursor = Cursors.Default;
		}

		private void ActionRerunMarksMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				shortSaleBillingMarksInputForm = new ShortSaleBillingMarksInputForm(mainForm,FromDatePicker.Text, ToDatePicker.Text, ((GroupCodeCombo.Text.Equals("[Please select...]")?"":GroupCodeCombo.Text)), "");
				shortSaleBillingMarksInputForm.MdiParent = mainForm;
				shortSaleBillingMarksInputForm.Show();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);	
			}
		}					

		private void FromDatePicker_ValueChanged(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				rebateSummaryDataSet = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
					FromDatePicker.Text, 
					ToDatePicker.Text, 
					"");
		
				RebateSummaryGrid.SetDataBinding(rebateSummaryDataSet, "CorrespondentSummary", true);				
				RebateSumaryFooterSet();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);	
			}

			this.Cursor = Cursors.Default;
		}

		private void ToDatePicker_ValueChanged(object sender, System.EventArgs e)
		{			
			this.Cursor = Cursors.WaitCursor;

			try
			{
				rebateSummaryDataSet = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
					FromDatePicker.Text, 
					ToDatePicker.Text, 
					"");
		
				RebateSummaryGrid.SetDataBinding(rebateSummaryDataSet, "CorrespondentSummary", true);				
				RebateSumaryFooterSet();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);	
			}

			this.Cursor = Cursors.Default;
		}		

		private void SummaryGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{
				if (!SummaryGrid.Columns["SecId"].Text.Equals(secId))
				{
					secId = SummaryGrid.Columns["SecId"].Text;
					mainForm.SecId = secId;
				}

				if (bool.Parse(SummaryGrid.Columns["IsLocked"].Value.ToString()))
				{
					if (editableMaster)
					{
						SummaryGrid.AllowUpdate = false;
					}					
				}
				else
				{
					if (editableMaster)
					{
						SummaryGrid.AllowUpdate = editable;
					}					
				}

			}	
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);	
			}
		}
	

		private void ActionCreateBillsMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (!HasInstance(typeof(ShortSaleBillingBillsForm)))
				{
					shortSaleBillingBillsForm = new ShortSaleBillingBillsForm(mainForm, FromDatePicker.Text, ToDatePicker.Text,((GroupCodeCombo.Text.Equals("[Please select...]")?"":GroupCodeCombo.Text))) ;
					shortSaleBillingBillsForm.MdiParent = mainForm;
					shortSaleBillingBillsForm.Show();
				}
				else
				{
					shortSaleBillingBillsForm.Activate();
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);	
			}
		}

		private void ActionDatesMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (!HasInstance(typeof(ShortSaleBillingDatesLockInputForm)))
				{
					shortSaleBillingDatesLockInputForm = new ShortSaleBillingDatesLockInputForm(mainForm, FromDatePicker.Text, ToDatePicker.Text,((GroupCodeCombo.Text.Equals("[Please select...]")?"":GroupCodeCombo.Text)), "");
					shortSaleBillingDatesLockInputForm.MdiParent = mainForm;
					shortSaleBillingDatesLockInputForm.Show();
				}
				else
				{
					shortSaleBillingDatesLockInputForm.Activate();
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);	
			}
		}

		private void SummaryGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			DataSet rowDataSet = new DataSet();

			try
			{
				mainForm.RebateAgent.ShortSaleBillingSummaryRecordSet(
					SummaryGrid.Columns["ProcessId"].Text,
					SummaryGrid.Columns["BizDate"].Text,
					SummaryGrid.Columns["Rate"].Value.ToString(),
					SummaryGrid.Columns["Price"].Value.ToString(),
					SummaryGrid.Columns["OriginalCharge"].Value.ToString(),
					SummaryGrid.Columns["Comment"].Value.ToString(),
					mainForm.UserId);

				rowDataSet = mainForm.RebateAgent.ShortSaleBillingSummaryRecordGet(
					SummaryGrid.Columns["ProcessId"].Text,
					SummaryGrid.Columns["BizDate"].Text);				
							
				foreach (DataRow dr in rowDataSet.Tables["BillingRecord"].Rows)
				{					
					summaryDataSet.Tables["BillingSummary"].LoadDataRow(dr.ItemArray, true);
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);	
				e.Cancel = true;
			}
		}

		private void SummaryGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			try
			{
				if (e.KeyChar.Equals((char)13))
				{
					SummaryGrid.UpdateData();
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void RefreshButton_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
				rebateSummaryDataSet = mainForm.RebateAgent.ShortSaleBillingCorrespondentSummaryGet(
					FromDatePicker.Text, 
					ToDatePicker.Text, 
					"");
		
				RebateSummaryGrid.SetDataBinding(rebateSummaryDataSet, "CorrespondentSummary", true);				
				RebateSumaryFooterSet();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);	
			}

			this.Cursor = Cursors.Default;
		}

		private bool HasInstance(Type formType)
		{
			if(mainForm.MdiChildren.Length > 0)
			{
				for(int i = 0; i < mainForm.MdiChildren.Length; i++)
				{
					if(mainForm.MdiChildren[i].Name.Equals(formType.Name))
					{
						return true;
					}
				}
			}

			return false;
		}

		private void ShowSettlementDateMenuItem_Click(object sender, System.EventArgs e)
		{
			SummaryGrid.Splits[0].DisplayColumns["SettlementDate"].Visible = !ShowSettlementDateMenuItem.Checked;
			ShowSettlementDateMenuItem.Checked = !ShowSettlementDateMenuItem.Checked;			
		}

		private void ShowPriceMenuItem_Click(object sender, System.EventArgs e)
		{
			SummaryGrid.Splits[0].DisplayColumns["Price"].Visible = !ShowPriceMenuItem.Checked;
			ShowPriceMenuItem.Checked = !ShowPriceMenuItem.Checked;			
		}

		private void ShowOriginalCharge_Click(object sender, System.EventArgs e)
		{
			SummaryGrid.Splits[0].DisplayColumns["OriginalCharge"].Visible = !ShowOriginalChargeMenuItem.Checked;
			ShowOriginalChargeMenuItem.Checked = !ShowOriginalChargeMenuItem.Checked;			
		}

		private void ShowCommentMenuItem_Click(object sender, System.EventArgs e)
		{
			SummaryGrid.Splits[0].DisplayColumns["Comment"].Visible = !ShowCommentMenuItem.Checked;
			ShowCommentMenuItem.Checked = !ShowCommentMenuItem.Checked;		
		}

		private void SummaryGrid_Error(object sender, C1.Win.C1TrueDBGrid.ErrorEventArgs e)
		{
			e.Handled = true;
		}

		private void SendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (SummaryGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
				return;
			}

			try
			{
				maxTextLength = new int[SummaryGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SummaryGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in SummaryGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SummaryGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SummaryGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SummaryGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in SummaryGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SummaryGrid.SelectedCols)
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
				
				mainForm.Alert("Total: " + SummaryGrid.SelectedRows.Count + " items added to clipboard.");
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

			if (SummaryGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
				return;
			}

			try
			{
				maxTextLength = new int[SummaryGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SummaryGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in SummaryGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SummaryGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SummaryGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SummaryGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in SummaryGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in SummaryGrid.SelectedCols)
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

				mainForm.Alert("Total: " + SummaryGrid.SelectedRows.Count + " items added to e-mail.");
			}
			catch (Exception error)
			{       				
				mainForm.Alert(error.Message, PilotState.RunFault);
			}		
		}

		private void SummaryGrid_AfterFilter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
		{
			SummaryFooterSet();
		}

		private void ShowMarkupRateMenuItem_Click(object sender, System.EventArgs e)
		{
			SummaryGrid.Splits[0].DisplayColumns["MarkupRate"].Visible = !ShowMarkupRateMenuItem.Checked;
			ShowMarkupRateMenuItem.Checked = !ShowMarkupRateMenuItem.Checked;		
		}

		private void CreditDebitAccountsMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
			/*	if (!HasInstance(typeof(ShortSaleBillingBillsAccountsStatusForm)))
				{
					shortSaleBillingBillsAccountsStatusForm = new ShortSaleBillingBillsAccountsStatusForm(mainForm, "");
					shortSaleBillingBillsAccountsStatusForm.MdiParent = mainForm;
					shortSaleBillingBillsAccountsStatusForm.Show();
				}
				else
				{
					shortSaleBillingBillsAccountsStatusForm.Activate();
				}*/
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);	
			}
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			Excel excel = new Excel();
			excel.ExportGridToExcel(ref SummaryGrid);
		}

		private void ActionSetMarkUpsMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (!HasInstance(typeof(ShortSaleTradingGroupsNegativeRebatesBillingForm)))
				{
					shortSaleTradingGroupsNegativeRebatesBillingForm = new ShortSaleTradingGroupsNegativeRebatesBillingForm(mainForm);
					shortSaleTradingGroupsNegativeRebatesBillingForm.MdiParent = mainForm;
					shortSaleTradingGroupsNegativeRebatesBillingForm.Show();
				}
				else
				{
					shortSaleTradingGroupsNegativeRebatesBillingForm.Activate();
				}
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

		private void RebateSummaryGrid_BeforeOpen(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			SummaryGrid.Width = RebateSummaryGrid.Width - 50;
			
			try
			{
				summaryDataSet = mainForm.RebateAgent.ShortSaleBillingSummaryGet(FromDatePicker.Text, ToDatePicker.Text, RebateSummaryGrid.Columns["GroupCode"].Text, "");
				SummaryGrid.SetDataBinding(summaryDataSet, "BillingSummary", true);
				SummaryFooterSet();
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);	
			}
		}

		private void MainSendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (RebateSummaryGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
				return;
			}

			try
			{
				maxTextLength = new int[RebateSummaryGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RebateSummaryGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in RebateSummaryGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RebateSummaryGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RebateSummaryGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RebateSummaryGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in RebateSummaryGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RebateSummaryGrid.SelectedCols)
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
				
				mainForm.Alert("Total: " + RebateSummaryGrid.SelectedRows.Count + " items added to clipboard.");
			}
			catch (Exception error)
			{       				
				mainForm.Alert(error.Message, PilotState.RunFault);
			}		
		}

		private void MainSendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			Excel excel = new Excel();
			excel.ExportGridToExcel(ref RebateSummaryGrid);
		}

		private void MainSendToMailRecipientMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (RebateSummaryGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.");
				return;
			}

			try
			{
				maxTextLength = new int[RebateSummaryGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RebateSummaryGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in RebateSummaryGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RebateSummaryGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RebateSummaryGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RebateSummaryGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in RebateSummaryGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in RebateSummaryGrid.SelectedCols)
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

				mainForm.Alert("Total: " + RebateSummaryGrid.SelectedRows.Count + " items added to e-mail.");
			}
			catch (Exception error)
			{       				
				mainForm.Alert(error.Message, PilotState.RunFault);
			}		
		}

		private void ShortSaleBillingForm_Resize(object sender, System.EventArgs e)
		{
			SummaryGrid.Width = RebateSummaryGrid.Width - 50;
		}

		private void ShowRateMenuItem_Click(object sender, System.EventArgs e)
		{
			SummaryGrid.Splits[0].DisplayColumns["Rate"].Visible = !ShowRateMenuItem.Checked;
			ShowRateMenuItem.Checked = !ShowRateMenuItem.Checked;		
		}

		private void MainShowChangeHistoryMenuItem_Click(object sender, System.EventArgs e)
		{						
		try
			{
				/*if (!HasInstance(typeof(ShortSaleBillingBillsAccountsStatusForm)))
				{
					shortSaleBillingSummaryActivityForm = new ShortSaleBillingSummaryActivityForm(RebateSummaryGrid.Columns["GroupCode"].Text, mainForm);
					shortSaleBillingSummaryActivityForm.MdiParent = mainForm;
					shortSaleBillingSummaryActivityForm.Show();
				}
				else
				{
					shortSaleBillingSummaryActivityForm.Activate();
				}*/
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}		
		}			
	}
}

