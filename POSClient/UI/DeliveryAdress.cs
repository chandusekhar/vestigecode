using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.UI;
using CoreComponent.UI;

namespace POSClient.UI
{
    public partial class DeliveryAdress : POSClient.UI.BaseChildForm
    {
        public Address ReturnObject { get; set; }
        public int DistributorId { get; set; }
        private Address m_Address;
        public DeliveryAdress()
        {
            InitializeComponent();
            LoadControl();
        }

        private void LoadControl()
        {
            try
            {
                int key03 = 0;

                if (Common.IsMiniBranchLocation == 1)
                {
                    key03 = 1;
                }
                else
                {
                    key03 = 0;
                }
                cmbContactCity.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbContactCountry.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbContactState.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbPickUpCenter.DropDownStyle = ComboBoxStyle.DropDownList;
                FillPickUpCenter();
                FillCountry();
                FillState();
                FillCity();
                DataTable dtAddressType = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("COURIERADDRESSTYPE", 0, 0, key03));
                cmbAddressType.DropDownStyle = ComboBoxStyle.DropDownList;
                if (dtAddressType != null)
                {
                    cmbAddressType.DataSource = dtAddressType;
                    cmbAddressType.DisplayMember = Common.KEYVALUE1;
                    cmbAddressType.ValueMember = Common.KEYCODE1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        #region Events

        private void cmbAddressType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                m_Address = new Address();
                if (cmbAddressType.SelectedIndex > 0)
                {
                    if (Convert.ToInt32(cmbAddressType.SelectedValue) == (int)Common.CourierAddressType.PC)
                    {
                        cmbPickUpCenter.Enabled = true;
                        pnlAddress.Enabled = false;
                        txtDistributorId.Enabled = false;
                        txtDistributorId.Text = string.Empty;
                        m_Address = GetPickUpCenterAddress(Convert.ToInt32(cmbPickUpCenter.SelectedValue));
                    }
                    if (Convert.ToInt32(cmbAddressType.SelectedValue) == (int)Common.CourierAddressType.Distributor)
                    {
                        cmbPickUpCenter.SelectedValue = -1;
                        txtDistributorId.Text = DistributorId.ToString();
                        m_Address = GetDistributorAddress(DistributorId);
                        pnlAddress.Enabled = false;
                        txtDistributorId.Enabled = true;
                        cmbPickUpCenter.Enabled = false;
                    }
                    if (Convert.ToInt32(cmbAddressType.SelectedValue) == (int)Common.CourierAddressType.Other)
                    {
                        cmbPickUpCenter.SelectedValue = -1;
                        pnlAddress.Enabled = true;
                        cmbPickUpCenter.Enabled = false;
                        txtDistributorId.Enabled = false;
                        txtDistributorId.Text = string.Empty;
                    }

                }
                else
                {
                    cmbPickUpCenter.SelectedValue = -1;
                    cmbPickUpCenter.Enabled = false;
                    txtDistributorId.Enabled = false;
                    txtDistributorId.Text = string.Empty;
                }
                if (!Common.CheckIfDistributorAddHidden(DistributorId))
                {
                    SetAddress();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void cmbPickUpCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPickUpCenter.SelectedIndex >= 0)
                {
                    if (cmbPickUpCenter.SelectedValue.GetType() != typeof(System.Data.DataRowView))
                    {
                        m_Address = GetPickUpCenterAddress(Convert.ToInt32(cmbPickUpCenter.SelectedValue));
                        if (m_Address != null)
                        {
                            SetAddress();
                            btnOk.Focus();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void cmbContactCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillState();
                //FillCity();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void cmbContactState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               FillCity();
                //FillState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateAdd();
                StringBuilder sbError = new StringBuilder();
                sbError = GenerateAddError();
                if (sbError.ToString().Length > 0)
                {
                    MessageBox.Show(Common.ReturnErrorMessage(sbError).ToString());
                    return;
                }
                else if (Convert.ToInt32(cmbAddressType.SelectedValue) == (int)Common.CourierAddressType.Distributor || Convert.ToInt32(cmbAddressType.SelectedValue) == (int)Common.CourierAddressType.Other)
                {
                    //if (Common.IsMiniBranchLocation != 1)
                    //{
                    if (Convert.ToInt32(cmbAddressType.SelectedValue) != (int)Common.CourierAddressType.Distributor)
                    {
                        ValidateAddress();
                    }
                    //}
                    sbError = new StringBuilder();
                    sbError = GenerateAddError();

                    if (this.cmbContactCity.Text.Equals("Select"))
                    {
                        MessageBox.Show("Please Select/Enter the field : City");
                        return;
                    }
                    if (sbError.ToString().Length > 0)
                    {
                        MessageBox.Show(Common.ReturnErrorMessage(sbError).ToString());
                        return;
                    }
                    else
                    {
                        m_Address = CreateAddress();
                    }
                }
                ReturnObject = m_Address;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void txtDistributorId_Leave(object sender, EventArgs e)
        {
            try
            {
                int distID;
                if (Int32.TryParse(txtDistributorId.Text, out distID))
                    m_Address = GetDistributorAddress(distID);
                else
                    m_Address = new Address();
                SetAddress();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        #endregion

        private void FillPickUpCenter()
        {
            try
            {
                DataTable dtPOSLocations = Common.ParameterLookup(Common.ParameterType.POSLocations, new ParameterFilter(Common.LocationCode, -1, -1, -1));
                dtPOSLocations.DefaultView.RowFilter = " LocationType='PC'";
                if (dtPOSLocations != null)
                {

                    cmbPickUpCenter.DataSource = dtPOSLocations.DefaultView.ToTable();
                    cmbPickUpCenter.DisplayMember = "LocationCode";
                    cmbPickUpCenter.ValueMember = "LocationId";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillCountry()
        {
            try
            {

                DataTable dtCountry = Common.ParameterLookup(Common.ParameterType.Country, new ParameterFilter(string.Empty, 0, 0, 0));
                if (dtCountry != null)
                {
                    cmbContactCountry.DataSource = dtCountry;
                    cmbContactCountry.DisplayMember = "CountryName";
                    cmbContactCountry.ValueMember = "CountryId";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillState()
        {
            try
            {
                cmbContactState.DataSource = null;
                DataTable dtState = new DataTable();
                if (cmbContactCountry.SelectedIndex > 0)
                {
                    dtState = Common.ParameterLookup(Common.ParameterType.State, new ParameterFilter(string.Empty, Convert.ToInt32(cmbContactCountry.SelectedValue), 0, 0));
                    cmbContactState.DataSource = dtState;
                    cmbContactState.DisplayMember = "StateName";
                    cmbContactState.ValueMember = "StateId";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillCity()
        {
            try
            {
                cmbContactCity.DataSource = null;
                DataTable dtCity = new DataTable();
                if (cmbContactState.SelectedIndex > 0)
                {
                    dtCity = Common.ParameterLookup(Common.ParameterType.City, new ParameterFilter(string.Empty, Convert.ToInt32(cmbContactState.SelectedValue), 0, 0));
                    cmbContactCity.DataSource = dtCity;
                    cmbContactCity.DisplayMember = "CityName";
                    cmbContactCity.ValueMember = "CityId";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private Address GetDistributorAddress(int DistributorID)
        {
            try
            {
                Address _address = new Address();
                DataTable dtDistributor = Common.ParameterLookup(Common.ParameterType.Distributor, new ParameterFilter(string.Empty, DistributorID, 0, 0));
                if (dtDistributor != null && dtDistributor.Rows.Count > 0)
                {
                    _address = Address.CreateAddressObject(dtDistributor.Rows[0]);
                }
                return _address;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Address GetPickUpCenterAddress(int LocationID)
        {
            try
            {
                Address _address = new Address();
                DataTable dt = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter(string.Empty, -1, LocationID, -1));
                if (dt != null && dt.Rows.Count > 0)
                {
                    _address = Address.CreateAddressObject(dt.Rows[0]);
                }
                return _address;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetAddress()
        {
            try
            {
                if (m_Address != null)
                {
                    txtContactAddress1.Text = m_Address.Address1;
                    txtContactAddress2.Text = m_Address.Address2;
                    txtContactAddress3.Text = m_Address.Address3;
                    txtContactAddress4.Text = m_Address.Address4;
                    txtContactEmail1.Text = m_Address.Email1;
                    txtContactEmail2.Text = m_Address.Email2;
                    txtContactFax1.Text = m_Address.Fax1;
                    txtContactFax2.Text = m_Address.Fax2;
                    txtContactMobile1.Text = m_Address.Mobile1;
                    txtContactMobile2.Text = m_Address.Mobile2;
                    txtContactPhone2.Text = m_Address.PhoneNumber1;
                    txtContactPhone2.Text = m_Address.PhoneNumber2;
                    txtContactPinCode.Text = m_Address.PinCode;

                    cmbContactCountry.SelectedValue = m_Address.CountryId;
                    cmbContactState.SelectedValue = m_Address.StateId;
                    cmbContactCity.SelectedValue = m_Address.CityId;
                    txtContactWebsite.Text = m_Address.Website;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Address CreateAddress()
        {
            try
            {
                Address address = new Address();
                address.Address1 = txtContactAddress1.Text.Trim();
                address.Address2 = txtContactAddress2.Text.Trim();
                address.Address3 = txtContactAddress3.Text.Trim();
                address.Address4 = txtContactAddress4.Text.Trim();
                address.CityId = Convert.ToInt32(cmbContactCity.SelectedValue);
                address.City = cmbContactCity.Text;
                address.StateId = Convert.ToInt32(cmbContactState.SelectedValue);
                address.State = cmbContactState.Text;
                address.CountryId = Convert.ToInt32(cmbContactCountry.SelectedValue);
                address.Country = cmbContactCountry.Text;
                address.Email1 = txtContactEmail1.Text.Trim();
                address.Email2 = txtContactEmail2.Text.Trim();
                address.Fax1 = txtContactFax1.Text.Trim();
                address.Fax2 = txtContactFax2.Text.Trim();
                address.Mobile1 = txtContactMobile1.Text.Trim();
                address.Mobile2 = txtContactMobile2.Text.Trim();
                address.PhoneNumber1 = txtContactPhone1.Text.Trim();
                address.PhoneNumber2 = txtContactPhone2.Text.Trim();
                address.PinCode = txtContactPinCode.Text.Trim();
                address.Website = txtContactWebsite.Text.Trim();
                return address;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region Validation

        private void ValidateAdd()
        {
            try
            {
                errorAdd.Clear();
                //ValidateRequiredComboField(cmbAddressType, lblAddress);
                errorAdd.SetError(cmbAddressType, string.Empty);
                if (cmbAddressType.SelectedIndex <= 0)
                {
                    errorAdd.SetError(cmbAddressType, Common.GetMessage("VAL0002", lblAddress.Text.Trim().Substring(0, lblAddress.Text.Trim().Length - 2)));
                }

                if (Convert.ToInt32(cmbAddressType.SelectedValue) == (int)Common.CourierAddressType.PC)
                    ValidateRequiredComboField(cmbPickUpCenter, lblPC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidateAddress()
        {
            try
            {
                ValidateRequiredTextField(txtContactAddress1, lblContactAddress1);
                ValidateRequiredTextField(txtContactAddress2, lblContactAddress2);
                ValidateRequiredComboField(cmbContactCountry, lblContactCountry);
                ValidateRequiredComboField(cmbContactState, lblContactState);
                ValidateRequiredComboField(cmbContactCity, lblContactCity);
                ValidateEmail(txtContactEmail1, lblContactEmail1);
                //ValidateEmail(txtContactEmail2, lblContactEmail2);
                ValidatePinCode(txtContactPinCode, lblContactPinCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private StringBuilder GenerateAddError()
        {
            try
            {
                StringBuilder sbError = new StringBuilder();
                bool isFocus = false;

                if (errorAdd.GetError(cmbAddressType).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(cmbAddressType));
                    sbError.AppendLine();
                    if (!isFocus)
                    {
                        cmbAddressType.Focus();
                        isFocus = true;
                    }
                }
                if (errorAdd.GetError(cmbPickUpCenter).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(cmbPickUpCenter));
                    sbError.AppendLine();
                    if (!isFocus)
                    {
                        cmbPickUpCenter.Focus();
                        isFocus = true;
                    }
                }
                if (errorAdd.GetError(txtContactAddress1).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(txtContactAddress1));
                    sbError.AppendLine();
                    if (!isFocus)
                    {
                        txtContactAddress1.Focus();
                        isFocus = true;
                    }
                }
                if (errorAdd.GetError(txtContactAddress2).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(txtContactAddress2));
                    sbError.AppendLine();
                    if (!isFocus)
                    {
                        txtContactAddress2.Focus();
                        isFocus = true;
                    }
                }
                if (errorAdd.GetError(cmbContactCountry).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(cmbContactCountry));
                    sbError.AppendLine();
                    if (!isFocus)
                    {
                        cmbContactCountry.Focus();
                        isFocus = true;
                    }
                }
                if (errorAdd.GetError(cmbContactState).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(cmbContactState));
                    sbError.AppendLine();
                    if (!isFocus)
                    {
                        cmbContactState.Focus();
                        isFocus = true;
                    }
                }
                if (errorAdd.GetError(cmbContactCity).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(cmbContactCity));
                    sbError.AppendLine();
                    if (!isFocus)
                    {
                        
                        cmbContactCity.Focus();
                        isFocus = true;
                    }
                }
                if (errorAdd.GetError(txtContactEmail1).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(txtContactEmail1));
                    sbError.AppendLine();
                    if (!isFocus)
                    {
                        txtContactEmail1.Focus();
                        isFocus = true;
                    }
                }
                if (errorAdd.GetError(txtContactEmail2).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(txtContactEmail2));
                    sbError.AppendLine();
                    if (!isFocus)
                    {
                        txtContactEmail2.Focus();
                        isFocus = true;
                    }
                }
                if (errorAdd.GetError(txtContactPinCode).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(txtContactPinCode));
                    sbError.AppendLine();
                    if (!isFocus)
                    {
                        txtContactPinCode.Focus();
                        isFocus = true;
                    }
                }
                return sbError;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool ValidateRequiredTextField(TextBox txt, Label lbl)
        {
            try
            {
                bool isvalid = true;
                errorAdd.SetError(txt, string.Empty);
                bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Trim().Length);
                if (isTextBoxEmpty == true)
                {
                    isvalid = false;
                    errorAdd.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                }
                return isvalid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidateRequiredComboField(ComboBox cmb, Label lbl)
        {
            try
            {
                bool isvalid = true;
                errorAdd.SetError(cmb, string.Empty);
                if (cmb.SelectedIndex < 0)
                {
                    isvalid = false;
                    errorAdd.SetError(cmb, Common.GetMessage("VAL0002", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                }
                return isvalid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidateEmail(TextBox txt, Label lbl)
        {
            try
            {
                errorAdd.SetError(txt, string.Empty);
                bool isEmailValid = Validators.IsValidEmailID(txt.Text.Trim(), true);
                if (!isEmailValid)
                {
                    errorAdd.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 1)));
                }
                return isEmailValid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidatePinCode(TextBox txt, Label lbl)
        {
            if (txt.Text.Trim().Length > 0)
            {
                bool isValidPinCode = Validators.IsValidPinCode(txt.Text.Trim());
                if (!isValidPinCode)
                {
                    errorAdd.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 1)));
                }
            }
        }

        #endregion

        private void txtDistributorId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDistributorId_Validated(null, null);
                txtDistributorId_Leave(null, null);
            }
        }

        private void txtDistributorId_Validated(object sender, EventArgs e)
        {
            try
            {


                if (txtDistributorId.Text.Trim() != string.Empty)
                {
                    int outDisNumber = 0;
                    if (int.TryParse(txtDistributorId.Text.Trim(), out outDisNumber))
                    {
                        Distributor upline = new Distributor();
                        upline.SDistributorId = txtDistributorId.Text.Trim();
                        upline.DistributorId = Convert.ToInt32(txtDistributorId.Text.Trim());
                        string errorMessage = string.Empty;
                        List<Distributor> dist = upline.SearchDistributor(ref errorMessage);
                        if (dist == null)
                        {
                            txtDistributorId.Text = string.Empty;
                            MessageBox.Show(Common.GetMessage("40018"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        else if (dist.Count == 0)
                        {
                            txtDistributorId.Text = string.Empty;
                            MessageBox.Show(Common.GetMessage("40018"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        else if (dist.Count > 1)
                        {
                            using (DistributorPopup dp = new DistributorPopup(dist))
                            {
                                Point pointTree = new Point();
                                pointTree = pnlAddress.PointToScreen(new Point(314, -33));
                                pointTree.Y = pointTree.Y + 25;
                                pointTree.X = pointTree.X + 5;
                                dp.Location = pointTree;
                                if (dp.ShowDialog() == DialogResult.OK)
                                {
                                    txtDistributorId.Text = dp.SelectedDistributor.DistributorId.ToString().Trim();
                                    btnOk.Focus();
                                }
                                else
                                {
                                    txtDistributorId.Text = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            txtDistributorId.Text = dist[0].DistributorId.ToString().Trim();
                            btnOk.Focus();
                        }
                    }
                    else
                    {
                        txtDistributorId.Text = string.Empty;
                    }
                }
                else
                {
                    txtDistributorId.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }


    }
}
