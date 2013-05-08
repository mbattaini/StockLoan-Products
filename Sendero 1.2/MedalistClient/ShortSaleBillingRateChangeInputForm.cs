// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Anetics.Common;

namespace Anetics.Medalist
{
	public class ShortSaleBillingRateChangeInputForm : System.Windows.Forms.Form
	{
		private MainForm mainForm;
		private string secId;
    
		private System.Windows.Forms.Button SubmitButton;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.CheckBox OverwriteCheckBox;
		private C1.Win.C1Input.C1Label ToLabel;
		private C1.Win.C1Input.C1Label FromLabel;
		private System.Windows.Forms.DateTimePicker FromDatePicker;
		private System.Windows.Forms.DateTimePicker ToDatePicker;
		private C1.Win.C1Input.C1Label StatusMessageLabel;
		private C1.Win.C1Input.C1Label StatusLabel;
		private System.Windows.Forms.NumericUpDown RateNumericEdit;
		private C1.Win.C1Input.C1Label RateLabel;
		private C1.Win.C1Input.C1Label SecurityLabel;
		private System.Windows.Forms.Panel BackgroundPanel;
    
		private System.ComponentModel.Container components = null;
    
		public ShortSaleBillingRateChangeInputForm(MainForm mainForm, string secId)
		{    
			InitializeComponent();     
    
			try
			{  
				this.mainForm = mainForm;
				this.secId = secId;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortSaleBillingRateChangeInputForm.ShortSaleBillingRateChangeInputForm]", Log.Error, 1);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if(components != null)
				{
					components.Dispose();
				}
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
			this.SubmitButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.OverwriteCheckBox = new System.Windows.Forms.CheckBox();
			this.ToLabel = new C1.Win.C1Input.C1Label();
			this.FromLabel = new C1.Win.C1Input.C1Label();
			this.FromDatePicker = new System.Windows.Forms.DateTimePicker();
			this.ToDatePicker = new System.Windows.Forms.DateTimePicker();
			this.StatusMessageLabel = new C1.Win.C1Input.C1Label();
			this.StatusLabel = new C1.Win.C1Input.C1Label();
			this.RateNumericEdit = new System.Windows.Forms.NumericUpDown();
			this.RateLabel = new C1.Win.C1Input.C1Label();
			this.SecurityLabel = new C1.Win.C1Input.C1Label();
			this.BackgroundPanel = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.ToLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.FromLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RateNumericEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RateLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SecurityLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// SubmitButton
			// 
			this.SubmitButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.SubmitButton.Location = new System.Drawing.Point(84, 256);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(84, 24);
			this.SubmitButton.TabIndex = 6;
			this.SubmitButton.Text = "&Submit";
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			// 
			// CloseButton
			// 
			this.CloseButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.CloseButton.Location = new System.Drawing.Point(212, 256);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(84, 24);
			this.CloseButton.TabIndex = 7;
			this.CloseButton.Text = "&Close";
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// OverwriteCheckBox
			// 
			this.OverwriteCheckBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.OverwriteCheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomRight;
			this.OverwriteCheckBox.Location = new System.Drawing.Point(108, 156);
			this.OverwriteCheckBox.Name = "OverwriteCheckBox";
			this.OverwriteCheckBox.Size = new System.Drawing.Size(184, 18);
			this.OverwriteCheckBox.TabIndex = 55;
			this.OverwriteCheckBox.Text = "Overwrite Existing Rates:";
			// 
			// ToLabel
			// 
			this.ToLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ToLabel.Location = new System.Drawing.Point(76, 92);
			this.ToLabel.Name = "ToLabel";
			this.ToLabel.Size = new System.Drawing.Size(32, 16);
			this.ToLabel.TabIndex = 54;
			this.ToLabel.Tag = null;
			this.ToLabel.Text = "To:";
			this.ToLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.ToLabel.TextDetached = true;
			// 
			// FromLabel
			// 
			this.FromLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.FromLabel.Location = new System.Drawing.Point(52, 64);
			this.FromLabel.Name = "FromLabel";
			this.FromLabel.Size = new System.Drawing.Size(56, 16);
			this.FromLabel.TabIndex = 53;
			this.FromLabel.Tag = null;
			this.FromLabel.Text = "From:";
			this.FromLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.FromLabel.TextDetached = true;
			// 
			// FromDatePicker
			// 
			this.FromDatePicker.Location = new System.Drawing.Point(108, 60);
			this.FromDatePicker.Name = "FromDatePicker";
			this.FromDatePicker.Size = new System.Drawing.Size(216, 21);
			this.FromDatePicker.TabIndex = 52;
			// 
			// ToDatePicker
			// 
			this.ToDatePicker.Location = new System.Drawing.Point(108, 88);
			this.ToDatePicker.Name = "ToDatePicker";
			this.ToDatePicker.Size = new System.Drawing.Size(216, 21);
			this.ToDatePicker.TabIndex = 51;
			// 
			// StatusMessageLabel
			// 
			this.StatusMessageLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.StatusMessageLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.StatusMessageLabel.ForeColor = System.Drawing.Color.Maroon;
			this.StatusMessageLabel.Location = new System.Drawing.Point(72, 188);
			this.StatusMessageLabel.Name = "StatusMessageLabel";
			this.StatusMessageLabel.Size = new System.Drawing.Size(272, 44);
			this.StatusMessageLabel.TabIndex = 50;
			this.StatusMessageLabel.Tag = null;
			this.StatusMessageLabel.TextDetached = true;
			// 
			// StatusLabel
			// 
			this.StatusLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.StatusLabel.Location = new System.Drawing.Point(16, 188);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(52, 16);
			this.StatusLabel.TabIndex = 49;
			this.StatusLabel.Tag = null;
			this.StatusLabel.Text = "Status:";
			this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.StatusLabel.TextDetached = true;
			// 
			// RateNumericEdit
			// 
			this.RateNumericEdit.DecimalPlaces = 4;
			this.RateNumericEdit.Location = new System.Drawing.Point(112, 120);
			this.RateNumericEdit.Minimum = new System.Decimal(new int[] {
																																		1000,
																																		0,
																																		0,
																																		-2147483648});
			this.RateNumericEdit.Name = "RateNumericEdit";
			this.RateNumericEdit.TabIndex = 56;
			// 
			// RateLabel
			// 
			this.RateLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.RateLabel.Location = new System.Drawing.Point(76, 120);
			this.RateLabel.Name = "RateLabel";
			this.RateLabel.Size = new System.Drawing.Size(36, 16);
			this.RateLabel.TabIndex = 57;
			this.RateLabel.Tag = null;
			this.RateLabel.Text = "Rate:";
			this.RateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.RateLabel.TextDetached = true;
			// 
			// SecurityLabel
			// 
			this.SecurityLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.SecurityLabel.ForeColor = System.Drawing.Color.Navy;
			this.SecurityLabel.Location = new System.Drawing.Point(8, 8);
			this.SecurityLabel.Name = "SecurityLabel";
			this.SecurityLabel.Size = new System.Drawing.Size(364, 20);
			this.SecurityLabel.TabIndex = 58;
			this.SecurityLabel.Tag = null;
			this.SecurityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.SecurityLabel.TextDetached = true;
			// 
			// BackgroundPanel
			// 
			this.BackgroundPanel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.BackgroundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.BackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BackgroundPanel.DockPadding.All = 1;
			this.BackgroundPanel.Location = new System.Drawing.Point(1, 1);
			this.BackgroundPanel.Name = "BackgroundPanel";
			this.BackgroundPanel.Size = new System.Drawing.Size(376, 293);
			this.BackgroundPanel.TabIndex = 59;
			// 
			// ShortSaleBillingRateChangeInputForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(378, 295);
			this.Controls.Add(this.SecurityLabel);
			this.Controls.Add(this.RateLabel);
			this.Controls.Add(this.RateNumericEdit);
			this.Controls.Add(this.OverwriteCheckBox);
			this.Controls.Add(this.ToLabel);
			this.Controls.Add(this.FromLabel);
			this.Controls.Add(this.FromDatePicker);
			this.Controls.Add(this.ToDatePicker);
			this.Controls.Add(this.StatusMessageLabel);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.SubmitButton);
			this.Controls.Add(this.BackgroundPanel);
			this.DockPadding.All = 1;
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ShortSaleBillingRateChangeInputForm";
			this.Text = "ShortSale - Billing - Rates";
			this.Load += new System.EventHandler(this.ShortSaleBillingRateChangeInputForm_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ShortSaleBillingRateChangeInputForm_Paint);
			((System.ComponentModel.ISupportInitialize)(this.ToLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.FromLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusMessageLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatusLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RateNumericEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RateLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SecurityLabel)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

  

		private void ShortSaleBillingRateChangeInputForm_Closed(object sender, System.EventArgs e)
		{
			mainForm.Refresh();
		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void SubmitButton_Click(object sender, System.EventArgs e)
		{
			int recordsUpdated = 0;

			try
			{
				recordsUpdated  = mainForm.ShortSaleAgent.ShortSaleBillingSummaryRateSet(
					DateTime.Parse(FromDatePicker.Text).ToString(Standard.DateFormat),
					DateTime.Parse(ToDatePicker.Text).ToString(Standard.DateFormat),
					secId,
					RateNumericEdit.Value.ToString(),
					OverwriteCheckBox.Checked,
					mainForm.UserId);				
			
				StatusMessageLabel.Text = "Updated " + recordsUpdated + " records for " + secId + ".";
			}
			catch (Exception error)
			{			
				StatusMessageLabel.Text = error.Message;
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		private void ShortSaleBillingRateChangeInputForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Pen pen = new Pen(Color.DimGray, 1.8F);
      
			float x1 = 10;
			float x2 = this.Size.Width - 10;
      
			float y0 = StatusLabel.Location.Y - 5;
			float y1 = StatusMessageLabel.Location.Y  + StatusMessageLabel.Size.Height + 5;
			
			e.Graphics.DrawLine(pen, x1, y0, x2, y0);   
			e.Graphics.DrawLine(pen, x1, y1, x2, y1);   

			e.Graphics.Dispose();
		}

		private void ShortSaleBillingRateChangeInputForm_Load(object sender, System.EventArgs e)
		{
			mainForm.SecId = secId;

			SecurityLabel.Text = mainForm.Symbol + "-" + mainForm.Description;
		}
	}
}
