using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using System.Data;
using Vinculum.Framework.Data;
using Vinculum.Framework;
using Vinculum.Framework.DataTypes;
namespace CoreComponent.MasterData.BusinessObjects
{
    [Serializable]
    public class ItemVendor : ItemMaster, IVendor
    {
        private Int32 m_vendorId = Common.INT_DBNULL;
        private String m_vendorName = String.Empty;

        public Int32 VendorId
        {
            get { return m_vendorId; }
            set { m_vendorId = value; }
        }

        public String VendorName
        {
            get { return m_vendorName; }
            set { m_vendorName = value; }
        }
    }

    [Serializable]
    public class ItemVendorDetails : ItemVendor
    {
        private const String SP_VENDOR_SEARCH = "usp_ItemVendorSearch";
        private const String SP_VENDOR_SAVE = "usp_ItemVendorSave";

        public static readonly String CMB_VENDOR_VALUE = "VendorId";
        public static readonly String CMB_VENDOR_TEXT = "VendorName";

        private Int32 m_primaryVendor = Common.INT_DBNULL;
        private Decimal m_itemCost = Common.INT_DBNULL;
        private List<ItemVendorDetails> m_vendorList = null;

        public Decimal ItemCostByVendor
        {
            get { return m_itemCost; }
            set { m_itemCost = value; }
        }

        public Int32 VendorStatus
        {
            get;
            set;
        }


        public Int32 IsVendorPrimary
        {
            get { return m_primaryVendor; }
            set { m_primaryVendor = value; }
        }

        public List<ItemVendorDetails> ListOfVendors
        {
            get { return m_vendorList; }
            set { m_vendorList = value; }
        }

        public List<ItemVendorDetails> Search()
        {
            List<ItemVendorDetails> lst = null;
            try
            {
                string errorMessage = string.Empty;
                DataTable dataTableVendors = GetSelectedRecordsInDataTable(Common.ToXml(this), SP_VENDOR_SEARCH, ref errorMessage);

                if (dataTableVendors == null || dataTableVendors.Rows.Count <= 0)
                    return null;

                lst = new List<ItemVendorDetails>();
                foreach (DataRow drow in dataTableVendors.Rows)
                {
                    ItemVendorDetails ivd = new ItemVendorDetails();
                    ivd.ItemId = Validators.CheckForDBNull(drow["ItemId"], Convert.ToInt32(Common.INT_DBNULL));
                    ivd.ItemCode = Validators.CheckForDBNull(drow["ItemCode"], string.Empty);
                    ivd.ItemName = Validators.CheckForDBNull(drow["ItemName"], string.Empty);

                    ivd.VendorId = Validators.CheckForDBNull(drow["VendorId"], Convert.ToInt32(Common.INT_DBNULL));
                    ivd.VendorName = Validators.CheckForDBNull(drow["VendorName"], string.Empty);

                    ivd.IsVendorPrimary = Validators.CheckForDBNull(
                                                                    !string.IsNullOrEmpty(drow["IsVendorPrim"].ToString()) ? (drow["IsVendorPrim"].ToString().ToLower() == "true" ? 1 : 0) : 0, 
                                                                    Convert.ToInt32(Common.INT_DBNULL)
                                                                    );

                    ivd.Status = Validators.CheckForDBNull(
                                                            !string.IsNullOrEmpty(drow["Status"].ToString()) ? Int32.Parse(drow["Status"].ToString()) : -1,
                                                            Convert.ToInt32(Common.INT_DBNULL)
                                                            );
                    ivd.StatusName = Validators.CheckForDBNull(drow["StatusName"], string.Empty);

                    ivd.ItemCostByVendor = Validators.CheckForDBNull(drow["CostByVendor"], Convert.ToDecimal(Common.INT_DBNULL));

                    ivd.ModifiedDate = Validators.CheckForDBNull(drow["ModifiedDate"], Common.DATETIME_NULL);
                    ivd.VendorStatus = Validators.CheckForDBNull(drow["VendorStatus"], Common.INT_DBNULL);

                    lst.Add(ivd);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return lst;
        }

        public Boolean Save(ref String errorMessage)
        {
            try
            {             
                DBParameterList dbParam;
                bool isSuccess = false;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    dtManager.BeginTransaction();
                    {
                        try
                        {
                            dbParam = new DBParameterList();
                            dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(this), DbType.String));
                            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                            DataTable dt = dtManager.ExecuteDataTable(SP_VENDOR_SAVE, dbParam);

                            errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                            {
                                if (errorMessage.Length > 0)
                                {
                                    isSuccess = false;
                                    dtManager.RollbackTransaction();
                                }
                                else
                                {
                                    dtManager.CommitTransaction();
                                    isSuccess = true;
                                }
                            }
                            return isSuccess;
                        }
                        catch (Exception ex)
                        {
                            dtManager.RollbackTransaction();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean SaveList(List<ItemVendorDetails> itemVendorList, ref String errorMessage)
        {
            try
            {
                DBParameterList dbParam;
                bool isSuccess = false;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    dtManager.BeginTransaction();
                    foreach (ItemVendorDetails vendor in itemVendorList)
                    {
                        try
                        {
                            dbParam = new DBParameterList();
                            dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(vendor), DbType.String));
                            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                            DataTable dt = dtManager.ExecuteDataTable(SP_VENDOR_SAVE, dbParam);

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
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (dtManager != null)
                            {
                                dtManager.RollbackTransaction();
                                isSuccess = false;
                            }
                            throw ex;
                        }
                    }                    
                    if (isSuccess)
                    {
                        dtManager.CommitTransaction();
                    }
                }
                return isSuccess;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
