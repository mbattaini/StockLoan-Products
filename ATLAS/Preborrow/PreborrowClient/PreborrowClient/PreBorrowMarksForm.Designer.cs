namespace PreborrowClient
{
  partial class PreBorrowMarksForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreBorrowMarksForm));
      C1.Win.C1TrueDBGrid.Style style1 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style2 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style3 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style4 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style5 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style6 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style7 = new C1.Win.C1TrueDBGrid.Style();
      C1.Win.C1TrueDBGrid.Style style8 = new C1.Win.C1TrueDBGrid.Style();
      this.MarksGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
      this.DefaultMarkUpLabel = new C1.Win.C1Input.C1Label();
      this.DefaultMarkUpTextBox = new C1.Win.C1Input.C1TextBox();
      this.TradingGroupDropdown = new C1.Win.C1TrueDBGrid.C1TrueDBDropdown();
      this.StatusMessageLabel = new C1.Win.C1Input.C1Label();
      ((System.ComponentModel.ISupportInitialize)(this.MarksGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.DefaultMarkUpLabel)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.DefaultMarkUpTextBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TradingGroupDropdown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).BeginInit();
      this.SuspendLayout();
      // 
      // MarksGrid
      // 
      this.MarksGrid.AllowAddNew = true;
      this.MarksGrid.AllowDelete = true;
      this.MarksGrid.CaptionHeight = 17;
      this.MarksGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
      this.MarksGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MarksGrid.EmptyRows = true;
      this.MarksGrid.ExtendRightColumn = true;
      this.MarksGrid.GroupByCaption = "Drag a column header here to group by that column";
      this.MarksGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("MarksGrid.Images"))));
      this.MarksGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("MarksGrid.Images1"))));
      this.MarksGrid.Location = new System.Drawing.Point(0, 40);
      this.MarksGrid.Name = "MarksGrid";
      this.MarksGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
      this.MarksGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
      this.MarksGrid.PreviewInfo.ZoomFactor = 75;
      this.MarksGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("MarksGrid.PrintInfo.PageSettings")));
      this.MarksGrid.RowDivider.Color = System.Drawing.Color.LightGray;
      this.MarksGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
      this.MarksGrid.RowHeight = 15;
      this.MarksGrid.Size = new System.Drawing.Size(562, 495);
      this.MarksGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
      this.MarksGrid.TabIndex = 0;
      this.MarksGrid.Text = "MarksGrid";
      this.MarksGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.MarksGrid_FormatText);
      this.MarksGrid.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.MarksGrid_BeforeDelete);
      this.MarksGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.MarksGrid_BeforeUpdate);
      this.MarksGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MarksGrid_KeyPress);
      this.MarksGrid.PropBag = resources.GetString("MarksGrid.PropBag");
      // 
      // DefaultMarkUpLabel
      // 
      this.DefaultMarkUpLabel.AutoSize = true;
      this.DefaultMarkUpLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.DefaultMarkUpLabel.Location = new System.Drawing.Point(3, 9);
      this.DefaultMarkUpLabel.Name = "DefaultMarkUpLabel";
      this.DefaultMarkUpLabel.Size = new System.Drawing.Size(110, 13);
      this.DefaultMarkUpLabel.TabIndex = 1;
      this.DefaultMarkUpLabel.Tag = null;
      this.DefaultMarkUpLabel.Text = "Default Markup:";
      this.DefaultMarkUpLabel.TextDetached = true;
      // 
      // DefaultMarkUpTextBox
      // 
      this.DefaultMarkUpTextBox.FormatType = C1.Win.C1Input.FormatTypeEnum.UseEvent;
      this.DefaultMarkUpTextBox.Location = new System.Drawing.Point(119, 6);
      this.DefaultMarkUpTextBox.Name = "DefaultMarkUpTextBox";
      this.DefaultMarkUpTextBox.Size = new System.Drawing.Size(142, 21);
      this.DefaultMarkUpTextBox.TabIndex = 2;
      this.DefaultMarkUpTextBox.Tag = null;
      this.DefaultMarkUpTextBox.Formatting += new C1.Win.C1Input.FormatEventHandler(this.DefaultMarkUpTextBox_Formatting);
      this.DefaultMarkUpTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DefaultMarkUpTextBox_KeyPress);
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
      this.TradingGroupDropdown.Location = new System.Drawing.Point(47, 80);
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
      this.TradingGroupDropdown.TabIndex = 3;
      this.TradingGroupDropdown.TabStop = false;
      this.TradingGroupDropdown.Text = "c1TrueDBDropdown1";
      this.TradingGroupDropdown.Visible = false;
      this.TradingGroupDropdown.PropBag = resources.GetString("TradingGroupDropdown.PropBag");
      // 
      // StatusMessageLabel
      // 
      this.StatusMessageLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.StatusMessageLabel.AutoSize = true;
      this.StatusMessageLabel.Location = new System.Drawing.Point(9, 548);
      this.StatusMessageLabel.Name = "StatusMessageLabel";
      this.StatusMessageLabel.Size = new System.Drawing.Size(0, 13);
      this.StatusMessageLabel.TabIndex = 29;
      this.StatusMessageLabel.Tag = null;
      this.StatusMessageLabel.TextDetached = true;
      // 
      // PreBorrowMarksForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(562, 575);
      this.Controls.Add(this.StatusMessageLabel);
      this.Controls.Add(this.TradingGroupDropdown);
      this.Controls.Add(this.DefaultMarkUpTextBox);
      this.Controls.Add(this.DefaultMarkUpLabel);
      this.Controls.Add(this.MarksGrid);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "PreBorrowMarksForm";
      this.Padding = new System.Windows.Forms.Padding(0, 40, 0, 40);
      this.Text = "PreBorrow - GroupCode Marks";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreBorrowMarksForm_FormClosed);
      this.Load += new System.EventHandler(this.PreBorrowMarksForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.MarksGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.DefaultMarkUpLabel)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.DefaultMarkUpTextBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TradingGroupDropdown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private C1.Win.C1TrueDBGrid.C1TrueDBGrid MarksGrid;
    private C1.Win.C1Input.C1Label DefaultMarkUpLabel;
    private C1.Win.C1Input.C1TextBox DefaultMarkUpTextBox;
    private C1.Win.C1TrueDBGrid.C1TrueDBDropdown TradingGroupDropdown;
    private C1.Win.C1Input.C1Label StatusMessageLabel;
  }
}