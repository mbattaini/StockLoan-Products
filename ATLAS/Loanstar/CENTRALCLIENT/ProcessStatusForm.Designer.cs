namespace CentralClient
{
	partial class ProcessStatusForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessStatusForm));
			this.ProcessStatusGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.ProcessStatusGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// ProcessStatusGrid
			// 
			this.ProcessStatusGrid.AllowAddNew = true;
			this.ProcessStatusGrid.AllowColSelect = false;
			this.ProcessStatusGrid.AllowFilter = false;
			this.ProcessStatusGrid.AllowRowSelect = false;
			this.ProcessStatusGrid.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None;
			this.ProcessStatusGrid.AllowUpdate = false;
			this.ProcessStatusGrid.AllowUpdateOnBlur = false;
			this.ProcessStatusGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(174)))), ((int)(((byte)(181)))));
			this.ProcessStatusGrid.CaptionHeight = 17;
			this.ProcessStatusGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.ProcessStatusGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ProcessStatusGrid.EmptyRows = true;
			this.ProcessStatusGrid.ExtendRightColumn = true;
			this.ProcessStatusGrid.FilterBar = true;
			this.ProcessStatusGrid.GroupByAreaVisible = false;
			this.ProcessStatusGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.ProcessStatusGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ProcessStatusGrid.Images"))));
			this.ProcessStatusGrid.Location = new System.Drawing.Point(0, 2);
			this.ProcessStatusGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.NoMarquee;
			this.ProcessStatusGrid.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.Simple;
			this.ProcessStatusGrid.Name = "ProcessStatusGrid";
			this.ProcessStatusGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.ProcessStatusGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.ProcessStatusGrid.PreviewInfo.ZoomFactor = 75;
			this.ProcessStatusGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("ProcessStatusGrid.PrintInfo.PageSettings")));
			this.ProcessStatusGrid.RecordSelectors = false;
			this.ProcessStatusGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.ProcessStatusGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.ProcessStatusGrid.RowHeight = 15;
			this.ProcessStatusGrid.Size = new System.Drawing.Size(1258, 270);
			this.ProcessStatusGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
			this.ProcessStatusGrid.TabIndex = 6;
			this.ProcessStatusGrid.Text = "c1TrueDBGrid2";
			this.ProcessStatusGrid.UseColumnStyles = false;
			this.ProcessStatusGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
			this.ProcessStatusGrid.WrapCellPointer = true;
			this.ProcessStatusGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.ProcessStatusGrid_FormatText);
			this.ProcessStatusGrid.PropBag = resources.GetString("ProcessStatusGrid.PropBag");
			// 
			// ProcessStatusForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1258, 272);
			this.Controls.Add(this.ProcessStatusGrid);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ProcessStatusForm";
			this.Text = "Process - Status";
			this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Silver;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProcessStatusForm_FormClosed);
			this.Load += new System.EventHandler(this.ProcessStatusForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.ProcessStatusGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ProcessStatusGrid;
	}
}