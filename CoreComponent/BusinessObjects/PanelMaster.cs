using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;
namespace CoreComponent.BusinessObjects
{
  /// <summary>
  /// Master Classes
  /// </summary>
    public class PanelCommon
    {
        public enum DataType
        {
            Int32 = 1,
            String = 2,
            Boolean = 3,
            Decimal = 4,
            Double = 5
        }

        public enum ControlType
        {
            TextBox = 1,
            ComboBox = 2,
            CheckBox = 3,
            DateTime =4
        }

        public enum PropertyType
        {
            None=0,
            Column =1,
            Property=2
        }

        public static object InvokeMethod(MethodMaster method, ref string errorMessage)
        {
            try
            {
                if (method.AssemblyName != string.Empty && method.ClassName != string.Empty && method.MethodName != string.Empty)
                {
                    Assembly asy = Assembly.Load(method.AssemblyName);
                    if (asy != null)
                    {
                        Type t = asy.GetType(method.ClassName);
                        if (t != null)
                        {
                            MethodInfo realMethodinfo = t.GetMethod(method.MethodName);
                            if (realMethodinfo != null)
                            {
                                object obj = null;
                                object[] parameter = GetParameters(method.ParameterList, realMethodinfo);
                                if (!realMethodinfo.IsStatic)
                                {
                                    obj = Activator.CreateInstance(t);
                                    // SetProperties(ref obj, m_CurrPanel.ListSearchField);
                                }
                                return realMethodinfo.Invoke(obj, parameter);
                            }
                            else
                            {
                                errorMessage = "Method Not Found!";
                                return null;
                            }
                        }
                        else
                        {
                            errorMessage = "Class Not Found!";
                            return null;
                        }
                    }
                    else
                    {
                        errorMessage = "Assembly Not Found!";
                        return null;
                    }
                }
                else
                {
                    errorMessage = "Method Details Not Found!";
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static object InvokeMethod(MethodMaster method, List<UserSearchField> Property, ref string errorMessage)
        {
            try
            {
                if (method.AssemblyName != string.Empty && method.ClassName != string.Empty && method.MethodName != string.Empty)
                {
                    Assembly asy = Assembly.Load(method.AssemblyName);
                    if (asy != null)
                    {
                        Type t = asy.GetType(method.ClassName);
                        if (t != null)
                        {
                            MethodInfo realMethodinfo = t.GetMethod(method.MethodName);
                            if (realMethodinfo != null)
                            {
                                object obj = null;
                                object[] parameter = GetParameters(method.ParameterList, realMethodinfo);
                                if (!realMethodinfo.IsStatic)
                                {
                                    obj = Activator.CreateInstance(t);
                                    SetProperty(ref obj, Property, ref errorMessage);
                                }
                                if (!errorMessage.Trim().Equals(string.Empty))
                                    return null;
                                else
                                    return realMethodinfo.Invoke(obj, parameter);
                            }
                            else
                            {
                                errorMessage = "Method Not Found!";
                                return null;
                            }
                        }
                        else
                        {
                            errorMessage = "Class Not Found!";
                            return null;
                        }
                    }
                    else
                    {
                        errorMessage = "Assembly Not Found!";
                        return null;
                    }
                }
                else
                {
                    errorMessage = "Method Details Not Found!";
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        private static object[] GetParameters(List<MethodParameter> mpList, MethodInfo info)
        {
            try
            {
                ParameterInfo[] ParamInfo = info.GetParameters();
                object[] paramList = null;
                int i = 0;
                if (mpList != null && ParamInfo != null && ParamInfo.Length > 0 && mpList.Count > 0)
                {
                    paramList = new object[ParamInfo.Length];
                    // For Each Parameter in Method
                    foreach (ParameterInfo p in ParamInfo)
                    {
                        var query = from q in mpList where q.ParameterName == p.Name select q;
                        if (query != null)
                        {
                            MethodParameter param = (MethodParameter)query.ToList()[0];
                            paramList[i] = GetParamValue(param, p.ParameterType.Name.ToString());
                            i++;
                        }
                        else
                        {
                            paramList[i] = null;
                            i++;
                        }
                    }
                }
                return paramList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object[] GetParameters(List<MethodParameter> mpList)
        {
            try
            {
                object[] paramList = null;
                int i = 0;
                if (mpList != null && mpList.Count > 0)
                {
                    paramList = new object[mpList.Count];
                    foreach (MethodParameter p in mpList)
                    {
                        paramList[i] = GetParamValue(p);
                        i++;
                    }
                }
                return paramList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static object GetParamValue(MethodParameter param)
        {
           return GetParamValue(param, param.ParameterType.ToString());           
        }

        private static object GetParamValue(MethodParameter param, string type)
        {
            try
            {
                if (type.ToUpper() == typeof(Boolean).Name.ToUpper())
                    return Convert.ToBoolean(param.Value);

                if (type.ToUpper() == typeof(Decimal).Name.ToUpper())
                    return Convert.ToDecimal(param.Value);

                if (type.ToUpper() == typeof(Double).Name.ToUpper())
                    return Convert.ToDouble(param.Value);

                if (type.ToUpper() == typeof(Int32).Name.ToUpper())
                    return Convert.ToInt32(param.Value);

                if (type.ToUpper() == typeof(String).Name.ToUpper())
                    return Convert.ToString(param.Value);

                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private static void SetProperty(ref object obj, List<UserSearchField> PropertyList, ref string errorMessage)
        {
            try
            {
                if (obj.GetType() != null && PropertyList!=null)
                {
                    PropertyInfo[] properties = obj.GetType().GetProperties();
                    if (properties != null)
                    {
                        foreach (UserSearchField p in PropertyList)
                        {
                            var query = from q in properties where q.Name == p.Field.PropertyName select q;
                            if (query != null)
                            {

                                PropertyInfo pInfo = (PropertyInfo)query.ToList()[0];
                                //                            obj.GetType().GetProperty(p.PropertyName).SetValue(obj, p.Value == string.Empty ? p.DefaultValue : p.Value, null);
                                switch (pInfo.PropertyType.Name.ToString().ToUpper())
                                {
                                    case "STRING":
                                        {
                                            obj.GetType().GetProperty(p.Field.PropertyName).SetValue(obj, p.Value == string.Empty ? p.Field.DefaultValue : p.Value, null);
                                            break;
                                        }
                                    case "INT32":
                                        {
                                            obj.GetType().GetProperty(p.Field.PropertyName).SetValue(obj, p.Value == string.Empty ? Convert.ToInt32(p.Field.DefaultValue) : Convert.ToInt32(p.Value), null);
                                            break;
                                        }
                                    case "BOOLEAN":
                                        {
                                            obj.GetType().GetProperty(p.Field.PropertyName).SetValue(obj, p.Value == string.Empty ? Convert.ToBoolean(p.Field.DefaultValue) : Convert.ToBoolean(p.Value), null);
                                            break;
                                        }
                                    case "DECIMAL":
                                        {
                                            obj.GetType().GetProperty(p.Field.PropertyName).SetValue(obj, p.Value == string.Empty ? Convert.ToBoolean(p.Field.DefaultValue) : Convert.ToBoolean(p.Value), null);
                                            break;
                                        }
                                    case "DOUBLE":
                                        {
                                            obj.GetType().GetProperty(p.Field.PropertyName).SetValue(obj, p.Value == string.Empty ? Convert.ToBoolean(p.Field.DefaultValue) : Convert.ToBoolean(p.Value), null);
                                            break;
                                        }
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    
                }
                else
                {

                }
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class MethodMaster
    {
        #region SP Declaration
        private const string SP_PANEL_METHOD_SEARCH = "usp_GetPanelMethod";
        #endregion

        public int MethodID { get; set; }
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public string AssemblyName { get; set; }
        public List<MethodParameter> ParameterList { get; set; }
        
        public static MethodMaster GetMethodDetail(int methodID)
        {
            MethodMaster method=null;
            System.Data.DataSet dSet = new DataSet();
            try
            {
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@MethodID", methodID, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    dSet = dt.ExecuteDataSet(SP_PANEL_METHOD_SEARCH, dbParam);
                    if (dbMessage == string.Empty)
                    {
                        if (dSet != null && dSet.Tables.Count > 0)
                        {
                            if (dSet.Tables[0].Rows.Count > 0)
                            {
                                method = CreateObject(dSet.Tables[0].Rows[0]);
                                if(dSet.Tables[1]!=null)
                                    method.ParameterList = MethodParameter.GetParameterList(dSet.Tables[1]);
                            }
                        }
                    }
                }
                return method;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static MethodMaster CreateObject(DataRow dr)
        {
            try
            {
                MethodMaster method = new MethodMaster();
                method.AssemblyName = Convert.ToString(dr["AssemblyName"]);
                method.ClassName = Convert.ToString(dr["ClassName"]);
                method.MethodID = Convert.ToInt32(dr["MethodID"]);
                method.MethodName = Convert.ToString(dr["MethodName"]);
                return method;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

    public class MethodParameter
    {
        public int ParameterID { get; set; }
        public int MethodID { get; set; }
        public string ParameterName { get; set; }
        public int ParameterType { get; set; }
        
        public object Value { get; set; }
        public PanelCommon.DataType DataType { get; set; }
        public PanelCommon.PropertyType PropertyType { get; set; }
        public int SeqNo { get; set; }
        public string ValueFrom { get; set; }
        public static List<MethodParameter> GetParameterList(DataTable dt)
        {
            List<MethodParameter> Parameters = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                Parameters = new List<MethodParameter>();
                foreach (DataRow dr in dt.Rows)
                {
                    MethodParameter param = new MethodParameter();
                    param.MethodID = Convert.ToInt32(dr["MethodID"]);
                    param.ParameterID = Convert.ToInt32(dr["ParameterID"]);
                    param.ParameterName = Convert.ToString(dr["ParameterName"]);
                    param.ParameterType = Convert.ToInt32(dr["ParameterType"]);
                    param.DataType = (PanelCommon.DataType)dr["ParameterType"];
                    param.Value = Convert.ToString(dr["Value"]);
                    param.ValueFrom = Convert.ToString(dr["ValueFrom"]);
                    param.PropertyType = (PanelCommon.PropertyType)dr["FromType"];
                    param.SeqNo = Convert.ToInt32(dr["SeqNo"]);
                    Parameters.Add(param);
                }
            }
            return Parameters;
        }
     
    }
    
    public class SearchField
    {
        #region SP Declaration
        private const string SP_SEARCH = "usp_PanelFieldSearch";
        #endregion

        public int FieldID { get; set; }
        public int PanelID { get; set; }
        public string SearchColumnName { get; set; }
        public string PropertyName { get; set; }
        // public DataType DataType { get; set; }
        public string DefaultValue { get; set; }    
        //public PropertyMaster Property { get; set; }
        public PanelCommon.ControlType Controltype { get; set; }
        public int ComboID { get; set; }

        #region methods
        private static SearchField CreateSearchField(DataRow dr)
        {
            if (dr != null)
            {
                SearchField _field = new SearchField();
                _field.FieldID = Convert.ToInt32(dr["FieldId"]);
                _field.PanelID = Convert.ToInt32(dr["PanelId"]);
                _field.SearchColumnName = Convert.ToString(dr["SearchColumn"]);
                _field.Controltype = (PanelCommon.ControlType)dr["ControlType"];
                _field.ComboID = Convert.ToInt32(dr["ComboID"]);
                _field.PropertyName=Convert.ToString(dr["PropertyName"]);
                _field.DefaultValue=Convert.ToString(dr["DefaultValue"]);                
                return _field;
            }
            return null;
        }
        public static List<SearchField> CreateSearchFieldList(DataRow[] dr)
        {
            List<SearchField> ListField = null;
            if (dr != null && dr.Length > 0)
            {
                ListField = new List<SearchField>();
                foreach (System.Data.DataRow drow in dr)
                {
                    SearchField _field = CreateSearchField(drow);
                    ListField.Add(_field);
                }
            }
            return ListField;
        }
        public static List<SearchField> Search(int panelID, int fieldID)
        {           
            try
            {
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@PanelID", panelID, DbType.Int32));
                dbParam.Add(new DBParameter("@FieldID", fieldID, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    DataTable dTable=new DataTable();
                    dTable = dt.ExecuteDataTable(SP_SEARCH, dbParam);
                    if (dbMessage == string.Empty)
                    {
                        if (dTable != null && dTable.Rows.Count > 0)
                        {
                            List<SearchField> _fields = new List<SearchField>();
                            foreach (DataRow dr in dTable.Rows)
                            {
                                SearchField field = CreateSearchField(dr);
                                _fields.Add(field);
                            }
                            return _fields;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static SearchField GetFieldDetail(int fieldID)
        {
             List<SearchField> list=Search( -1,fieldID);
             if (list != null && list.Count > 0)
                 return list[0];
             else
                 return null;
        }
        #endregion
    }

    public class ColumnMaster
    {
        private const string SP_GET_COLUMN = "usp_GetToAddColumn";
        private const string SP_TOREMOVE_COLUMN = "usp_GetToRemoveColumn";
        private const string SP_SEARCH_COLUMN = "usp_PanelColumnSearch";
        public int ColumnID { get; set; }
        public int PanelID { get; set; }
        public string ColumnName { get; set; }
        public string DataPropertyName { get; set; }
        public string HeaderText { get; set; }
        public bool IsVisible { get; set; }
        public int Width { get; set; }
        public List<ColumnMaster> GetToAddColumn(int userPanelID, ref string errorMessage)
        {
            List<ColumnMaster> ColumnList;
            try
            {
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@PanelID", this.PanelID, DbType.Int32));
                dbParam.Add(new DBParameter("@UserPanelID", userPanelID, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    ColumnList = new List<ColumnMaster>();
                    System.Data.DataTable dTable = new DataTable();
                    dTable = dt.ExecuteDataTable(SP_GET_COLUMN, dbParam);
                    if (dbMessage == string.Empty)
                    {
                        if (dTable != null && dTable.Rows.Count > 0)
                        {
                            foreach (System.Data.DataRow drow in dTable.Rows)
                            {
                                ColumnMaster Column = new ColumnMaster();
                                ColumnList = CreateColumnList(dTable);
                            }
                        }
                    }
                    else
                    {
                        errorMessage = dbMessage.ToString();
                    }
                }
                return ColumnList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ColumnMaster> GetToRemoveColumn(int userPanelID, ref string errorMessage)
        {
            List<ColumnMaster> ColumnList;
            try
            {
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@PanelID", this.PanelID, DbType.Int32));
                dbParam.Add(new DBParameter("@UserPanelID", userPanelID, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    ColumnList = new List<ColumnMaster>();
                    System.Data.DataTable dTable = new DataTable();
                    dTable = dt.ExecuteDataTable(SP_TOREMOVE_COLUMN, dbParam);
                    if (dbMessage == string.Empty)
                    {
                        if (dTable != null && dTable.Rows.Count > 0)
                        {
                            foreach (System.Data.DataRow drow in dTable.Rows)
                            {
                                ColumnMaster Column = new ColumnMaster();
                                ColumnList = CreateColumnList(dTable);
                            }
                        }
                    }
                    else
                    {
                        errorMessage = dbMessage.ToString();
                    }
                }
                return ColumnList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<ColumnMaster> CreateColumnList(DataTable dt)
        {
            List<ColumnMaster> ListColumn = new List<ColumnMaster>();
            foreach (System.Data.DataRow drow in dt.Rows)
            {
                ColumnMaster _Column = CreateColumObject(drow);
                ListColumn.Add(_Column);
            }
            return ListColumn;
        }
        public static List<ColumnMaster> CreateColumnList(DataRow[] dr)
        {
            List<ColumnMaster> ListColumn = null;
            if (dr != null && dr.Length > 0)
            {
                ListColumn = new List<ColumnMaster>();
                foreach (System.Data.DataRow drow in dr)
                {
                    ColumnMaster _Column = CreateColumObject(drow);
                    ListColumn.Add(_Column);
                }
            }
            return ListColumn;
        }
        public static ColumnMaster GetColumDetail(int columnID)
        {            
            try
            {
                List<ColumnMaster> list = Search(-1, columnID);
                if (list != null && list.Count > 0)
                    return list[0];
                else
                    return null; 
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private static ColumnMaster CreateColumObject(DataRow dr)
        {
            if (dr != null)
            {
                ColumnMaster _Column = new ColumnMaster();
                _Column.ColumnID = Convert.ToInt32(dr["ColumnId"]);
                _Column.ColumnName = Convert.ToString(dr["ColumnName"]);
                _Column.DataPropertyName = Convert.ToString(dr["DataPropertyName"]);
                _Column.HeaderText = Convert.ToString(dr["HeaderText"]);
                _Column.IsVisible = Convert.ToBoolean(dr["IsVisible"]);
                _Column.PanelID = Convert.ToInt32(dr["PanelId"]);
                _Column.Width = Convert.ToInt32(dr["Width"]);
                return _Column;
            }
            return null;
        }
        public static List<ColumnMaster> Search(int PanelID,int columnID)
        {
            try
            {
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@PanelID", PanelID, DbType.Int32));
                dbParam.Add(new DBParameter("@ColumnID", columnID, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    DataTable dTable = new DataTable();
                    dTable = dt.ExecuteDataTable(SP_SEARCH_COLUMN, dbParam);
                    if (dbMessage == string.Empty)
                    {
                        if (dTable != null && dTable.Rows.Count > 0)
                        {
                            List<ColumnMaster> Columns = new List<ColumnMaster>();
                            foreach (DataRow dr in dTable.Rows)
                            {
                                ColumnMaster Column = CreateColumObject(dr);
                                Columns.Add(Column);
                            }
                            return Columns;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
  
    public class PanelMaster
    {
        #region SP Declaration
        // Change S.p add ControlType in search fields table
       // private const string SP_PANEL_SEARCH = "usp_PanelSearch";
        private const string SP_PANEL_SEARCH = "usp_GetPanel";
        
        private const string SP_PANEL_SAVE = "usp_PanelSave";        
        #endregion

        public int PanelID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SearchMethodID { get; set; }
        public int ShowMethodID { get; set; }
        public MethodMaster SearchMethod { get; set; }
        public MethodMaster ShowMethod { get; set; }
        public MethodMaster ConstructorMethod { get; set; }
        public int ConstructorID { get; set; }
        public string FormName { get; set; }
        public string Tag { get; set; }
        public List<SearchField> ListSearchField { get; set; }
        public List<ColumnMaster> ListColumn { get; set; }

        //Search Method Return List of Panels
        public List<PanelMaster> Search()
        {
            List<PanelMaster> panelList = new List<PanelMaster>();
            
            try
            {
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter ("@PanelID",this.PanelID, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT,dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    DataTable dTable = new DataTable();
                    dTable = dt.ExecuteDataTable(SP_PANEL_SEARCH, dbParam);
                    if (dbMessage == string.Empty)
                    {
                        if (dTable != null && dTable.Rows.Count > 0)
                        {
                            panelList = new List<PanelMaster>();
                            foreach (System.Data.DataRow drow in dTable.Rows)
                            {
                                PanelMaster _panel = CreatePanelObject(drow);
                                panelList.Add(_panel);
                            }                           
                        }
                    }
                }
                return panelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        //Return Single Panel
        public static PanelMaster Search(int panelID)
        {
            PanelMaster panel = new PanelMaster();
            panel.PanelID = panelID;
            List<PanelMaster> Panels= panel.Search();
            if (Panels != null && Panels.Count > 0)
            {
                Panels[0].ListColumn = ColumnMaster.Search(Panels[0].PanelID, -1);
                Panels[0].ListSearchField = SearchField.Search(Panels[0].PanelID, -1);
                return Panels[0];
            }
            return null;
        }
        // Create  Panel Object        
        private PanelMaster CreatePanelObject(System.Data.DataRow drow)
        {
            PanelMaster panel = new PanelMaster();
            panel.Description = Convert.ToString(drow["Description"]);
//            panel.FormName = Convert.ToString(drow["FormName"]);
            panel.Name = Convert.ToString(drow["Name"]);
            panel.PanelID = Convert.ToInt32(drow["PanelId"]);
            panel.SearchMethodID = Convert.ToInt32(drow["SearchMethodId"]);
            panel.SearchMethod = MethodMaster.GetMethodDetail(panel.SearchMethodID);
            panel.ShowMethodID = Convert.ToInt32(drow["ShowMethodID"]);
            panel.ShowMethod = MethodMaster.GetMethodDetail(panel.ShowMethodID);
            panel.ConstructorID = Convert.ToInt32(drow["ConstructorID"]);
            panel.ConstructorMethod = MethodMaster.GetMethodDetail(panel.ConstructorID);
            panel.Tag = Convert.ToString(drow["Tag"]);
            return panel;
        }

        public bool Save(ref string errorMessage)
        {
            try
            {
                DBParameterList dbParam;
                bool isSuccess = false;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    dtManager.BeginTransaction();
                    {
                        string xmlDoc = Common.ToXml(this);

                        dbParam = new DBParameterList();
                        dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));

                        dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                        DataTable dt = dtManager.ExecuteDataTable(SP_PANEL_SAVE, dbParam);

                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                        {
                            if (errorMessage.Length > 0)
                            {
                                isSuccess = false;
                                dtManager.RollbackTransaction();
                            }
                            else
                            {
                                isSuccess = true;
                                dtManager.CommitTransaction();
                            }
                        }
                    }
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }

   
    /// <summary>
    /// User Classes
    /// </summary>
    /// 

    public class UserPanel
    {
        #region SP Declaration
        private const string SP_USERPANEL_SEARCH = "usp_UserPanelSearch";
        private const string SP_USERPANEL_SAVE = "usp_AddUserPanel";
        private const string SP_USERPANEL_REMOVE = "usp_RemoveUserPanel";
        #endregion

        public int UserPanelID { get; set; }
        public int PanelID { get; set; }
        public int UserID { get; set; }
        public int LocationID { get; set; }   
        public PanelMaster Panel { get; set; }   

        public List<UserSearchField> ListSearchField { get; set; }
        public List<UserColumn> ListColumn { get; set; }

        #region Methods
        /// <summary>
        /// Serach 
        /// </summary>
        /// <returns></returns>
        public List<UserPanel> Search()
        {
            List<UserPanel> panelList = new List<UserPanel>();           
            System.Data.DataSet dSet = new DataSet();

            try
            {
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@UserPanelID", this.UserPanelID, DbType.Int32));
                dbParam.Add(new DBParameter("@PanelID", this.PanelID, DbType.Int32));
                dbParam.Add(new DBParameter("@UserID", this.UserID, DbType.Int32));
                dbParam.Add(new DBParameter("@LocationID", this.LocationID, DbType.Int32));                
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    dSet = dt.ExecuteDataSet(SP_USERPANEL_SEARCH, dbParam);
                    if (dbMessage == string.Empty)
                    {
                        if (dSet != null && dSet.Tables.Count > 0)
                        {
                            foreach (System.Data.DataRow drow in dSet.Tables[0].Rows)
                            {
                                UserPanel _panel = CreateUserPanelObject(drow);
                                if(dSet.Tables[1]!=null)
                                {
                                    DataRow[] drColumns = dSet.Tables[1].Select("UserPanelId=" + _panel.UserPanelID );
                                    _panel.ListColumn = UserColumn.CreateColumnList(drColumns);
                                }
                                if(dSet.Tables[2]!=null)
                                {
                                    DataRow[] drSearchField = dSet.Tables[2].Select("UserPanelId=" + _panel.UserPanelID);
                                    _panel.ListSearchField =UserSearchField.CreateSearchList(drSearchField);
                                }                                
                                panelList.Add(_panel);
                            }                            
                        }
                    }
                }
                return panelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Create user Panel Object 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private UserPanel CreateUserPanelObject(DataRow dr)
        {
            UserPanel panel = new UserPanel();
            panel.LocationID = Convert.ToInt32(dr["locationId"]);
            panel.PanelID = Convert.ToInt32(dr["PanelId"]);
            panel.UserID = Convert.ToInt32(dr["UserId"]);
            panel.UserPanelID = Convert.ToInt32(dr["UserPanelId"]);
            panel.Panel = PanelMaster.Search(panel.PanelID);
            return panel;            
        }   
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Save(ref string errorMessage)
        {
            string dbMessage = string.Empty;
            bool isSuccess = false;
            DBParameterList dbParam = new DBParameterList();            
            dbParam.Add(new DBParameter("@PanelID", this.PanelID, DbType.Int32));
            dbParam.Add(new DBParameter("@UserID", this.UserID, DbType.Int32));
            dbParam.Add(new DBParameter("@LocationID", this.LocationID, DbType.Int32));
            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
            using (DataTaskManager dt = new DataTaskManager())
            {
                dt.BeginTransaction();
                DataTable dttable = dt.ExecuteDataTable(SP_USERPANEL_SAVE, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                if (errorMessage.Length > 0)
                {
                    isSuccess = false;
                    dt.RollbackTransaction();
                }
                else
                {
                    isSuccess = true;
                    dt.CommitTransaction();
                    this.UserPanelID = Convert.ToInt32(dttable.Rows[0]["UserPanelID"]);
                }
                return isSuccess;
            }
        }
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Remove(ref string errorMessage)
        {
            string dbMessage = string.Empty;
            bool isSuccess = false;
            DBParameterList dbParam = new DBParameterList();
            dbParam.Add(new DBParameter("@UserPanelID", this.UserPanelID, DbType.Int32));
            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
            using (DataTaskManager dt = new DataTaskManager())
            {
                dt.BeginTransaction();
                dt.ExecuteNonQuery(SP_USERPANEL_REMOVE, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                if (errorMessage.Length > 0)
                {
                    isSuccess = false;
                    dt.RollbackTransaction();
                }
                else
                {
                    isSuccess = true;
                    dt.CommitTransaction();                    
                }
                return isSuccess;
            }
        }
        #endregion
    }
    
    public class UserColumn
    {
        #region SP Declaration
        private const string SP_USERCOLUMN_SAVE = "usp_AddUserColumn";
        private const string SP_USERCOLUMN_REMOVE = "usp_RemoveUserColumn";
        #endregion

        public int UserPanelID { get; set; }
        public ColumnMaster Column { get; set; }

        #region Method
        public bool Save(ref string errorMessage)
        {
            string dbMessage = string.Empty;
            bool isSuccess = false;
            DBParameterList dbParam = new DBParameterList();
            dbParam.Add(new DBParameter("@UserPanelID", this.UserPanelID, DbType.Int32));
            dbParam.Add(new DBParameter("@ColumnID", this.Column.ColumnID, DbType.Int32));
            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
            using (DataTaskManager dt = new DataTaskManager())
            {
                dt.BeginTransaction();
                DataTable dttable = dt.ExecuteDataTable(SP_USERCOLUMN_SAVE, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                if (errorMessage.Length > 0)
                {
                    isSuccess = false;
                    dt.RollbackTransaction();
                }
                else
                {
                    isSuccess = true;
                    dt.CommitTransaction();
                    //this.UserPanelID = Convert.ToInt32(dttable.Rows[0]["UserColumnID"]);
                }
                return isSuccess;
            }
        }
        public bool Remove(ref string errorMessage)
        {
            string dbMessage = string.Empty;
            bool isSuccess = false;
            DBParameterList dbParam = new DBParameterList();
            dbParam.Add(new DBParameter("@ColumnID", this.Column.ColumnID, DbType.Int32));
            dbParam.Add(new DBParameter("@UserPanelID", this.UserPanelID, DbType.Int32));
            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
            using (DataTaskManager dt = new DataTaskManager())
            {
                dt.BeginTransaction();
                dt.ExecuteNonQuery(SP_USERCOLUMN_REMOVE, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                if (errorMessage.Length > 0)
                {
                    isSuccess = false;
                    dt.RollbackTransaction();
                }
                else
                {
                    isSuccess = true;
                    dt.CommitTransaction();
                }
                return isSuccess;
            }
        }
        public static List<UserColumn> CreateColumnList(DataRow[] dr)
        {
            List<UserColumn> ListColumn = new List<UserColumn>();            
            if (dr != null && dr.Length > 0)
            {
                foreach (System.Data.DataRow drow in dr)
                {
                    UserColumn _Column = new UserColumn();
                    _Column.UserPanelID = Convert.ToInt32(drow["UserPanelId"]);
                    _Column.Column = new ColumnMaster();
                    _Column.Column.ColumnID = Convert.ToInt32(drow["ColumnId"]);
                    _Column.Column.ColumnName = Convert.ToString(drow["ColumnName"]);
                    _Column.Column.DataPropertyName = Convert.ToString(drow["DataPropertyName"]);
                    _Column.Column.HeaderText = Convert.ToString(drow["HeaderText"]);
                    _Column.Column.IsVisible = Convert.ToBoolean(drow["IsVisible"]);
                    _Column.Column.PanelID = Convert.ToInt32(drow["PanelId"]);
                    _Column.Column.Width = Convert.ToInt32(drow["Width"]);              
                    ListColumn.Add(_Column);
                }
            }
            return ListColumn;
        }

        #endregion
    }

    public class UserSearchField
    {
        #region SP Declaration
        //Change procedure
        private const string SP_USERFIELD_SEARCH = "usp_GetUserSearchField";
        private const string SP_USERFIELD_SAVE = "usp_SaveUserSearchField";
        #endregion

        public int UserPanelID { get; set; }
        public int FieldID { get; set; }
        public SearchField Field { get; set; }
        public string Value { get; set; }

        #region Method
        public List<UserSearchField> Search()
        {
            List<UserSearchField> searchList;
            try
            {
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@PanelID", this.Field.PanelID, DbType.Int32));
                dbParam.Add(new DBParameter("@UserPanelID", this.UserPanelID, DbType.Int32));

                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    searchList = new List<UserSearchField>();
                    System.Data.DataTable dTable = new DataTable();
                    dTable = dt.ExecuteDataTable(SP_USERFIELD_SEARCH, dbParam);
                    if (dbMessage == string.Empty)
                    {
                        if (dTable != null && dTable.Rows.Count > 0)
                        {
                            searchList = CreateSearchList(dTable);
                        }
                    }
                }
                return searchList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<UserSearchField> CreateSearchList(DataTable dt)
        {
            List<UserSearchField> ListSearch = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                ListSearch = new List<UserSearchField>();
                foreach (System.Data.DataRow drow in dt.Rows)
                {
                    UserSearchField _field =  CreateSearchObject(drow);
                    ListSearch.Add(_field);
                }
            }
            return ListSearch;
        }

        public static List<UserSearchField> CreateSearchList(DataRow[] dr)
        {
            List<UserSearchField> ListSearch = null;
            if (dr != null && dr.Length > 0)
            {
                ListSearch = new List<UserSearchField>();
                foreach (System.Data.DataRow drow in dr)
                {
                    UserSearchField _field = CreateSearchObject(drow);
                    ListSearch.Add(_field);
                }
            }
            return ListSearch;
        }

        private static UserSearchField  CreateSearchObject(DataRow dr)
        {
            try
            {
                UserSearchField _field = new UserSearchField();
                _field.UserPanelID = Convert.ToInt32(dr["UserPanelId"]);
                _field.FieldID = Convert.ToInt32(dr["FieldId"]);
                _field.Field = SearchField.GetFieldDetail(Convert.ToInt32(dr["FieldId"]));
                if(_field.Field!=null)
                    _field.Value = Convert.ToString(dr["Value"]);
                return _field;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public bool save(ref string errormessage)
        //{
        //    string dbmessage = string.empty;
        //    bool issuccess = false;
        //    dbparameterlist dbparam = new dbparameterlist();
        //    dbparam.add(new dbparameter("@userpanelid", this.userpanelid, dbtype.int32));
        //    dbparam.add(new dbparameter("@fieldid", this.field.fieldid, dbtype.int32));
        //    dbparam.add(new dbparameter("@value", this.value, dbtype.string));
        //    dbparam.add(new dbparameter(common.param_output, dbmessage, dbtype.string, parameterdirection.output, common.param_output_length));
        //    using (datataskmanager dt = new datataskmanager())
        //    {
        //        dt.begintransaction();
        //        datatable dttable = dt.executedatatable(sp_userfield_save, dbparam);
        //        errormessage = dbparam[common.param_output].value.tostring();
        //        if (errormessage.length > 0)
        //        {
        //            issuccess = false;
        //            dt.rollbacktransaction();
        //        }
        //        else
        //        {
        //            issuccess = true;
        //            dt.committransaction();
        //        }
        //        return issuccess;
        //    }
        //}

        public static bool SaveList(List<UserSearchField> SearchList, ref string errorMessage)
        {
            try
            {
                DBParameterList dbParam;
                bool isSuccess = false;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    dtManager.BeginTransaction();
                    {
                        string xmlDoc = Common.ToXml(SearchList);

                        dbParam = new DBParameterList();
                        dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));

                        dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                        DataTable dt = dtManager.ExecuteDataTable(SP_USERFIELD_SAVE, dbParam);

                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                        {
                            if (errorMessage.Length > 0)
                            {
                                isSuccess = false;
                                dtManager.RollbackTransaction();
                            }
                            else
                            {
                                isSuccess = true;
                                dtManager.CommitTransaction();
                            }
                        }
                    }
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

    }

    public class ComboMaster
    {
        #region SP Declaration
        private const string SP_COMBO_SEARCH = "usp_GetComboDetails";
        
        #endregion

        public int ComboID { get; set; }
        public string ComboName { get; set; }
        public MethodMaster Method { get; set; }
        //public string AssemblyName { get; set; }
        //public string ClassName { get; set; }
        //public string MethodName { get; set; }
        public string DisplayMember{ get; set; }
        public string ValueMember { get; set; }
       // public List<ComboParameter> ListParameter { get; set; }
        public List<ComboMaster> Search()
        {
            List<ComboMaster> searchList;
            try
            {
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@ComboID", this.ComboID, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    searchList = new List<ComboMaster>();
                    System.Data.DataSet dSet = new DataSet();
                    dSet = dt.ExecuteDataSet(SP_COMBO_SEARCH, dbParam);
                    if (dbMessage == string.Empty)
                    {
                        if (dSet != null && dSet.Tables.Count > 0)
                        {
                            searchList = CreateComboList(dSet);

                        }
                    }
                }
                return searchList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<ComboMaster> CreateComboList(DataSet ds)
        {
            List<ComboMaster> ListSearch = null;
            DataTable dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                ListSearch = new List<ComboMaster>();
                foreach (System.Data.DataRow drow in dt.Rows)
                {
                    ComboMaster _combo = new ComboMaster();
                    //_combo.AssemblyName = Convert.ToString(drow["AssemblyName"]);
                    //_combo.ClassName = Convert.ToString(drow["ClassName"]);
                    _combo.ComboID = Convert.ToInt32(drow["ComboId"]);
                    _combo.ComboName = Convert.ToString(drow["ComboName"]);
                    _combo.Method = MethodMaster.GetMethodDetail(Convert.ToInt32(drow["MethodId"]));                   
                    _combo.DisplayMember = Convert.ToString(drow["DisplayMember"]);
                    _combo.ValueMember = Convert.ToString(drow["ValueMember"]);
                    //_combo.ListParameter =ComboParameter.GetParameterList(_combo.ComboID,ds.Tables[1]);
                    ListSearch.Add(_combo);
                }
            }
            return ListSearch;
        }
    }
    
    

}
