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

        public ActionResult Index(string price, string alphabet)
        {
            var editions = _editionRepository.GetAll().Where(a => a.EditionStatus == EditionStatus.Active);

            if(price == "Asc")
            {
                editions = editions.OrderBy(a => a.EditionPrice);
            }
            else if (price == "Desc")
            {
                editions = editions.OrderByDescending(a => a.EditionPrice);
            }

            if(alphabet == "Asc")
            {
                editions = editions.OrderBy(a => a.EditionTitle);
            }
            else if (price == "Desc")
            {
                editions = editions.OrderByDescending(a => a.EditionTitle);
            }

            Dictionary<EditionModel, IEnumerable<ArticleModel>> editionsAndArticles = new Dictionary<EditionModel, IEnumerable<ArticleModel>>();

            foreach (var edition in editions)
            {
                editionsAndArticles.Add(edition, _editionRepository.GetArticles(edition.EditionId));
            }

            ViewBag.Editions = editionsAndArticles;

            return View();
        }

        public RedirectToRouteResult Search(string searchText)
        {
            if(searchText.Length < 5)
            {
                return RedirectToAction("Index");
            }

            EditionModel edition = _editionRepository.GetAll().FirstOrDefault(a => a.EditionTitle.Contains(searchText));

            return RedirectToAction("Index", "Edition", new { magazine = edition.Magazine.MagazineName, year = edition.EditionYear, month = (int)edition.EditionMonth });
        }
       

    }
}

