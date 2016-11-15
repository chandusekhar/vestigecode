using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WSS.InternalApplication.Authorization;

namespace WSS.InternalApplication.CustomAttributes
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "We want to support extensibility")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CanExecuteFunctionAttribute : AuthorizeAttribute
    {
        public string FunctionCode { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = (WindowsPrincipal)Thread.CurrentPrincipal;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }
            if (FunctionCode != null)
            {
                return new WSSPrincipal((WindowsIdentity)user.Identity, user.Identity.Name).CanExecuteFunction(FunctionCode, this.Roles);
            }
            // else
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //const string redirectUrl = "/Errors/NotAuthenticated";
            //if (filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    var urlHelper = new UrlHelper(filterContext.RequestContext);
            //    var path = urlHelper.Action("NotAuthenticated", "Errors", filterContext.RequestContext.HttpContext.Request.Url.Scheme);
            //    filterContext.RequestContext.HttpContext.Response.Redirect(redirectUrl);
            //}
        }
    }
}