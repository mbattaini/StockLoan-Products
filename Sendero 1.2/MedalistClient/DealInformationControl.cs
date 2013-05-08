// Licensed Materials - Property of Penson Financial Services
// Copyright (C) Penson Financial Services 2007  All rights reserved.
//
// Added by Yasir Bashir
// On 7/24/2007

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Anetics.Common;
using C1.Win.C1FlexGrid;

namespace Anetics.Medalist
{
	public class DealInformationControl : System.Windows.Forms.UserControl
	{
		#region Declarations
		private MainForm mainForm;
		private DataSet dataSet;
		private System.ComponentModel.Container components = null;
		private C1.Win.C1FlexGrid.C1FlexGrid DealInformationGrid;
		private const decimal E = 0.000000000000000001M;
		#endregion

		public DealInformationControl()
		{
			try
			{
				InitializeComponent();				
			}
			catch (Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.DealInformationGrid = new C1.Win.C1FlexGrid.C1FlexGrid();
			((System.ComponentModel.ISupportInitialize)(this.DealInformationGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// DealInformationGrid
			// 
			this.DealInformationGrid.ColumnInfo = "8,1,0,0,0,90,Columns:";
			this.DealInformationGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DealInformationGrid.Location = new System.Drawing.Point(0, 0);
			this.DealInformationGrid.Name = "DealInformationGrid";
			this.DealInformationGrid.Size = new System.Drawing.Size(476, 160);
			this.DealInformationGrid.Styles = new C1.Win.C1FlexGrid.CellStyleCollection(@"Normal{Font:Verdana, 8.25pt;}	Fixed{BackColor:Control;ForeColor:ControlText;Border:Flat,1,ControlDark,Both;}	Highlight{BackColor:Highlight;ForeColor:HighlightText;}	Search{BackColor:Highlight;ForeColor:HighlightText;}	Frozen{BackColor:Beige;}	EmptyArea{BackColor:AppWorkspace;Border:Flat,1,ControlDarkDark,Both;}	GrandTotal{BackColor:Blue;ForeColor:Blue;}");
			this.DealInformationGrid.TabIndex = 0;
			this.DealInformationGrid.AfterDataRefresh += new System.ComponentModel.ListChangedEventHandler(this.DealInformationGrid_AfterDataRefresh);
			// 
			// DealInformationControl
			// 
			this.Controls.Add(this.DealInformationGrid);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "DealInformationControl";
			this.Size = new System.Drawing.Size(476, 160);
			((System.ComponentModel.ISupportInitialize)(this.DealInformationGrid)).EndInit();
			this.ResumeLayout(false);
		}
		#endregion

		private void DealInformationGrid_AfterDataRefresh(object sender, System.ComponentModel.ListChangedEventArgs e)
		{
			try
			{
				// Setup Grid layout/behavoir
				DealInformationGrid.AllowSorting = AllowSortingEnum.None;
				DealInformationGrid.AllowMerging = AllowMergingEnum.None;
				DealInformationGrid.SelectionMode = SelectionModeEnum.Default;
				DealInformationGrid.AllowEditing = false;
				DealInformationGrid.ExtendLastCol = true;

				DealInformationGrid.Cols["SecId"].Caption = "Security ID";
				DealInformationGrid.Cols["Symbol"].Caption = "Symbol";
				DealInformationGrid.Cols["DealType"].Caption = "Deal Type";
				DealInformationGrid.Cols["Quantity"].Caption = "Quantity";
				DealInformationGrid.Cols["Rate"].Caption = "Rate";
				DealInformationGrid.Cols["Amount"].Caption = "Amount";
				
				DealInformationGrid.Cols["Rate"].Format = "#0.000";
				DealInformationGrid.Cols["Quantity"].Format = "#,##0";
				DealInformationGrid.Cols["Amount"].Format = "#,##0.00";

				DealInformationGrid.Cols["SecId"].AllowDragging = false;
				DealInformationGrid.Cols["Symbol"].AllowDragging = false;
				DealInformationGrid.Cols["DealType"].AllowDragging = false;
				DealInformationGrid.Cols["Quantity"].AllowDragging = false;
				DealInformationGrid.Cols["Rate"].AllowDragging = false;
				DealInformationGrid.Cols["Amount"].AllowDragging = false;

				DealInformationGrid.Cols[0].Width = DealInformationGrid.Cols.DefaultSize / 4;

				DealInformationGrid.Tree.Style = TreeStyleFlags.Simple;
				DealInformationGrid.Tree.Column = 1;
				DealInformationGrid.AllowMerging = AllowMergingEnum.None;

				DealInformationGrid.Subtotal(AggregateEnum.Average, 1, 1, 5, "Net Totals");
				DealInformationGrid.AutoSizeCols(1, 1, 1000, 3, 30, AutoSizeFlags.IgnoreHidden);
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}

		public void LoadData(string BookGroup, string SecId)
		{
			try
			{
				decimal amount = 0M;
				decimal rate = 0M;
				decimal sumfrequency = 0M;
				long	quantity = 0;
				int		count = 1;
				int		sumcount = 0;
				bool	loopstatus = true;

				if (!SecId.Equals(""))					
				{
					dataSet = mainForm.PositionAgent.DealsDetailDataGet(BookGroup, SecId , mainForm.UtcOffset);
					DealInformationGrid.DataSource = dataSet.Tables["Deals"];
					DealInformationGrid.Cols["DealType"].Width = 70;
					
					foreach (DataRow dr in dataSet.Tables["Deals"].Rows)
					{
						if (dr["DealType"].ToString() == "L")
						{
							quantity -= long.Parse(dr["Quantity"].ToString());
							amount-= decimal.Parse(dr["Amount"].ToString());
						}
						else if (dr["DealType"].ToString() == "B")
						{
							quantity += long.Parse(dr["Quantity"].ToString());
							amount+= decimal.Parse(dr["Amount"].ToString());
						}
					}

					foreach (DataRow dr in dataSet.Tables["Deals"].Select("", "Rate Desc"))
					{
						if (dr["Rate"].ToString() != "")
						{
							if (decimal.Parse(dr["Rate"].ToString()) != rate)
							{
								if (loopstatus == true)
								{
									rate = decimal.Parse(dr["Rate"].ToString());
									loopstatus = false;
								}
								
								rate = decimal.Parse(dr["Rate"].ToString());
								count = 1;
								sumcount = sumcount + count;
								sumfrequency = sumfrequency + (rate * count);
							}
							else if (decimal.Parse(dr["Rate"].ToString()) == rate)
							{
								sumcount = sumcount + count;
								sumfrequency = sumfrequency + (rate * count);
								count = count + 1;
							}
						}
					}

					// create style 
					CellStyle s1 = DealInformationGrid.Styles["Blue"];
					if (s1 == null)
					{
						s1 = DealInformationGrid.Styles.Add("Blue");
						s1.BackColor = Color.Blue;
					}

					// create style 
					CellStyle s2 = DealInformationGrid.Styles["Red"];
					if (s2 == null)
					{
						s2 = DealInformationGrid.Styles.Add("Red");
						s2.BackColor = Color.Red;
					}

					if (quantity <= 0)
					{
						CellRange rg = DealInformationGrid.GetCellRange(1, 1, 1, 6);
						rg.Style = (true)? s1: null;
					}
					else if (quantity > 0)
					{
						CellRange rg = DealInformationGrid.GetCellRange(1, 1, 1, 6);
						rg.Style = (true) ? s2: null;
					}

					DealInformationGrid[1, "Quantity"] = quantity.ToString();
					DealInformationGrid[1, "Amount"] = amount.ToString();
					DealInformationGrid[1, "Rate"] = (sumfrequency/sumcount).ToString();

					
				}
				else
				{
					DealInformationGrid.Clear();
				}
			}
			catch(Exception error)
			{
				mainForm.Alert(error.Message, PilotState.RunFault);
			}
		}
	
		public MainForm MainForm
		{
			set
			{
				mainForm = value;
			}
		}
	}
}
