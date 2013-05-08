// Licensed Materials - Property of StockLoan, LLC.
// (C) Copyright StockLoan, LLC. 2005 All rights reserved.

using System;
using StockLoan.Common;
using StockLoan.BackOffice;

namespace StockLoan.Process
{
  public class Rate_Change_BoxRate
  {
    private string message = "79";

    public Rate_Change_BoxRate()
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
      string  securityType,
      decimal boxRate,
      string  actUserId)
    {
      message += bookGroup.PadLeft(4, ' ').Substring(0, 4);
      message += contractType.PadLeft(1, ' ').Substring(0, 1);
      message += book.PadLeft(4, ' ').Substring(0, 4);
      message += securityType.PadLeft(1, ' ').Substring(0, 1);
      
      boxRate = boxRate * 1000;
      message += boxRate.ToString("00000").Substring(0, 5);
      boxRate = boxRate / 1000;
            
      ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
        Standard.ProcessId(), 
        "L", 
        "RCB", 
        (securityType.Equals("B")) ? "Box bond rate change to: " + boxRate.ToString("##.000") : "Box stock rate change to: " + boxRate.ToString("##.000"), 
        "", 
        actUserId, 
        false,
        bookGroup,
        "",
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
  
    public ProcessStatusEventArgs  Reply(string messageId, string messageText)
    {
      string status = "";
      string bookGroup    = "";
      string contractId   = "";
      string contractType = "";
      string systemTime = "";
      bool   hasError = false;

      systemTime   = messageText.Substring(3, 8);
      bookGroup    = messageText.Substring(11, 4);
      contractId   = new string(' ', 9);
      contractType = messageText.Substring(19, 1);

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
        "RCB", 
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
            case 26: error += Errors.MESSAGE_TYPE_ERROR;
              break;

            case 27: error += Errors.BOOK_GROUP_ERROR;
              break;

            case 28: error += Errors.CONTRACT_TYPE_ERROR;
              break;

            case 29: error += Errors.BOOK_ERROR;
              break;

            case 30: error += Errors.SECURITY_TYPE_ERROR;
              break;

            case 31: error += Errors.TABLE_RATE_ERROR;
              break;
          }
          error += Errors.ERROR_SEPERATOR;
        }
      }

      return error; 
    }
  }
}