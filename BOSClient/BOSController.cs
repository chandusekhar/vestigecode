using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Collections;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.Hierarchies.UI;
using AuthenticationComponent.UI;
using CoreComponent.MasterData.UI;
using PromotionsComponent.UI;
using PurchaseComponent.UI;

namespace BOSClient
{
    /*
    ------------------------------------------------------------------------
    Created by			    :	Amit Bansal
    Created Date		    :	08/June/2009
    Purpose				    :	This class is acting as controller to invoke all menus and 
     *                          opening respective forms from MDI. This class receives call via
     *                          a common event at MDI, then process accordingly from its 'OpenForm()'
     *                          method.
    Modified by			    :	Ajay Kumar Singh
    Date of Modification    :	09/June/2009
    Purpose of Modification	:	Coding to invoke forms from corresponding menu.
    ------------------------------------------------------------------------    
    */

    internal class BOSController
    {
        public BOSController()
        {
        }

        public event SignInHandler SignIn;

        protected virtual void OnSignIn(SignInArgs e)
        {
            if (SignIn != null)
                SignIn(this, e);
        }

        /// <summary>
        /// 1.  It ensures that only one form is open at a time
        /// 2.  If the requested form is already open, then it returns false
        /// 3.  If any form other than the requested form is open, then it asks the user whether they want 
        ///     to close the open form. If yes, the open form is closed and the requested form is opened,
        ///     otherwise, the open form is retained
        /// 4.  If no form is currently open, this method returns true    
        /// </summary>
        /// <param name="parent">Parent form which would be the container for the requested form</param>
        /// <param name="menuId">Id of the menu clicked, same as the value of tag in the requested form</param>
        /// <returns>Boolean value indicating whether the request can be performed</returns>
        internal bool CanOpenForm(Form parent, string menuId)
        {
            try
            {
                bool returnValue = true;

                #if DEBUG
                
                try
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < Application.OpenForms.Count; i++)
                    {
                        sb.AppendLine(Application.OpenForms[i].Name);
                    }
                    sb.AppendLine("New Form Requested = " + menuId);
                    Common.LogInformation(sb.ToString());
                }
                catch (Exception ex)
                {
                    Common.LogException(ex);
                }

                #endif

                if (parent.MdiChildren.Length > 0)
                {
                    if (parent.MdiChildren[0].Tag.ToString() == menuId)
                    {
                        returnValue = false;
                    }
                    else
                    {
                        //parent.MdiChildren[0].Activate();
                        DialogResult result = MessageBox.Show(Common.GetMessage("5002"), Common.GetMessage("10001"),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            returnValue = false;
                        }
                        else
                        {
                            returnValue = true;
                        }
                    }
                }
                if (returnValue && parent.MdiChildren.Length > 0)
                {
                    //parent.MdiChildren[0].Close();
                    Form frm = parent.MdiChildren[0];
                    frm.Close();
                    frm.Dispose();
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void OpenForm(frmMDIMain frmMDIMain, string menuCode, CoreComponent.BusinessObjects.MenuItem mItem)
        {
            try
            {
                //if (CanOpenForm(frmMDIMain, menuCode))
                //{
                    string assemblyName = mItem.AssemblyName;
                    string className = mItem.ClassName;
                    if (string.IsNullOrEmpty(assemblyName) || string.IsNullOrEmpty(className))
                    {
                        MessageBox.Show(Common.GetMessage("2005"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Assembly.Load(assemblyName).CreateInstance(
                        Assembly a = Assembly.Load(assemblyName);

                      
                        object formObject = null;//Activator.CreateInstance(a.GetType(className));
                        if (mItem.MenuItemParamList.Count == 0)
                        {
                            formObject = Activator.CreateInstance(a.GetType(className));
                        }
                        else
                        {
                            ArrayList arr = new ArrayList();
                            foreach (MenuItemParameter p in mItem.MenuItemParamList)
                            {
                                arr.Add(p.Value);
                            }
                            formObject = Activator.CreateInstance(a.GetType(className), arr.ToArray());
                        }

                        
                        (formObject as Form).StartPosition = FormStartPosition.Manual;
                        (formObject as Form).FormBorderStyle = FormBorderStyle.None;
                        (formObject as Form).MdiParent = frmMDIMain;
                        (formObject as Form).Tag = menuCode;
                        (formObject as Form).Location = new Point(0, 0);
                        (formObject as Form).Tag = menuCode;
                        (formObject as Form).Show();
                        if (menuCode == Common.LoginModuleCode)
                        {
                            (formObject as frmLogin).SignIn += new SignInHandler(BOSController_SignIn);
                        }
                        else
                        {
                            ((formObject as Form).Controls.Find("lblAppUser", true)[0] as Label).Text = Common.GetMessage("10005", AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName);
                            ((formObject as Form).Controls.Find("lblAppUser", true)[0] as Label).Dock = DockStyle.Left;
                            ((formObject as Form).Controls.Find("lblAppUser", true)[0] as Label).Anchor =AnchorStyles.Left;
                            ((formObject as Form).Controls.Find("lblAppUser", true)[0] as Label).Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void BOSController_SignIn(object sender, SignInArgs e)
        {
            OnSignIn(e);   
        }
    }
}
