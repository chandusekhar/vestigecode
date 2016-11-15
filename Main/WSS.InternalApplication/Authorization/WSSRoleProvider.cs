using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;
using WWDCommon.Data;

namespace WSS.InternalApplication.Authorization
{
    public class WSSRoleProvider : RoleProvider
    {
        //[Inject]
        private static IUnitOfWork UnitOfWork;

        static WSSRoleProvider()
        {
            WWDCommon.Data.WDDCommonContext ccc = new WDDCommonContext();
            UnitOfWork = new UnitOfWork(ccc);
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username is null or empty", "username");
            }

            var user = UnitOfWork.UserRepository.FindAll().FirstOrDefault(n => n.Username == username && !n.isDeleted);

            if (user != null)
            {
                var userRoles = GetRolesForUser(username);

                return userRoles.Any(x => x.Equals(roleName));
            }
            return false;
        }
        public override string[] GetRolesForUser(string username)
        {
            var roles = new string[] { };
            var user = UnitOfWork.UserRepository.FindAll().FirstOrDefault(n => n.Username == username && !n.isDeleted);
            if (user != null)
            {
                roles = user.Roles.Select(r => r.RoleName).ToArray();
            }
            return roles;
        }

        public override void CreateRole(string roleName)
        {
            var role = new WWDCommon.Data.Roles();
            role.RoleId = 0;
            role.RoleName = roleName;
            role.RoleDescription = roleName;

            UnitOfWork.RolesRepository.Insert(role);
            UnitOfWork.Save("0");
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            try
            {
                var role = new WWDCommon.Data.Roles();
                role.RoleId = 0;
                role.RoleName = roleName;
                role.RoleDescription = roleName;

                UnitOfWork.RolesRepository.Delete(role);
                UnitOfWork.Save("0");
                return true;
            }
            catch { }
            return false;
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
            var users = new string[] { };
            var role = UnitOfWork.RolesRepository.FindAll().FirstOrDefault(x => x.RoleName == roleName);
            if (role != null)
            {
                users = role.Users.Select(u => u.Username).ToArray();
            }
            return users;
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

        public void CreateFunction(string functionName)
        {
            var function = new WWDCommon.Data.Functions();
            function.FunctionId = 0;
            function.FunctionName = functionName;
            function.FunctionDescription = functionName;
            UnitOfWork.FunctionRespository.Insert(function);
            UnitOfWork.Save("0");
        }

        public static string[] GetFunctionsByRole(string functionName, string roleName)
        {
            var functions = new string[] { };
            var roleId = UnitOfWork.RolesRepository.FindAll().FirstOrDefault(u => u.RoleName == roleName).RoleId;
            return functions;// UnitOfWork.RoleFunctionRespository.FindAll().Where(x => x.RoleId == roleId).Select(x => x.Functions.FunctionName).ToArray();
        }

        public static string[] GetFunctionsByUser(string functionName, string userName)
        {
            var functions = new List<string>();
            var users = UnitOfWork.UserRepository.FindAll().Where(u => u.Username == userName).ToArray();
            foreach (var user in users)
            {
                var roles = user.Roles;
                foreach (var role in roles)
                {
                    var func = role.Functions.Select(f => f.FunctionName).ToList();
                    functions.AddRange(func);
                }
            }

            return functions.ToArray();
        }

        public static WWDCommon.Data.Users GetUserByName(string name)
        {
            return UnitOfWork.UserRepository.FindAll().Include(u => u.Roles).FirstOrDefault(x => x.Username == name);
        }

        public override string ApplicationName { get; set; }
    }
}