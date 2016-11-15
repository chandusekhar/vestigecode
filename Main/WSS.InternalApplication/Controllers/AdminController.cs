using System.Web.Mvc;

namespace WSS.InternalApplication.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(WSS.Email.Service.SendEmail mail) : base(mail)
        {
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}