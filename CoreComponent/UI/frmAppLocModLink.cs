using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.Hierarchies.BusinessObjects;
using CoreComponent.UI;
using CoreComponent.BusinessObjects;


namespace CoreComponent.UI
{
    public partial class frmAppLocModLink : CoreComponent.Core.UI.Transaction
    {
        #region Variable Declaration

        private AppLocModFunctions m_applocobjadd = null;
        private List<AppLocModFunctions> m_apploclistsearch = null;
        int m_recordid;

        #endregion 

        #region Constructor

        public frmAppLocModLink()
        {
            m_applocobjadd = new AppLocModFunctions();
            m_apploclistsearch = new List<AppLocModFunctions>();
            m_recordid = 0;
            InitializeComponent();
        }

#endregion

        #region Events

        private void frmAppLocModLink_Load(object sender, EventArgs e)
        {
            try
            {
                lblPageTitle.Text = "Menu Creation";
                initializeaddcomponents();
                initializesearchcomponents();
                dgvApplocmodfunc = Common.GetDataGridViewColumns(dgvApplocmodfunc, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cmbloctypeadd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                changeindexofapplicationorlocation();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
               
        private void cmbmoduleadd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((Int32.Parse(cmbapptypeadd.SelectedValue.ToString()) > -1) && (Int32.Parse(cmbloctypeadd.SelectedValue.ToString()) > -1))
                {
                    if ((Int32.Parse(cmbmoduleadd.SelectedValue.ToString()) > -1))
                    {
                        // populate functions linked to selected module in functionchecklistbox. 
                        DataTable dtfunctionsadd = Common.ParameterLookup(Common.ParameterType.function, (new ParameterFilter("", Int32.Parse(cmbmoduleadd.SelectedValue.ToString()), Common.INT_DBNULL, Common.INT_DBNULL)));
                        listBoxfunctionsadd.DataSource = dtfunctionsadd;
                        listBoxfunctionsadd.DisplayMember = "FunctionName";
                        listBoxfunctionsadd.ValueMember = "FunctionId";
                    }
                    else
                    {

                        listBoxfunctionsadd.DataSource = null;
                        listBoxfunctionsadd.Items.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbapptypeadd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbapptypeadd.SelectedValue.ToString()) == -1)
                {
                    resetalladdcontrols();
                }
                else
                {
                    changeindexofapplicationorlocation();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void btnClearDetails_Click(object sender, EventArgs e)
        {
            try
            {
                resetalladdcontrols();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (startvalidation())
                {
                    Addandsave();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        


        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                startsearch();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                resetsearchcontrols();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvApplocmodfunc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    if ((dgvApplocmodfunc.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
                    {
                        gotoedit(e);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        private void cmbParentadd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbParentadd.SelectedValue) == -1)
                {
                    cmbmoduleadd.SelectedValue = -1;
                    cmbmoduleadd.Enabled = false;
                }
                else
                {
                    cmbmoduleadd.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Methods

        private void initializesearchcomponents()
        {
            DataTable dtapptypesearch, dtloctypesearch, dtmodulesearch;

            // to populate app type
            dtapptypesearch = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("APPLICATIONTYPE", 0, 0, 0));
            cmbapptype.DataSource = dtapptypesearch;
            cmbapptype.DisplayMember = Common.KEYVALUE1;
            cmbapptype.ValueMember = Common.KEYCODE1;

            // to populate loc type in search tab
            dtloctypesearch = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("LOCATIONTYPE", 0, 0, 0));
            cmbloctype.DataSource = dtloctypesearch;
            cmbloctype.DisplayMember = Common.KEYVALUE1;
            cmbloctype.ValueMember = Common.KEYCODE1;

            // to populate modules in search tab
            dtmodulesearch = Common.ParameterLookup(Common.ParameterType.Module, (new ParameterFilter("", Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL)));
            cmbmodule.DataSource = dtmodulesearch;
            cmbmodule.DisplayMember = "ModuleCode";
            cmbmodule.ValueMember = "ModuleId";
        }

        private void initializeaddcomponents()
        {
            DataTable dtstatusadd, dtapptypeadd, dtloctypeadd;
            try
            {

                // code to populate App. Type in Combo box  in create tab
                cmbapptypeadd.SelectedIndexChanged -= new EventHandler(cmbapptypeadd_SelectedIndexChanged);
                dtapptypeadd = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("APPLICATIONTYPE", 0, 0, 0));
                cmbapptypeadd.DataSource = dtapptypeadd;
                cmbapptypeadd.DisplayMember = Common.KEYVALUE1;
                cmbapptypeadd.ValueMember = Common.KEYCODE1;

                // code for populating location type in cmbloctypeadd combo box  in create tab
                dtloctypeadd = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("LOCATIONTYPE", 0, 0, 0));
                cmbloctypeadd.SelectedIndexChanged -= new EventHandler(cmbmoduleadd_SelectedIndexChanged);
                cmbloctypeadd.DataSource = dtloctypeadd;
                cmbloctypeadd.DisplayMember = Common.KEYVALUE1;
                cmbloctypeadd.ValueMember = Common.KEYCODE1;
                cmbapptypeadd.SelectedIndexChanged += new EventHandler(cmbapptypeadd_SelectedIndexChanged);
                cmbloctypeadd.SelectedIndexChanged += new EventHandler(cmbloctypeadd_SelectedIndexChanged);

                // to add items to Status Combo box  in create tab
                dtstatusadd = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("STATUS", 0, 0, 0));
                cmbstatusadd.DataSource = dtstatusadd;
                cmbstatusadd.DisplayMember = Common.KEYVALUE1;
                cmbstatusadd.ValueMember = Common.KEYCODE1;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resetalladdcontrols()
        {
            try
            {
                // if we reset apptype and location type, it will reset all dependent combobox.
                cmbapptypeadd.SelectedValue = -1;
                cmbloctypeadd.SelectedValue = -1;
                cmbstatusadd.SelectedValue = -1;
                menunameadd.Text = string.Empty;
                seqnoadd.Text = string.Empty;
                cmbapptypeadd.Enabled = true;
                cmbloctypeadd.Enabled = true;
                cmbParentadd.Enabled = true;
                cmbmoduleadd.Enabled = true;
                errprovadd.Clear();
                m_recordid = 0;
                tabControlTransaction.TabPages[1].Text = "Create";
                cmbapptypeadd.Focus();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Addandsave()
        {
            // get all selected data from frontend in global object of business class and call save method.
            try
            {
                m_applocobjadd.Recordid = m_recordid;
                m_applocobjadd.Apptypeid = Int32.Parse(cmbapptypeadd.SelectedValue.ToString());
                m_applocobjadd.Apptypename = cmbapptypeadd.Text;
                m_applocobjadd.Locid = Int32.Parse(cmbloctypeadd.SelectedValue.ToString());
                m_applocobjadd.Locname = cmbloctypeadd.Text;
                m_applocobjadd.Moduleid = Int32.Parse(cmbmoduleadd.SelectedValue.ToString());
                m_applocobjadd.Modulename = cmbmoduleadd.Text;
                m_applocobjadd.Parentid = Int32.Parse(cmbParentadd.SelectedValue.ToString());
                m_applocobjadd.Menuname = menunameadd.Text;
                m_applocobjadd.Seqno = Int32.Parse(seqnoadd.Text);
                m_applocobjadd.Status = Int32.Parse(cmbstatusadd.SelectedValue.ToString());
                m_applocobjadd.Funclist = new List<Functions>();

                foreach (object o in listBoxfunctionsadd.CheckedIndices)
                {
                    Functions funcobj = new Functions();
                    funcobj.Functionid = Convert.ToInt32(((DataRowView)listBoxfunctionsadd.Items[(int)o]).Row["Functionid"].ToString());
                    funcobj.Functioncode = ((DataRowView)listBoxfunctionsadd.Items[(int)o]).Row["FunctionName"].ToString();
                    m_applocobjadd.Funclist.Add(funcobj);
                }

                string errmessage = string.Empty;
                bool b = m_applocobjadd.Save(ref errmessage);

                if (b == true)
                {
                    resetalladdcontrols();
                    MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    startsearch();
                }
                else
                {
                    {
                        MessageBox.Show(Common.GetMessage(errmessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool startvalidation()
        {
            // Validate all data filled at frontend.

            bool ret = true;
            StringBuilder errorstring = new StringBuilder();
            try
            {
                errorstring.Remove(0, errorstring.Length);

                // to validate app type selection
                if (Int32.Parse(cmbapptypeadd.SelectedValue.ToString()) == -1)
                {
                    errprovadd.SetError(cmbapptypeadd, Common.GetMessage("VAL0002", "Application Type"));
                    errorstring.AppendLine(Common.GetMessage("VAL0002", "Application Type"));
                    ret = false;
                }
                else
                {
                    errprovadd.SetError(cmbapptypeadd, String.Empty);
                }

                // to validate loctype selection
                if (Int32.Parse(cmbloctypeadd.SelectedValue.ToString()) == -1)
                {
                    errprovadd.SetError(cmbloctypeadd, Common.GetMessage("VAL0002", "Location Type"));
                    errorstring.AppendLine(Common.GetMessage("VAL0002", "Location Type"));
                    ret = false;
                }
                else
                {
                    errprovadd.SetError(cmbloctypeadd, String.Empty);
                }

                //to validate menuname,seq no., status 

                if ((Int32.Parse(cmbapptypeadd.SelectedValue.ToString()) > -1) && (Int32.Parse(cmbloctypeadd.SelectedValue.ToString()) > -1))
                {
                    //to validate menuname
                    if (menunameadd.Text.Trim().Length == 0)
                    {
                        errprovadd.SetError(menunameadd, Common.GetMessage("VAL0001", "Menu Name"));
                        errorstring.AppendLine(Common.GetMessage("VAL0001", "Menu Name"));
                        ret = false;
                    }
                    else
                    {
                        errprovadd.SetError(menunameadd, string.Empty);
                    }
                    // Validate Sequence No. Combobox
                    if ((seqnoadd.Text.Trim().Length == 0))
                    {
                        errprovadd.SetError(seqnoadd, Common.GetMessage("VAL0001", "Sequence Number"));
                        errorstring.AppendLine(Common.GetMessage("VAL0001", "Sequence Number"));
                        ret = false;
                    }
                    else
                    {
                        // Validate whether Sequence is Numeric or not.
                        if ((Validators.IsNumeric(seqnoadd.Text) == false))
                        {
                            errprovadd.SetError(seqnoadd, Common.GetMessage("VAL0001", "Numeric Sequence No"));
                            errorstring.AppendLine(Common.GetMessage("VAL0001", "Numeric Sequence No"));
                            ret = false;
                        }
                        else
                        {
                            errprovadd.SetError(seqnoadd, string.Empty);
                        }
                    }
                    // Validate status combobox
                    if (Int32.Parse(cmbstatusadd.SelectedValue.ToString()) == -1)
                    {
                        errprovadd.SetError(cmbstatusadd, Common.GetMessage("VAL0002", "Status"));
                        errorstring.AppendLine(Common.GetMessage("VAL0002", "Status"));
                        ret = false;
                    }
                    else
                    {
                        errprovadd.SetError(cmbstatusadd, String.Empty);

                    }
                }
                if (ret == false)
                {
                    MessageBox.Show(Common.ReturnErrorMessage(errorstring).ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }

                return ret;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ret;
            }
        }

        private void gotoedit(DataGridViewCellEventArgs er)
        {
            // get data from search grid to edit mode.
            try
            {
                {
                    AppLocModFunctions m_applocobjadd = new AppLocModFunctions();
                    cmbapptypeadd.SelectedValue = -1;
                    cmbloctypeadd.SelectedValue = -1;
                    m_applocobjadd = m_apploclistsearch[er.RowIndex];
                    m_recordid = m_applocobjadd.Recordid;
                    cmbapptypeadd.SelectedValue = m_applocobjadd.Apptypeid;
                    cmbloctypeadd.SelectedValue = m_applocobjadd.Locid;
                    cmbmoduleadd.SelectedValue = m_applocobjadd.Moduleid;
                    cmbParentadd.SelectedValue = m_applocobjadd.Parentid;
                    menunameadd.Text = m_applocobjadd.Menuname;
                    seqnoadd.Text = m_applocobjadd.Seqno.ToString();
                    cmbstatusadd.SelectedValue = m_applocobjadd.Status;
                    List<Functions> funclist = new List<Functions>();
                    AppLocModFunctions funcobj = new AppLocModFunctions();
                    string funcerr = string.Empty;
                    funclist = funcobj.funcSearch(m_applocobjadd.Recordid, ref funcerr);
                    foreach (object o in funclist)
                    {
                        Functions functionobj = new Functions();
                        functionobj = (Functions)o;
                        listBoxfunctionsadd.SelectedValue = functionobj.Functionid;
                        listBoxfunctionsadd.SetItemChecked(listBoxfunctionsadd.SelectedIndex, true);
                    }
                    tabControlTransaction.SelectedIndex = 1;
                    tabControlTransaction.TabPages[1].Text = "Edit";
                    cmbapptypeadd.Enabled = false;
                    cmbloctypeadd.Enabled = false;
                    cmbParentadd.Enabled = false;
                    cmbmoduleadd.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                Common.LogException(exc);
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void dgvApplocmodfunc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex > 0) && (dgvApplocmodfunc.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() != typeof(DataGridViewImageCell)))
            {
                gotoedit(e);
            }
        }

        void resetsearchcontrols()
        {
            try
            {
                cmbapptype.SelectedValue = -1;
                cmbloctype.SelectedValue = -1;
                cmbmodule.SelectedValue = -1;
                dgvApplocmodfunc.DataSource = null;
                cmbapptypeadd.SelectedValue = -1;
                cmbloctypeadd.SelectedValue = -1;
                cmbstatusadd.SelectedValue = -1;
                menunameadd.Text = string.Empty;
                seqnoadd.Text = string.Empty;
                m_recordid = 0;
                cmbapptype.Focus();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool startsearch()
        {
            // Search and display in datagrid.

            try
            {
                AppLocModFunctions applocmodobj = new AppLocModFunctions();
                applocmodobj.Apptypeid = Int32.Parse(cmbapptype.SelectedValue.ToString());
                applocmodobj.Locid = Int32.Parse(cmbloctype.SelectedValue.ToString());
                applocmodobj.Moduleid = Int32.Parse(cmbmodule.SelectedValue.ToString());
                AppLocModFunctions almf = new AppLocModFunctions();
                string errormessage = string.Empty;
                List<AppLocModFunctions> searchlist = new List<AppLocModFunctions>();
                searchlist = almf.Search(applocmodobj.Apptypeid, applocmodobj.Locid, applocmodobj.Moduleid, ref errormessage);
                if (errormessage == string.Empty)
                {
                    m_apploclistsearch.Clear();
                    m_apploclistsearch = searchlist;
                    dgvApplocmodfunc.DataSource = m_apploclistsearch;
                    dgvApplocmodfunc.ClearSelection();
                    if (m_apploclistsearch.Count == 0)
                    {
                        MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        void changeindexofapplicationorlocation()
        {

            DataTable dtparentadd;
            try
            {
                if ((Int32.Parse(cmbapptypeadd.SelectedValue.ToString()) > -1) && (Int32.Parse(cmbloctypeadd.SelectedValue.ToString()) > -1))
                {
                    // to Add items in Parent Menu in create tab

                    if (m_recordid == 0)
                    {
                        dtparentadd = Common.ParameterLookup(Common.ParameterType.MenuName, (new ParameterFilter("", Int32.Parse(cmbapptypeadd.SelectedValue.ToString()), Int32.Parse(cmbloctypeadd.SelectedValue.ToString()), 1)));
                        cmbParentadd.SelectedIndexChanged -= new EventHandler(cmbParentadd_SelectedIndexChanged);
                        cmbParentadd.DataSource = dtparentadd;
                        cmbParentadd.DisplayMember = "MenuName";
                        cmbParentadd.ValueMember = "recordId";
                    }
                    else
                    {
                        dtparentadd = Common.ParameterLookup(Common.ParameterType.MenuName, (new ParameterFilter("", Int32.Parse(cmbapptypeadd.SelectedValue.ToString()), Int32.Parse(cmbloctypeadd.SelectedValue.ToString()), Common.INT_DBNULL)));
                        cmbParentadd.SelectedIndexChanged -= new EventHandler(cmbParentadd_SelectedIndexChanged);
                        cmbParentadd.DataSource = dtparentadd;
                        cmbParentadd.DisplayMember = "MenuName";
                        cmbParentadd.ValueMember = "recordId";
                    }

                    // to add items in module combobox  in create tab
                    cmbmoduleadd.Enabled = false;
                    DataTable dtmoduleadd = Common.ParameterLookup(Common.ParameterType.Module, (new ParameterFilter("", Common.INT_DBNULL, Common.INT_DBNULL, Common.INT_DBNULL)));
                    cmbmoduleadd.SelectedIndexChanged -= new EventHandler(cmbmoduleadd_SelectedIndexChanged);
                    cmbmoduleadd.DataSource = dtmoduleadd;
                    cmbmoduleadd.DisplayMember = "ModuleCode";
                    cmbmoduleadd.ValueMember = "ModuleId";
                    listBoxfunctionsadd.DataSource = null;
                    listBoxfunctionsadd.Items.Clear();
                    cmbParentadd.SelectedIndexChanged += new EventHandler(cmbParentadd_SelectedIndexChanged);
                    cmbmoduleadd.SelectedIndexChanged += new EventHandler(cmbmoduleadd_SelectedIndexChanged);
                }
                else
                {
                    cmbmoduleadd.SelectedIndexChanged -= new EventHandler(cmbmoduleadd_SelectedIndexChanged);
                    cmbmoduleadd.DataSource = null;
                    cmbmoduleadd.Items.Clear();
                    listBoxfunctionsadd.DataSource = null;
                    listBoxfunctionsadd.Items.Clear();
                    cmbstatusadd.SelectedValue = -1;
                    cmbParentadd.DataSource = null;
                    cmbmoduleadd.SelectedIndexChanged += new EventHandler(cmbmoduleadd_SelectedIndexChanged);

                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        #endregion

    }
}
