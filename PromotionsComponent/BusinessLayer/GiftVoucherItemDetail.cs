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
    public class GiftVoucherItemDetail
    {

        #region Property

        public string VoucherCode { get; set; }        
        public int ItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public bool IsComposite { get; set; }
        public string ShortName { get; set; }
        public string PrintName { get; set; }
        public string ReceiptName { get; set; }
        public string DisplayName { get; set; }
        public decimal MRP { get; set; }
        public decimal DistributorPrice { get; set; }
        public decimal BusinessVolume  { get; set; }
        public decimal PointValue { get; set; }
        public decimal Quantity   { get; set; }

        #endregion
        
        #region Database Fields
        private const string CON_FIELD_ITEMCODE = "ItemCode";
        private const string CON_FIELD_ITEMDESC = "ItemDescription";
        private const string CON_FIELD_ITEMID = "ItemId";
        private const string CON_FIELD_VOUCHERCODE = "GiftVoucherCode";
        private const string CON_FIELD_ISCOMPOSITE = "IsComposite";
        private const string CON_FIELD_SHORTNAME = "ShortName";
        private const string CON_FIELD_PRINTNAME = "PrintName";
        private const string CON_FIELD_RECEIPTNAME = "ReceiptName";
        private const string CON_FIELD_DISPLAYNAME = "DisplayName";
        private const string CON_FIELD_MRP = "MRP";
        private const string CON_FIELD_DISTRIBUTORPRICE = "DistributorPrice";
        private const string CON_FIELD_BUSINESSVOLUME = "BusinessVolume";
        private const string CON_FIELD_POINTVALUE = "PointValue";
        private const string CON_FIELD_QUANTITY = "Quantity";
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

        //public List<GiftVoucherItemDetail> Search()
        //{
        //    List<GiftVoucherItemDetail> VoucherList = new List<GiftVoucherItemDetail>();
        //    System.Data.DataTable dTable = new DataTable();
        //    try
        //    {
        //        string errorMessage = string.Empty;
        //        dTable = GetSelectedRecords(Common.ToXml(this), SP_GIFTVOUCHER_ITEM_SEARCH, ref errorMessage);

        //        if (dTable != null)
        //            foreach (System.Data.DataRow drow in dTable.Rows)
        //            {
        //                GiftVoucherItemDetail _voucher = CreateVoucherObject(drow);
        //                VoucherList.Add(_voucher);
        //            }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return VoucherList;
        //}

        public void CreateVoucherObject(DataRow dr)
        {
            try
            {                
                this.ItemCode = Convert.ToString(dr[CON_FIELD_ITEMCODE]);
                this.ItemDescription = Convert.ToString(dr[CON_FIELD_ITEMDESC]);
                this.ItemID = Convert.ToInt32(dr[CON_FIELD_ITEMID]);
                this.VoucherCode = Convert.ToString(dr[CON_FIELD_VOUCHERCODE]);
                this.IsComposite = Convert.ToBoolean(dr[CON_FIELD_ISCOMPOSITE]);
                this.BusinessVolume = Convert.ToDecimal(dr[CON_FIELD_BUSINESSVOLUME]);
                this.DisplayName = Convert.ToString(dr[CON_FIELD_DISPLAYNAME]);
                this.DistributorPrice = Convert.ToDecimal(dr[CON_FIELD_DISTRIBUTORPRICE]);
                this.MRP = Convert.ToDecimal(dr[CON_FIELD_MRP]);
                this.PointValue = Convert.ToDecimal(dr[CON_FIELD_POINTVALUE]);
                this.PrintName = Convert.ToString(dr[CON_FIELD_PRINTNAME]);
                this.Quantity = Convert.ToDecimal(dr[CON_FIELD_QUANTITY]);
                this.ReceiptName = Convert.ToString(dr[CON_FIELD_RECEIPTNAME]);
                this.ShortName = Convert.ToString(dr[CON_FIELD_SHORTNAME]);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
       

    }
}
