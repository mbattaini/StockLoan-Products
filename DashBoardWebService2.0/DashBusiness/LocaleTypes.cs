using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StockLoan.Common;

namespace DashBusiness
{
    public enum Locale
    {
        Domestic,
        International,
        None
    };

    public enum HtbLocale
    {
        Penson,
        Bps
    };

    public class LocaleTypes
    {
        public static Locale LocaleGet(string bookGroup)
        {
            switch (Standard.ConfigValue("BookGroup" + bookGroup + "_Locale").ToLower())
            {
                case "domestic":
                    return Locale.Domestic;
                    break;

                case "international":
                    return Locale.International;
                    break;

                default:
                    return Locale.None;
            }
        }
    }
}
