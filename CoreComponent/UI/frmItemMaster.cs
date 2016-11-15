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
using AuthenticationComponent.BusinessObjects;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Net;
namespace CoreComponent.MasterData.UI
{
    public partial class frmItemMaster : MultiTabTemplate
    {
        #region Shopping Cart
        string ItemImage = "";
        string LoadedImage = string.Empty;
        private string Filepath = "";
        string CurrentEnvironmentPath = Environment.CurrentDirectory;
        string EnvironmentPath = Environment.CurrentDirectory;
        #endregion

        private string GRIDVIEW_XML_PATH = string.Empty;
        private string UserName;
        private string CON_MODULENAME = string.Empty;
        // Variables for Rights

        private Boolean IsSaveAvailable = false;
        private Boolean IsUpdateAvailable = false;
        private Boolean IsSearchAvailable = false;
        private Boolean IsBrowseAvailable = false;

        #region Variables for Item-Master
        public List<ItemDetails> m_ItemsList = null;
        public List<ItemBarCode> m_ItemBarCodeList = null;
        private frmLocationListNew m_objFrmLocList = null;
        private frmUOM objFrmUOM = null;
        private frmCompositeItemsList objFrmCompositeItemsList = null;
        private frmItemBarCode m_objFrmBarCode = null;
        private Int32 m_selectedItemId = Common.INT_DBNULL;
        private int m_userId;
        private DateTime m_modifiedDate;
        #endregion

        #region Variables for Item Vendor
        private List<ItemDetails> ItemList;
        private AutoCompleteStringCollection _itemcollection;
        private Boolean m_isVendorModified = false;
        private ItemVendorDetails m_ItemVendor = null;
        private List<ItemVendorDetails> m_selectedItemVendors = null;
        #endregion

        #region Variables for Item-Vendor-Location
        private Boolean m_isVendorLocModified = false;
        //  private String m_vendorLocItemCode = String.Empty;
        // private Int32 m_vendorLocVendorId = Common.INT_DBNULL;
        // private Int32 m_vendorLocLocationId = Common.INT_DBNULL;
        // private DateTime m_vendorLocModDate = Common.DATETIME_NULL;

        private ItemVendorLocationDetails m_ItemVendorLoc = null;
        private List<ItemVendorLocationDetails> m_selectedVendorLoc = null;
        #endregion


        #region Constructors
        public frmItemMaster()
        {
            InitializeComponent();

            (new ModifyTabControl()).RemoveTabPageArray(tabMultiTabTemplate, 3);
            label1.Text = "Item";
        }

        private void frmItemMaster_Load(object sender, EventArgs e)
        {
            if (AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser != null)
            {
                m_userId = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserId;
                UserName = AuthenticationComponent.BusinessObjects.Authenticate.LoggedInUser.UserName;
            }
            m_modifiedDate = Common.DATETIME_NULL;
            GRIDVIEW_XML_PATH = Environment.CurrentDirectory + "\\App_Data\\GridViewDefinition.xml";

            CON_MODULENAME = this.Tag.ToString();
            InitializeRights();

            InitializeControls();
        }
        private void InitializeRights()
        {
            try
            {
                if (UserName != null && !CON_MODULENAME.Equals(string.Empty))
                {
                    #region Shopping Cart
                    IsBrowseAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SAVE);
                    #endregion

                    IsSaveAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SAVE);
                    IsUpdateAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_UPDATE);
                    IsSearchAvailable = AuthenticationComponent.BusinessObjects.Authenticate.IsFunctionAccessible(UserName, Common.LocationCode, CON_MODULENAME, Common.FUNCTIONCODE_SEARCH);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void InitializeControls()
        {
            try
            {
                InitailizeItemMasterTab();

                InitailizeItemVendorTab();

                InitailizeVendorLocationTab();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Item Master

        void InitailizeItemMasterTab()
        {
            try
            {
                //Bind Status Combobox
                DataTable datatableStatus = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.STATUS, 0, 0, 0));
                cmbStatus.DataSource = datatableStatus;
                cmbStatus.ValueMember = Common.KEYCODE1;
                cmbStatus.DisplayMember = Common.KEYVALUE1;
                cmbStatus.SelectedValue = 1;

                //Bind ItemPackType
                DataTable dtItemPackType = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("ItemType", 0, 0, 0));
                cmbItemPackType.DataSource = dtItemPackType;
                cmbItemPackType.ValueMember = Common.KEYCODE1;
                cmbItemPackType.DisplayMember = Common.KEYVALUE1;
                cmbItemPackType.SelectedValue = 1;

                //Bind ItemExpiryDateFormat
                DataTable dtItemExpiryDateFormat = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter("ItemExpiryDateFormat", 0, 0, 0));
                dtItemExpiryDateFormat.Rows[0].Delete();
                cmbExpiryDateFormat.DataSource = dtItemExpiryDateFormat;
                cmbExpiryDateFormat.ValueMember = Common.KEYCODE1;
                cmbExpiryDateFormat.DisplayMember = Common.KEYVALUE1;
                cmbExpiryDateFormat.SelectedValue = 1;

                //Bind Sub Category Combobox
                DataTable datatableSubCategory = Common.ParameterLookup(Common.ParameterType.SubCategory, new ParameterFilter(string.Empty, 0, 0, 0));
                cmbSubCategory.DataSource = datatableSubCategory;
                cmbSubCategory.ValueMember = Common.SUB_CATEGORY_ID;
                cmbSubCategory.DisplayMember = Common.SUB_CATEGORY_NAME;

                //Bind Tax Category Combobox
                DataTable datatableTaxCategory = Common.ParameterLookup(Common.ParameterType.TaxCategory, new ParameterFilter(string.Empty, 0, 0, 0));
                cmbTaxCat.DataSource = datatableTaxCategory;
                cmbTaxCat.ValueMember = Common.TAX_CATEGORY_ID;
                cmbTaxCat.DisplayMember = Common.TAX_CATEGORY_NAME;

                //Check Checkboxes to Indeterminate
                chkComposite.CheckState = CheckState.Indeterminate;
                chkKit.CheckState = CheckState.Indeterminate;
                chkIsPromoPart.CheckState = CheckState.Indeterminate;
                chkIsAvailableForGift.CheckState = CheckState.Unchecked;
                chkRegistrationPurpose.CheckState = CheckState.Unchecked;
                //Disbale Button for Composite Items
                btnCompsiteItems.Enabled = false;

                //Disbale Text for Min order value, enable when Is Kit is set
                // txtMinKitValue.Enabled = false;

                DataGridView dgv = Common.GetDataGridViewColumns(dgvItemMaster, GRIDVIEW_XML_PATH);
                FormatGridView(dgvItemMaster);

                #region Shopping Cart
                btnBrowse.Enabled = IsBrowseAvailable;
                #endregion

                btnSave.Enabled = IsSaveAvailable;
                btnSearch.Enabled = IsSearchAvailable;

                ResetControls();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region Events

