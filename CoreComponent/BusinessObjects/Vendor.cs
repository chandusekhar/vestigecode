using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using System.Data;


/*Reason for Creating: We have to use the address class for inheritence, instead of creating the property
 *                      show address detail in grid and vendorMaster is already used for Purchase order.
 * 
 */
namespace  CoreComponent.MasterData.BusinessObjects
{
    [Serializable]
    public class Vendor : Address
    {

        public const string MODULE_CODE = "MDM04";
        #region SP Declaration
        private const string SP_Vendor_SAVE = "usp_VendorSave";
        private const string SP_Vendor_SEARCH = "usp_VendorSearch";
        private const string SP_Vendor_CONTACT_SEARCH = "usp_VendorContactSearch";
        #endregion

        #region Property
        private int mobjVendorID = Common.INT_DBNULL;
        private string mobjVendorCode = string.Empty;
        private string mobjVendorName = string.Empty;
        private string m_tinNo = string.Empty;
        //private int m_taxJuridicationID;
        private int m_status = Common.INT_DBNULL;
        private string m_statusValue = string.Empty;
        
        private string m_warehouse = string.Empty;
        private string m_warehouseValue = string.Empty;
        private int m_createdBy = Common.INT_DBNULL;
        private int m_modifiedBy = Common.INT_DBNULL;
        private List<Contact> m_contact;
        private List<Contact> m_removeContact;
        private List<string> m_modifiedContactId;
        private string m_createdDate;
        private string m_modifiedDate;
        private int m_paymentTerms = Common.INT_DBNULL;

        public List<Contact> RemovedContacts
        {
            get { return m_removeContact; }
            set { m_removeContact = value; }

        }
        public List<Contact> Contact
        {
            get { return m_contact; }
            set { m_contact = value; }
        }
        public List<string> ModifiedContactId
        {
            get { return m_modifiedContactId; }
            set { m_modifiedContactId = value; }
        }
        public string CstNo
        {
            get;
            set;
        }
        public string VatNo
        {
            get;
            set;
        }

        public int VendorID
        {
            get { return mobjVendorID; }
            set { mobjVendorID = value; }
        }
        public string VendorCode
        {
            get { return mobjVendorCode; }
            set { mobjVendorCode = value; }
        }
        public string VendorName
        {
            get { return mobjVendorName; }
            set { mobjVendorName = value; }
        }
        public string TinNo
        {
            get { return m_tinNo; }
            set { m_tinNo = value; }
        }
        public int PaymentTerms
        {
            get { return m_paymentTerms; }
            set { m_paymentTerms = value; }
        }
        public string WarehouseValue
        {
            get { return m_warehouseValue; }
            set { m_warehouseValue = value; }
        }
        public string Warehouse
        {
            get { return m_warehouse; }
            set { m_warehouse = value; }
        }
        //public int TaxJuridicationID
        //{
        //    get { return m_taxJuridicationID; }
        //    set { m_taxJuridicationID = value; }
        //}
        public int Status
        {
            get { return m_status; }
            set { m_status = value; }
        }
        public int CreatedBy
        {
            get { return m_createdBy; }
            set { m_createdBy = value; }
        }
        public int ModifiedBy
        {
            get { return m_modifiedBy; }
            set { m_modifiedBy = value; }
        }
        public string CreatedDate
        {
            get { return m_createdDate; }
            set { m_createdDate = value; }
        }
        public string ModifiedDate
        {
            get { return m_modifiedDate; }
            set { m_modifiedDate = value; }
        }
        public string StatusValue
        {
            get { return m_statusValue; }
            set { m_statusValue = value; }
        }

        public string TaxJurisdictionId
        {
            get;
            set;
        }
        #endregion

        #region Methods

        public List<Vendor> GetVendors()
        {
            ItemMaster im = new ItemMaster();
            //returns vendor details if require only one vendor, else pass -1 to get all active vendors .
            //DataTable dtVendors = Common.ParameterLookup(Common.ToXml(this), new ParameterFilter("", vendorID, 0, 0));
            string errorMessage = string.Empty;
            System.Data.DataTable dTable = im.GetSelectedRecordsInDataTable(Common.ToXml(this), SP_Vendor_SEARCH, ref errorMessage);
            List<Vendor> lstVendor = new List<Vendor>();
            if (dTable != null)
            {
                foreach (DataRow dr in dTable.Rows)
                {
                    Vendor objVendor = CreateVendorObject(dr);
                    lstVendor.Add(objVendor);
                }
            }
            return lstVendor;
        }

