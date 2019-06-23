using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeriodicalsFinal.DataAccess.Repository;
using PeriodicalsFinal.Filters;

namespace PeriodicalsFinal.Controllers
{
    [MyAuthorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleRepository _rolesManager = new RoleRepository();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string role)
        {
            var createrole = _rolesManager.CreateRole(role);

            return Content($"{createrole}");
        }
    }
}