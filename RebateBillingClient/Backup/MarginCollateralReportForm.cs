using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using C1.C1Pdf;
using C1.Win.C1Ribbon;
using C1.Win.C1TrueDBGrid;

using StockLoan.Common;
using StockLoan.NorthStar;

namespace NorthStarClient
{
	public partial class MarginCollateralReportForm : C1RibbonForm
	{
		private MainForm mainForm = null;		
		private DataSet dsMarginCollateralPhase3 = null;				//DC new
		private DataView dvMarginCollateralPhase3 = null;				//DC new

		private DataSet dsMarginCollateralBroadRidge = null;			//DC new
		private DataView dvMarginCollateralBroadRidge = null;			//DC new

		//private DataSet dsExchange = null;		//DC  2009-12-04 

		private string secId = "";
		private Point mouseLocation;
		private ContextMenuExtend contextMenuExtendPhase3 = null;		//DC new
		private ContextMenuExtend contextMenuExtendBroadRidge = null;	//DC new 

		private Dictionary<string, ExcelCellStyle> excelCellStyleDictionary = null;
		
		public MarginCollateralReportForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
			this.mouseLocation = new Point(-1, -1);
			this.dsMarginCollateralPhase3 = new DataSet();			//DC new 
			this.dsMarginCollateralBroadRidge = new DataSet();		//DC new 


			InitializeExcelCellStyleDictionary();

			InitializeComponent();

			//Initialize contextMenuExtend object

			contextMenuExtendPhase3 = new ContextMenuExtend(ref this.MarginCollateralPhase3Grid, ref this.MainContextMenu);		//DC 
			contextMenuExtendBroadRidge = new ContextMenuExtend(ref this.MarginCollateralBroadRidgeGrid, ref this.MainContextMenu);	//DC new

		}

		private void StatusRibbonLabelUpdate()
		{
			try
			{
				if (MarginCollateralPhase3TabPage.Visible == true)		//DC new
				{
					if (dvMarginCollateralPhase3 != null)
						this.StatusRibbonLabel.Text = "Result: " + dvMarginCollateralPhase3.Count.ToString("#,##0") + " rows in grid";
				}
				else if (MarginCollateralBroadRidgeTabPage.Visible == true)
				{
					if (dvMarginCollateralBroadRidge != null)
						this.StatusRibbonLabel.Text = "Result: " + dvMarginCollateralBroadRidge.Count.ToString("#,##0") + " rows in grid";
				}
			}
			catch (Exception error) 
			{
				Log.Write(error.Message + " [MarginCollateralReportForm.StatusRibbonLabelUpdate]", 1);
			}
		}

