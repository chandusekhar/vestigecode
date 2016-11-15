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
    public class GiftVoucherDistributor
    {
        #region SP_Declaration

        private const string SP_GIFTVOUCHER_DISTRIBUTOR_SEARCH = "usp_GiftVoucherDistributorSearch";

        #endregion

        #region Constants
        private const string CON_FIELD_VOUCHERCODE = "GiftVoucherCode";
        private const string CON_FIELD_APPLICABLEFROM = "ApplicableFrom";
        private const string CON_FIELD_APPLICABLETO = "ApplicableTo";
        private const string CON_FIELD_SERIESID = "SeriesId";
        private const string CON_FIELD_AVAILED = "Availed";
        private const string CON_FIELD_VOUCHERSRNO = "VoucherSrNo";
        private const string CON_FIELD_AVAILEDDATE = "AvailedDate";
        private const string CON_FIELD_ISSUEDATE = "IssueDate";
        private const string CON_FIELD_ISSUETO = "IssuedTo";
        private const string CON_FIELD_BUYAMOUNT = "MinBuyAmount";
        private const string CON_FIELD_VOUCHERDESCRIPTION = "VoucherDescription";
        private const string CON_FIELD_VOUCHERNAME = "VoucherName";

        #endregion

        public GiftVoucherDistributor()
        {
            ApplicableFrom = Common.DATETIME_NULL.ToString();
            ApplicableTo = Common.DATETIME_NULL.ToString();
            AvailedDate = Common.DATETIME_NULL.ToString();
            IssueDate = Common.DATETIME_NULL.ToString();
            Availed = Common.INT_DBNULL;
        }
        #region Property

        public string GiftVoucherCode { get; set; }

        public int SeriesID { get; set; }

        public string IssueDate { get; set; }

        public string DisplayIssueDate
        {
            get { return string.IsNullOrEmpty(IssueDate)? "": Convert.ToDateTime(IssueDate).ToString(Common.DTP_DATE_FORMAT); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public int IssueTo { get; set; }

        public string VoucherSrNo { get; set; }

        public string ApplicableFrom { get; set; }

        public string ApplicableTo { get; set; }

        public int Availed { get; set; }

        public string AvailedDate { get; set; }

        public string DisplayAvailedDate
        {
            get { return string.IsNullOrEmpty(AvailedDate) ? "" : Convert.ToDateTime(AvailedDate).ToString(Common.DTP_DATE_FORMAT); }
            set { throw new NotImplementedException("This Property can not be explicitly set"); }
        }

        public string VoucherName { get; set; }

        public string VoucherDescription { get; set; }        

        public decimal MinBuyAmount { get; set; }

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


        #endregion

        #region Methods

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

        public virtual DataTable GetSelectedRecords(DBParameterList dbParam, string spName, ref string errorMessage)
        {
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

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

        public List<GiftVoucherDistributor> Search()
        {
            List<GiftVoucherDistributor> VoucherList = new List<GiftVoucherDistributor>();
            System.Data.DataTable dTable = new DataTable();
            try
            {
                string errorMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@VoucherCode", GiftVoucherCode, DbType.String));
                dbParam.Add(new DBParameter("@DistributorID", IssueTo, DbType.Int32));
                dbParam.Add(new DBParameter("@Availed", Availed, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                dTable = GetSelectedRecords(dbParam, SP_GIFTVOUCHER_DISTRIBUTOR_SEARCH, ref errorMessage);

                if (dTable != null)
                {
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        GiftVoucherDistributor _voucher = CreateVoucherObject(drow);
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

        private GiftVoucherDistributor CreateVoucherObject(DataRow dr)
        {
            try
            {
                GiftVoucherDistributor voucher = new GiftVoucherDistributor();
                voucher.ApplicableFrom = Convert.ToString(dr[CON_FIELD_APPLICABLEFROM]);
                voucher.ApplicableTo = Convert.ToString(dr[CON_FIELD_APPLICABLETO]);
                voucher.Availed = Convert.ToInt32(dr[CON_FIELD_AVAILED]);
                voucher.VoucherSrNo = Convert.ToString(dr[CON_FIELD_VOUCHERSRNO]);
                voucher.AvailedDate = Convert.ToString(dr[CON_FIELD_AVAILEDDATE]);
                voucher.IssueDate = Convert.ToString(dr[CON_FIELD_ISSUEDATE]);
                voucher.IssueTo = Convert.ToInt32(dr[CON_FIELD_ISSUETO]);
                voucher.GiftVoucherCode = Convert.ToString(dr[CON_FIELD_VOUCHERCODE]);
                voucher.SeriesID = Convert.ToInt32(dr[CON_FIELD_SERIESID]);
                voucher.MinBuyAmount = Convert.ToDecimal(dr[CON_FIELD_BUYAMOUNT]);
                voucher.VoucherDescription = Convert.ToString(dr[CON_FIELD_VOUCHERDESCRIPTION]);
                voucher.VoucherName = Convert.ToString(dr[CON_FIELD_VOUCHERNAME]);
                return voucher;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
