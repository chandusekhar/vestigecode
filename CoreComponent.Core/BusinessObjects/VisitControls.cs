using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CoreComponent.Core.BusinessObjects
{
    public class VisitControls
    {
        public void ResetAllControlsInPanel(System.Windows.Forms.Panel currentPanel)
        {
            System.Windows.Forms.Control ctrl = currentPanel.GetNextControl(currentPanel, true);

            while (ctrl != null)
            {
                System.Type ctrlType = ctrl.GetType();
                if (ctrlType == typeof(System.Windows.Forms.TextBox))
                {
                    System.Windows.Forms.TextBox currentTextBox = ctrl as System.Windows.Forms.TextBox;
                    currentTextBox.ResetText();
                }
                else if (ctrlType == typeof(System.Windows.Forms.ComboBox))
                {
                    System.Windows.Forms.ComboBox currentComboBox = ctrl as System.Windows.Forms.ComboBox;
                    if (currentComboBox.Items.Count > 0) currentComboBox.SelectedIndex = 0;
                }
                else if (ctrlType == typeof(System.Windows.Forms.ListBox))
                {
                    System.Windows.Forms.ListBox currentListBox = ctrl as System.Windows.Forms.ListBox;
                    if (currentListBox.Items.Count > 0) currentListBox.SelectedIndex = 0;
                }
                else if (ctrlType == typeof(System.Windows.Forms.CheckBox))
                {
                    System.Windows.Forms.CheckBox currentCheckBox = ctrl as System.Windows.Forms.CheckBox;
                    currentCheckBox.Checked = false;
                }
                ctrl = currentPanel.GetNextControl(ctrl, true);
            }
        }

        public void ResetAllControlsInPanel(ErrorProvider ep, System.Windows.Forms.Panel currentPanel)
        {
            System.Windows.Forms.Control ctrl = currentPanel.GetNextControl(currentPanel, true);

            while (ctrl != null)
            {
                System.Type ctrlType = ctrl.GetType();
                if (ctrlType == typeof(System.Windows.Forms.TextBox))
                {
                    System.Windows.Forms.TextBox currentTextBox = ctrl as System.Windows.Forms.TextBox;
                    currentTextBox.ResetText();
                    ep.SetError(currentTextBox, string.Empty);
                }
                else if (ctrlType == typeof(System.Windows.Forms.ComboBox))
                {
                    System.Windows.Forms.ComboBox currentComboBox = ctrl as System.Windows.Forms.ComboBox;
                    if (currentComboBox.Items.Count > 0) currentComboBox.SelectedIndex = 0;
                    ep.SetError(currentComboBox, string.Empty);
                }
                else if (ctrlType == typeof(System.Windows.Forms.ListBox))
                {
                    System.Windows.Forms.ListBox currentListBox = ctrl as System.Windows.Forms.ListBox;
                    if (currentListBox.Items.Count > 0) currentListBox.SelectedIndex = 0;
                    ep.SetError(currentListBox, string.Empty);
                }
                else if (ctrlType == typeof(System.Windows.Forms.CheckBox))
                {
                    System.Windows.Forms.CheckBox currentCheckBox = ctrl as System.Windows.Forms.CheckBox;
                    currentCheckBox.Checked = false;
                    ep.SetError(currentCheckBox, string.Empty);
                }
                else if (ctrlType == typeof(System.Windows.Forms.CheckedListBox))
                {
                    System.Windows.Forms.CheckedListBox currentCheckBox = ctrl as System.Windows.Forms.CheckedListBox;
                    ep.SetError(currentCheckBox, string.Empty);
                }
                ctrl = currentPanel.GetNextControl(ctrl, true);
            }
        }

        public void ResetAllControlsInPanel(ErrorProvider ep, System.Windows.Forms.GroupBox currentPanel)
        {
            System.Windows.Forms.Control ctrl = currentPanel.GetNextControl(currentPanel, true);

            while (ctrl != null)
            {
                System.Type ctrlType = ctrl.GetType();
                if (ctrlType == typeof(System.Windows.Forms.TextBox))
                {
                    System.Windows.Forms.TextBox currentTextBox = ctrl as System.Windows.Forms.TextBox;
                    currentTextBox.ResetText();
                    ep.SetError(currentTextBox, string.Empty);
                }
                else if (ctrlType == typeof(System.Windows.Forms.ComboBox))
                {
                    System.Windows.Forms.ComboBox currentComboBox = ctrl as System.Windows.Forms.ComboBox;
                    if (currentComboBox.Items.Count > 0) currentComboBox.SelectedIndex = 0;
                    ep.SetError(currentComboBox, string.Empty);
                }
                else if (ctrlType == typeof(System.Windows.Forms.ListBox))
                {
                    System.Windows.Forms.ListBox currentListBox = ctrl as System.Windows.Forms.ListBox;
                    if (currentListBox.Items.Count > 0) currentListBox.SelectedIndex = 0;
                    ep.SetError(currentListBox, string.Empty);
                }
                else if (ctrlType == typeof(System.Windows.Forms.CheckBox))
                {
                    System.Windows.Forms.CheckBox currentCheckBox = ctrl as System.Windows.Forms.CheckBox;
                    currentCheckBox.Checked = false;
                    ep.SetError(currentCheckBox, string.Empty);
                }
                else if (ctrlType == typeof(System.Windows.Forms.CheckedListBox))
                {
                    System.Windows.Forms.CheckedListBox currentCheckBox = ctrl as System.Windows.Forms.CheckedListBox;
                    ep.SetError(currentCheckBox, string.Empty);
                }
                ctrl = currentPanel.GetNextControl(ctrl, true);
            }
        }


        public void ResetCurrentGrid(System.Windows.Forms.Panel headerPanel, System.Windows.Forms.DataGridView dgvToReset)
        {
            ResetAllControlsInPanel(headerPanel);
            dgvToReset.DataSource = null;
        }

        public void ResetCurrentGrid(System.Windows.Forms.Panel headerPanel, System.Windows.Forms.DataGridView dgvToReset, System.Windows.Forms.ErrorProvider currentErrorProvider)
        {
            ResetAllControlsInPanel(currentErrorProvider, headerPanel);
            dgvToReset.DataSource = null;
        }
    }
}
