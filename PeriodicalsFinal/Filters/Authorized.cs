using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace PeriodicalsFinal.Filters
{
    public class Authorized : FilterAttribute, IAuthorizationFilter
    {
        public string RolesString { get; set; }

        public Authorized(string roles)
        {
            RolesString = roles;
        }

        public Authorized()
        {

        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var roles = RolesString.Split(',');
            var returnUrl = filterContext.HttpContext.Request["returnUrl"];
            var userRoles = Roles.GetRolesForUser();

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (roles.Any(a => userRoles.Contains(a)))
                {

                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    { "action", "Login" },
                    { "controller", "Account" },
                    { "returnUrl", returnUrl }
                });

            }
        }
    }
}