using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using WSS.Data;

namespace WSS.InternalApplication.Authorization
{
    public class WSSPrincipal : WindowsPrincipal
    {
        public WSSPrincipal(WindowsIdentity source, User baseUser) :
            base(source)
        {
            User = baseUser;
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

        public bool CanExecuteFunction(string functionCode)
        {
            if (User == null)
            {
                return false;
            }
            return
                User.Roles.Any(r => r.RoleName == "Admin") ||
                User.Roles.SelectMany(r => r.Functions.Where(f => f.FunctionCode == functionCode)).Any();
        }

        public User User { get; protected set; }

        public bool CanExecuteFunction(IEnumerable<string> functionCodes)
        {
            return functionCodes.Any(CanExecuteFunction);
        }
    }
}