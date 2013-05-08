namespace CentralClient
{
	partial class AdminLoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminLoginForm));
            this.StatusMessageTextBox = new C1.Win.C1Input.C1TextBox();
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.LoginRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            this.CancelRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            this.PasswordChange = new C1.Win.C1Ribbon.RibbonButton();
            this.c1Label1 = new C1.Win.C1Input.C1Label();
            this.c1Label2 = new C1.Win.C1Input.C1Label();
            this.LoginTextBox = new C1.Win.C1Input.C1TextBox();
            this.PasswordTextBox = new C1.Win.C1Input.C1TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.StatusMessageTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoginTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusMessageTextBox
            // 
            this.StatusMessageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusMessageTextBox.Location = new System.Drawing.Point(29, 57);
            this.StatusMessageTextBox.Multiline = true;
            this.StatusMessageTextBox.Name = "StatusMessageTextBox";
            this.StatusMessageTextBox.Size = new System.Drawing.Size(197, 48);
            this.StatusMessageTextBox.TabIndex = 6;
            this.StatusMessageTextBox.Tag = null;
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.LoginRibbonButton);
            this.c1StatusBar1.RightPaneItems.Add(this.CancelRibbonButton);
            this.c1StatusBar1.RightPaneItems.Add(this.PasswordChange);
            // 
            // LoginRibbonButton
            // 
            this.LoginRibbonButton.Name = "LoginRibbonButton";
            this.LoginRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("LoginRibbonButton.SmallImage")));
            this.LoginRibbonButton.Text = "Login";
            this.LoginRibbonButton.Click += new System.EventHandler(this.LoginRibbonButton_Click);
            // 
            // CancelRibbonButton
            // 
            this.CancelRibbonButton.Name = "CancelRibbonButton";
            this.CancelRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("CancelRibbonButton.SmallImage")));
            this.CancelRibbonButton.Text = "Cancel";
            this.CancelRibbonButton.Click += new System.EventHandler(this.CancelRibbonButton_Click);
            // 
            // PasswordChange
            // 
            this.PasswordChange.Name = "PasswordChange";
            this.PasswordChange.SmallImage = ((System.Drawing.Image)(resources.GetObject("PasswordChange.SmallImage")));
            this.PasswordChange.Text = "Change Password";
            this.PasswordChange.Click += new System.EventHandler(this.PasswordChange_Click);
            // 
            // c1Label1
            // 
            this.c1Label1.AutoSize = true;
            this.c1Label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(225)))), ((int)(((byte)(238)))));
            this.c1Label1.ForeColor = System.Drawing.Color.Black;
            this.c1Label1.Location = new System.Drawing.Point(22, 9);
            this.c1Label1.Name = "c1Label1";
            this.c1Label1.Size = new System.Drawing.Size(70, 13);
            this.c1Label1.TabIndex = 12;
            this.c1Label1.Tag = null;
            this.c1Label1.Text = "Username:";
            this.c1Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.c1Label1.TextDetached = true;
            this.c1Label1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            // 
            // c1Label2
            // 
            this.c1Label2.AutoSize = true;
            this.c1Label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(225)))), ((int)(((byte)(238)))));
            this.c1Label2.ForeColor = System.Drawing.Color.Black;
            this.c1Label2.Location = new System.Drawing.Point(26, 34);
            this.c1Label2.Name = "c1Label2";
            this.c1Label2.Size = new System.Drawing.Size(66, 13);
            this.c1Label2.TabIndex = 13;
            this.c1Label2.Tag = null;
            this.c1Label2.Text = "Password:";
            this.c1Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.c1Label2.TextDetached = true;
            this.c1Label2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.AutoSize = false;
            this.LoginTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoginTextBox.Location = new System.Drawing.Point(105, 7);
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(121, 19);
            this.LoginTextBox.TabIndex = 10;
            this.LoginTextBox.Tag = null;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.AcceptsReturn = true;
            this.PasswordTextBox.AutoSize = false;
            this.PasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PasswordTextBox.Location = new System.Drawing.Point(105, 32);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(121, 19);
            this.PasswordTextBox.TabIndex = 11;
            this.PasswordTextBox.Tag = null;
            this.PasswordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PasswordTextBox_KeyPress);
            // 
            // AdminLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 137);
            this.Controls.Add(this.c1Label1);
            this.Controls.Add(this.c1Label2);
            this.Controls.Add(this.LoginTextBox);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.c1StatusBar1);
            this.Controls.Add(this.StatusMessageTextBox);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminLoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin - Login";
            this.TopMost = true;
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Windows7;
            this.Load += new System.EventHandler(this.AdminLoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.StatusMessageTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoginTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private C1.Win.C1Input.C1TextBox StatusMessageTextBox;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Ribbon.RibbonButton LoginRibbonButton;
        private C1.Win.C1Ribbon.RibbonButton CancelRibbonButton;
        private C1.Win.C1Ribbon.RibbonButton PasswordChange;
        private C1.Win.C1Input.C1Label c1Label1;
        private C1.Win.C1Input.C1Label c1Label2;
        private C1.Win.C1Input.C1TextBox LoginTextBox;
        private C1.Win.C1Input.C1TextBox PasswordTextBox;
	}
}