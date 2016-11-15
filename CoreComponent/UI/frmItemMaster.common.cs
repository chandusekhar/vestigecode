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
using CoreComponent.MasterData.BusinessObjects;
using CoreComponent.Hierarchies.BusinessObjects;

namespace CoreComponent.MasterData.UI
{
    public partial class frmItemMaster
    {
        public bool m_suspendEventHandler = false;

        enum ListOfDataSources
        {
            ListOfItemDetails = 0,
            ListOfReturnedLocations = 1,
            ListOfVendors = 2,
            ListOfVendorLoc = 3
        }

        void BindDataGridView(DataGridView dgv, ListOfDataSources lds)
        {
            try
            {
                switch (lds)
                {
                    case ListOfDataSources.ListOfItemDetails:
                        dgv.DataSource = m_ItemsList;
                        dgv.Select();
                        break;
                    case ListOfDataSources.ListOfReturnedLocations:
                        break;
                    case ListOfDataSources.ListOfVendors:
                        dgv.DataSource = m_selectedItemVendors;
                        dgv.Select();
                        break;
                    case ListOfDataSources.ListOfVendorLoc:
                        dgv.DataSource = m_selectedVendorLoc;
                        dgv.Select();
                        break;
                    default: dgv.DataSource = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FormatGridView(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
        }

        void NullifyDataGridView(DataGridView dgv)
        {
            dgv.DataSource = null;
        }

        Point PlacePopUp(Point ctrlLocation)
        {
            Point pointTree = new Point();
            pointTree = this.PointToScreen(ctrlLocation);
            pointTree.Y = pointTree.Y + Common.TREE_COMP_Y;
            pointTree.X = pointTree.X + Common.TREE_COMP_X;

            return pointTree;
        }

        void EnableDisableOnCheck(CheckBox chk, Control ctrlToDisplay)
        {
            try
            {
                if (chk.CheckState == CheckState.Checked)
                {
                    ctrlToDisplay.Enabled = true;
                   chkRegistrationPurpose.CheckState = CheckState.Unchecked;
                    txtMinKitValue.Enabled = false;
                    txtMinKitValue.Text = string.Empty;
                }
                else if (chk.CheckState == CheckState.Indeterminate || chk.CheckState == CheckState.Unchecked)
                {
                    ctrlToDisplay.Enabled = false;
                  
                    txtDP.Enabled = true;
                    txtMRP.Enabled = true;
                    chkKit.Enabled = true;
                    txtLandedPrice.Enabled = true;
                    
                   
                    if (chkKit.CheckState == CheckState.Unchecked)
                    {
                       // chkRegistrationPurpose.CheckState = CheckState.Checked;
                        txtMinKitValue.Enabled = true;
                    }
                    else if (chkKit.CheckState == CheckState.Indeterminate)
                    {
                        //chkRegistrationPurpose.CheckState = CheckState.Indeterminate;
                        txtMinKitValue.Enabled = false;
                        txtMinKitValue.Text = string.Empty;
                    }
                }


                if ((chkKit.CheckState == CheckState.Unchecked && chkRegistrationPurpose.CheckState == CheckState.Checked) ||

                    (chkKit.CheckState == CheckState.Checked && chkRegistrationPurpose.CheckState == CheckState.Unchecked))
                {
                    txtMinKitValue.Enabled = true;
                }
                else
                {
                    txtMinKitValue.Enabled = false;
                    txtMinKitValue.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void TextBoxValidations(TextBox txt, Label lbl, ErrorProvider ep)
        {
            try
            {
                if (Validators.CheckForEmptyString(txt.Text.Length))
                    Validators.SetErrorMessage(ep, txt, "VAL0001", lbl.Text);
                else
                    Validators.SetErrorMessage(ep, txt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void AmountValidations(TextBox txt, Label lbl, ErrorProvider ep,bool isGreaterThanZero)
        {
            try
            {
                if (Validators.CheckForEmptyString(txt.Text.Length))
                    Validators.SetErrorMessage(ep, txt, "VAL0001", lbl.Text);
                else if(!Validators.IsValidAmount(txt.Text))
                    Validators.SetErrorMessage(ep, txt, "VAL0009", lbl.Text);
                else if (isGreaterThanZero)
                {
                   bool isGreater=Validators.IsGreaterThanZero(txt.Text.Trim());
                   if (!isGreater)
                   {
                       Validators.SetErrorMessage(ep, txt, "VAL0033", lbl.Text);
                   }
                   else
                   {
                       Validators.SetErrorMessage(ep, txt);
                   }
                }
                else
                    Validators.SetErrorMessage(ep, txt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void QuantityValidations(TextBox txt, Label lbl, ErrorProvider ep)
        {
            try
            {
                if (Validators.CheckForEmptyString(txt.Text.Length))
                    Validators.SetErrorMessage(ep, txt, "VAL0001", lbl.Text);
                else if (Validators.IsValidQuantity(txt.Text))
                    Validators.SetErrorMessage(ep, txt, "VAL0009", lbl.Text);
                else
                    Validators.SetErrorMessage(ep, txt);
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
        void CheckBoxValidations(CheckBox chk, Label lbl, ErrorProvider ep)
        {
            try
            {
                if(chk.CheckState==CheckState.Indeterminate)
                    Validators.SetErrorMessage(ep, chk, "VAL0002", lbl.Text);
                else
                    Validators.SetErrorMessage(ep, chk);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void GreaterThanZeroValidations(TextBox txt, Label lbl, ErrorProvider ep)
        {
            try
            {
                if (Validators.CheckForEmptyString(txt.Text.Length))
                    Validators.SetErrorMessage(ep, txt, "VAL0033", lbl.Text);
                else if (Convert.ToInt32(txt.Text) <= 0)
                    Validators.SetErrorMessage(ep, txt, "VAL0033", lbl.Text);
                else
                    Validators.SetErrorMessage(ep, txt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void IsDecimal(TextBox txt, Label lbl, ErrorProvider ep)
        {
            try
            {
                if (!Validators.IsDecimal(txt.Text))
                    Validators.SetErrorMessage(ep, txt, "VAL0001", lbl.Text);
                else
                    Validators.SetErrorMessage(ep, txt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void IsInteger(TextBox txt, Label lbl, ErrorProvider ep)
        {
            try
            {
                if (!Validators.IsInt32(txt.Text))
                    Validators.SetErrorMessage(ep, txt, "VAL0001", lbl.Text);
                else
                    Validators.SetErrorMessage(ep, txt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void IsInteger(TextBox txt, Label lbl, ErrorProvider ep,bool CanBeBlank)
        {
            try
            {
                if (Validators.CheckForEmptyString(txt.Text.Trim().Length))
                {
                    if (!CanBeBlank)
                        Validators.SetErrorMessage(ep, txt, "VAL0001", lbl.Text);
                    else
                    {
                        Validators.SetErrorMessage(ep, txt);
                        return;
                    }
                }
                else if (!Validators.IsInt32(txt.Text))
                    Validators.SetErrorMessage(ep, txt, "VAL0009", lbl.Text);
                else
                    Validators.SetErrorMessage(ep, txt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ValidateItemCode(TextBox txt, Label lbl,  ErrorProvider ep)
        {

        }

        private void ValidatePointValue(TextBox txt, Label lbl, ErrorProvider ep)
        {
            IsDecimal(txt, lbl, ep);
            if (ep.GetError(txt).Equals(string.Empty))
            {
                if (!Validators.IsGreaterThanZero(txt.Text.Trim()))
                {
                    ep.SetError(txt, Common.GetMessage("VAL0033", lbl.Text.Substring(0, lbl.Text.Length - 2)));
                }
            }
            else
            {
                Validators.SetErrorMessage(ep, txt, "VAL0081", lbl.Text);
            }
        }

        private void ValidateDistributorPrice(TextBox txtMRP, TextBox txtDP, Label lbl, ErrorProvider ep)
        {
            decimal mrpPrice = Decimal.TryParse(txtMRP.Text, out mrpPrice) ? mrpPrice : Common.INT_DBNULL;
            decimal distributorPrice = Decimal.TryParse(txtDP.Text, out distributorPrice) ? distributorPrice : Common.INT_DBNULL;

            if (distributorPrice != Common.INT_DBNULL)
            {
                if (mrpPrice != Common.INT_DBNULL)
                {
                    if (distributorPrice > mrpPrice)
                    {
                        Validators.SetErrorMessage(ep, txtDP, "VAL0079", "Distributor Price");
                    }
                }
            }
            else
            {
                Validators.SetErrorMessage(ep, txtDP, "VAL0001", "Distributor Price");
            }
        }
    }
}
