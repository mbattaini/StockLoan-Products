namespace StockLoan.PreBorrow
{
  partial class ProjectInstaller
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
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
      // 
      // serviceInstaller
      // 
      this.serviceInstaller.DisplayName = "StockLoan.PreBorrow";
      this.serviceInstaller.ServiceName = "StockLoan.PreBorrow";
      this.serviceInstaller.ServicesDependedOn = new string[] {
        "Event Log"};
      this.serviceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
      // 
      // ProjectInstaller
      // 
      this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller});

    }

    #endregion

    private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
    private System.ServiceProcess.ServiceInstaller serviceInstaller;
  }
}