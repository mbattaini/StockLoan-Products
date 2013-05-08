namespace LocatesClient
{
	partial class LocatesResearchSummaryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocatesResearchSummaryForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.RefreshButton = new C1.Win.C1Input.C1Button();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.TradeDateLabel = new C1.Win.C1Input.C1Label();
            this.LocatesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.DockingTab = new C1.Win.C1Command.C1DockingTab();
            this.SecurityDockingTab = new C1.Win.C1Command.C1DockingTabPage();
            this.LocatesSummaryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.GroupCodeDockingTab = new C1.Win.C1Command.C1DockingTabPage();
            this.GroupCodeSummaryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.GroupCodeContextMenu = new C1.Win.C1Command.C1ContextMenu();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.SendToCommand = new C1.Win.C1Command.C1CommandMenu();
            this.SendToLocatesExcelCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.SendToLocatesExcelCommand = new C1.Win.C1Command.C1Command();
            this.SendToClipboardCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.SendToClipboardCommand = new C1.Win.C1Command.C1Command();
            this.MainCommandHolder = new C1.Win.C1Command.C1CommandHolder();
            this.LocatesContextMenu = new C1.Win.C1Command.C1ContextMenu();
            this.SendToLocatesCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TradeDateLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocatesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DockingTab)).BeginInit();
            this.DockingTab.SuspendLayout();
            this.SecurityDockingTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocatesSummaryGrid)).BeginInit();
            this.GroupCodeDockingTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GroupCodeSummaryGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.RefreshButton);
            this.panel2.Controls.Add(this.DateTimePicker);
            this.panel2.Controls.Add(this.TradeDateLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1407, 40);
            this.panel2.TabIndex = 7;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(1329, 8);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 2;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTimePicker.Location = new System.Drawing.Point(92, 9);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(109, 21);
            this.DateTimePicker.TabIndex = 1;
            this.DateTimePicker.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
            // 
            // TradeDateLabel
            // 
            this.TradeDateLabel.AutoSize = true;
            this.TradeDateLabel.BackColor = System.Drawing.Color.Transparent;
            this.TradeDateLabel.ForeColor = System.Drawing.Color.Black;
            this.TradeDateLabel.Location = new System.Drawing.Point(7, 13);
            this.TradeDateLabel.Name = "TradeDateLabel";
            this.TradeDateLabel.Size = new System.Drawing.Size(76, 13);
            this.TradeDateLabel.TabIndex = 0;
            this.TradeDateLabel.Tag = null;
            this.TradeDateLabel.Text = "Trade Date:";
            this.TradeDateLabel.TextDetached = true;
            this.TradeDateLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // LocatesGrid
            // 
            this.LocatesGrid.AllowColSelect = false;
            this.LocatesGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.LocatesGrid.AlternatingRows = true;
            this.MainCommandHolder.SetC1ContextMenu(this.LocatesGrid, this.GroupCodeContextMenu);
            this.LocatesGrid.CaptionHeight = 17;
            this.LocatesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.LocatesGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LocatesGrid.EmptyRows = true;
            this.LocatesGrid.ExtendRightColumn = true;
            this.LocatesGrid.FetchRowStyles = true;
            this.LocatesGrid.FilterBar = true;
            this.LocatesGrid.GroupByAreaVisible = false;
            this.LocatesGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.LocatesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("LocatesGrid.Images"))));
            this.LocatesGrid.Location = new System.Drawing.Point(0, 474);
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
            this.LocatesGrid.Size = new System.Drawing.Size(1407, 309);
            this.LocatesGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
            this.LocatesGrid.TabIndex = 5;
            this.LocatesGrid.Text = "5";
            this.LocatesGrid.UseColumnStyles = false;
            this.LocatesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.LocatesGrid_FormatText);
            this.LocatesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.LocatesGrid_BeforeUpdate);
            this.LocatesGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LocatesGrid_KeyPress);
            this.LocatesGrid.PropBag = resources.GetString("LocatesGrid.PropBag");
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 471);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1407, 3);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // DockingTab
            // 
            this.DockingTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DockingTab.Controls.Add(this.SecurityDockingTab);
            this.DockingTab.Controls.Add(this.GroupCodeDockingTab);
            this.DockingTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockingTab.Location = new System.Drawing.Point(0, 40);
            this.DockingTab.Name = "DockingTab";
            this.DockingTab.SelectedIndex = 1;
            this.DockingTab.SelectedTabBold = true;
            this.DockingTab.Size = new System.Drawing.Size(1407, 431);
            this.DockingTab.TabIndex = 10;
            this.DockingTab.TabsSpacing = 5;
            this.DockingTab.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007;
            this.DockingTab.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Blue;
            this.DockingTab.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue;
            this.DockingTab.SelectedIndexChanged += new System.EventHandler(this.DockingTab_SelectedIndexChanged);
            this.DockingTab.TabIndexChanged += new System.EventHandler(this.DockingTab_SelectedIndexChanged);
            // 
            // SecurityDockingTab
            // 
            this.SecurityDockingTab.Controls.Add(this.LocatesSummaryGrid);
            this.SecurityDockingTab.Location = new System.Drawing.Point(1, 25);
            this.SecurityDockingTab.Name = "SecurityDockingTab";
            this.SecurityDockingTab.Size = new System.Drawing.Size(1405, 405);
            this.SecurityDockingTab.TabIndex = 0;
            this.SecurityDockingTab.Text = "Security";
            // 
            // LocatesSummaryGrid
            // 
            this.MainCommandHolder.SetC1ContextMenu(this.LocatesSummaryGrid, this.GroupCodeContextMenu);
            this.LocatesSummaryGrid.CaptionHeight = 17;
            this.LocatesSummaryGrid.ColumnFooters = true;
            this.LocatesSummaryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LocatesSummaryGrid.EmptyRows = true;
            this.LocatesSummaryGrid.ExtendRightColumn = true;
            this.LocatesSummaryGrid.FilterBar = true;
            this.LocatesSummaryGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.LocatesSummaryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("LocatesSummaryGrid.Images"))));
            this.LocatesSummaryGrid.Location = new System.Drawing.Point(0, 0);
            this.LocatesSummaryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
            this.LocatesSummaryGrid.Name = "LocatesSummaryGrid";
            this.LocatesSummaryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.LocatesSummaryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.LocatesSummaryGrid.PreviewInfo.ZoomFactor = 75;
            this.LocatesSummaryGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("LocatesSummaryGrid.PrintInfo.PageSettings")));
            this.LocatesSummaryGrid.RecordSelectors = false;
            this.LocatesSummaryGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.LocatesSummaryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.LocatesSummaryGrid.RowHeight = 15;
            this.LocatesSummaryGrid.Size = new System.Drawing.Size(1405, 405);
            this.LocatesSummaryGrid.TabIndex = 9;
            this.LocatesSummaryGrid.Text = "c1TrueDBGrid1";
            this.LocatesSummaryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.LocatesSummaryGrid_FormatText);
            this.LocatesSummaryGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.LocatesSummaryGrid_Paint);
            this.LocatesSummaryGrid.PropBag = resources.GetString("LocatesSummaryGrid.PropBag");
            // 
            // GroupCodeDockingTab
            // 
            this.GroupCodeDockingTab.Controls.Add(this.GroupCodeSummaryGrid);
            this.GroupCodeDockingTab.Location = new System.Drawing.Point(1, 25);
            this.GroupCodeDockingTab.Name = "GroupCodeDockingTab";
            this.GroupCodeDockingTab.Size = new System.Drawing.Size(1405, 405);
            this.GroupCodeDockingTab.TabIndex = 1;
            this.GroupCodeDockingTab.Text = "Group Code";
            // 
            // GroupCodeSummaryGrid
            // 
            this.MainCommandHolder.SetC1ContextMenu(this.GroupCodeSummaryGrid, this.GroupCodeContextMenu);
            this.GroupCodeSummaryGrid.CaptionHeight = 17;
            this.GroupCodeSummaryGrid.ColumnFooters = true;
            this.GroupCodeSummaryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupCodeSummaryGrid.EmptyRows = true;
            this.GroupCodeSummaryGrid.ExtendRightColumn = true;
            this.GroupCodeSummaryGrid.FilterBar = true;
            this.GroupCodeSummaryGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.GroupCodeSummaryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("GroupCodeSummaryGrid.Images"))));
            this.GroupCodeSummaryGrid.Location = new System.Drawing.Point(0, 0);
            this.GroupCodeSummaryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
            this.GroupCodeSummaryGrid.Name = "GroupCodeSummaryGrid";
            this.GroupCodeSummaryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.GroupCodeSummaryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.GroupCodeSummaryGrid.PreviewInfo.ZoomFactor = 75;
            this.GroupCodeSummaryGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("GroupCodeSummaryGrid.PrintInfo.PageSettings")));
            this.GroupCodeSummaryGrid.RecordSelectors = false;
            this.GroupCodeSummaryGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.GroupCodeSummaryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.GroupCodeSummaryGrid.RowHeight = 15;
            this.GroupCodeSummaryGrid.Size = new System.Drawing.Size(1405, 405);
            this.GroupCodeSummaryGrid.TabIndex = 0;
            this.GroupCodeSummaryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.GroupCodeSummaryGrid_FormatText);
            this.GroupCodeSummaryGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.GroupCodeSummaryGrid_Paint);
            this.GroupCodeSummaryGrid.PropBag = resources.GetString("GroupCodeSummaryGrid.PropBag");
            // 
            // GroupCodeContextMenu
            // 
            this.GroupCodeContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1});
            this.GroupCodeContextMenu.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupCodeContextMenu.Name = "GroupCodeContextMenu";
            this.GroupCodeContextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.SendToCommand;
            // 
            // SendToCommand
            // 
            this.SendToCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SendToLocatesExcelCommandLink,
            this.SendToClipboardCommandLink});
            this.SendToCommand.Name = "SendToCommand";
            this.SendToCommand.Text = "Send To";
            this.SendToCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // SendToLocatesExcelCommandLink
            // 
            this.SendToLocatesExcelCommandLink.Command = this.SendToLocatesExcelCommand;
            // 
            // SendToLocatesExcelCommand
            // 
            this.SendToLocatesExcelCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToLocatesExcelCommand.Icon")));
            this.SendToLocatesExcelCommand.Name = "SendToLocatesExcelCommand";
            this.SendToLocatesExcelCommand.Text = "Excel";
            this.SendToLocatesExcelCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SendToLocatesExcelCommand_Click);
            // 
            // SendToClipboardCommandLink
            // 
            this.SendToClipboardCommandLink.Command = this.SendToClipboardCommand;
            this.SendToClipboardCommandLink.SortOrder = 1;
            // 
            // SendToClipboardCommand
            // 
            this.SendToClipboardCommand.Enabled = false;
            this.SendToClipboardCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToClipboardCommand.Icon")));
            this.SendToClipboardCommand.Name = "SendToClipboardCommand";
            this.SendToClipboardCommand.Text = "Clipboard";
            // 
            // MainCommandHolder
            // 
            this.MainCommandHolder.Commands.Add(this.GroupCodeContextMenu);
            this.MainCommandHolder.Commands.Add(this.SendToCommand);
            this.MainCommandHolder.Commands.Add(this.SendToLocatesExcelCommand);
            this.MainCommandHolder.Commands.Add(this.SendToClipboardCommand);
            this.MainCommandHolder.Commands.Add(this.LocatesContextMenu);
            this.MainCommandHolder.Owner = this;
            // 
            // LocatesContextMenu
            // 
            this.LocatesContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SendToLocatesCommandLink});
            this.LocatesContextMenu.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocatesContextMenu.Name = "LocatesContextMenu";
            this.LocatesContextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // SendToLocatesCommandLink
            // 
            this.SendToLocatesCommandLink.Command = this.SendToCommand;
            // 
            // LocatesResearchSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 783);
            this.Controls.Add(this.DockingTab);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.LocatesGrid);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LocatesResearchSummaryForm";
            this.Text = "Locates - Research Summary";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LocatesResearchSummaryForm_FormClosed);
            this.Load += new System.EventHandler(this.LocatesResearchSummaryForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TradeDateLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LocatesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DockingTab)).EndInit();
            this.DockingTab.ResumeLayout(false);
            this.SecurityDockingTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LocatesSummaryGrid)).EndInit();
            this.GroupCodeDockingTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GroupCodeSummaryGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid LocatesGrid;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.DateTimePicker DateTimePicker;
		private C1.Win.C1Input.C1Label TradeDateLabel;
		private System.Windows.Forms.Splitter splitter1;
		private C1.Win.C1Input.C1Button RefreshButton;
		private C1.Win.C1Command.C1DockingTab DockingTab;
		private C1.Win.C1Command.C1DockingTabPage SecurityDockingTab;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid LocatesSummaryGrid;
		private C1.Win.C1Command.C1DockingTabPage GroupCodeDockingTab;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid GroupCodeSummaryGrid;
		private C1.Win.C1Command.C1CommandHolder MainCommandHolder;
		private C1.Win.C1Command.C1ContextMenu GroupCodeContextMenu;
		private C1.Win.C1Command.C1CommandLink c1CommandLink1;
		private C1.Win.C1Command.C1CommandMenu SendToCommand;
		private C1.Win.C1Command.C1CommandLink SendToLocatesExcelCommandLink;
		private C1.Win.C1Command.C1Command SendToLocatesExcelCommand;
		private C1.Win.C1Command.C1CommandLink SendToClipboardCommandLink;
		private C1.Win.C1Command.C1Command SendToClipboardCommand;
		private System.Windows.Forms.SaveFileDialog SaveFileDialog;
		private C1.Win.C1Command.C1ContextMenu LocatesContextMenu;
		private C1.Win.C1Command.C1CommandLink SendToLocatesCommandLink;
	}
}