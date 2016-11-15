using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using WSS.InternalApplication.App_Start;

namespace WSS.InternalApplication
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMaps();
        }

        //protected void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
        //{
        //    if (User == null)
        //    {
        //        return;
        //    }
        //    var unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>();
        //    var dbUser = unitOfWork.UsersRepository.FindAll().Include(u => u.Roles).FirstOrDefault(user => user.Username == User.Identity.Name);
        //    Thread.CurrentPrincipal =
        //        HttpContext.Current.User = new WSSPrincipal((WindowsIdentity)User.Identity, dbUser);
        //    if (dbUser == null)
        //    {
        //        throw new HttpException(401, "Auth Failed");
        //    }
        //    if (!dbUser.Roles.Any())
        //    {
        //        throw new HttpException(401, "Auth Failed");
        //    }

        //}
    }
}