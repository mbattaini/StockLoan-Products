namespace CentralClient
{
  partial class MainServerStatusEventsForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainServerStatusEventsForm));
      this.StatusGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
      ((System.ComponentModel.ISupportInitialize)(this.StatusGrid)).BeginInit();
      this.SuspendLayout();
      // 
      // StatusGrid
      // 
      this.StatusGrid.AllowAddNew = true;
      this.StatusGrid.AllowDelete = true;
      this.StatusGrid.CaptionHeight = 17;
      this.StatusGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
      this.StatusGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.StatusGrid.EmptyRows = true;
      this.StatusGrid.ExtendRightColumn = true;
      this.StatusGrid.GroupByCaption = "Drag a column header here to group by that column";
      this.StatusGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("StatusGrid.Images"))));
      this.StatusGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("StatusGrid.Images1"))));
      this.StatusGrid.Location = new System.Drawing.Point(0, 0);
      this.StatusGrid.Name = "StatusGrid";
      this.StatusGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
      this.StatusGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
      this.StatusGrid.PreviewInfo.ZoomFactor = 75;
      this.StatusGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("BookContactsGrid.PrintInfo.PageSettings")));
      this.StatusGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
      this.StatusGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
      this.StatusGrid.RowHeight = 15;
      this.StatusGrid.Size = new System.Drawing.Size(709, 272);
      this.StatusGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation;
      this.StatusGrid.TabIndex = 2;
      this.StatusGrid.Text = "c1TrueDBGrid2";
      this.StatusGrid.OwnerDrawCell += new C1.Win.C1TrueDBGrid.OwnerDrawCellEventHandler(this.BookContactsGrid_OwnerDrawCell);
      this.StatusGrid.PropBag = resources.GetString("StatusGrid.PropBag");
      // 
      // MainServerStatusEventsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
      this.ClientSize = new System.Drawing.Size(709, 272);
      this.Controls.Add(this.StatusGrid);
      this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Name = "MainServerStatusEventsForm";
      this.Text = "MainServerStatusEventsForm";
      this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Black;
      ((System.ComponentModel.ISupportInitialize)(this.StatusGrid)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private C1.Win.C1TrueDBGrid.C1TrueDBGrid StatusGrid;
  }
}