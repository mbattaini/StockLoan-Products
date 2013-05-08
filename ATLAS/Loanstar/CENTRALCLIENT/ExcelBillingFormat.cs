using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using C1.C1Excel;
using StockLoan.Common;
using StockLoan.MainBusiness;

namespace CentralClient
{
    class ExcelBillingFormat
    {
        const int HDR_Book = 0;
        const int HDR_ContractId = 1;
        const int HDR_SecId = 2;
        const int HDR_Symbol = 3;
        const int HDR_Type = 4;
        const int HDR_Quantity = 5;
        const int HDR_CurrencyIso = 6;
        const int HDR_FeeAmount = 7;
        const int HDR_RebateAmount = 8;
        const int HDR_TotalRebate = 9;

        const int BDY_TradeDate = 0;
        const int BDY_SecId = 1;
        const int BDY_Symbol = 2;
        const int BDY_StartDate = 3;
        const int BDY_StopDate = 4;
        const int BDY_Quantity = 5;
        const int BDY_BizDate = 6;
        const int BDY_Price = 7;
        const int BDY_Margin = 8;
        const int BDY_Amount = 9;
        const int BDY_FeeRate = 10;
        const int BDY_FeeAmount = 11;
        const int BDY_RebateRate = 12;
        const int BDY_RebateAmount = 13;
        const int BDY_PL = 14;

        const int TRL_TotalFeeAmount = 11;
        const int TRL_TotalRebateAmount = 13;
        const int TRL_TotalPL = 14;

        private C1XLBook excelBook;
        private int lineCounter = 0;
        private string fileName = "";
        private XLSheet xlSheet;

        public void ExcelBillingBookOpen(string fileName)
        {
            this.fileName = fileName;
            excelBook = new C1XLBook();
            xlSheet = excelBook.Sheets.Add();
            
            lineCounter = 0;
        }

        public void ExcelBillingBookSave()
        {
            AutoSizeColumns(excelBook, xlSheet); 
            excelBook.Sheets.RemoveAt(0);           
            excelBook.Save(fileName);
            Process.Start(fileName);
        }

        private XLStyle StylePicker(string dataField, object value)
        {
            XLStyle valueStyle = new XLStyle(excelBook);

            XLStyle xlStyleCollateral = new XLStyle(excelBook);            
            xlStyleCollateral.AlignHorz = XLAlignHorzEnum.Right;
            xlStyleCollateral.Format = Formats.Collateral;

            XLStyle xlStyleCash = new XLStyle(excelBook);
            xlStyleCash.AlignHorz = XLAlignHorzEnum.Right;
            xlStyleCash.Format = Formats.Cash;

            XLStyle xlStylePrice = new XLStyle(excelBook);
            xlStylePrice.AlignHorz = XLAlignHorzEnum.Right;
            xlStylePrice.Format = Formats.Price;

            XLStyle xlStyleRate = new XLStyle(excelBook);
            xlStyleRate.AlignHorz = XLAlignHorzEnum.Right;
            xlStyleRate.Format = Formats.Rate;

            XLStyle xlStyleMargin = new XLStyle(excelBook);
            xlStyleMargin.AlignHorz = XLAlignHorzEnum.Right;
            xlStyleMargin.Format = Formats.Margin;
            xlStyleMargin.ForeColor = System.Drawing.Color.Blue;


            switch (dataField)
            {
                case "Collateral":
                    if ((long)value < 0)
                    {
                        xlStyleCollateral.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        xlStyleCollateral.ForeColor = System.Drawing.Color.Blue;
                    }
                    valueStyle = xlStyleCollateral;
                    break;

                case "Cash":
                    if ((decimal)value < 0)
                    {
                        xlStyleCash.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        xlStyleCash.ForeColor = System.Drawing.Color.Blue;
                    }
                    valueStyle = xlStyleCash;
                    break;

                case "Price":
                    if ((decimal)value < 0)
                    {
                        xlStylePrice.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        xlStylePrice.ForeColor = System.Drawing.Color.Blue;
                    }
                    valueStyle = xlStyleCash;
                    break;

                case "Rate":
                    if ((decimal)value < 0)
                    {
                        xlStyleRate.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        xlStyleRate.ForeColor = System.Drawing.Color.Blue;
                    }
                    valueStyle = xlStyleRate;
                    break;

                case "Margin":
                    if ((decimal)value < 0)
                    {
                        xlStyleMargin.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        xlStyleMargin.ForeColor = System.Drawing.Color.Blue;
                    }
                    valueStyle = xlStyleMargin;
                    break;
            }

            return valueStyle;
        }

