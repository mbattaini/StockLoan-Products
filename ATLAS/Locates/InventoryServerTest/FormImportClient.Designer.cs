namespace StockLoan.Inventory
{
    partial class FormImportClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImportClient));
            this.btnBeginImport = new System.Windows.Forms.Button();
            this.tabdockImportPages = new C1.Win.C1Command.C1DockingTab();
            this.tabpageFiles = new C1.Win.C1Command.C1DockingTabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPatternPreview = new System.Windows.Forms.TextBox();
            this.browserPreview = new System.Windows.Forms.WebBrowser();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabpagePatterns = new C1.Win.C1Command.C1DockingTabPage();
            this.gridviewPatterns = new System.Windows.Forms.DataGridView();
            this.tabpageSubscriptions = new C1.Win.C1Command.C1DockingTabPage();
            this.gridviewSubscriptions = new System.Windows.Forms.DataGridView();
            this.tabpageExecutions = new C1.Win.C1Command.C1DockingTabPage();
            this.flexgridExecutions = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabpageImport = new C1.Win.C1Command.C1DockingTabPage();
            this.flexgridImport = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.statusbarCurrentState = new C1.Win.C1Ribbon.C1StatusBar();
            this.statuslabelCurrentState = new C1.Win.C1Ribbon.RibbonLabel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancelImport = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.cbxImportMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabpageLog = new C1.Win.C1Command.C1DockingTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.tabdockImportPages)).BeginInit();
            this.tabdockImportPages.SuspendLayout();
            this.tabpageFiles.SuspendLayout();
            this.tabpagePatterns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewPatterns)).BeginInit();
            this.tabpageSubscriptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewSubscriptions)).BeginInit();
            this.tabpageExecutions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flexgridExecutions)).BeginInit();
            this.tabpageImport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flexgridImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusbarCurrentState)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBeginImport
            // 
            this.btnBeginImport.Enabled = false;
            this.btnBeginImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBeginImport.Location = new System.Drawing.Point(202, 12);
            this.btnBeginImport.Name = "btnBeginImport";
            this.btnBeginImport.Size = new System.Drawing.Size(161, 65);
            this.btnBeginImport.TabIndex = 1;
            this.btnBeginImport.Text = "&Begin Import";
            this.btnBeginImport.UseVisualStyleBackColor = true;
            this.btnBeginImport.Click += new System.EventHandler(this.btnGetImportFiles_Click);
            // 
            // tabdockImportPages
            // 
            this.tabdockImportPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabdockImportPages.Controls.Add(this.tabpageFiles);
            this.tabdockImportPages.Controls.Add(this.tabpageLog);
            this.tabdockImportPages.Controls.Add(this.tabpagePatterns);
            this.tabdockImportPages.Controls.Add(this.tabpageSubscriptions);
            this.tabdockImportPages.Controls.Add(this.tabpageExecutions);
            this.tabdockImportPages.Controls.Add(this.tabpageImport);
            this.tabdockImportPages.Location = new System.Drawing.Point(13, 101);
            this.tabdockImportPages.Name = "tabdockImportPages";
            this.tabdockImportPages.SelectedIndex = 5;
            this.tabdockImportPages.Size = new System.Drawing.Size(886, 638);
            this.tabdockImportPages.TabAreaBorder = true;
            this.tabdockImportPages.TabIndex = 3;
            this.tabdockImportPages.TabsSpacing = 5;
            this.tabdockImportPages.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007;
            this.tabdockImportPages.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Blue;
            this.tabdockImportPages.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Blue;
            this.tabdockImportPages.Enter += new System.EventHandler(this.tabdockImportPages_Enter);
            this.tabdockImportPages.Leave += new System.EventHandler(this.tabdockImportPages_Leave);
            // 
            // tabpageFiles
            // 
            this.tabpageFiles.Controls.Add(this.label2);
            this.tabpageFiles.Controls.Add(this.tbPatternPreview);
            this.tabpageFiles.Controls.Add(this.browserPreview);
            this.tabpageFiles.Controls.Add(this.btnBrowse);
            this.tabpageFiles.Controls.Add(this.panel1);
            this.tabpageFiles.Location = new System.Drawing.Point(1, 24);
            this.tabpageFiles.Name = "tabpageFiles";
            this.tabpageFiles.Size = new System.Drawing.Size(884, 613);
            this.tabpageFiles.TabIndex = 4;
            this.tabpageFiles.Text = "File Matching";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(170, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Regex Pattern To Match:";
            // 
            // tbPatternPreview
            // 
            this.tbPatternPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPatternPreview.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPatternPreview.Location = new System.Drawing.Point(173, 37);
            this.tbPatternPreview.Name = "tbPatternPreview";
            this.tbPatternPreview.Size = new System.Drawing.Size(677, 26);
            this.tbPatternPreview.TabIndex = 1;
            this.tbPatternPreview.Leave += new System.EventHandler(this.tbPatternPreview_Leave);
            this.tbPatternPreview.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPatternPreview_KeyPress);
            // 
            // browserPreview
            // 
            this.browserPreview.AllowNavigation = false;
            this.browserPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.browserPreview.Location = new System.Drawing.Point(188, 79);
            this.browserPreview.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserPreview.Name = "browserPreview";
            this.browserPreview.Size = new System.Drawing.Size(645, 504);
            this.browserPreview.TabIndex = 2;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(23, 21);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(134, 42);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(173, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(677, 527);
            this.panel1.TabIndex = 2;
            // 
            // tabpagePatterns
            // 
            this.tabpagePatterns.Controls.Add(this.gridviewPatterns);
            this.tabpagePatterns.Location = new System.Drawing.Point(1, 24);
            this.tabpagePatterns.Name = "tabpagePatterns";
            this.tabpagePatterns.Size = new System.Drawing.Size(884, 613);
            this.tabpagePatterns.TabIndex = 2;
            this.tabpagePatterns.Text = "Patterns";
            this.tabpagePatterns.Leave += new System.EventHandler(this.tabpagePatterns_Leave);
            this.tabpagePatterns.Enter += new System.EventHandler(this.tabpagePatterns_Enter);
            // 
            // gridviewPatterns
            // 
            this.gridviewPatterns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridviewPatterns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridviewPatterns.Location = new System.Drawing.Point(3, 23);
            this.gridviewPatterns.Name = "gridviewPatterns";
            this.gridviewPatterns.Size = new System.Drawing.Size(878, 587);
            this.gridviewPatterns.TabIndex = 0;
            this.gridviewPatterns.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridviewPatterns_CellEndEdit);
            // 
            // tabpageSubscriptions
            // 
            this.tabpageSubscriptions.Controls.Add(this.gridviewSubscriptions);
            this.tabpageSubscriptions.Location = new System.Drawing.Point(1, 24);
            this.tabpageSubscriptions.Name = "tabpageSubscriptions";
            this.tabpageSubscriptions.Size = new System.Drawing.Size(884, 613);
            this.tabpageSubscriptions.TabIndex = 3;
            this.tabpageSubscriptions.Text = "Subscriptions";
            this.tabpageSubscriptions.Leave += new System.EventHandler(this.tabpageSubscriptions_Leave);
            this.tabpageSubscriptions.Enter += new System.EventHandler(this.tabpageSubscriptions_Enter);
            // 
            // gridviewSubscriptions
            // 
            this.gridviewSubscriptions.AllowUserToDeleteRows = false;
            this.gridviewSubscriptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridviewSubscriptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridviewSubscriptions.Location = new System.Drawing.Point(4, 3);
            this.gridviewSubscriptions.Name = "gridviewSubscriptions";
            this.gridviewSubscriptions.Size = new System.Drawing.Size(877, 607);
            this.gridviewSubscriptions.TabIndex = 0;
            // 
            // tabpageExecutions
            // 
            this.tabpageExecutions.Controls.Add(this.flexgridExecutions);
            this.tabpageExecutions.Location = new System.Drawing.Point(1, 24);
            this.tabpageExecutions.Name = "tabpageExecutions";
            this.tabpageExecutions.Size = new System.Drawing.Size(884, 613);
            this.tabpageExecutions.TabIndex = 1;
            this.tabpageExecutions.Text = "Executions";
            // 
            // flexgridExecutions
            // 
            this.flexgridExecutions.AllowEditing = false;
            this.flexgridExecutions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flexgridExecutions.ColumnInfo = "10,1,0,0,0,85,Columns:";
            this.flexgridExecutions.Location = new System.Drawing.Point(3, 3);
            this.flexgridExecutions.Name = "flexgridExecutions";
            this.flexgridExecutions.Rows.DefaultSize = 17;
            this.flexgridExecutions.Size = new System.Drawing.Size(878, 607);
            this.flexgridExecutions.TabIndex = 0;
            // 
            // tabpageImport
            // 
            this.tabpageImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabpageImport.Controls.Add(this.flexgridImport);
            this.tabpageImport.Location = new System.Drawing.Point(1, 24);
            this.tabpageImport.Name = "tabpageImport";
            this.tabpageImport.Size = new System.Drawing.Size(884, 613);
            this.tabpageImport.TabIndex = 0;
            this.tabpageImport.Text = "Import";
            // 
            // flexgridImport
            // 
            this.flexgridImport.AllowEditing = false;
            this.flexgridImport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flexgridImport.ColumnInfo = resources.GetString("flexgridImport.ColumnInfo");
            this.flexgridImport.Location = new System.Drawing.Point(3, 3);
            this.flexgridImport.Name = "flexgridImport";
            this.flexgridImport.Rows.Count = 1;
            this.flexgridImport.Rows.DefaultSize = 17;
            this.flexgridImport.Size = new System.Drawing.Size(878, 577);
            this.flexgridImport.TabIndex = 3;
            // 
            // statusbarCurrentState
            // 
            this.statusbarCurrentState.LeftPaneItems.Add(this.statuslabelCurrentState);
            this.statusbarCurrentState.Location = new System.Drawing.Point(0, 827);
            this.statusbarCurrentState.Name = "statusbarCurrentState";
            this.statusbarCurrentState.Size = new System.Drawing.Size(911, 22);
            this.statusbarCurrentState.TabIndex = 7;
            // 
            // statuslabelCurrentState
            // 
            this.statuslabelCurrentState.ID = "statuslabelCurrentState";
            this.statuslabelCurrentState.Text = "\"\"";
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(738, 766);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(161, 55);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "&Apply Changes";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Visible = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancelImport
            // 
            this.btnCancelImport.Enabled = false;
            this.btnCancelImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelImport.Location = new System.Drawing.Point(703, 12);
            this.btnCancelImport.Name = "btnCancelImport";
            this.btnCancelImport.Size = new System.Drawing.Size(161, 65);
            this.btnCancelImport.TabIndex = 9;
            this.btnCancelImport.Text = "&Cancel Import";
            this.btnCancelImport.UseVisualStyleBackColor = true;
            this.btnCancelImport.Click += new System.EventHandler(this.btnCancelImport_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Location = new System.Drawing.Point(369, 12);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(161, 65);
            this.btnPause.TabIndex = 10;
            this.btnPause.Text = "&Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnResume
            // 
            this.btnResume.Enabled = false;
            this.btnResume.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResume.Location = new System.Drawing.Point(536, 12);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(161, 65);
            this.btnResume.TabIndex = 11;
            this.btnResume.Text = "&Resume";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // cbxImportMode
            // 
            this.cbxImportMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxImportMode.FormattingEnabled = true;
            this.cbxImportMode.Location = new System.Drawing.Point(13, 24);
            this.cbxImportMode.Name = "cbxImportMode";
            this.cbxImportMode.Size = new System.Drawing.Size(134, 28);
            this.cbxImportMode.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Import Mode:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.cbxImportMode);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(17, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(161, 65);
            this.panel2.TabIndex = 14;
            // 
            // tabpageLog
            // 
            this.tabpageLog.Location = new System.Drawing.Point(1, 24);
            this.tabpageLog.Name = "tabpageLog";
            this.tabpageLog.Size = new System.Drawing.Size(884, 613);
            this.tabpageLog.TabIndex = 5;
            this.tabpageLog.Text = "Log";
            // 
            // FormImportClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 849);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnCancelImport);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.statusbarCurrentState);
            this.Controls.Add(this.tabdockImportPages);
            this.Controls.Add(this.btnBeginImport);
            this.Controls.Add(this.panel2);
            this.Name = "FormImportClient";
            this.Text = "Test Import Client";
            this.Load += new System.EventHandler(this.FormImportClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabdockImportPages)).EndInit();
            this.tabdockImportPages.ResumeLayout(false);
            this.tabpageFiles.ResumeLayout(false);
            this.tabpageFiles.PerformLayout();
            this.tabpagePatterns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridviewPatterns)).EndInit();
            this.tabpageSubscriptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridviewSubscriptions)).EndInit();
            this.tabpageExecutions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flexgridExecutions)).EndInit();
            this.tabpageImport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flexgridImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusbarCurrentState)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBeginImport;
        private C1.Win.C1Command.C1DockingTab tabdockImportPages;
        private C1.Win.C1Command.C1DockingTabPage tabpageImport;
        private C1.Win.C1FlexGrid.C1FlexGrid flexgridImport;
        private C1.Win.C1Command.C1DockingTabPage tabpageExecutions;
        private C1.Win.C1FlexGrid.C1FlexGrid flexgridExecutions;
        private C1.Win.C1Command.C1DockingTabPage tabpagePatterns;
        private C1.Win.C1Ribbon.C1StatusBar statusbarCurrentState;
        private C1.Win.C1Ribbon.RibbonLabel statuslabelCurrentState;
        private C1.Win.C1Command.C1DockingTabPage tabpageSubscriptions;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancelImport;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.ComboBox cbxImportMode;
        private System.Windows.Forms.Label label1;
        private C1.Win.C1Command.C1DockingTabPage tabpageFiles;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.WebBrowser browserPreview;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPatternPreview;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridviewPatterns;
        private System.Windows.Forms.DataGridView gridviewSubscriptions;
        private System.Windows.Forms.Panel panel2;
        private C1.Win.C1Command.C1DockingTabPage tabpageLog;
    }
}