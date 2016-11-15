using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using Vinculum.Framework.DataTypes;
using Vinculum.Framework.Data;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.BusinessObjects
{
    public class MenuItem
    {

        public MenuItem()
        {
            m_moduleFunctions = new List<MenuItemFunction>();
            m_menuItemParamList = new List<MenuItemParameter>();
        }
        //Add 
        //private const string SP_NAME = "usp_GetLocationTypeBasedModules_Test";
        private const string SP_NAME = "usp_GetLocationTypeBasedModules";

        private string XML_FILE_PATH = Application.StartupPath + "\\APP_DATA\\MenuClass.xml";

        private List<MenuItemParameter> m_menuItemParamList;

        public List<MenuItemParameter> MenuItemParamList
        {
            get { return m_menuItemParamList; }
            set { m_menuItemParamList = value; }
        }
        private int m_recordId;

        public int RecordId
        {
            get { return m_recordId; }
            set { m_recordId = value; }
        }
        private int m_appType;

        public int AppType
        {
            get { return m_appType; }
            set { m_appType = value; }
        }
        private int m_locType;

        public int LocType
        {
            get { return m_locType; }
            set { m_locType = value; }
        }
        private string m_menuName;

        public string MenuName
        {
            get { return m_menuName; }
            set { m_menuName = value; }
        }
        private int m_parentId;

        public int ParentId
        {
            get { return m_parentId; }
            set { m_parentId = value; }
        }
        private int m_seqNo;

        public int SeqNo
        {
            get { return m_seqNo; }
            set { m_seqNo = value; }
        }
        private int m_moduleId;

        public int ModuleId
        {
            get { return m_moduleId; }
            set { m_moduleId = value; }
        }
        private string m_moduleCode;

        public string ModuleCode
        {
            get { return m_moduleCode; }
            set { m_moduleCode = value; }
        }
        private string m_assemblyName;

        public string AssemblyName
        {
            get { return m_assemblyName; }
            set { m_assemblyName = value; }
        }
        private string m_className;

        public string ClassName
        {
            get { return m_className; }
            set { m_className = value; }
        }
        private List<MenuItemFunction> m_moduleFunctions;

        public List<MenuItemFunction> ModuleFunctions
        {
            get { return m_moduleFunctions; }
            set { m_moduleFunctions = value; }
        }

        public static List<MenuItem> GetLocTypeBasedModules(string locationCode, string appType)
        {
            try
            {
                string errorMessage = string.Empty;
                DBParameterList dbParam;
                List<MenuItem> lMenuItems = new List<MenuItem>();
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    // initialize the parameter list object
                    dbParam = new DBParameterList();

                    // add the relevant 2 parameters
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, locationCode, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_DATA2, Convert.ToInt32(appType), DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dbParam.Add(new DBParameter("@Version", Common.Version, DbType.String));
                    dbParam.Add(new DBParameter("@Apptype", (Common.ApplicationType)Convert.ToInt32(Common.AppType), DbType.String));
                    DataSet ds = dtManager.ExecuteDataSet(SP_NAME, dbParam);

                    // update database message
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    if (errorMessage != string.Empty)
                    {
                        throw new Exception(errorMessage);
                    }
                    else
                    {
                        if (ds.Tables.Count > 0)
                        {
                            
                            XmlDocument mnuDoc = new XmlDocument();
                            mnuDoc.Load(Application.StartupPath + "\\APP_DATA\\MenuClass.xml");
                            //DataSet xmlDs = new DataSet();
                            //xmlDs.ReadXml(XML_FILE_PATH);
                            ds.Relations.Add(new DataRelation("ModFunc", ds.Tables[0].Columns["RECORDID"], ds.Tables[1].Columns["RECORDID"]));
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                MenuItem mnu = new MenuItem();
                                mnu.RecordId = Convert.ToInt32(dr["RECORDID"]);
                                mnu.AppType = Convert.ToInt32(dr["APPTYPE"]);
                                mnu.LocType = Convert.ToInt32(dr["LOCTYPE"]);
                                mnu.MenuName = dr["MENUNAME"].ToString();
                                mnu.ParentId = Convert.ToInt32(dr["PARENTID"].ToString().Trim().Length == 0 ? Common.INT_DBNULL : dr["PARENTID"]);
                                mnu.SeqNo = Convert.ToInt32(dr["SEQNO"]);
                                mnu.ModuleId = Convert.ToInt32(dr["MODULEID"]);
                                mnu.ModuleCode = dr["MODULECODE"].ToString();
                                mnu.AssemblyName = dr["ASSEMBLYNAME"].ToString();
                                mnu.ClassName = dr["CLASSNAME"].ToString();
                                if (dr["PARAMETERID"].ToString() != string.Empty)
                                {
                                    mnu.m_menuItemParamList.Add(new MenuItemParameter(dr["PARAMETERID"].ToString(), dr["PARAMVALUE"].ToString()));
                                }
                                DataRow[] childRows = dr.GetChildRows("ModFunc");
                                foreach(DataRow row in childRows)
                                {
                                    MenuItemFunction mif = new MenuItemFunction();
                                    mif.FunctionId = Convert.ToInt32(row["FUNCTIONID"]);
                                    mif.FunctionCode = row["FUNCTIONCODE"].ToString();
                                    mnu.m_moduleFunctions.Add(mif);
                                }
                                //if (mnu.ModuleCode != string.Empty)
                                //{
                                //    XmlNode node = mnuDoc.SelectSingleNode("//Menu/MenuItem[@ID='" + mnu.ModuleCode + "']");
                                //    if (node != null)
                                //    {
                                //        mnu.AssemblyName = node.Attributes.GetNamedItem("AssemblyName").Value.ToString();
                                //        mnu.ClassName = node.Attributes.GetNamedItem("ClassName").Value.ToString();
                                //        if (node.ChildNodes.Count > 0)
                                //        {
                                //            foreach (XmlNode n in node.ChildNodes)
                                //            {
                                //                if (n.Name == "MenuItemParameter")
                                //                    mnu.m_menuItemParamList.Add(new MenuItemParameter(n.Attributes["ID"].Value, n.Attributes["Value"].Value));
                                //            }
                                //        }
                                //    }
                                //}
                                lMenuItems.Add(mnu);
                            }
                            
                        }
                    }
                }
                return lMenuItems;
            }
            catch
            {
                throw;
            }
        }
    }
}
