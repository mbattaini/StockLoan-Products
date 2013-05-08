namespace BPSA_Viewer
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
            this.SecurityIdTextBox = new C1.Win.C1Input.C1TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LookupButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SecurityIdTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SecurityIdTextBox
            // 
            this.SecurityIdTextBox.Location = new System.Drawing.Point(101, 8);
            this.SecurityIdTextBox.Name = "SecurityIdTextBox";
            this.SecurityIdTextBox.Size = new System.Drawing.Size(178, 22);
            this.SecurityIdTextBox.TabIndex = 1;
            this.SecurityIdTextBox.Tag = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "ADP Number";
            // 
            // LookupButton
            // 
            this.LookupButton.Location = new System.Drawing.Point(285, 7);
            this.LookupButton.Name = "LookupButton";
            this.LookupButton.Size = new System.Drawing.Size(75, 23);
            this.LookupButton.TabIndex = 3;
            this.LookupButton.Text = "Lookup";
            this.LookupButton.UseVisualStyleBackColor = true;
            this.LookupButton.Click += new System.EventHandler(this.LookupButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 580);
            this.Controls.Add(this.LookupButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SecurityIdTextBox);
            this.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(20, 50, 20, 20);
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.SecurityIdTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Input.C1TextBox SecurityIdTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button LookupButton;        

    }
}

