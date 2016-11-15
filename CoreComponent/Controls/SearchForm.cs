using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.Xml;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using System.Collections.Specialized;


namespace CoreComponent.Controls
{
    public enum SearchTypes
    {
        Item = 1,
        Vendor = 2,
        PO = 3,
        GRN = 4,
        ItemBatch = 5,
        ItemBatchMaster = 6,
        ItemLocationVendor=7,
        TOI=8,
        TO=9,
        ItemLocationComposite=10,
        POGRN=11,
        ItemReturnToVendor=12,
        ItemMaster = 13,
        DistributorSearch=14,
        LocationSearch = 15,
        //added by Kaushik
        Invoice = 16,
        EOI = 17,
        EO = 18
    }

    public class SearchForm
    {
        private const string SEARCHXML_PATH = "App_Data/SearchControl.xml";
        //private const int PARAMSINONEROW = 3;

        public SearchForm(SearchTypes sType, DataGridView dgv, Panel searchPanel, NameValueCollection nvCollection, Label lblPageTitle)
        {
            this.m_searchPanel = searchPanel;
            this.m_searchParams = new List<SearchParam>();
            this.m_searchValidations = new List<SearchValidation>();
            DataSet ds = LoadXML(SEARCHXML_PATH);
            LoadDataSetToBL(ds, (int)sType);
            foreach (string key in nvCollection.AllKeys)
            {
                m_searchParams.Find(delegate(SearchParam sp) { return sp.PropertyName == key; }).DefaultValue = nvCollection[key];
            }
            InitializeControlObjects(sType, dgv, searchPanel, ds, lblPageTitle);
        }

        public SearchForm(SearchTypes sType, DataGridView dgv, Panel searchPanel, Label lblPageTitle)
        {
            this.m_searchPanel = searchPanel;
            this.m_searchParams = new List<SearchParam>();
            this.m_searchValidations = new List<SearchValidation>();
            DataSet ds = LoadXML(SEARCHXML_PATH);
            LoadDataSetToBL(ds, (int)sType);
            InitializeControlObjects(sType, dgv, searchPanel, ds, lblPageTitle);
        }

        private void InitializeControlObjects(SearchTypes sType, DataGridView dgv, Panel searchPanel, DataSet ds, Label lblPageTitle)
        {
            LoadBLToControls(ref searchPanel);
            CreateSearchGrid(dgv, ds, (int)sType);
            m_searchClass = Assembly.Load(this.AssemblyName).GetType(this.ClassName);
            m_searchObject = Activator.CreateInstance(m_searchClass);
            lblPageTitle.Text = this.PageTitle;
        }

        private Type m_searchClass;
        public bool ItemCodeMapping
        {
            get;
            set;
        }
        public string FromItemCode
        {
            get;
            set;
        }
        public Type SearchClass
        {
            get { return m_searchClass; }
            set { m_searchClass = value; }
        }
        private object m_searchObject;

        public object SearchObject
        {
            get { return m_searchObject; }
            set { m_searchObject = value; }
        }
        private Panel m_searchPanel;

        public Panel SearchPanel
        {
            get { return m_searchPanel; }
            set { m_searchPanel = value; }
        }
        private string m_assemblyName;

        public string AssemblyName
        {
            get { return m_assemblyName; }
            set { m_assemblyName = value; }
        }
        private string m_pageTitle;

        public string PageTitle
        {
            get { return m_pageTitle; }
            set { m_pageTitle = value; }
        }
        private string m_className;

        public string ClassName
        {
            get { return m_className; }
            set { m_className = value; }
        }
        private string m_searchMethod;

        public string SearchMethod
        {
            get { return m_searchMethod; }
            set { m_searchMethod = value; }
        }
        private int m_paramsInOneRow;

        public int ParamsInOneRow
        {
            get { return m_paramsInOneRow; }
            set { m_paramsInOneRow = value; }
        }
        private string m_id;

