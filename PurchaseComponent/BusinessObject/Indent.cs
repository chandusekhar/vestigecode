using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace PurchaseComponent.BusinessObjects
{
    [Serializable]
    public class Indent: IIndent
    {
        #region SP Declaration 
        private const string SP_INDENT_SAVE = "usp_IndentSave";
        private const string SP_INDENT_SEARCH = "usp_IndentSearch";
        private const string SP_GET_INDENT_DETAIL = "usp_IndentDetailSearch";
        #endregion

        #region variables 
        private int m_createdBy=0;
        private DateTime m_createdDate=Convert.ToDateTime(DateTime.Today.ToShortTimeString());
        private int m_modifiedBy=0;        
        private DateTime m_modifiedDate=Common.DATETIME_NULL;
        private string m_indentNo=string.Empty;
        private DateTime m_indentDate = Common.DATETIME_NULL;
        private string m_strIndentDate = string.Empty;
        private DateTime m_fromDate=Common.DATETIME_NULL;
        private DateTime m_toDate=Convert.ToDateTime(DateTime.Today.ToShortTimeString());        
        private int m_indentType=0;
        private string m_indentTypeName;
        private int m_locationID=-1;
        private string m_locationCode=string.Empty;
        private string m_locationName=string.Empty;        
        private int m_status=(Int32)Common.IndentStatus.New;
        private string m_statusDesc=Convert.ToString(Common.IndentStatus.New);        
        private int m_approvedBy=0;
        private DateTime m_approvedDate=Common.DATETIME_NULL;
        private string m_remark=string.Empty;
        private double m_adjustmentFactor=0;
        private List<IndentDetail> m_indentDetail;//=new List<IndentDetail>();
        
        #endregion

        #region IIndent Members

        public int CreatedBy
        {
            get
            {
                return m_createdBy;
            }
            set
            {
                m_createdBy = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return m_createdDate;
            }
            set
            {
                m_createdDate = value;
            }
        }

        public int ModifiedBy
        {
            get
            {
                return m_modifiedBy;
            }
            set
            {
                m_modifiedBy = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return m_modifiedDate;
            }
            set
            {
                m_modifiedDate = value;
            }
        }

        #endregion

        #region Properties
        public string IndentNo
        {
            get
            {
                return m_indentNo;
            }
            set
            {
                m_indentNo = value;
            }
        }

        public DateTime IndentDate
        {
            get
            {
                return m_indentDate;
            }
            set
            {
                m_indentDate = value;
            }
        }

        public string StrIndentDate
        {
            get
            {
                return m_strIndentDate;
            }
            set
            {
                m_strIndentDate = value;
            }
        }
      
        public string DisplayIndentDate
        {
            get {
                    if (IndentDate.ToString(Common.DTP_DATE_FORMAT) == "01-01-1900")
                        return string.Empty;
                    else 
                        return IndentDate.ToString(Common.DTP_DATE_FORMAT); }

            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public DateTime FromIndentDate
        {
            get { return m_fromDate; }
            set { m_fromDate = value; }
        }

        public DateTime ToIndentDate
        {
            get { return m_toDate; }
            set { m_toDate = value; }
        }

        public int IndentType
        {
            get
            {
                return m_indentType;
            }
            set
            {
                m_indentType = value;
            }
        }

        public string IndentTypeName
        {
            get
            {
                return m_indentTypeName;
            }
            set
            {
                m_indentTypeName = value;
            }
        }

        public int LocationID
        {
            get
            {
                return m_locationID;
            }
            set
            {
                m_locationID = value;
            }
        }

        public string LocationCode
        {
            get { return m_locationCode; }
            set { m_locationCode = value; }
        }

        public string LocationName
        {
            get { return m_locationName; }
            set { m_locationName = value; }
        }

        public int Status
        {
            get
            {
                return m_status;
            }
            set
            {
                m_status = value;
            }
        }

        public string StatusName
        {
            get { return m_statusDesc; }
            set { m_statusDesc = value; }
        }

        public int ApprovedBy
        {
            get
            {
                return m_approvedBy;
            }
            set
            {
                m_approvedBy = value;
            }
        }

        public DateTime ApprovedDate
        {
            get
            {
                return m_approvedDate;
            }
            set
            {
                m_approvedDate = value;
            }
        }
        public string DisplayApprovedDate
        {
            get { return ApprovedDate.ToString(Common.DTP_DATE_FORMAT); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public string Remark
        {
            get
            {
                return m_remark;
            }
            set
            {
                m_remark = value;
            }
        }

        public double AdjustmentFactor
        {
            get
            {
                return m_adjustmentFactor;
            }
            set
            {
                m_adjustmentFactor = value;
            }
        }
        public List<IndentDetail> IndentDetail
        {
            get { return m_indentDetail; }
            set { m_indentDetail = value; }
        }
        public string CityName
        {
            get;
            set;
        }
        public string DisplayCreatedDate
        {
            get { return CreatedDate.ToString(Common.DTP_DATE_FORMAT); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

       
        #endregion

        #region Methods


        public bool Save(ref string errorMessage)
        {
            try
            {
                DBParameterList dbParam;
                bool isSuccess = false;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    try
                    {
                        dtManager.BeginTransaction();
                        {
                            string xmlDoc = Common.ToXml(this);

                            dbParam = new DBParameterList();
                            dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                            DataTable dt = dtManager.ExecuteDataTable(SP_INDENT_SAVE, dbParam);

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
                                   this.IndentNo=Convert.ToString(dt.Rows[0]["IndentNo"]);
                                }
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
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public virtual DataTable GetSelectedRecords(string xmlDoc, string spName, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                                
                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(spName, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Indent> Search()
        {
            List<Indent> indentList = new List<Indent>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = GetSelectedRecords(Common.ToXml(this), SP_INDENT_SEARCH, ref errorMessage);

                if (dTable == null)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    Indent indent = new Indent();
                    indent.CreateIndentObject(drow);
                    indentList.Add(indent);
                }
                return indentList;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public List<IndentDetail> GetIndentDetail()
        {
            try
            {
                List<IndentDetail> _indentDetailList = new List<IndentDetail>();
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@IndentNo",this.IndentNo, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                using (DataTaskManager dt = new DataTaskManager())
                {
                    System.Data.DataSet dSet = new DataSet();
                    dSet = dt.ExecuteDataSet(SP_GET_INDENT_DETAIL, dbParam);
                    if (dbMessage.Trim().Equals(string.Empty) && dSet != null && dSet.Tables.Count>0 && dSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dSet.Tables[0].Rows)
                        {
                            IndentDetail detail = new IndentDetail();
                            detail.CreateIndentDetailObject(dr);
                            detail.GetPONO(dSet.Tables[1]);
                            detail.GetTONO(dSet.Tables[2]);   
                            _indentDetailList.Add(detail);
                        }
                    }
                }
                return _indentDetailList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetCompleteIndent()
        {
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = GetSelectedRecords(Common.ToXml(this), SP_INDENT_SEARCH, ref errorMessage);

                if(dTable!=null && dTable.Rows.Count>0)
                {
                    this.CreateIndentObject(dTable.Rows[0]);                    
                    this.IndentDetail=this.GetIndentDetail();
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public void CreateIndentObject(DataRow drow)
        {
            try
            {
                this.AdjustmentFactor = Convert.ToDouble(drow["AdjustmentFactor"]);
                this.IndentDate = Convert.ToDateTime(drow["IndentDate"]);
                this.IndentNo = Convert.ToString(drow["IndentNo"]);
                this.IndentType = Convert.ToInt32(drow["IndentType"]);
                this.IndentTypeName = ((Common.IndentType)(Convert.ToInt32(drow["IndentType"]))).ToString();
                this.LocationCode = Convert.ToString(drow["LocationCode"]);
                this.LocationID = Convert.ToInt32(drow["LocationId"]);
                this.LocationName = Convert.ToString(drow["LocationName"]);
                this.CityName = Convert.ToString(drow["CityName"]);
                this.Remark = Convert.ToString(drow["Remark"]);
                this.Status = Convert.ToInt32(drow["Status"]);
                this.StatusName = Convert.ToString(drow["StatusDescription"]);
                this.CreatedBy = Convert.ToInt32(drow["CreatedBy"]);
                this.CreatedDate = Convert.ToDateTime(drow["CreatedDate"]);
                this.ModifiedBy = Convert.ToInt32(drow["ModifiedBy"]);
                this.ModifiedDate = Convert.ToDateTime(drow["ModifiedDate"]);
                this.ApprovedBy = Convert.ToInt32(drow["ApprovedBy"]);
                this.ApprovedDate = Convert.ToDateTime(drow["ApprovedDate"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable GetItemVendor(List<Item> lstItemWarehouseRequest, ref String errorMessage)
        {
            string SP_NAME = "usp_GetItemVendor";
            DataTable dtTemp = new DataTable();
            //Get Items and Warehouse items detail
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                //Declare and initialize the parameter list object.
                DBParameterList dbParam = new DBParameterList();

                //Add the relevant 2 parameters
                dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(lstItemWarehouseRequest), DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                    ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                dtTemp = dtManager.ExecuteDataTable(SP_NAME, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
            }

            return dtTemp;
        }
        
        #endregion



       
    }
}
