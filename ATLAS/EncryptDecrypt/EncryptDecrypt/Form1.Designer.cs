namespace EncryptDecrypt
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
            this.Encrypted = new System.Windows.Forms.TextBox();
            this.Decrypted = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Encrypted
            // 
            this.Encrypted.Location = new System.Drawing.Point(12, 12);
            this.Encrypted.Name = "Encrypted";
            this.Encrypted.Size = new System.Drawing.Size(372, 20);
            this.Encrypted.TabIndex = 0;
            this.Encrypted.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Encrypted_KeyPress);
            // 
            // Decrypted
            // 
            this.Decrypted.Location = new System.Drawing.Point(12, 38);
            this.Decrypted.Name = "Decrypted";
            this.Decrypted.Size = new System.Drawing.Size(372, 20);
            this.Decrypted.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 262);
            this.Controls.Add(this.Decrypted);
            this.Controls.Add(this.Encrypted);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Encrypted;
        private System.Windows.Forms.TextBox Decrypted;
    }
}

