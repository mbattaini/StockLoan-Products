// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using Anetics.Common;
using Anetics.Process;

namespace Anetics.Loanet
{
	public class Recall_Delete
	{
    private string message = "C7";
    
    public Recall_Delete()
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
      int    recallSequence,
      string lenderReference,
      string comment,
      string actUserId)
    {
      message += bookGroup.PadLeft(4, ' ').Substring(0, 4);
      message += contractType.PadLeft(1, ' ').Substring(0, 1);
      message += book.PadLeft(4, ' ').Substring(0, 4);
      message += contractId.PadLeft(9, '0').Substring(0, 9);
      message += DateTime.Parse(recallDate).ToString("MMddyy");
      message += recallSequence.ToString("000000").PadLeft(6, '0').Substring(0, 6);
      message += comment.PadRight(20, ' ').Substring(0, 20);
      
      ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
        Standard.ProcessId(), 
        "L", 
        "RCD", 
        "Recall delete, lender reference: " + lenderReference, 
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
        lenderReference);

      return processStatusEventArgsItem;
    }
   
    public ProcessStatusEventArgs Reply(string messageId, string messageText)
    {
      string status       = "";
      string bookGroup    = "";
      string contractId   = "";
      string contractType = "";
      string book         = "";
      bool   hasError     = false;

      book         = messageText.Substring(15, 4);
      bookGroup    = messageText.Substring(11, 4);
      contractId   = messageText.Substring(19, 9);
      contractType = messageText.Substring(28, 1);

      switch(messageText.Substring(2, 1))
      {
        case "A" : 
          status = "OK";
          hasError = false;
          break;
        
        case "X" :
          status = "System/Processing Error";
          hasError = true;
          break;
        
        case "R" :
          status = ParseRejectedRequest(messageText);
          hasError = true;
          break;
        
        default :
          status = "Unknown";
          hasError = true;
          break;
      }
      
      ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(messageId, 
        "L", 
        "RCD", 
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

            case 33: error += Errors.CONTRACT_ID_ERROR;
              break;

            case 34: error += Errors.RECALL_DATE_ERROR;
              break;

            case 35: error += Errors.SEQUENCE_NUMBER_ERROR;
              break;
          }

          error += Errors.ERROR_SEPERATOR;
        }
      }

      return error; 
    }        
	}
}
