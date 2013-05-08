using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using StockLoan.Main;

namespace StockLoan.MainBusiness
{
    public class Contracts
    {

        public static string TermDate(DataSet dsContracts, string contractId)
        {
            string result;
            string lastDate = "2001-01-01";

            foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
            {
                if (DateTime.Parse(lastDate) <= DateTime.Parse(dr["BizDate"].ToString()))
                {
                    lastDate = dr["BizDate"].ToString();
                }
            }

            result = lastDate;

            return result;        
        }
        public static void PopulateTermDate(ref DataSet dsContracts)
        {
            string termDate;

            DataSet dsContractTemp = new DataSet();
            dsContractTemp = dsContracts;

            foreach (DataRow dr in dsContractTemp.Tables["Contracts"].Rows)
            {
                termDate = TermDate(dsContractTemp, dr["ContractId"].ToString());

                foreach (DataRow dr2 in dsContracts.Tables["Contracts"].Rows)
                {
                    if (dr2["TermDate"].ToString().Equals("") && dr2["ContractId"].ToString().Equals(dr["ContractId"].ToString()))
                    {
                        dr2["TermDate"] = termDate;
                    }
                }

                termDate = "";
            }

            dsContracts.AcceptChanges();
        }
        public static DataSet SummaryByCashPools(string bizdate, ref DataSet dsContracts)
        {

            DataSet dsTarget = new DataSet();
            bool found;

            dsTarget.Tables.Add("ContractSummary");
            dsTarget.Tables["ContractSummary"].Columns.Add("BookGroup", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("Book", typeof(string));            
            dsTarget.Tables["ContractSummary"].Columns.Add("CurrencyIso", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("BorrowQuantity", typeof(long));
            dsTarget.Tables["ContractSummary"].Columns.Add("BorrowAmount", typeof(decimal));            
            dsTarget.Tables["ContractSummary"].Columns.Add("LoanQuantity", typeof(long));
            dsTarget.Tables["ContractSummary"].Columns.Add("LoanAmount", typeof(decimal));
            dsTarget.AcceptChanges();


            foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
            {
                found = false;

                foreach (DataRow dr2 in dsTarget.Tables["ContractSummary"].Rows)
                {
                    if (dr["BookGroup"].ToString().Equals(dr2["BookGroup"].ToString()) &&
                        dr["Book"].ToString().Equals(dr2["Book"].ToString()) &&
                        dr["CurrencyIso"].ToString().Equals(dr2["CurrencyIso"].ToString()))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    DataRow tempRecord = dsTarget.Tables["ContractSummary"].NewRow();
                    tempRecord["BookGroup"] = dr["BookGroup"].ToString().ToUpper();
                    tempRecord["Book"] = dr["Book"].ToString().ToUpper();                       
                    tempRecord["CurrencyIso"] = dr["CurrencyIso"].ToString();
                    tempRecord["BorrowQuantity"] = 0;
                    tempRecord["BorrowAmount"] = 0;
                    tempRecord["LoanQuantity"] = 0;
                    tempRecord["LoanAmount"] = 0;                    

                    dsTarget.Tables["ContractSummary"].Rows.Add(tempRecord);
                    dsTarget.AcceptChanges();
                }
            }

            dsTarget.AcceptChanges();

            long borrowQuantity = 0;
            long loanQuantity = 0;

            decimal borrowAmount = 0;
            decimal loanAmount = 0;
            
            foreach (DataRow dr in dsTarget.Tables["ContractSummary"].Rows)
            {
                borrowQuantity = 0;
                loanQuantity = 0;
                borrowAmount = 0;
                loanAmount = 0;

                foreach (DataRow dr2 in dsContracts.Tables["Contracts"].Rows)
                {

                    if (dr["BookGroup"].ToString().Equals(dr2["BookGroup"].ToString()) &&
                        dr["Book"].ToString().Equals(dr2["Book"].ToString()) &&
                        dr["CurrencyIso"].ToString().Equals(dr2["CurrencyIso"].ToString()))
                    {
                        switch (dr2["ContractType"].ToString())
                        {
                            case "B":
                                borrowQuantity += (long)dr2["Quantity"];
                                borrowAmount = (decimal)dr2["Amount"];
                                break;

                            case "L":
                                loanQuantity = (long)dr2["Quantity"];
                                loanAmount = (decimal)dr2["Amount"];
                                break;
                        }
                    }
                }
            }

            dsTarget.AcceptChanges();

            return dsTarget;
        }
        public static void SummaryByBookBilling(string bizDate, ref DataSet dsContracts, ref DataSet dsTarget)
        {
            bool isFound = false;

            dsTarget.Tables.Add("BillingSummary");
            dsTarget.Tables["BillingSummary"].Columns.Add("BookGroup", typeof(string));
            dsTarget.Tables["BillingSummary"].Columns.Add("Book", typeof(string));
            dsTarget.Tables["BillingSummary"].Columns.Add("BookName", typeof(string));
            dsTarget.Tables["BillingSummary"].Columns.Add("CurrencyIso", typeof(string));
            dsTarget.Tables["BillingSummary"].Columns.Add("PoolCode", typeof(string));
            dsTarget.Tables["BillingSummary"].Columns.Add("CashInBalance", typeof(long));
            dsTarget.Tables["BillingSummary"].Columns.Add("CashInRecieveable", typeof(long));
            dsTarget.Tables["BillingSummary"].Columns.Add("CashOutBalance", typeof(long));
            dsTarget.Tables["BillingSummary"].Columns.Add("CashOutPayable", typeof(long));
            dsTarget.Tables["BillingSummary"].Columns.Add("CashNet", typeof(long));
            dsTarget.Tables["BillingSummary"].Columns.Add("CashNetReceivePayable", typeof(long));
            dsTarget.AcceptChanges();

            //Create blank dataset with distinct values
            isFound = false;

            foreach (DataRow drRow1 in dsContracts.Tables["Contracts"].Rows)
            {
                foreach (DataRow drRow2 in dsTarget.Tables["BillingSummary"].Rows)
                {
                    if (drRow1["BookGroup"].ToString().Equals(drRow2["BooKGroup"].ToString()) &&
                        drRow1["Book"].ToString().Equals(drRow2["Book"].ToString()) &&
                        drRow1["CurrencyIso"].ToString().Equals(drRow2["CurrencyIso"].ToString()) &&
                        drRow1["PoolCode"].ToString().Equals(drRow2["PoolCode"].ToString()))
                    {
                        isFound = true;
                    }
                } 

                if (!isFound)
                {
                    DataRow tempDr = dsTarget.Tables["BillingSummary"].NewRow();
                    
                    tempDr["BookGroup"] = drRow1["BookGroup"].ToString();
                    tempDr["Book"] = drRow1["Book"].ToString();
                    tempDr["BookName"] = drRow1["BookName"].ToString();
                    tempDr["CurrencyIso"] = drRow1["CurrencyIso"].ToString();
                    tempDr["PoolCode"] = drRow1["PoolCode"].ToString();
                    tempDr["CashInBalance"] = 0;
                    tempDr["CashInRecieveable"] = 0;
                    tempDr["CashOutBalance"] = 0;
                    tempDr["CashOutPayable"] = 0;
                    tempDr["CashNet"] = 0;
                    tempDr["CashNetReceivePayable"] = 0;

                    dsTarget.Tables["BillingSummary"].Rows.Add(tempDr);
                }
            }
            
            
            //Calculate Cash In Balance
            {
                isFound = false;

                foreach (DataRow drRow1 in dsTarget.Tables["BillingSummary"].Rows)
                {
                    foreach (DataRow drRow2 in dsContracts.Tables["Contracts"].Rows)
                    {

                    }
                    

                }
            }


        }
        public decimal TotalAmount(ref DataSet dsContracts, string bookGroup, string book, string contractType, string currencyIso, bool isActive, string bizDate)
        {
            decimal value = 0;

            foreach (DataRow drRow in dsContracts.Tables["Contracts"].Rows)
            {
                if (drRow["BizDate"].ToString().Equals(bizDate) &&
                    drRow["BookGroup"].ToString().Equals(bookGroup) &&
                    drRow["Book"].ToString().Equals(book) &&
                    drRow["ContractType"].ToString().Equals(contractType) &&
                    drRow["CurrencyIso"].ToString().Equals(currencyIso) &&
                    ((StandardFunctions.CompareDates(drRow["SettleDate"].ToString(), bizDate) == -1) || (StandardFunctions.CompareDates(drRow["SettleDate"].ToString(), bizDate) == 0)))
                {
                    value += decimal.Parse(drRow["Amount"].ToString());
                }
            }

            return value;
       }
        public static void SummaryByContractsBilling(ref DataSet dsContracts, ref DataSet dsTarget)
        {
            bool found;

            dsTarget.Tables.Add("ContractSummary");
            dsTarget.Tables["ContractSummary"].Columns.Add("BookGroup", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("Book", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("ContractId", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("ContractType", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("SecId", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("Symbol", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("Quantity", typeof(long));
            dsTarget.Tables["ContractSummary"].Columns.Add("CurrencyIso", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("FeeAmount", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("RebateAmount", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("TotalRebate", typeof(decimal));
            dsTarget.AcceptChanges();


            foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
            {
                found = false;

                foreach (DataRow dr2 in dsTarget.Tables["ContractSummary"].Rows)
                {
                    if (dr["BookGroup"].ToString().Equals(dr2["BookGroup"].ToString()) &&
                        dr["Book"].ToString().Equals(dr2["Book"].ToString()) &&
                        dr["ContractId"].ToString().Equals(dr2["ContractId"].ToString()) &&
                        dr["ContractType"].ToString().Equals(dr2["ContractType"].ToString()))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    DataRow tempRecord = dsTarget.Tables["ContractSummary"].NewRow();
                    tempRecord["BookGroup"] = dr["BookGroup"].ToString().ToUpper();
                    tempRecord["Book"] = dr["Book"].ToString().ToUpper();
                    tempRecord["ContractId"] = dr["ContractId"].ToString().ToUpper();
                    tempRecord["ContractType"] = dr["ContractType"].ToString().ToUpper();
                    tempRecord["SecId"] = dr["SecId"].ToString().ToUpper();
                    tempRecord["Symbol"] = dr["Symbol"].ToString().ToUpper();
                    tempRecord["Quantity"] = long.Parse(dr["Quantity"].ToString()) * -1;
                    tempRecord["CurrencyIso"] = dr["CurrencyIso"].ToString();
                    tempRecord["FeeAmount"] = 0;
                    tempRecord["RebateAmount"] = 0;
                    tempRecord["TotalRebate"] = 0;

                    dsTarget.Tables["ContractSummary"].Rows.Add(tempRecord);
                    dsTarget.AcceptChanges();
                }
            }

            dsTarget.AcceptChanges();

            decimal totalRebate = 0;
            decimal feeAmount = 0;
            decimal rebateAmount = 0;

            foreach (DataRow dr in dsTarget.Tables["ContractSummary"].Rows)
            {
                totalRebate = 0;
                feeAmount = 0;
                rebateAmount = 0;

                foreach (DataRow dr2 in dsContracts.Tables["Contracts"].Rows)
                {
                    if (dr["ContractId"].ToString().Equals(dr2["ContractId"].ToString()))
                    {
                        feeAmount += (decimal)dr2["FeeAmount"];
                        rebateAmount += (decimal)dr2["RebateAmount"];
                        totalRebate += (decimal)dr2["PL"];
                    }
                }

                dr["FeeAmount"] = feeAmount;
                dr["RebateAmount"] = rebateAmount;
                dr["TotalRebate"] = totalRebate;
            }

            dsTarget.AcceptChanges();
        }
        public static void SummaryByBookProfitLoss(ref DataSet dsContracts, ref DataSet dsTarget, string bookGroup)
        {
            dsTarget = new DataSet();

            StandardFunctions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");

            dsContracts.Tables["Contracts"].Columns.Add("FeeRate", typeof(decimal));
            dsContracts.Tables["Contracts"].Columns.Add("CashPool", typeof(decimal));
            dsContracts.Tables["Contracts"].Columns.Add("FeeAmount", typeof(decimal));
            dsContracts.Tables["Contracts"].Columns.Add("RebateAmount", typeof(decimal));
            dsContracts.Tables["Contracts"].Columns.Add("PL", typeof(decimal));
            dsContracts.AcceptChanges();

            foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
            {
                if (DateTime.Parse(dr["BizDate"].ToString()) >= DateTime.Parse(dr["SettleDate"].ToString()))
                {
                    switch (dr["Fund"].ToString())
                    {
                        case ("REBATE"):
                            dr["FeeRate"] = dr["RebateRate"];
                            dr["FeeAmount"] = 0;
                            dr["CashPool"] = 0;
                            dr["RebateAmount"] = ((decimal)dr["Amount"] * ((decimal)dr["RebateRate"] / 100)) / 360;

                            if (dr["ContractType"].ToString().Equals("L"))
                            {
                                dr["RebateAmount"] = (decimal)dr["RebateAmount"] * -1;
                            }

                            if (dr["ContractType"].ToString().Equals("B"))
                            {
                                dr["PL"] = (decimal)dr["RebateAmount"] + (decimal)dr["FeeAmount"];
                            }
                            else
                            {
                                dr["PL"] = (decimal)dr["FeeAmount"] + (decimal)dr["RebateAmount"];
                            }
 

                            break;

                        default:
                            dr["FeeRate"] = (decimal)dr["FundingRate"];
                            dr["CashPool"] = (decimal)dr["Price"] * (long)dr["Quantity"];
                            dr["FeeAmount"] = ((decimal)dr["CashPool"] * ((decimal)dr["RebateRate"] / 100)) / 360;
                            dr["RebateAmount"] = ((decimal)dr["Amount"] * ((decimal)dr["FundingRate"] / 100)) / 360;

                            dr["FeeRate"] = (decimal)dr["RebateRate"];
                            dr["RebateRate"] = (decimal)dr["FundingRate"];

                            if (dr["ContractType"].ToString().Equals("B"))
                            {
                                dr["FeeAmount"] = (decimal)dr["FeeAmount"] * -1;
                                dr["PL"] = (decimal)dr["RebateAmount"] + (decimal)dr["FeeAmount"];
                            }
                            else if (dr["ContractType"].ToString().Equals("L"))
                            {
                                dr["RebateAmount"] = (decimal)dr["RebateAmount"] * -1;
                                dr["PL"] = (decimal)dr["FeeAmount"] + (decimal)dr["RebateAmount"];
                            }
                            break;
                    }
                }
                else
                {
                    dr["FeeRate"] = (decimal)dr["FundingRate"];
                    dr["RebateAmount"] = 0;
                    dr["CashPool"] = 0;
                    dr["FeeAmount"] = 0;
                    dr["PL"] = 0;
                }
            }

            dsContracts.AcceptChanges();

            dsTarget.Tables.Add("ContractSummary");            
            dsTarget.Tables["ContractSummary"].Columns.Add("BookGroup", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("Book", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("BookName", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("CurrencyIso", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("BorrowCashPool", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("LoanCashPool", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("BorrowRebate", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("LoanRebate", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("TotalRebate", typeof(decimal));
            dsTarget.AcceptChanges();

            bool found = false;

            foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
            {
                found = false;

                foreach (DataRow dr2 in dsTarget.Tables["ContractSummary"].Rows)
                {
                    if (dr["BookGroup"].ToString().Equals(dr2["BookGroup"].ToString()) &&
                        dr["Book"].ToString().Equals(dr2["Book"].ToString()) &&
                        dr["CurrencyIso"].ToString().Equals(dr2["CurrencyIso"].ToString()))
                    {
                        if (dr["ContractType"].ToString().Equals("B"))
                        {
                            dr2["BorrowRebate"] = (decimal)dr2["BorrowRebate"] + (decimal)dr["PL"];
                            dr2["BorrowCashPool"] = (decimal)dr2["BorrowCashPool"] - (decimal)dr["Amount"];
                        }
                        else
                        {
                            dr2["LoanRebate"] = (decimal)dr2["LoanRebate"] - (decimal)dr["PL"];
                            dr2["LoanCashPool"] = (decimal)dr2["LoanCashPool"] + (decimal)dr["Amount"];
                        }
                        found = true;
                    }
                }

                if (!found)
                {
                    DataRow tempRecord = dsTarget.Tables["ContractSummary"].NewRow();                    
                    tempRecord["BookGroup"] = dr["BookGroup"].ToString().ToUpper();
                    tempRecord["Book"] = dr["Book"].ToString().ToUpper();
                    tempRecord["BookName"] = dr["BookName"].ToString().ToUpper();
                    tempRecord["CurrencyIso"] = dr["CurrencyIso"].ToString().ToUpper();
                    if (dr["ContractType"].ToString().Equals("B"))
                    {
                        tempRecord["BorrowRebate"] = (decimal)dr["PL"];
                        tempRecord["BorrowCashPool"] = (decimal)dr["Amount"] * -1;
                        tempRecord["LoanRebate"] = 0;
                        tempRecord["LoanCashPool"] = 0;
                    }
                    else
                    {

                        tempRecord["BorrowRebate"] = 0;
                        tempRecord["BorrowCashPool"] = 0;
                        tempRecord["LoanRebate"] = (decimal)dr["PL"] * -1;
                        tempRecord["LoanCashPool"] = (decimal)dr["Amount"];
                    }

                    dsTarget.Tables["ContractSummary"].Rows.Add(tempRecord);
                    dsTarget.AcceptChanges();
                }
            }

            foreach (DataRow dr in dsTarget.Tables["ContractSummary"].Rows)
            {
                dr["TotalRebate"] = (decimal)dr["BorrowRebate"] + (decimal)dr["LoanRebate"];
            }

            dsTarget.AcceptChanges();
        }
        public static void SummaryBySecurity(DataSet dsContracts, ref DataSet dsTarget, string bookGroup, bool usePoolCode)
        {
            bool found = false;

            StandardFunctions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");

            dsTarget = new DataSet();
            dsTarget.Tables.Add("ContractSummary");

            dsTarget.Tables["ContractSummary"].Columns.Add("BookGroup", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("SecId", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("Description", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("Symbol", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("Isin", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("CurrencyIso", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("PoolCode", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("Price", typeof(float));
            dsTarget.Tables["ContractSummary"].Columns.Add("BorrowQuantity", typeof(long));
            dsTarget.Tables["ContractSummary"].Columns.Add("BorrowAmount", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("LoanQuantity", typeof(long));
            dsTarget.Tables["ContractSummary"].Columns.Add("LoanAmount", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("BorrowRate", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("LoanRate", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("BorrowValue", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("LoanValue", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("TotalValue", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("NetQuantity", typeof(long));
            dsTarget.Tables["ContractSummary"].Columns.Add("NetAmount", typeof(decimal));
            dsTarget.AcceptChanges();

            if (dsContracts != null)
            {
                foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
                {
                    found = false;

                    foreach (DataRow dr2 in dsTarget.Tables["ContractSummary"].Rows)
                    {
                        if ((dr["BookGroup"].ToString().ToUpper().Equals(dr2["BookGroup"].ToString().ToUpper())) &&
                            (dr["SecId"].ToString().ToUpper().Equals(dr2["SecId"].ToString().ToUpper())) &&
                            (dr["CurrencyIso"].ToString().ToUpper().Equals(dr2["CurrencyIso"].ToString().ToUpper())) &&
                            (dr["PoolCode"].ToString().ToUpper().Equals(dr2["PoolCode"].ToString().ToUpper())) &&                            
                            bool.Parse(dr["IsActive"].ToString()) == true)
                        {
                            if (usePoolCode)
                            {
                                if (dr["PoolCode"].ToString().ToUpper().Equals(dr2["PoolCode"].ToString().ToUpper()))
                                {
                                    found = true;
                                }
                                else
                                {
                                    found = false;
                                }
                            }
                            else
                            {
                                found = true;
                            }
                        }
                    }

                    if (!found && bool.Parse(dr["IsActive"].ToString()) == true)
                    {

                        DataRow tempRecord = dsTarget.Tables["ContractSummary"].NewRow();
                        tempRecord["BookGroup"] = dr["BookGroup"].ToString();
                        tempRecord["SecId"] = dr["SecId"].ToString().ToUpper();
                        tempRecord["Description"] = dr["Description"].ToString().ToUpper();
                        tempRecord["Symbol"] = dr["Symbol"].ToString().ToUpper();
                        tempRecord["Isin"] = dr["Isin"].ToString().ToUpper();
                        tempRecord["CurrencyIso"] = dr["CurrencyIso"].ToString().ToUpper();
                        tempRecord["BorrowQuantity"] = 0;
                        tempRecord["BorrowAmount"] = 0;
                        tempRecord["LoanQuantity"] = 0;
                        tempRecord["LoanAmount"] = 0;
                        tempRecord["BorrowRate"] = 0;
                        tempRecord["LoanRate"] = 0;

                        if (usePoolCode)
                        {
                            tempRecord["PoolCode"] = dr["PoolCode"].ToString();
                        }
                        else
                        {
                            tempRecord["PoolCode"] = "";
                        }

                        dsTarget.Tables["ContractSummary"].Rows.Add(tempRecord);
                        dsTarget.AcceptChanges();
                    }
                }
            }

            long borrowQuantity = 0;
            long loanQuantity = 0;

            long borrowRateCount = 0;
            long loanRateCount = 0;

            decimal borrowRate = 0;
            decimal loanRate = 0;


            decimal borrowAmount = 0;
            decimal loanAmount = 0;

            foreach (DataRow dr in dsTarget.Tables["ContractSummary"].Rows)
            {
                borrowQuantity = 0;
                loanQuantity = 0;

                borrowAmount = 0;
                loanAmount = 0;

                borrowRateCount = 0;
                loanRateCount = 0;

                borrowRate = 0;
                loanRate = 0;

                foreach (DataRow dr2 in dsContracts.Tables["Contracts"].Rows)
                {
                    if ((dr["BookGroup"].ToString().ToUpper().Equals(dr2["BookGroup"].ToString().ToUpper())) &&
                        (dr["SecId"].ToString().Trim().ToUpper().Equals(dr2["SecId"].ToString().Trim().ToUpper())) &&
                         (dr["PoolCode"].ToString().ToUpper().Equals(dr2["PoolCode"].ToString().ToUpper())) && 
                        (dr["CurrencyIso"].ToString().ToUpper().Equals(dr2["CurrencyIso"].ToString().ToUpper())))
                    {
                        if (dr2["ContractType"].ToString().Equals("B"))
                        {
                            borrowQuantity += (long)dr2["Quantity"];
                            borrowAmount += (decimal)dr2["Amount"];

                            borrowRate += (decimal)dr2["RebateRate"];
                            borrowRateCount++;
                        }
                        else
                        {
                            loanQuantity += (long)dr2["Quantity"];
                            loanAmount += (decimal)dr2["Amount"];

                            loanRate += (decimal)dr2["RebateRate"];
                            loanRateCount++;
                        }
                    }
                }

                dr["BorrowQuantity"] = borrowQuantity;
                dr["BorrowAmount"] = borrowAmount;

                dr["LoanQuantity"] = loanQuantity;
                dr["LoanAmount"] = loanAmount;

                if (borrowRateCount != 0)
                {

                    if (borrowRate != 0)
                    {
                        dr["BorrowRate"] = borrowRate / borrowRateCount;
                        dr["BorrowValue"] = (decimal)dr["BorrowAmount"] * ((1 / ((decimal)dr["BorrowRate"])) / 360);
                    }
                    else
                    {
                        dr["BorrowRate"] = 0;
                        dr["BorrowValue"] = 0;
                    }
                }
                else
                {
                    dr["BorrowRate"] = 0;
                    dr["BorrowValue"] = 0;
                }

                if (loanRateCount != 0)
                {
                    if (loanRate != 0)
                    {
                        dr["LoanRate"] = loanRate / loanRateCount;
                        dr["LoanValue"] = (decimal)dr["LoanAmount"] * ((1 / ((decimal)dr["LoanRate"])) / 360);
                    }
                    else
                    {
                        dr["LoanRate"] = 0;
                        dr["LoanValue"] = 0;
                    }
                }
                else
                {
                    dr["LoanRate"] = 0;
                    dr["LoanValue"] = 0;
                }
            }
        }
        public static void SummaryByBookCash(DataSet dsContracts, ref DataSet dsTarget, string bookGroup)
        {
            bool found = false;

            StandardFunctions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");

            dsTarget = new DataSet();
            dsTarget.Tables.Add("ContractSummary");

            dsTarget.Tables["ContractSummary"].Columns.Add("BookGroup", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("Book", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("BookName", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("CurrencyIso", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("BorrowAmount", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("LoanAmount", typeof(decimal));
            dsTarget.AcceptChanges();

            if (dsContracts != null)
            {
                foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
                {
                    found = false;

                    foreach (DataRow dr2 in dsTarget.Tables["ContractSummary"].Rows)
                    {
                        if ((dr["BookGroup"].ToString().ToUpper().Equals(bookGroup.ToUpper())) &&
                            (dr["Book"].ToString().ToUpper().Equals(dr2["Book"].ToString().ToUpper())) &&
                            (dr["CurrencyIso"].ToString().ToUpper().Equals(dr2["CurrencyIso"].ToString().ToUpper())) &&
                            bool.Parse(dr["IsActive"].ToString()) == true)
                        {
                            found = true;
                        }
                    }

                    if (!found && bool.Parse(dr["IsActive"].ToString()) == true)
                    {
                        DataRow tempRecord = dsTarget.Tables["ContractSummary"].NewRow();
                        tempRecord["BookGroup"] = dr["BookGroup"].ToString().ToUpper();
                        tempRecord["Book"] = dr["Book"].ToString().ToUpper();
                        tempRecord["BookName"] = dr["BookName"].ToString().ToUpper();
                        tempRecord["CurrencyIso"] = dr["CurrencyIso"].ToString().ToUpper();
                        tempRecord["BorrowAmount"] = 0;
                        tempRecord["LoanAmount"] = 0;

                        dsTarget.Tables["ContractSummary"].Rows.Add(tempRecord);
                        dsTarget.AcceptChanges();
                    }
                }

                decimal borrowAmount = 0;
                decimal loanAmount = 0;

                foreach (DataRow dr in dsTarget.Tables["ContractSummary"].Rows)
                {

                    borrowAmount = 0;
                    loanAmount = 0;

                    foreach (DataRow dr2 in dsContracts.Tables["Contracts"].Rows)
                    {
                        if ((dr["BookGroup"].ToString().ToUpper().Equals(bookGroup.ToUpper())) &&
                            (dr["Book"].ToString().ToUpper().Equals(dr2["Book"].ToString().ToUpper())) &&
                            (dr["CurrencyIso"].ToString().ToUpper().Equals(dr2["CurrencyIso"].ToString().ToUpper())))
                        {
                            if (dr2["ContractType"].ToString().Equals("B"))
                            {
                                borrowAmount += (decimal)dr2["Amount"];
                            }
                            else
                            {
                                loanAmount += (decimal)dr2["Amount"];
                            }
                        }
                    }

                    dr["BorrowAmount"] = borrowAmount;
                    dr["LoanAmount"] = loanAmount;
                }
            }
        }
        public static void SummaryByBookHypothication(DataSet dsContracts, ref DataSet dsTarget, string bookGroup)
        {
            bool found = false;

            if (dsContracts != null)
            {
                StandardFunctions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");
                SummaryByBookCash(dsContracts, ref dsTarget, bookGroup);

                dsTarget.Tables["ContractSummary"].Columns.Add("TotalBorrowAmount", typeof(decimal));
                dsTarget.Tables["ContractSummary"].Columns.Add("TotalLoanAmount", typeof(decimal));
                dsTarget.Tables["ContractSummary"].Columns.Add("LoanAmountPercent", typeof(decimal));
                dsTarget.Tables["ContractSummary"].Columns.Add("BorrowAmountPercent", typeof(decimal));
                dsTarget.AcceptChanges();

                dsTarget.Tables.Add("ContractCurrencyTotals");
                dsTarget.Tables["ContractCurrencyTotals"].Columns.Add("BookGroup", typeof(string));
                dsTarget.Tables["ContractCurrencyTotals"].Columns.Add("CurrencyIso", typeof(string));
                dsTarget.Tables["ContractCurrencyTotals"].Columns.Add("BorrowAmount", typeof(decimal));
                dsTarget.Tables["ContractCurrencyTotals"].Columns.Add("LoanAmount", typeof(decimal));
                dsTarget.AcceptChanges();

                foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
                {
                    foreach (DataRow dr2 in dsTarget.Tables["ContractCurrencyTotals"].Rows)
                    {
                        if (dr["BookGroup"].ToString().Equals(dr2["BookGroup"].ToString()) &&
                            dr["CurrencyIso"].ToString().Equals(dr2["CurrencyIso"].ToString()))
                        {
                            switch (dr["ContractType"].ToString())
                            {
                                case "B":
                                    dr2["BorrowAmount"] = (decimal)dr2["BorrowAmount"] + (decimal)dr["Amount"];
                                    break;

                                case "L":
                                    dr2["LoanAmount"] = (decimal)dr2["LoanAmount"] + (decimal)dr["Amount"];
                                    break;
                            }

                            found = true;
                        }
                    }

                    if (!found)
                    {
                        DataRow tempDr = dsTarget.Tables["ContractCurrencyTotals"].NewRow();
                        tempDr["BookGroup"] = dr["BookGroup"].ToString();
                        tempDr["CurrencyIso"] = dr["CurrencyIso"].ToString();

                        if (dr["ContractType"].ToString().Equals("B"))
                        {
                            tempDr["BorrowAmount"] = (decimal)dr["Amount"];
                            tempDr["LoanAmount"] = 0;
                        }
                        else
                        {
                            tempDr["LoanAmount"] = (decimal)dr["Amount"];
                            tempDr["BorrowAmount"] = 0;
                        }

                        dsTarget.Tables["ContractCurrencyTotals"].Rows.Add(tempDr);
                    }

                    found = false;
                }

                foreach (DataRow dr in dsTarget.Tables["ContractCurrencyTotals"].Rows)
                {
                    foreach (DataRow dr2 in dsTarget.Tables["ContractSummary"].Rows)
                    {
                        if (dr["BookGroup"].ToString().Equals(dr2["BookGroup"].ToString()) &&
                            dr["CurrencyIso"].ToString().Equals(dr2["CurrencyIso"].ToString()))
                        {
                            dr2["TotalBorrowAmount"] = (decimal)dr["BorrowAmount"];
                            dr2["TotalLoanAmount"] = (decimal)dr["LoanAmount"];
                        }
                    }
                }


                foreach (DataRow dr2 in dsTarget.Tables["ContractSummary"].Rows)
                {

                    if ((decimal)dr2["BorrowAmount"] > 0)
                    {
                        dr2["BorrowAmountPercent"] = (((decimal)dr2["TotalBorrowAmount"]) / ((decimal)dr2["BorrowAmount"])) / 100;
                    }

                    if ((decimal)dr2["LoanAmount"] > 0)
                    {
                        dr2["LoanAmountPercent"] = (((decimal)dr2["TotalLoanAmount"]) / ((decimal)dr2["LoanAmount"])) / 100;
                    }
                }
            }
        }
        public static void DistinctCurrencies(DataSet dsContracts, ref DataSet dsTarget, string bookGroup)
        {
            bool isFound = false;

            StandardFunctions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");

            dsTarget = new DataSet();
            dsTarget.Tables.Add("CurrencyIso");
            dsTarget.Tables["CurrencyIso"].Columns.Add("CurrencyIso");
            dsTarget.AcceptChanges();

            foreach (DataRow drContract in dsContracts.Tables["Contracts"].Rows)
            {
                isFound = false;

                foreach (DataRow drCurrencyIso in dsTarget.Tables["CurrencyIso"].Rows)
                {
                    if (drContract["CurrencyIso"].ToString().Equals(drCurrencyIso["CurrencyIso"].ToString()))
                    {
                        isFound = true;
                    }
                }

                if (!isFound)
                {
                    DataRow drTemp;

                    drTemp = dsTarget.Tables["CurrencyIso"].NewRow();
                    drTemp["CurrencyIso"] = drContract["CurrencyIso"].ToString();

                    dsTarget.Tables["CurrencyIso"].Rows.Add(drTemp);
                    dsTarget.AcceptChanges();
                }
            }
        }
        public static void ValidateData(DataRowView dr)
        {
            if (dr["Quantity"].ToString().Equals(""))
            {
                throw new Exception("Quantity field cannot be empty");
            }

            if (dr["Amount"].ToString().Equals(""))
            {
                throw new Exception("Amount field cannot be empty");
            }

            if (!dr["ValueDate"].ToString().Equals("") && !dr["SettleDate"].ToString().Equals(""))
            {
                if (DateTime.Parse(dr["ValueDate"].ToString()) > DateTime.Parse(dr["SettleDate"].ToString()))
                {
                    throw new Exception("Value Date cannot be greater then Settle Date");
                }
            }

            if (dr["Book"].ToString().Equals(""))
            {
                throw new Exception("Book field cannot be empty");
            }

            if (dr["RebateRate"].ToString().Equals(""))
            {
                throw new Exception("Rate field cannot be empty");
            }

            if (dr["Margin"].ToString().Equals(""))
            {
                throw new Exception("Margin field cannot be empty");
            }
        }
        public static void SummaryByCredtisDebits(DataSet dsContracts, DataSet dsBillingSummaryPrior, DataSet dsBillingSummary, DataSet dsMarks, DataSet dsReturns, ref DataSet dsTarget)
        {            
            //StandardFunctions.DataSetScrub(ref dsBillingSummaryPrior, "Billing", "BookGroup", "PFSI");
            //StandardFunctions.DataSetScrub(ref dsBillingSummary, "Billing", "BookGroup", "PFSI");

            StandardFunctions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");
            StandardFunctions.DataSetScrub(ref dsReturns, "Returns", "IsActive");
            StandardFunctions.DataSetScrub(ref dsMarks, "Marks", "IsActive");

            dsTarget = new DataSet();
            dsTarget.Tables.Add("CreditDebitSummary");
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("BookGroup", typeof(string));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("CurrencyIso", typeof(string));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("NewBorrows", typeof(decimal));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("NewLoans", typeof(decimal));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("DebitReturns", typeof(decimal));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("CreditReturns", typeof(decimal));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("DebitMarks", typeof(decimal));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("CreditMarks", typeof(decimal));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("BorrowsPrior", typeof(decimal));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("LoansPrior", typeof(decimal));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("Borrows", typeof(decimal));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("Loans", typeof(decimal));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("BorrowsDiff", typeof(decimal));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("LoansDiff", typeof(decimal));

            dsTarget.AcceptChanges();

            bool found = false;

            #region Billing Summary Prior
            foreach (DataRow dr in dsBillingSummaryPrior.Tables["Billing"].Rows)
            {
                found = false;

                foreach (DataRow dr2 in dsTarget.Tables["CreditDebitSummary"].Rows)
                {
                    if (dr["CurrencyCode"].ToString().Equals(dr2["CurrencyIso"].ToString()))
                    {

                        dr2["BorrowsPrior"] = (decimal)dr2["BorrowsPrior"] + (decimal)dr["CashOutBalance"];
                        dr2["LoansPrior"] = (decimal)dr2["LoansPrior"] + (decimal)dr["CashInBalance"];
                        found = true;
                    }
                }

                if (!found)
                {
                    DataRow tempDr = dsTarget.Tables["CreditDebitSummary"].NewRow();

                    tempDr["CurrencyIso"] = dr["CurrencyCode"].ToString();


                    try
                    {
                        tempDr["BorrowsPrior"] = (decimal)dr["CashOutBalance"];
                        tempDr["LoansPrior"] = (decimal)dr["CashInBalance"];
                    }
                    catch { }

                    tempDr["NewBorrows"] = 0;
                    tempDr["NewLoans"] = 0;
                    tempDr["CreditReturns"] = 0;
                    tempDr["DebitReturns"] = 0;
                    tempDr["CreditMarks"] = 0;
                    tempDr["DebitMarks"] = 0;
                    tempDr["Borrows"] = 0;
                    tempDr["Loans"] = 0;
                    tempDr["BorrowsDiff"] = 0;
                    tempDr["LoansDiff"] = 0;

                    dsTarget.Tables["CreditDebitSummary"].Rows.Add(tempDr);
                    dsTarget.AcceptChanges();
                }
            }

            #endregion       

            #region Returns
            foreach (DataRow dr in dsReturns.Tables["Returns"].Rows)
            {
                found = false;

                foreach (DataRow dr2 in dsTarget.Tables["CreditDebitSummary"].Rows)
                {
                    if (dr["CurrencyIso"].ToString().Equals(dr2["CurrencyIso"].ToString()) &&
                        (StandardFunctions.CompareDates(dr["BizDate"].ToString(), dr["SettleDateActual"].ToString()) == 0))
                    {

                        switch (StandardFunctions.BalanceTypeCheck(InformationType.Returns, dr["ContractType"].ToString(), (decimal)dr["CashReturn"]))
                        {
                            case BalanceTypes.Credit:
                                dr2["CreditReturns"] = (decimal)dr2["CreditReturns"] - (decimal)dr["CashReturn"];
                                break;

                            case BalanceTypes.Debit:
                                dr2["DebitReturns"] = (decimal)dr2["DebitReturns"] + (decimal)dr["CashReturn"];
                                break;
                        }                        

                        found = true;
                    }
                }

                if (!found)
                {
                    if ((StandardFunctions.CompareDates(dr["BizDate"].ToString(), dr["SettleDateActual"].ToString()) == 0))
                    {
                        DataRow tempDr = dsTarget.Tables["CreditDebitSummary"].NewRow();

                        tempDr["CurrencyIso"] = dr["CurrencyIso"].ToString();

                        try
                        {
                            switch (StandardFunctions.BalanceTypeCheck(InformationType.Returns, dr["ContractType"].ToString(), (decimal)dr["CashReturn"]))
                            {
                                case BalanceTypes.Credit:
                                    tempDr["CreditReturns"] = (decimal)dr["CashReturn"] * -1;
                                    tempDr["DebitReturns"] = 0.0;
                                    break;

                                case BalanceTypes.Debit:
                                    tempDr["DebitReturns"] =  (decimal)dr["CashReturn"];
                                    tempDr["CreditReturns"] = 0.0;
                                    break;
                            }     
                        }
                        catch { }

                        tempDr["NewBorrows"] = 0;
                        tempDr["NewLoans"] = 0;
                        tempDr["CreditMarks"] = 0;
                        tempDr["DebitMarks"] = 0;
                        tempDr["BorrowsPrior"] = 0;
                        tempDr["LoansPrior"] = 0;
                        tempDr["Borrows"] = 0;
                        tempDr["Loans"] = 0;
                        tempDr["BorrowsDiff"] = 0;
                        tempDr["LoansDiff"] = 0;

                        dsTarget.Tables["CreditDebitSummary"].Rows.Add(tempDr);
                        dsTarget.AcceptChanges();
                    }
                }
            }
            #endregion

            #region Marks
            foreach (DataRow dr in dsMarks.Tables["Marks"].Rows)
            {
                found = false;

                foreach (DataRow dr2 in dsTarget.Tables["CreditDebitSummary"].Rows)
                {
                    if (dr["CurrencyIso"].ToString().Equals(dr2["CurrencyIso"].ToString()))
                    {
                        switch (StandardFunctions.BalanceTypeCheck(InformationType.Marks, dr["ContractType"].ToString(), (decimal)dr["Amount"]))
                        {
                            case BalanceTypes.Debit:
                                dr2["DebitMarks"] = (decimal)dr2["DebitMarks"] + (decimal)dr["Amount"];
                                break;

                            case BalanceTypes.Credit:
                                dr2["CreditMarks"] = (decimal) dr2["CreditMarks"] + (decimal)dr["Amount"];
                                break;
                        }

                        found = true;
                    }
                }

                if (!found)
                {
                    DataRow tempDr = dsTarget.Tables["CreditDebitSummary"].NewRow();
                   
                    tempDr["CurrencyIso"] = dr["CurrencyIso"].ToString();

                    try
                    {
                        switch (StandardFunctions.BalanceTypeCheck(InformationType.Marks, dr["ContractType"].ToString(), (decimal)dr["Amount"]))
                        {
                            case BalanceTypes.Credit:
                                tempDr["CreditMarks"] = (decimal)dr["Amount"];
                                tempDr["DebitMarks"] = 0.0;
                                break;

                            case BalanceTypes.Debit:
                                tempDr["DebitMarks"] = (decimal)dr["Amount"];
                                tempDr["CreditMarks"] = 0.0;
                                break;
                        }
                    }
                    catch { }

                    tempDr["NewBorrows"] = 0;
                    tempDr["NewLoans"] = 0;
                    tempDr["CreditReturns"] = 0;
                    tempDr["DebitReturns"] = 0;
                    tempDr["BorrowsPrior"] = 0;
                    tempDr["LoansPrior"] = 0;
                    tempDr["Borrows"] = 0;
                    tempDr["Loans"] = 0;
                    tempDr["BorrowsDiff"] = 0;
                    tempDr["LoansDiff"] = 0;

                    dsTarget.Tables["CreditDebitSummary"].Rows.Add(tempDr);
                    dsTarget.AcceptChanges();
                }
            }
            #endregion

            #region new contracts
            foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
            {
                found = false;

                if (DateTime.Parse(dr["SettleDate"].ToString()).ToString("yyyy-MM-dd").Equals(DateTime.Parse(dr["BizDate"].ToString()).ToString("yyyy-MM-dd")))
                {
                    foreach (DataRow dr2 in dsTarget.Tables["CreditDebitSummary"].Rows)
                    {
                        if (dr["CurrencyIso"].ToString().Equals(dr2["CurrencyIso"].ToString()))
                        {
                            switch (dr["ContractType"].ToString())
                            {
                                case ("L"):
                                    dr2["NewLoans"] = (decimal)dr2["NewLoans"] + (decimal)dr["Amount"];
                                    break;

                                case ("B"):
                                    dr2["NewBorrows"] = (decimal)dr2["NewBorrows"] - (decimal)dr["Amount"];
                                    break;

                            }

                            found = true;
                        }
                    }

                    if (!found)
                    {
                        if (DateTime.Parse(dr["SettleDate"].ToString()).ToString("yyyy-MM-dd").Equals(DateTime.Parse(dr["BizDate"].ToString()).ToString("yyyy-MM-dd")))
                        {
                            DataRow tempDr = dsTarget.Tables["CreditDebitSummary"].NewRow();

                            tempDr["CurrencyIso"] = dr["CurrencyIso"].ToString();


                            try
                            {
                                switch (dr["ContractType"].ToString())
                                {
                                    case ("B"):
                                        tempDr["NewBorrows"] = (decimal)dr["Amount"] * -1;
                                        tempDr["NewLoans"] = 0.0;
                                        break;

                                    case ("L"):
                                        tempDr["NewLoans"] = (decimal)dr["Amount"];
                                        tempDr["NewBorrows"] = 0.0;
                                        break;

                                }
                            }
                            catch { }

                            tempDr["CreditReturns"] = 0;
                            tempDr["DebitReturns"] = 0;
                            tempDr["CreditMarks"] = 0;
                            tempDr["DebitMarks"] = 0;
                            tempDr["BorrowsPrior"] = 0;
                            tempDr["LoansPrior"] = 0;
                            tempDr["Borrows"] = 0;
                            tempDr["Loans"] = 0;
                            tempDr["BorrowsDiff"] = 0;
                            tempDr["LoansDiff"] = 0;

                            dsTarget.Tables["CreditDebitSummary"].Rows.Add(tempDr);
                            dsTarget.AcceptChanges();
                        }
                    }
                }
            }
            #endregion

            #region Billing Summary
            foreach (DataRow dr in dsBillingSummary.Tables["Billing"].Rows)
            {
                found = false;

                foreach (DataRow dr2 in dsTarget.Tables["CreditDebitSummary"].Rows)
                {
                    if (dr["CurrencyCode"].ToString().Equals(dr2["CurrencyIso"].ToString()))
                    {


                        dr2["Borrows"] = (decimal)dr2["Borrows"] + (decimal)dr["CashOutBalance"];


                        dr2["Loans"] = (decimal)dr2["Loans"] + (decimal)dr["CashInBalance"];
                        found = true;
                    }
                }

                /*if (!found)
                {
                    DataRow tempDr = dsTarget.Tables["CreditDebitSummary"].NewRow();

                    tempDr["CurrencyIso"] = dr["CurrencyCode"].ToString();


                    try
                    {
                        tempDr["Borrows"] = (decimal)dr["CashOutBalance"];
                        tempDr["Loans"] = (decimal)dr["CashInBalance"];
                    }
                    catch { }

                    tempDr["NewBorrows"] = 0;
                    tempDr["NewLoans"] = 0;
                    tempDr["CreditReturns"] = 0;
                    tempDr["DebitReturns"] = 0;
                    tempDr["CreditMarks"] = 0;
                    tempDr["DebitMarks"] = 0;          
                    tempDr["BorrowsDiff"] = 0;
                    tempDr["LoansDiff"] = 0;

                    dsTarget.Tables["CreditDebitSummary"].Rows.Add(tempDr);
                    dsTarget.AcceptChanges();
                }*/
            }
            #endregion       
            #region Debit Total

            foreach (DataRow dr in dsTarget.Tables["CreditDebitSummary"].Rows)
            {
                dr["BorrowsDiff"] = (decimal)dr["BorrowsPrior"] - (decimal)dr["Borrows"];
                dr["LoansDiff"] = (decimal)dr["LoansPrior"] - (decimal)dr["Loans"];
            }

            dsTarget.AcceptChanges();
            #endregion
        }
        public static void SummaryByCashContracts(DataSet dsContracts, ref DataSet dsTarget, SettlementType settlementType)
        {
            bool found = false;

            StandardFunctions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");

            dsTarget = new DataSet();
            dsTarget.Tables.Add("CreditDebitSummary");            
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("Book", typeof(string));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("BookName", typeof(string));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("CurrencyIso", typeof(string));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("Debit", typeof(decimal));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("Credit", typeof(decimal));
            dsTarget.AcceptChanges();
            
            switch (settlementType)
            {
                case (SettlementType.SettledToday):
                    foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
                    {
                        if (!DateTime.Parse(dr["SettleDate"].ToString()).ToString("yyyy-MM-dd").Equals(DateTime.Parse(dr["BizDate"].ToString()).ToString("yyyy-MM-dd")))
                        {
                            dr.Delete();
                        }
                    }

                    dsContracts.AcceptChanges();
                    break;

                case (SettlementType.Settled):
                    {
                        foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
                        {
                            if (DateTime.Parse(dr["SettleDate"].ToString()) >= DateTime.Parse(dr["BizDate"].ToString()))
                            {
                                dr.Delete();
                            }
                        }

                        dsContracts.AcceptChanges();
                    }
                    break;

                default:
                    break;
            }            
            
            foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
            {
                found = false;

                foreach (DataRow dr2 in dsTarget.Tables["CreditDebitSummary"].Rows)
                {
                    if ((dr["Book"].ToString().Equals(dr2["Book"].ToString())) &&
                        (dr["CurrencyIso"].ToString().Equals(dr2["CurrencyIso"].ToString())))
                    {
                        switch (StandardFunctions.BalanceTypeCheck(InformationType.Contracts, dr["ContractType"].ToString(), (decimal)dr["Amount"]))
                        {
                            case (BalanceTypes.Credit):
                                dr2["Credit"] = (decimal)dr2["Credit"] + Math.Abs((decimal)dr["Amount"]);
                                break;

                            case (BalanceTypes.Debit):
                                dr2["Debit"] = (decimal)dr2["Debit"] - Math.Abs((decimal)dr["Amount"]);
                                break;
                        }

                        found = true;
                    }
                }

                if (!found)
                {
                    DataRow tempDr = dsTarget.Tables["CreditDebitSummary"].NewRow();                    
                    tempDr["Book"] = dr["Book"].ToString();
                    tempDr["BookName"] = dr["BookName"].ToString();
                    tempDr["CurrencyIso"] = dr["CurrencyIso"].ToString();


                    switch (StandardFunctions.BalanceTypeCheck(InformationType.Contracts, dr["ContractType"].ToString(), (decimal)dr["Amount"]))
                    {
                        case (BalanceTypes.Credit):
                            tempDr["Credit"] = Math.Abs((decimal)dr["Amount"]);
                            tempDr["Debit"] = 0;
                            break;

                        case (BalanceTypes.Debit):
                            tempDr["Debit"] = Math.Abs((decimal)dr["Amount"]) * -1;
                            tempDr["Credit"] = 0;
                            break;
                    }

                    dsTarget.Tables["CreditDebitSummary"].Rows.Add(tempDr);
                    dsTarget.AcceptChanges();
                }
            }
        }
    }
}
