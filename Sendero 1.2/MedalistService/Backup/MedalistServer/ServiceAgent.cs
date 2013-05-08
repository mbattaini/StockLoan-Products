// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting;
using System.Threading;
using Anetics.Common;
using Anetics.Process;
using Anetics.SmartSeg;

namespace Anetics.Medalist
{
  public class ServiceAgent : MarshalByRefObject, IService
  {
    public event HeartbeatEventHandler HeartbeatEvent;
    public event DeskQuipEventHandler DeskQuipEvent;
  
    private int heartbeatInterval;
    private Thread heartbeatThread = null;

		private int smartSegHeartbeatInterval;
		private Thread smartSegHeartbeatThread = null;

    private string dbCnStr;        

    private IProcess processAgent = null;
		private ISmartSeg smartSegAgent = null;

		private PositionAgent positionAgent;
		private SubstitutionAgent substitutionAgent;

    public ServiceAgent(string dbCnStr, ref IProcess processAgent, ref PositionAgent positionAgent, ref ISmartSeg smartSegAgent, ref SubstitutionAgent substitutionAgent)
    {
      this.dbCnStr = dbCnStr;
      this.processAgent = processAgent;
      this.positionAgent = positionAgent;
			this.substitutionAgent = substitutionAgent;
			this.smartSegAgent = smartSegAgent;

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
      if (processAgent == null)
      {
        Log.Write("The process agent has not been enabled for this session. [ServiceAgent.Start]", 2);
        return;
      }
			
      positionAgent.ProcessAgentConnect();
			
			if (smartSegAgent == null)
			{
				Log.Write("The smartseg agent has not been enabled for this session. [ServiceAgent.Start]", 2);
				return;
			}
			
			substitutionAgent.SubstitutionAgentConnect();
		

      if ((heartbeatThread == null) || (!heartbeatThread.IsAlive)) // Must create new thread.
      {
        heartbeatThread = new Thread(new ThreadStart(Heartbeat));
        heartbeatThread.Name = "Heartbeat";
        heartbeatThread.Start();

        Log.Write("Start command issued with new heartbeat thread. [ServiceAgent.Start]", 3);
      }
      else // Old thread will be just fine.
      {
        Log.Write("Start command issued with heartbeat thread already running. [ServiceAgent.Start]", 3);
      }

			if ((smartSegHeartbeatThread == null) || (!smartSegHeartbeatThread.IsAlive)) // Must create new thread.
			{
				smartSegHeartbeatThread = new Thread(new ThreadStart(SmartSegHeartbeat));
				smartSegHeartbeatThread.Name = "SmartSegHeartbeat";
				smartSegHeartbeatThread.Start();

				Log.Write("Start command issued with new smartseg heartbeat thread. [ServiceAgent.Start]", 3);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Start command issued with heartbeat thread already running. [ServiceAgent.Start]", 3);
			}
    }

		public void Stop()
		{
			if (processAgent == null)
			{
				Log.Write("The process agent has not been enabled for this session. [ServiceAgent.Stop]", 2);
				return;
			}
			
			if (smartSegAgent == null)
			{
				Log.Write("The smartseg agent has not been enabled for this session. [ServiceAgent.Stop]", 2);
				return;
			}

			substitutionAgent.SubstitutionAgentDisconnect();
			positionAgent.ProcessAgentDisconnect();

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
      
			HeartbeatEventInvoke(new HeartbeatEventArgs(HeartbeatStatus.Stopping, "", HeartbeatStatus.Unknown, ""));			
		}

