using System;
using System.Data;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.ServiceProcess;
using System.ComponentModel;
using System.Collections.Generic;

using StockLoan.Common;

namespace StockLoan.Inventory
{
    public delegate void ImportWorkerReadyDelegate();
    public delegate void ImportCompleteDelegate();

    public partial class InventoryService : ServiceBase
    {
        InventoryImportController importer;
        
        public InventoryService()
        {
            InitializeComponent();

            this.ServiceName = "InventoryService";
            this.EventLog.Log = "Application";
            this.AutoLog = true;

            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;

            this.CanHandlePowerEvent = false;
            this.CanHandleSessionChangeEvent = false;

            Log.Level = StockLoan.Common.Standard.ConfigValue("LoggerLevel");
            Log.FilePath = StockLoan.Common.Standard.ConfigValue("InventoryLocalDestination");
            Log.Name = Assembly.GetExecutingAssembly().GetName().Name;

            CreateImporter();

        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Log.Write("Starting WorkerThread" + " [InventoryService.OnStart]", Log.Information, 3);

                WaitCallback wcImport = new WaitCallback(StartImports);
                System.Threading.ThreadPool.QueueUserWorkItem(wcImport);                
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryService.OnStart]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }

        }

        protected override void OnStop()
        {
            StopImports();
        }

        protected override void OnPause()
        {
            importer.PauseImports();
        }

        protected override void OnContinue()
        {
            importer.ResumeImports();
        }

        protected override void OnShutdown()
        {
            StopImports();
        }

        //protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        //{
        //}
        //protected override void OnCustomCommand(int command)
        //{
        //}
        //protected override void OnSessionChange(SessionChangeDescription changeDescription)
        //{
        //}

        private void CreateImporter()
        {
            Log.Write("Building ImportController" + " [InventoryService.CreateImporter]", Log.Information, 3);
            importer = new InventoryImportController(ImportMode.Service);
            Log.Write("ImportController Is Now Built" + " [InventoryService.CreateImporter]", Log.Information, 3);
        }

        private void StartImports(Object stateInfo)
        {
            try
            {
                if (null != importer)
                {
                    CreateImporter();
                }
                if ((null != importer) && (importer.HasImportSiteSpecs))
                {
                    Log.Write("Starting Import Service Loop" + " [InventoryService.OnStart]", Log.Information, 3);
                    importer.StartImports();                   
                }
                else
                {
                    Log.Write("Unable to Start Import Service Loop! ImportController is either NULL or Empty" + " [InventoryService.ImportData]", Log.Error, 1);
                }

            }
            catch (Exception ex)
            {
                Log.Write(ex.Message + " [InventoryService.ImportData]", Log.Error, 1);
                Console.WriteLine(ex.Message);
            }
        }

        private void StopImports()
        {
            Log.Write("InventoryService.OnStop Called; ", Log.Information, 3);

            importer.StopImports();
            Log.Write(" InventoryImportController.ContinueServiceLoop = false; Waiting On Last Loop to Finish; ", Log.Information, 3);

            InventoryImportController.ServiceStop.WaitOne();
            Log.Write(" Last Loop Finished; Calling Dispose;", Log.Information, 3);

            importer.Dispose();
            InventoryImportController.ServiceStop.WaitOne();

            Log.Write(" Dispose Finished; Exiting.", Log.Information, 3);
            this.ExitCode = 0;
        }

    }
}
