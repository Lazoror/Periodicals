using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PeriodicalsFinal.DataAccess.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeriodicalsFinal.DataAccess.Repository;
using PeriodicalsFinal.DataAccess.Models;
using System.IO;
using PeriodicalsFinal.Attributes;

namespace PeriodicalsFinal.Controllers
{
    [MyAuthorize(Roles = "Admin")]
    public class MagazineController : Controller
    {
        private readonly EditionRepository _editionRepository = new EditionRepository();
        private readonly MagazineRepository _magazineRepository = new MagazineRepository();
        private readonly TopicRepository _topicRepository = new TopicRepository();

        

        // GET: Edition
        public ActionResult Index()
        {
            return View();
        }

        

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MagazineModel edition, string deleteConfirm)
        {
            if(deleteConfirm == "Yes")
            {
                _magazineRepository.Delete(edition);
                _magazineRepository.Save();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MagazineModel magazine)
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult UnauthorizedError()
        {
            return View();
        }
    }
}