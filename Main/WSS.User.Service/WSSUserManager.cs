using System;
using System.Configuration;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using WSS.Logging.Service;

namespace WSS.User.Service
{
    /// <summary>
    /// Methods to Add, Edit, Delete and Retrieve WSS Users
    /// </summary>
    public class WssUserManager : IWssUserManager
    {
        private readonly bool _bypassLdap = (ConfigurationManager.AppSettings["WSS.LDAP.Bypass"] ?? "false").Equals("true");
        private static readonly ILogger Logger = new Logger(typeof(WssUserManager));

        public void AddUser(LDAPUser user)
        {
            // DF - Enables us to configure the ability to bypass the ldap server outside the code
            if (_bypassLdap) return;

            var decoratedName = "CN=" + user.UId;

            var objAdam = new DirectoryEntry(BindingPath("")); // Binding object.
            objAdam.RefreshCache();
            var objUser = objAdam.Children.Add(decoratedName, "user");
            objUser.Properties["displayName"].Add(user.UId);
            objUser.Properties["userPrincipalName"].Add(user.UId);
            objUser.CommitChanges();
        }

        private string BindingPath(string decorateduserName)
        {
            const string parentContainer = "CN=Users"; // no spaces should be present
            var dlsConnectionString = ConfigurationManager.ConnectionStrings["DLSConnectionString"].ConnectionString;
            if (decorateduserName.Length > 0)
                dlsConnectionString = dlsConnectionString.Replace(parentContainer, decorateduserName + parentContainer);

            return dlsConnectionString;
        }

        public void EditUser(LDAPUser user)
        {
            // DF - Enables us to configure the ability to bypass the ldap server outside the code
            if (_bypassLdap) return;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check if a user exists in the system by email (user name)
        /// </summary>
        /// <param name="email">email to check existance of</param>
        /// <returns>true if user exists false if not.</returns>
        public bool UserExists(string email)
        {
            // DF - Enables us to configure the ability to bypass the ldap server outside the code
            // Returning false by default here so we can continue to register new users
            if (_bypassLdap) return false;

            throw new NotImplementedException();
        }

        /// <summary>
        /// Mark a WSS User as deleted.
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(LDAPUser user)
        {
            // DF - Enables us to configure the ability to bypass the ldap server outside the code
            if (_bypassLdap) return;

            var decoratedUserName = "CN=" + user.UId;
            // Get AD LDS object.
            // bindingPath Example: "LDAP://servername:port/OU=securitygroup1, OU=securitygroup2, DC=OrgName, DC=com";
            var objAdam = new DirectoryEntry(BindingPath(""));  // Binding object.
            objAdam.RefreshCache();
            var objUser = objAdam.Children.Find(decoratedUserName, "user");   // User object.
            objAdam.Children.Remove(objUser);
        }

        /// <summary>
        /// Find WSS User by ccb Account Number
        /// </summary>
        /// <param name="email"></param>
        /// <returns>WssAccount Object</returns>
        public LDAPUser FindUser(string email)
        {
            // DF - Enables us to configure the ability to bypass the ldap server outside the code
            if (_bypassLdap) return null;

            try
            {
                var decoratedUserName = "CN=" + email;
                // Get AD LDS object.
                // bindingPath Example: "LDAP://servername:port/OU=securitygroup1, OU=securitygroup2, DC=OrgName, DC=com";
                var objAdam = new DirectoryEntry(BindingPath("")); // Binding object.
                objAdam.RefreshCache();
                var objUser = objAdam.Children.Find(decoratedUserName, "user"); // User object.

                var result = new LDAPUser
                {
                    UId = objUser.Name,
                    Guid = objUser.Guid
                };

                return result;
            }
            catch (COMException cex)
            {
                Logger.Info($"COM Exception occurred when attempting to find the user by e-mail address.  E-mail address: {email}", cex);
                return null;
            }
        }

        /// <summary>
        /// Get a WSS User by it's WSS Index
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns>WssAccount Object</returns>
        public LDAPUser GetUser(Guid userGuid)
        {
            //TODO Handle the BypassLdap setting as above
            throw new NotImplementedException();
        }

        /// <summary>
        /// set the Password on a WSSAccount
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newPassword"></param>
        public void SetPassword(LDAPUser user, string newPassword)
        {
            // DF - Enables us to configure the ability to bypass the ldap server outside the code
            if (_bypassLdap) return;

            const long adsOptionPasswordPortnumber = 6;
            const long adsOptionPasswordMethod = 7;
            const int adsPasswordEncodeClear = 1;

            var decoratedUserName = "CN=" + user.UId + ",";
            var strPath = BindingPath(decoratedUserName);
            var serverPort = ConfigurationManager.AppSettings["ADLDSHostPort"];

            // Set authentication flags.
            // For non-secure connection, use LDAP port and
            //  ADS_USE_SIGNING |
            //  ADS_USE_SEALING |
            //  ADS_SECURE_AUTHENTICATION
            // For secure connection, use SSL port and
            //  ADS_USE_SSL | ADS_SECURE_AUTHENTICATION
            var authTypes = AuthenticationTypes.Signing |
                            AuthenticationTypes.Sealing |
                            AuthenticationTypes.Secure;

            // Bind to user object using LDAP port.
            var objUser = new DirectoryEntry(strPath, null, null, authTypes);         // User object.
            objUser.RefreshCache();

            // Set port number, method, and password.
            var intPort = int.Parse(serverPort);

            //  Be aware that, for security, a password should
            //  not be entered in code, but should be obtained
            //  from the user interface.
            objUser.Invoke("SetOption", new object[] { adsOptionPasswordPortnumber, intPort });
            objUser.Invoke("SetOption", new object[] { adsOptionPasswordMethod, adsPasswordEncodeClear });
            objUser.Invoke("SetPassword", newPassword);
        }
    }
}