        private void btnCompsiteItems_Click(object sender, EventArgs e)
        {
            try
            {
                if (objFrmCompositeItemsList == null)
                    objFrmCompositeItemsList = new frmCompositeItemsList(txtItemCode.Text);
                DialogResult dResult = objFrmCompositeItemsList.ShowDialog();
                if (dResult == DialogResult.OK && objFrmCompositeItemsList.SelectedCompositeItems.Count == 0)
                {
                    objFrmCompositeItemsList = null;
                }
                else if (dResult == DialogResult.Cancel && objFrmCompositeItemsList.OriginalCompositeList.Count == 0)
                {
                    objFrmCompositeItemsList = null;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLocations_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objFrmLocList == null)
                    m_objFrmLocList = new frmLocationListNew(m_selectedItemId);
                DialogResult dResult = m_objFrmLocList.ShowDialog();
                if (dResult == DialogResult.OK && m_objFrmLocList.SelectedLocations.Count == 0)
                {
                    m_objFrmLocList = null;
                }
                else if (dResult == DialogResult.Cancel && m_objFrmLocList.OriginalLocationList.Count == 0)
                {
                    m_objFrmLocList = null;
                }

                //if (objFrmLocList == null)
                //    objFrmLocList = new frmLocationListNew();
                ////objFrmLocList.Location = PlacePopUp(txtLocations.Location);
                //objFrmLocList.ShowDialog();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                epItemMaster.Clear();
                SearchItems();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void dgvItemMaster_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    try
        //    {
        //        //SelectGridData(e);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogException(ex);
        //        MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        private void dgvItemMaster_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView dgv = (sender as DataGridView);
                SelectGridData(dgv);
                btnSave.Enabled = false;
                btnSearch.Enabled = IsSearchAvailable;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvItemMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvItemMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell))
                {
                    DataGridView dgv = (sender as DataGridView);
                    SelectGridData(dgv);
                    btnSave.Enabled = IsUpdateAvailable;
                    btnSearch.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnUOM_Click(object sender, EventArgs e)
        {
            try
            {
                if (objFrmUOM == null)
                    objFrmUOM = new frmUOM();
                DialogResult dResult = objFrmUOM.ShowDialog();
                if (dResult == DialogResult.OK && objFrmUOM.SelectedUOMs.Count == 0)
                {
                    objFrmUOM = null;
                }
                else if (dResult == DialogResult.Cancel && objFrmUOM.OriginalItemUOMList.Count == 0)
                {
                    objFrmUOM = null;
                }
                //DialogResult dr =  objFrmUOM.ShowDialog();
                //if (dr == DialogResult.OK && (objFrmUOM.SelectedUOMs == null || objFrmUOM.SelectedUOMs.Count <= 0))
                //{
                //    objFrmUOM = null;
                //}
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            catch (Exception ex)
            {
                return imageDir;
            }
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                CallItemMasterValidations();

                String errMessage = GetErrorMessages();
                if (errMessage.Length > 0)
                {
                    MessageBox.Show(errMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                string msg = string.Empty;
                if (m_selectedItemId > 0)
                    msg = "Edit";
                else
                    msg = "Save";
                DialogResult saveResult = MessageBox.Show(Common.GetMessage("5010", msg), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveResult == DialogResult.Yes)
                {
                    try
                    {
                        #region Shopping Cart
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
                        }

                        if (m_selectedItemId > 0) // Edit mode
                        {
                            if (Filepath != string.Empty)
                            {

                                ItemDetails ItemDetails = new ItemDetails();
                                string imageName = ItemDetails.GetItemImageSearch(m_selectedItemId);

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
                        }
                        else  // Create New Mode
                        {
                            if (Filepath != string.Empty)  // Check if image has been selected by the user
                            {
                                Upload(imagePath);
                            }
                        }
                        if (Filepath != string.Empty)
                            File.Delete(imagePath);//To Delete Temp file from local drive after uploaded on FTP Server

                        Environment.CurrentDirectory = CurrentEnvironmentPath;
                        EnvironmentPath = Environment.CurrentDirectory;

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Environment.CurrentDirectory = CurrentEnvironmentPath;
                        Common.LogException(ex);
                    }
                    SaveItems();

                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void chkComposite_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                EnableDisableOnCheck(chkComposite, btnCompsiteItems);

                if (chkComposite.CheckState == CheckState.Checked)
                {
                    objFrmCompositeItemsList = null;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void chkKit_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                EnableDisableOnCheck(chkKit, txtMinKitValue);
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkRegistrationPurpose_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRegistrationPurpose.CheckState == CheckState.Checked)
                {
                    txtDP.Text = (0.00).ToString();
                    txtMRP.Text = (0.00).ToString();
                    chkKit.Checked = false;
                    txtLandedPrice.Text = (0.00).ToString();

                    txtDP.Enabled = false;
                    txtMRP.Enabled = false;

                    chkKit.CheckState = CheckState.Unchecked;
                    txtLandedPrice.Enabled = false;
                    txtMinKitValue.Enabled = true;

                }
                else if (chkRegistrationPurpose.CheckState == CheckState.Unchecked)
                {
                    txtDP.Enabled = true;
                    txtMRP.Enabled = true;
                    //chkKit.CheckState = CheckState.Checked;
                    txtLandedPrice.Enabled = true;
                    txtMinKitValue.Enabled = false;
                    txtMinKitValue.Text = string.Empty;

                }
                else if (chkRegistrationPurpose.CheckState == CheckState.Indeterminate)
                {
                    txtDP.Enabled = true;
                    txtMRP.Enabled = true;
                    //chkKit.CheckState = CheckState.Indeterminate;
                    txtLandedPrice.Enabled = true;
                    txtMinKitValue.Enabled = false;
                    txtMinKitValue.Text = string.Empty;
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
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPV_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    epItemMaster.SetError(txtPV, string.Empty);
            //    if (Validators.IsValidAmount(txtPV.Text.Trim()))
            //    {
            //        decimal pvalue = Convert.ToDecimal(txtPV.Text);
            //        txtBV.Text = Convert.ToString(pvalue * 16);
            //    }
            //    else
            //    {
            //        epItemMaster.SetError(txtPV, Common.GetMessage("VAL0009", "Point Value"));
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Common.LogException(ex);
            //    MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void btnBarCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_ItemBarCodeList == null)
                    m_ItemBarCodeList = new List<ItemBarCode>();
                m_objFrmBarCode = new frmItemBarCode(m_selectedItemId, m_ItemBarCodeList);
                DialogResult dResult = m_objFrmBarCode.ShowDialog();

                if (dResult == DialogResult.OK && m_objFrmBarCode.ItemBarCodeList.Count > 0)
                {
                    m_ItemBarCodeList = m_objFrmBarCode.ItemBarCodeList;
                }
                else
                {
                    m_ItemBarCodeList = null;
                }


            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        #region Validation Events


        void CallItemMasterValidations()
        {
            try
            {
                epItemMaster.Clear();

                TextBoxValidations(txtItemCode, lblItemCode, epItemMaster);
                if (epItemMaster.GetError(txtItemCode).Trim().Equals(string.Empty))
                {
                    string ItemError = Common.CodeValidate(txtItemCode.Text, lblItemCode.Text.Trim().Substring(0, lblItemCode.Text.Length - 2));
                    epItemMaster.SetError(txtItemCode, ItemError);
                }
                TextBoxValidations(txtItemName, lblItemName, epItemMaster);

                TextBoxValidations(txtShortName, lblShortName, epItemMaster);
                TextBoxValidations(txtPrintName, lblPrintName, epItemMaster);
                TextBoxValidations(txtDisplayName, lblDisplayName, epItemMaster);
                TextBoxValidations(txtReceiptName, lblReceiptName, epItemMaster);
                //TextBoxValidations(txtWebDescriptions, lblWebDescription, epItemMaster); //Roshan				
                //TextBoxValidations(txtUSDPrice, lblUSDPrice, epItemMaster);//Roshan
                ComboBoxValidations(cmbSubCategory, lblSubCatName, epItemMaster);
                //AmountValidations(txtMRP, lblMRP, epItemMaster, true);

                //AmountValidations(txtCost, lblCost, epItemMaster);

                //AmountValidations(txtDP, lblDP, epItemMaster, true);

                //AmountValidations(txtBV, lblBV, epItemMaster, true);

                //AmountValidations(txtPV, lblPV, epItemMaster, true);

                ComboBoxValidations(cmbTaxCat, lblTaxCat, epItemMaster);
                ComboBoxValidations(cmbStatus, lblStatus, epItemMaster);
                CheckBoxValidations(chkComposite, lblComposite, epItemMaster);
                //CheckBoxValidations(chkKit, lblKit, epItemMaster);
                CheckBoxValidations(chkIsPromoPart, lblIsPromoPart, epItemMaster);
                //ValidateExpDuration(txtExpDuration, lblExpDuration.Text.Trim().Substring(0, lblExpDuration.Text.Length - 2));

                if (chkIsPromoPart.CheckState == CheckState.Unchecked)
                {
                    DataTable dt = Common.ParameterLookup(Common.ParameterType.ItemGroupsOfNonPromoItem, new ParameterFilter("", m_selectedItemId, 0, 0));
                    if (dt.Rows.Count > 0)
                    {
                        Validators.SetErrorMessage(epItemMaster, chkIsPromoPart, "VAL0111", "remove", "Promo-Participation", "it is already linked with existing active Item-Groups");
                    }
                }
                CheckBoxValidations(chkIsAvailableForGift, lblIsAvailableForGift, epItemMaster);
                CheckBoxValidations(chkKit, lblKit, epItemMaster); ;
                CheckBoxValidations(chkRegistrationPurpose, lblIsForRegistration, epItemMaster);

                // Required except create /update KIt and reg Purpose item
                if (chkKit.CheckState == CheckState.Checked || chkRegistrationPurpose.CheckState == CheckState.Checked)
                {
                    if (!Validators.IsDecimal(txtMinKitValue.Text))
                    {
                        Validators.SetErrorMessage(epItemMaster, txtMinKitValue, "VAL0001", "Min. Kit Order Value");
                    }
                }
                //IsDecimal(txtMRP, lblMRP, epItemMaster);
                //IsDecimal(txtDP, lblDP, epItemMaster);
                if (string.IsNullOrEmpty(epItemMaster.GetError(txtDP)))
                {
                    ValidateDistributorPrice(txtMRP, txtDP, lblDP, epItemMaster);
                }
                //IsDecimal(txtCost, lblCost, epItemMaster);
                //IsDecimal(txtBV, lblBV, epItemMaster);
                // IsDecimal(txtPV, lblPV, epItemMaster);
                GreaterThanZeroValidations(txtExpDuration, lblExpDuration, epItemMaster);
                //AmountValidations(txtPV, lblPV, epItemMaster, true);


                IsInteger(txtLength, lblLength, epItemMaster, true);
                IsInteger(txtWidth, lblWidth, epItemMaster, true);
                IsInteger(txtHeight, lblHeight, epItemMaster, true);
                // IsInteger(txtWeight, lblWeight, epItemMaster, true); //  commented by alok
                IsDecimal(txtWeight, lblWeight, epItemMaster); // inducted by alok
                IsInteger(txtStackLimit, lblStackLimit, epItemMaster, true);
                //IsInteger(txtExpDuration, lblExpDuration, epItemMaster, true);
                #region Shopping Cart
                IsInteger(txtUSDPrice, lblUSDPrice, epItemMaster, true);

                #endregion
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

                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtItemCode), ref sbError);

                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtItemName), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtShortName), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtPrintName), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtDisplayName), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtReceiptName), ref sbError);

                #region Shopping Cart

                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtWebDescriptions), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtUSDPrice), ref sbError);

                #endregion

                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, cmbSubCategory), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtMRP), ref sbError);
                if (!string.IsNullOrEmpty(Validators.GetErrorMessage(epItemMaster, txtCost)))
                {
                    Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtCost), ref sbError);
                }
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtDP), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtPV), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtBV), ref sbError);

                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, chkComposite), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, chkKit), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, chkIsAvailableForGift), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, chkIsPromoPart), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, chkRegistrationPurpose), ref sbError);
                //if (chkIsPromoPart.CheckState == CheckState.Indeterminate)
                //{
                //    Validators.SetErrorMessage(epItemMaster, chkIsPromoPart, "VAL0002", lblIsPromoPart.Text);
                //    Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, chkIsPromoPart), ref sbError);
                //}
                //else
                //{
                //    Validators.SetErrorMessage(epItemMaster, chkIsPromoPart);
                //}
                //if (chkComposite.CheckState == CheckState.Indeterminate)
                //{
                //    Validators.SetErrorMessage(epItemMaster, chkComposite, "VAL0002", lblComposite.Text);
                //    Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, btnCompsiteItems), ref sbError);
                //}
                //else
                //{
                //    Validators.SetErrorMessage(epItemMaster, chkComposite);
                //}
                //if (chkKit.CheckState == CheckState.Indeterminate)
                //{
                //    Validators.SetErrorMessage(epItemMaster, chkKit, "VAL0002", lblKit.Text);
                //    Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, chkKit), ref sbError);
                //}
                //else
                //{
                //    Validators.SetErrorMessage(epItemMaster, chkKit);
                //}

                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtMinKitValue), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, cmbTaxCat), ref sbError);
                //Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster,txtExpDuration), ref sbError);

                if (!string.IsNullOrEmpty(Validators.GetErrorMessage(epItemMaster, cmbStatus)))
                {
                    Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, cmbStatus), ref sbError);
                }

                if (m_objFrmLocList == null || m_objFrmLocList.SelectedLocations == null || m_objFrmLocList.SelectedLocations.Count <= 0)
                {
                    Validators.AppendToStringBuilder(Common.GetMessage("VAL0001", "one or more Locations for the Item"), ref sbError);
                }
                if (objFrmUOM == null || objFrmUOM.SelectedUOMs == null || objFrmUOM.SelectedUOMs.Count <= 0)
                {
                    Validators.AppendToStringBuilder(Common.GetMessage("VAL0001", "one or more Unit of Measurement for the Item"), ref sbError);
                }
                //else
                //{
                //    if (objFrmUOM.SelectedUOMs.Count > 0)
                //    {
                //        int count = (from p in objFrmUOM.SelectedUOMs
                //                     where p.IsPrimary == true && p.TOMId == 1
                //                     select p).Count();
                //        if (count == 0)
                //        {
                //            Validators.AppendToStringBuilder(Common.GetMessage("VAL0024", "Purchase UOM for the item"), ref sbError);
                //        }
                //    }
                //}

                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtLength), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtWeight), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtWidth), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtHeight), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtStackLimit), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, txtExpDuration), ref sbError);

                //if (chkIsAvailableForGift.CheckState == CheckState.Checked && chkComposite.CheckState != CheckState.Unchecked)
                //{
                //    Validators.SetErrorMessage(epItemMaster, chkIsAvailableForGift, "VAL0076");
                //    Validators.AppendToStringBuilder(Validators.GetErrorMessage(epItemMaster, chkIsAvailableForGift), ref sbError);
                //}
                //else
                //{
                //    Validators.SetErrorMessage(epItemMaster, chkIsAvailableForGift);
                //}

                if (chkComposite.CheckState == CheckState.Checked && (objFrmCompositeItemsList == null || objFrmCompositeItemsList.SelectedCompositeItems == null || objFrmCompositeItemsList.SelectedCompositeItems.Count <= 0))
                {
                    Validators.AppendToStringBuilder(Common.GetMessage("VAL0001", "one or more Items for Composition"), ref sbError);
                }

                return Common.ReturnErrorMessage(sbError).ToString();
                //if (!Validators.IsDecimal(txtLength.Text))
                //    Validators.AppendToStringBuilder(Common.GetMessage("VAL0001", "Item Length"), ref sbError);

                //if (!Validators.IsDecimal(txtWidth.Text))
                //    Validators.AppendToStringBuilder(Common.GetMessage("VAL0001", "Item Width"), ref sbError);

                //if (!Validators.IsDecimal(txtHeight.Text))
                //    Validators.AppendToStringBuilder(Common.GetMessage("VAL0001", "Item Height"), ref sbError);

                //if (!Validators.IsDecimal(txtWeight.Text))
                //    Validators.AppendToStringBuilder(Common.GetMessage("VAL0001", "Item Weight"), ref sbError);

                //if (!Validators.IsDecimal(txtStackLimit.Text))
                //    Validators.AppendToStringBuilder(Common.GetMessage("VAL0001", "Item stack Limit"), ref sbError);

                //if (!Validators.IsInt32(txtExpDuration.Text))
                //    Validators.AppendToStringBuilder(Common.GetMessage("VAL0001", "Expiry Duration"), ref sbError);







            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region Methods

        ItemDetails CreateItemObject()
        {
            ItemDetails item = new ItemDetails();

            #region Shopping Cart

            item.ItemImage = ItemImage;
            item.WebDescription = !Validators.CheckForEmptyString(txtWebDescriptions.Text.Length) ? txtWebDescriptions.Text : string.Empty;
            item.USDPrice = !Validators.CheckForEmptyString(txtUSDPrice.Text.Length) ? txtUSDPrice.Text : string.Empty;

            #endregion

            item.ItemId = m_selectedItemId;
            item.ItemCode = !Validators.CheckForEmptyString(txtItemCode.Text.Length) ? txtItemCode.Text : string.Empty;
            //item.ItemBarCode = !Validators.CheckForEmptyString(txtBarcode.Text.Length) ? txtBarcode.Text : string.Empty;
            item.ItemBarCodeList = m_ItemBarCodeList != null ? m_ItemBarCodeList : null;

            item.ItemName = !Validators.CheckForEmptyString(txtItemName.Text.Length) ? txtItemName.Text : string.Empty;
            item.ShortName = !Validators.CheckForEmptyString(txtShortName.Text.Length) ? txtShortName.Text : string.Empty;
            item.DisplayName = !Validators.CheckForEmptyString(txtDisplayName.Text.Length) ? txtDisplayName.Text : string.Empty;
            item.PrintName = !Validators.CheckForEmptyString(txtPrintName.Text.Length) ? txtPrintName.Text : string.Empty;
            item.ReceiptName = !Validators.CheckForEmptyString(txtReceiptName.Text.Length) ? txtReceiptName.Text : string.Empty;

            item.SubCategoryId = !Validators.CheckForSelectedValue(cmbSubCategory.SelectedIndex) ? Convert.ToInt32(cmbSubCategory.SelectedValue) : -1;
            //item.MRP = Validators.IsDecimal(txtMRP.Text) ? Convert.ToDecimal(txtMRP.Text) : 0;
            item.MRP = Validators.IsDecimalCanbeEmpty(txtMRP.Text) ? Convert.ToDecimal(txtMRP.Text) : 0;
            item.PrimaryCost = Validators.IsDecimal(txtCost.Text) ? Convert.ToDecimal(txtCost.Text) : 0;
            item.DistributorPrice = Validators.IsDecimal(txtDP.Text) ? Convert.ToDecimal(txtDP.Text) : 0;
            /*item.TransferPrice = Validators.IsDecimal(txtTP.Text) ? Convert.ToDecimal(txtTP.Text) : 0;*/
            item.BusinessVolume = Validators.IsDecimalCanbeEmpty(txtBV.Text) ? Convert.ToDecimal(txtBV.Text) : 0;
            item.PointValue = Validators.IsDecimalCanbeEmpty(txtPV.Text) ? Convert.ToDecimal(txtPV.Text) : 0;

            item.IsPromoPart = Convert.ToInt32(chkIsPromoPart.CheckState);
            item.IsKit = Convert.ToInt32(chkKit.CheckState);
            item.IsComposite = Convert.ToInt32(chkComposite.CheckState);
            item.TaxCategoryId = !Validators.CheckForSelectedValue(cmbTaxCat.SelectedIndex) ? Convert.ToInt32(cmbTaxCat.SelectedValue) : -1;
            item.Status = !Validators.CheckForSelectedValue(cmbStatus.SelectedIndex) ? Convert.ToInt32(cmbStatus.SelectedValue) : -1;
            item.MinKitValue = Validators.IsInt32(txtMinKitValue.Text.Trim()) ? Convert.ToInt32(txtMinKitValue.Text.Trim()) : 0;
            item.IsAvailableForGift = Convert.ToInt32(chkIsAvailableForGift.CheckState);
            item.ModifiedBy = m_userId;
            item.ModifiedDate = m_modifiedDate;

            //item dimensions
            item.ItemLength = Validators.IsInt32(txtLength.Text.Trim()) ? Convert.ToInt32(txtLength.Text.Trim()) : 0;
            item.ItemWidth = Validators.IsInt32(txtWidth.Text.Trim()) ? Convert.ToInt32(txtWidth.Text.Trim()) : 0;
            item.ItemHeight = Validators.IsInt32(txtHeight.Text.Trim()) ? Convert.ToInt32(txtHeight.Text.Trim()) : 0;
            item.StackLimit = Validators.IsInt32(txtStackLimit.Text.Trim()) ? Convert.ToInt32(txtStackLimit.Text.Trim()) : 0;
            item.ItemWeight = Convert.ToDecimal(txtWeight.Text.Trim()); // modified by Alok
            item.ExpiryDuration = Validators.IsInt32(txtExpDuration.Text.Trim()) ? Convert.ToInt32(txtExpDuration.Text.Trim()) : 0;
            item.BayNumber = txtBayNumber.Text.Trim();
            item.LocationsList = m_objFrmLocList != null ? m_objFrmLocList.SelectedLocations : null;
            item.ItemUOMList = objFrmUOM != null ? objFrmUOM.SelectedUOMs : null;
            item.CompositeItems = objFrmCompositeItemsList != null ? objFrmCompositeItemsList.SelectedCompositeItems : null;
            item.TransferPrice = Validators.IsDecimalCanbeEmpty(txtItemPrice.Text) ? Convert.ToDecimal(txtItemPrice.Text) : 0;
            item.LandedPrice = Validators.IsDecimalCanbeEmpty(txtLandedPrice.Text) ? Convert.ToDecimal(txtLandedPrice.Text) : 0;
            item.IsForRegistrationPurpose = Convert.ToInt32(chkRegistrationPurpose.CheckState);
            item.ItemTypeID = Convert.ToInt32(cmbItemPackType.SelectedValue);
            item.ItemPackSize = Convert.ToInt32(txtItemPackSize.Text);
            item.ExpiryDateFormat = Convert.ToInt32(cmbExpiryDateFormat.SelectedValue);

            //if(chkRegistrationPurpose.CheckState == CheckState.Checked)  
            //    item.IsForRegistrationPurpose = 1;
            //else if (chkRegistrationPurpose.CheckState == CheckState.Unchecked)
            //    item.IsForRegistrationPurpose = 0;
            //else if (chkRegistrationPurpose.CheckState == CheckState.Indeterminate)
            //{
            //    item.IsForRegistrationPurpose = 0;
            //    item.IsKit = 0;
            //}

            return item;
        }



        void SetItemObject(ItemDetails selectedItem)
        {
            txtItemCode.Text = Validators.CheckForDBNull(selectedItem.ItemCode, string.Empty);
            //txtBarcode.Text = Validators.CheckForDBNull(selectedItem.ItemBarCode, string.Empty);

            txtItemName.Text = Validators.CheckForDBNull(selectedItem.ItemName, string.Empty);
            txtShortName.Text = Validators.CheckForDBNull(selectedItem.ShortName, string.Empty);
            txtPrintName.Text = Validators.CheckForDBNull(selectedItem.PrintName, string.Empty);
            txtDisplayName.Text = Validators.CheckForDBNull(selectedItem.DisplayName, string.Empty);
            txtReceiptName.Text = Validators.CheckForDBNull(selectedItem.ReceiptName, string.Empty);

            cmbSubCategory.SelectedValue = Validators.CheckForDBNull(selectedItem.SubCategoryId, (Int32)(-1));

            chkIsPromoPart.CheckState = (CheckState)Validators.CheckForDBNull(selectedItem.IsPromoPart, (Int32)2);
            chkKit.CheckState = (CheckState)Validators.CheckForDBNull(selectedItem.IsKit, (Int32)2);
            chkComposite.CheckState = (CheckState)Validators.CheckForDBNull(selectedItem.IsComposite, (Int32)2);
            txtMinKitValue.Text = Validators.CheckForDBNull(selectedItem.MinKitValue, string.Empty);
            chkIsAvailableForGift.CheckState = (CheckState)Validators.CheckForDBNull(selectedItem.IsAvailableForGift, (Int32)2);
            cmbTaxCat.SelectedValue = Validators.CheckForDBNull(selectedItem.TaxCategoryId, (Int32)(-1));
            cmbStatus.SelectedValue = Validators.CheckForDBNull(selectedItem.Status, (Int32)(-1));

            txtMRP.Text = selectedItem.DisplayMRP.ToString();
            txtCost.Text = selectedItem.DisplayPrimaryCost.ToString();
            txtDP.Text = Validators.CheckForDBNull(selectedItem.DisplayDistributorPrice, string.Empty);
            /*txtTP.Text = Validators.CheckForDBNull(selectedItem.TransferPrice, string.Empty);*/
            txtBV.Text = Validators.CheckForDBNull(selectedItem.DisplayBusinessVolume, string.Empty);
            txtPV.Text = Validators.CheckForDBNull(selectedItem.DisplayPointValue, string.Empty);

            txtLength.Text = Validators.CheckForDBNull(selectedItem.ItemLength, string.Empty);
            txtWidth.Text = Validators.CheckForDBNull(selectedItem.ItemWidth, string.Empty);
            txtHeight.Text = Validators.CheckForDBNull(selectedItem.ItemHeight, string.Empty);
            txtWeight.Text = Validators.CheckForDBNull(selectedItem.ItemWeight, string.Empty);
            txtStackLimit.Text = Validators.CheckForDBNull(selectedItem.StackLimit, string.Empty);
            txtBayNumber.Text = Validators.CheckForDBNull(selectedItem.BayNumber, string.Empty);
            txtExpDuration.Text = selectedItem.ExpiryDuration.ToString();
            txtLandedPrice.Text = selectedItem.DisplayLandedPrice.ToString();
            txtItemPrice.Text = selectedItem.DisplayTransferPrice.ToString();
            #region Shopping Cart

            txtUSDPrice.Text = selectedItem.USDPrice;
            txtWebDescriptions.Text = selectedItem.WebDescription;

            if (string.IsNullOrEmpty(selectedItem.ItemImage) == false)
            {
                try
                {
                    string imagePath = DownLoad(selectedItem.ItemImage);
                    //this.ProductImage.Load(ConfigurationManager.AppSettings["WebImagesPath"] + selectedItem.ItemImage);
                    this.ProductImage.Load(imagePath);
                    LoadedImage = selectedItem.ItemImage;

                    if (File.Exists(imagePath))
                        File.Delete(imagePath);
                }
                catch (Exception ex)
                {
                    ProductImage.Image = null;
                    this.LoadedImage = "";
                    this.Filepath = "";
                    Common.LogException(ex);
                }

            }
            else
            {
                ProductImage.Image = null;
                this.LoadedImage = "";
                this.Filepath = "";
            }
            Environment.CurrentDirectory = EnvironmentPath;

            #endregion

            chkRegistrationPurpose.CheckState = (CheckState)Validators.CheckForDBNull(selectedItem.IsForRegistrationPurpose, (Int32)2);

            //if (chkRegistrationPurpose.CheckState = CheckState.Indeterminate)
            //{ 
            //     chkKit.CheckState
            //}
            //objFrmLocList = new frmLocationsList(selectedItem.LocationsList);
            m_objFrmLocList = new frmLocationListNew(selectedItem.LocationsList, selectedItem.ItemId);
            objFrmUOM = new frmUOM(selectedItem.ItemUOMList);
            if (chkComposite.CheckState == CheckState.Checked)
                objFrmCompositeItemsList = new frmCompositeItemsList(selectedItem.CompositeItems, selectedItem.ItemId >= 0);
            //chkComposite.Enabled = false;
            //chkKit.Enabled = false;
            //chkIsAvailableForGift.Enabled = false;
            //chkIsPromoPart.Enabled = false;
            //btnCompsiteItems.Enabled = false;
            cmbItemPackType.SelectedValue = selectedItem.ItemTypeID;
            txtItemPackSize.Text = selectedItem.ItemPackSize.ToString();
            cmbExpiryDateFormat.SelectedValue = selectedItem.ExpiryDateFormat;
        }

        void SearchItems()
        {
            try
            {

                dgvItemMaster.SelectionChanged -= dgvItemMaster_SelectionChanged;
                ItemDetails currentItem = CreateItemObject();
                NullifyDataGridView(dgvItemMaster);

                m_ItemsList = currentItem.Search();
                if (m_ItemsList != null)
                {
                    //(new VisitControls()).ResetAllControlsInPanel(epItemMaster, pnlSearchHeader);
                    BindDataGridView(dgvItemMaster, ListOfDataSources.ListOfItemDetails);
                }
                else
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                dgvItemMaster.ClearSelection();
                dgvItemMaster.SelectionChanged += new EventHandler(dgvItemMaster_SelectionChanged);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void SelectGridData(DataGridView dgv)
        {
            try
            {
                if (dgv.CurrentRow.Index >= 0)
                {
                    m_ItemBarCodeList = null;
                    objFrmCompositeItemsList = null;
                    Int32 itemId = Convert.ToInt32(dgv.CurrentRow.Cells[ItemDetails.GRID_ITEM_ID].Value);
                    ItemDetails selectedItem = (from item in m_ItemsList where item.ItemId == itemId select item).FirstOrDefault();
                    SetItemObject(selectedItem);
                    m_selectedItemId = itemId;

                    m_modifiedDate = Convert.ToDateTime(dgv.CurrentRow.Cells[ItemDetails.GRID_MODIFIED_DATE].Value);
                    EnableUniqueElements(false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void SelectGridData(DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex >= 0 && e.RowIndex >= 0) && dgvItemMaster.Columns[e.ColumnIndex].IsDataBound == true)
                {
                    Int32 itemId = Convert.ToInt32(dgvItemMaster.Rows[e.RowIndex].Cells[ItemDetails.GRID_ITEM_ID].Value);

                    ItemDetails selectedItem = (from item in m_ItemsList where item.ItemId == itemId select item).FirstOrDefault();

                    SetItemObject(selectedItem);

                    m_selectedItemId = itemId;
                    m_modifiedDate = Convert.ToDateTime(dgvItemMaster.Rows[e.RowIndex].Cells[ItemDetails.GRID_MODIFIED_DATE].Value);

                    EnableUniqueElements(false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void SaveItems()
        {
            try
            {
                ItemDetails newItem = CreateItemObject();
                String errorMessage = string.Empty;
                Boolean isSuccess = newItem.Save(ref errorMessage);

                if (isSuccess)
                {
                    //NullifyDataGridView(dgvItemMaster);
                    newItem = CreateItemObject();

                    m_ItemsList = newItem.Search();
                    BindDataGridView(dgvItemMaster, ListOfDataSources.ListOfItemDetails);
                    MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (errorMessage.StartsWith("Exists"))
                    {
                        errorMessage = errorMessage.Substring(0, errorMessage.Length - 2);
                        MessageBox.Show(Common.GetMessage("INF0143", errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                        MessageBox.Show(Common.GetMessage(errorMessage), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ResetControls()
        {
            try
            {


                NullifyDataGridView(dgvItemMaster);
                chkComposite.Enabled = true;
                chkKit.Enabled = true;
                chkIsPromoPart.Enabled = true;
                (new VisitControls()).ResetAllControlsInPanel(epItemMaster, pnlSearchHeader);
                cmbStatus.SelectedValue = 1;
                chkComposite.CheckState = CheckState.Indeterminate;
                chkIsPromoPart.CheckState = CheckState.Indeterminate;
                chkKit.CheckState = CheckState.Indeterminate;
                chkIsAvailableForGift.Enabled = true;
                chkIsAvailableForGift.CheckState = CheckState.Indeterminate;
                chkRegistrationPurpose.CheckState = CheckState.Indeterminate;
                EnableUniqueElements(true);
                m_selectedItemId = Common.INT_DBNULL;
                m_objFrmLocList = null;
                objFrmCompositeItemsList = null;
                objFrmUOM = null;
                m_ItemsList = null;
                m_ItemBarCodeList = null;
                m_objFrmBarCode = null;

                Validators.SetErrorMessage(epItemMaster, cmbSubCategory);
                Validators.SetErrorMessage(epItemMaster, cmbTaxCat);
                Validators.SetErrorMessage(epItemMaster, cmbStatus);
                Validators.SetErrorMessage(epItemMaster, btnCompsiteItems);

                txtCost.Text = "0.00";
                txtLength.Text = "0";
                txtWidth.Text = "0";
                txtHeight.Text = "0";
                txtWeight.Text = "0";
                txtStackLimit.Text = "0";
                txtExpDuration.Text = "0";

                #region Shopping Cart

                txtUSDPrice.Text = string.Empty;
                txtWebDescriptions.Text = string.Empty;
                ProductImage.Image = null;
                this.LoadedImage = "";
                this.Filepath = "";

                #endregion

                cmbExpiryDateFormat.SelectedValue = 1;
                cmbItemPackType.SelectedValue = -1;
                txtItemPackSize.Text = "0";

                m_selectedItemId = Common.INT_DBNULL;
                m_modifiedDate = Common.DATETIME_NULL;
                btnSave.Enabled = IsSaveAvailable;
                btnSearch.Enabled = IsSearchAvailable;

                #region Shopping Cart
                btnBrowse.Enabled = IsBrowseAvailable;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void EnableUniqueElements(Boolean enable)
        {
            try
            {
                txtItemCode.Enabled = enable;
                txtBarcode.Enabled = enable;
                chkIsAvailableForGift.Enabled = enable;
                btnSearch.Enabled = enable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.SuppressKeyPress = true;
            }
        }

        #endregion
        // Item Vendor Code

        #region ItemVendor Tab

        //Initializing all controls in vendor tab
        private void InitailizeItemVendorTab()
        {
            //Bind Status comboboxes
            DataTable datatableStatusVendor = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.STATUS, 0, 0, 0));
            //By Amit - To Remove select from dropdown list. Need to evaluate the option to remove select in the case of STATUS from SP itself
            //datatableStatusVendor.Rows.Remove(datatableStatusVendor.Select("keycode1 = -1")[0]);
            cmbStatusVendor.DataSource = datatableStatusVendor;
            cmbStatusVendor.ValueMember = Common.KEYCODE1;
            cmbStatusVendor.DisplayMember = Common.KEYVALUE1;
            cmbStatusVendor.SelectedValue = 1;

            //Bind Vendor Combobox
            DataTable dataTableVendor = Common.ParameterLookup(Common.ParameterType.ItemVendor, new ParameterFilter(String.Empty, Common.INT_DBNULL, 0, 0));
            cmbVendor.DataSource = dataTableVendor;
            cmbVendor.DisplayMember = ItemVendorDetails.CMB_VENDOR_TEXT;
            cmbVendor.ValueMember = ItemVendorDetails.CMB_VENDOR_VALUE;

            DataGridView dgv1 = Common.GetDataGridViewColumns(dgvVendor, GRIDVIEW_XML_PATH);
            FormatGridView(dgvVendor);

            RefreshVendorItemList();

            txtItemCodeVendor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtItemCodeVendor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            if (_itemcollection != null)
                txtItemCodeVendor.AutoCompleteCustomSource = _itemcollection;

            btnSearchVendor.Enabled = IsSaveAvailable;
            btnSaveVendor.Enabled = IsSearchAvailable;

            ResetVendorControls();
        }
        #region Methods

        bool SaveVendor(ref string errMsg)
        {
            try
            {
                if (m_ItemVendor == null)
                    m_ItemVendor = new ItemVendorDetails();
                m_ItemVendor.CreatedBy = m_userId;
                m_ItemVendor.IsVendorPrimary = Convert.ToInt32(chkPrimVendor.CheckState);
                m_ItemVendor.ItemCode = txtItemCodeVendor.Text.Trim();
                m_ItemVendor.ModifiedBy = m_userId;
                m_ItemVendor.VendorId = Convert.ToInt32(cmbVendor.SelectedValue);
                m_ItemVendor.Status = Convert.ToInt32(cmbStatusVendor.SelectedValue);
                Boolean isSuccess = m_ItemVendor.Save(ref errMsg);
                m_ItemVendor = null;
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ResetVendorControls()
        {
            try
            {
                epVendor.Clear();
                if (m_ItemVendor != null)
                {
                    txtItemCodeVendor.Text = m_ItemVendor.ItemCode;
                    cmbVendor.SelectedValue = m_ItemVendor.VendorId;
                    chkPrimVendor.CheckState = m_ItemVendor.IsVendorPrimary == 1 ? CheckState.Checked : CheckState.Unchecked;
                    cmbStatusVendor.SelectedValue = m_ItemVendor.Status;
                    m_isVendorModified = true;
                    //m_vendorItemCode = txtItemCodeVendor.Text;
                    //m_vendorId = Convert.ToInt32(cmbVendor.SelectedValue);
                    //m_vendorName = cmbVendor.Text;
                    //m_vendorModDate = Convert.ToDateTime(dgvVendor.Rows[rowIndex].Cells["ModifiedDate"].Value);
                    txtItemCodeVendor.Enabled = false;
                    cmbVendor.Enabled = false;

                    btnSearchVendor.Enabled = false;

                    btnSaveVendor.Enabled = IsUpdateAvailable;
                    btnResetVendor.Enabled = true;
                }
                else
                {
                    txtItemCodeVendor.Text = String.Empty;
                    cmbVendor.SelectedIndex = cmbVendor.Items.Count > 0 ? 0 : -1;
                    chkPrimVendor.CheckState = CheckState.Indeterminate;
                    //cmbStatusVendor.SelectedIndex = cmbStatusVendor.Items.Count > 0 ? 0 : -1;
                    cmbStatusVendor.SelectedValue = 1;
                    txtItemCodeVendor.Enabled = true;
                    cmbVendor.Enabled = true;

                    btnSearchVendor.Enabled = IsSearchAvailable;
                    btnSaveVendor.Enabled = IsSaveAvailable;
                    btnResetVendor.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SearchVendor()
        {
            try
            {
                m_isVendorModified = false;
                epVendor.Clear();
                dgvVendor.SelectionChanged -= dgvVendor_SelectionChanged;
                ItemVendorDetails searchVendor = CreateVendorObject();

                NullifyDataGridView(dgvVendor);

                m_selectedItemVendors = searchVendor.Search();
                if (m_selectedItemVendors != null)
                    BindDataGridView(dgvVendor, ListOfDataSources.ListOfVendors);
                else
                {
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dgvVendor.SelectionChanged += new EventHandler(dgvVendor_SelectionChanged);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Reset()
        {
            try
            {
                m_ItemVendor = null;
                ResetVendorControls();
                m_isVendorModified = false;
                NullifyDataGridView(dgvVendor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ItemVendorDetails CreateVendorObject()
        {
            try
            {
                ItemVendorDetails ivd = new ItemVendorDetails();
                ivd.ItemCode = !Validators.CheckForEmptyString(txtItemCodeVendor.Text.Length) ? txtItemCodeVendor.Text : String.Empty;
                ivd.VendorId = !Validators.CheckForSelectedValue(cmbVendor.SelectedIndex) ? Convert.ToInt32(cmbVendor.SelectedValue) : Common.INT_DBNULL;
                ivd.VendorName = !Validators.CheckForSelectedValue(cmbVendor.SelectedIndex) ? cmbVendor.Text : String.Empty;
                ivd.IsVendorPrimary = Convert.ToInt32(chkPrimVendor.CheckState);
                ivd.Status = !Validators.CheckForSelectedValue(cmbStatusVendor.SelectedIndex) ? Convert.ToInt32(cmbStatusVendor.SelectedValue) : Common.INT_DBNULL;
                ivd.StatusName = !Validators.CheckForSelectedValue(cmbStatusVendor.SelectedIndex) ? cmbStatusVendor.Text : String.Empty;


                return ivd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Events

        private void btnSearchVendor_Click(object sender, EventArgs e)
        {
            try
            {
                SearchVendor();
                dgvVendor.ClearSelection();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveVendor_Click(object sender, EventArgs e)
        {
            try
            {
                CallItemVendorValidations();

                string errMessage = GetVendorErrorMessages();
                if (errMessage.Length > 0)
                {
                    MessageBox.Show(errMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                bool IsPrimaryExist = false;

                ItemVendorDetails itemVendor = new ItemVendorDetails();
                itemVendor.ItemCode = txtItemCodeVendor.Text.Trim();
                List<ItemVendorDetails> itemVendorList = itemVendor.Search();
                if (itemVendorList != null && itemVendorList.Count > 0)
                {
                    if (!m_isVendorModified)
                    {
                        //check duplicate record
                        var query = (from I in itemVendorList where I.VendorId == Convert.ToInt32(cmbVendor.SelectedValue) select I.ItemCode);
                        if (query == null || query.ToList<string>().Count > 0)
                        {
                            MessageBox.Show(Common.GetMessage("INF0112"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    //Check Primary Vendor Exists or Not
                    var query1 = (from I in itemVendorList where I.IsVendorPrimary == 1 && I.VendorId != Convert.ToInt32(cmbVendor.SelectedValue) select I.ItemCode);
                    if (query1 != null && query1.ToList<string>().Count > 0)
                    {
                        IsPrimaryExist = true;
                    }
                }
                string errMsg = string.Empty;
                if (IsPrimaryExist && chkPrimVendor.Checked)
                {
                    //confirm t reset existing primary vendr
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5014"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.No)
                        return;
                }
                else if (!IsPrimaryExist && !chkPrimVendor.Checked)
                {
                    // set error Primary vendor should be there
                    MessageBox.Show(Common.GetMessage("INF0113", "Vendor"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (chkPrimVendor.Checked && Convert.ToInt32(cmbStatusVendor.SelectedValue) != 1)
                {
                    MessageBox.Show(Common.GetMessage("INF0114", "Vendor"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string msg = string.Empty;
                if (m_ItemVendor != null && m_ItemVendor.VendorId > 0)
                    msg = "Edit";
                else
                    msg = "Save";
                DialogResult result = MessageBox.Show(Common.GetMessage("5010", msg), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (SaveVendor(ref errMsg))
                    {
                        MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SearchVendor();
                        dgvVendor.ClearSelection();
                        m_ItemVendor = null;
                        ResetVendorControls();
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage(errMsg), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetVendor_Click(object sender, EventArgs e)
        {
            try
            {
                Reset();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVendor_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvVendor.SelectedRows.Count > 0)
                {
                    m_isVendorModified = false;
                    m_ItemVendor = m_selectedItemVendors[dgvVendor.SelectedRows[0].Index];
                    ResetVendorControls();
                    btnSaveVendor.Enabled = false;
                    btnSearchVendor.Enabled = IsSearchAvailable;
                    //SelectVendorDetails(dgvVendor.SelectedRows[0].Index);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvVendor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvVendor.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell))
                {
                    m_isVendorModified = false;
                    m_ItemVendor = m_selectedItemVendors[dgvVendor.SelectedRows[0].Index];
                    ResetVendorControls();
                    btnSaveVendor.Enabled = IsUpdateAvailable;
                    btnSearchVendor.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Validations

        void CallItemVendorValidations()
        {
            try
            {
                // check valid item
                epVendor.Clear();
                CheckItemValid(txtItemCodeVendor, lblItemCodeVendor, epVendor);
                ComboBoxValidations(cmbVendor, lblVendor, epVendor);
                if (chkPrimVendor.CheckState == CheckState.Indeterminate)
                {
                    Validators.SetErrorMessage(epVendor, chkPrimVendor, "VAL0002", lblPrimVendor.Text);
                }
                ComboBoxValidations(cmbStatusVendor, lblStatusVendor, epVendor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        String GetVendorErrorMessages()
        {
            try
            {
                StringBuilder sbError = new StringBuilder();

                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendor, txtItemCodeVendor), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendor, chkPrimVendor), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendor, cmbVendor), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendor, cmbStatusVendor), ref sbError);

                return Common.ReturnErrorMessage(sbError).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region ItemVendorLocation Tab

        private void InitailizeVendorLocationTab()
        {
            try
            {
                //Initialize all Control in Vendor-Location Tab

                DataTable datatableStatusVendorLoc = Common.ParameterLookup(Common.ParameterType.Parameter, new ParameterFilter(Common.STATUS, 0, 0, 0));
                cmbStatusVendorLoc.DataSource = datatableStatusVendorLoc;
                cmbStatusVendorLoc.ValueMember = Common.KEYCODE1;
                cmbStatusVendorLoc.DisplayMember = Common.KEYVALUE1;
                cmbStatusVendorLoc.SelectedValue = 1;
                //Vendor List
                BindVendorList(string.Empty);
                BindLocationList(string.Empty);
                //Bind Vendor-Location Combobox
                //DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("", (int)Common.LocationConfigId.WH, 0, 0));                
                //cmbLocVendorLoc.DataSource = dtLocations;
                //cmbLocVendorLoc.DisplayMember = "LocationName";
                //cmbLocVendorLoc.ValueMember = "LocationId";

                DataGridView dgv2 = Common.GetDataGridViewColumns(dgvVendorLocation, GRIDVIEW_XML_PATH);
                FormatGridView(dgvVendorLocation);

                txtItemCodeVendorLoc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtItemCodeVendorLoc.AutoCompleteSource = AutoCompleteSource.CustomSource;
                if (_itemcollection != null)
                    txtItemCodeVendorLoc.AutoCompleteCustomSource = _itemcollection;

                btnSearchVendorLoc.Enabled = IsSearchAvailable;
                btnItemVendorSearch.Enabled = IsSearchAvailable;
                btnSaveVendorLoc.Enabled = IsSaveAvailable;
                btnItemVendorSave.Enabled = IsSaveAvailable;

                ResetVendorLocationControls();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Methods

        private void BindVendorList(string ItemCode)
        {
            try
            {
                if (ItemCode.Equals(string.Empty))
                {
                    //Bind Vendor Combobox
                    DataTable dTableVendors = Common.ParameterLookup(Common.ParameterType.ItemVendor, new ParameterFilter(String.Empty, Common.INT_DBNULL, 0, 0));
                    cmbVendorLoc.DataSource = dTableVendors;
                    cmbVendorLoc.DisplayMember = ItemVendorDetails.CMB_VENDOR_TEXT;
                    cmbVendorLoc.ValueMember = ItemVendorDetails.CMB_VENDOR_VALUE;
                }
                else
                {
                    cmbVendorLoc.DataSource = null;
                    List<ItemVendor> vnd = (new ItemVendorLocationDetails()).SearchVendors(txtItemCodeVendorLoc.Text);
                    if (vnd != null && vnd.Count > 0)
                    {
                        cmbVendorLoc.DataSource = vnd;
                        cmbVendorLoc.DisplayMember = "VendorName";
                        cmbVendorLoc.ValueMember = "VendorId";
                    }
                    else
                    {
                        cmbVendorLoc.Text = "Select";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindLocationList(string ItemCode)
        {
            try
            {
                if (ItemCode.Equals(string.Empty))
                {
                    //Bind Vendor-Location Combobox
                    cmbLocVendorLoc.DataSource = null;
                    DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.Locations, new ParameterFilter("", -5, 0, 0));
                    cmbLocVendorLoc.DataSource = dtLocations;
                    cmbLocVendorLoc.DisplayMember = "LocationName";
                    cmbLocVendorLoc.ValueMember = "LocationId";
                }
                else
                {
                    cmbLocVendorLoc.DataSource = null;
                    DataTable dtLocations = Common.ParameterLookup(Common.ParameterType.ItemLocations, new ParameterFilter(ItemCode, -5, 0, 0));
                    cmbLocVendorLoc.DataSource = dtLocations;
                    cmbLocVendorLoc.DisplayMember = "LocationName";
                    cmbLocVendorLoc.ValueMember = "LocationId";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ResetVendorLocationControls()
        {
            try
            {
                epVendorLoc.Clear();
                if (m_ItemVendorLoc != null)
                {
                    txtItemCodeVendorLoc.Text = m_ItemVendorLoc.ItemCode;
                    BindVendorList(m_ItemVendorLoc.ItemCode);
                    cmbVendorLoc.SelectedValue = m_ItemVendorLoc.VendorId;
                    cmbLocVendorLoc.SelectedValue = m_ItemVendorLoc.LocationId;
                    txtLeadTime.Text = m_ItemVendorLoc.LeadTime.ToString();
                    txtMinOrder.Text = m_ItemVendorLoc.DisplayMinOrderQuantity.ToString();
                    txtPUF.Text = m_ItemVendorLoc.DisplayPurchaseUnitFactor.ToString();
                    txtLocationCost.Text = m_ItemVendorLoc.DisplayCostForLocation.ToString();
                    cmbStatusVendorLoc.SelectedValue = m_ItemVendorLoc.Status;

                    chkIsPrimaryForLocation.CheckState = m_ItemVendorLoc.IsVendorPrimaryForLocation == 1 ? CheckState.Checked : CheckState.Unchecked;
                    chkIsInclusiveofTax.CheckState = m_ItemVendorLoc.IsInclusiveofTax == 1 ? CheckState.Checked : CheckState.Unchecked;
                    btnSearchVendorLoc.Enabled = false;

                    btnSaveVendorLoc.Enabled = IsUpdateAvailable;
                    btnItemVendorSave.Enabled = IsUpdateAvailable;

                    btnResetVendorLocation.Enabled = true;
                    txtItemCodeVendorLoc.Enabled = false;
                    cmbLocVendorLoc.Enabled = false;
                    cmbVendorLoc.Enabled = false;
                }
                else
                {
                    txtItemCodeVendorLoc.Text = String.Empty;
                    cmbVendorLoc.SelectedIndex = cmbVendorLoc.Items.Count > 0 ? 0 : -1;
                    cmbLocVendorLoc.SelectedIndex = cmbLocVendorLoc.Items.Count > 0 ? 0 : -1;
                    txtLeadTime.Text = "0";
                    txtMinOrder.Text = "0";
                    txtPUF.Text = string.Empty;
                    txtLocationCost.Text = "0";
                    //cmbStatusVendorLoc.SelectedIndex = cmbStatusVendorLoc.Items.Count > 0 ? 0 : -1;
                    cmbStatusVendorLoc.SelectedValue = 1;
                    chkIsPrimaryForLocation.CheckState = CheckState.Indeterminate;
                    chkIsInclusiveofTax.CheckState = CheckState.Indeterminate;
                    btnSearchVendorLoc.Enabled = IsSearchAvailable;
                    btnSaveVendorLoc.Enabled = IsSaveAvailable;
                    btnItemVendorSave.Enabled = IsSaveAvailable;
                    btnResetVendorLocation.Enabled = true;

                    txtItemCodeVendorLoc.Enabled = true;
                    cmbLocVendorLoc.Enabled = true;
                    cmbVendorLoc.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SearchVendorLocation()
        {
            try
            {
                epVendorLoc.Clear();
                dgvVendorLocation.SelectionChanged -= dgvVendorLocation_SelectionChanged;
                ItemVendorLocationDetails ivld = new ItemVendorLocationDetails();
                ivld.ItemCode = txtItemCodeVendorLoc.Text.Trim();
                if (cmbVendorLoc.Enabled == true)
                {
                    ivld.VendorId = Convert.ToInt32(cmbVendorLoc.SelectedValue);
                }
                ivld.LocationId = Convert.ToInt32(cmbLocVendorLoc.SelectedValue);
                ivld.LeadTime = txtLeadTime.Text == string.Empty ? 0 : Convert.ToInt32(txtLeadTime.Text);
                ivld.MinOrderQuantity = txtMinOrder.Text == string.Empty ? 0 : Convert.ToDecimal(txtMinOrder.Text);
                ivld.CostForLocation = txtLocationCost.Text == string.Empty ? 0 : Convert.ToDecimal(txtLocationCost.Text);
                ivld.PurchaseUnitFactor = txtPUF.Text == string.Empty ? 0 : Convert.ToDecimal(txtPUF.Text);
                ivld.IsVendorPrimaryForLocation = Convert.ToInt32(chkIsPrimaryForLocation.CheckState);
                ivld.Status = Convert.ToInt32(cmbStatusVendorLoc.SelectedValue);
                ivld.IsInclusiveofTax = Convert.ToInt32(chkIsInclusiveofTax.CheckState);
                NullifyDataGridView(dgvVendorLocation);

                m_selectedVendorLoc = ivld.Search();
                if (m_selectedVendorLoc != null)
                    BindDataGridView(dgvVendorLocation, ListOfDataSources.ListOfVendorLoc);
                else
                {
                    MessageBox.Show(Common.GetMessage("8002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dgvVendorLocation.SelectionChanged += new EventHandler(dgvVendorLocation_SelectionChanged);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ResetVendorLocation()
        {
            try
            {
                m_ItemVendorLoc = null;
                ResetVendorLocationControls();
                m_isVendorLocModified = false;
                NullifyDataGridView(dgvVendorLocation);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool SaveVendorLocation(ref string errMsg)
        {
            try
            {

                if (m_ItemVendorLoc == null)
                    m_ItemVendorLoc = new ItemVendorLocationDetails();

                m_ItemVendorLoc.CostForLocation = Validators.CheckForDBNull(txtLocationCost.Text, Convert.ToDecimal(Common.INT_DBNULL));
                m_ItemVendorLoc.CreatedBy = m_userId;
                m_ItemVendorLoc.VendorId = Convert.ToInt32(cmbVendorLoc.SelectedValue);
                m_ItemVendorLoc.IsVendorPrimaryForLocation = Convert.ToInt32(chkIsPrimaryForLocation.CheckState);
                m_ItemVendorLoc.ItemCode = txtItemCodeVendorLoc.Text.Trim();
                m_ItemVendorLoc.LeadTime = Validators.CheckForDBNull(txtLeadTime.Text, Convert.ToInt32(Common.INT_DBNULL));
                m_ItemVendorLoc.LocationId = Convert.ToInt32(cmbLocVendorLoc.SelectedValue);
                m_ItemVendorLoc.MinOrderQuantity = Validators.CheckForDBNull(txtMinOrder.Text, Convert.ToDecimal(Common.INT_DBNULL));
                m_ItemVendorLoc.ModifiedBy = m_userId;
                m_ItemVendorLoc.PurchaseUnitFactor = Validators.CheckForDBNull(txtPUF.Text, Convert.ToDecimal(Common.INT_DBNULL));
                m_ItemVendorLoc.Status = Validators.CheckForDBNull(cmbStatusVendorLoc.SelectedValue, Convert.ToInt32(Common.INT_DBNULL));
                m_ItemVendorLoc.IsInclusiveofTax = Convert.ToInt32(chkIsInclusiveofTax.CheckState);
                String errorMessage = String.Empty;
                bool isSuccess = m_ItemVendorLoc.Save(ref errorMessage);

                errMsg = errorMessage;

                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Events

        private void btnSearchVendorLoc_Click(object sender, EventArgs e)
        {
            try
            {
                SearchVendorLocation();
                dgvVendorLocation.ClearSelection();
                ResetVendorLocationControls();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnSaveVendorLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CallItemVendorLocationValidations();

                string errMessage = GetVendorLocationErrorMessage();
                if (errMessage.Length > 0)
                {
                    MessageBox.Show(errMessage, Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                bool IsPrimaryCostExist = false;

                ItemVendorLocationDetails item = new ItemVendorLocationDetails();
                item.ItemCode = txtItemCodeVendorLoc.Text.Trim();
                List<ItemVendorLocationDetails> itemVendorLocationList = item.Search();
                if (itemVendorLocationList != null && itemVendorLocationList.Count > 0)
                {
                    if (!m_isVendorLocModified)
                    {
                        //check duplicate record
                        var query = (from I in itemVendorLocationList where I.VendorId == Convert.ToInt32(cmbVendorLoc.SelectedValue) && I.LocationId == Convert.ToInt32(cmbLocVendorLoc.SelectedValue) select I.ItemCode);
                        if (query == null || query.ToList<string>().Count > 0)
                        {
                            MessageBox.Show(Common.GetMessage("INF0112"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    //Check Primary Vendor Exists or Not
                    var query1 = (from I in itemVendorLocationList where I.IsVendorPrimaryForLocation == 1 && I.VendorId == Convert.ToInt32(cmbVendorLoc.SelectedValue) && I.LocationId != Convert.ToInt32(cmbLocVendorLoc.SelectedValue) select I.ItemCode);
                    if (query1 != null && query1.ToList<string>().Count > 0)
                    {
                        IsPrimaryCostExist = true;
                    }
                }
                if (IsPrimaryCostExist && chkIsPrimaryForLocation.Checked)
                {
                    //confirm t reset existing primary vendr
                    DialogResult saveResult = MessageBox.Show(Common.GetMessage("5014"), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (saveResult == DialogResult.No)
                        return;
                }
                else if (!IsPrimaryCostExist && !chkIsPrimaryForLocation.Checked)
                {
                    // set error Primary vendor should be there
                    MessageBox.Show(Common.GetMessage("INF0113", "Cost"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (chkIsPrimaryForLocation.Checked && Convert.ToInt32(cmbStatusVendorLoc.SelectedValue) != 1)
                {
                    MessageBox.Show(Common.GetMessage("INF0114", "Cost"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string errMsg = string.Empty;
                string msg = string.Empty;
                if (m_ItemVendorLoc != null && m_ItemVendorLoc.VendorId > 0)
                    msg = "Edit";
                else
                    msg = "Save";
                DialogResult result = MessageBox.Show(Common.GetMessage("5010", msg), Common.GetMessage("10001"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (SaveVendorLocation(ref errMsg))
                    {
                        MessageBox.Show(Common.GetMessage("8001"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SearchVendorLocation();
                        dgvVendorLocation.ClearSelection();
                        m_ItemVendorLoc = null;
                        ResetVendorLocationControls();
                    }
                    else
                    {
                        MessageBox.Show(Common.GetMessage(errMsg), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnResetVendorLocation_Click(object sender, EventArgs e)
        {
            try
            {
                ResetVendorLocation();
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtItemCodeVendorLoc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!Validators.CheckForEmptyString(txtItemCodeVendorLoc.Text.Length))
                {
                    BindVendorList(txtItemCodeVendorLoc.Text);
                    BindLocationList(txtItemCodeVendorLoc.Text);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVendorLocation_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvVendorLocation.SelectedRows.Count > 0)
                {
                    m_isVendorLocModified = false;
                    m_ItemVendorLoc = m_selectedVendorLoc[dgvVendorLocation.SelectedRows[0].Index];
                    ResetVendorLocationControls();
                    btnSaveVendorLoc.Enabled = false;
                    btnItemVendorSave.Enabled = false;
                    btnSearchVendorLoc.Enabled = IsSearchAvailable;
                    btnItemVendorSearch.Enabled = IsSearchAvailable;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVendorLocation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvVendorLocation.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewImageCell))
                {
                    m_isVendorLocModified = true;
                    m_ItemVendorLoc = m_selectedVendorLoc[dgvVendorLocation.SelectedRows[0].Index];
                    ResetVendorLocationControls();
                    btnSaveVendorLoc.Enabled = IsUpdateAvailable;
                    btnItemVendorSave.Enabled = IsUpdateAvailable;
                    btnSearchVendorLoc.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Validations

        void CallItemVendorLocationValidations()
        {
            try
            {
                epVendorLoc.Clear();
                TextBoxValidations(txtItemCodeVendorLoc, lblItemCodeVendorLoc, epVendorLoc);
                //validate item code
                CheckItemValid(txtItemCodeVendorLoc, lblItemCodeVendorLoc, epVendorLoc);
                ComboBoxValidations(cmbVendorLoc, lblVendorLoc, epVendorLoc);
                ComboBoxValidations(cmbLocVendorLoc, lblLocationVendorLoc, epVendorLoc);
                if (!Validators.IsInt32(txtLeadTime.Text.Trim()))
                    Validators.SetErrorMessage(epVendorLoc, txtLeadTime, "VAL0009", lblLeadTime.Text);
                else if (!Validators.IsGreaterThanZero(txtLeadTime.Text.Trim()))
                    Validators.SetErrorMessage(epVendorLoc, txtLeadTime, "VAL0033", lblLeadTime.Text);

                if (!Validators.IsValidQuantity(txtMinOrder.Text.Trim()))
                    Validators.SetErrorMessage(epVendorLoc, txtMinOrder, "VAL0009", lblMinOrder.Text);
                else if (!Validators.IsGreaterThanZero(txtMinOrder.Text.Trim()))
                    Validators.SetErrorMessage(epVendorLoc, txtMinOrder, "VAL0033", lblMinOrder.Text);

                if (!Validators.IsValidQuantity(txtPUF.Text.Trim()))
                    Validators.SetErrorMessage(epVendorLoc, txtPUF, "VAL0009", lblPUF.Text);
                else if (!Validators.IsGreaterThanZero(txtPUF.Text.Trim()))
                    Validators.SetErrorMessage(epVendorLoc, txtPUF, "VAL0033", lblPUF.Text);


                if (!Validators.IsValidAmount(txtLocationCost.Text.Trim()))
                    Validators.SetErrorMessage(epVendorLoc, txtLocationCost, "VAL0009", lblLocationCost.Text);
                else if (!Validators.IsGreaterThanZero(txtLocationCost.Text.Trim()))
                    Validators.SetErrorMessage(epVendorLoc, txtLocationCost, "VAL0033", lblLocationCost.Text);

                ComboBoxValidations(cmbStatusVendorLoc, lblStatusVendorLoc, epVendorLoc);
                if (epVendorLoc.GetError(txtPUF).Trim().Length == 0 && epVendorLoc.GetError(txtMinOrder).Trim().Length == 0)
                {
                    if (Convert.ToDecimal(txtPUF.Text.Trim()) > Convert.ToDecimal(txtMinOrder.Text.Trim()))
                    {
                        epVendorLoc.SetError(txtMinOrder, Common.GetMessage("VAL0047", "MinOrder", "Purchase Unit Factor"));
                    }
                    else if (Convert.ToDecimal(txtMinOrder.Text.Trim()) % Convert.ToDecimal(txtPUF.Text.Trim()) > 0)
                    {
                        epVendorLoc.SetError(txtMinOrder, Common.GetMessage("INF0201"));
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        String GetVendorLocationErrorMessage()
        {
            try
            {
                StringBuilder sbError = new StringBuilder();

                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendorLoc, txtItemCodeVendorLoc), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendorLoc, cmbVendorLoc), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendorLoc, cmbLocVendorLoc), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendorLoc, txtLeadTime), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendorLoc, txtMinOrder), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendorLoc, txtPUF), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendorLoc, txtLocationCost), ref sbError);
                Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendorLoc, cmbStatusVendorLoc), ref sbError);

                if (chkIsPrimaryForLocation.CheckState == CheckState.Indeterminate)
                {
                    Validators.SetErrorMessage(epVendorLoc, chkIsPrimaryForLocation, "VAL0002", "Is Primary for Location :");//lblIsPRimaryForLocation.Text);
                    Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendorLoc, chkIsPrimaryForLocation), ref sbError);
                }
                else
                {
                    Validators.SetErrorMessage(epVendorLoc, chkIsPrimaryForLocation);
                }

                if (chkIsInclusiveofTax.CheckState == CheckState.Indeterminate)
                {
                    Validators.SetErrorMessage(epVendorLoc, chkIsInclusiveofTax, "VAL0002", "Is inclusive of Tax :");//lblIsPRimaryForLocation.Text);
                    Validators.AppendToStringBuilder(Validators.GetErrorMessage(epVendorLoc, chkIsInclusiveofTax), ref sbError);
                }
                else
                {
                    Validators.SetErrorMessage(epVendorLoc, chkIsPrimaryForLocation);
                }

                return Common.ReturnErrorMessage(sbError).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


        #endregion


        private void CheckItemValid(TextBox txt, Label lbl, ErrorProvider ep)
        {
            try
            {
                ep.SetError(txt, string.Empty);
                if (Validators.CheckForEmptyString(txt.Text.Length))
                {
                    Validators.SetErrorMessage(ep, txt, "VAL0001", lbl.Text);
                }
                else
                {
                    RefreshVendorItemList();
                    if (ItemList != null)
                    {
                        var query = (from I in ItemList where I.ItemCode.ToUpper().Trim() == txt.Text.ToUpper().Trim() select I.ItemCode);
                        if (query == null || query.ToList<string>().Count == 0)
                        {
                            ep.SetError(txt, Common.GetMessage("INF0010", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));

                        }
                    }
                    else
                    {
                        ep.SetError(txt, Common.GetMessage("INF0010", lbl.Text.Trim().Substring(0, lbl.Text.Trim().Length - 2)));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RefreshVendorItemList()
        {
            try
            {
                _itemcollection = new AutoCompleteStringCollection();
                DataTable dtStatus = Common.ParameterLookup(Common.ParameterType.ActiveItem, new ParameterFilter(string.Empty, 0, 0, 0));
                if (dtStatus != null)
                {
                    ItemList = new List<ItemDetails>();
                    for (int i = 0; i < dtStatus.Rows.Count; i++)
                    {
                        ItemDetails item = new ItemDetails();
                        item.ItemCode = Convert.ToString(dtStatus.Rows[i]["ItemCode"]);
                        item.ItemId = Convert.ToInt32(dtStatus.Rows[i]["ItemId"]);
                        ItemList.Add(item);
                        _itemcollection.Add(Convert.ToString(dtStatus.Rows[i]["ItemCode"]));
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            RefreshVendorItemList();
            txtItemCodeVendor.AutoCompleteCustomSource = _itemcollection;
            txtItemCodeVendorLoc.AutoCompleteCustomSource = _itemcollection;

        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            RefreshVendorItemList();
            txtItemCodeVendor.AutoCompleteCustomSource = _itemcollection;
            txtItemCodeVendorLoc.AutoCompleteCustomSource = _itemcollection;
        }

        private void txtItemCodeVendor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == Common.F4KEY && !e.Alt)
                {
                    NameValueCollection _collection = new NameValueCollection();
                    _collection.Add("LocationId", Common.CurrentLocationId.ToString());
                    CoreComponent.Controls.frmSearch _frmSearch = new CoreComponent.Controls.frmSearch(CoreComponent.Controls.SearchTypes.Item, _collection);
                    //CoreComponent.MasterData.BusinessObjects.ItemDetails _Item = (CoreComponent.MasterData.BusinessObjects.ItemDetails)_frmSearch.ReturnObject;
                    _frmSearch.ShowDialog();
                    //_frmSearch.MdiParent = this.MdiParent;
                    CoreComponent.MasterData.BusinessObjects.ItemDetails _Item = (CoreComponent.MasterData.BusinessObjects.ItemDetails)_frmSearch.ReturnObject;
                    if (_Item != null)
                    {
                        txtItemCodeVendor.Text = _Item.ItemCode.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(ex.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemCodeVendorLoc_KeyDown(object sender, KeyEventArgs e)
        {
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
                        txtItemCodeVendorLoc.Text = _Item.ItemCode.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(ex.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ValidateExpDuration(TextBox txt, string labelText)
        {
            epItemMaster.SetError(txt, string.Empty);
            try
            {
                if (Validators.CheckForEmptyString(txt.Text.Length))
                    Validators.SetErrorMessage(epItemMaster, txt, "VAL0001", labelText);
                else if (!Validators.IsInt16(txt.Text))
                    Validators.SetErrorMessage(epItemMaster, txt, "VAL0009", labelText);
                else
                {
                    bool isGreater = Validators.IsGreaterThanZero(txt.Text.Trim());
                    if (!isGreater)
                    {
                        Validators.SetErrorMessage(epItemMaster, txt, "VAL0033", labelText);
                    }
                    else
                    {
                        Validators.SetErrorMessage(epItemMaster, txt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void txtExpDuration_Validated(object sender, EventArgs e)
        {
            try
            {

                // ValidateExpDuration(txtExpDuration, lblExpDuration.Text.Trim().Substring(0, lblExpDuration.Text.Length - 2));
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(ex.ToString(), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDP_Validated(object sender, EventArgs e)
        {
            //txtItemPrice.Text = txtDP.Text;
            txtItemPrice.Text = "0";
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    var FD = new System.Windows.Forms.OpenFileDialog();
        //    if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        this.ProductImage.Load(FD.FileName);
        //        Filepath = FD.FileName;
        //    }

        //}

        private void pnlSearchHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        #region Shopping Cart
        private void btnBrowse_Click(object sender, EventArgs e)
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
                    this.ProductImage.Load(FD.FileName);
                    Filepath = FD.FileName;
                }
                Environment.CurrentDirectory = CurrentEnvironmentPath;
            }
            catch (Exception)
            {
                MessageBox.Show(Common.GetMessage("VAL0137"), "Product Image", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        #endregion

        private void txtPV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBV_Leave(object sender, EventArgs e)
        {
            try
            {
                epItemMaster.SetError(txtBV, string.Empty);
                if (Validators.IsValidAmount(txtBV.Text.Trim()))
                {
                    decimal bvalue = Convert.ToDecimal(txtBV.Text);
                    txtPV.Text = Convert.ToString(bvalue / 16);
                }
                else
                {
                    epItemMaster.SetError(txtBV, Common.GetMessage("VAL0009", "Business Volume"));
                }

            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            txtDisplayName.Text = txtItemName.Text ;
            txtPrintName.Text = txtItemName.Text;
            txtShortName.Text = txtItemName.Text;
            txtReceiptName.Text = txtItemName.Text;
        }


    }
}
