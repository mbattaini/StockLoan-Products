namespace CentralClient
{
    partial class HelpAboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpAboutForm));
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.c1Label1 = new C1.Win.C1Input.C1Label();
            this.UserIdLabel = new C1.Win.C1Input.C1Label();
            this.VersionIdLabel = new C1.Win.C1Input.C1Label();
            this.c1Label4 = new C1.Win.C1Input.C1Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserIdLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VersionIdLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 121);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.Size = new System.Drawing.Size(341, 23);
            // 
            // c1Label1
            // 
            this.c1Label1.AutoSize = true;
            this.c1Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(225)))), ((int)(((byte)(238)))));
            this.c1Label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label1.ForeColor = System.Drawing.Color.Black;
            this.c1Label1.Location = new System.Drawing.Point(16, 16);
            this.c1Label1.Name = "c1Label1";
            this.c1Label1.Size = new System.Drawing.Size(56, 13);
            this.c1Label1.TabIndex = 2;
            this.c1Label1.Tag = null;
            this.c1Label1.Text = "User ID:";
            this.c1Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.c1Label1.TextDetached = true;
            this.c1Label1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            // 
            // UserIdLabel
            // 
            this.UserIdLabel.AutoSize = true;
            this.UserIdLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(225)))), ((int)(((byte)(238)))));
            this.UserIdLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UserIdLabel.ForeColor = System.Drawing.Color.Black;
            this.UserIdLabel.Location = new System.Drawing.Point(97, 16);
            this.UserIdLabel.Name = "UserIdLabel";
            this.UserIdLabel.Size = new System.Drawing.Size(0, 13);
            this.UserIdLabel.TabIndex = 3;
            this.UserIdLabel.Tag = null;
            this.UserIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UserIdLabel.TextDetached = true;
            this.UserIdLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            // 
            // VersionIdLabel
            // 
            this.VersionIdLabel.AutoSize = true;
            this.VersionIdLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(225)))), ((int)(((byte)(238)))));
            this.VersionIdLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.VersionIdLabel.ForeColor = System.Drawing.Color.Black;
            this.VersionIdLabel.Location = new System.Drawing.Point(97, 45);
            this.VersionIdLabel.Name = "VersionIdLabel";
            this.VersionIdLabel.Size = new System.Drawing.Size(0, 13);
            this.VersionIdLabel.TabIndex = 5;
            this.VersionIdLabel.Tag = null;
            this.VersionIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.VersionIdLabel.TextDetached = true;
            this.VersionIdLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            // 
            // c1Label4
            // 
            this.c1Label4.AutoSize = true;
            this.c1Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(225)))), ((int)(((byte)(238)))));
            this.c1Label4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1Label4.ForeColor = System.Drawing.Color.Black;
            this.c1Label4.Location = new System.Drawing.Point(16, 45);
            this.c1Label4.Name = "c1Label4";
            this.c1Label4.Size = new System.Drawing.Size(73, 13);
            this.c1Label4.TabIndex = 4;
            this.c1Label4.Tag = null;
            this.c1Label4.Text = "Version ID:";
            this.c1Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.c1Label4.TextDetached = true;
            this.c1Label4.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(190, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 117);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // HelpAboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 144);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.VersionIdLabel);
            this.Controls.Add(this.c1Label4);
            this.Controls.Add(this.UserIdLabel);
            this.Controls.Add(this.c1Label1);
            this.Controls.Add(this.c1StatusBar1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(349, 173);
            this.MinimumSize = new System.Drawing.Size(349, 173);
            this.Name = "HelpAboutForm";
            this.Text = "Help - About";
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Windows7;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HelpAboutForm_FormClosed);
            this.Load += new System.EventHandler(this.HelpAboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserIdLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VersionIdLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Input.C1Label c1Label1;
        private C1.Win.C1Input.C1Label UserIdLabel;
        private C1.Win.C1Input.C1Label VersionIdLabel;
        private C1.Win.C1Input.C1Label c1Label4;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}