using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockLoan.Business
{
    public class IPAddressRetrieve
    {
        private string GetIpAddress()
        {
            string hostName = System.Net.Dns.GetHostName();
            string currAddress = "";
            System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostName);  //System.Net.Dns.GetHostByName (hostName);
            System.Net.IPAddress [] addr = ipEntry.AddressList;
          
            for (int i = 0; i < addr.Length; i++)
            {
                currAddress = (addr[i].ToString());
            }
            return currAddress;
        }
    }
}
