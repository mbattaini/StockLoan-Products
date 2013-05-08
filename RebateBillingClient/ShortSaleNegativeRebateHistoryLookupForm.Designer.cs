namespace Golden
{
    partial class ShortSaleNegativeRebateHistoryLookupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortSaleNegativeRebateHistoryLookupForm));
            this.c1Label3 = new C1.Win.C1Input.C1Label();
            this.c1Label2 = new C1.Win.C1Input.C1Label();
            this.c1Label1 = new C1.Win.C1Input.C1Label();
            this.BroadRidgeRadio = new System.Windows.Forms.RadioButton();
            this.PensonRadio = new System.Windows.Forms.RadioButton();
            this.FromDatePicker = new System.Windows.Forms.DateTimePicker();
            this.ToDatePicker = new System.Windows.Forms.DateTimePicker();
            this.SummaryGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.AccountNumberTextBox = new C1.Win.C1Input.C1TextBox();
            this.c1Label4 = new C1.Win.C1Input.C1Label();
            this.MainContextMenu = new C1.Win.C1Command.C1ContextMenu();
            this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
            this.SendToCommand = new C1.Win.C1Command.C1CommandMenu();
            this.c1CommandLink3 = new C1.Win.C1Command.C1CommandLink();
            this.SendToExcelCommand = new C1.Win.C1Command.C1Command();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            this.CreateBillingCommand = new C1.Win.C1Command.C1Command();
            this.c1Label5 = new C1.Win.C1Input.C1Label();
            this.GroupCodeCombo = new C1.Win.C1List.C1Combo();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RadioByGroupCode = new System.Windows.Forms.RadioButton();
            this.RadioByAccountNumber = new System.Windows.Forms.RadioButton();
            this.RadioBySecurity = new System.Windows.Forms.RadioButton();
            this.RadioDetail = new System.Windows.Forms.RadioButton();
            this.LookupButton = new C1.Win.C1Input.C1Button();
            this.RadioByCharges = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SummaryGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountNumberTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupCodeCombo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c1Label3
            // 
            this.c1Label3.AutoSize = true;
            this.c1Label3.Location = new System.Drawing.Point(324, 12);
            this.c1Label3.Name = "c1Label3";
            this.c1Label3.Size = new System.Drawing.Size(55, 13);
            this.c1Label3.TabIndex = 38;
            this.c1Label3.Tag = null;
            this.c1Label3.Text = "System:";
            this.c1Label3.TextDetached = true;
            this.c1Label3.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // c1Label2
            // 
            this.c1Label2.AutoSize = true;
            this.c1Label2.Location = new System.Drawing.Point(24, 35);
            this.c1Label2.Name = "c1Label2";
            this.c1Label2.Size = new System.Drawing.Size(26, 13);
            this.c1Label2.TabIndex = 37;
            this.c1Label2.Tag = null;
            this.c1Label2.Text = "To:";
            this.c1Label2.TextDetached = true;
            this.c1Label2.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // c1Label1
            // 
            this.c1Label1.AutoSize = true;
            this.c1Label1.Location = new System.Drawing.Point(9, 11);
            this.c1Label1.Name = "c1Label1";
            this.c1Label1.Size = new System.Drawing.Size(41, 13);
            this.c1Label1.TabIndex = 36;
            this.c1Label1.Tag = null;
            this.c1Label1.Text = "From:";
            this.c1Label1.TextDetached = true;
            this.c1Label1.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // BroadRidgeRadio
            // 
            this.BroadRidgeRadio.AutoSize = true;
            this.BroadRidgeRadio.Location = new System.Drawing.Point(457, 10);
            this.BroadRidgeRadio.Name = "BroadRidgeRadio";
            this.BroadRidgeRadio.Size = new System.Drawing.Size(91, 17);
            this.BroadRidgeRadio.TabIndex = 35;
            this.BroadRidgeRadio.Text = "BroadRidge";
            this.BroadRidgeRadio.UseVisualStyleBackColor = true;
            this.BroadRidgeRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // PensonRadio
            // 
            this.PensonRadio.AutoSize = true;
            this.PensonRadio.Checked = true;
            this.PensonRadio.Location = new System.Drawing.Point(385, 10);
            this.PensonRadio.Name = "PensonRadio";
            this.PensonRadio.Size = new System.Drawing.Size(66, 17);
            this.PensonRadio.TabIndex = 34;
            this.PensonRadio.TabStop = true;
            this.PensonRadio.Text = "Penson";
            this.PensonRadio.UseVisualStyleBackColor = true;
            this.PensonRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // FromDatePicker
            // 
            this.FromDatePicker.Location = new System.Drawing.Point(56, 7);
            this.FromDatePicker.Name = "FromDatePicker";
            this.FromDatePicker.Size = new System.Drawing.Size(245, 21);
            this.FromDatePicker.TabIndex = 33;
            this.FromDatePicker.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // ToDatePicker
            // 
            this.ToDatePicker.Location = new System.Drawing.Point(56, 31);
            this.ToDatePicker.Name = "ToDatePicker";
            this.ToDatePicker.Size = new System.Drawing.Size(245, 21);
            this.ToDatePicker.TabIndex = 32;
            this.ToDatePicker.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // SummaryGrid
            // 
            this.SummaryGrid.AllowColSelect = false;
            this.SummaryGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
            this.SummaryGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c1CommandHolder1.SetC1ContextMenu(this.SummaryGrid, this.MainContextMenu);
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
            this.SummaryGrid.Location = new System.Drawing.Point(3, 88);
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
            this.SummaryGrid.Size = new System.Drawing.Size(1181, 529);
            this.SummaryGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
            this.SummaryGrid.TabIndex = 39;
            this.SummaryGrid.TabStop = false;
            this.SummaryGrid.WrapCellPointer = true;
            this.SummaryGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.SummaryGrid_FormatText);
            this.SummaryGrid.PropBag = resources.GetString("SummaryGrid.PropBag");
            // 
            // AccountNumberTextBox
            // 
            this.AccountNumberTextBox.Location = new System.Drawing.Point(967, 31);
            this.AccountNumberTextBox.Name = "AccountNumberTextBox";
            this.AccountNumberTextBox.Size = new System.Drawing.Size(201, 21);
            this.AccountNumberTextBox.TabIndex = 40;
            this.AccountNumberTextBox.Tag = null;
            this.AccountNumberTextBox.TextDetached = true;
            // 
            // c1Label4
            // 
            this.c1Label4.AutoSize = true;
            this.c1Label4.Location = new System.Drawing.Point(876, 36);
            this.c1Label4.Name = "c1Label4";
            this.c1Label4.Size = new System.Drawing.Size(85, 13);
            this.c1Label4.TabIndex = 41;
            this.c1Label4.Tag = null;
            this.c1Label4.Text = "Acct Number:";
            this.c1Label4.TextDetached = true;
            this.c1Label4.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // MainContextMenu
            // 
            this.MainContextMenu.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1});
            this.MainContextMenu.Name = "MainContextMenu";
            this.MainContextMenu.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
            // 
            // c1CommandLink1
            // 
            this.c1CommandLink1.Command = this.SendToCommand;
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
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Commands.Add(this.MainContextMenu);
            this.c1CommandHolder1.Commands.Add(this.CreateBillingCommand);
            this.c1CommandHolder1.Commands.Add(this.SendToCommand);
            this.c1CommandHolder1.Commands.Add(this.SendToExcelCommand);
            this.c1CommandHolder1.Owner = this;
            // 
            // CreateBillingCommand
            // 
            this.CreateBillingCommand.Name = "CreateBillingCommand";
            this.CreateBillingCommand.Text = "Create Bills";
            // 
            // c1Label5
            // 
            this.c1Label5.AutoSize = true;
            this.c1Label5.Location = new System.Drawing.Point(880, 12);
            this.c1Label5.Name = "c1Label5";
            this.c1Label5.Size = new System.Drawing.Size(81, 13);
            this.c1Label5.TabIndex = 99;
            this.c1Label5.Tag = null;
            this.c1Label5.Text = "Group Code:";
            this.c1Label5.TextDetached = true;
            this.c1Label5.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // GroupCodeCombo
            // 
            this.GroupCodeCombo.AddItemSeparator = ';';
            this.GroupCodeCombo.Caption = "";
            this.GroupCodeCombo.CaptionHeight = 17;
            this.GroupCodeCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.GroupCodeCombo.ColumnCaptionHeight = 17;
            this.GroupCodeCombo.ColumnFooterHeight = 17;
            this.GroupCodeCombo.ColumnWidth = 100;
            this.GroupCodeCombo.ContentHeight = 16;
            this.GroupCodeCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.GroupCodeCombo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown;
            this.GroupCodeCombo.DropDownWidth = 300;
            this.GroupCodeCombo.EditorBackColor = System.Drawing.SystemColors.Window;
            this.GroupCodeCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupCodeCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.GroupCodeCombo.EditorHeight = 16;
            this.GroupCodeCombo.ExtendRightColumn = true;
            this.GroupCodeCombo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupCodeCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("GroupCodeCombo.Images"))));
            this.GroupCodeCombo.ItemHeight = 15;
            this.GroupCodeCombo.Location = new System.Drawing.Point(967, 7);
            this.GroupCodeCombo.MatchEntryTimeout = ((long)(2000));
            this.GroupCodeCombo.MaxDropDownItems = ((short)(5));
            this.GroupCodeCombo.MaxLength = 32767;
            this.GroupCodeCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.GroupCodeCombo.Name = "GroupCodeCombo";
            this.GroupCodeCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.GroupCodeCombo.Size = new System.Drawing.Size(201, 22);
            this.GroupCodeCombo.TabIndex = 98;
            this.GroupCodeCombo.VisualStyle = C1.Win.C1List.VisualStyle.System;
            this.GroupCodeCombo.PropBag = resources.GetString("GroupCodeCombo.PropBag");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RadioByCharges);
            this.groupBox1.Controls.Add(this.RadioByGroupCode);
            this.groupBox1.Controls.Add(this.RadioByAccountNumber);
            this.groupBox1.Controls.Add(this.RadioBySecurity);
            this.groupBox1.Controls.Add(this.RadioDetail);
            this.groupBox1.Location = new System.Drawing.Point(328, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 44);
            this.groupBox1.TabIndex = 100;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Options";
            // 
            // RadioByGroupCode
            // 
            this.RadioByGroupCode.AutoSize = true;
            this.RadioByGroupCode.Location = new System.Drawing.Point(293, 20);
            this.RadioByGroupCode.Name = "RadioByGroupCode";
            this.RadioByGroupCode.Size = new System.Drawing.Size(113, 17);
            this.RadioByGroupCode.TabIndex = 3;
            this.RadioByGroupCode.TabStop = true;
            this.RadioByGroupCode.Text = "By Group Code";
            this.RadioByGroupCode.UseVisualStyleBackColor = true;
            this.RadioByGroupCode.CheckedChanged += new System.EventHandler(this.RadioByGroupCode_CheckedChanged);
            // 
            // RadioByAccountNumber
            // 
            this.RadioByAccountNumber.AutoSize = true;
            this.RadioByAccountNumber.Location = new System.Drawing.Point(170, 20);
            this.RadioByAccountNumber.Name = "RadioByAccountNumber";
            this.RadioByAccountNumber.Size = new System.Drawing.Size(117, 17);
            this.RadioByAccountNumber.TabIndex = 2;
            this.RadioByAccountNumber.TabStop = true;
            this.RadioByAccountNumber.Text = "By Acct Number";
            this.RadioByAccountNumber.UseVisualStyleBackColor = true;
            this.RadioByAccountNumber.CheckedChanged += new System.EventHandler(this.RadioByAccountNumber_CheckedChanged);
            // 
            // RadioBySecurity
            // 
            this.RadioBySecurity.AutoSize = true;
            this.RadioBySecurity.Location = new System.Drawing.Point(73, 20);
            this.RadioBySecurity.Name = "RadioBySecurity";
            this.RadioBySecurity.Size = new System.Drawing.Size(91, 17);
            this.RadioBySecurity.TabIndex = 1;
            this.RadioBySecurity.TabStop = true;
            this.RadioBySecurity.Text = "By Seucrity";
            this.RadioBySecurity.UseVisualStyleBackColor = true;
            this.RadioBySecurity.CheckedChanged += new System.EventHandler(this.RadioBySecurity_CheckedChanged);
            // 
            // RadioDetail
            // 
            this.RadioDetail.AutoSize = true;
            this.RadioDetail.Location = new System.Drawing.Point(6, 20);
            this.RadioDetail.Name = "RadioDetail";
            this.RadioDetail.Size = new System.Drawing.Size(58, 17);
            this.RadioDetail.TabIndex = 0;
            this.RadioDetail.TabStop = true;
            this.RadioDetail.Text = "Detail";
            this.RadioDetail.UseVisualStyleBackColor = true;
            this.RadioDetail.CheckedChanged += new System.EventHandler(this.RadioDetail_CheckedChanged);
            // 
            // LookupButton
            // 
            this.LookupButton.Location = new System.Drawing.Point(1087, 58);
            this.LookupButton.Name = "LookupButton";
            this.LookupButton.Size = new System.Drawing.Size(81, 20);
            this.LookupButton.TabIndex = 101;
            this.LookupButton.Text = "Look Up";
            this.LookupButton.UseVisualStyleBackColor = true;
            this.LookupButton.Click += new System.EventHandler(this.LookupButton_Click);
            // 
            // RadioByCharges
            // 
            this.RadioByCharges.AutoSize = true;
            this.RadioByCharges.Location = new System.Drawing.Point(412, 20);
            this.RadioByCharges.Name = "RadioByCharges";
            this.RadioByCharges.Size = new System.Drawing.Size(92, 17);
            this.RadioByCharges.TabIndex = 4;
            this.RadioByCharges.TabStop = true;
            this.RadioByCharges.Text = "By Charges";
            this.RadioByCharges.UseVisualStyleBackColor = true;
            this.RadioByCharges.CheckedChanged += new System.EventHandler(this.RadioByCharges_CheckedChanged);
            // 
            // ShortSaleNegativeRebateHistoryLookupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 618);
            this.Controls.Add(this.LookupButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.c1Label5);
            this.Controls.Add(this.GroupCodeCombo);
            this.Controls.Add(this.c1Label4);
            this.Controls.Add(this.AccountNumberTextBox);
            this.Controls.Add(this.SummaryGrid);
            this.Controls.Add(this.c1Label3);
            this.Controls.Add(this.c1Label2);
            this.Controls.Add(this.c1Label1);
            this.Controls.Add(this.BroadRidgeRadio);
            this.Controls.Add(this.PensonRadio);
            this.Controls.Add(this.FromDatePicker);
            this.Controls.Add(this.ToDatePicker);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShortSaleNegativeRebateHistoryLookupForm";
            this.Padding = new System.Windows.Forms.Padding(0, 80, 0, 0);
            this.Text = "Hard To Borrow Lookup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShortSaleNegativeRebateHistoryLookupForm_FormClosing);
            this.Load += new System.EventHandler(this.ShortSaleNegativeRebateHistoryLookupForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SummaryGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountNumberTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupCodeCombo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Input.C1Label c1Label3;
        private C1.Win.C1Input.C1Label c1Label2;
        private C1.Win.C1Input.C1Label c1Label1;
        private System.Windows.Forms.RadioButton BroadRidgeRadio;
        private System.Windows.Forms.RadioButton PensonRadio;
        private System.Windows.Forms.DateTimePicker FromDatePicker;
        private System.Windows.Forms.DateTimePicker ToDatePicker;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid SummaryGrid;
        private C1.Win.C1Input.C1TextBox AccountNumberTextBox;
        private C1.Win.C1Input.C1Label c1Label4;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        private C1.Win.C1Command.C1ContextMenu MainContextMenu;
        private C1.Win.C1Command.C1Command CreateBillingCommand;
        private C1.Win.C1Command.C1CommandLink c1CommandLink1;
        private C1.Win.C1Command.C1CommandMenu SendToCommand;
        private C1.Win.C1Command.C1CommandLink c1CommandLink3;
        private C1.Win.C1Command.C1Command SendToExcelCommand;
		private C1.Win.C1Input.C1Label c1Label5;
		private C1.Win.C1List.C1Combo GroupCodeCombo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RadioByGroupCode;
        private System.Windows.Forms.RadioButton RadioByAccountNumber;
        private System.Windows.Forms.RadioButton RadioBySecurity;
        private System.Windows.Forms.RadioButton RadioDetail;
        private C1.Win.C1Input.C1Button LookupButton;
        private System.Windows.Forms.RadioButton RadioByCharges;
    }
}