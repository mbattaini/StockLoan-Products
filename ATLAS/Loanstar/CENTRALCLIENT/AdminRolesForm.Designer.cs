namespace CentralClient
{
	partial class AdminRolesForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid RolesGrid;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid RolesFunctionGrid;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminRolesForm));
            this.RolesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.RolesFunctionGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
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
            ((System.ComponentModel.ISupportInitialize)(this.RolesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RolesFunctionGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // RolesGrid
            // 
            this.RolesGrid.AllowAddNew = true;
            this.RolesGrid.AllowDelete = true;
            this.RolesGrid.AlternatingRows = true;
            this.CommandHolder.SetC1ContextMenu(this.RolesGrid, this.ContextMenu);
            this.RolesGrid.CaptionHeight = 17;
            this.RolesGrid.ChildGrid = this.RolesFunctionGrid;
            this.RolesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.RolesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RolesGrid.EmptyRows = true;
            this.RolesGrid.ExtendRightColumn = true;
            this.RolesGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.RolesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("RolesGrid.Images"))));
            this.RolesGrid.Location = new System.Drawing.Point(0, 0);
            this.RolesGrid.Name = "RolesGrid";
            this.RolesGrid.Padding = new System.Windows.Forms.Padding(3);
            this.RolesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.RolesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.RolesGrid.PreviewInfo.ZoomFactor = 75;
            this.RolesGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("RolesGrid.PrintInfo.PageSettings")));
            this.RolesGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.RolesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.RolesGrid.RowHeight = 15;
            this.RolesGrid.Size = new System.Drawing.Size(1029, 680);
            this.RolesGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
            this.RolesGrid.TabIndex = 2;
            this.RolesGrid.TabStop = false;
            this.RolesGrid.Text = "Roles";
            this.RolesGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.RolesGrid_BeforeColEdit);
            this.RolesGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.RolesGrid_BeforeUpdate);
            this.RolesGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.RolesGrid_BeforeDelete);
            this.RolesGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RolesGrid_KeyPress);
            this.RolesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.Grid_FormatText);
            this.RolesGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.RolesGrid_RowColChange);
            this.RolesGrid.PropBag = resources.GetString("RolesGrid.PropBag");
            // 
            // RolesFunctionGrid
            // 
            this.RolesFunctionGrid.AlternatingRows = true;
            this.CommandHolder.SetC1ContextMenu(this.RolesFunctionGrid, this.ContextMenu);
            this.RolesFunctionGrid.CaptionHeight = 17;
            this.RolesFunctionGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.RolesFunctionGrid.EmptyRows = true;
            this.RolesFunctionGrid.ExtendRightColumn = true;
            this.RolesFunctionGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.RolesFunctionGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("RolesFunctionGrid.Images"))));
            this.RolesFunctionGrid.Location = new System.Drawing.Point(117, 67);
            this.RolesFunctionGrid.Name = "RolesFunctionGrid";
            this.RolesFunctionGrid.Padding = new System.Windows.Forms.Padding(3);
            this.RolesFunctionGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.RolesFunctionGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.RolesFunctionGrid.PreviewInfo.ZoomFactor = 75;
            this.RolesFunctionGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("RolesFunctionGrid.PrintInfo.PageSettings")));
            this.RolesFunctionGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.RolesFunctionGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.RolesFunctionGrid.RowHeight = 15;
            this.RolesFunctionGrid.Size = new System.Drawing.Size(685, 360);
            this.RolesFunctionGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
            this.RolesFunctionGrid.TabIndex = 3;
            this.RolesFunctionGrid.TabStop = false;
            this.RolesFunctionGrid.Text = "Role Functions";
            this.RolesFunctionGrid.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.RolesFunctionGrid_BeforeColEdit);
            this.RolesFunctionGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.RolesFunctionGrid_BeforeUpdate);
            this.RolesFunctionGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RolesFunctionGrid_KeyPress);
            this.RolesFunctionGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.Grid_FormatText);
            this.RolesFunctionGrid.PropBag = resources.GetString("RolesFunctionGrid.PropBag");
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
            this.CommandHolder.Commands.Add(this.SendToExcelCommand);
            this.CommandHolder.Commands.Add(this.SendToClipboardCommand);
            this.CommandHolder.Commands.Add(this.SendToEmailCommand);
            this.CommandHolder.Owner = this;
            this.CommandHolder.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.Name = "c1StatusBar1";
            // 
            // AdminRolesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 702);
            this.Controls.Add(this.RolesFunctionGrid);
            this.Controls.Add(this.RolesGrid);
            this.Controls.Add(this.c1StatusBar1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminRolesForm";
            this.Text = "Admin - Roles";
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Silver;
            this.Load += new System.EventHandler(this.AdminRolesForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminRolesForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.RolesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RolesFunctionGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CommandHolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private C1.Win.C1Command.C1ContextMenu ContextMenu;
        private C1.Win.C1Command.C1CommandHolder CommandHolder;
        private C1.Win.C1Command.C1CommandLink SendToCommandLink;
        private C1.Win.C1Command.C1CommandMenu SendToCommand;
        private C1.Win.C1Command.C1CommandLink SendToExcelCommandLink;
        private C1.Win.C1Command.C1Command SendToExcelCommand;
        private C1.Win.C1Command.C1CommandLink SendToClipboardCommandLink;
        private C1.Win.C1Command.C1Command SendToClipboardCommand;
        private C1.Win.C1Command.C1CommandLink SendToEmailCommandLink;
        private C1.Win.C1Command.C1Command SendToEmailCommand;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;

	}
}