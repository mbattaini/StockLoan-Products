using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using C1.C1Pdf;
using C1.Win.C1Ribbon;
using C1.Win.C1Command;
using C1.Win.C1TrueDBGrid;

using StockLoan.Common;
using StockLoan.NorthStar;

namespace NorthStarClient
{
	public partial class ShortInterestReportingForm : C1RibbonForm
	{
		private MainForm mainForm = null;
		private DataSet dsShortInterestPhase3 = null;					//DC new 
		private DataView dvShortInterestPhase3 = null;					//DC new

		private DataSet dsShortInterestBroadRidge = null;				//DC new
		private DataView dvShortInterestBroadRidge = null;				//DC new

		private string secId = "";
		private Point mouseLocation;
		private ContextMenuExtend contextMenuExtendPhase3 = null;		//DC new 
		private ContextMenuExtend contextMenuExtendBroadRidge = null;	//DC new 

		private Dictionary<string, ExcelCellStyle> excelCellStyleDictionary = null;

		public ShortInterestReportingForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
			this.mouseLocation = new Point(-1, -1);
			this.dsShortInterestPhase3 = new DataSet();					//DC new 
			this.dsShortInterestBroadRidge = new DataSet();				//DC new
			

			InitializeExcelCellStyleDictionary();

			InitializeComponent();

			//initialize a contextMenuExtend object.

			contextMenuExtendPhase3 = new ContextMenuExtend(ref this.ShortInterestPhase3Grid, ref this.MainContextMenu);			//DC 
			contextMenuExtendBroadRidge = new ContextMenuExtend(ref this.ShortInterestBroadRidgeGrid, ref this.MainContextMenu);	//DC new

		}


		private void StatusRibbonLabelUpdate()
		{
			try
			{
				if (ShortInterestPhase3TabPage.Visible == true)		//DC new 
				{
					if (dvShortInterestPhase3 != null)
						this.StatusRibbonLabel.Text = "Result: " + dvShortInterestPhase3.Count.ToString("#,##0") + " rows in grid";
				}
				else if (ShortInterestBroadRidgeTabPage.Visible == true)
				{
					if (dvShortInterestBroadRidge != null)
						this.StatusRibbonLabel.Text = "Result: " + dvShortInterestBroadRidge.Count.ToString("#,##0") + " rows in grid";
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortInterestReportingForm.StatusRibbonLabelUpdate]", 1);
			}
		}

		private void FindButton_Click(object sender, EventArgs e)
		{
			if (DataValidation() == false)
				return;

			this.Cursor = Cursors.WaitCursor;

			try
			{
				if (ShortInterestPhase3TabPage.Visible == true)		//DC new 
				{
					dsShortInterestPhase3.Clear();
					dsShortInterestPhase3 = mainForm.ShortInterestAgent.ShortInterestGet(
											this.MpidTextBox.Text.Trim(),
											this.CusipTextBox.Text.Trim(),
											this.QuantityGreaterThanTextBox.Text.Trim(),
											this.QuantityLessThanTextBox.Text.Trim(),
											this.PriceLessThanTextBox.Text.Trim(),
											this.ShortInterestMidMonthGreaterThanTextBox.Text.Trim(),
											this.ShortInterestMonthEndGreaterThanTextBox.Text.Trim(),
											this.AccountNumberTextBox.Text.Trim(),	//DC  
											true,									//DC new	showPhase3		(SqlDbType.Bit)
											false									//DC new	showBroadRidge	(SqlDbType.Bit)
										 );
					dvShortInterestPhase3 = new DataView(dsShortInterestPhase3.Tables["ShortInterest"], "", "Symbol", DataViewRowState.CurrentRows);
					this.ShortInterestPhase3Grid.SetDataBinding(dvShortInterestPhase3, "", true);
				}
				else if (ShortInterestBroadRidgeTabPage.Visible == true)
				{
					dsShortInterestBroadRidge.Clear();
					dsShortInterestBroadRidge = mainForm.ShortInterestAgent.ShortInterestGet(
											"",										//DC new	this.MpidTextBox.Text.Trim(),
											this.CusipTextBox.Text.Trim(),
											this.QuantityGreaterThanTextBox.Text.Trim(),
											this.QuantityLessThanTextBox.Text.Trim(),
											this.PriceLessThanTextBox.Text.Trim(),
											this.ShortInterestMidMonthGreaterThanTextBox.Text.Trim(),
											this.ShortInterestMonthEndGreaterThanTextBox.Text.Trim(),
											this.AccountNumberTextBox.Text.Trim(),	//DC  
											false,									//DC new	showPhase3		(SqlDbType.Bit)
											true									//DC new	showBroadRidge	(SqlDbType.Bit)
										 );
					dvShortInterestBroadRidge = new DataView(dsShortInterestBroadRidge.Tables["ShortInterest"], "", "Symbol", DataViewRowState.CurrentRows);
					this.ShortInterestBroadRidgeGrid.SetDataBinding(dvShortInterestBroadRidge, "", true);
				}
				StatusRibbonLabelUpdate();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortInterestReportingForm.buttonFind_Click]", 1);
			}

