using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Xml;
using System.IO;


namespace VestigePOS.Config
{
    [RunInstaller(true)]
    public partial class POSInstaller : Installer
    {
        
        private const string AppConfigFile = @"\POSClient.exe.config";
        private const string InstallDir = "installfolder";
        private const string LocationId = "LocationID";
        private const string TerminalId = "TerminalID";
        private const string LocalServer = "LocalServer";
        private const string LocalDB = "LocalDB";
        private const string LocalUser = "LocalUser";
        private const string LocalPassword = "LocalPass";

        public POSInstaller()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }
        
        public override void Commit(IDictionary savedState)
        {
            string ti = string.Empty;
            
            base.Commit(savedState);

            ti = Context.Parameters[TerminalId].Trim().ToUpper().Replace(" ", "");
            ti = ti.Length > 50 ? ti.Substring(0, 50) : ti;

            // Location ID
            SetAppSetting("LocationCode", Context.Parameters[LocationId].Trim().ToUpper().Replace(" ", ""), Context.Parameters[InstallDir].Trim().TrimEnd('/', '\\') + AppConfigFile);

            // Terminal ID
            SetAppSetting("TerminalCode", ti, Context.Parameters[InstallDir].Trim().TrimEnd('/', '\\') + AppConfigFile);

            // Conn String
            SetConnString("POSDB", string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", Context.Parameters[LocalServer], Context.Parameters[LocalDB], Context.Parameters[LocalUser], Context.Parameters[LocalPassword]), Context.Parameters[InstallDir].Trim().TrimEnd('/', '\\') + AppConfigFile);

        }

        private static void SetAppSetting(string key, string value, string configFile)
        {
            try
            {
                XmlDataDocument config = new XmlDataDocument();
                config.Load(configFile);

                XmlNodeList nl = config.SelectNodes("//configuration/appSettings/add");// GetElementsByTagName("appSettings")[0].ChildNodes;

                for (int index = 0; index < nl.Count; index++)
                {
                    if (nl[index].Attributes["key"].Value.ToLower().CompareTo(key.Trim().ToLower()) == 0)
                        nl[index].Attributes["value"].Value = value;
                }
                config.Save(configFile);
            }
            catch (IOException) { }
        }

        private static void SetConnString(string key, string value, string configFile)
        {
            try
            {
                XmlDataDocument config = new XmlDataDocument();
                config.Load(configFile);

                XmlNodeList nl = config.SelectNodes("//configuration/connectionStrings/add");// GetElementsByTagName("appSettings")[0].ChildNodes;

                for (int index = 0; index < nl.Count; index++)
                {
                    if (nl[index].Attributes["name"].Value.ToLower().CompareTo(key.Trim().ToLower()) == 0)
                        nl[index].Attributes["connectionString"].Value = value;
                }
                config.Save(configFile);
            }
            catch (IOException) { }
        }
    }
}
