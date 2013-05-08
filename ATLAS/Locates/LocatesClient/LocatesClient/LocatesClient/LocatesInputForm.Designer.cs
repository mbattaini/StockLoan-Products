namespace LocatesClient
{
  partial class LocatesInputForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocatesInputForm));
		this.panel2 = new System.Windows.Forms.Panel();
		this.TradingGroupCombo = new C1.Win.C1List.C1Combo();
		this.GroupCodeLabel = new C1.Win.C1Input.C1Label();
		this.ClearButton = new C1.Win.C1Input.C1Button();
		this.SubmitButton = new C1.Win.C1Input.C1Button();
		this.InputTextBox = new C1.Win.C1Input.C1TextBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.StatusMessageLabel = new C1.Win.C1Input.C1Label();
		this.StatusLabel = new C1.Win.C1Input.C1Label();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.TradingGroupCombo)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.GroupCodeLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.InputTextBox)).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
		this.SuspendLayout();
		// 
		// panel2
		// 
		this.panel2.Controls.Add(this.TradingGroupCombo);
		this.panel2.Controls.Add(this.GroupCodeLabel);
		this.panel2.Controls.Add(this.ClearButton);
		this.panel2.Controls.Add(this.SubmitButton);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(519, 56);
		this.panel2.TabIndex = 5;
		// 
		// TradingGroupCombo
		// 
		this.TradingGroupCombo.AddItemSeparator = ';';
		this.TradingGroupCombo.Caption = "";
		this.TradingGroupCombo.CaptionHeight = 17;
		this.TradingGroupCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
		this.TradingGroupCombo.ColumnCaptionHeight = 17;
		this.TradingGroupCombo.ColumnFooterHeight = 17;
		this.TradingGroupCombo.ContentHeight = 16;
		this.TradingGroupCombo.DeadAreaBackColor = System.Drawing.Color.Empty;
		this.TradingGroupCombo.DropDownWidth = 400;
		this.TradingGroupCombo.EditorBackColor = System.Drawing.SystemColors.Window;
		this.TradingGroupCombo.EditorFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.TradingGroupCombo.EditorForeColor = System.Drawing.SystemColors.WindowText;
		this.TradingGroupCombo.EditorHeight = 16;
		this.TradingGroupCombo.ExtendRightColumn = true;
		this.TradingGroupCombo.Images.Add(((System.Drawing.Image)(resources.GetObject("TradingGroupCombo.Images"))));
		this.TradingGroupCombo.ItemHeight = 15;
		this.TradingGroupCombo.Location = new System.Drawing.Point(99, 12);
		this.TradingGroupCombo.MatchEntryTimeout = ((long)(2000));
		this.TradingGroupCombo.MaxDropDownItems = ((short)(5));
		this.TradingGroupCombo.MaxLength = 32767;
		this.TradingGroupCombo.MouseCursor = System.Windows.Forms.Cursors.Default;
		this.TradingGroupCombo.Name = "TradingGroupCombo";
		this.TradingGroupCombo.RowDivider.Color = System.Drawing.Color.DarkGray;
		this.TradingGroupCombo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
		this.TradingGroupCombo.RowSubDividerColor = System.Drawing.Color.DarkGray;
		this.TradingGroupCombo.Size = new System.Drawing.Size(121, 22);
		this.TradingGroupCombo.TabIndex = 3;
		this.TradingGroupCombo.PropBag = resources.GetString("TradingGroupCombo.PropBag");
		// 
		// GroupCodeLabel
		// 
		this.GroupCodeLabel.AutoSize = true;
		this.GroupCodeLabel.BackColor = System.Drawing.Color.Transparent;
		this.GroupCodeLabel.ForeColor = System.Drawing.Color.Black;
		this.GroupCodeLabel.Location = new System.Drawing.Point(5, 17);
		this.GroupCodeLabel.Name = "GroupCodeLabel";
		this.GroupCodeLabel.Size = new System.Drawing.Size(81, 13);
		this.GroupCodeLabel.TabIndex = 2;
		this.GroupCodeLabel.Tag = null;
		this.GroupCodeLabel.Text = "Group Code:";
		this.GroupCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.GroupCodeLabel.TextDetached = true;
		this.GroupCodeLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
		// 
		// ClearButton
		// 
		this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.ClearButton.Location = new System.Drawing.Point(432, 12);
		this.ClearButton.Name = "ClearButton";
		this.ClearButton.Size = new System.Drawing.Size(75, 23);
		this.ClearButton.TabIndex = 1;
		this.ClearButton.Text = "Clear";
		this.ClearButton.UseVisualStyleBackColor = true;
		this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
		// 
		// SubmitButton
		// 
		this.SubmitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		this.SubmitButton.Location = new System.Drawing.Point(317, 12);
		this.SubmitButton.Name = "SubmitButton";
		this.SubmitButton.Size = new System.Drawing.Size(99, 23);
		this.SubmitButton.TabIndex = 0;
		this.SubmitButton.Text = "Submit List";
		this.SubmitButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
		this.SubmitButton.UseVisualStyleBackColor = true;
		this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
		// 
		// InputTextBox
		// 
		this.InputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.InputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.InputTextBox.Location = new System.Drawing.Point(0, 56);
		this.InputTextBox.Multiline = true;
		this.InputTextBox.Name = "InputTextBox";
		this.InputTextBox.Size = new System.Drawing.Size(519, 366);
		this.InputTextBox.TabIndex = 6;
		this.InputTextBox.Tag = null;
		// 
		// panel1
		// 
		this.panel1.Controls.Add(this.StatusMessageLabel);
		this.panel1.Controls.Add(this.StatusLabel);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 366);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(519, 56);
		this.panel1.TabIndex = 7;
		// 
		// StatusMessageLabel
		// 
		this.StatusMessageLabel.AutoSize = true;
		this.StatusMessageLabel.BackColor = System.Drawing.Color.Transparent;
		this.StatusMessageLabel.ForeColor = System.Drawing.Color.Black;
		this.StatusMessageLabel.Location = new System.Drawing.Point(58, 17);
		this.StatusMessageLabel.Name = "StatusMessageLabel";
		this.StatusMessageLabel.Size = new System.Drawing.Size(0, 13);
		this.StatusMessageLabel.TabIndex = 3;
		this.StatusMessageLabel.Tag = null;
		this.StatusMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.StatusMessageLabel.TextDetached = true;
		this.StatusMessageLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
		// 
		// StatusLabel
		// 
		this.StatusLabel.AutoSize = true;
		this.StatusLabel.BackColor = System.Drawing.Color.Transparent;
		this.StatusLabel.ForeColor = System.Drawing.Color.Black;
		this.StatusLabel.Location = new System.Drawing.Point(4, 17);
		this.StatusLabel.Name = "StatusLabel";
		this.StatusLabel.Size = new System.Drawing.Size(48, 13);
		this.StatusLabel.TabIndex = 2;
		this.StatusLabel.Tag = null;
		this.StatusLabel.Text = "Status:";
		this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.StatusLabel.TextDetached = true;
		this.StatusLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
		// 
		// LocatesInputForm
		// 
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.ClientSize = new System.Drawing.Size(519, 422);
		this.Controls.Add(this.panel1);
		this.Controls.Add(this.InputTextBox);
		this.Controls.Add(this.panel2);
		this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		this.MaximumSize = new System.Drawing.Size(685, 450);
		this.Name = "LocatesInputForm";
		this.Text = "Locates - Input";
		this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LocatesInputForm_FormClosed);
		this.Load += new System.EventHandler(this.LocatesInputForm_Load);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.TradingGroupCombo)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.GroupCodeLabel)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.InputTextBox)).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
		this.ResumeLayout(false);

    }

    #endregion

	  private System.Windows.Forms.Panel panel2;
    private C1.Win.C1Input.C1TextBox InputTextBox;
	  private C1.Win.C1List.C1Combo TradingGroupCombo;
	  private C1.Win.C1Input.C1Label GroupCodeLabel;
	  private C1.Win.C1Input.C1Button ClearButton;
	  private C1.Win.C1Input.C1Button SubmitButton;
	  private System.Windows.Forms.Panel panel1;
	  private C1.Win.C1Input.C1Label StatusMessageLabel;
	  private C1.Win.C1Input.C1Label StatusLabel;

  }
}