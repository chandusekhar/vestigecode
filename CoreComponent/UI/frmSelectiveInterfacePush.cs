using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//vinculum-framework namespace(s)
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects;


namespace CoreComponent.UI
{
    public partial class frmSelectiveInterfacePush : CoreComponent.Core.UI.BlankTemplate
    {

        #region Variables

        private const string m_uspDataSearch = "usp_getSelectiveInterfacePushData";
        private const string m_uspDataSave = "usp_Interface_Audit";
        private const int m_userId = -2;

        private bool m_boolSuspendEventHandler = false;
        private DataTable m_dsInterfaceKeysMarkup = null;
        private StringBuilder m_sbErrMsgs = null;

        private List<SelectiveInterfacePush> m_lstUnprocessedRecords = null;

        private ScreenState m_scrstState = ScreenState.None;

        private const string CON_SEQNUMBER = "SEQNUMBER";
        private const string CON_STKLEDGER = "STKLEDGER";
        private const string CON_STKDAILY = "STKDAILY";
        private const string CON_STKMONTH = "STKMONTH";
        private const string CON_STKYEAR = "STKYEAR";
        private const string CON_INSERT = "I";

        private System.Collections.ArrayList ar = null;

        #endregion

        #region Enum

        private enum ScreenState
        {
            None = 0,
            Add = 1,
            View = 2
        }

        private enum PushType
        {
            All = 1,
            Selective = 2
        }

        #endregion

        #region Constructor

