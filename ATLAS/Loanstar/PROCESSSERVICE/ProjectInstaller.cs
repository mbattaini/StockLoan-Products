
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace Anetics.Medalist
{
	/// <summary>
	/// Summary description for ProjectInstaller.
	/// </summary>
	[RunInstaller(true)]
	public class ProjectInstaller : System.Configuration.Install.Installer
	{
    private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
    private System.ServiceProcess.ServiceInstaller serviceInstaller;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ProjectInstaller()
		{
			// This call is required by the Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller
            // 
            this.serviceProcessInstaller.Password = null;
            this.serviceProcessInstaller.Username = null;
            this.serviceProcessInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceProcessInstaller_AfterInstall);
            // 
            // serviceInstaller
            // 
            this.serviceInstaller.DisplayName = "StockLoan.Medalist";
            this.serviceInstaller.ServiceName = "StockLoan.Medalist";
            this.serviceInstaller.ServicesDependedOn = new string[] {
        "Event Log"};
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
