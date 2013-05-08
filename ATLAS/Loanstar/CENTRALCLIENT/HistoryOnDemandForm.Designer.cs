namespace CentralClient
{
    partial class HistoryOnDemandForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryOnDemandForm));
            this.MarksGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.HistoryDockingTab = new C1.Win.C1Command.C1DockingTab();
            this.MarksDockingTabPage = new C1.Win.C1Command.C1DockingTabPage();
            this.ReturnsDockingTabPage = new C1.Win.C1Command.C1DockingTabPage();
            this.ReturnsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.ContractsDockingTabPage = new C1.Win.C1Command.C1DockingTabPage();
            this.ContractsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.CashDockingTabPage = new C1.Win.C1Command.C1DockingTabPage();
            this.CashGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.MainContextMenu = new C1.Win.C1Command.C1ContextMenu();
            this.SendToCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.SendToCommand = new C1.Win.C1Command.C1CommandMenu();
            this.SendToExcelCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.SendToExcelCommand = new C1.Win.C1Command.C1Command();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.SendToEmailCommand = new C1.Win.C1Command.C1Command();
            this.HistoryOnDemandCommand = new C1.Win.C1Command.C1Command();
            this.ApplyCommand = new C1.Win.C1Command.C1CommandMenu();
            this.ApplyMarksCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.ApplyMarksCommand = new C1.Win.C1Command.C1Command();
            this.SendToClipboardCommand = new C1.Win.C1Command.C1Command();
            this.ContractToCommand = new C1.Win.C1Command.C1CommandMenu();
            this.ContractToReturnCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.ContractToReturnCommand = new C1.Win.C1Command.C1Command();
            this.ContractToRecallCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.ContractToRecallCommand = new C1.Win.C1Command.C1Command();
            this.RecallToCommand = new C1.Win.C1Command.C1CommandMenu();
            this.RecallToBuyInCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.RecallToBuyInCommand = new C1.Win.C1Command.C1Command();
            this.RecallToMoveCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.RecallToMoveCommand = new C1.Win.C1Command.C1Command();
            this.RecallToCloseCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.RecallToCloseCommand = new C1.Win.C1Command.C1Command();
            this.ShowCommand = new C1.Win.C1Command.C1CommandMenu();
            this.ShowBorrowsCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.ShowBorrowsCommand = new C1.Win.C1Command.C1Command();
            this.ShowLoansCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.ShowLoansCommand = new C1.Win.C1Command.C1Command();
            this.ShowTotalsCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.ShowTotalsCommand = new C1.Win.C1Command.C1Command();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.ShowSummaryCommand = new C1.Win.C1Command.C1Command();
            ((System.ComponentModel.ISupportInitialize)(this.MarksGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryDockingTab)).BeginInit();
            this.HistoryDockingTab.SuspendLayout();
            this.MarksDockingTabPage.SuspendLayout();
            this.ReturnsDockingTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReturnsGrid)).BeginInit();
            this.ContractsDockingTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContractsGrid)).BeginInit();
            this.CashDockingTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CashGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            this.SuspendLayout();
            // 
            // MarksGrid
            // 
            this.MarksGrid.AllowDelete = true;
            this.MarksGrid.AlternatingRows = true;
            this.MarksGrid.CaptionHeight = 17;
            this.MarksGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.MarksGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MarksGrid.EmptyRows = true;
            this.MarksGrid.ExtendRightColumn = true;
            this.MarksGrid.FetchRowStyles = true;
            this.MarksGrid.FilterBar = true;
            this.MarksGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.MarksGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("MarksGrid.Images"))));
            this.MarksGrid.Location = new System.Drawing.Point(0, 0);
            this.MarksGrid.Name = "MarksGrid";
            this.MarksGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.MarksGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.MarksGrid.PreviewInfo.ZoomFactor = 75D;
            this.MarksGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("MarksGrid.PrintInfo.PageSettings")));
            this.MarksGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.MarksGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.MarksGrid.RowHeight = 15;
            this.MarksGrid.Size = new System.Drawing.Size(1449, 315);
            this.MarksGrid.TabIndex = 0;
            this.MarksGrid.Text = "c1TrueDBGrid1";
            this.MarksGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.Grid_FormatText);
            this.MarksGrid.FetchCellStyle += new C1.Win.C1TrueDBGrid.FetchCellStyleEventHandler(this.Grid_FetchCellStyle);
            this.MarksGrid.PropBag = resources.GetString("MarksGrid.PropBag");
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.Name = "c1StatusBar1";
            // 
            // HistoryDockingTab
            // 
            this.HistoryDockingTab.Controls.Add(this.MarksDockingTabPage);
            this.HistoryDockingTab.Controls.Add(this.ReturnsDockingTabPage);
            this.HistoryDockingTab.Controls.Add(this.ContractsDockingTabPage);
            this.HistoryDockingTab.Controls.Add(this.CashDockingTabPage);
            this.HistoryDockingTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HistoryDockingTab.Location = new System.Drawing.Point(0, 0);
            this.HistoryDockingTab.Name = "HistoryDockingTab";
            this.HistoryDockingTab.SelectedIndex = 3;
            this.HistoryDockingTab.ShowSingleTab = false;
            this.HistoryDockingTab.Size = new System.Drawing.Size(1451, 340);
            this.HistoryDockingTab.TabIndex = 3;
            this.HistoryDockingTab.TabsSpacing = 5;
            this.HistoryDockingTab.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // MarksDockingTabPage
            // 
            this.MarksDockingTabPage.Controls.Add(this.MarksGrid);
            this.MarksDockingTabPage.Location = new System.Drawing.Point(1, 25);
            this.MarksDockingTabPage.Name = "MarksDockingTabPage";
            this.MarksDockingTabPage.Size = new System.Drawing.Size(1449, 314);
            this.MarksDockingTabPage.TabIndex = 0;
            this.MarksDockingTabPage.Text = "Marks";
            // 
            // ReturnsDockingTabPage
            // 
            this.ReturnsDockingTabPage.Controls.Add(this.ReturnsGrid);
            this.ReturnsDockingTabPage.Location = new System.Drawing.Point(1, 25);
            this.ReturnsDockingTabPage.Name = "ReturnsDockingTabPage";
            this.ReturnsDockingTabPage.Size = new System.Drawing.Size(1449, 314);
            this.ReturnsDockingTabPage.TabIndex = 1;
            this.ReturnsDockingTabPage.Text = "Returns";
            // 
            // ReturnsGrid
            // 
            this.ReturnsGrid.AllowDelete = true;
            this.ReturnsGrid.AlternatingRows = true;
            this.ReturnsGrid.CaptionHeight = 17;
            this.ReturnsGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.ReturnsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReturnsGrid.EmptyRows = true;
            this.ReturnsGrid.ExtendRightColumn = true;
            this.ReturnsGrid.FetchRowStyles = true;
            this.ReturnsGrid.FilterBar = true;
            this.ReturnsGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.ReturnsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ReturnsGrid.Images"))));
            this.ReturnsGrid.Location = new System.Drawing.Point(0, 0);
            this.ReturnsGrid.Name = "ReturnsGrid";
            this.ReturnsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.ReturnsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.ReturnsGrid.PreviewInfo.ZoomFactor = 75D;
            this.ReturnsGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("ReturnsGrid.PrintInfo.PageSettings")));
            this.ReturnsGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.ReturnsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.ReturnsGrid.RowHeight = 15;
            this.ReturnsGrid.Size = new System.Drawing.Size(1449, 315);
            this.ReturnsGrid.TabIndex = 1;
            this.ReturnsGrid.Text = "c1TrueDBGrid1";
            this.ReturnsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.Grid_FormatText);
            this.ReturnsGrid.FetchCellStyle += new C1.Win.C1TrueDBGrid.FetchCellStyleEventHandler(this.Grid_FetchCellStyle);
            this.ReturnsGrid.PropBag = resources.GetString("ReturnsGrid.PropBag");
            // 
            // ContractsDockingTabPage
            // 
            this.ContractsDockingTabPage.Controls.Add(this.ContractsGrid);
            this.ContractsDockingTabPage.Location = new System.Drawing.Point(1, 25);
            this.ContractsDockingTabPage.Name = "ContractsDockingTabPage";
            this.ContractsDockingTabPage.Size = new System.Drawing.Size(1449, 314);
            this.ContractsDockingTabPage.TabIndex = 2;
            this.ContractsDockingTabPage.Text = "Contracts";
            // 
            // ContractsGrid
            // 
            this.ContractsGrid.AllowColSelect = false;
            this.ContractsGrid.AllowDelete = true;
            this.ContractsGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.ContractsGrid.CaptionHeight = 17;
            this.ContractsGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.ContractsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContractsGrid.EmptyRows = true;
            this.ContractsGrid.ExtendRightColumn = true;
            this.ContractsGrid.FetchRowStyles = true;
            this.ContractsGrid.FilterBar = true;
            this.ContractsGrid.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.Flat;
            this.ContractsGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.ContractsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ContractsGrid.Images"))));
            this.ContractsGrid.Location = new System.Drawing.Point(0, 0);
            this.ContractsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
            this.ContractsGrid.Name = "ContractsGrid";
            this.ContractsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.ContractsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.ContractsGrid.PreviewInfo.ZoomFactor = 75D;
            this.ContractsGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("ContractsGrid.PrintInfo.PageSettings")));
            this.ContractsGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.ContractsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.ContractsGrid.RowHeight = 15;
            this.ContractsGrid.Size = new System.Drawing.Size(1449, 315);
            this.ContractsGrid.TabIndex = 2;
            this.ContractsGrid.Text = "c1TrueDBGrid1";
            this.ContractsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.Grid_FormatText);
            this.ContractsGrid.FetchCellStyle += new C1.Win.C1TrueDBGrid.FetchCellStyleEventHandler(this.Grid_FetchCellStyle);
            this.ContractsGrid.PropBag = resources.GetString("ContractsGrid.PropBag");
            // 
            // CashDockingTabPage
            // 
            this.CashDockingTabPage.Controls.Add(this.CashGrid);
            this.CashDockingTabPage.Location = new System.Drawing.Point(1, 25);
            this.CashDockingTabPage.Name = "CashDockingTabPage";
            this.CashDockingTabPage.Size = new System.Drawing.Size(1449, 314);
            this.CashDockingTabPage.TabIndex = 3;
            this.CashDockingTabPage.Text = "Cash";
            // 
            // CashGrid
            // 
            this.CashGrid.AllowColSelect = false;
            this.CashGrid.AllowDelete = true;
            this.CashGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.CashGrid.CaptionHeight = 17;
            this.CashGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.CashGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CashGrid.EmptyRows = true;
            this.CashGrid.ExtendRightColumn = true;
            this.CashGrid.FetchRowStyles = true;
            this.CashGrid.FilterBar = true;
            this.CashGrid.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.Flat;
            this.CashGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.CashGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("CashGrid.Images"))));
            this.CashGrid.Location = new System.Drawing.Point(0, 0);
            this.CashGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
            this.CashGrid.Name = "CashGrid";
            this.CashGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.CashGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.CashGrid.PreviewInfo.ZoomFactor = 75D;
            this.CashGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.CashGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.CashGrid.RowHeight = 15;
            this.CashGrid.Size = new System.Drawing.Size(1449, 315);
            this.CashGrid.TabIndex = 3;
            this.CashGrid.Text = "c1TrueDBGrid1";
            this.CashGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.CashGrid_BeforeUpdate);
            this.CashGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.Grid_FormatText);
            this.CashGrid.PropBag = resources.GetString("CashGrid.PropBag");
            // 
            // MainContextMenu
            // 
            this.MainContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SendToCommandLink});
            this.MainContextMenu.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainContextMenu.Name = "MainContextMenu";
            this.MainContextMenu.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
            this.MainContextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Silver;
            // 
            // SendToCommandLink
            // 
            this.SendToCommandLink.Command = this.SendToCommand;
            // 
            // SendToCommand
            // 
            this.SendToCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SendToExcelCommandLink});
            this.SendToCommand.Name = "SendToCommand";
            this.SendToCommand.Text = "Send To";
            this.SendToCommand.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
            this.SendToCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Silver;
            // 
            // SendToExcelCommandLink
            // 
            this.SendToExcelCommandLink.Command = this.SendToExcelCommand;
            // 
            // SendToExcelCommand
            // 
            this.SendToExcelCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToExcelCommand.Icon")));
            this.SendToExcelCommand.Name = "SendToExcelCommand";
            this.SendToExcelCommand.Text = "Excel";
            this.SendToExcelCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SendToExcelCommand_Click);
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Commands.Add(this.MainContextMenu);
            this.c1CommandHolder1.Commands.Add(this.SendToCommand);
            this.c1CommandHolder1.Commands.Add(this.SendToExcelCommand);
            this.c1CommandHolder1.Commands.Add(this.SendToEmailCommand);
            this.c1CommandHolder1.Commands.Add(this.HistoryOnDemandCommand);
            this.c1CommandHolder1.Commands.Add(this.ApplyCommand);
            this.c1CommandHolder1.Commands.Add(this.ApplyMarksCommand);
            this.c1CommandHolder1.Commands.Add(this.SendToClipboardCommand);
            this.c1CommandHolder1.Commands.Add(this.ContractToCommand);
            this.c1CommandHolder1.Commands.Add(this.ContractToReturnCommand);
            this.c1CommandHolder1.Commands.Add(this.ContractToRecallCommand);
            this.c1CommandHolder1.Commands.Add(this.RecallToCommand);
            this.c1CommandHolder1.Commands.Add(this.RecallToBuyInCommand);
            this.c1CommandHolder1.Commands.Add(this.RecallToMoveCommand);
            this.c1CommandHolder1.Commands.Add(this.RecallToCloseCommand);
            this.c1CommandHolder1.Commands.Add(this.ShowCommand);
            this.c1CommandHolder1.Commands.Add(this.ShowBorrowsCommand);
            this.c1CommandHolder1.Commands.Add(this.ShowLoansCommand);
            this.c1CommandHolder1.Commands.Add(this.ShowTotalsCommand);
            this.c1CommandHolder1.Commands.Add(this.ShowSummaryCommand);
            this.c1CommandHolder1.Owner = this;
            // 
            // SendToEmailCommand
            // 
            this.SendToEmailCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToEmailCommand.Icon")));
            this.SendToEmailCommand.Name = "SendToEmailCommand";
            this.SendToEmailCommand.Text = "Mail";
            // 
            // HistoryOnDemandCommand
            // 
            this.HistoryOnDemandCommand.Name = "HistoryOnDemandCommand";
            this.HistoryOnDemandCommand.Text = "History on Demand";
            // 
            // ApplyCommand
            // 
            this.ApplyCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.ApplyMarksCommandLink});
            this.ApplyCommand.Name = "ApplyCommand";
            this.ApplyCommand.Text = "Apply";
            this.ApplyCommand.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
            this.ApplyCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Silver;
            // 
            // ApplyMarksCommandLink
            // 
            this.ApplyMarksCommandLink.Command = this.ApplyMarksCommand;
            // 
            // ApplyMarksCommand
            // 
            this.ApplyMarksCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("ApplyMarksCommand.Icon")));
            this.ApplyMarksCommand.Name = "ApplyMarksCommand";
            this.ApplyMarksCommand.Text = "Marks";
            // 
            // SendToClipboardCommand
            // 
            this.SendToClipboardCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("SendToClipboardCommand.Icon")));
            this.SendToClipboardCommand.Name = "SendToClipboardCommand";
            this.SendToClipboardCommand.Text = "Clipboard";
            // 
            // ContractToCommand
            // 
            this.ContractToCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.ContractToReturnCommandLink,
            this.ContractToRecallCommandLink});
            this.ContractToCommand.Name = "ContractToCommand";
            this.ContractToCommand.Text = "Contract To";
            this.ContractToCommand.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
            this.ContractToCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Silver;
            // 
            // ContractToReturnCommandLink
            // 
            this.ContractToReturnCommandLink.Command = this.ContractToReturnCommand;
            // 
            // ContractToReturnCommand
            // 
            this.ContractToReturnCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("ContractToReturnCommand.Icon")));
            this.ContractToReturnCommand.Name = "ContractToReturnCommand";
            this.ContractToReturnCommand.Text = "Return";
            // 
            // ContractToRecallCommandLink
            // 
            this.ContractToRecallCommandLink.Command = this.ContractToRecallCommand;
            this.ContractToRecallCommandLink.SortOrder = 1;
            // 
            // ContractToRecallCommand
            // 
            this.ContractToRecallCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("ContractToRecallCommand.Icon")));
            this.ContractToRecallCommand.Name = "ContractToRecallCommand";
            this.ContractToRecallCommand.Text = "Recall";
            // 
            // RecallToCommand
            // 
            this.RecallToCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.RecallToBuyInCommandLink,
            this.RecallToMoveCommandLink,
            this.RecallToCloseCommandLink});
            this.RecallToCommand.Name = "RecallToCommand";
            this.RecallToCommand.Text = "Recall To";
            this.RecallToCommand.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
            this.RecallToCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Silver;
            // 
            // RecallToBuyInCommandLink
            // 
            this.RecallToBuyInCommandLink.Command = this.RecallToBuyInCommand;
            // 
            // RecallToBuyInCommand
            // 
            this.RecallToBuyInCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("RecallToBuyInCommand.Icon")));
            this.RecallToBuyInCommand.Name = "RecallToBuyInCommand";
            this.RecallToBuyInCommand.Text = "BuyIn";
            // 
            // RecallToMoveCommandLink
            // 
            this.RecallToMoveCommandLink.Command = this.RecallToMoveCommand;
            this.RecallToMoveCommandLink.SortOrder = 1;
            // 
            // RecallToMoveCommand
            // 
            this.RecallToMoveCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("RecallToMoveCommand.Icon")));
            this.RecallToMoveCommand.Name = "RecallToMoveCommand";
            this.RecallToMoveCommand.Text = "Move";
            // 
            // RecallToCloseCommandLink
            // 
            this.RecallToCloseCommandLink.Command = this.RecallToCloseCommand;
            this.RecallToCloseCommandLink.SortOrder = 2;
            // 
            // RecallToCloseCommand
            // 
            this.RecallToCloseCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("RecallToCloseCommand.Icon")));
            this.RecallToCloseCommand.Name = "RecallToCloseCommand";
            this.RecallToCloseCommand.Text = "Close";
            // 
            // ShowCommand
            // 
            this.ShowCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.ShowBorrowsCommandLink,
            this.ShowLoansCommandLink,
            this.ShowTotalsCommandLink,
            this.c1CommandLink2});
            this.ShowCommand.Name = "ShowCommand";
            this.ShowCommand.Text = "Show";
            this.ShowCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // ShowBorrowsCommandLink
            // 
            this.ShowBorrowsCommandLink.Command = this.ShowBorrowsCommand;
            // 
            // ShowBorrowsCommand
            // 
            this.ShowBorrowsCommand.CheckAutoToggle = true;
            this.ShowBorrowsCommand.Checked = true;
            this.ShowBorrowsCommand.Name = "ShowBorrowsCommand";
            this.ShowBorrowsCommand.Text = "Borrows";
            // 
            // ShowLoansCommandLink
            // 
            this.ShowLoansCommandLink.Command = this.ShowLoansCommand;
            this.ShowLoansCommandLink.SortOrder = 1;
            // 
            // ShowLoansCommand
            // 
            this.ShowLoansCommand.CheckAutoToggle = true;
            this.ShowLoansCommand.Checked = true;
            this.ShowLoansCommand.Name = "ShowLoansCommand";
            this.ShowLoansCommand.Text = "Loans";
            // 
            // ShowTotalsCommandLink
            // 
            this.ShowTotalsCommandLink.Command = this.ShowTotalsCommand;
            this.ShowTotalsCommandLink.SortOrder = 2;
            // 
            // ShowTotalsCommand
            // 
            this.ShowTotalsCommand.CheckAutoToggle = true;
            this.ShowTotalsCommand.Checked = true;
            this.ShowTotalsCommand.Name = "ShowTotalsCommand";
            this.ShowTotalsCommand.Text = "Totals";
            // 
            // c1CommandLink2
            // 
            this.c1CommandLink2.Command = this.ShowSummaryCommand;
            this.c1CommandLink2.SortOrder = 3;
            // 
            // ShowSummaryCommand
            // 
            this.ShowSummaryCommand.CheckAutoToggle = true;
            this.ShowSummaryCommand.Checked = true;
            this.ShowSummaryCommand.Name = "ShowSummaryCommand";
            this.ShowSummaryCommand.Text = "Summary";
            // 
            // HistoryOnDemandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1451, 363);
            this.Controls.Add(this.HistoryDockingTab);
            this.Controls.Add(this.c1StatusBar1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HistoryOnDemandForm";
            this.Text = "History";
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Windows7;
            this.Load += new System.EventHandler(this.HistoryToMarksForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MarksGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryDockingTab)).EndInit();
            this.HistoryDockingTab.ResumeLayout(false);
            this.MarksDockingTabPage.ResumeLayout(false);
            this.ReturnsDockingTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReturnsGrid)).EndInit();
            this.ContractsDockingTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ContractsGrid)).EndInit();
            this.CashDockingTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CashGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid MarksGrid;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Command.C1DockingTab HistoryDockingTab;
        private C1.Win.C1Command.C1DockingTabPage MarksDockingTabPage;
        private C1.Win.C1Command.C1DockingTabPage ReturnsDockingTabPage;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid ReturnsGrid;
        private C1.Win.C1Command.C1DockingTabPage ContractsDockingTabPage;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid ContractsGrid;
        private C1.Win.C1Command.C1ContextMenu MainContextMenu;
        private C1.Win.C1Command.C1CommandLink SendToCommandLink;
        private C1.Win.C1Command.C1CommandMenu SendToCommand;
        private C1.Win.C1Command.C1CommandLink SendToExcelCommandLink;
        private C1.Win.C1Command.C1Command SendToExcelCommand;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        private C1.Win.C1Command.C1Command SendToEmailCommand;
        private C1.Win.C1Command.C1Command HistoryOnDemandCommand;
        private C1.Win.C1Command.C1CommandMenu ApplyCommand;
        private C1.Win.C1Command.C1CommandLink ApplyMarksCommandLink;
        private C1.Win.C1Command.C1Command ApplyMarksCommand;
        private C1.Win.C1Command.C1Command SendToClipboardCommand;
        private C1.Win.C1Command.C1CommandMenu ContractToCommand;
        private C1.Win.C1Command.C1CommandLink ContractToReturnCommandLink;
        private C1.Win.C1Command.C1Command ContractToReturnCommand;
        private C1.Win.C1Command.C1CommandLink ContractToRecallCommandLink;
        private C1.Win.C1Command.C1Command ContractToRecallCommand;
        private C1.Win.C1Command.C1CommandMenu RecallToCommand;
        private C1.Win.C1Command.C1CommandLink RecallToBuyInCommandLink;
        private C1.Win.C1Command.C1Command RecallToBuyInCommand;
        private C1.Win.C1Command.C1CommandLink RecallToMoveCommandLink;
        private C1.Win.C1Command.C1Command RecallToMoveCommand;
        private C1.Win.C1Command.C1CommandLink RecallToCloseCommandLink;
        private C1.Win.C1Command.C1Command RecallToCloseCommand;
        private C1.Win.C1Command.C1CommandMenu ShowCommand;
        private C1.Win.C1Command.C1CommandLink ShowBorrowsCommandLink;
        private C1.Win.C1Command.C1Command ShowBorrowsCommand;
        private C1.Win.C1Command.C1CommandLink ShowLoansCommandLink;
        private C1.Win.C1Command.C1Command ShowLoansCommand;
        private C1.Win.C1Command.C1CommandLink ShowTotalsCommandLink;
        private C1.Win.C1Command.C1Command ShowTotalsCommand;
        private C1.Win.C1Command.C1CommandLink c1CommandLink2;
        private C1.Win.C1Command.C1Command ShowSummaryCommand;
        private C1.Win.C1Command.C1DockingTabPage CashDockingTabPage;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid CashGrid;
    }
}