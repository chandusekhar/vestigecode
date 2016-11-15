using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;

namespace TransfersComponent.BusinessObjects
{
    [Serializable]
    public class TIHeader : TransferOrder
    {
        public const string MODULE_CODE = "TSF03";

        #region SP Declaration
        private const string SP_TI_SEARCH = "usp_TISearch";
        private const string SP_TI_SAVE = "usp_TISave";
        private const string SP_TI_ITEM_SEARCH = "usp_TIItemSearch";
        #endregion

        #region Variable Declaration
        int m_stateId = Common.INT_DBNULL;
        int m_packSize = Common.INT_DBNULL;
        string m_toNumber, m_toiNumber;
        List<TIDetail> m_tiItems;
        string m_fromTODate, m_toTODate, m_fromRecDate, m_toRecDate;
        string m_shippingBillNo, m_shippingDetails, m_remarks, m_receivedDate, m_receivedTime, m_toShippingDate;
        decimal m_totalTIQuantity, m_totalTIAmount, m_grossWeight, m_totalTOQuantity, m_totalTOAmount;
        #endregion SP Declaration

        public TIHeader()
        {
            Indentised = Common.INT_DBNULL;
        }

        #region Property
        public decimal TotalTOAmount
        {
            get { return m_totalTOAmount; }
            set { m_totalTOAmount = value; }
        }
        public decimal DisplayTotalTOAmount
        {
            get { return Math.Round(DBTotalTOAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBTotalTOAmount
        {
            get { return Math.Round(TotalTOAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal TotalTOQuantity
        {
            get { return m_totalTOQuantity; }
            set { m_totalTOQuantity = value; }
        }
        public decimal DisplayTotalTOQuantity
        {
            get { return Math.Round(DBTotalTOQuantity, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBTotalTOQuantity
        {
            get { return Math.Round(TotalTOQuantity, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }


        public decimal GrossWeight
        {
            get { return m_grossWeight; }
            set { m_grossWeight = value; }
        }
        public decimal TotalTIQuantity
        {
            get { return m_totalTIQuantity; }
            set { m_totalTIQuantity = value; }
        }
        public decimal DisplayTotalTIQuantity
        {
            get { return Math.Round(DBTotalTIQuantity, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBTotalTIQuantity
        {
            get { return Math.Round(TotalTIQuantity, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal TotalTIAmount
        {
            get { return m_totalTIAmount; }
            set { m_totalTIAmount = value; }
        }
        public decimal DisplayTotalTIAmount
        {
            get { return Math.Round(DBTotalTIAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBTotalTIAmount
        {
            get { return Math.Round(TotalTIAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public string ShippingDetails
        {
            get { return m_shippingDetails; }
            set { m_shippingDetails = value; }
        }
        public string Remarks
        {
            get { return m_remarks; }
            set { m_remarks = value; }
        }
        public string ShippingBillNo
        {
            get { return m_shippingBillNo; }
            set { m_shippingBillNo = value; }
        }
        public string TOShippingDate
        {
            get { return m_toShippingDate; }
            set { m_toShippingDate = value; }
        }
        public string DisplayTOShippingDate
        {
            get
            {
                if (TOShippingDate != null && TOShippingDate.ToString().Length > 0)
                    return Convert.ToDateTime(TOShippingDate).ToString(Common.DTP_DATE_FORMAT);
                else
                    return string.Empty;
            }
            set { TOShippingDate = value; }
        }

        public string ReceivedTime
        {
            get { return m_receivedTime; }
            set { m_receivedTime = value; }
        }
        public string ReceivedDate
        {
            get { return m_receivedDate; }
            set { m_receivedDate = value; }
        }
        public string DisplayReceivedDate
        {
            get
            {
                if (ReceivedDate != null && ReceivedDate.ToString().Length > 0)
                    return Convert.ToDateTime(ReceivedDate).ToString(Common.DTP_DATE_FORMAT);
                else
                    return string.Empty;
            }
            set { ReceivedDate = value; }
        }

        public string FromTODate
        {
            get { return m_fromTODate; }
            set { m_fromTODate = value; }
        }
        public string ToTODate
        {
            get { return m_toTODate; }
            set { m_toTODate = value; }
        }
        public string FromReceiveDate
        {
            get { return m_fromRecDate; }
            set { m_fromRecDate = value; }
        }
        public string ToReceiveDate
        {
            get { return m_toRecDate; }
            set { m_toRecDate = value; }
        }
        public int PackSize
        {
            get { return m_packSize; }
            set { m_packSize = value; }
        }
        public int StateId
        {
            get { return m_stateId; }
            set { m_stateId = value; }
        }

        public string TOINumber
        {
            get { return m_toiNumber; }
            set { m_toiNumber = value; }
        }
        public string TONumber
        {
            get { return m_toNumber; }
            set { m_toNumber = value; }
        }
        public List<TIDetail> TIItems
        {
            get { return m_tiItems; }
            set { m_tiItems = value; }
        }

    #endregion

        #region Methods

        /// <summary>
        /// Return a list of TIs
        /// </summary>
        /// <returns></returns>
        public List<TIHeader> Search()
        {
            List<TIHeader> tiList = new List<TIHeader>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetSelectedRecords(CoreComponent.Core.BusinessObjects.Common.ToXml(this), SP_TI_SEARCH, ref errorMessage);

                if (dTable == null | dTable.Rows.Count == 0)
                    return null;

                byte columnSearched = 0;
                foreach (DataColumn column in dTable.Columns)
                {
                    Console.WriteLine(column.ColumnName);
                    if ((column.ColumnName.ToLower() == "Isexported".ToLower()))
                        columnSearched = 1;
                }

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    TIHeader objTI = new TIHeader();
                    objTI.ModifiedDate = drow["ModifiedDate"].ToString();
                    objTI.PackSize = Convert.ToInt32(drow["PackSize"]);
                    objTI.SourceLocationId = Convert.ToInt32(drow["SourceLocationId"]);
                    objTI.SourceAddress = drow["SourceAddress"].ToString();
                    objTI.DestinationAddress = drow["DestinationAddress"].ToString();
                    objTI.ReceivedDate = drow["ReceivedDate"].ToString();
                    objTI.ReceivedTime = drow["ReceivedTime"].ToString();
                    objTI.Remarks = drow["Remarks"].ToString();
                    objTI.ShippingDetails = drow["ShippingDetails"].ToString();
                    objTI.DestinationLocationId = Convert.ToInt32(drow["DestinationLocationId"]);
                    objTI.StatusId = Convert.ToInt32(drow["Status"]);
                    objTI.StatusName = drow["StatusName"].ToString();
                    objTI.ShippingBillNo = drow["ShippingWayBillNo"].ToString();
                    objTI.TOINumber = drow["TOINumber"].ToString();
                    objTI.TONumber = drow["TONumber"].ToString();
                    objTI.TNumber = drow["TINumber"].ToString();
                    objTI.GrossWeight = Math.Round(Convert.ToDecimal(drow["GrossWeight"]),3);
                    objTI.TOShippingDate = drow["TOShippingDate"].ToString();
                    
                    objTI.TotalTOQuantity = Convert.ToDecimal(drow["TotalTOQuantity"]);
                    objTI.TotalTOAmount = Convert.ToDecimal(drow["TotalTOAmount"]);


                    objTI.TotalTIQuantity = Convert.ToDecimal(drow["TotalTIQuantity"]);
                    objTI.TotalTIAmount = Convert.ToDecimal(drow["TotalTIAmount"]);
                   

                    if (columnSearched == 1)
                    {
                        objTI.Isexported = Convert.ToBoolean(drow["Isexported"] );
                        objTI.Isexport = (objTI.Isexported == true ? 1 : 0);
                    }
                    else
                    {
                        objTI.Isexported = false;
                        objTI.Isexport = 0;
                    }


                    tiList.Add(objTI);
                }
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return tiList;
        }

        /// <summary>
        /// Save TI
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Save(string xmlString, ref string errorMessage)
        {
            bool isSuccess = false;
            // call the save method which returns whether the save was successfull or not
            isSuccess = base.Save(xmlString, SP_TI_SAVE, ref errorMessage);
            return isSuccess;
        }

        /// <summary>
        /// To Search Item
        /// </summary>
        /// <param name="toNumber"></param>
        /// <param name="sourceAddressId"></param>
        /// <returns></returns>
        public List<TIDetail> SearchItem(string toNumber, string tiNumber, int sourceAddressId)
        {
            List<TIDetail> tiItemList = new List<TIDetail>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetSelectedItems(toNumber, tiNumber, sourceAddressId, SP_TI_ITEM_SEARCH, ref errorMessage);

                if (dTable == null | dTable.Rows.Count == 0)
                    return null;

                tiItemList = CreateTOTIObject(dTable);
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return tiItemList;
        }

        List<TIDetail> CreateTOTIObject(System.Data.DataTable dTable)
        {
            List<TIDetail> tiItemList = new List<TIDetail>();
            foreach (System.Data.DataRow drow in dTable.Rows)
            {
                TIDetail objTI = new TIDetail();
                objTI.ItemCode = drow["ItemCode"].ToString();
                //objTI.IndentNo = drow["IndentNo"].ToString();
                objTI.BatchNo = drow["BatchNo"].ToString();
                objTI.ItemName = drow["ItemDescription"].ToString();
                objTI.ItemUnitPrice = Convert.ToDecimal(drow["TransferPrice"]);
                objTI.ItemTotalAmount = Convert.ToDecimal(drow["TotalAmount"]);
                objTI.UOMId = Convert.ToInt32(drow["UOMId"].ToString());
                objTI.UOMName = drow["UOMName"].ToString();

                objTI.AfterAdjustQty = Convert.ToDecimal(drow["AfterAdjustQty"]);
                objTI.RequestQty = Convert.ToDecimal(drow["RequestQty"]);
                objTI.AvailableQty = Convert.ToDecimal(drow["AvailableQty"]);

                objTI.Weight = Convert.ToDecimal(drow["Weight"]);
                objTI.MRP = Convert.ToDecimal(drow["MRP"]);
                objTI.MfgDate = drow["MfgDate"].ToString();
                objTI.ExpDate = drow["ExpDate"].ToString();

                objTI.ManufactureBatchNo = drow["ManufactureBatchNo"].ToString();
                objTI.BucketId = Convert.ToInt32(drow["BucketId"]);
                objTI.BucketName = drow["BucketName"].ToString();

                objTI.ItemId = Convert.ToInt32(drow["ItemId"]);
                objTI.RowNo = Convert.ToInt32(drow["RowNo"]);

                tiItemList.Add(objTI);
            }
            return tiItemList;
        }
        
        /// <summary>
        /// Search Item Details and returns DataTable
        /// </summary>
        /// <param name="toNumber"></param>
        /// <param name="tiNumber"></param>
        /// <param name="sourceAddressId"></param>
        /// <returns></returns>
        public DataTable SearchItemDataTable(string toNumber, string tiNumber, int sourceAddressId)
        {
            DataTable dTable = new DataTable();
            try
            {
                string errorMessage = string.Empty;
                dTable = base.GetSelectedItems(toNumber, tiNumber, sourceAddressId, SP_TI_ITEM_SEARCH, ref errorMessage);

                if (dTable == null | dTable.Rows.Count == 0)
                    return null;                
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return dTable;
        }

        #endregion
    }
}
