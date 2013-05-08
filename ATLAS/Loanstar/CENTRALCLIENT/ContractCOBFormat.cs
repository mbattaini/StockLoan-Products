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
    class ContractCOBFormat
    {
        const int HDR_BookGroup = 0;
        const int HDR_Type = 1;
        const int HDR_BookName = 2; 
        const int HDR_SecId = 3;        
        const int HDR_ISIN = 4;
        const int HDR_Symbol = 5;
        const int HDR_Quantity = 6;
        const int HDR_Amount = 7;
        const int HDR_CurrencyIso = 8;
        const int HDR_SettleDate = 9;
        const int HDR_PoolCode = 10;

        private C1XLBook excelBook;
        private int lineCounter = 0;
        private string fileName = "";
        private string bizDate = "";

        private XLSheet xlSheet;


        public void ExcelBillingBookOpen(string fileName, string bizDate)
        {
            this.fileName = fileName;
            excelBook = new C1XLBook();
            this.bizDate = bizDate;
        }

        public long ContractCurrencyCount(string bookGroup, string currencyIso, DataSet dsContracts)
        {
            long count = 0;

            for (int index = 0; index < dsContracts.Tables["Contracts"].Rows.Count; index++)
            {
                if ((dsContracts.Tables["Contracts"].Rows[index]["CurrencyIso"].ToString().Equals(currencyIso)))
                {
                    count++;
                }
            }

            return count;
        }


        public void CloseOfBusinessGenerate(string bookGroup, string bizDate, DataSet dsCurrencies, DataSet dsContracts)
        {
            foreach (DataRow drTemp in dsCurrencies.Tables["Currencies"].Rows)
            {
                if (ContractCurrencyCount(bookGroup, drTemp["CurrencyCode"].ToString(), dsContracts) > 0)
                {
                    ExcelBillingBookAddSheet(bookGroup, drTemp["CurrencyCode"].ToString(), bizDate,  dsContracts); 
                }
            }
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

        public void ExcelBillingBookAddSheet(string bookGroup, string currencyIso, string bizDate, DataSet dsContracts)
        {
            decimal amount = 0;
            
            lineCounter = 0;

            xlSheet = excelBook.Sheets.Add();

            //---START HEADER                
            XLCell xlCell = xlSheet[lineCounter, HDR_BookGroup];
            xlCell.Value = "Book Group";

            xlCell = xlSheet[lineCounter, HDR_Type];
            xlCell.Value = "Type";

            xlCell = xlSheet[lineCounter, HDR_BookName];
            xlCell.Value = "Book";           

            xlCell = xlSheet[lineCounter, HDR_SecId];
            xlCell.Value = "Security ID";

            xlCell = xlSheet[lineCounter, HDR_ISIN];
            xlCell.Value = "ISIN";

            xlCell = xlSheet[lineCounter, HDR_Symbol];
            xlCell.Value = "Symbol";

            xlCell = xlSheet[lineCounter, HDR_Quantity];
            xlCell.Value = "Quantity";

            xlCell = xlSheet[lineCounter, HDR_Amount];
            xlCell.Value = "Amount";

            xlCell = xlSheet[lineCounter, HDR_CurrencyIso];
            xlCell.Value = "Currency ISO";

            xlCell = xlSheet[lineCounter, HDR_SettleDate];
            xlCell.Value = "Settle Date";

            xlCell = xlSheet[lineCounter, HDR_PoolCode];
            xlCell.Value = "Pool Code";


            lineCounter++;

            xlSheet.Name = currencyIso;

            //---END HEADER

            //---START BODY

            for (int index = 0; index < dsContracts.Tables["Contracts"].Rows.Count; index++)
            {
				

               if ((dsContracts.Tables["Contracts"].Rows[index]["CurrencyIso"].ToString().Equals(currencyIso)) &&                    
                    (dsContracts.Tables["Contracts"].Rows[index]["ContractType"].ToString().Equals("B")) &&
                    (dsContracts.Tables["Contracts"].Rows[index]["IsActive"].Equals(true)) &&
                    ((DateTime.Parse(dsContracts.Tables["Contracts"].Rows[index]["SettleDate"].ToString()) <= (DateTime.Parse(bizDate)))))
                {
                    xlCell = xlSheet[lineCounter, HDR_BookGroup];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["BookGroup"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_Type];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["ContractType"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_BookName];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["BookName"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_SecId];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["SecId"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_ISIN];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["ISIN"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_Symbol];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["Symbol"].ToString();

                    try
                    {
                        xlCell = xlSheet[lineCounter, HDR_Quantity];
                        xlCell.Style = StylePicker("Quantity", dsContracts.Tables["Contracts"].Rows[index]["Quantity"]);
                        xlCell.Value = long.Parse(dsContracts.Tables["Contracts"].Rows[index]["Quantity"].ToString());
                    }
                    catch { }


                    try
                    {
                        xlCell = xlSheet[lineCounter, HDR_Amount];
                        xlCell.Style = StylePicker("Cash", ((decimal)dsContracts.Tables["Contracts"].Rows[index]["Amount"] * -1));
                        xlCell.Value = decimal.Parse("-" + dsContracts.Tables["Contracts"].Rows[index]["Amount"].ToString());
                    }
                    catch 
                    {
                        Console.WriteLine(dsContracts.Tables["Contracts"].Rows[index]["ContractId"].ToString());
                        Console.WriteLine(dsContracts.Tables["Contracts"].Rows[index]["Amount"].ToString());
                    }

                    xlCell = xlSheet[lineCounter, HDR_CurrencyIso];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["CurrencyIso"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_SettleDate];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["SettleDate"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_PoolCode];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["PoolCode"].ToString();

                    amount += decimal.Parse("-" + dsContracts.Tables["Contracts"].Rows[index]["Amount"].ToString());
                    lineCounter++;
                }
            }
            
            xlCell = xlSheet[lineCounter, HDR_Amount];
            xlCell.Style = StylePicker("Cash", amount);
            xlCell.Value = amount;

            lineCounter += 2;
            amount = 0;

            for (int index = 0; index < dsContracts.Tables["Contracts"].Rows.Count; index++)
            {
                if ((dsContracts.Tables["Contracts"].Rows[index]["CurrencyIso"].ToString().Equals(currencyIso)) &&             
                    (dsContracts.Tables["Contracts"].Rows[index]["ContractType"].ToString().Equals("L")) &&
					(dsContracts.Tables["Contracts"].Rows[index]["IsActive"].Equals(true)) &&
                      ((DateTime.Parse(dsContracts.Tables["Contracts"].Rows[index]["SettleDate"].ToString()) <= (DateTime.Parse(bizDate)))))
                {
                    xlCell = xlSheet[lineCounter, HDR_BookGroup];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["BookGroup"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_Type];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["ContractType"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_BookName];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["BookName"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_SecId];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["SecId"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_ISIN];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["ISIN"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_Symbol];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["Symbol"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_Quantity];
                    xlCell.Style = StylePicker("Quantity", dsContracts.Tables["Contracts"].Rows[index]["Quantity"]);
                    xlCell.Value = long.Parse(dsContracts.Tables["Contracts"].Rows[index]["Quantity"].ToString());

                    xlCell = xlSheet[lineCounter, HDR_Amount];
                    xlCell.Style = StylePicker("Cash", dsContracts.Tables["Contracts"].Rows[index]["Amount"]);
                    xlCell.Value = decimal.Parse(dsContracts.Tables["Contracts"].Rows[index]["Amount"].ToString());

                    xlCell = xlSheet[lineCounter, HDR_CurrencyIso];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["CurrencyIso"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_SettleDate];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["SettleDate"].ToString();

                    xlCell = xlSheet[lineCounter, HDR_PoolCode];
                    xlCell.Value = dsContracts.Tables["Contracts"].Rows[index]["PoolCode"].ToString();

                    amount += decimal.Parse(dsContracts.Tables["Contracts"].Rows[index]["Amount"].ToString());
                    lineCounter++;
                }
            }

            xlCell = xlSheet[lineCounter, HDR_Amount];
            xlCell.Style = StylePicker("Cash", amount);
            xlCell.Value = amount;


            AutoSizeColumns(excelBook, xlSheet);
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
