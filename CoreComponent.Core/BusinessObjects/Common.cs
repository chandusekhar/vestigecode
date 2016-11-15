using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Resources;
using System.Xml;
using System.Xml.Serialization;
using Vinculum.Framework.Logging;
using System.Windows.Forms;
using System.Configuration;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace CoreComponent.Core.BusinessObjects
{
    /*
    ------------------------------------------------------------------------
    Created by			    :	Amit Bansal
    Created Date		    :	09/June/2009
    Purpose				    :	This class contains all the common functions and constants 
                                used in the application.
    Modified by			    :	Ajay Kumar Singh
    Date of Modification    :	09/June/2009
    Purpose of Modification	:	Added 'GetMessage()' method to retrieve all messages from common XML file.
    ------------------------------------------------------------------------    
    */
    /// <summary>
    /// Use for filter parameter 
    /// </summary>
    #region Structure Parameter Filter
    [Serializable]
    public struct ParameterFilter
    {
        private string code;
        public int Key01, Key02, Key03;

        public ParameterFilter(string code, int key1, int key2, int key3)
        {
            this.code = code;
            this.Key01 = key1;
            this.Key02 = key2;
            this.Key03 = key3;
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

    }
    #endregion

    public class State
    {
        public string StateName
        {
            get;
            set;
        }

        public int CountryId
        {
            get;
            set;
        }

        public string CountryName
        {
            get;
            set;
        }

        public int StateId
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }
        public string StatusName
        {
            get;
            set;
        }


    }

    /// <summary>
    /// Class for all common and utility functions.
    /// CONSTANTS will also be defined in the same class.
    /// </summary>
    public static class Common
    {
        public static bool CheckIfDistributorAddHidden(int distributorID)
        {
            DataTable dtLocation = Common.ParameterLookup(Common.ParameterType.IsMiniBranch, new ParameterFilter(Common.LocationCode, distributorID, 0, 0));
            try
            {
                if (dtLocation != null && dtLocation.Rows.Count > 0 && dtLocation.Rows[0].ItemArray[0] != null && dtLocation.Rows[0].ItemArray[0].ToString() == "1")
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static DGVColumnHeader CreateCheckBoxColumn(DataGridView dgvData, string sText,string sName)
        {
            DGVColumnHeader dgvColumnHeader = null;
            dgvColumnHeader = new DGVColumnHeader();
            //Add columns dynamically  to gridview
            dgvData.Columns.RemoveAt(0);
            dgvData.Columns.Insert(0, new DataGridViewCheckBoxColumn());
            dgvData.Columns[0].HeaderCell = dgvColumnHeader;
            dgvData.Columns[0].Name = sName;
            dgvData.Columns[0].HeaderText = "        " + sText;
            dgvData.AutoGenerateColumns = false;
            return dgvColumnHeader;
            
        }
        
        private static void SetAppSetting(string key, string value)
        {
            string configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;

            try
            {
                XmlDataDocument config = new XmlDataDocument();
                config.Load(configFile);

                XmlNodeList nl = config.GetElementsByTagName("appSettings")[0].ChildNodes;
                for (int index = 0; index < nl.Count; index++)
                {
                    if (nl[index].Attributes["key"].Value.ToLower().CompareTo(key.Trim().ToLower()) == 0)
                        nl[index].Attributes["value"].Value = value;
                }
                config.Save(configFile);
            }
            catch (IOException) { }
            ConfigurationManager.RefreshSection("appSettings");
        }

        #region POS Configurations

        private const string ReceiptFormat1Setting = "ReceiptFormat1";
        private const string ReceiptFormat2Setting = "ReceiptFormat2";
        private const string TaxRegistrationNo = "TaxRegistrationNo";
        private const string ShopNameSetting = "ShopName";
        private const string ShopName02Setting = "ShopName02";
        private const string Address01Setting = "Address01";
        private const string Address02Setting = "Address02";
        private const string ContactSetting = "Contact";
        private const string PrintCopySetting = "PrintCopy";
        private const string Contact2Setting = "Contact2";
        private const string Footer1Setting = "Footer1";
        private const string Footer2Setting = "Footer2";

        public static string ReceiptFormat1
        {
            get { return ConfigurationManager.AppSettings[ReceiptFormat1Setting]; }
            set { SetAppSetting(ReceiptFormat1Setting, value.Trim()); }
        }

        public static string ReceiptFormat2
        {
            get { return ConfigurationManager.AppSettings[ReceiptFormat2Setting]; }
            set { SetAppSetting(ReceiptFormat2Setting, value.Trim()); }
        }

        public static string TaxNumber
        {
            get { return ConfigurationManager.AppSettings[TaxRegistrationNo]; }
            set { SetAppSetting(TaxRegistrationNo, value.Trim()); }
        }

        public static string ShopName
        {
            get { return ConfigurationManager.AppSettings[ShopNameSetting]; }
            set { SetAppSetting(ShopNameSetting, value.Trim()); }
        }

        public static string ShopName02
        {
            get { return ConfigurationManager.AppSettings[ShopName02Setting]; }
            set { SetAppSetting(ShopName02Setting, value.Trim()); }
        }

        public static string Address01
        {
            get { return ConfigurationManager.AppSettings[Address01Setting]; }
            set { SetAppSetting(Address01Setting, value.Trim()); }
        }

        public static string Address02
        {
            get { return ConfigurationManager.AppSettings[Address02Setting]; }
            set { SetAppSetting(Address02Setting, value.Trim()); }
        }

        public static string Contact
        {
            get { return ConfigurationManager.AppSettings[ContactSetting]; }
            set { SetAppSetting(ContactSetting, value.Trim()); }
        }

        public static string Contact2
        {
            get { return ConfigurationManager.AppSettings[Contact2Setting]; }
            set { SetAppSetting(Contact2Setting, value.Trim()); }
        }

        public static string Footer1
        {
            get { return ConfigurationManager.AppSettings[Footer1Setting]; }
            set { SetAppSetting(Footer1Setting, value.Trim()); }
        }

        public static string Footer2
        {
            get { return ConfigurationManager.AppSettings[Footer2Setting]; }
            set { SetAppSetting(Footer2Setting, value.Trim()); }
        }

        public static int PrintCopy
        {
            get
            {
                int val = 1;
                return int.TryParse(ConfigurationManager.AppSettings[PrintCopySetting], out val) ? val <= 0 ? 1 : val : 1;
            }
            set { SetAppSetting(PrintCopySetting, value.ToString()); }
        }

        //public static string HalfPageSizePrintConstants
        //{
        //    get { return ConfigurationManager.AppSettings[HalfPageSizePrintConfig]; }
            
        //}


        #endregion

        #region POS Types

        //AKASH
        public static bool CheckedReportLayout
        {
            set;
            get;
        }

        public enum GRNInvoiceType
        {
            GRN = 1,
            Invoice = 2
        }
        public enum DistributorStatusenum
        {
            Register = 1,
            Activated = 2,
            Terminated = 3
        }

        public enum TaxType
        {
            SOTAX,
            POTAX,
            TOTAX
        }
        public enum CourierAddressType
        {
            PC=1,
            Distributor=2,
            Other=3
        }
        public enum ScreenMode
        {
            NewOrder,
            KitOrder,
            OrderInMemory,
            OrderSaved,
            OrderCancelled,
            OrderConfirmed,
            Invoiced,
            InvoicePrinted,
            InvoiceCancelled,
            LoggedIn,
            LoggedOut,
            DistributorAdded,
            OrderModify
        }

        public enum OrderStatus
        {
            Created = 1,
            Cancelled = 2,
            Confirmed = 3,
            Invoiced = 4,
            InvoiceCancelled = 5,
            Modify = 6
        }

        public enum OrderType
        {
            FirstOrder = 1,
            Reorder = 2,
            KitOrder = 3
        }
        public enum enuschedularstatus
        {
            FTPService = 1,
            BackUpSol  = 2,
            UpdateCompare = 3
        }

        public enum PopUp
        {
            Cheque = 1,
            CreditCard = 2,
            LocalCurrency = 3,
            EPS = 4,
            ForexConversion = 5,
            BonusCheque = 6,
            Bank=7
        }

        public enum CreditCardType
        {
            MasterCard,
            BankCard,
            Visa,
            Amex,
            Discover,
            DinersClub,
            JCB,
            Unknown
        }

        public enum PaymentMode
        {           
            Cash=1,
            CreditCard=2,
            Forex=3,           
            Cheque=4,
            BonusCheque=5,
            Bank = 6 

        }
        public enum DeliveryMode
        {
            Self= 1,
            Courier = 2            
        }


        [FlagsAttribute]
        public enum CreditCardInformation
        {
            CardType = 1,
            CustomerName = 2,
            CardNumber = 4,
            ExpiryDate = 8
        }
        public enum LogStatus
        {
            Open = 1,
            Closed = 2,
        }
        public enum COLogType
        {
            Log = 1,
            TeamOrder = 2
        }

        public static int BOId
        {
            get;
            set;
        }

        public static int PCId
        {
            get;
            set;
        }

        public static Address DeliverToAddress
        {
            get;
            set;
        }

        public static Address DeliverFromAddress
        {
            get;
            set;
        }

        public static PrintPageSize PageSize
        {
            get;
            set;
        }


        #endregion

        #region Enums

        public enum ApplicationType
        {
            BOS = 1,
            POS = 2
        }

        public enum InvoiceGRNType
        {
            Invoice = 1,
            GRN = 2
        }

        public static string CountryID
        {
            set;
            get;
        }
        public enum SchedularStatus
        {
            FTPService = 1,
            BackUpSolution = 2,
            UpdateCompare = 3
        }
        public enum PrintPageSize : int
        {
            Half = 550,
            Full = 1100
        }

        #region ParameterType
        public enum ParameterType
        {
            Parameter = 1,
            TaxJurisdiction = 2,
            Country = 3,
            State = 4,
            City = 5,
            Item = 6,
            UOM = 7,
            Distributor = 8,
            Locations = 9,
            SubCategory = 10,
            TaxCategory = 11,
            ItemVendor = 12,
            BucketItemLocation = 13,
            Vendor = 14,
            AllLocations = 15,
            ItemCode = 16,
            Users = 17,
            ItemGroup = 18,
            InventoryBucketBatchLocation = 19,
            Module = 20,
            function = 21,
            MenuName = 22,
            ItemGiftVoucher = 23,
            TaxCode = 24,
            TaxAppliedOn = 25,
            Zone = 26,
            SubZone = 27,
            Area = 28,
            POSLocations = 29,
            LocationAddress = 30,
            AllSubBuckets = 31,
            ItemsAvailableForPromotion = 32,
            VendorLocation = 33,
            ActiveItem = 34,
            ItemBySubCategory = 35,
            AllBucketReport = 36,
            AllMonths = 37,
            YearsForInvoice = 38,
            BOandPCLocations = 39,
            ItemLocations = 40,
            VendorsByLocation = 41,
            FirstAmendmentQuantity = 42,
            ItemGroupsOfNonPromoItem = 43,
            InterfaceProcess = 44,
            InterfacePushTypes = 45,
            InterfaceAction = 46,
            WhgBoPucLocations = 47,
            LocationsWithAllStatus = 48,
            DistributorMonthEnd = 49,
            RptLocationByType = 50,
            RptBOWithPC = 51,
            RptLocationBOAndWH = 52,
            RptPCunderCurrentBO = 53,
            RptDynamicLocationByType = 54,
            RptDynamicPCLocation = 55,
            RptDynamicBOwithPCLocation = 56,
            LogType = 57,
            GetUserNamebyId = 58,
            TaxZones = 59,
            GetTaxJurisdictionByLocId = 60,
            PUCDetails = 61,
            RptUserNameLocationwise = 62,
            BOwithPUC = 63,
            FinancialYears = 64,
            LogPayments = 65,
            DistributorName =66,
            CenterInfo =67,
            InvoiceRpt =68,
            InvoiceReturn = 69,
            KitInvoiceRpt = 70,
            KitInvoiceRptern = 71,
            PriceMode = 72,
            InvoiceLayoutRpt = 73,
            IsMiniBranch = 74,
            HalfPagePrintSizeConstants = 75,
            DistributorLevel = 76,
            TotalLocations = 77,
            DistPaymentMode = 78,
            YearsForBonus = 79,
            PreCarriageBy = 80,
            CourierRequired = 81,
            GIFTVOUCHERDISCVALUE=82,
            WaiveoffCourierLimit = 83,
            CourierAmount = 84,
            RPTDISTACCEPTREJECT = 85,
            ISMINBRANCH = 86,
            MiniLocation=87,
            RptDynamicState=88,
            InclExclDlcp =89,
            RptStatewiseBO=90
        }
        #endregion

        #region StockStatus
        public enum CustomerReturnStatus
        {
            New = -1,
            Created = 1,
            Cancelled = 2,
            Approved = 3
        }
        #endregion

        #region StockStatus
        public enum StockStatus
        {
            New = -1,
            Created = 1,
            Cancelled = 2,
            Initiated = 3,
            Processed = 4,
            Executed = 5,
            Closed = 6
        }
        #endregion

        #region InventoryStatus
        public enum InventoryStatus
        {
            New = -1,
            Created = 1,
            Initiated = 2,
            Approved = 3,
            Rejected = 4,
            Cancelled = 5,
        }
        #endregion

        #region LocationOrgLevel
        public enum LocationOrgLevel
        {
            WHZone = 3,
            BOArea = 5
        }
        #endregion

        #region Location
        public enum LocationConfigId
        {
            HO = 1,
            WH = 2,
            BO = 3,
            PC = 4
        }
        #endregion

        #region IndentType
        public enum IndentType
        {
            MANUAL = 1,
            SUGGESTED = 2
        }
        #endregion

        #region "Indent Consolidation Detail Record Type"
        public enum IndentConsolidationRecordType
        {
            PO = 1,
            TOI = 2
        }
        #endregion

        #region IndentStatus
        public enum IndentStatus
        {
            New = -1,
            Created = 0,
            Confirmed = 1,
            Cancelled = 2,
            Rejected = 3,
            Approved = 4,
            InConsolidation = 5,
            ConsolidationComplete = 6,
            Closed = 7
        }
        #endregion

        #region TOIStatus
        public enum TOIStatus
        {
            New = -1,
            Created = 1,
            Confirmed = 2,
            Cancelled = 3,
            Rejected = 4,
            Closed = 5
        }
        #endregion

        #region TOStatus
        public enum TOStatus
        {
            New = -1,
            Created = 1,
            Shipped = 2,
            Closed = 3
        }
        #endregion

        #region RTV Status
        public enum RTVStatus
        {
            New = -1,            
            Created = 1,
            Cancelled = 2,
            Confirmed = 3,
            Approved = 4,
            Shipped = 5
        }
        #endregion
        #region TIStatus
        public enum TIStatus
        {
            New = -1,
            Created = 1,
            Confirmed = 2
        }
        #endregion

        #region IndentConsolidation Enum
        public enum IndentConsolidationSource
        {
            Manual = 1,
            Auto = 2
        }

        public enum IndentConsolidationState
        {
            Cancelled = 2,
            InConsolidation = 5,
            ConsolidationComplete = 6
        }

        #endregion

        #region POStatus
        public enum POStatus
        {
            New = -1,
            Created = 0,
            Confirmed = 1,
            Cancelled = 2,
            Recieved = 3,
            ShortClosed = 4,
            Closed = 5
        }
        #endregion

        #region DistributorStatus
        public enum DistributorStatus
        {
            Created = 1,
            Activated = 2,
            Cancel = 3
        }
        #endregion

        #region POFormType
        public enum POFormType
        {
            PO = 0,
            Amendment = 1
        }
        #endregion

        #region POtype
        public enum POType
        {
            ADHOC = 1,
            INDENTISED = 2
        }
        #endregion

        #region GRNStatus
        public enum GRNStatus
        {
            New = 0,
            Created = 1,
            Cancelled = 2,
            Closed = 3
        }
        #endregion

        #region ORGSTATE
        public enum ORGSTATE
        {
            HO = 1,
            ZONE = 2,
            SUBZONE = 3,
            AREA = 4
        }
        #endregion

        #region ReportType
        public enum ReportType
        {
            GRN = 1,
            TOI = 2,
            TO = 3,
            TI = 4,
            PO = 5,
            ManualIndent = 6,
            StockCount = 7,
            RTV = 8,
            StockAdjustment = 9,
            PackUnpack = 10,
            SuggestedIndent = 11,
            CR = 12,
            CRCreditNote = 13,
            RTVDebitNote = 14,
            CustomerInvoice = 15,
            CustomerOrder  = 16,
            CustomerInvoiceN = 17,
            ICMP = 18,// ItemCode Mapping
            BonusStatementDirectors = 19,
            CarBonusReport = 20,
            TravelFundReport = 21,
            TOExportInvoice = 22, //Export Invoice 
            EOI = 23,
            TOExportPackingList = 24,
            DistributorDetail = 25
        }
        #endregion

        #region ReportPath
        public enum ReportPath
        {
            GRNScreen = 1,
            TOIScreen = 2,
            TOScreen = 3,
            TIScreen = 4,
            POScreen = 5,
            IndentScreen = 6,
            StockCountScreen = 7,
            RTVScreen = 8,
            InventoryAdjScreen = 9,
            PackUnpackScreen = 10,
            SuggestedIndentScreen = 11,
            CRScreen = 12,
            CRCreditNoteScreen = 13,
            RTVDebitNoteScreen = 14,
            CustomerInvoiceScreen = 15,
            CustomerOrderScreen = 16,
            CustomerInvoiceScreenN = 17,
            ICMP = 18,// ItemCode Mapping
            TOExportInvoice = 22,
            EOIScreen = 23,
            TOExportPackingList =24,
            DistributorDetail=25
        }
        #endregion

        public enum CustomerType
        {
            Distributor = 1,
            PC = 2,
            Sale = 3,
            InvoiceReturn = 4
        }

        public enum InventoryReportType
        {
            TO = 1,
            Receiving = 2
        }

        public enum StockSummaryReportType
        {
            AllStockSummary = 1,
            WHStockSummary = 2,
            BOStockSummary = 3
        }

        #region SelectiveInterfacePush

        public enum InterfaceAction
        {
            Insert = 1,
            Update = 2
        }

        #endregion

        #region BonusChequeStatus

        public enum BonusChequeStatus
        {
            New = 1,
            PartialUsed = 2,
            FullyUsed = 3
        }

        #endregion

        #region PrintType

        public enum PrintType
        {
            PrintOrder = 1,
            PrintInvoice = 2
        }

        #endregion



        #region CommonStatusText
        public enum CommonStatusText
        {
            New = -1,
            Create = 1,
            Cancel = 2,
            Initiate = 3,
            Process = 4,
            Execute = 5,
            Close = 6,
            Confirm = 7,
            Approve = 8,
            Reject = 9,
            Inconsolidation = 10,
            ConsolidationCompleted = 11,
            Ship = 12,
            Recieve = 13,
            ShortClose = 14,
            Activate = 15
        }
        #endregion

        #region DistributorRejectFlag
        public enum DistRejectType
        { 
            None=1,
            Pan=2,
            Bank=3,
            Both=4
        }
        #endregion
        
        #endregion

        #region Constants & ReadOnly

        public const string DTP_DATE_FORMAT = "dd-MM-yyyy";
        public const string TIME_FORMAT = "HH:mm:ss";
        public const string START_TIME = "00:00:00";
        public const string END_TIME = "23:99:99";
        private const string LOGIN_LOCATION = "LocationHierarchy";
        private static int m_currentLocationId;
        private static int m_registeredAddressLocationId;
        private static int m_currentLocationTypeId;
        private static string m_HODB;
        private static int m_IsMiniBranchLocation;

        public static int ON_HAND_SUB_BUCKET_ID = 5;

        private const string DISPLAY_AMOUNT_ROUNDING = "DisplayAmountRounding";
        private const string DB_AMOUNT_ROUNDING = "DBAmountRounding";
        private const string DISPLAY_QTY_ROUNDING = "DisplayQtyRounding";
        private const string DB_QTY_ROUNDING = "DBQtyRounding";

        private const string CURRENT_LOCATION_CODE = "CurrentLocationCode";
        private const string CURRENT_TERMINAL_CODE = "CurrentTerminalCode";
        private const string CURRENT_LOCATION_TYPE = "CurrentLocationType";
        private const string CURRENT_APP_TYPE = "CurrentAppType";
        private const string CURRENT_EMAILID = "EMailID";
        private const string CURRENT_TINNO = "TINNO";
        private const string CURRENT_PANNO = "PANNO";        
        public const string PARAM_MODE = "Mode";
        public const string PARAM_DATA = "InputParam";
        public const string PARAM_DATA2 = "InputParam2";
        public const string PARAM_DATA3 = "InputParam3";
        public const string PARAM_DATA4 = "InputParam4";
        public const string PARAM_DATA5 = "InputParam5";
        public const string PARAM_DATA6 = "InputParam6";
        public const string PARAM_DATA7 = "InputParam7";

        public const string PARAM_OUTPUT = "OutParam";
        public const string PARAM_VALIDATIONMESSAGE = "validationMessage";
        public const int PARAM_OUTPUT_LENGTH = 500;

        public const string INTERFACE_ID = "vInterfaceId";
        public const string INTERFACE_LOCCODE = "vLocationCode";
        public const string INTERFACE_TABLENAME = "vTableName";
        public const string INTERFACE_KEY1 = "vKey1";
        public const string INTERFACE_KEY2 = "vKey2";
        public const string INTERFACE_KEY3 = "vKey3";
        public const string INTERFACE_KEY4 = "vKey4";
        public const string INTERFACE_KEY5 = "vKey5";
        public const string INTERFACE_ACTION = "vAction";
        public const string INTERFACE_USERID = "vUserId";
        public const int INTERFACE_USERID_VAL = -2;
        public const string INTERFACE_OUTPARAM = "RecCnt";
        public const int INTERFACE_OUTPARAM_VAL = 2000;

        public static readonly string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public static readonly string DATE_FORMAT = "yyyy-MM-dd ";
        public static readonly DateTime DATETIME_NULL = Convert.ToDateTime(new DateTime(1900, 1, 1).ToString(DATE_TIME_FORMAT));
        public static readonly DateTime DATETIME_CURRENT = Convert.ToDateTime(DateTime.Now.ToString(DATE_TIME_FORMAT));
        public static readonly DateTime DATETIME_MAX = Convert.ToDateTime(new DateTime(2099, 12, 31).ToString(Common.DATE_TIME_FORMAT));


        public static readonly int INT_DBNULL = -1;
        public static readonly string DBNULL_VAL = DBNull.Value.ToString();
        public static readonly string SELECT_ALL = "All";
        public static readonly string SELECT_ONE = "Select";

        //Hierarchy 
        public static readonly string HIERARCHY_NAME = "HierarchyName";
        public static readonly string HIERARCHY_ID = "HierarchyId";
        public static readonly string HIERARCHY_LEVEL = "HierarchyLevel";
        public static readonly string HIERARCHY_CONFIG = "HierarchyConfigId";
        public static readonly int TREE_COMP_X = 20;
        public static readonly int TREE_COMP_Y = 70;

        //parameter
        public static readonly string KEYVALUE1 = "KeyValue1";
        public static readonly string KEYCODE1 = "KeyCode1";

        //Parameter Type Code
        public const string COUNTRY = "COUNTRY";
        public const string STATE = "STATE";
        public const string CITY = "CITY";
        public const string STATUS = "STATUS"; //Active, Inactive, Deleted.
        public const string SEARCH_STATUS = "SEARCH STATUS"; //Active and Inactive only.
        public const string ITEM = "ITEM";
        public const string TOM = "TOM";
        public const string INDENT_STATUS = "INDENT STATUS";
        public const string PROMOTION_CATEGORY = "PROMOCAT";
        public const string PROMOTION_DISCOUNT_TYPE = "DISCOUNTTYPE";
        public const string PROMO_STATUS = "PROMOSTATUS";
        public const string PROMOTION_APPLICABILITY = "PROMOAPPLY";
        public const string PROMOTION_CONDOPERATION = "CONDOPERATION";
        public const string PROMOTION_CONDITIONTYPE = "CONDITIONTYPE";
        public const string PROMOTION_CONDITIONON = "CONDITIONON";
        public const string PROMOTION_LOCSTATUS = "PROMOLOCSTATUS";
        public const string RTV_STATUS = "RTVSTATUS";
        public const string DEBIT_NOTE_TEXT = "RTVDEBITNOTE";
        public const string GRN_INVOICE_TYPE = "INVOICEGRNTYPE";
        public const string PROMO_Beneficiary = "Beneficiary";


        //Grid Button and Tab Control Constants
        public const string GRID_EDIT_BUTTON = "Edit";
        public const string GRID_REMOVE = "Remove";
        public const string TAB_CREATE_MODE = "Create";
        public const string TAB_UPDATE_MODE = "Update";
        public const string TAB_SEARCH_MODE = "Search";
        public const string GRID_SELECT_BUTTON = "Select";

        //User-Location-Role-Module-Function Constants
        //User
        public const string USER_ID = "UserId";
        public const string LOGIN_MODULE_CODE = "LoginModuleCode";
        public const string LOGOUT_MODULE_CODE = "LogoutModuleCode";

        //Location
        public const string LOCATION_ID = "LocationId";
        public const string LOCATION_NAME = "LocationName";

        //Role
        public const string ROLE_ID = "RoleId";
        public const string ROLE_NAME = "RoleName";

        public const Int16 GAP_BETWEEN_CONTROLS = 35;
        public const Int16 DISTRIBUTOR_CODE_LENGTH = 20;
        public const Int16 DISTRIBUTOR_NAME_LENGTH = 50;
        public const string VALIDATION_ERROR = "Validation Error";
        //Module

        //Function

        //Item Master
        public const string SUB_CATEGORY_ID = "MerchHierarchyId";
        public const string SUB_CATEGORY_NAME = "MerchHierarchyName";

        public const string TAX_CODE = "TaxCode";
        public const string TAX_CODE_ID = "TaxCodeId";

        public const string TAX_CATEGORY_ID = "TaxCategoryId";
        public const string TAX_CATEGORY_NAME = "TaxCategoryName";

        public const string MESSAGE_PATH = "MessagePath";

        public const int F4KEY = 115;

        //Module name for POS
        public const string MODULE_ORDERFORM = "POS01";
        public const string MODULE_TEAMORDER = "POS02";
        public const string MODULE_REGISTER_DISTRIBUTOR = "POS03";
        public const string MODULE_ORDERHISTORY = "POS04";


        //function code
        public const string FUNCTIONCODE_SEARCH = "F001";
        public const string FUNCTIONCODE_CREATE = "F002";
        public const string FUNCTIONCODE_EDIT = "F003";
        public const string FUNCTIONCODE_UPDATE = "F004";
        public const string FUNCTIONCODE_ADD = "F005";
        public const string FUNCTIONCODE_SAVE = "F006";
        public const string FUNCTIONCODE_CONFIRM = "F007";
        public const string FUNCTIONCODE_CANCEL = "F008";
        public const string FUNCTIONCODE_APPROVE = "F009";
        public const string FUNCTIONCODE_REJECT = "F010";
        public const string FUNCTIONCODE_DELETE = "F011";
        public const string FUNCTIONCODE_RECIEVED = "F012";
        public const string FUNCTIONCODE_CLOSE = "F013";
        public const string FUNCTIONCODE_INITIATE = "F014";
        public const string FUNCTIONCODE_EXECUTE = "F015";
        public const string FUNCTIONCODE_VIEW = "F016";
        public const string FUNCTIONCODE_CREATE_GRN = "F017";
        public const string FUNCTIONCODE_COPY = "F018";
        public const string FUNCTIONCODE_PRINT = "F019";
        public const string FUNCTIONCODE_CONSOLIDATE = "F020";
        public const string FUNCTIONCODE_AMEND = "F021";
        public const string FUNCTIONCODE_REMOVE = "F022";
        public const string FUNCTIONCODE_PWDMODIFY = "F023";
        public const string FUNCTIONCODE_PROCESS = "F026";
        public const string FUNCTIONCODE_SHIP = "F028";

        public const string FUNCTIONCODE_NEWORDER = "F029";
        public const string FUNCTIONCODE_TEAMORDER = "F030";
        public const string FUNCTIONCODE_REG_DISTRIBUTOR = "F031";
        public const string FUNCTIONCODE_KITORDER = "F032";
        public const string FUNCTIONCODE_PREVIEW = "F033";
        public const string FUNCTIONCODE_INVOICE = "F034";
        public const string FUNCTIONCODE_PRINTINVOICE = "F035";
        public const string FUNCTIONCODE_PRINTORDER = "F036";
        public const string FUNCTIONCODE_ADDTOLOG = "F037";
        public const string FUNCTIONCODE_LOG = "F038";
        public const string FUNCTIONCODE_STOCKPOINT = "F039";
        public const string FUNCTIONCODE_CHANGEDELIVERYMODE = "F040";
        public const string FUNCTIONCODE_MODIFYORDER = "F041";


        #endregion Constants & ReadOnly

        #region Properties

        //public static Hierarchies.BusinessObjects.LocationHierarchy LocationHierarchy
        //{
        //    get { return (Hierarchies.BusinessObjects.LocationHierarchy) AppDomain.CurrentDomain.GetData(LOGIN_LOCATION); }
        //    set { AppDomain.CurrentDomain.SetData(LOGIN_LOCATION, value); }
        //}

        public static string MessagePath
        {
            get { return AppDomain.CurrentDomain.GetData(MESSAGE_PATH).ToString(); }
            set { AppDomain.CurrentDomain.SetData(MESSAGE_PATH, value); }
        }

        public static int DisplayAmountRounding
        {
            get { return Convert.ToInt32(AppDomain.CurrentDomain.GetData(DISPLAY_AMOUNT_ROUNDING)); }
            set { AppDomain.CurrentDomain.SetData(DISPLAY_AMOUNT_ROUNDING, value); }
        }

        public static int DBAmountRounding
        {
            get { return Convert.ToInt32(AppDomain.CurrentDomain.GetData(DB_AMOUNT_ROUNDING)); }
            set { AppDomain.CurrentDomain.SetData(DB_AMOUNT_ROUNDING, value); }
        }

        public static int DisplayQtyRounding
        {
            get { return Convert.ToInt32(AppDomain.CurrentDomain.GetData(DISPLAY_QTY_ROUNDING)); }
            set { AppDomain.CurrentDomain.SetData(DISPLAY_QTY_ROUNDING, value); }
        }

        public static int DBQtyRounding
        {
            get { return Convert.ToInt32(AppDomain.CurrentDomain.GetData(DB_QTY_ROUNDING)); }
            set { AppDomain.CurrentDomain.SetData(DB_QTY_ROUNDING, value); }
        }

        public static string LogoutModuleCode
        {
            get { return AppDomain.CurrentDomain.GetData(LOGOUT_MODULE_CODE).ToString(); }
            set { AppDomain.CurrentDomain.SetData(LOGOUT_MODULE_CODE, value); }
        }

        public static string LoginModuleCode
        {
            get { return AppDomain.CurrentDomain.GetData(LOGIN_MODULE_CODE).ToString(); }
            set { AppDomain.CurrentDomain.SetData(LOGIN_MODULE_CODE, value); }
        }
        public static Int32 CurrentLocationId
        {
            get { return m_currentLocationId; }
            set { m_currentLocationId = value; }
        }
        public static Int32 RegisteredAddressLocationId
        {
            get { return m_registeredAddressLocationId; }
            set { m_registeredAddressLocationId = value; }
        }


        public static int BoId_Invoice
        {
            set;
            get;
        }
        public static Int32 CurrentLocationTypeId
        {
            get { return m_currentLocationTypeId; }
            set { m_currentLocationTypeId = value; }
        }

        public static string LocationCode
        {
            get { return AppDomain.CurrentDomain.GetData(CURRENT_LOCATION_CODE).ToString(); }
            set { AppDomain.CurrentDomain.SetData(CURRENT_LOCATION_CODE, value); }
        }
        public static string LocationType
        {
            get { return AppDomain.CurrentDomain.GetData(CURRENT_LOCATION_TYPE).ToString(); }
            set { AppDomain.CurrentDomain.SetData(CURRENT_LOCATION_TYPE, value); }
        }

        public static string Version
        {
            get;
            set;
        }

        public static string TerminalCode
        {
            get { return AppDomain.CurrentDomain.GetData(CURRENT_TERMINAL_CODE).ToString(); }
            set { AppDomain.CurrentDomain.SetData(CURRENT_TERMINAL_CODE, value); }
        }

        public static string AppType
        {
            get { return AppDomain.CurrentDomain.GetData(CURRENT_APP_TYPE).ToString(); }
            set { AppDomain.CurrentDomain.SetData(CURRENT_APP_TYPE, value); }
        }
        public static string EMAILID
        {
            get { return AppDomain.CurrentDomain.GetData(CURRENT_EMAILID).ToString(); }
            set { AppDomain.CurrentDomain.SetData(CURRENT_EMAILID, value); }
        }
       
        public static string TINNO
        {
            get { return AppDomain.CurrentDomain.GetData(CURRENT_TINNO).ToString(); }
            set { AppDomain.CurrentDomain.SetData(CURRENT_TINNO, value); }
        }
        public static string PANNO
        {
            get { return AppDomain.CurrentDomain.GetData(CURRENT_PANNO).ToString(); }
            set { AppDomain.CurrentDomain.SetData(CURRENT_PANNO, value); }
        }
        
        public static string HODB
        {
            get
            {
                return "HODB";
            }
            set
            {
                throw new Exception("This property cannot be set explicitly.");
            }
        }

        public static int IsMiniBranchLocation
        {
            get { return m_IsMiniBranchLocation; }
            set { m_IsMiniBranchLocation = value; }
        }

        public static Boolean ForSkinCareItem
        {
            get;
            set;
        }
        
        #endregion

        #region SP Declaration
        private const string SP_PARAMETER_SEARCH = "usp_Parameter";
        private const string SP_GET_PVMONTH = "usp_GetPVMonth";
        private const string SP_LOCATION_TERMINAL_CHECK = "usp_LocationTerminalCheck";
        private const string SP_DAYBEGIN = "usp_daybegin";
        private const string SP_SchedularJob = "usp_SchedularStatus";

        #endregion

        public static string CodeValidate(string code, string labelText)
        {
            string retError = string.Empty;
            if (code.Trim().IndexOf(" ") >= 0)
            {
                retError = Common.GetMessage("VAL0082", labelText);
            }
            return retError;
        }

        public static StringBuilder ReturnErrorMessage(StringBuilder sb)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            sb3.Append(Common.GetMessage("VAL0086"));

            List<string> a = new List<string>();
            a = sb.ToString().Split(("\r\n").ToCharArray()).ToList();
            //  a.Sort();
            for (int i = 0; i < a.Count; i++)
            {
                if ((a[i].Trim().Length > 0) && (a[i].StartsWith("Please select")))
                {
                    sb1.Append("-");
                    sb1.Append(a[i].Substring("Please select".Length));
                    sb1.AppendLine();
                }
                else if ((a[i].Trim().Length > 0) && (a[i].StartsWith("Please enter")))
                {
                    sb1.Append("-");
                    sb1.Append(a[i].Substring("Please enter".Length));
                    sb1.AppendLine();
                }
                else if (a[i].Trim().Length > 0)
                {
                    sb2.Append(a[i]);
                    sb2.AppendLine();
                }
            }

            if (sb1.Length > 0)
            {
                sb3.AppendLine();
                sb3.Append(sb1);
                sb3.AppendLine();
            }
            if (sb2.Length > 0)
            {
                if (sb1.ToString().Trim().Length == 0)
                    sb3 = sb2;
                else
                    sb3.Append(sb2);
            }
            return sb1.Length > 0 || sb2.Length > 0 ? sb3 : new StringBuilder();
        }

        #region Methods

        /// <summary>
        /// this method is used to get dayBegin
        /// </summary>
        /// <returns></returns>
        /// 


        
        //AKASH
        public static bool GetDayBegin(out string sMessage)
        {
            DBParameterList objParameterList = null;
            sMessage = "";
            string sOutparam = null;
            try
            {
                objParameterList = new DBParameterList();
                objParameterList.Add(new DBParameter(PARAM_OUTPUT, sOutparam, DbType.String, ParameterDirection.Output, PARAM_OUTPUT_LENGTH));
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    dtManager.ExecuteNonQuery(SP_DAYBEGIN, objParameterList);
                }
                sOutparam = objParameterList[PARAM_OUTPUT].Value.ToString();
                if (string.Compare(sOutparam, "1", true) == 0)
                    return true;
                else
                    sMessage = GetMessage("40034");
                return false;
            }
            finally
            { }
        }

        /// <summary>
        /// This method selects/deselects all items in a checkedlistbox
        /// </summary>
        /// <param name="clb">checkedlistbox object</param>
        /// <param name="action">true to select all, false to deselect all</param>
        public static void SelectDeselectAll(CheckedListBox clb, bool action)
        {
            for (int i=0; i < clb.Items.Count; i++)
            {
                clb.SetItemChecked(i, action); 
            }
        }

        public static bool CheckTerminalIsActive(ref string errorMessage)
        {
            bool returnValue = false;
            if (string.IsNullOrEmpty(Common.TerminalCode) || string.IsNullOrEmpty(Common.LocationCode))
            {
                returnValue = false;
            }
            else
            {
                try
                {
                    DBParameterList dbParamList = new DBParameterList();
                    dbParamList.Add(new DBParameter("@locationCode", Common.LocationCode, DbType.String));
                    dbParamList.Add(new DBParameter("@terminalCode", Common.TerminalCode, DbType.String));
                    dbParamList.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    dbParamList.Add(new DBParameter("@Version", Common.Version, DbType.String));
                    dbParamList.Add(new DBParameter("@Apptype", (Common.ApplicationType)Convert.ToInt32(Common.AppType), DbType.String));
                    using (DataTaskManager dtManager = new DataTaskManager())
                    {
                        object returnObj = dtManager.ExecuteScalar(SP_LOCATION_TERMINAL_CHECK, dbParamList);

                        errorMessage = dbParamList[Common.PARAM_OUTPUT].Value.ToString();
                        if (!string.IsNullOrEmpty(errorMessage) || returnObj == null)
                        {
                            returnValue = false;
                        }
                        else
                        {
                            returnValue = Convert.ToBoolean(returnObj);
                        }
                    }
                    return returnValue;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return returnValue;
        }

        ///<summary>
        ///Method to retreive message text corresponding to message ID
        ///</summary>
        ///<param name="strMessageID">Message ID for which message text is requested</param>
        ///<returns>Message text as string</returns>
        ///<remarks></remarks>
        public static string GetMessage(string messageId)
        {
            DataSet dsMessage;
            string msgText = string.Empty;
            DataRow[] drSelect;

            try
            {
                if (string.IsNullOrEmpty(messageId) || messageId.Trim().Length == 0)
                {

                    throw new ArgumentNullException("messageId");
                }
                dsMessage = new DataSet();
                dsMessage.ReadXml(Environment.CurrentDirectory + "\\App_Data\\Message.xml");
                string[] param = messageId.Split(",".ToCharArray()[0]);
                if (param.Length > 1)
                {
                    return GetMessage(param);
                }
                else
                {
                    drSelect = dsMessage.Tables[0].Select("id = '" + messageId + "'");
                    if (drSelect.Length == 0)
                    {
                        throw new ArgumentException("Message does not exists - " + messageId, "messageId");
                    }
                    msgText = drSelect[0]["text"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dsMessage = null;
                drSelect = null;
            }
            return msgText.Replace("\\n", Environment.NewLine);
        }

        /// <summary>
        /// Method to fetch system defined messages
        /// </summary>
        /// <param name="param">Message Parameters</param>
        /// <returns></returns>
        public static string GetMessage(params string[] param)
        {
            string code;
            string[] mparam;
            StringBuilder msg;

            try
            {
                if (param.Length == 0)
                    return string.Empty;

                XmlDocument messageDoc = new XmlDocument();
                messageDoc.Load(Environment.CurrentDirectory + "\\App_Data\\Message.xml");

                mparam = param.Length == 1 ? param[0].Split(',') : param;
                code = mparam[0];
                for (int i = 1; i < mparam.Length; i++)
                    mparam[i - 1] = mparam[i];
                mparam[mparam.Length - 1] = string.Empty;

                XmlNodeList messageList = messageDoc.SelectNodes("//messages/message[@id='" + code + "']");
                if (messageList.Count > 0)
                    msg = new StringBuilder(messageList[0].Attributes.GetNamedItem("text").Value);
                else
                    msg = new StringBuilder(messageDoc.SelectNodes("//messages/message[@id='0']")[0].Attributes.GetNamedItem("text").Value);

                for (int i = 0; msg.ToString().IndexOf("{" + i.ToString() + "}") >= 0; i++)
                {
                    code = i < mparam.Length ? mparam[i] : string.Empty;
                    msg.Replace("{" + i.ToString() + "}", code);
                }

                return msg.ToString().Replace("\\n", Environment.NewLine);
            }
            catch { throw; }
        }

        /// <summary>
        /// Method to fetch multi-error messages to display for more than one error-codes
        /// </summary>
        /// <param name="errMsg">Error-Message containing error-code and parameters for the error-code</param>
        /// <returns></returns>
        public static string GetMultiErrorMessage(string errorMessage)
        {
            try
            {
                if (string.IsNullOrEmpty(errorMessage))
                {
                    return string.Empty;
                }

                string[] strarrTemp = errorMessage.Split('|');
                string errMsg = string.Empty;

                for (int i = 1; i < strarrTemp.Length; i+=2)
                {
                    string parameter = strarrTemp[i + 1];
                    if (parameter.Contains(","))
                    {
                        List<string> lstTempItems = parameter.Split(',').ToList<string>();
                        List<string> lstDuplicateItems = new List<string>();
                        foreach (string tempItem in lstTempItems)
                        {
                            if (!lstDuplicateItems.Contains(tempItem))
                            {
                                lstDuplicateItems.Add(tempItem);
                            }
                        }

                        parameter = string.Empty;
                        foreach (string tempItem in lstDuplicateItems)
                        {
                            parameter += tempItem + ",";
                        }
                        parameter = parameter.Substring(0, parameter.Length - 1);
                    }

                    if (string.IsNullOrEmpty(errMsg))
                    {
                        errMsg += Common.GetMessage(strarrTemp[i], parameter);
                    }
                    else
                    {
                        errMsg += Environment.NewLine + Common.GetMessage(strarrTemp[i], parameter);
                    }
                }

                //if (strarrTemp.Length > 2)
                //{
                //    if (strarrTemp[1] != null)
                //    {
                //        string items = strarrTemp[2];
                //        if (items.Contains(","))
                //        {
                //            List<string> lstTempItems = items.Split(',').ToList<string>();
                //            List<string> lstDuplicateItems = new List<string>();
                //            foreach (string tempItem in lstTempItems)
                //            {
                //                if (!lstDuplicateItems.Contains(tempItem))
                //                {
                //                    lstDuplicateItems.Add(tempItem);
                //                }
                //            }

                //            items = string.Empty;
                //            foreach (string tempItem in lstDuplicateItems)
                //            {
                //                items += tempItem + ",";
                //            }
                //            items = items.Substring(0, items.Length - 1);
                //        }
                //        errMsg = Common.GetMessage(strarrTemp[1], items);
                //    }
                //}
                //if (strarrTemp.Length > 4)
                //{
                //    if (strarrTemp[3] != null)
                //    {
                //        string items = strarrTemp[4];
                //        if (items.Contains(","))
                //        {
                //            List<string> lstTempItems = items.Split(',').ToList<string>();
                //            List<string> lstDuplicateItems = new List<string>();
                //            foreach (string tempItem in lstTempItems)
                //            {
                //                if (!lstDuplicateItems.Contains(tempItem))
                //                {
                //                    lstDuplicateItems.Add(tempItem);
                //                }
                //            }

                //            items = string.Empty;
                //            foreach (string tempItem in lstDuplicateItems)
                //            {
                //                items += tempItem + ",";
                //            }
                //            items = items.Substring(0, items.Length - 1);
                //        }

                //        if (string.IsNullOrEmpty(errMsg))
                //        {
                //            errMsg += Common.GetMessage(strarrTemp[3], items);
                //        }
                //        else
                //        {
                //            errMsg += Environment.NewLine + Common.GetMessage(strarrTemp[3], items);
                //        }
                //    }
                //}

                //return msg.ToString().Replace("\\n", Environment.NewLine);
                return errMsg;
            }
            catch { throw; }
        }

        /// <summary>
        /// Method to return a string-format for DisplayAmountRounding property
        /// </summary>
        /// <param name="zeroesCount">Number of zeroes as derived from DisplayAmountRounding property</param>
        /// <returns>String-format for DisplayAmountRounding property; for use in Type.ToString() method</returns>
        public static string GetRoundingZeroes(int zeroesCount)
        {
            string zeroesToReturn = "0";
            if (zeroesCount > 0)
            {
                zeroesToReturn += ".";
                for (int cnt = 0; cnt < zeroesCount; cnt++)
                {
                    zeroesToReturn += "0";
                }
            }

            return zeroesToReturn;
        }

        /// <summary>
        /// Method to maintain Log of all exceptions at specified location in local drive.
        /// </summary>
        /// <param name="ex">Exception object</param>
        public static void LogException(Exception ex)
        {
            LogManager.WriteExceptionLog(ex);
        }

        /// <summary>
        /// Method to maintain Log of informations (if any) at specified location in local drive.
        /// (Currently not in use)
        /// </summary>
        /// <param name="message">Message to be written in Information Log.</param>
        public static void LogInformation(string message)
        {
            LogManager.WriteInformationLog(message);
        }

        /// <summary>
        /// Function to close the form. This function should be invoked at 'Exit'
        /// button of each form.
        /// </summary>
        /// <param name="formName"></param>
        public static void CloseThisForm(Form formName)
        {
            try
            {
                string formTitle = "";
                if (Common.AppType == ((int)Common.ApplicationType.BOS).ToString())
                    formTitle = "10001";
                else
                    formTitle = "10004";
                DialogResult result = MessageBox.Show(GetMessage("5003"),
                    GetMessage(formTitle), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
                else
                {
                    //Application.DoEvents();
                    formName.Close();
                    formName.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get Address of HO for Report Header. 
        /// </summary>
        /// <returns></returns>
        public static string ReportHeaderAddress()
        {
            //CountryID = "1";
            //int iLocationId = 1;
            int iCurrentLocationId = 0;
            if (Common.BoId_Invoice == 0)
                iCurrentLocationId = Common.CurrentLocationId;
            else
                iCurrentLocationId = Common.BoId_Invoice;
            DataTable dtAddress = new DataTable();
           // if (Common.CurrentLocationId == 29)
               // iLocationId = 29;
            dtAddress = ParameterLookup(ParameterType.LocationAddress, new ParameterFilter("", iCurrentLocationId, 0, 0));
            string address1 = dtAddress.Rows[0]["Address1"].ToString();
            string address2 = dtAddress.Rows[0]["Address2"].ToString() + "," + "*|$|*" + dtAddress.Rows[0]["Address3"].ToString() + ", " + dtAddress.Rows[0]["CityName"].ToString() + " " + dtAddress.Rows[0]["Pincode"].ToString().Trim() + ", " + dtAddress.Rows[0]["StateName"].ToString().Trim() + ", " + dtAddress.Rows[0]["CountryName"].ToString().Trim() + "*|$|*" + "Phone: " + dtAddress.Rows[0]["Phone1"].ToString() + " Fax: " + dtAddress.Rows[0]["Fax1"].ToString();
            string completeAddress = address1 + "*|$|*" + address2;
            CountryID = dtAddress.Rows[0]["CountryID"].ToString();
            return completeAddress;
        }

        public static string RegisteredOfficeAddress()
        {
            
            DataTable dtAddress = new DataTable();
            int iCurrentLocationId = 0;
            if (Common.BoId_Invoice == 0)
            {
                iCurrentLocationId = Common.RegisteredAddressLocationId;
            }
                //iCurrentLocationId = Common.CurrentLocationId;
            else
            {
                iCurrentLocationId = Common.BoId_Invoice;
            }
            //if ((iCurrentLocationId == 13) || (iCurrentLocationId == 14) || (iCurrentLocationId == 18))
            //    dtAddress = ParameterLookup(ParameterType.LocationAddress, new ParameterFilter("", 13, 0, 0));
            //else

            dtAddress = ParameterLookup(ParameterType.LocationAddress, new ParameterFilter("", iCurrentLocationId, 0, 0));

            string address1 = dtAddress.Rows[0]["Address1"].ToString();
            string address2 = dtAddress.Rows[0]["Address2"].ToString() + "," + "*|$|*" + dtAddress.Rows[0]["Address3"].ToString() + ", " + dtAddress.Rows[0]["CityName"].ToString() + " " + dtAddress.Rows[0]["Pincode"].ToString().Trim() + ", " + dtAddress.Rows[0]["StateName"].ToString().Trim() + ", " + dtAddress.Rows[0]["CountryName"].ToString().Trim() + "*|$|*" + "Phone: " + dtAddress.Rows[0]["Phone1"].ToString() + " Fax: " + dtAddress.Rows[0]["Fax1"].ToString();
            string completeAddress = address1 + "*|$|*" + address2;
            //CountryID = dtAddress.Rows[0]["CountryID"].ToString();
            return completeAddress;
        }
        
       
        #region ToXml
        /// <summary>
        /// Method to generate XML string for BL class objects
        /// </summary>
        /// <param name="target">BL class instance</param>
        /// <returns></returns>
        public static string ToXml(object target)
        {
            string xmlstring = string.Empty;
            StringWriter output = null;
            XmlSerializer xs = null;

            try
            {
                output = new StringWriter(new StringBuilder());
                xs = new XmlSerializer(target.GetType());
                xs.Serialize(output, target);
                xmlstring = output.ToString().Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "").Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "").Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "").Replace("\r\n", "").Trim();
                return xmlstring;
            }
            catch { throw; }
        }
        #endregion

        public static DataTable ParameterLookupXML(string key, ParameterFilter filter)
        {
            ParameterType pt = (ParameterType)Enum.Parse(typeof(ParameterType), key);
            return ParameterLookup(pt, filter);
        }

        //Created By Punit, 22 Jun 
        /// <summary>
        /// Method to fetch Parameter Data
        /// </summary>
        /// <param name="key">Parameter Identifier</param>
        /// <param name="filter">Parameter Filter Criteria</param>
        /// <returns></returns>
        public static DataTable ParameterLookup(ParameterType key, ParameterFilter filter)
        {
            DBParameterList dbParam = null;

            try
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(PARAM_MODE, (int)key, DbType.Int32));
                dbParam.Add(new DBParameter(PARAM_DATA, ToXml(filter), DbType.String));
                dbParam.Add(new DBParameter(PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    return dtManager.ExecuteDataTable(SP_PARAMETER_SEARCH, dbParam);
                }
            }
            catch { throw; }
        }

        public static DataTable Lookup(string paramCode, int keyCode1, int keyCode2, int keyCode3)
        {
            try
            {
                return ParameterLookup(ParameterType.Parameter, new ParameterFilter(paramCode, keyCode1, keyCode2, keyCode3));
            }
            catch { throw; }
        }
        /// <summary>
        /// Method to bind ComboBox from Parameter master, 
        /// like 'STATUS', 'TITLE' etc.
        /// </summary>
        /// <param name="cmbName">Name of ComboBox</param>
        /// <param name="parameterCode">Parameter master code (STATUS, TITLE)</param>
        /// <param name="keyCode1"></param>
        /// <param name="keyCode2"></param>
        /// <param name="keyCode3"></param>
        public static void BindParamComboBox(ComboBox cmbName, string paramCode, int keyCode1, int keyCode2, int keyCode3)
        {
            DataTable dtComboValues = new DataTable();
            try
            {
                dtComboValues = ParameterLookup(ParameterType.Parameter,
                     new ParameterFilter(paramCode, keyCode1, keyCode2, keyCode3));
                cmbName.DataSource = dtComboValues;
                cmbName.ValueMember = KEYCODE1;
                cmbName.DisplayMember = KEYVALUE1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtComboValues = null;
            }
        }

        /// <summary>
        /// Method to bind 'non Parameter master' values in ComboBox, 
        /// like 'Country', 'State' and 'City'.
        /// </summary>
        /// <param name="cmbName">Name of ComboBox</param>
        /// <param name="paramType">Pass the type of values to bind in this ComboBox, like 
        /// <example>ParameterType.Country, ParameterType.State, ParameterType.City, etc.
        /// </example>
        /// </param>
        /// <param name="keyCode1"></param>
        public static void FillComboBox(ComboBox cmbName, ParameterType paramType, int keyCode1)
        {

            DataTable dtComboValues = new DataTable();
            try
            {
                switch (paramType)
                {
                    case ParameterType.Country:
                        dtComboValues = Common.ParameterLookup(paramType,
                    new ParameterFilter(string.Empty, keyCode1, 0, 0));
                        cmbName.DataSource = dtComboValues;
                        cmbName.ValueMember = "CountryId";
                        cmbName.DisplayMember = "CountryName";
                        break;

                    case ParameterType.State:
                        dtComboValues = Common.ParameterLookup(paramType,
                    new ParameterFilter(string.Empty, keyCode1, 0, 0));

                        cmbName.DataSource = dtComboValues;
                        cmbName.ValueMember = "StateId";
                        cmbName.DisplayMember = "StateName";
                        break;

                    case ParameterType.City:
                        dtComboValues = Common.ParameterLookup(paramType,
                    new ParameterFilter(string.Empty, keyCode1, 0, 0));
                        cmbName.DataSource = dtComboValues;
                        cmbName.ValueMember = "CityId";
                        cmbName.DisplayMember = "CityName";
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtComboValues = null;
            }
        }

        /// <summary>
        /// Creates Columns for the specified datagridview from the xml at the specified path
        /// </summary>
        /// <param name="dgv">DataGridView in which the columns have to be created</param>
        /// <param name="xmlFilePath">Path of the xml file which contains column definitions</param>
        /// <returns></returns>
        public static DataGridView GetDataGridViewColumns(DataGridView dgv, string xmlFilePath)
        {
            //dgv.FindForm().SuspendLayout();
            dgv.Parent.SuspendLayout();


            DataSet dsGrids = new DataSet();
            dsGrids.ReadXml(xmlFilePath);
            DataRow[] grids = dsGrids.Tables[0].Select("Name = '" + dgv.Name + "'");
            DataRow[] gridColumns = dsGrids.Tables[1].Select("GridView_Id = '" + grids[0][0] + "'");
            CreateGridColums(dgv, grids, gridColumns);

            //dgv.AlternatingRowsDefaultCellStyle.BackColor = System.color#CCE9FB
            //Set Grid Properties   
            dgv.Parent.ResumeLayout(false);
            //dgv.FindForm().ResumeLayout(false);
            return dgv;
        }

        public static void CreateGridColums(DataGridView dgv, DataRow[] grids, DataRow[] gridColumns)
        {
            dgv.EnableHeadersVisualStyles = false;
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            //GET FROM Config
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;

            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;

            dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;


            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgv.DefaultCellStyle = dataGridViewCellStyle3;

            switch (grids[0]["SelectionMode"].ToString().ToUpper())
            {
                case "FULLROWSELECT":
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    break;
                case "FULLCOLUMNSELECT":
                    dgv.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
                    break;
                case "ROWHEADERSELECT":
                    dgv.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                    break;
                case "COLUMNHEADERSELECT":
                    dgv.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                    break;
                default:
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    break;
            }

            dgv.AllowUserToResizeColumns = Convert.ToBoolean(grids[0]["AllowUserToResizeColumns"]);
            dgv.AllowUserToResizeRows = Convert.ToBoolean(grids[0]["AllowUserToResizeRows"]);
            dgv.AutoGenerateColumns = Convert.ToBoolean(grids[0]["AutoGenerateColumns"]);

            foreach (DataRow dr in gridColumns)
            {
                switch (dr["ColumnType"].ToString().ToUpper())
                {
                    case "BUTTON":
                        DataGridViewButtonColumn dgvcButton = new DataGridViewButtonColumn();
                        dgvcButton.Name = dr["Name"].ToString();
                        dgvcButton.HeaderText = dr["HeaderText"].ToString();
                        if (dr["DataPropertyName"].ToString() != string.Empty) dgvcButton.DataPropertyName = dr["DataPropertyName"].ToString();
                        dgvcButton.Visible = Convert.ToBoolean(dr["Visible"]);
                        dgvcButton.Width = Convert.ToInt32(dr["Width"]);
                        dgvcButton.ReadOnly = Convert.ToBoolean(dr["ReadOnly"]);
                        dgvcButton.UseColumnTextForButtonValue = true;
                        dgvcButton.Text = dr["HeaderText"].ToString();
                        dgv.Columns.Add(dgvcButton);
                        break;
                    case "CHECKBOX":
                        DataGridViewCheckBoxColumn dgvcCheck = new DataGridViewCheckBoxColumn();
                        dgvcCheck.Name = dr["Name"].ToString();
                        dgvcCheck.HeaderText = dr["HeaderText"].ToString();
                        if (dr["DataPropertyName"].ToString() != string.Empty) dgvcCheck.DataPropertyName = dr["DataPropertyName"].ToString();
                        dgvcCheck.Visible = Convert.ToBoolean(dr["Visible"]);
                        dgvcCheck.Width = Convert.ToInt32(dr["Width"]);
                        dgvcCheck.ReadOnly = Convert.ToBoolean(dr["ReadOnly"]);
                        //dgvcCheck.ThreeState = true;
                        //dgvcCheck.TrueValue = true;
                        dgv.Columns.Add(dgvcCheck);
                        break;
                    case "COMBOBOX":
                        DataGridViewComboBoxColumn dgvcCombo = new DataGridViewComboBoxColumn();
                        dgvcCombo.Name = dr["Name"].ToString();
                        dgvcCombo.HeaderText = dr["HeaderText"].ToString();
                        if (dr["DataSource"].ToString() != string.Empty)
                            dgvcCombo.DataSource = dr["DataSource"].ToString();
                        if (dr["DataPropertyName"].ToString() != string.Empty)
                            dgvcCombo.DataPropertyName = dr["DataPropertyName"].ToString();
                        if (dr["DisplayMember"].ToString() != string.Empty)
                            dgvcCombo.DisplayMember = dr["DisplayMember"].ToString();
                        if (dr["ValueMember"].ToString() != string.Empty)
                            dgvcCombo.ValueMember = dr["ValueMember"].ToString();
                        dgvcCombo.Visible = Convert.ToBoolean(dr["Visible"]);
                        dgvcCombo.Width = Convert.ToInt32(dr["Width"]);
                        dgvcCombo.ReadOnly = Convert.ToBoolean(dr["ReadOnly"]);
                        dgv.Columns.Add(dgvcCombo);
                        break;
                    case "IMAGE":
                        DataGridViewImageColumn dgvcImage = new DataGridViewImageColumn();
                        dgvcImage.Name = dr["Name"].ToString();
                        dgvcImage.HeaderText = dr["HeaderText"].ToString();
                        if (dr["DataPropertyName"].ToString() != string.Empty) dgvcImage.DataPropertyName = dr["DataPropertyName"].ToString();
                        ResourceManager rm = new ResourceManager("CoreComponent.Core.Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
                        dgvcImage.Image = rm.GetObject(dr["ValueMember"].ToString()) as System.Drawing.Image;
                        dgvcImage.Visible = Convert.ToBoolean(dr["Visible"]);
                        dgvcImage.Width = Convert.ToInt32(dr["Width"]);
                        dgvcImage.ReadOnly = Convert.ToBoolean(dr["ReadOnly"]);
                        dgv.Columns.Add(dgvcImage);
                        rm.ReleaseAllResources();
                        break;
                    case "LINK":
                        DataGridViewImageColumn dgvcLink = new DataGridViewImageColumn();
                        dgvcLink.Name = dr["Name"].ToString();
                        dgvcLink.HeaderText = dr["HeaderText"].ToString();
                        if (dr["DataPropertyName"].ToString() != string.Empty) dgvcLink.DataPropertyName = dr["DataPropertyName"].ToString();
                        dgvcLink.Visible = Convert.ToBoolean(dr["Visible"]);
                        dgvcLink.Width = Convert.ToInt32(dr["Width"]);
                        dgvcLink.ReadOnly = Convert.ToBoolean(dr["ReadOnly"]);
                        dgv.Columns.Add(dgvcLink);
                        break;
                    case "TEXTBOX":
                        DataGridViewTextBoxColumn dgvcText = new DataGridViewTextBoxColumn();
                        dgvcText.Name = dr["Name"].ToString();
                        dgvcText.HeaderText = dr["HeaderText"].ToString();
                        if (dr["DataPropertyName"].ToString() != string.Empty)
                            dgvcText.DataPropertyName = dr["DataPropertyName"].ToString();
                        dgvcText.Visible = Convert.ToBoolean(dr["Visible"]);
                        dgvcText.Width = Convert.ToInt32(dr["Width"]);
                        dgvcText.ReadOnly = Convert.ToBoolean(dr["ReadOnly"]);
                        dgv.Columns.Add(dgvcText);
                        break;
                    default:
                        break;
                }
            }
        }
        
        public static string GetPVMonth()
        {
            DBParameterList dbParam = new DBParameterList();

            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DataTable dt = dtManager.ExecuteDataTable(SP_GET_PVMONTH, dbParam);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return (Convert.ToString(dt.Rows[0]["Month"]));
                    }
                    else
                        return null;
                }
            }
            catch { throw; }
        }
        //AKASH
        public static string SchedularJob(string jobName)
        {
            string errorMessage = string.Empty;
            try
            {
                
                DataSet ds = new DataSet();
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam = new DBParameterList();
                    dbParam = new DBParameterList();

                    dbParam.Add(new DBParameter("@JobName", jobName, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    ds = dtManager.ExecuteDataSet(SP_SchedularJob, dbParam);
                     errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return errorMessage;
        }
        public static string GetConfirmationStatusText(string statusText)
        {
            statusText = statusText.ToLower().Trim().Substring(0, statusText.Trim().Length >= 4 ? 4 : statusText.Trim().Length);
            int count = Enum.GetValues(typeof(CommonStatusText)).Length;
            for (int i = 0; i < count; i++)
            {
                if (statusText == Enum.GetValues(typeof(CommonStatusText)).GetValue(i).ToString().Trim().ToLower().Substring(0,4))
                    return Enum.GetValues(typeof(CommonStatusText)).GetValue(i).ToString();
            }
           
            return null;
        }

        public static DateTime FormatDateTextBox(TextBox txt)
        {
            if (txt.Text.Length > 0)
            {                
                string s3 = string.Empty;
                string s1 = string.Empty;
                string s2 = string.Empty;

                txt.Text = txt.Text.Replace("/", "");
                txt.Text = txt.Text.Replace("-", "");
                if (txt.Text.Length >= 2)
                {
                    s1 = txt.Text.Substring(0, 2) + "/";

                }

                if (txt.Text.Length >= 4)
                {
                    s2 = txt.Text.Substring(2, 2) + "/";
                    //textBox1.Text = textBox1.Text + s2 + "/";
                }
                if (txt.Text.Length >= 8)
                {
                    s3 = txt.Text.Substring(4, 4);
                    //textBox1.Text = textBox1.Text + s3;
                }
                string dateText = s1 + s2 + s3;
                string dateValue = s2 + s1 + s3;

                if (Validators.IsDateTime(dateValue) && Convert.ToDateTime(dateValue) > Convert.ToDateTime("01/01/1900"))
                {
                    txt.Text = dateText;
                    return Convert.ToDateTime(dateValue);
                }
                else
                {
                    //txt.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    MessageBox.Show(Common.GetMessage("VAL0604",dateText),Common.GetMessage("10004"),MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    txt.Text = string.Empty;
                    txt.Focus();
                    return DateTime.Today;                    
                }                
            }
            //txt.Text = DateTime.Today.ToString("dd/MM/yyyy");
            return DateTime.Today;
        }

        #endregion Methods




        /// <summary>
        /// This Class can be used to convert decimal currency to words 
        /// </summary>
        public class AmountToWords
        {
            private static string strReplicate(string str, int intD)
            {
                string functionReturnValue = null;
                //This fucntion padded "0" after the number to evaluate hundred, thousand and on....
                //using this function you can replicate any Charactor with given string.
                int i = 0;
                functionReturnValue = "";
                for (i = 1; i <= intD; i++)
                {
                    functionReturnValue = functionReturnValue + str;
                }
                return functionReturnValue;
            }

            public static string AmtInWord(decimal Num)
            {
                string functionReturnValue = null;
                //I have created this function for converting amount in indian rupees (INR). 
                //You can manipulate as you wish like decimal setting, Doller (any currency) Prefix.

                string strNum = null;
                string strNumDec = null;
                string StrWord = null;
                strNum = Num.ToString();

                if (strNum.IndexOf(".") != -1)
                {
                    strNumDec = strNum.Substring(strNum.IndexOf(".") + 1);

                    if (strNumDec.Length == 1)
                    {
                        strNumDec = strNumDec + "0";
                    }
                    if (strNumDec.Length > 2)
                    {
                        strNumDec = strNumDec.Substring(0, 2);
                    }

                    strNum = strNum.Substring(0, strNum.IndexOf("."));
                    StrWord = (Convert.ToDecimal(strNum) == 1 ? "Rupee " : "Rupees ") + NumToWord(Convert.ToDecimal(strNum)) + (Convert.ToDecimal(strNumDec) > 0 ? " and Paise" + NumToWord(Convert.ToDecimal(strNumDec)) : "");
                }
                else
                {
                    StrWord = (Convert.ToDecimal(strNum) == 1 ? "Rupee " : "Rupees ") + NumToWord(Convert.ToDecimal(strNum));
                }
                functionReturnValue = StrWord + " Only";
                return functionReturnValue;

            }

            private static string NumToWord(decimal Num)
            {
                //I divided this function in two part.
                //1. Three or less digit number.
                //2. more than three digit number.
                string strNum = null;
                string StrWord = null;
                strNum = Num.ToString();

                if (strNum.Length <= 3)
                {
                    StrWord = cWord3(Convert.ToDecimal(strNum));
                }
                else
                {
                    StrWord = cWordG3(Convert.ToDecimal(strNum.Substring(0, strNum.Length - 3))) + " " + cWord3(Convert.ToDecimal(strNum.Substring(strNum.Length - 3)));
                }
                return StrWord;
            }

            private static string cWordG3(decimal Num)
            {
                string functionReturnValue = null;
                //2. more than three digit number.
                string strNum = "";
                string StrWord = "";
                string readNum = "";
                strNum = Num.ToString();
                if (strNum.Length % 2 != 0)
                {
                    readNum = strNum.Substring(0, 1);
                    if (readNum != "0")
                    {
                        StrWord = retWord(Convert.ToDecimal(readNum));
                        readNum = "1" + strReplicate("0", strNum.Length - 1) + "000";
                        StrWord = StrWord + " " + retWord(Convert.ToDecimal(readNum));
                    }
                    strNum = strNum.Substring(1);
                }
                while (!(strNum.Length == 0))
                {
                    readNum = strNum.Substring(0, 2);
                    if (Convert.ToDouble(readNum) != 0)
                    {
                        StrWord = StrWord + " " + cWord3(Convert.ToDecimal(readNum));
                        readNum = "1" + strReplicate("0", strNum.Length - 2) + "000";
                        StrWord = StrWord + " " + retWord(Convert.ToDecimal(readNum));
                    }
                    strNum = strNum.Substring(2);
                }
                functionReturnValue = StrWord;
                return functionReturnValue;
            }

            private static string cWord3(decimal Num)
            {
                string functionReturnValue = null;
                //1. Three or less digit number.
                string strNum = "";
                string StrWord = "";
                string readNum = "";
                if (Num < 0) Num = Num * -1;
                strNum = Num.ToString();

                if (strNum.Length == 3)
                {
                    readNum = strNum.Substring(0, 1);
                    StrWord = retWord(Convert.ToDecimal(readNum)) + " Hundred";
                    strNum = strNum.Substring(1, strNum.Length - 1);
                }

                if (strNum.Length <= 2)
                {
                    if (Convert.ToDecimal(strNum) >= 0 & Convert.ToDecimal(strNum) <= 20)
                    {
                        StrWord = StrWord + " " + retWord(Convert.ToDecimal(strNum));
                    }
                    else
                    {
                        StrWord = StrWord + " " + retWord(Convert.ToDecimal(strNum.Substring(0, 1) + "0")) + " " + retWord(Convert.ToDecimal(strNum.Substring(1, 1)));
                    }
                }

                strNum = Num.ToString();
                functionReturnValue = StrWord;
                return functionReturnValue;
            }

            private static string retWord(decimal Num)
            {
                string functionReturnValue = null;
                //This two dimensional array store the primary word convertion of number.
                functionReturnValue = "";
                object[,] ArrWordList = { { 0, "" }, { 1, "One" }, { 2, "Two" }, { 3, "Three" }, { 4, "Four" }, { 5, "Five" }, { 6, "Six" }, { 7, "Seven" }, { 8, "Eight" }, { 9, "Nine" }, 
        { 10, "Ten" }, { 11, "Eleven" }, { 12, "Twelve" }, { 13, "Thirteen" }, { 14, "Fourteen" }, { 15, "Fifteen" }, { 16, "Sixteen" }, { 17, "Seventeen" }, { 18, "Eighteen" }, { 19, "Nineteen" }, 
        { 20, "Twenty" }, { 30, "Thirty" }, { 40, "Forty" }, { 50, "Fifty" }, { 60, "Sixty" }, { 70, "Seventy" }, { 80, "Eighty" }, { 90, "Ninety" }, { 100, "Hundred" }, { 1000, "Thousand" }, 
        { 100000, "Lac" }, { 10000000, "Crore" } };

                int i = 0;
                for (i = 0; i <= ArrWordList.Length; i++)
                {
                    if (Num == Convert.ToDecimal(ArrWordList[i, 0]))
                    {
                        functionReturnValue = ArrWordList[i, 1].ToString();
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                return functionReturnValue;
            }
        }
    }

   public class DGVColumnHeader : DataGridViewColumnHeaderCell
    {
        private Rectangle ChkBoxRegion;
        private bool checkAll = false;
        LinearGradientBrush objBrush;
        public event EventHandler OnCheckBoxMouseClick;
        protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates dataGridViewElementState,
            object value, object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value,
                formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            objBrush = new LinearGradientBrush(cellBounds, Color.Transparent, Color.Transparent, LinearGradientMode.Vertical);

            //graphics.FillRectangle(new SolidBrush(cellStyle.BackColor), cellBounds);
            graphics.FillRectangle(objBrush, cellBounds);

            ChkBoxRegion = new Rectangle(
                cellBounds.Location.X + 4,
                cellBounds.Location.Y + 2,
                15, cellBounds.Size.Height - 6);


            if (this.checkAll)
                ControlPaint.DrawCheckBox(graphics, ChkBoxRegion, ButtonState.Checked);
            else
                ControlPaint.DrawCheckBox(graphics, ChkBoxRegion, ButtonState.Normal);
            
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            //Convert the CheckBoxRegion 
            Rectangle rec = new Rectangle(new Point(0, 0), this.ChkBoxRegion.Size);
            this.checkAll = !this.checkAll;
            if (rec.Contains(e.Location))
            {
                this.DataGridView.Invalidate();
                if (OnCheckBoxMouseClick != null)
                    OnCheckBoxMouseClick(null, null);
            }
            base.OnMouseClick(e);
        }

        public bool CheckAll
        {
            get { return this.checkAll; }
            set { this.checkAll = value; }
        }

        public Size CheckBoxRegion
        {
            get { return this.ChkBoxRegion.Size; }
        }
       
    }

}