		private void MarginCollateralReportForm_Load(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;		  

			try
			{	// Load Form previously recoreded form size and location
				this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "0"));
				this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "0"));
				this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "570"));
				this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "1530"));

				// Load Phase3 grid's column show/hide information
				foreach (C1DisplayColumn column in this.MarginCollateralPhase3Grid.Splits[0].DisplayColumns)
				{
					column.Visible = int.Parse(RegistryValue.Read(this.Name, this.MarginCollateralPhase3Grid.Name + "." + column.Name, "1")) == 1 ? true : false;
				}
				contextMenuExtendPhase3.AppendContextMenu();			//DC new 

				// Load BroadRidge Grid's column show.hide information	//DC new 
				foreach (C1DisplayColumn column in MarginCollateralBroadRidgeGrid.Splits[0].DisplayColumns)
				{
					column.Visible = int.Parse(RegistryValue.Read(this.Name, MarginCollateralBroadRidgeGrid.Name + "." + column.Name, "1")) == 1 ? true : false;
				}
				contextMenuExtendBroadRidge.AppendContextMenu();		//DC new  


				// Load Exchange Markets ComboBox (Dropdown List style) 
				ExchangeComboLoad();

				
				if (mainForm.ShortInterestAgent == null)
				{
					this.MarginCollateralDataStatusLabel.Text = "NorthStar server is not available.";
				}
				else
				{
					string dataDate = mainForm.ShortInterestAgent.ShortInterestDataDateGet();


					if (dataDate.Equals(""))
					{
						this.MarginCollateralDataStatusLabel.Text = "XFL data is not available.";
					}
					else
					{
						this.MarginCollateralDataStatusLabel.Text = "XFL Data is current for " + dataDate;
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[MarginCollateralReportForm.MarginCollateralReportForm_Load]", 1);
				this.MarginCollateralDataStatusLabel.Text = "NorthStar server is not available.";
			}

			this.Cursor = Cursors.Default;		 
		}

		private void MarginCollateralReportForm_FormClosed(object sender, FormClosedEventArgs e)
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
				// Save grid column show/hide information

				foreach (C1DisplayColumn column in this.MarginCollateralPhase3Grid.Splits[0].DisplayColumns)
				{
					RegistryValue.Write(this.Name, this.MarginCollateralPhase3Grid.Name + "." + column.Name, column.Visible ? "1" : "0");
				}

				//DC new 
				foreach (C1DisplayColumn column in this.MarginCollateralBroadRidgeGrid.Splits[0].DisplayColumns)	//DC new 
				{
					RegistryValue.Write(this.Name, this.MarginCollateralBroadRidgeGrid.Name + "." + column.Name, column.Visible ? "1" : "0");
				}

			}
			catch (Exception ex)
			{
				Log.Write(ex.Message + " [MarginCollateralReportForm.MarginCollateralReportForm_FormClosed]", Log.Error, 1);
			}

			mainForm.marginCollateralReportForm = null;
		}

		private void ExchangeComboLoad()
		{	
			DataSet dsExchange = null;		

			try
			{
				dsExchange = mainForm.ShortInterestAgent.ExchangeMarketGet();
				ExchangeCombo.HoldFields();
				ExchangeCombo.DataSource = dsExchange.Tables["Exchange"];
				ExchangeCombo.DataMember = "ExchId";
				ExchangeCombo.DisplayMember = "ExchDesc";
				ExchangeCombo.SelectedIndex = -1;			//No selection
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [MarginCollateralReportForm.ExchangeComboLoad]", 1);
				this.MarginCollateralDataStatusLabel.Text = "Exchange Market Listing is not available.";
			}

			dsExchange = null;
		}

		private void FindButton_Click(object sender, EventArgs e)
		{	
			if (DataValidation() == false)
				return;

			this.Cursor = Cursors.WaitCursor;

			try
			{
				string tmpExchId = "";
				tmpExchId = this.ExchangeCombo.Columns[0].Text;
				if (tmpExchId == "0") { tmpExchId = ""; }

				if (MarginCollateralPhase3TabPage.Visible == true )		//DC new
				{
					dsMarginCollateralPhase3.Clear();
					dsMarginCollateralPhase3 = mainForm.ShortInterestAgent.CashAccountOrMarginCollateralGet(
												this.TwentyDaysAverageMovingVolumeGreaterThanTextBox.Text.Trim(),
												this.TwentyDaysAverageMovingVolumeLessThanTextBox.Text.Trim(),
												this.PensonPercentOfFloatGreaterThanTextBox.Text.Trim(),
												this.PensonPercentOfFloatLessThanTextBox.Text.Trim(),
												this.PriceLessThanTextBox.Text.Trim(),
												this.DaysToLiquidityGreaterThanTextBox.Text.Trim(),
												this.Margin100ReqCodeNotRequiredCheckBox.Checked,				 // (SqlDbType.Bit)
												tmpExchId,								//DC 
												this.AccountNumberTextBox.Text.Trim(),	//DC 
												"2",									//DC		accountType
												"C",									//DC 		locMemoInclude
												"",										//DC		locMemoExclude
												true,									//DC new	showPhase3		(SqlDbType.Bit)
												false									//DC new	showBroadRidge	(SqlDbType.Bit)
												);
					dvMarginCollateralPhase3 = new DataView(dsMarginCollateralPhase3.Tables["MarginCollateral"], "", "Symbol", DataViewRowState.CurrentRows);
					this.MarginCollateralPhase3Grid.SetDataBinding(dvMarginCollateralPhase3, "", true);
				}
				else if (MarginCollateralBroadRidgeTabPage.Visible == true)
				{
					dsMarginCollateralBroadRidge.Clear();
					dsMarginCollateralBroadRidge = mainForm.ShortInterestAgent.CashAccountOrMarginCollateralGet(
												this.TwentyDaysAverageMovingVolumeGreaterThanTextBox.Text.Trim(),
												this.TwentyDaysAverageMovingVolumeLessThanTextBox.Text.Trim(),
												this.PensonPercentOfFloatGreaterThanTextBox.Text.Trim(),
												this.PensonPercentOfFloatLessThanTextBox.Text.Trim(),
												this.PriceLessThanTextBox.Text.Trim(),
												this.DaysToLiquidityGreaterThanTextBox.Text.Trim(),
												this.Margin100ReqCodeNotRequiredCheckBox.Checked,				 // (SqlDbType.Bit)
												tmpExchId,								//DC 
												this.AccountNumberTextBox.Text.Trim(),	//DC 
												"2",									//DC		accountType
												"",										//DC 		locMemoInclude
												"",										//DC		locMemoExclude
												false,									//DC new	showPhase3		(SqlDbType.Bit)
												true									//DC new	showBroadRidge	(SqlDbType.Bit)
												);
					dvMarginCollateralBroadRidge = new DataView(dsMarginCollateralBroadRidge.Tables["MarginCollateral"], "", "Symbol", DataViewRowState.CurrentRows);
					this.MarginCollateralBroadRidgeGrid.SetDataBinding(dvMarginCollateralBroadRidge, "", true);
				}
				StatusRibbonLabelUpdate();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [MarginCollateralReportForm.FindButton_Click]", 1);
			}

			this.Cursor = Cursors.Default;
		}

		private void MarginCollateralGrid_FetchCellTips(object sender, C1.Win.C1TrueDBGrid.FetchCellTipsEventArgs e)
		{
			string toolTipString = "";

            try
            {
				if (e.Row == -2)		// Column Header Row
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
                        case "TradeDateSharesLong":
                            toolTipString = "Total Shares Long On Trade Date";
                            break;
                        case "SettleDateSharesLong":
                            toolTipString = "Total Shares Long On Settlement Date";
                            break;
                        case "TwentyDaysAvgVolume":
                            toolTipString = "Twenty Days Average Moving Volume";
                            break;
                        case "DaysToLiquidity":
                            toolTipString = "Days To Liquidity";
                            break;
                        case "PensonPercentOfFloat":
                            toolTipString = "Penson Percent Of Float";
                            break;
                        case "ExchangeDescription":
                            toolTipString = "Exchange";
                            break;
                        case "CurrentPrice":
                            toolTipString = "Current Price";
                            break;
                        case "Margin100ReqCode":
                            toolTipString = "Margin 100% Required Code";
                            break;
                        case "Margin100ReqCodeBitValue":                    
                            toolTipString = "Margin 100% Required";         
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
                        case "SecurityType":
                            toolTipString = "Phase3 Security Type";
                            break;
                        case "TotalEquity":
                            toolTipString = "Settlement Date Market Value = (Quantity * Price)";
                            break;
                        case "GicsSectorCode":
                            toolTipString = "GICS Sector Code (Global Industry Classification Standard)";
                            break;
                        case "GicsGroupCode":
                            toolTipString = "GICS Group Code (Global Industry Classification Standard)";
                            break;
                        case "GicsIndustryCode":
                            toolTipString = "GICS Industry Code (Global Industry Classification Standard)";
                            break;
                        case "GicsSubIndustryCode":
                            toolTipString = "GICS Sub-Industry Code (Global Industry Classification Standard)";
                            break;
						//-------------------------- 
						case "AccountType":
							toolTipString = "Account Type";
							break;
						case "LocMemo":
							toolTipString = "Loc Memo";
							break;
						case "LocLocation":
							toolTipString = "Loc Location";
							break;
                        default:
                            toolTipString = "";
                            break;
                    }

                }
                else
                {   // Inside the grid data area, display the actual GICS Code's Description in CellTip 
                    // Note: need to display the GICS description where the mouse hovers, not for the current Row selected/focused.
                    // Using the grid's Row property, retrieve the bookmark of the row under the cursor, 
                    // then use the C1DataColumn.CellText method to get text from other (hidden description) cells.

					if (MarginCollateralPhase3TabPage.Visible == true)
					{
						switch (e.Column.DataColumn.DataField)
						{
							case "GicsSectorCode":
								//toolTipString = MarginCollateralPhase3Grid.Columns["GicsSectorDesc"].Text;          //This gets from current row
								toolTipString = MarginCollateralPhase3Grid.Columns["GicsSectorDesc"].CellText(e.Row); //This gets from mouse hover row
								break;
							case "GicsGroupCode":
								toolTipString = MarginCollateralPhase3Grid.Columns["GicsGroupDesc"].CellText(e.Row);
								break;
							case "GicsIndustryCode":
								toolTipString = MarginCollateralPhase3Grid.Columns["GicsIndustryDesc"].CellText(e.Row);
								break;
							case "GicsSubIndustryCode":
								toolTipString = MarginCollateralPhase3Grid.Columns["GicsSubIndustryDesc"].CellText(e.Row);
								break;
							default:
								toolTipString = "";
								break;
						}
					}
					else if (MarginCollateralBroadRidgeTabPage.Visible == true)
					{	//DC new 
						switch (e.Column.DataColumn.DataField)
						{
							case "GicsSectorCode":
								//toolTipString = MarginCollateralBroadRidgeGrid.Columns["GicsSectorDesc"].Text;          //This gets from current row
								toolTipString = MarginCollateralBroadRidgeGrid.Columns["GicsSectorDesc"].CellText(e.Row); //This gets from mouse hover row
								break;
							case "GicsGroupCode":
								toolTipString = MarginCollateralBroadRidgeGrid.Columns["GicsGroupDesc"].CellText(e.Row);
								break;
							case "GicsIndustryCode":
								toolTipString = MarginCollateralBroadRidgeGrid.Columns["GicsIndustryDesc"].CellText(e.Row);
								break;
							case "GicsSubIndustryCode":
								toolTipString = MarginCollateralBroadRidgeGrid.Columns["GicsSubIndustryDesc"].CellText(e.Row);
								break;
							default:
								toolTipString = "";
								break;
						}
					}
                }

                e.CellTip = toolTipString;
            }
            catch (Exception error)
            {
                Log.Write(error.Message + "  [MarginCollateralReportForm.MarginCollateralGrid_FetchCellTips]", 1);
            }

		}

		private void MarginCollateralGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{

			switch (e.Column.DataField)
			{
				case "SettleDateSharesLong":
				case "TradeDateSharesLong":
				case "SharesFloat":
					try
					{
						e.Value = long.Parse(e.Value.ToString()).ToString("#,##0");
					}
					catch { }
					break;
                case "TotalEquity":             //TotalEquity = Settlement Date Market Value
				case "CurrentPrice":
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
			}
		}

		private bool DataValidation()
		{
			decimal value = 0.0M;

			try
			{
				// we need at least one searching criteria entered.
				if (TwentyDaysAverageMovingVolumeGreaterThanTextBox.Text.Trim().Equals("") &&
						TwentyDaysAverageMovingVolumeLessThanTextBox.Text.Trim().Equals("") &&
						PensonPercentOfFloatGreaterThanTextBox.Text.Trim().Equals("") &&
						PensonPercentOfFloatLessThanTextBox.Text.Trim().Equals("") &&
						PriceLessThanTextBox.Text.Trim().Equals("") &&
                        DaysToLiquidityGreaterThanTextBox.Text.Trim().Equals("") && 
                        !Margin100ReqCodeNotRequiredCheckBox.Checked  &&
						(ExchangeCombo.SelectedText.Trim().Equals("") || ExchangeCombo.SelectedText.Trim().Equals("ALL")) &&
						AccountNumberTextBox.Text.Trim().Equals("")			//DC new 
					    )
				{
					MessageBox.Show("Please enter at least one searching criterion", "Error");
					this.AccountNumberTextBox.Focus(); 
					return false;
				}

				if (!TwentyDaysAverageMovingVolumeGreaterThanTextBox.Text.Trim().Equals(""))
				{
					if (decimal.TryParse(TwentyDaysAverageMovingVolumeGreaterThanTextBox.Text, out value) == false)
					{
						MessageBox.Show("value is incorrect", "Error");
						this.TwentyDaysAverageMovingVolumeGreaterThanTextBox.Focus();
						return false;
					}
					else if (value < 0)
					{
						MessageBox.Show("value can not be negative", "Error");
						this.TwentyDaysAverageMovingVolumeGreaterThanTextBox.Focus();
						return false;
					}
				}

				if (!TwentyDaysAverageMovingVolumeLessThanTextBox.Text.Trim().Equals(""))
				{
					if (decimal.TryParse(TwentyDaysAverageMovingVolumeLessThanTextBox.Text, out value) == false)
					{
						MessageBox.Show("value is incorrect", "Error");
						this.TwentyDaysAverageMovingVolumeLessThanTextBox.Focus();
						return false;
					}
					else if (value <= 0)
					{
						MessageBox.Show("value must be greater than zero", "Error");
						this.TwentyDaysAverageMovingVolumeLessThanTextBox.Focus();
						return false;
					}
				}

				if (!PensonPercentOfFloatGreaterThanTextBox.Text.Trim().Equals(""))
				{
					if (decimal.TryParse(PensonPercentOfFloatGreaterThanTextBox.Text, out value) == false)
					{
						MessageBox.Show("value is incorrect", "Error");
						this.PensonPercentOfFloatGreaterThanTextBox.Focus();
						return false;
					}
					else if (value < 0)
					{
						MessageBox.Show("value can not be negative", "Error");
						this.PensonPercentOfFloatGreaterThanTextBox.Focus();
						return false;
					}
				}

				if (!PensonPercentOfFloatLessThanTextBox.Text.Trim().Equals(""))
				{
					if (decimal.TryParse(PensonPercentOfFloatLessThanTextBox.Text, out value) == false)
					{
						MessageBox.Show("value is incorrect", "Error");
						this.PensonPercentOfFloatLessThanTextBox.Focus();
						return false;
					}
					else if (value <= 0)
					{
						MessageBox.Show("value must be greater than zero", "Error");
						this.PensonPercentOfFloatLessThanTextBox.Focus();
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
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [MarginCollateralReportForm.Datavalidation]", 1);
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
				style.DataField = "CurrentPrice";
				style.DataType = typeof(decimal);
				style.StringFormat = "#,##0.000";
				excelCellStyleDictionary.Add(style.DataField, style);
				
				style = new ExcelCellStyle();			//DC
				style.DataField = "TotalEquity";
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
				style.DataField = "SettleDateSharesLong";
				style.DataType = typeof(int);
				style.StringFormat = "#,##0";
				excelCellStyleDictionary.Add(style.DataField, style);

				style = new ExcelCellStyle();
				style.DataField = "TradeDateSharesLong";
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
				Log.Write(error.Message + "[MarginCollateralReportForm.InitializeExcelCellStyleDictionary]", 1);
			}
		}

		private void MarginCollateralPhase3Grid_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Control && e.KeyCode == Keys.Home)
			{
				if (this.MarginCollateralPhase3Grid.Splits[0].Rows.Count > 0)
				{
					MarginCollateralPhase3Grid.SetActiveCell(0, 0);
				}
			}
			else if (e.Control && e.KeyCode == Keys.End)
			{
				if (this.MarginCollateralPhase3Grid.Splits[0].Rows.Count > 0)
				{
                    MarginCollateralPhase3Grid.SetActiveCell(MarginCollateralPhase3Grid.Splits[0].Rows.Count - 1, 0);
				}
			}
		}

		private void MarginCollateralBroadRidgeGrid_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.Home)
			{
				if (this.MarginCollateralBroadRidgeGrid.Splits[0].Rows.Count > 0)
				{
					MarginCollateralBroadRidgeGrid.SetActiveCell(0, 0);
				}
			}
			else if (e.Control && e.KeyCode == Keys.End)
			{
				if (this.MarginCollateralBroadRidgeGrid.Splits[0].Rows.Count > 0)
				{
					MarginCollateralBroadRidgeGrid.SetActiveCell(MarginCollateralBroadRidgeGrid.Splits[0].Rows.Count - 1, 0);
				}
			}
		}	//DC new 

		private void MarginCollateralPhase3Grid_Paint(object sender, PaintEventArgs e)
		{
			if (!secId.Equals(MarginCollateralPhase3Grid.Columns["CUSIP"].Text))
			{
				secId = MarginCollateralPhase3Grid.Columns["CUSIP"].Text;
				mainForm.SecId = secId;        
			}

			StatusRibbonLabelUpdate();
		}

		private void MarginCollateralBroadRidgeGrid_Paint(object sender, PaintEventArgs e)
		{
			if (!secId.Equals(MarginCollateralBroadRidgeGrid.Columns["CUSIP"].Text))
			{
				secId = MarginCollateralBroadRidgeGrid.Columns["CUSIP"].Text;
				mainForm.SecId = secId;
			}

			StatusRibbonLabelUpdate();
		}	//DC new

		private void MarginCollateralGrid_MouseLeave(object sender, EventArgs e)
		{
			this.mouseLocation.X = -1;
			this.mouseLocation.Y = -1;
		}

		private void MarginCollateralGrid_MouseMove(object sender, MouseEventArgs e)
		{
			this.mouseLocation.X = e.X;
			this.mouseLocation.Y = e.Y;

		}

		private void MarginCollateralPhase3TabPage_VisibleChanged(object sender, EventArgs e)
		{
			if (MarginCollateralPhase3TabPage.Visible == true)
			{
				if (dsMarginCollateralBroadRidge != null) 		//DC new clear the non-visible grid's dataset 
				{
					dsMarginCollateralBroadRidge.Clear();
				}
			}
			this.AccountNumberTextBox.Text = "";
			this.Margin100ReqCodeNotRequiredCheckBox.Checked = false;
			this.ExchangeCombo.SelectedIndex = -1;	
			this.DaysToLiquidityGreaterThanTextBox.Text = "";
			this.PensonPercentOfFloatGreaterThanTextBox.Text = "";
			this.TwentyDaysAverageMovingVolumeLessThanTextBox.Text = "";
			this.PriceLessThanTextBox.Text = "";
		}

		private void MarginCollateralBroadRidgeTabPage_VisibleChanged(object sender, EventArgs e)
		{
			if (MarginCollateralBroadRidgeTabPage.Visible == true)
			{
				if (dsMarginCollateralPhase3 != null) 		//DC new clear the non-visible grid's dataset 
				{
					dsMarginCollateralPhase3.Clear();
				}
			}
			this.AccountNumberTextBox.Text = "";
			this.Margin100ReqCodeNotRequiredCheckBox.Checked = false;
			this.ExchangeCombo.SelectedIndex = -1;
			this.DaysToLiquidityGreaterThanTextBox.Text = "";
			this.PensonPercentOfFloatGreaterThanTextBox.Text = "";
			this.TwentyDaysAverageMovingVolumeLessThanTextBox.Text = "";
			this.PriceLessThanTextBox.Text = "";
		}

		private void MainContextMenu_Popup(object sender, EventArgs e)
		{
			//DC new
			if (MarginCollateralPhase3TabPage.Visible == true)
			{
				contextMenuExtendPhase3.EnableDisableContextMenu(mouseLocation);
			}
			else if (MarginCollateralBroadRidgeTabPage.Visible == true)
			{
				contextMenuExtendBroadRidge.EnableDisableContextMenu(mouseLocation);
			}
		}

		private void ClearResultsMenuItem_Click(object sender, EventArgs e)
		{
			if (MarginCollateralPhase3TabPage.Visible == true)
			{
				this.dsMarginCollateralPhase3.Clear();
				this.StatusRibbonLabel.Text = "";
			}
			else if (MarginCollateralBroadRidgeTabPage.Visible == true)
			{
				this.dsMarginCollateralBroadRidge.Clear();
				this.StatusRibbonLabel.Text = "";
			}
		}

		private string GetSelectedContent(ref C1TrueDBGrid grid)
		{
			return ""; 
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
							}
						}

						if (visibleColumn)
							maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
					}
				}
				else
				{
					maxTextLength = new int[grid.SelectedCols.Count];

					foreach (C1DataColumn dataColumn in grid.SelectedCols)
					{
						maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
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
					builder.Append(new string('-', maxTextLength[++columnIndex]) + "  ");
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
								builder.Append(dataColumn.CellText(rowIndex).Trim().PadLeft(maxTextLength[++columnIndex]) + "  ");
							}
						}
					}

					builder.AppendLine();
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [MarginCollateralReportingForm.GetSelectedContent]", 1);
				builder = new StringBuilder();
			}

			return builder.ToString();
		***/
		}

		//---------------------------------- 

 		private void SendToClipboardCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{
				if (MarginCollateralPhase3Grid.Focused)
				{
					mainForm.SendToClipboard(ref MarginCollateralPhase3Grid);
				}
				else if (MarginCollateralBroadRidgeGrid.Focused)
				{
					//DC new
					mainForm.SendToClipboard(ref MarginCollateralBroadRidgeGrid);
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [MarginCollateralReportForm.SendToClipboardCommand_Click]", 1);
			}
		}

		private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{
				if (MarginCollateralPhase3Grid.Focused)
				{
					mainForm.SendToExcel(ref MarginCollateralPhase3Grid, 0, excelCellStyleDictionary);
				}
				else if (MarginCollateralBroadRidgeGrid.Focused)
				{
					//DC new
					mainForm.SendToExcel(ref MarginCollateralBroadRidgeGrid, 0, excelCellStyleDictionary);
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [MarginCollateralReportForm.SendToExcelCommand_Click]", 1);
			}
		}

		private void SendToEmailCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{
				if (MarginCollateralPhase3Grid.Focused)
				{
					mainForm.SendToEmail(ref MarginCollateralPhase3Grid);
				}
				else if (MarginCollateralBroadRidgeGrid.Focused)
				{	
					//DC new 
					mainForm.SendToEmail(ref MarginCollateralBroadRidgeGrid);
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [MarginCollateralReportForm.SendToEmailCommand_Click]", 1);
			}
		}

        

	}
}