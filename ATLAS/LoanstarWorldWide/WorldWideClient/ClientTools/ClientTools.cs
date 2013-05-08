using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Data;
using C1.Win.C1TrueDBGrid;
using C1.C1Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using StockLoan.Transport;

namespace StockLoan.ClientTools
{
    public class ClientTools
    {

        public static string GetIpAddress()
        {
            string hostName = System.Net.Dns.GetHostName();
            string currAddress = "";
            System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostName);
            System.Net.IPAddress[] addr = ipEntry.AddressList;

            for (int i = 0; i < addr.Length; i++)
            {
                currAddress = (addr[i].ToString());
            }
            return currAddress;
        }

        public static void SendDataTableToExcel(DataTable table, string fileName)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Workbooks.Add();

            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
            int ColumnIndex = 0;

            foreach (DataColumn col in table.Columns)
            {
                ColumnIndex++;
                excelApp.Cells[1, ColumnIndex] = col.ColumnName;
            }
            int rowIndex = 0;
            foreach (DataRow row in table.Rows)
            {
                rowIndex++;
                ColumnIndex = 0;
                foreach (DataColumn col in table.Columns)
                {
                    ColumnIndex++;
                    excelApp.Cells[rowIndex + 1, ColumnIndex] = row[col.ColumnName].ToString();
                    //excelApp.Cells[rowIndex + 1, ColumnIndex].Autosize();
                }
            }
            //excelApp.Save(fileName);
            
