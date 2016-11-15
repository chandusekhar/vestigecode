using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using System.Data;
using System.ComponentModel;
using System.Reflection;
namespace PurchaseComponent.BusinessObjects
{
    [Serializable]
    public class Grn:IGrn
    {
        #region SP_Declaration
        private const string SP_GRN_SAVE = "usp_GRNSave";
        private const string SP_GRN_SEARCH = "usp_GRNSearch";
    
        #endregion

        #region FieldName
        private const string CON_FIELD_GRNNO    = "GRNNo";
        private const string CON_FIELD_PONUMBER = "PONumber";
        private const string CON_FIELD_AMENDMENTNO = "AmendmentNo";
        private const string CON_FIELD_GRNDATE = "GRNDate";
        
        private const string CON_FIELD_PODATE = "PODate";
        private const string CON_FIELD_CHALLANNO = "ChallanNo";
        private const string CON_FIELD_CHALLANDATE = "ChallanDate";
        private const string CON_FIELD_GROSSWEIGHT = "GrossWeight";
        private const string CON_FIELD_NOOFBOXES = "NoOfBoxes";
        private const string CON_FIELD_INVOICENO = "InvoiceNo";
        private const string CON_FIELD_INVOICEDATE = "InvoiceDate";
        private const string CON_FIELD_INVOICETAX = "InvoiceTaxAmount";
        private const string CON_FIELD_INVOICEAMOUNT = "InvoiceAmount";
        private const string CON_FIELD_SHIPPINGDETAILS = "ShippingDetails";
        private const string CON_FIELD_RECEIVEDBY = "ReceivedBy";
        private const string CON_FIELD_STATUS= "Status";
        private const string CON_FIELD_STATUSNAME = "StatusName";
        private const string CON_FIELD_VEHICLENO = "VehicleNo";
        private const string CON_FIELD_VENDORCODE = "VendorCode";
        private const string CON_FIELD_VENDORNAME = "VendorName";
        private const string CON_FIELD_LOCATIONCODE = "LocationCode";
        
        private const string CON_FIELD_CREATEDBY = "CreatedBy";

		private const string CON_FIELD_CREATEDDATE = "CreatedDate";
        private const string CON_FIELD_MODIFYBY = "ModifiedBy";
        private const string CON_FIELD_MODIFYDATE = "ModifiedDate";

        // Location fields
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

        #endregion

        #region Property

        public int VendorID
        {
            get;
            set;
        }

        public int ToStateId
        {
            set;
            get;
        }

        public int FromStateId
        {
            set;
            get;
        }

        private string m_GrnNo = string.Empty;
        public string GRNNo
        {
            get { return m_GrnNo; }
            set { m_GrnNo = value; }
        }

        private string m_GRNDate = Common.DATETIME_CURRENT.ToString();
        public string GRNDate
        {
            get { return m_GRNDate; }
            set { m_GRNDate = value; }
        }

        public string DisplayGRNDate
        {
            get { return Convert.ToDateTime(GRNDate).ToString(Common.DTP_DATE_FORMAT); }
            set { GRNDate = value; }
        }

        private string m_PONumber = string.Empty;
        public string PONumber
        {
            get { return m_PONumber; }
            set { m_PONumber = value; }
        }

        private string m_PODate = Common.DATETIME_NULL.ToString();
        public string PODate
        {
            get { return m_PODate; }
            set { m_PODate = value; }
        }

        public string DisplayPODate
        {
            get { return Convert.ToDateTime(PODate).ToString(Common.DTP_DATE_FORMAT); }
            set { PODate = value; }
        }

        private int m_AmendmentNo = 0;
        public int AmendmentNo
        {
            get { return m_AmendmentNo; }
            set { m_AmendmentNo = value; }
        }
        
        private string m_ChallanNo = string.Empty;
        public string ChallanNo
        {
            get { return m_ChallanNo; }
            set { m_ChallanNo = value; }
        }

        private string m_challanDate = DateTime.Today.ToString();
        public string ChallanDate
        {
            get { return m_challanDate; }
            set { m_challanDate = value; }
        }
                

        private string m_grossWeight = string.Empty;
        public string GrossWeight
        {
            get { return m_grossWeight; }
            set { m_grossWeight = value; }
        }

        private int m_noOfBoxes = 0;
        public int NoOfBoxes
        {
            get { return m_noOfBoxes; }
            set { m_noOfBoxes = value; }
        }

        private string m_invoiceNo = string.Empty;
        public string InvoiceNo
        {
            get { return m_invoiceNo; }
            set { m_invoiceNo = value; }
        }

        private string m_invoiceDate = DateTime.Today.ToString();
        public string InvoiceDate
        {
            get { return m_invoiceDate; }
            set { m_invoiceDate = value; }
        }

