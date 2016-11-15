using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.UI;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using System.Collections.Specialized;
using Vinculum.Framework.DataTypes;
using Vinculum.Framework.Data;
using AuthenticationComponent.BusinessObjects;
using CoreComponent.MasterData.BusinessObjects;


namespace CoreComponent.MasterData.UI
{
    public partial class frmItemCodeMapping : MultiTabTemplate
    {
        DataTable dtData = new DataTable();
        
        DataSet m_printDataSet;
        #region Constant
        private string ITEM_CODEMAPPING = "usp_ItemCodeMapping";
        private bool m_bFlag;
        
        #endregion

        #region Constructor
        public frmItemCodeMapping()
        {
            InitializeComponent();
            (new ModifyTabControl()).RemoveTabPageArray(tabMultiTabTemplate, 1);
            label1.Text = "Item Code Mapping";
            tabPage1.Text = "ItemCodeMapping";
            FillStatus();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = Common.DTP_DATE_FORMAT;
            dateTimePicker1.ShowCheckBox = true;
            dateTimePicker1.Checked = true;
            cmbBoxStatus.Text = "Active";
        }
        #endregion



        #region Method
       private void CreatePrintDataSet()
        {
            m_printDataSet = new DataSet();

            //DataTable dtTOIDetail = new DataTable("ICMP");

            //dtTOIDetail = dtData;
            //dtTOIDetail.TableName = "ICMP";

           dtData.TableName = "ICMP";
           m_printDataSet.Tables.Add(dtData.Copy());
         

            
        }

        private void PrintReport()
        {
            CreatePrintDataSet();
            CoreComponent.UI.ReportScreen reportScreenObj = new CoreComponent.UI.ReportScreen((int)Common.ReportType.ICMP, m_printDataSet);
            reportScreenObj.ShowDialog();
            //reportScreenObj.PrintReport();
            m_printDataSet = null;
        }

        void FillStatus()
        {
            DataTable dtTemp;
            
            dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMO_STATUS, 0, 0, 0));
            //Remove the 'Select' row
            
