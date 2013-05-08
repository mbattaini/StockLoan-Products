using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Collections;
using StockLoan.Common;
using StockLoan.BackOffice;

namespace StockLoan.Process
{
	public delegate bool ReplyHandlerDelegate(string messageId, string messageText);
	public delegate bool ActivityHandlerDelegate(string messageText);

	public class ProcessAgent : MarshalByRefObject, IBackOffice
	{         
		private string dbCnStr;
		private MqSeries mqSeries;


		public ProcessAgent(string dbCnStr)
		{
			this.dbCnStr = dbCnStr;
			ArrayList messages = new ArrayList();
			DataSet dsMessages = new DataSet();

			try
			{
				dsMessages = ProcessMessageGet(Master.BizDate, false);

				foreach (DataRow dr in dsMessages.Tables["ProcessMessages"].Rows)
				{
					MessageRequestStruct tempMessage = new MessageRequestStruct();
				
					tempMessage.credentials = dr["Credentials"].ToString();
					tempMessage.messageId = dr["ProcessId"].ToString();
					tempMessage.messageText = dr["Content"].ToString();
					tempMessage.queueManagerName = dr["QueueManagerName"].ToString();
					tempMessage.queueName = dr["QueueName"].ToString();
					tempMessage.remoteQueueManager = dr["RemoteQueueManager"].ToString();
					tempMessage.replyToQueue = dr["ReplyToQueue"].ToString();
					tempMessage.replyToQueueManager = dr["ReplyToQueueManager"].ToString();

					messages.Add(tempMessage);
				}
				
				mqSeries = new MqSeries(
					Standard.ConfigValue("HostName"),
					Standard.ConfigValue("Channel"),
					int.Parse(Standard.ConfigValue("Port")),
					messages);
			}
			catch (Exception e)
			{
				Log.Write(e.Message + "[ProcessAgent.ProcessAgent]", Log.Error, 1);
			}
		}

		public void Start()
		{
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

				mqSeries.WaitPutStart(int.Parse(Standard.ConfigValue("ActivityWaitSeconds")));
			}
			catch (Exception e)
			{
				Log.Write(e.Message + "[ProcessAgent.Start]", Log.Error, 1);
			}
		}

