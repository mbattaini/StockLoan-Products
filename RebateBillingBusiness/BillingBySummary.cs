using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace RebateBillingBusiness
{
    public class BillingBySummary
    {

        public static DataSet SummaryBySecurity(DataSet dsSummary)
        {
            bool found = false;

            DataSet dsTarget = new DataSet();

            dsTarget.Tables.Add("Rebate");
            dsTarget.Tables["Rebate"].Columns.Add("SecId");
            dsTarget.Tables["Rebate"].Columns.Add("Symbol");
            dsTarget.Tables["Rebate"].Columns.Add("QuantityShorted");
            dsTarget.Tables["Rebate"].Columns.Add("QuantityCovered");
            dsTarget.Tables["Rebate"].Columns.Add("OriginalCharge");
            dsTarget.Tables["Rebate"].Columns.Add("ModifiedCharge");
            dsTarget.AcceptChanges();

            foreach (DataRow dr in dsSummary.Tables["Rebate"].Rows)
            {
                found = false;

                foreach (DataRow dr2 in dsTarget.Tables["Rebate"].Rows)
                {
                    if (dr["SecId"].ToString().ToUpper().Equals(dr2["SecId"].ToString().ToUpper()))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    DataRow tempRecord = dsTarget.Tables["Rebate"].NewRow();
                    tempRecord["SecId"] = dr["SecId"].ToString().ToUpper();
                    tempRecord["Symbol"] = dr["Symbol"].ToString().ToUpper();
                    tempRecord["QuantityShorted"] = 0;
                    tempRecord["QuantityCovered"] = 0;
                    tempRecord["OriginalCharge"] = 0;
                    tempRecord["ModifiedCharge"] = 0;
                    dsTarget.Tables["Rebate"].Rows.Add(tempRecord);
                    dsTarget.AcceptChanges();
                }
            }

            decimal quantityShorted = 0;
            decimal quantityCovered = 0;
            decimal originalCharge = 0;
            decimal modifiedCharge = 0;

            foreach (DataRow dr in dsTarget.Tables["Rebate"].Rows)
            {
                quantityShorted = 0;
                quantityCovered = 0;
                originalCharge = 0;
                modifiedCharge = 0;

                foreach (DataRow dr2 in dsSummary.Tables["Rebate"].Rows)
                {
                    if (dr["SecId"].ToString().ToUpper().Equals(dr2["SecId"].ToString().ToUpper()))
                    {
                        quantityShorted += decimal.Parse(dr2["QuantityShorted"].ToString());
                        quantityCovered += decimal.Parse(dr2["QuantityCovered"].ToString());
                        // originalCharge += decimal.Parse(dr2["OriginalCharge"].ToString());
                        // modifiedCharge += decimal.Parse(dr2["ModifiedCharge"].ToString());
                        originalCharge += (dr2["OriginalCharge"].ToString().Equals("")) ? 0 : decimal.Parse(dr2["OriginalCharge"].ToString());  
                        modifiedCharge += (dr2["ModifiedCharge"].ToString().Equals("")) ? 0 : decimal.Parse(dr2["ModifiedCharge"].ToString()); 
                    }
                }

                dr["QuantityShorted"] = quantityShorted;
                dr["QuantityCovered"] = quantityCovered;
                dr["OriginalCharge"] = originalCharge;
                dr["ModifiedCharge"] = modifiedCharge;
            }

            return dsTarget;
        }

        public static DataSet SummaryByGroupCode(DataSet dsSummary)
        {
            bool found = false;

            DataSet dsTarget = new DataSet();

            dsTarget.Tables.Add("Rebate");
            dsTarget.Tables["Rebate"].Columns.Add("GroupCode");
            dsTarget.Tables["Rebate"].Columns.Add("OriginalCharge");
            dsTarget.Tables["Rebate"].Columns.Add("ModifiedCharge");
            dsTarget.AcceptChanges();

            foreach (DataRow dr in dsSummary.Tables["Rebate"].Rows)
            {
                found = false;

                foreach (DataRow dr2 in dsTarget.Tables["Rebate"].Rows)
                {
                    if (dr["GroupCode"].ToString().ToUpper().Equals(dr2["GroupCode"].ToString().ToUpper()))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    DataRow tempRecord = dsTarget.Tables["Rebate"].NewRow();
                    tempRecord["GroupCode"] = dr["GroupCode"].ToString().ToUpper();
                    tempRecord["OriginalCharge"] = 0;
                    tempRecord["ModifiedCharge"] = 0;
                    dsTarget.Tables["Rebate"].Rows.Add(tempRecord);
                    dsTarget.AcceptChanges();
                }
            }

            decimal originalCharge = 0;
            decimal modifiedCharge = 0;

            foreach (DataRow dr in dsTarget.Tables["Rebate"].Rows)
            {
                originalCharge = 0;
                modifiedCharge = 0;

                foreach (DataRow dr2 in dsSummary.Tables["Rebate"].Rows)
                {
                    if (dr["GroupCode"].ToString().ToUpper().Equals(dr2["GroupCode"].ToString().ToUpper()))
                    {
                        //originalCharge += decimal.Parse(dr2["OriginalCharge"].ToString());
                        //modifiedCharge += decimal.Parse(dr2["ModifiedCharge"].ToString());
                        originalCharge += (dr2["OriginalCharge"].ToString().Equals("")) ? 0 : decimal.Parse(dr2["OriginalCharge"].ToString());  
                        modifiedCharge += (dr2["ModifiedCharge"].ToString().Equals("")) ? 0 : decimal.Parse(dr2["ModifiedCharge"].ToString());  
                    }
                }

                dr["OriginalCharge"] = originalCharge;
                dr["ModifiedCharge"] = modifiedCharge;
            }

            return dsTarget;
        }

        public static DataSet SummaryByAccountNumber(DataSet dsSummary)
        {
            bool found = false;

            DataSet dsTarget = new DataSet();

            dsTarget.Tables.Add("Rebate");
            dsTarget.Tables["Rebate"].Columns.Add("AccountNumber");
            dsTarget.Tables["Rebate"].Columns.Add("OriginalCharge");
            dsTarget.Tables["Rebate"].Columns.Add("ModifiedCharge");
            dsTarget.AcceptChanges();

            foreach (DataRow dr in dsSummary.Tables["Rebate"].Rows)
            {
                found = false;

                foreach (DataRow dr2 in dsTarget.Tables["Rebate"].Rows)
                {
                    if (dr["AccountNumber"].ToString().ToUpper().Equals(dr2["AccountNumber"].ToString().ToUpper()))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    DataRow tempRecord = dsTarget.Tables["Rebate"].NewRow();
                    tempRecord["AccountNumber"] = dr["AccountNumber"].ToString().ToUpper();
                    tempRecord["OriginalCharge"] = 0;
                    tempRecord["ModifiedCharge"] = 0;
                    dsTarget.Tables["Rebate"].Rows.Add(tempRecord);
                    dsTarget.AcceptChanges();
                }
            }

            decimal originalCharge = 0;
            decimal modifiedCharge = 0;

            foreach (DataRow dr in dsTarget.Tables["Rebate"].Rows)
            {
                originalCharge = 0;
                modifiedCharge = 0;

                foreach (DataRow dr2 in dsSummary.Tables["Rebate"].Rows)
                {
                    if (dr["AccountNumber"].ToString().ToUpper().Equals(dr2["AccountNumber"].ToString().ToUpper()))
                    {
                        //originalCharge += decimal.Parse(dr2["OriginalCharge"].ToString());
                        //modifiedCharge += decimal.Parse(dr2["ModifiedCharge"].ToString());
                        originalCharge += (dr2["OriginalCharge"].ToString().Equals("")) ? 0 : decimal.Parse(dr2["OriginalCharge"].ToString());  
                        modifiedCharge += (dr2["ModifiedCharge"].ToString().Equals("")) ? 0 : decimal.Parse(dr2["ModifiedCharge"].ToString());  
                    }
                }

                dr["OriginalCharge"] = originalCharge;
                dr["ModifiedCharge"] = modifiedCharge;
            }

            return dsTarget;
        }

        public static DataSet SummaryByCharges(DataSet dsSummary)
        {   //DC  Logan's Month End Summary Charges, 2010-12-28 

            bool found = false;
            DataSet dsTarget = new DataSet();

            try
            {

                dsTarget.Tables.Add("Rebate");
                dsTarget.Tables["Rebate"].Columns.Add("GroupCode");
                dsTarget.Tables["Rebate"].Columns.Add("AccountNumber");
                dsTarget.Tables["Rebate"].Columns.Add("OriginalCharge");    // Not visible but kept for code compatibility
                dsTarget.Tables["Rebate"].Columns.Add("ModifiedCharge");    // Not viaible but kept for code compatibility 
                dsTarget.Tables["Rebate"].Columns.Add("SumOfCharges");      //DC 2010-12-28  new column 
                dsTarget.AcceptChanges();

                foreach (DataRow dr in dsSummary.Tables["Rebate"].Rows)
                {
                    found = false;

                    foreach (DataRow dr2 in dsTarget.Tables["Rebate"].Rows)
                    {
                        if (dr["GroupCode"].ToString().ToUpper().Equals(dr2["GroupCode"].ToString().ToUpper()) &&
                            dr["AccountNumber"].ToString().ToUpper().Equals(dr2["AccountNumber"].ToString().ToUpper()))
                        {
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        DataRow tempRecord = dsTarget.Tables["Rebate"].NewRow();
                        tempRecord["GroupCode"] = dr["GroupCode"].ToString().ToUpper();
                        tempRecord["AccountNumber"] = dr["AccountNumber"].ToString().ToUpper();
                        tempRecord["OriginalCharge"] = 0;
                        tempRecord["ModifiedCharge"] = 0;
                        tempRecord["SumOfCharges"] = 0;
                        dsTarget.Tables["Rebate"].Rows.Add(tempRecord);
                        dsTarget.AcceptChanges();
                    }
                }

                decimal originalCharge = 0;
                decimal modifiedCharge = 0;
                decimal sumOfCharge = 0;

                foreach (DataRow dr in dsTarget.Tables["Rebate"].Rows)
                {
                    originalCharge = 0;
                    modifiedCharge = 0;
                    sumOfCharge = 0;

                    foreach (DataRow dr2 in dsSummary.Tables["Rebate"].Rows)
                    {
                        if (dr["GroupCode"].ToString().ToUpper().Equals(dr2["GroupCode"].ToString().ToUpper()) &&
                            dr["AccountNumber"].ToString().ToUpper().Equals(dr2["AccountNumber"].ToString().ToUpper()))
                        {
                            //originalCharge += decimal.Parse(dr2["OriginalCharge"].ToString());    // original failed at NULL value 
                            //modifiedCharge += decimal.Parse(dr2["ModifiedCharge"].ToString());    // original failed at NULL value 
                            originalCharge += (dr2["OriginalCharge"].ToString().Equals("")) ? 0 : decimal.Parse(dr2["OriginalCharge"].ToString());  
                            modifiedCharge += (dr2["ModifiedCharge"].ToString().Equals("")) ? 0 : decimal.Parse(dr2["ModifiedCharge"].ToString());  
                            sumOfCharge += (dr2["ModifiedCharge"].ToString().Equals("")) ? 0 : decimal.Parse(dr2["ModifiedCharge"].ToString());     //DC
                        }
                    }

                    dr["OriginalCharge"] = originalCharge;      // Kept for compatibility 
                    dr["ModifiedCharge"] = modifiedCharge;      // Kept for compatibility 
                    dr["SumOfCharges"] = sumOfCharge;
                }

                return dsTarget;
            }
            catch 
            {
                throw;
            }

        }

    }
}
