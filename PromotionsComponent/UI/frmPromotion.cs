using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Data.SqlClient;

//vinculum framwork namespace(s)
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using PromotionsComponent.BusinessLayer;
using CoreComponent.Hierarchies.UI;
using AuthenticationComponent.BusinessObjects;
using CoreComponent.Hierarchies.BusinessObjects;
using System.IO;
using System.Configuration;
using System.Net;

namespace PromotionsComponent.UI
{
    /// <summary>
    /// TODO: User Role/Right check
    /// </summary>
    public partial class frmPromotion : CoreComponent.Core.UI.Transaction
    {
        #region Variables
        //Promotion object for the screen
        string ItemImage = "";
        string LoadedImage = string.Empty;
        private string Filepath = "";
        Promotion promotion = null;
        PromotionCondition condition = null;
        PromotionLocation location = null;
        PromotionTier tier = null;
        string CurrentEnvironmentPath = Environment.CurrentDirectory;
        string EnvironmentPath = Environment.CurrentDirectory;
        private int hierarchyLevelId = Common.INT_DBNULL;

        //Edit button options
        private const string CON_LOCATION_EDIT = "EDIT";
        private const string CON_LOCATION_REMOVE = "REMOVE";
        private const string CON_TIER_REMOVE = "REMOVE";
        private const string CON_CONDITION_TIER_REMOVE = "REMOVE";
        private const string CON_CONDITION_TIER_ADD = "ADDTIER";
        private const string CON_CONDITION_EDIT = "EDIT";
        private const string CON_SEARCH_VIEW = "VIEW";
        private const string CON_SEARCH_PROMOTION_ID = "PromotionId";
        private const int CON_SEARCH_PROMOTION_DISCOUNTTYPE = 1;
        private const int CON_SAVE_PROMOTION_DISCOUNTTYPE = 2;
        private string CON_MODULENAME = string.Empty;

        private bool m_suspendEventHandler = false;
        private bool m_isSearchAvailable = false;
        private bool m_isSaveAvailable = false;

        //User Info
        private int userId = Authenticate.LoggedInUser.UserId;
        private string userName = Authenticate.LoggedInUser.UserName;
        //Location Info
        private string locationCode = Common.LocationCode;
        #endregion

