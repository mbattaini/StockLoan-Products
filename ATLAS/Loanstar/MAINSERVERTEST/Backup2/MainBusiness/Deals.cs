using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using StockLoan.Main;

namespace StockLoan.MainBusiness
{
    public class Deals
    {   
        private short utcOffset = 0;

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

            if (dr["Rate"].ToString().Equals(""))
            {
                throw new Exception("Rate field cannot be empty");
            }

            if (dr["Margin"].ToString().Equals(""))
            {
                throw new Exception("Margin field cannot be empty");
            }
        }
       
    }
}
