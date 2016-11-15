using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PromotionsComponent.BusinessLayer;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Logging;
namespace POSClient.UI
{
    public partial class frmGiftVoucher : POSClient.UI.BaseChildForm
    {
        private List<PromotionsComponent.BusinessLayer.GiftVoucherDistributor> m_lstGiftVoucher;
        private int m_distributorID;
        private GiftVoucherDistributor m_CurrentVoucherDistributor;
        private GiftVoucher m_CurrentVoucher;
        private GiftVoucherItemDetail m_VoucherItem;
        private Price m_price;
        public decimal MinBuyAmount { get; set; }
        public Price Price
        {
            get { return m_price; }
            set { m_price = value; }
        }
        public frmGiftVoucher(int distributorID)
        {
            InitializeComponent();
            m_distributorID = distributorID;
            InitializeControl();
        }
        
        private void InitializeControl()
        {
            try
            {
                lblDistributorValue.Text = m_distributorID.ToString();

                string GRIDVIEW_XML_PATH = Environment.CurrentDirectory + "\\App_Data\\POSGridViewDefinition.xml";
                dgvGiftVoucher.AutoGenerateColumns = false;
                dgvGiftVoucher.AllowUserToAddRows = false;
                dgvGiftVoucher.AllowUserToDeleteRows = false;
                dgvGiftVoucher.SelectionMode = DataGridViewSelectionMode.CellSelect;
                DataGridView dgvSearchNew = CoreComponent.Core.BusinessObjects.Common.GetDataGridViewColumns(dgvGiftVoucher, GRIDVIEW_XML_PATH);

                dgvGiftVoucherItem.AutoGenerateColumns = false;
                dgvGiftVoucherItem.AllowUserToAddRows = false;
                dgvGiftVoucherItem.AllowUserToDeleteRows = false;
                dgvGiftVoucherItem.SelectionMode = DataGridViewSelectionMode.CellSelect;
                DataGridView dgvGiftVoucherItemNew = CoreComponent.Core.BusinessObjects.Common.GetDataGridViewColumns(dgvGiftVoucherItem, GRIDVIEW_XML_PATH);

                GetGiftVoucher();
                BindGrid();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        private void GetGiftVoucher()
        {
            try
            {
                GiftVoucherDistributor voucher = new GiftVoucherDistributor();
                voucher.IssueTo = m_distributorID;
                voucher.Availed = 0;
                voucher.MinBuyAmount = 0;
                m_lstGiftVoucher = voucher.Search();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindGrid()
        {
            try
            {
                dgvGiftVoucher.DataSource = null;
                if (m_lstGiftVoucher != null)
                {
                    dgvGiftVoucher.DataSource = m_lstGiftVoucher;
                    dgvGiftVoucher.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        private void BindItemGrid()
        {
            try
            {
                dgvGiftVoucherItem.DataSource = null;
                if (m_CurrentVoucher != null)
                {
                    dgvGiftVoucherItem.DataSource = m_CurrentVoucher.VoucherItemDetailList;
                    dgvGiftVoucherItem.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvGiftVoucher_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvGiftVoucher.SelectedRows.Count > 0)
                {
                    m_CurrentVoucherDistributor = m_lstGiftVoucher[dgvGiftVoucher.SelectedRows[0].Index];
                    BindItems();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }
        
        private void BindItems()
        {
            if (m_CurrentVoucherDistributor != null)
            {
                GiftVoucher voucher = new GiftVoucher();
                voucher.VoucherCode = m_CurrentVoucherDistributor.GiftVoucherCode;
                List<GiftVoucher> lstVouchers=voucher.Search();
                if (lstVouchers != null && lstVouchers.Count > 0)
                {
                    m_CurrentVoucher = lstVouchers[0];
                }
                BindItemGrid();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvGiftVoucherItem.SelectedRows.Count > 0)
                {
                    m_VoucherItem = m_CurrentVoucher.VoucherItemDetailList[dgvGiftVoucherItem.SelectedRows[0].Index];
                    MinBuyAmount = m_CurrentVoucher.MinBuyAmount;
                    SetPrice();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("40000"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.WriteExceptionLog(ex);
            }
        }     

        private void SetPrice()
        {
            try
            {
                DataTable dtSetDiscountPrice = Common.ParameterLookup(Common.ParameterType.GIFTVOUCHERDISCVALUE, new ParameterFilter(Common.LocationCode, Convert.ToInt16(Common.CountryID), 0, 0));
                if (dtSetDiscountPrice.Rows.Count > 0)
                {
                    m_price = new Price(Common.INT_DBNULL, m_VoucherItem.ItemID, m_VoucherItem.ItemCode, m_VoucherItem.ItemDescription, m_VoucherItem.ShortName, m_VoucherItem.PrintName, m_VoucherItem.ReceiptName, m_VoucherItem.DisplayName, m_VoucherItem.MRP, m_VoucherItem.DistributorPrice, (m_VoucherItem.DistributorPrice - 1) / m_VoucherItem.DistributorPrice * 100, (m_VoucherItem.DistributorPrice - 1) * m_VoucherItem.Quantity, Convert.ToInt32(dtSetDiscountPrice.Rows[0]["KeyValue2"]) * m_VoucherItem.Quantity, m_VoucherItem.BusinessVolume, m_VoucherItem.PointValue, m_VoucherItem.Quantity);
                    m_price.GiftVoucherNumber = m_VoucherItem.VoucherCode;
                    m_price.VoucherSrNo = m_CurrentVoucherDistributor.VoucherSrNo;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteExceptionLog(ex);
            }
            /*if (Common.CountryID == "4")
            {
                m_price = new Price(Common.INT_DBNULL, m_VoucherItem.ItemID, m_VoucherItem.ItemCode, m_VoucherItem.ItemDescription, m_VoucherItem.ShortName, m_VoucherItem.PrintName, m_VoucherItem.ReceiptName, m_VoucherItem.DisplayName, m_VoucherItem.MRP, m_VoucherItem.DistributorPrice, 0, (m_VoucherItem.DistributorPrice - 1) * m_VoucherItem.Quantity, 0, m_VoucherItem.BusinessVolume, m_VoucherItem.PointValue, m_VoucherItem.Quantity);
            }
            else
            {
                m_price = new Price(Common.INT_DBNULL, m_VoucherItem.ItemID, m_VoucherItem.ItemCode, m_VoucherItem.ItemDescription, m_VoucherItem.ShortName, m_VoucherItem.PrintName, m_VoucherItem.ReceiptName, m_VoucherItem.DisplayName, m_VoucherItem.MRP, m_VoucherItem.DistributorPrice, (m_VoucherItem.DistributorPrice - 1) / m_VoucherItem.DistributorPrice * 100, (m_VoucherItem.DistributorPrice - 1) * m_VoucherItem.Quantity, 1 * m_VoucherItem.Quantity, m_VoucherItem.BusinessVolume, m_VoucherItem.PointValue, m_VoucherItem.Quantity);
            }*/
        }

        private void dgvGiftVoucherItem_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                m_VoucherItem = m_CurrentVoucher.VoucherItemDetailList[e.RowIndex];
                MinBuyAmount = m_CurrentVoucher.MinBuyAmount;
                SetPrice();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
