using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StockLoan.Common;
using StockLoan.Main;
using StockLoan.MainBusiness;

namespace CentralClient
{
	public partial class TradingContractsMarksForm : C1.Win.C1Ribbon.C1RibbonForm
	{
		private DataSet dsContracts;
		private DataSet dsBookGroups;

		private DataView dvContracts;
		private MainForm mainForm;

        private DataSet dsMarks;

		public TradingContractsMarksForm(MainForm mainForm)
		{
			InitializeComponent();

			this.mainForm = mainForm;
		}

		private void TradingContractsMarksForm_Load(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;

			try
			{
                int height = this.Height;
                int width = this.Width;

                this.Top = int.Parse(RegistryValue.Read(this.Name, "Top", "25"));
                this.Left = int.Parse(RegistryValue.Read(this.Name, "Left", "25"));
                this.Height = int.Parse(RegistryValue.Read(this.Name, "Height", height.ToString()));
                this.Width = int.Parse(RegistryValue.Read(this.Name, "Width", width.ToString()));

				dsBookGroups = mainForm.ServiceAgent.BookGroupGet(mainForm.UserId);

				BookGroupNameLabel.DataSource = dsBookGroups.Tables["BookGroups"];
				BookGroupNameLabel.DataField = "BookGroupName";

				BookGroupCombo.HoldFields();
				BookGroupCombo.DataSource = dsBookGroups.Tables["BookGroups"];
				BookGroupCombo.SelectedIndex = -1;

				if (dsBookGroups.Tables["BookGroups"].Rows.Count > 0)
				{
					BookGroupCombo.SelectedIndex = 0;
				}

				dsContracts = mainForm.PositionAgent.ContractDataGet(mainForm.UtcOffset, mainForm.ServiceAgent.BizDate(), "", "");				
				dsContracts.Tables["Contracts"].Columns.Add("MarkAmount");
                dsContracts.Tables["Contracts"].Columns.Add("NewContractAmount");
                dsContracts.Tables["Contracts"].Columns.Add("MarkStatus");
                dsContracts.Tables["Contracts"].Columns.Add("DoMark");
                dsContracts.AcceptChanges();

				dvContracts = new DataView(dsContracts.Tables["Contracts"], "BookGroup = '" + BookGroupCombo.Text + "' AND Quantity > 0 AND Amount > 0 AND SettleDate <='" + mainForm.ServiceAgent.BizDate() +"'", "", DataViewRowState.CurrentRows);
				ContractMarksGrid.SetDataBinding(dvContracts, "", true);

                CalulcateMarks();
                CheckDoMark(false);
            }
			catch (Exception error)
			{
				mainForm.Alert(this.Name, error.Message);
			}

			this.Cursor = Cursors.Default;
		}

        private void CheckDoMark(bool check)
        {
            foreach (DataRowView dr in dvContracts)
            {
                if (!dr["CurrentPrice"].ToString().Equals(""))
                {
                    dr["DoMark"] = check;
                }
            }
        }
        
        private void CalulcateMarks()
        {            
            for (int i = 0; i < ContractMarksGrid.RowCount; i ++)
            {
                if (!ContractMarksGrid.Columns["CurrentPrice"].CellText(i).Equals(""))
                {
                    CalculateMark(i);			
                }
            }
        }
        
        private void BookGroupCombo_TextChanged(object sender, EventArgs e)
		{
			if (dvContracts != null)
			{
				dvContracts.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
                CalulcateMarks();
			}
		}

		private void ContractMarksGrid_FormatText(object sender, C1.Win.C1TrueDBGrid.FormatTextEventArgs e)
		{
            e.Value = mainForm.Format(e.Column.DataField, e.Value);
		}

        private void CalculateMark(int row)
        {
            decimal oldAmount = 0;				
            decimal newAmount = 0;
            decimal margin = 0;
            decimal currentPrice = 0;
            long quantity = 0;

            
            try
            {
                if (ContractMarksGrid.Columns["CurrentPrice"].CellValue(row).ToString().Equals("") || (row < 0))
                {
                    return;
                }

                currentPrice = decimal.Parse(ContractMarksGrid.Columns["CurrentPrice"].CellValue(row).ToString());
                margin = decimal.Parse(ContractMarksGrid.Columns["Margin"].CellValue(row).ToString());
                quantity = long.Parse(ContractMarksGrid.Columns["Quantity"].CellValue(row).ToString());

                oldAmount = decimal.Parse(ContractMarksGrid.Columns["Amount"].CellValue(row).ToString());
                newAmount = quantity * currentPrice * margin;
                
                ContractMarksGrid[row, "NewContractAmount"] = newAmount;
                ContractMarksGrid[row, "CurrentPriceMargin"] = currentPrice * margin;

                switch (ContractMarksGrid.Columns["ContractType"].CellText(row))
                {
                    case "B":
                        ContractMarksGrid[row, "MarkAmount"] = ((newAmount - oldAmount) * -1);

                        if (ContractMarksGrid[row, "DoMark"].ToString().Equals(""))
                        {
                            ContractMarksGrid[row, "DoMark"] = false;
                        }
                        break;

                    case "L":
                        ContractMarksGrid[row, "MarkAmount"] = (newAmount - oldAmount);
                        if (ContractMarksGrid[row, "DoMark"].ToString().Equals(""))
                        {
                            ContractMarksGrid[row, "DoMark"] = false;
                        }                        
                        break;
                }
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }
        }

