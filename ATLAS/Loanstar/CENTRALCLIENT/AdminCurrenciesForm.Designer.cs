namespace CentralClient
{
	partial class AdminCurrenciesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminCurrenciesForm));
            this.CurrenciesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
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
            this.StatusMessageLabel = new C1.Win.C1Ribbon.RibbonLabel();
            this.CurrenciesRibbonProgressBar = new C1.Win.C1Ribbon.RibbonProgressBar();
            this.CurrenciesBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.CurrenciesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // CurrenciesGrid
            // 
            this.CurrenciesGrid.AlternatingRows = true;
            this.CommandHolder.SetC1ContextMenu(this.CurrenciesGrid, this.ContextMenu);
            this.CurrenciesGrid.CaptionHeight = 17;
            this.CurrenciesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrenciesGrid.EmptyRows = true;
            this.CurrenciesGrid.ExtendRightColumn = true;
            this.CurrenciesGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.CurrenciesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("CurrenciesGrid.Images"))));
            this.CurrenciesGrid.Location = new System.Drawing.Point(0, 0);
            this.CurrenciesGrid.Name = "CurrenciesGrid";
            this.CurrenciesGrid.Padding = new System.Windows.Forms.Padding(3);
            this.CurrenciesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.CurrenciesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.CurrenciesGrid.PreviewInfo.ZoomFactor = 75D;
            this.CurrenciesGrid.RecordSelectors = false;
            this.CurrenciesGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.CurrenciesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.CurrenciesGrid.RowHeight = 15;
            this.CurrenciesGrid.Size = new System.Drawing.Size(341, 249);
            this.CurrenciesGrid.TabIndex = 0;
            this.CurrenciesGrid.Text = "Currencies";
            this.CurrenciesGrid.PropBag = resources.GetString("CurrenciesGrid.PropBag");
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
            this.SendToClipboardCommandLink.Text = "Clipboard";
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
            this.c1StatusBar1.LeftPaneItems.Add(this.StatusMessageLabel);
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 249);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.CurrenciesRibbonProgressBar);
            this.c1StatusBar1.Size = new System.Drawing.Size(341, 23);
            // 
            // StatusMessageLabel
            // 
            this.StatusMessageLabel.Name = "StatusMessageLabel";
            this.StatusMessageLabel.Text = "Label";
            // 
            // CurrenciesRibbonProgressBar
            // 
            this.CurrenciesRibbonProgressBar.Name = "CurrenciesRibbonProgressBar";
            // 
            // CurrenciesBackgroundWorker
            // 
            this.CurrenciesBackgroundWorker.WorkerReportsProgress = true;
            this.CurrenciesBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CurrenciesBackgroundWorker_DoWork);
            this.CurrenciesBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.CurrenciesBackgroundWorker_ProgressChanged);
            this.CurrenciesBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CurrenciesBackgroundWorker_RunWorkerCompleted);
            // 
            // AdminCurrenciesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 272);
            this.Controls.Add(this.CurrenciesGrid);
            this.Controls.Add(this.c1StatusBar1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminCurrenciesForm";
            this.Text = "Admin - Currencies";
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Windows7;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminCurrenciesForm_FormClosed);
            this.Load += new System.EventHandler(this.AdminCurrenciesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CurrenciesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid CurrenciesGrid;
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
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Ribbon.RibbonLabel StatusMessageLabel;
        private C1.Win.C1Ribbon.RibbonProgressBar CurrenciesRibbonProgressBar;
        private System.ComponentModel.BackgroundWorker CurrenciesBackgroundWorker;
	}
}