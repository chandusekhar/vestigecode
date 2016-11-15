using System.Web.Mvc;

namespace WSS.InternalApplication.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult NotAuthenticated()
        {
            return View();
        }
    }
}