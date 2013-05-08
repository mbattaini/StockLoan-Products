// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using Anetics.Common;
using Anetics.Process;

namespace Anetics.Loanet
{
  public class Contract_Maintenance
  {
    private string message = "51";
    
    public Contract_Maintenance()
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
			string contractId,      
			string batchCode,
			string deliveryDate,
			string marginCode,
			string margin,
			string divRate,
			string incomeTracked,
			string expiryDate,
			string comments,
			string actUserId)
		{     
			string tempDate = "";
			string act = "Contract Maintenance: ";

			message += "     "; // version, spaces use most current version of message
			message += bookGroup.PadRight(4, '0');
			message += contractType.Substring(0, 1);
			message += contractId.PadLeft(9, '0').Substring(0, 9);			            
			message += batchCode.PadRight(1, ' ').Substring(0, 1);
      
			if (!deliveryDate.Equals(""))
			{
				message += "Y";				

				try
				{
					tempDate = DateTime.Parse(deliveryDate).ToString("MMddyy");       
					act += "Delivery Date ";
				}
				catch (Exception error)
				{
					Log.Write(error.Message + " Invalid delivery date, " + deliveryDate + ", delivery date will not be updated. [Contract_Maintenance.Request]", Log.Error, 1);        				
				}
				
				message += tempDate;				
			}
			else
			{
				message += " ";
				message += new string(' ', 6);
			}

			if (!margin.Equals(""))
			{
				message += "Y";
				message += marginCode.PadRight(1, ' ').Substring(0, 1);      
				
				decimal localMargin = decimal.Parse(margin);

				switch (marginCode)
				{
					case "%":
					case "1":
						localMargin = localMargin * 100 - 100;      
						message += localMargin.ToString("00").PadLeft(2, '0').Substring(0, 2);
						break;

					case "0":
						localMargin = 100 - localMargin * 100;      
						message += localMargin.ToString().PadLeft(2, '0').Substring(0, 2);
						break;
					default:
						throw new Exception("Unanticipated margin code: " + marginCode + " [NewContract_SameDay_add.Request]");
				}

				act += "Margin & Margin Code ";
			}
			else
			{
				message += " ";
				message += " ";
				message += new string(' ', 3);
			}

			if (!divRate.Equals(""))
			{
				message += "Y";

				decimal localDivRate;

				localDivRate = decimal.Parse(divRate);
				if ( localDivRate == 100)
				{
					message += new string(' ', 6);
				}
				else
				{
					localDivRate = localDivRate  * 1000;
					message += localDivRate.ToString("000000").PadLeft(6, '0').Substring(0, 6);
				}			

				act += "DivRate ";
			}
			else
			{
				message += " ";
				message += new string(' ', 6);				
			}


			if (!incomeTracked.Equals(""))
			{
				message += "Y";

				bool localIncomeTracked = bool.Parse(incomeTracked);

				if (localIncomeTracked)
				{
					message += " ";
				}
				else
				{
					message += "N";
				}

				act += "Income Tracked ";
			}
			else
			{
				message += " ";
				message += " ";		
			}
		
			if (!expiryDate.Equals(""))
			{			
				message += "Y";			

				try
				{
					tempDate = DateTime.Parse(expiryDate).ToString("MMddyy");       
					act += "Delivery Date ";
				}
				catch (Exception error)
				{
					Log.Write(error.Message + " Invalid expiry date, " + error + ", expiry date will not be updated. [Contract_Maintenance.Request]", Log.Error, 1);        				
				}
				
				message += tempDate;
				act += "Expiry Date ";
			}
			else
			{
				message += " ";
				message += new string(' ', 6);		
			}
		
			if (!comments.Equals(""))
			{
				message += "Y";
				comments = comments.PadRight(20, ' ').Substring(0, 20);								
				act += "Comments ";
			}
			else
			{
				message += " ";
				message += new string(' ', 20);		
			}
			

			ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
				Standard.ProcessId(),
				"L",
				"CTM",
				act,
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
			string status       = "";
			string bookGroup    = "";
			string contractId   = "";
			string contractType = "";			
			bool   hasError      = false;

			bookGroup    = messageText.Substring(16, 4);
			contractType = messageText.Substring(20, 1);
			contractId   = messageText.Substring(21, 9);						
     
			switch (messageText.Substring(7, 1))
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
				"CTM",
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
            case 30: error += Errors.VERSION_ERROR;
              break;

            case 31: error += Errors.BOOK_GROUP_ERROR;;
              break;

            case 32: error += Errors.CONTRACT_TYPE_ERROR;
              break;
            
            case 33: error += Errors.CONTRACT_ID_ERROR;
              break;
            
            case 34: error += Errors.BATCH_CODE_ERROR;
              break;

            case 35: error += Errors.DELIVERY_DATE_ERROR;
              break;
							
						case 36: error += Errors.MARGIN_CODE_ERROR;
							break;

						case 37: error += Errors.MARGIN_ERROR;
							break;

						case 38: error += Errors.DIV_RATE_ERROR;
							break;

						case 39: error += Errors.INCOME_TRACKED_ERROR;
							break;

						case 40: error += Errors.EXPIRY_DATE_ERROR;
							break;
						
						case 41: error += Errors.SECURITY_TYPE_ERROR;
							break;

						case 42: error += Errors.CONTRACT_NOT_FOUND_ERROR;
							break;

						case 43: error += Errors.FUTURE_DATE_ERROR;
							break;

						case 44: error += Errors.MARGIN_ERROR;
							break;

						case 45: error += Errors.EXPIRY_DATE_ERROR;
							break;
							
						case 46: error += Errors.EXPIRY_DATE_ERROR;
							break;

						case 47: error += Errors.NO_UPDATES_ERROR;
							break;
	
          }
          error += Errors.ERROR_SEPERATOR;
        }
      }

      return error; 
    }
  }
}
