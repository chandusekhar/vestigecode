using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AuthenticationComponent.UI;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Logging;


namespace POSClient.UI
{
	public partial class Login : BaseChildForm
	{
        public event SignInHandler SignIn;

		public Login()
		{
			InitializeComponent();
			txtUserCode.Text = txtPassword.Text = string.Empty;
			DialogResult = DialogResult.Cancel;
            //oskbSignIn.CurrentFocus = txtUserCode;
			txtUserCode.Focus();
		}

		public Login(bool authorize)
			: this()
		{
			if (!authorize)
			{
				lblPassword.Visible = txtPassword.Visible = false;
				txtUserCode.UseSystemPasswordChar = true;
			}
		}

        protected virtual void OnSignIn(SignInArgs e)
        {
            if (SignIn != null)
            {
                SignIn(this, e);
            }
        }

		public Login(string usercode)
			: this()
		{
			if (usercode.Trim().Length > 0)
			{
				txtUserCode.Text = usercode.Trim();
                //oskbSignIn.CurrentFocus = txtPassword;
				txtPassword.Focus();
			}
		}

		private void TextBox_Enter(object sender, EventArgs e)
		{
            //oskbSignIn.CurrentFocus = (TextBoxBase)sender;
		}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SignInArgs sArgs = new SignInArgs();
            //MessageBox.Show(Common.GetMessage("INF0018"), Common.GetMessage("5008"),
                  //MessageBoxButtons.OK, MessageBoxIcon.Information);
            sArgs.IsSuccess = false;
            OnSignIn(sArgs);
            this.Close();      
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = txtUserCode.Text.Trim();
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
	}
}

