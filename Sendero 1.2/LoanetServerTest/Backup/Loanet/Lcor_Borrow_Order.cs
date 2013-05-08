// Licensed Materials - Property of Anetics, LLC.
// (C) Copyright Anetics, LLC. 2005  All rights reserved.

using System;
using Anetics.Common;
using Anetics.Process;

namespace Anetics.Loanet
{
	public class Lcor_Borrow_Order
	{
		private string message = "";

		public Lcor_Borrow_Order()
		{
		}

		public ProcessStatusEventArgs Request(		
			string  bookGroup,            
			string  book,
			string  secId,						
			long    quantity,			
			string	collateralCode,
			string	comments,
			decimal	rateMin,
			string  rateMinCode,
			string  quantityMin,
			string  priceMax,
			string  waitTime,
			bool	incomeTracked,
			decimal	divRate,
			bool	bookFlag,
			string  batchCode,
			string  poolCode,
			string  marginCode,
			decimal	margin,
			string	actUserId)
		{						
			string act = "";

			message = "XB";
			message += bookGroup.PadLeft(4, '0').Substring(0, 4);           
			message += secId.PadLeft(9, '0').Substring(0, 9);      			
			message += book.PadLeft(4, '0').Substring(0, 4);
			message += quantity.ToString().PadLeft(9, '0').Substring(0, 9);
			message += collateralCode.PadLeft(1, ' ').Substring(0, 1);
			message += comments.PadLeft(20, ' ').Substring(0, 20);
			
			if (rateMinCode.Equals("A") || rateMinCode.Equals("T"))
			{
				rateMin = 0;
			}
			
			rateMin = rateMin * 1000;
			message += rateMin.ToString("00000");
			
			message += rateMinCode.PadLeft(1, ' ').Substring(0, 1);
			
			message += quantityMin.ToString().PadLeft(9, '0').Substring(0, 9);
				
			try
			{
				decimal price = 0;
	
				if (priceMax.Equals(""))
				{
					message += "99999999";
				}
				else
				{
					price = decimal.Parse(priceMax);
					price = price * 100;
					message += price.ToString("00000000");
				}
			}
			catch
			{
				throw;
			}

			message += waitTime.PadLeft(4, '0');
			
			if (incomeTracked)
			{
				message += "O";
			}
			else
			{
				message += "N";
			}
			
			if (divRate == 100)
			{
				message += "      ";
			}
			else
			{
				divRate = divRate * 1000;
				message += divRate.ToString("000000");
			}

			message += "  ";
			
			if (bookFlag)
			{
				message += "Y";
				message += batchCode.PadLeft(1, ' ').Substring(0, 1);
				message += poolCode.PadLeft(1, ' ').Substring(0, 1);
				message += marginCode.PadLeft(1, ' ').Substring(0, 1);

				switch (marginCode)
				{
					case "%":
					case "1":
					case " ":
						margin = margin * 100 - 100;      
						message += margin.ToString("00").PadLeft(2, '0').Substring(0, 2);
						break;

					case "0":
						margin = 100 - margin * 100;      
						message += margin.ToString().PadLeft(2, '0').Substring(0, 2);
						break;
					default:
						throw new Exception("Unanticipated margin code: " + marginCode + " [NewContract_SameDay_add.Request]");
				}
			}
			
			
			act = "New Borrow Order of " + quantity.ToString("#,##0") + " for " + secId + " with " + book;
			
			ProcessStatusEventArgs processStatusEventArgs = new ProcessStatusEventArgs(Standard.ProcessId(), 
				"L",
				"ABW",
				act, 
				"",
				actUserId, 
				false, 
				bookGroup,
				"",
				"",
				book,
				secId,
				"",
				quantity.ToString(),
				"",
				"",
				"",
				"");
      
			return processStatusEventArgs;
		}
    
    
		public ProcessStatusEventArgs Reply(string messageId, string messageText)
		{
			string status				= "";
			string bookGroup			= "";       			
			string borrowSequenceNumber = "";

			bool   isError        = false;
      
			bookGroup    = messageText.Substring(11, 4);						
			
			switch (messageText.Substring(2, 1))
			{
				case "A": 
					status = "OK";
					borrowSequenceNumber = messageText.Substring(28, 6);
					isError = false;                  

					break;
        
				case "X": 
					status = "System/Processing Error";
					isError = true;
					break;

				case "R": 
					status = ParseRejectedRequest(messageText);
					isError = true;
					break;
			}
      
			ProcessStatusEventArgs processStatusEventArgs = new ProcessStatusEventArgs(messageId, 
				"L", 
				"ABW", 
				"", 
				"", 
				"", 
				isError,
				bookGroup,
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				status,
				"",				
				borrowSequenceNumber);
     
			return processStatusEventArgs;
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
						case 28: error += Errors.MESSAGE_TYPE_ERROR;
							break;

						case 29: error += Errors.BOOK_GROUP_ERROR;
							break;
            
						case 30: error += Errors.CCF_ERROR;
							break;

						case 31: error += Errors.CUSIP_ERROR;
							break;

						case 32: error += Errors.BOOK_ERROR;
							break;

						case 33: error += Errors.QUANTITY_ERROR;
							break;

						case 34: error += Errors.COLLATERAL_CODE_ERROR;
							break;
            
						case 35: error += Errors.RATE_ERROR;
							break;

						case 36: error += Errors.RATE_CODE_ERROR;
							break;

						case 37: error += Errors.QUANTITY_ERROR;
							break; 
            
						case 38: error += Errors.PRICE_ERROR;
							break;

						case 39: error += Errors.TIME_LIMIT_ERROR;
							break;
            
						case 40: error += Errors.INCOME_TRACKED_ERROR;
							break;

						case 41: error += Errors.DIV_RATE_ERROR;
							break;

						case 42: error += Errors.FILLER_ERROR;
							break; 
            
						case 43: error += Errors.ADD_INDICATOR_ERROR;
							break;

						case 44: error += Errors.BATCH_CODE_ERROR;
							break;
            
						case 45: error += Errors.POOL_CODE_ERROR;
							break;

						case 46: error += Errors.MARGIN_CODE_ERROR;
							break;            
						
						case 47: error += Errors.MARGIN_ERROR;
							break;            
					}
					error += Errors.ERROR_SEPERATOR;
				}
			}
			return error; 
		}        
	
	
		public string Message
		{
			get
			{
				return message;
			}
		}
	}
}
