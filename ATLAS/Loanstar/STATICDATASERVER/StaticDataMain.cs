using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Mail;
using StockLoan.Common;

namespace StockLoan.StaticData
{
    public class StaticData
    {
        private string countryCode;

        private string dbCnStr;
        private SqlConnection dbCn = null;

		private string externalDbCnStr;
		private SqlConnection externalDbCn = null;

        private string externalPriceDbCnStr;
        private SqlConnection externalPriceDbCn = null;

        private string externalCurrencyConversionStr;
        private SqlConnection externalCurrencyConversionDbCn = null;

        private Thread mainThread = null;

        private static bool isStopped = true;
        private static string tempPath;        

        public StaticData(string dbCnStr, string externalDbCnStr, string externalPriceDbCnStr, string externalCurrencyConversionStr)
        {
            this.dbCnStr = dbCnStr;
			this.externalDbCnStr = externalDbCnStr;
            this.externalPriceDbCnStr = externalPriceDbCnStr;
            this.externalCurrencyConversionStr = externalCurrencyConversionStr;

            try
            {
                dbCn = new SqlConnection(dbCnStr);
				externalDbCn = new SqlConnection(externalDbCnStr);
                externalPriceDbCn = new SqlConnection(externalPriceDbCnStr);
                externalCurrencyConversionDbCn = new SqlConnection(externalCurrencyConversionStr);
            }
            catch (Exception e)
            {
                Log.Write(e.Message + " [StaticData.StaticData]", Log.Error, 1);
            }

            countryCode = Standard.ConfigValue("CountryCode", "US");
            Log.Write("Using country code: " + countryCode + " [StaticData.StaticData]", 2);

            if (Standard.ConfigValueExists("TempPath"))
            {
                tempPath = Standard.ConfigValue("TempPath");

                if (!Directory.Exists(tempPath))
                {
                    Log.Write("The configuration value for TempPath, " + tempPath + ", does not exist. [StaticData.StaticData]", Log.Error, 1);
                    tempPath = Directory.GetCurrentDirectory();
                }
            }
            else
            {
                Log.Write("A configuration value for TempPath has not been provided. [StaticData.StaticData]", Log.Information, 1);
                tempPath = Directory.GetCurrentDirectory();
            }

            Log.Write("Temporary files will be staged at " + tempPath + ". [StaticData.StaticData]", 2);
        }

        ~StaticData()
        {
            if (dbCn != null)
            {
                dbCn.Dispose();
            }
        }

        public void Start()
        {
            isStopped = false;

            if ((mainThread == null) || (!mainThread.IsAlive)) // Must create new thread.
            {
                mainThread = new Thread(new ThreadStart(StaticDataLoop));
                mainThread.Name = "Main";
                mainThread.Start();

                Log.Write("Start command issued with new main thread. [StaticData.Start]", 3);
            }
            else // Old thread will be just fine.
            {
                Log.Write("Start command issued with main thread already running. [StaticData.Start]", 3);
            }
        }