        public string Id
        {
            get { return m_id; }
            set { m_id = value; }
        }
        private List<SearchValidation> m_searchValidations;

        public List<SearchValidation> SearchValidations
        {
            get { return m_searchValidations; }
            set { m_searchValidations = value; }
        }
        private List<SearchParam> m_searchParams;

        public List<SearchParam> SearchParams
        {
            get { return m_searchParams; }
            set { m_searchParams = value; }
        }

        /// <summary>
        /// Loads Data from the Dataset to the specific business layer objects
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="searchId"></param>
        private void LoadDataSetToBL(DataSet ds, int searchId)
        {
            DataRow[] searchRows = ds.Tables[0].Select("ID = '" + searchId + "'");
            //DataRow[] searchRows = ds.Tables[0].Select("ID = " + searchId);
            this.AssemblyName = searchRows[0]["AssemblyName"].ToString();
            this.ClassName = searchRows[0]["ClassName"].ToString();
            this.SearchMethod = searchRows[0]["SearchMethod"].ToString();
            this.ParamsInOneRow = Convert.ToInt32(searchRows[0]["ParamsInOneRow"]);
            this.PageTitle = searchRows[0]["PageTitle"].ToString();

            DataRow[] paramRows = ds.Tables[1].Select("Search_Id = " + searchRows[0][0]);//ds.Tables[0].Rows[0][0]);
            //Dim report As New ReportDatum()
            if (ds.Tables.Count > 4)
            {
                DataRow[] validationRows = ds.Tables[4].Select("Search_Id = " + ds.Tables[0].Rows[0][0]);
                foreach (DataRow dr in validationRows)
                {
                    SearchValidation validator = new SearchValidation();
                    validator.Parameter1 = dr["Parameter1"].ToString();
                    validator.Parameter2 = dr["Parameter2"].ToString();
                    validator.Comparison = dr["Comparison"].ToString();
                    m_searchValidations.Add(validator);
                }
            }

            //Add report params to the report object
            foreach (DataRow row in paramRows)
            {
                SearchParam param = new SearchParam();
                param.Name = row["Name"].ToString();
                param.DataType = row["DataType"].ToString();
                param.Label = row["Label"].ToString();
                param.Source = row["Source"].ToString();
                //param.LovId = row("LovId")
                param.ControlWidth = Convert.ToInt32(row["ControlWidth"]);
                param.ControlRow = Convert.ToInt32(row["ControlRow"]);
                param.ControlColumn = Convert.ToInt32(row["ControlCol"]);
                param.DefaultValue = row["DefaultValue"].ToString();
                param.PropertyName = row["PropertyName"].ToString();
                param.ParameterType = row["ParameterType"].ToString();
                param.IsReadOnly = row["IsReadOnly"] == null ? false : Convert.ToBoolean(row["IsReadOnly"]);
                param.IsVisible = row["IsVisible"] == null ? false : Convert.ToBoolean(row["IsVisible"]);
                param.IsEnabled = row["IsEnabled"] == null ? false : Convert.ToBoolean(row["IsEnabled"]);
                if (param.DataType.ToUpper() == "LIST")
                {
                    param.AssemblyName = row["AssemblyName"].ToString();
                    param.ClassName = row["ClassName"].ToString();
                    param.MethodName = row["MethodName"].ToString();
                    param.ParameterCode = row["ParameterCode"].ToString();
                    param.Key1 = Convert.ToInt32(row["Key1"]);
                    param.Key2 = Convert.ToInt32(row["Key2"]);
                    param.Key3 = Convert.ToInt32(row["Key3"]);
                }
                if (row["MaxLength"].ToString() == String.Empty)
                {
                    param.MaxLength = -1;
                }
                else
                {
                    param.MaxLength = Convert.ToInt32(row["MaxLength"]);
                }
                param.IsMandatory = Convert.ToBoolean(row["IsMandatory"]);
                m_searchParams.Add(param);
            }
        }

