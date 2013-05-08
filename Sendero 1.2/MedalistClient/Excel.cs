// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Globalization;
using Anetics.Common;
using C1.C1Excel;

namespace Anetics.Medalist
{
	public class Excel
	{	
		public Excel()
		{
		}
    
		
		public void ExportGridToExcel(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid exportGrid)
		{
			ExportGridToExcel(ref exportGrid, 0);
		}
		
		public void ExportGridToExcel(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid exportGrid, int split)
		{														
			try
			{
				C1XLBook book = new C1XLBook();
			
				XLSheet sheet =  book.Sheets[0];
			
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
				styleDecimal.Format  = "#,##0.00";
				styleDecimal.AlignHorz = XLAlignHorzEnum.Right;

				XLStyle styleInteger = new XLStyle(book);
				styleInteger.Format  = "#,##0";
				styleInteger.AlignHorz = XLAlignHorzEnum.Right;
			
				int workSheetRow = 0;
				int workSheetCol = 0;	
		
				bool visibleColumn = false;
				
				if (exportGrid.SelectedRows.Count == 0)
				{					
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in exportGrid.Columns)
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
							workSheetCol ++;
						}
						visibleColumn = false;
						
					}

					workSheetCol = 0;
					workSheetRow ++;

					visibleColumn = false;


