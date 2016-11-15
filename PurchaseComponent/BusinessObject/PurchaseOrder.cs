using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using System.Data;

namespace PurchaseComponent.BusinessObjects
{
    [Serializable]
    public class PurchaseOrder : IPurchaserOrder
    {
        #region DATA Field Constants
        private const string CON_FIELD_PONUMBER = "PONumber";
        private const string CON_FIELD_AMENDMENTNO = "AmendmentNo";
        private const string CON_FIELD_POTYPE = "POType";
        private const string CON_FIELD_VENDORID = "VendorId";
        private const string CON_FIELD_VENDORCODE = "VendorCode";
        private const string CON_FIELD_VENDORNAME = "VendorName";
        private const string CON_FIELD_PAYMENTTERMS = "PaymentTerms";
        private const string CON_FIELD_PODATE = "PODate";
        private const string CON_FIELD_EXPECTEDDELIVERYDATE = "ExpectedDeliveryDate";
        private const string CON_FIELD_MAXDELIVERYDATE = "MaxDeliveryDate";
        private const string CON_FIELD_STATUS = "Status";
        private const string CON_FIELD_REMARKS = "Remarks";
        private const string CON_FIELD_SHIPPINGDETAILS = "ShippingDetails";
        private const string CON_FIELD_PAYMENTDETAILS = "PaymentDetails";
        private const string CON_FIELD_TOTALTAXAMOUNT = "TotalTaxAmount";
        private const string CON_FIELD_TOTALPOAMOUNT = "TotalPOAmount";
        private const string CON_FIELD_ISFORMCAPPLICABLE = "IsFormCApplicable";
        private const string CON_FIELD_CREATEDBY = "CreatedBy";
        private const string CON_FIELD_CREATEDDATE = "CreatedDate";
        private const string CON_FIELD_MODIFIEDBY = "ModifiedBy";
        private const string CON_FIELD_MODIFIEDDATE = "ModifiedDate";
        private const string CON_FIELD_LOCATIONNAME = "LocationName";

        private const string CON_FIELD_V_ADDRESS1 = "V_Address1";
        private const string CON_FIELD_V_ADDRESS2 = "V_Address2";
        private const string CON_FIELD_V_ADDRESS3 = "V_Address3";
        private const string CON_FIELD_V_ADDRESS4 = "V_Address4";
        private const string CON_FIELD_V_CITY = "V_City";
        private const string CON_FIELD_V_CITYNAME = "V_CityName";
        private const string CON_FIELD_V_STATE = "V_State";
        private const string CON_FIELD_V_STATENAME = "V_StateName";
        private const string CON_FIELD_V_COUNTRY = "V_Country";
        private const string CON_FIELD_V_COUNTRYNAME = "V_CountryName";
        private const string CON_FIELD_V_PINCODE = "V_PinCode";
        private const string CON_FIELD_V_PHONE1 = "V_Phone1";
        private const string CON_FIELD_V_PHONE2 = "V_Phone2";
        private const string CON_FIELD_V_MOBILE1 = "V_Mobile1";
        private const string CON_FIELD_V_MOBILE2 = "V_Mobile2";
        private const string CON_FIELD_V_FAX1 = "V_Fax1";
        private const string CON_FIELD_V_FAX2 = "V_Fax2";
        private const string CON_FIELD_V_EMAIL1 = "V_Email1";
        private const string CON_FIELD_V_EMAIL2 = "V_Email2";
        private const string CON_FIELD_V_WEBSITE = "V_WebSite";

