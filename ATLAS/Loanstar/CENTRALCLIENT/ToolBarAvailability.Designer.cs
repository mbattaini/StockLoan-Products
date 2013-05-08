namespace CentralClient
{
    partial class AvailabilityToolBar
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
          this.components = new System.ComponentModel.Container();
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AvailabilityToolBar));
          this.ShellAppBar = new LogicNP.ShellObjects.ShellAppBar(this.components);
          this.c1TrueDBGrid1 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
          ((System.ComponentModel.ISupportInitialize)(this.ShellAppBar)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid1)).BeginInit();
          this.SuspendLayout();
          // 
          // ShellAppBar
          // 
          this.ShellAppBar.AllowedDragDockingEdges = ((LogicNP.ShellObjects.DragDockingEdges)(((LogicNP.ShellObjects.DragDockingEdges.UnDocked | LogicNP.ShellObjects.DragDockingEdges.Left)
                      | LogicNP.ShellObjects.DragDockingEdges.Right)));
          this.ShellAppBar.DockingEdgeChanged += new System.EventHandler(this.ShellAppBar_DockingEdgeChanged);
          this.ShellAppBar.HostForm = this;
          // 
          // c1TrueDBGrid1
          // 
          this.c1TrueDBGrid1.AllowUpdate = false;
          this.c1TrueDBGrid1.AlternatingRows = true;
          this.c1TrueDBGrid1.CaptionHeight = 17;
          this.c1TrueDBGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
          this.c1TrueDBGrid1.EmptyRows = true;
          this.c1TrueDBGrid1.ExtendRightColumn = true;
          this.c1TrueDBGrid1.FilterBar = true;
          this.c1TrueDBGrid1.GroupByCaption = "Drag a column header here to group by that column";
          this.c1TrueDBGrid1.Images.Add(((System.Drawing.Image)(resources.GetObject("c1TrueDBGrid1.Images"))));
          this.c1TrueDBGrid1.Images.Add(((System.Drawing.Image)(resources.GetObject("c1TrueDBGrid1.Images1"))));
          this.c1TrueDBGrid1.Location = new System.Drawing.Point(0, 0);
          this.c1TrueDBGrid1.Name = "c1TrueDBGrid1";
          this.c1TrueDBGrid1.PreviewInfo.Location = new System.Drawing.Point(0, 0);
          this.c1TrueDBGrid1.PreviewInfo.Size = new System.Drawing.Size(0, 0);
          this.c1TrueDBGrid1.PreviewInfo.ZoomFactor = 75;
          this.c1TrueDBGrid1.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("c1TrueDBGrid1.PrintInfo.PageSettings")));
          this.c1TrueDBGrid1.RowHeight = 15;
          this.c1TrueDBGrid1.Size = new System.Drawing.Size(331, 273);
          this.c1TrueDBGrid1.TabIndex = 0;
          this.c1TrueDBGrid1.Text = "c1TrueDBGrid1";
          this.c1TrueDBGrid1.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Black;
          this.c1TrueDBGrid1.PropBag = resources.GetString("c1TrueDBGrid1.PropBag");
          // 
          // AvailabilityToolBar
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
          this.ClientSize = new System.Drawing.Size(331, 273);
          this.Controls.Add(this.c1TrueDBGrid1);
          this.DoubleBuffered = true;
          this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
          this.Name = "AvailabilityToolBar";
          this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Black;
          ((System.ComponentModel.ISupportInitialize)(this.ShellAppBar)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid1)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

        private LogicNP.ShellObjects.ShellAppBar ShellAppBar;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1TrueDBGrid1;
    }
}