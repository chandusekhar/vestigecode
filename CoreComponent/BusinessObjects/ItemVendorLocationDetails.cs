using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using System.Data;
using System.Data.SqlClient;
using Vinculum.Framework.Data;
using Vinculum.Framework;
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.MasterData.BusinessObjects
{
    public class ItemVendorLocationDetails : ItemVendor, ILocation
    {
        private const String SP_VENDORLOC_SEARCH = "usp_ItemVendorLocationSearch";
        private const String SP_VENDORLOC_SAVE = "usp_ItemVendorLocationSave";
        private const String SP_ITEMVENDOR_LINK = "usp_ItemVendorLink_Search";
        private const String SP_LOCITEMVENDOR = "usp_LocationsItemVendorLoc";
        private const String SP_ITEMFORVENDORLOCATION = "usp_ItemsForVendorLocation";
        

        private Int32 m_locationId = Common.INT_DBNULL;
        private String m_locationName = String.Empty;
        private String m_locationCode = String.Empty;
        private Int32 m_leadTime = Common.INT_DBNULL;
        private Decimal m_minQty = Convert.ToDecimal(Common.INT_DBNULL);
        private Decimal m_locationCost = Convert.ToDecimal(Common.INT_DBNULL);
        private Int32 m_primaryForLoc = Convert.ToInt32(System.Windows.Forms.CheckState.Indeterminate);
        private Decimal m_purchaseUnitFactor = Convert.ToDecimal(Common.INT_DBNULL);
        private List<ItemVendorLocationDetails> m_listVendorLoc = null;

        public Int32 LocationId
        {
            get { return m_locationId; }
            set { m_locationId = value; }
        }

        public String LocationCode
        {
            get { return m_locationCode; }
            set { m_locationCode = value; }
        }

        public String LocationName
        {
            get { return m_locationName; }
            set { m_locationName = value; }
        }

        public Int32 LeadTime
        {
            get { return m_leadTime; }
            set { m_leadTime = value; }
        }

        public Decimal CostForLocation
        {
            get { return m_locationCost; }
            set { m_locationCost = value; }
        }
        public Decimal DisplayCostForLocation
        {
            get { return Math.Round(DBCostForLocation, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }            
        }
        public Decimal DBCostForLocation
        {
            get { return Math.Round(CostForLocation, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }            
        }
        public Decimal MinOrderQuantity
        {
            get { return m_minQty; }
            set { m_minQty = value; }
        }
        public Decimal DisplayMinOrderQuantity
        {
            get { return Math.Round(DBMinOrderQuantity, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public Decimal DBMinOrderQuantity
        {
            get { return Math.Round(MinOrderQuantity, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public Int32 IsVendorPrimaryForLocation
        {
            get { return m_primaryForLoc; }
            set { m_primaryForLoc = value; }
        }

        public Int32 IsCompositeItem
        {
            get;
            set;
        }

        public Decimal PurchaseUnitFactor
        {
            get { return m_purchaseUnitFactor; }
            set { m_purchaseUnitFactor = value; }
        }
        public Decimal DisplayPurchaseUnitFactor
        {
            get { return Math.Round(DBPurchaseUnitFactor, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public Decimal DBPurchaseUnitFactor
        {
            get { return Math.Round(PurchaseUnitFactor, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        public List<ItemVendorLocationDetails> ListOfVendorLocs
        {
            get { return m_listVendorLoc; }
            set { m_listVendorLoc = value; }
        }

        public Int32 IsInclusiveofTax
        {
            get;
            set;
        }

        public List<ItemVendorLocationDetails> Search()
        {
            List<ItemVendorLocationDetails> lst = null;
            try
            {
                string errorMessage = string.Empty;
                DataTable dataTableVendorsLoc = GetSelectedRecordsInDataTable(Common.ToXml(this), SP_VENDORLOC_SEARCH, ref errorMessage);

                if (dataTableVendorsLoc == null || dataTableVendorsLoc.Rows.Count <= 0)
                    return null;

                lst = new List<ItemVendorLocationDetails>();
                foreach (DataRow drow in dataTableVendorsLoc.Rows)
                {
                    ItemVendorLocationDetails ivld = new ItemVendorLocationDetails();
                    ivld.ItemId = Validators.CheckForDBNull(drow["ItemId"], Convert.ToInt32(Common.INT_DBNULL));
                    ivld.ItemCode = Validators.CheckForDBNull(drow["ItemCode"], String.Empty);
                    
                    ivld.VendorId = Validators.CheckForDBNull(drow["VendorId"], Convert.ToInt32(Common.INT_DBNULL));
                    ivld.VendorName = Validators.CheckForDBNull(drow["VendorName"], String.Empty);
                    ivld.LocationId = Validators.CheckForDBNull(drow["LocationId"], Convert.ToInt32(Common.INT_DBNULL));
                    ivld.LocationCode = Validators.CheckForDBNull(drow["LocationCode"], String.Empty);
                    ivld.LocationName = Validators.CheckForDBNull(drow["LocationName"], String.Empty);
                    ivld.LeadTime = Validators.CheckForDBNull(drow["LeadTime"], Convert.ToInt32(Common.INT_DBNULL));
                    ivld.MinOrderQuantity = Validators.CheckForDBNull(drow["MinOrderQty"], Convert.ToDecimal(Common.INT_DBNULL));
                    ivld.CostForLocation = Validators.CheckForDBNull(drow["LocationCost"], Convert.ToDecimal(Common.INT_DBNULL));
                    ivld.PurchaseUnitFactor = Validators.CheckForDBNull(drow["PUF"], Convert.ToDecimal(Common.INT_DBNULL));
                    ivld.ItemName = Validators.CheckForDBNull(drow["ItemName"], String.Empty);

                    ivld.IsVendorPrimaryForLocation = Validators.CheckForDBNull(
                                                                                !string.IsNullOrEmpty(drow["IsVendorPrimForLoc"].ToString()) ? (drow["IsVendorPrimForLoc"].ToString().ToLower() == "true" ? 1 : 0) : 0,
                                                                                Convert.ToInt32(Common.INT_DBNULL)
                                                                                );

                    ivld.Status = Validators.CheckForDBNull(
                                                            !string.IsNullOrEmpty(drow["Status"].ToString()) ? Int32.Parse(drow["Status"].ToString()) : -1, 
                                                            Convert.ToInt32(Common.INT_DBNULL)
                                                            );
                    ivld.StatusName = Validators.CheckForDBNull(drow["StatusName"], String.Empty);

                    ivld.ModifiedBy = Validators.CheckForDBNull(drow["ModifiedBy"], Convert.ToInt32(Common.INT_DBNULL));
                    ivld.ModifiedDate = Validators.CheckForDBNull(drow["ModifiedDate"], Common.DATETIME_NULL);
                    ivld.IsInclusiveofTax = Convert.ToInt32(drow["IsInclusiveofTax"]);
                    lst.Add(ivld);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<ItemVendor> SearchVendors(String itemCode)
        {
            List<ItemVendor> lst = null;
            DBParameterList dbParam;
            try
            {
                string errorMessage = string.Empty;

                Vinculum.Framework.Data.DataTaskManager dtManager = new Vinculum.Framework.Data.DataTaskManager();

                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@itemCode", itemCode, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataTable dt = dtManager.ExecuteDataTable(SP_ITEMVENDOR_LINK, dbParam);

                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (errorMessage.Length != 0)
                    return null;

                if (dt != null && dt.Rows.Count > 0)
                {
                    lst = new List<ItemVendor>();
                    foreach (DataRow drow in dt.Rows)
                    {
                        ItemVendor iv = new ItemVendor();
                        iv.VendorId = Validators.CheckForDBNull(drow["VendorId"], Convert.ToInt32(0));
                        iv.VendorName = Validators.CheckForDBNull(drow["VendorName"], String.Empty);

                        lst.Add(iv);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<LocationList> GetLocations()
        {
            List<LocationList> lst = null;
            DBParameterList dbParam;
            try
            {
                string errorMessage = string.Empty;

                Vinculum.Framework.Data.DataTaskManager dtManager = new Vinculum.Framework.Data.DataTaskManager();

                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataTable dt = dtManager.ExecuteDataTable(SP_LOCITEMVENDOR, dbParam);

                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (errorMessage.Length != 0)
                    return null;

                if (dt != null && dt.Rows.Count > 0)
                {
                    lst = new List<LocationList>();
                    foreach (DataRow drow in dt.Rows)
                    {
                        LocationList ll = new LocationList();
                        ll.LocationId = Validators.CheckForDBNull(drow["LocationId"], Convert.ToInt32(0));
                        ll.LocationName = Validators.CheckForDBNull(drow["LocationName"], String.Empty);

                        lst.Add(ll);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }
       
        public Boolean Save(ref String errorMessage)
        {
            
            try
            {
                Boolean isSuccess = false;
                isSuccess = base.Save(Common.ToXml(this), SP_VENDORLOC_SAVE, ref errorMessage);
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemVendorLocationDetails> GetItemsForVendorLocation()
        {
            try
            {
                List<ItemVendorLocationDetails> listItem = new List<ItemVendorLocationDetails>();
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@VendorId", this.VendorId, DbType.Int32));
                dbParam.Add(new DBParameter("@ItemCode",this.ItemCode, DbType.String));
                dbParam.Add(new DBParameter("@ItemName",this.ItemName, DbType.String));
                dbParam.Add(new DBParameter("@ItemId",this.ItemId, DbType.Int32));
                dbParam.Add(new DBParameter("@LocationId",this.LocationId, DbType.Int32));
                dbParam.Add(new DBParameter("@Status", this.Status, DbType.Int32));
                dbParam.Add(new DBParameter("@IsCompositeItem", this.IsCompositeItem, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
               
                using (DataTaskManager dt = new DataTaskManager())
                {
                    System.Data.DataTable dTable = new DataTable();
                    dTable = dt.ExecuteDataTable(SP_ITEMFORVENDORLOCATION, dbParam);
                    if (dbMessage == string.Empty && dTable != null && dTable.Rows.Count > 0)
                    {
                        foreach (DataRow drow in dTable.Rows)
                        {
                            ItemVendorLocationDetails ivld = new ItemVendorLocationDetails();
                            ivld.ItemId = Validators.CheckForDBNull(drow["ItemId"], Convert.ToInt32(Common.INT_DBNULL));
                            ivld.ItemCode = Validators.CheckForDBNull(drow["ItemCode"], String.Empty);

                            ivld.VendorId = Validators.CheckForDBNull(drow["VendorId"], Convert.ToInt32(Common.INT_DBNULL));
                            ivld.VendorName = Validators.CheckForDBNull(drow["VendorName"], String.Empty);
                            ivld.LocationId = Validators.CheckForDBNull(drow["LocationId"], Convert.ToInt32(Common.INT_DBNULL));
                            ivld.LocationCode = Validators.CheckForDBNull(drow["LocationCode"], String.Empty);
                            ivld.LocationName = Validators.CheckForDBNull(drow["LocationName"], String.Empty);
                            ivld.LeadTime = Validators.CheckForDBNull(drow["LeadTime"], Convert.ToInt32(Common.INT_DBNULL));
                            ivld.MinOrderQuantity = Validators.CheckForDBNull(drow["MinOrderQty"], Convert.ToDecimal(Common.INT_DBNULL));
                            ivld.CostForLocation = Validators.CheckForDBNull(drow["LocationCost"], Convert.ToDecimal(Common.INT_DBNULL));
                            ivld.PurchaseUnitFactor = Validators.CheckForDBNull(drow["PUF"], Convert.ToDecimal(Common.INT_DBNULL));
                            ivld.ItemName = Validators.CheckForDBNull(drow["ItemName"], String.Empty);
                            
                            ivld.IsVendorPrimaryForLocation = Validators.CheckForDBNull(
                                                                                        !string.IsNullOrEmpty(drow["IsVendorPrimForLoc"].ToString()) ? (drow["IsVendorPrimForLoc"].ToString().ToLower() == "true" ? 1 : 0) : 0,
                                                                                        Convert.ToInt32(Common.INT_DBNULL)
                                                                                        );

                            ivld.Status = Validators.CheckForDBNull(
                                                                    !string.IsNullOrEmpty(drow["Status"].ToString()) ? Int32.Parse(drow["Status"].ToString()) : -1,
                                                                    Convert.ToInt32(Common.INT_DBNULL)
                                                                    );
                            ivld.StatusName = Validators.CheckForDBNull(drow["StatusName"], String.Empty);

                            ivld.ModifiedBy = Validators.CheckForDBNull(drow["ModifiedBy"], Convert.ToInt32(Common.INT_DBNULL));
                            ivld.ModifiedDate = Validators.CheckForDBNull(drow["ModifiedDate"], Common.DATETIME_NULL);
                            ivld.IsCompositeItem= Convert.ToInt32(drow["IsComposite"]);
                            ivld.IsInclusiveofTax = Convert.ToInt32(drow["IsInclusiveofTax"]);
                            listItem.Add(ivld);
                        }
                    }
                }
                return listItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
    }
}
