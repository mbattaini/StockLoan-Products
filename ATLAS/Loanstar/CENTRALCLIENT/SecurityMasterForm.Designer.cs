namespace CentralClient
{
    partial class SecurityMasterForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecurityMasterForm));
            this.ShellAppBar = new LogicNP.ShellObjects.ShellAppBar(this.components);
            this.SecurityMasterImageList = new System.Windows.Forms.ImageList(this.components);
            this.MainSecMaster = new CentralClient.SecMaster();
            ((System.ComponentModel.ISupportInitialize)(this.ShellAppBar)).BeginInit();
            this.SuspendLayout();
            // 
            // ShellAppBar
            // 
            this.ShellAppBar.AllowedDragDockingEdges = ((LogicNP.ShellObjects.DragDockingEdges)((LogicNP.ShellObjects.DragDockingEdges.Top | LogicNP.ShellObjects.DragDockingEdges.Bottom)));
            this.ShellAppBar.DockingEdge = LogicNP.ShellObjects.DockingEdges.Top;
            this.ShellAppBar.HostForm = this;
            // 
            // SecurityMasterImageList
            // 
            this.SecurityMasterImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SecurityMasterImageList.ImageStream")));
            this.SecurityMasterImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.SecurityMasterImageList.Images.SetKeyName(0, "down.png");
            this.SecurityMasterImageList.Images.SetKeyName(1, "up.png");
            // 
            // MainSecMaster
            // 
            this.MainSecMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.MainSecMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSecMaster.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainSecMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(128)))), ((int)(((byte)(62)))));
            this.MainSecMaster.Location = new System.Drawing.Point(0, 0);
            this.MainSecMaster.Margin = new System.Windows.Forms.Padding(0);
            this.MainSecMaster.Name = "MainSecMaster";
            this.MainSecMaster.SecId = "";
            this.MainSecMaster.Size = new System.Drawing.Size(1829, 18);
            this.MainSecMaster.TabIndex = 0;
            // 
            // SecurityMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1829, 18);
            this.ControlBox = false;
            this.Controls.Add(this.MainSecMaster);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SecurityMasterForm";
            this.ShowIcon = false;
            this.Text = "SecurityMasterForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SecurityMasterForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SecurityMasterForm_FormClosed);
            this.Load += new System.EventHandler(this.SecurityMasterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ShellAppBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LogicNP.ShellObjects.ShellAppBar ShellAppBar;
		private SecMaster MainSecMaster;
        private System.Windows.Forms.ImageList SecurityMasterImageList;

    }
}