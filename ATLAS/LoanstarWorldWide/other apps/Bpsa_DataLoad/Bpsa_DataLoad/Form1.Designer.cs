namespace Bpsa_DataLoad
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
            this.Scanq5LoadTextBox = new System.Windows.Forms.TextBox();
            this.Scanq5LoadButton = new System.Windows.Forms.Button();
            this.RdsweepLoadButton = new System.Windows.Forms.Button();
            this.RdsweepLoadTextBox = new System.Windows.Forms.TextBox();
            this.SiacLoadButton = new System.Windows.Forms.Button();
            this.SiacLoadTextBox = new System.Windows.Forms.TextBox();
            this.SCANQ5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Scanq5LoadTextBox
            // 
            this.Scanq5LoadTextBox.Location = new System.Drawing.Point(80, 12);
            this.Scanq5LoadTextBox.Name = "Scanq5LoadTextBox";
            this.Scanq5LoadTextBox.Size = new System.Drawing.Size(235, 20);
            this.Scanq5LoadTextBox.TabIndex = 0;
            // 
            // Scanq5LoadButton
            // 
            this.Scanq5LoadButton.Location = new System.Drawing.Point(321, 10);
            this.Scanq5LoadButton.Name = "Scanq5LoadButton";
            this.Scanq5LoadButton.Size = new System.Drawing.Size(39, 25);
            this.Scanq5LoadButton.TabIndex = 1;
            this.Scanq5LoadButton.Text = "Load";
            this.Scanq5LoadButton.UseVisualStyleBackColor = true;
            this.Scanq5LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // RdsweepLoadButton
            // 
            this.RdsweepLoadButton.Location = new System.Drawing.Point(322, 36);
            this.RdsweepLoadButton.Name = "RdsweepLoadButton";
            this.RdsweepLoadButton.Size = new System.Drawing.Size(39, 25);
            this.RdsweepLoadButton.TabIndex = 3;
            this.RdsweepLoadButton.Text = "Load";
            this.RdsweepLoadButton.UseVisualStyleBackColor = true;
            this.RdsweepLoadButton.Click += new System.EventHandler(this.RdsweepLoadButton_Click);
            // 
            // RdsweepLoadTextBox
            // 
            this.RdsweepLoadTextBox.Location = new System.Drawing.Point(81, 38);
            this.RdsweepLoadTextBox.Name = "RdsweepLoadTextBox";
            this.RdsweepLoadTextBox.Size = new System.Drawing.Size(235, 20);
            this.RdsweepLoadTextBox.TabIndex = 2;
            // 
            // SiacLoadButton
            // 
            this.SiacLoadButton.Location = new System.Drawing.Point(322, 62);
            this.SiacLoadButton.Name = "SiacLoadButton";
            this.SiacLoadButton.Size = new System.Drawing.Size(39, 25);
            this.SiacLoadButton.TabIndex = 5;
            this.SiacLoadButton.Text = "Load";
            this.SiacLoadButton.UseVisualStyleBackColor = true;
            this.SiacLoadButton.Click += new System.EventHandler(this.SiacLoadButton_Click);
            // 
            // SiacLoadTextBox
            // 
            this.SiacLoadTextBox.Location = new System.Drawing.Point(81, 64);
            this.SiacLoadTextBox.Name = "SiacLoadTextBox";
            this.SiacLoadTextBox.Size = new System.Drawing.Size(235, 20);
            this.SiacLoadTextBox.TabIndex = 4;
            // 
            // SCANQ5
            // 
            this.SCANQ5.AutoSize = true;
            this.SCANQ5.Location = new System.Drawing.Point(21, 16);
            this.SCANQ5.Name = "SCANQ5";
            this.SCANQ5.Size = new System.Drawing.Size(53, 13);
            this.SCANQ5.TabIndex = 6;
            this.SCANQ5.Text = "SCANQ5:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "RDSWEEP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "SIAC:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 101);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SCANQ5);
            this.Controls.Add(this.SiacLoadButton);
            this.Controls.Add(this.SiacLoadTextBox);
            this.Controls.Add(this.RdsweepLoadButton);
            this.Controls.Add(this.RdsweepLoadTextBox);
            this.Controls.Add(this.Scanq5LoadButton);
            this.Controls.Add(this.Scanq5LoadTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Scanq5LoadTextBox;
        private System.Windows.Forms.Button Scanq5LoadButton;
        private System.Windows.Forms.Button RdsweepLoadButton;
        private System.Windows.Forms.TextBox RdsweepLoadTextBox;
        private System.Windows.Forms.Button SiacLoadButton;
        private System.Windows.Forms.TextBox SiacLoadTextBox;
        private System.Windows.Forms.Label SCANQ5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

