using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace DashBusiness
{
    public class LCOR_BorrowOrder
    {
        private static DataSet dsBorrowOrderSubmission;

        public static string BorrowOrderSubmissionHeaderSet(string bookGroup, bool responseFile, string date)
        {
            string strHeader = "";

            strHeader = "1";
            strHeader += bookGroup.Substring(0, 4).PadLeft(4, ' ');
            strHeader += "LCOR";

            if (responseFile)
                strHeader += "Y";
            else
                strHeader += " ";

            strHeader += new string(' ', 9);
            strHeader += DateTime.Parse(date).ToString("MMddyy");
            strHeader += new string(' ', 75);
            return strHeader;
        }


        public static string BorrowOrderSubmissionTrailerSet()
        {
            string strTrailer = "";

            strTrailer = "9";
            strTrailer += dsBorrowOrderSubmission.Tables["BorrowOrder"].Rows.Count.ToString().PadLeft(5, '0').PadLeft(5, '0');
            strTrailer += BorrowOrderQuantityCount.ToString().PadLeft(16, '0').Substring(0, 16);
            strTrailer += new string(' ', 78);
            return strTrailer;
        }

        public static string BorrowOrderSubmissionBody()
        {
            string body = "";

            foreach (DataRow drTemp in dsBorrowOrderSubmission.Tables["BorrowOrder"].Rows)
            {
                body += BorrowOrderSubmissionDetailSet(drTemp);
            }

            return body;
        }


        public static string BorrowOrderSubmissionDetailSet(DataRow drDetail)
        {
            string strBody = "";

            try
            {
                strBody = "2";
                strBody += drDetail["ClientId"].ToString().Trim().PadRight(4, ' ');
                strBody += drDetail["CUSIP"].ToString().Trim().PadRight(9, ' ');
                strBody += drDetail["ContraParty"].ToString().Trim().PadRight(4, ' ');
                strBody += drDetail["Quantity"].ToString().PadLeft(9, '0').Substring(0, 9);
                strBody += "C";
                strBody += drDetail["Comments"].ToString().PadRight(20, ' ').Substring(0, 20); ;
                strBody += new string(' ', 5);
                strBody += drDetail["RebateRateCode"].ToString().PadRight(1, ' ').Substring(0, 1);
                strBody += drDetail["PartialQuantityMinimum"].ToString().PadLeft(9, '0').Substring(0, 9);
                strBody += new string(' ', 8);
                strBody += drDetail["TimeOut"].ToString().PadLeft(4, '0').Substring(0, 4);
                strBody += drDetail["AddToLoanetIndicator"].ToString().PadRight(1, ' ').Substring(0, 1);
                strBody += drDetail["BatchCode"].ToString().PadRight(1, ' ').Substring(0, 1);
                strBody += drDetail["ProfitCenter"].ToString().PadRight(1, ' ').Substring(0, 1);
                strBody += drDetail["MarkParameterType"].ToString().PadRight(1, ' ').Substring(0, 1);
                strBody += "00";
                strBody += new string(' ', 1);
                strBody += new string(' ', 6);
                strBody += new string(' ', 12);
                //                strBody += "\n";
            }
            catch (Exception e)
            {
                // error handling
            }

            return strBody;
        }

        public static string BorrowOrderAddItem(string bookGroup, string book, string secId, string quantity, string rate)
        {
            DataRow drTemp = dsBorrowOrderSubmission.Tables["BorrowOrder"].NewRow();

            drTemp["ClientId"] = bookGroup;
            drTemp["CUSIP"] = secId;
            drTemp["ContraParty"] = book;
            drTemp["Quantity"] = quantity;
            drTemp["CollateralCode"] = " ";
            drTemp["Comments"] = " ";
            drTemp["RebateRate"] = rate;
            drTemp["RebateRateCode"] = "A";
            drTemp["PartialQuantityMinimum"] = 100;
            drTemp["PriceMaximum"] = "";
            drTemp["TimeOut"] = 5;
            drTemp["AddToLoanetIndicator"] = "Y";
            drTemp["BatchCode"] = "A";
            drTemp["ProfitCenter"] = " ";
            drTemp["MarkParameterType"] = " ";
            drTemp["MarkParameterAmount"] = "00";
            drTemp["TrackingIndicator"] = " ";
            drTemp["DividendFlowThru"] = 0;

            dsBorrowOrderSubmission.Tables["BorrowOrder"].Rows.Add(drTemp);

            return BorrowOrderSubmissionDetailSet(drTemp);
        }

        public static void BorrowOrderSubmissionInit()
        {
            dsBorrowOrderSubmission = new DataSet();

            try
            {
                dsBorrowOrderSubmission.Tables.Add("BorrowOrder");
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("ClientId", typeof(string));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("CUSIP", typeof(string));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("ContraParty", typeof(string));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("Quantity", typeof(long));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("CollateralCode", typeof(string));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("Comments", typeof(string));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("RebateRate", typeof(decimal));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("RebateRateCode", typeof(string));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("PartialQuantityMinimum", typeof(long));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("PriceMaximum", typeof(string));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("TimeOut", typeof(int));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("AddToLoanetIndicator", typeof(string));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("BatchCode", typeof(string));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("ProfitCenter", typeof(string));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("MarkParameterType", typeof(string));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("MarkParameterAmount", typeof(string));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("TrackingIndicator", typeof(string));
                dsBorrowOrderSubmission.Tables["BorrowOrder"].Columns.Add("DividendFlowThru", typeof(decimal));

                dsBorrowOrderSubmission.Tables["BorrowOrder"].AcceptChanges();
                dsBorrowOrderSubmission.AcceptChanges();
            }
            catch (Exception e)
            {
                // error handling 
            }
        }

        public static DataSet BorrowOrderData
        {
            get
            {
                return dsBorrowOrderSubmission;
            }
        }

        public static int BorrowOrderRecordCount
        {
            get
            {
                return dsBorrowOrderSubmission.Tables["BorrowOrder"].Rows.Count;
            }
        }

        public static long BorrowOrderQuantityCount
        {
            get
            {
                long temp = 0;

                foreach (DataRow drNew in dsBorrowOrderSubmission.Tables["BorrowOrder"].Rows)
                {
                    temp += long.Parse(drNew["Quantity"].ToString());
                }

                return temp;
            }
        }
    }
}
