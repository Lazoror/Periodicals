using System;
using System.Linq;
using System.Web.Mvc;
using PeriodicalsFinal.DataAccess.Models;
using PeriodicalsFinal.DataAccess.Repository;
using System.Data.Entity;
using System.Collections.Generic;

namespace PeriodicalsFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly EditionRepository _editionRepository = new EditionRepository();

        public ActionResult Index()
        {
            var editions = _editionRepository.GetAll().Where(a => a.EditionStatus == EditionStatus.Active);
            Dictionary<EditionModel, IEnumerable<ArticleModel>> editionsAndArticles = new Dictionary<EditionModel, IEnumerable<ArticleModel>>();

            foreach (var edition in editions)
            {
                editionsAndArticles.Add(edition, _editionRepository.GetArticles(edition.EditionId));
            }

            ViewBag.Editions = editionsAndArticles;

            return View();
        }

       

    }
}