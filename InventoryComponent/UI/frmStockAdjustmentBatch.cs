using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using CoreComponent.Controls;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.Hierarchies.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;
using System.Collections.Specialized;
using AuthenticationComponent.BusinessObjects;
using InventoryComponent.BusinessObjects;
//using CoreComponent.BusinessObjects;


namespace InventoryComponent.UI
{
    public partial class frmStockAdjustmentBatch : Form
    {
        public static string Itemcode
        {
            get;
            set;
        }

        public static int CurrentSerialno
        {
            get;
            set;
        }
        private object m_returnObject;

        public object ReturnObject
        {
            get { return m_returnObject; }
            set { m_returnObject = value; }
        }
        public frmStockAdjustmentBatch()
        {
            try
            {
                InitializeComponent();
                //m_CurrentGRNDetail = detail;
                bool Editable = true;
                EnableControl = Editable;
                dtpExpiryDate.CustomFormat = Common.DTP_DATE_FORMAT;
                dtpMfgDate.CustomFormat = Common.DTP_DATE_FORMAT;
                // dtpExpiryDate.Value = DateTime.Today.AddDays(1);
                dtpMfgDate.MaxDate = DateTime.Today;

                //ItemDetail.GRNBatchDetailList;
                InitailizeControls();
                txtManuBatchNo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        private void InitailizeControls()
        {
            try
            {
                
                // Create GridView
                dgvBatchDetails.AutoGenerateColumns = false;
                dgvBatchDetails.DataSource = null;
                DataGridView dgvItemsNew = Common.GetDataGridViewColumns(dgvBatchDetails, GRIDVIEW_XML_PATH);

                dgvBatchDetails.AutoGenerateColumns = false;
              
                dgvBatchDetails.DataSource = null;


                CoreComponent.MasterData.BusinessObjects.ItemDetails itemDetail = new CoreComponent.MasterData.BusinessObjects.ItemDetails();
                ItemDetails objnew = new ItemDetails();
                //itemDetail.ItemId = objnew.ToItemId;
                itemDetail.ItemCode = Itemcode.ToUpper();
                List<CoreComponent.MasterData.BusinessObjects.ItemDetails> items = itemDetail.Search();
                List<CoreComponent.MasterData.BusinessObjects.ItemDetails> lstItem;
                if (items != null && items.Count > 0)
                {
                    var query = from a in items where a.ItemCode.ToUpper() == Itemcode.ToUpper() select a;
                    lstItem = (List<CoreComponent.MasterData.BusinessObjects.ItemDetails>)query.ToList();
                    if (lstItem.Count > 0)
                    {

                        m_expiryMonth = lstItem[0].ExpiryDuration;
                    }
                }

                dtpExpiryDate.Value = DateTime.Today.AddMonths(m_expiryMonth);
                if (ItemInventory.BatchDetailList != null && ItemInventory.BatchDetailList.Count > 0)
                {
                    var query = (from p in ItemInventory.BatchDetailList select p.SerialNo).Max();
                    m_SerialNo = Convert.ToInt32(query) + 1;
                }
                else
                    m_SerialNo++;



                if (ItemInventory.BatchDetailList == null || ItemInventory.BatchDetailList.Count == 0)
                {
                    dgvBatchDetails.AutoGenerateColumns = false;
                    dgvBatchDetails.DataSource = null;
                    dgvBatchDetails.DataSource = new List<ItemBatchDetails>();
                }
                ResetGrid();

                btnAdd.Enabled = EnableControl;
                txtManuBatchNo.ReadOnly = !EnableControl;
                dtpExpiryDate.Enabled = EnableControl;
                dtpMfgDate.Enabled = EnableControl;
                dgvBatchDetails.Columns[CON_GRID_Remove].Visible = EnableControl;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
        private const string CON_REASON_PARAM = "REASONCODE";

        #region Variables Declaration
        // Variables for Rights Check    
        private const string BTN_ADD = "&Add";
        private const string BTN_EDIT = "&Edit";
        private const string CON_GRID_Remove = "Remove";
        private string GRIDVIEW_XML_PATH = Environment.CurrentDirectory + "\\App_Data\\Inventory.xml";
        //private ItemBatchDetails m_CurrentDetail = null;
        private ItemBatchDetails m_CurrentBatch = null;
        CurrencyManager m_bindingMgr;
        //DataGridView m_ParentGridView;
        private static int  m_expiryMonth = 0;
        int m_SerialNo = -1;
        bool EnableControl = true;

        #endregion
        #region

        private void ClearItem()
        {
            try
            {
                errorAdd.Clear();
                txtManuBatchNo.Text = string.Empty;

                dtpExpiryDate.Value = DateTime.Today.AddDays(1);
                dtpMfgDate.Value = DateTime.Today;
                btnAdd.Text = BTN_ADD;
                //dgvBatchDetails.ClearSelection();
                m_CurrentBatch = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Remove(int index)
        {
            try
            {
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Remove"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    ItemInventory.BatchDetailList.RemoveAt(index);

                    dgvBatchDetails.DataSource = new List<ItemBatchDetails>();
                    //ResetGrid();

                    ////m_ParentGridView.Refresh();
                    ClearItem();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void ResetGrid()
        {
            try
            {
                dgvBatchDetails.DataSource = null;
                //dgvBatchDetails.DataSource = new List<ItemBatchDetails>();
                if (ItemInventory.BatchDetailList != null && ItemInventory.BatchDetailList.Count > 0)
                {

                    var query = from a in ItemInventory.BatchDetailList where a.ToItemCode.ToUpper()== Itemcode.ToUpper() select a;
                    if (query.Count(c => c.ToItemCode == Itemcode) > 0)
                    {
                        List<CoreComponent.MasterData.BusinessObjects.ItemBatchDetails> lstItem;
                        lstItem = (List<CoreComponent.MasterData.BusinessObjects.ItemBatchDetails>)query.ToList();

                        
                        //int s = 0;
                        foreach (ItemBatchDetails cn in lstItem)
                        {
                            CurrentSerialno = cn.SerialNo;
                            if (dgvBatchDetails.Rows.Count==0)
                                dgvBatchDetails.Rows.Add();
                            //int s = cn.SerialNo;
                            //dgvBatchDetails.Rows[0].Cells[0].Value = " X ";
                            dgvBatchDetails.Rows[0].Cells[1].Value = lstItem[0].ManufactureBatchNo;
                            dgvBatchDetails.Rows[0].Cells[2].Value = lstItem[0].DisplayManufacure;
                            dgvBatchDetails.Rows[0].Cells[3].Value = lstItem[0].DisplayExpiry;
                                //s++;
                        }

                    }
                    //else if()
                    //{
                        //foreach (ItemBatchDetails cn in ItemDetail.GRNBatchDetailList)
                        //{
                        //    int s = cn.SerialNo - 1;
                        //    dgvBatchDetails.Rows[s].Cells[1].Value = ItemDetail.GRNBatchDetailList[0].ManufactureBatchNo;
                        //    dgvBatchDetails.Rows[s].Cells[2].Value = ItemDetail.GRNBatchDetailList[0].DisplayMfgDate;
                        //    dgvBatchDetails.Rows[s].Cells[3].Value = ItemDetail.GRNBatchDetailList[0].DisplayExpDate;
                        //}
                        dgvBatchDetails.ClearSelection();
                        m_bindingMgr = (CurrencyManager)this.BindingContext[ItemInventory.BatchDetailList];
                        m_bindingMgr.Refresh();
                    
                }
                dgvBatchDetails.ClearSelection();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void ResetValues(ItemBatchDetails batch)
        {
            try
            {
                if (batch != null)
                {
                    txtManuBatchNo.Text = batch.ManufactureBatchNo.ToString();
                    dtpExpiryDate.Value = Convert.ToDateTime(batch.Expiry);
                    //dtpMfgDate.Value = Convert.ToDateTime(batch.MfgDate);
                    btnAdd.Text = BTN_EDIT;
                }
                else
                    ClearItem();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private void EditItem()
        {
            try
            {
                ValidateEdit();
                StringBuilder sbError;
                sbError = GenerateAddError();
                if (sbError.ToString().Trim().Equals(string.Empty))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Edit"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        if (ItemInventory.BatchDetailList != null && ItemInventory.BatchDetailList.Count > 0)
                        {
                            var query = from a in ItemInventory.BatchDetailList where a.ToItemCode.ToUpper() == Itemcode.ToUpper() select a;
                            List<CoreComponent.MasterData.BusinessObjects.ItemBatchDetails> lstItem;
                            lstItem = (List<CoreComponent.MasterData.BusinessObjects.ItemBatchDetails>)query.ToList();
                            //int s = 0;
                            foreach (ItemBatchDetails cn in lstItem)
                            {
                                CurrentSerialno = cn.SerialNo;
                                int s = cn.SerialNo;

                                ItemInventory.BatchDetailList[s].ItemCode = Itemcode.Trim().ToString();
                                ItemInventory.BatchDetailList[s].ExpDate = dtpExpiryDate.Value.ToShortDateString();
                                ItemInventory.BatchDetailList[s].MfgDate = dtpMfgDate.Value.ToShortDateString();
                                ItemInventory.BatchDetailList[s].ManufactureBatchNo = txtManuBatchNo.Text.Trim();
                                ItemInventory.BatchDetailList[s].DisplayExpDate = dtpExpiryDate.Value.ToString(Common.DTP_DATE_FORMAT);

                                ItemInventory.BatchDetailList[s].DisplayMfgDate = dtpMfgDate.Value.ToString(Common.DTP_DATE_FORMAT);
                            }
                            ResetGrid();
                            //m_ParentGridView.Refresh();
                            ClearItem();
                            MessageBox.Show(Common.GetMessage("INF0055", "Batch", "Updated"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }   
                    }
                }
                else
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private ItemBatchDetails CopyObject(ItemBatchDetails objbatch)
        {
            try
            {

                ItemBatchDetails batch = new ItemBatchDetails();
                batch.ToItemCode= objbatch.ToItemCode;
                batch.Expiry = objbatch.Expiry;
                batch.ManufactureBatchNo= objbatch.ManufactureBatchNo;
                batch.Manufacure = objbatch.Manufacure;
                batch.SerialNo = objbatch.SerialNo;
                return batch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AddItem()
        {
            try
            {
                ValidateAdd();
                StringBuilder sbError;
                sbError = GenerateAddError();
                if (sbError.ToString().Trim().Equals(string.Empty))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Add"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        if (ItemInventory.BatchDetailList != null && ItemInventory.BatchDetailList.Count > 0)
                        {
                            var query = from a in ItemInventory.BatchDetailList where a.ToItemCode.ToUpper() == Itemcode.ToUpper() select a;
                            if (query.Count(c => c.ToItemCode == Itemcode) > 0)
                            {
                                MessageBox.Show(Common.GetMessage("INF0227"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (query.Count(c => c.ToItemCode == Itemcode)== 0)
                            {
                                
                            ItemBatchDetails batch = new ItemBatchDetails();                                
                            batch.ToItemCode = Itemcode.Trim().ToString();
                            batch.ManufactureBatchNo = txtManuBatchNo.Text.Trim();
                            batch.Expiry = dtpExpiryDate.Value.ToShortDateString();
                            batch.Manufacure = dtpMfgDate.Value.ToShortDateString();
                            //batch.DisplayMfgDate = dtpMfgDate.Value.ToString(Common.DTP_DATE_FORMAT);
                            batch.SerialNo = m_SerialNo++;

                            if (ItemInventory.BatchDetailList == null)
                                ItemInventory.BatchDetailList = new List<ItemBatchDetails>();

                            ItemInventory.BatchDetailList.Add(batch);


                            ResetGrid();
                            //m_ParentGridView.Refresh();
                            ClearItem();
                            MessageBox.Show(Common.GetMessage("INF0055", "Batch", "Added"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            
                        }
                            
                        else
                        {
                            ItemBatchDetails batch = new ItemBatchDetails();
                            batch.ToItemCode = Itemcode.Trim().ToString();
                            batch.ManufactureBatchNo = txtManuBatchNo.Text.Trim();
                            batch.Expiry = dtpExpiryDate.Value.ToShortDateString();
                            //batch.DisplayExpDate = dtpExpiryDate.Value.ToString(Common.DTP_DATE_FORMAT); 
                            //batch.ItemId = m_CurrentGRNDetail.ItemId;
                            batch.Manufacure = dtpMfgDate.Value.ToShortDateString();
                            //batch.DisplayMfgDate = dtpMfgDate.Value.ToString(Common.DTP_DATE_FORMAT);
                            batch.SerialNo = m_SerialNo++;

                            if (ItemInventory.BatchDetailList == null)
                                ItemInventory.BatchDetailList = new List<ItemBatchDetails>();

                            ItemInventory.BatchDetailList.Add(batch);


                            ResetGrid();
                            //m_ParentGridView.Refresh();
                            ClearItem();
                            MessageBox.Show(Common.GetMessage("INF0055", "Batch", "Added"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(sbError.ToString(), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearItem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                //frmInventoryAdjustment obj = new frmInventoryAdjustment();
                //obj.txtToBatchNo.Text = (ItemDetail.GRNBatchDetailList[0].ManufactureBatchNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (btnAdd.Text.Trim() == BTN_ADD)
                    AddItem();

                else
                    EditItem();

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }  
        }

        #region GridView

        private void dgvBatchDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (dgvBatchDetails.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell) && dgvBatchDetails.Columns[e.ColumnIndex].Name == "Remove")
                    {
                        Remove(e.RowIndex);
                    }
                    //m_returnObject = dgvBatchDetails.SelectedRows[0].DataBoundItem;
                    //DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        private void dgvBatchDetails_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvBatchDetails.SelectedRows.Count > 0)
                {
                    errorAdd.Clear();
                    if (ItemInventory.BatchDetailList != null && ItemInventory.BatchDetailList.Count > 0)
                    {
                        var query = from a in ItemInventory.BatchDetailList where a.ToItemCode.ToUpper() == Itemcode.ToUpper() select a;
                        List<CoreComponent.MasterData.BusinessObjects.ItemBatchDetails> lstItem;
                        lstItem = (List<CoreComponent.MasterData.BusinessObjects.ItemBatchDetails>)query.ToList();

                        foreach (ItemBatchDetails cn in lstItem)
                        {
                            CurrentSerialno = cn.SerialNo;
                            int s = cn.SerialNo;

                            m_CurrentBatch = ItemInventory.BatchDetailList[0];
                        }
                        ItemBatchDetails batch = CopyObject(m_CurrentBatch);

                        ResetValues(batch);
                        //m_returnObject = dgvBatchDetails.SelectedRows[0].DataBoundItem;
                        //DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        #endregion

        #region Validation
        /// <summary>
        /// 1. RecievedQty should be greater than 0
        /// 2. Man batch no Not blank
        /// 3. Expiry date and Manu date should not be current date
        /// 4. Expiry date shuld not be less than man date
        /// 5. Alreadyrecieved+ Recieve qty should not be greater than max qty
        /// 2. MRP should not be blank
        /// </summary>
        /// <returns></returns>
        private void ValidateAdd()
        {
            try
            {
                errorAdd.Clear();
                // MRP Blank check
        
                // Manu Batch No Blank Check
                ValidateRequiredTextField(txtManuBatchNo, lblManuBatchNo);
                // Manu date Blank check               
                ValidateDates(dtpMfgDate, lblMfgDate, dtpExpiryDate, lblExpiryDate);
                Label lbl = new Label();
                lbl.Text = "Today Date";
                if (ItemInventory.BatchDetailList != null)
                {
                    var query = from q in ItemInventory.BatchDetailList
                                where
                                    q.Expiry == dtpExpiryDate.Value.ToString(Common.DTP_DATE_FORMAT)
                                    && q.ManufactureBatchNo == txtManuBatchNo.Text.Trim()
                                    && q.Manufacure == dtpMfgDate.Value.ToString(Common.DTP_DATE_FORMAT)

                                select q;
                    if (query != null && query.ToList().Count > 0)
                    {
                        errorAdd.SetError(dgvBatchDetails, Common.GetMessage("INF0205"));
                        return;
                    }
                }

                // Valid Recieved Qty

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void ValidateEdit()
        {
            try
            {
                errorAdd.Clear();
                // MRP Blank check
               
                // Manu Batch No Blank Check
                ValidateRequiredTextField(txtManuBatchNo, lblManuBatchNo);
                // Manu date Blank check               
                ValidateDates(dtpMfgDate, lblMfgDate, dtpExpiryDate, lblExpiryDate);
                Label lbl = new Label();
                lbl.Text = "Today Date";

              
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private StringBuilder GenerateAddError()
        {
            try
            {
                bool focus = false;
                StringBuilder sbError = new StringBuilder();

                if (errorAdd.GetError(txtManuBatchNo).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(txtManuBatchNo));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtManuBatchNo.Focus();
                        focus = true;
                    }
                }
                if (errorAdd.GetError(dtpMfgDate).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(dtpMfgDate));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        dtpMfgDate.Focus();
                        focus = true;
                    }
                }
                if (errorAdd.GetError(dtpExpiryDate).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(dtpExpiryDate));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        dtpExpiryDate.Focus();
                        focus = true;
                    }
                }

                if (errorAdd.GetError(dgvBatchDetails).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(dgvBatchDetails));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        dgvBatchDetails.Focus();
                        focus = true;
                    }
                }
                sbError = Common.ReturnErrorMessage(sbError);
                return sbError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) || ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) && (!e.Shift)) || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Alt || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Shift || e.KeyCode == Keys.Home || e.KeyCode == Keys.End || e.KeyCode == Keys.Decimal))
                {
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidateRequiredTextField(TextBox txt, Label lbl)
        {
            try
            {
                errorAdd.SetError(txt, string.Empty);
                bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Trim().Length);
                if (isTextBoxEmpty == true)
                {
                    errorAdd.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

 
        private void ValidateRequiredDateField(DateTimePicker dtp, Label lbl)
        {
            try
            {
                errorAdd.SetError(dtp, string.Empty);
                if (dtp.Value.ToShortDateString() == Common.DATETIME_CURRENT.ToShortDateString())
                {
                    errorAdd.SetError(dtp, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ValidateNotZeroField(TextBox txt, Label lbl)
        {
            try
            {
                errorAdd.SetError(txt, string.Empty);
                bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Trim().Length);
                if (isTextBoxEmpty == true)
                {
                    errorAdd.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                }
                else
                {
                    bool isValid = CoreComponent.Core.BusinessObjects.Validators.IsGreaterThanZero(txt.Text.Trim());
                    if (!isValid)
                        errorAdd.SetError(txt, Common.GetMessage("INF0010", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ValidateLessThanZeroField(TextBox txt, Label lbl)
        {
            try
            {
                errorAdd.SetError(txt, string.Empty);
                bool isTextBoxEmpty = CoreComponent.Core.BusinessObjects.Validators.CheckForEmptyString(txt.Text.Trim().Length);
                if (isTextBoxEmpty == true)
                {
                    errorAdd.SetError(txt, Common.GetMessage("INF0019", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                }
                else
                {
                    bool isValid = CoreComponent.Core.BusinessObjects.Validators.IsLessThanZero(txt.Text.Trim());
                    if (isValid)
                        errorAdd.SetError(txt, Common.GetMessage("INF0010", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void ValidateDates(DateTimePicker From, Label lblFrom, DateTimePicker To, Label lblTo)
        {
            try
            {
                if (Convert.ToDateTime(To.Value.ToShortDateString()) < From.Value.AddMonths(m_expiryMonth))
                {
                    //errorAdd.SetError(To, "Expiry Duration should be atleast "+ m_expiryMonth +" Months");
                    errorAdd.SetError(To, Common.GetMessage("VAL0124", m_expiryMonth.ToString()));
                }
                else if (Convert.ToDateTime(To.Value.ToShortDateString()) < DateTime.Today.AddDays(1))
                {
                    errorAdd.SetError(To, Common.GetMessage("INF0098", lblTo.Text.Trim().Substring(0, lblTo.Text.Trim().Length - 2), "Today"));
                }
                else
                {
                    int days = DateTime.Compare(Convert.ToDateTime(From.Value.ToShortDateString()), Convert.ToDateTime(To.Value.ToShortDateString()));
                    if (days == 1)
                    {
                        errorAdd.SetError(From, Common.GetMessage("INF0034", lblFrom.Text.Trim().Substring(0, lblFrom.Text.Trim().Length - 2), lblTo.Text.Trim().Substring(0, lblTo.Text.Trim().Length - 2)));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void dtpMfgDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtpExpiryDate.Value = dtpMfgDate.Value.AddMonths(m_expiryMonth);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            SetSelection();
        }

        private void SetSelection()
        {
            if (dgvBatchDetails.Rows.Count > 0 && dgvBatchDetails.SelectedRows[0].Index >= 0)
            {
                m_returnObject = dgvBatchDetails.SelectedRows[0].DataBoundItem;
                DialogResult = DialogResult.OK;
                //this.Close();
            }
        }
        private void CheckAndSetSelection()
        {
            if (dgvBatchDetails.Rows.Count > 0 && dgvBatchDetails.SelectedRows[0].Index >= 0)
            {
                SetSelection();
            }
        }

        private void dgvBatchDetails_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CheckAndSetSelection();
        }
    }
}
