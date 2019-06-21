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
using NLog;

namespace PeriodicalsFinal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Set Initializer for Database
            Database.SetInitializer(new ApplicationInitializer());
            using (var context = new ApplicationDbContext())
            {
                context.Database.Initialize(true);
            }
        }

        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            // Log the error!
            _Logger.Trace($"URL:{Request.Url} \n {exception}");

        }
    }
}