        public void Stop()
        {
            isStopped = true;

            if (mainThread == null)
            {
                Log.Write("Stop command issued, main thread never started. [StaticData.Stop]", 3);
            }
            else if (mainThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
            {
                mainThread.Abort();
                Log.Write("Stop command issued, sleeping main thread aborted. [StaticData.Stop]", 3);
            }
            else
            {
                Log.Write("Stop command issued, main thread is still active. [StaticData.Stop]", 3);
            }
        }

        private void StaticDataLoop()
        {
            while (!isStopped) // Loop through this block (otherwise exit method and thread dies).
            {
                Log.Write("Start of cycle. [StaticData.StaticDataLoop]", 2);
                KeyValue.Set("StaticDataCycleStartTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);

                Master.BizDate = KeyValue.Get("BizDate", "", dbCnStr);
                Master.BizDatePrior = KeyValue.Get("BizDatePrior", "", dbCnStr);

                SecurityMasterLoad();
                if (isStopped) break;

                /*CurrencyLoad();
                if (isStopped) break;*/

                CurrencyConversionsLoad();
                if (isStopped) break;

                StarsPricingLoad();
                if (isStopped) break;

                KeyValue.Set("StaticDataCycleStopTime", DateTime.UtcNow.ToString(Standard.DateTimeFormat), dbCn);
                Log.Write("End of cycle. [StaticData.StaticDataLoop]", 2);

                if (!isStopped)
                {
                    Thread.Sleep(RecycleInterval());
                }
            }
        }

		private string CountryCodeConvert(string countryCode)
		{
			DataSet dsCountryCodes = new DataSet();
			
			SqlConnection dbCn = new SqlConnection(dbCnStr);

			string answer = "";

			try
			{
				SqlCommand dbCmd = new SqlCommand("spCountryCodeIsoConversionGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsCountryCodes, "CountryCodes");


				foreach (DataRow dr in dsCountryCodes.Tables["CountryCodes"].Rows)
				{
					if (dr["CountryCodeIso"].ToString().Trim().Equals(countryCode))
					{
						answer = dr["CountryCode"].ToString();
					}
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [StaticData.CountryCodeConvert]", Log.Error, 1);
			}

			return answer;
		}

        public void SecurityMasterLoad()
		{
			DataSet dsSecMaster = new DataSet();			
			string secMasterSql = "";

			try
			{
				if (Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
				{
					if (!KeyValue.Get("StaticDataSecurityMasterLoadDate", "", dbCnStr).Equals(Master.BizDate))
					{
						if (DateTime.Now >= DateTime.Parse(KeyValue.Get("StaticDataSecurityMasterLoadTime", "04:30", dbCnStr)))
						{
							Log.Write("Loading Secuity Master.  [StaticData.SecurityMasterLoad]", 2);

                            secMasterSql = "select	ID As Sedol, \r\n" +
                                            "       stockFullName As Description, \r\n" +
                                            "	    substring(securityType, 1,1) AS BaseType, \r\n" +
                                            "	    securityType AS ClassGroup, \r\n" +
                                            "	    countryOfIssue As CountryCode, \r\n" +
                                            "	    denominatedCurrency As CurrencyIso, \r\n" +
                                            "	    '' AS AccruedInterest, \r\n" +
                                            "	    '' AS RecordDateCash, \r\n" +
                                            "	    100.0 As DividendRate, \r\n" +
                                            "	    '' As SecIdGroup, \r\n" +
                                            "	    tickerCode AS Symbol, \r\n" +
                                            "	    ISIN as Isin, \r\n" +
                                            "	    ''  AS Cusip \r\n" +
                                            "from	Instrument (nolock) \r\n" +
                                            "Where	UpdatedAt >= 2010-01-01 \r\n" +
                                            "And		denominatedCurrency is not null\r\n" +
                                            "And        len(ltrim(rtrim(denominatedCurrency))) > 0\r\n" +
                                            "And        len(ltrim(rtrim(countryOfIssue))) > 0\r\n" +
                                            "And		countryOfIssue is not null\r\n";


							SqlCommand externalDbCmd = new SqlCommand(secMasterSql, new SqlConnection(externalDbCnStr));
							externalDbCmd.CommandType = CommandType.Text;

							externalDbCmd.CommandTimeout = int.Parse(KeyValue.Get("StaticDataSecurityMasterTimeOut", "600", dbCnStr));

							SqlDataAdapter dataAdapter = new SqlDataAdapter(externalDbCmd);
							dataAdapter.Fill(dsSecMaster, "SecurityMaster");

							Log.Write("Loaded: " + dsSecMaster.Tables["SecurityMaster"].Rows.Count.ToString("#,##0") + " security master records.", 2);

							int i = 0;

							foreach (DataRow dr in dsSecMaster.Tables["SecurityMaster"].Rows)
							{
								SecMasterItemSet(dr["sedol"].ToString(),
                                                            dr["Description"].ToString(),
                                                             dr["BaseType"].ToString(),
                                                            dr["ClassGroup"].ToString(),
                                                             dr["CountryCode"].ToString(),
															dr["CurrencyIso"].ToString(),
                                                            dr["AccruedInterest"].ToString(),
                                                            dr["RecordDateCash"].ToString(),
                                                            dr["DividendRate"].ToString(),
                                                            dr["SecIdGroup"].ToString(),
                                                            dr["Symbol"].ToString(),
															dr["isin"].ToString(),
															dr["cusip"].ToString());


								i++;

								if ((i % 5000) == 0)
								{
									Log.Write("Processed: " + i.ToString("#,##0") + " items. [StaticData.SecurityMasterLoad]", 2);
								}
							}

							Log.Write("Security Master loaded for : " + Master.BizDate + ". [StaticData.SecurityMasterLoad]", 2);
							KeyValue.Set("StaticDataSecurityMasterLoadDate", Master.BizDate, dbCnStr);
						}
					}
					else
					{
						Log.Write("Security Master already loaded for : " + Master.BizDate + ". [StaticData.SecurityMasterLoad]", 2);
					}
				}
				else
				{
					Log.Write("Security Master load must wait for current buisness date. [StaticData.SecurityMasterLoad]", 2);
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [StaticData.SecurityMasterLoad]", 2);
			}

			dsSecMaster = null;
		}

		private void CurrencyLoad()
		{
			DataSet dsCurrencies = new DataSet();
			string currenciesSql = "";

			try
			{
				if (Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
				{
					if (!KeyValue.Get("StaticDataCurrencyLoadDate", "", dbCnStr).Equals(Master.BizDate))
					{
						if (DateTime.Now >= DateTime.Parse(KeyValue.Get("StaticDataCurrencyLoadTime", "04:30", dbCnStr)))
						{
							Log.Write("Loading Currencies.  [StaticData.CurrencyLoad]", 2);

							currenciesSql = "select	Isocurcd,\r\n" +
												   "			Isocurnm,\r\n" +
												   "			Isocurdd\r\n" +
												   "from		currency";

							SqlCommand externalDbCmd = new SqlCommand(currenciesSql, new SqlConnection(externalDbCnStr));
							externalDbCmd.CommandType = CommandType.Text;

							SqlDataAdapter dataAdapter = new SqlDataAdapter(externalDbCmd);
							dataAdapter.Fill(dsCurrencies, "Currency");

							Log.Write("Loaded: " + dsCurrencies.Tables["Currency"].Rows.Count.ToString("#,##0") + " currency iso records.", 2);

							int i = 0;

							foreach (DataRow dr in dsCurrencies.Tables["Currency"].Rows)
							{
								CurrencySet(dr["Isocurcd"].ToString(),
													dr["Isocurnm"].ToString(),
													(dr["Isocurdd"].ToString().Equals("") ? true : false));
								i++;

								if ((i % 5000) == 0)
								{
									Log.Write("Processed: " + i.ToString("#,##0") + " items. [StaticData.CurrencyLoad]", 2);
								}
							}

							Log.Write("Currencies loaded for : " + Master.BizDate + ". [StaticData.CurrencyLoad]", 2);
							KeyValue.Set("StaticDataCurrencyLoadDate", Master.BizDate, dbCnStr);
						}
					}
					else
					{
						Log.Write("Currencies already loaded for : " + Master.BizDate + ". [StaticData.CurrencyLoad]", 2);
					}
				}
				else
				{
					Log.Write("Currencies load must wait for current buisness date. [StaticData.CurrencyLoad]", 2);
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [StaticData.CurrencyLoad]", 2);
			}

			dsCurrencies = null;
		}

		private void CurrencyConversionsLoad()
		{
			DataSet dsCurrencies = new DataSet();
            string currenciesConversionsSql = "";

			try
			{
				if (Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
				{
					if (!KeyValue.Get("StaticDataCurrencyConversionsLoadDate", "", dbCnStr).Equals(Master.BizDate))
					{
						if (DateTime.Now >= DateTime.Parse(KeyValue.Get("StaticDataCurrencyConversionsLoadTime", "05:30", dbCnStr)))
						{
							Log.Write("Loading Currency Conversions.  [StaticData.CurrencyConversionsLoad]", 2);

                            currenciesConversionsSql = "Select	From_CCY AS ForeignCurrencyCode,\n" +
                                            "To_CCY As BaseCurrencyCode,\n" +
                                            "Price_Date As PriceDate,\n" +
                                            "ExRate As ExchangeRate\n" +
                                            "From	Fx_Rate\n" + 
                                            "Where ExtractNumber = 1\n";


                            SqlCommand externalDbCmd = new SqlCommand(currenciesConversionsSql, new SqlConnection(externalCurrencyConversionStr));
							externalDbCmd.CommandType = CommandType.Text;

							SqlDataAdapter dataAdapter = new SqlDataAdapter(externalDbCmd);
							dataAdapter.Fill(dsCurrencies, "Currency");

							Log.Write("Loaded: " + dsCurrencies.Tables["Currency"].Rows.Count.ToString("#,##0") + " currency iso coversion records.", 2);

							int i = 0;

							foreach (DataRow dr in dsCurrencies.Tables["Currency"].Rows)
							{
                                try
                                {
                                    CurrencyConversionSet(dr["ForeignCurrencyCode"].ToString(),
                                                        dr["BaseCurrencyCode"].ToString(),
                                                        decimal.Parse(dr["ExchangeRate"].ToString()),
                                                        dr["PriceDate"].ToString());
                                }
                                catch
                                {
                                    Log.Write("Skipping conversion rate " + dr["ForeignCurrencyCode"].ToString() + " to " + dr["BaseCurrencyCode"].ToString() + " w/rate " + dr["ExchangeRate"].ToString(), 1);
                                }

								i++;

								if ((i % 5000) == 0)
								{
									Log.Write("Processed: " + i.ToString("#,##0") + " items. [StaticData.CurrencyConversionsLoad]", 2);
								}
							}

							Log.Write("Currency Conversions loaded for : " + Master.BizDate + ". [StaticData.CurrencyConversionsLoad]", 2);
							KeyValue.Set("StaticDataCurrencyConversionsLoadDate", Master.BizDate, dbCnStr);
						}
					}
					else
					{
						Log.Write("Currency Conversion already loaded for : " + Master.BizDate + ". [StaticData.CurrencyConversionsLoad]", 2);
					}
				}
				else
				{
					Log.Write("Currency Conversion load must wait for current buisness date. [StaticData.CurrencyConversionsLoad]", 2);
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [StaticData.CurrencyConversionsLoad]", 2);
			}

			dsCurrencies = null;
		}

        private void StarsPricingLoad()
        {
            DataSet dsPrices = new DataSet();
            string pricingSql = "";

            try
            {
                if (Master.BizDate.Equals(DateTime.Now.ToString(Standard.DateFormat)))
                {
                    if (!KeyValue.Get("StaticDataStarsPriceLoadDate", "", dbCnStr).Equals(Master.BizDate))
                    {
                        if (DateTime.Now >= DateTime.Parse(KeyValue.Get("StaticDataStarsPriceLoadTime", "04:30", dbCnStr)))
                        {
                            Log.Write("Loading stars pricing.  [StaticData.StarsPricingLoad]", 2);

                            pricingSql =    "Select	S.ID AS Sedol,\r\n" +
		                                    "I.countryOfIssue As CountryCode,\r\n" +
		                                    "S.currencyOfQuote As currency,\r\n" +
		                                    "S.bidprice,\r\n" +
		                                    "S.priceChangeDate AS BizDate\r\n" +
                                            "from	price S,\r\n" +
                                            "		Instrument I\r\n" +
                                            "where	S.updatedat >= '" + Master.BizDate + "'\r\n" +
                                            "And		I.ID = S.ID\r\n" +
                                            "And		len(S.currencyOfQuote) = 3\r\n";

                            SqlCommand externalDbCmd = new SqlCommand(pricingSql, new SqlConnection(externalPriceDbCnStr));
                            externalDbCmd.CommandType = CommandType.Text;

                            SqlDataAdapter dataAdapter = new SqlDataAdapter(externalDbCmd);
                            dataAdapter.Fill(dsPrices, "Pricing");

                            Log.Write("Loaded: " + dsPrices.Tables["Pricing"].Rows.Count.ToString("#,##0") + " pricing records.", 2);

                            int i = 0;


                            if (dsPrices.Tables["Pricing"].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsPrices.Tables["Pricing"].Rows)
                                {
                                    PriceSet(dr["Sedol"].ToString(), dr["CountryCode"].ToString(), dr["Currency"].ToString(), decimal.Parse(dr["bidprice"].ToString()), Master.BizDate);

                                    i++;

                                    if ((i % 100) == 0)
                                    {
                                        Log.Write("Processed: " + i.ToString("#,##0") + " items. [StaticData.StarsPricingLoad]", 2);
                                    }
                                }

                                Log.Write("Pricing loaded for : " + Master.BizDate + ". [StaticData.StarsPricingLoad]", 2);
                                KeyValue.Set("StaticDataStarsPriceLoadDate", Master.BizDate, dbCnStr);
                            }
                            else
                            {
                                Log.Write("Pricing information unavailable.[StaticData.StarsPricingLoad]", 2);
                            }
                        }
                    }
                    else
                    {
                        Log.Write("Pricing already loaded for : " + Master.BizDate + ". [StaticData.StarsPricingLoad]", 2);
                    }
                }
                else
                {
                    Log.Write("Pricing load must wait for current buisness date. [StaticData.StarsPricingLoad]", 2);
                }
            }
            catch (Exception error)
            {
                Log.Write(error.Message + ". [StaticData.StarsPricingLoad]", 2);
            }

            dsPrices = null;
        }

		private void PriceSet(string secId, string countryCode, string currencyIso, decimal price, string priceDate)
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spPriceSet", localDbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
                paramBizDate.Value = Master.BizDate;
                
                SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

                SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 2);
                paramCountryCode.Value = countryCode;

                SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.Char, 3);
				paramCurrencyIso.Value = currencyIso;

				SqlParameter paramPrice = dbCmd.Parameters.Add("@Price", SqlDbType.Decimal);
				paramPrice.Value = price;

				SqlParameter paramPriceDate = dbCmd.Parameters.Add("@PriceDate", SqlDbType.DateTime);
				paramPriceDate.Value = priceDate;

				localDbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [StaticData.PriceSet]", Log.Error, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}
		}

	    private void SecMasterItemSet(string secId, string description, string baseType, string classGroup, string countryCode, string currencyIso, string accruedInterest, string recordDateCash, string dividendRate, string secIdGroup, string symbol, string isin, string cusip)
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spSecMasterItemSet", localDbCn);
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

				if (!currencyIso.Equals(""))
				{
					SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.Char, 3);
					paramCurrencyIso.Value = currencyIso;
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

				localDbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [StaticData.SecMasterItemSet]", Log.Error, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}
		}

		private void CurrencySet(string currencyIso, string currency, bool isActive)
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spCurrencySet", localDbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramCurrencyIso = dbCmd.Parameters.Add("@CurrencyIso", SqlDbType.Char, 3);
				paramCurrencyIso.Value = currencyIso;

				SqlParameter paramCurrency = dbCmd.Parameters.Add("@Currency", SqlDbType.Char, 50);
				paramCurrency.Value = currency;

				SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
				paramIsActive.Value = isActive;

				localDbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [StaticData.CurrencySet]", Log.Error, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}
		}

		private void CurrencyConversionSet(string currencyIsoFrom, string currencyIsoTo, decimal currencyConvertRate, string lastUpdated)
		{
			SqlConnection localDbCn = new SqlConnection(dbCnStr);

			try
			{
				SqlCommand dbCmd = new SqlCommand("spCurrencyConversionSet", localDbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramCurrencyIsoFrom = dbCmd.Parameters.Add("@CurrencyIsoFrom", SqlDbType.VarChar, 3);
				paramCurrencyIsoFrom.Value = currencyIsoFrom;

				SqlParameter paramCurrencyIsoTo = dbCmd.Parameters.Add("@CurrencyIsoTo", SqlDbType.VarChar, 3);
				paramCurrencyIsoTo.Value = currencyIsoTo;

				SqlParameter paramCurrencyConvertRate = dbCmd.Parameters.Add("@CurrencyConvertRate", SqlDbType.Decimal);
				paramCurrencyConvertRate.Value = currencyConvertRate;

                if (!lastUpdated.Equals(""))
                {
                    SqlParameter paramlastUpdated = dbCmd.Parameters.Add("@LastUpdated", SqlDbType.DateTime);
                    paramlastUpdated.Value = lastUpdated;
                }
			
				localDbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [StaticData.CurrencyConversionSet]", Log.Error, 1);
			}
			finally
			{
				if (localDbCn.State != ConnectionState.Closed)
				{
					localDbCn.Close();
				}
			}
		}
        
        private TimeSpan RecycleInterval()
        {
            string recycleInterval;
            string[] values;

            int hours;
            int minutes;

            bool isBizDay = Standard.IsBizDate(DateTime.UtcNow.Date, countryCode, Standard.HolidayType.Any, dbCn);
            TimeSpan timeSpan;

            char[] delimiter = new char[1];
            delimiter[0] = ':';

            if (isBizDay)
            {
                recycleInterval = KeyValue.Get("StaticDataLoopRecycleIntervalBizDay",  "0:15", dbCn);
            }
            else
            {
                recycleInterval = KeyValue.Get("StaticDataLoopRecycleIntervalNonBizDay",  "4:00", dbCn);
            }

            try
            {
                values = recycleInterval.Split(delimiter, 2);
                hours = int.Parse(values[0]);
                minutes = int.Parse(values[1]);
                timeSpan = new TimeSpan(hours, minutes, 0);
            }
            catch
            {
                if (isBizDay)
                {
                    KeyValue.Set("StaticDataRecycleIntervalBizDay",  "0:15", dbCn);
                    hours = 0;
                    minutes = 30;
                    timeSpan = new TimeSpan(hours, minutes, 0);
                    Log.Write("StaticDataRecycleIntervalBizDay reset to " + hours + " hours, " + minutes + " minutes. [StaticData.RecycleInterval]", Log.Error, 1);
                }
                else
                {
                    KeyValue.Set("StaticDataRecycleIntervalNonBizDay",  "4:00", dbCn);
                    hours = 6;
                    minutes = 0;
                    timeSpan = new TimeSpan(hours, minutes, 0);
                    Log.Write("StaticDataRecycleIntervalNonBizDay reset to " + hours + " hours, " + minutes + " minutes. [StaticData.RecycleInterval]", Log.Error, 1);
                }
            }

            Log.Write("StaticData will recycle in " + hours + " hours, " + minutes + " minutes. [StaticData.RecycleInterval]", 2);
            return timeSpan;
        }

		public static string TempPath
        {
            get
            {
                return tempPath;
            }
        }

        public static bool IsStopped
        {
            get
            {
                return isStopped;
            }
        }
    }
}

