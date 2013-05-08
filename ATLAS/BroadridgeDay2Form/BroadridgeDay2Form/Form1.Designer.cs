namespace BroadridgeDay2Form
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ExcessGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.ApibalGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.TransferGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            ((System.ComponentModel.ISupportInitialize)(this.ExcessGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ApibalGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransferGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ExcessGrid
            // 
            this.ExcessGrid.Location = new System.Drawing.Point(12, 27);
            this.ExcessGrid.Name = "ExcessGrid";
            this.ExcessGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.ExcessGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.ExcessGrid.PreviewInfo.ZoomFactor = 75D;
            this.ExcessGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("c1TrueDBGrid1.PrintInfo.PageSettings")));
            this.ExcessGrid.PropBag = resources.GetString("ExcessGrid.PropBag");
            this.ExcessGrid.Size = new System.Drawing.Size(940, 150);
            this.ExcessGrid.TabIndex = 0;
            this.ExcessGrid.Text = "c1TrueDBGrid1";
            // 
            // ApibalGrid
            // 
            this.ApibalGrid.Location = new System.Drawing.Point(12, 183);
            this.ApibalGrid.Name = "ApibalGrid";
            this.ApibalGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.ApibalGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.ApibalGrid.PreviewInfo.ZoomFactor = 75D;
            this.ApibalGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("c1TrueDBGrid2.PrintInfo.PageSettings")));
            this.ApibalGrid.PropBag = resources.GetString("ApibalGrid.PropBag");
            this.ApibalGrid.Size = new System.Drawing.Size(940, 150);
            this.ApibalGrid.TabIndex = 1;
            this.ApibalGrid.Text = "c1TrueDBGrid2";
            // 
            // TransferGrid
            // 
            this.TransferGrid.Location = new System.Drawing.Point(12, 339);
            this.TransferGrid.Name = "TransferGrid";
            this.TransferGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.TransferGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.TransferGrid.PreviewInfo.ZoomFactor = 75D;
            this.TransferGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("c1TrueDBGrid3.PrintInfo.PageSettings")));
            this.TransferGrid.PropBag = resources.GetString("TransferGrid.PropBag");
            this.TransferGrid.Size = new System.Drawing.Size(940, 150);
            this.TransferGrid.TabIndex = 2;
            this.TransferGrid.Text = "c1TrueDBGrid3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 502);
            this.Controls.Add(this.TransferGrid);
            this.Controls.Add(this.ApibalGrid);
            this.Controls.Add(this.ExcessGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ExcessGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ApibalGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransferGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid ExcessGrid;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid ApibalGrid;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid TransferGrid;
    }
}

