using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using PackUnpackComponent.BusinessObjects;

namespace PackUnpackComponent.UI
{
    public partial class frmBatchDetails : Form
    {
        #region Variables
        private List<BatchDetails> m_BatchList = null;
        private List<BatchDetails> m_originalBatchList = null;
        private int m_TotalUnPackQty = 0;
        public const String GRID_BATCHNO_COL = "BatchNo";
        private CompositeBOM m_CompositeBOM;
        private int m_RequestedQty;
        #endregion

        #region Constructors
        public frmBatchDetails()
        {
            InitializeComponent();
            InitializeControls();
        }

        public frmBatchDetails(CompositeBOM objCompositeBOM)
        {
            InitializeComponent();
     

            m_CompositeBOM = objCompositeBOM;
            m_originalBatchList = objCompositeBOM.ListAllBatchDetails;
            m_BatchList = objCompositeBOM.ListSelectedBatchDetails;
            m_TotalUnPackQty = objCompositeBOM.TotalQty;
            InitializeControls();

            dgvBatchDetails.DataSource = null;
          
        }

        #endregion
        #region Events
        private void frmBatchDetails_Load(object sender, EventArgs e)
        {
            try
            {
                txtBatchUnPackQty.Focus();
                BindGrid();
            }
            catch (Exception ex)
            {


                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                CallValidations();

                String errMessage = GetErrorMessages();
                if (errMessage.Length > 0)
                {
                    MessageBox.Show(errMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                m_RequestedQty = Convert.ToInt32(txtBatchUnPackQty.Text.Trim());
                if (CheckIfRecordExists(Convert.ToString(cmbMfgBatch.SelectedValue)))
                {
                    DialogResult result;
                    result = MessageBox.Show(Common.GetMessage("INF0063"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        RemoveRecord(Convert.ToString(cmbMfgBatch.SelectedValue));
                        AddRecords(Convert.ToString(cmbMfgBatch.SelectedValue));

                    }
                }
                else
                {
                    AddRecords(Convert.ToString(cmbMfgBatch.SelectedValue));
                }

            
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_BatchList != null && m_BatchList.Count > 0)
                {


                    if (IsTotalRequestedQtyLessThanTotalQty(0))
                    {
                        DialogResult result = MessageBox.Show(Common.GetMessage("INF0064"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            this.Close();
                        }
                        else
                        {
                            return;
                        }
                        
                    }
                    else
                    {

                        this.Close();
                    }

                }

                this.Close();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Remove items from grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBatchDetail_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    if (dgvBatchDetails.Columns[e.ColumnIndex].CellType == typeof(DataGridViewImageCell))
                    {
                        string BatchNo = Convert.ToString(dgvBatchDetails.Rows[e.RowIndex].Cells[frmBatchDetails.GRID_BATCHNO_COL].Value);

                        if (BatchNo == string.Empty)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0010", "Batch Detail"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        DialogResult result = MessageBox.Show(Common.GetMessage("INF0028"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            RemoveRecord(BatchNo);
                           
                            
                        }
                        return;
                    }

               
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        #endregion

        #region Methods
        void InitializeControls()
        {
            try
            {
                //Bind Mfg Combo
                lblDisplayItemCode.Text = m_CompositeBOM.ItemCode;
                lblDisplayTotalUnpackQty.Text =Convert.ToString(m_CompositeBOM.TotalQty);

                cmbMfgBatch.DataSource = m_CompositeBOM.ListAllBatchDetails;
                cmbMfgBatch.DisplayMember = "MfgBatchNo";
                cmbMfgBatch.ValueMember = "BatchNo";

                cmbMfgBatch.SelectedIndex = 0;
                cmbMfgBatch_SelectedIndexChanged(null, null);

                //Get Columns for DataGridView
                DataGridView dgv = Common.GetDataGridViewColumns(dgvBatchDetails, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition_PUVoucherSearch.xml");
                dgvBatchDetails.AutoGenerateColumns = false;
                dgvBatchDetails.AllowUserToAddRows = false;
                dgvBatchDetails.AllowUserToDeleteRows = false;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvBatchDetails.ReadOnly = true;

             
                ResetControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Remove batchdetail onject from BatchDetail List
        /// </summary>
        /// <param name="BatchNo"></param>
        void RemoveRecord(string BatchNo)
        {
          
            BatchDetails RemoveBatchDetails;
            RemoveBatchDetails = (from u in m_BatchList where (u.BatchNo.Equals(BatchNo, StringComparison.CurrentCultureIgnoreCase)) select u).FirstOrDefault();

            if (RemoveBatchDetails != null)
                m_BatchList.Remove(RemoveBatchDetails);

            BindGrid();
        }

        void ResetControls()
        {
            try
            {
                cmbMfgBatch.SelectedIndex = 0;
                txtBatchUnPackQty.Text = string.Empty;
                 Validators.SetErrorMessage(errBatchDetails, txtBatchUnPackQty);
                 //Validators.SetErrorMessage(errBatchDetails, cmbMfgBatch);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


/// <summary>
/// To check requested quantity against quantity in case
/// if less message prompt to user before existing from 
/// window
/// </summary>
/// <param name="RequestedBatchQty"></param>
/// <returns></returns>
        private Boolean IsTotalRequestedQtyLessThanTotalQty(int RequestedBatchQty)
        {
            try
            {
                int TotalRequestQty = RequestedBatchQty;

             

                    foreach (BatchDetails objBatchDetails in m_BatchList)
                    {
                        TotalRequestQty = TotalRequestQty + objBatchDetails.RequestedQty;
                    }
                    if (TotalRequestQty < m_TotalUnPackQty)
                    {
                        return true;
                    }
                
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Before adding to grid check requested quantity is not 
        /// greater than total quantity in pack
        /// </summary>
        /// <param name="RequestedBatchQty"></param>
        /// <returns></returns>
        private Boolean IsRequestedQtyGreaterThanTotalQty(int RequestedBatchQty)
        {
            try
            {
                int TotalRequestQty = RequestedBatchQty;

                if (m_BatchList == null)
                {
                    if (TotalRequestQty > m_TotalUnPackQty)
                        return true;
                }
                else
                {

                    foreach (BatchDetails objBatchDetails in m_BatchList)
                    {
                        TotalRequestQty = TotalRequestQty + objBatchDetails.RequestedQty;
                    }
                    if (TotalRequestQty > m_TotalUnPackQty)
                    {
                        return true;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void AddRecords(string BatchNo )
        {
            try
            {
                BatchDetails newBatch = null;
                

                 newBatch = (from u in m_originalBatchList where (u.BatchNo.Equals(BatchNo, StringComparison.CurrentCultureIgnoreCase)) select u).FirstOrDefault(); 


                 if (newBatch != null)
                 {
                     if (IsRequestedQtyGreaterThanTotalQty(m_RequestedQty))
                     {
                         MessageBox.Show(Common.GetMessage("INF0062"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                         return;
                      
                     }
                     else
                     {
                         newBatch.RequestedQty = Convert.ToInt32(m_RequestedQty);
                         m_BatchList.Add(newBatch);

                     }
                 }

                BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     

        void BindGrid()
        {
            dgvBatchDetails.DataSource = null;
            if (m_BatchList.Count > 0)
            {
                dgvBatchDetails.DataSource = m_BatchList;
                dgvBatchDetails.ClearSelection();
                ResetControls();
            }
        }

  


        Boolean CheckIfRecordExists(string batchNo)
        {
            if (m_BatchList == null) return false;
            Int32 count = (from u in m_BatchList where (u.BatchNo.Equals(batchNo,StringComparison.CurrentCultureIgnoreCase)) select u).Count();

            if (count > 0) return true;
            return false;
        }

        void CallValidations()
        {
            try
            {
                //ComboBoxValidations(cmbMfgBatch, lblMfgBatch, errBatchDetails);
                if (txtBatchUnPackQty.Text.Trim().Length < 0)
                {
                   
                    Validators.SetErrorMessage(errBatchDetails, txtBatchUnPackQty,"INF0019",lblBatchUnPackQty.Text);
                }
                else if (!Validators.IsInt64(txtBatchUnPackQty.Text.Trim()))
                {

                    Validators.SetErrorMessage(errBatchDetails, txtBatchUnPackQty, "INF0010", lblBatchUnPackQty.Text);
                }
                else if(Convert.ToInt32(txtBatchUnPackQty.Text.Trim())==0)
                {

                    Validators.SetErrorMessage(errBatchDetails, txtBatchUnPackQty, "INF0010", lblBatchUnPackQty.Text);
                }
                else
                {
                    Validators.SetErrorMessage(errBatchDetails,txtBatchUnPackQty );
                }

             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void ComboBoxValidations(ComboBox cmb, Label lbl, ErrorProvider ep)
        {
            try
            {
                if (Validators.CheckForSelectedValue(cmb.SelectedIndex))
                    Validators.SetErrorMessage(ep, cmb, "VAL0002", lbl.Text);
                else
                    Validators.SetErrorMessage(ep, cmb);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        String GetErrorMessages()
        {
            try
            {
                StringBuilder sbError = new StringBuilder();

                Validators.AppendToStringBuilder(Validators.GetErrorMessage(errBatchDetails, cmbMfgBatch), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(errBatchDetails, txtBatchUnPackQty), ref sbError);

                return Common.ReturnErrorMessage(sbError).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void dgvBatchDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbMfgBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

         
            if (m_originalBatchList != null)
            {
                if (m_originalBatchList.Count > 0)
                {
                    string BatchNo = cmbMfgBatch.SelectedValue.ToString();

                    BatchDetails batchdetail = null;
                    batchdetail = (from u in m_originalBatchList where (u.BatchNo.Equals(BatchNo, StringComparison.CurrentCultureIgnoreCase)) select u).FirstOrDefault();
                    if (batchdetail != null)
                    {
                        lblDisplayExpDate.Text = batchdetail.ExpDate;
                        lblDisplayMfgDate.Text = batchdetail.MfgDate;
                        lblDisplayMRP.Text = batchdetail.MRP;

                    }

                }
 
            }
            }
            catch (Exception)
            {

                throw;
            }
          

        }
   

    }
}
