namespace Atlas_Schedule
{
	partial class AtlasScheduleForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AtlasScheduleForm));
			this.c1ContextMenu1 = new C1.Win.C1Command.C1ContextMenu();
			this.c1CommandLink1 = new C1.Win.C1Command.C1CommandLink();
			this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
			this.TasksGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TasksGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// c1ContextMenu1
			// 
			this.c1ContextMenu1.CommandLinks.AddRange(new C1.Win.C1Command.C1CommandLink[] {
            this.c1CommandLink1});
			this.c1ContextMenu1.Name = "c1ContextMenu1";
			this.c1ContextMenu1.VisualStyleBase = C1.Win.C1Command.VisualStyle.OfficeXP;
			// 
			// c1CommandLink1
			// 
			this.c1CommandLink1.Text = "New Command";
			// 
			// c1CommandHolder1
			// 
			this.c1CommandHolder1.Commands.Add(this.c1ContextMenu1);
			this.c1CommandHolder1.Owner = this;
			// 
			// TasksGrid
			// 
			this.TasksGrid.CaptionHeight = 17;
			this.TasksGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TasksGrid.EmptyRows = true;
			this.TasksGrid.ExtendRightColumn = true;
			this.TasksGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.TasksGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("TasksGrid.Images"))));
			this.TasksGrid.Location = new System.Drawing.Point(0, 0);
			this.TasksGrid.Name = "TasksGrid";
			this.TasksGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.TasksGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.TasksGrid.PreviewInfo.ZoomFactor = 75;
			this.TasksGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("TasksGrid.PrintInfo.PageSettings")));
			this.TasksGrid.RowDivider.Color = System.Drawing.Color.Transparent;
			this.TasksGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
			this.TasksGrid.RowHeight = 15;
			this.TasksGrid.Size = new System.Drawing.Size(1243, 403);
			this.TasksGrid.TabIndex = 0;
			this.TasksGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
			this.TasksGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.TasksGrid_BeforeUpdate);
			this.TasksGrid.PropBag = resources.GetString("TasksGrid.PropBag");
			// 
			// AtlasScheduleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1243, 403);
			this.Controls.Add(this.TasksGrid);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AtlasScheduleForm";
			this.Text = "ATLAS -Schedule";
			this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Silver;
			((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TasksGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private C1.Win.C1Command.C1ContextMenu c1ContextMenu1;
		private C1.Win.C1Command.C1CommandLink c1CommandLink1;
		private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid TasksGrid;
	}
}

