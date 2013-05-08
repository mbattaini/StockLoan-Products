using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using StockLoan.Common;


namespace StockLoan.DataAccess
{
    public class DBSecMaster
    {
        private static string dbCnStr = DBStandardFunctions.DbCnStr;

        public static void PriceSet(
            string bizDate,
            string secId,
            string countryCode,
            string currencyIso,
            string price,
            string priceDate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spPriceSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = bizDate;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
                paramCountryCode.Value = countryCode;

                SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.VarChar, 3);
                paramCurrencyIso.Value = currencyIso;

                SqlParameter paramPrice = dbCmd.Parameters.Add("@Price", SqlDbType.Float);
                paramPrice.Value = price;

                SqlParameter paramPriceDate = dbCmd.Parameters.Add("@PriceDate", SqlDbType.DateTime);
                paramPriceDate.Value = priceDate;

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

        public static DataSet PricesGet(string bizDate, string secId, string currencyIso)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spPricesGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!bizDate.Equals(""))
                {
                    SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                    paramBizDate.Value = bizDate;
                }

                if (!secId.Equals(""))
                {
                    SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                    paramSecId.Value = secId;
                }

                if (!currencyIso.Equals(""))
                {
                    SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.VarChar, 3);
                    paramCurrencyIso.Value = currencyIso;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "Prices");
            }
            catch
            {
                throw;
            }

            return dsTemp;
        }

        public static void SecIdAliasSet(string secId, string secIdTypeIndex, string secIdAlias)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spSecIdAliasSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramSecIdTypeIndex = dbCmd.Parameters.Add("@SecIdTypeIndex", SqlDbType.SmallInt);
                paramSecIdTypeIndex.Value = secIdTypeIndex;

                SqlParameter paramSecIdAlias = dbCmd.Parameters.Add("@SecIdAlias", SqlDbType.VarChar, 12);
                paramSecIdAlias.Value = secIdAlias;

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

        public static void SecIdSymbolLookup(
            string secIdAlias,
            ref string secId,
            ref string symbol,
            ref string sedol,
            ref string isin,
            ref string cusip,
            ref string ibes_ticker)
        {
            secId = "";
            symbol = "";
            sedol = "";
            isin = "";
            cusip = "";
            ibes_ticker = "";

            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spSecIdSymbolLookup", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramSecIdAlias = dbCmd.Parameters.Add("@SecIdAlias", SqlDbType.VarChar, 12);
                paramSecIdAlias.Value = secIdAlias;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Direction = ParameterDirection.Output;

                SqlParameter paramSymbol = dbCmd.Parameters.Add("@Symbol", SqlDbType.VarChar, 8);
                paramSymbol.Direction = ParameterDirection.Output;

                SqlParameter paramSedol = dbCmd.Parameters.Add("@Sedol", SqlDbType.VarChar, 12);
                paramSedol.Direction = ParameterDirection.Output;

                SqlParameter paramIsin = dbCmd.Parameters.Add("@Isin", SqlDbType.VarChar, 12);
                paramIsin.Direction = ParameterDirection.Output;

                SqlParameter paramCusip = dbCmd.Parameters.Add("@Cusip", SqlDbType.VarChar, 12);
                paramCusip.Direction = ParameterDirection.Output;

                SqlParameter paramIBES_ticker = dbCmd.Parameters.Add("@IBES_ticker", SqlDbType.VarChar, 8);
                paramIBES_ticker.Direction = ParameterDirection.Output;

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
                dbCn.Close();

                secId = paramSecId.Value.ToString();
                symbol = paramSymbol.Value.ToString();
                sedol = paramSedol.Value.ToString();
                isin = paramIsin.Value.ToString();
                cusip = paramCusip.Value.ToString();
                ibes_ticker = paramIBES_ticker.Value.ToString();
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

        public static DataSet SecMasterGet(
            string secId,
            string countryCode,
            string currencyIso,
            string bookGroup,
            string lookUpCriteria)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);
            DataSet dsTemp = new DataSet();

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spSecMasterGet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                if (!secId.Equals(""))
                {
                    SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                    paramSecId.Value = secId;
                }

                if (!countryCode.Equals(""))
                {
                    SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
                    paramCountryCode.Value = countryCode;
                }

                if (!currencyIso.Equals(""))
                {
                    SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.VarChar, 3);
                    paramCurrencyIso.Value = currencyIso;
                }

                if (!bookGroup.Equals(""))
                {
                    SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
                    paramBookGroup.Value = bookGroup;
                }

                if (!lookUpCriteria.Equals(""))
                {
                    SqlParameter paramLookUpCriteria = dbCmd.Parameters.Add("@LookUpCriteria", SqlDbType.VarChar, 50);
                    paramLookUpCriteria.Value = lookUpCriteria;
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
                dataAdapter.Fill(dsTemp, "SecMaster");
            }
            catch
            {
                throw;
            }

            return dsTemp;
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
            string isin,
            string cusip,
            string price,
            string priceDate,
            bool isActive)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("dbo.spSecMasterSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;
                dbCmd.CommandTimeout = int.Parse(Standard.ConfigValue("DatabaseTimeout", "300"));

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                if (!description.Equals(""))
                {
                    SqlParameter paramDescription = dbCmd.Parameters.Add("@Description", SqlDbType.VarChar, 250);
                    paramDescription.Value = description;
                }

                if (!baseType.Equals(""))
                {
                    SqlParameter paramBaseType = dbCmd.Parameters.Add("@BaseType", SqlDbType.Char, 1);
                    paramBaseType.Value = baseType;
                }

                if (!classGroup.Equals(""))
                {
                    SqlParameter paramClassGroup = dbCmd.Parameters.Add("@ClassGroup", SqlDbType.VarChar, 25);
                    paramClassGroup.Value = classGroup;
                }

                if (!countryCode.Equals(""))
                {
                    SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
                    paramCountryCode.Value = countryCode;
                }

                if (!currencyIso.Equals(""))
                {
                    SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.VarChar, 3);
                    paramCurrencyIso.Value = currencyIso;
                }

                if (!accruedInterest.Equals(""))
                {
                    SqlParameter paramAccruedInterest = dbCmd.Parameters.Add("@AccruedInterest", SqlDbType.Float);
                    paramAccruedInterest.Value = accruedInterest;
                }

                if (!recordDateCash.Equals(""))
                {
                    SqlParameter paramRecordDateCash = dbCmd.Parameters.Add("@RecordDateCash", SqlDbType.DateTime);
                    paramRecordDateCash.Value = recordDateCash;
                }

                if (!dividendRate.Equals(""))
                {
                    SqlParameter paramDividendRate = dbCmd.Parameters.Add("@DividendRate", SqlDbType.Float);
                    paramDividendRate.Value = dividendRate;
                }

                if (!secIdGroup.Equals(""))
                {
                    SqlParameter paramSecIdGroup = dbCmd.Parameters.Add("@SecIdGroup", SqlDbType.VarChar, 15);
                    paramSecIdGroup.Value = secIdGroup;
                }

                if (!symbol.Equals(""))
                {
                    SqlParameter paramSymbol = dbCmd.Parameters.Add("@Symbol", SqlDbType.VarChar, 12);
                    paramSymbol.Value = symbol;
                }

                if (!isin.Equals(""))
                {
                    SqlParameter paramIsin = dbCmd.Parameters.Add("@Isin", SqlDbType.VarChar, 12);
                    paramIsin.Value = isin;
                }

                if (!cusip.Equals(""))
                {
                    SqlParameter paramCusip = dbCmd.Parameters.Add("@Cusip", SqlDbType.VarChar, 12);
                    paramCusip.Value = cusip;
                }

                if (!price.Equals(""))
                {
                    SqlParameter paramPrice = dbCmd.Parameters.Add("@Price", SqlDbType.Float);
                    paramPrice.Value = price;
                }

                if (!priceDate.Equals(""))
                {
                    SqlParameter paramPriceDate = dbCmd.Parameters.Add("@PriceDate", SqlDbType.DateTime);
                    paramPriceDate.Value = priceDate;
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
    }
}
