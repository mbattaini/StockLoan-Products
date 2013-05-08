namespace CentralClient
{
	partial class AdminCountriesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminCountriesForm));
            this.CountriesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
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
            this.CountryCodeStatusBar = new C1.Win.C1Ribbon.C1StatusBar();
            this.StatusMessageLabel = new C1.Win.C1Ribbon.RibbonLabel();
            this.CountryCodeRibbonProgressBar = new C1.Win.C1Ribbon.RibbonProgressBar();
            this.CountryCodeBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.CountriesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountryCodeStatusBar)).BeginInit();
            this.SuspendLayout();
            // 
            // CountriesGrid
            // 
            this.CountriesGrid.AlternatingRows = true;
            this.CommandHolder.SetC1ContextMenu(this.CountriesGrid, this.ContextMenu);
            this.CountriesGrid.CaptionHeight = 17;
            this.CountriesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.CountriesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CountriesGrid.EmptyRows = true;
            this.CountriesGrid.ExtendRightColumn = true;
            this.CountriesGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.CountriesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("CountriesGrid.Images"))));
            this.CountriesGrid.Location = new System.Drawing.Point(0, 0);
            this.CountriesGrid.Name = "CountriesGrid";
            this.CountriesGrid.Padding = new System.Windows.Forms.Padding(3);
            this.CountriesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.CountriesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.CountriesGrid.PreviewInfo.ZoomFactor = 75D;
            this.CountriesGrid.RecordSelectors = false;
            this.CountriesGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.CountriesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.CountriesGrid.RowHeight = 15;
            this.CountriesGrid.Size = new System.Drawing.Size(341, 249);
            this.CountriesGrid.TabIndex = 1;
            this.CountriesGrid.Text = "Countries";
            this.CountriesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.CountriesGrid_BeforeUpdate);
            this.CountriesGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CountriesGrid_KeyPress);
            this.CountriesGrid.PropBag = resources.GetString("CountriesGrid.PropBag");
            // 
            // ContextMenu
            // 
            this.ContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SendToCommandLink});
            this.ContextMenu.Name = "ContextMenu";
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
            // CountryCodeStatusBar
            // 
            this.CountryCodeStatusBar.LeftPaneItems.Add(this.StatusMessageLabel);
            this.CountryCodeStatusBar.Location = new System.Drawing.Point(0, 249);
            this.CountryCodeStatusBar.Name = "CountryCodeStatusBar";
            this.CountryCodeStatusBar.RightPaneItems.Add(this.CountryCodeRibbonProgressBar);
            this.CountryCodeStatusBar.Size = new System.Drawing.Size(341, 23);
            // 
            // StatusMessageLabel
            // 
            this.StatusMessageLabel.Name = "StatusMessageLabel";
            this.StatusMessageLabel.Text = "Label";
            // 
            // CountryCodeRibbonProgressBar
            // 
            this.CountryCodeRibbonProgressBar.Name = "CountryCodeRibbonProgressBar";
            // 
            // CountryCodeBackgroundWorker
            // 
            this.CountryCodeBackgroundWorker.WorkerReportsProgress = true;
            this.CountryCodeBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CountryCodeBackgroundWorker_DoWork);
            this.CountryCodeBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.CountryCodeBackgroundWorker_ProgressChanged);
            this.CountryCodeBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CountryCodeBackgroundWorker_RunWorkerCompleted);
            // 
            // AdminCountriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 272);
            this.Controls.Add(this.CountriesGrid);
            this.Controls.Add(this.CountryCodeStatusBar);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminCountriesForm";
            this.Text = "Admin - Countries";
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Windows7;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminCountriesForm_FormClosed);
            this.Load += new System.EventHandler(this.AdminCountriesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CountriesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountryCodeStatusBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid CountriesGrid;
        private C1.Win.C1Command.C1ContextMenu ContextMenu;
        private C1.Win.C1Command.C1CommandHolder CommandHolder;
        private C1.Win.C1Command.C1CommandLink SendToCommandLink;
        private C1.Win.C1Command.C1CommandMenu SendToCommand;
        private C1.Win.C1Command.C1CommandLink SendToClipboardCommandLink;
        private C1.Win.C1Command.C1Command SendToClipboardCommand;
        private C1.Win.C1Command.C1CommandLink SendToExcelCommandLink;
        private C1.Win.C1Command.C1Command SendToExcelCommand;
        private C1.Win.C1Command.C1CommandLink SendToEmailCommandLink;
        private C1.Win.C1Command.C1Command SendToEmailCommand;
        private C1.Win.C1Ribbon.C1StatusBar CountryCodeStatusBar;
        private C1.Win.C1Ribbon.RibbonLabel StatusMessageLabel;
        private C1.Win.C1Ribbon.RibbonProgressBar CountryCodeRibbonProgressBar;
        private System.ComponentModel.BackgroundWorker CountryCodeBackgroundWorker;
	}
}