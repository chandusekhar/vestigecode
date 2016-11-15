using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using System.Data;

namespace CoreComponent.MasterData.BusinessObjects
{
    [Serializable]
    public class VendorMaster
    {
        #region Property
        private int m_vendorID = Common.INT_DBNULL;

        public int VendorID
        {
            get { return m_vendorID; }
            set { m_vendorID = value; }
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
        private string m_tinNo = string.Empty;

        public string TinNo
        {
            get { return m_tinNo; }
            set { m_tinNo = value; }
        }
        private string m_paymentTerms = string.Empty;

        public string PaymentTerms
        {
            get { return m_paymentTerms; }
            set { m_paymentTerms = value; }
        }

        private Address m_address;
        public Address Address
        {
            get { return m_address; }
            set { m_address = value; }
        }

        private string m_addressText;

        public string AddressText
        {
            get { return m_addressText; }
            set { m_addressText = value; }
        }

        private string m_warehouse = string.Empty;
        public string Warehouse
        {
            get { return m_warehouse; }
            set { m_warehouse = value; }
        }
        private int m_WareHouseID;

        public int WareHouseID
        {
            get { return m_WareHouseID; }
            set { m_WareHouseID = value; }
        }
        private int m_taxJuridicationID;

        public int TaxJuridicationID
        {
            get { return m_taxJuridicationID; }
            set { m_taxJuridicationID = value; }
        }
        private int m_status = Common.INT_DBNULL;

        public int Status
        {
            get { return m_status; }
            set { m_status = value; }
        }
        private int m_createdBy = Common.INT_DBNULL;

        public int CreatedBy
        {
            get { return m_createdBy; }
            set { m_createdBy = value; }
        }
        private int m_modifiedBy = Common.INT_DBNULL;

        public int ModifiedBy
        {
            get { return m_modifiedBy; }
            set { m_modifiedBy = value; }
        }
        private DateTime m_createdDate;

        public DateTime CreatedDate
        {
            get { return m_createdDate; }
            set { m_createdDate = value; }
        }
        private DateTime m_modifiedDate;

        public DateTime ModifiedDate
        {
            get { return m_modifiedDate; }
            set { m_modifiedDate = value; }
        }
        #endregion

        #region Methods
        public static List<VendorMaster> GetVendor(int vendorID)
        {
            //returns vendor details if require only one vendor, else pass -1 to get all active vendors .
            DataTable dtVendors = Common.ParameterLookup(Common.ParameterType.Vendor, new ParameterFilter("",vendorID, 0, 0));
            List<VendorMaster> _vendors = new List<VendorMaster>();
            if (dtVendors != null)
            {
                foreach (DataRow dr in dtVendors.Rows)
                {
                    VendorMaster _vendor = CreateVendorObject(dr);
                    _vendors.Add(_vendor);
                }
            }
            return _vendors;
        }
        public static VendorMaster CreateVendorObject(DataRow dr)
        {
            VendorMaster _vendor = new VendorMaster();
            _vendor.VendorID = Convert.ToInt32(dr["VendorId"]);
            _vendor.VendorName = Convert.ToString(dr["VendorName"]);
            _vendor.VendorCode = Convert.ToString(dr["VendorCode"]);

            _vendor.Address = new Address();
            _vendor.Address.Address1 = Convert.ToString(dr["Address1"]);
            _vendor.Address.Address2 = Convert.ToString(dr["Address2"]);
            _vendor.Address.Address3 = Convert.ToString(dr["Address3"]);
            _vendor.Address.Address4 = Convert.ToString(dr["Address4"]);
            _vendor.Address.City = Convert.ToString(dr["CityName"]);
            _vendor.Address.State = Convert.ToString(dr["StateName"]);
            _vendor.Address.Country = Convert.ToString(dr["CountryName"]);
            _vendor.Address.CityId = Convert.ToInt32(dr["CityId"]);
            _vendor.Address.StateId = Convert.ToInt32(dr["StateId"]);
            _vendor.Address.CountryId = Convert.ToInt32(dr["CountryId"]);
            _vendor.Address.Website = Convert.ToString(dr["WebSite"]);
            _vendor.Address.PinCode = Convert.ToString(dr["PinCode"]);
            _vendor.Address.PhoneNumber2 = Convert.ToString(dr["Phone2"]);
            _vendor.Address.PhoneNumber1 = Convert.ToString(dr["Phone1"]);
            _vendor.Address.Mobile2 = Convert.ToString(dr["Mobile2"]);
            _vendor.Address.Mobile1 = Convert.ToString(dr["Mobile1"]);
            _vendor.Address.Fax1 = Convert.ToString(dr["Fax1"]);
            _vendor.Address.Fax2 = Convert.ToString(dr["Fax2"]);
            _vendor.Address.Email1 = Convert.ToString(dr["EmailId1"]);
            _vendor.Address.Email2 = Convert.ToString(dr["EmailId2"]);
            _vendor.Address.AddressModifiedDate = Convert.ToDateTime(dr["ModifiedDate"]);
            _vendor.AddressText = _vendor.Address.GetAddress();

            _vendor.PaymentTerms = Convert.ToString(dr["PaymentTerms"]);
            _vendor.WareHouseID = Convert.ToInt32(dr["Warehouse"]);
            _vendor.TaxJuridicationID = Convert.ToInt32(dr["TaxJurisdictionId"]);
            return _vendor;
        }
        #endregion
    }
}
