using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoanetParsingMessages
{
    public class Functions
    {
        public static string CurrencyIso(string loanetCurrencyCode)
        {
            switch (loanetCurrencyCode.Trim())
            {
                case "00":
                    return "USD";

                default:
                    return "***";
            }
        }    
    }
}
