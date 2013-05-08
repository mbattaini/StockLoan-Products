using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting;
using System.Threading;
using StockLoan.Common;
using StockLoan.MainBusiness;

namespace StockLoan.Main
{
  public class ServiceAgent : MarshalByRefObject, IService
  {  
    private int heartbeatInterval;
    private Thread heartbeatThread = null;

    private string dbCnStr;        

      public ServiceAgent(string dbCnStr)
      {
          this.dbCnStr = dbCnStr;
      }

      public void Start()
      {
      }

		public void Stop()
		{
	
			if (heartbeatThread == null)
			{
				Log.Write("Stop command issued, heartbeat thread never started. [ServiceAgent.Stop]", 3);
			}
			else if (heartbeatThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				Log.Write("Stop command issued, heartbeat thread is sleeping. [ServiceAgent.Stop]", 3);
			}
			else
			{
				Log.Write("Stop command issued, heartbeat thread is still active. [ServiceAgent.Stop]", 3);
			}
    	}

      public string BizDate()
      {
          return Master.BizDate;
      }

    public string BizDateNext()
    {
      return Master.BizDateNext;
    }

    public string BizDatePrior()
    {
      return Master.BizDatePrior;
    }
    
    public string BizDateBank()
    {
      return Master.BizDateBank;
    }

    public string BizDateNextBank()
    {
      return Master.BizDateNextBank;
    }

    public string BizDatePriorBank()
    {
      return Master.BizDatePriorBank;
    }
    
    public string BizDateExchange()
    {
      return Master.BizDateExchange;
    }

    public string BizDateNextExchange()
    {
      return Master.BizDateNextExchange;
    }

    public string BizDatePriorExchange()
    {
      return Master.BizDatePriorExchange;
    }

    public string ContractsBizDate()
    {
      return Master.ContractsBizDate;
    }

	  public string ProcessId()
	  {
		  return Standard.ProcessId();
	  }

	  public bool UseSystemSettlementEngine()
	  {
		  return Master.UseSystemSettlementEngine;
	  }