    private void Heartbeat()
    {
      while (!MedalistMain.IsStopped) // Loop through this block.
      {
        switch (positionAgent.Status)
        {
          case Anetics.Process.HeartbeatStatus.Unknown :
            HeartbeatEventInvoke(new HeartbeatEventArgs(HeartbeatStatus.Normal, "", HeartbeatStatus.Unknown, ""));
            positionAgent.ProcessAgentConnect();
            break;
          case Anetics.Process.HeartbeatStatus.Stopping :
            HeartbeatEventInvoke(new HeartbeatEventArgs(HeartbeatStatus.Normal, "", HeartbeatStatus.Stopping, ""));
            break;
          case Anetics.Process.HeartbeatStatus.Alert :
            HeartbeatEventInvoke(new HeartbeatEventArgs(HeartbeatStatus.Normal, "", HeartbeatStatus.Alert, positionAgent.Alert));
            break;
          default :
            HeartbeatEventInvoke(new HeartbeatEventArgs(HeartbeatStatus.Normal, "", HeartbeatStatus.Normal, ""));
            break;
        }

        if (!MedalistMain.IsStopped)
        {
          Thread.Sleep(heartbeatInterval);
        }
      }
    }

		private void SmartSegHeartbeat()
		{
			while (!MedalistMain.IsStopped) // Loop through this block.
			{
				switch (substitutionAgent.Status)
				{
					case Anetics.SmartSeg.HeartbeatStatus.Unknown :
						HeartbeatEventInvoke(new HeartbeatEventArgs(HeartbeatStatus.Normal, "", HeartbeatStatus.Unknown, ""));						
						substitutionAgent.SubstitutionAgentConnect();
						break;
					case Anetics.SmartSeg.HeartbeatStatus.Stopping :
						HeartbeatEventInvoke(new HeartbeatEventArgs(HeartbeatStatus.Normal, "", HeartbeatStatus.Stopping, ""));
						break;
					case Anetics.SmartSeg.HeartbeatStatus.Alert :
						HeartbeatEventInvoke(new HeartbeatEventArgs(HeartbeatStatus.Normal, "", HeartbeatStatus.Alert, substitutionAgent.Alert));
						break;
					default :
						HeartbeatEventInvoke(new HeartbeatEventArgs(HeartbeatStatus.Normal, "", HeartbeatStatus.Normal, ""));
						break;
				}

				if (!MedalistMain.IsStopped)
				{
					Thread.Sleep(heartbeatInterval);
				}
			}
		}


