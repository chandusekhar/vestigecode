namespace winsrvInterfaceInOut
{
    partial class instInterfaceCallerInstaller
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
            this.srvprocinstInterfaceCaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.srvinstInterfaceCaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // srvprocinstInterfaceCaller
            // 
            this.srvprocinstInterfaceCaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.srvprocinstInterfaceCaller.Password = null;
            this.srvprocinstInterfaceCaller.Username = null;
            // 
            // srvinstInterfaceCaller
            // 
            this.srvinstInterfaceCaller.Description = "Call Interface-In and Interface-Out";
            this.srvinstInterfaceCaller.DisplayName = "Interface Caller";
            this.srvinstInterfaceCaller.ServiceName = "InterfaceCaller";
            this.srvinstInterfaceCaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // instInterfaceCallerInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.srvprocinstInterfaceCaller,
            this.srvinstInterfaceCaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller srvprocinstInterfaceCaller;
        private System.ServiceProcess.ServiceInstaller srvinstInterfaceCaller;
    }
}