        public frmSelectiveInterfacePush()
        {
            try
            {
                InitializeComponent();

                dgvUnprocessedRecords.AutoGenerateColumns = false;
                dgvUnprocessedRecords = Common.GetDataGridViewColumns(dgvUnprocessedRecords, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

                FillControlData();
                ShowHidePUC(false);
                FillUnprocessedRecords();

                if (dgvUnprocessedRecords.Rows.Count > 0)
                {
                    m_scrstState = ScreenState.View;
                }
                else
                {
                    m_scrstState = ScreenState.None;
                }
                ManageScreenState();

                lblPageTitle.Text = "Selective Interface Push";
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region Events

        private void cmbLocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox ctrl = (ComboBox)sender;
                if (!m_boolSuspendEventHandler)
                {
                    FillLocationData("LOCVAL", ctrl.SelectedValue.ToString());
                    if (Convert.ToInt32(ctrl.SelectedValue) == (int)Common.LocationConfigId.BO)
                    {
                        ShowHidePUC(true);
                    }
                    else
                    {
                        ShowHidePUC(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbLocCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox ctrl = (ComboBox)sender;
                if (!m_boolSuspendEventHandler)
                {
                    if (Convert.ToInt32(cmbLocType.SelectedValue) == (int)Common.LocationConfigId.BO)
                    {
                        FillLocationData("PUC", cmbLocType.SelectedValue.ToString(), ctrl.SelectedValue.ToString());
                        ShowHidePUC(true);
                    }
                    else
                    {
                        ShowHidePUC(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ReflectInterfaceKeys((ComboBox)sender);

                //ComboBox ctrl = (ComboBox)sender;
                //if (!m_boolSuspendEventHandler)
                //{
                //    ClearInterfaceKeys();

                //    if (cmbPushType.Items.Count > 0)
                //    {
                //        if (Convert.ToInt32(cmbPushType.SelectedValue) == (int)PushType.All)
                //        {
                //            EnableDisableInterfaceKeys(new bool[] { false, false, false, false, false });
                //        }
                //        else if (Convert.ToInt32(cmbPushType.SelectedValue) == (int)PushType.Selective)
                //        {
                //            ManageInterfaceKeys(Convert.ToInt32(ctrl.SelectedValue));
                //        }
                //    }
                //    else
                //    {
                //        ManageInterfaceKeys(0);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbPushType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox ctrl = (ComboBox)sender;
                if (!m_boolSuspendEventHandler)
                {
                    if (Convert.ToInt32(ctrl.SelectedValue) == 1)
                    {
                        ClearInterfaceKeys();
                        EnableDisableInterfaceKeys(new bool[] { false, false, false, false, false });
                    }
                    else if (Convert.ToInt32(ctrl.SelectedValue) == 2)
                    {
                        ReflectInterfaceKeys(cmbInterfaceProcess);
                        //cmbInterface_SelectedIndexChanged(cmbInterfaceProcess, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateRecord())
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010","Save"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        string interfaceId = string.Empty;
                        string locCode = string.Empty;
                        string key1 = Common.DBNULL_VAL;
                        string key2 = Common.DBNULL_VAL;
                        string key3 = Common.DBNULL_VAL;
                        string key4 = Common.DBNULL_VAL;
                        string key5 = Common.DBNULL_VAL;
                        string action = string.Empty;

                        interfaceId = ((DataRowView)cmbInterfaceProcess.SelectedItem)["INTERFACEVAL"].ToString();

                        if (Convert.ToInt32(cmbPushType.SelectedValue) == 2)
                        {
                            key1 = txtID1.Text;
                            key2 = txtID2.Enabled ? txtID2.Text : key2;
                            key3 = txtID3.Enabled ? txtID3.Text : key3;
                            key4 = txtID4.Enabled ? txtID4.Text : key4;
                            key5 = txtID5.Enabled ? txtID5.Text : key5;
                        }

                        if (Convert.ToInt32(cmbLocType.SelectedValue) == 2)
                        {
                            locCode = ((DataRowView)cmbLocCode.SelectedItem)["LOCATIONCODE"].ToString();
                        }
                        else if (Convert.ToInt32(cmbLocType.SelectedValue) == 3)
                        {
                            if (cmbPUC.SelectedValue != null)
                            {
                                if (Convert.ToInt32(cmbPUC.SelectedValue) > -1)
                                {
                                    locCode = ((DataRowView)cmbPUC.SelectedItem)["LOCATIONCODE"].ToString();
                                }
                                else
                                {
                                    locCode = ((DataRowView)cmbLocCode.SelectedItem)["LOCATIONCODE"].ToString();
                                }
                            }
                            else
                            {
                                locCode = ((DataRowView)cmbLocCode.SelectedItem)["LOCCODE"].ToString();
                            }
                        }

                        DataTable dtTemp = ((DataTable)cmbAction.DataSource).Copy();
                        dtTemp.DefaultView.RowFilter = "ActionVal='" + Convert.ToInt32(cmbAction.SelectedValue) + "'";
                        action = dtTemp.DefaultView.ToTable().Rows[0]["ActionCodeId"].ToString();
                        //action = ((DataRowView)cmbAction.SelectedValue)["ActionVal"].ToString();

                        SaveUnprocessedData(interfaceId, locCode, key1, key2, key3, key4, key5, action, Common.INTERFACE_USERID_VAL);
                                                                //cmbAction.SelectedValue.ToString(), Common.INTERFACE_USERID_VAL);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resetresult = MessageBox.Show(Common.GetMessage("5006"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resetresult == DialogResult.Yes)
                {
                    errprovValidate.Clear();
                    m_sbErrMsgs = null;
                    FillUnprocessedRecords();
                    dgvUnprocessedRecords.ClearSelection();
                    m_scrstState = ScreenState.Add;
                    ManageScreenState();

                    cmbLocType.Focus();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUnprocessedRecords_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_boolSuspendEventHandler)
                {
                    if (dgvUnprocessedRecords.SelectedRows.Count > 0)
                    {
                        errprovValidate.Clear();
                        ReflectInterfaceRecord(dgvUnprocessedRecords.SelectedRows[0].Index);
                        m_scrstState = ScreenState.View;
                        ManageScreenState();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSelectiveInterfacePush_Load(object sender, EventArgs e)
        {
            ar = new System.Collections.ArrayList();
            ar.Add("STKLEDGER");
            ar.Add("PUC");           
        }

        #endregion


        #region Methods

        private void FillControlData()
        {
            DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter(string.Empty, 0, 0, 1));
            dtLocations.DefaultView.RowFilter = "LOCTYPEVAL IN (-1,2,3)";
            m_boolSuspendEventHandler = true;
            cmbLocType.DataSource = dtLocations.DefaultView.ToTable().Copy();
            cmbLocType.DisplayMember = "LOCTYPECODE";
            cmbLocType.ValueMember = "LOCTYPEVAL";
            m_boolSuspendEventHandler = false;

            DataTable dtInterfaceProcesses = Common.ParameterLookup(Common.ParameterType.InterfaceProcess, new ParameterFilter(string.Empty, 0, 0, 0));
            m_boolSuspendEventHandler = true;
            cmbInterfaceProcess.DataSource = null;
            cmbInterfaceProcess.DataSource = dtInterfaceProcesses;
            cmbInterfaceProcess.DisplayMember = "INTERFACEPROCESS";
            cmbInterfaceProcess.ValueMember = "INTERFACEID";
            m_boolSuspendEventHandler = false;
            m_dsInterfaceKeysMarkup = new DataTable();
            m_dsInterfaceKeysMarkup = dtInterfaceProcesses.Copy();

            DataTable dtInterfacePushTypes = Common.ParameterLookup(Common.ParameterType.InterfacePushTypes, new ParameterFilter(string.Empty, 0, 0, 0));
            m_boolSuspendEventHandler = true;
            cmbPushType.DataSource = null;
            cmbPushType.DataSource = dtInterfacePushTypes;
            cmbPushType.DisplayMember = "PUSHCODE";
            cmbPushType.ValueMember = "PUSHVAL";
            m_boolSuspendEventHandler = false;

            DataTable dtInterfaceAction = Common.ParameterLookup(Common.ParameterType.InterfaceAction, new ParameterFilter(string.Empty, 0, 0, 0));
            cmbAction.DataSource = null;
            cmbAction.DataSource = dtInterfaceAction;
            cmbAction.DisplayMember = "ACTIONCODE";
            cmbAction.ValueMember = "ACTIONVAL";
        }

        private void FillLocationData(params string[] locationParams)
        {
            string dataToFill = string.Empty;
            string locType = string.Empty;
            string locCode = Common.INT_DBNULL.ToString();

            if (locationParams.Length > 0)
            {
                dataToFill = locationParams[0];
            }
            if (locationParams.Length > 1)
            {
                locType = locationParams[1];
            }
            if (locationParams.Length > 2)
            {
                locCode = locationParams[2];
            }

            m_boolSuspendEventHandler = true;
            switch (dataToFill)
            {
                case "LOCVAL":
                    {
                        DataTable dtBOorWHGs = Common.ParameterLookup(Common.ParameterType.WhgBoPucLocations, new ParameterFilter(string.Empty, Convert.ToInt32(locType), Convert.ToInt32(locCode), 0));
                        cmbLocCode.DataSource = null;
                        cmbLocCode.DataSource = dtBOorWHGs;
                        cmbLocCode.DisplayMember = "LOCCODE";
                        cmbLocCode.ValueMember = "LOCVAL";
                        
                        DataTable dtLocations = null;
                        switch (Convert.ToInt32(locType))
                        {
                            case (int)Common.LocationConfigId.WH:
                                {
                                    dtLocations = Common.ParameterLookup(Common.ParameterType.WhgBoPucLocations, new ParameterFilter(string.Empty, Common.INT_DBNULL, (int)Common.LocationConfigId.PC, 0));
                                    cmbPUC.DataSource = null;
                                    cmbPUC.DataSource = dtLocations;
                                    cmbPUC.DisplayMember = "PUCCODE";
                                    cmbPUC.ValueMember = "LOCVAL";
                                }
                                break;

                            case (int)Common.LocationConfigId.BO:
                                {
                                    dtLocations = Common.ParameterLookup(Common.ParameterType.WhgBoPucLocations, new ParameterFilter(string.Empty, (int)Common.LocationConfigId.PC, Convert.ToInt32(locCode), 0));
                                    cmbPUC.DataSource = null;
                                    cmbPUC.DataSource = dtLocations;
                                    cmbPUC.DisplayMember = "PUCCODE";
                                    cmbPUC.ValueMember = "LOCVAL";                                    
                                }
                                break;

                            default:
                                {
                                    dtLocations = Common.ParameterLookup(Common.ParameterType.WhgBoPucLocations, new ParameterFilter(string.Empty, Common.INT_DBNULL, (int)Common.LocationConfigId.BO, 0));
                                    cmbLocCode.DataSource = null;
                                    cmbLocCode.DataSource = dtLocations;
                                    cmbLocCode.DisplayMember = "LOCCODE";
                                    cmbLocCode.ValueMember = "LOCVAL";

                                    dtLocations = Common.ParameterLookup(Common.ParameterType.WhgBoPucLocations, new ParameterFilter(string.Empty, Common.INT_DBNULL, (int)Common.LocationConfigId.PC, 0));
                                    cmbPUC.DataSource = null;
                                    cmbPUC.DataSource = dtLocations;
                                    cmbPUC.DisplayMember = "PUCCODE";
                                    cmbPUC.ValueMember = "LOCVAL";
                                }
                                break;
                        }
                    }
                    break;

                case "PUC":
                    {
                        DataTable dtPUCs = Common.ParameterLookup(Common.ParameterType.WhgBoPucLocations, new ParameterFilter(string.Empty, (int)Common.LocationConfigId.PC, Convert.ToInt32(locCode), 0));
                        cmbPUC.DataSource = null;
                        cmbPUC.DataSource = dtPUCs;
                        cmbPUC.DisplayMember = "PUCCODE";
                        cmbPUC.ValueMember = "LOCVAL";
                    }
                    break;
            }
            m_boolSuspendEventHandler = false;
        }

        private void FillUnprocessedRecords()
        {
            string errMsg = string.Empty;
            SelectiveInterfacePush objTemp = new SelectiveInterfacePush();
            DataTable dtSource = objTemp.SearchUnprocessedRecords(ref errMsg);

            if (string.IsNullOrEmpty(errMsg))
            {
                m_lstUnprocessedRecords = new List<SelectiveInterfacePush>();

                foreach (DataRow drRow in dtSource.Rows)
                {
                    SelectiveInterfacePush objUnprocessedRecord = new SelectiveInterfacePush();
                    objUnprocessedRecord.InterfaceProcessId = drRow["INTERFACEPROCESSCODEID"].ToString();
                    objUnprocessedRecord.InterfaceProcessCode = drRow["INTERFACEPROCESSCODE"].ToString();
                    objUnprocessedRecord.InterfaceProcessName = drRow["INTERFACEPROCESSNAME"].ToString();
                    objUnprocessedRecord.LocationTypeId = Convert.ToInt32(drRow["LocationTypeId"]);
                    objUnprocessedRecord.LocationCodeId = Convert.ToInt32(drRow["LocationCodeId"]);
                    objUnprocessedRecord.LeafLocationId = Convert.ToInt32(drRow["LeafLocationCodeId"]);
                    objUnprocessedRecord.LeafLocationCode = drRow["LeafLocationCode"].ToString();
                    objUnprocessedRecord.Key1 = drRow["Key1"].ToString();
                    objUnprocessedRecord.Key2 = drRow["Key2"].ToString();
                    objUnprocessedRecord.Key3 = drRow["Key3"].ToString();
                    objUnprocessedRecord.Key4 = drRow["Key4"].ToString();
                    objUnprocessedRecord.Key5 = drRow["Key5"].ToString();
                    objUnprocessedRecord.ActionId = drRow["ActionCodeId"].ToString();
                    objUnprocessedRecord.ActionCode = drRow["ActionCode"].ToString();
                    objUnprocessedRecord.InsertedById = Convert.ToInt32(drRow["InsertedById"]);
                    objUnprocessedRecord.InsertedBy = drRow["InsertedBy"].ToString();
                    objUnprocessedRecord.InsertedDate = Convert.ToDateTime(drRow["InsertedDate"].ToString());

                    m_lstUnprocessedRecords.Add(objUnprocessedRecord);
                }

                dgvUnprocessedRecords.DataSource = null;
                dgvUnprocessedRecords.DataSource = m_lstUnprocessedRecords;
            }
        }

        private void ReflectInterfaceKeys(ComboBox cmbSender)
        {
            if (!m_boolSuspendEventHandler)
            {
                ClearInterfaceKeys();

                if (cmbPushType.Items.Count > 0)
                {
                    if (Convert.ToInt32(cmbPushType.SelectedValue) == (int)PushType.All)
                    {
                        EnableDisableInterfaceKeys(new bool[] { false, false, false, false, false });
                    }
                    else if (Convert.ToInt32(cmbPushType.SelectedValue) == (int)PushType.Selective)
                    {
                        ManageInterfaceKeys(Convert.ToInt32(cmbSender.SelectedValue));
                    }
                }
                else
                {
                    ManageInterfaceKeys(0);
                }
            }
        }

        private void ManageScreenState()
        {
            switch (m_scrstState)
            {
                case ScreenState.None:
                case ScreenState.Add:
                    {
                        ResetLocation();
                        ResetOthers();
                        ManageInputControls(true, false);
                        ResetInterface(true, true);
                        btnSave.Enabled = true;
                        btnReset.Enabled = true;
                    }
                    break;

                case ScreenState.View:
                    {
                        ManageInputControls(false, true);
                        ManageInterfaceKeys(0);
                        btnSave.Enabled = false;
                        btnReset.Enabled = true;
                    }
                    break;
            }
        }

        private void ManageInputControls(bool isEnabled, bool isReadOnly)
        {
            cmbLocType.Enabled = isEnabled;
            cmbLocCode.Enabled = isEnabled;
            cmbPUC.Enabled = isEnabled;
            cmbPushType.Enabled = isEnabled;
            cmbAction.Enabled = isEnabled;
            cmbInterfaceProcess.Enabled = isEnabled;
        }

        private void ShowHidePUC(bool isVisible)
        {
            lblPUC.Visible = isVisible;
            cmbPUC.Visible = isVisible;
        }

        private void ManageInterfaceKeys(int interfaceValue)
        {
            m_dsInterfaceKeysMarkup.DefaultView.RowFilter = "INTERFACEID=" + interfaceValue;
            int keysRequired = 0;
            if (m_dsInterfaceKeysMarkup.DefaultView.Count > 0)
            {
                keysRequired = Convert.ToInt32(m_dsInterfaceKeysMarkup.DefaultView[0]["INTERFACEKEYS"].ToString());
            }

            switch (keysRequired)
            {
                case 1:
                    {
                        EnableDisableInterfaceKeys(new bool[] { true, false, false, false, false });
                    }
                    break;

                case 2:
                    {
                        EnableDisableInterfaceKeys(new bool[] { true, true, false, false, false });
                    }
                    break;

                case 3:
                    {
                        EnableDisableInterfaceKeys(new bool[] { true, true, true, false, false });
                    }
                    break;

                case 4:
                    {
                        EnableDisableInterfaceKeys(new bool[] { true, true, true, true, false });
                    }
                    break;

                case 5:
                    {
                        EnableDisableInterfaceKeys(new bool[] { true, true, true, true, true });
                    }
                    break;

                default:
                    {
                        EnableDisableInterfaceKeys(new bool[] { false, false, false, false, false });
                    }
                    break;
            }
        }

        private void EnableDisableInterfaceKeys(bool[] keysParams)
        {
            txtID1.Enabled = keysParams[0];
            txtID2.Enabled = keysParams[1];
            txtID3.Enabled = keysParams[2];
            txtID4.Enabled = keysParams[3];
            txtID5.Enabled = keysParams[4];
        }

        private void ClearInterfaceKeys()
        {
            txtID1.Text = string.Empty;
            txtID2.Text = string.Empty;
            txtID3.Text = string.Empty;
            txtID4.Text = string.Empty;
            txtID5.Text = string.Empty;
        }

        private void ReflectInterfaceRecord(int recordIndex)
        {
            SelectiveInterfacePush objInterfacePush = m_lstUnprocessedRecords[recordIndex];

            cmbLocType.SelectedValue = objInterfacePush.LocationTypeId;
            cmbLocCode.SelectedValue = objInterfacePush.LocationCodeId;
            if (objInterfacePush.LeafLocationId != objInterfacePush.LocationCodeId)
            {
                cmbPUC.SelectedValue = objInterfacePush.LeafLocationId;
                ShowHidePUC(true);
            }
            else
            {
                cmbPUC.SelectedValue = Common.INT_DBNULL;
                ShowHidePUC(false);
            }

            if (string.IsNullOrEmpty(objInterfacePush.Key1) && string.IsNullOrEmpty(objInterfacePush.Key2) &&
                string.IsNullOrEmpty(objInterfacePush.Key3) && string.IsNullOrEmpty(objInterfacePush.Key4) && string.IsNullOrEmpty(objInterfacePush.Key5))
            {
                cmbPushType.SelectedValue = (int)PushType.All;
            }
            else
            {
                cmbPushType.SelectedValue = (int)PushType.Selective;
            }
            cmbAction.SelectedValue = objInterfacePush.ActionId;

            cmbInterfaceProcess.SelectedValue = objInterfacePush.InterfaceProcessId;

            txtID1.Text = objInterfacePush.Key1;
            txtID2.Text = objInterfacePush.Key2;
            txtID3.Text = objInterfacePush.Key3;
            txtID4.Text = objInterfacePush.Key4;
            txtID5.Text = objInterfacePush.Key5;
        }

        private bool ValidateRecord()
        {
            bool IsValid = true;
            errprovValidate.Clear();
            m_sbErrMsgs = new StringBuilder();
            string errMsg = string.Empty;

            if (Convert.ToInt32(cmbLocType.SelectedValue) == Common.INT_DBNULL)
            {
                errMsg = Common.GetMessage("VAL0001", lblLocType.Text.Replace(":", "").Replace("*", ""));
                m_sbErrMsgs.AppendLine(errMsg);
                errprovValidate.SetError(cmbLocType, errMsg);
            }
            else
            {
                errprovValidate.SetError(cmbLocType, string.Empty);
            }

            if (Convert.ToInt32(cmbLocCode.SelectedValue) == Common.INT_DBNULL)
            {
                errMsg = Common.GetMessage("VAL0001", lblLocCode.Text.Replace(":", "").Replace("*", ""));
                m_sbErrMsgs.AppendLine(errMsg);
                errprovValidate.SetError(cmbLocCode, errMsg);
            }
            else
            {
                errprovValidate.SetError(cmbLocCode, string.Empty);
            }

            if (Convert.ToInt32(cmbPushType.SelectedValue) == Common.INT_DBNULL)
            {
                errMsg = Common.GetMessage("VAL0001", lblPushType.Text.Replace(":", "").Replace("*", ""));
                m_sbErrMsgs.AppendLine(errMsg);
                errprovValidate.SetError(cmbPushType, errMsg);
            }
            else
            {
                errprovValidate.SetError(cmbPushType, string.Empty);
            }

            if (Convert.ToInt32(cmbAction.SelectedValue) == Common.INT_DBNULL)
            {
                errMsg = Common.GetMessage("VAL0001", lblAction.Text.Replace(":", "").Replace("*", ""));
                m_sbErrMsgs.AppendLine(errMsg);
                errprovValidate.SetError(cmbAction, errMsg);
            }
            else
            {
                errprovValidate.SetError(cmbAction, string.Empty);
            }

            if (Convert.ToInt32(cmbInterfaceProcess.SelectedValue) == Common.INT_DBNULL)
            {
                errMsg = Common.GetMessage("VAL0001", lblInterface.Text.Replace(":", "").Replace("*", ""));
                m_sbErrMsgs.AppendLine(errMsg);
                errprovValidate.SetError(cmbInterfaceProcess, errMsg);
            }
            else
            {
                errprovValidate.SetError(cmbInterfaceProcess, string.Empty);
            }

            if (txtID1.Enabled)
            {
                if (string.IsNullOrEmpty(txtID1.Text.Trim()))
                {
                    errMsg = Common.GetMessage("VAL0001", lblID1.Text.Replace(":",""));
                    m_sbErrMsgs.AppendLine(errMsg);
                    errprovValidate.SetError(txtID1, errMsg);
                }
                else
                {
                    errprovValidate.SetError(txtID1, string.Empty);
                }
            }
            if (txtID2.Enabled)
            {
                if (string.IsNullOrEmpty(txtID2.Text.Trim()))
                {
                    errMsg = Common.GetMessage("VAL0001", lblID2.Text.Replace(":", ""));
                    m_sbErrMsgs.AppendLine(errMsg);
                    errprovValidate.SetError(txtID2, errMsg);
                }
                else
                {
                    errprovValidate.SetError(txtID2, string.Empty);
                }
            }
            if (txtID3.Enabled)
            {
                if (string.IsNullOrEmpty(txtID3.Text.Trim()))
                {
                    errMsg = Common.GetMessage("VAL0001", lblID3.Text.Replace(":", ""));
                    m_sbErrMsgs.AppendLine(errMsg);
                    errprovValidate.SetError(txtID3, errMsg);
                }
                else
                {
                    errprovValidate.SetError(txtID3, string.Empty);
                }
            }
            if (txtID4.Enabled)
            {
                if (string.IsNullOrEmpty(txtID4.Text.Trim()))
                {
                    errMsg = Common.GetMessage("VAL0001", lblID4.Text.Replace(":", ""));
                    m_sbErrMsgs.AppendLine(errMsg);
                    errprovValidate.SetError(txtID4, errMsg);
                }
                else
                {
                    errprovValidate.SetError(txtID4, string.Empty);
                }
            }
            if (txtID5.Enabled)
            {
                if (string.IsNullOrEmpty(txtID5.Text.Trim()))
                {
                    errMsg = Common.GetMessage("VAL0001", lblID5.Text.Replace(":", ""));
                    m_sbErrMsgs.AppendLine(errMsg);
                    errprovValidate.SetError(txtID5, errMsg);
                }
                else
                {
                    errprovValidate.SetError(txtID5, string.Empty);
                }
            }

            if (!string.IsNullOrEmpty(m_sbErrMsgs.ToString()))
            {
                IsValid = false;
                MessageBox.Show(m_sbErrMsgs.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return IsValid;
        }

        private void SaveUnprocessedData(string interfaceId, string locCode, string key1, string key2, string key3, string key4, string key5, string actionType, int userId)
        {
            string output = string.Empty;

            SelectiveInterfacePush objTemp = new SelectiveInterfacePush();
            objTemp.InterfaceProcessId = interfaceId;
            objTemp.LeafLocationCode = locCode;
            objTemp.Key1 = key1;
            objTemp.Key2 = key2;
            objTemp.Key3 = key3;
            objTemp.Key4 = key4;
            objTemp.Key5 = key5;
            objTemp.ActionId = GetAction(interfaceId, actionType);
            objTemp.InsertedById = userId;

            if (objTemp.InterfaceProcessId == CON_SEQNUMBER)
            {
                objTemp.Key1 = cmbLocCode.SelectedValue.ToString();
                objTemp.ActionId = CON_INSERT;
            }

            if (objTemp.InterfaceProcessId == CON_STKLEDGER)
            {
                objTemp.SaveUnprocessedRecord(CON_STKDAILY, objTemp.LeafLocationCode, cmbLocCode.SelectedValue.ToString(), objTemp.Key2, objTemp.Key3, objTemp.Key4, objTemp.Key5, objTemp.ActionId, objTemp.InsertedById, ref output);
                if (output == string.Empty)
                {
                    goto re;
                }
                objTemp.SaveUnprocessedRecord(CON_STKMONTH, objTemp.LeafLocationCode, cmbLocCode.SelectedValue.ToString(), objTemp.Key2, objTemp.Key3, objTemp.Key4, objTemp.Key5, objTemp.ActionId, objTemp.InsertedById, ref output);
                if (output == string.Empty)
                {
                    goto re;
                }

                objTemp.SaveUnprocessedRecord(CON_STKYEAR, objTemp.LeafLocationCode, cmbLocCode.SelectedValue.ToString(), objTemp.Key2, objTemp.Key3, objTemp.Key4, objTemp.Key5, objTemp.ActionId, objTemp.InsertedById, ref output);
                if (output == string.Empty)
                {
                    goto re;
                }
                else
                    output = "3";
            }
            else
            {
                objTemp.SaveUnprocessedRecord(objTemp.InterfaceProcessId, objTemp.LeafLocationCode, objTemp.Key1, objTemp.Key2, objTemp.Key3, objTemp.Key4, objTemp.Key5, objTemp.ActionId, objTemp.InsertedById, ref output);
            }
            re:
            if (!string.IsNullOrEmpty(output))
            {
                int rowsAffected = Common.INT_DBNULL;
                if (Int32.TryParse(output, out rowsAffected))
                {
                    FillUnprocessedRecords();
                    if (dgvUnprocessedRecords.Rows.Count > 0)
                    {
                        dgvUnprocessedRecords.Rows[0].Selected = false;
                        dgvUnprocessedRecords.Rows[dgvUnprocessedRecords.Rows.Count - 1].Selected = true;
                        dgvUnprocessedRecords.FirstDisplayedScrollingRowIndex = dgvUnprocessedRecords.Rows.Count - 1;
                    }

                    MessageBox.Show(Common.GetMessage("VAL0077", rowsAffected.ToString(), objTemp.ActionId == "I" ? "insert" : "update"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    m_scrstState = ScreenState.View;
                    ManageScreenState();
                }
                else
                {
                    MessageBox.Show(output, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ResetLocation()
        {
            cmbLocType.SelectedValue = Common.INT_DBNULL;
            cmbLocCode.SelectedValue = Common.INT_DBNULL;
            cmbPUC.SelectedValue = Common.INT_DBNULL;
        }

        private void ResetInterface(bool resetInterface, bool resetInterfaceKeys)
        {
            if (resetInterface)
            {
                cmbInterfaceProcess.SelectedValue = Common.INT_DBNULL;
            }

            if (resetInterfaceKeys)
            {
                ClearInterfaceKeys();
                EnableDisableInterfaceKeys(new bool[] { false, false, false, false, false });
            }
        }

        private void ResetOthers()
        {
            cmbPushType.SelectedValue = Common.INT_DBNULL;
            cmbAction.SelectedValue = Common.INT_DBNULL;
        }

        private string GetAction(string interfaceID,string Action)
        {
            if (ar.IndexOf(interfaceID) >= 0)
                return "I";           
            else
                return Action;
        }

        #endregion

        

    }
}
