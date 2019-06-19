using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PeriodicalsFinal.DataAccess;
using PeriodicalsFinal.DataAccess.DAL;
using PeriodicalsFinal.DataAccess.Models;
using PeriodicalsFinal.DataAccess.Repository;

namespace PeriodicalsFinal.Controllers
{
    public class HomeController : Controller
    {

        private readonly EditionRepository _editionRepository = new EditionRepository();

        public ActionResult Index()
        {
            var editions = _editionRepository.GetAll();

            ViewBag.Editions = editions;

            return View();
        }

       

    }
}