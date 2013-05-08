using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Globalization;
using StockLoan.DataAccess;

namespace StockLoan.Business
{
    
    public class Contracts
    {
        public static int ContractBizDateProcessRoll(string bizDate, string bizDatePrior)
        {
            
            try
            {
                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date value is required");
                }

                if (bizDatePrior.Equals(""))
                {
                    throw new Exception("Prior Biz Date value is required");
                }

                if (bizDatePrior.Equals(bizDate))
                {
                    throw new Exception("Contracts have already been rolled to " + bizDate);
                }

                int rowCount = 0;

                rowCount = DBContracts.ContractBizDateProcessRoll(bizDate, bizDatePrior);

                return rowCount;
            }
            catch 
            {
                throw;
            }
        }

        public static int ContractBizDateSystemRoll(string bizDate, string bizDatePrior)
        {
            try
            {
                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date value is required");
                }

                if (bizDatePrior.Equals(""))
                {
                    throw new Exception("Prior Biz Date value is required");
                }

                if (bizDatePrior.Equals(bizDate))
                {
                    throw new Exception("Contracts Date has already been rolled to " + bizDate);
                }

                int rowCount = 0;

                rowCount = DBContracts.ContractBizDateSystemRoll(bizDate, bizDatePrior);

                return rowCount;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet ContractDistinctCurrencies(DataSet dsContracts, string bookGroup)
        {
            bool isFound = false;

            try
            {
                Functions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");

                DataSet dsCurrency = new DataSet();

                dsCurrency.Tables.Add("CurrencyIso");
                dsCurrency.Tables["CurrencyIso"].Columns.Add("CurrencyIso");
                dsCurrency.AcceptChanges();

                foreach (DataRow drContract in dsContracts.Tables["Contracts"].Rows)
                {
                    isFound = false;

                    foreach (DataRow drCurrencyIso in dsCurrency.Tables["CurrencyIso"].Rows)
                    {
                        if (drContract["CurrencyIso"].ToString().Equals(drCurrencyIso["CurrencyIso"].ToString()))
                        {
                            isFound = true;
                        }
                    }

                    if (!isFound)
                    {
                        DataRow drTemp;

                        drTemp = dsCurrency.Tables["CurrencyIso"].NewRow();
                        drTemp["CurrencyIso"] = drContract["CurrencyIso"].ToString();

                        dsCurrency.Tables["CurrencyIso"].Rows.Add(drTemp);
                        dsCurrency.AcceptChanges();
                    }
                }

                return dsCurrency;
            }
            catch
            {
                throw;
            }
        }

        public static int ContractRateChangeAsOfSet(
            string startDate, 
            string endDate, 
            string bookGroup, 
            string book,
            string contractId, 
            string oldRate, 
            string newRate, 
            string actUserId)
        {
            int rowCount = 0;

            try
            {
                if (startDate.Equals(""))
                {
                    throw new Exception("Start Date is required");
                }

                if (endDate.Equals(""))
                {
                    throw new Exception("End Date is required");
                }

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required");
                }
            
                if (contractId.Equals(""))
                {
                    throw new Exception("Contract ID is required");
                }

                rowCount = DBContracts.ContractRateChangeAsOfSet(
                    startDate, 
                    endDate, 
                    bookGroup, 
                    book, 
                    contractId, 
                    oldRate, 
                    newRate, 
                    actUserId);

                return rowCount;
            }
            catch
            {
                throw;
            }            
        }

