using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Remoting;
using System.ComponentModel;
using System.Windows.Forms;

using StockLoan.Common;
using RebateBillingBusiness;

namespace Golden
{
	public class ShortSaleNegativeRebateMarkUpForm : System.Windows.Forms.Form
	{
	
        private System.ComponentModel.Container components = null;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid ShortSaleTradingGroupsGrid;
		private MainForm mainForm;

		private string tradingGroup = "";

		private DataSet dataSet = null;		
		private System.Windows.Forms.ContextMenu MainMenu;
		private System.Windows.Forms.MenuItem Seperator;
		private System.Windows.Forms.MenuItem ExitMenuItem;		
		private System.Windows.Forms.MenuItem TradingGroupsSendToEmailMenuItem;
		private System.Windows.Forms.MenuItem TradingGroupsSendToClipboardMenuItem;
		private System.Windows.Forms.MenuItem TradingGroupsSendToExcelMenuItem;
		private System.Windows.Forms.MenuItem TradingGroupsSendToMenuItem;
		private C1.Win.C1Input.C1Label NegativeRebateMarkUpDefaultLabel;
		private C1.Win.C1Input.C1NumericEdit MarkUpDefaultNumericEdit;
		private System.Windows.Forms.MenuItem ManageMenuItem;
		private System.Windows.Forms.MenuItem ManageAccountMenuItem;
        private RadioButton rdoBPS;
        private RadioButton rdoPenson;
        private Label lblSystem;
		private System.Windows.Forms.MenuItem ManageOfficeCodesMenuItem;

