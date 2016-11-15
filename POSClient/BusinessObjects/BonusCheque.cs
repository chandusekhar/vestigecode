using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
namespace POSClient.BusinessObjects
{
    [Serializable]
    public class BonusCheque
    {
        #region SP_Declaration
        private const string SP_DISTRIBUTOR_BONUS_CHEQUE_SEARCH = "usp_DistributorBonusChequeSearch";
        #endregion

        #region Property
        public int DistributorId { get; set; }
        public string ChequeNo { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string ExpiryDate { get; set; }
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public decimal UsedAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal UseAmount { get; set; }
        public int CanBeUsedAgain { get; set; }
        public string OrderNo { get; set; }
        public string TeamOrderNo { get; set; }
        
        #endregion
        public List<BonusCheque> Search(ref string errorMessage)
        {
            List<BonusCheque> BonusList = new List<BonusCheque>();
            System.Data.DataTable dTable = new DataTable();
            try
            {
                dTable = GetSelectedRecords(Common.ToXml(this), SP_DISTRIBUTOR_BONUS_CHEQUE_SEARCH, ref errorMessage);

                if (dTable != null)
                {
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        //CO order = new CO();
                        //order.CreateCOObject(drow);
                        //OrderList.Add(order);
                    }
                }
                return BonusList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GetChequeDetail()
        {            
            System.Data.DataTable dTable = new DataTable();
            try
            {
                string errorMessage = string.Empty;               
                dTable = GetSelectedRecords(Common.ToXml(this), SP_DISTRIBUTOR_BONUS_CHEQUE_SEARCH, ref errorMessage);

                if (dTable != null)
                {
                    foreach (System.Data.DataRow drow in dTable.Rows)
                    {
                        CreateBonusChequeObject(drow);
                    }
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable GetSelectedRecords(string xmlDoc, string spName, ref string errorMessage)
        {
            DBParameterList dbParam;
            try
            {
                Vinculum.Framework.Data.DataTaskManager dtManager = new DataTaskManager();

                // initialize the parameter list object
                dbParam = new DBParameterList();

                // add the relevant 2 parameters                
                dbParam.Add(new DBParameter("@ChequeNo",this.ChequeNo, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                // executing procedure to save the record 
                DataTable dt = dtManager.ExecuteDataTable(spName, dbParam);

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

        private void CreateBonusChequeObject(DataRow dr)
        {
            try
            {
                this.Amount = Convert.ToDecimal(dr["Amount"]);
                this.BalanceAmount = Convert.ToDecimal(dr["BalanceAmount"]);
                this.BankID = Convert.ToInt32(dr["BankID"]);
                this.BankName = Convert.ToString(dr["BankName"]);
                this.CanBeUsedAgain = Convert.ToInt32(dr["CanBeUsedAgain"]);
                this.ChequeNo = Convert.ToString(dr["ChequeNo"]);
                this.Description = Convert.ToString(dr["Description"]);
                this.DistributorId = Convert.ToInt32(dr["DistributorId"]);
                this.ExpiryDate = Convert.ToString(dr["ExpiryDate"]);
                this.Name = Convert.ToString(dr["Name"]);
                this.OrderNo = Convert.ToString(dr["OrderNo"]);
                this.Status = Convert.ToInt32(dr["Status"]);
                this.StatusName = Convert.ToString(dr["StatusName"]);
                this.TeamOrderNo = Convert.ToString(dr["TeamOrderNo"]);
                this.UsedAmount = Convert.ToDecimal(dr["UsedAmount"]);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