			this.Cursor = Cursors.Default;
		}
		
		private void ShortInterestGrid_FetchCellTips(object sender, C1.Win.C1TrueDBGrid.FetchCellTipsEventArgs e)
		{
			string toolTipString = "";

			if (e.Row == -2)		//Header Row 
			{
				switch (e.Column.DataColumn.DataField)
				{
					case "AccountNumber":
						toolTipString = "Account Number";
						break;
					case "AccountName":
						toolTipString = "Account Name";
						break;
					case "CUSIP":
						toolTipString = "Security ID";
						break;
					case "Symbol":
						toolTipString = "Symbol";
						break;
					case "TradeDateSharesShort":
						toolTipString = "Total Shares Short On Trade Date";
						break;
					case "SettleDateSharesShort":
						toolTipString = "Total Shares Short On Settlement Date";
						break;
					case "TotalEquity":
						toolTipString = "Total Equity";
						break;
					case "TwentyDaysAvgVolume":
						toolTipString = "Twenty Days Average Moving Volume";
						break;
					case "DaysToLiquidity":
						toolTipString = "Days To Cover";
						break;
					case "PensonPercentOfFloat":
						toolTipString = "Penson Percent Of Float";
						break;
					case "TotalShortInterestMidMonth_MIL":
						toolTipString = "Total Short Interest at Middle Of Month";
						break;
					case "TotalShortInterestMidMonthDate":
						toolTipString = "Date of Total Short Interest at Middle Of Month";
						break;
					case "TotalShortInterestMonthEnd_MIL":
						toolTipString = "Total Short Interest at End Of Month";
						break;
					case "TotalShortInterestMonthEndDate":
						toolTipString = "Date of Total Short Interest at End Of Month";
						break;
					case "ExchangeDescription":
						toolTipString = "Exchange";
						break;
					case "CurrentPrice":
						toolTipString = "Current Price";
						break;
					case "ISIN":
						toolTipString = "International Security Identification Number";
						break;
					case "CurrencyCode":
						toolTipString = "Currency Code";
						break;
					case "SharesFloat":
						toolTipString = "Total Shares Float";
						break;
					case "TwentyDaysMovingAvgPrice":
						toolTipString = "Twenty Days Average Moving Price";
						break;
					case "FiveDaysMovingAvgPrice":
						toolTipString = "Five Days Average Moving Price";
						break;
					case "FiveDaysAvgVolume":
						toolTipString = "Five Days Average Moving Volume";
						break;
					default:
						toolTipString = "";
						break;
				}
				e.CellTip = toolTipString;
			}
			else
			{
				e.CellTip = "";
			}
		}

		private void ShortInterestGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
			switch (e.Column.DataField)
			{
				case "SettleDateSharesShort":
				case "TradeDateSharesShort":
				case "SharesFloat":
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch { }
					break;
				case "CurrentPrice":
				case "TotalEquity":
				case "TotalShortInterestMidMonth_MIL":
				case "TotalShortInterestMonthEnd_MIL":
				case "TwentyDaysAvgVolume":
				case "FiveDaysAvgVolume":
				case "TwentyDaysMovingAvgPrice":
				case "FiveDaysMovingAvgPrice":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.000");
					}
					catch { }
					break;
				case "DaysToLiquidity":
				case "PensonPercentOfFloat":
					try
					{
						e.Value = decimal.Parse(e.Value.ToString()).ToString("#,##0.00000000");
					}
					catch { }
					break;

				case "TotalShortInterestMidMonthDate":
				case "TotalShortInterestMonthEndDate":
					try
					{
						e.Value = Tools.FormatDate(e.Value.ToString(), Standard.DateFormat);
					}
					catch { }
					break;
			}
		}

		private void ShortInterestReportingForm_Load(object sender, EventArgs e)
		{
			try
			{
				// Load Form previouslt recoreded form size and location
				this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "0"));
				this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "0"));
				this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "570"));
				this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "1530"));


				// Load Phase3 grid's column show/hide information
				foreach (C1DisplayColumn column in ShortInterestPhase3Grid.Splits[0].DisplayColumns)
				{
					column.Visible = int.Parse(RegistryValue.Read(this.Name, ShortInterestPhase3Grid.Name + "." + column.Name, "1")) == 1 ? true : false;
				}
				contextMenuExtendPhase3.AppendContextMenu();				//DC new 


				// Load Load BroadRidge Grid's column show.hide information //DC new 
				foreach (C1DisplayColumn column in ShortInterestBroadRidgeGrid.Splits[0].DisplayColumns)
				{
					column.Visible = int.Parse(RegistryValue.Read(this.Name, ShortInterestBroadRidgeGrid.Name + "." + column.Name, "1")) == 1 ? true : false;
				}
				contextMenuExtendBroadRidge.AppendContextMenu();			//DC new  

				
				if (mainForm.ShortInterestAgent == null)
				{
					this.ShortInterestDataStatusLabel.Text = "NorthStar server is not available.";
				}
				else
				{
					string dataDate = mainForm.ShortInterestAgent.ShortInterestDataDateGet();

					if (dataDate.Equals(""))
					{
						this.ShortInterestDataStatusLabel.Text = "XFL data is not available.";
					}
					else
					{
						this.ShortInterestDataStatusLabel.Text = "XFL Data is current for " + dataDate;
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[ShortInterestReportingForm.ShortInterestReportForm_Load]", 1);
				this.ShortInterestDataStatusLabel.Text = "NorthStar server is not available.";
			}
		}

		private void ShortInterestReportingForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (this.WindowState.Equals(FormWindowState.Normal))
			{
				RegistryValue.Write(this.Name, "Top", this.Top.ToString());
				RegistryValue.Write(this.Name, "Left", this.Left.ToString());
				RegistryValue.Write(this.Name, "Height", this.Height.ToString());
				RegistryValue.Write(this.Name, "Width", this.Width.ToString());
			}

			try
			{
				// Save grid column show/hid information

				foreach (C1DisplayColumn column in this.ShortInterestPhase3Grid.Splits[0].DisplayColumns)
				{
					RegistryValue.Write(this.Name, this.ShortInterestPhase3Grid.Name + "." + column.Name, column.Visible ? "1" : "0");
				}

				//DC new 
				foreach (C1DisplayColumn column in this.ShortInterestBroadRidgeGrid.Splits[0].DisplayColumns)	//DC new 
				{
					RegistryValue.Write(this.Name, this.ShortInterestBroadRidgeGrid.Name + "." + column.Name, column.Visible ? "1" : "0");
				}

			}
			catch (Exception ex)
			{
				Log.Write(ex.Message + " [ShortInterestReportingForm.ShortInterestReportingForm_FormClosed]", Log.Error, 1);
			}

			mainForm.shortInterestReportForm = null;
		}

		private bool DataValidation()
		{
			decimal value = 0.0M;

			try
			{
				/*** //DC Kimberly request to allow ShortInterest query without any searching criteria, i.e. get the whole resultset. 
				if ( ShortInterestMonthEndGreaterThanTextBox.Text.Trim().Equals("") &&
					 ShortInterestMidMonthGreaterThanTextBox.Text.Trim().Equals("") &&
					 QuantityGreaterThanTextBox.Text.Trim().Equals("") &&
					 PriceLessThanTextBox.Text.Trim().Equals("") &&
					 CusipTextBox.Text.Trim().Equals("") &&				//DC
					 MpidTextBox.Text.Trim().Equals("") &&				//DC
					 AccountNumberTextBox.Text.Trim().Equals("")		//DC 
					)
				{
					MessageBox.Show("Please enter at least one searching criterion", "Error");
					this.AccountNumberTextBox.Focus(); 
					return false;
				}
				***/

				if (!QuantityGreaterThanTextBox.Text.Trim().Equals(""))
				{
					if (decimal.TryParse(QuantityGreaterThanTextBox.Text, out value) == false)
					{
						MessageBox.Show("value is incorrect", "Error");
						this.QuantityGreaterThanTextBox.Focus();
						return false;
					}
					else if (value < 0)
					{
						MessageBox.Show("value can not be negative", "Error");
						this.QuantityGreaterThanTextBox.Focus();
						return false;
					}
				}

				if (!QuantityLessThanTextBox.Text.Trim().Equals(""))
				{
					if (decimal.TryParse(QuantityLessThanTextBox.Text, out value) == false)
					{
						MessageBox.Show("value is incorrect", "Error");
						this.QuantityLessThanTextBox.Focus();
						return false;
					}
					else if (value <= 0)
					{
						MessageBox.Show("value must be greater than zero", "Error");
						this.QuantityGreaterThanTextBox.Focus();
						return false;
					}
				}

				if (!this.PriceLessThanTextBox.Text.Trim().Equals(""))
				{
					if (decimal.TryParse(this.PriceLessThanTextBox.Text, out value) == false)
					{
						MessageBox.Show("value is incorrect", "Error");
						this.PriceLessThanTextBox.Focus();
						return false;
					}
					else if (value <= 0)
					{
						MessageBox.Show("value must be greater than zero", "Error");
						this.PriceLessThanTextBox.Focus();
						return false;
					}
				}

				if (!this.ShortInterestMidMonthGreaterThanTextBox.Text.Trim().Equals(""))
				{
					if (decimal.TryParse(this.ShortInterestMidMonthGreaterThanTextBox.Text, out value) == false)
					{
						MessageBox.Show("value is incorrect", "Error");
						this.ShortInterestMidMonthGreaterThanTextBox.Focus();
						return false;
					}
					else if (value < 0)
					{
						MessageBox.Show("value can not be negative", "Error");
						this.ShortInterestMidMonthGreaterThanTextBox.Focus();
						return false;
					}
				}

				if (!this.ShortInterestMonthEndGreaterThanTextBox.Text.Trim().Equals(""))
				{
					if (decimal.TryParse(this.ShortInterestMonthEndGreaterThanTextBox.Text, out value) == false)
					{
						MessageBox.Show("value is incorrect", "Error");
						this.ShortInterestMonthEndGreaterThanTextBox.Focus();
						return false;
					}
					else if (value < 0)
					{
						MessageBox.Show("value can not be negative", "Error");
						this.ShortInterestMonthEndGreaterThanTextBox.Focus();
						return false;
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortInterestReportingForm.Datavalidation]", 1);
				return false;
			}

			return true;
		}

		private void InitializeExcelCellStyleDictionary()
		{
			try
			{
				excelCellStyleDictionary = new Dictionary<string, ExcelCellStyle>();

				ExcelCellStyle style = new ExcelCellStyle();
				style.DataField = "TotalEquity";
				style.DataType = typeof(decimal);
				style.StringFormat = "#,##0.000";
				excelCellStyleDictionary.Add(style.DataField, style);

				style = new ExcelCellStyle();
				style.DataField = "CurrentPrice";
				style.DataType = typeof(decimal);
				style.StringFormat = "#,##0.000";
				excelCellStyleDictionary.Add(style.DataField, style);

				style = new ExcelCellStyle();
				style.DataField = "TotalShortInterestMidMonth_MIL";
				style.DataType = typeof(decimal);
				style.StringFormat = "#,##0.000";
				excelCellStyleDictionary.Add(style.DataField, style);

				style = new ExcelCellStyle();
				style.DataField = "TotalShortInterestMonthEnd_MIL";
				style.DataType = typeof(decimal);
				style.StringFormat = "#,##0.000";
				excelCellStyleDictionary.Add(style.DataField, style);

				style = new ExcelCellStyle();
				style.DataField = "TwentyDaysAvgVolume";
				style.DataType = typeof(decimal);
				style.StringFormat = "#,##0.000";
				excelCellStyleDictionary.Add(style.DataField, style);

				style = new ExcelCellStyle();
				style.DataField = "DaysToLiquidity";
				style.DataType = typeof(decimal);
				style.StringFormat = "#,##0.00000000";
				excelCellStyleDictionary.Add(style.DataField, style);

				style = new ExcelCellStyle();
				style.DataField = "PensonPercentOfFloat";
				style.DataType = typeof(decimal);
				style.StringFormat = "#,##0.00000000";
				excelCellStyleDictionary.Add(style.DataField, style);

				style = new ExcelCellStyle();
				style.DataField = "SettleDateSharesShort";
				style.DataType = typeof(int);
				style.StringFormat = "#,##0";
				excelCellStyleDictionary.Add(style.DataField, style);

				style = new ExcelCellStyle();
				style.DataField = "TradeDateSharesShort";
				style.DataType = typeof(int);
				style.StringFormat = "#,##0";
				excelCellStyleDictionary.Add(style.DataField, style);

				style = new ExcelCellStyle();
				style.DataField = "SharesFloat";
				style.DataType = typeof(int);
				style.StringFormat = "#,##0";
				excelCellStyleDictionary.Add(style.DataField, style);

				style = new ExcelCellStyle();
				style.DataField = "TwentyDaysMovingAvgPrice";
				style.DataType = typeof(decimal);
				style.StringFormat = "#,##0.000";
				excelCellStyleDictionary.Add(style.DataField, style);

				style = new ExcelCellStyle();
				style.DataField = "FiveDaysMovingAvgPrice";
				style.DataType = typeof(decimal);
				style.StringFormat = "#,##0.000";
				excelCellStyleDictionary.Add(style.DataField, style);

				style = new ExcelCellStyle();
				style.DataField = "FiveDaysAvgVolume";
				style.DataType = typeof(decimal);
				style.StringFormat = "#,##0.000";
				excelCellStyleDictionary.Add(style.DataField, style);

			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[ShortInterestReportingForm.InitializeExcelCellStyleDictionary]", 1);
			}
		}

		private void ShortInterestPhase3Grid_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.Home)
			{
				if (this.ShortInterestPhase3Grid.Splits[0].Rows.Count > 0)
				{
					ShortInterestPhase3Grid.SetActiveCell(0, 0);
				}
			}
			else if (e.Control && e.KeyCode == Keys.End)
			{
				if (this.ShortInterestPhase3Grid.Splits[0].Rows.Count > 0)
				{
					ShortInterestPhase3Grid.SetActiveCell(ShortInterestPhase3Grid.Splits[0].Rows.Count - 1, 0);
				}
			}
		}

		private void ShortInterestBroadRidgeGrid_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.Home)
			{
				if (this.ShortInterestBroadRidgeGrid.Splits[0].Rows.Count > 0)
				{
					ShortInterestBroadRidgeGrid.SetActiveCell(0, 0);
				}
			}
			else if (e.Control && e.KeyCode == Keys.End)
			{
				if (this.ShortInterestBroadRidgeGrid.Splits[0].Rows.Count > 0)
				{
					ShortInterestBroadRidgeGrid.SetActiveCell(ShortInterestBroadRidgeGrid.Splits[0].Rows.Count - 1, 0);
				}
			}
		}	//DC new 

		private void ShortInterestPhase3Grid_Paint(object sender, PaintEventArgs e)
		{
			if (!secId.Equals(ShortInterestPhase3Grid.Columns["CUSIP"].Text))
			{
				secId = ShortInterestPhase3Grid.Columns["CUSIP"].Text;
				mainForm.SecId = secId;
			}

			StatusRibbonLabelUpdate();
		}

		private void ShortInterestBroadRidgeGrid_Paint(object sender, PaintEventArgs e)
		{
			if (!secId.Equals(ShortInterestBroadRidgeGrid.Columns["CUSIP"].Text))
			{
				secId = ShortInterestBroadRidgeGrid.Columns["CUSIP"].Text;
				mainForm.SecId = secId;
			}

			StatusRibbonLabelUpdate();
		} //DC new
		
		private void ShortInterestGrid_MouseLeave(object sender, EventArgs e)
		{
			this.mouseLocation.X = -1;
			this.mouseLocation.Y = -1;
		}

		private void ShortInterestGrid_MouseMove(object sender, MouseEventArgs e)
		{
			this.mouseLocation.X = e.X;
			this.mouseLocation.Y = e.Y;
		}

		private void ShortInterestPhase3TabPage_VisibleChanged(object sender, EventArgs e)
		{	//DC new 
			if (ShortInterestPhase3TabPage.Visible == true)
			{
				this.MpidLabel.Visible = true;
				this.MpidTextBox.Text = "";
				this.MpidTextBox.Enabled = true;
				this.MpidTextBox.Visible = true;
				if (dsShortInterestBroadRidge != null) 		//DC new clear the non-visible grid's dataset 
				{
					dsShortInterestBroadRidge.Clear();
				}

			}
			this.AccountNumberTextBox.Text = "";
			this.CusipTextBox.Text = "";
			this.PriceLessThanTextBox.Text = "";
			this.QuantityGreaterThanTextBox.Text = "";
			this.QuantityLessThanTextBox.Text = "";
			this.ShortInterestMidMonthGreaterThanTextBox.Text = "";
			this.ShortInterestMonthEndGreaterThanTextBox.Text = "";
		}

		private void ShortInterestBroadRidgeTabPage_VisibleChanged(object sender, EventArgs e)
		{	//DC new
			if (ShortInterestBroadRidgeTabPage.Visible == true)
			{
				this.MpidLabel.Visible = false;
				this.MpidTextBox.Text = "";
				this.MpidTextBox.Enabled = false;
				this.MpidTextBox.Visible = false;
				if (dsShortInterestPhase3 != null) 		//DC new clear the non-visible grid's dataset 
				{	
					dsShortInterestPhase3.Clear();
				}
			}
			this.AccountNumberTextBox.Text = "";
			this.CusipTextBox.Text = "";
			this.PriceLessThanTextBox.Text = "";
			this.QuantityGreaterThanTextBox.Text = "";
			this.QuantityLessThanTextBox.Text = "";
			this.ShortInterestMidMonthGreaterThanTextBox.Text = "";
			this.ShortInterestMonthEndGreaterThanTextBox.Text = "";
		}

		private void MainContextMenu_Popup(object sender, EventArgs e)
		{	//DC new 
			if (ShortInterestPhase3TabPage.Visible == true)
			{
				contextMenuExtendPhase3.EnableDisableContextMenu(mouseLocation);
			}
			else if (ShortInterestBroadRidgeTabPage.Visible == true)
			{
				contextMenuExtendBroadRidge.EnableDisableContextMenu(mouseLocation);
			}
		}

		private void ClearResultsMenuItem_Click(object sender, EventArgs e)
		{
			if (ShortInterestPhase3TabPage.Visible == true)
			{
				this.dsShortInterestPhase3.Clear();
				this.StatusRibbonLabel.Text = "";
			}
			else if (ShortInterestBroadRidgeTabPage.Visible == true)
			{
				this.dsShortInterestBroadRidge.Clear();
				this.StatusRibbonLabel.Text = "";
			}
		}

		private string GetSelectedContent(ref C1TrueDBGrid grid)
		{
			return ""; 

			//DC:  Lwu original: columns and column-width does not align well... 
			/*****
			int textLength;
			int[] maxTextLength;

			int columnIndex = -1;
			StringBuilder builder = new StringBuilder();
			bool isWholeRowSelected = false;
			bool visibleColumn = false;
			int numberOfVisibleColumn = 0;

			try
			{
				if (grid.SelectedCols.Count == 0 && grid.SelectedRows.Count > 0)
				{
					isWholeRowSelected = true;

					for (int columns = 0; columns < grid.Splits[0].DisplayColumns.Count; columns++)
					{
						if (grid.Splits[0].DisplayColumns[columns].Visible)
							numberOfVisibleColumn++;
					}

					maxTextLength = new int[numberOfVisibleColumn];

					foreach (C1DataColumn dataColumn in grid.Columns)
					{
						visibleColumn = false;

						for (int columns = 0; columns < grid.Splits[0].DisplayColumns.Count; columns++)
						{
							if (grid.Splits[0].DisplayColumns[columns].DataColumn == dataColumn)
							{
								if (grid.Splits[0].DisplayColumns[columns].Visible == true)
								{
									visibleColumn = true;
								}
								continue;
							}
						}

						if (visibleColumn)
							maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
					}
				}
				else
				{
					numberOfVisibleColumn = 0;
					foreach (C1DataColumn dataColumn in grid.SelectedCols)
					{
						for (int columns = 0; columns < grid.Splits[0].DisplayColumns.Count; columns++)
						{
							if (grid.Splits[0].DisplayColumns[columns].DataColumn == dataColumn)
							{
								if (grid.Splits[0].DisplayColumns[columns].Visible == true)
								{
									numberOfVisibleColumn++;
								}
								continue;
							}
						}
					}

					maxTextLength = new int[numberOfVisibleColumn];

					foreach (C1DataColumn dataColumn in grid.SelectedCols)
					{

						for (int columns = 0; columns < grid.Splits[0].DisplayColumns.Count; columns++)
						{
							if (grid.Splits[0].DisplayColumns[columns].DataColumn == dataColumn)
							{
								if (grid.Splits[0].DisplayColumns[columns].Visible == true)
								{
									maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
								}
								continue;
							}
						}
					}
				}

				foreach (int rowIndex in grid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1DataColumn dataColumn in isWholeRowSelected ? grid.Columns : grid.SelectedCols)
					{

						visibleColumn = false;

						for (int columns = 0; columns < grid.Splits[0].DisplayColumns.Count; columns++)
						{
							if (grid.Splits[0].DisplayColumns[columns].DataColumn == dataColumn)
							{
								if (grid.Splits[0].DisplayColumns[columns].Visible == true)
								{
									visibleColumn = true;
								}
							}
						}

						if (visibleColumn)
						{
							if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
							{
								maxTextLength[columnIndex] = textLength;
							}
						}
					}
				}

				columnIndex = -1;

				foreach (C1DataColumn dataColumn in isWholeRowSelected ? grid.Columns : grid.SelectedCols)
				{
					visibleColumn = false;

					for (int columns = 0; columns < grid.Splits[0].DisplayColumns.Count; columns++)
					{
						if (grid.Splits[0].DisplayColumns[columns].DataColumn == dataColumn)
						{
							if (grid.Splits[0].DisplayColumns[columns].Visible == true)
							{
								visibleColumn = true;
							}
						}
					}

					if (visibleColumn)
						builder.Append(dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' '));
				}

				builder.AppendLine();

				columnIndex = -1;

				for (int i = 0; i < maxTextLength.Length; i++)
				{
					builder.Append(new string('-', maxTextLength[++columnIndex]) + "	");
				}
				builder.AppendLine();

				foreach (int rowIndex in grid.SelectedRows)
				{
					columnIndex = -1;

					foreach (C1DataColumn dataColumn in isWholeRowSelected ? grid.Columns : grid.SelectedCols)
					{
						visibleColumn = false;

						for (int columns = 0; columns < grid.Splits[0].DisplayColumns.Count; columns++)
						{
							if (grid.Splits[0].DisplayColumns[columns].DataColumn == dataColumn)
							{
								if (grid.Splits[0].DisplayColumns[columns].Visible == true)
								{
									visibleColumn = true;
								}
							}
						}

						if (visibleColumn)
						{
							if (dataColumn.Value.GetType().Equals(typeof(string)))
							{
								builder.Append(dataColumn.CellText(rowIndex).Trim().PadRight(maxTextLength[++columnIndex] + 2));
							}
							else
							{
								builder.Append(dataColumn.CellText(rowIndex).Trim().PadLeft(maxTextLength[++columnIndex]) + "	");
							}
						}
					}

					builder.AppendLine();
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ShortInterestReportingForm.GetSelectedContent]", 1);
				builder = new StringBuilder();
			}

			return builder.ToString();
		*****/ 
		}

		
