namespace CentralClient
{
	partial class AnalysisContractSummarySecurityForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalysisContractSummarySecurityForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.BookGroupCombo = new C1.Win.C1List.C1Combo();
			this.DateTimePicker = new C1.Win.C1Input.C1DateEdit();
			this.BizDateLabel = new System.Windows.Forms.Label();
			this.BookGroupNameLabel = new C1.Win.C1Input.C1Label();
			this.label1 = new System.Windows.Forms.Label();
			this.GenerateButton = new C1.Win.C1Input.C1Button();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DateTimePicker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.panel1.Controls.Add(this.GenerateButton);
			this.panel1.Controls.Add(this.BookGroupCombo);
			this.panel1.Controls.Add(this.DateTimePicker);
			this.panel1.Controls.Add(this.BizDateLabel);
			this.panel1.Controls.Add(this.BookGroupNameLabel);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(737, 35);
			this.panel1.TabIndex = 2;
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
			this.BookGroupCombo.Location = new System.Drawing.Point(337, 6);
			this.BookGroupCombo.MatchEntryTimeout = ((long)(2000));
			this.BookGroupCombo.MaxDropDownItems = ((short)(5));
			this.BookGroupCombo.MaxLength = 32767;
			this.BookGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
			this.BookGroupCombo.Name = "BookGroupCombo";
			this.BookGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
			this.BookGroupCombo.Size = new System.Drawing.Size(122, 19);
			this.BookGroupCombo.TabIndex = 112;
			this.BookGroupCombo.PropBag = resources.GetString("BookGroupCombo.PropBag");
			// 
			// DateTimePicker
			// 
			this.DateTimePicker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			// 
			// 
			// 
			this.DateTimePicker.Calendar.DayNameLength = 2;
			this.DateTimePicker.Calendar.DayNamesFont = new System.Drawing.Font("Tahoma", 8F);
			this.DateTimePicker.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
			this.DateTimePicker.Calendar.TitleFont = new System.Drawing.Font("Tahoma", 8F);
			this.DateTimePicker.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
			this.DateTimePicker.CustomFormat = "yyyy-MM-dd";
			this.DateTimePicker.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.DateTimePicker.Location = new System.Drawing.Point(80, 6);
			this.DateTimePicker.Name = "DateTimePicker";
			this.DateTimePicker.Size = new System.Drawing.Size(120, 19);
			this.DateTimePicker.TabIndex = 111;
			this.DateTimePicker.Tag = null;
			this.DateTimePicker.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
			// 
			// BizDateLabel
			// 
			this.BizDateLabel.AutoSize = true;
			this.BizDateLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.BizDateLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BizDateLabel.ForeColor = System.Drawing.Color.Black;
			this.BizDateLabel.Location = new System.Drawing.Point(3, 9);
			this.BizDateLabel.Name = "BizDateLabel";
			this.BizDateLabel.Size = new System.Drawing.Size(60, 13);
			this.BizDateLabel.TabIndex = 109;
			this.BizDateLabel.Text = "Biz Date:";
			this.BizDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// BookGroupNameLabel
			// 
			this.BookGroupNameLabel.AutoSize = true;
			this.BookGroupNameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.BookGroupNameLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BookGroupNameLabel.ForeColor = System.Drawing.Color.Black;
			this.BookGroupNameLabel.Location = new System.Drawing.Point(467, 9);
			this.BookGroupNameLabel.Name = "BookGroupNameLabel";
			this.BookGroupNameLabel.Size = new System.Drawing.Size(134, 13);
			this.BookGroupNameLabel.TabIndex = 108;
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
			this.label1.Location = new System.Drawing.Point(237, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 13);
			this.label1.TabIndex = 106;
			this.label1.Text = "Book Group:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GenerateButton
			// 
			this.GenerateButton.Location = new System.Drawing.Point(630, 4);
			this.GenerateButton.Name = "GenerateButton";
			this.GenerateButton.Size = new System.Drawing.Size(87, 23);
			this.GenerateButton.TabIndex = 129;
			this.GenerateButton.Text = "Generate";
			this.GenerateButton.UseVisualStyleBackColor = true;
			this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
			// 
			// AnalysisContractSummarySecurityForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(737, 35);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AnalysisContractSummarySecurityForm";
			this.Text = "Analysis - Contract Summary Security";
			this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Silver;
			this.Load += new System.EventHandler(this.AnalysisContractSummarySecurityForm_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupCombo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DateTimePicker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookGroupNameLabel)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private C1.Win.C1List.C1Combo BookGroupCombo;
		private C1.Win.C1Input.C1DateEdit DateTimePicker;
		private System.Windows.Forms.Label BizDateLabel;
		private C1.Win.C1Input.C1Label BookGroupNameLabel;
		private System.Windows.Forms.Label label1;
		private C1.Win.C1Input.C1Button GenerateButton;
	}
}