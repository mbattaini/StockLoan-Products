// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Anetics.Common;
using Anetics.Process;

namespace Anetics.Loanet
{
	public delegate bool ReplyHandlerDelegate(string messageId, string messageText);
	public delegate bool ActivityHandlerDelegate(string messageText);

	public class ProcessAgent : MarshalByRefObject, IProcess
	{    
		public event ProcessStatusEventHandler ProcessStatusEvent;
		public event HeartbeatEventHandler HeartbeatEvent;
     
		private string dbCnStr;    
    
		private MqSeries mqSeries;		

		private int heartbeatInterval = 20000;
		private Thread heartbeatThread = null;

		public ProcessAgent(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;

			try
			{
				heartbeatInterval = int.Parse(Standard.ConfigValue("HeartbeatInterval", "20000"));

				mqSeries = new MqSeries(
					Standard.ConfigValue("HostName"),
					Standard.ConfigValue("Channel"),
					int.Parse(Standard.ConfigValue("Port")));
    
				
			}
			catch (Exception e)
			{
				Log.Write(e.Message + "[ProcessAgent.ProcessAgent]", Log.Error, 1);
			}
		}
    
		public void Start()
		{
			if ((heartbeatThread == null) || (!heartbeatThread.IsAlive)) // Must create new thread.
			{
				heartbeatThread = new Thread(new ThreadStart(Heartbeat));
				heartbeatThread.Name = "Heartbeat";
				heartbeatThread.Start();

				Log.Write("Start command issued with new heartbeat thread. [ProcessAgent.Start]", 3);
			}
			else // Old thread will be just fine.
			{
				Log.Write("Start command issued with heartbeat thread already running. [ProcessAgent.Start]", 3);
			}

			try
			{
				mqSeries.WaitReplyStart(
					Standard.ConfigValue("ReplyQueueManagerName"),
					Standard.ConfigValue("ReplyQueueName"),
					int.Parse(Standard.ConfigValue("ReplyWaitSeconds")),
					new ReplyHandlerDelegate(ReplyHandler));

				mqSeries.WaitActivityStart(
					Standard.ConfigValue("ActivityQueueManagerName"),
					Standard.ConfigValue("ActivityQueueName"),
					int.Parse(Standard.ConfigValue("ActivityWaitSeconds")),
					new ActivityHandlerDelegate(ActivityHandler));
			}
			catch (Exception e)
			{
				Log.Write(e.Message + "[ProcessAgent.Stop]", Log.Error, 1);
			}
		}

		public void Stop()
		{
			try
			{
				mqSeries.WaitReplyStop();
				mqSeries.WaitActivityStop();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + "[ProcessAgent.Stop]", Log.Error, 1);
			}

			if (heartbeatThread == null)
			{
				Log.Write("Stop command issued, heartbeat thread never started. [ProcessAgent.Stop]", 3);
			}
			else if (heartbeatThread.ThreadState.Equals(ThreadState.WaitSleepJoin))
			{
				Log.Write("Stop command issued, heartbeat thread is sleeping. [ProcessAgent.Stop]", 3);
			}
			else
			{
				Log.Write("Stop command issued, heartbeat thread is still active. [ProcessAgent.Stop]", 3);
			}

			HeartbeatEventInvoke(new HeartbeatEventArgs(Anetics.Process.HeartbeatStatus.Stopping, ""));
		}

		private void Heartbeat()
		{
			while (!LoanetMain.IsStopped) // Loop through this block.
			{
				HeartbeatEventInvoke(new HeartbeatEventArgs(HeartbeatStatus.Normal, ""));

				if (!LoanetMain.IsStopped)
				{
					Thread.Sleep(heartbeatInterval);
				}
			}
		}

		private void HeartbeatEventInvoke(HeartbeatEventArgs heartbeatEventArgs)
		{
			HeartbeatEventHandler heartbeatEventHandler = null;

			Log.Write(heartbeatEventArgs.Status.ToString() + ": " + heartbeatEventArgs.Alert + " [ProcessAgent.HeartbeatEventInvoke]", 3);

			try
			{
				if (HeartbeatEvent == null)
				{
					Log.Write("There are no HeartbeatEvent delegates. [ProcessAgent.HeartbeatEventInvoke]", 3);
				}
				else
				{
					int n = 0;

					Delegate[] eventDelegates = HeartbeatEvent.GetInvocationList();
					Log.Write("HeartbeatEvent has " + eventDelegates.Length + " delegate[s]. [ProcessAgent.HeartbeatEventInvoke]", 3);
      
					foreach (Delegate eventDelegate in eventDelegates)
					{
						Log.Write("Invoking HeartbeatEvent delegate [" + (++n) + "]. [ProcessAgent.HeartbeatEventInvoke]", 3);
            
						try
						{
							heartbeatEventHandler = (HeartbeatEventHandler) eventDelegate;
							heartbeatEventHandler(heartbeatEventArgs);
						}
						catch (System.Net.Sockets.SocketException)
						{
							HeartbeatEvent -= heartbeatEventHandler;
							Log.Write("HeartbeatEvent delegate [" + n + "] has been removed from the invocation list. [ProcessAgent.HeartbeatEventInvoke]", 3);
						}
						catch (Exception e)
						{
							Log.Write(e.Message + " [ProcessAgent.HeartbeatEventInvoke]", Log.Error, 1);
						}
					}
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ProcessAgent.HeartbeatEventInvoke]", Log.Error, 1);
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
				Log.Write(error.Message + " [ProcessAgent.ProcessStatusSet]", Log.Error, 1);
			}
			finally
			{
				dbCn.Close();
			}

			return processStatusEventArgs;
		}
    
		private void CoOpMessageSet(string processId , string messageText)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
        
				dbCmd = new SqlCommand("spLoanetCoOpMessageInsert", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.Char, 16);			
				paramProcessId.Value = processId;

