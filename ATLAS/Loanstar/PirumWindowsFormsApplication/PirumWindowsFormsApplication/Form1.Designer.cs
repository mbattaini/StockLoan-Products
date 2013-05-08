namespace PirumWindowsFormsApplication
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
            this.c1Label1 = new C1.Win.C1Input.C1Label();
            this.c1Label2 = new C1.Win.C1Input.C1Label();
            this.BookGroupTextBox = new C1.Win.C1Input.C1TextBox();
            this.StatusTextBox = new C1.Win.C1Input.C1TextBox();
            this.BizDateEdit = new C1.Win.C1Input.C1DateEdit();
            this.QueryButton = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BizDateEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // c1Label1
            // 
            this.c1Label1.AutoSize = true;
            this.c1Label1.Location = new System.Drawing.Point(12, 20);
            this.c1Label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.c1Label1.Name = "c1Label1";
            this.c1Label1.Size = new System.Drawing.Size(84, 14);
            this.c1Label1.TabIndex = 0;
            this.c1Label1.Tag = null;
            this.c1Label1.Text = "Buisness Date";
            this.c1Label1.TextDetached = true;
            // 
            // c1Label2
            // 
            this.c1Label2.AutoSize = true;
            this.c1Label2.Location = new System.Drawing.Point(12, 46);
            this.c1Label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.c1Label2.Name = "c1Label2";
            this.c1Label2.Size = new System.Drawing.Size(70, 14);
            this.c1Label2.TabIndex = 1;
            this.c1Label2.Tag = null;
            this.c1Label2.Text = "Book Group";
            this.c1Label2.TextDetached = true;
            // 
            // BookGroupTextBox
            // 
            this.BookGroupTextBox.Location = new System.Drawing.Point(104, 43);
            this.BookGroupTextBox.Name = "BookGroupTextBox";
            this.BookGroupTextBox.Size = new System.Drawing.Size(100, 20);
            this.BookGroupTextBox.TabIndex = 3;
            this.BookGroupTextBox.Tag = null;
            this.BookGroupTextBox.Text = "PFSI";
            this.BookGroupTextBox.TextDetached = true;
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusTextBox.Font = new System.Drawing.Font("Lucida Sans", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusTextBox.Location = new System.Drawing.Point(0, 100);
            this.StatusTextBox.Multiline = true;
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.Size = new System.Drawing.Size(731, 314);
            this.StatusTextBox.TabIndex = 4;
            this.StatusTextBox.Tag = null;
            // 
            // BizDateEdit
            // 
            // 
            // 
            // 
            this.BizDateEdit.Calendar.DayNameLength = 1;
            this.BizDateEdit.CustomFormat = "yyyy-MM-dd";
            this.BizDateEdit.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.BizDateEdit.Location = new System.Drawing.Point(104, 17);
            this.BizDateEdit.Name = "BizDateEdit";
            this.BizDateEdit.Size = new System.Drawing.Size(100, 20);
            this.BizDateEdit.TabIndex = 5;
            this.BizDateEdit.Tag = null;
            this.BizDateEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // QueryButton
            // 
            this.QueryButton.Location = new System.Drawing.Point(210, 38);
            this.QueryButton.Name = "QueryButton";
            this.QueryButton.Size = new System.Drawing.Size(57, 31);
            this.QueryButton.TabIndex = 6;
            this.QueryButton.Text = "Query";
            this.QueryButton.UseVisualStyleBackColor = true;
            this.QueryButton.Click += new System.EventHandler(this.QueryButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 414);
            this.Controls.Add(this.QueryButton);
            this.Controls.Add(this.BizDateEdit);
            this.Controls.Add(this.StatusTextBox);
            this.Controls.Add(this.BookGroupTextBox);
            this.Controls.Add(this.c1Label2);
            this.Controls.Add(this.c1Label1);
            this.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(0, 100, 0, 0);
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.c1Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookGroupTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatusTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BizDateEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Input.C1Label c1Label1;
        private C1.Win.C1Input.C1Label c1Label2;
        private C1.Win.C1Input.C1TextBox BookGroupTextBox;
        private C1.Win.C1Input.C1TextBox StatusTextBox;
        private C1.Win.C1Input.C1DateEdit BizDateEdit;
        private C1.Win.C1Input.C1Button QueryButton;
    }
}

