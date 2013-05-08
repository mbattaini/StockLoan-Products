namespace LocatesClient
{
	partial class LocatesTradingGroupsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocatesTradingGroupsForm));
			this.TradingGroupsParametersGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.TradingGroupsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.panel1 = new System.Windows.Forms.Panel();
			this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.DefaultAutoApprovalTextBox = new C1.Win.C1Input.C1TextBox();
			this.DefaultAutoApprovalLabel = new C1.Win.C1Input.C1Label();
			this.PremiumMaxTextBox = new C1.Win.C1Input.C1TextBox();
			this.PremiumMinTextBox = new C1.Win.C1Input.C1TextBox();
			this.DefaultPriceMinimumTextBox = new C1.Win.C1Input.C1TextBox();
			this.DefaultPremiumThresholdLabel = new C1.Win.C1Input.C1Label();
			this.DefaultPriceLabel = new C1.Win.C1Input.C1Label();
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupsParametersGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupsGrid)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DefaultAutoApprovalTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultAutoApprovalLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PremiumMaxTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PremiumMinTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultPriceMinimumTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultPremiumThresholdLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultPriceLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// TradingGroupsParametersGrid
			// 
			this.TradingGroupsParametersGrid.CaptionHeight = 17;
			this.TradingGroupsParametersGrid.EmptyRows = true;
			this.TradingGroupsParametersGrid.ExtendRightColumn = true;
			this.TradingGroupsParametersGrid.FilterBar = true;
			this.TradingGroupsParametersGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.TradingGroupsParametersGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("TradingGroupsParametersGrid.Images"))));
			this.TradingGroupsParametersGrid.Location = new System.Drawing.Point(93, 136);
			this.TradingGroupsParametersGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
			this.TradingGroupsParametersGrid.Name = "TradingGroupsParametersGrid";
			this.TradingGroupsParametersGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.TradingGroupsParametersGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.TradingGroupsParametersGrid.PreviewInfo.ZoomFactor = 75;
			this.TradingGroupsParametersGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("TradingGroupsParametersGrid.PrintInfo.PageSettings")));
			this.TradingGroupsParametersGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.TradingGroupsParametersGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.TradingGroupsParametersGrid.RowHeight = 15;
			this.TradingGroupsParametersGrid.Size = new System.Drawing.Size(1026, 56);
			this.TradingGroupsParametersGrid.TabIndex = 5;
			this.TradingGroupsParametersGrid.TabStop = false;
			this.TradingGroupsParametersGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.TradingGroupsParametersGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.TradingGroupsParametersGrid_BeforeUpdate);
			this.TradingGroupsParametersGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TradingGroupsParametersGrid_KeyPress);
			this.TradingGroupsParametersGrid.PropBag = resources.GetString("TradingGroupsParametersGrid.PropBag");
			// 
			// TradingGroupsGrid
			// 
			this.TradingGroupsGrid.AllowAddNew = true;
			this.TradingGroupsGrid.CaptionHeight = 17;
			this.TradingGroupsGrid.ChildGrid = this.TradingGroupsParametersGrid;
			this.TradingGroupsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TradingGroupsGrid.EmptyRows = true;
			this.TradingGroupsGrid.ExtendRightColumn = true;
			this.TradingGroupsGrid.FetchRowStyles = true;
			this.TradingGroupsGrid.FilterBar = true;
			this.TradingGroupsGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.TradingGroupsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("TradingGroupsGrid.Images"))));
			this.TradingGroupsGrid.Location = new System.Drawing.Point(0, 60);
			this.TradingGroupsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
			this.TradingGroupsGrid.Name = "TradingGroupsGrid";
			this.TradingGroupsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.TradingGroupsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.TradingGroupsGrid.PreviewInfo.ZoomFactor = 75;
			this.TradingGroupsGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("TradingGroupsGrid.PrintInfo.PageSettings")));
			this.TradingGroupsGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
			this.TradingGroupsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.TradingGroupsGrid.RowHeight = 15;
			this.TradingGroupsGrid.Size = new System.Drawing.Size(1200, 212);
			this.TradingGroupsGrid.TabIndex = 3;
			this.TradingGroupsGrid.Text = "c1TrueDBGrid1";
			this.TradingGroupsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
			this.TradingGroupsGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.TradingGroupsGrid_FetchRowStyle);
			this.TradingGroupsGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.TradingGroupsGrid_Paint);
			this.TradingGroupsGrid.PropBag = resources.GetString("TradingGroupsGrid.PropBag");
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.DateTimePicker);
			this.panel1.Controls.Add(this.DefaultAutoApprovalTextBox);
			this.panel1.Controls.Add(this.DefaultAutoApprovalLabel);
			this.panel1.Controls.Add(this.PremiumMaxTextBox);
			this.panel1.Controls.Add(this.PremiumMinTextBox);
			this.panel1.Controls.Add(this.DefaultPriceMinimumTextBox);
			this.panel1.Controls.Add(this.DefaultPremiumThresholdLabel);
			this.panel1.Controls.Add(this.DefaultPriceLabel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1200, 60);
			this.panel1.TabIndex = 4;
			// 
			// DateTimePicker
			// 
			this.DateTimePicker.Location = new System.Drawing.Point(936, 6);
			this.DateTimePicker.Name = "DateTimePicker";
			this.DateTimePicker.Size = new System.Drawing.Size(261, 21);
			this.DateTimePicker.TabIndex = 7;
			this.DateTimePicker.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
			// 
			// DefaultAutoApprovalTextBox
			// 
			this.DefaultAutoApprovalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DefaultAutoApprovalTextBox.DataType = typeof(long);
			this.DefaultAutoApprovalTextBox.Location = new System.Drawing.Point(555, 7);
			this.DefaultAutoApprovalTextBox.Name = "DefaultAutoApprovalTextBox";
			this.DefaultAutoApprovalTextBox.Size = new System.Drawing.Size(170, 19);
			this.DefaultAutoApprovalTextBox.TabIndex = 6;
			this.DefaultAutoApprovalTextBox.Tag = null;
			this.DefaultAutoApprovalTextBox.TextDetached = true;
			this.DefaultAutoApprovalTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
			// 
			// DefaultAutoApprovalLabel
			// 
			this.DefaultAutoApprovalLabel.AutoSize = true;
			this.DefaultAutoApprovalLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(195)))), ((int)(((byte)(235)))));
			this.DefaultAutoApprovalLabel.ForeColor = System.Drawing.Color.Black;
			this.DefaultAutoApprovalLabel.Location = new System.Drawing.Point(415, 10);
			this.DefaultAutoApprovalLabel.Name = "DefaultAutoApprovalLabel";
			this.DefaultAutoApprovalLabel.Size = new System.Drawing.Size(139, 13);
			this.DefaultAutoApprovalLabel.TabIndex = 5;
			this.DefaultAutoApprovalLabel.Tag = null;
			this.DefaultAutoApprovalLabel.Text = "Default Auto-Approval:";
			this.DefaultAutoApprovalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.DefaultAutoApprovalLabel.TextDetached = true;
			this.DefaultAutoApprovalLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
			// 
			// PremiumMaxTextBox
			// 
			this.PremiumMaxTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PremiumMaxTextBox.DataType = typeof(long);
			this.PremiumMaxTextBox.Location = new System.Drawing.Point(276, 33);
			this.PremiumMaxTextBox.Name = "PremiumMaxTextBox";
			this.PremiumMaxTextBox.Size = new System.Drawing.Size(77, 19);
			this.PremiumMaxTextBox.TabIndex = 4;
			this.PremiumMaxTextBox.Tag = null;
			this.PremiumMaxTextBox.TextDetached = true;
			this.PremiumMaxTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
			// 
			// PremiumMinTextBox
			// 
			this.PremiumMinTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PremiumMinTextBox.DataType = typeof(long);
			this.PremiumMinTextBox.Location = new System.Drawing.Point(183, 33);
			this.PremiumMinTextBox.Name = "PremiumMinTextBox";
			this.PremiumMinTextBox.Size = new System.Drawing.Size(86, 19);
			this.PremiumMinTextBox.TabIndex = 3;
			this.PremiumMinTextBox.Tag = null;
			this.PremiumMinTextBox.TextDetached = true;
			this.PremiumMinTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
			// 
			// DefaultPriceMinimumTextBox
			// 
			this.DefaultPriceMinimumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DefaultPriceMinimumTextBox.DataType = typeof(decimal);
			this.DefaultPriceMinimumTextBox.Location = new System.Drawing.Point(183, 7);
			this.DefaultPriceMinimumTextBox.Name = "DefaultPriceMinimumTextBox";
			this.DefaultPriceMinimumTextBox.Size = new System.Drawing.Size(170, 19);
			this.DefaultPriceMinimumTextBox.TabIndex = 2;
			this.DefaultPriceMinimumTextBox.Tag = null;
			this.DefaultPriceMinimumTextBox.TextDetached = true;
			this.DefaultPriceMinimumTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
			// 
			// DefaultPremiumThresholdLabel
			// 
			this.DefaultPremiumThresholdLabel.AutoSize = true;
			this.DefaultPremiumThresholdLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(195)))), ((int)(((byte)(235)))));
			this.DefaultPremiumThresholdLabel.ForeColor = System.Drawing.Color.Black;
			this.DefaultPremiumThresholdLabel.Location = new System.Drawing.Point(14, 36);
			this.DefaultPremiumThresholdLabel.Name = "DefaultPremiumThresholdLabel";
			this.DefaultPremiumThresholdLabel.Size = new System.Drawing.Size(168, 13);
			this.DefaultPremiumThresholdLabel.TabIndex = 1;
			this.DefaultPremiumThresholdLabel.Tag = null;
			this.DefaultPremiumThresholdLabel.Text = "Default Premium Threshold:";
			this.DefaultPremiumThresholdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.DefaultPremiumThresholdLabel.TextDetached = true;
			this.DefaultPremiumThresholdLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
			// 
			// DefaultPriceLabel
			// 
			this.DefaultPriceLabel.AutoSize = true;
			this.DefaultPriceLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(195)))), ((int)(((byte)(235)))));
			this.DefaultPriceLabel.ForeColor = System.Drawing.Color.Black;
			this.DefaultPriceLabel.Location = new System.Drawing.Point(91, 10);
			this.DefaultPriceLabel.Name = "DefaultPriceLabel";
			this.DefaultPriceLabel.Size = new System.Drawing.Size(85, 13);
			this.DefaultPriceLabel.TabIndex = 0;
			this.DefaultPriceLabel.Tag = null;
			this.DefaultPriceLabel.Text = "Default Price:";
			this.DefaultPriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.DefaultPriceLabel.TextDetached = true;
			this.DefaultPriceLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
			// 
			// LocatesTradingGroupsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1200, 272);
			this.Controls.Add(this.TradingGroupsParametersGrid);
			this.Controls.Add(this.TradingGroupsGrid);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LocatesTradingGroupsForm";
			this.Text = "Locates - Trading Groups";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LocatesTradingGroupsForm_FormClosed);
			this.Load += new System.EventHandler(this.LocatesTradingGroupsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupsParametersGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TradingGroupsGrid)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DefaultAutoApprovalTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultAutoApprovalLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PremiumMaxTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PremiumMinTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultPriceMinimumTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultPremiumThresholdLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DefaultPriceLabel)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid TradingGroupsParametersGrid;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid TradingGroupsGrid;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DateTimePicker DateTimePicker;
		private C1.Win.C1Input.C1TextBox DefaultAutoApprovalTextBox;
		private C1.Win.C1Input.C1Label DefaultAutoApprovalLabel;
		private C1.Win.C1Input.C1TextBox PremiumMaxTextBox;
		private C1.Win.C1Input.C1TextBox PremiumMinTextBox;
		private C1.Win.C1Input.C1TextBox DefaultPriceMinimumTextBox;
		private C1.Win.C1Input.C1Label DefaultPremiumThresholdLabel;
		private C1.Win.C1Input.C1Label DefaultPriceLabel;
	}
}