using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Anetics.Common;
using Anetics.S3;
using Anetics.SmartSeg;

namespace Anetics.S3
{	
	public delegate bool TransactionRepsonseHandlerDelegate(string messageId, string messageText);
	public delegate bool SegEntriesHandlerDelegate(string messageId, string messageText);
	public delegate bool TrmLockupHandlerDelegate(string messageId, string messageText);
	public delegate bool TrmReleaseHandlerDelegate(string messageId, string messageText);

	public class S3Agent :  MarshalByRefObject, ISmartSeg
	{
		public event ProcessStatusEventHandler ProcessStatusEvent;
		public event HeartbeatEventHandler HeartbeatEvent;
		
		private MqSeries mqSeries = null;
		private string dbCnStr = "";

		private int heartbeatInterval = 20000;
		private Thread heartbeatThread = null;
		
		private string successCode = "";

		public S3Agent(string dbCnStr)
		{		
			try
			{
				this.dbCnStr = dbCnStr;

				successCode = KeyValue.Get("STKSEGSuccessCode", "ok", dbCnStr);
				
				mqSeries = new MqSeries(
					Standard.ConfigValue("HostName"),
					Standard.ConfigValue("Channel"),
					int.Parse(Standard.ConfigValue("Port")));
			}
			catch (Exception e)
			{
				Log.Write(e.Message + "[S3Agent.S3Agent]", 1);
			}
		}

		public void Start()
		{		
			if ((heartbeatThread == null) || (!heartbeatThread.IsAlive)) // Must create new thread.
			{
				heartbeatThread = new Thread(new ThreadStart(Heartbeat));
				heartbeatThread.Name = "Heartbeat";
				heartbeatThread.Start();

				Log.Write("Start command issued with new heartbeat thread. [S3Agent.Start]", 3);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Start command issued with heartbeat thread already running. [S3Agent.Start]", 3);
			}

			try
			{			
				mqSeries.WaitSegEntriesStart(
					Standard.ConfigValue("SegEntriesQueueManagerName"),
					Standard.ConfigValue("SegEntriesQueueName"),
					int.Parse(Standard.ConfigValue("SegEntriesWaitSeconds")),
					new SegEntriesHandlerDelegate(SegEntriesHandler));		
				
				mqSeries.WaitTransactionResponseStart(
					Standard.ConfigValue("SubstitutionResponseQueueManagerName"),
					Standard.ConfigValue("SubstitutionResponseQueueName"),
					int.Parse(Standard.ConfigValue("SubstitutionResponseWaitSeconds")),
					new TransactionRepsonseHandlerDelegate(TransactionResponseHandler));	

				mqSeries.WaitTrmReleaseStart(
					Standard.ConfigValue("TrmReleaseQueueManagerName"),
					Standard.ConfigValue("TrmReleaseQueueName"),
					int.Parse(Standard.ConfigValue("TrmReleaseWaitSeconds")),
					new TrmReleaseHandlerDelegate(TrmReleaseHandler));	

				mqSeries.WaitTrmLockupStart(
					Standard.ConfigValue("TrmLockupQueueManagerName"),
					Standard.ConfigValue("TrmLockupQueueName"),
					int.Parse(Standard.ConfigValue("TrmLockupWaitSeconds")),
					new TrmLockupHandlerDelegate(TrmLockupHandler));	
			}
			catch (Exception e)
			{
				Log.Write(e.Message + "[S3Agent.Start]", Log.Error, 1);
			}
		}

		public void Stop()
		{
			try
			{				
				mqSeries.WaitSegEntriesStop();
				mqSeries.WaitTransactionResponseStop();
				mqSeries.WaitTrmLockupStop();
				mqSeries.WaitTrmReleaseStop();

			}
			catch (Exception e)
			{
				Log.Write(e.Message + "[S3Agent.Stop]", Log.Error, 1);
			}		

			HeartbeatEventInvoke(new HeartbeatEventArgs(Anetics.SmartSeg.HeartbeatStatus.Stopping, ""));
		}


		private void Heartbeat()
		{
			while (!S3Main.IsStopped) // Loop through this block.
			{
				HeartbeatEventInvoke(new HeartbeatEventArgs(HeartbeatStatus.Normal, ""));

				if (!S3Main.IsStopped)
				{
					Thread.Sleep(heartbeatInterval);
				}
			}
		}

