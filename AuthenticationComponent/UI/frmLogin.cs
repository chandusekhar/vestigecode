using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;

namespace AuthenticationComponent.UI
{
    public delegate void  SignInHandler(object sender, SignInArgs e);

    public partial class frmLogin : CoreComponent.Core.UI.BlankTemplate
    {
        public event SignInHandler SignIn;

        public frmLogin()
        {
            lblPageTitle.Text = "";
            InitializeComponent();
        }

        protected virtual void OnSignIn(SignInArgs e)
        {
            if (SignIn != null)
            {
                SignIn(this, e);
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = txtUserName.Text.Trim();
                string password = txtPassword.Text.Trim();
                if (userName == string.Empty)
                {
                    // Show Message
                    MessageBox.Show(Common.GetMessage("INF0050"), Common.GetMessage("5007"), 
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (password == string.Empty)
                {
                    // Show Message
                    MessageBox.Show(Common.GetMessage("INF0051"), Common.GetMessage("5007"), 
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //Sign in
                    SignInArgs sArgs = new SignInArgs();
                    if (AuthenticationComponent.BusinessObjects.Authenticate.LogInUser(userName, password, CoreComponent.Core.BusinessObjects.Common.LocationCode))
                    {
                        sArgs.IsSuccess = true;
                        OnSignIn(sArgs);
                        this.Close();
                    }
                    else
                    {

                        MessageBox.Show(Common.GetMessage("INF0018"), Common.GetMessage("5008"), 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        sArgs.IsSuccess = false;
                        txtPassword.SelectAll();
                        OnSignIn(sArgs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }
    }

    public class SignInArgs : EventArgs
    {

        private bool m_isSuccess;

        public bool IsSuccess
        {
            get { return m_isSuccess; }
            set { m_isSuccess = value; }
        }

        public SignInArgs()
        {
        }

        public SignInArgs(bool isSuccess)
        {
            this.m_isSuccess = isSuccess;
        }
    }

}
