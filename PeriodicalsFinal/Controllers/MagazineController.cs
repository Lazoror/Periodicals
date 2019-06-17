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
        private readonly RoleRepository roleRepository = new RoleRepository();
        private readonly EditionRepository editionRepository = new EditionRepository();



        [AllowAnonymous]
        public ActionResult Index()
        {
            

            string userID = User.Identity.GetUserId();

            //ViewBag.Subs = db.Subscriptions.Where(a => a.UserId == userID).ToList();

            return View();
        }

        [Authorize(Roles = "Publisher,Admin")]
        public ActionResult Create()
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