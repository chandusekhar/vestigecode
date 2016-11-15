using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreComponent.Core.BusinessObjects;

namespace AuthenticationComponent.BusinessObjects
{
    public class Authenticate
    {
        private static AppUser m_loggedInUser;

        public static AppUser LoggedInUser
        {
            get { return Authenticate.m_loggedInUser; }
            set { Authenticate.m_loggedInUser = value; }
        }

        public static bool CheckUserIsValid(string userName, string password, string locationId)
        {
            if (AuthenticateUser(userName, password, locationId) == null)
                return false;
            else
                return true;
        }

        public static bool LogInUser(string userName, string password, string locationId)
        {
            m_loggedInUser = AuthenticateUser(userName, password, locationId);
            if (m_loggedInUser == null)
            {
                return false;
            }
            return true;
        }

        public static bool LogOutUser()
        {
            m_loggedInUser = null;
            return true;
        }

        public static AppUser AuthenticateUser(string userName, string password, string locationId)
        {
            return AppUser.LogInUser(userName, password, locationId);
        }

        public static bool IsModuleAccessible(string userName, string locationCode, string moduleCode)
        {
            bool moduleFound = false;
            if (m_loggedInUser != null && m_loggedInUser.UserName == userName)
            {
                moduleFound = IsModuleAccessible(m_loggedInUser, locationCode, moduleCode);
            }
            else
            {
                AppUser user = AppUser.Search(userName, locationCode);
                moduleFound = IsModuleAccessible(user, locationCode, moduleCode);
            }
            return moduleFound;
        }

        private static bool IsModuleAccessible(AppUser user, string locationCode, string moduleCode)
        {
            bool moduleFound = false;
            if (user != null)
            {
                foreach (AppUserLocationRoles lr in user.LocationRoles)
                {
                    if (lr.LocationCode.ToUpper() == locationCode.ToUpper())
                    {
                        foreach (Role r in lr.Roles)
                        {
                            foreach (Module m in r.AssignedModules)
                            {
                                if (m.Code == moduleCode)
                                {
                                    moduleFound = true;
                                    break;
                                }
                            }
                            if (moduleFound) break;
                        }
                    }
                    if (moduleFound) break;
                }
            }
            return moduleFound;
        }

        public static Module GetModuleFromUser(string userName, string locationCode, string moduleCode)
        {
            Module module = null;
            if (m_loggedInUser != null && m_loggedInUser.UserName == userName)
            {
                module = GetModuleFromUser(m_loggedInUser, locationCode, moduleCode);
            }
            else
            {
                AppUser user = AppUser.Search(userName, locationCode);
                module = GetModuleFromUser(user, locationCode, moduleCode);
            }
            return module;
        }

        private static Module GetModuleFromUser(AppUser user, string locationCode, string moduleCode)
        {
            Module module = null;
            if (user != null)
            {
                foreach (AppUserLocationRoles lr in user.LocationRoles)
                {
                    if (lr.LocationCode.ToUpper() == locationCode.ToUpper())
                    {
                        foreach (Role r in lr.Roles)
                        {
                            foreach (Module m in r.AssignedModules)
                            {
                                if (m.Code == moduleCode)
                                {
                                    module = m;
                                    break;
                                }
                            }
                            if (module != null) break;
                        }
                    }
                    if (module != null) break;
                }
            }
            return module;
        }

        public static bool IsFunctionAccessible(string userName, string locationCode, string moduleCode, string functionCode)
        {
            bool functionAccessible = false;
            if (IsModuleAccessible(userName, locationCode, moduleCode))
            {
                Module module = GetModuleFromUser(userName, locationCode, moduleCode);
                if (module != null)
                {
                    foreach (Function f in module.Functions)
                    {
                        if (f.Code == functionCode)
                        {
                            functionAccessible = true;
                            break;
                        }
                    }
                }
            }
            return functionAccessible;
        }

        public static Function GetFunctionFromModule(string userName, string locationCode, string moduleCode, string functionCode)
        {
            Function function = null;
            if (IsModuleAccessible(userName, locationCode, moduleCode))
            {
                Module module = GetModuleFromUser(userName, locationCode, moduleCode);
                if (module != null)
                {
                    foreach (Function f in module.Functions)
                    {
                        if (f.Code == functionCode)
                        {
                            function = f;
                            break;
                        }
                    }
                }
            }
            return function;
        }

        public static bool IsConditionAccessible(string userName, string locationCode, string moduleCode, string functionCode, string conditionCode)
        {
            bool conditionAccessible = false;
            if (IsFunctionAccessible(userName, locationCode, moduleCode, functionCode))
            {
                Function function = GetFunctionFromModule(userName, locationCode, moduleCode, functionCode);
                if (function != null)
                {
                    foreach (Condition c in function.AssignedConditions)
                    {
                        if (c.Code == conditionCode)
                        {
                            conditionAccessible = true;
                            break;
                        }
                    }
                }
            }
            return conditionAccessible;
        }

        public static Condition GetConditionFromFunction(string userName, string locationCode, string moduleCode, string functionCode, string conditionCode)
        {
            Condition condition = null;
            if (IsFunctionAccessible(userName, locationCode, moduleCode, functionCode))
            {
                Function function = GetFunctionFromModule(userName, locationCode, moduleCode, functionCode);
                if (function != null)
                {
                    foreach (Condition c in function.AssignedConditions)
                    {
                        if (c.Code == conditionCode)
                        {
                            condition = c;
                            break;
                        }
                    }
                }
            }
            return condition;
        }

        public static List<Module> AccessibleModules(string userName, string locationCode)
        {
            List<Module> moduleList = new List<Module>();
            if (m_loggedInUser != null && m_loggedInUser.UserName == userName)
            {
                moduleList = AccessibleModules(m_loggedInUser, locationCode);
            }
            else
            {
                moduleList = AccessibleModules(AppUser.Search(userName, locationCode), locationCode);
            }
            return moduleList;
        }

        public static List<Module> AccessibleModules(AppUser user, string locationCode)
        {
            List<Module> mList = new List<Module>();
            if (user != null)
            {
                foreach (AppUserLocationRoles lr in user.LocationRoles)
                {
                    if (lr.LocationCode.ToUpper() == locationCode.ToUpper())
                    {
                        foreach (Role r in lr.Roles)
                        {
                            foreach (Module m in r.AssignedModules)
                            {
                                if (mList.Find(delegate(Module mo) { return mo.ModuleId == m.ModuleId; }) == null)
                                {
                                    mList.Add(m);
                                }
                            }
                        }
                    }
                }
            }
            Module loginModule = new Module();
            loginModule.Code = Common.LoginModuleCode;
            loginModule.Description = "Login";
            loginModule.ModuleId = -1;
            loginModule.Name = "Login";
            loginModule.Status = 1;
            Function fn = new Function();
            fn.Code = "F024";
            fn.Description = "Login";
            fn.FunctionId = -1;
            fn.Name = "Login";
            fn.Status = 1;
            loginModule.Functions.Add(fn);
            Module logoutModule = new Module();
            logoutModule.Code = Common.LogoutModuleCode;
            logoutModule.Description = "Logout";
            logoutModule.ModuleId = -2;
            logoutModule.Name = "Logout";
            logoutModule.Status = 1;
            Function fn1 = new Function();
            fn1.Code = "F025";
            fn1.Description = "Logout";
            fn1.FunctionId = -2;
            fn1.Name = "Logout";
            fn1.Status = 1;
            loginModule.Functions.Add(fn1);
            mList.Add(loginModule);
            mList.Add(logoutModule);
            return mList;
        }
    }
}
