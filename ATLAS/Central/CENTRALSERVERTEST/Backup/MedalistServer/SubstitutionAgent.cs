// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Anetics.Common;
using Anetics.SmartSeg;

namespace Anetics.Medalist
{
	public class SubstitutionAgent : MarshalByRefObject, ISubstitution
	{
		public event SubstitutionActivityEventHandler SubstitutionActivityEvent;

		private string dbCnStr;
		private Anetics.SmartSeg.ISmartSeg smartSegAgent;

		private IPosition positionAgent;

		private Anetics.SmartSeg.HeartbeatEventWrapper smartSegHeartbeatEventWrapper;
		private Anetics.SmartSeg.ProcessStatusEventWrapper smartSegProcessStatusEventWrapper;		
		
		private ArrayList smartSegProcessStatusEventArgsArray;

		private bool smartSegprocessStatusIsReady = false;

		private double heartbeatInterval;
		private string heartbeatAlert = "";

		private double smartSegHeartbeatInterval;
		private string smartSegHeartbeatAlert = "";
   
		private DateTime smartSegHeartbeatTimestamp = DateTime.UtcNow;
		private Anetics.SmartSeg.HeartbeatStatus smartSegHeartbeatStatus = Anetics.SmartSeg.HeartbeatStatus.Unknown;

		private bool faxEnabled;

		private delegate void FaxStatusUpdateDelegate();
		
    
		public SubstitutionAgent(string dbCnStr, ref Anetics.SmartSeg.ISmartSeg smartSegAgent, ref PositionAgent positionAgent)
		{
			this.dbCnStr				= dbCnStr;			
			this.smartSegAgent	= smartSegAgent;
			this.positionAgent  = positionAgent;

			smartSegHeartbeatEventWrapper = new Anetics.SmartSeg.HeartbeatEventWrapper(); 
			smartSegHeartbeatEventWrapper.HeartbeatEvent += new Anetics.SmartSeg.HeartbeatEventHandler(SmartSegHeartbeatOnEvent);
			
			smartSegProcessStatusEventWrapper = new Anetics.SmartSeg.ProcessStatusEventWrapper();
			smartSegProcessStatusEventWrapper.ProcessStatusEvent += new Anetics.SmartSeg.ProcessStatusEventHandler(SmartSegProcessStatusOnEvent);

			smartSegProcessStatusEventArgsArray = new ArrayList();		

			try
			{
				faxEnabled = bool.Parse(Standard.ConfigValue("WebServiceFaxEnabled", "False"));
				Log.Write("Faxing for recalls enabled: " + faxEnabled.ToString() + ". [SubstitutionAgent.SubstitutionAgent]", 2);

				heartbeatInterval = double.Parse(Standard.ConfigValue("HeartbeatInterval", "20000"));        
				Log.Write("Running with heartbeat interval of " + heartbeatInterval + " milliseconds. [SubstitutionAgent.SubstitutionAgent]", 2);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [SubstitutionAgent.SubstitutionAgent]", Log.Error, 1);
			} 

			SmartSegProcessStatusIsReady = true;
		}

		public void SubstitutionAgentConnect()
		{
			try
			{				
				smartSegAgent.HeartbeatEvent -= new Anetics.SmartSeg.HeartbeatEventHandler(smartSegHeartbeatEventWrapper.DoEvent);
				smartSegAgent.HeartbeatEvent += new Anetics.SmartSeg.HeartbeatEventHandler(smartSegHeartbeatEventWrapper.DoEvent);

				smartSegAgent.ProcessStatusEvent -= new Anetics.SmartSeg.ProcessStatusEventHandler(smartSegProcessStatusEventWrapper.DoEvent);
				smartSegAgent.ProcessStatusEvent += new Anetics.SmartSeg.ProcessStatusEventHandler(smartSegProcessStatusEventWrapper.DoEvent);
					
				SmartSegHeartbeatOnEvent(new Anetics.SmartSeg.HeartbeatEventArgs(Anetics.SmartSeg.HeartbeatStatus.Normal, ""));
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [SubstitutionAgent.SubstitutionAgentConnect]", Log.Error, 1);
			}
		}

