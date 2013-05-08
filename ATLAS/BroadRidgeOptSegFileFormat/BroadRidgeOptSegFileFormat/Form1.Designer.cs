namespace BroadRidgeOptSegFileFormat
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
            this.BizDateEdit = new C1.Win.C1Input.C1DateEdit();
            this.FileTextBox = new C1.Win.C1Input.C1TextBox();
            this.SubmitButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BizDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // BizDateEdit
            // 
            // 
            // 
            // 
            this.BizDateEdit.Calendar.DayNameLength = 1;
            this.BizDateEdit.Location = new System.Drawing.Point(12, 12);
            this.BizDateEdit.Name = "BizDateEdit";
            this.BizDateEdit.Size = new System.Drawing.Size(224, 20);
            this.BizDateEdit.TabIndex = 0;
            this.BizDateEdit.Tag = null;
            this.BizDateEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // FileTextBox
            // 
            this.FileTextBox.Location = new System.Drawing.Point(12, 51);
            this.FileTextBox.Multiline = true;
            this.FileTextBox.Name = "FileTextBox";
            this.FileTextBox.Size = new System.Drawing.Size(717, 525);
            this.FileTextBox.TabIndex = 1;
            this.FileTextBox.Tag = null;
            this.FileTextBox.TextDetached = true;
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(653, 12);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(76, 20);
            this.SubmitButton.TabIndex = 2;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 588);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.FileTextBox);
            this.Controls.Add(this.BizDateEdit);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.BizDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FileTextBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Input.C1DateEdit BizDateEdit;
        private C1.Win.C1Input.C1TextBox FileTextBox;
        private System.Windows.Forms.Button SubmitButton;
    }
}

