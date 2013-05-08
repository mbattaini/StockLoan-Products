using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using StockLoan.Common;

namespace StockLoan.ParsingFiles
{
 
    class ParseSecMaster
    {
        public event EventHandler<ProgressEventArgs> ProgressChanged = delegate { };

        private string dbCnStr;
        private string exDbCnStr;
        private string bookGroup;
        private int count;
        private int interval;

        public ParseSecMaster(string dbCnStr, string extDbCnStr, string bookGroup)
        {
            this.dbCnStr = dbCnStr;
            this.exDbCnStr = extDbCnStr;
            this.bookGroup = bookGroup;
            count = 0;
            interval = 100;
        }

        public void Load(string secmasterSql, string bizDate)
        {
            
            SqlConnection dbCn = new SqlConnection(exDbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand(secmasterSql, dbCn);
                dbCmd.CommandType = CommandType.Text;
                dbCmd.CommandTimeout = 900;

                dbCn.Open();
                SqlDataReader sqlDataReader = dbCmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    SecMasterItemSet(
                        sqlDataReader.GetValue(0).ToString(),
                        sqlDataReader.GetValue(1).ToString(),
                        sqlDataReader.GetValue(2).ToString(),
                        sqlDataReader.GetValue(3).ToString(),
                        sqlDataReader.GetValue(4).ToString(),
                        sqlDataReader.GetValue(5).ToString(),
                        sqlDataReader.GetValue(6).ToString(),
                        Standard.ConfigValue("SecMasterMSDBaseType[" + sqlDataReader.GetValue(7).ToString() + "]", "Unknown"),
                        Standard.ConfigValue("SecMasterMSDCode[" + sqlDataReader.GetValue(7).ToString() + "]", "U"),
                        sqlDataReader.GetValue(8).ToString(),
                        sqlDataReader.GetValue(9).ToString(),
                        sqlDataReader.GetValue(10).ToString(),
                        sqlDataReader.GetValue(11).ToString(),
                        sqlDataReader.GetValue(12).ToString(),
                        sqlDataReader.GetValue(12).ToString());

                    count++;

                    if ((count % interval) == 0)
                    {
                        UpdateProgress(count);
                    }
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public  void UpdateProgress(long count)
        {
            EventHandler<ProgressEventArgs> progressEvent = ProgressChanged;

            progressEvent(null, new ProgressEventArgs(count));
        }

        private void SecMasterItemSet(
            string secId, 
            string custom,
            string symbol, 
            string isin, 
            string sedol,
            string cusip,
            string description, 
            string baseType, 
            string classGroup, 
            string countryCode, 
            string currencyIso, 
            string lastPrice,
            string lastPriceDate,       
            string recordDateCash, 
            string dividendRate)
        {
            SqlConnection dbCn = new SqlConnection(dbCnStr);

            try
            {
                SqlCommand dbCmd = new SqlCommand("spSecMasterItemSet", dbCn);
                dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
                paramSecId.Value = secId;

                SqlParameter paramDescription = dbCmd.Parameters.Add("@Description", SqlDbType.VarChar, 8000);
                paramDescription.Value = description;

                SqlParameter paramBaseType = dbCmd.Parameters.Add("@BaseType", SqlDbType.Char, 1);
                paramBaseType.Value = baseType;

                if (!classGroup.Equals(""))
                {
                    SqlParameter paramClassGroup = dbCmd.Parameters.Add("@ClassGroup", SqlDbType.VarChar, 25);
                    paramClassGroup.Value = classGroup;
                }

                if (!countryCode.Equals(""))
                {
                    SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.Char, 4);
                    paramCountryCode.Value = countryCode;
                }

                if (!currencyIso.Equals(  ""))
                {
                    SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyCode", SqlDbType.Char, 3);
                    paramCurrencyIso.Value = currencyIso;
                }

                if (!custom.Equals(""))
                {
                    SqlParameter paramCustom = dbCmd.Parameters.Add("@Custom", SqlDbType.VarChar, 12);
                    paramCustom.Value = custom;
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

                if (!lastPrice.Equals(""))
                {
                    SqlParameter paramLastPrice = dbCmd.Parameters.Add("@LastPrice", SqlDbType.Float);
                    paramLastPrice.Value = lastPrice;
                }

                if (!lastPriceDate.Equals(""))
                {
                    SqlParameter paramLastPriceDate = dbCmd.Parameters.Add("@LastPriceDate", SqlDbType.DateTime);
                    paramLastPriceDate.Value = lastPriceDate;
                }

                if (!recordDateCash.Equals(""))
                {
                    SqlParameter paramRecordDateCash = dbCmd.Parameters.Add("@RecordDateCash", SqlDbType.DateTime);
                    paramRecordDateCash.Value = recordDateCash;
                }

                dbCn.Open();
                dbCmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
            finally
            {
                if (dbCn.State != ConnectionState.Closed)
                {
                    dbCn.Close();
                }
            }
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public int Interval
        {
            get
            {
                return interval;
            }

            set
            {
                interval = value;
            }
        }
    }
}
