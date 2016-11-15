using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.Configuration;
using AuthenticationComponent.BusinessObjects;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using System.Reflection;

namespace BOSClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary> 
        [STAThread]
        static void Main()
        {
            //Method to generate Password Encryption Key.
            string sMessage;
            try
            {
                bool bContinue = true;
                string[] args = Environment.GetCommandLineArgs();
                foreach (string arg in args)
                {
                    switch (arg.Split('=')[0].ToLower())
                    {
                        case "-u":
                            bContinue = false;
                            Process.Start(new ProcessStartInfo(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\msiexec.exe", "/x" + arg.Split('=')[1]));
                            break;
                    }
                }
                if (bContinue)
                {
                    User.GeneratePasswordEncryptionKey();
                    if (!Common.GetDayBegin(out sMessage))
                    {
                        MessageBox.Show(sMessage);
                        return;
                    }
                    string AppCulture = "en-US";
                    DataTable dtCulture = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("APPLICATIONCULTURE", 0, 0, 0));
                    if (dtCulture != null && dtCulture.Rows.Count == 2)
                        AppCulture = dtCulture.Rows[1]["keyvalue1"].ToString();
                    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(AppCulture);
                    

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Common.LocationCode = ConfigurationManager.AppSettings["LocationCode"];

                    #region To Initialize Location Object
                    DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.AllLocations, new ParameterFilter(Common.LocationCode, 0, 0, 0));
                    if (dtLocations != null && dtLocations.Rows.Count > 0)
                    {
                        Common.CurrentLocationId = Convert.ToInt32(dtLocations.Rows[0]["LocationId"]);
                        Common.CurrentLocationTypeId = Convert.ToInt32(dtLocations.Rows[0]["LocationConfigId"]);
                        Common.RegisteredAddressLocationId = Convert.ToInt32(dtLocations.Rows[0]["RegAddLocationId"]);
                        Common.EMAILID = Convert.ToString(dtLocations.Rows[0]["EmailID1"]);
                    }
                    #endregion
                    Common.Version = GetVersion();
                    Common.TerminalCode = ConfigurationManager.AppSettings["TerminalCode"];
                    Common.AppType = ConfigurationManager.AppSettings["AppType"];
                    Common.LoginModuleCode = ConfigurationManager.AppSettings["LoginModuleCode"];
                    Common.LogoutModuleCode = ConfigurationManager.AppSettings["LogoutModuleCode"];
                    Common.PANNO = ConfigurationManager.AppSettings["PANNO"] != null ? ConfigurationManager.AppSettings["PANNO"] : "";
                    Common.MessagePath = Application.StartupPath + "/App_Data/Messages.xml";
                    DataTable dtRoundings = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("ROUNDING", 0, 0, 0));
                    foreach (DataRow dr in dtRoundings.Rows)
                    {
                        switch (Convert.ToInt32(dr["keycode1"]))
                        {
                            case 1:
                                Common.DisplayAmountRounding = Convert.ToInt32(dr["keyvalue1"]);
                                break;
                            case 2:
                                Common.DBAmountRounding = Convert.ToInt32(dr["keyvalue1"]);
                                break;
                            case 3:
                                Common.DisplayQtyRounding = Convert.ToInt32(dr["keyvalue1"]);
                                break;
                            case 4:
                                Common.DBQtyRounding = Convert.ToInt32(dr["keyvalue1"]);
                                break;
                        }
                    }
                    //Common.LocationType = ConfigurationManager.AppSettings["LocationCode"];
                    List<CoreComponent.BusinessObjects.MenuItem> mnuItem = CoreComponent.BusinessObjects.MenuItem.GetLocTypeBasedModules(Common.LocationCode, Common.AppType);
                    if (mnuItem.Count > 0)
                    {
                          
                        Application.Run(new frmMDIMain(mnuItem));
                        //Application.Run(new PurchaseComponent.UI.Test());
                        //  Application.Run(new CoreComponent.UI.frmAppLocModLink());
                        //Application.Run(new PackUnpackComponent.UI.frmPackUnpack());
                    }
                    else
                    {
                        //Application.Run(new CoreComponent.UI.frmAppLocModLink());
                        MessageBox.Show(Common.GetMessage("2006"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            catch (Exception ex)
            {
                if (string.Compare(ex.Message, "VAL0132", true) == 0)
                    MessageBox.Show(Common.GetMessage("VAL0132"));
                else if (ex.Message.IndexOf("30001", 0) >= 0)
                {
                    Common.LogException(ex);
                    MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    //    MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    //}
                    Common.LogException(ex);
                    MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static string GetVersion()
        {
            Assembly obj = Assembly.GetExecutingAssembly();
            Version Var = obj.GetName().Version;
            return Var.ToString();
        }
    }
}