		public void SubstitutionAgentDisconnect()
		{
			if (smartSegAgent == null)
			{
				Log.Write("The substitution agent has not been enabled for this session. [SubstitutionAgent.SubstitutionAgentDisconnect]", 2);
				return;
			}

			try
			{								
				smartSegAgent.HeartbeatEvent -= new Anetics.SmartSeg.HeartbeatEventHandler(smartSegHeartbeatEventWrapper.DoEvent);
				smartSegAgent.ProcessStatusEvent -= new Anetics.SmartSeg.ProcessStatusEventHandler(smartSegProcessStatusEventWrapper.DoEvent);
			}
			catch
			{
				Log.Write("The smartseg agent server is unreachable. [SubstitutionAgent.SubstitutionAgentDisconnect]", Log.Warning, 1);
			}
		}

		public DataSet SubstitutionInventoryDataGet(string effectDate)
		{
			return SubstitutionInventoryDataGet(effectDate, "");
		}
		
		public DataSet SubstitutionInventoryDataGet(string effectDate, string secId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spInventorySubstitutionsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				if (!effectDate.Equals(""))
				{
					SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);			
					paramBizDate.Value = effectDate;
				}

				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "SubstitutionInventory");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [SubstitutionAgent.SubstitutionInventoryDataGet]", 1);
				throw;
			}

			return dataSet;
		}

		public DataSet SubstitutionUpdatedDeficitExcessDataGet(string effectDate)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spUpdatedDeficitExcessGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);			
				paramBizDate.Value = effectDate;
				
				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "SubstitutionUpdatedDeficitExcess");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [SubstitutionAgent.SubstitutionUpdatedDeficitExcessDataGet]", 1);
				throw;
			}

			return dataSet;		
		}


		public DataSet SubstitutionSegEntriesDataGet(string bizDate, int utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spSegEntriesGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "SubstitutionSegEntries");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [SubstitutionAgent.SusbtitutionSegEntriesDataGet]", 1);
				throw;
			}

			return dataSet;
		}

		public DataSet SubstitutionMemoSegEntriesDataGet(string bizDate, int utcOffset)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dataSet = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spMemoSegEntriesGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
				paramUtcOffset.Value = utcOffset;

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "SubstitutionMemoSegEntries");
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [SubstitutionAgent.SubstitutionMemoSegEntriesDataGet]", 1);
				throw;
			}

			return dataSet;
		}

		public void SubstitutionSegEntryFlagSet(string processid, bool isrequested, bool isprocessed)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
      
			try
			{
				SqlCommand dbCmd = new SqlCommand("spSegEntrySet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
          
				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 16);
				paramProcessId.Value = processid;
        
				SqlParameter paramRequested = dbCmd.Parameters.Add("@IsRequested", SqlDbType.Bit, 1);
				paramRequested.Value = isrequested;

				SqlParameter paramProcessed = dbCmd.Parameters.Add("@IsProcessed", SqlDbType.Bit, 1);
				paramProcessed.Value = isprocessed;     

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [SubstitutionAgent.SubstitutionSegEntryFlagSet]", Log.Error, 1);
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

		public DataSet SubstitutionGet(
			string processId,
			string bookGroup,
			string actUserId,
			short  utcOffset)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			SqlDataAdapter dataAdapter = null;
			DataSet dataSet = new DataSet();

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spSubstitutionsGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 16);
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				SqlParameter paramUtcOffset = dbCmd.Parameters.Add("@UtcOffset", SqlDbType.SmallInt);
			
				if (!processId.Equals(""))
				{
					paramProcessId.Value = processId;
				}

				if(!bookGroup.Equals(""))
				{
					paramBookGroup.Value = bookGroup;
				}

				if (!actUserId.Equals(""))
				{
					paramActUserId.Value = actUserId;
				}

				paramUtcOffset.Value = utcOffset;			

				dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dataSet, "Substitutions");

				dataSet.Tables["Substitutions"].PrimaryKey = new DataColumn[2]
					{ 
						dataSet.Tables["Substitutions"].Columns["BizDate"],
						dataSet.Tables["Substitutions"].Columns["ProcessId"]
					};
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[SubstitutionAgent.SubstitutionsGet]", Log.Error, 1);
				throw;
			}

			return dataSet;
		}
		
		public string SubstitutionSet(
			string	processId,
			string	bookGroup,			
			string	secId,
			string	requestedQuantity,
			string	quantity,
			string  overrideRate,
			string  excessQuantity,
			string	psrQuantity,
			string	substitutionQuantity,
			string	type,
			string	status,
			string	actUserId)
		{
			SqlConnection dbCn = null;				
			SqlCommand dbCmd = null;
			SqlDataReader dataReader = null;
			SubstitutionActivityEventArgs substitutionActivityEventArgs = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spSubstitutionSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				
				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 10);
				paramProcessId.Value = ((processId.Equals(""))?"5" + DateTime.UtcNow.ToString("dHHmmssff"):processId);
				
				if (!bookGroup.Equals(""))
				{
					SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
					paramBookGroup.Value = bookGroup;
				}

				if (!secId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);      
					paramSecId.Value = secId;
				}

				if (!requestedQuantity.Equals(""))
				{
					SqlParameter paramRequestedQuantity = dbCmd.Parameters.Add("@RequestedQuantity", SqlDbType.BigInt);      
					paramRequestedQuantity.Value = requestedQuantity;
				}
        
				if (!quantity.Equals(""))
				{
					SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);      
					paramQuantity.Value = quantity;
				}        


				if (!overrideRate.Equals(""))
				{
					SqlParameter paramOverrideRate = dbCmd.Parameters.Add("@OverrideRate", SqlDbType.Decimal);      
					paramOverrideRate.Value = overrideRate;
				}

				if (!excessQuantity.Equals(""))
				{
					SqlParameter paramExcessQuantity = dbCmd.Parameters.Add("@ExcessQuantity", SqlDbType.BigInt);      
					paramExcessQuantity.Value = excessQuantity;
				}        

				if (!psrQuantity.Equals(""))
				{
					SqlParameter paramPsrQuantity = dbCmd.Parameters.Add("@PsrQuantity", SqlDbType.BigInt);      
					paramPsrQuantity.Value = psrQuantity;
				}    				

				if (!substitutionQuantity.Equals(""))
				{
					SqlParameter paramSubstitutionQuantity = dbCmd.Parameters.Add("@SubstitutionQuantity", SqlDbType.BigInt);      
					paramSubstitutionQuantity.Value = substitutionQuantity;
				}        


				if (!type.Equals(""))
				{
					SqlParameter paramType = dbCmd.Parameters.Add("@Type", SqlDbType.VarChar, 1);      
					paramType.Value = type;
				}

				SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.VarChar, 1);
				if (!status.Equals(""))
				{
					paramStatus.Value = status;
				}

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				if(!actUserId.Equals(""))
				{
					paramActUserId.Value = actUserId;
				}

				dbCn.Open();	
				dataReader = dbCmd.ExecuteReader();
				
				while(dataReader.Read())
				{
					substitutionActivityEventArgs = new SubstitutionActivityEventArgs(
						dataReader.GetValue(0).ToString(),
						dataReader.GetValue(1).ToString(),
						dataReader.GetValue(2).ToString(),
						dataReader.GetValue(3).ToString(),
						dataReader.GetValue(4).ToString(),
						dataReader.GetValue(5).ToString(),
						dataReader.GetValue(6).ToString(),
						dataReader.GetValue(7).ToString(),
						dataReader.GetValue(8).ToString(),
						dataReader.GetValue(9).ToString(),						
						dataReader.GetValue(10).ToString(),
						dataReader.GetValue(11).ToString(),
						dataReader.GetValue(12).ToString(),
						dataReader.GetValue(13).ToString(),						
						dataReader.GetValue(14).ToString(),
						dataReader.GetValue(15).ToString());
				}
	
				dataReader.Close();
			
				if (status.Equals("S"))
				{
					try
					{
						if (smartSegAgent == null)
						{
							Log.Write("SmartSeg Is Null!!!!", 3);
						}
						
						smartSegAgent.SubstitutionRequest(
							substitutionActivityEventArgs.ProcessId,
							substitutionActivityEventArgs.SecId,
							"CUSIP", //always submit cusips
							substitutionActivityEventArgs.Type,
							substitutionActivityEventArgs.RequestedQuantity.ToString(),
							substitutionActivityEventArgs.Quantity.ToString(),							
							substitutionActivityEventArgs.OverrideRate,
							"",
							false.ToString()); //S3 deprecated feature
					
						paramStatus.Value = "P";
						substitutionActivityEventArgs.Status = "P";
					}
					catch (Exception error)
					{
						paramStatus.Value = "E";
						substitutionActivityEventArgs.Status = "E";
						Log.Write(error.StackTrace, 3);
					}
					
					dbCmd.ExecuteNonQuery();
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [SubstitutionAgent.SubstitutionSet]", Log.Error, 1);
				throw;
			}
			finally
			{
				if (!dataReader.IsClosed)
				{
					dataReader.Close();
				}
				
				if (dbCn.State != ConnectionState.Closed)
				{
					dbCn.Close();
				}
			}

			SubstitutionActivityEventHandler substitutionActivityEventHandler = new SubstitutionActivityEventHandler(SubstitutionActivityEventInvoke);
			substitutionActivityEventHandler.BeginInvoke(substitutionActivityEventArgs, null, null);
		
			return substitutionActivityEventArgs.ProcessId;
		}

		public void SmartSegHeartbeatOnEvent(Anetics.SmartSeg.HeartbeatEventArgs e)
		{ 
			smartSegHeartbeatTimestamp = DateTime.UtcNow;
      
			smartSegHeartbeatAlert = e.Alert;
			smartSegHeartbeatStatus = e.Status;
      
			Log.Write(smartSegHeartbeatStatus.ToString() + ": " + smartSegHeartbeatAlert + " [SubstitutionAgent.SmartSegHeartbeatOnEvent]", 3);
		}

		private void SubstitutionActivityEventInvoke(SubstitutionActivityEventArgs substitutionActivityEventArgs)
		{
			SubstitutionActivityEventHandler substitutionActivityEventHandler = null;

			string substitutionIdentifier = substitutionActivityEventArgs.BookGroup + ":" + substitutionActivityEventArgs.Type + ":" + substitutionActivityEventArgs.SecId + "[" + substitutionActivityEventArgs.Symbol + "]:" +  substitutionActivityEventArgs.Quantity.ToString() + " [SubstitutionAgent.SubstitutionActivityEventInvoke]";

			try
			{
				if (SubstitutionActivityEvent == null)
				{
					Log.Write("Handling a substitution event for " + substitutionIdentifier + " with no delegates. [SubstitutionAgent.SubstitutionActivityEventInvoke]", 2);
				}
				else
				{
					int n = 0;

					Delegate[] eventDelegates = SubstitutionActivityEvent.GetInvocationList();
					Log.Write("Handling a substitution event for " + substitutionIdentifier + " with " + eventDelegates.Length + " delegates. [SubstitutionAgent.SubstitutionActivityEventInvoke]", 2);
          
					foreach (Delegate eventDelegate in eventDelegates)
					{
						Log.Write("Invoking delegate [" + (++n) + "]. [SubstitutionAgent.SubstitutionActivityEventInvoke]", 3);

						try
						{
							substitutionActivityEventHandler = (SubstitutionActivityEventHandler)eventDelegate;
							substitutionActivityEventHandler(substitutionActivityEventArgs);
						}
						catch (System.Net.Sockets.SocketException)
						{
							SubstitutionActivityEvent -= substitutionActivityEventHandler;
							Log.Write("Substitution event delegate [" + n + "] has been removed from the invocation list. [SubstitutionAgent.SubstitutionActivityEventInvoke]", 3);
						}
						catch (Exception e)
						{
							Log.Write(e.Message + " [SubstitutionAgent.SubstitutionActivityEventInvoke]", Log.Error, 1);
						}
					}

					Log.Write("Done invoking the substitution event invocation list. [SubstitutionAgent.SubstitutionActivityEventInvoke]", 3);
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + ". [SubstitutionAgent.SubstitutionActivityEventInvoke]", Log.Error, 1);
			}
		}
		

		private bool SmartSegProcessStatusIsReady
		{
			get
			{
				return smartSegprocessStatusIsReady;
			}

			set
			{
				Anetics.SmartSeg.ProcessStatusEventArgs processStatusEventArgs ;

				try
				{
					lock (this)
					{
						if (value && (smartSegProcessStatusEventArgsArray.Count > 0))
						{          
							smartSegprocessStatusIsReady = false;

							processStatusEventArgs = (Anetics.SmartSeg.ProcessStatusEventArgs)smartSegProcessStatusEventArgsArray[0];
							smartSegProcessStatusEventArgsArray.RemoveAt(0);

							Anetics.SmartSeg.ProcessStatusEventHandler processStatusEventHandler;
							processStatusEventHandler = new Anetics.SmartSeg.ProcessStatusEventHandler(SmartSegProcessStatusDoEvent);
              
							processStatusEventHandler.BeginInvoke(processStatusEventArgs, null, null);            
						}
						else
						{
							smartSegprocessStatusIsReady = value;
						}
					}
				}
				catch (Exception ee)
				{
					Log.Write(ee.Message + " [SubstitutionAgent.SmartSegProcessStatusIsReady(set)]", Log.Error, 1); 
				}
			}
		}

		private void SmartSegProcessStatusOnEvent(Anetics.SmartSeg.ProcessStatusEventArgs e)
		{
			lock (this)
			{
				Log.Write("Queuing smart seg process status event [" + e.ActCode + "] " + e.ProcessId + " with " +
					smartSegProcessStatusEventArgsArray.Count + " events already queued. [SubstitutionAgent.SmartSegProcessStatusOnEvent]", 3);

				smartSegProcessStatusEventArgsArray.Add(e);
    
				if (SmartSegProcessStatusIsReady) // Force reset to trigger handling of event.
				{
					SmartSegProcessStatusIsReady = true;
				}
			}
		}
    
		private void SmartSegProcessStatusDoEvent(Anetics.SmartSeg.ProcessStatusEventArgs e)
		{
			Log.Write("Handling status event [" + e.ActCode + "] " + e.ProcessId + ". [SubstitutionAgent.SmartSegProcessStatusDoEvent]", 3);

			try
			{
				switch(e.ActCode.Trim())
				{					
					case "SR":
						SubstitutionSet(e.ProcessId, "", "", "", "", "", "", "", "", "", "", "");						
						break;

					default:
						Log.Write("Unanticipated event type:" + e.ActCode + " [SubstitutionAgent.SmartSegProcessStatusDoEvent]", Log.Error, 1);

						break;
				}
				
				positionAgent.ProcessStatusEventInvoke(new Anetics.Medalist.ProcessStatusEventArgs(
					e.ProcessId,
					e.SystemCode,
					e.ActCode,
					e.Act,
					e.ActTime,
					e.ActUser,
					e.HasError,
					e.BookGroup,
					e.ContractId,
					e.ContractType,
					e.Book,
					e.SecId,
					e.Symbol,
					e.Quantity,
					e.Amount,
					e.Status,
					e.StatusTime,
					e.Tag));
			}
			catch (Exception ee)
			{
				Log.Write(ee.Message + " [SubstitutionAgent.SmartSegProcessStatusDoEvent]", Log.Error, 1);
			}
			finally
			{
				SmartSegProcessStatusIsReady = true;
			}
		}
  
		public string Alert
		{
			get
			{
				return heartbeatAlert;
			}
		}

		public Anetics.SmartSeg.HeartbeatStatus Status
		{
			get
			{
				if (smartSegHeartbeatTimestamp.AddMilliseconds(1.5 * heartbeatInterval).CompareTo(DateTime.UtcNow).Equals(1))
				{
					return smartSegHeartbeatStatus;
				}
				else
				{
					return Anetics.SmartSeg.HeartbeatStatus.Unknown;
				}
			}
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
