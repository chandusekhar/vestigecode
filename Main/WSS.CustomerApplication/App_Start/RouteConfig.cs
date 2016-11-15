using System.Web.Mvc;
using System.Web.Routing;

namespace WSS.CustomerApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                name: "Defaultaction",
                url: "{controller}/{actiontoken}",
                defaults: new { controller = "Action", action = "Index", actiontoken = UrlParameter.Optional }
            );*/

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}