using System.Security.Principal;
using System.Threading;
using System.Web.Mvc;
using WSS.InternalApplication.Authorization;

namespace WSS.InternalApplication.Controllers
{
    public class RoutingController : Controller
    {
        // GET: Routing
        public ActionResult Index()
        {
            var currentUser = (WindowsPrincipal)Thread.CurrentPrincipal;
            var currentUserName = currentUser.Identity.Name;
            var roleProvider = new WSSRoleProvider();

            if (roleProvider.IsUserInRole(currentUserName, Roles.IntrcGA) || roleProvider.IsUserInRole(currentUserName, Roles.IntrcWithRemove))
            {
                // Bill Intercept App 
                return RedirectToAction("Index", "DocumentSearch");
            }
            else
            {
                //  CSR App 
                return RedirectToAction("Index", "Search");
            }
        }
    }
}