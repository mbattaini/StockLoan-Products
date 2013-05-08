using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace StockLoan.Inventory
{
    [RunInstallerAttribute(true)]
    public class InventoryServiceInstaller : Installer
    {
        
        public InventoryServiceInstaller()
        {
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            // Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalService;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //System.Net.CredentialCache.DefaultNetworkCredentials


            //serviceProcessInstaller.Account = ServiceAccount.User;
            //serviceProcessInstaller.Username = @"PENDAL_NT\BPritchard";
            //serviceProcessInstaller.Password = @"sh@wk1ng";


            // Service Information
            serviceInstaller.DisplayName = "Inventory Service";
            serviceInstaller.StartType = ServiceStartMode.Manual;


            // This must be identical to the WindowsService.ServiceBase name
            // set in the constructor of WindowsService.cs
            serviceInstaller.ServiceName = "InventoryService";

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }


    }
}
