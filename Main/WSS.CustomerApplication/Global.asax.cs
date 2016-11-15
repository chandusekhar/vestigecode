using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WSS.CustomerApplication.App_Start;
using WSS.CustomerApplication.Controllers;

namespace WSS.CustomerApplication
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMaps();
        }

        protected void Application_Error()
        {

            if (Context.IsCustomErrorEnabled)
                ShowCustomErrorPage(Server.GetLastError());

        }

        private void ShowCustomErrorPage(Exception exception)
        {
            var httpException = exception as HttpException ?? new HttpException(500, "Internal Server Error", exception);

            Response.Clear();

            switch (httpException.GetHttpCode())
            {
                case 404:
                    Response.Redirect("~/Error/HttpError404");
                    break;

                case 500:
                    Response.Redirect("~/Error/HttpError500");
                    break;

                case 502:
                    Response.Redirect("~/Error/HttpError502");
                    break;

                case 503:
                    Response.Redirect("~/Error/HttpError503");
                    break;

                case 504:
                    Response.Redirect("~/Error/HttpError504");
                    break;

                case 400:
                    Response.Redirect("~/Error/HttpError400");
                    break;

                case 408:
                    Response.Redirect("~/Error/HttpError408");
                    break;

                case 421:
                    Response.Redirect("~/Error/HttpError421");
                    break;

                default:
                    Response.Redirect("~/Error/ErrorPage");
                    break;
            }
            Server.ClearError();
        }
    }
}