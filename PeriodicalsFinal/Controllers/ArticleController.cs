using PeriodicalsFinal.DataAccess.Models;
using PeriodicalsFinal.DataAccess.Repository;
using PeriodicalsFinal.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeriodicalsFinal.Controllers
{

    [MyAuthorize]
    public class ArticleController : Controller
    {
        private readonly ArticleRepository _articleRepository = new ArticleRepository();

        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [MyAuthorize(Roles = "Admin, Publisher")]
        public ActionResult Create(string editionId)
        {
           ViewBag.EditionId = editionId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAuthorize(Roles = "Admin, Publisher")]
        public ActionResult Create(ArticleModel article, Guid editionId, string urlReferrer)
        {
            if (ModelState.IsValid)
            {
                article.ArticleId = Guid.NewGuid();
                article.EditionId = editionId;
                article.ArticleStatus = ActiveStatus.Active;
                _articleRepository.Create(article);
                _articleRepository.Save();

                return Redirect(urlReferrer);
            }

            return View(article);
        }
    }
}