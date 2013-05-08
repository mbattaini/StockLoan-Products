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
	public partial class CashAccountReportForm : C1RibbonForm
	{
		private MainForm mainForm = null;		
		private DataSet dsCashAccountPhase3 = null;				//DC new
		private DataView dvCashAccountPhase3 = null;			//DC new 
		private DataSet dsCashAccountBroadRidge = null;			//DC new
		private DataView dvCashAccountBroadRidge = null;		//DC new 

		//private DataSet dsExchange = null;					//DC 2009-12-04 

		private string secId = "";
		private Point mouseLocation;
		private ContextMenuExtend contextMenuExtendPhase3 = null;		//DC new 
		private ContextMenuExtend contextMenuExtendBroadRidge = null;	//DC new 

		private Dictionary<string, ExcelCellStyle> excelCellStyleDictionary = null;
		
		public CashAccountReportForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
			this.mouseLocation = new Point(-1, -1);
			this.dsCashAccountPhase3 = new DataSet();			//DC new
			this.dsCashAccountBroadRidge = new DataSet();		//DC new 

			InitializeExcelCellStyleDictionary();

			InitializeComponent();

			//Initialize contextMenuExtend object
			contextMenuExtendPhase3 = new ContextMenuExtend(ref this.CashAccountPhase3Grid, ref this.MainContextMenu);				//DC 
			contextMenuExtendBroadRidge = new ContextMenuExtend(ref this.CashAccountBroadRidgeGrid, ref this.MainContextMenu);		//DC new 

		}

		private void StatusRibbonLabelUpdate()
		{
			try
			{
				if (CashAccountPhase3TabPage.Visible == true)
				{
					if (dvCashAccountPhase3 != null)
						this.StatusRibbonLabel.Text = "Result: " + dvCashAccountPhase3.Count.ToString("#,##0") + " rows in grid";
				}
				else if (CashAccountBroadRidgeTabPage.Visible == true)
				{
					if (dvCashAccountBroadRidge != null)
						this.StatusRibbonLabel.Text = "Result: " + dvCashAccountBroadRidge.Count.ToString("#,##0") + " rows in grid";
				}
			}
			catch (Exception error) 
			{
				Log.Write(error.Message + " [CashAccountReportForm.StatusRibbonLabelUpdate]", 1);
			}
		}

		private void CashAccountReportForm_Load(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;		  

			try
			{	// Load Form previously recoreded form size and location
				this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "0"));
				this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "0"));
				this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", "570"));
				this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", "1530"));

				// Load Phase3 grid's column show/hide information
				foreach (C1DisplayColumn column in this.CashAccountPhase3Grid.Splits[0].DisplayColumns)
				{
					column.Visible = int.Parse(RegistryValue.Read(this.Name, this.CashAccountPhase3Grid.Name + "." + column.Name, "1")) == 1 ? true : false;
				}
				contextMenuExtendPhase3.AppendContextMenu();			//DC new 

				// Load BroadRidge Grid's column show.hide information	//DC new 
				foreach (C1DisplayColumn column in CashAccountBroadRidgeGrid.Splits[0].DisplayColumns)
				{
					column.Visible = int.Parse(RegistryValue.Read(this.Name, CashAccountBroadRidgeGrid.Name + "." + column.Name, "1")) == 1 ? true : false;
				}
				contextMenuExtendBroadRidge.AppendContextMenu();		//DC new  


				// Load Exchange Markets ComboBox (Dropdown List style) 
				ExchangeComboLoad();


				if (mainForm.ShortInterestAgent == null)
				{
					this.CashAccountDataStatusLabel.Text = "NorthStar server is not available.";
				}
				else
				{
					string dataDate = mainForm.ShortInterestAgent.ShortInterestDataDateGet();

					if (dataDate.Equals(""))
					{
						this.CashAccountDataStatusLabel.Text = "XFL data is not available.";
					}
					else
					{
						this.CashAccountDataStatusLabel.Text = "XFL Data is current for " + dataDate;
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[CashAccountReportForm.CashAccountReportForm_Load]", 1);
				this.CashAccountDataStatusLabel.Text = "NorthStar server is not available.";
			}

			this.Cursor = Cursors.Default;		 
		}

		private void CashAccountReportForm_FormClosed(object sender, FormClosedEventArgs e)
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
				foreach (C1DisplayColumn column in this.CashAccountPhase3Grid.Splits[0].DisplayColumns)
				{
					RegistryValue.Write(this.Name, this.CashAccountPhase3Grid.Name + "." + column.Name, column.Visible ? "1" : "0");
				}
				//DC new 
				foreach (C1DisplayColumn column in this.CashAccountBroadRidgeGrid.Splits[0].DisplayColumns)	//DC new 
				{
					RegistryValue.Write(this.Name, this.CashAccountBroadRidgeGrid.Name + "." + column.Name, column.Visible ? "1" : "0");
				}
			}
			catch (Exception ex)
			{
				Log.Write(ex.Message + " [CashAccountReportForm.CashAccountReportForm_FormClosed]", Log.Error, 1);
			}

			mainForm.cashAccountReportForm = null;
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
				Log.Write(error.Message + " [CashAccountReportForm.ExchangeComboLoad]", 1);
				this.CashAccountDataStatusLabel.Text = "Exchange Market Listing is not available.";
			}

			dsExchange = null;
		}

		private void FindButton_Click(object sender, EventArgs e)
		{	
			//DC per Dave Rafteseth 20100312: definition for Cash Accounts: [StockLocationCurrent] table's AccountType = '1' and LocMemo <> 'S' 

			if (DataValidation() == false)
				return;

			this.Cursor = Cursors.WaitCursor;

			try
			{
				string tmpExchId = "";
				tmpExchId = this.ExchangeCombo.Columns[0].Text;
				if (tmpExchId == "0") { tmpExchId = ""; }

				if (CashAccountPhase3TabPage.Visible == true)	//DC new
				{
					dsCashAccountPhase3.Clear();
					dsCashAccountPhase3 = mainForm.ShortInterestAgent.CashAccountOrMarginCollateralGet(
												this.TwentyDaysAverageMovingVolumeGreaterThanTextBox.Text.Trim(),
												this.TwentyDaysAverageMovingVolumeLessThanTextBox.Text.Trim(),
												this.PensonPercentOfFloatGreaterThanTextBox.Text.Trim(),
												this.PensonPercentOfFloatLessThanTextBox.Text.Trim(),
												this.PriceLessThanTextBox.Text.Trim(),
												this.DaysToLiquidityGreaterThanTextBox.Text.Trim(),
												this.Margin100ReqCodeNotRequiredCheckBox.Checked,
												tmpExchId,								//DC 
												this.AccountNumberTextBox.Text.Trim(),	//DC 
												"1",									//DC		accountType
												"",										//DC		locMemoInclude
												"S",									//DC		locMemoExclude
												true,									//DC new	showPhase3		(SqlDbType.Bit)
												false									//DC new	showBroadRidge	(SqlDbType.Bit)
												);
					dvCashAccountPhase3 = new DataView(dsCashAccountPhase3.Tables["CashAccount"], "", "Symbol", DataViewRowState.CurrentRows);
					this.CashAccountPhase3Grid.SetDataBinding(dvCashAccountPhase3, "", true);
				}
				else if (CashAccountBroadRidgeTabPage.Visible == true)
				{
					dsCashAccountBroadRidge.Clear();
					dsCashAccountBroadRidge = mainForm.ShortInterestAgent.CashAccountOrMarginCollateralGet(
												this.TwentyDaysAverageMovingVolumeGreaterThanTextBox.Text.Trim(),
												this.TwentyDaysAverageMovingVolumeLessThanTextBox.Text.Trim(),
												this.PensonPercentOfFloatGreaterThanTextBox.Text.Trim(),
												this.PensonPercentOfFloatLessThanTextBox.Text.Trim(),
												this.PriceLessThanTextBox.Text.Trim(),
												this.DaysToLiquidityGreaterThanTextBox.Text.Trim(),
												this.Margin100ReqCodeNotRequiredCheckBox.Checked,				 // (SqlDbType.Bit)
												tmpExchId,								//DC 
												this.AccountNumberTextBox.Text.Trim(),	//DC 
												"1",									//DC		accountType
												"",										//DC 		locMemoInclude
												"",										//DC		locMemoExclude
												false,									//DC new	showPhase3		(SqlDbType.Bit)
												true									//DC new	showBroadRidge	(SqlDbType.Bit)
												);
					dvCashAccountBroadRidge = new DataView(dsCashAccountBroadRidge.Tables["CashAccount"], "", "Symbol", DataViewRowState.CurrentRows);
					this.CashAccountBroadRidgeGrid.SetDataBinding(dvCashAccountBroadRidge, "", true);
				}
				StatusRibbonLabelUpdate();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [CashAccountReportForm.FindButton_Click]", 1);
			}

			this.Cursor = Cursors.Default;
		}

		private void CashAccountGrid_FetchCellTips(object sender, C1.Win.C1TrueDBGrid.FetchCellTipsEventArgs e)
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

					if (CashAccountPhase3TabPage.Visible == true)
					{
						switch (e.Column.DataColumn.DataField)
						{
							case "GicsSectorCode":
								//toolTipString = CashAccountPhase3Grid.Columns["GicsSectorDesc"].Text;          //This gets from current row
								toolTipString = CashAccountPhase3Grid.Columns["GicsSectorDesc"].CellText(e.Row); //This gets from mouse hover row
								break;
							case "GicsGroupCode":
								toolTipString = CashAccountPhase3Grid.Columns["GicsGroupDesc"].CellText(e.Row);
								break;
							case "GicsIndustryCode":
								toolTipString = CashAccountPhase3Grid.Columns["GicsIndustryDesc"].CellText(e.Row);
								break;
							case "GicsSubIndustryCode":
								toolTipString = CashAccountPhase3Grid.Columns["GicsSubIndustryDesc"].CellText(e.Row);
								break;
							default:
								toolTipString = "";
								break;
						}
					}
					else if (CashAccountBroadRidgeTabPage.Visible == true)
					{	//DC new 
						switch (e.Column.DataColumn.DataField)
						{
							case "GicsSectorCode":
								//toolTipString = CashAccountBroadRidgeGrid.Columns["GicsSectorDesc"].Text;          //This gets from current row
								toolTipString = CashAccountBroadRidgeGrid.Columns["GicsSectorDesc"].CellText(e.Row); //This gets from mouse hover row
								break;
							case "GicsGroupCode":
								toolTipString = CashAccountBroadRidgeGrid.Columns["GicsGroupDesc"].CellText(e.Row);
								break;
							case "GicsIndustryCode":
								toolTipString = CashAccountBroadRidgeGrid.Columns["GicsIndustryDesc"].CellText(e.Row);
								break;
							case "GicsSubIndustryCode":
								toolTipString = CashAccountBroadRidgeGrid.Columns["GicsSubIndustryDesc"].CellText(e.Row);
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
				Log.Write(error.Message + "  [CashAccountReportForm.CashAccountGrid_FetchCellTips]", 1);
            }

		}

		private void CashAccountGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
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
						AccountNumberTextBox.Text.Trim().Equals("")			//DC 
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
				Log.Write(error.Message + " [CashAccountReportForm.Datavalidation]", 1);
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
				Log.Write(error.Message + "[CashAccountReportForm.InitializeExcelCellStyleDictionary]", 1);
			}
		}

		private void CashAccountPhase3Grid_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Control && e.KeyCode == Keys.Home)
			{
				if (this.CashAccountPhase3Grid.Splits[0].Rows.Count > 0)
				{
					CashAccountPhase3Grid.SetActiveCell(0, 0);
				}
			}
			else if (e.Control && e.KeyCode == Keys.End)
			{
				if (this.CashAccountPhase3Grid.Splits[0].Rows.Count > 0)
				{
                    CashAccountPhase3Grid.SetActiveCell(CashAccountPhase3Grid.Splits[0].Rows.Count - 1, 0);
				}
			}
		}

		private void CashAccountBroadRidgeGrid_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.Home)
			{
				if (this.CashAccountBroadRidgeGrid.Splits[0].Rows.Count > 0)
				{
					CashAccountBroadRidgeGrid.SetActiveCell(0, 0);
				}
			}
			else if (e.Control && e.KeyCode == Keys.End)
			{
				if (this.CashAccountBroadRidgeGrid.Splits[0].Rows.Count > 0)
				{
					CashAccountBroadRidgeGrid.SetActiveCell(CashAccountBroadRidgeGrid.Splits[0].Rows.Count - 1, 0);
				}
			}
		}

		private void CashAccountPhase3Grid_Paint(object sender, PaintEventArgs e)
		{
			if (!secId.Equals(CashAccountPhase3Grid.Columns["CUSIP"].Text))
			{
				secId = CashAccountPhase3Grid.Columns["CUSIP"].Text;
				mainForm.SecId = secId;        
			}

			StatusRibbonLabelUpdate();
		}

		private void CashAccountBroadRidgeGrid_Paint(object sender, PaintEventArgs e)
		{
			if (!secId.Equals(CashAccountBroadRidgeGrid.Columns["CUSIP"].Text))
			{
				secId = CashAccountBroadRidgeGrid.Columns["CUSIP"].Text;
				mainForm.SecId = secId;
			}

			StatusRibbonLabelUpdate();
		}	//DC new

		private void CashAccountGrid_MouseLeave(object sender, EventArgs e)
		{
			this.mouseLocation.X = -1;
			this.mouseLocation.Y = -1;
		}

		private void CashAccountGrid_MouseMove(object sender, MouseEventArgs e)
		{
			this.mouseLocation.X = e.X;
			this.mouseLocation.Y = e.Y;

		}

		private void CashAccountPhase3TabPage_VisibleChanged(object sender, EventArgs e)
		{
			if (CashAccountPhase3TabPage.Visible == true)
			{
				if (dsCashAccountBroadRidge != null) 		//DC new clear the non-visible grid's dataset 
				{
					dsCashAccountBroadRidge.Clear();
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

		private void CashAccountBroadRidgeTabPage_VisibleChanged(object sender, EventArgs e)
		{
			if (CashAccountBroadRidgeTabPage.Visible == true)
			{
				if (dsCashAccountPhase3 != null) 		//DC new clear the non-visible grid's dataset 
				{
					dsCashAccountPhase3.Clear();
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
		{	//DC new 
			if (CashAccountPhase3TabPage.Visible == true)
			{
				contextMenuExtendPhase3.EnableDisableContextMenu(mouseLocation);
			}
			else if (CashAccountBroadRidgeTabPage.Visible == true)
			{
				contextMenuExtendBroadRidge.EnableDisableContextMenu(mouseLocation);
			}
		}

		private void ClearResultsMenuItem_Click(object sender, EventArgs e)
		{
			if (CashAccountPhase3TabPage.Visible == true)
			{
				this.dsCashAccountPhase3.Clear();
				this.StatusRibbonLabel.Text = "";
			}
			else if (CashAccountBroadRidgeTabPage.Visible == true)
			{
				this.dsCashAccountBroadRidge.Clear();
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
				Log.Write(error.Message + " [CashAccountReportingForm.GetSelectedContent]", 1);
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
				if (CashAccountPhase3Grid.Focused)
				{
					mainForm.SendToClipboard(ref CashAccountPhase3Grid);
				}
				else if (CashAccountBroadRidgeGrid.Focused)
				{	//DC new 
					mainForm.SendToClipboard(ref CashAccountBroadRidgeGrid);
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [CashAccountReportForm.SendToClipboardCommand_Click]", 1);
			}
		}

		private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{
				if (CashAccountPhase3Grid.Focused)
				{
					mainForm.SendToExcel(ref CashAccountPhase3Grid, 0, excelCellStyleDictionary);
				}
				else if (CashAccountBroadRidgeGrid.Focused)
				{	//DC new 
					mainForm.SendToExcel(ref CashAccountBroadRidgeGrid, 0, excelCellStyleDictionary);
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [CashAccountReportForm.SendToExcelCommand_Click]", 1);
			}
		}

		private void SendToEmailCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
		{
			try
			{
				if (CashAccountPhase3Grid.Focused)
				{
					mainForm.SendToEmail(ref CashAccountPhase3Grid);
				}
				else if (CashAccountBroadRidgeGrid.Focused)
				{	//DC new 
					mainForm.SendToEmail(ref CashAccountBroadRidgeGrid);
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "  [CashAccountReportForm.SendToEmailCommand_Click]", 1);
			}
		}
        

	}
}