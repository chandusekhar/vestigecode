using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using AuthenticationComponent.BusinessObjects;
using POSClient.BusinessObjects;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace POSClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
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
                    
                    Common.LocationCode = ConfigurationManager.AppSettings["LocationCode"];
                    Common.AppType = ConfigurationManager.AppSettings["AppType"];
                    Common.TerminalCode = ConfigurationManager.AppSettings["TerminalCode"];
                    Common.LoginModuleCode = ConfigurationManager.AppSettings["LoginModuleCode"];
                    Common.LogoutModuleCode = ConfigurationManager.AppSettings["LogoutModuleCode"];
                    Common.TINNO = ConfigurationManager.AppSettings["TINNO"];
                    Common.PANNO = ConfigurationManager.AppSettings["PANNO"];
                    Common.MessagePath = Application.StartupPath + "/App_Data/Messages.xml";
                    Common.ForSkinCareItem = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSkinCarePOS"]); 
                    Common.Version = GetVersion();
                    #region To Initialize Location Object
                    DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.AllLocations, new ParameterFilter(Common.LocationCode, 0, 0, 0));
                    if (dtLocations != null && dtLocations.Rows.Count > 0)
                    {
                        Common.CurrentLocationId = Convert.ToInt32(dtLocations.Rows[0]["LocationId"]);
                        Common.CurrentLocationTypeId = Convert.ToInt32(dtLocations.Rows[0]["LocationConfigId"]);
                        Common.EMAILID = Convert.ToString(dtLocations.Rows[0]["EmailID1"]);
                        Common.TINNO = Convert.ToString(dtLocations.Rows[0]["TINNo"]);
                        Common.RegisteredAddressLocationId = Convert.ToInt32(dtLocations.Rows[0]["RegAddLocationId"]);
                        Common.IsMiniBranchLocation =  Convert.ToInt32(dtLocations.Rows[0]["IsMiniBranch"]);
                        Common.CountryID = dtLocations.Rows[0]["CountryID"].ToString();
                    }
                    #endregion
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
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    string errorMessage = string.Empty;
                    if (Common.CheckTerminalIsActive(ref errorMessage))
                    {
                        //Application.Run(new POSClient.UI.MDIPOS());
                        PreventOpenTwiceApp();
                    }
                    else
                    {
                        if (string.Compare(errorMessage, "VAL0132", true) == 0)
                            MessageBox.Show(Common.GetMessage("VAL0132"));
                        else
                            MessageBox.Show(Common.GetMessage("40023", Common.LocationCode, Common.TerminalCode), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Show Message
                    }
                    //Application.Run(new POSClient.UI.frmDistributorSearch());
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Show Message
            }
        
        }

        private static string GetVersion()
        {
            Assembly obj = Assembly.GetExecutingAssembly();
            Version Var = obj.GetName().Version;
            return Var.ToString();
        }
        

        private static void PreventOpenTwiceApp()
        {
            bool instanceCountOne = false;
            Mutex mutex = new Mutex(true, "Pos", out instanceCountOne);
            try
            {
                using (mutex)
                {
                    if (instanceCountOne)
                        Application.Run(new POSClient.UI.MDIPOS());
                    //else
                      //  MessageBox.Show("The application is already running");
                }
            }
            catch { }
            finally
            {
                try { mutex.ReleaseMutex(); }
                catch { }
            }
        }
    }
}
