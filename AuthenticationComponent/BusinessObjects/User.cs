using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using Vinculum.Framework.Cryptography;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using Vinculum.Framework.Logging;
using CoreComponent.Core.BusinessObjects;

namespace AuthenticationComponent.BusinessObjects
{
    /*
    ------------------------------------------------------------------------
    Created by			    :	Amit Bansal
    Created Date		    :	19/June/2009
    Purpose				    :	Class to manage User.
     *                          
    Modified by			    :	Ajay Kumar Singh
    Date of Modification    :	22/June/2009
    Purpose of Modification	:	Write code for applicable methods and members in this class.
    ------------------------------------------------------------------------    
    */

    [Serializable]
    public class User : Address, IUser
    {
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
        
        private System.String m_dob;

        public System.String Dob
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

        private System.String m_createdDate;

        public System.String CreatedDate
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

        private System.String m_modifiedDate;

        public System.String ModifiedDate
        {
            get { return m_modifiedDate; }
            set { m_modifiedDate = value; }
        }

        private int m_PasswordModifiedTrueFalse;

        //1 For Modified password and 0 for as it is
        public int PasswordModifiedTrueFalse
        {
            get { return m_PasswordModifiedTrueFalse; }
            set { m_PasswordModifiedTrueFalse = value; }
        }

        //private List<Role> m_assignedRoles;
        //public List<Role> AssignedRoles
        //{
        //    get { return m_assignedRoles; }
        //    set { m_assignedRoles = value; }
        //}

        private List<LocationRole> m_locationRoles;

        public List<LocationRole> LocationRoles
        {
            get { return m_locationRoles; }
            set { m_locationRoles = value; }
        }

        #endregion Properties

        #region Constants

        public const string USER_SAVE = "usp_UserSave";
        public const string USER_SEARCH = "usp_UserSearch";

        //GridView definition XML path
        public const string GRIDVIEW_DEFINITION_XML_PATH = "\\App_Data\\GridViewDefinition_Auth.xml";

        #endregion Constants

        #region Methods
        