    private void HeartbeatEventInvoke(HeartbeatEventArgs heartbeatEventArgs)
    {
      HeartbeatEventHandler heartbeatEventHandler = null;

      Log.Write(heartbeatEventArgs.MainStatus.ToString() + ": " + heartbeatEventArgs.MainAlert + " " + 
        heartbeatEventArgs.ProcessStatus.ToString() + ": " + heartbeatEventArgs.ProcessAlert + " [ServiceAgent.HeartbeatEventInvoke]", 3);

      try
      {
        if (HeartbeatEvent == null)
        {
          Log.Write("There are no HeartbeatEvent delegates. [ServiceAgent.HeartbeatEventInvoke]", 3);
        }
        else
        {
          int n = 0;

          Delegate[] eventDelegates = HeartbeatEvent.GetInvocationList();
          Log.Write("HeartbeatEvent has " + eventDelegates.Length + " delegate[s]. [ServiceAgent.HeartbeatEventInvoke]", 3);

          foreach (Delegate eventDelegate in eventDelegates)
          {
            Log.Write("Invoking HeartbeatEvent delegate [" + (++n) + "]. [ServiceAgent.HeartbeatEventInvoke]", 3);
            
            try
            {
              heartbeatEventHandler = (HeartbeatEventHandler)eventDelegate;
              heartbeatEventHandler(heartbeatEventArgs);
            }
            catch (System.Net.Sockets.SocketException)
            {
              HeartbeatEvent -= heartbeatEventHandler;
              Log.Write("HeartbeatEvent delegate [" + n + "] has been removed from the invocation list. [ServiceAgent.HeartbeatEventInvoke]", 3);
            }
            catch (Exception e)
            {
              Log.Write(e.Message + " [ServiceAgent.HeartbeatEventInvoke]", Log.Error, 1);
            }
          }
        }
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.HeartbeatEventInvoke]", Log.Error, 1);
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

		public bool IsSubstitutionActive()
		{
			return Master.IsSubstitutionActive;
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

        if (withBox)
        {
          if (dataSet.Tables["SecMasterItem"].Rows.Count.Equals(1)) // Switch to the resolved SecId.
          {
            paramSecId.Value = dataSet.Tables["SecMasterItem"].Select()[0]["SecId"];
          }

          dbCmd.CommandText = "spBoxLocationGet";
          dataAdapter.Fill(dataSet, "BoxLocation");
 
          SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
          paramBizDate.Value = Master.BizDate;
      
          dbCmd.CommandText = "spBoxPositionGet";
          dataAdapter.Fill(dataSet, "BoxPosition");

          dbCmd.Parameters.Remove(paramBizDate);

          dataSet.ExtendedProperties.Add("LoadDateTime",
            KeyValue.Get("BoxPositionLoadDateTime", "0001-01-01 00:00:00", dbCn));
        }

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

    public DataSet DeskQuipGet(short utcOffset)
    {
      return DeskQuipGet(utcOffset, "");
    }
    
    public DataSet DeskQuipGet(short utcOffset, string secId)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      Log.Write("Returning desk quips for parameters: " + utcOffset + "|" + secId + ". [ServiceAgent.DeskQuipGet]", 3);

      try
      {
        SqlCommand dbCmd = new SqlCommand("spDeskQuipGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        if (!secId.Equals(""))
        {
          SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);			
          paramSecId.Value = secId;
        }
      
        SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
        paramUtcOffset.Value = utcOffset;
        
        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);        
        dataAdapter.Fill(dataSet, "DeskQuips");
      }
      catch (Exception e)
      {
        Log.Write(e.Message + ". [ServiceAgent.DeskQuipGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }

    public void DeskQuipSet(string secId, string deskQuip, string actUserId)
    {
      Log.Write("Setting new desk quip: " + secId + "|" + actUserId + "|" + deskQuip + " [ServiceAgent.DeskQuipSet]", 3);

      SqlConnection dbCn = new SqlConnection(dbCnStr);
      SqlCommand dbCmd = new SqlCommand("spDeskQuipSet", dbCn);
      dbCmd.CommandType = CommandType.StoredProcedure;

      SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);			
      paramSecId.Value = secId;
      
      SqlParameter paramDeskQuip = dbCmd.Parameters.Add("@DeskQuip", SqlDbType.VarChar, 50);			
      paramDeskQuip.Value = deskQuip;
      
      SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);			
      paramActUserId.Value = actUserId;
      
      SqlParameter paramActTime = dbCmd.Parameters.Add("@ActTime", SqlDbType.DateTime);			
      paramActTime.Direction = ParameterDirection.Output;
      
      SqlParameter paramSymbol = dbCmd.Parameters.Add("@Symbol", SqlDbType.VarChar, 12);			
      paramSymbol.Direction = ParameterDirection.Output;
      
      SqlParameter paramActUserShortName = dbCmd.Parameters.Add("@ActUserShortName", SqlDbType.VarChar, 15);			
      paramActUserShortName.Direction = ParameterDirection.Output;
      
      try
      {        
        dbCn.Open();
        dbCmd.ExecuteNonQuery();

        DeskQuipEventArgs deskQuipEventArgs = new DeskQuipEventArgs(
          secId,
          paramSymbol.Value.ToString(), 
          deskQuip,
          paramActUserShortName.Value.ToString(),
          Tools.FormatDate(paramActTime.Value.ToString(), Standard.DateTimeFormat));
        
        DeskQuipEventHandler deskQuipEventHandler = new DeskQuipEventHandler(DeskQuipEventInvoke);
        deskQuipEventHandler.BeginInvoke(deskQuipEventArgs, null, null);
      }
      catch(Exception e) 
      {
        Log.Write(e.Message + " [ServiceAgent.DeskQuipSet]", Log.Error, 1);
      }
      finally
      {
        if (dbCn.State != ConnectionState.Closed)
        {
          dbCn.Close();
        }
      }
    }

    private void DeskQuipEventInvoke(DeskQuipEventArgs deskQuipEventArgs)
    {
      DeskQuipEventHandler deskQuipEventHandler = null;

      Log.Write(deskQuipEventArgs.ActUserShortName + ": " + deskQuipEventArgs.SecId + " [ServiceAgent.DeskQuipEventInvoke]", 3);

      try
      {
        if (DeskQuipEvent == null)
        {
          Log.Write("There are no DeskQuipEvent delegates. [ServiceAgent.DeskQuipEventInvoke]", 3);
        }
        else
        {
          int n = 0;

          Delegate[] eventDelegates = DeskQuipEvent.GetInvocationList();
          Log.Write("DeskQuipEvent has " + eventDelegates.Length + " delegate[s]. [ServiceAgent.DeskQuipEventInvoke]", 3);
        
          foreach (Delegate eventDelegate in eventDelegates)
          {
            Log.Write("Invoking DeskQuipEvent delegate [" + (++n) + "]. [ServiceAgent.DeskQuipEventInvoke]", 3);

            try
            {
              deskQuipEventHandler = (DeskQuipEventHandler) eventDelegate;
              deskQuipEventHandler(deskQuipEventArgs);
            }
            catch (System.Net.Sockets.SocketException)
            {
              DeskQuipEvent -= deskQuipEventHandler;
              Log.Write("DeskQuipEvent delegate [" + n + "] has been removed from the invocation list. [ServiceAgent.DeskQuipEventInvoke]", 3);
            }
            catch (Exception e)
            {
              Log.Write(e.Message + " [ServiceAgent.DeskQuipEventInvoke]", Log.Error, 1);
            }
          }
        }
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.DeskQuipEventInvoke]", Log.Error, 1);
      }
    }

