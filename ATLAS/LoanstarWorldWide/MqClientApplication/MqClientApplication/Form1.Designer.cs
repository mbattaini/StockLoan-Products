namespace MqClientApplication
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
            this.GetMsgButton = new System.Windows.Forms.Button();
            this.DataGridMsg = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridMsg)).BeginInit();
            this.SuspendLayout();
            // 
            // GetMsgButton
            // 
            this.GetMsgButton.Location = new System.Drawing.Point(6, 8);
            this.GetMsgButton.Name = "GetMsgButton";
            this.GetMsgButton.Size = new System.Drawing.Size(60, 19);
            this.GetMsgButton.TabIndex = 1;
            this.GetMsgButton.Text = "Get";
            this.GetMsgButton.UseVisualStyleBackColor = true;
            this.GetMsgButton.Click += new System.EventHandler(this.GetMsgButton_Click);
            // 
            // DataGridMsg
            // 
            this.DataGridMsg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridMsg.Location = new System.Drawing.Point(0, 50);
            this.DataGridMsg.Name = "DataGridMsg";
            this.DataGridMsg.Size = new System.Drawing.Size(749, 212);
            this.DataGridMsg.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 262);
            this.Controls.Add(this.DataGridMsg);
            this.Controls.Add(this.GetMsgButton);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(0, 50, 0, 0);
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridMsg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GetMsgButton;
        private System.Windows.Forms.DataGridView DataGridMsg;
    }
}

