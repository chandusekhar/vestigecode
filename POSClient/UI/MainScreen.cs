using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using AuthenticationComponent.BusinessObjects;
using POSClient.BusinessObjects;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using PromotionsComponent.BusinessLayer;
using Vinculum.Framework.Logging;
using Vinculum.Framework.Printing;
using System.Reflection;
using CoreComponent.Core.UI;
using CoreComponent.UI;
using POSClient.UI.Controls;

namespace POSClient.UI
{
    public partial class MainScreen : Form
    {

        #region Variable Declarations

        //private string m_sDistName;
        private CO m_currentOrder;
        private List<CO> m_holdOrders;
        private int m_multiplierIndex;
        private MDIPOS frmMdiParent;
        private ContextMenuStrip m_mnuContextMenu;
        private DataTable dtPOSLocations;
        private int m_switchIndex;
        private string m_LocationCode;
        private string m_UserName;
        private CO m_existingOrder;
        private PrintManager m_printMgr;
        private bool isModify = false;
        private Boolean IsNewOrderAvailable = false;
        private Boolean IsTeamOrderAvailable = false;
        private Boolean IsRegisterDistributorAvailable = false;
        private Boolean IsKitOrderAvailable = false;
        private Boolean IsCancelAvailable = false;
        private Boolean IsPreviewAvailable = false;
        private Boolean IsConfirmAvailable = false;
        private Boolean IsInvoiceAvailable = false;
        private Boolean IsPrintInvoiceAvailable = false;
        private Boolean IsPrintOrderAvailable = false;
        private Boolean IsLogAvailable = false;
        private Boolean IsStockPointAvailable = false;
        private Boolean IsChangeDeliveryModeAvailable = false;
        private Boolean IsModifyAvailable = false;
        private bool _isKitAdded;
        private ItemSelector itemSelector;
        private Boolean IsCourierRequired = false;
        private Boolean IsCourierRequiredOnOrder = false;
        private List<CO> lstOrder;
        decimal WaiveoffCourierLimit;
        #endregion

        #region C'tors

        public MainScreen()
        {
            InitializeComponent();
            InitializeApplication();
        }

        public MainScreen(MDIPOS mdi)
        {
            InitializeComponent();
            InitializeApplication();
            frmMdiParent = mdi;
        }

        #endregion

        #region Properties

        public List<CO> HoldOrders
        {
            get { return m_holdOrders; }
            set { m_holdOrders = value; }
        }
        public CO CurrentOrder
        {
            get { return m_currentOrder; }
            set { m_currentOrder = value; }
        }

        #endregion

        #region Event Handlers

