// Licensed Materials - Property of Anetics, LLC.
// Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

using System;
using System.Threading;

namespace Anetics.Courier
{
  class TestHarness
  {
    [STAThread]
    static void Main(string[] args)
    {
      string userCommand;

      int logLevel = -1; // File-based logging verbosity index.
      string logFilePath = ""; // Where to write log file if other than current working directory,

      foreach (string arg in args)
      {
        try
        {
          if (arg.Length > 1) // Then this must be log file path.
          {
            logFilePath = arg;
          }            
          else // Most likely the integer value for log verbosity level.
          {
            logLevel = int.Parse(arg); // 0 = No log file; 1 = Write events to file; 2+ = Verbose log writing.
          }
        }
        catch {}
      }
      
      Master master = new Master(logLevel, logFilePath);

      Console.WriteLine("");
      Console.WriteLine("Test harness is running, type 'exit' then press <return> to exit, or...");
      Console.WriteLine("");
      Console.WriteLine("Type 'start' then press <return> to start service.");
      Console.WriteLine("Type 'stop' then press <return> to stop service.");
      Console.WriteLine("");

      while(true) 
      {
        userCommand = Console.ReadLine();

        switch (userCommand.ToLower()) 
        {
          case "start":
            master.OnStart();
            Console.WriteLine("--> OnStart call has completed.");
            break;
          case "stop":
            master.OnStop();
            Console.WriteLine("--> OnStop call has completed.");
            break;
          case "exit":
            master.OnStop();
            Console.WriteLine("--> Bye...");
            Thread.Sleep(1500);
            return;
          default:
            Console.WriteLine("--> Unknown command, try again.");
            break;
        }
      }
    }
  }
}
