namespace Golden
{
    partial class ShortSaleNegativeRebateBillingReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortSaleNegativeRebateBillingReportForm));
            this.GroupCodeCombo = new C1.Win.C1List.C1Combo();
            this.FromDatePicker = new System.Windows.Forms.DateTimePicker();
            this.ToDatePicker = new System.Windows.Forms.DateTimePicker();
            this.MessageListBox = new System.Windows.Forms.ListBox();
            this.CreateBillButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.BroadRidgeRadio = new System.Windows.Forms.RadioButton();
            this.PensonRadio = new System.Windows.Forms.RadioButton();
            this.c1Label1 = new C1.Win.C1Input.C1Label();
            this.c1Label2 = new C1.Win.C1Input.C1Label();
            this.c1Label3 = new C1.Win.C1Input.C1Label();
            this.c1Label4 = new C1.Win.C1Input.C1Label();
            this.c1Label5 = new C1.Win.C1Input.C1Label();
            this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.ExcelRadio = new System.Windows.Forms.RadioButton();
            this.PdfRadio = new System.Windows.Forms.RadioButton();
            this.ExcludeMasterBillCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.GroupCodeCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label5)).BeginInit();
            this.OptionsGroupBox.SuspendLayout();
            this.SuspendLayout();
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
            this.GroupCodeCombo.Location = new System.Drawing.Point(124, 71);
            this.GroupCodeCombo.MatchEntryTimeout = ((long)(2000));
            this.GroupCodeCombo.MaxDropDownItems = ((short)(5));
            this.GroupCodeCombo.MaxLength = 32767;
            this.GroupCodeCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.GroupCodeCombo.Name = "GroupCodeCombo";
            this.GroupCodeCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.GroupCodeCombo.Size = new System.Drawing.Size(261, 22);
            this.GroupCodeCombo.TabIndex = 91;
            this.GroupCodeCombo.VisualStyle = C1.Win.C1List.VisualStyle.System;
            this.GroupCodeCombo.PropBag = resources.GetString("GroupCodeCombo.PropBag");
            // 
            // FromDatePicker
            // 
            this.FromDatePicker.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromDatePicker.Location = new System.Drawing.Point(124, 14);
            this.FromDatePicker.Name = "FromDatePicker";
            this.FromDatePicker.Size = new System.Drawing.Size(261, 21);
            this.FromDatePicker.TabIndex = 87;
            this.FromDatePicker.ValueChanged += new System.EventHandler(this.FromDatePicker_ValueChanged);
            // 
            // ToDatePicker
            // 
            this.ToDatePicker.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToDatePicker.Location = new System.Drawing.Point(124, 42);
            this.ToDatePicker.Name = "ToDatePicker";
            this.ToDatePicker.Size = new System.Drawing.Size(261, 21);
            this.ToDatePicker.TabIndex = 89;
            this.ToDatePicker.ValueChanged += new System.EventHandler(this.ToDatePicker_ValueChanged);
            // 
            // MessageListBox
            // 
            this.MessageListBox.HorizontalScrollbar = true;
            this.MessageListBox.Location = new System.Drawing.Point(124, 151);
            this.MessageListBox.Name = "MessageListBox";
            this.MessageListBox.Size = new System.Drawing.Size(452, 108);
            this.MessageListBox.TabIndex = 100;
            // 
            // CreateBillButton
            // 
            this.CreateBillButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CreateBillButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateBillButton.Location = new System.Drawing.Point(124, 271);
            this.CreateBillButton.Name = "CreateBillButton";
            this.CreateBillButton.Size = new System.Drawing.Size(98, 24);
            this.CreateBillButton.TabIndex = 101;
            this.CreateBillButton.Text = "Create &Bill";
            this.CreateBillButton.UseVisualStyleBackColor = false;
            this.CreateBillButton.Click += new System.EventHandler(this.CreateBillButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CloseButton.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(478, 271);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(98, 24);
            this.CloseButton.TabIndex = 102;
            this.CloseButton.Text = "&Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // BroadRidgeRadio
            // 
            this.BroadRidgeRadio.AutoSize = true;
            this.BroadRidgeRadio.Location = new System.Drawing.Point(231, 105);
            this.BroadRidgeRadio.Name = "BroadRidgeRadio";
            this.BroadRidgeRadio.Size = new System.Drawing.Size(91, 17);
            this.BroadRidgeRadio.TabIndex = 94;
            this.BroadRidgeRadio.Text = "BroadRidge";
            this.BroadRidgeRadio.UseVisualStyleBackColor = true;
            this.BroadRidgeRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // PensonRadio
            // 
            this.PensonRadio.AutoSize = true;
            this.PensonRadio.Checked = true;
            this.PensonRadio.Location = new System.Drawing.Point(124, 105);
            this.PensonRadio.Name = "PensonRadio";
            this.PensonRadio.Size = new System.Drawing.Size(66, 17);
            this.PensonRadio.TabIndex = 93;
            this.PensonRadio.TabStop = true;
            this.PensonRadio.Text = "Penson";
            this.PensonRadio.UseVisualStyleBackColor = true;
            this.PensonRadio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // c1Label1
            // 
            this.c1Label1.AutoSize = true;
            this.c1Label1.Location = new System.Drawing.Point(60, 18);
            this.c1Label1.Name = "c1Label1";
            this.c1Label1.Size = new System.Drawing.Size(41, 13);
            this.c1Label1.TabIndex = 86;
            this.c1Label1.Tag = null;
            this.c1Label1.Text = "From:";
            this.c1Label1.TextDetached = true;
            this.c1Label1.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // c1Label2
            // 
            this.c1Label2.AutoSize = true;
            this.c1Label2.Location = new System.Drawing.Point(75, 46);
            this.c1Label2.Name = "c1Label2";
            this.c1Label2.Size = new System.Drawing.Size(26, 13);
            this.c1Label2.TabIndex = 88;
            this.c1Label2.Tag = null;
            this.c1Label2.Text = "To:";
            this.c1Label2.TextDetached = true;
            this.c1Label2.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // c1Label3
            // 
            this.c1Label3.AutoSize = true;
            this.c1Label3.Location = new System.Drawing.Point(20, 75);
            this.c1Label3.Name = "c1Label3";
            this.c1Label3.Size = new System.Drawing.Size(81, 13);
            this.c1Label3.TabIndex = 90;
            this.c1Label3.Tag = null;
            this.c1Label3.Text = "Group Code:";
            this.c1Label3.TextDetached = true;
            this.c1Label3.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // c1Label4
            // 
            this.c1Label4.AutoSize = true;
            this.c1Label4.Location = new System.Drawing.Point(53, 151);
            this.c1Label4.Name = "c1Label4";
            this.c1Label4.Size = new System.Drawing.Size(48, 13);
            this.c1Label4.TabIndex = 99;
            this.c1Label4.Tag = null;
            this.c1Label4.Text = "Status:";
            this.c1Label4.TextDetached = true;
            this.c1Label4.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // c1Label5
            // 
            this.c1Label5.AutoSize = true;
            this.c1Label5.Location = new System.Drawing.Point(46, 107);
            this.c1Label5.Name = "c1Label5";
            this.c1Label5.Size = new System.Drawing.Size(55, 13);
            this.c1Label5.TabIndex = 92;
            this.c1Label5.Tag = null;
            this.c1Label5.Text = "System:";
            this.c1Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.c1Label5.TextDetached = true;
            this.c1Label5.VisualStyle = C1.Win.C1Input.VisualStyle.System;
            // 
            // OptionsGroupBox
            // 
            this.OptionsGroupBox.Controls.Add(this.ExcelRadio);
            this.OptionsGroupBox.Controls.Add(this.PdfRadio);
            this.OptionsGroupBox.Controls.Add(this.ExcludeMasterBillCheckBox);
            this.OptionsGroupBox.Location = new System.Drawing.Point(410, 12);
            this.OptionsGroupBox.Name = "OptionsGroupBox";
            this.OptionsGroupBox.Size = new System.Drawing.Size(166, 121);
            this.OptionsGroupBox.TabIndex = 95;
            this.OptionsGroupBox.TabStop = false;
            this.OptionsGroupBox.Text = "Options";
            // 
            // ExcelRadio
            // 
            this.ExcelRadio.AutoSize = true;
            this.ExcelRadio.Location = new System.Drawing.Point(89, 89);
            this.ExcelRadio.Name = "ExcelRadio";
            this.ExcelRadio.Size = new System.Drawing.Size(55, 17);
            this.ExcelRadio.TabIndex = 98;
            this.ExcelRadio.Text = "Excel";
            this.ExcelRadio.UseVisualStyleBackColor = true;
            // 
            // PdfRadio
            // 
            this.PdfRadio.AutoSize = true;
            this.PdfRadio.Checked = true;
            this.PdfRadio.Location = new System.Drawing.Point(11, 89);
            this.PdfRadio.Name = "PdfRadio";
            this.PdfRadio.Size = new System.Drawing.Size(43, 17);
            this.PdfRadio.TabIndex = 97;
            this.PdfRadio.TabStop = true;
            this.PdfRadio.Text = "Pdf";
            this.PdfRadio.UseVisualStyleBackColor = true;
            // 
            // ExcludeMasterBillCheckBox
            // 
            this.ExcludeMasterBillCheckBox.AutoSize = true;
            this.ExcludeMasterBillCheckBox.Location = new System.Drawing.Point(11, 34);
            this.ExcludeMasterBillCheckBox.Name = "ExcludeMasterBillCheckBox";
            this.ExcludeMasterBillCheckBox.Size = new System.Drawing.Size(133, 17);
            this.ExcludeMasterBillCheckBox.TabIndex = 96;
            this.ExcludeMasterBillCheckBox.Text = "Exclude Master Bill";
            this.ExcludeMasterBillCheckBox.UseVisualStyleBackColor = true;
            // 
            // ShortSaleNegativeRebateBillingReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 322);
            this.Controls.Add(this.OptionsGroupBox);
            this.Controls.Add(this.c1Label5);
            this.Controls.Add(this.c1Label4);
            this.Controls.Add(this.c1Label3);
            this.Controls.Add(this.c1Label2);
            this.Controls.Add(this.c1Label1);
            this.Controls.Add(this.BroadRidgeRadio);
            this.Controls.Add(this.PensonRadio);
            this.Controls.Add(this.GroupCodeCombo);
            this.Controls.Add(this.FromDatePicker);
            this.Controls.Add(this.ToDatePicker);
            this.Controls.Add(this.MessageListBox);
            this.Controls.Add(this.CreateBillButton);
            this.Controls.Add(this.CloseButton);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShortSaleNegativeRebateBillingReportForm";
            this.Text = "Report Creation";
            this.Load += new System.EventHandler(this.ShortSaleNegativeRebateBillingReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GroupCodeCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label5)).EndInit();
            this.OptionsGroupBox.ResumeLayout(false);
            this.OptionsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1List.C1Combo GroupCodeCombo;
        private System.Windows.Forms.DateTimePicker FromDatePicker;
        private System.Windows.Forms.DateTimePicker ToDatePicker;
        private System.Windows.Forms.ListBox MessageListBox;
        private System.Windows.Forms.Button CreateBillButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.RadioButton BroadRidgeRadio;
        private System.Windows.Forms.RadioButton PensonRadio;
        private C1.Win.C1Input.C1Label c1Label1;
        private C1.Win.C1Input.C1Label c1Label2;
        private C1.Win.C1Input.C1Label c1Label3;
        private C1.Win.C1Input.C1Label c1Label4;
        private C1.Win.C1Input.C1Label c1Label5;
		private System.Windows.Forms.GroupBox OptionsGroupBox;
		private System.Windows.Forms.CheckBox ExcludeMasterBillCheckBox;
		private System.Windows.Forms.RadioButton ExcelRadio;
		private System.Windows.Forms.RadioButton PdfRadio;
    }
}