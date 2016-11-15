using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WSS.CustomerApplication.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult ErrorPage()
        {
            return View();
        }

        public ActionResult HttpError404()
        {
            return View();
        }

        public ActionResult HttpError500()
        {
            return View();
        }

        public ActionResult HttpError400()
        {
            return View();
        }

        public ActionResult HttpError408()
        {
            return View();
        }

        public ActionResult HttpError421()
        {
            return View();
        }

        public ActionResult HttpError502()
        {
            return View();
        }

        public ActionResult HttpError503()
        {
            return View();
        }

        public ActionResult HttpError504()
        {
            return View();
        }
    }
}