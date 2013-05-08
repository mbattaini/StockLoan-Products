using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using StockLoan.Common;

namespace StockLoan.Inventory
{


    class InventoryDataParser
    {
        public event ImportErrorEventHandler ImportErrorEvent;

        public void RaiseImportErrorEvent(object sender, ImportErrorEventArgs e)
        {
            if (null != ImportErrorEvent)
            {
                ImportErrorEvent(sender, e);
            }
        }


        public InventoryDataParser()
        {

        }

        public int ParseData(ImportSpec ImportSpecification)
        {

            // Return Value as Percent of File Successfully Imported
            int nReturnPercentRowsImported = 0;
            int nNumRowsTotal = 0; // FileRows.Length;
            int nNumRowsImported = 0;

            try
            {
                string[] arrayStrFileRows;

                // Find Row data
                if (ImportSpecification.HasImportData)
                {
                    arrayStrFileRows = (string[])ImportSpecification.InventoryImportText.ToArray(typeof(string));
                }
                else
                {
                    // Read File
                    string strFileLocation = ImportSpecification.LocalFilePath;
                    arrayStrFileRows = System.IO.File.ReadAllLines(strFileLocation);
                    if (0 < arrayStrFileRows.Length)
                    {
                        ImportSpecification.HasImportData = true;
                    }
                }

                if (ImportSpecification.HasImportData)
                {
                    if (0 < arrayStrFileRows.Length)
                    {
                        nNumRowsTotal = arrayStrFileRows.Length;

                        // Regex Objects for Finding Whole Rows of Data
                        Regex rgxDataRow = null;
                        Regex rgxHeaderRow = null;
                        Regex rgxTrailerRow = null;

                        // Regex Objects for Finding Individual Values in Columns
                        Regex rgxAccountCol = null;
                        Regex rgxDateCol = null;
                        Regex rgxRowCountCol = null;

                        //Regex For Replacing Comma Separated Values
                        Regex rgxComma = null;

                        if (ImportSpecification.HasImportData)
                        {
                            //Construct RegEx(s)
                            RegexOptions options = InventoryImportController.RegExOptions;
                            if (!string.IsNullOrEmpty(ImportSpecification.RegexData)) { rgxDataRow = new Regex(ImportSpecification.RegexData, options); }
                            if (!string.IsNullOrEmpty(ImportSpecification.RegexHeader)) { rgxHeaderRow = new Regex(ImportSpecification.RegexHeader, options); }
                            if (!string.IsNullOrEmpty(ImportSpecification.RegexTrailer)) { rgxTrailerRow = new Regex(ImportSpecification.RegexTrailer, options); }
                            if (!string.IsNullOrEmpty(ImportSpecification.RegexAccount)) { rgxAccountCol = new Regex(ImportSpecification.RegexAccount, options); }
                            if (!string.IsNullOrEmpty(ImportSpecification.RegexDate)) { rgxDateCol = new Regex(ImportSpecification.RegexDate, options); }
                            if (!string.IsNullOrEmpty(ImportSpecification.RegexRowCount)) { rgxRowCountCol = new Regex(ImportSpecification.RegexRowCount, options); }
                            rgxComma = new Regex(",", options);


                            // Build Import Variables                    
                            string strHeaderRow = "";
                            string strAccount = "";

                            // Search Each Row For Relevant Data
                            foreach (string strRow in arrayStrFileRows)
                            {
                                if ((null != strRow) && (string.Empty != strRow.Trim()))
                                {

                                    // Find Whole Header (Find Once)
                                    if ((null != rgxHeaderRow) && (string.IsNullOrEmpty(strHeaderRow)) && (rgxHeaderRow.IsMatch(strRow)))
                                    {
                                        strHeaderRow = strRow;
                                    }

                                    // Find Date (Find Once)
                                    if ((null != rgxDateCol) && (1900 == ImportSpecification.BizDateSpecified.Year))
                                    {
                                        ImportSpecification.BizDateSpecified = ParseDateText(ImportSpecification, strRow);
                                    }

                                    // Find Record Count (Find Once)
                                    if ((null != rgxRowCountCol) && (0 == ImportSpecification.NumRecordsExpected))
                                    {
                                        Match matchRowCountCol = rgxRowCountCol.Match(strRow);
                                        if (matchRowCountCol.Success)
                                        {
                                            string strRowCount = matchRowCountCol.Groups["RowCount"].Value;
                                            string strRowCountNoCommas = rgxComma.Replace(strRowCount, "");
                                            int nRowCount = int.Parse(strRowCountNoCommas);
                                            ImportSpecification.NumRecordsExpected = nRowCount;
                                        }
                                    }

                                    // Find Account (Find Once)
                                    if ((null != rgxAccountCol) && (string.IsNullOrEmpty(strAccount)))
                                    {
                                        Match matchAccountCol = rgxAccountCol.Match(strRow);
                                        if (matchAccountCol.Success)
                                        {
                                            try
                                            {
                                                strAccount = matchAccountCol.Groups["Account"].Value;
                                            }
                                            catch (Exception ex)
                                            {
                                                Log.Write(ex.Message + " [InventoryDataParser.ParseData]", Log.Error, 1);
                                                Console.WriteLine(ex.Message);
                                                RaiseErrorEvent(ImportSpecification, ex);
                                            }
                                        }
                                    }


                                    // Find Row Data (Find All)
                                    if (null != rgxDataRow)
                                    {
                                        Match matchDataRow = rgxDataRow.Match(strRow);
                                        if (matchDataRow.Success)
                                        {
                                            string strSecId = matchDataRow.Groups["SecId"].Value;
                                            string strQuantity = matchDataRow.Groups["Quantity"].Value;

                                            // Check for Valid Row Data: SecId, Quantity
                                            if ((!string.IsNullOrEmpty(strSecId)) && (!string.IsNullOrEmpty(strQuantity)))
                                            {
                                                string strQuantityNoCommas = rgxComma.Replace(strQuantity, "");
                                                long nQuantity = long.Parse(strQuantityNoCommas);
                                                InventoryDataEntry ActiveRecord = new InventoryDataEntry(strSecId, nQuantity, ImportSpecification.ExecutionID);

                                                // Build New Row
                                                ImportSpecification.InventoryEntryObjects.Add(ActiveRecord);
                                            }

                                        }
                                    }




                                }//End If Not Null StrRow
                            } // End Foreach Text Row


                            // Populate Individual Inventory Entries with Common Data: Desk, Bizdate, Account
                            foreach (InventoryDataEntry ActiveInventoryEntry in ImportSpecification.InventoryEntryObjects)
                            {
                                //Set Desk
                                ActiveInventoryEntry.Desk = ImportSpecification.Desk;

                                //Set Date
                                if (1900 != ImportSpecification.BizDateSpecified.Year)
                                {
                                    ActiveInventoryEntry.BizDate = ImportSpecification.BizDateSpecified;
                                }
                                else
                                {
                                    if (!ImportSpecification.IsBizDatePrior)
                                    {
                                        ActiveInventoryEntry.BizDate = InventoryImportController.GetBizDate();
                                    }
                                    else
                                    {
                                        ActiveInventoryEntry.BizDate = InventoryImportController.GetPriorBizDate();
                                    }
                                }

                                // Set Account
                                if (!string.IsNullOrEmpty(strAccount))
                                {
                                    ActiveInventoryEntry.Account = strAccount;
                                }

                            }

                        }
                    }
                }// End HasData

            } // end try
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryDataParser.ParseData]", Log.Error, 1);
                Console.WriteLine(ex.Message);
                RaiseErrorEvent(ImportSpecification, ex);
            }

