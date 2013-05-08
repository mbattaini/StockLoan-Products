namespace CentralClient
{
	partial class TradingContractsMarksForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradingContractsMarksForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.BookGroupCombo = new C1.Win.C1List.C1Combo();
            this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
            this.label1 = new System.Windows.Forms.Label();
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.ApplyRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            this.CancelRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            this.CheckAllRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            this.UncheckAllRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            this.ContractMarksGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.MarksContextMenu = new C1.Win.C1Command.C1ContextMenu();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.SendToCommand = new C1.Win.C1Command.C1CommandMenu();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.SendToExcelCommand = new C1.Win.C1Command.C1Command();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContractMarksGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.BookGroupCombo);
            this.panel2.Controls.Add(this.BookGroupNameLabel);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1354, 32);
            this.panel2.TabIndex = 1;
            // 
            // BookGroupCombo
            // 
            this.BookGroupCombo.AddItemSeparator = ';';
            this.BookGroupCombo.AutoSize = false;
            this.BookGroupCombo.Caption = "";
            this.BookGroupCombo.CaptionHeight = 17;
            this.BookGroupCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.BookGroupCombo.ColumnCaptionHeight = 17;
            this.BookGroupCombo.ColumnFooterHeight = 17;
            this.BookGroupCombo.ColumnWidth = 100;
            this.BookGroupCombo.ContentHeight = 13;
            this.BookGroupCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.BookGroupCombo.DropDownWidth = 250;
            this.BookGroupCombo.EditorBackColor = System.Drawing.SystemColors.Window;
            this.BookGroupCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookGroupCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.BookGroupCombo.EditorHeight = 13;
            this.BookGroupCombo.ExtendRightColumn = true;
            this.BookGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("BookGroupCombo.Images"))));
            this.BookGroupCombo.ItemHeight = 15;
            this.BookGroupCombo.Location = new System.Drawing.Point(100, 7);
            this.BookGroupCombo.MatchEntryTimeout = ((long)(2000));
            this.BookGroupCombo.MaxDropDownItems = ((short)(5));
            this.BookGroupCombo.MaxLength = 32767;
            this.BookGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.BookGroupCombo.Name = "BookGroupCombo";
            this.BookGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.BookGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.BookGroupCombo.Size = new System.Drawing.Size(105, 19);
            this.BookGroupCombo.TabIndex = 113;
            this.BookGroupCombo.TextChanged += new System.EventHandler(this.BookGroupCombo_TextChanged);
            this.BookGroupCombo.PropBag = resources.GetString("BookGroupCombo.PropBag");
            // 
            // BookGroupNameLabel
            // 
            this.BookGroupNameLabel.AutoSize = true;
            this.BookGroupNameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.BookGroupNameLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookGroupNameLabel.ForeColor = System.Drawing.Color.Black;
            this.BookGroupNameLabel.Location = new System.Drawing.Point(218, 10);
            this.BookGroupNameLabel.Name = "BookGroupNameLabel";
            this.BookGroupNameLabel.Size = new System.Drawing.Size(134, 13);
            this.BookGroupNameLabel.TabIndex = 111;
            this.BookGroupNameLabel.Tag = null;
            this.BookGroupNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BookGroupNameLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 109;
            this.label1.Text = "Book Group:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.ApplyRibbonButton);
            this.c1StatusBar1.RightPaneItems.Add(this.CancelRibbonButton);
            this.c1StatusBar1.RightPaneItems.Add(this.CheckAllRibbonButton);
            this.c1StatusBar1.RightPaneItems.Add(this.UncheckAllRibbonButton);
            // 
            // ApplyRibbonButton
            // 
            this.ApplyRibbonButton.Name = "ApplyRibbonButton";
            this.ApplyRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("ApplyRibbonButton.SmallImage")));
            this.ApplyRibbonButton.Text = "Apply";
            this.ApplyRibbonButton.Click += new System.EventHandler(this.ApplyRibbonButton_Click);
            // 
            // CancelRibbonButton
            // 
            this.CancelRibbonButton.Name = "CancelRibbonButton";
            this.CancelRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("CancelRibbonButton.SmallImage")));
            this.CancelRibbonButton.Text = "Cancel";
            this.CancelRibbonButton.Click += new System.EventHandler(this.CancelRibbonButton_Click);
            // 
            // CheckAllRibbonButton
            // 
            this.CheckAllRibbonButton.Name = "CheckAllRibbonButton";
            this.CheckAllRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("CheckAllRibbonButton.SmallImage")));
            this.CheckAllRibbonButton.Text = "Check All";
            this.CheckAllRibbonButton.Click += new System.EventHandler(this.CheckAllRibbonButton_Click);
            // 
            // UncheckAllRibbonButton
            // 
            this.UncheckAllRibbonButton.Name = "UncheckAllRibbonButton";
            this.UncheckAllRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("UncheckAllRibbonButton.SmallImage")));
            this.UncheckAllRibbonButton.Text = "Uncheck All";
            this.UncheckAllRibbonButton.Click += new System.EventHandler(this.UncheckAllRibbonButton_Click);
            // 
            // ContractMarksGrid
            // 
            this.ContractMarksGrid.AllowFilter = false;
            this.ContractMarksGrid.AlternatingRows = true;
            this.c1CommandHolder1.SetC1ContextMenu(this.ContractMarksGrid, this.MarksContextMenu);
            this.ContractMarksGrid.CaptionHeight = 17;
            this.ContractMarksGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.ContractMarksGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContractMarksGrid.EmptyRows = true;
            this.ContractMarksGrid.ExtendRightColumn = true;
            this.ContractMarksGrid.FetchRowStyles = true;
            this.ContractMarksGrid.FilterBar = true;
            this.ContractMarksGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.ContractMarksGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ContractMarksGrid.Images"))));
            this.ContractMarksGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ContractMarksGrid.Images1"))));
            this.ContractMarksGrid.Location = new System.Drawing.Point(0, 32);
            this.ContractMarksGrid.Name = "ContractMarksGrid";
            this.ContractMarksGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.ContractMarksGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.ContractMarksGrid.PreviewInfo.ZoomFactor = 75D;
            this.ContractMarksGrid.RecordSelectors = false;
            this.ContractMarksGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.ContractMarksGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.ContractMarksGrid.RowHeight = 15;
            this.ContractMarksGrid.Size = new System.Drawing.Size(1354, 352);
            this.ContractMarksGrid.TabIndex = 4;
            this.ContractMarksGrid.Text = "c1TrueDBGrid2";
            this.ContractMarksGrid.UseColumnStyles = false;
            this.ContractMarksGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Black;
            this.ContractMarksGrid.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.ContractMarksGrid_AfterColUpdate);
            this.ContractMarksGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ContractMarksGrid_BeforeUpdate);
            this.ContractMarksGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ContractMarksGrid_FormatText);
            this.ContractMarksGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.ContractMarksGrid_FetchRowStyle);
            this.ContractMarksGrid.Filter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.ContractMarksGrid_Filter);
            this.ContractMarksGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ContractMarksGrid_KeyPress);
            this.ContractMarksGrid.PropBag = resources.GetString("ContractMarksGrid.PropBag");
            // 
            // MarksContextMenu
            // 
            this.MarksContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1});
            this.MarksContextMenu.Name = "MarksContextMenu";
            this.MarksContextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.SendToCommand;
            // 
            // SendToCommand
            // 
            this.SendToCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink2});
            this.SendToCommand.Name = "SendToCommand";
            this.SendToCommand.Text = "Send To";
            this.SendToCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // c1CommandLink2
            // 
            this.c1CommandLink2.Command = this.SendToExcelCommand;
            // 
            // SendToExcelCommand
            // 
            this.SendToExcelCommand.Name = "SendToExcelCommand";
            this.SendToExcelCommand.Text = "Excel";
            this.SendToExcelCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SendToExcelCommand_Click);
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Commands.Add(this.MarksContextMenu);
            this.c1CommandHolder1.Commands.Add(this.SendToCommand);
            this.c1CommandHolder1.Commands.Add(this.SendToExcelCommand);
            this.c1CommandHolder1.Owner = this;
            // 
            // TradingContractsMarksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 407);
            this.Controls.Add(this.ContractMarksGrid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.c1StatusBar1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TradingContractsMarksForm";
            this.Text = "Trading - Contracts Marks";
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Windows7;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TradingContractsMarksForm_FormClosed);
            this.Load += new System.EventHandler(this.TradingContractsMarksForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ContractMarksGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Panel panel2;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ContractMarksGrid;
		private C1.Win.C1Input.C1Label BookGroupNameLabel;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1List.C1Combo BookGroupCombo;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Ribbon.RibbonButton ApplyRibbonButton;
        private C1.Win.C1Ribbon.RibbonButton CancelRibbonButton;
        private C1.Win.C1Ribbon.RibbonButton CheckAllRibbonButton;
        private C1.Win.C1Ribbon.RibbonButton UncheckAllRibbonButton;
        private C1.Win.C1Command.C1ContextMenu MarksContextMenu;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink1;
        private C1.Win.C1Command.C1CommandMenu SendToCommand;
        private C1.Win.C1Command.C1CommandLink c1CommandLink2;
        private C1.Win.C1Command.C1Command SendToExcelCommand;
	}
}