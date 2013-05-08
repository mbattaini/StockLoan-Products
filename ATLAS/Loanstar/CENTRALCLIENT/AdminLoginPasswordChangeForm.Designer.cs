namespace CentralClient
{
    partial class AdminLoginPasswordChangeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminLoginPasswordChangeForm));
            this.OldPasswordTextBox = new C1.Win.C1Input.C1TextBox();
            this.NewTwoPasswordTextBox = new C1.Win.C1Input.C1TextBox();
            this.NewOnePasswordTextBox = new C1.Win.C1Input.C1TextBox();
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.SubmitRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            this.CancelRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            this.StatusMessageTextBox = new C1.Win.C1Input.C1TextBox();
            this.c1Label3 = new C1.Win.C1Input.C1Label();
            this.c1Label1 = new C1.Win.C1Input.C1Label();
            this.c1Label2 = new C1.Win.C1Input.C1Label();
            ((System.ComponentModel.ISupportInitialize)(this.OldPasswordTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewTwoPasswordTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewOnePasswordTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusMessageTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).BeginInit();
            this.SuspendLayout();
            // 
            // OldPasswordTextBox
            // 
            this.OldPasswordTextBox.AcceptsReturn = true;
            this.OldPasswordTextBox.AutoSize = false;
            this.OldPasswordTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(164)))), ((int)(((byte)(164)))));
            this.OldPasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OldPasswordTextBox.Location = new System.Drawing.Point(219, 12);
            this.OldPasswordTextBox.Name = "OldPasswordTextBox";
            this.OldPasswordTextBox.PasswordChar = '*';
            this.OldPasswordTextBox.Size = new System.Drawing.Size(129, 19);
            this.OldPasswordTextBox.TabIndex = 1;
            this.OldPasswordTextBox.Tag = null;
            this.OldPasswordTextBox.Leave += new System.EventHandler(this.OldPasswordTextBox_Leave);
            // 
            // NewTwoPasswordTextBox
            // 
            this.NewTwoPasswordTextBox.AcceptsReturn = true;
            this.NewTwoPasswordTextBox.AutoSize = false;
            this.NewTwoPasswordTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(164)))), ((int)(((byte)(164)))));
            this.NewTwoPasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NewTwoPasswordTextBox.Location = new System.Drawing.Point(219, 66);
            this.NewTwoPasswordTextBox.Name = "NewTwoPasswordTextBox";
            this.NewTwoPasswordTextBox.PasswordChar = '*';
            this.NewTwoPasswordTextBox.Size = new System.Drawing.Size(129, 19);
            this.NewTwoPasswordTextBox.TabIndex = 3;
            this.NewTwoPasswordTextBox.Tag = null;
            this.NewTwoPasswordTextBox.Leave += new System.EventHandler(this.NewTwoPasswordTextBox_Leave);
            // 
            // NewOnePasswordTextBox
            // 
            this.NewOnePasswordTextBox.AcceptsReturn = true;
            this.NewOnePasswordTextBox.AutoSize = false;
            this.NewOnePasswordTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(164)))), ((int)(((byte)(164)))));
            this.NewOnePasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NewOnePasswordTextBox.Location = new System.Drawing.Point(219, 38);
            this.NewOnePasswordTextBox.Name = "NewOnePasswordTextBox";
            this.NewOnePasswordTextBox.PasswordChar = '*';
            this.NewOnePasswordTextBox.Size = new System.Drawing.Size(129, 19);
            this.NewOnePasswordTextBox.TabIndex = 2;
            this.NewOnePasswordTextBox.Tag = null;
            this.NewOnePasswordTextBox.Leave += new System.EventHandler(this.NewTwoPasswordTextBox_Leave);
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.SubmitRibbonButton);
            this.c1StatusBar1.RightPaneItems.Add(this.CancelRibbonButton);
            // 
            // SubmitRibbonButton
            // 
            this.SubmitRibbonButton.Name = "SubmitRibbonButton";
            this.SubmitRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("SubmitRibbonButton.SmallImage")));
            this.SubmitRibbonButton.Text = "Submit";
            this.SubmitRibbonButton.Click += new System.EventHandler(this.SubmitRibbonButton_Click);
            // 
            // CancelRibbonButton
            // 
            this.CancelRibbonButton.Name = "CancelRibbonButton";
            this.CancelRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("CancelRibbonButton.SmallImage")));
            this.CancelRibbonButton.Text = "Cancel";
            this.CancelRibbonButton.Click += new System.EventHandler(this.CancelRibbonButton_Click);
            // 
            // StatusMessageTextBox
            // 
            this.StatusMessageTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(164)))), ((int)(((byte)(164)))));
            this.StatusMessageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusMessageTextBox.Location = new System.Drawing.Point(12, 98);
            this.StatusMessageTextBox.Multiline = true;
            this.StatusMessageTextBox.Name = "StatusMessageTextBox";
            this.StatusMessageTextBox.Size = new System.Drawing.Size(338, 40);
            this.StatusMessageTextBox.TabIndex = 13;
            this.StatusMessageTextBox.Tag = null;
            this.StatusMessageTextBox.TextDetached = true;
            // 
            // c1Label3
            // 
            this.c1Label3.AutoSize = true;
            this.c1Label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(225)))), ((int)(((byte)(238)))));
            this.c1Label3.ForeColor = System.Drawing.Color.Black;
            this.c1Label3.Location = new System.Drawing.Point(12, 40);
            this.c1Label3.Name = "c1Label3";
            this.c1Label3.Size = new System.Drawing.Size(94, 13);
            this.c1Label3.TabIndex = 17;
            this.c1Label3.Tag = null;
            this.c1Label3.Text = "New Password:";
            this.c1Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.c1Label3.TextDetached = true;
            this.c1Label3.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            // 
            // c1Label1
            // 
            this.c1Label1.AutoSize = true;
            this.c1Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(225)))), ((int)(((byte)(238)))));
            this.c1Label1.ForeColor = System.Drawing.Color.Black;
            this.c1Label1.Location = new System.Drawing.Point(12, 68);
            this.c1Label1.Name = "c1Label1";
            this.c1Label1.Size = new System.Drawing.Size(139, 13);
            this.c1Label1.TabIndex = 16;
            this.c1Label1.Tag = null;
            this.c1Label1.Text = "New Password: (again)";
            this.c1Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.c1Label1.TextDetached = true;
            this.c1Label1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            // 
            // c1Label2
            // 
            this.c1Label2.AutoSize = true;
            this.c1Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(225)))), ((int)(((byte)(238)))));
            this.c1Label2.ForeColor = System.Drawing.Color.Black;
            this.c1Label2.Location = new System.Drawing.Point(12, 14);
            this.c1Label2.Name = "c1Label2";
            this.c1Label2.Size = new System.Drawing.Size(89, 13);
            this.c1Label2.TabIndex = 15;
            this.c1Label2.Tag = null;
            this.c1Label2.Text = "Old Password:";
            this.c1Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.c1Label2.TextDetached = true;
            this.c1Label2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            // 
            // AdminLoginPasswordChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 185);
            this.Controls.Add(this.c1Label3);
            this.Controls.Add(this.c1Label1);
            this.Controls.Add(this.c1Label2);
            this.Controls.Add(this.StatusMessageTextBox);
            this.Controls.Add(this.c1StatusBar1);
            this.Controls.Add(this.NewOnePasswordTextBox);
            this.Controls.Add(this.NewTwoPasswordTextBox);
            this.Controls.Add(this.OldPasswordTextBox);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminLoginPasswordChangeForm";
            this.Text = "Admin - Login - Password Change";
            this.TopMost = true;
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Windows7;
            ((System.ComponentModel.ISupportInitialize)(this.OldPasswordTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewTwoPasswordTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewOnePasswordTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusMessageTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Input.C1TextBox OldPasswordTextBox;
        private C1.Win.C1Input.C1TextBox NewTwoPasswordTextBox;
        private C1.Win.C1Input.C1TextBox NewOnePasswordTextBox;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Input.C1TextBox StatusMessageTextBox;
        private C1.Win.C1Ribbon.RibbonButton SubmitRibbonButton;
        private C1.Win.C1Ribbon.RibbonButton CancelRibbonButton;
        private C1.Win.C1Input.C1Label c1Label3;
        private C1.Win.C1Input.C1Label c1Label1;
        private C1.Win.C1Input.C1Label c1Label2;
    }
}