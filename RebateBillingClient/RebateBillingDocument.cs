using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
//using System.Diagnostics;
//using System.Globalization;
//using System.Runtime.Remoting;

using StockLoan.Common;		
using StockLoan.Golden;		
using C1.C1Pdf;				
using C1.C1Excel;			


namespace Golden
{

    public class RebateBillingDocument
    {

        // Summary Column Numbers
        const int SUM_HDR_Type = 0;
        const int SUM_HDR_GrpCode = 0;
        const int SUM_HDR_AcctNum = 1;
        const int SUM_HDR_Charge = 2;

        // Account Detail Column Numbers
        const int ACC_HDR_Type = 0;
        const int ACC_HDR_Date = 0;
        const int ACC_HDR_GrpCode = 1;
        const int ACC_HDR_AcctNum = 2;
        const int ACC_HDR_Cusip = 3;
        const int ACC_HDR_Symbol = 4;
        const int ACC_HDR_Quantity = 5;
        const int ACC_HDR_Rate = 6;
        const int ACC_HDR_Charge = 7;

        // CUSIP Detail Column Numbers
        const int CSP_HDR_Type = 0;
        const int CSP_HDR_Date = 0;
        const int CSP_HDR_GrpCode = 1;
        const int CSP_HDR_Cusip = 2;
        const int CSP_HDR_Symbol = 3;
        const int CSP_HDR_Quantity = 4;
        const int CSP_HDR_Rate = 5;
        const int CSP_HDR_Charge = 6;

        const string HDR_Date_Desc = "Date";
        const string HDR_GrpCode_Desc = "Group Code";
        const string HDR_AcctNum_Desc = "Account Number";
        const string HDR_Cusip_Desc = "CUSIP";
        const string HDR_Symbol_Desc = "Symbol";
        const string HDR_Quantity_Desc = "Quantity";
        const string HDR_Rate_Desc = "Rate";
        const string HDR_Charge_Desc = "Charge";

        //private MainForm mainForm = null;


        public RebateBillingDocument(MainForm mainForm)
		{            
        }

        public static void ShortSaleSummaryCorrespondentBillCreate(MainForm mainForm, string fileName, string billType, string platForm, string fromDate, string stopDate, string groupCode)
        {
            if(billType.ToLower().Equals("pdf"))
            {
                bool bResult = false;
                bResult = ShortSaleSummaryCorrespondentBillPdfCreate(mainForm, fileName, platForm, fromDate, stopDate, groupCode);
                // To Do PDF correspondent bill processing
            }
            else
            {
                bool bResult = false;
                bResult = ShortSaleSummaryCorrespondentBillExcelCreate(mainForm, fileName, platForm, fromDate, stopDate, groupCode);
                //To Do Excel correspondent bill processing
            
            }

        }

        private static bool ShortSaleSummaryCorrespondentBillExcelCreate(MainForm mainForm, string fileName, string platForm, string startDate, string stopDate, string groupCode)
        {
            // Write Excel worksheet based on Dataset which returned Multiple tables 
            // dsExcelCorrespondent.Tables["ExcelAccountsSummary"]
            // dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"]
            // dsExcelCorrespondent.Tables["ExcelBillingSummary"]
            // dsEcellCorrespondent.Tables["TradingGroupNames"]

            try
            {
                //MainForm mainForm = null;
				DataSet dsExcelCorrespondent = mainForm.RebateAgent.ShortSaleBillingSummaryCorrespondentBillExcelGet(
                    startDate, stopDate, groupCode, platForm);

                int tblIndex = dsExcelCorrespondent.Tables.Count;
                string groupName = "";

                foreach (DataRow row in dsExcelCorrespondent.Tables["TradingGroupNames"].Rows)
                {
                    if (row["GroupCode"].ToString().Trim().Equals(groupCode.Trim()))
                    {
                        groupName = groupCode.Trim() + " - " + row["GroupName"].ToString().Trim();
                        break;
                    }
                }

                C1XLBook excelBook = new C1XLBook();
                XLSheet xlSheet = excelBook.Sheets.Add("Summary By Account");

                xlSheet = BuildExcelSummary(xlSheet, dsExcelCorrespondent, groupName, startDate, stopDate, excelBook);

                // This Detail Worksheet Done, auto size it
                Excel.AutoSizeColumns(excelBook, excelBook.Sheets["Summary By Account"]);

                xlSheet = excelBook.Sheets.Add("Summary By CUSIP");

                xlSheet = BuildExcelCusip(xlSheet, dsExcelCorrespondent, groupName, startDate, stopDate, excelBook);

                // This Detail Worksheet Done, auto size it
                Excel.AutoSizeColumns(excelBook, excelBook.Sheets["Summary By CUSIP"]);

                string acctCompare = "0";
                int rowIndex = 0;
                foreach (DataRow drTemp in dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows)
                {

                    string accountNumber;

                    if (drTemp["GroupCode"].ToString().ToUpper().Equals("GROUPCODE"))
                        rowIndex++;

                    accountNumber = dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["AccountNumber"].ToString().Trim();
                    if (accountNumber != acctCompare)
                    {
                        xlSheet = excelBook.Sheets.Add(accountNumber);
                        xlSheet = BuildExcelAccount(xlSheet, dsExcelCorrespondent, groupName, accountNumber, excelBook);

                        // This Detail Worksheet Done, auto size it
                        Excel.AutoSizeColumns(excelBook, excelBook.Sheets[accountNumber]);

                        acctCompare = accountNumber;
                    }
                    rowIndex++;
                }
                int lineCounter;
                //int xlCell;

                lineCounter = xlSheet.Rows.Count;

                // This save is to be done after the process completes. Will be moved to another process when finished
                excelBook.Sheets.RemoveAt(0);
                excelBook.Save(fileName);
                //Process.Start(fileName);

                return true;
            }
			catch (Exception error)
			{
				Log.Write(error.Message + " [RebateBillingDocument.ShortSaleSummaryCorrespondentBillExcelCreate]", Log.Error, 1);
				//throw;
                return false;
            }
            
        }

