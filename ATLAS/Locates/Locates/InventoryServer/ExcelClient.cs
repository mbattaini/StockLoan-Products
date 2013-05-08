using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

using StockLoan.Common;

using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;

namespace StockLoan.Inventory
{
    class ExcelClient
    {
        internal static Microsoft.Office.Interop.Excel.Application appExcel;
        public static Mutex mtxImportExcel = new Mutex(false, "InventoryImportExcel");
        

        /// <summary>
        /// Reads an Excel Workbook into a Tab-Delimited Text File of the Same Name (File.xls = File.txt)
        /// </summary>
        /// <param name="ImportSpecification"></param>
        /// <returns></returns>
        public static ArrayList ReadWorkbook(ImportSpec ImportSpecification)
        {
            mtxImportExcel.WaitOne();
            ArrayList alStrReturnRows = new ArrayList();

            try
            {
                appExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();
                object m_objOpt = System.Reflection.Missing.Value;

                Microsoft.Office.Interop.Excel.Workbooks collectionWorkbooks = appExcel.Workbooks;
                Workbook workbookImportData = collectionWorkbooks.Open(ImportSpecification.LocalFilePath, 0, false, 5,
                                                                                System.Reflection.Missing.Value, System.Reflection.Missing.Value, false,
                                                                                System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                                                                true, false, System.Reflection.Missing.Value, false, false, false
                                                                            );
                Sheets xlsheets = workbookImportData.Sheets;
                Worksheet excelWorksheet = (Worksheet)xlsheets[1];
                Range rangeUsedCells = (Range)excelWorksheet.UsedRange;
                try
                {
                    int nRows = rangeUsedCells.Rows.Count;

                    foreach (Range ActiveRow in rangeUsedCells.Rows)
                    {
                        if (null != ActiveRow)
                        {
                            int nNumCols = ActiveRow.Columns.Count;
                            int nRowIndex = 1;
                            string strRowText = "";
                            foreach (Range ActiveCell in ActiveRow.Cells)
                            {
                                if ((null != ActiveCell) && (null != ActiveCell.Value2))
                                {
                                    strRowText += ActiveCell.Value2.ToString();
                                }
                                if (nRowIndex < nNumCols)
                                {
                                    strRowText += "\t";
                                }
                                nRowIndex++;
                            }
                            alStrReturnRows.Add(strRowText);
                        }
                    }

                    FileInfo infoInputFile = new FileInfo(ImportSpecification.LocalFilePath);
                    string strInputExtension = infoInputFile.Extension;

                    string strOutputFileLocation = infoInputFile.FullName.Replace(strInputExtension, ".txt");
                    System.IO.File.WriteAllLines(strOutputFileLocation, (string[])alStrReturnRows.ToArray(typeof(string)));




                }
                catch (Exception ex)
                {
                    System.Console.Write(ex.Message);
                    Log.Write(ex.Message + " [ExcelClient.ReadWorkbook]", Log.Error, 1);
                }
                finally
                {
                    ReleaseReference(rangeUsedCells);
                    ReleaseReference(excelWorksheet);
                    ReleaseReference(xlsheets);


                    workbookImportData.Close(false, m_objOpt, m_objOpt);
                    ReleaseReference(workbookImportData);
                    ReleaseReference(collectionWorkbooks);
                    appExcel.Quit();
                    ReleaseReference(appExcel);
                    GC.Collect();
                }

            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
                Log.Write(ex.Message + " [ExcelClient.ReadWorkbook]", Log.Error, 1);
            }
            
            mtxImportExcel.ReleaseMutex();
            return alStrReturnRows;

        }


        private static void ReleaseReference(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch { }
            finally
            {
                o = null;
            }
        }



    }
}
//Excel.Range excelCell = (Excel.Range)excelWorksheet  //get_Range("B4:FZ4", Type.Missing); //Select a range of cells
//Excel.Range excelCell = (Excel.Range)excelWorksheet.get_Range("B4:FZ4", Type.Missing); //Select a range of cells
//Excel.Range excelCell2 = (Excel.Range)excelWorksheet.get_Range("A5:A5", Type.Missing); //Select a single cell
//Console.WriteLine(excelCell2.Cells.Value2.ToString()); //Print the value of the cell for a single cell selection
