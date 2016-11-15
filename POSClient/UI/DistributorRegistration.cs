using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using POSClient.BusinessObjects;
using CoreComponent.Core.UI;
using CoreComponent.UI;

namespace POSClient.UI
{
    public partial class DistributorRegistration : POSClient.UI.BaseChildForm
    {
        private Boolean IsSaveAvailable = false;
        private string CON_MODULENAME;
        private string m_LocationCode;
        private string m_UserName;
        private DateTime m_distDOB = DateTime.Today;
        private DateTime m_coDistDOB = DateTime.Today;
        private DataTable m_dtCities;
        public DistributorRegistration()
        {
            InitializeComponent();
            try
            {
                if (AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser != null)
                {
                    m_UserName = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;
                }
                InitializeRights();
                BindDropDowns();
                txtPVMonth.Text = Common.GetPVMonth();
                btnSave.Enabled = IsSaveAvailable;
                txtStatus.Text = Common.DistributorStatus.Created.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void InitializeRights()
        {
            m_LocationCode = Common.LocationCode;
            CON_MODULENAME = Common.MODULE_REGISTER_DISTRIBUTOR;
            if (!string.IsNullOrEmpty(m_UserName) && !string.IsNullOrEmpty(CON_MODULENAME))
            {
                IsSaveAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SAVE);
            }
        }

        private void BindDropDowns()
        {
            try
            {
                cmbCountry.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbState.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbZone.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbSubZone.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbArea.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbDistributorTitle.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbCoDistribtorTitle.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbBank.DropDownStyle = ComboBoxStyle.DropDownList;


                DataTable dtCountry = Common.ParameterLookup(Common.ParameterType.Country, new ParameterFilter(Common.LocationCode, 0, 0, 0));
                cmbCountry.DataSource = dtCountry;
                cmbCountry.DisplayMember = "CountryName";
                cmbCountry.ValueMember = "CountryId";

                //Select india as selected country
                if (dtCountry.Rows.Count > 1)
                    cmbCountry.SelectedValue = 1;

                DataTable dtTitles = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("TITLE", 0, 0, 0));
                DataTable dtCoTitles = dtTitles.Copy();

                cmbDistributorTitle.DataSource = dtTitles;
                cmbDistributorTitle.DisplayMember = "keyvalue1";
                cmbDistributorTitle.ValueMember = "keycode1";

                cmbCoDistribtorTitle.DataSource = dtCoTitles;
                cmbCoDistribtorTitle.DisplayMember = "keyvalue1";
                cmbCoDistribtorTitle.ValueMember = "keycode1";

                DataTable dtBanks = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("BANKID", 0, 0, 0));
                cmbBank.DataSource = dtBanks;
                cmbBank.DisplayMember = "keyvalue1";
                cmbBank.ValueMember = "keycode1";


                AddSelectItemInCombo(cmbZone);
                cmbZone.SelectedIndex = 0;

                AddSelectItemInCombo(cmbSubZone);
                cmbSubZone.SelectedIndex = 0;

                AddSelectItemInCombo(cmbArea);
                cmbArea.SelectedIndex = 0;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtUplineNo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                epDistributor.SetError(txtUplineNo, string.Empty);

                // ------Commented by Anubhav..on 27May2010-------------
                //Distributor distributor;
                //if (ValidateDistributorNo(out distributor) && distributor != null)
                //{
                //    txtFirstName.Text = distributor.DistributorFirstName;
                //    txtLastName.Text = distributor.DistributorLastName;
                //    txtUplineAddress.Text = distributor.DistributorAddress;
                //    txtDirectorGroup.Text = distributor.DirectorGroup;
                //    txtDirectorName.Text = distributor.DirectorName;
                //   // txtStatus.Text = distributor.DistributorStatusText;
                //}
                //else
                //{
                //    txtFirstName.Text = string.Empty;
                //    txtLastName.Text = string.Empty;
                //    txtUplineAddress.Text = string.Empty;
                //    txtDirectorGroup.Text = string.Empty;
                //    txtDirectorName.Text = string.Empty;
                //    txtStatus.Text = string.Empty;
                //}

                if (txtUplineNo.Text.Trim() != string.Empty)
                {
                    int outDisNumber = 0;
                    if (int.TryParse(txtUplineNo.Text.Trim(), out outDisNumber))
                    {
                        Distributor upline = new Distributor();
                        upline.SDistributorId = txtUplineNo.Text.Trim();
                        upline.DistributorId = Convert.ToInt32(txtUplineNo.Text.Trim());
                        string errorMessage = string.Empty;
                        List<Distributor> dist = upline.SearchDistributor(ref errorMessage);
                        if (dist == null)
                        {
                            txtFirstName.Text = string.Empty;
                            txtLastName.Text = string.Empty;
                            txtUplineAddress.Text = string.Empty;
                            txtDirectorGroup.Text = string.Empty;
                            txtDirectorName.Text = string.Empty;
                            txtStatus.Text = string.Empty;
                            MessageBox.Show(Common.GetMessage("40018"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        else if (dist.Count == 0)
                        {
                            txtFirstName.Text = string.Empty;
                            txtLastName.Text = string.Empty;
                            txtUplineAddress.Text = string.Empty;
                            txtDirectorGroup.Text = string.Empty;
                            txtDirectorName.Text = string.Empty;
                            txtStatus.Text = string.Empty;
                            MessageBox.Show(Common.GetMessage("40018"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        else if (dist.Count > 1)
                        {
                            using (DistributorPopup dp = new DistributorPopup(dist))
                            {
                                Point pointTree = new Point();
                                pointTree = pnlUpline.PointToScreen(new Point(18, 26));
                                pointTree.Y = pointTree.Y + 25;
                                pointTree.X = pointTree.X + 5;
                                dp.Location = pointTree;
                                if (dp.ShowDialog() == DialogResult.OK)
                                {
                                    txtUplineNo.Text = dp.SelectedDistributor.DistributorId.ToString().Trim();
                                    txtFirstName.Text = dp.SelectedDistributor.DistributorFirstName.ToString().Trim();
                                    txtLastName.Text = dp.SelectedDistributor.DistributorLastName.ToString().Trim();

                                    if (dp.SelectedDistributor.DistributorAddress != null)
                                        txtUplineAddress.Text = dp.SelectedDistributor.DistributorAddress.ToString().Trim();


                                    txtDirectorGroup.Text = dp.SelectedDistributor.DirectorGroup.ToString().Trim();
                                    txtDirectorName.Text = dp.SelectedDistributor.DirectorName.ToString().Trim();
                                    //cmbDistributorTitle.Focus();
                                }
                                else
                                {
                                    txtUplineNo.Text = string.Empty;
                                    txtFirstName.Text = string.Empty;
                                    txtLastName.Text = string.Empty;
                                    txtUplineAddress.Text = string.Empty;
                                    txtDirectorGroup.Text = string.Empty;
                                    txtDirectorName.Text = string.Empty;
                                    txtStatus.Text = string.Empty;
                                    txtUplineNo.Focus();
                                }
                            }
                        }
                        else
                        {
                            txtUplineNo.Text = dist[0].DistributorId.ToString().Trim();
                            txtFirstName.Text = dist[0].DistributorFirstName.ToString().Trim();
                            txtLastName.Text = dist[0].DistributorLastName.ToString().Trim();

                            if (dist[0].DistributorAddress != null)
                                txtUplineAddress.Text = dist[0].DistributorAddress.ToString().Trim();

                            txtDirectorGroup.Text = dist[0].DirectorGroup.ToString().Trim();
                            txtDirectorName.Text = dist[0].DirectorName.ToString().Trim();
                            //cmbDistributorTitle.Focus();
                        }
                    }
                    else
                    {

                        txtFirstName.Text = string.Empty;
                        txtLastName.Text = string.Empty;
                        txtUplineAddress.Text = string.Empty;
                        txtDirectorGroup.Text = string.Empty;
                        txtDirectorName.Text = string.Empty;
                        txtStatus.Text = string.Empty;
                        txtUplineNo.Focus();
                        MessageBox.Show(Common.GetMessage("40018"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {

                    txtFirstName.Text = string.Empty;
                    txtLastName.Text = string.Empty;
                    txtUplineAddress.Text = string.Empty;
                    txtDirectorGroup.Text = string.Empty;
                    txtDirectorName.Text = string.Empty;
                    txtStatus.Text = string.Empty;

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CallValidations();
                string Error = GenerateSaveError();
                if (Error.Trim().Equals(string.Empty))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Save"), Common.GetMessage("10004"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        Distributor d = new Distributor();
                        d.AreaCode = Convert.ToInt32(cmbArea.SelectedValue);
                        d.BankCode = Convert.ToInt32(cmbBank.SelectedValue);
                        d.CoDistributorDOB = txtCoDistDOB.Text.Trim().Length > 0 ? m_coDistDOB.ToString() : Common.DATETIME_NULL.ToString();
                        d.CoDistributorFirstName = txtCDFirstName.Text.Trim();
                        d.CoDistributorLastName = txtCDLastName.Text.Trim();
                        d.CoDistributorTitleId = Convert.ToInt32(cmbCoDistribtorTitle.SelectedValue);
                        d.CoDistributorTitle = cmbDistributorTitle.SelectedText;
                        d.CreatedById = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
                        d.DirectorGroup = txtDirectorGroup.Text.Trim();
                        d.DirectorName = txtDirectorName.Text.Trim();
                        d.DistributorAddress1 = txtAddress1.Text.Trim();
                        d.DistributorAddress2 = txtAddress2.Text.Trim();
                        d.DistributorAddress3 = txtAddress3.Text.Trim();
                        d.DistributorCityCode = GetCityId();
                        d.DistributorCity = txtCity.Text.Trim();
                        d.DistributorCountryCode = Convert.ToInt32(cmbCountry.SelectedValue);
                        d.DistributorDOB = m_distDOB.ToString();
                        d.DistributorEMailID = txtEmail.Text.Trim();
                        d.DistributorFirstName = txtDFirstName.Text.Trim();
                        d.DistributorId = Convert.ToInt32(txtDistributorNo.Text.Trim());
                        d.DistributorLastName = txtDLastName.Text.Trim();
                        d.AccountNumber = txtAccountNumber.Text.Trim();
                        d.DistributorMobNumber = txtMobile.Text.Trim();
                        d.DistributorPANNumber = txtPan.Text.Trim();
                        d.DistributorPinCode = txtPin.Text.Trim();
                        d.DistributorStateCode = Convert.ToInt32(cmbState.SelectedValue);
                        d.DistributorStatus = ((int)Common.DistributorStatus.Created).ToString();
                        d.DistributorTeleNumber = txtPhone.Text.Trim();
                        d.DistributorTitleId = Convert.ToInt32(cmbDistributorTitle.SelectedValue);
                        d.FirstOrderTaken = false;
                        d.KitOrderNo = txtKitOrderNo.Text.Trim();
                        d.LocationId = Common.CurrentLocationId;
                        d.ModifiedById = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
                        d.Remarks = txtRemarks.Text.Trim();
                        d.SubZoneCode = Convert.ToInt32(cmbSubZone.SelectedValue);
                        d.UplineDistributorId = Convert.ToInt32(txtUplineNo.Text.Trim());
                        d.ZoneCode = Convert.ToInt32(cmbZone.SelectedValue);
                        d.DistributorTitle = cmbCoDistribtorTitle.SelectedText;
                        d.DistributorState = cmbState.SelectedText;
                        d.Zone = cmbZone.SelectedText;
                        d.UserName = txtDistributorNo.Text.Trim();
                        d.Password = GetEncryptedRandomString(new Random(), 8);
                        string dbValidationMessage = string.Empty;
                        string dbErrorMessage = string.Empty;
                        string distributorSerialNo = string.Empty;
                        if (d.Register(ref dbValidationMessage, ref dbErrorMessage, ref distributorSerialNo))
                        {
                            txtSerialNo.Text = distributorSerialNo.Trim();
                            DialogResult dr = MessageBox.Show(Common.GetMessage("INF0221", distributorSerialNo), Common.GetMessage("10004"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dr == DialogResult.Yes)
                            {
                                ClearScreen();
                            }
                            else
                            {
                                btnSave.Enabled = false;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(dbErrorMessage))
                            {
                                MessageBox.Show(Common.GetMessage(dbErrorMessage), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show(Common.GetMessage(dbValidationMessage.Split(",".ToCharArray()[0])), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {

                    MessageBox.Show(Error.ToString(), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private string GetEncryptedRandomString(Random rnd, int length)
        {
            string charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            StringBuilder rs = new StringBuilder();

            while (length-- > 0)
                rs.Append(charPool[(int)(rnd.NextDouble() * charPool.Length)]);

            //AuthenticationComponent.BusinessObjects.User objUser = new AuthenticationComponent.BusinessObjects.User();
            // string EncryptedPassword = objUser.EncryptPassword(rs.ToString());
            string EncryptedPassword = rs.ToString();
            return EncryptedPassword;
        }

        private void ClearScreen()
        {
            txtDistributorNo.Text = string.Empty;
            txtKitOrderNo.Text = string.Empty;
            txtKitDate.Text = string.Empty;
            txtUplineNo.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtUplineAddress.Text = string.Empty;
            cmbDistributorTitle.SelectedIndex = 0;
            txtDFirstName.Text = string.Empty;
            txtDLastName.Text = string.Empty;
            //dtpDBirthDate.Value = DateTime.Today;
            cmbCoDistribtorTitle.SelectedIndex = 0;
            txtCDFirstName.Text = string.Empty;
            txtCDLastName.Text = string.Empty;
            //dtpCDBirthDate.Value = DateTime.Today;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtAddress3.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            if (cmbCountry.Items.Count > 0)
                cmbCountry.SelectedValue = 1;
            cmbState.SelectedIndex = 0;
            AddSelectItemInCombo(cmbZone);
            cmbZone.SelectedIndex = 0;
            AddSelectItemInCombo(cmbSubZone);
            cmbSubZone.SelectedIndex = 0;
            AddSelectItemInCombo(cmbArea);
            cmbArea.SelectedIndex = 0;
            txtPin.Text = string.Empty;
            cmbBank.SelectedIndex = 0;
            txtPan.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtDirectorGroup.Text = string.Empty;
            txtDirectorName.Text = string.Empty;
            txtAccountNumber.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCity.Enabled = false;
            m_distDOB = DateTime.Today;
            m_coDistDOB = DateTime.Today;
            m_dtCities = null;
            txtDistDOB.Text = string.Empty;
            txtCoDistDOB.Text = string.Empty;
            txtSerialNo.Text = string.Empty;
            btnSave.Enabled = true;
            epDistributor.Clear();
            txtKitOrderNo.Focus();
        }

        private void AddSelectItemInCombo(ComboBox cmb)
        {
            DataTable dtSelect = new DataTable("SleectHeader");
            DataColumn SelectText = new DataColumn("SelectText", Type.GetType("System.String"));
            DataColumn SelectTextValue = new DataColumn("SelectTextValue", Type.GetType("System.String"));

            dtSelect.Columns.Add(SelectText);
            dtSelect.Columns.Add(SelectTextValue);

            DataRow dRow = dtSelect.NewRow();
            dRow["SelectText"] = "Select";
            dRow["SelectTextValue"] = "-1";
            dtSelect.Rows.Add(dRow);



            cmb.DataSource = dtSelect;
            cmb.ValueMember = "SelectTextValue";
            cmb.DisplayMember = "SelectText";
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCountry.SelectedIndex > 0)
                {
                    DataTable dtStates = Common.ParameterLookup(Common.ParameterType.State, new ParameterFilter("", Convert.ToInt32(cmbCountry.SelectedValue), -1, -1));
                    cmbState.DataSource = dtStates;
                    cmbState.DisplayMember = "StateName";
                    cmbState.ValueMember = "StateId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbState.SelectedIndex > 0)
                {
                    DataTable dtZones = Common.ParameterLookup(Common.ParameterType.Zone, new ParameterFilter("", Convert.ToInt32(cmbState.SelectedValue), 0, 0));
                    cmbZone.DataSource = dtZones;
                    cmbZone.DisplayMember = "OrgHierarchyCode";
                    cmbZone.ValueMember = "OrgHierarchyId";

                    if (cmbZone.Items.Count > 1)
                    {
                        cmbZone.SelectedIndex = 1;
                    }
                    cmbZone.Enabled = false;
                    m_dtCities = Common.ParameterLookup(Common.ParameterType.City, new ParameterFilter("", Convert.ToInt32(cmbState.SelectedValue), -1, -1));

                    m_dtCities.Rows.RemoveAt(0);

                    AutoCompleteStringCollection objCollection = new AutoCompleteStringCollection();
                    foreach (DataRow drow in m_dtCities.Rows)
                    {
                        objCollection.Add(drow["CityName"].ToString());
                    }
                    txtCity.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txtCity.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtCity.AutoCompleteCustomSource = objCollection;
                    txtCity.Enabled = true;
                    txtCity.Text = string.Empty;
                }
                else
                {
                    txtCity.Text = string.Empty;
                    txtCity.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void cmbZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbZone.SelectedIndex > 0)
                {
                    DataTable dtSubZones = Common.ParameterLookup(Common.ParameterType.SubZone, new ParameterFilter("", Convert.ToInt32(cmbZone.SelectedValue), -1, -1));
                    cmbSubZone.DataSource = dtSubZones;
                    cmbSubZone.DisplayMember = "OrgHierarchyCode";
                    cmbSubZone.ValueMember = "OrgHierarchyId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void cmbSubZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSubZone.SelectedIndex > 0)
                {
                    DataTable dtAreas = Common.ParameterLookup(Common.ParameterType.Area, new ParameterFilter("", Convert.ToInt32(cmbSubZone.SelectedValue), -1, -1));
                    cmbArea.DataSource = dtAreas;
                    cmbArea.DisplayMember = "OrgHierarchyCode";
                    cmbArea.ValueMember = "OrgHierarchyId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        #region Validations

        private bool CallValidations()
        {
            try
            {
                //RequiredFields
                epDistributor.Clear();
                // Check for blank Item Code
                //Distributor No.
                if (ValidateRequiredTextField(txtDistributorNo, lblDistributorNo))
                {
                    if (!Validators.IsInt32(txtDistributorNo.Text.Trim()))
                    {
                        //invalid distributor No.
                        epDistributor.SetError(txtDistributorNo, Common.GetMessage("INF0010", lblDistributorNo.Text.Trim()));
                    }
                    else if (txtDistributorNo.Text.Trim().Length != 8)
                    {
                        epDistributor.SetError(txtDistributorNo, Common.GetMessage("INF0010", lblDistributorNo.Text.Trim()));
                    }
                    else if (FindDistributor(Convert.ToInt32(txtDistributorNo.Text)))
                    {
                        epDistributor.SetError(txtDistributorNo, Common.GetMessage("VAL0500", txtDistributorNo.Text.Trim()));
                    }
                    else if (txtDistributorNo.Text.Trim() == txtUplineNo.Text.Trim())
                    {
                        epDistributor.SetError(txtDistributorNo, Common.GetMessage("VAL0605", txtDistributorNo.Text.Trim()));
                    }
                    else
                        epDistributor.SetError(txtDistributorNo, string.Empty);
                }
                // Kit Order No.
                if (ValidateRequiredTextField(txtKitOrderNo, lblKitOrderNo))
                {
                    //valid kit no.

                    if (txtKitDate.Text.Trim().Length <= 0)
                    {
                        //invalid Kit order No.                        
                        epDistributor.SetError(txtKitOrderNo, Common.GetMessage("VAL0501", txtKitOrderNo.Text.ToString()));
                    }
                    else
                    {
                        epDistributor.SetError(txtKitOrderNo, string.Empty);
                    }
                    //else if(Order.Status==(int)Common.OrderStatus.Created || Order.Status==(int)Common.OrderStatus.Cancelled)
                    //{
                    //    epDistributor.SetError(txtKitOrderNo, Common.GetMessage("VAL0504", txtKitOrderNo.Text.ToString()));
                    //}
                    //ValidateRequiredTextField(txtKitDate, lblKitDate);
                }
                if (txtFirstName.Text.Trim().Length <= 0 && txtUplineNo.Text.Trim().Length > 0)
                {
                    //valid Upline No
                    //Distributor distributor;
                    //if (!ValidateDistributorNo(out distributor))
                    {
                        //invalid Upline No
                        epDistributor.SetError(txtUplineNo, Common.GetMessage("VAL0503", txtUplineNo.Text));
                    }
                }
                else if (txtUplineNo.Text.Trim().Length == 0)
                {
                    epDistributor.SetError(txtUplineNo, Common.GetMessage("VAL0001", lblUplineNo.Text));
                }
                else
                    epDistributor.SetError(txtUplineNo, string.Empty);
                //if (ValidateRequiredTextField(txtUplineNo, lblUplineNo))
                //{
                //    //valid Upline No
                //    Distributor distributor;
                //    if (!ValidateDistributorNo(out distributor))
                //    {
                //        //invalid Upline No
                //        epDistributor.SetError(txtUplineNo, Common.GetMessage("VAL0503", txtUplineNo.Text));
                //    }
                //}                

                ValidateRequiredCombo(cmbDistributorTitle, "Distributor Title");
                ValidateRequiredTextField(txtDFirstName, "Distributor First Name");
                //ValidateRequiredTextField(txtDLastName, "Distributor Last Name");

                //ValidateDate(dtpDBirthDate, "Distributor");

                if (m_distDOB > DateTime.Today.AddYears(-18))
                {
                    epDistributor.SetError(txtDistDOB, Common.GetMessage("40005", lblDOB.Text));
                }
                else
                {
                    epDistributor.SetError(txtDistDOB, string.Empty);
                }

                //ValidateRequiredCombo(cmbBank, lblBank.Text);
                if ((cmbBank.SelectedIndex == 0) && (txtAccountNumber.Text.Trim().Length > 0))
                {
                    epDistributor.SetError(cmbBank, Common.GetMessage("VAL0009", lblBank.Text.Trim()));
                }
                else
                {
                    epDistributor.SetError(cmbBank, string.Empty);
                }

                if ((cmbBank.SelectedIndex > 0) && (txtAccountNumber.Text.Trim().Length == 0))
                {
                    epDistributor.SetError(txtAccountNumber, Common.GetMessage("VAL0009", lblAccountNo.Text.Trim()));
                }
                else
                {
                    epDistributor.SetError(txtAccountNumber, string.Empty);
                }
                //ValidateRequiredTextField(txtAccountNumber, lblAccountNo);
                //ValidateRequiredDate();
                if (cmbCoDistribtorTitle.SelectedIndex > 0 || txtCDFirstName.Text.Trim().Length > 0 || txtCDLastName.Text.Trim().Length > 0)
                {
                    ValidateRequiredCombo(cmbCoDistribtorTitle, "CO Distributor Title");
                    ValidateRequiredTextField(txtCDFirstName, "CODistributor First Name");
                    //ValidateRequiredTextField(txtCDLastName, "CO Distributor Last Name");
                    //ValidateDate(dtpCDBirthDate, "Co Distributor");
                    if (txtCoDistDOB.Text.Trim().Length > 0 && m_coDistDOB > DateTime.Today.AddYears(-18))
                    {
                        epDistributor.SetError(txtCoDistDOB, Common.GetMessage("40005", lblDOB.Text));
                    }
                    else
                    {
                        epDistributor.SetError(txtCoDistDOB, string.Empty);
                    }
                }
                if (txtAddress1.Text.Trim().Length > 0)
                {
                    ValidateRequiredTextField(txtAddress1, lblAddress);
                }
                else
                {
                    epDistributor.SetError(txtAddress1, string.Empty);
                }

                ValidateRequiredCombo(cmbCountry, "Country");
                ValidateRequiredCombo(cmbState, "State");
                ValidateRequiredTextField(txtCity, lblCity);
                //ValidateRequiredCombo(cmbCity, "City");
                if (cmbCountry.SelectedValue.ToString() != "4")
                {
                    ValidateRequiredCombo(cmbZone, "Zone");
                    ValidateRequiredCombo(cmbArea, "Area");
                    ValidateRequiredCombo(cmbSubZone, "Sub Zone");
                }
                    if (txtPan.Text.Trim().Length > 0)
                {
                    System.Text.RegularExpressions.Regex objRegex = new System.Text.RegularExpressions.Regex("[A-Z]{5}\\d{4}[A-Z]{1}");
                    if (!objRegex.IsMatch(txtPan.Text))
                    {
                        epDistributor.SetError(txtPan, Common.GetMessage("VAL0009", lblPan.Text.Trim()));
                    }
                    else
                    {
                        epDistributor.SetError(txtPan, string.Empty);
                    }
                }
                else
                {
                    epDistributor.SetError(txtPan, string.Empty);
                }

                if (txtPin.Text.Trim().Length > 0)
                {
                    if (ValidateRequiredTextField(txtPin, lblPin))
                    {
                        if (!Validators.IsValidPinCode(txtPin.Text.Trim()))
                        {
                            //invalid Pin
                            epDistributor.SetError(txtPin, Common.GetMessage("INF0010", lblPin.Text.Trim()));
                        }
                    }
                    else
                    {
                        epDistributor.SetError(txtPin, string.Empty);
                    }
                }

                if (txtEmail.Text.Trim().Length > 0)
                {
                    if (!Validators.IsValidEmailID(txtEmail.Text.Trim(), true))
                    {
                        //invalid email
                        epDistributor.SetError(txtEmail, Common.GetMessage("INF0010", lblEmail.Text.Trim()));
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateSaveError()
        {
            try
            {
                bool focus = false;
                StringBuilder sbError = new StringBuilder();

                //Kit Order
                if (epDistributor.GetError(txtKitOrderNo).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtKitOrderNo));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtKitOrderNo.Focus();
                        focus = true;
                    }
                }

                //Distributor No
                if (epDistributor.GetError(txtDistributorNo).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtDistributorNo));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtDistributorNo.Focus();
                        focus = true;
                    }
                }

                //Kit Date
                if (epDistributor.GetError(txtKitDate).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtKitDate));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtKitDate.Focus();
                        focus = true;
                    }
                }
                //Upline No
                if (epDistributor.GetError(txtUplineNo).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtUplineNo));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtUplineNo.Focus();
                        focus = true;
                    }
                }
                // Distributor Title
                if (epDistributor.GetError(cmbDistributorTitle).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(cmbDistributorTitle));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        cmbDistributorTitle.Focus();
                        focus = true;
                    }
                }
                //First Name
                if (epDistributor.GetError(txtDFirstName).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtDFirstName));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtDFirstName.Focus();
                        focus = true;
                    }
                }
                //Last Name
                //if (epDistributor.GetError(txtDLastName).Trim().Length > 0)
                //{
                //    sbError.Append(epDistributor.GetError(txtDLastName));
                //    sbError.AppendLine();
                //    if (!focus)
                //    {
                //        txtDLastName.Focus();
                //        focus = true;
                //    }
                //}
                //Date of Birth
                if (epDistributor.GetError(txtDistDOB).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtDistDOB));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtDistDOB.Focus();
                        focus = true;
                    }
                }
                // CO Distributor Title
                if (epDistributor.GetError(cmbCoDistribtorTitle).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(cmbCoDistribtorTitle));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        cmbCoDistribtorTitle.Focus();
                        focus = true;
                    }
                }
                // FIrst Name
                if (epDistributor.GetError(txtCDFirstName).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtCDFirstName));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtCDFirstName.Focus();
                        focus = true;
                    }
                }
                //Last Name
                //if (epDistributor.GetError(txtCDLastName).Trim().Length > 0)
                //{
                //    sbError.Append(epDistributor.GetError(txtCDLastName));
                //    sbError.AppendLine();
                //    if (!focus)
                //    {
                //        txtCDLastName.Focus();
                //        focus = true;
                //    }
                //}
                // Date of Birth
                if (epDistributor.GetError(txtCoDistDOB).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtCoDistDOB));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtCoDistDOB.Focus();
                        focus = true;
                    }
                }

