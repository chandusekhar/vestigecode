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
    public class ItemDetails: ItemMaster, IItemDetails, IItemPrice, IItemDimensions
    {
        private const string SP_ITEM_IMAGE = "usp_GetItemImage";
        private const string SP_ITEM_SEARCH = "usp_ItemSearch";
        private const string SP_ITEM_SAVE = "usp_ItemSave";
        private const string SP_ITEM_LOCATION_SEARCH = "usp_ItemLocationSearch";
        

        public const string GRID_ITEM_ID = "ItemId";
        public const string GRID_MODIFIED_DATE = "ModifiedDate";

        #region Variables

        System.String m_itemDisplayName = String.Empty, m_itemReceiptName = String.Empty, m_itemPrintName = String.Empty, m_itemShortName = String.Empty;
        System.String m_itemBarCode = String.Empty;
        String m_itemTaxCategoryName = String.Empty;
        System.Int32 m_itemTaxCategoryId = Common.INT_DBNULL;
        System.Decimal m_itemPrimaryCost = Common.INT_DBNULL;
        Decimal m_itemDistributorPrice = Common.INT_DBNULL;
        Decimal m_itemBusinessVolume = Common.INT_DBNULL;
        Decimal m_itemPointValue = Common.INT_DBNULL;
        Decimal m_itemMRP = Common.INT_DBNULL;
        System.Int32 m_isAvailableForGift = 2;        
        System.Int32 m_itemIsKit = 2, m_itemIsComposite = 2, m_itemPromoPart = 2;
        System.Int32 m_minKitValue = Common.INT_DBNULL;
        System.Int32 m_itemLength = Common.INT_DBNULL, m_itemWidth = Common.INT_DBNULL, m_itemHeight = Common.INT_DBNULL, m_itemStackLimit = Common.INT_DBNULL;
        System.Decimal  m_itemWeight  = Common.INT_DBNULL;
        System.String m_itemBayNumber = String.Empty;
        System.Int32 m_itemExpDuration = Common.INT_DBNULL;
        List<CompositeItem> m_compositeList = null;
        List<LocationList> m_locationList = null;
        List<ItemUOMDetails> m_itemUOMList = null;
        List<ItemBarCode> m_itemBarCodeList = null;

        System.Int32 m_itemSubCategoryId = Common.INT_DBNULL;
        System.String m_itemSubCategoryName = String.Empty;
        string m_locationId = String.Empty;
        string m_FromItemCode ="";
        Decimal m_TransferPrice = Common.INT_DBNULL;
        Decimal m_LandedPrice = Common.INT_DBNULL;
        int m_itemIsForRegistrationPurpose = Common.INT_DBNULL;
        int m_ItemTypeID = Common.INT_DBNULL;
        int m_ItemPackSize = Common.INT_DBNULL;
        int m_ExpiryDateFormat = Common.INT_DBNULL;
        #endregion

        #region Properties

        #region Merchandise Details
        public Int32 SubCategoryId
        {
            get { return m_itemSubCategoryId; }
            set { m_itemSubCategoryId = value; }
        }
        public String SubCategoryName
        {
            get { return m_itemSubCategoryName; }
            set { m_itemSubCategoryName = value; }
        }
        #endregion

        #region Item Names

        public String ShortName
        {
            get { return m_itemShortName; }
            set { m_itemShortName = value; }
        }

        public String DisplayName
        {
            get { return m_itemDisplayName; }
            set { m_itemDisplayName = value; }
        }

        public String PrintName
        {
            get { return m_itemPrintName; }
            set { m_itemPrintName = value; }
        }

        public String ReceiptName
        {
            get { return m_itemReceiptName; }
            set { m_itemReceiptName = value; }
        }
        #endregion
        //private List<ItemBatchDetails> m_grnBatchDetailList = null;
        //public List<ItemBatchDetails> GRNBatchDetailList
        //{
        //    get { return m_grnBatchDetailList; }
        //    set { m_grnBatchDetailList = value; }
        //}
        public decimal TransferPrice
        {
            get { return m_TransferPrice; }
            set { m_TransferPrice = value; }
        }
        public Decimal DisplayTransferPrice
        {
            get { return Math.Round(DBTransferPrice, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public Decimal DBTransferPrice
        {
            get { return Math.Round(TransferPrice, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public decimal LandedPrice
        {
            get { return m_LandedPrice; }
            set { m_LandedPrice = value; }
        }
        public Decimal DisplayLandedPrice
        {
            get { return Math.Round(DBLandedPrice, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public Decimal DBLandedPrice
        {
            get { return Math.Round(LandedPrice, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public String ItemBarCode
        {
            get { return m_itemBarCode; }
            set { m_itemBarCode = value; }
        }

        public String FromItemCode
        {
            get { return m_FromItemCode; }
            set { m_FromItemCode = value; }
        }

        public Int32 TaxCategoryId
        {
            get { return m_itemTaxCategoryId; }
            set { m_itemTaxCategoryId = value; }
        }

        public String TaxCategoryName
        {
            get { return m_itemTaxCategoryName; }
            set { m_itemTaxCategoryName = value; }
        }

        public Int32 ExpiryDuration
        {
            get { return m_itemExpDuration; }
            set { m_itemExpDuration = value; }
        }

        public Int32 IsKit
        {
            get { return m_itemIsKit; }
            set { m_itemIsKit = value; }
        }

        public Int32 MinKitValue
        {
            get { return m_minKitValue; }
            set { m_minKitValue = value; }
        }

        public Int32 IsComposite
        {
            get { return m_itemIsComposite; }
            set { m_itemIsComposite = value; }
        }

        public Int32 IsPromoPart
        {
            get { return m_itemPromoPart; }
            set { m_itemPromoPart = value; }
        }

        public System.Int32 IsAvailableForGift
        {
            get { return m_isAvailableForGift; }
            set { m_isAvailableForGift = value; }
        }

        public List<CompositeItem> CompositeItems
        {
            get { return m_compositeList; }
            set { m_compositeList = value; }
        }

        public List<LocationList> LocationsList
        {
            get { return m_locationList; }
            set { m_locationList = value; }
        }

        public List<ItemUOMDetails> ItemUOMList
        {
            get { return m_itemUOMList; }
            set { m_itemUOMList = value; }
        }

        public List<ItemBarCode> ItemBarCodeList
        {
            get { return m_itemBarCodeList; }
            set { m_itemBarCodeList = value; }
        }

        public string LocationId
        {
            get { return m_locationId; }
            set { m_locationId = value; }
        }

        public static int IsInternaladj
        {
            get  ; 
            set  ;
        }
        public int IsForRegistrationPurpose
        {
            get { return m_itemIsForRegistrationPurpose; }
            set { m_itemIsForRegistrationPurpose = value; }
        }
        #region Shopping Cart
        public string ItemImage
		{
			get;
			set;
		}
		public string WebDescription 
		{
			get;
			set;
		}
		public string USDPrice 
		{
			get;
			set;
        }
        #endregion
        public int ExpiryDateFormat
        {
            get { return m_ExpiryDateFormat; }
            set { m_ExpiryDateFormat = value; }
        }

        public int ItemPackSize
        {
            get { return m_ItemPackSize; }
            set { m_ItemPackSize = value; }
        }
        public int ItemTypeID
        {
            get { return m_ItemTypeID; }
            set { m_ItemTypeID = value; }
        }


        #region Pricing
        public Decimal PrimaryCost
        {
            get { return m_itemPrimaryCost; }
            set { m_itemPrimaryCost = value; }
        }
        public Decimal DisplayPrimaryCost
        {
            get { return Math.Round(DBPrimaryCost, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public Decimal DBPrimaryCost
        {
            get { return Math.Round(PrimaryCost, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public Decimal DistributorPrice
        {
            get { return m_itemDistributorPrice; }
            set { m_itemDistributorPrice = value; }
        }
        public Decimal DisplayDistributorPrice
        {
            get { return Math.Round(DBDistributorPrice, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public Decimal DBDistributorPrice
        {
            get { return Math.Round(DistributorPrice, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        /*public Decimal TransferPrice
        {
            get { return m_itemTransferPrice; }
            set { m_itemTransferPrice = value; }
        }*/
        public Decimal BusinessVolume
        {
            get { return m_itemBusinessVolume; }
            set { m_itemBusinessVolume = value; }
        }
        public Decimal DisplayBusinessVolume
        {
            get { return Math.Round(DBBusinessVolume, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public Decimal DBBusinessVolume
        {
            get { return Math.Round(BusinessVolume, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public Decimal PointValue
        {
            get { return m_itemPointValue; }
            set { m_itemPointValue = value; }
        }
        public Decimal DisplayPointValue
        {
            get { return Math.Round(DBPointValue, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public Decimal DBPointValue
        {
            get { return Math.Round(PointValue, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public Decimal MRP
        {
            get { return m_itemMRP; }
            set { m_itemMRP = value; }
        }
        public Decimal DisplayMRP
        {
            get { return Math.Round(DBMRP, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public Decimal DBMRP
        {
            get { return Math.Round(MRP, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        #endregion

        #region Dimensions
        public Int32 ItemLength
        {
            get { return m_itemLength; }
            set { m_itemLength = value; }
        }
        public Int32 ItemWidth
        {
            get { return m_itemWidth; }
            set { m_itemWidth = value; }
        }
        public Int32 ItemHeight
        {
            get { return m_itemHeight; }
            set { m_itemHeight = value; }
        }
       // public Int32 ItemWeight
        public Decimal ItemWeight
        {
            get { return m_itemWeight; }
            set { m_itemWeight = value; }
        }
        public Int32 StackLimit
        {
            get { return m_itemStackLimit; }
            set { m_itemStackLimit = value; }
        }
        public String BayNumber
        {
            get { return m_itemBayNumber; }
            set { m_itemBayNumber = value; }
        }
        #endregion 

        #endregion

        #region Methods
        public Boolean Save(ref string errorMessage)
        {
            try
            {
                Boolean isSuccess = false;
                isSuccess = base.Save(Common.ToXml(this), SP_ITEM_SAVE, ref errorMessage);
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Shopping Cart
        public string GetItemImageSearch(int itemid)
        {
            string fileName = null;
            try
            {
                string errorMessage = string.Empty;
                DataTable dt = GetItemImage(itemid, SP_ITEM_IMAGE);

                if (dt == null || dt.Rows.Count <= 0)
                    return null;

                fileName = Convert.ToString(dt.Rows[0]["ImageName"]);
            }
            catch (Exception ex)
            {
                throw ex;
                //Common.LogException(ex);
            }
            return fileName;
        }
        #endregion

        public List<ItemDetails> Search()
        {
            List<ItemDetails> lst = null;
            try
            {
                string errorMessage = string.Empty;
                DataSet dataSetItems = GetSelectedRecordsInDataSet(Common.ToXml(this), SP_ITEM_SEARCH, ref errorMessage);

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

        List<ItemDetails> CreateItemObject(DataSet currentDataSet)
        {
            List<ItemDetails> currentList = new List<ItemDetails>();
            
            foreach (DataRow drow in currentDataSet.Tables[0].Rows)
            {
                ItemDetails item = new ItemDetails();
                item.ItemId = Validators.CheckForDBNull(drow["ItemId"], (Int32)0);
                item.ItemCode = drow["ItemCode"].ToString();
              //  item.ItemBarCode = drow["ItemBarCode"].ToString();

                item.ItemName = drow["ItemName"].ToString();
                item.ShortName = drow["ShortName"].ToString();
                item.PrintName = drow["PrintName"].ToString();
                item.DisplayName = drow["DisplayName"].ToString();
                item.ReceiptName = drow["ReceiptName"].ToString();

                item.SubCategoryId = Convert.ToInt32(drow["SubCategoryId"]);
                item.SubCategoryName = Validators.CheckForDBNull(drow["SubCategoryName"], string.Empty);

                item.MRP = Convert.ToDecimal(Validators.CheckForDBNull(drow["ItemMRP"], (decimal)0));//Validators.CheckForDBNull(drow["ItemMRP"], (decimal)0);
                item.PrimaryCost = Convert.ToDecimal(Validators.CheckForDBNull(drow["ItemPrimaryCost"], (decimal)0));
                item.DistributorPrice = Convert.ToDecimal(drow["ItemDP"]);
                /*item.TransferPrice = Convert.ToDecimal(drow["ItemTP"]);*/
                item.BusinessVolume = Convert.ToDecimal(drow["ItemBV"]);
                item.PointValue = Convert.ToDecimal(drow["ItemPV"]);
                item.MinKitValue = Convert.ToInt32(drow["MinKitValue"]);
                item.IsAvailableForGift = Convert.ToInt32(drow["IsAvailableForGift"]);
                item.IsPromoPart = Convert.ToInt32(drow["IsPromoPart"]);
                item.IsKit = Convert.ToInt32(drow["IsKit"]);
                item.IsComposite = Convert.ToInt32(drow["IsComposite"]);
                item.TaxCategoryId = Convert.ToInt32(drow["TaxCategoryId"]);
                item.Status = Convert.ToInt32(drow["Status"]);
                item.StatusName = drow["StatusName"].ToString();
                
                #region Shopping Cart

                item.ItemImage = drow["ImageName"].ToString();
				item.WebDescription = drow["WebDescription"].ToString();
				item.USDPrice = drow["USDPrice"].ToString();

                #endregion


                //item dimensions
                item.ItemLength = Convert.ToInt32(drow["Length"]);
                item.ItemWidth = Convert.ToInt32(drow["Width"]);
                item.ItemHeight = Convert.ToInt32(drow["Height"]);
                item.StackLimit = Convert.ToInt32(drow["StackLimit"]);
                item.BayNumber = Convert.ToString(drow["BayNumber"]);
                item.ItemWeight = Convert.ToDecimal(drow["Weight"]);
                item.ExpiryDuration = Convert.ToInt32(drow["ExpDuration"]);
                item.IsForRegistrationPurpose = Convert.ToInt32(drow["IsForRegistrationPurpose"]);
            

                item.ModifiedBy = Validators.CheckForDBNull(drow["ModifiedBy"], (int)0);
                item.ModifiedDate = Validators.CheckForDBNull(drow["ModifiedDate"], Convert.ToDateTime(new DateTime(1900, 1, 1).ToString(Common.DATE_TIME_FORMAT)));

                item.LocationsList = (List<LocationList>)Validators.CheckForDBNull(CreateLocationListObject(currentDataSet.Tables[1], item.ItemId), (object)null);
                item.ItemUOMList = (List<ItemUOMDetails>)Validators.CheckForDBNull(CreateUOMListObject(currentDataSet.Tables[2], item.ItemId), (object)null);
                item.CompositeItems = (List<CompositeItem>)Validators.CheckForDBNull(CreateCompositeBOMListObject(currentDataSet.Tables[3], item.ItemId), (object) null);
                item.TransferPrice = Convert.ToDecimal(Validators.CheckForDBNull(drow["TransferPrice"], (decimal)0));
                item.LandedPrice = Convert.ToDecimal(Validators.CheckForDBNull(drow["LandedPrice"], (decimal)0));
                item.ItemTypeID = Convert.ToInt32(drow["ItemTypeID"] == DBNull.Value ? -1 : drow["ItemTypeID"]);
                item.ItemPackSize = Convert.ToInt32(drow["ItemPackSize"] == DBNull.Value ? 0 : drow["ItemPackSize"]);
                item.ExpiryDateFormat = Convert.ToInt32(drow["ExpiryDateFormat"] == DBNull.Value ? 1 : drow["ExpiryDateFormat"]);
                currentList.Add(item);
            }

            return currentList;
        }

        List<LocationList> CreateLocationListObject(DataTable currentDataTable, Int32 itemId)
        {
            List<LocationList> currentList = new List<LocationList>();
            foreach (DataRow drow in currentDataTable.Rows)
            {
                if (Validators.CheckForDBNull(drow["ItemId"], (Int32)0) == itemId)
                {
                    LocationList newLocation = new LocationList();
                    newLocation.ItemId = Validators.CheckForDBNull(drow["ItemId"], (Int32)0);
                    newLocation.LocationId = Validators.CheckForDBNull(drow["LocationId"], (Int32)0);
                    newLocation.LocationDisplayName = Validators.CheckForDBNull(drow["DisplayName"], string.Empty);
                    newLocation.ReorderLevel = Validators.CheckForDBNull(drow["ReorderLevel"], Convert.ToDecimal(Decimal.MinValue));
                    newLocation.Status = Validators.CheckForDBNull(drow["Status"], Convert.ToInt32(0));
                    newLocation.StatusName = Validators.CheckForDBNull(drow["StatusName"], String.Empty);

                    newLocation.ModifiedBy = Validators.CheckForDBNull(drow["ModifiedBy"], (int)0);
                    newLocation.ModifiedDate = Validators.CheckForDBNull(drow["ModifiedDate"], Common.DATETIME_NULL);

                    currentList.Add(newLocation);
                }
            }
            return currentList;
        }

        List<ItemUOMDetails> CreateUOMListObject(DataTable currentDataTable, Int32 itemId)
        {
            List<ItemUOMDetails> currentList = new List<ItemUOMDetails>();
            foreach (DataRow drow in currentDataTable.Rows)
            {
                if (Validators.CheckForDBNull(drow["ItemId"], Convert.ToInt32(0)) == itemId)
                {
                    if (currentList == null)
                        currentList = new List<ItemUOMDetails>();

                    ItemUOMDetails newUOM = new ItemUOMDetails();
                    newUOM.ItemId = Validators.CheckForDBNull(drow["ItemId"], Convert.ToInt32(Common.INT_DBNULL));
                    newUOM.ItemUOMId = Validators.CheckForDBNull(drow["ItemUOMId"], Convert.ToInt32(Common.INT_DBNULL));
                    newUOM.UOMId = Validators.CheckForDBNull(drow["UOMId"], Convert.ToInt32(Common.INT_DBNULL));
                    newUOM.TOMId = Validators.CheckForDBNull(drow["TOMId"], Convert.ToInt32(Common.INT_DBNULL));
                    newUOM.UOMName = Validators.CheckForDBNull(drow["UOMName"], String.Empty);
                    newUOM.TOMName = Validators.CheckForDBNull(drow["TOMName"], String.Empty);
                    newUOM.IsPrimary = Validators.CheckForDBNull(drow["IsPrimary"], false);
                    newUOM.ModifiedBy = Validators.CheckForDBNull(drow["MOdifiedBy"], Convert.ToInt32(Common.INT_DBNULL));
                    newUOM.ModifiedDate = Validators.CheckForDBNull(drow["UOMName"], Common.DATETIME_NULL);

                    currentList.Add(newUOM);
                }
            }
            return currentList;
        }

        List<CompositeItem> CreateCompositeBOMListObject(DataTable currentDataTable, Int32 itemId)
        {
            List<CompositeItem> currentList = new List<CompositeItem>();
            foreach (DataRow drow in currentDataTable.Rows)
            {
                if (Validators.CheckForDBNull(drow["CompositeItemId"], Convert.ToInt32(0)) == itemId)
                {
                    if (currentList == null)
                        currentList = new List<CompositeItem>();

                    CompositeItem newCompBOM = new CompositeItem();
                    newCompBOM.CompositeItemId = Convert.ToInt32(drow["CompositeItemId"]);
                    newCompBOM.ItemId = Convert.ToInt32(drow["ItemId"]);
                    newCompBOM.UOMId = Convert.ToInt32(drow["PurchasedUOM"]);
                    newCompBOM.UOMName = drow["UOMName"].ToString();
                    newCompBOM.Quantity = Convert.ToInt32(drow["Quantity"]);
                    newCompBOM.ModifiedBy = Convert.ToInt32(drow["ModifiedBy"]);
                    newCompBOM.ModifiedDate = Convert.ToDateTime(drow["ModifiedDate"]);
                    newCompBOM.ItemCode = drow["ItemCode"].ToString();
                    newCompBOM.IsTradable = Convert.ToBoolean(drow["IsTradable"]);
                    currentList.Add(newCompBOM);
                }
            }
            return currentList;
        }

        public List<ItemDetails> SearchLocationItem()
        {
            List<ItemDetails> lst = new List<ItemDetails>();
            try
            {
                if (IsInternaladj == 1)
                {
                    this.FromItemCode = null;
                }
                string errorMessage = string.Empty;
                DataSet dataSetItems = GetSelectedRecordsInDataSet(Common.ToXml(this), SP_ITEM_LOCATION_SEARCH, ref errorMessage);

                if (dataSetItems == null | dataSetItems.Tables.Count > 0 | dataSetItems.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSetItems.Tables[0].Rows)
                    {
                        ItemDetails item = new ItemDetails();
                        item.ItemId = Convert.ToInt32(drow["ItemId"]);
                        item.ItemCode = drow["ItemCode"].ToString();
                        item.ItemName = drow["ItemName"].ToString();
                        item.ShortName = drow["ShortName"].ToString();                       
                        item.DisplayName = drow["DisplayName"].ToString();
                        item.SubCategoryId = Convert.ToInt32(drow["SubCategoryId"]);
                        item.SubCategoryName = Validators.CheckForDBNull(drow["SubCategoryName"], string.Empty);
                        item.IsComposite = Convert.ToInt32(drow["IsComposite"]);
                        item.Status = Convert.ToInt32(drow["Status"]);
                        item.StatusName = drow["StatusName"].ToString();
                        item.LocationId = Convert.ToString(drow["LocationId"]);                        
                        lst.Add(item);
                    }
                }
                return lst;
                
            }
            catch (Exception ex)
            {
                throw ex;
                //Common.LogException(ex);
            }
        }


        public List<ItemDetails> SearchItemMaster()
        {
            List<ItemDetails> lst = new List<ItemDetails>();
            lst = Search();
            return lst;
        }

        #endregion
    }
}
