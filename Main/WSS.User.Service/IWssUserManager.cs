using System;

namespace WSS.User.Service
{
    public interface IWssUserManager
    {
        void AddUser(LDAPUser user);

        void EditUser(LDAPUser user);

        /// <summary>
        /// Check if a user exists in the system by email (user name)
        /// </summary>
        /// <param name="email">email to check existance of</param>
        /// <returns>true if user exists false if not.</returns>
        bool UserExists(string email);

        /// <summary>
        /// Mark a WSS User as deleted.
        /// </summary>
        /// <param name="user"></param>
        void DeleteUser(LDAPUser user);

        /// <summary>
        /// Find WSS User by ccb Account Number
        /// </summary>
        /// <param name="email"></param>
        /// <returns>WssAccount Object</returns>
        LDAPUser FindUser(string email);

        /// <summary>
        /// Get a WSS User by it's WSS Index
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns>WssAccount Object</returns>
        LDAPUser GetUser(Guid userGuid);

        /// <summary>
        /// set the Password on a WSSAccount
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newPassword"></param>
        void SetPassword(LDAPUser user, string newPassword);
    }
}