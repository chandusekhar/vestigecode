using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AuthenticationComponent.BusinessObjects;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;




namespace BOSClient
{
    /*
    ------------------------------------------------------------------------
    Created by			    :	Ajay Kumar Singh
    Created Date		    :	09/June/2009
    Purpose				    :	This form will act as MDI Parent form for all other forms in this application.    
    Modified by			    :	
    Date of Modification    :	
    Purpose of Modification	:	
    ------------------------------------------------------------------------    
    */

    public partial class frmMDIMain : Form
    {
        private BOSController m_bosControlObject;

        internal BOSController BosControlObject
        {
            get { return m_bosControlObject; }
            set { m_bosControlObject = value; }
        }

        private List<CoreComponent.BusinessObjects.MenuItem> m_menuItems;

        public List<CoreComponent.BusinessObjects.MenuItem> MenuItems
        {
            get { return m_menuItems; }
            set { m_menuItems = value; }
        }

        public frmMDIMain()
        {
            try
            {
                InitializeComponent();
                m_bosControlObject = new BOSController();
                m_bosControlObject.SignIn += new AuthenticationComponent.UI.SignInHandler(m_bosControlObject_SignIn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        void m_bosControlObject_SignIn(object sender, AuthenticationComponent.UI.SignInArgs e)
        {
            try
            {
                if (e.IsSuccess)
                {
                    List<Module> mList = Authenticate.AccessibleModules(AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser, Common.LocationCode);
                    mnuNewMenu.Enabled = true;
                    foreach (Module m in mList)
                    {
                        if (mnuNewMenu.Items.Find(m.Code, true).Count() > 0)
                        {
                            ToolStripItem t = mnuNewMenu.Items.Find(m.Code, true)[0];
                            if (t.Name == Common.LoginModuleCode)
                            {
                                t.Visible = false;
                            }
                            else
                            {
                                ToolStripItem parent = t.OwnerItem;
                                while (parent != null)
                                {
                                    if (parent.Name == string.Empty)
                                    {
                                        parent.Enabled = true;
                                        parent = parent.OwnerItem;
                                    }
                                    else
                                    {
                                        parent = null;
                                    }

                                }
                                //if (t.OwnerItem.Name == string.Empty)
                                //{
                                //    t.OwnerItem.Enabled = true;
                                //}
                                t.Enabled = true;
                            }
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

        public frmMDIMain(List<CoreComponent.BusinessObjects.MenuItem> mnuItems)
        {
            try
            {
                InitializeComponent();
                //AuthenticationComponent.BusinessObjects.Authenticate.LogInUser("amit", "test", "HO");
                m_menuItems = mnuItems;
                CreateMenus(m_menuItems);
                mnuNewMenu.Enabled = false;
                m_bosControlObject = new BOSController();
                m_bosControlObject.SignIn += new AuthenticationComponent.UI.SignInHandler(m_bosControlObject_SignIn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        public void CreateMenus(List<CoreComponent.BusinessObjects.MenuItem> mnuItems)
        {
            mnuItems.Sort(new CoreComponent.Core.BusinessObjects.GenericComparer<CoreComponent.BusinessObjects.MenuItem>("SeqNo", SortDirection.Ascending));
            foreach (CoreComponent.BusinessObjects.MenuItem mnu in mnuItems)
            {
                if (/*mnu.ModuleCode == string.Empty &&*/ mnu.ParentId < 0)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(mnu.MenuName);
                    tsmi.Name = mnu.ModuleCode;
                    tsmi.Enabled = false;
                    tsmi.BackColor = Color.White;
                    tsmi.AutoSize = true;
                    if (mnu.ModuleCode == string.Empty)
                    {
                        List<CoreComponent.BusinessObjects.MenuItem> subMenus = mnuItems.FindAll(delegate(CoreComponent.BusinessObjects.MenuItem m) { return m.ParentId == mnu.RecordId; });
                        subMenus.Sort(new CoreComponent.Core.BusinessObjects.GenericComparer<CoreComponent.BusinessObjects.MenuItem>("SeqNo", SortDirection.Ascending));
                        CreateSubMenus(tsmi, subMenus);
                    }
                    else
                    {
                        tsmi.Tag = mnu.ModuleCode;
                        // Attach Event Handler
                        tsmi.Click += new EventHandler(MenuItemClick);
                    }
                    mnuNewMenu.Items.Add(tsmi);
                }
                //Create ToolstripMenuItem
                //Check if modulecode is string.empty, then find children and call this method recursively
                //If modulecode is not empty then attach event handler
            }
        }

        private void CreateSubMenus(ToolStripMenuItem tsmi, List<CoreComponent.BusinessObjects.MenuItem> subMenus)
        {
            foreach (CoreComponent.BusinessObjects.MenuItem m in subMenus)
            {
                ToolStripMenuItem ts = new ToolStripMenuItem(m.MenuName);
                ts.Name = m.ModuleCode;
                ts.Enabled = false;
                ts.BackColor = Color.White;
                ts.AutoSize = true;
                if (m.ModuleCode == string.Empty)
                {
                    List<CoreComponent.BusinessObjects.MenuItem> subMenuList = m_menuItems.FindAll(delegate(CoreComponent.BusinessObjects.MenuItem m1) { return m1.ParentId == m.RecordId; });
                    subMenuList.Sort(new CoreComponent.Core.BusinessObjects.GenericComparer<CoreComponent.BusinessObjects.MenuItem>("SeqNo", SortDirection.Ascending));
                    CreateSubMenus(ts, subMenuList);
                }
                else
                {
                    ts.Tag = m.ModuleCode;
                    //Attach event handler
                    ts.Click += new EventHandler(MenuItemClick);
                }
                tsmi.DropDownItems.Add(ts);
            }
        }

        /// <summary>
        /// Common function to handle click event of all Menus on this MDI from.
        /// It will send the 'Tag' value of currently clicked menu to 'BOSController' where it will be 
        /// handled by its 'OpenForm()' method to open particular desired form.
        /// </summary>
        /// <param name="sender">The current ToolStripMenuItem that is clicked.</param>
        /// <param name="e">Event argument for this ToolStripMenuItem.</param>
        private void MenuItemClick(object sender, EventArgs e)
        {
            try
            {
                CoreComponent.BusinessObjects.MenuItem mItem = m_menuItems.Find(delegate(CoreComponent.BusinessObjects.MenuItem m) { return m.ModuleCode == (sender as ToolStripMenuItem).Tag.ToString(); });
                if (mItem.ModuleCode == Common.LogoutModuleCode)
                {
                    Authenticate.LoggedInUser = null;
                    CoreComponent.BusinessObjects.MenuItem mItem1 = m_menuItems.Find(delegate(CoreComponent.BusinessObjects.MenuItem m) { return m.ModuleCode == Common.LoginModuleCode; });
                    if (m_bosControlObject.CanOpenForm(this, Common.LoginModuleCode))
                    {
                        m_bosControlObject.OpenForm(this, Common.LoginModuleCode, mItem1);
                        DisableAllMenus();
                    }
                }
                else
                {
                    if (m_bosControlObject.CanOpenForm(this, (sender as ToolStripMenuItem).Tag.ToString()))
                    {
                        m_bosControlObject.OpenForm(this, (sender as ToolStripMenuItem).Tag.ToString(), mItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void DisableAllMenus()
        {
            foreach (ToolStripMenuItem tsmi in mnuNewMenu.Items)
            {
                foreach (ToolStripMenuItem t in tsmi.DropDownItems)
                {
                    DisableMenuItem(t);
                }
                tsmi.Enabled = false;
            }
        }

        private void DisableMenuItem(ToolStripMenuItem t)
        {
            foreach (ToolStripMenuItem t1 in t.DropDownItems)
            {
                DisableMenuItem(t1);
            }
            t.Enabled = false;
        }

        private void frmMDIMain_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = CoreComponent.Core.BusinessObjects.Common.GetMessage("10003") + "  [" + (Common.LocationConfigId)Common.CurrentLocationTypeId + " : " + Common.LocationCode + "] " + "[v" + Common.Version + "]";          //Application name
                CoreComponent.BusinessObjects.MenuItem menuItem = m_menuItems.Find(delegate(CoreComponent.BusinessObjects.MenuItem m) { return m.ModuleCode == Common.LoginModuleCode; });
                m_bosControlObject.OpenForm(this, Common.LoginModuleCode, menuItem);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
            //this.Text += " V1.0"; //Set version number
        }
       

        private static int currentUserId;
        public static int CurrentUserId
        {
            get { return currentUserId; }
            set { currentUserId = value; }
        }
    }
}
