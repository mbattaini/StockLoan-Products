// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC  2005, 2006  All rights reserved.

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using	Anetics.Common;

namespace Anetics.Medalist 
{
	public class CreditControl : System.Windows.Forms.UserControl
	{
		#region Member Variables

		private string bookGroup;
		private string book;
		
		private MainForm mainForm;
		private DataSet dataSetDeals = null;    
		private DataSet dataSetContracts = null;    
		private DataSet dataSetBookLimits = null;    

		private System.Windows.Forms.GroupBox BookParentGroupBox;
		private System.ComponentModel.Container components = null;

    private C1.Win.C1Input.C1Label BorrowsLabel;
    private C1.Win.C1Input.C1Label LoansLabel;
    private C1.Win.C1Input.C1Label TotalLabel;

    private C1.Win.C1Input.C1Label OpenLabel;
    private C1.Win.C1Input.C1Label OpenBorrowsLabel;
    private C1.Win.C1Input.C1Label OpenLoansLabel;
    private C1.Win.C1Input.C1Label OpenTotalLabel;
    
    private C1.Win.C1Input.C1Label DealsLabel;
    private C1.Win.C1Input.C1Label DealBorrowsLabel;
    private C1.Win.C1Input.C1Label DealLoansLabel;
    private C1.Win.C1Input.C1Label DealTotalLabel;
    
    private C1.Win.C1Input.C1Label LimitLabel;
    private C1.Win.C1Input.C1Label LimitBorrowsLabel;
    private C1.Win.C1Input.C1Label LimitLoansLabel;
    private C1.Win.C1Input.C1Label LimitTotalLabel;

    private C1.Win.C1Input.C1Label BalanceLabel;
    private C1.Win.C1Input.C1Label BalanceBorrowsLabel;
    private C1.Win.C1Input.C1Label BalanceLoansLabel;
    private C1.Win.C1Input.C1Label BalanceTotalLabel;

		private ArrayList dealEventArgsArray;
		private DealEventWrapper dealEventWrapper = null;
		private DealEventHandler dealEventHandler = null;

