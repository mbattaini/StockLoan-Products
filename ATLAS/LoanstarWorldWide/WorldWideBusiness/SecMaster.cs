using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using StockLoan.DataAccess;

namespace StockLoan.Business
{
    public class SecMaster
    {

        public static void PriceSet(
            string bizDate, 
            string secId, 
            string countryCode,
            string currencyIso, 
            string price,
            string priceDate)
        {
            try
            {
                if (bizDate.Equals(""))
                {
                    throw new Exception("Biz Date is required");
                }
            
                if (secId.Equals(""))
                {
                    throw new Exception("Security ID is required");
                }
                
                if (countryCode.Equals(""))
                {
                    throw new Exception("Country Code is required");
                }
                
                if (currencyIso.Equals(""))
                {
                    throw new Exception("CurrencyISO value is required");
                }
                
                if (price.Equals(""))
                {
                    throw new Exception("Price is required");
                }
                
                if (priceDate.Equals(""))
                {
                    throw new Exception("Price Date  is required");
                }

                DBSecMaster.PriceSet(bizDate, secId, countryCode, currencyIso, price, priceDate);

            }
            catch 
            {
                throw;
            }
        }

        public static DataSet PricesGet(string bizDate, string secId, string currencyIso)
        {
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = DBSecMaster.PricesGet(bizDate, secId, currencyIso);

                return dsTemp;
            }
            catch 
            {
                throw;
            }
        }

        public static void SecIdAliasSet(string secId, string secIdTypeIndex, string secIdAlias)
        {
            try
            {
                if (secId.Equals(""))
                {
                    throw new Exception("Security ID is required");
                }
                
                if (secIdTypeIndex.Equals(""))
                {
                    throw new Exception("Security Type Index is required");
                }
                
                if (secIdAlias.Equals(""))
                {
                    throw new Exception("Security ID Alias is required");
                }

                DBSecMaster.SecIdAliasSet(secId, secIdTypeIndex, secIdAlias);
                
            }
            catch 
            {
                throw;
            }
        }

        public static void SecIdSymbolLookup(
            string secIdAlias, 
            ref string secId, 
            ref string symbol, 
            ref string sedol,
            ref string isin, 
            ref string cusip, 
            ref string ibes_ticker)
        {
            if (secIdAlias.Equals(""))
            {
                throw new Exception("Security ID Alias is required");
            }

            try
            {

                DBSecMaster.SecIdSymbolLookup(
                    secIdAlias, 
                    ref secId,
                    ref symbol, 
                    ref sedol, 
                    ref isin, 
                    ref cusip, 
                    ref ibes_ticker);
            }
            catch 
            {
                throw;
            }
        }

        public static DataSet SecMasterGet(string secId, string countryCode, string currencyIso, string bookGroup, string lookUpCriteria)
        {

            DataSet dsSecMaster = new DataSet();

            try
            {
                
                dsSecMaster = DBSecMaster.SecMasterGet(secId, countryCode, currencyIso, bookGroup, lookUpCriteria);

                return dsSecMaster;

            }
            catch 
            {
                throw;
            }
        }

        public static void SecMasterSet(
            string secId, 
            string description, 
            string baseType,
            string classGroup,
            string countryCode, 
            string currencyIso,
            string accruedInterest, 
            string recordDateCash,
            string dividendRate,
            string secIdGroup,
            string symbol,
            string Isin,
            string cusip,
            string price,
            string priceDate,
            bool isActive)
        {
            try
            {
                if (secId.Equals(""))
                {
                    throw new Exception("Security ID is required");
                }
                
                DBSecMaster.SecMasterSet(
                    secId, 
                    description,
                    baseType, 
                    classGroup, 
                    countryCode,
                    currencyIso,
                    accruedInterest,
                    recordDateCash,
                    dividendRate,
                    secIdGroup,
                    symbol,
                    Isin, 
                    cusip, 
                    price, 
                    priceDate,
                    isActive);
            }
            catch 
            {
                throw;
            }
        }
    }
}
