using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashBusiness
{
    public class Contracts
    {
        public static DataSet ContractsDetailByMarketValue(DataSet dsContracts, Locale localeType)
        {
            decimal price = 0;

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

        public static DataSet ContractsByBookGroupCash(DataSet dsContracts, Locale localeType)
        {
            DataSet dsTarget = new DataSet();
            bool found = false;

            dsTarget.Tables.Add("ContractSummary");

            dsTarget.Tables["ContractSummary"].Columns.Add("BookGroup", typeof(string));            
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
                        if ((dr["BookGroup"].ToString().ToUpper().Equals(dr2["BookGroup"].ToString().ToUpper())) &&                            
                            (dr["CurrencyIso"].ToString().ToUpper().Equals(dr2["CurrencyIso"].ToString().ToUpper())))
                        {
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        DataRow tempRecord = dsTarget.Tables["ContractSummary"].NewRow();
                        tempRecord["BookGroup"] = dr["BookGroup"].ToString().ToUpper();                        
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
                        if ((dr["BookGroup"].ToString().ToUpper().Equals(dr2["BookGroup"].ToString().ToUpper())) &&                             
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

            return dsTarget;
        }

        public static DataSet ContractsByBookGroupSharesCash(DataSet dsContracts, Locale localeType)
        {
            DataSet dsTarget = new DataSet();
            bool found = false;

            dsTarget.Tables.Add("ContractSummary");

            dsTarget.Tables["ContractSummary"].Columns.Add("BookGroup", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("CurrencyIso", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("BorrowQuantity", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("LoanQuantity", typeof(decimal));
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
                        if ((dr["BookGroup"].ToString().ToUpper().Equals(dr2["BookGroup"].ToString().ToUpper())) &&
                            (dr["CurrencyIso"].ToString().ToUpper().Equals(dr2["CurrencyIso"].ToString().ToUpper())))
                        {
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        DataRow tempRecord = dsTarget.Tables["ContractSummary"].NewRow();
                        tempRecord["BookGroup"] = dr["BookGroup"].ToString().ToUpper();
                        tempRecord["CurrencyIso"] = dr["CurrencyIso"].ToString().ToUpper();
                        tempRecord["BorrowQuantity"] = 0;
                        tempRecord["BorrowAmount"] = 0;
                        tempRecord["LoanQuantity"] = 0;
                        tempRecord["LoanAmount"] = 0;

                        dsTarget.Tables["ContractSummary"].Rows.Add(tempRecord);
                        dsTarget.AcceptChanges();
                    }
                }

                long borrowQuantity = 0;
                long loanQuantity = 0;

                decimal borrowAmount = 0;
                decimal loanAmount = 0;

                foreach (DataRow dr in dsTarget.Tables["ContractSummary"].Rows)
                {
                    borrowQuantity = 0;
                    borrowAmount = 0;
                    
                    loanQuantity = 0;
                    loanAmount = 0;

                    foreach (DataRow dr2 in dsContracts.Tables["Contracts"].Rows)
                    {
                        if ((dr["BookGroup"].ToString().ToUpper().Equals(dr2["BookGroup"].ToString().ToUpper())) &&
                             (dr["CurrencyIso"].ToString().ToUpper().Equals(dr2["CurrencyIso"].ToString().ToUpper())))
                        {
                            if (dr2["ContractType"].ToString().Equals("B"))
                            {
                                borrowQuantity += (long)dr2["Quantity"];
                                borrowAmount += (decimal)dr2["Amount"];
                            }
                            else
                            {
                                loanQuantity += (long)dr2["Quantity"];
                                loanAmount += (decimal)dr2["Amount"];
                            }
                        }
                    }

                    dr["BorrowQuantity"] = borrowQuantity;
                    dr["LoanQuantity"] = loanQuantity;

                    dr["BorrowAmount"] = borrowAmount;
                    dr["LoanAmount"] = loanAmount;
                }
            }

            return dsTarget;
        }


        public static DataSet ContractsByBookCash(DataSet dsContracts, Locale localeType)
        {
            bool found = false;


                foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
                {
                    dr["BookGroup"] = "****";
                }
                dsContracts.AcceptChanges();

            DataSet dsTarget = new DataSet();
            dsTarget.Tables.Add("ContractSummary");

            dsTarget.Tables["ContractSummary"].Columns.Add("BookGroup", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("Book", typeof(string));            
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
                        if ((dr["BookGroup"].ToString().ToUpper().Equals(dr2["BookGroup"].ToString().ToUpper())) &&
                            (dr["Book"].ToString().ToUpper().Equals(dr2["Book"].ToString().ToUpper())) &&
                            (dr["CurrencyIso"].ToString().ToUpper().Equals(dr2["CurrencyIso"].ToString().ToUpper())))
                        {
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        DataRow tempRecord = dsTarget.Tables["ContractSummary"].NewRow();
                        tempRecord["BookGroup"] = dr["BookGroup"].ToString().ToUpper();
                        tempRecord["Book"] = dr["Book"].ToString().ToUpper();                        
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
                        if ((dr["BookGroup"].ToString().ToUpper().Equals(dr2["BookGroup"].ToString().ToUpper())) &&
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

            return dsTarget;
        }

        public static DataSet ContractsBySecurity(DataSet dsContracts)
        {
            DataSet dsTarget = new DataSet();

            bool found = false;

            dsTarget = new DataSet();
            dsTarget.Tables.Add("ContractSummary");

            dsTarget.Tables["ContractSummary"].Columns.Add("BookGroup", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("SecId", typeof(string));           
            dsTarget.Tables["ContractSummary"].Columns.Add("Symbol", typeof(string));            
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
                            (dr["PoolCode"].ToString().ToUpper().Equals(dr2["PoolCode"].ToString().ToUpper())))
                        {
                            if (dr["PoolCode"].ToString().ToUpper().Equals(dr2["PoolCode"].ToString().ToUpper()))
                                {
                                    found = true;
                                }                            
                        }
                    }

                    if (!found)
                    {

                        DataRow tempRecord = dsTarget.Tables["ContractSummary"].NewRow();
                        tempRecord["BookGroup"] = dr["BookGroup"].ToString();
                        tempRecord["SecId"] = dr["SecId"].ToString().ToUpper();                        
                        tempRecord["Symbol"] = dr["Symbol"].ToString().ToUpper();                       
                        tempRecord["CurrencyIso"] = dr["CurrencyIso"].ToString().ToUpper();
                        tempRecord["BorrowQuantity"] = 0;
                        tempRecord["BorrowAmount"] = 0;
                        tempRecord["LoanQuantity"] = 0;
                        tempRecord["LoanAmount"] = 0;
                        tempRecord["BorrowRate"] = 0;
                        tempRecord["LoanRate"] = 0;
                        tempRecord["PoolCode"] = dr["PoolCode"].ToString();
                      

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

            return dsTarget;
        }

        public static DataSet ContractsExcessCollateral(DataSet dsContracts)
        {
            decimal price = 0;
            decimal excessCollateral = 0;

            dsContracts.Tables["Contracts"].Columns.Add("excessCollateral");            
            dsContracts.AcceptChanges();

            foreach (DataRow drRow in dsContracts.Tables["Contracts"].Rows)
            {
                excessCollateral = 0;
                price = 0;

                    try
                    {
                        price = (decimal.Parse(drRow["Amount"].ToString()) / decimal.Parse(drRow["Quantity"].ToString())) / decimal.Parse(drRow["Margin"].ToString());
                    }
                    catch { }

                    switch (drRow["ContractType"].ToString())
                    {                            
                        case "B":                            
                            excessCollateral = decimal.Parse(drRow["Amount"].ToString()) - (price * decimal.Parse(drRow["Quantity"].ToString()) * decimal.Parse("1.05"));
                            break;

                        case "L":                            
                            excessCollateral = (price * decimal.Parse(drRow["Quantity"].ToString())) - decimal.Parse(drRow["Amount"].ToString());
                            break;
                    }

                    drRow["excessCollateral"] = excessCollateral;                
            }

            dsContracts.AcceptChanges();

            return dsContracts;
        }

        public static DataSet ContractsExcessCollateralSummary(DataSet dsContracts)
        {
            bool found = false;
          
            DataSet dsTarget = new DataSet();
            dsTarget.Tables.Add("ContractSummary");

            dsTarget.Tables["ContractSummary"].Columns.Add("BookGroup", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("Book", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("CurrencyIso", typeof(string));
            dsTarget.Tables["ContractSummary"].Columns.Add("BorrowExCoAmount", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("LoanExCoAmount", typeof(decimal));
            dsTarget.Tables["ContractSummary"].Columns.Add("NetExCoAmount", typeof(decimal));
            dsTarget.AcceptChanges();

            if (dsContracts != null)
            {
                foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
                {
                    found = false;

                    foreach (DataRow dr2 in dsTarget.Tables["ContractSummary"].Rows)
                    {
                        if ((dr["BookGroup"].ToString().ToUpper().Equals(dr2["BookGroup"].ToString().ToUpper())) &&
                            (dr["Book"].ToString().ToUpper().Equals(dr2["Book"].ToString().ToUpper())) &&
                            (dr["CurrencyIso"].ToString().ToUpper().Equals(dr2["CurrencyIso"].ToString().ToUpper())))
                        {
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        DataRow tempRecord = dsTarget.Tables["ContractSummary"].NewRow();
                        tempRecord["BookGroup"] = dr["BookGroup"].ToString().ToUpper();
                        tempRecord["Book"] = dr["Book"].ToString().ToUpper();
                        tempRecord["CurrencyIso"] = dr["CurrencyIso"].ToString().ToUpper();

                        if (dr["ContractType"].ToString().Equals("B"))
                        {
                            tempRecord["BorrowExCoAmount"] = 0;
                        }
                        else
                        {
                            tempRecord["LoanExCoAmount"] = 0;
                        }

                        tempRecord["NetExCoAmount"] = 0;
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
                        if ((dr["BookGroup"].ToString().ToUpper().Equals(dr2["BookGroup"].ToString().ToUpper())) &&
                             (dr["Book"].ToString().ToUpper().Equals(dr2["Book"].ToString().ToUpper())) &&
                             (dr["CurrencyIso"].ToString().ToUpper().Equals(dr2["CurrencyIso"].ToString().ToUpper())))
                        {
                            if (dr2["ContractType"].ToString().Equals("B"))
                            {
                                borrowAmount += decimal.Parse(dr2["excessCollateral"].ToString());
                            }
                            else
                            {
                                loanAmount += decimal.Parse(dr2["excessCollateral"].ToString());
                            }
                        }
                    }

                    dr["BorrowExCoAmount"] = borrowAmount;
                    dr["LoanExCoAmount"] = loanAmount;
                    dr["NetExCoAmount"] = borrowAmount + loanAmount;
                }
            }

            return dsTarget;
        }

        public static DataSet ContractsDetailsByBookGet(DataSet dsContracts, string bookGroup, string book, string currencyIso)
        {
            dsContracts = ContractsDetailByMarketValue(dsContracts, Locale.None);
            
            string dsContractsFilter = ((bookGroup.Equals("****")?"" : "BookGroup = '" + bookGroup + "' AND") + " Book = '" + book + "' AND CurrencyIso = '" + currencyIso + "'");
            DataView dvContracts = new DataView(dsContracts.Tables["Contracts"], dsContractsFilter.Trim(), "", DataViewRowState.CurrentRows);
            DataSet dsContractSummary = new DataSet();
            dsContractSummary.Tables.Add(dvContracts.ToTable("Contracts"));

            return dsContractSummary;
        }
    }

}
