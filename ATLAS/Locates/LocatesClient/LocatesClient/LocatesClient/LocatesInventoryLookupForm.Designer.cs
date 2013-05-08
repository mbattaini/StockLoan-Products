namespace LocatesClient
{
	partial class LocatesInventoryLookupForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocatesInventoryLookupForm));
			this.InventoryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.SecurityIdLabel = new C1.Win.C1Input.C1Label();
			this.SecurityIdTextBox = new C1.Win.C1Input.C1TextBox();
			this.MainContextMenu = new C1.Win.C1Command.C1ContextMenu();
			this.MainCommandHolder = new C1.Win.C1Command.C1CommandHolder();
			this.SendToCommandLink = new C1.Win.C1Command.C1CommandLink();
			this.SendToCommand = new C1.Win.C1Command.C1CommandMenu();
			this.SendToExcelCommandLink = new C1.Win.C1Command.C1CommandLink();
			this.SendToExcelCommand = new C1.Win.C1Command.C1Command();
			this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.InventoryGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SecurityIdLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SecurityIdTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).BeginInit();
			this.SuspendLayout();
			// 
			// InventoryGrid
			// 
			this.MainCommandHolder.SetC1ContextMenu(this.InventoryGrid, this.MainContextMenu);
			this.InventoryGrid.CaptionHeight = 17;
			this.InventoryGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.InventoryGrid.EmptyRows = true;
			this.InventoryGrid.ExtendRightColumn = true;
			this.InventoryGrid.FetchRowStyles = true;
			this.InventoryGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.InventoryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("InventoryGrid.Images"))));
			this.InventoryGrid.Location = new System.Drawing.Point(0, 32);
			this.InventoryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
			this.InventoryGrid.Name = "InventoryGrid";
			this.InventoryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.InventoryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.InventoryGrid.PreviewInfo.ZoomFactor = 75;
			this.InventoryGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("InventoryGrid.PrintInfo.PageSettings")));
			this.InventoryGrid.RecordSelectors = false;
			this.InventoryGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.InventoryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.InventoryGrid.RowHeight = 15;
			this.InventoryGrid.Size = new System.Drawing.Size(517, 313);
			this.InventoryGrid.TabIndex = 1;
			this.InventoryGrid.Text = "c1TrueDBGrid1";
			this.InventoryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.InventoryGrid_FormatText);
			this.InventoryGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.InventoryGrid_FetchRowStyle);
			this.InventoryGrid.PropBag = resources.GetString("InventoryGrid.PropBag");
			// 
			// SecurityIdLabel
			// 
			this.SecurityIdLabel.AutoSize = true;
			this.SecurityIdLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(195)))), ((int)(((byte)(235)))));
			this.SecurityIdLabel.ForeColor = System.Drawing.Color.Black;
			this.SecurityIdLabel.Location = new System.Drawing.Point(5, 9);
			this.SecurityIdLabel.Name = "SecurityIdLabel";
			this.SecurityIdLabel.Size = new System.Drawing.Size(77, 13);
			this.SecurityIdLabel.TabIndex = 3;
			this.SecurityIdLabel.Tag = null;
			this.SecurityIdLabel.Text = "Security ID:";
			this.SecurityIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.SecurityIdLabel.TextDetached = true;
			this.SecurityIdLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
			// 
			// SecurityIdTextBox
			// 
			this.SecurityIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SecurityIdTextBox.Location = new System.Drawing.Point(108, 7);
			this.SecurityIdTextBox.Name = "SecurityIdTextBox";
			this.SecurityIdTextBox.Size = new System.Drawing.Size(100, 19);
			this.SecurityIdTextBox.TabIndex = 5;
			this.SecurityIdTextBox.Tag = null;
			this.SecurityIdTextBox.TextDetached = true;
			this.SecurityIdTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SecurityIdTextBox_KeyPress);
			// 
			// MainContextMenu
			// 
			this.MainContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.SendToCommandLink});
			this.MainContextMenu.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MainContextMenu.Name = "MainContextMenu";
			this.MainContextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
			// 
			// MainCommandHolder
			// 
			this.MainCommandHolder.Commands.Add(this.MainContextMenu);
			this.MainCommandHolder.Commands.Add(this.SendToCommand);
			this.MainCommandHolder.Commands.Add(this.SendToExcelCommand);
			this.MainCommandHolder.Owner = this;
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
			this.SendToCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
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
			// SaveDialog
			// 
			this.SaveDialog.DefaultExt = "xls";
			// 
			// LocatesInventoryLookupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(517, 345);
			this.Controls.Add(this.SecurityIdLabel);
			this.Controls.Add(this.InventoryGrid);
			this.Controls.Add(this.SecurityIdTextBox);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "LocatesInventoryLookupForm";
			this.Text = "Locates - Inventory Lookup";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LocatesInventoryLookupForm_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.InventoryGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SecurityIdLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SecurityIdTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MainCommandHolder)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid InventoryGrid;
		private C1.Win.C1Input.C1Label SecurityIdLabel;
		private C1.Win.C1Input.C1TextBox SecurityIdTextBox;
		private C1.Win.C1Command.C1ContextMenu MainContextMenu;
		private C1.Win.C1Command.C1CommandLink SendToCommandLink;
		private C1.Win.C1Command.C1CommandHolder MainCommandHolder;
		private C1.Win.C1Command.C1CommandMenu SendToCommand;
		private C1.Win.C1Command.C1CommandLink SendToExcelCommandLink;
		private C1.Win.C1Command.C1Command SendToExcelCommand;
		private System.Windows.Forms.SaveFileDialog SaveDialog;
	}
}