            Process.Start(fileName);

        }

        public static void SendDataGridViewToExcel(DataGridView dgResults)
        {

            Excel.Application excelApp = new Excel.Application();
            //excelApp.Visible = true; //BS; Don't show the Excel object 

            // Add has an optional parameter for specifying a praticular template. 
            excelApp.Workbooks.Add();

            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            int columnIndex = 0;
            foreach (DataGridViewColumn column in dgResults.Columns)
            {
                columnIndex++;
                excelApp.Cells[1, columnIndex] = column.HeaderText;
            }
            int rowIndex = 0;
            foreach (DataGridViewRow row in dgResults.Rows)
            {
                rowIndex++;
                columnIndex = 0;
                foreach (DataGridViewColumn column in dgResults.Columns)
                {
                    columnIndex++;
                    excelApp.Cells[rowIndex + 1, columnIndex] = row.Cells[column.Name].FormattedValue;
                }
            }
            excelApp.Visible = true;
            workSheet.Activate();

        }

        // BS; send text from a Component One True grid
        public static void SendC1GridToClipboard(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid)
        {
            string data = "";
            try
            {
                if (grid.Focused)
                {
                    data = GridToText(ref grid);
                }
                Clipboard.SetDataObject(data, true);
            }
            catch (Exception error)
            {
                throw new Exception("SendToClipboard Error");
            }
        }

        private static string GridToText(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid)
        {
            string gridData = "";

            try
            {
                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
                {
                    gridData += dataColumn.Caption + "\t";
                }

                gridData += "\r\n";

                foreach (int row in grid.SelectedRows)
                {
                    foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
                    {
                        gridData += dataColumn.CellText(row) + "\t";
                    }

                    gridData += "\r\n";
                }

                return gridData;
            }
            catch (Exception error)
            {
                throw new Exception("Error converting C1 Grid To Text " + error.Message);
            }
        }

        /// <summary>
        /// Send text to the system clipboard.
        /// </summary>
        public static void SendToClipboard(string data)
        {
            try
            {
                if ((data.Equals("")) || (data.Equals(null)))
                {
                    throw new Exception("No data selected");
                }

                Clipboard.SetDataObject(data, true);
            }
            catch (Exception er)
            {
                throw new Exception("MainForm.SendToClipboard");
            }
        }


        /// <summary>
        /// Returns the value '[Null]' if anyString is null or zero-length.
        /// </summary>
        public static string ZeroLengthNull(string anyString)
        {
          if ((anyString == null) || anyString.Length.Equals(0))
          {
            return "[Null]";
          }
          else
          {
            return anyString;
          }
        }

        /// <summary>
        /// Returns anyDate formatted per formatString or a zero-length string if anyDate is not a date.
        /// </summary>
        public static string FormatDate(string anyDate, string formatString)
        {
          return FormatDate(anyDate, formatString, 0);
        }

        /// <summary>
        /// Returns anyDate adjusted by offset minutes formatted per formatString or a zero-length string if anyDate is not a date.
        /// </summary>
        public static string FormatDate(string anyDate, string formatString, short offset)
        {
          try
          {
            return DateTime.Parse(anyDate).AddMinutes(offset).ToString(formatString);
          }
          catch
          {
            return "";
          }
        }

        /// <summary>
        /// Returns true if anyDate is a valid date.
        /// </summary>
        public static bool IsDate(string anyDate)
        {
          try
          {
            DateTime d = DateTime.Parse(anyDate);
            return true;
          }
          catch
          {
            return false;
          }
        }

        /// <summary>
        /// Returns true if anyValue is numerical.
        /// </summary>
        public static bool IsNumeric(string anyValue)
        {
          try
          {
            decimal.Parse(anyValue);
            return true;
          }
          catch
          {
            return false;
          }
        }

        /// <summary>
        /// Returns the item at index from itemSource where delimited by delimiter.
        /// </summary>
        public static string SplitItem(string itemSource, string delimiter, int index)
        {
          string [] item = null;

          if ((itemSource.Length.Equals(0)) || (delimiter.Length.Equals(0)) || (index < 0))
          {
            return "";
          }

          char [] delimiterChars = delimiter.ToCharArray();

          item = itemSource.Split(delimiterChars, index + 2);
      
          if (item.Length > index)
          {
            return item[index];
          }
          else
          {
            return "";
          }
        }

        /// <summary>
        /// Returns the long interger component of a numeric text value.
        /// </summary>
        public static long ParseLong(string anyNumeric)
        {
          try
          {
            return long.Parse(ClientTools.SplitItem(anyNumeric, ".", 0).Replace(",", ""));
          }
          catch
          {
            return 0;
          }
        }

        /// <summary>
        /// Returns a string of number formatted per format.
        /// </summary>
        public static string FormatPadNumber(string number, string format)
        {
            return FormatPadNumber(number, format, (short)format.Length);
        }           

        /// <summary>
        /// Returns a string of number formatted per format padded with blanks to length.
        /// </summary>
        public static string FormatPadNumber(string number, string format, short length)
        {
          decimal d;
          string result;

          try
          {        
            d = decimal.Parse(number);
            result = d.ToString(format).PadLeft(length, ' ');            
          }
          catch
          {
            return new String(' ', length);
          }
  
          if (result.Length > length)
          {
            return new String('*', length);
          }

          return result;
        }           
    
        /// <summary>
        /// Returns a string of text padded with blanks to length.
        /// </summary>        
        public static string FormatPadText(string text, short length)
        {
          return FormatPadText(text, ' ', length);
        }

        /// <summary>
        /// Returns a string of text padded with padCharacter to length.
        /// </summary>        
        public static string FormatPadText(string text, char padCharacter, short length)
        {
          string result;

          try
          {
            result = text.PadRight(length, padCharacter);
          }
          catch
          {
            return new String(' ', length);
          }
  
          return result.Substring(0, length);
        }

        public static void ExportGridToExcel(ref C1TrueDBGrid grid)
        {
            ExportGridToExcel(ref grid, "", 0, null);
        }

        public static void ExportGridToExcel(ref C1TrueDBGrid grid, int split)
        {
            ExportGridToExcel(ref grid, "", split, null);
        }

        public static void ExportGridToExcel(ref C1TrueDBGrid grid, string fileName, int split,
            Dictionary<string, ExcelCellStyle> excelCellStyleDictionary)
        {
            try
            {
                C1XLBook book = new C1XLBook();

                XLSheet sheet = book.Sheets[0];

                // Name the new worksheet
                if (!grid.Caption.Equals(""))
                {
                    sheet.Name = grid.Caption;
                }
                else if (!grid.Text.Equals(""))
                {
                    sheet.Name = grid.Text;
                }
                else
                {
                    sheet.Name = grid.Name;
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

                if (grid.SelectedRows.Count == 0)
                {
                    foreach (C1DataColumn dataColumn in grid.Columns)
                    {

                        for (int columns = 0; columns < grid.Splits[split].DisplayColumns.Count; columns++)
                        {
                            if (grid.Splits[split].DisplayColumns[columns].DataColumn.Equals(dataColumn))
                            {
                                if (grid.Splits[split].DisplayColumns[columns].Visible)
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


                    for (int row = 0; row < grid.Splits[split].Rows.Count; row++)
                    {
                        foreach (C1DataColumn dataColumn in grid.Columns)
                        {
                            for (int columns = 0; columns < grid.Splits[split].DisplayColumns.Count; columns++)
                            {
                                if (grid.Splits[split].DisplayColumns[columns].DataColumn.Equals(dataColumn))
                                {
                                    if (grid.Splits[split].DisplayColumns[columns].Visible)
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
                    foreach (C1DataColumn dataColumn in (grid.SelectedCols.Count > 0) ? grid.SelectedCols : grid.Columns)
                    {
                        XLCell cell = sheet[workSheetRow, workSheetCol];
                        cell.Value = dataColumn.Caption;
                        workSheetCol++;
                    }

                    workSheetCol = 0;
                    workSheetRow++;

                    foreach (int row in grid.SelectedRows)
                    {
                        foreach (C1DataColumn dataColumn in (grid.SelectedCols.Count > 0) ? grid.SelectedCols : grid.Columns)
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

                book.Save(fileName);
                Process.Start(fileName);
            }
            catch (Exception e)
            {
                //Log.Write(e.Message + " [Excel.ExportGridToExcel]", 1);
                throw;
            }
        }

        public static void AutoSizeColumns(C1XLBook book, XLSheet sheet)
        {	// Used in RebateBillingDocument class

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

        public static string SendDataSetToExcel(ref DataSet dsInformation, string tableName, string sheetName, string fileName, bool includeColumnHeaders)
        {
            try
            {
                C1XLBook book = new C1XLBook();
                XLSheet sheet = book.Sheets[0];
                sheet.Name = sheetName;

                //Create styles for cells
                XLStyle styleDecimal = new XLStyle(book);
                styleDecimal.Format = "#,##0.00";
                styleDecimal.AlignHorz = XLAlignHorzEnum.Right;

                XLStyle styleInteger = new XLStyle(book);
                styleInteger.Format = "#,##0";
                styleInteger.AlignHorz = XLAlignHorzEnum.Right;

                int workSheetRow = 0;
                int workSheetCol = 0;

                if (includeColumnHeaders)
                {
                    foreach (DataColumn dataColumn in dsInformation.Tables[tableName].Columns)
                    {
                        XLCell cell = sheet[workSheetRow, workSheetCol];
                        cell.Value = dataColumn.Caption;
                        workSheetCol++;
                    }

                    workSheetCol = 0;
                    workSheetRow++;
                }

                foreach (DataRow row in dsInformation.Tables[tableName].Rows)
                {
                    foreach (DataColumn dataColumn in dsInformation.Tables[tableName].Columns)
                    {
                        if (row[dataColumn.Caption].GetType().Equals(typeof(System.Decimal)))
                        {
                            XLCell cell = sheet[workSheetRow, workSheetCol];
                            cell.Value = row[dataColumn.Caption];
                            cell.Style = styleDecimal;
                        }
                        else if (row[dataColumn.Caption].GetType().Equals(typeof(System.Int64)) ||
                            row[dataColumn.Caption].GetType().Equals(typeof(System.Int16)) ||
                            row[dataColumn.Caption].GetType().Equals(typeof(System.Int32)))
                        {
                            XLCell cell = sheet[workSheetRow, workSheetCol];
                            cell.Value = row[dataColumn.Caption];
                            cell.Style = styleInteger;
                        }
                        else
                        {
                            XLCell cell = sheet[workSheetRow, workSheetCol];
                            cell.Value = row[dataColumn.Caption];
                        }

                        workSheetCol++;
                    }

                    workSheetCol = 0;
                    workSheetRow++;
                }

                //fileName = "C:\\AppDev\\StockLoan\\Temp\\C1ExportTest.xls"; //Standard.ConfigValue("TempPath", @"C:\Sendero\Temp\") + sheet.Name + "_" + Standard.ProcessId() + ".xls";
                book.Save(fileName);	// this worked as of 2010-07-12, in framework 2.0 
                Process.Start(fileName);
                
            }
            catch (Exception e)
            {
                throw new Exception("ExportDataSetToExcel failed " + e.Message);
            }

            return fileName;
        }

        public static void SendToEmail(string subject, string body, string fromAddress, string recipientList, string ccList, string attachmentList)
        {
            try
            {
                Transport.Transport.SendEMail(subject, body, fromAddress, recipientList, ccList, attachmentList);
            }
            catch (Exception error)
            {
                throw new Exception("SendToEmail Failed. Error = " + error.Message);
            }
        }

        public static void SendGridToEmail(C1.Win.C1TrueDBGrid.C1TrueDBGrid grid, string fromAddress, string recipientList, 
                string ccList, string attachmentList)
        {
            int textLength;
            int[] maxTextLength;
            int columnIndex = -1;
            string gridData = "\n\n";


            try
            {
                if (grid.SelectedCols.Count.Equals(0))
                {
                    MessageBox.Show("You have not selected any rows to send.", "Alert");
                    return;
                }

                if (fromAddress.Equals(""))
                {
                    MessageBox.Show("You must indicate who message is from.", "Alert");
                    return;
                }

                if (recipientList.Equals(""))
                {
                    MessageBox.Show("You must indicate who the message is addressed to.", "Alert");
                    return;
                }

                maxTextLength = new int[grid.SelectedCols.Count];

                // Get the caption length for each column.
                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
                {
                    maxTextLength[++columnIndex] = dataColumn.Caption.Trim().Length;
                }

                // Get the maximum item length for each row in each column.
                foreach (int rowIndex in grid.SelectedRows)
                {
                    columnIndex = -1;

                    foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
                    {
                        if ((textLength = dataColumn.CellText(rowIndex).Trim().Length) > maxTextLength[++columnIndex])
                        {
                            maxTextLength[columnIndex] = textLength;
                        }
                    }
                }

                columnIndex = -1;

                // Read grid caption 
                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
                {
                    gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
                }
                gridData += "\n";

                columnIndex = -1;

                // Create field dash lines with same width as max field length
                foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
                {
                    gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
                }
                gridData += "\n";

                // Read grid selected data 
                foreach (int rowIndex in grid.SelectedRows)
                {
                    columnIndex = -1;

                    foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
                    {
                        if (dataColumn.Value.GetType().Equals(typeof(System.String)))
                        {
                            //gridData += dataColumn.CellText(rowIndex).PadRight(maxTextLength[++columnIndex] + 2);			//original
                            gridData += dataColumn.CellText(rowIndex).Trim().PadRight(maxTextLength[++columnIndex] + 2);	//new
                        }
                        else
                        {
                            gridData += dataColumn.CellText(rowIndex).PadLeft(maxTextLength[++columnIndex]) + "  ";
                        }
                    }

                    gridData += "\n";
                }
                Transport.Transport.SendEMail(grid.Text.ToString() + " _ " + DateTime.Now.ToString("yyyy-MM-dd"), gridData, fromAddress, 
                        recipientList, ccList, attachmentList);

            }
            catch (Exception error)
            {
                throw new Exception("SendToEmail failed. Error Message = " + error.Message);
            }
        }

    }
}