        private const string CON_FIELD_D_LocationID = "D_LocationId";
        private const string CON_FIELD_D_ADDRESS1 = "D_Address1";
        private const string CON_FIELD_D_ADDRESS2 = "D_Address2";
        private const string CON_FIELD_D_ADDRESS3 = "D_Address3";
        private const string CON_FIELD_D_ADDRESS4 = "D_Address4";
        private const string CON_FIELD_D_CITY = "D_City";
        private const string CON_FIELD_D_CITYNAME = "D_CityName";
        private const string CON_FIELD_D_STATE = "D_State";
        private const string CON_FIELD_D_STATENAME = "D_StateName";
        private const string CON_FIELD_D_COUNTRY = "D_Country";
        private const string CON_FIELD_D_COUNTRYNAME = "D_CountryName";
        private const string CON_FIELD_D_PINCODE = "D_PinCode";
        private const string CON_FIELD_D_PHONE1 = "D_Phone1";
        private const string CON_FIELD_D_PHONE2 = "D_Phone2";
        private const string CON_FIELD_D_MOBILE1 = "D_Mobile1";
        private const string CON_FIELD_D_MOBILE2 = "D_Mobile2";
        private const string CON_FIELD_D_FAX1 = "D_Fax1";
        private const string CON_FIELD_D_FAX2 = "D_Fax2";
        private const string CON_FIELD_D_EMAIL1 = "D_Email1";
        private const string CON_FIELD_D_EMAIL2 = "D_Email2";
        private const string CON_FIELD_D_WEBSITE = "D_Website";
        private const string CON_FIELD_D_LocationCode = "LocationCode";

        private const string CON_FIELD_STATUSNAME = "StatusName";
        private const string CON_FIELD_POTYPENAME = "POTypeName";

        #endregion

        #region SP Declaration
        private const string SP_PO_SAVE = "usp_POSave";
        private const string SP_PO_SEARCH = "usp_POSearch";
        private const string SP_PO_GRN_SEARCH = "usp_POGRNSearch";
        private const string SP_PODETAIL_SEARCH = "usp_PODetailSearch";

        #endregion

        #region Properties
        private DateTime m_fromDate = Common.DATETIME_NULL;
        public DateTime FromDate
        {
            get { return m_fromDate; }
            set { m_fromDate = value; }
        }

        private DateTime m_toDate = Common.DATETIME_NULL;
        public DateTime ToDate
        {
            get { return m_toDate; }
            set { m_toDate = value; }
        }

        private string m_LocationName = string.Empty;
        public string LocationName
        {
            get { return m_LocationName; }
            set { m_LocationName = value; }
        }


        private string m_poNumber = string.Empty;
        public string PONumber
        {
            get { return m_poNumber; }
            set { m_poNumber = value; }
        }

        private int m_amendmentNo = 0;
        public int AmendmentNo
        {
            get { return m_amendmentNo; }
            set { m_amendmentNo = value; }
        }

        private int m_poType = Common.INT_DBNULL;
        public int POType
        {
            get { return m_poType; }
            set { m_poType = value; }
        }

        private string m_poTypeName = string.Empty;

        public string POTypeName
        {
            get { return m_poTypeName; }
            set { m_poTypeName = value; }
        }

        private int m_locationId = Common.INT_DBNULL;
        public int DestinationLocationID
        {
            get { return m_locationId; }
            set { m_locationId = value; }
        }

        private string m_locationCode = string.Empty;
        public string DestinationLocationCode
        {
            get { return m_locationCode; }
            set { m_locationCode = value; }
        }
        private int m_vendorId = Common.INT_DBNULL;
        public int VendorID
        {
            get { return m_vendorId; }
            set { m_vendorId = value; }
        }

        private string m_vendorCode = string.Empty;
        public string VendorCode
        {
            get { return m_vendorCode; }
            set { m_vendorCode = value; }
        }

        private string m_vendorName = string.Empty;
        public string VendorName
        {
            get { return m_vendorName; }
            set { m_vendorName = value; }
        }

        private Address m_vendorAddress = new Address();
        public Address VendorAddress
        {
            get { return m_vendorAddress; }
            set { m_vendorAddress = value; }
        }

        private string m_paymentTerms = string.Empty;
        public string PaymentTerms
        {
            get { return m_paymentTerms; }
            set { m_paymentTerms = value; }
        }

