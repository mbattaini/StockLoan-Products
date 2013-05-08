namespace CNSProjection0234
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RunButton = new System.Windows.Forms.Button();
            this.FileTextBox = new System.Windows.Forms.TextBox();
            this.CnsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Projections0158Button = new System.Windows.Forms.Button();
            this.DataBaseGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CnsGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataBaseGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RunButton);
            this.groupBox1.Controls.Add(this.FileTextBox);
            this.groupBox1.Controls.Add(this.CnsGrid);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
            this.groupBox1.Size = new System.Drawing.Size(826, 152);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "0234 CNS Projections";
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(250, 24);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(45, 19);
            this.RunButton.TabIndex = 4;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // FileTextBox
            // 
            this.FileTextBox.Location = new System.Drawing.Point(6, 23);
            this.FileTextBox.Name = "FileTextBox";
            this.FileTextBox.Size = new System.Drawing.Size(223, 20);
            this.FileTextBox.TabIndex = 3;
            this.FileTextBox.Text = "\\\\rsdmz001\\siac\\CNSPR.TXT.C00";
            this.FileTextBox.TextChanged += new System.EventHandler(this.FileTextBox_TextChanged);
            // 
            // CnsGrid
            // 
            this.CnsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CnsGrid.Location = new System.Drawing.Point(3, 63);
            this.CnsGrid.Name = "CnsGrid";
            this.CnsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.CnsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.CnsGrid.PreviewInfo.ZoomFactor = 75D;
            this.CnsGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("CnsGrid.PrintInfo.PageSettings")));
            this.CnsGrid.PropBag = resources.GetString("CnsGrid.PropBag");
            this.CnsGrid.Size = new System.Drawing.Size(820, 86);
            this.CnsGrid.TabIndex = 1;
            this.CnsGrid.Text = "c1TrueDBGrid1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Projections0158Button);
            this.groupBox2.Controls.Add(this.DataBaseGrid);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 50, 3, 3);
            this.groupBox2.Size = new System.Drawing.Size(826, 152);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "0158 CNS Projections";
            // 
            // Projections0158Button
            // 
            this.Projections0158Button.Location = new System.Drawing.Point(6, 25);
            this.Projections0158Button.Name = "Projections0158Button";
            this.Projections0158Button.Size = new System.Drawing.Size(45, 19);
            this.Projections0158Button.TabIndex = 4;
            this.Projections0158Button.Text = "Run";
            this.Projections0158Button.UseVisualStyleBackColor = true;
            this.Projections0158Button.Click += new System.EventHandler(this.Projections0158Button_Click);
            // 
            // DataBaseGrid
            // 
            this.DataBaseGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataBaseGrid.Location = new System.Drawing.Point(3, 63);
            this.DataBaseGrid.Name = "DataBaseGrid";
            this.DataBaseGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DataBaseGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DataBaseGrid.PreviewInfo.ZoomFactor = 75D;
            this.DataBaseGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DataBaseGrid.PrintInfo.PageSettings")));
            this.DataBaseGrid.PropBag = resources.GetString("DataBaseGrid.PropBag");
            this.DataBaseGrid.Size = new System.Drawing.Size(820, 86);
            this.DataBaseGrid.TabIndex = 1;
            this.DataBaseGrid.Text = "c1TrueDBGrid1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 481);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CnsGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataBaseGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid CnsGrid;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.TextBox FileTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Projections0158Button;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid DataBaseGrid;
    }
}

