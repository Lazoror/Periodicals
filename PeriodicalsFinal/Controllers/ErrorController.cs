using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodicalsFinal.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(string message)
        {
            return View();
        }

        public ActionResult HttpError404(string message)
        {
            return View();
        }

        public ActionResult HttpError500(string message)
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}