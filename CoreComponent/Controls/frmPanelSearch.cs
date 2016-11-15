using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.BusinessObjects;
using System.Reflection;
using CoreComponent.Core.BusinessObjects;
namespace CoreComponent.Controls
{
    public partial class frmPanelSearch : Form
    {
        ucPanel m_panel; List<UserSearchField> SearchList;
        public frmPanelSearch(ucPanel panel)
        {
            try
            {
                InitializeComponent();
                m_panel = panel;
                CreateControls();
                
                SetDefaultValue();
            }
            catch(Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        private void SetDefaultValue()
        {
            foreach (UserSearchField field in SearchList)
            {
                if (field.Field.Controltype == PanelCommon.ControlType.ComboBox)
                {
                    if (pnlFields.Controls.Find(field.Field.SearchColumnName,true) != null)
                    {
                        
                        ((ComboBox)pnlFields.Controls.Find(field.Field.SearchColumnName,true)[0]).SelectedValue=Convert.ToInt32(field.Value == string.Empty ? Convert.ToString(field.Field.DefaultValue) : Convert.ToString(field.Value));
                    }
                }
            }
        }
        private void CreateControls()
        {
            try
            {               
                UserSearchField search = new UserSearchField();
                search.Field = new SearchField();
                search.Field.PanelID = m_panel.PanelID;
                search.UserPanelID = m_panel.UserPanelID;
                SearchList = search.Search();
                if (SearchList != null)
                {
                    int x = 5; int y = 3;
                    btnSearch.Visible = true;
                    foreach (UserSearchField field in SearchList)
                    {
                        Label lbl = new Label();
                        lbl.Text = field.Field.SearchColumnName;
                        lbl.Name = "lbl" + field.Field.FieldID+" :";
                        lbl.Location = new Point(x,y);
                        lbl.Width = 100;
                        x = x + 110;                      
                        pnlFields.Controls.Add(lbl);
                        switch (field.Field.Controltype)
                        {
                            case PanelCommon.ControlType.TextBox: 
                                TextBox txtBox = new TextBox();
                                txtBox.Name = field.Field.SearchColumnName;
                                txtBox.Location = new Point(x, y);
                                txtBox.Width = 120;
                                //txtBox.MaxLength = m_searchParams[i].MaxLength;
                                //txtBox.Width = m_searchParams[i].ControlWidth;
                                txtBox.Text = field.Value == string.Empty ? Convert.ToString(field.Field.DefaultValue) : Convert.ToString(field.Value);
                                pnlFields.Controls.Add(txtBox);
                               break;
                            case PanelCommon.ControlType.DateTime:
                                DateTimePicker dt = new DateTimePicker();
                                dt.Format = DateTimePickerFormat.Custom;
                                dt.CustomFormat = "dd-MM-yy";
                                dt.ShowCheckBox = true;
                                dt.Checked = false;
                                dt.Name = field.Field.SearchColumnName;
                                dt.Width = 100;
                                dt.Text = field.Value == string.Empty ? Convert.ToString(field.Field.DefaultValue) : Convert.ToString(field.Value);
                                pnlFields.Controls.Add(dt);
                                break;
                            case PanelCommon.ControlType.ComboBox:
                                ComboBox combo = new ComboBox();
                                combo.Name = field.Field.SearchColumnName;
                                combo.Location = new Point(x, y);
                                combo.DropDownStyle = ComboBoxStyle.DropDownList;
                                ComboMaster cmb = new ComboMaster();
                                cmb.ComboID = field.Field.ComboID;                                
                                try
                                {
                                    List<ComboMaster> ListComboDetail = cmb.Search();
                                    string _error = string.Empty;                                    
                                    combo.DataSource = PanelCommon.InvokeMethod(ListComboDetail[0].Method, null, ref _error);
                                    combo.DisplayMember = ListComboDetail[0].DisplayMember.ToString();
                                    combo.ValueMember = ListComboDetail[0].ValueMember.ToString();
                                }
                                catch
                                {

                                }
                                pnlFields.Controls.Add(combo);                                                              
                               
                                break;                                                            
                            case PanelCommon.ControlType.CheckBox:
                                CheckBox chkBox = new CheckBox();
                                chkBox.Name = field.Field.SearchColumnName;
                                chkBox.ThreeState = true;
                                switch (Convert.ToInt32(field.Value == string.Empty ? Convert.ToString(field.Field.DefaultValue) : Convert.ToString(field.Value)))
                                {
                                    case 0:
                                        {
                                            chkBox.CheckState = CheckState.Unchecked;
                                            break;
                                        }
                                    case 1:
                                        {
                                            chkBox.CheckState = CheckState.Checked;
                                            break;
                                        }
                                    case 2:
                                        {
                                            chkBox.CheckState = CheckState.Indeterminate;
                                            break;
                                        }
                                    default:
                                        {
                                            chkBox.CheckState = CheckState.Indeterminate;
                                            break;
                                        }

                                }
                                pnlFields.Controls.Add(chkBox);                                
                                break;                                
                        }
                        y = y + 25;
                        x = 5;
                    }
                }
            }
            catch
            {

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
              if (SearchList != null)
              {
                    foreach (UserSearchField field in SearchList)
                    {
                        if(field.Field!=null)
                        {
                            Control[] ctrl=this.Controls.Find(field.Field.SearchColumnName,true);
                            if (ctrl != null)
                            {
                                switch (field.Field.Controltype)
                                {
                                    case PanelCommon.ControlType.CheckBox:
                                        {
                                            field.Value=((CheckBox)ctrl[0]).Checked.ToString();                                            
                                            break;
                                        }
                                    case PanelCommon.ControlType.ComboBox:
                                        {
                                            field.Value = ((ComboBox)ctrl[0]).SelectedValue.ToString();
                                            break;

                                        }
                                    case PanelCommon.ControlType.DateTime:
                                        {
                                            field.Value = ((DateTimePicker)ctrl[0]).Value.ToShortDateString();
                                            break;
                                        }
                                    case PanelCommon.ControlType.TextBox:
                                        {
                                            field.Value = ((TextBox)ctrl[0]).Text;
                                            break;
                                        }
                                }
                            }
                           // field.Value=ctrl[0].Text;
                        }
                    }
              }
              string errorMessage = string.Empty;

              bool issaved=UserSearchField.SaveList(SearchList,ref errorMessage);
              m_panel.initailizeControls();         
        }
    }
}
