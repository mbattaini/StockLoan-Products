namespace LocatesClient
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
		this.MainShellAppBar = new LogicNP.ShellObjects.ShellAppBar(this.components);
		this.MainToolbar = new C1.Win.C1Command.C1ToolBar();
		this.MainCommandHolder = new C1.Win.C1Command.C1CommandHolder();
		this.LocatesInputCommand = new C1.Win.C1Command.C1Command();
		this.LocatesSummaryCommand = new C1.Win.C1Command.C1Command();
		this.LocatesResearchCommand = new C1.Win.C1Command.C1Command();
		this.LocatesTradingGroupCommand = new C1.Win.C1Command.C1Command();
		this.LocatesExportToolCommand = new C1.Win.C1Command.C1Command();
		this.LocatesInputToolbarLink = new C1.Win.C1Command.C1CommandLink();
		this.LocatesSummaryCommandToolbarLink = new C1.Win.C1Command.C1CommandLink();
		this.LocatesResearchCommandToolbarLink = new C1.Win.C1Command.C1CommandLink();
		this.LocatesTradingGroupCommandToolbarLink = new C1.Win.C1Command.C1CommandLink();
		this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
		this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
		this.CloseButton = new C1.Win.C1Input.C1Button();
		this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
		this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
		this.LocatesInventoryLookupCommandLink = new C1.Win.C1Command.C1CommandLink();
		this.LocatesInventoryLookupCommand = new C1.Win.C1Command.C1Command();
		((System.ComponentModel.ISupportInitialize)(this.MainShellAppBar)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).BeginInit();
		this.SuspendLayout();
		// 
		// MainShellAppBar
		// 
		this.MainShellAppBar.DockingEdge = LogicNP.ShellObjects.DockingEdges.Top;
		this.MainShellAppBar.HostForm = this;
		// 
		// MainToolbar
		// 
		this.MainToolbar.AccessibleName = "Tool Bar";
		this.MainToolbar.AutoSize = false;
		this.MainToolbar.BackColor = System.Drawing.Color.AliceBlue;
		this.MainToolbar.CommandHolder = this.MainCommandHolder;
		this.MainToolbar.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.LocatesInputToolbarLink,
            this.LocatesSummaryCommandToolbarLink,
            this.LocatesInventoryLookupCommandLink,
            this.LocatesResearchCommandToolbarLink,
            this.LocatesTradingGroupCommandToolbarLink,
            this.c1CommandLink2});
		this.MainToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
		this.MainToolbar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.MainToolbar.Location = new System.Drawing.Point(0, 0);
		this.MainToolbar.Movable = false;
		this.MainToolbar.Name = "MainToolbar";
		this.MainToolbar.Size = new System.Drawing.Size(1251, 25);
		this.MainToolbar.Text = "c1ToolBar1";
		this.MainToolbar.VisualStyle = C1.Win.C1Command.VisualStyle.Custom;
		this.MainToolbar.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
		// 
		// MainCommandHolder
		// 
		this.MainCommandHolder.Commands.Add(this.LocatesInputCommand);
		this.MainCommandHolder.Commands.Add(this.LocatesSummaryCommand);
		this.MainCommandHolder.Commands.Add(this.LocatesResearchCommand);
		this.MainCommandHolder.Commands.Add(this.LocatesTradingGroupCommand);
		this.MainCommandHolder.Commands.Add(this.LocatesExportToolCommand);
		this.MainCommandHolder.Commands.Add(this.LocatesInventoryLookupCommand);
		this.MainCommandHolder.Owner = this;
		// 
		// LocatesInputCommand
		// 
		this.LocatesInputCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("LocatesInputCommand.Icon")));
		this.LocatesInputCommand.Name = "LocatesInputCommand";
		this.LocatesInputCommand.Text = "Locates Input";
		this.LocatesInputCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.LocatesInputCommand_Click);
		// 
		// LocatesSummaryCommand
		// 
		this.LocatesSummaryCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("LocatesSummaryCommand.Icon")));
		this.LocatesSummaryCommand.Name = "LocatesSummaryCommand";
		this.LocatesSummaryCommand.Text = "Locates Summary";
		this.LocatesSummaryCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.LocatesSummaryCommand_Click);
		// 
		// LocatesResearchCommand
		// 
		this.LocatesResearchCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("LocatesResearchCommand.Icon")));
		this.LocatesResearchCommand.Name = "LocatesResearchCommand";
		this.LocatesResearchCommand.Text = "Locates Research";
		this.LocatesResearchCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.LocatesResearchCommand_Click);
		// 
		// LocatesTradingGroupCommand
		// 
		this.LocatesTradingGroupCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("LocatesTradingGroupCommand.Icon")));
		this.LocatesTradingGroupCommand.Name = "LocatesTradingGroupCommand";
		this.LocatesTradingGroupCommand.Text = "Locates Trading Groups";
		this.LocatesTradingGroupCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.LocatesTradingGroupCommand_Click);
		// 
		// LocatesExportToolCommand
		// 
		this.LocatesExportToolCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("LocatesExportToolCommand.Icon")));
		this.LocatesExportToolCommand.Name = "LocatesExportToolCommand";
		this.LocatesExportToolCommand.Text = "New Command";
		this.LocatesExportToolCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.LocatesExportToolCommand_Click);
		// 
		// LocatesInputToolbarLink
		// 
		this.LocatesInputToolbarLink.Command = this.LocatesInputCommand;
		// 
		// LocatesSummaryCommandToolbarLink
		// 
		this.LocatesSummaryCommandToolbarLink.Command = this.LocatesSummaryCommand;
		this.LocatesSummaryCommandToolbarLink.SortOrder = 1;
		// 
		// LocatesResearchCommandToolbarLink
		// 
		this.LocatesResearchCommandToolbarLink.Command = this.LocatesResearchCommand;
		this.LocatesResearchCommandToolbarLink.SortOrder = 3;
		// 
		// LocatesTradingGroupCommandToolbarLink
		// 
		this.LocatesTradingGroupCommandToolbarLink.Command = this.LocatesTradingGroupCommand;
		this.LocatesTradingGroupCommandToolbarLink.SortOrder = 4;
		// 
		// c1CommandLink2
		// 
		this.c1CommandLink2.Command = this.LocatesExportToolCommand;
		this.c1CommandLink2.SortOrder = 5;
		// 
		// CloseButton
		// 
		this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(241)))), ((int)(((byte)(250)))));
		this.CloseButton.Dock = System.Windows.Forms.DockStyle.Right;
		this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.CloseButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.CloseButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseButton.Image")));
		this.CloseButton.Location = new System.Drawing.Point(1251, 0);
		this.CloseButton.Name = "CloseButton";
		this.CloseButton.Size = new System.Drawing.Size(27, 25);
		this.CloseButton.TabIndex = 1;
		this.CloseButton.UseVisualStyleBackColor = false;
		this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
		// 
		// LocatesInventoryLookupCommandLink
		// 
		this.LocatesInventoryLookupCommandLink.Command = this.LocatesInventoryLookupCommand;
		this.LocatesInventoryLookupCommandLink.SortOrder = 2;
		// 
		// LocatesInventoryLookupCommand
		// 
		this.LocatesInventoryLookupCommand.Icon = ((System.Drawing.Icon)(resources.GetObject("LocatesInventoryLookupCommand.Icon")));
		this.LocatesInventoryLookupCommand.Name = "LocatesInventoryLookupCommand";
		this.LocatesInventoryLookupCommand.Text = "Inventory Lookup";
		this.LocatesInventoryLookupCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.LocatesInventoryLookupCommand_Click);
		// 
		// MainForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(1278, 25);
		this.Controls.Add(this.MainToolbar);
		this.Controls.Add(this.CloseButton);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.Name = "MainForm";
		this.Text = "MainForm";
		this.Load += new System.EventHandler(this.MainForm_Load);
		((System.ComponentModel.ISupportInitialize)(this.MainShellAppBar)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).EndInit();
		this.ResumeLayout(false);

    }

    #endregion

    private LogicNP.ShellObjects.ShellAppBar MainShellAppBar;
    private C1.Win.C1Command.C1ToolBar MainToolbar;
    private C1.Win.C1Command.C1CommandHolder MainCommandHolder;
    private C1.Win.C1Command.C1CommandLink LocatesInputToolbarLink;
    private C1.Win.C1Command.C1Command LocatesInputCommand;
    private C1.Win.C1Command.C1Command LocatesSummaryCommand;
    private C1.Win.C1Command.C1CommandLink LocatesSummaryCommandToolbarLink;
    private C1.Win.C1Command.C1Command LocatesResearchCommand;
    private C1.Win.C1Command.C1Command LocatesTradingGroupCommand;
    private C1.Win.C1Command.C1CommandLink LocatesResearchCommandToolbarLink;
    private C1.Win.C1Command.C1CommandLink LocatesTradingGroupCommandToolbarLink;
    private C1.Win.C1Command.C1CommandLink c1CommandLink1;
    private C1.Win.C1Input.C1Button CloseButton;
    private C1.Win.C1Command.C1Command LocatesExportToolCommand;
    private C1.Win.C1Command.C1CommandLink c1CommandLink2;
	  private C1.Win.C1Command.C1CommandLink c1CommandLink3;
	  private C1.Win.C1Command.C1CommandLink c1CommandLink4;
	  private C1.Win.C1Command.C1Command LocatesInventoryLookupCommand;
	  private C1.Win.C1Command.C1CommandLink LocatesInventoryLookupCommandLink;
  }
}