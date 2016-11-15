using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace winsrvInterfaceInOut
{
    [RunInstaller(true)]
    public partial class instInterfaceCallerInstaller : Installer
    {
        public instInterfaceCallerInstaller()
        {
            InitializeComponent();
        }
    }
}
