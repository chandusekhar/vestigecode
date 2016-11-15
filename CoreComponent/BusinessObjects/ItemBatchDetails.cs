using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.MasterData.BusinessObjects
{
    [Serializable]
    public class ItemBatchDetails : ItemMaster
    {
        #region SP Declaration
        private const string SP_ITEM_BATCHDETAIL_SEARCH = "usp_ItemBatchDetailSearch";
        private const string SP_ITEM_BATCH_SEARCH = "usp_ItemBatchSearch";
        private const string SP_ITEM_BATCH_MASTER_SEARCH = "usp_ItemBatchMasterSearch";
        private const string SP_VENDOR_ITEM_SEARCH = "usp_VendorItemSearch";
        #endregion 

        #region Variables

        decimal m_MRP, m_Quantity;
        string m_batchNo, m_manufactureBatchNo, m_mfgDate, m_expDate, m_bucketName, m_locationId, m_bucketId, m_VendorId, m_tomfgDate, m_toexpDate;
        string m_TobatchNo, m_TomanufactureBatchNo;
         static Int32 spcallforbatch =0;
        //int m_status;
        #endregion

        #region Properties

         public int SerialNo
         {
             get;
             set;
         }
        public string LocationId
        {
            get { return m_locationId; }
            set { m_locationId = value; }
        }

        public string VendorId
        {
            get
            {
                return m_VendorId;
            }
            set
            {
                m_VendorId = value;
            }
        }

        public string BucketId
        {
            get { return m_bucketId; }
            set { m_bucketId = value; }
        }

        public string BucketName
        {
            get { return m_bucketName; }
            set { m_bucketName = value; }
        }

        public decimal Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }

        public decimal DisplayQuantity
        {
            get { return Math.Round(DBQuantity, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBQuantity
        {
            get { return Math.Round(Quantity, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        //public int Status
        //{
        //    get { return m_status; }
        //    set { m_status = value; }
        //}

        public string ToManufactureBatchNo
        {
            get { return m_TomanufactureBatchNo; }
            set { m_TomanufactureBatchNo = value; }
        }

        public string ToBatchNo
        {
            get { return m_TobatchNo; }
            set { m_TobatchNo = value; }
        }



        public string ManufactureBatchNo
        {
            get { return m_manufactureBatchNo; }
            set { m_manufactureBatchNo = value; }
        }

        public string BatchNo
        {
            get { return m_batchNo; }
            set { m_batchNo = value; }
        }

        public string MfgDate
        {
            get { return m_mfgDate; }
            set { m_mfgDate = value; }
        }

        public string DisplayMfgDate
        {
            get
            {
                return Convert.ToDateTime(MfgDate).ToString(Common.DTP_DATE_FORMAT);
            }
            set
            {
                MfgDate = value;
            }
        }

        public string ExpDate
        {
            get { return m_expDate; }
            set { m_expDate = value; }
        }

        public string DisplayExpDate
        {
            get
            {
                return Convert.ToDateTime(ExpDate).ToString(Common.DTP_DATE_FORMAT);
            }
            set
            {
                ExpDate = value;
            }
        }

        public decimal MRP
        {
            get { return m_MRP; }
            set { m_MRP = value; }
        }

        public decimal DisplayMRP
        {
            get { return Math.Round(DBMRP, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal DBMRP
        {
            get { return Math.Round(MRP, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public DateTime FromMfgDate
        {
            get;
            set;
        }

        public DateTime ToMfgDate
        {
            get;
            set;
        }

        public static Int32 Spcall
        {
            get { return spcallforbatch; }
            set { spcallforbatch = value; }
            //get;
            //set;
        }
        public  Int32 Mode
        {

            get;
            set;
        }

        public string Manufacure
        {
            get { return m_tomfgDate; }
            set { m_tomfgDate = value; }
        }

        public string DisplayManufacure
        {
            get
            {
                return Convert.ToDateTime(Manufacure).ToString(Common.DTP_DATE_FORMAT);
            }
            set
            {
                Manufacure = value;
            }
        }

        public string Expiry
        {
            get { return m_toexpDate; }
            set { m_toexpDate = value; }
        }

        public string DisplayExpiry
        {
            get
            {
                return Convert.ToDateTime(Expiry).ToString(Common.DTP_DATE_FORMAT);
            }
            set
            {
                Expiry = value;
            }
        }

        #endregion

        #region Methods



        public List<ItemBatchDetails> SearchItemBatchMaster()
        {
            List<ItemBatchDetails> lst = null;
            try
            {
                string errorMessage = string.Empty;
                Status = 1;

                DataSet dataSetItems = GetSelectedRecordsInDataSet(Common.ToXml(this), SP_ITEM_BATCH_MASTER_SEARCH, ref errorMessage);

                if (dataSetItems == null | dataSetItems.Tables.Count <= 0 | dataSetItems.Tables[0].Rows.Count <= 0)
                    return null;

                lst = CreateItemObject(dataSetItems);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return lst;
        }
        public List<ItemBatchDetails> SearchItemReturnToVendor()
        {
            List<ItemBatchDetails> lst = null;
            try
            {
                string errorMessage = string.Empty;
                Status = 1;

                DataSet dataSetItems = GetSelectedRecordsInDataSet(Common.ToXml(this), SP_VENDOR_ITEM_SEARCH , ref errorMessage);

                if (dataSetItems == null | dataSetItems.Tables.Count <= 0 | dataSetItems.Tables[0].Rows.Count <= 0)
                    return null;

                lst = CreateItemObject(dataSetItems);
            }
            catch (Exception ex)
            {
                throw ex;
                //Common.LogException(ex);
            }
            return lst;
        }

        ///// <summary>
        ///// To Create Item Object
        ///// </summary>
        ///// <param name="currentDataSet"></param>
        ///// <returns></returns>
        //List<ItemBatchDetails> CreateItemBatchMasterObject(DataSet currentDataSet)
        //{
        //    List<ItemBatchDetails> currentList = new List<ItemBatchDetails>();
        //    foreach (DataRow drow in currentDataSet.Tables[0].Rows)
        //    {
        //        ItemBatchDetails item = new ItemBatchDetails();
        //        item.ItemId = Validators.CheckForDBNull(drow["ItemId"], (Int32)0);
        //        item.ItemCode = drow["ItemCode"].ToString();

        //        item.ItemName = drow["ItemName"].ToString();
        //        item.BatchNo = drow["BatchNo"].ToString();
        //        item.ManufactureBatchNo = drow["ManufactureBatchNo"].ToString();
        //        item.MfgDate = drow["MfgDate"].ToString();
        //        item.MRP = Convert.ToDecimal(drow["MRP"]);
        //        item.ExpDate = drow["ExpDate"].ToString();

        //        currentList.Add(item);
        //    }

        //    return currentList;
        //}

        /// <summary>
        /// To Search Item Batch Information
        /// </summary>
        /// <returns></returns>
        public List<ItemBatchDetails> Search()
        {
            List<ItemBatchDetails> lst = null;
            try
            {
                string errorMessage = string.Empty;
                Status = 1;
                this.Mode = Spcall;
                    DataSet dataSetItems = GetSelectedRecordsInDataSet(Common.ToXml(this), SP_ITEM_BATCH_SEARCH, ref errorMessage);
                

                if (dataSetItems == null | dataSetItems.Tables.Count <= 0 | dataSetItems.Tables[0].Rows.Count <= 0)
                    return null;

                lst = CreateItemObject(dataSetItems);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return lst;
        }

        public List<ItemBatchDetails> SearchBatchDetail()
        {
            List<ItemBatchDetails> lst = null;
            try
            {
                string errorMessage = string.Empty;
                Status = 1;

                DataSet dataSetItems = GetSelectedRecordsInDataSet(Common.ToXml(this), SP_ITEM_BATCHDETAIL_SEARCH, ref errorMessage);

                if (dataSetItems == null | dataSetItems.Tables.Count <= 0 | dataSetItems.Tables[0].Rows.Count <= 0)
                    return null;

                lst = CreateItemObject(dataSetItems);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return lst;
        }

        /// <summary>
        /// To Create Item Object
        /// </summary>
        /// <param name="currentDataSet"></param>
        /// <returns></returns>
        List<ItemBatchDetails> CreateItemObject(DataSet currentDataSet)
        {
            List<ItemBatchDetails> currentList = new List<ItemBatchDetails>();
            foreach (DataRow drow in currentDataSet.Tables[0].Rows)
            {
                ItemBatchDetails item = new ItemBatchDetails();
                item.ItemId = Validators.CheckForDBNull(drow["ItemId"], (Int32)0);
                item.ItemCode = drow["ItemCode"].ToString();

                item.ItemName = drow["ItemName"].ToString();
                item.BatchNo = drow["BatchNo"].ToString();
                item.ManufactureBatchNo = drow["ManufactureBatchNo"].ToString();
                item.MfgDate = drow["MfgDate"].ToString();
                item.Quantity = Convert.ToDecimal(drow["Quantity"]);
                item.MRP = Convert.ToDecimal(drow["MRP"]);
                item.BucketId = drow["BucketId"].ToString();
                item.BucketName = drow["BucketName"].ToString();
                item.ExpDate = drow["ExpDate"].ToString();

                currentList.Add(item);
            }

            return currentList;
        }

        #endregion
    }
}
