using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using System.Data;
namespace PromotionsComponent.BusinessLayer
{
    [Serializable]
    public  class GiftVoucherDetail
    { 
        #region SP_Declaration
      
        private const string SP_GIFTVOUCHER_SEARCH = "usp_GiftVoucherDetailSearch";
    
        #endregion

        #region Constants
        private const string CON_FIELD_VOUCHERCODE = "GiftVoucherCode";
        private const string CON_FIELD_ENDSERIES = "EndSeries";
        private const string CON_FIELD_SERIESID = "SeriesId";
        private const string CON_FIELD_STARTSERIES = "StartSeries";
        private const string CON_FIELD_STATUS = "Status";
        private const string CON_FIELD_APPLICABLEFROM = "ApplicableFrom";
        private const string CON_FIELD_APPLICABLETO = "ApplicableTo";
        
        #endregion

        public GiftVoucherDetail()
        {
            ApplicableFrom=Common.DATETIME_CURRENT.ToString();
            ApplicableTo=Common.DATETIME_CURRENT.ToString();
        }
        #region Property

        public string GiftVoucherCode{get;set;}

        public int SeriesID{get;set;}

        public string EndSeries{get;set;}

        public string StartSeries{get;set;}
       
        public int StartRange{get;set;}

        public int EndRange{get;set;}

        public string StartText{get;set;}

        public bool IsActive{get;set;}

        public string ApplicableFrom{get;set;}

        public string DisplayApplicableFrom
        {
            get { return Convert.ToDateTime(ApplicableFrom).ToString(Common.DTP_DATE_FORMAT); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }
      
        public string ApplicableTo{get;set;}

        public string DisplayApplicableTo
        {
            get { return Convert.ToDateTime(ApplicableTo).ToString(Common.DTP_DATE_FORMAT); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        #endregion

        #region Methods

        public List<GiftVoucherDetail> Search()
        {
            List<GiftVoucherDetail> VoucherList = new List<GiftVoucherDetail>();
            System.Data.DataTable dTable = new DataTable();
            try
            {
                string errorMessage = string.Empty;
                dTable = GetSelectedRecords(Common.ToXml(this), SP_GIFTVOUCHER_SEARCH, ref errorMessage);

                if (dTable != null)
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        GiftVoucherDetail _voucher = CreateVoucherObject(drow);
                        VoucherList.Add(_voucher);
                    }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return VoucherList;
        }

        public GiftVoucherDetail CreateVoucherObject(DataRow dr)
        {
            try
            {
                GiftVoucherDetail voucher = new GiftVoucherDetail();
                voucher.EndSeries = Convert.ToString(dr[CON_FIELD_ENDSERIES]);
                voucher.GiftVoucherCode = Convert.ToString(dr[CON_FIELD_VOUCHERCODE]);
                voucher.SeriesID = Convert.ToInt32(dr[CON_FIELD_SERIESID]);
                voucher.StartSeries = Convert.ToString(dr[CON_FIELD_STARTSERIES]);
                voucher.IsActive = Convert.ToBoolean(dr[CON_FIELD_STATUS]);
                voucher.ApplicableTo = Convert.ToString(dr[CON_FIELD_APPLICABLETO]);
                voucher.ApplicableFrom = Convert.ToString(dr[CON_FIELD_APPLICABLEFROM]);
                return voucher;
            }
            catch (Exception ex)
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
        #endregion
    }
}