        public static XLSheet BuildExcelSummary(XLSheet xlSheet, DataSet dsExcelCorrespondent, string groupCode, string startDate, string stopDate, C1XLBook excelBook)
        {

            xlSheet = BuildExcelHeader("Summary", groupCode, startDate, stopDate, "", xlSheet, excelBook);

            XLCell xlCell;

            //// Create styles for cells  (for whole book level)
            XLStyle styleDecimal = new XLStyle(excelBook);
            styleDecimal = new XLStyle(excelBook);
            styleDecimal.Format = "#,##0.00";
            styleDecimal.AlignHorz = XLAlignHorzEnum.Right;

            XLStyle styleInteger = new XLStyle(excelBook);
            styleInteger.Format = "#,##0";
            styleInteger.AlignHorz = XLAlignHorzEnum.Right;

            XLStyle styleTextRightAlign = new XLStyle(excelBook);
            styleTextRightAlign.AlignHorz = XLAlignHorzEnum.Right;

            XLStyle styleTextLeftAlign = new XLStyle(excelBook);
            styleTextLeftAlign.AlignHorz = XLAlignHorzEnum.Left;

            XLStyle styleDecimal3 = new XLStyle(excelBook);
            styleDecimal3 = new XLStyle(excelBook);
            styleDecimal3.Format = "#,##0.000";
            styleDecimal3.AlignHorz = XLAlignHorzEnum.Right;

            int lineCounter = xlSheet.Rows.Count;
            int rowIndex = 0; 
            decimal totAmount = 0;
            decimal amount;

            if (!string.IsNullOrEmpty(dsExcelCorrespondent.Tables["ExcelAccountsSummary"].Rows[rowIndex]["Charge"].ToString()))
            {
                // Build detail of SummaryByAccount worksheet
                foreach (DataRow drTemp in dsExcelCorrespondent.Tables["ExcelAccountsSummary"].Rows)
                {   // Building Summary Report
                    if (drTemp["GroupCode"].ToString().ToUpper().Equals("GROUPCODE"))
                        rowIndex++;
                    xlCell = xlSheet[lineCounter, SUM_HDR_GrpCode];
                    xlCell.Value = dsExcelCorrespondent.Tables["ExcelAccountsSummary"].Rows[rowIndex]["GroupCode"].ToString().Trim();
                    xlCell.Style = styleTextLeftAlign;

                    xlCell = xlSheet[lineCounter, SUM_HDR_AcctNum];
                    xlCell.Value = dsExcelCorrespondent.Tables["ExcelAccountsSummary"].Rows[rowIndex]["AccountNumber"].ToString().Trim();

                    xlCell = xlSheet[lineCounter, SUM_HDR_Charge];

                    if (string.IsNullOrEmpty(dsExcelCorrespondent.Tables["ExcelAccountsSummary"].Rows[rowIndex]["Charge"].ToString()))
                        amount = 0;
                    else
                        amount = (System.Convert.ToDecimal(dsExcelCorrespondent.Tables["ExcelAccountsSummary"].Rows[rowIndex]["Charge"]));

                    xlCell.Value = amount;  //dsExcelCorrespondent.Tables["ExcelAccountsSummary"].Rows[rowIndex]["Charge"].ToString().Trim();
                    xlCell.Style = styleDecimal;

                    totAmount += amount;

                    lineCounter++;
                    rowIndex++;
                }

                lineCounter++; 
                xlCell = xlSheet[lineCounter, SUM_HDR_AcctNum];
                xlCell.Value = "Total Charge:";
                xlCell.Style = styleTextRightAlign;

                xlCell = xlSheet[lineCounter, SUM_HDR_Charge];
                xlCell.Value = totAmount;
                xlCell.Style = styleDecimal;
            }
                return xlSheet;
        }

