using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.DataAccess;

using StockLoan.Common;

namespace StockLoan.Business
{
    public class Positions
    {

        public static DataSet BoxPositionGet(string bizDate, string bookGroup, string secId)
        {
            DataSet dsTemp = new DataSet();

            try
            {
                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date is required");
                }


                dsTemp = DBPositions.BoxPositionsGet(bizDate, bookGroup, secId);

                return dsTemp;
            }
            catch
            {
                throw;
            }
        }

        public static DataSet BoxPositionItemGet(string bizDate, string bookGroup, string secId)
        {
            DataSet dsTemp = new DataSet();

            try
            {
                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date is required");
                }


                dsTemp = DBPositions.BoxPositionsGet(bizDate, bookGroup, secId);

                return dsTemp;
            }
            catch
            {
                throw;
            }
        }

        public static DataSet BoxPositionLookupGet(string boxList, string bookGroup, bool includeRates)
        {
            List list = new List(boxList);
            DataSet dsBoxItems = new DataSet();
            dsBoxItems.Tables.Add("Box");
            dsBoxItems.Tables["Box"].Columns.Add("SecId");
            dsBoxItems.Tables["Box"].Columns.Add("Quantity");
            dsBoxItems.Tables["Box"].Columns.Add("BoxQuantity");
            dsBoxItems.Tables["Box"].Columns.Add("Price");
            dsBoxItems.Tables["Box"].Columns.Add("Amount");
            dsBoxItems.Tables["Box"].Columns.Add("Rate");
            dsBoxItems.Tables["Box"].Columns.Add("Profit");
            dsBoxItems.Tables["Box"].Columns.Add("IsTrashed");
            dsBoxItems.AcceptChanges();

            if (list.Status)
            {
                ListItem listItem = new ListItem();

                for (int index = 0; index < list.Count; index++)
                {
                    listItem = list.ItemGet(index);

                    DataRow drItem = dsBoxItems.Tables["Box"].NewRow();
                    drItem["SecId"] = listItem.SecId;
                    drItem["Quantity"] = listItem.Quantity;
                    drItem["IsTrashed"] = false;
                    dsBoxItems.Tables["Box"].Rows.Add(drItem);
                }

                dsBoxItems.AcceptChanges();
            }

            if (!bookGroup.Equals(""))
            {
                DataSet dsBox;
                DataSet dsItems;
                DataSet dsRates;


                decimal amount;
                decimal rate;
                decimal margin = 1.02M;

                foreach (DataRow drItem in dsBoxItems.Tables["Box"].Rows)
                {
                    amount = -1;
                    rate = 1000;
                    dsItems = DBPositions.BoxPositionsGet(DBStandardFunctions.BizDateStrGet(bookGroup, "BIZDATE"), bookGroup, drItem["SecId"].ToString());
                    dsRates = DBInventory.InventoryRatesGet(DBStandardFunctions.BizDateStrGet(bookGroup, "BIZDATE"), bookGroup, "", drItem["SecId"].ToString(), "", "", "");

                    if (dsItems.Tables["BoxPosition"].Rows.Count > 0)
                    {

                        Console.Write(dsItems.Tables["BoxPosition"].Rows[0]["Price"].ToString() + " " + drItem["Quantity"].ToString());

                        try
                        {
                            drItem["BoxQuantity"] = long.Parse(dsItems.Tables["BoxPosition"].Rows[0]["ExDeficitSettled"].ToString());
                        }
                        catch
                        {
                            drItem["BoxQuantity"] = 0;
                        }

                        try
                        {

                            drItem["Amount"] = float.Parse(dsItems.Tables["BoxPosition"].Rows[0]["Price"].ToString()) * long.Parse(drItem["BoxQuantity"].ToString());
                            amount = decimal.Parse(drItem["Amount"].ToString());
                        }
                        catch
                        {
                            amount = -1;
                            drItem["Amount"] = 0;
                        }
                    }
                    else
                    {
                        drItem["BoxQuantity"] = 0;
                        drItem["Amount"] = 0;
                    }


                    if (dsRates.Tables["InventoryRates"].Rows.Count > 0)
                    {
                        try
                        {
                            drItem["Rate"] = decimal.Parse(dsItems.Tables["InventoryRates"].Rows[0]["Rate"].ToString());
                            rate = decimal.Parse(drItem["Rate"].ToString());
                        }
                        catch
                        {
                            rate = 1000;
                            drItem["Rate"] = "";
                        }
                    }
                    else
                    {
                        drItem["Rate"] = "";
                    }

                    if (amount != -1 & rate != 1000)
                    {

                        drItem["Profit"] = ((amount * margin) * (rate / 100)) / 360;
                    }
                }

            }

            dsBoxItems.AcceptChanges();

            return dsBoxItems;
        }

        public static DataSet BoxSummaryDataConfig(string bizDate, string bookGroup, bool IsOptimistic, string hairCutUser)
        {
            DataSet dsBoxSummary = DBPositions.BoxSummaryGet(bizDate, bookGroup);

            long available;
            long recallBalance;
            long deliveryBalance;

            string secId = "";

            double hairCut = 0;

            try
            {
                dsBoxSummary.Tables["BoxSummary"].Columns.Add("AvailableModified", typeof(long));
                dsBoxSummary.Tables["BoxSummary"].Columns.Add("AvailableAmount", typeof(long));
                dsBoxSummary.Tables["BoxSummary"].Columns.Add("PledgeUndo", typeof(long));
                dsBoxSummary.Tables["BoxSummary"].Columns.Add("Borrow", typeof(long));
                dsBoxSummary.Tables["BoxSummary"].Columns.Add("BorrowAmount", typeof(long));
                dsBoxSummary.Tables["BoxSummary"].Columns.Add("Recall", typeof(long));
                dsBoxSummary.AcceptChanges();

                hairCut = double.Parse(hairCutUser);
                hairCut = hairCut / 100;
            }
            catch
            {
                hairCut = 0;
            }

            try
            {
                foreach (DataRow row in dsBoxSummary.Tables["BoxSummary"].Rows)
                {
                    secId = "[" + row["SecId"].ToString().Trim() + "] "; 

                    recallBalance = (long)row["LoanRecalls"] - (long)row["BorrowRecalls"];

                    if (IsOptimistic)
                    {
                        deliveryBalance = (long)row["ReceiveFails"];
                    }
                    else
                    {
                        deliveryBalance = 0;
                    }


                    available = (long)row["Available"];
                    available = available + deliveryBalance;

                    if (available <= 0)
                    {
                        if ((available + (long)row["OnPledge"]) >= 0)
                        {
                            row["PledgeUndo"] = -available;
                        }
                        else
                        {
                            row["PledgeUndo"] = row["OnPledge"];
                        }


                        if ((available + (long)row["Loans"] - (long)row["LoanRecalls"]) >= 0)
                        {
                            row["Recall"] = -(available);
                        }
                        else if (available < 0)
                        {
                            row["Recall"] = (long)row["Loans"] - (long)row["LoanRecalls"];
                        }
                        else
                        {
                            row["Recall"] = 0;
                        }

                        row["Borrow"] = -(available);
                    }
                    else
                    {
                        row["PledgeUndo"] = 0;
                        if (available >= (long)row["LoanRecalls"])
                        {
                            row["Recall"] = -(long)row["LoanRecalls"];

                        }
                        else if (available < (long)row["LoanRecalls"])
                        {
                            row["Recall"] = -available;

                            if (row["BaseType"].ToString().Equals("B"))
                            {
                                row["Recall"] = (long)row["Recall"] - ((long)row["Recall"] % 100000);
                            }
                            else
                            {
                                row["Recall"] = (long)row["Recall"] - ((long)row["Recall"] % 100);
                            }
                        }

                        if (available > (long)row["Borrows"])
                        {
                            row["Borrow"] = -(long)row["Borrows"];
                        }
                        else if (available < (long)row["Borrows"])
                        {
                            row["Borrow"] = -available;
                        }
                        else
                        {
                            row["Borrow"] = 0;
                        }
                    }

                    if ((long)row["Recall"] > 0)
                    {
                        if ((long)row["LoanRecalls"] >= (long)row["Recall"])
                        {
                            row["Recall"] = 0;
                        }
                        else
                        {
                            row["Recall"] = (long)row["Recall"] - (long)row["LoanRecalls"];
                        }
                    }
                    else if ((long)row["Recall"] < 0)
                    {
                        if (available > 0)
                        {
                            row["Recall"] = -(long)row["LoanRecalls"];
                        }
                        else
                        {
                            row["Recall"] = available + (long)row["Recall"];
                        }
                    }

                    if ((long)row["Borrow"] > 0)
                    {
                        if (row["BaseType"].ToString().Equals("B") && (((long)row["Borrow"] % 100000) > 0))
                        {
                            row["Borrow"] = (long)row["Borrow"] - ((long)row["Borrow"] % 100000) + 100000;
                        }
                        else if (((long)row["Borrow"] % 100) >= 0)
                        {
                            row["Borrow"] = (long)row["Borrow"] - ((long)row["Borrow"] % 100) + 100;
                        }
                    }
                    else if ((long)row["Borrow"] < 0)
                    {
                        if (row["BaseType"].ToString().Equals("B"))
                        {
                            row["Borrow"] = (long)row["Borrow"] - ((long)row["Borrow"] % 100000);
                        }
                        else
                        {
                            row["Borrow"] = (long)row["Borrow"] - ((long)row["Borrow"] % 100);
                        }
                    }


                    row["AvailableModified"] = (long)row["Available"] - ((long)row["Available"] % 100);
                    
                    if (!row["LastPrice"].Equals(DBNull.Value))
                    {
                        if (row["BaseType"].ToString().Equals("B"))
                        {                            
                            row["AvailableAmount"] = (long)((long)row["Available"] * (double)row["LastPrice"] / 100.0);
                            row["BorrowAmount"] = (long)((long)row["Borrow"] * (double)row["LastPrice"] / 100.0);                         
                        }
                        else
                        {                            
                            row["AvailableAmount"] = (long)((long)row["Available"] * (double)row["LastPrice"]);
                            row["BorrowAmount"] = (long)((long)row["Borrow"] * (double)row["LastPrice"]);                         
                        }
                    }
                    else
                    {
                        row["AvailableAmount"] = DBNull.Value;
                        row["BorrowAmount"] = DBNull.Value;                        
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write("[" + secId + e.Message + "] " + " [Positions.BoxSummaryDataConfig]", Log.Error, 1);
            }

            return dsBoxSummary;
        }        
    }
}