		public ShortSaleNegativeRebateMarkUpForm(MainForm mainForm)
		{
			InitializeComponent();
			this.mainForm = mainForm;
			
			dataSet = new DataSet();

			try
			{
				ShortSaleTradingGroupsGrid.AllowUpdate = true;
			}
			catch(Exception e)
			{
                MessageBox.Show(e.Message);
			}
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortSaleNegativeRebateMarkUpForm));
            this.ShortSaleTradingGroupsGrid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.MainMenu = new System.Windows.Forms.ContextMenu();
            this.ManageMenuItem = new System.Windows.Forms.MenuItem();
            this.TradingGroupsSendToMenuItem = new System.Windows.Forms.MenuItem();
            this.TradingGroupsSendToEmailMenuItem = new System.Windows.Forms.MenuItem();
            this.TradingGroupsSendToClipboardMenuItem = new System.Windows.Forms.MenuItem();
            this.TradingGroupsSendToExcelMenuItem = new System.Windows.Forms.MenuItem();
            this.Seperator = new System.Windows.Forms.MenuItem();
            this.ExitMenuItem = new System.Windows.Forms.MenuItem();
            this.ManageAccountMenuItem = new System.Windows.Forms.MenuItem();
            this.ManageOfficeCodesMenuItem = new System.Windows.Forms.MenuItem();
            this.MarkUpDefaultNumericEdit = new C1.Win.C1Input.C1NumericEdit();
            this.NegativeRebateMarkUpDefaultLabel = new C1.Win.C1Input.C1Label();
            this.rdoBPS = new System.Windows.Forms.RadioButton();
            this.rdoPenson = new System.Windows.Forms.RadioButton();
            this.lblSystem = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ShortSaleTradingGroupsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarkUpDefaultNumericEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NegativeRebateMarkUpDefaultLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // ShortSaleTradingGroupsGrid
            // 
            this.ShortSaleTradingGroupsGrid.CaptionHeight = 17;
            this.ShortSaleTradingGroupsGrid.ContextMenu = this.MainMenu;
            this.ShortSaleTradingGroupsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShortSaleTradingGroupsGrid.ExtendRightColumn = true;
            this.ShortSaleTradingGroupsGrid.FetchRowStyles = true;
            this.ShortSaleTradingGroupsGrid.FilterBar = true;
            this.ShortSaleTradingGroupsGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShortSaleTradingGroupsGrid.GroupByCaption = "Drag a column header here to group by that column";
            this.ShortSaleTradingGroupsGrid.Images.Add(((System.Drawing.Image)(resources.GetObject("ShortSaleTradingGroupsGrid.Images"))));
            this.ShortSaleTradingGroupsGrid.Location = new System.Drawing.Point(1, 40);
            this.ShortSaleTradingGroupsGrid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.DottedRowBorder;
            this.ShortSaleTradingGroupsGrid.Name = "ShortSaleTradingGroupsGrid";
            this.ShortSaleTradingGroupsGrid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.ShortSaleTradingGroupsGrid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.ShortSaleTradingGroupsGrid.PreviewInfo.ZoomFactor = 75D;
            this.ShortSaleTradingGroupsGrid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("ShortSaleTradingGroupsGrid.PrintInfo.PageSettings")));
            this.ShortSaleTradingGroupsGrid.RowDivider.Color = System.Drawing.Color.LightGray;
            this.ShortSaleTradingGroupsGrid.RowDivider.Style = C1.Win.C1TrueDBGrid.LineStyleEnum.Single;
            this.ShortSaleTradingGroupsGrid.RowHeight = 15;
            this.ShortSaleTradingGroupsGrid.Size = new System.Drawing.Size(881, 432);
            this.ShortSaleTradingGroupsGrid.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
            this.ShortSaleTradingGroupsGrid.TabIndex = 0;
            this.ShortSaleTradingGroupsGrid.Text = "Trading Groups";
            this.ShortSaleTradingGroupsGrid.BeforeUpdate += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.ShortSaleTradingGroupsGrid_BeforeUpdate);
            this.ShortSaleTradingGroupsGrid.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.ShortSaleTradingGroupsGrid_RowColChange);
            this.ShortSaleTradingGroupsGrid.FormatText += new C1.Win.C1TrueDBGrid.FormatTextEventHandler(this.FormatText);
            this.ShortSaleTradingGroupsGrid.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.ShortSaleTradingGroupsGrid_FetchRowStyle);
            this.ShortSaleTradingGroupsGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ShortSaleTradingGroupsGrid_KeyPress);
            this.ShortSaleTradingGroupsGrid.PropBag = resources.GetString("ShortSaleTradingGroupsGrid.PropBag");
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ManageMenuItem,
            this.TradingGroupsSendToMenuItem,
            this.Seperator,
            this.ExitMenuItem});
            // 
            // ManageMenuItem
            // 
            this.ManageMenuItem.Index = 0;
            this.ManageMenuItem.Text = "";
            // 
            // TradingGroupsSendToMenuItem
            // 
            this.TradingGroupsSendToMenuItem.Index = 1;
            this.TradingGroupsSendToMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.TradingGroupsSendToEmailMenuItem,
            this.TradingGroupsSendToClipboardMenuItem,
            this.TradingGroupsSendToExcelMenuItem});
            this.TradingGroupsSendToMenuItem.Text = "Trading Groups Send To";
            // 
            // TradingGroupsSendToEmailMenuItem
            // 
            this.TradingGroupsSendToEmailMenuItem.Index = 0;
            this.TradingGroupsSendToEmailMenuItem.Text = "Email";
            this.TradingGroupsSendToEmailMenuItem.Click += new System.EventHandler(this.TradingGroupsSendToEmailMenuItem_Click);
            // 
            // TradingGroupsSendToClipboardMenuItem
            // 
            this.TradingGroupsSendToClipboardMenuItem.Index = 1;
            this.TradingGroupsSendToClipboardMenuItem.Text = "Clipboard";
            this.TradingGroupsSendToClipboardMenuItem.Click += new System.EventHandler(this.TradingGroupsSendToClipboardMenuItem_Click);
            // 
            // TradingGroupsSendToExcelMenuItem
            // 
            this.TradingGroupsSendToExcelMenuItem.Index = 2;
            this.TradingGroupsSendToExcelMenuItem.Text = "Excel";
            this.TradingGroupsSendToExcelMenuItem.Click += new System.EventHandler(this.TradingGroupsSendToExcelMenuItem_Click);
            // 
            // Seperator
            // 
            this.Seperator.Index = 2;
            this.Seperator.Text = "-";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Index = 3;
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // ManageAccountMenuItem
            // 
            this.ManageAccountMenuItem.Index = -1;
            this.ManageAccountMenuItem.Text = "";
            // 
            // ManageOfficeCodesMenuItem
            // 
            this.ManageOfficeCodesMenuItem.Index = -1;
            this.ManageOfficeCodesMenuItem.Text = "";
            // 
            // MarkUpDefaultNumericEdit
            // 
            this.MarkUpDefaultNumericEdit.Location = new System.Drawing.Point(216, 11);
            this.MarkUpDefaultNumericEdit.Name = "MarkUpDefaultNumericEdit";
            this.MarkUpDefaultNumericEdit.Size = new System.Drawing.Size(80, 21);
            this.MarkUpDefaultNumericEdit.TabIndex = 4;
            this.MarkUpDefaultNumericEdit.Tag = null;
            this.MarkUpDefaultNumericEdit.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
            this.MarkUpDefaultNumericEdit.TextChanged += new System.EventHandler(this.MarkUpDefaultNumericEdit_TextChanged);
            this.MarkUpDefaultNumericEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MarkUpDefaultNumericEdit_KeyPress);
            // 
            // NegativeRebateMarkUpDefaultLabel
            // 
            this.NegativeRebateMarkUpDefaultLabel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.NegativeRebateMarkUpDefaultLabel.ForeColor = System.Drawing.Color.Black;
            this.NegativeRebateMarkUpDefaultLabel.Location = new System.Drawing.Point(4, 11);
            this.NegativeRebateMarkUpDefaultLabel.MaximumSize = new System.Drawing.Size(225, 40);
            this.NegativeRebateMarkUpDefaultLabel.Name = "NegativeRebateMarkUpDefaultLabel";
            this.NegativeRebateMarkUpDefaultLabel.Size = new System.Drawing.Size(210, 21);
            this.NegativeRebateMarkUpDefaultLabel.TabIndex = 5;
            this.NegativeRebateMarkUpDefaultLabel.Tag = null;
            this.NegativeRebateMarkUpDefaultLabel.Text = "Default Negative Rebate Mark Up";
            this.NegativeRebateMarkUpDefaultLabel.TextDetached = true;
            this.NegativeRebateMarkUpDefaultLabel.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Silver;
            // 
            // rdoBPS
            // 
            this.rdoBPS.AutoSize = true;
            this.rdoBPS.Checked = true;
            this.rdoBPS.Location = new System.Drawing.Point(590, 11);
            this.rdoBPS.Name = "rdoBPS";
            this.rdoBPS.Size = new System.Drawing.Size(91, 17);
            this.rdoBPS.TabIndex = 6;
            this.rdoBPS.TabStop = true;
            this.rdoBPS.Text = "BroadRidge";
            this.rdoBPS.UseVisualStyleBackColor = true;
            this.rdoBPS.CheckedChanged += new System.EventHandler(this.rdoBPS_CheckedChanged);
            // 
            // rdoPenson
            // 
            this.rdoPenson.Location = new System.Drawing.Point(499, 11);
            this.rdoPenson.Name = "rdoPenson";
            this.rdoPenson.Size = new System.Drawing.Size(75, 17);
            this.rdoPenson.TabIndex = 7;
            this.rdoPenson.Text = "Penson";
            this.rdoPenson.UseVisualStyleBackColor = true;
            this.rdoPenson.CheckedChanged += new System.EventHandler(this.rdoPenson_CheckedChanged);
            // 
            // lblSystem
            // 
            this.lblSystem.AutoSize = true;
            this.lblSystem.Location = new System.Drawing.Point(429, 12);
            this.lblSystem.Name = "lblSystem";
            this.lblSystem.Size = new System.Drawing.Size(55, 13);
            this.lblSystem.TabIndex = 8;
            this.lblSystem.Text = "System:";
            this.lblSystem.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ShortSaleNegativeRebateMarkUpForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(883, 473);
            this.Controls.Add(this.NegativeRebateMarkUpDefaultLabel);
            this.Controls.Add(this.lblSystem);
            this.Controls.Add(this.rdoPenson);
            this.Controls.Add(this.rdoBPS);
            this.Controls.Add(this.MarkUpDefaultNumericEdit);
            this.Controls.Add(this.ShortSaleTradingGroupsGrid);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShortSaleNegativeRebateMarkUpForm";
            this.Padding = new System.Windows.Forms.Padding(1, 40, 1, 1);
            this.Text = "Short Sale Billing - Set MarkUps For Trading Groups";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ShortSaleNegativeRebateMarkUpForm_Closing);
            this.Load += new System.EventHandler(this.ShortSaleNegativeRebateMarkUpForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ShortSaleTradingGroupsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MarkUpDefaultNumericEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NegativeRebateMarkUpDefaultLabel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

    
		public void LoadTradingGroupGrid(string effectDate, string platForm)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.Refresh();

				dataSet = mainForm.RebateAgent.ShortSaleBillingSummaryTradingGroupGet("", (rdoPenson.Checked) ? "Penson" : "BroadRidge");								
																
				ShortSaleTradingGroupsGrid.SetDataBinding(dataSet, "TradingGroups", true);						
			
                if(platForm.ToLower().ToString().Trim().Equals("penson"))
                {
                    MarkUpDefaultNumericEdit.Value = mainForm.RebateAgent.KeyValueGet("ShortSaleNegativeDefaultMarkUp", "0.25");
                }
                else
                {
                    MarkUpDefaultNumericEdit.Value = mainForm.RebateAgent.KeyValueGet("ShortSaleBPSNegativeDefaultMarkUp", "0.25");
                }

			}
			catch(Exception e)
			{
                Log.Write(e.Message + " [ShortSaleNegativeRebateMarkUpForm.LoadTradingGroupGrid]", Log.Error, 1); 
			}

			this.Cursor = Cursors.Default;
		}

        private void ShortSaleNegativeRebateMarkUpForm_Load(object sender, System.EventArgs e)
		{
			int height = mainForm.Height / 2;
			int width  = mainForm.Width / 2;
      
			try
			{
				this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
				this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
				this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
				this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

                LoadTradingGroupGrid("", (rdoPenson.Checked) ? "Penson" : "BroadRidge");      
			}
			catch(Exception ee)
			{
                MessageBox.Show(ee.Message);
			}
		}

		private void ShortSaleNegativeRebateMarkUpForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name,  "Top",  this.Top.ToString());    
				RegistryValue.Write(this.Name,  "Left",  this.Left.ToString());    
				RegistryValue.Write(this.Name,  "Height",  this.Height.ToString());    
				RegistryValue.Write(this.Name,  "Width",  this.Width.ToString());    
			}
            if (ShortSaleTradingGroupsGrid.DataChanged) 
            {
                UpdateMarkUpData();
            }
      }

    private void UpdateMarkUpData()
      {
          try
          {
              int iparamEditFlag = 1;
              string negMarkup = null;
              if (!ShortSaleTradingGroupsGrid.Columns["NegativeRebateMarkUp"].Text.ToString().Trim().Equals(""))
              { 
                  negMarkup = ShortSaleTradingGroupsGrid.Columns["NegativeRebateMarkUp"].Text.ToString().Trim(); 
              }
              
              mainForm.RebateAgent.ShortSaleBillingSummaryTradingGroupSet(
              ShortSaleTradingGroupsGrid.Columns["GroupCode"].Text,
              negMarkup, ShortSaleTradingGroupsGrid.Columns["NegativeRebateBill"].Text,
              mainForm.UserId, ((rdoPenson.Checked) ? "Penson" : "BroadRidge"), iparamEditFlag);
            }
            catch (Exception err)
            {
                Log.Write(err.Message + " [ShortSaleNegativeRebateMarkUpForm.Closing]", Log.Error, 1);
                MessageBox.Show(err.Message);

            }
        }
   
		private void FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "NegativeRebateMarkUp":
				case "PositiveRebateMarkUp":
				case "FedFundsMarkUp":
				case "LiborFundsMarkUp":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.000");
					}
					catch {}
					break;			
				
				case "ActTime":
					try
					{
						e.Value = DateTime.Parse(e.Value.ToString()).ToString(Standard.DateTimeFormat);
					}
					catch {}
					break;
			}		
		}

        private void ShortSaleTradingGroupsGrid_RowColChange(object sender, C1.Win.C1TrueDBGrid.RowColChangeEventArgs e)
        {
            if (this.ShortSaleTradingGroupsGrid.DataChanged)
            {
                ShortSaleTradingGroupsGrid.UpdateData();
            }
        }


        private void ShortSaleTradingGroupsGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
		{
            try
            {
                if (!bool.Parse(ShortSaleTradingGroupsGrid.Columns["IsActive"].CellValue(e.Row).ToString()))
                {
                    e.CellStyle.BackColor = System.Drawing.Color.LightGray;
                }
            }
            catch { }
		}

		private void ExitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void TradingGroupsSendToEmailMenuItem_Click(object sender, System.EventArgs e)
		{
            mainForm.SendToEmail(ref ShortSaleTradingGroupsGrid);
		}

        private void ShortSaleTradingGroupsGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            if (ShortSaleTradingGroupsGrid.DataChanged) 
            {
                UpdateMarkUpData();
            }
        }

        private void TradingGroupsSendToClipboardMenuItem_Click(object sender, System.EventArgs e)
		{
			string gridData = "";

			foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
			{
				gridData += dataColumn.Caption + "\t";
			}
			gridData += "\r\n";

			foreach (int row in ShortSaleTradingGroupsGrid.SelectedRows)
			{
				foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in ShortSaleTradingGroupsGrid.SelectedCols)
				{
					gridData += dataColumn.CellText(row) + "\t";
				}
				gridData += "\r\n";
			}

			Clipboard.SetDataObject(gridData, true);
	
		}

		private void TradingGroupsSendToExcelMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

            mainForm.SendToExcel(ref ShortSaleTradingGroupsGrid);

			this.Cursor = Cursors.Default;
		}

         private void MarkUpDefaultNumericEdit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)13))
            {
                string platForm = ((rdoPenson.Checked) ? "Penson" : "BroadRidge");
                if(platForm.ToLower().ToString().Trim().Equals("penson"))
                {
                    mainForm.RebateAgent.KeyValueSet("ShortSaleNegativeDefaultMarkUp", MarkUpDefaultNumericEdit.Text.ToString());
                }
                else
                {
                    mainForm.RebateAgent.KeyValueSet("ShortSaleBPSNegativeDefaultMarkUp", MarkUpDefaultNumericEdit.Text.ToString());
                }
            }
        }

		private void ShortSaleTradingGroupsGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)13))    
			{
                ShortSaleTradingGroupsGrid.UpdateData();
			}
		}

        private void rdoBPS_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBPS.Checked)
            {
                LoadTradingGroupGrid("", (rdoPenson.Checked) ? "Penson" : "BroadRidge");
            }

        }

        private void rdoPenson_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPenson.Checked)
            {
                LoadTradingGroupGrid("", (rdoPenson.Checked) ? "Penson" : "BroadRidge");
            }
        }

        private void MarkUpDefaultNumericEdit_TextChanged(object sender, EventArgs e)
        {
            string platForm = ((rdoPenson.Checked) ? "Penson" : "BroadRidge");
            if (platForm.ToLower().ToString().Trim().Equals("penson"))
            {
                mainForm.RebateAgent.KeyValueSet("ShortSaleNegativeDefaultMarkUp", MarkUpDefaultNumericEdit.Text.ToString());
            }
            else
            {
                mainForm.RebateAgent.KeyValueSet("ShortSaleBPSNegativeDefaultMarkUp", MarkUpDefaultNumericEdit.Text.ToString());
            }

        }
    }
}
