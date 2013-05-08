// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2005  All rights reserved.

using System;
using Anetics.Common;
using Anetics.Process;

namespace Anetics.Loanet
{

	public class General_System_Reject
	{
		private string dbCnStr;    
      
    public General_System_Reject(string dbCnStr)
		{
      this.dbCnStr = dbCnStr;
		}

    public ProcessStatusEventArgs Activity(string message)
    {
      string rejectCode   = message.Substring(2, 1);
      string timestamp    = message.Substring(3, 8);
      string inputMessage = message.Substring(11, 300);                  
      string status       = "";

      switch (rejectCode)
      {
        case "S": 
          status = "Security Failure";
          break;
        case "P": 
          status = "Past Processsing Cutoff";
          break;
        case "U": 
          status = "Unknown Message Type";
          break;
        default:
          status = "Unknown";
          break;
      }
      
      ProcessStatusEventArgs processStatusEventArgsItem= new ProcessStatusEventArgs(
        Standard.ProcessId(),
        "L",
        "GSR",
        status,
        "",
        "ADMIN",
        true,
        "",
        "",
        "",
		"",
		"",
        "",
        "", 
        "",
        inputMessage,
        "",
        "");

      return processStatusEventArgsItem;
    }   
	}
}
