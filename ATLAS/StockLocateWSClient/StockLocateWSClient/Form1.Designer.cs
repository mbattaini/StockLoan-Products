namespace StockLocateWSClient
{
    partial class LocatesTestForm
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
            this.SubmitTextBox = new System.Windows.Forms.TextBox();
            this.ClientIdTextBox = new System.Windows.Forms.TextBox();
            this.EncryptionKeyTextBox = new System.Windows.Forms.TextBox();
            this.SubmitLocatesButton = new System.Windows.Forms.Button();
            this.ViewLocatesButton = new System.Windows.Forms.Button();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.AnswersTextBox = new System.Windows.Forms.TextBox();
            this.AddressTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SubmitTextBox
            // 
            this.SubmitTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.SubmitTextBox.Location = new System.Drawing.Point(0, 100);
            this.SubmitTextBox.Multiline = true;
            this.SubmitTextBox.Name = "SubmitTextBox";
            this.SubmitTextBox.Size = new System.Drawing.Size(614, 182);
            this.SubmitTextBox.TabIndex = 0;
            // 
            // ClientIdTextBox
            // 
            this.ClientIdTextBox.Location = new System.Drawing.Point(12, 27);
            this.ClientIdTextBox.Name = "ClientIdTextBox";
            this.ClientIdTextBox.Size = new System.Drawing.Size(102, 20);
            this.ClientIdTextBox.TabIndex = 1;
            this.ClientIdTextBox.Text = "641";
            // 
            // EncryptionKeyTextBox
            // 
            this.EncryptionKeyTextBox.Location = new System.Drawing.Point(133, 27);
            this.EncryptionKeyTextBox.Name = "EncryptionKeyTextBox";
            this.EncryptionKeyTextBox.Size = new System.Drawing.Size(102, 20);
            this.EncryptionKeyTextBox.TabIndex = 2;
            this.EncryptionKeyTextBox.Text = "3aFe7qAu9o";
            // 
            // SubmitLocatesButton
            // 
            this.SubmitLocatesButton.Location = new System.Drawing.Point(241, 25);
            this.SubmitLocatesButton.Name = "SubmitLocatesButton";
            this.SubmitLocatesButton.Size = new System.Drawing.Size(75, 23);
            this.SubmitLocatesButton.TabIndex = 3;
            this.SubmitLocatesButton.Text = "Submit";
            this.SubmitLocatesButton.UseVisualStyleBackColor = true;
            this.SubmitLocatesButton.Click += new System.EventHandler(this.SubmitLocatesButton_Click);
            // 
            // ViewLocatesButton
            // 
            this.ViewLocatesButton.Location = new System.Drawing.Point(322, 25);
            this.ViewLocatesButton.Name = "ViewLocatesButton";
            this.ViewLocatesButton.Size = new System.Drawing.Size(75, 23);
            this.ViewLocatesButton.TabIndex = 5;
            this.ViewLocatesButton.Text = "View";
            this.ViewLocatesButton.UseVisualStyleBackColor = true;
            this.ViewLocatesButton.Click += new System.EventHandler(this.ViewLocatesButton_Click);
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(432, 30);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(0, 13);
            this.TimeLabel.TabIndex = 6;
            // 
            // AnswersTextBox
            // 
            this.AnswersTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnswersTextBox.Location = new System.Drawing.Point(0, 282);
            this.AnswersTextBox.Multiline = true;
            this.AnswersTextBox.Name = "AnswersTextBox";
            this.AnswersTextBox.Size = new System.Drawing.Size(614, 236);
            this.AnswersTextBox.TabIndex = 7;
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.Location = new System.Drawing.Point(12, 63);
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.Size = new System.Drawing.Size(385, 20);
            this.AddressTextBox.TabIndex = 8;
            this.AddressTextBox.Text = "https://webservices.staging.penson.com/stocklocate.asmx";
            // 
            // LocatesTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 518);
            this.Controls.Add(this.AddressTextBox);
            this.Controls.Add(this.AnswersTextBox);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.ViewLocatesButton);
            this.Controls.Add(this.SubmitLocatesButton);
            this.Controls.Add(this.EncryptionKeyTextBox);
            this.Controls.Add(this.ClientIdTextBox);
            this.Controls.Add(this.SubmitTextBox);
            this.Name = "LocatesTestForm";
            this.Padding = new System.Windows.Forms.Padding(0, 100, 0, 0);
            this.Text = "Locates Test Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SubmitTextBox;
        private System.Windows.Forms.TextBox ClientIdTextBox;
        private System.Windows.Forms.TextBox EncryptionKeyTextBox;
        private System.Windows.Forms.Button SubmitLocatesButton;
        private System.Windows.Forms.Button ViewLocatesButton;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.TextBox AnswersTextBox;
        private System.Windows.Forms.TextBox AddressTextBox;
    }
}

