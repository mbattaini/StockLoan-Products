namespace CentralClient
{
  partial class AdminUsersForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminUsersForm));
        this.UsersGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
        this.UserRolesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
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
        ((System.ComponentModel.ISupportInitialize)(this.UsersGrid)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.UserRolesGrid)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
        this.SuspendLayout();
        // 
        // UsersGrid
        // 
        this.UsersGrid.AllowAddNew = true;
        this.UsersGrid.AllowDelete = true;
        this.UsersGrid.AlternatingRows = true;
        this.CommandHolder.SetC1ContextMenu(this.UsersGrid, this.ContextMenu);
        this.UsersGrid.CaptionHeight = 17;
        this.UsersGrid.ChildGrid = this.UserRolesGrid;
        this.UsersGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
        this.UsersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        this.UsersGrid.EmptyRows = true;
        this.UsersGrid.ExtendRightColumn = true;
        this.UsersGrid.GroupByCaption = "Drag a column header here to group by that column";
        this.UsersGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("UsersGrid.Images"))));
        this.UsersGrid.Location = new System.Drawing.Point(0, 0);
        this.UsersGrid.Name = "UsersGrid";
        this.UsersGrid.Padding = new System.Windows.Forms.Padding(3);
        this.UsersGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
        this.UsersGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
        this.UsersGrid.PreviewInfo.ZoomFactor = 75;
        this.UsersGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("UsersGrid.PrintInfo.PageSettings")));
        this.UsersGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
        this.UsersGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
        this.UsersGrid.RowHeight = 15;
        this.UsersGrid.Size = new System.Drawing.Size(1519, 721);
        this.UsersGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
        this.UsersGrid.TabIndex = 0;
        this.UsersGrid.Text = "Users";
        this.UsersGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.UsersGrid_BeforeColEdit);
        this.UsersGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.UsersGrid_BeforeUpdate);
        this.UsersGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.UsersGrid_BeforeDelete);
        this.UsersGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UsersGrid_KeyPress);
        this.UsersGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.Grid_FormatText);
        this.UsersGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.UsersGrid_RowColChange);
        this.UsersGrid.PropBag = resources.GetString("UsersGrid.PropBag");
        // 
        // UserRolesGrid
        // 
        this.UserRolesGrid.AllowAddNew = true;
        this.UserRolesGrid.AllowColMove = false;
        this.UserRolesGrid.AllowColSelect = false;
        this.UserRolesGrid.AllowDelete = true;
        this.UserRolesGrid.AlternatingRows = true;
        this.CommandHolder.SetC1ContextMenu(this.UserRolesGrid, this.ContextMenu);
        this.UserRolesGrid.CaptionHeight = 17;
        this.UserRolesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
        this.UserRolesGrid.EmptyRows = true;
        this.UserRolesGrid.ExtendRightColumn = true;
        this.UserRolesGrid.GroupByCaption = "Drag a column header here to group by that column";
        this.UserRolesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("UserRolesGrid.Images"))));
        this.UserRolesGrid.Location = new System.Drawing.Point(209, 60);
        this.UserRolesGrid.Name = "UserRolesGrid";
        this.UserRolesGrid.Padding = new System.Windows.Forms.Padding(3);
        this.UserRolesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
        this.UserRolesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
        this.UserRolesGrid.PreviewInfo.ZoomFactor = 75;
        this.UserRolesGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("UserRolesGrid.PrintInfo.PageSettings")));
        this.UserRolesGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
        this.UserRolesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
        this.UserRolesGrid.RowHeight = 15;
        this.UserRolesGrid.Size = new System.Drawing.Size(863, 395);
        this.UserRolesGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
        this.UserRolesGrid.TabIndex = 1;
        this.UserRolesGrid.TabStop = false;
        this.UserRolesGrid.Text = "User Roles";
        this.UserRolesGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.UserRolesGrid_BeforeColEdit);
        this.UserRolesGrid.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.UserRolesGrid_AfterColEdit);
        this.UserRolesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.UserRolesGrid_BeforeUpdate);
        this.UserRolesGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.UserRolesGrid_BeforeDelete);
        this.UserRolesGrid.OnAddNew += new System.EventHandler(this.UserRolesGrid_OnAddNew);
        this.UserRolesGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserRolesGrid_KeyPress);
        this.UserRolesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.Grid_FormatText);
        this.UserRolesGrid.PropBag = resources.GetString("UserRolesGrid.PropBag");
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
        this.SendToCommand.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
        this.SendToCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Silver;
        this.SendToCommand.Popup += new System.EventHandler(this.SendToCommand_Popup);
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
        // AdminUsersForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1519, 743);
        this.Controls.Add(this.UserRolesGrid);
        this.Controls.Add(this.UsersGrid);
        this.Controls.Add(this.c1StatusBar1);
        this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Name = "AdminUsersForm";
        this.Text = "Admin - Users";
        this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Silver;
        this.Load += new System.EventHandler(this.AdminUsersForm_Load);
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminUsersForm_FormClosed);
        ((System.ComponentModel.ISupportInitialize)(this.UsersGrid)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.UserRolesGrid)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

	  private C1.Win.C1TrueDBGrid.C1TrueDBGrid UsersGrid;
      private C1.Win.C1TrueDBGrid.C1TrueDBGrid UserRolesGrid;
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
  }
}