        public List<Contact> ContactSearch(ref string errorMessage)
        {
            ItemMaster im = new ItemMaster();
            //returns vendor details if require only one vendor, else pass -1 to get all active vendors .
            //DataTable dtVendors = Common.ParameterLookup(Common.ToXml(this), new ParameterFilter("", vendorID, 0, 0));
            
            System.Data.DataTable dTable = im.GetSelectedRecordsInDataTable(Common.ToXml(this), SP_Vendor_CONTACT_SEARCH, ref errorMessage);
            List<Contact> lstVendor = new List<Contact>();
            if (dTable != null)
            {
                if (dTable == null | dTable.Rows.Count <= 0)
                    return null;

                foreach (System.Data.DataRow drow in dTable.Rows)
                {
                    Contact cnt = new Contact();
                    cnt.ContactId = drow["VendorContactId"].ToString();

                    cnt.Title = Convert.ToInt32(drow["Title"].ToString());
                    cnt.FirstName = drow["FirstName"].ToString();
                    cnt.MiddleName = drow["MiddleName"].ToString();
                    cnt.LastName = drow["LastName"].ToString();
                    cnt.Address1 = drow["Address1"].ToString();
                    cnt.Address2 = drow["Address2"].ToString();
                    cnt.Address3 = drow["Address3"].ToString();
                    cnt.PinCode = drow["PinCode"].ToString();
                    cnt.Mobile1 = drow["Mobile1"].ToString();
                    cnt.PhoneNumber1 = drow["Phone1"].ToString();
                    cnt.Fax1 = drow["Fax1"].ToString();
                    cnt.Email1 = drow["EmailId1"].ToString();
                    cnt.Designation = drow["Designation"].ToString();
                    cnt.Department = drow["Department"].ToString();
                    cnt.CountryId = Convert.ToInt32(drow["CountryId"]);
                    cnt.StateId = Convert.ToInt32(drow["StateId"]);
                    cnt.CityId = Convert.ToInt32(drow["CityId"]);

                    cnt.Country = drow["Country"].ToString();
                    cnt.State = drow["State"].ToString();
                    cnt.City = drow["City"].ToString();

                    cnt.Website = drow["Website"].ToString();
                    cnt.IsPrimary = Convert.ToBoolean(drow["IsPrimary"]);
                    cnt.Status = Convert.ToInt32(drow["Status"]);
                    cnt.ModifiedDate = drow["ModifiedDate"].ToString();
                    cnt.StatusValue = drow["StatusValue"].ToString();

                    lstVendor.Add(cnt);
                }
            }
            return lstVendor;
        }

        public static Vendor CreateVendorObject(DataRow dr)
        {
            Vendor objVendor = new Vendor();
            objVendor.VendorID = Convert.ToInt32(dr["VendorId"]);
            objVendor.VendorName = Convert.ToString(dr["VendorName"]);
            objVendor.VendorCode = Convert.ToString(dr["VendorCode"]);

            objVendor.Address1 = Convert.ToString(dr["Address1"]);
            objVendor.Address2 = Convert.ToString(dr["Address2"]);
            objVendor.Address3 = Convert.ToString(dr["Address3"]);
            objVendor.City = Convert.ToString(dr["City"]);
            objVendor.State = Convert.ToString(dr["State"]);
            objVendor.Country = Convert.ToString(dr["Country"]);
            objVendor.CityId = Convert.ToInt32(dr["CityId"]);
            objVendor.StateId = Convert.ToInt32(dr["StateId"]);
            objVendor.CountryId = Convert.ToInt32(dr["CountryId"]);
            objVendor.Website = Convert.ToString(dr["WebSite"]);
            objVendor.PinCode = Convert.ToString(dr["PinCode"]);

            objVendor.PhoneNumber1 = dr["Phone1"].ToString();
            objVendor.Mobile1 = dr["Mobile1"].ToString();
            objVendor.Fax1 = dr["Fax1"].ToString();
            objVendor.Email1 = dr["EmailId1"].ToString();

            objVendor.TinNo = dr["TinNo"].ToString();
            objVendor.VatNo = dr["VatNo"].ToString();
            objVendor.CstNo = dr["CstNo"].ToString();
            objVendor.Warehouse = dr["Warehouse"].ToString();
            objVendor.WarehouseValue = dr["WarehouseValue"].ToString();
            //objVendor.TaxJuridicationID = Convert.ToInt32(dr["TaxJurisdictionId"]);

            objVendor.Status = Convert.ToInt32(dr["Status"].ToString().Length==0?Common.INT_DBNULL.ToString():dr["Status"].ToString());
            objVendor.StatusValue = dr["StatusValue"].ToString();
            objVendor.ModifiedDate = dr["ModifiedDate"].ToString();
            objVendor.PaymentTerms = Convert.ToInt32(dr["PaymentTerms"]);
            objVendor.TaxJurisdictionId = dr["TaxJurisdictionId"].ToString();
            return objVendor;
        }


        public bool Save(ref string errorMessage)
        {
            ItemMaster im = new ItemMaster();
            bool isSuccess = false;
            // call the save method which returns whether the save was successfull or not
            isSuccess = im.Save(Common.ToXml(this), SP_Vendor_SAVE, ref errorMessage);
            return isSuccess;
        }
        #endregion
    }
}
