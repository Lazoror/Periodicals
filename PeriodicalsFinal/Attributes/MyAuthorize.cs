using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PeriodicalsFinal.Attributes
{
    public class MyAuthorize : AuthorizeAttribute
    {
        private readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped);
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                    filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);


            if (skipAuthorization) return;

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary {
                        { "action", "Login" },
                        { "controller", "Account" },
                        { "returnUrl", returnUrl }
                    });
                return;
            }

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                _Logger.Trace($"Info: Unauthorized user \n UserName: {filterContext.HttpContext.User.Identity.Name} \n " +
                    $"URL: {returnUrl}");
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary {
                        { "action", "Unauthorized" },
                        { "controller", "Error" },
                        { "returnUrl", returnUrl }
                    });
                return;
            }
        }
    }
}