                // Bank Name
                if (epDistributor.GetError(cmbBank).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(cmbBank));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        cmbBank.Focus();
                        focus = true;
                    }
                }

                // Account No.
                if (epDistributor.GetError(txtAccountNumber).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtAccountNumber));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtAccountNumber.Focus();
                        focus = true;
                    }
                }

                //Address

                if (epDistributor.GetError(txtAddress1).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtAddress1));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtAddress1.Focus();
                        focus = true;
                    }
                }
                //Country
                if (epDistributor.GetError(cmbCountry).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(cmbCountry));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        cmbCountry.Focus();
                        focus = true;
                    }
                }
                //State
                if (epDistributor.GetError(cmbState).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(cmbState));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        cmbState.Focus();
                        focus = true;
                    }
                }
                //City
                if (epDistributor.GetError(txtCity).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtCity));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtCity.Focus();
                        focus = true;
                    }
                }
                //Zone
                if (epDistributor.GetError(cmbZone).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(cmbZone));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        cmbZone.Focus();
                        focus = true;
                    }
                }
                //SubZone
                if (epDistributor.GetError(cmbSubZone).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(cmbSubZone));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        cmbSubZone.Focus();
                        focus = true;
                    }
                }
                //Area
                if (epDistributor.GetError(cmbArea).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(cmbArea));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        cmbArea.Focus();
                        focus = true;
                    }
                }

                //Email
                if (epDistributor.GetError(txtEmail).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtEmail));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtEmail.Focus();
                        focus = true;
                    }
                }

                //Pin Code

                if (epDistributor.GetError(txtPin).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtPin));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtPin.Focus();
                        focus = true;
                    }
                }

                //Pan NO.
                if (epDistributor.GetError(txtPan).Trim().Length > 0)
                {
                    sbError.Append(epDistributor.GetError(txtPan));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtPan.Focus();
                        focus = true;
                    }
                }

                sbError = Common.ReturnErrorMessage(sbError);
                return sbError.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtKitOrderNo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                //epDistributor.SetError(txtKitOrderNo, string.Empty);
                //CO Order;
                //if (ValidateKitOrderNo(out Order) && Order != null)
                //{
                //    txtKitDate.Text = Order.DisplayOrderDate;
                //}
                //else
                //{
                //    MessageBox.Show(Common.GetMessage("VAL0510"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtKitDate.Text = string.Empty;
                //}

                if (txtKitOrderNo.Text.Trim() != string.Empty)
                {

                    if (txtKitOrderNo.Text.Trim().Length > 0)
                    {
                        Distributor upline = new Distributor();
                        CO order = new CO();
                        order.CustomerOrderNo = txtKitOrderNo.Text.Trim();
                        order.OrderType = (int)Common.OrderType.KitOrder;
                        order.Status = -1;
                        string errorMessage = string.Empty;
                        List<CO> listCO = order.Search(ref errorMessage);
                        if (listCO != null)
                        {
                            var query = from p in listCO where p.UsedForRegistration == false && (p.Status == 3 || p.Status == 4) select p;
                            List<CO> orders = (List<CO>)query.ToList();
                            if (orders == null)
                            {
                                txtKitDate.Text = string.Empty;
                                MessageBox.Show(Common.GetMessage("VAL0510"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (orders.Count == 0)
                            {

                                txtKitDate.Text = string.Empty;
                                MessageBox.Show(Common.GetMessage("VAL0510"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (orders.Count > 1)
                            {
                                using (OrderPopup op = new OrderPopup(orders))
                                {
                                    Point pointTree = new Point();
                                    pointTree = pnlKitDetails.PointToScreen(new Point(134, 24));
                                    pointTree.Y = pointTree.Y + 25;
                                    pointTree.X = pointTree.X + 5;
                                    op.Location = pointTree;
                                    if (op.ShowDialog() == DialogResult.OK)
                                    {
                                        txtKitOrderNo.Text = op.SelectedOrder.CustomerOrderNo.Trim();
                                        txtKitDate.Text = (Convert.ToDateTime(op.SelectedOrder.OrderDate)).ToString(Common.DTP_DATE_FORMAT);
                                        //txtUplineNo.Focus();
                                    }
                                    else
                                    {
                                        txtKitOrderNo.Text = string.Empty;
                                        txtKitDate.Text = string.Empty;
                                        txtKitOrderNo.Focus();
                                    }
                                }
                            }
                            else
                            {
                                txtKitOrderNo.Text = orders[0].CustomerOrderNo.Trim();
                                txtKitDate.Text = (Convert.ToDateTime(orders[0].OrderDate.Trim())).ToString(Common.DTP_DATE_FORMAT);
                                //txtUplineNo.Focus();
                            }
                        }
                        else
                        {
                            txtKitDate.Text = string.Empty;
                        }
                    }
                    else
                    {
                        txtKitDate.Text = string.Empty;
                    }
                }
                else
                {
                    txtKitDate.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private bool ValidateKitOrderNo(out CO ReturnOrder)
        {
            try
            {
                ReturnOrder = null;
                if (Validators.CheckForEmptyString(txtKitOrderNo.Text.Trim().Length))
                    return false;
                // Check valid kit no 
                CO order = new CO();
                order.CustomerOrderNo = txtKitOrderNo.Text.Trim();
                order.OrderType = (int)Common.OrderType.KitOrder;
                order.Status = -1;
                string errorMessage = string.Empty;
                List<CO> listCO = order.Search(ref errorMessage);
                if (listCO != null && listCO.Count > 0)
                {
                    var query = from p in listCO where p.UsedForRegistration == false && (p.Status == 3 || p.Status == 4) select p;
                    List<CO> orders = (List<CO>)query.ToList();
                    if (orders != null && orders.Count > 0)
                    {
                        ReturnOrder = orders[0];
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidateDistributorNo(out Distributor ReturnDistributor)
        {
            try
            {
                ReturnDistributor = null;
                Distributor distributor = new Distributor();
                if (Validators.IsInt32(txtUplineNo.Text.Trim()))
                {
                    distributor.DistributorId = Convert.ToInt32(txtUplineNo.Text.Trim());
                    distributor.SDistributorId = txtUplineNo.Text.Trim();
                    string errorMessage = string.Empty;
                    List<Distributor> lstDistributor = distributor.SearchDistributor(ref errorMessage);
                    if (lstDistributor != null && lstDistributor.Count > 0)
                    {
                        ReturnDistributor = lstDistributor[0];
                        return true;
                    }
                }
                return false;
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
                bool isTextBoxEmpty = Validators.CheckForEmptyString(txt.Text.Trim().Length);
                if (isTextBoxEmpty == true)
                {
                    epDistributor.SetError(txt, Common.GetMessage("VAL0001", lbl.Text.Trim()));
                }
                return !isTextBoxEmpty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidateRequiredTextField(TextBox txt, string lbl)
        {
            try
            {
                bool isTextBoxEmpty = Validators.CheckForEmptyString(txt.Text.Length);
                if (isTextBoxEmpty == true)
                {
                    epDistributor.SetError(txt, Common.GetMessage("VAL0001", lbl));
                }
                return !isTextBoxEmpty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidateRequiredCombo(ComboBox cmb, string lbl)
        {
            try
            {
                bool isComboNotSelected = Validators.CheckForSelectedValue(cmb.SelectedIndex);
                if (isComboNotSelected == true)
                {
                    //selected Value Message
                    epDistributor.SetError(cmb, Common.GetMessage("VAL0002", lbl.Trim()));
                }
                return !isComboNotSelected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidateDate(DateTimePicker dtp, string lbl)
        {
            if (dtp.Value > DateTime.Today.AddYears(-18))
            {
                epDistributor.SetError(dtp, Common.GetMessage("40005", lbl));
                return false;
            }
            return true;
        }

        #endregion

        private void txtKitOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtKitOrderNo_Validating(null, null);
            }
        }

        private void txtUplineNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUplineNo_Validating(null, null);
            }
        }

        private void txtCity_Validated(object sender, EventArgs e)
        {
            if (txtCity.Text.Trim().Length > 0)
            {
                if (!ValidateCity())
                {
                    MessageBox.Show(Common.GetMessage("VAL0603"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCity.Text = string.Empty;
                    txtCity.Focus();
                }
            }
        }

        private bool ValidateCity()
        {
            if (m_dtCities != null && m_dtCities.Rows.Count > 0)
            {
                foreach (DataRow drow in m_dtCities.Rows)
                {
                    if (txtCity.Text.Trim().ToUpper() == drow["CityName"].ToString().ToUpper())
                        return true;
                }
            }
            return false;
        }

        private int GetCityId()
        {
            if (m_dtCities != null && m_dtCities.Rows.Count > 0)
            {
                foreach (DataRow drow in m_dtCities.Rows)
                {
                    if (txtCity.Text.Trim().ToUpper() == drow["CityName"].ToString().ToUpper())
                        return Convert.ToInt32(drow["CityId"]);
                }
            }
            return Common.INT_DBNULL;
        }

        private void txtDistDOB_Leave(object sender, EventArgs e)
        {
            try
            {
                m_distDOB = Common.FormatDateTextBox(txtDistDOB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void txtDistDOB_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    txtDistDOB_Leave(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void txtCoDistDOB_Leave(object sender, EventArgs e)
        {
            try
            {
                m_coDistDOB = Common.FormatDateTextBox(txtCoDistDOB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void txtCoDistDOB_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    txtCoDistDOB_Leave(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        void ValidateDistributorId(int dis)
        {
            if (ValidateRequiredTextField(txtDistributorNo, lblDistributorNo))
            {
                if (!Validators.IsInt32(txtDistributorNo.Text.Trim()))
                {
                    //invalid distributor No.
                    epDistributor.SetError(txtDistributorNo, Common.GetMessage("INF0010", lblDistributorNo.Text.Trim()));
                }
                else
                {
                    if (FindDistributor(dis))
                    {
                        epDistributor.SetError(txtDistributorNo, Common.GetMessage("VAL0500", txtDistributorNo.Text.Trim()));
                        MessageBox.Show(Common.GetMessage("VAL0500", txtDistributorNo.Text.Trim()), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                        epDistributor.SetError(txtDistributorNo, string.Empty);
                }
            }
        }

        private bool FindDistributor(int dis)
        {
            DataTable dtDistributor = Common.ParameterLookup(Common.ParameterType.Distributor, new ParameterFilter(string.Empty, dis, 0, 0));
            if (dtDistributor != null && dtDistributor.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void txtDistributorNo_Validated(object sender, EventArgs e)
        {
            try
            {
                int dis;
                if (txtDistributorNo.Text.Length > 0 && txtDistributorNo.Text.Length == 8 && Int32.TryParse(txtDistributorNo.Text.Trim(), out dis))
                {
                    epDistributor.SetError(txtDistributorNo, string.Empty);
                    ValidateDistributorId(dis);
                }
                else if (txtDistributorNo.Text.Trim().Length > 0)
                    epDistributor.SetError(txtDistributorNo, Common.GetMessage("INF0010", lblDistributorNo.Text.Trim()));
                else
                    epDistributor.SetError(txtDistributorNo, string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Common.LogException(ex);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ClearScreen();
            }

            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void DistributorRegistration_Load(object sender, EventArgs e)
        {
            btnCancel.CausesValidation = false;
            btnReset.CausesValidation = false;
        }

        private void DistributorRegistration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ((this.ActiveControl is TextBox) || (this.ActiveControl is ComboBox)))
            {
                SendKeys.Send("{TAB}");
            }
        }


        //private void SelectNext(Control c)
        //{
        //    if (c.Focused)
        //    {
        //        Control skipControls = this.GetNextControl(c, true);
        //        do
        //        {
        //            while (skipControls is Button ||
        //                skipControls is GroupBox ||
        //                skipControls is Panel ||
        //                skipControls is Label || 
        //                (skipControls is TextBox && ((skipControls as TextBox).ReadOnly == true || (skipControls as TextBox).Enabled == false))) 
        //            skipControls = this.GetNextControl(skipControls, true);
        //            if (null != skipControls)
        //            {
        //                skipControls.Select();
        //                break;
        //            }
        //            else
        //                skipControls = this.Controls[0];
        //        } while (true);
        //        return;
        //    }
        //    foreach (Control childControl in c.Controls)
        //    {
        //        SelectNext(childControl);
        //    }
        //}
        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Enter:
        //            SelectNext(this);
        //            break;
        //        default:
        //            base.OnKeyDown(e);
        //            break;
        //    }
        //}        

    }
}
