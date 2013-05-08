using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace StockLoan.XflNotification
{
  public partial class Service : ServiceBase
  {
    private Master master = null;

    public Service()
    {
      InitializeComponent();
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    private static void Main()
    {
        ServiceBase[] ServicesToRun;

        // More than one user Service may run within the same process. To add
        // another service to this process, change the following line to
        // create a second service object. For example,
        //
        //   ServicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
        //
        ServicesToRun = new ServiceBase[] { new Service() };

        ServiceBase.Run(ServicesToRun);
    }

    protected override void OnStart(string[] args)
    {
      int logLevel = -1;
      string logFilePath = "";

      foreach (string arg in args)
      {
        try
        {
          if (arg.Length > 1) logFilePath = arg; // If logLevel > 0, where to write log file if other than default.
          else logLevel = int.Parse(arg); // 0 = No log file; 1 = Write events to file; 2+ = Verbose log writing.
        }
        catch { }
      }

      master = new Master(logLevel, logFilePath);
      master.OnStart();
    }

    protected override void OnStop()
    {
      if (null != master)
        master.OnStop();
    }
    protected override void OnShutdown()
    {
      if (null != master)
        master.OnStop();
    }
  }
}