        #region CTOR
        public frmPromotion()
        {
            InitializeComponent();

            lblPageTitle.Text = "Promotion";
            dgvCreatePromoCondition.AutoGenerateColumns = false;
            dgvCreatePromoCondition =
                Common.GetDataGridViewColumns(dgvCreatePromoCondition,
                Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

            dgvCreatePromoLocation.AutoGenerateColumns = false;
            dgvCreatePromoLocation =
                Common.GetDataGridViewColumns(dgvCreatePromoLocation,
                Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

            dgvCreatePromoTier =
                Common.GetDataGridViewColumns(dgvCreatePromoTier,
                Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

            dgvSearchPromo =
                Common.GetDataGridViewColumns(dgvSearchPromo,
                Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml");

            dtpSearchPromoStartDate.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpSearchPromoEndDate.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpCreatePromoStartDate.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpCreatePromoEndDate.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpCreatePromoDurationStart.CustomFormat = Common.TIME_FORMAT;
            dtpCreatePromoDurationEnd.CustomFormat = Common.TIME_FORMAT;

            //Create promotion Hierarchy
            promotion = new Promotion();
        }
        #endregion

        #region Methods

        private void InitializeRights()
        {
            if (AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser != null)
            {
                userId = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
                userName = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;
            }

            if (userName != null && !CON_MODULENAME.Equals(string.Empty))
            {
                m_isSaveAvailable = Authenticate.IsFunctionAccessible(userName, locationCode, LocationHierarchy.MODULE_CODE, Common.FUNCTIONCODE_SAVE);
                m_isSearchAvailable = Authenticate.IsFunctionAccessible(userName, locationCode, LocationHierarchy.MODULE_CODE, Common.FUNCTIONCODE_SEARCH);
            }

            btnSearch.Enabled = m_isSearchAvailable;
            btnSave.Enabled = m_isSaveAvailable;
        }

        /// <summary>
        /// This method would validate the form for valid values and display error symbols and messages
        /// </summary>
        /// <returns>true when valid/ false otherwise</returns>
        private Boolean ValidateForm()
        {
            Boolean IsValidationSuccess = true;

            StringBuilder errorMessages = new StringBuilder();
            string errorMessage = string.Empty;

            //CHECK NOT IN USE, CURRENTLY
            /*
            //Check for any pending transaction left for Location/Tier/Condition
            if (btnCreatePromoConditionAdd.Text == "Save")
            {
                DialogResult confirmresult = MessageBox.Show(Common.GetMessage("INF0061", "Promotion Condition"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmresult == DialogResult.Yes)
                {
                    IsValidationSuccess = true;
                }
                else
                {
                    return false;
                }
            }
            else if (btnCreatePromoLocationAdd.Text == "Save")
            {
                DialogResult confirmresult = MessageBox.Show(Common.GetMessage("INF0061", "Promotion Location"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmresult == DialogResult.Yes)
                {
                    IsValidationSuccess = true;
                }
                else
                {
                    return false;
                }
            }
            else if (btnCreatePromoTierAdd.Text == "Save")
            {
                DialogResult confirmresult = MessageBox.Show(Common.GetMessage("INF0061", "Promotion Tier"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmresult == DialogResult.Yes)
                {
                    IsValidationSuccess = true;
                }
                else
                {
                    return false;
                }
            }
             * */

            //Mandatory Promotion Name Check
            if (Validators.CheckForEmptyString(txtCreatePromotionName.Text.Length))
            {
                errorMessage = Common.GetMessage("VAL0001", lblCreatePromotionName.Text.Replace(":*", "").Replace("*", ""));
                errorPromotion.SetError(txtCreatePromotionName, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else
            {
                errorPromotion.SetError(txtCreatePromotionName, string.Empty);
            }

            //If Category is Mandatory to select 
            if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == Common.INT_DBNULL)
            {
                errorMessage = Common.GetMessage("VAL0002", lblCreatePromoCategory.Text.Replace(":*", "").Replace("*", ""));
                errorPromotion.SetError(cmbCreatePromoCategory, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else
            {
                errorPromotion.SetError(cmbCreatePromoCategory, string.Empty);
            }

            //Mandatory Promotion Code Check
            if (Validators.CheckForEmptyString(txtCreatePromoCode.Text.Length))
            {
                errorMessage = Common.GetMessage("VAL0001", lblCreatePromoCode.Text.Replace(":*", ""));
                errorPromotion.SetError(txtCreatePromoCode, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else
            {
                string codeError = Common.CodeValidate(txtCreatePromoCode.Text.Trim(), lblCreatePromoCode.Text.Replace(":", "").Replace("*", ""));
                if (!string.IsNullOrEmpty(codeError))
                {
                    errorPromotion.SetError(txtCreatePromoCode, codeError);
                }
                else
                {
                    errorPromotion.SetError(txtCreatePromoCode, string.Empty);
                }
            }

            //StartDate and End Date Check
            if ((Convert.ToInt32(dtpCreatePromoStartDate.Value.ToString("yyyyMMdd")) - Convert.ToInt32(dtpCreatePromoEndDate.Value.ToString("yyyyMMdd"))) > 0)
            {
                errorMessage = Common.GetMessage("VAL0047", lblCreatePromoEndDate.Text.Replace(":*", ""), lblCreatePromoStartDate.Text.Replace(":*", ""));
                errorPromotion.SetError(dtpCreatePromoStartDate, errorMessage);
                errorPromotion.SetError(dtpCreatePromoEndDate, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else
            {
                errorPromotion.SetError(dtpCreatePromoStartDate, string.Empty);
                errorPromotion.SetError(dtpCreatePromoEndDate, string.Empty);
            }

            //Duration time validation
            if (dtpCreatePromoDurationStart.Checked == true & dtpCreatePromoDurationEnd.Checked == false)
            {
                errorMessage = Common.GetMessage("VAL0001", lblCreateDurationEndTime.Text.Replace(":", "").Replace("*", ""));
                errorPromotion.SetError(dtpCreatePromoDurationEnd, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else if (dtpCreatePromoDurationStart.Checked == false & dtpCreatePromoDurationEnd.Checked == true)
            {
                errorMessage = Common.GetMessage("VAL0001", lblCreateDurationStartTime.Text.Replace(":", "").Replace("*", ""));
                errorPromotion.SetError(dtpCreatePromoDurationStart, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else if (dtpCreatePromoDurationStart.Checked && dtpCreatePromoDurationEnd.Checked)
            {
                if ((Convert.ToInt32(dtpCreatePromoDurationStart.Value.ToString("HHmmss")) - Convert.ToInt32(dtpCreatePromoDurationStart.Value.ToString("HHmmss"))) > 0)
                {
                    errorMessage = Common.GetMessage("VAL0047", lblCreateDurationEndTime.Text.Replace(":", "").Replace("*", ""), lblCreateDurationStartTime.Text.Replace(":", "").Replace("*", ""));
                    errorPromotion.SetError(dtpCreatePromoDurationStart, errorMessage);
                    errorPromotion.SetError(dtpCreatePromoDurationEnd, errorMessage);
                    errorMessages.Append(errorMessage);
                    errorMessages.Append(Environment.NewLine);
                    IsValidationSuccess = false;
                }
            }
            else
            {
                errorPromotion.SetError(dtpCreatePromoDurationStart, string.Empty);
                errorPromotion.SetError(dtpCreatePromoDurationEnd, string.Empty);
            }

            //Discount Value
            if (Validators.CheckForEmptyString(txtCreatePromoDiscountValue.Text.Length))
            {
                errorMessage = Common.GetMessage("VAL0001", lblCreatePromoDiscountValue.Text.Replace(":*", ""));
                errorPromotion.SetError(txtCreatePromoDiscountValue, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else if (!Validators.IsDecimal(txtCreatePromoDiscountValue.Text))
            {
                //Buy From Qty should be decimal
                errorMessage = Common.GetMessage("VAL0009", lblCreatePromoDiscountValue.Text.Replace(":*", ""));
                errorPromotion.SetError(txtCreatePromoDiscountValue, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else
            {
                errorPromotion.SetError(txtCreatePromoDiscountValue, string.Empty);
            }

            //Max order Quantity
            if (Validators.CheckForEmptyString(txtCreatePromoMaxOrderQty.Text.Length))
            {
                errorMessage = Common.GetMessage("VAL0001", lblCreatePromoMaxOrderQty.Text.Replace(":*", ""));
                errorPromotion.SetError(txtCreatePromoMaxOrderQty, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else if (!Validators.IsDecimal(txtCreatePromoMaxOrderQty.Text))
            {
                //Buy From Qty should be decimal
                errorMessage = Common.GetMessage("VAL0009", lblCreatePromoMaxOrderQty.Text.Replace(":*", ""));
                errorPromotion.SetError(txtCreatePromoMaxOrderQty, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else
            {
                errorPromotion.SetError(txtCreatePromoMaxOrderQty, string.Empty);
            }

            //Promotion-Applicability
            if (Convert.ToInt32(cmbCreatePromotionApp.SelectedValue) == Common.INT_DBNULL)
            {
                errorMessage = Common.GetMessage("VAL0002", lblCreatePromotionApp.Text.Replace(":*", "").Replace("*", ""));
                errorPromotion.SetError(cmbCreatePromotionApp, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else
            {
                errorPromotion.SetError(cmbCreatePromotionApp, string.Empty);
            }

            //If repeat factor is given it should be integer greater than or equal to 0
            if (Validators.CheckForEmptyString(txtCreatePromoRepeat.Text.Length))
            {
                errorMessage = Common.GetMessage("VAL0001", lblCreatePromoRepeat.Text.Replace(":*", ""));
                errorPromotion.SetError(txtCreatePromoRepeat, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else if (!Validators.IsValidQuantity(txtCreatePromoRepeat.Text))
            {
                //Repeat factor should be integer
                errorMessage = Common.GetMessage("VAL0009", lblCreatePromoRepeat.Text.Replace(":*", ""));
                errorPromotion.SetError(txtCreatePromoRepeat, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else
            {
                errorPromotion.SetError(txtCreatePromoRepeat, string.Empty);
            }

            //Depending on the Type of promotion category selected : Check for if condition, condition-tier or location is defined
            if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) != Common.INT_DBNULL)
            {
                switch (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue))
                {
                    case (int)Promotion.PromotionCategoryType.Line:
                        {
                            #region Validate line promotion
                            //Condition and location is must
                            if (promotion.Conditions.Count == 0)
                            {
                                errorMessage = Common.GetMessage("VAL0062", "Conditions", Promotion.PromotionCategoryType.Line.ToString());
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                int countActiveConditions = (from p in promotion.Conditions
                                                             where p.StatusId == 1
                                                             select p).Count();
                                if (countActiveConditions == 0)
                                {
                                    errorMessage = Common.GetMessage("INF0117", "Promotion", "Condition");
                                    errorMessages.Append(errorMessage);
                                    errorMessages.Append(Environment.NewLine);
                                    IsValidationSuccess = false;
                                }
                            }

                            if (promotion.Locations.Count == 0)
                            {
                                errorMessage = Common.GetMessage("VAL0062", "Locations", Promotion.PromotionCategoryType.Line.ToString());
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                int countActiveLocations = (from p in promotion.Locations
                                                            where p.StatusId == 1
                                                            select p).Count();
                                if (countActiveLocations == 0)
                                {
                                    errorMessage = Common.GetMessage("INF0117", "Promotion", "Location");
                                    errorMessages.Append(errorMessage);
                                    errorMessages.Append(Environment.NewLine);
                                    IsValidationSuccess = false;
                                }
                            }

                            break;
                            #endregion
                        }

                    case (int)Promotion.PromotionCategoryType.Quantity:
                        {
                            #region Validate Quantity promotion
                            //Condition and location is must
                            if (promotion.Conditions.Count == 0)
                            {
                                errorMessage = Common.GetMessage("VAL0062", "Conditions", Promotion.PromotionCategoryType.Quantity.ToString());
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                //Check whether at least one Buy and one Get condition is Active;
                                int countBuyConditions = (from p in promotion.Conditions
                                                          where p.ConditionTypeId == 1 && p.StatusId == 1
                                                          select p).Count();
                                int countGetConditions = (from p in promotion.Conditions
                                                          where p.ConditionTypeId == 2 && p.StatusId == 1
                                                          select p).Count();
                                if ((countBuyConditions == 0) || (countGetConditions == 0))
                                {
                                    errorMessage = Common.GetMessage("INF0118", "Buy", "Get");
                                    errorMessages.Append(errorMessage);
                                    errorMessages.Append(Environment.NewLine);
                                    IsValidationSuccess = false;
                                }
                                //else
                                //{
                                //    //Check whether Buy Condition is on a Product, in case Get condition(s) have a free-item
                                //    int count = 0;
                                //    count = (from p in promotion.Conditions
                                //             where p.ConditionTypeId == 2 && p.DiscountTypeId == 4
                                //             select p).Count();
                                //    if (count > 0)
                                //    {
                                //        count = 0;
                                //        count = (from p in promotion.Conditions
                                //                 where p.ConditionTypeId == 1 && p.ConditionOnId == 1 
                                //                 select p).Count();
                                //        if (count == 0)
                                //        {
                                //            errorMessage = Common.GetMessage("VAL0093");
                                //            errorMessages.Append(errorMessage);
                                //            errorMessages.Append(Environment.NewLine);
                                //            IsValidationSuccess = false;
                                //        }
                                //    }
                                //}
                            }

                            if (promotion.Locations.Count == 0)
                            {
                                errorMessage = Common.GetMessage("VAL0062", "Locations", Promotion.PromotionCategoryType.Quantity.ToString());
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                int countActiveLocations = (from p in promotion.Locations
                                                            where p.StatusId == 1
                                                            select p).Count();
                                if (countActiveLocations == 0)
                                {
                                    errorMessage = Common.GetMessage("INF0117", "Promotion", "Location");
                                    errorMessages.Append(errorMessage);
                                    errorMessages.Append(Environment.NewLine);
                                    IsValidationSuccess = false;
                                }
                            }

                            break;
                            #endregion
                        }

                    //Bill buster and Volumn would be handled in the same way    
                    case (int)Promotion.PromotionCategoryType.BillBuster:
                        {
                            #region Validate Bill Buster
                            //Condition,Condition Tier and location is must
                            if (promotion.Conditions.Count == 0)
                            {
                                errorMessage = Common.GetMessage("VAL0062", "Conditions", Promotion.PromotionCategoryType.BillBuster.ToString());
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                //Check whether at least one condition is Active;
                                int count = (from p in promotion.Conditions
                                             where p.ConditionTypeId == 1 && p.StatusId == 1
                                             select p).Count();
                                if (count == 0)
                                {
                                    errorMessage = Common.GetMessage("INF0117", "Buy", "Condition");
                                    errorMessages.Append(errorMessage);
                                    errorMessages.Append(Environment.NewLine);
                                    IsValidationSuccess = false;
                                }
                            }

                            for (int i = 0; i < promotion.Conditions.Count; i++)
                            {
                                if (promotion.Conditions[i].Tiers.Count == 0)
                                {
                                    errorMessage = Common.GetMessage("VAL0062", "Condition-Tiers", Promotion.PromotionCategoryType.BillBuster.ToString());
                                    errorMessages.Append(errorMessage);
                                    errorMessages.Append(Environment.NewLine);
                                    IsValidationSuccess = false;
                                    break;
                                }
                                else
                                {
                                    int countActiveTiers = (from p in promotion.Conditions[i].Tiers
                                                            where p.StatusId == 1
                                                            select p).Count();
                                    if (countActiveTiers == 0)
                                    {
                                        errorMessage = Common.GetMessage("INF0119", "Active Tier", "should be present in each Condition", "for " + Promotion.PromotionCategoryType.BillBuster.ToString() + " promotion");
                                        errorMessages.Append(errorMessage);
                                        errorMessages.Append(Environment.NewLine);
                                        IsValidationSuccess = false;
                                        break;
                                    }
                                }
                            }

                            if (promotion.Locations.Count == 0)
                            {
                                errorMessage = Common.GetMessage("VAL0062", "Locations", Promotion.PromotionCategoryType.BillBuster.ToString());
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                int count = (from p in promotion.Locations
                                             where p.StatusId == 1
                                             select p).Count();
                                if (count == 0)
                                {
                                    errorMessage = Common.GetMessage("INF0117", "Promotion", "Location");
                                    errorMessages.Append(errorMessage);
                                    errorMessages.Append(Environment.NewLine);
                                    IsValidationSuccess = false;
                                }
                            }
                            break;
                            #endregion
                        }

                    case (int)Promotion.PromotionCategoryType.Volume:
                        {
                            #region Validate Disable Volumn
                            //Condition,Condition Tier and location is must
                            if (promotion.Conditions.Count == 0)
                            {
                                errorMessage = Common.GetMessage("VAL0062", "Conditions", Promotion.PromotionCategoryType.Volume.ToString());
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                //Check whether at least one condition is Active;
                                int count = (from p in promotion.Conditions
                                             where p.ConditionTypeId == 1 && p.StatusId == 1
                                             select p).Count();
                                if (count == 0)
                                {
                                    errorMessage = Common.GetMessage("INF0117", "Buy", "Condition");
                                    errorMessages.Append(errorMessage);
                                    errorMessages.Append(Environment.NewLine);
                                    IsValidationSuccess = false;
                                }
                            }

                            for (int i = 0; i < promotion.Conditions.Count; i++)
                            {
                                if (promotion.Conditions[i].Tiers.Count == 0)
                                {
                                    errorMessage = Common.GetMessage("VAL0062", "Condition-Tiers", Promotion.PromotionCategoryType.Volume.ToString());
                                    errorMessages.Append(errorMessage);
                                    errorMessages.Append(Environment.NewLine);
                                    IsValidationSuccess = false;
                                    break;
                                }
                                else
                                {
                                    int countActiveTiers = (from p in promotion.Conditions[i].Tiers
                                                            where p.StatusId == 1
                                                            select p).Count();
                                    if (countActiveTiers == 0)
                                    {
                                        errorMessage = Common.GetMessage("INF0119", "Active Tier", "should be present in each Condition", "for " + Promotion.PromotionCategoryType.Volume.ToString() + " promotion");
                                        errorMessages.Append(errorMessage);
                                        errorMessages.Append(Environment.NewLine);
                                        IsValidationSuccess = false;
                                        break;
                                    }
                                }
                            }
                            if (promotion.Locations.Count == 0)
                            {
                                errorMessage = Common.GetMessage("VAL0062", "Locations", Promotion.PromotionCategoryType.Volume.ToString());
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                int count = (from p in promotion.Locations
                                             where p.StatusId == 1
                                             select p).Count();
                                if (count == 0)
                                {
                                    errorMessage = Common.GetMessage("INF0117", "Promotion", "Location");
                                    errorMessages.Append(errorMessage);
                                    errorMessages.Append(Environment.NewLine);
                                    IsValidationSuccess = false;
                                }
                            }
                            break;
                            #endregion
                        }
                }
            }

            errorMessages = Common.ReturnErrorMessage(errorMessages);
            if (!String.IsNullOrEmpty(errorMessages.ToString()))
            {
                MessageBox.Show(errorMessages.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return IsValidationSuccess;
        }

        /// <summary>
        /// Validate the promotion condition tab
        /// </summary>
        /// <returns></returns>
        private Boolean ValidateCondition()
        {
            Boolean IsValidationSuccess = true;

            StringBuilder errorMessages = new StringBuilder();
            string errorMessage = string.Empty;

            if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) != Common.INT_DBNULL)
            {
                if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) != (int)Promotion.PromotionCategoryType.Quantity)
                {
                    if (promotion.Conditions.Count == 1)
                    {
                        //if (promotion.Conditions[0].ConditionId != Common.INT_DBNULL)
                        //{
                        //    if (btnCreatePromoConditionAdd.Text == "Add")
                        //    {
                        //        IsValidationSuccess = false;
                        //    }
                        //}
                        //else
                        //{
                        //    if (btnCreatePromoConditionAdd.Text == "Add")
                        //    {
                        //        IsValidationSuccess = false;
                        //    }
                        //}

                        if (btnCreatePromoConditionAdd.Text == "A&dd")
                        {
                            IsValidationSuccess = false;
                        }
                    }

                    if (!IsValidationSuccess)
                    {
                        IsValidationSuccess = false;
                        string promoCategory = string.Empty;
                        switch (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue))
                        {
                            case 1:
                                {
                                    promoCategory = "Bill Buster";
                                }
                                break;

                            case 2:
                                {
                                    promoCategory = "Line";
                                }
                                break;

                            case 3:
                                {
                                    promoCategory = "Quantity";
                                }
                                break;

                            case 4:
                                {
                                    promoCategory = "Volume";
                                }
                                break;
                        }
                        MessageBox.Show(Common.GetMessage("VAL0095", promoCategory + " Promotion", "1 applicable-condition"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return IsValidationSuccess;
                    }

                    //if (promotion.Conditions.Count == 1)
                    //{
                    //    IsValidationSuccess = false;
                    //    string promoCategory = string.Empty;
                    //    switch (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue))
                    //    {
                    //        case 1:
                    //            {
                    //                promoCategory = "Bill Buster";
                    //            }
                    //            break;

                    //        case 2:
                    //            {
                    //                promoCategory = "Line";
                    //            }
                    //            break;

                    //        case 3:
                    //            {
                    //                promoCategory = "Quantity";
                    //            }
                    //            break;

                    //        case 4:
                    //            {
                    //                promoCategory = "Volume";
                    //            }
                    //            break;
                    //    }
                    //    MessageBox.Show(Common.GetMessage("VAL0095", promoCategory + " Promotion", "1 applicable-condition"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    //errorMessage = Common.GetMessage("VAL0095", promoCategory + " Promotion", "1 applicable-condition");
                    //    //errorMessages.Append(errorMessage);
                    //    //errorMessages.Append(Environment.NewLine);
                    //    return IsValidationSuccess;
                    //}
                }
            }
            if ((Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) != Common.INT_DBNULL) && (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) != (int)Promotion.PromotionCategoryType.Volume) && (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) != (int)Promotion.PromotionCategoryType.BillBuster))
            {
                if (!Validators.IsValidQuantity(txtCreatePromoConditionQty.Text) || !Validators.IsNonZero(txtCreatePromoConditionQty.Text))
                {
                    errorMessage = Common.GetMessage("VAL0009", lblCreatePromoConditionQty.Text.Replace(":*", ""));
                    errorPromotion.SetError(txtCreatePromoConditionQty, errorMessage);
                    errorMessages.Append(errorMessage);
                    errorMessages.Append(Environment.NewLine);
                    IsValidationSuccess = false;
                }
                else
                {
                    errorPromotion.SetError(txtCreatePromoConditionQty, string.Empty);
                }
            }
            else
            {
                errorPromotion.SetError(txtCreatePromoConditionQty, string.Empty);
            }

            //Mandatory Condition Type
            if (Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) == Common.INT_DBNULL)
            {
                errorMessage = Common.GetMessage("VAL0002", lblCreatePromoConditionType.Text.Replace(":*", ""));
                errorPromotion.SetError(cmbCreatePromoConditionType, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else
            {
                errorPromotion.SetError(cmbCreatePromoConditionType, string.Empty);
            }

            switch (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue))
            {
                case (int)Promotion.PromotionCategoryType.Line:
                    {
                        #region Validations on Condition when promotion category is line
                        //Mandatory Condition On
                        if (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Common.INT_DBNULL)
                        {
                            errorMessage = Common.GetMessage("VAL0002", lblCreatePromoConditionOn.Text.Replace(":*", ""));
                            errorPromotion.SetError(cmbCreatePromoConditionOn, errorMessage);
                            errorMessages.Append(errorMessage);
                            errorMessages.Append(Environment.NewLine);
                            IsValidationSuccess = false;
                        }
                        else
                        {
                            errorPromotion.SetError(cmbCreatePromoConditionOn, string.Empty);
                        }

                        //The Condition Type, Conditon On, Condition Code must be unique with in the conditions
                        int conditionCode = Common.INT_DBNULL;

                        if ((Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCT))
                            | (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCTGROUP)))
                        {
                            conditionCode = Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue);
                        }
                        else
                        {
                            conditionCode = hierarchyLevelId;
                        }

                        //Mandatory Condition Code
                        if (conditionCode == Common.INT_DBNULL)
                        {
                            errorMessage = Common.GetMessage("VAL0002", lblCreatePromoConditionCode.Text.Replace(":*", ""));
                            errorPromotion.SetError(cmbCreatePromoConditionCode, errorMessage);
                            errorMessages.Append(errorMessage);
                            errorMessages.Append(Environment.NewLine);
                            IsValidationSuccess = false;
                        }
                        else
                        {
                            Promotion objTemp = new Promotion();
                            string errMsg = string.Empty;
                            if (btnCreatePromoConditionAdd.Text == "A&dd")
                            {
                                if (!objTemp.ValidatePromotionAgainstExisiting(Convert.ToInt32(cmbCreatePromoCategory.SelectedValue),
                                                                            Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue),
                                                                            conditionCode,
                                    //Convert.ToDateTime(dtpCreatePromoStartDate.Value.ToString(Common.DTP_DATE_FORMAT)),
                                                                            dtpCreatePromoStartDate.Value,
                                    //Convert.ToDateTime(dtpCreatePromoEndDate.Value.ToString(Common.DTP_DATE_FORMAT)), 
                                                                            dtpCreatePromoEndDate.Value,
                                                                            ref errMsg))
                                {
                                    errorMessage = Common.GetMessage("VAL0106", lblCreatePromoConditionCode.Text.Replace(":", "").Replace("*", ""));
                                    errorPromotion.SetError(cmbCreatePromoConditionCode, errorMessage);
                                    errorMessages.Append(errorMessage);
                                    errorMessages.Append(Environment.NewLine);
                                    IsValidationSuccess = false;
                                }
                                else
                                {
                                    errorPromotion.SetError(cmbCreatePromoConditionCode, string.Empty);
                                }
                            }
                        }

                        //Mandatory Quantity and should be greater than 0
                        if (!Validators.IsDecimal(txtCreatePromoConditionQty.Text))
                        {
                            errorPromotion.SetError(txtCreatePromoConditionQty, string.Empty);
                            errorMessage = Common.GetMessage("VAL0009", lblCreatePromoConditionQty.Text.Replace(":*", ""));
                            errorPromotion.SetError(txtCreatePromoConditionQty, errorMessage);
                            errorMessages.Append(errorMessage);
                            errorMessages.Append(Environment.NewLine);
                            IsValidationSuccess = false;
                        }
                        else if (Convert.ToDecimal(txtCreatePromoConditionQty.Text) <= 0)
                        {
                            errorPromotion.SetError(txtCreatePromoConditionQty, string.Empty);
                            errorMessage = Common.GetMessage("VAL0033", lblCreatePromoConditionQty.Text.Replace(":", ""));
                            errorPromotion.SetError(txtCreatePromoConditionQty, errorMessage);
                            errorMessages.Append(errorMessage);
                            errorMessages.Append(Environment.NewLine);
                            IsValidationSuccess = false;
                        }
                        else
                        {
                            errorPromotion.SetError(txtCreatePromoConditionQty, string.Empty);
                        }

                        if (Convert.ToInt32(cmbCreatePromoConditionDiscountType.SelectedValue) == Common.INT_DBNULL)
                        {
                            errorMessage = Common.GetMessage("VAL0002", lblCreatePromoConditionDiscountValue.Text.Replace(":", ""));
                            errorPromotion.SetError(cmbCreatePromoConditionDiscountType, errorMessage);
                            errorMessages.Append(errorMessage);
                            errorMessages.Append(Environment.NewLine);
                            IsValidationSuccess = false;
                        }
                        else if (Convert.ToInt32(cmbCreatePromoConditionDiscountType.SelectedValue) == 4)
                        {
                            errorMessage = Common.GetMessage("VAL0091", lblCreatePromoConditionDiscountValue.Text.Replace(":", ""));
                            errorPromotion.SetError(cmbCreatePromoConditionDiscountType, errorMessage);
                            errorMessages.Append(errorMessage);
                            errorMessages.Append(Environment.NewLine);
                            IsValidationSuccess = false;
                        }
                        else
                        {
                            errorPromotion.SetError(cmbCreatePromoConditionDiscountType, string.Empty);
                        }

                        if (cmbCreatePromoConditionCode.SelectedValue != null)
                        {
                            if ((Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) != Common.INT_DBNULL) &&
                                (Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue) != Common.INT_DBNULL))
                            {
                                string errMsg = string.Empty;
                                PromotionCondition tempObj = new PromotionCondition();
                                double minDistributorPrice = tempObj.FetchMinimumDistributorPrice(Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue), Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue), ref errMsg);
                                if (!Validators.IsDecimal(txtCreatePromoConditionDiscountValue.Text))
                                {
                                    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);

                                    errorMessage = Common.GetMessage("VAL0009", lblCreatePromoConditionDiscountValue.Text.Replace(":*", ""));
                                    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                    errorMessages.Append(errorMessage);
                                    errorMessages.Append(Environment.NewLine);
                                    IsValidationSuccess = false;
                                }
                                else if (Convert.ToInt32(cmbCreatePromoConditionDiscountType.SelectedValue) == 1)
                                {
                                    if ((Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) <= 0) ||
                                        (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) >= 100))
                                    {
                                        errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);

                                        errorMessage = Common.GetMessage("VAL0033", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0").Replace(".", "") + " and less than 100.";
                                        errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                        errorMessages.Append(errorMessage);
                                        errorMessages.Append(Environment.NewLine);
                                        IsValidationSuccess = false;
                                    }
                                    else
                                    {
                                        errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);
                                    }
                                }
                                else if (Convert.ToInt32(cmbCreatePromoConditionDiscountType.SelectedValue) == 2)
                                {
                                    //string errMsg = "";

                                    //double minDistributorPrice = this.condition.FetchMinimumDistributorPrice(Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue), Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue), ref errMsg);
                                    if (!string.IsNullOrEmpty(errMsg))
                                    {
                                        //TODO: HANDLE ERROR
                                    }
                                    else
                                    {
                                        errorMessage = string.Empty;
                                        bool valid = true;

                                        if (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) <= 0)
                                        {
                                            errorMessage = Common.GetMessage("VAL0104", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0");
                                            valid = false;
                                        }
                                        else if (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) >= Convert.ToDecimal(minDistributorPrice))
                                        {
                                            errorMessage = Common.GetMessage("VAL0105", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "minimum Distributor Price of " + minDistributorPrice);// " or greater than the minimum Distributor Price of " + minDistributorPrice + ".";
                                            valid = false;
                                        }

                                        errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                        errorMessages.Append(errorMessage);

                                        if (!valid)
                                        {
                                            IsValidationSuccess = false;
                                            errorMessages.Append(Environment.NewLine);
                                        }

                                        //if ((Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) <= 0) ||
                                        //    (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) >= Convert.ToDecimal(minDistributorPrice)))
                                        //{
                                        //    errorMessage = Common.GetMessage("VAL0072", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0") + Environment.NewLine + Common.GetMessage("VAL0104", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "minimum Distributor Price of " + minDistributorPrice);// " or greater than the minimum Distributor Price of " + minDistributorPrice + ".";
                                        //    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                        //    errorMessages.Append(errorMessage);
                                        //    errorMessages.Append(Environment.NewLine);
                                        //    IsValidationSuccess = false;
                                        //}
                                        //else
                                        //{
                                        //    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);
                                        //}
                                    }
                                }
                                else if (Convert.ToInt32(cmbCreatePromoConditionDiscountType.SelectedValue) == 3)
                                {
                                    //string errMsg = "";

                                    //double minDistributorPrice = this.condition.FetchMinimumDistributorPrice(Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue), Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue), ref errMsg);
                                    if (!string.IsNullOrEmpty(errMsg))
                                    {
                                        //TODO: HANDLE ERROR
                                    }
                                    else
                                    {
                                        //if (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) < 0)
                                        //{
                                        //    errorMessage = Common.GetMessage("INF0059", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0");
                                        //    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                        //    errorMessages.Append(errorMessage);
                                        //    errorMessages.Append(Environment.NewLine);
                                        //    IsValidationSuccess = false;
                                        //}
                                        //else if (Convert.ToDecimal(minDistributorPrice) > 0)
                                        //{
                                        //    if (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) >= Convert.ToDecimal(minDistributorPrice))
                                        //    {
                                        //        //errorMessage = Common.GetMessage("VAL0072", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0").Replace(".", "") + " or greater than the minimum Distributor Price of " + minDistributorPrice + ".";
                                        //        errorMessage = Common.GetMessage("VAL0072", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0") + Environment.NewLine + Common.GetMessage("VAL0104", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "minimum Distributor Price of " + minDistributorPrice);
                                        //        errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                        //        errorMessages.Append(errorMessage);
                                        //        errorMessages.Append(Environment.NewLine);
                                        //        IsValidationSuccess = false;
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);
                                        //}

                                        errorMessage = string.Empty;
                                        bool valid = true;

                                        if (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) <= 0)
                                        {
                                            errorMessage = Common.GetMessage("VAL0104", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0");
                                            valid = false;
                                        }
                                        else if (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) >= Convert.ToDecimal(minDistributorPrice))
                                        {
                                            errorMessage = Common.GetMessage("VAL0105", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "minimum Distributor Price of " + minDistributorPrice);// " or greater than the minimum Distributor Price of " + minDistributorPrice + ".";
                                            valid = false;
                                        }

                                        errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                        errorMessages.Append(errorMessage);

                                        if (!valid)
                                        {
                                            IsValidationSuccess = false;
                                            errorMessages.Append(Environment.NewLine);
                                        }
                                    }
                                }
                                else
                                {
                                    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);
                                }
                            }
                        }

                        int count = (from p in promotion.Conditions
                                     where p.ConditionTypeId == Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) &&
                                           p.ConditionOnId == Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) &&
                                           p.ConditionCodeId == conditionCode
                                     select p).Count();

                        //In case of edit mode there should be one record exists for this combination
                        if (btnCreatePromoConditionAdd.Text == "S&ave")
                        {
                            //Duplicate should exists
                            if (count <= 0)
                            {
                                throw new InvalidOperationException("Screen state is invalid");
                            }
                        }
                        else
                        {
                            //if duplicate exists
                            if (count > 0)
                            {
                                //Record already exists can't insert
                                errorMessage = Common.GetMessage("VAL0007", "Promotion Condition", "0");
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                        }

                        break;
                        #endregion
                    }
                case (int)Promotion.PromotionCategoryType.Quantity:
                    {
                        #region Validations on Condition when promotion category is line

                        ////Mandatory Condition On if condition is buy
                        //if (Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue) == (int)Promotion.ConditionType.Buy)
                        //{
                        //    if (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Common.INT_DBNULL)
                        //    {
                        //        errorMessage = Common.GetMessage("VAL0002", lblCreatePromoConditionOn.Text.Replace(":*", ""));
                        //        errorPromotion.SetError(cmbCreatePromoConditionOn, errorMessage);
                        //        errorMessages.Append(errorMessage);
                        //        errorMessages.Append(Environment.NewLine);
                        //        IsValidationSuccess = false;
                        //    }
                        //    else
                        //    {
                        //        errorPromotion.SetError(cmbCreatePromoConditionOn, string.Empty);
                        //    }
                        //}

                        //The Condition Type, Conditon On, Condition Code must be unique with in the conditions
                        int conditionCode = Common.INT_DBNULL;

                        if ((Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCT))
                            | (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCTGROUP)))
                        {
                            conditionCode = Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue);
                        }
                        else
                        {
                            conditionCode = hierarchyLevelId;
                        }

                        if (conditionCode == Common.INT_DBNULL)
                        {
                            errorMessage = Common.GetMessage("VAL0002", lblCreatePromoConditionOn.Text.Replace(":*", ""));
                            errorPromotion.SetError(cmbCreatePromoConditionOn, errorMessage);
                            errorMessages.Append(errorMessage);
                            errorMessages.Append(Environment.NewLine);
                            IsValidationSuccess = false;
                        }
                        else
                        {
                            errorPromotion.SetError(cmbCreatePromoConditionOn, string.Empty);

                            if (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Common.INT_DBNULL)
                            {
                                errorMessage = Common.GetMessage("VAL0002", lblCreatePromoConditionOn.Text.Replace(":*", ""));
                                errorPromotion.SetError(cmbCreatePromoConditionOn, errorMessage);
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                errorPromotion.SetError(cmbCreatePromoConditionOn, string.Empty);
                            }
                        }

                        //Mandatory Condition Code
                        if (Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue) == (int)Promotion.ConditionType.Buy
                            | Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) != Common.INT_DBNULL)
                        {
                            if (conditionCode == Common.INT_DBNULL)
                            {
                                errorMessage = Common.GetMessage("VAL0002", lblCreatePromoConditionCode.Text.Replace(":*", ""));
                                errorPromotion.SetError(cmbCreatePromoConditionCode, errorMessage);
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                errorPromotion.SetError(cmbCreatePromoConditionCode, string.Empty);
                            }
                        }

                        //If condition code is there which is mandatory for buy
                        //so qty would be checked                        

                        if ((Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) == (int)Promotion.ConditionType.Get) & Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) != Common.INT_DBNULL)
                        {
                            if (Convert.ToInt32(cmbCreatePromoConditionDiscountType.SelectedValue) == Common.INT_DBNULL)
                            {
                                errorMessage = Common.GetMessage("VAL0002", lblCreatePromoConditionDiscountValue.Text.Replace(":", ""));
                                errorPromotion.SetError(cmbCreatePromoConditionDiscountType, errorMessage);
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                errorPromotion.SetError(cmbCreatePromoConditionDiscountType, string.Empty);
                            }

                            //if (!Validators.IsDecimal(txtCreatePromoConditionDiscountValue.Text))
                            //{
                            //    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);

                            //    errorMessage = Common.GetMessage("VAL0009", lblCreatePromoConditionDiscountValue.Text.Replace(":*", ""));
                            //    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                            //    errorMessages.Append(errorMessage);
                            //    errorMessages.Append(Environment.NewLine);
                            //    IsValidationSuccess = false;
                            //}
                            //else if (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) <= 0)
                            //{
                            //    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);

                            //    errorMessage = Common.GetMessage("VAL0033", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0");
                            //    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                            //    errorMessages.Append(errorMessage);
                            //    errorMessages.Append(Environment.NewLine);
                            //    IsValidationSuccess = false;
                            //}
                            //else
                            //{
                            //    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);
                            //}

                            if (cmbCreatePromoConditionCode.SelectedValue != null)
                            {
                                string errMsg = string.Empty;
                                double minDistributorPrice = 0.00;
                                if ((Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) != Common.INT_DBNULL) && (Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue) != Common.INT_DBNULL))
                                {
                                    PromotionCondition tempObj = new PromotionCondition();
                                    minDistributorPrice = tempObj.FetchMinimumDistributorPrice(Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue), Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue), ref errMsg);
                                }
                                if (!Validators.IsDecimal(txtCreatePromoConditionDiscountValue.Text))
                                {
                                    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);

