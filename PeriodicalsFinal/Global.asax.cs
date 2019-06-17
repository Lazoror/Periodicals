using PeriodicalsFinal.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PeriodicalsFinal.DataAccess.DAL;

namespace PeriodicalsFinal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Set Initializer for Database

            Database.SetInitializer(new ApplicationInitializer());

            //System.Data.Entity.Database.SetInitializer(new PeriodicalsFinal.DataAccess.DAL.AplicationInitializer());

            using (var context = new ApplicationDbContext())
            {
                context.Database.Initialize(true);
            }
        }
    }
}
