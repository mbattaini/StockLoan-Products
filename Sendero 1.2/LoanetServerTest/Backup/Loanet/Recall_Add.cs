// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using Anetics.Common;
using Anetics.Process;

namespace Anetics.Loanet
{
  public class Recall_Add
  {
    private string message = "C3";

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
      message += bookGroup.PadLeft(4, ' ').Substring(0, 4);
      message += contractType.PadLeft(1, ' ').Substring(0, 1);
      message += book.PadLeft(4, ' ').Substring(0, 4);
      message += contractId.PadLeft(9, '0').Substring(0, 9);
      message += DateTime.Parse(recallDate).ToString("MMddyy");
      message += recallQuantity.ToString("000000000");
      message += DateTime.Parse(buyinDate).ToString("MMddyy");
      message += DateTime.Parse(zeroInterest).ToString("MMddyy");
      message += terminationIndicator.PadRight(1, ' ').Substring(0, 1);
      message += recallReasonCode.PadRight(2, ' ').Substring(0, 2);      
      
      message += new string('0', 16);     // blank lender reference 

      message += comment.PadRight(20, ' ').Substring(0, 20);
    
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
      string recallSequence  = "";
      bool   hasError        = false;

      book         = messageText.Substring(15, 4);
      bookGroup    = messageText.Substring(11, 4);
      contractId   = messageText.Substring(19, 9);
      contractType = messageText.Substring(28, 1);
      
      switch(messageText.Substring(2, 1))
      {
        case "A" :
          status = "OK";
          hasError = false;
          recallSequence  = messageText.Substring(29, 6);
          lenderReference = messageText.Substring(35, 16);
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
        recallSequence + "|" + lenderReference);
     
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

            case 33: error += Errors.CONTRACT_ID_ERROR;
              break;

            case 34: error += Errors.RECALL_DATE_ERROR;
              break;

            case 35: error += Errors.QUANTITY_ERROR;
              break;

            case 36: error += Errors.BUYIN_DATE_ERROR;
              break;

            case 37: error += Errors.ZEROINTEREST_DATE_ERROR;
              break;

            case 38: error += Errors.RECALL_TERMINATION_ERROR;
              break;

            case 39: error += Errors.REASON_CODE_ERROR;
              break;

            case 40: error += Errors.LENDER_REFERENCE_ERROR;
              break;
          }

          error += Errors.ERROR_SEPERATOR;
        }
      }

      return error; 
    }        
  }
}
