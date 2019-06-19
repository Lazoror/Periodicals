using PeriodicalsFinal.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodicalsFinal.Controllers
{

    [Authorize(Roles = "Admin, Publisher")]
    public class ArticleController : Controller
    {
        [Authorize]
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleModel article)
        {

            return View();
        }
    }
}