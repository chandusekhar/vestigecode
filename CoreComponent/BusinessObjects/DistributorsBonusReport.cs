using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vinculum.Framework.DataTypes;
using System.Data;
using Vinculum.Framework.Data;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.BusinessObjects
{
    public class DistributorsBonusReport
    {

        #region Constants

       // public const string SP_DIST_BONUS_PRINT = "usp_DistributorBonusPrint";
        public const string SP_DIST_BONUS_PRINT = "usp_BonusStatementDirectors";
        public const string DIST_BONUS_SEARCH = "usp_DistributorBonusSearch"; // To create sp later
        #endregion

        #region Properties
        private int? m_distributorId;
        private int? m_CityId;
        private int? m_StateId;
        private int? m_CountryId;
        private int m_Level;
        private decimal m_BonusPercent;
        private string m_BusinessMonth;
        private string m_BonusTypes;

        public int? DistributorId
        {
            get { return m_distributorId; }
            set { m_distributorId = value; }
        }
        public int? CityId
        {
            get { return m_CityId; }
            set { m_CityId = value; }
        }
        public int? StateId
        {
            get { return m_StateId; }
            set { m_StateId = value; }
        }
        public int? CountryId
        {
            get { return m_CountryId; }
            set { m_CountryId = value; }
        }
        public int? LevelId
        {
            get { return m_Level; }
            set { m_Level = value.Value; }
        }
        public decimal? BonusPercent
        {
            get { return m_BonusPercent; }
            set { m_BonusPercent = value.Value; }
        }
        public string BusinessMonth
        {
            get { return m_BusinessMonth; }
            set { m_BusinessMonth = value; }
        }
        public string BonusTypes
        {
            get { return m_BonusTypes; }
            set { m_BonusTypes = value; }
        }





        #endregion

        public List<DistributorsBonusInfo> SearchDistributorBonus(string xmlDoc, string spName, ref string errorMessage)
        {
            List<DistributorsBonusInfo> distributorsBonusInfoCollection = new List<DistributorsBonusInfo>();
            try
            {
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    //Declare and initialize the parameter list object.
                    DBParameterList dbParam = new DBParameterList();

                    //Add the relevant 2 parameters
                    dbParam.Add(new DBParameter(Common.PARAM_DATA, xmlDoc, DbType.String));
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String,
                        ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    using (DataTable dtDistributorsBonusData = dtManager.ExecuteDataTable(spName, dbParam))
                    {
                        // update database message
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                        if (errorMessage.Length == 0 && dtDistributorsBonusData.Rows.Count > 0) //No dbError
                        {
                            for (int i = 0; i < dtDistributorsBonusData.Rows.Count; i++)
                            {
                                DistributorsBonusInfo distributorsBonusInfo = new DistributorsBonusInfo();
                                distributorsBonusInfo.DistributorId = Convert.ToInt32(dtDistributorsBonusData.Rows[i]["DistributorId"]);
                                distributorsBonusInfo.DistributorName = Convert.ToString(dtDistributorsBonusData.Rows[i]["DistributorName"]);
                                distributorsBonusInfo.Amount = Convert.ToDecimal(dtDistributorsBonusData.Rows[i]["Amount"]);
                                distributorsBonusInfo.LevelName = Convert.ToString(dtDistributorsBonusData.Rows[i]["LevelName"]);
                                distributorsBonusInfo.BonusPercent = Convert.ToString(dtDistributorsBonusData.Rows[i]["BonusPercent"]);
                                distributorsBonusInfo.CountryName = Convert.ToString(dtDistributorsBonusData.Rows[i]["CountryName"]);
                                distributorsBonusInfo.StateName = Convert.ToString(dtDistributorsBonusData.Rows[i]["StateName"]);
                                distributorsBonusInfo.CityName = Convert.ToString(dtDistributorsBonusData.Rows[i]["CityName"]);
                                distributorsBonusInfoCollection.Add(distributorsBonusInfo);
                            } //Loop Roles
                        }
                    }
                }
            }
            catch { throw; }
            return distributorsBonusInfoCollection;
        }

        public static DataSet GetDistributorBonusReportForPrint(int type, int DistributorId, DateTime BusinessMonth, ref string errorMessage)
        {
            try
            {
                string DistId = DistributorId.ToString();
                string Month = BusinessMonth.Month.ToString();
                string Year = BusinessMonth.Year.ToString();

                DataSet ds = new DataSet();
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DBParameterList dbParam = new DBParameterList();
                    dbParam.Add(new DBParameter("@Month", Month, DbType.String));
                    dbParam.Add(new DBParameter("@Year", Year, DbType.String));
                    dbParam.Add(new DBParameter("@DistributorId", DistId, DbType.String));
                   
                    dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                    ds = dtManager.ExecuteDataSet(SP_DIST_BONUS_PRINT, dbParam);
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
    public enum BonusType : int
    {
        PB = 1,
        DB = 2,
        CF = 3,
        HF = 4,
        TF = 5,
        LOB = 6
    }
    public class DistributorsBonusInfo
    {
        private int m_distributorId;
        private string m_distributorName;
        private string m_BonusPercent;
        private string m_LevelName;
        private decimal m_Amount;
        private string m_Country;
        private string m_State;
        private string m_City;
        private bool m_Print;

        public int DistributorId
        {
            get { return m_distributorId; }
            set { m_distributorId = value; }
        }

        public string DistributorName
        {
            get { return m_distributorName; }
            set { m_distributorName = value; }
        }

        public string BonusPercent
        {
            get { return m_BonusPercent; }
            set { m_BonusPercent = value; }
        }

        public string LevelName
        {
            get { return m_LevelName; }
            set { m_LevelName = value; }
        }

        public decimal Amount
        {
            get { return m_Amount; }
            set { m_Amount = value; }
        }

        public string CountryName
        {
            get { return m_Country; }
            set { m_Country = value; }
        }
        public string StateName
        {
            get { return m_State; }
            set { m_State = value; }
        }
        public string CityName
        {
            get { return m_City; }
            set { m_City = value; }
        }
        public bool Print
        {
            get { return m_Print; }
            set { m_Print = value; }
        }


    }
}
