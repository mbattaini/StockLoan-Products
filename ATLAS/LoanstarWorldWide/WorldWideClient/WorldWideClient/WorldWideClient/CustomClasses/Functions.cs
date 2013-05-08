using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using C1.Silverlight.Data;
using C1.Silverlight.DataGrid;
using C1.Silverlight.DataGrid.Excel;
using C1.Silverlight.Pdf;
using C1.Silverlight.PdfViewer;

namespace WorldWideClient
{
	public class Functions
	{        
        public static int CreateControlHandle()
        {
            Random rndrm = new Random();
            return rndrm.Next(0, 35366);
        }

	   public static C1.Silverlight.Data.DataTable ConvertToDataTable(byte[] e, string tableName)
        {
            DataSet dsTemp = new DataSet();

            var ms = new System.IO.MemoryStream(e);
            dsTemp = new DataSet();
            dsTemp.ReadXml(ms);

            return dsTemp.Tables[tableName];
        }		
	}

    public class Export
    {
        public static void Excel (C1DataGrid _dataGrid)
        {
             var options = new ExcelSaveOptions()
            {
                FileFormat = ExcelFileFormat.Xlsx, // change this to offer a different Excel format
                KeepColumnWidths = true,
                KeepRowHeights = true
            };

            var excelExt = options.FileFormat.ToString();

            var dialog = new System.Windows.Controls.SaveFileDialog()
            {
                DefaultExt = "*." + excelExt,
                Filter = "Excel " + excelExt + " (*." + excelExt + ")|*." + excelExt  + "|All files (*.*)|*.*",
            };

            if (dialog.ShowDialog() == false) return;

            using (var stream = dialog.OpenFile())
            {
                _dataGrid.Save(stream, options);
            }
        }
		
		public static void Pdf(C1DataGrid _dataGrid)
		{
					  var dialog = new System.Windows.Controls.SaveFileDialog();

            dialog.DefaultExt = "*.pdf";
            dialog.Filter = "Portable document format (*.pdf)|*.pdf|All files (*.*)|*.*";

            if (dialog.ShowDialog() == false) return;

            using (var s = dialog.OpenFile())
            {
                var pdfDoc = new C1PdfDocument();
                pdfDoc.DocumentInfo.Title = "Sample DataGrid";
                _dataGrid.ToPdf(pdfDoc);
                pdfDoc.Save(s);
            }
		}
    }
	
	    public static class C1DataGridPdfExtension
    {
        public static C1PdfDocument ToPdf(this C1DataGrid grid)
        {
            return grid.ToPdf(null);
        }

        public static C1PdfDocument ToPdf(this C1DataGrid grid, C1PdfDocument pdf)
        {
            return grid.ToPdf(pdf, new Thickness(75), 2);
        }

        public static C1PdfDocument ToPdf(this C1DataGrid grid, C1PdfDocument pdf, Thickness pageMargins, double cellsMargins)
        {
            if (pdf == null)
            {
                pdf = new C1PdfDocument();
            }
            Rect rcPage =  PdfUtils.PageRectangle(pdf, pageMargins);
            if (rcPage.Width > 0 && rcPage.Height > 0)
            {
                Rect rc = rcPage;

                // title
                if (!string.IsNullOrEmpty(pdf.DocumentInfo.Title))
                {
                    // add title
                    Font titleFont = new Font("Tahoma", 24, PdfFontStyle.Bold);
                    rc = PdfUtils.RenderParagraph(pdf, pdf.DocumentInfo.Title, titleFont, rcPage, rc, false);
                }

                Font cellFont = new Font("Tahoma", 10);
                // body
                double[] columnsWidth, rowsHeight;
                double indentWidth = 8;
                MeasureCells(grid, pdf, ref cellFont, rcPage.Width - (grid.GroupedColumns.Length * indentWidth), cellsMargins, out rowsHeight, out columnsWidth);

                double rowOffset = rcPage.Top + rc.Height + cellsMargins;
                for (int i = 0; i <= grid.Rows.Count; i++)
                {
                    var row = i > 0 ? grid.Rows[i - 1] : null;

                    if (rowOffset + rowsHeight[i] > rcPage.Bottom)
                    {
                        pdf.NewPage();
                        rowOffset = rcPage.Top;
                        if (i > 0)
                        {
                            rcPage = RenderRow(grid, cellsMargins, pdf, rcPage, cellFont, columnsWidth, rowsHeight[0], rowOffset, indentWidth, null);
                            rowOffset += rowsHeight[0];
                        }
                    }
                    // Apply indentation

                    rcPage = RenderRow(grid, cellsMargins, pdf, rcPage, cellFont, columnsWidth, rowsHeight[i], rowOffset, indentWidth, row);
                    rowOffset += rowsHeight[i];
                }
            }
            return pdf;
        }

