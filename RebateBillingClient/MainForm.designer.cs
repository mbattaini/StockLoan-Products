namespace Golden
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ToolBar = new System.Windows.Forms.ToolStrip();
            this.ShortSaleBillingButton = new System.Windows.Forms.ToolStripButton();
            this.LookupButton = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolBar
            // 
            this.ToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShortSaleBillingButton,
            this.LookupButton});
            this.ToolBar.Location = new System.Drawing.Point(0, 0);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.Size = new System.Drawing.Size(1330, 25);
            this.ToolBar.TabIndex = 2;
            this.ToolBar.Text = "toolStrip1";
            // 
            // ShortSaleBillingButton
            // 
            this.ShortSaleBillingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ShortSaleBillingButton.Image = ((System.Drawing.Image)(resources.GetObject("ShortSaleBillingButton.Image")));
            this.ShortSaleBillingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShortSaleBillingButton.Name = "ShortSaleBillingButton";
            this.ShortSaleBillingButton.Size = new System.Drawing.Size(23, 22);
            this.ShortSaleBillingButton.Text = "Short Sale Billing";
            this.ShortSaleBillingButton.Click += new System.EventHandler(this.ShortSaleBillingButton_Click);
            // 
            // LookupButton
            // 
            this.LookupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LookupButton.Image = ((System.Drawing.Image)(resources.GetObject("LookupButton.Image")));
            this.LookupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LookupButton.Name = "LookupButton";
            this.LookupButton.Size = new System.Drawing.Size(23, 22);
            this.LookupButton.Text = "History Lookup";
            this.LookupButton.Click += new System.EventHandler(this.LookupButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1330, 747);
            this.Controls.Add(this.ToolBar);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "Rebate Billing";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ToolBar.ResumeLayout(false);
            this.ToolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ToolBar;
        private System.Windows.Forms.ToolStripButton ShortSaleBillingButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripButton LookupButton;


    }
}