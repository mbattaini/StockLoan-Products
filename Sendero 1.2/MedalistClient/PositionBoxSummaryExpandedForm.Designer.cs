namespace Anetics.Medalist
{
    partial class PositionBoxSummaryExpandedForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PositionBoxSummaryExpandedForm));
            this.CombinedBookGroupGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.BsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CombinedViewStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BookGroup1StripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BookGroup2StripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.availabilityStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.returnsStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lockupRollupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BookGroup1NameLabel = new C1.Win.C1Input.C1Label();
            this.BookGroup1Label = new C1.Win.C1Input.C1Label();
            this.BookGroup1Combo = new C1.Win.C1List.C1Combo();
            this.Vsplitter = new System.Windows.Forms.Splitter();
            this.lrPanel = new System.Windows.Forms.Panel();
            this.BookGroup2Grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.Hsplitter = new System.Windows.Forms.Splitter();
            this.BookGroup1Grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.BookGroup2NameLabel = new C1.Win.C1Input.C1Label();
            this.c1Label2 = new C1.Win.C1Input.C1Label();
            this.BookGroup2Combo = new C1.Win.C1List.C1Combo();
            ((System.ComponentModel.ISupportInitialize)(this.CombinedBookGroupGrid)).BeginInit();
            this.BsContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup1NameLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup1Label)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup1Combo)).BeginInit();
            this.lrPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup2Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup1Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup2NameLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup2Combo)).BeginInit();
            this.SuspendLayout();
            // 
            // CombinedBookGroupGrid
            // 
            this.CombinedBookGroupGrid.AllowColSelect = false;
            this.CombinedBookGroupGrid.AllowFilter = false;
            this.CombinedBookGroupGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.CombinedBookGroupGrid.AllowUpdate = false;
            this.CombinedBookGroupGrid.AllowUpdateOnBlur = false;
            this.CombinedBookGroupGrid.Caption = "Combined Group";
            this.CombinedBookGroupGrid.CaptionHeight = 17;
            this.CombinedBookGroupGrid.ContextMenuStrip = this.BsContextMenuStrip;
            this.CombinedBookGroupGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.CombinedBookGroupGrid.EmptyRows = true;
            this.CombinedBookGroupGrid.ExtendRightColumn = true;
            this.CombinedBookGroupGrid.FilterBar = true;
            this.CombinedBookGroupGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.CombinedBookGroupGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("CombinedBookGroupGrid.Images"))));
            this.CombinedBookGroupGrid.Location = new System.Drawing.Point(1, 50);
            this.CombinedBookGroupGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
            this.CombinedBookGroupGrid.Name = "CombinedBookGroupGrid";
            this.CombinedBookGroupGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.CombinedBookGroupGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.CombinedBookGroupGrid.PreviewInfo.ZoomFactor = 75D;
            this.CombinedBookGroupGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("CombinedBookGroupGrid.PrintInfo.PageSettings")));
            this.CombinedBookGroupGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.CombinedBookGroupGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.CombinedBookGroupGrid.RowHeight = 15;
            this.CombinedBookGroupGrid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
            this.CombinedBookGroupGrid.Size = new System.Drawing.Size(1434, 301);
            this.CombinedBookGroupGrid.TabIndex = 0;
            this.CombinedBookGroupGrid.Text = "c1TrueDBGrid1";
            this.CombinedBookGroupGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.BookGroupGrid_RowColChange);
            this.CombinedBookGroupGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.Grid_FormatText);
            this.CombinedBookGroupGrid.FilterChange += new System.EventHandler(this.CombinedBookGroupGrid_FilterChange);
            this.CombinedBookGroupGrid.PropBag = resources.GetString("CombinedBookGroupGrid.PropBag");
            // 
            // BsContextMenuStrip
            // 
            this.BsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CombinedViewStripMenuItem,
            this.BookGroup1StripMenuItem,
            this.BookGroup2StripMenuItem,
            this.sendToToolStripMenuItem,
            this.showToolStripMenuItem,
            this.syncFiltersToolStripMenuItem,
            this.lockupRollupToolStripMenuItem});
            this.BsContextMenuStrip.Name = "BsContextMenuStrip";
            this.BsContextMenuStrip.Size = new System.Drawing.Size(172, 158);
            this.BsContextMenuStrip.Text = "Options";
            // 
            // CombinedViewStripMenuItem
            // 
            this.CombinedViewStripMenuItem.Checked = true;
            this.CombinedViewStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CombinedViewStripMenuItem.Name = "CombinedViewStripMenuItem";
            this.CombinedViewStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.CombinedViewStripMenuItem.Text = "Combined View";
            this.CombinedViewStripMenuItem.Click += new System.EventHandler(this.CombinedViewStripMenuItem_Click);
            // 
            // BookGroup1StripMenuItem
            // 
            this.BookGroup1StripMenuItem.Checked = true;
            this.BookGroup1StripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BookGroup1StripMenuItem.Name = "BookGroup1StripMenuItem";
            this.BookGroup1StripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.BookGroup1StripMenuItem.Text = "BookGroup 1 View";
            this.BookGroup1StripMenuItem.Click += new System.EventHandler(this.BookGroup1StripMenuItem_Click);
            // 
            // BookGroup2StripMenuItem
            // 
            this.BookGroup2StripMenuItem.Checked = true;
            this.BookGroup2StripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BookGroup2StripMenuItem.Name = "BookGroup2StripMenuItem";
            this.BookGroup2StripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.BookGroup2StripMenuItem.Text = "BookGroup 2 View";
            this.BookGroup2StripMenuItem.Click += new System.EventHandler(this.BookGroup2StripMenuItem_Click);
            // 
            // sendToToolStripMenuItem
            // 
            this.sendToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelToolStripMenuItem});
            this.sendToToolStripMenuItem.Name = "sendToToolStripMenuItem";
            this.sendToToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.sendToToolStripMenuItem.Text = "Send To";
            // 
            // excelToolStripMenuItem
            // 
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.excelToolStripMenuItem.Text = "Excel";
            this.excelToolStripMenuItem.Click += new System.EventHandler(this.excelToolStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.availabilityStripMenuItem,
            this.returnsStripMenuItem,
            this.cancelStripMenuItem});
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // availabilityStripMenuItem
            // 
            this.availabilityStripMenuItem.Name = "availabilityStripMenuItem";
            this.availabilityStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.availabilityStripMenuItem.Text = "Availability";
            this.availabilityStripMenuItem.Click += new System.EventHandler(this.availabilityStripMenuItem_Click);
            // 
            // returnsStripMenuItem
            // 
            this.returnsStripMenuItem.Name = "returnsStripMenuItem";
            this.returnsStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.returnsStripMenuItem.Text = "Borrows / Returns";
            this.returnsStripMenuItem.Click += new System.EventHandler(this.returnsStripMenuItem_Click);
            // 
            // cancelStripMenuItem
            // 
            this.cancelStripMenuItem.Name = "cancelStripMenuItem";
            this.cancelStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.cancelStripMenuItem.Text = "Cancels / Recalls";
            this.cancelStripMenuItem.Click += new System.EventHandler(this.cancelStripMenuItem_Click);
            // 
            // syncFiltersToolStripMenuItem
            // 
            this.syncFiltersToolStripMenuItem.Name = "syncFiltersToolStripMenuItem";
            this.syncFiltersToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.syncFiltersToolStripMenuItem.Text = "Sync Filters";
            this.syncFiltersToolStripMenuItem.Click += new System.EventHandler(this.syncFiltersToolStripMenuItem_Click);
            // 
            // lockupRollupToolStripMenuItem
            // 
            this.lockupRollupToolStripMenuItem.Name = "lockupRollupToolStripMenuItem";
            this.lockupRollupToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.lockupRollupToolStripMenuItem.Text = "Lockup Rollup";
            this.lockupRollupToolStripMenuItem.Click += new System.EventHandler(this.lockupRollupToolStripMenuItem_Click);
            // 
            // BookGroup1NameLabel
            // 
            this.BookGroup1NameLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookGroup1NameLabel.ForeColor = System.Drawing.Color.Navy;
            this.BookGroup1NameLabel.Location = new System.Drawing.Point(219, 5);
            this.BookGroup1NameLabel.Name = "BookGroup1NameLabel";
            this.BookGroup1NameLabel.Size = new System.Drawing.Size(248, 18);
            this.BookGroup1NameLabel.TabIndex = 12;
            this.BookGroup1NameLabel.Tag = null;
            this.BookGroup1NameLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // BookGroup1Label
            // 
            this.BookGroup1Label.Location = new System.Drawing.Point(21, 5);
            this.BookGroup1Label.Name = "BookGroup1Label";
            this.BookGroup1Label.Size = new System.Drawing.Size(92, 18);
            this.BookGroup1Label.TabIndex = 10;
            this.BookGroup1Label.Tag = null;
            this.BookGroup1Label.Text = "Book Group (1):";
            this.BookGroup1Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BookGroup1Label.TextDetached = true;
            // 
            // BookGroup1Combo
            // 
            this.BookGroup1Combo.AddItemSeparator = ';';
            this.BookGroup1Combo.AutoCompletion = true;
            this.BookGroup1Combo.AutoDropDown = true;
            this.BookGroup1Combo.AutoSelect = true;
            this.BookGroup1Combo.AutoSize = false;
            this.BookGroup1Combo.Caption = "";
            this.BookGroup1Combo.CaptionHeight = 17;
            this.BookGroup1Combo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.BookGroup1Combo.ColumnCaptionHeight = 17;
            this.BookGroup1Combo.ColumnFooterHeight = 17;
            this.BookGroup1Combo.ContentHeight = 14;
            this.BookGroup1Combo.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.BookGroup1Combo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.BookGroup1Combo.DropDownWidth = 425;
            this.BookGroup1Combo.EditorBackColor = System.Drawing.SystemColors.Window;
            this.BookGroup1Combo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookGroup1Combo.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.BookGroup1Combo.EditorHeight = 14;
            this.BookGroup1Combo.ExtendRightColumn = true;
            this.BookGroup1Combo.Images.Add(((System.Drawing.Image)(resources.GetObject("BookGroup1Combo.Images"))));
            this.BookGroup1Combo.ItemHeight = 15;
            this.BookGroup1Combo.KeepForeColor = true;
            this.BookGroup1Combo.LimitToList = true;
            this.BookGroup1Combo.Location = new System.Drawing.Point(117, 4);
            this.BookGroup1Combo.MatchEntryTimeout = ((long)(2000));
            this.BookGroup1Combo.MaxDropDownItems = ((short)(10));
            this.BookGroup1Combo.MaxLength = 15;
            this.BookGroup1Combo.MouseCursor = System.Windows.Forms.Cursors.Arrow;
            this.BookGroup1Combo.Name = "BookGroup1Combo";
            this.BookGroup1Combo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.BookGroup1Combo.Size = new System.Drawing.Size(96, 20);
            this.BookGroup1Combo.TabIndex = 11;
            this.BookGroup1Combo.TextChanged += new System.EventHandler(this.BookGroupCombo_TextChanged);
            this.BookGroup1Combo.PropBag = resources.GetString("BookGroup1Combo.PropBag");
            // 
            // Vsplitter
            // 
            this.Vsplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.Vsplitter.Location = new System.Drawing.Point(1, 351);
            this.Vsplitter.Name = "Vsplitter";
            this.Vsplitter.Size = new System.Drawing.Size(1434, 3);
            this.Vsplitter.TabIndex = 15;
            this.Vsplitter.TabStop = false;
            // 
            // lrPanel
            // 
            this.lrPanel.Controls.Add(this.BookGroup2Grid);
            this.lrPanel.Controls.Add(this.Hsplitter);
            this.lrPanel.Controls.Add(this.BookGroup1Grid);
            this.lrPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lrPanel.Location = new System.Drawing.Point(1, 354);
            this.lrPanel.Name = "lrPanel";
            this.lrPanel.Size = new System.Drawing.Size(1434, 334);
            this.lrPanel.TabIndex = 16;
            // 
            // BookGroup2Grid
            // 
            this.BookGroup2Grid.AllowColSelect = false;
            this.BookGroup2Grid.AllowFilter = false;
            this.BookGroup2Grid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.BookGroup2Grid.AllowUpdate = false;
            this.BookGroup2Grid.AllowUpdateOnBlur = false;
            this.BookGroup2Grid.Caption = "Book Group (2)";
            this.BookGroup2Grid.CaptionHeight = 17;
            this.BookGroup2Grid.ContextMenuStrip = this.BsContextMenuStrip;
            this.BookGroup2Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BookGroup2Grid.EmptyRows = true;
            this.BookGroup2Grid.ExtendRightColumn = true;
            this.BookGroup2Grid.FilterBar = true;
            this.BookGroup2Grid.GroupByCaption = "Drag a column header here to group by that column";
            this.BookGroup2Grid.Images.Add(((System.Drawing.Image)(resources.GetObject("BookGroup2Grid.Images"))));
            this.BookGroup2Grid.Location = new System.Drawing.Point(0, 155);
            this.BookGroup2Grid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
            this.BookGroup2Grid.Name = "BookGroup2Grid";
            this.BookGroup2Grid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.BookGroup2Grid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.BookGroup2Grid.PreviewInfo.ZoomFactor = 75D;
            this.BookGroup2Grid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("BookGroup2Grid.PrintInfo.PageSettings")));
            this.BookGroup2Grid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.BookGroup2Grid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.BookGroup2Grid.RowHeight = 15;
            this.BookGroup2Grid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
            this.BookGroup2Grid.Size = new System.Drawing.Size(1434, 179);
            this.BookGroup2Grid.TabIndex = 17;
            this.BookGroup2Grid.Text = "c1TrueDBGrid3";
            this.BookGroup2Grid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.BookGroupGrid_RowColChange);
            this.BookGroup2Grid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.Grid_FormatText);
            this.BookGroup2Grid.FilterChange += new System.EventHandler(this.BookGroup2Grid_FilterChange);
            this.BookGroup2Grid.PropBag = resources.GetString("BookGroup2Grid.PropBag");
            // 
            // Hsplitter
            // 
            this.Hsplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.Hsplitter.Location = new System.Drawing.Point(0, 152);
            this.Hsplitter.Name = "Hsplitter";
            this.Hsplitter.Size = new System.Drawing.Size(1434, 3);
            this.Hsplitter.TabIndex = 16;
            this.Hsplitter.TabStop = false;
            // 
            // BookGroup1Grid
            // 
            this.BookGroup1Grid.AllowColSelect = false;
            this.BookGroup1Grid.AllowFilter = false;
            this.BookGroup1Grid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.BookGroup1Grid.AllowUpdate = false;
            this.BookGroup1Grid.AllowUpdateOnBlur = false;
            this.BookGroup1Grid.Caption = "Book Group (1)";
            this.BookGroup1Grid.CaptionHeight = 17;
            this.BookGroup1Grid.ContextMenuStrip = this.BsContextMenuStrip;
            this.BookGroup1Grid.Dock = System.Windows.Forms.DockStyle.Top;
            this.BookGroup1Grid.EmptyRows = true;
            this.BookGroup1Grid.ExtendRightColumn = true;
            this.BookGroup1Grid.FilterBar = true;
            this.BookGroup1Grid.GroupByCaption = "Drag a column header here to group by that column";
            this.BookGroup1Grid.Images.Add(((System.Drawing.Image)(resources.GetObject("BookGroup1Grid.Images"))));
            this.BookGroup1Grid.Location = new System.Drawing.Point(0, 0);
            this.BookGroup1Grid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
            this.BookGroup1Grid.Name = "BookGroup1Grid";
            this.BookGroup1Grid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.BookGroup1Grid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.BookGroup1Grid.PreviewInfo.ZoomFactor = 75D;
            this.BookGroup1Grid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("BookGroup1Grid.PrintInfo.PageSettings")));
            this.BookGroup1Grid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.BookGroup1Grid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.BookGroup1Grid.RowHeight = 15;
            this.BookGroup1Grid.RowSubDividerColor = System.Drawing.Color.Gainsboro;
            this.BookGroup1Grid.Size = new System.Drawing.Size(1434, 152);
            this.BookGroup1Grid.TabIndex = 1;
            this.BookGroup1Grid.Text = "c1TrueDBGrid2";
            this.BookGroup1Grid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.BookGroupGrid_RowColChange);
            this.BookGroup1Grid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.Grid_FormatText);
            this.BookGroup1Grid.FilterChange += new System.EventHandler(this.BookGroup1Grid_FilterChange);
            this.BookGroup1Grid.PropBag = resources.GetString("BookGroup1Grid.PropBag");
            // 
            // BookGroup2NameLabel
            // 
            this.BookGroup2NameLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookGroup2NameLabel.ForeColor = System.Drawing.Color.Navy;
            this.BookGroup2NameLabel.Location = new System.Drawing.Point(219, 28);
            this.BookGroup2NameLabel.Name = "BookGroup2NameLabel";
            this.BookGroup2NameLabel.Size = new System.Drawing.Size(248, 18);
            this.BookGroup2NameLabel.TabIndex = 20;
            this.BookGroup2NameLabel.Tag = null;
            this.BookGroup2NameLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // c1Label2
            // 
            this.c1Label2.Location = new System.Drawing.Point(21, 28);
            this.c1Label2.Name = "c1Label2";
            this.c1Label2.Size = new System.Drawing.Size(92, 18);
            this.c1Label2.TabIndex = 18;
            this.c1Label2.Tag = null;
            this.c1Label2.Text = "Book Group (2):";
            this.c1Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.c1Label2.TextDetached = true;
            // 
            // BookGroup2Combo
            // 
            this.BookGroup2Combo.AddItemSeparator = ';';
            this.BookGroup2Combo.AutoCompletion = true;
            this.BookGroup2Combo.AutoDropDown = true;
            this.BookGroup2Combo.AutoSelect = true;
            this.BookGroup2Combo.AutoSize = false;
            this.BookGroup2Combo.Caption = "";
            this.BookGroup2Combo.CaptionHeight = 17;
            this.BookGroup2Combo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.BookGroup2Combo.ColumnCaptionHeight = 17;
            this.BookGroup2Combo.ColumnFooterHeight = 17;
            this.BookGroup2Combo.ContentHeight = 14;
            this.BookGroup2Combo.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.BookGroup2Combo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.BookGroup2Combo.DropDownWidth = 425;
            this.BookGroup2Combo.EditorBackColor = System.Drawing.SystemColors.Window;
            this.BookGroup2Combo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookGroup2Combo.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.BookGroup2Combo.EditorHeight = 14;
            this.BookGroup2Combo.ExtendRightColumn = true;
            this.BookGroup2Combo.Images.Add(((System.Drawing.Image)(resources.GetObject("BookGroup2Combo.Images"))));
            this.BookGroup2Combo.ItemHeight = 15;
            this.BookGroup2Combo.KeepForeColor = true;
            this.BookGroup2Combo.LimitToList = true;
            this.BookGroup2Combo.Location = new System.Drawing.Point(117, 27);
            this.BookGroup2Combo.MatchEntryTimeout = ((long)(2000));
            this.BookGroup2Combo.MaxDropDownItems = ((short)(10));
            this.BookGroup2Combo.MaxLength = 15;
            this.BookGroup2Combo.MouseCursor = System.Windows.Forms.Cursors.Arrow;
            this.BookGroup2Combo.Name = "BookGroup2Combo";
            this.BookGroup2Combo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.BookGroup2Combo.Size = new System.Drawing.Size(96, 20);
            this.BookGroup2Combo.TabIndex = 19;
            this.BookGroup2Combo.TextChanged += new System.EventHandler(this.BookGroupCombo_TextChanged);
            this.BookGroup2Combo.PropBag = resources.GetString("BookGroup2Combo.PropBag");
            // 
            // PositionBoxSummaryExpandedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1436, 708);
            this.Controls.Add(this.BookGroup2NameLabel);
            this.Controls.Add(this.c1Label2);
            this.Controls.Add(this.BookGroup2Combo);
            this.Controls.Add(this.lrPanel);
            this.Controls.Add(this.Vsplitter);
            this.Controls.Add(this.BookGroup1NameLabel);
            this.Controls.Add(this.BookGroup1Label);
            this.Controls.Add(this.BookGroup1Combo);
            this.Controls.Add(this.CombinedBookGroupGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PositionBoxSummaryExpandedForm";
            this.Padding = new System.Windows.Forms.Padding(1, 50, 1, 20);
            this.Text = "Position - Box Summary - Expanded";
            this.Load += new System.EventHandler(this.PositionBoxSummaryExpandedForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CombinedBookGroupGrid)).EndInit();
            this.BsContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup1NameLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup1Label)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup1Combo)).EndInit();
            this.lrPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup2Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup1Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup2NameLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroup2Combo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid CombinedBookGroupGrid;
        private C1.Win.C1Input.C1Label BookGroup1NameLabel;
        private C1.Win.C1Input.C1Label BookGroup1Label;
        private C1.Win.C1List.C1Combo BookGroup1Combo;
        private System.Windows.Forms.Splitter Vsplitter;
        private System.Windows.Forms.Panel lrPanel;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid BookGroup2Grid;
        private System.Windows.Forms.Splitter Hsplitter;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid BookGroup1Grid;
        private C1.Win.C1Input.C1Label BookGroup2NameLabel;
        private C1.Win.C1Input.C1Label c1Label2;
        private C1.Win.C1List.C1Combo BookGroup2Combo;
        private System.Windows.Forms.ContextMenuStrip BsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem CombinedViewStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BookGroup1StripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BookGroup2StripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem availabilityStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem returnsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem syncFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockupRollupToolStripMenuItem;
    }
}