        private static Rect RenderRow(C1DataGrid grid, double margin, C1PdfDocument pdf, Rect rcPage, Font cellFont, double[] columnsWidth, double rowHeight, double rowOffset, double indentWidth, DataGridRow row)
        {
            var groupRow = row as DataGridGroupRow;
            if (groupRow != null)
            {
                // render group header
                double width = rcPage.Width - (indentWidth * groupRow.Level);
                double left = rcPage.Left + (indentWidth * groupRow.Level);
                Rect rcGroup = new Rect(left, rowOffset, width, rowHeight);
                rcGroup = PdfUtils.Inflate(rcGroup, -margin, -margin);
                pdf.DrawString(groupRow.GetGroupText(), cellFont, Colors.Black, rcGroup);
                pdf.DrawLine(new Pen(Colors.Black, 1), left, rowOffset + rowHeight - 1, rcPage.Right, rowOffset + rowHeight - 1);
            }
            else
            {
                double columnOffset = rcPage.Left;
                var orderedColumns = grid.Columns.OrderBy(column => column.DisplayIndex).ToList();
                for (int j = -1; j < orderedColumns.Count; j++)
                {
                    if (j >= 0)
                    {
                        // render cell
                        Rect rcCell = new Rect(columnOffset, rowOffset, columnsWidth[j], rowHeight);
                        var column = orderedColumns[j];
                        string text;
                        if (row == null)
                        {
                            pdf.FillRectangle(Colors.LightGray, rcCell);
                            text = column.GetColumnText();
                        }
                        else
                        {
                            pdf.DrawRectangle(Colors.LightGray, rcCell);
                            text = column.GetCellText(row);
                        }
                        rcCell = PdfUtils.Inflate(rcCell, -margin, -margin);
                        pdf.DrawString(text, cellFont, Colors.Black, rcCell, new StringFormat() { Alignment = column.HorizontalAlignment});
                        columnOffset += columnsWidth[j];
                    }
                    else
                    {
                        //render indent column
                        double indentColumnWidth = (indentWidth * grid.GroupedColumns.Length);
                        Rect rcCell = new Rect(columnOffset, rowOffset, indentColumnWidth, rowHeight);
                        if (row == null)
                        {
                            pdf.FillRectangle(Colors.LightGray, rcCell);
                        }
                        columnOffset += indentColumnWidth;
                    }
                }
            }
            return rcPage;
        }

        private static void MeasureCells(C1DataGrid grid, C1PdfDocument pdf, ref Font cellFont, double availableWidth, double margin, out double[] rowsHeight, out double[] colsWidth)
        {
            var orderedColumns = grid.Columns.OrderBy(column => column.DisplayIndex).ToList();
            rowsHeight = new double[grid.Rows.Count + 1];
            colsWidth = new double[orderedColumns.Count];
            rowsHeight.Initialize();
            colsWidth.Initialize();
            for (int i = 0; i <= grid.Rows.Count; i++)
            {
                var row = i > 0 ? grid.Rows[i - 1] : null;
                var groupRow = row as DataGridGroupRow;
                if (groupRow != null)
                {
                    var groupSize = pdf.MeasureString(groupRow.GetGroupText(), cellFont);
                    rowsHeight[i] = groupSize.Height + (margin * 2) + (0.4 * cellFont.Size);
                }
                else
                {
                    for (int j = 0; j < orderedColumns.Count; j++)
                    {
                        var column = orderedColumns[j];
                        //if is the column header row
                        string text;
                        if (row == null)
                        {
                            text = column.GetColumnText();
                        }
                        else
                        {
                            // gets and measure cell text
                            text = column.GetCellText(row);
                        }
                        var textSize = pdf.MeasureString(text, cellFont);
                        colsWidth[j] = Math.Max(textSize.Width + margin * 2, colsWidth[j]);
                        rowsHeight[i] = Math.Max(textSize.Height + (margin * 2) + (0.4 * cellFont.Size), rowsHeight[i]);
                        if (colsWidth.Sum() > availableWidth)
                        {
                            var newCellFont = ReduceSize(cellFont);
                            if (newCellFont.Size > 0)
                            {
                                cellFont = newCellFont;
                                MeasureCells(grid, pdf, ref cellFont, availableWidth, margin, out rowsHeight, out colsWidth);
                                goto a;
                            }
                        }
                    }
                }
            }a: ;
            //shares the remaining available width
            double remainingWidth = availableWidth - colsWidth.Sum();
            if (remainingWidth > 0)
            {
                double additionalWidth = remainingWidth / colsWidth.Length;
                for (int i = 0; i < colsWidth.Length; i++)
                {
                    colsWidth[i] += additionalWidth;
                }
            }
        }

        private static Font ReduceSize(Font cellFont)
        {
            return new Font(cellFont.Name, cellFont.Size - 1);
        }
    }
}


