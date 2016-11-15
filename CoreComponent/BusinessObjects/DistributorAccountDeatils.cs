using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace CoreComponent.BusinessObjects
{
    [Serializable]
    class DistributorAccountDeatils
    {
        #region SP DECLARATION

        //constant defined for storing 'search-all-info' sp-name
        private const string DISTRIBUTORACCOUNTHISTORY = "usp_DistributorAccountHistory";

        #endregion

        #region Properties

        public int DistributorId
        {
            get;
            set;
        }


        public string DistributorBankName
        {
            get;
            set;
        }

        public string DistributorBankBranch
        {
            get;
            set;
        }

        public string DistributorBankAccNumber
        {
            get;
            set;
        }

        public int BankID
        {
            get;
            set;
        }

        public String DistributorFirstName
        {
            get;
            set;
        }

        public String DistributorLastName
        {
            get;
            set;
        }

        public string DistributorFullName
        {
            get
            {
                if (string.IsNullOrEmpty(DistributorLastName.Trim()))
                {
                    return DistributorFirstName.Trim();
                }
                else
                {
                    return DistributorFirstName.Trim() + " " + DistributorLastName.Trim();
                }
            }
        }       

        public Int32 CreatedById
        {
            get;
            set;
        }

        public String CreatedBy
        {
            get;
            set;
        }

        public string CreatedDate
        {
            get;
            set;
        }

        public Int32 ModifiedById
        {
            get;
            set;
        }

        public String ModifiedBy
        {
            get;
            set;
        }

        public string ModifiedDate
        {
            get;
            set;
        }

        public string DistributorFName
        {
            get;
            set;
        }
        public string DistributorLName
        {
            get;
            set;
        }

        public string DistributorPANNo
        {
            get;
            set;
        }
        #endregion


        #region GetDeistributorHistory
        /// <summary>
        /// This method is used to get the data of Distributor History.
        /// </summary>
        /// <param name="sDistributorId"></param>
        /// <returns></returns>
        public DataTable GetDistributorAccountHistory(string sDistributorId)
        {
            DataTaskManager objDataManager;
            DBParameterList dbParam;
            string sErrorMessage = "";
            DataTable dtData = null;
            try
            {
                if (Common.AppType == ((int)Common.ApplicationType.BOS).ToString())
                {
                    objDataManager = new DataTaskManager(Common.HODB);
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("distributorid", sDistributorId, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, sErrorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dtData = objDataManager.ExecuteDataTable(DISTRIBUTORACCOUNTHISTORY, dbParam);
                }
                else
                {
                    objDataManager = new DataTaskManager();
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("distributorid", sDistributorId, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, sErrorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dtData = objDataManager.ExecuteDataTable(DISTRIBUTORACCOUNTHISTORY, dbParam);
                }
                return dtData;
            }
            finally
            {
                objDataManager = null;
                dbParam = null;
                dtData = null;
            }

        }
        #endregion     
    }
}
