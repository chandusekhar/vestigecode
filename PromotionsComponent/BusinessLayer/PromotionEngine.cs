using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace PromotionsComponent.BusinessLayer
{
    public class PromotionEngine
    {
        private const string SP_PROMO_ENGINE= "usp_PromotionEngine";

        /// <summary>
        /// Returns the Price Object after applying Line Promotion
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemId"></param>
        /// <param name="purchaseQty"></param>
        /// <param name="locationCode"></param>
        /// <returns></returns>
        public static Price GetPrice(string itemCode, int itemId, decimal purchaseQty, string locationCode, bool isFirstOrder)
        {
            try
            {
                int paramFirstOrder = isFirstOrder ? 2 : 1;
                Price newPrice = null;
                DBParameterList dbParamList = new DBParameterList();
                dbParamList.Add(new DBParameter("@iType", 1, DbType.Int32));
                dbParamList.Add(new DBParameter("@vItemCode", itemCode, DbType.String));
                dbParamList.Add(new DBParameter("@iItemId", itemId, DbType.Int32));
                dbParamList.Add(new DBParameter("@vLocationcode", locationCode, DbType.String));
                dbParamList.Add(new DBParameter("@iPurchaseQty", purchaseQty, DbType.Decimal));
                dbParamList.Add(new DBParameter("@vCustomerOrder", string.Empty, DbType.String));
                dbParamList.Add(new DBParameter("@fromStateID", -1, DbType.Int32));
                dbParamList.Add(new DBParameter("@toStateID", -1, DbType.Int32));
                dbParamList.Add(new DBParameter("@TaxTypeCode", Common.TaxType.SOTAX.ToString(), DbType.String));
                dbParamList.Add(new DBParameter("@Date", null, DbType.DateTime));
                dbParamList.Add(new DBParameter("@IsFormC", 0, DbType.Boolean));
                dbParamList.Add(new DBParameter("@IsFirstOrder", paramFirstOrder, DbType.Int32));
                dbParamList.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                dbParamList.Add(new DBParameter("@validationMessage", string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DataTable dt = dtManager.ExecuteDataTable(SP_PROMO_ENGINE, dbParamList);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        newPrice = new Price(Convert.ToInt32(dt.Rows[0]["PromotionId"])
                                            , Convert.ToInt32(dt.Rows[0]["ItemId"])
                                            , dt.Rows[0]["ItemCode"].ToString()
                                            , dt.Rows[0]["ItemName"].ToString()
                                            , dt.Rows[0]["ShortName"].ToString()
                                            , dt.Rows[0]["PrintName"].ToString()
                                            , dt.Rows[0]["ReceiptName"].ToString()
                                            , dt.Rows[0]["DisplayName"].ToString()
                                            , Convert.ToDecimal(dt.Rows[0]["MRP"])
                                            , Convert.ToDecimal(dt.Rows[0]["DistributorPrice"])
                                            , Convert.ToDecimal(dt.Rows[0]["DiscountP"])
                                            , Convert.ToDecimal(dt.Rows[0]["DiscountValue"])
                                            , Convert.ToDecimal(dt.Rows[0]["DiscountedPrice"])
                                            , Convert.ToDecimal(dt.Rows[0]["BusinessVolume"])
                                            , Convert.ToDecimal(dt.Rows[0]["PointValue"])
                                            , purchaseQty
                                            );
                    }
                }
                return newPrice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