                                    errorMessage = Common.GetMessage("VAL0009", lblCreatePromoConditionDiscountValue.Text.Replace(":*", ""));
                                    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                    errorMessages.Append(errorMessage);
                                    errorMessages.Append(Environment.NewLine);
                                    IsValidationSuccess = false;
                                }
                                else if (Convert.ToInt32(cmbCreatePromoConditionDiscountType.SelectedValue) == 1)
                                {
                                    if ((Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) <= 0) ||
                                        (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) >= 100))
                                    {
                                        errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);

                                        errorMessage = Common.GetMessage("VAL0033", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0").Replace(".", "") + " and less than 100.";
                                        errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                        errorMessages.Append(errorMessage);
                                        errorMessages.Append(Environment.NewLine);
                                        IsValidationSuccess = false;
                                    }
                                    else
                                    {
                                        errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);
                                    }
                                }
                                else if (Convert.ToInt32(cmbCreatePromoConditionDiscountType.SelectedValue) == 2)
                                {
                                    //string errMsg = "";

                                    //double minDistributorPrice = this.condition.FetchMinimumDistributorPrice(Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue), Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue), ref errMsg);
                                    if (!string.IsNullOrEmpty(errMsg))
                                    {
                                        //TODO: HANDLE ERROR
                                    }
                                    else
                                    {
                                        //if ((Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) <= 0) ||
                                        //    (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) >= Convert.ToDecimal(minDistributorPrice)))
                                        //{
                                        //    //errorMessage = Common.GetMessage("VAL0072", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0").Replace(".", "") + " or greater than the minimum Distributor Price of " + minDistributorPrice + ".";
                                        //    errorMessage = Common.GetMessage("VAL0072", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0") + Environment.NewLine + Common.GetMessage("VAL0104", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "minimum Distributor Price of " + minDistributorPrice);
                                        //    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                        //    errorMessages.Append(errorMessage);
                                        //    errorMessages.Append(Environment.NewLine);
                                        //    IsValidationSuccess = false;
                                        //}
                                        //else
                                        //{
                                        //    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);
                                        //}

                                        errorMessage = string.Empty;
                                        bool valid = true;

                                        if (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) <= 0)
                                        {
                                            errorMessage = Common.GetMessage("VAL0104", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0");
                                            valid = false;
                                        }
                                        else if (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) >= Convert.ToDecimal(minDistributorPrice))
                                        {
                                            errorMessage = Common.GetMessage("VAL0105", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "minimum Distributor Price of " + minDistributorPrice);// " or greater than the minimum Distributor Price of " + minDistributorPrice + ".";
                                            valid = false;
                                        }

                                        errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                        errorMessages.Append(errorMessage);

                                        if (!valid)
                                        {
                                            IsValidationSuccess = false;
                                            errorMessages.Append(Environment.NewLine);
                                        }
                                    }
                                }
                                else if (Convert.ToInt32(cmbCreatePromoConditionDiscountType.SelectedValue) == 3)
                                {
                                    //string errMsg = "";

                                    //double minDistributorPrice = this.condition.FetchMinimumDistributorPrice(Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue), Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue), ref errMsg);
                                    if (!string.IsNullOrEmpty(errMsg))
                                    {
                                        //TODO: HANDLE ERROR
                                    }
                                    else
                                    {
                                        //if (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) <= 0)
                                        //{
                                        //    errorMessage = Common.GetMessage("INF0059", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0");
                                        //    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                        //    errorMessages.Append(errorMessage);
                                        //    errorMessages.Append(Environment.NewLine);
                                        //    IsValidationSuccess = false;
                                        //}
                                        //else if (Convert.ToDecimal(minDistributorPrice) > 0)
                                        //{
                                        //    if (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) >= Convert.ToDecimal(minDistributorPrice))
                                        //    {
                                        //        //errorMessage = Common.GetMessage("VAL0072", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0").Replace(".", "") + " or greater than the minimum Distributor Price of " + minDistributorPrice + ".";
                                        //        errorMessage = Common.GetMessage("VAL0072", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0") + Environment.NewLine + Common.GetMessage("VAL0104", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "minimum Distributor Price of " + minDistributorPrice);
                                        //        errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                        //        errorMessages.Append(errorMessage);
                                        //        errorMessages.Append(Environment.NewLine);
                                        //        IsValidationSuccess = false;
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);
                                        //}

                                        errorMessage = string.Empty;
                                        bool valid = true;

                                        if (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) <= 0)
                                        {
                                            errorMessage = Common.GetMessage("VAL0072", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "0");
                                            valid = false;
                                        }
                                        else if (Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text) >= Convert.ToDecimal(minDistributorPrice))
                                        {
                                            errorMessage = Common.GetMessage("VAL0104", lblCreatePromoConditionDiscountValue.Text.Replace(":", "").Replace("*", ""), "minimum Distributor Price of " + minDistributorPrice);// " or greater than the minimum Distributor Price of " + minDistributorPrice + ".";
                                            valid = false;
                                        }

                                        errorPromotion.SetError(txtCreatePromoConditionDiscountValue, errorMessage);
                                        errorMessages.Append(errorMessage);

                                        if (!valid)
                                        {
                                            IsValidationSuccess = false;
                                            errorMessages.Append(Environment.NewLine);
                                        }
                                    }
                                }
                                else
                                {
                                    errorPromotion.SetError(txtCreatePromoConditionDiscountValue, string.Empty);
                                }
                            }
                        }


                        int count = (from p in promotion.Conditions
                                     where p.ConditionTypeId == Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) &&
                                           p.ConditionOnId == Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) &&
                                           p.ConditionCodeId == conditionCode
                                     select p).Count();

                        //In case of edit mode there should be one record exists for this combination
                        if (btnCreatePromoConditionAdd.Text == "S&ave")
                        {
                            //Duplicate should exists
                            if (count <= 0)
                            {
                                throw new InvalidOperationException("Screen state is invalid");
                            }
                        }
                        else
                        {
                            //if duplicate exists
                            if (count > 0)
                            {
                                //Record already exists can't insert
                                errorMessage = Common.GetMessage("VAL0007", "Promotion Condition", "0");
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                        }

                        break;
                        #endregion
                    }
                //Bill buster and volumn would be handled in the same way
                case (int)Promotion.PromotionCategoryType.BillBuster:
                case (int)Promotion.PromotionCategoryType.Volume:
                    {
                        #region Volumn and Bill Buster Validation

                        int conditionCode = Common.INT_DBNULL;

                        if (cmbCreatePromoConditionOn.SelectedValue != null)
                        {
                            if ((Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCT))
                                | (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCTGROUP)))
                            {
                                conditionCode = Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue);
                            }
                            else
                            {
                                conditionCode = hierarchyLevelId;
                            }
                        }

                        if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) != (int)Promotion.PromotionCategoryType.BillBuster)
                        {
                            //Can only be created on Item / Product, and not on Product-Group or Hierarchy
                            if (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Common.INT_DBNULL)
                            {
                                errorMessage = Common.GetMessage("VAL0002", lblCreatePromoConditionOn.Text.Replace(":*", ""));
                                errorPromotion.SetError(cmbCreatePromoConditionOn, errorMessage);
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else if ((Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == 2) ||
                                    (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == 3))
                            {
                                errorMessage = Common.GetMessage("VAL0088").Replace("\\n", Environment.NewLine);
                                errorPromotion.SetError(cmbCreatePromoConditionOn, errorMessage);
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                errorPromotion.SetError(cmbCreatePromoConditionOn, string.Empty);
                            }

                            ////Mandatory Condition On
                            //if (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Common.INT_DBNULL)
                            //{
                            //    errorMessage = Common.GetMessage("VAL0002", lblCreatePromoConditionOn.Text.Replace(":*", ""));
                            //    errorPromotion.SetError(cmbCreatePromoConditionOn, errorMessage);
                            //    errorMessages.Append(errorMessage);
                            //    errorMessages.Append(Environment.NewLine);
                            //    IsValidationSuccess = false;
                            //}
                            //else
                            //{
                            //    errorPromotion.SetError(cmbCreatePromoConditionOn, string.Empty);
                            //}


                            //The Condition Type, Conditon On, Condition Code must be unique with in the conditions
                            if ((Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCT))
                                | (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCTGROUP)))
                            {
                                conditionCode = Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue);
                            }
                            else
                            {
                                conditionCode = hierarchyLevelId;
                            }

                            //Mandatory Condition Code
                            if (conditionCode == Common.INT_DBNULL)
                            {
                                errorMessage = Common.GetMessage("VAL0002", lblCreatePromoConditionCode.Text.Replace(":*", ""));
                                errorPromotion.SetError(cmbCreatePromoConditionCode, errorMessage);
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                            else
                            {
                                errorPromotion.SetError(cmbCreatePromoConditionCode, string.Empty);
                            }
                        }

                        int count = (from p in promotion.Conditions
                                     where p.ConditionTypeId == Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) &&
                                           p.ConditionOnId == Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) &&
                                           p.ConditionCodeId == conditionCode
                                     select p).Count();

                        //In case of edit mode there should be one record exists for this combination
                        if (btnCreatePromoConditionAdd.Text == "S&ave")
                        {
                            //Duplicate should exists
                            if (count <= 0)
                            {
                                throw new InvalidOperationException("Screen state is invalid");
                            }
                        }
                        else
                        {
                            //if duplicate exists
                            if (count > 0)
                            {
                                //Record already exists can't insert
                                errorMessage = Common.GetMessage("VAL0007", "Promotion Condition", "0");
                                errorMessages.Append(errorMessage);
                                errorMessages.Append(Environment.NewLine);
                                IsValidationSuccess = false;
                            }
                        }

                        break;
                        #endregion
                    }
            }
            errorMessages = Common.ReturnErrorMessage(errorMessages);
            if (!String.IsNullOrEmpty(errorMessage.ToString()))
            {
                MessageBox.Show(errorMessages.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return IsValidationSuccess;
        }

        /// <summary>
        /// Validate the location
        /// </summary>
        /// <returns></returns>
        private Boolean ValidateLocation()
        {
            //Location Id is mandatory
            Boolean IsValidationSuccess = true;

            StringBuilder errorMessages = new StringBuilder();
            string errorMessage = string.Empty;

            //Mandatory Condition Type
            //if (Convert.ToInt32(cmbCreatePromoLocation.SelectedValue) == Common.INT_DBNULL)

            //Skip the check for top-most location, i.e. "All"
            //if (Convert.ToInt32(chklstPromoLocations.SelectedValue) == Common.INT_DBNULL)
            //{
            //    errorMessage = Common.GetMessage("VAL0002", lblCreatePromoLocation.Text.Replace(":*", ""));
            //    //errorPromotion.SetError(cmbCreatePromoLocation, errorMessage);
            //    errorPromotion.SetError(chklstPromoLocations, errorMessage);
            //    errorMessages.Append(errorMessage);
            //    errorMessages.Append(Environment.NewLine);
            //    IsValidationSuccess = false;
            //}
            //else
            //{
            //    //errorPromotion.SetError(cmbCreatePromoLocation, string.Empty);
            //    errorPromotion.SetError(chklstPromoLocations, string.Empty);
            //}

            /*
            int count = (from p in promotion.Locations
                         where p.LocationId == Convert.ToInt32(cmbCreatePromoLocation.SelectedValue)
                         select p).Count();

            if (btnCreatePromoLocationAdd.Text == "Save")
            {
                //Duplicate should exists
                if (count <= 0)
                {
                    throw new InvalidOperationException("Screen state is invalid");
                }
            }
            else
            {
                //if duplicate exists
                if (count > 0)
                {
                    //Record already exists can't insert
                    errorMessage = Common.GetMessage("VAL0007", "Promotion Location", "0");
                    errorMessages.Append(errorMessage);
                    errorMessages.Append(Environment.NewLine);
                    IsValidationSuccess = false;
                }
            }
             * */

            /*
            int count = (from p in promotion.Locations
                         where p.LocationId == Convert.ToInt32(cmbCreatePromoLocation.SelectedValue)
                         select p).Count();
            */

            if (chklstPromoLocations.CheckedIndices.Count == 0)
            {
                errorMessage = Common.GetMessage("VAL0002", "Promotion Location");
                errorPromotion.SetError(chklstPromoLocations, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else
            {
                List<int> selectedPromoLocs = new List<int>();
                List<string> lstSelectedLocCodes = new List<string>();
                foreach (object checkedItem in chklstPromoLocations.CheckedIndices)
                {
                    int locId = Convert.ToInt32(((DataRowView)chklstPromoLocations.Items[(int)checkedItem]).Row["LocationID"].ToString());
                    if (locId > Common.INT_DBNULL)
                    {
                        int count = (from p in promotion.Locations
                                     where p.LocationId == Convert.ToInt32(((DataRowView)chklstPromoLocations.Items[(int)checkedItem]).Row["LocationID"].ToString())
                                     select p).Count();

                        if (btnCreatePromoLocationAdd.Text == "S&ave")
                        {
                            //if modified record is non-existent, then add such item to the error-list
                            if (count <= 0)
                            {
                                selectedPromoLocs.Add(count);
                            }
                        }
                        else
                        {
                            //if new record already exists, then add such item to the error-list
                            if (count > 0)
                            {
                                //PromotionLocation locType = new PromotionLocation();
                                selectedPromoLocs.Add(count);
                                PromotionLocation locCode =
                                                  (from p in promotion.Locations
                                                   where p.LocationId == Convert.ToInt32(((DataRowView)chklstPromoLocations.Items[(int)checkedItem]).Row["LocationID"].ToString())
                                                   select p).FirstOrDefault();

                                //lstSelectedLocCodes.Add(locCode.LocationCode);
                                lstSelectedLocCodes.Add(((DataRowView)chklstPromoLocations.Items[(int)checkedItem]).Row["LocationName"].ToString());

                                m_suspendEventHandler = true;
                                chklstPromoLocations.SetItemChecked((int)checkedItem, false);
                                m_suspendEventHandler = false;
                            }
                        }
                    }
                }

                if (lstSelectedLocCodes.Count > 0)
                {
                    m_suspendEventHandler = true;
                    chklstPromoLocations.SetItemChecked((int)0, false);
                    m_suspendEventHandler = false;
                }

                if (btnCreatePromoLocationAdd.Text == "S&ave")
                {
                    //if modified-record in non-existent, then find 1st occurence of such record and throw exception on such
                    if (selectedPromoLocs.Count > 0)
                    {
                        throw new InvalidOperationException("Screen state is invalid");
                    }
                }
                else
                {
                    //if added-record already exists, then show error and set ValidationStatus to false
                    if (lstSelectedLocCodes.Count > 0)
                    {
                        string strSelectedLocCodes = string.Empty;
                        for (int indx = 0; indx < lstSelectedLocCodes.Count; indx++)// string locCode in lstSelectedLocCodes)
                        {
                            if (((indx + 1) % 4) == 0)
                            {
                                strSelectedLocCodes += "\n" + lstSelectedLocCodes[indx] + ", ";
                            }
                            else
                            {
                                strSelectedLocCodes += lstSelectedLocCodes[indx] + ", ";
                            }
                        }
                        strSelectedLocCodes = strSelectedLocCodes.Trim().Substring(0, strSelectedLocCodes.Trim().Length - 1);

                        errorMessage = Common.GetMessage("VAL0103", "Promotion Locations", strSelectedLocCodes).Replace("\\n", Environment.NewLine);
                        errorMessages.Append(errorMessage);
                        errorMessages.Append(Environment.NewLine);
                        IsValidationSuccess = false;
                    }
                }
            }

            errorMessages = Common.ReturnErrorMessage(errorMessages);
            if (!String.IsNullOrEmpty(errorMessage.ToString()))
            {
                MessageBox.Show(errorMessages.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return IsValidationSuccess;
        }

        /// <summary>
        /// Validate the tier Data
        /// </summary>
        /// <returns></returns>
        private Boolean ValidateTier()
        {
            Boolean IsValidationSuccess = true;

            StringBuilder errorMessages = new StringBuilder();
            string errorMessage = string.Empty;

            //Buy From Qty is mandatory
            if (Validators.CheckForEmptyString(txtCreatePromoTierBuyQtyFrom.Text.Length))
            {
                errorMessage = Common.GetMessage("VAL0001", lblCreatePromoTierBuyQtyFrom.Text.Replace(":*", ""));
                errorPromotion.SetError(txtCreatePromoTierBuyQtyFrom, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else if (!Validators.IsInt32(txtCreatePromoTierBuyQtyFrom.Text))
            {
                //Buy From Qty should be decimal
                errorMessage = Common.GetMessage("VAL0009", lblCreatePromoTierBuyQtyFrom.Text.Replace(":*", ""));
                errorPromotion.SetError(txtCreatePromoTierBuyQtyFrom, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else
            {
                errorPromotion.SetError(txtCreatePromoTierBuyQtyFrom, string.Empty);
            }

            if (Validators.CheckForEmptyString(txtCreatePromoTierBuyQtyTo.Text.Length))
            {
                //Buy To Qty is mandatory
                errorMessage = Common.GetMessage("VAL0001", lblCreatePromoTierBuyQtyTo.Text.Replace(":*", ""));
                errorPromotion.SetError(txtCreatePromoTierBuyQtyTo, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else if (!Validators.IsInt32(txtCreatePromoTierBuyQtyTo.Text))
            {
                //Buy From Qty should be decimal
                errorMessage = Common.GetMessage("VAL0009", lblCreatePromoTierBuyQtyTo.Text.Replace(":*", ""));
                errorPromotion.SetError(txtCreatePromoTierBuyQtyTo, errorMessage);
                errorMessages.Append(errorMessage);
                errorMessages.Append(Environment.NewLine);
                IsValidationSuccess = false;
            }
            else if (errorMessage.Length == 0)
            {
                if (Convert.ToInt32(txtCreatePromoTierBuyQtyTo.Text) < Convert.ToInt32(txtCreatePromoTierBuyQtyFrom.Text))
                {
                    errorMessage = Common.GetMessage("VAL0047", lblCreatePromoTierBuyQtyTo.Text.Replace(":*", ""), lblCreatePromoTierBuyQtyFrom.Text.Replace(":*", ""));
                    errorPromotion.SetError(txtCreatePromoTierBuyQtyTo, errorMessage);
                    errorMessages.Append(errorMessage);
                    errorMessages.Append(Environment.NewLine);
                    IsValidationSuccess = false;
                }
            }
            else
            {
                errorPromotion.SetError(txtCreatePromoTierBuyQtyTo, string.Empty);
            }


            if (!String.IsNullOrEmpty(errorMessage.ToString()))
            {
                MessageBox.Show(errorMessages.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return IsValidationSuccess;
        }

        /// <summary>
        /// Clears the search form
        /// </summary>
        private void ClearSearchForm()
        {
            txtSearchPromotionName.Text = string.Empty;
            cmbSearchPromotionCategory.SelectedValue = Common.INT_DBNULL;
            txtSearchPromotionCode.Text = string.Empty;
            dtpSearchPromoStartDate.Value = DateTime.Now;
            dtpSearchPromoStartDate.Checked = false;
            dtpSearchPromoEndDate.Value = DateTime.Now;
            dtpSearchPromoEndDate.Checked = false;
            cmbSearchPromoDiscountType.SelectedValue = Common.INT_DBNULL;
            //Select the first index
            cmbSearchPromoStatus.SelectedIndex = 0;
            cmbSearchPromoApp.SelectedValue = Common.INT_DBNULL;
            dgvSearchPromo.DataSource = null;
        }

        /// <summary>
        /// Clear form values
        /// </summary>
        private void ClearForm()
        {
            //Other elements
            errorPromotion.Clear();

            txtCreatePromotionName.Text = string.Empty;
            cmbCreatePromoCategory.SelectedValue = Common.INT_DBNULL;
            txtCreatePromoCode.Text = string.Empty;
            dtpCreatePromoStartDate.Value = DateTime.Now;
            dtpCreatePromoEndDate.Value = DateTime.Now;
            dtpCreatePromoDurationStart.Value = DateTime.Now;
            dtpCreatePromoDurationStart.Checked = false;
            dtpCreatePromoDurationEnd.Value = DateTime.Now;
            dtpCreatePromoDurationEnd.Checked = false;
            cmbCreatePromoDiscountType.SelectedValue = Common.INT_DBNULL;
            txtCreatePromoDiscountValue.Text = "0.00";
            txtCreatePromoMaxOrderQty.Text = "0.00";
            cmbCreatePromoStatus.SelectedValue = Common.INT_DBNULL;

            //Reset to active status
            cmbCreatePromoStatus.SelectedIndex = 0;
            cmbCreatePromotionApp.SelectedValue = Common.INT_DBNULL;
            cmbCreatePromoBuyType.SelectedValue = Common.INT_DBNULL;
            cmbCreatePromoGetType.SelectedValue = Common.INT_DBNULL;
            txtCreatePromoRepeat.Text = "0";
            #region Shopping Cart
            chkPOS.Checked = false; //Roshan
            chkWeb.Checked = false;			//Roshan
            //To Dispose Image:
            ImgWebPromotion.Image = null;
            this.LoadedImage = "";
            this.Filepath = "";
            #endregion

            #region Enable/Disable the controls to the Start Values
            EnablePromotionMasterElements(false);
            #endregion

            grpAddDetails.Enabled = false;

            ClearPromotionLocation();
            this.dgvCreatePromoLocation.SelectionChanged -= new System.EventHandler(this.dgvCreatePromoLocation_SelectionChanged);
            dgvCreatePromoLocation.DataSource = null;
            this.dgvCreatePromoLocation.SelectionChanged += new System.EventHandler(this.dgvCreatePromoLocation_SelectionChanged);

            ClearPromotionCondition();
            this.dgvCreatePromoCondition.SelectionChanged -= new System.EventHandler(this.dgvCreatePromoCondition_SelectionChanged);
            dgvCreatePromoCondition.DataSource = null;
            this.dgvCreatePromoCondition.SelectionChanged += new System.EventHandler(this.dgvCreatePromoCondition_SelectionChanged);

            ClearPromotionTier();
            dgvCreatePromoTier.DataSource = null;

            //Set focus of Promotion-Details to 1st tab-page
            tabPromotion.SelectedTab = tabPromotion.TabPages["tabCondition"];

            //Promotion object for the screen
            promotion = null;
            promotion = new Promotion();
            condition = null;
            location = null;
            tier = null;
            hierarchyLevelId = Common.INT_DBNULL;
            cmbCreatePromoCategory.Focus();
        }

        /// <summary>
        /// Clear Promotion Condition Values
        /// </summary>
        private void ClearPromotionCondition()
        {
            cmbCreatePromoConditionType.SelectedIndex = 0;
            cmbCreatePromoConditionOn.SelectedIndex = 0;
            if (cmbCreatePromoConditionCode.Items.Count > 0)
            {
                cmbCreatePromoConditionCode.SelectedIndex = 0;
            }

            txtCreatePromoConditionCode.Text = "";
            txtCreatePromoConditionMinQty.Text = "0.00";
            txtCreatePromoConditionMaxQty.Text = "0.00";
            txtCreatePromoConditionQty.Text = "0.00";
            cmbCreatePromoConditionDiscountType.SelectedIndex = 0;
            txtCreatePromoConditionDiscountValue.Text = "0.00";
            cmbCreatePromoConditionStatus.SelectedIndex = 0;
            btnCreatePromoConditionAdd.Text = "A&dd";

            //Clear the current condition object
            this.condition = null;
            EnableDisableCondition(ConditionOperationState.CLEAR);
            cmbCreatePromoConditionType.Focus();

        }

        /// <summary>
        /// Clear Promotion Location Values
        /// </summary>
        private void ClearPromotionLocation()
        {
            //cmbCreatePromoLocation.SelectedIndex = 0;
            chklstPromoLocations.ClearSelected();
            foreach (object checkedItem in chklstPromoLocations.CheckedIndices)
            {
                chklstPromoLocations.SetItemChecked((int)checkedItem, false);
            }

            cmbCreatePromoLocStatus.SelectedIndex = 0;
            btnCreatePromoLocationAdd.Text = "A&dd";
            //clear the current object
            this.location = null;
            EnableDisableLocation(LocationOperationState.CLEAR);
            chklstPromoLocations.Focus();
        }

        /// <summary>
        /// Clear Promotion Tier Values
        /// </summary>
        private void ClearPromotionTier()
        {
            if (promotion != null && promotion.Tiers != null && promotion.Tiers.Count > 0)
            {
                txtCreatePromoTierBuyQtyFrom.Enabled = false;
                txtCreatePromoTierBuyQtyFrom.Text = (Convert.ToInt32(promotion.Tiers[promotion.Tiers.Count - 1].BuyQtyTo) + 1).ToString();
            }
            else
            {
                txtCreatePromoTierBuyQtyFrom.Text = string.Empty;
                txtCreatePromoTierBuyQtyFrom.Enabled = true;
            }
            txtCreatePromoTierBuyQtyTo.Text = string.Empty;
            cmbCreatePromoTierDiscountType.SelectedValue = Common.INT_DBNULL;
            txtCreatePromoTierDiscountValue.Text = "0.00";
            cmbCreatePromoTierStatus.SelectedValue = Common.INT_DBNULL;
            btnCreatePromoTierAdd.Text = "Add";
            EnableDisableTier(TierOperationState.CLEAR);
            txtCreatePromoTierBuyQtyFrom.Focus();
        }

        /// <summary>
        /// Enable/Disable Promotion-Master elements
        /// </summary>
        /// <param name="enableState">If true, then Promotion-Master elements are enabled</param>
        private void EnablePromotionMasterElements(bool enableState)
        {
            /*
            txtCreatePromotionName.Enabled = true;
            cmbCreatePromoCategory.Enabled = true;
            txtCreatePromoCode.Enabled = true;
            dtpCreatePromoStartDate.Enabled = true;
            dtpCreatePromoEndDate.Enabled = true;
            dtpCreatePromoDurationEnd.Enabled = true;
            dtpCreatePromoDurationStart.Enabled = true;
            cmbCreatePromoDiscountType.Enabled = true;
            txtCreatePromoDiscountValue.Enabled = true;
            txtCreatePromoMaxOrderQty.Enabled = true;
            cmbCreatePromoStatus.Enabled = true;
            cmbCreatePromotionApp.Enabled = true;
            cmbCreatePromoBuyType.Enabled = true;
            cmbCreatePromoGetType.Enabled = true;
            txtCreatePromoRepeat.Enabled = true; 
             * */

            txtCreatePromotionName.Enabled = enableState;
            cmbCreatePromoCategory.Enabled = enableState;
            txtCreatePromoCode.Enabled = enableState;
            dtpCreatePromoStartDate.Enabled = enableState;
            dtpCreatePromoEndDate.Enabled = enableState;
            dtpCreatePromoDurationStart.Enabled = enableState;
            dtpCreatePromoDurationStart.Checked = enableState;
            dtpCreatePromoDurationStart.Value = Convert.ToDateTime("00:00:00.000");
            dtpCreatePromoDurationEnd.Enabled = enableState;
            dtpCreatePromoDurationEnd.Checked = enableState;
            dtpCreatePromoDurationEnd.Value = Convert.ToDateTime("23:59:59.999");
            cmbCreatePromoDiscountType.Enabled = enableState;
            txtCreatePromoDiscountValue.Enabled = enableState;
            txtCreatePromoMaxOrderQty.Enabled = enableState;
            cmbCreatePromoStatus.Enabled = enableState;
            cmbCreatePromotionApp.Enabled = enableState;
            cmbCreatePromoBuyType.Enabled = enableState;
            cmbCreatePromoGetType.Enabled = enableState;
            txtCreatePromoRepeat.Enabled = enableState;
        }

        /// <summary>
        /// Enable disable items of the screen based the promotion selected
        /// </summary>
        /// <param name="state"></param>
        private void EnableDisable(OperationState state)
        {
            //Enable all screen elements and start disabling them depending on the conditions passed
            grpAddDetails.Enabled = true;
            EnablePromotionMasterElements(true);

            switch (Convert.ToInt32(state))
            {
                //In the add mode
                case (int)OperationState.ADD:
                    {
                        switch (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue))
                        {

                            case (int)Promotion.PromotionCategoryType.Line:
                                {
                                    #region Line Promotion Enable Disable
                                    cmbCreatePromoDiscountType.Enabled = false;
                                    txtCreatePromoDiscountValue.Enabled = false;
                                    txtCreatePromoMaxOrderQty.Enabled = false;
                                    cmbCreatePromoBuyType.Enabled = false;
                                    cmbCreatePromoGetType.Enabled = false;
                                    tabPromotion.TabPages.Remove(tabTier);
                                    cmbCreatePromoBuyType.SelectedValue = (int)Promotion.ConditionOperation.Or;
                                    EnableDisableCondition(ConditionOperationState.ADD);
                                    EnableDisableLocation(LocationOperationState.ADD);
                                    EnableDisableTier(TierOperationState.ADD);
                                    cmbCreatePromoStatus.SelectedValue = (int)Promotion.Status.Active;
                                    break;
                                    #endregion
                                }
                            //Bill buster an volumn are same time of promotions
                            case (int)Promotion.PromotionCategoryType.BillBuster:
                            case (int)Promotion.PromotionCategoryType.Volume:
                                {
                                    #region Volumn and Bill Buster Promotion Enable Disable
                                    cmbCreatePromoBuyType.SelectedIndex = 0;
                                    cmbCreatePromoDiscountType.Enabled = false;
                                    txtCreatePromoDiscountValue.Enabled = false;
                                    txtCreatePromoMaxOrderQty.Enabled = false;
                                    //Disable Buy and Get Type at Header Level
                                    cmbCreatePromoBuyType.Enabled = false;
                                    cmbCreatePromoGetType.Enabled = false;
                                    tabPromotion.TabPages.Remove(tabTier);
                                    //Default the Buy Type to OR
                                    cmbCreatePromoBuyType.SelectedValue = (int)Promotion.ConditionOperation.Or;
                                    EnableDisableCondition(ConditionOperationState.ADD);
                                    EnableDisableLocation(LocationOperationState.ADD);
                                    EnableDisableTier(TierOperationState.ADD);
                                    cmbCreatePromoStatus.SelectedValue = (int)Promotion.Status.Active;
                                    break;
                                    #endregion

                                }
                            case (int)Promotion.PromotionCategoryType.Quantity:
                                {
                                    #region Quantity Promotion Enable Disable
                                    cmbCreatePromoBuyType.SelectedIndex = 0;
                                    cmbCreatePromoDiscountType.Enabled = false;
                                    txtCreatePromoDiscountValue.Enabled = false;
                                    txtCreatePromoMaxOrderQty.Enabled = false;
                                    tabPromotion.TabPages.Remove(tabTier);
                                    EnableDisableCondition(ConditionOperationState.ADD);
                                    EnableDisableLocation(LocationOperationState.ADD);
                                    EnableDisableTier(TierOperationState.ADD);
                                    cmbCreatePromoStatus.SelectedValue = (int)Promotion.Status.Active;
                                    break;
                                    #endregion
                                }
                        }
                        break;
                    }
                case (int)OperationState.EDIT:
                    {
                        switch (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue))
                        {

                            case (int)Promotion.PromotionCategoryType.Line:
                                {
                                    #region Line Promotion Enable Disable
                                    txtCreatePromotionName.Enabled = false;
                                    txtCreatePromoCode.Enabled = false;
                                    cmbCreatePromoDiscountType.Enabled = false;
                                    txtCreatePromoDiscountValue.Enabled = false;
                                    txtCreatePromoMaxOrderQty.Enabled = false;
                                    cmbCreatePromoBuyType.Enabled = false;
                                    cmbCreatePromoGetType.Enabled = false;
                                    tabPromotion.TabPages.Remove(tabTier);
                                    cmbCreatePromoBuyType.SelectedValue = (int)Promotion.ConditionOperation.Or;
                                    txtCreatePromoRepeat.Enabled = false;
                                    EnableDisableCondition(ConditionOperationState.ADD);
                                    EnableDisableLocation(LocationOperationState.ADD);
                                    EnableDisableTier(TierOperationState.ADD);
                                    //cmbCreatePromoStatus.SelectedValue = (int)Promotion.Status.Active;
                                    break;
                                    #endregion
                                }
                            //Bill buster an volumn are same time of promotions
                            case (int)Promotion.PromotionCategoryType.BillBuster:
                                {
                                    #region Bill Buster Promotion Enable Disable
                                    txtCreatePromotionName.Enabled = false;
                                    txtCreatePromoCode.Enabled = false;
                                    cmbCreatePromoBuyType.SelectedIndex = 0;
                                    cmbCreatePromoDiscountType.Enabled = false;
                                    txtCreatePromoDiscountValue.Enabled = false;
                                    txtCreatePromoMaxOrderQty.Enabled = false;
                                    cmbCreatePromoBuyType.Enabled = false;
                                    cmbCreatePromoGetType.Enabled = false;
                                    tabPromotion.TabPages.Remove(tabTier);
                                    cmbCreatePromoBuyType.SelectedValue = (int)Promotion.ConditionOperation.Or;
                                    txtCreatePromoRepeat.Enabled = true;
                                    EnableDisableCondition(ConditionOperationState.ADD);
                                    EnableDisableLocation(LocationOperationState.ADD);
                                    EnableDisableTier(TierOperationState.ADD);
                                    //cmbCreatePromoStatus.SelectedValue = (int)Promotion.Status.Active;
                                    break;
                                    #endregion
                                }
                            case (int)Promotion.PromotionCategoryType.Volume:
                                {
                                    #region Volume Promotion Enable Disable
                                    txtCreatePromotionName.Enabled = false;
                                    txtCreatePromoCode.Enabled = false;
                                    cmbCreatePromoBuyType.SelectedIndex = 0;
                                    cmbCreatePromoDiscountType.Enabled = false;
                                    txtCreatePromoDiscountValue.Enabled = false;
                                    txtCreatePromoMaxOrderQty.Enabled = false;
                                    cmbCreatePromoBuyType.Enabled = false;
                                    cmbCreatePromoGetType.Enabled = false;
                                    tabPromotion.TabPages.Remove(tabTier);
                                    cmbCreatePromoBuyType.SelectedValue = (int)Promotion.ConditionOperation.Or;
                                    txtCreatePromoRepeat.Enabled = false;
                                    EnableDisableCondition(ConditionOperationState.ADD);
                                    EnableDisableLocation(LocationOperationState.ADD);
                                    EnableDisableTier(TierOperationState.ADD);
                                    //cmbCreatePromoStatus.SelectedValue = (int)Promotion.Status.Active;
                                    break;
                                    #endregion
                                }
                            case (int)Promotion.PromotionCategoryType.Quantity:
                                {
                                    #region Quantity Promotion Enable Disable
                                    txtCreatePromotionName.Enabled = false;
                                    txtCreatePromoCode.Enabled = false;
                                    cmbCreatePromoDiscountType.Enabled = false;
                                    txtCreatePromoDiscountValue.Enabled = false;
                                    txtCreatePromoMaxOrderQty.Enabled = false;
                                    tabPromotion.TabPages.Remove(tabTier);
                                    txtCreatePromoRepeat.Enabled = true;
                                    EnableDisableCondition(ConditionOperationState.ADD);
                                    EnableDisableLocation(LocationOperationState.ADD);
                                    EnableDisableTier(TierOperationState.ADD);
                                    //cmbCreatePromoStatus.SelectedValue = (int)Promotion.Status.Active;
                                    break;
                                    #endregion
                                }
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// Enable disable logic for condition tab
        /// </summary>
        /// <param name="state"></param>
        private void EnableDisableCondition(ConditionOperationState state)
        {
            //Disable Header Promotion Category if any of Location, Tier or Condition grid is popuulated
            if (dgvCreatePromoCondition.RowCount > 0 | dgvCreatePromoLocation.RowCount > 0 | dgvCreatePromoTier.RowCount > 0)
            {
                cmbCreatePromoCategory.Enabled = false;
            }
            else
            {
                cmbCreatePromoCategory.Enabled = true;
            }

            //Enable all the condition controls
            cmbCreatePromoConditionType.Enabled = true;
            cmbCreatePromoConditionOn.Enabled = true;
            cmbCreatePromoConditionCode.Enabled = true;
            txtCreatePromoConditionCode.Enabled = true;
            btnCreatePromoConditionCode.Enabled = true;
            //txtCreatePromoConditionQty.Enabled = true;
            txtCreatePromoConditionMinQty.Enabled = true;
            txtCreatePromoConditionMaxQty.Enabled = true;
            cmbCreatePromoConditionDiscountType.Enabled = true;
            txtCreatePromoConditionDiscountValue.Enabled = true;
            dgvCreatePromoCondition.Columns[CON_CONDITION_TIER_ADD].Visible = true;
            tabPromotion.TabPages.Remove(tabTier);

            switch ((int)state)
            {
                case ((int)ConditionOperationState.ADD):
                    {
                        #region Add Case
                        DataTable dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_CONDITIONON, 0, 0, 0));
                        switch (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue))
                        {
                            case (int)Promotion.PromotionCategoryType.Line:
                                {
                                    #region Enable disable line
                                    cmbCreatePromoConditionType.Enabled = false;
                                    cmbCreatePromoConditionType.SelectedValue = (int)Promotion.ConditionType.Buy;
                                    txtCreatePromoConditionMinQty.Enabled = false;
                                    txtCreatePromoConditionMaxQty.Enabled = false;

                                    txtCreatePromoConditionQty.Enabled = false;
                                    dgvCreatePromoCondition.Columns[CON_CONDITION_TIER_ADD].Visible = false;
                                    break;
                                    #endregion
                                }
                            case (int)Promotion.PromotionCategoryType.Quantity:
                                {
                                    #region Enable disable quantity
                                    txtCreatePromoConditionMinQty.Enabled = false;
                                    txtCreatePromoConditionMaxQty.Enabled = false;
                                    dgvCreatePromoCondition.Columns[CON_CONDITION_TIER_ADD].Visible = false;

                                    if (Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) == (int)Promotion.ConditionType.Buy)
                                    {
                                        cmbCreatePromoConditionDiscountType.Enabled = false;
                                        txtCreatePromoConditionDiscountValue.Enabled = false;
                                    }
                                    else if (Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) == (int)Promotion.ConditionType.Get)
                                    {
                                        cmbCreatePromoConditionDiscountType.Enabled = true;
                                        txtCreatePromoConditionDiscountValue.Enabled = true;

                                        dtTemp.DefaultView.RowFilter = "KeyCode1 IN (-1,1)";
                                        dtTemp = dtTemp.DefaultView.ToTable();
                                    }
                                    else
                                    {
                                        cmbCreatePromoConditionDiscountType.Enabled = false;
                                        txtCreatePromoConditionDiscountValue.Enabled = false;
                                    }
                                    #endregion

                                    //#region Reset Condition-On values
                                    //DataTable dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_CONDITIONON, 0, 0, 0));
                                    //if (Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) == (int)Promotion.ConditionType.Buy)
                                    //{
                                    //    dtTemp.DefaultView.RowFilter = "KeyCode1 IN (1,-1)";
                                    //    dtTemp = dtTemp.DefaultView.ToTable();
                                    //}
                                    //cmbCreatePromoConditionOn.DataSource = dtTemp;
                                    //cmbCreatePromoConditionOn.ValueMember = Common.KEYCODE1;
                                    //cmbCreatePromoConditionOn.DisplayMember = Common.KEYVALUE1;
                                    //#endregion

                                    break;
                                }
                            //Bill buster and Volumn would be handled in the same way    
                            case (int)Promotion.PromotionCategoryType.BillBuster:
                            case (int)Promotion.PromotionCategoryType.Volume:
                                {
                                    #region Enable disable Volumn or Bill Buster
                                    cmbCreatePromoConditionType.Enabled = false;
                                    cmbCreatePromoConditionType.SelectedValue = (int)Promotion.ConditionType.Buy;

                                    //For Volume Promotion,
                                    //Condition-On is applicable only on Product, hence the RowFilter applied to the effect
                                    if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.Volume)
                                    {
                                        dtTemp.DefaultView.RowFilter = "KeyCode1 IN (-1,1)";
                                        dtTemp = dtTemp.DefaultView.ToTable();
                                    }

                                    //if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.BillBuster)
                                    //{
                                    //    if (cmbCreatePromoConditionOn.SelectedValue != null)
                                    //    {
                                    //        if (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Common.INT_DBNULL)
                                    //        {
                                    //            txtCreatePromoConditionQty.Enabled = false;
                                    //        }
                                    //        else
                                    //        {
                                    //            txtCreatePromoConditionQty.Enabled = true;
                                    //        }
                                    //    }
                                    //    txtCreatePromoConditionQty.Text = "0";
                                    //}

                                    txtCreatePromoConditionMinQty.Enabled = false;
                                    txtCreatePromoConditionMaxQty.Enabled = false;
                                    txtCreatePromoConditionQty.Enabled = false;
                                    txtCreatePromoConditionQty.Text = "0";
                                    cmbCreatePromoConditionDiscountType.Enabled = false;
                                    txtCreatePromoConditionDiscountValue.Enabled = false;
                                    break;
                                    #endregion
                                }
                        }

                        cmbCreatePromoConditionOn.DataSource = dtTemp;
                        cmbCreatePromoConditionOn.ValueMember = Common.KEYCODE1;
                        cmbCreatePromoConditionOn.DisplayMember = Common.KEYVALUE1;


                        #endregion
                    }
                    break;

                case ((int)ConditionOperationState.CLEAR):
                    {
                        #region Clear Case
                        switch (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue))
                        {

                            case (int)Promotion.PromotionCategoryType.Line:
                                {
                                    #region Clear Line Promotion
                                    dgvCreatePromoCondition.Columns[CON_CONDITION_TIER_ADD].Visible = false;
                                    cmbCreatePromoConditionType.Enabled = false;
                                    cmbCreatePromoConditionType.SelectedValue = (int)Promotion.ConditionType.Buy;
                                    txtCreatePromoConditionQty.Text = "1";
                                    txtCreatePromoConditionMinQty.Enabled = false;
                                    txtCreatePromoConditionMaxQty.Enabled = false;
                                    break;
                                    #endregion
                                }
                            case (int)Promotion.PromotionCategoryType.Quantity:
                                {
                                    #region Clear Quantity Promotion
                                    dgvCreatePromoCondition.Columns[CON_CONDITION_TIER_ADD].Visible = false;
                                    txtCreatePromoConditionMinQty.Enabled = false;
                                    txtCreatePromoConditionMaxQty.Enabled = false;
                                    cmbCreatePromoConditionType.SelectedIndex = 0;

                                    if (Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) == (int)Promotion.ConditionType.Buy)
                                    {
                                        cmbCreatePromoConditionDiscountType.Enabled = false;
                                        txtCreatePromoConditionDiscountValue.Enabled = false;
                                    }
                                    else if (Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) == (int)Promotion.ConditionType.Get)
                                    {
                                        cmbCreatePromoConditionDiscountType.Enabled = true;
                                        txtCreatePromoConditionDiscountValue.Enabled = true;
                                    }
                                    else
                                    {
                                        cmbCreatePromoConditionDiscountType.Enabled = false;
                                        txtCreatePromoConditionDiscountValue.Enabled = false;
                                    }
                                    break;
                                    #endregion

                                }
                            //Bill Buster and volumn would be handled in the same way
                            case (int)Promotion.PromotionCategoryType.BillBuster:
                            case (int)Promotion.PromotionCategoryType.Volume:
                                {
                                    #region Clear Quantity Promotion
                                    cmbCreatePromoConditionType.Enabled = false;
                                    cmbCreatePromoConditionType.SelectedValue = (int)Promotion.ConditionType.Buy;
                                    txtCreatePromoConditionMinQty.Enabled = false;
                                    txtCreatePromoConditionMaxQty.Enabled = false;
                                    txtCreatePromoConditionQty.Enabled = false;
                                    cmbCreatePromoConditionDiscountType.Enabled = false;
                                    txtCreatePromoConditionDiscountValue.Enabled = false;
                                    break;
                                    #endregion
                                }
                        }
                        dgvCreatePromoCondition.ClearSelection();
                        #endregion
                    }
                    break;

                case ((int)ConditionOperationState.EDIT):
                    {
                        #region Edit Case
                        switch (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue))
                        {

                            case (int)Promotion.PromotionCategoryType.Line:
                                {
                                    #region Enable/Disable Line
                                    dgvCreatePromoCondition.Columns[CON_CONDITION_TIER_ADD].Visible = false;
                                    cmbCreatePromoConditionType.Enabled = false;
                                    cmbCreatePromoConditionType.SelectedValue = (int)Promotion.ConditionType.Buy;
                                    txtCreatePromoConditionMinQty.Enabled = false;
                                    txtCreatePromoConditionMaxQty.Enabled = false;
                                    cmbCreatePromoConditionOn.Enabled = false;
                                    cmbCreatePromoConditionCode.Enabled = false;
                                    txtCreatePromoConditionCode.Enabled = false;
                                    btnCreatePromoConditionCode.Enabled = false;
                                    break;
                                    #endregion
                                }
                            case (int)Promotion.PromotionCategoryType.Quantity:
                                {
                                    #region Enable disable quantity
                                    dgvCreatePromoCondition.Columns[CON_CONDITION_TIER_ADD].Visible = false;
                                    txtCreatePromoConditionMinQty.Enabled = false;
                                    txtCreatePromoConditionMaxQty.Enabled = false;
                                    cmbCreatePromoConditionType.Enabled = false;
                                    cmbCreatePromoConditionOn.Enabled = false;
                                    cmbCreatePromoConditionCode.Enabled = false;
                                    txtCreatePromoConditionCode.Enabled = false;
                                    btnCreatePromoConditionCode.Enabled = false;

                                    if (Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) == (int)Promotion.ConditionType.Buy)
                                    {
                                        cmbCreatePromoConditionDiscountType.Enabled = false;
                                        txtCreatePromoConditionDiscountValue.Enabled = false;
                                    }
                                    else if (Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) == (int)Promotion.ConditionType.Get)
                                    {
                                        cmbCreatePromoConditionDiscountType.Enabled = true;
                                        txtCreatePromoConditionDiscountValue.Enabled = true;
                                    }
                                    else
                                    {
                                        cmbCreatePromoConditionDiscountType.Enabled = false;
                                        txtCreatePromoConditionDiscountValue.Enabled = false;
                                    }
                                    break;
                                    #endregion

                                }
                            //Bill buster and volumn would be handled in the same way
                            case (int)Promotion.PromotionCategoryType.BillBuster:
                            case (int)Promotion.PromotionCategoryType.Volume:
                                {
                                    #region Enable disable Volumn Promotion
                                    //dgvCreatePromoCondition.Columns[CON_CONDITION_TIER_ADD].Visible = false;
                                    cmbCreatePromoConditionType.Enabled = false;
                                    cmbCreatePromoConditionOn.Enabled = false;
                                    cmbCreatePromoConditionCode.Enabled = false;
                                    txtCreatePromoConditionCode.Enabled = false;
                                    btnCreatePromoConditionCode.Enabled = false;
                                    txtCreatePromoConditionMinQty.Enabled = false;
                                    txtCreatePromoConditionMaxQty.Enabled = false;
                                    txtCreatePromoConditionQty.Enabled = false;
                                    cmbCreatePromoConditionDiscountType.Enabled = false;
                                    txtCreatePromoConditionDiscountValue.Enabled = false;
                                    break;
                                    #endregion
                                }
                        }


                        #endregion
                    }
                    break;
            }
        }

        /// <summary>
        /// Enable disable logic for location tab
        /// </summary>
        /// <param name="state"></param>
        private void EnableDisableLocation(LocationOperationState state)
        {
            //Disable Header Promotion Category if any of Location, Tier or Condition grid is popuulated
            if (dgvCreatePromoCondition.RowCount > 0 | dgvCreatePromoLocation.RowCount > 0 | dgvCreatePromoTier.RowCount > 0)
            {
                cmbCreatePromoCategory.Enabled = false;
            }
            else
            {
                cmbCreatePromoCategory.Enabled = true;
            }


            if (state == LocationOperationState.EDIT)
            {
                //cmbCreatePromoLocation.Enabled = false;
                chklstPromoLocations.Enabled = false;
            }
            else if (state == LocationOperationState.CLEAR)
            {
                //cmbCreatePromoLocation.Enabled = true;
                chklstPromoLocations.Enabled = true;
                dgvCreatePromoLocation.ClearSelection();
            }
        }

        /// <summary>
        /// Enable disable logic for Tier Tab
        /// </summary>
        /// <param name="state"></param>
        private void EnableDisableTier(TierOperationState state)
        {
            //Disable Header Promotion Category if any of Location, Tier or Condition grid is popuulated
            if (dgvCreatePromoCondition.RowCount > 0 | dgvCreatePromoLocation.RowCount > 0 | dgvCreatePromoTier.RowCount > 0)
            {
                cmbCreatePromoCategory.Enabled = false;
            }
            else
            {
                cmbCreatePromoCategory.Enabled = true;
            }

            if (state == TierOperationState.EDIT)
            {
                txtCreatePromoTierBuyQtyTo.Enabled = false;

            }
            else if (state == TierOperationState.CLEAR)
            {
                txtCreatePromoTierBuyQtyTo.Enabled = true;
            }
        }

        /// <summary>
        /// Populate the screen based on promotion object
        /// </summary>
        /// <param name="promotion"></param>
        private void PopulateForm(Promotion promotion)
        {
            //Other elements
            txtCreatePromotionName.Text = promotion.PromotionName;
            cmbCreatePromoCategory.SelectedValue = promotion.PromotionCategoryId;
            txtCreatePromoCode.Text = promotion.PromotionCode;
            dtpCreatePromoStartDate.Value = promotion.StartDate;
            dtpCreatePromoEndDate.Value = promotion.EndDate;
            if (promotion.DurationStart == Common.DATETIME_NULL)
            {
                dtpCreatePromoDurationStart.Value = DateTime.Now;
                dtpCreatePromoDurationStart.Checked = false;
            }
            else
            {
                dtpCreatePromoDurationStart.Value = promotion.DurationStart;
            }

            if (promotion.DurationEnd == Common.DATETIME_NULL)
            {
                dtpCreatePromoDurationEnd.Value = DateTime.Now;
                dtpCreatePromoDurationEnd.Checked = false;
            }
            else
            {
                dtpCreatePromoDurationEnd.Value = promotion.DurationEnd;
            }

            cmbCreatePromoDiscountType.SelectedValue = promotion.DiscountTypeId;
            txtCreatePromoDiscountValue.Text = promotion.DisplayDiscountValue.ToString();
            txtCreatePromoMaxOrderQty.Text = promotion.DisplayMaxOrderQty.ToString();
            cmbCreatePromoStatus.SelectedValue = promotion.StatusId;
            cmbCreatePromotionApp.SelectedValue = promotion.ApplicabilityId;
            cmbCreatePromoBuyType.SelectedValue = promotion.BuyConditionTypeId;
            cmbCreatePromoGetType.SelectedValue = promotion.GetConditionTypeId;
            txtCreatePromoRepeat.Text = promotion.RepeatFactor.ToString();

            //Bind Condition and Location grids
            this.dgvCreatePromoCondition.SelectionChanged -= new System.EventHandler(this.dgvCreatePromoCondition_SelectionChanged);
            dgvCreatePromoCondition.DataSource = new List<PromotionCondition>();
            if (promotion.Conditions.Count > 0)
            {
                dgvCreatePromoCondition.DataSource = promotion.Conditions;
                ((CurrencyManager)this.BindingContext[promotion.Conditions]).Refresh();
            }
            //dgvCreatePromoCondition.DataSource = promotion.Conditions;
            dgvCreatePromoCondition.ClearSelection();
            this.dgvCreatePromoCondition.SelectionChanged += new System.EventHandler(this.dgvCreatePromoCondition_SelectionChanged);
            dgvCreatePromoCondition.ClearSelection();

            this.dgvCreatePromoLocation.SelectionChanged -= new System.EventHandler(this.dgvCreatePromoLocation_SelectionChanged);
            dgvCreatePromoLocation.DataSource = promotion.Locations;
            dgvCreatePromoLocation.DataSource = new List<PromotionLocation>();
            if (promotion.Locations.Count > 0)
            {
                dgvCreatePromoLocation.DataSource = promotion.Locations;
                ((CurrencyManager)this.BindingContext[promotion.Locations]).Refresh();
            }
            dgvCreatePromoLocation.ClearSelection();
            this.dgvCreatePromoLocation.SelectionChanged += new System.EventHandler(this.dgvCreatePromoLocation_SelectionChanged);
            dgvCreatePromoLocation.ClearSelection();
            #region Shopping Cart
            /* Roshan */
            if (promotion.POS == true)
            {
                chkPOS.Checked = true;
            }
            else
            {
                chkPOS.Checked = false;
            }
            if (promotion.Web == true)
            {
                chkWeb.Checked = true;
            }
            else
            {
                chkWeb.Checked = false;
            }
            if (string.IsNullOrEmpty(promotion.WebImage) == false)
            {
               // this.ImgWebPromotion.Load(ConfigurationManager.AppSettings["WebImagesPath"] + promotion.WebImage);
               // LoadedImage = promotion.WebImage;
                try
                {
                    string imagePath = DownLoad(promotion.WebImage);
                    this.ImgWebPromotion.Load(imagePath);
                    LoadedImage = promotion.WebImage;

                    if (File.Exists(imagePath))
                        File.Delete(imagePath);
                }
                catch(Exception ex)
                {
                    ImgWebPromotion.Image = null;
                    this.LoadedImage = "";
                    this.Filepath = "";
                    Common.LogException(ex);
                }
                
            }        
            else
            {
                ImgWebPromotion.Image = null;
                this.LoadedImage = "";
                this.Filepath = "";
            }
            Environment.CurrentDirectory = EnvironmentPath;
            /* Roshan */

            #endregion
        }

        /// <summary>
        /// Populate Promotion Condition Values
        /// </summary>
        private void PopulatePromotionCondition(PromotionCondition condition)
        {
            cmbCreatePromoConditionType.SelectedValue = condition.ConditionTypeId;
            cmbCreatePromoConditionOn.SelectedValue = condition.ConditionOnId;
            cmbCreatePromoConditionCode.SelectedValue = condition.ConditionCodeId;
            txtCreatePromoConditionCode.Text = condition.ConditionCodeVal;
            txtCreatePromoConditionMinQty.Text = condition.DisplayMinBuyQty.ToString();
            txtCreatePromoConditionMaxQty.Text = condition.DisplayMaxBuyQty.ToString();
            txtCreatePromoConditionQty.Text = condition.DisplayQty.ToString();
            cmbCreatePromoConditionDiscountType.SelectedValue = condition.DiscountTypeId;
            txtCreatePromoConditionDiscountValue.Text = condition.DisplayDiscountValue.ToString();
            cmbCreatePromoConditionStatus.SelectedValue = condition.StatusId;
        }

        /// <summary>
        /// Populate Promotion Condition Values
        /// </summary>
        private void PopulatePromotionTier(PromotionTier tier)
        {
            txtCreatePromoTierBuyQtyFrom.Text = Convert.ToInt32(tier.BuyQtyFrom).ToString();

            txtCreatePromoTierBuyQtyTo.Text = Convert.ToInt32(tier.BuyQtyTo).ToString();
            cmbCreatePromoTierDiscountType.SelectedValue = Convert.ToInt32(tier.DiscountTypeId).ToString();
            txtCreatePromoTierDiscountValue.Text = tier.DiscountValue.ToString();
            cmbCreatePromoTierStatus.SelectedValue = tier.StatusId;
        }

        /// <summary>
        /// Populate Promotion Location Values
        /// </summary>
        private void PopulatePromotionLocation(PromotionLocation location)
        {
            //cmbCreatePromoLocation.SelectedValue = location.LocationId;
            chklstPromoLocations.SelectedValue = location.LocationId;
            foreach (object otherSelectedIndices in chklstPromoLocations.CheckedIndices)
            {
                chklstPromoLocations.SetItemChecked((int)otherSelectedIndices, false);
            }
            //if (((DataRowView)chklstPromoLocations.Items[chklstPromoLocations.SelectedIndex]).
            if ((location.StatusId == 1) && (chklstPromoLocations.SelectedIndex > -1))
            {
                chklstPromoLocations.SetItemChecked(chklstPromoLocations.SelectedIndex, true);
            }

            cmbCreatePromoLocStatus.SelectedValue = location.StatusId;
        }

        /// <summary>
        /// This method would populate all the combo boxes from Parameter Master and related tables.
        /// This would called only once to get valid values for combo
        /// </summary>
        private void PopulateCombo()
        {
            //Bind Status Combobox
            DataTable dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_CATEGORY, 0, 0, 0));
            cmbCreatePromoCategory.DataSource = dtTemp;
            cmbCreatePromoCategory.ValueMember = Common.KEYCODE1;
            cmbCreatePromoCategory.DisplayMember = Common.KEYVALUE1;

            this.cmbCreatePromoCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCreatePromoCategory_SelectedIndexChanged);

            dtTemp = dtTemp.Copy();
            cmbSearchPromotionCategory.DataSource = dtTemp;
            cmbSearchPromotionCategory.ValueMember = Common.KEYCODE1;
            cmbSearchPromotionCategory.DisplayMember = Common.KEYVALUE1;

            dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_DISCOUNT_TYPE, 0, 0, 0));
            cmbCreatePromoDiscountType.DataSource = dtTemp;
            cmbCreatePromoDiscountType.ValueMember = Common.KEYCODE1;
            cmbCreatePromoDiscountType.DisplayMember = Common.KEYVALUE1;


            dtTemp = dtTemp.Copy();
            cmbCreatePromoConditionDiscountType.DataSource = dtTemp;
            cmbCreatePromoConditionDiscountType.ValueMember = Common.KEYCODE1;
            cmbCreatePromoConditionDiscountType.DisplayMember = Common.KEYVALUE1;

            dtTemp = dtTemp.Copy();
            cmbCreatePromoTierDiscountType.DataSource = dtTemp;
            cmbCreatePromoTierDiscountType.ValueMember = Common.KEYCODE1;
            cmbCreatePromoTierDiscountType.DisplayMember = Common.KEYVALUE1;

            dtTemp = dtTemp.Copy();
            cmbSearchPromoDiscountType.DataSource = dtTemp;
            cmbSearchPromoDiscountType.ValueMember = Common.KEYCODE1;
            cmbSearchPromoDiscountType.DisplayMember = Common.KEYVALUE1;


            dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMO_STATUS, 0, 0, 0));
            //Remove the 'Select' row
            dtTemp.Select(Common.KEYCODE1 + "=" + Common.INT_DBNULL.ToString())[0].Delete();
            dtTemp.Select(Common.KEYCODE1 + "=3")[0].Delete();
            dtTemp.AcceptChanges();
            cmbCreatePromoStatus.DataSource = dtTemp;
            cmbCreatePromoStatus.ValueMember = Common.KEYCODE1;
            cmbCreatePromoStatus.DisplayMember = Common.KEYVALUE1;

            dtTemp = dtTemp.Copy();
            cmbSearchPromoStatus.DataSource = dtTemp;
            cmbSearchPromoStatus.ValueMember = Common.KEYCODE1;
            cmbSearchPromoStatus.DisplayMember = Common.KEYVALUE1;

            dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_APPLICABILITY, 0, 0, 0));
            cmbCreatePromotionApp.DataSource = dtTemp;
            cmbCreatePromotionApp.ValueMember = Common.KEYCODE1;
            cmbCreatePromotionApp.DisplayMember = Common.KEYVALUE1;

            dtTemp = dtTemp.Copy();
            cmbSearchPromoApp.DataSource = dtTemp;
            cmbSearchPromoApp.ValueMember = Common.KEYCODE1;
            cmbSearchPromoApp.DisplayMember = Common.KEYVALUE1;

            dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_CONDOPERATION, 0, 0, 0));
            cmbCreatePromoBuyType.DataSource = dtTemp;
            cmbCreatePromoBuyType.ValueMember = Common.KEYCODE1;
            cmbCreatePromoBuyType.DisplayMember = Common.KEYVALUE1;

            dtTemp = dtTemp.Copy();
            cmbCreatePromoGetType.DataSource = dtTemp;
            cmbCreatePromoGetType.ValueMember = Common.KEYCODE1;
            cmbCreatePromoGetType.DisplayMember = Common.KEYVALUE1;

            dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_CONDITIONTYPE, 0, 0, 0));
            cmbCreatePromoConditionType.DataSource = dtTemp;
            cmbCreatePromoConditionType.ValueMember = Common.KEYCODE1;
            cmbCreatePromoConditionType.DisplayMember = Common.KEYVALUE1;

            dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_CONDITIONON, 0, 0, 0));
            cmbCreatePromoConditionOn.DataSource = dtTemp;
            cmbCreatePromoConditionOn.ValueMember = Common.KEYCODE1;
            cmbCreatePromoConditionOn.DisplayMember = Common.KEYVALUE1;

            dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_LOCSTATUS, 0, 0, 0));
            //Remove the 'Select' row
            dtTemp.Select(Common.KEYCODE1 + "=" + Common.INT_DBNULL.ToString())[0].Delete();
            dtTemp.AcceptChanges();
            cmbCreatePromoLocStatus.DataSource = dtTemp;
            cmbCreatePromoLocStatus.ValueMember = Common.KEYCODE1;
            cmbCreatePromoLocStatus.DisplayMember = Common.KEYVALUE1;

            dtTemp = dtTemp.Copy();
            cmbCreatePromoConditionStatus.DataSource = dtTemp;
            cmbCreatePromoConditionStatus.ValueMember = Common.KEYCODE1;
            cmbCreatePromoConditionStatus.DisplayMember = Common.KEYVALUE1;

            dtTemp = dtTemp.Copy();
            cmbCreatePromoTierStatus.DataSource = dtTemp;
            cmbCreatePromoTierStatus.ValueMember = Common.KEYCODE1;
            cmbCreatePromoTierStatus.DisplayMember = Common.KEYVALUE1;

            /*
            dtTemp = Common.ParameterLookup(Common.ParameterType.AllLocations, new ParameterFilter("", 0, 0, 0));
            cmbCreatePromoLocation.DataSource = dtTemp;
            cmbCreatePromoLocation.ValueMember = "LocationId";
            cmbCreatePromoLocation.DisplayMember = "LocationName";

            this.cmbCreatePromoLocation.SelectedIndexChanged += new System.EventHandler(this.cmbCreatePromoLocation_SelectedIndexChanged);
            txtCreatePromoLocationCode.Text = Validators.CheckForDBNull(dtTemp.Select("LocationId =" + cmbCreatePromoLocation.SelectedValue.ToString())[0]["LocationCode"].ToString(), string.Empty);
            txtCreatePromoLocationType.Text = Validators.CheckForDBNull(dtTemp.Select("LocationId =" + cmbCreatePromoLocation.SelectedValue.ToString())[0]["LocationName"].ToString(), string.Empty);
             * */

            dtTemp = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("", 3, 0, 0));
            dtTemp = dtTemp.Copy();
            //dtTemp.Rows.RemoveAt(0);
            dtTemp.Rows[0]["DisplayName"] = "All";
            dtTemp.Rows[0]["LocationName"] = "All";
            dtTemp.AcceptChanges();
            chklstPromoLocations.DataSource = dtTemp;
            chklstPromoLocations.ValueMember = "LocationId";
            chklstPromoLocations.DisplayMember = "LocationName";
        }

        private void ReflectSelectedPromotion(DataGridView sender)
        {
            ClearForm();
            Promotion tmpPromotion = new Promotion();
            string errorMessage = string.Empty;
            tmpPromotion.PromotionId = Convert.ToInt32(sender.Rows[sender.SelectedRows[0].Index].Cells[CON_SEARCH_PROMOTION_ID].Value);
            promotion = tmpPromotion.Search(Common.ToXml(tmpPromotion), "usp_PromotionSearch", ref errorMessage);

            promotion = ProcessPromotionCondition(promotion, CON_SEARCH_PROMOTION_DISCOUNTTYPE);

            PopulateForm(promotion);
            EnableDisable(OperationState.EDIT);
            tabControlTransaction.SelectedIndex = 1;
        }

        private void ReflectSelectedCondition(DataGridView sender)
        {
            if (sender.SelectedCells.Count > 0)
            {
                //Mode is enabled for the record
                PromotionCondition condition = promotion.Conditions[sender.SelectedCells[0].RowIndex];
                //Update the promotion values

                //this.condition = condition;
                //this.hierarchyLevelId = condition.ConditionCodeId;

                cmbCreatePromoConditionOn.SelectedValue = Common.INT_DBNULL;
                cmbCreatePromoConditionDiscountType.SelectedValue = Common.INT_DBNULL;

                this.condition = condition;
                this.hierarchyLevelId = condition.ConditionCodeId;

                PopulatePromotionCondition(condition);
                btnCreatePromoConditionAdd.Text = "S&ave";
                EnableDisableCondition(ConditionOperationState.EDIT);

                //btnCreatePromoConditionAdd.Enabled = false;

                //this.condition = condition;
                //this.hierarchyLevelId = condition.ConditionCodeId;
            }
            else
            {
                ClearPromotionCondition();
            }
        }

        private void ReflectSelectedLocation(DataGridView sender)
        {
            if (sender.SelectedCells.Count > 0)
            {
                this.location = promotion.Locations[sender.SelectedCells[0].RowIndex];
                //Update the promotion values
                //By Default on row select enable edit mode
                PopulatePromotionLocation(location);
                btnCreatePromoLocationAdd.Text = "S&ave";
                EnableDisableLocation(LocationOperationState.EDIT);
            }
            else
            {
                ClearPromotionLocation();
            }
        }

        private Promotion ProcessPromotionCondition(Promotion currentPromotion, int processingType)
        {
            foreach (PromotionCondition promoCond in currentPromotion.Conditions)
            {
                if ((promoCond.DiscountTypeId == (int)Promotion.DiscountType.FreeItem) ||
                    ((promoCond.DiscountTypeId == (int)Promotion.DiscountType.PercentOff) && (promoCond.DiscountValue == 100m)))
                {
                    switch (processingType)
                    {
                        case CON_SEARCH_PROMOTION_DISCOUNTTYPE:
                            {
                                promoCond.DiscountTypeId = (int)Promotion.DiscountType.FreeItem;
                                promoCond.DiscountTypeVal = "Free Item";
                                promoCond.DiscountValue = 100m;
                            }
                            break;

                        case CON_SAVE_PROMOTION_DISCOUNTTYPE:
                            {
                                promoCond.DiscountTypeId = (int)Promotion.DiscountType.PercentOff;
                                promoCond.DiscountTypeVal = "Percent off";
                                promoCond.DiscountValue = 100m;
                            }
                            break;
                    }
                }

                foreach (PromotionTier promocondTier in promoCond.Tiers)
                {
                    if ((promocondTier.DiscountTypeId == (int)Promotion.DiscountType.FreeItem) ||
                        ((promocondTier.DiscountTypeId == (int)Promotion.DiscountType.PercentOff) && (promocondTier.DiscountValue == 100m)))
                    {
                        switch (processingType)
                        {
                            case CON_SEARCH_PROMOTION_DISCOUNTTYPE:
                                {
                                    promocondTier.DiscountTypeId = (int)Promotion.DiscountType.FreeItem;
                                    promocondTier.DiscountTypeVal = "Free Item";
                                    promocondTier.DiscountValue = 100m;
                                }
                                break;

                            case CON_SAVE_PROMOTION_DISCOUNTTYPE:
                                {
                                    promocondTier.DiscountTypeId = (int)Promotion.DiscountType.PercentOff;
                                    promocondTier.DiscountTypeVal = "Percent off";
                                    promocondTier.DiscountValue = 100m;
                                }
                                break;
                        }
                    }
                    //else if (promocondTier.DiscountValue == 100m)
                    //{
                    //    promocondTier.DiscountTypeId = (int)Promotion.DiscountType.FreeItem;
                    //    promocondTier.DiscountTypeVal = "Free Item";
                    //}
                }
            }

            return currentPromotion;
        }

        #endregion

        #region Events

        /// <summary>
        /// On form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPromotion_Load(object sender, EventArgs e)
        {
            try
            {
                CON_MODULENAME = this.Tag.ToString();
                InitializeRights();

                //Populate data in Promotion elements
                PopulateCombo();

                //Also reset the Promotion elements, disabling them
                ClearForm();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Create tab click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCreate_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Get Condition Code values for the selected condition on 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCreatePromoConditionOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCreatePromoConditionOn.SelectedValue != null)
                {
                    if (cmbCreatePromoConditionOn.SelectedValue.GetType() != typeof(System.Data.DataRowView))
                    {
                        if (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCT))
                        {
                            cmbCreatePromoConditionCode.Visible = true;

                            //Get products and bind it;
                            //for Line Promotion, bind items that are part of Tradable Hierarchy(s)
                            DataTable dtTemp = null;
                            if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.Line)
                            {
                                //dtTemp = Common.ParameterLookup(Common.ParameterType.ItemsAvailableForPromotion, new ParameterFilter("Item", 1, 0, 0));
                                if (condition != null)
                                {
                                    if (condition.ConditionId == Common.INT_DBNULL)
                                    {
                                        Promotion objTemp = new Promotion();
                                        string errorMessage = string.Empty;
                                        dtTemp = objTemp.GetLocationCodes(Convert.ToInt32(cmbCreatePromoCategory.SelectedValue),
                                                                        Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue),
                                                                        dtpCreatePromoStartDate.Value, dtpCreatePromoEndDate.Value,
                                                                        ref errorMessage);
                                    }
                                    else
                                    {
                                        int buyCondition = Common.INT_DBNULL;
                                        if (cmbCreatePromoBuyType.SelectedValue != null)
                                        {
                                            buyCondition = Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue);
                                        }
                                        dtTemp = Common.ParameterLookup(Common.ParameterType.ItemsAvailableForPromotion, new ParameterFilter("Item", 1, buyCondition, 0));
                                    }
                                }
                                else
                                {
                                    Promotion objTemp = new Promotion();
                                    string errorMessage = string.Empty;
                                    dtTemp = objTemp.GetLocationCodes(Convert.ToInt32(cmbCreatePromoCategory.SelectedValue),
                                                                    Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue),
                                                                    dtpCreatePromoStartDate.Value, dtpCreatePromoEndDate.Value,
                                                                    ref errorMessage);
                                }
                            }
                            else
                            {
                                int buyCondition = Common.INT_DBNULL;
                                if (cmbCreatePromoBuyType.SelectedValue != null)
                                {
                                    buyCondition = Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue);
                                }
                                dtTemp = Common.ParameterLookup(Common.ParameterType.ItemsAvailableForPromotion, new ParameterFilter("Item", 1, buyCondition, 0));
                            }
                            cmbCreatePromoConditionCode.DataSource = dtTemp;
                            cmbCreatePromoConditionCode.ValueMember = "ItemId";
                            cmbCreatePromoConditionCode.DisplayMember = "ItemName";

                            if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.BillBuster)
                            {
                                txtCreatePromoConditionQty.Enabled = false;
                                txtCreatePromoConditionQty.Text = "0";
                            }

                            //Clear the Hierarchy Condition Code
                            txtCreatePromoConditionCode.Text = string.Empty;
                            hierarchyLevelId = Common.INT_DBNULL;
                            txtCreatePromoConditionCode.Visible = false;
                            btnCreatePromoConditionCode.Visible = false;
                        }
                        else if (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCTGROUP))
                        {
                            cmbCreatePromoConditionCode.Visible = true;
                            //Get product group and bind it
                            //Get products and bind it
                            DataTable dtTemp = null;
                            if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.Line)
                            {
                                if (condition != null)
                                {
                                    if (condition.ConditionId == Common.INT_DBNULL)
                                    {
                                        //dtTemp = Common.ParameterLookup(Common.ParameterType.ItemsAvailableForPromotion, new ParameterFilter("Item", 1, 0, 0));
                                        Promotion objTemp = new Promotion();
                                        string errorMessage = string.Empty;
                                        dtTemp = objTemp.GetLocationCodes(Convert.ToInt32(cmbCreatePromoCategory.SelectedValue),
                                                                        Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue),
                                                                        dtpCreatePromoStartDate.Value, dtpCreatePromoEndDate.Value,
                                                                        ref errorMessage);
                                    }
                                    else
                                    {
                                        int buyCondition = Common.INT_DBNULL;
                                        if (cmbCreatePromoBuyType.SelectedValue != null)
                                        {
                                            buyCondition = Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue);
                                        }
                                        dtTemp = Common.ParameterLookup(Common.ParameterType.ItemsAvailableForPromotion, new ParameterFilter("Product-Group", 2, buyCondition, 0));
                                    }
                                }
                                else
                                {
                                    Promotion objTemp = new Promotion();
                                    string errorMessage = string.Empty;
                                    dtTemp = objTemp.GetLocationCodes(Convert.ToInt32(cmbCreatePromoCategory.SelectedValue),
                                                                    Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue),
                                                                    dtpCreatePromoStartDate.Value, dtpCreatePromoEndDate.Value,
                                                                    ref errorMessage);
                                }
                            }
                            else
                            {
                                int buyCondition = Common.INT_DBNULL;
                                if (cmbCreatePromoBuyType.SelectedValue != null)
                                {
                                    buyCondition = Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue);
                                }
                                dtTemp = Common.ParameterLookup(Common.ParameterType.ItemsAvailableForPromotion, new ParameterFilter("Product-Group", 2, buyCondition, 0));
                            }
                            cmbCreatePromoConditionCode.DataSource = dtTemp;
                            cmbCreatePromoConditionCode.ValueMember = "GroupItemId";
                            cmbCreatePromoConditionCode.DisplayMember = "GroupName";

                            if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.BillBuster)
                            {
                                txtCreatePromoConditionQty.Enabled = false;
                                txtCreatePromoConditionQty.Text = "0";
                            }

                            //Clear the Hierarchy Condition Code
                            txtCreatePromoConditionCode.Text = string.Empty;
                            hierarchyLevelId = Common.INT_DBNULL;
                            txtCreatePromoConditionCode.Visible = false;
                            btnCreatePromoConditionCode.Visible = false;
                        }
                        else if (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.MERCHANDISINGHIERARCHY))
                        {
                            cmbCreatePromoConditionCode.Visible = false;
                            txtCreatePromoConditionCode.Visible = true;
                            btnCreatePromoConditionCode.Visible = true;

                            if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.BillBuster)
                            {
                                txtCreatePromoConditionQty.Enabled = false;
                                txtCreatePromoConditionQty.Text = "0";
                            }
                        }
                        else
                        {
                            cmbCreatePromoConditionCode.Visible = true;
                            if (cmbCreatePromoConditionCode.DataSource != null)
                            {
                                cmbCreatePromoConditionCode.SelectedValue = Common.INT_DBNULL;
                            }

                            if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.BillBuster)
                            {
                                txtCreatePromoConditionQty.Enabled = false;
                                txtCreatePromoConditionQty.Text = "0";
                            }

                            //Clear the Hierarchy Condition Code
                            txtCreatePromoConditionCode.Text = string.Empty;
                            hierarchyLevelId = Common.INT_DBNULL;
                            txtCreatePromoConditionCode.Visible = false;
                            btnCreatePromoConditionCode.Visible = false;
                        }
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
        /// On change of Locations populate other details of the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCreatePromoLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //On change of location populate other details related to location
            try
            {
                if (cmbCreatePromoLocation.SelectedValue != null)
                {
                    txtCreatePromoLocationCode.Text = Validators.CheckForDBNull(((DataTable)((System.Windows.Forms.ListControl)(sender)).DataSource).Select("LocationId =" + cmbCreatePromoLocation.SelectedValue.ToString())[0]["LocationCode"].ToString(), string.Empty);
                    txtCreatePromoLocationType.Text = Validators.CheckForDBNull(((DataTable)((System.Windows.Forms.ListControl)(sender)).DataSource).Select("LocationId =" + cmbCreatePromoLocation.SelectedValue.ToString())[0]["LocationType"].ToString(), string.Empty);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Clear the condition Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatePromoConditionClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPromotionCondition();
                errorPromotion.Clear();

                ////TODO: ENABLE/DISABLE BUTTON ACC. TO ASSESED RIGHTS
                //btnCreatePromoConditionAdd.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Clear the Location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatePromoLocationClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPromotionLocation();
                errorPromotion.Clear();

                ////TODO: ENABLE/DISABLE BUTTON ACC. TO ASSESED RIGHTS
                //btnCreatePromoLocationAdd.Enabled = true;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Add the promo conditions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatePromoConditionAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //Disable Header Promotion Category if any of Location, Tier or Condition grid is populated
                if (dgvCreatePromoCondition.RowCount > 0 | dgvCreatePromoLocation.RowCount > 0 | dgvCreatePromoTier.RowCount > 0)
                {
                    cmbCreatePromoCategory.Enabled = false;
                }
                else
                {
                    cmbCreatePromoCategory.Enabled = true;
                }

                //Validate the UI input conditions
                if (ValidateCondition())
                {
                    //The Condition Type, Conditon On, Condition Code must be unique with in the conditions
                    int conditionCodeId = Common.INT_DBNULL;
                    string conditionCode = string.Empty;

                    if ((Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCT))
                        | (Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue) == Convert.ToInt32(PromotionCondition.PromotionConditionOn.PRODUCTGROUP)))
                    {
                        conditionCodeId = Convert.ToInt32(cmbCreatePromoConditionCode.SelectedValue);
                        conditionCode = cmbCreatePromoConditionCode.Text;
                    }
                    else
                    {
                        conditionCodeId = hierarchyLevelId;
                        conditionCode = txtCreatePromoConditionCode.Text;
                    }

                    if (btnCreatePromoConditionAdd.Text == "S&ave")
                    {
                        //Search existing condition and update current condition object
                        PromotionCondition.UpdateCondition(this.condition, promotion.PromotionId,
                                                                                this.condition.ConditionId,
                                                                                Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue),
                                                                                cmbCreatePromoConditionType.Text,
                                                                                Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue),
                                                                                cmbCreatePromoConditionOn.Text,
                                                                                conditionCodeId,
                                                                                conditionCode, Convert.ToDecimal(txtCreatePromoConditionMinQty.Text),
                                                                                Convert.ToDecimal(txtCreatePromoConditionMaxQty.Text),
                                                                                Convert.ToDecimal(txtCreatePromoConditionQty.Text),
                                                                                Convert.ToInt32(cmbCreatePromoConditionDiscountType.SelectedValue),
                                                                                cmbCreatePromoConditionDiscountType.Text,
                                                                                Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text),
                                                                                Convert.ToInt32(cmbCreatePromoConditionStatus.SelectedValue),
                                                                                cmbCreatePromoConditionStatus.Text
                                                                                );
                    }
                    else
                    {
                        //Create the new promotion condition object
                        PromotionCondition condition = new PromotionCondition(promotion.PromotionId,
                                                                                Common.INT_DBNULL,
                                                                                Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue),
                                                                                cmbCreatePromoConditionType.Text,
                                                                                Convert.ToInt32(cmbCreatePromoConditionOn.SelectedValue),
                                                                                cmbCreatePromoConditionOn.Text,
                                                                                conditionCodeId,
                                                                                conditionCode,
                                                                                Convert.ToDecimal(txtCreatePromoConditionMinQty.Text),
                                                                                Convert.ToDecimal(txtCreatePromoConditionMaxQty.Text),
                                                                                Convert.ToDecimal(txtCreatePromoConditionQty.Text),
                                                                                Convert.ToInt32(cmbCreatePromoConditionDiscountType.SelectedValue),
                                                                                cmbCreatePromoConditionDiscountType.Text,
                                                                                Convert.ToDecimal(txtCreatePromoConditionDiscountValue.Text),
                                                                                Convert.ToInt32(cmbCreatePromoConditionStatus.SelectedValue),
                                                                                cmbCreatePromoConditionStatus.Text
                                                                                );

                        //Add it to the current Promotion Object
                        promotion.Conditions.Add(condition);
                    }
                    //Refresh condition grid
                    this.dgvCreatePromoCondition.SelectionChanged -= new System.EventHandler(this.dgvCreatePromoCondition_SelectionChanged);
                    dgvCreatePromoCondition.DataSource = null;
                    dgvCreatePromoCondition.DataSource = new List<PromotionCondition>();
                    if (promotion.Conditions.Count > 0)
                    {
                        dgvCreatePromoCondition.DataSource = promotion.Conditions;
                        ((CurrencyManager)this.BindingContext[promotion.Conditions]).Refresh();

                        if (btnCreatePromoConditionAdd.Text == "A&dd")
                        {
                            dgvCreatePromoCondition.ClearSelection();

                            for (int index = 0; index < promotion.Conditions.Count; index++)
                            {
                                if (promotion.Conditions[index].ConditionId == -1)
                                {
                                    dgvCreatePromoCondition.Rows[index].Cells["SavedRow"].Value = "-1";
                                }
                            }

                            //dgvCreatePromoCondition.Rows[promotion.Conditions.Count - 1].Selected = true;
                            //dgvCreatePromoCondition.Rows[promotion.Conditions.Count - 1].Cells["SavedRow"].Value = "-1";
                        }
                    }

                    this.dgvCreatePromoCondition.SelectionChanged += new System.EventHandler(this.dgvCreatePromoCondition_SelectionChanged);
                    //dgvCreatePromoCondition_SelectionChanged(dgvCreatePromoCondition, null);
                    ReflectSelectedCondition(dgvCreatePromoCondition);
                    //ClearPromotionCondition();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Add locations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatePromoLocationAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //Disable Header Promotion Category if any of Location, Tier or Condition grid is populated
                if (dgvCreatePromoCondition.RowCount > 0 | dgvCreatePromoLocation.RowCount > 0 | dgvCreatePromoTier.RowCount > 0)
                {
                    cmbCreatePromoCategory.Enabled = false;
                }
                else
                {
                    cmbCreatePromoCategory.Enabled = true;
                }

                //Validate the UI input conditions
                if (ValidateLocation())
                {
                    List<int> lstLocIds = new List<int>();

                    if (btnCreatePromoLocationAdd.Text == "S&ave")
                    {
                        /*
                        PromotionLocation location = new PromotionLocation();
                        location.UpdateLocation(this.location, Convert.ToInt32(cmbCreatePromoLocation.SelectedValue), promotion.PromotionId,
                                                                                cmbCreatePromoLocation.Text,
                                                                                txtCreatePromoLocationCode.Text,
                                                                                txtCreatePromoLocationType.Text,
                                                                                Convert.ToInt32(cmbCreatePromoLocStatus.SelectedValue),
                                                                                cmbCreatePromoLocStatus.Text);
                        */

                        PromotionLocation location = new PromotionLocation();
                        string promoLocID = ((DataRowView)chklstPromoLocations.Items[chklstPromoLocations.SelectedIndex]).Row["LocationID"].ToString();
                        string promoLocName = ((DataRowView)chklstPromoLocations.Items[chklstPromoLocations.SelectedIndex]).Row["LocationName"].ToString();
                        string promoLocType = ((DataRowView)chklstPromoLocations.Items[chklstPromoLocations.SelectedIndex]).Row["LocationType"].ToString();
                        string promoLocCode = ((DataRowView)chklstPromoLocations.Items[chklstPromoLocations.SelectedIndex]).Row["LocationCode"].ToString();

                        location.UpdateLocation(this.location, Convert.ToInt32(promoLocID), promotion.PromotionId,
                                                                                promoLocName,
                                                                                promoLocCode,
                                                                                promoLocType,
                                                                                this.location.LineNo,
                                                                                Convert.ToInt32(cmbCreatePromoLocStatus.SelectedValue),
                                                                                cmbCreatePromoLocStatus.Text);

                        /*
                        foreach (object objCheckedItem in chklstPromoLocations.CheckedIndices)
                        {
                            string promoLocID = ((DataRowView)chklstPromoLocations.Items[(int)objCheckedItem]).Row["LocationID"].ToString();
                            string promoLocName = ((DataRowView)chklstPromoLocations.Items[(int)objCheckedItem]).Row["LocationName"].ToString();
                            string promoLocType = ((DataRowView)chklstPromoLocations.Items[(int)objCheckedItem]).Row["LocationType"].ToString();
                            string promoLocCode = ((DataRowView)chklstPromoLocations.Items[(int)objCheckedItem]).Row["LocationCode"].ToString();

                            PromotionLocation location = new PromotionLocation();
                        
                            //location.UpdateLocation(this.location, Convert.ToInt32(cmbCreatePromoLocation.SelectedValue), promotion.PromotionId,
                            //                                                        cmbCreatePromoLocation.Text,
                            //                                                        txtCreatePromoLocationCode.Text,
                            //                                                        txtCreatePromoLocationType.Text,
                            //                                                        Convert.ToInt32(cmbCreatePromoLocStatus.SelectedValue),
                            //                                                        cmbCreatePromoLocStatus.Text);
                        
                            location.UpdateLocation(this.location, Convert.ToInt32(promoLocID), promotion.PromotionId,
                                                    promoLocName,
                                                    promoLocType,
                                                    promoLocCode,
                                                    Convert.ToInt32(cmbCreatePromoLocStatus.SelectedValue),
                                                    cmbCreatePromoLocStatus.Text);
                        }
                        */
                    }
                    else
                    {
                        //Create the new promotion condition object
                        /*
                        PromotionLocation location = new PromotionLocation(Convert.ToInt32(cmbCreatePromoLocation.SelectedValue), promotion.PromotionId,
                                                                                cmbCreatePromoLocation.Text,
                                                                                txtCreatePromoLocationCode.Text,
                                                                                txtCreatePromoLocationType.Text,
                                                                                Convert.ToInt32(cmbCreatePromoLocStatus.SelectedValue),
                                                                                cmbCreatePromoLocStatus.Text
                                                                                );
                         * */
                        foreach (object objCheckedItem in chklstPromoLocations.CheckedIndices)
                        {
                            int locId = Convert.ToInt32(((DataRowView)chklstPromoLocations.Items[(int)objCheckedItem]).Row["LocationId"].ToString());
                            if (locId > -1)
                            {
                                lstLocIds.Add(locId);

                                string promoLocID = ((DataRowView)chklstPromoLocations.Items[(int)objCheckedItem]).Row["LocationID"].ToString();
                                string promoLocName = ((DataRowView)chklstPromoLocations.Items[(int)objCheckedItem]).Row["LocationName"].ToString();
                                string promoLocType = ((DataRowView)chklstPromoLocations.Items[(int)objCheckedItem]).Row["LocationType"].ToString();
                                string promoLocCode = ((DataRowView)chklstPromoLocations.Items[(int)objCheckedItem]).Row["LocationCode"].ToString();
                                //int promoLocLineNo = ((DataRowView)chklstPromoLocations.Items[(int)objCheckedItem]).Row["LineNumber"].ToString();

                                PromotionLocation location = new PromotionLocation(Convert.ToInt32(promoLocID), promotion.PromotionId,
                                                                                        promoLocName,
                                                                                        promoLocCode,
                                                                                        promoLocType,
                                                                                        Common.INT_DBNULL,
                                                                                        Convert.ToInt32(cmbCreatePromoLocStatus.SelectedValue),
                                                                                        cmbCreatePromoLocStatus.Text
                                                                                        );

                                //Add it to the current Promotion Object
                                promotion.Locations.Add(location);
                            }
                        }
                    }

                    //Refresh condition grid
                    this.dgvCreatePromoLocation.SelectionChanged -= new System.EventHandler(this.dgvCreatePromoLocation_SelectionChanged);
                    dgvCreatePromoLocation.DataSource = null;
                    dgvCreatePromoLocation.DataSource = new List<Promotion>();
                    if (promotion.Locations.Count > 0)
                    {
                        dgvCreatePromoLocation.DataSource = promotion.Locations;
                        ((CurrencyManager)this.BindingContext[promotion.Locations]).Refresh();

                        if (btnCreatePromoLocationAdd.Text == "A&dd")
                        {
                            dgvCreatePromoLocation.ClearSelection();
                            //foreach (DataGridViewRow dgvrRow in dgvCreatePromoLocation.Rows)
                            //{
                            //    if (lstLocIds.Contains(Convert.ToInt32(dgvrRow.Cells["LocationId"].Value.ToString())))
                            //    {
                            //        dgvrRow.Cells["SavedRow"].Value = "-1";
                            //    }
                            //}

                            for (int indx = 0; indx < promotion.Locations.Count; indx++)
                            {
                                if (promotion.Locations[indx].LineNo == Common.INT_DBNULL)
                                {
                                    dgvCreatePromoLocation.Rows[indx].Cells["SavedRow"].Value = "-1";
                                }
                            }

                            //dgvCreatePromoLocation.Rows[promotion.Locations.Count - 1].Selected = true;
                        }
                    }
                    this.dgvCreatePromoLocation.SelectionChanged += new System.EventHandler(this.dgvCreatePromoLocation_SelectionChanged);
                    //dgvCreatePromoLocation_SelectionChanged(dgvCreatePromoLocation, null);
                    ReflectSelectedLocation(dgvCreatePromoLocation);
                    //ClearPromotionLocation();
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
                //Disable Header Promotion Category if any of Location, Tier or Condition grid is popuulated
                if (dgvCreatePromoCondition.RowCount > 0 | dgvCreatePromoLocation.RowCount > 0 | dgvCreatePromoTier.RowCount > 0)
                {
                    cmbCreatePromoCategory.Enabled = false;
                }
                else
                {
                    cmbCreatePromoCategory.Enabled = true;
                }

                //Validate the UI input conditions
                if (ValidateTier())
                {
                    if (btnCreatePromoTierAdd.Text == "Save")
                    {
                        PromotionTier tier = new PromotionTier();
                        tier.UpdateTier(this.tier, promotion.PromotionId,
                                                                Common.INT_DBNULL,
                                                                Common.INT_DBNULL,
                                                                this.tier.LineNumber,
                                                                Common.INT_DBNULL,
                                                                string.Empty,
                                                                Common.INT_DBNULL,
                                                                string.Empty,
                                                                Convert.ToDecimal(txtCreatePromoTierBuyQtyFrom.Text),
                                                                Convert.ToDecimal(txtCreatePromoTierBuyQtyTo.Text),
                                                                0,
                                                                Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue),
                                                                cmbCreatePromoTierDiscountType.Text,
                                                                Convert.ToDecimal(txtCreatePromoTierDiscountValue.Text),
                                                                Convert.ToInt32(cmbCreatePromoTierStatus.SelectedValue),
                                                                cmbCreatePromoTierStatus.Text);
                    }
                    else
                    {
                        //Create the new promotion condition object
                        PromotionTier tier = new PromotionTier(promotion.PromotionId,
                                                                Common.INT_DBNULL,
                                                                Common.INT_DBNULL,
                                                                promotion.Tiers.Count + 1,
                                                                Common.INT_DBNULL,
                                                                String.Empty,
                                                                Common.INT_DBNULL,
                                                                string.Empty,
                                                                Convert.ToDecimal(txtCreatePromoTierBuyQtyFrom.Text),
                                                                Convert.ToDecimal(txtCreatePromoTierBuyQtyTo.Text),
                                                                0,
                                                                Convert.ToInt32(cmbCreatePromoTierDiscountType.SelectedValue),
                                                                cmbCreatePromoTierDiscountType.Text,
                                                                Convert.ToDecimal(txtCreatePromoTierDiscountValue.Text),
                                                                Convert.ToInt32(cmbCreatePromoTierStatus.SelectedValue),
                                                                cmbCreatePromoTierStatus.Text
                                                               );

                        //Add it to the current Promotion Object
                        promotion.Tiers.Add(tier);
                    }
                    //rebind the grid
                    dgvCreatePromoTier.DataSource = null;
                    dgvCreatePromoTier.DataSource = promotion.Tiers; //(from p in promotion.Tiers where p.StatusId != 3 select p); 

                    ClearPromotionTier();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //#region Shopping Cart
        //private void Upload(string filename)
        //{

        //    FileInfo fileInf = new FileInfo(filename);

        //    FtpWebRequest reqFTP;

        //    // Create FtpWebRequest object from the Uri provided           
        //    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ConfigurationManager.AppSettings["FTPHOPath"] + "/" + fileInf.Name));

        //    // Provide the WebPermission Credintials
        //    reqFTP.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FTPUser"], ConfigurationManager.AppSettings["FTPPassword"]);
        //    // reqFTP.Credentials = new NetworkCredential("", "");
        //    // By default KeepAlive is true, where the control connection
        //    // is not closed after a command is executed.
        //    reqFTP.KeepAlive = false;

        //    // Specify the command to be executed.
        //    reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

        //    // Specify the data transfer type.
        //    reqFTP.UseBinary = true;

        //    // Notify the server about the size of the uploaded file
        //    reqFTP.ContentLength = filename.Length;

        //    // The buffer size is set to 1MB
        //    //51200
        //    //1048576
        //    int buffLength = 2048;
        //    byte[] buff = new byte[buffLength];
        //    int contentLen;

        //    // Opens a file stream (System.IO.FileStream) to read the file
        //    // to be uploaded
        //    FileStream fs = fileInf.OpenRead();

        //    // Stream to which the file to be upload is written
        //    Stream strm = reqFTP.GetRequestStream();

        //    // Read from the file stream 2kb at a time
        //    contentLen = fs.Read(buff, 0, buffLength);

        //    // Till Stream content ends
        //    while (contentLen != 0)
        //    {
        //        // Write Content from the file stream to the FTP Upload
        //        // Stream
        //        strm.Write(buff, 0, contentLen);
        //        contentLen = fs.Read(buff, 0, buffLength);
        //    }

        //    // Close the file stream and the Request Stream
        //    strm.Close();
        //    fs.Close();
        //    reqFTP = null;

        //}
        //private void DeleteFileAtFTP(string fileName)
        //{
        //    //if (fileName.ToLower().Contains(".zip") || fileName.ToLower().Contains(".zip"))
        //    //{
        //    FtpWebRequest reqFTP;

        //    FileInfo fileInf = new FileInfo(fileName);

        //    // Create FtpWebRequest object from the Uri provided
        //    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ConfigurationManager.AppSettings["FTPHOPath"] + "/" + fileInf.Name));

        //    // Provide the WebPermission Credintials
        //    reqFTP.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FTPUser"], ConfigurationManager.AppSettings["FTPPassword"]);

        //    // By default KeepAlive is true, where the control connection
        //    // is not closed after a command is executed.
        //    reqFTP.KeepAlive = false;
        //    // Specify the data transfer type.
        //    reqFTP.UseBinary = true;
        //    // Specify the command to be executed.
        //    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

        //    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
        //    response.Close();

        //}

        //private string DownLoad(string filename)
        //{
        //    string imageDir = string.Empty;
        //    byte[] downloadedData = new byte[0];
        //    FtpWebRequest reqFTP;

        //    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ConfigurationManager.AppSettings["FTPHOPath"] + "/" + filename));

        //    // Provide the WebPermission Credintials
        //    reqFTP.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FTPUser"], ConfigurationManager.AppSettings["FTPPassword"]);

        //    // By default KeepAlive is true, where the control connection
        //    // is not closed after a command is executed.
        //    reqFTP.KeepAlive = false;
        //    // Specify the data transfer type.
        //    reqFTP.UseBinary = true;


        //    // Specify the command to be executed.
        //    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;

        //    FtpWebResponse response = reqFTP.GetResponse() as FtpWebResponse;
        //    Stream reader = response.GetResponseStream();

        //    try
        //    {
        //        //Download to memory
        //        //Note: adjust the streams here to download directly to the hard drive
        //        MemoryStream memStream = new MemoryStream();
        //        // The buffer size is set to 2kb
        //        int buffLength = 2048;
        //        byte[] buffer = new byte[buffLength]; //downloads in chuncks

        //        while (true)
        //        {
        //            //Try to read the data
        //            int bytesRead = reader.Read(buffer, 0, buffer.Length);
        //            if (bytesRead == 0)
        //            {
        //                break;
        //            }

        //            //Write the downloaded data
        //            memStream.Write(buffer, 0, bytesRead);

        //            //Convert the downloaded stream to a byte array
        //            downloadedData = memStream.ToArray();
        //        }

        //        reqFTP = null;
        //        reader.Close();
        //        memStream.Close();
        //        response.Close();
        //        imageDir = Environment.CurrentDirectory + "/" + "TempWebImages";
        //        if (Directory.Exists(imageDir))
        //            Directory.CreateDirectory(imageDir);
        //        if (downloadedData != null && downloadedData.Length != 0)
        //        {
        //            imageDir = imageDir + "/" + filename;
        //            FileStream newFile = new FileStream(imageDir, FileMode.Create);
        //            newFile.Write(downloadedData, 0, downloadedData.Length);
        //            newFile.Close();
        //        }
        //        return imageDir;
        //    }
        //    catch (Exception ex)
        //    {
        //        return imageDir;
        //    }
        //}

        //#endregion

        #region Shopping Cart
        private bool Upload(string filename)
        {
            bool isSuccess = false;
            try
            {
                FileInfo fileInf = new FileInfo(filename);

                FtpWebRequest reqFTP;

                // Create FtpWebRequest object from the Uri provided           
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ConfigurationManager.AppSettings["FTPHOPath"] + "/" + fileInf.Name));

                // Provide the WebPermission Credintials
                reqFTP.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FTPUser"], ConfigurationManager.AppSettings["FTPPassword"]);
                // reqFTP.Credentials = new NetworkCredential("", "");
                // By default KeepAlive is true, where the control connection
                // is not closed after a command is executed.
                reqFTP.KeepAlive = false;

                // Specify the command to be executed.
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

                // Specify the data transfer type.
                reqFTP.UseBinary = true;

                // Notify the server about the size of the uploaded file
                reqFTP.ContentLength = filename.Length;

                // The buffer size is set to 1MB
                //51200
                //1048576
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;

                // Opens a file stream (System.IO.FileStream) to read the file
                // to be uploaded
                FileStream fs = fileInf.OpenRead();

                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload
                    // Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
                reqFTP = null;
                isSuccess = true;
                return isSuccess;
            }
            catch (Exception)
            {
                return isSuccess;
            }
        }
        private bool DeleteFileAtFTP(string fileName)
        {
            bool isSuccess = false;
            try
            {
                //if (fileName.ToLower().Contains(".zip") || fileName.ToLower().Contains(".zip"))
                //{
                FtpWebRequest reqFTP;

                FileInfo fileInf = new FileInfo(fileName);

                // Create FtpWebRequest object from the Uri provided
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ConfigurationManager.AppSettings["FTPHOPath"] + "/" + fileInf.Name));

                // Provide the WebPermission Credintials
                reqFTP.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FTPUser"], ConfigurationManager.AppSettings["FTPPassword"]);

                // By default KeepAlive is true, where the control connection
                // is not closed after a command is executed.
                reqFTP.KeepAlive = false;
                // Specify the data transfer type.
                reqFTP.UseBinary = true;
                // Specify the command to be executed.
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
                isSuccess = true;
                return isSuccess;
            }
            catch (Exception)
            {
                return isSuccess;
            }

        }
        private string DownLoad(string filename)
        {
            string imageDir = string.Empty;
            try
            {
                byte[] downloadedData = new byte[0];
                FtpWebRequest reqFTP;

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ConfigurationManager.AppSettings["FTPHOPath"] + "/" + filename));

                // Provide the WebPermission Credintials
                reqFTP.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FTPUser"], ConfigurationManager.AppSettings["FTPPassword"]);

                // By default KeepAlive is true, where the control connection
                // is not closed after a command is executed.
                reqFTP.KeepAlive = false;
                // Specify the data transfer type.
                reqFTP.UseBinary = true;

                // Specify the command to be executed.
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;

                FtpWebResponse response = reqFTP.GetResponse() as FtpWebResponse;
                Stream reader = response.GetResponseStream();


                //Download to memory
                //Note: adjust the streams here to download directly to the hard drive
                MemoryStream memStream = new MemoryStream();
                // The buffer size is set to 2kb
                int buffLength = 2048;
                byte[] buffer = new byte[buffLength]; //downloads in chuncks

                while (true)
                {
                    //Try to read the data
                    int bytesRead = reader.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    //Write the downloaded data
                    memStream.Write(buffer, 0, bytesRead);

                    //Convert the downloaded stream to a byte array
                    downloadedData = memStream.ToArray();
                }

                reqFTP = null;
                reader.Close();
                memStream.Close();
                response.Close();
                imageDir = Environment.CurrentDirectory + "/" + "TempWebImages";
                if (Directory.Exists(imageDir))
                    Directory.CreateDirectory(imageDir);
                if (downloadedData != null && downloadedData.Length != 0)
                {
                    imageDir = imageDir + "/" + filename;
                    FileStream newFile = new FileStream(imageDir, FileMode.Create);
                    newFile.Write(downloadedData, 0, downloadedData.Length);
                    newFile.Close();
                }
                return imageDir;
            }
            catch (Exception)
            {
                return imageDir;
            }
        }

        #endregion

        /// <summary>
        /// Save the Promotion Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    bool POS = false;
                    bool web = false;
                    if (chkPOS.Checked == true)
                    {
                        POS = true;
                    }
                    if (chkWeb.Checked == true)
                    {
                        web = true;
                    }
                    #region Shopping Cart
                    try
                    {
                        string imagePath = string.Empty;
                        string imageDir = Environment.CurrentDirectory + "/" + "TempWebImages";
                        if (Filepath != string.Empty)
                        {
                            ItemImage = DateTime.Now.Ticks + Path.GetFileName(Filepath);
                            if (!Directory.Exists(imageDir))
                                Directory.CreateDirectory(imageDir);

                            imagePath = imageDir + "/" + ItemImage;
                            File.Copy(Filepath, imagePath, true);

                            EnvironmentPath = Environment.CurrentDirectory;

                            string imageName = promotion.GetPromotionImage(promotion.PromotionId, "usp_GetPromotionImage");

                            if (LoadedImage != string.Empty)
                            {
                                DeleteFileAtFTP(imageName);
                            }
                            //ItemImage = Path.GetFileName(Filepath);
                            Upload(imagePath);
                        }
                        else  // Edit Mode.  No new image is selected by the user.
                        {
                            ItemImage = LoadedImage;
                        }
                        if (Filepath != string.Empty)
                            File.Delete(imagePath);//To Delete Temp file from local drive after uploaded on FTP Server

                        Environment.CurrentDirectory = CurrentEnvironmentPath;
                        EnvironmentPath = Environment.CurrentDirectory;
                    
                    }
                    catch (Exception ex)
                    {
                        Environment.CurrentDirectory = CurrentEnvironmentPath;
                        Common.LogException(ex);
                    }
                    

                    #endregion

                    promotion = ProcessPromotionCondition(promotion, CON_SAVE_PROMOTION_DISCOUNTTYPE);
                    this.promotion = new Promotion(promotion.PromotionId,
                                                  txtCreatePromotionName.Text,
                                                  Convert.ToInt32(cmbCreatePromoCategory.SelectedValue),
                                                  cmbCreatePromoCategory.Text,
                                                  txtCreatePromoCode.Text,
                                                  Convert.ToDateTime(dtpCreatePromoStartDate.Value.ToString(Common.DATE_TIME_FORMAT)),
                                                  Convert.ToDateTime(dtpCreatePromoEndDate.Value.ToString(Common.DATE_TIME_FORMAT)),
                                                  dtpCreatePromoDurationStart.Checked == true ? Convert.ToDateTime(dtpCreatePromoDurationStart.Value.ToString(Common.DATE_TIME_FORMAT)) : Common.DATETIME_NULL,
                                                  dtpCreatePromoDurationEnd.Checked == true ? Convert.ToDateTime(dtpCreatePromoDurationEnd.Value.ToString(Common.DATE_TIME_FORMAT)) : Common.DATETIME_NULL,
                                                  Convert.ToInt32(cmbCreatePromoDiscountType.SelectedValue),
                                                  cmbCreatePromoDiscountType.Text,
                                                  Convert.ToDecimal(txtCreatePromoDiscountValue.Text),
                                                  Convert.ToDecimal(txtCreatePromoMaxOrderQty.Text),
                                                  Convert.ToInt32(cmbCreatePromoStatus.SelectedValue),
                                                  cmbCreatePromoStatus.Text,
                                                  userId,
                                                  userId,
                                                  promotion.CreatedDate,
                                                  promotion.ModifiedDate,
                                                  Convert.ToInt32(cmbCreatePromotionApp.SelectedValue),
                                                  cmbCreatePromotionApp.Text,
                                                  Convert.ToInt32(cmbCreatePromoBuyType.SelectedValue),
                                                  cmbCreatePromoBuyType.Text,
                                                  Convert.ToInt32(cmbCreatePromoGetType.SelectedValue),
                                                  cmbCreatePromoGetType.Text,
                                                  Convert.ToInt32(txtCreatePromoRepeat.Text),
                                                  promotion.Conditions,
                                                  promotion.Locations,
                                                  promotion.Tiers,
                                                  POS,
                                                  web,
                                                  ItemImage
                                                  );
                    //Save the Promotions
                    string errorMessage = string.Empty;
                    //Save the Promotion Object
                    Int32 promotionId = Common.INT_DBNULL;
                    Boolean IsSuccess = promotion.Save(Common.ToXml(promotion), "usp_PromotionSave", ref promotionId, ref errorMessage);
                    if (!IsSuccess)
                    {
                        if (errorMessage.StartsWith("30001"))
                        {
                            MessageBox.Show(Common.GetMessage("30001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Common.LogException(new Exception(errorMessage));
                        }
                        else
                        {
                            MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();

                        //Repopulate the form with the currently loaded Promotion
                        Promotion tmpPromotion = new Promotion();
                        tmpPromotion.PromotionId = promotionId;
                        this.promotion = tmpPromotion.Search(Common.ToXml(tmpPromotion), "usp_PromotionSearch", ref errorMessage);

                        PopulateForm(this.promotion);
                        EnableDisable(OperationState.EDIT);
                    }

                    //int selectedRowIndex = dgvSearchPromo.SelectedRows[0].Index;
                    SearchForPromotion();
                    //if (dgvSearchPromo.Rows.Count > selectedRowIndex)
                    //{
                    //    dgvSearchPromo.Rows[0].Selected = false;
                    //    dgvSearchPromo.Rows[selectedRowIndex].Selected = true;
                    //    dgvSearchPromo.FirstDisplayedScrollingRowIndex = selectedRowIndex;
                    //}
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// On Clear button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatePromoTierClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPromotionTier();

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreatePromoConditionCode_Click(object sender, EventArgs e)
        {
            try
            {
                frmTree objTree = new frmTree("Promotion", "", Common.INT_DBNULL, this);
                Point pointTree = new Point();
                pointTree = this.PointToScreen(txtCreatePromoConditionCode.Location);
                pointTree.Y = pointTree.Y + Common.TREE_COMP_Y;
                pointTree.X = pointTree.X + Common.TREE_COMP_X;
                objTree.Location = pointTree;
                objTree.ShowDialog();
                txtCreatePromoConditionCode.Text = objTree.SelectedText;
                hierarchyLevelId = objTree.SelectedValue;

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// On edit of condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCreatePromoCondition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (((DataGridView)sender).Columns[e.ColumnIndex].Name.ToUpper() == CON_LOCATION_EDIT)
                //{
                //    //Else 
                //    PromotionCondition condition = promotion.Conditions[e.RowIndex];
                //    //Update the promotion values
                //    PopulatePromotionCondition(condition);
                //    btnCreatePromoConditionAdd.Text = "Save";
                //    EnableDisableCondition(ConditionOperationState.EDIT);

                //    this.condition = condition;
                //    this.hierarchyLevelId = condition.ConditionCodeId;
                //}
                if ((((DataGridView)sender).Columns[e.ColumnIndex].Name.ToUpper() == CON_CONDITION_TIER_ADD) && e.RowIndex > -1)
                {
                    promotion = ProcessPromotionCondition(promotion, CON_SEARCH_PROMOTION_DISCOUNTTYPE);
                    PromotionCondition condition = promotion.Conditions[e.RowIndex];
                    EnvironmentPath = Environment.CurrentDirectory;
                    //Update the promotion values
                    PopulatePromotionCondition(condition);
                    this.condition = condition;
                    this.hierarchyLevelId = condition.ConditionCodeId;
                    //Show tier for the current promotion

                    frmConditionTier tier = new frmConditionTier(this.condition, Convert.ToInt32(cmbCreatePromoCategory.SelectedValue));
                    tier.ShowDialog();
                }
                else if ((((DataGridView)sender).Columns[e.ColumnIndex].Name.ToUpper() == CON_CONDITION_TIER_REMOVE) && e.RowIndex > -1)
                {
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells["SavedRow"].Value != null)
                    {
                        DialogResult confirmresult = MessageBox.Show(Common.GetMessage("5012"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmresult == DialogResult.Yes)
                        {
                            promotion.Conditions.Remove(promotion.Conditions[e.RowIndex]);
                            dgvCreatePromoCondition.SelectionChanged -= new EventHandler(dgvCreatePromoCondition_SelectionChanged);
                            dgvCreatePromoCondition.DataSource = null;
                            dgvCreatePromoCondition.DataSource = new List<PromotionCondition>();
                            if (promotion.Conditions.Count > 0)
                            {
                                dgvCreatePromoCondition.DataSource = promotion.Conditions;
                                ((CurrencyManager)this.BindingContext[promotion.Conditions]).Refresh();
                            }
                            dgvCreatePromoCondition.SelectionChanged += new EventHandler(dgvCreatePromoCondition_SelectionChanged);
                            //dgvCreatePromoCondition_SelectionChanged(dgvCreatePromoCondition, null);
                            ClearPromotionCondition();

                            //Make sure that in the remaining conditions, only the unsaved-records are available for deletion
                            for (int index = 0; index < promotion.Conditions.Count; index++)
                            {
                                if (promotion.Conditions[index].ConditionId == -1)
                                {
                                    dgvCreatePromoCondition.Rows[index].Cells["SavedRow"].Value = "-1";
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage("INF0077"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if ((((DataGridView)sender).Columns[e.ColumnIndex].Name.ToUpper() == CON_CONDITION_EDIT) && e.RowIndex > -1)
                {
                    EnableDisableCondition(ConditionOperationState.EDIT);
                }
                //else
                //{
                //}
            }
            //catch (IndexOutOfRangeException IRE)
            //{
            //    Common.LogException(IRE);
            //}
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// On change of Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCreatePromoCondition_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ReflectSelectedCondition((DataGridView)sender);
                //if (dgvCreatePromoCondition.SelectedCells.Count > 0)
                //{
                //    //Mode is enabled for the record
                //    PromotionCondition condition = promotion.Conditions[dgvCreatePromoCondition.SelectedCells[0].RowIndex];
                //    //Update the promotion values

                //    //this.condition = condition;
                //    //this.hierarchyLevelId = condition.ConditionCodeId;

                //    cmbCreatePromoConditionOn.SelectedValue = Common.INT_DBNULL;
                //    cmbCreatePromoConditionDiscountType.SelectedValue = Common.INT_DBNULL;

                //    this.condition = condition;
                //    this.hierarchyLevelId = condition.ConditionCodeId;

                //    PopulatePromotionCondition(condition);
                //    btnCreatePromoConditionAdd.Text = "Save";
                //    EnableDisableCondition(ConditionOperationState.EDIT);

                //    //btnCreatePromoConditionAdd.Enabled = false;

                //    //this.condition = condition;
                //    //this.hierarchyLevelId = condition.ConditionCodeId;
                //}
                //else
                //{
                //    ClearPromotionCondition();
                //}
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// On change of location Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCreatePromoLocation_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ReflectSelectedLocation((DataGridView)sender);
                //btnCreatePromoLocationAdd.Enabled = false;

                //if (dgvCreatePromoLocation.SelectedCells.Count > 0)
                //{
                //    this.location = promotion.Locations[dgvCreatePromoLocation.SelectedCells[0].RowIndex];
                //    //Update the promotion values
                //    //By Default on row select enable edit mode
                //    PopulatePromotionLocation(location);
                //    btnCreatePromoLocationAdd.Text = "Save";
                //    EnableDisableLocation(LocationOperationState.EDIT);
                //}
                //else
                //{
                //    ClearPromotionLocation();
                //}
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// On edit of location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCreatePromoLocation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //When edit button of the location grid is selected.
                //Then change the add mode to save mode
                //if ((((DataGridView)sender).Columns[e.ColumnIndex].Name.ToUpper() == CON_LOCATION_EDIT) && (e.RowIndex > -1))
                //{
                //    btnCreatePromoLocationAdd.Enabled = true;
                //}
                if ((((DataGridView)sender).Columns[e.ColumnIndex].Name.ToUpper() == CON_LOCATION_REMOVE) && (e.RowIndex > -1))
                {
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells["SavedRow"].Value != null)
                    {
                        DialogResult confirmresult = MessageBox.Show(Common.GetMessage("5012"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmresult == DialogResult.Yes)
                        {
                            promotion.Locations.Remove(promotion.Locations[e.RowIndex]);
                            this.dgvCreatePromoLocation.SelectionChanged -= new System.EventHandler(this.dgvCreatePromoLocation_SelectionChanged);
                            dgvCreatePromoLocation.DataSource = null;
                            dgvCreatePromoLocation.DataSource = new List<PromotionLocation>();
                            if (promotion.Locations.Count > 0)
                            {
                                dgvCreatePromoLocation.DataSource = promotion.Locations;
                                ((CurrencyManager)this.BindingContext[promotion.Locations]).Refresh();
                            }
                            this.dgvCreatePromoLocation.SelectionChanged += new System.EventHandler(this.dgvCreatePromoLocation_SelectionChanged);
                            //dgvCreatePromoLocation_SelectionChanged(dgvCreatePromoLocation, null);

                            for (int indx = 0; indx < promotion.Locations.Count; indx++)
                            {
                                if (promotion.Locations[indx].LineNo == Common.INT_DBNULL)
                                {
                                    dgvCreatePromoLocation.Rows[indx].Cells["SavedRow"].Value = "-1";
                                }
                            }

                            ReflectSelectedLocation(dgvCreatePromoLocation);
                        }
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage("INF0077"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (e.RowIndex > -1)
                    {
                        this.location = promotion.Locations[e.RowIndex];
                        //Update the promotion values
                        //By Default on row select enable edit mode
                        PopulatePromotionLocation(location);
                        btnCreatePromoLocationAdd.Text = "S&ave";
                        EnableDisableLocation(LocationOperationState.EDIT);
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// On Edit of Promotion Tiers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCreatePromoTier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //When edit button of the location grid is selected.
                //Then change the add mode to save mode
                if (((DataGridView)sender).Columns[e.ColumnIndex].Name.ToUpper() == CON_LOCATION_EDIT)
                {
                    this.tier = promotion.Tiers[e.RowIndex];
                    //Update the promotion values
                    PopulatePromotionTier(tier);
                    btnCreatePromoTierAdd.Text = "Save";
                    EnableDisableTier(TierOperationState.EDIT);
                }
                else if (((DataGridView)sender).Columns[e.ColumnIndex].Name.ToUpper() == CON_TIER_REMOVE)
                {
                    if (((DataGridView)sender).RowCount == e.RowIndex + 1)
                    {
                        DialogResult confirmresult = MessageBox.Show(Common.GetMessage("5012"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmresult == DialogResult.Yes)
                        {
                            promotion.Tiers.Remove(promotion.Tiers[e.RowIndex]);
                            dgvCreatePromoTier.DataSource = null;
                            dgvCreatePromoTier.DataSource = promotion.Tiers;
                            ClearPromotionTier();
                        }
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage("INF0060"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// On click of Tab page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabTier_Click(object sender, EventArgs e)
        {
            //If there is data in the tier grid. Disable QtyFrom Text Box
            //Populate it with the last value of the QtyTo + 1
            try
            {
                if (dgvCreatePromoTier.Rows.Count > 0)
                {
                    txtCreatePromoTierBuyQtyFrom.Enabled = false;
                }
                else
                {
                    txtCreatePromoTierBuyQtyFrom.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// On change of Promotion Category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCreatePromoCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCreatePromoCategory.SelectedValue != null)
                {
                    if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) != Common.INT_DBNULL)
                    {
                        EnableDisable(OperationState.ADD);

                        //If promotion-category is 'Line', then enable 'Qty' for promotion-condtion;
                        //otherwise disable it
                        if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.Line)
                        {
                            txtCreatePromoConditionQty.Text = "1";
                            txtCreatePromoConditionQty.Enabled = false;

                            DataTable dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_DISCOUNT_TYPE, 0, 0, 0));
                            dtTemp.DefaultView.RowFilter = "KeyCode1 <> 4";
                            dtTemp = dtTemp.DefaultView.ToTable();
                            cmbCreatePromoConditionDiscountType.DataSource = dtTemp;
                            cmbCreatePromoConditionDiscountType.ValueMember = Common.KEYCODE1;
                            cmbCreatePromoConditionDiscountType.DisplayMember = Common.KEYVALUE1;
                        }
                        else
                        {
                            if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) != (int)Promotion.PromotionCategoryType.BillBuster)
                            {
                                txtCreatePromoConditionQty.Text = "0";
                                txtCreatePromoConditionQty.Enabled = true;
                            }

                            DataTable dtTemp = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.PROMOTION_DISCOUNT_TYPE, 0, 0, 0));
                            cmbCreatePromoConditionDiscountType.DataSource = dtTemp;
                            cmbCreatePromoConditionDiscountType.ValueMember = Common.KEYCODE1;
                            cmbCreatePromoConditionDiscountType.DisplayMember = Common.KEYVALUE1;
                        }

                        //If promotion-category is 'Quantity', then enable 'Repeat-factor';
                        //otherwise disable it and mark it as zero
                        if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.Quantity)
                        {
                            txtCreatePromoRepeat.Enabled = true;
                        }
                        else if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.BillBuster)
                        {
                            txtCreatePromoRepeat.Enabled = true;
                            txtCreatePromoRepeat.Text = "0";
                        }
                        else
                        {
                            txtCreatePromoRepeat.Text = "0";
                            txtCreatePromoRepeat.Enabled = false;
                        }

                        //If promotion-category is 'Volume', then disable Condition's 'Qty' field
                        if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.Volume)
                        {
                            txtCreatePromoConditionQty.Enabled = false;
                        }
                    }
                    else
                    {
                        ClearForm();
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
        /// On change of buy condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCreatePromoConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EnableDisableCondition(ConditionOperationState.ADD);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// On click of the search button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchForPromotion();
        }

        private void SearchForPromotion()
        {
            try
            {
                List<Promotion> lstPromotion = new List<Promotion>();
                Promotion tmpPromotion = new Promotion(
                                                        txtSearchPromotionName.Text,
                                                        Convert.ToInt32(cmbSearchPromotionCategory.SelectedValue),
                                                        txtSearchPromotionCode.Text,
                                                        dtpSearchPromoStartDate.Checked == true ? dtpSearchPromoStartDate.Value : Common.DATETIME_NULL,
                                                        dtpSearchPromoEndDate.Checked == true ? dtpSearchPromoEndDate.Value : Common.DATETIME_NULL,
                                                        Convert.ToInt32(cmbSearchPromoDiscountType.SelectedValue),
                                                        Convert.ToInt32(cmbSearchPromoStatus.SelectedValue),
                                                        Convert.ToInt32(cmbSearchPromoApp.SelectedValue)
                                                       );
                tmpPromotion.DurationStart = dtpSearchPromoStartDate.Checked == true ? new DateTime(dtpSearchPromoStartDate.Value.Date.Year, dtpSearchPromoStartDate.Value.Date.Month, dtpSearchPromoStartDate.Value.Date.Day, 0, 0, 0) : Common.DATETIME_NULL;
                tmpPromotion.DurationEnd = dtpSearchPromoEndDate.Checked == true ? new DateTime(dtpSearchPromoEndDate.Value.Date.Year, dtpSearchPromoEndDate.Value.Date.Month, dtpSearchPromoEndDate.Value.Date.Day, 23, 59, 59) : Common.DATETIME_NULL;
                string errorMessage = String.Empty;
                lstPromotion = tmpPromotion.SearchPromotions(Common.ToXml(tmpPromotion), "usp_PromotionSearch", ref errorMessage);
                if (lstPromotion.Count == 0)
                {
                    dgvSearchPromo.DataSource = null;
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dgvSearchPromo.DataSource = lstPromotion;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// On click of View Button in Promotion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSearchPromo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvSearchPromo.Columns[e.ColumnIndex].Name.ToUpper() == CON_SEARCH_VIEW)
                {
                    ReflectSelectedPromotion((DataGridView)sender);
                    //ClearForm();
                    //Promotion tmpPromotion = new Promotion();
                    //string errorMessage = string.Empty;
                    //tmpPromotion.PromotionId = Convert.ToInt32(dgvSearchPromo.Rows[e.RowIndex].Cells[CON_SEARCH_PROMOTION_ID].Value);
                    //promotion = tmpPromotion.Search(Common.ToXml(tmpPromotion), "usp_PromotionSearch", ref errorMessage);

                    //promotion = ProcessPromotionCondition(promotion, CON_SEARCH_PROMOTION_DISCOUNTTYPE);

                    //PopulateForm(promotion);
                    //EnableDisable(OperationState.EDIT);
                    //tabControlTransaction.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSearchPromo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView ctrl = (DataGridView)sender;
                if (ctrl.SelectedRows.Count > 0)
                {
                    if (ctrl.SelectedRows[0].Index > Common.INT_DBNULL)
                    {
                        ReflectSelectedPromotion((DataGridView)sender);
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
        /// Reset Search Button and Reset Create Promotion Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchReset_Click(object sender, EventArgs e)
        {
            try
            {
                //DialogResult resetresult = MessageBox.Show(Common.GetMessage("5006"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (resetresult == DialogResult.Yes)
                {
                    ClearSearchForm();
                    ClearForm();
                    txtSearchPromotionName.Focus();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reset Create Promotion Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateReset_Click(object sender, EventArgs e)
        {
            try
            {
                //DialogResult resetresult = MessageBox.Show(Common.GetMessage("5006"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (resetresult == DialogResult.Yes)
                {
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chklstPromoLocations_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!m_suspendEventHandler)
            {
                if (chklstPromoLocations.SelectedItems.Count == 1)
                {
                    if (chklstPromoLocations.SelectedValue.ToString() == "-1")
                    {
                        CheckedListBox ctrl = (CheckedListBox)sender;
                        if (e.NewValue == CheckState.Checked)
                        {
                            if (ctrl.Items.Count > 1)
                            {
                                m_suspendEventHandler = true;
                                for (int itemIndex = 1; itemIndex < ctrl.Items.Count; itemIndex++)
                                {
                                    //ctrl.SetItemCheckState(itemIndex, CheckState.Checked);
                                    ctrl.SetItemChecked(itemIndex, true);
                                }
                                m_suspendEventHandler = false;
                            }
                        }
                        else
                        {
                            m_suspendEventHandler = true;
                            for (int itemIndex = 1; itemIndex < ctrl.Items.Count; itemIndex++)
                            {
                                ctrl.SetItemCheckState(itemIndex, CheckState.Unchecked);
                            }
                            m_suspendEventHandler = false;
                        }
                    }
                }
            }
        }

        private void cmbCreatePromoConditionDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox ctrl = (ComboBox)sender;

            if (ctrl.SelectedValue != null)
            {
                //check whether the control's datasource is being changed
                if (ctrl.SelectedValue.GetType() != typeof(DataRowView))
                {
                    txtCreatePromoConditionDiscountValue.Text = "0.00";

                    //if Condition's discount type is 'free-item', disable Condition's discount-value
                    //if (Convert.ToInt32(ctrl.SelectedValue) == (int)Promotion.PromotionCategoryType.Quantity)
                    if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.Line)
                    {
                        txtCreatePromoConditionDiscountValue.Enabled = true;
                    }
                    else if (Convert.ToInt32(cmbCreatePromoCategory.SelectedValue) == (int)Promotion.PromotionCategoryType.Quantity)
                    {
                        if (Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) == (int)Promotion.ConditionType.Buy)
                        {
                            txtCreatePromoConditionDiscountValue.Enabled = false;
                        }
                        else if (Convert.ToInt32(cmbCreatePromoConditionType.SelectedValue) == (int)Promotion.ConditionType.Get)
                        {
                            txtCreatePromoConditionDiscountValue.Enabled = true;
                        }

                        if (Convert.ToInt32(cmbCreatePromoConditionDiscountType.SelectedValue) != Common.INT_DBNULL)
                        {
                            switch (Convert.ToInt32(cmbCreatePromoConditionDiscountType.SelectedValue))
                            {
                                case -1:
                                case 4:
                                    {
                                        txtCreatePromoConditionDiscountValue.Enabled = false;
                                    }
                                    break;

                                default:
                                    {
                                        txtCreatePromoConditionDiscountValue.Enabled = true;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            txtCreatePromoConditionDiscountValue.Enabled = false;
                        }
                    }
                    else
                    {
                        txtCreatePromoConditionDiscountValue.Enabled = false;
                    }
                }
            }
        }

        #endregion

        #region Enum
        private enum ConditionOperationState
        {
            EDIT,
            ADD,
            CLEAR,
        }

        private enum LocationOperationState
        {
            EDIT,
            ADD,
            CLEAR,
        }

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

        private void btnBrowse_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click_1(object sender, EventArgs e)
        {
            try
            {
                var FD = new System.Windows.Forms.OpenFileDialog();
                if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string[] allowExt = { ".BMP", ".PNG", ".JPEG", ".JPG", ".GIF" };
                    if (!allowExt.Contains(Path.GetExtension(FD.SafeFileName).ToUpper()))
                    {
                        MessageBox.Show(Common.GetMessage("VAL0136"), "Product Image", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    this.ImgWebPromotion.Load(FD.FileName);
                    Filepath = FD.FileName;
                    Environment.CurrentDirectory = EnvironmentPath;
                }

            }
            catch (Exception)
            {
                MessageBox.Show(Common.GetMessage("VAL0137"), "Product Image", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


        }


    }
}
