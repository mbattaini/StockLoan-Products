using System;
using System.Collections.Generic;
using System.Text;

namespace StockLoan.MainBusiness
{
    public struct PriceFileMask
    {
        int secIdIndex;
        int secIdAltIndex;
        int countryCodeIndex;
        int currencyIsoIndex;
        int priceIndex;

        public int SecIdIndex
        {
            get
            {
                return secIdIndex;
            }

            set
            {
                secIdIndex = value;
            }
        }

        public int SecIdAltIndex
        {
            get
            {
                return secIdAltIndex;
            }

            set
            {
                secIdAltIndex = value;
            }
        }

        public int CountryCodeIndex
        {
            get
            {
                return countryCodeIndex;
            }

            set
            {
                countryCodeIndex = value;
            }
        }

        public int CurrencyIsoIndex
        {
            get
            {
                return currencyIsoIndex;
            }

            set
            {
                currencyIsoIndex = value;
            }
        }

        public int PriceIndex
        {
            get
            {
                return priceIndex;
            }

            set
            {
                priceIndex = value;
            }
        }
    }

    public struct ReportItem
    {
        public string reportId;
        public string reportName;
        public bool isBizDateStartParamUsed;
        public bool isBizDateStopParamUsed;
        public bool isBookGroupParamUsed;
        public string report_sp;
    }

    public struct ReportRequestItem
    {
        public string bizDate;
        public string reportId;
        public string recipient;
        public string reportType;
        public string recipientEmail;
        public string bizDateStartParam;
        public string bizDateStopParam;
        public string bookGroupParam;
        public string emailDate;
        public string status;
        public string statusMessage;
    }
}
