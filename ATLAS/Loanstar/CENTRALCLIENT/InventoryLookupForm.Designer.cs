namespace CentralClient
{
	partial class InventoryLookupForm
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
            C1.Win.C1Input.C1Label SecIdLookupLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryLookupForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.SecIdLookupTextBox = new C1.Win.C1Input.C1TextBox();
            this.DockingTab = new C1.Win.C1Command.C1DockingTab();
            this.SecIdDockingTabPage = new C1.Win.C1Command.C1DockingTabPage();
            this.SecIdLookupGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.DeskDockingTabPage = new C1.Win.C1Command.C1DockingTabPage();
            this.DeskLookupGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.SearchButton = new C1.Win.C1Input.C1Button();
            SecIdLookupLabel = new C1.Win.C1Input.C1Label();
            ((System.ComponentModel.ISupportInitialize)(SecIdLookupLabel)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SecIdLookupTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DockingTab)).BeginInit();
            this.DockingTab.SuspendLayout();
            this.SecIdDockingTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SecIdLookupGrid)).BeginInit();
            this.DeskDockingTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeskLookupGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // SecIdLookupLabel
            // 
            SecIdLookupLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            SecIdLookupLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            SecIdLookupLabel.ForeColor = System.Drawing.Color.Black;
            SecIdLookupLabel.Location = new System.Drawing.Point(14, 9);
            SecIdLookupLabel.Name = "SecIdLookupLabel";
            SecIdLookupLabel.Size = new System.Drawing.Size(53, 13);
            SecIdLookupLabel.TabIndex = 8;
            SecIdLookupLabel.Tag = null;
            SecIdLookupLabel.Text = "Lookup:";
            SecIdLookupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            SecIdLookupLabel.TextDetached = true;
            SecIdLookupLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.SearchButton);
            this.panel1.Controls.Add(this.SecIdLookupTextBox);
            this.panel1.Controls.Add(SecIdLookupLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 37);
            this.panel1.TabIndex = 0;
            // 
            // SecIdLookupTextBox
            // 
            this.SecIdLookupTextBox.BackColor = System.Drawing.Color.White;
            this.SecIdLookupTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SecIdLookupTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecIdLookupTextBox.Location = new System.Drawing.Point(71, 7);
            this.SecIdLookupTextBox.Name = "SecIdLookupTextBox";
            this.SecIdLookupTextBox.Size = new System.Drawing.Size(185, 19);
            this.SecIdLookupTextBox.TabIndex = 9;
            this.SecIdLookupTextBox.Tag = null;
            this.SecIdLookupTextBox.TextDetached = true;
            this.SecIdLookupTextBox.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black;
            this.SecIdLookupTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SecIdLookupTextBox_KeyPress);
            // 
            // DockingTab
            // 
            this.DockingTab.Controls.Add(this.SecIdDockingTabPage);
            this.DockingTab.Controls.Add(this.DeskDockingTabPage);
            this.DockingTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockingTab.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DockingTab.Location = new System.Drawing.Point(0, 37);
            this.DockingTab.Name = "DockingTab";
            this.DockingTab.Size = new System.Drawing.Size(714, 297);
            this.DockingTab.TabAreaBorder = true;
            this.DockingTab.TabIndex = 7;
            this.DockingTab.TabsSpacing = 5;
            this.DockingTab.TabStyle = C1.Win.C1Command.TabStyleEnum.Office2007;
            this.DockingTab.VisualStyle = C1.Win.C1Command.VisualStyle.Office2007Silver;
            this.DockingTab.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2007Silver;
            // 
            // SecIdDockingTabPage
            // 
            this.SecIdDockingTabPage.Controls.Add(this.SecIdLookupGrid);
            this.SecIdDockingTabPage.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecIdDockingTabPage.Location = new System.Drawing.Point(1, 25);
            this.SecIdDockingTabPage.Name = "SecIdDockingTabPage";
            this.SecIdDockingTabPage.Size = new System.Drawing.Size(712, 271);
            this.SecIdDockingTabPage.TabIndex = 1;
            this.SecIdDockingTabPage.Text = "Secuity ID";
            // 
            // SecIdLookupGrid
            // 
            this.SecIdLookupGrid.BackColor = System.Drawing.Color.GhostWhite;
            this.SecIdLookupGrid.CaptionHeight = 17;
            this.SecIdLookupGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SecIdLookupGrid.EmptyRows = true;
            this.SecIdLookupGrid.ExtendRightColumn = true;
            this.SecIdLookupGrid.FilterBar = true;
            this.SecIdLookupGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.SecIdLookupGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("SecIdLookupGrid.Images"))));
            this.SecIdLookupGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("SecIdLookupGrid.Images1"))));
            this.SecIdLookupGrid.Location = new System.Drawing.Point(0, 0);
            this.SecIdLookupGrid.Name = "SecIdLookupGrid";
            this.SecIdLookupGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.SecIdLookupGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.SecIdLookupGrid.PreviewInfo.ZoomFactor = 75;
            this.SecIdLookupGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("SecIdLookupGrid.PrintInfo.PageSettings")));
            this.SecIdLookupGrid.RecordSelectors = false;
            this.SecIdLookupGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.SecIdLookupGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.SecIdLookupGrid.RowHeight = 15;
            this.SecIdLookupGrid.Size = new System.Drawing.Size(712, 271);
            this.SecIdLookupGrid.TabIndex = 2;
            this.SecIdLookupGrid.Text = "SubscriptionGrid";
            this.SecIdLookupGrid.UseColumnStyles = false;
            this.SecIdLookupGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
            this.SecIdLookupGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.Grid_FormatText);
            this.SecIdLookupGrid.PropBag = resources.GetString("SecIdLookupGrid.PropBag");
            // 
            // DeskDockingTabPage
            // 
            this.DeskDockingTabPage.Controls.Add(this.DeskLookupGrid);
            this.DeskDockingTabPage.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeskDockingTabPage.Location = new System.Drawing.Point(1, 25);
            this.DeskDockingTabPage.Name = "DeskDockingTabPage";
            this.DeskDockingTabPage.Size = new System.Drawing.Size(712, 271);
            this.DeskDockingTabPage.TabIndex = 2;
            this.DeskDockingTabPage.Text = "Desk";
            // 
            // DeskLookupGrid
            // 
            this.DeskLookupGrid.BackColor = System.Drawing.Color.GhostWhite;
            this.DeskLookupGrid.CaptionHeight = 17;
            this.DeskLookupGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeskLookupGrid.EmptyRows = true;
            this.DeskLookupGrid.ExtendRightColumn = true;
            this.DeskLookupGrid.FilterBar = true;
            this.DeskLookupGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.DeskLookupGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("DeskLookupGrid.Images"))));
            this.DeskLookupGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("DeskLookupGrid.Images1"))));
            this.DeskLookupGrid.Location = new System.Drawing.Point(0, 0);
            this.DeskLookupGrid.Name = "DeskLookupGrid";
            this.DeskLookupGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.DeskLookupGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.DeskLookupGrid.PreviewInfo.ZoomFactor = 75;
            this.DeskLookupGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("DeskLookupGrid.PrintInfo.PageSettings")));
            this.DeskLookupGrid.RecordSelectors = false;
            this.DeskLookupGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
            this.DeskLookupGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
            this.DeskLookupGrid.RowHeight = 15;
            this.DeskLookupGrid.Size = new System.Drawing.Size(712, 271);
            this.DeskLookupGrid.TabIndex = 3;
            this.DeskLookupGrid.Text = "c1TrueDBGrid1";
            this.DeskLookupGrid.UseColumnStyles = false;
            this.DeskLookupGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
            this.DeskLookupGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.Grid_FormatText);
            this.DeskLookupGrid.PropBag = resources.GetString("DeskLookupGrid.PropBag");
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(261, 5);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(61, 23);
            this.SearchButton.TabIndex = 48;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            // 
            // InventoryLookupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 334);
            this.Controls.Add(this.DockingTab);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InventoryLookupForm";
            this.Text = "Inventory - Lookup";
            this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Silver;
            this.Load += new System.EventHandler(this.InventoryLookupForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InventoryLookupForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(SecIdLookupLabel)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SecIdLookupTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DockingTab)).EndInit();
            this.DockingTab.ResumeLayout(false);
            this.SecIdDockingTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SecIdLookupGrid)).EndInit();
            this.DeskDockingTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DeskLookupGrid)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private C1.Win.C1Input.C1TextBox SecIdLookupTextBox;
		private C1.Win.C1Command.C1DockingTab DockingTab;
		private C1.Win.C1Command.C1DockingTabPage SecIdDockingTabPage;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid SecIdLookupGrid;
		private C1.Win.C1Command.C1DockingTabPage DeskDockingTabPage;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid DeskLookupGrid;
        private C1.Win.C1Input.C1Button SearchButton;
	}
}