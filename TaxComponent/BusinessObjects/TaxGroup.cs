using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;

namespace TaxComponent.BusinessObjects
{
    public class TaxGroup : Tax
    {
        #region Variable & Property Declaration

        private System.Int32 m_taxGroupId;

        public System.Int32 TaxGroupId
        {
            get { return m_taxGroupId; }
            set { m_taxGroupId = value; }
        }

        private System.String m_taxGroupCode;

        public System.String TaxGroupCode
        {
            get { return m_taxGroupCode; }
            set { m_taxGroupCode = value; }
        }
        private System.Int32 m_taxCodeId;

        public System.Int32 TaxCodeId
        {
            get { return m_taxCodeId; }
            set { m_taxCodeId = value; }
        }

        private System.String m_taxCode;

        public System.String TaxCode
        {
            get { return m_taxCode; }
            set { m_taxCode = value; }
        }

        private System.DateTime m_startDate;

        public System.DateTime StartDate
        {
            get { return m_startDate; }
            set { m_startDate = value; }
        }

        private String m_startDateVal;

        public String StartDateVal
        {
            get { return m_startDateVal; }
            set { m_startDateVal = value; }
        }

        private System.Int32 m_groupOrder;

        public System.Int32 GroupOrder
        {
            get { return m_groupOrder; }
            set { m_groupOrder = value; }
        }

        private System.String m_appliedOn;

        public System.String AppliedOn
        {
            get { return m_appliedOn; }
            set { m_appliedOn = value; }
        }

        private String m_appliedOnText;

        public String AppliedOnText
        {
            get { return m_appliedOnText; }
            set { m_appliedOnText = value; }
        }

        public int Status
        {
            get;
            set;
        }

        public string StatusName
        {
            get;
            set;
        }       

        public List<TaxDetail> TaxCodeList
        {
            get;
            set;
        }
        public List<TaxGroup> TaxGroupList
        {
            get;
            set;
        }

        public bool IsInclusive
        {
            get;
            set;
        }
        #endregion 

        #region  SP DECLARATION

        private const string SP_TAXGROUP_SAVE = "usp_TaxGroupSave";
        private const string SP_TAXGROUP_SEARCH = "usp_TaxGroupSearch";

        #endregion

        #region  Methods
               
        /// <summary>
        /// Save TaxGroup
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns>Boolean</returns>
        public Boolean SaveTaxGroup(ref string errorMsg)
        {
            Boolean isSuccess = false;
                 try
                    {
                        string xmlDoc = Common.ToXml(this);
                        isSuccess =  base.Save(xmlDoc,SP_TAXGROUP_SAVE,ref errorMsg);
                                 
                        if (errorMsg.Length > 0)
                            {
                                isSuccess = false;                                
                            }
                        else
                            {                                
                                isSuccess = true;
                            }
                        return isSuccess; 
                    }
                  catch (Exception ex)
                    {                        
                        throw ex;
                    }
         }
               
        /// <summary>
        /// Search Tax Group
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns>List</returns>
        public List<TaxGroup> SearchTaxGroup(ref string errorMsg)
        {
            List<TaxGroup> listTaxGroup = new List<TaxGroup>();
            
            try
            {
                String xmlDoc = Common.ToXml(this);
                DataTable dt = base.GetSelectedRecords(xmlDoc, SP_TAXGROUP_SEARCH, ref errorMsg);

                if (errorMsg.Trim().Length == 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TaxGroup taxGroupObj = new TaxGroup();
                            taxGroupObj.TaxGroupId = Convert.ToInt32(dt.Rows[i]["TaxGroupId"]);
                            taxGroupObj.TaxGroupCode = Convert.ToString(dt.Rows[i]["TaxGroupCode"]);
                            taxGroupObj.StartDate = Convert.ToDateTime(dt.Rows[i]["StartDate"]);
                            taxGroupObj.StartDateVal = Convert.ToDateTime(dt.Rows[i]["StartDate"]).ToString(Common.DTP_DATE_FORMAT);
                            taxGroupObj.TaxCodeId = Convert.ToInt32(dt.Rows[i]["TaxCodeId"]);
                            taxGroupObj.TaxCode = Convert.ToString(dt.Rows[i]["TaxCode"]);
                            taxGroupObj.GroupOrder = Convert.ToInt32(dt.Rows[i]["GroupOrder"]);
                            taxGroupObj.AppliedOn = Convert.ToString(dt.Rows[i]["AppliedOn"]);
                            taxGroupObj.AppliedOnText = Convert.ToString(dt.Rows[i]["AppliedOnText"]);
                            taxGroupObj.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                            taxGroupObj.StatusName = Convert.ToString(dt.Rows[i]["StatusName"]);
                            listTaxGroup.Add(taxGroupObj);
                        }
                    }
                }
                return listTaxGroup;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;

            }
        }

        

        #endregion
    }
}
