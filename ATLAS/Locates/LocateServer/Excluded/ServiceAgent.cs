
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting;
using System.Threading;
using StockLoan.Common;

namespace StockLoan.Locates
{
  public class ServiceAgent : MarshalByRefObject, IService
  {  
    private int heartbeatInterval;
    private Thread heartbeatThread = null;

    private string dbCnStr;        

      public ServiceAgent(string dbCnStr)
      {
          this.dbCnStr = dbCnStr;

          try
          {
              heartbeatInterval = int.Parse(Standard.ConfigValue("HeartbeatInterval", "20000"));
          }
          catch (Exception e)
          {
              Log.Write(e.Message + " [ServiceAgent.ServiceAgent]", Log.Error, 1);
          }
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
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.KeyValueGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }
    
    public string KeyValueGet(string keyId, string keyIdCategory, string keyValueDefault)
    {
      return KeyValue.Get(keyId, keyIdCategory, keyValueDefault, dbCnStr);
    }

    public void KeyValueSet(string keyId, string keyidCategory, string keyValue)
    {
      KeyValue.Set(keyId, keyidCategory, keyValue, dbCnStr);
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
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.FirmGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }
    
    public DataSet CountryGet()
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spCountryGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "Countries");
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.CountryGet]", Log.Error, 1);
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

        if ((userId != null) && (functionPath != null))
        {
          SqlParameter paramUserId = dbCmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50);
          paramUserId.Value = userId;
          
          SqlParameter paramFunctionPath = dbCmd.Parameters.Add("@FunctionPath", SqlDbType.VarChar, 50);
          paramFunctionPath.Value = functionPath;
        }

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
        dataAdapter.Fill(dataSet, "BookGroups");
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.BookGroupGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }
    
    public DataSet SecMasterLookup(string secId)
    {
      return SecMasterLookup(secId, false, false, 0, "");
    }
    
    public DataSet SecMasterLookup(string secId, bool withBox)
    {
      return SecMasterLookup(secId, withBox, false, 0, "");
    }
    
    public DataSet SecMasterLookup(string secId, bool withBox, bool withDeskQuips, short utcOffset, string since)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      Log.Write("Doing a security data lookup on " + secId + ", withBox = " + withBox.ToString() + 
        ", withDeskQuips = " + withDeskQuips.ToString() + ", utcOffset = " + utcOffset + 
        ", since = " + since + ". [ServiceAgent.SecMasterLookup]", 3);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spSecMasterItemGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
        paramSecId.Value = secId;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);        
        dataAdapter.Fill(dataSet, "SecMasterItem");

        if (withDeskQuips)
        {
          dbCmd.Parameters.Remove(paramSecId);

          SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);			
          paramUtcOffset.Value = utcOffset;
      
          SqlParameter paramSince = dbCmd.Parameters.Add("@Since", SqlDbType.DateTime);			
          paramSince.Value = since;
      
          dbCmd.CommandText = "spDeskQuipGet";
          dataAdapter.Fill(dataSet, "DeskQuips"); 
        }
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.SecMasterLookup]", Log.Error, 1);
      }

      return dataSet;
    }

        
    
    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
}
