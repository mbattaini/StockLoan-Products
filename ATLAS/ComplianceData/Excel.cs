
using System;
using System.IO;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Globalization;
using StockLoan.Common;
using C1.C1Excel;

namespace StockLoan.ComplianceData
{
    public class Excel
    {
        public Excel()
        {
        }

        public static string ExportDataSetToExcel(ref DataSet dsInformation, string tableName, string sheetName, bool includeColumnHeaders)
        {
            string fileName = "";

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

                fileName = Standard.ConfigValue("TempPath", @"C:\Sendero\Temp\") + sheet.Name + "_" + Standard.ProcessId() + ".xls";
                book.Save(fileName);	//DChen this worked as of 2010-07-12, in framework 2.0 
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [Excel.ExportDataSetToExcel]", Log.Error, 1);
            }

            return fileName;
        }
    }
}