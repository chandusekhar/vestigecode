using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CoreComponent.Core.BusinessObjects;
using Vinculum.Framework.Cryptography;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;

namespace AuthenticationComponent.BusinessObjects
{
    public class AppUser : Address
    {
        public AppUser()
        {
            m_locationRoles = new List<AppUserLocationRoles>();
        }

        public const string SP_USER_SEARCH = "usp_GetUser";

        #region Properties

        private System.Int32 m_userId;

        public System.Int32 UserId
        {
            get { return m_userId; }
            set { m_userId = value; }
        }

        private System.Int32 m_title;

        public System.Int32 Title
        {
            get { return m_title; }
            set { m_title = value; }
        }
        private string m_titleName;

        public string TitleName
        {
            get { return m_titleName; }
            set { m_titleName = value; }
        }

        private string m_userName;

        public string UserName
        {
            get { return m_userName; }
            set { m_userName = value; }
        }
        private string m_password;

        public string Password
        {
            get { return m_password; }
            set { m_password = value; }
        }

        private System.String m_firstName;

        public System.String FirstName
        {
            get { return m_firstName; }
            set { m_firstName = value; }
        }

        private System.String m_middleName;

        public System.String MiddleName
        {
            get { return m_middleName; }
            set { m_middleName = value; }
        }

        private System.String m_lastName;

        public System.String LastName
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }

        private string m_dob;

        public string Dob
        {
            get { return m_dob; }
            set { m_dob = value; }
        }

        private System.String m_designation;

        public System.String Designation
        {
            get { return m_designation; }
            set { m_designation = value; }
        }

        private System.Int32 m_status;

        public System.Int32 Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        private string m_statusValue;

        public string StatusValue
        {
            get { return m_statusValue; }
            set { m_statusValue = value; }
        }

        private System.Int32 m_createdBy;

        public System.Int32 CreatedBy
        {
            get { return m_createdBy; }
            set { m_createdBy = value; }
        }

        private string m_createdDate;

        public string CreatedDate
        {
            get { return m_createdDate; }
            set { m_createdDate = value; }
        }

        private System.Int32 m_modifiedBy;

        public System.Int32 ModifiedBy
        {
            get { return m_modifiedBy; }
            set { m_modifiedBy = value; }
        }

        private string m_modifiedDate;

        public string ModifiedDate
        {
            get { return m_modifiedDate; }
            set { m_modifiedDate = value; }
        }

        //private List<Role> m_assignedRoles;
        //public List<Role> AssignedRoles
        //{
        //    get { return m_assignedRoles; }
        //    set { m_assignedRoles = value; }
        //}

        private List<AppUserLocationRoles> m_locationRoles;

        public List<AppUserLocationRoles> LocationRoles
        {
            get { return m_locationRoles; }
            set { m_locationRoles = value; }
        }

        #endregion Properties

