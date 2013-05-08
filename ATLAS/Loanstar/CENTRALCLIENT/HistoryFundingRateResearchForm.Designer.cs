namespace CentralClient
{
    partial class HistoryFundingRateResearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryFundingRateResearchForm));
            this.BackPanel = new System.Windows.Forms.Panel();
            this.StopDateLabel = new C1.Win.C1Input.C1Label();
            this.StartDateLabel = new C1.Win.C1Input.C1Label();
            this.StopDateEdit = new C1.Win.C1Input.C1DateEdit();
            this.StartDateEdit = new C1.Win.C1Input.C1DateEdit();
            this.FundsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.FundingRateResearchGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.FundChart = new C1.Win.C1Chart.C1Chart();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.BackPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StopDateLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StopDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FundsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FundingRateResearchGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FundChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // BackPanel
            // 
            this.BackPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.BackPanel.Controls.Add(this.StopDateLabel);
            this.BackPanel.Controls.Add(this.StartDateLabel);
            this.BackPanel.Controls.Add(this.StopDateEdit);
            this.BackPanel.Controls.Add(this.StartDateEdit);
            this.BackPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.BackPanel.Location = new System.Drawing.Point(0, 0);
            this.BackPanel.Name = "BackPanel";
            this.BackPanel.Size = new System.Drawing.Size(1020, 27);
            this.BackPanel.TabIndex = 15;
            // 
            // StopDateLabel
            // 
            this.StopDateLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.StopDateLabel.ForeColor = System.Drawing.Color.Black;
            this.StopDateLabel.Location = new System.Drawing.Point(341, 7);
            this.StopDateLabel.Name = "StopDateLabel";
            this.StopDateLabel.Size = new System.Drawing.Size(69, 13);
            this.StopDateLabel.TabIndex = 3;
            this.StopDateLabel.Tag = null;
            this.StopDateLabel.Text = "Stop Date:";
            this.StopDateLabel.TextDetached = true;
            this.StopDateLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            // 
            // StartDateLabel
            // 
            this.StartDateLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.StartDateLabel.ForeColor = System.Drawing.Color.Black;
            this.StartDateLabel.Location = new System.Drawing.Point(10, 7);
            this.StartDateLabel.Name = "StartDateLabel";
            this.StartDateLabel.Size = new System.Drawing.Size(71, 13);
            this.StartDateLabel.TabIndex = 2;
            this.StartDateLabel.Tag = null;
            this.StartDateLabel.Text = "Start Date:";
            this.StartDateLabel.TextDetached = true;
            this.StartDateLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
            // 
            // StopDateEdit
            // 
            this.StopDateEdit.AutoSize = false;
            this.StopDateEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.StopDateEdit.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.StopDateEdit.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.StopDateEdit.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.StopDateEdit.CustomFormat = "yyyy-MM-dd";
            this.StopDateEdit.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.StopDateEdit.Location = new System.Drawing.Point(426, 4);
            this.StopDateEdit.Name = "StopDateEdit";
            this.StopDateEdit.Size = new System.Drawing.Size(200, 19);
            this.StopDateEdit.TabIndex = 1;
            this.StopDateEdit.Tag = null;
            this.StopDateEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.StopDateEdit.ValueChanged += new System.EventHandler(this.StartDateEdit_ValueChanged);
            // 
            // StartDateEdit
            // 
            this.StartDateEdit.AutoSize = false;
            this.StartDateEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // 
            // 
            this.StartDateEdit.Calendar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.StartDateEdit.Calendar.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.StartDateEdit.Calendar.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.StartDateEdit.CustomFormat = "yyyy-MM-dd";
            this.StartDateEdit.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
            this.StartDateEdit.Location = new System.Drawing.Point(99, 4);
            this.StartDateEdit.Name = "StartDateEdit";
            this.StartDateEdit.Size = new System.Drawing.Size(200, 19);
            this.StartDateEdit.TabIndex = 0;
            this.StartDateEdit.Tag = null;
            this.StartDateEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            this.StartDateEdit.ValueChanged += new System.EventHandler(this.StartDateEdit_ValueChanged);
            // 
            // FundsGrid
            // 
            this.FundsGrid.AllowAddNew = true;
            this.FundsGrid.AlternatingRows = true;
            this.FundsGrid.CaptionHeight = 17;
            this.FundsGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.FundsGrid.Dock = System.Windows.Forms.DockStyle.Left;
            this.FundsGrid.EmptyRows = true;
            this.FundsGrid.ExtendRightColumn = true;
            this.FundsGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.FundsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("FundsGrid.Images"))));
            this.FundsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("FundsGrid.Images1"))));
            this.FundsGrid.Location = new System.Drawing.Point(0, 27);
            this.FundsGrid.Name = "FundsGrid";
            this.FundsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.FundsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.FundsGrid.PreviewInfo.ZoomFactor = 75D;
            this.FundsGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("FundsGrid.PrintInfo.PageSettings")));
            this.FundsGrid.RecordSelectors = false;
            this.FundsGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.FundsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.FundsGrid.RowHeight = 15;
            this.FundsGrid.Size = new System.Drawing.Size(156, 447);
            this.FundsGrid.TabIndex = 17;
            this.FundsGrid.Text = "Funding Rates";
            this.FundsGrid.UseColumnStyles = false;
            this.FundsGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
            this.FundsGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.FundsGrid_Paint);
            this.FundsGrid.PropBag = resources.GetString("FundsGrid.PropBag");
            // 
            // FundingRateResearchGrid
            // 
            this.FundingRateResearchGrid.AllowAddNew = true;
            this.FundingRateResearchGrid.AlternatingRows = true;
            this.FundingRateResearchGrid.CaptionHeight = 17;
            this.FundingRateResearchGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
            this.FundingRateResearchGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.FundingRateResearchGrid.EmptyRows = true;
            this.FundingRateResearchGrid.ExtendRightColumn = true;
            this.FundingRateResearchGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.FundingRateResearchGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("FundingRateResearchGrid.Images"))));
            this.FundingRateResearchGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("FundingRateResearchGrid.Images1"))));
            this.FundingRateResearchGrid.Location = new System.Drawing.Point(159, 27);
            this.FundingRateResearchGrid.Name = "FundingRateResearchGrid";
            this.FundingRateResearchGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.FundingRateResearchGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.FundingRateResearchGrid.PreviewInfo.ZoomFactor = 75D;
            this.FundingRateResearchGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("FundingRateResearchGrid.PrintInfo.PageSettings")));
            this.FundingRateResearchGrid.RecordSelectors = false;
            this.FundingRateResearchGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.FundingRateResearchGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.FundingRateResearchGrid.RowHeight = 15;
            this.FundingRateResearchGrid.Size = new System.Drawing.Size(861, 149);
            this.FundingRateResearchGrid.TabIndex = 18;
            this.FundingRateResearchGrid.Text = "Funding Rates";
            this.FundingRateResearchGrid.UseColumnStyles = false;
            this.FundingRateResearchGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
            this.FundingRateResearchGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.FundingRateResearchGrid_BeforeUpdate);
            this.FundingRateResearchGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FundingRateResearchGrid_FormatText);
            this.FundingRateResearchGrid.PropBag = resources.GetString("FundingRateResearchGrid.PropBag");
            // 
            // FundChart
            // 
            this.FundChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.FundChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FundChart.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.FundChart.Location = new System.Drawing.Point(159, 179);
            this.FundChart.Name = "FundChart";
            this.FundChart.PropBag = resources.GetString("FundChart.PropBag");
            this.FundChart.Size = new System.Drawing.Size(861, 295);
            this.FundChart.TabIndex = 19;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.splitter1.Location = new System.Drawing.Point(156, 27);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 447);
            this.splitter1.TabIndex = 20;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(159, 176);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(861, 3);
            this.splitter2.TabIndex = 21;
            this.splitter2.TabStop = false;
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.Name = "c1StatusBar1";
            // 
            // HistoryFundingRateResearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 497);
            this.Controls.Add(this.FundChart);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.FundingRateResearchGrid);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.FundsGrid);
            this.Controls.Add(this.BackPanel);
            this.Controls.Add(this.c1StatusBar1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HistoryFundingRateResearchForm";
            this.Text = "History - Funding Rate Research";
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Windows7;
            this.Load += new System.EventHandler(this.HistoryFundingRateResearchForm_Load);
            this.BackPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StopDateLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StopDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FundsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FundingRateResearchGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FundChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel BackPanel;
        private C1.Win.C1Input.C1Label StopDateLabel;
        private C1.Win.C1Input.C1Label StartDateLabel;
        private C1.Win.C1Input.C1DateEdit StopDateEdit;
        private C1.Win.C1Input.C1DateEdit StartDateEdit;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid FundsGrid;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid FundingRateResearchGrid;
        private C1.Win.C1Chart.C1Chart FundChart;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
    }
}