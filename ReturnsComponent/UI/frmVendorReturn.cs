using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using CoreComponent.Core.BusinessObjects;
using CoreComponent.Hierarchies.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;
using System.Collections.Specialized;
using ReturnsComponent.BusinessObjects;

namespace ReturnsComponent.UI
{
    public partial class frmVendorReturn : Form
    {
        DataView m_vendorView;
        bool m_f4KeyPressed = false;
        private const string CON_VENDOR_DISPLAYNAME = "VendorCode";
        private const string CON_VENDOR_ID = "VendorId";
        private RetVendorHeader m_objRetVendorHeader;
        private List<RetVendorHeader> m_ListRetVendorHeader;
        int m_UserID = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
        //int m_Status = (int)Common.GRNStatus.New;
        private int m_CurrentLocationId = Common.CurrentLocationId;
        private int m_SelectedLocationID;        
        private string m_LocationCode = Common.LocationCode;

        private string m_UserName = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;
        private Common.LocationConfigId m_LocationType = (Common.LocationConfigId)Common.CurrentLocationTypeId;
        private const string CON_LOCATION_DISPLAYNAME = "DisplayName";
        private const string CON_LOCATION_ID = "LocationId";
        private const string CON_DETAILITEM_BUCKETID = "BucketId";
        private const string CON_DETAILITEM_BATCHNO = "BatchNo";
        private const string CON_DETAILITEM_ITEMID = "ItemId";

        private List<RetVendorDetails> m_ListRetVendorDetails = new List<RetVendorDetails>();
        private RetVendorDetails m_objRetVendorDetails;
        private string m_TabMode = Common.TAB_CREATE_MODE;
        private int m_selectedItemRowIndex = -1;
        //private Boolean m_UpdateFlag = false;
        public string m_DebitNoteText = string.Empty;
        private string m_ModifiedDate = string.Empty;
        private int m_currentLocationType = Common.CurrentLocationTypeId;

        public const string MODULE_CODE = "RET01";
        private Boolean IsCreateAvailable = false;
        private Boolean IsConfirmAvailable = false;
        private Boolean IsSearchAvailable = false;
        private Boolean IsCancelAvailable = false;
        private Boolean IsApproveAvailable = false;
        private Boolean IsShipAvailable = false;
        private Boolean IsPrintAvailable = true;

        private StringBuilder errorMessages = null;
        private DataSet m_printDataSet = null;

