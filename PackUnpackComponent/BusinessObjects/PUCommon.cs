using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace PackUnpackComponent.BusinessObjects
{
 
    [Serializable]
   public class PUCommon
    {
        #region Constants
        public const string MODULE_CODE = "PUN01";
        public const string Pack_SAVE = "usp_PackSave";
        public const string UNPack_SAVE = "usp_UnpackSave";
        public const string PUVocher_SEARCH = "usp_GetPUVoucherSearch";
        public const string DATE_FORMAT_yyy_MM_dd= "yyyy-MM-dd";
        public const string SEARCH_PUVOUCHER = "PU";
        public const string SEARCH_COMPOSITE = "COMPOSITE";
        public const string SEARCH_CONSTITUENT = "CONSTITUENT";
        public const string COlOUMN_COMPOSITE_ITEMID = "ItemId";
        public const string SELECTED_TAB_SEARCH = "Search";
        public const string SELECTED_TAB_CREATE = "Create";
        public const string COlOUMN_ISPACKVOUCHER = "IsPackVoucher";
        public const string COMBO_PACKUNPACK = "PACKUNPACK";
        public const int COMBO_PACK = 0;
        public const int COMBO_UNPACK = 1;
        public const string DISPLAY_PACK = "Pack";
        public const string DISPLAY_UNPACK = "Unpack";
        public const string GRID_ITEMID_COL = "ItemId";

        public const int GOOD_BUCKET = 5;
        
        public const int All_PURecords = -1;
        public const int Only_PackRecords = 0;
        public const int Only_UnPackRecords = 1;
        
        
        //GridView definition XML path
        public const string GRIDVIEW_DEFINITION_XML_PATH = "\\App_Data\\GridViewDefinition_PUVoucherSearch.xml";

        #endregion Constants
        public string FromDate
        { get; set; }

        public String DisplayFromDate
        {
            get
            {
                if ((FromDate != null) && (FromDate.Length > 0))
                {
                    return Convert.ToDateTime(FromDate).ToString(Common.DTP_DATE_FORMAT);
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public string ToDate
        { get; set; }

        public String DisplayToDate
        {
            get
            {
                if ((ToDate != null) && (ToDate.Length > 0))
                {
                    return Convert.ToDateTime(ToDate).ToString(Common.DTP_DATE_FORMAT);
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public int PU_All
        { get; set; }
        public string SearchParam
        {get;set;}
        public string ItemCode
        { get; set; }
        public int LocationId
        { get; set; }
        public int ItemId
        { get; set; }
        public string PUNo
        { get; set; }
        public PUHeader ObjPUHeader
        { get; set; }
        public CompositeItem ObjCCompositeItem
        { get; set; }
        /// <summary>
        /// Search  pack/unpack records from
        /// databse base on search conditions
        /// 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="spName"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<PUHeader> Search_Header_Detail(string xmlDoc, string spName, ref string errorMessage)
        {
            List<PUHeader> listPUHeader = new List<PUHeader>();
            try
            {
                DataTaskManager dt = new DataTaskManager();

                 using (DataTaskManager dtManager = new DataTaskManager())
                 {
                     DBParameterList dbParam = new DBParameterList();

                     dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                     dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                         ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                     using (DataSet ds = dtManager.ExecuteDataSet(spName, dbParam))
                     {
                         errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                         //ds.WriteXml("c:\\aaaa.xml");
                         if (errorMessage.Length == 0 && ds != null && ds.Tables.Count > 0) //No dbError
                            {
                                foreach (DataRow drHeader in ds.Tables[0].Rows)
                                {
                                    PUHeader objPUHeader = new PUHeader();
                                    objPUHeader.ItemName = Convert.ToString(drHeader["ItemName"]);
                                    objPUHeader.LocationId = Convert.ToInt32(drHeader["LocationId"]);
                                    objPUHeader.ModifiedDate = Convert.ToDateTime(drHeader["ModifiedDate"]).ToString(Common.DATE_TIME_FORMAT);
                                    objPUHeader.PUNo = Convert.ToString(drHeader["PUNo"]);
                                    objPUHeader.CompositeItemId = Convert.ToInt32(drHeader["CompositeItemId"]);
                                    objPUHeader.Quantity = Convert.ToInt32(drHeader["Quantity"]);
                                    objPUHeader.Remarks = Convert.ToString(drHeader["Remarks"]);
                                    objPUHeader.IsPackVoucher = Convert.ToBoolean(drHeader["PU_Flag"]);
                                    if (objPUHeader.IsPackVoucher)
                                        objPUHeader.DisplayIsPackVoucher = PUCommon.DISPLAY_PACK;
                                    else
                                        objPUHeader.DisplayIsPackVoucher = PUCommon.DISPLAY_UNPACK;


                                    objPUHeader.ItemCode = Convert.ToString(drHeader["ItemCode"]);
                                    objPUHeader.PUDate = Convert.ToDateTime(drHeader["PUDate"]).ToString(PUCommon.DATE_FORMAT_yyy_MM_dd);

                                    DataRow[] detailList = ds.Tables[1].Select("PUNo = '" + objPUHeader.PUNo + "'");

                                    List<PUDetail> listPUDetail = new List<PUDetail>();
                                    if(detailList.Length>0)
                                    {
                                        foreach (DataRow drDetail in detailList)
                                        {
                                            PUDetail objPUDetail = new PUDetail();
                                            objPUDetail.PUNo = Convert.ToString(drDetail["PUNo"]);
                                            objPUDetail.ItemName = Convert.ToString(drDetail["ItemName"]);
                                            objPUDetail.ItemId = Convert.ToInt32(drDetail["ItemId"]);
                                            objPUDetail.SeqNo = Convert.ToInt32(drDetail["SeqNo"]);
                                            objPUDetail.Quantity = Convert.ToInt32(drDetail["Quantity"]);
                                            objPUDetail.ItemCode = Convert.ToString(drDetail["ItemCode"]);
                                            listPUDetail.Add(objPUDetail);
                                        }
                                    
                                    }

                                    objPUHeader.DetailItem = listPUDetail;

                                    listPUHeader.Add(objPUHeader);

                                }

                            }
                      }

                 }
                 return listPUHeader;
               

            }
            catch (Exception ex)
            {

                throw ex;
            }
        
        }
        /// <summary>
        /// Search Header & Detail and return Dataset
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="spName"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public DataSet Search_Header_Detail_DatSet(string xmlDoc, string spName, ref string errorMessage)
        {
            
            try
            {
                DataTaskManager dt = new DataTaskManager();
                DataSet ds = new DataSet();
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                    ds = dtManager.ExecuteDataSet(spName, dbParam);
                    {
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                        if (errorMessage != string.Empty)
                            return null;
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw ex;
            }
        }
        /// <summary>
        /// search composite item details 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="spName"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public List<CompositeItem> Search_CompositeItem(string xmlDoc, string spName, ref string errorMessage)
        {
            List<CompositeItem> listCompositeItem = new List<CompositeItem>();
            try
            {
                DataTaskManager dt = new DataTaskManager();

                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam = new DBParameterList();

                    dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                        ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    using (DataSet ds = dtManager.ExecuteDataSet(spName, dbParam))
                    {
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                        if (errorMessage.Length == 0 && ds != null && ds.Tables.Count > 0) //No dbError
                        {
                            foreach (DataRow drCompositeItem in ds.Tables[0].Rows)
                            {
                                CompositeItem objCompositeItem = new CompositeItem();
                                objCompositeItem.ItemName = Convert.ToString(drCompositeItem["ItemName"]);
                                objCompositeItem.ItemId = Convert.ToInt32(drCompositeItem["ItemId"]);
                                objCompositeItem.ItemCode = Convert.ToString(drCompositeItem["ItemCode"]);
                                objCompositeItem.Shortname = Convert.ToString(drCompositeItem["Shortname"]);



                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    DataRow[] drBatchDetails = ds.Tables[1].Select("ItemId=" + objCompositeItem.ItemId);
                                    if (drBatchDetails.Length > 0)
                                    {
                                        List<BatchDetails> listBatchDetails = new List<BatchDetails>();
                                        foreach (DataRow drBatchDetail in drBatchDetails)
                                        {


                                            BatchDetails objBatchDetails = new BatchDetails();
                                            objBatchDetails.ItemId = Convert.ToInt32(drBatchDetail["ItemId"]);
                                            objBatchDetails.ItemName = Convert.ToString(drBatchDetail["ItemName"]);
                                         
                                            objBatchDetails.ItemCode = Convert.ToString(drBatchDetail["ItemCode"]);
                                            objBatchDetails.AvailableQty = Convert.ToInt32(drBatchDetail["AvailableQty"]);
                                            objBatchDetails.BatchNo = Convert.ToString(drBatchDetail["BatchNo"]);
                                            objBatchDetails.MfgBatchNo = Convert.ToString(drBatchDetail["ManufactureBatchNo"]);
                                            objBatchDetails.ExpDate = Convert.ToDateTime(drBatchDetail["ExpDate"]).ToString(PUCommon.DATE_FORMAT_yyy_MM_dd);
                                            objBatchDetails.MfgDate = Convert.ToDateTime(drBatchDetail["MfgDate"]).ToString(PUCommon.DATE_FORMAT_yyy_MM_dd);
                                            objBatchDetails.MRP = Convert.ToString(drBatchDetail["MRP"]);

                                            listBatchDetails.Add(objBatchDetails);


                                        }
                                        objCompositeItem.ListBatchDetails = listBatchDetails;
                                    }
                                }

                                listCompositeItem.Add(objCompositeItem);

                            }


                        }
                    }
                    return listCompositeItem;


                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// save pack/unpack details 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="spName"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool PUSave(string xmlDoc, string spName, ref string errorMessage)
        {
            bool isSuccess = false;
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {

                    //Declare and initialize the parameter list object
                    DBParameterList dbParam = new DBParameterList();

                    //Add the relevant 2 parameters
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                        ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    try
                    {
                        //Begin the transaction and executing procedure to save the record(s) 
                        dtManager.BeginTransaction();

                        // executing procedure to save the record 

                        DataSet ds;

                        ds = dtManager.ExecuteDataSet(spName, dbParam);

                        //Update database message
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                        //If an error returned from the database
                        if (errorMessage.Length > 0)
                        {
                            dtManager.RollbackTransaction();
                            isSuccess = false;
                        }
                        else
                        {
                            dtManager.CommitTransaction();
                            isSuccess = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        dtManager.RollbackTransaction();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSuccess;
        }
        /// <summary>
        /// Search constituent
        /// </summary>
        /// <param name="objCompositeItem"></param>
        /// <param name="xmlDoc"></param>
        /// <param name="spName"></param>
        /// <param name="errorMessage"></param>
        public void Search_CompositeBOM(CompositeItem objCompositeItem, string xmlDoc, string spName, ref string errorMessage)
        {
            List<CompositeBOM> listCompositeBOM = new List<CompositeBOM>();
            try
            {
                                DataTaskManager dt = new DataTaskManager();

                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam = new DBParameterList();

                    dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                        ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    using (DataSet ds = dtManager.ExecuteDataSet(spName, dbParam))
                    {
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                        if (errorMessage.Length == 0 && ds != null && ds.Tables.Count > 0) //No dbError
                        {
                        
                            //Exp date
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                objCompositeItem.ExpDate=Convert.ToDateTime(ds.Tables[2].Rows[0][0]).ToString(Common.DATE_TIME_FORMAT);
 
                            }
                            foreach (DataRow drConstituent in  ds.Tables[0].Rows)
                            {
                                CompositeBOM objCompositeBOM = new CompositeBOM();
                                objCompositeBOM.ItemId = Convert.ToInt32(drConstituent["ItemId"]);
                                objCompositeBOM.ItemName = Convert.ToString(drConstituent["ItemName"]);
                                objCompositeBOM.CompositeItemId = Convert.ToInt32(drConstituent["CompositeItemID"]);
                                objCompositeBOM.ItemCode = Convert.ToString(drConstituent["ItemCode"]);
                                objCompositeBOM.ShortName = Convert.ToString(drConstituent["Shortname"]);
                              
                                objCompositeBOM.Quantity = Convert.ToInt32(drConstituent["Quantity"]);
                                objCompositeBOM.AvailableQty = Convert.ToInt32(drConstituent["AvailableQty"]);

                                

                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    DataRow[] drBatchDetails = ds.Tables[1].Select("ItemId=" + objCompositeBOM.ItemId );
                                    if (drBatchDetails.Length > 0)
                                    {
                                        List<BatchDetails> listBatchDetails = new List<BatchDetails>();
                                        foreach (DataRow drBatchDetail in drBatchDetails)
                                        {
                                            

                                            BatchDetails objBatchDetails = new BatchDetails();
                                            objBatchDetails.ItemId = Convert.ToInt32(drBatchDetail["ItemId"]);
                                            objBatchDetails.ItemName = Convert.ToString(drBatchDetail["ItemName"]);
                                            objBatchDetails.CompositeItemId = Convert.ToInt32(drBatchDetail["CompositeItemID"]);
                                            objBatchDetails.ItemCode = Convert.ToString(drBatchDetail["ItemCode"]);
                                            objBatchDetails.AvailableQty = Convert.ToInt32(drBatchDetail["AvailableQty"]);
                                            objBatchDetails.BatchNo = Convert.ToString(drBatchDetail["BatchNo"]);
                                            objBatchDetails.MfgBatchNo = Convert.ToString(drBatchDetail["ManufactureBatchNo"]);
                                            objBatchDetails.ExpDate=Convert.ToDateTime(drBatchDetail["ExpDate"]).ToString(PUCommon.DATE_FORMAT_yyy_MM_dd);
                                            objBatchDetails.MfgDate=Convert.ToDateTime(drBatchDetail["MfgDate"]).ToString(PUCommon.DATE_FORMAT_yyy_MM_dd);
                                            objBatchDetails.MRP=Convert.ToString(drBatchDetail["MRP"]);
                                         
                                            listBatchDetails.Add(objBatchDetails);

                                            
                                        }
                                        objCompositeBOM.ListAllBatchDetails = listBatchDetails;
                                    }

                                    listCompositeBOM.Add(objCompositeBOM);
                                       
 
                                }
                                

                            }
                            objCompositeItem.CompositeDetail = listCompositeBOM;

                        }
                    }

                }
                


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// Get id of good bucket
        /// </summary>
        /// <param name="yesNo"></param>
        public int FillBucket()
        {
            DataView dv = new DataView();
            DataTable dt = new DataTable();
            DataTable m_dtSearchAllBucket = new DataTable();
            m_dtSearchAllBucket = Common.ParameterLookup(Common.ParameterType.AllSubBuckets, new ParameterFilter(string.Empty, 0, 0, 0));
            if (m_dtSearchAllBucket != null && m_dtSearchAllBucket.Rows.Count > 0)
            {
                //m_dtSearchAllBucket = m_dtSearchAllBucket.AsEnumerable().Distinct().CopyToDataTable();
                //dt = m_dtSearchAllBucket.Select("BucketId=" + CoreComponent.Core.BusinessObjects.Common.ON_HAND_SUB_BUCKET_ID).AsEnumerable().Distinct().CopyToDataTable();
                //dt.Rows.RemoveAt(0);
                dv = new DataView(m_dtSearchAllBucket.DefaultView.ToTable(true, "BucketId", "BucketName", "Sellable"));
                dv.RowFilter = "Sellable=1";
            }

            if (dv != null && dv.Count > 0)
            {
                return Convert.ToInt32(dv[0][0]);
            }
            else
                return Common.INT_DBNULL;
        }
    }
}
