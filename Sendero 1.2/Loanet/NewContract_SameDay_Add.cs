// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using Anetics.Common;
using Anetics.Process;

namespace Anetics.Loanet
{
	public class NewContract_SameDay_Add
	{
    private string message = "05";

    public NewContract_SameDay_Add()
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
      string  dealId,
      string  bookGroup,
      string  dealType,
      string  secId,
      string  book,
      string  deliveryCode,
      long    quantity,
      decimal amount,
      string  batchCode,
      string  collateralCode,
      string  expiryDate,
      decimal rate,
      string  rateCode,
      string  poolCode,
      string  markParameterType,
      decimal markParameterAmount,
      decimal negotiatedNewRate,
      string  comment,
      string  otherBook,
      decimal fixedInvesmtmentRate,
      bool    incomeTracked,
      decimal divRate,
      string  actUserId)
    {     
      string tempDate = "";            
         
      message += bookGroup.Substring(0, 4);
      message += dealType.Substring(0, 1);
      message += secId.PadRight(9,' ').Substring(0, 9);
      message += book.PadRight(4, '0').Substring(0, 4);
      message += new string(' ', 4);
      message += batchCode.Substring(0,1);
      message += deliveryCode.Substring(0,1);
      message += quantity.ToString().PadLeft(9,'0').Substring(0,9);
      
      switch (collateralCode.Substring(0,1))
      {
          
        case "1":
        case "2":
        case "5":                       
          amount = amount * 100;
          message += amount.ToString("000000000000").Substring(0, 12);
					amount = amount / 100;
          break;

        case "3":
        case "4":
        case "6":
          message += new string('0', 12);
          break;

        case "C":
        case "7":
          amount = amount * 100;          
          message += amount.ToString("000000000000").Substring(0, 12);
					amount = amount / 100;
          break;
        default:
          throw new Exception("Unanticipated collateral code: " + collateralCode.Substring(0,1) + " [NewContract_SameDay_add.Request]");
      }
          
      message += collateralCode.Substring(0,1).ToUpper();
      
      try
      {
        tempDate = DateTime.Parse(expiryDate).ToString("MMddyy");
      }
      catch
      {
        Log.Write(" Expiry date, " + expiryDate + ", was invalid or not specified using, 000000. [NewContract_SameDay_Add.Request]", Log.Information, 3);        
        tempDate = new string('0', 6);
      }
      
      message += tempDate;
      
      
      if (rateCode.PadRight(1,' ').Substring(0, 1).Equals("T"))
      {
        message += "00000";
        message += "T";
      }
      else
      {
        if (rate < 0)
        {
          rate *= -1;
          rate = rate * 1000;
          message += rate.ToString("00000").Substring(0, 5);
          message += "N";
        }
        else
        {
          rate = rate * 1000;
          message += rate.ToString("00000");
          message += rateCode.PadRight(1, ' ').Substring(0,1);
        }
      }
   
      message += poolCode.PadRight(1,' ').Substring(0,1).ToUpper();
      message += markParameterType.PadRight(1, ' ').Substring(0, 1);      
      
      switch (markParameterType)
      {
        case "%":
        case "1":
          markParameterAmount = markParameterAmount * 100 - 100;      
          message += markParameterAmount.ToString("00").PadLeft(2, '0').Substring(0, 2);
          break;

        case "0":
          markParameterAmount = 100 - markParameterAmount * 100;      
          message += markParameterAmount.ToString().PadLeft(2, '0').Substring(0, 2);
          break;
        default:
          throw new Exception("Unanticipated margin code: " + markParameterType + " [NewContract_SameDay_add.Request]");
      }

      negotiatedNewRate = negotiatedNewRate * 1000;
      message += negotiatedNewRate.ToString("00000").PadRight(5, '0').Substring(0, 5);      
      message += comment.PadRight(20, ' ').Substring(0, 20);
      message += otherBook.PadRight(4, ' ').Substring(0, 4);
      
      if (fixedInvesmtmentRate == 0)
      {
        message += new string(' ', 5);
      }
      else
      {
        fixedInvesmtmentRate = fixedInvesmtmentRate * 1000;
        message += fixedInvesmtmentRate.ToString("00000").PadLeft(5,'0').Substring(0, 5);
      }

      if (incomeTracked)
      {
        message += " ";
      }
      else
      {
        message += "N";
      }
      
      if (divRate == 100)
      {
        message += new string(' ', 6);
      }
      else
      {
        divRate = divRate  * 1000;
        message += divRate.ToString("000000").PadLeft(6, '0').Substring(0, 6);
      }
               
      ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
        dealId, 
        "L", 
        "NCS", 
        "New domestic contract", 
        "",
        actUserId, 
        false, 
        bookGroup,
        "",
        dealType,
        book,
        secId,
				"",
        quantity.ToString(),
				amount.ToString(),
        "Pending",
        "",
        "");      

