// Licensed Materials - Property of Anetics, LLC.
// (C) Copyright Anetics, LLC. 2005  All rights reserved.

using System;
using Anetics.Common;
using Anetics.Process;

namespace Anetics.Loanet
{
	public class BankLoan_Pledge_Release
	{
		private string message = "";

		public BankLoan_Pledge_Release()
		{
		}

		public ProcessStatusEventArgs Request(
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
			
			string tempDate = "";			
			string act = "";

			message = "P1";
			message += bookGroup.PadLeft(4, ' ').Substring(0, 4);           
			message += secId.PadLeft(9, '0').Substring(0, 9);      
			message += recordType.PadLeft(3, '0').Substring(0, 3);
			message += book.PadLeft(4, '0').Substring(0, 4);
			message += quantity.ToString().PadLeft(9, '0').Substring(0, 9);
      
			try
			{
				tempDate = DateTime.Parse(loanDate).ToString("yyyyMMdd");        
			}
			catch (Exception error)
			{
				Log.Write(error.Message + ". Invalid loan date, " + loanDate + ", using, 000000. [BankLoan_Pledge_Release.Request]", Log.Error, 3);        
				tempDate = new string('0', 8);        
			}

			message += tempDate;
      
			if (recordType.Equals("001") || recordType.Equals("003"))
			{
				message += new string(' ', 12);
			}
			else
			{
				amount = amount * 100;
				message += amount.ToString("000000000000").PadLeft(12, '0').Substring(0, 12);
				amount = amount / 100;
			}

			message += loanPurpose.PadLeft(1, ' ').Substring(0, 1);
			message += releaseType.PadLeft(1, ' ').Substring(0, 1);
			message += hypothecation.PadLeft(1, ' ').Substring(0, 1);
			message += preventPendIndicator.PadLeft(1, ' ').Substring(0, 1);      
			message += cnsIndicator.PadLeft(1, ' ').Substring(0, 1);    
			message += ipoIndicator.PadLeft(1, ' ').Substring(0, 1);    
			message += ptaIndicator.PadLeft(1, ' ').Substring(0, 1);
			message += occParticipantNumber.PadLeft(4, ' ').Substring(0, 4);
			message += occNumber.PadLeft(3, ' ').Substring(0, 3);       
			message += comment.PadLeft(56, ' ').Substring(0, 56);
			message += dtcInputSequence.PadLeft(5, ' ').Substring(0, 5);
			 
			switch(recordType)
			{
				case "001":
					act = "Free Pledge of " + secId + " for " + quantity.ToString("#,##0") + " for loan date " + tempDate;
					break;

				case "002":						
					act = "Valued Pledge of " + secId + " for " + quantity.ToString("#,##0") + " for loan date " + tempDate;
					break;

				case "003":
					act = "Free Release " + secId + " for " + quantity.ToString("#,##0") + " for loan date " + tempDate;
					break;

				case "004":										
					act = "Valued Release " + secId + " for " + quantity.ToString("#,##0") + " for loan date " + tempDate;
					break;

				default:
					act = "Unrecongized record type: " + recordType.ToString();
					break;
			}
			
			ProcessStatusEventArgs processStatusEventArgs = new ProcessStatusEventArgs(Standard.ProcessId(), 
				"L",
				"BLA",
				act, 
				"",
				actUserId, 
				false, 
				bookGroup,
				"",
				"",
				book,
				secId,
				"",
				quantity.ToString(),
				amount.ToString(),
				"",
				"",
				messageId);
      
			return processStatusEventArgs;
		}
    
    
		public ProcessStatusEventArgs Reply(string messageId, string messageText)
		{
			string status         = "";
			string bookGroup      = "";       			
			
			bool   isError        = false;
      
			bookGroup    = messageText.Substring(11, 4);						
			
			switch (messageText.Substring(2, 1))
			{
				case "A": status = "OK";
					isError = false;                  
					break;
        
				case "X": status = "System/Processing Error";
					isError = true;
					break;

				case "R": status = ParseRejectedRequest(messageText);
					isError = true;
					break;
			}
      
			ProcessStatusEventArgs processStatusEventArgs = new ProcessStatusEventArgs(messageId, 
				"L", 
				"BLA", 
				"", 
				"", 
				"", 
				isError,
				bookGroup,
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				status,
				"",
				"");
     
			return processStatusEventArgs;
		}

		private string ParseRejectedRequest(string message)
		{
			string error = "";

			for (int index = 0; index < message.Length; index ++)
			{
				if (message[index].Equals('X'))
				{
					switch (index)
					{
						case 24: error += Errors.MESSAGE_TYPE_ERROR;
							break;

						case 25: error += Errors.BOOK_GROUP_ERROR;
							break;
            
						case 26: error += Errors.CUSIP_ERROR;
							break;

						case 27: error += Errors.RECORD_TYPE_ERROR;
							break;

						case 28: error += Errors.BANK_NUMBER_ERROR;
							break;

						case 29: error += Errors.QUANTITY_ERROR;
							break;

						case 30: error += Errors.LOAN_DATE_ERROR;
							break;
            
						case 31: error += Errors.AMOUNT_ERROR;
							break;

						case 32: error += Errors.LOAN_PURPOSE_ERROR;
							break;

						case 33: error += Errors.RELEASE_TYPE_ERROR;
							break; 
            
						case 34: error += Errors.HYPOTHECATION_ERROR;
							break;

						case 35: error += Errors.PREVENT_PEND_ERROR;
							break;
            
						case 36: error += Errors.CNS_ERROR;
							break;

						case 37: error += Errors.IPO_ERROR;
							break;

						case 38: error += Errors.PTA_ERROR;
							break; 
            
						case 39: error += Errors.OCC_PARTICIPENT_ERROR;
							break;

						case 40: error += Errors.OCC_NUMBER_ERROR;
							break;
            
						case 41: error += Errors.COMMENT_ERROR;
							break;

						case 42: error += Errors.INPUT_SEQUENCE_ERROR;
							break;            
					}
					error += Errors.ERROR_SEPERATOR;
				}
			}
			return error; 
		}        
	
	
		public string Message
		{
			get
			{
				return message;
			}
		}
	}
}
