using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;


namespace CoreComponent.BusinessObjects
{
    [Serializable]
    public class ItemBarCode
    {
        #region Variable & Property Declaration

        private int m_itemId;
        private string m_itemBarCodeVal;
        private string m_startsOn;
        private int m_status;
        private string m_statusText;

        public int ItemId
        {
            get { return m_itemId; }
            set { m_itemId = value; }
        }
        public string ItemBarCodeVal
        {
            get { return m_itemBarCodeVal; }
            set { m_itemBarCodeVal = value; }
        }
        public string StartsOn
        {
            get { return m_startsOn; }
            set { m_startsOn = value; }
        }
        public string StartsOnText
        {
            get { return (Convert.ToDateTime(StartsOn)).ToString(Common.DTP_DATE_FORMAT); }
            set { m_startsOn = value; }
        }
        public int Status
        {
            get { return m_status; }
            set { m_status = value; }
        }
        public string StatusText
        {
          get { return m_statusText; }
          set { m_statusText = value; }
        }

        private Boolean barcodeState;
        public Boolean BarcodeState
        {
            get { return barcodeState; }
            set { barcodeState = value; }
        }

        #endregion

        #region SP Declaration

        private const string SP_ITEM_BARCODE_SEARCH = "usp_ItemBarCodeSearch";

        #endregion

        #region  Methods

        /// <summary>
        /// Search method, Fetches records from DB using usp_ItemBarCodeSearch SP
        /// </summary>
        /// <param name="itemIdParam"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<ItemBarCode> SearchItemBarCode(int itemIdParam, ref string errorMsg)
        {
            List<ItemBarCode> itemBarCodeList = new List<ItemBarCode>();
            try
            { 
                DataTaskManager dtManager = new DataTaskManager();
                DBParameterList dbParam = new DBParameterList();                
                dbParam.Add(new DBParameter("ItemId", itemIdParam, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                DataTable dt = dtManager.ExecuteDataTable(SP_ITEM_BARCODE_SEARCH, dbParam);
                errorMsg = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                if (errorMsg == string.Empty)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ItemBarCode itemBarCodeObj = new ItemBarCode();
                            itemBarCodeObj.ItemId = Convert.ToInt32(dt.Rows[i]["ItemId"]);
                            itemBarCodeObj.ItemBarCodeVal = dt.Rows[i]["BarCode"].ToString();
                            itemBarCodeObj.StartsOn = Convert.ToDateTime(dt.Rows[i]["StartsOn"]).ToShortDateString();
                            itemBarCodeObj.Status = Convert.ToInt32(dt.Rows[i]["Status"]);
                            itemBarCodeObj.StatusText = dt.Rows[i]["StatusText"].ToString();
                            itemBarCodeObj.BarcodeState = true;
                            itemBarCodeList.Add(itemBarCodeObj);
                        }
                    }
                }
                return itemBarCodeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
