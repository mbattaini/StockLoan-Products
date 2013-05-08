namespace CentralClient
{
	partial class InventoryUploadForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryUploadForm));
			this.InputGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.panel1 = new System.Windows.Forms.Panel();
			this.CommitButton = new C1.Win.C1Input.C1Button();
			this.ParseListButton = new C1.Win.C1Input.C1Button();
			this.DeskinputLabel = new System.Windows.Forms.Label();
			this.DeskTextBox = new C1.Win.C1Input.C1TextBox();
			this.InputTextBox = new C1.Win.C1Input.C1TextBox();
			((System.ComponentModel.ISupportInitialize)(this.InputGrid)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DeskTextBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.InputTextBox)).BeginInit();
			this.SuspendLayout();
			// 
			// InputGrid
			// 
			this.InputGrid.AllowAddNew = true;
			this.InputGrid.CaptionHeight = 17;
			this.InputGrid.DirectionAfterEnter = C1.Win.C1TrueDBGrid.DirectionAfterEnterEnum.MoveNone;
			this.InputGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.InputGrid.EmptyRows = true;
			this.InputGrid.ExtendRightColumn = true;
			this.InputGrid.GroupByCaption = "Drag a column header here to group by that column";
			this.InputGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("InputGrid.Images"))));
			this.InputGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("InputGrid.Images1"))));
			this.InputGrid.Location = new System.Drawing.Point(0, 270);
			this.InputGrid.Name = "InputGrid";
			this.InputGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.InputGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.InputGrid.PreviewInfo.ZoomFactor = 75;
			this.InputGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("InputGrid.PrintInfo.PageSettings")));
			this.InputGrid.RowDivider.Color = System.Drawing.Color.DarkGray;
			this.InputGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.None;
			this.InputGrid.RowHeight = 15;
			this.InputGrid.Size = new System.Drawing.Size(449, 270);
			this.InputGrid.TabIndex = 4;
			this.InputGrid.Text = "c1TrueDBGrid2";
			this.InputGrid.UseColumnStyles = false;
			this.InputGrid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
			this.InputGrid.PropBag = resources.GetString("InputGrid.PropBag");
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.panel1.Controls.Add(this.CommitButton);
			this.panel1.Controls.Add(this.ParseListButton);
			this.panel1.Controls.Add(this.DeskinputLabel);
			this.panel1.Controls.Add(this.DeskTextBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(449, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(185, 540);
			this.panel1.TabIndex = 5;
			// 
			// CommitButton
			// 
			this.CommitButton.Location = new System.Drawing.Point(7, 88);
			this.CommitButton.Name = "CommitButton";
			this.CommitButton.Size = new System.Drawing.Size(173, 23);
			this.CommitButton.TabIndex = 49;
			this.CommitButton.Text = "Commit List";
			this.CommitButton.UseVisualStyleBackColor = true;
			this.CommitButton.Click += new System.EventHandler(this.CommitButton_Click);
			// 
			// ParseListButton
			// 
			this.ParseListButton.Location = new System.Drawing.Point(7, 59);
			this.ParseListButton.Name = "ParseListButton";
			this.ParseListButton.Size = new System.Drawing.Size(173, 23);
			this.ParseListButton.TabIndex = 48;
			this.ParseListButton.Text = "Parse List";
			this.ParseListButton.UseVisualStyleBackColor = true;
			this.ParseListButton.Click += new System.EventHandler(this.ParseListButton_Click);
			// 
			// DeskinputLabel
			// 
			this.DeskinputLabel.AutoSize = true;
			this.DeskinputLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DeskinputLabel.ForeColor = System.Drawing.Color.Black;
			this.DeskinputLabel.Location = new System.Drawing.Point(7, 9);
			this.DeskinputLabel.Name = "DeskinputLabel";
			this.DeskinputLabel.Size = new System.Drawing.Size(41, 13);
			this.DeskinputLabel.TabIndex = 51;
			this.DeskinputLabel.Text = "Desk:";
			this.DeskinputLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DeskTextBox
			// 
			this.DeskTextBox.BackColor = System.Drawing.Color.White;
			this.DeskTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DeskTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DeskTextBox.Location = new System.Drawing.Point(7, 25);
			this.DeskTextBox.Name = "DeskTextBox";
			this.DeskTextBox.Size = new System.Drawing.Size(173, 19);
			this.DeskTextBox.TabIndex = 50;
			this.DeskTextBox.Tag = null;
			this.DeskTextBox.TextDetached = true;
			this.DeskTextBox.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Black;
			// 
			// InputTextBox
			// 
			this.InputTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
			this.InputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.InputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InputTextBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.InputTextBox.Location = new System.Drawing.Point(0, 0);
			this.InputTextBox.Multiline = true;
			this.InputTextBox.Name = "InputTextBox";
			this.InputTextBox.Size = new System.Drawing.Size(449, 540);
			this.InputTextBox.TabIndex = 12;
			this.InputTextBox.Tag = null;
			this.InputTextBox.TextDetached = true;
			this.InputTextBox.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Silver;
			// 
			// InventoryUploadForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(634, 540);
			this.Controls.Add(this.InputGrid);
			this.Controls.Add(this.InputTextBox);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "InventoryUploadForm";
			this.Text = "Inventory - Upload";
			this.VisualStyleHolder = C1.Win.C1Ribbon.VisualStyle.Office2007Silver;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InventoryUploadForm_FormClosed);
			this.LocationChanged += new System.EventHandler(this.InventoryUploadForm_LocationChanged);
			this.Load += new System.EventHandler(this.InventoryUploadForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.InputGrid)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DeskTextBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.InputTextBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid InputGrid;
		private System.Windows.Forms.Panel panel1;
		private C1.Win.C1Input.C1TextBox InputTextBox;
		private C1.Win.C1Input.C1Button CommitButton;
		private C1.Win.C1Input.C1Button ParseListButton;
		private C1.Win.C1Input.C1TextBox DeskTextBox;
		private System.Windows.Forms.Label DeskinputLabel;
	}
}