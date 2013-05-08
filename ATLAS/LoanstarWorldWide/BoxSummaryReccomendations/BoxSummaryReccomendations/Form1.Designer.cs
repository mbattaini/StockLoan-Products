namespace BoxSummaryReccomendations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.RecallGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.BizDateEdit = new C1.Win.C1Input.C1DateEdit();
            this.LookupButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RecallGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BizDateEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // RecallGrid
            // 
            this.RecallGrid.ExtendRightColumn = true;
            this.RecallGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("RecallGrid.Images"))));
            this.RecallGrid.Location = new System.Drawing.Point(6, 50);
            this.RecallGrid.Name = "RecallGrid";
            this.RecallGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.RecallGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.RecallGrid.PreviewInfo.ZoomFactor = 75D;
            this.RecallGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("c1TrueDBGrid1.PrintInfo.PageSettings")));
            this.RecallGrid.PropBag = resources.GetString("RecallGrid.PropBag");
            this.RecallGrid.Size = new System.Drawing.Size(546, 456);
            this.RecallGrid.TabIndex = 0;
            this.RecallGrid.Text = "c1TrueDBGrid1";
            // 
            // BizDateEdit
            // 
            // 
            // 
            // 
            this.BizDateEdit.Calendar.DayNameLength = 1;
            this.BizDateEdit.CustomFormat = "yyyy-MM-dd";
            this.BizDateEdit.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.BizDateEdit.Location = new System.Drawing.Point(6, 12);
            this.BizDateEdit.Name = "BizDateEdit";
            this.BizDateEdit.Size = new System.Drawing.Size(160, 20);
            this.BizDateEdit.TabIndex = 1;
            this.BizDateEdit.Tag = null;
            // 
            // LookupButton
            // 
            this.LookupButton.Location = new System.Drawing.Point(172, 5);
            this.LookupButton.Name = "LookupButton";
            this.LookupButton.Size = new System.Drawing.Size(65, 32);
            this.LookupButton.TabIndex = 2;
            this.LookupButton.Text = "Lookup";
            this.LookupButton.UseVisualStyleBackColor = true;
            this.LookupButton.Click += new System.EventHandler(this.LookupButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(243, 5);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(65, 32);
            this.ExportButton.TabIndex = 3;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 518);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.LookupButton);
            this.Controls.Add(this.BizDateEdit);
            this.Controls.Add(this.RecallGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.RecallGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BizDateEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid RecallGrid;
        private C1.Win.C1Input.C1DateEdit BizDateEdit;
        private System.Windows.Forms.Button LookupButton;
        private System.Windows.Forms.Button ExportButton;
    }
}

