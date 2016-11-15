using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using System.Data;

namespace POSClient.UI
{
    public partial class MainScreen
    {

        public void SetScreenState(Common.ScreenMode screenMode)
        {
            switch (screenMode)
            {

                case Common.ScreenMode.LoggedIn:
                    InitializeRights();
                    DisableAllExcludeRadio();
                    if (cmbStockpoint.Items.Count > 0)
                        cmbStockpoint.SelectedIndex = 0;
                    ClearReceiptHeader();
                    RebindGrid(dgvReceiptItems);
                    RebindGrid(dgvPayments);
                    ClearDistributorDetails();                    
                    btnNewOrder.Enabled = IsNewOrderAvailable;
                    btnDistributorRegister.Enabled = IsRegisterDistributorAvailable;
                    btnKitOrder.Enabled = IsKitOrderAvailable;
                    btnTeamOrder.Enabled = IsTeamOrderAvailable;
                    SetSwitchIndex(-m_switchIndex);
                    btnSwitch.Enabled = true;
                    btnNext.Enabled = false;
                    btnPrevious.Enabled = false;
                    break;
                case Common.ScreenMode.LoggedOut:
                    DisableAll();


                    if (cmbStockpoint.Items.Count > 0)
                        cmbStockpoint.SelectedIndex = 0;

                    ClearReceiptHeader();
                    RebindGrid(dgvReceiptItems);
                    RebindGrid(dgvPayments);
                    ClearDistributorDetails();
                    btnNewOrder.Enabled = false;
                    btnTeamOrder.Enabled = false;
                    btnDistributorRegister.Enabled = false;
                    btnKitOrder.Enabled = false;
                    btnNext.Enabled = false;
                    btnPrevious.Enabled = false;
                    ClearReceiptHeader();
                    break;
                case Common.ScreenMode.Invoiced:
                    //DisableAll();
                    DisableAllExcludeRadio();
                    btnPrintCO.Enabled = IsPrintOrderAvailable;
                    btnPrintCI.Enabled = IsPrintInvoiceAvailable;
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnSwitch.Enabled = true;
                    //this.EnableOrDisblePrintButton();
                    break;
                case Common.ScreenMode.InvoiceCancelled:
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    break;
                case Common.ScreenMode.InvoicePrinted:
                    break;
                case Common.ScreenMode.KitOrder:
                case Common.ScreenMode.NewOrder:
                    DisableAll();                    
                    if (cmbStockpoint.Items.Count > 0)
                        cmbStockpoint.SelectedIndex = 0;
                    SetMultiplierIndex(1);


                    //   rdoLog.Checked = true;
                    //gbLogType.Enabled = IsLogAvailable;
                    cmbLogList.Enabled = IsLogAvailable;
                    cmbStockpoint.Enabled = IsStockPointAvailable;
                    txtDistributorNo.Text = string.Empty;
                    txtDistributorName.Text = string.Empty;
                    btnNext.Enabled = false;
                    btnPrevious.Enabled = false;
                    if (cmbLogList.Items.Count > 0)
                    {
                        //List<POSClient.BusinessObjects.COLog> LogList = new List<POSClient.BusinessObjects.COLog>();
                        //POSClient.BusinessObjects.COLog logSearch = new POSClient.BusinessObjects.COLog();
                        //logSearch.LogNo = "Select Log";
                        //LogList.Insert(0, logSearch);

                        //cmbLogList.DataSource = LogList;
                        //cmbLogList.DisplayMember = "LogNo";
                        //cmbLogList.ValueMember = "LogValue";

                        cmbLogList.SelectedIndex = 0;

                    }
                    rtxtDistributorAddress.Text = string.Empty;
                    pnlDistDetails.Enabled = true;
                    txtDistributorNo.Enabled = true;
                    btnCancel.Enabled = IsCancelAvailable;
                    btnSwitch.Enabled = true;
                    rdoLog.Checked = true;
                    break;

                case Common.ScreenMode.OrderCancelled:
                    //DisableAll();
                    DisableAllExcludeRadio();
                    btnDistributorRegister.Enabled = IsRegisterDistributorAvailable;
                    btnKitOrder.Enabled = IsKitOrderAvailable;
                    btnTeamOrder.Enabled = IsTeamOrderAvailable;
                    btnSwitch.Enabled = true;
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                    break;
                case Common.ScreenMode.OrderConfirmed:
                    //DisableAll();
                    DisableAllExcludeRadio();
                    btnClearPayments.Enabled = true;
                    btnPrintCO.Enabled = IsPrintOrderAvailable;
                    btnInvoice.Enabled = IsInvoiceAvailable;
                    btnInvoiceandPrint.Enabled = IsInvoiceAvailable && IsPrintInvoiceAvailable;
                    //this.EnableOrDisblePrintButton();
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;

                    DataTable dtLocation = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("", -1, m_currentOrder.PCId, 0));
                    if (dtLocation != null && dtLocation.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtLocation.Rows[0]["LocationTypeId"]) == (int)Common.LocationConfigId.PC)
                        {
                            btnModify.Enabled = IsModifyAvailable;
                            btnModify.Enabled = false;
                        }
                    }
                    btnSwitch.Enabled = true;
                    btnCancel.Enabled = IsCancelAvailable;
                    break;
                case Common.ScreenMode.OrderInMemory:
                    DisableAll();
                    pnlDistDetails.Enabled = true;
                    if (m_currentOrder.DistributorId != 0)
                    {
                        pnlReceiptHeader.Enabled = true;
                        pnlReceiptTotals.Enabled = true;
                        gbLogType.Enabled = IsLogAvailable;
                        cmbLogList.Enabled = IsLogAvailable;
                        cmbStockpoint.Enabled = IsStockPointAvailable;
                        gbDeliveryMode.Enabled = IsChangeDeliveryModeAvailable;
                    }
                    else
                    {
                        gbLogType.Enabled = IsLogAvailable;
                        cmbLogList.Enabled = IsLogAvailable;
                        cmbStockpoint.Enabled = IsStockPointAvailable;
                        cmbLogList.SelectedIndex = 0;
                        pnlDistDetails.Enabled = true;
                        txtDistributorNo.Enabled = true;
                    }
                    pnlItems.Enabled = true;
                    if (m_currentOrder.OrderType == (int)Common.OrderType.KitOrder)
                    {
                        btnMultiplier.Enabled = false;
                    }
                    else
                    {
                        btnMultiplier.Enabled = true;
                    }
                    btnClearServices.Enabled = IsNewOrderAvailable;
                    btnSave.Enabled = IsNewOrderAvailable;
                    btnSwitch.Enabled = true;
                    btnCancel.Enabled = IsCancelAvailable;
                    break;
                case Common.ScreenMode.OrderSaved:
                    //DisableAll();
                    DisableAllExcludeRadio();
                    pnlPayments.Enabled = true;
                    btnClearPayments.Enabled = true;
                    btnConfirm.Enabled = IsConfirmAvailable;
                    btnCancel.Enabled = IsCancelAvailable;
                    pnlDistDetails.Enabled = true;
                    if (m_currentOrder != null && m_currentOrder.OrderType != (int)Common.OrderType.KitOrder)
                        btnGiftVoucher.Enabled = true;
                    btnNewOrder.Enabled = IsNewOrderAvailable;
                    btnSwitch.Enabled = true;
                    btnNext.Enabled = true;
                    btnPrevious.Enabled = true;
                    break;
                case Common.ScreenMode.DistributorAdded:
                    DisableAll();
                    pnlItems.Enabled = true;
                    if (m_currentOrder.OrderType == (int)Common.OrderType.KitOrder)
                    {
                        btnMultiplier.Enabled = false;
                    }
                    else
                    {
                        btnMultiplier.Enabled = true;
                    }
                    pnlDistDetails.Enabled = true;
                    //                   pnlReceiptPreview.Enabled = true;                    
                    pnlReceiptHeader.Enabled = true;
                    lblBarcode.Enabled = true;
                    txtBarcode.Enabled = true;
                    pnlReceiptTotals.Enabled = true;
                    cmbLogList.Enabled = IsLogAvailable;
                    cmbStockpoint.Enabled = IsStockPointAvailable;
                    btnCancel.Enabled = IsCancelAvailable;
                    btnCancelAll.Enabled = IsCancelAvailable;
                    btnSave.Enabled = IsNewOrderAvailable;
                    btnSave.Enabled = IsPreviewAvailable;
                    btnSwitch.Enabled = true;
                    gbLogType.Enabled = IsLogAvailable;
                    gbDeliveryMode.Enabled = IsChangeDeliveryModeAvailable;
                    btnNext.Enabled = false;
                    btnPrevious.Enabled = false;
                    break;
                case Common.ScreenMode.OrderModify:
                    DisableAll();
                    SetMultiplierIndex(1);
                    btnModify.Enabled = IsConfirmAvailable;
                    btnModify.Enabled = false;
                    btnSwitch.Enabled = true;
                    btnConfirm.Enabled = IsConfirmAvailable;
                    pnlPayments.Enabled = true;
                    pnlItems.Enabled = true;
                    break;
                default:
                    break;
            }

        }
        public void DisableAllExcludeRadio()
        {
            try
            {
                btnNewOrder.Enabled = IsNewOrderAvailable;
                btnTeamOrder.Enabled = IsTeamOrderAvailable;
                btnDistributorRegister.Enabled = IsRegisterDistributorAvailable;
                btnKitOrder.Enabled = IsKitOrderAvailable;
                btnPrintCO.Enabled = false;
                btnPrintCI.Enabled = false;
                btnSwitch.Enabled = false;
                btnGiftVoucher.Enabled = false;
                pnlDistDetails.Enabled = false;
                btnNext.Enabled = false;
                btnPrevious.Enabled=false;
                if (iItems.ItemCount != 0)
                    iItems.Reset();

                pnlItems.Enabled = false;
                if (iPayments.ItemCount != 0)
                    iPayments.Reset();
                pnlPayments.Enabled = false;
                btnCancel.Enabled = false;
                btnCancelAll.Enabled = false;
                btnSave.Enabled = false;
                btnConfirm.Enabled = false;
                btnInvoice.Enabled = false;
                btnInvoiceandPrint.Enabled = false;
                pnlReceiptHeader.Enabled = false;
                txtBarcode.Enabled = false;
                pnlReceiptTotals.Enabled = false;
                btnModify.Enabled = false;
                //  pnlReceiptPreview.Enabled = false;
                txtDistributorNo.Enabled = false;
                gbLogType.Enabled = false;

                //rdoLog.Checked = true;
                cmbLogList.Enabled = false;
                cmbStockpoint.Enabled = false;
                //rtxtStockPointAddress.Enabled = false;
                gbDeliveryMode.Enabled = false;
                btnGetAddress.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DisableAll()
        {
            try
            {
                btnNewOrder.Enabled = IsNewOrderAvailable;
                btnTeamOrder.Enabled = IsTeamOrderAvailable;
                btnDistributorRegister.Enabled = IsRegisterDistributorAvailable;
                btnKitOrder.Enabled = IsKitOrderAvailable;
                btnPrintCO.Enabled = false;
                btnPrintCI.Enabled = false;
                btnSwitch.Enabled = false;
                btnGiftVoucher.Enabled = false;
                btnNext.Enabled = false;
                btnPrevious.Enabled = false;
                pnlDistDetails.Enabled = false;
                if (iItems.ItemCount != 0)
                    iItems.Reset();

                pnlItems.Enabled = false;
                if (iPayments.ItemCount != 0)
                    iPayments.Reset();
                pnlPayments.Enabled = false;
                btnCancel.Enabled = false;
                btnCancelAll.Enabled = false;
                btnSave.Enabled = false;
                btnConfirm.Enabled = false;
                btnInvoice.Enabled = false;
                btnInvoiceandPrint.Enabled = false;
                pnlReceiptHeader.Enabled = false;
                pnlReceiptTotals.Enabled = false;
                txtBarcode.Enabled = false;
                lblBarcode.Enabled = false;
                btnModify.Enabled = false;
                //  pnlReceiptPreview.Enabled = false;
                txtDistributorNo.Enabled = false;
                gbLogType.Enabled = false;
                rdoByHand.Checked = true;
                rdoLog.Checked = true;
                cmbLogList.Enabled = false;
                cmbStockpoint.Enabled = false;
                //rtxtStockPointAddress.Enabled = false;
                gbDeliveryMode.Enabled = false;
                btnGetAddress.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