      return processStatusEventArgsItem;
    }
  
    public ProcessStatusEventArgs Reply(string messageId, string messageText)
    {  
      string status = "";
      string bookGroup = "";
      string contractId = "";
      string contractType = "";
      string book = "";
      string secId = "";
      bool   hasError = false;      

      switch (messageText.Substring(2, 1))
      {
        case "A": 
          status = ParseAcceptedRequest(messageText);
          contractId = status;
          status = "OK";
          hasError = false;
          break;
        
        case "X": 
          status = "System/Processing Error";
          hasError = true; 
          break;

        case "R": 
          status  = ParseRejectedRequest(messageText);
          hasError = true; 
          break;
             
        default :
          status = "Unknown";
          hasError = true;
          break;
      }
      
      bookGroup    = messageText.Substring(11, 4);
      contractType = messageText.Substring(28, 1);
      secId        = messageText.Substring(19, 9);
      book         = messageText.Substring(15, 4);

      ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
        messageId, 
        "L",
        "NCS", 
        "", 
        "", 
        "",
        hasError, 
        "", 
        contractId, 
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

    private string ParseAcceptedRequest(string message)
    {
      string contractId;

      contractId = message.Substring(29, 9);

      return contractId;
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

            case 32: error += Errors.CCF_ERROR;
              break;

            case 33: error += Errors.CUSIP_ERROR;
              break;

            case 34: error += Errors.BOOK_ERROR;
              break;

            case 35: error += Errors.ACCOUNT_CREDITOR_ERROR;
              break;

            case 36: error += Errors.BATCH_CODE_ERROR;
              break;

            case 37: error += Errors.DELIVERY_CODE_ERROR;
              break;

            case 38: error += Errors.QUANTITY_ERROR;
              break;

            case 39: error += Errors.AMOUNT_ERROR;
              break;

            case 40: error += Errors.COLLATERAL_CODE_ERROR;
              break;

            case 41: error += Errors.EXPIRY_DATE_ERROR;
              break;

            case 42: error += Errors.RATE_ERROR;
              break;

            case 43: error += Errors.RATE_CODE_ERROR;
              break;

            case 44: error += Errors.POOL_CODE_ERROR;
              break;

            case 45: error += Errors.MARGIN_CODE_ERROR;
              break;

            case 46: error += Errors.MARGIN_ERROR;
              break;

            case 47: error += Errors.NEGOTIATED_RATE_ERROR;
              break;

            case 48: error += Errors.OTHER_BOOK_ERROR;
              break;

            case 49: error += Errors.FIXED_INVESTMENT_RATE_ERROR;
              break;

            case 50: error += Errors.INCOME_TRACKED_ERROR;
              break;

            case 51: error += Errors.DIV_RATE_ERROR;
              break;
          }
          error += Errors.ERROR_SEPERATOR;
        }
      }

      return error; 
    }
  }
}