//------------------------------------------------------------- 

		private void SendToClipboardCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{
				if (ShortInterestPhase3Grid.Focused)
				{
					//DC: Lwu Original: Clipboard.SetDataObject(GetSelectedContent(ref ShortInterestPhase3Grid)); 
					mainForm.SendToClipboard(ref ShortInterestPhase3Grid);  
				}
				else if (ShortInterestBroadRidgeGrid.Focused)
				{
					//DC new 
					mainForm.SendToClipboard(ref ShortInterestBroadRidgeGrid);
				}

			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [ShortInterestReportForm.SendToClipboardCommand_Click]", 1);
			}
		}

		private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{
				if (ShortInterestPhase3Grid.Focused)
				{
					mainForm.SendToExcel(ref ShortInterestPhase3Grid, 0, excelCellStyleDictionary);
				}
				else if (ShortInterestBroadRidgeGrid.Focused)	//DC new 
				{
					mainForm.SendToExcel(ref ShortInterestBroadRidgeGrid, 0, excelCellStyleDictionary);
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [ShortInterestReportForm.SendToExcelCommand_Click]", 1);
			}
		}

		private void SendToEmailCommand_Click(object sender, ClickEventArgs e)
		{
			try
			{
				if (ShortInterestPhase3Grid.Focused)
				{
					mainForm.SendToEmail(ref ShortInterestPhase3Grid);
				}
				else if (ShortInterestBroadRidgeGrid.Focused)
				{
					mainForm.SendToEmail(ref ShortInterestBroadRidgeGrid);
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [ShortInterestReportForm.SendToEmailCommand_Click]", 1);
			}
		}


	}
}