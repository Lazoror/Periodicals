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

namespace PeriodicalsFinal.Controllers
{
    [Authorize]
    public class MagazineController : Controller
    {
        private readonly RoleRepository _roleRepository = new RoleRepository();
        private readonly EditionRepository _editionRepository = new EditionRepository();


        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(EditionModel edition)
        {
            if (ModelState.IsValid)
            {
                edition.EditionStatus = EditionStatus.Creating;
                
            }


            return View(edition);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit()
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