using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;

namespace TransfersComponent.BusinessObjects
{
    [Serializable]
    public class TOHeader : TransferOrder
    {
        public const string MODULE_CODE = "TSF02";

        #region SP Declaration
        private const string SP_TO_SEARCH = "usp_TOSearch";
        private const string SP_TO_ADJUST_ITEM = "usp_TOAdjustItemSearch";
        private const string SP_TO_SAVE = "usp_TOSave";
        private const string SP_TO_ITEM_SEARCH = "usp_TOItemSearch";
        #endregion

        #region Variable Declaration
        int m_stateId = Common.INT_DBNULL;
        int m_packSize = Common.INT_DBNULL;
        string m_toiNumber;
        List<TODetail> m_toItems;
        DateTime m_fromTODate, m_toTODate, m_fromShipDate, m_toShipDate;
        string m_shipDate, m_creationDate, m_refNumber, m_expectedDeliveryDate, m_shippingBillNo, m_shippingDetails,m_remarks,m_exportNo,m_importNo;
        //Bikram: Export Envoice
        string m_ExporterRef, m_OtherRef, m_BuyerOtherthanConsignee, m_PreCarriage, m_PlaceofReceiptbyPreCarrier, m_VesselflightNo, m_PortofLoading, m_PortofDischarge, m_PortofDestination, m_TermsofDelivery, m_DELIVERY, m_PAYMENT, m_IECCode, m_CountryOfOrigin, m_CountryOfDestination;
        string m_TOCreationDate, m_TOIDate, m_BuyerOrderNo, m_BuyerOrderDate;

        public string BuyerOrderDate
        {
            get { return m_BuyerOrderDate; }
            set { m_BuyerOrderDate = value; }
        }

        public string BuyerOrderNo
        {
            get { return m_BuyerOrderNo; }
            set { m_BuyerOrderNo = value; }
        }

        public string TOIDate
        {
            get { return m_TOIDate; }
            set { m_TOIDate = value; }
        }

        public string TOCreationDate
        {
            get { return m_TOCreationDate; }
            set { m_TOCreationDate = value; }
        }

        public string CountryOfOrigin
        {
            get { return m_CountryOfOrigin; }
            set { m_CountryOfOrigin = value; }
        }
        public string CountryOfDestination
        {
            get { return m_CountryOfDestination; }
            set { m_CountryOfDestination = value; }
        }

        public string PAYMENT
        {
            get { return m_PAYMENT; }
            set { m_PAYMENT = value; }
        }

        public string DELIVERY
        {
            get { return m_DELIVERY; }
            set { m_DELIVERY = value; }
        }

        public string TermsofDelivery
        {
            get { return m_TermsofDelivery; }
            set { m_TermsofDelivery = value; }
        }

        public string PortofDestination
        {
            get { return m_PortofDestination; }
            set { m_PortofDestination = value; }
        }

        public string PortofDischarge
        {
            get { return m_PortofDischarge; }
            set { m_PortofDischarge = value; }
        }

        public string PortofLoading
        {
            get { return m_PortofLoading; }
            set { m_PortofLoading = value; }
        }

        public string VesselflightNo
        {
            get { return m_VesselflightNo; }
            set { m_VesselflightNo = value; }
        }

        public string PlaceofReceiptbyPreCarrier
        {
            get { return m_PlaceofReceiptbyPreCarrier; }
            set { m_PlaceofReceiptbyPreCarrier = value; }
        }

        public string PreCarriage
        {
            get { return m_PreCarriage; }
            set { m_PreCarriage = value; }
        }

        public string BuyerOtherthanConsignee
        {
            get { return m_BuyerOtherthanConsignee; }
            set { m_BuyerOtherthanConsignee = value; }
        }

        public string OtherRef
        {
            get { return m_OtherRef; }
            set { m_OtherRef = value; }
        }

        public string ExporterRef
        {
            get { return m_ExporterRef; }
            set { m_ExporterRef = value; }
        }
        decimal m_totalTOQuantity, m_totalTOAmount, m_grossWeight;
        private bool MessageFlag = false;
        #endregion SP Declaration

