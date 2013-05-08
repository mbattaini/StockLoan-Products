using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Anetics.Medalist
{
    
	public class SubstitutionStatus : System.Windows.Forms.UserControl
	{
		private C1.Win.C1Input.C1Label StatusLabel;
		private System.ComponentModel.IContainer components;

		public SubstitutionStatus()
		{      
			InitializeComponent();
		}
    

		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(components != null)
				{
					components.Dispose();
				}
			}

			base.Dispose(disposing);
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// StatusLabel
			// 
			this.StatusLabel.BackColor = System.Drawing.Color.Lime;
			this.StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.StatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.StatusLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.StatusLabel.Location = new System.Drawing.Point(0, 0);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(56, 16);
			this.StatusLabel.TabIndex = 0;
			this.StatusLabel.Tag = null;
			this.StatusLabel.Text = "Online";
			this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.StatusLabel.TextDetached = true;
			// 
			// SubstitutionStatus
			// 
			this.BackColor = System.Drawing.Color.Gray;
			this.Controls.Add(this.StatusLabel);
			this.Name = "SubstitutionStatus";
			this.Size = new System.Drawing.Size(56, 16);
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	
		public bool IsSubstiutionActive
		{
			set 
			{
				try
				{					
					if ((bool)value)
					{
						this.StatusLabel.BackColor = System.Drawing.Color.Lime;
						StatusLabel.Text = "Online";
					}
					else
					{
						this.StatusLabel.BackColor = System.Drawing.Color.Coral;
						StatusLabel.Text = "Offline";
					}
				}
				catch
				{	
					this.BackColor = System.Drawing.Color.Gray;
					StatusLabel.Text = "";
				}
			}
		}	
	}
}
