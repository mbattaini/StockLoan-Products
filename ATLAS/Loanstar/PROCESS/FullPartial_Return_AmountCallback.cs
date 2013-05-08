using System;
using StockLoan.Common;
using StockLoan.BackOffice;

namespace StockLoan.Process
{
	public class FullPartial_Return_AmountCallback
	{
    private string message = "25";
    
    public FullPartial_Return_AmountCallback()
		{
		}
    
    public string Message
    {
      get
      {
        return message;
      }
    }

		public ProcessStatusEventArgs Request(
			string returnId,
			string bookGroup,
			string contractType,
			string contractId,
			long   returnQuantity,
			string batchCode,
			string deliveryCode,
			decimal returnAmount,
			string callbackRequired,
			string recDelLocation,
			string cashDepot,
			string actUserId)
		{
			returnAmount = returnAmount * 100;
      
			message += bookGroup.PadLeft(4, '0').Substring(0, 4);      
			message += contractType.Substring(0, 1);      
			message += contractId.PadRight(9, '0').Substring(0, 9);                
			message += returnQuantity.ToString("000000000").PadLeft(9, '0').Substring(0, 9);      
			message += batchCode.PadRight(1, ' ').Substring(0, 1);      
			message += deliveryCode.PadLeft(1, ' ').Substring(0, 1);      
			message += returnAmount.ToString("000000000000").PadLeft(12, '0').Substring(0, 12);           
			message += callbackRequired.PadLeft(1, ' ').Substring(0, 1);            
			message += recDelLocation.PadRight(2, ' ');
			message += cashDepot.PadRight(2, ' ').Substring(0, 2);
      
			ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
				returnId, 
				"L",
				"FRC", 
				"Return of " + returnQuantity.ToString(), 
				"",
				actUserId, 
				false, 
				bookGroup,
				contractId,
				contractType,
				"",
				"", 
				"",
				((contractType.Equals("B")) ? returnQuantity * (-1): returnQuantity).ToString(),
				returnAmount.ToString(),
				"Pending", 
				"",
				"");

			return processStatusEventArgsItem;
		}
		
    public ProcessStatusEventArgs Reply(string messageId, string messageText)
    {
      string status       = "";
      string bookGroup    = "";
      string contractId   = "";
      string contractType = "";
      bool   hasError      = false;

      bookGroup    = messageText.Substring(11, 4);
      contractId   = messageText.Substring(15, 9);
      contractType = messageText.Substring(24, 1);    

      switch (messageText.Substring(2, 1))
      {
        case "A": 
          status  = "OK";
          hasError = false; 
          break;
        
        case "X": 
          status = "System/Processing Error";
          hasError = true;
          break;

        case "R": 
          status = ParseRejectedRequest(messageText);
          hasError = true;
          break;

        default :
          status = "Unknown";
          hasError = true;
          break;
      }

      ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
        messageId,
        "L",
        "FRC",
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
        "");
      
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
						case 25: error += Errors.MESSAGE_TYPE_ERROR;
							break;

						case 26: error += Errors.BOOK_GROUP_ERROR;
							break;

						case 27: error += Errors.CONTRACT_TYPE_ERROR;
							break;

						case 28: error += Errors.CCF_ERROR;
							break;

						case 29: error += Errors.CONTRACT_ID_ERROR;
							break;

						case 30: error += Errors.QUANTITY_ERROR;
							break;

						case 31: error += Errors.BATCH_CODE_ERROR;
							break;

            case 32: error += Errors.DELIVERY_CODE_ERROR;
              break;

            case 33: error += Errors.AMOUNT_ERROR;
              break;

            case 34: error += Errors.CALLBACK_ERROR;
              break;

            case 35: error += Errors.RECDEL_ERROR;
              break;

            case 36: error += Errors.MONEY_SETTLEMENT_ERROR;
              break;
					}
					error += Errors.ERROR_SEPERATOR;
				}
			}

			return error; 
		}
	}
}