		private void HeartbeatEventInvoke(HeartbeatEventArgs heartbeatEventArgs)
		{
			HeartbeatEventHandler heartbeatEventHandler = null;

			Log.Write(heartbeatEventArgs.Status.ToString() + ": " + heartbeatEventArgs.Alert + " [S3Agent.HeartbeatEventInvoke]", 3);

			try
			{
				if (HeartbeatEvent == null)
				{
					Log.Write("There are no HeartbeatEvent delegates. [S3Agent.HeartbeatEventInvoke]", 3);
				}
				else
				{
					int n = 0;

					Delegate[] eventDelegates = HeartbeatEvent.GetInvocationList();
					Log.Write("HeartbeatEvent has " + eventDelegates.Length + " delegate[s]. [S3Agent.HeartbeatEventInvoke]", 3);
      
					foreach (Delegate eventDelegate in eventDelegates)
					{
						Log.Write("Invoking HeartbeatEvent delegate [" + (++n) + "]. [S3Agent.HeartbeatEventInvoke]", 3);
            
						try
						{
							heartbeatEventHandler = (HeartbeatEventHandler) eventDelegate;
							heartbeatEventHandler(heartbeatEventArgs);
						}
						catch (System.Net.Sockets.SocketException)
						{
							HeartbeatEvent -= heartbeatEventHandler;
							Log.Write("HeartbeatEvent delegate [" + n + "] has been removed from the invocation list. [S3Agent.HeartbeatEventInvoke]", 3);
						}
						catch (Exception e)
						{
							Log.Write(e.Message + " [S3Agent.HeartbeatEventInvoke]", Log.Error, 1);
						}
					}
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [S3Agent.HeartbeatEventInvoke]", Log.Error, 1);
			}
		}

		public void SubstitutionRequest (
			string	processId,
			string	secId,
			string	secIdType,
			string  requestType,
			string 	quantity,
			string	minQuantity,
			string	overrideRate,
			string	maxProcessingTime,
			string	sendDTCMemoSeg)
		{			
			S3_Substitution subReq = new S3_Substitution();
			
			if (requestType.Equals("S"))
			{
				requestType = "Substitution";
			}
			else
			{
				requestType = "WhatIf";
			}
			
			ProcessStatusEventArgs processStatusEventArgs = subReq.Request(
				processId,
				requestType,
				secId,
				secIdType,
				quantity,
				minQuantity,
				overrideRate,
				maxProcessingTime,
				false.ToString());		
			
			ProcessStatusEventInvoke(ProcessStatusSet(processStatusEventArgs));				
			XmlMessageSet(Standard.ProcessId(), subReq.Message);

			try
			{
				mqSeries.Put(
					Standard.ConfigValue("SubstitutionRequestQueueManagerName"),
					Standard.ConfigValue("SubstitutionRequestQueueName"),
					Standard.ConfigValue("Credentials"),
					processId,
					subReq.Message,
					Standard.ConfigValue("SubstitutionResponseQueueManagerName"),
					Standard.ConfigValue("SubstitutionResponseQueueName"));
			}
			catch (Exception error)
			{																
				processStatusEventArgs = new ProcessStatusEventArgs(
					processStatusEventArgs.ProcessId,
					processStatusEventArgs.SystemCode,
					processStatusEventArgs.ActCode,
					"",
					"",
					"",
					true,
					"",
					"",
					"",
					"",
					"",
					"",
					"",
					"", 
					error.Message,
					"",
					"");
				
				ProcessStatusEventInvoke(ProcessStatusSet(processStatusEventArgs));				
				
				Log.Write(error.Message + " [S3Agent.SubstitutionRequest]", Log.Error, 3);
				throw new Exception(error.Message);
			}
		}


		public void MarginPositonDeleteRequest (
			string		processId,
			string		secId,
			string		secIdType,
			string		accountNumber)
		{			
			S3_Margin_Positon_Delete mpdReq = new S3_Margin_Positon_Delete();
			
			ProcessStatusEventArgs processStatusEventArgs = mpdReq.Request(
				processId,				
				secId,
				secIdType,
				accountNumber);								

			XmlMessageSet(Standard.ProcessId(), mpdReq.Message);
			ProcessStatusSet(processStatusEventArgs);
			
			try
			{
				mqSeries.Put(
					Standard.ConfigValue("SubstitutionRequestQueueManagerName"),
					Standard.ConfigValue("SubstitutionRequestQueueName"),
					Standard.ConfigValue("Credentials"),
					processId,
					mpdReq.Message,
					Standard.ConfigValue("SubstitutionResponseQueueManagerName"),
					Standard.ConfigValue("SubstitutionResponseQueueName"));
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[S3Agent.MarginPositionDeleteRequest]", Log.Error, 1);								
			}
		}

		private void MemoSegEntrySet(string processId, string secId, string quantity, bool isRequested, bool isProcessed)
		{						
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			SqlCommand dbCmd  = null;

			try
			{
			
				dbCmd = new SqlCommand("spMemoSegEntrySet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 16);
				paramProcessId.Value = processId;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);				
				paramQuantity.Value = quantity;

				SqlParameter paramIsRequested = dbCmd.Parameters.Add("@IsRequested", SqlDbType.Bit);
				paramIsRequested.Value = isRequested;

				SqlParameter paramIsProcessed = dbCmd.Parameters.Add("@IsProcessed", SqlDbType.Bit);
				paramIsProcessed.Value = isProcessed;
		
				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + "[S3Agent.MemoSegEntrySet]", Log.Error, 1);								
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

		private bool SegEntriesHandler(string messsageId, string messageText)
		{            
			ProcessStatusEventArgs processStatusEventArgs = null;
			
			S3Server.STKSEGModule.STKSEGModule webService = new S3Server.STKSEGModule.STKSEGModule();
			
			string accountNumber = "";
			string indicator = "";
			string processId = "";
			string fromLocation = "";
			string toLocation = "";

			XmlMessageSet(Standard.ProcessId(), messageText);	

				try
				{							
						if ((KeyValue.Get("S3StartOfDayDate", "2001-01-01", dbCnStr).Equals(Master.BizDateExchange)) && DateTime.Now.ToString("yyyy-MM-dd").Equals(Master.BizDateExchange))
						{																			
								S3_SegEntry_Datagram segEntryDatagram = new S3_SegEntry_Datagram(dbCnStr);
								processStatusEventArgs = segEntryDatagram.Response(messageText);
								processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);	
				
								accountNumber = Tools.SplitItem(processStatusEventArgs.Tag, "|", 0);
								indicator = Tools.SplitItem(processStatusEventArgs.Tag, "|", 1);
								processId = Standard.ProcessId("S");

								SegEntrySet(
										processId,
										accountNumber.Substring(0, 8),
										accountNumber.Substring(9, 1),
										processStatusEventArgs.SecId,
										processStatusEventArgs.Quantity,
										indicator,
										"I",
										true,
										false,
										false,
										"ADMIN");

								accountNumber = accountNumber.Substring(0, 8) + accountNumber.Substring(9, 1);

								switch(indicator)
								{
										case "I":
												fromLocation = "C";
												toLocation = "S";
												break;

										case "D":
												fromLocation = "S";
												toLocation = "C";
												break;

										default:
												fromLocation = "";
												toLocation = "";
												break;
								}

				
								webService.Url = Standard.ConfigValue("STKSEGWebService", "");
								S3Server.STKSEGModule.STKSEGTaskInfo objStatus = webService.STKSEGTask(fromLocation, toLocation, accountNumber, processStatusEventArgs.Quantity,  processStatusEventArgs.SecId, Standard.ConfigValue("STKSEGDescription", ""));								
				
								SegEntrySet(
										processId,
										"",
										"",
										"",
										"",
										"",					
										"",
										((objStatus.status.Equals(successCode))? true: false),
										((objStatus.status.Equals(successCode))? true: false),
										((objStatus.status.Equals(successCode))? false: true),
										"ADMIN");
																
								ProcessStatusEventInvoke(processStatusEventArgs);
								Log.Write(messageText + " [S3Agent.SegEntriesHandler]", 3);                  																									
						}	
						else
						{
								Log.Write("S3 Start Of Day has not completed yet for : " + Master.BizDateExchange  +  ". [S3Agent.SegEntriesHandler]", Log.Warning, 1);
								return false;
						}
				}
				catch (Exception e)
				{
						Log.Write(e.Message + " [S3Agent.SegEntriesHandler]", Log.Error, 1);    
						return false;
				}			
			
			return true;
		}

		public void SegEntrySet(string processid, string accountNumber, string accountType, string secId, string quantity, string indicator, string timeOfDay,  bool isrequested, bool isprocessed, bool isFailed, string actUserId)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
      
			try
			{
				SqlCommand dbCmd = new SqlCommand("spSegEntrySet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
          
				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 16);
				paramProcessId.Value = processid;
        
				if (!accountNumber.Equals(""))
				{
					SqlParameter paramAccountNumber = dbCmd.Parameters.Add("@AccountNumber", SqlDbType.VarChar, 8);
					paramAccountNumber.Value = accountNumber;
				}

				if (!accountType.Equals(""))
				{
					SqlParameter paramAccountType = dbCmd.Parameters.Add("@AccountType", SqlDbType.VarChar, 1);
					paramAccountType.Value = accountType;
				}
					
				if (!secId.Equals(""))
				{
				
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = secId;
				}

				if (!quantity.Equals(""))
				{				
					SqlParameter paramEntryQty = dbCmd.Parameters.Add("@EntryQty", SqlDbType.BigInt);
					paramEntryQty.Value = quantity;
				}
				if (!indicator.Equals(""))
				{
				
					SqlParameter paramIndicator = dbCmd.Parameters.Add("@Indicator", SqlDbType.VarChar, 1);
					paramIndicator.Value = indicator;
				}
				
				if (!timeOfDay.Equals(""))
				{				
					SqlParameter paramTimeOfDay = dbCmd.Parameters.Add("@TimeOfDay", SqlDbType.VarChar, 1);
					paramTimeOfDay.Value = timeOfDay;
				}

				SqlParameter paramRequested = dbCmd.Parameters.Add("@IsRequested", SqlDbType.Bit, 1);
				paramRequested.Value = isrequested;
				
				SqlParameter paramProcessed = dbCmd.Parameters.Add("@IsProcessed", SqlDbType.Bit, 1);
				paramProcessed.Value = isprocessed;  
   
				SqlParameter paramIsFailed = dbCmd.Parameters.Add("@IsFailed", SqlDbType.Bit, 1);
				paramIsFailed.Value = isFailed;   

				SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
				paramActUserId.Value = actUserId; 

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [S3Agent.SegEntrySet]", Log.Error, 1);
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

			private bool TransactionResponseHandler(string messageId, string messageText)
			{       			     
					ProcessStatusEventArgs processStatusEventArgs = null;          
					string status = "";
					string excessQuantity, psrQuantity, substitutionQuantity;

					Log.Write(messageText + " [S3Agent.TransactionResponseHandler]", 3);                  						

					XmlMessageSet(Standard.ProcessId(), messageText);

					try
					{						

							S3_Substitution subReq = new S3_Substitution();
							processStatusEventArgs = subReq.Response(messageText);
							processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);	
				
							if (processStatusEventArgs.HasError)
							{
									status = "E";
				
									excessQuantity = "";
									psrQuantity = "";					
									substitutionQuantity = "";

							}
							else
							{
									status = "A";
					
									excessQuantity = Tools.SplitItem(processStatusEventArgs.Tag, "|", 0);
									psrQuantity = Tools.SplitItem(processStatusEventArgs.Tag, "|", 1);	
									substitutionQuantity = Tools.SplitItem(processStatusEventArgs.Tag, "|", 2);	
			
							}
				
							SubstitutionSet(processStatusEventArgs.ProcessId, 
									excessQuantity,
									psrQuantity,
									substitutionQuantity,
									status);

				
							ProcessStatusEventInvoke(processStatusEventArgs);
										
							return true;
					}
					catch (Exception e)
					{
							Log.Write(e.Message + " [S3Agent.TransactionResponseHandler]", Log.Error, 1);    
					}			
			
					return (false || (bool.Parse(KeyValue.Get("SubstitutionTransactionReplyOnError", "False", dbCnStr))));
			}

		private void ProcessStatusEventInvoke(ProcessStatusEventArgs processStatusEventArgs)
		{
			ProcessStatusEventHandler processStatusEventHandler = null;
      
			string processStatusIdentifier =  "[" + processStatusEventArgs.ActCode + "] " + processStatusEventArgs.ProcessId;

			try
			{
				if (ProcessStatusEvent == null)
				{
					Log.Write("Handling a process status event for " + processStatusIdentifier + " with no delegates. [S3Agent.ProcessStatusEventInvoke]", 2);
				}
				else
				{
					int n = 0;

					Delegate[] eventDelegates = ProcessStatusEvent.GetInvocationList();
					Log.Write("Handling a process status event for " + processStatusIdentifier + " with " + eventDelegates.Length + " delegates. [S3Agent.ProcessStatusEventInvoke]", 2);
          
					foreach (Delegate eventDelegate in eventDelegates)
					{
						Log.Write("Invoking delegate [" + (++n) + "]. [S3Agent.ProcessStatusEventInvoke]", 3);

						try
						{
							processStatusEventHandler = (ProcessStatusEventHandler) eventDelegate;
							processStatusEventHandler(processStatusEventArgs);				
						}
						catch (System.Net.Sockets.SocketException)
						{
							ProcessStatusEvent -= processStatusEventHandler;
							Log.Write("Process status event delegate [" + n + "] has been removed from the invocation list. [S3Agent.ProcessStatusEventInvoke]", 3);
						}
						catch (Exception e)
						{
							Log.Write(e.Message + " [S3Agent.ProcessStatusEventInvoke]", Log.Error, 1);
						}
					}

					Log.Write("Done invoking the process status event invocation list. [S3Agent.ProcessStatusEventInvoke]", 3);
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [S3Agent.ProcessStatusEventInvoke]", Log.Error, 1);
			}
		}

		public void SubstitutionSet(
			string	processId,		
			string  excessQuantity,
			string	psrQuantity,		
			string	substitutionQuantity,
			string	status)
		{
			SqlConnection dbCn = null;				
			SqlCommand dbCmd = null;						

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spSubstitutionSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
				
				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 16);
				paramProcessId.Value = processId;							

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
						
				SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.VarChar, 1);
				if (!status.Equals(""))
				{
					paramStatus.Value = status;
				}

				dbCn.Open();	
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [S3Agent.SubstitutionSet]", Log.Error, 1);
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

		private bool TrmReleaseHandler(string messsageId, string messageText)
		{            
			S3_TRM_Datagram trmDatagram = null;

			XmlMessageSet(Standard.ProcessId(), messageText);     
			
			try
			{							
				Log.Write(messageText + " [S3Agent.TrmReleaseHandler]", 3);
				
				trmDatagram = new S3_TRM_Datagram();
				ProcessStatusEventArgs processStatusEventArgs = trmDatagram.Reponse(dbCnStr, messageText, false);
								
				ProcessStatusEventInvoke(ProcessStatusSet(processStatusEventArgs));			            						
			}		
			catch (Exception e)
			{
				Log.Write(e.Message + " [S3Agent.TrmReleaseHandler]", Log.Error, 1);    
				return false;
			}			
			
			return true;
		}

		private bool TrmLockupHandler(string messsageId, string messageText)
		{            
			S3_TRM_Datagram trmDatagram = null;

			XmlMessageSet(Standard.ProcessId(), messageText);     

			try
			{							
				Log.Write(messageText + " [S3Agent.TrmLockupHandler]", 3);
				
				trmDatagram = new S3_TRM_Datagram();
				ProcessStatusEventArgs processStatusEventArgs = trmDatagram.Reponse(dbCnStr, messageText, true);
								
				ProcessStatusEventInvoke(ProcessStatusSet(processStatusEventArgs));
				            						
			}		
			catch (Exception e)
			{
				Log.Write(e.Message + " [S3Agent.TrmLockupHandler]", Log.Error, 1);    
				return false;
			}			
			
			return true;
		}

		private void XmlMessageSet(string processId , string messageText)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

				try
				{
						dbCn = new SqlConnection(dbCnStr);
        
						dbCmd = new SqlCommand("spSmartSegXmlMessageInsert", dbCn);
						dbCmd.CommandType = CommandType.StoredProcedure;

						SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.Char, 16);			
						paramProcessId.Value = processId;

						SqlParameter paramContent = dbCmd.Parameters.Add("@Content", SqlDbType.VarChar, 8000);			
						paramContent.Value = messageText;

						dbCn.Open();
						dbCmd.ExecuteNonQuery();
				}
				catch (Exception error)
				{
						Log.Write(error.Message + " [S3Agent.XmlMessageSet]", Log.Error, 1);       				
						throw;
				}
				finally
				{
				if(dbCn.State != ConnectionState.Closed)
						dbCn.Close();
				}
		}

