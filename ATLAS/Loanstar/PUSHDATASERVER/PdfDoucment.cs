using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using C1.C1Pdf;
using StockLoan.Common;

namespace StockLoan.PushData
{
	class PdfDoucment
	{
		public static int LINES_PER_PAGE = 50;
		public static int MAX_CHARS_LINE = 143;
		public static char LINE_DELIMITER = '#';

		private C1PdfDocument pdfDocument;

		public PdfDoucment()
		{
		}

		public void Create(string filePath, string reportName, string header, string body)
		{
			string line = "";

			pdfDocument = new C1PdfDocument(System.Drawing.Printing.PaperKind.Custom, true);
			Font font = new Font("Courier New", 9, FontStyle.Regular);

			WriteParagraph(body);
			AddHeaders(reportName);
			pdfDocument.Save(filePath);
		}

		private void WriteParagraph(string body)
		{
			int index = 0;
			string tempString = "";
			string bodyString = "";
			ArrayList sentencesList = new ArrayList();

			RectangleF rPage = GetPageRect();
			RectangleF rc = rPage;

			Font font = new Font("Courier New", 9, FontStyle.Regular);

			while (true)
			{
				tempString = Tools.SplitItem(body, "#", index);

				if (tempString.Equals(""))
				{
					break;
				}
				else
				{
					sentencesList.Add(tempString);
					index++;
				}
			}

			for (int counter = 0; counter < sentencesList.Count; counter++)
			{
				bodyString = (string)sentencesList[counter];
				pdfDocument.DrawString(bodyString, font, Brushes.Black, rc);
				rc.Y += (font.GetHeight() + 2);

				if (rc.Y >= rPage.Bottom)
				{
					rc.Y = rPage.Y;
					pdfDocument.NewPage();
				}
			}
		}


		private void AddHeaders(string reportName)
		{
			Font fontHorz = new Font("Courier New", 8, FontStyle.Bold);
			Font fontVert = new Font("Courier New", 8, FontStyle.Bold);

			StringFormat sfRight = new StringFormat();
			sfRight.Alignment = StringAlignment.Far;

			StringFormat sfLeft = new StringFormat();
			sfLeft.Alignment = StringAlignment.Near;

			StringFormat sfCenter = new StringFormat();
			sfCenter.Alignment = StringAlignment.Center;

			StringFormat sfVert = new StringFormat();
			sfVert.FormatFlags |= StringFormatFlags.NoWrap;
			sfVert.Alignment = StringAlignment.Near;

			for (int page = 0; page < pdfDocument.Pages.Count; page++)
			{
				// select page we want (could change PageSize)
				pdfDocument.CurrentPage = page;

				// build rectangles for rendering text
				RectangleF rcPage = GetPageRect();

				RectangleF rcHeader = rcPage;
				rcHeader.Y = rcHeader.Top - 14;
				rcHeader.X = rcHeader.Left;
				rcHeader.Height = 12;

				// add left-aligned footer
				string text = pdfDocument.DocumentInfo.Title;
				pdfDocument.DrawString(text, fontHorz, Brushes.Gray, rcHeader);

				// add near text
				text = pdfDocument.DocumentInfo.Title + "Penson Financial Svcs";
				pdfDocument.DrawString(text, fontVert, Brushes.Gray, rcHeader, sfLeft);

				// add center text
				text = reportName + " -" + DateTime.Now.ToString("yyyy-MM-dd");
				pdfDocument.DrawString(text, fontVert, Brushes.Gray, rcHeader, sfCenter);

				// add far text
				text = string.Format("Page {0} of {1}", page + 1, pdfDocument.Pages.Count);
				pdfDocument.DrawString(text, fontHorz, Brushes.Gray, rcHeader, sfRight);
			}
		}

		internal RectangleF GetPageRect()
		{
			RectangleF rcPage = pdfDocument.PageRectangle;
			rcPage.Inflate(-20, -20);
			return rcPage;
		}

	}
}