using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.BusinessObjects
{
    [Serializable]
    public class AppLocModFunctions
    {
        #region Variable and Property Declaration

        private int m_apptypeid;

        public int Apptypeid
        {
            get { return m_apptypeid; }
            set { m_apptypeid = value; }
        }
        private string m_apptypename;

        public string Apptypename
        {
            get { return m_apptypename; }
            set { m_apptypename = value; }
        }
        private int m_locid;

        public int Locid
        {
            get { return m_locid; }
            set { m_locid = value; }
        }
        private string m_locname;

        public string Locname
        {
            get { return m_locname; }
            set { m_locname = value; }
        }
        private string m_menuname;

        public string Menuname
        {
            get { return m_menuname; }
            set { m_menuname = value; }
        }
        private int m_parentid;

        public int Parentid
        {
            get { return m_parentid; }
            set { m_parentid = value; }
        }
        private string m_parentname;

        public string Parentname
        {
            get { return m_parentname; }
            set { m_parentname = value; }
        }
        private int m_seqno;

        public int Seqno
        {
            get { return m_seqno; }
            set { m_seqno = value; }
        }
        private int m_moduleid;

        public int Moduleid
        {
            get { return m_moduleid; }
            set { m_moduleid = value; }
        }
        private string m_modulename;

        public string Modulename
        {
            get { return m_modulename; }
            set { m_modulename = value; }
        }
        private int m_functionid;

        public int Functionid
        {
            get { return m_functionid; }
            set { m_functionid = value; }
        }
        string m_functionname;

        public string Functionname
        {
            get { return m_functionname; }
            set { m_functionname = value; }
        }
        private int m_status;

        public int Status
        {
            get { return m_status; }
            set { m_status = value; }
        }
        private int m_recordid;

        public int Recordid
        {
            get { return m_recordid; }
            set { m_recordid = value; }
        }
        private List<Functions> m_funclist = null;

        public List<Functions> Funclist
        {
            get { return m_funclist; }
            set { m_funclist = value; }
        }
        private string m_statustext;

        public string Statustext
        {
            get { return m_statustext; }
            set { m_statustext = value; }
        }
        #endregion


        #region SP Declaration
        private const string SP_APPLOCMODLINK_SAVE = "usp_AppLocModLinkSave";
        private const string SP_APPLOC_SEARCH = "usp_AppLocModLinkSearch";
        private const string SP_FUNC_SEARCH = "usp_AppLocModFuncLinkSearch";
        #endregion

        

        #region save method
      
        public Boolean Save(ref String errorMessage)
        {
            DBParameterList dbParam;
            bool isSuccess = false;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dtManager.BeginTransaction();
                {
                    try
                    {
                        string xmlDoc = Common.ToXml(this);
                        dbParam = new DBParameterList();
                        dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                        dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                        DataTable dt = dtManager.ExecuteDataTable(SP_APPLOCMODLINK_SAVE, dbParam);

                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                        {
                            if (errorMessage.Length > 0)
                            {
                                isSuccess = false;
                                dtManager.RollbackTransaction();
                            }
                            else
                            {
                                dtManager.CommitTransaction();
                                isSuccess = true;
                            }
                        }
                        return isSuccess;
                    }
                    catch (Exception ex)
                    {
                        dtManager.RollbackTransaction();
                        throw ex;
                    }
                }
            }
        }


        #endregion



        #region Search method
        public List<AppLocModFunctions> Search(int appid,int locid,int moduleid, ref string errorMessage)
        {
            List<AppLocModFunctions> searchlist = new List<AppLocModFunctions>();
            AppLocModFunctions applocobj = null;
            DBParameterList dbParam;
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("AppidParam", appid, DbType.Int32));
                    dbParam.Add(new DBParameter("LocidParam", locid, DbType.Int32));
                    dbParam.Add(new DBParameter("ModuleidParam", moduleid, DbType.Int32));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    DataTable dt = dtManager.ExecuteDataTable(SP_APPLOC_SEARCH, dbParam);
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            applocobj = new AppLocModFunctions();
                            applocobj.Recordid = Convert.ToInt32(dt.Rows[i]["RecordIdVal"]);
                            applocobj.Apptypeid = Convert.ToInt32(dt.Rows[i]["AppTypeIdVal"]);
                            applocobj.Apptypename = Convert.ToString(dt.Rows[i]["ApptypeVal"]);
                            applocobj.Locid = Convert.ToInt32(dt.Rows[i]["LocIdVal"]);
                            applocobj.Locname = Convert.ToString(dt.Rows[i]["LocNameVal"]);
                            applocobj.Moduleid = Convert.ToInt32(dt.Rows[i]["ModuleIdVal"]);
                            applocobj.Modulename = Convert.ToString(dt.Rows[i]["ModuleNameVal"]);
                            applocobj.Menuname = Convert.ToString(dt.Rows[i]["MenuIdVal"]);
                            applocobj.Parentid = Validators.CheckForDBNull((dt.Rows[i]["ParentIdVal"]), -1);
                            applocobj.Parentname = Convert.ToString(dt.Rows[i]["ParentNameVal"]);
                            applocobj.Seqno = Convert.ToInt32(dt.Rows[i]["SeqNoVal"]);
                            applocobj.Status = Convert.ToInt32(dt.Rows[i]["StatusVal"]);
                            applocobj.Statustext = Convert.ToString(dt.Rows[i]["StatusTextVal"]);
                            searchlist.Add(applocobj);

                        }

                    }

                }
                return searchlist;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;

            }
        }

        #endregion


        #region Function Search

        public List<Functions> funcSearch(int recid, ref string errorMessage)
        {
            List<Functions> searchfunclist = new List<Functions>();
            Functions funcobj = null;
            DBParameterList dbParam;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("recidParam", recid, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataTable dt = dtManager.ExecuteDataTable(SP_FUNC_SEARCH, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        funcobj = new Functions();
                        funcobj.Recordid = Convert.ToInt32(dt.Rows[i]["RecIdVal"]);
                        funcobj.Functionid = Convert.ToInt32(dt.Rows[i]["FunctionIdVal"]);
                        funcobj.Functioncode = Convert.ToString(dt.Rows[i]["FunctionNameVal"]);
                        searchfunclist.Add(funcobj);

                    }

                }

            }
            return searchfunclist;
        }


        #endregion

    }
}
