using System;
using StockLoan.Common;
using StockLoan.BackOffice;

namespace StockLoan.Process
{
  public class ProfitCenter_Change
  {
    private string message = "41";
    
    public ProfitCenter_Change()
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
      string bookGroup,
      string contractType,
      string book,
      string contractId,
      string profitCenter,
      string batchCode,
      string effectiveDate,
      string actUserId)
    {     
      string tempDate;

      message += bookGroup.PadRight(4, '0');
      message += contractType.Substring(0, 1);
      message += book.PadRight(4, '0').Substring(0, 4);
      message += contractId.PadLeft(9, '0').Substring(0, 9);
      message += profitCenter.PadRight(1, ' ').Substring(0, 1);
      message += batchCode.PadRight(1, ' ').Substring(0, 1);
      
      try
      {
        tempDate = DateTime.Parse(effectiveDate).ToString("MMddyy");       
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " Invalid effective date, " + effectiveDate + ", using, 000000. [ProfitCenter_Change.Request]", Log.Error, 1);        
        tempDate = new string('0', 6);
      }

      message += tempDate;

      ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
        Standard.ProcessId(),
        "L",
        "PCC",
        "PC change to: " + profitCenter + " for " + tempDate,
        "",
        actUserId,
        false,
        bookGroup,
        contractId,
        contractType,
        book,
        "",
        "",
		"",
		"",
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
      string book         = "";
      bool   hasError      = false;

      bookGroup    = messageText.Substring(11, 4);
      contractId   = messageText.Substring(19, 9);
      contractType = messageText.Substring(28, 1);
      book         = messageText.Substring(15, 4);
     
      switch (messageText.Substring(2, 1))
      {
        case "A": 
          status = "OK";
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
        "PCC",
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
            case 30: error += Errors.MESSAGE_TYPE_ERROR;
              break;

            case 31: error += Errors.BOOK_GROUP_ERROR;;
              break;

            case 32: error += Errors.CONTRACT_TYPE_ERROR;
              break;

            case 33: error += Errors.BOOK_ERROR;
              break;

            case 34: error += Errors.CONTRACT_ID_ERROR;
              break;

            case 35: error += Errors.POOL_CODE_ERROR;
              break;

            case 36: error += Errors.BATCH_CODE_ERROR;
              break;

            case 37: error += Errors.EFFECTIVE_DATE_ERROR;
              break;
          }
          error += Errors.ERROR_SEPERATOR;
        }
      }

      return error; 
    }
  }
}
