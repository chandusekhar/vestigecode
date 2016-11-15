using WSS.InternalApplication.Authorization;

namespace WSS.InternalApplication.CustomAttributes
{
    using System.Data.Entity;
    using System.Linq;
    using System.Security.Principal;
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;
    using WWDCommon.Data;

    public class WSSAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>();
            var dbUser = unitOfWork.UserRepository.FindAll().Include(u => u.Roles).FirstOrDefault(user => user.Username == filterContext.HttpContext.User.Identity.Name);
            filterContext.Principal = new WSSPrincipal((WindowsIdentity)filterContext.HttpContext.User.Identity, dbUser.Username);
            if (dbUser == null)
            {
                //				filterContext.Result = new RedirectResult("/Error/NotAuthorised");
                //throw new HttpException(401, "Auth Failed");
                filterContext.Result = new HttpUnauthorizedResult("Auth Failed");
            }
            if (!dbUser.Roles.Any())
            {
                filterContext.Result = new HttpUnauthorizedResult();
                //				throw new HttpException(401, "Auth Failed");
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}