namespace CentralClient
{
    partial class InventorySubscriptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventorySubscriptionsForm));
            this.DockingTab = new C1.Win.C1Command.C1DockingTab();
            this.InventorySubscriptionDockingTabPage = new C1.Win.C1Command.C1DockingTabPage();
            this.SubscriptionGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.InventoryFeedUploadDockingTabPage = new C1.Win.C1Command.C1DockingTabPage();
            this.FeedUploadGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.OkRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            this.CancelRibbonButton = new C1.Win.C1Ribbon.RibbonButton();
            ((System.ComponentModel.ISupportInitialize)(this.DockingTab)).BeginInit();
            this.DockingTab.SuspendLayout();
            this.InventorySubscriptionDockingTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SubscriptionGrid)).BeginInit();
            this.InventoryFeedUploadDockingTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FeedUploadGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // DockingTab
            // 
            this.DockingTab.Controls.Add(this.InventorySubscriptionDockingTabPage);
            this.DockingTab.Controls.Add(this.InventoryFeedUploadDockingTabPage);
            this.DockingTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockingTab.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DockingTab.Location = new System.Drawing.Point(0, 0);
            this.DockingTab.Name = "DockingTab";
            this.DockingTab.Size = new System.Drawing.Size(1033, 283);
            this.DockingTab.TabAreaBorder = true;
            this.DockingTab.TabIndex = 7;
            this.DockingTab.TabsSpacing = 5;
            this.DockingTab.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007;
            this.DockingTab.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
            this.DockingTab.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Silver;
            // 
            // InventorySubscriptionDockingTabPage
            // 
            this.InventorySubscriptionDockingTabPage.Controls.Add(this.SubscriptionGrid);
            this.InventorySubscriptionDockingTabPage.Enabled = false;
            this.InventorySubscriptionDockingTabPage.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InventorySubscriptionDockingTabPage.Location = new System.Drawing.Point(1, 25);
            this.InventorySubscriptionDockingTabPage.Name = "InventorySubscriptionDockingTabPage";
            this.InventorySubscriptionDockingTabPage.Size = new System.Drawing.Size(1031, 257);
            this.InventorySubscriptionDockingTabPage.TabIndex = 1;
            this.InventorySubscriptionDockingTabPage.Text = "Subscriptions";
            // 
            // SubscriptionGrid
            // 
            this.SubscriptionGrid.AlternatingRows = true;
            this.SubscriptionGrid.CaptionHeight = 17;
            this.SubscriptionGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubscriptionGrid.EmptyRows = true;
            this.SubscriptionGrid.ExtendRightColumn = true;
            this.SubscriptionGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.SubscriptionGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("SubscriptionGrid.Images"))));
            this.SubscriptionGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("SubscriptionGrid.Images1"))));
            this.SubscriptionGrid.Location = new System.Drawing.Point(0, 0);
            this.SubscriptionGrid.Name = "SubscriptionGrid";
            this.SubscriptionGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.SubscriptionGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.SubscriptionGrid.PreviewInfo.ZoomFactor = 75;
            this.SubscriptionGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("SubscriptionGrid.PrintInfo.PageSettings")));
            this.SubscriptionGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.SubscriptionGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.SubscriptionGrid.RowHeight = 15;
            this.SubscriptionGrid.Size = new System.Drawing.Size(1031, 257);
            this.SubscriptionGrid.TabIndex = 2;
            this.SubscriptionGrid.Text = "SubscriptionGrid";
            this.SubscriptionGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
            this.SubscriptionGrid.PropBag = resources.GetString("SubscriptionGrid.PropBag");
            // 
            // InventoryFeedUploadDockingTabPage
            // 
            this.InventoryFeedUploadDockingTabPage.Controls.Add(this.FeedUploadGrid);
            this.InventoryFeedUploadDockingTabPage.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InventoryFeedUploadDockingTabPage.Location = new System.Drawing.Point(1, 25);
            this.InventoryFeedUploadDockingTabPage.Name = "InventoryFeedUploadDockingTabPage";
            this.InventoryFeedUploadDockingTabPage.Size = new System.Drawing.Size(1031, 257);
            this.InventoryFeedUploadDockingTabPage.TabIndex = 2;
            this.InventoryFeedUploadDockingTabPage.Text = "Feed Upload";
            // 
            // FeedUploadGrid
            // 
            this.FeedUploadGrid.BackColor = System.Drawing.Color.Silver;
            this.FeedUploadGrid.CaptionHeight = 17;
            this.FeedUploadGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FeedUploadGrid.EmptyRows = true;
            this.FeedUploadGrid.ExtendRightColumn = true;
            this.FeedUploadGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.FeedUploadGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("FeedUploadGrid.Images"))));
            this.FeedUploadGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("FeedUploadGrid.Images1"))));
            this.FeedUploadGrid.Location = new System.Drawing.Point(0, 0);
            this.FeedUploadGrid.Name = "FeedUploadGrid";
            this.FeedUploadGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.FeedUploadGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.FeedUploadGrid.PreviewInfo.ZoomFactor = 75;
            this.FeedUploadGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("FeedUploadGrid.PrintInfo.PageSettings")));
            this.FeedUploadGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.FeedUploadGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.FeedUploadGrid.RowHeight = 15;
            this.FeedUploadGrid.Size = new System.Drawing.Size(1031, 257);
            this.FeedUploadGrid.TabIndex = 3;
            this.FeedUploadGrid.Text = "c1TrueDBGrid1";
            this.FeedUploadGrid.UseColumnStyles = false;
            this.FeedUploadGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
            this.FeedUploadGrid.PropBag = resources.GetString("FeedUploadGrid.PropBag");
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.OkRibbonButton);
            this.c1StatusBar1.RightPaneItems.Add(this.CancelRibbonButton);
            // 
            // OkRibbonButton
            // 
            this.OkRibbonButton.Enabled = false;
            this.OkRibbonButton.Name = "OkRibbonButton";
            this.OkRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("OkRibbonButton.SmallImage")));
            this.OkRibbonButton.Text = "OK";
            this.OkRibbonButton.Click += new System.EventHandler(this.OkRibbonButton_Click);
            // 
            // CancelRibbonButton
            // 
            this.CancelRibbonButton.Enabled = false;
            this.CancelRibbonButton.Name = "CancelRibbonButton";
            this.CancelRibbonButton.SmallImage = ((System.Drawing.Image)(resources.GetObject("CancelRibbonButton.SmallImage")));
            this.CancelRibbonButton.Text = "Cancel";
            this.CancelRibbonButton.Click += new System.EventHandler(this.CancelRibbonButton_Click);
            // 
            // InventorySubscriptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1033, 305);
            this.Controls.Add(this.DockingTab);
            this.Controls.Add(this.c1StatusBar1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "InventorySubscriptionsForm";
            this.Text = "Inventory - Subscriptions";
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Silver;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InventorySubscriptionsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.DockingTab)).EndInit();
            this.DockingTab.ResumeLayout(false);
            this.InventorySubscriptionDockingTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SubscriptionGrid)).EndInit();
            this.InventoryFeedUploadDockingTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FeedUploadGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1Command.C1DockingTab DockingTab;
        private C1.Win.C1Command.C1DockingTabPage InventorySubscriptionDockingTabPage;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid SubscriptionGrid;
        private C1.Win.C1Command.C1DockingTabPage InventoryFeedUploadDockingTabPage;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid FeedUploadGrid;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Ribbon.RibbonButton OkRibbonButton;
        private C1.Win.C1Ribbon.RibbonButton CancelRibbonButton;


    }
}