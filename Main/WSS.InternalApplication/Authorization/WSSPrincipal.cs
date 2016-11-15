using System;
using System.Linq;
using System.Security.Principal;
using WWDCommon.Data;

namespace WSS.InternalApplication.Authorization
{
    public class WSSPrincipal : WindowsPrincipal
    {
        public WSSPrincipal(WindowsIdentity source, string UserName) :
            base(source)
        {
            User = WSSRoleProvider.GetUserByName(UserName);
        }

        public override bool IsInRole(string role)
        {
            return (base.IsInRole(role) ||
                    (
                        User != null &&
                        User.Roles != null &&
                        User.Roles.Count > 0 &&
                        User.Roles.Any(r =>
                            r.RoleName == "Admin" ||
                            r.RoleName == role)
                        ));
        }
        public static bool IsUserInRole(string username, string roleName)
        {
            WSSRoleProvider roleProvider = new WSSRoleProvider();
            return roleProvider.IsUserInRole(username, roleName);
        }

        public bool CanExecuteFunction(string functionCode, string roles)
        {
            if (User == null)
            {
                return false;
            }
            return User.Roles.Any(r => roles.Contains(r.RoleName)) || User.Roles.SelectMany(r => r.Functions.Where(f => f.FunctionCode == functionCode)).Any();
        }

        public Users User { get; protected set; }
    }
}