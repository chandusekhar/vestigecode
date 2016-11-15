using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.UI;
using CoreComponent.Core.BusinessObjects;


namespace CoreComponent.UI
{
    public partial class frmBonusMaster : HierarchyTemplate
    {
        #region VARIABLE DECLARATION

        private bool m_isProcessAvailable = false;
        private int m_userId;
        private string m_strUserName;
        private string m_strLocationCode;
        private int m_currentLocationId;
        private int m_locationType;
        private const string MODULENAME = "BAT01";

        #endregion

        #region C'TOR

        public frmBonusMaster()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region METHODS

        private void InitializeControls()
        {
            lblPageTitle.Text = "Bonus Master Batch";
            ResetControls();
        }

        private void InitializeRights()
        {
            m_userId = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
            m_strUserName = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;

            m_strLocationCode = Common.LocationCode;
            m_currentLocationId = Common.CurrentLocationId;
            m_locationType = Common.CurrentLocationTypeId;
            m_isProcessAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_strUserName, m_strLocationCode, MODULENAME, Common.FUNCTIONCODE_PROCESS);
        }

        private void ResetControls()
        {
            btnSave.Enabled = m_isProcessAvailable;
            rdoMonthly.Checked = true;
            CheckAllCheckBox(false);
        }

        private void EnableDisableAllCheckBox(bool check)
        {
            foreach (Control c in grpOptions.Controls)
            {
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                    ((CheckBox)c).Enabled = check;
                }
            }
        }

        private void CheckAllCheckBox(bool check)
        {
            foreach (Control c in grpOptions.Controls)
            {
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = check;                   
                }
            }
        }

        private void ChangeRadioButton(RadioButton rb)
        {
            EnableDisableAllCheckBox(false);
            if (rb.Name == "rdoMonthly")
            {
                chkPVBV.Enabled = true;
                chkLevels.Enabled = true;
                chkBonus.Enabled = true;
                chkCloseMonth.Enabled = true;
            }
            else
            {
                chkPVBV.Enabled = true;
            }
        }
        
        private bool ValidateBeforeProcess()
        {
            bool ret = true;
            StringBuilder sb = new StringBuilder();
            if (chkPVBV.Checked == false && chkBonus.Checked == false && chkLevels.Checked == false && chkCloseMonth.Checked == false)
            {
                ret = false;
                sb.AppendLine(Common.GetMessage("VAL0118"));
            }
            else if (chkPVBV.Checked == true && chkBonus.Checked == true && chkLevels.Checked == false)
            {
                ret = false;
                sb.AppendLine(Common.GetMessage("VAL0116"));                
            }
            else if (GetMode() == -1)
            {
                ret = false;
                sb.AppendLine(Common.GetMessage("VAL0117"));
            }

            if (!ret)
            {
                MessageBox.Show(sb.ToString(),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            return ret;
        }
        
        private int GetFrequency()
        {
            int freq = 0;
            if (rdoMonthly.Checked)
                freq = 2;
            else
                freq = 1;
            return freq;
        }

        private int GetMode()
        {
            int mode = 0;
            if (chkPVBV.Checked == true && chkLevels.Checked == false && chkBonus.Checked == false && chkCloseMonth.Checked == false)
                mode = 1;
            else if (chkPVBV.Checked == true && chkLevels.Checked == true && (chkBonus.Checked) == false && chkCloseMonth.Checked == false)
                mode = 2;
            else if (chkPVBV.Checked == false && chkLevels.Checked == true && chkBonus.Checked == false && chkCloseMonth.Checked == false)
                mode = 2;
            else if (chkPVBV.Checked == false && chkLevels.Checked == false && chkBonus.Checked == true && chkCloseMonth.Checked == false)
                mode = 3;
            else if (chkPVBV.Checked == true && chkLevels.Checked == true && chkBonus.Checked == false && chkCloseMonth.Checked == false)
                mode = 4;
            else if (chkPVBV.Checked == false && chkLevels.Checked == true && chkBonus.Checked == true && chkCloseMonth.Checked == false)
                mode = 5;
            else if (chkPVBV.Checked == true && chkLevels.Checked == true && chkBonus.Checked == true && chkCloseMonth.Checked == false)
                mode = 6;
            else if (chkPVBV.Checked == false && chkLevels.Checked == false && chkBonus.Checked == false && chkCloseMonth.Checked == true)
                mode = 7;
            else if (chkPVBV.Checked == true && chkLevels.Checked == true && chkBonus.Checked == true && chkCloseMonth.Checked == true)
                mode = 8;
            else if (chkPVBV.Checked == false && chkLevels.Checked == true && chkBonus.Checked == true && chkCloseMonth.Checked == true)
                mode = 9;
            else
                mode = -1;
            return mode;
        }

        private void ProcessBatch()
        {
            string validationMessage = string.Empty;
            string errorMessage = string.Empty;
            CoreComponent.BusinessObjects.BonusMasterBatch objBonusMasterBatch = new CoreComponent.BusinessObjects.BonusMasterBatch();
            bool result = objBonusMasterBatch.ProcessBatch(GetFrequency(),1,GetMode(), ref validationMessage,ref errorMessage);
            if (result)
              {
                 MessageBox.Show(Common.GetMessage("INF0206"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
              }
              else
              {
                 if (validationMessage.Length > 0)
                    MessageBox.Show(Common.GetMessage(validationMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 else
                    MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
        }

        #endregion

        #region EVENTS

        private void frmBonusMaster_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeRights();
                InitializeControls();
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
                if (ValidateBeforeProcess())             
                    ProcessBatch();
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
                ResetControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void rdoMonthly_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoMonthly.Checked)
                    ChangeRadioButton((RadioButton)sender);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdoDaily_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoDaily.Checked)
                    ChangeRadioButton((RadioButton)sender);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion 

        private void btnExport_Click(object sender, EventArgs e)
        {
            CoreComponent.UI.frmDistributorPaymentSummary objfrmSearch = new CoreComponent.UI.frmDistributorPaymentSummary();
           
            objfrmSearch.ShowDialog();
            //_distributor = (Distributor)objfrmSearch.ReturnObject;

        }


    }
}