    public DataSet KeyValueGet()
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spKeyValueGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "KeyValues");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.KeyValueGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }
    
    public string KeyValueGet(string keyId, string keyValueDefault)
    {
      return KeyValue.Get(keyId, keyValueDefault, dbCnStr);
    }

    public void KeyValueSet(string keyId, string keyValue)
    {
      KeyValue.Set(keyId, keyValue, dbCnStr);
    }
    
    public string NewProcessId(string prefix)
    {
      return Standard.ProcessId(prefix);
    }

    public DataSet ProcessStatusGet(short utcOffset)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spProcessStatusGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);			
        paramBizDate.Value = Master.ContractsBizDate;      
        
        SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);			
        paramUtcOffset.Value = utcOffset;      

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "ProcessStatus");

        dataSet.Tables["ProcessStatus"].PrimaryKey = new DataColumn[3]
          { 
            dataSet.Tables["ProcessStatus"].Columns["ProcessId"],
            dataSet.Tables["ProcessStatus"].Columns["SystemCode"],
            dataSet.Tables["ProcessStatus"].Columns["ActCode"] };

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.ProcessStatusGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }
    
    public DataSet FirmGet()
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spFirmGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);        
        dataAdapter.Fill(dataSet, "Firms");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.FirmGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }

    public DataSet CurrenciesGet()
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spCurrenciesGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "Currencies");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.CurrenciesGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }

	  public void CountrySet(string countryCode, string country, string settleDays, bool isActive)
	  {
		  SqlConnection dbCn = new SqlConnection(dbCnStr);

		  try
		  {
			  SqlCommand dbCmd = new SqlCommand("spCountrySet", dbCn);
			  dbCmd.CommandType = CommandType.StoredProcedure;

			  SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 4);
			  paramCountryCode.Value = countryCode;

			  SqlParameter paramCountry = dbCmd.Parameters.Add("@Country", SqlDbType.VarChar, 50);
			  paramCountry.Value = country;

			  SqlParameter paramSettleDays = dbCmd.Parameters.Add("@SettleDays", SqlDbType.BigInt);
			  paramSettleDays.Value = settleDays;

			  SqlParameter paramIsActive = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
			  paramIsActive.Value = isActive;

			  dbCn.Open();
			  dbCmd.ExecuteNonQuery();
		  }
		  catch (Exception e)
		  {
			  Log.Write(e.Message + " [ServiceAgent.CountrySet]", Log.Error, 1);
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

    public DataSet CountriesGet(string countryCode)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spCountriesGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

		if (!countryCode.Equals(""))
		{
			SqlParameter paramCountryCode = dbCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 4);
			paramCountryCode.Value = countryCode;
		}

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "Countries");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.CountriesGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }
    
    public DataSet DeskTypeGet()
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spDeskTypeGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);        
        dataAdapter.Fill(dataSet, "DeskTypes");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.DeskTypeGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }

    public DataSet DeskGet()
    {
      return DeskGet("", false);
    }
    
    public DataSet DeskGet(string desk)
    {
      return DeskGet(desk, false);
    }
    
    public DataSet DeskGet(bool isNotSubscriber)
    {
      return DeskGet("", isNotSubscriber);
    }

    private DataSet DeskGet(string desk, bool isNotSubscriber)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();
      
      try
      {
        SqlCommand dbCmd = new SqlCommand("spDeskGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        
        if(!desk.Equals(""))
        {
          SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);
          paramDesk.Value = desk;
        }

        SqlParameter paramNoSubscription = dbCmd.Parameters.Add("@IsNotSubscriber", SqlDbType.Bit);
        paramNoSubscription.Value = isNotSubscriber;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);        
        dataAdapter.Fill(dataSet, "Desks");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.DeskGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }

    public DataSet BookGroupGet()
    {
      return BookGroupGet(null, null);
    }
    
    public DataSet BookGroupGet(string userId, string functionPath)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spBookGroupGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
        paramBizDate.Value = Master.BizDate;

        if ((userId != null) && (functionPath != null))
        {
          SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
          paramUserId.Value = userId;
          
          SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
          paramFunctionPath.Value = functionPath;
        }

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "BookGroups");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.BookGroupGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }  

    public DataSet SecMasterSearch(string lookupciteria)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      Log.Write("Doing a security data search for " + lookupciteria + ". [ServiceAgent.SecMasterLookup]", 3);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spSecMasterSearchGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramLookUp = dbCmd.Parameters.Add("@LookUpCiteria", SqlDbType.VarChar, 50);
        paramLookUp.Value = lookupciteria;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "SecMasterResults");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch (Exception e)
      {
		  Log.Write(e.Message + " [ServiceAgent.SecMasterSearch]", Log.Error, 1);
      }

      return dataSet;
    }

	  public DataSet SecMasterGroupGet(string secId)
	  {
		  SqlConnection dbCn = new SqlConnection(dbCnStr);
		  DataSet dataSet = new DataSet();

		  Log.Write("Doing a security data group lookup on " + secId + ". [ServiceAgent.SecMasterGroupGet]", 3);

		  try
		  {
			  SqlCommand dbCmd = new SqlCommand("spSecMasterGroupGet", dbCn);
			  dbCmd.CommandType = CommandType.StoredProcedure;

			  SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
			  paramSecId.Value = secId;

			  SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
			  dataAdapter.Fill(dataSet, "SecMasterGroup");

              dataSet.RemotingFormat = SerializationFormat.Binary;
		  }
		  catch (Exception e)
		  {
			  Log.Write(e.Message + " [ServiceAgent.SecMasterLookup]", Log.Error, 1);
		  }

		  return dataSet;
	  }

    public DataSet SecMasterLookup(string secId)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      Log.Write("Doing a security data lookup on " + secId + ". [ServiceAgent.SecMasterLookup]", 3);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spSecMasterItemGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
        paramSecId.Value = secId;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "SecMasterItem");

        dataSet.RemotingFormat = SerializationFormat.Binary;
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.SecMasterLookup]", Log.Error, 1);
      }

      return dataSet;
    }

	  public DataSet SecMasterGet()
	  {
		  SqlConnection dbCn = new SqlConnection(dbCnStr);
		  DataSet dataSet = new DataSet();

		  try
		  {
			  SqlCommand dbCmd = new SqlCommand("spSecMasterGet", dbCn);
			  dbCmd.CommandType = CommandType.StoredProcedure;

			  SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
			  dataAdapter.Fill(dataSet, "SecMaster");

			  dataSet.RemotingFormat = SerializationFormat.Binary;
		  }
		  catch (Exception e)
		  {
			  Log.Write(e.Message + " [ServiceAgent.SecMasterGet]", Log.Error, 1);
		  }

		  return dataSet;
	  }


      public void SecMasterItemSet(string secId, string description, string baseType, string classGroup, string countryCode, string currencyIso, string accruedInterest, string recordDateCash, string dividendRate, string secIdGroup, string symbol, string isin, string cusip, string price)
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

              if (!price.Equals(""))
              {
                  SqlParameter paramPrice = dbCmd.Parameters.Add("@Price", SqlDbType.Decimal);
                  paramPrice.Value = float.Parse(price).ToString(Formats.Price);
              }

              if (!price.Equals(""))
              {
                  SqlParameter paramPriceDate = dbCmd.Parameters.Add("@PriceDate", SqlDbType.DateTime);
                  paramPriceDate.Value = Master.BizDate;
              }

              localDbCn.Open();
              dbCmd.ExecuteNonQuery();
          }
          catch (Exception error)
          {
              Log.Write(error.Message + " [ServiceAgent.SecMasterItemSet]", Log.Error, 1);
          }
          finally
          {
              if (localDbCn.State != ConnectionState.Closed)
              {
                  localDbCn.Close();
              }
          }
      }

      private void PriceSet(string secId, string currencyIso, decimal price, string priceDate)
      {
          SqlConnection localDbCn = new SqlConnection(dbCnStr);

          try
          {
              SqlCommand dbCmd = new SqlCommand("spPriceSet", localDbCn);
              dbCmd.CommandType = CommandType.StoredProcedure;

              SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
              paramSecId.Value = secId;

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
    
    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
}
