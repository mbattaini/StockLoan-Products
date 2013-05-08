namespace LocatesClient
{
  partial class LocatesSummaryForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocatesSummaryForm));
        this.LocatesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
        this.panel1 = new System.Windows.Forms.Panel();
        this.InventoryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
        this.LocatesSummaryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
        this.MainCommandHolder = new C1.Win.C1Command.C1CommandHolder();
        this.MainContextMenu = new C1.Win.C1Command.C1ContextMenu();
        this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
        this.SendToCommand = new C1.Win.C1Command.C1CommandMenu();
        this.SendToEmailCommandMenuLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToEmailCommand = new C1.Win.C1Command.C1Command();
        this.SendToExcelCommandMenuLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToExcelCommand = new C1.Win.C1Command.C1Command();
        this.SendToPdfCommandMenuLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToPdfCommand = new C1.Win.C1Command.C1Command();
        this.MainDockingTab = new C1.Win.C1Command.C1DockingTab();
        this.SecurityIdSummaryDockingPage = new C1.Win.C1Command.C1DockingTabPage();
        this.LocatesSecIdGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
        this.GroupCodeSummaryDockingPage = new C1.Win.C1Command.C1DockingTabPage();
        this.LocatesGroupCodeGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
        this.panel2 = new System.Windows.Forms.Panel();
        this.ShowPendingCheckBox = new System.Windows.Forms.CheckBox();
        this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
        this.TradeDateLabel = new C1.Win.C1Input.C1Label();
        this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
        ((System.ComponentModel.ISupportInitialize)(this.LocatesGrid)).BeginInit();
        this.panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.InventoryGrid)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.LocatesSummaryGrid)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.MainDockingTab)).BeginInit();
        this.MainDockingTab.SuspendLayout();
        this.SecurityIdSummaryDockingPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.LocatesSecIdGrid)).BeginInit();
        this.GroupCodeSummaryDockingPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.LocatesGroupCodeGrid)).BeginInit();
        this.panel2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.TradeDateLabel)).BeginInit();
        this.SuspendLayout();
        // 
        // LocatesGrid
        // 
        this.LocatesGrid.AllowColSelect = false;
        this.LocatesGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
        this.LocatesGrid.AlternatingRows = true;
        this.MainCommandHolder.SetC1ContextMenu(this.LocatesGrid, this.MainContextMenu);
        this.LocatesGrid.CaptionHeight = 17;
        this.LocatesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
        this.LocatesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        this.LocatesGrid.EmptyRows = true;
        this.LocatesGrid.ExtendRightColumn = true;
        this.LocatesGrid.FetchRowStyles = true;
        this.LocatesGrid.FilterBar = true;
        this.LocatesGrid.GroupByAreaVisible = false;
        this.LocatesGrid.GroupByCaption = "Drag a column header here to group by that column";
        this.LocatesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("LocatesGrid.Images"))));
        this.LocatesGrid.Location = new System.Drawing.Point(201, 25);
        this.LocatesGrid.MaintainRowCurrency = true;
        this.LocatesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;
        this.LocatesGrid.Name = "LocatesGrid";
        this.LocatesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
        this.LocatesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
        this.LocatesGrid.PreviewInfo.ZoomFactor = 75;
        this.LocatesGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("LocatesGrid.PrintInfo.PageSettings")));
        this.LocatesGrid.RecordSelectors = false;
        this.LocatesGrid.RowDivider.Color = System.Drawing.Color.LightGray;
        this.LocatesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
        this.LocatesGrid.RowHeight = 17;
        this.LocatesGrid.Size = new System.Drawing.Size(1255, 485);
        this.LocatesGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
        this.LocatesGrid.TabIndex = 0;
        this.LocatesGrid.Text = "5";
        this.LocatesGrid.UseColumnStyles = false;
        this.LocatesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.LocatesGrid_FormatText);
        this.LocatesGrid.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.LocatesGrid_AfterColEdit);
        this.LocatesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.LocatesGrid_BeforeUpdate);
        this.LocatesGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.LocatesGrid_FetchRowStyle);
        this.LocatesGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.LocatesGrid_BeforeColEdit);
        this.LocatesGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.LocatesGrid_Paint);
        this.LocatesGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LocatesGrid_KeyPress);
        this.LocatesGrid.PropBag = resources.GetString("LocatesGrid.PropBag");
        // 
        // panel1
        // 
        this.panel1.BackColor = System.Drawing.Color.AliceBlue;
        this.panel1.Controls.Add(this.InventoryGrid);
        this.panel1.Controls.Add(this.LocatesSummaryGrid);
        this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.panel1.Location = new System.Drawing.Point(0, 510);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(1456, 239);
        this.panel1.TabIndex = 1;
        // 
        // InventoryGrid
        // 
        this.InventoryGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.InventoryGrid.CaptionHeight = 17;
        this.InventoryGrid.EmptyRows = true;
        this.InventoryGrid.ExtendRightColumn = true;
        this.InventoryGrid.FetchRowStyles = true;
        this.InventoryGrid.GroupByCaption = "Drag a column header here to group by that column";
        this.InventoryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("InventoryGrid.Images"))));
        this.InventoryGrid.Location = new System.Drawing.Point(12, 5);
        this.InventoryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
        this.InventoryGrid.Name = "InventoryGrid";
        this.InventoryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
        this.InventoryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
        this.InventoryGrid.PreviewInfo.ZoomFactor = 75;
        this.InventoryGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("InventoryGrid.PrintInfo.PageSettings")));
        this.InventoryGrid.RecordSelectors = false;
        this.InventoryGrid.RowDivider.Color = System.Drawing.Color.LightGray;
        this.InventoryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
        this.InventoryGrid.RowHeight = 15;
        this.InventoryGrid.Size = new System.Drawing.Size(500, 230);
        this.InventoryGrid.TabIndex = 1;
        this.InventoryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.InventoryGrid_FormatText);
        this.InventoryGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.InventoryGrid_FetchRowStyle);
        this.InventoryGrid.PropBag = resources.GetString("InventoryGrid.PropBag");
        // 
        // LocatesSummaryGrid
        // 
        this.LocatesSummaryGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.LocatesSummaryGrid.CaptionHeight = 17;
        this.LocatesSummaryGrid.ColumnFooters = true;
        this.LocatesSummaryGrid.EmptyRows = true;
        this.LocatesSummaryGrid.ExtendRightColumn = true;
        this.LocatesSummaryGrid.GroupByCaption = "Drag a column header here to group by that column";
        this.LocatesSummaryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("LocatesSummaryGrid.Images"))));
        this.LocatesSummaryGrid.Location = new System.Drawing.Point(1140, 6);
        this.LocatesSummaryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
        this.LocatesSummaryGrid.Name = "LocatesSummaryGrid";
        this.LocatesSummaryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
        this.LocatesSummaryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
        this.LocatesSummaryGrid.PreviewInfo.ZoomFactor = 75;
        this.LocatesSummaryGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("LocatesSummaryGrid.PrintInfo.PageSettings")));
        this.LocatesSummaryGrid.RecordSelectors = false;
        this.LocatesSummaryGrid.RowDivider.Color = System.Drawing.Color.LightGray;
        this.LocatesSummaryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
        this.LocatesSummaryGrid.RowHeight = 15;
        this.LocatesSummaryGrid.Size = new System.Drawing.Size(313, 229);
        this.LocatesSummaryGrid.TabIndex = 2;
        this.LocatesSummaryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.LocatesSummaryGrid_FormatText);
        this.LocatesSummaryGrid.PropBag = resources.GetString("LocatesSummaryGrid.PropBag");
        // 
        // MainCommandHolder
        // 
        this.MainCommandHolder.Commands.Add(this.MainContextMenu);
        this.MainCommandHolder.Commands.Add(this.SendToCommand);
        this.MainCommandHolder.Commands.Add(this.SendToExcelCommand);
        this.MainCommandHolder.Commands.Add(this.SendToPdfCommand);
        this.MainCommandHolder.Commands.Add(this.SendToEmailCommand);
        this.MainCommandHolder.Owner = this;
        // 
        // MainContextMenu
        // 
        this.MainContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1});
        this.MainContextMenu.Name = "MainContextMenu";
        this.MainContextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
        // 
        // c1CommandLink1
        // 
        this.c1CommandLink1.Command = this.SendToCommand;
        // 
        // SendToCommand
        // 
        this.SendToCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SendToEmailCommandMenuLink,
            this.SendToExcelCommandMenuLink,
            this.SendToPdfCommandMenuLink});
        this.SendToCommand.Name = "SendToCommand";
        this.SendToCommand.Text = "Send To";
        this.SendToCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
        // 
        // SendToEmailCommandMenuLink
        // 
        this.SendToEmailCommandMenuLink.Command = this.SendToEmailCommand;
        // 
        // SendToEmailCommand
        // 
        this.SendToEmailCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToEmailCommand.Icon")));
        this.SendToEmailCommand.Name = "SendToEmailCommand";
        this.SendToEmailCommand.Text = "Email";
        // 
        // SendToExcelCommandMenuLink
        // 
        this.SendToExcelCommandMenuLink.Command = this.SendToExcelCommand;
        this.SendToExcelCommandMenuLink.SortOrder = 1;
        // 
        // SendToExcelCommand
        // 
        this.SendToExcelCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToExcelCommand.Icon")));
        this.SendToExcelCommand.Name = "SendToExcelCommand";
        this.SendToExcelCommand.Text = "Excel";
        // 
        // SendToPdfCommandMenuLink
        // 
        this.SendToPdfCommandMenuLink.Command = this.SendToPdfCommand;
        this.SendToPdfCommandMenuLink.SortOrder = 2;
        // 
        // SendToPdfCommand
        // 
        this.SendToPdfCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToPdfCommand.Icon")));
        this.SendToPdfCommand.Name = "SendToPdfCommand";
        this.SendToPdfCommand.Text = "Pdf";
        // 
        // MainDockingTab
        // 
        this.MainDockingTab.Controls.Add(this.SecurityIdSummaryDockingPage);
        this.MainDockingTab.Controls.Add(this.GroupCodeSummaryDockingPage);
        this.MainDockingTab.Dock = System.Windows.Forms.DockStyle.Left;
        this.MainDockingTab.Location = new System.Drawing.Point(0, 0);
        this.MainDockingTab.Name = "MainDockingTab";
        this.MainDockingTab.Size = new System.Drawing.Size(201, 510);
        this.MainDockingTab.TabIndex = 3;
        this.MainDockingTab.TabsSpacing = 5;
        this.MainDockingTab.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007;
        this.MainDockingTab.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue;
        this.MainDockingTab.SelectedIndexChanged += new System.EventHandler(this.MainDockingTab_SelectedIndexChanged);
        // 
        // SecurityIdSummaryDockingPage
        // 
        this.SecurityIdSummaryDockingPage.Controls.Add(this.LocatesSecIdGrid);
        this.SecurityIdSummaryDockingPage.Location = new System.Drawing.Point(1, 25);
        this.SecurityIdSummaryDockingPage.Name = "SecurityIdSummaryDockingPage";
        this.SecurityIdSummaryDockingPage.Size = new System.Drawing.Size(199, 484);
        this.SecurityIdSummaryDockingPage.TabIndex = 1;
        this.SecurityIdSummaryDockingPage.Text = "Security ID";
        // 
        // LocatesSecIdGrid
        // 
        this.LocatesSecIdGrid.CaptionHeight = 17;
        this.LocatesSecIdGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        this.LocatesSecIdGrid.EmptyRows = true;
        this.LocatesSecIdGrid.ExtendRightColumn = true;
        this.LocatesSecIdGrid.FilterBar = true;
        this.LocatesSecIdGrid.GroupByCaption = "Drag a column header here to group by that column";
        this.LocatesSecIdGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("LocatesSecIdGrid.Images"))));
        this.LocatesSecIdGrid.Location = new System.Drawing.Point(0, 0);
        this.LocatesSecIdGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
        this.LocatesSecIdGrid.Name = "LocatesSecIdGrid";
        this.LocatesSecIdGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
        this.LocatesSecIdGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
        this.LocatesSecIdGrid.PreviewInfo.ZoomFactor = 75;
        this.LocatesSecIdGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("LocatesSecIdGrid.PrintInfo.PageSettings")));
        this.LocatesSecIdGrid.RecordSelectors = false;
        this.LocatesSecIdGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
        this.LocatesSecIdGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
        this.LocatesSecIdGrid.RowHeight = 15;
        this.LocatesSecIdGrid.Size = new System.Drawing.Size(199, 484);
        this.LocatesSecIdGrid.TabIndex = 1;
        this.LocatesSecIdGrid.Text = "c1TrueDBGrid3";
        this.LocatesSecIdGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.LocatesSecIdGrid_Paint);
        this.LocatesSecIdGrid.PropBag = resources.GetString("LocatesSecIdGrid.PropBag");
        // 
        // GroupCodeSummaryDockingPage
        // 
        this.GroupCodeSummaryDockingPage.Controls.Add(this.LocatesGroupCodeGrid);
        this.GroupCodeSummaryDockingPage.Location = new System.Drawing.Point(1, 25);
        this.GroupCodeSummaryDockingPage.Name = "GroupCodeSummaryDockingPage";
        this.GroupCodeSummaryDockingPage.Size = new System.Drawing.Size(199, 484);
        this.GroupCodeSummaryDockingPage.TabIndex = 0;
        this.GroupCodeSummaryDockingPage.Text = "Group Code";
        // 
        // LocatesGroupCodeGrid
        // 
        this.LocatesGroupCodeGrid.CaptionHeight = 17;
        this.LocatesGroupCodeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        this.LocatesGroupCodeGrid.EmptyRows = true;
        this.LocatesGroupCodeGrid.ExtendRightColumn = true;
        this.LocatesGroupCodeGrid.FilterBar = true;
        this.LocatesGroupCodeGrid.GroupByCaption = "Drag a column header here to group by that column";
        this.LocatesGroupCodeGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("LocatesGroupCodeGrid.Images"))));
        this.LocatesGroupCodeGrid.Location = new System.Drawing.Point(0, 0);
        this.LocatesGroupCodeGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
        this.LocatesGroupCodeGrid.Name = "LocatesGroupCodeGrid";
        this.LocatesGroupCodeGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
        this.LocatesGroupCodeGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
        this.LocatesGroupCodeGrid.PreviewInfo.ZoomFactor = 75;
        this.LocatesGroupCodeGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("LocatesGroupCodeGrid.PrintInfo.PageSettings")));
        this.LocatesGroupCodeGrid.RecordSelectors = false;
        this.LocatesGroupCodeGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
        this.LocatesGroupCodeGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
        this.LocatesGroupCodeGrid.RowHeight = 15;
        this.LocatesGroupCodeGrid.Size = new System.Drawing.Size(199, 484);
        this.LocatesGroupCodeGrid.TabIndex = 0;
        this.LocatesGroupCodeGrid.Text = "c1TrueDBGrid2";
        this.LocatesGroupCodeGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.LocatesGroupCodeGrid_Paint);
        this.LocatesGroupCodeGrid.PropBag = resources.GetString("LocatesGroupCodeGrid.PropBag");
        // 
        // panel2
        // 
        this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(250)))));
        this.panel2.Controls.Add(this.ShowPendingCheckBox);
        this.panel2.Controls.Add(this.DateTimePicker);
        this.panel2.Controls.Add(this.TradeDateLabel);
        this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
        this.panel2.Location = new System.Drawing.Point(201, 0);
        this.panel2.Name = "panel2";
        this.panel2.Size = new System.Drawing.Size(1255, 25);
        this.panel2.TabIndex = 4;
        // 
        // ShowPendingCheckBox
        // 
        this.ShowPendingCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.ShowPendingCheckBox.AutoSize = true;
        this.ShowPendingCheckBox.Location = new System.Drawing.Point(1149, 3);
        this.ShowPendingCheckBox.Name = "ShowPendingCheckBox";
        this.ShowPendingCheckBox.Size = new System.Drawing.Size(106, 17);
        this.ShowPendingCheckBox.TabIndex = 2;
        this.ShowPendingCheckBox.Text = "Show Pending";
        this.ShowPendingCheckBox.UseVisualStyleBackColor = true;
        this.ShowPendingCheckBox.CheckedChanged += new System.EventHandler(this.ShowPendingCheckBox_CheckedChanged);
        // 
        // DateTimePicker
        // 
        this.DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        this.DateTimePicker.Location = new System.Drawing.Point(79, 3);
        this.DateTimePicker.Name = "DateTimePicker";
        this.DateTimePicker.Size = new System.Drawing.Size(94, 21);
        this.DateTimePicker.TabIndex = 1;
        // 
        // TradeDateLabel
        // 
        this.TradeDateLabel.AutoSize = true;
        this.TradeDateLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(251)))));
        this.TradeDateLabel.ForeColor = System.Drawing.Color.Black;
        this.TradeDateLabel.Location = new System.Drawing.Point(6, 7);
        this.TradeDateLabel.Name = "TradeDateLabel";
        this.TradeDateLabel.Size = new System.Drawing.Size(76, 13);
        this.TradeDateLabel.TabIndex = 0;
        this.TradeDateLabel.Tag = null;
        this.TradeDateLabel.Text = "Trade Date:";
        this.TradeDateLabel.TextDetached = true;
        this.TradeDateLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
        // 
        // RefreshTimer
        // 
        this.RefreshTimer.Enabled = true;
        this.RefreshTimer.Interval = 30000;
        this.RefreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
        // 
        // LocatesSummaryForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(195)))), ((int)(((byte)(235)))));
        this.ClientSize = new System.Drawing.Size(1456, 749);
        this.Controls.Add(this.LocatesGrid);
        this.Controls.Add(this.panel2);
        this.Controls.Add(this.MainDockingTab);
        this.Controls.Add(this.panel1);
        this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Name = "LocatesSummaryForm";
        this.Text = "Locates - Summary";
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LocatesSummaryForm_FormClosed);
        this.Load += new System.EventHandler(this.LocatesSummaryForm_Load);
        ((System.ComponentModel.ISupportInitialize)(this.LocatesGrid)).EndInit();
        this.panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.InventoryGrid)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.LocatesSummaryGrid)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.MainDockingTab)).EndInit();
        this.MainDockingTab.ResumeLayout(false);
        this.SecurityIdSummaryDockingPage.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.LocatesSecIdGrid)).EndInit();
        this.GroupCodeSummaryDockingPage.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.LocatesGroupCodeGrid)).EndInit();
        this.panel2.ResumeLayout(false);
        this.panel2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.TradeDateLabel)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private C1.Win.C1TrueDBGrid.C1TrueDBGrid LocatesGrid;
    private System.Windows.Forms.Panel panel1;
    private C1.Win.C1Command.C1CommandHolder MainCommandHolder;
    private C1.Win.C1Command.C1DockingTab MainDockingTab;
    private C1.Win.C1Command.C1DockingTabPage GroupCodeSummaryDockingPage;
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid LocatesGroupCodeGrid;
    private C1.Win.C1Command.C1DockingTabPage SecurityIdSummaryDockingPage;
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid LocatesSecIdGrid;
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid InventoryGrid;
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid LocatesSummaryGrid;
    private System.Windows.Forms.Panel panel2;
    private C1.Win.C1Input.C1Label TradeDateLabel;
    private System.Windows.Forms.DateTimePicker DateTimePicker;
    private C1.Win.C1Command.C1ContextMenu MainContextMenu;
    private C1.Win.C1Command.C1CommandLink c1CommandLink1;
    private C1.Win.C1Command.C1CommandMenu SendToCommand;
    private C1.Win.C1Command.C1CommandLink SendToEmailCommandMenuLink;
    private C1.Win.C1Command.C1Command SendToEmailCommand;
    private C1.Win.C1Command.C1CommandLink SendToExcelCommandMenuLink;
    private C1.Win.C1Command.C1Command SendToExcelCommand;
    private C1.Win.C1Command.C1CommandLink SendToPdfCommandMenuLink;
    private C1.Win.C1Command.C1Command SendToPdfCommand;
    private System.Windows.Forms.Timer RefreshTimer;
      private System.Windows.Forms.CheckBox ShowPendingCheckBox;
  }
}

