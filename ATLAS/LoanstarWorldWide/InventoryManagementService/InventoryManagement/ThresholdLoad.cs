using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StockLoan.Common;
using StockLoan.Transport;

namespace StockLoan.InventoryService
{
    public class ThresholdLoad
    {
        public ThresholdLoad(string bizDate)
        {

        }

        public void Load()
        {
            string url = Standard.ConfigValue("ThresholdListUrl", "");
            string query = Standard.ConfigValue("ThresholdListUrlQuery", "");

            HttpTransfer hTransfer = new HttpTransfer();
            hTransfer.FileGet(url, query, "", "",  @"C:\temp\Threshold.txt", true);
        }

    }
}