        public void ExcelBillingBookAddSheet(DataRow drHeader, ref C1.Win.C1TrueDBGrid.C1TrueDBGrid BillingRecordsGrid)
        {                       
            //---START HEADER                
            XLCell xlCell = xlSheet[lineCounter, HDR_Book];
            xlCell.Value = "Book";

            xlCell = xlSheet[lineCounter, HDR_ContractId];
            xlCell.Value = "Contarct ID";

            xlCell = xlSheet[lineCounter, HDR_SecId];
            xlCell.Value = "Security ID";

            xlCell = xlSheet[lineCounter, HDR_Symbol];
            xlCell.Value = "Symbol";

            xlCell = xlSheet[lineCounter, HDR_Type];
            xlCell.Value = "Type";

            xlCell = xlSheet[lineCounter, HDR_Quantity];
            xlCell.Value = "Quantity";

            xlCell = xlSheet[lineCounter, HDR_CurrencyIso];
            xlCell.Value = "Currency Iso";

            xlCell = xlSheet[lineCounter, HDR_FeeAmount];
            xlCell.Value = "Fee Amount";

            xlCell = xlSheet[lineCounter, HDR_RebateAmount];
            xlCell.Value = "Rebate Amount";

            xlCell = xlSheet[lineCounter, HDR_TotalRebate];
            xlCell.Value = "Total Rebate";
            
            lineCounter++;


            xlCell = xlSheet[lineCounter, HDR_Book];
            xlCell.Value = drHeader["Book"].ToString();

            xlCell = xlSheet[lineCounter, HDR_ContractId];
            xlCell.Value = drHeader["ContractId"].ToString();

            xlCell = xlSheet[lineCounter, HDR_SecId];
            xlCell.Value = drHeader["SecId"].ToString();

            xlCell = xlSheet[lineCounter, HDR_Symbol];
            xlCell.Value = drHeader["Symbol"].ToString();

            xlCell = xlSheet[lineCounter, HDR_Type];
            xlCell.Value = drHeader["ContractType"].ToString();

            xlCell = xlSheet[lineCounter, HDR_Quantity];
            xlCell.Style = StylePicker("Collateral", drHeader["Quantity"]);
            xlCell.Value = long.Parse(drHeader["Quantity"].ToString()).ToString(Formats.Collateral);

            xlCell = xlSheet[lineCounter, HDR_CurrencyIso];
            xlCell.Value = drHeader["CurrencyIso"].ToString();

            xlCell = xlSheet[lineCounter, HDR_FeeAmount];
            xlCell.Style = StylePicker("Cash", drHeader["FeeAmount"]);
            xlCell.Value = decimal.Parse(drHeader["FeeAmount"].ToString()).ToString(Formats.Cash); ;

            xlCell = xlSheet[lineCounter, HDR_RebateAmount];

            xlCell.Style = StylePicker("Cash", drHeader["RebateAmount"]);            
            xlCell.Value = decimal.Parse(drHeader["RebateAmount"].ToString()).ToString(Formats.Cash);

            xlCell = xlSheet[lineCounter, HDR_TotalRebate];
            xlCell.Style = StylePicker("Cash", drHeader["TotalRebate"]);  
            xlCell.Value = decimal.Parse(drHeader["TotalRebate"].ToString()).ToString(Formats.Cash);

            xlSheet.Name = drHeader["Book"].ToString() + " " + drHeader["CurrencyIso"].ToString();

            lineCounter += 3;
            //---END HEADER

            //---START BODY

            xlCell = xlSheet[lineCounter, BDY_TradeDate];
            xlCell.Value = "Book";

            xlCell = xlSheet[lineCounter, BDY_SecId];
            xlCell.Value = "Security ID";

            xlCell = xlSheet[lineCounter, BDY_Symbol];
            xlCell.Value = "Symbol";

            xlCell = xlSheet[lineCounter, BDY_StartDate];
            xlCell.Value = "Start Date";

            xlCell = xlSheet[lineCounter, BDY_StopDate];
            xlCell.Value = "Stop Date";

            xlCell = xlSheet[lineCounter, BDY_Quantity];
            xlCell.Value = "Quantity";

            xlCell = xlSheet[lineCounter, BDY_BizDate];
            xlCell.Value = "Date";

            xlCell = xlSheet[lineCounter, BDY_Price];
            xlCell.Value = "Price";

            xlCell = xlSheet[lineCounter, BDY_Margin];
            xlCell.Value = "Margin (%)";

            xlCell = xlSheet[lineCounter, BDY_Amount];
            xlCell.Value = "Amount";

            xlCell = xlSheet[lineCounter, BDY_FeeRate];
            xlCell.Value = "Fee Rate";

            xlCell = xlSheet[lineCounter, BDY_FeeAmount];
            xlCell.Value = "Fee Amount";

            xlCell = xlSheet[lineCounter, BDY_RebateRate];
            xlCell.Value = "Rebate Rate";

            xlCell = xlSheet[lineCounter, BDY_RebateAmount];
            xlCell.Value = "Rebate Amount";

            xlCell = xlSheet[lineCounter, BDY_PL];
            xlCell.Value = "Due";
            
            lineCounter++;

            for (int index = 0; index < BillingRecordsGrid.RowCount; index++)
            {

                xlCell = xlSheet[lineCounter, BDY_TradeDate];
                xlCell.Value = BillingRecordsGrid.Columns["ContractType"].CellText(index);

                xlCell = xlSheet[lineCounter, BDY_SecId];
                xlCell.Value = BillingRecordsGrid.Columns["SecId"].CellText(index);

                xlCell = xlSheet[lineCounter, BDY_Symbol];
                xlCell.Value = BillingRecordsGrid.Columns["Symbol"].CellText(index);

                xlCell = xlSheet[lineCounter, BDY_StartDate];
                xlCell.Value = BillingRecordsGrid.Columns["ValueDate"].CellText(index);

                xlCell = xlSheet[lineCounter, BDY_StopDate];
                xlCell.Value = BillingRecordsGrid.Columns["TermDate"].CellText(index);

                xlCell = xlSheet[lineCounter, BDY_Quantity];
                xlCell.Style = StylePicker("Collateral", BillingRecordsGrid.Columns["QuantitySettled"].CellValue(index));
                xlCell.Value = BillingRecordsGrid.Columns["QuantitySettled"].CellText(index);

                xlCell = xlSheet[lineCounter, BDY_BizDate];
                xlCell.Value = BillingRecordsGrid.Columns["BizDate"].CellText(index);

                xlCell = xlSheet[lineCounter, BDY_Price];
                xlCell.Style = StylePicker("Price", BillingRecordsGrid.Columns["Price"].CellValue(index));
                xlCell.Value = BillingRecordsGrid.Columns["Price"].CellText(index);


                xlCell = xlSheet[lineCounter, BDY_Margin];
                xlCell.Style = StylePicker("Margin", BillingRecordsGrid.Columns["Margin"].CellValue(index));
                xlCell.Value = BillingRecordsGrid.Columns["Margin"].CellText(index);

                xlCell = xlSheet[lineCounter, BDY_Amount];
                xlCell.Style = StylePicker("Cash", BillingRecordsGrid.Columns["AmountSettled"].CellValue(index));
                xlCell.Value = BillingRecordsGrid.Columns["AmountSettled"].CellText(index);

                xlCell = xlSheet[lineCounter, BDY_FeeRate];
                xlCell.Style = StylePicker("Rate", BillingRecordsGrid.Columns["FeeRate"].CellValue(index));
                xlCell.Value = BillingRecordsGrid.Columns["FeeRate"].CellText(index);

                xlCell = xlSheet[lineCounter, BDY_FeeAmount];
                xlCell.Style = StylePicker("Cash", BillingRecordsGrid.Columns["FeeAmount"].CellValue(index));           
                xlCell.Value = BillingRecordsGrid.Columns["FeeAmount"].CellText(index);

                xlCell = xlSheet[lineCounter, BDY_RebateRate];
                xlCell.Style = StylePicker("Rate", BillingRecordsGrid.Columns["RebateRate"].CellValue(index));           
                xlCell.Value = BillingRecordsGrid.Columns["RebateRate"].CellText(index);

                xlCell = xlSheet[lineCounter, BDY_RebateAmount];
                xlCell.Style = StylePicker("Cash", BillingRecordsGrid.Columns["RebateAmount"].CellValue(index));                           
                xlCell.Value = BillingRecordsGrid.Columns["RebateAmount"].CellText(index);

                xlCell = xlSheet[lineCounter, BDY_PL];
                xlCell.Style = StylePicker("Cash", BillingRecordsGrid.Columns["PL"].CellValue(index));                           
                xlCell.Value = BillingRecordsGrid.Columns["PL"].CellText(index);

                lineCounter++;
            }

            /*xlCell = xlSheet[lineCounter, TRL_TotalFeeAmount];
            xlCell.Style = StylePicker("Cash", decimal.Parse(BillingRecordsGrid.Columns["FeeAmount"].FooterText.Replace(",", "")));
            xlCell.Value = BillingRecordsGrid.Columns["FeeAmount"].FooterText;


            xlCell = xlSheet[lineCounter, TRL_TotalRebateAmount];
            xlCell.Style = StylePicker("Cash", decimal.Parse(BillingRecordsGrid.Columns["RebateAmount"].FooterText.Replace(",","")));
            xlCell.Value = BillingRecordsGrid.Columns["RebateAmount"].FooterText;*/


            xlCell = xlSheet[lineCounter, TRL_TotalPL];
            xlCell.Style = StylePicker("Cash", decimal.Parse(BillingRecordsGrid.Columns["PL"].FooterText.Replace(",", "")));
            xlCell.Value = BillingRecordsGrid.Columns["PL"].FooterText;

            lineCounter += 5;


                       
        }

