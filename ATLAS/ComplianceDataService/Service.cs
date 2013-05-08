using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;

namespace StockLoan.ComplianceData
{
	public class Service : System.ServiceProcess.ServiceBase
	{
		private System.ComponentModel.Container components = null;

    private Master master;

		public Service()
		{
			InitializeComponent();
    }

		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new Service() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}

		private void InitializeComponent()
		{
			// 
			// Service
			// 
			this.AutoLog = false;
			this.CanShutdown = true;
			this.ServiceName = "StockLoan.BDData";

		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
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
        catch {}
      }
      
      master = new Master(logLevel, logFilePath);
      master.OnStart();
    }
 
		protected override void OnStop()
		{
      master.OnStop();
    }
 
    protected override void OnShutdown()
    {
      master.OnStop();
    }
  }
}