        #region Property
        public decimal GrossWeight
        {
            get { return m_grossWeight; }
            set { m_grossWeight = value; }
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

        public string ShippingDetails
        {
            get { return m_shippingDetails; }
            set { m_shippingDetails = value; }
        }
        //public string ExportNo
        //{
        //    get { return m_exportNo; }
        //    set { m_exportNo = value; }
        //}
        //public string ImportNo
        //{
        //    get { return m_importNo; }
        //    set { m_importNo = value; }
        //}
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

        public string RefNumber
        {
            get { return m_refNumber; }
            set { m_refNumber = value; }
        }
        public string CreationDate
        {
            get { return m_creationDate; }
            set { m_creationDate = value; }
        }
        public string DisplayCreationDate
        {
            get{
                if (CreationDate != null && CreationDate.ToString().Length > 0)
                    return Convert.ToDateTime(CreationDate).ToString(Common.DTP_DATE_FORMAT);
                else
                    return string.Empty; 
            }
            set{CreationDate = value;}
        }
        public string ShipDate
        {
            get { return m_shipDate; }
            set { m_shipDate = value; }
        }
        public string DisplayShipDate
        {
            get
            {
                if (ShipDate != null && ShipDate.ToString().Length > 0)
                    return Convert.ToDateTime(ShipDate).ToString(Common.DTP_DATE_FORMAT);
                else
                    return string.Empty; 
            }
            set{ShipDate = value;}
        }

        public string ExpectedDeliveryDate
        {
            get { return m_expectedDeliveryDate; }
            set { m_expectedDeliveryDate = value; }
        }

        public DateTime FromTODate
        {
            get { return m_fromTODate; }
            set { m_fromTODate = value; }
        }
        public DateTime ToTODate
        {
            get { return m_toTODate; }
            set { m_toTODate = value; }
        }
        public DateTime FromShipDate
        {
            get { return m_fromShipDate; }
            set { m_fromShipDate = value; }
        }
        public DateTime ToShipDate
        {
            get { return m_toShipDate; }
            set { m_toShipDate = value; }
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

        //public int LocationId
        //{
        //    get { return m_locationId; }
        //    set { m_locationId = value; }
        //}

        public string TOINumber
        {
            get { return m_toiNumber; }
            set { m_toiNumber = value; }
        }

        public List<TODetail> TOItems
        {
            get { return m_toItems; }
            set { m_toItems = value; }
        }
        public string ModifiedByName
        {
            get;
            set;
        }
        public string SourcePhone
        {
            get;
            set;
        }
        public string email1
        {
            get;
            set;
        }
        public string SourceCity
        {
            get;
            set;
        }
        public string DestinationCity
        {
            get;
            set;
        }
        public string IECCode
        {
            get{return m_IECCode;}
            set { m_IECCode = value;}
        }
        #endregion

        #region Methods

        /// <summary>
        /// Return a list of TOs
        /// Bikram: Export Envoice
        /// </summary>
        /// <returns></returns>
        public List<TOHeader> Search()
        {
            List<TOHeader> toiList = new List<TOHeader>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetSelectedRecords(CoreComponent.Core.BusinessObjects.Common.ToXml(this), SP_TO_SEARCH, ref errorMessage);

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
                    TOHeader objTO = new TOHeader();
                    objTO.ModifiedDate = drow["ModifiedDate"].ToString();
                    objTO.PackSize = Convert.ToInt32(drow["PackSize"]);
                    objTO.SourceLocationId = Convert.ToInt32(drow["SourceLocationId"]);
                    objTO.SourceAddress = drow["SourceAddress"].ToString();
                    objTO.DestinationAddress = drow["DestinationAddress"].ToString();
                    objTO.ExpectedDeliveryDate = drow["ExpectedDeliveryDate"].ToString();
                    objTO.Remarks = drow["Remarks"].ToString();
                    objTO.ShippingDetails = drow["ShippingDetails"].ToString();
                    objTO.DestinationLocationId = Convert.ToInt32(drow["DestinationLocationId"]);
                    objTO.StatusId = Convert.ToInt32(drow["Status"]);
                    objTO.StatusName = drow["StatusName"].ToString();
                    objTO.ShippingBillNo = drow["ShippingWayBillNo"].ToString();
                    objTO.RefNumber = drow["RefNumber"].ToString();
                    objTO.TOINumber = drow["TOINumber"].ToString();
                    objTO.TNumber = drow["TONumber"].ToString();
                    objTO.GrossWeight = Math.Round(Convert.ToDecimal(drow["GrossWeight"]),3);
                    objTO.ShipDate = drow["ShipDate"].ToString();
                    objTO.CreationDate = drow["CreationDate"].ToString();
                    objTO.StateId = Convert.ToInt32(drow["StateId"]); // Used in TO Time, We need to fetch STN Remarks based on StateId
                    objTO.TotalTOQuantity = Convert.ToDecimal(drow["TotalTOQuantity"]);
                    objTO.TotalTOAmount = Convert.ToDecimal(drow["TotalTOAmount"]);
                    objTO.SourcePhone = drow["SourcePhone"].ToString(); // Added for TO Screen Report
                    objTO.ModifiedByName = drow["ModifiedByName"].ToString(); // Added for TO Screen Report
                    objTO.email1 = drow["EmailId1"].ToString(); //Added for TO Screen Report
                    objTO.SourceCity = drow["SourceCity"].ToString(); //Added for TO Screen Report
                    objTO.DestinationCity = drow["DestinationCity"].ToString(); //Added for TO Screen Report
                    //objTO.ExportNo = drow["ExportNo"].ToString();
                    //objTO.ImportNo = drow["ImportNo"].ToString();
                    objTO.IECCode = drow["IECCode"].ToString();
                    //Bikram:ExportInvoice
                    objTO.ExporterRef = drow["ExporterRef"].ToString();
                    objTO.OtherRef = drow["OtherRef"].ToString();
                    objTO.BuyerOtherthanConsignee = drow["BuyerOtherthanConsignee"].ToString();
                    objTO.PreCarriage = drow["PreCarriage"].ToString();
                    objTO.PlaceofReceiptbyPreCarrier = drow["PlaceofReceiptbyPreCarrier"].ToString();
                    objTO.CountryOfOrigin = drow["CountryOfOrigin"].ToString();
                    objTO.CountryOfDestination = drow["CountryOfDestination"].ToString();
                    objTO.VesselflightNo = drow["VesselflightNo"].ToString();
                    objTO.PortofLoading = drow["PortofLoading"].ToString();
                    objTO.PortofDischarge = drow["PortofDischarge"].ToString();
                    objTO.PortofDestination = drow["PortofDestination"].ToString();
                    objTO.BuyerOrderNo = drow["BuyerOrderNo"].ToString();
                    objTO.BuyerOrderDate = drow["BuyerOrderDate"].ToString();
                    objTO.TermsofDelivery = drow["TermsofDelivery"].ToString();
                    objTO.DELIVERY = drow["DELIVERY"].ToString();
                    objTO.PAYMENT = drow["PAYMENT"].ToString();                    
                    objTO.TOCreationDate = drow["TOCreationDate"].ToString();
                    objTO.TOIDate = drow["TOIDate"].ToString();
                    if (columnSearched == 1)
                    {
                        objTO.Isexported = Convert.ToBoolean(drow["Isexported"]);
                        objTO.Isexport = (objTO.Isexported == true ? 1 : 0);
                    }
                    else
                    {
                        objTO.Isexported = false;
                        objTO.Isexport = 0;
                    }
                    
                    toiList.Add(objTO);
                }
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return toiList;
        }
        /// <summary>
        /// Save TO
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Save(string xmlString, ref string errorMessage)
        {
            bool isSuccess = false;
            // call the save method which returns whether the save was successfull or not
            isSuccess = base.Save(xmlString, SP_TO_SAVE, ref errorMessage);
            //if (isSuccess == false && errorMessage.Contains("INF0229"))
            //{
            //    MessageBox.Show(Common.GetMessage("INF0229"));
            //}
            return isSuccess;
        }

        /// <summary>
        /// To Search and return list of FIFO Items
        /// </summary>
        /// <param name="toiNumber"></param>
        /// <param name="locationId"></param>
        /// <param name="errMessage"></param>
        /// <returns></returns>
        public List<TODetail> AdjustItemAndBatch(string toiNumber, int locationId, ref string errMessage)
        {
            List<TODetail> toItemList = new List<TODetail>();
            try
            {
                System.Data.DataTable dTable = base.GetSelectedItems(toiNumber, locationId, SP_TO_ADJUST_ITEM, ref errMessage);

                if (dTable == null | dTable.Rows.Count == 0)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    TODetail objTO = new TODetail();
                    objTO.ItemCode = drow["ItemCode"].ToString();
                   // objTO.IndentNo = drow["IndentNo"].ToString();
                    objTO.ItemName = drow["ItemDescription"].ToString();
                    objTO.BucketId = Convert.ToInt32(drow["BucketId"]);
                    objTO.BucketName = drow["BucketName"].ToString();
                    objTO.BatchNo = drow["BatchNo"].ToString();
                    objTO.UOMId = Convert.ToInt32(drow["UOMId"]);
                    objTO.UOMName = drow["UOMName"].ToString();
                    objTO.ManufactureBatchNo = drow["ManufactureBatchNo"].ToString();

                    objTO.MRP = Convert.ToDecimal(drow["MRP"]);
                    objTO.MfgDate = Convert.ToDateTime(drow["MfgDate"].ToString()).ToString(CoreComponent.Core.BusinessObjects.Common.DATE_TIME_FORMAT);
                    objTO.ExpDate = Convert.ToDateTime(drow["ExpDate"].ToString()).ToString(CoreComponent.Core.BusinessObjects.Common.DATE_TIME_FORMAT); ;

                    objTO.ManufactureBatchNo = drow["ManufactureBatchNo"].ToString();
                    objTO.ItemUnitPrice = Convert.ToDecimal(drow["TransferPrice"]);
                    objTO.ItemTotalAmount = Convert.ToDecimal(drow["TotalAmount"]);

                    objTO.AfterAdjustQty = Convert.ToDecimal(drow["AfterAdjustQty"]);
                    objTO.RequestQty = Convert.ToDecimal(drow["RequestQty"]);
                    objTO.AvailableQty = Convert.ToDecimal(drow["AvailableQty"]);

                    objTO.ItemId = Convert.ToInt32(drow["ItemId"]);
                    objTO.RowNo = Convert.ToInt32(drow["RowNo"]);
                    objTO.Weight = Convert.ToDecimal(drow["Weight"]);
                    toItemList.Add(objTO);
                }
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return toItemList;
        }

        /// <summary>
        /// To Search Item
        /// </summary>
        /// <param name="toNumber"></param>
        /// <param name="sourceAddressId"></param>
        /// <returns></returns>
        public List<TODetail> SearchItem(string toNumber, int sourceAddressId)
        {
            List<TODetail> toItemList = new List<TODetail>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetSelectedItems(toNumber, sourceAddressId, SP_TO_ITEM_SEARCH, ref errorMessage);

                if (dTable == null | dTable.Rows.Count == 0)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    TODetail objTO = new TODetail();
                    objTO.ItemCode = drow["ItemCode"].ToString();
                   // objTO.IndentNo = drow["IndentNo"].ToString();
                    objTO.BatchNo = drow["BatchNo"].ToString();
                    objTO.ItemName = drow["ItemDescription"].ToString();
                    objTO.ItemUnitPrice = Convert.ToDecimal(drow["TransferPrice"]);
                    objTO.ItemTotalAmount = Convert.ToDecimal(drow["TotalAmount"]);
                    objTO.UOMId = Convert.ToInt32(drow["UOMId"]);
                    objTO.UOMName = drow["UOMName"].ToString();

                    objTO.AfterAdjustQty = Convert.ToDecimal(drow["AfterAdjustQty"]);
                    objTO.RequestQty = Convert.ToDecimal(drow["RequestQty"]);
                    objTO.AvailableQty = Convert.ToDecimal(drow["AvailableQty"]);

                    objTO.Weight = Convert.ToDecimal(drow["Weight"]);
                    objTO.MRP = Convert.ToDecimal(drow["MRP"]);
                    objTO.MfgDate = drow["MfgDate"].ToString();
                    objTO.ExpDate = drow["ExpDate"].ToString();

                    objTO.ManufactureBatchNo = drow["ManufactureBatchNo"].ToString();
                    objTO.BucketId = Convert.ToInt32(drow["BucketId"]);
                    objTO.BucketName = drow["BucketName"].ToString();

                    objTO.ItemId = Convert.ToInt32(drow["ItemId"]);
                    objTO.RowNo = Convert.ToInt32(drow["RowNo"]);

                    //Bikranm:ExportInvoice
                    objTO.EachCartonQty = Convert.ToInt32(drow["EachCartonQty"] == DBNull.Value ? "0" : drow["EachCartonQty"].ToString());
                    objTO.ContainerNOFromTo = drow["ContainerNOFromTo"] == DBNull.Value ? "" : drow["ContainerNOFromTo"].ToString();
                    objTO.GrossWeightItem = Convert.ToDecimal(drow["GrossWeightItem"] == DBNull.Value ? "0" : drow["GrossWeightItem"].ToString());

                    //objTO.TaxCategoryName = drow["TaxCategoryName"].ToString();
                    //objTO.TaxPercent = drow["TaxPercent"].ToString();
                    toItemList.Add(objTO);
                }
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return toItemList;
        }

        public DataTable SearchItemDataTable(string toNumber, int sourceAddressId)
        {
            DataTable dTable = new DataTable(); ;
            try
            {
                string errorMessage = string.Empty;
                dTable = base.GetSelectedItems(toNumber, sourceAddressId, SP_TO_ITEM_SEARCH, ref errorMessage);

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
