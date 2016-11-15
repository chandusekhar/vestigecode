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
    public class GiftVoucher
    {
        #region SP_Declaration
        private const string SP_GIFTVOUCHER_SAVE = "usp_GiftVoucherSave";
        private const string SP_GIFTVOUCHER_SEARCH = "usp_GiftVoucherSearch";

        #endregion
        public GiftVoucher()
        {
            VoucherItemDetailList = new List<GiftVoucherItemDetail>();
            VoucherDetailList = new List<GiftVoucherDetail>();
        }

        #region Database Fields
        private const string CON_FIELD_CREATEDBY = "CreatedBy";
        private const string CON_FIELD_CREATEDDATE = "CreatedDate";
        private const string CON_FIELD_MODIFYBY = "ModifiedBy";
        private const string CON_FIELD_MODIFYDATE = "ModifiedDate";
        private const string CON_FIELD_ITEMCODE = "ItemCode";
        private const string CON_FIELD_ITEMDESC = "ItemDescription";
        private const string CON_FIELD_ITEMID = "ItemId";
        private const string CON_FIELD_VOUCHERCODE = "GiftVoucherCode";
        private const string CON_FIELD_MINBUYAMOUNT = "MinBuyAmount";
        private const string CON_FIELD_VOUCHERDESCRIPTION = "VoucherDescription";
        private const string CON_FIELD_VOUCHERNAME = "VoucherName";

        #endregion

        #region Property

        public string VoucherCode{get;set;}

        public string VoucherName{get;set;}

        public string VoucherDescription{get;set;}

        public string ItemCode{get;set;}

        public int ItemID{get;set;}

        public string ItemDescription{get;set;}

        public decimal MinBuyAmount{get;set;}

        public decimal DisplayMinBuyAmount
        {
            get { return Math.Round(DBMinBuyAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBMinBuyAmount
        {
            get { return Math.Round(MinBuyAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public List<GiftVoucherItemDetail> VoucherItemDetailList { get; set; }

        public List<GiftVoucherDetail> VoucherDetailList{get;set;}

        public int CreatedBy { get; set; }
       
        public string CreatedDate{get;set;}        

        public string DisplayCreatedDate
        {
            get { return Convert.ToDateTime(CreatedDate).ToString(Common.DTP_DATE_FORMAT); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public int ModifiedBy{ get; set; }
        
        public DateTime ModifiedDate{ get; set; }
       
        private DateTime m_toDate = Common.DATETIME_NULL;
        public DateTime ToDate
        {
            get
            {
                return m_toDate;
            }
            set
            {
                m_toDate = value;
            }
        }
        
        private DateTime m_fromDate = Common.DATETIME_NULL;
        public DateTime FromDate
        {
            get
            {
                return m_fromDate;
            }
            set
            {
                m_fromDate = value;
            }
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

                            DataTable dt = dtManager.ExecuteDataTable(SP_GIFTVOUCHER_SAVE, dbParam);
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
                                    this.VoucherCode = Convert.ToString(dt.Rows[0]["VoucherCode"]);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        dtManager.RollbackTransaction();
                        throw ex;
                    }
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GiftVoucher> Search()
        {
            List<GiftVoucher> VoucherList = new List<GiftVoucher>();
            //System.Data.DataTable dTable = new DataTable();
            System.Data.DataSet dSet = new DataSet();
            try
            {
                string errorMessage = string.Empty;
                // dTable = GetSelectedRecords(Common.ToXml(this), SP_GIFTVOUCHER_SEARCH, ref errorMessage);
                dSet = GetSelectedRecordsDataSet(Common.ToXml(this), SP_GIFTVOUCHER_SEARCH, ref errorMessage);
                if (dSet != null && dSet.Tables.Count > 0)
                {
                    foreach (System.Data.DataRow drow in dSet.Tables[0].Rows)
                    {
                        GiftVoucher _voucher = CreateVoucherObject(drow);
                        if (dSet.Tables[1] != null)
                            _voucher.GetVoucherItemDetailList(dSet.Tables[1]);
                        if (dSet.Tables[2] != null)
                            _voucher.GetVoucherDetailList(dSet.Tables[2]);
                        VoucherList.Add(_voucher);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return VoucherList;
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

        private DataSet GetSelectedRecordsDataSet(string xmlDoc, string spName, ref string errorMessage)
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
                DataSet ds = dtManager.ExecuteDataSet(spName, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        private void GetVoucherItemDetailList(DataTable dt)
        {
            List<GiftVoucherItemDetail> ListVoucher = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] drCollection = dt.Select("GiftVoucherCode='" + this.VoucherCode + "'");
                ListVoucher = new List<GiftVoucherItemDetail>();

                for (int i = 0; i < drCollection.Length; i++)
                {
                    GiftVoucherItemDetail voucher = new GiftVoucherItemDetail();
                    voucher.CreateVoucherObject(drCollection[i]);
                    ListVoucher.Add(voucher);
                }
                this.VoucherItemDetailList= ListVoucher;
            }
        }

        private GiftVoucher CreateVoucherObject(DataRow dr)
        {
            try
            {
                GiftVoucher voucher = new GiftVoucher();
                voucher.CreatedBy = Convert.ToInt32(dr[CON_FIELD_CREATEDBY]);
                voucher.CreatedDate = Convert.ToString(dr[CON_FIELD_CREATEDDATE]);
                //voucher.ItemCode = Convert.ToString(dr[CON_FIELD_ITEMCODE]);
                //voucher.ItemDescription = Convert.ToString(dr[CON_FIELD_ITEMDESC]);
                //voucher.ItemID = Convert.ToInt32(dr[CON_FIELD_ITEMID]);
                voucher.MinBuyAmount = Convert.ToDecimal(dr[CON_FIELD_MINBUYAMOUNT]);
                voucher.VoucherCode = Convert.ToString(dr[CON_FIELD_VOUCHERCODE]);
                voucher.VoucherDescription = Convert.ToString(dr[CON_FIELD_VOUCHERDESCRIPTION]);
                voucher.VoucherName = Convert.ToString(dr[CON_FIELD_VOUCHERNAME]);
                voucher.ModifiedBy = Convert.ToInt32(dr[CON_FIELD_MODIFYBY]);
                voucher.ModifiedDate = Convert.ToDateTime(dr[CON_FIELD_MODIFYDATE]);
                return voucher;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetVoucherDetailList(DataTable dt)
        {
            try
            {
                List<GiftVoucherDetail> ListVoucher = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] drCollection = dt.Select("GiftVoucherCode='" + this.VoucherCode + "'");
                    ListVoucher = new List<GiftVoucherDetail>();

                    for (int i = 0; i < drCollection.Length; i++)
                    {
                        GiftVoucherDetail voucher = new GiftVoucherDetail();
                        voucher = voucher.CreateVoucherObject(drCollection[i]);
                        ListVoucher.Add(voucher);
                    }
                    this.VoucherDetailList = ListVoucher;
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
