// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using Anetics.Common;
using Anetics.Process;

namespace Anetics.Loanet
{
  public class Recall_Add
  {
    private string message = "S1";

    public Recall_Add()
    {
    }

    public string Message
    {
      get
      {
        return message;
      }
    }

		public ProcessStatusEventArgs Request(string bookGroup,
			string contractType,
			string book,
			string contractId,
			string recallDate,
			long   recallQuantity,
			string buyinDate,
			string zeroInterest,
			string terminationIndicator,
			string recallReasonCode,
			string lenderReference,
			string comment,
			string actUserId)
		{
			message += "01.00";
			message += bookGroup.PadLeft(4, ' ').Substring(0, 4);
			message += book.PadLeft(4, ' ').Substring(0, 4);			
			message += DateTime.Parse(recallDate).ToString("MMddyy");
			message += recallQuantity.ToString("000000000");
			message += DateTime.Parse(buyinDate).ToString("MMddyy");
			message += DateTime.Parse(zeroInterest).ToString("MMddyy");
			message += DateTime.Parse(buyinDate).ToString("MMddyy");
			message += terminationIndicator.PadRight(1, ' ').Substring(0, 1);
			message += recallReasonCode.PadRight(2, ' ').Substring(0, 2);            
			message += new string('0', 16);     // blank lender reference 
			message += comment.PadRight(20, ' ').Substring(0, 20);
			message += comment.PadRight(50, ' ').Substring(0, 50);
			message += contractId.PadRight(15, ' ').Substring(0, 15);
			message += contractType.PadLeft(1, ' ').Substring(0, 1);
			message += "F";
      

      
    
			ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
				Standard.ProcessId(), 
				"L", 
				"RCA",
				"New recall of " + recallQuantity.ToString(), 
				"", 
				actUserId, 
				false, 
				bookGroup,
				contractId,
				contractType,
				book,        
				"",
				"",
				recallQuantity.ToString(), 
				"",
				"Pending", 
				"",
				lenderReference);
      
			return processStatusEventArgsItem;
		}
    
		public ProcessStatusEventArgs Reply(string messageId, string messageText)
		{
			string status          = "";
			string bookGroup       = "";
			string contractId      = "";
			string contractType    = "";
			string book            = "";
			string lenderReference = "";			
			bool   hasError        = false;

			book         = messageText.Substring(20, 8);
			bookGroup    = messageText.Substring(11, 4);
			contractId   = messageText.Substring(28, 15);
			contractType = messageText.Substring(43, 1);
      
			switch(messageText.Substring(7, 1))
			{
				case "A" :
					status = "OK";
					hasError = false;					
					lenderReference = messageText.Substring(44, 16);
					break;
        
				case "X" : 
					status = "System/Processing Error";
					hasError = true;
					break;

				case "R":
					status = ParseRejectedRequest(messageText);
					hasError = true;
					break;

				default:
					status = "Unknown";
					hasError = true;
					break;
			}
      
			ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
				messageId, 
				"L", 
				"RCA", 
				"", 
				"", 
				"", 
				hasError,
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				status,
				"",
				lenderReference);
     
			return processStatusEventArgsItem;
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
            case 44: error += Errors.VERSION_ERROR;
              break;

            case 45: error += Errors.BOOK_GROUP_ERROR;
              break;

            case 46: error += Errors.BOOK_ERROR;
              break;

            case 47: error += Errors.RECALL_DATE_ERROR;
              break;

            case 48: error += Errors.QUANTITY_ERROR;
              break;

            case 49: error += Errors.BUYIN_DATE_ERROR;
              break;

            case 50: error += Errors.ZEROINTEREST_DATE_ERROR;
              break;

            case 51: error += Errors.RECALL_DUE_DATE_ERROR;
              break;

            case 52: error += Errors.RECALL_TERMINATION_ERROR;
              break;

            case 53: error += Errors.REASON_CODE_ERROR;
              break;

            case 54: error += Errors.LENDER_REFERENCE_ERROR;
              break;

            case 55: error += Errors.CONTRACT_ID_ERROR;
              break;

						case 56: error += Errors.CONTRACT_TYPE_ERROR;
							break;

						case 57: error += Errors.CONTRACT_SOURCE_ERROR;
							break;

						case 58: error += Errors.CUSIP_ERROR;
							break;

						case 59: error += Errors.SECURITY_TYPE_ERROR;
							break;

						case 60: error += Errors.AMOUNT_ERROR;
							break;

						case 61: error += Errors.QUANTITY_ERROR;
							break;

						case 62: error += Errors.DELIVERY_DATE_ERROR;
							break;	

						case 63: error += Errors.RATE_ERROR;
							break;

						case 64: error += Errors.RATE_CODE_ERROR;
							break;

						case 65: error += Errors.CURRENCY_CODE_ERROR;
							break;

						case 66: error += Errors.COLLATERAL_CODE_ERROR;
							break;

						case 67: error += Errors.DIV_RATE_ERROR;
							break;

						case 68: error += Errors.SECURITY_VIOLATION;
							break;
						
						case 69: error += Errors.ARMS_CONTRA_NOT_PARTICIPANT;
							break;
						
						case 70: error += Errors.ARMS_CONTRACT_NOT_FILE_ERROR;
							break;
						
						case 71: error += Errors.ARMS_RECALL_DATE_ERROR;
							break;
						
						case 72: error += Errors.ARMS_BUYIN_RECALL_DATE_ERROR;
							break;
						
						case 73: error += Errors.ARMS_ZERO_INTEREST_RECALL_ERROR;
							break;
						
						case 74: error += Errors.ARMS_RECALLDUE_RECALL_DATE_ERROR;
							break;
						
						case 75: error += Errors.ARMS_RECALLDATE_DELIVERYDATE_ERROR;
							break;
						
						case 76: error += Errors.ARMS_ASOF_RECALL_ERROR;
							break;
						
						case 77: error += Errors.ARMS_LATE_RECALL_ERROR;
							break;
						
						case 78: error += Errors.ARMS_DTC_CONTRA_ERROR;
							break;
						
						case 79: error += Errors.ARMS_LENDER_REF_UNIQUE_ERROR;
							break;
						
						case 80: error += Errors.ARMS_CONTRACT_CONTRA_ERROR;
							break;
						
						case 81: error += Errors.ARMS_RECALL_QUANTITY_CONTRACT_ERROR;
							break;
						
						case 82: error += Errors.ARMS_RECALL_QUANTITY_AVAILABLE_ERROR;
							break;
          }

          error += Errors.ERROR_SEPERATOR;
        }
      }

      return error; 
    }        
  }
}
