using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;

namespace TransfersComponent.BusinessObjects
{
    [Serializable]
    public class TOI : TransferOrder
    {
        public const string MODULE_CODE = "TSF01";
        
        #region SP Declaration
        private const string SP_TOI_SEARCH = "usp_TOISearch";
        private const string SP_TOI_ITEM_SEARCH = "usp_TOIItemSearch";
        private const string SP_TOI_SAVE = "usp_TOISave";
        #endregion

        DateTime m_fromTOIDate, m_toTOIDate, m_fromCreationDate, m_toCreationDate;
        string m_toiDate, m_creationDate, m_poNumber, m_poStatusName;
        decimal m_totalTOIQuantity, m_totalTOITaxAmount, m_totalTOIAmount;
        List<TOIDetail> m_toiItems;
        List<int> m_removeItems;
        int m_stateId, m_poStatus;
        static int exportMode;
        string m_appDep = string.Empty;
        string m_percentage = string.Empty;
        string m_priceMode = string.Empty;

        #region Property

        public  int ExportMode
        {
            get { return exportMode; }
            set { exportMode = value; }
        }
        public int StateId
        {
            get { return m_stateId; }
            set { m_stateId = value; }
        }
        public List<int> RemoveItems
        {
            get { return m_removeItems; }
            set { m_removeItems = value; }
        }
        public List<TOIDetail> TOIItems
        {
            get { return m_toiItems; }
            set { m_toiItems = value; }
        }
        public decimal TotalTOIQuantity
        {
            get { return m_totalTOIQuantity; }
            set { m_totalTOIQuantity = value; }
        }
        public decimal DisplayTotalTOIQuantity
        {
            get { return Math.Round(DBTotalTOIQuantity, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBTotalTOIQuantity
        {
            get { return Math.Round(TotalTOIQuantity, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal TotalTOITaxAmount
        {
            get { return m_totalTOITaxAmount; }
            set { m_totalTOITaxAmount = value; }
        }
        public decimal DisplayTotalTOITaxAmount
        {
            get { return Math.Round(DBTotalTOITaxAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBTotalTOITaxAmount
        {
            get { return Math.Round(TotalTOITaxAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal TotalTOIAmount
        {
            get { return m_totalTOIAmount; }
            set { m_totalTOIAmount = value; }
        }
        public decimal DisplayTotalTOIAmount
        {
            get { return Math.Round(DBTotalTOIAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public decimal DBTotalTOIAmount
        {
            get { return Math.Round(TotalTOIAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public string Percentage
        {
            get { return m_percentage; }
            set { m_percentage = value; }
        }
        public string PriceMode
        {
            get { return m_priceMode; }
            set { m_priceMode = value; }
        }
        public string AppDep
        {
            get { return m_appDep; }
            set { m_appDep = value; }
        }
        public string PONumber
        {
            get { return m_poNumber; }
            set { m_poNumber = value; }
        }
        public string POStatusName
        {
            get { return m_poStatusName; }
            set { m_poStatusName = value; }
        }
        public int POStatus
        {
            get { return m_poStatus; }
            set { m_poStatus = value; }
        }
        public string TOIDate
        {
            get { return m_toiDate; }
            set { m_toiDate = value; }
        }
        public string DisplayTOIDate
        {
            get {
                if (TOIDate != null && TOIDate.ToString().Length > 0)
                    return Convert.ToDateTime(TOIDate).ToString(Common.DTP_DATE_FORMAT);
                else
                    return string.Empty; 
            }
            set { TOIDate = value; }
        }
        public string CreationDate
        {
            get { return m_creationDate; }
            set { m_creationDate = value; }
        }
        public string DisplayCreationDate
        {
            get {
                if (CreationDate == null || CreationDate.ToString().Length > 0)
                    return Convert.ToDateTime(CreationDate).ToString(Common.DTP_DATE_FORMAT);
                else
                    return string.Empty; 
            
            }
            set { CreationDate = value; }
        }
        public DateTime FromTOIDate
        {
            get { return m_fromTOIDate; }
            set { m_fromTOIDate = value; }
        }
        public DateTime ToTOIDate
        {
            get { return m_toTOIDate; }
            set { m_toTOIDate = value; }
        }
        public DateTime FromCreationDate
        {
            get { return m_fromCreationDate; }
            set { m_fromCreationDate = value; }
        }
        public DateTime ToCreationDate
        {
            get { return m_toCreationDate; }
            set { m_toCreationDate = value; }
        }
        public string TOICreatedBY
        {
            get;
            set;
        }
        #endregion

        #region Method
        /// <summary>
        /// To Return Transfer Price
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public decimal CalTransferPrice(  bool modeValue, string itemCode, string locationId)
        {
            decimal price = 0;
            try
            {
                string errorMessage = string.Empty;
                price = base.CalculateTransferPrice(modeValue,itemCode, locationId, ref errorMessage);
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return price;
        }
        /// <summary>
        /// To Save TOI
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Save(string xmlString, ref string errorMessage)
        {
            bool isSuccess = false;
            // call the save method which returns whether the save was successfull or not
            isSuccess = base.Save(xmlString, SP_TOI_SAVE, ref errorMessage);
            return isSuccess;
        }

        /// <summary>
        /// Return a list of TOIs
        /// </summary>
        /// <returns></returns>
        public List<TOI> Search()
        {
            List<TOI> toiList = new List<TOI>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetSelectedRecords(CoreComponent.Core.BusinessObjects.Common.ToXml(this), SP_TOI_SEARCH, ref errorMessage);

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
                    TOI objTOI = new TOI();
                    objTOI.ModifiedDate = drow["ModifiedDate"].ToString();
                    objTOI.SourceLocationId = Convert.ToInt32(drow["SourceLocationId"]);
                    objTOI.SourceAddress = drow["SourceAddress"].ToString();
                    objTOI.DestinationAddress = drow["DestinationAddress"].ToString();
                    objTOI.Indentised = Convert.ToInt32(drow["Indentised"]);
                    
                    objTOI.DestinationLocationId = Convert.ToInt32(drow["DestinationLocationId"]);
                    objTOI.StatusId = Convert.ToInt32(drow["Status"]);
                    objTOI.StatusName = drow["StatusName"].ToString();
                    objTOI.TNumber = drow["TOINumber"].ToString();
                    objTOI.CreationDate = drow["CreationDate"].ToString();
                    objTOI.TOIDate = drow["TOIDate"].ToString();
                    objTOI.StateId = Convert.ToInt32(drow["StateId"]); // Used in TO Time, We need to fetch STN Remarks based on StateId
                    objTOI.TotalTOIQuantity = Convert.ToDecimal(drow["TotalTOIQuantity"]);
                    objTOI.TotalTOIAmount = Convert.ToDecimal(drow["TotalTOIAmount"]);
                    objTOI.PONumber = drow["PONumber"].ToString();
                    objTOI.POStatus = Convert.ToInt32(drow["POStatus"]);
                    objTOI.POStatusName = drow["POStatusName"].ToString();
                    objTOI.Percentage = drow["Percentage"].ToString();
                    objTOI.PriceMode = drow["PriceMode"].ToString();
                    objTOI.AppDep = drow["AppDep"].ToString();
                    objTOI.TOICreatedBY = drow["TOICreatedBY"].ToString();
                    if (columnSearched == 1)
                        objTOI.Isexport = (Convert.ToBoolean(drow["Isexported"]) == true ? 1 : 0);
                    else
                        objTOI.Isexport = 0;

                    toiList.Add(objTOI);
                }
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return toiList;
        }
        /// <summary>
        /// Return a list of Items
        /// </summary>
        /// <param name="toiNumber"></param>
        /// <param name="sourceAddressId"></param>
        /// <returns></returns>
        public List<TOIDetail> SearchItem(string toiNumber, int sourceAddressId)
        {
            List<TOIDetail> toiItemList = new List<TOIDetail>();
            try
            {
                string errorMessage = string.Empty;
                System.Data.DataTable dTable = base.GetSelectedItems(toiNumber, sourceAddressId, SP_TOI_ITEM_SEARCH, ref errorMessage);

                if (dTable == null | dTable.Rows.Count == 0)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    TOIDetail objTOI = new TOIDetail();
                    objTOI.ItemCode = drow["ItemCode"].ToString();
                    //objTOI.IndentNo = drow["IndentNo"].ToString();
                    
                    objTOI.ItemName = drow["ItemDescription"].ToString();
                    objTOI.ItemUnitPrice = Convert.ToDecimal(drow["TransferPrice"]);
                    objTOI.ItemTotalAmount =  Convert.ToDecimal(drow["TotalAmount"]);
                    objTOI.UnitQty =  Convert.ToDecimal(drow["ItemQuantity"]);
                    objTOI.AvailableQty =  Convert.ToDecimal(drow["AvailableQty"]);
                    objTOI.UOMId = Convert.ToInt32(drow["UOMId"]);
                    

                    objTOI.ModifiedDate = drow["ModifiedDate"].ToString();
                    objTOI.UOMName = drow["UOMName"].ToString();
                    objTOI.BucketId = Convert.ToInt32(drow["BucketId"]);
                    objTOI.BucketName = drow["BucketName"].ToString();

                    objTOI.ItemId = Convert.ToInt32(drow["ItemId"]);
                    objTOI.RowNo = Convert.ToInt32(drow["RowNo"]);

                    toiItemList.Add(objTOI);
                }
            }
            catch (Exception ex)
            {
                CoreComponent.Core.BusinessObjects.Common.LogException(ex);
            }
            return toiItemList;
        }

        public DataTable SearchItemDataTable(string toiNumber, int sourceAddressId)
        {
            List<TOIDetail> toiItemList = new List<TOIDetail>();
            DataTable dTable = new DataTable();
            try
            {
                string errorMessage = string.Empty;
                dTable = base.GetSelectedItems(toiNumber, sourceAddressId, SP_TOI_ITEM_SEARCH, ref errorMessage);

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
