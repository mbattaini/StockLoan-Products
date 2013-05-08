namespace CentralClient
{
    partial class ErrorMessageForm
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorMessageForm));
          this.ErrorGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
          ((System.ComponentModel.ISupportInitialize)(this.ErrorGrid)).BeginInit();
          this.SuspendLayout();
          // 
          // ErrorGrid
          // 
          this.ErrorGrid.CaptionHeight = 17;
          this.ErrorGrid.Dock = System.Windows.Forms.DockStyle.Fill;
          this.ErrorGrid.EmptyRows = true;
          this.ErrorGrid.ExtendRightColumn = true;
          this.ErrorGrid.GroupByCaption = "Drag a column header here to group by that column";
          this.ErrorGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ErrorGrid.Images"))));
          this.ErrorGrid.Location = new System.Drawing.Point(0, 0);
          this.ErrorGrid.Name = "ErrorGrid";
          this.ErrorGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
          this.ErrorGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
          this.ErrorGrid.PreviewInfo.ZoomFactor = 75;
          this.ErrorGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("ErrorGrid.PrintInfo.PageSettings")));
          this.ErrorGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
          this.ErrorGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
          this.ErrorGrid.RowHeight = 15;
          this.ErrorGrid.Size = new System.Drawing.Size(341, 194);
          this.ErrorGrid.TabIndex = 0;
          this.ErrorGrid.Text = "c1TrueDBGrid1";
          this.ErrorGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Black;
          this.ErrorGrid.PropBag = resources.GetString("ErrorGrid.PropBag");
          // 
          // ErrorMessageForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
          this.ClientSize = new System.Drawing.Size(341, 194);
          this.Controls.Add(this.ErrorGrid);
          this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.Name = "ErrorMessageForm";
          this.Text = "Errors";
          this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Black;
          this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ErrorMessageForm_FormClosing);
          ((System.ComponentModel.ISupportInitialize)(this.ErrorGrid)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid ErrorGrid;
    }
}