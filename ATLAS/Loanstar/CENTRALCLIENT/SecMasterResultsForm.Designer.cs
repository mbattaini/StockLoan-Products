namespace CentralClient
{
  partial class SecMasterResultsForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecMasterResultsForm));
        this.ResultsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
        this.ContextMenu = new C1.Win.C1Command.C1ContextMenu();
        this.SendToCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToCommand = new C1.Win.C1Command.C1CommandMenu();
        this.SendToClipboardCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToClipboardCommand = new C1.Win.C1Command.C1Command();
        this.SendToExcelCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToExcelCommand = new C1.Win.C1Command.C1Command();
        this.SendToEmailCommandLink = new C1.Win.C1Command.C1CommandLink();
        this.SendToEmailCommand = new C1.Win.C1Command.C1Command();
        this.CommandHolder = new C1.Win.C1Command.C1CommandHolder();
        this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
        ((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
        this.SuspendLayout();
        // 
        // ResultsGrid
        // 
        this.ResultsGrid.AlternatingRows = true;
        this.CommandHolder.SetC1ContextMenu(this.ResultsGrid, this.ContextMenu);
        this.ResultsGrid.CaptionHeight = 17;
        this.ResultsGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
        this.ResultsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        this.ResultsGrid.EmptyRows = true;
        this.ResultsGrid.ExtendRightColumn = true;
        this.ResultsGrid.GroupByCaption = "Drag a column header here to group by that column";
        this.ResultsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ResultsGrid.Images"))));
        this.ResultsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ResultsGrid.Images1"))));
        this.ResultsGrid.Location = new System.Drawing.Point(0, 0);
        this.ResultsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
        this.ResultsGrid.Name = "ResultsGrid";
        this.ResultsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
        this.ResultsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
        this.ResultsGrid.PreviewInfo.ZoomFactor = 75D;
        this.ResultsGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("ResultsGrid.PrintInfo.PageSettings")));
        this.ResultsGrid.RecordSelectors = false;
        this.ResultsGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
        this.ResultsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
        this.ResultsGrid.RowHeight = 15;
        this.ResultsGrid.Size = new System.Drawing.Size(741, 163);
        this.ResultsGrid.TabIndex = 4;
        this.ResultsGrid.Text = "Security Master";
        this.ResultsGrid.UseColumnStyles = false;
        this.ResultsGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
        this.ResultsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ResultsGrid_FormatText);
        this.ResultsGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.ResultsGrid_Paint);
        this.ResultsGrid.PropBag = resources.GetString("ResultsGrid.PropBag");
        // 
        // ContextMenu
        // 
        this.ContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SendToCommandLink});
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
        // CommandHolder
        // 
        this.CommandHolder.Commands.Add(this.ContextMenu);
        this.CommandHolder.Commands.Add(this.SendToCommand);
        this.CommandHolder.Commands.Add(this.SendToClipboardCommand);
        this.CommandHolder.Commands.Add(this.SendToExcelCommand);
        this.CommandHolder.Commands.Add(this.SendToEmailCommand);
        this.CommandHolder.Owner = this;
        this.CommandHolder.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
        // 
        // c1StatusBar1
        // 
        this.c1StatusBar1.Name = "c1StatusBar1";
        // 
        // SecMasterResultsForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(741, 186);
        this.Controls.Add(this.ResultsGrid);
        this.Controls.Add(this.c1StatusBar1);
        this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Name = "SecMasterResultsForm";
        this.Text = "Security Master - Search Results";
        this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Windows7;
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SecMasterResultsForm_FormClosed);
        this.Load += new System.EventHandler(this.SecMasterResultsForm_Load);
        ((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private C1.Win.C1TrueDBGrid.C1TrueDBGrid ResultsGrid;
      private C1.Win.C1Command.C1CommandHolder CommandHolder;
      private C1.Win.C1Command.C1ContextMenu ContextMenu;
      private C1.Win.C1Command.C1CommandLink SendToCommandLink;
      private C1.Win.C1Command.C1CommandMenu SendToCommand;
      private C1.Win.C1Command.C1CommandLink SendToClipboardCommandLink;
      private C1.Win.C1Command.C1Command SendToClipboardCommand;
      private C1.Win.C1Command.C1CommandLink SendToExcelCommandLink;
      private C1.Win.C1Command.C1Command SendToExcelCommand;
      private C1.Win.C1Command.C1CommandLink SendToEmailCommandLink;
      private C1.Win.C1Command.C1Command SendToEmailCommand;
      private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
  }
}