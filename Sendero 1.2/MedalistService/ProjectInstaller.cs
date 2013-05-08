using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace Anetics.Medalist
{
    [RunInstaller(true)]
    public class ProjectInstaller : System.Configuration.Install.Installer
    {
        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller serviceInstaller;

        private System.ComponentModel.Container components = null;

        public ProjectInstaller()
        {        
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            
            this.serviceProcessInstaller.Password = null;
            this.serviceProcessInstaller.Username = null;
            this.serviceProcessInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceProcessInstaller_AfterInstall);
            
            this.serviceInstaller.DisplayName = "Anetics.Medalist";
            this.serviceInstaller.ServiceName = "Anetics.Medalist";
            /*this.serviceInstaller.ServicesDependedOn = new string[] {
                                                                      "Event Log"};*/
            this.serviceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.serviceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
                                                                              this.serviceProcessInstaller,
                                                                              this.serviceInstaller});

        }
        #endregion

        private void serviceProcessInstaller_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {

        }

        private void serviceInstaller_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {

        }
    }
}
