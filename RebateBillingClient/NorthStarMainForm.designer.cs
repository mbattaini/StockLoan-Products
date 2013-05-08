namespace NorthStar
{
    partial class NorthStarMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NorthStarMainForm));
            this.MainMenu = new C1.Win.C1Command.C1MainMenu();
            this.ShortMenu = new C1.Win.C1Command.C1CommandLink();
            this.ShortCommand = new C1.Win.C1Command.C1CommandMenu();
            this.ShortShortInterestMenuItem = new C1.Win.C1Command.C1CommandLink();
            this.ShortInterestCommand = new C1.Win.C1Command.C1CommandMenu();
            this.ShortsShortInterestReportMenuItem = new C1.Win.C1Command.C1CommandLink();
            this.ShortInterestReportCommand = new C1.Win.C1Command.C1Command();
            this.MainToolBar = new C1.Win.C1Command.C1ToolBar();
            this.ShortInterestReportCommandLink = new C1.Win.C1Command.C1CommandLink();
            this.CommandHolder = new C1.Win.C1Command.C1CommandHolder();
            ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.AccessibleName = "Menu Bar";
            this.MainMenu.CommandHolder = null;
            this.MainMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.ShortMenu});
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1542, 24);
            this.MainMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue;
            // 
            // ShortMenu
            // 
            this.ShortMenu.Command = this.ShortCommand;
            this.ShortMenu.Text = "Shorts";
            // 
            // ShortCommand
            // 
            this.ShortCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.ShortShortInterestMenuItem});
            this.ShortCommand.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShortCommand.HideNonRecentLinks = false;
            this.ShortCommand.Name = "ShortCommand";
            this.ShortCommand.Text = "Shorts";
            this.ShortCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // ShortShortInterestMenuItem
            // 
            this.ShortShortInterestMenuItem.Command = this.ShortInterestCommand;
            // 
            // ShortInterestCommand
            // 
            this.ShortInterestCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.ShortsShortInterestReportMenuItem});
            this.ShortInterestCommand.HideNonRecentLinks = false;
            this.ShortInterestCommand.Name = "ShortInterestCommand";
            this.ShortInterestCommand.Text = "Short Interest";
            this.ShortInterestCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // ShortsShortInterestReportMenuItem
            // 
            this.ShortsShortInterestReportMenuItem.Command = this.ShortInterestReportCommand;
            // 
            // ShortInterestReportCommand
            // 
            this.ShortInterestReportCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("ShortInterestReportCommand.Icon")));
            this.ShortInterestReportCommand.Name = "ShortInterestReportCommand";
            this.ShortInterestReportCommand.Text = "Short Interest Report";
            this.ShortInterestReportCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.ShortsShortInterestReportCommand_Click);
            // 
            // MainToolBar
            // 
            this.MainToolBar.AccessibleName = "Tool Bar";
            this.MainToolBar.AutoSize = false;
            this.MainToolBar.CommandHolder = null;
            this.MainToolBar.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.ShortInterestReportCommandLink});
            this.MainToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainToolBar.Location = new System.Drawing.Point(0, 24);
            this.MainToolBar.Movable = false;
            this.MainToolBar.Name = "MainToolBar";
            this.MainToolBar.Size = new System.Drawing.Size(1542, 26);
            this.MainToolBar.Text = "MainToolBar";
            this.MainToolBar.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue;
            // 
            // ShortInterestReportCommandLink
            // 
            this.ShortInterestReportCommandLink.Command = this.ShortInterestReportCommand;
            this.ShortInterestReportCommandLink.ToolTipText = "Short Interest Report";
            // 
            // CommandHolder
            // 
            this.CommandHolder.Commands.Add(this.ShortCommand);
            this.CommandHolder.Commands.Add(this.ShortInterestCommand);
            this.CommandHolder.Commands.Add(this.ShortInterestReportCommand);
            this.CommandHolder.Owner = this;
            // 
            // NorthStarMainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1542, 636);
            this.Controls.Add(this.MainToolBar);
            this.Controls.Add(this.MainMenu);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.Name = "NorthStarMainForm";
            this.Text = "NorthStar";
            this.Load += new System.EventHandler(this.NorthStarMainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Command.C1MainMenu MainMenu;
        private C1.Win.C1Command.C1ToolBar MainToolBar;
        private C1.Win.C1Command.C1CommandLink ShortInterestReportCommandLink;
        private C1.Win.C1Command.C1CommandLink ShortMenu;
        private C1.Win.C1Command.C1CommandMenu ShortCommand;
        private C1.Win.C1Command.C1CommandLink ShortShortInterestMenuItem;
        private C1.Win.C1Command.C1CommandMenu ShortInterestCommand;
        private C1.Win.C1Command.C1CommandLink ShortsShortInterestReportMenuItem;
        private C1.Win.C1Command.C1Command ShortInterestReportCommand;
        private C1.Win.C1Command.C1CommandHolder CommandHolder;
    }
}