        /// <summary>
        /// Creates Search Parameter Controls from the Business Objects loaded from XML
        /// </summary>
        /// <param name="searchPanel">Panel object in which the Search parameter controls are to be placed</param>
        private void LoadBLToControls( ref Panel searchPanel)
        {
            if (m_searchParams.Count > 0)
            {
                int noofVisibleRows=0;
                var query = from m in m_searchParams where m.IsVisible == true select m;
                if(query!=null && query.ToList().Count>0)
                    noofVisibleRows = (int)Math.Ceiling((double)((int)query.ToList().Count / this.ParamsInOneRow)) + 1; ;
                
                int numberOfRows = (int)Math.Ceiling((double)(m_searchParams.Count / this.ParamsInOneRow)) + 1;                
                TableLayoutPanel searchTable = new TableLayoutPanel();
                //'reportTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
                searchPanel.Height = 34 + (30 * (noofVisibleRows));
                searchTable.Width = searchPanel.Width;
                searchTable.Height = searchPanel.Height - 34;
                searchTable.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
                searchTable.Padding = new System.Windows.Forms.Padding(3, 10, 3, 3);
                searchTable.ColumnCount = this.ParamsInOneRow * 2;
                searchTable.RowCount = numberOfRows;
                searchTable.BackColor = System.Drawing.Color.Transparent;
                searchTable.AutoSize = false;

                searchTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;

                searchTable.Dock = DockStyle.Top;

                for (int i = 0; i <= m_searchParams.Count - 1; i++)
                {
                    //Create Label
                    Label lbl = new Label();
                    lbl.AutoSize = true;
                    lbl.Text = m_searchParams[i].Label;
                    lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                    lbl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl.Visible = m_searchParams[i].IsVisible;
                    lbl.Anchor = AnchorStyles.Left;
                    searchTable.Controls.Add(lbl, m_searchParams[i].ControlColumn - 1, m_searchParams[i].ControlRow);

                    //lbl.Dock = DockStyle.Left;
                    //lbl.Dock = DockStyle.Left|DockStyle.Top;
                    //lbl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    //lbl.Dock = DockStyle.Fill;

                    //Create Input control
                    switch (m_searchParams[i].DataType.ToUpper())
                    {
                        case "VARCHAR":
                            TextBox txtBox = new TextBox();
                            txtBox.Name = m_searchParams[i].Name;
                            txtBox.MaxLength = m_searchParams[i].MaxLength;
                            txtBox.Width = m_searchParams[i].ControlWidth;
                            txtBox.Text = m_searchParams[i].DefaultValue;
                            txtBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            txtBox.ReadOnly = m_searchParams[i].IsReadOnly;
                            txtBox.Visible = m_searchParams[i].IsVisible;
                            txtBox.Enabled = m_searchParams[i].IsEnabled;
                            txtBox.Text = m_searchParams[i].DefaultValue == string.Empty ? string.Empty : m_searchParams[i].DefaultValue;
                            searchTable.Controls.Add(txtBox, m_searchParams[i].ControlColumn, m_searchParams[i].ControlRow);
                            break;
                        case "INTEGER":
                            TextBox txtBox1 = new TextBox();
                            txtBox1.Name = m_searchParams[i].Name;
                            txtBox1.MaxLength = m_searchParams[i].MaxLength;
                            txtBox1.Width = m_searchParams[i].ControlWidth;
                            txtBox1.Text = m_searchParams[i].DefaultValue;
                            txtBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            txtBox1.ReadOnly = m_searchParams[i].IsReadOnly;
                            txtBox1.Visible = m_searchParams[i].IsVisible;
                            txtBox1.Enabled = m_searchParams[i].IsEnabled;
                            txtBox1.Text = m_searchParams[i].DefaultValue == string.Empty ? string.Empty : m_searchParams[i].DefaultValue;
                            searchTable.Controls.Add(txtBox1, m_searchParams[i].ControlColumn, m_searchParams[i].ControlRow);
                            break;
                        case "DECIMAL":
                            TextBox txtBox2 = new TextBox();
                            txtBox2.Name = m_searchParams[i].Name;
                            txtBox2.MaxLength = m_searchParams[i].MaxLength;
                            txtBox2.Text = m_searchParams[i].DefaultValue;
                            txtBox2.Width = m_searchParams[i].ControlWidth;
                            txtBox2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            txtBox2.ReadOnly = m_searchParams[i].IsReadOnly;
                            txtBox2.Visible = m_searchParams[i].IsVisible;
                            txtBox2.Enabled = m_searchParams[i].IsEnabled;
                            txtBox2.Text = m_searchParams[i].DefaultValue == string.Empty ? string.Empty : m_searchParams[i].DefaultValue;
                            searchTable.Controls.Add(txtBox2, m_searchParams[i].ControlColumn, m_searchParams[i].ControlRow);
                            break;
                        case "DATETIME":
                            DateTimePicker dt = new DateTimePicker();
                            dt.Format = DateTimePickerFormat.Custom;
                            dt.CustomFormat = Common.DTP_DATE_FORMAT;
                            dt.ShowCheckBox = true;
                            dt.Checked = false;
                            dt.Name = m_searchParams[i].Name;
                            dt.Width = m_searchParams[i].ControlWidth;
                            dt.Visible = m_searchParams[i].IsVisible;
                            dt.Enabled = m_searchParams[i].IsEnabled;
                            dt.Text = m_searchParams[i].DefaultValue == string.Empty ? string.Empty : m_searchParams[i].DefaultValue;
                            if ((m_searchParams[i].DefaultValue.ToUpper()) == "SYSDATE")
                            {
                                dt.Value = DateTime.Now;
                                dt.Checked = true;
                            }
                            else if (m_searchParams[i].DefaultValue != string.Empty)
                            {
                                dt.Value = Convert.ToDateTime((m_searchParams[i].DefaultValue.ToUpper()));
                                dt.Checked = true;
                            }
                            searchTable.Controls.Add(dt, m_searchParams[i].ControlColumn, m_searchParams[i].ControlRow);
                            break;
                        case "LIST":
                            ComboBox combo = new ComboBox();
                            combo.Name = m_searchParams[i].Name;
                            Type classType = Assembly.Load(m_searchParams[i].AssemblyName).GetType(m_searchParams[i].ClassName);
                            ParameterFilter pf = new ParameterFilter();
                            pf.Code = m_searchParams[i].ParameterCode;
                            pf.Key01 = m_searchParams[i].Key1;
                            pf.Key02 = m_searchParams[i].Key2;
                            pf.Key03 = m_searchParams[i].Key3;
                            string ParameterType = m_searchParams[i].ParameterType;
                            DataTable dTable = (DataTable)classType.GetMethod(m_searchParams[i].MethodName).Invoke(null, new object[] { ParameterType, pf });
                            //Dim dt As DataTable = Utility.GetParameterDetail(m_searchParams[i].ParamSource)
                            combo.DataSource = dTable;
                            combo.DisplayMember = dTable.Columns[1].ToString();
                            combo.ValueMember = dTable.Columns[0].ToString();
                            combo.DropDownStyle = ComboBoxStyle.DropDownList;
                            combo.Width = m_searchParams[i].ControlWidth;
                            combo.Enabled = m_searchParams[i].IsEnabled;
                            combo.Visible = m_searchParams[i].IsVisible;
                            if (m_searchParams[i].DefaultValue != string.Empty)
                            {
                                combo.SelectedValue = m_searchParams[i].DefaultValue;
                            }
                            searchTable.Controls.Add(combo, m_searchParams[i].ControlColumn, m_searchParams[i].ControlRow);
                            break;
                        case "CHECKBOX":
                            CheckBox chkBox = new CheckBox();
                            chkBox.Name = m_searchParams[i].Name;
                            //chkBox.Text = m_searchParams[i].DefaultValue;
                            chkBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            chkBox.Enabled = m_searchParams[i].IsEnabled;
                            chkBox.Visible = m_searchParams[i].IsVisible;
                            chkBox.ThreeState = true;
                            //chkBox.CheckState = CheckState.Indeterminate;
                            switch (Convert.ToInt32(m_searchParams[i].DefaultValue))
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
                            searchTable.Controls.Add(chkBox, m_searchParams[i].ControlColumn, m_searchParams[i].ControlRow);
                            break;
                        case "INTEGERNULL":
                            TextBox txtBox3 = new TextBox();
                            txtBox3.Name = m_searchParams[i].Name;
                            txtBox3.MaxLength = m_searchParams[i].MaxLength;
                            txtBox3.Width = m_searchParams[i].ControlWidth;
                            txtBox3.Text = m_searchParams[i].DefaultValue;
                            txtBox3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
                            txtBox3.ReadOnly = m_searchParams[i].IsReadOnly;
                            txtBox3.Visible = m_searchParams[i].IsVisible;
                            txtBox3.Enabled = m_searchParams[i].IsEnabled;
                            txtBox3.Text = m_searchParams[i].DefaultValue == string.Empty ? string.Empty : m_searchParams[i].DefaultValue;
                            searchTable.Controls.Add(txtBox3, m_searchParams[i].ControlColumn, m_searchParams[i].ControlRow);
                            break;
                    }
                }
                searchPanel.Controls.Add(searchTable);
                searchTable.BringToFront();
            }
        }