        public static XLSheet BuildExcelCusip(XLSheet xlSheet, DataSet dsExcelCorrespondent, string groupCode, string startDate, string stopDate, C1XLBook excelBook)
        {
            // dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"]
            xlSheet = BuildExcelHeader("Cusip", groupCode, startDate, stopDate, "", xlSheet, excelBook);

            XLCell xlCell;

            //// Create styles for cells  (for whole book level)
            XLStyle styleDecimal = new XLStyle(excelBook);
            styleDecimal = new XLStyle(excelBook);
            styleDecimal.Format = "#,##0.00";
            styleDecimal.AlignHorz = XLAlignHorzEnum.Right;

            XLStyle styleInteger = new XLStyle(excelBook);
            styleInteger.Format = "#,##0";
            styleInteger.AlignHorz = XLAlignHorzEnum.Right;

            XLStyle styleTextRightAlign = new XLStyle(excelBook);
            styleTextRightAlign.AlignHorz = XLAlignHorzEnum.Right;

            XLStyle styleTextLeftAlign = new XLStyle(excelBook);
            styleTextLeftAlign.AlignHorz = XLAlignHorzEnum.Left;

            XLStyle styleDecimal3 = new XLStyle(excelBook);
            styleDecimal3 = new XLStyle(excelBook);
            styleDecimal3.Format = "#,##0.000";
            styleDecimal3.AlignHorz = XLAlignHorzEnum.Right;
            
            int lineCounter = xlSheet.Rows.Count;
            int rowIndex = 0; 
            decimal totAmount = 0;
            decimal amount = 0;
            string bizDate;
            if (!string.IsNullOrEmpty(dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"].Rows[rowIndex]["MarkupRate"].ToString()))
            {
                // Build detail of SummaryByAccount worksheet
                foreach (DataRow drTemp in dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"].Rows)
                {   // Building Summary Report
                    if (drTemp["GroupCode"].ToString().ToUpper().Equals("GROUPCODE"))
                        rowIndex++;
                    xlCell = xlSheet[lineCounter, CSP_HDR_Date];
                    bizDate = dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"].Rows[rowIndex]["BizDate"].ToString().Trim();
                    bizDate = DateTime.Parse(bizDate).ToString("yyyy-MM-dd");
                    xlCell.Value = bizDate;

                    xlCell = xlSheet[lineCounter, CSP_HDR_GrpCode];
                    xlCell.Value = dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"].Rows[rowIndex]["GroupCode"].ToString().Trim();
                    xlCell.Style = styleTextLeftAlign;

                    xlCell = xlSheet[lineCounter, CSP_HDR_Cusip];
                    xlCell.Value = dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"].Rows[rowIndex]["SecId"].ToString().Trim();

                    xlCell = xlSheet[lineCounter, CSP_HDR_Symbol];
                    xlCell.Value = dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"].Rows[rowIndex]["Symbol"].ToString().Trim();

                    xlCell = xlSheet[lineCounter, CSP_HDR_Quantity];
                    decimal qty = (Decimal.Parse(dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"].Rows[rowIndex]["QuantityCovered"].ToString()));
                    //xlCell.Value = dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"].Rows[rowIndex]["QuantityCovered"].ToString().Trim();
                    xlCell.Value = qty;
                    xlCell.Style = styleInteger;

                    decimal testDec3;
                    xlCell = xlSheet[lineCounter, CSP_HDR_Rate];
                    if (string.IsNullOrEmpty(dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"].Rows[rowIndex]["MarkupRate"].ToString()))
                    { }
                    else
                    {
                        amount = (testDec3 = (Decimal.Parse(dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"].Rows[rowIndex]["MarkupRate"].ToString())));
                    }
                    xlCell.Value = amount;
                    xlCell.Style = styleDecimal3;

                    xlCell = xlSheet[lineCounter, CSP_HDR_Charge];
                    if (string.IsNullOrEmpty(dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"].Rows[rowIndex]["Charge"].ToString()))
                        amount = 0;
                    else
                        amount = (System.Convert.ToDecimal(dsExcelCorrespondent.Tables["ExcelSummaryBySecurity"].Rows[rowIndex]["Charge"]));

                    xlCell.Value = amount;  //dsExcelCorrespondent.Tables["ExcelAccountsSummary"].Rows[rowIndex]["Charge"].ToString().Trim();
                    xlCell.Style = styleDecimal;
                    totAmount += amount;

                    lineCounter++;
                    rowIndex++;
                }

                lineCounter++; 
                xlCell = xlSheet[lineCounter, CSP_HDR_Rate];
                xlCell.Value = "Total Charge:";
                xlCell.Style = styleTextRightAlign;

                xlCell = xlSheet[lineCounter, CSP_HDR_Charge];
                xlCell.Value = totAmount;
                xlCell.Style = styleDecimal;
            }

                return xlSheet;
        }

        public static XLSheet BuildExcelAccount(XLSheet xlSheet, DataSet dsExcelCorrespondent, string groupCode, string accountNumber, C1XLBook excelBook)
        {
            // dsExcelCorrespondent.Tables["ExcelBillingSummary"]
            
            xlSheet = BuildExcelHeader("Account", groupCode, "", "", accountNumber, xlSheet, excelBook);

            XLCell xlCell;

            //// Create styles for cells  (for whole book level)
            XLStyle styleDecimal = new XLStyle(excelBook);
            styleDecimal = new XLStyle(excelBook);
            styleDecimal.Format = "#,##0.00";
            styleDecimal.AlignHorz = XLAlignHorzEnum.Right;

            XLStyle styleInteger = new XLStyle(excelBook);
            styleInteger.Format = "#,##0";
            styleInteger.AlignHorz = XLAlignHorzEnum.Right;

            XLStyle styleTextRightAlign = new XLStyle(excelBook);
            styleTextRightAlign.AlignHorz = XLAlignHorzEnum.Right;

            XLStyle styleTextLeftAlign = new XLStyle(excelBook);
            styleTextLeftAlign.AlignHorz = XLAlignHorzEnum.Left;

            XLStyle styleDecimal3 = new XLStyle(excelBook);
            styleDecimal3 = new XLStyle(excelBook);
            styleDecimal3.Format = "#,##0.000";
            styleDecimal3.AlignHorz = XLAlignHorzEnum.Right;

            int lineCounter = xlSheet.Rows.Count;
            int rowIndex = 0; 
            decimal totAmount = 0;
            decimal amount = 0;
            string bizDate;
            if (!string.IsNullOrEmpty(dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["MarkupRate"].ToString()))
            {
                // Build detail of SummaryByAccount worksheet
                foreach (DataRow drTemp in dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows)
                {   // Building Summary Report
                    if (drTemp["GroupCode"].ToString().ToUpper().Equals("GROUPCODE"))
                        rowIndex++;
                    if (drTemp["AccountNumber"].ToString().Equals(accountNumber))
                    {
                        xlCell = xlSheet[lineCounter, ACC_HDR_Date];
                        bizDate = dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["BizDate"].ToString().Trim();
                        bizDate = DateTime.Parse(bizDate).ToString("yyyy-MM-dd");
                        xlCell.Value = bizDate;

                        xlCell = xlSheet[lineCounter, ACC_HDR_GrpCode];
                        xlCell.Value = dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["GroupCode"].ToString().Trim();
                        xlCell.Style = styleTextLeftAlign;

                        xlCell = xlSheet[lineCounter, ACC_HDR_AcctNum];
                        xlCell.Value = dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["AccountNumber"].ToString().Trim();

                        xlCell = xlSheet[lineCounter, ACC_HDR_Cusip];
                        xlCell.Value = dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["SecId"].ToString().Trim();

                        xlCell = xlSheet[lineCounter, ACC_HDR_Symbol];
                        xlCell.Value = dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["Symbol"].ToString().Trim();

                        xlCell = xlSheet[lineCounter, ACC_HDR_Quantity];
                        //xlCell.Value = dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["QuantityCovered"].ToString().Trim();
                        decimal qty = (Decimal.Parse(dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["QuantityCovered"].ToString()));
                        xlCell.Value = qty;
                        xlCell.Style = styleInteger;

                        decimal testDec3 = 0;
                        xlCell = xlSheet[lineCounter, ACC_HDR_Rate];
                        //decimal testDec3 = (Decimal.Parse(dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["MarkupRate"].ToString()));
                        if (string.IsNullOrEmpty(dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["MarkupRate"].ToString()))
                            testDec3 = 0;
                        else testDec3 = (Decimal.Parse(dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["MarkupRate"].ToString()));
                        xlCell.Value = testDec3;
                        xlCell.Style = styleDecimal3;

                        xlCell = xlSheet[lineCounter, ACC_HDR_Charge];
                        if (string.IsNullOrEmpty(dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["ModifiedCharge"].ToString()))
                            amount = 0;
                        else
                            amount = (System.Convert.ToDecimal(dsExcelCorrespondent.Tables["ExcelBillingSummary"].Rows[rowIndex]["ModifiedCharge"]));

                        xlCell.Value = amount;  //dsExcelCorrespondent.Tables["ExcelAccountsSummary"].Rows[rowIndex]["Charge"].ToString().Trim();
                        xlCell.Style = styleDecimal;
                        totAmount += amount;

                        lineCounter++;

                    }
                    rowIndex++;
                }
                lineCounter++; 
                xlCell = xlSheet[lineCounter, ACC_HDR_Rate];
                xlCell.Value = "Total Charge:";
                xlCell.Style = styleTextRightAlign;

                xlCell = xlSheet[lineCounter, ACC_HDR_Charge];
                xlCell.Value = totAmount;
                xlCell.Style = styleDecimal;
            }
            return xlSheet;
        }

        public static XLSheet BuildExcelHeader(string hdrType, string groupCode, string startDate, string stopDate, string accountNumber, XLSheet xlSheet, C1XLBook excelBook)
        {
            //this.groupCode = groupCode;

            switch (hdrType.ToUpper())
             {
                case "SUMMARY":

                    xlSheet = BuildSummaryHeader(groupCode, startDate, stopDate, xlSheet, excelBook);

                    break;

                case "CUSIP":

                    xlSheet = BuildCusipHeader(groupCode, startDate, stopDate, xlSheet, excelBook);
                    break;

                case "ACCOUNT":

                    xlSheet = BuildAccountHeader(groupCode, accountNumber, xlSheet, excelBook);

                    break;

                default:
                    
                    break;
             }

            return xlSheet;

        }

		public static XLSheet BuildSummaryHeader(string groupCode, string startDate, string stopDate, XLSheet xlSheet, C1XLBook excelBook)
        {
                    
            int lineCounter = 0;

            //// Create styles for cells  (for whole book level)
            XLStyle styleDecimal = new XLStyle(excelBook);
            styleDecimal = new XLStyle(excelBook);
            styleDecimal.Format = "#,##0.00";
            styleDecimal.AlignHorz = XLAlignHorzEnum.Right;

            XLStyle styleInteger = new XLStyle(excelBook);
            styleInteger.Format = "#,##0";
            styleInteger.AlignHorz = XLAlignHorzEnum.Right;

            XLStyle styleTextRightAlign = new XLStyle(excelBook);
            styleTextRightAlign.AlignHorz = XLAlignHorzEnum.Right;

            string Header1 = groupCode + " - " + Standard.ConfigValue("BillingTitle", "Stock Borrow Billing");

            XLCell xlCell = xlSheet[lineCounter, SUM_HDR_Type];
            xlCell.Value = Header1;

            lineCounter++;
            //lineCounter++;

            Header1 = "Billing for " + DateTime.Parse(startDate).ToString("yyyy-MM-dd") + " to " + DateTime.Parse(stopDate).ToString("yyyy-MM-dd");

            xlCell = xlSheet[lineCounter, SUM_HDR_Type];
            xlCell.Value = Header1;

            lineCounter++;
            lineCounter++;

            Header1 = "Account(s)  Summary;";

            xlCell = xlSheet[lineCounter, 0];
            xlCell.Value = Header1;

            lineCounter++;
            lineCounter++;

            Header1 = "Summary for " + DateTime.Parse(startDate).ToString("yyyy-MM-dd") + " to " + DateTime.Parse(stopDate).ToString("yyyy-MM-dd");
            xlCell = xlSheet[lineCounter, 0];
            xlCell.Value = Header1;

            lineCounter++;
            lineCounter++;

            xlCell = xlSheet[lineCounter, SUM_HDR_GrpCode];
            xlCell.Value = HDR_GrpCode_Desc;

            xlCell = xlSheet[lineCounter, SUM_HDR_AcctNum];
            xlCell.Value = HDR_AcctNum_Desc;

            xlCell = xlSheet[lineCounter, SUM_HDR_Charge];
            xlCell.Value = HDR_Charge_Desc;
            xlCell.Style = styleTextRightAlign;

        return xlSheet;
    }

		public static XLSheet BuildCusipHeader(string groupCode, string startDate, string stopDate, XLSheet xlSheet, C1XLBook excelBook)
    {

        int lineCounter = 0;

        //// Create styles for cells  (for whole book level)
        XLStyle styleDecimal = new XLStyle(excelBook);
        styleDecimal = new XLStyle(excelBook);
        styleDecimal.Format = "#,##0.00";
        styleDecimal.AlignHorz = XLAlignHorzEnum.Right;

        XLStyle styleInteger = new XLStyle(excelBook);
        styleInteger.Format = "#,##0";
        styleInteger.AlignHorz = XLAlignHorzEnum.Right;

        XLStyle styleTextRightAlign = new XLStyle(excelBook);
        styleTextRightAlign.AlignHorz = XLAlignHorzEnum.Right;

        string Header1 = groupCode + " - " + Standard.ConfigValue("BillingTitle", "Stock Borrow Billing");

        XLCell xlCell = xlSheet[lineCounter, CSP_HDR_Type];
        xlCell.Value = Header1;

        lineCounter++;
        lineCounter++;

        Header1 = "Summary for " + startDate + " to " + stopDate;

        xlCell = xlSheet[lineCounter, 0];
        xlCell.Value = Header1;

        lineCounter++;
        lineCounter++;

        xlCell = xlSheet[lineCounter, CSP_HDR_Date];
        xlCell.Value = HDR_Date_Desc;

        xlCell = xlSheet[lineCounter, CSP_HDR_GrpCode];
        xlCell.Value = HDR_GrpCode_Desc;

        xlCell = xlSheet[lineCounter, CSP_HDR_Cusip];
        xlCell.Value = HDR_Cusip_Desc;

        xlCell = xlSheet[lineCounter, CSP_HDR_Symbol];
        xlCell.Value = HDR_Symbol_Desc;

        xlCell = xlSheet[lineCounter, CSP_HDR_Quantity];
        xlCell.Value = HDR_Quantity_Desc;
        xlCell.Style = styleTextRightAlign;
       
        xlCell = xlSheet[lineCounter, CSP_HDR_Rate];
        xlCell.Value = HDR_Rate_Desc;
        xlCell.Style = styleTextRightAlign;

        xlCell = xlSheet[lineCounter, CSP_HDR_Charge];
        xlCell.Value = HDR_Charge_Desc;
        xlCell.Style = styleTextRightAlign;
        return xlSheet;
    }

		public static XLSheet BuildAccountHeader(string groupCode, string accountNumber, XLSheet xlSheet, C1XLBook excelBook)
    {

        int lineCounter = 0;

        //// Create styles for cells  (for whole book level)
        XLStyle styleDecimal = new XLStyle(excelBook);
        styleDecimal = new XLStyle(excelBook);
        styleDecimal.Format = "#,##0.00";
        styleDecimal.AlignHorz = XLAlignHorzEnum.Right;

        XLStyle styleInteger = new XLStyle(excelBook);
        styleInteger.Format = "#,##0";
        styleInteger.AlignHorz = XLAlignHorzEnum.Right;

        XLStyle styleTextRightAlign = new XLStyle(excelBook);
        styleTextRightAlign.AlignHorz = XLAlignHorzEnum.Right;

        string Header1 = groupCode + " - " + Standard.ConfigValue("BillingTitle", "Stock Borrow Billing");

        XLCell xlCell = xlSheet[lineCounter, 0];
        xlCell.Value = Header1;

        lineCounter++;
        lineCounter++;

        Header1 = groupCode + " - " + accountNumber;

        xlCell = xlSheet[lineCounter, 0];
        xlCell.Value = Header1;

        lineCounter++;
        lineCounter++;

        xlCell = xlSheet[lineCounter, ACC_HDR_Date];
        xlCell.Value = HDR_Date_Desc;

        xlCell = xlSheet[lineCounter, ACC_HDR_GrpCode];
        xlCell.Value = HDR_GrpCode_Desc;

        xlCell = xlSheet[lineCounter, ACC_HDR_AcctNum];
        xlCell.Value = HDR_AcctNum_Desc;

        xlCell = xlSheet[lineCounter, ACC_HDR_Cusip];
        xlCell.Value = HDR_Cusip_Desc;

        xlCell = xlSheet[lineCounter, ACC_HDR_Symbol];
        xlCell.Value = HDR_Symbol_Desc;

        xlCell = xlSheet[lineCounter, ACC_HDR_Quantity];
        xlCell.Value = HDR_Quantity_Desc;

        xlCell = xlSheet[lineCounter, ACC_HDR_Rate];
        xlCell.Value = HDR_Rate_Desc;
        xlCell.Style = styleTextRightAlign;

        xlCell = xlSheet[lineCounter, ACC_HDR_Charge];
        xlCell.Value = HDR_Charge_Desc;
        xlCell.Style = styleTextRightAlign;

        return xlSheet;
    }

		public static bool ShortSaleSummaryMasterBillCreate(MainForm mainForm, string fileName, string billType, string platForm, string startDate, string stopDate)
        {
            if(billType.ToString().Trim().ToLower().Equals("pdf"))
            {
                bool bResult = ShortSaleSummaryMasterBillPdfCreate(mainForm, fileName, billType, platForm, startDate, stopDate);
                 
                return bResult;
            }

            else
            {
                bool bResult = ShortSaleSummaryMasterBillExcelCreate(mainForm, fileName, billType, platForm, startDate, stopDate); 
            }
            return true;
        }

		private static bool ShortSaleSummaryCorrespondentBillPdfCreate(MainForm mainForm, string fileName, string platForm, string startDate, string stopDate, string groupCode)
    {
        try
        {
            //MainForm mainForm = null;
            // PDF output format 
            int index = 0;
            string bill = mainForm.RebateAgent.ShortSaleBillingSummaryBillGet(startDate, stopDate, groupCode, platForm);
            C1PdfDocument doc = new C1PdfDocument(System.Drawing.Printing.PaperKind.Legal, true);
            string page = Tools.SplitItem(bill, "!", 0);

            Font font = new Font("Courier New", 10);
            RectangleF rc = doc.PageRectangle;
            rc.Inflate(-72, -72);
            long pageNumber = doc.CurrentPage + 1;
            doc.DrawString(pageNumber.ToString(), font, Brushes.Black, new RectangleF(rc.Left, rc.Bottom, 30, 30));

            // Draw content of PDF file 
            while (true)
            {
                doc.DrawString(page, font, Brushes.Black, rc);
                index++;

                page = Tools.SplitItem(bill, "!", index);

                if (page.Equals(""))
                {
                    break;
                }
                else
                {
                    doc.NewPage();
                    pageNumber = doc.CurrentPage + 1;
                    doc.DrawString(pageNumber.ToString(), font, Brushes.Black, new RectangleF(rc.Left, rc.Bottom, 30, 30));
                }
            }

            // Save each groupCode PDF document to tempPath folder
            doc.Save(fileName);
            return true;
        }
        catch (Exception error)
        {
            MessageBox.Show(error.Message);
            return false;
        }
    }

		public static bool ShortSaleSummaryMasterBillExcelCreate(MainForm mainForm, string fileName, string billType, string platForm, string startDate, string stopDate)
		 {
			int sheetIndex = 0;
			int workSheetRow = 0;
			int workSheetCol = 0;
			string groupCode = "";
			string groupName = "";
			decimal totalCharge = 0;

        //ShortSaleSummaryMasterBillExcelCreate(mainForm, fileName, billType, platForm, startDate, stopDate)
            try
			{

                DataSet dsMasterBillExcel = null;

                // Get Master-Bill content in a Dataset (multiple tables)
                dsMasterBillExcel = mainForm.RebateAgent.ShortSaleBillingSummaryMasterBillExcelGet(startDate, stopDate, platForm);

                // Write Excel worksheet based on Dataset (Multiple tables) 
				// dsMasterBillExcel.Tables["TradingGroups"]
				// dsMasterBillExcel.Tables["BillingSummary"]
				// dsMasterBillExcel.Tables["TradingGroupNames"]

				startDate = DateTime.Parse(startDate).ToString("yyyy-MM-dd");
				stopDate = DateTime.Parse(stopDate).ToString("yyyy-MM-dd"); 

				// Create Excel book, worksheet collection, and base Style -----------------------------------------------------------
				C1XLBook book = new C1XLBook();									// This creates a default worksheet, name = "Sheet1"
				XLSheet sheet = book.Sheets[sheetIndex];						// Do Not add Summary Sheet, just rename the default "Sheet1" 
				sheet.Name = "CorrespondentSummary";

				// Create styles for cells  (for whole book level)
				XLStyle styleDecimal = new XLStyle(book);
				styleDecimal.Format = "#,##0.00";
				styleDecimal.AlignHorz = XLAlignHorzEnum.Right;

				XLStyle styleInteger = new XLStyle(book);
				styleInteger.Format = "#,##0";
				styleInteger.AlignHorz = XLAlignHorzEnum.Right;

				XLStyle styleTextRightAlign = new XLStyle(book);
				styleTextRightAlign.AlignHorz = XLAlignHorzEnum.Right;


				// Do Worksheet -- Correspondent Summary Worksheet --------------------------------------------

				// Correspondent Summary Worksheet Header 
				XLCell cell = sheet[0, 0];
                cell.Value = "Master Bill - " + Standard.ConfigValue("BillingTitle", "Stock Borrow Billing"); ;
				cell = sheet[1, 0];
				cell.Value = "Billing for " + startDate + " to " + stopDate;
				cell = sheet[3, 0];
				cell.Value = "Correspondent(s) Summary";
				cell = sheet[4, 0];
				cell.Value = "Summary for " + startDate + " to " + stopDate;

				// Correspondent Summary Column Header 
				cell = sheet[6, 0];
				cell.Value = "Group Code";
				cell = sheet[6, 1];
				cell.Value = "Charge";
				cell.Style = styleTextRightAlign; 
				
				//Summay Details Loop 
				workSheetRow = 7;
				workSheetCol = 0;
				totalCharge = 0;

				foreach (DataRow row in dsMasterBillExcel.Tables["TradingGroups"].Rows)
				{
					if (!row["ClientCharge"].ToString().Trim().Equals(""))					// skip Groups with no charge 
					{
						cell = sheet[workSheetRow,workSheetCol];
						cell.Value = row["GroupCode"].ToString().Trim();
						workSheetCol++;

						cell = sheet[workSheetRow, workSheetCol];
						cell.Value = decimal.Parse(row["ClientCharge"].ToString());			// cell.Value = row["ClientCharge"];
						cell.Style = styleDecimal;

						totalCharge += (!row["ClientCharge"].ToString().Equals("")) ? decimal.Parse(row["ClientCharge"].ToString()) : 0;
						workSheetCol = 0;
						workSheetRow++;
					}
				}

				// Summary Total Charge 
				workSheetRow++;			//Skip a line
				cell = sheet[workSheetRow, workSheetCol];
				cell.Value = "Total Charge:";
				cell.Style = styleTextRightAlign; 
				workSheetCol ++;
				cell = sheet[workSheetRow, workSheetCol];
				cell.Value = totalCharge;
				cell.Style = styleDecimal;

				Excel.AutoSizeColumns(book, book.Sheets["CorrespondentSummary"]);
				

				// Do Worksheet -- Account Summary Detail ----------------------------------------------------- 
				sheetIndex = 1;
				groupCode = "";
				groupName = "";
				workSheetCol = 0;
				workSheetRow = 0;
				totalCharge = 0;


				// Outer GroupCode Loop -- create a new worksheet for each GroupCode --------------------------
				foreach (DataRow drGroups in dsMasterBillExcel.Tables["TradingGroups"].Rows)
				{
					//Get current GroupCode and GroupName
					groupCode = drGroups["GroupCode"].ToString().Trim();
					
					foreach (DataRow dr in dsMasterBillExcel.Tables["TradingGroupNames"].Select("GroupCode = '" + groupCode + "'"))
					{	groupName = dr["GroupName"].ToString().Trim();	}

					// create new Worksheet 
					sheet = book.Sheets.Add(groupCode);		

					// GroupCode Accounts Worksheet Header 
					cell = sheet[0, 0];
                    cell.Value = groupCode + "  - " + groupName + " - " +Standard.ConfigValue("BillingTitle", "Stock Borrow Billing");
					cell = sheet[2, 0];
					cell.Value = "Account(s) Summary";
					cell = sheet[4, 0];
					cell.Value = "Summary for " + startDate + " to " + stopDate;

					//GroupCode Account Summary Column Header 
					cell = sheet[6, 0];
					cell.Value = "Group Code";
					cell = sheet[6, 1];
					cell.Value = "Account Number";
					cell = sheet[6, 2];
					cell.Value = "Symbol";
					cell = sheet[6, 3];
					cell.Value = "Charge";		// ModifiedCharge
					cell.Style = styleTextRightAlign; 

					// Inner loop -- Account Summary Detail data 
					workSheetRow = 7;
					workSheetCol = 0;
					totalCharge = 0;

					foreach (DataRow row in dsMasterBillExcel.Tables["BillingSummary"].Select("GroupCode = '" + groupCode + "'", "GroupCode, AccountNumber, SecId"))
					{
						if (!row["ModifiedCharge"].ToString().Trim().Equals(""))
						{
							cell = sheet[workSheetRow, workSheetCol++];
							cell.Value = row["GroupCode"].ToString().Trim();

							cell = sheet[workSheetRow, workSheetCol++];
							cell.Value = row["AccountNumber"].ToString().Trim();
		
							cell = sheet[workSheetRow, workSheetCol++];
							cell.Value = row["Symbol"].ToString().Trim();

							cell = sheet[workSheetRow, workSheetCol++];					// Modified Charge
							cell.Value = decimal.Parse(row["ModifiedCharge"].ToString());
							cell.Style = styleDecimal;

							totalCharge += (!row["ModifiedCharge"].ToString().Equals("")) ? decimal.Parse(row["ModifiedCharge"].ToString()) : 0;
							workSheetRow++;
							workSheetCol = 0;
						}
					}	// foreach Detail Data loop 

					//Detail GroupCode Total Charge
					workSheetCol = 2;
					workSheetRow++;
					cell = sheet[workSheetRow, workSheetCol];
					cell.Value = "Total Charge:";
					cell.Style = styleTextRightAlign;
					workSheetCol++;
					cell = sheet[workSheetRow, workSheetCol];
					cell.Value = totalCharge;
					cell.Style = styleDecimal;

					// This Detail Worksheet Done, auto size it
					Excel.AutoSizeColumns(book, book.Sheets[groupCode]);
					sheetIndex++;

				} // foreach Outer GroupCode loop 
				

				// Save Book with multiple-sheets as fileName --------------------------------
				book.Save(fileName);
                return true;
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [RebateBillingDocument.ExportMasterBillToExcel]", Log.Error, 1);
                return false;
            }
		}

		public static bool ShortSaleSummaryMasterBillPdfCreate(MainForm mainForm, string fileName, string billType, string platForm, string startDate, string stopDate)
		{	
            //MainForm mainForm = null;
            int index = 0;
            string bill;  
            string page = "";
			long pageNumber = 0;

            bill = mainForm.RebateAgent.ShortSaleBillingSummaryMasterBillGet(startDate, stopDate, platForm);
            
            try
			{
				C1PdfDocument doc = new C1PdfDocument(System.Drawing.Printing.PaperKind.Legal, true);
				index = 0;
				page = Tools.SplitItem(bill, "!", 0);

				Font font = new Font("Courier New", 10);
				RectangleF rc = doc.PageRectangle;
				rc.Inflate(-72, -72);

				while (true)
				{
					doc.DrawString(page, font, Brushes.Black, rc);
					index++;

					page = Tools.SplitItem(bill, "!", index);

					if (page.Equals(""))
					{
						break;
					}
					else
					{
						doc.NewPage();
						pageNumber = doc.CurrentPage + 1;
						doc.DrawString(pageNumber.ToString(), font, Brushes.Black, new RectangleF(rc.Left, rc.Bottom, 30, 30));
					}
				}

				doc.NewPage();
				pageNumber = doc.CurrentPage + 1;
				doc.DrawString(pageNumber.ToString(), font, Brushes.Black, new RectangleF(rc.Left, rc.Bottom, 30, 30));

				doc.Save(fileName);
                return true;
			}
			catch (Exception error)
			{
                Log.Write(error.Message + " [RebateBillingDocument.ExportMasterBillToPdf]", Log.Error, 1);
                return false;
            }

		}


    }
}
