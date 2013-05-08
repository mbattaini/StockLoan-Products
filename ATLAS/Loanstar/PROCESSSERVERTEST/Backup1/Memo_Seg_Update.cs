// Licensed Materials - Property of Anetics, LLC.
// (C) Copyright Anetics, LLC. 2005 All rights reserved.

using System;
using Anetics.Common;
using Anetics.Process;

namespace Anetics.Loanet
{
	public class Memo_Seg_Update
	{
		private string message = "M1";

		public Memo_Seg_Update()
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
			string	messageId,
			string  bookGroup,            
			string  secId,
			long		quantity,
			string	actionCode,
			string	dtcSerialNumber,
			string	dtcInputSequence,
			string	comments,
			string  actUserId)
		{
			string actionCodeDescription = "";

			message += bookGroup.PadLeft(4, ' ').Substring(0, 4);      
			message += secId.PadLeft(9, '0').Substring(0, 9);  
			message += quantity.ToString().PadLeft(9, '0').Substring(0, 9);
      
			switch (actionCode)
			{
				case "A": actionCode = "A";
					actionCodeDescription  = "Addition";
					break;

				case "S": actionCode = "S";
					actionCodeDescription  = "Subtraction";
					break;

				case "O":	actionCode = "O";
					actionCodeDescription  = "Overlay";
					break;

				default:	actionCode = "";
					actionCodeDescription = "Unknown";
					break;
			}
            
			message += actionCode.PadLeft(1, ' ').Substring(0, 1);
			message += new string(' ', 1);
			message += dtcSerialNumber.PadLeft(7, ' ').Substring(0, 7);
			message += dtcInputSequence.PadLeft(5, ' ').Substring(0, 5);
			message += comments.PadRight(80, ' ').Substring(0, 80);
			
			ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
				Standard.ProcessId(), 
				"L", 
				"MSU", 
				"Memo Seg Update " + actionCodeDescription,
				"", 
				actUserId, 
				false,
				bookGroup,
				"",
				"",
				"",
				secId,
				"",
				quantity.ToString(),
				"",
				"Pending",
				"",
				messageId);

			return processStatusEventArgsItem;
		}
  
		public ProcessStatusEventArgs  Reply(string messageId, string messageText)
		{
			string status = "";
			string bookGroup    = "";
			string systemTime = "";
			string secId = "";
			string quantity = "";
			bool   hasError = false;

			systemTime   = messageText.Substring(3, 8);
			bookGroup    = messageText.Substring(11, 4);
			secId				 = messageText.Substring(15, 9);

			switch (messageText.Substring(2, 1))
			{
				case "A": 
					status = "OK";
          
					try
					{
						quantity = messageText.Substring(24, 9);
					}
					catch 
					{
						quantity = "";
					}

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
				"MSU", 
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
				quantity,
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
						case 24: error += Errors.MESSAGE_TYPE_ERROR;
							break;

						case 25: error += Errors.BOOK_GROUP_ERROR;
							break;

						case 26: error += Errors.CUSIP_ERROR;
							break;

						case 27: error += Errors.QUANTITY_ERROR;
							break;

						case 28: error += Errors.INVALID_ACTION_CODE;
							break;
					}

					error += Errors.ERROR_SEPERATOR;
				}
			}

			return error; 
		}
	}
}