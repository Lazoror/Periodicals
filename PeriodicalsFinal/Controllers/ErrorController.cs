using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodicalsFinal.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        public ActionResult HttpError404(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        public ActionResult HttpError500(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        public ActionResult Unauthorized(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}