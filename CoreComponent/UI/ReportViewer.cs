using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CoreComponent.Core.BusinessObjects;
using System.Reflection;
using Microsoft.Reporting.WinForms;
using System.Collections.Specialized;

namespace CoreComponent.UI
{
    public partial class ReportViewer : Form
    {
        private bool m_refresh = false;
        private Type m_catype;
        private ReportEntry m_creport;
        ReportEntry m_re = new ReportEntry();
        private List<ReportEntry> m_reports;
        private int m_locationType = Common.CurrentLocationTypeId;

        private const string CON_RPTLOCATIONBYTYPE = "RptLocationByType";
        private const string CON_RPTBOWITHPC = "RptBOWithPC";
        private const string CON_RPTLOCATIONBOANDWH = "RptLocationBOAndWH";
        private const string CON_RPTPCUNDERCURRENTBO = "RptPCunderCurrentBO";
        private const string CON_RPTDYNAMICLOCATIONBYTYPE = "RptDynamicLocationByType";
        private const string CON_RPTUSERNAMELOCATIONWISE = "RptUserNameLocationwise";
        private const string CON_PCLOCATION = "PCLocation";
        private const string CON_BOLOCATION = "BOlocation";
        private const string CON_RPTDYNAMICSTATE = "RptDynamicState";
        private const string CON_ISMINBRANCH = "ISMINBRANCH";

        private bool m_showBaseLocation = false;
      
