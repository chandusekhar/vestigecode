using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using System.Data;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace ReturnsComponent.BusinessObjects
{
    [Serializable]
    public class RetVendorCommon:Return
    {
        public const string PO_DETAILS = "PODETAILS";
        public const string RTVHEADER= "RTVHEADER";
        public const string RTVDETAILS = "RTVDETAIL";
        
        public const string SP_RETURN_VENDOR_DETAIL = "VENDORDETAIL";
        public const string SP_GET_VENDOR_DETAILS = "usp_GetReturnToVendorDetails";
        public const string SP_SAVE_VENDOR_DETAILS = "usp_ReturnToVendorSave";
        public const string SP_CHECK_RET_QTY = "usp_ReturnToVendor_CheckReturnQty";
        public const string CREATE_MODE = "Create";
        public const string EDIT_MODE = "Edit";


        public RetVendorCommon()
        {

        }
     
        public string BatchNo
        { get; set; }
        public string SelectMode
        { get; set; }
        public string FromDate
        { get; set; }
        public string ManufactureBatchNo
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

        public int LocationId
        { get; set; }
        public int VendorId
        { get; set; }
        public int Status
        { get; set; }
        public int ItemId
        { get; set; }
        public string DebitNoteNumber
        { get; set; }


        public List<RetVendorDetails> GetVendorDetail(string xmlDoc, string spName, ref string errorMessage)
        {
            List<RetVendorDetails> listDeatil;
            try
            {
                listDeatil = new List<RetVendorDetails>();
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
                            foreach (DataRow drDeatils in ds.Tables[0].Rows)
                            {
                                RetVendorDetails obj = new RetVendorDetails();

                                obj.BatchNo = Convert.ToString(drDeatils["BatchNo"]);
                                obj.ItemDescription = Convert.ToString(drDeatils["ItemName"]);
                                obj.ItemCode = Convert.ToString(drDeatils["ItemCode"]);
                                obj.PODate = Convert.ToDateTime(drDeatils["PODate"]).ToString(Common.DATE_TIME_FORMAT);
                                //obj.DisplayPODate = Convert.ToDateTime(drDeatils["PODate"]).ToString(Common.DTP_DATE_FORMAT);
                                obj.ItemId = Convert.ToInt32(drDeatils["ItemId"]);
                                obj.POQty = Convert.ToDouble(drDeatils["POQty"]);
                                obj.ReturnQty = Convert.ToInt32(drDeatils["ReturnQty"]);
                                obj.PONumber = Convert.ToString(drDeatils["PONumber"]);
                                obj.ReturnReason = Convert.ToString(drDeatils["ReturnReason"]);
                                obj.Bucket = Convert.ToString(drDeatils["Bucket"]);
                                obj.BucketId = Convert.ToInt32(drDeatils["BucketId"]);
                                obj.AvailableQty = Convert.ToInt32(drDeatils["AvailableQty"]);
                                obj.POAmount = Convert.ToDouble(drDeatils["POAmount"]);

                                obj.GRNInvoiceNumber = drDeatils["GRNInvoiceNo"].ToString();
                                obj.GRNReceivedQty = Convert.ToDouble(drDeatils["GRNReceivedQty"]);
                                obj.GRNInvoiceType = Convert.ToInt32(drDeatils["GRNInvoiceType"]);
                                obj.LineTaxAmount = Convert.ToDecimal(drDeatils["LineTaxAmount"]);
                                obj.ManufactureBatchNo = drDeatils["ManufactureBatchNo"].ToString();

                                listDeatil.Add(obj);
                            }
                        }
                    }
                }
                return listDeatil;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetVendorDetailDataTable(string xmlDoc, string spName, ref string errorMessage)
        {
            
            try
            {
                DataSet ds = new DataSet();
                DataTaskManager dt = new DataTaskManager();

                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam = new DBParameterList();

                    dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                        ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    using (ds = dtManager.ExecuteDataSet(spName, dbParam))
                    {
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                        if (errorMessage.Length != 0) //No dbError
                        {
                            return null;
                        }
                    }
                }

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                throw;
            }

        }


        public DataTable GetPODetails(RetVendorDetails objDetails, string xmlDoc, string spName, ref string errorMessage)
        {
            try
            {
                DataTable dtVendor = new DataTable();
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
                            dtVendor = ds.Tables[0];
                            if (ds.Tables[0].Rows.Count == 1)
                            {
                                
                                objDetails.PODate = Convert.ToDateTime(ds.Tables[0].Rows[0]["PODate"]).ToString(Common.DATE_TIME_FORMAT);
                                //objDetails.DisplayPODate = Convert.ToDateTime(ds.Tables[0].Rows[0]["PODate"]).ToString(Common.DTP_DATE_FORMAT);
                                objDetails.PONumber = Convert.ToString(ds.Tables[0].Rows[0]["PONumber"]);
                                objDetails.POQty = Convert.ToInt32(ds.Tables[0].Rows[0]["POQty"]);
                                objDetails.POAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["MRP"]);
                               
                            }
                            else
                            {
                                
                            }
                        }
                    }

                }

                return dtVendor;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RetVendorHeader> GetHeaderDetails(string xmlDoc, string spName, ref string errorMessage)
        {
            try
            {
                List<RetVendorHeader> listRetVendorHeader = new List<RetVendorHeader>();
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
                            foreach(DataRow drRetVendorHeader in ds.Tables[0].Rows)
                            {
                                RetVendorHeader objRetVendorHeader = new RetVendorHeader();
                                objRetVendorHeader.ReturnNo = Convert.ToString(drRetVendorHeader["ReturnNo"]);
                                objRetVendorHeader.RetVendorDate = Convert.ToDateTime(drRetVendorHeader["RetVendorDate"]).ToString(Common.DATE_TIME_FORMAT);
                                //objRetVendorHeader.DisplayRetVendorDate = Convert.ToDateTime(drRetVendorHeader["RetVendorDate"]).ToString(Common.DTP_DATE_FORMAT);
                                objRetVendorHeader.ModifiedDate = Convert.ToDateTime(drRetVendorHeader["ModifiedDate"]).ToString(Common.DATE_TIME_FORMAT);
                                objRetVendorHeader.LocationId = Convert.ToInt32(drRetVendorHeader["LocationId"]);
                                objRetVendorHeader.VendorId = Convert.ToInt32(drRetVendorHeader["VendorId"]);
                                objRetVendorHeader.Remarks = Convert.ToString(drRetVendorHeader["Remarks"]);
                                objRetVendorHeader.Quantity = Convert.ToInt32(drRetVendorHeader["Quantity"]);
                                objRetVendorHeader.ShippingDetails = Convert.ToString(drRetVendorHeader["ShippingDetails"]);
                                objRetVendorHeader.ShipmentDate = drRetVendorHeader["ShipmentDate"].ToString().Trim() == string.Empty ? string.Empty : Convert.ToDateTime(drRetVendorHeader["ShipmentDate"]).ToString(Common.DATE_TIME_FORMAT);
                                objRetVendorHeader.StatusName = Convert.ToString(drRetVendorHeader["StatusName"]);
                                objRetVendorHeader.StatusId = Convert.ToInt32(drRetVendorHeader["StatusId"]);
                                objRetVendorHeader.DebitNoteNumber = Convert.ToString(drRetVendorHeader["DebitNoteNumber"]);
                                objRetVendorHeader.DebitNoteAmount = Math.Round(Convert.ToDouble(drRetVendorHeader["DebitNoteAmount"]),2);
                                objRetVendorHeader.TotalAmount = Math.Round(Convert.ToDouble(drRetVendorHeader["TotalAmount"]), 2);
                                objRetVendorHeader.VendorName = Convert.ToString(drRetVendorHeader["VendorName"]);
                                listRetVendorHeader.Add(objRetVendorHeader);
                            }
                        }
                    }
                }

                return listRetVendorHeader;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckForValidReturnQty(string xmlDoc, ref string errorMessage)
        {
            bool isValid = true;
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
            nfi.PercentDecimalDigits = Common.DisplayAmountRounding;
            string strRoundingZeroesFormat = Common.GetRoundingZeroes(Common.DisplayQtyRounding); //"0.00";

            DataTaskManager dtDataMgr = new DataTaskManager();
            using (DataTaskManager dtManager = new DataTaskManager())
            {
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataSet ds = dtDataMgr.ExecuteDataSet(SP_CHECK_RET_QTY, dbParam))
                {
                    string tempErrCode = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                    if (!string.IsNullOrEmpty(tempErrCode))
                    {
                        //errorMessage = Common.GetMessage(tempErr);
                        DataTable dtTemp = ds.Tables[0];
                        decimal totGRNRecQty = Convert.ToDecimal(dtTemp.Rows[0]["TotalRemainingQty"].ToString());
                        errorMessage = Common.GetMessage(tempErrCode, totGRNRecQty.ToString(strRoundingZeroesFormat, nfi));
                        isValid = false;
                    }
                }
            }

            return isValid;
        }

        /// <summary>
        /// Save CUSTOMER RETURN
        /// </summary>
        /// <param name="xmlString"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Save(string xmlString, ref string errorMessage)
        {
            bool isSuccess = false;
            // call the save method which returns whether the save was successfull or not
            isSuccess = base.RetVendorSave(xmlString, SP_SAVE_VENDOR_DETAILS, ref errorMessage);
            return isSuccess;
        }

        /// <summary>
        /// save details 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="spName"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        //public bool RTVSave(string xmlDoc, string spName, ref string errorMessage)
        //{
        //    bool isSuccess = false;
        //    try
        //    {
        //        using (DataTaskManager dtManager = new DataTaskManager())
        //        {

        //            //Declare and initialize the parameter list object
        //            DBParameterList dbParam = new DBParameterList();

        //            //Add the relevant 2 parameters
        //            dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
        //            dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
        //                ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

        //            try
        //            {
        //                //Begin the transaction and executing procedure to save the record(s) 
        //                dtManager.BeginTransaction();

        //                // executing procedure to save the record 

        //                DataSet ds;

        //                ds = dtManager.ExecuteDataSet(spName, dbParam);

        //                //Update database message
        //                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

        //                //If an error returned from the database
        //                if (errorMessage.Length > 0)
        //                {
        //                    dtManager.RollbackTransaction();
        //                    isSuccess = false;
        //                }
        //                else
        //                {
        //                    dtManager.CommitTransaction();
        //                    isSuccess = true;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                dtManager.RollbackTransaction();
        //                throw ex;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return isSuccess;
        //}



    }
}
