using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PurchaseComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

namespace PurchaseComponent.UI
{
    public partial class frmGRNBatch : Form
    {
        private const string CON_REASON_PARAM = "GRNREASONCODE";
        
        #region Variables Declaration
        // Variables for Rights Check    
        private const string BTN_ADD = "&Add";
        private const string BTN_EDIT = "&Edit";
        private const string CON_GRID_Remove = "Remove";
        private string GRIDVIEW_XML_PATH = Environment.CurrentDirectory + "\\App_Data\\Purchase.xml";
        private GrnDetail m_CurrentGRNDetail = null;
        private GrnBatchDetail m_CurrentBatch = null;
        CurrencyManager m_bindingMgr;
        DataGridView m_ParentGridView;
        private decimal m_MRP = 0;
        private int m_expiryMonth = 0;
        int m_SerialNo = 0;
        bool EnableControl = true;
        
        #endregion

        public frmGRNBatch(GrnDetail detail, DataGridView dgv, bool Editable)
        {
            try
            {
                InitializeComponent();
                m_CurrentGRNDetail = detail;
                m_ParentGridView = dgv;
                EnableControl = Editable;
                dtpExpiryDate.CustomFormat = Common.DTP_DATE_FORMAT;
                dtpMfgDate.CustomFormat = Common.DTP_DATE_FORMAT;
                //dt. 12-04-2012 Nitin has changed as per the requirement
               // dtpExpiryDate.Value = DateTime.Today.AddDays(1);
                dtpMfgDate.MaxDate = DateTime.Today;
                InitailizeControls();
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
                DataGridView dgvGRNItemsNew = Common.GetDataGridViewColumns(dgvBatchDetails, GRIDVIEW_XML_PATH);
                txtItemCode.Text = m_CurrentGRNDetail.ItemCode;
                CoreComponent.MasterData.BusinessObjects.ItemDetails itemDetail=new CoreComponent.MasterData.BusinessObjects.ItemDetails();
                itemDetail.ItemId=m_CurrentGRNDetail.ItemId;
                itemDetail.ItemCode = m_CurrentGRNDetail.ItemCode;
                List<CoreComponent.MasterData.BusinessObjects.ItemDetails> items = itemDetail.Search();
                List<CoreComponent.MasterData.BusinessObjects.ItemDetails> lstItem;
                if (items != null && items.Count > 0)
                {
                    var query = from a in items where a.ItemId == m_CurrentGRNDetail.ItemId select a;
                    lstItem = (List<CoreComponent.MasterData.BusinessObjects.ItemDetails>)query.ToList();
                    if (lstItem.Count > 0)
                    {
                        m_MRP = lstItem[0].DisplayMRP;
                        m_expiryMonth = lstItem[0].ExpiryDuration;
                    }
                }
                txtMRP.Text = m_MRP.ToString();
                
                //dtpExpiryDate.Value = DateTime.Today.AddMonths(m_expiryMonth);
                //dt. 12-04-2012 Nitin has changed as per the requirement
                dtpExpiryDate.Value = Convert.ToDateTime("01-01-1900");
                if (m_CurrentGRNDetail.GRNBatchDetailList != null && m_CurrentGRNDetail.GRNBatchDetailList.Count>0)
                {
                    var query = (from p in m_CurrentGRNDetail.GRNBatchDetailList select p.SerialNo).Max();
                    m_SerialNo = Convert.ToInt32(query) + 1;
                }
                else
                    m_SerialNo++;
                ResetGrid();
                btnAdd.Enabled = EnableControl;
                txtManuBatchNo.ReadOnly = !EnableControl;
                txtMRP.ReadOnly = !EnableControl;
                txtReceivedQty.ReadOnly = !EnableControl;
                dtpExpiryDate.Enabled = EnableControl;
                dtpMfgDate.Enabled = EnableControl;
                dgvBatchDetails.Columns[CON_GRID_Remove].Visible = EnableControl;


            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        private void ResetGrid()
        {
            try
            {
                dgvBatchDetails.DataSource = null;
                dgvBatchDetails.DataSource = new List<GrnBatchDetail>();
                if (m_CurrentGRNDetail.GRNBatchDetailList != null && m_CurrentGRNDetail.GRNBatchDetailList.Count>0)
                {
                    dgvBatchDetails.DataSource = m_CurrentGRNDetail.GRNBatchDetailList;
                    dgvBatchDetails.ClearSelection();
                    m_bindingMgr = (CurrencyManager)this.BindingContext[m_CurrentGRNDetail.GRNBatchDetailList];
                    m_bindingMgr.Refresh();
                }
                dgvBatchDetails.ClearSelection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void ResetValues(GrnBatchDetail batch)
        {
            try
            {
                if (batch != null)
                {
                    txtManuBatchNo.Text = batch.ManufacturerBatchNumber.ToString();
                    txtMRP.Text = batch.DisplayMRP.ToString();
                    txtReceivedQty.Text = batch.DisplayReceivedQty.ToString();
                    dtpExpiryDate.Value = Convert.ToDateTime(batch.ExpiryDate);
                    dtpMfgDate.Value = Convert.ToDateTime(batch.ManufacturingDate);
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
        
        private GrnBatchDetail CopyObject(GrnBatchDetail objbatch)
        {
            try
            {
                GrnBatchDetail batch = new GrnBatchDetail();
                batch.BatchNumber = objbatch.BatchNumber;
                batch.ExpiryDate = objbatch.ExpiryDate;
                batch.GRNNo = objbatch.GRNNo;
                batch.ItemId = objbatch.ItemId;
                batch.ManufacturerBatchNumber = objbatch.ManufacturerBatchNumber;
                batch.ManufacturingDate = objbatch.ManufacturingDate;
                batch.MRP = objbatch.MRP;
                batch.ReceivedQty = objbatch.ReceivedQty;
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
                sbError=GenerateAddError();
                if (sbError.ToString().Trim().Equals(string.Empty))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Add"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        GrnBatchDetail batch = new GrnBatchDetail();
                        batch.ManufacturerBatchNumber = txtManuBatchNo.Text.Trim();
                        batch.ExpiryDate = dtpExpiryDate.Value.ToShortDateString();
                        batch.GRNNo = m_CurrentGRNDetail.GRNNo;
                        batch.ItemId = m_CurrentGRNDetail.ItemId;
                        batch.ManufacturingDate = dtpMfgDate.Value.ToShortDateString();
                        batch.MRP = Convert.ToDouble(txtMRP.Text.Trim());
                        batch.ReceivedQty = Convert.ToDouble(txtReceivedQty.Text.Trim());
                        batch.SerialNo = m_SerialNo++;

                        if (m_CurrentGRNDetail.GRNBatchDetailList == null)
                            m_CurrentGRNDetail.GRNBatchDetailList = new List<GrnBatchDetail>();

                        m_CurrentGRNDetail.GRNBatchDetailList.Add(batch);
                        m_CurrentGRNDetail.ChallanQty = m_CurrentGRNDetail.ReceivedQty;
                        m_CurrentGRNDetail.InvoiceQty = m_CurrentGRNDetail.ReceivedQty;

                        ResetGrid();
                        m_ParentGridView.Refresh();
                        ClearItem();
                        MessageBox.Show(Common.GetMessage("INF0055", "Batch", "Added"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        
        private void EditItem()
        {
            try
            {
                ValidateEdit();
                StringBuilder sbError;
                sbError=GenerateAddError();
                if (sbError.ToString().Trim().Equals(string.Empty))
                {
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", "Edit"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.Yes)
                    {
                        m_CurrentBatch.ExpiryDate = dtpExpiryDate.Value.ToShortDateString();
                        m_CurrentBatch.ManufacturingDate = dtpMfgDate.Value.ToShortDateString();
                        m_CurrentBatch.MRP = Convert.ToDouble(txtMRP.Text.Trim());
                        m_CurrentBatch.ReceivedQty = Convert.ToDouble(txtReceivedQty.Text.Trim());
                        m_CurrentBatch.ManufacturerBatchNumber = txtManuBatchNo.Text.Trim();

                        m_CurrentGRNDetail.ChallanQty = m_CurrentGRNDetail.ReceivedQty;
                        m_CurrentGRNDetail.InvoiceQty = m_CurrentGRNDetail.ReceivedQty;

                        ResetGrid();
                        m_ParentGridView.Refresh();
                        ClearItem();
                        MessageBox.Show(Common.GetMessage("INF0055", "Batch", "Updated"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        
        private void ClearItem()
        {
            try
            {
                errorAdd.Clear();
                txtManuBatchNo.Text = string.Empty;
                txtMRP.Text =EnableControl? m_MRP.ToString():string.Empty;
                txtReceivedQty.Text = string.Empty;
                //dtpExpiryDate.Value = DateTime.Today.AddDays(1);
                
                //dt. 12-04-2012 Nitin has changed as per the requirement 
                //dtpExpiryDate.Value = DateTime.Today.AddMonths(m_expiryMonth);
                dtpExpiryDate.Value = Convert.ToDateTime( "01-01-1900");
                dtpMfgDate.Value = DateTime.Today;
                btnAdd.Text = BTN_ADD;
                dgvBatchDetails.ClearSelection();
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
                    m_CurrentGRNDetail.GRNBatchDetailList.RemoveAt(index);                   
                    ResetGrid();
                    m_CurrentGRNDetail.ChallanQty = m_CurrentGRNDetail.ReceivedQty;
                    m_CurrentGRNDetail.InvoiceQty = m_CurrentGRNDetail.ReceivedQty;
                    m_ParentGridView.Refresh();
                    ClearItem();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        /// <summary>
        /// 1.  Validate Entry
        /// 2.  Add new object of BATCHGRN in Existing list of GRNDetail Object
        /// 3.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        #region Event 

        #region Button
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //ValidateAdd();
                //StringBuilder sbError;
                //sbError=GenerateAddError();
                //if(sbError.ToString().Trim().Equals(string.Empty))
                //{
                    if (btnAdd.Text.Trim() == BTN_ADD)
                        AddItem();
                    else
                        EditItem();
                //}
                //else
                //{
                //    MessageBox.Show(sbError.ToString(), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }            
        }

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        #endregion

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
                    m_CurrentBatch = m_CurrentGRNDetail.GRNBatchDetailList[dgvBatchDetails.SelectedRows[0].Index];
                    GrnBatchDetail batch = CopyObject(m_CurrentBatch);
                    ResetValues(batch);
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        #endregion

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
                 ValidateRequiredTextField(txtMRP, lblMRP);
                 // Manu Batch No Blank Check
                 ValidateRequiredTextField(txtManuBatchNo,lblManuBatchNo);
                 // Manu date Blank check               
                 ValidateDates(dtpMfgDate, lblMfgDate, dtpExpiryDate, lblExpiryDate);
                 Label lbl=new Label();
                 lbl.Text="Today Date";
                 if (m_CurrentGRNDetail.GRNBatchDetailList != null)
                 {
                     var query = from q in m_CurrentGRNDetail.GRNBatchDetailList
                                 where
                                     q.DisplayExpiryDate == dtpExpiryDate.Value.ToString(Common.DTP_DATE_FORMAT)
                                     && q.ManufacturerBatchNumber == txtManuBatchNo.Text.Trim()
                                     && q.DisplayManufacturingDate == dtpMfgDate.Value.ToString(Common.DTP_DATE_FORMAT)
                                     && q.MRP.ToString() == txtMRP.Text.Trim()
                                 select q;
                     if (query != null && query.ToList().Count > 0)
                     {
                         errorAdd.SetError(dgvBatchDetails, Common.GetMessage("INF0205"));
                         return;
                     }
                 }

                 // Valid Recieved Qty
                 ValidateRecievedQty();
                 if (!Validators.IsValidAmount(txtMRP.Text.Trim()))
                 {
                     errorAdd.SetError(txtMRP, Common.GetMessage("INF0010", lblMRP.Text.Trim().Substring(0, lblMRP.Text.Trim().Length - 2)));
                 }
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
                ValidateRequiredTextField(txtMRP, lblMRP);
                // Manu Batch No Blank Check
                ValidateRequiredTextField(txtManuBatchNo, lblManuBatchNo);
                // Manu date Blank check               
                ValidateDates(dtpMfgDate, lblMfgDate, dtpExpiryDate, lblExpiryDate);
                Label lbl = new Label();
                lbl.Text = "Today Date";
                //if (m_CurrentGRNDetail.GRNBatchDetailList != null)
                //{
                //    var query = from q in m_CurrentGRNDetail.GRNBatchDetailList
                //                where
                //                    q.DisplayExpiryDate == dtpExpiryDate.Value.ToString(Common.DTP_DATE_FORMAT)
                //                    && q.ManufacturerBatchNumber == txtManuBatchNo.Text.Trim()
                //                    && q.DisplayManufacturingDate == dtpMfgDate.Value.ToString(Common.DTP_DATE_FORMAT)
                //                    && q.MRP.ToString() == txtMRP.Text.Trim()
                //                select q;
                //    if (query != null && query.ToList().Count > 0)
                //    {
                //        errorAdd.SetError(dgvBatchDetails, Common.GetMessage("INF0205"));
                //        return;
                //    }
                //}

                // Valid Recieved Qty
                ValidateRecievedQty();
                if (!Validators.IsValidAmount(txtMRP.Text.Trim()))
                {
                    errorAdd.SetError(txtMRP, Common.GetMessage("INF0010", lblMRP.Text.Trim().Substring(0, lblMRP.Text.Trim().Length - 2)));
                }
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
                if (errorAdd.GetError(txtMRP).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(txtMRP));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtMRP.Focus();
                        focus = true;
                    }
                }
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
                if (errorAdd.GetError(txtReceivedQty).Trim().Length > 0)
                {
                    sbError.Append(errorAdd.GetError(txtReceivedQty));
                    sbError.AppendLine();
                    if (!focus)
                    {
                        txtReceivedQty.Focus();
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

        private void ValidateRecievedQty()
        {
            try
            {
                ValidateRequiredTextField(txtReceivedQty, lblReceivedQty);
                ValidateNotZeroField(txtReceivedQty, lblReceivedQty);
                if (errorAdd.GetError(txtReceivedQty).Trim().Equals(string.Empty))
                {
                    if (Validators.IsValidQuantity(txtReceivedQty.Text.Trim()))
                    {
                        double currentQty = 0;
                        if (m_CurrentBatch != null)
                        {
                           currentQty = m_CurrentBatch.ReceivedQty;
                        }
                        if (m_CurrentGRNDetail.AlreadyReceivedQty + m_CurrentGRNDetail.ReceivedQty - currentQty + Convert.ToDouble(txtReceivedQty.Text.Trim()) > m_CurrentGRNDetail.MaxQty)
                        {
                           double qty = m_CurrentGRNDetail.MaxQty - (Convert.ToDouble(m_CurrentGRNDetail.AlreadyReceivedQty + m_CurrentGRNDetail.ReceivedQty - currentQty));
                           errorAdd.SetError(txtReceivedQty, Common.GetMessage("VAL0055", Math.Round(qty, Common.DBQtyRounding).ToString()));                        
                        }
                    }
                    else
                    {
                        errorAdd.SetError(txtReceivedQty, Common.GetMessage("INF0010", lblReceivedQty.Text.Trim().Substring(0, lblReceivedQty.Text.Trim().Length - 2)));
                    }
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
                    if(!isValid)
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
        private void ValidateDates(DateTimePicker From,Label lblFrom, DateTimePicker To, Label lblTo)
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

        private void frmGRNBatch_Load(object sender, EventArgs e)
        {

        }

        
    }
}
