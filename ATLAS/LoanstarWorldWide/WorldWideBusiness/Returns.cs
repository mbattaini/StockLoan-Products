using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.DataAccess;


namespace StockLoan.Business
{
    public class Returns
    { 
    
        public static int ReturnAsOfSet(
            string tradeDate, 
            string bookGroup, 
            string book, 
            string contractId, 
            string contractType, 
            string returnId, 
            string quantity, 
            string actUserId, 
            string settleDate)
        {
            try
            {

                if (tradeDate.Equals(""))
                {
                    throw new Exception("Trade Date is required");
                }

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required");
                }
                
                if (book.Equals(""))
                {
                    throw new Exception("Book is required");
                }
               
                if (contractId.Equals(""))
                {
                    throw new Exception("Contract ID is required");
                }
               
                if (contractType.Equals(""))
                {
                    throw new Exception("Contract Type is required");
                }
               
                if (returnId.Equals(""))
                {
                    throw new Exception("Return ID is required");
                }
               
                if (quantity.Equals(""))
                {
                    throw new Exception("Quantity is required");
                }
                
                if (actUserId.Equals(""))
                {
                    throw new Exception("User ID is required");
                }
                
                if (settleDate.Equals(""))
                {
                    throw new Exception("Settle Date is required");
                }

                int rowsReturn;

                rowsReturn = DBReturns.ReturnAsOfSet(
                    tradeDate, 
                    settleDate, 
                    bookGroup,
                    book,
                    contractId, 
                    contractType,
                    returnId, 
                    quantity,
                    actUserId);

                return rowsReturn;
            }
            catch 
            {
                throw;
            }
        }

        public static void ReturnSet(
            string returnId, 
            string bizDate, 
            string bookGroup, 
            string book, 
            string contractId,
            string contractType,
            string quantity, 
            string actUserId,
            string settleDateProjected, 
            string settleDateActual,
            bool isActive)
        {
            try
            {
                if (returnId.Equals(""))
                {
                    throw new Exception("Return ID is required");
                }
                
                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date is required");
                }
                
                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required");
                }
                
                if (book.Equals(""))
                {
                    throw new Exception("Book is required");
                }
                
                if (contractId.Equals(""))
                {
                    throw new Exception("Contract ID is required");
                }
                
                if (contractType.Equals(""))
                {
                    throw new Exception("Contract Type is required");
                }
                
                if (quantity.Equals(""))
                {
                    throw new Exception("Quantity is required");
                }
                
                if (actUserId.Equals(""))
                {
                    throw new Exception("User ID is required");
                }

                DBReturns.ReturnSet(
                    returnId,
                    bizDate, 
                    bookGroup, 
                    book,
                    contractId, 
                    contractType, 
                    quantity,
                    settleDateProjected,
                    settleDateActual, 
                    actUserId,
                    isActive);
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet ReturnsGet(string returnId, string bizDate, string bookGroup, string contractId, short utcOffSet)
        {
            DataSet dsTemp = new DataSet();
            try
            {
                dsTemp = DBReturns.ReturnsGet(returnId, bizDate, bookGroup, contractId, utcOffSet);

                return dsTemp;
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet ReturnsSummaryByCash(DataSet dsReturns)
        {
            DataSet dsTarget = new DataSet();

            bool found = false;
            try
            {
                Functions.DataSetScrub(ref dsReturns, "Returns", "IsActive");

                dsTarget = new DataSet();
                dsTarget.Tables.Add("CreditDebitSummary");
                dsTarget.Tables["CreditDebitSummary"].Columns.Add("Book", typeof(string));
                dsTarget.Tables["CreditDebitSummary"].Columns.Add("BookName", typeof(string));
                dsTarget.Tables["CreditDebitSummary"].Columns.Add("CurrencyIso", typeof(string));
                dsTarget.Tables["CreditDebitSummary"].Columns.Add("Debit", typeof(decimal));
                dsTarget.Tables["CreditDebitSummary"].Columns.Add("Credit", typeof(decimal));
                dsTarget.AcceptChanges();

                foreach (DataRow dr in dsReturns.Tables["Returns"].Rows)
                {
                    found = false;

                    foreach (DataRow dr2 in dsTarget.Tables["CreditDebitSummary"].Rows)
                    {
                        if ((Functions.CompareDates(dr["BizDate"].ToString(), dr["SettleDateActual"].ToString()) == 0))
                        {
                            if ((dr["Book"].ToString().Equals(dr2["Book"].ToString())) &&
                                (dr["CurrencyIso"].ToString().Equals(dr2["CurrencyIso"].ToString())))
                            {
                                switch (Functions.BalanceTypeCheck(InformationType.Returns, dr["ContractType"].ToString(), (decimal)dr["CashReturn"]))
                                {
                                    case (BalanceTypes.Credit):
                                        dr2["Credit"] = (decimal)dr2["Credit"] - (decimal)dr["CashReturn"];
                                        break;

                                    case (BalanceTypes.Debit):
                                        dr2["Debit"] = (decimal)dr2["Debit"] + (decimal)dr["CashReturn"];
                                        break;
                                }

                                found = true;
                            }
                        }
                    }

                    if (!found)
                    {
                        if ((Functions.CompareDates(dr["BizDate"].ToString(), dr["SettleDateActual"].ToString()) == 0))
                        {
                            DataRow tempDr = dsTarget.Tables["CreditDebitSummary"].NewRow();
                            tempDr["Book"] = dr["Book"].ToString();
                            tempDr["BookName"] = dr["BookName"].ToString();
                            tempDr["CurrencyIso"] = dr["CurrencyIso"].ToString();

                            switch (Functions.BalanceTypeCheck(InformationType.Returns, dr["ContractType"].ToString(), (decimal)dr["CashReturn"]))
                            {
                                case (BalanceTypes.Credit):
                                    tempDr["Credit"] = (decimal)dr["CashReturn"] * -1;
                                    tempDr["Debit"] = 0;
                                    break;

                                case (BalanceTypes.Debit):
                                    tempDr["Debit"] = (decimal)dr["CashReturn"];
                                    tempDr["Credit"] = 0;
                                    break;
                            }

                            dsTarget.Tables["CreditDebitSummary"].Rows.Add(tempDr);
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
    }
}
