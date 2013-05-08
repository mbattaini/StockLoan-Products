using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.DataAccess;

namespace StockLoan.Business
{
    public class Marks
    {
        public static bool MarkIsExist(
            string bookGroup, 
            string book, 
            string contractId, 
            string contractType, 
            string secId, 
            string amount, 
            DataSet dsMarks)
        {
            try
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
            catch 
            {
                throw;
            }
        }

        public static DataSet MarksGet(string markId, string bizDate, string contractId, string bookGroup, short utcOffset)
        {
            DataSet dsTemp = new DataSet();

            try
            {
                dsTemp = DBMarks.MarksGet(markId, bizDate, contractId, bookGroup, utcOffset);

                return dsTemp;
            }
            catch 
            {
                throw;
            }
        }

        public static void MarkSet(
            string markId, 
            string bizDate, 
            string bookGroup, 
            string book, 
            string contractId, 
            string contractType, 
            string secId, 
            string amount, 
            string openDate, 
            string settleDate, 
            string deliveryCode, 
            string actUserId, 
            bool isActive)
        {
            try
            {

                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date is required");
                }

                if (actUserId.Equals(""))
                {
                    throw new Exception("User Id is required");
                }

                if (markId.Equals(""))
                {
                    throw new Exception("Mark ID is required");
                }

                if (bookGroup.Equals(""))
                {
                    throw new Exception("Book Group is required");
                }

                DBMarks.MarkSet(
                    markId, 
                    bizDate, 
                    bookGroup, 
                    book, 
                    contractId, 
                    contractType, 
                    secId, 
                    amount, 
                    openDate,
                    settleDate, 
                    deliveryCode, 
                    actUserId, 
                    isActive);
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet MarkSummary(DataSet dsMarks, string bizDate, string bookGroup, string bizDateFormat)
        {
            bool found = false;

            DataSet dsTarget = new DataSet();
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
                {	

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
                            }
                            else
                            {
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
            return dsTarget;
        }


        public static DataSet MarksSummaryByCash(DataSet dsMarks)
        {
            DataSet dsTarget = new DataSet();
            bool found = false;
            
            try
            {
                Functions.DataSetScrub(ref dsMarks, "Marks", "IsActive");

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
                            switch (Functions.BalanceTypeCheck(InformationType.Marks, dr["ContractType"].ToString(), (decimal)dr["Amount"]))
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

                        switch (Functions.BalanceTypeCheck(InformationType.Marks, dr["ContractType"].ToString(), (decimal)dr["Amount"]))
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
                        return dsTarget;
                    }
                }
            
                return dsTarget;
            }
            catch 
            {
                throw;
            }
        }


        public static int MarkAsOfSet(
            string tradeDate, 
            string settleDate, 
            string bookGroup, 
            string book, 
            string contractId, 
            string contractType,
            string price, 
            string markId, 
            string deliveryCode, 
            string actUserId)
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

                if (price.Equals(""))
                {
                    throw new Exception("Price is required");
                }

                if (markId.Equals(""))
                {
                    throw new Exception("MarkId is required");
                }

                if (deliveryCode.Equals(""))
                {
                    deliveryCode.Equals(DBNull.Value);
                }

                if (actUserId.Equals(""))
                {
                    throw new Exception("User ID is required");
                }

                int recordsUpdated;

                recordsUpdated = DBMarks.RetroMarkSet(
                   tradeDate,
                   settleDate,
                   bookGroup,
                   book,
                   contractId,
                   contractType,
                   price,
                   markId,
                   deliveryCode,
                   actUserId);

                return recordsUpdated;
            }
            catch
            {
                throw;
            }
        }

    }
}
