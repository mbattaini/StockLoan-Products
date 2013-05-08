using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using C1.Silverlight.Pdf;
using System.IO;

namespace StockLoan_LocatesClient
{
    /// <summary>
    /// Utility class on top of C1.Silverlight.Pdf.
    /// </summary>
    public static class PdfUtils
    {
        // ***********************************************************************
        // C1PdfDocuments for Rect
        // ***********************************************************************


        // measure a paragraph, skip a page if it won't fit, render it into a rectangle,
        // and update the rectangle for the next paragraph.
        // 
        // optionally mark the paragraph as an outline entry and as a link target.
        //
        // this routine will not break a paragraph across pages. for that, see the Text Flow sample.
        //
        public static Rect RenderParagraph(this C1PdfDocument pdf, string text, Font font, Rect rcPage, Rect rc, bool outline, bool linkTarget)
        {
            // if it won't fit this page, do a page break
            rc.Height = pdf.MeasureString(text, font, rc.Width).Height;
            if (rc.Bottom > rcPage.Bottom)
            {
                pdf.NewPage();
                rc.Y = rcPage.Top;
            }

            // draw the string
            pdf.DrawString(text, font, Colors.Black, rc);

            // show bounds (to check word wrapping)
            //var p = Pen.GetPen(Colors.Orange);
            //pdf.DrawRectangle(p, rc);

            // add headings to outline
            if (outline)
            {
                pdf.DrawLine(Colors.Black, rc.X, rc.Y, rc.Right, rc.Y);
                pdf.AddBookmark(text, 0, rc.Y);
            }

            // add link target
            if (linkTarget)
            {
                pdf.AddTarget(text, rc);
            }

            // update rectangle for next time
            rc = Offset(rc, 0, rc.Height);
            return rc;
        }

        public static Rect RenderParagraph(this C1PdfDocument doc, string text, Font font, Rect rcPage, Rect rc, bool outline)
        {
            return RenderParagraph(doc, text, font, rcPage, rc, outline, false);
        }

        public static Rect RenderParagraph(this C1PdfDocument doc, string text, Font font, Rect rcPage, Rect rc)
        {
            return RenderParagraph(doc, text, font, rcPage, rc, false, false);
        }

        public static Rect PageRectangle(this C1PdfDocument pdf)
        {
            return PageRectangle(pdf, new Thickness(72));
        }

        public static Rect PageRectangle(this C1PdfDocument pdf, Thickness pageMargins)
        {
            Rect rc = pdf.PageRectangle;
            double left = Math.Min(rc.Width, rc.Left + pageMargins.Left);
            double top = Math.Min(rc.Height, rc.Top + pageMargins.Top);
            double width = Math.Max(0, rc.Width - (pageMargins.Left + pageMargins.Right));
            double height = Math.Max(0, rc.Height - (pageMargins.Top + pageMargins.Bottom));
            return new Rect(left, top, width, height);
        }

        public static void Save(this C1PdfDocument pdf) 
        {
            var dlg = new SaveFileDialog();
            dlg.DefaultExt = ".pdf";
            var dr = dlg.ShowDialog();
            if (!dr.HasValue || !dr.Value)
            {
                return;
            }

            using (var stream = dlg.OpenFile())
            {
                pdf.Save(stream);
            }
        }



        // ***********************************************************************
        // Extension methods for Rect
        // ***********************************************************************

        public static Rect Inflate(this Rect rc, double dx, double dy)
        {
            rc.X -= dx;
            rc.Y -= dy;
            rc.Width += 2 * dx;
            rc.Height += 2 * dy;
            return rc;
        }

        public static Rect Offset(this Rect rc, double dx, double dy)
        {
            rc.X += dx;
            rc.Y += dy;
            return rc;
        }

    }
}
