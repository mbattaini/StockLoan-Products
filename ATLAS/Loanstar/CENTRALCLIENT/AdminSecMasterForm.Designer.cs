namespace CentralClient
{
	partial class AdminSecMasterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminSecMasterForm));
            this.SecMasterGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.SecMasterDetailsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
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
            this.UploadCommand = new C1.Win.C1Command.C1CommandMenu();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.UploadPricingCommand = new C1.Win.C1Command.C1Command();
            this.CommandHolder = new C1.Win.C1Command.C1CommandHolder();
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.ribbonSeparator1 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.StatusMessageLabel = new C1.Win.C1Ribbon.RibbonLabel();
            this.LoadRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            this.SecMasterRibbonProgressBar = new C1.Win.C1Ribbon.RibbonProgressBar();
            this.SecMasterLoadBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.SecMasterGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecMasterDetailsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // SecMasterGrid
            // 
            this.SecMasterGrid.AllowAddNew = true;
            this.SecMasterGrid.AlternatingRows = true;
            this.CommandHolder.SetC1ContextMenu(this.SecMasterGrid, this.ContextMenu);
            this.SecMasterGrid.CaptionHeight = 17;
            this.SecMasterGrid.ChildGrid = this.SecMasterDetailsGrid;
            this.SecMasterGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SecMasterGrid.EmptyRows = true;
            this.SecMasterGrid.ExtendRightColumn = true;
            this.SecMasterGrid.FilterBar = true;
            this.SecMasterGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.SecMasterGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("SecMasterGrid.Images"))));
            this.SecMasterGrid.Location = new System.Drawing.Point(0, 0);
            this.SecMasterGrid.Name = "SecMasterGrid";
            this.SecMasterGrid.Padding = new System.Windows.Forms.Padding(3);
            this.SecMasterGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.SecMasterGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.SecMasterGrid.PreviewInfo.ZoomFactor = 75D;
            this.SecMasterGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("SecMasterGrid.PrintInfo.PageSettings")));
            this.SecMasterGrid.RecordSelectors = false;
            this.SecMasterGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.SecMasterGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.SecMasterGrid.RowHeight = 15;
            this.SecMasterGrid.Size = new System.Drawing.Size(934, 534);
            this.SecMasterGrid.TabIndex = 0;
            this.SecMasterGrid.Text = "Security Master";
            this.SecMasterGrid.BeforeOpen += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.SecMasterGrid_BeforeOpen);
            this.SecMasterGrid.AfterFilter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.SecMasterGrid_AfterFilter);
            this.SecMasterGrid.PropBag = resources.GetString("SecMasterGrid.PropBag");
            // 
            // SecMasterDetailsGrid
            // 
            this.SecMasterDetailsGrid.AllowAddNew = true;
            this.SecMasterDetailsGrid.AlternatingRows = true;
            this.SecMasterDetailsGrid.CaptionHeight = 17;
            this.SecMasterDetailsGrid.EmptyRows = true;
            this.SecMasterDetailsGrid.ExtendRightColumn = true;
            this.SecMasterDetailsGrid.FilterBar = true;
            this.SecMasterDetailsGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.SecMasterDetailsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("SecMasterDetailsGrid.Images"))));
            this.SecMasterDetailsGrid.Location = new System.Drawing.Point(12, 54);
            this.SecMasterDetailsGrid.Name = "SecMasterDetailsGrid";
            this.SecMasterDetailsGrid.Padding = new System.Windows.Forms.Padding(3);
            this.SecMasterDetailsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.SecMasterDetailsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.SecMasterDetailsGrid.PreviewInfo.ZoomFactor = 75D;
            this.SecMasterDetailsGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("SecMasterDetailsGrid.PrintInfo.PageSettings")));
            this.SecMasterDetailsGrid.RecordSelectors = false;
            this.SecMasterDetailsGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.SecMasterDetailsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.SecMasterDetailsGrid.RowHeight = 15;
            this.SecMasterDetailsGrid.Size = new System.Drawing.Size(656, 252);
            this.SecMasterDetailsGrid.TabIndex = 1;
            this.SecMasterDetailsGrid.TabStop = false;
            this.SecMasterDetailsGrid.Text = "c1TrueDBGrid1";
            this.SecMasterDetailsGrid.PropBag = resources.GetString("SecMasterDetailsGrid.PropBag");
            // 
            // ContextMenu
            // 
            this.ContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SendToCommandLink,
            this.c1CommandLink1});
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
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.UploadCommand;
            this.c1CommandLink1.SortOrder = 1;
            // 
            // UploadCommand
            // 
            this.UploadCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink2});
            this.UploadCommand.Name = "UploadCommand";
            this.UploadCommand.Text = "Upload";
            this.UploadCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Silver;
            // 
            // c1CommandLink2
            // 
            this.c1CommandLink2.Command = this.UploadPricingCommand;
            // 
            // UploadPricingCommand
            // 
            this.UploadPricingCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("UploadPricingCommand.Icon")));
            this.UploadPricingCommand.Name = "UploadPricingCommand";
            this.UploadPricingCommand.Text = "Pricing";
            this.UploadPricingCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.UploadPricingCommand_Click);
            // 
            // CommandHolder
            // 
            this.CommandHolder.Commands.Add(this.ContextMenu);
            this.CommandHolder.Commands.Add(this.SendToCommand);
            this.CommandHolder.Commands.Add(this.SendToClipboardCommand);
            this.CommandHolder.Commands.Add(this.SendToExcelCommand);
            this.CommandHolder.Commands.Add(this.SendToEmailCommand);
            this.CommandHolder.Commands.Add(this.UploadCommand);
            this.CommandHolder.Commands.Add(this.UploadPricingCommand);
            this.CommandHolder.Owner = this;
            this.CommandHolder.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.LeftPaneItems.Add(this.ribbonSeparator1);
            this.c1StatusBar1.LeftPaneItems.Add(this.StatusMessageLabel);
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 534);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.LoadRibbonButton);
            this.c1StatusBar1.RightPaneItems.Add(this.SecMasterRibbonProgressBar);
            this.c1StatusBar1.Size = new System.Drawing.Size(934, 23);
            // 
            // ribbonSeparator1
            // 
            this.ribbonSeparator1.Name = "ribbonSeparator1";
            // 
            // StatusMessageLabel
            // 
            this.StatusMessageLabel.Name = "StatusMessageLabel";
            // 
            // LoadRibbonButton
            // 
            this.LoadRibbonButton.Name = "LoadRibbonButton";
            this.LoadRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("LoadRibbonButton.SmallImage")));
            this.LoadRibbonButton.Text = "Load";
            this.LoadRibbonButton.Click += new System.EventHandler(this.LoadRibbonButton_Click);
            // 
            // SecMasterRibbonProgressBar
            // 
            this.SecMasterRibbonProgressBar.Name = "SecMasterRibbonProgressBar";
            // 
            // SecMasterLoadBackgroundWorker
            // 
            this.SecMasterLoadBackgroundWorker.WorkerReportsProgress = true;
            this.SecMasterLoadBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SecMasterLoadBackgroundWorker_DoWork);
            this.SecMasterLoadBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.SecMasterLoadBackgroundWorker_ProgressChanged);
            this.SecMasterLoadBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SecMasterLoadBackgroundWorker_RunWorkerCompleted);
            // 
            // AdminSecMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(934, 557);
            this.Controls.Add(this.SecMasterDetailsGrid);
            this.Controls.Add(this.SecMasterGrid);
            this.Controls.Add(this.c1StatusBar1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminSecMasterForm";
            this.Text = "Admin - Security Master";
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Silver;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminSecMasterForm_FormClosed);
            this.Load += new System.EventHandler(this.AdminSecMasterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SecMasterGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecMasterDetailsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid SecMasterGrid;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid SecMasterDetailsGrid;
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
        private C1.Win.C1Command.C1CommandLink c1CommandLink1;
        private C1.Win.C1Command.C1CommandMenu UploadCommand;
        private C1.Win.C1Command.C1CommandLink c1CommandLink2;
        private C1.Win.C1Command.C1Command UploadPricingCommand;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Ribbon.RibbonProgressBar SecMasterRibbonProgressBar;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator1;
        private C1.Win.C1Ribbon.RibbonLabel StatusMessageLabel;
        private System.ComponentModel.BackgroundWorker SecMasterLoadBackgroundWorker;
        private C1.Win.C1Ribbon.RibbonButton LoadRibbonButton;
	}
}