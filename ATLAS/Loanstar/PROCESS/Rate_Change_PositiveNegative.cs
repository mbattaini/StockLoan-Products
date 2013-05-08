// Licensed Materials - Property of StockLoan, LLC.
// Copyright (C) StockLoan, LLC. 2005  All rights reserved.

using System;
using StockLoan.Common;
using StockLoan.BackOffice;

namespace StockLoan.Process
{
  public class Rate_Change_PositiveNegative
  {
    private string message = "77";      

    public Rate_Change_PositiveNegative()
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
      string  contractId,
      string  rateCode,
      decimal rate,
      string  batchCode,
      string  deliveryCode,
      string  profitCenter,
      string  actUserId)
    {
      string act = "";
      
      message += bookGroup.PadLeft(4, ' ').Substring(0, 4);
      message += contractType.PadLeft(1, ' ').Substring(0, 1);
      message += contractId.PadLeft(9, '0').Substring(0, 9);
      
      if (rate < 0)
      {
        rate     = rate * -1;
        rateCode = "N";
      }

      message += rateCode.PadLeft(1, ' ').Substring(0, 1);
      
      rate = rate * 1000;
      message += rate.ToString("00000");
      rate = rate / 1000;

      if (rateCode.Equals("N"))
      {
        rate = rate * -1;
      }
      
      message += batchCode.PadLeft(1, ' ').Substring(0, 1);
      message += deliveryCode.PadRight(1, ' ').Substring(0, 1);
      message += profitCenter.PadRight(1, ' ').Substring(0, 1);                                   
      
      ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
        Standard.ProcessId(), 
        "L", 
        "PTN", 
        act = "Positive/Negative rate change to rate: " +  rate.ToString("##.000"),
        "", 
        actUserId, 
        false,
        bookGroup,
        contractId,
        contractType,
        "",
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
      string status        = "";
      string bookGroup     = "";
      string contractId    = "";     
      string contractType  = "";      
      bool   hasError       = false;
      
      bookGroup    = messageText.Substring(11, 4);
      contractId   = messageText.Substring(15, 9);
      contractType = messageText.Substring(24, 1);
      
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
        "PTN", 
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
        if (message[index].Equals('X') || message[index].Equals('N'))
        {
          switch (index)
          {
            case 25: error += Errors.MESSAGE_TYPE_ERROR;
              break;

            case 26: error += Errors.BOOK_GROUP_ERROR;
              break;

            case 27: error += Errors.BOOK_ERROR;
              break;

            case 28: error += Errors.CONTRACT_ID_ERROR;
              break;
            
            case 29: error += Errors.RATE_CODE_ERROR;
              break;

            case 30:  
              if (message[index].Equals('N'))
              {
                error += Errors.NEGATIVE_ERROR;
              }
              else
              {
                error += Errors.RATE_ERROR;
              }
              break;

            case 31: error += Errors.BATCH_CODE_ERROR;
              break;

            case 32: error += Errors.DELIVERY_CODE_ERROR;
              break;

            case 33: error += Errors.POOL_CODE_ERROR;
              break;            
          }
          error += " ";
        }
      }

      return error; 
    }
  }
}