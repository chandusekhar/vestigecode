using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using winsrvInterfaceInOut.BusinessObjects;

//vinculum framework namespace(s)
using CoreComponent.Core.BusinessObjects;


namespace winsrvInterfaceInOut
{
    public partial class srvInterfaceCaller : ServiceBase
    {
        public srvInterfaceCaller()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //First run Interface-IN stored-procedure
            try
            {
                CallInterfaceProcess objInterface = new CallInterfaceProcess();
                objInterface.RunInterfaceIn();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }

            //Then, run Interface-OUT stored-procedure 
            try
            {
                CallInterfaceProcess objInterface = new CallInterfaceProcess();
                objInterface.RunInterfaceOut();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
        }

        protected override void OnStop()
        {
        }
    }
}