				SqlParameter paramContent = dbCmd.Parameters.Add("@Content", SqlDbType.VarChar, 500);			
				paramContent.Value = messageText;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ProcessAgent.CoOpMessageSet]", 3);       
				throw;      
			}
			finally
			{
				dbCn.Close();
			}
		}
 
		private string CoOpMessageGet(string processId)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			SqlDataReader dataReader = null;
			string content = "";

			try
			{
				dbCn = new SqlConnection(dbCnStr);
        
				dbCmd = new SqlCommand("spLoanetCoOpMessageGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.Char, 16);			
				paramProcessId.Value = processId;

				dbCn.Open();
				dataReader = dbCmd.ExecuteReader();

				while(dataReader.Read())
				{
					content = (string) dataReader.GetValue(0); // should only return one item
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ProcessAgent.CoOpMessageSet]", 3);       
				throw;      
			}
			finally
			{
				dbCn.Close();
			}

			return content;
		}

		private void BankLoanBizDateSet(string bookGroup)
		{
			if(!Master.BizDatePriorBank.Equals(KeyValue.Get("BankLoanBizDate" + bookGroup, "2001-01-01", dbCnStr)))
			{
				Master.BizDatePriorBank = KeyValue.Get("BizDatePriorBank", "2001-01-01", dbCnStr);				
			
				KeyValue.Set("BankLoanBizDate" + bookGroup, Master.BizDatePriorBank, dbCnStr);				
				KeyValue.Set("BankLoanDtcReset" + bookGroup, false.ToString(), dbCnStr);
			}
		}
		
		private string BookGroupCredentials(string bookGroup)
		{
			return (Standard.ConfigValue("LoanetCoOpUserId" + bookGroup) + Standard.ConfigValue("LoanetCoOpPassword" + bookGroup));        
		}
		
		private bool AutoMarkIsCurrent(string bookGroup)
		{
			return KeyValue.Get("LoanetMarksBizDate" + bookGroup, "0001-01-01", dbCnStr).Equals(Master.ContractsBizDate);
		}

		
		public void	AutoBorrowOrder(			
			string  bookGroup,            
			string  book,
			string  secId,						
			long    quantity,			
			string	collateralCode,
			string	comments,
			decimal	rateMin,
			string  rateMinCode,
			string  quantityMin,
			string  priceMax,
			string  waitTime,
			bool		incomeTracked,
			decimal	divRate,
			bool		bookFlag,
			string  batchCode,
			string  poolCode,
			string  marginCode,
			decimal	margin,
			string	actUserId)
		{
			ProcessStatusEventArgs processStatus = null;
			
			Lcor_Borrow_Order lcorBorrowOrder = new Lcor_Borrow_Order();
			
			processStatus = lcorBorrowOrder.Request(				
				bookGroup,            
				book,
				secId,
				quantity,
				collateralCode,	
				comments,
				rateMin,
				rateMinCode,
				quantityMin,
				priceMax,
				waitTime,
				incomeTracked,
				divRate,
				bookFlag,
				batchCode,
				poolCode,
				marginCode,
				margin,
				actUserId);
		
			if (processStatus != null)
			{
				ProcessStatusEventInvoke(ProcessStatusSet(processStatus));
				CoOpMessageSet(processStatus.ProcessId, lcorBorrowOrder.Message);
      
				try
				{
					mqSeries.Put(Standard.ConfigValue("LcorQueueManagerName", ""),
						Standard.ConfigValue("LcorQueueName", ""),
						BookGroupCredentials(bookGroup),
						processStatus.ProcessId,
						lcorBorrowOrder.Message,            
						Standard.ConfigValue("ReplyQueueManagerName", ""),
						Standard.ConfigValue("ReplyQueueName", ""));      
				
					LoanetBorrowOrderSet(bookGroup, book, secId, quantity.ToString(), "", false);
				}
				catch (Exception error)
				{
					processStatus = new ProcessStatusEventArgs(
						processStatus.ProcessId,
						processStatus.SystemCode,
						processStatus.ActCode,
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
					
					ProcessStatusEventInvoke(ProcessStatusSet(processStatus));
					LoanetBorrowOrderSet(bookGroup, book, secId, quantity.ToString(), "", true);

					Log.Write(error.Message + " [ProcessAgent.AutoBorrowOrder]", Log.Error, 3);
					throw new Exception(error.Message);
				}           
			}		
		}
		
		public void BankLoanPledgeRelease(
			string  messageId,
			string  bookGroup,            
			string  book,
			string  loanDate,
			string  recordType,
			string  secId,						
			long    quantity,			
			decimal amount,
			string  loanPurpose,
			string  releaseType,
			string  hypothecation,
			string  preventPendIndicator,
			string  cnsIndicator,
			string  ipoIndicator,
			string  ptaIndicator,
			string  occParticipantNumber,
			string  occNumber,
			string  comment,
			string  dtcInputSequence,
			string	actUserId)
		{						
			BankLoanBizDateSet(bookGroup);

			ProcessStatusEventArgs processStatus = null;
			BankLoan_Pledge_Release bankLoanPledge = new BankLoan_Pledge_Release();
			processStatus = bankLoanPledge.Request(
				messageId,
				bookGroup,            
				book,
				loanDate,
				recordType,
				secId,						
				quantity,			
				amount,
				loanPurpose,
				releaseType,
				hypothecation,
				preventPendIndicator,
				cnsIndicator,
				ipoIndicator,
				ptaIndicator,
				occParticipantNumber,
				occNumber,
				comment,
				dtcInputSequence,
				actUserId);

			if (processStatus != null)
			{
				ProcessStatusEventInvoke(ProcessStatusSet(processStatus));
				CoOpMessageSet(processStatus.ProcessId, bankLoanPledge.Message);
      
				try
				{
					mqSeries.Put(Standard.ConfigValue("ContractMaintenanceQueueManagerName", ""),
						Standard.ConfigValue("ContractMaintenanceQueueName", ""),
						BookGroupCredentials(bookGroup),
						processStatus.ProcessId,
						bankLoanPledge.Message,            
						Standard.ConfigValue("ReplyQueueManagerName", ""),
						Standard.ConfigValue("ReplyQueueName", ""));      
				}
				catch (Exception error)
				{
					processStatus = new ProcessStatusEventArgs(
						processStatus.ProcessId,
						processStatus.SystemCode,
						processStatus.ActCode,
						"",
						"",
						"",
						true,
						bookGroup,
						"",
						"",
						book,
						secId,
						"",
						quantity.ToString(),
						amount.ToString(), 
						error.Message,
						"",
						"");

					LoanetBankLoanSet(bookGroup, messageId, true);

					ProcessStatusEventInvoke(ProcessStatusSet(processStatus));

					Log.Write(error.Message + " [ProcessAgent.BankLoanPledge]", Log.Error, 3);
					throw new Exception(error.Message);
				}              
			}
		}
    
		public void ContractAdd(
			string  dealId,
			string  bookGroup,
			string  dealType,
			string  secId,
			string  book,
			long    quantity,
			decimal amount,      
			string  collateralCode,
			string  expiryDate,
			decimal rate,
			string  rateCode,
			string  poolCode,
			string  marginCode,
			decimal margin,
			decimal negotiatedNewRate,
			string  comment,
			string  otherBook,
			decimal fixedInvesmtmentRate,
			bool    incomeTracked,
			decimal divRate,
			string  actUserId)
		{
			ProcessStatusEventArgs processStatus = null;
			NewContract_SameDay_Add newContract = new NewContract_SameDay_Add();
      
			string batchCode = "";
			string deliveryCode = "D";
      
			if (dealType.Equals("B"))
			{
				batchCode = KeyValue.Get("LoanetBatchCodeBorrows", "A", dbCnStr);
				deliveryCode = "D";
			}
			else if (dealType.Equals("L"))
			{
				batchCode = KeyValue.Get("LoanetBatchCodeLoans", "B", dbCnStr);
				deliveryCode = "0"; // Zero.
			}     
            
			processStatus = newContract.Request(
				dealId,
				bookGroup,
				dealType,
				secId,
				book,
				deliveryCode,
				quantity,
				amount,
				batchCode,
				collateralCode,
				expiryDate,
				rate,
				rateCode,
				poolCode,
				marginCode,
				margin,
				negotiatedNewRate,
				comment,
				otherBook,
				fixedInvesmtmentRate,
				incomeTracked,
				divRate,
				actUserId);

			if (processStatus != null)
			{
				ProcessStatusEventInvoke(ProcessStatusSet(processStatus));
				CoOpMessageSet(processStatus.ProcessId, newContract.Message);
      
				try
				{
					mqSeries.Put(Standard.ConfigValue("ContractAddQueueManagerName", ""),
						Standard.ConfigValue("ContractAddQueueName", ""),
						BookGroupCredentials(bookGroup),
						processStatus.ProcessId,
						newContract.Message,            
						Standard.ConfigValue("ReplyQueueManagerName", ""),
						Standard.ConfigValue("ReplyQueueName", ""));      
				}
				catch (Exception error)
				{
					processStatus = new ProcessStatusEventArgs(
						processStatus.ProcessId,
						processStatus.SystemCode,
						processStatus.ActCode,
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

					ProcessStatusEventInvoke(ProcessStatusSet(processStatus));

					Log.Write(error.Message + " [ProcessAgent.Contract]", Log.Error, 3);
					throw new Exception(error.Message);
				}              
			}
		}

		public void ContractAdd(
			string   dealId,
			string   bookGroup,
			string   dealType,
			string   secId,
			string   book,      
			long     quantity,
			decimal  amount,      
			string   collateralCode,
			string   expiryDate,
			decimal  rate,
			string   rateCode,
			string   poolCode,
			string   marginCode,
			decimal  margin,
			decimal  negotiatedNewRate,
			string   comment,
			string   otherBook,
			decimal  fixedInvesmtmentRate,
			string   deliveryLocation,
			string   deliveryDate,
			bool     incomeTracked,
			decimal  divRate,
			bool     divCallable,
			string   currencyIso,
			string   cashDepot,
			string   exchange,
			string   actUserId)
		{
			ProcessStatusEventArgs processStatus = null;

			NewContract_International_Add newContract = new NewContract_International_Add();

			string batchCode = "";
			string deliveryCode = "D";
      
			if (dealType.Equals("B"))
			{
				batchCode = KeyValue.Get("LoanetBatchCodeBorrows", "A", dbCnStr);
				deliveryCode = "D";
			}
			else if (dealType.Equals("L"))
			{
				batchCode = KeyValue.Get("LoanetBatchCodeLoans", "B", dbCnStr);
				deliveryCode = "0"; // Zero.
			}     

			processStatus = newContract.Request(
				dealId,
				bookGroup,
				dealType,
				secId,
				book,
				deliveryCode,
				quantity,
				amount,
				batchCode,
				collateralCode,
				expiryDate,
				rate,
				rateCode,
				poolCode,
				marginCode,
				margin,
				negotiatedNewRate,
				comment,
				otherBook,
				fixedInvesmtmentRate,
				deliveryLocation,
				deliveryDate,
				incomeTracked,
				divRate,
				divCallable,
				currencyIso,
				cashDepot,
				exchange,
				actUserId);      
      
			if (processStatus != null)
			{
				ProcessStatusEventInvoke(ProcessStatusSet(processStatus));
				CoOpMessageSet(processStatus.ProcessId, newContract.Message);
      
				try
				{
					mqSeries.Put(Standard.ConfigValue("ContractAddQueueManagerName", ""),
						Standard.ConfigValue("ContractAddQueueName", ""),
						BookGroupCredentials(bookGroup),
						processStatus.ProcessId,
						newContract.Message,           
						Standard.ConfigValue("ReplyQueueManagerName", ""),
						Standard.ConfigValue("ReplyQueueName", ""));      
				}
				catch (Exception error)
				{
					processStatus = new ProcessStatusEventArgs(
						processStatus.ProcessId,
						processStatus.SystemCode,
						processStatus.ActCode,
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

					ProcessStatusEventInvoke(ProcessStatusSet(processStatus));

					Log.Write(error.Message + " [ProcessAgent.Contract]", Log.Error, 3);
					throw new Exception(error.Message);
				}              
			}
		}

		public void RateChange( 
			string  bookGroup,
			string  contractType,
			string  book,
			string  securityType,
			string  contractId,
			decimal rateOld,
			string  rateCodeOld,
			decimal rateNew,
			string  rateCodeNew,            
			string  poolCode,
			string  effectiveDate,
			string  actUserId)
		{            
			ProcessStatusEventArgs processStatus = null;
      
			string message = "";           
			string deliveryCode = "D";
      
			if (!securityType.Equals(""))
			{
				Rate_Change_BoxRate newBoxRate = new Rate_Change_BoxRate();
				processStatus =  newBoxRate.Request(
					bookGroup,
					contractType,
					book,
					securityType,
					rateNew,
					actUserId);
        
				message = newBoxRate.Message;
			}
			else if (!rateCodeOld.Equals("T") && rateCodeNew.Equals("T"))
			{
				Rate_Change_NegotiatedToBox negotiatedToBox = new Rate_Change_NegotiatedToBox();
				processStatus = negotiatedToBox.Request(bookGroup,
					contractType,
					book,
					contractId,
					actUserId);

				message = negotiatedToBox.Message;
			}
			else if (rateCodeOld.Equals("T") && !rateCodeNew.Equals("T") && ((rateOld * rateNew) >= 0) )
			{
				Rate_Change_BoxToNegotiated boxToNegotiated = new Rate_Change_BoxToNegotiated();
				processStatus = boxToNegotiated.Request(
					bookGroup,
					contractType,
					book,
					contractId,
					rateNew,
					actUserId);
      
				message = boxToNegotiated.Message;
			}      
			else if ((rateCodeOld.Equals("N") && !rateCodeNew.Equals("N")) || (!rateCodeOld.Equals("N") && rateCodeNew.Equals("N")))
			{                
				string batchCode = KeyValue.Get("LoanetBatchCodeContractAdj", "Z", dbCnStr);                      
        
				Rate_Change_PositiveNegative posNegRate = new Rate_Change_PositiveNegative();
				processStatus = posNegRate.Request(
					bookGroup,
					contractType,
					contractId,
					rateCodeNew,
					rateNew,
					batchCode,
					deliveryCode,
					poolCode,
					actUserId);

				message = posNegRate.Message;
			}
			else
			{
				string bizDate = Master.BizDate;

				Rate_Change_NegotiatedRate negotiatedRate = new Rate_Change_NegotiatedRate();
				processStatus = negotiatedRate.Request(
					bookGroup,
					contractType,
					book,
					contractId,
					rateNew,
					effectiveDate,
					bizDate,
					actUserId);
        
				message = negotiatedRate.Message;
			}
    
      
			if (processStatus != null)
			{       
				ProcessStatusEventInvoke(ProcessStatusSet(processStatus));
				CoOpMessageSet(processStatus.ProcessId, message);
        
				try
				{
					mqSeries.Put(Standard.ConfigValue("ContractMaintenanceQueueManagerName", ""),
						Standard.ConfigValue("ContractMaintenanceQueueName", ""),
						BookGroupCredentials(bookGroup),
						processStatus.ProcessId,
						message,
						Standard.ConfigValue("ReplyQueueManagerName", ""),
						Standard.ConfigValue("ReplyQueueName", ""));      
				}
				catch (Exception error)
				{
					processStatus = new ProcessStatusEventArgs(
						processStatus.ProcessId,
						processStatus.SystemCode,
						processStatus.ActCode,
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

					ProcessStatusEventInvoke(ProcessStatusSet(processStatus));

					Log.Write(error.Message + " [ProcessAgent.RateChange]", Log.Error, 3);
					throw new Exception(error.Message);
				}              
			}
		}
    
		public void Return(
			string  bookGroup,
			string  contractType,
			string  contractId,
			long    returnQuantity,            
			decimal returnAmount,
			string  callbackRequired,
			string  recDelLocation,
			string  cashDepot,
			string  actUserId)
		{
			FullPartial_Return_AmountCallback returnRequest = new FullPartial_Return_AmountCallback();            
      
			string batchCode = "";
			string deliveryCode = "D";

			if (contractType.Equals("B"))
			{
				batchCode = KeyValue.Get("LoanetBatchCodeReturnsBorrows", "3", dbCnStr);                         
				deliveryCode = "0";
			}      
			else if (contractType.Equals("L"))
			{
				batchCode = KeyValue.Get("LoanetBatchCodeReturnsLoans", "4", dbCnStr);
				deliveryCode = "D"; // Zero.      
			}  
      
			callbackRequired = KeyValue.Get("LoanetCallbackRequiredCode", "L", dbCnStr);

			ProcessStatusEventArgs processStatus = returnRequest.Request(
				bookGroup, 
				contractType, 
				contractId, 
				returnQuantity, 
				batchCode, 
				deliveryCode, 
				returnAmount, 
				callbackRequired, 
				recDelLocation, 
				cashDepot,
				actUserId);

			if (processStatus != null)
			{
				ProcessStatusEventInvoke(ProcessStatusSet(processStatus));
				CoOpMessageSet(processStatus.ProcessId, returnRequest.Message);
      
				try
				{
					mqSeries.Put(Standard.ConfigValue("ReturnQueueManagerName", ""),
						Standard.ConfigValue("ReturnQueueName", ""),
						BookGroupCredentials(bookGroup),
						processStatus.ProcessId,
						returnRequest.Message,
						Standard.ConfigValue("ReplyQueueManagerName", ""),
						Standard.ConfigValue("ReplyQueueName", ""));      
				}
				catch (Exception error)
				{
					processStatus = new ProcessStatusEventArgs(
						processStatus.ProcessId,
						processStatus.SystemCode,
						processStatus.ActCode,
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

					ProcessStatusEventInvoke(ProcessStatusSet(processStatus));

					Log.Write(error.Message + " [ProcessAgent.Return]", Log.Error, 3);
					throw new Exception(error.Message);
				} 
			}     
		}

		public void ContractMaintenance(
			string bookGroup,
			string contractId,
			string contractType,
			string book,      
			string poolCode,
			string effectiveDate,
			string deliveryDate,
			string marginCode,
			string margin,
			string divRate,
			string incomeTracked,
			string expiryDate,
			string comment,
			string actUserId)
		{
			ProcessStatusEventArgs processStatus = null;
			string message = "";

			if (!poolCode.Equals(""))
			{
				ProfitCenter_Change profitCenterChange = new ProfitCenter_Change();
      
				string batchCode = KeyValue.Get("LoanetBatchCodeContractAdj", "Z", dbCnStr);

				processStatus = profitCenterChange.Request(
					bookGroup,
					contractType,
					book,
					contractId,
					poolCode,
					batchCode,
					effectiveDate,
					actUserId);
        
				message = profitCenterChange.Message;
			}
      
			if (processStatus != null)
			{              
				ProcessStatusEventInvoke(ProcessStatusSet(processStatus));
				CoOpMessageSet(processStatus.ProcessId, message);
        
				try
				{
					mqSeries.Put(Standard.ConfigValue("ContractMaintenanceQueueManagerName", ""),
						Standard.ConfigValue("ContractMaintenanceQueueName", ""),
						BookGroupCredentials(bookGroup),
						processStatus.ProcessId,
						message,
						Standard.ConfigValue("ReplyQueueManagerName", ""),
						Standard.ConfigValue("ReplyQueueName", ""));       
				}
				catch (Exception error)
				{
					processStatus = new ProcessStatusEventArgs(
						processStatus.ProcessId,
						processStatus.SystemCode,
						processStatus.ActCode,
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

					ProcessStatusEventInvoke(ProcessStatusSet(processStatus));

					Log.Write(error.Message + " [ProcessAgent.ContractMaintenance]", Log.Error, 3);
					throw new Exception(error.Message);
				}            
			}
		}
    
		public void Recall(
			string bookGroup,
			string contractType,
			string book,
			string contractId,
			string recallDate,
			int    recallSequence,
			long   recallQuantity,
			string buyinDate,
			string zeroInterest,
			string terminationIndicator,
			string recallReasonCode,
			string recallId,
			string comment,
			string actUserId,
			bool   delete)
		{
			ProcessStatusEventArgs processStatus = null;
			string message;

			if (delete)
			{
				Recall_Delete recallDelete = new Recall_Delete();

				processStatus = recallDelete.Request(
					bookGroup,
					contractType,
					book,
					contractId,
					recallDate,
					recallSequence,
					recallId,
					comment,
					actUserId);

				message = recallDelete.Message;
			}
			else
			{
				Recall_Add recallAdd = new Recall_Add();

				processStatus = recallAdd.Request(
					bookGroup,
					contractType,
					book,
					contractId,
					recallDate,
					recallQuantity,
					buyinDate,
					zeroInterest,
					terminationIndicator,
					recallReasonCode,
					recallId,
					comment,
					actUserId);

				message = recallAdd.Message;
			}
    
			if (processStatus != null)
			{
				ProcessStatusEventInvoke(ProcessStatusSet(processStatus));
				CoOpMessageSet(processStatus.ProcessId, message);
        
				try
				{
					mqSeries.Put(Standard.ConfigValue("RecallQueueManagerName", ""),
						Standard.ConfigValue("RecallQueueName", ""),
						BookGroupCredentials(bookGroup),
						processStatus.ProcessId,
						message,
						Standard.ConfigValue("ReplyQueueManagerName", ""),
						Standard.ConfigValue("ReplyQueueName", ""));              
				}
				catch (Exception error)
				{
					processStatus = new ProcessStatusEventArgs(
						processStatus.ProcessId,
						processStatus.SystemCode,
						processStatus.ActCode,
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

					ProcessStatusEventInvoke(ProcessStatusSet(processStatus));

					Log.Write(error.Message + " [ProcessAgent.Recall]", Log.Error, 1);
					throw new Exception(error.Message);
				}
			}
		}


		public void MemoSegUpdate(
			string  messageId,
			string  bookGroup,            
			string  secId,
			long		quantity,
			string	actionCode,
			string	dtcSerialNumber,
			string	dtcInputSequence,
			string	comments,
			string  actUserId)
		{
			ProcessStatusEventArgs processStatus = null;
			string message;

			Memo_Seg_Update memoSegUpdate = new Memo_Seg_Update();

			processStatus = memoSegUpdate.Request(
				messageId,
				bookGroup,            
				secId,
				quantity,	
				actionCode,
				dtcSerialNumber,
				dtcInputSequence,
				comments,
				actUserId);

			message = memoSegUpdate.Message;
    
			if (processStatus != null)
			{
				ProcessStatusEventInvoke(ProcessStatusSet(processStatus));
				CoOpMessageSet(processStatus.ProcessId, message);
        
				try
				{
					mqSeries.Put(Standard.ConfigValue("ContractMaintenanceQueueManagerName", ""),
						Standard.ConfigValue("ContractMaintenanceQueueName", ""),
						BookGroupCredentials(bookGroup),
						processStatus.ProcessId,
						message,
						Standard.ConfigValue("ReplyQueueManagerName", ""),
						Standard.ConfigValue("ReplyQueueName", ""));              
				}
				catch (Exception error)
				{
					processStatus = new ProcessStatusEventArgs(
						processStatus.ProcessId,
						processStatus.SystemCode,
						processStatus.ActCode,
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

					ProcessStatusEventInvoke(ProcessStatusSet(processStatus));

					Log.Write(error.Message + " [ProcessAgent.Recall]", Log.Error, 1);
					throw new Exception(error.Message);
				}
			}
		}

		private bool ReplyHandler(string messageId, string messageText)
		{
			Log.Write(messageId + ": " + messageText + " [ProcessAgent.ReplyHandler]", 3);
     
			ProcessStatusEventArgs processStatusEventArgs = null;     
     
			messageId = messageId.TrimEnd('\0', ' ');

			try
			{
				switch(messageText.Substring(0, 2))
				{            
					case "06":  
						NewContract_SameDay_Add contractAdd = new NewContract_SameDay_Add();
						processStatusEventArgs = contractAdd.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);
            
						try
						{
							LoanetContractSet(processStatusEventArgs.BookGroup, processStatusEventArgs.ProcessId, processStatusEventArgs.ContractId, processStatusEventArgs.ContractType, processStatusEventArgs.HasError);              
						}
						catch (Exception contractError)
						{
							Log.Write(contractError.Message + ". [ProcessAgent.ReplyHandler.NewContract_SameDay_Add]", 1);
						}
						break;

					case "10":  
						NewContract_International_Add internationalContractAdd = new NewContract_International_Add();
						processStatusEventArgs = internationalContractAdd.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);  
            
						try
						{
							LoanetContractSet(processStatusEventArgs.BookGroup, processStatusEventArgs.ProcessId, processStatusEventArgs.ContractId, processStatusEventArgs.ContractType, processStatusEventArgs.HasError);             
						}
						catch (Exception contractInternationalError)
						{
							Log.Write(contractInternationalError.Message + ". [ProcessAgent.ReplyHandler.NewContract_International_Add]", 1);
						}
						break;

					case "26":  
						FullPartial_Return_AmountCallback returnAdd = new FullPartial_Return_AmountCallback();
						processStatusEventArgs = returnAdd.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);
						break;

					case "42":  
						ProfitCenter_Change profitCenterChange = new ProfitCenter_Change();
						processStatusEventArgs = profitCenterChange.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);
						break;

					case "72":  
						Rate_Change_NegotiatedRate negotiatedRateChange = new Rate_Change_NegotiatedRate();
						processStatusEventArgs = negotiatedRateChange.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);
						break;

					case "74":  
						Rate_Change_NegotiatedToBox negotiatedToBoxChange = new Rate_Change_NegotiatedToBox();
						processStatusEventArgs = negotiatedToBoxChange.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);
						break;

					case "76":  
						Rate_Change_BoxToNegotiated boxToNegotiatedChange = new Rate_Change_BoxToNegotiated();
						processStatusEventArgs = boxToNegotiatedChange.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);
						break;

					case "78":  
						Rate_Change_PositiveNegative posNegRateChange = new Rate_Change_PositiveNegative();
						processStatusEventArgs = posNegRateChange.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);
						break;

					case "80":  
						Rate_Change_BoxRate boxRateChange = new Rate_Change_BoxRate();
						processStatusEventArgs = boxRateChange.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);
						break;
					
					case "P2":
						BankLoan_Pledge_Release bankLoanAcitivty = new BankLoan_Pledge_Release();
						processStatusEventArgs = bankLoanAcitivty.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);
						
						try
						{
							LoanetBankLoanSet(processStatusEventArgs.BookGroup, processStatusEventArgs.Tag, processStatusEventArgs.HasError);              
						}
						catch (Exception error)
						{
							Log.Write(error.Message + ". [ProcessAgent.ReplyHandler.BankLoan_Pledge_Release]", 1);
						}
						break;

					case "C4":  
						Recall_Add recallAdd = new Recall_Add();
						processStatusEventArgs = recallAdd.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);
            
						try
						{
							LoanetRecallSet(processStatusEventArgs.Tag, Tools.SplitItem(processStatusEventArgs.Tag, "|", 1), Tools.SplitItem(processStatusEventArgs.Tag, "|", 2), processStatusEventArgs.HasError);            							
						}
						catch (Exception recallError)
						{
							Log.Write(recallError.Message + " [ProcessAgent.ReplyHandler.Recall_Add]", 1);
						}
						break;

					case "C8":  
						Recall_Delete recallDelete = new Recall_Delete();
						processStatusEventArgs = recallDelete.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);

						try
						{
							LoanetRecallSet("", "", processStatusEventArgs.Tag.Trim(), processStatusEventArgs.HasError);            
						}
						catch (Exception recallError)
						{
							Log.Write(recallError.Message + " [ProcessAgent.ReplyHandler.Recall_Delete]", 1);
						}
						break;

					case "M2":  
						Memo_Seg_Update memoSegUpdate = new Memo_Seg_Update();
						processStatusEventArgs = memoSegUpdate.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);
						break;
     
					case "XC":
						Lcor_Borrow_Order borrowOrder = new Lcor_Borrow_Order();
						processStatusEventArgs = borrowOrder.Reply(messageId, messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);
						try
						{
							LoanetBorrowOrderSet(processStatusEventArgs.BookGroup, processStatusEventArgs.Book, processStatusEventArgs.SecId, processStatusEventArgs.Quantity, processStatusEventArgs.Tag, processStatusEventArgs.HasError);
						}
						catch (Exception borrowError)
						{
							Log.Write(borrowError.Message + " [ProcessAgent.ReplyHandler.Lcor_Borrow_Order]", 1);
						}
						break;

					case "ZZ": 
						General_System_Reject generalSystemReject = new General_System_Reject(dbCnStr);
						processStatusEventArgs = generalSystemReject.Activity(messageText);
						processStatusEventArgs = ProcessStatusSet(processStatusEventArgs);
						break;                                      
				}
   
				if (processStatusEventArgs != null)
				{                
					ProcessStatusEventInvoke(processStatusEventArgs);
					CoOpMessageSet(processStatusEventArgs.ProcessId, messageText + " " + processStatusEventArgs.Status);    
          
					if (processStatusEventArgs.HasError && processStatusEventArgs.Status.IndexOf(Errors.CCF_ERROR) > -1)
					{
						string requestMessage = CoOpMessageGet(messageId);
            
						switch(messageText.Substring(0, 2))
						{                
							case "06":
							case "10":
								try
								{
									
									requestMessage = requestMessage.Remove(24, 1);
									
									if (requestMessage.Substring(6, 1).Equals("B"))
									{
										requestMessage = requestMessage.Insert(24, KeyValue.Get("LoanetBatchCodeBorrowsPts", "H", dbCnStr));
									}
									else
									{
										requestMessage = requestMessage.Insert(24, KeyValue.Get("LoanetBatchCodeLoansPts", "H", dbCnStr));
									}

									requestMessage = requestMessage.Remove(25, 1);
									requestMessage = requestMessage.Insert(25, "D");
								
									mqSeries.Put(Standard.ConfigValue("ContractAddQueueManagerName", ""),
										Standard.ConfigValue("ContractAddQueueName", ""),
										BookGroupCredentials(requestMessage.Substring(2, 4)),
										messageId,
										requestMessage,
										Standard.ConfigValue("ReplyQueueManagerName", ""),
										Standard.ConfigValue("ReplyQueueName", ""));
                
									processStatusEventArgs = new ProcessStatusEventArgs(
										processStatusEventArgs.ProcessId,
										processStatusEventArgs.SystemCode,
										processStatusEventArgs.ActCode,
										"PTS " + processStatusEventArgs.Act,
										"",
										"",
										false,
										"",
										"",
										"",
										"",
										"",
										"",
										"",
										"", 
										"",
										"",
										"");
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
								}

								ProcessStatusEventInvoke(ProcessStatusSet(processStatusEventArgs));              
								break;

							case "26":
								try
								{ 
									requestMessage = requestMessage.Remove(25, 1);
									
									if (requestMessage.Substring(6, 1).Equals("B"))
									{																		
										requestMessage = requestMessage.Insert(25, KeyValue.Get("LoanetBatchCodeReturnsBorrowsPts", "J", dbCnStr));
									}
									else
									{									
										requestMessage = requestMessage.Insert(25, KeyValue.Get("LoanetBatchCodeReturnsLoansPts", "J", dbCnStr));
									}  
																									
									requestMessage = requestMessage.Remove(26, 1);
									requestMessage = requestMessage.Insert(26, "D");                  
									
									mqSeries.Put(Standard.ConfigValue("ReturnQueueManagerName", ""),
										Standard.ConfigValue("ReturnQueueName", ""),
										BookGroupCredentials(requestMessage.Substring(2, 4)),
										messageId,
										requestMessage,
										Standard.ConfigValue("ReplyQueueManagerName", ""),
										Standard.ConfigValue("ReplyQueueName", ""));                 
                  
									processStatusEventArgs = new ProcessStatusEventArgs(
										processStatusEventArgs.ProcessId,
										processStatusEventArgs.SystemCode,
										processStatusEventArgs.ActCode,
										"PTS " + processStatusEventArgs.Act,
										"",
										"",
										false,
										"",
										"",
										"",
										"",
										"",
										"",
										"",
										"", 
										"",
										"",
										"");
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
								}

								ProcessStatusEventInvoke(ProcessStatusSet(processStatusEventArgs));        
								break;

							default:
								Log.Write("Unanticipated message type past CCF cutoff: " + requestMessage.Substring(0, 2) + " [ProcessAgent.ReplyHandler]", Log.Error, 1);
								break;
						}                                               
					}           
					return true;
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ProcessAgent.ReplyHandler]", 1);
				Log.Write(messageText + " [ProcessAgent.ReplyHandler]", 1);
			}

			return (false || (bool.Parse(KeyValue.Get("LoanetCoOpBinReplyOnError", "False", dbCnStr))));
		}
    
		private bool ActivityHandler(string messageText)
		{            
			ProcessStatusEventArgs processStatusEventArgs = null;
			ProcessStatusEventArgs[] processStatusEventArgsArray = null;      
      
			Log.Write(messageText + " [ProcessAgent.ActivityHandler]", 3);            
      
			try
			{
				if (!AutoMarkIsCurrent(messageText.Substring(12, 4)))
				{
					Log.Write("Auto marks [LAMS] have not been applied for this book group. [ProcessAgent.ActivityHandler]", Log.Error, 1);
					return false;            
				}

				CoOpMessageSet(Standard.ProcessId(), messageText);
        
				switch(messageText.Substring(0, 2))
				{          
					case "R2" : 
						Debit_Data_Datagram debitDatagram = new Debit_Data_Datagram(dbCnStr);
						processStatusEventArgs = debitDatagram.Activity(messageText); 
            
						if (processStatusEventArgs.ActCode.Equals("DBT"))
						{
							LoanetRecallDebitUpdate(processStatusEventArgs, long.Parse(processStatusEventArgs.Tag.Trim()));
						}
						break; 
		
					case "P4":
						BankLoan_Pledge_Release_Datagram bankLoanDatagram = new BankLoan_Pledge_Release_Datagram(dbCnStr);
						processStatusEventArgs = bankLoanDatagram.Activity(messageText);
						BankLoanBizDateSet(processStatusEventArgs.BookGroup);
						break;
 
					case "T4" : 
						Transaction_Data_Datagram transactionDatagram = new Transaction_Data_Datagram(dbCnStr);
						processStatusEventArgsArray = transactionDatagram.Activity(messageText);                       
            
						if(messageText.Substring(70, 11).Trim().Equals("Closed"))
						{
							LoanetContractRecallUpdate(processStatusEventArgsArray[0]);
						}
						break;

					case "M4":
						Memo_Seg_Update_Datagram memoSegUpdateDatagram =  new Memo_Seg_Update_Datagram();
						processStatusEventArgs = memoSegUpdateDatagram.Activity(messageText);

						if (processStatusEventArgs.Tag.Equals("M") && Master.IsS3Active)
						{
							LoanetS3MemoSegUpdateSet(Master.BizDate, processStatusEventArgs.SecId, processStatusEventArgs.Quantity);	
						}
						break;					
			  
					case "XD":
						Lcor_Borrow_Order_Executed_Datagram borrowOrderExecutedDatagram = new Lcor_Borrow_Order_Executed_Datagram(dbCnStr);
						processStatusEventArgs = borrowOrderExecutedDatagram.Activity(messageText);
						break;
					
					case "XE":
						Lcor_Borrow_Order_Unexecuted_Datagram borrowOrderUnexecutedDatagram =  new Lcor_Borrow_Order_Unexecuted_Datagram(dbCnStr);
						processStatusEventArgs = borrowOrderUnexecutedDatagram.Activity(messageText);
						break;
					
					default : 
						Log.Write("Unanticipated message type: " +  messageText.Substring(0, 2) + ". [ProcessAgent.ActivityHandler]", Log.Error, 1);                    
						return true;
				}

				if (processStatusEventArgs != null)
				{                        
					Log.Write("Handling: " + processStatusEventArgs.Act + " [ProcessAgent.ActivityHandler]", 3);            
					ProcessStatusEventInvoke(ProcessStatusSet(processStatusEventArgs));          
				}
                
				if (processStatusEventArgsArray != null)
				{
					foreach (ProcessStatusEventArgs processStatusEventArgsItem in processStatusEventArgsArray)
					{
						Log.Write("Handling: " + processStatusEventArgsItem.Act + " [ProcessAgent.ActivityHandler]", 3);            
						ProcessStatusEventInvoke(ProcessStatusSet(processStatusEventArgsItem));            
					}
				}

				return true;
			}
			catch (SqlException e)
			{
				Log.Write(e.Message + " [ProcessAgent.ActivityHandler]", Log.Error, 1);    

				if (e.Class.Equals((byte)11) && e.State.Equals((byte)100)) // OK to continue processsing.
				{
					return true;
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + " [ProcessAgent.ActivityHandler]", Log.Error, 1);    
			}
     
			return (false || (bool.Parse(KeyValue.Get("LoanetCoOpBinActivityOnError", "False", dbCnStr))));
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}
  
		private void ProcessStatusEventInvoke(ProcessStatusEventArgs processStatusEventArgs)
		{
			ProcessStatusEventHandler processStatusEventHandler = null;
      
			string processStatusIdentifier =  processStatusEventArgs.ProcessId
				+ "|" + processStatusEventArgs.ActCode + "|" + processStatusEventArgs.SystemCode;

			try
			{
				if (ProcessStatusEvent == null)
				{
					Log.Write("Handling a process status event for " + processStatusIdentifier + " with no delegates. [ProcessAgent.ProcessStatusEventInvoke]", 2);
				}
				else
				{
					int n = 0;
					Delegate[] eventDelegates = ProcessStatusEvent.GetInvocationList();

					Log.Write("Handling a process status event for " + processStatusIdentifier + " with " + eventDelegates.Length + " delegates. [ProcessAgent.ProcessStatusEventInvoke]", 2);
          
					foreach (Delegate eventDelegate in eventDelegates)
					{
						Log.Write("Invoking delegate [" + (++n) + "]. [ProcessAgent.ProcessStatusEventInvoke]", 3);

						try
						{
							processStatusEventHandler = (ProcessStatusEventHandler) eventDelegate;
							processStatusEventHandler(processStatusEventArgs);
						}
						catch (System.Net.Sockets.SocketException)
						{
							ProcessStatusEvent -= processStatusEventHandler;
							Log.Write("Process status event delegate [" + n + "] has been removed from the invocation list. [ProcessAgent.ProcessStatusEventInvoke]", 3);
						}
						catch (Exception e)
						{
							Log.Write(e.Message + " [ProcessAgent.ProcessStatusEventInvoke]", Log.Error, 1);
						}
					}

					Log.Write("Done invoking the process status event invocation list. [ProcessAgent.ProcessStatusEventInvoke]", 3);
				}
			}
			catch (Exception e)
			{
				Log.Write(e.Message + ". [ProcessAgent.ProcessStatusEventInvoke]", Log.Error, 1);
			}
		}
		private void LoanetBorrowOrderSet(string bookGroup, string book, string secId, string quantity, string serialId, bool isError)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			string status = "";

			if (isError)
			{
				status = "F";
			}
			else
			{
				status = "A";
			}
			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spLoanetLcorBorrowOrderSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.Char, 4);			
				paramBookGroup.Value = bookGroup;

				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.Char, 4);			
				paramBook.Value = book;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.Char, 9);			
				paramSecId.Value = secId;

				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);			
				paramQuantity.Value = quantity;

				if (!serialId.Equals(""))
				{
					SqlParameter paramSerialId = dbCmd.Parameters.Add("@SerialId", SqlDbType.Char, 6);			
					paramSerialId.Value = serialId;
				}

				SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.Char, 1);			
				paramStatus.Value = status;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [ProcessAgent.LoanetBankLoanSet]", 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}

		}

		private void LoanetBankLoanSet(string bookGroup, string processId, bool isError)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			string status = "";

			if (isError)
			{
				status = "F";
			}
			else
			{
				status = "A";
			}
      
			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spLoanetBankLoanSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 4);			
				paramBookGroup.Value = bookGroup;

				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.Char, 16);			
				paramProcessId.Value = processId;

				SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.Char, 1);			
				paramStatus.Value = status;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [ProcessAgent.LoanetBankLoanSet]", 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}
		
		private void LoanetContractSet(string bookGroup, string dealId, string contractId, string contractType, bool isError)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			string status = "";

			if (isError)
			{
				status = "F";
			}
			else
			{
				status = "A";
			}
      
			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spLoanetContractSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramDealId = dbCmd.Parameters.Add("@DealId", SqlDbType.Char, 16);			
				paramDealId.Value = dealId;

				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 4);			
				paramBookGroup.Value = bookGroup;

				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.Char, 9);			
				paramContractId.Value = contractId;

				SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);			
				paramContractType.Value = contractType;

				SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.Char, 1);			
				paramStatus.Value = status;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [ProcessAgent.LoanetContractSet]", 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}

		public void LoanetRecallSet(string temporaryLenderReference, string recallSeqeunce, string lenderReference, bool isError)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;
			string status = "";

			if (isError)
			{
				status = "F";
			}
			else
			{
				status = "A";
			}
      
			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spLoanetRecallSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
       
				if (!temporaryLenderReference.Equals(""))
				{
					SqlParameter paramTemporaryRecallId = dbCmd.Parameters.Add("@TemporaryRecallId", SqlDbType.Char, 16);			
					paramTemporaryRecallId.Value = temporaryLenderReference.Trim();
				}

				if (!lenderReference.Equals(""))
				{
					SqlParameter paramLoanetRecallId = dbCmd.Parameters.Add("@LoanetRecallId", SqlDbType.Char, 16);			
					paramLoanetRecallId.Value = lenderReference;
				}

				if (!recallSeqeunce.Equals(""))
				{
					SqlParameter paramRecallSequence = dbCmd.Parameters.Add("@RecallSequence", SqlDbType.SmallInt);			
					paramRecallSequence.Value = recallSeqeunce;
				}

				SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.Char, 1);			
				paramStatus.Value = status;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [ProcessAgent.LoanetRecallSet]", 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}
    
		private void LoanetRecallDebitUpdate(ProcessStatusEventArgs debitProcessStatusEventArgs, long quantity)
		{      
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spLoanetRecallDebitUpdate", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
       
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 4);			
				paramBookGroup.Value = debitProcessStatusEventArgs.BookGroup;      

				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.Char, 9);			
				paramContractId.Value = debitProcessStatusEventArgs.ContractId;

				SqlParameter paramQuantity = dbCmd.Parameters.Add("@DeliveredQuantity", SqlDbType.BigInt);			
				paramQuantity.Direction = ParameterDirection.InputOutput;
				paramQuantity.Value = quantity;

				SqlParameter paramRecallId = dbCmd.Parameters.Add("@RecallId", SqlDbType.Char , 16);			
				paramRecallId.Direction = ParameterDirection.Output;
				paramRecallId.Value = "tenp";
        
				SqlParameter paramStatus = dbCmd.Parameters.Add("@Status", SqlDbType.VarChar, 255);			
				paramStatus.Direction = ParameterDirection.Output;
        
				dbCn.Open();

				while ((long)paramQuantity.Value > 0 )
				{
					paramQuantity.Value  = quantity;
					dbCmd.ExecuteNonQuery();          
        
					quantity = (long)paramQuantity.Value;      
          
					if (paramRecallId.Value.ToString().Equals(""))
					{
						break;
					}
          
					ProcessStatusEventArgs processStatusEventArgs = new ProcessStatusEventArgs(
						Standard.ProcessId(),
						"L",
						"DBR",
						paramStatus.Value.ToString(),
						"",
						"",
						false,
						debitProcessStatusEventArgs.BookGroup,
						debitProcessStatusEventArgs.ContractId,
						debitProcessStatusEventArgs.ContractType,
						debitProcessStatusEventArgs.Book,
						debitProcessStatusEventArgs.SecId,
						"",
						"",
						"",
						"",
						"",
						paramRecallId.Value.ToString());
                                        
					ProcessStatusEventInvoke(ProcessStatusSet(processStatusEventArgs));
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [ProcessAgent.LoanetRecallDebitUpdate]", 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}
  
		private void LoanetContractRecallUpdate(ProcessStatusEventArgs contractProcessStatusEventArgs)
		{      
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spLoanetContractRecallUpdate", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
       
				SqlParameter paramBookGroup = dbCmd.Parameters.Add("@BookGroup", SqlDbType.VarChar, 4);			
				paramBookGroup.Value = contractProcessStatusEventArgs.BookGroup;      

				SqlParameter paramContractId = dbCmd.Parameters.Add("@ContractId", SqlDbType.Char, 9);			
				paramContractId.Value = contractProcessStatusEventArgs.ContractId;       

				SqlParameter paramContractType = dbCmd.Parameters.Add("@ContractType", SqlDbType.Char, 1);			
				paramContractType.Value = contractProcessStatusEventArgs.ContractType;

				SqlParameter paramBook = dbCmd.Parameters.Add("@Book", SqlDbType.Char, 4);			
				paramBook.Value = contractProcessStatusEventArgs.Book;

				SqlParameter paramRecallId = dbCmd.Parameters.Add("@RecallId", SqlDbType.Char , 16);			
				paramRecallId.Direction = ParameterDirection.Output;
				paramRecallId.Value = "temp";
        
				dbCn.Open();

				while (true)
				{                   
					dbCmd.ExecuteNonQuery();

					if (paramRecallId.Value.ToString().Equals(""))
					{
						break;
					}

					ProcessStatusEventArgs processStatusEventArgs = new ProcessStatusEventArgs(
						Standard.ProcessId(),
						"L",
						"DRC",
						"Closed recall with lender reference: " + paramRecallId.Value.ToString(),
						"",
						"",
						false,
						contractProcessStatusEventArgs.BookGroup,
						contractProcessStatusEventArgs.ContractId,
						contractProcessStatusEventArgs.ContractType,
						contractProcessStatusEventArgs.Book,
						contractProcessStatusEventArgs.SecId,
						"",
						"",
						"",
						"",
						"",
						paramRecallId.Value.ToString());
                                        
					ProcessStatusEventInvoke(ProcessStatusSet(processStatusEventArgs));
				}
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [ProcessAgent.LoanetContractRecallUpdate]", 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}

		private void LoanetS3MemoSegUpdateSet(
			string bizDate,
			string secId,
			string quantity)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spMemoSegEntrySet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;
       
				SqlParameter paramBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				paramBizDate.Value = bizDate;

				SqlParameter paramSecId = dbCmd.Parameters.Add("@SecId", SqlDbType.VarChar, 12);
				paramSecId.Value = secId;

				SqlParameter paramQuantity = dbCmd.Parameters.Add("@Quantity", SqlDbType.BigInt);
				paramQuantity.Value = quantity;

			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [ProcessAgent.LoanetS3MemoSegUpdateSet]", 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}
	}
}