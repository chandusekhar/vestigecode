using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

namespace PurchaseComponent.BusinessObjects
{
    [Serializable]
    #region IndentConsolidation
    public class IndentConsolidation : IIndentConsolidation
    {
        #region CTOR
        public IndentConsolidation() { }

        public IndentConsolidation(
            int consolidationId,
            int status,
            int source,
            int createdBy,
            string createdDate,
            int modifiedBy,
            string modifiedDate,
            List<IndentConsolidationHeader> indentConsolidationHeaderList,
            IndentConsolidationBL.OperationState enumOperationState)
        {
            this.m_ConsolidationId = consolidationId;
            this.m_Status = status;
            this.m_Source = source;
            this.m_CreateBy = createdBy;
            this.m_CreatedDate = createdDate;
            this.m_ModifiedBy = modifiedBy;
            this.m_ModifiedDate = modifiedDate;
            this.m_OperationState = enumOperationState;
            //If it is a new indent, sequentially generated the header id and details ids
            if (consolidationId == Common.INT_DBNULL)
            {          
                if (enumOperationState == IndentConsolidationBL.OperationState.New)
                {
                    int tmpHeaderId = 0;
                    int tmpDetailId = 1;
                    for (int i = 0; i < indentConsolidationHeaderList.Count; i++)
                    {
                        tmpHeaderId = i + 1;
                        indentConsolidationHeaderList[i].HeaderId = tmpHeaderId;
                        for (int j = 0; j < indentConsolidationHeaderList[i].IndentConsolidationDetail.Count; j++)
                        {
                            indentConsolidationHeaderList[i].IndentConsolidationDetail[j].HeaderId = tmpHeaderId;
                            indentConsolidationHeaderList[i].IndentConsolidationDetail[j].DetailId = tmpDetailId;
                            tmpDetailId++;
                        }
                    }
                    this.m_IndentConsolidationHeader = indentConsolidationHeaderList;
                }
               
            }           
            else
            {
                this.m_IndentConsolidationHeader = indentConsolidationHeaderList;
            }
        }

        public IndentConsolidation(
                    string indentNo,
                    int consolidationId,
                    int status,
                    int source,
                    DateTime dateFrom,
                    DateTime dateTo
            )
        {
            this.IndentNo = indentNo;
            this.m_ConsolidationId = consolidationId;
            this.m_Status = status;
            this.m_Source = source;
            this.m_DateFrom = dateFrom;
            this.m_DateTo = dateTo;
        }

        #endregion

        #region IIndentConsolidation Members

        private string m_IndentNo;
        public string IndentNo
        {
            get
            {
                return m_IndentNo;
            }
            set
            {
                m_IndentNo = value;
            }
        }

        private int m_ConsolidationId;
        public int ConsolidationId
        {
            get
            {
                return m_ConsolidationId;
            }
            set
            {
                m_ConsolidationId = value;
            }
        }

