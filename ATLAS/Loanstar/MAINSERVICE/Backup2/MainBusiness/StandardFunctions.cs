using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace StockLoan.MainBusiness
{
    public class StandardFunctions
    {
        public static void DataSetScrub(ref DataSet dsTarget, string tableName, string columnName, string value)
        {
            foreach (DataRow dr in dsTarget.Tables[tableName].Rows)
            {
                if (!dr[columnName].ToString().Equals(value))
                {
                    dr.Delete();
                }
            }
            dsTarget.AcceptChanges();
        } 

        public static void DataSetScrub(ref DataSet dsTarget, string tableName, string columnName)
        {
            foreach (DataRow dr in dsTarget.Tables[tableName].Rows)
            {
                if (!bool.Parse(dr[columnName].ToString()))
                {
                    dr.Delete();
                }
            }
            dsTarget.AcceptChanges();
        }

        public static BalanceTypes BalanceTypeCheck(InformationType infoType, string contractType, decimal amount)
        {
            switch (infoType)
            {
                case InformationType.Contracts:
                    if (contractType.Equals("B"))
                    {
                        return BalanceTypes.Debit;
                    }
                    else
                    {
                        return BalanceTypes.Credit;
                    }
                    break;

                case InformationType.Marks:
                    if (amount >= 0)
                    {
                        return BalanceTypes.Debit;
                    }
                    else
                    {
                        return BalanceTypes.Credit;
                    }
                    break;

                case InformationType.Returns:
                    if (contractType.Equals("B"))
                    {
                        return BalanceTypes.Debit;
                    }
                    else
                    {
                        return BalanceTypes.Credit;
                    }
                    break;
            
                default:
                    return BalanceTypes.Credit;
            }

        }

        public static int CompareDates(string date1, string date2)
        {            
            //  1 datetime1 >  datetime2
            //  0 datetime1 =  datetime2
            // -1 datetime1 <  datetime2         
            // -99 no result/error

            DateTime dateTime1;
            DateTime dateTime2;
            
            int result = -99;

            try
            {
                dateTime1 = DateTime.Parse(date1);
                dateTime2 = DateTime.Parse(date2);

                if (dateTime1 == dateTime2)
                {
                    result = 0;
                }
                else if (dateTime1 > dateTime2)
                {
                    result = 1;
                }
                else if (dateTime1 < dateTime2)
                {
                    result = -1;
                }
            }
            catch
            {
                result = -99;
            }

            return result;
        }
    }
}
