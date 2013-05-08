namespace NorthStarClient
{
    partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.MainCommandHolder = new C1.Win.C1Command.C1CommandHolder();
			this.ShortInterestReportingCommand = new C1.Win.C1Command.C1Command();
			this.ReportingCommand = new C1.Win.C1Command.C1CommandMenu();
			this.CashAccountReportingCommandLink = new C1.Win.C1Command.C1CommandLink();
			this.CashAccountReportingCommand = new C1.Win.C1Command.C1Command();
			this.MarginCollateralReportingCommandLink = new C1.Win.C1Command.C1CommandLink();
			this.MarginCollateralReportingCommand = new C1.Win.C1Command.C1Command();
			this.ShortInterestReportingCommandLink = new C1.Win.C1Command.C1CommandLink();
			this.c1MainMenu1 = new C1.Win.C1Command.C1MainMenu();
			this.ReportingCommandLink = new C1.Win.C1Command.C1CommandLink();
			this.SecMaster = new NorthStarClient.SecMaster();
			this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).BeginInit();
			this.SuspendLayout();
			// 
			// MainCommandHolder
			// 
			this.MainCommandHolder.Commands.Add(this.ShortInterestReportingCommand);
			this.MainCommandHolder.Commands.Add(this.ReportingCommand);
			this.MainCommandHolder.Commands.Add(this.MarginCollateralReportingCommand);
			this.MainCommandHolder.Commands.Add(this.CashAccountReportingCommand);
			this.MainCommandHolder.Owner = this;
			// 
			// ShortInterestReportingCommand
			// 
			this.ShortInterestReportingCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("ShortInterestReportingCommand.Icon")));
			this.ShortInterestReportingCommand.Name = "ShortInterestReportingCommand";
			this.ShortInterestReportingCommand.Text = "Short Interest Report";
			this.ShortInterestReportingCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.ShortInterestReportingCommand_Click);
			// 
			// ReportingCommand
			// 
			this.ReportingCommand.BackHiColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(195)))), ((int)(((byte)(235)))));
			this.ReportingCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.CashAccountReportingCommandLink,
            this.MarginCollateralReportingCommandLink,
            this.ShortInterestReportingCommandLink});
			this.ReportingCommand.HideNonRecentLinks = false;
			this.ReportingCommand.Name = "ReportingCommand";
			this.ReportingCommand.Text = "Reporting";
			this.ReportingCommand.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
			this.ReportingCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
			// 
			// CashAccountReportingCommandLink
			// 
			this.CashAccountReportingCommandLink.Command = this.CashAccountReportingCommand;
			// 
			// CashAccountReportingCommand
			// 
			this.CashAccountReportingCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("CashAccountReportingCommand.Icon")));
			this.CashAccountReportingCommand.Name = "CashAccountReportingCommand";
			this.CashAccountReportingCommand.Text = "Cash Account Report";
			this.CashAccountReportingCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.CashAccountReportingCommand_Click);
			// 
			// MarginCollateralReportingCommandLink
			// 
			this.MarginCollateralReportingCommandLink.Command = this.MarginCollateralReportingCommand;
			this.MarginCollateralReportingCommandLink.SortOrder = 1;
			// 
			// MarginCollateralReportingCommand
			// 
			this.MarginCollateralReportingCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("MarginCollateralReportingCommand.Icon")));
			this.MarginCollateralReportingCommand.Name = "MarginCollateralReportingCommand";
			this.MarginCollateralReportingCommand.Text = "Margin Collateral Report";
			this.MarginCollateralReportingCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.MarginCollateralReportingCommand_Click);
			// 
			// ShortInterestReportingCommandLink
			// 
			this.ShortInterestReportingCommandLink.Command = this.ShortInterestReportingCommand;
			this.ShortInterestReportingCommandLink.SortOrder = 2;
			// 
			// c1MainMenu1
			// 
			this.c1MainMenu1.AccessibleName = "Menu Bar";
			this.c1MainMenu1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(195)))), ((int)(((byte)(235)))));
			this.c1MainMenu1.CommandHolder = this.MainCommandHolder;
			this.c1MainMenu1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.ReportingCommandLink});
			this.c1MainMenu1.Dock = System.Windows.Forms.DockStyle.Top;
			this.c1MainMenu1.Location = new System.Drawing.Point(0, 0);
			this.c1MainMenu1.Name = "c1MainMenu1";
			this.c1MainMenu1.Size = new System.Drawing.Size(1542, 24);
			this.c1MainMenu1.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
			this.c1MainMenu1.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue;
			// 
			// ReportingCommandLink
			// 
			this.ReportingCommandLink.Command = this.ReportingCommand;
			// 
			// SecMaster
			// 
			this.SecMaster.Dock = System.Windows.Forms.DockStyle.Top;
			this.SecMaster.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SecMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(128)))), ((int)(((byte)(62)))));
			this.SecMaster.Location = new System.Drawing.Point(0, 24);
			this.SecMaster.Margin = new System.Windows.Forms.Padding(0);
			this.SecMaster.Name = "SecMaster";
			this.SecMaster.Size = new System.Drawing.Size(1542, 90);
			this.SecMaster.TabIndex = 2;
			// 
			// SaveFileDialog
			// 
			this.SaveFileDialog.DefaultExt = "xls";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(1542, 747);
			this.Controls.Add(this.SecMaster);
			this.Controls.Add(this.c1MainMenu1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Name = "MainForm";
			this.Text = "NorthStar";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Command.C1CommandHolder MainCommandHolder;
      private C1.Win.C1Command.C1Command ShortInterestReportingCommand;
      private C1.Win.C1Command.C1CommandMenu ReportingCommand;
      private C1.Win.C1Command.C1CommandLink ShortInterestReportingCommandLink;
      private C1.Win.C1Command.C1MainMenu c1MainMenu1;
      private C1.Win.C1Command.C1CommandLink ReportingCommandLink;
      private SecMaster SecMaster;
			private C1.Win.C1Command.C1CommandLink MarginCollateralReportingCommandLink;
			private C1.Win.C1Command.C1Command MarginCollateralReportingCommand;
		private System.Windows.Forms.SaveFileDialog SaveFileDialog;
		private C1.Win.C1Command.C1CommandLink CashAccountReportingCommandLink;
		private C1.Win.C1Command.C1Command CashAccountReportingCommand;

      }
}