    public DataSet InventoryDataMaskGet(string desk)
    {
      SqlConnection dbCn =new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();
    
      try
      {
        SqlCommand sqlDbCmd = new SqlCommand("spInventoryFileDataMaskList", dbCn);
        sqlDbCmd.CommandType = CommandType.StoredProcedure;
        
        SqlParameter paramDesk = sqlDbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);			
        paramDesk.Value = desk;

        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlDbCmd);
        dataAdapter.Fill(dataSet, "InventoryDataMasks");
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.InventoryDataMaskGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }

    public void InventoryDataMaskSet(
      string  desk, 
      short   recordLength, 
      char    headerFlag, 
      char    dataFlag, 
      char    trailerFlag, 
      short   accountLocale, 
      char    delimiter, 
      short   accountOrdinal,
      short   secIdOrdinal, 
      short   quantityOrdinal, 
      short   recordCountOrdinal, 
      short   accountPosition, 
      short   accountLength, 
      short   bizDateDD, 
      short   bizDateMM, 
      short   bizDateYY, 
      short   secIdPosition, 
      short   secIdLength, 
      short   quantityPosition, 
      short   quantityLength, 
      short   recordCountPosition, 
      short   recordCountLength,
      string  actUserId)
     {
      SqlConnection dbCn =new SqlConnection(dbCnStr);
      SqlCommand sqlDbCmd = null;

      try
      {
        sqlDbCmd = new SqlCommand("spInventoryFileDataMaskSet", dbCn);
        sqlDbCmd.CommandType = CommandType.StoredProcedure;
        
        SqlParameter paramDesk = sqlDbCmd.Parameters.Add("@Desk", SqlDbType.VarChar, 12);			
        paramDesk.Value = desk;
       
        SqlParameter paramRecordLength = sqlDbCmd.Parameters.Add("@RecordLength", SqlDbType.SmallInt);			
        paramRecordLength.Value = recordLength;
      
        SqlParameter paramHeaderFlag = sqlDbCmd.Parameters.Add("@HeaderFlag", SqlDbType.Char, 1);			
        paramHeaderFlag.Value = headerFlag;
      
        SqlParameter paramDataFlag = sqlDbCmd.Parameters.Add("@DataFlag", SqlDbType.Char, 1);			
        paramDataFlag.Value = dataFlag;
      
        SqlParameter paramTrailerFlag = sqlDbCmd.Parameters.Add("@TrailerFlag", SqlDbType.Char, 1);			
        paramTrailerFlag.Value = trailerFlag;
      
        SqlParameter paramAccountLocale = sqlDbCmd.Parameters.Add("@AccountLocale", SqlDbType.SmallInt, 1);			
        paramAccountLocale.Value = accountLocale;

        SqlParameter paramDelimiter = sqlDbCmd.Parameters.Add("@Delimiter", SqlDbType.Char, 1);			
        paramDelimiter.Value = delimiter;
      
        SqlParameter paramAccountOrdinal = sqlDbCmd.Parameters.Add("@AccountOrdinal", SqlDbType.SmallInt, 1);			
        paramAccountOrdinal.Value = accountOrdinal;

        SqlParameter paramSecIdOrdinal = sqlDbCmd.Parameters.Add("@SecIdOrdinal", SqlDbType.SmallInt);			
        paramSecIdOrdinal.Value = secIdOrdinal;
      
        SqlParameter paramQuantityOrdinal = sqlDbCmd.Parameters.Add("@QuantityOrdinal", SqlDbType.SmallInt);			
        paramQuantityOrdinal.Value = quantityOrdinal;
      
        SqlParameter paramRecordCountOrdinal = sqlDbCmd.Parameters.Add("@RecordCountOrdinal", SqlDbType.SmallInt);			
        paramRecordCountOrdinal.Value = recordCountOrdinal;
      
        SqlParameter paramAccountPosition = sqlDbCmd.Parameters.Add("@AccountPosition", SqlDbType.SmallInt);			
        paramAccountPosition.Value = accountPosition;
      
        SqlParameter paramAccountLength = sqlDbCmd.Parameters.Add("@AccountLength", SqlDbType.SmallInt);			
        paramAccountLength.Value = accountLength;
      
        SqlParameter paramBizDateDD = sqlDbCmd.Parameters.Add("@BizDateDD", SqlDbType.SmallInt);			
        paramBizDateDD.Value = bizDateDD;
      
        SqlParameter paramBizDateMM = sqlDbCmd.Parameters.Add("@BizDateMM", SqlDbType.SmallInt);			
        paramBizDateMM.Value = bizDateMM;
      
        SqlParameter paramBizDateYY = sqlDbCmd.Parameters.Add("@BizDateYY", SqlDbType.SmallInt);			
        paramBizDateYY.Value = bizDateYY;
      
        SqlParameter paramSecIdPosition = sqlDbCmd.Parameters.Add("@SecIdPosition", SqlDbType.SmallInt);			
        paramSecIdPosition.Value = secIdPosition;
      
        SqlParameter paramSecIdLength = sqlDbCmd.Parameters.Add("@SecIdLength", SqlDbType.SmallInt);			
        paramSecIdLength.Value = secIdLength;
      
        SqlParameter paramQuantityPosition = sqlDbCmd.Parameters.Add("@QuantityPosition", SqlDbType.SmallInt);			
        paramQuantityPosition.Value = quantityPosition;
      
        SqlParameter paramQuantityLength = sqlDbCmd.Parameters.Add("@QuantityLength", SqlDbType.SmallInt);			
        paramQuantityLength.Value = quantityLength;
      
        SqlParameter paramRecordCountPosition= sqlDbCmd.Parameters.Add("@RecordCountPosition", SqlDbType.SmallInt);			
        paramRecordCountPosition.Value = recordCountPosition;
      
        SqlParameter paramRecordCountLength = sqlDbCmd.Parameters.Add("@RecordCountLength", SqlDbType.SmallInt);			
        paramRecordCountLength.Value = recordCountLength;

        SqlParameter paramActUserId = sqlDbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 25);
        paramActUserId.Value = actUserId;
        
        dbCn.Open();
        sqlDbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.InventoryDataMaskSet]", Log.Error, 1);
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
    