        private double m_invoiceTax = 0.00;
        public double InvoiceTaxAmount
        {
            get { return m_invoiceTax; }
            set { m_invoiceTax = value; }
        }
        public double DisplayInvoiceTaxAmount
        {
            get { return Math.Round(DBInvoiceTaxAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);}
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }            
        }
        public double DBInvoiceTaxAmount
        {
            get { return Math.Round(InvoiceTaxAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero);}
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }            
        }

        private double m_invoiceAmount = 0.00;
        public double InvoiceAmount
        {
            get { return m_invoiceAmount; }
            set { m_invoiceAmount = value; }
        }
        public double DisplayInvoiceAmount
        {
            get { return Math.Round(DBInvoiceAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);}
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }            
        }
        public double DBInvoiceAmount
        {
            get { return Math.Round(InvoiceAmount, Common.DBAmountRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }            
        }

        private string m_shippingDetails = string.Empty;
        public string ShippingDetails
        {
            get { return m_shippingDetails; }
            set { m_shippingDetails = value; }
        }

        private string m_vehicleNo = string.Empty;
        public string VehicleNo
        {
            get { return m_vehicleNo; }
            set { m_vehicleNo = value; }
        }

        private string m_receivedBy = string.Empty;
        public string ReceivedBy
        {
            get { return m_receivedBy; }
            set { m_receivedBy = value; }
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

        private string m_vendorCode = string.Empty;
        public string LocationCode
        {
            get;
            set;
        }
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

        private int m_destinationId = Common.INT_DBNULL;
        public int DestLocationID
        {
            get { return m_destinationId; }
            set { m_destinationId = value; }
        }

        private string m_destination = string.Empty;
        public string DestinationLocation
        {
            get { return m_destination; }
            set { m_destination = value; }
        }

        private string m_destinationCode = string.Empty;
        public string DestinationLocationCode
        {
            get { return m_destinationCode; }
            set { m_destinationCode = value; }
        }

        private string m_fromGrnDate = Common.DATETIME_NULL.ToShortDateString();
        public string FromGrnDate
        {
            get { return m_fromGrnDate; }
            set { m_fromGrnDate = value; }
        }

        private string m_toGrnDate = Common.DATETIME_NULL.ToShortDateString();
        public string ToGrnDate
        {
            get { return m_toGrnDate; }
            set { m_toGrnDate = value; }
        }

        private string m_fromPODate = Common.DATETIME_NULL.ToShortDateString();
        public string FromPODate
        {
            get { return m_fromPODate; }
            set { m_fromPODate = value; }
        }

        private string m_toPODate = Common.DATETIME_NULL.ToShortDateString();
        public string ToPODate
        {
            get { return m_toPODate; }
            set { m_toPODate = value; }
        }
        
        private List<GrnDetail> m_GRNDetailList = null;

        public List<GrnDetail> GRNDetailList
        {
            get { return m_GRNDetailList; }
            set { m_GRNDetailList = value; }
        }

        #endregion      

        #region Methods
        public bool  Save(ref string errorMessage)
        {
            try
            {
                DBParameterList dbParam;
                bool isSuccess = false;
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    try
                    {
                        dtManager.BeginTransaction();
                        {
                            string xmlDoc = Common.ToXml(this);

                            dbParam = new DBParameterList();
                            dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));

                            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                            DataTable dt = dtManager.ExecuteDataTable(SP_GRN_SAVE, dbParam);

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
                                    dtManager.CommitTransaction();
                                    this.GRNNo = Convert.ToString(dt.Rows[0]["GRNNo"]);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dtManager.RollbackTransaction();
                        throw ex;
                    }
                }
                return isSuccess;
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

        public List<Grn> Search()
        {
            List<Grn> GrnList = new List<Grn>();
            System.Data.DataTable dTable = new DataTable();
            try
            {
                string errorMessage = string.Empty;
                dTable = GetSelectedRecords(Common.ToXml(this), SP_GRN_SEARCH, ref errorMessage);

                if (dTable != null)
                {
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        Grn _grn = CreateGRNObject(drow);
                        GrnList.Add(_grn);
                    }
                }
                return GrnList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public DataTable SearchGRNDataTable()
        {
            DataTable dTable = new DataTable();           
            try
            {
                string errorMessage = string.Empty;
                dTable = GetSelectedRecords(Common.ToXml(this), SP_GRN_SEARCH, ref errorMessage);
                if (errorMessage != string.Empty)
                {
                    dTable = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dTable;
        }
       
        private Grn CreateGRNObject(DataRow dr)
        {
            try
            {
                Grn grn = new Grn();
                grn.AmendmentNo = Convert.ToInt32(dr[CON_FIELD_AMENDMENTNO]);
                grn.ChallanDate = Convert.ToString(dr[CON_FIELD_CHALLANDATE]);
                grn.ChallanNo = Convert.ToString(dr[CON_FIELD_CHALLANNO]);
                grn.CreatedBy = Convert.ToInt32(dr[CON_FIELD_CREATEDBY]);
                grn.GrossWeight = Convert.ToString(dr[CON_FIELD_GROSSWEIGHT]);
                grn.GRNDate = Convert.ToString(dr[CON_FIELD_GRNDATE]);
                grn.GRNNo = Convert.ToString(dr[CON_FIELD_GRNNO]);
                grn.InvoiceDate = Convert.ToString(dr[CON_FIELD_INVOICEDATE]);
                grn.InvoiceNo = Convert.ToString(dr[CON_FIELD_INVOICENO]);
                grn.ModifiedBy = Convert.ToInt32(dr[CON_FIELD_MODIFYBY]);
                grn.ModifiedDate = Convert.ToDateTime(dr[CON_FIELD_MODIFYDATE]);
                grn.NoOfBoxes = Convert.ToInt32(dr[CON_FIELD_NOOFBOXES]);
                grn.PODate = Convert.ToString(dr[CON_FIELD_PODATE]);
                grn.PONumber = Convert.ToString(dr[CON_FIELD_PONUMBER]);
                grn.ReceivedBy = Convert.ToString(dr[CON_FIELD_RECEIVEDBY]);
                grn.ShippingDetails = Convert.ToString(dr[CON_FIELD_SHIPPINGDETAILS]);
                grn.Status = Convert.ToInt32(dr[CON_FIELD_STATUS]);
                grn.StatusName = Convert.ToString(dr[CON_FIELD_STATUSNAME]);
                grn.VehicleNo = Convert.ToString(dr[CON_FIELD_VEHICLENO]);
                grn.VendorCode = Convert.ToString(dr[CON_FIELD_VENDORCODE]);
                grn.VendorName = Convert.ToString(dr[CON_FIELD_VENDORNAME]);
                grn.InvoiceAmount = Convert.ToDouble(dr[CON_FIELD_INVOICEAMOUNT]);
                grn.InvoiceTaxAmount = Convert.ToDouble(dr[CON_FIELD_INVOICETAX]);
                grn.DestinationLocationCode = Convert.ToString(dr[CON_FIELD_LOCATIONCODE]);
                Address _address = new Address();
                _address.Address1 = Convert.ToString(dr[CON_FIELD_LOCATIONCODE]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_ADDRESS1))
                    _address.Address1 = Convert.ToString(dr[CON_FIELD_D_ADDRESS1]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_ADDRESS2))
                    _address.Address2 = Convert.ToString(dr[CON_FIELD_D_ADDRESS2]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_ADDRESS3))
                    _address.Address3 = Convert.ToString(dr[CON_FIELD_D_ADDRESS3]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_ADDRESS4))
                    _address.Address4 = Convert.ToString(dr[CON_FIELD_D_ADDRESS4]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_CITYNAME))
                    _address.City = Convert.ToString(dr[CON_FIELD_D_CITYNAME]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_CITY))
                    _address.CityId = Convert.ToInt32(dr[CON_FIELD_D_CITY]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_COUNTRYNAME))
                    _address.Country = Convert.ToString(dr[CON_FIELD_D_COUNTRYNAME]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_COUNTRY))
                    _address.CountryId = Convert.ToInt32(dr[CON_FIELD_D_COUNTRY]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_EMAIL1))
                    _address.Email1 = Convert.ToString(dr[CON_FIELD_D_EMAIL1]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_EMAIL2))
                    _address.Email2 = Convert.ToString(dr[CON_FIELD_D_EMAIL2]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_FAX1))
                    _address.Fax1 = Convert.ToString(dr[CON_FIELD_D_FAX1]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_FAX2))
                    _address.Fax2 = Convert.ToString(dr[CON_FIELD_D_FAX2]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_MOBILE1))
                    _address.Mobile1 = Convert.ToString(dr[CON_FIELD_D_MOBILE1]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_MOBILE2))
                    _address.Mobile2 = Convert.ToString(dr[CON_FIELD_D_MOBILE2]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_PHONE1))
                    _address.PhoneNumber1 = Convert.ToString(dr[CON_FIELD_D_PHONE1]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_PHONE2))
                    _address.PhoneNumber2 = Convert.ToString(dr[CON_FIELD_D_PHONE2]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_PINCODE))
                    _address.PinCode = Convert.ToString(dr[CON_FIELD_D_PINCODE]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_STATENAME))
                    _address.State = Convert.ToString(dr[CON_FIELD_D_STATENAME]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_STATE))
                    _address.StateId = Convert.ToInt32(dr[CON_FIELD_D_STATE]);
                if (dr.Table.Columns.Contains(CON_FIELD_D_WEBSITE))
                    _address.Website = Convert.ToString(dr[CON_FIELD_D_WEBSITE]);
                grn.DestinationLocation = _address.GetAddress().ToString();
                return grn;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int GetGrossWeight()
        {
            try
            {
                int weight = 0;
                foreach (GrnDetail detail in GRNDetailList)
                {
                    weight = weight + (Convert.ToInt32(detail.ReceivedQty) * detail.Weight);
                }
                return weight;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IGrn Members

        private int m_createdBy=Common.INT_DBNULL;
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

        private string m_createdDate = Common.DATETIME_CURRENT.ToString();
        public string CreatedDate
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

        private int m_modifiedBy = Common.INT_DBNULL;
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

        private DateTime m_modifiedDate = Common.DATETIME_CURRENT;
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

        #endregion
    }

}