            //dtTemp.Select(Common.KEYCODE1 + "=" + Common.INT_DBNULL.ToString())[0].Delete();
            dtTemp.Select(Common.KEYCODE1 + "=3")[0].Delete();
            dtTemp.AcceptChanges();
            cmbBoxStatus.DataSource = dtTemp;
            cmbBoxStatus.ValueMember = Common.KEYCODE1;
            cmbBoxStatus.DisplayMember = Common.KEYVALUE1;
        }
        void FormatGridView()
        {
            dgvItemCodeMapping.AutoGenerateColumns = false;
            dgvItemCodeMapping.MultiSelect = false;
            dgvItemCodeMapping.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItemCodeMapping.AllowUserToAddRows = false;
            dgvItemCodeMapping.AllowUserToDeleteRows = false;
            dgvItemCodeMapping.AllowUserToOrderColumns = false;
            dgvItemCodeMapping.AllowUserToResizeColumns = false;
            dgvItemCodeMapping.AllowUserToResizeRows = false;
            dgvItemCodeMapping.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItemCodeMapping.RowHeadersVisible = false;
        }
        private void frmItemCodeMapping_Load(object sender, EventArgs e)
        {
            InitailizeItemMasterTab();
        }
        void InitailizeItemMasterTab()
        {
            try
            {
                Common.GetDataGridViewColumns(dgvItemCodeMapping, Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");
                FormatGridView();
                dateTimePicker1.CustomFormat = Common.DTP_DATE_FORMAT;
                dateTimePicker1.ShowCheckBox = true;
                dateTimePicker1.Checked = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        void ResetControl()
        {
            errorSave.SetError(txtToItemName, "");
            errorSave.SetError(txtitemcode, "");
            errorSave.SetError(txtfitemName, "");
            errorSave.SetError(txtToItemCode, "");
            errorSave.SetError(cmbBoxStatus, "");
            dgvItemCodeMapping.DataSource = null;
            txtitemcode.Text = "";
            txtfitemName.Text = "";
            txtToItemCode.Text = "";
            txtToItemName.Text = "";
            dateTimePicker1.Checked = false;
            btnSave.Enabled = true;
            txtitemcode.ReadOnly = false;
            btnSearch.Enabled = true;
            cmbBoxStatus.SelectedValue = "-1";
        }
        #endregion Method

        #region Event
        private void txtitemcode_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox objTxtBox = null;
            try
            {
                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    NameValueCollection _collection = new NameValueCollection();
                    _collection.Add("LocationId", Common.CurrentLocationId.ToString());
                    CoreComponent.Controls.frmSearch _frmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.Item, _collection);
                    _frmSearch.ShowDialog();
                    CoreComponent.MasterData.BusinessObjects.ItemDetails _Item = (CoreComponent.MasterData.BusinessObjects.ItemDetails)_frmSearch.ReturnObject;
                    if (_Item != null)
                    {
                        objTxtBox = ((TextBox)sender);
                        objTxtBox.Text = _Item.ItemCode.ToString();
                        if (objTxtBox.Tag != null && string.Compare(objTxtBox.Tag.ToString(), "FromItem", true) == 0)
                            txtfitemName.Text = _Item.ItemName;
                        else
                            txtToItemName.Text = _Item.ItemName;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(ex.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion Event

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
            DBParameterList dbParamlist = null;
            DataTaskManager dttskMgr = null;
            try
            {
                EmptyErrorProvider();
                dbParamlist = new DBParameterList();
                dttskMgr = new DataTaskManager();
                SetParameter(dbParamlist, "S");
                dtData = dttskMgr.ExecuteDataTable(ITEM_CODEMAPPING, dbParamlist);
                if (dtData.Rows.Count > 0)
                {
                    m_bFlag = true;
                    dgvItemCodeMapping.DataSource = null;
                    dgvItemCodeMapping.DataSource = dtData;
                    dgvItemCodeMapping.ClearSelection();
                }
                    
                else if (sender != null)
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSearch.Enabled = true;
                if (sender != null)
                    btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            finally
            {
                m_bFlag = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DBParameterList objDbParam = null;
            DataTaskManager objDataMgr = null;
            Int32 iCount = 0;
            StringBuilder sbData = null;
            try
            {


                //EmptyErrorProvider();
                ValidateEmptyValue();
                sbData = GenerateError();
                if (sbData == null || sbData.Length != 0)
                {
                    MessageBox.Show(sbData.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //if (txtToItemCode.Text )
                if (string.Compare(txtToItemCode.Text.Trim(), txtitemcode.Text.Trim(), true) == 0)
                {
                    //MessageBox.Show(Common.GetMessage("VAL0610"));
                    MessageBox.Show(Common.GetMessage("VAL0610"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                objDbParam = new DBParameterList();
                if (btnSearch.Enabled)
                    SetParameter(objDbParam, "I");
                else
                    SetParameter(objDbParam, "U");
                objDataMgr = new DataTaskManager();
                iCount = objDataMgr.ExecuteNonQuery(ITEM_CODEMAPPING, objDbParam);
                if (iCount > 0)
                {
                    // MessageBox.Show(Common.GetMessage("8001"));
                    MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSearch_Click(null, null);
                }
                else
                {
                    string sMessage = objDbParam[Common.PARAM_OUTPUT].Value.ToString();
                    if (!string.IsNullOrEmpty(sMessage))
                        MessageBox.Show(Common.GetMessage(sMessage));
                }

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(ex.ToString());
                //MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
            }
        }

        void ReadOnlyControl(bool bFlag)
        {
            txtfitemName.ReadOnly = bFlag;
            txtToItemName.ReadOnly = bFlag;
        }
        private StringBuilder GenerateError()
        {
            StringBuilder sbError = new StringBuilder();
            //if (errorSave.GetError(cmbLocation).Trim().Length > 0)
            //{
            //    sbError.Append(errInventory.GetError(cmbLocation));
            //    sbError.AppendLine();
            //}
           
            if (errorSave.GetError(txtitemcode).Trim().Length > 0)
            {
                sbError.Append(errorSave.GetError(txtitemcode));
                sbError.AppendLine();
            }

            if (errorSave.GetError(txtToItemCode).Trim().Length > 0)
            {
                sbError.Append(errorSave.GetError(txtToItemCode));
                sbError.AppendLine();
            }
            if (errorSave.GetError(cmbBoxStatus).Trim().Length > 0)
            {
                sbError.Append(errorSave.GetError(cmbBoxStatus));
                sbError.AppendLine();
            }
           
            return Common.ReturnErrorMessage(sbError);
            //return sbError;
        }

        void EmptyErrorProvider()
        {
            errorSave.SetError(txtitemcode, "");
            errorSave.SetError(txtToItemCode, "");
            errorSave.SetError(cmbBoxStatus, "");
            //errorSave.SetError(txtitemcode, "");
        }

        void ValidateEmptyValue()
        {
            if (string.IsNullOrEmpty(txtitemcode.Text.Trim()))
                errorSave.SetError(txtitemcode, Common.GetMessage("INF0019", lblFromitemcode.Text.Trim()));
            if (string.IsNullOrEmpty(txtToItemCode.Text.Trim()))
                errorSave.SetError(txtToItemCode, Common.GetMessage("INF0019", lbltoitemCode.Text.Trim()));
            if(cmbBoxStatus.SelectedIndex ==-1 || cmbBoxStatus.SelectedValue.ToString() =="-1")
                errorSave.SetError(cmbBoxStatus, Common.GetMessage("INF0019", lblStatus.Text.Trim()));
        }

        void ValidateValue( bool Toflag)
        {
            
            if (Toflag)
                errorSave.SetError(txtToItemCode, Common.GetMessage("VAL0006", lbltoitemCode.Text.Trim()));
            else
                errorSave.SetError(txtitemcode, Common.GetMessage("VAL0006", lblFromitemcode.Text.Trim()));
            
        }


        private void SetParameter(DBParameterList objDbParam, string sMode)
        {
            try
            {
                objDbParam.Add(new DBParameter("@ToItemCode", txtToItemCode.Text.Trim(), DbType.String));
                objDbParam.Add(new DBParameter("@FromItemCode", txtitemcode.Text.Trim(), DbType.String));
                objDbParam.Add(new DBParameter("@CreatedBy", Authenticate.LoggedInUser.UserId, DbType.Int32));
                objDbParam.Add(new DBParameter("@Mode", sMode, DbType.String));
                objDbParam.Add(new DBParameter("@ToItemName", txtToItemName.Text.Trim(), DbType.String));
                objDbParam.Add(new DBParameter("@FromItemName", txtfitemName.Text.Trim(), DbType.String));
                objDbParam.Add(new DBParameter("@Status", cmbBoxStatus.SelectedValue.ToString(), DbType.String));
                objDbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
            }
            finally
            {
            }
        }

        private void dgvItemCodeMapping_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow objRow = null;
            try
            {
                txtitemcode.ReadOnly = true;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvItemCodeMapping.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell))
                {
                    btnSave.Enabled = true;
                    btnSearch.Enabled = false;
                    
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvItemCodeMapping_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_bFlag)
                {
                    EmptyErrorProvider();
                    DataGridViewRow objRow = null;
                    btnSave.Enabled = false;
                    btnSearch.Enabled = true;
                    objRow = dgvItemCodeMapping.CurrentRow;
                    SetValues(objRow);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetValues(DataGridViewRow objRows)
        {
            txtfitemName.Text = objRows.Cells["FromitemName"].Value.ToString();
            txtToItemName.Text = objRows.Cells["ToitemName"].Value.ToString();
            txtToItemCode.Text = objRows.Cells["ToItemCode"].Value.ToString();
            txtitemcode.Text = objRows.Cells["Fromitemcode"].Value.ToString();
            cmbBoxStatus.SelectedValue = objRows.Cells["Status"].Value.ToString() == "Active" ? 1 : 2;
            
        }

        private void txtitemcode_Validated(object sender, EventArgs e)
        {

                        
            TextBox objBox = null;
            try
            {
                objBox = (sender) as TextBox;
             
                if (string.IsNullOrEmpty(objBox.Text.Trim()))
                    return;
                ItemDetails itemDetails = new ItemDetails();
                itemDetails.LocationId = "-1";

                List<ItemDetails> lst = new List<ItemDetails>();
                ItemDetails idetails = new ItemDetails();
                itemDetails.ItemCode = objBox.Text;
                lst = itemDetails.SearchLocationItem();
                if (lst != null && lst.Count > 0)
                {
                    var query = (from p in lst where p.ItemCode.Equals(objBox.Text) select p).First();

                    idetails = ((BusinessObjects.ItemDetails)query);

                    objBox.Text = idetails.ItemCode;
                    errorSave.SetError(objBox, "");
                    if (objBox.Tag != null && string.Compare(objBox.Tag.ToString(), "ToItem", true) == 0)
                    {

                        txtToItemName.Text = idetails.ItemName;
                    }
                    else
                    {
                        //errorSave.SetError(txtitemcode, "");
                        //errorSave.SetError(txtfitemName, "");
                        txtfitemName.Text = idetails.ItemName;
                    }
                }
                else
                {
                    if (objBox.Tag != null && string.Compare(objBox.Tag.ToString(), "ToItem", true) == 0)
                    {
                        //errorSave.SetError(txtToItemCode, Common.GetMessage("VAL0006", lblFromitemcode.Text));
                        ValidateValue(true);
                        txtToItemName.Text = string.Empty;
                    }
                    else
                    {
                       // errorSave.SetError(txtitemcode, Common.GetMessage("VAL0006", lblFromitemcode.Text));
                        ValidateValue(false);
                        txtfitemName.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBoxStatus_Validated(object sender, EventArgs e)
        {
            if (cmbBoxStatus.SelectedValue.ToString() == "-1" || cmbBoxStatus.SelectedIndex == -1)
                errorSave.SetError(cmbBoxStatus, Common.GetMessage("VAL0006", lblStatus.Text.Trim()));
            else
                errorSave.SetError(cmbBoxStatus, "");
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvItemCodeMapping.DataSource == null)
            {
                MessageBox.Show("CAN NOT PRINT");
                
            }
            else
            {
                PrintReport();
                
                
            }
            
        }

        private void cmbBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
