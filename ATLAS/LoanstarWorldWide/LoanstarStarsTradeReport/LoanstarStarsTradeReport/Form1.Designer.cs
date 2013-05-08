namespace LoanstarStarsTradeReport
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
            this.InfoTextBox = new C1.Win.C1Input.C1TextBox();
            this.BizDateEdit = new C1.Win.C1Input.C1DateEdit();
            this.SubmitButton = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.InfoTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BizDateEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // InfoTextBox
            // 
            this.InfoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoTextBox.Location = new System.Drawing.Point(5, 40);
            this.InfoTextBox.Multiline = true;
            this.InfoTextBox.Name = "InfoTextBox";
            this.InfoTextBox.Size = new System.Drawing.Size(907, 321);
            this.InfoTextBox.TabIndex = 0;
            this.InfoTextBox.Tag = null;
            // 
            // BizDateEdit
            // 
            // 
            // 
            // 
            this.BizDateEdit.Calendar.DayNameLength = 1;
            this.BizDateEdit.CustomFormat = "yyyy-MM-dd";
            this.BizDateEdit.Location = new System.Drawing.Point(8, 6);
            this.BizDateEdit.Name = "BizDateEdit";
            this.BizDateEdit.Size = new System.Drawing.Size(200, 20);
            this.BizDateEdit.TabIndex = 1;
            this.BizDateEdit.Tag = null;
            this.BizDateEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(214, 4);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(75, 23);
            this.SubmitButton.TabIndex = 2;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 366);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.BizDateEdit);
            this.Controls.Add(this.InfoTextBox);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(5, 40, 5, 5);
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.InfoTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BizDateEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Input.C1TextBox InfoTextBox;
        private C1.Win.C1Input.C1DateEdit BizDateEdit;
        private C1.Win.C1Input.C1Button SubmitButton;
    }
}

