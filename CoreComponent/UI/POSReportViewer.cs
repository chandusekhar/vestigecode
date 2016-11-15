using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using Microsoft.Reporting.WinForms;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects.Reports;

namespace CoreComponent.UI
{
    public partial class POSReportViewer : Form
    {
        #region Constants

        private const int MAXREPORTPERPAGE = 8;

        #endregion

        #region Member Variables

        ReportEntry m_re = new ReportEntry();
        private int m_pageIndex;
        private bool m_refresh = false;
        private Type m_catype;
        private ReportEntry m_creport;
        private List<ReportEntry> m_reports;

        private const string CON_RPTLOCATIONBYTYPE = "RptLocationByType";
        private const string CON_RPTBOWITHPC = "RptBOWithPC";
        private const string CON_RPTLOCATIONBOANDWH = "RptLocationBOAndWH";
        private const string CON_RPTPCUNDERCURRENTBO = "RptPCunderCurrentBO";
        private const string CON_RPTDYNAMICLOCATIONBYTYPE = "RptDynamicLocationByType";
        private const string CON_PCLOCATION = "PCLocation";
        private const string CON_RPTDYNAMICBOWITHPCLOCATION = "RptDynamicBOwithPCLocation";
        private const string CON_RPTDYNAMICPCLOCATION = "RptDynamicPCLocation";
        private const string CON_RPTUSERNAMELOCATIONWISE = "RptUserNameLocationwise";
        private const string CON_RPTSTATE = "RptDynamicState";
        #endregion
        public POSReportViewer()
        {
            try
            {
                InitializeComponent();
                m_pageIndex = 0;
                m_catype = null;
                m_creport = null;
                m_reports = new List<ReportEntry>();
                DialogResult = DialogResult.None;

                XmlDocument xrpt = new XmlDocument();
                xrpt.Load(Application.StartupPath + @"/App_Data/POSReports.xml");

                foreach (XmlNode rxn in xrpt.GetElementsByTagName("Report"))
                {
                    if (Common.IsMiniBranchLocation == 1)
                    {
                        if (rxn.Attributes["IsPosVisible"].Value.Trim() == "true" && rxn.Attributes["IsMiniBranchVisible"].Value.Trim() == "true")
                        {
                            ReportEntry re = new ReportEntry(rxn.Attributes["Index"].Value.Trim(), rxn.Attributes["Name"].Value.Trim(), rxn.Attributes["Path"].Value.Trim(), rxn.Attributes["Assembly"].Value.Trim(), rxn.Attributes["Class"].Value.Trim(), rxn.Attributes["ValidatorFunction"].Value.Trim(), rxn.Attributes["DataSourceFunction"].Value.Trim(), rxn.Attributes["Subreport"].Value.Trim());

                            foreach (XmlNode pxn in rxn.ChildNodes)
                            {
                                //re += new ReportParameter(pxn.Attributes["name"].Value.Trim(), pxn.Attributes["caption"].Value.Trim(), (ReportParamType)Enum.Parse(typeof(ReportParamType), pxn.Attributes["type"].Value.Trim()), pxn.Attributes["datasource"].Value.Trim());
                                if (pxn.Attributes["AssemblyName"] != null)
                                    re += new ReportParameter(pxn.Attributes["Name"].Value.Trim(), Convert.ToInt32(pxn.Attributes["MaxLength"].Value), pxn.Attributes["Caption"].Value.Trim(), (ControlType)Enum.Parse(typeof(ControlType), pxn.Attributes["ControlType"].Value.Trim()), pxn.Attributes["DataSourceFunction"].Value.Trim(), pxn.Attributes["AssemblyName"].Value.Trim(), pxn.Attributes["ClassName"].Value.Trim(), pxn.Attributes["ParameterType"].Value.Trim(), pxn.Attributes["ParameterCode"].Value.Trim(), Convert.ToInt32(pxn.Attributes["Key1"].Value), Convert.ToInt32(pxn.Attributes["Key2"].Value), Convert.ToInt32(pxn.Attributes["Key3"].Value), true);
                                else
                                    re += new ReportParameter(pxn.Attributes["Name"].Value.Trim(), Convert.ToInt32(pxn.Attributes["MaxLength"].Value), pxn.Attributes["Caption"].Value.Trim(), (ControlType)Enum.Parse(typeof(ControlType), pxn.Attributes["ControlType"].Value.Trim()), pxn.Attributes["DataSourceFunction"].Value.Trim());
                            }
                            m_reports.Add(re);
                        }
                    }

                    else
                    {
                        if (rxn.Attributes["IsPosVisible"].Value.Trim() == "true")
                        {
                            ReportEntry re = new ReportEntry(rxn.Attributes["Index"].Value.Trim(), rxn.Attributes["Name"].Value.Trim(), rxn.Attributes["Path"].Value.Trim(), rxn.Attributes["Assembly"].Value.Trim(), rxn.Attributes["Class"].Value.Trim(), rxn.Attributes["ValidatorFunction"].Value.Trim(), rxn.Attributes["DataSourceFunction"].Value.Trim(), rxn.Attributes["Subreport"].Value.Trim());

                            foreach (XmlNode pxn in rxn.ChildNodes)
                            {
                                //re += new ReportParameter(pxn.Attributes["name"].Value.Trim(), pxn.Attributes["caption"].Value.Trim(), (ReportParamType)Enum.Parse(typeof(ReportParamType), pxn.Attributes["type"].Value.Trim()), pxn.Attributes["datasource"].Value.Trim());
                                if (pxn.Attributes["AssemblyName"] != null)
                                    re += new ReportParameter(pxn.Attributes["Name"].Value.Trim(), Convert.ToInt32(pxn.Attributes["MaxLength"].Value), pxn.Attributes["Caption"].Value.Trim(), (ControlType)Enum.Parse(typeof(ControlType), pxn.Attributes["ControlType"].Value.Trim()), pxn.Attributes["DataSourceFunction"].Value.Trim(), pxn.Attributes["AssemblyName"].Value.Trim(), pxn.Attributes["ClassName"].Value.Trim(), pxn.Attributes["ParameterType"].Value.Trim(), pxn.Attributes["ParameterCode"].Value.Trim(), Convert.ToInt32(pxn.Attributes["Key1"].Value), Convert.ToInt32(pxn.Attributes["Key2"].Value), Convert.ToInt32(pxn.Attributes["Key3"].Value), true);
                                else
                                    re += new ReportParameter(pxn.Attributes["Name"].Value.Trim(), Convert.ToInt32(pxn.Attributes["MaxLength"].Value), pxn.Attributes["Caption"].Value.Trim(), (ControlType)Enum.Parse(typeof(ControlType), pxn.Attributes["ControlType"].Value.Trim()), pxn.Attributes["DataSourceFunction"].Value.Trim());
                            }
                            m_reports.Add(re);
                        }
                    }


                }

                SetReportSelectorPage();
                DataSet ds = new DataSet();
                int currentLocationType = Common.CurrentLocationTypeId;
                ds.ReadXml(Application.StartupPath + @"\App_Data\POSReports.xml");
                DataRow[] rowCollection = ds.Tables[0].Select("LocationTypeId like '%" + currentLocationType.ToString() + "%'");
                int c = rowCollection.Count();
                if (c > 0)
                {
                    DataTable dt = ds.Tables[0].Select("LocationTypeId like '%" + currentLocationType.ToString() + "%'").AsEnumerable().Distinct().CopyToDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        int i = Convert.ToInt32(dt.Rows[0]["Report_Id"]);

                        //string i = dt.Rows[0]["Id"].ToString();
                        ShowControls(i);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RVButton_Click(object sender, EventArgs e)
        {
            try
            {
                switch (((Button)sender).Name)
                {
                    case "btnUp":
                        --m_pageIndex;
                        SetReportSelectorPage();
                        break;

                    case "btnDown":
                        ++m_pageIndex;
                        SetReportSelectorPage();
                        break;

                    case "btnClose":
                        this.Close();
                        break;

                    case "btnView":
                        rvReport.RefreshReport();
                        rvReport.Reset();

                        rvReport.LocalReport.DataSources.Clear();
                        rvReport.LocalReport.DisplayName = m_re.ReportName;
                        rvReport.LocalReport.ReportPath = Application.StartupPath + m_re.ReportPath;
                        rvReport.ShowProgress = true;
                        m_creport = m_re;
                        if (rvReport.LocalReport.DataSources.Count > 0)
                            rvReport.LocalReport.DataSources.Clear();

                        if (m_creport != null && m_catype != null)
                        {
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

                                    case ControlType.DropDown:
                                        param[index] = (tlpParameter.Controls[m_creport.Parameters[index - 1].ParameterName] as ComboBox).SelectedValue;
                                        break;

                                    case ControlType.Text:
                                        param[index] = tlpParameter.Controls[m_creport.Parameters[index - 1].ParameterName].Text.Trim();
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
                        break;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }            
            
        private void SetReportSelectorPage()
        {
            for (int index = 1; index <= MAXREPORTPERPAGE; index++)
            {
                tsReport.Items["tsb" + index.ToString("00")].Visible = false;
                if (index < MAXREPORTPERPAGE) tsReport.Items["tss" + index.ToString("00")].Visible = false;
            }

            DataSet ds = new DataSet();
            int currentLocationType = Common.CurrentLocationTypeId;
            ds.ReadXml(Application.StartupPath + @"\App_Data\POSReports.xml");

            DataRow[] rowCollection = ds.Tables[0].Select("LocationTypeId like '%" + currentLocationType.ToString() + "%'");
            int c = rowCollection.Count();
            if (c > 0)
            {
                DataTable dt = ds.Tables[0].Select("LocationTypeId like '%" + currentLocationType.ToString() + "%'").AsEnumerable().Distinct().CopyToDataTable();

                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int index = MAXREPORTPERPAGE * m_pageIndex, bndex = 1; index < m_reports.Count && bndex <= MAXREPORTPERPAGE; index++, bndex++)
                    {
                        if (dt.Rows[row]["Index"].ToString() == m_reports[index].ReportID)
                        {
                            ToolStripButton tsb = (tsReport.Items["tsb" + bndex.ToString("00")] as ToolStripButton);
                            tsb.Text = m_reports[index].ReportName.Replace(" ", "\n");
                            tsb.Tag = index;
                            tsb.Visible = true;
                            if (bndex > 1) tsReport.Items["tss" + (bndex - 1).ToString("00")].Visible = true;
                            (tsReport.Items["tsb" + bndex.ToString("00")] as ToolStripButton).Click += new EventHandler(ReportTSB_Click);
                        }
                    }
                }
            }

            btnUp.Enabled = (m_pageIndex > 0);
            btnDown.Enabled = (MAXREPORTPERPAGE * (m_pageIndex + 1) < m_reports.Count);
        }

        private void ShowControls(int index)
        {
            m_re = null;
            rvReport.LocalReport.DataSources.Clear();

            int paramperrow;

            ReportParameter[] rp;
            var query = (from p in m_reports where Convert.ToInt32(p.ReportID) == Convert.ToInt32(index) + 1 select p);
           // var query = (from p in m_reports where Convert.ToInt32(p.ReportID) == Convert.ToInt32(index)+1  select p);
            if (query.ToList().Count == 1)
            {
                m_re = (ReportEntry)query.ToList()[0];
            }

            //re = m_reports[0];
            rp = m_re.Parameters;
            m_catype = Assembly.Load(m_re.ReportAssembly).GetType(m_re.ReportClass);

            paramperrow = tlpParameter.ColumnCount / 2;
            if (((rp.Length % paramperrow) == 1 && Convert.ToDecimal(rp.Length) / Convert.ToDecimal(paramperrow) == 1) || (rp.Length % paramperrow == 0))
                tlpParameter.RowCount = (rp.Length / paramperrow);
            else
                tlpParameter.RowCount = (rp.Length / paramperrow) + 1;

            //pParameter.Height = 35 * tlpParameter.RowCount;
            for (int tindex = 0; tindex < tlpParameter.RowStyles.Count; tindex++)
            {
                tlpParameter.RowStyles[tindex].SizeType = SizeType.Absolute;
                tlpParameter.RowStyles[tindex].Height = 35;
            }

            tlpParameter.SuspendLayout();

            tlpParameter.Controls.Clear();
            for (int tindex = 0, col = 0, row = 0; tindex < rp.Length; tindex++)
            {
                Control control;

                control = new Label();
                control.Text = rp[tindex].ParameterCaption;
                (control as Label).Text = (control as Label).Text + ":";
                (control as Label).Font = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Bold); 
                (control as Label).TextAlign = ContentAlignment.MiddleRight;
                control.Location = new Point(0, control.Location.Y);
                tlpParameter.Controls.Add(control, col, row);
                control.Width = control.Parent.Width;
                control.Anchor = AnchorStyles.Top;

                switch (rp[tindex].ControlType)
                {
                    case ControlType.Date:
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
                        if (ParameterType.Contains(CON_RPTLOCATIONBYTYPE) || (ParameterType.Contains(CON_RPTBOWITHPC)) || (ParameterType.Contains(CON_RPTLOCATIONBOANDWH)) || (ParameterType.Contains(CON_RPTPCUNDERCURRENTBO)) || (ParameterType.Contains(CON_RPTDYNAMICLOCATIONBYTYPE)) || (ParameterType.Contains(CON_RPTDYNAMICBOWITHPCLOCATION)) || (ParameterType.Contains(CON_RPTDYNAMICPCLOCATION)) || (ParameterType.Contains(CON_RPTUSERNAMELOCATIONWISE)))
                            pf.Key03 = Common.CurrentLocationId;
                        else
                            pf.Key03 = rp[tindex].Key3;

                        DataTable dTable = (DataTable)classType.GetMethod(rp[tindex].DataProviderFunction).Invoke(null, new object[] { ParameterType, pf });
                        (control as ComboBox).DataSource = dTable;
                        (control as ComboBox).DropDownStyle = ComboBoxStyle.DropDownList;
                        (control as ComboBox).DisplayMember = dTable.Columns[1].ToString();
                        (control as ComboBox).ValueMember = dTable.Columns[0].ToString();

                        if (ParameterType.Contains(CON_RPTDYNAMICLOCATIONBYTYPE))
                            (control as ComboBox).SelectedIndexChanged += new EventHandler(updatePCforBO);

                        if (ParameterType.Contains(CON_RPTUSERNAMELOCATIONWISE))
                        {
                           // ((ComboBox)(tlpParameter.Controls.Find("BOlocation", false)[0])).SelectedIndexChanged += new EventHandler(FillUserName);

                            ((ComboBox)(tlpParameter.Controls.Find("PClocation", false)[0])).SelectedIndexChanged += new EventHandler(FillUserName);
                        }

                        break;

                    case ControlType.Text:
                        control = new TextBox();
                        control.Name = rp[tindex].ParameterName;
                        (control as TextBox).MaxLength = rp[tindex].MaxLength;
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
                }

                col += 2;
                col %= (2 * paramperrow);
                row += (col == 0 ? 1 : 0);
            }

            tlpParameter.ResumeLayout();

            // Commented by Anubhav, this code doesnot work when parameter count is 3.
            // -->
            //tlpParameter.Controls.Add(btnView, tlpParameter.ColumnCount - 2, tlpParameter.RowCount - 1);
            //tlpParameter.Controls.Add(btnClose, tlpParameter.ColumnCount - 1, tlpParameter.RowCount - 1);

            tlpParameter.Controls.Add(btnView, 4, 1);
            tlpParameter.Controls.Add(btnClose, 5, 1);

            pParameter.Height = tlpParameter.Height;

            //if (Common.CurrentLocationTypeId == 4)
            //{
            //    foreach (Control ctrl in tlpParameter.Controls)
            //    {
            //        if (ctrl.GetType() == typeof(ComboBox))
            //        {
            //            if ((ctrl as ComboBox).Items.Count <= 1)
            //            {
            //                (ctrl as ComboBox).Enabled = false;
            //            }
            //            else
            //            {
            //                (ctrl as ComboBox).Enabled = true;
            //            }
            //        }
            //    }
            //}

            this.AcceptButton = btnView;
            this.CancelButton = btnClose;

        }

        private void ReportTSB_Click(object sender, EventArgs e)
        {
            try
            {
                rvReport.LocalReport.DataSources.Clear();
                rvReport.Reset();
                rvReport.Refresh();
                ShowControls((int)((ToolStripButton)sender).Tag);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
           
        }

        private void FillUserName(object sender, EventArgs e)
        {
            int locId = -1;
            if (Convert.ToInt32((sender as ComboBox).SelectedValue) > -1)
            {
                locId = Convert.ToInt32((sender as ComboBox).SelectedValue);
            }
            else
                locId = Common.CurrentLocationId;
            
            DataTable dt = Common.ParameterLookup(Common.ParameterType.RptUserNameLocationwise, new ParameterFilter("", -1, -1, locId));

            ((ComboBox)(tlpParameter.Controls.Find("UserId", false)[0])).DataSource = dt;
            ((ComboBox)(tlpParameter.Controls.Find("UserId", false)[0])).DisplayMember = dt.Columns[1].ToString(); ;
            ((ComboBox)(tlpParameter.Controls.Find("UserId", false)[0])).ValueMember = dt.Columns[0].ToString(); ;
        
        }

        private void updatePCforBO(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex >= 0)
            {
                DataTable dt = null;
                int key3 = Convert.ToInt32(((ComboBox)sender).SelectedValue);
                dt = Common.ParameterLookup(Common.ParameterType.RptBOWithPC, new ParameterFilter("", -1, -1, key3));
                Control myControl = new Control();
                foreach (Control c in tlpParameter.Controls)
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

        private void rvReport_RenderingBegin(object sender, CancelEventArgs e)
        {
            try
            {
                btnView.Enabled = false;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnView.Enabled = true;
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
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnView.Enabled = true;
            }
        }
      
    }

 
}