        private void dgvReceiptItems_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvReceiptItems.Rows.Count > 0)
                {
                    DataGridView.HitTestInfo hitTestInfo;
                    if ((e.Button == MouseButtons.Right) || (e.Button == MouseButtons.Left))
                    {
                        hitTestInfo = dgvReceiptItems.HitTest(e.X, e.Y);
                        // If column is first column
                        if (hitTestInfo.RowIndex != -1)
                        {
                            if (m_currentOrder.Status == (int)Common.OrderStatus.Created && !isModify && !string.IsNullOrEmpty(m_currentOrder.CustomerOrderNo))
                            //(m_currentOrder.Status == (int)Common.OrderStatus.Cancelled) ||
                            //(m_currentOrder.Status == (int)Common.OrderStatus.Confirmed) ||
                            //(m_currentOrder.Status == (int)Common.OrderStatus.Invoiced) ||
                            //(m_currentOrder.Status == (int)Common.OrderStatus.InvoiceCancelled))
                            {
                                //dont show context menu in this case
                                if (e.Button == MouseButtons.Right)
                                {
                                    dgvReceiptItems.Rows[hitTestInfo.RowIndex].Selected = true;
                                }
                                if (!string.IsNullOrEmpty(((CODetail)dgvReceiptItems.Rows[hitTestInfo.RowIndex].DataBoundItem).GiftVoucherNumber))
                                {
                                    m_mnuContextMenu.Items[0].Text = dgvReceiptItems.Rows[hitTestInfo.RowIndex].Cells[1].Value.ToString();
                                    m_mnuContextMenu.Show(dgvReceiptItems, new Point(e.X, e.Y));
                                }
                            }
                            else if (m_currentOrder.Status == (int)Common.OrderStatus.Created && isModify && !string.IsNullOrEmpty(m_currentOrder.CustomerOrderNo))
                            {
                                if (e.Button == MouseButtons.Right)
                                {
                                    dgvReceiptItems.Rows[hitTestInfo.RowIndex].Selected = true;
                                }
                                if (!((CODetail)dgvReceiptItems.Rows[hitTestInfo.RowIndex].DataBoundItem).IsPromo)
                                {
                                    m_mnuContextMenu.Items[0].Text = dgvReceiptItems.Rows[hitTestInfo.RowIndex].Cells[1].Value.ToString();
                                    m_mnuContextMenu.Show(dgvReceiptItems, new Point(e.X, e.Y));
                                }
                                //m_mnuContextMenu.Items[0].Text = dgvReceiptItems.Rows[hitTestInfo.RowIndex].Cells[1].Value.ToString();
                                //m_mnuContextMenu.Show(dgvReceiptItems, new Point(e.X, e.Y));
                            }
                            else if (m_currentOrder.Status == (int)Common.OrderStatus.Created && string.IsNullOrEmpty(m_currentOrder.CustomerOrderNo))
                            {
                                if (e.Button == MouseButtons.Right)
                                {
                                    dgvReceiptItems.Rows[hitTestInfo.RowIndex].Selected = true;
                                }

                                m_mnuContextMenu.Items[0].Text = dgvReceiptItems.Rows[hitTestInfo.RowIndex].Cells[1].Value.ToString();
                                m_mnuContextMenu.Show(dgvReceiptItems, new Point(e.X, e.Y));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }

        }
        void FillLogDropDown()
        {
            COLog log = new COLog();
            List<COLog> LogList = log.Search(1, Common.CurrentLocationId, string.Empty, (int)Common.COLogType.Log, Common.INT_DBNULL, Common.INT_DBNULL);
            if (LogList != null)
            {
                COLog logSearch = new COLog();
                logSearch.LogNo = "Select Log";
                LogList.Insert(0, logSearch);

                cmbLogList.DataSource = LogList;
                cmbLogList.DisplayMember = "LogNo";
                cmbLogList.ValueMember = "LogValue";
            }
        }
        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            try
            {
                //cmbLogList.ValueMember = "LogValue";

                BindLogList((int)Common.COLogType.Log, string.Empty);

                //FillLogDropDown();

                SetScreenState(Common.ScreenMode.NewOrder);
                CreateOrder(Common.OrderType.FirstOrder);
                if ((int)Common.LocationConfigId.BO == Common.CurrentLocationTypeId)
                    rdoByHand.Checked = true;
                else
                    rdoCourier.Checked = true;
                SetDeliveryFromAddress();

                DataTable dtTaxJurisdiction = Common.ParameterLookup(Common.ParameterType.GetTaxJurisdictionByLocId, new ParameterFilter("", Common.BOId, -1, -1));
                if (dtTaxJurisdiction != null && dtTaxJurisdiction.Rows.Count == 1)
                    m_currentOrder.TaxJurisdictionId = dtTaxJurisdiction.Rows[0][0].ToString();
                else m_currentOrder.TaxJurisdictionId = Common.INT_DBNULL.ToString();

                SetDeliveryToAddress();

                string dbMessage = string.Empty;
                iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, false, 1, ref dbMessage));
                iPayments.LoadItems<POSPayments>(POSPayments.Search(ref dbMessage));
                txtDistributorNo.Focus();
                isModify = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void btnKitOrder_Click(object sender, EventArgs e)
        {
            try
            {
                SetScreenState(Common.ScreenMode.KitOrder);
                CreateOrder(Common.OrderType.KitOrder);
                if ((int)Common.LocationConfigId.BO == Common.CurrentLocationTypeId)
                    rdoByHand.Checked = true;
                else
                    rdoCourier.Checked = true;
                SetDeliveryFromAddress();
                DataTable dtTaxJurisdiction = Common.ParameterLookup(Common.ParameterType.GetTaxJurisdictionByLocId, new ParameterFilter("", Common.BOId, -1, -1));
                if (dtTaxJurisdiction != null && dtTaxJurisdiction.Rows.Count == 1)
                    m_currentOrder.TaxJurisdictionId = dtTaxJurisdiction.Rows[0][0].ToString();
                else m_currentOrder.TaxJurisdictionId = Common.INT_DBNULL.ToString();
                SetDeliveryToAddress();

                string dbMessage = string.Empty;
                iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, true, 2, ref dbMessage));
                iPayments.LoadItems<POSPayments>(POSPayments.Search(ref dbMessage));
                txtDistributorNo.Focus();
                isModify = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void btnTeamOrder_Click(object sender, EventArgs e)
        {
            try
            {
                TeamOrder order = new TeamOrder();
                DialogResult dr = order.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    int logType = 0;
                    if (rdoTeamOrder.Checked)
                    {
                        logType = (int)Common.COLogType.TeamOrder;
                    }
                    else if (rdoLog.Checked)
                    {
                        logType = (int)Common.COLogType.Log;
                    }
                    BindLogList(logType, null);
                }
                //Reload Order after closing team-order screen
                if (m_currentOrder != null && m_currentOrder.CustomerOrderNo != null && m_currentOrder.CustomerOrderNo != "")
                {
                    m_currentOrder.GetCOAllDetails(m_currentOrder.CustomerOrderNo, -1);
                    LoadOrder(m_currentOrder);

                    if (m_currentOrder.Status == (int)Common.OrderStatus.Created)
                        SetScreenState(Common.ScreenMode.OrderSaved);
                    else if (m_currentOrder.Status == (int)Common.OrderStatus.Confirmed)
                        SetScreenState(Common.ScreenMode.OrderConfirmed);
                    else if (m_currentOrder.Status == (int)Common.OrderStatus.Cancelled)
                        SetScreenState(Common.ScreenMode.OrderCancelled);
                    else if (m_currentOrder.Status == (int)Common.OrderStatus.Modify)
                        SetScreenState(Common.ScreenMode.OrderModify);
                    else if (m_currentOrder.Status == (int)Common.OrderStatus.Invoiced)
                        SetScreenState(Common.ScreenMode.Invoiced);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void btnPrintCO_Click(object sender, EventArgs e)
        {
            if (m_currentOrder == null)
                throw new Exception("No order to print");

            try
            {
                DataSet dsReport = CreatePrintDataSet((int)Common.PrintType.PrintOrder);
                CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.CustomerOrder, dsReport);
                reportScreenObj.ShowDialog();
                dsReport = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void btnPrintCI_Click(object sender, EventArgs e)
        {
            if (m_currentOrder == null)
                throw new Exception("No order to print");

            try
            {
                ////this is required to print the tax reg number. 
                ////If AppConfiguration.TaxNumber is blank it will not be printed.
                //for (int i = 0; i < Common.PrintCopy; i++)
                //    m_printMgr.Print(Common.ToXml(m_currentOrder));
                DataSet dsReport = CreatePrintDataSet((int)Common.PrintType.PrintInvoice);
                CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.CustomerInvoice, dsReport);

                //EnableOrDisblePrintButton(reportScreenObj); 

                reportScreenObj.ShowDialog();
                dsReport = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void EnableOrDisblePrintButton()
        {

            // To stop invoive printing of 0 price kit print 
            // Disallow invoice printing of older invoices


            if (m_currentOrder.OrderType == (int)CoreComponent.Core.BusinessObjects.Common.OrderType.KitOrder)
            {
                if (Convert.ToBoolean(m_currentOrder.IsPrintAllowed) == true)
                {
                    this.btnPrintCI.Enabled = true;
                    //btnInvoiceandPrint.Enabled = true;
                }
                else
                {
                    this.btnPrintCI.Enabled = false;
                    //btnInvoiceandPrint.Enabled = false;
                }
            }
            else if ((Convert.ToInt32(m_currentOrder.ValidReportPrintDays) > 0) && Convert.ToInt32(m_currentOrder.ValidReportPrintDays) >= (DateTime.Today - Convert.ToDateTime(m_currentOrder.InvoiceDate)).Days)
            {

                this.btnPrintCI.Enabled = true;
                // btnInvoiceandPrint.Enabled = true;
            }
            else
            {
                this.btnPrintCI.Enabled = false;
                // btnInvoiceandPrint.Enabled = false;
            }


        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_switchIndex == 0 && m_holdOrders.Count == 0)
                {
                    //Do Nothing
                }
                else
                {
                    if (m_switchIndex == 0)
                    {
                        SetSwitchIndex(m_holdOrders.Count);
                    }
                    if (m_currentOrder != null && m_currentOrder.Status == (int)Common.OrderStatus.Created)
                    {
                        if (!m_holdOrders.Contains(m_currentOrder))
                        {
                            LoadOrder(m_holdOrders[m_switchIndex - 1]);
                            SetSwitchIndex(-1);
                            m_holdOrders.Remove(m_currentOrder);
                        }
                        else
                        {
                            LoadOrder(m_holdOrders[m_switchIndex - 1]);
                            m_holdOrders.Remove(m_currentOrder);
                            SetSwitchIndex(-1);
                        }
                    }
                    else
                    {
                        LoadOrder(m_holdOrders[m_switchIndex - 1]);
                        m_holdOrders.Remove(m_currentOrder);
                        SetSwitchIndex(-1);
                    }
                    Common.ScreenMode mode = Common.ScreenMode.OrderInMemory;
                    if (string.IsNullOrEmpty(m_currentOrder.CustomerOrderNo))
                        SetScreenState(mode);
                    else
                    {
                        switch (m_currentOrder.Status)
                        {
                            case 1:
                                {
                                    mode = Common.ScreenMode.OrderSaved;
                                    break;
                                }
                            case 2:
                                {
                                    mode = Common.ScreenMode.OrderCancelled;
                                    break;
                                }
                            case 3:
                                {
                                    mode = Common.ScreenMode.OrderConfirmed;
                                    break;
                                }
                            case 4:
                                {
                                    mode = Common.ScreenMode.Invoiced;
                                    break;
                                }
                            case 5:
                                {
                                    mode = Common.ScreenMode.InvoiceCancelled;
                                    break;
                                }
                            default:
                                break;
                        }
                        SetScreenState(mode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_currentOrder != null)
                {
                    bool bProceed = true;
                    bool bSuccess = false;
                    if ((m_currentOrder.Status == (int)Common.OrderStatus.Created) || (m_currentOrder.Status == (int)Common.OrderStatus.Confirmed))
                    {
                        if (!string.IsNullOrEmpty(m_currentOrder.CustomerOrderNo))
                        {
                            bSuccess = SaveOrder((int)Common.OrderStatus.Cancelled);
                            bProceed = false;
                        }
                        else
                        {
                            m_currentOrder.Status = ((int)Common.OrderStatus.Cancelled);
                            bProceed = true;
                        }
                    }
                    else
                    {
                        bProceed = false;
                        //Show message that order cannot be cancelled
                    }
                    if (bProceed)
                    {
                        //m_currentOrder = null;
                        if (m_holdOrders.Count <= 0)
                        {
                            m_currentOrder = null;
                            SetScreenState(Common.ScreenMode.LoggedIn);
                        }
                        else
                        {
                            LoadOrder(m_holdOrders[m_switchIndex - 1]);
                            SetSwitchIndex(-1);
                            m_holdOrders.Remove(m_currentOrder);
                            if (m_currentOrder.Status == 1 && string.IsNullOrEmpty(m_currentOrder.CustomerOrderNo))
                            {
                                SetScreenState(Common.ScreenMode.OrderInMemory);
                            }
                            else if (m_currentOrder.Status == 1)
                            {
                                SetScreenState(Common.ScreenMode.OrderSaved);
                            }
                        }
                    }
                    else
                    {
                        if (bSuccess)
                            SetScreenState(Common.ScreenMode.OrderCancelled);

                        if (iItems != null && iItems.ItemCount > 0)
                            iItems.Reset();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            try
            {
                m_holdOrders = new List<CO>();
                SetMultiplierIndex(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbLogList.SelectedIndex > 0)
                {
                    m_currentOrder.LogValue = Convert.ToString(cmbLogList.SelectedValue);
                    m_currentOrder.LogNo = Convert.ToString(cmbLogList.Text);
                }
                if ((m_currentOrder.PCId != Common.BOId) && (String.IsNullOrEmpty(m_currentOrder.LogNo)) && Common.CurrentLocationTypeId == (int)Common.LocationConfigId.BO)
                {
                    DialogResult dr = MessageBox.Show(Common.GetMessage("VAL0606"), Common.GetMessage("10004"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.No)
                        return;
                }
                if (m_currentOrder.CODetailList.Count > 0)
                {
                    bool toSave = true;
                    if (m_currentOrder.IsFirstOrderForDistributor)
                    {
                        toSave = true;
                        //if (m_currentOrder.CODetailList.Find(p => p.IsKit == true) == null)
                        //{
                        //    toSave = false;
                        //    //MessageBox.Show(Common.GetMessage(validationMessage), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    MessageBox.Show("Select 1 kit item", "VestigeMLM POS",MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
                        //else
                        //{
                        //    toSave = true;
                        //}
                    }
                    else
                    {
                        toSave = true;
                    }
                    if (toSave)
                    {
                        if (SaveOrder((int)Common.OrderStatus.Created))
                        {
                            m_currentOrder.GetCOAllDetails(m_currentOrder.CustomerOrderNo, -1);
                            LoadOrder(m_currentOrder);
                            SetScreenState(Common.ScreenMode.OrderSaved);                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }
        private decimal GetCourierAmount()
        {
            decimal CourierAmt = -1;
            IEnumerable<COPayment> objPay = from n in m_currentOrder.COPaymentList
                                            where n.TenderType == 101
                                            select n;
            foreach (COPayment obj in objPay)
            {
                CourierAmt = Math.Round(obj.PaymentAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            }
            return CourierAmt;
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {                   
                if (m_currentOrder.RoundedChangeAmount < 0)
                {
                    MessageBox.Show(Common.GetMessage("VAL0507"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int status = (int)Common.OrderStatus.Confirmed;
                m_currentOrder.ChangeAmount = m_currentOrder.ChangeAmount;
                if (isModify)
                {
                    status = (int)Common.OrderStatus.Modify;
                    m_currentOrder.Status = (int)Common.OrderStatus.Modify;
                    m_currentOrder.ChangeAmount = m_currentOrder.ChangeAmount;
                }
                //if (m_currentOrder.Status == (int)Common.OrderStatus.Modify)

                if (SaveOrder(status))
                {
                    m_currentOrder.GetCOAllDetails(m_currentOrder.CustomerOrderNo, -1);
                    LoadOrder(m_currentOrder);
                    SetScreenState(Common.ScreenMode.OrderConfirmed);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }        

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            decimal pucAvailableAmt = 0;
            decimal requiredAmt = m_currentOrder.DBRoundedPaymentAmount - m_currentOrder.DBRoundedChangeAmount;
            requiredAmt = Math.Round(requiredAmt, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            string s = m_currentOrder.CheckPUCAmount(m_currentOrder.PCId, m_currentOrder.BOId, m_currentOrder.DBRoundedPaymentAmount, m_currentOrder.DBRoundedChangeAmount, ref pucAvailableAmt);
            if (s != string.Empty)
            {
                if (s.Contains("INF0145"))
                {
                    MessageBox.Show(Common.GetMessage(s, pucAvailableAmt.ToString(), requiredAmt.ToString()), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(Common.GetMessage(s), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (string.IsNullOrEmpty(m_currentOrder.CustomerOrderNo))
                return;
            frmInvoice invoice = new frmInvoice(m_currentOrder.CustomerOrderNo, string.Empty);
            if (invoice.ErrorCode.Equals(string.Empty))
            {
                DialogResult result = invoice.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //invoice.ErrorCode
                    if (invoice.ErrorCode.Trim().Equals(string.Empty) && (invoice.ReturnMessage.Trim().Length > 0))
                    {
                        lblInvoiceNoValue.Text = invoice.InvoiceNo.ToString();
                        m_currentOrder.GetCOAllDetails(m_currentOrder.CustomerOrderNo, -1);
                        LoadOrder(m_currentOrder);
                        SetScreenState(Common.ScreenMode.Invoiced);
                        MessageBox.Show(Common.GetMessage(invoice.ReturnMessage) + "Invoice No is " + invoice.InvoiceNo, Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage(invoice.ErrorCode), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show(Common.GetMessage(invoice.ErrorCode), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnInvoiceandPrint_Click(object sender, EventArgs e)
        {
            decimal pucAvailableAmt = 0;
            decimal requiredAmt = m_currentOrder.DBRoundedPaymentAmount - m_currentOrder.DBRoundedChangeAmount;
            requiredAmt = Math.Round(requiredAmt, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
            string s = m_currentOrder.CheckPUCAmount(m_currentOrder.PCId, m_currentOrder.BOId, m_currentOrder.DBRoundedPaymentAmount, m_currentOrder.DBRoundedChangeAmount, ref pucAvailableAmt);

            if (s != string.Empty)
            {
                if (s.Contains("INF0145"))
                {
                    MessageBox.Show(Common.GetMessage(s, pucAvailableAmt.ToString(), requiredAmt.ToString()), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(Common.GetMessage(s), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (string.IsNullOrEmpty(m_currentOrder.CustomerOrderNo))
                return;
            frmInvoice invoice = new frmInvoice(m_currentOrder.CustomerOrderNo, string.Empty);
            if (invoice.ErrorCode.Equals(string.Empty))
            {
                DialogResult result = invoice.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //invoice.ErrorCode
                    if (invoice.ErrorCode.Trim().Equals(string.Empty) && (invoice.ReturnMessage.Trim().Length > 0))
                    {
                        m_currentOrder.GetCOAllDetails(m_currentOrder.CustomerOrderNo, -1);
                        LoadOrder(m_currentOrder);
                        SetScreenState(Common.ScreenMode.Invoiced);
                        MessageBox.Show(Common.GetMessage(invoice.ReturnMessage) + "Invoice No is " + invoice.InvoiceNo, Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Add Print Code
                        DataSet dsReport = CreatePrintDataSet((int)Common.PrintType.PrintInvoice);
                        CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.CustomerInvoice, dsReport);
                        reportScreenObj.PrintReport();
                        dsReport = null;
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage(invoice.ErrorCode), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show(Common.GetMessage(invoice.ErrorCode), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMultiplier_Click(object sender, EventArgs e)
        {
            try
            {
                using (Multiplier mult = new Multiplier())
                {
                    mult.ShowDialog();
                    SetMultiplierIndex(mult.Quantity);
                    txtBarcode.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void btnClearServices_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_currentOrder != null)
                {
                    m_currentOrder.CODetailList = new List<CODetail>();
                    RebindGrid(dgvReceiptItems);
                    iItems.Reset();
                    txtBarcode.Focus();
                    RefreshOrderTotals();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void btnClearPayments_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_currentOrder.Status == (int)Common.OrderStatus.Created)
                {
                    m_currentOrder.COPaymentList = new List<COPayment>();
                    RebindGrid(dgvPayments);
                    //Bikram:AED
                    if (IsCourierRequiredOnOrder)
                    {
                        AddCourierPaymentToOrder();
                    }
                    RefreshOrderTotals();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void btnDistributorRegister_Click(object sender, EventArgs e)
        {
            try
            {
                using (DistributorRegistration dr = new DistributorRegistration())
                {
                    dr.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void iItems_ItemSelected(object sender, POSClient.UI.Controls.SelectableItem i)
        {
            try
            {
                if (m_currentOrder.OrderType == (int)Common.OrderType.KitOrder)
                {
                    if (m_currentOrder.CODetailList.Count < 1)
                    {

                        POSItem p = i.DataInstance as POSItem;
                        _isKitAdded = p.IsKit;
                        AdjustItem(p.Id, p.Code, 1);
                    }
                }
                else
                {
                    POSItem p = i.DataInstance as POSItem;
                    if (m_currentOrder.CODetailList.Find(pp => pp.IsKit == true) == null && p.IsKit)
                    {
                        _isKitAdded = p.IsKit;
                        AdjustItem(p.Id, p.Code, Convert.ToInt32(btnMultiplier.Tag.ToString()));
                    }
                    else if (p.IsKit == false)
                    {
                        _isKitAdded = p.IsKit;
                        AdjustItem(p.Id, p.Code, Convert.ToInt32(btnMultiplier.Tag.ToString()));
                    }

                    //else if (m_currentOrder.CODetailList.Find(pp => pp.IsKit == true) == null)
                    //{
                    //    _isKitAdded = p.IsKit;
                    //    AdjustItem(p.Id, p.Code, Convert.ToInt32(btnMultiplier.Tag.ToString()));
                    //}


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }
        /// <summary>
        /// Bikram: 
        /// </summary>
        /// <returns></returns>
        private bool PickupCenter()
        {
            DataRow[] rows = dtPOSLocations.Select("LocationId = " + Convert.ToInt32(((DataRowView)cmbStockpoint.SelectedItem)["LocationId"]));
            if (rows[0]["LocationType"].ToString() == Common.LocationConfigId.PC.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// Get Couriered Invoice detail
        /// </summary>
        private string GetCourieredInvoice(List<CO> lstCoProc)
        {
            CO currentOrder;
            CO m_currentOrder;
            string CourieredInvoice = string.Empty;
           
            for (int i = 0; i < lstCoProc.Count; i++)
            {
                currentOrder = new CO();
                currentOrder = lstCoProc[i];
                //m_currentOrder = new CO();
                //m_currentOrder.GetCOAllDetails(currentOrder.CustomerOrderNo, currentOrder.Status);

                IEnumerable<COPayment> objPay = from n in currentOrder.COPaymentList
                                                where n.TenderType == 101
                                                select n;
                foreach (COPayment obj in objPay)
                {
                    CourieredInvoice = obj.CustomerOrderNo;
                }
            }
            return CourieredInvoice;
        }
        private void iPayments_ItemSelected(object sender, POSClient.UI.Controls.SelectableItem i)
        {
            try
            {
                POSPayments payment = i.DataInstance as POSPayments;
                
                //Bikram:Ignore bank for Branch
                //If payment mode is "Bank" and location is not pick up center
                // then return
                //if ((payment.PaymentModeId == 12) && (!PickupCenter()))
                //{
                //    MessageBox.Show(Common.GetMessage("VAL0622"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //END:Ignore bank for Branch
/*
                //Bikram: AED CHANGES, 
                if ((m_currentOrder.OrderMode == 1) && (payment.PaymentMode == 101))
                    return;

                //Only one order should have courier charged
                if ((m_currentOrder.OrderMode == 2) && (payment.PaymentMode == 101) && !(((COLog)(cmbLogList.SelectedItem)).LogNo.Equals(string.Empty)))
                {
                    if (CorrectCourierAmount(payment))
                    {
                        string errorMessage = string.Empty;
                        CO currentOrder = new CO();
                        string strCourieredOrderNO = string.Empty;
                        currentOrder.LogNo = ((COLog)(cmbLogList.SelectedItem)).LogNo;
                        currentOrder.Status = (int)Common.OrderStatus.Confirmed;
                        errorMessage = string.Empty;
                        // Search All confirmed orders in this Log order
                        lstCourieredOrder = currentOrder.Search(ref errorMessage);
                        strCourieredOrderNO = GetCourieredInvoice(lstCourieredOrder);
                        if (!strCourieredOrderNO.Equals(string.Empty))
                        {
                            MessageBox.Show(Common.GetMessage("VAL0623", strCourieredOrderNO), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                //End:Only one order should have courier charged

                if ((m_currentOrder.RoundedChangeAmount < 0) && (payment.PaymentMode == 101))
                {
                    MessageBox.Show(Common.GetMessage("VAL0621"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
 */ 
                decimal TotalChangeValue = lblTotalChangeValue.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(lblTotalChangeValue.Text);
                if ((m_currentOrder.RoundedPaymentAmount < m_currentOrder.RoundedTotalAmount) || (TotalChangeValue < 0))
                {
                    switch (payment.PopupId)
                    {
                        case (int)Common.PopUp.CreditCard:
                            OpenCreditCardPopup(payment);
                            break;

                        case (int)Common.PopUp.EPS:
                            OpenEPSPopup(payment);
                            break;

                        case (int)Common.PopUp.LocalCurrency:
                            OpenLocalCurrencyPopup(payment);
                            break;

                        case (int)Common.PopUp.ForexConversion:
                            OpenForexConversionPopUp(payment);
                            break;

                        case (int)Common.PopUp.BonusCheque:
                            OpenBonusCheckPopUp(payment);
                            break;
                        default:
                            AddCashPaymentToOrder(payment);
                            break;
                    }
                    RefreshOrderTotals();
                    //// Skip if payment mode is courier
                    //if (payment.PaymentMode != 101)
                    //{
                    //    RefreshOrderTotals();
                    //}
                    //else
                    //{
                    /*
                        decimal Courier = GetCourierAmount();
                        if((Convert.ToInt32(lblCourierCharges.Text.ToString()))<=0)
                        {
                            lblCourierCharges.Text = (Courier + (lblCourierCharges.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(lblCourierCharges.Text))).ToString();
                        }
                        lblTotalChangeValue.Text = m_currentOrder.RoundedChangeAmount.ToString();
                        //40/50/15 Bikram: AED
                        decimal Change = Convert.ToDecimal(lblTotalChangeValue.Text);                        
                        if ((Change - Courier) <= 0)
                        {
                            //lblTotalPaymentValue.Text = ((Convert.ToDecimal(lblTotalPaymentValue.Text) - Change) + Courier).ToString();
                            lblTotalChangeValue.Text = "0";
                        }
                        else
                        {
                            //lblTotalPaymentValue.Text = ((Convert.ToDecimal(lblTotalPaymentValue.Text)) + Courier).ToString();
                            lblTotalChangeValue.Text = lblCourierCharges.Text == "" ? m_currentOrder.RoundedChangeAmount.ToString() : (m_currentOrder.RoundedChangeAmount - Convert.ToDecimal(Courier)).ToString();                                        
                        }
                     * */
                    //}
                    RebindGrid(dgvPayments);
                    iPayments.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }
        /// <summary>
        /// Bikra: AED - Add Courier payment to order
        /// </summary>
        private void AddCourierPaymentToOrder()
        {
            decimal Courier = GetCourierAmount();            
            if ((Courier <= 0) && (IsCourierRequired))
            {
                decimal RequiredCourier = GetLocationCourierAmount();
                if (RequiredCourier > 0)
                {
                    COPayment newPayment = new COPayment();
                    newPayment.PaymentAmount = RequiredCourier;
                    newPayment.ReceiptDisplay = "Courier";
                    newPayment.CurrencyCode = "AED";
                    newPayment.TenderType = 101;
                    newPayment.PaymentModeId = 101;
                    m_currentOrder.COPaymentList.Add(newPayment);
                }
            }           
        }
        /// <summary>
        /// Get Location specific Courier Amount
        /// </summary>
        /// <returns></returns>
        private decimal GetLocationCourierAmount()
        {
            decimal LocationCourierAmount = 0;
            DataTable dtLocationCourierAmount = Common.ParameterLookup(Common.ParameterType.CourierAmount, new ParameterFilter("CourierAmount", m_currentOrder.DeliverToStateId, 0, 0));
            if (dtLocationCourierAmount.Rows.Count > 0)
            {
                LocationCourierAmount = decimal.TryParse(dtLocationCourierAmount.Rows[0]["KeyValue1"].ToString(), out LocationCourierAmount) ? LocationCourierAmount : 0;
            }
            return LocationCourierAmount;
        }
        
        private void txtDistributorNo_Validated(object sender, EventArgs e)
        {
            try
            {
                string dbMessage = string.Empty;
                bool prevDeliveryType = rdoByHand.Checked;
                if (txtDistributorNo.Text.Trim() != string.Empty && string.IsNullOrEmpty(txtDistributorName.Text))
                {
                    int outDisNumber = 0;
                    if (int.TryParse(txtDistributorNo.Text.Trim(), out outDisNumber))
                    {
                        Distributor upline = new Distributor();
                        upline.SDistributorId = txtDistributorNo.Text.Trim();
                        upline.DistributorId = Convert.ToInt32(txtDistributorNo.Text.Trim());
                        upline.forSkinCareItem = Common.ForSkinCareItem;
                        string errorMessage = string.Empty;
                        List<Distributor> dist = upline.SearchDistributor(ref errorMessage);
                        if (dist == null)
                        {
                            MessageBox.Show(Common.GetMessage("40018"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (dist.Count == 0)
                        {
                            MessageBox.Show(Common.GetMessage("40018"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (dist.Count > 1)
                        {
                            using (DistributorPopup dp = new DistributorPopup(dist))
                            {
                                Point pointTree = new Point();
                                pointTree = pnlDistDetails.PointToScreen(new Point(61, 18));
                                pointTree.Y = pointTree.Y + 25;
                                pointTree.X = pointTree.X + 5;
                                dp.Location = pointTree;
                                if (dp.ShowDialog() == DialogResult.OK)
                                {
                                    txtDistributorNo.Text = dp.SelectedDistributor.DistributorId.ToString();
                                    txtDistributorName.Text = dp.SelectedDistributor.DistributorFirstName.Trim() + " " + dp.SelectedDistributor.DistributorLastName.Trim();
                                    m_currentOrder.DistributorAddress = rtxtDistributorAddress.Text = dp.SelectedDistributor.DistributorAddress;
                                    m_currentOrder.DistributorId = dp.SelectedDistributor.DistributorId;
                                    m_currentOrder.DistributorName = txtDistributorName.Text.Trim();
                                    m_currentOrder.IsFirstOrderForDistributor = !dp.SelectedDistributor.FirstOrderTaken;
                                    if (m_currentOrder.OrderType != (int)Common.OrderType.KitOrder)
                                        m_currentOrder.OrderType = m_currentOrder.IsFirstOrderForDistributor ? (int)Common.OrderType.FirstOrder : (int)Common.OrderType.Reorder;


                                    if ((int)Common.OrderType.KitOrder == m_currentOrder.OrderType)
                                    {
                                        iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, true, 2, ref dbMessage));
                                    }
                                    else if (m_currentOrder.IsFirstOrderForDistributor == false)
                                    {

                                        // iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, false, false, ref dbMessage), true);
                                        iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, false, 0, ref dbMessage));
                                    }
                                    else if (m_currentOrder.IsFirstOrderForDistributor == true)
                                    {
                                        iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, false, 1, ref dbMessage));
                                        //  iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, false, true, ref dbMessage), true);
                                    }


                                    m_currentOrder.MinimumOrderAmount = m_currentOrder.IsFirstOrderForDistributor == true ? dp.SelectedDistributor.MinFirstPurchaseAmount : 0;
                                    m_currentOrder.BOId = Common.BOId;
                                    m_currentOrder.PCId = Common.PCId;
                                    m_currentOrder.DeliverFromAddress = Common.DeliverFromAddress;
                                    m_currentOrder.DeliverToAddress = Common.DeliverToAddress;
                                    m_currentOrder.TerminalCode = Common.TerminalCode;
                                    SetScreenState(Common.ScreenMode.DistributorAdded);
                                    txtBarcode.Focus();
                                    if ((!dist[0].FirstOrderTaken) && (m_currentOrder != null) && (m_currentOrder.OrderType != (int)Common.OrderType.KitOrder))
                                        MessageBox.Show(Common.GetMessage("INF0225"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else if (dist[0].AllInvoiceAmountSum < dist[0].MinimumSaleAmount)
                                        MessageBox.Show(Common.GetMessage("VAL0617", dist[0].AllInvoiceAmountSum.ToString(), dist[0].MinimumSaleAmount.ToString()), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    txtDistributorNo.Text = "";
                                    txtDistributorNo.Focus();
                                }
                            }
                        }
                        else
                        {
                            txtDistributorNo.Text = dist[0].DistributorId.ToString();
                            txtDistributorName.Text = dist[0].DistributorTitle.Trim() + " " + dist[0].DistributorFirstName.Trim() + " " + dist[0].DistributorLastName.Trim();
                            m_currentOrder.DistributorAddress = dist[0].DistributorAddress;

                            if ((Common.IsMiniBranchLocation != 1) && (!Common.CheckIfDistributorAddHidden(dist[0].DistributorId)))
                            {
                                rtxtDistributorAddress.Text = dist[0].DistributorAddress;
                            }

                            m_currentOrder.DistributorId = dist[0].DistributorId;
                            m_currentOrder.DistributorName = txtDistributorName.Text.Trim();
                            m_currentOrder.IsFirstOrderForDistributor = !dist[0].FirstOrderTaken;
                            if (m_currentOrder.OrderType != (int)Common.OrderType.KitOrder)
                                m_currentOrder.OrderType = m_currentOrder.IsFirstOrderForDistributor ? (int)Common.OrderType.FirstOrder : (int)Common.OrderType.Reorder;

                            if ((int)Common.OrderType.KitOrder == m_currentOrder.OrderType)
                            {
                                iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, true, 2, ref dbMessage));
                            }
                            else if (m_currentOrder.IsFirstOrderForDistributor == false)
                            {

                                // iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, false, false, ref dbMessage), true);
                                iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, false, 0, ref dbMessage));
                            }
                            else if (m_currentOrder.IsFirstOrderForDistributor == true)
                            {
                                iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, false, 1, ref dbMessage));
                                //  iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, false, true, ref dbMessage), true);
                            }

                            m_currentOrder.MinimumOrderAmount = m_currentOrder.IsFirstOrderForDistributor == true ? dist[0].MinFirstPurchaseAmount : 0;
                            SetScreenState(Common.ScreenMode.DistributorAdded);
                            m_currentOrder.BOId = Common.BOId;
                            m_currentOrder.PCId = Common.PCId;
                            m_currentOrder.DeliverFromAddress = Common.DeliverFromAddress;
                            m_currentOrder.DeliverToAddress = Common.DeliverToAddress;
                            m_currentOrder.TerminalCode = Common.TerminalCode;
                            if ((!dist[0].FirstOrderTaken) && (m_currentOrder != null) && (m_currentOrder.OrderType != (int)Common.OrderType.KitOrder))
                            {
                                MessageBox.Show(Common.GetMessage("INF0225"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtBarcode.Focus();
                            }
                            else if ((m_currentOrder.OrderType == (int)Common.OrderType.FirstOrder || m_currentOrder.OrderType == (int)Common.OrderType.Reorder) && (dist[0].AllInvoiceAmountSum < dist[0].MinimumSaleAmount))
                            {
                                MessageBox.Show(Common.GetMessage("VAL0617", dist[0].AllInvoiceAmountSum.ToString(), dist[0].MinimumSaleAmount.ToString()), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtBarcode.Focus();
                            }

                        }
                    }
                }
                rdoByHand.Checked = prevDeliveryType;
                rdoCourier.Checked = !prevDeliveryType;

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void mnuItemAdd1_Click(object sender, EventArgs e)
        {
            try
            {
                List<CODetail> cod = m_currentOrder.CODetailList.FindAll(delegate(CODetail od) { return od.ItemId == Convert.ToInt32(dgvReceiptItems.SelectedRows[0].Cells[0].Value); });
                AdjustItem(cod[0].ItemId, cod[0].ItemCode, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void mnuItemRemove1_Click(object sender, EventArgs e)
        {
            try
            {
                List<CODetail> cod = m_currentOrder.CODetailList.FindAll(delegate(CODetail od) { return od.ItemId == Convert.ToInt32(dgvReceiptItems.SelectedRows[0].Cells[0].Value); });
                AdjustItem(cod[0].ItemId, cod[0].ItemCode, -1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void mnuItemRemoveAll_Click(object sender, EventArgs e)
        {
            try
            {
                List<CODetail> cod = m_currentOrder.CODetailList.FindAll(delegate(CODetail od) { return od.ItemId == Convert.ToInt32(dgvReceiptItems.SelectedRows[0].Cells[0].Value); });
                AdjustItem(cod[0].ItemId, cod[0].ItemCode, -cod[0].Qty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void mnuItemCancel_Click(object sender, EventArgs e)
        {
            //DO NOTHING
        }

        private void cmbStockpoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                SetDeliveryFromAddress();
                SetDeliveryToAddress();
                //if ((m_currentOrder != null) && (m_currentOrder.CustomerOrderNo == string.Empty))
                {
                    if (Common.CurrentLocationTypeId == (int)Common.LocationConfigId.BO && cmbStockpoint.SelectedIndex == 0)
                        rdoByHand.Checked = true;
                    else
                        rdoCourier.Checked = true;
                }


                //DataTable dtLocationFrom = Common.ParameterLookup(Common.ParameterType.LocationAddress, new ParameterFilter("", Common.BOId, -1, -1));
                //DataTable dtLocationTo = Common.ParameterLookup(Common.ParameterType.LocationAddress, new ParameterFilter("", Common.PCId, -1, -1));

                //CreateAddress(Common.DeliverFromAddress = new Address(), dtLocationFrom);
                //CreateAddress(Common.DeliverToAddress = new Address(), dtLocationTo);

                CheckLogAndTeamOrderValue();

                //if (dtLocationFrom.Rows[0][0].ToString() != dtLocationTo.Rows[0][0].ToString())
                //{
                //    rdoCourier.Checked = true;
                //    rdoByHand.Checked = false;
                //}


            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }


        private void SetDeliveryToAddress()
        {

            if (m_currentOrder != null && m_currentOrder.CustomerOrderNo == string.Empty && m_currentOrder.Status == (int)Common.OrderStatus.Created)
            {
                if ((m_currentOrder == null) || (m_currentOrder.CustomerOrderNo == string.Empty))
                {
                    DataTable dtLocationTo = null;//= Common.ParameterLookup(Common.ParameterType.LocationAddress, new ParameterFilter("", Common.PCId, -1, -1));
                    if (rdoByHand.Checked == true)
                    {
                        dtLocationTo = Common.ParameterLookup(Common.ParameterType.LocationAddress, new ParameterFilter("", Common.BOId, -1, -1));
                        CreateAddress(Common.DeliverToAddress = new Address(), dtLocationTo);
                    }
                    else if (rdoCourier.Checked == true)
                    {
                        //Bikram: Courier AED Change
                        if ((Common.BOId == Common.PCId)&&(cmbLogList.SelectedIndex==0))
                        {
                            //Common.DeliverToAddress = Common.DeliverFromAddress;
                            dtLocationTo = Common.ParameterLookup(Common.ParameterType.Distributor, new ParameterFilter(string.Empty, m_currentOrder.DistributorId, 0, 0));
                            Common.DeliverToAddress = Address.CreateAddressObject(dtLocationTo.Rows[0]);
                        }
                        else if ((Common.BOId == Common.PCId) && (cmbLogList.SelectedIndex > 0))
                        {
                            btnGetAddress.Enabled = false;
                            dtLocationTo = Common.ParameterLookup(Common.ParameterType.Distributor, new ParameterFilter(string.Empty, ((POSClient.BusinessObjects.COLog)(cmbLogList.SelectedItem)).DistributorId, 0, 0));
                            Common.DeliverToAddress = Address.CreateAddressObject(dtLocationTo.Rows[0]);
                        }
                        else
                        {
                            dtLocationTo = Common.ParameterLookup(Common.ParameterType.LocationAddress, new ParameterFilter("", Common.PCId, -1, -1));

                            // City Check
                            //if (m_currentOrder.DeliverToAddressLine1 != dtLocationTo.Rows[0]["Address1"].ToString())
                            CreateAddress(Common.DeliverToAddress = new Address(), dtLocationTo);
                        }
                    }
                }
                else
                {
                    Common.DeliverToAddress = m_currentOrder.DeliverToAddress;
                }
            }

            if (m_currentOrder != null && m_currentOrder.CustomerOrderNo == string.Empty && m_currentOrder.Status == (int)Common.OrderStatus.Created)
            {
                //m_currentOrder.BOId = Common.BOId;
                //m_currentOrder.PCId = Common.PCId;
                //m_currentOrder.DeliverFromAddress = Common.DeliverFromAddress;

                m_currentOrder.DeliverToAddress = Common.DeliverToAddress;
                //m_currentOrder.TerminalCode = Common.TerminalCode;
            }
            //rtxtStockPointAddress.Text = Common.DeliverFromAddress.GetAddress();
            if (Common.DeliverToAddress != null)
            {
                //if ((Common.DeliverToAddress.DistributorName == null || Common.DeliverToAddress.DistributorName.Trim().Length == 0) && !rdoCourier.Checked)
                rtxtDeliveryAddress.Text = Common.DeliverToAddress.GetAddress();
                //else
                //    rtxtDeliveryAddress.Text = Common.DeliverToAddress.DistributorName.Trim().Length > 0 ? Common.DeliverToAddress.DistributorName.Trim() + '\n' + Common.DeliverToAddress.GetAddress() : Common.DeliverToAddress.GetAddress();
            }
        }

        private string GetDistributorName()
        {
            string sName = "";
            DataTable dtLocationFrom = Common.ParameterLookup(Common.ParameterType.DistributorName, new ParameterFilter("", Common.PCId, -1, -1));
            if (dtLocationFrom != null && dtLocationFrom.Rows.Count > 0)
                sName = dtLocationFrom.Rows[0]["DistributorName"].ToString();
            return sName;
        }

        private void SetDeliveryFromAddress()
        {
            if (m_currentOrder != null && m_currentOrder.CustomerOrderNo == string.Empty && m_currentOrder.Status == (int)Common.OrderStatus.Created)
            {
                DataRow[] rows = dtPOSLocations.Select("LocationId = " + Convert.ToInt32(((DataRowView)cmbStockpoint.SelectedItem)["LocationId"]));
                Common.BOId = rows[0]["LocationType"].ToString() == Common.LocationConfigId.BO.ToString() ? Convert.ToInt32(rows[0]["LocationId"]) : Convert.ToInt32(rows[0]["ReplenishmentId"]);
                //Common.BOId = Convert.ToInt32(rows[0]["ReplenishmentId"]) == -1 ? Convert.ToInt32(rows[0]["LocationId"]) : Convert.ToInt32(rows[0]["ReplenishmentId"]);
                Common.PCId = Convert.ToInt32(rows[0]["LocationId"]);

                DataTable dtLocationFrom = Common.ParameterLookup(Common.ParameterType.LocationAddress, new ParameterFilter("", Common.BOId, -1, -1));


                CreateAddress(Common.DeliverFromAddress = new Address(), dtLocationFrom);



                if (m_currentOrder != null)
                {
                    m_currentOrder.BOId = Common.BOId;
                    m_currentOrder.PCId = Common.PCId;
                    m_currentOrder.DeliverFromAddress = Common.DeliverFromAddress;

                    //m_currentOrder.DeliverToAddress = Common.DeliverToAddress;
                    m_currentOrder.TerminalCode = Common.TerminalCode;
                }
                rtxtStockPointAddress.Text = Common.DeliverFromAddress.GetAddress();
                //if (Common.DeliverToAddress != null)
                //rtxtDeliveryAddress.Text = Common.DeliverToAddress.GetAddress();

            }
        }

        private void btnGiftVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtDistributorName.Text.Trim().Equals(string.Empty))
                {
                    //m_currentOrder.TotalAmount
                    frmGiftVoucher voucher = new frmGiftVoucher(Convert.ToInt32(txtDistributorNo.Text.Trim()));
                    DialogResult result = voucher.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        decimal minAmount = voucher.MinBuyAmount;
                        if (minAmount > m_currentOrder.TotalAmount)
                        {
                            MessageBox.Show(Common.GetMessage("40028", minAmount.ToString("0.00")), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        Price price = voucher.Price;
                        //Check Gift Voucher Item Exists
                        foreach (CODetail detail in m_currentOrder.CODetailList)
                        {
                            if (detail.VoucherSrNo == price.VoucherSrNo)
                            {
                                //Message
                                MessageBox.Show(Common.GetMessage("40032", price.GiftVoucherNumber), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        CreateOrderDetail(price, false);
                        RebindGrid(dgvReceiptItems);
                        RefreshOrderTotals();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        void SelectByHand()
        {
            btnGetAddress.Enabled = false;
            cmbStockpoint.Enabled = IsStockPointAvailable;
            SetDeliveryToAddress();

            if (m_currentOrder != null && m_currentOrder.Status == (int)Common.OrderStatus.Created && m_currentOrder.CustomerOrderNo == string.Empty)
            {
                //m_currentOrder.DeliverToAddress = Common.DeliverToAddress;
                m_currentOrder.OrderMode = (int)Common.DeliveryMode.Self;
            }

            //if (m_currentOrder != null)
            //{
            //rtxtDeliveryAddress.Text = Common.DeliverFromAddress.GetAddress();
            //m_currentOrder.DeliverFromAddress = Common.DeliverFromAddress;
            //m_currentOrder.DeliverToAddress = Common.DeliverFromAddress;
            //}
        }
        void SelectByCourier()
        {
            btnGetAddress.Enabled = true;
            //cmbStockpoint.Enabled = false;
            SetDeliveryToAddress();
            if (m_currentOrder != null && m_currentOrder.Status == (int)Common.OrderStatus.Created && m_currentOrder.CustomerOrderNo == string.Empty)
                m_currentOrder.OrderMode = (int)Common.DeliveryMode.Courier;

            //if (m_currentOrder != null && m_currentOrder.Status == (int)Common.OrderStatus.Created && m_currentOrder.CustomerOrderNo == string.Empty)
            //{
            //rtxtDeliveryAddress.Text = Common.DeliverToAddress.GetAddress();
            //m_currentOrder.DeliverFromAddress = Common.DeliverFromAddress;
            //m_currentOrder.DeliverToAddress = Common.DeliverToAddress;
            //}
        }
        private void rdoByHand_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoByHand.Checked)
            {
                SelectByHand();
            }
        }

        private void rdoCourier_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCourier.Checked)
            {
                SelectByCourier();
                
            }
        }

        private void btnGetAddress_Click(object sender, EventArgs e)
        {
            try
            {
                DeliveryAdress address = new DeliveryAdress();
                if (txtDistributorNo.Text.ToString().Length > 0)
                {
                    address.DistributorId = Convert.ToInt32(txtDistributorNo.Text);
                    DialogResult result = address.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Address returnAddress = address.ReturnObject;
                        Common.DeliverToAddress = returnAddress;
                        rtxtDeliveryAddress.Text = returnAddress.GetAddress();
                        //rtxtDeliveryAddress.Text = returnAddress.DistributorName.Trim() + returnAddress.GetAddress();
                        m_currentOrder.DeliverToAddress = Common.DeliverToAddress;
                    }
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("VAL0001", "Distributor No"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void lblDistributorNo_Click(object sender, EventArgs e)
        {

        }

        void CheckLogAndTeamOrderValue()
        {
            int logType = 0;
            if (rdoTeamOrder.Checked)
            {
                logType = (int)Common.COLogType.TeamOrder;
            }
            else if (rdoLog.Checked)
            {
                logType = (int)Common.COLogType.Log;
            }
            BindLogList(logType, null);
        }

        private void rdoTeamOrder_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckLogAndTeamOrderValue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void BindLogList(int logType, string LogNo)
        {
            try
            {
                List<COLog> LogList = new List<COLog>();
                if (string.IsNullOrEmpty(LogNo))
                {
                    COLog log = new COLog();

                    if (rdoTeamOrder.Checked)
                    {
                        LogList = log.Search(1, Common.CurrentLocationId, string.Empty, logType, Common.INT_DBNULL, Common.INT_DBNULL);
                    }
                    else if (rdoLog.Checked)
                    {
                        if ("System.Data.DataRowView" != cmbStockpoint.SelectedValue.ToString())
                            LogList = log.Search(1, Common.CurrentLocationId, string.Empty, logType, Common.INT_DBNULL, Convert.ToInt32(cmbStockpoint.SelectedValue));
                    }

                    if (LogList != null)
                    {
                        COLog logSearch = new COLog();
                        logSearch.LogNo = "Select Log";
                        LogList.Insert(0, logSearch);
                    }
                }
                else
                {
                    COLog logSearch = new COLog();
                    logSearch = new COLog();
                    logSearch.LogNo = LogNo;
                    LogList.Insert(0, logSearch);



                }
                cmbLogList.DataSource = LogList;
                cmbLogList.DisplayMember = "LogNo";
                cmbLogList.ValueMember = "LogValue";
            }
            catch (Exception ex)
            {
                LogManager.WriteExceptionLog(ex);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                isModify = true;

                m_existingOrder = new CO();
                m_existingOrder.GetCOAllDetails(m_currentOrder.CustomerOrderNo, -1);
                m_currentOrder.Status = (int)Common.OrderStatus.Created;
                LoadOrder(m_currentOrder);
                SetScreenState(Common.ScreenMode.OrderModify);
                btnModify.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }

        #endregion

        #region Private Methods

        private void InitializeRights()
        {

            if (AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser != null)
            {
                m_UserName = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;
            }
            m_LocationCode = Common.LocationCode;
            string CON_MODULENAME = Common.MODULE_ORDERFORM;
            if (!string.IsNullOrEmpty(m_UserName) && !string.IsNullOrEmpty(CON_MODULENAME))
            {
                IsNewOrderAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_NEWORDER);
                IsTeamOrderAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_TEAMORDER);
                IsRegisterDistributorAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_REG_DISTRIBUTOR);
                IsKitOrderAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_KITORDER);
                IsCancelAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_CANCEL);
                IsPreviewAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_PREVIEW);
                IsConfirmAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_CONFIRM);
                IsInvoiceAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_INVOICE);
                IsPrintInvoiceAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_PRINTINVOICE);
                IsPrintOrderAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_PRINTORDER);
                IsLogAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_LOG);
                IsStockPointAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_STOCKPOINT);
                IsChangeDeliveryModeAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_CHANGEDELIVERYMODE);
                IsModifyAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(m_UserName, m_LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_MODIFYORDER);
            }
        }

        private void AdjustItem(int id, string code, decimal addQty)
        {
            if (isModify)
            {
                foreach (CODetail c in m_currentOrder.CODetailList)
                {
                    c.CODetailTaxList = new List<CODetailTax>();
                    c.TaxAmount = 0;
                    c.Amount = c.DP * c.Qty - c.Discount;
                    c.UnitPrice = (c.DP * c.Qty - c.Discount) / c.Qty;
                }
                m_currentOrder.TaxAmount = 0;
                //m_currentOrder.OrderAmount = m_currentOrder.TotalAmount;
            }

            bool isSuccess = false;
            CODetail existingDetail = m_currentOrder.CODetailList.Find(delegate(CODetail d) { return d.ItemId == id; });
            //if (existingDetail.Qty + addQty > 0)
            //{
            Price newPrice = null;
            if (existingDetail != null)
            {
                newPrice = PromotionEngine.GetPrice(code, id, existingDetail.Qty + addQty, Common.LocationCode, m_currentOrder.IsFirstOrderForDistributor);
            }
            else
            {
                newPrice = PromotionEngine.GetPrice(code, id, addQty, Common.LocationCode, m_currentOrder.IsFirstOrderForDistributor);
            }
            if (newPrice != null)
            {
                if (existingDetail == null)
                {
                    CreateOrderDetail(newPrice, _isKitAdded);
                    isSuccess = true;
                }
                else
                {
                    if (existingDetail.Qty + addQty > 0)
                    {
                        existingDetail.Qty = newPrice.Quantity;
                        existingDetail.Amount = newPrice.DiscountedPrice;
                        //detail.Discount = newPrice.DiscountValue;
                        existingDetail.CODiscountList.Clear();
                        if (newPrice.PromotionId != -1 || !string.IsNullOrEmpty(newPrice.GiftVoucherNumber))
                        {
                            CODetailDiscount cdd = new CODetailDiscount();
                            cdd.DiscountPercent = newPrice.DiscountPercent;
                            cdd.DiscountAmount = newPrice.DiscountValue;
                            cdd.PromotionId = newPrice.PromotionId;
                            existingDetail.CODiscountList.Add(cdd);
                        }
                        isSuccess = true;
                    }
                    else
                    {
                        m_currentOrder.CODetailList.Remove(existingDetail);
                        isSuccess = true;
                    }
                }
            }
            else
            {
                MessageBox.Show(Common.GetMessage("40014"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (isSuccess)
            {
                RebindGrid(dgvReceiptItems);
                RefreshOrderTotals();
            }
            SetMultiplierIndex(1);
        }

        private void CreateAddress(Address address, DataTable dtLocation)
        {
            address.Address1 = dtLocation.Rows[0]["Address1"].ToString();
            address.Address2 = dtLocation.Rows[0]["Address2"].ToString();
            address.Address3 = dtLocation.Rows[0]["Address3"].ToString();
            address.Address4 = dtLocation.Rows[0]["Address4"].ToString();
            address.CityId = Convert.ToInt32(dtLocation.Rows[0]["CityId"]);
            address.City = dtLocation.Rows[0]["CityName"].ToString();
            address.StateId = Convert.ToInt32(dtLocation.Rows[0]["StateId"]);
            address.State = dtLocation.Rows[0]["StateName"].ToString();
            address.CountryId = Convert.ToInt32(dtLocation.Rows[0]["CountryId"]);
            address.Country = dtLocation.Rows[0]["CountryName"].ToString();
            address.PinCode = dtLocation.Rows[0]["PinCode"].ToString();
            address.Mobile1 = dtLocation.Rows[0]["Mobile1"].ToString();
            address.PhoneNumber1 = dtLocation.Rows[0]["Phone1"].ToString();
            address.Email1 = dtLocation.Rows[0]["EmailId1"].ToString();
            address.Website = dtLocation.Rows[0]["Website"].ToString();
            address.Mobile2 = dtLocation.Rows[0]["Mobile2"].ToString();
            address.PhoneNumber2 = dtLocation.Rows[0]["Phone2"].ToString();
            address.Fax1 = dtLocation.Rows[0]["Fax1"].ToString();
            address.Fax2 = dtLocation.Rows[0]["Fax2"].ToString();
            address.DistributorName = dtLocation.Rows[0]["DistributorName"].ToString();
            if (dtLocation.Columns.Contains("DistributorName"))
                address.DistributorName = dtLocation.Rows[0]["DistributorName"].ToString();
            //m_sDistName = dtLocation.Rows[0]["DistributorName"].ToString();
        }

        private void RefreshOrderTotals()
        {
            lblProductAmountValue.Text = m_currentOrder.RoundedOrderAmount.ToString();
            lblTaxAmountValue.Text = m_currentOrder.RoundedTaxAmount.ToString();
            lblTotalAmountValue.Text = m_currentOrder.RoundedTotalAmount.ToString();

            //lblProductAmountValue.Text = m_currentOrder.RoundedOrderAmount.ToString();
            lblTotalPaymentValue.Text = m_currentOrder.RoundedPaymentAmount.ToString();
            lblTotalChangeValue.Text = m_currentOrder.RoundedChangeAmount.ToString();
            lblQtyValue.Text = m_currentOrder.RoundedTotalUnits.ToString();
            lblPVValue.Text = m_currentOrder.RoundedTotalPV.ToString();
            lblBVValue.Text = m_currentOrder.RoundedTotalBV.ToString(); 
            //Bikram: AED Changes
            decimal Change = Convert.ToDecimal(lblTotalChangeValue.Text);
            decimal Courier = GetCourierAmount();
            if (Courier == -1)
            {
                lblCourierCharges.Text = Convert.ToString(0); ;
                return;
            }
            lblTotalAmountValue.Text = (m_currentOrder.RoundedTotalAmount + Courier).ToString();
            lblTotalPaymentValue.Text = (m_currentOrder.RoundedPaymentAmount + Courier).ToString();

            if ((Change - Courier) <= 0)
            {
                //lblTotalPaymentValue.Text = ((Convert.ToDecimal(lblTotalPaymentValue.Text) - Change) + Courier).ToString();
                //lblTotalChangeValue.Text = "0";
            }
            else
            {
                //lblTotalPaymentValue.Text = ((Convert.ToDecimal(lblTotalPaymentValue.Text)) + Courier).ToString();
                //lblTotalChangeValue.Text = lblCourierCharges.Text == "" ? m_currentOrder.RoundedChangeAmount.ToString() : (m_currentOrder.RoundedChangeAmount - Convert.ToDecimal(Courier)).ToString();
            }
        }

        private void CreateOrderDetail(Price newPrice, bool isKit)
        {
            CODetail detail = new CODetail();
            detail.ItemCode = newPrice.ItemCode;
            detail.ItemId = newPrice.ItemId;
            detail.ItemName = newPrice.ItemName;
            detail.ItemPrintName = newPrice.PrintName;
            detail.ItemReceiptName = newPrice.ReceiptName;
            detail.ItemShortName = newPrice.ShortName;
            detail.ItemDisplayName = newPrice.DisplayName;
            detail.MRP = newPrice.MRP;
            detail.BV = newPrice.BusinessVolume;
            detail.PV = newPrice.PointValue;
            detail.Qty = newPrice.Quantity;
            //detail.Discount = newPrice.DiscountValue;
            detail.DP = newPrice.DistributorPrice;
            detail.Amount = newPrice.DiscountedPrice;
            detail.GiftVoucherNumber = newPrice.GiftVoucherNumber;
            detail.VoucherSrNo = newPrice.VoucherSrNo;
            detail.UnitPrice = detail.Amount / detail.Qty;
            detail.IsKit = isKit;
            if (newPrice.PromotionId != -1 || !string.IsNullOrEmpty(newPrice.GiftVoucherNumber))
            {
                CODetailDiscount cdd = new CODetailDiscount();
                cdd.DiscountPercent = newPrice.DiscountPercent;
                cdd.DiscountAmount = newPrice.DiscountValue;
                cdd.PromotionId = newPrice.PromotionId;
                detail.CODiscountList.Add(cdd);
            }
            m_currentOrder.CODetailList.Add(detail);
        }

        private void OpenCreditCardPopup(POSPayments payment)
        {
            CreditCard popup = new CreditCard(-m_currentOrder.RoundedChangeAmount, string.Empty);
            if (popup.ShowDialog() == DialogResult.OK)
            {
                popup.Close();
                COPayment orderPayment = m_currentOrder.COPaymentList.Find(delegate(COPayment p) { return p.CreditCardNumber != string.Empty && p.CreditCardNumber == popup.CardNumber; });
                if (orderPayment == null)
                {
                    COPayment newOrderPayment = new COPayment();
                    //Calculate Total Payment
                    //m_currentOrder.PaymentAmount = m_currentOrder.PaymentAmount + popup.Amount;
                    newOrderPayment.PaymentAmount = popup.Amount;
                    newOrderPayment.ReceiptDisplay = payment.ReceiptDisplay;
                    newOrderPayment.CardExpiryDate = popup.ExpiryDate;
                    newOrderPayment.CardHolderName = popup.CardHolderName;
                    newOrderPayment.CardType = (int)Enum.Parse(typeof(Common.CreditCardType), popup.CardType);
                    newOrderPayment.CreditCardNumber = popup.CardNumber;
                    newOrderPayment.TenderType = payment.PaymentMode;
                    newOrderPayment.PaymentModeId = payment.PaymentModeId;
                    m_currentOrder.COPaymentList.Add(newOrderPayment);
                }
                else
                {
                    orderPayment.CardExpiryDate = popup.ExpiryDate;
                    orderPayment.CardHolderName = popup.CardHolderName;
                    //Calculate Total Payment
                    //m_currentOrder.PaymentAmount = m_currentOrder.PaymentAmount + popup.Amount - orderPayment.PaymentAmount;
                    orderPayment.PaymentAmount = popup.Amount;
                    orderPayment.PaymentModeId = payment.PaymentModeId;
                }
            }
            else
            {
                popup.Close();
            }
        }

        private void OpenEPSPopup(POSPayments payment)
        {
            if (m_currentOrder.RoundedChangeAmount < 0)
            {
                CurrencyCalculator calc = new CurrencyCalculator(Currency.BaseCurrency.CurrencyCode, Currency.BaseCurrency.CurrencyCode, -m_currentOrder.RoundedChangeAmount, 0, 1, 1);
                if (calc.ShowDialog() == DialogResult.OK)
                {
                    COPayment oldPayment = m_currentOrder.COPaymentList.Find(delegate(COPayment p) { return p.CurrencyCode == string.Empty && p.TenderType == payment.PaymentModeId; });
                    if (oldPayment == null)
                    {
                        COPayment newPayment = new COPayment();
                        newPayment.ReceiptDisplay = payment.ReceiptDisplay;
                        newPayment.PaymentAmount = calc.CurrencyToAmount;
                        newPayment.CurrencyCode = "";
                        newPayment.TenderType = payment.PaymentMode;
                        newPayment.PaymentModeId = payment.PaymentModeId;
                        //Calculate Total Payment
                        // m_currentOrder.PaymentAmount = m_currentOrder.PaymentAmount + newPayment.PaymentAmount;
                        m_currentOrder.COPaymentList.Add(newPayment);
                    }
                    else
                    {
                        //Calculate Total Payment
                        //  m_currentOrder.PaymentAmount = m_currentOrder.PaymentAmount + calc.CurrencyToAmount;
                        oldPayment.PaymentAmount = oldPayment.PaymentAmount + calc.CurrencyToAmount;
                        oldPayment.PaymentModeId = payment.PaymentModeId;
                    }
                }
            }
            iPayments.ClearSelection();
        }

        private void OpenLocalCurrencyPopup(POSPayments payment)
        {
            if ((m_currentOrder.RoundedChangeAmount < 0))
            {
                CurrencyCalculator calc = new CurrencyCalculator(Currency.BaseCurrency.CurrencyCode, Currency.BaseCurrency.CurrencyCode, -m_currentOrder.RoundedChangeAmount, 0, 1, 1);
                if (calc.ShowDialog() == DialogResult.OK)
                {
                    COPayment oldPayment = m_currentOrder.COPaymentList.Find(delegate(COPayment p) { return p.CurrencyCode == Currency.BaseCurrency.CurrencyCode && p.TenderType == payment.ParentId; });
                    if (oldPayment == null)
                    {
                        COPayment newPayment = new COPayment();
                        newPayment.ReceiptDisplay = payment.ReceiptDisplay;
                        newPayment.PaymentAmount = calc.CurrencyToAmount;
                        newPayment.CurrencyCode = calc.CurrencyTo;
                        newPayment.ExchangeRate = calc.CurrencyFromExchangeRate;

                        // if (payment.PaymentMode == (int)Common.PaymentMode.Bank)
                        newPayment.TenderType = payment.PaymentMode;
                        newPayment.PaymentModeId = payment.PaymentModeId;
                        //else
                        //newPayment.TenderType = payment.ParentId;

                        //Calculate Total Payment
                        //m_currentOrder.PaymentAmount = m_currentOrder.PaymentAmount + newPayment.PaymentAmount;
                        m_currentOrder.COPaymentList.Add(newPayment);
                    }
                    else
                    {
                        //Calculate Total Payment
                        //m_currentOrder.PaymentAmount = m_currentOrder.PaymentAmount + calc.CurrencyToAmount;
                        oldPayment.PaymentAmount = oldPayment.PaymentAmount + calc.CurrencyToAmount;
                        oldPayment.PaymentModeId = payment.PaymentModeId;
                    }
                }
            }
            iPayments.ClearSelection();
        }

        private void OpenForexConversionPopUp(POSPayments payment)
        {
            if (m_currentOrder.RoundedChangeAmount < 0)
            {
                CurrencyCalculator calc = new CurrencyCalculator(true, -m_currentOrder.RoundedChangeAmount);
                if (calc.ShowDialog() == DialogResult.OK)
                {
                    COPayment oldPayment = m_currentOrder.COPaymentList.Find(delegate(COPayment p) { return p.CurrencyCode != string.Empty && p.CurrencyCode == calc.CurrencyFrom; });
                    if (oldPayment == null)
                    {
                        COPayment newPayment = new COPayment();
                        newPayment.ReceiptDisplay = payment.ReceiptDisplay;
                        newPayment.ForexAmount = calc.CurrencyFromAmount;
                        newPayment.PaymentAmount = calc.CurrencyToAmount;
                        newPayment.CurrencyCode = calc.CurrencyFrom;
                        newPayment.ExchangeRate = calc.CurrencyFromExchangeRate;
                        newPayment.TenderType = payment.PaymentMode;
                        newPayment.PaymentModeId = payment.PaymentModeId;
                        //Calculate Total Payment
                        //m_currentOrder.PaymentAmount = m_currentOrder.PaymentAmount + newPayment.PaymentAmount;
                        m_currentOrder.COPaymentList.Add(newPayment);
                    }
                    else
                    {
                        //Calculate Total Payment
                        //m_currentOrder.PaymentAmount = m_currentOrder.PaymentAmount - oldPayment.PaymentAmount + calc.CurrencyToAmount;
                        oldPayment.ForexAmount = calc.CurrencyFromAmount;
                        oldPayment.PaymentAmount = calc.CurrencyToAmount;
                        oldPayment.PaymentModeId = payment.PaymentModeId;
                    }
                }
            }
            iPayments.ClearSelection();
        }

        private void OpenBonusCheckPopUp(POSPayments payment)
        {
            BonusCheque bonusCheque = new BonusCheque();
            bonusCheque.Order = m_currentOrder;
            if (bonusCheque.ShowDialog() == DialogResult.OK)
            {
                if (bonusCheque.ReturnObject != null)
                {
                    COPayment oldPayment = m_currentOrder.COPaymentList.Find(delegate(COPayment p) { return p.CurrencyCode != string.Empty && p.ChequeNo != null && p.ChequeNo == bonusCheque.ReturnObject.ChequeNo; });
                    if (oldPayment == null)
                    {
                        COPayment newPayment = new COPayment();
                        newPayment.PaymentAmount = bonusCheque.ReturnObject.UseAmount;
                        newPayment.ReceiptDisplay = payment.ReceiptDisplay;
                        newPayment.TenderType = payment.PaymentMode;
                        newPayment.PaymentModeId = payment.PaymentModeId;
                        newPayment.ChequeNo = bonusCheque.ReturnObject.ChequeNo;
                        newPayment.ChqExpiryDate = bonusCheque.ReturnObject.ExpiryDate;
                        newPayment.BankName = bonusCheque.ReturnObject.BankName;
                        newPayment.Reference = bonusCheque.ReturnObject.ChequeNo;
                        m_currentOrder.COPaymentList.Add(newPayment);
                    }
                    else
                    {
                        //Calculate Total Payment
                        //m_currentOrder.PaymentAmount = m_currentOrder.PaymentAmount - oldPayment.PaymentAmount + calc.CurrencyToAmount;
                        // oldPayment.ForexAmount = calc.CurrencyFromAmount;
                        oldPayment.PaymentAmount = bonusCheque.ReturnObject.UseAmount;
                        oldPayment.PaymentModeId = payment.PaymentModeId;
                    }
                }
            }
        }

        private void AddCashPaymentToOrder(POSPayments payment)
        {
            //Bikram: AED changes
            //COPayment oldPayment = m_currentOrder.COPaymentList.Find(delegate(COPayment p) { return p.CurrencyCode == payment.Currency; });
            COPayment oldPayment = m_currentOrder.COPaymentList.Find(delegate(COPayment p) { return p.CurrencyCode == payment.Currency && p.ReceiptDisplay.Trim().ToUpper() == payment.ReceiptDisplay.Trim().ToUpper(); });
            if (oldPayment == null)
            {
                COPayment newPayment = new COPayment();
                //Calculate Total Payment
                //m_currentOrder.PaymentAmount = m_currentOrder.PaymentAmount + payment.Value;
                newPayment.PaymentAmount = payment.Value;
                newPayment.ReceiptDisplay = payment.ReceiptDisplay;
                newPayment.CurrencyCode = payment.Currency;
                newPayment.TenderType = payment.ParentId;
                newPayment.PaymentModeId = payment.PaymentModeId;
                m_currentOrder.COPaymentList.Add(newPayment);
            }
            else
            {
                oldPayment.CurrencyCode = payment.Currency;
                //Add to the existing amount for that payment type id
                oldPayment.PaymentAmount += payment.Value;
                //Calculate Total Payment
                // m_currentOrder.PaymentAmount = m_currentOrder.PaymentAmount + payment.Value;
                oldPayment.PaymentModeId = payment.PaymentModeId;
            }

        }

        private void CreateOrder(Common.OrderType orderType)
        {
            if (m_currentOrder != null && m_currentOrder.Status == (int)Common.OrderStatus.Created && m_currentOrder.DistributorId != 0 && string.IsNullOrEmpty(m_currentOrder.CustomerOrderNo))
            ////&& m_currentOrder.Status != (int)Common.OrderStatus.Closed
            //    && m_currentOrder.Status != (int)Common.OrderStatus.InvoiceCancelled
            //    && m_currentOrder.Status != (int)Common.OrderStatus.Invoiced && m_currentOrder.Status != (int)Common.OrderStatus.Confirmed)
            {
                m_holdOrders.Add(m_currentOrder);
                SetSwitchIndex(1);
            }
            m_currentOrder = new CO();
            m_currentOrder.CreatedBy = m_currentOrder.ModifiedBy = Authenticate.LoggedInUser.UserId;
            m_currentOrder.CreatedByName = Authenticate.LoggedInUser.UserName;
            m_currentOrder.CreatedDate = "";
            m_currentOrder.CustomerOrderNo = "";
            m_currentOrder.OrderType = (int)orderType;
            m_currentOrder.OrderMode = (int)Common.DeliveryMode.Self;
            m_currentOrder.Status = (int)Common.OrderStatus.Created;

            cmbStockpoint.SelectedIndex = 0;
            ClearReceiptHeader();
            RebindGrid(dgvReceiptItems);
            RebindGrid(dgvPayments);
            RefreshOrderTotals();
        }

        public void LoadOrder(CO order)
        {
            if (m_currentOrder != null && m_currentOrder.Status == (int)Common.OrderStatus.Created
                && m_currentOrder.DistributorId != 0 && string.IsNullOrEmpty(m_currentOrder.CustomerOrderNo)
                //&& m_currentOrder.Status != (int)Common.OrderStatus.Cancelled
                ////&& m_currentOrder.Status != (int) Common.OrderStatus.Closed 
                //    && m_currentOrder.Status != (int)Common.OrderStatus.InvoiceCancelled
                //    && m_currentOrder.Status != (int)Common.OrderStatus.Invoiced & m_currentOrder.Status != (int)Common.OrderStatus.Confirmed 
                    && m_currentOrder != order)
            {
                m_holdOrders.Insert(0, m_currentOrder);
                SetSwitchIndex(1);
            }

            string dbMessage = string.Empty;
            // iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, false, ref dbMessage));
            iPayments.LoadItems<POSPayments>(POSPayments.Search(ref dbMessage));
            iItems.LoadItems<POSItem>(POSItem.Search(Common.LocationCode, false, 1, ref dbMessage));

            m_currentOrder = order;
                        
            //EnableOrDisblePrintButton(m_currentOrder);
            RefreshDistributorDetails(order);
            RefreshOrderHeader(order);
            RebindGrid(dgvReceiptItems);
            //Bikram: AED
            if (m_currentOrder.Status == (int)Common.OrderStatus.Created)
            {
                CourierRequiredOnOrder();
                ////if order doesn't have courieramount added.
                if (IsCourierRequiredOnOrder)
                {
                    AddCourierPaymentToOrder();
                }
            }
            RebindGrid(dgvPayments);            
            //Bikram: AED            
            lblCourierCharges.Text = "";
            IEnumerable<COPayment> objPay = from n in m_currentOrder.COPaymentList
                                            where n.TenderType == 101
                                            select n;
            foreach (COPayment obj in objPay)
            {
                lblCourierCharges.Text = Math.Round(obj.PaymentAmount, Common.DisplayAmountRounding, MidpointRounding.AwayFromZero).ToString();
            }

            RefreshOrderTotals();
        }
        /// <summary>
        /// Courier amount required on order or NOT
        /// if order/Log/Team order >=400 AED courier not required
        /// 
        /// </summary>
        private void CourierRequiredOnOrder()
        {
            IsCourierRequiredOnOrder = false;
            if ((m_currentOrder.OrderMode == 2) && (m_currentOrder.DBRoundedTotalAmount < WaiveoffCourierLimit) && (((COLog)(cmbLogList.SelectedItem)).LogNo.Equals("Select Log")))
            {
                IsCourierRequiredOnOrder = true;
                return;
            }
            //Order stay in LOG/Team Order
            if ((m_currentOrder.OrderMode == 2) && !(((COLog)(cmbLogList.SelectedItem)).LogNo.Equals("Select Log")))
            {
                //Find if there order exist in log 
                string errorMessage = string.Empty;
                CO currentOrder = new CO();
                currentOrder.LogNo = ((COLog)(cmbLogList.SelectedItem)).LogNo;
                currentOrder.Status = -1;
                errorMessage = string.Empty;
                // Search All orders in this Log order
                lstOrder = currentOrder.Search(ref errorMessage);
                //if log/team order having single orders and order amount is less then waiveoffCourierLimit
                if (lstOrder.Count == 1 && (m_currentOrder.DBRoundedTotalAmount < WaiveoffCourierLimit))
                {
                    IsCourierRequiredOnOrder = true;
                    return;
                }
                else
                {
                    decimal TotalOrderAmount = 0;
                    CO objco = new CO();
                    for (int i = 0; i < lstOrder.Count; i++)
                    {
                        objco = lstOrder[i];
                        if(objco.Status == (int)Common.OrderStatus.Cancelled)
                            continue;
                        objco.GetCOAllDetails(objco.CustomerOrderNo.ToString(), objco.Status);
                        TotalOrderAmount = TotalOrderAmount + objco.OrderAmount;
                    }
                    //if total log order has less then waiveoff limit and not any order has Courier charged.
                    string strCourierChargedOrder = GetCourieredInvoice(lstOrder);
                    if ((TotalOrderAmount < WaiveoffCourierLimit) && (strCourierChargedOrder.Equals(string.Empty)))
                    {
                        IsCourierRequiredOnOrder = true;
                        return;
                    }
                    else if ((TotalOrderAmount >= WaiveoffCourierLimit) && (!(strCourierChargedOrder.Equals(string.Empty))))
                    {
                        //delete courier payment from team ordedr and update the order
                        string dbMessage = string.Empty;
                        bool result = objco.DeleteCourierPayment(strCourierChargedOrder, "101",ref dbMessage);
                        if (!result)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0621"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                        }
                    }
                    
                }
            }            
        }

        private void EnableOrDisblePrintButton(CO m_currentOrder)
        {
            // To stop invoive printing of 0 price kit print 
            // Disallow invoice printing of older invoices

            if (Convert.ToBoolean(m_currentOrder.IsPrintAllowed) == true)
            {
                this.btnPrintCI.Enabled = true;
            }
            else if ((Convert.ToInt32(m_currentOrder.ValidReportPrintDays) > 0) && Convert.ToInt32(m_currentOrder.ValidReportPrintDays) <= (DateTime.Today - Convert.ToDateTime(m_currentOrder.InvoiceDate)).Days)
            {

                this.btnPrintCI.Enabled = true;
            }
            else
            {
                this.btnPrintCI.Enabled = true;
            }
        }

        private void RefreshDistributorDetails(CO order)
        {
            txtDistributorName.Text = order.DistributorName;
            txtDistributorNo.Text = order.DistributorId == 0 ? "" : order.DistributorId.ToString();
            cmbStockpoint.SelectedValue = order.PCId;
            if ((int)Common.DeliveryMode.Self == order.OrderMode)
                rdoByHand.Checked = true;
            else
                rdoCourier.Checked = true;
            rtxtStockPointAddress.Text = order.DeliverFromAddress.GetAddress();

            if ((Common.IsMiniBranchLocation != 1) && (Common.CheckIfDistributorAddHidden(order.DistributorId)))
                rtxtDistributorAddress.Text = order.DistributorAddress;


            string sName = GetDistributorName();
            if (string.IsNullOrEmpty(sName) && !rdoCourier.Checked)
                rtxtDeliveryAddress.Text = order.DeliverToAddress.GetAddress();
            else
                rtxtDeliveryAddress.Text = sName + '\n' + order.DeliverToAddress.GetAddress();
        }

        private void SetSwitchIndex(int incrementBy)
        {
            m_switchIndex += incrementBy;
            btnSwitch.Tag = m_switchIndex;
            btnSwitch.Text = Convert.ToInt32(m_switchIndex) == 0 ? "Switch" : "Switch [" + btnSwitch.Tag.ToString() + "]";
        }

        private void RefreshOrderHeader(CO order)
        {
            if (order != null && order.InvoiceNo != null)
                lblInvoiceNoValue.Text = order.InvoiceNo.ToString();
            else
                lblInvoiceNoValue.Text = string.Empty;
            lblOrderNoValue.Text = order.CustomerOrderNo.ToString();
            lblOrderDateValue.Text = order.CreatedDate.ToString();
            lblOrderStatusValue.Text = Enum.Parse(typeof(Common.OrderStatus), order.Status.ToString()).ToString();
            lblCreatedByValue.Text = order.CreatedByName;
            //if (order.OrderMode == (int)Common.DeliveryMode.Self)
            //{
            //    rdoByHand.Checked = true;
            //}
            //else if (order.OrderMode == (int)Common.DeliveryMode.Courier)
            //{
            //    rdoCourier.Checked = true;
            //}

            //if (m_currentOrder != null)
            //{
            //    if (m_currentOrder.OrderMode == (int)Common.DeliveryMode.Self)
            //    {
            //        //cmbStockpoint.SelectedIndex = 0;
            //        cmbStockpoint.SelectedValue = m_currentOrder.PCId;
            //        //rdoByHand.Checked = true;
            //    }
            //    else if (m_currentOrder.OrderMode == (int)Common.DeliveryMode.Courier)
            //    {

            //        // Check Index of PUC
            //        cmbStockpoint.SelectedValue = m_currentOrder.PCId;
            //        SetDeliveryFromToAddress();
            //        //rdoCourier.Checked = true;
            //    }

            //}

            //if (order.OrderMode == (int)Common.DeliveryMode.Self)
            //{
            //    rdoByHand.Checked = true;
            //    SelectByHand();
            //}
            //else if (order.OrderMode == (int)Common.DeliveryMode.Courier)
            //{
            //    rdoCourier.Checked = true;
            //    SelectByCourier();
            //}

            BindLogList(-1, order.LogNo);
        }

        private void InitializeApplication()
        {

            NameValueCollection token = new NameValueCollection();
            token.Add("#ShopName", Common.ShopName);
            token.Add("#Address01", Common.Address01);
            token.Add("#Contact", Common.Contact);
            token.Add("#ShopName02", Common.ShopName02);
            token.Add("#Address02", Common.Address02);
            token.Add("#SIT", Common.TaxNumber);
            token.Add("#Contact2", Common.Contact2);
            token.Add("#Footer1", Common.Footer1);
            token.Add("#Footer2", Common.Footer2);

            m_printMgr = new PrintManager(Application.StartupPath + @"\ReceiptFormat\" + Common.ReceiptFormat1, token);

            m_mnuContextMenu = new ContextMenuStrip();
            SetScreenState(Common.ScreenMode.LoggedOut);
            CreateReceiptGrid();
            CreateContextMenu();
            m_currentOrder = null;
            m_holdOrders = new List<CO>();
            dtPOSLocations = Common.ParameterLookup(Common.ParameterType.POSLocations, new ParameterFilter(Common.LocationCode, -1, -1, -1));
            cmbStockpoint.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStockpoint.DataSource = dtPOSLocations;
            cmbStockpoint.DisplayMember = "LocationCode";
            cmbStockpoint.ValueMember = "LocationId";
            if ((int)Common.LocationConfigId.BO == Common.CurrentLocationTypeId)
                iItems.ItemTextField = "BODisplayName";
            else
                iItems.ItemTextField = "Code";

            //Bikram: AED CHANGES, Check only for dubai location
            //get Courier amount
            DataTable dtCourierRequired = Common.ParameterLookup(Common.ParameterType.CourierRequired, new ParameterFilter("CourierRequired", 0, 0, 0));
            
            foreach (DataRow dr in dtCourierRequired.Rows)
            {
                if (dr[1].ToString().Trim().ToUpper() == Common.LocationCode.ToUpper())
                {
                    IsCourierRequired = true;
                    break;
                }
            }
            lblCourier.Visible = IsCourierRequired;
            lblCourierCharges.Visible = IsCourierRequired;
            lblExtra.Visible = IsCourierRequired;
            WaiveoffCourierLimit = CO.GetWaiveoffCourierLimit();
        }

        private void CreateReceiptGrid()
        {
            CreateItemsGrid();
            CreatePaymentsGrid();
        }

        private void CreatePaymentsGrid()
        {
            dgvPayments.Columns.Clear();
            dgvPayments.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();

            dgvc.Name = "dgvcId";
            dgvc.HeaderText = "Id";
            dgvc.Visible = false;
            dgvc.DataPropertyName = "PaymentModeId";
            dgvPayments.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "dgvcItem";
            dgvc.HeaderText = "Payment Type";
            dgvc.Width = 200;
            dgvc.DataPropertyName = "ItemReceiptDisplay";
            dgvPayments.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "dgvcItem";
            dgvc.HeaderText = "Amount";
            dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvc.Width = 106;
            dgvc.DataPropertyName = "RoundedPaymentAmount";
            dgvPayments.Columns.Add(dgvc);

        }

        private void CreateItemsGrid()
        {
            dgvReceiptItems.Columns.Clear();
            dgvReceiptItems.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "dgvcId";
            dgvc.HeaderText = "Id";
            dgvc.Visible = false;
            dgvc.DataPropertyName = "ItemId";
            dgvReceiptItems.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "dgvcReceiptItemDisplay";
            dgvc.HeaderText = "Item";
            dgvc.Width = 220;
            dgvc.Visible = true;

            dgvc.DataPropertyName = "ItemReceiptDisplay";
            dgvReceiptItems.Columns.Add(dgvc);

            dgvc = new DataGridViewTextBoxColumn();
            dgvc.Name = "dgvcAmount";
            dgvc.HeaderText = "RoundedAmount";
            dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvc.Width = 86;
            dgvc.DataPropertyName = "DisplayTotalAmount";
            dgvReceiptItems.Columns.Add(dgvc);
        }

        private void CreateContextMenu()
        {
            ToolStripSeparator separator = new ToolStripSeparator();
            ToolStripSeparator separator1 = new ToolStripSeparator();
            ToolStripSeparator separator2 = new ToolStripSeparator();
            ToolStripSeparator separator3 = new ToolStripSeparator();
            ToolStripSeparator separator4 = new ToolStripSeparator();
            ToolStripSeparator separator5 = new ToolStripSeparator();
            m_mnuContextMenu.AutoSize = false;
            m_mnuContextMenu.Width = 250;
            m_mnuContextMenu.Height = 210;
            m_mnuContextMenu.ShowCheckMargin = m_mnuContextMenu.ShowImageMargin = false;
            ToolStripLabel mnuItemDescription = new ToolStripLabel();
            mnuItemDescription.AutoSize = false;
            mnuItemDescription.Width = m_mnuContextMenu.Width - 15;
            mnuItemDescription.Height = 32;
            mnuItemDescription.TextAlign = ContentAlignment.MiddleLeft;
            mnuItemDescription.BackgroundImage = Properties.Resources.GlassyLook;
            mnuItemDescription.BackgroundImageLayout = ImageLayout.Stretch;
            mnuItemDescription.BackColor = Color.FromArgb(192, 219, 192);
            mnuItemDescription.Font = new Font("Verdana", 9, FontStyle.Bold);

            ToolStripButton mnuItemAdd1 = new ToolStripButton();
            mnuItemAdd1.AutoSize = false;
            mnuItemAdd1.Width = m_mnuContextMenu.Size.Width - 15;
            mnuItemAdd1.Height = 32;
            mnuItemAdd1.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            mnuItemAdd1.TextImageRelation = TextImageRelation.ImageBeforeText;
            mnuItemAdd1.Image = Properties.Resources.Add;
            mnuItemAdd1.ImageAlign = ContentAlignment.MiddleLeft;
            mnuItemAdd1.Text = "Add 1";
            mnuItemAdd1.TextAlign = ContentAlignment.MiddleCenter;
            mnuItemAdd1.Font = new Font("Verdana", 9, FontStyle.Bold);
            mnuItemAdd1.Click += new EventHandler(mnuItemAdd1_Click);

            ToolStripButton mnuItemRemove1 = new ToolStripButton();
            mnuItemRemove1.AutoSize = false;
            mnuItemRemove1.Width = m_mnuContextMenu.Size.Width - 15;
            mnuItemRemove1.Height = 32;
            mnuItemRemove1.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            mnuItemRemove1.TextImageRelation = TextImageRelation.ImageBeforeText;
            mnuItemRemove1.ForeColor = Color.Red;
            mnuItemRemove1.Image = Properties.Resources.Remove;
            mnuItemRemove1.ImageAlign = ContentAlignment.MiddleLeft;
            mnuItemRemove1.Text = "Remove 1";
            mnuItemRemove1.TextAlign = ContentAlignment.MiddleCenter;
            mnuItemRemove1.Font = new Font("Verdana", 9, FontStyle.Bold);
            mnuItemRemove1.Click += new EventHandler(mnuItemRemove1_Click);

            ToolStripButton mnuItemRemoveAll = new ToolStripButton();
            mnuItemRemoveAll.AutoSize = false;
            mnuItemRemoveAll.Width = m_mnuContextMenu.Size.Width - 15;
            mnuItemRemoveAll.Height = 32;
            mnuItemRemoveAll.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            mnuItemRemoveAll.TextImageRelation = TextImageRelation.ImageBeforeText;
            mnuItemRemoveAll.ForeColor = Color.Red;
            mnuItemRemoveAll.Image = Properties.Resources.Remove;
            mnuItemRemoveAll.ImageAlign = ContentAlignment.MiddleLeft;
            mnuItemRemoveAll.Text = "Remove";
            mnuItemRemoveAll.TextAlign = ContentAlignment.MiddleCenter;
            mnuItemRemoveAll.Font = new Font("Verdana", 9, FontStyle.Bold);
            mnuItemRemoveAll.Click += new EventHandler(mnuItemRemoveAll_Click);

            ToolStripButton mnuItemCancel = new ToolStripButton();
            mnuItemCancel.AutoSize = false;
            mnuItemCancel.Width = m_mnuContextMenu.Size.Width - 15;
            mnuItemCancel.Height = 32;
            mnuItemCancel.Text = "Cancel";
            mnuItemCancel.TextAlign = ContentAlignment.MiddleLeft;
            mnuItemCancel.Font = new Font("Verdana", 9, FontStyle.Bold);
            mnuItemCancel.Click += new EventHandler(mnuItemCancel_Click);

            m_mnuContextMenu.Items.Add(mnuItemDescription);
            m_mnuContextMenu.Items.Add(separator4);
            m_mnuContextMenu.Items.Add(mnuItemAdd1);
            m_mnuContextMenu.Items.Add(separator);
            m_mnuContextMenu.Items.Add(mnuItemRemove1);
            m_mnuContextMenu.Items.Add(separator1);
            m_mnuContextMenu.Items.Add(mnuItemRemoveAll);
            m_mnuContextMenu.Items.Add(separator2);
            //m_mnuContextMenu.Items.Add(separator3);
            m_mnuContextMenu.Items.Add(mnuItemCancel);
        }

        private void ClearReceiptHeader()
        {
            lblOrderNoValue.Text = "";
            lblInvoiceNoValue.Text = "";
            lblOrderDateValue.Text = "";
            lblOrderStatusValue.Text = "";
            lblCreatedByValue.Text = "";
        }

        private void RebindGrid(DataGridView dgv)
        {
            switch (dgv.Name)
            {
                case "dgvReceiptItems":
                    if (m_currentOrder == null)
                    {
                        dgv.DataSource = null;
                        List<CODetail> newList = new List<CODetail>();
                        dgv.DataSource = newList;
                        ((CurrencyManager)dgv.BindingContext[newList]).Refresh();
                    }
                    else
                    {
                        dgv.DataSource = m_currentOrder.CODetailList;
                        ((CurrencyManager)dgv.BindingContext[m_currentOrder.CODetailList]).Refresh();
                    }
                    break;
                case "dgvPayments":
                    if (m_currentOrder == null)
                    {
                        dgv.DataSource = null;
                        dgv.DataSource = new List<COPayment>();
                    }
                    else
                    {

                        dgv.DataSource = m_currentOrder.COPaymentList;
                        ((CurrencyManager)dgv.BindingContext[m_currentOrder.COPaymentList]).Refresh();

                    }
                    break;

            }
            int iCount = dgv.Rows.Count;
            if (iCount > 0 && dgv.ColumnCount > 1 && dgv.Rows[iCount - 1].Cells[1].Visible)
                dgv.CurrentCell = dgv.Rows[iCount - 1].Cells[1];
        }

        private bool SaveOrder(int statusParam)
        {
            bool returnStatus = false;
            //if (m_currentOrder.MinimumOrderAmount <= m_currentOrder.OrderAmount || statusParam == (int)Common.OrderStatus.Cancelled)
            //{

            if (m_currentOrder.CODetailList.Count > 0)
            {
                m_currentOrder.SourceLocationCode = Common.LocationCode;

                string validationMessage = string.Empty, dbMessage = string.Empty;
                if (m_currentOrder.Status == (int)Common.OrderStatus.Modify)
                    returnStatus = m_currentOrder.Save(statusParam, m_existingOrder, ref validationMessage, ref dbMessage);
                else
                    returnStatus = m_currentOrder.Save(statusParam, ref validationMessage, ref dbMessage);
                if (returnStatus)
                {
                    //Search Order
                    m_currentOrder.GetCOAllDetails(m_currentOrder.CustomerOrderNo, m_currentOrder.Status);
                    LoadOrder(m_currentOrder);
                    returnStatus = true;
                    string msg = string.Empty;
                    if (m_currentOrder.Status == (int)Common.OrderStatus.Created)
                        msg = "Saved";
                    else if (m_currentOrder.Status == (int)Common.OrderStatus.Confirmed)
                        msg = "Confirmed";
                    else if (m_currentOrder.Status == (int)Common.OrderStatus.Cancelled)
                        msg = "Cancelled";
                    else if (m_currentOrder.Status == (int)Common.OrderStatus.Modify)
                        msg = "Modified";
                    else if (m_currentOrder.Status == (int)Common.OrderStatus.Invoiced)
                        msg = "Invoiced";
                    MessageBox.Show(Common.GetMessage("40015", msg), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Set screen mode to saved...disable save button
                }
                else if (!string.IsNullOrEmpty(validationMessage))
                {
                    MessageBox.Show(Common.GetMessage(validationMessage), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!string.IsNullOrEmpty(dbMessage))
                {
                    MessageBox.Show(Common.GetMessage("40016", dbMessage), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Common.LogException(new Exception(dbMessage));
                }
                else
                {
                    MessageBox.Show(Common.GetMessage("40017"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //}
            //else if (statusParam != (int)Common.OrderStatus.Cancelled)
            //{
            //    MessageBox.Show(Common.GetMessage("VAL0506", m_currentOrder.MinimumOrderAmount.ToString("0.00")), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            return returnStatus;
        }

        private void ClearDistributorDetails()
        {
            txtDistributorNo.Text = "";
            rtxtDistributorAddress.Text = "";
            txtDistributorName.Text = "";
            cmbLogList.SelectedIndex = -1;
        }

        private DataSet CreatePrintDataSet(int type)
        {

            string errorMessage = string.Empty;
            DataSet ds = CO.GetOrderForPrint(type, m_currentOrder.CustomerOrderNo, ref errorMessage);
            if (errorMessage.Trim().Length == 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add(new DataColumn("Header", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("DateText", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("TINNo", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("OrderAmountWords", Type.GetType("System.String")));
                    ds.Tables[1].Columns.Add(new DataColumn("PriceInclTax", Type.GetType("System.String")));
                    ds.Tables[1].Columns.Add(new DataColumn("IsLocation", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("PANNo", Type.GetType("System.String")));
                    ds.Tables[0].Columns.Add(new DataColumn("CourierAmount", Type.GetType("System.String")));

                    ds.Tables[0].Rows[0]["Header"] = (type == 1 ? "Customer Order" : "Retail Invoice");
                    ds.Tables[0].Rows[0]["TINNo"] = Common.TINNO;
                    ds.Tables[0].Rows[0]["PANNo"] = Common.PANNO;
                    //if (type == 2)
                    //{
                    //    if (ds.Tables[0].Rows[0]["Password"].ToString() != string.Empty)
                    //    {
                    //        User objUser = new User();
                    //        ds.Tables[0].Rows[0]["Password"] = objUser.DecryptPassword(ds.Tables[0].Rows[0]["Password"].ToString());
                    //    }
                    //    else
                    //    {
                    //        ds.Tables[0].Rows[0]["Password"] = string.Empty;

                    //    }
                    //}

                    ds.Tables[0].Columns.Add(new DataColumn("MiniBranch", Type.GetType("System.String")));
                    ds.Tables[0].Rows[0]["MiniBranch"] = (Common.IsMiniBranchLocation == 1 ? "N" : "Y");


                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["OrderAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["OrderAmountWords"] = Common.AmountToWords.AmtInWord(Convert.ToDecimal(ds.Tables[0].Rows[i]["OrderAmount"]) + Convert.ToDecimal(ds.Tables[0].Rows[i]["TaxAmount"]));
                    ds.Tables[0].Rows[i]["PaymentAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["PaymentAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["ChangeAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["ChangeAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["TotalBV"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalBV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["TotalPV"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalPV"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[0].Rows[i]["TotalWeight"] = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalWeight"]), 3);
                    ds.Tables[0].Rows[i]["DateText"] = (Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"])).ToString(Common.DTP_DATE_FORMAT);
                }
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    ds.Tables[1].Rows[i]["DP"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["DP"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["UnitPrice"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["UnitPrice"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["Qty"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["Qty"]), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["Amount"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["Amount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["PriceInclTax"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["UnitPrice"]) + (Convert.ToDecimal(ds.Tables[1].Rows[i]["TaxAmount"]) / Convert.ToDecimal(ds.Tables[1].Rows[i]["Qty"])), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    if (ds.Tables[1].Rows[i]["TaxPercent"].ToString().Length > 0)
                        ds.Tables[1].Rows[i]["TaxPercent"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["TaxPercent"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[1].Rows[i]["Discount"] = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[i]["Discount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                }
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    ds.Tables[2].Rows[i]["PaymentAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[2].Rows[i]["PaymentAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    //Bikram:AED Changes
                    if (Convert.ToInt32(ds.Tables[2].Rows[i]["TenderType"].ToString()) == 101)
                    {
                        ds.Tables[0].Rows[0]["CourierAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[2].Rows[i]["PaymentAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                        ds.Tables[2].Rows[i]["PaymentAmount"] = Math.Round(Convert.ToDecimal(0), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    }
                }

                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    ds.Tables[3].Rows[i]["TaxPercent"] = Math.Round(Convert.ToDecimal(ds.Tables[3].Rows[i]["TaxPercent"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                    ds.Tables[3].Rows[i]["TaxAmount"] = Math.Round(Convert.ToDecimal(ds.Tables[3].Rows[i]["TaxAmount"]), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
                }
            }

            //if (Common.IsMiniBranchLocation == 1)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {

            //        ds.Tables[0].Rows[i]["DeliverFromAddress1"] = string.Empty;
            //        ds.Tables[0].Rows[i]["DeliverFromAddress2"] = string.Empty;
            //        ds.Tables[0].Rows[i]["DeliverFromAddress3"] = string.Empty;
            //        ds.Tables[0].Rows[i]["DeliverFromAddress4"] = string.Empty;
            //        ds.Tables[0].Rows[i]["ToCity"] = string.Empty;
            //        ds.Tables[0].Rows[i]["DeliverToPincode"] = string.Empty;
            //        ds.Tables[0].Rows[i]["ToState"] = string.Empty;
            //        ds.Tables[0].Rows[i]["ToCountry"] = string.Empty;
            //        ds.Tables[0].Rows[i]["DeliverToTelephone"] = string.Empty;
            //        ds.Tables[0].Rows[i]["DeliverToMobile"] = string.Empty;

            //    }
            //}
            return ds;
        }

        #endregion

        #region Public Methods

        public void SetMultiplierIndex(int value)
        {
            btnMultiplier.Tag = value;
            m_multiplierIndex = value;
            btnMultiplier.Text = "x " + value;
        }

        #endregion

        private void cmbStockpoint_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable dtLocationFrom = Common.ParameterLookup(Common.ParameterType.LocationAddress, new ParameterFilter("", Common.BOId, -1, -1));
            //    DataTable dtLocationTo = Common.ParameterLookup(Common.ParameterType.LocationAddress, new ParameterFilter("", Common.PCId, -1, -1));

            //    CreateAddress(Common.DeliverFromAddress = new Address(), dtLocationFrom);
            //    CreateAddress(Common.DeliverToAddress = new Address(), dtLocationTo);

            //    if (dtLocationFrom.Rows[0][0].ToString() != dtLocationTo.Rows[0][0].ToString())
            //    {
            //        if (rdoCourier != null)
            //            rdoCourier.Checked = true;

            //        if (rdoByHand != null)
            //            rdoByHand.Checked = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    LogManager.WriteExceptionLog(ex);
            //}
        }


        private void txtDistributorNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDistributorNo_Validated(null, null);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int iIndex = 0;
            string orderno = "";
            string sTempOrder = "";
            int ilstDigit = 0;
            Button objBtn = null;
            try
            {
                if (m_currentOrder == null || string.IsNullOrEmpty(m_currentOrder.CustomerOrderNo))
                    return;
                objBtn = (Button)sender;
                orderno = m_currentOrder.CustomerOrderNo;
                iIndex = orderno.LastIndexOf('/');
                ilstDigit = Convert.ToInt32(orderno.Substring(iIndex + 1, orderno.Length - iIndex - 1));
                if (objBtn != null && string.Compare(objBtn.Tag.ToString(), "Previous", true) == 0)
                    ilstDigit = ilstDigit - 1;
                else
                    ilstDigit = ilstDigit + 1;
                orderno = orderno.Substring(0, iIndex + 1 + (6 - (ilstDigit.ToString().Length)));
                orderno += ilstDigit;
                CO order = new CO();
                order.GetCOAllDetails(orderno, -1);
                if (order != null && !string.IsNullOrEmpty(order.CustomerOrderNo))
                    GetCustomerDetail(order);
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void GetCustomerDetail(CO Objorder)
        {
            try
            {
                Common.ScreenMode mode = Common.ScreenMode.LoggedIn;
                switch (Objorder.Status)
                {
                    case 1:
                        {
                            mode = Common.ScreenMode.OrderSaved;
                            break;
                        }
                    case 2:
                        {
                            mode = Common.ScreenMode.OrderCancelled;
                            break;
                        }
                    case 3:
                        {
                            mode = Common.ScreenMode.OrderConfirmed;
                            break;
                        }
                    case 4:
                        {
                            mode = Common.ScreenMode.Invoiced;
                            break;
                        }
                    case 5:
                        {
                            mode = Common.ScreenMode.InvoiceCancelled;
                            break;
                        }
                    default:
                        break;
                }


                LoadOrder(Objorder);
                SetScreenState(mode);
            }
            catch
            {
            }
        }

        private void iItems_Load(object sender, EventArgs e)
        {

        }

        private void txtDistributorNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtBarcode_Validated(object sender, EventArgs e)
        {

        }

        

       private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
       {
           try
           {
               if (e.KeyCode == Keys.Enter || e.KeyCode==Keys.Tab)
               {
                   string dbMessage = string.Empty;
                   POSItem posItem = new POSItem();

                   if (m_currentOrder.OrderType != (int)Common.OrderType.KitOrder)
                       m_currentOrder.OrderType = m_currentOrder.IsFirstOrderForDistributor ? (int)Common.OrderType.FirstOrder : (int)Common.OrderType.Reorder;

                   if ((int)Common.OrderType.KitOrder == m_currentOrder.OrderType)
                   {
                       posItem = POSItem.Search(txtBarcode.Text, Common.LocationCode, true, 2, ref dbMessage);
                   }
                   else if (m_currentOrder.IsFirstOrderForDistributor == false)
                   {
                       posItem = POSItem.Search(txtBarcode.Text, Common.LocationCode, false, 0, ref dbMessage);
                   }
                   else if (m_currentOrder.IsFirstOrderForDistributor == true)
                   {
                       posItem = POSItem.Search(txtBarcode.Text, Common.LocationCode, false, 1, ref dbMessage);
                   }

                   if (m_currentOrder.OrderType == (int)Common.OrderType.KitOrder)
                   {
                       if (m_currentOrder.CODetailList.Count < 1)
                       {
                           _isKitAdded = posItem.IsKit;
                           AdjustItem(posItem.Id, posItem.Code, 1);
                       }
                   }
                   else
                   {

                       if (m_currentOrder.CODetailList.Find(pp => pp.IsKit == true) == null && posItem.IsKit)
                       {
                           _isKitAdded = posItem.IsKit;
                           AdjustItem(posItem.Id, posItem.Code, Convert.ToInt32(btnMultiplier.Tag.ToString()));
                       }
                       else if (posItem.IsKit == false)
                       {
                           _isKitAdded = posItem.IsKit;
                           AdjustItem(posItem.Id, posItem.Code, Convert.ToInt32(btnMultiplier.Tag.ToString()));
                       }

                   }
                   txtBarcode.Text = "";
                   txtBarcode.Focus();
               }

           }
           catch (Exception ex) {
               MessageBox.Show(Common.GetMessage("VAL0087"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
               LogManager.WriteExceptionLog(ex);
               txtBarcode.Text = "";
               txtBarcode.Focus();
           }
       
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbLogList_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGetAddress.Enabled = true;
            if((rdoCourier.Checked))
            {
                SetDeliveryToAddress();

            }
        }
      }
}
