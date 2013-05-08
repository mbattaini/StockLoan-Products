using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using StockLoan.Main;

namespace StockLoan.MainBusiness
{
    public class Formats
    {
        public static string Price  = "#,##0.000";
        public static string Rate   = "0.000";
        public static string FundingRate = "0.00000";
        public static string Margin = "0.00";
        public static string Cash = "#,##0.00";
        public static string MarkCash = "#,##0.00";
        public static string MarkPrice = "#,##0.00000000";
        public static string Collateral = "#,##0";
        public static string DividendRate = "0.0";
    }

    public class Filters
    {
        public bool IsIsin(string secId)
        {
          if (secId.Length == 12)
            {
                if (Char.IsLetter(secId[0]) && Char.IsLetter(secId[1]))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool IsSedol(string secId)
        {
            if (secId.Length == 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsCusip(string secId)
        {
            if (secId.Length == 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SecurityTypes SecurityTypeGet(string secId)
        {
            if (IsIsin(secId))
            {
                return SecurityTypes.Isin;
            }
            else if (IsCusip(secId))
            {
                return SecurityTypes.Cusip;
            }
            else if (IsSedol(secId))
            {
                return SecurityTypes.Sedol;
            }           
            else
            {
                return SecurityTypes.Unknown;
            }
        }
    }
}