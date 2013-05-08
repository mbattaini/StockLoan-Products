using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using StockLoan.Common;

namespace StockLoan.DataAccess
{
    public class DBAdmin
    {
        private static string dbCnStr = DBStandardFunctions.DbCnStr;
                    
        public static DataSet CountriesGet(string countryCode)
        {
            SqlConnection dbCn = new SqlConnection(DBStandardFunctions.DbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spCountriesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!countryCode.Equals(""))
                {
                    SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
                    paramCountryCode.Value = countryCode;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Countries");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static DataSet CountryCodeIsoConversionGet()
        {
            SqlConnection dbCn = new SqlConnection(DBStandardFunctions.DbCnStr);
            DataSet dsTemp = new DataSet();
            
            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spCountryCodeIsoConversionGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "IsoCountryConversions");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void CountrySet(string countryCode, string country, string settleDays, bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spCountrySet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
                paramCountryCode.Value = countryCode;

                if (!country.Equals(""))
                {
                    SqlParameter paramCountry = dbCmd.Parameters.Add("@Country", SqlDbType.VarChar, 50);
                    paramCountry.Value = country;
                }

                if (!settleDays.Equals(""))
                {
                    SqlParameter paramSettleDays = dbCmd.Parameters.Add("@SettleDays", SqlDbType.BigInt);
                    paramSettleDays.Value = settleDays;
                }

                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public static DataSet CurrenciesGet(string currencyIso)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spCurrenciesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!currencyIso.Equals(""))
                {
                    SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.VarChar, 3);
                    paramCurrencyIso.Value = currencyIso;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Currencies");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static DataSet CurrencyConversionsGet(string currencyIsoFrom)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spCurrencyConversionsGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!currencyIsoFrom.Equals(""))
                {
                    SqlParameter paramIsoFrom = dbCmd.Parameters.Add("@CurrencyIsoFrom", SqlDbType.VarChar, 3);
                    paramIsoFrom.Value = currencyIsoFrom;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "CurrencyConversions");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }
        
        public static void CurrencyConversionSet(string currencyIsoFrom, string currencyIsoTo, string currencyConvertRate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spCurrencyConversionSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramCurrencyIsoFrom = dbCmd.Parameters.Add("@CurrencyIsoFrom", SqlDbType.VarChar, 3);
                paramCurrencyIsoFrom.Value = currencyIsoFrom;

                SqlParameter paramCurrencyIsoTo = dbCmd.Parameters.Add("@CurrencyIsoTo", SqlDbType.VarChar, 3);
                paramCurrencyIsoTo.Value = currencyIsoTo;

                SqlParameter paramCurrencyConvertRate = dbCmd.Parameters.Add("@CurrencyConvertRate", SqlDbType.Decimal);
                paramCurrencyConvertRate.Value = currencyConvertRate;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public static void CurrencySet(string currencyIso, string currency, bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spCurrencySet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.VarChar, 3);
                paramCurrencyIso.Value = currencyIso;

                SqlParameter paramCurrency = dbCmd.Parameters.Add("@Currency", SqlDbType.VarChar, 50);
                paramCurrency.Value = currency;

                SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                paramIsActive.Value = isActive;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public static DataSet DeliveryTypesGet()
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spDeliveryTypesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "DeliveryTypes");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }
    }
}
