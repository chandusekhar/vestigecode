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
    [Serializable]
    public class TaxCode :Tax
    {
        public const string MODULE_CODE = "MDM09";
        
        #region Variable Declaration
                
        private int m_taxCodeId;

        public int TaxCodeId
        {
            get { return m_taxCodeId; }
            set { m_taxCodeId = value; }
        }

        private string m_taxCodeVal;

        public string TaxCodeVal
        {
            get { return m_taxCodeVal; }
            set { m_taxCodeVal = value; }
        }
        
        private string m_startDate;

        public string StartDate
        {
            get { return m_startDate; }
            set { m_startDate = value; }
        }

        private DateTime m_startDateVal;

        public DateTime StartDateVal
        {
            get { return m_startDateVal; }
            set { m_startDateVal = value; }
        }

        private decimal m_taxPercent;

        public decimal TaxPercent
        {
            get { return m_taxPercent; }
            set { m_taxPercent = value; }
        }

        private string m_description;

        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        private int m_status;

        public int Status
        {
            get { return m_status; }
            set { m_status = value; }
        }     

        private string statusText;

        public string StatusText
        {
            get { return statusText; }
            set { statusText = value; }
        }

        #endregion

        #region SP Declaration

        private const string SP_TAXCODE_SEARCH = "usp_TaxCodeSearch";
        private const string SP_TAXCODE_SAVE = "usp_TaxCodeSave";

        #endregion

        
        /// <summary>
        /// Search Tax Code
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns>List</returns>
        public List<TaxCode> SearchTaxCode(ref string errorMsg)
        {            
            List<TaxCode> listOfTaxCode = new List<TaxCode>();
            
            try
            {
                string xmlDoc = Common.ToXml(this);
                DataTable dt = base.GetSelectedRecords(xmlDoc, SP_TAXCODE_SEARCH, ref errorMsg);
                if (errorMsg.Trim().Length == 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TaxCode taxCodeObj = new TaxCode();

                            taxCodeObj.TaxCodeId = Convert.ToInt32(dt.Rows[i]["TaxCodeId"]);
                            taxCodeObj.TaxCodeVal = Convert.ToString(dt.Rows[i]["TaxCode"]);
                            taxCodeObj.StartDate = Convert.ToDateTime(dt.Rows[i]["StartDate"]).ToString(Common.DTP_DATE_FORMAT);
                            taxCodeObj.StartDateVal = Convert.ToDateTime(dt.Rows[i]["StartDate"]);
                            taxCodeObj.TaxPercent = Math.Round(Convert.ToDecimal(dt.Rows[i]["TaxPercent"]),Common.DisplayAmountRounding);
                            taxCodeObj.Description = Convert.ToString(dt.Rows[i]["Description"]);
                            //taxCodeObj.IsFormCApplicable = Convert.ToInt32(dt.Rows[i]["IsFormCTax"]);
                            //taxCodeObj.IsFormCApplicableText = Convert.ToString(dt.Rows[i]["IsFormCApplicableText"]);
                            taxCodeObj.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                            taxCodeObj.StatusText = Convert.ToString(dt.Rows[i]["StatusText"]);
                            listOfTaxCode.Add(taxCodeObj);                   
                        }

                    }
                }


                return listOfTaxCode;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;

            }

        }
              
        /// <summary>
        /// Save Tax Code
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns>Boolean</returns>
        public Boolean SaveTaxCode(ref String errorMsg)
        {            
            Boolean isSuccess = false;
            
             try
                {
                 string xmlDoc = Common.ToXml(this);
                 isSuccess = base.Save(xmlDoc,SP_TAXCODE_SAVE,ref errorMsg);
                                        
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
      
    }
}
