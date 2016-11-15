using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using AuthenticationComponent.BusinessObjects;
namespace AuthenticationComponent.UI
{
    public partial class frmChangePassword :CoreComponent.Core.UI.BlankTemplate
    {
        int m_userId = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;        
        public frmChangePassword()
        {
           
            InitializeComponent();
            lblPageTitle.Text = "Change Password";
        }

        private void SaveUserData()
        {
            try
            {
                //Sign in
                SignInArgs sArgs = new SignInArgs();
                if (AuthenticationComponent.BusinessObjects.Authenticate.CheckUserIsValid(AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName, txtOldPassword.Text, CoreComponent.Core.BusinessObjects.Common.LocationCode))
                {
                   
                    User m_objUser = new User();
                    m_objUser.UserId = m_userId;
                    m_objUser.Password = m_objUser.EncryptPassword(txtNewPassword.Text.Trim());
                    m_objUser.PasswordModifiedTrueFalse = 1;
                    m_objUser.UserId = m_userId; // Global variable    
                    string errMsg = string.Empty;
                    bool retVal = m_objUser.UserSave(Common.ToXml(m_objUser), User.USER_SAVE, ref errMsg);
                    if (retVal)
                    {
                        MessageBox.Show(Common.GetMessage("INF0004"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else if (!errMsg.Trim().Equals(string.Empty))
                    {
                        if (errMsg.Trim().IndexOf("30001") >= 0)
                        {
                            MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Common.LogException(new Exception(errMsg));
                        }
                        else
                        {
                            MessageBox.Show(Common.GetMessage(errMsg.Trim()),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("INF0005"), Common.GetMessage("5008"),
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                bool isvalid = ValidateChange();
                if (!isvalid)
                {
                    StringBuilder sbError = GenerateChangeError();
                    if (sbError.ToString().Trim().Length > 0)
                    {
                        MessageBox.Show(sbError.ToString(),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (txtOldPassword.Text.Trim().Equals(txtNewPassword.Text.Trim()))
                    {
                        MessageBox.Show(Common.GetMessage("8010"),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                    else
                    {
                        SaveUserData();
                        clearForm();
                    }
                    //save
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(),Common.GetMessage("10001"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        private void clearForm()
        {
            txtOldPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;
            txtConPassword.Text = string.Empty;
        }
        private bool ValidateChange()
        {
            bool isvalid = true;
            // Check Item Code Blank
            errorChange.Clear();
            //Check Vendor Code
            if (Validators.CheckForEmptyString(txtOldPassword.Text.Length))
            {
                isvalid = false;
                errorChange.SetError(txtOldPassword, Common.GetMessage("INF0019", " Existing Password"));                
            }
            if (Validators.CheckForEmptyString(txtNewPassword.Text.Length))
            {
                isvalid = false;
                errorChange.SetError(txtNewPassword, Common.GetMessage("INF0019", " New Password"));
            }
            else if (!Validators.RangeValidator(txtNewPassword.Text.Trim().Length,6, 20))
            {
                errorChange.SetError(txtNewPassword, Common.GetMessage("VAL0014", "6", "20"));
            }
            if (isvalid)
            {
                if (Validators.CheckForEmptyString(txtConPassword.Text.Length))
                {
                    isvalid = false;
                    errorChange.SetError(txtConPassword, Common.GetMessage("INF0019", " New Password Again"));
                }
            }
            if (isvalid)
            {
                if (!txtNewPassword.Text.Equals(txtConPassword.Text))
                {
                    isvalid = false;
                    errorChange.SetError(txtConPassword,Common.GetMessage("INF0078"));
                }
            }
            if (isvalid)
            {
                if (txtNewPassword.Text.Equals(txtOldPassword.Text))
                {
                    isvalid = false;
                    errorChange.SetError(txtNewPassword, Common.GetMessage("INF0008"));
                }
            }
            
            return isvalid;
        }
        private StringBuilder GenerateChangeError()
        {
            bool focus = false;
            StringBuilder sbError = new StringBuilder();

            if (errorChange.GetError(txtOldPassword).Trim().Length > 0)
            {
                sbError.Append(errorChange.GetError(txtOldPassword));
                sbError.AppendLine();
                if (!focus)
                {
                    txtOldPassword.Focus();
                    focus = true;
                }
            }
            if (errorChange.GetError(txtNewPassword).Trim().Length > 0)
            {
                sbError.Append(errorChange.GetError(txtNewPassword));
                sbError.AppendLine();
                if (!focus)
                {
                    txtNewPassword.Focus();
                    focus = true;
                }
            }
            if (errorChange.GetError(txtConPassword).Trim().Length > 0)
            {
                sbError.Append(errorChange.GetError(txtConPassword));
                sbError.AppendLine();
                if (!focus)
                {
                    txtConPassword.Focus();
                    focus = true;
                }
            }
            return Common.ReturnErrorMessage(sbError);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtConPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;
            txtOldPassword.Text = string.Empty;
            errorChange.Clear();
        }
      
    }
}