        public ReportViewer(params object[] arr)
        {
            try
            {
                InitializeComponent();
                InitializeControls(arr[0].ToString());
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void InitializeControls(string reportCategory)
        {
            DataTable dtModuleName = Common.ParameterLookup(Common.ParameterType.Module, new ParameterFilter("", 0, 0, 0));
            DataRow[] foundRows;
            string expression;
            expression = "DBModuleCode = '"+reportCategory+"'";
            foundRows = dtModuleName.Select(expression);
            if(foundRows.Length > 0)
                lblPageTitle.Text = foundRows[0]["ModuleName"].ToString();

            m_catype = null;
            m_creport = null;
            m_reports = new List<ReportEntry>();
            DialogResult = DialogResult.None;
            //pParameter.Visible = false;
            XmlDocument xrpt = new XmlDocument();
            xrpt.Load(Application.StartupPath + @"\App_Data\Reports.xml");

            foreach (XmlNode rxn in xrpt.GetElementsByTagName("Report"))
            {
                ReportEntry re = new ReportEntry(rxn.Attributes["Index"].Value.Trim(), rxn.Attributes["Name"].Value.Trim(), rxn.Attributes["Path"].Value.Trim(), rxn.Attributes["Assembly"].Value.Trim(), rxn.Attributes["Class"].Value.Trim(), rxn.Attributes["ValidatorFunction"].Value.Trim(), rxn.Attributes["DataSourceFunction"].Value.Trim(), rxn.Attributes["Subreport"].Value.Trim());

                foreach (XmlNode pxn in rxn.ChildNodes)
                {
                    //re += new ReportParameter(pxn.Attributes["name"].Value.Trim(), pxn.Attributes["caption"].Value.Trim(), (ReportParamType)Enum.Parse(typeof(ReportParamType), pxn.Attributes["type"].Value.Trim()), pxn.Attributes["datasource"].Value.Trim());
                    if (pxn.Attributes["AssemblyName"] != null)
                        re += new ReportParameter(pxn.Attributes["Name"].Value.Trim(), Convert.ToInt32(pxn.Attributes["MaxLength"].Value), pxn.Attributes["Caption"].Value.Trim(), (ControlType)Enum.Parse(typeof(ControlType), pxn.Attributes["ControlType"].Value.Trim()), pxn.Attributes["DataSourceFunction"].Value.Trim(), pxn.Attributes["AssemblyName"].Value.Trim(), pxn.Attributes["ClassName"].Value.Trim(), pxn.Attributes["ParameterType"].Value.Trim(), pxn.Attributes["ParameterCode"].Value.Trim(), Convert.ToInt32(pxn.Attributes["Key1"].Value), Convert.ToInt32(pxn.Attributes["Key2"].Value), Convert.ToInt32(pxn.Attributes["Key3"].Value), Convert.ToBoolean(pxn.Attributes["ShowBaseLocation"].Value));
                    else
                        re += new ReportParameter(pxn.Attributes["Name"].Value.Trim(), Convert.ToInt32(pxn.Attributes["MaxLength"].Value), pxn.Attributes["Caption"].Value.Trim(), (ControlType)Enum.Parse(typeof(ControlType), pxn.Attributes["ControlType"].Value.Trim()), pxn.Attributes["DataSourceFunction"].Value.Trim());
                }
                m_reports.Add(re);
            }


            cmbReport.SelectedIndexChanged -= new EventHandler(cmbReport_SelectedIndexChanged);

            DataSet ds = new DataSet();
            ds.ReadXml(Application.StartupPath + @"\App_Data\Reports.xml");

            DataTable dt = ds.Tables[0].Select("LocationTypeId like '%" + m_locationType.ToString() + "%' AND (ReportCategory = '" + reportCategory + "' OR ReportCategory ='-1')").AsEnumerable().Distinct().CopyToDataTable();

            if (dt.Rows.Count > 0)
            {
                cmbReport.DataSource = dt;
                cmbReport.ValueMember = "Index";
                cmbReport.DisplayMember = "Name";
                cmbReport.SelectedIndexChanged += new EventHandler(cmbReport_SelectedIndexChanged);
            }
        }

        public ReportViewer()
        {
            try
            {
                InitializeComponent();
                InitializeControls(string.Empty);
               
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                //this.rvReport.RefreshReport();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {                
                rvReport.LocalReport.DisplayName = m_re.ReportName;
                rvReport.LocalReport.ReportPath = Application.StartupPath + m_re.ReportPath;                
                rvReport.ShowProgress = true;                
                m_creport = m_re;
                if (rvReport.LocalReport.DataSources.Count > 0)
                    rvReport.LocalReport.DataSources.Clear();

                //Point pointTree = new Point();
                //pointTree = this.PointToScreen(pParameter.Location);
                //pointTree.Y = pointTree.Y + pParameter.Height - 50;
                //pointTree.X = 920;
                //btnView.Location = pointTree;
                //pointTree.X = 25;
                //pointTree.Y = pointTree.Y + Common.GAP_BETWEEN_CONTROLS;
                //rvReport.Location = pointTree;
                
                this.Text = string.Format("Report Viewer: {0}", m_re.ReportName);
                
                object[] param = new object[m_creport.Parameters.Length + 1];
                param[0] = string.Empty;

                for (int index = 1; index < m_creport.Parameters.Length + 1; index++)
                {
                    switch (m_creport.Parameters[index - 1].ControlType)
                    {
                        case ControlType.Date:
                        case ControlType.DateTime:
                            param[index] = (tlpParameter.Controls[m_creport.Parameters[index - 1].ParameterName] as DateTimePicker).Checked ? (tlpParameter.Controls[m_creport.Parameters[index - 1].ParameterName] as DateTimePicker).Value.ToString(Common.DATE_TIME_FORMAT) : string.Empty;
                            break;
                        case ControlType.Text:
                            param[index] = (tlpParameter.Controls[m_creport.Parameters[index - 1].ParameterName] as TextBox).Text;
                            break;
                        case ControlType.DropDown:
                            param[index] = (tlpParameter.Controls[m_creport.Parameters[index - 1].ParameterName] as ComboBox).SelectedValue;
                            break;
                    }
                }
                if (m_creport.ValidatorFunction.Trim().Length == 0 || Convert.ToBoolean(m_catype.GetMethod(m_creport.ValidatorFunction).Invoke(null, param)) == true)
                {
                    bool dataLoaded = false;
                    List<ReportDataSource> lrds = new List<ReportDataSource>(m_catype.GetMethod(m_creport.DataProviderFunction).Invoke(null, param) as IEnumerable<ReportDataSource>);
                    if (lrds.Count > 0)
                    {
                        for (int index = 0; index < lrds.Count; index++, dataLoaded = true)
                            rvReport.LocalReport.DataSources.Add(lrds[index]);                    
                    }
                    if (dataLoaded)
                    {
                        if (m_refresh)
                        {
                            rvReport.RefreshReport();                            
                        }
                        m_refresh = true;
                        rvReport.SetDisplayMode(DisplayMode.PrintLayout);
                        rvReport.ZoomMode = ZoomMode.PageWidth;
                        //rvReport.ZoomPercent = 100;                           
                    }
                    else
                    {
                        rvReport.Reset();                        
                        MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbReport.SelectedValue) > 0)
                {
                    pParameter.Visible = true;
                    tlpParameter.Visible = true;
                    btnView.Enabled = true;
                    ShowControls(Convert.ToInt32(cmbReport.SelectedValue));                    
                }
                else
                {
                    //pParameter.Visible = false;
                    tlpParameter.Visible = false;
                    btnView.Enabled = false;
                }
                rvReport.Reset();               
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ShowControls(int index)
        {
            int paramperrow;
          
            ReportParameter[] rp;

            var query = (from p in m_reports where Convert.ToInt32(p.ReportID) == Convert.ToInt32(index) select p);
            if (query.ToList().Count == 1)
            {
                m_re = (ReportEntry)query.ToList()[0];
            }

            //re = m_reports[0];
            rp = m_re.Parameters;
            m_catype = Assembly.Load(m_re.ReportAssembly).GetType(m_re.ReportClass);

            paramperrow = tlpParameter.ColumnCount / 2;
            if (((rp.Length % paramperrow) == 1 && Convert.ToDecimal(rp.Length) / Convert.ToDecimal(paramperrow)==1) || (rp.Length % paramperrow==0))
                tlpParameter.RowCount = (rp.Length / paramperrow) ;
            else
                tlpParameter.RowCount = (rp.Length / paramperrow)+ 1;

            //pParameter.Height = 35 * tlpParameter.RowCount;
            for (int tindex = 0; index < tlpParameter.RowStyles.Count; index++)
            {
                tlpParameter.RowStyles[tindex].SizeType = SizeType.Absolute;
                tlpParameter.RowStyles[tindex].Height = 35;
            }

            tlpParameter.SuspendLayout();

            tlpParameter.Controls.Clear();
            //tlpParameter.Controls.Add(btnView, tlpParameter.ColumnCount - 2, tlpParameter.RowCount - 1);
            //tlpParameter.Controls.Add(btnClose, tlpParameter.ColumnCount - 1, tlpParameter.RowCount - 1);

            for (int tindex = 0, col = 0, row = 0; tindex < rp.Length; tindex++)
            {
                Control control;

                control = new Label();
                control.Text = rp[tindex].ParameterCaption;
                (control as Label).Text = (control as Label).Text + ":";
                (control as Label).TextAlign = ContentAlignment.MiddleRight;
                control.Location = new Point(0, control.Location.Y);
                tlpParameter.Controls.Add(control, col, row);
                control.Width = control.Parent.Width;
                control.Anchor = AnchorStyles.Top;
                //control.Anchor = AnchorStyles.Left | AnchorStyles.Right;

                switch (rp[tindex].ControlType)
                {
                    case ControlType.Date:
                        control = new DateTimePicker();
                        control.Name = rp[tindex].ParameterName;
                        (control as DateTimePicker).Format = DateTimePickerFormat.Custom;
                        (control as DateTimePicker).CustomFormat = Common.DTP_DATE_FORMAT;
                        (control as DateTimePicker).ShowCheckBox = true;
                        (control as DateTimePicker).Checked = true;
                        if(control.Name.ToLower().IndexOf("from") >=0)
                            (control as DateTimePicker).Value = DateTime.Today.AddDays(-7);
                        else
                            (control as DateTimePicker).Value = DateTime.Today;
                        break;

                    case ControlType.DateTime:
                        control = new DateTimePicker();
                        control.Name = rp[tindex].ParameterName;
                        (control as DateTimePicker).Format = DateTimePickerFormat.Custom;
                        (control as DateTimePicker).CustomFormat = Common.DTP_DATE_FORMAT;
                        (control as DateTimePicker).ShowCheckBox = true;
                        (control as DateTimePicker).Checked = true;
                        if (control.Name.ToLower().IndexOf("from") >= 0)
                            (control as DateTimePicker).Value = DateTime.Today.AddDays(-7);
                        else
                            (control as DateTimePicker).Value = DateTime.Today;
                        break;

                    case ControlType.DropDown:
                        control = new ComboBox();
                        control.Name = rp[tindex].ParameterName;
                        (control as ComboBox).DropDownStyle = ComboBoxStyle.DropDownList;

                        Type classType = Assembly.Load(rp[tindex].AssemblyName).GetType(rp[tindex].ClassName);
                        ParameterFilter pf = new ParameterFilter();
                        string ParameterType = rp[tindex].ParameterType;
                        pf.Code = rp[tindex].ParameterCode;
                        pf.Key01 = rp[tindex].Key1;
                        pf.Key02 = rp[tindex].Key2;
                        if (ParameterType.Contains(CON_RPTLOCATIONBYTYPE) || (ParameterType.Contains(CON_RPTBOWITHPC)) || (ParameterType.Contains(CON_RPTLOCATIONBOANDWH)) || (ParameterType.Contains(CON_RPTPCUNDERCURRENTBO)) || (ParameterType.Contains(CON_RPTDYNAMICLOCATIONBYTYPE)) || (ParameterType.Contains(CON_RPTUSERNAMELOCATIONWISE)))
                            pf.Key03 = Common.CurrentLocationId;
                        else
                            pf.Key03 = rp[tindex].Key3;
                        
                        DataTable dTable = (DataTable)classType.GetMethod(rp[tindex].DataProviderFunction).Invoke(null, new object[] { ParameterType, pf });
                        (control as ComboBox).DataSource = dTable;
                        (control as ComboBox).DropDownStyle = ComboBoxStyle.DropDownList;
                        (control as ComboBox).DisplayMember = dTable.Columns[1].ToString();
                        (control as ComboBox).ValueMember = dTable.Columns[0].ToString();


                        if (ParameterType.Contains("RptStatewiseBO"))
                        {
                            m_showBaseLocation = rp[tindex].ShowBaseLocation;
                            ((ComboBox)(tlpParameter.Controls.Find("BOState",false)[0])).SelectedIndexChanged += new EventHandler(updateBOForState);
                        }
                        if (ParameterType.Contains(CON_RPTDYNAMICLOCATIONBYTYPE) || ParameterType.Contains("RptStatewiseBO"))
                        {
                            m_showBaseLocation = rp[tindex].ShowBaseLocation;
                            (control as ComboBox).SelectedIndexChanged += new EventHandler(updatePCforBO);                            
                        }
                        if (ParameterType.Contains(CON_RPTUSERNAMELOCATIONWISE))
                        {                            
                            ((ComboBox)(tlpParameter.Controls.Find("BOlocation",false)[0])).SelectedIndexChanged += new EventHandler(FillUserName);
                            
                            ((ComboBox)(tlpParameter.Controls.Find("PClocation", false)[0])).SelectedIndexChanged += new EventHandler(FillUserName);

                            ((ComboBox)(tlpParameter.Controls.Find("PClocation", false)[0])).SelectedIndexChanged += new EventHandler(FillUserName);  
                        }
                        
                        break;

                    case ControlType.Text:
                        control = new TextBox();
                        control.Name = rp[tindex].ParameterName;
                        (control as TextBox).MaxLength = rp[tindex].MaxLength;
                        if (control.Name.Contains("Distributor"))
                        {
                            (control as TextBox).KeyDown += new KeyEventHandler(txtDistributorId_KeyDown);                            
                        }
                        else if (control.Name.Contains("itemCode"))
                        {
                            (control as TextBox).KeyDown += new KeyEventHandler(txtItem_KeyDown);
                        }
                        break;

                    default:
                        control = null;
                        break;
                }
                if (control != null)
                {
                    control.Location = new Point(0, control.Location.Y);
                    tlpParameter.Controls.Add(control, col + 1, row);
                    control.Width = control.Parent.Width;
                    if (index == 6 && rp[tindex].ParameterCode == "BUSINESSREPORTTYPE" &&
                            (control as ComboBox).Items.Count > 1)
                    {
                        (control as ComboBox).SelectedIndex = 1;
                    }
                    //control.Anchor = AnchorStyles.Top;
                    //control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                }

                col += 2;
                col %= (2 * paramperrow);
                row += (col == 0 ? 1 : 0);
            }

            tlpParameter.ResumeLayout();

            Point pointTree = new Point();
            pointTree = this.PointToScreen(pParameter.Location);
            pointTree.Y = pointTree.Y + pParameter.Height - 50;
            pointTree.X = 920;
            btnView.Location = pointTree;


            pointTree.X = 25;
            pointTree.Y = pointTree.Y + Common.GAP_BETWEEN_CONTROLS;
            rvReport.Location = pointTree;



            // Select current BO as default location and fetch aal PC beneath this BO.
            if (Common.CurrentLocationTypeId == 3)
            {
                int key3 = -1;
                if (tlpParameter.Controls.Find(CON_BOLOCATION, true).Length > 0)
                {
                    ComboBox cb = (ComboBox)tlpParameter.Controls.Find(CON_BOLOCATION, true)[0];
                    if (cb != null)
                    {
                        cb.SelectedValue = Common.CurrentLocationId.ToString();
                        key3 = Convert.ToInt32(cb.SelectedValue);
                    }
                }

                if (tlpParameter.Controls.Find(CON_PCLOCATION, true).Length > 0)
                {
                    ComboBox cb = (ComboBox)tlpParameter.Controls.Find(CON_PCLOCATION, true)[0];
                    if (cb != null)
                    {
                        DataTable dt = Common.ParameterLookup(Common.ParameterType.RptBOWithPC, new ParameterFilter("", -1, -1, key3));
                        cb.DataSource = dt;
                        cb.DisplayMember = dt.Columns[1].ToString();
                        cb.ValueMember = dt.Columns[0].ToString();
                        cb.Enabled = true;
                    }
                }
            }

            // Make combobox disabled on BO or WH location.
            if (Common.CurrentLocationTypeId > 1)
            {
                foreach (Control ctrl in tlpParameter.Controls)
                {
                    if (ctrl.GetType() == typeof(ComboBox))
                    {
                        if ((ctrl as ComboBox).Items.Count == 1)
                        {
                            (ctrl as ComboBox).Enabled = false;
                        }
                        else
                        {
                            (ctrl as ComboBox).Enabled = true;
                        }
                    }
                }
            }            
        }

        private void FillUserName(object sender, EventArgs e)
        {
            int locId = -1;
            if (Convert.ToInt32((sender as ComboBox).SelectedValue) > -1)            
                locId = Convert.ToInt32((sender as ComboBox).SelectedValue);            
            else if((sender as ComboBox).Name == "PCLocation")
                locId = Convert.ToInt32(((ComboBox)(tlpParameter.Controls.Find("BOLocation", false)[0])).SelectedValue);
            
            DataTable dt = Common.ParameterLookup(Common.ParameterType.RptUserNameLocationwise, new ParameterFilter("", -1, -1, locId));
            ((ComboBox)(tlpParameter.Controls.Find("UserId", false)[0])).DataSource = dt;
            ((ComboBox)(tlpParameter.Controls.Find("UserId", false)[0])).DisplayMember = dt.Columns[1].ToString(); ;
            ((ComboBox)(tlpParameter.Controls.Find("UserId", false)[0])).ValueMember = dt.Columns[0].ToString(); ;
        }

        private void FillLocations(object sender, EventArgs e)
        {
            int locId = -1;
            if (Convert.ToInt32((sender as ComboBox).SelectedValue) > -1)
                locId = Convert.ToInt32((sender as ComboBox).SelectedValue);
            else if ((sender as ComboBox).Name == "ISMINBRANCH")
                locId = Convert.ToInt32(((ComboBox)(tlpParameter.Controls.Find("BOLocation", false)[0])).SelectedValue);

            DataTable dt = Common.ParameterLookup(Common.ParameterType.RptUserNameLocationwise, new ParameterFilter("", -1, -1, locId));
            ((ComboBox)(tlpParameter.Controls.Find("UserId", false)[0])).DataSource = dt;
            ((ComboBox)(tlpParameter.Controls.Find("UserId", false)[0])).DisplayMember = dt.Columns[1].ToString(); ;
            ((ComboBox)(tlpParameter.Controls.Find("UserId", false)[0])).ValueMember = dt.Columns[0].ToString(); ;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                CoreComponent.Core.BusinessObjects.Common.CloseThisForm(this);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void updateBOForState(object sender, EventArgs e)
        {
            DataTable dt = null;
            int key3=0;
            int key1=0;

            if (((ComboBox)sender).SelectedIndex >= 0)
            {
                if (((ComboBox)sender).Name == "BOState" ) 
                {
                     key3 = Convert.ToInt32( ((ComboBox)sender).SelectedValue);
                     //key1 = Convert.ToInt32(((ComboBox)sender).SelectedValue);
                }
                //if (((ComboBox)sender).Name == "DLCP")
                //{
                //     key1 = Convert.ToInt32(((ComboBox)sender).SelectedValue);
                //}
                dt=Common.ParameterLookup(Common.ParameterType.RptStatewiseBO,new ParameterFilter("",1,-1,key3));
                Control myControl = new Control();
                foreach (Control c in tlpParameter.Controls)
                    if (c.Name == "BOlocation")
                    {
                        myControl = c;
                        (myControl as ComboBox).DataSource = dt;
                        (myControl as ComboBox).DisplayMember = dt.Columns[1].ToString();
                        (myControl as ComboBox).ValueMember = dt.Columns[0].ToString();
                        break;
                    }
            }
        }

        private void updatePCforBO(object sender,EventArgs e)
        {
            if(((ComboBox)sender).SelectedIndex >= 0)
            {
                DataTable dt = null;
               int key3 = Convert.ToInt32(((ComboBox)sender).SelectedValue);
               // if report is for PUC Acct Summary then only PC.
               if (!m_showBaseLocation)
               {
                  dt  = Common.ParameterLookup(Common.ParameterType.RptPCunderCurrentBO, new ParameterFilter("", 4, -1, key3));
               }
               // if report is not for PUC Acct Summary then BO and their PC.
               else
               {
                   dt = Common.ParameterLookup(Common.ParameterType.RptBOWithPC, new ParameterFilter("", -1, -1, key3));
               }
               Control myControl = new Control();
               foreach (Control c in tlpParameter.Controls )
                   if (c.Name == CON_PCLOCATION)
                   {
                       myControl = c;
                       (myControl as ComboBox).DataSource = dt;
                       (myControl as ComboBox).DisplayMember = dt.Columns[1].ToString();
                       (myControl as ComboBox).ValueMember = dt.Columns[0].ToString();
                       break;
                   }
            }
        }

        private void txtDistributorId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {                   
                        System.Collections.Specialized.NameValueCollection nvc = new NameValueCollection();
                        nvc.Add("DistributorStatus", "2");
                        CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.DistributorSearch, nvc);
                        CoreComponent.BusinessObjects.Distributor _distributor = (CoreComponent.BusinessObjects.Distributor)objfrmSearch.ReturnObject;
                        objfrmSearch.ShowDialog();
                        _distributor = (CoreComponent.BusinessObjects.Distributor)objfrmSearch.ReturnObject;

                        if (_distributor != null)
                        {
                            (sender as TextBox).Text = _distributor.DistributorId.ToString();
                        }                    
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rvReport_RenderingComplete(object sender, RenderingCompleteEventArgs e)
        {
            try
            {
                btnView.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnView.Enabled = true;
            }
        }

        private void rvReport_RenderingBegin(object sender, CancelEventArgs e)
        {
            try
            {
                btnView.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnView.Enabled = true;
            }
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    System.Collections.Specialized.NameValueCollection nvc = new NameValueCollection();
                    CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.ItemMaster, nvc);
                    objfrmSearch.ShowDialog();
                    CoreComponent.MasterData.BusinessObjects.ItemDetails _Item = (CoreComponent.MasterData.BusinessObjects.ItemDetails)objfrmSearch.ReturnObject;
                    if (_Item != null)
                    {
                        (sender as TextBox).Text = _Item.ItemCode.ToString();                        
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblPageTitle_Click(object sender, EventArgs e)
        {

        }
        
    }

    internal class ReportEntry
    {
        #region Member Variables

        private string m_id, m_name, m_path, m_assembly, m_class, m_validator, m_datasource, m_subreport;
        private List<ReportParameter> m_params;

        #endregion

        #region Constructors
        public ReportEntry()
        {
        }
        public ReportEntry(string id, string name, string path, string assembly, string member, string validator, string datasource, string subreport)
        {
            m_id = id;
            m_name = name;
            m_path = path;
            m_assembly = assembly;
            m_class = member;
            m_validator = validator;
            m_datasource = datasource;
            m_subreport = subreport;
            m_params = new List<ReportParameter>();
        }

        #endregion

        #region Properties

        public string ReportID
        {
            get { return m_id; }
        }

        public string ReportName
        {
            get { return m_name; }
        }

        public string ReportPath
        {
            get { return m_path; }
        }

        public string ReportAssembly
        {
            get { return m_assembly; }
        }

        public string ReportClass
        {
            get { return m_class; }
        }

        public string ValidatorFunction
        {
            get { return m_validator; }
        }

        public string DataProviderFunction
        {
            get { return m_datasource; }
        }

        public string SubReport
        {
            get { return m_subreport; }
        }
        public ReportParameter[] Parameters
        {
            get { return m_params.ToArray(); }
        }



        #endregion

        #region Operators

        public static ReportEntry operator +(ReportEntry parent, ReportParameter child)
        {
            if (parent != null && child != null)
                parent.m_params.Add(child);
            return parent;
        }

        #endregion
    }

    internal enum ControlType
    {
        Date,
        DateTime,
        DropDown,
        Text
    }

    internal class ReportParameter
    {
        #region Member Variables

        private ControlType m_type;
        private string m_name, m_caption, m_dataprovider, m_assemblyName, m_className, m_parameterCode, m_parameterType;
        int m_key1, m_key2, m_key3, m_length;
        bool m_ShowBaseLocation;

        #endregion

        #region Constructors

        public ReportParameter(string name, int maxLength, string caption, ControlType type, string dataprovider)
        {
            m_name = name;
            m_caption = caption;
            m_type = type;
            m_dataprovider = dataprovider;
            m_length = maxLength;
        }
        public ReportParameter(string name, int maxLength, string caption, ControlType type, string dataprovider, string assemblyName, string className, string parameterType, string parameterCode, int key1, int key2, int key3, bool showBaseLocation)
        {
            m_name = name;
            m_length = maxLength;
            m_caption = caption;
            m_type = type;
            m_dataprovider = dataprovider;
            m_assemblyName = assemblyName;
            m_className = className;
            m_key1 = key1;
            m_key2 = key2;
            m_key3 = key3;
            m_parameterCode = parameterCode;
            m_parameterType = parameterType;
            m_ShowBaseLocation = showBaseLocation;
        }

        #endregion

        #region Properties
        public int MaxLength
        {
            get { return m_length; }
        }

        public string AssemblyName
        {
            get { return m_assemblyName; }
        }

        public string ClassName
        {
            get { return m_className; }
        }

        public string ParameterCode
        {
            get { return m_parameterCode; }
        }
        public string ParameterType
        {
            get { return m_parameterType; }
        }

        public int Key1
        {
            get { return m_key1; }
        }
        public int Key2
        {
            get { return m_key2; }
        }
        public int Key3
        {
            get { return m_key3; }
        }

        public string ParameterName
        {
            get { return m_name; }
        }

        public string ParameterCaption
        {
            get { return m_caption; }
        }

        public ControlType ControlType
        {
            get { return m_type; }
        }

        public string DataProviderFunction
        {
            get { return m_dataprovider; }
        }

        public bool ShowBaseLocation
        {
            get { return m_ShowBaseLocation; }
        }

        #endregion



       
    }
}