            return nReturnPercentRowsImported;

        }


        public DateTime ParseDateText(ImportSpec ImportSpecification, string DataRow)
        {
            DateTime dtBizDateSpecified = DateTime.Parse("1-1-1900");

            if (!string.IsNullOrEmpty(ImportSpecification.RegexDate))
            {
                Regex rgxDateCol = new Regex(ImportSpecification.RegexDate, InventoryImportController.RegExOptions);
                Match matchDateCol = rgxDateCol.Match(DataRow);
                if (matchDateCol.Success)
                {
                    try
                    {
                        string strDay = matchDateCol.Groups["Day"].Value;
                        string strMonth = matchDateCol.Groups["Month"].Value;
                        string strYear = matchDateCol.Groups["Year"].Value;

                        // Handle 2-digit year '09' = '2009'
                        if (4 > strYear.Length)
                        {
                            string strShortYear = strYear;
                            int nDigitsNeeded = 4 - strYear.Length;
                            string strFullYear = DateTime.Now.Year.ToString();
                            string strDigitsToAdd = strFullYear.Substring(0, nDigitsNeeded);
                            strYear = strDigitsToAdd + strShortYear;
                        }

                        string strBizdate = string.Format("{0}/{1}/{2} 12:00:00 AM", strMonth, strDay, strYear);
                        dtBizDateSpecified = DateTime.Parse(strBizdate);
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex.Message + " [InventoryDataParser.ParseDateText]", Log.Error, 1);
                        Console.WriteLine(ex.Message);
                        RaiseErrorEvent(ImportSpecification, ex);
                    }
                }
            }
            return dtBizDateSpecified;

        }


        private void RaiseErrorEvent(ImportSpec ImportSpecification, Exception Ex)
        {
            ImportErrorEventArgs argsErrorEvent = new ImportErrorEventArgs(ImportSpecification, Ex);
            RaiseImportErrorEvent(this, argsErrorEvent);
        }


    }
}
