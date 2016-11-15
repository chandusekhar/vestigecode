using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthenticationComponent.BusinessObjects
{
    public interface IAuthenticate
    {
        IUser AuthenticateUser(int userId, string password, string locationId);
        bool IsModuleAccessible(int userId, string locationCode, string moduleCode);
        bool IsFunctionAccessible(int userId, string locationCode, string moduleCode, string functionCode);
        bool IsConditionAccessible(int userId, string locationCode, string moduleCode, string functionCode, string conditionCode);
        List<Module> AccessibleModules(int userId, string locationCode);
        List<Function> FunctionsAccessibleInModule(int userId, string locationCode, string moduleCode);
    }
}
