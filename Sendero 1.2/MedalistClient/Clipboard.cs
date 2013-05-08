// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using Anetics.Common;

namespace Anetics.Medalist
{
  public class Clipboard
  {
		private MainForm mainForm;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
		private bool showAll;

		public Clipboard(MainForm mainForm, ref C1.Win.C1TrueDBGrid.C1TrueDBGrid grid, bool showAll)
		{
			try
			{
				this.grid = grid;
				this.mainForm = mainForm;
				this.showAll = showAll;
			}
			catch {}
		}
        
   private void exportToClipboard ()
    {
		 try
		 {
			 int textLength;
			 int [] maxTextLength;

			 int columnIndex = -1;
			 string gridData = "\n\n\n";

			 if (grid.SelectedCols.Count.Equals(0))
			 {
				 mainForm.Alert("You have not selected any rows.");
				 return;
			 }

			 try
			 {
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

				 foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
				 {
					 gridData += dataColumn.Caption.PadRight(maxTextLength[++columnIndex] + 2, ' ');
				 }
				 gridData += "\n";
        
				 columnIndex = -1;

				 foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
				 {
					 gridData += new String('-', maxTextLength[++columnIndex]) + "  ";
				 }
				 gridData += "\n";
        
				 foreach (int rowIndex in grid.SelectedRows)
				 {
					 columnIndex = -1;

					 foreach (C1.Win.C1TrueDBGrid.C1DataColumn dataColumn in grid.SelectedCols)
					 {
						 if (dataColumn.Value.GetType().Equals(typeof(System.String)))
						 {
							 gridData += dataColumn.CellText(rowIndex).PadRight(maxTextLength[++columnIndex] + 2);
						 }
						 else
						 {
							 gridData += dataColumn.CellText(rowIndex).PadLeft(maxTextLength[++columnIndex]) + "  ";
						 }
					 }
  
					 gridData += "\n";
				 }
                
				 Clipboard.SetDataObject(gridData, true);

				 mainForm.Alert("Total: " + grid.SelectedRows.Count + " items added to clipboard.");
			 }				
			 catch (Exception e)
			 {
				 Log.Write(e.Message + " [Email.DoSend]", Log.Error, 1);
			 }
		 }
    }
  }
}

