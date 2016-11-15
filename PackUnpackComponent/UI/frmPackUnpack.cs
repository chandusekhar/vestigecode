using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects;
using PackUnpackComponent.BusinessObjects;
using System.Collections.Specialized;
using AuthenticationComponent.BusinessObjects;

namespace PackUnpackComponent.UI
{
    public partial class frmPackUnpack :CoreComponent.Core.UI.BlankTemplate
    {
        #region Variables

        List<PUHeader> m_listPUHeader;
        List<PUDetail> m_listPUDetail;
        List<CompositeItem> m_ListCompositeItem;
       // List<CompositeBOM> m_ListCompositeBOM;
        private CompositeItem m_CompositeItem;
        private string m_CompositeItemCode;
        private string m_PUno;
        private Boolean m_RecordFound = false;
        private int m_SelVal_PackUnpack = 0;
        private int m_AvailableComQuantity = 0;
        private Boolean m_ProcessClick = false;
        private Boolean m_isSaveAvailable = false;
        private Boolean m_isSearchAvailable = false;
        private int m_userId = Authenticate.LoggedInUser.UserId;
        private string strUserName = Authenticate.LoggedInUser.UserName;

        private string strLocationCode = Common.LocationCode;
        private int m_currentLocationId = Common.CurrentLocationId;
        private int m_locationType = Common.CurrentLocationTypeId;

        DataSet m_printDataSet = null;
        #endregion
        #region c'tor
        public frmPackUnpack()
        {
            InitializeComponent();

            m_isSaveAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, PUCommon.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
            m_isSearchAvailable = Authenticate.IsFunctionAccessible(strUserName, strLocationCode, PUCommon.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);


        }
        #endregion      
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region Initialize Control
        
