// Licensed Materials - Property of StockLoan, LLC.
// Copyright (C) StockLoan, LLC. 2005  All rights reserved.

using System;
using StockLoan.Common;
using StockLoan.BackOffice;

namespace StockLoan.Process
{
  public class Rate_Change_NegotiatedRate
  {
    private string message = "71";

    public Rate_Change_NegotiatedRate()
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
      string  bookGroup,
      string  contractType,
      string  book,
      string  contractId,
      decimal interestRate,
      string  effectiveDate,
      string  bizDate,
      string  actUserId)
    {     
      string tempDate;
      string rateCode = "";

      message += bookGroup.PadLeft(4, ' ').Substring(0, 4);
      message += contractType.PadLeft(1, ' ').Substring(0, 1);
      message += book.PadLeft(4, ' ').Substring(0, 4);
      message += contractId.PadLeft(9, '0').Substring(0, 9);
      
      if (interestRate < 0)
      {
        interestRate = interestRate * -1;
        rateCode = "N";
      }
      
      interestRate = interestRate * 1000;
      message += interestRate.ToString("00000");
      interestRate = interestRate / 1000;

      if (rateCode.Equals("N"))
      {
        interestRate = interestRate * -1;
      }
      
      try
      {
        tempDate = DateTime.Parse(effectiveDate).ToString("MMddyy");
        if(tempDate.Equals(DateTime.ParseExact(bizDate, "yyyy-MM-dd", null).ToString("MMddyy")))
        {
          tempDate = new string('0', 6);
        }      
      }
      catch (Exception error)
      {
        Log.Write(error.Message + " Invalid effective date, " + effectiveDate + ", using, 000000. [Rate_Change_NegotiatedRate.Request]", Log.Error, 3);        
        tempDate = new string('0', 6);
      }
      
      message += tempDate;
      
      ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
        Standard.ProcessId(), 
        "L", 
        "NGR", 
        "Negotiated rate change to rate: " + interestRate.ToString("##.000"), 
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

      book         = messageText.Substring(15, 4);
      bookGroup    = messageText.Substring(11, 4);
      contractId   = messageText.Substring(19, 9);
      contractType = messageText.Substring(28, 1);

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
        "NGR", 
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
            case 29: error += Errors.MESSAGE_TYPE_ERROR;
              break;

            case 30: error += Errors.BOOK_GROUP_ERROR;
              break;

            case 31: error += Errors.CONTRACT_TYPE_ERROR;
              break;

            case 32: error += Errors.BOOK_ERROR;
              break;

            case 33: error += Errors.CONTRACT_ID_ERROR;;
              break;

            case 34: error += Errors.INTEREST_RATE_ERROR;
              break;

            case 35: error += Errors.EFFECTIVE_DATE_ERROR;
              break;
          }
          error += Errors.ERROR_SEPERATOR;
        }
      }

      return error; 
    }
  }
}