        private Address m_deliveryAddress = new Address();
        public Address DeliveryAddress
        {
            get { return m_deliveryAddress; }
            set { m_deliveryAddress = value; }
        }

        private DateTime m_poDate = Common.DATETIME_NULL;
        public DateTime PODate
        {
            get { return m_poDate; }
            set { m_poDate = value; }
        }
        public string DisplayPODate
        {
            get { return PODate.ToString(Common.DTP_DATE_FORMAT); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        private DateTime m_expectedDeliveryDate = Common.DATETIME_CURRENT;
        public DateTime ExpectedDeliveryDate
        {
            get { return m_expectedDeliveryDate; }
            set { m_expectedDeliveryDate = value; }
        }

        private DateTime m_maxDeliveryDate = Common.DATETIME_CURRENT;
        public DateTime MaxDeliveryDate
        {
            get { return m_maxDeliveryDate; }
            set { m_maxDeliveryDate = value; }
        }

        private int m_status = Common.INT_DBNULL;
        public int Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        private string m_statusName = string.Empty;

        public string StatusName
        {
            get { return m_statusName; }
            set { m_statusName = value; }
        }

        private string m_remarks = string.Empty;
        public string Remarks
        {
            get { return m_remarks; }
            set { m_remarks = value; }
        }

        private string m_shippingDetails = string.Empty;
        public string ShippingDetails
        {
            get { return m_shippingDetails; }
            set { m_shippingDetails = value; }
        }

        private string m_paymentDetails = string.Empty;
        public string PaymentDetails
        {
            get { return m_paymentDetails; }
            set { m_paymentDetails = value; }
        }

        private Decimal m_totalTaxAmount = 0;
        public Decimal TotalTaxAmount
        {
            get { return m_totalTaxAmount; }
            set { m_totalTaxAmount = value; }
        }

        public Decimal DisplayTotalTaxAmount
        {
            get { return Math.Round(DBTotalTaxAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public Decimal DBTotalTaxAmount
        {
            get { return Math.Round(TotalTaxAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        private Decimal m_totalPoAmount = 0;
        public Decimal TotalPOAmount
        {
            get { return m_totalPoAmount; }
            set { m_totalPoAmount = value; }
        }

        public Decimal DisplayTotalPOAmount
        {
            get { return Math.Round(DBTotalPOAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public Decimal DBTotalPOAmount
        {
            get { return Math.Round(TotalPOAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        private bool m_isFormCApplicable = false;
        public bool IsFormCApplicable
        {
            get { return m_isFormCApplicable; }
            set { m_isFormCApplicable = value; }
        }
        public string DisplayCreateDate
        {
            get { return CreatedDate.ToString(Common.DTP_DATE_FORMAT); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
        private List<PurchaseOrderDetail> m_purchaseOrderDetail;
        public List<PurchaseOrderDetail> PurchaseOrderDetail
        {
            get { return m_purchaseOrderDetail; }
            set { m_purchaseOrderDetail = value; }
        }

        public int TaxJurisdictionId
        {
            get;
            set;
        }



        #endregion

        #region IPurchaserOrder Members

        private int m_createdBy = 0;
        public int CreatedBy
        {
            get
            {
                return m_createdBy;
            }
            set
            {
                m_createdBy = value;
            }
        }

        private DateTime m_createdDate = DateTime.Today;
        public DateTime CreatedDate
        {
            get
            {
                return m_createdDate;
            }
            set
            {
                m_createdDate = value;
            }
        }

        private int m_modifiedBy = 0;
        public int ModifiedBy
        {
            get
            {
                return m_modifiedBy;
            }
            set
            {
                m_modifiedBy = value;
            }
        }

        private DateTime m_modifiedDate = Common.DATETIME_NULL;
        public DateTime ModifiedDate
        {
            get
            {
                return m_modifiedDate;
            }
            set
            {
                m_modifiedDate = value;
            }
        }


        string IPurchaserOrder.Save(ref string errorMessage)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region  Methods

        public List<PurchaseOrderDetail> SearchDetails()
        {
            List<PurchaseOrderDetail> purchaseOrderList = new List<PurchaseOrderDetail>();
            System.Data.DataTable dTable = new DataTable();
            try
            {
                string errorMessage = string.Empty;
                
                dTable = GetSelectedRecords(Common.ToXml(this), SP_PODETAIL_SEARCH, ref errorMessage);

                if (dTable != null)
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        PurchaseOrderDetail _purchase = new PurchaseOrderDetail(this.TaxJurisdictionId, this.DeliveryAddress.StateId);
                        _purchase.CreatePODetailObject(drow);
                        _purchase.PurchaseOrderTaxDetail = _purchase.GetTaxDetail();
                        purchaseOrderList.Add(_purchase);
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return purchaseOrderList;
        }
       
        public DataTable SearchDetailsDataTable()
        {
            System.Data.DataTable dTable = new DataTable();
            try
            {
                string errorMessage = string.Empty;
                dTable = GetSelectedRecords(Common.ToXml(this), SP_PODETAIL_SEARCH, ref errorMessage);

                if (errorMessage != String.Empty)
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dTable;
        }

        public DataTable SearchFirstAmendmentQty(string poNumber, int itemId)
        {

            return null;
        }

        public bool Save(ref string errorMessage, bool amend)
        {
            try
            {
                DBParameterList dbParam;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    try
                    {
                        dtManager.BeginTransaction();                       
                            string xmlDoc = Common.ToXml(this);
                            dbParam = new DBParameterList();
                            dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                            dbParam.Add(new DBParameter("@amend", amend, DbType.Boolean));
                            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                            DataTable dt = dtManager.ExecuteDataTable(SP_PO_SAVE, dbParam);

                            errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                            if (errorMessage.Length > 0)
                            {
                                dtManager.RollbackTransaction();
                                return false;
                            }
                            else
                            {                             
                                this.PONumber = Convert.ToString(dt.Rows[0]["PoNo"]);
                                dtManager.CommitTransaction();                               
                                return true;
                            }                            
                        
                    }
                    catch (Exception ex)
                    {
                        dtManager.RollbackTransaction();
                        throw ex;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
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

        public virtual DataSet GetSelectedRecordsDataSet(string xmlDoc, string spName, ref string errorMessage)
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

        public List<PurchaseOrder> Search()
        {
            List<PurchaseOrder> purchaseOrderList = new List<PurchaseOrder>();
            System.Data.DataTable dTable = new DataTable();
            try
            {
                string errorMessage = string.Empty;
                dTable = GetSelectedRecords(Common.ToXml(this), SP_PO_SEARCH, ref errorMessage);

                if (dTable != null)
                {
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        PurchaseOrder _purchase = CreatePOObject(drow);
                        purchaseOrderList.Add(_purchase);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return purchaseOrderList;
        }

        private PurchaseOrder CreatePOObject(System.Data.DataRow drow)
        {
            try
            {
                PurchaseOrder m_po = new PurchaseOrder();
                if (drow.Table.Columns.Contains(CON_FIELD_AMENDMENTNO))
                    m_po.AmendmentNo = Convert.ToInt32(drow[CON_FIELD_AMENDMENTNO]);

                if (drow.Table.Columns.Contains(CON_FIELD_CREATEDBY))
                    m_po.CreatedBy = Convert.ToInt32(drow[CON_FIELD_CREATEDBY]);

                if (drow.Table.Columns.Contains(CON_FIELD_CREATEDDATE))
                    m_po.CreatedDate = Convert.ToDateTime(drow[CON_FIELD_CREATEDDATE]);

                if (drow.Table.Columns.Contains(CON_FIELD_EXPECTEDDELIVERYDATE))
                    m_po.ExpectedDeliveryDate = Convert.ToDateTime(drow[CON_FIELD_EXPECTEDDELIVERYDATE]);

                if (drow.Table.Columns.Contains(CON_FIELD_ISFORMCAPPLICABLE))
                    m_po.IsFormCApplicable = Convert.ToBoolean(drow[CON_FIELD_ISFORMCAPPLICABLE]);

                if (drow.Table.Columns.Contains(CON_FIELD_MAXDELIVERYDATE))
                    m_po.MaxDeliveryDate = Convert.ToDateTime(drow[CON_FIELD_MAXDELIVERYDATE]);

                if (drow.Table.Columns.Contains(CON_FIELD_MODIFIEDBY))
                    m_po.ModifiedBy = Convert.ToInt32(drow[CON_FIELD_MODIFIEDBY]);

                if (drow.Table.Columns.Contains(CON_FIELD_MODIFIEDDATE))
                    m_po.ModifiedDate = Convert.ToDateTime(drow[CON_FIELD_MODIFIEDDATE]);

                if (drow.Table.Columns.Contains(CON_FIELD_PAYMENTDETAILS))
                    m_po.PaymentDetails = Convert.ToString(drow[CON_FIELD_PAYMENTDETAILS]);

                if (drow.Table.Columns.Contains(CON_FIELD_PAYMENTTERMS))
                    m_po.PaymentTerms = Convert.ToString(drow[CON_FIELD_PAYMENTTERMS]);

                //if (drow.Table.Columns.Contains(CON_FIELD_POCREATIONDATE))
                //    m_po.POCreationDate=Convert.ToDateTime(drow[CON_FIELD_POCREATIONDATE]);

                if (drow.Table.Columns.Contains(CON_FIELD_PODATE))
                    m_po.PODate = Convert.ToDateTime(drow[CON_FIELD_PODATE]);

                if (drow.Table.Columns.Contains(CON_FIELD_REMARKS))
                    m_po.Remarks = Convert.ToString(drow[CON_FIELD_REMARKS]);

                if (drow.Table.Columns.Contains(CON_FIELD_PONUMBER))
                    m_po.PONumber = Convert.ToString(drow[CON_FIELD_PONUMBER]);

                if (drow.Table.Columns.Contains(CON_FIELD_POTYPE))
                    m_po.POType = Convert.ToInt32(drow[CON_FIELD_POTYPE]);

                if (drow.Table.Columns.Contains(CON_FIELD_SHIPPINGDETAILS))
                    m_po.ShippingDetails = Convert.ToString(drow[CON_FIELD_SHIPPINGDETAILS]);

                if (drow.Table.Columns.Contains(CON_FIELD_STATUS))
                    m_po.Status = Convert.ToInt32(drow[CON_FIELD_STATUS]);

                if (drow.Table.Columns.Contains(CON_FIELD_TOTALPOAMOUNT))
                    m_po.TotalPOAmount = Convert.ToDecimal(drow[CON_FIELD_TOTALPOAMOUNT]);

                if (drow.Table.Columns.Contains(CON_FIELD_TOTALTAXAMOUNT))
                    m_po.TotalTaxAmount = Convert.ToDecimal(drow[CON_FIELD_TOTALTAXAMOUNT]);

                if (drow.Table.Columns.Contains(CON_FIELD_VENDORCODE))
                    m_po.VendorCode = Convert.ToString(drow[CON_FIELD_VENDORCODE]);

                if (drow.Table.Columns.Contains(CON_FIELD_VENDORID))
                    m_po.VendorID = Convert.ToInt32(drow[CON_FIELD_VENDORID]);

                if (drow.Table.Columns.Contains(CON_FIELD_VENDORNAME))
                    m_po.VendorName = Convert.ToString(drow[CON_FIELD_VENDORNAME]);

                if (drow.Table.Columns.Contains(CON_FIELD_STATUSNAME))
                    m_po.StatusName = Convert.ToString(drow[CON_FIELD_STATUSNAME]);

                if (drow.Table.Columns.Contains(CON_FIELD_POTYPENAME))
                    m_po.POTypeName = Convert.ToString(drow[CON_FIELD_POTYPENAME]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_ADDRESS1))
                    m_po.VendorAddress.Address1 = Convert.ToString(drow[CON_FIELD_V_ADDRESS1]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_ADDRESS2))
                    m_po.VendorAddress.Address2 = Convert.ToString(drow[CON_FIELD_V_ADDRESS2]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_ADDRESS3))
                    m_po.VendorAddress.Address3 = Convert.ToString(drow[CON_FIELD_V_ADDRESS3]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_ADDRESS4))
                    m_po.VendorAddress.Address4 = Convert.ToString(drow[CON_FIELD_V_ADDRESS4]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_CITYNAME))
                    m_po.VendorAddress.City = Convert.ToString(drow[CON_FIELD_V_CITYNAME]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_CITY))
                    m_po.VendorAddress.CityId = Convert.ToInt32(drow[CON_FIELD_V_CITY]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_COUNTRYNAME))
                    m_po.VendorAddress.Country = Convert.ToString(drow[CON_FIELD_V_COUNTRYNAME]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_COUNTRY))
                    m_po.VendorAddress.CountryId = Convert.ToInt32(drow[CON_FIELD_V_COUNTRY]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_EMAIL1))
                    m_po.VendorAddress.Email1 = Convert.ToString(drow[CON_FIELD_V_EMAIL1]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_EMAIL2))
                    m_po.VendorAddress.Email2 = Convert.ToString(drow[CON_FIELD_V_EMAIL2]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_FAX1))
                    m_po.VendorAddress.Fax1 = Convert.ToString(drow[CON_FIELD_V_FAX1]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_FAX2))
                    m_po.VendorAddress.Fax2 = Convert.ToString(drow[CON_FIELD_V_FAX2]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_MOBILE1))
                    m_po.VendorAddress.Mobile1 = Convert.ToString(drow[CON_FIELD_V_MOBILE1]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_MOBILE2))
                    m_po.VendorAddress.Mobile2 = Convert.ToString(drow[CON_FIELD_V_MOBILE2]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_PHONE1))
                    m_po.VendorAddress.PhoneNumber1 = Convert.ToString(drow[CON_FIELD_V_PHONE1]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_PHONE2))
                    m_po.VendorAddress.PhoneNumber2 = Convert.ToString(drow[CON_FIELD_V_PHONE2]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_PINCODE))
                    m_po.VendorAddress.PinCode = Convert.ToString(drow[CON_FIELD_V_PINCODE]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_STATENAME))
                    m_po.VendorAddress.State = Convert.ToString(drow[CON_FIELD_V_STATENAME]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_STATE))
                    m_po.VendorAddress.StateId = Convert.ToInt32(drow[CON_FIELD_V_STATE]);

                if (drow.Table.Columns.Contains(CON_FIELD_V_WEBSITE))
                    m_po.VendorAddress.Website = Convert.ToString(drow[CON_FIELD_V_WEBSITE]);

                if (drow.Table.Columns.Contains(CON_FIELD_D_LocationID))
                    m_po.DestinationLocationID = Convert.ToInt32(drow[CON_FIELD_D_LocationID]);

                if (drow.Table.Columns.Contains(CON_FIELD_D_LocationCode))
                    m_po.DestinationLocationCode = Convert.ToString(drow[CON_FIELD_D_LocationCode]);

                if (drow.Table.Columns.Contains(CON_FIELD_D_ADDRESS1))
                    m_po.DeliveryAddress.Address1 = Convert.ToString(drow[CON_FIELD_D_ADDRESS1]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_ADDRESS2))
                    m_po.DeliveryAddress.Address2 = Convert.ToString(drow[CON_FIELD_D_ADDRESS2]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_ADDRESS3))
                    m_po.DeliveryAddress.Address3 = Convert.ToString(drow[CON_FIELD_D_ADDRESS3]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_ADDRESS4))
                    m_po.DeliveryAddress.Address4 = Convert.ToString(drow[CON_FIELD_D_ADDRESS4]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_CITYNAME))
                    m_po.DeliveryAddress.City = Convert.ToString(drow[CON_FIELD_D_CITYNAME]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_CITY))
                    m_po.DeliveryAddress.CityId = Convert.ToInt32(drow[CON_FIELD_D_CITY]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_COUNTRYNAME))
                    m_po.DeliveryAddress.Country = Convert.ToString(drow[CON_FIELD_D_COUNTRYNAME]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_COUNTRY))
                    m_po.DeliveryAddress.CountryId = Convert.ToInt32(drow[CON_FIELD_D_COUNTRY]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_EMAIL1))
                    m_po.DeliveryAddress.Email1 = Convert.ToString(drow[CON_FIELD_D_EMAIL1]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_EMAIL2))
                    m_po.DeliveryAddress.Email2 = Convert.ToString(drow[CON_FIELD_D_EMAIL2]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_FAX1))
                    m_po.DeliveryAddress.Fax1 = Convert.ToString(drow[CON_FIELD_D_FAX1]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_FAX2))
                    m_po.DeliveryAddress.Fax2 = Convert.ToString(drow[CON_FIELD_D_FAX2]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_MOBILE1))
                    m_po.DeliveryAddress.Mobile1 = Convert.ToString(drow[CON_FIELD_D_MOBILE1]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_MOBILE2))
                    m_po.DeliveryAddress.Mobile2 = Convert.ToString(drow[CON_FIELD_D_MOBILE2]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_PHONE1))
                    m_po.DeliveryAddress.PhoneNumber1 = Convert.ToString(drow[CON_FIELD_D_PHONE1]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_PHONE2))
                    m_po.DeliveryAddress.PhoneNumber2 = Convert.ToString(drow[CON_FIELD_D_PHONE2]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_PINCODE))
                    m_po.DeliveryAddress.PinCode = Convert.ToString(drow[CON_FIELD_D_PINCODE]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_STATENAME))
                    m_po.DeliveryAddress.State = Convert.ToString(drow[CON_FIELD_D_STATENAME]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_STATE))
                    m_po.DeliveryAddress.StateId = Convert.ToInt32(drow[CON_FIELD_D_STATE]);
                if (drow.Table.Columns.Contains(CON_FIELD_D_WEBSITE))
                    m_po.DeliveryAddress.Website = Convert.ToString(drow[CON_FIELD_D_WEBSITE]);

                if (drow.Table.Columns.Contains(CON_FIELD_LOCATIONNAME))
                    m_po.LocationName = Convert.ToString(drow[CON_FIELD_LOCATIONNAME]);

                return m_po;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<PurchaseOrder> SearchPOForGRN()
        {
            List<PurchaseOrder> purchaseOrderList = new List<PurchaseOrder>();
            System.Data.DataTable dTable = new DataTable();
            try
            {
                string errorMessage = string.Empty;
                dTable = GetSelectedRecords(Common.ToXml(this), SP_PO_GRN_SEARCH, ref errorMessage);

                if (dTable != null)
                {
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        PurchaseOrder _purchase = CreatePOObject(drow);
                        purchaseOrderList.Add(_purchase);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return purchaseOrderList;
        }

        public void SetTotalAmount()
        {
            try
            {
                decimal m_total = 0; decimal m_tax = 0;
                if (PurchaseOrderDetail != null && PurchaseOrderDetail.Count > 0)
                {
                    foreach (PurchaseOrderDetail pur in PurchaseOrderDetail)
                    {
                        m_tax = m_tax + pur.LineTaxAmount;
                        m_total = m_total + pur.LineAmount;
                    }
                }
                //m_total = m_total + m_tax;
                TotalPOAmount = m_total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetTotalTax()
        {
            try
            {
                decimal m_tax = 0;
                if (PurchaseOrderDetail != null && PurchaseOrderDetail.Count > 0)
                {
                    foreach (PurchaseOrderDetail pur in PurchaseOrderDetail)
                    {
                        m_tax = m_tax + pur.LineTaxAmount;
                    }
                }
                TotalTaxAmount = m_tax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
