namespace CentralClient
{
    partial class TradingBookCreditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradingBookCreditForm));
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.panel12 = new System.Windows.Forms.Panel();
            this.DateTimePicker = new C1.Win.C1Input.C1DateEdit();
            this.BookGroupCombo = new C1.Win.C1List.C1Combo();
            this.BizDateLabel = new System.Windows.Forms.Label();
            this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.CreditGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DateTimePicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CreditGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.Name = "c1StatusBar1";
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.Transparent;
            this.panel12.Controls.Add(this.DateTimePicker);
            this.panel12.Controls.Add(this.BookGroupCombo);
            this.panel12.Controls.Add(this.BizDateLabel);
            this.panel12.Controls.Add(this.BookGroupNameLabel);
            this.panel12.Controls.Add(this.label1);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(1024, 28);
            this.panel12.TabIndex = 106;
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(187)))), ((int)(((byte)(214)))));
            this.DateTimePicker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.DateTimePicker.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.DateTimePicker.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DateTimePicker.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.DateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.DateTimePicker.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.DateTimePicker.Location = new System.Drawing.Point(112, 6);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(120, 19);
            this.DateTimePicker.TabIndex = 113;
            this.DateTimePicker.Tag = null;
            this.DateTimePicker.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.DateTimePicker.TextChanged += new System.EventHandler(this.DateTimePicker_TextChanged);
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
            this.BookGroupCombo.Location = new System.Drawing.Point(357, 6);
            this.BookGroupCombo.MatchEntryTimeout = ((long)(2000));
            this.BookGroupCombo.MaxDropDownItems = ((short)(5));
            this.BookGroupCombo.MaxLength = 32767;
            this.BookGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.BookGroupCombo.Name = "BookGroupCombo";
            this.BookGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.BookGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.BookGroupCombo.Size = new System.Drawing.Size(122, 19);
            this.BookGroupCombo.TabIndex = 113;
            this.BookGroupCombo.VisualStyle = C1.Win.C1List.VisualStyle.Office2007Blue;
            this.BookGroupCombo.TextChanged += new System.EventHandler(this.BookGroupCombo_TextChanged);
            this.BookGroupCombo.PropBag = resources.GetString("BookGroupCombo.PropBag");
            // 
            // BizDateLabel
            // 
            this.BizDateLabel.AutoSize = true;
            this.BizDateLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.BizDateLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BizDateLabel.ForeColor = System.Drawing.Color.Black;
            this.BizDateLabel.Location = new System.Drawing.Point(14, 9);
            this.BizDateLabel.Name = "BizDateLabel";
            this.BizDateLabel.Size = new System.Drawing.Size(93, 13);
            this.BizDateLabel.TabIndex = 112;
            this.BizDateLabel.Text = "Business Date:";
            this.BizDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BookGroupNameLabel
            // 
            this.BookGroupNameLabel.AutoSize = true;
            this.BookGroupNameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.BookGroupNameLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookGroupNameLabel.ForeColor = System.Drawing.Color.Black;
            this.BookGroupNameLabel.Location = new System.Drawing.Point(487, 9);
            this.BookGroupNameLabel.Name = "BookGroupNameLabel";
            this.BookGroupNameLabel.Size = new System.Drawing.Size(134, 13);
            this.BookGroupNameLabel.TabIndex = 103;
            this.BookGroupNameLabel.Tag = null;
            this.BookGroupNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BookGroupNameLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(264, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Book Group:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 28);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1024, 3);
            this.splitter1.TabIndex = 107;
            this.splitter1.TabStop = false;
            // 
            // CreditGrid
            // 
            this.CreditGrid.CaptionHeight = 17;
            this.CreditGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.CreditGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CreditGrid.EmptyRows = true;
            this.CreditGrid.ExtendRightColumn = true;
            this.CreditGrid.FilterBar = true;
            this.CreditGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.CreditGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("CreditGrid.Images"))));
            this.CreditGrid.Location = new System.Drawing.Point(0, 31);
            this.CreditGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
            this.CreditGrid.Name = "CreditGrid";
            this.CreditGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.CreditGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.CreditGrid.PreviewInfo.ZoomFactor = 75D;
            this.CreditGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("CreditGrid.PrintInfo.PageSettings")));
            this.CreditGrid.RecordSelectors = false;
            this.CreditGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.CreditGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.CreditGrid.RowHeight = 15;
            this.CreditGrid.Size = new System.Drawing.Size(1024, 660);
            this.CreditGrid.TabIndex = 108;
            this.CreditGrid.Text = "CreditGrid";
            this.CreditGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.CreditGrid_FormatText);
            this.CreditGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CreditGrid_KeyPress);
            this.CreditGrid.PropBag = resources.GetString("CreditGrid.PropBag");
            // 
            // TradingBookCreditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 714);
            this.Controls.Add(this.CreditGrid);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.c1StatusBar1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TradingBookCreditForm";
            this.Text = "Admin - Book Credit";
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2010Blue;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TradingBookCreditForm_FormClosed);
            this.Load += new System.EventHandler(this.AdminBookCreditForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TradingBookCreditForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DateTimePicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CreditGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private System.Windows.Forms.Panel panel12;
        private C1.Win.C1List.C1Combo BookGroupCombo;
        private C1.Win.C1Input.C1Label BookGroupNameLabel;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Input.C1DateEdit DateTimePicker;
        private System.Windows.Forms.Label BizDateLabel;
        private System.Windows.Forms.Splitter splitter1;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid CreditGrid;
    }
}