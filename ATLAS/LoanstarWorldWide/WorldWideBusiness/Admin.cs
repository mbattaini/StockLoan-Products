using System;
using System.Text;
using System.Data;
using System.Globalization;
using StockLoan.DataAccess;

namespace StockLoan.Business
{
    public class Admin
    {
        public static DataSet CountriesGet(string countryCode)
        {
            try
            {
                if (!countryCode.Equals(""))
                {
                    countryCode.ToUpper();
                }

                DataSet dsCountries = new DataSet();
                dsCountries = DBAdmin.CountriesGet(countryCode);

                return dsCountries;
            }
            catch 
            {
                throw;
            }

        }

        public static DataSet CountryCodeIsoConversionsGet()
        {
            try
            {
                DataSet dsIsoCountryConversions = new DataSet();
                dsIsoCountryConversions = DBAdmin.CountryCodeIsoConversionGet();

                return dsIsoCountryConversions;
            }
            catch 
            {
                throw;
            }
        }

        public static void CountrySet(string countryCode, string country, string settleDays, bool isActive)
        {
            try
            {
                if (countryCode.Equals(""))
                {
                    throw new Exception("Country Code is required");
                }

                DBAdmin.CountrySet(countryCode, country, settleDays, isActive);

            }
            catch 
            {
                throw;
            }
        }

        public static DataSet CurrenciesGet(string currencyIso)
        {
            {
                try
                {
                    DataSet dsCurrencies = new DataSet();
                    dsCurrencies = DBAdmin.CurrenciesGet(currencyIso);

                    return dsCurrencies;
                }
                catch 
                {
                    throw;
                }
            }
        }

        public static DataSet CurrencyConversionsGet(string currencyIsoFrom)
        {
            try
            {
                DataSet dsCurrencyConversions = new DataSet();
                dsCurrencyConversions = DBAdmin.CurrencyConversionsGet(currencyIsoFrom);

                return dsCurrencyConversions;
            }
            catch 
            {
                throw;
            }
        }

        public static void currencyConversionSet(string currencyIsoFrom, string currencyIsoTo, string currencyConvertRate)
        {
            try
            {
                if (currencyConvertRate.Equals(""))
                {
                    throw new Exception("Currency convert rate is required");
                }

                if (currencyIsoFrom.Equals(""))
                {
                    throw new Exception("Currency ISO From value is required");
                }

                if (currencyIsoTo.Equals(""))
                {
                    throw new Exception("Currency ISO To value is required");
                }

                DBAdmin.CurrencyConversionSet(currencyIsoFrom, currencyIsoTo, currencyConvertRate);

            }
            catch 
            {
                throw;
            }
        }

        public static void CurrencySet(string currencyIso, string currency, bool isActive)
        {
            try
            {
                if (currencyIso.Equals(""))
                {
                    throw new Exception("Currency ISO is required");
                }

                if (currency.Equals(""))
                {
                    throw new Exception("Currency is required");
                }

                DBAdmin.CurrencySet(currencyIso, currency, isActive);

            }
            catch 
            {
                throw;
            }
        }

        public static DataSet DeliveryTypesGet()
        {
            try
            {
                DataSet dsDeliveryTypes = new DataSet();
                dsDeliveryTypes = DBAdmin.DeliveryTypesGet();

                return dsDeliveryTypes;
            }
            catch 
            {
                throw;
            }
        }

    }
}
