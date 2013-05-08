namespace Golden
{
    partial class ShortSaleNegativeRebateBillingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortSaleNegativeRebateBillingForm));
            this.RefreshButton = new System.Windows.Forms.Button();
            this.SummaryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.RebateSummaryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.FromLabel = new C1.Win.C1Input.C1Label();
            this.FromDatePicker = new System.Windows.Forms.DateTimePicker();
            this.ToDatePicker = new System.Windows.Forms.DateTimePicker();
            this.PensonRadio = new System.Windows.Forms.RadioButton();
            this.BroadRidgeRadio = new System.Windows.Forms.RadioButton();
            this.MainContextMenu = new C1.Win.C1Command.C1ContextMenu();
            this.c1CommandLink4 = new C1.Win.C1Command.C1CommandLink();
            this.ActionsCommand = new C1.Win.C1Command.C1CommandMenu();
            this.c1CommandLink6 = new C1.Win.C1Command.C1CommandLink();
            this.mnuSetMarkups = new C1.Win.C1Command.C1Command();
            this.c1CommandLink2 = new C1.Win.C1Command.C1CommandLink();
            this.c1CommandControl1 = new C1.Win.C1Command.C1Command();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.SendToCommand = new C1.Win.C1Command.C1CommandMenu();
            this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
            this.SendToExcelCommand = new C1.Win.C1Command.C1Command();
            this.CreateBillingCommand = new C1.Win.C1Command.C1Command();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.c1Label1 = new C1.Win.C1Input.C1Label();
            this.c1Label2 = new C1.Win.C1Input.C1Label();
            this.c1Label3 = new C1.Win.C1Input.C1Label();
            ((System.ComponentModel.ISupportInitialize)(this.SummaryGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RebateSummaryGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FromLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).BeginInit();
            this.SuspendLayout();
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshButton.Location = new System.Drawing.Point(1217, 13);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(87, 23);
            this.RefreshButton.TabIndex = 25;
            this.RefreshButton.Text = "&Refresh";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // SummaryGrid
            // 
            this.SummaryGrid.AllowColSelect = false;
            this.SummaryGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.SummaryGrid.CaptionHeight = 17;
            this.SummaryGrid.ColumnFooters = true;
            this.SummaryGrid.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.SummaryGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.SummaryGrid.EmptyRows = true;
            this.SummaryGrid.ExtendRightColumn = true;
            this.SummaryGrid.FetchRowStyles = true;
            this.SummaryGrid.FilterBar = true;
            this.SummaryGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SummaryGrid.GroupByAreaVisible = false;
            this.SummaryGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.SummaryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("SummaryGrid.Images"))));
            this.SummaryGrid.Location = new System.Drawing.Point(38, 158);
            this.SummaryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
            this.SummaryGrid.Name = "SummaryGrid";
            this.SummaryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.SummaryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.SummaryGrid.PreviewInfo.ZoomFactor = 75D;
            this.SummaryGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("SummaryGrid.PrintInfo.PageSettings")));
            this.SummaryGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
            this.SummaryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.SummaryGrid.RowHeight = 15;
            this.SummaryGrid.RowSubDividerColor = System.Drawing.Color.LightGray;
            this.SummaryGrid.Size = new System.Drawing.Size(1173, 246);
            this.SummaryGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
            this.SummaryGrid.TabIndex = 23;
            this.SummaryGrid.TabStop = false;
            this.SummaryGrid.WrapCellPointer = true;
            this.SummaryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.SummaryGrid_FormatText);
            this.SummaryGrid.PropBag = resources.GetString("SummaryGrid.PropBag");
            // 
            // RebateSummaryGrid
            // 
            this.RebateSummaryGrid.AllowColSelect = false;
            this.RebateSummaryGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.RebateSummaryGrid.CaptionHeight = 17;
            this.RebateSummaryGrid.ChildGrid = this.SummaryGrid;
            this.RebateSummaryGrid.ColumnFooters = true;
            this.RebateSummaryGrid.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.RebateSummaryGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.RebateSummaryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RebateSummaryGrid.EmptyRows = true;
            this.RebateSummaryGrid.ExtendRightColumn = true;
            this.RebateSummaryGrid.FetchRowStyles = true;
            this.RebateSummaryGrid.FilterBar = true;
            this.RebateSummaryGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RebateSummaryGrid.GroupByAreaVisible = false;
            this.RebateSummaryGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.RebateSummaryGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("RebateSummaryGrid.Images"))));
            this.RebateSummaryGrid.Location = new System.Drawing.Point(0, 40);
            this.RebateSummaryGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
            this.RebateSummaryGrid.Name = "RebateSummaryGrid";
            this.RebateSummaryGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.RebateSummaryGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.RebateSummaryGrid.PreviewInfo.ZoomFactor = 75D;
            this.RebateSummaryGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("RebateSummaryGrid.PrintInfo.PageSettings")));
            this.RebateSummaryGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
            this.RebateSummaryGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.RebateSummaryGrid.RowHeight = 15;
            this.RebateSummaryGrid.RowSubDividerColor = System.Drawing.Color.LightGray;
            this.RebateSummaryGrid.Size = new System.Drawing.Size(1308, 527);
            this.RebateSummaryGrid.TabIndex = 26;
            this.RebateSummaryGrid.WrapCellPointer = true;
            this.RebateSummaryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.RebateSummaryGrid_FormatText);
            this.RebateSummaryGrid.BeforeOpen += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.RebateSummaryGrid_BeforeOpen);
            this.RebateSummaryGrid.PropBag = resources.GetString("RebateSummaryGrid.PropBag");
            // 
            // FromLabel
            // 
            this.FromLabel.Location = new System.Drawing.Point(-113, 16);
            this.FromLabel.Name = "FromLabel";
            this.FromLabel.Size = new System.Drawing.Size(75, 16);
            this.FromLabel.TabIndex = 20;
            this.FromLabel.Tag = null;
            this.FromLabel.Text = "From:";
            this.FromLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.FromLabel.TextDetached = true;
            // 
            // FromDatePicker
            // 
            this.FromDatePicker.Location = new System.Drawing.Point(63, 14);
            this.FromDatePicker.Name = "FromDatePicker";
            this.FromDatePicker.Size = new System.Drawing.Size(230, 21);
            this.FromDatePicker.TabIndex = 19;
            this.FromDatePicker.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // ToDatePicker
            // 
            this.ToDatePicker.Location = new System.Drawing.Point(411, 14);
            this.ToDatePicker.Name = "ToDatePicker";
            this.ToDatePicker.Size = new System.Drawing.Size(251, 21);
            this.ToDatePicker.TabIndex = 18;
            this.ToDatePicker.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // PensonRadio
            // 
            this.PensonRadio.AutoSize = true;
            this.PensonRadio.Checked = true;
            this.PensonRadio.Location = new System.Drawing.Point(770, 16);
            this.PensonRadio.Name = "PensonRadio";
            this.PensonRadio.Size = new System.Drawing.Size(66, 17);
            this.PensonRadio.TabIndex = 27;
            this.PensonRadio.TabStop = true;
            this.PensonRadio.Text = "Penson";
            this.PensonRadio.UseVisualStyleBackColor = true;
            this.PensonRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // BroadRidgeRadio
            // 
            this.BroadRidgeRadio.AutoSize = true;
            this.BroadRidgeRadio.Location = new System.Drawing.Point(842, 16);
            this.BroadRidgeRadio.Name = "BroadRidgeRadio";
            this.BroadRidgeRadio.Size = new System.Drawing.Size(91, 17);
            this.BroadRidgeRadio.TabIndex = 28;
            this.BroadRidgeRadio.Text = "BroadRidge";
            this.BroadRidgeRadio.UseVisualStyleBackColor = true;
            this.BroadRidgeRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // MainContextMenu
            // 
            this.MainContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink4,
            this.c1CommandLink2,
            this.c1CommandLink1});
            this.MainContextMenu.Name = "MainContextMenu";
            this.MainContextMenu.Text = "Set MarkUp";
            this.MainContextMenu.ToolTipText = "Set MarkUp rates for Trading Groups";
            this.MainContextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // c1CommandLink4
            // 
            this.c1CommandLink4.Command = this.ActionsCommand;
            // 
            // ActionsCommand
            // 
            this.ActionsCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink6});
            this.ActionsCommand.Name = "ActionsCommand";
            this.ActionsCommand.Text = "Actions";
            this.ActionsCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // c1CommandLink6
            // 
            this.c1CommandLink6.Command = this.mnuSetMarkups;
            // 
            // mnuSetMarkups
            // 
            this.mnuSetMarkups.Name = "mnuSetMarkups";
            this.mnuSetMarkups.Text = "Set Markups";
            this.mnuSetMarkups.ToolTipText = "Set Trading Group Markups";
            this.mnuSetMarkups.Click += new C1.Win.C1Command.ClickEventHandler(this.mnuSetMarkups_Click);
            // 
            // c1CommandLink2
            // 
            this.c1CommandLink2.Command = this.CreateBillingCommand;
            this.c1CommandLink2.SortOrder = 1;
            this.c1CommandLink2.ToolTipText = "Create Rebate Bills";
            // 
            // c1CommandControl1
            // 
            this.c1CommandControl1.Name = "c1CommandControl1";
            this.c1CommandControl1.Text = "Create Bills";
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.SendToCommand;
            this.c1CommandLink1.SortOrder = 2;
            // 
            // SendToCommand
            // 
            this.SendToCommand.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink3});
            this.SendToCommand.Name = "SendToCommand";
            this.SendToCommand.Text = "Send To";
            this.SendToCommand.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // c1CommandLink3
            // 
            this.c1CommandLink3.Command = this.SendToExcelCommand;
            // 
            // SendToExcelCommand
            // 
            this.SendToExcelCommand.Name = "SendToExcelCommand";
            this.SendToExcelCommand.Text = "Excel";
            this.SendToExcelCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.SendToExcelCommand_Click);
            // 
            // CreateBillingCommand
            // 
            this.CreateBillingCommand.Name = "CreateBillingCommand";
            this.CreateBillingCommand.Text = "Create Bills";
            this.CreateBillingCommand.Click += new C1.Win.C1Command.ClickEventHandler(this.CreateBillingCommand_Click);
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Commands.Add(this.MainContextMenu);
            this.c1CommandHolder1.Commands.Add(this.CreateBillingCommand);
            this.c1CommandHolder1.Commands.Add(this.SendToCommand);
            this.c1CommandHolder1.Commands.Add(this.SendToExcelCommand);
            this.c1CommandHolder1.Commands.Add(this.mnuSetMarkups);
            this.c1CommandHolder1.Commands.Add(this.c1CommandControl1);
            this.c1CommandHolder1.Commands.Add(this.ActionsCommand);
            this.c1CommandHolder1.Owner = this;
            // 
            // c1Label1
            // 
            this.c1Label1.AutoSize = true;
            this.c1Label1.Location = new System.Drawing.Point(16, 18);
            this.c1Label1.Name = "c1Label1";
            this.c1Label1.Size = new System.Drawing.Size(41, 13);
            this.c1Label1.TabIndex = 29;
            this.c1Label1.Tag = null;
            this.c1Label1.Text = "From:";
            this.c1Label1.TextDetached = true;
            this.c1Label1.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // c1Label2
            // 
            this.c1Label2.AutoSize = true;
            this.c1Label2.Location = new System.Drawing.Point(379, 18);
            this.c1Label2.Name = "c1Label2";
            this.c1Label2.Size = new System.Drawing.Size(26, 13);
            this.c1Label2.TabIndex = 30;
            this.c1Label2.Tag = null;
            this.c1Label2.Text = "To:";
            this.c1Label2.TextDetached = true;
            this.c1Label2.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // c1Label3
            // 
            this.c1Label3.AutoSize = true;
            this.c1Label3.Location = new System.Drawing.Point(706, 18);
            this.c1Label3.Name = "c1Label3";
            this.c1Label3.Size = new System.Drawing.Size(55, 13);
            this.c1Label3.TabIndex = 31;
            this.c1Label3.Tag = null;
            this.c1Label3.Text = "System:";
            this.c1Label3.TextDetached = true;
            this.c1Label3.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // ShortSaleNegativeRebateBillingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.c1CommandHolder1.SetC1ContextMenu(this, this.MainContextMenu);
            this.ClientSize = new System.Drawing.Size(1308, 567);
            this.Controls.Add(this.c1Label3);
            this.Controls.Add(this.c1Label2);
            this.Controls.Add(this.c1Label1);
            this.Controls.Add(this.BroadRidgeRadio);
            this.Controls.Add(this.PensonRadio);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.SummaryGrid);
            this.Controls.Add(this.RebateSummaryGrid);
            this.Controls.Add(this.FromLabel);
            this.Controls.Add(this.FromDatePicker);
            this.Controls.Add(this.ToDatePicker);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShortSaleNegativeRebateBillingForm";
            this.Padding = new System.Windows.Forms.Padding(0, 40, 0, 0);
            this.Text = "Hard To Borrow Billing";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShortSaleNegativeRebateBillingForm_FormClosed);
            this.Load += new System.EventHandler(this.ShortSaleNegativeRebateBillingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SummaryGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RebateSummaryGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FromLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RefreshButton;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid SummaryGrid;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid RebateSummaryGrid;
        private C1.Win.C1Input.C1Label FromLabel;
        private System.Windows.Forms.DateTimePicker FromDatePicker;
        private System.Windows.Forms.DateTimePicker ToDatePicker;
        private System.Windows.Forms.RadioButton PensonRadio;
        private System.Windows.Forms.RadioButton BroadRidgeRadio;
        private C1.Win.C1Command.C1ContextMenu MainContextMenu;
        private C1.Win.C1Command.C1CommandLink c1CommandLink2;
        private C1.Win.C1Command.C1Command CreateBillingCommand;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        private C1.Win.C1Command.C1CommandLink c1CommandLink1;
        private C1.Win.C1Command.C1CommandMenu SendToCommand;
        private C1.Win.C1Command.C1CommandLink c1CommandLink3;
        private C1.Win.C1Command.C1Command SendToExcelCommand;
        private C1.Win.C1Input.C1Label c1Label2;
        private C1.Win.C1Input.C1Label c1Label1;
        private C1.Win.C1Input.C1Label c1Label3;
        private C1.Win.C1Command.C1Command mnuSetMarkups;
        private C1.Win.C1Command.C1CommandLink c1CommandLink4;
        private C1.Win.C1Command.C1CommandMenu ActionsCommand;
        private C1.Win.C1Command.C1CommandLink c1CommandLink6;
        private C1.Win.C1Command.C1Command c1CommandControl1;
    }
}