					for (int row = 0; row < exportGrid.Splits[split].Rows.Count; row++)
					{
						foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in exportGrid.Columns)
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
								if (	dataColumn.CellValue(row).GetType().Equals(typeof(System.Decimal))) 
								{
									XLCell cell = sheet[workSheetRow, workSheetCol];
									cell.Value = dataColumn.CellValue(row);																			
									cell.Style = styleDecimal;
								}	
								else if (	dataColumn.CellValue(row).GetType().Equals(typeof(System.Int64)) ||
									dataColumn.CellValue(row).GetType().Equals(typeof(System.Int16)) ||
									dataColumn.CellValue(row).GetType().Equals(typeof(System.Int32)))
								{
									XLCell cell = sheet[workSheetRow, workSheetCol];
									cell.Value = dataColumn.CellValue(row);																			
									cell.Style = styleInteger;
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
						workSheetRow ++;					
					}
			
				}								
				else
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in (exportGrid.SelectedCols.Count > 0) ? exportGrid.SelectedCols : exportGrid.Columns)
					{					
						XLCell cell = sheet[workSheetRow, workSheetCol];
						cell.Value = dataColumn.Caption;																									
						workSheetCol ++;
					}

					workSheetCol = 0;
					workSheetRow ++;

					foreach (int row in exportGrid.SelectedRows)
					{
						foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in (exportGrid.SelectedCols.Count > 0) ? exportGrid.SelectedCols : exportGrid.Columns)
						{																										
							if (	dataColumn.CellValue(row).GetType().Equals(typeof(System.Decimal))) 
							{
								XLCell cell = sheet[workSheetRow, workSheetCol];
								cell.Value = dataColumn.CellValue(row);																			
								cell.Style = styleDecimal;
							}	
							else if (	dataColumn.CellValue(row).GetType().Equals(typeof(System.Int64)) ||
								dataColumn.CellValue(row).GetType().Equals(typeof(System.Int16)) ||
								dataColumn.CellValue(row).GetType().Equals(typeof(System.Int32)))
							{
								XLCell cell = sheet[workSheetRow, workSheetCol];
								cell.Value = dataColumn.CellValue(row);																			
								cell.Style = styleInteger;
							}
							else
							{
								XLCell cell = sheet[workSheetRow, workSheetCol];							
								cell.Value = dataColumn.CellValue(row).ToString();																							
							}
						
							workSheetCol++;
						}
					
						workSheetCol = 0;
						workSheetRow ++;					
					}			
				}

				AutoSizeColumns(book, sheet);
				
				string fileName = Standard.ConfigValue("TempPath", @"C:\Sendero\Temp\") + sheet.Name + "_" + Standard.ProcessId()+ ".xls";
				book.Save(fileName);
				System.Diagnostics.Process.Start(fileName);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Excel.ExportGridToExcel]", Log.Error, 1);				
			}
		}

		/*public void ExportGridToExcel(ref C1.Win.C1TrueDBGrid.C1TrueDBGrid exportGrid, int Split)
		{											
			
			try
			{
				C1XLBook book = new C1XLBook();
			
				XLSheet sheet =  book.Sheets[0];
			
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
				styleDecimal.Format  = "#,##0.00";
				styleDecimal.AlignHorz = XLAlignHorzEnum.Right;

				XLStyle styleInteger = new XLStyle(book);
				styleInteger.Format  = "#,##0";
				styleInteger.AlignHorz = XLAlignHorzEnum.Right;
			
				int workSheetRow = 0;
				int workSheetCol = 0;	
		
				
				// -- Added by Yasir Bashir
				// -- on 9/17/2007
				bool visibleColumn = false;
				// -- 

				if (exportGrid.SelectedRows.Count == 0) // If no rows were selected, select everything and export to excel.
				{					
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in exportGrid.Columns)
					{	
						// -- Added by Yasir Bashir
						// -- on 9/17/2007
						for (int columns = 0; columns < exportGrid.Splits[Split].DisplayColumns.Count; columns++)
						{
							if (exportGrid.Splits[Split].DisplayColumns[columns].DataColumn == dataColumn)
							{
								if (exportGrid.Splits[Split].DisplayColumns[columns].Visible == true)
								{
									visibleColumn = true;
								}
							}
						}
						if (visibleColumn)
						{
							// --
							XLCell cell = sheet[workSheetRow, workSheetCol];
							cell.Value = dataColumn.Caption;																									
							workSheetCol ++;
							// -- 
						}
						visibleColumn = false;
						// -- 
					}

					workSheetCol = 0;
					workSheetRow ++;

					// -- Added by Yasir Bashir
					// -- on 9/14/2007
					visibleColumn = false;



					for (int row = 0; row < exportGrid.Splits[Split].Rows.Count; row++)
					{
						foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in exportGrid.Columns)
						{	

							// -- Added by Yasir Bashir
							// -- on 9/17/2007
							for (int columns = 0; columns < exportGrid.Splits[Split].DisplayColumns.Count; columns++)
							{
								if (exportGrid.Splits[Split].DisplayColumns[columns].DataColumn == dataColumn)
								{
									if (exportGrid.Splits[Split].DisplayColumns[columns].Visible == true)
									{
										visibleColumn = true;
									}
								}
							}
							if (visibleColumn)
							{
								// -- 
								if (	dataColumn.CellValue(row).GetType().Equals(typeof(System.Decimal))) 
								{
									XLCell cell = sheet[workSheetRow, workSheetCol];
									cell.Value = dataColumn.CellValue(row);																			
									cell.Style = styleDecimal;
								}	
								else if (	dataColumn.CellValue(row).GetType().Equals(typeof(System.Int64)) ||
									dataColumn.CellValue(row).GetType().Equals(typeof(System.Int16)) ||
									dataColumn.CellValue(row).GetType().Equals(typeof(System.Int32)))
								{
									XLCell cell = sheet[workSheetRow, workSheetCol];
									cell.Value = dataColumn.CellValue(row);																			
									cell.Style = styleInteger;
								}
								else
								{
									XLCell cell = sheet[workSheetRow, workSheetCol];							
									cell.Value = dataColumn.CellValue(row).ToString();																							
								}
							
								workSheetCol++;
								// -- 
							}
							visibleColumn = false;
							// -- 
						}
					
						workSheetCol = 0;
						workSheetRow ++;					
					}
			
				}								
				else
				{
					foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in (exportGrid.SelectedCols.Count > 0) ? exportGrid.SelectedCols : exportGrid.Columns)
					{					
						XLCell cell = sheet[workSheetRow, workSheetCol];
						cell.Value = dataColumn.Caption;																									
						workSheetCol ++;
					}

					workSheetCol = 0;
					workSheetRow ++;

					foreach (int row in exportGrid.SelectedRows)
					{
						foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in (exportGrid.SelectedCols.Count > 0) ? exportGrid.SelectedCols : exportGrid.Columns)
						{																										
							if (	dataColumn.CellValue(row).GetType().Equals(typeof(System.Decimal))) 
							{
								XLCell cell = sheet[workSheetRow, workSheetCol];
								cell.Value = dataColumn.CellValue(row);																			
								cell.Style = styleDecimal;
							}	
							else if (	dataColumn.CellValue(row).GetType().Equals(typeof(System.Int64)) ||
								dataColumn.CellValue(row).GetType().Equals(typeof(System.Int16)) ||
								dataColumn.CellValue(row).GetType().Equals(typeof(System.Int32)))
							{
								XLCell cell = sheet[workSheetRow, workSheetCol];
								cell.Value = dataColumn.CellValue(row);																			
								cell.Style = styleInteger;
							}
							else
							{
								XLCell cell = sheet[workSheetRow, workSheetCol];							
								cell.Value = dataColumn.CellValue(row).ToString();																							
							}
						
							workSheetCol++;
						}
					
						workSheetCol = 0;
						workSheetRow ++;					
					}			
				}

				AutoSizeColumns(book, sheet);
				
				string fileName = Standard.ConfigValue("TempPath", @"C:\Sendero\Temp\") + sheet.Name + "_" + Standard.ProcessId()+ ".xls";
				book.Save(fileName);
				System.Diagnostics.Process.Start(fileName);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [Excel.ExportGridToExcel]", Log.Error, 1);				
			}
		}*/

		private void AutoSizeColumns(C1XLBook book, XLSheet sheet)
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