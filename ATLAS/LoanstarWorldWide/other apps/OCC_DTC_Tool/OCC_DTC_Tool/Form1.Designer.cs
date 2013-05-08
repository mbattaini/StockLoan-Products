namespace OCC_DTC_Tool
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
            this.OccGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.ListTextBox = new C1.Win.C1Input.C1TextBox();
            this.RadioPledge = new System.Windows.Forms.RadioButton();
            this.RadioRelease = new System.Windows.Forms.RadioButton();
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.ButtonParseList = new C1.Win.C1Ribbon.RibbonButton();
            this.ButtonSendOcc = new C1.Win.C1Ribbon.RibbonButton();
            ((System.ComponentModel.ISupportInitialize)(this.OccGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // OccGrid
            // 
            this.OccGrid.CaptionHeight = 17;
            this.OccGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OccGrid.Location = new System.Drawing.Point(0, 162);
            this.OccGrid.Name = "OccGrid";
            this.OccGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.OccGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.OccGrid.PreviewInfo.ZoomFactor = 75D;
            this.OccGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("OccGrid.PrintInfo.PageSettings")));
            this.OccGrid.PropBag = resources.GetString("OccGrid.PropBag");
            this.OccGrid.RowHeight = 15;
            this.OccGrid.Size = new System.Drawing.Size(498, 273);
            this.OccGrid.TabIndex = 0;
            this.OccGrid.Text = "c1TrueDBGrid1";
            this.OccGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
            // 
            // ListTextBox
            // 
            this.ListTextBox.Location = new System.Drawing.Point(3, 6);
            this.ListTextBox.Multiline = true;
            this.ListTextBox.Name = "ListTextBox";
            this.ListTextBox.Size = new System.Drawing.Size(300, 149);
            this.ListTextBox.TabIndex = 1;
            this.ListTextBox.Tag = null;
            // 
            // RadioPledge
            // 
            this.RadioPledge.AutoSize = true;
            this.RadioPledge.Location = new System.Drawing.Point(309, 8);
            this.RadioPledge.Name = "RadioPledge";
            this.RadioPledge.Size = new System.Drawing.Size(60, 18);
            this.RadioPledge.TabIndex = 2;
            this.RadioPledge.TabStop = true;
            this.RadioPledge.Text = "Pledge";
            this.RadioPledge.UseVisualStyleBackColor = true;
            // 
            // RadioRelease
            // 
            this.RadioRelease.AutoSize = true;
            this.RadioRelease.Location = new System.Drawing.Point(309, 33);
            this.RadioRelease.Name = "RadioRelease";
            this.RadioRelease.Size = new System.Drawing.Size(64, 18);
            this.RadioRelease.TabIndex = 3;
            this.RadioRelease.TabStop = true;
            this.RadioRelease.Text = "Release";
            this.RadioRelease.UseVisualStyleBackColor = true;
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.LeftPaneItems.Add(this.ButtonParseList);
            this.c1StatusBar1.LeftPaneItems.Add(this.ButtonSendOcc);
            this.c1StatusBar1.Name = "c1StatusBar1";
            // 
            // ButtonParseList
            // 
            this.ButtonParseList.Name = "ButtonParseList";
            this.ButtonParseList.SmallImage = ((System.Drawing.Image)(resources.GetObject("ButtonParseList.SmallImage")));
            this.ButtonParseList.Text = "Parse List";
            // 
            // ButtonSendOcc
            // 
            this.ButtonSendOcc.Name = "ButtonSendOcc";
            this.ButtonSendOcc.SmallImage = ((System.Drawing.Image)(resources.GetObject("ButtonSendOcc.SmallImage")));
            this.ButtonSendOcc.Text = "Send OCC / DTCC";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 457);
            this.Controls.Add(this.RadioRelease);
            this.Controls.Add(this.RadioPledge);
            this.Controls.Add(this.ListTextBox);
            this.Controls.Add(this.OccGrid);
            this.Controls.Add(this.c1StatusBar1);
            this.Font = new System.Drawing.Font("Candara", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(0, 162, 0, 0);
            this.Text = "OCC / DTCC Tool";
            ((System.ComponentModel.ISupportInitialize)(this.OccGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid OccGrid;
        private C1.Win.C1Input.C1TextBox ListTextBox;
        private System.Windows.Forms.RadioButton RadioPledge;
        private System.Windows.Forms.RadioButton RadioRelease;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Ribbon.RibbonButton ButtonParseList;
        private C1.Win.C1Ribbon.RibbonButton ButtonSendOcc;
    }
}

