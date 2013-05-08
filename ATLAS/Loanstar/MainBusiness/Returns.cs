using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace StockLoan.MainBusiness
{  
    public class Returns
    {                

        private InformationType infoType;
        private SettlementType settlementType;
        private ContractType contractType;    

        public static void SummaryByCashReturns(DataSet dsReturns, ref DataSet dsTarget)
        {
            bool found = false;

            StandardFunctions.DataSetScrub(ref dsReturns, "Returns", "IsActive");

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
                    if ((StandardFunctions.CompareDates(dr["BizDate"].ToString(), dr["SettleDateActual"].ToString()) == 0))
                    {
                        if ((dr["Book"].ToString().Equals(dr2["Book"].ToString())) &&
                            (dr["CurrencyIso"].ToString().Equals(dr2["CurrencyIso"].ToString())))
                        {
                            switch (StandardFunctions.BalanceTypeCheck(InformationType.Returns, dr["ContractType"].ToString(), (decimal)dr["CashReturn"]))
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
                    if ((StandardFunctions.CompareDates(dr["BizDate"].ToString(), dr["SettleDateActual"].ToString()) == 0))
                    {
                        DataRow tempDr = dsTarget.Tables["CreditDebitSummary"].NewRow();
                        tempDr["Book"] = dr["Book"].ToString();
                        tempDr["BookName"] = dr["BookName"].ToString();
                        tempDr["CurrencyIso"] = dr["CurrencyIso"].ToString();

                        switch (StandardFunctions.BalanceTypeCheck(InformationType.Returns, dr["ContractType"].ToString(), (decimal)dr["CashReturn"]))
                        {
                            case (BalanceTypes.Credit):
                                tempDr["Credit"] = (decimal)dr["CashReturn"]* -1;
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
        }
    }
}
