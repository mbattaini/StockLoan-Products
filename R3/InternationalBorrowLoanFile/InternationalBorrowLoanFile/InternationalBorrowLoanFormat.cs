using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using StockLoan.Common;

namespace InternationalBorrowLoanFile
{
    class InternationalBorrowLoanFormat
    {     
        public static string Parse(string bizDate, DataSet dsContracts)
        {
            string file = "";
            int recordCount = 0;


            //construct header
            string header = "";
            header += "1" + Standard.ConfigValue("FileName") + DateTime.Parse(bizDate).ToString("yyyyMMdd") + new string(' ', 89) + "\r\n";

            //construct body
            string body = "";
            string line = "";

            foreach (DataRow dr in dsContracts.Tables["Contracts"].Rows)
            {
                line += "5";

                if (dr["Isin"].ToString().Equals(""))
                {
                    if (dr["Symbol"].ToString().Equals(""))
                    {
                        line += dr["SecId"].ToString().PadLeft(25, ' ');
                    }
                    else
                    {
                        line += dr["Symbol"].ToString().PadLeft(25, ' ');
                    }
                }
                else
                {
                    line += dr["Isin"].ToString().PadLeft(25, ' ');
                }
                

                if (dr["ContractType"].ToString().Equals("B"))
                {
                    line += Standard.ConfigValue("BorrowAccountNumber", "123456789").PadLeft(25, ' ');
                }
                else
                {
                    line += Standard.ConfigValue("LoanAccountNumber", "123456789").PadLeft(25, ' ');
                }

                if (dr["ContractType"].ToString().Equals("B"))
                {
                    line += "D";
                }
                else
                {
                    line += "R";
                }

                if (dr["ContractType"].ToString().Equals("B"))
                {
                    line += "+" + long.Parse(dr["Quantity"].ToString()).ToString("0000000000000.00000");
                }
                else
                {
                    line += "-" + long.Parse(dr["Quantity"].ToString()).ToString("0000000000000.00000");
                }


                if (dr["ContractType"].ToString().Equals("B"))
                {
                    line += "+" + decimal.Parse(dr["Amount"].ToString()).ToString("0000000000000.00000");
                }
                else
                {
                    line += "-" + decimal.Parse(dr["Amount"].ToString()).ToString("0000000000000.00000");
                }


                line += "+000";
                line += dr["CurrencyIso"].ToString();
                line += " \r\n";

                recordCount++;
                body += line;
                line = "";
            }


            //
            string trailer = "";
            trailer += "9" + recordCount.ToString().PadLeft(18, '0') + (new string(' ', 89)) + "\r\n";

            file = header + body + trailer;
            return file;
        }
    }
}