		private ProcessStatusEventArgs ProcessStatusSet(ProcessStatusEventArgs processStatus)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			SqlDataReader dataReader = null;
			ProcessStatusEventArgs processStatusEventArgs = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spProcessStatusSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 16);
				paramProcessId.Value = processStatus.ProcessId;

				SqlParameter paramSystemCode = dbCmd.Parameters.Add("@SystemCode", SqlDbType.Char, 1);
				paramSystemCode.Value = processStatus.SystemCode;

				SqlParameter paramActCode = dbCmd.Parameters.Add("@ActCode", SqlDbType.Char, 3);
				paramActCode.Value = processStatus.ActCode;

				if (!processStatus.Act.Equals(""))
				{
					SqlParameter paramAct = dbCmd.Parameters.Add("@Act", SqlDbType.VarChar, 255);
					paramAct.Value = processStatus.Act;
				}
	
				if (!processStatus.ActUser.Equals(""))
				{
					SqlParameter paramActUserId = dbCmd.Parameters.Add("@ActUserId", SqlDbType.VarChar, 50);
					paramActUserId.Value = processStatus.ActUser;
				}

				SqlParameter paramHasError = dbCmd.Parameters.Add("@HasError", SqlDbType.Bit);
				paramHasError.Value = processStatus.HasError;

				if (!processStatus.BookGroup.Equals(""))
				{
					SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 10);
					paramBookGroup.Value = processStatus.BookGroup;
				}

				if (!processStatus.ContractId.Equals(""))
				{
					SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.VarChar, 15);
					paramContractId.Value = processStatus.ContractId;
				}

				if (!processStatus.ContractType.Equals(""))
				{
					SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);
					paramContractType.Value = processStatus.ContractType;
				}

				if (!processStatus.Book.Equals(""))
				{
					SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.VarChar, 10);
					paramBook.Value = processStatus.Book;
				}

				if (!processStatus.SecId.Equals(""))
				{
					SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
					paramSecId.Value = processStatus.SecId;
				}

				if (!processStatus.Quantity.Equals(""))
				{
					SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
					paramQuantity.Value = processStatus.Quantity;
				}

				if (!processStatus.Amount.Equals(""))
				{
					SqlParameter paramAmount = dbCmd.Parameters.Add("@Amount", SqlDbType.Money);
					paramAmount.Value = processStatus.Amount;
				}

				if (!processStatus.Status.Equals(""))
				{
					SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.VarChar, 255);
					paramStatus.Value = processStatus.Status;
				}

				if (!processStatus.StatusTime.Equals(""))
				{
					SqlParameter paramStatusTime = dbCmd.Parameters.Add("@StatusTime", SqlDbType.DateTime);
					paramStatusTime.Value = processStatus.StatusTime;
				}
      
				if (!processStatus.Tag.Equals(""))
				{
					SqlParameter paramTag = dbCmd.Parameters.Add("@Tag", SqlDbType.VarChar, 255);
					paramTag.Value = processStatus.Tag;
				}
      
				SqlParameter paramReturnData = dbCmd.Parameters.Add("@ReturnData", SqlDbType.Bit);
				paramReturnData.Value = true;                  
        
				dbCn.Open();
				dataReader = dbCmd.ExecuteReader();
        
				while(dataReader.Read()) // Expect one row.
				{
					processStatusEventArgs = new ProcessStatusEventArgs(
						dataReader.GetValue(0).ToString(),
						dataReader.GetValue(1).ToString(),
						dataReader.GetValue(2).ToString(),
						dataReader.GetValue(3).ToString(),
						dataReader.GetValue(4).ToString(),
						dataReader.GetValue(5).ToString(),
						(bool) dataReader.GetValue(6),
						dataReader.GetValue(7).ToString(),
						dataReader.GetValue(8).ToString(),
						dataReader.GetValue(9).ToString(),
						dataReader.GetValue(10).ToString(),
						dataReader.GetValue(11).ToString(),
						dataReader.GetValue(12).ToString(),
						dataReader.GetValue(13).ToString(),
						dataReader.GetValue(14).ToString(),
						dataReader.GetValue(15).ToString(),
						dataReader.GetValue(16).ToString(),
						dataReader.GetValue(17).ToString()
						);
				}
			}
			catch (Exception error)
			{        
				Log.Write(error.Message + " [S3Agent.ProcessStatusSet]", Log.Error, 1);
			}
			finally
			{
				dbCn.Close();
			}

			return processStatusEventArgs;
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