         /// <summary>
      /// Fill control with default value 
      /// bind combos
      /// </summary>
        private void InitializeControls()
        {
            try
            {
                dtpFromDate.CustomFormat = Common.DTP_DATE_FORMAT;
                dtpToDate.CustomFormat = Common.DTP_DATE_FORMAT;
                dtpPUDate.CustomFormat = Common.DTP_DATE_FORMAT;
                dtpMfgDate.CustomFormat = Common.DTP_DATE_FORMAT;
                dtpExpDate.CustomFormat = Common.DTP_DATE_FORMAT;
                
                DateTime dtAssignDate = Convert.ToDateTime(DateTime.Now.ToString(Common.DATE_TIME_FORMAT));
                dtpFromDate.MaxDate = dtAssignDate;
                dtpToDate.MaxDate = dtAssignDate;
                dtpPUDate.MaxDate = dtAssignDate;
                dtpMfgDate.MaxDate = dtAssignDate;

                dtpFromDate.Value = dtAssignDate;
                dtpToDate.Value = dtAssignDate;
                dtpPUDate.Value = dtAssignDate;
                dtpMfgDate.Value = dtAssignDate;
                txtSerachVouWithItemCode.Focus();
                dtpExpDate.MaxDate = DateTime.MaxValue.AddYears(-2);
                dtpFromDate.Checked = false;
                dtpToDate.Checked = false;
                this.DefineGridView();
                this.cmbPackUnpack.Focus();
               
                Common.BindParamComboBox(cmbPackUnpack, PUCommon.COMBO_PACKUNPACK , 0, 0, 0);
                Common.BindParamComboBox(cmbSearchPU, PUCommon.COMBO_PACKUNPACK, 0, 0, 0);

                if ((m_isSearchAvailable==true) && (m_isSaveAvailable==true))
                {

                  
                }
                else if ((m_isSearchAvailable == true) && (m_isSaveAvailable == false))
                {
                    tabControlHierarchy.TabPages.Remove(tabControlHierarchy.TabPages[1]);
                }
                else if ((m_isSearchAvailable == false) && (m_isSaveAvailable == true))
                {
                    tabControlHierarchy.TabPages.Remove(tabControlHierarchy.TabPages[0]);
                   
                }
                else
                {
                    tabControlHierarchy.TabPages.Remove(tabControlHierarchy.TabPages[0]);
                    tabControlHierarchy.TabPages.Remove(tabControlHierarchy.TabPages[1]);

                }
                lblPageTitle.Text = "Pack/Unpack";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  
        /// <summary>
        /// Read grid defination from defin in xml under app data
        /// </summary>
        private void DefineGridView()
        {
            try
            {
                dgvPUHeader.AutoGenerateColumns = false;
                dgvPUHeader.DataSource = null;
                DataGridView dgvPUHeaderTemp =
                    Common.GetDataGridViewColumns(dgvPUHeader,
                    Environment.CurrentDirectory + PUCommon.GRIDVIEW_DEFINITION_XML_PATH);

               dgvPUDetail.AutoGenerateColumns = false;
               dgvPUDetail.DataSource = null;
               DataGridView dgvPUDetailTemp =
                    Common.GetDataGridViewColumns(dgvPUDetail,
                    Environment.CurrentDirectory + PUCommon.GRIDVIEW_DEFINITION_XML_PATH);


                   dgvConstituentPack.AutoGenerateColumns = false;
                   dgvConstituentPack.DataSource = null;
                   DataGridView dgvConstituentPackTemp =
                    Common.GetDataGridViewColumns(dgvConstituentPack,
                    Environment.CurrentDirectory + PUCommon.GRIDVIEW_DEFINITION_XML_PATH);

               dgvConstituentUnpack.AutoGenerateColumns = false;
               dgvConstituentUnpack.DataSource = null;
               DataGridView dgvConstituentUnpackTemp =
                    Common.GetDataGridViewColumns(dgvConstituentUnpack,
                    Environment.CurrentDirectory + PUCommon.GRIDVIEW_DEFINITION_XML_PATH);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        private void frmPackUnpack_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitializeControls();

            }
            catch (Exception)
            {
                
                throw;
            }

        }
        #region Function,Event Use in Search Tab
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchPUHeader_Detail();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void SearchPUHeader_Detail()
        {
            try
            {
                m_listPUHeader = null;
                m_listPUDetail = null;
                m_PUno = "0";
                dgvPUDetail.DataSource = null;
                dgvPUHeader.DataSource = null;

                PUCommon objPUCommon = new PUCommon();
                if (dtpFromDate.Checked)
                    objPUCommon.FromDate = dtpFromDate.Value.ToString(Common.DATE_TIME_FORMAT);
                else
                    objPUCommon.FromDate = string.Empty;

                if (dtpToDate.Checked)
                    objPUCommon.ToDate = dtpToDate.Value.ToString(Common.DATE_TIME_FORMAT);
                else
                    objPUCommon.ToDate = string.Empty;

                objPUCommon.SearchParam = PUCommon.SEARCH_PUVOUCHER;
                objPUCommon.LocationId = m_currentLocationId;
                objPUCommon.ItemCode = txtSerachVouWithItemCode.Text.Trim();

                switch (Convert.ToInt32(cmbSearchPU.SelectedValue)) 
                {
                    case -1:
                        objPUCommon.PU_All = PUCommon.All_PURecords;
                        break;
                    case 0:
                        objPUCommon.PU_All = PUCommon.Only_PackRecords;
                        break;
                    case 1:
                        objPUCommon.PU_All = PUCommon.Only_UnPackRecords;
                        break;
                    default:
                        objPUCommon.PU_All = PUCommon.All_PURecords;
                        break;
                }


                string errMsg = string.Empty;
                m_listPUHeader = objPUCommon.Search_Header_Detail(Common.ToXml(objPUCommon), PUCommon.PUVocher_SEARCH, ref errMsg);

                //Bind Grid
                BindGridView_Header();
            }
            catch { throw; }
        }

        /// <summary>
        /// Bind Header
        /// </summary>
        private void BindGridView_Header()
        {
            try
            {
                dgvPUHeader.DataSource = new List<PUHeader>();
                if (m_listPUHeader.Count > 0)
                {
                    dgvPUHeader.DataSource = m_listPUHeader;
                    dgvPUHeader.Rows[0].Selected = true;
                    dgvPUHeader_CurrentCellChanged(null, null);

                }
                else
                {
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void BindGridView_Constituent()
        {
            try
            {
                dgvConstituentPack.DataSource = new List<CompositeBOM>();
                if (m_CompositeItem.CompositeDetail.Count > 0)
                {
                    dgvConstituentPack.DataSource = m_CompositeItem.CompositeDetail;
                    dgvConstituentUnpack.DataSource = m_CompositeItem.CompositeDetail;

                    lblDisplayItemName.Text = m_CompositeItem.ItemName;
                    dtpExpDate.Value = Convert.ToDateTime(m_CompositeItem.ExpDate);
                   
                    IsRecordFind(true);
                    m_RecordFound = true;

                }
                else
                {
                    IsRecordFind(false);
                    m_RecordFound = false;
                    Reset_CreateTab();
                    MessageBox.Show(Common.GetMessage("VAL0127"), Common.GetMessage("10001"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Bind Header Detail
        /// </summary>
        private void BindGridView_Composite()
        {
            try
            {

                if (m_ListCompositeItem.Count > 0)
                {
                    if (m_ListCompositeItem.Count == 1)
                    {

                    }
                    else
                    {
                        //More Then One record
                        MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"),
                                                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates DataSet for Printing PU Screen report
        /// </summary>
        private void CreatePrintDataSet(int index)
        {
            string errorMessage = string.Empty;
            PUCommon objPUCommon = new PUCommon();
            objPUCommon.SearchParam = PUCommon.SEARCH_PUVOUCHER;
            objPUCommon.LocationId = m_currentLocationId;
            objPUCommon.FromDate = "";
            objPUCommon.ToDate = "";
            objPUCommon.PU_All = -1;
            objPUCommon.PUNo = dgvPUHeader.CurrentRow.Cells["PUNo"].Value.ToString();
            objPUCommon.ItemCode = dgvPUHeader.CurrentRow.Cells["ItemCode"].Value.ToString();
            m_printDataSet = objPUCommon.Search_Header_Detail_DatSet(Common.ToXml(objPUCommon), PUCommon.PUVocher_SEARCH, ref errorMessage);
            m_printDataSet.Tables[0].Columns.Add(new DataColumn("PUDateString", Type.GetType("System.String")));
            m_printDataSet.Tables[0].Columns.Add(new DataColumn("MfgDateString", Type.GetType("System.String")));
            m_printDataSet.Tables[0].Columns.Add(new DataColumn("ExpDateString", Type.GetType("System.String")));
            for (int i = 0; i < m_printDataSet.Tables[0].Rows.Count; i++)
            {
                m_printDataSet.Tables[0].Rows[i]["PUDateString"] = (Convert.ToDateTime(m_printDataSet.Tables[0].Rows[i]["PUDate"])).ToString(Common.DTP_DATE_FORMAT);
                m_printDataSet.Tables[0].Rows[i]["MfgDateString"] = (Convert.ToDateTime(m_printDataSet.Tables[0].Rows[i]["MfgDate"])).ToString(Common.DTP_DATE_FORMAT);
                m_printDataSet.Tables[0].Rows[i]["ExpDateString"] = (Convert.ToDateTime(m_printDataSet.Tables[0].Rows[i]["ExpDate"])).ToString(Common.DTP_DATE_FORMAT);
                m_printDataSet.Tables[0].Rows[i]["Quantity"] = Math.Round(Convert.ToDouble(m_printDataSet.Tables[0].Rows[i]["Quantity"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                m_printDataSet.Tables[0].Rows[i]["MRP"] = Math.Round(Convert.ToDecimal(m_printDataSet.Tables[0].Rows[i]["MRP"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            }
            for (int i = 0; i < m_printDataSet.Tables[1].Rows.Count; i++)
                m_printDataSet.Tables[1].Rows[i]["Quantity"] = Math.Round(Convert.ToDecimal(m_printDataSet.Tables[1].Rows[i]["Quantity"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
        }
        /// <summary>
        /// Prints Screen report
        /// </summary>
        private void PrintReport(int index)
        {
            CreatePrintDataSet(index);
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.PackUnpack, m_printDataSet);
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }

        private void dgvPUHeader_CurrentCellChanged(object sender, EventArgs e)
        {

            try
            {
                PUHeader objPUHeader;
                if (dgvPUHeader.SelectedRows.Count == 0) return;
                string strPuno = Convert.ToString(dgvPUHeader.SelectedRows[0].Cells["PUNo"].Value);
                if (strPuno != "0")
                {

                    if (!strPuno.Equals(m_PUno, StringComparison.CurrentCultureIgnoreCase))
                    {
                        m_PUno = strPuno; //Assign this m_PUno to Global Variable.

                        objPUHeader = m_listPUHeader.Find(delegate(PUHeader puheader)
                        {
                            return puheader.PUNo.Equals(m_PUno, StringComparison.CurrentCultureIgnoreCase);
                        });

                        if (objPUHeader != null)
                        {
                            m_listPUDetail = objPUHeader.DetailItem;
                            BindGridView_Detaill();
                        }
                        else
                        {
                            m_listPUDetail = null;
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
   
        /// <summary>
        /// Bind Constitunet items(Detail) in selected pack/unpack Vocher Header
        /// </summary>
        private void BindGridView_Detaill()
        {
            try
            {
                dgvPUDetail.DataSource = new List<PUHeader>();
                if (m_listPUDetail.Count > 0)
                {
                    dgvPUDetail.DataSource = m_listPUDetail;
                    dgvPUDetail.Rows[0].Selected = false;
                    btnSavePUVoucher.Enabled = true;

                }
                else
                {
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                Reset_SearchMode();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Reset search tab controls
        /// </summary>
        private void Reset_SearchMode()
        {
            try
            {

                dgvPUDetail.DataSource = null;
                dgvPUHeader.DataSource = null;
                dtpFromDate.Value = DateTime.Today;                
                dtpToDate.Value = DateTime.Today;
                dtpFromDate.Checked = false;
                dtpToDate.Checked = false;
                cmbSearchPU.SelectedIndex = 0;
                txtSerachVouWithItemCode.Text = string.Empty;
                cmbSearchPU.Focus();
            }
            catch (Exception)
            {

                throw;
            }
        }
 
       
  

        #endregion

        #region Function,Event Use In Create Tab
        //On leave Fill all the objects related to composite item
        private void txtSearchItemCode_Leave(object sender, EventArgs e)
        {

            try
            {
                if (txtSearchItemCode.Text.Trim().Length > 0)
                {
                    SearchPUComposite();
                }

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        ///  Search Composite Item after item code return  by popup
        /// </summary>
        private void SearchPUComposite()
        {
            try
            {
                m_ListCompositeItem = null;
                PUCommon objPUCommon = new PUCommon();
                objPUCommon.SearchParam = PUCommon.SEARCH_COMPOSITE;
                objPUCommon.LocationId = m_currentLocationId;
                if (txtSearchItemCode.Text.Trim().Length > 0)
                {
                    objPUCommon.ItemCode = txtSearchItemCode.Text.Trim();
                    m_CompositeItemCode = txtSearchItemCode.Text.Trim();
                }
                else
                {
                    objPUCommon.ItemCode = string.Empty;
                }


                string errMsg = string.Empty;
                m_ListCompositeItem = objPUCommon.Search_CompositeItem(Common.ToXml(objPUCommon), PUCommon.PUVocher_SEARCH, ref errMsg);

                if (m_ListCompositeItem.Count > 0)
                {
                    if (m_ListCompositeItem.Count == 1)
                    {
                        //Bind Grid
                        SearchPUConstituent();
                    }
                    else
                    {
                        //More Then OneRecords

                        MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"),
                                                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }
                else
                {
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"),
                                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch { throw; }
        }
        /// <summary>
        /// Search Constituent items for selected Composite Items
        /// </summary>
        private void SearchPUConstituent()
        {
            try
            {

                m_CompositeItem = m_ListCompositeItem.Find(delegate(CompositeItem compositeItem)
                {
                    return compositeItem.ItemCode.Equals(m_CompositeItemCode, StringComparison.CurrentCultureIgnoreCase);
                });

                if (m_CompositeItem.ListBatchDetails != null)
                {
                    if (m_CompositeItem.ListBatchDetails.Count > 0)
                    {
                        cmbMfgBatch.DataSource = m_CompositeItem.ListBatchDetails;
                        cmbMfgBatch.DisplayMember = "MfgBatchNo";
                        cmbMfgBatch.ValueMember = "BatchNo";
                    }

                    cmbMfgBatch.SelectedIndex = 0;
                    cmbMfgBatch_SelectedIndexChanged(null, null);
                }

                m_CompositeItem.CompositeDetail = null;
                dgvConstituentPack.DataSource = null;

                PUCommon objPUCommon = new PUCommon();
                objPUCommon.SearchParam = PUCommon.SEARCH_CONSTITUENT;
                objPUCommon.LocationId = m_currentLocationId;
                objPUCommon.ItemId = m_CompositeItem.ItemId;

                string errMsg = string.Empty;

                objPUCommon.Search_CompositeBOM(m_CompositeItem, Common.ToXml(objPUCommon), PUCommon.PUVocher_SEARCH, ref errMsg);

                //Bind Grid
                BindGridView_Constituent();
            }
            catch { throw; }
        }
        /// <summary>
        /// Save pack/unpack records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSavePUVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                DisableValidation();
                Boolean IsSave = false;
                string errMessage = string.Empty;

                ValidateControls();

                errMessage = GetErrorMessages();
                if (errMessage.Length > 0)
                {
                    MessageBox.Show(errMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (!ValidatePackUnpackQuantity())
                    return ;

                errMessage = string.Empty;


                PUCommon objPUCommon = new PUCommon();


                objPUCommon.LocationId = m_currentLocationId;
                PUHeader objPUHeader = new PUHeader();
                objPUHeader.CompositeItemId = m_CompositeItem.ItemId;
                objPUHeader.BucketId = objPUCommon.FillBucket();
                objPUHeader.CreatedBy = this.m_userId;
                objPUHeader.IsPackVoucher = m_CompositeItem.IsPackVoucher;
                objPUHeader.Quantity = Convert.ToInt32(txtPUQuantity.Text);
                objPUHeader.LocationId = m_currentLocationId;
                objPUHeader.Remarks = txtRemarks.Text.Trim();
                objPUHeader.PUDate = dtpPUDate.Value.ToString(Common.DATE_TIME_FORMAT);
                objPUCommon.ObjCCompositeItem = m_CompositeItem;
                objPUCommon.ObjPUHeader = objPUHeader;
                objPUHeader.ModifiedBy = 1;
                m_CompositeItem.PackUnpackQty = Convert.ToInt32(txtPUQuantity.Text);
                m_CompositeItem.ExpDate = Convert.ToDateTime(dtpExpDate.Value).ToString(Common.DATE_TIME_FORMAT);
                m_CompositeItem.MfgDate = Convert.ToDateTime(dtpMfgDate.Value).ToString(Common.DATE_TIME_FORMAT);
                m_CompositeItem.MRP = Convert.ToDouble(txtMrp.Text);
                m_CompositeItem.PackUnpackQty = Convert.ToInt32(txtPUQuantity.Text);
               

                if (!m_CompositeItem.IsPackVoucher)
                {
                    if (!IsTotalBatchQuantityEqualToTotalQuantity())
                    {
                        return;
                    }
                    else
                    {
                        m_CompositeItem.MfgNo = Convert.ToString(cmbMfgBatch.SelectedText.Trim());
                        IsSave = objPUCommon.PUSave(Common.ToXml(objPUCommon), PUCommon.UNPack_SAVE, ref errMessage);
                    }

                    
                }
                else
                {
                    m_CompositeItem.MfgNo = Convert.ToString(txtMfgNo.Text);
                    IsSave = objPUCommon.PUSave(Common.ToXml(objPUCommon), PUCommon.Pack_SAVE, ref errMessage);
                }


                if (IsSave)
                {
                    if (m_CompositeItem.IsPackVoucher)
                    {
                        MessageBox.Show(Common.GetMessage("8007"), Common.GetMessage("10001"),
                                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage("8008"), Common.GetMessage("10001"),
                                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    m_ProcessClick = false;
                    cmbPackUnpack.Focus();
                    Reset_CreateTab();
                }
                else
                {
                    if (errMessage.Equals("VAL0057"))
                    {
                        MessageBox.Show(Common.GetMessage("VAL0057"), Common.GetMessage("10001"),
                                                       MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;

                    }
                    else if (errMessage.Equals("VAL0058"))
                    {
                        MessageBox.Show(Common.GetMessage("VAL0058"), Common.GetMessage("10001"),
                                                         MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    else if (errMessage.Equals("VAL0059"))
                    {
                        MessageBox.Show(Common.GetMessage("VAL0059"), Common.GetMessage("10001"),
                                                         MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    else
                    {
                        Common.LogException(new Exception(errMessage));
                        MessageBox.Show(Common.GetMessage("2003"), Common.GetMessage("10001"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            catch (Exception ex)
            {

                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbPackUnpack.Focus();
                Reset_CreateTab();
            }

        }
    

        /// <summary>
        /// Enable/disable control on the basis certain conditions
        /// </summary>
        /// <param name="bolCheck"></param>
        private void IsRecordFind(Boolean bolCheck)
        {
            if (bolCheck)
            {
                dgvConstituentPack.Visible = false;
                dgvConstituentUnpack.Visible=false;
                btnSavePUVoucher.Enabled = true;
                txtRemarks.Enabled = true;
                txtPUQuantity.Enabled = true;
                dtpPUDate.Enabled = true;
               


                switch (Convert.ToInt32(cmbPackUnpack.SelectedValue))
                {
                    case PUCommon.COMBO_PACK:
                        dtpExpDate.Enabled = true;
                        dtpMfgDate.Enabled = true;
                        txtMrp.Enabled = true;
                        txtMfgNo.Visible = true;
                        txtMfgNo.Enabled = true;                      
                        cmbMfgBatch.Visible = false;
                        cmbMfgBatch.SendToBack();
                        txtMfgNo.BringToFront();

                        txtMrp.Text = string.Empty;
                        txtMfgNo.Text = string.Empty;
                        DateTime dtAssignDate = Convert.ToDateTime(DateTime.Now.ToString(Common.DATE_TIME_FORMAT));
                        dtpMfgDate.MaxDate = dtAssignDate;
                        dtpMfgDate.Value = dtAssignDate;
                        dtpExpDate.Value = Convert.ToDateTime(m_CompositeItem.ExpDate);
                        lblDisplayAvailableQty.Visible = false;
                        lblAvailableQty.Visible = false;
                        m_CompositeItem.IsPackVoucher = true;

                        break;
                    case PUCommon.COMBO_UNPACK:
                        dtpExpDate.Enabled = false;
                        dtpMfgDate.Enabled = false;
                        txtMrp.Enabled = false;
                        txtMfgNo.Visible = false;
                        txtMfgNo.Enabled = false;

                        cmbMfgBatch.Visible = true;
                        cmbMfgBatch.Enabled = true;
                        cmbMfgBatch.BringToFront();
                        txtMfgNo.SendToBack();
                        lblDisplayAvailableQty.Visible=true;
                        lblAvailableQty.Visible = true;
                        cmbMfgBatch_SelectedIndexChanged(null, null);
                        m_CompositeItem.IsPackVoucher = false;
                        break;
                    default:
                        break;
                }
              
                dtpPUDate.Focus();

            }
            else
            {
                btnSavePUVoucher.Enabled = false;
                txtRemarks.Enabled = false;
                txtPUQuantity.Enabled = false;
                dtpPUDate.Enabled = false;
               
                dtpExpDate.Enabled = false;
                dtpMfgDate.Enabled = false;
                txtMrp.Enabled = false;
                dgvConstituentUnpack.Visible = false;
                dgvConstituentPack.Visible = false;
                txtMfgNo.Enabled = false;
                cmbMfgBatch.Enabled = false;
                
            }

        }
        //Reset Control on create tab
        private void btnResetCreate_Click(object sender, EventArgs e)
        {
            try
            {
                cmbPackUnpack.Focus();
                Reset_CreateTab();

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Process Pack/Unpack quantity
        /// </summary>
        private void ProcessPackUnpack()
        {
            try
            {
              DisableValidation();
              dgvConstituentUnpack.Visible = false;
              dgvConstituentPack.Visible = false;
              string strErrMessage = string.Empty;

              ValidateControls();
              strErrMessage = GetErrorMessages();

              if (!strErrMessage.Equals(string.Empty))
              {
                  MessageBox.Show(strErrMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                  return;

              }

              if (!IsValidatePackUnpackQty())
              {
                  return;
              }

              m_ProcessClick = true;
            foreach (CompositeBOM compositeBOM in m_CompositeItem.CompositeDetail)
            {
                List<BatchDetails> ListTempBatchDetails = new List<BatchDetails>();
                compositeBOM.ListSelectedBatchDetails = ListTempBatchDetails;
            }


            if (cmbPackUnpack.SelectedIndex == 0)
            {
                errCreatePU.SetError(cmbPackUnpack, Common.GetMessage("INF0026", lblSelPackUnPack.Text));

                MessageBox.Show(errCreatePU.GetError(cmbPackUnpack), Common.GetMessage("10001"),
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                switch (Convert.ToInt32(cmbPackUnpack.SelectedValue))
                {
                    case PUCommon.COMBO_PACK:
                        m_CompositeItem.IsPackVoucher = true;
                        dgvConstituentPack.Visible = true;
                        dgvConstituentUnpack.Visible = false;
                       
                        break;
                    case PUCommon.COMBO_UNPACK:
                        m_CompositeItem.IsPackVoucher = false;
                        dgvConstituentUnpack.Visible = true;
                        dgvConstituentPack.Visible = false;
                        break;
                    default:
                        break;
                }

               bool b= ValidatePackUnpackQuantity();
            }


            
            }
            catch (Exception)
            {

                throw;
            }

        }
        //Input validation check for Pack/Unpack quantity
        private Boolean IsValidatePackUnpackQty()
        {
            try
            {

                if (txtPUQuantity.Text.Trim().Length <= 0)
                {

                    Validators.SetErrorMessage(errCreatePU, txtPUQuantity, "INF0019", lblPUQty.Text);
                }
                else if (!Validators.IsInt64(txtPUQuantity.Text.Trim()))
                {

                    Validators.SetErrorMessage(errCreatePU, txtPUQuantity, "INF0010", lblPUQty.Text);
                }
                else
                {
                    Validators.SetErrorMessage(errCreatePU, txtPUQuantity);
                }

                string strError;
                strError = GetErrorMessages();
                if (strError == "")
                {
                    return true;
                }
                else
                {
                    if (strError.Length > 0)
                    {
                        MessageBox.Show(strError, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    }
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    
        /// <summary>
        ///   On F4 Key Press popup will open ,through which we can search
        ///   only composite item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ( e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    NameValueCollection nvc = new NameValueCollection();
                    nvc.Add("LocationId", m_currentLocationId.ToString());

                    CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.ItemLocationComposite, nvc);
                    CoreComponent.MasterData.BusinessObjects.ItemDetails _Item;
                    objfrmSearch.ShowDialog();
                    _Item = (CoreComponent.MasterData.BusinessObjects.ItemDetails)objfrmSearch.ReturnObject;

                    if (_Item != null)
                    {
                        txtSearchItemCode.Text = _Item.ItemCode;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// reset all the controls
        /// </summary>
        private void Reset_CreateTab()
        {
            try
            {
                DisableValidation();
                dgvConstituentPack.DataSource = null;
                dgvConstituentUnpack.DataSource = null;
               
                cmbMfgBatch.DataSource = null;
                cmbMfgBatch.Visible = false;
                txtMfgNo.Visible = true;
                txtMfgNo.BringToFront();

                txtSearchItemCode.Text = string.Empty;
                txtPUQuantity.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                cmbPackUnpack.SelectedIndex = 0;
                lblDisplayItemName.Text = string.Empty;
                txtMrp.Text = string.Empty;

                DateTime dtAssignDate = Convert.ToDateTime(DateTime.Now.ToString(Common.DATE_TIME_FORMAT));
                dtpMfgDate.MaxDate = dtAssignDate;
                dtpMfgDate.Value = dtAssignDate;
                dtpPUDate.MaxDate = dtAssignDate;
                dtpPUDate.Value = dtAssignDate;
                txtMfgNo.Text = string.Empty;
                lblDisplayAvailableQty.Visible = false;
                lblAvailableQty.Visible = false;
                m_CompositeItem = null;
                m_ProcessClick = false;
                m_AvailableComQuantity = 0;
                m_RecordFound = false;
                IsRecordFind(false);
                cmbPackUnpack.Focus();

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Populate grid on the basis of selected value Pack/Unpack combo box 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessPackUnpack();

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        ///     Disable Validations
        /// </summary>
        private void DisableValidation()
        {
            errCreatePU.SetError(cmbPackUnpack, string.Empty);
            errCreatePU.SetError(txtPUQuantity, string.Empty);
            errCreatePU.SetError(txtMfgNo, string.Empty);
            errCreatePU.SetError(txtMrp, string.Empty);
            errCreatePU.SetError(txtSearchItemCode, string.Empty);
            errCreatePU.SetError(dtpExpDate, string.Empty);

        }

        /// <summary>
        /// Validate Pack/Unpack quantity entered in 
        ///Pack/Unpack quantity text box against available qty in case of Pack.
        /// And also refresh the grid
        /// </summary>
        private bool ValidatePackUnpackQuantity()
        {
            try
            {
                DisableValidation();

                if (cmbPackUnpack.SelectedIndex != 0)
                {

                    if (txtPUQuantity.Text.Trim().Length > 0)
                    {
                        int intPUQty;
                        intPUQty = Convert.ToInt32(txtPUQuantity.Text.Trim());
                        if (m_CompositeItem.IsPackVoucher == true)
                        {
                             
                              dgvConstituentUnpack.Visible = false;

                              foreach (CompositeBOM compositeBOM in m_CompositeItem.CompositeDetail)
                              {


                                  int intTemp;
                                  intTemp = compositeBOM.Quantity * intPUQty;
                                  if (intTemp > compositeBOM.AvailableQty)
                                  {
                                      MessageBox.Show(Common.GetMessage("INF0069"), Common.GetMessage("10001"),
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                                      //foreach (CompositeBOM objcompositeBOM in m_CompositeItem.CompositeDetail)
                                      //{
                                      //    objcompositeBOM.TotalQty = 0;
                                      //}
                                      //dgvConstituentPack.Visible = false;
                                      
                                      dgvConstituentPack.Refresh();
                                      return false;

                                  }
                                  else
                                  {
                                      compositeBOM.TotalQty = intTemp;
                                  }
                              }
                              dgvConstituentPack.Visible = true;
                              dgvConstituentPack.Refresh();

                        }
                        else
                        {
                                dgvConstituentPack.Visible = false;
                                

                                if (intPUQty > m_AvailableComQuantity)
                                {
                                    MessageBox.Show(Common.GetMessage("INF0070"), Common.GetMessage("10001"),
                                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //foreach (CompositeBOM compositeBOM in m_CompositeItem.CompositeDetail)
                                    //{
                                    //    compositeBOM.TotalQty = 0;
                                    //}

                                    //dgvConstituentUnpack.Visible = false;
                                    dgvConstituentUnpack.Refresh();
                                    return false;
                                }
                                else
                                {
                                    foreach (CompositeBOM compositeBOM in m_CompositeItem.CompositeDetail)
                                    {
                                        compositeBOM.TotalQty = compositeBOM.Quantity * intPUQty;
                                    }

                                    dgvConstituentUnpack.Visible = true;
                                    dgvConstituentUnpack.Refresh();
                                }
                            
                        }
                        
                    }
                    else
                    {
                        errCreatePU.SetError(txtPUQuantity, Common.GetMessage("INF0019", lblPUQty.Text));

                        MessageBox.Show(errCreatePU.GetError(txtPUQuantity), Common.GetMessage("10001"),
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                dgvConstituentPack.Refresh();
                return true;
            }
            catch { throw; }
        }
       /// <summary>
      ///  return error message from mandatory control
      /// </summary>
      /// <returns>Error Message</returns>
        String GetErrorMessages()
        {
            try
            {
                StringBuilder sbError = new StringBuilder();
                if (Validators.GetErrorMessage(errCreatePU, cmbPackUnpack).Trim().Length > 0)
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(errCreatePU, cmbPackUnpack), ref sbError);
                if(Validators.GetErrorMessage(errCreatePU, txtSearchItemCode).Trim().Length >0)
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(errCreatePU, txtSearchItemCode), ref sbError);
                if (Validators.GetErrorMessage(errCreatePU, txtMfgNo).Trim().Length > 0)
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(errCreatePU, txtMfgNo), ref sbError);
                if(Validators.GetErrorMessage(errCreatePU, txtMrp).Trim().Length > 0)
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(errCreatePU, txtMrp), ref sbError);
                if(Validators.GetErrorMessage(errCreatePU, dtpExpDate).Trim().Length >0)
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(errCreatePU, dtpExpDate), ref sbError);
                if(Validators.GetErrorMessage(errCreatePU, txtPUQuantity).Trim().Length >0)
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(errCreatePU, txtPUQuantity), ref sbError);

                return Common.ReturnErrorMessage(sbError).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// For open popup to enter batch Pack quantity
        /// Un pack Only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvConstituentUnpack_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DisableValidation();
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (dgvConstituentUnpack.Columns[e.ColumnIndex].CellType == typeof(DataGridViewButtonCell))
                    {

                        if (IsValidatePackUnpackQty())
                            ValidatePackUnpackQuantity();
                        else
                            return;


                        Int32 ItemId = 0;
                        ItemId = Convert.ToInt32(dgvConstituentUnpack.Rows[e.RowIndex].Cells[PUCommon.GRID_ITEMID_COL].Value);


                        if (ItemId != 0)
                        {
                            CompositeBOM objCompositeBOM;
                            objCompositeBOM = (from compositeBOM in m_CompositeItem.CompositeDetail where compositeBOM.ItemId == ItemId select compositeBOM).FirstOrDefault();

                            if (objCompositeBOM != null)
                            {
                                if (objCompositeBOM.ListSelectedBatchDetails == null)
                                {
                                    List<BatchDetails> TempBatchDetails = new List<BatchDetails>();
                                    objCompositeBOM.ListSelectedBatchDetails = TempBatchDetails;
                                }
                                frmBatchDetails objfrmBatchDetails = new frmBatchDetails(objCompositeBOM);
                                objfrmBatchDetails.ShowDialog();
                            }
                        }
                        else
                        {

                        }

                    }


                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 
        /// <summary>
        /// Validate Unpack adjustment Total qty in batch
        /// must be equal to constituent items quantity
        /// </summary>
        /// <returns></returns>
        private Boolean IsTotalBatchQuantityEqualToTotalQuantity()
        {
            try
            {

                foreach (CompositeBOM objcompositeBOM in m_CompositeItem.CompositeDetail)
                {
                    int TotalPackQty = objcompositeBOM.TotalQty;
                    int TotalBatchQuantity = 0;
                    foreach (BatchDetails objBatchDetails in objcompositeBOM.ListSelectedBatchDetails)
                    {
                        TotalBatchQuantity = TotalBatchQuantity + objBatchDetails.RequestedQty;

                    }
                    if (TotalPackQty > TotalBatchQuantity)
                    {
                        MessageBox.Show(Common.GetMessage("INF0065"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;

                    }

                }

                return true;

            }
            catch (Exception)
            {

                throw;
            }



        }
        //Check Validation in create tab only
        /// <summary>
        /// Validate mandatory control before processing and saving the records
        /// </summary>
        void ValidateControls()
        {
            try
            {
                if (cmbPackUnpack.SelectedIndex == 0)
                {
                    Validators.SetErrorMessage(errCreatePU, cmbPackUnpack, "INF0026", lblSelPackUnPack.Text);

                }
                else
                {
                    Validators.SetErrorMessage(errCreatePU, cmbPackUnpack);
                }
                if (txtSearchItemCode.Text.Trim().Length <= 0)
                {

                    Validators.SetErrorMessage(errCreatePU, txtSearchItemCode, "INF0019", lblSearchItemCode.Text);
                }
                else
                {
                    Validators.SetErrorMessage(errCreatePU, txtSearchItemCode);

                }

                if (txtMfgNo.Text.Trim().Length <= 0)
                {
                    if (m_CompositeItem != null)
                    {
                        if (m_CompositeItem.IsPackVoucher)
                            Validators.SetErrorMessage(errCreatePU, txtMfgNo, "INF0019", lblMfg.Text);
                    }
                }
                else
                {
                    Validators.SetErrorMessage(errCreatePU, txtMfgNo);

                }
                if (txtMrp.Text.Trim().Length <= 0)
                {

                    Validators.SetErrorMessage(errCreatePU, txtMrp, "INF0019", lblMrp.Text);
                }
                else if (!Validators.IsValidAmount(txtMrp.Text.Trim()))
                {

                    Validators.SetErrorMessage(errCreatePU, txtMrp, "INF0010", lblMrp.Text);
                }
                else
                {
                    Validators.SetErrorMessage(errCreatePU, txtMrp);
                }



                if (dtpExpDate.Value.Date <= dtpMfgDate.Value.Date)
                {
                    Validators.SetErrorMessage(errCreatePU, dtpExpDate, "INF0071");
                }
                else
                {
                    Validators.SetErrorMessage(errCreatePU, dtpExpDate);
                }
                
                if (txtPUQuantity.Text.Trim().Length <= 0)
                {

                    Validators.SetErrorMessage(errCreatePU, txtPUQuantity, "INF0019", lblPUQty.Text);
                }
                else if (!Validators.IsInt64(txtPUQuantity.Text.Trim()))
                {

                    Validators.SetErrorMessage(errCreatePU, txtPUQuantity, "INF0010", lblPUQty.Text);
                }
                else
                {
                    Validators.SetErrorMessage(errCreatePU, txtPUQuantity);
                }

              

            
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Enable/disable control on the basis of selected value
        /// set m_CompositeItem object property IsPack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPackUnpack_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_RecordFound)
                {
                    if (m_SelVal_PackUnpack == Convert.ToInt32(cmbPackUnpack.SelectedValue))
                    {


                    }
                    else if (m_SelVal_PackUnpack != Convert.ToInt32(cmbPackUnpack.SelectedValue) && (m_ProcessClick == true))
                    {
                        m_SelVal_PackUnpack = Convert.ToInt32(cmbPackUnpack.SelectedValue);
                        m_ProcessClick = false;

                        MessageBox.Show(Common.GetMessage("INF0068"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    IsRecordFind(true);


                }
                else
                {

                    IsRecordFind(false);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Set MRP,Exp date, Mfg date,Availabel Qty when selected index changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMfgBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbMfgBatch.DataSource != null)
                {

                    if (m_CompositeItem.ListBatchDetails != null)
                    {
                        if (m_CompositeItem.ListBatchDetails.Count > 0)
                        {
                            string BatchNo = Convert.ToString(cmbMfgBatch.SelectedValue);
                            m_CompositeItem.BatchNo = Convert.ToString(cmbMfgBatch.SelectedValue);


                            BatchDetails batchdetail = null;
                            batchdetail = (from u in m_CompositeItem.ListBatchDetails where (u.BatchNo.Equals(BatchNo, StringComparison.CurrentCultureIgnoreCase)) select u).FirstOrDefault();
                            if (batchdetail != null)
                            {
                                m_AvailableComQuantity = Convert.ToInt32(batchdetail.AvailableQty);
                                lblDisplayAvailableQty.Text = Convert.ToString(batchdetail.AvailableQty);
                                dtpExpDate.Value = Convert.ToDateTime(batchdetail.ExpDate);
                                dtpMfgDate.Value = Convert.ToDateTime(batchdetail.MfgDate);
                                txtMrp.Text = batchdetail.MRP;


                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }


        }
        #endregion
        /// <summary>
        /// To reset the controls when user switche's between Tabs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlHierarchy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (tabControlHierarchy.SelectedTab.Text == PUCommon.SELECTED_TAB_CREATE)
                {
                    cmbPackUnpack.Focus();
                    Reset_CreateTab();
                }
                else
                {
                    Reset_SearchMode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }            
        }

        private void dgvPUHeader_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex > -1)
                {                    
                    ((Button)sender).Enabled = false;
                    PrintReport(e.RowIndex);
                    ((Button)sender).Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
        }
        private void dgvPUHeader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex > -1)
                {
                    PrintReport(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
        }        
    }
}