        #region C'tor
        public frmVendorReturn()
        {
            try
            {
                errorMessages = new StringBuilder();
                InitializeComponent();
                InitializeDateControl();
                InitializeRights();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        private void InitializeRights()
        {
            try
            {
                
                IsCreateAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, MODULE_CODE, Common.FUNCTIONCODE_CREATE);
                IsConfirmAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, MODULE_CODE, Common.FUNCTIONCODE_CONFIRM);
                IsCancelAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, MODULE_CODE, Common.FUNCTIONCODE_CANCEL);
                IsSearchAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, MODULE_CODE, Common.FUNCTIONCODE_SEARCH);
                IsApproveAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, MODULE_CODE, Common.FUNCTIONCODE_APPROVE);
                IsShipAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, MODULE_CODE, Common.FUNCTIONCODE_SHIP);
                IsPrintAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, MODULE_CODE, Common.FUNCTIONCODE_PRINT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Add Select Item into Combo Box
        /// </summary>
        /// <param name="cmb"></param>
        private void AddSelectItemInCombo(ComboBox cmb)
        {
            DataTable dtSelect = new DataTable("SleectHeader");
            DataColumn SelectText = new DataColumn("SelectText", Type.GetType("System.String"));
            DataColumn SelectTextValue = new DataColumn("SelectTextValue", Type.GetType("System.String"));

            dtSelect.Columns.Add(SelectText);
            dtSelect.Columns.Add(SelectTextValue);

            DataRow dRow = dtSelect.NewRow();
            dRow["SelectText"] = "Select";
            dRow["SelectTextValue"] = "-1";
            dtSelect.Rows.Add(dRow);



            cmb.DataSource = dtSelect;
            cmb.ValueMember = "SelectTextValue";
            cmb.DisplayMember = "SelectText";
        }

        void InitializeDateControl()
        {
            try
            {
                BindLocation();
                DataTable dtVendors = Common.ParameterLookup(Common.ParameterType.ItemVendor, new ParameterFilter("", Common.INT_DBNULL, 0, 0));
                if (dtVendors != null)
                {
                    
                    cmbSearchVendorCode.DataSource = dtVendors;
                    cmbSearchVendorCode.DisplayMember = CON_VENDOR_DISPLAYNAME;
                    cmbSearchVendorCode.ValueMember = CON_VENDOR_ID;
                }
                
                tabControlTransaction.TabPages[0].Text = Common.TAB_SEARCH_MODE;
                tabControlTransaction.TabPages[1].Text = Common.TAB_CREATE_MODE;

                Common.BindParamComboBox(cmbSearchStatus, Common.RTV_STATUS, 0, 0, 0);
                cmbGRNInvoiceType.SelectedIndexChanged -= new System.EventHandler(cmbGRNInvoiceType_SelectedIndexChanged);
                DataTable dtGRNInvoice = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.GRN_INVOICE_TYPE.ToString(), 0, 0, 0));
                if (dtGRNInvoice != null)
                {
                    cmbGRNInvoiceType.DataSource = dtGRNInvoice;
                    cmbGRNInvoiceType.DisplayMember = Common.KEYVALUE1;
                    cmbGRNInvoiceType.ValueMember = Common.KEYCODE1;
                }
                cmbGRNInvoiceType.SelectedIndexChanged += new System.EventHandler(cmbGRNInvoiceType_SelectedIndexChanged);

                DataTable dtDebitNoteText = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.DEBIT_NOTE_TEXT.ToString(), 0, 0, 0));
                if (dtDebitNoteText != null)
                {
                    if (dtDebitNoteText.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtDebitNoteText.Rows[0].ItemArray[0]) == -1)
                        {
                            dtDebitNoteText.Rows.RemoveAt(0);
                            dtDebitNoteText.AcceptChanges();
                        }
                    }
                }
                foreach (DataRow drTemp in dtDebitNoteText.Rows)
                {
                    m_DebitNoteText += Convert.ToString(drTemp[Common.KEYVALUE1]);


                }
                Reset_CreateTabDate();
                Reset_SearchTabDate();
                lblPageTitle.Text = "Vendor Return";
                cmbPONumber.SelectedIndexChanged -= new EventHandler(cmbPONumber_SelectedIndexChanged);
                AddSelectItemInCombo(cmbPONumber);
                cmbPONumber.SelectedIndexChanged += new EventHandler(cmbPONumber_SelectedIndexChanged);
            }
            catch (Exception)
            {
                throw;
            }
        }

        void FillVendors()
        {
            DataTable dtVendors = Common.ParameterLookup(Common.ParameterType.VendorsByLocation, new ParameterFilter("", Convert.ToInt32(cmbLocationCode.SelectedValue), 0, 0));
            if (dtVendors != null)
            {
                cmbVendorCode.DataSource = dtVendors;
                cmbVendorCode.DisplayMember = CON_VENDOR_DISPLAYNAME;
                cmbVendorCode.ValueMember = CON_VENDOR_ID;                
            }
        }

        void GridInitialize()
        {
            dgvSearchReturnToVendor.AutoGenerateColumns = false;
            dgvSearchReturnToVendor.DataSource = null;
            DataGridView dgvSearchReturnToVendorNew = Common.GetDataGridViewColumns(dgvSearchReturnToVendor, Environment.CurrentDirectory + "\\App_Data\\Return.xml");


            dgvReturnToVendorItems.AutoGenerateColumns = false;
            dgvReturnToVendorItems.DataSource = null;
            DataGridView dgvReturnToVendorItemsNew = Common.GetDataGridViewColumns(dgvReturnToVendorItems, Environment.CurrentDirectory + "\\App_Data\\Return.xml");
        }

        private StringBuilder GenerateError()
        {
            StringBuilder sbError = new StringBuilder();
            if (errItem.GetError(txtItemCode).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtItemCode));
                sbError.AppendLine();
            }
            if (errItem.GetError(cmbPONumber).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(cmbPONumber));
                sbError.AppendLine();
            }
            if (errItem.GetError(cmbGRNInvoiceType).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(cmbGRNInvoiceType));
                sbError.AppendLine();
            }
            if (errItem.GetError(cmbGRNInvoiceNo).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(cmbGRNInvoiceNo));
                sbError.AppendLine();
            }
            if (errItem.GetError(txtReturnQty).Trim().Length > 0)
            {
                sbError.Append(errItem.GetError(txtReturnQty));
                sbError.AppendLine();
            }
            return sbError;
        }

        /// <summary>
        /// Copy Item into seperate List exclusive of current selected item
        /// </summary>
        /// <param name="excludeIndex"></param>
        /// <param name="lst"></param>
        /// <returns></returns>
        List<RetVendorDetails> CopyItemDetail(int excludeIndex, List<RetVendorDetails> lst)
        {
            List<RetVendorDetails> returnList = new List<RetVendorDetails>();
            for (int i = 0; i < lst.Count; i++)
            {

                if (i != excludeIndex)
                {
                    RetVendorDetails tdetail = new RetVendorDetails();
                    tdetail = lst[i];


                    RetVendorCommon obj = new RetVendorCommon();
                    string strErrMsg = string.Empty;
                    obj.LocationId = m_SelectedLocationID;
                    obj.ItemId = tdetail.ItemId;
                    obj.BatchNo = tdetail.BatchNo;
                    obj.VendorId = Convert.ToInt32(cmbVendorCode.SelectedValue.ToString());
                    obj.SelectMode = RetVendorCommon.PO_DETAILS;


                    DataTable vendorDT = new DataTable();
                    RetVendorDetails objDetails = new RetVendorDetails();
                    //IsPODetailFound = obj.GetPODetails(m_objRetVendorDetails, Common.ToXml(obj), RetVendorCommon.SP_GET_VENDOR_DETAILS, ref strErrMsg);
                    vendorDT = obj.GetPODetails(objDetails, Common.ToXml(obj), RetVendorCommon.SP_GET_VENDOR_DETAILS, ref strErrMsg);

                    DataView vendorView = vendorDT.DefaultView; //= new DataView(m_vendorView.ToTable(true, "PONumber", "InvoiceNo", "GRNNo", "IsFormCApplicable"));
                    if (vendorDT.Rows.Count > 1)
                    {
                        if (tdetail.GRNInvoiceType == 1)
                        {
                            vendorView.RowFilter = "PONumber ='" + tdetail.PONumber.ToString() + "' And InvoiceNo ='" + tdetail.GRNInvoiceNumber + "'";
                            tdetail.InvoiceNumber = vendorView[0]["InvoiceNo"].ToString();
                            tdetail.GRNNumber = vendorView[0]["GRNNo"].ToString();
                        }
                        else if (tdetail.GRNInvoiceType == 2)
                        {
                            vendorView.RowFilter = "PONumber ='" + tdetail.PONumber.ToString() + "' And GRNNo ='" + tdetail.GRNInvoiceNumber + "'";
                            tdetail.GRNNumber = vendorView[0]["GRNNo"].ToString();
                            tdetail.InvoiceNumber = vendorView[0]["InvoiceNo"].ToString();
                        }
                    }
                    returnList.Add(tdetail);
                }
            }
            return returnList;
        }


        private void AddItem()
        {
            try
            {
                errorMessages = new StringBuilder();
                errItem.Clear();


                ValidateItemCode(false);
                //IsValidateReturnQty();
                ValidateQuantity(true);
                ValidatePONumber(true);
                ValidateGRNInvoiceType(true);
                ValidateGRNInvoiceNo(true);


                #region Check Errors
                StringBuilder sbError = new StringBuilder();
                sbError = GenerateError();
                sbError = Common.ReturnErrorMessage(sbError);
                #endregion

                if (!sbError.ToString().Trim().Equals(string.Empty))
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                m_objRetVendorDetails.GRNInvoiceNumber = cmbGRNInvoiceNo.SelectedValue.ToString();
                m_objRetVendorDetails.GRNInvoiceType = Convert.ToInt32(cmbGRNInvoiceType.SelectedValue);
                m_objRetVendorDetails.GRNReceivedQty = Convert.ToInt32(txtReturnQty.Text.Trim());
                m_objRetVendorDetails.PONumber = cmbPONumber.SelectedValue.ToString();
                m_objRetVendorDetails.ReturnQty = Convert.ToInt32(txtReturnQty.Text.Trim());
                m_objRetVendorDetails.ReturnReason = Convert.ToString(txtReturnReason.Text.Trim());
                m_objRetVendorDetails.CurrentLocationId = Convert.ToInt32(cmbLocationCode.SelectedValue);

                //Check if Return-Qty is valid
                //by checking it with 
                //(SUM OF TOTAL-AVAIL-QTY FORM INV-LOC-BUCKET-BATCH) - (SUM OF GRN-RECEIVED-QTY)
                //for the present Item and Batch,
                //and check if this difference is greater than or equal to the Return-Qty
                //to it to be valid qty
                RetVendorCommon objTemp = new RetVendorCommon();
                string errMsg = string.Empty;
                if (!objTemp.CheckForValidReturnQty(Common.ToXml(m_objRetVendorDetails), ref errMsg))
                {
                    MessageBox.Show(errMsg, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (m_ListRetVendorDetails != null && m_ListRetVendorDetails.Count > 0)
                {
                    List<RetVendorDetails> tiDetail = CopyItemDetail(m_selectedItemRowIndex, m_ListRetVendorDetails);
                    //checked based on ItemCode and Bucket Id
                    int isDuplicateRecordFound = (from p in tiDetail where p.BatchNo.Trim().ToLower() == m_objRetVendorDetails.BatchNo.Trim().ToLower() && p.ItemCode.Trim().ToLower() == txtItemCode.Text.Trim().ToLower() && p.BucketId == m_objRetVendorDetails.BucketId && p.PONumber == m_objRetVendorDetails.PONumber && ((p.GRNInvoiceType == 2 && p.GRNNumber == m_objRetVendorDetails.GRNInvoiceNumber) || (p.GRNInvoiceType == 1 && p.InvoiceNumber == m_objRetVendorDetails.GRNInvoiceNumber)) select p).Count();

                    if (isDuplicateRecordFound > 0)
                    {
                        MessageBox.Show(Common.GetMessage("VAL0063", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                //if (IsDuplicate(m_objRetVendorDetails.ItemId, m_objRetVendorDetails.BatchNo, m_objRetVendorDetails.BucketId, cmbPONumber.SelectedValue.ToString(),Convert.ToInt32(cmbGRNInvoiceType.SelectedValue),cmbGRNInvoiceNo.SelectedValue.ToString(), ref RowIndex))
                //{
                if ((m_selectedItemRowIndex != Common.INT_DBNULL) && (m_selectedItemRowIndex <= dgvReturnToVendorItems.Rows.Count))
                {
                    m_ListRetVendorDetails.Insert(m_selectedItemRowIndex, m_objRetVendorDetails);
                    m_ListRetVendorDetails.RemoveAt(m_selectedItemRowIndex + 1);
                }
                else
                    m_ListRetVendorDetails.Add(m_objRetVendorDetails);

                m_selectedItemRowIndex = -1;
                BindGridViewDetailItem(true);

                CalulateDebitAmount_TotalQuantity();
                ResetItemControl(true);
                CheckItemList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BindGridViewDetailItem(bool SetIndex)
        {
            try
            {
                dgvReturnToVendorItems.DataSource = null;
                if (m_ListRetVendorDetails != null)
                {
                    if (m_ListRetVendorDetails.Count > 0)
                    {
                        dgvReturnToVendorItems.DataSource = m_ListRetVendorDetails;
                    }
                }

                dgvReturnToVendorItems.Refresh();
                if (SetIndex)
                {
                    if (m_selectedItemRowIndex >= 0 && dgvReturnToVendorItems.DataSource != null)
                    {
                        dgvReturnToVendorItems.Rows[m_selectedItemRowIndex].Selected = true;
                    }
                }
                else
                {
                    m_selectedItemRowIndex = -1;
                    dgvReturnToVendorItems.ClearSelection();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        void ValidateControls()
        {
            try
            {
                if (cmbVendorCode.SelectedIndex == 0)
                {
                    Validators.SetErrorMessage(errItem, cmbVendorCode, "INF0026", lblVendorCode.Text);

                }
                else
                {
                    Validators.SetErrorMessage(errItem, cmbVendorCode);
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindGridViewHeaderItem()
        {
            try
            {
                dgvSearchReturnToVendor.DataSource = null;
                if (m_ListRetVendorHeader != null)
                {
                    if (m_ListRetVendorHeader.Count > 0)
                    {
                        dgvSearchReturnToVendor.DataSource = m_ListRetVendorHeader;
                    }
                }
                dgvSearchReturnToVendor.Refresh();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Creates DataSet for Printing RTV Screen report
        /// </summary>
        private void CreatePrintDataSet()
        {
            m_printDataSet = new DataSet();
            DataTable dtRTVHeader = new DataTable("RTVHeader");
            DataColumn ReturnNumber = new DataColumn("ReturnNumber", Type.GetType("System.String"));
            DataColumn Location = new DataColumn("Location", Type.GetType("System.String"));
            DataColumn VendorCode = new DataColumn("VendorCode", Type.GetType("System.String"));
            DataColumn ReturnDate = new DataColumn("ReturnDate", Type.GetType("System.String"));
            DataColumn CurrentStatus = new DataColumn("CurrentStatus", Type.GetType("System.String"));
            DataColumn TotalReturnQty = new DataColumn("TotalReturnQty", Type.GetType("System.String"));
            DataColumn TotalReturnCost = new DataColumn("TotalReturnCost", Type.GetType("System.String"));
            DataColumn Remarks = new DataColumn("Remarks", Type.GetType("System.String"));
            DataColumn ShipmentDate = new DataColumn("ShipmentDate", Type.GetType("System.String"));
            DataColumn ShipmentDetails = new DataColumn("ShipmentDetails", Type.GetType("System.String"));
            DataColumn DebitNoteNumber = new DataColumn("DebitNoteNumber", Type.GetType("System.String"));
            DataColumn DebitNoteAmount = new DataColumn("DebitNoteAmount", Type.GetType("System.String"));
            DataColumn VendorName = new DataColumn("VendorName", Type.GetType("System.String"));
            DataColumn VendorAddress = new DataColumn("VendorAddress", Type.GetType("System.String"));
            DataColumn AmountInWords = new DataColumn("AmountInWords", Type.GetType("System.String"));
            dtRTVHeader.Columns.Add(ReturnNumber);
            dtRTVHeader.Columns.Add(Location);
            dtRTVHeader.Columns.Add(VendorCode);
            dtRTVHeader.Columns.Add(ReturnDate);
            dtRTVHeader.Columns.Add(CurrentStatus);
            dtRTVHeader.Columns.Add(TotalReturnQty);
            dtRTVHeader.Columns.Add(TotalReturnCost);
            dtRTVHeader.Columns.Add(Remarks);
            dtRTVHeader.Columns.Add(ShipmentDate);
            dtRTVHeader.Columns.Add(ShipmentDetails);
            dtRTVHeader.Columns.Add(DebitNoteNumber);
            dtRTVHeader.Columns.Add(DebitNoteAmount);
            dtRTVHeader.Columns.Add(VendorName);
            dtRTVHeader.Columns.Add(VendorAddress);
            dtRTVHeader.Columns.Add(AmountInWords);
            DataRow dRow = dtRTVHeader.NewRow();
            dRow["ReturnNumber"] = m_objRetVendorHeader.ReturnNo;
            dRow["Location"] = cmbLocationCode.Text;
            dRow["VendorCode"] = cmbVendorCode.Text;
            dRow["ReturnDate"] = Convert.ToDateTime(m_objRetVendorHeader.RetVendorDate).ToString(Common.DTP_DATE_FORMAT);
            dRow["CurrentStatus"] = txtCurrentStatus.Text;
            dRow["TotalReturnQty"] = m_objRetVendorHeader.Quantity;
            dRow["TotalReturnCost"] = Math.Round(Convert.ToDecimal(m_objRetVendorHeader.TotalAmount), Common.DisplayAmountRounding,MidpointRounding.AwayFromZero);
            dRow["Remarks"] = txtRemarks.Text;
            dRow["ShipmentDate"] = m_objRetVendorHeader.ShipmentDate != string.Empty ? Convert.ToDateTime(m_objRetVendorHeader.ShipmentDate).ToString(Common.DTP_DATE_FORMAT) : ""; 
            dRow["ShipmentDetails"] = txtShippingDetails.Text;
            dRow["DebitNoteNumber"] = txtDebitNoteNo.Text;
            dRow["DebitNoteAmount"] = Math.Round(Convert.ToDecimal(m_objRetVendorHeader.DebitNoteAmount),Common.DisplayAmountRounding,MidpointRounding.AwayFromZero);
            DataTable dtVendor = Common.ParameterLookup(Common.ParameterType.Vendor,new ParameterFilter("",m_objRetVendorHeader.VendorId,-1,-1));
            dRow["VendorName"] = dtVendor.Rows[0]["VendorName"].ToString();
            dRow["VendorAddress"] = dtVendor.Rows[0]["Address1"].ToString() + " " + dtVendor.Rows[0]["Address2"].ToString() + Environment.NewLine + dtVendor.Rows[0]["Address3"].ToString()+ " " + dtVendor.Rows[0]["Address4"].ToString()+ Environment.NewLine + dtVendor.Rows[0]["CityName"].ToString() + " "+  dtVendor.Rows[0]["StateName"].ToString() + Environment.NewLine + dtVendor.Rows[0]["CountryName"].ToString();
            dRow["AmountInWords"] = Common.AmountToWords.AmtInWord(Convert.ToDecimal(dRow["DebitNoteAmount"]));
            dtRTVHeader.Rows.Add(dRow);
            DataTable dtRTVDetail = new DataTable("RTVDetail");
            RetVendorCommon objRetVendorCommon = new RetVendorCommon();
            string errorMessage = string.Empty;
            objRetVendorCommon.ReturnNo = m_objRetVendorHeader.ReturnNo;
            objRetVendorCommon.SelectMode = RetVendorCommon.RTVDETAILS;
            dtRTVDetail = objRetVendorCommon.GetVendorDetailDataTable(Common.ToXml(objRetVendorCommon), RetVendorCommon.SP_GET_VENDOR_DETAILS, ref errorMessage);
            dtRTVDetail.Columns.Add(new DataColumn("PODateText", Type.GetType("System.String")));
            for (int i = 0; i < dtRTVDetail.Rows.Count; i++)
            {
                dtRTVDetail.Rows[i]["AvailableQty"] = Math.Round(Convert.ToDecimal(dtRTVDetail.Rows[i]["AvailableQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtRTVDetail.Rows[i]["PoQty"] = Math.Round(Convert.ToDecimal(dtRTVDetail.Rows[i]["PoQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtRTVDetail.Rows[i]["POAmount"] = Math.Round(Convert.ToDecimal(dtRTVDetail.Rows[i]["POAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                dtRTVDetail.Rows[i]["ReturnQty"] = Math.Round(Convert.ToDecimal(dtRTVDetail.Rows[i]["ReturnQty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                dtRTVDetail.Rows[i]["PODateText"] = Convert.ToDateTime(dtRTVDetail.Rows[i]["PODate"]).ToString(Common.DTP_DATE_FORMAT);
            }
            m_printDataSet.Tables.Add(dtRTVHeader);
            m_printDataSet.Tables.Add(dtRTVDetail.Copy());
            m_printDataSet.Tables[1].TableName = "RTVDetail";
        }

        /// <summary>
        /// Prints RTV Screen report
        /// </summary>
        private void PrintDebitNoteReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.RTVDebitNote, m_printDataSet);
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }

        private void PrintReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.RTV, m_printDataSet);
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }

        void ValidateF4KeyPressForItem()
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("LocationId", m_SelectedLocationID.ToString());
            nvc.Add("VendorId", Convert.ToString(cmbVendorCode.SelectedValue));
            nvc.Add("ItemCode", Convert.ToString(txtItemCode.Text.ToLower().Trim()));

            CoreComponent.Controls.frmSearch objfrmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.ItemReturnToVendor, nvc);
            objfrmSearch.ShowDialog();
            ItemBatchDetails _Item = (ItemBatchDetails)objfrmSearch.ReturnObject;

            if (_Item != null)
            {
                m_f4KeyPressed = true;
                ShowItemInfo(_Item);
            }
        }

        void ShowItemInfo(ItemBatchDetails _Item)
        {
            txtItemCode.Text = _Item.ItemCode;
            txtItemDescription.Text = _Item.ItemName;
            txtAvailableQty.Text = Convert.ToString(Math.Round(_Item.Quantity, 0));
            txtBucketName.Text = Convert.ToString(_Item.BucketName);
            m_objRetVendorDetails = new RetVendorDetails();
            m_objRetVendorDetails.BatchNo = _Item.BatchNo;
            m_objRetVendorDetails.ManufactureBatchNo = _Item.ManufactureBatchNo;


            m_objRetVendorDetails.BucketId = Convert.ToInt32(_Item.BucketId);
            m_objRetVendorDetails.ItemId = _Item.ItemId;
            m_objRetVendorDetails.ItemDescription = _Item.ItemName;
            m_objRetVendorDetails.Bucket = Convert.ToString(_Item.BucketName);
            m_objRetVendorDetails.ItemCode = Convert.ToString(_Item.ItemCode);
            m_objRetVendorDetails.AvailableQty = Convert.ToInt32(_Item.Quantity);
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt)
                {
                    if (cmbVendorCode.SelectedIndex == 0)
                    {
                        Validators.SetErrorMessage(errItem, cmbVendorCode, "INF0026", lblVendorCode.Text);
                        MessageBox.Show(errItem.GetError(cmbVendorCode), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbVendorCode.Focus();
                        return;
                    }
                    else
                    {
                        Validators.SetErrorMessage(errItem, cmbVendorCode);
                    }
                }

                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    ValidateF4KeyPressForItem();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetHeaderRecords(int status)
        {
            switch (status)
            {
                case (int)Common.RTVStatus.New:
                    if (m_TabMode.Equals(RetVendorCommon.CREATE_MODE))
                    {
                        m_objRetVendorHeader = new RetVendorHeader();
                        m_objRetVendorHeader.StatusId = (int)Common.RTVStatus.Created;
                        m_objRetVendorHeader.LocationId = m_SelectedLocationID;
                        m_objRetVendorHeader.CurrentLocationId = Common.CurrentLocationId;
                        m_objRetVendorHeader.VendorId = Convert.ToInt32(cmbVendorCode.SelectedValue);
                    }

                    m_objRetVendorHeader.RetVendorDate = dtpReturnDate.Value.ToString(Common.DATE_TIME_FORMAT);
                    if (txtTotalReturnQty.Text.Trim().Equals(string.Empty))
                        m_objRetVendorHeader.Quantity = 0;
                    else
                        m_objRetVendorHeader.Quantity = Convert.ToInt32(txtTotalReturnQty.Text.Trim());

                    if (txtDebitNoteAmount.Text.Trim().Equals(string.Empty))
                        m_objRetVendorHeader.DebitNoteAmount = 0;
                    else
                        m_objRetVendorHeader.DebitNoteAmount = Convert.ToDouble(txtDebitNoteAmount.Text.Trim());

                    if (txtTotalTaxAmount.Text.Trim().Equals(string.Empty))
                        m_objRetVendorHeader.TotalAmount = 0;
                    else
                        m_objRetVendorHeader.TotalAmount = Convert.ToDouble(txtTotalTaxAmount.Text.Trim());
                                        
                    break;
                case (int)Common.RTVStatus.Created:     

                    
                    m_objRetVendorHeader.RetVendorDate = dtpReturnDate.Value.ToString(Common.DATE_TIME_FORMAT);
                    
                    if (txtTotalReturnQty.Text.Trim().Equals(string.Empty))
                        m_objRetVendorHeader.Quantity = 0;
                    else
                        m_objRetVendorHeader.Quantity = Convert.ToInt32(txtTotalReturnQty.Text.Trim());

                    if (txtDebitNoteAmount.Text.Trim().Equals(string.Empty))
                        m_objRetVendorHeader.DebitNoteAmount = 0;
                    else
                        m_objRetVendorHeader.DebitNoteAmount = Convert.ToDouble(txtDebitNoteAmount.Text.Trim());

                    if (txtTotalTaxAmount.Text.Trim().Equals(string.Empty))
                        m_objRetVendorHeader.TotalAmount = 0;
                    else
                        m_objRetVendorHeader.TotalAmount = Convert.ToDouble(txtTotalTaxAmount.Text.Trim());                    
                    
                    break;

                case (int)Common.RTVStatus.Confirmed:
                    
                    if (txtDebitNoteNo.Text.Trim().Length > 0)
                        m_objRetVendorHeader.DebitNoteNumber = txtDebitNoteNo.Text.Trim();
                    
                    m_objRetVendorHeader.RetVendorDate = dtpReturnDate.Value.ToString(Common.DATE_TIME_FORMAT);

                    if (txtTotalReturnQty.Text.Trim().Equals(string.Empty))
                        m_objRetVendorHeader.Quantity = 0;
                    else
                        m_objRetVendorHeader.Quantity = Convert.ToInt32(txtTotalReturnQty.Text.Trim());

                    if (txtDebitNoteAmount.Text.Trim().Equals(string.Empty))
                        m_objRetVendorHeader.DebitNoteAmount = 0;
                    else
                        m_objRetVendorHeader.DebitNoteAmount = Convert.ToDouble(txtDebitNoteAmount.Text.Trim());

                    if (txtTotalTaxAmount.Text.Trim().Equals(string.Empty))
                        m_objRetVendorHeader.TotalAmount = 0;
                    else
                        m_objRetVendorHeader.TotalAmount = Convert.ToDouble(txtTotalTaxAmount.Text.Trim());
                    
                    m_objRetVendorHeader.DebitNoteText = string.Empty;
                    m_objRetVendorHeader.DebitNoteText = ReplaceParam(m_DebitNoteText, "28/12/2009", "27");
                    break;
                case (int)Common.RTVStatus.Approved:                   
                    if (txtDebitNoteNo.Text.Trim().Length > 0)
                        m_objRetVendorHeader.DebitNoteNumber = txtDebitNoteNo.Text.Trim();
                        break;
                default:
                    break;
            }
            m_objRetVendorHeader.Remarks = txtRemarks.Text.Trim();
            m_objRetVendorHeader.ShippingDetails = txtShippingDetails.Text.Trim();
            
        }

        private string ReplaceParam(params string[] param)
        {

            string[] mparam;
            string DebitText = m_DebitNoteText; ;

            try
            {
                if (param.Length == 0)
                    return string.Empty;



                mparam = param.Length == 1 ? param[0].Split(',') : param;

                for (int i = 1; i < mparam.Length; i++)
                    mparam[i - 1] = mparam[i];

                mparam[mparam.Length - 1] = string.Empty;

                string ParmaText;

                for (int i = 0; DebitText.ToString().IndexOf("{" + i.ToString() + "}") >= 0; i++)
                {
                    ParmaText = i < mparam.Length ? mparam[i] : string.Empty;
                    DebitText = DebitText.Replace("{" + i.ToString() + "}", ParmaText);
                }

                return DebitText;
            }
            catch { throw; }
        }

        private void frmVendorReturn_Load(object sender, EventArgs e)
        {
            try
            {
                GridInitialize();
                BindGridViewHeaderItem();
                BindGridViewDetailItem(false);

                this.lblPageTitle.Text = "Vendor Return";
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemCode_Validating(object sender, CancelEventArgs e)
        {
            if (txtItemCode.Text.Trim().Length > 0)
                ValidateItemCode(true);
            //try
            //{
            //    errItem.Clear();
            //    ValidateItemCode();

            //    if (!string.IsNullOrEmpty(errItem.GetError(txtItemCode)))
            //    {
            //        MessageBox.Show(errItem.GetError(txtItemCode), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }

            //    //bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Length);
            //    //if (isTextBoxEmpty == true && yesNo == false)
            //    //    errContact.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            //    //else //if (isTextBoxEmpty == false)
            //    //    errContact.SetError(txt, string.Empty);
            //}
            //catch (Exception ex)
            //{
            //    Common.LogException(ex);
            //    MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        void ValidateItemCode(bool yesNo)
        {
            try
            {
                bool IsTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtItemCode.Text.Trim().Length);
                if (IsTextBoxEmpty == true)
                {
                    errItem.SetError(txtItemCode, Common.GetMessage("VAL0001", "Item-Code"));
                    //errorMessages.AppendLine(Common.GetMessage("VAL0001", "Item-Code"));
                    txtItemCode.Focus();
                    return;
                }
                else if (IsTextBoxEmpty == false)
                {
                    if (yesNo == true && m_selectedItemRowIndex < 0 && m_f4KeyPressed == false)
                    {
                        m_objRetVendorDetails = new RetVendorDetails();
                        ItemBatchDetails ibd = new ItemBatchDetails();
                        List<ItemBatchDetails> lst = new List<ItemBatchDetails>();

                        ibd.LocationId = m_SelectedLocationID.ToString();
                        ibd.VendorId = cmbVendorCode.SelectedValue.ToString();
                        ibd.ItemCode = txtItemCode.Text.ToLower().Trim();

                        lst = ibd.SearchItemReturnToVendor();
                        if (lst != null && lst.Count == 1)
                        {
                            ItemBatchDetails _Item = lst[0];

                            ShowItemInfo(_Item);
                        }
                        else if (lst != null && lst.Count > 1 && m_f4KeyPressed == false)
                        {
                            ValidateF4KeyPressForItem();
                        }
                    }

                    if ((yesNo == true) && (m_objRetVendorDetails != null))
                    {
                        //bool IsPODetailFound = false;
                        RetVendorCommon obj = new RetVendorCommon();
                        string strErrMsg = string.Empty;
                        obj.LocationId = m_SelectedLocationID;
                        obj.ItemId = m_objRetVendorDetails.ItemId;
                        obj.ManufactureBatchNo = m_objRetVendorDetails.ManufactureBatchNo;
                        obj.VendorId = Convert.ToInt32(cmbVendorCode.SelectedValue.ToString());
                        obj.SelectMode = RetVendorCommon.PO_DETAILS;

                        DataTable vendorDT = new DataTable();
                        //IsPODetailFound = obj.GetPODetails(m_objRetVendorDetails, Common.ToXml(obj), RetVendorCommon.SP_GET_VENDOR_DETAILS, ref strErrMsg);
                        vendorDT = obj.GetPODetails(m_objRetVendorDetails, Common.ToXml(obj), RetVendorCommon.SP_GET_VENDOR_DETAILS, ref strErrMsg);
                        if (!strErrMsg.Equals(string.Empty))
                        {
                            //throw new Exception(strErrMsg);
                            //errorMessages.AppendLine(strErrMsg);
                            errItem.SetError(txtItemCode, strErrMsg);
                        }
                        else
                        {
                            if (vendorDT.Rows.Count > 0)
                            {
                                m_vendorView = new DataView(vendorDT.DefaultView.ToTable(true, "PONumber", "PODate", "POQty", "MRP", "InvoiceNo", "GRNNo", "IsFormCApplicable", "UnitPrice", "ReceivedQty"));


                                DataView dv = new DataView(m_vendorView.ToTable(true, "PONumber", "PODate", "POQty", "MRP"));

                                cmbPONumber.SelectedIndexChanged -= new EventHandler(cmbPONumber_SelectedIndexChanged);
                                cmbPONumber.DataSource = dv;
                                cmbPONumber.ValueMember = "PONumber";
                                cmbPONumber.DisplayMember = "PONumber";
                                cmbPONumber.SelectedIndexChanged += new EventHandler(cmbPONumber_SelectedIndexChanged);
                                //txtPODate.Text = Convert.ToDateTime(m_objRetVendorDetails.PODate).ToString(Common.DTP_DATE_FORMAT);
                                //txtPOItemQty.Text = Convert.ToString(m_objRetVendorDetails.POQty);
                                //txtPONumber.Text = m_objRetVendorDetails.PONumber;
                                //txtPOItemAmount.Text = Convert.ToString(Math.Round(m_objRetVendorDetails.POAmount, 2));
                                //btnAddDetails.Enabled = true;
                                errItem.SetError(txtItemCode, string.Empty);
                            }
                            else
                            {
                                //MessageBox.Show("To check PO, detail is not present.");
                                ResetItemControl(false);
                                strErrMsg = Common.GetMessage("VAL0094");
                                //strErrMsg = "PO Information for the selected item is not present. Please select any other record.";
                                errItem.SetError(txtItemCode, strErrMsg);
                                //if (!errorMessages.ToString().Contains(Common.GetMessage("VAL0001")))
                                //{
                                //    //errorMessages.AppendLine(Common.GetMessage("VAL0070"));
                                //    //txtItemCode.Focus();
                                //}
                                //errItem.SetError(txtItemCode, "Detail Information for the selected PO is not present. Please select any other PO record.");
                            }
                        }
                    }
                    else if (yesNo == true)
                    {

                        errItem.SetError(txtItemCode, Common.GetMessage("VAL0006", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                        //errorMessages.AppendLine(Common.GetMessage("VAL0070"));
                        //txtItemCode.Focus();

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //private bool IsValidateReturnQty()
        //{
        //    try
        //    {
        //        if (m_objRetVendorDetails == null)
        //        {
        //            if (txtItemCode.Text.Length > 0)
        //            {
        //                if (!errorMessages.ToString().Contains(Common.GetMessage("VAL0070")))
        //                {
        //                    errItem.SetError(txtItemCode, Common.GetMessage("VAL0070"));
        //                    errorMessages.AppendLine(Common.GetMessage("VAL0070"));
        //                    txtItemCode.Focus();
        //                }
        //            }
        //            return false;
        //        }

        //        if (CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txtReturnQty.Text.Trim().Length) == true)
        //        {
        //            //errorMessages.AppendLine(Common.GetMessage("INF0019"));
        //            Validators.SetErrorMessage(errItem, txtReturnQty, "INF0019", lblReturnQty.Text.Trim());
        //            //MessageBox.Show(errItem.GetError(txtReturnQty), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            txtReturnQty.Focus();
        //            return false;
        //        }
        //        else if (CoreComponent.Core.BusinessObjects.Validators.IsInt32(txtReturnQty.Text.Trim()) == false)
        //        {
        //            //errorMessages.AppendLine(Common.GetMessage("INF0010"));
        //            Validators.SetErrorMessage(errItem, txtReturnQty, "INF0010", lblReturnQty.Text.Trim());
        //            //MessageBox.Show(errItem.GetError(txtReturnQty), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            txtReturnQty.Focus();
        //            return false;
        //        }
        //        else if (Convert.ToInt32(txtReturnQty.Text.Trim()) > Convert.ToInt32(txtAvailableQty.Text.Trim()))
        //        {
        //            //errorMessages.AppendLine(Common.GetMessage("INF0073"));
        //            Validators.SetErrorMessage(errItem, txtReturnQty, "INF0073");
        //            //MessageBox.Show(errItem.GetError(txtReturnQty), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            txtReturnQty.Focus();
        //            return false;
        //        }
        //        else
        //        {
        //            errItem.SetError(txtReturnQty, string.Empty);
        //            //btnAddDetails.Enabled = true;
        //            return true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        private void dgvReturnToVendorItems_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SelectGridRow(e);

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectGridRow(EventArgs e)
        {
            try
            {


                if (dgvReturnToVendorItems.SelectedCells.Count > 0)
                {
                    int rowIndex = dgvReturnToVendorItems.SelectedCells[0].RowIndex;
                    int columnIndex = dgvReturnToVendorItems.SelectedCells[0].ColumnIndex;

                    if (rowIndex >= 0 && columnIndex >= 0)
                    {
                        int selectedRow = dgvReturnToVendorItems.SelectedCells[0].RowIndex;

                        int ItemId = Common.INT_DBNULL;
                        string BatchNumber = string.Empty;
                        int BucketId = Common.INT_DBNULL;
                        string grnInvoiceNo = string.Empty;
                        int grnInvoiceType = Common.INT_DBNULL;
                        string poNumber = string.Empty;

                        if ((m_ListRetVendorDetails != null) && (m_ListRetVendorDetails.Count <= 0))
                        { return; }

                        ItemId = Convert.ToInt32(dgvReturnToVendorItems.Rows[rowIndex].Cells["ItemId"].Value);
                        BatchNumber = dgvReturnToVendorItems.Rows[rowIndex].Cells["BatchNo"].Value.ToString().Trim();
                        BucketId = Convert.ToInt32(dgvReturnToVendorItems.Rows[rowIndex].Cells["BucketId"].Value.ToString());
                        grnInvoiceType = Convert.ToInt32(dgvReturnToVendorItems.Rows[rowIndex].Cells["grnInvoiceType"].Value.ToString());
                        poNumber = dgvReturnToVendorItems.Rows[rowIndex].Cells["PONumber"].Value.ToString();
                        grnInvoiceNo = dgvReturnToVendorItems.Rows[rowIndex].Cells["GRNInvoiceNumber"].Value.ToString();


                        RetVendorDetails objRetVendorDetails;
                        m_selectedItemRowIndex = rowIndex;
                        objRetVendorDetails = GetRetVendorDetail(ItemId, BatchNumber, BucketId, grnInvoiceType, grnInvoiceNo, poNumber);
                        if (objRetVendorDetails != null)
                        {

                            txtBucketName.Text = objRetVendorDetails.Bucket;
                            txtReturnQty.Text = objRetVendorDetails.DisplayReturnQty.ToString(); //Convert.ToString(objRetVendorDetails.ReturnQty);
                            txtReturnReason.Text = Convert.ToString(objRetVendorDetails.ReturnReason);
                            //txtPONumber.Text = Convert.ToString(objRetVendorDetails.PONumber);
                            txtPOItemQty.Text = objRetVendorDetails.DisplayPOQty.ToString(); //Convert.ToString(objRetVendorDetails.POQty);
                            txtPODate.Text = objRetVendorDetails.DisplayPODate;
                            txtItemDescription.Text = objRetVendorDetails.ItemDescription;
                            txtPOItemAmount.Text = objRetVendorDetails.DisplayPOAmount.ToString(); //Convert.ToString(Math.Round(objRetVendorDetails.POAmount, 2));
                            txtItemCode.Text = objRetVendorDetails.ItemCode;
                            //m_objRetVendorDetails.BatchNo = objRetVendorDetails.BatchNo;
                            m_objRetVendorDetails = objRetVendorDetails;
                            ValidateItemCode(true);
                            txtAvailableQty.Text = objRetVendorDetails.DisplayAvailableQty.ToString(); //Convert.ToString(objRetVendorDetails.AvailableQty);
                            cmbPONumber.SelectedValue = objRetVendorDetails.PONumber.ToString();
                            cmbGRNInvoiceType.SelectedValue = objRetVendorDetails.GRNInvoiceType.ToString();
                            ValidateGRNInvoiceType(false);
                            cmbGRNInvoiceNo.SelectedValue = objRetVendorDetails.GRNInvoiceNumber.ToString();
                            txtItemTax.Text = objRetVendorDetails.DisplayLineTaxAmount.ToString();

                            //if (m_objRetVendorHeader != null)
                            //{
                            //    if ((m_objRetVendorHeader.StatusId == (int)Common.RTVStatus.Created))
                            //    {
                            //        btnAddDetails.Enabled = true;
                            //    }
                            //}
                            //else
                            //{
                            //    btnAddDetails.Enabled = true;
                            //}
                        }


                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool IsDuplicate(int intItemid, string strBatchno, int intBucketId, string poNumber, int grnInvoiceType, string grnInvoiceNo, ref int RowIndex)
        {
            try
            {
                if (m_ListRetVendorDetails != null && m_ListRetVendorDetails.Count > 0)
                {
                    var itemSelect = (from p in m_ListRetVendorDetails where p.BucketId == intBucketId && p.BatchNo.Equals(strBatchno, StringComparison.CurrentCultureIgnoreCase) && p.ItemId == intItemid select p);
                    if (itemSelect.ToList<RetVendorDetails>().Count > 0)
                    {

                        RowIndex = m_ListRetVendorDetails.FindIndex(delegate(RetVendorDetails obj) { return obj.BucketId == intBucketId && obj.BatchNo.Equals(strBatchno, StringComparison.CurrentCultureIgnoreCase) && obj.ItemId == intItemid; });
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void RemoveItem(int intItemid, string strBatchno, int intBucketId)
        {
            try
            {
                if (m_ListRetVendorDetails != null)
                {
                    if (m_ListRetVendorDetails.Count > 0)
                    {
                        RetVendorDetails objRetVendorDetails;

                        objRetVendorDetails = m_ListRetVendorDetails.Find(delegate(RetVendorDetails obj)
                        { return ((obj.BatchNo.Equals(strBatchno, StringComparison.CurrentCultureIgnoreCase)) && (obj.BucketId == intBucketId) && (obj.ItemId == intItemid)); });

                        if (objRetVendorDetails != null)
                        {
                            m_ListRetVendorDetails.Remove(objRetVendorDetails);
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private RetVendorDetails GetRetVendorDetail(int intItemid, string strBatchno, int intBucketId, int grnInvoiceType, string grnInvoiceNo, string poNumber)
        {
            try
            {
                if (m_ListRetVendorDetails != null && m_ListRetVendorDetails.Count > 0)
                {
                    var itemSelect = (from p in m_ListRetVendorDetails where p.BucketId == intBucketId && p.BatchNo.Equals(strBatchno, StringComparison.CurrentCultureIgnoreCase) && p.ItemId == intItemid && p.PONumber == poNumber && p.GRNInvoiceNumber == grnInvoiceNo && p.GRNInvoiceType == grnInvoiceType select p);
                    if (itemSelect.ToList<RetVendorDetails>().Count == 1)
                    {
                        return itemSelect.ToList<RetVendorDetails>()[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CheckItemList()
        {
            try
            {
                if (m_ListRetVendorDetails.Count > 0)
                    btnSave.Enabled = true;
                else
                    btnSave.Enabled = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ResetItemControl(bool flushItemCode)
        {
            txtPODate.Text = string.Empty;
            if (flushItemCode)
            {
                txtItemCode.TextChanged -= new EventHandler(txtItemCode_TextChanged);
                txtItemCode.Text = string.Empty;
                txtItemCode.TextChanged += new EventHandler(txtItemCode_TextChanged);
            }
            txtPOItemAmount.Text = string.Empty;
            m_f4KeyPressed = false;
            txtReturnReason.Text = string.Empty;
            txtReturnQty.Text = string.Empty;
            txtItemDescription.Text = string.Empty;
            txtAvailableQty.Text = string.Empty;
            txtBucketName.Text = string.Empty;
            txtPOItemQty.Text = string.Empty;
            m_objRetVendorDetails = null;
            //btnAddDetails.Enabled = false;
            txtItemCode.Focus();
            m_vendorView = new DataView();
            txtGRNInvoiceQty.Text = string.Empty;

            cmbGRNInvoiceType.SelectedIndexChanged -= new EventHandler(cmbGRNInvoiceType_SelectedIndexChanged);
            cmbGRNInvoiceType.SelectedValue = Common.INT_DBNULL;
            cmbGRNInvoiceType.SelectedIndexChanged += new EventHandler(cmbGRNInvoiceType_SelectedIndexChanged);

            txtItemTax.Text = string.Empty;

            cmbGRNInvoiceNo.SelectedIndexChanged -= new EventHandler(cmbGRNInvoiceNo_SelectedIndexChanged);
            AddSelectItemInCombo(cmbGRNInvoiceNo);
            cmbGRNInvoiceNo.SelectedIndexChanged += new EventHandler(cmbGRNInvoiceNo_SelectedIndexChanged);

            cmbPONumber.SelectedIndexChanged -= new EventHandler(cmbPONumber_SelectedIndexChanged);
            AddSelectItemInCombo(cmbPONumber);
            cmbPONumber.SelectedIndexChanged += new EventHandler(cmbPONumber_SelectedIndexChanged);

            dgvReturnToVendorItems.ClearSelection();
            m_selectedItemRowIndex = Common.INT_DBNULL;

            if (dgvReturnToVendorItems.Rows.Count > 0)
            {
                cmbVendorCode.Enabled = false;
                cmbLocationCode.Enabled = false;
            }
        }

        private void dgvSearchReturnToVendor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EditItemDetails(e);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void SelectSearchGrid(string returnNumber)
        {
            m_objRetVendorHeader = m_ListRetVendorHeader.Find(delegate(RetVendorHeader obj)
            { return obj.ReturnNo.Equals(returnNumber, StringComparison.CurrentCultureIgnoreCase); });

            RetVendorHeader objVendorHeader = m_ListRetVendorHeader.Find(delegate(RetVendorHeader obj)
            { return obj.ReturnNo.Equals(returnNumber, StringComparison.CurrentCultureIgnoreCase); });

            if (m_objRetVendorHeader != null)
            {
                tabControlTransaction.SelectedIndex = 1;
                tabControlTransaction.TabPages[1].Text = Common.TAB_UPDATE_MODE;

                m_objRetVendorHeader = objVendorHeader;
                dgvReturnToVendorItems.SelectionChanged -= new System.EventHandler(dgvReturnToVendorItems_SelectionChanged);

                Search_Detail(m_objRetVendorHeader.ReturnNo);
                dgvReturnToVendorItems.SelectionChanged += new System.EventHandler(dgvReturnToVendorItems_SelectionChanged);

                DisableEnableCon_OnStatus(m_objRetVendorHeader.StatusId);
                m_TabMode = Common.TAB_UPDATE_MODE;
                cmbLocationCode.SelectedValue = m_objRetVendorHeader.LocationId;
                txtReturnNumber.Text = m_objRetVendorHeader.ReturnNo;
                txtRemarks.Text = m_objRetVendorHeader.Remarks;
                txtShippingDetails.Text = m_objRetVendorHeader.ShippingDetails;                
                dtpReturnDate.Value = Convert.ToDateTime(m_objRetVendorHeader.RetVendorDate);
                txtDebitNoteNo.Text = m_objRetVendorHeader.DebitNoteNumber;
                txtDebitNoteAmount.Text = m_objRetVendorHeader.DisplayDebitNoteAmount.ToString("#.00"); //Convert.ToDouble(Math.Round(m_objRetVendorHeader.DebitNoteAmount, 2)).ToString();
                txtTotalTaxAmount.Text = m_objRetVendorHeader.DisplayTotalAmount == 0 ? m_objRetVendorHeader.DisplayTotalAmount.ToString() : m_objRetVendorHeader.DisplayTotalAmount.ToString("#.00"); //Convert.ToDouble(Math.Round(m_objRetVendorHeader.DebitNoteAmount, 2)).ToString();
                txtTotalItemCost.Text = (m_objRetVendorHeader.DisplayDebitNoteAmount - m_objRetVendorHeader.DisplayTotalAmount).ToString("#.00");
                cmbVendorCode.SelectedValue = m_objRetVendorHeader.VendorId;
                txtTotalReturnQty.Text = m_objRetVendorHeader.DisplayQuantity.ToString(); //Convert.ToString(m_objRetVendorHeader.Quantity);
                txtCurrentStatus.Text = m_objRetVendorHeader.StatusName;
                m_ModifiedDate = m_objRetVendorHeader.ModifiedDate;
                dgvReturnToVendorItems.ClearSelection();
                ResetItemControl(true);
                //m_UpdateFlag = true;
            }
        }

        private void EditItemDetails(DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.RowIndex >= 0) && (dgvSearchReturnToVendor.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell)))
                {
                    string strReturnNumber = string.Empty;
                    strReturnNumber = Convert.ToString(dgvSearchReturnToVendor.Rows[e.RowIndex].Cells["ReturnNo"].Value);

                    SelectSearchGrid(strReturnNumber);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                dgvReturnToVendorItems.SelectionChanged -= new System.EventHandler(dgvReturnToVendorItems_SelectionChanged);
                AddItem();
                dgvReturnToVendorItems.SelectionChanged += new System.EventHandler(dgvReturnToVendorItems_SelectionChanged);
            }
            catch (Exception ex)
            {

                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        
        private void BindLocation()
        {
            DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("", -5, 0, 0));
            if (dtLocations != null)
            {
                cmbSearchLocationCode.DataSource = dtLocations;
                cmbSearchLocationCode.DisplayMember = CON_LOCATION_DISPLAYNAME;
                cmbSearchLocationCode.ValueMember = CON_LOCATION_ID;

                DataTable dtTemp = dtLocations.Copy();

                cmbLocationCode.DataSource = dtTemp;
                cmbLocationCode.DisplayMember = CON_LOCATION_DISPLAYNAME;
                cmbLocationCode.ValueMember = CON_LOCATION_ID;

                if (m_LocationType != Common.LocationConfigId.HO)
                {
                    cmbSearchLocationCode.SelectedValue = m_CurrentLocationId;
                    cmbSearchLocationCode.Enabled = false;
                    cmbLocationCode.SelectedValue = m_CurrentLocationId;
                    cmbLocationCode.Enabled = false;
                }
                cmbLocationCode_SelectedIndexChanged(null, null);
            }
        }

        private void cmbLocationCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbLocationCode.Items.Count > 0)
                {
                    m_SelectedLocationID = Convert.ToInt32(cmbLocationCode.SelectedValue);
                    FillVendors();
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnClearDetails_Click(object sender, EventArgs e)
        {
            try
            {
                ResetItemControl(true);
                errItem.Clear();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validate Starting Amount
        /// </summary>
        void ValidateQuantity(TextBox txt, Label lbl)
        {
            bool isValidQuantity = CoreComponent.Core.BusinessObjects.Validators.IsValidQuantity(txt.Text);

            if (isValidQuantity == false)
                errItem.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else if (Convert.ToDecimal(txt.Text) <= 0)
                errItem.SetError(txt, Common.GetMessage("VAL0009", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
            else
            {
                if (Convert.ToDecimal(txt.Text) > Convert.ToDecimal(txtGRNInvoiceQty.Text.Trim().Length == 0 ? "0" : txtGRNInvoiceQty.Text.Trim()))
                    errItem.SetError(txt, Common.GetMessage("VAL0060", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2), lblGRNInvoiceQty.Text.Trim().Substring(0, lblGRNInvoiceQty.Text.Trim().Length - 2)));
                else if (Convert.ToDecimal(txt.Text) > Convert.ToDecimal(txtAvailableQty.Text.Trim().Length == 0 ? "0" : txtAvailableQty.Text.Trim()))
                    errItem.SetError(txt, Common.GetMessage("VAL0060", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2), lblAvailableQty.Text.Trim().Substring(0, lblAvailableQty.Text.Trim().Length - 2)));
                else
                {
                    errItem.SetError(txt, string.Empty);


                    if (m_vendorView != null)
                    {

                        DataView vendorView = new DataView(m_vendorView.ToTable(true, "PONumber", "InvoiceNo", "GRNNo", "IsFormCApplicable"));

                        //if (Convert.ToInt32(cmbGRNInvoiceType.SelectedValue) == (int)Common.InvoiceGRNType.Invoice)
                        vendorView.RowFilter = "PONumber ='" + cmbPONumber.SelectedValue.ToString() + "'";
                        //else
                        //    vendorView.RowFilter = "PONumber ='" + cmbPONumber.SelectedValue.ToString() + "' AND  GRNNo ='" + cmbGRNInvoiceNo.SelectedValue.ToString() + "'";

                        DataTable dtVendors = Common.ParameterLookup(Common.ParameterType.Vendor, new ParameterFilter("", Convert.ToInt32(cmbVendorCode.SelectedValue), 0, 0));
                        DataTable dtLocation = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("", 0, Convert.ToInt32(cmbLocationCode.SelectedValue), 0));

                        if (dtVendors != null && dtLocation != null && dtLocation.Rows.Count > 0 && dtVendors.Rows.Count > 0)
                        {
                            PurchaseComponent.BusinessObjects.PurchaseOrderDetail m_PurchaseDetail = new PurchaseComponent.BusinessObjects.PurchaseOrderDetail(Convert.ToInt32(dtLocation.Rows[0]["LocationId"]), Convert.ToBoolean(vendorView[0]["IsFormCApplicable"]), Convert.ToInt32(dtVendors.Rows[0]["StateId"]), Convert.ToInt32(dtLocation.Rows[0]["StateId"]));
                            m_PurchaseDetail.ItemID = m_objRetVendorDetails.ItemId;
                            m_PurchaseDetail.ItemCode = txtItemCode.Text.Trim();
                            m_PurchaseDetail.PONumber = cmbPONumber.SelectedValue.ToString();
                            m_PurchaseDetail.UnitPrice = m_objRetVendorDetails.UnitPrice;
                            m_PurchaseDetail.UnitQty = Convert.ToDecimal(txtReturnQty.Text);
                            m_PurchaseDetail.GetAndCalculateTaxes(true,GetVendorCode(),GetLocationCode()); //Pauru
                            txtItemTax.Text = m_PurchaseDetail.DisplayLineTaxAmount.ToString();
                            m_objRetVendorDetails.LineTaxAmount = m_PurchaseDetail.DisplayLineTaxAmount;
                        }
                    }
                }
            }
        }

        private string GetVendorCode()
        {
            DataRowView drView = cmbVendorCode.SelectedItem as DataRowView;
            string sVendorCode = "";
            if (drView != null)
                sVendorCode = drView.Row.ItemArray[2].ToString();
            return sVendorCode;
        }

        private string GetLocationCode()
        {
            DataRowView drView = cmbLocationCode.SelectedItem as DataRowView;
            string sLocationCode = "";
            if (drView != null)
                sLocationCode = drView.Row.ItemArray[2].ToString();
            return sLocationCode;
        }

        void ValidateQuantity(bool yesNo)
        {
            if (yesNo)
            {
                ValidateQuantity(txtReturnQty, lblReturnQty);
            }
        }

        private void txtReturnQty_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ValidateQuantity(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //try
            //{
            //    errItem.Clear();
            //    IsValidateReturnQty();
            //    if (!string.IsNullOrEmpty(errItem.GetError(txtReturnQty)))
            //    {
            //        MessageBox.Show(errItem.GetError(txtReturnQty), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Common.LogException(ex);
            //    MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void dgvReturnToVendorItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////if (m_objRetVendorHeader.StatusId == (int)(Common.RTVStatus.Closed))
                ////{
                ////    return;
                ////}

                //dgvReturnToVendorItems.SelectionChanged -= new System.EventHandler(dgvReturnToVendorItems_SelectionChanged);
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (dgvReturnToVendorItems.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                    {
                        string BatchNo = Convert.ToString(dgvReturnToVendorItems.Rows[e.RowIndex].Cells[frmVendorReturn.CON_DETAILITEM_BATCHNO].Value);
                        int BucketId = Convert.ToInt32(dgvReturnToVendorItems.Rows[e.RowIndex].Cells[frmVendorReturn.CON_DETAILITEM_BUCKETID].Value);
                        int ItemId = Convert.ToInt32(dgvReturnToVendorItems.Rows[e.RowIndex].Cells[frmVendorReturn.CON_DETAILITEM_ITEMID].Value);

                        if ((BatchNo == string.Empty) && (BucketId == 0) && (ItemId == 0))
                        {
                            m_objRetVendorDetails = null;
                            ResetItemControl(true);
                            grpAddDetails.Refresh();
                            MessageBox.Show(Common.GetMessage("VAL0010", "Detail Item"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        if ((m_objRetVendorHeader != null) && ((m_objRetVendorHeader.StatusId == (int)Common.RTVStatus.Confirmed) ||
                            (m_objRetVendorHeader.StatusId == (int)Common.RTVStatus.Shipped)))
                        {
                            MessageBox.Show(Common.GetMessage("VAL0069"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        DialogResult result = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            dgvReturnToVendorItems.SelectionChanged -= new System.EventHandler(dgvReturnToVendorItems_SelectionChanged);

                            RemoveItem(ItemId, BatchNo, BucketId);

                            BindGridViewDetailItem(false);
                            CalulateDebitAmount_TotalQuantity();
                            m_objRetVendorDetails = null;
                            ResetItemControl(true);
                            CheckItemList();

                            dgvReturnToVendorItems.SelectionChanged += new System.EventHandler(dgvReturnToVendorItems_SelectionChanged);
                        }
                    }
                }

                //dgvReturnToVendorItems.SelectionChanged += new System.EventHandler(dgvReturnToVendorItems_SelectionChanged);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalulateDebitAmount_TotalQuantity()
        {
            try
            {
                if (m_ListRetVendorDetails != null)
                {
                    if (m_ListRetVendorDetails.Count > 0)
                    {
                        var TotalQty = (from t in m_ListRetVendorDetails select t.ReturnQty).Sum();
                        var TotalDebitAmount = (from t in m_ListRetVendorDetails select t.POAmount * t.ReturnQty).Sum();
                        var totalTaxAmount = (from t in m_ListRetVendorDetails select t.LineTaxAmount).Sum();
                        
                        txtDebitNoteAmount.Text = Math.Round(Convert.ToDouble(TotalDebitAmount) + Convert.ToDouble(totalTaxAmount), CoreComponent.Core.BusinessObjects.Common.DisplayAmountRounding, MidpointRounding.AwayFromZero).ToString(".00");
                        txtTotalItemCost.Text = Math.Round(Convert.ToDouble(TotalDebitAmount), CoreComponent.Core.BusinessObjects.Common.DisplayAmountRounding, MidpointRounding.AwayFromZero).ToString(".00");
                        txtTotalReturnQty.Text = Convert.ToString(TotalQty);
                        //txtDebitNoteAmount.Text = Math.Round(Convert.ToDouble(TotalDebitAmount), 2).ToString("00");
                        txtTotalTaxAmount.Text = Math.Round(Convert.ToDouble(totalTaxAmount), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero).ToString();
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void DisableValidation()
        {
            errItem.SetError(cmbVendorCode, string.Empty);
            errItem.SetError(txtItemCode, string.Empty);
            errItem.SetError(txtReturnQty, string.Empty);
        }

        String GetErrorMessagesForCreateTab()
        {
            try
            {
                StringBuilder sbError = new StringBuilder();
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(errItem, cmbVendorCode), ref sbError);
                sbError = Common.ReturnErrorMessage(sbError);
                return sbError.ToString();//.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine).Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveRecord(bool ExecuteBlock)
        {
            try
            {
                string errMessage = string.Empty;
                List<RetVendorDetails> listRetVendorDetails = null;
                #region Validate Control

                DisableValidation();

                ValidateControls();
                errMessage = GetErrorMessagesForCreateTab();
                if (errMessage.Length > 0)
                {
                    MessageBox.Show(errMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                #endregion


                if (ExecuteBlock)
                {
                    #region Block To Populate Object to save Details of items
                    if (m_ListRetVendorDetails.Count == 0)
                    {
                        MessageBox.Show(Common.GetMessage("INF0075"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (m_TabMode.Equals(Common.TAB_CREATE_MODE, StringComparison.CurrentCultureIgnoreCase))
                        SetHeaderRecords((int)Common.RTVStatus.New);
                    else
                        SetHeaderRecords(m_objRetVendorHeader.StatusId);


                    if (m_ListRetVendorDetails.Count > 0)
                    {
                        listRetVendorDetails = PopulateSaveRecords();

                        if (listRetVendorDetails != null)
                        {
                            if (listRetVendorDetails.Count > 0)
                            {
                                m_objRetVendorHeader.ListRetVendorDetails = null;
                                m_objRetVendorHeader.ListRetVendorDetails = listRetVendorDetails;
                            }
                            else
                            {
                                //Object have some problem(Not Possible)
                                return;
                            }
                        }
                        else
                        {
                            //Object have some problem(Not Possible)
                            return;
                        }
                    }
                    else
                    {
                        //Object have some problem(Not Possible)
                        return;
                    }
                    #endregion

                }

                MemberInfo[] memberInfos = typeof(Common.RTVStatus).GetMembers(BindingFlags.Public | BindingFlags.Static);
               
                // Confirmation Before Saving
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", Common.GetConfirmationStatusText(((memberInfos[m_objRetVendorHeader.StatusId])).Name)) , Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (saveResult == DialogResult.Yes)
                {
                    RetVendorCommon objRetVendorCommon = new RetVendorCommon();
                    m_objRetVendorHeader.ReturnNo = txtReturnNumber.Text.Trim();
                    m_objRetVendorHeader.ModifiedDate = m_ModifiedDate;
                    m_objRetVendorHeader.ModifiedBy = m_UserID;
                    m_objRetVendorHeader.CurrentLocationType = Common.CurrentLocationTypeId;
                    errMessage = string.Empty;
                    bool IsSave = false;

                    IsSave = objRetVendorCommon.Save(Common.ToXml(m_objRetVendorHeader), ref errMessage);
                    if (errMessage.Equals(string.Empty))
                    {
                        txtReturnNumber.Text = objRetVendorCommon.ReturnNo;
                        txtDebitNoteNo.Text = objRetVendorCommon.DebitNoteNo;
                        m_objRetVendorHeader.ShipmentDate = objRetVendorCommon.ShippingDate;
                        txtCurrentStatus.Text = ((Common.RTVStatus)m_objRetVendorHeader.StatusId).ToString();
                        m_ModifiedDate = objRetVendorCommon.ModifiedDate;
                        m_TabMode = Common.TAB_UPDATE_MODE;
                        MessageBox.Show(Common.GetMessage("8013",((Common.RTVStatus)m_objRetVendorHeader.StatusId).ToString(), objRetVendorCommon.ReturnNo), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DisableEnableCon_OnStatus(m_objRetVendorHeader.StatusId);
                        //m_UpdateFlag = false;
                    }
                    else
                    {
                        if (errMessage.Equals("INF0022") || errMessage.Equals("INF0202"))
                        {
                            MessageBox.Show(Common.GetMessage(errMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else if (errMessage.Equals("INF0084"))
                        {
                            MessageBox.Show(Common.GetMessage(errMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        else
                        {
                            Common.LogException(new Exception(errMessage));
                            MessageBox.Show(Common.GetMessage(errMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //m_UpdateFlag = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //m_objRetVendorHeader.StatusId = (int)Common.RTVStatus.Created;
                SaveRecord(true);
            }
            catch (Exception ex)
            {

                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reset_CreateTabDate()
        {
            try
            {
                dtpReturnDate.CustomFormat = Common.DTP_DATE_FORMAT;
                DateTime dtAssignDate = Convert.ToDateTime(DateTime.Now.ToString(Common.DATE_TIME_FORMAT));
                dtpReturnDate.MaxDate = dtAssignDate;
                dtpReturnDate.Value = dtAssignDate;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Reset_SearchTabDate()
        {
            try
            {
                dtpSearchFrom.CustomFormat = Common.DTP_DATE_FORMAT;
                dtpSearchTO.CustomFormat = Common.DTP_DATE_FORMAT;

                DateTime dtAssignDate = Convert.ToDateTime(DateTime.Now.ToString(Common.DATE_TIME_FORMAT));
                dtpSearchFrom.MaxDate = dtAssignDate;
                dtpSearchFrom.Value = dtAssignDate;
                dtpSearchFrom.Checked = false;
                //dtpSearchFrom.MaxDate = dtAssignDate;
                //dtpSearchTO.Value = dtAssignDate;
                //dtpSearchTO.Value = dtAssignDate;
                dtpSearchTO.Value = dtAssignDate;
                dtpSearchTO.Checked = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Reset_CreateTab()
        {
            ResetItemControl(true);

            errItem.Clear();

            txtShippingDetails.Text = string.Empty;
            Reset_CreateTabDate();
            m_objRetVendorHeader = null;
            m_objRetVendorDetails = null;
            m_ModifiedDate = string.Empty;

            txtCurrentStatus.Text = Common.RTVStatus.New.ToString();

            txtDebitNoteAmount.Text = string.Empty;
            txtDebitNoteNo.Text = string.Empty;
            txtTotalTaxAmount.Text = string.Empty;
            txtTotalReturnQty.Text = string.Empty;
            txtTotalItemCost.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtReturnNumber.Text = string.Empty;
            
            //cmbVendorCode.SelectedIndex = 0;

            dgvReturnToVendorItems.DataSource = null;

            m_ListRetVendorDetails.Clear();

            m_TabMode = Common.TAB_CREATE_MODE;
            //m_UpdateFlag = false;
            tabControlTransaction.TabPages[1].Text = Common.TAB_CREATE_MODE;
            DisableEnableCon_OnStatus((int)Common.RTVStatus.New);
            dtpReturnDate.Checked = false;           

            if (m_LocationType == Common.LocationConfigId.WH)
            {
                cmbLocationCode.SelectedValue = m_CurrentLocationId;
                cmbVendorCode.SelectedIndex = 0;
                cmbLocationCode.Enabled = false;
                cmbVendorCode.Focus();
            }
            else
            {
                cmbLocationCode.SelectedIndex = 0;
                cmbVendorCode.SelectedIndex = 0;
                cmbLocationCode.Enabled = true;
                cmbLocationCode.Focus();
            }

            m_ListRetVendorHeader = null;
            m_objRetVendorHeader = null;

            dgvSearchReturnToVendor.DataSource = null;
            DisableEnableCon_OnStatus((int)Common.RTVStatus.New);
        }

        private List<RetVendorDetails> PopulateSaveRecords()
        {
            try
            {
                var listReturnVendorDetail = m_ListRetVendorDetails.GroupBy(x => new { x.ItemId, x.BucketId })
                                                             .Select(g => new
                                                             {
                                                                 ReturnNo = g.First<RetVendorDetails>().ReturnNo,
                                                                 ItemDescription = g.First<RetVendorDetails>().ItemDescription,
                                                                 ItemCode = g.First<RetVendorDetails>().ItemCode,
                                                                 ItemId = g.First<RetVendorDetails>().ItemId,
                                                                 BucketId = g.First<RetVendorDetails>().BucketId,
                                                                 GRNInvoiceNumber = g.First<RetVendorDetails>().GRNInvoiceNumber,
                                                                 GRNInvoiceType = g.First<RetVendorDetails>().GRNInvoiceType,
                                                                 LineTaxAmount = g.Sum(p => p.LineTaxAmount),
                                                                 GRNReceivedQty = g.Sum(p => p.GRNReceivedQty),
                                                                 TotalReturnQty = g.Sum(p => p.ReturnQty),
                                                                 AvailableQty = g.Sum(p => p.AvailableQty)

                                                             });






                List<RetVendorDetails> ListTotalRecords = new List<RetVendorDetails>();


                for (int intCounter = 0; intCounter < listReturnVendorDetail.ToList().Count; intCounter++)
                {
                    RetVendorDetails objRetVendorDetails = new RetVendorDetails();

                    objRetVendorDetails.BucketId = listReturnVendorDetail.ToList()[intCounter].BucketId;
                    objRetVendorDetails.ReturnNo = listReturnVendorDetail.ToList()[intCounter].ReturnNo;
                    objRetVendorDetails.ReturnQty = listReturnVendorDetail.ToList()[intCounter].TotalReturnQty;
                    objRetVendorDetails.ItemId = listReturnVendorDetail.ToList()[intCounter].ItemId;
                    objRetVendorDetails.ItemDescription = listReturnVendorDetail.ToList()[intCounter].ItemDescription;
                    objRetVendorDetails.AvailableQty = listReturnVendorDetail.ToList()[intCounter].AvailableQty;
                    objRetVendorDetails.GRNInvoiceNumber = listReturnVendorDetail.ToList()[intCounter].GRNInvoiceNumber;
                    objRetVendorDetails.GRNInvoiceType = listReturnVendorDetail.ToList()[intCounter].GRNInvoiceType;
                    objRetVendorDetails.GRNReceivedQty = listReturnVendorDetail.ToList()[intCounter].GRNReceivedQty;
                    objRetVendorDetails.LineTaxAmount = listReturnVendorDetail.ToList()[intCounter].LineTaxAmount;


                    var itemSelect = (from p in m_ListRetVendorDetails where p.BucketId == listReturnVendorDetail.ToList()[intCounter].BucketId && p.ItemId == listReturnVendorDetail.ToList()[intCounter].ItemId select p);

                    if (itemSelect != null)
                    {
                        if (itemSelect.ToList<RetVendorDetails>().Count > 0)
                        {
                            List<RetVendorBatchDetails> listRetVendorBatchDetails = new List<RetVendorBatchDetails>();

                            foreach (RetVendorDetails objBatchdetails in itemSelect.ToList<RetVendorDetails>())
                            {
                                RetVendorBatchDetails objRetVendorBatchDetails = new RetVendorBatchDetails();

                                objRetVendorBatchDetails.BatchNo = objBatchdetails.BatchNo;
                                objRetVendorBatchDetails.ItemId = objBatchdetails.ItemId;
                                objRetVendorBatchDetails.POAmount = objBatchdetails.POAmount;
                                objRetVendorBatchDetails.POQty = objBatchdetails.POQty;
                                objRetVendorBatchDetails.ReturnNo = objBatchdetails.ReturnNo;
                                objRetVendorBatchDetails.PODate = objBatchdetails.PODate;
                                objRetVendorBatchDetails.ReturnQty = objBatchdetails.ReturnQty;
                                objRetVendorBatchDetails.BucketId = objBatchdetails.BucketId;
                                objRetVendorBatchDetails.GRNInvoiceNumber = objBatchdetails.GRNInvoiceNumber;
                                objRetVendorBatchDetails.GRNInvoiceType = objBatchdetails.GRNInvoiceType;
                                objRetVendorBatchDetails.GRNReceivedQty = objBatchdetails.GRNReceivedQty;
                                objRetVendorBatchDetails.LineTaxAmount = objBatchdetails.LineTaxAmount;
                                objRetVendorBatchDetails.PONumber = objBatchdetails.PONumber;
                                objRetVendorBatchDetails.ReturnReason = objBatchdetails.ReturnReason;

                                listRetVendorBatchDetails.Add(objRetVendorBatchDetails);

                            }
                            objRetVendorDetails.ListRetVendorBatchDetails = listRetVendorBatchDetails;
                        }
                    }
                    ListTotalRecords.Add(objRetVendorDetails);
                }


                return ListTotalRecords;

            }
            catch (Exception)
            {

                throw;
            }
        }

        //private string GenerateErrorMessage()
        //{
        //    StringBuilder sbError = new StringBuilder();
        //    if (errItem.GetError(txtTOINumber).Trim().Length > 0)
        //    {
        //        sbError.Append(errItem.GetError(txtTOINumber));
        //        sbError.AppendLine();
        //    }
        //    if (errItem.GetError(txtPackSize).Trim().Length > 0)
        //    {
        //        sbError.Append(errItem.GetError(txtPackSize));
        //        sbError.AppendLine();
        //    }
        //    if (errItem.GetError(dtpExpectedDate).Trim().Length > 0)
        //    {
        //        sbError.Append(errItem.GetError(dtpExpectedDate));
        //        sbError.AppendLine();
        //    }

        //    if (errItem.GetError(txtRefNumber).Trim().Length > 0)
        //    {
        //        sbError.Append(errItem.GetError(txtRefNumber));
        //        sbError.AppendLine();
        //    }
        //    if (errItem.GetError(txtShippingDetails).Trim().Length > 0)
        //    {
        //        sbError.Append(errItem.GetError(txtShippingDetails));
        //        sbError.AppendLine();
        //    }

        //    if (errItem.GetError(txtShippingBillNo).Trim().Length > 0)
        //    {
        //        sbError.Append(errItem.GetError(txtShippingBillNo));
        //        sbError.AppendLine();
        //    }

        //    if (errItem.GetError(txtItemCode).Trim().Length > 0)
        //    {
        //        sbError.Append(errItem.GetError(txtItemCode));
        //        sbError.AppendLine();
        //    }
        //    if (errItem.GetError(txtBatchNo).Trim().Length > 0)
        //    {
        //        sbError.Append(errItem.GetError(txtBatchNo));
        //        sbError.AppendLine();
        //    }
        //    if (errItem.GetError(txtAdjustableQty).Trim().Length > 0)
        //    {
        //        sbError.Append(errItem.GetError(txtAdjustableQty));
        //        sbError.AppendLine();
        //    }
        //    return sbError;
        //}

        private void tabControlTransaction_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                if ((tabControlTransaction.SelectedIndex == 0) && dgvReturnToVendorItems.Rows.Count > 0)
                {

                    DialogResult result = MessageBox.Show(Common.GetMessage("VAL0026"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Cancel | DialogResult.No == result)
                    {
                        tabControlTransaction.SelectedIndex = 0;
                        e.Cancel = true;
                    }
                    else if (result == DialogResult.Yes)
                    {
                        Reset_CreateTab();
                        tabControlTransaction.TabPages[1].Text = Common.TAB_CREATE_MODE;
                        dgvReturnToVendorItems.ClearSelection();
                    }
                }
                else if (tabControlTransaction.SelectedIndex == 1)
                {
                    if (tabControlTransaction.TabPages[1].Text == Common.TAB_CREATE_MODE)
                    {
                        Reset_CreateTab();
                        //EnableDisableButton((int)Common.TIStatus.New);
                        //txtDistributorPCId.Enabled = true;
                        //cmbCustomerType.Enabled = true;
                    }

                }


                //if (tabControlTransaction.TabPages[1].Text.Equals(Common.TAB_UPDATE_MODE))
                //{
                //    if (m_UpdateFlag)
                //    {
                //        DialogResult confirmresult = MessageBox.Show(Common.GetMessage("5011"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //        if (confirmresult == DialogResult.Yes)
                //        {
                //            Reset_CreateTab();
                //            ResetSearch();
                //            e.Cancel = false;
                //        }

                //        else
                //        {
                //            e.Cancel = true;
                //        }
                //    }

                //}
                //else if (tabControlTransaction.TabPages[1].Text.Equals(Common.TAB_CREATE_MODE))
                //{
                //    if (m_TabMode.Equals(Common.TAB_CREATE_MODE))
                //        Reset_CreateTab();
                //}
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
                if (dtpSearchFrom.Checked == true && dtpSearchTO.Checked == true)
                {
                    if (dtpSearchTO.Value.Date < dtpSearchFrom.Value.Date)
                    {
                        Validators.SetErrorMessage(errSearch, dtpSearchTO, "VAL0068", "Search to", "from ");
                    }
                    else
                    {
                        Validators.SetErrorMessage(errSearch, dtpSearchTO);
                    }
                }
                string strError = string.Empty;

                strError = Validators.GetErrorMessage(errSearch, dtpSearchTO);
                if (!strError.Equals(string.Empty))
                {
                    MessageBox.Show(strError, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Search_Header();
            }
            catch (Exception ex)
            {

                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Search_Detail(string strReturnNo)
        {
            try
            {
                RetVendorCommon objRetVendorCommon = new RetVendorCommon();
                objRetVendorCommon.ReturnNo = strReturnNo;
                objRetVendorCommon.SelectMode = RetVendorCommon.RTVDETAILS;

                string errorMessage = string.Empty;

                m_ListRetVendorDetails = objRetVendorCommon.GetVendorDetail(Common.ToXml(objRetVendorCommon), RetVendorCommon.SP_GET_VENDOR_DETAILS, ref errorMessage);

                if (errorMessage.Equals(string.Empty))
                {
                    if (m_ListRetVendorDetails.Count <= 0)
                    {
                        MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    BindGridViewDetailItem(false);
                }
                else
                {
                    throw new Exception(errorMessage);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Search_Header()
        {
            try
            {
                RetVendorCommon objRetVendorCommon = new RetVendorCommon();

                if (dtpSearchFrom.Checked)
                    objRetVendorCommon.FromDate = dtpSearchFrom.Value.ToString(Common.DATE_TIME_FORMAT);
                else
                    objRetVendorCommon.FromDate = string.Empty;

                if (dtpSearchTO.Checked)
                    objRetVendorCommon.ToDate = dtpSearchTO.Value.ToString(Common.DATE_TIME_FORMAT);
                else
                    objRetVendorCommon.ToDate = string.Empty;

                objRetVendorCommon.LocationId = Convert.ToInt32(cmbSearchLocationCode.SelectedValue);
                objRetVendorCommon.VendorId = Convert.ToInt32(cmbSearchVendorCode.SelectedValue);
                objRetVendorCommon.ReturnNo = txtSearchVenRetNumber.Text.Trim();
                objRetVendorCommon.DebitNoteNumber = txtSearchDebitNo.Text.Trim();
                objRetVendorCommon.SelectMode = RetVendorCommon.RTVHEADER;
                objRetVendorCommon.Status = Convert.ToInt32(cmbSearchStatus.SelectedValue);

                string errorMessage = string.Empty;

                m_ListRetVendorHeader = objRetVendorCommon.GetHeaderDetails(Common.ToXml(objRetVendorCommon), RetVendorCommon.SP_GET_VENDOR_DETAILS, ref errorMessage);

                if (errorMessage.Equals(string.Empty))
                {
                    if (m_ListRetVendorHeader.Count <= 0)
                    {
                        MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"),
                                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    BindGridViewHeaderItem();
                }
                else
                {
                    throw new Exception(errorMessage);
                }

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisableEnableCon_OnStatus(int Status)
        {
            switch (Status)
            {
                case (int)Common.RTVStatus.New:
                    txtItemCode.Enabled = true;
                    txtReturnQty.Enabled = true;
                    txtReturnReason.Enabled = true;
                    txtShippingDetails.Enabled = false;
                    txtRemarks.Enabled = true;
                    dtpReturnDate.Enabled = true;

                    cmbPONumber.Enabled = true;
                    cmbGRNInvoiceNo.Enabled = true;
                    cmbGRNInvoiceType.Enabled = true;
                    btnAddDetails.Enabled = true;

                    btnClearDetails.Enabled = true;
                    btnSave.Enabled = false;
                    btnConfirm.Enabled = false;
                    btnShip.Enabled = false;
                    btnApprove.Enabled = false;
                    btnRTVCancel.Enabled = false;
                    btnCreateReset.Enabled = true;
                    btnPrint.Enabled = false;
                    if (m_LocationType == Common.LocationConfigId.WH)
                    {
                        cmbLocationCode.SelectedValue = m_SelectedLocationID;
                        cmbLocationCode.Enabled = false;
                    }
                    else
                    {
                        cmbLocationCode.SelectedIndex = 0;
                        cmbLocationCode.Enabled = true;
                    }
                    cmbVendorCode.Enabled = true;
                    break;

                case (int)Common.RTVStatus.Created:
                    txtItemCode.Enabled = true;
                    txtReturnQty.Enabled = true;
                    txtReturnReason.Enabled = true;
                    txtShippingDetails.Enabled = false;
                    txtRemarks.Enabled = true;
                    dtpReturnDate.Enabled = true;
                    cmbPONumber.Enabled = true;
                    cmbGRNInvoiceNo.Enabled = true;
                    cmbGRNInvoiceType.Enabled = true;
                    btnAddDetails.Enabled = true;
                    btnClearDetails.Enabled = true;
                    btnSave.Enabled = true;
                    btnConfirm.Enabled = true;
                    btnRTVCancel.Enabled = true;
                    btnShip.Enabled = false;
                    btnApprove.Enabled = false;
                    btnCreateReset.Enabled = true;
                    btnPrint.Enabled = false;
                    cmbLocationCode.Enabled = false;
                    cmbVendorCode.Enabled = false;
                    break;
                case (int)Common.RTVStatus.Cancelled:
                    txtItemCode.Enabled = false;
                    txtReturnQty.Enabled = false;
                    txtReturnReason.Enabled = false;
                    txtShippingDetails.Enabled = false;
                    txtRemarks.Enabled = false;
                    dtpReturnDate.Enabled = false;
                    cmbPONumber.Enabled = false;
                    cmbGRNInvoiceNo.Enabled = false;
                    cmbGRNInvoiceType.Enabled = false;
                    btnAddDetails.Enabled = false;
                    btnClearDetails.Enabled = false;
                    btnSave.Enabled = false;
                    btnRTVCancel.Enabled = false;
                    btnConfirm.Enabled = false;
                    btnShip.Enabled = false;
                    btnApprove.Enabled = false;
                    //btnCreateReset.Enabled = false;
                    btnCreateReset.Enabled = true;
                    btnPrint.Enabled = true;
                    cmbLocationCode.Enabled = false;
                    cmbVendorCode.Enabled = false;
                    break;

                case (int)Common.RTVStatus.Confirmed:
                    txtItemCode.Enabled = false;
                    txtReturnQty.Enabled = false;
                    txtReturnReason.Enabled = false;
                    txtShippingDetails.Enabled = false;
                    txtRemarks.Enabled = true;
                    dtpReturnDate.Enabled = false;
                    cmbPONumber.Enabled = false;
                    cmbGRNInvoiceNo.Enabled = false;
                    cmbGRNInvoiceType.Enabled = false;
                    btnAddDetails.Enabled = false;
                    btnClearDetails.Enabled = false;
                    btnRTVCancel.Enabled = m_currentLocationType == 1 ? true : false;
                    btnSave.Enabled = false;
                    btnConfirm.Enabled = false;
                    btnShip.Enabled = false;
                    btnApprove.Enabled = true;
                    //btnCreateReset.Enabled = false;
                    btnCreateReset.Enabled = true;
                    btnPrint.Enabled = false;
                    cmbLocationCode.Enabled = false;
                    cmbVendorCode.Enabled = false;
                    break;

                case (int)Common.RTVStatus.Approved:
                    txtItemCode.Enabled = false;
                    txtReturnQty.Enabled = false;
                    txtReturnReason.Enabled = false;
                    txtShippingDetails.Enabled = true;
                    txtRemarks.Enabled = false;
                    dtpReturnDate.Enabled = false;
                    cmbPONumber.Enabled = false;
                    cmbGRNInvoiceNo.Enabled = false;
                    cmbGRNInvoiceType.Enabled = false;
                    btnAddDetails.Enabled = false;
                    btnClearDetails.Enabled = false;
                    btnClearDetails.Enabled = false;
                    btnRTVCancel.Enabled = false;
                    btnSave.Enabled = false;
                    btnConfirm.Enabled = false;
                    btnShip.Enabled = true;
                    btnApprove.Enabled = false;
                    //btnCreateReset.Enabled = false;
                    btnCreateReset.Enabled = true;
                    btnPrint.Enabled = true;
                    cmbLocationCode.Enabled = false;
                    cmbVendorCode.Enabled = false;
                    break;

                case (int)Common.RTVStatus.Shipped:
                    txtItemCode.Enabled = false;
                    txtReturnQty.Enabled = false;
                    txtReturnReason.Enabled = false;
                    txtShippingDetails.Enabled = false;
                    txtRemarks.Enabled = false;
                    dtpReturnDate.Enabled = false;
                    cmbPONumber.Enabled = false;
                    cmbGRNInvoiceNo.Enabled = false;
                    cmbGRNInvoiceType.Enabled = false;
                    btnAddDetails.Enabled = false;
                    btnClearDetails.Enabled = false;
                    btnSave.Enabled = false;
                    btnConfirm.Enabled = false;
                    btnShip.Enabled = false;
                    btnApprove.Enabled = false;
                    btnRTVCancel.Enabled = false;
                    //btnCreateReset.Enabled = false;
                    btnCreateReset.Enabled = true;
                    btnPrint.Enabled = true;
                    cmbLocationCode.Enabled = false;
                    cmbVendorCode.Enabled = false;
                    break;

                default:
                    break;
            }

            btnSave.Enabled = btnSave.Enabled & IsCreateAvailable;
            btnConfirm.Enabled = btnConfirm.Enabled & IsConfirmAvailable;
            btnApprove.Enabled = btnApprove.Enabled & IsApproveAvailable;
            btnPrint.Enabled = IsPrintAvailable;
            btnShip.Enabled = btnShip.Enabled & IsShipAvailable;
            btnRTVCancel.Enabled = btnRTVCancel.Enabled & IsCancelAvailable;
        }

        private void btnCreateReset_Click(object sender, EventArgs e)
        {
            try
            {
                Reset_CreateTab();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                m_objRetVendorHeader.StatusId = (int)Common.RTVStatus.Confirmed;
                SaveRecord(true);
            }
            catch (Exception ex)
            {

                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                m_objRetVendorHeader.StatusId = (int)Common.RTVStatus.Approved;
                SaveRecord(true);
            }
            catch (Exception ex)
            {

                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRTVCancel_Click(object sender, EventArgs e)
        {
            try
            {
                m_objRetVendorHeader.StatusId = (int)Common.RTVStatus.Cancelled;
                SaveRecord(false);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShip_Click(object sender, EventArgs e)
        {
            try
            {
                m_objRetVendorHeader.StatusId = (int)Common.RTVStatus.Shipped;
                SaveRecord(true);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateReturnQtyInUpdateMode()
        {
            try
            {
                foreach (RetVendorDetails objRetVendorDetails in m_ListRetVendorDetails)
                {
                    if (objRetVendorDetails.ReturnQty > objRetVendorDetails.AvailableQty)
                    {
                        m_selectedItemRowIndex = m_ListRetVendorDetails.FindIndex(delegate(RetVendorDetails obj) { return obj.BucketId == objRetVendorDetails.BucketId && obj.BatchNo.Equals(objRetVendorDetails.BatchNo, StringComparison.CurrentCultureIgnoreCase) && obj.ItemId == objRetVendorDetails.ItemId; });

                        BindGridViewDetailItem(false);
                        MessageBox.Show(Common.GetMessage("VAL0060", "Return Qty (" + objRetVendorDetails.ItemCode + " )", "available qty"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetSearch();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetSearch()
        {
            try
            {
                m_ListRetVendorHeader = null;
                dgvSearchReturnToVendor.DataSource = null;
                if (m_LocationType == Common.LocationConfigId.HO)
                {
                    cmbSearchLocationCode.SelectedIndex = 0;
                }
                cmbSearchStatus.SelectedIndex = 0;
                cmbSearchVendorCode.SelectedIndex = 0;
                txtSearchVenRetNumber.Text = string.Empty;
                txtSearchDebitNo.Text = string.Empty;                
                cmbSearchLocationCode.Focus();
                Reset_SearchTabDate();
            }
            catch (Exception)
            {
                throw;
            }
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

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Validators.CheckForEmptyString(txtItemCode.Text.Length))
                {
                    //Validators.SetErrorMessage(errSearch, txtItemCode, "VAL0001", lblItemCode.Text);
                    errSearch.SetError(txtItemCode, Common.GetMessage("VAL0001", lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Trim().Length - 2)));
                }
                else
                {
                    errSearch.SetError(txtItemCode, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objRetVendorHeader != null && m_objRetVendorHeader.StatusId >= (int)Common.RTVStatus.Approved)
                {
                    btnPrint.Enabled = false;
                    PrintReport();
                    btnPrint.Enabled = true;
                }
                else
                    MessageBox.Show(Common.GetMessage("INF0101", "Return", Common.RTVStatus.Approved.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ValidateGRNInvoiceType(bool yesNo)
        {
            if (yesNo == false)
            {
                if (m_vendorView != null && cmbGRNInvoiceType.SelectedIndex > 0 && m_vendorView.Count>0)
                {
                    //"IsFormCApplicable", "UnitPrice", "ReceivedQty"
                    DataView vendorView;
                    cmbGRNInvoiceNo.SelectedIndexChanged -= new EventHandler(cmbGRNInvoiceNo_SelectedIndexChanged);
                    if (Convert.ToInt32(cmbGRNInvoiceType.SelectedValue) == (int)Common.InvoiceGRNType.Invoice)
                    {
                        vendorView = new DataView(m_vendorView.ToTable(true, "InvoiceNo", "PONumber"));
                        vendorView.RowFilter = "PONumber = '" + cmbPONumber.SelectedValue.ToString() + "' AND InvoiceNo <> '' OR PONumber ='Select'";
                        
                        cmbGRNInvoiceNo.DataSource = vendorView;
                        cmbGRNInvoiceNo.DisplayMember = "InvoiceNo";
                        cmbGRNInvoiceNo.ValueMember = "InvoiceNo";
                    }
                    else
                    {
                        vendorView = new DataView(m_vendorView.ToTable(true, "GRNNo", "PONumber"));
                        vendorView.RowFilter = "PONumber = '" + cmbPONumber.SelectedValue.ToString() + "' AND GRNNo <> '' OR PONumber ='Select'";
                        cmbGRNInvoiceNo.DataSource = vendorView;
                        cmbGRNInvoiceNo.DisplayMember = "GRNNo";
                        cmbGRNInvoiceNo.ValueMember = "GRNNo";
                    }

                    cmbGRNInvoiceNo.SelectedIndexChanged += new EventHandler(cmbGRNInvoiceNo_SelectedIndexChanged);
                    errItem.SetError(cmbGRNInvoiceType, string.Empty);
                }
                else if (cmbGRNInvoiceType.SelectedIndex == 0)
                {
                    errItem.SetError(cmbGRNInvoiceType, Common.GetMessage("VAL0001", lblInvoiceGRNType.Text.Trim().Substring(0, lblInvoiceGRNType.Text.Trim().Length - 2)));
                }
            }
        }

        private void cmbGRNInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {                
                txtGRNInvoiceQty.Text = string.Empty;                
                ValidateGRNInvoiceType(false);
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ValidateGRNInvoiceNo(bool yesNo)
        {
            if (m_vendorView != null && cmbGRNInvoiceNo.SelectedIndex > 0)
            {
                DataView vendorView;

                if (Convert.ToInt32(cmbGRNInvoiceType.SelectedValue) == (int)Common.InvoiceGRNType.Invoice)
                {
                    vendorView = new DataView(m_vendorView.ToTable(true, "InvoiceNo", "UnitPrice", "ReceivedQty"));
                    vendorView.RowFilter = "InvoiceNo ='" + cmbGRNInvoiceNo.SelectedValue.ToString() + "'";
                }
                else
                {
                    vendorView = new DataView(m_vendorView.ToTable(true, "PONumber", "PODate", "POQty", "MRP", "InvoiceNo", "GRNNo", "IsFormCApplicable", "UnitPrice", "ReceivedQty"));

                    //vendorView = new DataView(m_vendorView.ToTable(true, "GRNNo", "UnitPrice", "ReceivedQty"));
                    vendorView.RowFilter = "PONumber ='" + cmbPONumber.SelectedValue + "' AND GRNNo ='" + cmbGRNInvoiceNo.SelectedValue.ToString() + "'";
                }

                if (vendorView != null && vendorView.Count > 0)
                {
                    m_objRetVendorDetails.UnitPrice = Convert.ToDecimal(vendorView[0]["UnitPrice"]);
                    txtGRNInvoiceQty.Text = Math.Round(Convert.ToDouble(vendorView[0]["ReceivedQty"]), CoreComponent.Core.BusinessObjects.Common.DisplayQtyRounding, MidpointRounding.AwayFromZero).ToString();
                    errItem.SetError(cmbGRNInvoiceNo, string.Empty);
                }
                else
                {
                    errItem.SetError(cmbGRNInvoiceNo, Common.GetMessage("VAL0006", lblGRNInvoiceNo.Text.Trim().Substring(0, lblGRNInvoiceNo.Text.Trim().Length - 2)));
                }
            }
            else if (cmbGRNInvoiceNo.SelectedIndex == 0 && yesNo == true)
            {
                errItem.SetError(cmbGRNInvoiceNo, Common.GetMessage("VAL0001", lblGRNInvoiceNo.Text.Trim().Substring(0, lblGRNInvoiceNo.Text.Trim().Length - 2)));
            }
        }

        private void cmbGRNInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateGRNInvoiceNo(false);
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ValidatePONumber(bool yesNo)
        {
            if (m_vendorView != null && cmbPONumber.SelectedIndex > 0)
            {
                DataView vendorView;

                vendorView = new DataView(m_vendorView.ToTable(true, "PONumber", "PODate", "POQty", "MRP"));
                vendorView.RowFilter = "PONumber ='" + cmbPONumber.SelectedValue.ToString() + "'";

                if (vendorView != null && vendorView.Count > 0)
                {
                    m_objRetVendorDetails.PODate = Convert.ToDateTime(vendorView[0]["PODate"]).ToString(Common.DATE_TIME_FORMAT);
                    m_objRetVendorDetails.POQty = Convert.ToDouble(vendorView[0]["POQty"]);

                    txtPODate.Text = Convert.ToDateTime(vendorView[0]["PODate"]).ToString(Common.DTP_DATE_FORMAT);
                    txtPOItemQty.Text = Math.Round(m_objRetVendorDetails.POQty, CoreComponent.Core.BusinessObjects.Common.DisplayQtyRounding, MidpointRounding.AwayFromZero).ToString();

                    txtPOItemAmount.Text = Math.Round(Convert.ToDouble(vendorView[0]["MRP"]), CoreComponent.Core.BusinessObjects.Common.DisplayAmountRounding, MidpointRounding.AwayFromZero).ToString();
                    m_objRetVendorDetails.POAmount = Math.Round(Convert.ToDouble(vendorView[0]["MRP"]), CoreComponent.Core.BusinessObjects.Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                }

                errItem.SetError(cmbPONumber, string.Empty);
            }
            else if (cmbPONumber.SelectedIndex == 0 && yesNo == true)
            {
                errItem.SetError(cmbPONumber, Common.GetMessage("VAL0001", lblPONumber.Text.Trim().Substring(0, lblPONumber.Text.Trim().Length - 2)));
            }

            if (yesNo == false)
            {
                cmbGRNInvoiceNo.SelectedIndexChanged -= new EventHandler(cmbGRNInvoiceNo_SelectedIndexChanged);
                cmbGRNInvoiceType.SelectedIndexChanged -= new EventHandler(cmbGRNInvoiceType_SelectedIndexChanged);

                cmbGRNInvoiceType.SelectedIndex = 0;
                cmbGRNInvoiceNo.SelectedIndex = 0;
                cmbGRNInvoiceNo.SelectedIndexChanged += new EventHandler(cmbGRNInvoiceNo_SelectedIndexChanged);
                cmbGRNInvoiceType.SelectedIndexChanged += new EventHandler(cmbGRNInvoiceType_SelectedIndexChanged);
            }

            if (cmbPONumber.SelectedIndex == 0)
            {

                txtPODate.Text = string.Empty;
                txtPOItemQty.Text = string.Empty;
                txtPOItemAmount.Text = string.Empty;
                txtGRNInvoiceQty.Text = string.Empty;
                txtReturnQty.Text = string.Empty;
                txtItemTax.Text = string.Empty;
            }
        }

        private void cmbPONumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ValidatePONumber(false);
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSearchReturnToVendor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvSearchReturnToVendor.SelectedRows.Count > 0)
                {
                    string returnNo = Convert.ToString(dgvSearchReturnToVendor.SelectedRows[0].Cells["ReturnNo"].Value);

                    SelectSearchGrid(returnNo);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintDebitNote_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objRetVendorHeader != null && m_objRetVendorHeader.StatusId >= (int)Common.RTVStatus.Confirmed)
                {
                    btnPrintDebitNote.Enabled = false;
                    PrintDebitNoteReport();
                    btnPrintDebitNote.Enabled = true;
                }
                else
                    MessageBox.Show(Common.GetMessage("INF0101", "Return", Common.RTVStatus.Confirmed.ToString()), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                btnPrintDebitNote.Enabled = true;
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}

