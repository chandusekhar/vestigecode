using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using WSS.Data;
using Ninject;

namespace WSS.InternalApplication.Authorization
{
    public class WSSRoleProvider : RoleProvider
    {
        [Inject]
        private IUnitOfWork UnitOfWork { get; set; }

        public override bool IsUserInRole(string username, string roleName)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username is null or empty", "username");
            }

            var user = UnitOfWork.UsersRepository.FindAll().FirstOrDefault(n => n.Username == username && !n.isDeleted);
            return user != null;
        }

        public override string[] GetRolesForUser(string username)
        {
            var roles = new string[] { };
            var user = UnitOfWork.UsersRepository.FindAll().FirstOrDefault(n => n.Username == username && !n.isDeleted);
            if (user != null)
            {
                roles = user.Roles.Select(r => r.RoleName).ToArray();
            }
            return roles;
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return UnitOfWork.RolesRepository.FindAll().Any(r => r.RoleName == roleName);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            var list = UnitOfWork.RolesRepository.FindAll().Select(r => r.RoleName).ToArray();
            return list;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            var list =
                UnitOfWork.RolesRepository.FindAll()
                    .Where(r => r.RoleName == roleName && r.Users.Any(u => u.Username == usernameToMatch)).Select(r => r.RoleName);
            return list.ToArray();
        }

        public override string ApplicationName { get; set; }
    }
}