using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using C1.C1Excel;
using C1.Win.C1TrueDBGrid;
using StockLoan.Common;

namespace LocatesClient
{
    public class Excel
    {
        public Excel()
        {
        }

        public static void ExportGridToExcel(ref C1TrueDBGrid exportGrid)
        {
            ExportGridToExcel(ref exportGrid, "", 0, null);
        }

        public static void ExportGridToExcel(ref C1TrueDBGrid exportGrid, int split)
        {
            ExportGridToExcel(ref exportGrid, "", split, null);
        }

        public static void ExportGridToExcel(
            ref C1TrueDBGrid exportGrid, 
			string fileName,
            int split,
            Dictionary<string, ExcelCellStyle> excelCellStyleDictionary)
        {
            try
            {
                C1XLBook book = new C1XLBook();

                XLSheet sheet = book.Sheets[0];

                // Name the new worksheet
                if (!exportGrid.Caption.Equals(""))
                {
                    sheet.Name = exportGrid.Caption;
                }
                else if (!exportGrid.Text.Equals(""))
                {
                    sheet.Name = exportGrid.Text;
                }
                else
                {
                    sheet.Name = exportGrid.Name;
                }

                //Create styles for cells
                XLStyle styleDecimal = new XLStyle(book);
                styleDecimal.Format = "#,##0.00";
                styleDecimal.AlignHorz = XLAlignHorzEnum.Right;

                XLStyle styleInteger = new XLStyle(book);
                styleInteger.Format = "#,##0";
                styleInteger.AlignHorz = XLAlignHorzEnum.Right;

                int workSheetRow = 0;
                int workSheetCol = 0;

                bool visibleColumn = false;

                if (exportGrid.SelectedRows.Count == 0)
                {
                    foreach (C1DataColumn dataColumn in exportGrid.Columns)
                    {

                        for (int columns = 0; columns < exportGrid.Splits[split].DisplayColumns.Count; columns++)
                        {
                            if (exportGrid.Splits[split].DisplayColumns[columns].DataColumn == dataColumn)
                            {
                                if (exportGrid.Splits[split].DisplayColumns[columns].Visible == true)
                                {
                                    visibleColumn = true;
                                }
                            }
                        }
                        if (visibleColumn)
                        {
                            XLCell cell = sheet[workSheetRow, workSheetCol];
                            cell.Value = dataColumn.Caption;
                            workSheetCol++;
                        }
                        visibleColumn = false;

                    }

                    workSheetCol = 0;
                    workSheetRow++;

                    visibleColumn = false;


                    for (int row = 0; row < exportGrid.Splits[split].Rows.Count; row++)
                    {
                        foreach (C1DataColumn dataColumn in exportGrid.Columns)
                        {
                            for (int columns = 0; columns < exportGrid.Splits[split].DisplayColumns.Count; columns++)
                            {
                                if (exportGrid.Splits[split].DisplayColumns[columns].DataColumn == dataColumn)
                                {
                                    if (exportGrid.Splits[split].DisplayColumns[columns].Visible == true)
                                    {
                                        visibleColumn = true;
                                    }
                                }
                            }
                            if (visibleColumn)
                            {
                                if (dataColumn.CellValue(row).GetType().Equals(typeof(decimal)))
                                {
                                    XLCell cell = sheet[workSheetRow, workSheetCol];
                                    cell.Value = dataColumn.CellValue(row);
                                    if (excelCellStyleDictionary != null && 
                                        excelCellStyleDictionary[dataColumn.DataField].DataType.Equals(typeof(decimal)))
                                    {
                                        XLStyle style = new XLStyle(book);
                                        style.AlignHorz = XLAlignHorzEnum.Right;
                                        style.Format = excelCellStyleDictionary[dataColumn.DataField].StringFormat;
                                        cell.Style = style;
                                    }
                                    else
                                    {
                                        cell.Style = styleDecimal;
                                    }
                                }
                                else if (dataColumn.CellValue(row).GetType().Equals(typeof(System.Int64)) ||
                                    dataColumn.CellValue(row).GetType().Equals(typeof(System.Int16)) ||
                                    dataColumn.CellValue(row).GetType().Equals(typeof(System.Int32)))
                                {
                                    XLCell cell = sheet[workSheetRow, workSheetCol];
                                    cell.Value = dataColumn.CellValue(row);
                                    if (excelCellStyleDictionary != null && (
                                        excelCellStyleDictionary[dataColumn.DataField].DataType.Equals(typeof(System.Int16)) ||
                                        excelCellStyleDictionary[dataColumn.DataField].DataType.Equals(typeof(System.Int32)) ||
                                        excelCellStyleDictionary[dataColumn.DataField].DataType.Equals(typeof(System.Int64))
                                        ))
                                    {
                                        XLStyle style = new XLStyle(book);
                                        style.AlignHorz = XLAlignHorzEnum.Right;
                                        style.Format = excelCellStyleDictionary[dataColumn.DataField].StringFormat;
                                        cell.Style = style;
                                    }
                                    else
                                    {
                                        cell.Style = styleInteger;
                                    }
                                }
                                else
                                {
                                    XLCell cell = sheet[workSheetRow, workSheetCol];
                                    cell.Value = dataColumn.CellValue(row).ToString();
                                }

                                workSheetCol++;
                            }

                            visibleColumn = false;
                        }

                        workSheetCol = 0;
                        workSheetRow++;
                    }

                }
                else
                {
                    foreach (C1DataColumn dataColumn in (exportGrid.SelectedCols.Count > 0) ? exportGrid.SelectedCols : exportGrid.Columns)
                    {
                        XLCell cell = sheet[workSheetRow, workSheetCol];
                        cell.Value = dataColumn.Caption;
                        workSheetCol++;
                    }

                    workSheetCol = 0;
                    workSheetRow++;

                    foreach (int row in exportGrid.SelectedRows)
                    {
                        foreach (C1DataColumn dataColumn in (exportGrid.SelectedCols.Count > 0) ? exportGrid.SelectedCols : exportGrid.Columns)
                        {
                            if (dataColumn.CellValue(row).GetType().Equals(typeof(decimal)))
                            {
                                XLCell cell = sheet[workSheetRow, workSheetCol];
                                cell.Value = dataColumn.CellValue(row);
                                if (excelCellStyleDictionary != null && 
                                    excelCellStyleDictionary[dataColumn.DataField].DataType.Equals(typeof(decimal)))
                                {
                                    XLStyle style = new XLStyle(book);
                                    style.AlignHorz = XLAlignHorzEnum.Right;
                                    style.Format = excelCellStyleDictionary[dataColumn.DataField].StringFormat;
                                    cell.Style = style;
                                }
                                else
                                {
                                    cell.Style = styleDecimal;
                                }
                            }
                            else if (dataColumn.CellValue(row).GetType().Equals(typeof(System.Int64)) ||
                                dataColumn.CellValue(row).GetType().Equals(typeof(System.Int16)) ||
                                dataColumn.CellValue(row).GetType().Equals(typeof(System.Int32)))
                            {
                                XLCell cell = sheet[workSheetRow, workSheetCol];
                                cell.Value = dataColumn.CellValue(row);
                                if (excelCellStyleDictionary != null && (
                                    excelCellStyleDictionary[dataColumn.DataField].DataType.Equals(typeof(System.Int16)) ||
                                    excelCellStyleDictionary[dataColumn.DataField].DataType.Equals(typeof(System.Int32)) ||
                                    excelCellStyleDictionary[dataColumn.DataField].DataType.Equals(typeof(System.Int64))
                                    ))
                                {
                                    XLStyle style = new XLStyle(book);
                                    style.AlignHorz = XLAlignHorzEnum.Right;
                                    style.Format = excelCellStyleDictionary[dataColumn.DataField].StringFormat;
                                    cell.Style = style;
                                }
                                else
                                {
                                    cell.Style = styleInteger;
                                }
                            }
                            else
                            {
                                XLCell cell = sheet[workSheetRow, workSheetCol];
                                cell.Value = dataColumn.CellValue(row).ToString();
                            }

                            workSheetCol++;
                        }

                        workSheetCol = 0;
                        workSheetRow++;
                    }
                }

                AutoSizeColumns(book, sheet);

                fileName = fileName + ".xls";
                book.Save(fileName);
                Process.Start(fileName);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [Excel.ExportGridToExcel]", 1);
            }
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