		public void Stop()
		{
			try
			{
				mqSeries.WaitActivityStop();
				mqSeries.WaitReplyStop();
				mqSeries.WaitPutStop();
			}
			catch (Exception e)
			{
				Log.Write(e.Message + "[ProcessAgent.Stop]", Log.Error, 1);
			}			
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
		
		private string BookGroupCredentials(string bookGroup)
		{
			return (Standard.ConfigValue("LoanetCoOpUserId" + bookGroup) + Standard.ConfigValue("LoanetCoOpPassword" + bookGroup));        
		}
		
		private bool AutoMarkIsCurrent(string bookGroup)
		{
			return KeyValue.Get("LoanetMarksBizDate" + bookGroup, "0001-01-01", dbCnStr).Equals(Master.ContractsBizDate);
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
		}

		public void IntlContractAdd(
			string dealId,
			string bookGroup,
			string dealType,
			string secId,
			string book,
			long quantity,
			decimal amount,
			string collateralCode,
			string expiryDate,
			decimal rate,
			string rateCode,
			string poolCode,
			string marginCode,
			decimal margin,
			decimal negotiatedNewRate,
			string comment,
			string otherBook,
			decimal fixedInvesmtmentRate,
			string deliveryLocation,
			string deliveryDate,
			bool incomeTracked,
			decimal divRate,
			bool divCallable,
			string currencyIso,
			string cashDepot,
			string exchange,
			string actUserId)
		{

			ProcessStatusEventArgs processStatusEventItem = null;
			NewContract_International_Add contractRequest = new NewContract_International_Add();

			string batchCode = "";
			string deliveryCode = "D";

			if (dealType.Equals("B"))
			{
				batchCode = KeyValue.Get("ProcessBatchCodeBorrows", "A", dbCnStr);
				deliveryCode = "D";
			}
			else if (dealType.Equals("L"))
			{
				batchCode = KeyValue.Get("ProcessBatchCodeLoans", "B", dbCnStr);
				deliveryCode = "0"; // Zero.
			}

			processStatusEventItem = contractRequest.Request(
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


			try
			{
				ProcessStatusSet(processStatusEventItem);
				ProcessMessageSet(processStatusEventItem.ProcessId, contractRequest.Message, false);

				mqSeries.Put(Standard.ConfigValue("ContractAddQueueManagerName", ""),
					Standard.ConfigValue("ContractAddQueueName", ""),
					BookGroupCredentials(bookGroup),
					processStatusEventItem.ProcessId,
					contractRequest.Message,
					Standard.ConfigValue("ReplyQueueManagerName", ""),
					Standard.ConfigValue("ReplyQueueName", ""));


				ProcessMessageSet(processStatusEventItem.ProcessId, contractRequest.Message, true);
			}
			catch (Exception error)
			{
				processStatusEventItem = new ProcessStatusEventArgs(
					processStatusEventItem.ProcessId,
					processStatusEventItem.SystemCode,
					processStatusEventItem.ActCode,
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

				ProcessStatusSet(processStatusEventItem);
				ProcessMessageSet(processStatusEventItem.ProcessId, contractRequest.Message, false);

				Log.Write(error.Message + " [ProcessAgent.IntlContractAdd]", Log.Error, 3);
				throw new Exception(error.Message);
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
		}
    
		public void Return(
			string returnId,
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
			ProcessStatusEventArgs processStatusEventItem = null;
			FullPartial_Return_AmountCallback returnRequest = new FullPartial_Return_AmountCallback();            
      
			string batchCode = "";
			string deliveryCode = "D";

			if (contractType.Equals("B"))
			{
				batchCode = KeyValue.Get("ProcessBatchCodeReturnsBorrows", "3", dbCnStr);                         
				deliveryCode = "0";
			}      
			else if (contractType.Equals("L"))
			{
				batchCode = KeyValue.Get("ProcessBatchCodeReturnsLoans", "4", dbCnStr);
				deliveryCode = "D";
			}  
      
			callbackRequired = KeyValue.Get("ProcessCallbackRequiredCode", "L", dbCnStr);

			processStatusEventItem = returnRequest.Request(
				returnId,
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

			if (processStatusEventItem != null)
			{
				ProcessStatusSet(processStatusEventItem);
//				ProcessMessageSet(processStatusEventItem.ProcessId, returnRequest.Message);
      
				try
				{
					mqSeries.Put(Standard.ConfigValue("ReturnQueueManagerName", ""),
						Standard.ConfigValue("ReturnQueueName", ""),
						BookGroupCredentials(bookGroup),
						processStatusEventItem.ProcessId,
						returnRequest.Message,
						Standard.ConfigValue("ReplyQueueManagerName", ""),
						Standard.ConfigValue("ReplyQueueName", ""));      
				}
				catch (Exception error)
				{
					processStatusEventItem = new ProcessStatusEventArgs(
						processStatusEventItem.ProcessId,
						processStatusEventItem.SystemCode,
						processStatusEventItem.ActCode,
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

					ProcessStatusSet(processStatusEventItem);

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
			/*ProcessStatusEventArgs processStatus = null;
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
			}*/
		}


		private bool ReplyHandler(string messageId, string messageText)
		{
			ProcessStatusEventArgs processStatusEventItem = null;
			
//			ProcessMessageSet(messageId, messageText);

			switch (messageText.Substring(0, 2))
			{
				case "10":
					NewContract_International_Add contractRequest = new NewContract_International_Add();
					processStatusEventItem = contractRequest.Reply(messageId, messageText);
					ProcessStatusSet(processStatusEventItem);
					ProcessContractSet(processStatusEventItem.BookGroup, messageId, processStatusEventItem.ContractId, processStatusEventItem.ContractType, ((processStatusEventItem.HasError) ? "F" : "A"));
					break;

				case "26":
					FullPartial_Return_AmountCallback returnRequest = new FullPartial_Return_AmountCallback();
					processStatusEventItem = returnRequest.Reply(messageId, messageText);
					ProcessStatusSet(processStatusEventItem);
					break;

				default:
					break;
			}

			return true;
		}
    
		private bool ActivityHandler(string messageText)
		{
			ProcessStatusEventArgs[] processStatusEventList = null;
			
			Log.Write(messageText, 1);
			//ProcessMessageSet(Standard.ProcessId(), messageText);

			switch (messageText.Substring(0, 2))
			{
				case "T4":
					Transaction_Data_Datagram transDgram = new Transaction_Data_Datagram(dbCnStr);
					processStatusEventList = transDgram.Activity(messageText);

					foreach (ProcessStatusEventArgs processStatusEventItem in processStatusEventList)
					{
						ProcessStatusSet(processStatusEventItem);
					}
					break;

				default:
					break;
			
			}			
			return true;
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}

		private DataSet ProcessMessageGet(string bizDate, bool isProcessed)
		{
			SqlConnection dbCn = new SqlConnection(dbCnStr);
			DataSet dsProcesssMessages = new DataSet();

			try
			{
				SqlCommand dbCmd = new SqlCommand("spProcessStatusGet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter parmaBizDate = dbCmd.Parameters.Add("@BizDate", SqlDbType.DateTime);
				parmaBizDate.Value = bizDate;

				SqlParameter paramIsProcessed = dbCmd.Parameters.Add("@IsProcessed", SqlDbType.Bit);
				paramIsProcessed.Value = isProcessed.ToString();

				SqlDataAdapter dataAdapter = new SqlDataAdapter(dbCmd);
				dataAdapter.Fill(dsProcesssMessages, "ProcessMessages");
			}
			catch (Exception error)
			{
				Log.Write(error.Message + " [ProcessAgent.ProcessMessageGet]", Log.Error, 1);
			}

			return dsProcesssMessages;
		}

		
		private void ProcessMessageSet(string processId, string content, bool isProcessed)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spProcessMessageSet", dbCn);
				dbCmd.CommandType = CommandType.StoredProcedure;

				SqlParameter paramProcessId = dbCmd.Parameters.Add("@ProcessId", SqlDbType.VarChar, 16);
				paramProcessId.Value = processId;

				SqlParameter paramContent = dbCmd.Parameters.Add("@Content", SqlDbType.VarChar, 500);
				paramContent.Value = content;

				SqlParameter paramIsProcessed = dbCmd.Parameters.Add("@IsProcessed", SqlDbType.Bit);
				paramIsProcessed.Value = isProcessed;

				dbCn.Open();
				dbCmd.ExecuteNonQuery();
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". [ProcessAgent.ProcessMessageSet]", 1);
				throw;
			}
			finally
			{
				dbCn.Close();
			}
		}


		private void ProcessContractSet(string bookGroup, string dealId, string contractId, string contractType, string status)
		{
			SqlConnection dbCn = null;
			SqlCommand dbCmd = null;

			try
			{
				dbCn = new SqlConnection(dbCnStr);
				dbCmd = new SqlCommand("spProcessContractSet", dbCn);
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
				Log.Write(error.Message + ". [ProcessAgent.ProcessContractSet]", 1);
				throw;
			}
			finally
			{
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
				Log.Write(error.Message + " [ProcessAgent.ProcessStatusSet]", Log.Error, 1);
			}
			finally
			{
				dbCn.Close();
			}

			return processStatusEventArgs;
		}
 		
		
   
	}
}