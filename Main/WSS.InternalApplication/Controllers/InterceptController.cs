using System.Web.Mvc;
using WSS.InternalApplication.Authorization;
using WSS.InternalApplication.CustomAttributes;

namespace WSS.InternalApplication.Controllers
{
    public class InterceptController : BaseController
    {
        public InterceptController(WSS.Email.Service.SendEmail mail) : base(mail)
        {
        }

        // GET: Intercept

        public ActionResult Index()
        {
            //ViewBag.IsMenuHidden = "hidden";
            return View();
        }

        [HttpGet]
        public ActionResult DefaultSearch()
        {
            return View();
        }
    }
}