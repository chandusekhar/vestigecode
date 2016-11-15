using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreComponent.Core.BusinessObjects
{
    /// <summary>
    /// Structure to store generic address details of a single location
    /// </summary>
    public class Address
    {
        private const string CON_Address1 = "Address1";
        private const string CON_Address2 = "Address2";
        private const string CON_Address3 = "Address3";
        private const string CON_Address4 = "Address4";
        private const string CON_CityId = "CityId";
        private const string CON_StateID = "StateId";
        private const string CON_CountryId = "CountryId";
        private const string CON_Pincode = "Pincode";
        private const string CON_Phone1 = "Phone1";
        private const string CON_Phone2 = "Phone2";
        private const string CON_CityName = "CityName";
        private const string CON_StateName = "StateName";
        private const string CON_CountryName = "CountryName";
        private const string CON_Mobile1 = "Mobile1";
        private const string CON_Mobile2 = "Mobile2";
        private const string CON_Fax1 = "Fax1";
        private const string CON_Fax2 = "Fax2";
        private const string CON_EmailId1 = "EmailId1";
        private const string CON_EmailId2 = "EmailId2";
        private const string CON_WebSite = "WebSite";
        private const string CON_ModifiedDate = "ModifiedDate";
        private string m_address1, m_address2, m_address3, m_address4;
        private int m_cityId, m_stateId, m_countryId;
        private string m_country, m_state, m_city;
        private string m_phoneNumber1, m_phoneNumber2, m_mobile1, m_mobile2, m_fax1, m_fax2, m_email1, m_email2, m_website, m_pinCode;
        private DateTime m_addressModifiedDate = Common.DATETIME_NULL;
        private const string CON_DISTRIBUTORNAME = "DistributorName";
        public string DistributorName
        {
            get;
            set;
        }
        public string Country
        {
            get { return m_country; }
            set { m_country = value; }
        }

        public string State
        {
            get { return m_state; }
            set { m_state = value; }
        }

        public string City
        {
            get { return m_city; }
            set { m_city = value; }
        }

        public string PinCode
        {
            get { return m_pinCode; }
            set { m_pinCode = value; }
        }

        public string Website
        {
            get { return m_website; }
            set { m_website = value; }
        }

        public string Email2
        {
            get { return m_email2; }
            set { m_email2 = value; }
        }

        public string Email1
        {
            get { return m_email1; }
            set { m_email1 = value; }
        }

        public string Fax2
        {
            get { return m_fax2; }
            set { m_fax2 = value; }
        }

        public string Fax1
        {
            get { return m_fax1; }
            set { m_fax1 = value; }
        }

        public string Mobile2
        {
            get { return m_mobile2; }
            set { m_mobile2 = value; }
        }

        public string Mobile1
        {
            get { return m_mobile1; }
            set { m_mobile1 = value; }
        }

        public string PhoneNumber2
        {
            get { return m_phoneNumber2; }
            set { m_phoneNumber2 = value; }
        }

        public string PhoneNumber1
        {
            get { return m_phoneNumber1; }
            set { m_phoneNumber1 = value; }
        }

        public int CountryId
        {
            get { return m_countryId; }
            set { m_countryId = value; }
        }

        public int StateId
        {
            get { return m_stateId; }
            set { m_stateId = value; }
        }

        public int CityId
        {
            get { return m_cityId; }
            set { m_cityId = value; }
        }

        public string Address3
        {
            get { return m_address3; }
            set { m_address3 = value; }
        }

        public string Address4
        {
            get { return m_address4; }
            set { m_address4 = value; }
        }

        public string Address2
        {
            get { return m_address2; }
            set { m_address2 = value; }
        }

        public string Address1
        {
            get { return m_address1; }
            set { m_address1 = value; }
        }
        public string FullAddress
        {
            get { return GetAddress(); }
        }
        public DateTime AddressModifiedDate
        {
            get { return m_addressModifiedDate; }
            set { m_addressModifiedDate = value; }
        }
        public string GetAddress()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Address1 + " " + Address2 + " ");
            sb.AppendLine(Address3 + " " + Address4 + " " + City + " " + PinCode);
            sb.AppendLine(State + " " + Country);
            //sb.Append(Address1 + " " + Address2 +" "+Address3+" "+Address4);
            ////sb.AppendLine(" ");
            //sb.AppendLine(City+" "+State+" "+Country+"  " +PinCode);
            ////sb.Append(" ");
            return sb.ToString();
        }
        public static Address CreateAddressObject(System.Data.DataRow dr)
        {
            Address address = new Address();


            if (dr.Table.Columns.Contains(CON_Address1))
                address.Address1 = Convert.ToString(dr[CON_Address1]);
            if (dr.Table.Columns.Contains(CON_Address2))
                address.Address2 = Convert.ToString(dr[CON_Address2]);
            if (dr.Table.Columns.Contains(CON_Address3))
                address.Address3 = Convert.ToString(dr[CON_Address3]);
            if (dr.Table.Columns.Contains(CON_Address4))
                address.Address4 = Convert.ToString(dr[CON_Address4]);
            if (dr.Table.Columns.Contains(CON_CityId))
                address.City = Convert.ToString(dr[CON_CityName]);
            if (dr.Table.Columns.Contains(CON_CityName))
                address.CityId = Convert.ToInt32(dr[CON_CityId]);
            if (dr.Table.Columns.Contains(CON_CountryName))
                address.Country = Convert.ToString(dr[CON_CountryName]);
            if (dr.Table.Columns.Contains(CON_CountryId))
                address.CountryId = Convert.ToInt32(dr[CON_CountryId]);
            if (dr.Table.Columns.Contains(CON_EmailId1))
                address.Email1 = Convert.ToString(dr[CON_EmailId1]);
            if (dr.Table.Columns.Contains(CON_EmailId2))
                address.Email2 = Convert.ToString(dr[CON_EmailId2]);
            if (dr.Table.Columns.Contains(CON_Fax1))
                address.Fax1 = Convert.ToString(dr[CON_Fax1]);
            if (dr.Table.Columns.Contains(CON_Fax2))
                address.Fax2 = Convert.ToString(dr[CON_Fax2]);
            if (dr.Table.Columns.Contains(CON_Mobile1))
                address.Mobile1 = Convert.ToString(dr[CON_Mobile1]);
            if (dr.Table.Columns.Contains(CON_Mobile2))
                address.Mobile2 = Convert.ToString(dr[CON_Mobile2]);
            if (dr.Table.Columns.Contains(CON_Phone1))
                address.PhoneNumber1 = Convert.ToString(dr[CON_Phone1]);
            if (dr.Table.Columns.Contains(CON_Phone2))
                address.PhoneNumber2 = Convert.ToString(dr[CON_Phone2]);
            if (dr.Table.Columns.Contains(CON_Pincode))
                address.PinCode = Convert.ToString(dr[CON_Pincode]);
            if (dr.Table.Columns.Contains(CON_StateID))
                address.State = Convert.ToString(dr[CON_StateName]);
            if (dr.Table.Columns.Contains(CON_StateName))
                address.StateId = Convert.ToInt32(dr[CON_StateID]);
            if (dr.Table.Columns.Contains(CON_WebSite))
                address.Website = Convert.ToString(dr[CON_WebSite]);
            if (dr.Table.Columns.Contains(CON_ModifiedDate))
                address.AddressModifiedDate = Convert.ToDateTime(dr[CON_ModifiedDate]);
            if (dr.Table.Columns.Contains(CON_DISTRIBUTORNAME))
                address.DistributorName = Convert.ToString(dr[CON_DISTRIBUTORNAME]);

            return address;


        }
    }

    public class Contact : Address
    {
        private System.String m_contactId;

        public System.String ContactId
        {
            get { return m_contactId; }
            set { m_contactId = value; }
        }

        private System.Int32 m_rowNum;

        public System.Int32 RowNum
        {
            get { return m_rowNum; }
            set { m_rowNum = value; }
        }

        private System.Int32 m_title;

        public System.Int32 Title
        {
            get { return m_title; }
            set { m_title = value; }
        }

        private System.String m_firstName;

        public System.String FirstName
        {
            get { return m_firstName; }
            set { m_firstName = value; }
        }

        private System.String m_middleName;

        public System.String MiddleName
        {
            get { return m_middleName; }
            set { m_middleName = value; }
        }

        private System.String m_lastName;

        public System.String LastName
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }

        private System.String m_designation;

        public System.String Designation
        {
            get { return m_designation; }
            set { m_designation = value; }
        }

        public System.String Department
        {
            get;
            set;
        }

        private System.Boolean m_isPrimary;

        public System.Boolean IsPrimary
        {
            get { return m_isPrimary; }
            set { m_isPrimary = value; }
        }

        private System.Int32 m_status;

        public System.Int32 Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        private System.Int32 m_createdBy;

        public System.Int32 CreatedBy
        {
            get { return m_createdBy; }
            set { m_createdBy = value; }
        }

        private System.DateTime m_createdDate;

        public System.DateTime CreatedDate
        {
            get { return m_createdDate; }
            set { m_createdDate = value; }
        }

        private System.Int32 m_modifiedBy;

        public System.Int32 ModifiedBy
        {
            get { return m_modifiedBy; }
            set { m_modifiedBy = value; }
        }

        private string m_modifiedDate;

        public string ModifiedDate
        {
            get { return m_modifiedDate; }
            set { m_modifiedDate = value; }
        }

        private string m_statusValue;
        public string StatusValue
        {
            get { return m_statusValue; }
            set { m_statusValue = value; }
        }
    }
}
