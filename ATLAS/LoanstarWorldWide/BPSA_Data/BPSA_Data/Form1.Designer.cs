namespace BPSA_Data
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
            this.BoxGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.LookupButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.BoxGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // BoxGrid
            // 
            this.BoxGrid.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.Inverted;
            this.BoxGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BoxGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("BoxGrid.Images"))));
            this.BoxGrid.Location = new System.Drawing.Point(0, 60);
            this.BoxGrid.Name = "BoxGrid";
            this.BoxGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.BoxGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.BoxGrid.PreviewInfo.ZoomFactor = 75D;
            this.BoxGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("c1TrueDBGrid1.PrintInfo.PageSettings")));
            this.BoxGrid.PropBag = resources.GetString("BoxGrid.PropBag");
            this.BoxGrid.Size = new System.Drawing.Size(856, 529);
            this.BoxGrid.TabIndex = 0;
            this.BoxGrid.Text = "c1TrueDBGrid1";
            this.BoxGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
            // 
            // LookupButton
            // 
            this.LookupButton.Location = new System.Drawing.Point(236, 5);
            this.LookupButton.Name = "LookupButton";
            this.LookupButton.Size = new System.Drawing.Size(63, 21);
            this.LookupButton.TabIndex = 2;
            this.LookupButton.Text = "Look Up";
            this.LookupButton.UseVisualStyleBackColor = true;
            this.LookupButton.Click += new System.EventHandler(this.LookupButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(225, 20);
            this.textBox1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 589);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.LookupButton);
            this.Controls.Add(this.BoxGrid);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(0, 60, 0, 0);
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.BoxGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid BoxGrid;
        private System.Windows.Forms.Button LookupButton;
        private System.Windows.Forms.TextBox textBox1;
    }
}