    public DataSet SubscriberListGet(short utcOffset)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spInventorySubscriberGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
        paramUtcOffset.Value = utcOffset;
        
        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);        
        dataAdapter.Fill(dataSet, "SubscriberList");
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.SubscriberListGet]", Log.Error, 1);
      }

      return dataSet;
    }

    public void SubscriberListSet(
      string desk, 
      string ftpPath, 
      string ftpHost,
      string ftpUserName,
      string ftpPassword,
      string loadExPGP, 
      string comment, 
      string mailAddress, 
      string mailSubject, 
      string isActive, 
      string usePGP,
			bool	 isBizDatePrior,
      string actUserId)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      
      try
      {
        SqlCommand dbCmd = new SqlCommand("spInventorySubscriberSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
          
        SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar,(12));
        paramDesk.Value = desk;

        SqlParameter paramFtpPath = dbCmd.Parameters.Add("@FilePathName", SqlDbType.VarChar,(100));
        paramFtpPath.Value = ftpPath;
        
        SqlParameter paramFtpHost = dbCmd.Parameters.Add("@FileHost", SqlDbType.VarChar,(50));
        paramFtpHost.Value = ftpHost;

        SqlParameter paramFtpUserName = dbCmd.Parameters.Add("@FileUserName", SqlDbType.VarChar,(50));
        paramFtpUserName.Value = ftpUserName;

        SqlParameter paramFtpPassword = dbCmd.Parameters.Add("@FilePassword", SqlDbType.VarChar,(25));
        paramFtpPassword.Value = ftpPassword;

        SqlParameter paramLoadExPGP = dbCmd.Parameters.Add("@LoadExtensionPgp", SqlDbType.VarChar,(50));
        paramLoadExPGP.Value = loadExPGP;

				SqlParameter paramIsBizDatePrior = dbCmd.Parameters.Add("@IsBizDatePrior", SqlDbType.Bit);
				paramIsBizDatePrior.Value = isBizDatePrior;
				
				SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar,(50));
        paramComment.Value = comment;
        
        SqlParameter paramMailAddress = dbCmd.Parameters.Add("@MailAddress", SqlDbType.VarChar,(50));
        paramMailAddress.Value = mailAddress;

        SqlParameter paramMailSubject = dbCmd.Parameters.Add("@MailSubject", SqlDbType.VarChar,(25));
        paramMailSubject.Value = mailSubject;

        SqlParameter paramIsActive  = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
        paramIsActive.Value = isActive;

        SqlParameter paramUsePGP  = dbCmd.Parameters.Add("@UsePgp", SqlDbType.Bit);
        paramUsePGP.Value = usePGP;

        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actUserId;

        if (dbCn.State != ConnectionState.Open)
        {
          dbCn.Open();
        }

        dbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.SubscriberListSet]", Log.Error, 1);
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

    public DataSet PublisherListGet(short utcOffset)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      DataSet dataSet = new DataSet();

      try
      {
        SqlCommand dbCmd = new SqlCommand("spInventoryPublisherGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;

        SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
        paramUtcOffset.Value = utcOffset;
        
        SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);        
        dataAdapter.Fill(dataSet, "PublisherList");
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.PublisherListGet]", Log.Error, 1);
      }

      return dataSet;
    }

    public void PublisherListSet(
      string desk, 
      string ftpPath, 
      string ftpHost,
      string ftpUserName,
      string ftpPassword,
      string loadExPGP, 
      string comment, 
      string mailAddress, 
      string mailSubject, 
      string isActive, 
      string usePGP,
			string	reportName,
			string reportFrequency,
			string reportWaitUntil,
      string actUserId)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      
      try
      {
        SqlCommand dbCmd = new SqlCommand("spInventoryPublisherSet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;
          
        SqlParameter paramDesk = dbCmd.Parameters.Add("@Desk", SqlDbType.VarChar,(12));
        paramDesk.Value = desk;

				SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar,50);
				paramReportName.Value = reportName;

				SqlParameter paramReportFrequency = dbCmd.Parameters.Add("@ReportFrequency", SqlDbType.VarChar, 10);
				paramReportFrequency.Value = reportFrequency;

				SqlParameter paramReportWaitUntil = dbCmd.Parameters.Add("@ReportWaitUntil", SqlDbType.VarChar, 5);
				paramReportWaitUntil.Value = reportWaitUntil;

        SqlParameter paramFtpPath = dbCmd.Parameters.Add("@FilePathName", SqlDbType.VarChar,(100));
        paramFtpPath.Value = ftpPath;
        
        SqlParameter paramFtpHost = dbCmd.Parameters.Add("@FileHost", SqlDbType.VarChar,(50));
        paramFtpHost.Value = ftpHost;

        SqlParameter paramFtpUserName = dbCmd.Parameters.Add("@FileUserName", SqlDbType.VarChar,(50));
        paramFtpUserName.Value = ftpUserName;

        SqlParameter paramFtpPassword = dbCmd.Parameters.Add("@FilePassword", SqlDbType.VarChar,(25));
        paramFtpPassword.Value = ftpPassword;

        SqlParameter paramLoadExPGP = dbCmd.Parameters.Add("@LoadExtensionPgp", SqlDbType.VarChar,(50));
        paramLoadExPGP.Value = loadExPGP;

        SqlParameter paramComment = dbCmd.Parameters.Add("@Comment", SqlDbType.VarChar,(50));
        paramComment.Value = comment;
        
        SqlParameter paramMailAddress = dbCmd.Parameters.Add("@MailAddress", SqlDbType.VarChar,(50));
        paramMailAddress.Value = mailAddress;

        SqlParameter paramMailSubject = dbCmd.Parameters.Add("@MailSubject", SqlDbType.VarChar,(25));
        paramMailSubject.Value = mailSubject;

        SqlParameter paramIsActive  = dbCmd.Parameters.Add("@IsActive", SqlDbType.Bit);
        paramIsActive.Value = isActive;

        SqlParameter paramUsePGP  = dbCmd.Parameters.Add("@UsePgp", SqlDbType.Bit);
        paramUsePGP.Value = usePGP;

        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
        paramActUserId.Value = actUserId;

        if (dbCn.State != ConnectionState.Open)
        {
          dbCn.Open();
        }

        dbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.PublisherListSet]", Log.Error, 1);
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

		public DataSet PublisherReportsGet(string reportName)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryPublisherReportGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				if (!reportName.Equals(""))
				{
					SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50);
					paramReportName.Value = reportName;
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);        
				dataAdapter.Fill(dataSet, "Reports");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ServiceAgent.PublisherReportsGet]", Log.Error, 1);
			}

			return dataSet;
		}

		public void PublisherReportSet (
			string reportName,
			string reportStoredProc,
			string reportDescription
			)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
      
			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventoryPublisherReportSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
          
				SqlParameter paramReportName = dbCmd.Parameters.Add("@ReportName", SqlDbType.VarChar,50);
				paramReportName.Value = reportName;

				if (!reportStoredProc.Equals(""))
				{
					SqlParameter paramReportStoredProc = dbCmd.Parameters.Add("@ReportStoredProc", SqlDbType.VarChar, 100);
					paramReportStoredProc.Value = reportStoredProc;
				}

				if (!reportDescription.Equals(""))
				{
					SqlParameter paramReportDescription = dbCmd.Parameters.Add("@ReportDescription", SqlDbType.VarChar, 100);
					paramReportDescription.Value = reportDescription;
				}

				if (dbCn.State != ConnectionState.Open)
				{
					dbCn.Open();
				}

				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ServiceAgent.PublisherReportSet]", Log.Error, 1);
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

    public void BorrowHardSet(string secId, string actUserId, bool delete)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      SqlCommand dbCmd = new SqlCommand("spBorrowHardSet", dbCn);
      dbCmd.CommandType = CommandType.StoredProcedure;

      try
      {
        SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar,12);			
        paramSecId.Value = secId;
      
        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar,50);			
        paramActUserId.Value = actUserId;
      
        SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);			
        paramDelete.Value = delete;

        if (dbCn.State != ConnectionState.Open)
        {
          dbCn.Open();
        }

        dbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ShortSaleAgent.BororowHardSet]", Log.Error, 1);
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
    
    public void BorrowNoSet(string secId, string actUserId, bool delete)
    {
      SqlConnection dbCn = new SqlConnection(dbCnStr);
      SqlCommand dbCmd = new SqlCommand("spBorrowNoSet", dbCn);
      dbCmd.CommandType = CommandType.StoredProcedure;

      try
      {
        SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);			
        paramSecId.Value = secId;
      
        SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);			
        paramActUserId.Value = actUserId;
      
        SqlParameter paramDelete = dbCmd.Parameters.Add("@Delete", SqlDbType.Bit);			
        paramDelete.Value = delete;

        if (dbCn.State != ConnectionState.Open)
        {
          dbCn.Open();
        }

        dbCmd.ExecuteNonQuery();
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ShortSaleAgent.BororowNoSet]", Log.Error, 1);
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
    
    public DataSet InventoryDeskInputDataGet()
    {
      SqlConnection dbCn = null;
      SqlCommand dbCmd = null;

      SqlDataAdapter dataAdapter;
      DataSet dataSet = new DataSet();

      try
      {
        dbCn = new SqlConnection(dbCnStr);
        dbCmd = new SqlCommand("spDeskGet", dbCn);
        dbCmd.CommandType = CommandType.StoredProcedure;                
        dataAdapter = new SqlDataAdapter(dbCmd);        
        dataAdapter.Fill(dataSet, "Desks");
  
        dbCmd.CommandText = "spBooksGet";                
        dataAdapter.Fill(dataSet, "Books");

        dbCmd.CommandText = "spBookGroupGet";                                
        dataAdapter.Fill(dataSet, "BookGroups");        
      
        dbCmd.CommandText = "spCountryGet";                                
        dataAdapter.Fill(dataSet, "Countries");
            
        dbCmd.CommandText = "spDeskTypeGet";                                
        dataAdapter.Fill(dataSet, "DeskTypes");       

        dbCmd.CommandText = "spFirmGet";                                
        dataAdapter.Fill(dataSet, "Firms");       
        
        dataSet.Tables["Books"].PrimaryKey = new DataColumn[2]
          { 
            dataSet.Tables["Books"].Columns["BookGroup"],
            dataSet.Tables["Books"].Columns["Book"]
          };

        dataSet.Tables["Countries"].PrimaryKey = new DataColumn[1]
          { 
            dataSet.Tables["Countries"].Columns["CountryCode"]
          };
        
        dataSet.Tables["DeskTypes"].PrimaryKey = new DataColumn[1]
          { 
            dataSet.Tables["DeskTypes"].Columns["DeskTypeCode"]
          };

        dataSet.Tables["Firms"].PrimaryKey = new DataColumn[1]
          { 
            dataSet.Tables["Firms"].Columns["FirmCode"]
          };
      }
      catch (Exception e)
      {
        Log.Write(e.Message + " [ServiceAgent.InventoryDeskInputDataGet]", Log.Error, 1);
        throw;
      }

      return dataSet;
    }

    public override object InitializeLifetimeService()
    {
      return null;
    }
  }
}