		private void ContractMarksGrid_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar.Equals((char)13))
			{
				ContractMarksGrid.Update();
			}
		}

        private void TradingContractsMarksForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState.Equals(FormWindowState.Normal) && this.Dock.Equals(DockStyle.None))
            {
                RegistryValue.Write(this.Name, "Top", this.Top.ToString());
                RegistryValue.Write(this.Name, "Left", this.Left.ToString());
                RegistryValue.Write(this.Name, "Height", this.Height.ToString());
                RegistryValue.Write(this.Name, "Width", this.Width.ToString());
            }
        }

        private void ApplyRibbonButton_Click(object sender, EventArgs e)
        {
            decimal newAmount = 0;

            long markSuccessCount = 0;
            long markErrorCount = 0;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                dsMarks = mainForm.PositionAgent.MarksGet("", mainForm.ServiceAgent.BizDate(), "", "", mainForm.UtcOffset);

                foreach (DataRowView dr in dvContracts)
                {
                    if (!dr["CurrentPrice"].ToString().Equals("") && bool.Parse(dr["DoMark"].ToString()))
                    {
                        if (!Marks.MarkIsExist(dr["BookGroup"].ToString(), dr["Book"].ToString(), dr["ContractId"].ToString(), dr["ContractType"].ToString(), dr["SecId"].ToString(), dr["MarkAmount"].ToString(), dsMarks))
                        {
                            mainForm.PositionAgent.MarkSet(
                                "",
                                dr["BookGroup"].ToString(),
                                dr["Book"].ToString(),
                                dr["ContractId"].ToString(),
                                dr["ContractType"].ToString(),
                                dr["SecId"].ToString(),
                                dr["MarkAmount"].ToString(),
                                mainForm.ServiceAgent.BizDate(),
                                "",
                                "",
                                mainForm.UserId,
                                true);

                            try
                            {
                                mainForm.ServiceAgent.PriceSet(
                                    dr["SecId"].ToString(),
                                    dr["SecurityDepot"].ToString(),
                                    dr["CurrencyIso"].ToString(),
                                    dr["CurrentPrice"].ToString(),
                                    dr["BizDate"].ToString()
                                    );
                            }
                            catch (Exception error)
                            {
                                mainForm.Alert(this.Name, "Could not write new price to pricing table, " + error.Message);
                            }


                            if (bool.Parse(mainForm.ServiceAgent.KeyValueGet("AllowUserMarks", "")))
                            {
                                newAmount = (long.Parse(dr["Quantity"].ToString()) * decimal.Parse(dr["CurrentPrice"].ToString())) * decimal.Parse(dr["Margin"].ToString());

                                mainForm.PositionAgent.ContractSet(
                                    dr["BizDate"].ToString(),
                                    dr["BookGroup"].ToString(),
                                    dr["ContractId"].ToString(),
                                    dr["ContractType"].ToString(),
                                    dr["Book"].ToString(),
                                    "",
                                    "",
                                    "",
                                    newAmount.ToString(),
                                    newAmount.ToString(),
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                    true,
                                    dr["FeeAmount"].ToString(), 
                                    dr["FeeCurrencyIso"].ToString(), 
                                    dr["FeeType"].ToString(),
                                    "",
                                    "",
                                    "",
                                    "",
                                    "",
                                "");

                            }

                            markSuccessCount++;
                        }
                        else
                        {
                            dr["MarkStatus"] = "E";
                            markErrorCount++;
                        }
                    }
                }


				if (markSuccessCount > 0 && markErrorCount == 0)
				{
                    bool result;
                    DataSet dsContractsNew = new DataSet();
					                                       					
                    dsContractsNew = mainForm.PositionAgent.ContractDataGet(mainForm.UtcOffset, mainForm.ServiceAgent.BizDate(), "", "");
                    dsContractsNew.Tables["Contracts"].Columns.Add("MarkAmount");
                    dsContractsNew.Tables["Contracts"].Columns.Add("NewContractAmount");
                    dsContractsNew.Tables["Contracts"].Columns.Add("MarkStatus");
                    dsContractsNew.Tables["Contracts"].Columns.Add("DoMark");
                    dsContractsNew.AcceptChanges();

                    for (int i = 0; i < dsContracts.Tables["Contracts"].Rows.Count; i++)
                    {
                        if (bool.TryParse(dsContracts.Tables["Contracts"].Rows[i]["DoMark"].ToString(),out result))
                        {
                            if (bool.Parse(dsContracts.Tables["Contracts"].Rows[i]["DoMark"].ToString()))
                            {
                                for (int j = 0; j < dsContractsNew.Tables["Contracts"].Rows.Count; j++)
                                {
                                    if (dsContracts.Tables["Contracts"].Rows[i]["ContractId"].ToString().Equals(dsContractsNew.Tables["Contracts"].Rows[j]["ContractId"].ToString()))
                                    {
                                        dsContracts.Tables["Contracts"].Rows.RemoveAt(i);
                                        dsContracts.Tables["Contracts"].LoadDataRow(dsContractsNew.Tables["Contracts"].Rows[j].ItemArray, true);                                                                                
                                    }
                                }
                            }
                        }
                    }                                       
                                       
					CalulcateMarks();
					CheckDoMark(false);

					
					if (mainForm.tradingContractSummaryForm != null)
					{
						mainForm.tradingContractSummaryForm.ContractSummaryLoad();
					}

				}				
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, error.Message);
            }

            mainForm.Alert(this.Name, "Marks created: " + markSuccessCount.ToString(Formats.Collateral) + ". Marks errors: " + markErrorCount.ToString(Formats.Collateral) + ".");
            this.Cursor = Cursors.Default;
        }

        private void CancelRibbonButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ContractMarksGrid_FetchRowStyle(object sender, C1.Win.C1TrueDBGrid.FetchRowStyleEventArgs e)
        {
            if (ContractMarksGrid.Columns["MarkStatus"].CellText(e.Row).Equals("E"))
            {
                e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
            }
        }

        private void UncheckAllRibbonButton_Click(object sender, EventArgs e)
        {
            CheckDoMark(false);
        }

        private void CheckAllRibbonButton_Click(object sender, EventArgs e)
        {
            CheckDoMark(true);
        }
     
        private void ContractMarksGrid_BeforeUpdate(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            CalculateMark(ContractMarksGrid.Row - 1);

            
            try
            {
                mainForm.ServiceAgent.PriceSet(
                    ContractMarksGrid.Columns["SecId"].Text,
                    ContractMarksGrid.Columns["SecurityDepot"].Text,
                    ContractMarksGrid.Columns["CurrencyIso"].Text,
                    ContractMarksGrid.Columns["CurrentPrice"].Text,
                    ContractMarksGrid.Columns["BizDate"].Text
                    );
            }
            catch (Exception error)
            {
                mainForm.Alert(this.Name, "Could not write new price to pricing table, " + error.Message);
            }
        }

        private void ContractMarksGrid_AfterColUpdate(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
        {
            CalculateMark(ContractMarksGrid.Row);

            if (e.Column.DataColumn.DataField.Equals("CurrentPrice"))
            {
                try
                {
                    mainForm.ServiceAgent.PriceSet(
                        ContractMarksGrid.Columns["SecId"].Text,
                        ContractMarksGrid.Columns["SecurityDepot"].Text,
                        ContractMarksGrid.Columns["CurrencyIso"].Text,
                        ContractMarksGrid.Columns["CurrentPrice"].Text,
                        ContractMarksGrid.Columns["BizDate"].Text
                        );
                }
                catch (Exception error)
                {
                    mainForm.Alert(this.Name, "Could not write new price to pricing table, " + error.Message);
                }
            }
        }

        private void SendToExcelCommand_Click(object sender, C1.Win.C1Command.ClickEventArgs e)
        {
            mainForm.SendToExcel(ref ContractMarksGrid, false);
        }

        private void ContractMarksGrid_Filter(object sender, C1.Win.C1TrueDBGrid.FilterEventArgs e)
        {
            if (e.Condition.ToString().Equals(""))
            {
                dvContracts.RowFilter = dvContracts.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "'";
            }
            else
            {
                dvContracts.RowFilter = dvContracts.RowFilter = "BookGroup = '" + BookGroupCombo.Text + "' AND " + e.Condition.ToString();
            }
        }
	}
}