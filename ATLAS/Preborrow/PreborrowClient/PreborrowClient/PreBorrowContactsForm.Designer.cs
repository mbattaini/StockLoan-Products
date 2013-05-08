namespace PreborrowClient
{
  partial class PreBorrowContactsForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreBorrowContactsForm));
      C1.Win.C1TrueDBGrid.Style style1 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style2 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style3 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style4 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style5 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style6 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style7 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style8 = new C1.Win.C1TrueDBGrid.Style();
      this.ContactsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
      this.TradingGroupDropdown = new C1.Win.C1TrueDBGrid.C1TrueDBDropdown();
      this.StatusMessageLabel = new C1.Win.C1Input.C1Label();
      ((System.ComponentModel.ISupportInitialize)(this.ContactsGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TradingGroupDropdown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).BeginInit();
      this.SuspendLayout();
      // 
      // ContactsGrid
      // 
      this.ContactsGrid.AllowAddNew = true;
      this.ContactsGrid.CaptionHeight = 17;
      this.ContactsGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
      this.ContactsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ContactsGrid.EmptyRows = true;
      this.ContactsGrid.ExtendRightColumn = true;
      this.ContactsGrid.GroupByCaption = "Drag a column header here to group by that column";
      this.ContactsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ContactsGrid.Images"))));
      this.ContactsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ContactsGrid.Images1"))));
      this.ContactsGrid.Location = new System.Drawing.Point(0, 0);
      this.ContactsGrid.Name = "ContactsGrid";
      this.ContactsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
      this.ContactsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
      this.ContactsGrid.PreviewInfo.ZoomFactor = 75;
      this.ContactsGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("ContactsGrid.PrintInfo.PageSettings")));
      this.ContactsGrid.RowDivider.Color = System.Drawing.Color.LightGray;
      this.ContactsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
      this.ContactsGrid.RowHeight = 15;
      this.ContactsGrid.Size = new System.Drawing.Size(775, 394);
      this.ContactsGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
      this.ContactsGrid.TabIndex = 1;
      this.ContactsGrid.Text = "MarksGrid";
      this.ContactsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ContactsGrid_FormatText);
      this.ContactsGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ContactsGrid_BeforeUpdate);
      this.ContactsGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ContactsGrid_KeyPress);
      this.ContactsGrid.PropBag = resources.GetString("ContactsGrid.PropBag");
      // 
      // TradingGroupDropdown
      // 
      this.TradingGroupDropdown.AllowColMove = true;
      this.TradingGroupDropdown.AllowColSelect = true;
      this.TradingGroupDropdown.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.AllRows;
      this.TradingGroupDropdown.AlternatingRows = false;
      this.TradingGroupDropdown.BackColor = System.Drawing.Color.GhostWhite;
      this.TradingGroupDropdown.CaptionHeight = 17;
      this.TradingGroupDropdown.CaptionStyle = style1;
      this.TradingGroupDropdown.ColumnCaptionHeight = 17;
      this.TradingGroupDropdown.ColumnFooterHeight = 17;
      this.TradingGroupDropdown.EmptyRows = true;
      this.TradingGroupDropdown.EvenRowStyle = style2;
      this.TradingGroupDropdown.FetchRowStyles = false;
      this.TradingGroupDropdown.FooterStyle = style3;
      this.TradingGroupDropdown.HeadingStyle = style4;
      this.TradingGroupDropdown.HighLightRowStyle = style5;
      this.TradingGroupDropdown.Images.Add(((System.Drawing.Image)(resources.GetObject("TradingGroupDropdown.Images"))));
      this.TradingGroupDropdown.Images.Add(((System.Drawing.Image)(resources.GetObject("TradingGroupDropdown.Images1"))));
      this.TradingGroupDropdown.Location = new System.Drawing.Point(69, 57);
      this.TradingGroupDropdown.Name = "TradingGroupDropdown";
      this.TradingGroupDropdown.OddRowStyle = style6;
      this.TradingGroupDropdown.RecordSelectorStyle = style7;
      this.TradingGroupDropdown.RowDivider.Color = System.Drawing.Color.LightGray;
      this.TradingGroupDropdown.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
      this.TradingGroupDropdown.RowHeight = 15;
      this.TradingGroupDropdown.RowSubDividerColor = System.Drawing.Color.DarkGray;
      this.TradingGroupDropdown.ScrollTips = false;
      this.TradingGroupDropdown.Size = new System.Drawing.Size(100, 178);
      this.TradingGroupDropdown.Style = style8;
      this.TradingGroupDropdown.TabIndex = 4;
      this.TradingGroupDropdown.TabStop = false;
      this.TradingGroupDropdown.Visible = false;
      this.TradingGroupDropdown.PropBag = resources.GetString("TradingGroupDropdown.PropBag");
      // 
      // StatusMessageLabel
      // 
      this.StatusMessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.StatusMessageLabel.AutoSize = true;
      this.StatusMessageLabel.Location = new System.Drawing.Point(8, 408);
      this.StatusMessageLabel.Name = "StatusMessageLabel";
      this.StatusMessageLabel.Size = new System.Drawing.Size(0, 13);
      this.StatusMessageLabel.TabIndex = 30;
      this.StatusMessageLabel.Tag = null;
      this.StatusMessageLabel.TextDetached = true;
      // 
      // PreBorrowContactsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(775, 434);
      this.Controls.Add(this.StatusMessageLabel);
      this.Controls.Add(this.TradingGroupDropdown);
      this.Controls.Add(this.ContactsGrid);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "PreBorrowContactsForm";
      this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 40);
      this.Text = "PreBorrow - Contacts";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreBorrowContactsForm_FormClosed);
      this.Load += new System.EventHandler(this.PreBorrowContactsForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.ContactsGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TradingGroupDropdown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private C1.Win.C1TrueDBGrid.C1TrueDBGrid ContactsGrid;
    private C1.Win.C1TrueDBGrid.C1TrueDBDropdown TradingGroupDropdown;
    private C1.Win.C1Input.C1Label StatusMessageLabel;

  }
}