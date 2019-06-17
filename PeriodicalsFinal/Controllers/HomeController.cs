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

namespace PeriodicalsFinal.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {

            



            //var roles = new List<IdentityRole>
            //{
            //    new IdentityRole { Name = "User" },
            //    new IdentityRole { Name = "Admin" },
            //    new IdentityRole { Name = "Publisher" }
            //};

            //foreach (IdentityRole role in roles)
            //{
            //    if (!_roleManager.RoleExists(role.Name))
            //    {
            //        _roleManager.Create(role);
            //    }

            //}

            return View();
        }

       

    }
}