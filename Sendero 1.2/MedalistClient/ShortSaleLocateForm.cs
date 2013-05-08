// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class ShortSaleLocateForm : System.Windows.Forms.Form
	{ 
		const int QUANTITY = 9;
		const int SOURCE = 10;
		const int FEE_RATE = 12;
		const int PRE_BORROW = 13;

		private bool mayEdit = false;		
		private string secId = "";		
		private string groupCode = "";
		private MainForm mainForm;
	
		private ArrayList locateEventArgsArray;

		private DataSet mainDataSet = null;
		private DataSet inventoryDataSet = null;

		private DataView locatesDataView, locateSummaryDataView, tradingGroupDataView;
    
		private System.Windows.Forms.Label ClientCommentLabel;
		private System.Windows.Forms.TextBox ClientCommentText;
		private System.Windows.Forms.CheckBox EnableResearchCheck;
    
		private System.Windows.Forms.Button SubmitButton;

		private C1.Win.C1Input.C1Label TradeDateLabel;
		private C1.Win.C1List.C1Combo TradeDateCombo;
    
		private C1.Win.C1Input.C1Label CommentLabel;
		private C1.Win.C1Input.C1TextBox CommentTextBox;

		private C1.Win.C1Input.C1Label RequestLabel;
		private C1.Win.C1Input.C1TextBox RequestTextBox;

		private C1.Win.C1List.C1Combo TradingGroupCombo;
    
		private C1.Win.C1Input.C1Label SecIdLabel;
		private C1.Win.C1Input.C1TextBox SecIdTextBox;
    
		private C1.Win.C1Input.C1Label YearLabel;
		private C1.Win.C1Input.C1NumericEdit YearNumericEdit;
    
		private C1.Win.C1Input.C1Label QuarterLabel;
		private C1.Win.C1Input.C1NumericEdit QuarterNumericEdit;

		private C1.Win.C1Input.C1Label StatusLabel;
		private C1.Win.C1Input.C1TextBox StatusTextBox;

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid LocatesGrid;
    
		private C1.Win.C1List.C1List InventoryList;
		private C1.Win.C1List.C1List LocateSummaryList;

		private System.Windows.Forms.Label GroupNameLabel;
		private System.Windows.Forms.TextBox GroupNameText;

		private System.Windows.Forms.ContextMenu MainContextMenu;
    
		private System.Windows.Forms.MenuItem SendToMenuItem;
		private System.Windows.Forms.MenuItem SendToEmailMenuItem;
		private System.Windows.Forms.MenuItem Sep1MenuItem;
		private System.Windows.Forms.MenuItem ExitMenuItem;
		private System.Timers.Timer RefreshTimer;
		private System.Windows.Forms.CheckBox AutoUpdateCheckBox;
		private System.Windows.Forms.Button ShowPendingButton;
		private System.Windows.Forms.Button ShowAllButton;
		private System.Windows.Forms.MenuItem SendToExcelMenuItem;
		internal System.Windows.Forms.MenuItem AttachedMenuItem;
		private System.Windows.Forms.CheckBox AutoUpdateAllCheckBox;
		private System.Timers.Timer UpdateAllRefreshTimer;
		private System.Windows.Forms.MenuItem ActionMenuItem;
		private System.Windows.Forms.MenuItem ActionZeroLocatesMenuItem;
		private System.Windows.Forms.MenuItem ShowMenuItem;
		private System.Windows.Forms.MenuItem ShowPreBorrowsMenuItem;
		public ShortSaleLocatesPreBorrowForm shortSaleLocatesPreBorrowForm;

		private System.ComponentModel.IContainer components;

		public ShortSaleLocateForm(MainForm mainForm)
		{
			this.mainForm = mainForm;

			locateEventArgsArray = new ArrayList();

			InitializeComponent();

			try
			{
				TradeDateCombo.Tag = mainForm.ShortSaleAgent.TradeDate();

				if (mayEdit = mainForm.AdminAgent.MayEdit(mainForm.UserId, "ShortSaleLocates"))
				{
					LocatesGrid.AllowUpdate = true;
					CommentTextBox.ReadOnly = false;
					RequestTextBox.ReadOnly = false;
					TradingGroupCombo.ReadOnly = false;
				}

				RefreshTimer.Interval = double.Parse(Standard.ConfigValue("ShortSaleRefreshInterval", "30")) * 1000;
			}
			catch(Exception e)
			{
				mainForm.Alert(e.Message, PilotState.RunFault);
			}
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

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortSaleLocateForm));
            this.LocatesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.MainContextMenu = new System.Windows.Forms.ContextMenu();
            this.ActionMenuItem = new System.Windows.Forms.MenuItem();
            this.ActionZeroLocatesMenuItem = new System.Windows.Forms.MenuItem();
            this.SendToMenuItem = new System.Windows.Forms.MenuItem();
            this.SendToExcelMenuItem = new System.Windows.Forms.MenuItem();
            this.SendToEmailMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowMenuItem = new System.Windows.Forms.MenuItem();
            this.ShowPreBorrowsMenuItem = new System.Windows.Forms.MenuItem();
            this.AttachedMenuItem = new System.Windows.Forms.MenuItem();
            this.Sep1MenuItem = new System.Windows.Forms.MenuItem();
            this.ExitMenuItem = new System.Windows.Forms.MenuItem();
            this.ClientCommentText = new System.Windows.Forms.TextBox();
            this.ClientCommentLabel = new System.Windows.Forms.Label();
            this.CommentTextBox = new C1.Win.C1Input.C1TextBox();
            this.CommentLabel = new C1.Win.C1Input.C1Label();
            this.RequestTextBox = new C1.Win.C1Input.C1TextBox();
            this.RequestLabel = new C1.Win.C1Input.C1Label();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.GroupNameLabel = new System.Windows.Forms.Label();
            this.GroupNameText = new System.Windows.Forms.TextBox();
            this.StatusLabel = new C1.Win.C1Input.C1Label();
            this.StatusTextBox = new C1.Win.C1Input.C1TextBox();
            this.EnableResearchCheck = new System.Windows.Forms.CheckBox();
            this.SecIdTextBox = new C1.Win.C1Input.C1TextBox();
            this.SecIdLabel = new C1.Win.C1Input.C1Label();
            this.YearNumericEdit = new C1.Win.C1Input.C1NumericEdit();
            this.YearLabel = new C1.Win.C1Input.C1Label();
            this.QuarterLabel = new C1.Win.C1Input.C1Label();
            this.QuarterNumericEdit = new C1.Win.C1Input.C1NumericEdit();
            this.TradeDateLabel = new C1.Win.C1Input.C1Label();
            this.InventoryList = new C1.Win.C1List.C1List();
            this.TradeDateCombo = new C1.Win.C1List.C1Combo();
            this.TradingGroupCombo = new C1.Win.C1List.C1Combo();
            this.LocateSummaryList = new C1.Win.C1List.C1List();
            this.RefreshTimer = new System.Timers.Timer();
            this.AutoUpdateCheckBox = new System.Windows.Forms.CheckBox();
            this.ShowPendingButton = new System.Windows.Forms.Button();
            this.ShowAllButton = new System.Windows.Forms.Button();
            this.AutoUpdateAllCheckBox = new System.Windows.Forms.CheckBox();
            this.UpdateAllRefreshTimer = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.LocatesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RequestTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RequestLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecIdTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecIdLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuarterLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuarterNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TradeDateLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TradeDateCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TradingGroupCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocateSummaryList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateAllRefreshTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // LocatesGrid
            // 
            this.LocatesGrid.AllowColSelect = false;
            this.LocatesGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.LocatesGrid.AllowUpdate = false;
            this.LocatesGrid.AlternatingRows = true;
            this.LocatesGrid.BackColor = System.Drawing.SystemColors.ControlDark;
            this.LocatesGrid.CaptionHeight = 17;
            this.LocatesGrid.ContextMenu = this.MainContextMenu;
            this.LocatesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
            this.LocatesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LocatesGrid.EmptyRows = true;
            this.LocatesGrid.ExtendRightColumn = true;
            this.LocatesGrid.FilterBar = true;
            this.LocatesGrid.GroupByAreaVisible = false;
            this.LocatesGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.LocatesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("LocatesGrid.Images"))));
            this.LocatesGrid.Location = new System.Drawing.Point(1, 55);
            this.LocatesGrid.MaintainRowCurrency = true;
            this.LocatesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
            this.LocatesGrid.Name = "LocatesGrid";
            this.LocatesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.LocatesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.LocatesGrid.PreviewInfo.ZoomFactor = 75D;
            this.LocatesGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("LocatesGrid.PrintInfo.PageSettings")));
            this.LocatesGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
            this.LocatesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.LocatesGrid.RowHeight = 17;
            this.LocatesGrid.Size = new System.Drawing.Size(1214, 234);
            this.LocatesGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
            this.LocatesGrid.TabIndex = 1;
            this.LocatesGrid.AfterUpdate += new System.EventHandler(this.LocatesGrid_AfterUpdate);
            this.LocatesGrid.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.LocatesGrid_BeforeColUpdate);
            this.LocatesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.LocatesGrid_BeforeUpdate);
            this.LocatesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.LocatesGrid_FormatText);
            this.LocatesGrid.Error += new C1.Win.C1TrueDBGrid.ErrorEventHandler(this.LocatesGrid_Error);
            this.LocatesGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.LocatesGrid_Paint);
            this.LocatesGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LocatesGrid_KeyPress);
            this.LocatesGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LocatesGrid_MouseDown);
            this.LocatesGrid.PropBag = resources.GetString("LocatesGrid.PropBag");
            // 
            // MainContextMenu
            // 
            this.MainContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ActionMenuItem,
            this.SendToMenuItem,
            this.ShowMenuItem,
            this.AttachedMenuItem,
            this.Sep1MenuItem,
            this.ExitMenuItem});
            // 
            // ActionMenuItem
            // 
            this.ActionMenuItem.Index = 0;
            this.ActionMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ActionZeroLocatesMenuItem});
            this.ActionMenuItem.Text = "Action";
            // 
            // ActionZeroLocatesMenuItem
            // 
            this.ActionZeroLocatesMenuItem.Index = 0;
            this.ActionZeroLocatesMenuItem.Text = "Zero Locate(s)";
            this.ActionZeroLocatesMenuItem.Click += new System.EventHandler(this.ActionZeroLocatesMenuItem_Click);
            // 
            // SendToMenuItem
            // 
            this.SendToMenuItem.Index = 1;
            this.SendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.SendToExcelMenuItem,
            this.SendToEmailMenuItem});
            this.SendToMenuItem.Text = "Send To";
            // 
            // SendToExcelMenuItem
            // 
            this.SendToExcelMenuItem.Index = 0;
            this.SendToExcelMenuItem.Text = "Excel";
            this.SendToExcelMenuItem.Click += new System.EventHandler(this.SendToExcelMenuItem_Click);
            // 
            // SendToEmailMenuItem
            // 
            this.SendToEmailMenuItem.Index = 1;
            this.SendToEmailMenuItem.Text = "Mail Recipient";
            this.SendToEmailMenuItem.Click += new System.EventHandler(this.SendToEmailMenuItem_Click);
            // 
            // ShowMenuItem
            // 
            this.ShowMenuItem.Index = 2;
            this.ShowMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ShowPreBorrowsMenuItem});
            this.ShowMenuItem.Text = "Show";
            // 
            // ShowPreBorrowsMenuItem
            // 
            this.ShowPreBorrowsMenuItem.Index = 0;
            this.ShowPreBorrowsMenuItem.Text = "PreBorrows";
            this.ShowPreBorrowsMenuItem.Click += new System.EventHandler(this.ShowPreBorrowsMenuItem_Click);
            // 
            // AttachedMenuItem
            // 
            this.AttachedMenuItem.Index = 3;
            this.AttachedMenuItem.Text = "Attach";
            this.AttachedMenuItem.Click += new System.EventHandler(this.AttachMenuItem_Click);
            // 
            // Sep1MenuItem
            // 
            this.Sep1MenuItem.Index = 4;
            this.Sep1MenuItem.Text = "-";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Index = 5;
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // ClientCommentText
            // 
            this.ClientCommentText.BackColor = System.Drawing.SystemColors.Control;
            this.ClientCommentText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ClientCommentText.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientCommentText.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ClientCommentText.Location = new System.Drawing.Point(344, 10);
            this.ClientCommentText.Name = "ClientCommentText";
            this.ClientCommentText.ReadOnly = true;
            this.ClientCommentText.Size = new System.Drawing.Size(424, 16);
            this.ClientCommentText.TabIndex = 13;
            this.ClientCommentText.TabStop = false;
            // 
            // ClientCommentLabel
            // 
            this.ClientCommentLabel.Location = new System.Drawing.Point(234, 10);
            this.ClientCommentLabel.Name = "ClientCommentLabel";
            this.ClientCommentLabel.Size = new System.Drawing.Size(104, 16);
            this.ClientCommentLabel.TabIndex = 12;
            this.ClientCommentLabel.Text = "Client Comment:";
            this.ClientCommentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CommentTextBox.Label = this.CommentLabel;
            this.CommentTextBox.Location = new System.Drawing.Point(636, 304);
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.ReadOnly = true;
            this.CommentTextBox.Size = new System.Drawing.Size(288, 20);
            this.CommentTextBox.TabIndex = 3;
            this.CommentTextBox.Tag = null;
            this.CommentTextBox.TextDetached = true;
            this.CommentTextBox.TextChanged += new System.EventHandler(this.CommentText_TextChanged);
            // 
            // CommentLabel
            // 
            this.CommentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CommentLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CommentLabel.Location = new System.Drawing.Point(564, 301);
            this.CommentLabel.Name = "CommentLabel";
            this.CommentLabel.Size = new System.Drawing.Size(72, 24);
            this.CommentLabel.TabIndex = 17;
            this.CommentLabel.Tag = null;
            this.CommentLabel.Text = "Comment:";
            this.CommentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CommentLabel.TextDetached = true;
            // 
            // RequestTextBox
            // 
            this.RequestTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RequestTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.RequestTextBox.DateTimeInput = false;
            this.RequestTextBox.Label = this.RequestLabel;
            this.RequestTextBox.Location = new System.Drawing.Point(708, 336);
            this.RequestTextBox.Multiline = true;
            this.RequestTextBox.Name = "RequestTextBox";
            this.RequestTextBox.ReadOnly = true;
            this.RequestTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RequestTextBox.Size = new System.Drawing.Size(216, 124);
            this.RequestTextBox.TabIndex = 4;
            this.RequestTextBox.Tag = null;
            this.RequestTextBox.TextDetached = true;
            this.RequestTextBox.TextChanged += new System.EventHandler(this.RequestText_TextChanged);
            // 
            // RequestLabel
            // 
            this.RequestLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RequestLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RequestLabel.Location = new System.Drawing.Point(596, 333);
            this.RequestLabel.Name = "RequestLabel";
            this.RequestLabel.Size = new System.Drawing.Size(112, 24);
            this.RequestLabel.TabIndex = 18;
            this.RequestLabel.Tag = null;
            this.RequestLabel.Text = "Request Item List:";
            this.RequestLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RequestLabel.TextDetached = true;
            // 
            // SubmitButton
            // 
            this.SubmitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SubmitButton.Enabled = false;
            this.SubmitButton.Location = new System.Drawing.Point(580, 376);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(96, 24);
            this.SubmitButton.TabIndex = 8;
            this.SubmitButton.Text = "SUBMIT ";
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // GroupNameLabel
            // 
            this.GroupNameLabel.Location = new System.Drawing.Point(234, 28);
            this.GroupNameLabel.Name = "GroupNameLabel";
            this.GroupNameLabel.Size = new System.Drawing.Size(104, 16);
            this.GroupNameLabel.TabIndex = 14;
            this.GroupNameLabel.Text = "Group Name:";
            this.GroupNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GroupNameText
            // 
            this.GroupNameText.BackColor = System.Drawing.SystemColors.Control;
            this.GroupNameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GroupNameText.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupNameText.ForeColor = System.Drawing.Color.MidnightBlue;
            this.GroupNameText.Location = new System.Drawing.Point(344, 28);
            this.GroupNameText.Name = "GroupNameText";
            this.GroupNameText.ReadOnly = true;
            this.GroupNameText.Size = new System.Drawing.Size(424, 16);
            this.GroupNameText.TabIndex = 15;
            this.GroupNameText.TabStop = false;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusLabel.Location = new System.Drawing.Point(564, 468);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(48, 16);
            this.StatusLabel.TabIndex = 19;
            this.StatusLabel.Tag = null;
            this.StatusLabel.Text = "Status:";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.StatusLabel.TextDetached = true;
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusTextBox.AutoSize = false;
            this.StatusTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.StatusTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusTextBox.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusTextBox.ForeColor = System.Drawing.Color.Maroon;
            this.StatusTextBox.Label = this.StatusLabel;
            this.StatusTextBox.Location = new System.Drawing.Point(620, 468);
            this.StatusTextBox.Multiline = true;
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.Size = new System.Drawing.Size(304, 36);
            this.StatusTextBox.TabIndex = 20;
            this.StatusTextBox.TabStop = false;
            this.StatusTextBox.Tag = null;
            this.StatusTextBox.TextDetached = true;
            // 
            // EnableResearchCheck
            // 
            this.EnableResearchCheck.Location = new System.Drawing.Point(1088, 8);
            this.EnableResearchCheck.Name = "EnableResearchCheck";
            this.EnableResearchCheck.Size = new System.Drawing.Size(128, 16);
            this.EnableResearchCheck.TabIndex = 10;
            this.EnableResearchCheck.Text = "Enable Research";
            this.EnableResearchCheck.CheckedChanged += new System.EventHandler(this.EnableResearchCheck_CheckedChanged);
            // 
            // SecIdTextBox
            // 
            this.SecIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SecIdTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.SecIdTextBox.Label = this.SecIdLabel;
            this.SecIdTextBox.Location = new System.Drawing.Point(812, 440);
            this.SecIdTextBox.Name = "SecIdTextBox";
            this.SecIdTextBox.Size = new System.Drawing.Size(104, 20);
            this.SecIdTextBox.TabIndex = 7;
            this.SecIdTextBox.Tag = null;
            this.SecIdTextBox.TextDetached = true;
            this.SecIdTextBox.Visible = false;
            this.SecIdTextBox.TextChanged += new System.EventHandler(this.SecIdTextBox_TextChanged);
            // 
            // SecIdLabel
            // 
            this.SecIdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SecIdLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SecIdLabel.Location = new System.Drawing.Point(724, 440);
            this.SecIdLabel.Name = "SecIdLabel";
            this.SecIdLabel.Size = new System.Drawing.Size(80, 24);
            this.SecIdLabel.TabIndex = 21;
            this.SecIdLabel.Tag = null;
            this.SecIdLabel.Text = "Security ID:";
            this.SecIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SecIdLabel.TextDetached = true;
            this.SecIdLabel.Visible = false;
            // 
            // YearNumericEdit
            // 
            this.YearNumericEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.YearNumericEdit.DataType = typeof(int);
            this.YearNumericEdit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YearNumericEdit.FormatType = C1.Win.C1Input.FormatTypeEnum.Integer;
            this.YearNumericEdit.Label = this.YearLabel;
            this.YearNumericEdit.Location = new System.Drawing.Point(812, 408);
            this.YearNumericEdit.MaxLength = 4;
            this.YearNumericEdit.Name = "YearNumericEdit";
            this.YearNumericEdit.ShowContextMenu = false;
            this.YearNumericEdit.Size = new System.Drawing.Size(64, 21);
            this.YearNumericEdit.TabIndex = 6;
            this.YearNumericEdit.Tag = null;
            this.YearNumericEdit.TrimStart = true;
            this.YearNumericEdit.Value = 2005;
            this.YearNumericEdit.Visible = false;
            this.YearNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // YearLabel
            // 
            this.YearLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.YearLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.YearLabel.Location = new System.Drawing.Point(724, 408);
            this.YearLabel.Name = "YearLabel";
            this.YearLabel.Size = new System.Drawing.Size(80, 24);
            this.YearLabel.TabIndex = 22;
            this.YearLabel.Tag = null;
            this.YearLabel.Text = "Year:";
            this.YearLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.YearLabel.TextDetached = true;
            this.YearLabel.Visible = false;
            // 
            // QuarterLabel
            // 
            this.QuarterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.QuarterLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.QuarterLabel.Location = new System.Drawing.Point(724, 376);
            this.QuarterLabel.Name = "QuarterLabel";
            this.QuarterLabel.Size = new System.Drawing.Size(80, 24);
            this.QuarterLabel.TabIndex = 23;
            this.QuarterLabel.Tag = null;
            this.QuarterLabel.Text = "Quarter:";
            this.QuarterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.QuarterLabel.TextDetached = true;
            this.QuarterLabel.Visible = false;
            // 
            // QuarterNumericEdit
            // 
            this.QuarterNumericEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.QuarterNumericEdit.DataType = typeof(short);
            this.QuarterNumericEdit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuarterNumericEdit.FormatType = C1.Win.C1Input.FormatTypeEnum.Integer;
            this.QuarterNumericEdit.Label = this.QuarterLabel;
            this.QuarterNumericEdit.Location = new System.Drawing.Point(812, 376);
            this.QuarterNumericEdit.MaxLength = 1;
            this.QuarterNumericEdit.Name = "QuarterNumericEdit";
            this.QuarterNumericEdit.ShowContextMenu = false;
            this.QuarterNumericEdit.Size = new System.Drawing.Size(40, 21);
            this.QuarterNumericEdit.TabIndex = 5;
            this.QuarterNumericEdit.Tag = null;
            this.QuarterNumericEdit.TrimStart = true;
            this.QuarterNumericEdit.Value = ((short)(1));
            this.QuarterNumericEdit.Visible = false;
            this.QuarterNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.UpDown;
            // 
            // TradeDateLabel
            // 
            this.TradeDateLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TradeDateLabel.Location = new System.Drawing.Point(8, 20);
            this.TradeDateLabel.Name = "TradeDateLabel";
            this.TradeDateLabel.Size = new System.Drawing.Size(96, 16);
            this.TradeDateLabel.TabIndex = 11;
            this.TradeDateLabel.Tag = null;
            this.TradeDateLabel.Text = "For Trade Date:";
            this.TradeDateLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.TradeDateLabel.TextDetached = true;
            // 
            // InventoryList
            // 
            this.InventoryList.AddItemSeparator = ';';
            this.InventoryList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InventoryList.BackColor = System.Drawing.Color.Honeydew;
            this.InventoryList.Caption = "Available Inventory";
            this.InventoryList.CaptionHeight = 17;
            this.InventoryList.ColumnCaptionHeight = 17;
            this.InventoryList.ColumnFooterHeight = 17;
            this.InventoryList.DeadAreaBackColor = System.Drawing.Color.DarkGray;
            this.InventoryList.EmptyRows = true;
            this.InventoryList.ExtendRightColumn = true;
            this.InventoryList.FetchRowStyles = true;
            this.InventoryList.Images.Add(((System.Drawing.Image)(resources.GetObject("InventoryList.Images"))));
            this.InventoryList.ItemHeight = 15;
            this.InventoryList.Location = new System.Drawing.Point(16, 304);
            this.InventoryList.MatchEntryTimeout = ((long)(2000));
            this.InventoryList.Name = "InventoryList";
            this.InventoryList.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.InventoryList.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.InventoryList.Size = new System.Drawing.Size(542, 190);
            this.InventoryList.TabIndex = 24;
            this.InventoryList.TabStop = false;
            this.InventoryList.FetchRowStyle += new C1.Win.C1List.FetchRowStyleEventHandler(this.InventoryList_FetchRowStyle);
            this.InventoryList.FormatText += new C1.Win.C1List.FormatTextEventHandler(this.InventoryList_FormatText);
            this.InventoryList.PropBag = resources.GetString("InventoryList.PropBag");
            // 
            // TradeDateCombo
            // 
            this.TradeDateCombo.AddItemSeparator = ';';
            this.TradeDateCombo.AllowColMove = false;
            this.TradeDateCombo.AutoCompletion = true;
            this.TradeDateCombo.AutoSize = false;
            this.TradeDateCombo.Caption = "";
            this.TradeDateCombo.CaptionHeight = 17;
            this.TradeDateCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.TradeDateCombo.ColumnCaptionHeight = 17;
            this.TradeDateCombo.ColumnFooterHeight = 17;
            this.TradeDateCombo.ColumnHeaders = false;
            this.TradeDateCombo.ColumnWidth = 100;
            this.TradeDateCombo.ContentHeight = 14;
            this.TradeDateCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.TradeDateCombo.EditorBackColor = System.Drawing.SystemColors.Window;
            this.TradeDateCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TradeDateCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.TradeDateCombo.EditorHeight = 14;
            this.TradeDateCombo.Enabled = false;
            this.TradeDateCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TradeDateCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("TradeDateCombo.Images"))));
            this.TradeDateCombo.ItemHeight = 15;
            this.TradeDateCombo.Location = new System.Drawing.Point(112, 19);
            this.TradeDateCombo.MatchEntryTimeout = ((long)(2000));
            this.TradeDateCombo.MaxDropDownItems = ((short)(5));
            this.TradeDateCombo.MaxLength = 32767;
            this.TradeDateCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.TradeDateCombo.Name = "TradeDateCombo";
            this.TradeDateCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.TradeDateCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.TradeDateCombo.Size = new System.Drawing.Size(104, 20);
            this.TradeDateCombo.TabIndex = 25;
            this.TradeDateCombo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TradeDateCombo.RowChange += new System.EventHandler(this.TradeDateCombo_RowChange);
            this.TradeDateCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TradeDateCombo_KeyPress);
            this.TradeDateCombo.PropBag = resources.GetString("TradeDateCombo.PropBag");
            // 
            // TradingGroupCombo
            // 
            this.TradingGroupCombo.AddItemSeparator = ';';
            this.TradingGroupCombo.AllowColMove = false;
            this.TradingGroupCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TradingGroupCombo.AutoCompletion = true;
            this.TradingGroupCombo.AutoDropDown = true;
            this.TradingGroupCombo.AutoSize = false;
            this.TradingGroupCombo.Caption = "";
            this.TradingGroupCombo.CaptionHeight = 17;
            this.TradingGroupCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TradingGroupCombo.ColumnCaptionHeight = 17;
            this.TradingGroupCombo.ColumnFooterHeight = 17;
            this.TradingGroupCombo.ContentHeight = 14;
            this.TradingGroupCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.TradingGroupCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.RightDown;
            this.TradingGroupCombo.DropDownWidth = 350;
            this.TradingGroupCombo.EditorBackColor = System.Drawing.SystemColors.Control;
            this.TradingGroupCombo.EditorFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TradingGroupCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.TradingGroupCombo.EditorHeight = 14;
            this.TradingGroupCombo.ExtendRightColumn = true;
            this.TradingGroupCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TradingGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("TradingGroupCombo.Images"))));
            this.TradingGroupCombo.ItemHeight = 15;
            this.TradingGroupCombo.Location = new System.Drawing.Point(588, 416);
            this.TradingGroupCombo.MatchEntryTimeout = ((long)(2000));
            this.TradingGroupCombo.MaxDropDownItems = ((short)(10));
            this.TradingGroupCombo.MaxLength = 32767;
            this.TradingGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.TradingGroupCombo.Name = "TradingGroupCombo";
            this.TradingGroupCombo.ReadOnly = true;
            this.TradingGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.TradingGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.TradingGroupCombo.Size = new System.Drawing.Size(80, 20);
            this.TradingGroupCombo.TabIndex = 26;
            this.TradingGroupCombo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TradingGroupCombo.ItemChanged += new System.EventHandler(this.TradingGroupCombo_ItemChanged);
            this.TradingGroupCombo.PropBag = resources.GetString("TradingGroupCombo.PropBag");
            // 
            // LocateSummaryList
            // 
            this.LocateSummaryList.AddItemSeparator = ';';
            this.LocateSummaryList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LocateSummaryList.BackColor = System.Drawing.Color.Ivory;
            this.LocateSummaryList.Caption = "Locate Summary";
            this.LocateSummaryList.CaptionHeight = 17;
            this.LocateSummaryList.ColumnCaptionHeight = 17;
            this.LocateSummaryList.ColumnFooterHeight = 17;
            this.LocateSummaryList.DeadAreaBackColor = System.Drawing.Color.DarkGray;
            this.LocateSummaryList.EmptyRows = true;
            this.LocateSummaryList.ExtendRightColumn = true;
            this.LocateSummaryList.FetchRowStyles = true;
            this.LocateSummaryList.Images.Add(((System.Drawing.Image)(resources.GetObject("LocateSummaryList.Images"))));
            this.LocateSummaryList.ItemHeight = 15;
            this.LocateSummaryList.Location = new System.Drawing.Point(948, 304);
            this.LocateSummaryList.MatchEntryTimeout = ((long)(2000));
            this.LocateSummaryList.Name = "LocateSummaryList";
            this.LocateSummaryList.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.LocateSummaryList.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.LocateSummaryList.Size = new System.Drawing.Size(264, 190);
            this.LocateSummaryList.TabIndex = 28;
            this.LocateSummaryList.TabStop = false;
            this.LocateSummaryList.FormatText += new C1.Win.C1List.FormatTextEventHandler(this.LocateSummaryList_FormatText);
            this.LocateSummaryList.PropBag = resources.GetString("LocateSummaryList.PropBag");
            // 
            // RefreshTimer
            // 
            this.RefreshTimer.Interval = 20000D;
            this.RefreshTimer.SynchronizingObject = this;
            this.RefreshTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.RefreshTimer_Elapsed);
            // 
            // AutoUpdateCheckBox
            // 
            this.AutoUpdateCheckBox.Checked = true;
            this.AutoUpdateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoUpdateCheckBox.Location = new System.Drawing.Point(936, 8);
            this.AutoUpdateCheckBox.Name = "AutoUpdateCheckBox";
            this.AutoUpdateCheckBox.Size = new System.Drawing.Size(152, 16);
            this.AutoUpdateCheckBox.TabIndex = 29;
            this.AutoUpdateCheckBox.Text = "Auto Update Pending";
            this.AutoUpdateCheckBox.CheckedChanged += new System.EventHandler(this.AutoUpdateCheckBox_CheckedChanged);
            // 
            // ShowPendingButton
            // 
            this.ShowPendingButton.Location = new System.Drawing.Point(816, 8);
            this.ShowPendingButton.Name = "ShowPendingButton";
            this.ShowPendingButton.Size = new System.Drawing.Size(96, 16);
            this.ShowPendingButton.TabIndex = 30;
            this.ShowPendingButton.Text = "Show Pending";
            this.ShowPendingButton.Click += new System.EventHandler(this.ShowPendingButton_Click);
            // 
            // ShowAllButton
            // 
            this.ShowAllButton.Location = new System.Drawing.Point(816, 32);
            this.ShowAllButton.Name = "ShowAllButton";
            this.ShowAllButton.Size = new System.Drawing.Size(96, 16);
            this.ShowAllButton.TabIndex = 31;
            this.ShowAllButton.Text = "Show All";
            this.ShowAllButton.Click += new System.EventHandler(this.ShowAllButton_Click);
            // 
            // AutoUpdateAllCheckBox
            // 
            this.AutoUpdateAllCheckBox.Location = new System.Drawing.Point(936, 32);
            this.AutoUpdateAllCheckBox.Name = "AutoUpdateAllCheckBox";
            this.AutoUpdateAllCheckBox.Size = new System.Drawing.Size(152, 16);
            this.AutoUpdateAllCheckBox.TabIndex = 32;
            this.AutoUpdateAllCheckBox.Text = "Auto Update All";
            this.AutoUpdateAllCheckBox.CheckedChanged += new System.EventHandler(this.AutoUpdateAllCheckBox_CheckedChanged);
            // 
            // UpdateAllRefreshTimer
            // 
            this.UpdateAllRefreshTimer.Interval = 20000D;
            this.UpdateAllRefreshTimer.SynchronizingObject = this;
            this.UpdateAllRefreshTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateAllRefreshTimer_Elapsed);
            // 
            // ShortSaleLocateForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(1216, 509);
            this.Controls.Add(this.AutoUpdateAllCheckBox);
            this.Controls.Add(this.ShowAllButton);
            this.Controls.Add(this.ShowPendingButton);
            this.Controls.Add(this.AutoUpdateCheckBox);
            this.Controls.Add(this.LocateSummaryList);
            this.Controls.Add(this.InventoryList);
            this.Controls.Add(this.GroupNameText);
            this.Controls.Add(this.ClientCommentText);
            this.Controls.Add(this.LocatesGrid);
            this.Controls.Add(this.TradingGroupCombo);
            this.Controls.Add(this.TradeDateCombo);
            this.Controls.Add(this.TradeDateLabel);
            this.Controls.Add(this.SecIdLabel);
            this.Controls.Add(this.QuarterLabel);
            this.Controls.Add(this.QuarterNumericEdit);
            this.Controls.Add(this.YearLabel);
            this.Controls.Add(this.YearNumericEdit);
            this.Controls.Add(this.SecIdTextBox);
            this.Controls.Add(this.EnableResearchCheck);
            this.Controls.Add(this.StatusTextBox);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.GroupNameLabel);
            this.Controls.Add(this.RequestLabel);
            this.Controls.Add(this.CommentLabel);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.RequestTextBox);
            this.Controls.Add(this.CommentTextBox);
            this.Controls.Add(this.ClientCommentLabel);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "ShortSaleLocateForm";
            this.Padding = new System.Windows.Forms.Padding(1, 55, 1, 220);
            this.Text = "Short Sale - Locates";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ShortSaleLocateForm_Closing);
            this.Deactivate += new System.EventHandler(this.ShortSaleLocateForm_Deactivate);
            this.Load += new System.EventHandler(this.ShortSaleLocateForm_Load);
            this.Resize += new System.EventHandler(this.ShortSaleLocateForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.LocatesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommentLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RequestTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RequestLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecIdTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecIdLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuarterLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QuarterNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TradeDateLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TradeDateCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TradingGroupCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocateSummaryList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateAllRefreshTimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		private void InventoryListFill(string groupCode, string secId)
		{
			try
			{
				if (inventoryDataSet != null)
				{
					inventoryDataSet.Tables["Inventory"].Clear();
					inventoryDataSet.Tables["Inventory"].AcceptChanges(); // Clear list of potentially incorrect data.
				}

				if (secId != null)
				{
					InventoryList.Caption = "Available Inventory [" + secId + "]";
          
					inventoryDataSet = mainForm.ShortSaleAgent.InventoryGet(groupCode, secId, mainForm.UtcOffset); // Get new list for current security.
        
					InventoryList.HoldFields();
                    InventoryList.DataSource = inventoryDataSet.Tables["Inventory"].DefaultView ;					
					//InventoryList.Rebind();
				}

				if ((inventoryDataSet != null) && (inventoryDataSet.Tables["Inventory"].Rows.Count > 0))
				{
					InventoryList.DeadAreaBackColor = Color.Honeydew;
				}
				else
				{
					InventoryList.DeadAreaBackColor = Color.DarkGray;
				}
			}
			catch (Exception e)
			{
				InventoryList.DeadAreaBackColor = Color.RosyBrown;

				mainForm.Alert(e.Message, PilotState.RunFault);
				Log.Write(e.Message + " [ShortSaleLocateForm.InventoryListFill]", Log.Error, 1);
			}
		}
    
		private void LocatesGridFill()
		{
			LocatesGridFill(false);
		}
		
		private void LocatesGridFill(bool returnPending)
		{
			ClientCommentText.Text = "";
			GroupNameText.Text = "";

			mainForm.Alert("Please wait... Loading locates for " + TradeDateCombo.Text + "...", PilotState.Unknown);
			this.Cursor = Cursors.WaitCursor;

			LocateSummaryListFill("");			
			Application.DoEvents();

			try
			{
				if (TradeDateCombo.Text.Equals(TradeDateCombo.Tag)) // Load request is for the current trade date.
				{
					AutoUpdateCheckBox.Enabled = true;					

					LocatesGrid.AllowUpdate = mayEdit;
					LocatesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;	
				}
				else
				{					
					AutoUpdateCheckBox.Checked = false;
					AutoUpdateCheckBox.Enabled = false;

					LocatesGrid.AllowUpdate = false;
					LocatesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
				}

				DataRow [] rows = null;
				DataSet dataSet = null;
				
				if (returnPending)
				{
					dataSet = mainForm.ShortSaleAgent.LocatesGet(TradeDateCombo.Text, null, "Pending", mainForm.UtcOffset);
					rows = dataSet.Tables["Table"].Select();
				}
				else
				{
					dataSet = mainForm.ShortSaleAgent.LocatesGet(TradeDateCombo.Text, null, null, mainForm.UtcOffset);
					rows = dataSet.Tables["Table"].Select();
				}								
				
				if (returnPending && (rows.Length > 0))
				{
					NotifyWindow nw = new NotifyWindow("Locates @ " + DateTime.Now.ToString(Standard.TimeShortFormat), 
						rows.Length.ToString("#,##0") + " pending locate(s).");
					nw.Notify();
				}

				mainDataSet.Tables["Locates"].Rows.Clear();
				mainDataSet.Tables["Locates"].BeginLoadData();
      
				foreach (DataRow row in rows)
				{
					mainDataSet.Tables["Locates"].ImportRow(row);          
				}
		
				mainDataSet.Tables["Locates"].EndLoadData();        				
				
				mainDataSet.Tables["LocateSummary"].Rows.Clear();				
				mainDataSet.Tables["LocateSummary"].BeginLoadData();
				
				foreach (DataRow row in dataSet.Tables["Table1"].Rows)
				{
					mainDataSet.Tables["LocateSummary"].ImportRow(row);          
				}
				
				mainDataSet.Tables["LocateSummary"].EndLoadData();        

				mainForm.Alert("Loading locates for " + TradeDateCombo.Text + "... Done!", PilotState.Normal);
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [ShortSaleLocateForm.LocateGridFill]", 1);
			}			
			
			if (LocatesGrid.Splits[0,0].Rows.Count > 0)
			{
				LocateSummaryListFill(LocatesGrid.Columns["SecId"].Text);
			}		

			this.Cursor = Cursors.Default;
		}

		private void LocatesGridFill(string tradeDateMin, string tradeDateMax, string groupCode)
		{
			ClientCommentText.Text = "";
			GroupNameText.Text = "";

			mainForm.Alert("Please wait... Loading Locates research for Q" +
				QuarterNumericEdit.Text  + " " + YearNumericEdit.Text  + "...", PilotState.Unknown);
			this.Cursor = Cursors.WaitCursor;

			LocateSummaryListFill("");			
			Application.DoEvents();

			try
			{				
				DataRow[] rows = mainForm.ShortSaleAgent.LocatesGet(
					tradeDateMin, tradeDateMax, groupCode, SecIdTextBox.Text.Trim(), mainForm.UtcOffset).Tables["Locates"].Select();
    
				mainDataSet.Tables["Locates"].Rows.Clear();
				mainDataSet.Tables["Locates"].BeginLoadData();
      
				foreach (DataRow row in rows)
				{
					mainDataSet.Tables["Locates"].ImportRow(row);          
				}

				mainDataSet.Tables["Locates"].EndLoadData();        
				mainForm.Alert("Loading Locates research for Q" +
					QuarterNumericEdit.Text  + " " + YearNumericEdit.Text  + "... Done!", PilotState.Normal);
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [ShortSaleLocateForm.LocateGridFill]", Log.Error, 1);
			}			

			if (LocatesGrid.Splits[0,0].Rows.Count > 0)
			{
				LocateSummaryListFill(LocatesGrid.Columns["SecId"].Text);
			}

			this.Cursor = Cursors.Default;
		}		

		private void LocateSummaryListFill(string secId)
		{
			long clientQuantity = 0;
			long quantity = 0;
        
			LocateSummaryList.Caption = "Locate Summary [" + secId + "]";
      
			locateSummaryDataView.RowFilter = "SecId = '" + secId + "'";

			if (locateSummaryDataView.Count > 1)
			{
				LocateSummaryList.ColumnFooters = true;

				foreach (DataRowView row in locateSummaryDataView)
				{
					if (!row["ClientQuantity"].ToString().Equals(""))
					{
						clientQuantity += (long) row["ClientQuantity"];
					}
					
					if (!row["Quantity"].ToString().Equals(""))
					{					
						quantity += (long) row["Quantity"]; 
					}
				}

				LocateSummaryList.Columns["ClientQuantity"].FooterText = clientQuantity.ToString("#,##0");
				LocateSummaryList.Columns["Quantity"].FooterText = quantity.ToString("#,##0");
			}
			else
			{
				LocateSummaryList.ColumnFooters = false;
			}
		}

		private void ShortSaleLocateForm_Load(object sender, System.EventArgs e)
		{
			int height = mainForm.Height - 250;
			int width = 1220;

			this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "5"));
			this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "5"));
			this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
			this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

			this.Show();
			this.Cursor = Cursors.WaitCursor;
			Application.DoEvents();

			mainForm.Alert("Please wait... Loading current data...", PilotState.Unknown);

			try
			{				
				mainDataSet = mainForm.ShortSaleAgent.LocateDataGet("Pending", mainForm.UtcOffset);

				mainDataSet.Tables.Add("LocateSummary");
				mainDataSet.Tables["LocateSummary"].Columns.Add("SecId", typeof(string));
				mainDataSet.Tables["LocateSummary"].Columns.Add("GroupCode", typeof(string));
				mainDataSet.Tables["LocateSummary"].Columns.Add("ClientQuantity", typeof(long));
				mainDataSet.Tables["LocateSummary"].Columns.Add("Quantity", typeof(long));

				mainDataSet.Tables["LocateSummary"].PrimaryKey = new DataColumn[2] {
																																						 mainDataSet.Tables["LocateSummary"].Columns["SecId"],
																																						 mainDataSet.Tables["LocateSummary"].Columns["GroupCode"]};				
        
				locateSummaryDataView = new DataView(mainDataSet.Tables["LocateSummary"]);
				locateSummaryDataView.RowFilter = "SecId = ''";
				locateSummaryDataView.Sort = "GroupCode";

				LocateSummaryList.HoldFields();
				LocateSummaryList.DataSource = locateSummaryDataView;
        
				locatesDataView = new DataView(mainDataSet.Tables["Locates"]);
				locatesDataView.Sort = "LocateId DESC";				

				LocatesGrid.SetDataBinding(locatesDataView, null, true);
      
				tradingGroupDataView = new DataView(mainDataSet.Tables["TradingGroups"]);
				tradingGroupDataView.RowFilter = "IsActive = 1";
				tradingGroupDataView.Sort = "GroupName";
        
				TradingGroupCombo.HoldFields();
				TradingGroupCombo.DataSource = tradingGroupDataView;
				TradingGroupCombo.DataMember = "TradingGroups";
				TradingGroupCombo.SelectedIndex = 0;

				TradeDateCombo.DataSource = mainDataSet;      
				TradeDateCombo.DataMember = "TradeDates";
				TradeDateCombo.SelectedIndex = 0;

				InventoryListFill("", ""); // A necessary hack to force initialization.				
				LocatesGridFill(true);
				mainForm.Alert("Loading current data... Done!", PilotState.Normal);
								
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
				Log.Write(error.Message + " [ShortSaleLocateForm.ShortSaleLocateForm_Load]", Log.Error, 1);
			}

			TradeDateCombo.Enabled = true;
    
			this.Cursor = Cursors.Default;
		}

		private void ShortSaleLocateForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());    
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());    
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());    
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());    
			} 			

			RefreshTimer.Enabled = false;
			UpdateAllRefreshTimer.Enabled = false;

			mainForm.shortSaleLocateForm = null;
		}

		private void LocatesGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			int i = LocatesGrid.Col;
			string gridData = "";

			if ((e.KeyChar == 9) && (LocatesGrid.Col == PRE_BORROW)) // User has tabbed from the last column.
			{
				if (LocatesGrid.Splits[0].Rows.Count > (LocatesGrid.Row + 1))
				{
					LocatesGrid.Row += 1;
					e.Handled = true;
				}

				return;
			}

			if (e.KeyChar.Equals((char)32) && LocatesGrid.Col.Equals(QUANTITY)
				&& LocatesGrid.Columns[QUANTITY].Text.Trim().Equals("") && LocatesGrid.EditActive) // User wishes to copy client quantity.
			{
				LocatesGrid.Columns[QUANTITY].Text = LocatesGrid.Columns["Request"].Text;
				LocatesGrid.Col = SOURCE;

				return;
			}

			if (e.KeyChar.Equals((char)13))
			{
				if ((LocatesGrid.Splits[0].Rows.Count == 1) && (LocatesGrid.DataChanged))
				{
					LocatesGrid.UpdateData();      
				}
			}

			if (e.KeyChar.Equals((char)3) && LocatesGrid.SelectedRows.Count > 0)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
				{
					gridData += dataColumn.Caption + "\t";
				}
				gridData += "\n";

				foreach (int row in LocatesGrid.SelectedRows)
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
					{
						gridData += dataColumn.CellText(row) + "\t";
					}
					gridData += "\n";
				}

				Clipboard.SetDataObject(gridData, true);
				mainForm.Alert("Copied " + LocatesGrid.SelectedRows.Count + " rows to the clipboard.", PilotState.Normal);
				e.Handled = true;
			}
		}

		private void LocatesGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
		{
			DataSet tempDataSet = new DataSet();
			
			long locateQuantity = 0;
			float rate;
		
				
			if (!LocatesGrid.Columns[QUANTITY].Text.Equals(""))
			{
				try
				{
					locateQuantity = (long)LocatesGrid.Columns[QUANTITY].Value;
				}
				catch
				{
					mainForm.Alert("Entry for the quantity located must be a numeric value!", PilotState.RunFault);
					LocatesGrid.Col = QUANTITY;
					e.Cancel = true;
					return;
				}
			}

			if (!LocatesGrid.Columns["FeeRate"].Text.Equals(""))
			{
				try
				{
					rate = float.Parse(LocatesGrid.Columns["FeeRate"].Text);
				}
				catch
				{
					mainForm.Alert("Entry for a fee must be a numeric value!", PilotState.RunFault);
					LocatesGrid.Col = FEE_RATE;
					e.Cancel = true;
					return;
				}
			}

			LocatesGrid.Columns["Source"].Text = LocatesGrid.Columns["Source"].Text.Trim();

			try
			{
				if (bool.Parse(LocatesGrid.Columns[PRE_BORROW].Value.ToString()) == true)
				{		
					mainForm.ShortSaleAgent.LocatePreBorrowSubmit(
						(long) LocatesGrid.Columns["LocateId"].Value,
						LocatesGrid.Columns["GroupCode"].Text,
						LocatesGrid.Columns["SecId"].Text,
						LocatesGrid.Columns["Quantity"].Value.ToString(),					
						LocatesGrid.Columns["FeeRate"].Value.ToString(),
						mainForm.UserId
						);
				}
				else
				{
					
					mainForm.ShortSaleAgent.LocateItemSet(
						(long) LocatesGrid.Columns["LocateId"].Value,
						LocatesGrid.Columns["Located"].Value.ToString(),
						LocatesGrid.Columns["Source"].Text,
						LocatesGrid.Columns["FeeRate"].Value.ToString(),
						LocatesGrid.Columns["PreBorrow"].Value.ToString(),
						LocatesGrid.Columns["Comment"].Text,
						mainForm.UserId
						);
				}

				LocatesGrid.Columns["ActUserShortName"].Text = "me";
				LocatesGrid.Columns["ActTime"].Text = DateTime.Now.ToString("HH:mm:ss");

				if (!LocatesGrid.Columns[QUANTITY].Text.Equals(""))
				{
					if ((long)LocatesGrid.Columns["Request"].Value <= (long)LocatesGrid.Columns["Located"].Value)
					{
						LocatesGrid.Columns["Status"].Text = "FullOK";
					}
					else if (locateQuantity > 0)
					{
						LocatesGrid.Columns["Status"].Text = "Partial";       
					}
					else if (locateQuantity == 0)
					{
						LocatesGrid.Columns["Status"].Text = "None";       
					}
				}
								
				tempDataSet = mainForm.ShortSaleAgent.LocateItemGet(LocatesGrid.Columns["LocateId"].Value.ToString(), mainForm.UtcOffset);
				
				foreach (DataRow dr in tempDataSet.Tables["Table"].Rows)
				{
					mainDataSet.Tables["Locates"].LoadDataRow(dr.ItemArray, true);	
				}

				
				foreach (DataRow dr in tempDataSet.Tables["Table1"].Rows)
				{
					Log.Write(dr["ClientQuantity"].ToString(), 1);
					mainDataSet.Tables["LocateSummary"].LoadDataRow(dr.ItemArray, true);	
				}

				LocateSummaryListFill(secId);				
			}			
			catch(Exception ee)
			{
				mainForm.Alert("Error processing the update to ID " + LocatesGrid.Columns["LocateIdTail"].Text + ".", PilotState.RunFault);
				Log.Write(ee.Message + " [ShortSaleLocateForm.LocatesGrid_BeforeUpdate]", Log.Error, 1);
        
				e.Cancel = true;
				return;
			}
		}

		private void LocatesGrid_AfterUpdate(object sender, System.EventArgs e)
		{
			if(LocatesGrid.Splits[0].Rows.Count != LocatesGrid.Row)
			{
				LocatesGrid.Col = QUANTITY;
			}
		}

		private void LocatesGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0)
			{
				return;
			}
  
			try
			{
				switch(LocatesGrid.Columns[e.ColIndex].DataField)
				{
					case ("LocateIdTail"):
						e.Value = e.Value.ToString().PadLeft(5, '0');
						break;
					case ("OpenTime"):
						if (EnableResearchCheck.Checked)
						{
							e.Value = DateTime.Parse(e.Value).ToString(Standard.DateTimeShortFormat);
						}
						else
						{
							e.Value = DateTime.Parse(e.Value).ToString(Standard.TimeFileFormat);
						}
						break;
					case ("ClientQuantity"):
					case ("Quantity"):
						e.Value = long.Parse(e.Value).ToString("#,##0");
						break;
					case ("ActTime"):
						e.Value = Tools.FormatDate(e.Value.ToString(), "HH:mm:ss");          
						break;
				}
			}
			catch {}
		}

		private void LocatesGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			try
			{				
				if (!LocatesGrid.Columns["SecId"].Text.Equals(secId) || !LocatesGrid.Columns["GroupCode"].Text.Trim().Equals(groupCode.Trim()))
				{
					secId = LocatesGrid.Columns["SecId"].Text;
					groupCode = LocatesGrid.Columns["GroupCode"].Text;
					
					this.Cursor = Cursors.WaitCursor;      				
     
					ClientCommentText.Text = LocatesGrid.Columns["ClientComment"].Text;
					GroupNameText.Text = LocatesGrid.Columns["GroupName"].Text;
      
					mainForm.SecId = secId;

					InventoryListFill(groupCode, secId);
					LocateSummaryListFill(secId);

					this.Cursor = Cursors.Default;
				}
			}
			catch 
			{
				secId = "";

				this.Cursor = Cursors.WaitCursor;
      
				ClientCommentText.Text = "";
				GroupNameText.Text = "";
      
				mainForm.SecId = "";

				InventoryListFill("", null);
				LocateSummaryListFill("");

				this.Cursor = Cursors.Default;
			}    
		}

		private void LocatesGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.X <= LocatesGrid.RecordSelectorWidth && e.Y <= LocatesGrid.RowHeight)
			{
				if (LocatesGrid.SelectedRows.Count.Equals(0))
				{
					for (int i = 0; i < LocatesGrid.Splits[0,0].Rows.Count; i++)
					{
						LocatesGrid.SelectedRows.Add(i);
					}

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.Columns)
					{
						LocatesGrid.SelectedCols.Add(dataColumn);
					}
				}
				else
				{
					LocatesGrid.SelectedRows.Clear();
					LocatesGrid.SelectedCols.Clear();
				}
			}
		}

		private void RequestText_TextChanged(object sender, System.EventArgs e)
		{
			if (!RequestTextBox.Text.Equals(""))
			{
				SubmitButton.Enabled = true;
				StatusTextBox.Text = "";
			}
		}

		private void CommentText_TextChanged(object sender, System.EventArgs e)
		{
			if (!CommentTextBox.Text.Equals(""))
			{
				SubmitButton.Enabled = true;
				StatusTextBox.Text = "";
			}
		}

		private void SubmitButton_Click(object sender, System.EventArgs e)
		{
			string tradeDateMin = "";
			string tradeDateMax = "";
			string groupCode = "";

			this.Cursor = Cursors.WaitCursor;

			if (EnableResearchCheck.Checked)
			{
				switch ((short)QuarterNumericEdit.Value)
				{
					case 1 :
						tradeDateMin = YearNumericEdit.Text + "-01-01";
						tradeDateMax = YearNumericEdit.Text + "-03-31";
						break;
					case 2 :
						tradeDateMin = YearNumericEdit.Text + "-04-01";
						tradeDateMax = YearNumericEdit.Text + "-06-30";
						break;
					case 3 :
						tradeDateMin = YearNumericEdit.Text + "-07-01";
						tradeDateMax = YearNumericEdit.Text + "-09-30";
						break;
					case 4 :
						tradeDateMin = YearNumericEdit.Text + "-10-01";
						tradeDateMax = YearNumericEdit.Text + "-12-31";
						break;
				}

				if (!TradingGroupCombo.Text.Equals("***"))
				{
					groupCode = TradingGroupCombo.Text;
				}

				LocatesGridFill(tradeDateMin, tradeDateMax, groupCode);
			}
			else
			{
				if(!RequestTextBox.Text.Equals(""))
				{
					try
					{
						StatusTextBox.Text = mainForm.ShortSaleAgent.LocateListSubmit(mainForm.UserId,
							TradingGroupCombo.Text, CommentTextBox.Text, RequestTextBox.Text);
    
						RequestTextBox.Text = "";
						CommentTextBox.Text = "";
					}
					catch (Exception ee)
					{
						StatusTextBox.Text = ee.Message;             
					}
				}

				SubmitButton.Enabled = false;
			}

			this.Cursor = Cursors.Default;
		}

		private void EnableResearchCheck_CheckedChanged(object sender, System.EventArgs e)
		{
			ClientCommentText.Text = "";
			GroupNameText.Text = "";

			SecIdTextBox.Text = "";
			CommentTextBox.Text = "";
			RequestTextBox.Text = "";

			SecIdLabel.Visible = EnableResearchCheck.Checked;
			SecIdTextBox.Visible = EnableResearchCheck.Checked;
      
			YearLabel.Visible = EnableResearchCheck.Checked;
			YearNumericEdit.Visible = EnableResearchCheck.Checked;
      
			QuarterLabel.Visible = EnableResearchCheck.Checked;
			QuarterNumericEdit.Visible = EnableResearchCheck.Checked;

			CommentLabel.Visible = !EnableResearchCheck.Checked;
			CommentTextBox.Visible = !EnableResearchCheck.Checked;

			RequestLabel.Visible = !EnableResearchCheck.Checked;
			RequestTextBox.Visible = !EnableResearchCheck.Checked;
			
			StatusLabel.Visible = !EnableResearchCheck.Checked;
			//StatusTextBox.Visible = !EnableResearchCheck.Checked;
			
			ShowPendingButton.Enabled = !EnableResearchCheck.Checked;			
			ShowAllButton.Enabled = !EnableResearchCheck.Checked;
			
			AutoUpdateCheckBox.Checked = !EnableResearchCheck.Checked;
			AutoUpdateCheckBox.Enabled = !EnableResearchCheck.Checked;
			
			AutoUpdateAllCheckBox.Checked = !EnableResearchCheck.Checked;
			AutoUpdateAllCheckBox.Enabled = !EnableResearchCheck.Checked;
			
			RefreshTimer.Enabled = !EnableResearchCheck.Checked;
			
			locatesDataView.RowFilter = "";
			
			TradingGroupCombo.ReadOnly = (!EnableResearchCheck.Checked && RequestTextBox.ReadOnly);

			TradeDateLabel.Enabled = !EnableResearchCheck.Checked;
			TradeDateCombo.Enabled = !EnableResearchCheck.Checked;
			
			SubmitButton.Enabled = false;
      			
			if (EnableResearchCheck.Checked)
			{
				mainDataSet.Tables["Locates"].Clear();
				mainDataSet.Tables["Locates"].AcceptChanges();  
        
				LocatesGrid.Splits[0].DisplayColumns["OpenTime"].Width = 115;
				StatusTextBox.Text = "";				
			}
			else
			{
				LocatesGridFill();

				LocatesGrid.Splits[0].DisplayColumns["OpenTime"].Width = 65;
			}
		}

		private void TradingGroupCombo_ItemChanged(object sender, System.EventArgs e)
		{
			StatusTextBox.Text = TradingGroupCombo.GetItemText(TradingGroupCombo.WillChangeToIndex, 1);
      
			if (EnableResearchCheck.Checked)
			{
				SubmitButton.Enabled = !(TradingGroupCombo.GetItemText(TradingGroupCombo.WillChangeToIndex, 0).Equals("***")
					&& SecIdTextBox.Text.Trim().Equals(""));
			}
		}

		private void SecIdTextBox_TextChanged(object sender, System.EventArgs e)
		{
			SubmitButton.Enabled = !(TradingGroupCombo.Text.Equals("***") && SecIdTextBox.Text.Trim().Equals(""));   
		}

		private void InventoryList_FetchRowStyle(object sender, C1.Win.C1List.FetchRowStyleEventArgs e)
		{
			try
			{
				if (DateTime.Parse(InventoryList.GetItemText(e.Row, "Received")).ToString(Standard.DateFormat).CompareTo(TradeDateCombo.Tag) >= 0)
				{
					if (long.Parse(InventoryList.Columns["Quantity"].CellValue(e.Row).ToString()) < 0)
					{
						e.CellStyle.ForeColor = Color.Red;
					}
					else
					{
						e.CellStyle.ForeColor = Color.Navy;
					}
				}
				else
				{
					e.CellStyle.ForeColor = Color.Gray;
				}
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void InventoryList_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0) // Then nothing to do.
			{
				return;
			}
  
			try
			{
				switch(InventoryList.Columns[e.ColIndex].DataField)
				{
					case ("ScribeTime"):
						e.Value = DateTime.Parse(e.Value).ToString(Standard.DateTimeShortFormat);
						break;
					case ("BizDate"):
						e.Value = DateTime.Parse(e.Value).ToString(Standard.DateFormat);
						break;
					case ("Quantity"):
						e.Value = long.Parse(e.Value).ToString("#,##0");
						break;
				}        
			}
			catch{}
		}

		private void TradeDateCombo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)13))
			{
				TradeDateCombo.Text = Tools.FormatDate(TradeDateCombo.Text, Standard.DateFormat);
				e.Handled = true;
			}
		}

		private void TradeDateCombo_RowChange(object sender, System.EventArgs e)
		{
			if (TradeDateCombo.Enabled && TradeDateCombo.SelectedIndex > -1)
			{
				LocatesGridFill(); 
			}
		}

		private void LocateSummaryList_FormatText(object sender, C1.Win.C1List.FormatTextEventArgs e)
		{
			if (e.Value.Length == 0)
			{
				return;
			}
  
			try
			{
				switch(LocateSummaryList.Columns[e.ColIndex].DataField)
				{
					case ("ClientQuantity"):
					case ("Quantity"):
						e.Value = long.Parse(e.Value).ToString("#,##0");
						break;
				}
			}
			catch {}    
		}

		private void LocatesGrid_Error(object sender, C1.Win.C1TrueDBGrid.ErrorEventArgs e)
		{
			mainForm.Alert(e.Exception.Message, PilotState.RunFault); 
			e.Handled = true;
		}

		private void SendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
			int textLength;
			int [] maxTextLength;

			int columnIndex = -1;
			string gridData = "\n\n\n";

			if (LocatesGrid.SelectedCols.Count.Equals(0))
			{
				mainForm.Alert("You have not selected any rows.", PilotState.RunFault);
				return;
			}

			try
			{
				maxTextLength = new int[LocatesGrid.SelectedCols.Count];

				// Get the caption length for each column.
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
				{
					maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
				}

				// Get the maximum item length for each row in each column.
				foreach (int rowIndex in LocatesGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
					{
						if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
						{
							maxTextLength[columnIndex] = textLength;
						}
					}
				}

				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
				{
					gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				}
				gridData += "\n";
        
				columnIndex = -1;

				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
				{
					gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				}
				gridData += "\n";
        
				foreach (int rowIndex in LocatesGrid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in LocatesGrid.SelectedCols)
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

				mainForm.Alert("Total: " + LocatesGrid.SelectedRows.Count + " items added to e-mail.", PilotState.Normal);
			}
			catch (Exception error)
			{       
				Log.Write(error.Message + " [ShortSaleLocateForm.SendToEmailMenuItem_Click]", Log.Error, 1); 
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void RefreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (RefreshTimer.Enabled)
			{
				RefreshTimer.Enabled = false;			
			
				if (AutoUpdateAllCheckBox.Checked)
				{
					LocatesGridFill(false);
				}
				else if (AutoUpdateCheckBox.Checked)
				{
					LocatesGridFill(true);
				}				
		
				InventoryListFill("", "");
				RefreshTimer.Enabled = true;
			}
		}

		private void AutoUpdateCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{																
			if (AutoUpdateAllCheckBox.Checked)
			{
				AutoUpdateAllCheckBox.Checked = false;
			}
								
			RefreshTimer.Enabled = AutoUpdateCheckBox.Checked;			
		}

		private void ShowPendingButton_Click(object sender, System.EventArgs e)
		{		
			if (AutoUpdateCheckBox.Checked)
			{
				AutoUpdateCheckBox.Checked = false;			
			}
			
			if (AutoUpdateAllCheckBox.Checked)
			{
				AutoUpdateAllCheckBox.Checked = false;			
			}

			LocatesGridFill(true);						
		}

		private void ShowAllButton_Click(object sender, System.EventArgs e)
		{
			if (AutoUpdateCheckBox.Checked)
			{
				AutoUpdateCheckBox.Checked = false;
			}

			if (AutoUpdateAllCheckBox.Checked)
			{
				AutoUpdateAllCheckBox.Checked = false;			
			}

			LocatesGridFill();
		}

		private void SendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			Excel excel = new Excel();
			excel.ExportGridToExcel(ref LocatesGrid);
		
			this.Cursor = Cursors.Default;
		}

		private void AttachMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (!AttachedMenuItem.Checked)
				{
					mainForm.shortSaleLocateForm.MdiParent = mainForm;
					AttachedMenuItem.Checked = true;
				}
				else
				{
					mainForm.shortSaleLocateForm.MdiParent = null;
					AttachedMenuItem.Checked = false;
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void AutoUpdateAllCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			if (AutoUpdateCheckBox.Checked)
			{
				AutoUpdateCheckBox.Checked = false;
			}
						
			UpdateAllRefreshTimer.Enabled = AutoUpdateAllCheckBox.Checked;				
		}

		private void UpdateAllRefreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if ((UpdateAllRefreshTimer.Enabled) && (!RefreshTimer.Enabled))
			{
				UpdateAllRefreshTimer.Enabled = false;			
							
				LocatesGridFill(false);
						
				UpdateAllRefreshTimer.Enabled = true;				
			}
		}

		private void ActionZeroLocatesMenuItem_Click(object sender, System.EventArgs e)
		{
			DataSet tempDataSet = new DataSet();

			try
			{		
				for ( int count = 0; count < LocatesGrid.SelectedRows.Count; count++)
				{
					LocatesGrid[LocatesGrid.SelectedRows[count], "Located"] = 0;

					mainForm.ShortSaleAgent.LocateItemSet(
						(long) LocatesGrid.Columns["LocateId"].CellValue(LocatesGrid.SelectedRows[count]),
						LocatesGrid.Columns["Located"].CellValue(LocatesGrid.SelectedRows[count]).ToString(),
						LocatesGrid.Columns["Source"].CellText(LocatesGrid.SelectedRows[count]),
						LocatesGrid.Columns["FeeRate"].CellValue(LocatesGrid.SelectedRows[count]).ToString(),
						LocatesGrid.Columns["PreBorrow"].CellValue(LocatesGrid.SelectedRows[count]).ToString(),
						LocatesGrid.Columns["Comment"].CellText(LocatesGrid.SelectedRows[count]),
						mainForm.UserId
						);

					LocatesGrid[LocatesGrid.SelectedRows[count], "ActUserShortName"] = "me";
					LocatesGrid[LocatesGrid.SelectedRows[count], "ActTime"] = DateTime.Now.ToString("HH:mm:ss");					
								
					tempDataSet = mainForm.ShortSaleAgent.LocateItemGet(LocatesGrid.Columns["LocateId"].CellValue(LocatesGrid.SelectedRows[count]).ToString(), mainForm.UtcOffset);
				
					foreach (DataRow dr in tempDataSet.Tables["Table"].Rows)
					{
						mainDataSet.Tables["Locates"].LoadDataRow(dr.ItemArray, true);	
					}

					foreach (DataRow dr in tempDataSet.Tables["Table1"].Rows)
					{
						mainDataSet.Tables["LocateSummary"].LoadDataRow(dr.ItemArray, true);	
					}
				}
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);				
			}
		}

		void NotificationWindowTest ()
		{
			NotifyWindow nw = new NotifyWindow("Lcoate Notification", "Locate Notification");
			nw.Show();
		}

		private void ShortSaleLocateForm_Resize(object sender, System.EventArgs e)
		{
			if (this.WindowState.Equals(FormWindowState.Minimized))
			{
				if (!EnableResearchCheck.Checked)
				{
					AutoUpdateCheckBox.Checked = true;
				}
			}
		}

		private void ShortSaleLocateForm_Deactivate(object sender, System.EventArgs e)
		{
			if (!EnableResearchCheck.Checked)
			{
				AutoUpdateCheckBox.Checked = true;
			}
		}

		private void ShowPreBorrowsMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (shortSaleLocatesPreBorrowForm == null)
				{
					shortSaleLocatesPreBorrowForm = new ShortSaleLocatesPreBorrowForm(mainForm);
					shortSaleLocatesPreBorrowForm.MdiParent = mainForm;
					shortSaleLocatesPreBorrowForm.Show();
				}
				else
				{
					shortSaleLocatesPreBorrowForm.Activate();
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void LocatesGrid_BeforeColUpdate(object sender, C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs e)
		{
			try
			{
				if (e.Column.DataColumn.DataField.Equals("PreBorrow"))
				{
					if ((bool.Parse(e.OldValue.ToString()) == true) && (bool.Parse(LocatesGrid.Columns["PreBorrow"].Value.ToString()) == false))
					{
						LocatesGrid.Columns["Source"].Text = "";
						LocatesGrid.Columns["FeeRate"].Text = "";
					}
				}
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}		
	}
}
