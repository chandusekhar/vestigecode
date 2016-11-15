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
using PromotionsComponent.BusinessLayer;
using CoreComponent.Hierarchies.UI;
using AuthenticationComponent.BusinessObjects;

namespace PromotionsComponent.UI
{
    public partial class frmConditionTier : Form
    {
        #region Variables
        private PromotionCondition condition = null;
        PromotionTier tier = null;
        private int hierarchyLevelId = Common.INT_DBNULL;
        private List<PromotionTier> m_originalPromotionTiers = null;
        private int promotionType = -1;
        private bool m_boolLoadedFirstInstance = true;

        //Edit button options
        private const string CON_LOCATION_EDIT = "EDIT";
        private const string CON_TIER_REMOVE = "REMOVE";
        #endregion

        #region CTOR
        /// <summary>
        /// Default Constructor
        /// </summary>
        public frmConditionTier()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="condition"></param>
        public frmConditionTier(PromotionCondition condition, int promotionType)
        {
            this.condition = condition;
            m_originalPromotionTiers = new List<PromotionTier>();

            foreach (PromotionTier existingPromotionTier in condition.Tiers)
            {
                PromotionTier promotionTier = new PromotionTier();
                promotionTier.BuyQtyFrom = existingPromotionTier.DisplayBuyQtyFrom;
                promotionTier.BuyQtyTo = existingPromotionTier.DisplayBuyQtyTo;
                promotionTier.ConditionCodeId = existingPromotionTier.ConditionCodeId;
                promotionTier.ConditionCodeVal = existingPromotionTier.ConditionCodeVal;
                promotionTier.ConditionId = existingPromotionTier.ConditionId;
                promotionTier.ConditionOnId = existingPromotionTier.ConditionOnId;
                promotionTier.ConditionOnVal = existingPromotionTier.ConditionOnVal;
                //promotionTier.DiscountTypeId = existingPromotionTier.DiscountValue == 100m ? (int)Promotion.DiscountType.FreeItem : existingPromotionTier.DiscountTypeId;
                //promotionTier.DiscountTypeVal = promotionTier.DiscountTypeId == (int)Promotion.DiscountType.FreeItem ? "Free Item" : existingPromotionTier.DiscountTypeVal;
                promotionTier.DiscountTypeId = existingPromotionTier.DiscountTypeId;
                promotionTier.DiscountTypeVal = existingPromotionTier.DiscountTypeVal;
                promotionTier.DiscountValue = existingPromotionTier.DisplayDiscountValue;
                promotionTier.LineNumber = existingPromotionTier.LineNumber;
                promotionTier.PromotionId = existingPromotionTier.PromotionId;
                promotionTier.Qty = existingPromotionTier.DisplayQty;
                promotionTier.StatusId = existingPromotionTier.StatusId;
                promotionTier.StatusVal = existingPromotionTier.StatusVal;
                promotionTier.TempConditionId = existingPromotionTier.TempConditionId;
                promotionTier.TierId = existingPromotionTier.TierId;

                m_originalPromotionTiers.Add(promotionTier);
            }

            InitializeComponent();

            dgvCreatePromoTier =
             Common.GetDataGridViewColumns(dgvCreatePromoTier,
             Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

            this.promotionType = promotionType;

            PopulateForm(this.condition);

            m_boolLoadedFirstInstance = true;
        }

        #endregion

        #region Events
        /// <summary>
        /// On Promotion Tier 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConditionTier_Load(object sender, EventArgs e)
        {
            PopulateCombo();

            //To clear the selection in data-grid at the time of load, 
            //check whether grid has more than one rows and correspondingly clear the selection,
            //and clear all Condition-Elements as well.
            if (dgvCreatePromoTier.Rows.Count > 0)
            {
                //dgvCreatePromoTier.Rows[0].Selected = false;
                dgvCreatePromoTier.ClearSelection();
                ClearConditionTier();

                m_boolLoadedFirstInstance = false;
            }
        }

        /// <summary>
        /// On Edit of condition Tiers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCreatePromoTier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //When edit button of the tier grid is selected.
                //Then change the add mode to save mode
                //if (((DataGridView)sender).Columns[e.ColumnIndex].Name.ToUpper() == CON_LOCATION_EDIT)
                //{
                //    this.tier = condition.Tiers[e.RowIndex];
                //    //Update the condition values
                //    PopulatePromotionTier(tier);
                //    btnCreatePromoTierAdd.Text = "Save";
                //    EnableDisableTier(TierOperationState.EDIT);
                //}
                if ((((DataGridView)sender).Columns[e.ColumnIndex].Name.ToUpper() == CON_TIER_REMOVE) &&
                    (e.RowIndex > -1))
                {
                    if (this.condition.Tiers[e.RowIndex].TierId == Common.INT_DBNULL)
                    //if (((DataGridView)sender).Rows[e.RowIndex].Cells["SavedRow"].Value != null)
                    {
                        if (((DataGridView)sender).RowCount == e.RowIndex + 1)
                        {
                            DialogResult confirmresult = MessageBox.Show(Common.GetMessage("5012"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (confirmresult == DialogResult.Yes)
                            {
                                condition.Tiers.Remove(condition.Tiers[e.RowIndex]);
                                this.dgvCreatePromoTier.SelectionChanged -= new System.EventHandler(this.dgvCreatePromoTier_SelectionChanged);
                                dgvCreatePromoTier.DataSource = null;
                                dgvCreatePromoTier.DataSource = new List<PromotionCondition>();
                                if (condition.Tiers.Count > 0)
                                {
                                    dgvCreatePromoTier.DataSource = condition.Tiers;
                                    CurrencyManager cm = (CurrencyManager)this.BindingContext[condition.Tiers];
                                    cm.Refresh();
                                }
                                this.dgvCreatePromoTier.SelectionChanged += new System.EventHandler(this.dgvCreatePromoTier_SelectionChanged);
                                dgvCreatePromoTier_SelectionChanged(dgvCreatePromoTier, null);
                                ClearConditionTier();

                                //Make sure that in the remaining tier(s), only the unsaved-records are available for deletion
                                for (int index = 0; index < this.condition.Tiers.Count; index++)
                                {
                                    if (condition.Tiers[index].TierId == -1)
                                    {
                                        dgvCreatePromoTier.Rows[index].Cells["SavedRow"].Value = "-1";
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(Common.GetMessage("INF0060"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage("INF0077"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// On selection changed. To give a default edit functionality to the rows selected in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCreatePromoTier_SelectionChanged(object sender, EventArgs e)
        {

            //By Default on row select enable edit mode
            try
            {
                if (!m_boolLoadedFirstInstance)
                {
                    if (dgvCreatePromoTier.SelectedCells.Count > 0)
                    {
                        int rowIndex = dgvCreatePromoTier.SelectedCells[0].RowIndex;
                        this.tier = condition.Tiers[rowIndex];
                        //ClearConditionTier();

                        cmbCreatePromoTierOn.SelectedValue = Common.INT_DBNULL;
                        DataTable dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_DISCOUNT_TYPE, 0, 0, 0));
                        if (this.promotionType == (int)Promotion.PromotionCategoryType.Volume)
                        {
                            dtTemp.DefaultView.RowFilter = "KeyCode1 <> 4";
                            dtTemp = dtTemp.DefaultView.ToTable();
                        }
                        else if (this.promotionType == (int)Promotion.PromotionCategoryType.BillBuster)
                        {
                            dtTemp.DefaultView.RowFilter = "KeyCode1 <> 3";
                            dtTemp = dtTemp.DefaultView.ToTable();
                        }
                        cmbCreatePromoTierDiscountType.DataSource = dtTemp;
                        cmbCreatePromoTierDiscountType.ValueMember = Common.KEYCODE1;
                        cmbCreatePromoTierDiscountType.DisplayMember = Common.KEYVALUE1;

                        //Update the condition values
                        PopulatePromotionTier(tier);
                        btnCreatePromoTierAdd.Text = "&Save";
                        EnableDisableTier(TierOperationState.EDIT);
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
        /// Add Tier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatePromoTierAdd_Click(object sender, EventArgs e)
        {
            try
            {
                m_boolLoadedFirstInstance = false;

                //Validate the UI input conditions
                if (ValidateTier())
                {
                    //The Condition Type, Conditon On, Condition Code must be unique with in the conditions
                    int conditionCodeId = Common.INT_DBNULL;
                    string conditionCode = string.Empty;

                    if ((Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCT))
                        | (Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCTGROUP)))
                    {
                        conditionCodeId = Convert.ToInt32(cmbCreatePromoTierCode.SelectedValue);
                        conditionCode = cmbCreatePromoTierCode.Text;
                    }
                    else
                    {
                        conditionCodeId = hierarchyLevelId;
                        conditionCode = txtCreatePromoTierCode.Text;
                    }

                    if (btnCreatePromoTierAdd.Text == "&Save")
                    {
                        PromotionTier tier = new PromotionTier();
                        tier.UpdateTier(this.tier, condition.PromotionId,
                                                                this.tier.TierId , condition.ConditionId,
                                                                this.tier.LineNumber,
                                                                Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue),
                                                                cmbCreatePromoTierOn.Text,
                                                                conditionCodeId,
                                                                conditionCode,
                                                                Convert.ToDecimal(txtCreatePromoTierBuyQtyFrom.Text),
                                                                Convert.ToDecimal(txtCreatePromoTierBuyQtyTo.Text),
                                                                Convert.ToDecimal(txtCreatePromoTierQty.Text),
                                                                Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue),
                                                                cmbCreatePromoTierDiscountType.Text,
                                                                Convert.ToDecimal(txtCreatePromoTierDiscountValue.Text),
                                                                Convert.ToInt32(cmbCreatePromoTierStatus.SelectedValue),
                                                                cmbCreatePromoTierStatus.Text);
                    }
                    else
                    {
                        //Create the new promotion condition object
                        PromotionTier tier = new PromotionTier(condition.PromotionId,
                                                                Common.INT_DBNULL,
                                                                condition.ConditionId,
                                                                condition.Tiers.Count + 1,
                                                                Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue),
                                                                cmbCreatePromoTierOn.Text,
                                                                conditionCodeId,
                                                                conditionCode,
                                                                Convert.ToDecimal(txtCreatePromoTierBuyQtyFrom.Text),
                                                                Convert.ToDecimal(txtCreatePromoTierBuyQtyTo.Text),
                                                                Convert.ToDecimal(txtCreatePromoTierQty.Text),
                                                                Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue),
                                                                cmbCreatePromoTierDiscountType.Text,
                                                                Convert.ToDecimal(txtCreatePromoTierDiscountValue.Text),
                                                                Convert.ToInt32(cmbCreatePromoTierStatus.SelectedValue),
                                                                cmbCreatePromoTierStatus.Text
                                                               );

                        //Add it to the current Promotion Object
                        condition.Tiers.Add(tier);
                    }
                    //rebind the grid
                    this.dgvCreatePromoTier.SelectionChanged -= new System.EventHandler(this.dgvCreatePromoTier_SelectionChanged);

                    dgvCreatePromoTier.DataSource = null;
                    dgvCreatePromoTier.DataSource = new List<PromotionTier>();
                    if (condition.Tiers.Count > 0)
                    {
                        dgvCreatePromoTier.DataSource = condition.Tiers; //(from p in condition.Tiers where p.StatusId != 3 select p);

                        CurrencyManager cm = (CurrencyManager)this.BindingContext[condition.Tiers];
                        cm.Refresh();

                        if (btnCreatePromoTierAdd.Text == "A&dd")
                        {
                            dgvCreatePromoTier.ClearSelection();
                            dgvCreatePromoTier.Rows[condition.Tiers.Count - 1].Selected = true;
                            dgvCreatePromoTier.Rows[condition.Tiers.Count - 1].Cells["SavedRow"].Value = "-1";
                        }
                    }
                    
                    this.dgvCreatePromoTier.SelectionChanged += new System.EventHandler(this.dgvCreatePromoTier_SelectionChanged);
                    dgvCreatePromoTier_SelectionChanged(dgvCreatePromoTier, null);

                    //ClearConditionTier();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// On click of Condition Code to get Buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatePromoConditionCode_Click(object sender, EventArgs e)
        {
            try
            {
                frmTree objTree = new frmTree("Promotion", "", Common.INT_DBNULL, this);
                Point pointTree = new Point();
                pointTree = this.PointToScreen(txtCreatePromoTierCode.Location);
                pointTree.Y = pointTree.Y + Common.TREE_COMP_Y;
                pointTree.X = pointTree.X + Common.TREE_COMP_X;
                objTree.Location = pointTree;
                objTree.ShowDialog();
                txtCreatePromoTierCode.Text = objTree.SelectedText;
                hierarchyLevelId = objTree.SelectedValue;

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// On change on Promo Condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCreatePromoConditionOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCreatePromoTierOn.SelectedValue.GetType() != typeof(System.Data.DataRowView))
                {
                    if (Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCT))
                    {
                        cmbCreatePromoTierCode.Visible = true;
                        //Get products and bind it
                        DataTable dtTemp = null; //Common.ParameterLookup(Common.ParameterType.Item, new ParameterFilter("Item", 0, 0, 0));
                        dtTemp = Common.ParameterLookup(Common.ParameterType.ItemsAvailableForPromotion, new ParameterFilter("Item", 1, 2, 0));
                     
                        cmbCreatePromoTierCode.DataSource = dtTemp;
                        cmbCreatePromoTierCode.ValueMember = "ItemId";
                        cmbCreatePromoTierCode.DisplayMember = "ItemCode";

                          
                        //Clear the Hierarchy Condition Code
                        txtCreatePromoTierCode.Text = string.Empty;
                        hierarchyLevelId = Common.INT_DBNULL;
                        txtCreatePromoTierCode.Visible = false;
                        btnCreatePromoTierCode.Visible = false;
                        //txtCreatePromoTierQty.Enabled = true;

                        if (this.promotionType == (int)Promotion.PromotionCategoryType.BillBuster)
                        {
                            txtCreatePromoTierQty.Enabled = true;
                            if (tier != null)
                            {
                                if (tier.TierId == Common.INT_DBNULL)
                                {
                                    txtCreatePromoTierQty.Text = "0";
                                }
                            }
                            else
                            {
                                txtCreatePromoTierQty.Text = "0";
                            }

                            DataTable dtDiscountTypes = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_DISCOUNT_TYPE, 0, 0, 0));
                            dtDiscountTypes.DefaultView.RowFilter = "KeyCode1 IN (-1,4)";
                            dtDiscountTypes = dtDiscountTypes.DefaultView.ToTable();
                            cmbCreatePromoTierDiscountType.DataSource = dtDiscountTypes;
                        }
                    }
                    else if (Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCTGROUP))
                    {
                        cmbCreatePromoTierCode.Visible = true;
                        //Get product group and bind it
                        //Get products and bind it
                        DataTable dtTemp = Common.ParameterLookup(Common.ParameterType.ItemGroup, new ParameterFilter("-1", 0, 0, 0));
                        cmbCreatePromoTierCode.DataSource = dtTemp;
                        cmbCreatePromoTierCode.ValueMember = "GroupItemId";
                        cmbCreatePromoTierCode.DisplayMember = "GroupName";

                        //Clear the Hierarchy Condition Code
                        txtCreatePromoTierCode.Text = string.Empty;
                        hierarchyLevelId = Common.INT_DBNULL;
                        txtCreatePromoTierCode.Visible = false;
                        btnCreatePromoTierCode.Visible = false;
                        //txtCreatePromoTierQty.Enabled = true;
                    }
                    else if (Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.MERCHANDISINGHIERARCHY))
                    {
                        cmbCreatePromoTierCode.Visible = false;
                        txtCreatePromoTierCode.Visible = true;
                        btnCreatePromoTierCode.Visible = true;
                        //txtCreatePromoTierQty.Enabled = true;
                    }
                    else
                    {
                        cmbCreatePromoTierCode.Visible = true;
                        cmbCreatePromoTierCode.SelectedValue = Common.INT_DBNULL;  
                        //Clear the Hierarchy Condition Code
                        txtCreatePromoTierCode.Text = string.Empty;
                        hierarchyLevelId = Common.INT_DBNULL;
                        txtCreatePromoTierCode.Visible = false;
                        btnCreatePromoTierCode.Visible = false;

                        if (this.promotionType == (int)Promotion.PromotionCategoryType.BillBuster)
                        {
                            txtCreatePromoTierQty.Enabled = false;
                            if (tier != null)
                            {
                                if (tier.TierId == Common.INT_DBNULL)
                                {
                                    txtCreatePromoTierQty.Text = "0";
                                }
                            }
                            else
                            {
                                txtCreatePromoTierQty.Text = "0";
                            }

                            DataTable dtDiscountTypes = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_DISCOUNT_TYPE, 0, 0, 0));
                            dtDiscountTypes.DefaultView.RowFilter = "KeyCode1 NOT IN (3,4)";
                            dtDiscountTypes = dtDiscountTypes.DefaultView.ToTable();
                            cmbCreatePromoTierDiscountType.DataSource = dtDiscountTypes;
                        }

                        //txtCreatePromoTierQty.Enabled = false;
                        //txtCreatePromoTierQty.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }

        /// <summary>
        /// On Clear of the Promo Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatePromoTierClear_Click(object sender, EventArgs e)
        {
            try
            {
                m_boolLoadedFirstInstance = false;

                ClearConditionTier();

                dgvCreatePromoTier.ClearSelection();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCreatePromoTierDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ctrl = (ComboBox)sender;
            if (ctrl.SelectedValue != null)
            {
                if (ctrl.SelectedValue.GetType() != typeof(DataRowView))
                {
                    if (Convert.ToInt32(ctrl.SelectedValue) == (int)Promotion.DiscountType.FreeItem)
                    {
                        txtCreatePromoTierDiscountValue.Enabled = false;
                        txtCreatePromoTierDiscountValue.Text = ((Int32)100).ToString(Common.GetRoundingZeroes(Common.DisplayAmountRounding));
                    }
                    else
                    {
                        txtCreatePromoTierDiscountValue.Enabled = true;
                        txtCreatePromoTierDiscountValue.Text = ((Int32)0).ToString(Common.GetRoundingZeroes(Common.DisplayAmountRounding));
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.condition.Tiers = m_originalPromotionTiers;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Validate the tier Data
        /// </summary>
        /// <returns></returns>
        private Boolean ValidateTier()
        {
            Boolean IsValidationSuccess = true;

            StringBuilder errorMessages = new StringBuilder();
            string errorMessage = string.Empty;

            //The Condition Type, Conditon On, Condition Code must be unique with in the conditions
            int conditionCodeId = Common.INT_DBNULL;
            string conditionCode = string.Empty;

            //Buy From Qty is mandatory
            if (Validators.CheckForEmptyString(txtCreatePromoTierBuyQtyFrom.Text.Length))
            {
                errorMessage = Common.GetMessage("VAL0001", lblCreatePromoTierBuyQtyFrom.Text.Replace(":*", ""));
                errTier.SetError(txtCreatePromoTierBuyQtyFrom, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else if (!Validators.IsInt32(txtCreatePromoTierBuyQtyFrom.Text))
            {
                //Buy From Qty should be decimal
                errorMessage = Common.GetMessage("VAL0009", lblCreatePromoTierBuyQtyFrom.Text.Replace(":*", ""));
                errTier.SetError(txtCreatePromoTierBuyQtyFrom, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else
            {
                errTier.SetError(txtCreatePromoTierBuyQtyFrom, string.Empty);
            }

            //Buy From Qty is mandatory
            if (Validators.CheckForEmptyString(txtCreatePromoTierBuyQtyTo.Text.Length))
            {
                //Buy To Qty is mandatory
                errorMessage = Common.GetMessage("VAL0001", lblCreatePromoTierBuyQtyTo.Text.Replace(":*", ""));
                errTier.SetError(txtCreatePromoTierBuyQtyTo, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else if (!Validators.IsInt32(txtCreatePromoTierBuyQtyTo.Text))
            {
                //Buy To Qty should be decimal
                errorMessage = Common.GetMessage("VAL0009", lblCreatePromoTierBuyQtyTo.Text.Replace(":*", ""));
                errTier.SetError(txtCreatePromoTierBuyQtyTo, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else if (errorMessage.Length == 0)
            {
                if (Convert.ToInt32(txtCreatePromoTierBuyQtyTo.Text) < Convert.ToInt32(txtCreatePromoTierBuyQtyFrom.Text))
                {
                    errorMessage = Common.GetMessage("VAL0047", lblCreatePromoTierBuyQtyTo.Text.Replace(":*", ""), lblCreatePromoTierBuyQtyFrom.Text.Replace(":*", ""));
                    errTier.SetError(txtCreatePromoTierBuyQtyTo, errorMessage);
                    errorMessages.Append(errorMessage);
                    errorMessages.Append(Environment.NewLine);
                    IsValidationSuccess = false;
                }
                else
                {
                    errTier.SetError(txtCreatePromoTierBuyQtyTo, string.Empty);
                }
            }
            else
            {
                errTier.SetError(txtCreatePromoTierBuyQtyTo, string.Empty);
            }

            if (this.promotionType == (int)Promotion.PromotionCategoryType.Volume)
            {
                if (Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue) == Common.INT_DBNULL)
                {
                    errorMessage = Common.GetMessage("VAL0002", lblCreatePromoTierOn.Text.Replace(":", "").Replace("*", ""));
                    errTier.SetError(cmbCreatePromoTierOn, errorMessage);
                    errorMessages.Append(errorMessage);
                    errorMessages.Append(Environment.NewLine);
                    IsValidationSuccess = false;
                }
                else
                {
                    errTier.SetError(cmbCreatePromoTierOn, string.Empty);
                }

                if (Convert.ToInt32(cmbCreatePromoTierCode.SelectedValue) == Common.INT_DBNULL)
                {
                    errorMessage = Common.GetMessage("VAL0002", lblCreatePromoTierCode.Text.Replace(":", "").Replace("*", ""));
                    errTier.SetError(cmbCreatePromoTierCode, errorMessage);
                    errorMessages.Append(errorMessage);
                    errorMessages.Append(Environment.NewLine);
                    IsValidationSuccess = false;
                }
                else
                {
                    errTier.SetError(cmbCreatePromoTierCode, string.Empty);
                }
            }
            else if (this.promotionType == (int)Promotion.PromotionCategoryType.BillBuster)
            {
                if (Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue) == Common.INT_DBNULL)
                {
                    if (Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue) == 4)
                    {
                        errorMessage = Common.GetMessage("VAL0101");
                        errTier.SetError(cmbCreatePromoTierOn, errorMessage);
                        errorMessages.Append(errorMessage);
                        errorMessages.Append(Environment.NewLine);
                        IsValidationSuccess = false;
                    }
                    else
                    {
                        if (Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue) == (int)Promotion.DiscountType.PercentOff)
                        {
                            if (Convert.ToDecimal(txtCreatePromoTierDiscountValue.Text) <= 0)
                            {
                                errorMessage = Common.GetMessage("VAL0104", lblCreatePromoTierDiscountValue.Text.Replace(":", "").Replace("*", ""), "0");
                                errTier.SetError(txtCreatePromoTierDiscountValue, errorMessage);
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else if (Convert.ToDecimal(txtCreatePromoTierDiscountValue.Text) >= 100)
                            {
                                errorMessage = Common.GetMessage("VAL0105", lblCreatePromoTierDiscountValue.Text.Replace(":", "").Replace("*", ""), "100");
                                errTier.SetError(txtCreatePromoTierDiscountValue, errorMessage);
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                errTier.SetError(txtCreatePromoTierQty, string.Empty);
                            }
                        }
                        else if (Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue) == (int)Promotion.DiscountType.ValueOff)
                        {
                            if (Convert.ToDouble(txtCreatePromoTierDiscountValue.Text) <= 0)
                            {
                                errorMessage = Common.GetMessage("VAL0105", lblCreatePromoTierDiscountValue.Text.Replace(":", "").Replace("*", ""), "0");
                                errTier.SetError(txtCreatePromoTierDiscountValue, errorMessage);
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else if (Convert.ToDouble(txtCreatePromoTierDiscountValue.Text) >= Convert.ToDouble(txtCreatePromoTierBuyQtyFrom.Text))
                            {
                                errorMessage = Common.GetMessage("VAL0105", lblCreatePromoTierDiscountValue.Text.Replace(":", "").Replace("*", ""), txtCreatePromoTierBuyQtyFrom.Text);
                                errTier.SetError(txtCreatePromoTierDiscountValue, errorMessage);
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                errTier.SetError(txtCreatePromoTierDiscountValue, string.Empty);
                            }
                        }
                        else
                        {
                            errTier.SetError(cmbCreatePromoTierOn, string.Empty);
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue) != 4)
                    {
                        errorMessage = Common.GetMessage("VAL0100");
                        errTier.SetError(cmbCreatePromoTierDiscountType, errorMessage);
                        errorMessages.Append(errorMessage);
                        errorMessages.Append(Environment.NewLine);
                        IsValidationSuccess = false;
                    }
                    else
                    {
                        errTier.SetError(cmbCreatePromoTierDiscountType, string.Empty);
                    }
                }
            }

            //If conditionOn is selected then Quantity, Condition Code, DiscountType and DiscountValue is Mandatory
            if (Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue) != Common.INT_DBNULL)
            {
                if ((Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCT))
                 | (Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCTGROUP)))
                {
                    conditionCodeId = Convert.ToInt32(cmbCreatePromoTierCode.SelectedValue);
                    conditionCode = cmbCreatePromoTierCode.Text;
                }
                else
                {
                    conditionCodeId = hierarchyLevelId;
                    conditionCode = txtCreatePromoTierCode.Text;
                }

                //As condition on is selected so condition code is Mandatory
                if (conditionCodeId == Common.INT_DBNULL)
                {
                    errorMessage = Common.GetMessage("VAL0002", lblCreatePromoTierCode.Text.Replace(":*", ""));
                    errTier.SetError(cmbCreatePromoTierCode, errorMessage);
                    errorMessages.Append(errorMessage);
                    errorMessages.Append(Environment.NewLine);
                    IsValidationSuccess = false;
                }
                else
                {
                    errTier.SetError(cmbCreatePromoTierCode, string.Empty);
                }

                //As condition on is selected so Discount Code is Mandatory
                if (Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue) == Common.INT_DBNULL)
                {
                    errorMessage = Common.GetMessage("VAL0002", lblCreatePromoTierDiscountType.Text.Replace(":", ""));
                    errTier.SetError(cmbCreatePromoTierDiscountType, errorMessage);
                    errorMessages.Append(errorMessage);
                    errorMessages.Append(Environment.NewLine);
                    IsValidationSuccess = false;
                }
                else if (Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue) == 4)
                {
                    if (Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue) != 1)
                    {
                        errorMessage = Common.GetMessage("VAL0092");
                        errTier.SetError(cmbCreatePromoTierDiscountType, errorMessage);
                        errorMessages.Append(errorMessage);
                        errorMessages.Append(Environment.NewLine);
                        IsValidationSuccess = false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(errTier.GetError(cmbCreatePromoTierDiscountType)))
                    {
                        errTier.SetError(cmbCreatePromoTierDiscountType, string.Empty);
                    }
                }

                if (!Validators.IsDecimal(txtCreatePromoTierDiscountValue.Text) || !Validators.IsNonZero(txtCreatePromoTierDiscountValue.Text))
                {
                    errTier.SetError(txtCreatePromoTierDiscountValue, string.Empty);

                    errorMessage = Common.GetMessage("VAL0009", lblCreatePromoTierDiscountValue.Text.Replace(":*", ""));
                    errTier.SetError(txtCreatePromoTierDiscountValue, errorMessage);
                    errorMessages.Append(errorMessage);
                    errorMessages.Append(Environment.NewLine);
                    IsValidationSuccess = false;
                }
                else if (Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue) == 1)
                {
                    if (Convert.ToDecimal(txtCreatePromoTierDiscountValue.Text) <= 0)
                    {
                        errorMessage = Common.GetMessage("VAL0104", lblCreatePromoTierDiscountValue.Text.Replace(":", "").Replace("*", ""), "0");
                        errTier.SetError(txtCreatePromoTierDiscountValue, errorMessage);
                        errorMessages.Append(errorMessage);
                        errorMessages.Append(Environment.NewLine);
                        IsValidationSuccess = false;
                    }
                    else if (Convert.ToDecimal(txtCreatePromoTierDiscountValue.Text) >= 100)
                    {
                        errorMessage = Common.GetMessage("VAL0105", lblCreatePromoTierDiscountValue.Text.Replace(":", "").Replace("*", ""), "100");
                        errTier.SetError(txtCreatePromoTierDiscountValue, errorMessage);
                        errorMessages.Append(errorMessage);
                        errorMessages.Append(Environment.NewLine);
                        IsValidationSuccess = false;
                    }
                    else
                    {
                        errTier.SetError(txtCreatePromoTierQty, string.Empty);
                    }
                }
                else if (Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue) == 2)
                {
                    string errMsg = "";
                    //PromotionTier temp = new PromotionTier();
                    //double minDistributorPrice = temp.FetchMinimumDistributorPrice(Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue), Convert.ToInt32(cmbCreatePromoTierCode.SelectedValue), ref errMsg);
                    double minDistributorPrice = 0.00;//this.condition.FetchMinimumDistributorPrice(Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue), Convert.ToInt32(cmbCreatePromoTierCode.SelectedValue), ref errMsg);
                    if ((Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue) != Common.INT_DBNULL) && (Convert.ToInt32(cmbCreatePromoTierCode.SelectedValue) != Common.INT_DBNULL))
                    {
                        minDistributorPrice = this.condition.FetchMinimumDistributorPrice(Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue), Convert.ToInt32(cmbCreatePromoTierCode.SelectedValue), ref errMsg);
                    }
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        //TODO: HANDLE ERROR
                    }
                    else
                    {
                        if ((Convert.ToDecimal(txtCreatePromoTierDiscountValue.Text) <= 0) || 
                            (Convert.ToDecimal(txtCreatePromoTierDiscountValue.Text) >= Convert.ToDecimal(minDistributorPrice)))
                        {
                            //errTier.SetError(txtCreatePromoTierDiscountValue, string.Empty);

                            errorMessage = Common.GetMessage("VAL0072", lblCreatePromoTierDiscountValue.Text.Replace(":", "").Replace("*", ""), "0").Replace(".", "") + " or greater than the minimum Distributor Price of " + minDistributorPrice + ".";
                            errTier.SetError(txtCreatePromoTierDiscountValue, errorMessage);
                            errorMessages.Append(errorMessage);
                            errorMessages.Append(Environment.NewLine);
                            IsValidationSuccess = false;
                        }
                        else
                        {
                            errTier.SetError(txtCreatePromoTierDiscountValue, string.Empty);
                        }
                    }
                }
                else if (Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue) == 3)
                {
                    string errMsg = string.Empty;
                    //PromotionTier temp = new PromotionTier();
                    //double minDistributorPrice = temp.FetchMinimumDistributorPrice(Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue), Convert.ToInt32(cmbCreatePromoTierCode.SelectedValue), ref errMsg);
                    double minDistributorPrice = this.condition.FetchMinimumDistributorPrice(Convert.ToInt32(cmbCreatePromoTierOn.SelectedValue), Convert.ToInt32(cmbCreatePromoTierCode.SelectedValue), ref errMsg);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        //TODO: HANDLE ERROR
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtCreatePromoTierDiscountValue.Text) <= 0)
                        {
                            errorMessage = Common.GetMessage("VAL0104", lblCreatePromoTierDiscountValue.Text.Replace(":", "").Replace("*", ""), "0");
                            errTier.SetError(txtCreatePromoTierDiscountValue, errorMessage);
                            errorMessages.Append(errorMessage);
                            errorMessages.Append(Environment.NewLine);
                            IsValidationSuccess = false;
                        }
                        else if (Convert.ToDecimal(minDistributorPrice) > 0)
                        {
                            if (Convert.ToDecimal(txtCreatePromoTierDiscountValue.Text) >= Convert.ToDecimal(minDistributorPrice))
                            {
                                errorMessage = Common.GetMessage("VAL0072", lblCreatePromoTierDiscountValue.Text.Replace(":", "").Replace("*", ""), "0").Replace(".", "") + " or greater than the minimum Distributor Price of " + minDistributorPrice + ".";
                                errTier.SetError(txtCreatePromoTierDiscountValue, errorMessage);
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                        }
                        else
                        {
                            errTier.SetError(txtCreatePromoTierDiscountValue, string.Empty);
                        }
                    }
                }
                else
                {
                    errTier.SetError(txtCreatePromoTierDiscountValue, string.Empty);
                }

                //If condition On is selected, Mandatory Quantity and should be greater than 0
                if (!Validators.IsValidQuantity(txtCreatePromoTierQty.Text) || !Validators.IsNonZero(txtCreatePromoTierQty.Text))
                {
                    errTier.SetError(txtCreatePromoTierQty, string.Empty);
                    errorMessage = Common.GetMessage("VAL0009", lblCreatePromoTierQty.Text.Replace(":*", ""));
                    errTier.SetError(txtCreatePromoTierQty, errorMessage);
                    errorMessages.Append(errorMessage);
                    errorMessages.Append(Environment.NewLine);
                    IsValidationSuccess = false;
                }
                else if (Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue) != (int)Promotion.DiscountType.FreeItem)
                {
                    if (Convert.ToDecimal(txtCreatePromoTierQty.Text) <= 0)
                    {
                        errorMessage = Common.GetMessage("VAL0033", lblCreatePromoTierQty.Text.Replace(":", ""));
                        errTier.SetError(txtCreatePromoTierQty, errorMessage);
                        errorMessages.Append(errorMessage);
                        errorMessages.Append(Environment.NewLine);
                        IsValidationSuccess = false;
                    }
                    else
                    {
                        errTier.SetError(txtCreatePromoTierQty, string.Empty);
                    }
                }
                else
                {
                    errTier.SetError(txtCreatePromoTierQty, string.Empty);
                }
            }

            if (cmbCreatePromoTierDiscountType.SelectedValue != null)
            {
                if (((int)cmbCreatePromoTierDiscountType.SelectedValue) == Common.INT_DBNULL)
                {
                    if (string.IsNullOrEmpty(errTier.GetError(cmbCreatePromoTierDiscountType)))
                    {
                        errorMessage = Common.GetMessage("VAL0002", lblCreatePromoTierDiscountType.Text.Replace(":", ""));
                        errTier.SetError(cmbCreatePromoTierDiscountType, errorMessage);
                        errorMessages.Append(errorMessage);
                        errorMessages.Append(Environment.NewLine);
                        IsValidationSuccess = false;
                    }
                }
            }

            if (((int)cmbCreatePromoTierDiscountType.SelectedValue) != (int)Promotion.DiscountType.FreeItem)
            {
                if (string.IsNullOrEmpty(txtCreatePromoTierDiscountValue.Text))
                {
                    errorMessage = Common.GetMessage("VAL0001", lblCreatePromoTierDiscountValue.Text.Replace(":", ""));
                    errTier.SetError(txtCreatePromoTierDiscountValue, errorMessage);
                    errorMessages.Append(errorMessage);
                    errorMessages.Append(Environment.NewLine);
                    IsValidationSuccess = false;
                }
            }

            ////If Discount Code is Selected than Discount Value is Mandatory
            //if (Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue) != Common.INT_DBNULL)
            //{
            //    if (Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue) != 4)
            //    {
            //        if (!Validators.IsDecimal(txtCreatePromoTierDiscountValue.Text))
            //        {
            //            errTier.SetError(txtCreatePromoTierDiscountValue, string.Empty);

            //            errorMessage = Common.GetMessage("VAL0009", lblCreatePromoTierDiscountValue.Text.Replace(":*", ""));
            //            errTier.SetError(txtCreatePromoTierDiscountValue, errorMessage);
            //            errorMessages.Append(errorMessage);
            //            errorMessages.Append(Environment.NewLine);
            //            IsValidationSuccess = false;
            //        }
            //        else if (Convert.ToDecimal(txtCreatePromoTierDiscountValue.Text) <= 0)
            //        {
            //            errTier.SetError(txtCreatePromoTierDiscountValue, string.Empty);

            //            errorMessage = Common.GetMessage("VAL0033", lblCreatePromoTierDiscountValue.Text.Replace(":", "").Replace("*", ""), "0");
            //            errTier.SetError(txtCreatePromoTierDiscountValue, errorMessage);
            //            errorMessages.Append(errorMessage);
            //            errorMessages.Append(Environment.NewLine);
            //            IsValidationSuccess = false;
            //        }
            //        else
            //        {
            //            errTier.SetError(txtCreatePromoTierDiscountValue, string.Empty);
            //        }
            //    }
            //}
            //else
            //{
            //    errorMessage = Common.GetMessage("VAL0002", lblCreatePromoTierDiscountType.Text.Replace(":", "").Replace("*", ""));
            //    errTier.SetError(cmbCreatePromoTierDiscountType, errorMessage);
            //    errorMessages.Append(errorMessage);
            //    errorMessages.Append(Environment.NewLine);
            //    IsValidationSuccess = false;
            //}

            if (Convert.ToInt32(cmbCreatePromoTierStatus.SelectedValue) == Common.INT_DBNULL)
            {
                errorMessage = Common.GetMessage("VAL0002", cmbCreatePromoTierStatus.Text.Replace(":", "").Replace("*", ""));
                errTier.SetError(cmbCreatePromoTierStatus, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else
            {
                errTier.SetError(cmbCreatePromoTierStatus, string.Empty);
            }

            errorMessages = Common.ReturnErrorMessage(errorMessages);
            if (!String.IsNullOrEmpty(errorMessage.ToString()))
            {
                MessageBox.Show(errorMessages.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return IsValidationSuccess;
        }

        /// <summary>
        /// Clear condition Tier Values
        /// </summary>
        private void ClearConditionTier()
        {
            if (condition.Tiers.Count > 0)
            {
                txtCreatePromoTierBuyQtyFrom.Enabled = false;
                txtCreatePromoTierBuyQtyFrom.Text = (Convert.ToInt32(condition.Tiers[condition.Tiers.Count - 1].BuyQtyTo) + 1).ToString();
            }
            else
            {
                txtCreatePromoTierBuyQtyFrom.Text = string.Empty;
                txtCreatePromoTierBuyQtyFrom.Enabled = true;
            }
            txtCreatePromoTierBuyQtyTo.Text = string.Empty;

            if (this.promotionType == (int)Promotion.PromotionCategoryType.Volume)
            {
                txtCreatePromoTierQty.Enabled = false;
                txtCreatePromoTierQty.Text = "1";
            }
            else
            {
                if (this.promotionType != (int)Promotion.PromotionCategoryType.BillBuster)
                {
                    txtCreatePromoTierQty.Enabled = true;
                    txtCreatePromoTierQty.Text = "0";
                }
            }

            cmbCreatePromoTierStatus.SelectedValue = Common.INT_DBNULL;
            cmbCreatePromoTierOn.SelectedValue = Common.INT_DBNULL;
            if (this.promotionType == (int)Promotion.PromotionCategoryType.BillBuster)
            {
                DataTable dtDiscountTypes = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_DISCOUNT_TYPE, 0, 0, 0));
                dtDiscountTypes.DefaultView.RowFilter = "KeyCode1 NOT IN (3,4)";
                dtDiscountTypes = dtDiscountTypes.DefaultView.ToTable();
                cmbCreatePromoTierDiscountType.DataSource = dtDiscountTypes;
            } 
            hierarchyLevelId = Common.INT_DBNULL;
            txtCreatePromoTierCode.Text = string.Empty;
            cmbCreatePromoTierDiscountType.SelectedValue = Common.INT_DBNULL;
            txtCreatePromoTierDiscountValue.Text = "0.00";
            btnCreatePromoTierAdd.Text = "A&dd";
            EnableDisableTier(TierOperationState.CLEAR);
        }

        /// <summary>
        /// Enable disable logic for Tier Tab
        /// </summary>
        /// <param name="state"></param>
        private void EnableDisableTier(TierOperationState state)
        {
            if (state == TierOperationState.EDIT)
            {
                txtCreatePromoTierBuyQtyTo.Enabled = false;
                cmbCreatePromoTierOn.Enabled = false;
                cmbCreatePromoTierCode.Enabled = false;
                txtCreatePromoTierCode.Enabled = false;
                btnCreatePromoTierCode.Enabled = false;

                errTier.Clear();
            }
            else if (state == TierOperationState.CLEAR)
            {
                txtCreatePromoTierBuyQtyTo.Enabled = true;
                cmbCreatePromoTierOn.Enabled = true;
                cmbCreatePromoTierCode.Enabled = true;
                txtCreatePromoTierCode.Enabled = true;
                btnCreatePromoTierCode.Enabled = true;

                errTier.Clear();
            }
        }

        /// <summary>
        /// Populate the screen based on condition object
        /// </summary>
        /// <param name="condition"></param>
        private void PopulateForm(PromotionCondition condition)
        {
            PopulateCombo();
            //Bind the grid
            this.dgvCreatePromoTier.SelectionChanged -= new System.EventHandler(this.dgvCreatePromoTier_SelectionChanged);
            dgvCreatePromoTier.DataSource = condition.Tiers; //(from p in condition.Tiers where p.StatusId != 3 select p); 
            this.dgvCreatePromoTier.SelectionChanged += new System.EventHandler(this.dgvCreatePromoTier_SelectionChanged);

            //Make sure that in the remaining tier(s), only the unsaved-records are available for deletion
            for (int index = 0; index < this.condition.Tiers.Count; index++)
            {
                if (condition.Tiers[index].TierId == -1)
                {
                    dgvCreatePromoTier.Rows[index].Cells["SavedRow"].Value = "-1";
                }
            }

            ClearConditionTier();
        }

        /// <summary>
        /// Populate condition Condition Values
        /// </summary>
        private void PopulatePromotionTier(PromotionTier tier)
        {
            txtCreatePromoTierBuyQtyFrom.Text = tier.DisplayBuyQtyFrom.ToString();
            txtCreatePromoTierBuyQtyTo.Text = tier.DisplayBuyQtyTo.ToString();
            txtCreatePromoTierQty.Text = tier.DisplayQty.ToString();
            cmbCreatePromoTierOn.SelectedValue = Convert.ToInt32(tier.ConditionOnId);
            cmbCreatePromoTierCode.SelectedValue = Convert.ToInt32(tier.ConditionCodeId);
            txtCreatePromoTierCode.Text = tier.ConditionOnVal;
            hierarchyLevelId = Convert.ToInt32(tier.ConditionCodeId);
            cmbCreatePromoTierDiscountType.SelectedValue = Convert.ToInt32(tier.DiscountTypeId);
            if (Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue) == (int)Promotion.DiscountType.FreeItem)
            {
                txtCreatePromoTierDiscountValue.Enabled = false;
            }
            else
            {
                txtCreatePromoTierDiscountValue.Enabled = true;
            }
            txtCreatePromoTierDiscountValue.Text = tier.DisplayDiscountValue.ToString();
            cmbCreatePromoTierStatus.SelectedValue = tier.StatusId;
        }

        /// <summary>
        /// This method would populate all the combo boxes from Parameter Master and related tables.
        /// This would called only once to get valid values for combo
        /// </summary>
        private void PopulateCombo()
        {
            //Bind Status Combobox

            DataTable dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_DISCOUNT_TYPE, 0, 0, 0));
            if (this.promotionType == (int)Promotion.PromotionCategoryType.Volume)
            {
                dtTemp.DefaultView.RowFilter = "KeyCode1 <> 4";
                dtTemp = dtTemp.DefaultView.ToTable();
            }
            else if (this.promotionType == (int)Promotion.PromotionCategoryType.BillBuster)
            {
                dtTemp.DefaultView.RowFilter = "KeyCode1 <> 3";
                dtTemp = dtTemp.DefaultView.ToTable();
            }
            cmbCreatePromoTierDiscountType.DataSource = dtTemp;
            cmbCreatePromoTierDiscountType.ValueMember = Common.KEYCODE1;
            cmbCreatePromoTierDiscountType.DisplayMember = Common.KEYVALUE1;

            dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_LOCSTATUS, 0, 0, 0));
            //Remove the 'Select' row
            dtTemp.Select(Common.KEYCODE1 + "=" + Common.INT_DBNULL.ToString())[0].Delete();
            dtTemp.AcceptChanges();

            dtTemp = dtTemp.Copy();
            cmbCreatePromoTierStatus.DataSource = dtTemp;
            cmbCreatePromoTierStatus.ValueMember = Common.KEYCODE1;
            cmbCreatePromoTierStatus.DisplayMember = Common.KEYVALUE1;

            //Condition-On for Tier is applicable only on Product, hence the RowFilter applied to the effect
            dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_CONDITIONON, 0, 0, 0));
            dtTemp.DefaultView.RowFilter = "KeyCode1 IN (-1,1)";
            dtTemp = dtTemp.DefaultView.ToTable();
            cmbCreatePromoTierOn.DataSource = dtTemp;
            cmbCreatePromoTierOn.ValueMember = Common.KEYCODE1;
            cmbCreatePromoTierOn.DisplayMember = Common.KEYVALUE1;
        }

        #endregion

        #region Enum


        private enum TierOperationState
        {
            EDIT,
            ADD,
            CLEAR,
        }

        private enum OperationState
        {
            EDIT,
            ADD,
            CLEAR,
        }

        #endregion

    }
}
