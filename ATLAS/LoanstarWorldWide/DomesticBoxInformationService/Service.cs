using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

using StockLoan.DomesticBoxInformation;

namespace StockLoan.DomesticBoxInformation
{
    public partial class Service : ServiceBase
    {
        private Master master;

        public Service()
        {
            InitializeComponent();
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
            master.OnStop();
        }
        protected override void OnShutdown()
        {
            master.OnStop();
        }
    }
}
