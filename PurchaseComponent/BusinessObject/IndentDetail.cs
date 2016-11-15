using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.BusinessObjects;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using System.Data;

namespace PurchaseComponent.BusinessObjects
{
    public class IndentDetail
    { 
        #region SP Declaration
        private const string SP_INDENT_DETAIL_SEARCH = "usp_IndentDetailSearch";
        private const string SP_INDENTDETAIL_DELETE = "usp_IndentDetailDelete";
        private const string SP_ITEMLOCATION_STOCKDETAIL = "usp_GetItemDetail";
        private const string SP_ITEMWHLOCATION_STOCKDETAIL = "USP_LOCATION_INVENTORY";
        #endregion

        public IndentDetail()
        {
            
        }
       
        #region Properties
               
        public String IndentNo{get;set;}

        public Int32 ItemID{get;set;}

        public String ItemCode{get;set;}

        public String ItemName{get;set;}

        public double SuggestedQty { get; set; }

        public double DisplaySuggestedQty
        {
            get { return Math.Round(DBSuggestedQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public double DBSuggestedQty
        {
            get { return Math.Round(SuggestedQty ,Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public double RequestedQty { get; set; }
        private double whLocatinStock;
        public double WhLocationStock { 
            get {
                    return whLocatinStock;
                }
            set {
                    whLocatinStock = value;
                }
        }
        public double DisplayRequestedQty
        {
            get { return Math.Round(DBRequestedQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { RequestedQty = value; }
        }

        public double DBRequestedQty
        {
            get { return Math.Round(RequestedQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public double ApprovedHOQty { get; set; }

        public double DisplayApprovedHOQty
        {
            get { return Math.Round(DBApprovedHOQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { ApprovedHOQty = value; }
        }

        public double DBApprovedHOQty
        {
            get { return Math.Round(ApprovedHOQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public double ApprovedTOQty { get; set; }

        public double DisplayApprovedTOQty
        {
            get { return Math.Round(DBApprovedTOQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { ApprovedTOQty = value; }
        }

        public double DBApprovedTOQty
        {
            get { return Math.Round(ApprovedTOQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public double ApprovedPOQty { get; set; }

        public double DisplayApprovedPOQty
        {
            get { return Math.Round(DBApprovedPOQty, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { ApprovedPOQty = value; }
        }

        public double DBApprovedPOQty
        {
            get { return Math.Round(ApprovedPOQty, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }
     
        public List<string> PONumber{get;set;}
       
        public List<string> TONumber{get;set;}

        public double StockInHand { get; set; }

        public double DisplayStockInHand
        {
            get { return Math.Round(DBStockInHand, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public double DBStockInHand
        {
            get { return Math.Round(StockInHand, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public double StockInTransit { get; set; }

        public double DisplayStockInTransit
        {
            get { return Math.Round(DBStockInTransit, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public double DBStockInTransit
        {
            get { return Math.Round(StockInTransit, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public double AvgSale { get; set; }

        public double DisplayAvgSale
        {
            get { return Math.Round(DBAvgSale, Common.DisplayQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public double DBAvgSale
        {
            get { return Math.Round(AvgSale, Common.DBQtyRounding, MidpointRounding.AwayFromZero); }
            set { throw new NotImplementedException("This Property cannot be explicitly set"); }
        }

        public int TOFromLocationID{get;set;}
        public int ToFromLocationStock { get; set; }
        public int Vendor { get; set; }
        public int DelLocation { get; set; }  
        public int Status { get; set; }
        public Boolean IsFormC { get; set; }
        public Boolean IsConsolidate
        {
            get;
            set;
        }
        public double AvgStockTransfer { get; set; }
        public double TotalStock { get; set; }
        public double TotalSaleStockTransfer { get; set; }
        
        #endregion

        #region Methods

        /// <summary>
        ///  GET Stock Details of Item and Assigns to Object of Indent Detail
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <param name="ItemID"></param>
        /// <param name="LocationID"></param>
        public void GetItemStockDetail(string ItemCode, int ItemID, int LocationID)
        {
            try
            {
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@ItemCode", ItemCode, DbType.String));
                dbParam.Add(new DBParameter("@ItemId", ItemID, DbType.Int32));
                dbParam.Add(new DBParameter("@LocationId", LocationID, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    System.Data.DataTable dTable = new DataTable();
                    dTable = dt.ExecuteDataTable(SP_ITEMLOCATION_STOCKDETAIL, dbParam);
                    if (dbMessage == string.Empty)
                    {
                        if (dTable != null && dTable.Rows.Count > 0)
                        {
                            this.ItemCode = Convert.ToString(dTable.Rows[0]["ItemCode"]);
                            this.ItemID = Convert.ToInt32(dTable.Rows[0]["ItemId"]);
                            this.ItemName = Convert.ToString(dTable.Rows[0]["ItemName"]);
                            this.StockInHand = Convert.ToDouble(dTable.Rows[0]["StockInHand"]);
                            this.StockInTransit = Convert.ToDouble(dTable.Rows[0]["StockInTransit"]);
                            this.AvgSale = Convert.ToDouble(dTable.Rows[0]["AvgSale"]);
                            this.AvgStockTransfer = Convert.ToDouble(dTable.Rows[0]["AvgStockTransfer"]);
                            this.TotalStock = this.StockInHand + this.StockInTransit;
                            this.TotalSaleStockTransfer = this.AvgSale + this.AvgStockTransfer;
                           // this.TOFromLocationID = Convert.ToDouble(dTable.Rows[0][""]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IndentDetail> Search()
        {
            List<IndentDetail> indentDetailList = new List<IndentDetail>();
            try
            {
                string errorMessage = string.Empty;
                DataSet dSet = GetSelectedRecords(Common.ToXml(this), SP_INDENT_DETAIL_SEARCH, ref errorMessage);
                //System.Data.DataTable dTable = GetSelectedRecords(Common.ToXml(this), SP_INDENT_DETAIL_SEARCH, ref errorMessage);

                if (dSet == null | dSet.Tables[0].Rows.Count <= 0)
                    return null;

                foreach (System.Data.DataRow drow in dSet.Tables[0].Rows)
                {
                    IndentDetail indentDetail = new IndentDetail();
                    indentDetail.CreateIndentDetailObject(drow);                    
                    indentDetail.GetPONO(dSet.Tables[1]);
                    indentDetail.GetTONO(dSet.Tables[2]);                   
                    indentDetailList.Add(indentDetail);
                }
                return indentDetailList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void CreateIndentDetailObject(System.Data.DataRow drow)
        {
            try
            {
                this.ApprovedHOQty = Convert.ToDouble(drow["ApprovedHOQty"]);
                this.ApprovedPOQty = Convert.ToDouble(drow["ApprovedPOQty"]);
                //this.ApprovedTOQty = Convert.ToDouble(drow["ApprovedTOQty"]);
                this.ApprovedTOQty = Convert.ToDouble(drow["ApprovedHOQty"]);
                this.AvgSale = Convert.ToDouble(drow["AvgSaleFactor"]);
                this.IndentNo = Convert.ToString(drow["IndentNo"]);
                this.ItemCode = Convert.ToString(drow["ItemCode"]);
                this.ItemID = Convert.ToInt32(drow["ItemId"]);
                this.ItemName = Convert.ToString(drow["ItemName"]);
                this.RequestedQty = Convert.ToDouble(drow["RequestedQty"]);
                this.StockInHand = Convert.ToDouble(drow["StockInHand"]);
                this.StockInTransit = Convert.ToDouble(drow["StockInTransit"]);
                this.SuggestedQty = Convert.ToDouble(drow["SuggestedQty"]);
                this.Status = Convert.ToInt32(drow["Status"]);
                this.IsFormC = Convert.ToBoolean(drow["IsFormC"]);
                this.TOFromLocationID = Convert.ToInt32(drow["ToFromLocationID"]);
                this.ToFromLocationStock = Convert.ToInt32(drow["TOFromLocationStock"]);
                this.TotalStock = Convert.ToInt32(drow["TotalStock"]);
                this.AvgStockTransfer = Convert.ToInt32(drow["AvgStockTransfer"]);
                this.TotalSaleStockTransfer = Convert.ToInt32(drow["TotalSaleTransfer"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable IndentDetailSearchDataTable()
        {
            System.Data.DataSet dSet = new DataSet();
            try
            {
                string errorMessage = string.Empty;
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@IndentNo",this.IndentNo, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                using (DataTaskManager dt = new DataTaskManager())
                {                   
                    dSet = dt.ExecuteDataSet(SP_INDENT_DETAIL_SEARCH, dbParam);
                    if (dSet == null | dSet.Tables[0].Rows.Count <= 0)
                        return null;                   
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
            return dSet.Tables[0];
        }

        public void getFromLocationStock(int Locationid, int ItemId)
        {
            System.Data.DataSet dSet = new DataSet();
            try
            {
                string errorMessage = string.Empty;
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@Locationid", Locationid, DbType.Int16));
                dbParam.Add(new DBParameter("@Itemid", ItemId, DbType.Int16));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, dbMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                using (DataTaskManager dt = new DataTaskManager())
                {
                    dSet = dt.ExecuteDataSet(SP_ITEMWHLOCATION_STOCKDETAIL, dbParam);
                    if (dSet == null | dSet.Tables[0].Rows.Count <= 0)
                        return;

                    this.whLocatinStock = Convert.ToDouble(dSet.Tables[0].Rows[0]["AvailableQuantity"]);
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
            }
        }

        /// <summary>
        /// Get Po Numbers of Particular Item in Indent
        /// </summary>
        /// <param name="dt"></param>
        public void GetPONO(DataTable dt)
        {
            DataRow[] dataRow=dt.Select(" IndentNo='" + this.IndentNo + "' AND ItemId=" + this.ItemID);
            foreach (DataRow dr in dataRow)
            {
                if (this.PONumber == null)
                    this.PONumber = new List<string>();
                this.PONumber.Add(Convert.ToString(dr["PONumber"]));
                this.Vendor =Convert.ToInt16(dr["Vendorid"]);
                this.DelLocation = Convert.ToInt16(dr["DeliveryLocationId"]);
            }
        }
        
        /// <summary>
        /// Get To No of Particular Item in Indent
        /// </summary>
        /// <param name="dt"></param>
        public void GetTONO(DataTable dt)
        {
            DataRow[] dataRow = dt.Select(" IndentNo='" + this.IndentNo + "' AND ItemId=" + this.ItemID);
            //DataView dv = new DataView();            
            foreach (DataRow dr in dataRow)
            {
                if (this.TONumber == null)
                    this.TONumber = new List<string>();
                this.TONumber.Add(Convert.ToString(dr["TOINumber"]));
                this.TOFromLocationID = Convert.ToInt32(dr["TransferFromLocationId"]);
            }
        }
        
        public virtual DataSet GetSelectedRecords(string xmlDoc, string spName, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters
                dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataSet dt = dtManager.ExecuteDataSet(spName, dbParam);

                // update database message
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();

                // if an error returned from the database
                if (errorMessage != string.Empty)
                    return null;
                else
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
