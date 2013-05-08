// Licensed Materials - Property of Anetics, LLC.
// (C) Copyright Anetics, LLC. 2005 All rights reserved.

using System;
using Anetics.Common;
using Anetics.Process;

namespace Anetics.Loanet
{
	public class Memo_Seg_Update_Datagram
	{
		public Memo_Seg_Update_Datagram()
		{
		}
  
		public ProcessStatusEventArgs Activity(string message)
		{
			string systemTime							= message.Substring(3,8);
			string clientId               = message.Substring(12, 4);			
			string pendMadeFlag						= message.Substring(22, 1);						
			string secId                  = message.Substring(29, 9);      
			string quantity               = message.Substring(38, 9);												
			string dtcOriginatingSystem		= message.Substring(47, 1);
			string dtcActivityType        = message.Substring(48, 3);
			string dtcActionCode	        = message.Substring(51, 1);
			string dtcInputSequence       = message.Substring(74, 5);			
			string comment                = message.Substring(79, 56);
			string act										= "";

			switch (pendMadeFlag)
			{
				case "P": act = "Memo Seg Update pending " + quantity;
									break;
				case "M":	act = "Memo Seg Update made " + quantity;
									break;
				case "C":	act = "Memo Seg Update Made prior pending " + quantity;
									break;
				case "K":	act = "Memo Seg Update pending killed " + quantity;
									break;
				case "U":	act = "Memo Seg Update user entry " + quantity;
									break;
				default:	act = "Memo Seg Update unknown " + quantity;
									break;
			}
						
			ProcessStatusEventArgs processStatusEventArgsItem = new ProcessStatusEventArgs(
				Standard.ProcessId(), 
				"L", 
				"MSD", 
				act,
				"", 
				"ADMIN", 
				false,
				clientId,
				"",
				"",
				"",
				secId,
				"",
				quantity,
				"",
				"",
				"[" + dtcActivityType + "|" + pendMadeFlag + "|" + dtcActionCode + "]",
				pendMadeFlag);

			return processStatusEventArgsItem;
		}   
	}
}