// Licensed Materials - Property of StockLoan, LLC.
// Copyright (C) StockLoan, LLC. 2005  All rights reserved.

using System;
using StockLoan.Common;
using StockLoan.BackOffice;

namespace StockLoan.Process
{
  public class Rate_Change_NegotiatedToBox
  {
    private string message = "73";

    public Rate_Change_NegotiatedToBox()
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
      string actUserId)
    {
      message += bookGroup.PadLeft(4, ' ').Substring(0, 4);
      message += contractType.PadLeft(1, ' ').Substring(0, 1);
      message += book.PadLeft(4, ' ').Substring(0, 4);
      message += contractId.PadLeft(9, '0').Substring(0, 9);
      
      ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
        Standard.ProcessId(), 
        "L", 
        "NTB", 
        "Negotiated rate to box rate change", 
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
        "NTB", 
        "",
        "", 
        "ADMIN", 
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

            case 34: error += Errors.TABLE_RATE_ERROR;
              break;
          }
          error += Errors.ERROR_SEPERATOR;
        }
      }

      return error; 
    }
  }
}