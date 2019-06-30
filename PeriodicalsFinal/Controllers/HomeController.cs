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
        private readonly TopicRepository _topicRepository = new TopicRepository();


        public ActionResult Index(string price, string alphabet)
        {
            var editions = _editionRepository.GetAll().Where(a => a.EditionStatus == EditionStatus.Active);

            ViewBag.Topics = _topicRepository.GetAll().Select(a => a.TopicName);

            if (price == "Asc")
            {
                editions = editions.OrderBy(a => a.EditionPrice).ToList();
            }
            else if (price == "Desc")
            {
                editions = editions.OrderByDescending(a => a.EditionPrice).ToList();
            }

            if(alphabet == "Asc")
            {
                editions = editions.OrderBy(a => a.EditionTitle).ToList();
            }
            else if (alphabet == "Desc")
            {
                editions = editions.OrderByDescending(a => a.EditionTitle).ToList();
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