		private const string DEAL_ID_PREFIX = "D";
		private bool isReady = false;
    private bool bookChanged = false;
		private bool bookGroupChanged = false;


		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.BookParentGroupBox = new System.Windows.Forms.GroupBox();
			this.LimitTotalLabel = new C1.Win.C1Input.C1Label();
			this.BalanceTotalLabel = new C1.Win.C1Input.C1Label();
			this.DealTotalLabel = new C1.Win.C1Input.C1Label();
			this.OpenTotalLabel = new C1.Win.C1Input.C1Label();
			this.BalanceBorrowsLabel = new C1.Win.C1Input.C1Label();
			this.LimitBorrowsLabel = new C1.Win.C1Input.C1Label();
			this.DealBorrowsLabel = new C1.Win.C1Input.C1Label();
			this.OpenLoansLabel = new C1.Win.C1Input.C1Label();
			this.BalanceLoansLabel = new C1.Win.C1Input.C1Label();
			this.LimitLoansLabel = new C1.Win.C1Input.C1Label();
			this.DealLoansLabel = new C1.Win.C1Input.C1Label();
			this.OpenBorrowsLabel = new C1.Win.C1Input.C1Label();
			this.TotalLabel = new C1.Win.C1Input.C1Label();
			this.BorrowsLabel = new C1.Win.C1Input.C1Label();
			this.LoansLabel = new C1.Win.C1Input.C1Label();
			this.BalanceLabel = new C1.Win.C1Input.C1Label();
			this.LimitLabel = new C1.Win.C1Input.C1Label();
			this.DealsLabel = new C1.Win.C1Input.C1Label();
			this.OpenLabel = new C1.Win.C1Input.C1Label();
			this.BookParentGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.LimitTotalLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BalanceTotalLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DealTotalLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.OpenTotalLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BalanceBorrowsLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LimitBorrowsLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DealBorrowsLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.OpenLoansLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BalanceLoansLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LimitLoansLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DealLoansLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.OpenBorrowsLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TotalLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BorrowsLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LoansLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BalanceLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LimitLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DealsLabel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.OpenLabel)).BeginInit();
			this.SuspendLayout();
			// 
			// BookParentGroupBox
			// 
			this.BookParentGroupBox.Controls.Add(this.LimitTotalLabel);
			this.BookParentGroupBox.Controls.Add(this.BalanceTotalLabel);
			this.BookParentGroupBox.Controls.Add(this.DealTotalLabel);
			this.BookParentGroupBox.Controls.Add(this.OpenTotalLabel);
			this.BookParentGroupBox.Controls.Add(this.BalanceBorrowsLabel);
			this.BookParentGroupBox.Controls.Add(this.LimitBorrowsLabel);
			this.BookParentGroupBox.Controls.Add(this.DealBorrowsLabel);
			this.BookParentGroupBox.Controls.Add(this.OpenLoansLabel);
			this.BookParentGroupBox.Controls.Add(this.BalanceLoansLabel);
			this.BookParentGroupBox.Controls.Add(this.LimitLoansLabel);
			this.BookParentGroupBox.Controls.Add(this.DealLoansLabel);
			this.BookParentGroupBox.Controls.Add(this.OpenBorrowsLabel);
			this.BookParentGroupBox.Controls.Add(this.TotalLabel);
			this.BookParentGroupBox.Controls.Add(this.BorrowsLabel);
			this.BookParentGroupBox.Controls.Add(this.LoansLabel);
			this.BookParentGroupBox.Controls.Add(this.BalanceLabel);
			this.BookParentGroupBox.Controls.Add(this.LimitLabel);
			this.BookParentGroupBox.Controls.Add(this.DealsLabel);
			this.BookParentGroupBox.Controls.Add(this.OpenLabel);
			this.BookParentGroupBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BookParentGroupBox.Location = new System.Drawing.Point(0, 0);
			this.BookParentGroupBox.Name = "BookParentGroupBox";
			this.BookParentGroupBox.Size = new System.Drawing.Size(344, 104);
			this.BookParentGroupBox.TabIndex = 44;
			this.BookParentGroupBox.TabStop = false;
			this.BookParentGroupBox.Paint += new System.Windows.Forms.PaintEventHandler(this.BookParentGroupBox_Paint);
			// 
			// LimitTotalLabel
			// 
			this.LimitTotalLabel.CustomFormat = "#,##0";
			this.LimitTotalLabel.DataType = typeof(long);
			this.LimitTotalLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.LimitTotalLabel.Location = new System.Drawing.Point(248, 64);
			this.LimitTotalLabel.Name = "LimitTotalLabel";
			this.LimitTotalLabel.Size = new System.Drawing.Size(88, 12);
			this.LimitTotalLabel.TabIndex = 15;
			this.LimitTotalLabel.Tag = null;
			this.LimitTotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LimitTotalLabel.Value = ((long)(0));
			// 
			// BalanceTotalLabel
			// 
			this.BalanceTotalLabel.CustomFormat = "#,##0";
			this.BalanceTotalLabel.DataType = typeof(long);
			this.BalanceTotalLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.BalanceTotalLabel.Location = new System.Drawing.Point(248, 84);
			this.BalanceTotalLabel.Name = "BalanceTotalLabel";
			this.BalanceTotalLabel.Size = new System.Drawing.Size(88, 12);
			this.BalanceTotalLabel.TabIndex = 19;
			this.BalanceTotalLabel.Tag = null;
			this.BalanceTotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BalanceTotalLabel.Value = ((long)(0));
			// 
			// DealTotalLabel
			// 
			this.DealTotalLabel.CustomFormat = "#,##0";
			this.DealTotalLabel.DataType = typeof(System.Decimal);
			this.DealTotalLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.DealTotalLabel.Location = new System.Drawing.Point(248, 48);
			this.DealTotalLabel.Name = "DealTotalLabel";
			this.DealTotalLabel.Size = new System.Drawing.Size(88, 12);
			this.DealTotalLabel.TabIndex = 11;
			this.DealTotalLabel.Tag = null;
			this.DealTotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DealTotalLabel.Value = new System.Decimal(new int[] {
																																 0,
																																 0,
																																 0,
																																 0});
			// 
			// OpenTotalLabel
			// 
			this.OpenTotalLabel.CustomFormat = "#,##0";
			this.OpenTotalLabel.DataType = typeof(System.Decimal);
			this.OpenTotalLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.OpenTotalLabel.Location = new System.Drawing.Point(248, 32);
			this.OpenTotalLabel.Name = "OpenTotalLabel";
			this.OpenTotalLabel.Size = new System.Drawing.Size(88, 12);
			this.OpenTotalLabel.TabIndex = 7;
			this.OpenTotalLabel.Tag = null;
			this.OpenTotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.OpenTotalLabel.Value = new System.Decimal(new int[] {
																																 0,
																																 0,
																																 0,
																																 0});
			// 
			// BalanceBorrowsLabel
			// 
			this.BalanceBorrowsLabel.CustomFormat = "#,##0";
			this.BalanceBorrowsLabel.DataType = typeof(long);
			this.BalanceBorrowsLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.BalanceBorrowsLabel.Location = new System.Drawing.Point(64, 84);
			this.BalanceBorrowsLabel.Name = "BalanceBorrowsLabel";
			this.BalanceBorrowsLabel.Size = new System.Drawing.Size(88, 12);
			this.BalanceBorrowsLabel.TabIndex = 17;
			this.BalanceBorrowsLabel.Tag = null;
			this.BalanceBorrowsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BalanceBorrowsLabel.Value = ((long)(0));
			// 
			// LimitBorrowsLabel
			// 
			this.LimitBorrowsLabel.CustomFormat = "#,##0";
			this.LimitBorrowsLabel.DataType = typeof(long);
			this.LimitBorrowsLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.LimitBorrowsLabel.Location = new System.Drawing.Point(64, 64);
			this.LimitBorrowsLabel.Name = "LimitBorrowsLabel";
			this.LimitBorrowsLabel.Size = new System.Drawing.Size(88, 12);
			this.LimitBorrowsLabel.TabIndex = 13;
			this.LimitBorrowsLabel.Tag = null;
			this.LimitBorrowsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LimitBorrowsLabel.Value = ((long)(0));
			// 
			// DealBorrowsLabel
			// 
			this.DealBorrowsLabel.CustomFormat = "#,##0";
			this.DealBorrowsLabel.DataType = typeof(System.Decimal);
			this.DealBorrowsLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.DealBorrowsLabel.Location = new System.Drawing.Point(64, 48);
			this.DealBorrowsLabel.Name = "DealBorrowsLabel";
			this.DealBorrowsLabel.Size = new System.Drawing.Size(88, 12);
			this.DealBorrowsLabel.TabIndex = 9;
			this.DealBorrowsLabel.Tag = null;
			this.DealBorrowsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DealBorrowsLabel.Value = new System.Decimal(new int[] {
																																	 0,
																																	 0,
																																	 0,
																																	 0});
			// 
			// OpenLoansLabel
			// 
			this.OpenLoansLabel.CustomFormat = "#,##0";
			this.OpenLoansLabel.DataType = typeof(System.Decimal);
			this.OpenLoansLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.OpenLoansLabel.Location = new System.Drawing.Point(156, 32);
			this.OpenLoansLabel.Name = "OpenLoansLabel";
			this.OpenLoansLabel.Size = new System.Drawing.Size(88, 12);
			this.OpenLoansLabel.TabIndex = 6;
			this.OpenLoansLabel.Tag = null;
			this.OpenLoansLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.OpenLoansLabel.Value = new System.Decimal(new int[] {
																																 0,
																																 0,
																																 0,
																																 0});
			// 
			// BalanceLoansLabel
			// 
			this.BalanceLoansLabel.CustomFormat = "#,##0";
			this.BalanceLoansLabel.DataType = typeof(long);
			this.BalanceLoansLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.BalanceLoansLabel.Location = new System.Drawing.Point(156, 84);
			this.BalanceLoansLabel.Name = "BalanceLoansLabel";
			this.BalanceLoansLabel.Size = new System.Drawing.Size(88, 12);
			this.BalanceLoansLabel.TabIndex = 18;
			this.BalanceLoansLabel.Tag = null;
			this.BalanceLoansLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BalanceLoansLabel.Value = ((long)(0));
			// 
			// LimitLoansLabel
			// 
			this.LimitLoansLabel.CustomFormat = "#,##0";
			this.LimitLoansLabel.DataType = typeof(long);
			this.LimitLoansLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.LimitLoansLabel.Location = new System.Drawing.Point(156, 64);
			this.LimitLoansLabel.Name = "LimitLoansLabel";
			this.LimitLoansLabel.Size = new System.Drawing.Size(88, 12);
			this.LimitLoansLabel.TabIndex = 14;
			this.LimitLoansLabel.Tag = null;
			this.LimitLoansLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LimitLoansLabel.Value = ((long)(0));
			// 
			// DealLoansLabel
			// 
			this.DealLoansLabel.CustomFormat = "#,##0";
			this.DealLoansLabel.DataType = typeof(System.Decimal);
			this.DealLoansLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.DealLoansLabel.Location = new System.Drawing.Point(156, 48);
			this.DealLoansLabel.Name = "DealLoansLabel";
			this.DealLoansLabel.Size = new System.Drawing.Size(88, 12);
			this.DealLoansLabel.TabIndex = 10;
			this.DealLoansLabel.Tag = null;
			this.DealLoansLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DealLoansLabel.Value = new System.Decimal(new int[] {
																																 0,
																																 0,
																																 0,
																																 0});
			// 
			// OpenBorrowsLabel
			// 
			this.OpenBorrowsLabel.CustomFormat = "#,##0";
			this.OpenBorrowsLabel.DataType = typeof(System.Decimal);
			this.OpenBorrowsLabel.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
			this.OpenBorrowsLabel.Location = new System.Drawing.Point(64, 32);
			this.OpenBorrowsLabel.Name = "OpenBorrowsLabel";
			this.OpenBorrowsLabel.Size = new System.Drawing.Size(88, 12);
			this.OpenBorrowsLabel.TabIndex = 5;
			this.OpenBorrowsLabel.Tag = null;
			this.OpenBorrowsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.OpenBorrowsLabel.Value = new System.Decimal(new int[] {
																																	 0,
																																	 0,
																																	 0,
																																	 0});
			// 
			// TotalLabel
			// 
			this.TotalLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TotalLabel.ForeColor = System.Drawing.Color.DimGray;
			this.TotalLabel.Location = new System.Drawing.Point(248, 16);
			this.TotalLabel.Name = "TotalLabel";
			this.TotalLabel.Size = new System.Drawing.Size(88, 12);
			this.TotalLabel.TabIndex = 3;
			this.TotalLabel.Tag = null;
			this.TotalLabel.Text = "Total";
			this.TotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.TotalLabel.TextDetached = true;
			// 
			// BorrowsLabel
			// 
			this.BorrowsLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BorrowsLabel.ForeColor = System.Drawing.Color.DimGray;
			this.BorrowsLabel.Location = new System.Drawing.Point(64, 16);
			this.BorrowsLabel.Name = "BorrowsLabel";
			this.BorrowsLabel.Size = new System.Drawing.Size(88, 12);
			this.BorrowsLabel.TabIndex = 1;
			this.BorrowsLabel.Tag = null;
			this.BorrowsLabel.Text = "Borrows";
			this.BorrowsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BorrowsLabel.TextDetached = true;
			// 
			// LoansLabel
			// 
			this.LoansLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LoansLabel.ForeColor = System.Drawing.Color.DimGray;
			this.LoansLabel.Location = new System.Drawing.Point(156, 16);
			this.LoansLabel.Name = "LoansLabel";
			this.LoansLabel.Size = new System.Drawing.Size(88, 12);
			this.LoansLabel.TabIndex = 2;
			this.LoansLabel.Tag = null;
			this.LoansLabel.Text = "Loans";
			this.LoansLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LoansLabel.TextDetached = true;
			// 
			// BalanceLabel
			// 
			this.BalanceLabel.ForeColor = System.Drawing.Color.DimGray;
			this.BalanceLabel.Location = new System.Drawing.Point(4, 84);
			this.BalanceLabel.Name = "BalanceLabel";
			this.BalanceLabel.Size = new System.Drawing.Size(56, 12);
			this.BalanceLabel.TabIndex = 16;
			this.BalanceLabel.Tag = null;
			this.BalanceLabel.Text = "Balance:";
			this.BalanceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BalanceLabel.TextDetached = true;
			// 
			// LimitLabel
			// 
			this.LimitLabel.ForeColor = System.Drawing.Color.DimGray;
			this.LimitLabel.Location = new System.Drawing.Point(4, 64);
			this.LimitLabel.Name = "LimitLabel";
			this.LimitLabel.Size = new System.Drawing.Size(56, 12);
			this.LimitLabel.TabIndex = 12;
			this.LimitLabel.Tag = null;
			this.LimitLabel.Text = "Limit:";
			this.LimitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LimitLabel.TextDetached = true;
			// 
			// DealsLabel
			// 
			this.DealsLabel.ForeColor = System.Drawing.Color.DimGray;
			this.DealsLabel.Location = new System.Drawing.Point(4, 48);
			this.DealsLabel.Name = "DealsLabel";
			this.DealsLabel.Size = new System.Drawing.Size(56, 12);
			this.DealsLabel.TabIndex = 8;
			this.DealsLabel.Tag = null;
			this.DealsLabel.Text = "Deals:";
			this.DealsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.DealsLabel.TextDetached = true;
			// 
			// OpenLabel
			// 
			this.OpenLabel.ForeColor = System.Drawing.Color.DimGray;
			this.OpenLabel.Location = new System.Drawing.Point(4, 32);
			this.OpenLabel.Name = "OpenLabel";
			this.OpenLabel.Size = new System.Drawing.Size(56, 12);
			this.OpenLabel.TabIndex = 4;
			this.OpenLabel.Tag = null;
			this.OpenLabel.Text = "Open:";
			this.OpenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.OpenLabel.TextDetached = true;
			// 
			// CreditControl
			// 
			this.Controls.Add(this.BookParentGroupBox);
			this.Name = "CreditControl";
			this.Size = new System.Drawing.Size(344, 104);
			this.BookParentGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.LimitTotalLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BalanceTotalLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DealTotalLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.OpenTotalLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BalanceBorrowsLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LimitBorrowsLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DealBorrowsLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.OpenLoansLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BalanceLoansLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LimitLoansLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DealLoansLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.OpenBorrowsLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TotalLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BorrowsLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LoansLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BalanceLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LimitLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DealsLabel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.OpenLabel)).EndInit();
			this.ResumeLayout(false);

		}


		private void BookParentGroupBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			System.Drawing.Pen pen = new Pen(Color.DarkGray, 1);

			const float y = 80.0F;

			e.Graphics.DrawLine(pen, LimitLoansLabel.Left + 8F, y, LimitLoansLabel.Left + LimitLoansLabel.Width, y);
			e.Graphics.DrawLine(pen, LimitBorrowsLabel.Left + 8F, y, LimitBorrowsLabel.Left + LimitBorrowsLabel.Width, y);
			e.Graphics.DrawLine(pen, LimitTotalLabel.Left + 8F, y, LimitTotalLabel.Left + LimitTotalLabel.Width, y);

			pen.Dispose();
			e.Graphics.Dispose();
		}


		protected override void Dispose( bool disposing )
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

		
		#endregion

		#region Methods

		public CreditControl() 
		{
			InitializeComponent();						
		}


		public void InitializeCreditControl(MainForm mainForm)
		{
			this.mainForm = mainForm;

			try
			{        
				//Deals Delegate Event handler
				dealEventArgsArray = new ArrayList();      
				dealEventWrapper = new DealEventWrapper(); 
				dealEventWrapper.DealEvent += new DealEventHandler(DealOnEvent);       
				dealEventHandler = new DealEventHandler(DealDoEvent);

				//Subscribe to server event
				mainForm.PositionAgent.DealEvent += new DealEventHandler(dealEventWrapper.DoEvent); 

				this.isReady = true;
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message + " [CreditControl.InitializeCreditControl]");
			}
		}


		private bool IsReady
		{
			get
			{
				return isReady;
			}

			set
			{
				try
				{
					if (value && (dealEventArgsArray.Count > 0))
					{          
						isReady = false;

						dealEventHandler.BeginInvoke((DealEventArgs)dealEventArgsArray[0], null, null);            
						dealEventArgsArray.RemoveAt(0);
					}
					else
					{
						isReady = value;
					}
				}
				catch (Exception e)
				{
					Log.Write(e.Message + " [CreditControl.IsReady(set)]", Log.Error, 1); 
				}
			}
		}

		
		private void DealOnEvent(DealEventArgs dealEventArgs)
		{
			int i;
      
			if (dealEventArgs.DealId.StartsWith(DEAL_ID_PREFIX) )
			{
				if ((dealEventArgs.BookGroup == this.bookGroup) && (dealEventArgs.Book == this.book))
				{
					i = dealEventArgsArray.Add(dealEventArgs);
					Log.Write("Deal event queued at " + i + " for deal ID: " + dealEventArgs.DealId + " [CreditControl.DealOnEvent]" , 3);
      
					if (this.IsReady) // Force reset to trigger handling of event.
					{
						this.IsReady = true;
					}
				}
			}
			else
			{
				Log.Write("Deal event being discarded for deal ID: " + dealEventArgs.DealId + " [CreditControl.DealOnEvent]" , 3);
			}
		}

		
		private void DealDoEvent(DealEventArgs dealEventArgs)
		{
			try
			{ 
				Log.Write("Deal event being handled for deal ID: " + dealEventArgs.DealId + " [CreditControl.DealDoEvent]" , 3);

				dataSetDeals.Tables[0].BeginLoadData();

				dealEventArgs.UtcOffset = this.mainForm.UtcOffset;

				dataSetDeals.Tables[0].LoadDataRow(dealEventArgs.Values, true);        
				dataSetDeals.Tables[0].EndLoadData();      
      
				CalculateTotals();

				this.IsReady = true;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [CreditControl.DealDoEvent]", Log.Error, 1);          
			}
		}

		
		public void CalculateTotals()
		{			
			try
			{
				// Populate datasets
				if (dataSetBookLimits == null)
				{
					dataSetBookLimits = mainForm.AdminAgent.BookDataGet(-1);
				}
			
				if ((dataSetContracts == null) || (this.BookChanged || this.BookGroupChanged))
				{
					dataSetContracts =  mainForm.PositionAgent.ContractDataGet(this.bookGroup, this.book, "");				
				} 

				if ((dataSetDeals == null) || (this.bookChanged || this.bookGroupChanged))
				{
					dataSetDeals =  mainForm.PositionAgent.DealDataGet(this.bookGroup, this.book, "");
				}

				//	Contracts 
				decimal contractBorrowTotal = GetContractTotals("B");
				decimal contractLoanTotal = GetContractTotals("L");
				decimal contractTotal = contractBorrowTotal + contractLoanTotal;

				OpenBorrowsLabel.Value = contractBorrowTotal;
				OpenLoansLabel.Value = contractLoanTotal;
				OpenTotalLabel.Value = contractTotal;

				//	Deals
				decimal dealBorrowTotal = GetDealTotals("B");
				decimal dealLoanTotal = GetDealTotals("L");
				decimal dealTotal = dealBorrowTotal + dealLoanTotal;

				DealBorrowsLabel.Value = dealBorrowTotal;
				DealLoansLabel.Value = dealLoanTotal;
				DealTotalLabel.Value = dealTotal;

				//	Limits
				long borrowLimit = 0, loanLimit = 0, limitTotal = 0;
				GetBookLimitTotals(ref borrowLimit, ref loanLimit, ref limitTotal);

				LimitBorrowsLabel.Value = borrowLimit;
				LimitLoansLabel.Value = loanLimit;
				LimitTotalLabel.Value = limitTotal;

				//	Balance
				BalanceBorrowsLabel.Value = borrowLimit - (long)(dealBorrowTotal + contractBorrowTotal);
				BalanceLoansLabel.Value = loanLimit - (long)(dealLoanTotal + contractLoanTotal);
				BalanceTotalLabel.Value = limitTotal - (long)(dealTotal + contractTotal);

				this.BookGroupChanged = false;
				this.BookChanged = false;
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [CreditControl.CalculateTotals]", Log.Error, 1);          
				throw(e);				
			}
		}


		private void GetBookLimitTotals(ref long borrowLimit, ref long loanLimit, ref long limitTotal)
		{
			try
			{
				foreach (DataRow row in dataSetBookLimits.Tables["BookParents"].Rows)
				{
					if ((row["BookGroup"].ToString() == bookGroup) && (row["BookParent"].ToString() == book))
					{
						borrowLimit = (long) row["AmountLimitBorrow"];
						loanLimit = (long) row["AmountLimitLoan"];
						limitTotal = (long) row["AmountLimitTotal"];
						break;
					}
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [CreditControl.GetBookLimitTotals]", Log.Error, 1);          
			}
		}
		
		
		private decimal GetDealTotals(string dealType)
		{
			decimal dealTotals = 0M; 
			DataRow[] foundRows;

			try
			{
				foundRows =	dataSetDeals.Tables[0].Select("DealType = '" + dealType + "'");

				foreach (DataRow row in foundRows)
				{
					dealTotals = dealTotals + (decimal)row["Amount"];
				}

			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [CreditControl.GetDealTotals]", Log.Error, 1);          
			}
			
			return dealTotals;
		}
		

		private decimal GetContractTotals(string contractType)
		{
			decimal contractTotals = 0M; 
			DataRow[] foundRows;

			try
			{
				foundRows =	dataSetContracts.Tables[0].Select("ContractType = '" + contractType + "'");
			
				foreach (DataRow row in foundRows)
				{
					contractTotals = contractTotals + (decimal)row["Amount"];
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [CreditControl.GetContractTotals]", Log.Error, 1);          
			}


			return contractTotals;
		}

						
		public MainForm MainForm
		{
			set
			{
				mainForm = value;
			}
		}
		

		public string BookGroup 
		{
			set
			{
				this.BookGroupChanged = (bookGroup != value) ? true : false;

				bookGroup = value;
			}
		}

		
		public string Book
		{
			set
			{
				this.bookChanged = (book != value) ? true : false;

				book = value;
			}
		}


		private bool BookChanged
		{
			get
			{
				return bookChanged;
			}
			set
			{
				bookChanged = value;
			}
		}


		private bool BookGroupChanged
		{
			get
			{
				return bookGroupChanged;
			}
			set
			{
				bookGroupChanged = value;
			}
		}



		#endregion

	}
}
