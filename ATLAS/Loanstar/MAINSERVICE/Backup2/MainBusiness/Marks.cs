using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace StockLoan.MainBusiness
{
    public class Marks
    {        
        public static bool MarkIsExist(string bookGroup, string book, string contractId, string contractType, string secId, string amount, DataSet dsMarks)
        {
            foreach (DataRow drMarkRow in dsMarks.Tables["Marks"].Rows)
            {
                if (drMarkRow["BookGroup"].ToString().Equals(bookGroup) &&
                    drMarkRow["Book"].ToString().Equals(book) &&
                    drMarkRow["ContractId"].ToString().Equals(contractId) &&
                    drMarkRow["ContractType"].ToString().Equals(contractType) &&
                    drMarkRow["SecId"].ToString().Equals(secId))
                {
                    return true;
                }
            }

            return false;
        }
        public static void MarkSummary(DataSet dsMarks, ref DataSet dsTarget, string bizDate, string bookGroup, string bizDateFormat)
        {
            bool found = false;

            dsTarget = new DataSet();
            dsTarget.Tables.Add("MarkSummary");
            dsTarget.Tables["MarkSummary"].Columns.Add("BookGroup", typeof(string));
            dsTarget.Tables["MarkSummary"].Columns.Add("Book", typeof(string));
            dsTarget.Tables["MarkSummary"].Columns.Add("BookName", typeof(string));
            dsTarget.Tables["MarkSummary"].Columns.Add("CurrencyIso", typeof(string));
            dsTarget.Tables["MarkSummary"].Columns.Add("UnsettledCash", typeof(decimal));
            dsTarget.Tables["MarkSummary"].Columns.Add("SettledCash", typeof(decimal));
            dsTarget.AcceptChanges();

            foreach (DataRow dr in dsMarks.Tables["Marks"].Rows)
            {
                found = false;

                foreach (DataRow dr2 in dsTarget.Tables["MarkSummary"].Rows)
				{	//DC  Note: dsTarget => dsMarksSummary and MarksSummaryGrid was created with Group By = BookGroup, Book, CurrencyIso.

                    if (dr["BookGroup"].ToString().ToUpper().Equals(dr2["BookGroup"].ToString().ToUpper()) &&
                        dr["Book"].ToString().ToUpper().Equals(dr2["Book"].ToString().ToUpper()) &&
                        dr["CurrencyIso"].ToString().ToUpper().Equals(dr2["CurrencyIso"].ToString().ToUpper()))
                    {
                        try
                        {
							if (dr["SettleDate"] == DBNull.Value)					//DC won't work with == null
                            {
                                dr["SettleDate"] = "";
                            }

                            if (dr["SettleDate"].ToString().Equals(""))
                            {
                                dr2["UnSettledCash"] = (decimal)dr2["UnsettledCash"] + (decimal)dr["Amount"];
                                // dr2["SettledCash"] = 0;		//DC While accumulating UnSettled-Cash, do Not reset already accumulated Settled-Cash.
                            }
                            else
                            {
								// dr2["UnsettledCash"] = 0;	//DC While accumulating Settled-Cash, do Not reset already accumulated UNSettled-Cash.
                                dr2["SettledCash"] = (decimal)dr2["SettledCash"] + (decimal)dr["Amount"];
                            }
                        }
                        catch
                        {
                            Console.WriteLine(dr["SecId"].ToString() + " " + dr["ContractId"].ToString());
                        }
                            
                        found = true;
                    }                                       
                }

                if (!found)
                {	
                    DataRow tempDr = dsTarget.Tables["MarkSummary"].NewRow();
                    tempDr["BookGroup"] = dr["BookGroup"].ToString();
                    tempDr["Book"] = dr["Book"].ToString();
                    tempDr["BookName"] = dr["BookName"].ToString();
                    tempDr["CurrencyIso"] = dr["CurrencyIso"].ToString();

                    if (dr["SettleDate"] == DBNull.Value)					//DC was == null 
                    {
                        dr["SettleDate"] = "";
                    }

                    if (dr["SettleDate"].ToString().Equals(""))
                    {
                        tempDr["UnSettledCash"] = (decimal)dr["Amount"];
                        tempDr["SettledCash"] = 0;
                    }
                    else 
                    {
                        tempDr["UnsettledCash"] = 0;
                        tempDr["SettledCash"] = (decimal)dr["Amount"];
                    }


                    dsTarget.Tables["MarkSummary"].Rows.Add(tempDr);
                }
            }
        }
        public static void SummaryByCashMarks(DataSet dsMarks, ref DataSet dsTarget)
        {
            bool found = false;

            StandardFunctions.DataSetScrub(ref dsMarks, "Marks", "IsActive");

            dsTarget = new DataSet();
            dsTarget.Tables.Add("CreditDebitSummary");            
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("Book", typeof(string));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("BookName", typeof(string));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("CurrencyIso", typeof(string));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("Debit", typeof(decimal));
            dsTarget.Tables["CreditDebitSummary"].Columns.Add("Credit", typeof(decimal));
            dsTarget.AcceptChanges();

            foreach (DataRow dr in dsMarks.Tables["Marks"].Rows)
            {
                found = false;

                foreach (DataRow dr2 in dsTarget.Tables["CreditDebitSummary"].Rows)
                {
                    if ((dr["Book"].ToString().Equals(dr2["Book"].ToString())) &&
                        (dr["CurrencyIso"].ToString().Equals(dr2["CurrencyIso"].ToString())))
                    {
                        switch (StandardFunctions.BalanceTypeCheck(InformationType.Marks, dr["ContractType"].ToString(), (decimal)dr["Amount"]))
                        {
                            case (BalanceTypes.Credit):
                                dr2["Credit"] = (decimal)dr2["Credit"] + (decimal)dr["Amount"];
                                break;

                            case (BalanceTypes.Debit):
                                dr2["Debit"] = (decimal)dr2["Debit"] + (decimal)dr["Amount"];
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

                    switch (StandardFunctions.BalanceTypeCheck(InformationType.Marks, dr["ContractType"].ToString(), (decimal)dr["Amount"]))
                    {
                        case (BalanceTypes.Credit):
                            tempDr["Credit"] = (decimal)dr["Amount"];
                            tempDr["Debit"] = 0;
                            break;

                        case (BalanceTypes.Debit):
                            tempDr["Debit"] = (decimal)dr["Amount"];
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
