namespace CentralClient
{
  partial class TradingContractBlotterForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
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
        C1.Win.C1TrueDBGrid.Style style1 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style2 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style3 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style4 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style5 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style6 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style7 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style8 = new C1.Win.C1TrueDBGrid.Style();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradingContractBlotterForm));
        this.c1Sizer1 = new C1.Win.C1Sizer.C1Sizer();
        this.LoansGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
        this.splitter1 = new System.Windows.Forms.Splitter();
        this.LoansCountryCodeDropdown = new C1.Win.C1TrueDBGrid.C1TrueDBDropdown();
        this.BorrowsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
        this.BackPanel = new System.Windows.Forms.Panel();
        this.DateTimePicker = new C1.Win.C1Input.C1DateEdit();
        this.BizDateLabel = new System.Windows.Forms.Label();
        this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
        this.label1 = new System.Windows.Forms.Label();
        this.BookGroupCombo = new C1.Win.C1List.C1Combo();
        this.ContextMenu = new C1.Win.C1Command.C1ContextMenu();
        this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
        this.NewDealCommand = new C1.Win.C1Command.C1Command();
        this.SendToCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToCommand = new C1.Win.C1Command.C1CommandMenu();
        this.SendToClipboardCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToClipboardCommand = new C1.Win.C1Command.C1Command();
        this.SendToExcelCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToExcelCommand = new C1.Win.C1Command.C1Command();
        this.SendToEmailCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToEmailCommand = new C1.Win.C1Command.C1Command();
        this.DealToCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.DealToCommand = new C1.Win.C1Command.C1CommandMenu();
        this.DealToContractCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.DealToContractCommand = new C1.Win.C1Command.C1Command();
        this.RefreshCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.RefreshCommand = new C1.Win.C1Command.C1Command();
        this.DealToFlipCommand = new C1.Win.C1Command.C1Command();
        this.MainCommandHolder = new C1.Win.C1Command.C1CommandHolder();
        this.DealToContractAsOfCommand = new C1.Win.C1Command.C1Command();
        this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
        ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).BeginInit();
        this.c1Sizer1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.LoansGrid)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.LoansCountryCodeDropdown)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.BorrowsGrid)).BeginInit();
        this.BackPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.DateTimePicker)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
        this.SuspendLayout();
        // 
        // c1Sizer1
        // 
        this.c1Sizer1.Controls.Add(this.LoansGrid);
        this.c1Sizer1.Controls.Add(this.splitter1);
        this.c1Sizer1.Controls.Add(this.LoansCountryCodeDropdown);
        this.c1Sizer1.Controls.Add(this.BorrowsGrid);
        this.c1Sizer1.Controls.Add(this.BackPanel);
        this.c1Sizer1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.c1Sizer1.GridDefinition = "98.5585585585586:False:False;\t99.1666666666667:False:False;";
        this.c1Sizer1.Location = new System.Drawing.Point(0, 0);
        this.c1Sizer1.Name = "c1Sizer1";
        this.c1Sizer1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
        this.c1Sizer1.Size = new System.Drawing.Size(1200, 555);
        this.c1Sizer1.TabIndex = 0;
        this.c1Sizer1.Text = "c1Sizer1";
        // 
        // LoansGrid
        // 
        this.LoansGrid.AllowColSelect = false;
        this.LoansGrid.AllowDelete = true;
        this.LoansGrid.AllowFilter = false;
        this.LoansGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
        this.LoansGrid.BackColor = System.Drawing.Color.Silver;
        this.MainCommandHolder.SetC1ContextMenu(this.LoansGrid, this.ContextMenu);
        this.LoansGrid.CaptionHeight = 17;
        this.LoansGrid.CellTips = C1.Win.C1TrueDBGrid.CellTipEnum.Floating;
        this.LoansGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        this.LoansGrid.EmptyRows = true;
        this.LoansGrid.ExtendRightColumn = true;
        this.LoansGrid.FetchRowStyles = true;
        this.LoansGrid.FilterBar = true;
        this.LoansGrid.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.Flat;
        this.LoansGrid.GroupByCaption = "Drag a column header here to group by that column";
        this.LoansGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("LoansGrid.Images"))));
        this.LoansGrid.Location = new System.Drawing.Point(0, 270);
        this.LoansGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
        this.LoansGrid.Name = "LoansGrid";
        this.LoansGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
        this.LoansGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
        this.LoansGrid.PreviewInfo.ZoomFactor = 75D;
        this.LoansGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("LoansGrid.PrintInfo.PageSettings")));
        this.LoansGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
        this.LoansGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
        this.LoansGrid.RowHeight = 15;
        this.LoansGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
        this.LoansGrid.Size = new System.Drawing.Size(1200, 285);
        this.LoansGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
        this.LoansGrid.TabIndex = 110;
        this.LoansGrid.Text = "Contract Blotter Loans";
        this.LoansGrid.UseColumnStyles = false;
        this.LoansGrid.WrapCellPointer = true;
        this.LoansGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.LoansGrid_BeforeDelete);
        this.LoansGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
        this.LoansGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.LoansGrid_FetchRowStyle);
        this.LoansGrid.Error += new C1.Win.C1TrueDBGrid.ErrorEventHandler(this.LoansGrid_Error);
        this.LoansGrid.DoubleClick += new System.EventHandler(this.LoansGrid_DoubleClick);
        this.LoansGrid.PropBag = resources.GetString("LoansGrid.PropBag");
        // 
        // splitter1
        // 
        this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
        this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
        this.splitter1.Location = new System.Drawing.Point(0, 267);
        this.splitter1.Name = "splitter1";
        this.splitter1.Size = new System.Drawing.Size(1200, 3);
        this.splitter1.TabIndex = 115;
        this.splitter1.TabStop = false;
        // 
        // LoansCountryCodeDropdown
        // 
        this.LoansCountryCodeDropdown.AllowColMove = true;
        this.LoansCountryCodeDropdown.AllowColSelect = true;
        this.LoansCountryCodeDropdown.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows;
        this.LoansCountryCodeDropdown.AlternatingRows = false;
        this.LoansCountryCodeDropdown.CaptionHeight = 17;
        this.LoansCountryCodeDropdown.CaptionStyle = style1;
        this.LoansCountryCodeDropdown.ColumnCaptionHeight = 17;
        this.LoansCountryCodeDropdown.ColumnFooterHeight = 17;
        this.LoansCountryCodeDropdown.EmptyRows = true;
        this.LoansCountryCodeDropdown.EvenRowStyle = style2;
        this.LoansCountryCodeDropdown.ExtendRightColumn = true;
        this.LoansCountryCodeDropdown.FetchRowStyles = false;
        this.LoansCountryCodeDropdown.FooterStyle = style3;
        this.LoansCountryCodeDropdown.HeadingStyle = style4;
        this.LoansCountryCodeDropdown.HighLightRowStyle = style5;
        this.LoansCountryCodeDropdown.Images.Add(((System.Drawing.Image)(resources.GetObject("LoansCountryCodeDropdown.Images"))));
        this.LoansCountryCodeDropdown.Location = new System.Drawing.Point(601, 417);
        this.LoansCountryCodeDropdown.Name = "LoansCountryCodeDropdown";
        this.LoansCountryCodeDropdown.OddRowStyle = style6;
        this.LoansCountryCodeDropdown.RecordSelectorStyle = style7;
        this.LoansCountryCodeDropdown.RowDivider.Color = System.Drawing.Color.LightGray;
        this.LoansCountryCodeDropdown.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
        this.LoansCountryCodeDropdown.RowHeight = 15;
        this.LoansCountryCodeDropdown.RowSubDividerColor = System.Drawing.Color.DarkGray;
        this.LoansCountryCodeDropdown.ScrollTips = false;
        this.LoansCountryCodeDropdown.Size = new System.Drawing.Size(301, 150);
        this.LoansCountryCodeDropdown.Style = style8;
        this.LoansCountryCodeDropdown.TabIndex = 112;
        this.LoansCountryCodeDropdown.TabStop = false;
        this.LoansCountryCodeDropdown.Text = "c1TrueDBDropdown2";
        this.LoansCountryCodeDropdown.Visible = false;
        this.LoansCountryCodeDropdown.PropBag = resources.GetString("LoansCountryCodeDropdown.PropBag");
        // 
        // BorrowsGrid
        // 
        this.BorrowsGrid.AllowColSelect = false;
        this.BorrowsGrid.AllowDelete = true;
        this.BorrowsGrid.AllowFilter = false;
        this.BorrowsGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
        this.BorrowsGrid.BackColor = System.Drawing.Color.Silver;
        this.MainCommandHolder.SetC1ContextMenu(this.BorrowsGrid, this.ContextMenu);
        this.BorrowsGrid.CaptionHeight = 17;
        this.BorrowsGrid.CellTips = C1.Win.C1TrueDBGrid.CellTipEnum.Floating;
        this.BorrowsGrid.Dock = System.Windows.Forms.DockStyle.Top;
        this.BorrowsGrid.EmptyRows = true;
        this.BorrowsGrid.ExtendRightColumn = true;
        this.BorrowsGrid.FetchRowStyles = true;
        this.BorrowsGrid.FilterBar = true;
        this.BorrowsGrid.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.Flat;
        this.BorrowsGrid.GroupByCaption = "Drag a column header here to group by that column";
        this.BorrowsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("BorrowsGrid.Images"))));
        this.BorrowsGrid.Location = new System.Drawing.Point(0, 34);
        this.BorrowsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
        this.BorrowsGrid.Name = "BorrowsGrid";
        this.BorrowsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
        this.BorrowsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
        this.BorrowsGrid.PreviewInfo.ZoomFactor = 75D;
        this.BorrowsGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("BorrowsGrid.PrintInfo.PageSettings")));
        this.BorrowsGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
        this.BorrowsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
        this.BorrowsGrid.RowHeight = 15;
        this.BorrowsGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
        this.BorrowsGrid.Size = new System.Drawing.Size(1200, 233);
        this.BorrowsGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
        this.BorrowsGrid.TabIndex = 105;
        this.BorrowsGrid.Text = "Contract Blotter Borrows";
        this.BorrowsGrid.UseColumnStyles = false;
        this.BorrowsGrid.WrapCellPointer = true;
        this.BorrowsGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.BorrowsGrid_BeforeDelete);
        this.BorrowsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
        this.BorrowsGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.BorrowsGrid_FetchRowStyle);
        this.BorrowsGrid.Error += new C1.Win.C1TrueDBGrid.ErrorEventHandler(this.BorrowsGrid_Error);
        this.BorrowsGrid.DoubleClick += new System.EventHandler(this.BorrowsGrid_DoubleClick);
        this.BorrowsGrid.PropBag = resources.GetString("BorrowsGrid.PropBag");
        // 
        // BackPanel
        // 
        this.BackPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
        this.MainCommandHolder.SetC1ContextMenu(this.BackPanel, this.ContextMenu);
        this.BackPanel.Controls.Add(this.DateTimePicker);
        this.BackPanel.Controls.Add(this.BizDateLabel);
        this.BackPanel.Controls.Add(this.BookGroupNameLabel);
        this.BackPanel.Controls.Add(this.label1);
        this.BackPanel.Controls.Add(this.BookGroupCombo);
        this.BackPanel.Dock = System.Windows.Forms.DockStyle.Top;
        this.BackPanel.Location = new System.Drawing.Point(0, 0);
        this.BackPanel.Name = "BackPanel";
        this.BackPanel.Size = new System.Drawing.Size(1200, 34);
        this.BackPanel.TabIndex = 104;
        // 
        // DateTimePicker
        // 
        this.DateTimePicker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        // 
        // 
        // 
        this.DateTimePicker.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
        this.DateTimePicker.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
        this.DateTimePicker.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
        this.DateTimePicker.CustomFormat = "yyyy-MM-dd";
        this.DateTimePicker.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
        this.DateTimePicker.Location = new System.Drawing.Point(102, 7);
        this.DateTimePicker.Name = "DateTimePicker";
        this.DateTimePicker.Size = new System.Drawing.Size(103, 19);
        this.DateTimePicker.TabIndex = 112;
        this.DateTimePicker.Tag = null;
        this.DateTimePicker.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
        this.DateTimePicker.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
        // 
        // BizDateLabel
        // 
        this.BizDateLabel.AutoSize = true;
        this.BizDateLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
        this.BizDateLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.BizDateLabel.ForeColor = System.Drawing.Color.Black;
        this.BizDateLabel.Location = new System.Drawing.Point(4, 10);
        this.BizDateLabel.Name = "BizDateLabel";
        this.BizDateLabel.Size = new System.Drawing.Size(93, 13);
        this.BizDateLabel.TabIndex = 104;
        this.BizDateLabel.Text = "Business Date:";
        this.BizDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // BookGroupNameLabel
        // 
        this.BookGroupNameLabel.AutoSize = true;
        this.BookGroupNameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
        this.BookGroupNameLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.BookGroupNameLabel.ForeColor = System.Drawing.Color.Black;
        this.BookGroupNameLabel.Location = new System.Drawing.Point(390, 10);
        this.BookGroupNameLabel.Name = "BookGroupNameLabel";
        this.BookGroupNameLabel.Size = new System.Drawing.Size(134, 13);
        this.BookGroupNameLabel.TabIndex = 103;
        this.BookGroupNameLabel.Tag = null;
        this.BookGroupNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.BookGroupNameLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
        this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.ForeColor = System.Drawing.Color.Black;
        this.label1.Location = new System.Drawing.Point(211, 10);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(80, 13);
        this.label1.TabIndex = 100;
        this.label1.Text = "Book Group:";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // BookGroupCombo
        // 
        this.BookGroupCombo.AddItemSeparator = ';';
        this.BookGroupCombo.Caption = "";
        this.BookGroupCombo.CaptionHeight = 17;
        this.BookGroupCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
        this.BookGroupCombo.ColumnCaptionHeight = 17;
        this.BookGroupCombo.ColumnFooterHeight = 17;
        this.BookGroupCombo.ColumnWidth = 100;
        this.BookGroupCombo.ContentHeight = 16;
        this.BookGroupCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
        this.BookGroupCombo.DropDownWidth = 300;
        this.BookGroupCombo.EditorBackColor = System.Drawing.SystemColors.Window;
        this.BookGroupCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.BookGroupCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
        this.BookGroupCombo.EditorHeight = 16;
        this.BookGroupCombo.ExtendRightColumn = true;
        this.BookGroupCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("BookGroupCombo.Images"))));
        this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("BookGroupCombo.Images1"))));
        this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("BookGroupCombo.Images2"))));
        this.BookGroupCombo.ItemHeight = 15;
        this.BookGroupCombo.Location = new System.Drawing.Point(294, 5);
        this.BookGroupCombo.MatchEntryTimeout = ((long)(2000));
        this.BookGroupCombo.MaxDropDownItems = ((short)(5));
        this.BookGroupCombo.MaxLength = 32767;
        this.BookGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
        this.BookGroupCombo.Name = "BookGroupCombo";
        this.BookGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
        this.BookGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
        this.BookGroupCombo.Size = new System.Drawing.Size(90, 22);
        this.BookGroupCombo.TabIndex = 101;
        this.BookGroupCombo.Text = "c1Combo1";
        this.BookGroupCombo.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Silver;
        this.BookGroupCombo.TextChanged += new System.EventHandler(this.BookGroupCombo_TextChanged);
        this.BookGroupCombo.PropBag = resources.GetString("BookGroupCombo.PropBag");
        // 
        // ContextMenu
        // 
        this.ContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1,
            this.SendToCommandLink,
            this.DealToCommandLink,
            this.RefreshCommandLink});
        this.ContextMenu.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ContextMenu.Name = "ContextMenu";
        this.ContextMenu.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
        this.ContextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Silver;
        // 
        // c1CommandLink1
        // 
        this.c1CommandLink1.Command = this.NewDealCommand;
        // 
        // NewDealCommand
        // 
        this.NewDealCommand.Name = "NewDealCommand";
        this.NewDealCommand.Text = "New Deal";
        this.NewDealCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.NewDealCommand_Click);
        // 
        // SendToCommandLink
        // 
        this.SendToCommandLink.Command = this.SendToCommand;
        this.SendToCommandLink.SortOrder = 1;
        // 
        // SendToCommand
        // 
        this.SendToCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SendToClipboardCommandLink,
            this.SendToExcelCommandLink,
            this.SendToEmailCommandLink});
        this.SendToCommand.Name = "SendToCommand";
        this.SendToCommand.Text = "Send To";
        this.SendToCommand.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
        this.SendToCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Silver;
        // 
        // SendToClipboardCommandLink
        // 
        this.SendToClipboardCommandLink.Command = this.SendToClipboardCommand;
        // 
        // SendToClipboardCommand
        // 
        this.SendToClipboardCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToClipboardCommand.Icon")));
        this.SendToClipboardCommand.Name = "SendToClipboardCommand";
        this.SendToClipboardCommand.Text = "Clipboard";
        this.SendToClipboardCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SendToClipboardCommand_Click);
        // 
        // SendToExcelCommandLink
        // 
        this.SendToExcelCommandLink.Command = this.SendToExcelCommand;
        this.SendToExcelCommandLink.SortOrder = 1;
        // 
        // SendToExcelCommand
        // 
        this.SendToExcelCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToExcelCommand.Icon")));
        this.SendToExcelCommand.Name = "SendToExcelCommand";
        this.SendToExcelCommand.Text = "Excel";
        this.SendToExcelCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SendToExcelCommand_Click);
        // 
        // SendToEmailCommandLink
        // 
        this.SendToEmailCommandLink.Command = this.SendToEmailCommand;
        this.SendToEmailCommandLink.SortOrder = 2;
        // 
        // SendToEmailCommand
        // 
        this.SendToEmailCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToEmailCommand.Icon")));
        this.SendToEmailCommand.Name = "SendToEmailCommand";
        this.SendToEmailCommand.Text = "Mail";
        this.SendToEmailCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SendToEmailCommand_Click);
        // 
        // DealToCommandLink
        // 
        this.DealToCommandLink.Command = this.DealToCommand;
        this.DealToCommandLink.SortOrder = 2;
        // 
        // DealToCommand
        // 
        this.DealToCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.DealToContractCommandLink});
        this.DealToCommand.Name = "DealToCommand";
        this.DealToCommand.Text = "Deal To";
        this.DealToCommand.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
        this.DealToCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Silver;
        // 
        // DealToContractCommandLink
        // 
        this.DealToContractCommandLink.Command = this.DealToContractCommand;
        // 
        // DealToContractCommand
        // 
        this.DealToContractCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("DealToContractCommand.Icon")));
        this.DealToContractCommand.Name = "DealToContractCommand";
        this.DealToContractCommand.Text = "Contract";
        this.DealToContractCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SendToContractCommand_Click);
        // 
        // RefreshCommandLink
        // 
        this.RefreshCommandLink.Command = this.RefreshCommand;
        this.RefreshCommandLink.SortOrder = 3;
        // 
        // RefreshCommand
        // 
        this.RefreshCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("RefreshCommand.Icon")));
        this.RefreshCommand.Name = "RefreshCommand";
        this.RefreshCommand.Text = "Refresh";
        this.RefreshCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.RefreshCommand_Click);
        // 
        // DealToFlipCommand
        // 
        this.DealToFlipCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("DealToFlipCommand.Icon")));
        this.DealToFlipCommand.Name = "DealToFlipCommand";
        this.DealToFlipCommand.Text = "Flip Deal";
        // 
        // MainCommandHolder
        // 
        this.MainCommandHolder.Commands.Add(this.ContextMenu);
        this.MainCommandHolder.Commands.Add(this.SendToCommand);
        this.MainCommandHolder.Commands.Add(this.DealToContractCommand);
        this.MainCommandHolder.Commands.Add(this.SendToExcelCommand);
        this.MainCommandHolder.Commands.Add(this.DealToCommand);
        this.MainCommandHolder.Commands.Add(this.DealToFlipCommand);
        this.MainCommandHolder.Commands.Add(this.SendToClipboardCommand);
        this.MainCommandHolder.Commands.Add(this.SendToEmailCommand);
        this.MainCommandHolder.Commands.Add(this.RefreshCommand);
        this.MainCommandHolder.Commands.Add(this.DealToContractAsOfCommand);
        this.MainCommandHolder.Commands.Add(this.NewDealCommand);
        this.MainCommandHolder.Owner = this;
        // 
        // DealToContractAsOfCommand
        // 
        this.DealToContractAsOfCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("DealToContractAsOfCommand.Icon")));
        this.DealToContractAsOfCommand.Name = "DealToContractAsOfCommand";
        this.DealToContractAsOfCommand.Text = "Contract As Of";
        // 
        // c1StatusBar1
        // 
        this.c1StatusBar1.Name = "c1StatusBar1";
        // 
        // TradingContractBlotterForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.MainCommandHolder.SetC1ContextMenu(this, this.ContextMenu);
        this.ClientSize = new System.Drawing.Size(1200, 578);
        this.Controls.Add(this.c1Sizer1);
        this.Controls.Add(this.c1StatusBar1);
        this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Name = "TradingContractBlotterForm";
        this.Text = "Trading - Contract Blotter";
        this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Windows7;
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TradingContractBlotterForm_FormClosed);
        this.Load += new System.EventHandler(this.TradingContractBlotterForm_Load);
        ((System.ComponentModel.ISupportInitialize)(this.c1Sizer1)).EndInit();
        this.c1Sizer1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.LoansGrid)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.LoansCountryCodeDropdown)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.BorrowsGrid)).EndInit();
        this.BackPanel.ResumeLayout(false);
        this.BackPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.DateTimePicker)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

	  private C1.Win.C1Sizer.C1Sizer c1Sizer1;
    private C1.Win.C1List.C1Combo BookGroupCombo;
	  private System.Windows.Forms.Label label1;
    private C1.Win.C1Input.C1Label BookGroupNameLabel;
	  private System.Windows.Forms.Panel BackPanel;
      private C1.Win.C1Command.C1ContextMenu ContextMenu;
    private C1.Win.C1Command.C1CommandHolder MainCommandHolder;
    private C1.Win.C1Command.C1CommandMenu SendToCommand;
    private C1.Win.C1Command.C1CommandLink DealToContractCommandLink;
    private C1.Win.C1Command.C1Command DealToContractCommand;
	  private System.Windows.Forms.Label BizDateLabel;
	  private C1.Win.C1Command.C1CommandLink SendToExcelCommandLink;
      private C1.Win.C1Command.C1Command SendToExcelCommand;
      private C1.Win.C1TrueDBGrid.C1TrueDBDropdown LoansCountryCodeDropdown;
      private C1.Win.C1TrueDBGrid.C1TrueDBGrid LoansGrid;
	  private C1.Win.C1TrueDBGrid.C1TrueDBGrid BorrowsGrid;
	  private System.Windows.Forms.Splitter splitter1;
	  private C1.Win.C1Command.C1CommandLink DealToCommandLink;
      private C1.Win.C1Command.C1CommandMenu DealToCommand;
      private C1.Win.C1Command.C1Command DealToFlipCommand;
      private C1.Win.C1Command.C1CommandLink SendToClipboardCommandLink;
      private C1.Win.C1Command.C1Command SendToClipboardCommand;
      private C1.Win.C1Command.C1CommandLink SendToEmailCommandLink;
      private C1.Win.C1Command.C1Command SendToEmailCommand;
	  private C1.Win.C1Command.C1CommandLink RefreshCommandLink;
	  private C1.Win.C1Command.C1Command RefreshCommand;
	  private C1.Win.C1Command.C1Command DealToContractAsOfCommand;
      private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
      private C1.Win.C1Input.C1DateEdit DateTimePicker;
      private C1.Win.C1Command.C1CommandLink c1CommandLink1;
      private C1.Win.C1Command.C1Command NewDealCommand;
      private C1.Win.C1Command.C1CommandLink SendToCommandLink;
  }
}