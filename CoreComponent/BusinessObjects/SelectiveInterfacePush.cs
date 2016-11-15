using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//vinculum framework/namespace(s)
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;


namespace CoreComponent.BusinessObjects
{
    public class SelectiveInterfacePush
    {

        #region Variables

        private const string m_uspDataSearch = "usp_getSelectiveInterfacePushData";
        private const string m_uspDataSave = "usp_Interface_Audit";

        #endregion


        #region Constructor

        public SelectiveInterfacePush()
        {
        }

        #endregion


        #region Properties

        public string InterfaceProcessId
        {
            get;
            set;
        }

        public string InterfaceProcessCode
        {
            get;
            set;
        }

        public string InterfaceProcessName
        {
            get;
            set;
        }

        public int LocationTypeId
        {
            get;
            set;
        }

        public int LocationCodeId
        {
            get;
            set;
        }

        public int LeafLocationId
        {
            get;
            set;
        }

        public string LeafLocationCode
        {
            get;
            set;
        }

        public string Key1
        {
            get;
            set;
        }

        public string Key2
        {
            get;
            set;
        }

        public string Key3
        {
            get;
            set;
        }

        public string Key4
        {
            get;
            set;
        }

        public string Key5
        {
            get;
            set;
        }

        public string ActionId
        {
            get;
            set;
        }

        public string ActionCode
        {
            get;
            set;
        }

        public int InsertedById
        {
            get;
            set;
        }

        public String InsertedBy
        {
            get;
            set;
        }

        public DateTime InsertedDate
        {
            get;
            set;
        }

        public String DisplayInsertedDate
        {
            get
            {
                if ((InsertedDate != null) && (InsertedDate.ToString().Length > 0))
                {
                    return InsertedDate.ToString(Common.DTP_DATE_FORMAT);
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        #endregion


        #region Methods

        public DataTable SearchUnprocessedRecords(ref string errorMessage)
        {
            DBParameterList dbParam;
            DataTable dtUnprocessedRecords = null;
            using (Vinculum.Framework.Data.DataTaskManager dtManager = new Vinculum.Framework.Data.DataTaskManager())
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataTable dt = dtManager.ExecuteDataTable(m_uspDataSearch, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (dt != null)
                {
                    dtUnprocessedRecords = dt.Copy();
                }
            }

            return dtUnprocessedRecords;
        }

        public void SaveUnprocessedRecord(string interfaceId, string locCode, string key1, string key2, string key3, string key4, string key5, string actionType, int userId, ref string output)
        {
            DBParameterList dbParam;
            using (Vinculum.Framework.Data.DataTaskManager dtManager = new Vinculum.Framework.Data.DataTaskManager())
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.INTERFACE_ID, interfaceId, DbType.String));
                dbParam.Add(new DBParameter(Common.INTERFACE_LOCCODE, locCode, DbType.String));
                dbParam.Add(new DBParameter(Common.INTERFACE_TABLENAME, Common.DBNULL_VAL, DbType.String));
                dbParam.Add(new DBParameter(Common.INTERFACE_KEY1, key1, DbType.String));
                dbParam.Add(new DBParameter(Common.INTERFACE_KEY2, key2, DbType.String));
                dbParam.Add(new DBParameter(Common.INTERFACE_KEY3, key3, DbType.String));
                dbParam.Add(new DBParameter(Common.INTERFACE_KEY4, key4, DbType.String));
                dbParam.Add(new DBParameter(Common.INTERFACE_KEY5, key5, DbType.String));
                dbParam.Add(new DBParameter(Common.INTERFACE_ACTION, actionType, DbType.String));
                dbParam.Add(new DBParameter(Common.INTERFACE_USERID, userId, DbType.Int32));
                dbParam.Add(new DBParameter(Common.INTERFACE_OUTPARAM, output, DbType.String, ParameterDirection.Output, Common.INTERFACE_OUTPARAM_VAL));

                dtManager.ExecuteNonQuery(m_uspDataSave, dbParam);
                output = dbParam[Common.INTERFACE_OUTPARAM].Value.ToString();
            }
        }

        #endregion

    }
}
