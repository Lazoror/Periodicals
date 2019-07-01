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
        private readonly SubscribeRepository _subscribeRepository = new SubscribeRepository();

        [MyCustomException]
        public ActionResult Index(Guid articleId)
        {
            ArticleModel article = _articleRepository.GetById(articleId);

            if(article != null)
            {
                if(_subscribeRepository.IsUserSubscribed(article.Edition.EditionId, User.Identity.Name) || User.IsInRole("Publisher"))
                {
                    return View(article);
                }
            }

            return RedirectToAction("Index", "Error", new { message = "You're not subscribed. Subscribe first." });
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

        [MyAuthorize(Roles = "Admin, Publisher")]
        public ActionResult Edit(Guid articleId, string urlRefferer)
        {
            ArticleModel article = _articleRepository.GetById(articleId);

            ViewBag.UrlRefferer = urlRefferer;

            return View(article);
        }

        [HttpPost]
        [MyAuthorize(Roles = "Admin, Publisher")]
        public ActionResult Edit(ArticleModel article, Guid editionId, string urlReferrer)
        {
            if (ModelState.IsValid)
            {
                article.EditionId = editionId;
                _articleRepository.Update(article);
                _articleRepository.Save();

                return Redirect(urlReferrer);
            }

            return View(article);
        }
    }
}