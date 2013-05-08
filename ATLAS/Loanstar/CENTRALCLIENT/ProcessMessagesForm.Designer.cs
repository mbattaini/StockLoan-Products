namespace CentralClient
{
	partial class ProcessMessagesForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessMessagesForm));
			this.ProcessMessagesGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.ProcessMessagesGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// ProcessMessagesGrid
			// 
			this.ProcessMessagesGrid.AllowAddNew = true;
			this.ProcessMessagesGrid.CaptionHeight = 17;
			this.ProcessMessagesGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.ProcessMessagesGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ProcessMessagesGrid.EmptyRows = true;
			this.ProcessMessagesGrid.ExtendRightColumn = true;
			this.ProcessMessagesGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ProcessMessagesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ProcessMessagesGrid.Images"))));
			this.ProcessMessagesGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ProcessMessagesGrid.Images1"))));
			this.ProcessMessagesGrid.Location = new System.Drawing.Point(0, 2);
			this.ProcessMessagesGrid.Name = "ProcessMessagesGrid";
			this.ProcessMessagesGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ProcessMessagesGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ProcessMessagesGrid.PreviewInfo.ZoomFactor = 75;
			this.ProcessMessagesGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("ProcessMessagesGrid.PrintInfo.PageSettings")));
			this.ProcessMessagesGrid.RecordSelectors = false;
			this.ProcessMessagesGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.ProcessMessagesGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.ProcessMessagesGrid.RowHeight = 15;
			this.ProcessMessagesGrid.Size = new System.Drawing.Size(701, 270);
			this.ProcessMessagesGrid.TabIndex = 5;
			this.ProcessMessagesGrid.Text = "c1TrueDBGrid2";
			this.ProcessMessagesGrid.UseColumnStyles = false;
			this.ProcessMessagesGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
			this.ProcessMessagesGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ProcessMessagesGrid_FormatText);
			this.ProcessMessagesGrid.PropBag = resources.GetString("ProcessMessagesGrid.PropBag");
			// 
			// ProcessMessagesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(701, 272);
			this.Controls.Add(this.ProcessMessagesGrid);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ProcessMessagesForm";
			this.Text = "Process - Messages";
			this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Silver;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProcessMessagesForm_FormClosed);
			this.Load += new System.EventHandler(this.ProcessMessagesForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.ProcessMessagesGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ProcessMessagesGrid;
	}
}