        private static void AutoSizeColumns(C1XLBook book, XLSheet sheet)
        {
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                for (int c = 0; c < sheet.Columns.Count; c++)
                {
                    int colWidth = -1;
                    for (int r = 0; r < sheet.Rows.Count; r++)
                    {
                        object value = sheet[r, c].Value;
                        if (value != null)
                        {
                            // get value (unformatted at this point)
                            string text = value.ToString();

                            // format value if cell has a style with format set
                            C1.C1Excel.XLStyle s = sheet[r, c].Style;
                            if (s != null && s.Format.Length > 0 && value is IFormattable)
                            {
                                string fmt = XLStyle.FormatXLToDotNet(s.Format);
                                text = ((IFormattable)value).ToString(fmt, CultureInfo.CurrentCulture);
                            }

                            // get font (default or style)
                            Font font = book.DefaultFont;
                            if (s != null && s.Font != null)
                            {
                                font = s.Font;
                            }

                            // measure string (add a little tolerance)
                            Size sz = Size.Ceiling(g.MeasureString(text + "x", font));

                            // keep widest so far
                            if (sz.Width > colWidth)
                                colWidth = sz.Width;
                        }
                    }

                    // done measuring, set column width
                    if (colWidth > -1)
                        sheet.Columns[c].Width = C1XLBook.PixelsToTwips(colWidth);
                }
            }
        }
    }
}
