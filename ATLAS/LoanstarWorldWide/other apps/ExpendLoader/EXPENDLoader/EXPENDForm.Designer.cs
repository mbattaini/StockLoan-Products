namespace EXPENDLoader
{
    partial class EXPENDForm
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
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.FileNameTextBox = new System.Windows.Forms.TextBox();
            this.FilePathLabel = new System.Windows.Forms.Label();
            this.LoadExpendFileButton = new System.Windows.Forms.Button();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.FileDateTextBox = new System.Windows.Forms.TextBox();
            this.BizDateLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Location = new System.Drawing.Point(126, 58);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.Size = new System.Drawing.Size(271, 20);
            this.FilePathTextBox.TabIndex = 3;
            // 
            // FileNameTextBox
            // 
            this.FileNameTextBox.Location = new System.Drawing.Point(126, 91);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.Size = new System.Drawing.Size(271, 20);
            this.FileNameTextBox.TabIndex = 5;
            // 
            // FilePathLabel
            // 
            this.FilePathLabel.AutoSize = true;
            this.FilePathLabel.Location = new System.Drawing.Point(22, 61);
            this.FilePathLabel.Name = "FilePathLabel";
            this.FilePathLabel.Size = new System.Drawing.Size(98, 13);
            this.FilePathLabel.TabIndex = 2;
            this.FilePathLabel.Text = "EXPEND File Path:";
            // 
            // LoadExpendFileButton
            // 
            this.LoadExpendFileButton.Location = new System.Drawing.Point(127, 136);
            this.LoadExpendFileButton.Name = "LoadExpendFileButton";
            this.LoadExpendFileButton.Size = new System.Drawing.Size(270, 39);
            this.LoadExpendFileButton.TabIndex = 6;
            this.LoadExpendFileButton.Text = "&Load EXPEND File";
            this.LoadExpendFileButton.UseVisualStyleBackColor = true;
            this.LoadExpendFileButton.Click += new System.EventHandler(this.LoadExpendFileButton_Click);
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Location = new System.Drawing.Point(15, 94);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(105, 13);
            this.FileNameLabel.TabIndex = 4;
            this.FileNameLabel.Text = "EXPEND FIle Name:";
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageLabel.Location = new System.Drawing.Point(124, 194);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(88, 13);
            this.MessageLabel.TabIndex = 7;
            this.MessageLabel.Tag = "";
            this.MessageLabel.Text = "MessageLabel";
            // 
            // FileDateTextBox
            // 
            this.FileDateTextBox.Location = new System.Drawing.Point(126, 22);
            this.FileDateTextBox.Name = "FileDateTextBox";
            this.FileDateTextBox.Size = new System.Drawing.Size(271, 20);
            this.FileDateTextBox.TabIndex = 1;
            // 
            // BizDateLabel
            // 
            this.BizDateLabel.AutoSize = true;
            this.BizDateLabel.Location = new System.Drawing.Point(21, 25);
            this.BizDateLabel.Name = "BizDateLabel";
            this.BizDateLabel.Size = new System.Drawing.Size(99, 13);
            this.BizDateLabel.TabIndex = 0;
            this.BizDateLabel.Text = "EXPEND File Date:";
            // 
            // EXPENDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 232);
            this.Controls.Add(this.BizDateLabel);
            this.Controls.Add(this.FileDateTextBox);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.FileNameLabel);
            this.Controls.Add(this.LoadExpendFileButton);
            this.Controls.Add(this.FilePathLabel);
            this.Controls.Add(this.FileNameTextBox);
            this.Controls.Add(this.FilePathTextBox);
            this.Name = "EXPENDForm";
            this.Text = "EXPEND File -- Parser and Loader";
            this.Load += new System.EventHandler(this.EXPENDForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.TextBox FileNameTextBox;
        private System.Windows.Forms.Label FilePathLabel;
        private System.Windows.Forms.Button LoadExpendFileButton;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.TextBox FileDateTextBox;
        private System.Windows.Forms.Label BizDateLabel;
    }
}

