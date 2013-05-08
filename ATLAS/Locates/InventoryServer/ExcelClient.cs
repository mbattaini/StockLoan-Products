using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

using StockLoan.Common;

//using Microsoft.Office.Core;
//using Microsoft.Office.Interop.Excel;

namespace StockLoan.Inventory
{
    public class ExcelClient
    {
        //internal static Microsoft.Office.Interop.Excel.Application appExcel;
        internal static Mutex mtxImportExcel = new Mutex(false, "InventoryImportExcel");

        private static System.Data.DataTable sqlDataExcelSheet;
        private static ExcelReader readerExcel = null;

        /// <summary>
        /// Reads an Excel Workbook into a Tab-Delimited Text File of the Same Name (File.xls = File.txt)
        /// </summary>
        /// <param name="ImportSpecification"></param>
        /// <returns></returns>
        public static string[] ReadWorkbook(ImportSpec ImportSpecification)
        {
            return ReadWorkbook(ImportSpecification.LocalFilePath, true);
        }
        public static string[] ReadWorkbook(ImportSpec ImportSpecification, bool WriteOutputFile)
        {
            return ReadWorkbook(ImportSpecification.LocalFilePath, WriteOutputFile);
        }
        public static string[] ReadWorkbook(string WorkbookFilePath)
        {
            return ReadWorkbook(WorkbookFilePath, true);
        }
        public static string[] ReadWorkbook(string WorkbookFilePath, bool WriteOutputFile)
        {
            mtxImportExcel.WaitOne();

            string[] arrayStrReturnRows = null;
            ArrayList alStrReturnRows = new ArrayList();

            try
            {
                InitExcel(ref readerExcel, WorkbookFilePath);

                readerExcel.SheetName = "Sheet1";

                sqlDataExcelSheet = readerExcel.GetTable();

                readerExcel.Close();
                readerExcel.Dispose();
                readerExcel = null;

                foreach (System.Data.DataRow CurrentRow in sqlDataExcelSheet.Rows)
                {
                    int nNumCols = CurrentRow.ItemArray.Length;
                    int nRowIndex = 1;
                    string strRowText = "";
                    for (int iCol = 0; iCol < nNumCols; iCol++)
                    {
                        string strValue = "";
                        object objValue = CurrentRow.ItemArray[iCol];
                        string strTypeName = CurrentRow.ItemArray[iCol].GetType().ToString();

                        if ("System.DBNull" != strTypeName)
                        {
                            switch (strTypeName)
                            {
                                case "System.String":
                                    strValue = (string)objValue;
                                    break;
                                case "System.Double":
                                    double dblValue = (double)objValue;
                                    if (0 == (dblValue % 1))
                                            { strValue = (string)dblValue.ToString("F0"); }
                                    else    { strValue = (string)dblValue.ToString(); }
                                    break;
                                case "System.Integer":
                                    strValue = objValue.ToString();
                                    break;
                                default:
                                    strValue = (string)objValue;
                                    break;
                            }

                        }

                        strRowText += strValue;

                        if (nRowIndex < nNumCols)
                        {
                            strRowText += "\t";
                        }
                        nRowIndex++;
                    }

                    alStrReturnRows.Add(strRowText);
                }

                arrayStrReturnRows = (string[])alStrReturnRows.ToArray(typeof(string));

                if (WriteOutputFile)
                {
                    string strOutputFileLocation = CreateOutputTextFilePath(WorkbookFilePath);
                    System.IO.File.WriteAllLines(strOutputFileLocation, arrayStrReturnRows);
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
                Log.Write(ex.Message + " [ExcelClient.ReadWorkbook]", Log.Error, 1);
            }

            mtxImportExcel.ReleaseMutex();
            return arrayStrReturnRows;
        }

        private static void InitExcel(ref ExcelReader exr, string FilePath)
        {
            if (exr == null)
            {
                exr = new ExcelReader();
                exr.ExcelFilename = FilePath;
                exr.Headers = false;
                exr.MixedData = true;
            }
            if (sqlDataExcelSheet == null) sqlDataExcelSheet = new System.Data.DataTable("par");
            exr.KeepConnectionOpen = true;
        }

        public static string CreateOutputTextFilePath(string InputFilePath)
        {
            string strReturnOutputFileLocation = "";
            FileInfo infoInputFile = new FileInfo(InputFilePath);
            string strInputExtension = infoInputFile.Extension;

            strReturnOutputFileLocation = infoInputFile.FullName.Replace(strInputExtension, ".txt");
            return strReturnOutputFileLocation;
        }


        //public static string[] ReadWorkbook(string WorkbookFilePath, bool WriteOutputFile)
        //{
        //    mtxImportExcel.WaitOne();

        //    string[] arrayStrReturnRows = null;
        //    ArrayList alStrReturnRows = new ArrayList();

        //    try
        //    {
        //        appExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //        object m_objOpt = System.Reflection.Missing.Value;

        //        Microsoft.Office.Interop.Excel.Workbooks collectionWorkbooks = appExcel.Workbooks;
        //        Workbook workbookImportData = collectionWorkbooks.Open(WorkbookFilePath, 0, false, 5,
        //                                                                System.Reflection.Missing.Value, System.Reflection.Missing.Value, false,
        //                                                                System.Reflection.Missing.Value, System.Reflection.Missing.Value,
        //                                                                true, false, System.Reflection.Missing.Value, false, false, false
        //                                                               );
        //        Sheets xlsheets = workbookImportData.Sheets;
        //        Worksheet excelWorksheet = (Worksheet)xlsheets[1];
        //        Range rangeUsedCells = (Range)excelWorksheet.UsedRange;
        //        try
        //        {
        //            int nRows = rangeUsedCells.Rows.Count;

        //            foreach (Range ActiveRow in rangeUsedCells.Rows)
        //            {
        //                if (null != ActiveRow)
        //                {
        //                    int nNumCols = ActiveRow.Columns.Count;
        //                    int nRowIndex = 1;
        //                    string strRowText = "";
        //                    foreach (Range ActiveCell in ActiveRow.Cells)
        //                    {
        //                        if ((null != ActiveCell) && (null != ActiveCell.Value2))
        //                        {
        //                            strRowText += ActiveCell.Value2.ToString();
        //                        }
        //                        if (nRowIndex < nNumCols)
        //                        {
        //                            strRowText += "\t";
        //                        }
        //                        nRowIndex++;
        //                    }
        //                    alStrReturnRows.Add(strRowText);
        //                }
        //            }

        //            arrayStrReturnRows = (string[])alStrReturnRows.ToArray(typeof(string));

        //            if (WriteOutputFile)
        //            {
        //                string strOutputFileLocation = OutputFilePath(WorkbookFilePath);
        //                System.IO.File.WriteAllLines(strOutputFileLocation, arrayStrReturnRows);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            System.Console.Write(ex.Message);
        //            Log.Write(ex.Message + " [ExcelClient.ReadWorkbook]", Log.Error, 1);
        //        }
        //        finally
        //        {
        //            ReleaseReference(rangeUsedCells);
        //            ReleaseReference(excelWorksheet);
        //            ReleaseReference(xlsheets);

        //            workbookImportData.Close(false, m_objOpt, m_objOpt);
        //            ReleaseReference(workbookImportData);
        //            ReleaseReference(collectionWorkbooks);
        //            appExcel.Quit();
        //            ReleaseReference(appExcel);
        //            GC.Collect();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Console.Write(ex.Message);
        //        Log.Write(ex.Message + " [ExcelClient.ReadWorkbook]", Log.Error, 1);
        //    }

        //    mtxImportExcel.ReleaseMutex();
        //    return arrayStrReturnRows;
        //}

        //private static void ReleaseReference(object o)
        //{
        //    try
        //    {
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
        //    }
        //    catch { }
        //    finally
        //    {
        //        o = null;
        //    }
        //}

    }
}
