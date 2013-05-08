namespace LocatesClient
{
  partial class LocatesExportToolForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocatesExportToolForm));
		this.LocatesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.ShowDoneByCheckBox = new System.Windows.Forms.CheckBox();
		this.ShowStatusCheckBox = new System.Windows.Forms.CheckBox();
		this.ShowDoneAtCheckBox = new System.Windows.Forms.CheckBox();
		this.ShowLocateIdCheckBox = new System.Windows.Forms.CheckBox();
		this.ShowOpenTimeCheckBox = new System.Windows.Forms.CheckBox();
		this.ShowGroupCodeCheckBox = new System.Windows.Forms.CheckBox();
		this.ShowTradeDateCheckBox = new System.Windows.Forms.CheckBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.TradeDateMaxPicker = new System.Windows.Forms.DateTimePicker();
		this.TradeDateMaxLabel = new C1.Win.C1Input.C1Label();
		this.TradeDateMinPicker = new System.Windows.Forms.DateTimePicker();
		this.EmailAddressLabel = new C1.Win.C1Input.C1Label();
		this.TradeDateMinLabel = new C1.Win.C1Input.C1Label();
		this.EmailAddressTextBox = new C1.Win.C1Input.C1TextBox();
		this.LookUpButton = new C1.Win.C1Input.C1Button();
		this.GenerateButton = new C1.Win.C1Input.C1Button();
		this.ExportGroupBox = new System.Windows.Forms.GroupBox();
		this.ExportToExcelRadio = new System.Windows.Forms.RadioButton();
		this.ExportToPdfRadio = new System.Windows.Forms.RadioButton();
		this.ExportToEmailRadio = new System.Windows.Forms.RadioButton();
		this.FilterGroupBox = new System.Windows.Forms.GroupBox();
		this.TradingGroupsLabel = new C1.Win.C1Input.C1Label();
		this.SecurityIdLabel = new C1.Win.C1Input.C1Label();
		this.SecurityIdTextBox = new C1.Win.C1Input.C1TextBox();
		this.GroupCodeTextBox = new C1.Win.C1Input.C1TextBox();
		((System.ComponentModel.ISupportInitialize)(this.LocatesGrid)).BeginInit();
		this.panel1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.TradeDateMaxLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.EmailAddressLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.TradeDateMinLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.EmailAddressTextBox)).BeginInit();
		this.ExportGroupBox.SuspendLayout();
		this.FilterGroupBox.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.TradingGroupsLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.SecurityIdLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.SecurityIdTextBox)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.GroupCodeTextBox)).BeginInit();
		this.SuspendLayout();
		// 
		// LocatesGrid
		// 
		this.LocatesGrid.AllowColSelect = false;
		this.LocatesGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
		this.LocatesGrid.AllowUpdate = false;
		this.LocatesGrid.AlternatingRows = true;
		this.LocatesGrid.CaptionHeight = 17;
		this.LocatesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveDown;
		this.LocatesGrid.Dock = System.Windows.Forms.DockStyle.Right;
		this.LocatesGrid.EmptyRows = true;
		this.LocatesGrid.ExtendRightColumn = true;
		this.LocatesGrid.FetchRowStyles = true;
		this.LocatesGrid.FilterBar = true;
		this.LocatesGrid.GroupByAreaVisible = false;
		this.LocatesGrid.GroupByCaption = "Drag a column header here to group by that column";
		this.LocatesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("LocatesGrid.Images"))));
		this.LocatesGrid.Location = new System.Drawing.Point(301, 0);
		this.LocatesGrid.MaintainRowCurrency = true;
		this.LocatesGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.HighlightRow;
		this.LocatesGrid.Name = "LocatesGrid";
		this.LocatesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
		this.LocatesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
		this.LocatesGrid.PreviewInfo.ZoomFactor = 75;
		this.LocatesGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("LocatesGrid.PrintInfo.PageSettings")));
		this.LocatesGrid.RecordSelectors = false;
		this.LocatesGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
		this.LocatesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
		this.LocatesGrid.RowHeight = 17;
		this.LocatesGrid.Size = new System.Drawing.Size(926, 339);
		this.LocatesGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
		this.LocatesGrid.TabIndex = 2;
		this.LocatesGrid.Text = "c1TrueDBGrid1";
		this.LocatesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.LocatesGrid_FormatText);
		this.LocatesGrid.PropBag = resources.GetString("LocatesGrid.PropBag");
		// 
		// panel1
		// 
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.groupBox2);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Controls.Add(this.LookUpButton);
		this.panel1.Controls.Add(this.GenerateButton);
		this.panel1.Controls.Add(this.ExportGroupBox);
		this.panel1.Controls.Add(this.FilterGroupBox);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(301, 339);
		this.panel1.TabIndex = 7;
		// 
		// groupBox2
		// 
		this.groupBox2.Controls.Add(this.ShowDoneByCheckBox);
		this.groupBox2.Controls.Add(this.ShowStatusCheckBox);
		this.groupBox2.Controls.Add(this.ShowDoneAtCheckBox);
		this.groupBox2.Controls.Add(this.ShowLocateIdCheckBox);
		this.groupBox2.Controls.Add(this.ShowOpenTimeCheckBox);
		this.groupBox2.Controls.Add(this.ShowGroupCodeCheckBox);
		this.groupBox2.Controls.Add(this.ShowTradeDateCheckBox);
		this.groupBox2.Location = new System.Drawing.Point(110, 203);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(184, 103);
		this.groupBox2.TabIndex = 27;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Columns";
		// 
		// ShowDoneByCheckBox
		// 
		this.ShowDoneByCheckBox.AutoSize = true;
		this.ShowDoneByCheckBox.Location = new System.Drawing.Point(102, 52);
		this.ShowDoneByCheckBox.Name = "ShowDoneByCheckBox";
		this.ShowDoneByCheckBox.Size = new System.Drawing.Size(75, 17);
		this.ShowDoneByCheckBox.TabIndex = 6;
		this.ShowDoneByCheckBox.Text = "Done By";
		this.ShowDoneByCheckBox.UseVisualStyleBackColor = true;
		this.ShowDoneByCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
		// 
		// ShowStatusCheckBox
		// 
		this.ShowStatusCheckBox.AutoSize = true;
		this.ShowStatusCheckBox.Location = new System.Drawing.Point(102, 37);
		this.ShowStatusCheckBox.Name = "ShowStatusCheckBox";
		this.ShowStatusCheckBox.Size = new System.Drawing.Size(62, 17);
		this.ShowStatusCheckBox.TabIndex = 5;
		this.ShowStatusCheckBox.Text = "Status";
		this.ShowStatusCheckBox.UseVisualStyleBackColor = true;
		this.ShowStatusCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
		// 
		// ShowDoneAtCheckBox
		// 
		this.ShowDoneAtCheckBox.AutoSize = true;
		this.ShowDoneAtCheckBox.Location = new System.Drawing.Point(102, 21);
		this.ShowDoneAtCheckBox.Name = "ShowDoneAtCheckBox";
		this.ShowDoneAtCheckBox.Size = new System.Drawing.Size(72, 17);
		this.ShowDoneAtCheckBox.TabIndex = 4;
		this.ShowDoneAtCheckBox.Text = "Done At";
		this.ShowDoneAtCheckBox.UseVisualStyleBackColor = true;
		this.ShowDoneAtCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
		// 
		// ShowLocateIdCheckBox
		// 
		this.ShowLocateIdCheckBox.AutoSize = true;
		this.ShowLocateIdCheckBox.Location = new System.Drawing.Point(6, 67);
		this.ShowLocateIdCheckBox.Name = "ShowLocateIdCheckBox";
		this.ShowLocateIdCheckBox.Size = new System.Drawing.Size(75, 17);
		this.ShowLocateIdCheckBox.TabIndex = 3;
		this.ShowLocateIdCheckBox.Text = "LocateId";
		this.ShowLocateIdCheckBox.UseVisualStyleBackColor = true;
		this.ShowLocateIdCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
		// 
		// ShowOpenTimeCheckBox
		// 
		this.ShowOpenTimeCheckBox.AutoSize = true;
		this.ShowOpenTimeCheckBox.Location = new System.Drawing.Point(6, 52);
		this.ShowOpenTimeCheckBox.Name = "ShowOpenTimeCheckBox";
		this.ShowOpenTimeCheckBox.Size = new System.Drawing.Size(84, 17);
		this.ShowOpenTimeCheckBox.TabIndex = 2;
		this.ShowOpenTimeCheckBox.Text = "OpenTime";
		this.ShowOpenTimeCheckBox.UseVisualStyleBackColor = true;
		this.ShowOpenTimeCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
		// 
		// ShowGroupCodeCheckBox
		// 
		this.ShowGroupCodeCheckBox.AutoSize = true;
		this.ShowGroupCodeCheckBox.Location = new System.Drawing.Point(6, 37);
		this.ShowGroupCodeCheckBox.Name = "ShowGroupCodeCheckBox";
		this.ShowGroupCodeCheckBox.Size = new System.Drawing.Size(91, 17);
		this.ShowGroupCodeCheckBox.TabIndex = 1;
		this.ShowGroupCodeCheckBox.Text = "GroupCode";
		this.ShowGroupCodeCheckBox.UseVisualStyleBackColor = true;
		this.ShowGroupCodeCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
		// 
		// ShowTradeDateCheckBox
		// 
		this.ShowTradeDateCheckBox.AutoSize = true;
		this.ShowTradeDateCheckBox.Location = new System.Drawing.Point(6, 21);
		this.ShowTradeDateCheckBox.Name = "ShowTradeDateCheckBox";
		this.ShowTradeDateCheckBox.Size = new System.Drawing.Size(86, 17);
		this.ShowTradeDateCheckBox.TabIndex = 0;
		this.ShowTradeDateCheckBox.Text = "TradeDate";
		this.ShowTradeDateCheckBox.UseVisualStyleBackColor = true;
		this.ShowTradeDateCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
		// 
		// groupBox1
		// 
		this.groupBox1.Controls.Add(this.TradeDateMaxPicker);
		this.groupBox1.Controls.Add(this.TradeDateMaxLabel);
		this.groupBox1.Controls.Add(this.TradeDateMinPicker);
		this.groupBox1.Controls.Add(this.EmailAddressLabel);
		this.groupBox1.Controls.Add(this.TradeDateMinLabel);
		this.groupBox1.Controls.Add(this.EmailAddressTextBox);
		this.groupBox1.Location = new System.Drawing.Point(4, 11);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(290, 101);
		this.groupBox1.TabIndex = 26;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "General";
		// 
		// TradeDateMaxPicker
		// 
		this.TradeDateMaxPicker.CustomFormat = "yyyy-MM-dd";
		this.TradeDateMaxPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.TradeDateMaxPicker.Location = new System.Drawing.Point(126, 41);
		this.TradeDateMaxPicker.Name = "TradeDateMaxPicker";
		this.TradeDateMaxPicker.Size = new System.Drawing.Size(156, 21);
		this.TradeDateMaxPicker.TabIndex = 2;
		// 
		// TradeDateMaxLabel
		// 
		this.TradeDateMaxLabel.AutoSize = true;
		this.TradeDateMaxLabel.BackColor = System.Drawing.Color.Transparent;
		this.TradeDateMaxLabel.ForeColor = System.Drawing.Color.Black;
		this.TradeDateMaxLabel.Location = new System.Drawing.Point(6, 45);
		this.TradeDateMaxLabel.Name = "TradeDateMaxLabel";
		this.TradeDateMaxLabel.Size = new System.Drawing.Size(76, 13);
		this.TradeDateMaxLabel.TabIndex = 29;
		this.TradeDateMaxLabel.Tag = null;
		this.TradeDateMaxLabel.Text = "Trade Date:";
		this.TradeDateMaxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TradeDateMaxLabel.TextDetached = true;
		this.TradeDateMaxLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
		// 
		// TradeDateMinPicker
		// 
		this.TradeDateMinPicker.CustomFormat = "yyyy-MM-dd";
		this.TradeDateMinPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.TradeDateMinPicker.Location = new System.Drawing.Point(126, 16);
		this.TradeDateMinPicker.Name = "TradeDateMinPicker";
		this.TradeDateMinPicker.Size = new System.Drawing.Size(156, 21);
		this.TradeDateMinPicker.TabIndex = 1;
		// 
		// EmailAddressLabel
		// 
		this.EmailAddressLabel.AutoSize = true;
		this.EmailAddressLabel.BackColor = System.Drawing.Color.Transparent;
		this.EmailAddressLabel.ForeColor = System.Drawing.Color.Black;
		this.EmailAddressLabel.Location = new System.Drawing.Point(6, 71);
		this.EmailAddressLabel.Name = "EmailAddressLabel";
		this.EmailAddressLabel.Size = new System.Drawing.Size(93, 13);
		this.EmailAddressLabel.TabIndex = 27;
		this.EmailAddressLabel.Tag = null;
		this.EmailAddressLabel.Text = "Email Address:";
		this.EmailAddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.EmailAddressLabel.TextDetached = true;
		this.EmailAddressLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
		// 
		// TradeDateMinLabel
		// 
		this.TradeDateMinLabel.AutoSize = true;
		this.TradeDateMinLabel.BackColor = System.Drawing.Color.Transparent;
		this.TradeDateMinLabel.ForeColor = System.Drawing.Color.Black;
		this.TradeDateMinLabel.Location = new System.Drawing.Point(6, 20);
		this.TradeDateMinLabel.Name = "TradeDateMinLabel";
		this.TradeDateMinLabel.Size = new System.Drawing.Size(76, 13);
		this.TradeDateMinLabel.TabIndex = 26;
		this.TradeDateMinLabel.Tag = null;
		this.TradeDateMinLabel.Text = "Trade Date:";
		this.TradeDateMinLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TradeDateMinLabel.TextDetached = true;
		this.TradeDateMinLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
		// 
		// EmailAddressTextBox
		// 
		this.EmailAddressTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.EmailAddressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.EmailAddressTextBox.Location = new System.Drawing.Point(126, 68);
		this.EmailAddressTextBox.Name = "EmailAddressTextBox";
		this.EmailAddressTextBox.Size = new System.Drawing.Size(156, 19);
		this.EmailAddressTextBox.TabIndex = 30;
		this.EmailAddressTextBox.Tag = null;
		this.EmailAddressTextBox.TextDetached = true;
		this.EmailAddressTextBox.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
		// 
		// LookUpButton
		// 
		this.LookUpButton.Location = new System.Drawing.Point(4, 310);
		this.LookUpButton.Name = "LookUpButton";
		this.LookUpButton.Size = new System.Drawing.Size(75, 23);
		this.LookUpButton.TabIndex = 9;
		this.LookUpButton.Text = "Look Up";
		this.LookUpButton.UseVisualStyleBackColor = true;
		this.LookUpButton.Click += new System.EventHandler(this.LookUpButton_Click);
		// 
		// GenerateButton
		// 
		this.GenerateButton.Location = new System.Drawing.Point(85, 310);
		this.GenerateButton.Name = "GenerateButton";
		this.GenerateButton.Size = new System.Drawing.Size(75, 23);
		this.GenerateButton.TabIndex = 10;
		this.GenerateButton.Text = "Generate";
		this.GenerateButton.UseVisualStyleBackColor = true;
		this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
		// 
		// ExportGroupBox
		// 
		this.ExportGroupBox.Controls.Add(this.ExportToExcelRadio);
		this.ExportGroupBox.Controls.Add(this.ExportToPdfRadio);
		this.ExportGroupBox.Controls.Add(this.ExportToEmailRadio);
		this.ExportGroupBox.Location = new System.Drawing.Point(4, 203);
		this.ExportGroupBox.Name = "ExportGroupBox";
		this.ExportGroupBox.Size = new System.Drawing.Size(100, 103);
		this.ExportGroupBox.TabIndex = 17;
		this.ExportGroupBox.TabStop = false;
		this.ExportGroupBox.Text = "Export To";
		// 
		// ExportToExcelRadio
		// 
		this.ExportToExcelRadio.AutoSize = true;
		this.ExportToExcelRadio.Location = new System.Drawing.Point(7, 20);
		this.ExportToExcelRadio.Name = "ExportToExcelRadio";
		this.ExportToExcelRadio.Size = new System.Drawing.Size(55, 17);
		this.ExportToExcelRadio.TabIndex = 6;
		this.ExportToExcelRadio.TabStop = true;
		this.ExportToExcelRadio.Text = "Excel";
		this.ExportToExcelRadio.UseVisualStyleBackColor = true;
		// 
		// ExportToPdfRadio
		// 
		this.ExportToPdfRadio.AutoSize = true;
		this.ExportToPdfRadio.Location = new System.Drawing.Point(7, 43);
		this.ExportToPdfRadio.Name = "ExportToPdfRadio";
		this.ExportToPdfRadio.Size = new System.Drawing.Size(43, 17);
		this.ExportToPdfRadio.TabIndex = 7;
		this.ExportToPdfRadio.TabStop = true;
		this.ExportToPdfRadio.Text = "Pdf";
		this.ExportToPdfRadio.UseVisualStyleBackColor = true;
		// 
		// ExportToEmailRadio
		// 
		this.ExportToEmailRadio.AutoSize = true;
		this.ExportToEmailRadio.Location = new System.Drawing.Point(7, 66);
		this.ExportToEmailRadio.Name = "ExportToEmailRadio";
		this.ExportToEmailRadio.Size = new System.Drawing.Size(56, 17);
		this.ExportToEmailRadio.TabIndex = 8;
		this.ExportToEmailRadio.TabStop = true;
		this.ExportToEmailRadio.Text = "Email";
		this.ExportToEmailRadio.UseVisualStyleBackColor = true;
		// 
		// FilterGroupBox
		// 
		this.FilterGroupBox.Controls.Add(this.TradingGroupsLabel);
		this.FilterGroupBox.Controls.Add(this.SecurityIdLabel);
		this.FilterGroupBox.Controls.Add(this.SecurityIdTextBox);
		this.FilterGroupBox.Controls.Add(this.GroupCodeTextBox);
		this.FilterGroupBox.Location = new System.Drawing.Point(4, 120);
		this.FilterGroupBox.Name = "FilterGroupBox";
		this.FilterGroupBox.Size = new System.Drawing.Size(290, 77);
		this.FilterGroupBox.TabIndex = 16;
		this.FilterGroupBox.TabStop = false;
		this.FilterGroupBox.Text = "Filter Args";
		// 
		// TradingGroupsLabel
		// 
		this.TradingGroupsLabel.AutoSize = true;
		this.TradingGroupsLabel.BackColor = System.Drawing.Color.Transparent;
		this.TradingGroupsLabel.ForeColor = System.Drawing.Color.Black;
		this.TradingGroupsLabel.Location = new System.Drawing.Point(6, 22);
		this.TradingGroupsLabel.Name = "TradingGroupsLabel";
		this.TradingGroupsLabel.Size = new System.Drawing.Size(100, 13);
		this.TradingGroupsLabel.TabIndex = 9;
		this.TradingGroupsLabel.Tag = null;
		this.TradingGroupsLabel.Text = "Trading Groups:";
		this.TradingGroupsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TradingGroupsLabel.TextDetached = true;
		this.TradingGroupsLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
		// 
		// SecurityIdLabel
		// 
		this.SecurityIdLabel.AutoSize = true;
		this.SecurityIdLabel.BackColor = System.Drawing.Color.Transparent;
		this.SecurityIdLabel.ForeColor = System.Drawing.Color.Black;
		this.SecurityIdLabel.Location = new System.Drawing.Point(6, 46);
		this.SecurityIdLabel.Name = "SecurityIdLabel";
		this.SecurityIdLabel.Size = new System.Drawing.Size(107, 13);
		this.SecurityIdLabel.TabIndex = 11;
		this.SecurityIdLabel.Tag = null;
		this.SecurityIdLabel.Text = "Sec ID / Symbol:";
		this.SecurityIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.SecurityIdLabel.TextDetached = true;
		this.SecurityIdLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
		// 
		// SecurityIdTextBox
		// 
		this.SecurityIdTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.SecurityIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.SecurityIdTextBox.Location = new System.Drawing.Point(126, 44);
		this.SecurityIdTextBox.Name = "SecurityIdTextBox";
		this.SecurityIdTextBox.Size = new System.Drawing.Size(156, 19);
		this.SecurityIdTextBox.TabIndex = 13;
		this.SecurityIdTextBox.Tag = null;
		this.SecurityIdTextBox.TextDetached = true;
		this.SecurityIdTextBox.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
		// 
		// GroupCodeTextBox
		// 
		this.GroupCodeTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.GroupCodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.GroupCodeTextBox.Location = new System.Drawing.Point(126, 16);
		this.GroupCodeTextBox.Name = "GroupCodeTextBox";
		this.GroupCodeTextBox.Size = new System.Drawing.Size(156, 19);
		this.GroupCodeTextBox.TabIndex = 12;
		this.GroupCodeTextBox.Tag = null;
		this.GroupCodeTextBox.TextDetached = true;
		this.GroupCodeTextBox.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
		// 
		// LocatesExportToolForm
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(1227, 339);
		this.Controls.Add(this.panel1);
		this.Controls.Add(this.LocatesGrid);
		this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.MaximumSize = new System.Drawing.Size(1235, 367);
		this.MinimumSize = new System.Drawing.Size(1235, 367);
		this.Name = "LocatesExportToolForm";
		this.Text = "Locates - Export Tool";
		this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LocatesExportToolForm_FormClosed);
		this.Load += new System.EventHandler(this.LocatesExportToolForm_Load);
		((System.ComponentModel.ISupportInitialize)(this.LocatesGrid)).EndInit();
		this.panel1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.TradeDateMaxLabel)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.EmailAddressLabel)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.TradeDateMinLabel)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.EmailAddressTextBox)).EndInit();
		this.ExportGroupBox.ResumeLayout(false);
		this.ExportGroupBox.PerformLayout();
		this.FilterGroupBox.ResumeLayout(false);
		this.FilterGroupBox.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.TradingGroupsLabel)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.SecurityIdLabel)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.SecurityIdTextBox)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.GroupCodeTextBox)).EndInit();
		this.ResumeLayout(false);

    }

    #endregion

    private C1.Win.C1TrueDBGrid.C1TrueDBGrid LocatesGrid;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.RadioButton ExportToEmailRadio;
    private System.Windows.Forms.RadioButton ExportToPdfRadio;
    private System.Windows.Forms.RadioButton ExportToExcelRadio;
    private C1.Win.C1Input.C1Label SecurityIdLabel;
    private C1.Win.C1Input.C1Label TradingGroupsLabel;
    private System.Windows.Forms.GroupBox FilterGroupBox;
    private System.Windows.Forms.GroupBox ExportGroupBox;
    private C1.Win.C1Input.C1Button GenerateButton;
	  private C1.Win.C1Input.C1Button LookUpButton;
	  private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.DateTimePicker TradeDateMaxPicker;
    private C1.Win.C1Input.C1Label TradeDateMaxLabel;
    private System.Windows.Forms.DateTimePicker TradeDateMinPicker;
    private C1.Win.C1Input.C1Label EmailAddressLabel;
    private C1.Win.C1Input.C1Label TradeDateMinLabel;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.CheckBox ShowDoneByCheckBox;
    private System.Windows.Forms.CheckBox ShowStatusCheckBox;
    private System.Windows.Forms.CheckBox ShowDoneAtCheckBox;
    private System.Windows.Forms.CheckBox ShowLocateIdCheckBox;
    private System.Windows.Forms.CheckBox ShowOpenTimeCheckBox;
    private System.Windows.Forms.CheckBox ShowGroupCodeCheckBox;
    private System.Windows.Forms.CheckBox ShowTradeDateCheckBox;
	  private C1.Win.C1Input.C1TextBox EmailAddressTextBox;
	  private C1.Win.C1Input.C1TextBox SecurityIdTextBox;
	  private C1.Win.C1Input.C1TextBox GroupCodeTextBox;
  }
}