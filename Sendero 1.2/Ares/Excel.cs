
using System;
using System.IO;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Globalization;
using Anetics.Common;
using C1.C1Excel;

namespace Anetics.Ares
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

        public static string ExportDataSetToExcel_Worksheets(ref DataSet dsInformation, string[] tableName, string[] sheetName, int sheetCount, 
                                                             bool includeColumnHeaders, string fileName) 
        { 
            string filePathName = Standard.ConfigValue("TempPath", @"C:\Sendero\Temp\") + fileName + ".xls";
            C1XLBook book = new C1XLBook();

            try
            {
                for (int i = 0; i < sheetCount; i++)
                {
                    XLSheet sheet;

                    if (i.Equals(0))
                    {
                        sheet = book.Sheets[i];
                        sheet.Name = sheetName[i];
                    }
                    else
                    {
                        sheet = book.Sheets.Add(sheetName[i]);
                    }

                    //Create styles for cells
                    XLStyle styleDecimal = new XLStyle(book);
                    styleDecimal.Format = "#,##0.00";
                    styleDecimal.AlignHorz = XLAlignHorzEnum.Right;

                    XLStyle styleInteger = new XLStyle(book);
                    styleInteger.Format = "#,##0";
                    styleInteger.AlignHorz = XLAlignHorzEnum.Right;

                    XLStyle styleDateTime = new XLStyle(book);              
                    styleDateTime.Format = "MM/dd/yyyy HH:mm:ss";
                    styleDateTime.AlignHorz = XLAlignHorzEnum.Right;
                    
                    int workSheetRow = 0;
                    int workSheetCol = 0;

                    if (includeColumnHeaders)
                    {
                        foreach (DataColumn dataColumn in dsInformation.Tables[tableName[i]].Columns)   
                        {
                            XLCell cell = sheet[workSheetRow, workSheetCol];
                            cell.Value = dataColumn.Caption;
                            workSheetCol++;
                        }

                        workSheetCol = 0;
                        workSheetRow++;
                    }

                    foreach (DataRow row in dsInformation.Tables[tableName[i]].Rows)        
                    {
                        foreach (DataColumn dataColumn in dsInformation.Tables[tableName[i]].Columns)
                        {
                            if (row[dataColumn.Caption].GetType().Equals(typeof(System.Decimal))
                                //row[dataColumn.Caption].GetType().Equals(typeof(System.Single)) ||   
                                //row[dataColumn.Caption].GetType().Equals(typeof(System.Double)) ||    
                                )
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
                            else if (row[dataColumn.Caption].GetType().Equals(typeof(System.DateTime)))
                            {
                                XLCell cell = sheet[workSheetRow, workSheetCol];
                                cell.Value = row[dataColumn.Caption];
                                cell.Style = styleDateTime;
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


                } // for worksheet loop 

                book.Save(filePathName);	//worked as of 2010-07-12, in framework 2.0 
            }
            catch (Exception e)
            {
                Log.Write("Error: " + e.Message.Replace("\r\n", " ") + "  [Excel.ExportDataSetToExcel]", Log.Error, 1);
            }

            return filePathName;
        }

    }
}