        public static AppUser LogInUser(string userName, string password, string locationCode)
        {
            try
            {
                AppUser loggedInUser = null;
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_DATA, userName, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_DATA2, string.Empty, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_DATA3, string.Empty, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    object returnObject = dt.ExecuteScalar(SP_USER_SEARCH, dbParam);

                    dbMessage = (returnObject == null)? string.Empty:returnObject.ToString();
                    if (dbMessage != string.Empty)
                    {
                        if ((new CryptographyManager(CryptographyProviderType.SymmetricCryptoProvider)).Decrypt(dbMessage).CompareTo(password) == 0)
                        {
                            loggedInUser = AppUser.Search(userName, locationCode);
                        }
                    }
                }
                return loggedInUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static AppUser Search(string userName, string locationCode)
        {
            try
            {
                AppUser loggedInUser = null;
                string dbMessage = string.Empty;
                DBParameterList dbParam = new DBParameterList();
                dbParam.Add(new DBParameter(Common.PARAM_DATA, userName, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_DATA2, locationCode, DbType.String));
                dbParam.Add(new DBParameter(Common.PARAM_DATA3, Common.AppType, DbType.Int32));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));

                using (DataTaskManager dt = new DataTaskManager())
                {
                    DataSet ds = dt.ExecuteDataSet(SP_USER_SEARCH, dbParam);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        loggedInUser = new AppUser();
                        loggedInUser.Address1 = ds.Tables[0].Rows[0]["Address1"].ToString();
                        loggedInUser.Address2 = ds.Tables[0].Rows[0]["Address2"].ToString();
                        loggedInUser.Address3 = ds.Tables[0].Rows[0]["Address3"].ToString();
                        loggedInUser.Address4 = ds.Tables[0].Rows[0]["Address4"].ToString();
                        loggedInUser.City = ds.Tables[0].Rows[0]["CityName"].ToString();
                        loggedInUser.CityId = Convert.ToInt32(ds.Tables[0].Rows[0]["CityId"]);
                        loggedInUser.Country = ds.Tables[0].Rows[0]["CountryName"].ToString();
                        loggedInUser.CountryId = Convert.ToInt32(ds.Tables[0].Rows[0]["CountryId"]);
                        loggedInUser.CreatedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedBy"]);
                        loggedInUser.CreatedDate = ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                        loggedInUser.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();
                        loggedInUser.Dob = ds.Tables[0].Rows[0]["DOB"].ToString();
                        loggedInUser.Email1 = ds.Tables[0].Rows[0]["EmailId1"].ToString();
                        loggedInUser.Email2 = ds.Tables[0].Rows[0]["EmailId2"].ToString();
                        loggedInUser.Fax1 = ds.Tables[0].Rows[0]["Fax1"].ToString();
                        loggedInUser.Fax2 = ds.Tables[0].Rows[0]["Fax2"].ToString();
                        loggedInUser.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                        loggedInUser.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                        loggedInUser.MiddleName = ds.Tables[0].Rows[0]["MiddleName"].ToString();
                        loggedInUser.Mobile1 = ds.Tables[0].Rows[0]["Mobile1"].ToString();
                        loggedInUser.Mobile2 = ds.Tables[0].Rows[0]["Mobile2"].ToString();
                        loggedInUser.ModifiedBy = Convert.ToInt32(ds.Tables[0].Rows[0]["ModifiedBy"]);
                        loggedInUser.ModifiedDate = ds.Tables[0].Rows[0]["ModifiedDate"].ToString();
                        //loggedInUser.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                        loggedInUser.PhoneNumber1 = ds.Tables[0].Rows[0]["Phone1"].ToString();
                        loggedInUser.PhoneNumber2 = ds.Tables[0].Rows[0]["Phone2"].ToString();
                        loggedInUser.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                        loggedInUser.State = ds.Tables[0].Rows[0]["StateName"].ToString();
                        loggedInUser.StateId = Convert.ToInt32(ds.Tables[0].Rows[0]["StateId"]);
                        loggedInUser.Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"]);
                        loggedInUser.StatusValue = ds.Tables[0].Rows[0]["StatusName"].ToString();
                        loggedInUser.Title = Convert.ToInt32(ds.Tables[0].Rows[0]["Title"]);
                        loggedInUser.TitleName = ds.Tables[0].Rows[0]["TitleName"].ToString();
                        loggedInUser.UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"]);
                        loggedInUser.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                        loggedInUser.Website = ds.Tables[0].Rows[0]["Website"].ToString();

                        //Create AppUserLocationRole object and assign location details and role details

                        if (ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0)
                        {
                            AppUserLocationRoles locationRoles = new AppUserLocationRoles();
                            locationRoles.LocationId = Convert.ToInt32(ds.Tables[1].Rows[0]["LocationId"]);
                            locationRoles.LocationCode = ds.Tables[1].Rows[0]["LocationCode"].ToString();
                            locationRoles.LocationName = ds.Tables[1].Rows[0]["LocationName"].ToString();
                            foreach (DataRow dr in ds.Tables[2].Rows)
                            {
                                //If the role has not been already added then add role
                                if (locationRoles.Roles.Find(delegate(Role r) { return r.RoleCode == dr["RoleCode"].ToString(); }) == null)
                                {
                                    //Create and Add Role Object
                                    Role r = new Role();
                                    r.RoleCode = dr["RoleCode"].ToString();
                                    r.RoleId = Convert.ToInt32(dr["RoleId"]);
                                    r.RoleName = dr["RoleName"].ToString();

                                    DataRow[] moduleList = ds.Tables[2].Select("RoleId = " + r.RoleId + " AND ModuleCode <> '" + string.Empty + "'");
                                    foreach (DataRow drM in moduleList)
                                    {
                                        if (r.AssignedModules.Find(delegate(Module m) { return m.Code == drM["ModuleCode"].ToString(); }) == null)
                                        {
                                            //Create and add assigned module
                                            Module mod = new Module();
                                            mod.Code = drM["ModuleCode"].ToString();
                                            mod.ModuleId = Convert.ToInt32(drM["ModuleId"]);
                                            mod.Name = drM["ModuleName"].ToString();

                                            DataRow[] functionList = ds.Tables[2].Select("RoleId = " + r.RoleId + " AND ModuleId = " + mod.ModuleId + " AND FunctionCode <> '" + string.Empty + "'");
                                            foreach (DataRow drF in functionList)
                                            {
                                                if (mod.Functions.Find(delegate(Function f) { return f.Code == drF["FunctionCode"].ToString(); }) == null)
                                                {
                                                    //Create and add functions
                                                    Function newFunc = new Function();
                                                    newFunc.Code = drF["FunctionCode"].ToString();
                                                    newFunc.FunctionId = Convert.ToInt32(drF["FunctionId"]);
                                                    newFunc.Name = drF["FunctionName"].ToString();

                                                    DataRow[] conditionList = ds.Tables[2].Select("RoleId = " + r.RoleId + " AND ModuleId = " + mod.ModuleId + " AND FunctionId = " + newFunc.FunctionId + " AND ConditionCode <> '" + string.Empty + "'");

                                                    foreach (DataRow drC in conditionList)
                                                    {
                                                        if (newFunc.AssignedConditions.Find(delegate(Condition c) { return c.Code == drC["ConditionCode"].ToString(); }) == null)
                                                        {
                                                            Condition newCon = new Condition();
                                                            newCon.Code = drC["ConditionCode"].ToString();
                                                            newCon.ConditionId = Convert.ToInt32(drC["ConditionId"]);
                                                            newCon.Name = drC["ConditionName"].ToString();
                                                            newFunc.AssignedConditions.Add(newCon);
                                                        }
                                                    }
                                                    mod.Functions.Add(newFunc);
                                                }
                                            }
                                            r.AssignedModules.Add(mod);
                                        }
                                    }
                                    locationRoles.Roles.Add(r);
                                }
                            }
                            loggedInUser.LocationRoles.Add(locationRoles);
                        }
                    }
                }
                return loggedInUser;
            }
            catch
            {
                throw;
            }
        }
    }
}