        public static void ContractSet(
            string bizDate, 
            string bookGroup, 
            string contractId, 
            string contractType, 
            string book, 
            string secId, 
            string quantity,
            string quantitySettled, 
            string amount, 
            string amountSettled, 
            string collateralCode, 
            string valueDate, 
            string settleDate,
            string termDate, 
            string rate, 
            string rateCode, 
            string statusFlag, 
            string poolCode, 
            string divRate, 
            bool divCallable,
            bool incomeTracked, 
            string marginCode, 
            string margin, 
            string currencyIso, 
            string securityDepot, 
            string cashDepot,
            string otherBook, 
            string comment, 
            string fund, 
            string tradeRefId, 
            string feeAmount, 
            string feeCurrencyIso, 
            string feeType,
            bool returnData, 
            bool isIncremental, 
            bool isActive)
        {
            try
            {

                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date is required");
                }

                if (bookGroup.Equals(""))
                {
                    throw new Exception("BookGroup is required");
                }

                if (contractId.Equals(""))
                {
                    throw new Exception("ContractId is required");
                }

                if (contractType.Equals(""))
                {
                    throw new Exception("ContractType is required");
                }

                DBContracts.ContractSet(
                   bizDate,
                   bookGroup,
                   contractId,
                   contractType,
                   book,
                   secId,
                   quantity,
                   quantitySettled,
                   amount,
                   amountSettled,
                   collateralCode,
                   valueDate,
                   settleDate,
                   termDate,
                   rate,
                   rateCode,
                   statusFlag,
                   poolCode,
                   divRate,
                   divCallable,
                   incomeTracked,
                   marginCode,
                   margin,
                   currencyIso,
                   securityDepot,
                   cashDepot,
                   otherBook,
                   comment,
                   fund,
                   tradeRefId,
                   feeAmount,
                   feeCurrencyIso,
                   feeType,
                   returnData,
                   isIncremental,
                   isActive);

            }
            catch
            {
                throw;
            }
        }

        public static DataSet ContractsGet(string bizDate, string bookGroup, string contractId, string contractType)
        {
            try
            {
                DataSet dsContracts = new DataSet();

                dsContracts = DBContracts.ContractsGet(bizDate, bookGroup, contractId, contractType);

                return dsContracts;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet ContractSummaryByPriorBilling(DataSet dsContracts)
        {
            bool isFound = false;
            try
            {
                Functions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");

                DataSet dsTarget = new DataSet();

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

                return dsTarget;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet ContractsSummaryByBillings(
            string bizDate, 
            string startDate, 
            string stopDate,
            string bookGroup,
            string book,
            string contractId, 
            string secId, 
            string amount, 
            string logicId)
        {
            bool found;
            try
            {
                DataSet dsContracts = new DataSet();
                DataSet dsSummary = new DataSet();

                dsContracts = ContractsResearchGet(bizDate, startDate, stopDate, bookGroup, book, contractId, secId, amount, logicId);
                dsContracts.Tables[0].TableName = "Contracts";

                Functions.DataSetScrub(ref dsContracts, dsContracts.Tables[0].TableName.ToString(), "IsActive");

                dsSummary = ContractSummaryByBookProfitLoss(dsContracts, bookGroup);

                ContractTermDatePopulate(ref dsContracts, contractId, bizDate);

                DataSet dsTarget = new DataSet();

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
                            if (dr2["FeeAmount"].ToString().Equals(""))
                            { feeAmount += 0; }
                            else
                            {
                                feeAmount += (decimal)dr2["FeeAmount"];
                            }
                            if (dr2["RebateAmount"].ToString().Equals(""))
                            {
                                rebateAmount += 0;
                            }
                            else
                            {
                                rebateAmount += (decimal)dr2["RebateAmount"];
                            }
                            if (dr2["PL"].ToString().Equals(""))
                            {
                                totalRebate += 0;
                            }
                            else
                            {
                                totalRebate += (decimal)dr2["PL"];
                            }

                        }
                    }

                    dr["FeeAmount"] = feeAmount;
                    dr["RebateAmount"] = rebateAmount;
                    dr["TotalRebate"] = totalRebate;
                }

                dsTarget.AcceptChanges();

                return dsContracts;
            }
            catch
            {
                throw;
            }

        }

        public static DataSet ContractSummaryByBookCash(DataSet dsContracts, string bookGroup)
        {
            bool found = false;

            try
            {
                Functions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");
                
                DataSet dsTarget = new DataSet();

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
                
                dsTarget.AcceptChanges();
                
                return dsTarget;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet ContractSummaryByBookProfitLoss(DataSet dsContracts, string bookGroup)
        {
            bool found = false;

            try
            {
                Functions.DataSetScrub(ref dsContracts, dsContracts.Tables[0].TableName.ToString(), "IsActive");

                DataSet dsTarget = new DataSet();

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

                return dsTarget;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet ContractSummarybyCash(DataSet dsContracts, string settlementType)
        {
            bool found = false;

            try
            {
                Functions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");
                DataSet dsTarget = new DataSet();

                dsTarget.Tables.Add("CreditDebitSummary");
                dsTarget.Tables["CreditDebitSummary"].Columns.Add("Book", typeof(string));
                dsTarget.Tables["CreditDebitSummary"].Columns.Add("BookName", typeof(string));
                dsTarget.Tables["CreditDebitSummary"].Columns.Add("CurrencyIso", typeof(string));
                dsTarget.Tables["CreditDebitSummary"].Columns.Add("Debit", typeof(decimal));
                dsTarget.Tables["CreditDebitSummary"].Columns.Add("Credit", typeof(decimal));
                dsTarget.AcceptChanges();

                switch (settlementType)
                {
                    case ("SettledToday"):
                        foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
                        {
                            if (!DateTime.Parse(dr["SettleDate"].ToString()).ToString("yyyy-MM-dd").Equals(DateTime.Parse(dr["BizDate"].ToString()).ToString("yyyy-MM-dd")))
                            {
                                dr.Delete();
                            }
                        }

                        dsContracts.AcceptChanges();
                        break;

                    case ("Settled"):
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
                            switch (Functions.BalanceTypeCheck(InformationType.Contracts, dr["ContractType"].ToString(), (decimal)dr["Amount"]))
                            {
                                case (BalanceTypes.Credit):
                                    dr2["Credit"] = (decimal)dr2["Credit"] + Math.Abs((decimal)dr["Amount"]);
                                    break;

                                case (BalanceTypes.Debit):
                                    dr2["Debit"] = (decimal)dr2["Debit"] - Math.Abs((decimal)dr["Amount"]);
                                    break;
                            }
                            dsTarget.AcceptChanges();
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        DataRow tempDr = dsTarget.Tables["CreditDebitSummary"].NewRow();
                        tempDr["Book"] = dr["Book"].ToString();
                        tempDr["BookName"] = dr["BookName"].ToString();
                        tempDr["CurrencyIso"] = dr["CurrencyIso"].ToString();

                        switch (Functions.BalanceTypeCheck(InformationType.Contracts, dr["ContractType"].ToString(), (decimal)dr["Amount"]))
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
                    }
                }

                dsTarget.AcceptChanges();
                
                return dsTarget;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet ContractSummaryByCreditsDebits(string bizDate)
        {
            try
            {
                DataSet dsContracts = new DataSet();
                DataSet dsBillingSummary = new DataSet();
                DataSet dsBillingSummaryPrior = new DataSet();
                DataSet dsMarks = new DataSet();
                DataSet dsReturns = new DataSet();
                DataSet dsContractSummary = new DataSet();

                DateTime priorDate = new DateTime();

                priorDate = DateTime.Parse(bizDate);

                priorDate = priorDate.AddDays(-1); 

                string pDate = priorDate.ToString("MM/dd/yyyy");
                
                dsContracts = ContractsResearchGet(
                    bizDate, 
                    "", 
                    "", 
                    "", 
                    "", 
                    "", 
                    "", 
                    "", 
                    "");

                dsContracts.Tables[0].TableName = "Contracts";

                dsBillingSummary = ContractBillingsGet(bizDate);
                dsBillingSummary.Tables[0].TableName = "Billing";

                dsBillingSummaryPrior = ContractBillingsGet(pDate);
                dsBillingSummaryPrior.Tables[0].TableName = "Billing";

                dsMarks = Marks.MarksGet("", bizDate, "", "", 0);

                dsReturns = Returns.ReturnsGet("", bizDate, "", "", 0);

                Functions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");
                Functions.DataSetScrub(ref dsReturns, "Returns", "IsActive");
                Functions.DataSetScrub(ref dsMarks, "Marks", "IsActive");

                DataSet dsTarget = new DataSet();
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
                        (Functions.CompareDates(dr["BizDate"].ToString(), dr["SettleDateActual"].ToString()) == 0))
                    {

                        switch (Functions.BalanceTypeCheck(InformationType.Returns, dr["ContractType"].ToString(), (decimal)dr["CashReturn"]))
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
                    if ((Functions.CompareDates(dr["BizDate"].ToString(), dr["SettleDateActual"].ToString()) == 0))
                    {
                        DataRow tempDr = dsTarget.Tables["CreditDebitSummary"].NewRow();

                        tempDr["CurrencyIso"] = dr["CurrencyIso"].ToString();

                        try
                        {
                            switch (Functions.BalanceTypeCheck(InformationType.Returns, dr["ContractType"].ToString(), (decimal)dr["CashReturn"]))
                            {
                                case BalanceTypes.Credit:
                                    tempDr["CreditReturns"] = (decimal)dr["CashReturn"] * -1;
                                    tempDr["DebitReturns"] = 0.0;
                                    break;

                                case BalanceTypes.Debit:
                                    tempDr["DebitReturns"] = (decimal)dr["CashReturn"];
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
                        switch (Functions.BalanceTypeCheck(InformationType.Marks, dr["ContractType"].ToString(), (decimal)dr["Amount"]))
                        {
                            case BalanceTypes.Debit:
                                dr2["DebitMarks"] = (decimal)dr2["DebitMarks"] + (decimal)dr["Amount"];
                                break;

                            case BalanceTypes.Credit:
                                dr2["CreditMarks"] = (decimal)dr2["CreditMarks"] + (decimal)dr["Amount"];
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
                        switch (Functions.BalanceTypeCheck(InformationType.Marks, dr["ContractType"].ToString(), (decimal)dr["Amount"]))
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

            return dsTarget;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet ContractSummaryByHypothication(DataSet dsContracts, string bookGroup)
        {
            bool found = false;
            try
            {
                if (dsContracts != null)
                {
                    Functions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");
                    DataSet dsTarget = new DataSet();
                    dsTarget = ContractSummaryByBookCash(dsContracts, bookGroup);

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

                    return dsTarget;
                }

                return dsContracts;
            }          
            catch 
            {
                throw;
            }
        }

        public static DataSet ContractSummaryByMarketValue(DataSet dsContracts, string bookGroup)
        {
            decimal price = 0;

            try
            {
                dsContracts.Tables["Contracts"].Columns.Add("MarketValue");
                dsContracts.AcceptChanges();

                foreach (DataRow drContract in dsContracts.Tables["Contracts"].Rows)
                {
                    price = 0;

                    if ((decimal.Parse(drContract["QuantitySettled"].ToString()) > 0) &&
                         (decimal.Parse(drContract["AmountSettled"].ToString()) > 0))
                    {
                        price = (decimal.Parse(drContract["AmountSettled"].ToString()) / decimal.Parse(drContract["Margin"].ToString())) / decimal.Parse(drContract["QuantitySettled"].ToString());
                        drContract["MarketValue"] = (decimal.Parse(drContract["QuantitySettled"].ToString()) * price).ToString("#,##0.00");
                    }
                    else if ((decimal.Parse(drContract["Quantity"].ToString()) > 0) &&
                         (decimal.Parse(drContract["Amount"].ToString()) > 0))
                    {
                        price = (decimal.Parse(drContract["Amount"].ToString()) / decimal.Parse(drContract["Margin"].ToString())) / decimal.Parse(drContract["Quantity"].ToString());
                        drContract["MarketValue"] = (decimal.Parse(drContract["Quantity"].ToString()) * price).ToString("#,##0.00");
                    }
                    else
                    {
                        price = 0;
                        drContract["MarketValue"] = 0;
                    }
                }

                dsContracts.AcceptChanges();

                return dsContracts;
            }
            catch 
            {
                throw;
            }
    }

        public static DataSet ContractSummary(DataSet dsContracts, string bookGroup, bool usePoolCode)
        {

            long contractQuantity = 0;
            decimal contractAmount = 0;
            decimal rate = 0;
            bool found = false;
            DataSet dsTarget = new DataSet();

            try
            {
                Functions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");

                dsTarget.Tables.Add("ContractSummary");
                dsTarget.Tables["ContractSummary"].Columns.Add("BookGroup", typeof(string));
                dsTarget.Tables["ContractSummary"].Columns.Add("SecId", typeof(string));
                dsTarget.Tables["ContractSummary"].Columns.Add("Isin", typeof(string));
                dsTarget.Tables["ContractSummary"].Columns.Add("Symbol", typeof(string));
                dsTarget.Tables["ContractSummary"].Columns.Add("ContractType", typeof(string));
                dsTarget.Tables["ContractSummary"].Columns.Add("Quantity", typeof(long));
                dsTarget.Tables["ContractSummary"].Columns.Add("Amount", typeof(decimal));
                dsTarget.Tables["ContractSummary"].Columns.Add("CurrencyIso", typeof(string));
                dsTarget.Tables["ContractSummary"].Columns.Add("Rate", typeof(decimal));
                dsTarget.Tables["ContractSummary"].Columns.Add("ContractId", typeof(string));
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
                                (dr["ContractType"].ToString().ToUpper().Equals(dr2["ContractType"].ToString().ToUpper())) &&
                                (dr["CurrencyIso"].ToString().ToUpper().Equals(dr2["CurrencyIso"].ToString().ToUpper())))
                            {
                                found = true;
                            }

                        }
                        if (!found && bool.Parse(dr["IsActive"].ToString()) == true)
                        {
                            contractQuantity = 0;
                            contractAmount = 0;
                            rate = 0;

                            SummaryTotalsCalculate(dsContracts, dr["BookGroup"].ToString(),
                                        dr["SecId"].ToString().ToUpper(),
                                        dr["ContractType"].ToString().ToUpper(),
                                        dr["CurrencyIso"].ToString().ToUpper(),
                                        ref contractQuantity, ref contractAmount, ref rate);

                            DataRow tempRecord = dsTarget.Tables["ContractSummary"].NewRow();
                            tempRecord["BookGroup"] = dr["BookGroup"].ToString();
                            tempRecord["SecId"] = dr["SecId"].ToString().ToUpper();
                            tempRecord["Isin"] = dr["Isin"].ToString().ToUpper();
                            tempRecord["Symbol"] = dr["Symbol"].ToString().ToUpper();
                            tempRecord["ContractType"] = dr["ContractType"].ToString().ToUpper();
                            tempRecord["Quantity"] = contractQuantity;
                            tempRecord["Amount"] = contractAmount;
                            tempRecord["CurrencyIso"] = dr["CurrencyIso"].ToString().ToUpper();
                            tempRecord["Rate"] = rate;
                            tempRecord["ContractId"] = dr["ContractId"].ToString().ToUpper();

                            dsTarget.Tables["ContractSummary"].Rows.Add(tempRecord);
                            dsTarget.AcceptChanges();
                        }
                    }
                }

                return dsTarget;
            }
            catch
            {
                throw;
            }
        }

        public static void SummaryTotalsCalculate(
            DataSet dsContracts, 
            string bookGroup, 
            string secId, 
            string contractType, 
            string currencyIso, 
            ref long contractQuantity, 
            ref decimal contractAmount, 
            ref decimal rate)
        {
            long rateCount = 0;
            decimal rateAmt = 0;

            string contractId = "";

            foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
            {
                if ((dr["BookGroup"].ToString().ToUpper().Equals(bookGroup.ToString().ToUpper())) &&
                    (dr["SecId"].ToString().ToUpper().Equals(secId.ToString().ToUpper())) &&
                    (dr["ContractType"].ToString().ToUpper().Equals(contractType.ToString().ToUpper())) &&
                    (dr["CurrencyIso"].ToString().ToUpper().Equals(currencyIso.ToString().ToUpper()))) 
                {
                    if (!dr["ContractId"].ToString().ToUpper().Equals(contractId.ToString().ToUpper()))
                    {
                        contractId = dr["ContractId"].ToString().ToUpper();                       
                        contractQuantity += (long)dr["Quantity"];
                        contractAmount += (decimal)dr["Amount"];

                        rateAmt += (decimal)dr["RebateRate"];
                        rateCount++;
                    }

                }
            }
            
            if (rateCount != 0)
            {
                if (rateAmt != 0)
                {
                    rate = rateAmt / rateCount;
                }
                else
                {
                    rate = 0;
                }

            }
        }

        public static DataSet ContractSummaryBySecurity(DataSet dsContracts, string bookGroup, bool usePoolCode)
        {
            bool found = false;

            try
            {
                Functions.DataSetScrub(ref dsContracts, "Contracts", "IsActive");
                DataSet dsTarget = new DataSet();

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

                foreach (DataRow drContract in dsTarget.Tables["ContractSummary"].Rows)
                {
                    drContract["NetAmount"] = (decimal)drContract["BorrowValue"] + (decimal)drContract["LoanValue"];
                }

                return dsTarget;
            }
            catch 
            {
                throw;
            }
        }

        public static void ContractTermDatePopulate(ref DataSet dsContracts, string contractId, string bizDate)
        {
            try
            {
                if(dsContracts.Tables[0].TableName.ToString().Equals("Contracts"))
                {
                    foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
                    {
                        if (dr["ContractId"].ToString().Equals(contractId.ToLower()))
                        {
                            dr["TermDate"] = bizDate;
                            dr.AcceptChanges();
                        }
                    }
                }
                else
                {
                    foreach (DataRow dr in dsContracts.Tables["ContractResearch"].Rows)
                    {
                        if (dr["ContractId"].ToString().Equals(contractId.ToLower()))
                        {
                            dr["TermDate"] = bizDate;
                            dr.AcceptChanges();
                        }
                    }
                }
            }
            catch 
            {
                throw;
            }
        }

        public static void ContractValidateData(DataRow drValidate)
        {

            if (drValidate["Quantity"].ToString().Equals(""))
            {
                throw new Exception("Quantity field cannot be empty");
            }

            if (drValidate["Amount"].ToString().Equals(""))
            {
                throw new Exception("Amount field cannot be empty");
            }

            if (!drValidate["ValueDate"].ToString().Equals("") && !drValidate["SettleDate"].ToString().Equals(""))
            {
                if (DateTime.Parse(drValidate["ValueDate"].ToString()) > DateTime.Parse(drValidate["SettleDate"].ToString()))
                {
                    throw new Exception("Value Date cannot be greater then Settle Date");
                }
            }

            if (drValidate["Book"].ToString().Equals(""))
            {
                throw new Exception("Book field cannot be empty");
            }

            if (drValidate["RebateRate"].ToString().Equals(""))
            {
                throw new Exception("Rate field cannot be empty");
            }

            if (drValidate["Margin"].ToString().Equals(""))
            {
                throw new Exception("Margin field cannot be empty");
            }
        }


        public static DataSet ContractsResearchGet(
            string bizDate, 
            string startDate, 
            string stopDate, 
            string bookGroup, 
            string book,                 
            string contractId, 
            string secId, 
            string amount, 
            string logicId)
        {

            try
            {
                DataSet dsTemp = new DataSet();

                dsTemp = DBContracts.ContractResearchGet(
                                                    bizDate,
                                                    startDate,
                                                    stopDate,
                                                    bookGroup,
                                                    book,
                                                    contractId,
                                                    secId,
                                                    amount,
                                                    logicId);

                return dsTemp;
            }
            catch
            {
                throw;
            }
        }

        public static DataSet ContractDetailsGet(string bizDate, string bookGroup)
        {
            DataSet dsContractDetails = new DataSet();

            try
            {
                dsContractDetails = DBContracts.ContractDetailsGet(bizDate, bookGroup);
                return dsContractDetails;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet ContractBillingsGet(string bizDate)
        {
            DataSet dsBilling = new DataSet();

            try
            {
                dsBilling = DBContracts.ContractBillingsGet(bizDate);
                return dsBilling;
            }
            catch 
            {
                throw;
            }
        }
    }
}