        /// <summary>
        /// Loads XML File to a dataset based on the SearchId specified
        /// </summary>
        /// <param name="xmlPath">Path of the xml file</param>
        /// <param name="searchId">Id of the type of search to be made</param>
        /// <returns></returns>
        public DataSet LoadXML(string xmlPath)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlPath);
            return ds;
        }

        /// <summary>
        /// Creates Columns for the specified datagridview from the xml at the specified path
        /// </summary>
        /// <param name="dgv">DataGridView in which the columns have to be created</param>
        /// <param name="xmlFilePath">Path of the xml file which contains column definitions</param>
        /// <returns></returns>
        public DataGridView CreateSearchGrid(DataGridView dgv, DataSet dsGrids, int searchId)
        {

           // DataRow[] searchRows = dsGrids.Tables[0].Select("ID = " + searchId );
            // Code Commented By Mukesh Not Working On Search ID =10)
            dgv.Parent.SuspendLayout();
            DataRow[] searchRows = dsGrids.Tables[0].Select("ID = '" + searchId + "'");
            DataRow[] grids = dsGrids.Tables[2].Select("Name = '" + dgv.Name + "' AND Search_Id = " + searchRows[0][0] + "");
            DataRow[] gridColumns = dsGrids.Tables[3].Select("GridView_Id = '" + grids[0][0] + "'");
            Common.CreateGridColums(dgv, grids, gridColumns);
            dgv.Parent.ResumeLayout(false);
            return dgv;
        }

        private bool ValidateMandatoryParams()
        {
            foreach (SearchParam param in this.m_searchParams)
            {
                if ((param.IsMandatory))
                {
                    Control ctrl = m_searchPanel.Controls.Find(param.Name, true)[0]; ;
                    if (param.DataType.ToUpper() == "DATETIME")
                    {
                        if (!((DateTimePicker)ctrl).Checked)
                        {
                            MessageBox.Show(Common.GetMessage("INF0037"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            ctrl.Focus();
                            return false;
                        }
                    }
                    else if (param.DataType.ToUpper() == "CHECKBOX")
                    {
                        if (!((CheckBox)ctrl).Checked)
                        {
                            MessageBox.Show(Common.GetMessage("INF0037"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            ctrl.Focus();
                            return false;
                        }
                    }
                    else if (param.DataType.ToUpper() == "INTEGERNULL")
                    {
                        int value = -1;
                        if(!Int32.TryParse((ctrl as TextBox).Text.Trim(),out value))                        
                        {                           
                            MessageBox.Show(Common.GetMessage("VAL0110"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            ctrl.Focus();
                            return false;
                        }
                    }
                    else if (param.DataType.ToUpper() == "VARCHAR")
                    {
                        //int value = -1;
                        if ((ctrl as TextBox).Text.Length == 0)
                        {
                            MessageBox.Show(Common.GetMessage("VAL0110"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            ctrl.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        if (ctrl.Text == string.Empty)
                        {
                            MessageBox.Show(Common.GetMessage("INF0037"), String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            ctrl.Focus();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool ValidateInput()
        {
            bool validationResult = true;

            // Validate Mandatory Params check
            validationResult = ValidateMandatoryParams();
            // Validate atleast one mandatory check
            // Validate report level validations
            if (validationResult == false)
            {
                return false;
            }

            if (this.m_searchValidations.Count > 0)
            {
                foreach (SearchValidation validation in this.m_searchValidations)
                {
                    if ((validation.Parameter1 == null || validation.Parameter2 == null || ((validation.Parameter1 == string.Empty || validation.Parameter2 == string.Empty) && validation.Comparison.ToUpper() != "ANY")))
                    {
                        MessageBox.Show(Common.GetMessage("INF0038"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        validationResult = false;
                        break; // TODO: might not be correct. Was : Exit For
                    }

                    //Check for atleast one mandatory input validation
                    if (validation.Comparison.ToUpper() == "ANY")
                    {
                        bool isValid = false;
                        foreach (SearchParam param in this.m_searchParams)
                        {
                            Control ctrl = m_searchPanel.Controls.Find(param.Name, true)[0];
                            if (param.DataType.ToUpper() == "DATETIME")
                            {
                                if (((DateTimePicker)ctrl).Checked)
                                {
                                    isValid = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                            else
                            {
                                if (!(ctrl.Text == string.Empty))
                                {
                                    isValid = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                        }
                        if (!isValid)
                        {
                            MessageBox.Show(Common.GetMessage("INF0037"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            m_searchPanel.Controls.Find(this.m_searchParams[0].Name, true)[0].Focus();
                            validationResult = false;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                        else
                        {
                            validationResult = true;
                        }
                    }
                    else
                    {

                        Control ctrl1 = new Control();
                        Control ctrl2 = new Control();
                        string dataType1 = string.Empty;
                        string dataType2 = string.Empty;
                        string parameter1 = string.Empty;
                        string parameter2 = string.Empty;

                        foreach (SearchParam param in this.m_searchParams)
                        {
                            if (param.Name == validation.Parameter1)
                            {
                                ctrl1 = m_searchPanel.Controls.Find(param.Name, true)[0];
                                parameter1 = param.Label;
                                dataType1 = param.DataType;
                            }
                            if (param.Name == validation.Parameter2)
                            {
                                ctrl2 = m_searchPanel.Controls.Find(param.Name, true)[0];
                                parameter2 = param.Label;
                                dataType2 = param.DataType;
                            }
                        }
                        string comparisonText = string.Empty;
                        if ((validation.Comparison.ToUpper() != "ANY") && (dataType1 != dataType2))
                        {
                            MessageBox.Show(Common.GetMessage("INF0038"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            validationResult = false;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                        // Check if both dates refer to same month and year
                        else if (validation.Comparison.ToUpper() == "DATESSAMEMONTH")
                        {
                            if (((((DateTimePicker)ctrl1).Value.Month == ((DateTimePicker)ctrl2).Value.Month) && (((DateTimePicker)ctrl1).Value.Year == ((DateTimePicker)ctrl2).Value.Year)))
                            {
                                validationResult = true;
                            }
                            else
                            {
                                MessageBox.Show(string.Format(Common.GetMessage("INF0040"), parameter1, parameter2), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                validationResult = false;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                        else
                        {
                            switch (validation.Comparison)
                            {
                                case "<":
                                    comparisonText = "Less than";
                                    if (dataType1.ToUpper() == "DATETIME")
                                    {
                                        if ((((DateTimePicker)ctrl1).Checked && ((DateTimePicker)ctrl2).Checked))
                                        {
                                            validationResult = (System.DateTime.Compare(((DateTimePicker)ctrl1).Value.Date, ((DateTimePicker)ctrl2).Value.Date) < 0);
                                        }
                                        else
                                        {
                                            validationResult = true;
                                        }
                                    }
                                    else if ((dataType1.ToUpper() == "INTEGER") || (dataType1.ToUpper() == "DECIMAL"))
                                    {
                                        validationResult = string.CompareOrdinal(ctrl1.Text, ctrl2.Text) == 0;
                                    }
                                    else
                                    {
                                        validationResult = string.Compare(ctrl1.Text, ctrl2.Text) == 0;
                                    }

                                    break;
                                case "<=":
                                    comparisonText = "Less than or equal to";
                                    if (dataType1.ToUpper() == "DATETIME")
                                    {
                                        if ((((DateTimePicker)ctrl1).Checked && ((DateTimePicker)ctrl2).Checked))
                                        {
                                            validationResult = (System.DateTime.Compare(((DateTimePicker)ctrl1).Value.Date, ((DateTimePicker)ctrl2).Value.Date) <= 0);
                                        }
                                        else
                                        {
                                            //validationResult = Not validationResult
                                            validationResult = true;
                                        }
                                    }
                                    else if ((dataType1.ToUpper() == "INTEGER") || (dataType1.ToUpper() == "DECIMAL"))
                                    {
                                        validationResult = string.CompareOrdinal(ctrl1.Text, ctrl2.Text) <= 0;
                                    }
                                    else
                                    {
                                        validationResult = string.Compare(ctrl1.Text, ctrl2.Text) <= 0;
                                    }

                                    break;
                                case ">":
                                    comparisonText = "Greater than";
                                    if (dataType1.ToUpper() == "DATETIME")
                                    {
                                        if ((((DateTimePicker)ctrl1).Checked && ((DateTimePicker)ctrl2).Checked))
                                        {
                                            validationResult = (System.DateTime.Compare(((DateTimePicker)ctrl1).Value.Date, ((DateTimePicker)ctrl2).Value.Date) > 0);
                                        }
                                        else
                                        {
                                            validationResult = true;
                                        }
                                    }
                                    else if ((dataType1.ToUpper() == "INTEGER") || (dataType1.ToUpper() == "DECIMAL"))
                                    {
                                        validationResult = string.CompareOrdinal(ctrl1.Text, ctrl2.Text) > 0;
                                    }
                                    else
                                    {
                                        validationResult = string.Compare(ctrl1.Text, ctrl2.Text) > 0;
                                    }

                                    break;
                                case ">=":
                                    comparisonText = "Greater than or equal to";
                                    if (dataType1.ToUpper() == "DATETIME")
                                    {
                                        if ((((DateTimePicker)ctrl1).Checked && ((DateTimePicker)ctrl2).Checked))
                                        {
                                            validationResult = (System.DateTime.Compare(((DateTimePicker)ctrl1).Value.Date, ((DateTimePicker)ctrl2).Value.Date) >= 0);
                                        }
                                        else
                                        {
                                            validationResult = true;
                                        }
                                    }

                                    else if ((dataType1.ToUpper() == "INTEGER") || (dataType1.ToUpper() == "DECIMAL"))
                                    {
                                        validationResult = string.CompareOrdinal(ctrl1.Text, ctrl2.Text) >= 0;
                                    }
                                    else
                                    {
                                        validationResult = string.Compare(ctrl1.Text, ctrl2.Text) >= 0;
                                    }
                                    break;
                                default:
                                    comparisonText = "Equal to";
                                    if (dataType1.ToUpper() == "DATETIME")
                                    {
                                        if ((((DateTimePicker)ctrl1).Checked && ((DateTimePicker)ctrl2).Checked))
                                        {
                                            validationResult = (System.DateTime.Compare(((DateTimePicker)ctrl1).Value.Date, ((DateTimePicker)ctrl2).Value.Date) == 0);
                                        }
                                        else
                                        {
                                            validationResult = true;
                                        }
                                    }
                                    else
                                    {
                                        validationResult = (ctrl1.Text == ctrl2.Text);
                                    }

                                    break;
                            }
                            if (validationResult == false)
                            {
                                MessageBox.Show(string.Format(Common.GetMessage("INF0039"), parameter1, comparisonText, parameter2), Common.GetMessage("10000"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                }
            }
            return validationResult;
        }

        public List<T> Search<T>()
        {
            try
            {
                foreach (SearchParam sp in m_searchParams)
                {
                    Control ctrl = null;
                    switch (sp.DataType.ToUpper())
                    {
                        case "VARCHAR":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            m_searchObject.GetType().GetProperty(sp.PropertyName).SetValue(m_searchObject, (ctrl as TextBox).Text.Trim(), null);
                            break;
                        case "INTEGER":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            m_searchObject.GetType().GetProperty(sp.PropertyName).SetValue(m_searchObject,Convert.ToInt32((ctrl as TextBox).Text.Trim()), null);
                            break;
                        case "DECIMAL":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            m_searchObject.GetType().GetProperty(sp.PropertyName).SetValue(m_searchObject, (ctrl as TextBox).Text.Trim(), null);
                            break;
                        case "DATETIME":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            if ((ctrl as DateTimePicker).Checked)
                                m_searchObject.GetType().GetProperty(sp.PropertyName).SetValue(m_searchObject, (ctrl as DateTimePicker).Value, null);
                            else
                                m_searchObject.GetType().GetProperty(sp.PropertyName).SetValue(m_searchObject, Common.DATETIME_NULL, null);

                            break;
                        case "LIST":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            m_searchObject.GetType().GetProperty(sp.PropertyName).SetValue(m_searchObject, (ctrl as ComboBox).SelectedValue, null);
                            break;
                        case "CHECKBOX":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            m_searchObject.GetType().GetProperty(sp.PropertyName).SetValue(m_searchObject, Convert.ToInt32((ctrl as CheckBox).CheckState), null);
                            break;
                        case "INTEGERNULL":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            if (Validators.IsInt32((ctrl as TextBox).Text))
                                m_searchObject.GetType().GetProperty(sp.PropertyName).SetValue(m_searchObject, (Convert.ToInt32((ctrl as TextBox).Text.Trim())), null);
                            break;
                        default:
                            break;
                    }
                }
                if(ItemCodeMapping)
                    m_searchObject.GetType().GetProperty("FromItemCode").SetValue(m_searchObject, FromItemCode, null);
                return (List<T>)m_searchClass.GetMethod(this.SearchMethod).Invoke(m_searchObject, null);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        internal void ResetControls()
        {
            foreach (SearchParam sp in m_searchParams)
            {
                Control ctrl = null;
                if (sp.IsVisible)
                {
                    switch (sp.DataType.ToUpper())
                    {
                        case "VARCHAR":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            (ctrl as TextBox).Text = string.Empty;
                            break;
                        case "INTEGER":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            (ctrl as TextBox).Text = string.Empty;
                            break;
                        case "DECIMAL":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            (ctrl as TextBox).Text = string.Empty;
                            break;
                        case "DATETIME":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            (ctrl as DateTimePicker).Value = DateTime.Now;
                            (ctrl as DateTimePicker).Checked = false;
                            break;
                        case "LIST":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            (ctrl as ComboBox).SelectedIndex = 0;
                            break;
                        case "CHECKBOX":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            (ctrl as CheckBox).CheckState = CheckState.Indeterminate;
                            
                            break;
                        case "INTEGERNULL":
                            ctrl = m_searchPanel.Controls.Find(sp.Name, true)[0];
                            (ctrl as TextBox).Text = string.Empty;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
