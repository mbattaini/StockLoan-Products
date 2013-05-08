namespace CentralClient
{
  partial class TradingFundingRatesForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradingFundingRatesForm));
        C1.Win.C1TrueDBGrid.Style style1 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style2 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style3 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style4 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style5 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style6 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style7 = new C1.Win.C1TrueDBGrid.Style();
        C1.Win.C1TrueDBGrid.Style style8 = new C1.Win.C1TrueDBGrid.Style();
        this.ContextMenu = new C1.Win.C1Command.C1ContextMenu();
        this.SendToCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToCommand = new C1.Win.C1Command.C1CommandMenu();
        this.SendToClipboardCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToClipboardCommand = new C1.Win.C1Command.C1Command();
        this.SendToExcelCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToExcelCommand = new C1.Win.C1Command.C1Command();
        this.SendToEmailCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToEmailCommand = new C1.Win.C1Command.C1Command();
        this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
        this.HistoryFundingRateResearchCommand = new C1.Win.C1Command.C1Command();
        this.CommandHolder = new C1.Win.C1Command.C1CommandHolder();
        this.FundingGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
        this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
        this.FundDropdown = new C1.Win.C1TrueDBGrid.C1TrueDBDropdown();
        ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.FundingGrid)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.FundDropdown)).BeginInit();
        this.SuspendLayout();
        // 
        // ContextMenu
        // 
        this.ContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SendToCommandLink,
            this.c1CommandLink1});
        this.ContextMenu.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.ContextMenu.Name = "ContextMenu";
        this.ContextMenu.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
        this.ContextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Silver;
        // 
        // SendToCommandLink
        // 
        this.SendToCommandLink.Command = this.SendToCommand;
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
        // c1CommandLink1
        // 
        this.c1CommandLink1.Command = this.HistoryFundingRateResearchCommand;
        this.c1CommandLink1.SortOrder = 1;
        // 
        // HistoryFundingRateResearchCommand
        // 
        this.HistoryFundingRateResearchCommand.Name = "HistoryFundingRateResearchCommand";
        this.HistoryFundingRateResearchCommand.Text = "History On Demand";
        this.HistoryFundingRateResearchCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.HistoryFundingRateResearchCommand_Click);
        // 
        // CommandHolder
        // 
        this.CommandHolder.Commands.Add(this.ContextMenu);
        this.CommandHolder.Commands.Add(this.SendToCommand);
        this.CommandHolder.Commands.Add(this.SendToExcelCommand);
        this.CommandHolder.Commands.Add(this.SendToClipboardCommand);
        this.CommandHolder.Commands.Add(this.SendToEmailCommand);
        this.CommandHolder.Commands.Add(this.HistoryFundingRateResearchCommand);
        this.CommandHolder.Owner = this;
        this.CommandHolder.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
        // 
        // FundingGrid
        // 
        this.FundingGrid.AllowAddNew = true;
        this.FundingGrid.AlternatingRows = true;
        this.CommandHolder.SetC1ContextMenu(this.FundingGrid, this.ContextMenu);
        this.FundingGrid.CaptionHeight = 17;
        this.FundingGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
        this.FundingGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        this.FundingGrid.EmptyRows = true;
        this.FundingGrid.ExtendRightColumn = true;
        this.FundingGrid.GroupByCaption = "Drag a column header here to group by that column";
        this.FundingGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("FundingGrid.Images"))));
        this.FundingGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("FundingGrid.Images1"))));
        this.FundingGrid.Location = new System.Drawing.Point(0, 0);
        this.FundingGrid.Name = "FundingGrid";
        this.FundingGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
        this.FundingGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
        this.FundingGrid.PreviewInfo.ZoomFactor = 75D;
        this.FundingGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("FundingGrid.PrintInfo.PageSettings")));
        this.FundingGrid.RecordSelectors = false;
        this.FundingGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
        this.FundingGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
        this.FundingGrid.RowHeight = 15;
        this.FundingGrid.Size = new System.Drawing.Size(823, 523);
        this.FundingGrid.TabIndex = 3;
        this.FundingGrid.Text = "Funding Rates";
        this.FundingGrid.UseColumnStyles = false;
        this.FundingGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
        this.FundingGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.FundingGrid_BeforeUpdate);
        this.FundingGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FundingGrid_FormatText);
        this.FundingGrid.OnAddNew += new System.EventHandler(this.FundingGrid_OnAddNew);
        this.FundingGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FundingGrid_KeyPress);
        this.FundingGrid.PropBag = resources.GetString("FundingGrid.PropBag");
        // 
        // c1StatusBar1
        // 
        this.c1StatusBar1.Name = "c1StatusBar1";
        // 
        // FundDropdown
        // 
        this.FundDropdown.AllowColMove = true;
        this.FundDropdown.AllowColSelect = true;
        this.FundDropdown.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows;
        this.FundDropdown.AlternatingRows = false;
        this.FundDropdown.CaptionHeight = 17;
        this.FundDropdown.CaptionStyle = style1;
        this.FundDropdown.ColumnCaptionHeight = 17;
        this.FundDropdown.ColumnFooterHeight = 17;
        this.FundDropdown.DropdownWidth = C1.Win.C1TrueDBGrid.DropdownWidthEnum.Column;
        this.FundDropdown.EmptyRows = true;
        this.FundDropdown.EvenRowStyle = style2;
        this.FundDropdown.FetchRowStyles = false;
        this.FundDropdown.FooterStyle = style3;
        this.FundDropdown.HeadingStyle = style4;
        this.FundDropdown.HighLightRowStyle = style5;
        this.FundDropdown.Images.Add(((System.Drawing.Image)(resources.GetObject("FundDropdown.Images"))));
        this.FundDropdown.Location = new System.Drawing.Point(361, 198);
        this.FundDropdown.Name = "FundDropdown";
        this.FundDropdown.OddRowStyle = style6;
        this.FundDropdown.RecordSelectorStyle = style7;
        this.FundDropdown.RowDivider.Color = System.Drawing.Color.LightGray;
        this.FundDropdown.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
        this.FundDropdown.RowHeight = 15;
        this.FundDropdown.RowSubDividerColor = System.Drawing.Color.DarkGray;
        this.FundDropdown.ScrollTips = false;
        this.FundDropdown.Size = new System.Drawing.Size(100, 150);
        this.FundDropdown.Style = style8;
        this.FundDropdown.TabIndex = 115;
        this.FundDropdown.TabStop = false;
        this.FundDropdown.Text = "c1TrueDBDropdown1";
        this.FundDropdown.Visible = false;
        this.FundDropdown.PropBag = resources.GetString("FundDropdown.PropBag");
        // 
        // TradingFundingRatesForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(823, 546);
        this.Controls.Add(this.FundDropdown);
        this.Controls.Add(this.FundingGrid);
        this.Controls.Add(this.c1StatusBar1);
        this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Name = "TradingFundingRatesForm";
        this.Text = "Trading - Funding Rates";
        this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Windows7;
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TradingFundingRatesForm_FormClosed);
        this.Load += new System.EventHandler(this.TradingFundingRatesForm_Load);
        ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.FundingGrid)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.FundDropdown)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private C1.Win.C1Command.C1CommandHolder CommandHolder;
	  private C1.Win.C1Command.C1ContextMenu ContextMenu;
	  private C1.Win.C1Command.C1CommandLink SendToCommandLink;
	  private C1.Win.C1Command.C1CommandMenu SendToCommand;
	  private C1.Win.C1Command.C1CommandLink SendToExcelCommandLink;
      private C1.Win.C1Command.C1Command SendToExcelCommand;
      private C1.Win.C1Command.C1CommandLink SendToClipboardCommandLink;
      private C1.Win.C1Command.C1Command SendToClipboardCommand;
      private C1.Win.C1Command.C1CommandLink SendToEmailCommandLink;
      private C1.Win.C1Command.C1Command SendToEmailCommand;
      private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
      private C1.Win.C1TrueDBGrid.C1TrueDBGrid FundingGrid;
      private C1.Win.C1Command.C1CommandLink c1CommandLink1;
      private C1.Win.C1Command.C1Command HistoryFundingRateResearchCommand;
      private C1.Win.C1TrueDBGrid.C1TrueDBDropdown FundDropdown;

  }
}