        /// <summary>
        /// Method to generate Password Encryption Key.
        /// This method is main entry point for the application.
        /// </summary>
        public static void GeneratePasswordEncryptionKey()
        {            
            try
            {                
                // Code that runs on application startup
                //Common.MessagePath = Application.StartupPath + "/App_Data/Messages.xml";                    
                string defaultPassword = ConfigurationSettings.AppSettings["DefaultPassword"];
                
                if (!Directory.Exists(Properties.Resources.CryptoKeyPath)) Directory.CreateDirectory(Properties.Resources.CryptoKeyPath);
                if (!File.Exists(Properties.Resources.CryptoKeyPath + "Vestige.key"))
                {
                    using (Stream io = new FileStream(Properties.Resources.CryptoKeyPath + "Vestige.key", FileMode.Append))
                    {
                        CryptographyManager.WriteProtectedKey(io, 
                            CryptographyManager.CreateProtectedKey(CryptographyUtility.GetBytesFromHexString("22E3CA2764D24337430C7081CAB1751CDEC4081DDB8CF64AA292583A607B4A47"), DataProtectionScope.LocalMachine));
                        io.Close();                            
                    }
                    //using (DataTaskManager dtManager = new DataTaskManager())
                    //{
                    //    string adminPwd = (new CryptographyManager(CryptographyProviderType.SymmetricCryptoProvider)).Encrypt(ConfigurationSettings.AppSettings["DefaultPassword"]);
                    //    string sqlQuery = string.Empty;
                    //    //sqlQuery = "IF EXISTS(SELECT * FROM User_Master WHERE UserName = 'Admin' AND Status = 1) ";
                    //    sqlQuery += "UPDATE User_Master SET Password='" + adminPwd + "' WHERE UPPER(UserName) = 'SUPER.ADMIN' AND Status = 1 ";
                    //    //sqlQuery += "ELSE INSERT INTO User_Master(UserId, UserName, Password, FirstName, Status, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) ";
                    //    //sqlQuery += "VALUES(0, 'Admin', '" + adminPwd + "', 'Administrator', 1, 0, GETDATE(), 0, GETDATE())";
                    //    dtManager.ExecuteSqlQuery(sqlQuery);
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Common.LogException(ex);
                //MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10001"), 
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Application.Exit();
            }
        }

        /// <summary>
        /// Method to return encrypted password.
        /// </summary>
        /// <param name="pswdString">Pass the password string.</param>
        /// <returns>Encrypted Password</returns>
        public string EncryptPassword(string pswdString)
        {
            try
            {
                return (new CryptographyManager(CryptographyProviderType.SymmetricCryptoProvider))
                    .Encrypt(pswdString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DecryptPassword(string pswdString)
        {
            try
            {
                return (new CryptographyManager(CryptographyProviderType.SymmetricCryptoProvider))
                    .Decrypt(pswdString);

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Methods

        #region IUser Members

        /// <summary>
        /// This method is implemented to insert/update user and its details like associated
        /// locations and roles.
        /// </summary>
        /// <param name="xmlDoc">XML file containing values of db parameters.</param>
        /// <param name="spName">Name of stored procedure to invoke.</param>
        /// <param name="errorMessage">Error message (if any) returned from stored procedure.</param>
        /// <returns>true/false</returns>
        public bool UserSave(string xmlDoc, string spName, ref string errorMessage)
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
                            dtManager.ExecuteNonQuery(spName, dbParam);
                          
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
        /// This method is implementd to search users and return user details as list of users.
        /// Method will work for returning list of users as well as returning details of particular user i.e. 
        /// associated locations and roles for a single user.
        /// </summary>
        /// <param name="xmlDoc">XML file containing values of search parameters.</param>
        /// <param name="spName">Name of stored procedure to invoke.</param>
        /// <param name="errorMessage">Error message (if any) returned from stored procedure.</param>
        /// <returns>List of User(s)</returns>
        public List<User> UserSearch(string xmlDoc, string spName, ref string errorMessage)
        {
            List<User> lUser = new List<User>();
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

                    using (DataTable dtUserData = dtManager.ExecuteDataTable(spName, dbParam))
                    {
                        // update database message
                        errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                        if (errorMessage.Length == 0 && dtUserData.Rows.Count > 0) //No dbError
                        {
                            for (int i = 0; i < dtUserData.Rows.Count; i++)
                            {
                                User tUser;
                                int currentUserId = Convert.ToInt32(dtUserData.Rows[i]["UserId"]);
                                //Check duplicate UserId via delegate
                                User tempUser = lUser.Find(delegate(User user)
                                {
                                    return user.UserId == currentUserId;
                                });
                                if (tempUser == null) //If same UserId does not exist.
                                {
                                    tUser = new User();
                                    tUser.UserId = Convert.ToInt32(dtUserData.Rows[i]["UserId"]);
                                    tUser.UserName = Convert.ToString(dtUserData.Rows[i]["UserName"]);
                                    tUser.FirstName = Convert.ToString(dtUserData.Rows[i]["FirstName"]);
                                    tUser.LastName = Convert.ToString(dtUserData.Rows[i]["LastName"]);
                                    tUser.Password = Convert.ToString(dtUserData.Rows[i]["Password"]);
                                    tUser.Address1 = Convert.ToString(dtUserData.Rows[i]["Address1"]);
                                    tUser.Address2 = Convert.ToString(dtUserData.Rows[i]["Address2"]);
                                    tUser.Address3 = Convert.ToString(dtUserData.Rows[i]["Address3"]);
                                    tUser.Title = Convert.ToInt32(dtUserData.Rows[i]["Title"]);
                                    tUser.Dob = Convert.ToDateTime(dtUserData.Rows[i]["dob"]).ToString(Common.DATE_TIME_FORMAT);
                                    tUser.Designation = Convert.ToString(dtUserData.Rows[i]["Designation"]);
                                    tUser.Email1 = Convert.ToString(dtUserData.Rows[i]["Emailid1"]);

                                    tUser.CityId = Convert.ToInt32(dtUserData.Rows[i]["CityId"]);
                                    tUser.City = Convert.ToString(dtUserData.Rows[i]["City"]);

                                    tUser.PinCode = Convert.ToString(dtUserData.Rows[i]["Pincode"]);

                                    tUser.StateId = Convert.ToInt32(dtUserData.Rows[i]["StateId"]);
                                    tUser.State = Convert.ToString(dtUserData.Rows[i]["State"]);

                                    tUser.CountryId = Convert.ToInt32(dtUserData.Rows[i]["CountryId"]);
                                    tUser.Country = Convert.ToString(dtUserData.Rows[i]["Country"]);

                                    tUser.PhoneNumber1 = Convert.ToString(dtUserData.Rows[i]["Phone1"]);
                                    tUser.Mobile1 = Convert.ToString(dtUserData.Rows[i]["Mobile1"]);
                                    tUser.Fax1 = Convert.ToString(dtUserData.Rows[i]["Fax1"]); 
                                   
                                    tUser.Status = Convert.ToInt32(dtUserData.Rows[i]["Status"]);
                                    tUser.StatusValue = Convert.ToString(dtUserData.Rows[i]["StatusValue"]);
                                    tUser.ModifiedDate = Convert.ToDateTime(dtUserData.Rows[i]["ModifiedDate"]).ToString(Common.DATE_TIME_FORMAT);

                                    //Loop for Location-Roles
                                    List<LocationRole> lLocRole = new List<LocationRole>();
                                    DataRow[] drLocRole = dtUserData.Select("UserId = " + currentUserId);
                                    for (int j = 0; j < drLocRole.Length; j++)
                                    {
                                        LocationRole tLocRole = new LocationRole();
                                        tLocRole.LocationId = Convert.ToInt32(drLocRole[j]["LocationId"]);
                                        tLocRole.LocationName = Convert.ToString(drLocRole[j]["LocationName"]);
                                        tLocRole.RoleId = Convert.ToInt32(drLocRole[j]["RoleId"]);
                                        tLocRole.RoleName = Convert.ToString(drLocRole[j]["RoleName"]);

                                        //Add one by one tLocRole object to lLocRole list
                                        lLocRole.Add(tLocRole);
                                    } //Loop Location-Roles
                                    //Assign list of Location-Role to User object
                                    tUser.LocationRoles = lLocRole;
                                    //Add one by one tUser object to lUser list
                                    lUser.Add(tUser);
                                }
                            } //Loop Roles
                        }
                    }
                }
            }
            catch { throw; }
            return lUser;
        }

        #endregion
    }
    
}