        private int m_Status;
        public int Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
            }
        }

        private string m_StatusCap;
        public string StatusCap
        {
            get
            {
                return m_StatusCap;
            }
            set
            {
                m_StatusCap = value;
            }

        }

        private int m_Source;
        public int Source
        {
            get
            {
                return m_Source;
            }
            set
            {
                m_Source = value;
            }
        }

        private string m_SourceCap;
        public string SourceCap
        {
            get
            {
                return m_SourceCap;
            }
            set
            {
                m_SourceCap = value;
            }
        }

        private int m_CreateBy;
        public int CreatedBy
        {
            get
            {
                return m_CreateBy;
            }
            set
            {
                m_CreateBy = value;
            }
        }

        private string m_CreatedDate;
        public string CreatedDate
        {
            get
            {
                return m_CreatedDate;
            }
            set
            {
                m_CreatedDate = value;
            }
        }

        public String DisplayCreatedDate
        {
            get
            {
                if ((CreatedDate != null) && (CreatedDate.Length > 0))
                {
                    return Convert.ToDateTime(m_ModifiedDate).ToString(Common.DTP_DATE_FORMAT);
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                //Setter would do nothing. This is just to 
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        private int m_ModifiedBy;
        public int ModifiedBy
        {
            get
            {
                return m_ModifiedBy;
            }
            set
            {
                m_ModifiedBy = value;
            }
        }

        private string m_ModifiedDate;
        public string ModifiedDate
        {
            get
            {
                return m_ModifiedDate;
            }
            set
            {
                m_ModifiedDate = value;
            }
        }

        public String DisplayModifiedDate
        {
            get
            {
                if ((ModifiedDate != null) && (ModifiedDate.Length > 0))
                {
                    return Convert.ToDateTime(ModifiedDate).ToString(Common.DTP_DATE_FORMAT);
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                //Setter would do nothing. This is just to 
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        private DateTime m_DateFrom;
        public DateTime DateFrom
        {
            get
            {
                return m_DateFrom;
            }
            set
            {
                m_DateFrom = value;
            }
        }

        public String DisplayDateFrom
        {
            get
            {
                if ((DateFrom != null) && (DateFrom.ToString().Length > 0))
                {
                    return DateFrom.ToString(Common.DTP_DATE_FORMAT);
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                //Setter would do nothing. This is just to 
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        private DateTime m_DateTo;
        public DateTime DateTo
        {
            get
            {
                return m_DateTo;
            }
            set
            {
                m_DateTo = value;
            }
        }

        public String DisplayDateTo
        {
            get
            {
                if ((DateTo != null) && (DateTo.ToString().Length > 0))
                {
                    return DateTo.ToString(Common.DTP_DATE_FORMAT);
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                //Setter would do nothing. This is just to 
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }


        private IndentConsolidationBL.OperationState m_OperationState;
        public IndentConsolidationBL.OperationState OperationState
        {
            get { return m_OperationState; }
            set { m_OperationState = value; }
        }

        private int m_HeaderRecordTotal = 0;
        public int HeaderRecordTotal
        {
            get
            {
                m_HeaderRecordTotal = m_IndentConsolidationHeader.Count;
                return m_HeaderRecordTotal;
            }
            set
            {
                m_HeaderRecordTotal = m_IndentConsolidationHeader.Count;
            }
        }

        private int m_DetailRecordTotal = 0;
        public int DetailRecordTotal
        {
            get
            {
                m_DetailRecordTotal = 0;
                for (int i = 0; i < m_IndentConsolidationHeader.Count; i++)
                {
                    m_DetailRecordTotal = m_DetailRecordTotal + m_IndentConsolidationHeader[i].IndentConsolidationDetail.Count;
                }
                return m_DetailRecordTotal;
            }
            set { }
            //set
            //{
            //    for (int i = 0; i < m_IndentConsolidationHeader.Count; i++)
            //    {
            //        m_DetailRecordTotal = m_DetailRecordTotal + m_IndentConsolidationHeader[i].IndentConsolidationDetail.Count;
            //    }
            //}

        }


        private List<IndentConsolidationHeader> m_IndentConsolidationHeader = new List<IndentConsolidationHeader>();
        public List<IndentConsolidationHeader> IndentConsolidationHeader
        {
            get
            {
                return m_IndentConsolidationHeader;
            }
            set
            {
                m_IndentConsolidationHeader = value;
            }
        }

        public Double Stock { get; set; }

        //public bool Save(IIndentConsolidation objIndentConsolidation, int intConsolidationStatus, ref int intConsolidationId, ref string poNumbers, ref string toiNumbers, ref string errorMessage)
        public bool Save(IIndentConsolidation objIndentConsolidation, int intConsolidationStatus, ref int intConsolidationId, ref string errorMessage)
        {
            string USP_INDENT_SAVE = "usp_IndentConsolidationSave";
            DBParameterList dbParam;
            bool isSuccess = false;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                //dtManager.BeginTransaction();
                {
                    dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(objIndentConsolidation), DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    DataTable dt = dtManager.ExecuteDataTable(USP_INDENT_SAVE, dbParam);
                    errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                    if (errorMessage.Length > 0)
                    {
                        isSuccess = false;
                        //dtManager.RollbackTransaction();
                    }
                    else
                    {
                        if (dt == null)
                        {
                            isSuccess = false;
                            // dtManager.RollbackTransaction();
                        }
                        else
                        {
                            isSuccess = true;
                            intConsolidationId = Convert.ToInt32(Validators.CheckForDBNull(dt.Rows[0]["ConsolidationId"], Common.INT_DBNULL));
                            //poNumbers = dt.Rows[0]["PONumbers"].ToString();
                            //toiNumbers = dt.Rows[0]["TOINumbers"].ToString();
                            //dtManager.CommitTransaction();
                        }
                    }
                }
            }
            return isSuccess;
        }

        /// <summary>
        /// Search Consolidation objects to be displayed in search grid
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<IndentConsolidation> Search(IIndentConsolidation objIndentConsolidationReq, ref string errorMessage)
        {
            string USP_INDENT_SEARCH = "usp_IndentConsolidationSearch";
            List<IndentConsolidation> lstIndentConsolidation = new List<IndentConsolidation>();
            DBParameterList dbParam;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(objIndentConsolidationReq), DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_DATA2, Common.INT_DBNULL, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataTable dt = dtManager.ExecuteDataTable(USP_INDENT_SEARCH, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IndentConsolidation indentConsolidation = new IndentConsolidation();

                        // indentConsolidation.IndentNo = dt.Rows[i]["IndentNo"].ToString();
                        indentConsolidation.ConsolidationId = Convert.ToInt32(dt.Rows[i]["ConsolidationId"]);
                        indentConsolidation.Status = Convert.ToInt32(dt.Rows[i]["StatusVal"]);
                        indentConsolidation.StatusCap = Convert.ToString(dt.Rows[i]["StatusCap"]);
                        indentConsolidation.Source = Convert.ToInt32(dt.Rows[i]["SourceVal"]);
                        indentConsolidation.SourceCap = Convert.ToString(dt.Rows[i]["SourceCap"]);
                        indentConsolidation.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]).ToString(Common.DATE_TIME_FORMAT);
                        indentConsolidation.ModifiedDate = Convert.ToDateTime(dt.Rows[i]["ModifiedDate"]).ToString(Common.DATE_TIME_FORMAT);
                        lstIndentConsolidation.Add(indentConsolidation);
                    }
                }
            }
            return lstIndentConsolidation;
        }

        public List<IndentConsolidation> Search(IIndentConsolidation objIndentConsolidationReq,string PONumber,string TOINumber, ref string errorMessage)
        {
            string USP_INDENT_SEARCH = "usp_IndentConsolidationSearch";
            List<IndentConsolidation> lstIndentConsolidation = new List<IndentConsolidation>();
            DBParameterList dbParam;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(objIndentConsolidationReq), DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_DATA2, Common.INT_DBNULL, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                dbParam.Add(new DBParameter("@PONumber",PONumber, DbType.String));
                dbParam.Add(new DBParameter("@TOINumber", TOINumber, DbType.String));

                DataTable dt = dtManager.ExecuteDataTable(USP_INDENT_SEARCH, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IndentConsolidation indentConsolidation = new IndentConsolidation();

                        // indentConsolidation.IndentNo = dt.Rows[i]["IndentNo"].ToString();
                        indentConsolidation.ConsolidationId = Convert.ToInt32(dt.Rows[i]["ConsolidationId"]);
                        indentConsolidation.Status = Convert.ToInt32(dt.Rows[i]["StatusVal"]);
                        indentConsolidation.StatusCap = Convert.ToString(dt.Rows[i]["StatusCap"]);
                        indentConsolidation.Source = Convert.ToInt32(dt.Rows[i]["SourceVal"]);
                        indentConsolidation.SourceCap = Convert.ToString(dt.Rows[i]["SourceCap"]);
                        indentConsolidation.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]).ToString(Common.DATE_TIME_FORMAT);
                        indentConsolidation.ModifiedDate = Convert.ToDateTime(dt.Rows[i]["ModifiedDate"]).ToString(Common.DATE_TIME_FORMAT);
                        lstIndentConsolidation.Add(indentConsolidation);
                    }
                }
            }
            return lstIndentConsolidation;
        }
        public DataTable SearchForPOandTO(int indentConsolidationId, ref string errorMessage)
        {
            string USP_INDENT_SEARCH = "usp_IndentConsolidationSearch";

            DBParameterList dbParam;
            DataTable dt = null;
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_DATA, string.Empty, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_DATA2, indentConsolidationId, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                dt = dtManager.ExecuteDataTable(USP_INDENT_SEARCH, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
            }

            return dt;
        }

        #endregion
    }
    #endregion

    #region IndentConsolidationHeader
    public class IndentConsolidationHeader : IIndentConsolidationHeader
    {
        #region CTOR
        public IndentConsolidationHeader() { }

        public IndentConsolidationHeader(
            int headerId,
            int itemId,
            int lineNo,
            string indentNo,
            double approvedQty,
            int forLocationId,
            int consolidationId,
            List<IndentConsolidationDetail> indentConsolidationDetailList)
        {
            this.m_HeaderId = headerId;
            this.m_ItemID = itemId;
            this.m_LineNo = lineNo;
            this.m_IndentNo = indentNo;
            this.m_ApprovedQty = approvedQty;
            this.m_ForLocationId = forLocationId;
            this.m_ConsolidationId = consolidationId;
            this.m_IndentConsolidationDetail = indentConsolidationDetailList;
        }
        #endregion

        #region IIndentConsolidationHeader Members

        private int m_HeaderId;
        public int HeaderId
        {
            get
            {
                return m_HeaderId;
            }
            set
            {
                m_HeaderId = value;
            }
        }

        private int m_ItemID;
        public int ItemId
        {
            get
            {
                return m_ItemID;
            }
            set
            {
                m_ItemID = value;
            }
        }

        private int m_LineNo;
        public int LineNo
        {
            get
            {
                return m_LineNo;
            }
            set
            {
                m_LineNo = value;
            }
        }

        private string m_IndentNo;
        public string IndentNo
        {
            get
            {
                return m_IndentNo;
            }
            set
            {
                m_IndentNo = value;
            }
        }

        private double m_ApprovedQty;
        public double ApprovedQty
        {
            get
            {
                return m_ApprovedQty;
            }
            set
            {
                m_ApprovedQty = value;
            }
        }

        private int m_ForLocationId;
        public int ForLocationId
        {
            get
            {
                return m_ForLocationId;
            }
            set
            {
                m_ForLocationId = value;
            }
        }

        private int m_ConsolidationId;
        public int ConsolidationId
        {
            get
            {
                return m_ConsolidationId;
            }
            set
            {
                m_ConsolidationId = value;
            }
        }

        List<IndentConsolidationDetail> m_IndentConsolidationDetail;
        public List<IndentConsolidationDetail> IndentConsolidationDetail
        {
            get
            {
                return m_IndentConsolidationDetail;
            }
            set
            {
                m_IndentConsolidationDetail = value;
            }
        }

        #endregion

    }
    #endregion

    #region IndentConsolidationDetail
    public class IndentConsolidationDetail : IIndentConsolidationDetail
    {
        #region CTOR
        public IndentConsolidationDetail() { }

        public IndentConsolidationDetail(
            int headerId,
            int detailId,
            int lineNo,
            int status,
            int recordType,
            double quantity,
            int vendorId,
            int deliveryLocationId,
            int transferFromLocationId,
            bool isFormC
            )
        {
            this.m_HeaderId = headerId;
            this.m_DetailId = detailId;
            this.m_LineNo = lineNo;
            this.m_Status = status;
            this.m_RecordType = recordType;
            this.m_Quantity = quantity;
            this.m_VendorId = vendorId;
            this.m_DeliveryLocationId = deliveryLocationId;
            this.m_TransferFromLocationId = transferFromLocationId;
            this.IsFormC = isFormC;

        }
        #endregion

        #region IIndentConsolidationDetail Members

        private int m_HeaderId;
        public int HeaderId
        {
            get
            {
                return m_HeaderId;
            }
            set
            {
                m_HeaderId = value;
            }
        }


        private int m_DetailId;
        public int DetailId
        {
            get
            {
                return m_DetailId;
            }
            set
            {
                m_DetailId = value;
            }
        }

        private int m_LineNo;
        public int LineNo
        {
            get
            {
                return m_LineNo;
            }
            set
            {
                m_LineNo = value;
            }
        }

        private int m_Status;
        public int Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
            }
        }


        private int m_RecordType;
        public int RecordType
        {
            get
            {
                return m_RecordType;
            }
            set
            {
                m_RecordType = value;
            }
        }

        private double m_Quantity;
        public double Quantity
        {
            get
            {
                return m_Quantity;
            }
            set
            {
                m_Quantity = value;
            }
        }

        private int m_VendorId;
        public int VendorId
        {
            get
            {
                return m_VendorId;
            }
            set
            {
                m_VendorId = value;
            }
        }

        private int m_DeliveryLocationId;
        public int DeliveryLocationId
        {
            get
            {
                return m_DeliveryLocationId;
            }
            set
            {
                m_DeliveryLocationId = value;
            }
        }

        private int m_TransferFromLocationId;
        public int TransferFromLocationId
        {
            get
            {
                return m_TransferFromLocationId;
            }
            set
            {
                m_TransferFromLocationId = value;
            }
        }
        private bool m_IsFormC;
        public bool IsFormC
        {
            get
            {
                return m_IsFormC;
            }
            set
            {
                m_IsFormC = value;
            }
        }



        #endregion
    }
    #endregion

    #region Business Functions
    public class IndentConsolidationBL
    {
        public const string MODULE_CODE = "PUR04";
        public const string FUNCTION_CODE_SAVE = "F006";
        public const string FUNCTION_CODE_CANCEL = "F008";
        public const string FUNCTION_CODE_CONFIRM = "F007";
        public const string FUNCTION_CODE_SEARCH = "F001";

        private const string SP_APPROVEDITEM_SEARCH = "usp_GetItemForConsolidation";
        private const string SP_ITEMLOCATIONWAREHOUSE_SEARCH = "usp_ItemLocationWarehouse_Search";
        private const string SP_ITEMS_UNDER_CONSOLIDATION_SEARCH = "usp_ItemUnderConsolidation_Search";

        /// <summary>
        /// Get items to be consolidated
        /// </summary>
        /// <param name="IsItemUnderConsolidationExists"></param>
        /// <returns></returns>
        //public static List<Item> GetUnderConsolidationItems(int intConsolidationId, ref Boolean IsItemUnderConsolidationExists)
        public static List<Item> GetUnderConsolidationItems(int intConsolidationId, ref Boolean IsItemUnderConsolidationExists, ref List<Item> lstApprovedIndentItems)
        {
            IsItemUnderConsolidationExists = true;
            List<Item> lstItemsForConsolidation = new List<Item>();
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                //Declare and initialize the parameter list object.
                dbParam = new DBParameterList();

                //Add the relevant 2 parameters
                dbParam.Add(new DBParameter("@ConsolidationId", intConsolidationId, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                    ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_ITEMS_UNDER_CONSOLIDATION_SEARCH, dbParam);

                //Populate the List Item from the grid
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Item tmpItem = new Item();
                            tmpItem.ItemId = Convert.ToInt32(ds.Tables[0].Rows[i]["ITEMID"]);
                            tmpItem.ItemCode = ds.Tables[0].Rows[i]["ITEMCODE"].ToString();
                            tmpItem.ItemName = ds.Tables[0].Rows[i]["ITEMNAME"].ToString();
                            tmpItem.Selected = false;
                            tmpItem.IsComposite = Convert.ToBoolean(ds.Tables[0].Rows[i]["ISCOMPOSITE"]);
                            lstItemsForConsolidation.Add(tmpItem);
                            IsItemUnderConsolidationExists = true;
                        }
                    }
                    else
                    {
                        IsItemUnderConsolidationExists = false;
                    }

                    if (ds.Tables[1] != null)
                    {
                        foreach (DataRow drRow in ds.Tables[1].Rows)
                        {
                            Item tmpApprovedItem = new Item();
                            tmpApprovedItem.ItemId = Convert.ToInt32(drRow["ITEMID"]);
                            tmpApprovedItem.ItemCode = drRow["ITEMCODE"].ToString();
                            tmpApprovedItem.ItemName = drRow["ITEMNAME"].ToString();
                            tmpApprovedItem.Selected = false;
                            tmpApprovedItem.IsComposite = Convert.ToBoolean(drRow["ISCOMPOSITE"]);
                            lstApprovedIndentItems.Add(tmpApprovedItem);
                        }
                    }
                }
                return lstItemsForConsolidation;
                //}
                //else
                //{

                //    if (ds.Tables.Count >0 && ds.Tables[1]!=null)
                //    {
                //        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                //        {
                //            Item tmpItem = new Item();
                //            tmpItem.ItemId = Convert.ToInt32(ds.Tables[1].Rows[i]["ITEMID"]);
                //            tmpItem.ItemCode = ds.Tables[1].Rows[i]["ITEMCODE"].ToString();
                //            tmpItem.ItemName = ds.Tables[1].Rows[i]["ITEMNAME"].ToString();
                //            tmpItem.Selected = false;
                //            tmpItem.IsComposite = Convert.ToBoolean(ds.Tables[1].Rows[i]["ISCOMPOSITE"]);
                //            lstItemsForConsolidation.Add(tmpItem);
                //        }
                //    }
                //}                  
                //return lstItemsForConsolidation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Get indent consolidation Data
        /// </summary>
        /// <param name="indentConsolidationRequest"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public DataSet GetIndentConsolidationData(IndentConsolidationRequest indentConsolidationRequest, ref String errorMessage)
        {
            string SP_NAME = "usp_GetIndentConsolidationData";
            DataSet dsTemp = new DataSet();
            //Get Items and Warehouse items detail
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                //Declare and initialize the parameter list object.
                DBParameterList dbParam = new DBParameterList();

                //Add the relevant 2 parameters
                dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(indentConsolidationRequest), DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                    ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                dsTemp = dtManager.ExecuteDataSet(SP_NAME, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (string.IsNullOrEmpty(errorMessage))
                {
                    if (dsTemp.Tables.Count > 0)
                    {
                        if (dsTemp.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drRow in dsTemp.Tables[0].Rows)
                            {
                                if (!string.IsNullOrEmpty(drRow["ApprovedQty"].ToString()))
                                {
                                    drRow["ApprovedQty"] = Math.Round(Convert.ToDouble(drRow["ApprovedQty"].ToString()), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero).ToString();
                                }
                                if (!string.IsNullOrEmpty(drRow["TOIQty"].ToString()))
                                {
                                    drRow["TOIQty"] = Math.Round(Convert.ToDouble(drRow["TOIQty"].ToString()), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero).ToString();
                                }
                                if (!string.IsNullOrEmpty(drRow["POQty"].ToString()))
                                {
                                    drRow["POQty"] = Math.Round(Convert.ToDouble(drRow["POQty"].ToString()), Common.DisplayQtyRounding, MidpointRounding.AwayFromZero).ToString();
                                }
                                if (!string.IsNullOrEmpty(drRow["WarehouseQtySum"].ToString()))
                                {
                                    drRow["WarehouseQtySum"] = Math.Round(Convert.ToDouble(drRow["WarehouseQtySum"].ToString()), Common.DisplayAmountRounding, MidpointRounding.AwayFromZero).ToString();
                                }
                            }
                        }
                    }
                }
            }

            return dsTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstItemWarehouseRequest"></param>
        /// <param name="dtItemsDetails"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public DataTable GetItemWarehouse(List<Item> lstItemWarehouseRequest, ref String errorMessage)
        {
            string SP_NAME = "usp_GetItemsWarehouseQuantities";
            DataTable dtTemp = new DataTable();
            //Get Items and Warehouse items detail
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                //Declare and initialize the parameter list object.
                DBParameterList dbParam = new DBParameterList();

                //Add the relevant 2 parameters
                dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(lstItemWarehouseRequest), DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                    ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                dtTemp = dtManager.ExecuteDataTable(SP_NAME, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
            }

            return dtTemp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstItemWarehouseRequest"></param>
        /// <param name="dtItemsDetails"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public DataTable GetItemVendor(List<Item> lstItemWarehouseRequest, ref String errorMessage)
        {
            string SP_NAME = "usp_GetItemVendor";
            DataTable dtTemp = new DataTable();
            //Get Items and Warehouse items detail
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                //Declare and initialize the parameter list object.
                DBParameterList dbParam = new DBParameterList();

                //Add the relevant 2 parameters
                dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(lstItemWarehouseRequest), DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                    ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                dtTemp = dtManager.ExecuteDataTable(SP_NAME, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
            }

            return dtTemp;
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstItemWarehouseRequest"></param>
        /// <param name="dtItemsDetails"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public DataTable GetItemVendorWarehouseInfo(List<Item> lstItemWarehouseRequest, ref String errorMessage)
        {
            string SP_NAME = "usp_GetItemVendorWarehouseInfo";
            DataTable dtTemp = new DataTable();
            //Get Items and Warehouse items detail
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                //Declare and initialize the parameter list object.
                DBParameterList dbParam = new DBParameterList();

                //Add the relevant 2 parameters
                dbParam.Add(new DBParameter(Common.PARAM_DATA, Common.ToXml(lstItemWarehouseRequest), DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                    ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                dtTemp = dtManager.ExecuteDataTable(SP_NAME, dbParam);
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
            }

            return dtTemp;
        }

        public enum OperationState
        {
            New
        }

        private void ValidateQty(System.Windows.Forms.TextBox txt, System.Windows.Forms.Label lbl, System.Windows.Forms.ErrorProvider ep)
        {
            int pvVal = -999;
            pvVal = Int32.TryParse(txt.Text, out pvVal) ? pvVal : -1;

            if (pvVal > -1)
            {
                if (pvVal < 1)
                {
                    Validators.SetErrorMessage(ep, txt, "VAL0080", "Point Value", "1");
                }
            }
            else
            {
                //decimal val2 = Decimal.TryParse(txt.Text, out val2);
                //if (val2 > 0)
                //{
                //}
                //else
                //{
                //    Validators.SetErrorMessage(ep, txt, "VAL0081", lbl.Text);
                //}

                Validators.SetErrorMessage(ep, txt, "VAL0081", lbl.Text);
            }
        }
        
        // Added By Garima (11 Dec 2009)
        public List<Item> GetAppovedItemsForConsolidation()
        {
            List<Item> lstItemsForConsolidation = new List<Item>();
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                //Declare and initialize the parameter list object.
                dbParam = new DBParameterList();

                //Add the relevant 2 parameters
                dbParam.Add(new DBParameter("@ItemId", -1, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                    ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_APPROVEDITEM_SEARCH, dbParam);

                //Populate the List Item from the grid
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null)
                    {
                        foreach (DataRow drRow in ds.Tables[0].Rows)
                        {
                            Item tmpApprovedItem = new Item();
                            tmpApprovedItem.ItemId = Convert.ToInt32(drRow["ITEMID"]);
                            tmpApprovedItem.ItemCode = drRow["ITEMCODE"].ToString();
                            tmpApprovedItem.ItemName = drRow["ITEMNAME"].ToString();
                            tmpApprovedItem.Selected = false;
                            tmpApprovedItem.IsComposite = Convert.ToBoolean(drRow["ISCOMPOSITE"]);
                            tmpApprovedItem.ApprovedQty = Convert.ToDecimal(drRow["APPROVEDQTY"]);
                            lstItemsForConsolidation.Add(tmpApprovedItem);
                        }
                    }
                }
                return lstItemsForConsolidation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIndentDetailForItem(int ItemID)
        {
            List<Item> lstItemsForConsolidation = new List<Item>();
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                //Declare and initialize the parameter list object.
                dbParam = new DBParameterList();

                //Add the relevant 2 parameters
                dbParam.Add(new DBParameter("@ItemId", ItemID, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                    ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet ds = dtManager.ExecuteDataSet(SP_APPROVEDITEM_SEARCH, dbParam);

                //Populate the List Item from the grid
                if (ds != null && ds.Tables.Count > 0)
                {
                   
                        return ds.Tables[0];
                   
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IndentConsolidation GetConsolidationDetail(int ConsolidationId,bool IsUnderConsolidation)
        {
            
            IndentConsolidation consolidation = null;
            string SP_NAME = "usp_GetConsolidationDetail";
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                //Declare and initialize the parameter list object.
                DBParameterList dbParam = new DBParameterList();

                //Add the relevant 2 parameters
                dbParam.Add(new DBParameter("@ConsolidationId", ConsolidationId, DbType.Int32));
                dbParam.Add(new DBParameter("@UnderConsolidation", IsUnderConsolidation, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,     ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataSet dsTemp = dtManager.ExecuteDataSet(SP_NAME, dbParam);
                string errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                if (string.IsNullOrEmpty(errorMessage) && dsTemp.Tables.Count > 0)
                {
                    if (dsTemp.Tables[0].Rows.Count > 0)
                    {
                        consolidation = new IndentConsolidation();
                        DataRow dr=dsTemp.Tables[0].Rows[0];
                        consolidation.ConsolidationId = Convert.ToInt32(dr["ConsolidationId"]);                        
                        consolidation.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                        consolidation.Source = Convert.ToInt32(dr["SourceVal"]);
                        consolidation.SourceCap = Convert.ToString(dr["SourceCap"]);
                        consolidation.Status = Convert.ToInt32(dr["StatusVal"]);
                        consolidation.StatusCap = Convert.ToString(dr["StatusCap"]);
                        consolidation.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
                    }
                }
                return consolidation;
            }
        }

        public DataSet GetCompleteConsolidation(int ConsolidationId)
        {

            //IndentConsolidation consolidation = null;
            string SP_NAME = "usp_GetConsolidationDetail";
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                //Declare and initialize the parameter list object.
                DBParameterList dbParam = new DBParameterList();

                //Add the relevant 2 parameters
                dbParam.Add(new DBParameter("@ConsolidationId", ConsolidationId, DbType.Int32));
                dbParam.Add(new DBParameter("@UnderConsolidation", false, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                DataSet dsTemp = dtManager.ExecuteDataSet(SP_NAME, dbParam);
                string errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                //if (string.IsNullOrEmpty(errorMessage) && dsTemp.Tables.Count > 0)
                //{
                //    return dsTemp;
                //    //if (dsTemp.Tables[0].Rows.Count > 0)
                //    //{
                //    //    consolidation = new IndentConsolidation();
                //    //    DataRow dr = dsTemp.Tables[0].Rows[0];
                //    //    consolidation.ConsolidationId = Convert.ToInt32(dr["ConsolidationId"]);
                //    //    consolidation.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                //    //    consolidation.Source = Convert.ToInt32(dr["SourceVal"]);
                //    //    consolidation.SourceCap = Convert.ToString(dr["SourceCap"]);
                //    //    consolidation.Status = Convert.ToInt32(dr["StatusVal"]);
                //    //    consolidation.StatusCap = Convert.ToString(dr["StatusCap"]);
                //    //    consolidation.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
                //    //}
                //    //if (dsTemp.Tables.Count == 2)
                //    //    dtConsolidationDetail = dsTemp.Tables[1];

                //}
                return dsTemp;
                //return consolidation;
            }
        }

        public static IndentConsolidation GetIndentConsolidationObject(DataRow dr)
        {
            if (dr!=null)
            {
                IndentConsolidation consolidation = new IndentConsolidation();             
                consolidation.ConsolidationId = Convert.ToInt32(dr["ConsolidationId"]);
                consolidation.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                consolidation.Source = Convert.ToInt32(dr["SourceVal"]);
                consolidation.SourceCap = Convert.ToString(dr["SourceCap"]);
                consolidation.Status = Convert.ToInt32(dr["StatusVal"]);
                consolidation.StatusCap = Convert.ToString(dr["StatusCap"]);
                consolidation.ModifiedDate = Convert.ToString(dr["ModifiedDate"]);
                return consolidation;
            }
            return null;
        }
    }
        
        #endregion

        #region IndentRequestObject
        public class IndentConsolidationRequest
        {
            private int m_ConsolidationId = Common.INT_DBNULL;
            public int ConsolidationId
            {
                get
                {
                    return m_ConsolidationId;
                }
                set
                {
                    m_ConsolidationId = value;
                }
            }

            private List<Item> m_RequestedItems = new List<Item>();
            public List<Item> RequestedItems
            {
                get { return m_RequestedItems; }
                set { m_RequestedItems = value; }

            }

            private int m_Operation;
            public int Operation
            {
                get
                {
                    return m_Operation;
                }
                set
                {
                    m_Operation = value;
                }
            }

            List<Item> lstItems = new List<Item>();
        }
        #endregion

        #region "Item Class"
        public class Item
        {
            private Boolean m_Selected;
            public Boolean Selected
            {
                get { return m_Selected; }
                set { m_Selected = value; }
            }

            private int m_ItemId;
            public int ItemId
            {
                get { return m_ItemId; }
                set { m_ItemId = value; }
            }

            private string m_ItemCode;
            public string ItemCode
            {
                get { return m_ItemCode; }
                set { m_ItemCode = value; }
            }

            private string m_ItemName;
            public string ItemName
            {
                get { return m_ItemName; }
                set { m_ItemName = value; }
            }

            private Boolean m_IsComposite;
            public Boolean IsComposite
            {
                get { return m_IsComposite; }
                set { m_IsComposite = value; }
            }
            public Decimal ApprovedQty { get; set; }
            
        }
        #endregion

  
}
