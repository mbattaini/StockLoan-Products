namespace PreborrowClient
{
  partial class PreBorrowInputFrom
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreBorrowInputFrom));
      this.GroupCodeCombo = new C1.Win.C1List.C1Combo();
      this.InputGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
      this.ClearListButton = new C1.Win.C1Input.C1Button();
      this.SubmitListButton = new C1.Win.C1Input.C1Button();
      this.ParseListButton = new C1.Win.C1Input.C1Button();
      this.ContactPhoneNumberTextBox = new C1.Win.C1Input.C1TextBox();
      this.ContactEmailAddressTextBox = new C1.Win.C1Input.C1TextBox();
      this.EmailAddressLabel = new C1.Win.C1Input.C1Label();
      this.PhoneNumebrLabel = new C1.Win.C1Input.C1Label();
      this.NameLabel = new C1.Win.C1Input.C1Label();
      this.GroupCodeLabel = new C1.Win.C1Input.C1Label();
      this.StatusMessageLabel = new C1.Win.C1Input.C1Label();
      this.ListTextBox = new C1.Win.C1Input.C1TextBox();
      this.ContactsCombo = new C1.Win.C1List.C1Combo();
      this.ContactNameTextBox = new C1.Win.C1Input.C1TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.GroupCodeCombo)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.InputGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ContactPhoneNumberTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ContactEmailAddressTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.EmailAddressLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.PhoneNumebrLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.NameLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.GroupCodeLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ListTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ContactsCombo)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ContactNameTextBox)).BeginInit();
      this.SuspendLayout();
      // 
      // GroupCodeCombo
      // 
      this.GroupCodeCombo.AddItemSeparator = ';';
      this.GroupCodeCombo.Caption = "";
      this.GroupCodeCombo.CaptionHeight = 17;
      this.GroupCodeCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
      this.GroupCodeCombo.ColumnCaptionHeight = 17;
      this.GroupCodeCombo.ColumnFooterHeight = 17;
      this.GroupCodeCombo.ColumnWidth = 100;
      this.GroupCodeCombo.ContentHeight = 16;
      this.GroupCodeCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
      this.GroupCodeCombo.EditorBackColor = System.Drawing.SystemColors.Window;
      this.GroupCodeCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.GroupCodeCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
      this.GroupCodeCombo.EditorHeight = 16;
      this.GroupCodeCombo.ExtendRightColumn = true;
      this.GroupCodeCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("GroupCodeCombo.Images"))));
      this.GroupCodeCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("GroupCodeCombo.Images1"))));
      this.GroupCodeCombo.ItemHeight = 15;
      this.GroupCodeCombo.Location = new System.Drawing.Point(132, 6);
      this.GroupCodeCombo.MatchEntryTimeout = ((long)(2000));
      this.GroupCodeCombo.MaxDropDownItems = ((short)(40));
      this.GroupCodeCombo.MaxLength = 32767;
      this.GroupCodeCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
      this.GroupCodeCombo.Name = "GroupCodeCombo";
      this.GroupCodeCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
      this.GroupCodeCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
      this.GroupCodeCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
      this.GroupCodeCombo.Size = new System.Drawing.Size(135, 22);
      this.GroupCodeCombo.TabIndex = 1;
      this.GroupCodeCombo.TextChanged += new System.EventHandler(this.GroupCodeCombo_TextChanged);
      this.GroupCodeCombo.PropBag = resources.GetString("GroupCodeCombo.PropBag");
      // 
      // InputGrid
      // 
      this.InputGrid.CaptionHeight = 17;
      this.InputGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.InputGrid.EmptyRows = true;
      this.InputGrid.ExtendRightColumn = true;
      this.InputGrid.GroupByCaption = "Drag a column header here to group by that column";
      this.InputGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("InputGrid.Images"))));
      this.InputGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("InputGrid.Images1"))));
      this.InputGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("InputGrid.Images2"))));
      this.InputGrid.Location = new System.Drawing.Point(1, 315);
      this.InputGrid.Name = "InputGrid";
      this.InputGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
      this.InputGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
      this.InputGrid.PreviewInfo.ZoomFactor = 75;
      this.InputGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("InputGrid.PrintInfo.PageSettings")));
      this.InputGrid.RowDivider.Color = System.Drawing.Color.Gainsboro;
      this.InputGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
      this.InputGrid.RowHeight = 15;
      this.InputGrid.Size = new System.Drawing.Size(339, 291);
      this.InputGrid.TabIndex = 2;
      this.InputGrid.Text = "ListGrid";
      this.InputGrid.PropBag = resources.GetString("InputGrid.PropBag");
      // 
      // ClearListButton
      // 
      this.ClearListButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.ClearListButton.Location = new System.Drawing.Point(226, 642);
      this.ClearListButton.Name = "ClearListButton";
      this.ClearListButton.Size = new System.Drawing.Size(87, 23);
      this.ClearListButton.TabIndex = 8;
      this.ClearListButton.Text = "Clear List";
      this.ClearListButton.UseVisualStyleBackColor = true;
      this.ClearListButton.Click += new System.EventHandler(this.ClearListButton_Click);
      // 
      // SubmitListButton
      // 
      this.SubmitListButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.SubmitListButton.Location = new System.Drawing.Point(132, 642);
      this.SubmitListButton.Name = "SubmitListButton";
      this.SubmitListButton.Size = new System.Drawing.Size(87, 23);
      this.SubmitListButton.TabIndex = 7;
      this.SubmitListButton.Text = "Submit List";
      this.SubmitListButton.UseVisualStyleBackColor = true;
      this.SubmitListButton.Click += new System.EventHandler(this.SubmitListButton_Click);
      // 
      // ParseListButton
      // 
      this.ParseListButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.ParseListButton.Location = new System.Drawing.Point(37, 642);
      this.ParseListButton.Name = "ParseListButton";
      this.ParseListButton.Size = new System.Drawing.Size(87, 23);
      this.ParseListButton.TabIndex = 6;
      this.ParseListButton.Text = "Parse List";
      this.ParseListButton.UseVisualStyleBackColor = true;
      this.ParseListButton.Click += new System.EventHandler(this.ParseListButton_Click);
      // 
      // ContactPhoneNumberTextBox
      // 
      this.ContactPhoneNumberTextBox.Location = new System.Drawing.Point(132, 56);
      this.ContactPhoneNumberTextBox.Name = "ContactPhoneNumberTextBox";
      this.ContactPhoneNumberTextBox.Size = new System.Drawing.Size(181, 21);
      this.ContactPhoneNumberTextBox.TabIndex = 3;
      this.ContactPhoneNumberTextBox.Tag = null;
      this.ContactPhoneNumberTextBox.TextDetached = true;
      // 
      // ContactEmailAddressTextBox
      // 
      this.ContactEmailAddressTextBox.Location = new System.Drawing.Point(132, 81);
      this.ContactEmailAddressTextBox.Name = "ContactEmailAddressTextBox";
      this.ContactEmailAddressTextBox.Size = new System.Drawing.Size(181, 21);
      this.ContactEmailAddressTextBox.TabIndex = 4;
      this.ContactEmailAddressTextBox.Tag = null;
      this.ContactEmailAddressTextBox.TextDetached = true;
      // 
      // EmailAddressLabel
      // 
      this.EmailAddressLabel.AutoSize = true;
      this.EmailAddressLabel.BackColor = System.Drawing.Color.Transparent;
      this.EmailAddressLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.EmailAddressLabel.ForeColor = System.Drawing.Color.DarkBlue;
      this.EmailAddressLabel.Location = new System.Drawing.Point(5, 85);
      this.EmailAddressLabel.Name = "EmailAddressLabel";
      this.EmailAddressLabel.Size = new System.Drawing.Size(104, 13);
      this.EmailAddressLabel.TabIndex = 27;
      this.EmailAddressLabel.Tag = null;
      this.EmailAddressLabel.Text = "Email Address:";
      this.EmailAddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.EmailAddressLabel.TextDetached = true;
      // 
      // PhoneNumebrLabel
      // 
      this.PhoneNumebrLabel.AutoSize = true;
      this.PhoneNumebrLabel.BackColor = System.Drawing.Color.Transparent;
      this.PhoneNumebrLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.PhoneNumebrLabel.ForeColor = System.Drawing.Color.DarkBlue;
      this.PhoneNumebrLabel.Location = new System.Drawing.Point(5, 60);
      this.PhoneNumebrLabel.Name = "PhoneNumebrLabel";
      this.PhoneNumebrLabel.Size = new System.Drawing.Size(106, 13);
      this.PhoneNumebrLabel.TabIndex = 26;
      this.PhoneNumebrLabel.Tag = null;
      this.PhoneNumebrLabel.Text = "Phone Number:";
      this.PhoneNumebrLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.PhoneNumebrLabel.TextDetached = true;
      // 
      // NameLabel
      // 
      this.NameLabel.AutoSize = true;
      this.NameLabel.BackColor = System.Drawing.Color.Transparent;
      this.NameLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.NameLabel.ForeColor = System.Drawing.Color.DarkBlue;
      this.NameLabel.Location = new System.Drawing.Point(5, 36);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(48, 13);
      this.NameLabel.TabIndex = 25;
      this.NameLabel.Tag = null;
      this.NameLabel.Text = "Name:";
      this.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.NameLabel.TextDetached = true;
      // 
      // GroupCodeLabel
      // 
      this.GroupCodeLabel.AutoSize = true;
      this.GroupCodeLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.GroupCodeLabel.Location = new System.Drawing.Point(5, 10);
      this.GroupCodeLabel.Name = "GroupCodeLabel";
      this.GroupCodeLabel.Size = new System.Drawing.Size(86, 13);
      this.GroupCodeLabel.TabIndex = 24;
      this.GroupCodeLabel.Tag = null;
      this.GroupCodeLabel.Text = "Group Code:";
      this.GroupCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.GroupCodeLabel.TextDetached = true;
      // 
      // StatusMessageLabel
      // 
      this.StatusMessageLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.StatusMessageLabel.AutoSize = true;
      this.StatusMessageLabel.Location = new System.Drawing.Point(5, 618);
      this.StatusMessageLabel.Name = "StatusMessageLabel";
      this.StatusMessageLabel.Size = new System.Drawing.Size(0, 13);
      this.StatusMessageLabel.TabIndex = 28;
      this.StatusMessageLabel.Tag = null;
      this.StatusMessageLabel.TextDetached = true;
      // 
      // ListTextBox
      // 
      this.ListTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ListTextBox.Location = new System.Drawing.Point(1, 111);
      this.ListTextBox.Multiline = true;
      this.ListTextBox.Name = "ListTextBox";
      this.ListTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.ListTextBox.Size = new System.Drawing.Size(339, 204);
      this.ListTextBox.TabIndex = 5;
      this.ListTextBox.Tag = null;
      this.ListTextBox.TextDetached = true;
      this.ListTextBox.TrimEnd = false;
      // 
      // ContactsCombo
      // 
      this.ContactsCombo.AddItemSeparator = ';';
      this.ContactsCombo.Caption = "";
      this.ContactsCombo.CaptionHeight = 17;
      this.ContactsCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
      this.ContactsCombo.ColumnCaptionHeight = 17;
      this.ContactsCombo.ColumnFooterHeight = 17;
      this.ContactsCombo.ColumnWidth = 100;
      this.ContactsCombo.ContentHeight = 16;
      this.ContactsCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
      this.ContactsCombo.DropDownWidth = 400;
      this.ContactsCombo.EditorBackColor = System.Drawing.SystemColors.Window;
      this.ContactsCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ContactsCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
      this.ContactsCombo.EditorHeight = 16;
      this.ContactsCombo.ExtendRightColumn = true;
      this.ContactsCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("ContactsCombo.Images"))));
      this.ContactsCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("ContactsCombo.Images1"))));
      this.ContactsCombo.ItemHeight = 15;
      this.ContactsCombo.Location = new System.Drawing.Point(292, 31);
      this.ContactsCombo.MatchEntryTimeout = ((long)(2000));
      this.ContactsCombo.MaxDropDownItems = ((short)(5));
      this.ContactsCombo.MaxLength = 32767;
      this.ContactsCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
      this.ContactsCombo.Name = "ContactsCombo";
      this.ContactsCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
      this.ContactsCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
      this.ContactsCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
      this.ContactsCombo.Size = new System.Drawing.Size(22, 22);
      this.ContactsCombo.TabIndex = 29;
      this.ContactsCombo.Close += new C1.Win.C1List.CloseEventHandler(this.ContactsCombo_Close);
      this.ContactsCombo.RowChange += new System.EventHandler(this.ContactsCombo_RowChange);
      this.ContactsCombo.PropBag = resources.GetString("ContactsCombo.PropBag");
      // 
      // ContactNameTextBox
      // 
      this.ContactNameTextBox.Location = new System.Drawing.Point(132, 31);
      this.ContactNameTextBox.Name = "ContactNameTextBox";
      this.ContactNameTextBox.Size = new System.Drawing.Size(160, 21);
      this.ContactNameTextBox.TabIndex = 30;
      this.ContactNameTextBox.Tag = null;
      this.ContactNameTextBox.TextDetached = true;
      // 
      // PreBorrowInputFrom
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(341, 666);
      this.Controls.Add(this.ContactNameTextBox);
      this.Controls.Add(this.ContactsCombo);
      this.Controls.Add(this.ListTextBox);
      this.Controls.Add(this.StatusMessageLabel);
      this.Controls.Add(this.EmailAddressLabel);
      this.Controls.Add(this.PhoneNumebrLabel);
      this.Controls.Add(this.NameLabel);
      this.Controls.Add(this.GroupCodeLabel);
      this.Controls.Add(this.ContactEmailAddressTextBox);
      this.Controls.Add(this.ContactPhoneNumberTextBox);
      this.Controls.Add(this.ClearListButton);
      this.Controls.Add(this.SubmitListButton);
      this.Controls.Add(this.ParseListButton);
      this.Controls.Add(this.GroupCodeCombo);
      this.Controls.Add(this.InputGrid);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "PreBorrowInputFrom";
      this.Padding = new System.Windows.Forms.Padding(1, 111, 1, 60);
      this.Text = "PreBorrow - Input";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreBorrowInputFrom_FormClosed);
      this.DoubleClick += new System.EventHandler(this.PreBorrowInputFrom_DoubleClick);
      this.Load += new System.EventHandler(this.PreBorrowInputFrom_Load);
      ((System.ComponentModel.ISupportInitialize)(this.GroupCodeCombo)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.InputGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ContactPhoneNumberTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ContactEmailAddressTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.EmailAddressLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.PhoneNumebrLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.NameLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.GroupCodeLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ListTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ContactsCombo)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ContactNameTextBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private C1.Win.C1List.C1Combo GroupCodeCombo;
    private C1.Win.C1TrueDBGrid.C1TrueDBGrid InputGrid;
    private C1.Win.C1Input.C1Button ClearListButton;
    private C1.Win.C1Input.C1Button SubmitListButton;
    private C1.Win.C1Input.C1Button ParseListButton;
    private C1.Win.C1Input.C1TextBox ContactPhoneNumberTextBox;
    private C1.Win.C1Input.C1TextBox ContactEmailAddressTextBox;
    private C1.Win.C1Input.C1Label EmailAddressLabel;
    private C1.Win.C1Input.C1Label PhoneNumebrLabel;
    private C1.Win.C1Input.C1Label NameLabel;
    private C1.Win.C1Input.C1Label GroupCodeLabel;
    private C1.Win.C1Input.C1Label StatusMessageLabel;
    private C1.Win.C1Input.C1TextBox ListTextBox;
    private C1.Win.C1List.C1Combo ContactsCombo;
    private C1.Win.C1Input.C1TextBox ContactNameTextBox;

  }
}